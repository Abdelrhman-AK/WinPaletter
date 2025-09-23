using Serilog.Events;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Tabs;
using WinPaletter.UI.Retro;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// Class containing helper methods for applying styles to the program's UI.
    /// </summary>
    public partial class Helpers
    {
        /// <summary>
        /// Determines whether rounded corners should be applied to the program's style.
        /// </summary>
        public static void SetRoundedCorners()
        {
            Program.Style.RoundedCorners = GetRoundedCorners();
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"WinPaletter's style will be with {(Program.Style.RoundedCorners ? "rounded" : "sharp")} corners");
        }

        /// <summary>
        /// Returns if rounded corners should be applied to the program's style.
        /// </summary>
        public static bool GetRoundedCorners()
        {
            try
            {
                if (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors)
                {
                    // Check if appearance is managed by theme and custom colors are enabled
                    return Program.Settings.Appearance.RoundedCorners;
                }
                else if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    // Check if running in design mode. Drawing rounded corners in design mode is not necessary.
                    return false;
                }
                else if (OS.W12 || OS.W11)
                {
                    // Windows 11 (and maybe 12 in the future when it will be released) have rounded corners by default
                    return true;
                }
                else if (OS.W10 || OS.W8x)
                {
                    // Windows 10 and 8.x have sharp corners by default
                    return false;
                }
                else if (OS.W7 || OS.WXP || OS.WVista)
                {
                    // Windows 7, Vista and WXP have rounded corners when not using the classic theme
                    return !Program.ClassicThemeRunning;
                }
                else
                {
                    // Default to sharp corners
                    return false;
                }
            }
            catch
            {
                // Fall back to rounded corners if an exception occurs
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
                                Program.Style.DarkMode = !(ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 0) == 1);
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

            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"WinPaletter's style will be {(Program.Style.DarkMode ? "dark" : "light")} mode");
        }

        /// <summary>
        /// Applies a specific style to a form or all open forms based on program settings.
        /// </summary>
        /// <param name="Form">The form to apply the style to. If null, applies to all open forms.</param>
        /// <param name="IgnoreTitleBar">Flag indicating whether to ignore the title bar when applying the style.</param>
        public static void ApplyStyle(Form Form = null, bool IgnoreTitleBar = false)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"WinPaletter is loading style for {Form?.Name ?? "whole application"}");

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
            if (Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors)
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

                BackColor = DarkMode ? DefaultColors.BackColor_Dark : DefaultColors.BackColor_Light;
                AccentColor = DarkMode ? DefaultColors.PrimaryColor_Dark : DefaultColors.PrimaryColor_Light;
                Secondary = DarkMode ? DefaultColors.SecondaryColor_Dark : DefaultColors.SecondaryColor_Light;
                Tertiary = DarkMode ? DefaultColors.TertiaryColor_Dark : DefaultColors.TertiaryColor_Light;
                Disabled = DarkMode ? DefaultColors.DisabledColor_Dark : DefaultColors.DisabledColor_Light;
                Disabled_Background = DarkMode ? DefaultColors.DisabledBackColor_Dark : DefaultColors.DisabledBackColor_Light;

                CustomR = false;
            }

            TextRenderingHint textRenderingHint = Program.Style is not null ? Program.Style.TextRenderingHint : TextRenderingHint.SystemDefault;

            // Set the style for the program by the specified parameters from the previous variables
            Program.Style = new(AccentColor, Secondary, Tertiary, Disabled, BackColor, Disabled_Background, DarkMode, RoundedCorners, Animations)
            {
                TextRenderingHint = textRenderingHint
            };

            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81)
            {
                // try is used as a fallback for Windows 10 1809 and below
                try
                {
                    UxTheme.SetPreferredAppMode(DarkMode ? UxTheme.PreferredAppMode.Dark : UxTheme.PreferredAppMode.Light);
                }
                catch { } // ignored
            }

            if (Form is null)
            {
                // Apply the style to all open forms
                foreach (Form form in Application.OpenForms)
                {
                    if (form != Forms.GlassWindow)
                    {
                        bool FormWasVisible = form.Visible;
                        if (FormWasVisible) form.Visible = false;

                        //form.SuspendLayout();
                        form.BackColor = BackColor;

                        // Make the title bar dark if application mode is dark
                        if (!IgnoreTitleBar) DLLFunc.DarkTitlebar(form.Handle, DarkMode);

                        //// Check if the font "Segoe UI" is available, if not, use "Tahoma" as a fallback
                        //// This part is commented as it caused issues with the DPI scaling on some systems
                        //// and also assiging the font to the form might increase the memory usage
                        //if (!Fonts.Exists("Segoe UI"))
                        //{
                        //    form.AutoScaleMode = AutoScaleMode.Dpi;
                        //    form.Font = new("Tahoma", form.Font.Size, form.Font.Style);
                        //}

                        // Loop through all controls and apply the style to them
                        ApplyStyleToSubControls(form, DarkMode);

                        // Make the form have rounded corners if the operating system is Windows 11 or 12
                        // It should be used as a fallback for the custom styling. Make both start by 'If' statement, not 'Else If'
                        if (OS.W12 || OS.W11)
                        {
                            int argpvAttribute = (int)DWMAPI.FormCornersType.Default;
                            DWMAPI.DwmSetWindowAttribute(form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                        }

                        // Apply rectangular window corners if custom styling is enabled and rounded corners are disabled
                        // Make both start by 'If' statement, not 'Else If'
                        if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                        {
                            int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                            DWMAPI.DwmSetWindowAttribute(form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                        }

                        // Refresh the main groupbox to avoid bugged UI after switching dark mode
                        if (form is Home home) home.panel1.BackColor = home.BackColor;

                        if (FormWasVisible)
                        {
                            //form.ResumeLayout();
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

                // Make the title bar dark if application mode is dark
                if (!IgnoreTitleBar) DLLFunc.DarkTitlebar(Form.Handle, DarkMode);

                //// Check if the font "Segoe UI" is available, if not, use "Tahoma" as a fallback
                //// This part is commented as it caused issues with the DPI scaling on some systems
                //// and also assiging the font to the form might increase the memory usage
                //if (!Fonts.Exists("Segoe UI"))
                //{
                //    Form.AutoScaleMode = AutoScaleMode.Dpi;
                //    Form.Font = new("Tahoma", Form.Font.Size, Form.Font.Style);
                //}

                // Loop through all controls and apply the style to them
                ApplyStyleToSubControls(Form, DarkMode);

                // Make the form have rounded corners if the operating system is Windows 11 or 12
                // It should be used as a fallback for the custom styling. Make both start by 'If' statement, not 'Else If'
                if (OS.W12 || OS.W11)
                {
                    int argpvAttribute2 = (int)DWMAPI.FormCornersType.Default;
                    DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute2, Marshal.SizeOf(typeof(int)));
                }

                // Apply rectangular window corners if custom styling is enabled and rounded corners are disabled
                // Make both start by 'If' statement, not 'Else If'
                if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                {
                    int argpvAttribute3 = (int)DWMAPI.FormCornersType.Rectangular;
                    DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute3, Marshal.SizeOf(typeof(int)));
                }

                // Refresh the main groupbox to avoid bugged UI after switching dark mode
                if (Form is Home home) home.panel1.BackColor = home.BackColor;

                if (Form.Visible) Form.Invalidate();
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
                // Make the title bar dark if application mode is dark
                if (isWindow) DLLFunc.DarkTitlebar(Handle, Program.Style.DarkMode);

                // Make the form have rounded corners if the operating system is Windows 11 or 12
                // It should be used as a fallback for the custom styling. Make both start by 'If' statement, not 'Else If'
                if (OS.W12 || OS.W11)
                {
                    int argpvAttribute = (int)DWMAPI.FormCornersType.Default;
                    DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                }

                // Apply rectangular window corners if custom styling is enabled and rounded corners are disabled
                // Make both start by 'If' statement, not 'Else If'
                if (CustomR && !Program.Settings.Appearance.RoundedCorners)
                {
                    int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                    DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                }
            }

            SetControlTheme(Handle, Program.Style.DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default);
            //IntPtr hDC = User32.GetDC(Handle);
            //User32.SetBkColor(hDC, Program.Style.Schemes.Main.Colors.BackColor.ToArgb() & 0x00FFFFFF);
            //User32.SetTextColor(hDC, (Program.Style.DarkMode ? Color.White : Color.Black).ToArgb() & 0x00FFFFFF);
            //User32.ReleaseDC(Handle, hDC);
        }

        /// <summary>
        /// Applies a specific style to all sub-controls of a control.
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="DarkMode"></param>
        private static void ApplyStyleToSubControls(Control ctrl, bool DarkMode)
        {
            if (ctrl == null) return;

            // Don't apply the style to certain controls (Classic controls)
            bool b = ctrl.GetType().Namespace.ToUpper() == typeof(WindowR).Namespace.ToUpper();

            if (!b)
            {
                // This will make all control have a consistent dark\light mode.
                if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81) SetControlTheme(ctrl.Handle, DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default);

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

                //if (ctrl is not WinElement && ctrl is not Window && (ctrl.Font.Name == "Segoe UI" || ctrl.Font.Name == "Tahoma" || ctrl.Font.Name == "Microsoft Sans Serif") && !Fonts.Exists("Segoe UI")) ctrl.Font = new("Tahoma", ctrl.Font.Size == 9f ? 8.25f : ctrl.Font.Size, ctrl.Font.Style);
            }

            if (ctrl is WP.GroupBox box)
            {
                box.BackColor = ctrl.GetParentColor().CB((float)(ctrl.GetParentColor().IsDark() ? 0.04d : -0.05d));
            }

            else if (ctrl is WP.Button button)
            {
                button.UpdateStyleSchemes();
            }

            else if (ctrl is LinkLabel label)
            {
                label.LinkColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is WP.LinkLabel label1)
            {
                label1.LinkColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is TreeView temp1)
            {
                temp1.BackColor = ctrl.Parent.BackColor;
                temp1.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is ListView temp3)
            {
                temp3.BackColor = ctrl.Parent.BackColor;
                temp3.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is ListBox)
            {
                ctrl.BackColor = ctrl.Parent.BackColor;
            }

            else if (ctrl is CheckedListBox temp4)
            {
                temp4.BackColor = ctrl.Parent.BackColor;
                temp4.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is NumericUpDown temp5)
            {
                temp5.BackColor = ctrl.FindForm().BackColor.CB((float)(0.04d * (DarkMode ? +1 : -1)));
                temp5.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is ComboBox box1 && ctrl is not UI.WP.ComboBox)
            {
                ComboBox temp6 = box1;
                temp6.FlatStyle = FlatStyle.Flat;
                temp6.BackColor = ctrl.FindForm().BackColor.CB((float)(0.04d * (DarkMode ? +1 : -1)));
                temp6.ForeColor = DarkMode ? Color.White : Color.Black;
            }

            else if (ctrl is TitlebarExtender titlebarExtender)
            {
                titlebarExtender.UpdateBackDrop();
                titlebarExtender.Refresh();
            }

            else if (ctrl is DataGridView dataGridView)
            {
                Color Back = Program.Style.Schemes.Main.Colors.Back(ctrl.Level());
                Color BackHover = Program.Style.Schemes.Main.Colors.Back_Hover(ctrl.Level());
                Color Grid = Program.Style.Schemes.Main.Colors.Line_Hover(ctrl.Level());

                dataGridView.EnableHeadersVisualStyles = false;

                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    col.HeaderCell.Style.BackColor = BackHover;
                    col.HeaderCell.Style.ForeColor = DarkMode ? Color.White : Color.Black;
                }

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.HeaderCell.Style.BackColor = BackHover;
                    row.HeaderCell.Style.ForeColor = DarkMode ? Color.White : Color.Black;
                }

                dataGridView.BackColor = ctrl.Parent.BackColor;
                dataGridView.BackgroundColor = ctrl.Parent.BackColor;
                dataGridView.DefaultCellStyle.BackColor = Back;
                dataGridView.GridColor = Grid;
            }

            // Recursively apply the style to all sub-controls
            if (ctrl.HasChildren || ctrl.Controls?.Count > 0)
            {
                foreach (Control c in ctrl.Controls)
                {
                    if (c is TabPage) c.BackColor = ctrl.Parent.BackColor;

                    ApplyStyleToSubControls(c, DarkMode);
                }
            }

            // Invalidate the control to apply the style changes
            ctrl.Invalidate();
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
            /// WinTheme resembling the Explorer theme.
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
            // Check if the handle is valid
            if (handle == IntPtr.Zero) return 0;

            try
            {
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

                return 1;
            }
            catch { return 0; }
        }
    }
}