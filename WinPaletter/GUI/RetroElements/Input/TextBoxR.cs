using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// TextBox with Windows 9x style
    /// </summary>
    [Description("Retro TextBox with Windows 9x style")]
    [DefaultEvent("TextChanged")]
    public class TextBoxR : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxR"/> class.
        /// </summary>
        public TextBoxR()
        {
            _BaseColor = BackColor;
            _TextColor = ForeColor;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            ForeColor = SystemColors.WindowText;
            BackColor = SystemColors.Control;
            Font = new("Microsoft Sans Serif", 8f);

            // Initialize the masked TextBox
            TB = new()
            {
                Visible = true,
                Font = new("Microsoft Sans Serif", 8f),
                Text = Text,
                ForeColor = SystemColors.ControlText,
                MaxLength = _MaxLength,
                Multiline = _Multiline,
                ReadOnly = _ReadOnly,
                UseSystemPasswordChar = _UseSystemPasswordChar,
                BorderStyle = BorderStyle.None,
                Location = new(1, 0),
                Width = Width - 1,
                Cursor = Cursors.IBeam
            };

            // Set size of the masked TextBox based on the Multiline property correctly
            if (_Multiline)
            {
                TB.Height = Height - 8;
            }
            else
            {
                Height = TB.Height + 8;
            }

            // Assign the events to the masked TextBox
            TB.TextChanged += OnBaseTextChanged;
            TB.KeyDown += OnBaseKeyDown;
        }

        #region Variables

        private readonly Color _BaseColor;
        private readonly Color _TextColor;

        private MouseState State = MouseState.None;

        /// <summary>
        /// Mouse states for the control
        /// </summary>
        public enum MouseState : byte
        {
            /// <summary>
            /// Mouse is not over the control
            /// </summary>
            None = 0,

            /// <summary>
            /// Mouse is over the control
            /// </summary>
            Over = 1,

            /// <summary>
            /// Mouse is pressing the control
            /// </summary>
            Down = 2,
        }

        private TextBox _TB;
        /// <summary>
        /// A masked <see cref="TextBox"/> used to input text
        /// </summary>
        private TextBox TB
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get => _TB;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                // Unsubscribe from the old TextBox events
                if (_TB != null)
                {
                    _TB.MouseDown -= TB_MouseDown;
                    _TB.MouseEnter -= TB_MouseEnter;
                    _TB.MouseLeave -= TB_MouseLeave;
                    _TB.LostFocus -= TB_LostFocus;
                }

                _TB = value;

                // Subscribe to the new TextBox events
                if (_TB != null)
                {
                    _TB.MouseDown += TB_MouseDown;
                    _TB.MouseEnter += TB_MouseEnter;
                    _TB.MouseLeave += TB_MouseLeave;
                    _TB.LostFocus += TB_LostFocus;
                }
            }
        }

        #endregion

        #region Properties
        private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the text alignment of the text in the control
        /// </summary>
        [Category("Options")]
        public HorizontalAlignment TextAlign
        {
            get => _TextAlign;
            set
            {
                _TextAlign = value;
                if (TB is not null)
                {
                    TB.TextAlign = value;
                }
            }
        }

        private int _MaxLength = 32767;

        /// <summary>
        /// Gets or sets the maximum number of characters the user can type into the control
        /// </summary>
        [Category("Options")]
        public int MaxLength
        {
            get => _MaxLength;
            set
            {
                _MaxLength = value;
                if (TB is not null)
                {
                    TB.MaxLength = value;
                }
            }
        }

        private bool _ReadOnly;

        /// <summary>
        /// Gets or sets a value indicating whether the text in the control is read-only
        /// </summary>
        [Category("Options")]
        public bool ReadOnly
        {
            get => _ReadOnly;
            set
            {
                _ReadOnly = value;
                if (TB is not null)
                {
                    TB.ReadOnly = value;
                }
            }
        }

        private bool _UseSystemPasswordChar;

        /// <summary>
        /// Gets or sets a value indicating whether the text in the control should appear as the default password character
        /// </summary>
        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get => _UseSystemPasswordChar;
            set
            {
                _UseSystemPasswordChar = value;
                if (TB is not null)
                {
                    TB.UseSystemPasswordChar = value;
                }
            }
        }

        private bool _Multiline;

        /// <summary>
        /// Gets or sets a value indicating whether this is a multiline TextBox
        /// </summary>
        [Category("Options")]
        public bool Multiline
        {
            get => _Multiline;
            set
            {
                _Multiline = value;
                if (TB is not null)
                {
                    TB.Multiline = value;

                    // Set size of the masked TextBox based on the Multiline property correctly
                    if (value)
                    {
                        TB.Height = Height - 8;
                    }
                    else
                    {
                        Height = TB.Height + 8;
                    }

                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control
        /// </summary>
        [Category("Options")]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                if (TB is not null)
                {
                    TB.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control
        /// </summary>
        [Category("Options")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                if (TB is not null)
                {
                    TB.Font = value;

                    // Set location and size of the masked TextBox based on the font property correctly
                    TB.Location = new(4, 4);
                    TB.Width = Width - 8;
                    if (!_Multiline)
                    {
                        Height = TB.Height + 10;
                    }
                }
            }
        }

        private Color buttonShadow = SystemColors.ButtonShadow;
        private Color buttonDkShadow = SystemColors.ControlDark;
        private Color buttonHilight = SystemColors.ButtonHighlight;
        private Color buttonLight = SystemColors.ControlLight;

        /// <summary>
        /// Gets or sets the color of the button shadow
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

        /// <summary>
        /// Gets or sets the color of the button dark shadow
        /// </summary>
        public Color ButtonDkShadow
        {
            get => buttonDkShadow;
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
        /// Gets or sets the color of the button hilight
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

        /// <summary>
        /// Gets or sets the color of the button light
        /// </summary>
        public Color ButtonLight
        {
            get => buttonLight;
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
        /// Gets or sets the background color of the control
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

                    if (TB is not null)
                    {
                        TB.BackColor = value;
                        TB.Refresh();
                    }
                }
            }
        }

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Void to handle the OnMouseDown event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
            TB.Focus();
        }

        /// <summary>
        /// Void to handle the OnMouseUp event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            TB.Focus();
            Invalidate();
        }

        /// <summary>
        /// Void to handle the OnMouseEnter event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        /// <summary>
        /// Void to handle the OnMouseLeave event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        /// <summary>
        /// Void to handle the OnCreateControl event
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Check if the TextBox is not already added to the control
            if (!Controls.Contains(TB))
            {
                Controls.Add(TB);
            }
        }

        /// <summary>
        /// Changes the text of the control when the masked TextBox text changes
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = TB.Text;
        }

        /// <summary>
        /// Handles the KeyDown event for the masked TextBox
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                // Ctrl + A: Select all text
                TB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                // Ctrl + C: Copy selected text
                TB.Copy();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                // Ctrl + X: Cut selected text
                TB.Cut();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Ctrl + V: Paste text
                TB.Paste();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.Z)
            {
                // Ctrl + Z: Undo
                TB.Undo();
                e.SuppressKeyPress = true;
            }

            Invalidate();
        }

        /// <summary>
        /// Void to handle the OnResize event to adjust the size and location of the masked TextBox
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            TB.Location = new(4, 4);
            TB.Width = Width - 8;

            if (_Multiline)
            {
                TB.Height = Height - 8;
            }
            else
            {
                Height = TB.Height + 10;
            }

            base.OnResize(e);
        }

        /// <summary>
        /// Void to handle the mouse down event for the masked TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_MouseDown(object sender, MouseEventArgs e)
        {
            State = MouseState.Down;
            Invalidate();
        }

        /// <summary>
        /// Void to handle the mouse enter event for the masked TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Invalidate();
        }

        /// <summary>
        /// Void to handle the mouse leave event for the masked TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Invalidate();
        }

        /// <summary>
        /// Void to handle the lost focus event for the masked TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;
            Invalidate();
        }

        /// <summary>
        /// Void to handle the dispose event
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _TB?.Dispose();
        }

        #endregion

        /// <summary>
        /// Void to handle the paint event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            TB.ForeColor = ForeColor;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            // Draw the background
            G.Clear(BackColor);

            // Draw 3D borders
            using (Pen penButtonShadow = new(ButtonShadow))
            using (Pen penButtonDkShadow = new(ButtonDkShadow))
            using (Pen penButtonLight = new(ButtonLight))
            using (Pen penButtonHilight = new(ButtonHilight))
            using (SolidBrush br = new(ForeColor))
            {
                G.DrawLine(penButtonShadow, new Point(rect.X, rect.Y), new Point(rect.Width - 1, rect.Y));
                G.DrawLine(penButtonShadow, new Point(rect.X, rect.Y), new Point(rect.X, rect.Height - 1));

                G.DrawLine(penButtonDkShadow, new Point(rect.X, rect.Y) + (Size)new Point(1, 1), new Point(rect.Width - 2, rect.Y + 1));
                G.DrawLine(penButtonDkShadow, new Point(rect.X, rect.Y) + (Size)new Point(1, 1), new Point(rect.X + 1, rect.Height - 2));

                G.DrawLine(penButtonLight, new Point(rect.Width - 1, 1), new Point(rect.Width - 1, rect.Height - 1));
                G.DrawLine(penButtonLight, new Point(1, rect.Height - 1), new Point(rect.Width - 1, rect.Height - 1));

                G.DrawLine(penButtonHilight, new Point(rect.Width, rect.X), new Point(rect.Width, rect.Height));
                G.DrawLine(penButtonHilight, new Point(rect.X, rect.Height), new Point(rect.Width, rect.Height));

                G.DrawString(TB.Text, Font, br, new Point(2, 4));
            }

            G.Dispose();
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();

            base.OnPaint(e);
        }
    }
}