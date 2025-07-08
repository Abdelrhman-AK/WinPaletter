using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme
{
    /// <summary>
    /// A class that contains default Windows themes
    /// </summary>
    public static partial class Default
    {
        /// <summary>
        /// Get default Windows theme, based on your selection
        /// </summary>
        /// <param name="PreviewStyle">Windows edition</param>
        /// <returns><see cref="Manager"/></returns>
        public static Manager Get(WindowStyle PreviewStyle)
        {
            return PreviewStyle switch
            {
                WindowStyle.W12 => Windows12(),
                WindowStyle.W11 => Windows11(),
                WindowStyle.W10 => Windows10(),
                WindowStyle.W81 => Windows81(),
                WindowStyle.W8 => Windows8(),
                WindowStyle.W7 => Windows7(),
                WindowStyle.WVista => WindowsVista(),
                WindowStyle.WXP => WindowsXP(),
                _ => Windows12(),
            };
        }

        /// <summary>
        /// Get default Windows theme, based on current Windows edition
        /// </summary>
        /// <returns><see cref="Manager"/></returns>
        public static Manager Get()
        {
            if (OS.W12)
                return Windows12();

            if (OS.W11)
                return Windows11();

            else if (OS.W10)
                return Windows10();

            else if (OS.W81)
                return Windows81();

            else if (OS.W8)
                return Windows8();

            else if (OS.W7)
                return Windows7();

            else if (OS.WVista)
                return WindowsVista();

            else if (OS.WXP)
                return WindowsXP();

            else
                return Windows12();
        }
    }
}