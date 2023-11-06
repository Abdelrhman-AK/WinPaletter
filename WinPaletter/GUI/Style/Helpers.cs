using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Style
{
    public class Helpers
    {
        public static bool GetRoundedCorners()
        {
            try
            {
                if (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors)
                {
                    return Program.Settings.Appearance.RoundedCorners;
                }
                else if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    return false;
                }
                else if (OS.W11)
                {
                    return true;
                }
                else if (OS.W10 | OS.W8 | OS.W81)
                {
                    return false;
                }
                else if (OS.W7 || OS.WXP || OS.WVista)
                {
                    return !Program.StartedWithClassicTheme;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void FetchDarkMode()
        {
            try
            {
                if (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors)
                {
                    Program.Style.DarkMode = Program.Settings.Appearance.CustomTheme;
                }

                else
                {
                    int i;

                    if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                    {
                        Program.Style.DarkMode = true;
                    }
                    else
                    {
                        try
                        {
                            if (Program.Settings.Appearance.AutoDarkMode)
                            {
                                if (OS.W11 | OS.W10)
                                {
                                    try
                                    {
                                        i = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 0));
                                        Program.Style.DarkMode = !(i == 1L);
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            Program.Style.DarkMode = Program.Settings.Appearance.DarkMode;
                                        }
                                        catch
                                        {
                                            Program.Style.DarkMode = OS.W11 | OS.W10;
                                        }
                                    }
                                    finally
                                    {
                                        Program.Computer.Registry.CurrentUser.Close();
                                    }
                                }
                                else
                                {
                                    Program.Style.DarkMode = false;
                                }
                            }
                            else
                            {
                                Program.Style.DarkMode = Program.Settings.Appearance.DarkMode;
                            }
                        }
                        catch
                        {
                            try
                            {
                                Program.Style.DarkMode = Program.Settings.Appearance.DarkMode;
                            }
                            catch
                            {
                                Program.Style.DarkMode = OS.W11 | OS.W10;
                            }
                        }
                    }
                }
            }
            catch
            {
                Program.Style.DarkMode = true;
            }
        }

        public static void ApplyStyle(Form Form = null, bool IgnoreTitleBar = false)
        {
            bool DarkMode;
            Color BackColor;
            Color AccentColor;
            bool CustomR = false;

            if (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors)
            {
                BackColor = Program.Settings.Appearance.BackColor;
                AccentColor = Program.Settings.Appearance.AccentColor;
                DarkMode = Program.Settings.Appearance.CustomTheme;
                CustomR = !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81 && !OS.W10;
            }
            else
            {
                DarkMode = Program.Style.DarkMode;  // Must be before BackColor
                BackColor = DarkMode ? DefaultColors.BackColorDark : DefaultColors.BackColorLight;
                AccentColor = DefaultColors.Accent;
                CustomR = false;
            }

            Program.Style = new Config(AccentColor, BackColor, DarkMode);

            if (Form is null)
            {

                // ####################### For all open forms
                try
                {
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form != Forms.BK)
                        {
                            bool FormWasVisible = form.Visible;
                            if (FormWasVisible)
                                form.Visible = false;
                            form.SuspendLayout();
                            form.BackColor = BackColor;

                            if (!IgnoreTitleBar)
                                DLLFunc.DarkTitlebar(form.Handle, DarkMode);

                            ApplyStyleToSubControls(form, DarkMode);

                            if (OS.W11)
                            {
                                int argpvAttribute = (int)DWMAPI.FormCornersType.Default;
                                DWMAPI.DwmSetWindowAttribute(form.Handle, DWMAPI.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                            }
                            if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                            {
                                int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                                DWMAPI.DwmSetWindowAttribute(form.Handle, DWMAPI.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                            }

                            if (FormWasVisible)
                                form.Visible = true;

                            form.ResumeLayout();
                            form.Refresh();
                        }
                    }
                }
                catch { }
            }

            else
            {
                // ####################### For Selected [Form]
                Form.BackColor = BackColor;

                if (!IgnoreTitleBar)
                    DLLFunc.DarkTitlebar(Form.Handle, DarkMode);

                ApplyStyleToSubControls(Form, DarkMode);

                if (OS.W11)
                {
                    int argpvAttribute2 = (int)DWMAPI.FormCornersType.Default;
                    DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute2, Marshal.SizeOf(typeof(int)));
                }

                if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                {
                    int argpvAttribute3 = (int)DWMAPI.FormCornersType.Rectangular;
                    DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute3, Marshal.SizeOf(typeof(int)));
                }

                if ((Form.Name ?? "") == (Forms.ExternalTerminal.Name ?? ""))
                {
                    Forms.ExternalTerminal.Label102.ForeColor = DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
                }

                if ((Form.Name ?? "") == (Forms.MainFrm.Name ?? ""))
                {
                    Forms.MainFrm.status_lbl.ForeColor = DarkMode ? Color.White : Color.Black;
                }

                Form.Invalidate();
            }

        }

        public static void ApplyStyle(IntPtr Handle)
        {
            //var ctrl_theme = Program.Style.DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default;
            bool CustomR = Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors &&
                !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81 && !OS.W10;

            DLLFunc.DarkTitlebar(Handle, Program.Style.DarkMode);

            if (OS.W11)
            {
                int argpvAttribute = (int)DWMAPI.FormCornersType.Default;
                DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
            }
            if (CustomR & !Program.Settings.Appearance.RoundedCorners)
            {
                int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
            }
        }

        private static void ApplyStyleToSubControls(Control ctrl, bool DarkMode)
        {
            // This will make all control have a consistent dark\light mode.
            var ctrl_theme = DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default;

            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81)
                SetControlTheme(ctrl.Handle, ctrl_theme);

            bool b = false;
            if (ctrl is UI.Retro.ButtonR)
                b = true;
            if (ctrl is UI.Retro.PanelR)
                b = true;
            if (ctrl is UI.Retro.PanelRaisedR)
                b = true;
            if (ctrl is UI.Retro.TextBoxR)
                b = true;
            if (ctrl is UI.Retro.WindowR)
                b = true;
            if (ctrl is UI.WP.LabelAlt)
                b = true;

            if (!b)
            {
                switch (DarkMode)
                {
                    case true:
                        {
                            if (ctrl.ForeColor == Color.Black)
                                ctrl.ForeColor = Color.White;
                            break;
                        }
                    case false:
                        {
                            if (ctrl.ForeColor == Color.White)
                                ctrl.ForeColor = Color.Black;
                            break;
                        }
                }
            }

            if (ctrl is UI.WP.GroupBox)
            {
                ((UI.WP.GroupBox)ctrl).BackColor = ctrl.GetParentColor().CB((float)(ctrl.GetParentColor().IsDark() ? 0.04d : -0.05d));
            }

            else if (ctrl is UI.WP.RadioImage)
            {
                ((UI.WP.RadioImage)ctrl).BackColor = ctrl.GetParentColor().CB((float)(ctrl.GetParentColor().IsDark() ? 0.05d : -0.05d));
            }

            else if (ctrl is UI.WP.Button)
            {
                {
                    var temp = (UI.WP.Button)ctrl;
                    temp.BackColor = ctrl.GetParentColor().CB((float)(ctrl.GetParentColor().IsDark() ? 0.04d : -0.03d));
                    if (temp.DrawOnGlass)
                    {
                        temp.ForeColor = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active).GetColor(System.Windows.Forms.VisualStyles.ColorProperty.TextColor).Invert();
                    }
                }
            }

            else if (ctrl is RichTextBox)
            {
                ctrl.BackColor = ctrl.Parent.BackColor;
            }

            else if (ctrl is LinkLabel)
            {
                ((LinkLabel)ctrl).LinkColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is UI.WP.LinkLabel)
            {
                ((UI.WP.LinkLabel)ctrl).LinkColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is TreeView)
            {
                {
                    var temp1 = (TreeView)ctrl;
                    temp1.BackColor = ctrl.Parent.BackColor;
                    temp1.ForeColor = DarkMode ? Color.White : Color.Black;
                }
            }

            else if (ctrl is ListView)
            {
                {
                    var temp3 = (ListView)ctrl;
                    temp3.BackColor = ctrl.Parent.BackColor;
                }
            }

            else if (ctrl is ListBox)
            {
                ctrl.BackColor = ctrl.Parent.BackColor;
            }

            else if (ctrl is CheckedListBox)
            {
                {
                    var temp4 = (CheckedListBox)ctrl;
                    temp4.BackColor = ctrl.Parent.BackColor;
                    temp4.ForeColor = DarkMode ? Color.White : Color.Black;
                }
            }

            else if (ctrl is NumericUpDown)
            {
                {
                    var temp5 = (NumericUpDown)ctrl;
                    temp5.BackColor = ctrl.FindForm().BackColor.CB((float)(0.04d * (DarkMode ? +1 : -1)));
                    temp5.ForeColor = DarkMode ? Color.White : Color.Black;
                }
            }

            else if (ctrl is ComboBox)
            {
                {
                    var temp6 = (ComboBox)ctrl;
                    temp6.FlatStyle = FlatStyle.Flat;
                    temp6.BackColor = ctrl.FindForm().BackColor.CB((float)(0.04d * (DarkMode ? +1 : -1)));
                    temp6.ForeColor = DarkMode ? Color.White : Color.Black;
                }
            }

            else if (ctrl is DataGridView)
            {
                Color ColumnBack;
                Color CellBack;

                switch (DarkMode)
                {
                    case true:
                        {
                            ColumnBack = Program.Style.Colors.Back.Light(0.05f);
                            CellBack = Program.Style.Colors.Back;
                            break;
                        }

                    case false:
                        {
                            ColumnBack = Program.Style.Colors.Back.Dark(0.05f);
                            CellBack = Program.Style.Colors.Back;
                            break;
                        }

                }
                (ctrl as DataGridView).ColumnHeadersDefaultCellStyle.BackColor = ColumnBack;
                (ctrl as DataGridView).BackColor = ctrl.Parent.BackColor;
                (ctrl as DataGridView).BackgroundColor = ctrl.Parent.BackColor;
                (ctrl as DataGridView).DefaultCellStyle.BackColor = CellBack;

            }

            if (ctrl.HasChildren)
            {
                foreach (Control c in ctrl.Controls)
                {
                    if (c is TabPage)
                        c.BackColor = ctrl.Parent.BackColor;
                    ApplyStyleToSubControls(c, DarkMode);
                }
            }

            if (ctrl.FindForm().Visible)
                ctrl.Refresh();
        }

        public enum CtrlTheme
        {
            None,
            Explorer,
            DarkExplorer,
            Default
        }

        public static int SetControlTheme(IntPtr handle, CtrlTheme theme)
        {
            if (handle == IntPtr.Zero)
                return 0;

            switch (theme)
            {
                case CtrlTheme.None:
                    {
                        return UxTheme.SetWindowTheme(handle, "", null);
                    }
                case CtrlTheme.Explorer:
                    {
                        return UxTheme.SetWindowTheme(handle, "Explorer", null);
                    }
                case CtrlTheme.DarkExplorer:
                    {
                        return UxTheme.SetWindowTheme(handle, "DarkMode_Explorer", null);
                    }
                case CtrlTheme.Default:
                    {
                        return UxTheme.SetWindowTheme(handle, null, null);
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
    }
}