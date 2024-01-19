using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.Controllers;

namespace WinPaletter.UI.Style
{
    public partial class Helpers
    {
        /// <summary>
        /// Determines whether rounded corners should be applied to the program's style.
        /// </summary>
        public static void GetRoundedCorners()
        {
            try
            {
                if (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors)
                {
                    Program.Style.RoundedCorners = Program.Settings.Appearance.RoundedCorners;
                }

                // Check if running in design mode
                else if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    Program.Style.RoundedCorners = false;
                }

                else if (OS.W12 || OS.W11)
                {
                    Program.Style.RoundedCorners = true;
                }

                else if (OS.W10 || OS.W8 || OS.W81)
                {
                    Program.Style.RoundedCorners = false;
                }

                else if (OS.W7 || OS.WXP || OS.WVista)
                {
                    Program.Style.RoundedCorners = !Program.ClassicThemeRunning;
                }

                else
                {
                    Program.Style.RoundedCorners = false;
                }
            }
            catch
            {
                Program.Style.RoundedCorners = true;
            }
        }

        /// <summary>
        /// Returns if rounded corners should be applied to the program's style.
        /// </summary>
        public static bool FetchRoundedCorners()
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
                else if (OS.W12 || OS.W11)
                {
                    return true;
                }
                else if (OS.W10 || OS.W8 || OS.W81)
                {
                    return false;
                }
                else if (OS.W7 || OS.WXP || OS.WVista)
                {
                    return !Program.ClassicThemeRunning;
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

        /// <summary>
        /// Determines whether dark mode should be applied to the program's style.
        /// </summary>
        public static void GetDarkMode()
        {
            try
            {
                // Default to dark mode
                Program.Style.DarkMode = true;

                // Check if not running in design mode
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                {
                    // Check if appearance is managed by theme and custom colors are enabled
                    if (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors)
                    {
                        Program.Style.DarkMode = Program.Settings.Appearance.CustomTheme_DarkMode;
                    }
                    else
                    {
                        // Attempt to determine dark mode based on settings and operating system
                        if (Program.Settings.Appearance.AutoDarkMode)
                        {
                            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81)
                            {
                                Program.Style.DarkMode = !(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 0)) == 1);
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
                }
            }
            catch
            {
                Program.Style.DarkMode = true;
            }
        }

        /// <summary>
        /// Applies a specific style to a form or all open forms based on program settings.
        /// </summary>
        /// <param name="Form">The form to apply the style to. If null, applies to all open forms.</param>
        /// <param name="IgnoreTitleBar">Flag indicating whether to ignore the title bar when applying the style.</param>
        public static void ApplyStyle(Form Form = null, bool IgnoreTitleBar = false)
        {
            bool DarkMode;
            bool RoundedCorners;
            bool Animations = true;

            Color AccentColor;
            Color Secondary;
            Color Tertiary;
            Color Disabled;
            Color Disabled_Background;
            Color BackColor;

            bool CustomR = false;

            // Check if appearance is managed by theme and custom colors are enabled
            if (Form == Forms.ApplicationThemer || (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors))
            {
                DarkMode = Program.Settings.Appearance.CustomTheme_DarkMode;
                RoundedCorners = Program.Settings.Appearance.RoundedCorners;
                Animations = Program.Settings.Appearance.Animations;

                BackColor = Program.Settings.Appearance.BackColor;
                AccentColor = Program.Settings.Appearance.AccentColor;
                Secondary = Program.Settings.Appearance.SecondaryColor;
                Tertiary = Program.Settings.Appearance.TertiaryColor;
                Disabled = Program.Settings.Appearance.DisabledColor;
                Disabled_Background = Program.Settings.Appearance.DisabledBackColor;

                CustomR = !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81 && !OS.W10;
            }
            else
            {
                DarkMode = Program.Style.DarkMode;
                RoundedCorners = Program.Style.RoundedCorners;
                Animations = Program.Style.Animations;

                BackColor = DarkMode ? DefaultColors.BackColorDark : DefaultColors.BackColorLight;
                AccentColor = DefaultColors.PrimaryColor;
                Secondary = DefaultColors.SecondaryColor;
                Tertiary = DefaultColors.TertiaryColor;
                Disabled = DarkMode ? DefaultColors.DisabledColor_Dark : DefaultColors.DisabledColor_Light;
                Disabled_Background = DarkMode ? DefaultColors.DisabledBackColor_Dark : DefaultColors.DisabledBackColor_Light;

                CustomR = false;
            }

            Program.Style = new(AccentColor, Secondary, Tertiary, Disabled, BackColor, Disabled_Background, DarkMode, RoundedCorners, Animations);

            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81)
            {
                // try is used as a fallback for Windows 10 1809 and below
                try
                {
                    UxTheme.SetPreferredAppMode(DarkMode ? UxTheme.PreferredAppMode.Dark : UxTheme.PreferredAppMode.Light);
                }
                catch { }
            }

            // Apply the style to all open forms
            if (Form is null)
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

                        if (OS.W12 || OS.W11)
                        {
                            int argpvAttribute = (int)DWMAPI.FormCornersType.Default;
                            DWMAPI.DwmSetWindowAttribute(form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                        }

                        if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                        {
                            int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                            DWMAPI.DwmSetWindowAttribute(form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                        }

                        if (FormWasVisible)
                        {
                            form.ResumeLayout();
                            form.Refresh();
                            form.Visible = true;
                        }
                    }
                }
            }

