using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.Comctl32;

namespace WinPaletter.UI.Style
{
    public partial class Dialogs
    {
        /// <summary>
        /// A class instance that provides modern task dialog.
        /// </summary>
        private static TaskDialogState _TDState;

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
        /// Represents the current state of the task dialog.
        /// </summary>
        private class TaskDialogState
        {
            public IntPtr hWnd;
            public bool VerificationFlagChecked;
            public int SelectedButton;
            public int SelectedRadioButton;
            public List<TASKDIALOG_BUTTON> Buttons = new List<TASKDIALOG_BUTTON>();
            public List<TASKDIALOG_BUTTON> RadioButtons = new List<TASKDIALOG_BUTTON>();
            public Dictionary<int, Action> ButtonActions = new Dictionary<int, Action>();
            public Dictionary<int, Action> RadioButtonActions = new Dictionary<int, Action>();
            public Action<int> HyperlinkAction;
            public Action TimerAction;
            public Action ExpandAction;
            public Action VerificationAction;
            public IntPtr ConfigPointer = IntPtr.Zero;
        }

        private static string ConvertToLink(string String)
        {
            if (String == null) return String;

            List<string> c = [];

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
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.None, object SubMessage = null, object CollapsedText = null, object ExpandedText = null, object ExpandedDetails = null, object DialogTitle = null, object Footer = null, TaskDialogIcon FooterIcon = TaskDialogIcon.None, Icon FooterCustomIcon = null, bool RequireElevation = false)
        {
            try
            {
                // Windows XP does not support the modern task dialog.
                if (!OS.WXP)
                {
                    // Initialize state
                    _TDState = new TaskDialogState();

                    // Build the task dialog configuration
                    TASKDIALOGCONFIG config = new()
                    {
                        cbSize = (uint)Marshal.SizeOf<TASKDIALOGCONFIG>(),
                        hwndParent = IntPtr.Zero,
                        hInstance = IntPtr.Zero,
                        dwFlags = TaskDialogFlags.EnableHyperlinks | TaskDialogFlags.AllowDialogCancellation,
                        dwCommonButtons = 0,
                        pszWindowTitle = (DialogTitle ?? Application.ProductName).ToString(),
                        pszMainInstruction = (Message ?? string.Empty).ToString(),
                        pszContent = ConvertToLink((SubMessage ?? string.Empty).ToString()),
                        pszExpandedInformation = ConvertToLink((ExpandedDetails ?? string.Empty).ToString()),
                        pszExpandedControlText = (CollapsedText ?? string.Empty).ToString(),
                        pszCollapsedControlText = (ExpandedText ?? string.Empty).ToString(),
                        pszFooter = ConvertToLink((Footer ?? string.Empty).ToString()),
                        pfCallback = TaskDialogCallbackProc,
                        lpCallbackData = IntPtr.Zero,
                        cxWidth = 0,
                        pszVerificationText = null,
                        cButtons = 0,
                        pButtons = IntPtr.Zero,
                        cRadioButtons = 0,
                        pRadioButtons = IntPtr.Zero,
                        nDefaultButton = 0,
                        nDefaultRadioButton = 0,
                        hMainIcon = IntPtr.Zero,
                        hFooterIcon = IntPtr.Zero
                    };

                    // Set RTL layout if needed
                    if (Program.Localization.Information.RightToLeft)
                    {
                        config.dwFlags |= TaskDialogFlags.RTLLayout;
                    }

                    // Set icons
                    SetMainIcon(ref config, Icon);
                    SetFooterIcon(ref config, FooterIcon, FooterCustomIcon);

                    // Create buttons
                    CreateButtons(ref config, Buttons, RequireElevation);

                    // Marshal the config to unmanaged memory
                    int size = Marshal.SizeOf(config);
                    _TDState.ConfigPointer = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(config, _TDState.ConfigPointer, false);

                    // Show the dialog
                    int result = TaskDialogIndirect(ref config, out int pnButton, out int pnRadioButton, out bool pfVerificationFlagChecked);

                    // Map result to DialogResult
                    DialogResult dialogResult = MapButtonResult(pnButton);

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Message box result: {dialogResult}");

                    // Clean up
                    CleanupTaskDialog();

                    return dialogResult;
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

        private static void SetMainIcon(ref TASKDIALOGCONFIG config, MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    CustomSystemSounds.Asterisk.Play();
                    config.hMainIcon = new IntPtr((int)TaskDialogIcon.Information);
                    break;
                case MessageBoxIcon.Question:
                    CustomSystemSounds.Question.Play();
                    config.hMainIcon = questionIcon?.Handle ?? IntPtr.Zero;
                    if (questionIcon != null) config.dwFlags |= TaskDialogFlags.UseHIconMain;
                    break;
                case MessageBoxIcon.Error:
                    CustomSystemSounds.Hand.Play();
                    config.hMainIcon = new IntPtr((int)TaskDialogIcon.Error);
                    break;
                case MessageBoxIcon.Exclamation:
                    CustomSystemSounds.Exclamation.Play();
                    config.hMainIcon = new IntPtr((int)TaskDialogIcon.Warning);
                    break;
                default:
                    config.hMainIcon = IntPtr.Zero;
                    break;
            }
        }

        private static void SetFooterIcon(ref TASKDIALOGCONFIG config, TaskDialogIcon footerIcon, Icon customIcon)
        {
            if (customIcon != null)
            {
                config.hFooterIcon = customIcon.Handle;
                config.dwFlags |= TaskDialogFlags.UseHIconFooter;
            }
            else if (footerIcon == TaskDialogIcon.Information || footerIcon == TaskDialogIcon.Warning || footerIcon == TaskDialogIcon.Error || footerIcon == TaskDialogIcon.Shield)
            {
                config.hFooterIcon = new IntPtr((int)footerIcon);
            }
            else
            {
                config.hFooterIcon = IntPtr.Zero;
            }
        }

        private static void CreateButtons(ref TASKDIALOGCONFIG config, MessageBoxButtons buttons, bool requireElevation)
        {
            List<TASKDIALOG_BUTTON> buttonList = new List<TASKDIALOG_BUTTON>();
            TaskDialogCommonButtonFlags commonFlags = 0;

            switch (buttons)
            {
                case MessageBoxButtons.YesNoCancel:
                    commonFlags = TaskDialogCommonButtonFlags.YesNo | TaskDialogCommonButtonFlags.Cancel;
                    break;
                case MessageBoxButtons.YesNo:
                    commonFlags = TaskDialogCommonButtonFlags.YesNo;
                    break;
                case MessageBoxButtons.RetryCancel:
                    commonFlags = TaskDialogCommonButtonFlags.RetryCancel;
                    break;
                case MessageBoxButtons.OKCancel:
                    commonFlags = TaskDialogCommonButtonFlags.OKCancel;
                    break;
                case MessageBoxButtons.OK:
                default:
                    commonFlags = TaskDialogCommonButtonFlags.OK;
                    break;
            }

            config.dwCommonButtons = commonFlags;

            // Add custom button text for localization
            if (commonFlags.HasFlag(TaskDialogCommonButtonFlags.OK))
            {
                buttonList.Add(new TASKDIALOG_BUTTON { nButtonID = IDOK, pszButtonText = Program.Localization.Strings.General.OK });
            }
            if (commonFlags.HasFlag(TaskDialogCommonButtonFlags.Yes))
            {
                buttonList.Add(new TASKDIALOG_BUTTON { nButtonID = IDYES, pszButtonText = Program.Localization.Strings.General.Yes });
            }
            if (commonFlags.HasFlag(TaskDialogCommonButtonFlags.No))
            {
                buttonList.Add(new TASKDIALOG_BUTTON { nButtonID = IDNO, pszButtonText = Program.Localization.Strings.General.No });
            }
            if (commonFlags.HasFlag(TaskDialogCommonButtonFlags.Cancel))
            {
                buttonList.Add(new TASKDIALOG_BUTTON { nButtonID = IDCANCEL, pszButtonText = Program.Localization.Strings.General.Cancel });
            }
            if (commonFlags.HasFlag(TaskDialogCommonButtonFlags.Retry))
            {
                buttonList.Add(new TASKDIALOG_BUTTON { nButtonID = IDRETRY, pszButtonText = Program.Localization.Strings.General.Retry });
            }
            if (commonFlags.HasFlag(TaskDialogCommonButtonFlags.Close))
            {
                buttonList.Add(new TASKDIALOG_BUTTON { nButtonID = IDCLOSE, pszButtonText = Program.Localization.Strings.General.Close });
            }

            // Store buttons in state for reference
            _TDState.Buttons = buttonList;
        }

        private static DialogResult MapButtonResult(int buttonId)
        {
            switch (buttonId)
            {
                case IDOK:
                    return DialogResult.OK;
                case IDYES:
                    return DialogResult.Yes;
                case IDNO:
                    return DialogResult.No;
                case IDCANCEL:
                    return DialogResult.Cancel;
                case IDRETRY:
                    return DialogResult.Retry;
                case IDCLOSE:
                    return DialogResult.OK;
                default:
                    return DialogResult.OK;
            }
        }

        private static IntPtr TaskDialogCallbackProc(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr lpRefData)
        {
            switch (uNotification)
            {
                case TaskDialogNotification.Created:
                    _TDState.hWnd = hwnd;
                    ApplyDarkMode(hwnd);
                    break;

                case TaskDialogNotification.Navigated:
                    // Dialog navigation occurred
                    break;

                case TaskDialogNotification.ButtonClicked:
                    // Button was clicked
                    int buttonId = (int)wParam;
                    if (_TDState.ButtonActions.ContainsKey(buttonId))
                        _TDState.ButtonActions[buttonId]?.Invoke();
                    break;

                case TaskDialogNotification.HyperlinkClicked:
                    // Hyperlink was clicked
                    string href = Marshal.PtrToStringUni(lParam);
                    if (!string.IsNullOrEmpty(href))
                    {
                        try
                        {
                            Process.Start(href);
                        }
                        catch { }
                    }
                    break;

                case TaskDialogNotification.Timer:
                    // Timer tick
                    _TDState.TimerAction?.Invoke();
                    break;

                case TaskDialogNotification.RadioButtonClicked:
                    // Radio button was clicked
                    int radioId = (int)wParam;
                    if (_TDState.RadioButtonActions.ContainsKey(radioId))
                        _TDState.RadioButtonActions[radioId]?.Invoke();
                    break;

                case TaskDialogNotification.VerificationClicked:
                    // Verification checkbox was clicked
                    _TDState.VerificationFlagChecked = (int)wParam != 0;
                    _TDState.VerificationAction?.Invoke();
                    break;

                case TaskDialogNotification.ExpandButtonClicked:
                    // Expand button was clicked
                    _TDState.ExpandAction?.Invoke();
                    break;
            }

            return IntPtr.Zero;
        }

        private static void ApplyDarkMode(IntPtr hwnd)
        {
            if (!Program.Style.DarkMode) return;

            NativeMethods.Helpers.SetHWNDDarkMode(hwnd, true);

            if (_TDState != null && _TDState.ConfigPointer != IntPtr.Zero)
            {
                WinPaletter.DarkTaskDialog.DarkenTaskDialog(hwnd, _TDState.ConfigPointer);
            }

            //foreach (IntPtr child in User32.GetChildWindowHandles(hwnd))
            //{
            //    NativeMethods.Helpers.SetHWNDDarkMode(child, true);
            //    WinPaletter.DarkTaskDialog.DarkenTaskDialog(child, _config.hInstance);
            //}
        }

        private static void CleanupTaskDialog()
        {
            // Clean up resources
            if (_TDState != null)
            {
                // Free the marshaled config memory
                if (_TDState.ConfigPointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(_TDState.ConfigPointer);
                    _TDState.ConfigPointer = IntPtr.Zero;
                }

                _TDState.Buttons.Clear();
                _TDState.RadioButtons.Clear();
                _TDState.ButtonActions.Clear();
                _TDState.RadioButtonActions.Clear();
                _TDState = null;
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
            return MsgBox(Message, MessageBoxButtons.OK, MessageBoxIcon.None, null, null, null, null, null, null, (int)TaskDialogIcon.None, null, false);
        }

        /// <summary>
        /// A function that shows a message box with a message and buttons.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Buttons"></param>
        /// <returns></returns>
        public static DialogResult MsgBox(object Message, MessageBoxButtons Buttons)
        {
            return MsgBox(Message, Buttons, MessageBoxIcon.None, null, null, null, null, null, null, (int)TaskDialogIcon.None, null, false);
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
            return MsgBox(Message, Buttons, Icon, null, null, null, null, null, null, (int)TaskDialogIcon.None, null, false);
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
            return MsgBox(Message, Buttons, Icon, SubMessage, null, null, null, null, null, (int)TaskDialogIcon.None, null, false);
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
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, null, null, null, null, (int)TaskDialogIcon.None, null, false);
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
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, null, null, null, (int)TaskDialogIcon.None, null, false);
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
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, null, null, (int)TaskDialogIcon.None, null, false);
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
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, null, (int)TaskDialogIcon.None, null, false);
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
            return MsgBox(Message, Buttons, Icon, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, (int)TaskDialogIcon.None, null, false);
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
    }
}