using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    [Description("Retro window with Windows 9x style")]
    public class WindowR : Panel
    {
        public WindowR()
        {
            Font = new("Microsoft Sans Serif", 8f);
            titleHeight = PreviewHelpers.GetTitlebarTextHeight(Font);

            _CloseBtn = new() { Name = "CloseBtn", Text = "r", Font = new("Marlett", 7.8f), Size = new(BtnWidth, BtnHeight), TextAlign = ContentAlignment.MiddleCenter };
            _MinBtn = new() { Name = "MinBtn", Text = "1", Font = new("Marlett", 8f), Size = new(BtnWidth, BtnHeight), TextAlign = ContentAlignment.MiddleCenter };
            _MaxBtn = new() { Name = "MaxBtn", Text = "0", Font = new("Marlett", 8f), Size = new(BtnWidth, BtnHeight), TextAlign = ContentAlignment.MiddleCenter };
            BtnHeight = Metrics_CaptionHeight + titleHeight - 4;
            BtnWidth = Metrics_CaptionWidth - 2;
            DoubleBuffered = true;
            BackColor = SystemColors.ButtonFace;
            ForeColor = SystemColors.ControlText;
            BorderStyle = BorderStyle.None;
            Text = "New window";

            Controls.AddRange(new Control[] { _CloseBtn, _MaxBtn, _MinBtn });
            _CloseBtn.Visible = _ControlBox;
            _MinBtn.Visible = _ControlBox & _MinimizeBox;
            _MaxBtn.Visible = _ControlBox & _MaximizeBox;

            AdjustControlBoxFontsSizes();
            AdjustButtonSizes();
            AdjustLocations();
            AdjustPadding();
        }

        #region Variables

        Rectangle Rect;
        Rectangle Border;
        Rectangle TitlebarRect;
        Rectangle TitlebarTextRect;
        Rectangle r0;
        Rectangle r1;

        private int titleHeight;

        private Point newPoint = new();
        private Point oldPoint = new();

        private PointF[] btnShadowPoints0;
        private PointF[] btnShadowPoints1;
        private PointF[] btnDkShadowPoints0;
        private PointF[] btnDkShadowPoints1;
        private PointF[] btnHilightPoints0;
        private PointF[] btnHilightPoints1;
        private PointF[] btnLightPoints0;
        private PointF[] btnLightPoints1;

        int GripSize = 6;
        Rectangle editingRect;
        Rectangle Grip_topLeft;
        Rectangle Grip_topRight;
        Rectangle Grip_bottomLeft;
        Rectangle Grip_bottomRight;
        Rectangle Grip_topCenter;
        Rectangle Grip_bottomCenter;
        Rectangle Grip_leftCenter;
        Rectangle Grip_rightCenter;

        bool isMoving_Grip_topCenter = false;
        bool isMoving_Grip_padding_left = false;
        bool isMoving_Grip_borderWidth_left = false;
        bool isMoving_Grip_padding_right = false;
        bool isMoving_Grip_borderWidth_right = false;
        bool isMoving_Grip_padding_bottom = false;
        bool isMoving_Grip_borderWidth_bottom = false;

        #endregion

        #region ControlBox

        private readonly ButtonR _CloseBtn;
        private readonly ButtonR _MinBtn;
        private readonly ButtonR _MaxBtn;

        private int BtnHeight;
        private int BtnWidth;

        #endregion

        #region Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = "New window";

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor != value)
                {
                    base.BackColor = value;
                    _CloseBtn.BackColor = value;
                    _MinBtn.BackColor = value;
                    _MaxBtn.BackColor = value;
                    Refresh();
                }
            }
        }


        private bool _colorGradient = true;
        public bool ColorGradient
        {
            get => _colorGradient;
            set
            {
                if (_colorGradient != value)
                {
                    _colorGradient = value;
                    Refresh();
                }
            }
        }


        private bool _active = true;
        public bool Active
        {
            get => _active;
            set
            {
                if (_active != value)
                {
                    _active = value;
                    Refresh();
                }
            }
        }


        private Color _colorBorder = SystemColors.ActiveBorder;
        public Color ColorBorder
        {
            get { return _colorBorder; }
            set
            {
                if (_colorBorder != value)
                {
                    _colorBorder = value;
                    Refresh();
                }
            }
        }


        private bool _flat = false;
        public bool Flat
        {
            get { return _flat; }
            set
            {
                if (_flat != value)
                {
                    _flat = value;
                    Refresh();
                }
            }
        }


        private Color _Color1 = SystemColors.ActiveCaption;
        public Color Color1
        {
            get
            {
                return _Color1;
            }
            set
            {
                if (_Color1 != value)
                {
                    _Color1 = value;
                    Refresh();
                }
            }
        }


        private Color _Color2 = SystemColors.GradientActiveCaption;
        public Color Color2
        {
            get
            {
                return _Color2;
            }
            set
            {
                if (_Color2 != value)
                {
                    _Color2 = value;
                    Refresh();
                }
            }
        }


        private Color _ButtonShadow = SystemColors.ButtonShadow;
        public Color ButtonShadow
        {
            get => _ButtonShadow;
            set
            {
                if (_ButtonShadow != value)
                {
                    _ButtonShadow = value;
                    _CloseBtn.ButtonShadow = value;
                    _MinBtn.ButtonShadow = value;
                    _MaxBtn.ButtonShadow = value;
                    Refresh();
                }
            }
        }


        private Color _ButtonDkShadow = SystemColors.ControlDark;
        public Color ButtonDkShadow
        {
            get => _ButtonDkShadow;
            set
            {
                if (_ButtonDkShadow != value)
                {
                    _ButtonDkShadow = value;
                    _CloseBtn.ButtonDkShadow = value;
                    _MinBtn.ButtonDkShadow = value;
                    _MaxBtn.ButtonDkShadow = value;
                    Refresh();
                }
            }
        }


        private Color _ButtonHilight = SystemColors.ButtonHighlight;
        public Color ButtonHilight
        {
            get => _ButtonHilight;
            set
            {
                if (_ButtonHilight != value)
                {
                    _ButtonHilight = value;
                    _CloseBtn.ButtonHilight = value;
                    _MinBtn.ButtonHilight = value;
                    _MaxBtn.ButtonHilight = value;
                    Refresh();
                }
            }
        }


        private Color _ButtonLight = SystemColors.ControlLight;
        public Color ButtonLight
        {
            get => _ButtonLight;
            set
            {
                if (_ButtonLight != value)
                {
                    _ButtonLight = value;
                    _CloseBtn.ButtonLight = value;
                    _MinBtn.ButtonLight = value;
                    _MaxBtn.ButtonLight = value;
                    Refresh();
                }
            }
        }


        private Color _ButtonText = SystemColors.ControlText;
        public Color ButtonText
        {
            get => _ButtonText;
            set
            {
                if (_ButtonText != value)
                {
                    _ButtonText = value;
                    _CloseBtn.ForeColor = value;
                    _MinBtn.ForeColor = value;
                    _MaxBtn.ForeColor = value;
                    Refresh();
                }
            }
        }


        private int min_Metrics_CaptionHeight = 16;
        private int max_Metrics_CaptionHeight = 50;

        private int _Metrics_CaptionHeight = SystemInformation.CaptionHeight;
        public int Metrics_CaptionHeight
        {
            get => _Metrics_CaptionHeight;

            set
            {
                if (value < min_Metrics_CaptionHeight) value = min_Metrics_CaptionHeight;
                if (value > max_Metrics_CaptionHeight) value = max_Metrics_CaptionHeight;

                if (value != _Metrics_CaptionHeight)
                {
                    _Metrics_CaptionHeight = value;
                    AdjustButtonSizes();
                    AdjustControlBoxFontsSizes();
                    AdjustLocations();
                    AdjustPadding();
                    Refresh();

                    if (!DesignMode && EnableEditingMetrics && isMoving_Grip_topCenter) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_CaptionHeight)));
                }
            }
        }


        private int min_Metrics_CaptionWidth = 16;
        private int max_Metrics_CaptionWidth = 50;

        private int _Metrics_CaptionWidth = 22;
        public int Metrics_CaptionWidth
        {
            get => _Metrics_CaptionWidth;
            set
            {
                if (value < min_Metrics_CaptionWidth) value = min_Metrics_CaptionWidth;
                if (value > max_Metrics_CaptionWidth) value = max_Metrics_CaptionWidth;
                if (value != _Metrics_CaptionWidth)
                {
                    _Metrics_CaptionWidth = value;
                    AdjustButtonSizes();
                    AdjustLocations();
                    AdjustControlBoxFontsSizes();
                    Refresh();

                    //if (!DesignMode && EnableEditingMetrics && isMoving_Grip_topCenter) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_CaptionHeight)));
                }
            }
        }


        private int min_Metrics_BorderWidth = 0;
        private int max_Metrics_BorderWidth = 50;

        private int _Metrics_BorderWidth = 1;
        public int Metrics_BorderWidth
        {
            get => _Metrics_BorderWidth;
            set
            {
                if (value < min_Metrics_BorderWidth) value = min_Metrics_BorderWidth;
                if (value > max_Metrics_BorderWidth) value = max_Metrics_BorderWidth;
                if (value != _Metrics_BorderWidth)
                {
                    _Metrics_BorderWidth = value;
                    AdjustLocations();
                    AdjustPadding();
                    Refresh();

                    if (!DesignMode && EnableEditingMetrics && (isMoving_Grip_borderWidth_left || isMoving_Grip_borderWidth_right || isMoving_Grip_borderWidth_bottom))
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_BorderWidth)));
                }
            }
        }


        private int min_Metrics_PaddedBorderWidth = 0;
        private int max_Metrics_PaddedBorderWidth = 50;

        private int _Metrics_PaddedBorderWidth = 4;
        public int Metrics_PaddedBorderWidth
        {
            get => _Metrics_PaddedBorderWidth;
            set
            {
                if (value < min_Metrics_PaddedBorderWidth) value = min_Metrics_PaddedBorderWidth;
                if (value > max_Metrics_PaddedBorderWidth) value = max_Metrics_PaddedBorderWidth;

                if (value != _Metrics_PaddedBorderWidth)
                {
                    _Metrics_PaddedBorderWidth = value;
                    AdjustLocations();
                    AdjustPadding();
                    Refresh();

                    if (!DesignMode && EnableEditingMetrics && (isMoving_Grip_padding_right || isMoving_Grip_padding_left || isMoving_Grip_padding_bottom))
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_PaddedBorderWidth)));
                }
            }
        }


        private bool _ControlBox = true;
        public bool ControlBox
        {
            get => _ControlBox;
            set
            {
                if (_ControlBox != value)
                {
                    _ControlBox = value;
                    _CloseBtn.Visible = value;
                    _MinBtn.Visible = value && _MinimizeBox;
                    _MaxBtn.Visible = value && _MaximizeBox;
                    AdjustLocations();
                    Refresh();
                }
            }
        }


        private bool _MinimizeBox = true;
        public bool MinimizeBox
        {
            get => _MinimizeBox;
            set
            {
                if (_MinimizeBox != value)
                {
                    _MinimizeBox = value;
                    _MinBtn.Visible = _ControlBox && value;
                    AdjustLocations();
                    Refresh();
                }
            }
        }


        private bool _MaximizeBox = true;
        public bool MaximizeBox
        {
            get => _MaximizeBox;
            set
            {
                if (_MaximizeBox != value)
                {
                    _MaximizeBox = value;
                    _MaxBtn.Visible = _ControlBox && value;
                    AdjustLocations();
                    Refresh();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;

        #endregion

        #region Events/Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        protected override void OnFontChanged(EventArgs e)
        {
            try
            {
                titleHeight = PreviewHelpers.GetTitlebarTextHeight(Font);
                AdjustControlBoxFontsSizes();
                AdjustButtonSizes();
                AdjustLocations();
                AdjustPadding();
            }
            catch { }

            base.OnFontChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            AdjustLocations();
            AdjustPadding();

            base.OnSizeChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (TitlebarRect.Contains(e.Location) && e.Button == MouseButtons.Left)
                {
                    oldPoint = MousePosition - new Size(Location);
                    newPoint = oldPoint;
                }
            }

            if (!DesignMode && _MetricsEdit_Grip)
            {
                if (Grip_topCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = true;
                    isMoving_Grip_padding_left = false;
                }
                else if (Grip_leftCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;

                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;
                }
                else if (Grip_rightCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;

                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;
                }
                else if (Grip_bottomCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;

                    isMoving_Grip_padding_bottom = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_bottom = e.Button != MouseButtons.Right;
                }
                else if (Grip_bottomLeft.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;

                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;

                    isMoving_Grip_padding_bottom = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_bottom = e.Button != MouseButtons.Right;
                }
                else if (Grip_bottomRight.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;

                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;

                    isMoving_Grip_padding_bottom = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_bottom = e.Button != MouseButtons.Right;
                }
                else if (Grip_topLeft.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = true;

                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;

                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;
                }
                else if (Grip_topRight.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = true;

                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;

                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.Button == MouseButtons.Left && TitlebarRect.Contains(e.Location) && !isMoving_Grip_topCenter)
                {
                    newPoint = MousePosition - new Size(oldPoint);
                    Location = newPoint;
                }

                else if (EnableEditingColors)
                {
                    if (TitlebarTextRect.Contains(e.Location))
                    {
                        CursorOnTitlebarText = true;
                        CursorOnTitlebarColor1 = false;
                        CursorOnTitlebarColor2 = false;
                        CursorOnShadow = false;
                        CursorOnDkShadow = false;
                        CursorOnHilight = false;
                        CursorOnLight = false;
                        CursorOnBorder = false;
                        CursorOnFace = false;

                    }
                    else
                    {
                        CursorOnTitlebarText = false;
                        CursorOnTitlebarColor1 = r0.Contains(e.Location);
                        CursorOnTitlebarColor2 = r1.Contains(e.Location);

                        CursorOnShadow = (!Flat && (btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location)))
                            || (Flat && (Rect.BordersContains(e.Location)));

                        CursorOnDkShadow = btnDkShadowPoints0.Contains(e.Location) || btnDkShadowPoints1.Contains(e.Location);
                        CursorOnHilight = btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location);
                        CursorOnLight = btnLightPoints0.Contains(e.Location) || btnLightPoints1.Contains(e.Location);
                        CursorOnBorder = Border.BordersContains(e.Location);
                        CursorOnFace = Border.Contains(e.Location) && !CursorOnBorder && !TitlebarRect.Contains(e.Location);
                    }

                    Refresh();
                }

                else if (EnableEditingMetrics)
                {
                    CursorOnTitlebarText = TitlebarTextRect.Contains(e.Location);

                    if (isMoving_Grip_topCenter)
                    {
                        Metrics_CaptionHeight = e.Location.Y - (_Metrics_PaddedBorderWidth + _Metrics_BorderWidth + (int)(GripSize * 0.5));
                    }

                    if (isMoving_Grip_padding_left)
                    {
                        Metrics_PaddedBorderWidth = (e.Location.X - (int)(GripSize * 0.5)) - _Metrics_BorderWidth;
                    }
                    else if (isMoving_Grip_borderWidth_left)
                    {
                        Metrics_BorderWidth = (e.Location.X - (int)(GripSize * 0.5)) - _Metrics_PaddedBorderWidth;
                    }
                    else if (isMoving_Grip_padding_right)
                    {
                        Metrics_PaddedBorderWidth = (Width - e.Location.X - (int)(GripSize * 0.5)) - _Metrics_BorderWidth;
                    }
                    else if (isMoving_Grip_borderWidth_right)
                    {
                        Metrics_BorderWidth = (Width - e.Location.X - (int)(GripSize * 0.5)) - _Metrics_PaddedBorderWidth;
                    }
                    else if (isMoving_Grip_padding_bottom)
                    {
                        Metrics_PaddedBorderWidth = (Height - e.Location.Y - (int)(GripSize * 0.5)) - _Metrics_BorderWidth;
                    }
                    else if (isMoving_Grip_borderWidth_bottom)
                    {
                        Metrics_BorderWidth = (Height - e.Location.Y - (int)(GripSize * 0.5)) - _Metrics_PaddedBorderWidth;
                    }

                    else
                    {
                        CursorOnMetricsGrip = true;

                        if (Grip_topLeft.Contains(e.Location)) Cursor = Cursors.SizeNWSE;
                        else if (Grip_topRight.Contains(e.Location)) Cursor = Cursors.SizeNESW;
                        else if (Grip_bottomLeft.Contains(e.Location)) Cursor = Cursors.SizeNESW;
                        else if (Grip_bottomRight.Contains(e.Location)) Cursor = Cursors.SizeNWSE;
                        else if (Grip_topCenter.Contains(e.Location)) Cursor = Cursors.SizeNS;
                        else if (Grip_bottomCenter.Contains(e.Location)) Cursor = Cursors.SizeNS;
                        else if (Grip_leftCenter.Contains(e.Location)) Cursor = Cursors.SizeWE;
                        else if (Grip_rightCenter.Contains(e.Location)) Cursor = Cursors.SizeWE;
                        else Cursor = Cursors.Default;

                        Refresh();
                    }
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isMoving_Grip_topCenter = false;
            isMoving_Grip_padding_left = false;
            isMoving_Grip_borderWidth_left = false;
            isMoving_Grip_padding_right = false;
            isMoving_Grip_borderWidth_right = false;
            isMoving_Grip_padding_bottom = false;
            isMoving_Grip_borderWidth_bottom = false;

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnTitlebarText = false;
                CursorOnTitlebarColor1 = false;
                CursorOnTitlebarColor2 = false;
                CursorOnShadow = false;
                CursorOnDkShadow = false;
                CursorOnHilight = false;
                CursorOnLight = false;
                CursorOnBorder = false;
                CursorOnFace = false;

                Refresh();

                Cursor = Cursors.Default;
            }

            if (!DesignMode && EnableEditingMetrics)
            {
                CursorOnMetricsGrip = false;
                CursorOnTitlebarText = false;
                isMoving_Grip_topCenter = false;
                isMoving_Grip_padding_left = false;
                isMoving_Grip_borderWidth_left = false;
                isMoving_Grip_padding_right = false;
                isMoving_Grip_borderWidth_right = false;
                isMoving_Grip_padding_bottom = false;
                isMoving_Grip_borderWidth_bottom = false;

                Cursor = Cursors.Default;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!DesignMode && oldPoint == newPoint && EnableEditingColors)
            {
                if (CursorOnTitlebarColor1) EditorInvoker?.Invoke(this, new EditorEventArgs(Active ? nameof(Templates.RetroDesktopColors.ActiveTitle) : nameof(Templates.RetroDesktopColors.InactiveTitle)));
                else if (CursorOnTitlebarColor2) EditorInvoker?.Invoke(this, new EditorEventArgs(Active ? nameof(Templates.RetroDesktopColors.GradientActiveTitle) : nameof(Templates.RetroDesktopColors.GradientInactiveTitle)));
                else if (CursorOnTitlebarColor2) EditorInvoker?.Invoke(this, new EditorEventArgs(Active ? nameof(Templates.RetroDesktopColors.GradientActiveTitle) : nameof(Templates.RetroDesktopColors.GradientInactiveTitle)));
                else if (CursorOnTitlebarText) EditorInvoker?.Invoke(this, new EditorEventArgs(Active ? nameof(Templates.RetroDesktopColors.TitleText) : nameof(Templates.RetroDesktopColors.InactiveTitleText)));
                else if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonShadow)));
                else if (CursorOnDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonDkShadow)));
                else if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonHilight)));
                else if (CursorOnLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonLight)));
                else if (CursorOnBorder) EditorInvoker?.Invoke(this, new EditorEventArgs(Active ? nameof(Templates.RetroDesktopColors.ActiveBorder) : nameof(Templates.RetroDesktopColors.InactiveBorder)));
                else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonFace)));
            }

            else if (!DesignMode && _MetricsEdit_CaptionFont)
            {
                using (FontDialog fd = new() { Font = this.Font })
                {
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        Font = fd.Font;
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Font)));
                    }
                }
            }

            base.OnMouseClick(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _CloseBtn?.Dispose();
            _MinBtn?.Dispose();
            _MaxBtn?.Dispose();
        }

        #endregion

        #region Methods

        private void AdjustButtonSizes()
        {
            BtnHeight = Conversions.ToInteger(Math.Max(_Metrics_CaptionHeight + PreviewHelpers.GetTitlebarTextHeight(Font) - 4, 5));
            BtnWidth = Math.Max(_Metrics_CaptionWidth - 2, 5);

            if (_CloseBtn != null) _CloseBtn.Size = new(BtnWidth, BtnHeight);
            if (_MinBtn != null) _MinBtn.Size = new(BtnWidth, BtnHeight);
            if (_MaxBtn != null) _MaxBtn.Size = new(BtnWidth, BtnHeight);
            Refresh();
        }

        private void AdjustLocations()
        {
            if (_CloseBtn != null) _CloseBtn.Top = Metrics_PaddedBorderWidth + Metrics_BorderWidth + 5;
            if (_MinBtn != null) _MinBtn.Top = _CloseBtn.Top;
            if (_MaxBtn != null) _MaxBtn.Top = _CloseBtn.Top;

            if (_CloseBtn != null) _CloseBtn.Left = Width - _CloseBtn.Width - _Metrics_PaddedBorderWidth - _Metrics_BorderWidth - 5;

            if (MinimizeBox & MaximizeBox)
            {
                if (_MinBtn != null && _CloseBtn != null) _MinBtn.Left = _CloseBtn.Left - 2 - _MinBtn.Width;
                if (_MaxBtn != null && _MinBtn != null) _MaxBtn.Left = _MinBtn.Left - _MaxBtn.Width;
            }

            else if (MaximizeBox)
            {
                if (_MaxBtn != null && _CloseBtn != null) _MaxBtn.Left = _CloseBtn.Left - 2 - _MaxBtn.Width;
            }

            else if (MinimizeBox)
            {
                if (_MinBtn != null && _CloseBtn != null) _MinBtn.Left = _CloseBtn.Left - 2 - _MinBtn.Width;
            }

        }

        public void AdjustControlBoxFontsSizes()
        {
            try
            {
                float i0, iFx;
                i0 = Math.Abs(Math.Min(_Metrics_CaptionHeight, _Metrics_CaptionWidth));
                iFx = i0 / Math.Abs(Math.Min(17, 18));
                Font f = new("Marlett", (float)(6.8d * (double)iFx));
                if (_CloseBtn != null) _CloseBtn.Font = f;
                if (_MinBtn != null) _MinBtn.Font = f;
                if (_MaxBtn != null) _MaxBtn.Font = f;
            }
            catch { }
        }

        public void AdjustPadding()
        {
            int iP = 3 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth;
            int iT = 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth + _Metrics_CaptionHeight + PreviewHelpers.GetTitlebarTextHeight(Font);
            Padding _Padding = new(iP, iT, iP, iP);
            Padding = _Padding;

            editingRect = new(Padding.Left, Padding.Top, Width - Padding.Left * 2 - 1, Height - Padding.Bottom - Padding.Top - 1);

            Grip_topLeft = new(editingRect.X - (int)(0.5 * GripSize), editingRect.Y - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_topRight = new(editingRect.X + editingRect.Width - (int)(0.5 * GripSize), editingRect.Y - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_bottomLeft = new(editingRect.X - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_bottomRight = new(editingRect.X + editingRect.Width - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_topCenter = new(editingRect.X + editingRect.Width / 2 - (int)(0.5 * GripSize), editingRect.Y - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_bottomCenter = new(editingRect.X + editingRect.Width / 2 - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_leftCenter = new(editingRect.X - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height / 2 - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_rightCenter = new(editingRect.X + editingRect.Width - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height / 2 - (int)(0.5 * GripSize), GripSize, GripSize);

            int CompinedPadding = _Metrics_BorderWidth + _Metrics_PaddedBorderWidth + 3;
            TitlebarRect = new(CompinedPadding, CompinedPadding, Width - CompinedPadding * 2, _Metrics_CaptionHeight + titleHeight);

            SizeF textSize = Text.Measure(Font);
            int y = TitlebarRect.Y + (TitlebarRect.Height - (int)textSize.Height) / 2;
            TitlebarTextRect = new(TitlebarRect.X, y, (int)textSize.Width, (int)textSize.Height);

            r0 = new(TitlebarRect.X, TitlebarRect.Y, TitlebarRect.Width / 2, TitlebarRect.Height - 1);
            r1 = new(TitlebarRect.X + TitlebarRect.Width / 2, TitlebarRect.Y, TitlebarRect.Width / 2, TitlebarRect.Height - 1);

            Rect = new(0, 0, Width - 1, Height - 1);
            Border = new(2, 2, Width - 5, Height - 5);

            btnShadowPoints0 = new PointF[] { new Point(Rect.Width - 1, Rect.X + 1), new Point(Rect.Width - 1, Rect.Height - 1) };
            btnShadowPoints1 = new PointF[] { new Point(Rect.X + 1, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1) };

            btnDkShadowPoints0 = new PointF[] { new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height) };
            btnDkShadowPoints1 = new PointF[] { new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height) };

            btnHilightPoints0 = new PointF[] { new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 2, Rect.Y + 1) };
            btnHilightPoints1 = new PointF[] { new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 2) };

            btnLightPoints0 = new PointF[] { new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y) };
            btnLightPoints1 = new PointF[] { new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1) };
        }

        #endregion

        #region Colors editor

        private bool CursorOnTitlebarColor1, CursorOnTitlebarColor2, CursorOnTitlebarText, CursorOnShadow, CursorOnDkShadow, CursorOnHilight, CursorOnLight, CursorOnBorder, CursorOnFace;
        private bool _ColorEdit_TitlebarColor1 => EnableEditingColors && CursorOnTitlebarColor1;
        private bool _ColorEdit_TitlebarColor2 => EnableEditingColors && CursorOnTitlebarColor2;
        private bool _ColorEdit_TitlebarText => EnableEditingColors && CursorOnTitlebarText;
        private bool _ColorEdit_Shadow => EnableEditingColors && CursorOnShadow;
        private bool _ColorEdit_DkShadow => EnableEditingColors && CursorOnDkShadow;
        private bool _ColorEdit_Hilight => EnableEditingColors && CursorOnHilight;
        private bool _ColorEdit_Light => EnableEditingColors && CursorOnLight;
        private bool _ColorEdit_Border => EnableEditingColors && CursorOnBorder;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;

        private bool CursorOnMetricsGrip;
        private bool _MetricsEdit_Grip => EnableEditingMetrics && CursorOnMetricsGrip;
        private bool _MetricsEdit_CaptionFont => EnableEditingMetrics && CursorOnTitlebarText;
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;

            G.Clear(BackColor);

            #region Editor

            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, Rect); }
            }

            #endregion

            using (Pen btnShadow = new(ButtonShadow))
            using (Pen btnHilight = new(ButtonHilight))
            using (Pen btnLight = new(ButtonLight))
            using (Pen btnDkShadow = new(ButtonDkShadow))
            using (Pen btnFace = new(BackColor))
            using (Pen clrBorder = new(ColorBorder))
            {
                if (!Flat)
                {
                    G.DrawLine(btnShadow, new Point(Rect.Width - 1, Rect.X + 1), new Point(Rect.Width - 1, Rect.Height - 1));
                    G.DrawLine(btnShadow, new Point(Rect.X + 1, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1));

                    G.DrawLine(btnHilight, new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 2, Rect.Y + 1));
                    G.DrawLine(btnHilight, new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 2));

                    G.DrawLine(btnLight, new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y));
                    G.DrawLine(btnLight, new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1));

                    G.DrawLine(btnDkShadow, new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height));
                    G.DrawLine(btnDkShadow, new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height));

                    G.DrawRectangle(btnFace, new Rectangle(2, 2, Width - 5, Height - 5));

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
                    G.DrawRectangle(btnShadow, Rect);

                    #region Editor

                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color)) { G.DrawRectangle(P, Rect); }
                    }

                    #endregion
                }

                G.DrawRectangle(clrBorder, Border);

                #region Editor

                if (_ColorEdit_Border)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color)) { G.DrawRectangle(P, Border); }
                }

                #endregion
            }

            bool RTL = (int)RightToLeft == 1;

            if (ColorGradient)
            {
                using (LinearGradientBrush gr = new(TitlebarRect, RTL ? Color2 : Color1, RTL ? Color1 : Color2, LinearGradientMode.Horizontal))
                using (SolidBrush fixer = new(RTL ? Color2 : Color1))
                {
                    Rectangle TRectFixer = new(TitlebarRect.X, TitlebarRect.Y, 1, TitlebarRect.Height);

                    G.FillRectangle(gr, TitlebarRect);
                    G.FillRectangle(fixer, TRectFixer);

                    if (_ColorEdit_TitlebarColor1)
                    {
                        Color color = Color.FromArgb(128, Color1.IsDark() ? Color.White : Color.Black);
                        using (Pen P = new(color))
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                        {
                            G.FillRectangle(hb, r0);
                            G.DrawRectangle(P, r0);
                        }
                    }

                    else if (_ColorEdit_TitlebarColor2)
                    {
                        Color color = Color.FromArgb(128, Color1.IsDark() ? Color.White : Color.Black);
                        using (Pen P = new(color))
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                        {
                            G.FillRectangle(hb, r1);
                            G.DrawRectangle(P, r1);
                        }
                    }
                }
            }
            else
            {
                using (SolidBrush br = new(Color1)) { G.FillRectangle(br, TitlebarRect); }

                if (_ColorEdit_TitlebarColor1 || _ColorEdit_TitlebarColor2)
                {
                    Color color = Color1.IsDark() ? Color.White : Color.Black;
                    using (Pen P = new(color))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                    {
                        G.FillRectangle(hb, TitlebarRect);
                        G.DrawRectangle(P, TitlebarRect);
                    }
                }
            }

            using (SolidBrush br = new(ForeColor))
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat(RTL))
            {
                if (_ColorEdit_TitlebarText || _MetricsEdit_CaptionFont)
                {
                    Color color = Color.FromArgb(128, Color1.IsDark() ? Color.White : Color.Black);
                    using (Pen P = new(color))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                    {
                        G.FillRectangle(hb, TitlebarTextRect);
                        G.DrawRectangle(P, TitlebarTextRect);
                    }
                }

                G.DrawString(Text, Font, br, TitlebarRect, sf);
            }

            if (!DesignMode && _MetricsEdit_Grip)
            {
                using (Pen P = new(BackColor.Invert()) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(P, editingRect);
                }

                using (Pen P = new(BackColor.Invert()) { DashStyle = DashStyle.Solid })
                {
                    G.FillRoundedRect(Brushes.White, Grip_topLeft, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_topRight, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_bottomLeft, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_bottomRight, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_topCenter, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_bottomCenter, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_leftCenter, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_rightCenter, 1, true);

                    G.DrawRoundedRect(P, Grip_topLeft, 1, true);
                    G.DrawRoundedRect(P, Grip_topRight, 1, true);
                    G.DrawRoundedRect(P, Grip_bottomLeft, 1, true);
                    G.DrawRoundedRect(P, Grip_bottomRight, 1, true);
                    G.DrawRoundedRect(P, Grip_topCenter, 1, true);
                    G.DrawRoundedRect(P, Grip_bottomCenter, 1, true);
                    G.DrawRoundedRect(P, Grip_leftCenter, 1, true);
                    G.DrawRoundedRect(P, Grip_rightCenter, 1, true);
                }
            }
        }
    }
}