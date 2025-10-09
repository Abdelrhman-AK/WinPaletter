using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions to <see cref="Control"/> class
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Set the value of a property in an object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetProperty(this Control obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Get the value of a property in an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetProperty<T>(this Control obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanRead)
            {
                return (T)property.GetValue(obj);
            }

            return default;
        }

        /// <summary>
        /// Set the value of a property in an object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetProperty(this object obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Get the value of a property in an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetProperty<T>(this object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanRead)
            {
                return (T)property.GetValue(obj);
            }

            return default;
        }

        private static readonly object lockObject = new();

        /// <summary>
        /// Return graphical state of a control to a bitmap
        /// </summary>
        public static Bitmap ToBitmap(this Control control, bool FixMethod = false)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));

            if (!FixMethod)
            {
                // Keep original semantics: draw into a temp bitmap under the Control lock,
                // then return a detached clone (so caller owns the image).
                using (Bitmap bmp = new(Math.Max(1, control.Width), Math.Max(1, control.Height)))
                {
                    Rectangle rect = new(0, 0, bmp.Width, bmp.Height);
                    lock (control) // preserved from your original code
                    {
                        control.DrawToBitmap(bmp, rect);
                        return (Bitmap)bmp.Clone();
                    }
                }
            }
            else
            {
                return DrawToBitmap(control);
            }
        }

        private static Bitmap DrawToBitmap(Control control)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));

            Bitmap bmp = new(Math.Max(1, control.Width), Math.Max(1, control.Height));

            // Preserve your original behavior: copy controls, reverse array
            Control[] childControls = control.Controls.Cast<Control>().ToArray();
            Array.Reverse(childControls);

            var visibleChildControls = new List<Control>(childControls.Length);

            lock (lockObject)
            {
                // Temporarily hide visible children (in the reversed order you used)
                foreach (Control childControl in childControls)
                {
                    if (childControl.Visible)
                    {
                        visibleChildControls.Add(childControl);
                        childControl.Visible = false;
                    }
                }

                // Draw the parent (without those children)
                control.DrawToBitmap(bmp, control.ClientRectangle);

                // Restore children visibility
                foreach (Control childControl in visibleChildControls)
                    childControl.Visible = true;

                // Draw children manually (same iteration order as your original)
                foreach (Control childControl in childControls)
                {
                    if (!childControl.Visible) continue;

                    if (childControl.Bounds.Width <= 0 || childControl.Bounds.Height <= 0) continue;

                    childControl.DrawToBitmap(bmp, childControl.Bounds);
                }
            }

            return bmp;
        }

        /// <summary>
        /// Gets the effective background color of a control's parent, blending through transparent ancestors until a solid color is reached.
        /// </summary>
        /// <param name="ctrl">The control whose parent color is retrieved.</param>
        /// <param name="acceptTransparent">If true, returns the parent color directly, even if transparent.</param>
        /// <returns>The effective parent color, or <see cref="Color.Empty"/> if none.</returns>
        public static Color GetParentColor(this Control ctrl, bool acceptTransparent = false)
        {
            if (ctrl?.Parent == null) return Color.Empty;

            Color parentColor = ctrl.Parent.BackColor;

            // If transparency is accepted or fully opaque, return directly
            if (acceptTransparent || parentColor.A == 255) return parentColor;

            // Blend recursively with ancestor background
            Color ancestorColor = GetParentColor(ctrl.Parent, acceptTransparent: false);
            if (ancestorColor.IsEmpty) ancestorColor = Program.Style.Schemes.Main.Colors.Back(); // final fallback

            float alpha = parentColor.A / 255f;
            byte r = (byte)(parentColor.R * alpha + ancestorColor.R * (1 - alpha));
            byte g = (byte)(parentColor.G * alpha + ancestorColor.G * (1 - alpha));
            byte b = (byte)(parentColor.B * alpha + ancestorColor.B * (1 - alpha));

            return Color.FromArgb(r, g, b);
        }

        private static readonly PropertyInfo DoubleBufferedProp =
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void DoubleBuffer(this Control control)
        {
            if (control == null) return;

            // Apply double buffering to current control
            DoubleBufferedProp?.SetValue(control, true, null);

            // Recursively apply to children
            foreach (Control child in control.Controls)
            {
                child.DoubleBuffer();
            }
        }

        /// <summary>
        /// Get all controls within a parent control and its children.
        /// </summary>
        /// <param name="parent">The root control.</param>
        /// <param name="sort">Whether to sort controls by Name.</param>
        /// <returns>All descendant controls, optionally sorted.</returns>
        public static IEnumerable<Control> GetAllControls(this Control parent, bool sort = false)
        {
            if (parent == null) yield break;

            IEnumerable<Control> children = parent.Controls.OfType<Control>();

            if (sort) children = children.OrderBy(c => c.Name);

            foreach (var child in children)
            {
                // Recurse into children
                foreach (var grandChild in child.GetAllControls(sort)) yield return grandChild;

                yield return child;
            }
        }

        /// <summary>
        /// Get the depth (level) of a control in the control tree.
        /// </summary>
        /// <param name="control">The control to measure.</param>
        /// <returns>The level (0 = top-level control).</returns>
        public static int Level(this Control control)
        {
            int level = 0;

            for (var parent = control?.Parent; parent != null; parent = parent.Parent) level++;

            return level;
        }

        /// <summary>
        /// Set the text of a control in a thread safe way
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(this Control Ctrl, string text)
        {
            if (Ctrl is not null)
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl?.Invoke(new setCtrlTxtInvoker(SetText), Ctrl, text);
                }
                else
                {
                    Ctrl.Text = text;
                }
            }
        }
        private delegate void setCtrlTxtInvoker(Control Ctrl, string text);

        /// <summary>
        /// Set the tag of a control in a thread safe way
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="Tag"></param>
        public static void SetTag(this Control Ctrl, object Tag)
        {
            if (Ctrl is not null)
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl?.Invoke(new setCtrlTagInvoker(SetTag), Ctrl, Tag);
                }
                else
                {
                    Ctrl.Tag = Tag;
                }
            }
        }
        private delegate void setCtrlTagInvoker(Control Ctrl, object Tag);

        /// <summary>
        /// Add a node to a treeview in a thread safe way
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="text"></param>
        /// <param name="imagekey"></param>
        public static void AddTreeNode(this TreeView Ctrl, string text, string imagekey)
        {
            if (Ctrl is not null)
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl?.Invoke(new AddTreeNodeInvoker(AddTreeNode), Ctrl, text, imagekey);
                }
                else
                {
                    TreeNode temp = Ctrl?.Nodes?.Add(text);
                    if (temp is not null)
                    {
                        temp.ImageKey = imagekey;
                        temp.SelectedImageKey = imagekey;
                    }
                }
            }
        }
        private delegate void AddTreeNodeInvoker(TreeView Ctrl, string text, string imagekey);

        /// <summary>
        /// Perform a step in a progress bar in a thread safe way
        /// </summary>
        /// <param name="ProgressBar"></param>
        public static void PerformStepMethod2(this UI.WP.ProgressBar ProgressBar)
        {
            if (ProgressBar.InvokeRequired)
            {
                ProgressBar.Invoke(new PerformStepMethod2Invoker(PerformStepMethod2), ProgressBar);
            }
            else
            {
                ProgressBar.PerformStep();
            }
        }
        private delegate void PerformStepMethod2Invoker(UI.WP.ProgressBar ProgressBar);

        /// <summary>
        /// Determines whether the specified <see cref="Control"/> is currently in use or disposed.
        /// </summary>
        /// <remarks>This method attempts to access the <see cref="Control.Handle"/> property to determine
        /// the state of the control. If an <see cref="InvalidOperationException"/> is thrown during this access, the
        /// control is considered to be in use or disposed.</remarks>
        /// <param name="control">The <see cref="Control"/> to check. Must not be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="Control"/> is in use or has been disposed; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool IsInUse(this Control control)
        {
            if (control is null) return false;

            try
            {
                _ = control.Handle; // Attempt to access
                return false;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
        }
    }
}
