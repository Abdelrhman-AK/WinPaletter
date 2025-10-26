using System;
using System.IO;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Style
{
    public static class IconPicker
    {
        private const int MAX_PATH = 0x00000104;

        /// <summary>
        /// Picks an icon from a PE File
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="PEfileName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ShowIconPicker(IntPtr windowHandle, string PEfileName, int index = 0)
        {
            // Log method call and parameters
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "IconPicker.ShowIconPicker query");
            if (PEfileName is not null)
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"IconPicker.ShowIconPicker.PEfileName: {PEfileName}");
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"IconPicker.ShowIconPicker.InitialIndex: {index}");
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"IconPicker.ShowIconPicker.WindowHandle: {windowHandle}");

            // Create a StringBuilder with the PE file name
            StringBuilder sb = new(PEfileName, MAX_PATH);

            // Show the icon picker dialog
            int retval = Shell32.PickIconDlg(windowHandle, sb, sb.MaxCapacity, ref index);
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"IconPicker.ShowIconPicker.PickIconDlg returned: {retval}");

            if (retval != 0)
            {
                string result;

                if (Path.GetExtension(sb.ToString()).ToLower() != ".ico")
                {
                    // Icon from a PE file
                    result = $"{sb},{index}";
                }
                else
                {
                    // Icon from an ICO file
                    result = sb.ToString();
                }

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"IconPicker.ShowIconPicker.SelectedPEIcon: {result}");

                return result;
            }

            // User canceled the dialog
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "IconPicker canceled by user.");
            return null;
        }
    }
}
