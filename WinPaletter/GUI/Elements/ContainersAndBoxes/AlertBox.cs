using System;
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
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9f);
            Size = new Size(200, 40);
            CenterText = false;
            CustomColor = Color.FromArgb(0, 81, 210);
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
            get
            {
                return _alertStyle;
            }
            set
            {
                _alertStyle = value;
                Invalidate();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;
            DoubleBuffered = true;
            bool RTL = (int)RightToLeft == 1;
            bool DM = Program.Style.DarkMode;

            switch (_alertStyle)
            {
                case Style.Simple:
                    {
                        if (DM)
                        {
                            borderColor = Color.FromArgb(60, 60, 60);
                            innerColor = Color.FromArgb(50, 50, 50);
                            textColor = Color.FromArgb(150, 150, 150);
                        }
                        else
                        {
                            borderColor = Color.FromArgb(190, 190, 190);
                            innerColor = Color.FromArgb(150, 150, 150);
                            textColor = Color.FromArgb(250, 250, 250);
                        }

                        break;
                    }

                case Style.Success:
                    {
                        if (DM)
                        {
                            borderColor = Color.FromArgb(60, 98, 79);
                            innerColor = Color.FromArgb(60, 85, 79);
                            textColor = Color.FromArgb(35, 169, 110);
                        }
                        else
                        {
                            borderColor = Color.FromArgb(160, 198, 179);
                            innerColor = Color.FromArgb(140, 170, 155);
                            textColor = Color.FromArgb(135, 255, 210);
                        }

                        break;
                    }

                case Style.Notice:
                    {
                        if (DM)
                        {
                            borderColor = Color.FromArgb(70, 91, 107);
                            innerColor = Color.FromArgb(70, 91, 94);
                            textColor = Color.FromArgb(97, 185, 186);
                        }
                        else
                        {
                            borderColor = Color.FromArgb(170, 191, 207);
                            innerColor = Color.FromArgb(130, 155, 155);
                            textColor = Color.FromArgb(180, 255, 255);
                        }

                        break;
                    }

                case Style.Warning:
                    {
                        if (DM)
                        {
                            borderColor = Color.FromArgb(202, 41, 56);
                            innerColor = Color.FromArgb(125, 20, 30);
                            textColor = Color.FromArgb(254, 142, 122);
                        }
                        else
                        {
                            borderColor = Color.FromArgb(200, 171, 171);
                            innerColor = Color.FromArgb(150, 75, 75);
                            textColor = Color.FromArgb(255, 175, 175);
                        }

                        break;
                    }

                case Style.Information:
                    {
                        if (DM)
                        {
                            borderColor = Color.FromArgb(133, 133, 71);
                            innerColor = Color.FromArgb(120, 120, 71);
                            textColor = Color.FromArgb(254, 224, 122);
                        }
                        else
                        {
                            borderColor = Color.FromArgb(233, 233, 171);
                            innerColor = Color.FromArgb(195, 195, 150);
                            textColor = Color.FromArgb(250, 250, 150);
                        }

                        break;
                    }


                case Style.Indigo:
                    {
                        if (DM)
                        {
                            borderColor = Color.FromArgb(65, 0, 170);
                            innerColor = Color.FromArgb(60, 0, 140);
                            textColor = Color.FromArgb(140, 0, 255).CB(0.35f);
                        }
                        else
                        {
                            borderColor = Color.FromArgb(165, 0, 225);
                            innerColor = Color.FromArgb(129, 0, 200);
                            textColor = Color.FromArgb(210, 110, 255);
                        }

                        break;
                    }

                case Style.Custom:
                    {

                        if (DM)
                        {
                            borderColor = CustomColor.CB(0.03f);
                            innerColor = CustomColor.CB(0.01f);
                            textColor = CustomColor.LightLight();
                        }
                        else
                        {
                            borderColor = CustomColor.CB(0.3f);
                            innerColor = CustomColor.CB(0.1f);
                            textColor = CustomColor.CB(0.7f);
                        }

                        break;
                    }

                case Style.Adaptive:
                    {
                        if (Image is not null)
                        {
                            var cc = Image.AverageColor();

                            if (DM)
                            {
                                borderColor = cc.Light(0.01f);
                                innerColor = cc.Dark(0.05f);
                                textColor = cc.LightLight();
                            }
                            else
                            {
                                borderColor = cc.Light(1f);
                                innerColor = cc.LightLight().CB(0.35f);
                                textColor = cc;
                            }
                        }

                        else if (DM)
                        {
                            borderColor = CustomColor.CB(0.03f);
                            innerColor = CustomColor.CB(0.01f);
                            textColor = CustomColor.LightLight();
                        }
                        else
                        {
                            borderColor = CustomColor.CB(0.3f);
                            innerColor = CustomColor.CB(0.1f);
                            textColor = CustomColor.CB(0.7f);
                        }

                        break;
                    }

            }

            G.Clear(this.GetParentColor());

            BackColor = innerColor;

            using (var br = new SolidBrush(innerColor))
            {
                G.FillRoundedRect(br, new Rectangle(0, 0, Width - 1, Height - 1));
            }
            using (var P = new Pen(borderColor))
            {
                G.DrawRoundedRect_LikeW11(P, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            int textY = (int)Math.Round((Height - Text.Measure(Font).Height) / 2f);
            int TextX = 5;

            if (Image is not null)
                G.DrawImage(Image, new Rectangle(!RTL ? 5 : Width - 5 - Image.Width, 5, Image.Width, Image.Height));

            if (!CenterText)
            {
                if (Image is null)
                {
                    using (var br = new SolidBrush(textColor))
                    {
                        G.DrawString(Text, Font, br, new Rectangle(TextX, 0, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat(RTL));
                    }
                }
                else if (!RTL)
                {
                    using (var br = new SolidBrush(textColor))
                    {
                        G.DrawString(Text, Font, br, new Rectangle(10 + Image.Width, 7, Width - (5 + Image.Width), Height - 10), ContentAlignment.TopLeft.ToStringFormat());
                    }
                }
                else
                {
                    using (var br = new SolidBrush(textColor))
                    {
                        G.DrawString(Text, Font, br, new Rectangle(0, 7, Width - (10 + Image.Width), Height - 10), ContentAlignment.TopLeft.ToStringFormat(RTL));
                    }
                }
            }
            else
            {
                using (var br = new SolidBrush(textColor))
                {
                    G.DrawString(Text, Font, br, new Rectangle(1, 0, Width, Height), ContentAlignment.MiddleCenter.ToStringFormat(RTL));
                }
            }

        }

    }

}