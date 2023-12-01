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

        public Image Image { get; set; }
        public Color CustomColor { get; set; }
        public bool CenterText { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;
            DoubleBuffered = true;
            bool RTL = (int)RightToLeft == 1;
            bool DarkMode = Program.Style.DarkMode;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme1 = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Config.Scheme scheme2 = Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;
            Config.Scheme scheme3 = Enabled ? Program.Style.Schemes.Tertiary : Program.Style.Schemes.Disabled;

            switch (_alertStyle)
            {
                case Style.Simple:
                    {
                        borderColor = scheme1.Colors.Line;
                        innerColor = scheme1.Colors.Back;
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
                            borderColor = colors.Line_Checked_Hover;
                            innerColor = colors.Back_Checked_Hover;
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
                                borderColor = colors.Line_Checked_Hover;
                                innerColor = colors.Back_Checked_Hover;
                                textColor = colors.ForeColor_Accent;
                            }

                            break;
                        }

                        using (Config.Colors_Collection colors = new(CustomColor, default, DarkMode))
                        {
                            borderColor = colors.Line_Checked_Hover;
                            innerColor = colors.Back_Checked_Hover;
                            textColor = colors.ForeColor_Accent;
                        }

                        break;
                    }
            }

            using (SolidBrush br = new(innerColor)) { G.FillRoundedRect(br, new Rectangle(0, 0, Width - 1, Height - 1)); }

            using (Pen P = new(borderColor)) { G.DrawRoundedRect_LikeW11(P, new Rectangle(0, 0, Width - 1, Height - 1)); }

            int TextX = 5;

            if (Image is not null) { G.DrawImage(Image, new Rectangle(!RTL ? 5 : Width - 5 - Image.Width, 5, Image.Width, Image.Height)); }

            if (!CenterText)
            {
                if (Image is null)
                {
                    using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat(RTL))
                    {
                        using (SolidBrush br = new(textColor))
                        {
                            G.DrawString(Text, Font, br, new Rectangle(TextX, 0, Width, Height), sf);
                        }
                    }
                }
                else if (!RTL)
                {
                    using (StringFormat sf = ContentAlignment.TopLeft.ToStringFormat())
                    {
                        using (SolidBrush br = new(textColor))
                        {
                            G.DrawString(Text, Font, br, new Rectangle(10 + Image.Width, 7, Width - (5 + Image.Width), Height - 10), sf);
                        }
                    }
                }
                else
                {
                    using (StringFormat sf = ContentAlignment.TopLeft.ToStringFormat(RTL))
                    {
                        using (SolidBrush br = new(textColor))
                        {
                            G.DrawString(Text, Font, br, new Rectangle(0, 7, Width - (10 + Image.Width), Height - 10), sf);
                        }
                    }
                }
            }
            else
            {
                using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat(RTL))
                {
                    using (SolidBrush br = new(textColor))
                    {
                        G.DrawString(Text, Font, br, new Rectangle(1, 0, Width, Height), sf);
                    }
                }
            }

            base.OnPaint(e);
        }
    }
}