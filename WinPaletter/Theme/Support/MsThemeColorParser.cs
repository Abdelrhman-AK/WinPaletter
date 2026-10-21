using System;
using System.Collections.Generic;
using System.Linq;

namespace WinPaletter.Theme
{
    internal static class MsThemeColorParser
    {
        internal static IEnumerable<string> ExtractRgbValues(string text)
        {
            foreach (string line in text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
            {
                string[] fields = line.Split('=');
                if (fields.Length < 2) continue;

                string color = fields[1];
                string[] components = color.Split(' ');
                if (!color.Contains(" ") || components.Length != 3) continue;
                if (components.Any(component => !component.All(char.IsDigit))) continue;

                yield return color;
            }
        }
    }
}
