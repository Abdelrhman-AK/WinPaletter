using System;
using System.Drawing;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// WinPaletter theme configuration
    /// </summary>
    public partial class Config
    {
        /// <summary>
        /// WinPaletter theme scheme
        /// </summary>
        public struct Scheme
        {
            /// <summary>
            /// WinPaletter theme colors
            /// </summary>
            public Colors_Collection Colors;

            /// <summary>
            /// WinPaletter theme pens for drawing graphics using <see cref="System.Drawing.Graphics"/>
            /// </summary>
            public Pens_Collection Pens;

            /// <summary>
            /// WinPaletter theme brushes for drawing graphics using <see cref="System.Drawing.Graphics"/>
            /// </summary>
            public Brushes_Collection Brushes;

            /// <summary>
            /// Create a new WinPaletter theme scheme with the specified accent color, background color, and dark mode
            /// </summary>
            /// <param name="accent"></param>
            /// <param name="backcolor"></param>
            /// <param name="isDark"></param>
            public Scheme(Color accent, Color backcolor, bool isDark)
            {
                Colors = new(accent, backcolor, isDark);
                Pens = new(this);
                Brushes = new(this);
            }
        }

        /// <summary>
        /// WinPaletter theme schemes collection
        /// </summary>
        public class Schemes_Collection
        {
            /// <summary>
            /// Initialize WinPaletter theme schemes collection with default colors
            /// </summary>
            public Schemes_Collection()
            {
                Scheme Main = new(DefaultColors.PrimaryColor_Dark, DefaultColors.BackColor_Dark, true);

                Scheme Secondary = new(DefaultColors.SecondaryColor_Dark, DefaultColors.BackColor_Dark, true);

                Scheme Tertiary = new(DefaultColors.TertiaryColor_Dark, DefaultColors.BackColor_Dark, true);

                Scheme Disabled = new(DefaultColors.DisabledColor_Dark, DefaultColors.DisabledBackColor_Dark, true);
            }

            /// <summary>
            /// WinPaletter main theme scheme
            /// </summary>
            public Scheme Main;

            /// <summary>
            /// WinPaletter secondary theme scheme; used for error elements and other secondary elements that require user's attention
            /// </summary>
            public Scheme Secondary;

            /// <summary>
            /// WinPaletter tertiary theme scheme; used for warning elements and other tertiary elements that require user's attention
            /// </summary>
            public Scheme Tertiary;

            /// <summary>
            /// WinPaletter disabled theme scheme; used for disabled elements
            /// </summary>
            public Scheme Disabled;
        }

        /// <summary>
        /// WinPaletter theme colors collection
        /// </summary>
        public class Colors_Collection : IDisposable, ICloneable
        {
            /// <summary>
            /// Factor for colors changing voids
            /// </summary>
            private readonly float incrementFactor = 0.01f;

            /// <summary>
            /// Dark mode flag
            /// </summary>
            public bool DarkMode { get; set; } = true;

            /// <summary>
            /// Default accent color
            /// </summary>
            public Color Accent { get; set; } = DefaultColors.PrimaryColor_Dark;

            /// <summary>
            /// Default method that will be used to change the accent color to be an another color
            /// </summary>
            private readonly Method Accent_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Accent_Method"/>  to change the accent color to be an another color
            /// </summary>
            private readonly float Accent_Factor;

            /// <summary>
            /// Default alternative accent color
            /// </summary>
            public Color AccentAlt { get; set; } = DefaultColors.PrimaryColor_Dark.Light(0.5f);

            /// <summary>
            /// Default method that will be used to change the alternative accent color to be an another color
            /// </summary>
            private readonly Method AccentAlt_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="AccentAlt_Method"/>  to change the alternative accent color to be an another color
            /// </summary>
            private readonly float AccentAlt_Factor;

            /// <summary>
            /// Default background color
            /// </summary>
            public Color BackColor { get; set; } = DefaultColors.BackColor_Dark;

            /// <summary>
            /// Default method that will be used to change the background color to be an another color
            /// </summary>
            private readonly Method BackColor_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="BackColor_Method"/>  to change the background color to be an another color
            /// </summary>
            private readonly float BackColor_Factor;

            /// <summary>
            /// Default button color
            /// </summary>
            public Color Button { get; set; } = DefaultColors.BackColor_Dark.CB(0.07f);

            /// <summary>
            /// Default method that will be used to change the button color to be an another color
            /// </summary>
            private readonly Method Button_Method; // Use Color.CB() instead of Color.Light() or Color.Dark()

            /// <summary>
            /// Default factor that will be used by <see cref="Button_Method"/>  to change the button color to be an another color
            /// </summary>
            private readonly float Button_Factor; // Factor for colors changing voids

            /// <summary>
            /// Default button hover color
            /// </summary>
            public Color Button_Over { get; set; } = DefaultColors.BackColor_Dark.CB(0.1f);

            /// <summary>
            /// Default method that will be used to change the button hover color to be an another color
            /// </summary>
            private readonly Method Button_Over_Method; // Use Color.CB() instead of Color.Light() or Color.Dark()

            /// <summary>
            /// Default factor that will be used by <see cref="Button_Over_Method"/>  to change the button hover color to be an another color
            /// </summary>
            private readonly float Button_Over_Factor; // Factor for colors changing voids

            /// <summary>
            /// Default button down color
            /// </summary>
            public Color Button_Down { get; set; } = DefaultColors.BackColor_Dark.CB(0.07f);

            /// <summary>
            /// Default method that will be used to change the button down color to be an another color
            /// </summary>
            private readonly Method Button_Down_Method; // Use Color.CB() instead of Color.Light() or Color.Dark()

            /// <summary>
            /// Default factor that will be used by <see cref="Button_Down_Method"/>  to change the button down color to be an another color
            /// </summary>
            private readonly float Button_Down_Factor; // Factor for colors changing voids

            /// <summary>
            /// Default background color provided by the level of the control. Useful for nested controls.
            /// </summary>
            /// <param name="level"></param>
            /// <returns></returns>
            public Color Back(int level = 0)
            {
                float factor = incrementFactor * level * (DarkMode ? -0.5f : +0.5f);
                return ApplyMethod(BackColor, Back_Method, Back_Factor + factor);
            }

            /// <summary>
            /// Default method that will be used to change the background color to be an another color
            /// </summary>
            private readonly Method Back_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Back_Method"/>  to change the background color to be an another color
            /// </summary>
            private readonly float Back_Factor;

            /// <summary>
            /// Default line color provided by the level of the control. Useful for nested controls.
            /// </summary>
            /// <param name="level"></param>
            /// <returns></returns>
            public Color Line(int level = 0)
            {
                float factor = incrementFactor * level * (DarkMode ? -0.5f : +0.5f);
                return ApplyMethod(BackColor, Line_Method, Line_Factor + factor);
            }

            /// <summary>
            /// Default method that will be used to change the line color to be an another color
            /// </summary>
            private readonly Method Line_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Line_Method"/>  to change the line color to be an another color
            /// </summary>
            private readonly float Line_Factor;

            /// <summary>
            /// Default background hover color provided by the level of the control. Useful for nested controls.
            /// </summary>
            /// <param name="level"></param>
            /// <returns></returns>
            public Color Back_Hover(int level = 0)
            {
                float factor = incrementFactor * level * (DarkMode ? -1 : +1);
                return ApplyMethod(BackColor, Back_Hover_Method, Back_Hover_Factor + factor);
            }

            /// <summary>
            /// Default method that will be used to change the background hover color to be an another color
            /// </summary>
            private readonly Method Back_Hover_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Back_Hover_Method"/>  to change the background hover color to be an another color
            /// </summary>
            private readonly float Back_Hover_Factor;

            /// <summary>
            /// Default line hover color provided by the level of the control. Useful for nested controls.
            /// </summary>
            /// <param name="level"></param>
            /// <returns></returns>
            public Color Line_Hover(int level = 0)
            {
                float factor = incrementFactor * level * (DarkMode ? -1 : +1);
                return ApplyMethod(BackColor, Line_Hover_Method, Line_Hover_Factor + factor);
            }

            /// <summary>
            /// Default method that will be used to change the line hover color to be an another color
            /// </summary>
            private readonly Method Line_Hover_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Line_Hover_Method"/>  to change the line hover color to be an another color
            /// </summary>
            private readonly float Line_Hover_Factor;

            /// <summary>
            /// Default background checked color provided by the level of the control. Useful for nested controls.
            /// </summary>
            public Color Back_Checked { get; set; }

            /// <summary>
            /// Default method that will be used to change the background checked color to be an another color
            /// </summary>
            private readonly Method Back_Checked_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Back_Checked_Method"/>  to change the background checked color to be an another color
            /// </summary>
            private readonly float Back_Checked_Factor;

            /// <summary>
            /// Default line checked color provided by the level of the control. Useful for nested controls.
            /// </summary>
            public Color Line_Checked { get; set; }

            /// <summary>
            /// Default method that will be used to change the line checked color to be an another color
            /// </summary>
            private readonly Method Line_Checked_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Line_Checked_Method"/>  to change the line checked color to be an another color
            /// </summary>
            private readonly float Line_Checked_Factor;

            /// <summary>
            /// Default background checked hover color provided by the level of the control. Useful for nested controls.
            /// </summary>
            public Color Back_Checked_Hover { get; set; }

            /// <summary>
            /// Default method that will be used to change the background checked hover color to be an another color
            /// </summary>
            private readonly Method Back_Checked_Hover_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Back_Checked_Hover_Method"/>  to change the background checked hover color to be an another color
            /// </summary>
            private readonly float Back_Checked_Hover_Factor;

            /// <summary>
            /// Default line checked hover color provided by the level of the control. Useful for nested controls.
            /// </summary>
            public Color Line_Checked_Hover { get; set; }

            /// <summary>
            /// Default method that will be used to change the line checked hover color to be an another color
            /// </summary>
            private readonly Method Line_Checked_Hover_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="Line_Checked_Hover_Method"/>  to change the line checked hover color to be an another color
            /// </summary>
            private readonly float Line_Checked_Hover_Factor;

            /// <summary>
            /// Default foreground color provided by the level of the control. Useful for nested controls.
            /// </summary>
            public Color ForeColor { get; set; }

            /// <summary>
            /// Default method that will be used to change the foreground color to be an another color
            /// </summary>
            private readonly Method ForeColor_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="ForeColor_Method"/>  to change the foreground color to be an another color
            /// </summary>
            private readonly float ForeColor_Factor;

            /// <summary>
            /// Default accent foreground color provided by the level of the control. Useful for nested controls.
            /// </summary>
            public Color ForeColor_Accent { get; set; }

            /// <summary>
            /// Default method that will be used to change the accent foreground color to be an another color
            /// </summary>
            private readonly Method ForeColor_Accent_Method;

            /// <summary>
            /// Default factor that will be used by <see cref="ForeColor_Accent_Method"/>  to change the accent foreground color to be an another color
            /// </summary>
            private readonly float ForeColor_Accent_Factor;

            /// <summary>
            /// Create a new WinPaletter theme colors collection with the specified accent color, background color, and dark mode
            /// </summary>
            /// <param name="accent"></param>
            /// <param name="backcolor"></param>
            /// <param name="Dark"></param>
            public Colors_Collection(Color accent, Color backcolor, bool Dark)
            {
                Accent = accent;
                BackColor = backcolor;

                if (Dark)
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

            /// <summary>
            /// Apply the specified method to the color with the specified factor
            /// </summary>
            public enum Method
            {
                /// <summary>
                /// Change color brightness
                /// </summary>
                CB,

                /// <summary>
                /// Lighten color
                /// </summary>
                Light,

                /// <summary>
                /// Darken color
                /// </summary>
                Dark
            }

            /// <summary>
            /// Apply the specified method to the color with the specified factor
            /// </summary>
            /// <param name="color"></param>
            /// <param name="method"></param>
            /// <param name="factor"></param>
            /// <returns></returns>
            Color ApplyMethod(Color color, Method method, float factor)
            {
                return method switch
                {
                    Method.CB => color.CB(factor),
                    Method.Light => color.Light(factor),
                    Method.Dark => color.Dark(factor),
                    _ => color.CB(factor),
                };
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

            /// <summary>
            /// Clone WinPaletter theme
            /// </summary>
            /// <returns></returns>
            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        /// <summary>
        /// WinPaletter theme pens collection for drawing graphics using <see cref="System.Drawing.Graphics"/>
        /// </summary>
        /// <param name="scheme"></param>
        public class Pens_Collection(Scheme scheme)
        {
            /// <summary>
            /// Accent color pen
            /// </summary>
            public Pen Accent = new(scheme.Colors.Accent);

            /// <summary>
            /// Alternative accent color pen
            /// </summary>
            public Pen AccentAlt = new(scheme.Colors.AccentAlt);

            /// <summary>
            /// Background color pen
            /// </summary>
            public Pen BackColor = new(scheme.Colors.BackColor);

            /// <summary>
            /// Button color pen
            /// </summary>
            public Pen Button = new(scheme.Colors.Button);

            /// <summary>
            /// Button hover color pen
            /// </summary>
            public Pen Button_Over = new(scheme.Colors.Button_Over);

            /// <summary>
            /// Button down color pen
            /// </summary>
            public Pen Button_Down = new(scheme.Colors.Button_Down);

            /// <summary>
            /// Background color pen.
            /// </summary>
            public Pen Back_Checked = new(scheme.Colors.Back_Checked);

            /// <summary>
            /// Line color pen.
            /// </summary>
            public Pen Line_Checked = new(scheme.Colors.Line_Checked);

            /// <summary>
            /// Background checked color pen.
            /// </summary>
            public Pen Back_Checked_Hover = new(scheme.Colors.Back_Checked_Hover);

            /// <summary>
            /// Line checked color pen.
            /// </summary>
            public Pen Line_Checked_Hover = new(scheme.Colors.Line_Checked_Hover);

            /// <summary>
            /// Foreground color pen.
            /// </summary>
            public Pen ForeColor = new(scheme.Colors.ForeColor);

            /// <summary>
            /// Accent foreground color pen.
            /// </summary>
            public Pen ForeColor_Accent = new(scheme.Colors.ForeColor_Accent);
        }

        /// <summary>
        /// WinPaletter theme brushes collection for drawing graphics using <see cref="System.Drawing.Graphics"/>
        /// </summary>
        /// <param name="scheme"></param>
        public class Brushes_Collection(Scheme scheme)
        {
            /// <summary>
            /// Accent color brush
            /// </summary>
            public SolidBrush Accent = new(scheme.Colors.Accent);

            /// <summary>
            /// Alternative accent color brush
            /// </summary>
            public SolidBrush AccentAlt = new(scheme.Colors.AccentAlt);

            /// <summary>
            /// Background color brush
            /// </summary>
            public SolidBrush BackColor = new(scheme.Colors.BackColor);

            /// <summary>
            /// Button color brush
            /// </summary>
            public SolidBrush Button = new(scheme.Colors.Button);

            /// <summary>
            /// Button hover color brush
            /// </summary>
            public SolidBrush Button_Over = new(scheme.Colors.Button_Over);

            /// <summary>
            /// Button down color brush
            /// </summary>
            public SolidBrush Button_Down = new(scheme.Colors.Button_Down);

            /// <summary>
            /// Background color brush
            /// </summary>
            public SolidBrush Back_Checked = new(scheme.Colors.Back_Checked);

            /// <summary>
            /// Line color brush
            /// </summary>
            public SolidBrush Line_Checked = new(scheme.Colors.Line_Checked);

            /// <summary>
            /// Background checked color brush
            /// </summary>
            public SolidBrush Back_Checked_Hover = new(scheme.Colors.Back_Checked_Hover);

            /// <summary>
            /// Line checked color brush
            /// </summary>
            public SolidBrush Line_Checked_Hover = new(scheme.Colors.Line_Checked_Hover);

            /// <summary>
            /// Foreground color brush
            /// </summary>
            public SolidBrush ForeColor = new(scheme.Colors.ForeColor);

            /// <summary>
            /// Accent foreground color brush
            /// </summary>
            public SolidBrush ForeColor_Accent = new(scheme.Colors.ForeColor_Accent);
        }
    }
}