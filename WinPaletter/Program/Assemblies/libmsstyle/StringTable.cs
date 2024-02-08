using System;
using System.Collections.Generic;

namespace libmsstyle
{
    public class StringTable
    {
        // Find entries that look like a font. That is, they are of format: "Name, Size, [Style], [Quality]"
        public static Dictionary<int, string> FilterFonts(Dictionary<int, string> table)
        {
            Dictionary<int, string> fonts = new Dictionary<int, string>();
            foreach (KeyValuePair<int, string> entry in table)
            {
                // Need at least "Name, Size"
                string[] elem = entry.Value.Split(new char[] { ',' });
                if (elem.Length < 2)
                {
                    continue;
                }

                // Need a valid "Size"
                int fontSize;
                if (!Int32.TryParse(elem[1], out fontSize))
                {
                    continue;
                }

                // "Style" and "Quality" indicates a font
                string lower = entry.Value.ToLower();
                if (lower.Contains("bold") ||
                   lower.Contains("italic") ||
                   lower.Contains("underline") ||
                   lower.Contains("quality:"))
                {
                    fonts.Add(entry.Key, entry.Value);
                    continue;
                }

                // Without "Style" and "Quality", it must be just "Name, Size"
                if (elem.Length == 2)
                {
                    fonts.Add(entry.Key, entry.Value);
                    continue;
                }
            }

            return fonts;
        }
    }
}
