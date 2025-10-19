using System.Collections.Generic;
using System.Drawing;

namespace WinPaletter
{
    /// <summary>
    /// Provides a comparison mechanism for <see cref="Color"/> objects based on perceptual color properties.
    /// </summary>
    /// <remarks>This comparer sorts <see cref="Color"/> objects by their hue, perceived luminance,
    /// saturation, and finally a weighted RGB value to ensure deterministic ordering. The comparison prioritizes
    /// perceptual color differences, making it suitable for scenarios where human visual perception of color order is
    /// important.</remarks>
    public class RGBColorComparer : IComparer<Color>
    {
        public int Compare(Color a, Color b)
        {
            // Quick equality check to avoid unnecessary computation
            if (a.ToArgb() == b.ToArgb())
                return 0;

            // First compare by hue (perceptual color wheel order)
            float hueA = a.GetHue();
            float hueB = b.GetHue();
            int hueComparison = hueA.CompareTo(hueB);
            if (hueComparison != 0)
                return hueComparison;

            // Then compare by perceived luminance (weighted RGB)
            double lumA = GetPerceivedLuminance(a);
            double lumB = GetPerceivedLuminance(b);
            int lumComparison = lumA.CompareTo(lumB);
            if (lumComparison != 0)
                return lumComparison;

            // Then compare by saturation (color vividness)
            float satA = a.GetSaturation();
            float satB = b.GetSaturation();
            int satComparison = satA.CompareTo(satB);
            if (satComparison != 0)
                return satComparison;

            // Finally compare by weighted RGB values to ensure deterministic sorting
            int rgbA = WeightedRGBValue(a);
            int rgbB = WeightedRGBValue(b);
            return rgbA.CompareTo(rgbB);
        }

        /// <summary>
        /// Calculates perceived luminance using Rec. 709 weights (closer to human vision).
        /// </summary>
        private static double GetPerceivedLuminance(Color c)
        {
            return 0.2126 * c.R + 0.7152 * c.G + 0.0722 * c.B;
        }

        /// <summary>
        /// Creates a weighted integer representation of RGB to stabilize comparisons.
        /// </summary>
        private static int WeightedRGBValue(Color c)
        {
            return (c.R << 16) | (c.G << 8) | c.B;
        }
    }
}
