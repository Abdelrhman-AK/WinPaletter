using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static WinPaletter.Settings.Structures.NerdStats;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="Color"/> structure
    /// </summary>
    public static class ColorsExtensions
    {
        /// <summary>
        /// Converts a <see cref="Color"/> to string in the specified <see cref="Formats"/>.
        /// </summary>
        public static string ToString(this Color color, Formats format, bool hexHash = false, bool shortForm = false)
        {
            if (color.A == 0 && color.R == 0 && color.G == 0 && color.B == 0)
                return Program.Lang.Strings.General.Empty;

            // Handle "default" separately (not in enum)
            if (format == (Formats)(-1) || format == default) format = Program.Settings.NerdStats.Type;

            return format switch
            {
                Formats.HEX => color.ToHexString(hash: hexHash, alpha: color.A < 255),
                Formats.RGB => color.ToRgbString(shortForm),
                Formats.RGBPercent => color.ToRgbPercentString(shortForm),
                Formats.ARGB => color.ToArgbString(shortForm),
                Formats.HSL => color.ToHslString(shortForm),
                Formats.HSLA => color.ToHslaString(shortForm),
                Formats.HSV => color.ToHsvString(shortForm),
                Formats.CMYK => color.ToCmykString(shortForm),
                Formats.Dec => shortForm ? color.ToArgb().ToString() : $"Decimal: {color.ToArgb()}",
                Formats.Win32 => $"0x{(color.B << 16 | color.G << 8 | color.R):X6}",
                Formats.KnownName => color.IsKnownColor ? color.Name : color.ToHexString(hash: hexHash),
                Formats.CSS => color.ToCssString(shortForm),
                _ => color.ToHexString(hash: hexHash)
            };
        }

        /// <summary>
        /// Converts a string representation of a color into a <see cref="Color"/> object.
        /// </summary>
        /// <remarks>This method supports a wide range of color formats, including: <list type="bullet">
        /// <item><description>Hexadecimal color codes (e.g., "#RRGGBB", "#AARRGGBB").</description></item>
        /// <item><description>Known color names (e.g., "Red", "Blue").</description></item> <item><description>Decimal
        /// ARGB values (e.g., "4294901760").</description></item> <item><description>Functional notations such as RGB,
        /// ARGB, HSL, HSLA, HSV, HSVA, and CMYK.</description></item> </list> If the input string contains invalid or
        /// unsupported formats, the method gracefully falls back to returning <see cref="Color.Empty"/>.</remarks>
        /// <param name="input">The string representation of the color. This can be in various formats, such as: hexadecimal (e.g.,
        /// "#RRGGBB", "#AARRGGBB"), known color names (e.g., "Red", "Blue"), decimal ARGB values, or functional
        /// notations (e.g., "rgb(255, 0, 0)", "hsl(0, 100%, 50%)").</param>
        /// <returns>A <see cref="Color"/> object that represents the parsed color. If the input string is null, empty, or cannot
        /// be parsed into a valid color, the method returns <see cref="Color.Empty"/>.</returns>
        public static Color ToColor(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Color.Empty;

            string s = input.Trim().ToLowerInvariant();

            // Normalize various wrappers and delimiters
            s = Regex.Replace(s, @"color(\.fromargb)?|\[|\]", "", RegexOptions.IgnoreCase);
            s = s.Replace("(", " ").Replace(")", " ").Replace("=", " ");
            s = Regex.Replace(s, @"[,:;/]+", " ");
            s = Regex.Replace(s, @"\s+", " ").Trim();

            // --- HEX ---
            if (Regex.IsMatch(s, @"^#?[0-9a-f]{3,8}$"))
            {
                string hex = s.TrimStart('#');

                if (hex.Length == 3) hex = string.Concat(hex.Select(c => $"{c}{c}")); // #rgb
                if (hex.Length == 4) hex = string.Concat(hex.Select(c => $"{c}{c}")); // #argb

                if (hex.Length == 6) // RRGGBB
                    return Color.FromArgb(255,
                        Convert.ToInt32(hex.Substring(0, 2), 16),
                        Convert.ToInt32(hex.Substring(2, 2), 16),
                        Convert.ToInt32(hex.Substring(4, 2), 16));

                if (hex.Length == 8) // AARRGGBB
                    return Color.FromArgb(
                        Convert.ToInt32(hex.Substring(0, 2), 16),
                        Convert.ToInt32(hex.Substring(2, 2), 16),
                        Convert.ToInt32(hex.Substring(4, 2), 16),
                        Convert.ToInt32(hex.Substring(6, 2), 16));
            }

            // --- Known CSS color names ---
            var known = Color.FromName(input.Trim());
            if (known.IsKnownColor)
                return known;

            // --- Decimal ARGB ---
            if (int.TryParse(s, out int dec))
            {
                try { return Color.FromArgb(dec); } catch { }
            }

            // Extract numbers
            var nums = Regex.Matches(s, @"\d+(\.\d+)?%?")
                            .Cast<Match>()
                            .Select(m => m.Value)
                            .ToList();

            if (nums.Count > 0)
            {
                int Parse(string v, int max)
                {
                    if (v.EndsWith("%"))
                    {
                        double pct = double.Parse(v.TrimEnd('%'), CultureInfo.InvariantCulture) / 100.0;
                        return (int)Math.Round(pct * max);
                    }
                    return int.Parse(v, CultureInfo.InvariantCulture);
                }

                switch (nums.Count)
                {
                    case 3: // RGB / HSL / HSV
                        if (s.Contains("hsv") || s.Contains("v"))
                        {
                            int h = Parse(nums[0], 360);
                            int sat = Parse(nums[1], 100);
                            int v = Parse(nums[2], 100);
                            return HSVToColor(h, sat, v);
                        }
                        else if (s.Contains("hsl") || s.Contains("l"))
                        {
                            int h = Parse(nums[0], 360);
                            int sat = Parse(nums[1], 100);
                            int l = Parse(nums[2], 100);
                            return new HSL(h, sat, l).ToRGB();
                        }
                        else
                        {
                            int r = Parse(nums[0], 255);
                            int g = Parse(nums[1], 255);
                            int b = Parse(nums[2], 255);
                            return Color.FromArgb(255, r, g, b);
                        }

                    case 4: // ARGB / HSLA / HSVA / CMYK
                        if (s.Contains("hsva") || (s.Contains("hsv") && nums.Count == 4))
                        {
                            int h = Parse(nums[0], 360);
                            int sat = Parse(nums[1], 100);
                            int v = Parse(nums[2], 100);
                            int a = Parse(nums[3], 255);
                            return HSVToColor(h, sat, v, a);
                        }
                        else if (s.Contains("hsla") || (s.Contains("hsl") && nums.Count == 4))
                        {
                            int h = Parse(nums[0], 360);
                            int sat = Parse(nums[1], 100);
                            int l = Parse(nums[2], 100);
                            int a = Parse(nums[3], 255);
                            return new HSL(h, sat, l, a).ToRGB();
                        }
                        else if (s.Contains("cmyk"))
                        {
                            int c = Parse(nums[0], 100);
                            int m = Parse(nums[1], 100);
                            int y = Parse(nums[2], 100);
                            int k = Parse(nums[3], 100);
                            return CmykToRgb(c, m, y, k);
                        }
                        else
                        {
                            int a = Parse(nums[0], 255);
                            int r = Parse(nums[1], 255);
                            int g = Parse(nums[2], 255);
                            int b = Parse(nums[3], 255);
                            return Color.FromArgb(a, r, g, b);
                        }
                }
            }

            return Color.Empty;
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to its hexadecimal string representation.
        /// </summary>
        /// <remarks>The method does not produce shortened hexadecimal formats (e.g., #FFF). The output is
        /// always in full-length format, such as #RRGGBB or #AARRGGBB.</remarks>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="alpha">A value indicating whether the alpha channel should be included in the hexadecimal string. If <see
        /// langword="true"/>, the alpha channel is included; otherwise, it is omitted.</param>
        /// <param name="hash">A value indicating whether the hexadecimal string should be prefixed with a hash symbol (#). If <see
        /// langword="true"/>, the string is prefixed with a hash symbol; otherwise, it is not.</param>
        /// <returns>A string representing the <see cref="Color"/> in hexadecimal format. The string will always contain 6 or 8
        /// characters (depending on the value of <paramref name="alpha"/>), and will optionally be prefixed with a hash
        /// symbol (depending on the value of <paramref name="hash"/>).</returns>
        public static string ToHexString(this Color c, bool alpha = true, bool hash = false)
        {
            // Always full 6 or 8 digits, never shortened (#FFF style not allowed here)
            string hex = alpha
                ? $"{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}"
                : $"{c.R:X2}{c.G:X2}{c.B:X2}";
            return hash ? $"#{hex}" : hex;
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to a string representation in the Win32 format.
        /// </summary>
        /// <param name="Color">The <see cref="Color"/> to convert.</param>
        /// <param name="Alpha">A value indicating whether the alpha channel value should be included in the output. If <see langword="true"/>, the
        /// alpha value is included; otherwise, it is omitted.</param>
        /// <returns>A string representing the color in the format "R G B" or "A R G B" if the alpha channel is included.</returns>
        public static string ToStringWin32(this Color Color, bool Alpha = false)
        {
            return (Alpha ? $"{Color.A} " : string.Empty) + $"{Color.R} {Color.G} {Color.B}";
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to a string representation in RGB or ARGB format.
        /// </summary>
        /// <remarks>The short-form representation is a compact, space-separated list of numeric values, 
        /// while the long-form representation includes labeled components (e.g., "R=255, G=0, B=0").</remarks>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A boolean value indicating whether to use a short-form representation.  If <see langword="true"/>, the
        /// method returns a space-separated list of numeric values.  If <see langword="false"/>, the method returns a
        /// more descriptive format with labeled components.</param>
        /// <returns>A string representing the color in either RGB or ARGB format.  If the alpha channel is less than 255, the
        /// output includes the alpha component; otherwise, only the RGB components are included.</returns>
        public static string ToRgbString(this Color c, bool shortForm = false) =>
            shortForm
                ? (c.A < 255 ? $"{c.A} {c.R} {c.G} {c.B}" : $"{c.R} {c.G} {c.B}")
                : (c.A < 255 ? $"A={c.A}, R={c.R}, G={c.G}, B={c.B}" : $"R={c.R}, G={c.G}, B={c.B}");

        /// <summary>
        /// Converts the specified <see cref="Color"/> to a string representation of its RGB(A) values as percentages.
        /// </summary>
        /// <remarks>The percentages are calculated by dividing the color component values by 2.55, 
        /// converting the 0-255 range to a 0-100% range. The alpha (A) value is included  only if it is less than 255
        /// (fully opaque).</remarks>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A boolean value indicating whether to use the short form of the string representation.  If <see
        /// langword="true"/>, the output will omit decimal places and use a compact format.</param>
        /// <returns>A string representing the RGB(A) values of the color as percentages.  The format will be "rgb(R%, G%, B%)"
        /// or "rgb(R%, G%, B%) / A%" for the long form,  and "R% G% B%" or "R% G% B% A%" for the short form.</returns>
        public static string ToRgbPercentString(this Color c, bool shortForm = false) =>
            shortForm
                ? (c.A < 255
                    ? $"{c.R / 2.55:F0}% {c.G / 2.55:F0}% {c.B / 2.55:F0}% {c.A / 2.55:F0}%"
                    : $"{c.R / 2.55:F0}% {c.G / 2.55:F0}% {c.B / 2.55:F0}%")
                : $"rgb({c.R / 2.55:F1}%, {c.G / 2.55:F1}%, {c.B / 2.55:F1}%)" +
                  (c.A < 255 ? $" / {c.A / 2.55:F1}%" : "");

        /// <summary>
        /// Converts the specified <see cref="Color"/> to a string representation of its ARGB components.
        /// </summary>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A boolean value indicating whether to use a short format.  If <see langword="true"/>, the output is in the
        /// format "A R G B".  If <see langword="false"/>, the output is in the format "A=..., R=..., G=..., B=...".</param>
        /// <returns>A <see cref="string"/> representing the ARGB components of the <see cref="Color"/>.</returns>
        public static string ToArgbString(this Color c, bool shortForm = false) =>
            shortForm ? $"{c.A} {c.R} {c.G} {c.B}" : $"A={c.A}, R={c.R}, G={c.G}, B={c.B}";

        /// <summary>
        /// Converts the specified <see cref="Color"/> to its HSL (Hue, Saturation, Lightness) string representation.
        /// </summary>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A boolean value indicating whether to use the short form of the HSL string. If <see langword="true"/>, the
        /// result omits symbols (e.g., "°", "%").  If <see langword="false"/>, the result includes symbols for clarity.</param>
        /// <returns>A string representing the HSL values of the color. The format is: - Short form: "H S L" (e.g., "240 100
        /// 50"). - Long form: "H°, S%, L%" (e.g., "240°, 100%, 50%").</returns>
        public static string ToHslString(this Color c, bool shortForm = false)
        {
            var hsl = c.ToHSL();
            return shortForm
                ? $"{hsl.H:F0} {hsl.S * 100:F0} {hsl.L * 100:F0}"
                : $"{hsl.H:F0}°, {hsl.S * 100:F0}%, {hsl.L * 100:F0}%";
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to its HSLA (Hue, Saturation, Lightness, Alpha) string
        /// representation.
        /// </summary>
        /// <remarks>The HSLA representation is useful for working with colors in terms of their hue,
        /// saturation, and lightness,  which can be more intuitive for certain applications, such as UI design or color
        /// manipulation.</remarks>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A boolean value indicating whether to use the short form of the HSLA string.  If <see langword="true"/>, the
        /// method returns a space-separated string with integer values for hue,  saturation, and lightness (as
        /// percentages), and the alpha value as an integer.  If <see langword="false"/>, the method returns a
        /// CSS-compatible "hsla(...)" string with percentages  for saturation and lightness, and the alpha value as a
        /// decimal between 0 and 1.</param>
        /// <returns>A string representing the HSLA values of the color. The format depends on the value of <paramref
        /// name="shortForm"/>.</returns>
        public static string ToHslaString(this Color c, bool shortForm = false)
        {
            var hsl = c.ToHSL();
            return shortForm
                ? $"{hsl.H:F0} {hsl.S * 100:F0} {hsl.L * 100:F0} {c.A}"
                : $"hsla({hsl.H:F0}, {hsl.S:P0}, {hsl.L:P0}, {c.A / 255.0:F2})";
        }

        /// <summary>
        /// Converts the color to a string representation in the HSV (hue, saturation, value) color space.
        /// </summary>
        /// <remarks>The hue (H) is represented in degrees (0–360), while saturation (S) and value (V) are
        /// represented  as percentages. The alpha (A) value, if included, is represented as a fraction between 0 and
        /// 1.</remarks>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A boolean value indicating whether to use a short-form representation.  If <see langword="true"/>, the
        /// output is a space-separated string of numeric values.  If <see langword="false"/>, the output is a formatted
        /// string in the "hsv(...)" or "hsva(...)" format.</param>
        /// <returns>A string representing the color in the HSV color space. If <paramref name="shortForm"/> is  <see
        /// langword="true"/>, the format is "H S V" or "H S V A" (if the alpha channel is less than 255).  If <paramref
        /// name="shortForm"/> is <see langword="false"/>, the format is "hsv(H, S%, V%)" or  "hsva(H, S%, V%, A)" (if
        /// the alpha channel is less than 255).</returns>
        public static string ToHsvString(this Color c, bool shortForm = false)
        {
            (double h, double s, double v) = c.ToHSV();
            return shortForm
                ? $"{h:F0} {s * 100:F0} {v * 100:F0}" + (c.A < 255 ? $" {c.A}" : "")
                : (c.A < 255
                    ? $"hsva({h:F0}, {s:P0}, {v:P0}, {c.A / 255.0:F2})"
                    : $"hsv({h:F0}, {s:P0}, {v:P0})");
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to a CMYK string representation.
        /// </summary>
        /// <remarks>The CMYK values are calculated from the RGB components of the color and are expressed
        /// as percentages. The alpha value, if included, is represented as a fraction of 1 in the detailed format or as
        /// an integer in the short format.</remarks>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A value indicating whether to use the short form of the CMYK string. If <see langword="true"/>, the output
        /// will be in a compact format with percentages and optional alpha. If <see langword="false"/>, the output will
        /// be in a more detailed format with explicit CMYK(A) notation.</param>
        /// <returns>A string representing the CMYK values of the color. If the color has an alpha value less than 255,  the
        /// alpha component is included in the output.</returns>
        public static string ToCmykString(this Color c, bool shortForm = false)
        {
            (double cc, double mm, double yy, double kk) = c.ToCMYK();
            return shortForm
                ? $"{cc * 100:F0} {mm * 100:F0} {yy * 100:F0} {kk * 100:F0}" + (c.A < 255 ? $" {c.A}" : "")
                : (c.A < 255
                    ? $"cmyka({cc:P0}, {mm:P0}, {yy:P0}, {kk:P0}, {c.A / 255.0:F2})"
                    : $"cmyk({cc:P0}, {mm:P0}, {yy:P0}, {kk:P0})");
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to its CSS string representation.
        /// </summary>
        /// <remarks>The method handles both known and custom colors. For custom colors, the alpha channel
        /// is included  in the output if it is less than 255.</remarks>
        /// <param name="c">The <see cref="Color"/> to convert.</param>
        /// <param name="shortForm">A boolean value indicating whether to use a short-form representation.  If <see langword="true"/>, the
        /// method returns a space-separated string of color components.  If <see langword="false"/>, the method returns
        /// a CSS-compliant "rgb" or "rgba" string.</param>
        /// <returns>A string representing the color in CSS format. If the color is a known color, its name is returned  in
        /// lowercase. Otherwise, the method returns either a short-form string or a CSS "rgb"/"rgba" string,  depending
        /// on the value of <paramref name="shortForm"/>.</returns>
        public static string ToCssString(this Color c, bool shortForm = false) =>
            c.IsKnownColor
                ? c.Name.ToLower()
                : shortForm
                    ? (c.A < 255
                        ? $"{c.R} {c.G} {c.B} {c.A}"
                        : $"{c.R} {c.G} {c.B}")
                    : (c.A < 255
                        ? $"rgba({c.R}, {c.G}, {c.B}, {c.A / 255.0:F2})"
                        : $"rgb({c.R}, {c.G}, {c.B})");

        /// <summary>
        /// Converts an HSV (hue, saturation, value) color representation to an ARGB <see cref="Color"/>.
        /// </summary>
        /// <remarks>The method converts the HSV color model, commonly used in graphics applications, to
        /// the ARGB color model. The resulting color is represented as a <see cref="Color"/> structure, which includes
        /// the alpha channel for transparency.</remarks>
        /// <param name="h">The hue component of the color, in degrees. Must be in the range 0 to 360.</param>
        /// <param name="s">The saturation component of the color, as a percentage. Must be in the range 0 to 100.</param>
        /// <param name="v">The value (brightness) component of the color, as a percentage. Must be in the range 0 to 100.</param>
        /// <param name="a">The alpha (transparency) component of the color. Must be in the range 0 to 255. Defaults to 255 (fully
        /// opaque).</param>
        /// <returns>A <see cref="Color"/> structure representing the equivalent ARGB color.</returns>
        private static Color HSVToColor(int h, int s, int v, int a = 255)
        {
            double hh = h / 360.0, ss = s / 100.0, vv = v / 100.0;
            int hi = (int)Math.Floor(hh * 6) % 6;
            double f = hh * 6 - Math.Floor(hh * 6);

            double p = vv * (1 - ss);
            double q = vv * (1 - f * ss);
            double t = vv * (1 - (1 - f) * ss);

            double r = 0, g = 0, b = 0;
            switch (hi)
            {
                case 0: r = vv; g = t; b = p; break;
                case 1: r = q; g = vv; b = p; break;
                case 2: r = p; g = vv; b = t; break;
                case 3: r = p; g = q; b = vv; break;
                case 4: r = t; g = p; b = vv; break;
                case 5: r = vv; g = p; b = q; break;
            }

            return Color.FromArgb(a,
                (int)Math.Round(r * 255),
                (int)Math.Round(g * 255),
                (int)Math.Round(b * 255));
        }

        /// <summary>
        /// Converts a hue value to its corresponding RGB component value.
        /// </summary>
        /// <remarks>This method is typically used as part of the HSL-to-RGB color conversion process. The
        /// input parameter <paramref name="t"/> is expected to be normalized to the range [0, 1]. If <paramref
        /// name="t"/> falls outside this range, it is adjusted by wrapping around (e.g., adding or subtracting 1 as
        /// needed).</remarks>
        /// <param name="p">The first intermediate value used in the RGB conversion process. Typically derived from lightness and
        /// saturation.</param>
        /// <param name="q">The second intermediate value used in the RGB conversion process. Typically derived from lightness and
        /// saturation.</param>
        /// <param name="t">The hue value to be converted, represented as a fractional value in the range [0, 1].</param>
        /// <returns>A double representing the RGB component value, in the range [0, 1].</returns>
        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1.0 / 6.0) return p + (q - p) * 6 * t;
            if (t < 1.0 / 2.0) return q;
            if (t < 2.0 / 3.0) return p + (q - p) * (2.0 / 3.0 - t) * 6;
            return p;
        }

        /// <summary>
        /// Converts CMYK color values to an RGB color.
        /// </summary>
        /// <remarks>The method calculates the RGB values by converting the CMYK percentages into their
        /// corresponding RGB components. The resulting color is always fully opaque (alpha value of 255).</remarks>
        /// <param name="c">The cyan component of the color, expressed as an integer percentage (0 to 100).</param>
        /// <param name="m">The magenta component of the color, expressed as an integer percentage (0 to 100).</param>
        /// <param name="y">The yellow component of the color, expressed as an integer percentage (0 to 100).</param>
        /// <param name="k">The black (key) component of the color, expressed as an integer percentage (0 to 100).</param>
        /// <returns>A <see cref="Color"/> structure representing the equivalent RGB color.</returns>
        private static Color CmykToRgb(int c, int m, int y, int k)
        {
            double C = c / 100.0, M = m / 100.0, Y = y / 100.0, K = k / 100.0;
            int r = (int)Math.Round(255 * (1 - C) * (1 - K));
            int g = (int)Math.Round(255 * (1 - M) * (1 - K));
            int b = (int)Math.Round(255 * (1 - Y) * (1 - K));
            return Color.FromArgb(255, r, g, b);
        }

        /// <summary>
        /// Convert System.Drawing.Color to HSV (Hue 0–360, Saturation 0–1, Value 0–1).
        /// </summary>
        public static (double H, double S, double V) ToHSV(this Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double delta = max - min;

            double h = 0;

            if (delta > 0)
            {
                if (max == r)
                    h = 60 * (((g - b) / delta) % 6);
                else if (max == g)
                    h = 60 * (((b - r) / delta) + 2);
                else
                    h = 60 * (((r - g) / delta) + 4);
            }

            if (h < 0) h += 360;

            double s = max == 0 ? 0 : delta / max;
            double v = max;

            return (h, s, v);
        }

        /// <summary>
        /// Convert System.Drawing.Color to CMYK (0–1 ranges).
        /// </summary>
        public static (double C, double M, double Y, double K) ToCMYK(this Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double k = 1 - Math.Max(r, Math.Max(g, b));

            if (Math.Abs(k - 1.0) < 0.00001)
            {
                // pure black
                return (0, 0, 0, 1);
            }

            double c = (1 - r - k) / (1 - k);
            double m = (1 - g - k) / (1 - k);
            double y = (1 - b - k) / (1 - k);

            return (c, m, y, k);
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

        /// <summary>
        /// Blends two colors together by a specified weighting factor.
        /// </summary>
        /// <param name="color">The foreground color to blend on top.</param>
        /// <param name="backColor">The background color to blend with.</param>
        /// <param name="amount">
        /// The blending factor in the range [0, 1].  
        /// A value of 0 returns <paramref name="backColor"/> only,  
        /// a value of 1 returns <paramref name="color"/> only,  
        /// and values in between return a linear interpolation of the two.
        /// </param>
        /// <returns>The resulting blended <see cref="Color"/>.</returns>
        public static Color Blend(this Color color, Color backColor, float amount)
        {
            // Clamp amount to [0, 1]
            if (amount < 0f) amount = 0f;
            else if (amount > 1f) amount = 1f;

            byte a = (byte)(color.A * amount + backColor.A * (1 - amount));
            byte r = (byte)(color.R * amount + backColor.R * (1 - amount));
            byte g = (byte)(color.G * amount + backColor.G * (1 - amount));
            byte b = (byte)(color.B * amount + backColor.B * (1 - amount));

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// Reverse Color From RGB To BGR or RGBA To ABGR (As used in some Windows Registry values)
        /// </summary>
        public static Color Reverse(this Color Color)
        {
            return Color.FromArgb(Color.A, Color.B, Color.G, Color.R);
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
        /// Convert color to grayscale.
        /// </summary>
        public static Color Grayscale(this Color color)
        {
            int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
            return Color.FromArgb(color.A, gray, gray, gray);
        }

        /// <summary>
        /// Convert color to sepia tone.
        /// </summary>
        public static Color Sepia(this Color color)
        {
            int tr = (int)(0.393 * color.R + 0.769 * color.G + 0.189 * color.B);
            int tg = (int)(0.349 * color.R + 0.686 * color.G + 0.168 * color.B);
            int tb = (int)(0.272 * color.R + 0.534 * color.G + 0.131 * color.B);

            tr = Math.Min(255, tr);
            tg = Math.Min(255, tg);
            tb = Math.Min(255, tb);

            return Color.FromArgb(color.A, tr, tg, tb);
        }

        /// <summary>
        /// Rotate hue of a color by a given degree (0-360).
        /// </summary>
        public static Color RotateHue(this Color color, float degrees)
        {
            float hue = color.GetHue();
            float sat = color.GetSaturation();
            float bri = color.GetBrightness();
            hue = (hue + degrees) % 360;
            return FromAhsb(color.A, hue, sat, bri);
        }

        /// <summary>
        /// Helper: Create color from Alpha, Hue, Saturation, Brightness
        /// </summary>
        private static Color FromAhsb(int a, float h, float s, float b)
        {
            if (s == 0) return Color.FromArgb(a, (int)(b * 255), (int)(b * 255), (int)(b * 255));

            float fMax, fMid, fMin;
            int iSextant;
            if (b > 0.5)
            {
                fMax = b - (b * s) + s;
                fMin = b + (b * s) - s;
            }
            else
            {
                fMax = b + (b * s);
                fMin = b - (b * s);
            }

            iSextant = (int)Math.Floor(h / 60f);
            if (h >= 300f) h -= 360f;
            h /= 60f;
            h -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (iSextant % 2 == 0)
                fMid = h * (fMax - fMin) + fMin;
            else
                fMid = fMin - h * (fMax - fMin);

            int iMax = (int)(fMax * 255);
            int iMid = (int)(fMid * 255);
            int iMin = (int)(fMin * 255);

            switch (iSextant)
            {
                case 1: return Color.FromArgb(a, iMid, iMax, iMin);
                case 2: return Color.FromArgb(a, iMin, iMax, iMid);
                case 3: return Color.FromArgb(a, iMin, iMid, iMax);
                case 4: return Color.FromArgb(a, iMid, iMin, iMax);
                case 5: return Color.FromArgb(a, iMax, iMin, iMid);
                default: return Color.FromArgb(a, iMax, iMid, iMin);
            }
        }

        /// <summary>
        /// Get complementary (opposite) color.
        /// </summary>
        public static Color Complementary(this Color color)
        {
            return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
        }

        /// <summary>
        /// Reduce saturation of color.
        /// </summary>
        public static Color Desaturate(this Color color, float factor)
        {
            float h = color.GetHue();
            float s = Math.Max(0, color.GetSaturation() - factor);
            float b = color.GetBrightness();
            return FromAhsb(color.A, h, s, b);
        }

        /// <summary>
        /// Convert color to monochrome (black or white).
        /// Threshold = 0.5 brightness by default.
        /// </summary>
        public static Color Monochrome(this Color color, double threshold = 0.5)
        {
            double brightness = color.GetBrightness();
            return brightness < threshold
                ? Color.FromArgb(color.A, 0, 0, 0)
                : Color.FromArgb(color.A, 255, 255, 255);
        }

        /// <summary>
        /// Get the triadic colors (3 colors spaced 120° apart on the hue wheel).
        /// Returns an array of 3 colors: [original, triad1, triad2].
        /// </summary>
        public static Color[] Triadic(this Color color)
        {
            float hue = color.GetHue();
            float sat = color.GetSaturation();
            float bri = color.GetBrightness();

            Color triad1 = FromAhsb(color.A, (hue + 120f) % 360f, sat, bri);
            Color triad2 = FromAhsb(color.A, (hue + 240f) % 360f, sat, bri);

            return [color, triad1, triad2];
        }

        /// <summary>
        /// Get the tetradic colors (4 colors forming a rectangle on the hue wheel).
        /// Returns an array of 4 colors: [original, complement, triad1, triad2].
        /// </summary>
        public static Color[] Tetradic(this Color color)
        {
            float hue = color.GetHue();
            float sat = color.GetSaturation();
            float bri = color.GetBrightness();

            Color complement = FromAhsb(color.A, (hue + 180f) % 360f, sat, bri);
            Color alt1 = FromAhsb(color.A, (hue + 90f) % 360f, sat, bri);
            Color alt2 = FromAhsb(color.A, (hue + 270f) % 360f, sat, bri);

            return new[] { color, complement, alt1, alt2 };
        }

        /// <summary>
        /// Get analogous colors (neighbors on the hue wheel).
        /// Returns an array of 3 colors: [previous, original, next].
        /// </summary>
        public static Color[] Analogous(this Color color, float angle = 30f)
        {
            float hue = color.GetHue();
            float sat = color.GetSaturation();
            float bri = color.GetBrightness();

            Color prev = FromAhsb(color.A, (hue - angle + 360f) % 360f, sat, bri);
            Color next = FromAhsb(color.A, (hue + angle) % 360f, sat, bri);

            return new[] { prev, color, next };
        }

        /// <summary>
        /// Convert color to 16-bit (RGB565).
        /// </summary>
        public static Color To16Bit(this Color color)
        {
            int r = (color.R >> 3) & 0x1F;  // 5 bits
            int g = (color.G >> 2) & 0x3F;  // 6 bits
            int b = (color.B >> 3) & 0x1F;  // 5 bits

            // Expand back to 8-bit
            int r8 = (r << 3) | (r >> 2);
            int g8 = (g << 2) | (g >> 4);
            int b8 = (b << 3) | (b >> 2);

            return Color.FromArgb(color.A, r8, g8, b8);
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to the closest matching color in the 256-color VGA palette.
        /// </summary>
        /// <remarks>This method calculates the closest color in the VGA 256-color palette by minimizing
        /// the Euclidean distance in the RGB color space. The alpha channel of the input color is not used in the
        /// distance calculation but is retained in the resulting color.</remarks>
        /// <param name="input">The input <see cref="Color"/> to be converted.</param>
        /// <returns>A <see cref="Color"/> representing the closest match to the input color in the 256-color VGA palette. The
        /// alpha channel of the input color is preserved in the returned color.</returns>
        public static Color To256Color(this Color input)
        {
            var palette = GenerateVGA256();
            Color best = palette[0];
            int bestDist = int.MaxValue;

            foreach (var c in palette)
            {
                int dr = input.R - c.R;
                int dg = input.G - c.G;
                int db = input.B - c.B;
                int dist = dr * dr + dg * dg + db * db;

                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = c;
                    if (dist == 0) break;
                }
            }
            return Color.FromArgb(input.A, best);
        }

        private static Color[] GenerateVGA256()
        {
            var palette = new List<Color>(256);

            Color[] vga16 =
            [
                Color.FromArgb(0,   0,   0),   // 0 = Black
                Color.FromArgb(0,   0, 170),   // 1 = Blue
                Color.FromArgb(0, 170,   0),   // 2 = Green
                Color.FromArgb(0, 170, 170),   // 3 = Cyan
                Color.FromArgb(170, 0,   0),   // 4 = Red
                Color.FromArgb(170, 0, 170),   // 5 = Magenta
                Color.FromArgb(170, 85,  0),   // 6 = Brown (Dark Yellow)
                Color.FromArgb(170,170,170),   // 7 = Light Gray
                Color.FromArgb(85,  85,  85),  // 8 = Dark Gray
                Color.FromArgb(85,  85, 255),  // 9 = Light Blue
                Color.FromArgb(85, 255,  85),  // 10 = Light Green
                Color.FromArgb(85, 255, 255),  // 11 = Light Cyan
                Color.FromArgb(255, 85,  85),  // 12 = Light Red
                Color.FromArgb(255, 85, 255),  // 13 = Light Magenta
                Color.FromArgb(255, 255, 85),  // 14 = Yellow
                Color.FromArgb(255, 255, 255)  // 15 = White
            ];

            palette.AddRange(vga16);

            // 2. 6x6x6 cube (216 colors, VGA mapping: 0–63 scale)
            int[] steps = { 0x00, 0x24, 0x49, 0x6D, 0x92, 0xB6, 0xDB, 0xFF };
            // note: VGA used 0–63 (6-bit DAC), scaled to 0–255 → these are multiples of 255/7 ≈ 36

            for (int r = 0; r < 6; r++)
                for (int g = 0; g < 6; g++)
                    for (int b = 0; b < 6; b++)
                    {
                        int rr = (int)(r * 255.0 / 5);
                        int gg = (int)(g * 255.0 / 5);
                        int bb = (int)(b * 255.0 / 5);
                        palette.Add(Color.FromArgb(rr, gg, bb));
                    }

            // 3. 24 grayscale
            for (int i = 0; i < 24; i++)
            {
                int v = (int)(i * 255.0 / 23);
                palette.Add(Color.FromArgb(v, v, v));
            }

            return [.. palette];
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
        /// HSL color structure with alpha support
        /// </summary>
        /// <remarks>
        /// Generate an HSL structure with optional alpha channel
        /// </remarks>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="l"></param>
        /// <param name="a"></param>
        public struct HSL(int h, float s, float l, float a = 1f)
        {
            /// <summary>
            /// Hue (0–360)
            /// </summary>
            public float H { get; set; } = h;

            /// <summary>
            /// Saturation (0–1)
            /// </summary>
            public float S { get; set; } = s;

            /// <summary>
            /// Lightness (0–1)
            /// </summary>
            public float L { get; set; } = l;

            /// <summary>
            /// Alpha (0–1, default 1 = fully opaque)
            /// </summary>
            public float A { get; set; } = a;

            /// <summary>
            /// Check if two HSL colors are equal (including alpha)
            /// </summary>
            public readonly bool Equals(HSL hsl)
            {
                return H == hsl.H && S == hsl.S && L == hsl.L && A == hsl.A;
            }

            /// <summary>
            /// Adds the corresponding components of two <see cref="HSL"/> color values.
            /// </summary>
            /// <remarks>
            /// This operator performs component-wise addition of two <see cref="HSL"/> color values.  
            /// The hue (H) component is adjusted to ensure it remains within the valid range of [0, 360)  
            /// by wrapping around if necessary. The saturation (S) and lightness (L) components are clamped  
            /// to the range [0, 1]. The alpha (A) component is preserved from <paramref name="hsl1"/>.  
            /// </remarks>
            /// <param name="hsl1">The first <see cref="HSL"/> color value.</param>
            /// <param name="hsl2">The second <see cref="HSL"/> color value.</param>
            /// <returns>A new <see cref="HSL"/> color where H, S, and L are the sum of the corresponding  
            /// components of <paramref name="hsl1"/> and <paramref name="hsl2"/> (clamped to valid ranges),  
            /// and A is preserved from <paramref name="hsl1"/>.</returns>
            public static HSL operator +(HSL hsl1, HSL hsl2)
            {
                float h = hsl1.H + hsl2.H;
                if (h >= 360f) h -= 360f;
                if (h < 0f) h += 360f;

                float s = hsl1.S + hsl2.S;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = hsl1.L + hsl2.L;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l, A = hsl1.A };
            }

            /// <summary>
            /// Subtracts the components of one <see cref="HSL"/> color from another.
            /// </summary>
            /// <remarks>
            /// This operator performs component-wise subtraction of the H, S, and L values of the two colors.  
            /// The hue (H) is adjusted to remain in the valid range of [0, 360), while saturation (S) and  
            /// lightness (L) are clamped to the range [0, 1]. The alpha (A) component is preserved from <paramref name="hsl1"/>.  
            /// </remarks>
            /// <param name="hsl1">The first <see cref="HSL"/> color to subtract from.</param>
            /// <param name="hsl2">The second <see cref="HSL"/> color to subtract.</param>
            /// <returns>A new <see cref="HSL"/> color where H, S, and L are the result of subtracting  
            /// <paramref name="hsl2"/> from <paramref name="hsl1"/> (clamped to valid ranges), and A is preserved from <paramref name="hsl1"/>.</returns>
            public static HSL operator -(HSL hsl1, HSL hsl2)
            {
                float h = hsl1.H - hsl2.H;
                if (h >= 360f) h -= 360f;
                if (h < 0f) h += 360f;

                float s = hsl1.S - hsl2.S;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = hsl1.L - hsl2.L;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l, A = hsl1.A };
            }

            /// <summary>
            /// Multiplies the corresponding components of two <see cref="HSL"/> color values.
            /// </summary>
            /// <remarks>
            /// This operator performs component-wise multiplication of the hue (H), saturation (S),  
            /// and lightness (L) values of the two colors.  
            /// <list type="bullet">
            ///   <item><description>Hue (H) is wrapped to stay in the valid range [0, 360).</description></item>
            ///   <item><description>Saturation (S) and Lightness (L) are clamped to the valid range [0, 1].</description></item>
            ///   <item><description>Alpha (A) is preserved from <paramref name="hsl1"/>.</description></item>
            /// </list>
            /// </remarks>
            /// <param name="hsl1">The first <see cref="HSL"/> color value.</param>
            /// <param name="hsl2">The second <see cref="HSL"/> color value.</param>
            /// <returns>A new <see cref="HSL"/> color where H, S, and L are the products of  
            /// <paramref name="hsl1"/> and <paramref name="hsl2"/> (clamped to valid ranges), and A is preserved from <paramref name="hsl1"/>.</returns>
            public static HSL operator *(HSL hsl1, HSL hsl2)
            {
                float h = hsl1.H * hsl2.H;
                if (h >= 360f) h %= 360f;
                if (h < 0f) h += 360f;

                float s = hsl1.S * hsl2.S;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = hsl1.L * hsl2.L;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l, A = hsl1.A };
            }

            /// <summary>
            /// Divides the components of one <see cref="HSL"/> color by another.
            /// </summary>
            /// <remarks>
            /// This operator performs component-wise division of the hue (H), saturation (S),  
            /// and lightness (L) values of <paramref name="hsl1"/> by <paramref name="hsl2"/>.  
            /// <list type="bullet">
            ///   <item><description>Hue (H) is wrapped to stay in the valid range [0, 360).</description></item>
            ///   <item><description>Saturation (S) and Lightness (L) are clamped to the valid range [0, 1].</description></item>
            ///   <item><description>Alpha (A) is preserved from <paramref name="hsl1"/>.</description></item>
            /// </list>
            /// Division by zero is handled by returning 0 for that component.
            /// </remarks>
            /// <param name="hsl1">The numerator <see cref="HSL"/> color.</param>
            /// <param name="hsl2">The denominator <see cref="HSL"/> color.</param>
            /// <returns>A new <see cref="HSL"/> color where H, S, and L are the quotient of  
            /// <paramref name="hsl1"/> divided by <paramref name="hsl2"/> (clamped to valid ranges),  
            /// and A is preserved from <paramref name="hsl1"/>.</returns>
            public static HSL operator /(HSL hsl1, HSL hsl2)
            {
                float h = hsl2.H == 0 ? 0 : hsl1.H / hsl2.H;
                if (h >= 360f) h %= 360f;
                if (h < 0f) h += 360f;

                float s = hsl2.S == 0 ? 0 : hsl1.S / hsl2.S;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = hsl2.L == 0 ? 0 : hsl1.L / hsl2.L;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l, A = hsl1.A };
            }

            /// <summary>
            /// Multiplies the hue, saturation, and lightness of an <see cref="HSL"/> color by a scalar.
            /// </summary>
            /// <remarks>
            /// <list type="bullet">
            ///   <item><description>Hue (H) is multiplied and wrapped into [0, 360).</description></item>
            ///   <item><description>Saturation (S) and Lightness (L) are multiplied and clamped into [0, 1].</description></item>
            ///   <item><description>Alpha (A) is preserved.</description></item>
            /// </list>
            /// </remarks>
            /// <param name="hsl">The <see cref="HSL"/> color.</param>
            /// <param name="scalar">The multiplier.</param>
            /// <returns>A new <see cref="HSL"/> color with scaled H, S, and L.</returns>
            public static HSL operator *(HSL hsl, float scalar)
            {
                float h = hsl.H * scalar;
                if (h >= 360f) h %= 360f;
                if (h < 0f) h += 360f;

                float s = hsl.S * scalar;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = hsl.L * scalar;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l, A = hsl.A };
            }

            /// <summary>
            /// Divides the hue, saturation, and lightness of an <see cref="HSL"/> color by a scalar.
            /// </summary>
            /// <remarks>
            /// <list type="bullet">
            ///   <item><description>Division by zero returns 0 for that component.</description></item>
            ///   <item><description>Hue (H) is divided and wrapped into [0, 360).</description></item>
            ///   <item><description>Saturation (S) and Lightness (L) are divided and clamped into [0, 1].</description></item>
            ///   <item><description>Alpha (A) is preserved.</description></item>
            /// </list>
            /// </remarks>
            /// <param name="hsl">The <see cref="HSL"/> color.</param>
            /// <param name="scalar">The divisor.</param>
            /// <returns>A new <see cref="HSL"/> color with scaled H, S, and L.</returns>
            public static HSL operator /(HSL hsl, float scalar)
            {
                float h = scalar == 0 ? 0 : hsl.H / scalar;
                if (h >= 360f) h %= 360f;
                if (h < 0f) h += 360f;

                float s = scalar == 0 ? 0 : hsl.S / scalar;
                s = s < 0f ? 0f : (s > 1f ? 1f : s);

                float l = scalar == 0 ? 0 : hsl.L / scalar;
                l = l < 0f ? 0f : (l > 1f ? 1f : l);

                return new() { H = h, S = s, L = l, A = hsl.A };
            }
        }
    }
}