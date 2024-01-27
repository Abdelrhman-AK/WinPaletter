﻿using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Style
{
    public partial class Dialogs
    {
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

            List<string> c = new();
            foreach (string x in String.Split(' '))
            {
                if (Uri.IsWellFormedUriString(x, UriKind.Absolute))
                {
                    c.Add($"<a href=\"{x}\">{x}</a>");
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
        /// <param name="RequireElevation">Put shield icon beside (OK, Yes, Retry) that means administrator\elevation is required</param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.None, object SubMessage = null, object CollapsedText = null, object ExpandedText = null, object ExpandedDetails = null, object DialogTitle = null, object Footer = null, TaskDialogIcon FooterIcon = TaskDialogIcon.Custom, Icon FooterCustomIcon = null, bool RequireElevation = false)
        {
            try
            {
                if (!OS.WXP)
                {
                    TD = new()
                    {
                        EnableHyperlinks = true,
                        RightToLeft = Program.Lang.RightToLeft,
                        ButtonStyle = TaskDialogButtonStyle.Standard,
                        Content = ConvertToLink((SubMessage ?? string.Empty).ToString()),
                        FooterIcon = FooterIcon,
                        CenterParent = true,
                        WindowTitle = (DialogTitle ?? Application.ProductName).ToString(),
                        MainInstruction = (Message ?? string.Empty).ToString(),
                        CollapsedControlText = (ExpandedText ?? string.Empty).ToString(),
                        ExpandedControlText = (CollapsedText ?? string.Empty).ToString(),
                        ExpandedInformation = ConvertToLink((ExpandedDetails ?? string.Empty).ToString()),
                        Footer = ConvertToLink((Footer ?? string.Empty).ToString())
                    };

                    if (FooterCustomIcon is null)
                        FooterCustomIcon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
                    else
                        TD.CustomFooterIcon = FooterCustomIcon;

                    TaskDialogButton okButton = new(ButtonType.Custom) { Text = Program.Lang.OK, ElevationRequired = RequireElevation };
                    TaskDialogButton yesButton = new(ButtonType.Custom) { Text = Program.Lang.Yes, ElevationRequired = RequireElevation };
                    TaskDialogButton noButton = new(ButtonType.Custom) { Text = Program.Lang.No };
                    TaskDialogButton cancelButton = new(ButtonType.Custom) { Text = Program.Lang.Cancel };
                    TaskDialogButton retryButton = new(ButtonType.Custom) { Text = Program.Lang.Retry, ElevationRequired = RequireElevation };
                    TaskDialogButton closeButton = new(ButtonType.Custom) { Text = Program.Lang.Close };
                    TaskDialogButton customButton = new(ButtonType.Custom);
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
                        try { SystemSounds.Exclamation.Play(); }
                        catch { }
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

                    DialogResult result = DialogResult.OK;
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
            string SM = !string.IsNullOrWhiteSpace((SubMessage ?? string.Empty).ToString()) ? $"\r\n\r\n{SubMessage}" : string.Empty;
            string ED = !string.IsNullOrWhiteSpace((ExpandedDetails ?? string.Empty).ToString()) ? $"\r\n\r\n{ExpandedDetails}" : string.Empty;
            string Fo = !string.IsNullOrWhiteSpace((Footer ?? string.Empty).ToString()) ? $"\r\n\r\n{Footer}" : string.Empty;
            string T = !string.IsNullOrWhiteSpace((DialogTitle ?? string.Empty).ToString()) ? DialogTitle.ToString() : Application.ProductName;

            return MessageBox.Show($"{Message}{SM}{ED}{Fo}", T, Buttons, Icon);
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

        #region Events/Overrides
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

        public static void TD_Created(object sender, EventArgs e)
        {
            IntPtr hWnd = (sender as IWin32Window).Handle;

            ApplyStyle(hWnd, true);

            foreach (IntPtr ChildHwnd in User32.GetChildWindowHandles(sender as IWin32Window))
            {
                ApplyStyle(ChildHwnd, false);
            }
        }
        #endregion

    }
}