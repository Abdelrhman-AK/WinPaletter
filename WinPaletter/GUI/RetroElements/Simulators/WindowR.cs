﻿using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    [Description("Retro window with Windows 9x style")]
    public class WindowR : Panel
    {

        public WindowR()
        {
            Font = new("Microsoft Sans Serif", 8f);
            TitleHeight = PreviewHelpers.GetTitlebarTextHeight(Font);

            _CloseBtn = new() { Name = "CloseBtn", Text = "r", Font = new("Marlett", 7.8f), Size = new(BtnWidth, BtnHeight), TextAlign = ContentAlignment.MiddleCenter };
            _MinBtn = new() { Name = "MinBtn", Text = "1", Font = new("Marlett", 8f), Size = new(BtnWidth, BtnHeight), TextAlign = ContentAlignment.MiddleCenter };
            _MaxBtn = new() { Name = "MaxBtn", Text = "0", Font = new("Marlett", 8f), Size = new(BtnWidth, BtnHeight), TextAlign = ContentAlignment.MiddleCenter };
            BtnHeight = Metrics_CaptionHeight + TitleHeight - 4;
            BtnWidth = Metrics_CaptionWidth - 2;
            DoubleBuffered = true;
            BackColor = Color.FromArgb(192, 192, 192);
            ForeColor = Color.White;
            BorderStyle = BorderStyle.None;
            Text = "New Window";
            BackColorChanged += WindowR_BackColorChanged;
            HandleCreated += WindowR_HandleCreated;
            SizeChanged += WindowR_SizeChanged;
            FontChanged += WindowR_FontChanged;
        }

        #region Properties
        private int TitleHeight;

        public bool ColorGradient { get; set; } = true;
        public Color ColorBorder { get; set; } = Color.FromArgb(192, 192, 192);

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = "New window";

        public bool UseItAsMenu { get; set; } = false;
        public bool Flat { get; set; } = false;

        private Color _Color1 = Color.FromArgb(0, 0, 128);
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

        private Color _Color2 = Color.FromArgb(16, 132, 208);
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

        private Color _ButtonShadow = Color.FromArgb(128, 128, 128);
        public Color ButtonShadow
        {
            get
            {
                return _ButtonShadow;
            }
            set
            {
                _ButtonShadow = value;
                _CloseBtn.ButtonShadow = value;
                _MinBtn.ButtonShadow = value;
                _MaxBtn.ButtonShadow = value;
                // Refresh()
            }
        }

        private Color _ButtonDkShadow = Color.Black;
        public Color ButtonDkShadow
        {
            get
            {
                return _ButtonDkShadow;
            }
            set
            {
                _ButtonDkShadow = value;
                _CloseBtn.ButtonDkShadow = value;
                _MinBtn.ButtonDkShadow = value;
                _MaxBtn.ButtonDkShadow = value;
                // Refresh()
            }
        }

        private Color _ButtonHilight = Color.White;
        public Color ButtonHilight
        {
            get
            {
                return _ButtonHilight;
            }
            set
            {
                _ButtonHilight = value;
                _CloseBtn.ButtonHilight = value;
                _MinBtn.ButtonHilight = value;
                _MaxBtn.ButtonHilight = value;
                // Refresh()
            }
        }

        private Color _ButtonLight = Color.FromArgb(192, 192, 192);
        public Color ButtonLight
        {
            get
            {
                return _ButtonLight;
            }
            set
            {
                _ButtonLight = value;
                _CloseBtn.ButtonLight = value;
                _MinBtn.ButtonLight = value;
                _MaxBtn.ButtonLight = value;
                // Refresh()
            }
        }
        public Color ButtonFace { get; set; } = Color.FromArgb(192, 192, 192);

        private Color _ButtonText = Color.Black;
        public Color ButtonText
        {
            get
            {
                return _ButtonText;
            }
            set
            {
                _ButtonText = value;
                _CloseBtn.ForeColor = value;
                _MinBtn.ForeColor = value;
                _MaxBtn.ForeColor = value;
                // Refresh()
            }
        }

        private int _Metrics_CaptionHeight = 22;
        public int Metrics_CaptionHeight
        {
            get
            {
                return _Metrics_CaptionHeight;
            }

            set
            {
                _Metrics_CaptionHeight = value;
                AdjustButtonSizes();
                AdjustControlBoxFontsSizes();
                AdjustPadding();
                // Refresh()
            }
        }

        private int _Metrics_CaptionWidth = 22;
        public int Metrics_CaptionWidth
        {
            get
            {
                return _Metrics_CaptionWidth;
            }
            set
            {
                _Metrics_CaptionWidth = value;
                AdjustButtonSizes();
                AdjustLocations();
                AdjustControlBoxFontsSizes();
                // Refresh()
            }
        }

        private int _Metrics_BorderWidth = 1;
        public int Metrics_BorderWidth
        {
            get
            {
                return _Metrics_BorderWidth;
            }

            set
            {
                _Metrics_BorderWidth = value;
                AdjustLocations();
                AdjustPadding();
                // Refresh()
            }
        }

        private int _Metrics_PaddedBorderWidth = 4;
        public int Metrics_PaddedBorderWidth
        {
            get
            {
                return _Metrics_PaddedBorderWidth;
            }
            set
            {
                _Metrics_PaddedBorderWidth = value;
                AdjustLocations();
                AdjustPadding();
                // Refresh()
            }
        }

        private bool _ControlBox = true;
        public bool ControlBox
        {
            get
            {
                return _ControlBox;
            }
            set
            {
                _ControlBox = value;
                _CloseBtn.Visible = value;
                _MinBtn.Visible = value & _MinimizeBox;
                _MaxBtn.Visible = value & _MaximizeBox;
                AdjustLocations();
                // Refresh()
            }
        }

        private bool _MinimizeBox = true;
        public bool MinimizeBox
        {
            get
            {
                return _MinimizeBox;
            }
            set
            {
                _MinimizeBox = value;
                _MinBtn.Visible = value;
                AdjustLocations();
                // Refresh()
            }
        }

        private bool _MaximizeBox = true;
        public bool MaximizeBox
        {
            get
            {
                return _MaximizeBox;
            }
            set
            {
                _MaximizeBox = value;
                _MaxBtn.Visible = value;
                AdjustLocations();
                // Refresh()
            }
        }

        #endregion

        #region Events
        private void WindowR_BackColorChanged(object sender, EventArgs e)
        {
            _CloseBtn.BackColor = BackColor;
            _MinBtn.BackColor = BackColor;
            _MaxBtn.BackColor = BackColor;
        }

        private void WindowR_HandleCreated(object sender, EventArgs e)
        {
            if (!UseItAsMenu)
            {
                Controls.AddRange(new Control[] { _CloseBtn, _MaxBtn, _MinBtn });
                _CloseBtn.Visible = _ControlBox;
                _MinBtn.Visible = _ControlBox & _MinimizeBox;
                _MaxBtn.Visible = _ControlBox & _MaximizeBox;

                AdjustControlBoxFontsSizes();
                AdjustButtonSizes();
                AdjustLocations();
            }
        }

        private void WindowR_SizeChanged(object sender, EventArgs e)
        {
            AdjustLocations();
        }

        private void WindowR_FontChanged(object sender, EventArgs e)
        {
            TitleHeight = PreviewHelpers.GetTitlebarTextHeight(Font);
            AdjustControlBoxFontsSizes();
            AdjustButtonSizes();
            AdjustLocations();
        }
        #endregion

        #region Voids/Functions
        private void AdjustButtonSizes()
        {
            BtnHeight = Conversions.ToInteger(Math.Max(_Metrics_CaptionHeight + PreviewHelpers.GetTitlebarTextHeight(Font) - 4, 5));
            BtnWidth = Math.Max(_Metrics_CaptionWidth - 2, 5);

            _CloseBtn.Size = new(BtnWidth, BtnHeight);
            _MinBtn.Size = new(BtnWidth, BtnHeight);
            _MaxBtn.Size = new(BtnWidth, BtnHeight);
            Refresh();
        }

        private void AdjustLocations()
        {
            _CloseBtn.Top = Metrics_PaddedBorderWidth + Metrics_BorderWidth + 5;
            _MinBtn.Top = _CloseBtn.Top;
            _MaxBtn.Top = _CloseBtn.Top;

            _CloseBtn.Left = Width - _CloseBtn.Width - _Metrics_PaddedBorderWidth - _Metrics_BorderWidth - 5;

            if (MinimizeBox & MaximizeBox)
            {
                _MinBtn.Left = _CloseBtn.Left - 2 - _MinBtn.Width;
                _MaxBtn.Left = _MinBtn.Left - _MaxBtn.Width;
            }

            else if (MaximizeBox)
            {
                _MaxBtn.Left = _CloseBtn.Left - 2 - _MaxBtn.Width;
            }

            else if (MinimizeBox)
            {
                _MinBtn.Left = _CloseBtn.Left - 2 - _MinBtn.Width;

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
                _CloseBtn.Font = f;
                _MinBtn.Font = f;
                _MaxBtn.Font = f;
            }
            catch { }
        }

        public void AdjustPadding()
        {
            int iP = 3 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth;
            int iT = 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth + _Metrics_CaptionHeight + PreviewHelpers.GetTitlebarTextHeight(Font);
            Padding _Padding = new(iP, iT, iP, iP);
            Padding = _Padding;
        }

        #endregion

        #region ControlBox

        private readonly ButtonR _CloseBtn;
        private readonly ButtonR _MinBtn;
        private readonly ButtonR _MaxBtn;

        private int BtnHeight;
        private int BtnWidth;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;
            DoubleBuffered = true;

            // ################################################################################# Customizer
            Rectangle Rect = new(0, 0, Width - 1, Height - 1);

            int CompinedPadding = _Metrics_BorderWidth + _Metrics_PaddedBorderWidth + 3;

            Rectangle TRect = new(CompinedPadding, CompinedPadding, Width - CompinedPadding * 2, _Metrics_CaptionHeight + TitleHeight);

            Rectangle ARect = new(2, 2, Width - 5, Height - 5);
            // #################################################################################
            G.Clear(BackColor);

            if (!Flat)
            {
                G.DrawLine(new Pen(ButtonShadow), new Point(Rect.Width - 1, Rect.X + 1), new Point(Rect.Width - 1, Rect.Height - 1));
                G.DrawLine(new Pen(ButtonShadow), new Point(Rect.X + 1, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1));

                G.DrawLine(new Pen(ButtonHilight), new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 2, Rect.Y + 1));
                G.DrawLine(new Pen(ButtonHilight), new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 2));

                G.DrawLine(new Pen(ButtonLight), new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y));
                G.DrawLine(new Pen(ButtonLight), new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1));

                G.DrawLine(new Pen(ButtonDkShadow), new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height));
                G.DrawLine(new Pen(ButtonDkShadow), new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height));

                G.DrawRectangle(new Pen(ButtonFace), new Rectangle(2, 2, Width - 5, Height - 5));
            }
            else
            {
                G.DrawRectangle(new Pen(ButtonShadow), Rect);
            }

            if (!Flat & !UseItAsMenu)
                G.DrawRectangle(new Pen(ColorBorder), ARect);
            Font F;

            if (!UseItAsMenu)
            {
                if (G.TextRenderingHint == TextRenderingHint.SingleBitPerPixelGridFit)
                {
                    F = new("Microsoft Sans Serif", 8f, FontStyle.Bold);
                }
                else
                {
                    F = Font;
                }

                bool RTL = (int)RightToLeft == 1;

                try
                {
                    LinearGradientBrush gr = new(TRect, RTL ? Color2 : Color1, RTL ? Color1 : Color2, LinearGradientMode.Horizontal);
                    if (ColorGradient)
                    {
                        G.FillRectangle(gr, TRect);

                        Rectangle TRectFixer = new(TRect.X, TRect.Y, 1, TRect.Height);
                        G.FillRectangle(new SolidBrush(RTL ? Color2 : Color1), TRectFixer);
                    }

                    else
                    {
                        G.FillRectangle(new SolidBrush(Color1), TRect);
                    }
                }
                catch
                {
                }

                G.DrawString(Text, F, new SolidBrush(ForeColor), TRect, ContentAlignment.MiddleLeft.ToStringFormat(RTL));
            }

        }

    }

}