using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.Templates;

namespace WinPaletter.UI.Retro
{
    [Description("Retro ScrollBar with Windows 9x style")]
    public class ScrollBarR : Panel
    {
        public ScrollBarR()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(192, 192, 192);
            BorderStyle = BorderStyle.None;

            Controls.Add(btnUp);
            Controls.Add(btnDown);
            Controls.Add(btnScroll);

            btnUp.Click += (s, e) => Value--;
            btnDown.Click += (s, e) => Value++;

            btnScroll.MouseDown += BtnScroll_MouseDown;
            btnScroll.MouseMove += BtnScroll_MouseMove;
            btnScroll.MouseUp += BtnScroll_MouseUp;

            AdjustLayout();
        }

        #region Variables

        private bool invokeMouseScroll = false;
        private int mouseScrollRemainingPart = 0;
        bool isMoving_Grip = false;
        Rectangle rect;

        Point MP = Point.Empty;
        #endregion

        #region Properties

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


        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = Math.Max(minimum, Math.Min(maximum, value));
                OnValueChanged(EventArgs.Empty);
            }
        }


        private int maximum = 100;
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


        private int minimum = 0;
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


        private Orientation orientation = Orientation.Vertical;
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                orientation = value;
                AdjustLayout();
            }
        }

        public new int Width
        {
            get => base.Width;
            set
            {
                if (value < 13) value = 13;
                if (value > 50) value = 50;

                if (value != base.Width)
                {
                    base.Width = value;
                    AdjustLayout();
                    AdjustLocationFromValue();
                    Refresh();

                    if (EnableEditingMetrics && isMoving_Grip)
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowMetrics.ScrollWidth)));
                }
            }
        }

        public new int Height
        {
            get => base.Height;
            set
            {
                if (value < 13) value = 13;
                if (value > 50) value = 50;

                if (value != base.Height)
                {
                    base.Height = value;
                    AdjustLayout();
                    AdjustLocationFromValue();
                    Refresh();

                    if (EnableEditingMetrics && isMoving_Grip)
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowMetrics.ScrollHeight)));
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

        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            AdjustLocationFromValue();

            ValueChanged?.Invoke(this, e);
        }

        private void BtnScroll_MouseMove(object sender, MouseEventArgs e)
        {
            if (invokeMouseScroll)
            {
                Point MP = PointToClient(MousePosition);

                if (orientation == Orientation.Vertical)
                {
                    int y = MP.Y - mouseScrollRemainingPart;
                    btnScroll.Top = Math.Min(btnDown.Top - btnScroll.Height, Math.Max(btnUp.Bottom, y));
                }
                else
                {
                    int x = MP.X - mouseScrollRemainingPart;
                    btnScroll.Left = Math.Min(btnDown.Left - btnScroll.Width, Math.Max(btnUp.Right, x));
                }

                AdjustValueFromLocation();
            }
        }

        private void BtnScroll_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                invokeMouseScroll = true;

                if (orientation == Orientation.Vertical)
                {
                    mouseScrollRemainingPart = btnScroll.Height - e.Location.Y;
                }
                else
                {
                    mouseScrollRemainingPart = btnScroll.Width - e.Location.X;
                }
            }
        }

        private void BtnScroll_MouseUp(object sender, MouseEventArgs e)
        {
            invokeMouseScroll = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            AdjustLayout();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (EnableEditingMetrics && isMoving_Grip)
                {
                    if (Dock == DockStyle.Left || Dock == DockStyle.Right)
                    {
                        int delta = MousePosition.X - MP.X;
                        Width -= delta;
                        MP = MousePosition;
                    }
                    else
                    {
                        int delta = MousePosition.Y - MP.Y;
                        Height -= delta;
                        MP = MousePosition;
                    }

                    isMoving_Grip = true;
                }

                if (EnableEditingColors)
                {
                    CursorOnFace = e.Location.Y % 2 == 0 & e.Location.X % 2 == 0;
                    CursorOnHilight = !CursorOnFace;

                    Refresh();
                }

                else if (EnableEditingMetrics)
                {
                    CursorOnMe = rect.Contains(e.Location);

                    if (rect.BordersContains(e.Location) && (e.Location.Y == 0 || e.Location.Y == Height))
                    {
                        Cursor = Cursors.SizeNS;
                    }
                    else if (rect.BordersContains(e.Location) && (e.Location.X == 0 || e.Location.X == Width))
                    {
                        Cursor = Cursors.SizeWE;
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                    }

                    Refresh();
                }
            }


            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingMetrics)
            {
                isMoving_Grip = rect.BordersContains(e.Location);
                MP = MousePosition;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode)
            {
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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (EnableEditingMetrics)
                {
                    isMoving_Grip = false;
                    Refresh();
                }
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (EnableEditingColors)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (CursorOnFace)
                    {
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonFace)));
                    }
                    else if (CursorOnHilight)
                    {
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonHilight)));
                    }
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Value--;
            }
            else
            {
                Value++;
            }
            base.OnMouseWheel(e);
        }

        #endregion

        #region Controls

        private ButtonR btnUp = new() { Text = "t", Font = new("Marlett", 8.8f) };
        private ButtonR btnDown = new() { Text = "u", Font = new("Marlett", 8.8f) };
        private ButtonR btnScroll = new() { Text = string.Empty, Height = 50, UseItAsScrollbar = true };

        #endregion

        #region Methods

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

        private void AdjustLocationFromValue()
        {
            if (orientation == Orientation.Vertical)
            {
                int Length = btnDown.Top - btnUp.Bottom - btnScroll.Height;
                int val = btnUp.Bottom + (int)((float)_value / (float)(maximum - minimum) * Length);
                btnScroll.Top = Math.Min(btnDown.Top - btnScroll.Height, Math.Max(btnUp.Bottom, val));
            }
            else
            {
                int Length = btnDown.Left - btnUp.Right - btnScroll.Width;
                int val = btnUp.Right + (int)((float)_value / (float)(maximum - minimum) * Length);
                btnScroll.Left = Math.Min(btnDown.Left - btnScroll.Width, Math.Max(btnUp.Right, val));
            }
        }

        private void AdjustValueFromLocation()
        {
            if (orientation == Orientation.Vertical)
            {
                int Length = btnDown.Top - btnUp.Bottom - btnScroll.Height;
                Value = (int)((float)(btnScroll.Top - btnUp.Bottom) / (float)Length * (maximum - minimum));
            }
            else
            {
                int Length = btnDown.Left - btnUp.Right - btnScroll.Width;
                Value = (int)((float)(btnScroll.Left - btnUp.Right) / (float)Length * (maximum - minimum));
            }
        }

        #endregion

        #region Colors editor

        private bool CursorOnFace, CursorOnHilight, CursorOnMe;
        private bool _ColorEdit_Hilight => EnableEditingColors && CursorOnHilight;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _MetricsEdit_Grip => !DesignMode && EnableEditingMetrics && CursorOnMe;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            G.Clear(BackColor);
            using (HatchBrush b = new(HatchStyle.Percent50, ButtonHilight, BackColor)) { G.FillRectangle(b, rect); }

            #region Editor

            if (_ColorEdit_Face)
            {
                Color color = BackColor.Invert();
                using (HatchBrush hb = new(HatchStyle.Percent50, color, Color.Transparent)) { G.FillRectangle(hb, rect); }
            }

            if (_ColorEdit_Hilight)
            {
                Color color = ButtonHilight.Invert();
                using (HatchBrush hb = new(HatchStyle.Percent50, Color.Transparent, color)) { G.FillRectangle(hb, rect); }
            }

            #endregion

            #region Grip

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