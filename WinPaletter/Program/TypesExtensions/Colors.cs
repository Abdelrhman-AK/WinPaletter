using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="Color"/> structure
    /// </summary>
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
                S = $"{(string.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B))}{(string.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B))}{(string.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B))}{(string.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B))}";
            }
            else
            {
                S = $"{Color.R:X2}{Color.G:X2}{Color.B:X2}";
            }
            if (Hash)
                S = $"#{S}";
            return S;
        }

        /// <summary>
        /// Return HSL From RGB Color
        /// </summary>
        public static HSL ToHSL(this Color Color)
        {
            HSL _hsl = new();

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
        /// Saturate a color by a given percentage
        /// </summary>
        /// <param name="color"></param>
        /// <param name="saturation"></param>
        /// <returns></returns>
        public static Color Saturate(this Color color, float saturation)
        {
            HSL hsl = color.ToHSL();
            hsl.S = Math.Min(Math.Max(hsl.S * saturation, 0), 100); // Adjust saturation
            return hsl.ToRGB();
        }

        /// <summary>
        /// Return RGB Color From HSL
        /// </summary>
        public static Color ToRGB(this HSL hsl)
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
            return $"{Color.ToHSL().H} {Math.Round((double)(Color.ToHSL().S * 100f))}% {Math.Round((double)(Color.ToHSL().L * 100f))}%";
        }

        /// <summary>
        /// Get String in the format of R G B from a Color(R,G,B) or A R G B from a Color(A,R,G,B)
        /// </summary>
        public static string ToWin32Reg(this Color Color, bool Alpha = false)
        {
            if (!Alpha)
            {
                return $"{Color.R} {Color.G} {Color.B}";
            }
            else
            {
                return $"{Color.A} {Color.R} {Color.G} {Color.B}";
            }
        }

        /// <summary>
        /// Change color brightness
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

            red = Math.Min(Math.Max(red, 0f), 255f);
            green = Math.Min(Math.Max(green, 0f), 255f);
            blue = Math.Min(Math.Max(blue, 0f), 255f);

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        /// <summary>
        /// Get Color as string in the format you choose
        /// </summary>
        public static string ReturnFormat(this Color Color, ColorFormat Format, bool HexHash = false, bool Alpha = false)
        {
            string s = Program.Lang.Empty;

            if (Format == ColorFormat.Default)
            {
                if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.HEX) Format = ColorFormat.HEX;
                else if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.RGB) Format = ColorFormat.RGB;
                else if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.HSL) Format = ColorFormat.HSL;
                else if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.Dec) Format = ColorFormat.Dec;
                else Format = ColorFormat.HEX;
            }

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
                s = Program.Lang.Empty;
            }

            return s;
        }

        /// <summary>
        /// Get Color as string in the format you choose
        /// </summary>
        public enum ColorFormat
        {
            /// <summary>
            /// HEX color format
            /// </summary>
            HEX,
            /// <summary>
            /// RGB color format
            /// </summary>
            RGB,
            /// <summary>
            /// HSL color format
            /// </summary>
            HSL,
            /// <summary>
            /// Decimal color format
            /// </summary>
            Dec,
            /// <summary>
            /// Default color format: HEX
            /// </summary>
            Default
        }

        /// <summary>
        /// Get image From Color
        /// </summary>
        public static Bitmap ToBitmap(this Color Color, Size Size)
        {
            Bitmap b = new(Size.Width, Size.Height);
            Graphics G = Graphics.FromImage(b);
            G.Clear(Color);
            G.Save();
            return b;
            G.Dispose();
            b.Dispose();
        }

        /// <summary>Blends the specified colors together.</summary>
        /// <param name="color">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amount">How much of <paramref name="color"/> to keep,
        /// “on top of” <paramref name="backColor"/>.</param>
        /// <returns>The blended colors.</returns>
        public static Color Blend(this Color color, Color backColor, double amount)
        {
            byte a = (byte)(color.A * amount + backColor.A * (1 - amount));
            byte r = (byte)(color.R * amount + backColor.R * (1 - amount));
            byte g = (byte)(color.G * amount + backColor.G * (1 - amount));
            byte b = (byte)(color.B * amount + backColor.B * (1 - amount));
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

        /// <summary>
        /// HSL color structure
        /// </summary>
        public struct HSL
        {
            /// <summary>
            /// Generate a HSL structure
            /// </summary>
            /// <param name="h"></param>
            /// <param name="s"></param>
            /// <param name="l"></param>
            public HSL(int h, float s, float l)
            {
                H = h;
                S = s;
                L = l;
            }

            /// <summary>
            /// Hue
            /// </summary>
            public int H { get; set; }

            /// <summary>
            /// Saturation
            /// </summary>
            public float S { get; set; }

            /// <summary>
            /// Lightness
            /// </summary>
            public float L { get; set; }

            /// <summary>
            /// Check if two HSL colors are equal
            /// </summary>
            /// <param name="hsl"></param>
            /// <returns></returns>
            public bool Equals(HSL hsl)
            {
                return H == hsl.H && S == hsl.S && L == hsl.L;
            }

            /// <summary>
            /// Add two HSL colors
            /// </summary>
            /// <param name="hsl1"></param>
            /// <param name="hsl2"></param>
            /// <returns></returns>
            public static HSL operator +(HSL hsl1, HSL hsl2)
            {
                return new HSL(Math.Min(Math.Max(hsl1.H + hsl2.H, 0), 360), Math.Min(Math.Max(hsl1.S + hsl2.S, 0), 100), Math.Min(Math.Max(hsl1.L + hsl2.L, 0), 100));
            }

            /// <summary>
            /// Subtract two HSL colors
            /// </summary>
            /// <param name="hsl1"></param>
            /// <param name="hsl2"></param>
            /// <returns></returns>
            public static HSL operator -(HSL hsl1, HSL hsl2)
            {
                return new HSL(Math.Min(Math.Max(hsl1.H - hsl2.H, 0), 360), Math.Min(Math.Max(hsl1.S - hsl2.S, 0), 100), Math.Min(Math.Max(hsl1.L - hsl2.L, 0), 100));
            }
        }
    }
}