            // Apply the style to the specified form
            else
            {
                Form.BackColor = BackColor;

                if (!IgnoreTitleBar)
                    DLLFunc.DarkTitlebar(Form.Handle, DarkMode);

                ApplyStyleToSubControls(Form, DarkMode);

                if (OS.W12 || OS.W11)
                {
                    int argpvAttribute2 = (int)DWMAPI.FormCornersType.Default;
                    DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute2, Marshal.SizeOf(typeof(int)));
                }

                if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                {
                    int argpvAttribute3 = (int)DWMAPI.FormCornersType.Rectangular;
                    DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute3, Marshal.SizeOf(typeof(int)));
                }

                if (Form.Visible)
                    Form.Invalidate();
            }
        }

        /// <summary>
        /// Applies a specific style to a window identified by its handle.
        /// </summary>
        /// <param name="Handle">The handle of the window to apply the style to.</param>
        /// <param name="isWindow">Specify if the handle is a window or child window (controls)</param>
        public static void ApplyStyle(IntPtr Handle, bool isWindow = true)
        {
            // Determine if custom styling is applicable based on program settings and operating system
            bool CustomR = Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors && !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81 && !OS.W10;

            if (isWindow)
            {
                // Apply dark titlebar style
                if (isWindow) DLLFunc.DarkTitlebar(Handle, Program.Style.DarkMode);

                // Apply specific window corner preference for Windows 11/12
                if (OS.W12 || OS.W11)
                {
                    int argpvAttribute = (int)DWMAPI.FormCornersType.Default;
                    DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                }

                // Apply rectangular window corners if custom styling is enabled and rounded corners are disabled
                if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                {
                    int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                    DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                }
            }

            //SetControlTheme(Handle, Program.Style.DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default);
            //IntPtr hDC = User32.GetDC(Handle);
            //User32.SetBkColor(hDC, Program.Style.Schemes.Main.Colors.BackColor.ToArgb() & 0x00FFFFFF);
            //User32.SetTextColor(hDC, (Program.Style.DarkMode ? Color.White : Color.Black).ToArgb() & 0x00FFFFFF);
            //User32.ReleaseDC(Handle, hDC);
        }

        private static void ApplyStyleToSubControls(Control ctrl, bool DarkMode)
        {
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
            if (ctrl is UI.Retro.LabelR)
                b = true;
            if (ctrl is UI.Retro.ContextMenuR)
                b = true;
            if (ctrl is UI.Retro.MenuBarR)
                b = true;
            if (ctrl is UI.Retro.AppWorkspaceR)
                b = true;
            if (ctrl is UI.Retro.ToolTipR)
                b = true;

            if (!b)
            {
                // This will make all control have a consistent dark\light mode.
                if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81)
                    SetControlTheme(ctrl.Handle, DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default);

                switch (DarkMode)
                {
                    case true:
                        {
                            if (ctrl.ForeColor == Color.Black) ctrl.ForeColor = Color.White;
                            break;
                        }
                    case false:
                        {
                            if (ctrl.ForeColor == Color.White) ctrl.ForeColor = Color.Black;
                            break;
                        }
                }
            }

            if (ctrl is UI.WP.GroupBox)
            {
                ((UI.WP.GroupBox)ctrl).BackColor = ctrl.GetParentColor().CB((float)(ctrl.GetParentColor().IsDark() ? 0.04d : -0.05d));
            }

            else if (ctrl is UI.WP.Button)
            {
                ((UI.WP.Button)ctrl).UpdateStyleSchemes();
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
                TreeView temp1 = (TreeView)ctrl;
                temp1.BackColor = ctrl.Parent.BackColor;
                temp1.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is ListView)
            {
                ListView temp3 = (ListView)ctrl;
                temp3.BackColor = ctrl.Parent.BackColor;
                temp3.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is ListBox)
            {
                ctrl.BackColor = ctrl.Parent.BackColor;
            }

            else if (ctrl is CheckedListBox)
            {
                CheckedListBox temp4 = (CheckedListBox)ctrl;
                temp4.BackColor = ctrl.Parent.BackColor;
                temp4.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is NumericUpDown)
            {
                NumericUpDown temp5 = (NumericUpDown)ctrl;
                temp5.BackColor = ctrl.FindForm().BackColor.CB((float)(0.04d * (DarkMode ? +1 : -1)));
                temp5.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is System.Windows.Forms.ComboBox && ctrl is not UI.WP.ComboBox)
            {
                ComboBox temp6 = (System.Windows.Forms.ComboBox)ctrl;
                temp6.FlatStyle = FlatStyle.Flat;
                temp6.BackColor = ctrl.FindForm().BackColor.CB((float)(0.04d * (DarkMode ? +1 : -1)));
                temp6.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is WP.TitlebarExtender)
            {
                WP.TitlebarExtender titlebarExtender = ctrl as WP.TitlebarExtender;
                if (!titlebarExtender.DropDWMEffect)
                {
                    Config.Scheme scheme = titlebarExtender.Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
                    titlebarExtender.BackColor = scheme.Colors.Back_Hover;
                }

                Forms.MainFrm.tabsContainer1.Refresh();
            }

            else if (ctrl is DataGridView)
            {
                Color ColumnBack;
                Color CellBack;

                switch (DarkMode)
                {
                    case true:
                        {
                            ColumnBack = Program.Style.Schemes.Main.Colors.Back.Light(0.05f);
                            CellBack = Program.Style.Schemes.Main.Colors.Back;
                            break;
                        }

                    case false:
                        {
                            ColumnBack = Program.Style.Schemes.Main.Colors.Back.Dark(0.05f);
                            CellBack = Program.Style.Schemes.Main.Colors.Back;
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
                    if (c is TabPage) { c.BackColor = ctrl.Parent.BackColor; }

                    ApplyStyleToSubControls(c, DarkMode);
                }
            }

            if (ctrl.FindForm().Visible)
                ctrl.Refresh();
        }

        /// <summary>
        /// Enumeration representing different control themes.
        /// </summary>
        public enum CtrlTheme
        {
            /// <summary>
            /// No specific theme.
            /// </summary>
            None,

            /// <summary>
            /// Theme resembling the Explorer theme.
            /// </summary>
            Explorer,

            /// <summary>
            /// Dark-themed Explorer theme.
            /// </summary>
            DarkExplorer,

            /// <summary>
            /// Default system theme.
            /// </summary>
            Default
        }

        /// <summary>
        /// Sets the theme for a control identified by its handle.
        /// </summary>
        /// <param name="handle">The handle of the control.</param>
        /// <param name="theme">The theme to set for the control.</param>
        /// <returns>Zero if successful, otherwise an error code.</returns>
        public static int SetControlTheme(IntPtr handle, CtrlTheme theme)
        {
            if (handle == IntPtr.Zero)  return 0;
              
            try
            {
                // Load the uxtheme.dll library
                IntPtr uxtheme = Kernel32.LoadLibrary("uxtheme.dll");

                switch (theme)
                {
                    case CtrlTheme.None:
                        {
                            // Set the control theme to None
                            UxTheme.SetWindowTheme(handle, string.Empty, null);
                            break;
                        }
                    case CtrlTheme.Explorer:
                        {
                            // Set the control theme to Explorer
                            UxTheme.SetWindowTheme(handle, "Explorer", null);
                            break;
                        }
                    case CtrlTheme.DarkExplorer:
                        {
                            // Set the control theme to DarkExplorer
                            UxTheme.SetWindowTheme(handle, "DarkMode_Explorer", null);
                            break;
                        }
                    case CtrlTheme.Default:
                        {
                            // Set the control theme to the default system theme
                            UxTheme.SetWindowTheme(handle, null, null);
                            break;
                        }
                    default:
                        {
                            UxTheme.SetWindowTheme(handle, null, null);
                            break;
                        }
                }

                // Free the library
                Kernel32.FreeLibrary(uxtheme);

                return 1;
            }
            catch { return 0; }
        }
    }
}