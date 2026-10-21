using WinPaletter.Theme;

const string rgb = "12 34 56";
foreach (string newline in new[] { "\r", "\r\n", "\n" })
{
    string theme = $"[Theme]{newline}[Control Panel\\Colors]{newline}Window={rgb}{newline}";
    string[] colors = MsThemeColorParser.ExtractRgbValues(theme).ToArray();
    if (!colors.SequenceEqual(new[] { rgb }))
    {
        Console.Error.WriteLine($"FAIL for {Escape(newline)}: expected one color, found {colors.Length}");
        return 1;
    }
}

Console.WriteLine("PASS: RGB colors are extracted across CR, CRLF, and LF theme line endings.");
return 0;

static string Escape(string value) => value.Replace("\r", "\\r").Replace("\n", "\\n");
