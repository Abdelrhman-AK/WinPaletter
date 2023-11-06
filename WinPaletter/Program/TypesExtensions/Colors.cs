using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
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
            string s = Program.Lang.Empty;

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
        public static Bitmap ToBitmap(this Color Color, Size Size)
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

}
