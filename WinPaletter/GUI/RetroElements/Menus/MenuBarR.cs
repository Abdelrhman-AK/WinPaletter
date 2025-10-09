using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Templates;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A retro menu bar with 3 items: Normal, Disabled, Selected.
    /// </summary>
    public class MenuBarR : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBarR"/> class.
        /// </summary>
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

        string NormalStr => Program.Lang.Strings.Previewer.Normal;
        string DisabledStr => Program.Lang.Strings.Previewer.Disabled;
        string SelectedStr => Program.Lang.Strings.Previewer.Selected;

        private PointF[] btnShadowPoints0;
        private PointF[] btnShadowPoints1;
        private PointF[] btnHilightPoints0;
        private PointF[] btnHilightPoints1;

        readonly int GripSize = 6;
        Rectangle Grip;
        bool isMoving_Grip = false;

        #endregion

        #region Properties

        /// <summary>
        /// BackColor property of the control.
        /// </summary>
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

        /// <summary>
        /// ForeColor property of the control.
        /// </summary>
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


        /// <summary>
        /// The location of the selected item on the control.
        /// </summary>
        public Point SelectedItemLocation => PreviewSelectionItem ? new(item2.Left, item2.Top) : Point.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the control is flat design or not.
        /// </summary>
        public bool Flat
        {
            get => _flat;
            set
            {
                if (_flat != value)
                {
                    _flat = value;
                    Refresh();
                }
            }
        }
        private bool _flat = false;

        /// <summary>
        /// Menu bar color.
        /// </summary>
        public Color MenuBar
        {
            get => _menuBar;
            set
            {
                if (_menuBar != value)
                {
                    _menuBar = value;
                    Refresh();
                }
            }
        }
        private Color _menuBar = SystemColors.MenuBar;

        /// <summary>
        /// Button hilight color.
        /// </summary>
        public Color ButtonHilight
        {
            get => buttonHilight;
            set
            {
                if (buttonHilight != value)
                {
                    buttonHilight = value;
                    Refresh();
                }
            }
        }
        private Color buttonHilight = SystemColors.ButtonHighlight;

        /// <summary>
        /// Button shadow color.
        /// </summary>
        public Color ButtonShadow
        {
            get => buttonShadow;
            set
            {
                if (buttonShadow != value)
                {
                    buttonShadow = value;
                    Refresh();
                }
            }
        }
        private Color buttonShadow = SystemColors.ButtonShadow;

        /// <summary>
        /// Hilight color.
        /// </summary>
        public Color Hilight
        {
            get => _hilight;
            set
            {
                if (_hilight != value)
                {
                    _hilight = value;
                    Refresh();
                }
            }
        }
        private Color _hilight = SystemColors.Highlight;

        /// <summary>
        /// Hilight text color.
        /// </summary>
        public Color HilightText
        {
            get => _hilightText;
            set
            {
                if (_hilightText != value)
                {
                    _hilightText = value;
                    Refresh();
                }
            }
        }
        private Color _hilightText = SystemColors.HighlightText;

        /// <summary>
        /// Menu hilight color.
        /// </summary>
        public Color MenuHilight
        {
            get => _menuHilight;
            set
            {
                if (_menuHilight != value)
                {
                    _menuHilight = value;
                    Refresh();
                }
            }
        }
        private Color _menuHilight = SystemColors.MenuHighlight;

        /// <summary>
        /// Gray (disabled) text color.
        /// </summary>
        public Color GrayText
        {
            get => _grayText;
            set
            {
                if (_grayText != value)
                {
                    _grayText = value;
                    Refresh();
                }
            }
        }
        private Color _grayText = SystemColors.GrayText;

        /// <summary>
        /// Height of the menu bar in pixels.
        /// </summary>
        public int MenuHeight
        {
            get => _MenuHeight;
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
        private int _MenuHeight = SystemInformation.MenuHeight;

        /// <summary>
        /// Determines whether the selected item is previewed or not.
        /// </summary>
        public bool PreviewSelectionItem
        {
            get => _previewSelectionItem;
            set
            {
                if (_previewSelectionItem != value)
                {
                    _previewSelectionItem = value;
                    Refresh();
                }
            }
        }
        private bool _previewSelectionItem = true;

        /// <summary>
        /// A value indicating whether the control colors can be edited by the user.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        /// <summary>
        /// A value indicating whether the control metrics can be edited by the user.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;
        #endregion

        #region Events/Overrides

        /// <summary>
        /// Event handler for the editor when the user clicks on the control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Event raised when the user clicks on the control.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Raises the <see cref="Control.Click"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode)
            {
                // If this flag is enabled, the user can edit the colors after clicking on the control.
                if (EnableEditingColors)
                {
                    // Edit shadow color when the cursor is on the shadow area.
                    if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonShadow)));

                    // Edit hilight color when the cursor is on the hilight area.
                    else if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));

                    // Edit face color when the cursor is on the face area.
                    else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(Flat ? nameof(RetroDesktopColors.MenuBar) : nameof(RetroDesktopColors.ButtonFace)));

                    // Edit text color when the cursor is on the text area.
                    else if (CursorOnText0 | CursorOnText1) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonText)));

                    // Edit gray text color when the cursor is on the gray text area.
                    else if (CursorOnGrayText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.GrayText)));

                    // Edit menu hilight color when the cursor is on the menu hilight area.
                    else if (CursorOnSelectionText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.HilightText)));

                    // Edit menu hilight color when the cursor is on the menu hilight area.
                    else if (CursorOnMenuHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.MenuHilight)));

                    // Edit selection hilight color when the cursor is on the selection hilight area.
                    else if (CursorOnSelectionHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.Hilight)));
                }

                // If this flag is enabled, the user can edit the metrics after clicking on the control.
                else if (EnableEditingMetrics)
                {
                    // Edit the menu font when the cursor is on any text area.
                    if (CursorOnText0 || CursorOnGrayText)
                    {
                        using (FontDialog fd = new() { Font = Font })
                        {
                            if (fd.ShowDialog() == DialogResult.OK)
                            {
                                Font = fd.Font;
                                SetStyle();
                                Refresh();

                                // Raise the event to notify the editor that the font has been changed.
                                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Font)));
                            }
                        }
                    }
                }
            }

            base.OnClick(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.FontChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            SetStyle();
            base.OnFontChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.Resize"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            SetStyle();
            base.OnResize(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                // If the user is editing the metrics, the cursor changes to a grip cursor when the cursor is on the grip area.
                if (isMoving_Grip)
                {
                    MenuHeight = e.Location.Y;
                    Cursor = Cursors.SizeNS;
                }

                // Get flags that indicate the cursor is on a specific area of the control (for colors editing).
                if (EnableEditingColors)
                {
                    CursorOnText0 = item0Text.Contains(e.Location);
                    CursorOnText1 = !Flat && PreviewSelectionItem && item2Text.Contains(e.Location);
                    CursorOnSelectionText = PreviewSelectionItem && Flat && item2Text.Contains(e.Location);
                    CursorOnGrayText = item1Text.Contains(e.Location);

                    CursorOnMenuHilight = PreviewSelectionItem && Flat && item2.BordersContains(e.Location);
                    CursorOnSelectionHilight = PreviewSelectionItem && Flat && item2.Contains(e.Location) && !item2.BordersContains(e.Location) && !CursorOnSelectionText;

                    CursorOnShadow = PreviewSelectionItem && !Flat && (btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location));
                    CursorOnHilight = PreviewSelectionItem && !Flat && (btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location));

                    CursorOnFace = rect.Contains(e.Location) && !CursorOnText0 && !CursorOnText1 && !CursorOnSelectionText && !CursorOnGrayText && !CursorOnMenuHilight && !CursorOnSelectionHilight && !CursorOnShadow && !CursorOnHilight;

                    Refresh();
                }

                // Get flags that indicate the cursor is on a specific area of the control (for metrics editing).
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

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                // If the user is editing the metrics, the grip area can be moved.
                isMoving_Grip = CursorOnGrip;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                // Release the grip area.
                isMoving_Grip = false;
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseLeave"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode)
            {
                // Reset area flags when the cursor leaves the control.

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

        /// <summary>
        /// Sets the style of the control based on the metrics and fonts.
        /// </summary>
        private void SetStyle()
        {
            rect = new Rectangle(0, 0, Width - 1, Height - 1);

            Height = Math.Max(MenuHeight, Font.Height);

            // GetTextAndImageRectangles the rectangles of the items.
            item0 = new Rectangle(0, 0, (int)NormalStr.Measure(Font).Width, Height);
            item1 = new Rectangle(item0.Right, 0, (int)DisabledStr.Measure(Font).Width, Height);
            item2 = new Rectangle(item1.Right, 0, (int)SelectedStr.Measure(Font).Width, Height);

            // GetTextAndImageRectangles the rectangles of the text of the items.
            SizeF item0Size = NormalStr.Measure(Font) - new Size(6, 6);
            SizeF item1Size = DisabledStr.Measure(Font) - new Size(6, 6);
            SizeF item2Size = SelectedStr.Measure(Font) - new Size(6, 6);

            // Proper alignment of the text in the rectangles.
            item0Text = new Rectangle(item0.X + (item0.Width - (int)item0Size.Width) / 2, item0.Y + (item0.Height - (int)item0Size.Height) / 2, (int)item0Size.Width, (int)item0Size.Height);
            item1Text = new Rectangle(item1.X + (item1.Width - (int)item1Size.Width) / 2, item1.Y + (item1.Height - (int)item1Size.Height) / 2, (int)item1Size.Width, (int)item1Size.Height);
            item2Text = new Rectangle(item2.X + (item2.Width - (int)item2Size.Width) / 2, item2.Y + (item2.Height - (int)item2Size.Height) / 2, (int)item2Size.Width, (int)item2Size.Height);

            // Get points where the shadow and hilight lines are drawn.
            btnShadowPoints0 = [new(item2.Left, item2.Top), new(item2.Right - 1, item2.Top)];
            btnShadowPoints1 = [new(item2.Left, item2.Top), new(item2.Left, item2.Bottom - 1)];
            btnHilightPoints0 = [new(item2.Right - 1, item2.Top), new(item2.Right - 1, item2.Bottom - 1)];
            btnHilightPoints1 = [new(item2.Left, item2.Bottom - 1), new(item2.Right - 1, item2.Bottom - 1)];

            // Resizing grip area.
            Grip = new(rect.X + rect.Width / 2 - (int)(0.5 * GripSize), rect.Y + rect.Height - GripSize, GripSize, GripSize);
        }

        #endregion

        #region Colors editor

        /// <summary>
        /// Flags that indicate the cursor is on a specific area of the control.
        /// </summary>
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

        /// <summary>
        /// Paints the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Draw the control background. If the control is flat, the background is the same as the menu bar color.
            G.Clear(!Flat ? BackColor : MenuBar);

            #region Editor

            // Draw hatch brush on the face area when the user is editing the face color.
            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
            }

            #endregion

            // Draw the items if the flag is enabled.
            if (PreviewSelectionItem)
            {
                // Draw menu bar as flat design.
                if (Flat)
                {
                    using (Pen P = new(MenuHilight))
                    using (SolidBrush br = new(Hilight))
                    {
                        G.FillRectangle(br, item2);
                        G.DrawRectangle(P, new Rectangle(item2.X, item2.Y, item2.Width, item2.Height - 1));
                    }

                    #region Editor

                    // Draw hatch brush on the selection hilight area when the user is editing the selection hilight color.
                    if (_ColorEdit_SelectionHilight)
                    {
                        Color color = Color.FromArgb(100, Hilight.IsDark() ? Color.White : Color.Black);
                        using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, item2); }
                    }

                    // Draw the menu hilight area with a different color when the user is editing the menu hilight color.
                    if (_ColorEdit_MenuHilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color)) { G.DrawRectangle(P, new(item2.X, item2.Y, item2.Width, item2.Height - 1)); }
                    }

                    #endregion
                }
                else
                {
                    // Draw menu bar as non-flat design.

                    using (Pen pShadow = new(ButtonShadow))
                    using (Pen pHilight = new(ButtonHilight))
                    {
                        G.DrawLine(pShadow, item2.Left, item2.Top, item2.Right - 1, item2.Top);
                        G.DrawLine(pShadow, item2.Left, item2.Top, item2.Left, item2.Bottom - 1);
                        G.DrawLine(pHilight, item2.Right - 1, item2.Top, item2.Right - 1, item2.Bottom - 1);
                        G.DrawLine(pHilight, item2.Left, item2.Bottom - 1, item2.Right - 1, item2.Bottom - 1);
                    }

                    #region Editor

                    // Draw shadow lines with a different color when the user is editing the shadow color.
                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                            G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                        }
                    }

                    // Draw hilight lines with a different color when the user is editing the hilight color.
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

            // Draw hatch brush on the text area when the user is editing the text color.
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

            // Draw hatch brush on the gray text area when the user is editing the gray text color.
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

            // Draw hatch brush on the selection text area when the user is editing the selection text color.
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

            // Draw the text of the items.
            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            using (SolidBrush ForeColorBrush = new(ForeColor))
            using (SolidBrush DisabledBrush = new(GrayText))
            using (SolidBrush SelectedBrush = new(Flat ? HilightText : ForeColor))
            {
                G.DrawString(NormalStr, Font, ForeColorBrush, item0, sf);
                G.DrawString(DisabledStr, Font, DisabledBrush, item1, sf);

                if (PreviewSelectionItem) G.DrawString(SelectedStr, Font, SelectedBrush, item2, sf);
            }

            // Draw the grip area when the user is editing the menu height.
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