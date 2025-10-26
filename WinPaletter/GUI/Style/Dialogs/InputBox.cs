using Microsoft.VisualBasic;
using Ookii.Dialogs.WinForms;
using System.Windows.Forms;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// A class that provides modern dialogs.
    /// </summary>
    public partial class Dialogs
    {
        /// <summary>
        /// Displays an input dialog box that allows the user to enter a value, with support for modern and classic
        /// dialog styles.
        /// </summary>
        /// <remarks>This method uses a modern input dialog box on supported operating systems. On Windows
        /// XP, a classic input box is used as a fallback.</remarks>
        /// <param name="Instruction">The main instruction or prompt displayed to the user. This should clearly describe what input is expected.</param>
        /// <param name="Value">The default value pre-filled in the input box. If the user does not provide input, this value will be
        /// returned. Defaults to an empty string.</param>
        /// <param name="Notice">An optional notice or additional information displayed in the dialog box. This can provide context or
        /// guidance for the user.</param>
        /// <param name="Title">The title of the input dialog window. If not specified, the application name is used as the default title.</param>
        /// <returns>The string entered by the user. If the user cancels the dialog, the <paramref name="Value"/> parameter is
        /// returned.</returns>
        public static string InputBox(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            try
            {
                // Log input box details
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "InputBox query");

                if (!string.IsNullOrWhiteSpace(Value))
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.DefaultValue: {Value}");

                if (!string.IsNullOrWhiteSpace(Notice))
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.Notice: {Notice}");

                if (!string.IsNullOrWhiteSpace(Title))
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.Title: {Title}");
                else
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.Title: {Application.ProductName}");

                if (!string.IsNullOrWhiteSpace(Instruction))
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.Instruction: {Instruction}");

                //Windows XP does not support the modern input box dialog.
                if (!OS.WXP)
                {
                    // Create a new instance of the modern input box dialog.
                    using (InputDialog ib = new()
                    {
                        MainInstruction = Instruction,
                        Input = Value,
                        Content = Notice,
                        WindowTitle = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName,
                    })
                    {
                        // Hide the dialog and return the response.
                        if (ib.ShowDialog() == DialogResult.OK)
                        {
                            string response = ib.Input;
                            if (string.IsNullOrWhiteSpace(response)) response = Value;

                            // Log user input
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.UserInput: {response}");

                            return response;
                        }
                        else
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "InputBox > Canceled by user.");
                            return Value;
                        }
                    }
                }
                else
                {
                    // If Windows XP, use classic input box
                    return InputBox_Classic(Instruction, Value, Notice, Title);
                }
            }
            catch
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Warning, " Modern InputBox failed, falling back to classic input box.");
                return InputBox_Classic(Instruction, Value, Notice, Title);
            }
        }

        /// <summary>
        /// Displays a classic input box dialog that prompts the user for input.
        /// </summary>
        /// <remarks>This method uses the classic input box dialog provided by the
        /// Microsoft.VisualBasic.Interaction.InputBox API. It logs the details of the input box and the user's response
        /// for informational purposes.</remarks>
        /// <param name="Instruction">The main instruction or prompt displayed to the user.</param>
        /// <param name="Value">The default value pre-filled in the input box. This parameter is optional and defaults to an empty string.</param>
        /// <param name="Notice">An additional notice or message displayed below the instruction. This parameter is optional and defaults to
        /// an empty string.</param>
        /// <param name="Title">The title of the input box window. This parameter is optional and defaults to the application's product
        /// name.</param>
        /// <returns>The string entered by the user in the input box. If the user cancels the dialog, an empty string is
        /// returned.</returns>
        private static string InputBox_Classic(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            string N = !string.IsNullOrWhiteSpace(Notice) ? $"\r\n\r\n{Notice}" : string.Empty;
            string T = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName;

            // Log classic input box details
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "InputBox query");

            if (!string.IsNullOrWhiteSpace(Value))
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.DefaultValue: {Value}");

            if (!string.IsNullOrWhiteSpace(Notice))
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.Notice: {Notice}");

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.Title: {T}");

            if (!string.IsNullOrWhiteSpace(Instruction))
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.Instruction: {Instruction}");

            // Show the classic input box and return the response
            string response = Interaction.InputBox($"{Instruction}{N}", T, Value);

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"InputBox.UserInput: {response}");

            return response;
        }
    }
}
