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

            if (Accept_Transparent)
            {
                return ctrl.Parent.BackColor;
            }
            else
            {
                try
                {

                    if (ctrl.Parent is null)
                    {
                        return default;
                    }

                    if (ctrl.Parent.BackColor.A == 255)
                    {
                        return Color.FromArgb(255, ctrl.Parent.BackColor);
                    }
                    else
                    {
                        try
                        {
                            Color c1 = ctrl.Parent.BackColor;
                            Color c2;
                            if (!(ctrl.Parent.Parent is Form))
                            {
                                c2 = ctrl.Parent.Parent.BackColor;
                            }
                            else
                            {
                                c2 = ctrl.Parent.FindForm().BackColor;
                            }
                            double amount = c1.A / 255d;
                            byte r = (byte)Math.Round(c1.R * amount + c2.R * (1d - amount));
                            byte g = (byte)Math.Round(c1.G * amount + c2.G * (1d - amount));
                            byte b = (byte)Math.Round(c1.B * amount + c2.B * (1d - amount));
                            return Color.FromArgb(r, g, b);
                        }
                        catch
                        {
                            return ctrl.Parent.BackColor;
                        }
                    }
                }
                catch
                {
                }
            }

            return default;

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
            catch (Exception)
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

        public static void SetText(this Control Ctrl, string text)
        {
            try
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl.Invoke(new setCtrlTxtInvoker(SetText), Ctrl, text);
                }
                else
                {
                    Ctrl.Text = text;
                    Ctrl.Refresh();
                }
            }
            catch
            {

            }
        }
        private delegate void setCtrlTxtInvoker(Control Ctrl, string text);

        public static void SetTag(this Control Ctrl, object Tag)
        {
            try
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl.Invoke(new setCtrlTagInvoker(SetTag), Ctrl, Tag);
                }
                else
                {
                    Ctrl.Tag = Tag;
                }
            }
            catch
            {

            }
        }
        private delegate void setCtrlTagInvoker(Control Ctrl, object Tag);

        public static void AddTreeNode(this TreeView Ctrl, string text, string imagekey)
        {
            try
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl.Invoke(new AddTreeNodeInvoker(AddTreeNode), Ctrl, text, imagekey);
                }
                else
                {
                    {
                        TreeNode temp = Ctrl.Nodes.Add(text);
                        temp.ImageKey = imagekey;
                        temp.SelectedImageKey = imagekey;
                    }
                }
            }
            catch
            {

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
