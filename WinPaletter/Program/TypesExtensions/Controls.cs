using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    public static class ControlExtensions
    {
        /// <summary>
        /// SetStyle to controls without restriction
        /// </summary>
        public static bool SetStyle(this Control c, ControlStyles Style, bool value)
        {
            bool retval = false;
            if (c != null)
            {
                Type typeTB = typeof(Control);
                System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (misSetStyle != null) { misSetStyle.Invoke(c, new object[] { Style, value }); retval = true; }
            }
            return retval;
        }

        // Set the value of a property in an object
        public static void SetProperty(this Control obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value);
            }
        }

        // Get the value of a property in an object
        public static T GetProperty<T>(this Control obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanRead)
            {
                return (T)property.GetValue(obj);
            }

            return default(T);
        }

        // Set the value of a property in an object
        public static void SetProperty(this object obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value);
            }
        }

        // Get the value of a property in an object
        public static T GetProperty<T>(this object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanRead)
            {
                return (T)property.GetValue(obj);
            }

            return default(T);
        }

        /// <summary>
        /// Return graphical state of a control to a bitmap
        /// </summary>
        public static Bitmap ToBitmap(this Control Control, bool FixMethod = false)
        {
            if (!FixMethod)
            {
                using (Bitmap bmp = new(Control.Width, Control.Height))
                {
                    Rectangle rect = new(0, 0, bmp.Width, bmp.Height);
                    lock (Control)
                    {
                        Control.DrawToBitmap(bmp, rect);
                        return (Bitmap)bmp.Clone();
                    }
                }
            }
            else
            {
                return DrawToBitmap(Control);
            }

        }

        private static Bitmap DrawToBitmap(Control Control)
        {
            Control[] childControls = Control.Controls.Cast<Control>().ToArray();
            Array.Reverse(childControls);

            using (Bitmap bmp = new(Control.Width, Control.Height))
            {
                foreach (Control childControl in childControls)
                {
                    childControl.DrawToBitmap(bmp, childControl.Bounds);
                }
                return (Bitmap)bmp.Clone();
            }
        }

        public static void DoubleBufferedControl(Control Control, bool setting)
        {
            Type panType = Control.GetType();
            PropertyInfo pi = panType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(Control, setting, null);
        }

        public static Color GetParentColor(this Control ctrl, bool Accept_Transparent = false)
        {
            if (ctrl is null) return default;
            if (ctrl.Parent is null) return default;

            if (Accept_Transparent)
            {
                return ctrl.Parent.BackColor;
            }
            else
            {
                if (ctrl.Parent.BackColor.A == 255)
                {
                    return Color.FromArgb(255, ctrl.Parent.BackColor);
                }
                else
                {
                    Color c1 = ctrl.Parent.BackColor;
                    Color c2 = ctrl.Parent.BackColor;

                    if (ctrl.Parent.Parent is not null && ctrl.Parent.Parent is not Form)
                    {
                        c2 = ctrl.Parent.Parent.BackColor;
                    }
                    else if (ctrl.Parent.Parent is Form form && form is not null)
                    {
                        c2 = form.BackColor;
                    }

                    float amount = c1.A / 255f;
                    byte r = (byte)Math.Round(c1.R * amount + c2.R * (1f - amount));
                    byte g = (byte)Math.Round(c1.G * amount + c2.G * (1f - amount));
                    byte b = (byte)Math.Round(c1.B * amount + c2.B * (1f - amount));
                    return Color.FromArgb(r, g, b);
                }
            }
        }

        public static void DoubleBuffer(this Control Parent)
        {
            DoubleBufferedControl(Parent, true);

            foreach (Control ctrl in Parent.Controls)
            {
                if (ctrl.HasChildren)
                {
                    foreach (Control c in ctrl.Controls)
                        c.DoubleBuffer();
                }

                DoubleBufferedControl(ctrl, true);
            }
        }

        public static IEnumerable<Control> GetAllControls(this Control parent)
        {
            try
            {
                if (parent != null && parent.Controls.Count > 0)
                {
                    IOrderedEnumerable<Control> cs = parent.Controls.OfType<Control>().OrderBy(c => c.Name);
                    return cs.SelectMany(c => c.GetAllControls()).Concat(cs).OrderBy(c => c.Name);
                }
                else
                {
                    return new List<Control>();
                }

            }
            catch (Exception) // Use another method to get all controls
            {
                if (parent != null && parent.Controls.Count > 0)
                {
                    IEnumerable<Control> cs = parent.Controls.OfType<Control>();
                    return cs.SelectMany(c => c.GetAllControls()).Concat(cs);
                }
                else
                {
                    return new List<Control>();
                }
            }
        }

        /// <summary>
        /// Get level of a control in the control tree
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static int Level(this Control control)
        {
            int level = 0;
            Control currentControl = control;

            while (currentControl.Parent != null)
            {
                currentControl = currentControl.Parent;
                level++;
            }

            return level;
        }

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
    }
}
