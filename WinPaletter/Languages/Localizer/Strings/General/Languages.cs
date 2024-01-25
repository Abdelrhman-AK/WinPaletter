namespace WinPaletter
{
    public partial class Localizer
    {
        public string Lang_HasLeftToRight { get; set; } = "It has left-to-right layout";
        public string Lang_HasRightToLeft { get; set; } = "It has right-to-left layout";
        public string Lang_ChooseAForm { get; set; } = "Choose a form then open it. When you finish translation, close the child form below.";
        public string Lang_LoadingChildrenForms { get; set; } = "Loading GUI of all WinPaletter forms into your memory ({0}%). Be cautious as this will extensively increase WinPaletter memory usage.";
        public string LngExported { get; set; } = "Language exported successfully";
        public string LangSaved { get; set; } = "Language file has been saved successfully";
        public string LngShouldClose { get; set; } = "You should close the app if you want to export language.";
        public string LanguageRestart { get; set; } = "To apply this language, save settings and restart WinPaletter.";
    }
}