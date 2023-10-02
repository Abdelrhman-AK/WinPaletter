using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter
{

    public static class ColorsExtensions
    {

        /// <summary>
        /// Reverse Color From RGB To BGR
        /// </summary>
        public static Color Reverse(this Color Color, bool Alpha = false)
        {
            if (!Alpha)
            {
                return Color.FromArgb(Color.B, Color.G, Color.R);
            }

            else
            {
                return Color.FromArgb(Color.A, Color.B, Color.G, Color.R);

            }

        }

        /// <summary>
        /// Return HEX Color As String From RGB
        /// </summary>
        public static string HEX(this Color Color, bool Alpha = true, bool Hash = false)
        {
            string S;
            if (Alpha)
            {
                S = string.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B) + string.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B) + string.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B) + string.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B);
            }
            else
            {
                S = string.Format("{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B);
            }
            if (Hash)
                S = "#" + S;
            return S;
        }

        /// <summary>
        /// Return HSL_Structure From RGB Color
        /// </summary>
        public static HSL_Structure ToHSL(this Color Color)
        {
            var _hsl = new HSL_Structure();

            float r = Color.R / 255.0f;
            float g = Color.G / 255.0f;
            float b = Color.B / 255.0f;

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);
            float delta = max - min;

            _hsl.L = (max + min) / 2f;

            if (delta == 0f)
            {
                _hsl.H = 0;
                _hsl.S = 0.0f;
            }
            else
            {
                _hsl.S = (double)_hsl.L <= 0.5d ? delta / (max + min) : delta / (2f - max - min);

                float hue;

                if (r == max)
                {
                    hue = (g - b) / 6f / delta;
                }
                else if (g == max)
                {
                    hue = 1.0f / 3f + (b - r) / 6f / delta;
                }
                else
                {
                    hue = 2.0f / 3f + (r - g) / 6f / delta;
                }

                if (hue < 0f)
                {
                    hue += 1f;
                }
                if (hue > 1f)
                {
                    hue -= 1f;
                }

                _hsl.H = (int)Math.Round(Math.Truncate((double)(hue * 360f)));
            }

            return _hsl;
        }

        /// <summary>
        /// Return RGB Color From HSL_Structure
        /// </summary>
        public static Color ToRGB(this HSL_Structure hsl)
        {
            byte r;
            byte g;
            byte b;

            if (hsl.S == 0f)
            {
                r = (byte)Math.Round(Math.Truncate((double)(hsl.L * 255f)));
                g = (byte)Math.Round(Math.Truncate((double)(hsl.L * 255f)));
                b = (byte)Math.Round(Math.Truncate((double)(hsl.L * 255f)));
            }
            else
            {
                float v1;
                float v2;
                float hue = hsl.H / 360f;

                v2 = (double)hsl.L < 0.5d ? hsl.L * (1f + hsl.S) : hsl.L + hsl.S - hsl.L * hsl.S;
                v1 = 2f * hsl.L - v2;

                r = (byte)Math.Round(Math.Truncate((double)(255f * HueToRGB(v1, v2, hue + 1.0f / 3f))));
                g = (byte)Math.Round(Math.Truncate((double)(255f * HueToRGB(v1, v2, hue))));
                b = (byte)Math.Round(Math.Truncate((double)(255f * HueToRGB(v1, v2, hue - 1.0f / 3f))));
            }

            return Color.FromArgb(r, g, b);
        }

        private static float HueToRGB(float v1, float v2, float vH)
        {
            if (vH < 0f)
            {
                vH += 1f;
            }

            if (vH > 1f)
            {
                vH -= 1f;
            }

            if (6f * vH < 1f)
            {
                return v1 + (v2 - v1) * 6f * vH;
            }

            if (2f * vH < 1f)
            {
                return v2;
            }

            if (3f * vH < 2f)
            {
                return v1 + (v2 - v1) * (2.0f / 3f - vH) * 6f;
            }

            return v1;
        }

        /// <summary>
        /// Return HSL String in the format of H S% L% From RGB Color
        /// </summary>
        public static string HSL_Text(this Color Color)
        {
            return string.Format("{0} {1}% {2}%", Color.ToHSL().H, Math.Round((double)(Color.ToHSL().S * 100f)), Math.Round((double)(Color.ToHSL().L * 100f)));
        }

        /// <summary>
        /// Get String in the format of R G B from a Color(R,G,B) or A R G B from a Color(A,R,G,B)
        /// </summary>
        public static string ToWin32Reg(this Color Color, bool Alpha = false)
        {
            if (!Alpha)
            {
                return string.Format("{0} {1} {2}", Color.R, Color.G, Color.B);
            }
            else
            {
                return string.Format("{0} {1} {2} {3}", Color.A, Color.R, Color.G, Color.B);
            }
        }

        /// <summary>
        /// Change Color Brightness
        /// </summary>
        public static Color CB(this Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0f)
            {
                correctionFactor = 1f + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255f - red) * correctionFactor + red;
                green = (255f - green) * correctionFactor + green;
                blue = (255f - blue) * correctionFactor + blue;
            }
            try
            {
                return Color.FromArgb(color.A, (int)Math.Round(red), (int)Math.Round(green), (int)Math.Round(blue));
            }
            catch
            {
            }

            return default;
        }

        /// <summary>
        /// Get Color as string in the format you choose
        /// </summary>
        public static string ReturnFormat(this Color Color, ColorFormat Format, bool HexHash = false, bool Alpha = false)
        {
            string s = My.Env.Lang.Empty;

            if (Color != Color.FromArgb(0, 0, 0, 0))
            {
                switch (Format)
                {
                    case ColorFormat.HEX:
                        {
                            s = Color.HEX(Alpha, HexHash);
                            break;
                        }

                    case ColorFormat.RGB:
                        {
                            s = Color.ToWin32Reg(Alpha);
                            break;
                        }

                    case ColorFormat.HSL:
                        {
                            s = Color.HSL_Text();
                            break;
                        }

                    case ColorFormat.Dec:
                        {
                            s = Color.ToArgb().ToString();
                            break;
                        }

                }
            }
            else
            {
                s = My.Env.Lang.Empty;
            }

            return s;
        }
        public enum ColorFormat
        {
            HEX,
            RGB,
            HSL,
            Dec
        }

        /// <summary>
        /// Get image From Color
        /// </summary>
        public static object ToBitmap(this Color Color, Size Size)
        {
            var b = new Bitmap(Size.Width, Size.Height);
            var g = Graphics.FromImage(b);
            g.Clear(Color);
            g.Save();
            return b;
            g.Dispose();
            b.Dispose();
        }

        /// <summary>
        /// Return Color by blending two colors
        /// </summary>
        public static Color Blend(this Color color, Color backColor, double amount)
        {
            if (amount > 100d)
                amount = 100d;
            if (amount < 0d)
                amount = 0d;

            byte a = (byte)Math.Round((color.A * (amount / 100d) + backColor.A * (amount / 100d)) / 2d);
            byte r = (byte)Math.Round((color.R * (amount / 100d) + backColor.R * (amount / 100d)) / 2d);
            byte g = (byte)Math.Round((color.G * (amount / 100d) + backColor.G * (amount / 100d)) / 2d);
            byte b = (byte)Math.Round((color.B * (amount / 100d) + backColor.B * (amount / 100d)) / 2d);
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// Get Darker Color From a Color
        /// </summary>
        public static Color Dark(this Color Color)
        {
            return ControlPaint.Dark(Color);
        }

        /// <summary>
        /// Get Darker Color From a Color, with a given percentage
        /// </summary>
        public static Color Dark(this Color Color, float percentage)
        {
            return ControlPaint.Dark(Color, percentage);
        }

        /// <summary>
        /// Get Darkest Color From a Color
        /// </summary>
        public static Color DarkDark(this Color Color)
        {
            return ControlPaint.DarkDark(Color);
        }

        /// <summary>
        /// Get Lighter Color From a Color
        /// </summary>
        public static Color Light(this Color Color)
        {
            return ControlPaint.Light(Color);
        }

        /// <summary>
        /// Get Lighter Color From a Color, with a given percentage
        /// </summary>
        public static Color Light(this Color Color, float percentage)
        {
            return ControlPaint.Light(Color, percentage);
        }

        /// <summary>
        /// Get Lightest Color From a Color
        /// </summary>
        public static Color LightLight(this Color Color)
        {
            return ControlPaint.LightLight(Color);
        }

        /// <summary>
        /// Get Inverted Color From a Color
        /// </summary>
        public static Color Invert(this Color Color)
        {
            return Color.FromArgb(Color.A, 255 - Color.R, 255 - Color.G, 255 - Color.B);
        }

        /// <summary>
        /// Get If the color is dark or not
        /// </summary>
        public static bool IsDark(this Color Color)
        {
            return !(Color.R * 0.2126d + Color.G * 0.7152d + Color.B * 0.0722d > 255d / 2d);
        }

        public struct HSL_Structure
        {
            private int _h;
            private float _s;
            private float _l;

            public HSL_Structure(int h, float s, float l)
            {
                _h = h;
                _s = s;
                _l = l;
            }

            public int H
            {
                get
                {
                    return _h;
                }
                set
                {
                    _h = value;
                }
            }

            public float S
            {
                get
                {
                    return _s;
                }
                set
                {
                    _s = value;
                }
            }

            public float L
            {
                get
                {
                    return _l;
                }
                set
                {
                    _l = value;
                }
            }

            public bool Equals(HSL_Structure hsl)
            {
                return H == hsl.H && S == hsl.S && L == hsl.L;
            }
        }

    }

    public static class BooleanExtensions
    {

        /// <summary>
        /// Return Integer, If True; 1, If False; 0
        /// </summary>
        public static int ToInteger(this bool Boolean)
        {
            return Boolean ? 1 : 0;
        }

    }

    public static class IntegerExtensions
    {

        /// <summary>
        /// Return Boolean by comparison to 1 (Default)
        /// </summary>
        public static bool ToBoolean(this int Integer, int CompareBy = 1)
        {
            return Integer == CompareBy;
        }

        /// <summary>
        /// Return string in the format of XXXXXXXX, useful for registry handling
        /// </summary>
        public static string DWORD(this int @int)
        {
            if (@int.ToString().Count() <= 8)
            {
                int i = 8 - @int.ToString().Count();
                string s = "";
                var loopTo = i;
                for (i = 1; i <= loopTo; i++)
                    s += "0";
                s += @int.ToString();
                return s.Replace("-", "");
            }
            else
            {
                return @int.ToString().Replace("-", "");
            }

        }

        /// <summary>
        /// Return string in the format of XXXXXXXXXXXXXXXX, useful for registry handling
        /// </summary>
        public static string QWORD(this int @int)
        {
            if (@int.ToString().Count() <= 16)
            {
                int i = 16 - @int.ToString().Count();
                string s = "";
                var loopTo = i;
                for (i = 1; i <= loopTo; i++)
                    s += "0";
                s += @int.ToString();
                return s.Replace("-", "");
            }
            else
            {
                return @int.ToString().Replace("-", "");
            }

        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this long length, bool ShowSecondUnit = false)
        {
            long B = 0L;
            long KB = 1024L;
            long MB = KB * 1024L;
            long GB = MB * 1024L;
            long TB = GB * 1024L;
            double size = length;
            string suffix = My.Env.Lang.ByteSizeUnit;

            if (length >= TB)
            {
                size = Math.Round(length / (double)TB, 2);
                suffix = My.Env.Lang.TBSizeUnit;
            }

            else if (length >= GB)
            {
                size = Math.Round(length / (double)GB, 2);
                suffix = My.Env.Lang.GBSizeUnit;
            }

            else if (length >= MB)
            {
                size = Math.Round(length / (double)MB, 2);
                suffix = My.Env.Lang.MBSizeUnit;
            }

            else if (length >= KB)
            {
                size = Math.Round(length / (double)KB, 2);
                suffix = My.Env.Lang.KBSizeUnit;

            }

            if (ShowSecondUnit)
                suffix += My.Env.Lang.SecondUnit;

            return $"{size} {suffix}";
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this short length, bool ShowSecondUnit = false)
        {
            return ((long)length).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this float length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this double length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this decimal length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this int length, bool ShowSecondUnit = false)
        {
            return ((long)length).SizeString(ShowSecondUnit);
        }
    }

    public static class StringExtensions
    {

        /// <summary>
        /// Return Color From HEX String
        /// </summary>
        public static Color FromHEXToColor(this string String, bool Alpha = false)
        {

            try
            {
                if (!Alpha)
                {
                    return Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(String.Replace("#", ""), 16)));
                }
                else
                {
                    return Color.FromArgb(Convert.ToInt32(String.Replace("#", ""), 16));
                }
            }
            catch
            {
                return Color.Empty;
            }

        }

        /// <summary>
        /// Return Color From Reg String String
        /// </summary>
        public static Color FromWin32RegToColor(this string String)
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
        /// Convert String to List (String should be multi-lines)
        /// </summary>
        public static List<string> CList(this string String)
        {
            var List = new List<string>();
            List.Clear();
            using (var Reader = new StringReader(String))
            {
                while (Reader.Peek() >= 0)
                    List.Add(Reader.ReadLine());
                Reader.Close();
                Reader.Dispose();
            }
            return List;
        }

        /// <summary>
        /// Measure String by a certain font
        /// </summary>
        public static SizeF Measure(this string text, Font font)
        {

            try
            {
                var TextBitmap = new Bitmap(1, 1);
                var TextGraphics = Graphics.FromImage(TextBitmap);
                return TextGraphics.MeasureString(text, font);
            }
            catch
            {
            }

            return default;

        }


        public static string PhrasePath(this string path)
        {
            return Environment.ExpandEnvironmentVariables(path.Replace("%WinDir%", @"%windir%\").Replace("%ThemeDir%", @"%ThemeDir%\").Replace(@"\\", @"\").Replace("%ThemeDir%", My.Env.PATH_ProgramFiles + @"\Plus!\Themes"));
        }


        public static byte[] ToBytes(this string str)
        {
            string[] numChars = str.Split(' ');
            byte[] result = new byte[numChars.Length];

            for (int i = 0, loopTo = numChars.Length - 1; i <= loopTo; i++)
            {
                if (!string.IsNullOrWhiteSpace(numChars[i]))
                    result[i] = Conversions.ToByte(numChars[i]);
                else
                    result[i] = 0;
            }

            return result;
        }


        public static string Compress(this string uncompressedString)
        {
            byte[] compressedBytes;

            using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(uncompressedString)))
            {
                using (var compressedStream = new MemoryStream())
                {
                    using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true))
                    {
                        uncompressedStream.CopyTo(compressorStream);
                    }
                    compressedBytes = compressedStream.ToArray();
                }
            }

            return Convert.ToBase64String(compressedBytes);
        }


        public static string Decompress(this string compressedString)
        {
            byte[] decompressedBytes;
            var compressedStream = new MemoryStream(Convert.FromBase64String(compressedString));

            using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                using (var decompressedStream = new MemoryStream())
                {
                    decompressorStream.CopyTo(decompressedStream);
                    decompressedBytes = decompressedStream.ToArray();
                }
            }

            return Encoding.UTF8.GetString(decompressedBytes);
        }


        public static string Replace(this string s, string word, string by, StringComparison stringComparison, bool WholeWord)
        {
            s += " ";
            int wordSt;
            var sb = new StringBuilder();
            while (s.IndexOf(word, stringComparison) > -1)
            {
                wordSt = s.IndexOf(word, stringComparison);
                if (!WholeWord || (wordSt == 0 || !char.IsLetterOrDigit(char.Parse(s.Substring(wordSt - 1, 1)))) && !char.IsLetterOrDigit(char.Parse(s.Substring(wordSt + word.Length, 1))))
                {
                    sb.Append(s.Substring(0, wordSt) + by);
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


        public static string Replace(this string s, string word, string by, bool IgnoreCase, bool WholeWord)
        {
            var stringComparison = StringComparison.Ordinal;
            if (IgnoreCase)
                stringComparison = StringComparison.OrdinalIgnoreCase;
            return s.Replace(word, by, stringComparison, WholeWord);
        }
    }

    public static class ListOfStringExtensions
    {

        /// <summary>
        /// Deduplicate list of string
        /// </summary>
        public static List<string> DeDuplicate(this List<string> List)
        {
            var Result = new List<string>();

            bool Exist;

            foreach (string ElementString in List)
            {
                Exist = false;
                foreach (string ElementStringInResult in Result)
                {
                    if ((ElementString ?? "") == (ElementStringInResult ?? ""))
                    {
                        Exist = true;
                        break;
                    }
                }
                if (!Exist)
                {
                    Result.Add(ElementString);
                }
            }

            return Result;
        }

        /// <summary>
        /// Return String from List, each item is in a separate line
        /// </summary>
        public static string CString(this List<string> List, string JoinBy = "\r\n")
        {
            return string.Join(JoinBy, List.ToArray());
        }

    }

    public static class BitmapExtensions
    {
        /// <summary>
        /// Return Most Used Color From image
        /// </summary>
        public static Color AverageColor(this Bitmap Bitmap)
        {
            try
            {
                using (Bitmap bmp = (Bitmap)Bitmap.Clone())
                {
                    int totalR = 0;
                    int totalG = 0;
                    int totalB = 0;

                    try
                    {
                        if (bmp is not null)
                        {
                            for (int x = 0, loopTo = bmp.Width - 1; x <= loopTo; x++)
                            {
                                for (int y = 0, loopTo1 = bmp.Height - 1; y <= loopTo1; y++)
                                {
                                    var pixel = bmp.GetPixel(x, y);
                                    totalR += pixel.R;
                                    totalG += pixel.G;
                                    totalB += pixel.B;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }

                    if (bmp is not null)
                    {
                        int totalPixels = bmp.Height * bmp.Width;
                        int averageR = totalR / totalPixels;
                        int averageg = totalG / totalPixels;
                        int averageb = totalB / totalPixels;
                        return Color.FromArgb(averageR, averageg, averageb);
                    }
                    else
                    {
                        return Color.FromArgb(80, 80, 80);
                    }
                }
            }
            catch
            {
                return Color.Empty;
            }
        }

        /// <summary>
        /// Return Most Used Color From Image
        /// </summary>
        public static Color AverageColor(this Image Image)
        {
            return ((Bitmap)Image).AverageColor();
        }

        /// <summary>
        /// Return Blurred image
        /// </summary>
        public static Bitmap Blur(this Bitmap bitmap, int BlurForce = 2)
        {
            using (Bitmap img = new(bitmap))
            {
                using (Graphics G = Graphics.FromImage(img))
                {
                    G.SmoothingMode = SmoothingMode.AntiAlias;

                    var att = new ImageAttributes();
                    var m = new ColorMatrix() { Matrix33 = 0.4f };
                    att.SetColorMatrix(m);

                    for (double x = -BlurForce, loopTo = BlurForce; x <= loopTo; x += 0.5d)
                        G.DrawImage(img, new Rectangle((int)Math.Round(x), 0, img.Width - 1, img.Height - 1), 0, 0, img.Width - 1, img.Height - 1, GraphicsUnit.Pixel, att);

                    for (double y = -BlurForce, loopTo = BlurForce; y <= loopTo; y += 0.5d)
                        G.DrawImage(img, new Rectangle(0, (int)Math.Round(y), img.Width - 1, img.Height - 1), 0, 0, img.Width - 1, img.Height - 1, GraphicsUnit.Pixel, att);

                    G.Save();
                    att.Dispose();
                    return new Bitmap(img);
                }
            }
        }

        /// <summary>
        /// Return Noised bitmap (Noise of Windows 10 Acrylic)
        /// </summary>
        public static Bitmap Noise(this Bitmap bmp, NoiseMode NoiseMode, float opacity)
        {
            try
            {
                var g = Graphics.FromImage(bmp);

                if (NoiseMode == NoiseMode.Acrylic)
                {
                    TextureBrush br;
                    br = new TextureBrush(My.Resources.GaussianBlur.Fade((double)opacity));
                    g.FillRectangle(br, new Rectangle(0, 0, bmp.Width, bmp.Height));
                }
                else if (NoiseMode == NoiseMode.Aero)
                {
                    g.DrawImage(My.Resources.AeroGlass.Fade((double)opacity), new Rectangle(0, 0, bmp.Width, bmp.Height));
                }

                g.Save();
                return bmp;
                g.Dispose();
                bmp.Dispose();
            }
            catch
            {
                return null;
            }
        }
        public enum NoiseMode
        {
            Aero,
            Acrylic
        }

        /// <summary>
        /// Replace Color in image (Pixels) by color you choose
        /// </summary>
        public static Bitmap ReplaceColor(this Bitmap inputImage, Color oldColor, Color NewColor)
        {
            var outputImage = new Bitmap(inputImage.Width, inputImage.Height);
            var G = Graphics.FromImage(outputImage);


            for (int y = 0, loopTo = inputImage.Height - 1; y <= loopTo; y++)
            {

                for (int x = 0, loopTo1 = inputImage.Width - 1; x <= loopTo1; x++)
                {
                    var PixelColor = inputImage.GetPixel(x, y);

                    if (PixelColor == oldColor)
                    {
                        outputImage.SetPixel(x, y, NewColor);
                    }
                    else
                    {
                        outputImage.SetPixel(x, y, PixelColor);
                    }

                }
            }

            G.DrawImage(outputImage, 0, 0);
            G.Dispose();
            return outputImage;
            outputImage.Dispose();

        }

        /// <summary>
        /// Replace Color in Image (Pixels) by color you choose
        /// </summary>
        public static Image ReplaceColor(this Image inputImage, Color oldColor, Color NewColor)
        {
            return ((Bitmap)inputImage).ReplaceColor(oldColor, NewColor);
        }

        /// <summary>
        /// Return image filled in the scale of size you choose
        /// </summary>
        public static Bitmap FillScale(Bitmap Bitmap, Size Size)
        {
            try
            {
                int sourceWidth = Bitmap.Width;
                int sourceHeight = Bitmap.Height;
                int destX = 0;
                int destY = 0;
                float nPercent = 0f;
                float nPercentW = 0f;
                float nPercentH = 0f;
                nPercentW = Size.Width / (float)sourceWidth;
                nPercentH = Size.Height / (float)sourceHeight;

                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                    destX = Convert.ToInt16((Size.Width - sourceWidth * nPercent) / 2f);
                }
                else
                {
                    nPercent = nPercentW;
                    destY = Convert.ToInt16((Size.Height - sourceHeight * nPercent) / 2f);
                }

                int destWidth = (int)Math.Round(sourceWidth * nPercent);
                int destHeight = (int)Math.Round(sourceHeight * nPercent);
                var bmPhoto = new Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb);
                bmPhoto.SetResolution(Bitmap.HorizontalResolution, Bitmap.VerticalResolution);
                var grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.DrawImage(Bitmap, new Rectangle(0, 0, destWidth, destHeight));
                grPhoto.Dispose();
                var bm = bmPhoto.Clone(new Rectangle(0, 0, destWidth, destHeight), PixelFormat.Format32bppArgb);
                float f;

                if (nPercentH < nPercentW)
                {
                    f = Size.Width - bm.Width;
                    bm = bm.Resize(Size.Width, (int)Math.Round(Size.Height + f));
                    bm = bm.Clone(new Rectangle(0, (int)Math.Round(1d / 3d * (double)f), Size.Width, Size.Height), PixelFormat.Format32bppArgb);
                }
                else
                {
                    f = Size.Height - bm.Height;
                    bm = bm.Resize(Size.Width, (int)Math.Round(Size.Height + f));
                    bm = bm.Clone(new Rectangle((int)Math.Round(1d / 3d * (double)f), 0, Size.Width, Size.Height), PixelFormat.Format32bppArgb);
                }


                return bm;
            }
            catch
            {
                return Bitmap;
            }
        }

        /// <summary>
        /// Return Image filled in the scale of size you choose
        /// </summary>
        public static Image FillScale(this Image Image, Size Size)
        {
            if (Image != null)
                using (Bitmap bmp = new Bitmap(Image))
                {
                    return FillScale(bmp, Size);
                }
            else
                return null;
        }

        /// <summary>
        /// Resize image in the size you choose
        /// </summary>
        public static Bitmap Resize(this Bitmap bmSource, int TargetWidth, int TargetHeight)
        {
            if (bmSource is null)
                return null;

            using (var B = new Bitmap(TargetWidth, TargetHeight, PixelFormat.Format32bppArgb))
            {
                using (var G = Graphics.FromImage(B))
                {
                    G.CompositingQuality = CompositingQuality.HighQuality;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    G.CompositingMode = CompositingMode.SourceOver;
                    G.DrawImage(bmSource, 0, 0, TargetWidth, TargetHeight);
                }

                B.SetResolution(TargetWidth, TargetHeight);
                return (Bitmap)B.Clone();
            }
        }

        /// <summary>
        /// Resize image in the size you choose
        /// </summary>
        public static Image Resize(this Bitmap bmSource, Size TargetSize)
        {
            return bmSource.Resize(TargetSize.Width, TargetSize.Height);
        }

        /// <summary>
        /// Resize Image in the size you choose
        /// </summary>
        public static Image Resize(this Image imSource, int TargetWidth, int TargetHeight)
        {
            return ((Bitmap)imSource).Resize(TargetWidth, TargetHeight);
        }

        /// <summary>
        /// Resize Image in the size you choose
        /// </summary>
        public static Image Resize(this Image imSource, Size TargetSize)
        {
            return ((Bitmap)imSource).Resize(TargetSize.Width, TargetSize.Height);
        }

        /// <summary>
        /// Return Image Tinted by a color
        /// </summary>
        public static Bitmap Tint(this Image sourceImage, Color Color)
        {
            return ((Bitmap)sourceImage).Tint(Color);
        }

        /// <summary>
        /// Return image Tinted by a color
        /// </summary>
        public static Bitmap Tint(this Bitmap sourceBitmap, Color Color)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[(sourceData.Stride * sourceData.Height)];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);
            float blue;
            float green;
            float red;
            var k = default(int);

            while (k + 4 < pixelBuffer.Length)
            {
                blue = pixelBuffer[k] + (255 - pixelBuffer[k]) * Color.B;
                green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1]) * Color.G;
                red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2]) * Color.R;

                if (blue > 255f)
                {
                    blue = 255f;
                }

                if (green > 255f)
                {
                    green = 255f;
                }

                if (red > 255f)
                {
                    red = 255f;
                }

                pixelBuffer[k] = (byte)Math.Round(blue);
                pixelBuffer[k + 1] = (byte)Math.Round(green);
                pixelBuffer[k + 2] = (byte)Math.Round(red);
                k += 4;
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            var resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);
            return resultBitmap;
        }

        /// <summary>
        /// Fade image (Change Opacity)
        /// </summary>
        public static Bitmap Fade(this Bitmap originalBitmap, double opacity)
        {
            if (originalBitmap == null) return null;
            using (Bitmap bmp = new(originalBitmap.Width, originalBitmap.Height))
            {
                using (var gfx = Graphics.FromImage(bmp))
                {
                    var matrix = new ColorMatrix() { Matrix33 = (float)opacity };
                    var attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    gfx.DrawImage(originalBitmap, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes);
                }
                return (Bitmap)bmp.Clone();
            }
        }

        /// <summary>
        /// Fade Image (Change Opacity)
        /// </summary>
        public static Image Fade(this Image originalImage, double opacity)
        {
            return ((Bitmap)originalImage).Fade(opacity);
        }

        /// <summary>
        /// Return image in Grayscale
        /// </summary>
        public static Bitmap Grayscale(this Image original)
        {
            return ((Bitmap)original).Grayscale();
        }

        /// <summary>
        /// Return image in Grayscale
        /// </summary>
        public static Bitmap Grayscale(this Bitmap original)
        {
            var newBitmap = new Bitmap(original.Width, original.Height);

            using (var g = Graphics.FromImage(newBitmap))
            {
                var colorMatrix = new ColorMatrix(new float[][] { new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f }, new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f }, new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f }, new float[] { 0f, 0f, 0f, 1f, 0f }, new float[] { 0f, 0f, 0f, 0f, 1f } });

                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return newBitmap;
        }

        /// <summary>
        /// Return Inverted image
        /// </summary>
        public static Bitmap Invert(this Bitmap bmp)
        {
            Bitmap bmpDest = null;

            using (var bmpSource = new Bitmap(bmp))
            {
                bmpDest = new Bitmap(bmpSource.Width, bmpSource.Height);

                for (int x = 0, loopTo = bmpSource.Width - 1; x <= loopTo; x++)
                {
                    for (int y = 0, loopTo1 = bmpSource.Height - 1; y <= loopTo1; y++)
                    {
                        var clrPixel = bmpSource.GetPixel(x, y);
                        clrPixel = Color.FromArgb(clrPixel.A, 255 - clrPixel.R, 255 - clrPixel.G, 255 - clrPixel.B);
                        bmpDest.SetPixel(x, y, clrPixel);
                    }
                }
            }

            return bmpDest;
        }


        public static Bitmap Tile(this Bitmap bmp, Size Size)
        {
            using (var B = new Bitmap(Size.Width, Size.Height))
            {
                var G = Graphics.FromImage(B);
                G.SmoothingMode = SmoothingMode.HighSpeed;
                var tb = new TextureBrush(bmp);
                G.FillRectangle(tb, new Rectangle(0, 0, Size.Width, Size.Height));
                G.Save();
                return (Bitmap)B.Clone();
            }
        }
    }

    public static class ControlExtensions
    {
        /// <summary>
        /// Return graphical state of a control to a bitmap
        /// </summary>
        public static Bitmap ToBitmap(this Control Control, bool FixMethod = false)
        {
            if (!FixMethod)
            {
                var bm = new Bitmap(Control.Width, Control.Height);

                lock (Control)
                {
                    Control.DrawToBitmap(bm, new Rectangle(0, 0, Control.Width, Control.Height));
                    return (Bitmap)bm.Clone();
                }
            }
            else
            {
                return DrawToBitmap(Control);
            }

        }

        private static Bitmap DrawToBitmap(Control Control)
        {
            Control[] childControls = Control.Controls.Cast<Control>().ToArray();
            Array.Reverse(childControls);

            var bmp = new Bitmap(Control.Width, Control.Height);

            foreach (var childControl in childControls)
                childControl.DrawToBitmap(bmp, childControl.Bounds);

            return bmp;
        }

        public static void DoubleBufferedControl(Control Control, bool setting)
        {
            var panType = Control.GetType();
            var pi = panType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(Control, setting, null);
        }

        public static Color GetParentColor(this Control ctrl, bool Accept_Transparent = false)
        {

            if (Accept_Transparent)
            {
                return ctrl.Parent.BackColor;
            }
            else
            {
                try
                {

                    if (ctrl.Parent is null)
                    {
                        return default;
                    }

                    if (ctrl.Parent.BackColor.A == 255)
                    {
                        return Color.FromArgb(255, ctrl.Parent.BackColor);
                    }
                    else
                    {
                        try
                        {
                            var c1 = ctrl.Parent.BackColor;
                            Color c2;
                            if (!(ctrl.Parent.Parent is Form))
                            {
                                c2 = ctrl.Parent.Parent.BackColor;
                            }
                            else
                            {
                                c2 = ctrl.Parent.FindForm().BackColor;
                            }
                            double amount = c1.A / 255d;
                            byte r = (byte)Math.Round(c1.R * amount + c2.R * (1d - amount));
                            byte g = (byte)Math.Round(c1.G * amount + c2.G * (1d - amount));
                            byte b = (byte)Math.Round(c1.B * amount + c2.B * (1d - amount));
                            return Color.FromArgb(r, g, b);
                        }
                        catch
                        {
                            return ctrl.Parent.BackColor;
                        }
                    }
                }
                catch
                {
                }
            }

            return default;

        }

        public static void DoubleBuffer(this Control Parent)
        {
            DoubleBufferedControl(Parent, true);

            foreach (Control ctrl in Parent.Controls)
            {
                if (ctrl.HasChildren)
                {
                    foreach (Control c in ctrl.Controls)
                        c.DoubleBuffer();
                }

                DoubleBufferedControl(ctrl, true);
            }
        }

        public static IEnumerable<Control> GetAllControls(this Control parent)
        {
            try
            {
                var cs = parent.Controls.OfType<Control>().OrderBy(c => c.Name);
                return cs.SelectMany(c => c.GetAllControls()).Concat(cs).OrderBy(c => c.Name);
            }
            catch (Exception ex)
            {
                var cs = parent.Controls.OfType<Control>();
                return cs.SelectMany(c => c.GetAllControls()).Concat(cs);
            }
        }

        public static void SetText(this Control Ctrl, string text)
        {
            try
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl.Invoke(new setCtrlTxtInvoker(SetText), Ctrl, text);
                }
                else
                {
                    Ctrl.Text = text;
                    Ctrl.Refresh();
                }
            }
            catch
            {

            }
        }
        private delegate void setCtrlTxtInvoker(Control Ctrl, string text);

        public static void SetTag(this Control Ctrl, object Tag)
        {
            try
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl.Invoke(new setCtrlTagInvoker(SetTag), Ctrl, Tag);
                }
                else
                {
                    Ctrl.Tag = Tag;
                }
            }
            catch
            {

            }
        }
        private delegate void setCtrlTagInvoker(Control Ctrl, object Tag);

        public static void AddTreeNode(this TreeView Ctrl, string text, string imagekey)
        {
            try
            {
                if (Ctrl.InvokeRequired)
                {
                    Ctrl.Invoke(new AddTreeNodeInvoker(AddTreeNode), Ctrl, text, imagekey);
                }
                else
                {
                    {
                        var temp = Ctrl.Nodes.Add(text);
                        temp.ImageKey = imagekey;
                        temp.SelectedImageKey = imagekey;
                    }
                }
            }
            catch
            {

            }
        }
        private delegate void AddTreeNodeInvoker(TreeView Ctrl, string text, string imagekey);

        public static void PerformStepMethod2(this ProgressBar ProgressBar)
        {
            if (ProgressBar.InvokeRequired)
            {
                ProgressBar.Invoke(new PerformStepMethod2Invoker(PerformStepMethod2), ProgressBar);
            }
            else
            {
                ProgressBar.PerformStep();
            }
        }
        private delegate void PerformStepMethod2Invoker(ProgressBar ProgressBar);
    }

    static class GraphicsExtensions
    {

        public static void DrawGlowString(this Graphics G, int GlowSize, string Text, Font Font, Color ForeColor, Color GlowColor, Rectangle ClientRect, Rectangle Rect, StringFormat FormatX)
        {
            int w = (int)Math.Round(Math.Max(8d, ClientRect.Width / 5d));
            int h = (int)Math.Round(Math.Max(8d, ClientRect.Height / 5d));
            float emSize = G.DpiY * Font.SizeInPoints / 72f;
            if (Text is null | string.IsNullOrWhiteSpace(Text))
                Text = "";

            using (var b = new Bitmap(w, h))
            {
                using (var gp = new GraphicsPath())
                {
                    gp.AddString(Text, Font.FontFamily, (int)Font.Style, emSize, Rect, FormatX);

                    using (var gx = Graphics.FromImage(b))
                    {
                        using (var m = new Matrix(1.0f / 5f, 0f, 0f, 1.0f / 5f, -(1.0f / 5f), -(1.0f / 5f)))
                        {
                            gx.SmoothingMode = SmoothingMode.AntiAlias;
                            gx.Transform = m;
                            using (var pn = new Pen(GlowColor, GlowSize))
                            {
                                gx.DrawPath(pn, gp);
                                gx.FillPath(pn.Brush, gp);
                            }
                        }
                    }

                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.DrawImage(b, ClientRect, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel);

                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var br = new SolidBrush(ForeColor))
                    {
                        G.DrawString(Text, Font, br, Rect, FormatX);
                    }

                }
            }
        }

        public static void DrawGlow(this Graphics G, Rectangle R, Color GlowColor, int GlowSize = 5, int GlowFade = 7)
        {
            try
            {
                if (GlowSize <= 0)
                    GlowSize = 1;
                if (GlowFade <= 0)
                    GlowFade = 1;

                Rectangle Rect;
                Rect = new Rectangle(R.X - GlowSize - 2, R.Y - GlowSize - 2, R.Width + GlowSize * 2 + 3, R.Height + GlowSize * 2 + 3);

                using (var bm = new Bitmap((int)Math.Round(Rect.Width / (double)GlowFade), (int)Math.Round(Rect.Height / (double)GlowFade)))
                {
                    using (var G2 = Graphics.FromImage(bm))
                    {
                        var Rect2 = new Rectangle(1, 1, bm.Width, bm.Height);

                        using (var br = new SolidBrush(GlowColor))
                        {
                            G2.FillRectangle(br, Rect2);
                        }

                        G.DrawImage(bm, Rect);
                    }
                }
            }
            catch
            {
            }
        }

        public static void DrawAeroEffect(this Graphics G, Rectangle Rect, Bitmap BackgroundBlurred, Color Color1, decimal ColorBalance, Color Color2, decimal GlowBalance, decimal alpha, int Radius, bool RoundedCorners)
        {

            if (RoundedCorners)
            {
                if (BackgroundBlurred is not null)
                    G.DrawRoundImage(BackgroundBlurred, Rect, Radius, true);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * 255), Color.Black)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (ColorBalance * 255)), Color1)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }

                var C1 = Color.FromArgb((int)Math.Round(ColorBalance * 255), Color1);
                var C2 = Color.FromArgb((int)Math.Round(GlowBalance * 255), Color2);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 100)), Color2)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 150)), C1.Blend(C2, 100d))))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
            }
            else
            {
                if (BackgroundBlurred is not null)
                    G.DrawImage(BackgroundBlurred, Rect);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * 255), Color.Black)))
                {
                    G.FillRectangle(br, Rect);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (ColorBalance * 255)), Color1)))
                {
                    G.FillRectangle(br, Rect);
                }

                var C1 = Color.FromArgb((int)Math.Round(ColorBalance * 255), Color1);
                var C2 = Color.FromArgb((int)Math.Round(GlowBalance * 255), Color2);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 100)), Color2)))
                {
                    G.FillRectangle(br, Rect);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 150)), C1.Blend(C2, 100d))))
                {
                    G.FillRectangle(br, Rect);
                }
            }
        }

        public static void FillRoundedRect(this Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = 5;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if ((WPStyle.GetRoundedCorners() | ForcedRoundCorner) & Radius > 0)
                {
                    using (var path = Rectangle.Round(Radius))
                    {
                        Graphics.FillPath(Brush, path);
                    }
                }
                else
                {
                    Graphics.FillRectangle(Brush, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static void DrawRoundImage(this Graphics Graphics, Image Image, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = 5;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                if ((WPStyle.GetRoundedCorners() | ForcedRoundCorner) & Radius > 0)
                {
                    using (var path = Rectangle.Round(Radius))
                    {
                        var reg = new Region(path);
                        Graphics.Clip = reg;
                        Graphics.DrawImage(Image, Rectangle);
                        Graphics.ResetClip();
                    }
                }
                else
                {
                    Graphics.DrawImage(Image, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static GraphicsPath Round(this Rectangle r, int radius)
        {
            try
            {
                var path = new GraphicsPath();
                int d = radius * 2;

                path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top);
                path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90f);

                path.AddLine(r.Right, r.Top + d, r.Right, r.Bottom - d);
                path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Bottom - d, r.Right, r.Bottom), 0f, 90f);

                path.AddLine(r.Right - d, r.Bottom, r.Left + d, r.Bottom);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - d, r.Left + d, r.Bottom), 90f, 90f);

                path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180f, 90f);

                path.CloseFigure();
                return path;
            }
            catch
            {
                return null;
            }
        }

        public static void DrawRoundedRect(this Graphics Graphics, Pen Pen, Rectangle Rectangle, int Radius_willbe_x2 = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius_willbe_x2 == -1)
                    Radius_willbe_x2 = 5;
                Radius_willbe_x2 *= 2;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if ((WPStyle.GetRoundedCorners() | ForcedRoundCorner) & Radius_willbe_x2 > 0)
                {
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius_willbe_x2, Radius_willbe_x2, 180, 90);
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius_willbe_x2 / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius_willbe_x2 / 2d), Rectangle.Y);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius_willbe_x2, Rectangle.Y, Radius_willbe_x2, Radius_willbe_x2, 270, 90);
                    Graphics.DrawLine(Pen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius_willbe_x2 / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius_willbe_x2 / 2d));
                    Graphics.DrawLine(Pen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius_willbe_x2 / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius_willbe_x2 / 2d));
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius_willbe_x2 / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius_willbe_x2 / 2d), Rectangle.Y + Rectangle.Height);
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius_willbe_x2, Radius_willbe_x2, Radius_willbe_x2, 90, 90);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius_willbe_x2, Rectangle.Y + Rectangle.Height - Radius_willbe_x2, Radius_willbe_x2, Radius_willbe_x2, 0, 90);
                }
                else
                {
                    Graphics.DrawRectangle(Pen, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static void DrawRoundedRect_LikeW11(this Graphics Graphics, Pen PenX, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                bool Dark = My.Env.Style.DarkMode;

                if (Radius == -1)
                    Radius = 5;
                Radius *= 2;
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var Pen = new Pen(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                {
                    using (var Pen2 = new Pen(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                    {
                        var SidePen = new Pen(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset };

                        if (Dark)
                        {
                            Pen.Color = PenX.Color.CB(0.1f);
                            Pen2.Color = PenX.Color;
                        }
                        else
                        {
                            Pen.Color = PenX.Color.CB((float)-0.02d);
                            Pen2.Color = PenX.Color.CB((float)-0.05d);
                        }

                        LinearGradientBrush G;
                        var CColor = Pen2.Color.CB((float)(Dark ? 0.03d : -0.05d));

                        if (Dark)
                        {
                            G = new LinearGradientBrush(Rectangle, CColor, Pen.Color, 180f);
                            var cblend = new ColorBlend(3)
                            {
                                Colors = new Color[3] { CColor, Pen.Color, CColor },
                                Positions = new float[3] { 0f, 0.5f, 1.0f }
                            };
                            G.InterpolationColors = cblend;
                        }
                        else
                        {
                            G = new LinearGradientBrush(Rectangle, Pen.Color, CColor, 180f);
                            var cblend = new ColorBlend(3)
                            {
                                Colors = new Color[3] { Pen.Color, CColor, Pen.Color },
                                Positions = new float[3] { 0f, 0.5f, 1.0f }
                            };
                            G.InterpolationColors = cblend;
                        }

                        using (var PenG = new Pen(G, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                        {

                            if ((WPStyle.GetRoundedCorners() | ForcedRoundCorner) & Radius > 0)
                            {

                                if (Dark)
                                {
                                    Graphics.DrawLine(Pen2, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);
                                    Graphics.DrawArc(Pen2, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                    Graphics.DrawArc(Pen2, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);

                                    SidePen = Pen2;

                                    Graphics.DrawLine(SidePen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));
                                    Graphics.DrawLine(SidePen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                                    Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                    Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                                    Graphics.DrawLine(PenG, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);
                                }

                                else
                                {
                                    Graphics.DrawLine(PenG, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);
                                    Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                    Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);

                                    SidePen = Pen;

                                    Graphics.DrawLine(SidePen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));
                                    Graphics.DrawLine(SidePen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);
                                }
                            }

                            else
                            {

                                if (Dark)
                                {
                                    Pen.Color = PenX.Color.CB(0.05f);
                                }
                                else
                                {
                                    Pen.Color = PenX.Color.CB((float)-0.02d);
                                }

                                Graphics.DrawRectangle(Pen, Rectangle);

                            }

                            SidePen.Dispose();
                        }
                    }
                }
            }
            catch
            {
            }

        }

    }

    public static class ComboBoxExtensions
    {

        /// <summary>
        /// Add classic themes names to a ComboBox
        /// </summary>
        public static void PopulateThemes(this ComboBox ComboBox)
        {
            ComboBox.Items.Clear();
            ComboBox.Items.AddRange(My.Resources.RetroThemesDB.CList().Select(f => f.Split('|')[0]).ToArray());
        }
    }

    public static class TreeViewExtensions
    {

        /// <summary>
        /// Serialize from a JObj File to TreeView Nodes
        /// </summary>
        public static void FromJSON(this TreeView TreeView, string JSON_File, string rootName)
        {
            var reader = new StreamReader(JSON_File);
            var jsonReader = new JsonTextReader(reader);
            var root = JToken.Load(jsonReader);
            reader.Close();

            TreeView.BeginUpdate();
            try
            {
                TreeView.Nodes.Clear();

                {
                    var temp = TreeView.Nodes.Add(rootName);
                    temp.ImageKey = "json";
                    temp.SelectedImageKey = "json";
                    temp.Tag = root;
                }

                AddNode(root, TreeView.Nodes[0]);

                TreeView.CollapseAll();
            }
            finally
            {
                TreeView.EndUpdate();
            }
        }

        private static void AddNode(JToken token, TreeNode inTreeNode)
        {
            if (token is null)
                return;

            if (token is JValue)
            {

                {
                    var temp = inTreeNode.Nodes.Add(token.ToString());
                    temp.ImageKey = "value";
                    temp.SelectedImageKey = "value";
                    temp.Tag = token;
                }
            }

            else if (token is JObject)
            {
                JObject obj = (JObject)token;

                foreach (var property in obj.Properties())
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name)).ToString()];
                    childNode.Tag = property;
                    AddNode(property.Value, childNode);
                }
            }

            else if (token is JArray)
            {
                JArray array = (JArray)token;

                for (int i = 0, loopTo = array.Count - 1; i <= loopTo; i++)
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                    childNode.Tag = array[i];
                    AddNode(array[i], childNode);
                }
            }
        }

        /// <summary>
        /// Serialize a node to JObj Formatted String
        /// </summary>
        public static string ToJSON(this TreeNode TreeNode)
        {
            var J_All = new JObject();
            J_All.RemoveAll();

            foreach (TreeNode N in TreeNode.Nodes)
            {

                var J = new JObject();
                J.RemoveAll();
                LoopThroughNodes(N, N, J);

                J_All.Add(N.Text, J);
            }

            return J_All.ToString();
        }

        private static void LoopThroughNodes(TreeNode Node, TreeNode MainNode, JObject JSON)
        {
            foreach (TreeNode N in Node.Nodes)
            {
                if (N.Nodes.Count == 1)
                {
                    JSON.Add(N.Text, N.Nodes[0].Text);
                }
                else if (N.Nodes.Count > 1)
                {
                    JSON.Add(N.Text, new JObject());
                    JObject Jx = (JObject)JSON[N.Text];
                    LoopThroughNodes(N, MainNode, Jx);
                }
            }
        }
    }

    public static class Icons
    {

        public static byte[] ToByteArray(this Icon icon)
        {
            using (var ms = new MemoryStream())
            {
                icon.Save(ms);
                byte[] b = ms.ToArray();
                ms.Close();
                return ms.ToArray();
            }
        }

        public static Icon ToIcon(this byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return new Icon(ms);
            }
        }

    }

    public static class StringFormatExtensions
    {
        public static StringFormat ToStringFormat(this ContentAlignment TextAlign, bool RightToLeft = false)
        {
            var SF = new StringFormat();
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    {
                        SF.LineAlignment = StringAlignment.Near;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }
                case ContentAlignment.TopCenter:
                    {
                        SF.LineAlignment = StringAlignment.Near;
                        SF.Alignment = StringAlignment.Center;
                        break;
                    }
                case ContentAlignment.TopRight:
                    {
                        SF.LineAlignment = StringAlignment.Near;
                        SF.Alignment = StringAlignment.Far;
                        break;
                    }
                case ContentAlignment.MiddleLeft:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }
                case ContentAlignment.MiddleCenter:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Center;
                        break;
                    }
                case ContentAlignment.MiddleRight:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Far;
                        break;
                    }
                case ContentAlignment.BottomLeft:
                    {
                        SF.LineAlignment = StringAlignment.Far;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }
                case ContentAlignment.BottomCenter:
                    {
                        SF.LineAlignment = StringAlignment.Far;
                        SF.Alignment = StringAlignment.Center;
                        break;
                    }
                case ContentAlignment.BottomRight:
                    {
                        SF.LineAlignment = StringAlignment.Far;
                        SF.Alignment = StringAlignment.Far;
                        break;
                    }

                default:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }

            }

            if (RightToLeft)
                SF.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            return SF;
        }
    }
}