using Microsoft.VisualBasic;
using Ookii.Dialogs.WinForms;
using System.Windows.Forms;

namespace WinPaletter.UI.Style
{
    public partial class Dialogs
    {
        public static string InputBox(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            try
            {
                if (!OS.WXP)
                {
                    var ib = new InputDialog()
                    {
                        MainInstruction = Instruction,
                        Input = Value,
                        Content = Notice,
                        WindowTitle = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName
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
            string N = !string.IsNullOrWhiteSpace(Notice) ? "\r\n" + "\r\n" + Notice : string.Empty;
            string T = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName;

            return Interaction.InputBox(Instruction + N, T, Value);
        }

    }
}
