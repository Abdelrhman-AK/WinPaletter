using System.Drawing;

namespace WinPaletter.UI.Style
{
    public partial class Config
    {
        public struct Colors_Structure
        {
            public Color BaseColor;
            public Color Border;
            public Color Border_Checked;
            public Color Border_Checked_Hover;

            public Color Back;
            public Color Back_Checked;
            public Color Core;
            public Color Back_Hover;              // '''''''''''''''''''''''''''''''''''''
            public Color Border_Hover;            // '''''''''''''''''''''''''''''''''''''

            public Color NotTranslatedColor;
            public Color SearchColor;
        }
    }
}
