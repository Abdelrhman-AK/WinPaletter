using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{

    [Description("Retro 3D Panel Preview with Windows 9x style")]
    public class Preview3D : Control
    {

        #region Properties

        public Color WindowFrame { get; set; } = Color.Black;
        public Color ButtonShadow { get; set; } = Color.FromArgb(128, 128, 128);
        public Color ButtonDkShadow { get; set; } = Color.Black;
        public Color ButtonHilight { get; set; } = Color.White;
        public Color ButtonLight { get; set; } = Color.FromArgb(192, 192, 192);
        public int LineSize { get; set; } = 6;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var B = new Bitmap(Width, Height);
            var G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = My.Env.RenderingHint;
            DoubleBuffered = true;

            // ################################################################################# Customizer
            var BrushDkShadow = new SolidBrush(ButtonDkShadow);
            var BrushShadow = new SolidBrush(ButtonShadow);
            var BrushHilight = new SolidBrush(ButtonHilight);
            var BrushLight = new SolidBrush(ButtonLight);

            var HilightTopRect = new Rectangle(LineSize, LineSize, Width - LineSize * 2, LineSize);
            var HilightLeftRect = new Rectangle(LineSize, LineSize, LineSize, Height - LineSize * 2);

            var LightTopRect = new Rectangle(LineSize * 2, LineSize * 2, Width - LineSize * 4, LineSize);
            var LightLeftRect = new Rectangle(LineSize * 2, LineSize * 2, LineSize, Height - LineSize * 4);

            var DkShadowRightRect = new Rectangle(Width - LineSize * 2, LineSize, LineSize, Height - LineSize * 2);
            var DkShadowBottomRect = new Rectangle(LineSize, Height - LineSize * 2, Width - LineSize * 2, LineSize);

            var ShadowRightRect = new Rectangle(Width - LineSize * 3, LineSize * 2, LineSize, Height - LineSize * 4);
            var ShadowBottomRect = new Rectangle(LineSize * 2, Height - LineSize * 3, Width - LineSize * 4, LineSize);

            var Filling = new Rectangle(LightLeftRect.Right, LightTopRect.Bottom, ShadowRightRect.Left - LightLeftRect.Right, ShadowBottomRect.Top - LightTopRect.Bottom);

            int tw = (int)Math.Round(Filling.Width / 2d);
            int th = (int)Math.Round(LineSize * 1.75d);
            var TextRect = new Rectangle((int)Math.Round(Filling.X + (Filling.Width - tw) / 2d), (int)Math.Round(Filling.Y + (Filling.Height - th) / 2d), tw, th);

            // #################################################################################

            G.Clear(WindowFrame);

            G.FillRectangle(BrushHilight, HilightTopRect);
            G.FillRectangle(BrushHilight, HilightLeftRect);

            G.FillRectangle(BrushLight, LightTopRect);
            G.FillRectangle(BrushLight, LightLeftRect);

            G.FillRectangle(BrushDkShadow, DkShadowRightRect);
            G.FillRectangle(BrushDkShadow, DkShadowBottomRect);

            G.FillRectangle(BrushShadow, ShadowRightRect);
            G.FillRectangle(BrushShadow, ShadowBottomRect);

            G.FillRectangle(new SolidBrush(BackColor), Filling);

            G.FillRectangle(new SolidBrush(ForeColor), TextRect);

            e.Graphics.DrawImage(B, new Point(0, 0));
            G.Dispose();
            B.Dispose();
        }

    }
}