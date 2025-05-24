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
        /// Shows a modern input box dialog.
        /// </summary>
        /// <param name="Instruction"></param>
        /// <param name="Value"></param>
        /// <param name="Notice"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public static string InputBox(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            try
            {
                // Windows WXP does not support the modern input box dialog.
                if (!OS.WXP)
                {
                    // Create a new instance of the modern input box dialog.
                    using (InputDialog ib = new()
                    {
                        MainInstruction = Instruction,
                        Input = Value,
                        Content = Notice,
                        WindowTitle = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName
                    })
                    {
                        // Hide the dialog and return the response.
                        if (ib.ShowDialog() == DialogResult.OK)
                        {
                            // If the user did not enter a response, return the default value.
                            string response = ib.Input;

                            // If the user entered a response, return it.
                            if (string.IsNullOrWhiteSpace(response)) response = Value;

                            // Return the response.
                            return response;
                        }
                        else
                        {
                            // If the user canceled the dialog, return the default value.
                            return Value;
                        }
                    }
                }
                else
                {
                    // If the operating system is Windows WXP, use the classic input box dialog.
                    return InputBox_Classic(Instruction, Value, Notice, Title);
                }
            }
            catch
            {
                // If an error occurred, use the classic input box dialog.
                return InputBox_Classic(Instruction, Value, Notice, Title);
            }
        }


        /// <summary>
        /// Shows a classic input box dialog that is built into the .NET Framework. This method is used as a fallback when the modern input box dialog is not available as in Windows WXP.
        /// </summary>
        /// <param name="Instruction"></param>
        /// <param name="Value"></param>
        /// <param name="Notice"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        private static string InputBox_Classic(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            // If the user did not enter a notice message, use nothing.
            string N = !string.IsNullOrWhiteSpace(Notice) ? $"\r\n\r\n{Notice}" : string.Empty;

            // If the user did not enter a title, use the application name.
            string T = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName;

            // Hide the classic input box dialog and return the response.
            return Interaction.InputBox($"{Instruction}{N}", T, Value);
        }

    }
}
