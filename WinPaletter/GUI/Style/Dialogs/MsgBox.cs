using Ookii.Dialogs.WinForms;
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
        /// <summary>
        /// A class instance that provides modern task dialog.
        /// </summary>
        private static TaskDialog _TD;

        /// <summary>
        /// A class instance that provides modern task dialog.
        /// </summary>

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
                // Unsubscribe from the previous instance
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

                // Set the new instance
                _TD = value;

                // Subscribe to the new instance
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

        /// <summary>
        /// Convert URLs to clickable links format
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        public static string ConvertToLink(string String)
        {
            // If the string is null, return it as it is
            if (String == null) return String;

            List<string> c = [];

            // Split the string by spaces
            foreach (string x in String.Split(' '))
            {
                // Check if the string is a valid URL, if so, convert it to a clickable link
                if (Uri.IsWellFormedUriString(x, UriKind.Absolute))
                {
                    c.Add($"<a href=\"{x}\">{x}</a>");
                }
                else
                {
                    // If the string is not a valid URL, add it as it is
                    c.Add(x);
                }
            }

            // Return the formatted string
            return string.Join(" ", c);
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
        /// <param name="FooterIcon">Icon ButtonOverlay of the fotter</param>
        /// <param name="FooterCustomIcon">Icon of the fotter when its type is set to TaskDialogIcon.Custom</param>
        /// <param name="RequireElevation">Put shield icon beside (OK, Yes, Retry) that means administrator\elevation is required</param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.None, object SubMessage = null, object CollapsedText = null, object ExpandedText = null, object ExpandedDetails = null, object DialogTitle = null, object Footer = null, TaskDialogIcon FooterIcon = TaskDialogIcon.Custom, Icon FooterCustomIcon = null, bool RequireElevation = false)
        {
            try
            {
                // Windows XP does not support the modern task dialog.
                if (!OS.WXP)
                {
                    // Create a new instance of the modern task dialog.
                    TD = new()
                    {
                        EnableHyperlinks = true,
                        RightToLeft = Program.Lang.Information.RightToLeft,
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

                    // Get the icon of the footer
                    if (FooterCustomIcon is null)
                        FooterCustomIcon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
                    else
                        TD.CustomFooterIcon = FooterCustomIcon;

                    // Create the buttons of the dialog
                    TaskDialogButton okButton = new(ButtonType.Custom) { Text = Program.Lang.Strings.General.OK, ElevationRequired = RequireElevation };
                    TaskDialogButton yesButton = new(ButtonType.Custom) { Text = Program.Lang.Strings.General.Yes, ElevationRequired = RequireElevation };
                    TaskDialogButton noButton = new(ButtonType.Custom) { Text = Program.Lang.Strings.General.No };
                    TaskDialogButton cancelButton = new(ButtonType.Custom) { Text = Program.Lang.Strings.General.Cancel };
                    TaskDialogButton retryButton = new(ButtonType.Custom) { Text = Program.Lang.Strings.General.Retry, ElevationRequired = RequireElevation };
                    TaskDialogButton closeButton = new(ButtonType.Custom) { Text = Program.Lang.Strings.General.Close };
                    TaskDialogButton customButton = new(ButtonType.Custom);
                    TaskDialogIcon icon;

                    // Set the buttons of the dialog based on the MessageBoxButtons
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

                    // Set information icon based on MessageBoxIcon
                    if (Icon == MessageBoxIcon.Information) icon = TaskDialogIcon.Information;

                    else if (Icon == MessageBoxIcon.Question)
                    {
                        try { SystemSounds.Exclamation.Play(); }
                        catch { } // catch is used as an exception may be thrown if there is no sound driver installed

                        // Set question icon from shell32.dll, not from the system icons as question icon is old and inconsistent with Windows 10 and higher (but consistent with Windows 8.1 and lower)
                        icon = TaskDialogIcon.Custom;

                        TD.CustomMainIcon = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.HELP, Shell32.SHGSI.ICON);
                    }

                    else if (Icon == MessageBoxIcon.Error) icon = TaskDialogIcon.Error;

                    else if (Icon == MessageBoxIcon.Exclamation) icon = TaskDialogIcon.Warning;

                    else icon = TaskDialogIcon.Custom;

                    // Set the icon of the dialog
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

                    // Dispose the dialog and its components to free up memory
                    resultButton?.Dispose();
                    okButton?.Dispose();
                    yesButton?.Dispose();
                    noButton?.Dispose();
                    cancelButton?.Dispose();
                    retryButton?.Dispose();
                    closeButton?.Dispose();
                    customButton?.Dispose();
                    TD?.CustomFooterIcon?.Dispose();
                    FooterCustomIcon?.Dispose();
                    TD?.Dispose();

                    return result;
                }

                else
                {
                    // If the operating system is Windows XP, use the classic message box dialog.
                    return Msgbox_Classic(Message, SubMessage, ExpandedDetails, Footer, DialogTitle, Buttons, Icon);
                }
            }
            catch
            {
                // If an error occurred, use the classic message box dialog.
                return Msgbox_Classic(Message, SubMessage, ExpandedDetails, Footer, DialogTitle, Buttons, Icon);
            }
        }

        /// <summary>
        /// Windows XP Styled MsgBox
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="SubMessage"></param>
        /// <param name="ExpandedDetails"></param>
        /// <param name="Footer"></param>
        /// <param name="DialogTitle"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <returns></returns>
        private static DialogResult Msgbox_Classic(object Message, object SubMessage, object ExpandedDetails, object Footer, object DialogTitle, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Information)
        {
            string SM = !string.IsNullOrWhiteSpace((SubMessage ?? string.Empty).ToString()) ? $"\r\n\r\n{SubMessage}" : string.Empty;
            string ED = !string.IsNullOrWhiteSpace((ExpandedDetails ?? string.Empty).ToString()) ? $"\r\n\r\n{ExpandedDetails}" : string.Empty;
            string Fo = !string.IsNullOrWhiteSpace((Footer ?? string.Empty).ToString()) ? $"\r\n\r\n{Footer}" : string.Empty;
            string T = !string.IsNullOrWhiteSpace((DialogTitle ?? string.Empty).ToString()) ? DialogTitle.ToString() : Application.ProductName;

            return MessageBox.Show($"{Message}{SM}{ED}{Fo}", T, Buttons, Icon);
        }


        #region MsgBox Functions Branches

        /// <summary>
        /// A function that shows a message box with a message and an OK button.
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message)
        {
            return MsgBox(Message, MessageBoxButtons.OK, MessageBoxIcon.None, null, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message and buttons.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons)
        {
            return MsgBox(Message, Buttons, MessageBoxIcon.None, null, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, and an icon.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon)
        {
            return MsgBox(Message, Buttons, Icon, null, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, and a sub-message.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, null, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, a sub-message, and a collapsed text.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <param name="CollapsedText"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, null, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, a sub-message, a collapsed text, and an expanded text.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <param name="CollapsedText"></param>
        /// <param name="ExpandedText"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, null, null, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, a sub-message, a collapsed text, an expanded text, and expanded details.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <param name="CollapsedText"></param>
        /// <param name="ExpandedText"></param>
        /// <param name="ExpandedDetails"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, null, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, a sub-message, a collapsed text, an expanded text, expanded details, and a dialog title.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <param name="CollapsedText"></param>
        /// <param name="ExpandedText"></param>
        /// <param name="ExpandedDetails"></param>
        /// <param name="DialogTitle"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, null, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, a sub-message, a collapsed text, an expanded text, expanded details, a dialog title, and a footer.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <param name="CollapsedText"></param>
        /// <param name="ExpandedText"></param>
        /// <param name="ExpandedDetails"></param>
        /// <param name="DialogTitle"></param>
        /// <param name="Footer"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle, object Footer)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, TaskDialogIcon.Custom, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, a sub-message, a collapsed text, an expanded text, expanded details, a dialog title, a footer, and a footer icon.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <param name="CollapsedText"></param>
        /// <param name="ExpandedText"></param>
        /// <param name="ExpandedDetails"></param>
        /// <param name="DialogTitle"></param>
        /// <param name="Footer"></param>
        /// <param name="FooterIcon"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle, object Footer, TaskDialogIcon FooterIcon)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, FooterIcon, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message, buttons, an icon, a sub-message, a collapsed text, an expanded text, expanded details, a dialog title, a footer, a footer icon, and a custom footer icon.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <param name="SubMessage"></param>
        /// <param name="CollapsedText"></param>
        /// <param name="ExpandedText"></param>
        /// <param name="ExpandedDetails"></param>
        /// <param name="DialogTitle"></param>
        /// <param name="Footer"></param>
        /// <param name="FooterIcon"></param>
        /// <param name="FooterCustomIcon"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons, MessageBoxIcon Icon, object SubMessage, object CollapsedText, object ExpandedText, object ExpandedDetails, object DialogTitle, object Footer, TaskDialogIcon FooterIcon, Icon FooterCustomIcon)
        {
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, FooterIcon, FooterCustomIcon, false);
        }

        #endregion

        #region Events/Overrides

        /// <summary>
        /// A void that handles the HyperlinkClicked event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TD_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
        {
            // Check if e.Href is a valid URL
            bool isValidUrl = Uri.TryCreate(e.Href, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (isValidUrl)
            {
                // e.Href is a valid URL, proceed with opening it
                System.Diagnostics.Process.Start(e.Href);
            }
        }

        /// <summary>
        /// A void that handles the RadioButtonClicked event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TD_RadioButtonClicked(object sender, TaskDialogItemClickedEventArgs e)
        {

        }

        /// <summary>
        /// A void that handles the Timer event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TD_Timer(object sender, TimerEventArgs e)
        {

        }

        /// <summary>
        /// A void that handles the VerificationClicked event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TD_VerificationClicked(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// A void that handles the HelpRequested event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TD_HelpRequested(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// A void that handles the ExpandButtonClicked event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TD_ExpandButtonClicked(object sender, ExpandButtonClickedEventArgs e)
        {

        }

        /// <summary>
        /// A void that handles the ButtonClicked event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TD_ButtonClicked(object sender, TaskDialogItemClickedEventArgs e)
        {

        }

        /// <summary>
        /// A void that handles the Created event of the modern task dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TD_Created(object sender, EventArgs e)
        {
            // The following code is used to apply the modern style to the dialog and its child controls.
            // It used as a trial to make task dialogs have complete dark mode, but it is not fully functional yet.

            // Get the handle of the dialog
            IntPtr hWnd = (sender as IWin32Window).Handle;

            // Apply the modern style to the dialog: including titlebar dark/light mode, and rounded/sharp corners
            //ApplyStyle(hWnd, true);

            //// Apply the modern style to the child controls of the dialog
            //foreach (IntPtr ChildHwnd in User32.GetChildWindowHandles(sender as IWin32Window))
            //{
            //    ApplyStyle(ChildHwnd, false);
            //}
        }

        #endregion
    }
}