using System;
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
                Scheme Main = new(DefaultColors.PrimaryColor, DefaultColors.BackColorDark, true);

                Scheme Secondary = new(DefaultColors.SecondaryColor, DefaultColors.BackColorDark, true);

                Scheme Tertiary = new(DefaultColors.TertiaryColor, DefaultColors.BackColorDark, true);

                Scheme Disabled = new(DefaultColors.DisabledColor_Dark, DefaultColors.DisabledBackColor_Dark, true);
            }

            public Scheme Main;

            public Scheme Secondary;

            public Scheme Tertiary;

            public Scheme Disabled;
        }

        public class Colors_Collection : IDisposable
        {
            public Color Accent;
            public Color AccentAlt;

            public Color Button;
            public Color Button_Over;
            public Color Button_Down;

            public Color Back;
            public Color Line;

            public Color Back_Hover;
            public Color Line_Hover;

            public Color Back_Max;
            public Color Line_Max;

            public Color Back_Checked;
            public Color Line_Checked;

            public Color Line_Checked_Hover;
            public Color Back_Checked_Hover;

            public Color ForeColor;
            public Color ForeColor_Accent;

            public Colors_Collection(Color accent, Color backcolor, bool isDark)
            {
                Accent = accent;

                if (isDark)
                {
                    AccentAlt = accent.Light(0.65f);                    //<----------------

                    Button = backcolor.CB(0.07f);
                    Button_Over = backcolor.CB(0.1f);
                    Button_Down = backcolor.CB(0.07f);

                    Back = backcolor.CB(0.07f);
                    Line = backcolor.CB(0.08f);

                    Back_Hover = backcolor.CB(0.13f);
                    Line_Hover = backcolor.CB(0.25f);

                    Back_Max = backcolor.CB(0.4f);                      //<----------------
                    Line_Max = backcolor.CB(0.45f);                     //<----------------

                    Back_Checked = accent.CB(-0.5f);
                    Line_Checked = accent.CB(-0.43f);

                    Back_Checked_Hover = accent.CB(-0.3f);
                    Line_Checked_Hover = accent.CB(-0.2f);

                    ForeColor = backcolor.LightLight();
                    ForeColor_Accent = accent.LightLight();
                }

                else
                {
                    AccentAlt = accent.Light(0.5f);                     //<----------------

                    Button = backcolor.CB(-0.04f);
                    Button_Over = backcolor.CB(-0.1f);
                    Button_Down = backcolor.CB(-0.04f);

                    Back = backcolor.CB(-0.15f);
                    Line = backcolor.CB(-0.18f);

                    Back_Hover = backcolor.CB(-0.03f);
                    Line_Hover = backcolor.CB(-0.15f);

                    Back_Max = backcolor.CB(-0.45f);                     //<----------------
                    Line_Max = backcolor.CB(-0.5f);                      //<----------------

                    Back_Checked = accent.CB(0.65f);
                    Line_Checked = accent.CB(0.62f);

                    Back_Checked_Hover = accent.CB(0.45f);
                    Line_Checked_Hover = accent.CB(0.4f);

                    ForeColor = backcolor.Dark(0.4f);
                    ForeColor_Accent = accent.Dark(0.5f);
                }
            }

            private bool disposedValue;

            /// <summary>
            /// Dispose WinPaletter theme to free up memory
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects).
                    }
                }
                disposedValue = true;
            }


            /// <summary>
            /// Dispose WinPaletter theme to free up memory
            /// </summary>
            public void Dispose()
            {
                // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
                Dispose(true);
                GC.SuppressFinalize(this);
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
            public Pen Line;

            public Pen Back_Hover;
            public Pen Line_Hover;

            public Pen Back_Max;
            public Pen Line_Max;

            public Pen Back_Checked;
            public Pen Line_Checked;

            public Pen Back_Checked_Hover;
            public Pen Line_Checked_Hover;

            public Pen ForeColor;
            public Pen ForeColor_Accent;

            public Pens_Collection(Scheme scheme)
            {
                Accent = new(scheme.Colors.Accent);
                AccentAlt = new(scheme.Colors.AccentAlt);

                Button = new(scheme.Colors.Button);
                Button_Over = new(scheme.Colors.Button_Over);
                Button_Down = new(scheme.Colors.Button_Down);

                Back = new(scheme.Colors.Back);
                Line = new(scheme.Colors.Line);

                Back_Hover = new(scheme.Colors.Back_Hover);
                Line_Hover = new(scheme.Colors.Line_Hover);

                Back_Max = new(scheme.Colors.Back_Max);
                Line_Max = new(scheme.Colors.Line_Max);

                Back_Checked = new(scheme.Colors.Back_Checked);
                Line_Checked = new(scheme.Colors.Line_Checked);

                Back_Checked_Hover = new(scheme.Colors.Back_Checked_Hover);
                Line_Checked_Hover = new(scheme.Colors.Line_Checked_Hover);

                ForeColor = new(scheme.Colors.ForeColor);
                ForeColor_Accent = new(scheme.Colors.ForeColor_Accent);
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
            public SolidBrush Line;

            public SolidBrush Back_Hover;
            public SolidBrush Line_Hover;

            public SolidBrush Back_Max;
            public SolidBrush Line_Max;

            public SolidBrush Back_Checked;
            public SolidBrush Line_Checked;

            public SolidBrush Back_Checked_Hover;
            public SolidBrush Line_Checked_Hover;

            public SolidBrush ForeColor;
            public SolidBrush ForeColor_Accent;

            public Brushes_Collection(Scheme scheme)
            {
                Accent = new(scheme.Colors.Accent);
                AccentAlt = new(scheme.Colors.AccentAlt);

                Button = new(scheme.Colors.Button);
                Button_Over = new(scheme.Colors.Button_Over);
                Button_Down = new(scheme.Colors.Button_Down);

                Back = new(scheme.Colors.Back);
                Line = new(scheme.Colors.Line);

                Back_Hover = new(scheme.Colors.Back_Hover);
                Line_Hover = new(scheme.Colors.Line_Hover);

                Back_Max = new(scheme.Colors.Back_Max);
                Line_Max = new(scheme.Colors.Line_Max);

                Back_Checked = new(scheme.Colors.Back_Checked);
                Line_Checked = new(scheme.Colors.Line_Checked);

                Back_Checked_Hover = new(scheme.Colors.Back_Checked_Hover);
                Line_Checked_Hover = new(scheme.Colors.Line_Checked_Hover);

                ForeColor = new(scheme.Colors.ForeColor);
                ForeColor_Accent = new(scheme.Colors.ForeColor_Accent);
            }
        }
    }
}