﻿using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{

    [Description("Retro ScrollBar with Windows 9x style")]
    public class ScrollBarR : Panel
    {

        public ScrollBarR()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(192, 192, 192);
            BorderStyle = BorderStyle.None;
        }

        #region Properties
        public Color ButtonHilight { get; set; } = Color.White;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;
            DoubleBuffered = true;

            // ################################################################################# Customizer
            Rectangle Rect = new(0, 0, Width - 1, Height - 1);
            // #################################################################################
            G.Clear(BackColor);
            HatchBrush b = new(HatchStyle.Percent50, ButtonHilight, BackColor);
            G.FillRectangle(b, Rect);
        }

    }

}