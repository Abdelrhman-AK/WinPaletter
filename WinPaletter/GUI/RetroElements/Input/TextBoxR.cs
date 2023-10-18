using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    [Description("Retro TextBox with Windows 9x style")]
    [DefaultEvent("TextChanged")]
    public class TextBoxR : Control
    {

        public TextBoxR()
        {
            _BaseColor = BackColor;
            _TextColor = ForeColor;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            ForeColor = Color.Black;
            BackColor = Color.White;
            Font = new Font("Microsoft Sans Serif", 8f);

            TB = new TextBox()
            {
                Visible = true,
                Font = new Font("Microsoft Sans Serif", 8f),
                Text = Text,
                ForeColor = Color.White,
                MaxLength = _MaxLength,
                Multiline = _Multiline,
                ReadOnly = _ReadOnly,
                UseSystemPasswordChar = _UseSystemPasswordChar,
                BorderStyle = BorderStyle.None,
                Location = new Point(1, 0),
                Width = Width - 1
            };

            TB.Cursor = Cursors.IBeam;

            if (_Multiline)
            {
                TB.Height = Height - 8;
            }
            else
            {
                Height = TB.Height + 8;
            }

            TB.TextChanged += OnBaseTextChanged;
            TB.KeyDown += OnBaseKeyDown;
            BackColorChanged += TextBoxR_BackColorChanged;
        }

        #region Variables

        private readonly Color _BaseColor;
        private readonly Color _TextColor;

        private MouseState State = MouseState.None;

        public enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        private TextBox _TB;

        private TextBox TB
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TB;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TB != null)
                {
                    _TB.MouseDown -= TB_MouseDown;
                    _TB.MouseEnter -= TB_MouseEnter;
                    _TB.MouseLeave -= TB_MouseLeave;
                    _TB.LostFocus -= TB_LostFocus;
                }

                _TB = value;
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
        [Category("Options")]
        public HorizontalAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
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
        [Category("Options")]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
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
        [Category("Options")]
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
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
        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return _UseSystemPasswordChar;
            }
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
        [Category("Options")]
        public bool Multiline
        {
            get
            {
                return _Multiline;
            }
            set
            {
                _Multiline = value;
                if (TB is not null)
                {
                    TB.Multiline = value;

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

        [Category("Options")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (TB is not null)
                {
                    TB.Text = value;
                }
            }
        }

        [Category("Options")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                if (TB is not null)
                {
                    TB.Font = value;
                    TB.Location = new Point(4, 4);
                    TB.Width = Width - 8;
                    if (!_Multiline)
                    {
                        Height = TB.Height + 10;
                    }
                }
            }
        }

        public Color ButtonShadow { get; set; } = Color.FromArgb(128, 128, 128);
        public Color ButtonDkShadow { get; set; } = Color.Black;
        public Color ButtonHilight { get; set; } = Color.White;
        public Color ButtonLight { get; set; } = Color.FromArgb(192, 192, 192);
        #endregion

        #region Events
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
            TB.Focus();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            TB.Focus();
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(TB))
            {
                Controls.Add(TB);
            }
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = TB.Text;
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                TB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                TB.Copy();
                e.SuppressKeyPress = true;
            }
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            TB.Location = new Point(4, 4);
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

        private void TB_MouseDown(object sender, MouseEventArgs e)
        {
            State = MouseState.Down;
            Invalidate();
        }

        private void TB_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Invalidate();
        }

        private void TB_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Invalidate();
        }

        private void TB_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;
            Invalidate();
        }

        private void TextBoxR_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                if (TB is not null)
                    TB.BackColor = BackColor;
            }
            catch
            {
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var B = new Bitmap(Width, Height);
            var G = Graphics.FromImage(B);
            G = Graphics.FromImage(B);
            DoubleBuffered = true;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.RenderingHint;

            base.OnPaint(e);
            TB.ForeColor = ForeColor;

            // ################################################################################# Customizer
            var CheckRect = new Rectangle(0, 0, Width - 1, Height - 1);
            // #################################################################################

            G.Clear(BackColor);

            G.DrawLine(new Pen(ButtonShadow), new Point(CheckRect.X, CheckRect.Y), new Point(CheckRect.Width - 1, CheckRect.Y));
            G.DrawLine(new Pen(ButtonShadow), new Point(CheckRect.X, CheckRect.Y), new Point(CheckRect.X, CheckRect.Height - 1));
            G.DrawLine(new Pen(ButtonDkShadow), new Point(CheckRect.X, CheckRect.Y) + (Size)new Point(1, 1), new Point(CheckRect.Width - 2, CheckRect.Y + 1));
            G.DrawLine(new Pen(ButtonDkShadow), new Point(CheckRect.X, CheckRect.Y) + (Size)new Point(1, 1), new Point(CheckRect.X + 1, CheckRect.Height - 2));
            G.DrawLine(new Pen(ButtonLight), new Point(CheckRect.Width - 1, 1), new Point(CheckRect.Width - 1, CheckRect.Height - 1));
            G.DrawLine(new Pen(ButtonLight), new Point(1, CheckRect.Height - 1), new Point(CheckRect.Width - 1, CheckRect.Height - 1));
            G.DrawLine(new Pen(ButtonHilight), new Point(CheckRect.Width, CheckRect.X), new Point(CheckRect.Width, CheckRect.Height));
            G.DrawLine(new Pen(ButtonHilight), new Point(CheckRect.X, CheckRect.Height), new Point(CheckRect.Width, CheckRect.Height));

            G.DrawString(TB.Text, Font, new SolidBrush(ForeColor), new Point(2, 4));

            G.Dispose();
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

    }

}