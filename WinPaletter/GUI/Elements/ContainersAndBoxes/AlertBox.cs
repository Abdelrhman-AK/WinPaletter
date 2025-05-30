﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("AlertBox for WinPaletter UI")]
    public class AlertBox : ContainerControl
    {
        public AlertBox()
        {
            TabStop = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 9f);
            Size = new(200, 40);
            CenterText = false;
            CustomColor = Color.FromArgb(0, 81, 210);
            BackColor = Color.Transparent;
        }

        #region Variables

        private Color borderColor, innerColor, textColor;

        public enum Style
        {
            Adaptive,
            Simple,
            Success,
            Notice,
            Warning,
            Information,
            Indigo,
            Custom
        }

        public enum Close
        {
            Yes,
            No
        }

        #endregion

        #region Properties

        private Style _alertStyle;
        public Style AlertStyle
        {
            get => _alertStyle;
            set
            {
                if (value != _alertStyle)
                {
                    _alertStyle = value;
                    Invalidate();
                }
            }
        }

        private Image _image;
        public new Image Image { get => _image; set => _image = value; }
        public Color CustomColor { get; set; }
        public bool CenterText { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Image?.Dispose();
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            bool RTL = (int)RightToLeft == 1;
            bool DarkMode = Program.Style.DarkMode;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme1 = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Config.Scheme scheme2 = Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;
            Config.Scheme scheme3 = Enabled ? Program.Style.Schemes.Tertiary : Program.Style.Schemes.Disabled;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);
            Rectangle innerRect = new(5, 5, Width - 11, Height - 11);

            switch (_alertStyle)
            {
                case Style.Simple:
                    {
                        borderColor = scheme1.Colors.Line_Hover(parentLevel + 4);
                        innerColor = scheme1.Colors.Back(parentLevel);
                        textColor = scheme1.Colors.ForeColor;

                        break;
                    }

                case Style.Success:
                    {
                        borderColor = scheme3.Colors.Line_Checked_Hover;
                        innerColor = scheme3.Colors.Back_Checked_Hover;
                        textColor = scheme3.Colors.ForeColor_Accent;
                        break;
                    }

                case Style.Notice:
                    {
                        using (Config.Colors_Collection colors = new(Color.FromArgb(115, 113, 6), default, DarkMode))
                        {
                            borderColor = colors.Line_Checked;
                            innerColor = colors.Back_Checked;
                            textColor = colors.ForeColor_Accent;
                        }

                        break;
                    }

                case Style.Warning:
                    {
                        borderColor = scheme2.Colors.Line_Checked;
                        innerColor = scheme2.Colors.Back_Checked;
                        textColor = scheme2.Colors.ForeColor_Accent;
                        break;
                    }

                case Style.Information:
                    {
                        borderColor = scheme1.Colors.Line_Checked;
                        innerColor = scheme1.Colors.Back_Checked;
                        textColor = scheme1.Colors.ForeColor_Accent;

                        break;
                    }

                case Style.Indigo:
                    {
                        using (Config.Colors_Collection colors = new(Color.FromArgb(76, 0, 142), default, DarkMode))
                        {
                            borderColor = colors.Line_Checked_Hover;
                            innerColor = colors.Back_Checked_Hover;
                            textColor = colors.ForeColor_Accent;
                        }

                        break;
                    }

                case Style.Custom:
                    {
                        using (Config.Colors_Collection colors = new(CustomColor, default, DarkMode))
                        {
                            borderColor = colors.Line_Checked_Hover;
                            innerColor = colors.Back_Checked_Hover;
                            textColor = colors.ForeColor_Accent;
                        }

                        break;
                    }

                case Style.Adaptive:
                    {
                        if (Image is not null)
                        {
                            using (Config.Colors_Collection colors = new(Image.AverageColor(), default, DarkMode))
                            {
                                borderColor = colors.Line_Checked;
                                innerColor = colors.Back_Checked;
                                textColor = colors.ForeColor_Accent;
                            }

                            break;
                        }

                        using (Config.Colors_Collection colors = new(CustomColor, default, DarkMode))
                        {
                            borderColor = colors.Line_Checked;
                            innerColor = colors.Back_Checked;
                            textColor = colors.ForeColor_Accent;
                        }

                        break;
                    }
            }

            using (SolidBrush br = new(innerColor)) { G.FillRoundedRect(br, rect); }

            using (Pen P = new(borderColor)) { G.DrawRoundedRect(P, rect); }

            using (StringFormat sf = !CenterText ? ContentAlignment.MiddleLeft.ToStringFormat(RTL) : ContentAlignment.MiddleCenter.ToStringFormat(RTL))
            {
                sf.FormatFlags = StringFormatFlags.LineLimit; // Limit to lines fitting layout rectangle
                sf.Trimming = StringTrimming.EllipsisCharacter; // Optional: add ellipsis if overflow
                float maxWidth = innerRect.Width;

                Image img = _image;
                SizeF imageSize = img?.Size ?? SizeF.Empty;
                SizeF textSize = G.MeasureString(Text, Font, new SizeF(maxWidth, float.MaxValue), sf);

                GraphicsExtensions.GetTextAndImageRectangles(innerRect, imageSize, textSize, ContentAlignment.TopLeft, !CenterText ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter, TextImageRelation.ImageBeforeText,
                    out RectangleF imageRect, out RectangleF textRect);

                if (img != null) G.DrawImage(img, Rectangle.Round(imageRect));

                using (SolidBrush br = new(textColor))
                {
                    G.DrawString(Text, Font, br, textRect, sf);
                }
            }

            base.OnPaint(e);
        }
    }
}