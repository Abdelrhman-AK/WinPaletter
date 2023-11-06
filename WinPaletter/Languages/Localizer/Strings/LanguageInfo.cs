using System;

namespace WinPaletter
{
    public partial class Localizer
    {
        public string Name { get; set; } = Environment.UserName;
        public string TranslationVersion { get; set; } = "1.0";
        public string Lang { get; set; } = "English";
        public string LangCode { get; set; } = "EN-US";
        public string AppVer { get; set; } = Program.Version;
        public bool RightToLeft { get; set; } = false;
    }
}