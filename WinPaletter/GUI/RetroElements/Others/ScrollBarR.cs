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
    /// Retro ScrollBar with Windows 9x style
    /// </summary>
    [Description("Retro ScrollBar with Windows 9x style")]
    public class ScrollBarR : Panel
    {
        /// <summary>
        /// Initialize a new instance of <see cref="ScrollBarR"/>
        /// </summary>
        public ScrollBarR()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(192, 192, 192);
            BorderStyle = BorderStyle.None;

            // Add buttons; Up, Down, Scroll handle
            Controls.Add(btnUp);
            Controls.Add(btnDown);
            Controls.Add(btnScroll);

            // Add event handlers
            btnUp.Click += (s, e) => Value--;
            btnDown.Click += (s, e) => Value++;

            // Scroll handle events
            btnScroll.MouseDown += BtnScroll_MouseDown;
            btnScroll.MouseMove += BtnScroll_MouseMove;
            btnScroll.MouseUp += BtnScroll_MouseUp;

            AdjustLayout();
        }

        #region Variables

        /// <summary>
        /// If true, the mouse is scrolling the handle
        /// </summary>
        private bool invokeMouseScroll = false;

        /// <summary>
        /// Remaining part of the mouse position
        /// </summary>
        private int mouseScrollRemainingPart = 0;

        /// <summary>
        /// If true, the mouse is on the sizing grip and moving it.
        /// </summary>
        bool isMoving_Grip = false;
        Rectangle rect;

        Point MP = Point.Empty;
        #endregion

        #region Properties

        /// <summary>
        /// Color of the button hilight
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
        private Color _ButtonHilight = SystemColors.ButtonHighlight;

        /// <summary>
        /// Value of the scroll bar
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                // Clamp the value
                _value = Math.Max(minimum, Math.Min(maximum, value));
                OnValueChanged(EventArgs.Empty);
            }
        }
        private int _value = 0;

        /// <summary>
        /// Maximum value of the scroll bar
        /// </summary>
        public int Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                if (maximum < minimum)
                    minimum = maximum;
                Value = Math.Min(Value, maximum);
            }
        }
        private int maximum = 100;

        /// <summary>
        /// Minimum value of the scroll bar
        /// </summary>
        public int Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                if (minimum > maximum)
                    maximum = minimum;
                Value = Math.Max(Value, minimum);
            }
        }
        private int minimum = 0;

        /// <summary>
        /// Orientation of the scroll bar
        /// </summary>
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                orientation = value;
                // Swap the width and height
                AdjustLayout();
            }
        }
        private Orientation orientation = Orientation.Vertical;

        /// <summary>
        /// Width of the scroll bar
        /// </summary>
        public new int Width
        {
            get => base.Width;
            set
            {
                // Set minimum and maximum width
                if (value < 13) value = 13;
                if (value > 50) value = 50;

                if (value != base.Width)
                {
                    base.Width = value;

                    // Adjust the layout
                    AdjustLayout();

                    // Adjust the location of scroll handle from the value
                    AdjustLocationFromValue();
                    Refresh();

                    // If editing metrics is enabled and the grip is moving, invoke the editor
                    if (EnableEditingMetrics && isMoving_Grip)
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowMetrics.ScrollWidth)));
                }
            }
        }

        /// <summary>
        /// Height of the scroll bar
        /// </summary>
        public new int Height
        {
            get => base.Height;
            set
            {
                // Set minimum and maximum height
                if (value < 13) value = 13;
                if (value > 50) value = 50;

                if (value != base.Height)
                {
                    base.Height = value;

                    // Adjust the layout
                    AdjustLayout();

                    // Adjust the location of scroll handle from the value
                    AdjustLocationFromValue();
                    Refresh();

                    // If editing metrics is enabled and the grip is moving, invoke the editor
                    if (EnableEditingMetrics && isMoving_Grip)
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowMetrics.ScrollHeight)));
                }
            }
        }

        /// <summary>
        /// If true, the colors can be edited by clicking on the control
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        /// <summary>
        /// If true, the metrics can be edited by clicking on the control
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Event handler for the editor invoker after a click on the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Event handler for the editor invoker after a click on the control
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Event handler for the value changed
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Invokes the value changed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            // Adjust the location of scroll handle from the value
            AdjustLocationFromValue();

            // Invoke the event of value changed
            ValueChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Event handler for the scroll handle mouse move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScroll_MouseMove(object sender, MouseEventArgs e)
        {
            // If the mouse is scrolling the handle, adjust the value from the location
            if (invokeMouseScroll)
            {
                // Get the mouse position
                Point MP = PointToClient(MousePosition);

                // Adjust the location of the scroll handle
                if (orientation == Orientation.Vertical)
                {
                    // Adjust the top location of the scroll handle
                    int y = MP.Y - mouseScrollRemainingPart;
                    btnScroll.Top = Math.Min(btnDown.Top - btnScroll.Height, Math.Max(btnUp.Bottom, y));
                }
                else
                {
                    // Adjust the left location of the scroll handle
                    int x = MP.X - mouseScrollRemainingPart;
                    btnScroll.Left = Math.Min(btnDown.Left - btnScroll.Width, Math.Max(btnUp.Right, x));
                }

                // Adjust the value from the location of the scroll handle
                AdjustValueFromLocation();
            }
        }

        /// <summary>
        /// Event handler for the scroll handle mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScroll_MouseDown(object sender, MouseEventArgs e)
        {
            // If the left mouse button is clicked, invoke the mouse scroll
            if (e.Button == MouseButtons.Left)
            {
                invokeMouseScroll = true;

                // Get the remaining part of the mouse position
                if (orientation == Orientation.Vertical)
                {
                    // Get the remaining part of the mouse position from the top
                    mouseScrollRemainingPart = btnScroll.Height - e.Location.Y;
                }
                else
                {
                    // Get the remaining part of the mouse position from the left
                    mouseScrollRemainingPart = btnScroll.Width - e.Location.X;
                }
            }
        }

        /// <summary>
        /// Event handler for the scroll handle mouse up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScroll_MouseUp(object sender, MouseEventArgs e)
        {
            // If the left mouse button is released, stop invoking the mouse scroll
            invokeMouseScroll = false;
        }

        /// <summary>
        /// Event handler for the resize
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Adjust the layout according to the orientation and size
            AdjustLayout();
        }

        /// <summary>
        /// Event handler for the dock changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);

            // Adjust the layout according to the orientation and size
            AdjustLayout();
        }

        /// <summary>
        /// Event handler for the mouse move
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                // If the mouse is moving the grip and metrics editing is enabled, adjust the width or height

                if (EnableEditingMetrics && isMoving_Grip)
                {
                    if (Dock == DockStyle.Left || Dock == DockStyle.Right)
                    {
                        // Adjust the width from the mouse position
                        int delta = MousePosition.X - MP.X;
                        Width -= delta;
                        MP = MousePosition;
                    }
                    else
                    {
                        // Adjust the height from the mouse position
                        int delta = MousePosition.Y - MP.Y;
                        Height -= delta;
                        MP = MousePosition;
                    }

                    // Set the grip is moving flag to true
                    isMoving_Grip = true;
                }

                // If editing colors is enabled, set the flags according to the cursor position
                if (EnableEditingColors)
                {
                    // Set the cursor on the face or hilight
                    // If the cursor is on the face, set the cursor on the face flag to true
                    // If the cursor is on the hilight, set the cursor on the hilight flag to true
                    // A scrollbar has two parts; face and hilight. Both colors forms a figure of hatch style (50%).
                    // Face color is the even rows and columns, hilight color is the odd rows and columns.
                    CursorOnFace = e.Location.Y % 2 == 0 & e.Location.X % 2 == 0;
                    CursorOnHilight = !CursorOnFace;

                    Refresh();
                }

                else if (EnableEditingMetrics)
                {
                    // Set the cursor on the grip
                    CursorOnMe = rect.Contains(e.Location);

                    if (rect.BordersContains(e.Location) && (e.Location.Y == 0 || e.Location.Y == Height))
                    {
                        // Set cursor style to size NS if the cursor is on the top or bottom border
                        Cursor = Cursors.SizeNS;
                    }
                    else if (rect.BordersContains(e.Location) && (e.Location.X == 0 || e.Location.X == Width))
                    {
                        // Set cursor style to size WE if the cursor is on the left or right border
                        Cursor = Cursors.SizeWE;
                    }
                    else
                    {
                        // Set the default cursor
                        Cursor = Cursors.Default;
                    }

                    Refresh();
                }
            }


            base.OnMouseMove(e);
        }

        /// <summary>
        /// Event handler for the mouse down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                // If the left mouse button is clicked and the cursor is on the grip, set the grip is moving flag to true
                isMoving_Grip = rect.BordersContains(e.Location);
                MP = MousePosition;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Event handler for the mouse leave
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode)
            {
                // Reset the flags
                if (EnableEditingColors)
                {
                    CursorOnFace = false;
                    CursorOnHilight = false;

                    Refresh();
                }

                else if (EnableEditingMetrics)
                {
                    CursorOnMe = false;
                    isMoving_Grip = false;

                    Refresh();
                }

            }

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Event handler for the mouse up
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (EnableEditingMetrics)
                {
                    // If the left mouse button is released, set the grip is moving flag to false
                    isMoving_Grip = false;
                    Refresh();
                }
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Event handler for the mouse click
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            // If the left mouse button is clicked and editing colors is enabled, invoke the editor
            if (EnableEditingColors)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (CursorOnFace)
                    {
                        // Invoke the editor for the face color
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonFace)));
                    }
                    else if (CursorOnHilight)
                    {
                        // Invoke the editor for the hilight color
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));
                    }
                }
            }

            base.OnMouseClick(e);
        }

        /// <summary>
        /// Event handler for the mouse wheel
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                // If the mouse wheel is scrolled up, decrease the value
                Value--;
            }
            else
            {
                // If the mouse wheel is scrolled down, increase the value
                Value++;
            }
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// Disposes the resources
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            btnUp?.Dispose();
            btnDown?.Dispose();
            btnScroll?.Dispose();
        }

        #endregion

        #region Controls

        /// <summary>
        /// Button up for the scroll bar control
        /// </summary>
        private readonly ButtonR btnUp = new() { Text = "t", Font = new("Marlett", 8.8f) };

        /// <summary>
        /// Button down for the scroll bar control
        /// </summary>
        private readonly ButtonR btnDown = new() { Text = "u", Font = new("Marlett", 8.8f) };

        /// <summary>
        /// Scroll handle for the scroll bar control
        /// </summary>
        private readonly ButtonR btnScroll = new() { Text = string.Empty, Height = 50, UseItAsScrollbar = true };

        #endregion

        #region Methods

        /// <summary>
        /// Adjust the layout of the scroll bar according to the orientation and size
        /// </summary>
        private void AdjustLayout()
        {
            if (orientation == Orientation.Vertical)
            {
                btnUp.Dock = DockStyle.Top;
                btnDown.Dock = DockStyle.Bottom;
                btnScroll.Width = Width;
                btnScroll.Left = 0;
                btnScroll.Height = Height / 4;
                btnUp.Height = Width;
                btnDown.Height = Width;
                btnUp.Text = "t";
                btnDown.Text = "u";
            }
            else
            {
                btnUp.Dock = DockStyle.Left;
                btnDown.Dock = DockStyle.Right;
                btnScroll.Height = Height;
                btnScroll.Top = 0;
                btnScroll.Width = Width / 4;
                btnUp.Width = Height;
                btnDown.Width = Height;
                btnUp.Text = "3";
                btnDown.Text = "4";
            }

            rect = new(0, 0, Width - 1, Height - 1);
        }

        /// <summary>
        /// Adjust the location of the scroll handle from the value
        /// </summary>
        private void AdjustLocationFromValue()
        {
            if (orientation == Orientation.Vertical)
            {
                int Length = btnDown.Top - btnUp.Bottom - btnScroll.Height;
                int val = btnUp.Bottom + (int)(_value / (float)(maximum - minimum) * Length);
                btnScroll.Top = Math.Min(btnDown.Top - btnScroll.Height, Math.Max(btnUp.Bottom, val));
            }
            else
            {
                int Length = btnDown.Left - btnUp.Right - btnScroll.Width;
                int val = btnUp.Right + (int)(_value / (float)(maximum - minimum) * Length);
                btnScroll.Left = Math.Min(btnDown.Left - btnScroll.Width, Math.Max(btnUp.Right, val));
            }
        }

        /// <summary>
        /// Adjust the value of the scroll bar from the location of the scroll handle
        /// </summary>
        private void AdjustValueFromLocation()
        {
            if (orientation == Orientation.Vertical)
            {
                int Length = btnDown.Top - btnUp.Bottom - btnScroll.Height;
                Value = (int)((btnScroll.Top - btnUp.Bottom) / (float)Length * (maximum - minimum));
            }
            else
            {
                int Length = btnDown.Left - btnUp.Right - btnScroll.Width;
                Value = (int)((btnScroll.Left - btnUp.Right) / (float)Length * (maximum - minimum));
            }
        }

        #endregion

        #region Colors editor

        private bool CursorOnFace, CursorOnHilight, CursorOnMe;
        private bool _ColorEdit_Hilight => EnableEditingColors && CursorOnHilight;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _MetricsEdit_Grip => !DesignMode && EnableEditingMetrics && CursorOnMe;

        #endregion

        /// <summary>
        /// Paints the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            // Draw the background
            G.Clear(BackColor);

            // Draw the face and hilight
            // A scrollbar has two parts; face and hilight. Both colors forms a figure of hatch style (50%).
            // Face color is the even rows and columns, hilight color is the odd rows and columns.
            using (HatchBrush b = new(HatchStyle.Percent50, ButtonHilight, BackColor)) { G.FillRectangle(b, rect); }

            #region Editor

            // Draw a hatch style on the face to indicate the face color can be edited
            if (_ColorEdit_Face)
            {
                Color color = BackColor.Invert();
                using (HatchBrush hb = new(HatchStyle.Percent50, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
            }

            // Draw a hatch style on the hilight to indicate the hilight color can be edited
            if (_ColorEdit_Hilight)
            {
                Color color = ButtonHilight.Invert();
                using (HatchBrush hb = new(HatchStyle.Percent50, Color.Transparent, color)) { G.FillRectangle(hb, rect); }
            }

            #endregion

            #region Grip

            // Draw the grip on the scrollbar to indicate the metrics can be edited
            if (_MetricsEdit_Grip)
            {
                using (Pen P = new(BackColor.Invert()) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(P, rect);
                }
            }

            #endregion


        }
    }
}