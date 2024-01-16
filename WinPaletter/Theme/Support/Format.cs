using System;
using System.Linq;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        public static Format GetFormat(string File)
        {
            if (System.IO.File.Exists(File))
            {
                string txt = string.Join("\r\n", Decompress(File));
                bool JSON = IsValidJson(txt);
                bool WPTH = txt.StartsWith("<WinPaletter - ", StringComparison.OrdinalIgnoreCase);

                if (JSON)
                {
                    return Format.JSON;
                }

                else if (WPTH)
                {
                    return Format.OldFormat;
                }

                else
                {
                    return Format.Error;
                }
            }
            else
            {
                return Format.Error;
            }
        }

        public enum Format
        {
            JSON,
            OldFormat,
            Error
        }
    }
}
