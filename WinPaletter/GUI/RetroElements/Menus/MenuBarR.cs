using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    public class MenuBarR : System.Windows.Forms.Panel
    {
        public MenuBarR()
        {
            Font = new("Microsoft Sans Serif", 8f);
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            DoubleBuffered = true;
        }

        #region Variables

        Rectangle rect;
        Rectangle item0;
        Rectangle item1;
        Rectangle item2;

        Rectangle item0Text;
        Rectangle item1Text;
        Rectangle item2Text;

        string str0 = "Normal";
        string str1 = "Disabled";
        string str2 = "Selected";

        private PointF[] btnShadowPoints0;
        private PointF[] btnShadowPoints1;
        private PointF[] btnHilightPoints0;
        private PointF[] btnHilightPoints1;

        int GripSize = 6;
        Rectangle Grip;
        bool isMoving_Grip = false;

        #endregion

        #region Properties

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


        public Point SelectedItemLocation => PreviewSelectionItem ? new(item2.Left, item2.Top) : Point.Empty;


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


        private Color _menuBar = SystemColors.MenuBar;
        public Color MenuBar
        {
            get { return _menuBar; }
            set
            {
                if (_menuBar != value)
                {
                    _menuBar = value;
                    Refresh();
                }
            }
        }


        private Color buttonHilight = SystemColors.ButtonHighlight;
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


        private Color buttonShadow = SystemColors.ButtonShadow;
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


        private int _MenuHeight = SystemInformation.MenuHeight;
        public int MenuHeight
        {
            get { return _MenuHeight; }
            set
            {
                if (value < 15) value = 15;
                if (value > 50) value = 50;

                if (_MenuHeight != value)
                {
                    _MenuHeight = value;
                    SetStyle();
                    Refresh();

                    if (!DesignMode && EnableEditingMetrics && isMoving_Grip) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(MenuHeight)));
                }
            }
        }


        private bool _previewSelectionItem = true;
        public bool PreviewSelectionItem
        {
            get { return _previewSelectionItem; }
            set
            {
                if (_previewSelectionItem != value)
                {
                    _previewSelectionItem = value;
                    Refresh();
                }
            }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;
        #endregion

        #region Events/Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode)
            {
                if (EnableEditingColors)
                {
                    if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonShadow)));
                    else if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonHilight)));
                    else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(Flat ? nameof(Templates.RetroDesktopColors.MenuBar) : nameof(Templates.RetroDesktopColors.ButtonFace)));
                    else if (CursorOnText0 | CursorOnText1) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonText)));
                    else if (CursorOnGrayText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.GrayText)));
                    else if (CursorOnSelectionText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.HilightText)));
                    else if (CursorOnMenuHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.MenuHilight)));
                    else if (CursorOnSelectionHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.Hilight)));
                }

                else if (EnableEditingMetrics)
                {
                    if (CursorOnText0 || CursorOnGrayText)
                    {
                        using (FontDialog fd = new() { Font = this.Font })
                        {
                            if (fd.ShowDialog() == DialogResult.OK)
                            {
                                this.Font = fd.Font;
                                SetStyle();
                                Refresh();
                                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Font)));
                            }
                        }
                    }
                }
            }

            base.OnClick(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            SetStyle();
            base.OnFontChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            SetStyle();
            base.OnResize(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (isMoving_Grip)
                {
                    MenuHeight = e.Location.Y;
                    Cursor = Cursors.SizeNS;
                }

                if (EnableEditingColors)
                {
                    CursorOnText0 = item0Text.Contains(e.Location);
                    CursorOnText1 = !Flat && PreviewSelectionItem && item2Text.Contains(e.Location);
                    CursorOnSelectionText = PreviewSelectionItem && Flat && item2Text.Contains(e.Location);
                    CursorOnGrayText = item1Text.Contains(e.Location);

                    CursorOnMenuHilight = PreviewSelectionItem && Flat && item2.BordersContains(e.Location);
                    CursorOnSelectionHilight = PreviewSelectionItem && Flat && item2.Contains(e.Location) && !item2.BordersContains(e.Location) && !CursorOnSelectionText;

                    CursorOnShadow = (PreviewSelectionItem && !Flat) && (btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location));
                    CursorOnHilight = (PreviewSelectionItem && !Flat) && (btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location));

                    CursorOnFace = rect.Contains(e.Location) && !CursorOnText0 && !CursorOnText1 && !CursorOnSelectionText && !CursorOnGrayText && !CursorOnMenuHilight && !CursorOnSelectionHilight && !CursorOnShadow && !CursorOnHilight;

                    Refresh();
                }

                else if (EnableEditingMetrics)
                {
                    CursorOnMe = rect.Contains(e.Location);
                    CursorOnText0 = item0Text.Contains(e.Location);
                    CursorOnGrayText = item1Text.Contains(e.Location);
                    CursorOnGrip = Grip.Contains(e.Location);
                    Cursor = CursorOnGrip ? Cursors.SizeNS : Cursors.Default;

                    Refresh();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                isMoving_Grip = CursorOnGrip;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                isMoving_Grip = false;
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode)
            {
                if (EnableEditingColors)
                {
                    CursorOnText0 = false;
                    CursorOnText1 = false;
                    CursorOnGrayText = false;
                    CursorOnMenuHilight = false;
                    CursorOnSelectionHilight = false;
                    CursorOnSelectionText = false;
                    CursorOnShadow = false;
                    CursorOnHilight = false;
                    CursorOnFace = false;
                }
                else if (EnableEditingMetrics)
                {
                    CursorOnMe = false;
                    CursorOnGrip = false;
                    Cursor = Cursors.Default;
                }

                Refresh();
            }


            base.OnMouseLeave(e);
        }

        #endregion

        #region Methods

        private void SetStyle()
        {
            rect = new Rectangle(0, 0, Width - 1, Height - 1);

            Height = Math.Max(MenuHeight, Font.Height);

            item0 = new Rectangle(0, 0, (int)str0.Measure(Font).Width, Height);
            item1 = new Rectangle(item0.Right, 0, (int)str1.Measure(Font).Width, Height);
            item2 = new Rectangle(item1.Right, 0, (int)str2.Measure(Font).Width, Height);

            SizeF item0Size = str0.Measure(Font) - new Size(6, 6);
            SizeF item1Size = str1.Measure(Font) - new Size(6, 6);
            SizeF item2Size = str2.Measure(Font) - new Size(6, 6);

            item0Text = new Rectangle(item0.X + (item0.Width - (int)item0Size.Width) / 2, item0.Y + (item0.Height - (int)item0Size.Height) / 2, (int)item0Size.Width, (int)item0Size.Height);
            item1Text = new Rectangle(item1.X + (item1.Width - (int)item1Size.Width) / 2, item1.Y + (item1.Height - (int)item1Size.Height) / 2, (int)item1Size.Width, (int)item1Size.Height);
            item2Text = new Rectangle(item2.X + (item2.Width - (int)item2Size.Width) / 2, item2.Y + (item2.Height - (int)item2Size.Height) / 2, (int)item2Size.Width, (int)item2Size.Height);

            btnShadowPoints0 = new PointF[] { new PointF(item2.Left, item2.Top), new PointF(item2.Right - 1, item2.Top) };
            btnShadowPoints1 = new PointF[] { new PointF(item2.Left, item2.Top), new PointF(item2.Left, item2.Bottom - 1) };
            btnHilightPoints0 = new PointF[] { new PointF(item2.Right - 1, item2.Top), new PointF(item2.Right - 1, item2.Bottom - 1) };
            btnHilightPoints1 = new PointF[] { new PointF(item2.Left, item2.Bottom - 1), new PointF(item2.Right - 1, item2.Bottom - 1) };

            Grip = new(rect.X + rect.Width / 2 - (int)(0.5 * GripSize), rect.Y + rect.Height - GripSize, GripSize, GripSize);
        }

        #endregion

        #region Colors editor

        private bool CursorOnShadow, CursorOnHilight, CursorOnFace, CursorOnText0, CursorOnText1, CursorOnGrayText, CursorOnMenuHilight, CursorOnSelectionHilight, CursorOnSelectionText;

        private bool CursorOnMe, CursorOnGrip;
        private bool _ColorEdit_Shadow => EnableEditingColors && CursorOnShadow;
        private bool _ColorEdit_Hilight => EnableEditingColors && CursorOnHilight;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _ColorEdit_Text => EnableEditingColors && CursorOnText0;
        private bool _ColorEdit_TextSelectionItem_NonFlat => EnableEditingColors && CursorOnText1;
        private bool _ColorEdit_GrayText => EnableEditingColors && CursorOnGrayText;
        private bool _ColorEdit_MenuHilight => EnableEditingColors && CursorOnMenuHilight;
        private bool _ColorEdit_SelectionHilight => EnableEditingColors && CursorOnSelectionHilight;
        private bool _ColorEdit_SelectionText => EnableEditingColors && CursorOnSelectionText;
        private bool _MetricsEdit_MenuHeight => !DesignMode && EnableEditingMetrics && (CursorOnMe || isMoving_Grip);
        private bool _MetricsEdit_Text => EnableEditingMetrics && CursorOnText0;
        private bool _MetricsEdit_GrayText => EnableEditingMetrics && CursorOnGrayText;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = Program.Style.RenderingHint;

            G.Clear(!Flat ? BackColor : MenuBar);

            #region Editor

            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
            }

            #endregion

            if (PreviewSelectionItem)
            {
                if (Flat)
                {
                    using (Pen P = new(MenuHilight))
                    using (SolidBrush br = new(Hilight))
                    {
                        G.FillRectangle(br, item2);
                        G.DrawRectangle(P, new Rectangle(item2.X, item2.Y, item2.Width, item2.Height - 1));
                    }

                    #region Editor

                    if (_ColorEdit_SelectionHilight)
                    {
                        Color color = Color.FromArgb(100, Hilight.IsDark() ? Color.White : Color.Black);
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, item2); }
                    }

                    if (_ColorEdit_MenuHilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color)) { G.DrawRectangle(P, new(item2.X, item2.Y, item2.Width, item2.Height - 1)); }
                    }

                    #endregion
                }
                else
                {
                    using (Pen pShadow = new(ButtonShadow))
                    using (Pen pHilight = new(ButtonHilight))
                    {
                        G.DrawLine(pShadow, item2.Left, item2.Top, item2.Right - 1, item2.Top);
                        G.DrawLine(pShadow, item2.Left, item2.Top, item2.Left, item2.Bottom - 1);
                        G.DrawLine(pHilight, item2.Right - 1, item2.Top, item2.Right - 1, item2.Bottom - 1);
                        G.DrawLine(pHilight, item2.Left, item2.Bottom - 1, item2.Right - 1, item2.Bottom - 1);
                    }

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

                    if (_ColorEdit_Hilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                            G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                        }
                    }

                    #endregion
                }
            }

            #region Editor

            if (_ColorEdit_Text || _MetricsEdit_Text)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, item0Text);
                    G.DrawRectangle(P, item0Text);
                }
            }

            if (_ColorEdit_GrayText || _MetricsEdit_GrayText)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, item1Text);
                    G.DrawRectangle(P, item1Text);
                }
            }

            if (_ColorEdit_TextSelectionItem_NonFlat || _ColorEdit_SelectionText)
            {
                Color color;

                if (_ColorEdit_TextSelectionItem_NonFlat) color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                else color = Color.FromArgb(100, Hilight.IsDark() ? Color.White : Color.Black);

                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, item2Text);
                    G.DrawRectangle(P, item2Text);
                }
            }

            #endregion

            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            using (SolidBrush ForeColorBrush = new(ForeColor))
            using (SolidBrush DisabledBrush = new(GrayText))
            using (SolidBrush SelectedBrush = new(Flat ? HilightText : ForeColor))
            {
                G.DrawString(str0, Font, ForeColorBrush, item0, sf);
                G.DrawString(str1, Font, DisabledBrush, item1, sf);

                if (PreviewSelectionItem) G.DrawString(str2, Font, SelectedBrush, item2, sf);
            }

            if (_MetricsEdit_MenuHeight)
            {
                using (Pen P = new(BackColor.Invert()) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(P, rect);
                }

                using (Pen P = new(BackColor.Invert()) { DashStyle = DashStyle.Solid })
                {
                    G.FillRoundedRect(Brushes.White, Grip, 1, true);

                    G.DrawRoundedRect(P, Grip, 1, true);
                }
            }

            base.OnPaint(e);
        }
    }
}