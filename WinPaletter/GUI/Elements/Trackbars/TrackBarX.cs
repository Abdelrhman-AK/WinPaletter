using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.Controllers
{
    [DefaultEvent("ValueChanged")]
    public partial class TrackBarX : UserControl
    {
        public TrackBarX()
        {
            InitializeComponent();
            value_btn.Text = Value.ToString();
        }

        public event EventHandler ValueChanged;
        public event EventHandler Scroll;

        public int Value
        {
            get { return trackBar1.Value; }
            set
            {
                trackBar1.Value = value;
                value_btn.Text = Math.Max(Math.Min(value, Maximum), Minimum).ToString();
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }

        public int Minimum
        {
            get { return trackBar1.Minimum; }
            set
            {
                if (trackBar1.Minimum != value)
                {
                    trackBar1.Minimum = value;
                }
            }
        }

        public int Maximum
        {
            get { return trackBar1.Maximum; }
            set
            {
                if (trackBar1.Maximum != value)
                {
                    trackBar1.Maximum = value;
                }
            }
        }

        private int defaultValue = 0;
        public int DefaultValue
        {
            get => defaultValue;
            set
            {
                if (defaultValue != value)
                {
                    defaultValue = value;
                }
            }
        }

        private bool _animateChanges = true;
        public bool AnimateChanges
        {
            get => _animateChanges;
            set
            {
                if (_animateChanges != value)
                {
                    _animateChanges = value;
                }
            }
        }


        private void trackBar1_Scroll(object sender)
        {
            Value = trackBar1.Value;
            textBox1.Text = Value.ToString();
            Scroll?.Invoke(this, new EventArgs());
        }

        private void value_btn_Click(object sender, EventArgs e)
        {
            //UI.WP.Button button = sender as UI.WP.Button;
            //string response = InputBox(Program.Lang.InputValue, button.Text, Program.Lang.ItMustBeNumerical);
            //object value = Math.Max(Math.Min(Conversion.Val(response), Maximum), Minimum);
            //button.Text = value.ToString();

            //if (_animateChanges)
            //{
            //    FluentTransitions.Transition.With(this, nameof(Value), Convert.ToInt32(value)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            //}
            //else
            //{
            //    Value = Convert.ToInt32(value);
            //}

            textBox1.Text = Value.ToString();
            textBox1.BringToFront();
            Program.Animator.ShowSync(textBox1);
            textBox1.Focus();   
        }

        private void textBox1_KeyboardPress(object s, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape)
            {
                if (float.TryParse(textBox1.Text, out float value))
                {
                    Program.Animator.HideSync(textBox1);

                    if (e.KeyChar == (char)Keys.Enter)
                    {
                        if (_animateChanges)
                        {
                            FluentTransitions.Transition.With(this, nameof(Value), Convert.ToInt32(value)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                        }
                        else
                        {
                            Value = Convert.ToInt32(value);
                        }
                    }
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            if (_animateChanges)
            {
                FluentTransitions.Transition.With(this, nameof(Value), Math.Min(Math.Max(defaultValue, Minimum), Maximum)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                Value = Math.Min(Math.Max(defaultValue, Minimum), Maximum);
            }
        }

        private void value_btn_TextChanged(object sender, EventArgs e)
        {
            int innerPadding = trackBar1.Left;
            int width = TextRenderer.MeasureText((value_btn.Text ?? "0"), value_btn.Font).Width + 10;
            value_btn.Width = width;
            value_btn.Left = Width - width;
            reset.Left = value_btn.Left - reset.Width - innerPadding;
            trackBar1.Width = reset.Left - trackBar1.Left - innerPadding;
        }

        private void reset_LocationChanged(object sender, EventArgs e)
        {
            textBox1.Left = reset.Left;
            textBox1.Width = value_btn.Right - textBox1.Left;
        }

        private void reset_MouseEnter(object sender, EventArgs e)
        {
            Program.ToolTip.Show(sender as UI.WP.Button, string.Empty, Program.Lang.ClickToReset, null, new Point(0, (sender as UI.WP.Button).Height + 2));
        }

        private void reset_MouseLeave(object sender, EventArgs e)
        {
            Program.ToolTip.Hide(sender as UI.WP.Button);   
        }

        private void value_btn_MouseEnter(object sender, EventArgs e)
        {
            Program.ToolTip.Show(sender as UI.WP.Button, string.Empty, Program.Lang.ClickToEdit, null, new Point(0, (sender as UI.WP.Button).Height + 2));
        }

        private void value_btn_MouseLeave(object sender, EventArgs e)
        {
            Program.ToolTip.Hide(sender as UI.WP.Button);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            Program.ToolTip.Show(sender as UI.WP.TextBox, string.Empty, $"• {Program.Lang.PressEnterToUseValue}\r\n• {Program.Lang.PressEscToDismissEditing}", null, new Point(0, (sender as UI.WP.TextBox).Height + 2));
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            Program.ToolTip.Hide(sender as UI.WP.TextBox);
        }
    }
}
