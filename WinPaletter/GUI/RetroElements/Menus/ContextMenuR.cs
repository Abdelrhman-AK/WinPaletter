using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Templates;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// Retro context menu with Windows 9x style
    /// </summary>
    [Description("Retro context menu with Windows 9x style")]
    public class ContextMenuR : Panel
    {
        /// <summary>
        /// Initialize new instance of <see cref="ContextMenuR"/>
        /// </summary>
        public ContextMenuR()
        {
            Font = new("Microsoft Sans Serif", 8f);
            DoubleBuffered = true;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            BorderStyle = BorderStyle.None;
            Text = string.Empty;

            PositionMenuItems();
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

        private string str_MenuItem => Program.Lang.Strings.Previewer.MenuItem;
        private string str_Selection => Program.Lang.Strings.Previewer.Selection;
        private string str_DisabledItem => Program.Lang.Strings.Previewer.DisabledItem;

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

        /// <summary>
        /// Text of the control
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        /// <summary>
        /// Back color of the control
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
        /// Fore color of the control
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

        private Color _colorBorder = SystemColors.ActiveBorder;

        /// <summary>
        /// Border color of the context menu
        /// </summary>
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

        /// <summary>
        /// Flat style of the context menu
        /// </summary>
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

        /// <summary>
        /// Button shadow color of the context menu
        /// </summary>
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

        /// <summary>
        /// Button dark shadow color of the context menu
        /// </summary>
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

        /// <summary>
        /// Button hilight color of the context menu
        /// </summary>
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

        /// <summary>
        /// Button light color of the context menu
        /// </summary>
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

        /// <summary>
        /// Hilight color of the context menu
        /// </summary>
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

        /// <summary>
        /// Hilight text color of the context menu
        /// </summary>
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

        /// <summary>
        /// Menu hilight color of the context menu
        /// </summary>
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

        /// <summary>
        /// Gray (Disabled) text color of the context menu
        /// </summary>
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

        /// <summary>
        /// Enable editing colors of the context menu by clicking on them
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Event handler for the color editor invoker that is triggered when a color is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Event for the color editor invoker that is triggered when a color is clicked
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// On font changed event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            PositionMenuItems();

            base.OnFontChanged(e);
        }

        /// <summary>
        /// On size changed event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            // Set the rectangle and border of the context menu

            Rect = new(0, 0, Width - 1, Height - 1);
            Border = new(2, 2, Width - 5, Height - 5);

            btnShadowPoints0 = [new Point(Rect.Width - 1, Rect.X + 1), new Point(Rect.Width - 1, Rect.Height - 1)];
            btnShadowPoints1 = [new Point(Rect.X + 1, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1)];

            btnDkShadowPoints0 = [new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height)];
            btnDkShadowPoints1 = [new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height)];

            btnHilightPoints0 = [new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 2, Rect.Y + 1)];
            btnHilightPoints1 = [new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 2)];

            btnLightPoints0 = [new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y)];
            btnLightPoints1 = [new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1)];

            base.OnSizeChanged(e);
        }

        /// <summary>
        /// On mouse move event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Check if the mouse is on the context menu items and set flags according to mouse position
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnShadow = (!Flat && (btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location)))
                              || (Flat && Rect.BordersContains(e.Location));

                CursorOnDkShadow = btnDkShadowPoints0.Contains(e.Location) || btnDkShadowPoints1.Contains(e.Location);
                CursorOnHilight = btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location);
                CursorOnLight = btnLightPoints0.Contains(e.Location) || btnLightPoints1.Contains(e.Location);

                // Keep order of the following 3 lines as they are (NEVER CHANGE)
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

        /// <summary>
        /// On mouse leave event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // Reset flags when the mouse leaves the context menu
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

        /// <summary>
        /// On click event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            // Invoke the color editor invoker when a color is clicked
            if (!DesignMode && EnableEditingColors)
            {
                // Invoke editing shadow color
                if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonShadow)));

                // Invoke editing dark shadow color
                if (CursorOnDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonDkShadow)));

                // Invoke editing hilight color
                if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));

                // Invoke editing light color
                if (CursorOnLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonLight)));

                // Invoke editing face color
                if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonFace)));

                // Invoke editing selection hilight color
                if (CursorOnSelectionHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.Hilight)));

                // Invoke editing selection menu hilight color
                if (CursorOnSelectionMenuHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.MenuHilight)));

                // Invoke editing hilight text color
                if (CursorOnSelectionText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.HilightText)));

                // Invoke editing item text color
                if (CursorOnItemText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonText)));

                // Invoke editing gray (disabled) text color
                if (CursorOnGrayText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.GrayText)));
            }

            base.OnClick(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Position menu items on the context menu by calculating their sizes
        /// </summary>
        private void PositionMenuItems()
        {
            // Static rectangle and border of the context menu
            Border = new(2, 2, Width - 5, Height - 5);

            // GetTextAndImageRectangles the size of the menu items
            item0Size = str_MenuItem.Measure(Font);
            item1Size = str_Selection.Measure(Font);
            item2Size = str_DisabledItem.Measure(Font);

            // Set the rectangles of the menu items
            item0 = new(Border.X + 1, Border.Y + 1, Border.Width - 2, (int)item0Size.Height + 4);
            item1 = new(item0.X, item0.Y + item0.Height + 1, item0.Width, item0.Height + 1);
            item2 = new(item0.X, item1.Y + item1.Height + 1, item0.Width, item0.Height);

            // Set the rectangles of the menu item texts
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

        /// <summary>
        /// On paint event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Draw background color of the context menu
            G.Clear(BackColor);

            #region Editor

            // Draw a hatch brush on the face of the context menu when the color editor is enabled and the mouse is on the face
            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, Rect); }
            }

            #endregion

            // Draw the border of the context menu
            using (Pen btnShadow = new(ButtonShadow))
            using (Pen btnHilight = new(ButtonHilight))
            using (Pen btnLight = new(ButtonLight))
            using (Pen btnDkShadow = new(ButtonDkShadow))
            using (Pen btnFace = new(BackColor))
            {
                if (!Flat)
                {
                    // Draw 3D border of the context menu

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

                    // Draw alternative lines on the shadow of the context menu when the color editor is enabled and the mouse is on the shadow
                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                            G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                        }
                    }

                    // Draw alternative lines on the dark shadow of the context menu when the color editor is enabled and the mouse is on the dark shadow
                    if (_ColorEdit_DkShadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                            G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                        }
                    }

                    // Draw alternative lines on the hilight of the context menu when the color editor is enabled and the mouse is on the hilight
                    if (_ColorEdit_Hilight)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color))
                        {
                            G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                            G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                        }
                    }

                    // Draw alternative lines on the light of the context menu when the color editor is enabled and the mouse is on the light
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
                    // Draw flat border of the context menu

                    G.DrawRectangle(btnShadow, Rect);

                    #region Editor

                    // Draw an alternative rectangle on the context menu when the color editor is enabled and the mouse is on the shadow
                    if (_ColorEdit_Shadow)
                    {
                        Color color = Color.FromArgb(200, 128, 0, 0);
                        using (Pen P = new(color)) { G.DrawRectangle(P, Rect); }
                    }

                    #endregion
                }

            }

            #region Items

            // Draw the menu items of the context menu
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            using (SolidBrush item0Brush = new(ForeColor))
            using (SolidBrush HilightBrush = new(Hilight))
            using (Pen MenuHilightPen = new(MenuHilight))
            using (SolidBrush item1Brush = new(HilightText))
            using (SolidBrush item2Brush = new(GrayText))
            {
                // Draw the menu items
                if (!Flat)
                {
                    G.FillRectangle(HilightBrush, item1);
                }
                else
                {
                    // Draw flat menu items: its rectangle has 2 tones of colors (background and border)
                    G.FillRectangle(HilightBrush, item1);
                    G.DrawRectangle(MenuHilightPen, item1);
                }

                #region Editor

                // Draw a hatch brush on the selection hilight of the context menu when the color editor is enabled and the mouse is on selection hilight
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

                // Draw a rectangle on the selection menu hilight of the context menu when the color editor is enabled and the mouse is on selection menu hilight
                if (_ColorEdit_SelectionMenuHilight)
                {
                    Color color = Color.FromArgb(200, 128, 0, 0);
                    using (Pen P = new(color)) { G.DrawRectangle(P, item1); }
                }

                // Draw a hatch brush on the selection text of the context menu when the color editor is enabled and the mouse is on selection text
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

                // Draw a hatch brush on the item text of the context menu when the color editor is enabled and the mouse is on item text
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

                // Draw a hatch brush on the gray text of the context menu when the color editor is enabled and the mouse is on gray text
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

                // Draw the text of the menu items
                G.DrawString(str_MenuItem, Font, item0Brush, item0Text, sf);
                G.DrawString(str_Selection, Font, item1Brush, item1Text, sf);
                G.DrawString(str_DisabledItem, Font, item2Brush, item2Text, sf);
            }

            #endregion


        }
    }
}