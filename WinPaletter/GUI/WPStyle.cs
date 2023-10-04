using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public class WPStyle
    {

        public struct Colors_Structure
        {
            public Color BaseColor;
            public Color Border;
            public Color Border_Checked;
            public Color Border_Checked_Hover;

            public Color Back;
            public Color Back_Checked;
            public Color Core;
            public Color Back_Hover;              // '''''''''''''''''''''''''''''''''''''
            public Color Border_Hover;            // '''''''''''''''''''''''''''''''''''''

            public Color NotTranslatedColor;
            public Color SearchColor;
        }

        public Colors_Structure Colors;

        public Colors_Structure Disabled_Colors = new Colors_Structure();

        public bool DarkMode = true;

        public WPStyle(Color BaseColor, Color BackColor, bool Dark)
        {
            Colors = new Colors_Structure()
            {
                BaseColor = My.Env.DefaultAccent,
                Core = Colors.BaseColor.LightLight(),
                Back = Color.FromArgb(40, 40, 40),
                Back_Hover = Color.FromArgb(55, 55, 55),
                Back_Checked = Colors.BaseColor.Dark(0.2f),
                Border = Color.FromArgb(55, 55, 55),
                Border_Hover = Color.FromArgb(65, 65, 65),
                Border_Checked = Colors.BaseColor.CB(0.08f),
                Border_Checked_Hover = Colors.BaseColor.CB((float)-0.2d)
            };
            DarkMode = Dark;
            Colors.BaseColor = BaseColor;
            Colors.NotTranslatedColor = DarkMode ? Color.FromArgb(125, 20, 30) : Color.FromArgb(255, 136, 127);
            Colors.SearchColor = DarkMode ? Color.FromArgb(4, 94, 53) : Color.FromArgb(255, 255, 163);

            if (DarkMode)
            {
                Colors.Core = Colors.BaseColor.LightLight();

                Colors.Back = BackColor.CB(0.08f);
                Colors.Back_Hover = BackColor.CB(0.2f);

                Colors.Back_Checked = Colors.BaseColor.Dark(0.3f);

                Colors.Border = BackColor.CB(0.05f);
                Colors.Border_Hover = BackColor.CB(0.1f);

                Colors.Border_Checked = Colors.BaseColor.CB((float)-0.2d);
                Colors.Border_Checked_Hover = Colors.BaseColor.CB(0.08f);
            }

            else
            {
                Colors.Core = Colors.BaseColor.Light(0.5f);

                Colors.Back = BackColor.CB((float)-0.15d);
                Colors.Back_Hover = BackColor.CB((float)-0.2d);

                Colors.Back_Checked = Colors.BaseColor.CB(0.6f);

                Colors.Border = BackColor.CB((float)-0.05d);
                Colors.Border_Hover = BackColor.CB((float)-0.1d);

                Colors.Border_Checked = Colors.BaseColor.CB(0.5f);
                Colors.Border_Checked_Hover = Colors.BaseColor.CB(0.3f);
            }

            if (DarkMode)
            {
                Disabled_Colors.Back_Checked = Color.FromArgb(80, 80, 80);
                Disabled_Colors.Core = Color.FromArgb(90, 90, 90);
                Disabled_Colors.Border_Checked_Hover = Color.FromArgb(80, 80, 80);
                Disabled_Colors.Border = Color.FromArgb(90, 90, 90);
                Disabled_Colors.Back = Color.FromArgb(80, 80, 80);
            }
            else
            {
                Disabled_Colors.Back_Checked = Color.FromArgb(180, 180, 180);
                Disabled_Colors.Core = Color.FromArgb(190, 190, 190);
                Disabled_Colors.Border_Checked_Hover = Color.FromArgb(180, 180, 180);
                Disabled_Colors.Border = Color.FromArgb(190, 190, 190);
                Disabled_Colors.Back = Color.FromArgb(180, 180, 180);
            }

        }


        #region Helpers
        public static bool GetRoundedCorners()
        {
            try
            {
                if (My.Env.Settings.Appearance.ManagedByTheme && My.Env.Settings.Appearance.CustomColors)
                {
                    return My.Env.Settings.Appearance.RoundedCorners;
                }
                else if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    return false;
                }
                else if (My.Env.W11)
                {
                    return true;
                }
                else if (My.Env.W10 | My.Env.W8 | My.Env.W81)
                {
                    return false;
                }
                else if (My.Env.W7 || My.Env.WXP || My.Env.WVista)
                {
                    return !My.Env.StartedWithClassicTheme;
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
                if (My.Env.Settings.Appearance.ManagedByTheme && My.Env.Settings.Appearance.CustomColors)
                {
                    My.Env.Style.DarkMode = My.Env.Settings.Appearance.CustomTheme;
                }

                else
                {
                    long i;

                    if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                    {
                        My.Env.Style.DarkMode = true;
                    }
                    else
                    {
                        try
                        {
                            if (My.Env.Settings.Appearance.AutoDarkMode)
                            {
                                if (My.Env.W11 | My.Env.W10)
                                {
                                    try
                                    {
                                        i = Conversions.ToLong(My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").GetValue("AppsUseLightTheme", 0));
                                        My.Env.Style.DarkMode = !(i == 1L);
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            My.Env.Style.DarkMode = My.Env.Settings.Appearance.DarkMode;
                                        }
                                        catch
                                        {
                                            My.Env.Style.DarkMode = My.Env.W11 | My.Env.W10;
                                        }
                                    }
                                    finally
                                    {
                                        My.MyProject.Computer.Registry.CurrentUser.Close();
                                    }
                                }
                                else
                                {
                                    My.Env.Style.DarkMode = false;
                                }
                            }
                            else
                            {
                                My.Env.Style.DarkMode = My.Env.Settings.Appearance.DarkMode;
                            }
                        }
                        catch
                        {
                            try
                            {
                                My.Env.Style.DarkMode = My.Env.Settings.Appearance.DarkMode;
                            }
                            catch
                            {
                                My.Env.Style.DarkMode = My.Env.W11 | My.Env.W10;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.Env.Style.DarkMode = true;
            }
        }

        public static void ApplyStyle(Form Form = null, bool IgnoreTitleBar = false)
        {
            bool DarkMode;
            Color BackColor;
            Color AccentColor;
            bool CustomR = false;

            if (My.Env.Settings.Appearance.ManagedByTheme && My.Env.Settings.Appearance.CustomColors)
            {
                BackColor = My.Env.Settings.Appearance.BackColor;
                AccentColor = My.Env.Settings.Appearance.AccentColor;
                DarkMode = My.Env.Settings.Appearance.CustomTheme;
                CustomR = My.Env.W11;
            }
            else
            {
                DarkMode = My.Env.Style.DarkMode;  // Must be before BackColor
                BackColor = DarkMode ? My.Env.DefaultBackColorDark : My.Env.DefaultBackColorLight;
                AccentColor = My.Env.DefaultAccent;
                CustomR = false;
            }

            My.Env.Style = new WPStyle(AccentColor, BackColor, DarkMode);

            if (Form is null)
            {

                // ####################### For all open forms
                try
                {
                    foreach (Form OFORM in Application.OpenForms)
                    {
                        bool FormWasVisible = OFORM.Visible;
                        if (FormWasVisible)
                            OFORM.Visible = false;
                        OFORM.SuspendLayout();
                        OFORM.BackColor = BackColor;
                        if (!IgnoreTitleBar)
                            DLLFunc.DarkTitlebar(OFORM.Handle, DarkMode);
                        ApplyStyleToSubControls(OFORM, DarkMode);

                        if (My.Env.W11)
                        {
                            int argpvAttribute = (int)Dwmapi.FormCornersType.Default;
                            Dwmapi.DwmSetWindowAttribute(OFORM.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                        }
                        if (CustomR & !My.Env.Settings.Appearance.RoundedCorners)
                        {
                            int argpvAttribute1 = (int)Dwmapi.FormCornersType.Rectangular;
                            Dwmapi.DwmSetWindowAttribute(OFORM.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                        }

                        if (FormWasVisible)
                            OFORM.Visible = true;
                        OFORM.ResumeLayout();
                        OFORM.Refresh();
                    }
                }
                catch
                {

                }
            }

            else
            {
                // ####################### For Selected [Form]
                Form.BackColor = BackColor;

                if (!IgnoreTitleBar)
                    DLLFunc.DarkTitlebar(Form.Handle, DarkMode);
                ApplyStyleToSubControls(Form, DarkMode);

                if (My.Env.W11)
                {
                    int argpvAttribute2 = (int)Dwmapi.FormCornersType.Default;
                    Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute2, Marshal.SizeOf(typeof(int)));
                }
                if (CustomR & !My.Env.Settings.Appearance.RoundedCorners)
                {
                    int argpvAttribute3 = (int)Dwmapi.FormCornersType.Rectangular;
                    Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute3, Marshal.SizeOf(typeof(int)));
                }

                if ((Form.Name ?? "") == (My.MyProject.Forms.ExternalTerminal.Name ?? ""))
                {
                    My.MyProject.Forms.ExternalTerminal.Label102.ForeColor = DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
                }

                if ((Form.Name ?? "") == (My.MyProject.Forms.MainFrm.Name ?? ""))
                {
                    My.MyProject.Forms.MainFrm.status_lbl.ForeColor = DarkMode ? Color.White : Color.Black;
                }

                Form.Invalidate();
            }

        }

        public static void ApplyStyle(IntPtr Handle)
        {
            var ctrl_theme = My.Env.Style.DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default;
            bool CustomR = My.Env.Settings.Appearance.ManagedByTheme && My.Env.Settings.Appearance.CustomColors && My.Env.W11;

            NativeWindow nativeWindow = new();
            nativeWindow.AssignHandle(Handle);

            DLLFunc.DarkTitlebar(nativeWindow.Handle, My.Env.Style.DarkMode);

            if (My.Env.W11)
            {
                int argpvAttribute = (int)Dwmapi.FormCornersType.Default;
                Dwmapi.DwmSetWindowAttribute(nativeWindow.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
            }
            if (CustomR & !My.Env.Settings.Appearance.RoundedCorners)
            {
                int argpvAttribute1 = (int)Dwmapi.FormCornersType.Rectangular;
                Dwmapi.DwmSetWindowAttribute(nativeWindow.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
            }
        }

        private static void ApplyStyleToSubControls(Control ctrl, bool DarkMode)
        {
            // This will make all control have a consistent dark\light mode.
            var ctrl_theme = DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default;
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

            else if (ctrl is TreeView)
            {
                {
                    var temp2 = (TreeView)ctrl;
                    temp2.BackColor = ctrl.Parent.BackColor;
                    temp2.ForeColor = DarkMode ? Color.White : Color.Black;
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
                            ColumnBack = My.Env.Style.Colors.Back.Light(0.05f);
                            CellBack = My.Env.Style.Colors.Back;
                            break;
                        }

                    case false:
                        {
                            ColumnBack = My.Env.Style.Colors.Back.Dark(0.05f);
                            CellBack = My.Env.Style.Colors.Back;
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

        public enum CtrlTheme
        {
            None,
            Explorer,
            DarkExplorer,
            Default
        }
        #endregion

        #region Modern Dialogs

        #region MsgBox
        private static TaskDialog _TD;

        private static TaskDialog TD
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TD;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TD != null)
                {
                    _TD.HyperlinkClicked -= TD_HyperlinkClicked;
                    _TD.RadioButtonClicked -= TD_RadioButtonClicked;
                    _TD.Timer -= TD_Timer;
                    _TD.VerificationClicked -= TD_VerificationClicked;
                    _TD.HelpRequested -= TD_HelpRequested;
                    _TD.ExpandButtonClicked -= TD_ExpandButtonClicked;
                    _TD.ButtonClicked -= TD_ButtonClicked;
                    _TD.Created -= TD_Created;
                }

                _TD = value;
                if (_TD != null)
                {
                    _TD.HyperlinkClicked += TD_HyperlinkClicked;
                    _TD.RadioButtonClicked += TD_RadioButtonClicked;
                    _TD.Timer += TD_Timer;
                    _TD.VerificationClicked += TD_VerificationClicked;
                    _TD.HelpRequested += TD_HelpRequested;
                    _TD.ExpandButtonClicked += TD_ExpandButtonClicked;
                    _TD.ButtonClicked += TD_ButtonClicked;
                    _TD.Created += TD_Created;
                }
            }
        }

        public static string ConvertToLink(string String)
        {
            if (String == null)
                return String;

            var c = new List<string>();
            foreach (string x in String.Split(' '))
            {
                if (Uri.IsWellFormedUriString(x, UriKind.Absolute))
                {
                    c.Add(string.Format("<a href=\"{0}\">{0}</a>", x));
                }
                else
                {
                    c.Add(x);
                }
            }

            return c.CString(" ");
        }

        /// <summary>
        /// Windows Vista/7 Styled MsgBox
        /// </summary>
        /// <param name="Message">The first text in the dialog, with blue color and big font size</param>
        /// <param name="Buttons">The buttons of the dialog</param>
        /// <param name="Icon">The icon of the dialog</param>
        /// <param name="SubMessage">The text which is located under message, with black color and small size (It can accept URLs)</param>
        /// <param name="CollapsedText">Text beside collapsed button</param>
        /// <param name="ExpandedText">Text beside expanded button</param>
        /// <param name="ExpandedDetails">Text appear when the dialog is extended (It can accept URLs)</param>
        /// <param name="DialogTitle">Text of Dialog's Titlebar</param>
        /// <param name="Footer">Footer is the lowermost text (under the buttons) (It can accept URLs)</param>
        /// <param name="FooterIcon">Icon Type of the fotter</param>
        /// <param name="FooterCustomIcon">Icon of the fotter when its type is set to TaskDialogIcon.Custom</param>
        /// <param name="RequireElevation">Put shield icon beside (OK, Yes, Retry) that means Administrator\Elevation is required</param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.None, object SubMessage = null, object CollapsedText = null, object ExpandedText = null, object ExpandedDetails = null, object DialogTitle = null, object Footer = null, TaskDialogIcon FooterIcon = TaskDialogIcon.Custom, Icon FooterCustomIcon = null, bool RequireElevation = false)
        {
            try
            {
                if (!My.Env.WXP)
                {
                    TD = new TaskDialog()
                    {
                        EnableHyperlinks = true,
                        RightToLeft = My.Env.Lang.RightToLeft,
                        ButtonStyle = TaskDialogButtonStyle.Standard,
                        Content = ConvertToLink((SubMessage ?? "").ToString()),
                        FooterIcon = FooterIcon,
                        CenterParent = true
                    };

                    TD.WindowTitle = (DialogTitle ?? My.MyProject.Application.Info.Title).ToString();
                    TD.MainInstruction = (Message ?? "").ToString();
                    TD.CollapsedControlText = (ExpandedText ?? "").ToString();
                    TD.ExpandedControlText = (CollapsedText ?? "").ToString();
                    TD.ExpandedInformation = ConvertToLink((ExpandedDetails ?? "").ToString());
                    TD.Footer = ConvertToLink((Footer ?? "").ToString());

                    if (FooterCustomIcon is null)
                        FooterCustomIcon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
                    else
                        TD.CustomFooterIcon = FooterCustomIcon;

                    var okButton = new TaskDialogButton(ButtonType.Custom) { Text = My.Env.Lang.OK, ElevationRequired = RequireElevation };
                    var yesButton = new TaskDialogButton(ButtonType.Custom) { Text = My.Env.Lang.Yes, ElevationRequired = RequireElevation };
                    var noButton = new TaskDialogButton(ButtonType.Custom) { Text = My.Env.Lang.No };
                    var cancelButton = new TaskDialogButton(ButtonType.Custom) { Text = My.Env.Lang.Cancel };
                    var retryButton = new TaskDialogButton(ButtonType.Custom) { Text = My.Env.Lang.Retry, ElevationRequired = RequireElevation };
                    var closeButton = new TaskDialogButton(ButtonType.Custom) { Text = My.Env.Lang.Close };
                    var customButton = new TaskDialogButton(ButtonType.Custom);
                    TaskDialogIcon icon;

                    if (Buttons == MessageBoxButtons.YesNoCancel)
                    {
                        TD.Buttons.Add(yesButton);
                        TD.Buttons.Add(noButton);
                        TD.Buttons.Add(cancelButton);
                    }
                    else if (Buttons == MessageBoxButtons.YesNo)
                    {
                        TD.Buttons.Add(yesButton);
                        TD.Buttons.Add(noButton);
                    }
                    else if (Buttons == MessageBoxButtons.RetryCancel)
                    {
                        TD.Buttons.Add(retryButton);
                        TD.Buttons.Add(cancelButton);
                    }
                    else if (Buttons == MessageBoxButtons.OKCancel)
                    {
                        TD.Buttons.Add(okButton);
                        TD.Buttons.Add(cancelButton);
                    }
                    else if (Buttons == MessageBoxButtons.OK)
                    {
                        TD.Buttons.Add(okButton);
                    }
                    else
                    {
                        TD.Buttons.Add(okButton);
                    }

                    if (Icon == MessageBoxIcon.Information)
                        icon = TaskDialogIcon.Information;

                    else if (Icon == MessageBoxIcon.Question)
                    {
                        try
                        {
                            My.MyProject.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
                        }
                        catch
                        {
                        }
                        icon = TaskDialogIcon.Custom;

                        TD.CustomMainIcon = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.HELP, Shell32.SHGSI.ICON);
                    }

                    else if (Icon == MessageBoxIcon.Error)
                        icon = TaskDialogIcon.Error;

                    else if (Icon == MessageBoxIcon.Exclamation)
                        icon = TaskDialogIcon.Warning;

                    else
                        icon = TaskDialogIcon.Custom;

                    TD.MainIcon = icon;

                    var result = DialogResult.OK;
                    TaskDialogButton resultButton = TD.ShowDialog();

                    if (resultButton == yesButton)
                    {
                        result = DialogResult.Yes;
                    }
                    else if (resultButton == okButton)
                    {
                        result = DialogResult.OK;
                    }
                    else if (resultButton == noButton)
                    {
                        result = DialogResult.No;
                    }
                    else if (resultButton == cancelButton)
                    {
                        result = DialogResult.Cancel;
                    }
                    else if (resultButton == retryButton)
                    {
                        result = DialogResult.Cancel;
                    }
                    else if (resultButton == closeButton)
                    {
                        result = DialogResult.OK;
                    }
                    else if (resultButton == customButton)
                    {
                        result = DialogResult.OK;
                    }

                    TD.Dispose();
                    resultButton.Dispose();
                    okButton.Dispose();
                    yesButton.Dispose();
                    noButton.Dispose();
                    cancelButton.Dispose();
                    retryButton.Dispose();
                    closeButton.Dispose();
                    customButton.Dispose();

                    return result;
                }

                else
                {
                    return Msgbox_Classic(Message, SubMessage, ExpandedDetails, Footer, DialogTitle, Buttons, Icon);
                }
            }
            catch
            {
                return Msgbox_Classic(Message, SubMessage, ExpandedDetails, Footer, DialogTitle, Buttons, Icon);
            }
        }

        private static DialogResult Msgbox_Classic(object Message, object SubMessage, object ExpandedDetails, object Footer, object DialogTitle, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Information)
        {
            string SM = !string.IsNullOrWhiteSpace((SubMessage ?? "").ToString()) ? "\r\n" + "\r\n" + SubMessage.ToString() : "";
            string ED = !string.IsNullOrWhiteSpace((ExpandedDetails ?? "").ToString()) ? "\r\n" + "\r\n" + ExpandedDetails.ToString() : "";
            string Fo = !string.IsNullOrWhiteSpace((Footer ?? "").ToString()) ? "\r\n" + "\r\n" + Footer.ToString() : "";
            string T = !string.IsNullOrWhiteSpace((DialogTitle ?? "").ToString()) ? DialogTitle.ToString() : My.MyProject.Application.Info.Title;

            return MessageBox.Show(Message + SM + ED + Fo, T, Buttons, Icon);
        }


        #region MsgBox Functions Branches
        public static DialogResult MsgBox(object Message)
        {
            return MsgBox(Message, MessageBoxButtons.OK, MessageBoxIcon.None, null, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons)
        {
            return MsgBox(Message, Buttons, MessageBoxIcon.None, null, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon)
        {
            return MsgBox(Message, Buttons, Icon, null, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, null, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, null, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle, object Footer)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, TaskDialogIcon.Custom, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle, object Footer, TaskDialogIcon FooterIcon)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, FooterIcon, null, false);
        }

        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle, object Footer, TaskDialogIcon FooterIcon, Icon FooterCustomIcon)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, FooterIcon, FooterCustomIcon, false);
        }

        #endregion

        #region Events
        private static void TD_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
        {
            // Process.Start(e.Href)
        }

        private static void TD_RadioButtonClicked(object sender, TaskDialogItemClickedEventArgs e)
        {

        }

        private static void TD_Timer(object sender, TimerEventArgs e)
        {

        }

        private static void TD_VerificationClicked(object sender, EventArgs e)
        {

        }

        private static void TD_HelpRequested(object sender, EventArgs e)
        {

        }

        private static void TD_ExpandButtonClicked(object sender, ExpandButtonClickedEventArgs e)
        {

        }

        private static void TD_ButtonClicked(object sender, TaskDialogItemClickedEventArgs e)
        {

        }

        
        private static void TD_Created(object sender, EventArgs e)
        {
            ApplyStyle(((IWin32Window)sender).Handle);

            foreach (IntPtr i in User32.GetChildWindows(((IWin32Window)sender).Handle))
            {
                ApplyStyle(i);
            }
        }
        #endregion

        #endregion

        #region InputBox
        public static string InputBox(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            try
            {
                if (!My.Env.WXP)
                {
                    var ib = new InputDialog()
                    {
                        MainInstruction = Instruction,
                        Input = Value,
                        Content = Notice,
                        WindowTitle = !string.IsNullOrWhiteSpace(Title) ? Title : My.MyProject.Application.Info.Title
                    };

                    if (ib.ShowDialog() == DialogResult.OK)
                    {
                        string response = ib.Input;
                        if (string.IsNullOrWhiteSpace(response))
                            response = Value;
                        ib.Dispose();
                        return response;
                    }
                    else
                    {
                        ib.Dispose();
                        return Value;
                    }
                }
                else
                {
                    return InputBox_Classic(Instruction, Value, Notice, Title);
                }
            }
            catch
            {
                return InputBox_Classic(Instruction, Value, Notice, Title);
            }
        }


        private static string InputBox_Classic(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            string N = !string.IsNullOrWhiteSpace(Notice) ? "\r\n" + "\r\n" + Notice : "";
            string T = !string.IsNullOrWhiteSpace(Title) ? Title : My.MyProject.Application.Info.Title;

            return Interaction.InputBox(Instruction + N, T, Value);
        }

        #endregion

        #endregion

    }
}