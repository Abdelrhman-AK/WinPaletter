﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("RadioButton, but with image for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class RadioImage : Control
    {

        public RadioImage()
        {
            Timer = new Timer() { Enabled = false, Interval = 1 };
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
            Text = "";
            MouseEnter += RadioImage_MouseEnter;
            MouseLeave += RadioImage_MouseLeave;
            HandleCreated += RadioImage_HandleCreated;
            HandleDestroyed += RadioImage_HandleDestroyed;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private bool AnimateOnClick = false;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties

        private bool _Checked;
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                try
                {
                    _Checked = value;

                    if (_Checked)
                    {
                        UncheckOthersOnChecked();
                    }

                    CheckedChanged?.Invoke(this);

                    Invalidate();
                }
                catch
                {
                }
            }
        }

        public Image Image { get; set; }
        public bool ShowText { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = "";

        public ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleCenter;
        #endregion

        #region Events

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!Checked && e.Data.GetData(typeof(Controllers.ColorItem).FullName) is Controllers.ColorItem)
            {
                e.Effect = DragDropEffects.None;
                Checked = true;
            }
            else
            {
                return;
            }
            base.OnDragOver(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            AnimateOnClick = true;
            Checked = true;
            State = MouseState.Down;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void RadioImage_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void RadioImage_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void RadioImage_HandleCreated(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Shown += Showed;
                    Parent.BackColorChanged += RefreshColorPalette;
                    Parent.VisibleChanged += RefreshColorPalette;
                    Parent.EnabledChanged += RefreshColorPalette;
                    VisibleChanged += RefreshColorPalette;
                    EnabledChanged += RefreshColorPalette;
                }
            }
            catch
            {
            }

            try
            {
                alpha = 0;
            }
            catch
            {
            }
        }

        private void RadioImage_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Shown -= Showed;
                    Parent.BackColorChanged -= RefreshColorPalette;
                    Parent.VisibleChanged -= RefreshColorPalette;
                    Parent.EnabledChanged -= RefreshColorPalette;
                    VisibleChanged -= RefreshColorPalette;
                    EnabledChanged -= RefreshColorPalette;
                }
            }
            catch
            {
            }
        }

        public void Showed(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void RefreshColorPalette(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Voids/Functions
        private void UncheckOthersOnChecked()
        {
            if (Parent is null)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (C != this && C is RadioImage)
                {
                    ((RadioImage)C).Checked = false;
                }
            }
        }

        #endregion

        #region Animator

        private int alpha;
        private readonly int Factor = 25;
        private Timer Timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                if (State == MouseState.Over)
                {
                    if (alpha + Factor <= 255)
                    {
                        alpha += Factor;
                    }
                    else if (alpha + Factor > 255)
                    {
                        alpha = 255;
                        Timer.Enabled = false;
                        Timer.Stop();
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
                }

                if (!(State == MouseState.Over))
                {
                    if (alpha - Factor >= 0)
                    {
                        alpha -= Factor;
                    }
                    else if (alpha - Factor < 0)
                    {
                        alpha = 0;
                        Timer.Enabled = false;
                        Timer.Stop();
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
                }
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                var G = e.Graphics;
                if (Parent is null)
                    return;
                G.Clear(this.GetParentColor());

                G = e.Graphics;
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
                DoubleBuffered = true;

                var MainRect = new Rectangle(0, 0, Width - 1, Height - 1);
                var MainRectInner = new Rectangle(1, 1, Width - 3, Height - 3);
                var TextRect = new Rectangle(5, 5, Width - 10, Height - 10);

                var CenterRect = new Rectangle();

                if (Image is not null)
                    CenterRect = new Rectangle((int)Math.Round(MainRect.X + (MainRect.Width - Image.Width) / 2d), (int)Math.Round(MainRect.Y + (MainRect.Height - Image.Height) / 2d), Image.Width, Image.Height);

                var bkC = _Checked ? My.Env.Style.Colors.Back_Checked : My.Env.Style.Colors.Back;
                var bkCC = Color.FromArgb(alpha, My.Env.Style.Colors.Back_Checked);

                using (var br = new SolidBrush(bkC))
                {
                    G.FillRoundedRect(br, MainRectInner);
                }
                using (var br = new SolidBrush(bkCC))
                {
                    G.FillRoundedRect(br, MainRect);
                }

                var lC = Color.FromArgb(255 - alpha, _Checked ? My.Env.Style.Colors.Border_Checked : My.Env.Style.Colors.Border);
                var lCC = Color.FromArgb(alpha, My.Env.Style.Colors.Border_Checked_Hover);

                using (var P = new Pen(lC))
                {
                    G.DrawRoundedRect_LikeW11(P, MainRectInner);
                }
                using (var P = new Pen(lCC))
                {
                    G.DrawRoundedRect_LikeW11(P, MainRect);
                }

                if (Image is not null)
                    G.DrawImage(Image, CenterRect);

                if (ShowText)
                {
                    using (var br = new SolidBrush(ForeColor))
                    {
                        G.DrawString(Text, Font, br, TextRect, TextAlign.ToStringFormat());
                    }
                }
            }
            catch
            {

            }
        }

    }

}