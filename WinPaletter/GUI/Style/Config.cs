using System.Drawing;

namespace WinPaletter.UI.Style
{
    public partial class Config
    {
        public Config(Color BaseColor, Color BackColor, bool Dark, bool Rounded)
        {
            Colors = new Colors_Structure()
            {
                BaseColor = DefaultColors.Accent,
                Core = Colors.BaseColor.LightLight(),
                Back = Color.FromArgb(40, 40, 40),
                Back_Hover = Color.FromArgb(55, 55, 55),
                Back_Checked = Colors.BaseColor.Dark(0.2f),
                Border = Color.FromArgb(55, 55, 55),
                Border_Hover = Color.FromArgb(65, 65, 65),
                Border_Checked = Colors.BaseColor.CB(0.08f),
                Border_Checked_Hover = Colors.BaseColor.CB((float)-0.2d)
            };
            DarkMode = Dark;
            RoundedCorners = Rounded;
            Radius = 5;

            Colors.BaseColor = BaseColor;
            Colors.NotTranslatedColor = DarkMode ? Color.FromArgb(125, 20, 30) : Color.FromArgb(255, 136, 127);
            Colors.SearchColor = DarkMode ? Color.FromArgb(4, 94, 53) : Color.FromArgb(255, 255, 163);

            if (DarkMode)
            {
                Colors.Core = Colors.BaseColor.LightLight();

                Colors.Back = BackColor.CB(0.08f);
                Colors.Back_Hover = BackColor.CB(0.2f);

                Colors.Back_Checked = Colors.BaseColor.Dark(0.3f);

                Colors.Border = BackColor.CB(0.05f);
                Colors.Border_Hover = BackColor.CB(0.1f);

                Colors.Border_Checked = Colors.BaseColor.CB((float)-0.2d);
                Colors.Border_Checked_Hover = Colors.BaseColor.CB(0.08f);
            }

            else
            {
                Colors.Core = Colors.BaseColor.Light(0.5f);

                Colors.Back = BackColor.CB((float)-0.15d);
                Colors.Back_Hover = BackColor.CB((float)-0.2d);

                Colors.Back_Checked = Colors.BaseColor.CB(0.6f);

                Colors.Border = BackColor.CB((float)-0.05d);
                Colors.Border_Hover = BackColor.CB((float)-0.1d);

                Colors.Border_Checked = Colors.BaseColor.CB(0.5f);
                Colors.Border_Checked_Hover = Colors.BaseColor.CB(0.3f);
            }

            if (DarkMode)
            {
                Disabled_Colors.Back_Checked = Color.FromArgb(80, 80, 80);
                Disabled_Colors.Core = Color.FromArgb(90, 90, 90);
                Disabled_Colors.Border_Checked_Hover = Color.FromArgb(80, 80, 80);
                Disabled_Colors.Border = Color.FromArgb(90, 90, 90);
                Disabled_Colors.Back = Color.FromArgb(80, 80, 80);
            }
            else
            {
                Disabled_Colors.Back_Checked = Color.FromArgb(180, 180, 180);
                Disabled_Colors.Core = Color.FromArgb(190, 190, 190);
                Disabled_Colors.Border_Checked_Hover = Color.FromArgb(180, 180, 180);
                Disabled_Colors.Border = Color.FromArgb(190, 190, 190);
                Disabled_Colors.Back = Color.FromArgb(180, 180, 180);
            }

        }
    }
}