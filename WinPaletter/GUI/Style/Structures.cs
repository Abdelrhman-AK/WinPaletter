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
                Scheme Main = new(DefaultColors.PrimaryColor_Dark, DefaultColors.BackColor_Dark, true);

                Scheme Secondary = new(DefaultColors.SecondaryColor_Dark, DefaultColors.BackColor_Dark, true);

                Scheme Tertiary = new(DefaultColors.TertiaryColor_Dark, DefaultColors.BackColor_Dark, true);

                Scheme Disabled = new(DefaultColors.DisabledColor_Dark, DefaultColors.DisabledBackColor_Dark, true);
            }

            public Scheme Main;

            public Scheme Secondary;

            public Scheme Tertiary;

            public Scheme Disabled;
        }

        public class Colors_Collection : IDisposable, ICloneable
        {
            private float incrementFactor = 0.01f;

            public bool DarkMode { get; set; } = true;

            public Color Accent { get; set; } = DefaultColors.PrimaryColor_Dark;
            private Method Accent_Method;
            private float Accent_Factor;

            public Color AccentAlt { get; set; } = DefaultColors.PrimaryColor_Dark.Light(0.5f);
            private Method AccentAlt_Method;
            private float AccentAlt_Factor;

            public Color BackColor { get; set; } = DefaultColors.BackColor_Dark;
            private Method BackColor_Method;
            private float BackColor_Factor;

            public Color Button { get; set; } = DefaultColors.BackColor_Dark.CB(0.07f);
            private Method Button_Method; // Use Color.CB() instead of Color.Light() or Color.Dark()
            private float Button_Factor; // Factor for colors changing voids

            public Color Button_Over { get; set; } = DefaultColors.BackColor_Dark.CB(0.1f);
            private Method Button_Over_Method; // Use Color.CB() instead of Color.Light() or Color.Dark()
            private float Button_Over_Factor; // Factor for colors changing voids

            public Color Button_Down { get; set; } = DefaultColors.BackColor_Dark.CB(0.07f);
            private Method Button_Down_Method; // Use Color.CB() instead of Color.Light() or Color.Dark()
            private float Button_Down_Factor; // Factor for colors changing voids

            public Color Back(int level = 0)
            {
                float factor = incrementFactor * (float)level * (DarkMode ? -1 : +1);
                return ApplyMethod(BackColor, Back_Method, Back_Factor + factor);
            }

            private Method Back_Method;
            private float Back_Factor;

            public Color Line (int level = 0)
            {
                float factor = incrementFactor * (float)level * (DarkMode ? -1 : +1);
                return ApplyMethod(BackColor, Line_Method, Line_Factor + factor);
            }

            private Method Line_Method;
            private float Line_Factor;

            public Color Back_Hover(int level = 0)
            {
                float factor = incrementFactor * (float)level * (DarkMode ? -1 : +1);
                return ApplyMethod(BackColor, Back_Hover_Method, Back_Hover_Factor + factor);
            }

            private Method Back_Hover_Method;
            private float Back_Hover_Factor;

            public Color Line_Hover(int level = 0)
            {
                float factor = incrementFactor * (float)level * (DarkMode ? -1 : +1);
                return ApplyMethod(BackColor, Line_Hover_Method, Line_Hover_Factor + factor);
            }

            private Method Line_Hover_Method;
            private float Line_Hover_Factor;

            public Color Back_Checked { get; set; }
            private Method Back_Checked_Method;
            private float Back_Checked_Factor;

            public Color Line_Checked { get; set; }
            private Method Line_Checked_Method;
            private float Line_Checked_Factor;

            public Color Back_Checked_Hover { get; set; }
            private Method Back_Checked_Hover_Method;
            private float Back_Checked_Hover_Factor;

            public Color Line_Checked_Hover { get; set; }
            private Method Line_Checked_Hover_Method;
            private float Line_Checked_Hover_Factor;

            public Color ForeColor { get; set; }
            private Method ForeColor_Method;
            private float ForeColor_Factor;

            public Color ForeColor_Accent { get; set; }
            private Method ForeColor_Accent_Method;
            private float ForeColor_Accent_Factor;

            public Colors_Collection(Color accent, Color backcolor, bool isDark)
            {
                Accent = accent;
                BackColor = backcolor;

                if (isDark)
                {
                    Accent_Method = Method.CB;
                    Accent_Factor = 0f;
                    AccentAlt_Method = Method.Light;
                    AccentAlt_Factor = 0.6f;
                    BackColor_Method = Method.CB;
                    BackColor_Factor = 0f;
                    Button_Method = Method.Light;
                    Button_Factor = 0.15f;
                    Button_Over_Method = Method.Light;
                    Button_Over_Factor = 0.25f;
                    Button_Down_Method = Method.Light;
                    Button_Down_Factor = 0.18f;
                    Back_Method = Method.Light;
                    Back_Factor = 0.15f;
                    Line_Method = Method.Light;
                    Line_Factor = 0.15f;
                    Back_Hover_Method = Method.Light;
                    Back_Hover_Factor = 0.22f;
                    Line_Hover_Method = Method.Light;
                    Line_Hover_Factor = 0.35f;
                    Back_Checked_Method = Method.Dark;
                    Back_Checked_Factor = 0.25f;
                    Line_Checked_Method = Method.Dark;
                    Line_Checked_Factor = 0.15f;
                    Back_Checked_Hover_Method = Method.Dark;
                    Back_Checked_Hover_Factor = 0f;
                    Line_Checked_Hover_Method = Method.Dark;
                    Line_Checked_Hover_Factor = -0.25f;
                    ForeColor_Method = Method.Light;
                    ForeColor_Factor = 0.85f;
                    ForeColor_Accent_Method = Method.Light;
                    ForeColor_Accent_Factor = 0.6f;
                }
                else
                {
                    Accent_Method = Method.CB;
                    Accent_Factor = 0f;
                    AccentAlt_Method = Method.Light;
                    AccentAlt_Factor = 0.75f;
                    BackColor_Method = Method.CB;
                    BackColor_Factor = 0f;
                    Button_Method = Method.Light;
                    Button_Factor = -0.24f;
                    Button_Over_Method = Method.Light;
                    Button_Over_Factor = -0.74f;
                    Button_Down_Method = Method.Light;
                    Button_Down_Factor = -0.37f;
                    Back_Method = Method.Dark;
                    Back_Factor = -0.47f;
                    Line_Method = Method.Dark;
                    Line_Factor = -0.36f;
                    Back_Hover_Method = Method.CB;
                    Back_Hover_Factor = -0.05f;
                    Line_Hover_Method = Method.CB;
                    Line_Hover_Factor = -0.04f;
                    Back_Checked_Method = Method.CB;
                    Back_Checked_Factor = 0.76f;
                    Line_Checked_Method = Method.CB;
                    Line_Checked_Factor = 0.72f;
                    Back_Checked_Hover_Method = Method.CB;
                    Back_Checked_Hover_Factor = 0.63f;
                    Line_Checked_Hover_Method = Method.CB;
                    Line_Checked_Hover_Factor = 0.58f;
                    ForeColor_Method = Method.Dark;
                    ForeColor_Factor = 0.6f;
                    ForeColor_Accent_Method = Method.Dark;
                    ForeColor_Accent_Factor = 0.09f;
                }

                AccentAlt = ApplyMethod(accent, AccentAlt_Method, AccentAlt_Factor);

                Button = ApplyMethod(backcolor, Button_Method, Button_Factor);
                Button_Over = ApplyMethod(backcolor, Button_Over_Method, Button_Over_Factor);
                Button_Down = ApplyMethod(backcolor, Button_Down_Method, Button_Down_Factor);
                Back_Checked = ApplyMethod(accent, Back_Checked_Method, Back_Checked_Factor);
                Line_Checked = ApplyMethod(accent, Line_Checked_Method, Line_Checked_Factor);
                Back_Checked_Hover = ApplyMethod(accent, Back_Checked_Hover_Method, Back_Checked_Hover_Factor);
                Line_Checked_Hover = ApplyMethod(accent, Line_Checked_Hover_Method, Line_Checked_Hover_Factor);

                ForeColor = ApplyMethod(backcolor, ForeColor_Method, ForeColor_Factor);
                ForeColor_Accent = ApplyMethod(accent, ForeColor_Accent_Method, ForeColor_Accent_Factor);
            }

            public enum Method
            {
                CB,
                Light,
                Dark
            }

            Color ApplyMethod(Color color, Method method, float factor)
            {
                switch (method)
                {
                    case Method.CB:
                        return color.CB(factor);
                    case Method.Light:
                        return color.Light(factor);
                    case Method.Dark:
                        return color.Dark(factor);
                    default:
                        return color.CB(factor);
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

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        public class Pens_Collection
        {
            public Pen Accent;
            public Pen AccentAlt;
            public Pen BackColor;

            public Pen Button;
            public Pen Button_Over;
            public Pen Button_Down;

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
                BackColor = new(scheme.Colors.BackColor);

                Button = new(scheme.Colors.Button);
                Button_Over = new(scheme.Colors.Button_Over);
                Button_Down = new(scheme.Colors.Button_Down);

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
            public SolidBrush BackColor;

            public SolidBrush Button;
            public SolidBrush Button_Over;
            public SolidBrush Button_Down;

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
                BackColor = new(scheme.Colors.BackColor);

                Button = new(scheme.Colors.Button);
                Button_Over = new(scheme.Colors.Button_Over);
                Button_Down = new(scheme.Colors.Button_Down);

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