﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.Templates;
using WinPaletter.UI.Retro;

namespace WinPaletter.UI.Simulation
{
    [Description("Simulated Windows desktop icons")]
    public class WinIcon : Panel
    {
        public WinIcon()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            SetGrips();
        }

        #region Variables

        int GripSize = 10;
        Rectangle rect;
        Rectangle editorRect;
        Rectangle sizingRect;
        Rectangle spacingRect;
        Rectangle LabelRect;

        bool _hovering = false;

        bool _sizing = false;
        bool _moving = false;
        Point point = Point.Empty;
        int oldIconSize = 0;
        int oldLeft = 0;
        int oldTop = 0;
        int oldWidth = 0;

        public WinIcon hockIcon = null;

        #endregion

        #region Properties

        public Color ColorText { get; set; } = Color.White;
        public Color ColorGlow { get; set; } = Color.FromArgb(50, 0, 0, 0);
        public Icon Icon { get; set; }

        private int _IconSize = 32;
        public int IconSize
        {
            get => _IconSize;
            set
            {
                if (value < 16) value = 16;
                if (value > 256) value = 256;

                if (value != _IconSize)
                {
                    _IconSize = value;

                    if (EnableEditingMetrics && _sizing)
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(IconSize)));

                    Refresh();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingSpacingV { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingSpacingH { get; set; } = false;

        #endregion

        #region Events\Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        protected override void OnSizeChanged(EventArgs e)
        {
            SetGrips();

            base.OnSizeChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                _hovering = true;
                SetGrips();

                if (_sizing)
                {
                    int w = MousePosition.X - point.X;
                    int h = MousePosition.Y - point.Y;
                    int x = Math.Min(w, h);
                    if (oldIconSize + x != IconSize) IconSize = oldIconSize + x;
                }
                else if (_moving)
                {
                    if (EnableEditingSpacingH)
                    {
                        if (16 * (oldWidth + MousePosition.X - point.X - (_IconSize + 15)) < 101 &&
                            16 * (oldWidth + MousePosition.X - point.X - (_IconSize + 15)) > 29)
                        {
                            Width = oldWidth + MousePosition.X - point.X;

                            if (MousePosition.X - point.X != 0)
                            {
                                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(DesktopIcons.IconSpacing_Horizontal)));
                            }
                        }
                    }

                    if (EnableEditingSpacingV)
                    {
                        if (oldTop + MousePosition.Y - point.Y - hockIcon.Bottom + 45 <= 101 &&
                            oldTop + MousePosition.Y - point.Y - hockIcon.Bottom + 45 >= 29)
                        {
                            Top = oldTop + MousePosition.Y - point.Y;

                            if (MousePosition.Y - point.Y != 0)
                            {
                                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(DesktopIcons.IconSpacing_Vertical)));
                            }
                        }
                    }
                }

                if (sizingRect.Contains(e.Location))
                {
                    Cursor = Cursors.SizeNWSE;
                }
                else if (spacingRect.Contains(e.Location) && (EnableEditingSpacingV || EnableEditingSpacingH))
                {
                    Cursor = Cursors.SizeAll;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

                CursorOnLabel = LabelRect.Contains(e.Location);

                Refresh();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                _sizing = sizingRect.Contains(e.Location);
                _moving = spacingRect.Contains(e.Location);
                point = MousePosition;
                oldIconSize = IconSize;
                oldLeft = Left;
                oldTop = Top;
                oldWidth = Width;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (EnableEditingMetrics)
            {
                _sizing = false;
                _moving = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (EnableEditingMetrics)
            {
                _hovering = false;
                _sizing = false;
                _moving = false;
                CursorOnLabel = false;
                Cursor = Cursors.Default;

                Refresh();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                if (LabelRect.Contains(e.Location))
                {
                    using (FontDialog fd = new() { Font = Font })
                    {
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            Font = fd.Font;
                            Refresh();
                            EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(DesktopIcons.IconFont)));
                        }
                    }
                }
            }
            base.OnMouseClick(e);
        }

        #endregion

        #region Methods

        private void SetGrips()
        {
            if (EnableEditingMetrics)
            {
                rect = new(0, 0, Width - 1, Height - 1);
                editorRect = new(rect.X + GripSize / 2, rect.Y + GripSize / 2, rect.Width - GripSize, rect.Height - GripSize);
                sizingRect = new(editorRect.X + editorRect.Width - (int)(GripSize * 0.5), editorRect.Y + editorRect.Height - (int)(GripSize * 0.5), GripSize, GripSize);
                spacingRect = new(editorRect.X + (editorRect.Width - GripSize) / 2, editorRect.Y + (editorRect.Height - GripSize) / 2, GripSize, GripSize);

                SizeF labelSize = Text.Measure(Font);
                Rectangle labelContainer = new(0, Height - 35, Width - 1, 30);
                LabelRect = new(labelContainer.X + (labelContainer.Width - (int)labelSize.Width) / 2, labelContainer.Y + (labelContainer.Height - (int)labelSize.Height) / 2, (int)labelSize.Width, (int)labelSize.Height);
                Refresh();
            }
        }

        #endregion

        #region Metrics edios

        bool CursorOnLabel = false;

        bool _MetricsEdit_Label => !DesignMode && EnableEditingMetrics && CursorOnLabel;

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
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Rectangle IconRect = new(0, 0, Width - 1, Height - 30);

            Rectangle LabelRectX = new(0, Height - 35, Width - 1, 30);
            Rectangle LabelRectShadow = new(1, Height - 34, Width - 1, 30);

            Rectangle IconRectX = new((int)Math.Round(IconRect.X + (IconRect.Width - _IconSize) / 2d), (int)Math.Round(IconRect.Y + (IconRect.Height - _IconSize) / 2d), _IconSize, _IconSize);

            if (Icon is not null)
            {
                Icon ico = new(Icon, _IconSize, _IconSize);
                G.DrawIcon(ico, IconRectX);
                ico.Dispose();
            }

            if (_MetricsEdit_Label)
            {
                Color color = Color.FromArgb(128, 255, 255, 255);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, LabelRect);
                    G.DrawRectangle(P, LabelRect);
                }
            }

            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                if (ColorGlow.A > 0)
                    G.DrawString(Text, Font, Brushes.Black, LabelRectShadow, sf);

                G.DrawGlowString(1, Text, Font, ColorText, ColorGlow, rect, LabelRectX, sf);
            }

            if (EnableEditingMetrics && _hovering)
            {
                using (Pen P = new(Color.Gray) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(P, editorRect);

                    G.FillRoundedRect(Brushes.White, sizingRect, 1, true);
                    G.DrawRoundedRect(Pens.Black, sizingRect, 1, true);

                    if (EnableEditingSpacingV || EnableEditingSpacingH)
                    {
                        G.FillRoundedRect(Brushes.White, spacingRect, 1, true);
                        G.DrawRoundedRect(Pens.Black, spacingRect, 1, true);
                    }
                }
            }

            base.OnPaint(e);
        }
    }
}