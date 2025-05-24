using System.Collections.Generic;
using System.Drawing;

namespace WinPaletter
{
    /// <summary>
    /// Compares two colors based on their RGB values. Useful for palette generation.
    /// </summary>
    public class RGBColorComparer : IComparer<Color>
    {
        /// <summary>
        /// Compares two colors based on their RGB values. Useful for palette generation.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Compare(Color a, Color b)
        {
            // Compare hue values
            int hueComparison = a.GetHue().CompareTo(b.GetHue());
            if (hueComparison != 0)
                return hueComparison;

            // Compare brightness values
            int brightnessComparison = a.GetBrightness().CompareTo(b.GetBrightness());
            if (brightnessComparison != 0)
                return brightnessComparison;

            // Compare saturation values
            int saturationComparison = a.GetSaturation().CompareTo(b.GetSaturation());
            return saturationComparison;
        }
    }

}
