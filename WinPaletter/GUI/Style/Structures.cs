using System.Drawing;

namespace WinPaletter.UI.Style
{
    public partial class Config
    {
        public struct Scheme
        {
            public Colors_Collection Colors;
            public Pens_Collection Pens;
            public Brushes_Collection Brushes;

            public Scheme(Color accent, Color backcolor, bool isDark)
            {
                Colors = new(accent, backcolor, isDark);
                Pens = new(this);
                Brushes = new(this);
            }
        }

        public class Schemes_Collection
        {
            public Schemes_Collection()
            {
                Scheme Main = new(DefaultColors.Accent, DefaultColors.BackColorDark, true);

                Scheme Secondary = new(DefaultColors.Secondary, DefaultColors.BackColorDark, true);

                Scheme Tertiary = new(DefaultColors.Tertiary, DefaultColors.BackColorDark, true);

                Scheme Disabled = new(DefaultColors.Disabled, DefaultColors.DisabledBackColor, true);
            }

            public Scheme Main;

            public Scheme Secondary;

            public Scheme Tertiary;

            public Scheme Disabled;
        }

        public class Colors_Collection
        {
            public Color Accent;
            public Color AccentAlt;

            public Color Button;
            public Color Button_Over;
            public Color Button_Down;

            public Color Back;
            public Color Back_Hover;
            public Color Back_Checked;
            public Color Back_Checked_Hover;
            public Color Back_Max;

            public Color Line;
            public Color Line_Hover;
            public Color Line_Checked;
            public Color Line_CheckedHover;

            public Colors_Collection(Color accent, Color backcolor, bool isDark)
            {
                Accent = accent;

                if (isDark)
                {
                    AccentAlt = accent.Light(0.65f);

                    Button = backcolor.CB(0.08f);
                    Button_Over = backcolor.CB(0.1f);
                    Button_Down = backcolor.CB(0.07f);

                    Back = backcolor.CB(0.08f);
                    Back_Hover = backcolor.CB(0.2f);
                    Back_Checked = accent.Dark(0.3f);
                    Back_Checked_Hover = accent.Dark(0.1f);

                    Back_Max = backcolor.CB(0.5f);

                    Line = backcolor.CB(0.1f);

                    Line_Hover = accent.CB(0.1f);
                    Line_Checked = accent.CB(-0.2f);
                    Line_CheckedHover = accent.CB(0.08f);
                }
                else
                {
                    AccentAlt = accent.Light(0.5f);

                    Button = backcolor.CB(-0.05f);
                    Button_Over = backcolor.CB(-0.1f);
                    Button_Down = backcolor.CB(-0.04f);

                    Back = backcolor.CB(-0.15f);
                    Back_Hover = backcolor.CB(-0.2f);
                    Back_Checked = accent.CB(0.6f);
                    Back_Checked_Hover = accent.CB(0.3f);

                    Back_Max = backcolor.CB(-0.4f);

                    Line = backcolor.CB(-0.05f);

                    Line_Hover = accent.CB(-0.1f);
                    Line_Checked = accent.CB(0.5f);
                    Line_CheckedHover = accent.CB(0.3f);
                }
            }
        }

        public class Pens_Collection
        {
            public Pen Accent;
            public Pen AccentAlt;

            public Pen Button;
            public Pen Button_Over;
            public Pen Button_Down;

            public Pen Back;
            public Pen Back_Hover;
            public Pen Back_Checked;
            public Pen Back_Checked_Hover;
            public Pen Back_Max;

            public Pen Line;
            public Pen Line_Hover;
            public Pen Line_Checked;
            public Pen Line_CheckedHover;

            public Pens_Collection(Scheme scheme)
            {
                Accent = new(scheme.Colors.Accent);
                AccentAlt = new(scheme.Colors.AccentAlt);
                Button = new(scheme.Colors.Button);
                Button_Over = new(scheme.Colors.Button_Over);
                Button_Down = new(scheme.Colors.Button_Down);
                Back = new(scheme.Colors.Back);
                Back_Hover = new(scheme.Colors.Back_Hover);
                Back_Checked = new(scheme.Colors.Back_Checked);
                Back_Checked_Hover = new(scheme.Colors.Back_Checked_Hover);
                Back_Max = new(scheme.Colors.Back_Max);
                Line = new(scheme.Colors.Line);
                Line_Hover = new(scheme.Colors.Line_Hover);
                Line_Checked = new(scheme.Colors.Line_Checked);
                Line_CheckedHover = new(scheme.Colors.Line_CheckedHover);
            }
        }

        public class Brushes_Collection
        {
            public SolidBrush Accent;
            public SolidBrush AccentAlt;

            public SolidBrush Button;
            public SolidBrush Button_Over;
            public SolidBrush Button_Down;

            public SolidBrush Back;
            public SolidBrush Back_Hover;
            public SolidBrush Back_Checked;
            public SolidBrush Back_Checked_Hover;
            public SolidBrush Back_Max;

            public SolidBrush Line;
            public SolidBrush Line_Hover;
            public SolidBrush Line_Checked;
            public SolidBrush Line_CheckedHover;

            public Brushes_Collection(Scheme scheme)
            {
                Accent = new(scheme.Colors.Accent);
                AccentAlt = new(scheme.Colors.AccentAlt);
                Button = new(scheme.Colors.Button);
                Button_Over = new(scheme.Colors.Button_Over);
                Button_Down = new(scheme.Colors.Button_Down);
                Back = new(scheme.Colors.Back);
                Back_Hover = new(scheme.Colors.Back_Hover);
                Back_Checked = new(scheme.Colors.Back_Checked);
                Back_Checked_Hover = new(scheme.Colors.Back_Checked_Hover);
                Back_Max = new(scheme.Colors.Back_Max);
                Line = new(scheme.Colors.Line);
                Line_Hover = new(scheme.Colors.Line_Hover);
                Line_Checked = new(scheme.Colors.Line_Checked);
                Line_CheckedHover = new(scheme.Colors.Line_CheckedHover);
            }
        }

    }
}
