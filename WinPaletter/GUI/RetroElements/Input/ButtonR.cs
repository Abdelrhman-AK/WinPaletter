using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// Retro button with Windows 9x style
    /// </summary>

    [Description("Retro button with Windows 9x style")]
    public class ButtonR : Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonR"/> class.
        /// </summary>
        public ButtonR()
        {
            Font = new("Microsoft Sans Serif", 8f);
            ForeColor = Color.Black;
            BackColor = Color.FromArgb(192, 192, 192);
            Image = base.Image;
            DoubleBuffered = true;
            LostFocus += ButtonR_LostFocus;

            // Set button points and text rectangle (geometry)
            SetPoints();
        }

        #region Variables
        Rectangle rect;
        Rectangle rectinner;
        Rectangle rectdash;
        Rectangle TextRect;

        private PointF[] btnShadowPoints0;
        private PointF[] btnShadowPoints1;
        private PointF[] btnDkShadowPoints0;
        private PointF[] btnDkShadowPoints1;
        private PointF[] btnHilightPoints0;
        private PointF[] btnHilightPoints1;
        private PointF[] btnLightPoints0;
        private PointF[] btnLightPoints1;

        private MouseState State = MouseState.None;

        /// <summary>
        /// Mouse states enumeration
        /// </summary>
        public enum MouseState
        {
            /// <summary>
            /// Mouse is not over the button
            /// </summary>
            None,
            /// <summary>
            /// Mouse is over the button
            /// </summary>
            Over,
            /// <summary>
            /// Mouse is pressing the button
            /// </summary>
            Down
        }

        private bool Pressed;
        #endregion

        #region Properties

        private Image _Image;
        /// <summary>
        /// Gets or sets the image that is displayed on a button control.
        /// </summary>
        public new Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
                Invalidate();
            }
        }

        private Color windowFrame = SystemColors.WindowFrame;
        private Color buttonShadow = SystemColors.ButtonShadow;
        private Color buttonDkShadow = SystemColors.ControlDark;
        private Color buttonHilight = SystemColors.ButtonHighlight;
        private Color buttonLight = SystemColors.ControlLight;
        private bool useItAsScrollbar = false;
        private bool appearsAsPressed = false;
        private bool hatchBrush = false;
        private int focusRectWidth = 1;
        private int focusRectHeight = 1;

        /// <summary>
        /// Gets or sets the color of the window frame.
        /// </summary>
        public Color WindowFrame
        {
            get { return windowFrame; }
            set
            {
                if (windowFrame != value)
                {
                    windowFrame = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the button shadow.
        /// </summary>
        public Color ButtonShadow
        {
            get { return buttonShadow; }
            set
            {
                if (buttonShadow != value)
                {
                    buttonShadow = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the button dark shadow.
        /// </summary>
        public Color ButtonDkShadow
        {
            get { return buttonDkShadow; }
            set
            {
                if (buttonDkShadow != value)
                {
                    buttonDkShadow = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the button hilight.
        /// </summary>
        public Color ButtonHilight
        {
            get { return buttonHilight; }
            set
            {
                if (buttonHilight != value)
                {
                    buttonHilight = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the button light.
        /// </summary>
        public Color ButtonLight
        {
            get { return buttonLight; }
            set
            {
                if (buttonLight != value)
                {
                    buttonLight = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the button is used as a scrollbar.
        /// </summary>
        public bool UseItAsScrollbar
        {
            get { return useItAsScrollbar; }
            set
            {
                if (useItAsScrollbar != value)
                {
                    useItAsScrollbar = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the button appears as pressed.
        /// </summary>
        public bool AppearsAsPressed
        {
            get { return appearsAsPressed; }
            set
            {
                if (appearsAsPressed != value)
                {
                    appearsAsPressed = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the button uses a hatch brush.
        /// </summary>
        public bool HatchBrush
        {
            get { return hatchBrush; }
            set
            {
                if (hatchBrush != value)
                {
                    hatchBrush = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the focus rectangle.
        /// </summary>
        public int FocusRectWidth
        {
            get { return focusRectWidth; }
            set
            {
                if (focusRectWidth != value)
                {
                    focusRectWidth = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the focus rectangle.
        /// </summary>
        public int FocusRectHeight
        {
            get { return focusRectHeight; }
            set
            {
                if (focusRectHeight != value)
                {
                    focusRectHeight = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the colors editor is enabled.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Occurs when the colors editor is invoked after clicking on a color on the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Occurs when the colors editor is invoked after clicking on a color on the button.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Raises the <see cref="Control.LostFocus"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonR_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;
            Pressed = false;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseEnter"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseLeave"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // Reset editing flags on the button
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnShadow = false;
                CursorOnDkShadow = false;
                CursorOnHilight = false;
                CursorOnLight = false;
                CursorOnFace = false;
                CursorOnText = false;
                CursorOnWindowFrame = false;
            }

            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!EnableEditingColors || e.Button == MouseButtons.Left)
            {
                State = MouseState.Down;
                Pressed = true;
            }

            // Set button points and text rectangle (geometry)
            SetPoints();

            Invalidate();

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Invoke the colors editor if the button is right-clicked
            if (!DesignMode && EnableEditingColors && e.Button != MouseButtons.Left)
            {
                // Invoke the colors editor based on the clicked color

                // Edit button shadow color
                if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonShadow)));

                // Edit button dark shadow color
                else if (CursorOnDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonDkShadow)));

                // Edit button hilight color
                else if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonHilight)));

                // Edit button light color
                else if (CursorOnLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonLight)));

                // Edit button face color
                else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonFace)));

                // Edit button text color
                else if (CursorOnText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonText)));

                // Edit window frame color
                else if (CursorOnWindowFrame) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.WindowFrame)));
            }

            State = MouseState.Over;

            // Set button points and text rectangle (geometry)
            SetPoints();
            Invalidate();

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Set editing flags on the button
            if (!DesignMode && EnableEditingColors)
            {
                // Set editing flags based on the mouse position
                if ((State == MouseState.Over | State == MouseState.None | !Enabled) && Focused)
                {
                    CursorOnWindowFrame = rect.BordersContains(e.Location);
                    CursorOnShadow = !CursorOnWindowFrame && (btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location));
                    CursorOnDkShadow = !CursorOnWindowFrame && (btnDkShadowPoints0.Contains(e.Location) || btnDkShadowPoints1.Contains(e.Location));
                    CursorOnHilight = !CursorOnWindowFrame && (btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location));
                    CursorOnLight = !CursorOnWindowFrame && (btnLightPoints0.Contains(e.Location) || btnLightPoints1.Contains(e.Location));
                }
                else
                {
                    CursorOnWindowFrame = false;
                    CursorOnShadow = btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location);
                    CursorOnDkShadow = btnDkShadowPoints0.Contains(e.Location) || btnDkShadowPoints1.Contains(e.Location);
                    CursorOnHilight = btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location);
                    CursorOnLight = btnLightPoints0.Contains(e.Location) || btnLightPoints1.Contains(e.Location);
                }

                CursorOnText = TextRect.Contains(e.Location);
                CursorOnFace = rect.Contains(e.Location) && !CursorOnShadow && !CursorOnDkShadow && !CursorOnHilight && !CursorOnLight && !CursorOnText && !CursorOnWindowFrame;

                Refresh();
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.GotFocus"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            // Set button points and text rectangle (geometry)
            SetPoints();
            if (EnableEditingColors) Refresh();

            base.OnFontChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            // Set button points and text rectangle (geometry)
            SetPoints();
            if (EnableEditingColors) Refresh();

            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            // Dispose the button image
            base.Dispose(disposing);

            // Dispose the button image
            Image?.Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set button points and text rectangle (geometry)
        /// </summary>
        private void SetPoints()
        {
            // Main button rectangle
            rect = new(0, 0, Width - 1, Height - 1);

            // Inner button rectangle
            rectinner = new(1, 1, Width - 3, Height - 3);

            // Focus rectangle
            rectdash = new(4, 4, Width - 9, Height - 9);

            // Measure the text size
            SizeF TextSize = Text.Measure(Font);

            // Text rectangle
            TextRect = new Rectangle(rect.X + (rect.Width - (int)TextSize.Width) / 2, rect.Y + (rect.Height - (int)TextSize.Height) / 2, (int)TextSize.Width, (int)TextSize.Height);

            // Button points
            if (UseItAsScrollbar)
            {
                btnShadowPoints0 = [new Point(1, Height - 2), new Point(Width - 2, Height - 2)];
                btnShadowPoints1 = [new Point(Width - 2, 1), new Point(Width - 2, Height - 2)];
                btnDkShadowPoints0 = [new Point(0, Height - 1), new Point(Width - 1, Height - 1)];
                btnDkShadowPoints1 = [new Point(Width - 1, 0), new Point(Width - 1, Height - 1)];
                btnHilightPoints0 = [new Point(0, 0), new Point(Width - 1, 0)];
                btnHilightPoints1 = [new Point(0, 1), new Point(0, Height - 1)];
                btnLightPoints0 = [new Point(1, 1), new Point(Width - 2, 1)];
                btnLightPoints1 = [new Point(1, 2), new Point(1, Height - 2)];
            }
            else if (AppearsAsPressed)
            {
                btnDkShadowPoints0 = [new Point(0, 0), new Point(Width - 1, 0)];
                btnDkShadowPoints1 = [new Point(0, 1), new Point(0, Height - 1)];
                btnShadowPoints0 = [new Point(1, 1), new Point(Width - 2, 1)];
                btnShadowPoints1 = [new Point(1, 2), new Point(1, Height - 2)];
                btnHilightPoints0 = [new Point(0, Height - 1), new Point(Width - 1, Height - 1)];
                btnHilightPoints1 = [new Point(Width - 1, 0), new Point(Width - 1, Height - 1)];
                btnLightPoints0 = [new Point(1, Height - 2), new Point(Width - 2, Height - 2)];
                btnLightPoints1 = [new Point(Width - 2, 1), new Point(Width - 2, Height - 2)];
            }
            else if (State == MouseState.Over | State == MouseState.None | !Enabled)
            {
                if (!Focused)
                {
                    btnHilightPoints0 = [new Point(0, 0), new Point(Width - 1, 0)];
                    btnHilightPoints1 = [new Point(0, 1), new Point(0, Height - 1)];
                    btnDkShadowPoints0 = [new Point(0, Height - 1), new Point(Width - 1, Height - 1)];
                    btnDkShadowPoints1 = [new Point(Width - 1, 0), new Point(Width - 1, Height - 1)];
                    btnLightPoints0 = [new Point(1, 1), new Point(Width - 2, 1)];
                    btnLightPoints1 = [new Point(1, 2), new Point(1, Height - 2)];
                    btnShadowPoints0 = [new Point(1, Height - 2), new Point(Width - 2, Height - 2)];
                    btnShadowPoints1 = [new Point(Width - 2, 1), new Point(Width - 2, Height - 2)];
                }
                else
                {
                    btnHilightPoints0 = [new Point(1, 1), new Point(Width - 2, 1)];
                    btnHilightPoints1 = [new Point(1, 2), new Point(1, Height - 2)];
                    btnDkShadowPoints0 = [new Point(1, Height - 2), new Point(Width - 2, Height - 2)];
                    btnDkShadowPoints1 = [new Point(Width - 2, 1), new Point(Width - 2, Height - 2)];
                    btnLightPoints0 = [new Point(2, 2), new Point(Width - 3, 2)];
                    btnLightPoints1 = [new Point(2, 3), new Point(2, Height - 3)];
                    btnShadowPoints0 = [new Point(2, Height - 3), new Point(Width - 3, Height - 3)];
                    btnShadowPoints1 = [new Point(Width - 3, 2), new Point(Width - 3, Height - 3)];
                }
            }
            else { }
        }

        #endregion

        #region Colors editor

        private bool CursorOnShadow, CursorOnDkShadow, CursorOnHilight, CursorOnLight, CursorOnFace, CursorOnText, CursorOnWindowFrame;
        private bool _ColorEdit_Shadow => EnableEditingColors && CursorOnShadow;
        private bool _ColorEdit_DkShadow => EnableEditingColors && CursorOnDkShadow;
        private bool _ColorEdit_Hilight => EnableEditingColors && CursorOnHilight;
        private bool _ColorEdit_Light => EnableEditingColors && CursorOnLight;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _ColorEdit_Text => EnableEditingColors && CursorOnText;
        private bool _ColorEdit_WindowFrame => EnableEditingColors && CursorOnWindowFrame;

        #endregion

        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create a new bitmap and graphics object
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Set background color
            G.Clear(BackColor);

            #region Button Render

            if (UseItAsScrollbar)
            {
                #region Editor

                // Draw the face hatch to indicate that button face color will be edited if clicked
                if (_ColorEdit_Face)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                }

                #endregion

                // Draw the 3D button borders
                using (Pen penButtonHilight = new(ButtonHilight))
                using (Pen penButtonDkShadow = new(ButtonDkShadow))
                using (Pen penButtonLight = new(ButtonLight))
                using (Pen penButtonShadow = new(ButtonShadow))
                {
                    G.DrawLine(penButtonHilight, new Point(0, 0), new Point(Width - 1, 0));
                    G.DrawLine(penButtonHilight, new Point(0, 1), new Point(0, Height - 1));

                    G.DrawLine(penButtonDkShadow, new Point(0, Height - 1), new Point(Width - 1, Height - 1));
                    G.DrawLine(penButtonDkShadow, new Point(Width - 1, 0), new Point(Width - 1, Height - 1));

                    G.DrawLine(penButtonLight, new Point(1, 1), new Point(Width - 2, 1));
                    G.DrawLine(penButtonLight, new Point(1, 2), new Point(1, Height - 2));

                    G.DrawLine(penButtonShadow, new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                    G.DrawLine(penButtonShadow, new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
                }

                #region Editor

                // Draw an alternative button shadow to indicate that it will be edited if clicked
                if (_ColorEdit_Shadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                        G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                    }
                }

                // Draw an alternative button dark shadow to indicate that it will be edited if clicked
                if (_ColorEdit_DkShadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                        G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                    }
                }

                // Draw an alternative button hilight to indicate that it will be edited if clicked
                if (_ColorEdit_Hilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                        G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                    }
                }

                // Draw an alternative button light to indicate that it will be edited if clicked
                if (_ColorEdit_Light)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                        G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                    }
                }

                #endregion
            }
            else if (AppearsAsPressed)
            {
                // The button is pressed, fill with button hilight color instead of button face color
                G.Clear(ButtonHilight);

                #region Editor

                // Draw the face hatch to indicate that button face color will be edited if clicked
                if (_ColorEdit_Face)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                }

                #endregion

                // Draw hatch brush if required
                if (HatchBrush) { using (HatchBrush hb = new(HatchStyle.Percent50, ButtonHilight, BackColor)) { G.FillRectangle(hb, rect); } }

                // Draw the 3D button borders 
                using (Pen penButtonDkShadow = new(ButtonDkShadow))
                using (Pen penButtonShadow = new(ButtonShadow))
                using (Pen penButtonHilight = new(ButtonHilight))
                using (Pen penBackColor = new(BackColor))
                {
                    G.DrawLine(penButtonDkShadow, new Point(0, 0), new Point(Width - 1, 0));
                    G.DrawLine(penButtonDkShadow, new Point(0, 1), new Point(0, Height - 1));

                    G.DrawLine(penButtonShadow, new Point(1, 1), new Point(Width - 2, 1));
                    G.DrawLine(penButtonShadow, new Point(1, 2), new Point(1, Height - 2));

                    G.DrawLine(penButtonHilight, new Point(0, Height - 1), new Point(Width - 1, Height - 1));
                    G.DrawLine(penButtonHilight, new Point(Width - 1, 0), new Point(Width - 1, Height - 1));

                    G.DrawLine(penBackColor, new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                    G.DrawLine(penBackColor, new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
                }

                #region Editor

                // Draw an alternative button shadow to indicate that it will be edited if clicked
                if (_ColorEdit_Shadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                        G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                    }
                }

                // Draw an alternative button dark shadow to indicate that it will be edited if clicked
                if (_ColorEdit_DkShadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                        G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                    }
                }

                // Draw an alternative button hilight to indicate that it will be edited if clicked
                if (_ColorEdit_Hilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                        G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                    }
                }

                // Draw an alternative button light to indicate that it will be edited if clicked
                if (_ColorEdit_Light)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                        G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                    }
                }

                // IGNORE
                if (_ColorEdit_WindowFrame)
                {

                }

                #endregion

            }

            else if (State == MouseState.Over | State == MouseState.None | !Enabled)
            {
                if (!Focused)
                {
                    #region Editor

                    // Draw the face hatch to indicate that button face color will be edited if clicked
                    if (_ColorEdit_Face)
                    {
                        Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                    }

                    #endregion

                    // Draw the 3D button borders
                    using (Pen penButtonHilight = new(ButtonHilight))
                    using (Pen penButtonDkShadow = new(ButtonDkShadow))
                    using (Pen penButtonLight = new(ButtonLight))
                    using (Pen penButtonShadow = new(ButtonShadow))
                    {
                        G.DrawLine(penButtonHilight, new Point(0, 0), new Point(Width - 1, 0));
                        G.DrawLine(penButtonHilight, new Point(0, 1), new Point(0, Height - 1));

                        G.DrawLine(penButtonDkShadow, new Point(0, Height - 1), new Point(Width - 1, Height - 1));
                        G.DrawLine(penButtonDkShadow, new Point(Width - 1, 0), new Point(Width - 1, Height - 1));

                        G.DrawLine(penButtonLight, new Point(1, 1), new Point(Width - 2, 1));
                        G.DrawLine(penButtonLight, new Point(1, 2), new Point(1, Height - 2));

                        G.DrawLine(penButtonShadow, new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                        G.DrawLine(penButtonShadow, new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
                    }

                    #region Editor

                    // Draw an alternative button shadow to indicate that it will be edited if clicked
                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                            G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                        }
                    }

                    // Draw an alternative button dark shadow to indicate that it will be edited if clicked
                    if (_ColorEdit_DkShadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                            G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                        }
                    }

                    // Draw an alternative button hilight to indicate that it will be edited if clicked
                    if (_ColorEdit_Hilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                            G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                        }
                    }

                    // Draw an alternative button light to indicate that it will be edited if clicked
                    if (_ColorEdit_Light)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                            G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                        }
                    }

                    #endregion

                }
                else
                {
                    // Draw a focused button appearance

                    #region Editor

                    // Draw the face hatch to indicate that button face color will be edited if clicked
                    if (_ColorEdit_Face)
                    {
                        Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                    }

                    #endregion

                    // Draw the 3D button borders
                    using (Pen penButtonDkShadow = new(ButtonDkShadow))
                    using (Pen penButtonHilight = new(ButtonHilight))
                    using (Pen penButtonLight = new(ButtonLight))
                    using (Pen penButtonShadow = new(ButtonShadow))
                    {
                        G.DrawRectangle(penButtonDkShadow, rect);
                        G.DrawLine(penButtonHilight, new Point(1, 1), new Point(Width - 2, 1));
                        G.DrawLine(penButtonHilight, new Point(1, 2), new Point(1, Height - 2));
                        G.DrawLine(penButtonDkShadow, new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                        G.DrawLine(penButtonDkShadow, new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
                        G.DrawLine(penButtonLight, new Point(2, 2), new Point(Width - 3, 2));
                        G.DrawLine(penButtonLight, new Point(2, 3), new Point(2, Height - 3));
                        G.DrawLine(penButtonShadow, new Point(2, Height - 3), new Point(Width - 3, Height - 3));
                        G.DrawLine(penButtonShadow, new Point(Width - 3, 2), new Point(Width - 3, Height - 3));

                        // Draw the focus rectangle if the button is focused and not a control box button (Marlett font)
                        if (Pressed & Font.FontFamily.Name.ToLower() != "marlett")
                        {
                            Rectangle ur = new(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight);
                            Rectangle dr = new(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height);
                            Rectangle lr = new(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height);
                            Rectangle rr = new(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height);

                            // Fill by hatching
                            using (HatchBrush hb = new(HatchStyle.Percent50, Color.Black, BackColor))
                            {
                                G.FillRectangle(hb, ur);
                                G.FillRectangle(hb, dr);
                                G.FillRectangle(hb, lr);
                                G.FillRectangle(hb, rr);
                            }

                            // Draw the focus rectangle border
                            using (Pen penWindowFrame = new(WindowFrame))
                            {
                                G.DrawRectangle(penWindowFrame, rect);
                            }
                        }
                    }

                    #region Editor

                    // Draw an alternative button shadow to indicate that it will be edited if clicked
                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                            G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                        }
                    }

                    // Draw an alternative button dark shadow to indicate that it will be edited if clicked
                    if (_ColorEdit_DkShadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                            G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                        }
                    }

                    // Draw an alternative button hilight to indicate that it will be edited if clicked
                    if (_ColorEdit_Hilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                            G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                        }
                    }

                    // Draw an alternative button light to indicate that it will be edited if clicked
                    if (_ColorEdit_Light)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                            G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                        }
                    }

                    // Draw an alternative button window frame to indicate that it will be edited if clicked
                    if (_ColorEdit_WindowFrame)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color)) { G.DrawRectangle(P, rect); }
                    }

                    #endregion
                }
            }
            else
            {
                // Draw a clicked button appearance

                #region Editor

                // Draw the face hatch to indicate that button face color will be edited if clicked
                if (_ColorEdit_Face)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                }

                #endregion

                // Draw the 3D button borders
                using (Pen penWindowFrame = new(WindowFrame))
                using (Pen penButtonShadow = new(ButtonShadow))
                {
                    G.DrawRectangle(penWindowFrame, rect);
                    G.DrawRectangle(penButtonShadow, rectinner);

                    // Draw the focus rectangle if the button is focused and not a control box button (Marlett font)
                    if (Font.FontFamily.Name.ToLower() != "marlett")
                    {
                        Rectangle ur = new(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight);
                        Rectangle dr = new(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height);
                        Rectangle lr = new(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height);
                        Rectangle rr = new(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height);

                        // Fill by hatching
                        using (HatchBrush hb = new(HatchStyle.Percent50, Color.Black, BackColor))
                        {
                            G.FillRectangle(hb, ur);
                            G.FillRectangle(hb, dr);
                            G.FillRectangle(hb, lr);
                            G.FillRectangle(hb, rr);
                        }
                    }
                }

                #region Editor

                // Draw an alternative button shadow to indicate that it will be edited if clicked
                if (_ColorEdit_Shadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                        G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                    }
                }

                // Draw an alternative button dark shadow to indicate that it will be edited if clicked
                if (_ColorEdit_DkShadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                        G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                    }
                }

                // Draw an alternative button hilight to indicate that it will be edited if clicked
                if (_ColorEdit_Hilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                        G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                    }
                }

                // Draw an alternative button light to indicate that it will be edited if clicked
                if (_ColorEdit_Light)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                        G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                    }
                }

                #endregion
            }

            #endregion

            #region Text and Image Render

            // Draw the text in a decorative way to indicate that it will be edited if clicked
            if (_ColorEdit_Text)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, TextRect);
                    G.DrawRectangle(P, TextRect);
                }
            }

            int imgX = default, imgY = default;

            if (Image is not null)
            {
                imgX = (int)Math.Round((Width - Image.Width) / 2d);
                imgY = (int)Math.Round((Height - Image.Height) / 2d);
            }

            // Draw the text and image
            using (SolidBrush foreBrush = new(Enabled ? ForeColor : base.BackColor.CB((float)-0.2d)))
            {
                if (Image is null)
                {
                    if (TextAlign == ContentAlignment.MiddleCenter)
                    {
                        RectangleF r = rect;

                        // Resetting positions to fix layout misadjust
                        if (Font.Name == "Marlett" & Text.Count() == 1)
                        {
                            SizeF textSize = Text.Measure(Font);
                            float x = rect.X + (rect.Width - textSize.Width) / 2f;
                            float y = rect.Y + (rect.Height - textSize.Height) / 2f;
                            float w = textSize.Width;
                            float h = textSize.Height;

                            // Offsets are made to make control box label shown in its possition correctly
                            r = new(x + 0.5f, y + 1f, w, h);
                        }

                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            G.DrawString(Text, Font, foreBrush, r, sf);
                        }
                    }
                    else
                    {
                        using (StringFormat sf = base.TextAlign.ToStringFormat())
                        {
                            G.DrawString(Text, Font, foreBrush, new Rectangle(0, 0, Width - 1, Height - 1), sf);
                        }
                    }
                }

                else
                {
                    using (StringFormat ButtonString = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        switch (ImageAlign)
                        {
                            case ContentAlignment.MiddleCenter:
                                {
                                    ButtonString.Alignment = StringAlignment.Center;
                                    ButtonString.LineAlignment = StringAlignment.Near;

                                    // GetTextAndImageRectangles the image position, offsets are made to make image shown in its possition correctly
                                    int alx = (int)Math.Round((Height - (Image.Height + 4 + base.Text.Measure(base.Font).Height)) / 2f);
                                    if (string.IsNullOrEmpty(Text))
                                    {
                                        G.DrawImage(Image, new Rectangle(imgX, imgY, Image.Width, Image.Height));
                                    }
                                    else
                                    {
                                        G.DrawImage(Image, new Rectangle(imgX, alx, Image.Width, Image.Height));
                                    }

                                    // Offsets are made to make image shown in its possition correctly
                                    G.DrawString(Text, Font, foreBrush, new Rectangle(0, alx + 9 + Image.Height, Width, Height), ButtonString);
                                    break;
                                }

                            case ContentAlignment.MiddleLeft:
                                {
                                    ButtonString.Alignment = StringAlignment.Near;
                                    ButtonString.LineAlignment = StringAlignment.Center;
                                    // GetTextAndImageRectangles the image position, offsets are made to make image shown in its possition correctly
                                    int alx = (int)Math.Round((Width - (Image.Width + 4 + base.Text.Measure(base.Font).Width)) / 2f);
                                    G.DrawImage(Image, new Rectangle(alx, imgY - 1, Image.Width, Image.Height));
                                    G.DrawString(Text, Font, foreBrush, new Rectangle(alx + Image.Width, 0, Width, Height), ButtonString);
                                    break;
                                }

                            case ContentAlignment.MiddleRight:
                                {
                                    G.DrawImage(Image, new Rectangle(1, imgY - 1, Image.Width, Image.Height));
                                    using (StringFormat sf = base.TextAlign.ToStringFormat())
                                    {
                                        G.DrawString(Text, Font, foreBrush, new Rectangle(7, 0, Width, Height), sf);
                                    }
                                    break;
                                }

                            case ContentAlignment.BottomLeft:
                                {
                                    G.DrawImage(Image, new Rectangle(1, imgY, Image.Width, Image.Height));
                                    using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                                    {
                                        G.DrawString(Text, Font, foreBrush, new Rectangle(Image.Width + 1, 0, Width, Height), sf);
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            #endregion

            e.Graphics.DrawImage(B, new Point(0, 0));
            G.Dispose();
            B.Dispose();
        }
    }
}