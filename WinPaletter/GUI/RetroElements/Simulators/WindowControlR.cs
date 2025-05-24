using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A control that simulates a classic window.
    /// </summary>
    public class WindowControlR : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowControlR"/> class.
        /// </summary>
        public WindowControlR()
        {
            DoubleBuffered = true;
            ForeColor = SystemColors.WindowText;
            BackColor = SystemColors.Window;
        }

        #region Variables

        Rectangle Rect;
        Rectangle RectText;

        private PointF[] btnShadowPoints0;
        private PointF[] btnShadowPoints1;
        private PointF[] btnDkShadowPoints0;
        private PointF[] btnDkShadowPoints1;
        private PointF[] btnHilightPoints0;
        private PointF[] btnHilightPoints1;
        private PointF[] btnLightPoints0;
        private PointF[] btnLightPoints1;

        #endregion

        #region Properties

        private Color buttonShadow = SystemColors.ButtonShadow;
        private Color buttonDkShadow = SystemColors.ControlDark;
        private Color buttonHilight = SystemColors.ButtonHighlight;
        private Color buttonLight = SystemColors.ControlLight;

        /// <summary>
        /// Gets or sets the color of the shadow of the button.
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
        /// Gets or sets the color of the dark shadow of the button.
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
        /// Gets or sets the color of the hilight of the button.
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
        /// Gets or sets the color of the light of the button.
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
        /// Gets or sets the background color associated with this control.
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
        /// Enables the editing of colors by clicking on the control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Occurs when the editor is invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Occurs when the editor is invoked.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            SetPoints();

            base.OnFontChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetPoints();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // Reset editing flags
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnShadow = false;
                CursorOnDkShadow = false;
                CursorOnHilight = false;
                CursorOnLight = false;
                CursorOnFace = false;
                CursorOnText = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // If editing is enabled, check if the cursor is on a specific area of the control and set the flags accordingly
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnShadow = btnShadowPoints0.Contains(e.Location) || btnShadowPoints1.Contains(e.Location);
                CursorOnDkShadow = btnDkShadowPoints0.Contains(e.Location) || btnDkShadowPoints1.Contains(e.Location);
                CursorOnHilight = btnHilightPoints0.Contains(e.Location) || btnHilightPoints1.Contains(e.Location);
                CursorOnLight = btnLightPoints0.Contains(e.Location) || btnLightPoints1.Contains(e.Location);
                CursorOnText = RectText.Contains(e.Location);
                CursorOnFace = Rect.Contains(e.Location) && !CursorOnShadow && !CursorOnDkShadow && !CursorOnHilight && !CursorOnLight && !CursorOnText;

                Refresh();
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            // If editing is enabled, invoke the editor event with the appropriate color name
            if (!DesignMode && EnableEditingColors)
            {
                if (CursorOnShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonShadow)));
                else if (CursorOnDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonDkShadow)));
                else if (CursorOnHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonHilight)));
                else if (CursorOnLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonLight)));
                else if (CursorOnText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.WindowText)));
                else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.Window)));
            }

            base.OnClick(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the points of the control for drawing.
        /// </summary>
        private void SetPoints()
        {
            Rect = new(0, 0, Width - 1, Height - 1);

            btnShadowPoints0 = [new Point(Rect.Width - 1, Rect.X + 1), new Point(Rect.Width - 1, Rect.Height - 1)];
            btnShadowPoints1 = [new Point(Rect.X + 1, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1)];

            btnDkShadowPoints0 = [new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height)];
            btnDkShadowPoints1 = [new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height)];

            btnHilightPoints0 = [new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 2, Rect.Y + 1)];
            btnHilightPoints1 = [new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 2)];

            btnLightPoints0 = [new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y)];
            btnLightPoints1 = [new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1)];

            SizeF TextSize = Text.Measure(Font);
            RectText = new Rectangle(4, 4, (int)TextSize.Width, (int)TextSize.Height);
        }

        #endregion

        #region Colors editor

        private bool CursorOnShadow, CursorOnDkShadow, CursorOnHilight, CursorOnLight, CursorOnFace, CursorOnText;
        private bool _ColorEdit_Shadow => EnableEditingColors && CursorOnShadow;
        private bool _ColorEdit_DkShadow => EnableEditingColors && CursorOnDkShadow;
        private bool _ColorEdit_Hilight => EnableEditingColors && CursorOnHilight;
        private bool _ColorEdit_Light => EnableEditingColors && CursorOnLight;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _ColorEdit_Text => EnableEditingColors && CursorOnText;

        #endregion
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Create a rectangle that is 1 pixel less than the control's size
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            // Clear the control with the background color
            G.Clear(BackColor);

            #region Editor

            // If editing is enabled and cursor is no face area, draw a hatch brush over the control
            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, Rect); }
            }

            #endregion

            // Draw the window border
            using (Pen penButtonShadow = new(ButtonShadow))
            using (Pen penButtonDkShadow = new(ButtonDkShadow))
            using (Pen penButtonLight = new(ButtonLight))
            using (Pen penButtonHilight = new(ButtonHilight))
            {
                G.DrawLine(penButtonShadow, new Point(rect.X, rect.Y), new Point(rect.Width - 1, rect.Y));
                G.DrawLine(penButtonShadow, new Point(rect.X, rect.Y), new Point(rect.X, rect.Height - 1));

                G.DrawLine(penButtonDkShadow, new Point(rect.X, rect.Y) + (Size)new Point(1, 1), new Point(rect.Width - 2, rect.Y + 1));
                G.DrawLine(penButtonDkShadow, new Point(rect.X, rect.Y) + (Size)new Point(1, 1), new Point(rect.X + 1, rect.Height - 2));

                G.DrawLine(penButtonLight, new Point(rect.Width - 1, 1), new Point(rect.Width - 1, rect.Height - 1));
                G.DrawLine(penButtonLight, new Point(1, rect.Height - 1), new Point(rect.Width - 1, rect.Height - 1));

                G.DrawLine(penButtonHilight, new Point(rect.Width, rect.X), new Point(rect.Width, rect.Height));
                G.DrawLine(penButtonHilight, new Point(rect.X, rect.Height), new Point(rect.Width, rect.Height));
            }

            #region Editor

            // If editing is enabled and cursor is on a shadow line area, draw a line over the area
            if (_ColorEdit_Shadow)
            {
                Color color = Color.FromArgb(200, 128, 0, 0);
                using (Pen P = new(color))
                {
                    G.DrawLine(P, btnShadowPoints0[0], btnShadowPoints0[1]);
                    G.DrawLine(P, btnShadowPoints1[0], btnShadowPoints1[1]);
                }
            }

            // If editing is enabled and cursor is on a dark shadow line area, draw a line over the area
            if (_ColorEdit_DkShadow)
            {
                Color color = Color.FromArgb(200, 128, 0, 0);
                using (Pen P = new(color))
                {
                    G.DrawLine(P, btnDkShadowPoints0[0], btnDkShadowPoints0[1]);
                    G.DrawLine(P, btnDkShadowPoints1[0], btnDkShadowPoints1[1]);
                }
            }

            // If editing is enabled and cursor is on a hilight line area, draw a line over the area
            if (_ColorEdit_Hilight)
            {
                Color color = Color.FromArgb(200, 128, 0, 0);
                using (Pen P = new(color))
                {
                    G.DrawLine(P, btnHilightPoints0[0], btnHilightPoints0[1]);
                    G.DrawLine(P, btnHilightPoints1[0], btnHilightPoints1[1]);
                }
            }

            // If editing is enabled and cursor is on a light line area, draw a line over the area
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

            // Draw the text (caption)
            using (SolidBrush br = new(ForeColor))
            {
                G.DrawString(Text, Font, br, new Point(4, 4));
            }

            #region Editor

            // If editing is enabled and cursor is on the caption area, draw a rectangle around the text
            if (_ColorEdit_Text)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, RectText);
                    G.DrawRectangle(P, RectText);
                }
            }

            #endregion

            base.OnPaint(e);


        }
    }
}