using System.Drawing;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Global clipboard for color
    /// </summary>
    internal class ColorClipboard
    {
        /// <summary>
        /// Context menu events enumeration
        /// </summary>
        public enum MenuEvent
        {
            None,
            Copy,
            Cut,
            Paste,
            Override,
            Delete
        }

        /// <summary>
        /// Copied color into clipboard
        /// </summary>
        public static Color CopiedColor = default;

        /// <summary>
        /// Event that was triggered by context menu
        /// </summary>
        public static MenuEvent Event = MenuEvent.None;
    }
}
