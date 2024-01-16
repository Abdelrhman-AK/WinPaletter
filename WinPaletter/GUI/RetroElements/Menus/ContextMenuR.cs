using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    [Description("Retro context menu with Windows 9x style")]
    public class ContextMenuR : Panel
    {
        public ContextMenuR()
        {
            Font = new("Microsoft Sans Serif", 8f);
            DoubleBuffered = true;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            BorderStyle = BorderStyle.None;
            Text = string.Empty;

            RefreshItems();
        }

        #region Variables

        Rectangle Rect;
        Rectangle Border;

        private PointF[] btnShadowPoints0;
        private PointF[] btnShadowPoints1;
        private PointF[] btnDkShadowPoints0;
        private PointF[] btnDkShadowPoints1;
        private PointF[] btnHilightPoints0;
        private PointF[] btnHilightPoints1;
        private PointF[] btnLightPoints0;
        private PointF[] btnLightPoints1;

        string str0 = "Menu item";
        string str1 = "Selection";
        string str2 = "Disabled item";

        SizeF item0Size;
        SizeF item1Size;
        SizeF item2Size;

        Rectangle item0;
        Rectangle item1;
        Rectangle item2;

        Rectangle item0Text;
        Rectangle item1Text;
        Rectangle item2Text;
        #endregion

        #region Properties
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        public override Color BackColor
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

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor != value)
                {
                    base.ForeColor = value;
                    Refresh();
                }
            }
        }


        private Color _colorBorder = SystemColors.ActiveBorder;
        public Color ColorBorder
        {
            get { return _colorBorder; }
            set
            {
                if (_colorBorder != value)
                {
                    _colorBorder = value;
                    Refresh();
                }
            }
        }


        private bool _flat = false;
        public bool Flat
        {
            get { return _flat; }
            set
            {
                if (_flat != value)
                {
                    _flat = value;
                    Refresh();
                }
            }
        }


        private Color _ButtonShadow = SystemColors.ButtonShadow;
        public Color ButtonShadow
        {
            get
            {
                return _ButtonShadow;
            }
            set
            {
                _ButtonShadow = value;
                Refresh();
            }
        }


        private Color _ButtonDkShadow = SystemColors.ControlDark;
        public Color ButtonDkShadow
        {
            get
            {
                return _ButtonDkShadow;
            }
            set
            {
                _ButtonDkShadow = value;
                Refresh();
            }
        }


        private Color _ButtonHilight = SystemColors.ButtonHighlight;
        public Color ButtonHilight
        {
            get
            {
                return _ButtonHilight;
            }
            set
            {
                _ButtonHilight = value;
                Refresh();
            }
        }


        private Color _ButtonLight = SystemColors.ControlLight;
        public Color ButtonLight
        {
            get
            {
                return _ButtonLight;
            }
            set
            {
                _ButtonLight = value;
                Refresh();
            }
        }


        private Color _hilight = SystemColors.Highlight;
        public Color Hilight
        {
            get { return _hilight; }
            set
            {
                if (_hilight != value)
                {
                    _hilight = value;
                    Refresh();
                }
            }
        }


        private Color _hilightText = SystemColors.HighlightText;
        public Color HilightText
        {
            get { return _hilightText; }
            set
            {
                if (_hilightText != value)
                {
                    _hilightText = value;
                    Refresh();
                }
            }
        }


        private Color _menuHilight = SystemColors.MenuHighlight;
        public Color MenuHilight
        {
            get { return _menuHilight; }
            set
            {
                if (_menuHilight != value)
                {
                    _menuHilight = value;
                    Refresh();
                }
            }
        }


        private Color _grayText = SystemColors.GrayText;
        public Color GrayText
        {
            get { return _grayText; }
            set
            {
                if (_grayText != value)
                {
                    _grayText = value;
                    Refresh();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        protected override void OnFontChanged(EventArgs e)
        {
            try { RefreshItems(); } catch { }

            base.OnFontChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Rect = new(0, 0, Width - 1, Height - 1);
            Border = new(2, 2, Width - 5, Height - 5);

            btnShadowPoints0 = new PointF[] { new Point(Rect.Width - 1, Rect.X + 1), new Point(Rect.Width - 1, Rect.Height - 1) };
            btnShadowPoints1 = new PointF[] { new Point(Rect.X + 1, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1) };

            btnDkShadowPoints0 = new PointF[] { new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height) };
            btnDkShadowPoints1 = new PointF[] { new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height) };

            btnHilightPoints0 = new PointF[] { new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 2, Rect.Y + 1) };
            btnHilightPoints1 = new PointF[] { new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 2) };

            btnLightPoints0 = new PointF[] { new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y) };
            btnLightPoints1 = new PointF[] { new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1) };

            base.OnSizeChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnShadow = (!Flat && (btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location)))
                    || (Flat && (Rect.BordersContains(e.Location)));

                CursorOnDkShadow = btnDkShadowPoints0.Contains(e.Location) || btnDkShadowPoints1.Contains(e.Location);
                CursorOnHilight = btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location);
                CursorOnLight = btnLightPoints0.Contains(e.Location) || btnLightPoints1.Contains(e.Location);

                //Keep these orders as they are
                CursorOnSelectionText = item1Text.Contains(e.Location);
                CursorOnSelectionMenuHilight = !CursorOnSelectionText && item1.BordersContains(e.Location) && Flat;
                CursorOnSelectionHilight = !CursorOnSelectionText && item1.Contains(e.Location) && !CursorOnSelectionMenuHilight;

                CursorOnItemText = item0Text.Contains(e.Location);
                CursorOnGrayText = item2Text.Contains(e.Location);

                CursorOnFace = Border.Contains(e.Location) && !CursorOnSelectionHilight && !CursorOnSelectionMenuHilight && !CursorOnSelectionText && !CursorOnItemText && !CursorOnGrayText;

                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnShadow = false;
                CursorOnDkShadow = false;
                CursorOnHilight = false;
                CursorOnLight = false;
                CursorOnFace = false;
                CursorOnSelectionHilight = false;
                CursorOnSelectionMenuHilight = false;
                CursorOnSelectionText = false;
                CursorOnItemText = false;
                CursorOnGrayText = false;

                Refresh();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonShadow)));
                if (CursorOnDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonDkShadow)));
                if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonHilight)));
                if (CursorOnLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonLight)));
                if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonFace)));
                if (CursorOnSelectionHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.Hilight)));
                if (CursorOnSelectionMenuHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.MenuHilight)));
                if (CursorOnSelectionText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.HilightText)));
                if (CursorOnItemText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonText)));
                if (CursorOnGrayText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.GrayText)));
            }

            base.OnClick(e);
        }

        #endregion

        #region Methods

        private void RefreshItems()
        {
            Border = new(2, 2, Width - 5, Height - 5);

            item0Size = str0.Measure(Font);
            item1Size = str1.Measure(Font);
            item2Size = str2.Measure(Font);

            item0 = new(Border.X + 1, Border.Y + 1, Border.Width - 2, (int)item0Size.Height + 4);
            item1 = new(item0.X, item0.Y + item0.Height + 1, item0.Width, item0.Height + 1);
            item2 = new(item0.X, item1.Y + item1.Height + 1, item0.Width, item0.Height);

            item0Text = new(item0.X + 15, item0.Y + (item0.Height - (int)item0Size.Height) / 2, (int)item0Size.Width + 2, (int)item0Size.Height);
            item1Text = new(item1.X + 15, item1.Y + (item1.Height - (int)item1Size.Height) / 2, (int)item1Size.Width + 2, (int)item1Size.Height);
            item2Text = new(item2.X + 15, item2.Y + (item2.Height - (int)item2Size.Height) / 2, (int)item2Size.Width + 2, (int)item2Size.Height);

            Height = item0.Height + item1.Height + item2.Height + 2 + 2 + 2 + 2; // 2 + 2 = Top + bottom borders of shadow/dkshadow/light/hilight
        }

        #endregion

        #region Colors editor

        private bool CursorOnShadow, CursorOnDkShadow, CursorOnHilight, CursorOnLight, CursorOnFace,
            CursorOnSelectionHilight, CursorOnSelectionMenuHilight, CursorOnSelectionText, CursorOnItemText, CursorOnGrayText;

        private bool _ColorEdit_Shadow => EnableEditingColors && CursorOnShadow;
        private bool _ColorEdit_DkShadow => EnableEditingColors && CursorOnDkShadow;
        private bool _ColorEdit_Hilight => EnableEditingColors && CursorOnHilight;
        private bool _ColorEdit_Light => EnableEditingColors && CursorOnLight;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _ColorEdit_SelectionHilight => EnableEditingColors && CursorOnSelectionHilight;
        private bool _ColorEdit_SelectionMenuHilight => EnableEditingColors && CursorOnSelectionMenuHilight;
        private bool _ColorEdit_SelectionText => EnableEditingColors && CursorOnSelectionText;
        private bool _ColorEdit_ItemText => EnableEditingColors && CursorOnItemText;
        private bool _ColorEdit_GrayText => EnableEditingColors && CursorOnGrayText;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;

            G.Clear(BackColor);

            #region Editor

            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, Rect); }
            }

            #endregion

            using (Pen btnShadow = new(ButtonShadow))
            using (Pen btnHilight = new(ButtonHilight))
            using (Pen btnLight = new(ButtonLight))
            using (Pen btnDkShadow = new(ButtonDkShadow))
            using (Pen btnFace = new(BackColor))
            {
                if (!Flat)
                {
                    G.DrawLine(btnShadow, new Point(Rect.Width - 1, Rect.X + 1), new Point(Rect.Width - 1, Rect.Height - 1));
                    G.DrawLine(btnShadow, new Point(Rect.X + 1, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1));

                    G.DrawLine(btnHilight, new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 2, Rect.Y + 1));
                    G.DrawLine(btnHilight, new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 2));

                    G.DrawLine(btnLight, new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y));
                    G.DrawLine(btnLight, new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1));

                    G.DrawLine(btnDkShadow, new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height));
                    G.DrawLine(btnDkShadow, new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height));

                    G.DrawRectangle(btnFace, new Rectangle(2, 2, Width - 5, Height - 5));

                    #region Editor

                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                            G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                        }
                    }

                    if (_ColorEdit_DkShadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                            G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                        }
                    }

                    if (_ColorEdit_Hilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                            G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                        }
                    }

                    if (_ColorEdit_Light)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnLightPoints0[0], btnLightPoints0[1]);
                            G.DrawLine(P, btnLightPoints1[0], btnLightPoints1[1]);
                        }
                    }

                    #endregion

                }
                else
                {
                    G.DrawRectangle(btnShadow, Rect);

                    #region Editor

                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color)) { G.DrawRectangle(P, Rect); }
                    }

                    #endregion
                }

            }


            #region Items

            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            using (SolidBrush item0Brush = new(ForeColor))
            using (SolidBrush HilightBrush = new(Hilight))
            using (Pen MenuHilightPen = new(MenuHilight))
            using (SolidBrush item1Brush = new(HilightText))
            using (SolidBrush item2Brush = new(GrayText))
            {
                if (!Flat)
                {
                    G.FillRectangle(HilightBrush, item1);
                }
                else
                {
                    G.FillRectangle(HilightBrush, item1);
                    G.DrawRectangle(MenuHilightPen, item1);
                }

                #region Editor

                if (_ColorEdit_SelectionHilight)
                {
                    Color color = Color.FromArgb(100, Hilight.IsDark() ? Color.White : Color.Black);
                    using (Pen P = new(color))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                    {
                        G.FillRectangle(hb, item1);
                        G.DrawRectangle(P, item1);
                    }
                }

                if (_ColorEdit_SelectionMenuHilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color)) { G.DrawRectangle(P, item1); }
                }

                if (_ColorEdit_SelectionText)
                {
                    Color color = Color.FromArgb(100, Hilight.IsDark() ? Color.White : Color.Black);
                    using (Pen P = new(color))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                    {
                        G.FillRectangle(hb, item1Text);
                        G.DrawRectangle(P, item1Text);
                    }
                }

                if (_ColorEdit_ItemText)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (Pen P = new(color))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                    {
                        G.FillRectangle(hb, item0Text);
                        G.DrawRectangle(P, item0Text);
                    }
                }

                if (_ColorEdit_GrayText)
                {
                    Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                    using (Pen P = new(color))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                    {
                        G.FillRectangle(hb, item2Text);
                        G.DrawRectangle(P, item2Text);
                    }
                }

                #endregion

                G.DrawString(str0, Font, item0Brush, item0Text, sf);

                G.DrawString(str1, Font, item1Brush, item1Text, sf);

                G.DrawString(str2, Font, item2Brush, item2Text, sf);
            }

            #endregion
        }
    }
}