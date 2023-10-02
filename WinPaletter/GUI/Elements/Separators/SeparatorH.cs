﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Horizontal separator for WinPaletter UI")]
    public class SeparatorH : Control
    {

        public SeparatorH()
        {
            TabStop = false;
            DoubleBuffered = true;
            Text = "";
        }

        #region Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = "";

        public bool AlternativeLook { get; set; } = false;

        #endregion

        #region Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(Width, !AlternativeLook ? 1 : 2);
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            DoubleBuffered = true;
            base.OnPaint(e);

            // ################################################################################# Customizer
            Color IdleLine;

            if (Parent is not null)
            {

                if (My.Env.Style.DarkMode)
                {
                    if (!AlternativeLook)
                    {
                        IdleLine = Parent.BackColor.CB(0.1f);
                    }
                    else
                    {
                        IdleLine = Color.DarkRed;
                    }
                }
                else if (!AlternativeLook)
                {
                    IdleLine = Parent.BackColor.CB((float)-0.1d);
                }
                else
                {
                    IdleLine = Color.DarkRed;
                }
            }

            else if (My.Env.Style.DarkMode)
                IdleLine = Color.FromArgb(76, 76, 76);
            else
                IdleLine = Color.FromArgb(210, 210, 210);
            // ################################################################################# Customizer

            using (var C = new Pen(IdleLine, !AlternativeLook ? 1 : 2))
            {
                G.DrawLine(C, new Point(0, 0), new Point(Width, 0));
                G.DrawLine(C, new Point(0, 1), new Point(Width, 1));
            }
        }

    }

}