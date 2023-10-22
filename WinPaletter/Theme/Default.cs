using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme
{
    public static partial class Default
    {
        /// <summary>
        /// Alias to Get() function
        /// </summary>
        /// <returns>Default Windows theme, based on current Windows edition</returns>
        public static Manager From()
        {
            return Get();
        }

        /// <summary>
        /// Alias to Get(WindowStyle) function
        /// </summary>
        /// <returns>Default Windows theme, based on your selection</returns>
        public static Manager From(WindowStyle PreviewStyle)
        {
            return Get(PreviewStyle);
        }

        /// <summary>
        /// Get default Windows theme, based on your selection
        /// </summary>
        /// <param name="PreviewStyle">Windows edition</param>
        /// <returns><code>WinPaletter.Theme.Manager</code></returns>
        public static Manager Get(WindowStyle PreviewStyle)
        {
            return PreviewStyle switch
            {
                WindowStyle.W11 => Windows11(),
                WindowStyle.W10 => Windows10(),
                WindowStyle.W81 => Windows81(),
                WindowStyle.W7 => Windows7(),
                WindowStyle.WVista => WindowsVista(),
                WindowStyle.WXP => WindowsXP(),
                _ => Windows11(),
            };
        }

        /// <summary>
        /// Get default Windows theme, based on current Windows edition
        /// </summary>
        /// <returns><code>WinPaletter.Theme.Manager</code></returns>
        public static Manager Get()
        {
            if (OS.W11 || OS.W12)
                return Windows11();

            else if (OS.W10)
                return Windows10();

            else if (OS.W81 || OS.W8)
                return Windows81();

            else if (OS.W7)
                return Windows7();

            else if (OS.WVista)
                return WindowsVista();

            else if (OS.WXP)
                return WindowsXP();

            else
                return Windows11();
        }
    }
}