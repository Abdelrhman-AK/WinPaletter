using System.Collections.Generic;
using System.Drawing;

namespace WinPaletter
{
    public class RGBColorComparer : IComparer<Color>
    {
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
