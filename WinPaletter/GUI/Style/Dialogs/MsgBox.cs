using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
        /// Represents the system-defined icon used for help or question dialogs.<br></br>
        /// Get question icon from shell32.dll, not from the system icons as question icon is old and inconsistent with Windows 10 and higher (but consistent with Windows 8.1 and lower).
        /// </summary>
        private static Icon questionIcon = NativeMethods.Helpers.GetSystemIcon(Shell32.SHSTOCKICONID.HELP, Shell32.SHGSI.ICON);

        /// <summary>
        /// Represents the system-defined icon used for information dialogs.<br></br>
        /// Get question icon from shell32.dll, not from the system icons as question icon is old and inconsistent with Windows 10 and higher (but consistent with Windows 8.1 and lower).
        /// </summary>
        private static Icon informationIcon = NativeMethods.Helpers.GetSystemIcon(Shell32.SHSTOCKICONID.INFO, Shell32.SHGSI.ICON);

        /// <summary>
        /// Represents the system-defined icon used for errors dialogs.<br></br>
        /// Get question icon from shell32.dll, not from the system icons as question icon is old and inconsistent with Windows 10 and higher (but consistent with Windows 8.1 and lower).
        /// </summary>
        private static Icon errorIcon = NativeMethods.Helpers.GetSystemIcon(Shell32.SHSTOCKICONID.Error, Shell32.SHGSI.ICON);

        /// <summary>
        /// Represents the system-defined icon used for exclamation dialogs.<br></br>
        /// Get question icon from shell32.dll, not from the system icons as question icon is old and inconsistent with Windows 10 and higher (but consistent with Windows 8.1 and lower).
        /// </summary>
        private static Icon exclamationIcon = NativeMethods.Helpers.GetSystemIcon(Shell32.SHSTOCKICONID.WARNING, Shell32.SHGSI.ICON);
        /// <summary>
        /// A class instance that provides modern task dialog.
        /// </summary>

        private static TaskDialog TD
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get => _TD;

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

        private static string ConvertToLink(string String)
        {
            if (String == null) return String;

            List<string> c = new List<string>();

            string urlPattern = @"^(https?:\/\/)?(www\.)?[a-zA-Z0-9\-]+(\.[a-zA-Z]{2,})+(:\d+)?(\/[^\s]*)?$";
            string localPathPattern = @"^([a-zA-Z]:\\|\\\\)[^\s]+$";

            foreach (string x in String.Split(' '))
            {
                string trimmed = x.Trim();

                if (System.Text.RegularExpressions.Regex.IsMatch(trimmed, urlPattern))
                {
                    string url = trimmed.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? trimmed : "http://" + trimmed;
                    c.Add($"<a href=\"{url}\">{trimmed}</a>");
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(trimmed, localPathPattern))
                {
                    c.Add($"<a href=\"file:///{trimmed.Replace('\\', '/')}\" target=\"_blank\">{trimmed}</a>");
                }
                else
                {
                    c.Add(trimmed);
                }
            }

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
                        RightToLeft = Program.Localization.Information.RightToLeft,
                        ButtonStyle = TaskDialogButtonStyle.Standard,
                        Content = ConvertToLink((SubMessage ?? string.Empty).ToString()),
                        FooterIcon = FooterIcon,
                        CenterParent = true,
                        WindowTitle = (DialogTitle ?? Application.ProductName).ToString(),
                        MainInstruction = (Message ?? string.Empty).ToString(),
                        CollapsedControlText = (ExpandedText ?? string.Empty).ToString(),
                        ExpandedControlText = (CollapsedText ?? string.Empty).ToString(),
                        ExpandedInformation = ConvertToLink((ExpandedDetails ?? string.Empty).ToString()),
                        Footer = ConvertToLink((Footer ?? string.Empty).ToString()).ToLower(),
                        MainIcon = TaskDialogIcon.Custom
                    };

                    // Get the icon of the footer
                    if (FooterCustomIcon is null) FooterCustomIcon = FormsExtensions.Icon<MainForm>();
                    else TD.CustomFooterIcon = FooterCustomIcon;

                    // Create the buttons of the dialog
                    TaskDialogButton okButton = new(ButtonType.Custom) { Text = Program.Localization.Strings.General.OK, ElevationRequired = RequireElevation };
                    TaskDialogButton yesButton = new(ButtonType.Custom) { Text = Program.Localization.Strings.General.Yes, ElevationRequired = RequireElevation };
                    TaskDialogButton noButton = new(ButtonType.Custom) { Text = Program.Localization.Strings.General.No };
                    TaskDialogButton cancelButton = new(ButtonType.Custom) { Text = Program.Localization.Strings.General.Cancel };
                    TaskDialogButton retryButton = new(ButtonType.Custom) { Text = Program.Localization.Strings.General.Retry, ElevationRequired = RequireElevation };
                    TaskDialogButton closeButton = new(ButtonType.Custom) { Text = Program.Localization.Strings.General.Close };
                    TaskDialogButton customButton = new(ButtonType.Custom);

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
                    if (Icon == MessageBoxIcon.Information)
                    {
                        CustomSystemSounds.Asterisk.Play();
                        TD.CustomMainIcon = informationIcon;
                    }
                    else if (Icon == MessageBoxIcon.Question)
                    {
                        CustomSystemSounds.Question.Play();
                        TD.CustomMainIcon = questionIcon;
                    }
                    else if (Icon == MessageBoxIcon.Error)
                    {
                        CustomSystemSounds.Hand.Play();
                        TD.CustomMainIcon = errorIcon;
                    }
                    else if (Icon == MessageBoxIcon.Exclamation)
                    {
                        CustomSystemSounds.Exclamation.Play();
                        TD.CustomMainIcon = exclamationIcon;
                    }

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox query");

                    if (!string.IsNullOrWhiteSpace(TD.WindowTitle))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Title: {TD.WindowTitle}");

                    if (!string.IsNullOrWhiteSpace(TD.Content))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Content: {TD.Content}");

                    if (!string.IsNullOrWhiteSpace(TD.Footer))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Footer: {TD.Footer}");

                    if (!string.IsNullOrWhiteSpace(TD.ExpandedInformation))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Expanded Information: {TD.ExpandedInformation}");

                    if (!string.IsNullOrWhiteSpace(TD.CollapsedControlText))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Collapsed Control Text: {TD.CollapsedControlText}");

                    if (!string.IsNullOrWhiteSpace(TD.ExpandedControlText))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Expanded Control Text: {TD.ExpandedControlText}");

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Icon: {TD.MainIcon}");
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Footer Icon: {TD.FooterIcon}");

                    if (TD.Buttons is not null && TD.Buttons.Any(x => x is not null))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Buttons: {string.Join(", ", TD.Buttons.Where(x => x is not null).Select(x => x.Text))}");

                    if (!string.IsNullOrWhiteSpace(TD.MainInstruction))
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Message: {TD.MainInstruction}");

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

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Message box result: {result}");

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
        /// Displays a classic message box with customizable content, buttons, and icon.
        /// </summary>
        /// <remarks>This method constructs a message box with the specified content and logs the details
        /// of the message box to the application's logging system. The message box is displayed using the <see
        /// cref="MessageBox.Show(string, string, MessageBoxButtons, MessageBoxIcon)"/> method.</remarks>
        /// <param name="Message">The main message to display in the message box. This parameter cannot be null.</param>
        /// <param name="SubMessage">An optional sub-message to display below the main message. If null or empty, it will be omitted.</param>
        /// <param name="ExpandedDetails">Optional additional details to display below the sub-message. If null or empty, it will be omitted.</param>
        /// <param name="Footer">An optional footer message to display at the bottom of the message box. If null or empty, it will be
        /// omitted.</param>
        /// <param name="DialogTitle">The title of the message box window. If null or empty, the application name will be used as the title.</param>
        /// <param name="Buttons">The buttons to display in the message box. The default is <see cref="MessageBoxButtons.OK"/>.</param>
        /// <param name="Icon">The icon to display in the message box. The default is <see cref="MessageBoxIcon.Information"/>.</param>
        /// <returns>A <see cref="DialogResult"/> value indicating which button the user clicked to close the message box.</returns>
        private static DialogResult Msgbox_Classic(object Message, object SubMessage, object ExpandedDetails, object Footer, object DialogTitle, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Information)
        {
            string SM = !string.IsNullOrWhiteSpace((SubMessage ?? string.Empty).ToString()) ? $"\r\n\r\n{SubMessage}" : string.Empty;
            string ED = !string.IsNullOrWhiteSpace((ExpandedDetails ?? string.Empty).ToString()) ? $"\r\n\r\n{ExpandedDetails}" : string.Empty;
            string Fo = !string.IsNullOrWhiteSpace((Footer ?? string.Empty).ToString()) ? $"\r\n\r\n{Footer}" : string.Empty;
            string T = !string.IsNullOrWhiteSpace((DialogTitle ?? string.Empty).ToString()) ? DialogTitle.ToString() : Application.ProductName;
            string fullMessage = $"{Message}{SM}{ED}{Fo}";

            // Logging non-null properties
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "MsgBox query");

            if (!string.IsNullOrWhiteSpace(SubMessage?.ToString()))
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.SubMessage: {SubMessage}");

            if (!string.IsNullOrWhiteSpace(ExpandedDetails?.ToString()))
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.ExpandedDetails: {ExpandedDetails}");

            if (!string.IsNullOrWhiteSpace(Footer?.ToString()))
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Footer: {Footer}");

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Title: {T}");
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Buttons: {Buttons}");
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Icon: {Icon}");

            if (!string.IsNullOrWhiteSpace(Message?.ToString())) Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"MsgBox.Message: {Message}");

            return MessageBox.Show(fullMessage, T, Buttons, Icon);
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
                Process.Start(e.Href);
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
            // Get the handle of the dialog
            IntPtr hWnd = (sender as IWin32Window).Handle;

            // Don't make whole window dark, as it makes some controls invisible (like the expanded details box and labels)
            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, Program.Style.DarkMode, false);

            //// Apply the modern style to the child controls of the dialog
            //foreach (IntPtr ChildHwnd in User32.GetChildWindowHandles(hWnd))
            //{
            //    NativeMethods.Helpers.SetHWNDDarkMode(ChildHwnd, Program.Style.DarkMode);
            //}
        }

        #endregion
    }
}