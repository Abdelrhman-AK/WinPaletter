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
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            TabStop = false;
            Text = string.Empty;
        }

        #region Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        public bool AlternativeLook { get; set; } = false;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cpar = base.CreateParams;
                if (!DesignMode && !AlternativeLook)
                {
                    cpar.ExStyle |= 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }
        #endregion

        #region Events

        protected override void OnResize(EventArgs e)
        {
            Size = new(Width, !AlternativeLook ? 1 : 2);

            base.OnResize(e);
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color Line = AlternativeLook ? Color.DarkRed : Color.FromArgb(128, scheme.Colors.Back_Hover);

            using (Pen C = new(Line, !AlternativeLook ? 1 : 2))
            {
                G.DrawLine(C, new Point(0, 0), new Point(Width, 0));
                G.DrawLine(C, new Point(0, 1), new Point(Width, 1));
            }

            base.OnPaint(e);
        }
    }
}