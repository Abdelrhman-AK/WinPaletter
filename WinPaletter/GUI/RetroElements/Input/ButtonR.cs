using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{

    [Description("Retro button with Windows 9x style")]
    public class ButtonR : Button
    {
        public ButtonR()
        {
            Font = new("Microsoft Sans Serif", 8f);
            ForeColor = Color.Black;
            BackColor = Color.FromArgb(192, 192, 192);
            Image = base.Image;
            DoubleBuffered = true;
            LostFocus += ButtonR_LostFocus;

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

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        private bool Pressed;
        #endregion

        #region Properties

        private Image _Image;
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        private void ButtonR_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;
            Pressed = false;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!EnableEditingColors || e.Button == MouseButtons.Left)
            {
                State = MouseState.Down;
                Pressed = true;
            }

            SetPoints();

            Invalidate();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors && (e).Button != MouseButtons.Left)
            {
                if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonShadow)));
                else if (CursorOnDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonDkShadow)));
                else if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonHilight)));
                else if (CursorOnLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonLight)));
                else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonFace)));
                else if (CursorOnText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonText)));
                else if (CursorOnWindowFrame) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.WindowFrame)));
            }

            State = MouseState.Over;

            SetPoints();
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
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

        protected override void OnFontChanged(EventArgs e)
        {
            SetPoints();
            if (EnableEditingColors) Refresh();

            base.OnFontChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetPoints();
            if (EnableEditingColors) Refresh();

            base.OnSizeChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Image?.Dispose();   
        }

        #endregion

        #region Methods

        private void SetPoints()
        {
            rect = new(0, 0, Width - 1, Height - 1);
            rectinner = new(1, 1, Width - 3, Height - 3);
            rectdash = new(4, 4, Width - 9, Height - 9);

            SizeF TextSize = Text.Measure(Font);
            TextRect = new Rectangle(rect.X + (rect.Width - (int)TextSize.Width) / 2, rect.Y + (rect.Height - (int)TextSize.Height) / 2, (int)TextSize.Width, (int)TextSize.Height);

            if (UseItAsScrollbar)
            {
                btnShadowPoints0 = new PointF[] { new Point(1, Height - 2), new Point(Width - 2, Height - 2) };
                btnShadowPoints1 = new PointF[] { new Point(Width - 2, 1), new Point(Width - 2, Height - 2) };
                btnDkShadowPoints0 = new PointF[] { new Point(0, Height - 1), new Point(Width - 1, Height - 1) };
                btnDkShadowPoints1 = new PointF[] { new Point(Width - 1, 0), new Point(Width - 1, Height - 1) };
                btnHilightPoints0 = new PointF[] { new Point(0, 0), new Point(Width - 1, 0) };
                btnHilightPoints1 = new PointF[] { new Point(0, 1), new Point(0, Height - 1) };
                btnLightPoints0 = new PointF[] { new Point(1, 1), new Point(Width - 2, 1) };
                btnLightPoints1 = new PointF[] { new Point(1, 2), new Point(1, Height - 2) };
            }
            else if (AppearsAsPressed)
            {
                btnDkShadowPoints0 = new PointF[] { new Point(0, 0), new Point(Width - 1, 0) };
                btnDkShadowPoints1 = new PointF[] { new Point(0, 1), new Point(0, Height - 1) };
                btnShadowPoints0 = new PointF[] { new Point(1, 1), new Point(Width - 2, 1) };
                btnShadowPoints1 = new PointF[] { new Point(1, 2), new Point(1, Height - 2) };
                btnHilightPoints0 = new PointF[] { new Point(0, Height - 1), new Point(Width - 1, Height - 1) };
                btnHilightPoints1 = new PointF[] { new Point(Width - 1, 0), new Point(Width - 1, Height - 1) };
                btnLightPoints0 = new PointF[] { new Point(1, Height - 2), new Point(Width - 2, Height - 2) };
                btnLightPoints1 = new PointF[] { new Point(Width - 2, 1), new Point(Width - 2, Height - 2) };
            }
            else if (State == MouseState.Over | State == MouseState.None | !Enabled)
            {
                if (!Focused)
                {
                    btnHilightPoints0 = new PointF[] { new Point(0, 0), new Point(Width - 1, 0) };
                    btnHilightPoints1 = new PointF[] { new Point(0, 1), new Point(0, Height - 1) };
                    btnDkShadowPoints0 = new PointF[] { new Point(0, Height - 1), new Point(Width - 1, Height - 1) };
                    btnDkShadowPoints1 = new PointF[] { new Point(Width - 1, 0), new Point(Width - 1, Height - 1) };
                    btnLightPoints0 = new PointF[] { new Point(1, 1), new Point(Width - 2, 1) };
                    btnLightPoints1 = new PointF[] { new Point(1, 2), new Point(1, Height - 2) };
                    btnShadowPoints0 = new PointF[] { new Point(1, Height - 2), new Point(Width - 2, Height - 2) };
                    btnShadowPoints1 = new PointF[] { new Point(Width - 2, 1), new Point(Width - 2, Height - 2) };
                }
                else
                {
                    btnHilightPoints0 = new PointF[] { new Point(1, 1), new Point(Width - 2, 1) };
                    btnHilightPoints1 = new PointF[] { new Point(1, 2), new Point(1, Height - 2) };
                    btnDkShadowPoints0 = new PointF[] { new Point(1, Height - 2), new Point(Width - 2, Height - 2) };
                    btnDkShadowPoints1 = new PointF[] { new Point(Width - 2, 1), new Point(Width - 2, Height - 2) };
                    btnLightPoints0 = new PointF[] { new Point(2, 2), new Point(Width - 3, 2) };
                    btnLightPoints1 = new PointF[] { new Point(2, 3), new Point(2, Height - 3) };
                    btnShadowPoints0 = new PointF[] { new Point(2, Height - 3), new Point(Width - 3, Height - 3) };
                    btnShadowPoints1 = new PointF[] { new Point(Width - 3, 2), new Point(Width - 3, Height - 3) };
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;

            G.Clear(BackColor);

            #region Button Render

            if (UseItAsScrollbar)
            {
                #region Editor

                if (_ColorEdit_Face)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                }

                #endregion

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

                if (_ColorEdit_Shadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                        G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                    }
                }

                if (_ColorEdit_DkShadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                        G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                    }
                }

                if (_ColorEdit_Hilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                        G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                    }
                }

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
                G.Clear(ButtonHilight);

                #region Editor

                if (_ColorEdit_Face)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                }

                #endregion

                if (HatchBrush) { using (HatchBrush hb = new(HatchStyle.Percent50, ButtonHilight, BackColor)) { G.FillRectangle(hb, rect); } }

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

                if (_ColorEdit_Shadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                        G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                    }
                }

                if (_ColorEdit_DkShadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                        G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                    }
                }

                if (_ColorEdit_Hilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                        G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                    }
                }

                if (_ColorEdit_Light)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                        G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                    }
                }

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

                    if (_ColorEdit_Face)
                    {
                        Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                    }

                    #endregion

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

                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                            G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                        }
                    }

                    if (_ColorEdit_DkShadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                            G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                        }
                    }

                    if (_ColorEdit_Hilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                            G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                        }
                    }

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
                    #region Editor

                    if (_ColorEdit_Face)
                    {
                        Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                    }

                    #endregion

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

                        if (Pressed & !(Font.FontFamily.Name.ToLower() == "marlett"))
                        {
                            Rectangle ur = new(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight);
                            Rectangle dr = new(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height);
                            Rectangle lr = new(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height);
                            Rectangle rr = new(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height);

                            using (HatchBrush hb = new(HatchStyle.Percent50, Color.Black, BackColor))
                            {
                                G.FillRectangle(hb, ur);
                                G.FillRectangle(hb, dr);
                                G.FillRectangle(hb, lr);
                                G.FillRectangle(hb, rr);
                            }

                            using (Pen penWindowFrame = new(WindowFrame))
                            {
                                G.DrawRectangle(penWindowFrame, rect);
                            }
                        }
                    }

                    #region Editor

                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                            G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                        }
                    }

                    if (_ColorEdit_DkShadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                            G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                        }
                    }

                    if (_ColorEdit_Hilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                            G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                        }
                    }

                    if (_ColorEdit_Light)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                            G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                        }
                    }

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
                #region Editor

                if (_ColorEdit_Face)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
                }

                #endregion

                using (Pen penWindowFrame = new(WindowFrame))
                using (Pen penButtonShadow = new(ButtonShadow))
                {
                    G.DrawRectangle(penWindowFrame, rect);
                    G.DrawRectangle(penButtonShadow, rectinner);

                    if (!(Font.FontFamily.Name.ToLower() == "marlett"))
                    {
                        Rectangle ur = new(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight);
                        Rectangle dr = new(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height);
                        Rectangle lr = new(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height);
                        Rectangle rr = new(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height);

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

                if (_ColorEdit_Shadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                        G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                    }
                }

                if (_ColorEdit_DkShadow)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                        G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                    }
                }

                if (_ColorEdit_Hilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color))
                    {
                        G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                        G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                    }
                }

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

            StringFormat ButtonString = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            int imgX = default, imgY = default;

            try
            {
                if (Image is not null)
                    imgX = (int)Math.Round((Width - Image.Width) / 2d);
            }
            catch
            {
            }

            try
            {
                if (Image is not null)
                    imgY = (int)Math.Round((Height - Image.Height) / 2d);
            }
            catch
            {
            }

            Color FColor;
            if (Enabled)
                FColor = ForeColor;
            else
                FColor = base.BackColor.CB((float)-0.2d);

            if (Image is null)
            {

                if (TextAlign == ContentAlignment.MiddleCenter)
                {

                    Rectangle r = rect;

                    // Resetting positions to fix layout misadjust
                    // Never modify
                    if (Font.Name == "Marlett" & Text.Count() == 1)
                    {
                        SizeF textSize = Text.Measure(Font);
                        int x = (int)Math.Round(rect.X + (rect.Width - textSize.Width) / 2f);
                        int y = (int)Math.Round(rect.Y + (rect.Height - textSize.Height) / 2f);
                        int w = (int)Math.Round(textSize.Width);
                        int h = (int)Math.Round(textSize.Height);

                        if (Font.Size >= 4.4d && Font.Size < 5.2d)
                        {
                            h += 2;
                        }

                        else if (Font.Size >= 5.2d && Font.Size < 5.6d)
                        {
                            x += 1;
                        }

                        else if (Font.Size >= 5.6d && Font.Size < 6)
                        {
                            w += 1;
                            x += 1;
                            y += 1;
                        }

                        else if (Font.Size >= 6 && Font.Size < 6.4d)
                        {
                            x += 1;
                            y += 1;
                        }

                        else if (Font.Size >= 6.4d && Font.Size < 6.8d)
                        {
                            y += 1;
                        }

                        else if (Font.Size >= 6.8d && Font.Size < 7.2d)
                        {
                            x += 1;
                            y += 2;
                        }

                        else if (Font.Size >= 7.2d && Font.Size <= 7.6d)
                        {
                            y += 1;
                        }

                        else if (Font.Size >= 8 && Font.Size < 8.4d)
                        {
                            x += 1;
                        }

                        else if (Font.Size >= 8.4d && Font.Size < 8.8d)
                        {
                            x += 1;
                            y += 1;
                        }

                        else if (Font.Size == 8.8d)
                        {

                            if (Text == "3")
                            {
                                x += 1;
                                y += 1;
                            }
                            else if (Text != "u")
                            {
                                y += 1;
                            }

                        }

                        r = new(x, y, w, h);
                    }

                    if (!Enabled)
                        G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(r.X + 1, r.Y + 1, r.Width, r.Height), ContentAlignment.MiddleCenter.ToStringFormat());
                    G.DrawString(Text, Font, new SolidBrush(FColor), r, ContentAlignment.MiddleCenter.ToStringFormat());
                }
                else
                {
                    if (!Enabled)
                        G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(1, 1, Width, Height), base.TextAlign.ToStringFormat());
                    G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(0, 0, Width - 1, Height - 1), base.TextAlign.ToStringFormat());
                }
            }

            else
            {
                switch (ImageAlign)
                {
                    case ContentAlignment.MiddleCenter:
                        {
                            ButtonString.Alignment = StringAlignment.Center;
                            ButtonString.LineAlignment = StringAlignment.Near;

                            int alx = (int)Math.Round((Height - (Image.Height + 4 + base.Text.Measure(base.Font).Height)) / 2f);
                            if (string.IsNullOrEmpty(Text))
                            {
                                G.DrawImage(Image, new Rectangle(imgX, imgY, Image.Width, Image.Height));
                            }
                            else
                            {
                                G.DrawImage(Image, new Rectangle(imgX, alx, Image.Width, Image.Height));
                            }

                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(1, alx + 10 + Image.Height, Width, Height), ButtonString);
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(0, alx + 9 + Image.Height, Width, Height), ButtonString);
                            break;
                        }

                    case ContentAlignment.MiddleLeft:
                        {
                            ButtonString.Alignment = StringAlignment.Near;
                            ButtonString.LineAlignment = StringAlignment.Center;
                            int alx = (int)Math.Round((Width - (Image.Width + 4 + base.Text.Measure(base.Font).Width)) / 2f);
                            G.DrawImage(Image, new Rectangle(alx, imgY - 1, Image.Width, Image.Height));
                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(alx + 1 + Image.Width, 1, Width, Height), ButtonString);
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(alx + Image.Width, 0, Width, Height), ButtonString);
                            break;
                        }

                    case ContentAlignment.MiddleRight:
                        {
                            G.DrawImage(Image, new Rectangle(1, imgY - 1, Image.Width, Image.Height));
                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(8, 1, Width, Height), base.TextAlign.ToStringFormat());
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(7, 0, Width, Height), base.TextAlign.ToStringFormat());
                            break;
                        }

                    case ContentAlignment.BottomLeft:
                        {
                            G.DrawImage(Image, new Rectangle(1, imgY, Image.Width, Image.Height));
                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(1 + Image.Width + 2, 1, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat());
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(Image.Width + 1, 0, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat());
                            break;
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