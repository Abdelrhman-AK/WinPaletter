﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{

    /// <summary>
    /// A magnified preview of a 3D panel with Windows 9x style.
    /// </summary>
    [Description("Retro 3D Panel Preview with Windows 9x style")]
    public class Preview3D : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Preview3D"/> class.
        /// </summary>
        public Preview3D()
        {
            DoubleBuffered = true;
        }

        #region Properties

        private Color windowFrame = Color.Black;
        private Color buttonShadow = Color.FromArgb(128, 128, 128);
        private Color buttonDkShadow = Color.Black;
        private Color buttonHilight = Color.White;
        private Color buttonLight = Color.FromArgb(192, 192, 192);
        private int lineSize = 6;

        /// <summary>
        /// BackColor property (button face color).
        /// </summary>
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor != value)
                {
                    base.BackColor = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Color of border of a focused 3D button.
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
        /// Color of the shadow of a 3D button.
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
        /// Color of the dark shadow of a 3D button.
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
        /// Color of the hilight of a 3D button.
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
        /// Color of the light of a 3D button.
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
        /// Size of the lines of the 3D button.
        /// </summary>
        public int LineSize
        {
            get { return lineSize; }
            set
            {
                if (lineSize != value)
                {
                    lineSize = value;
                    Refresh();
                }
            }
        }

        #endregion

        /// <summary>
        /// Paints the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // ################################################################################# Customizer
            SolidBrush BrushDkShadow = new(ButtonDkShadow);
            SolidBrush BrushShadow = new(ButtonShadow);
            SolidBrush BrushHilight = new(ButtonHilight);
            SolidBrush BrushLight = new(ButtonLight);

            Rectangle HilightTopRect = new(LineSize, LineSize, Width - LineSize * 2, LineSize);
            Rectangle HilightLeftRect = new(LineSize, LineSize, LineSize, Height - LineSize * 2);

            Rectangle LightTopRect = new(LineSize * 2, LineSize * 2, Width - LineSize * 4, LineSize);
            Rectangle LightLeftRect = new(LineSize * 2, LineSize * 2, LineSize, Height - LineSize * 4);

            Rectangle DkShadowRightRect = new(Width - LineSize * 2, LineSize, LineSize, Height - LineSize * 2);
            Rectangle DkShadowBottomRect = new(LineSize, Height - LineSize * 2, Width - LineSize * 2, LineSize);

            Rectangle ShadowRightRect = new(Width - LineSize * 3, LineSize * 2, LineSize, Height - LineSize * 4);
            Rectangle ShadowBottomRect = new(LineSize * 2, Height - LineSize * 3, Width - LineSize * 4, LineSize);

            Rectangle Filling = new(LightLeftRect.Right, LightTopRect.Bottom, ShadowRightRect.Left - LightLeftRect.Right, ShadowBottomRect.Top - LightTopRect.Bottom);

            int tw = (int)Math.Round(Filling.Width / 2d);
            int th = (int)Math.Round(LineSize * 1.75d);
            Rectangle TextRect = new((int)Math.Round(Filling.X + (Filling.Width - tw) / 2d), (int)Math.Round(Filling.Y + (Filling.Height - th) / 2d), tw, th);

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