using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace AnimatorNS
{
    /// <summary>
    /// PointFConverter
    /// Thanks for Jay Riggs
    /// </summary>
    public class PointFConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                float x = 0, y = 0;
                int comma = s.IndexOf(',');
                if (comma > -1)
                {
                    // Fast substring and parse, avoid Split and extra allocations
                    string sx = s.Substring(0, comma).Trim('{', 'X', 'x', '=', ' ', '\t');
                    string sy = s.Substring(comma + 1).Trim('}', 'Y', 'y', '=', ' ', '\t');
                    if (!float.TryParse(sx, NumberStyles.Float, culture, out x)) x = 0;
                    if (!float.TryParse(sy, NumberStyles.Float, culture, out y)) y = 0;
                }
                else
                {
                    s = s.Trim();
                    if (!float.TryParse(s, NumberStyles.Float, culture, out x)) x = 0;
                    y = 0;
                }
                return new PointF(x, y);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is PointF pt)
            {
                // Use InvariantCulture for consistent formatting
                return string.Format(CultureInfo.InvariantCulture, "{{X={0}, Y={1}}}", pt.X, pt.Y);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}