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
        /// Reverse Color From RGB To BGR or RGBA To ABGR (As used in some Windows Registry values)
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
        public static string ToStringHex(this Color Color, bool Alpha = true, bool Hash = false)
        {
            string S;
            if (Alpha)
            {
                S = $"{string.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B)}{string.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B)}{string.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B)}{string.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B)}";
            }
            else
            {
                S = $"{Color.R:X2}{Color.G:X2}{Color.B:X2}";
            }

            return !Hash ? S : $"#{S}";
        }

        /// <summary>
        /// Convert Color (ARGB) to HSL
        /// </summary>
        public static HSL ToHSL(this Color color)
        {
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));
            float delta = max - min;

            HSL hsl = new()
            {
                L = (max + min) / 2f
            };

            if (delta == 0f)
            {
                hsl.H = 0f;
                hsl.S = 0f;
            }
            else
            {
                // Saturation
                hsl.S = hsl.L <= 0.5f ? delta / (max + min) : delta / (2f - max - min);

                // Hue
                float hue;
                if (r == max)
                    hue = (g - b) / delta;
                else if (g == max)
                    hue = 2f + (b - r) / delta;
                else
                    hue = 4f + (r - g) / delta;

                hue *= 60f; // convert to degrees
                if (hue < 0f) hue += 360f;

                hsl.H = hue;
            }

            return hsl;
        }

        /// <summary>
        /// Convert HSL to Color (RGB)
        /// </summary>
        public static Color ToRGB(this HSL hsl)
        {
            float r, g, b;

            if (hsl.S == 0f)
            {
                // Achromatic (gray)
                r = g = b = hsl.L;
            }
            else
            {
                float q = hsl.L < 0.5f ? hsl.L * (1f + hsl.S) : hsl.L + hsl.S - hsl.L * hsl.S;
                float p = 2f * hsl.L - q;

                float hk = hsl.H / 360f;
                float[] t = new float[3];
                t[0] = hk + 1f / 3f; // r
                t[1] = hk;           // g
                t[2] = hk - 1f / 3f; // b

                for (int i = 0; i < 3; i++)
                {
                    if (t[i] < 0f) t[i] += 1f;
                    if (t[i] > 1f) t[i] -= 1f;

                    if (t[i] < 1f / 6f)
                        t[i] = p + (q - p) * 6f * t[i];
                    else if (t[i] < 1f / 2f)
                        t[i] = q;
                    else if (t[i] < 2f / 3f)
                        t[i] = p + (q - p) * (2f / 3f - t[i]) * 6f;
                    else
                        t[i] = p;
                }

                r = t[0];
                g = t[1];
                b = t[2];
            }

            // Inline clamp to 0–255
            byte R = (byte)((r < 0f ? 0f : (r > 1f ? 1f : r)) * 255f);
            byte G = (byte)((g < 0f ? 0f : (g > 1f ? 1f : g)) * 255f);
            byte B = (byte)((b < 0f ? 0f : (b > 1f ? 1f : b)) * 255f);

            return Color.FromArgb(R, G, B);
        }

        /// <summary>
        /// Return HSL String in the format of H S% L% From a RGB Color
        /// </summary>
        public static string ToStringHSL(this Color Color)
        {
            return $"{Color.ToHSL().H} {Math.Round((double)(Color.ToHSL().S * 100f))}% {Math.Round((double)(Color.ToHSL().L * 100f))}%";
        }

        /// <summary>
        /// Get String in the format of R G B from a Color(R,G,B) or A R G B from a Color(A,R,G,B)
        /// </summary>
        public static string ToStringWin32(this Color Color, bool Alpha = false)
        {
            return (Alpha ? $"{Color.A} " : string.Empty) + $"{Color.R} {Color.G} {Color.B}";
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
            string s = Program.Lang.Strings.General.Empty;

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
                            s = Color.ToStringHex(Alpha, HexHash);
                            break;
                        }

                    case ColorFormat.RGB:
                        {
                            s = Color.ToStringWin32(Alpha);
                            break;
                        }

                    case ColorFormat.HSL:
                        {
                            s = Color.ToStringHSL();
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
                s = Program.Lang.Strings.General.Empty;
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
        /// Create a solid color bitmap of the given size.
        /// </summary>
        public static Bitmap ToBitmap(this Color color, Size size)
        {
            Bitmap bmp = new(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(color);
            }
            return bmp;
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
        /// Determines if the color is dark based on relative luminance.
        /// </summary>
        public static bool IsDark(this Color color)
        {
            double luminance = color.R * 0.2126 +
                               color.G * 0.7152 +
                               color.B * 0.0722;

            return luminance <= 127.5;
        }

        /// <summary>
        /// HSL color structure
        /// </summary>
        /// <remarks>
        /// Generate a HSL structure
        /// </remarks>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="l"></param>
        public struct HSL(int h, float s, float l)
        {
            /// <summary>
            /// Hue
            /// </summary>
            public float H { get; set; } = h;

            /// <summary>
            /// Saturation
            /// </summary>
            public float S { get; set; } = s;

            /// <summary>
            /// Lightness
            /// </summary>
            public float L { get; set; } = l;

            /// <summary>
            /// Check if two HSL colors are equal
            /// </summary>
            /// <param name="hsl"></param>
            /// <returns></returns>
            public readonly bool Equals(HSL hsl)
            {
                return H == hsl.H && S == hsl.S && L == hsl.L;
            }

            /// <summary>
            /// Adds the corresponding components of two <see cref="HSL"/> color values.
            /// </summary>
            /// <remarks>This operator performs component-wise addition of the hue, saturation, and
            /// lightness values. - The hue is adjusted to remain within the range [0, 360) by wrapping around if it
            /// exceeds the bounds. - The saturation and lightness values are clamped to the range [0, 1] to ensure
            /// valid HSL values.</remarks>
            /// <param name="hsl1">The first <see cref="HSL"/> color value.</param>
            /// <param name="hsl2">The second <see cref="HSL"/> color value.</param>
            /// <returns>A new <see cref="HSL"/> instance representing the sum of the two input colors.  The resulting hue is
            /// wrapped within the range [0, 360), and the saturation and lightness  are clamped to the range [0, 1].</returns>
            public static HSL operator +(HSL hsl1, HSL hsl2)
            {
                float h = hsl1.H + hsl2.H;
                if (h > 360f) h -= 360f;
                if (h < 0f) h += 360f;

                float s = hsl1.S + hsl2.S;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = hsl1.L + hsl2.L;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l };
            }

            /// <summary>
            /// Subtracts the components of one <see cref="HSL"/> color from another and returns the resulting <see
            /// cref="HSL"/> color.
            /// </summary>
            /// <remarks>This operator performs component-wise subtraction of the hue, saturation, and
            /// lightness values of the two <see cref="HSL"/> colors. - The hue is adjusted to wrap around within the
            /// range [0, 360]. - The saturation and lightness values are clamped to ensure they remain within the valid
            /// range of [0, 1].</remarks>
            /// <param name="hsl1">The first <see cref="HSL"/> color to subtract from.</param>
            /// <param name="hsl2">The second <see cref="HSL"/> color to subtract.</param>
            /// <returns>A new <see cref="HSL"/> color representing the result of subtracting the components of <paramref
            /// name="hsl2"/> from <paramref name="hsl1"/>. The resulting hue is adjusted to remain within the range [0,
            /// 360], and the saturation and lightness are clamped to the range [0, 1].</returns>
            public static HSL operator -(HSL hsl1, HSL hsl2)
            {
                float h = hsl1.H - hsl2.H;
                if (h > 360f) h -= 360f;
                if (h < 0f) h += 360f;

                float s = hsl1.S - hsl2.S;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = hsl1.L - hsl2.L;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l };
            }

        }
    }
}
