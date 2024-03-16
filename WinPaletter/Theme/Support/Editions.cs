﻿using System;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Get edition of a WinPaletter theme file
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        public static Editions GetEdition(string File)
        {
            if (System.IO.File.Exists(File))
            {
                string txt = string.Join("\r\n", Decompress(File));
                bool JSON = IsValidJson(txt);
                bool WPTH = txt.StartsWith("<WinPaletter - ", StringComparison.OrdinalIgnoreCase);

                if (JSON)
                {
                    return Editions.JSON;
                }

                else if (WPTH)
                {
                    return Editions.OldFormat;
                }

                else
                {
                    return Editions.Error;
                }
            }
            else
            {
                return Editions.Error;
            }
        }

        /// <summary>
        /// Enumeration for the editions of WinPaletter theme files
        /// </summary>
        public enum Editions
        {
            /// <summary>
            /// JSON format, the new format
            /// </summary>
            JSON,
            /// <summary>
            /// Old format, obsolete
            /// </summary>
            OldFormat,
            /// <summary>
            /// Error, file not found or not a WinPaletter theme file
            /// </summary>
            Error
        }
    }
}
