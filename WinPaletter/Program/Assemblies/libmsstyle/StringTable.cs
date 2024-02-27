using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace libmsstyle
{
    public class StringTable
    {
        // Find entries that look like a font. That is, they are of format: "Name, Size, [Style], [Quality]"
        public static Dictionary<int, string> FilterFonts(Dictionary<int, string> table)
        {
            var fonts = new Dictionary<int, string>();
            foreach(var entry in table)
            {
                // Need at least "Name, Size"
                var elem = entry.Value.Split(new char[] { ',' });
                if (elem.Length < 2)
                {
                    continue;
                }

                // Need a valid "Size"
                int fontSize;
                if(!Int32.TryParse(elem[1], out fontSize))
                {
                    continue;
                }

                // "Style" and "Quality" indicates a font
                var lower = entry.Value.ToLower();
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
