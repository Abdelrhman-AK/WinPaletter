using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// String Extensions Class
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Convert a HEX string into a Color.
        /// Supports: #RGB, #ARGB, #RRGGBB, #AARRGGBB (with or without '#').
        /// </summary>
        public static Color ToColorFromHex(this string hex)
        {
            if (!IsHexColor(hex)) return Color.Empty;

            try
            {
                // Remove '#' and trim spaces
                hex = hex.Trim().TrimStart('#');

                // Handle short hex (#RGB or #ARGB)
                if (hex.Length == 3) hex = string.Concat(hex.Select(c => $"{c}{c}"));       // RGB -> RRGGBB
                else if (hex.Length == 4) hex = string.Concat(hex.Select(c => $"{c}{c}"));  // ARGB -> AARRGGBB

                if (hex.Length == 6) // RRGGBB
                {
                    int r = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                    int g = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                    int b = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);

                    return Color.FromArgb(255, r, g, b);
                }
                else if (hex.Length == 8) // AARRGGBB
                {
                    int a = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                    int r = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                    int g = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                    int b = int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);

                    return Color.FromArgb(a, r, g, b);
                }
            }
            catch { }

            return Color.Empty;
        }

        /// <summary>
        /// Checks if a string looks like a valid HEX color.
        /// Supports #RGB, #ARGB, #RRGGBB, #AARRGGBB (with or without '#').
        /// </summary>
        public static bool IsHexColor(this string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return false;

            hex = hex.Trim().TrimStart('#');

            return hex.Length is 3 or 4 or 6 or 8 && hex.All(c => Uri.IsHexDigit(c));
        }

        /// <summary>
        /// Return Color From Reg String String
        /// </summary>
        public static Color ToColorFromWin32(this string String)
        {
            try
            {
                if (String.Contains(" "))
                {
                    string[] Splitted = String.Split(' ');

                    if (Splitted.Count() == 3)
                    {
                        return Color.FromArgb(255, (int)Math.Round(Conversion.Val(Splitted[0])), (int)Math.Round(Conversion.Val(Splitted[1])), (int)Math.Round(Conversion.Val(Splitted[2])));
                    }
                    else if (Splitted.Count() == 4)
                    {
                        return Color.FromArgb((int)Math.Round(Conversion.Val(Splitted[0])), (int)Math.Round(Conversion.Val(Splitted[1])), (int)Math.Round(Conversion.Val(Splitted[2])), (int)Math.Round(Conversion.Val(Splitted[3])));
                    }
                    else
                    {
                        return Color.Empty;
                    }
                }
                else
                {
                    return Color.Empty;
                }
            }
            catch
            {
                return Color.Empty;
            }

        }

        /// <summary>
        /// Measure String by a certain font
        /// </summary>
        public static SizeF Measure(this string text, Font font)
        {
            return TextRenderer.MeasureText(text, font) + new SizeF(3, 2);
        }

        /// <summary>
        /// Measure String by a certain font and a certain width
        /// </summary>
        public static SizeF Measure(this string text, Font font, int maxWidth)
        {
            return TextRenderer.MeasureText(text, font, new Size(maxWidth, int.MaxValue), TextFormatFlags.WordBreak);
        }

        /// <summary>
        /// Convert environment variables to their values
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ExpandEnvironmentVariables(this string path)
        {
            return Environment.ExpandEnvironmentVariables(path.Replace("%WinDir%", @"%windir%\").Replace("%ThemeDir%", @"%ThemeDir%\").Replace(@"\\", @"\").Replace("%ThemeDir%", $@"{SysPaths.ProgramFiles}\Plus!\Themes"));
        }

        /// <summary>
        /// Convert string to byte array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            string[] numChars = str.Split(' ');
            byte[] result = new byte[numChars.Length];

            for (int i = 0, loopTo = numChars.Length - 1; i <= loopTo; i++)
            {
                if (!string.IsNullOrWhiteSpace(numChars[i]))
                    result[i] = byte.Parse(numChars[i]);
                else
                    result[i] = 0;
            }

            return result;
        }

        /// <summary>
        /// Compress a string using DeflateStream
        /// </summary>
        /// <param name="uncompressedString"></param>
        /// <returns></returns>
        public static string Compress(this string uncompressedString)
        {
            byte[] compressedBytes;

            using (MemoryStream uncompressedStream = new(Encoding.UTF8.GetBytes(uncompressedString)))
            {
                using (MemoryStream compressedStream = new())
                {
                    using (DeflateStream compressorStream = new(compressedStream, CompressionLevel.Fastest, true))
                    {
                        uncompressedStream.CopyTo(compressorStream);
                    }
                    compressedBytes = compressedStream.ToArray();
                }
            }

            return Convert.ToBase64String(compressedBytes);
        }

        /// <summary>
        /// Decompress a string using DeflateStream
        /// </summary>
        /// <param name="compressedString"></param>
        /// <returns></returns>
        public static string Decompress(this string compressedString)
        {
            byte[] decompressedBytes;
            MemoryStream compressedStream = new(Convert.FromBase64String(compressedString));

            using (DeflateStream decompressorStream = new(compressedStream, CompressionMode.Decompress))
            {
                using (MemoryStream decompressedStream = new())
                {
                    decompressorStream.CopyTo(decompressedStream);
                    decompressedBytes = decompressedStream.ToArray();
                }
            }

            return Encoding.UTF8.GetString(decompressedBytes);
        }

        /// <summary>
        /// Replace a word in a string with another word with the option to ignore case and whole word
        /// </summary>
        /// <param name="s"></param>
        /// <param name="word"></param>
        /// <param name="by"></param>
        /// <param name="stringComparison"></param>
        /// <param name="WholeWord"></param>
        /// <returns></returns>
        public static string Replace(this string s, string word, string by, StringComparison stringComparison, bool WholeWord)
        {
            s += " ";
            int wordSt;
            StringBuilder sb = new();
            while (s.IndexOf(word, stringComparison) > -1)
            {
                wordSt = s.IndexOf(word, stringComparison);
                if (!WholeWord || (wordSt == 0 || !char.IsLetterOrDigit(char.Parse(s.Substring(wordSt - 1, 1)))) && !char.IsLetterOrDigit(char.Parse(s.Substring(wordSt + word.Length, 1))))
                {
                    sb.Append($"{s.Substring(0, wordSt)}{by}");
                }
                else
                {
                    sb.Append(s.Substring(0, wordSt + word.Length));
                }
                s = s.Substring(wordSt + word.Length);
            }
            sb.Append(s);
            return sb.ToString().Substring(0, sb.Length - 1);
        }

        /// <summary>
        /// Replace a word in a string with another word with the option to ignore case and whole word
        /// </summary>
        /// <param name="s"></param>
        /// <param name="word"></param>
        /// <param name="by"></param>
        /// <param name="IgnoreCase"></param>
        /// <param name="WholeWord"></param>
        /// <returns></returns>
        public static string Replace(this string s, string word, string by, bool IgnoreCase, bool WholeWord)
        {
            StringComparison stringComparison = StringComparison.Ordinal;
            if (IgnoreCase)
                stringComparison = StringComparison.OrdinalIgnoreCase;
            return s.Replace(word, by, stringComparison, WholeWord);
        }
    }
}
