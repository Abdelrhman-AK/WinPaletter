using System.Drawing;

namespace WinPaletter.GlobalVariables
{
    internal class ColorClipboard
    {
        public enum MenuEvent
        {
            None,
            Copy,
            Cut,
            Paste,
            Override,
            Delete
        }
        public static Color CopiedColor = default;
        public static MenuEvent Event = MenuEvent.None;
    }
}
