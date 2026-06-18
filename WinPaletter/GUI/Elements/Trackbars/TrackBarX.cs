using FluentTransitions;
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

            value_btn.Text = Value.ToString();
            value_btn.Font = Fonts.ConsoleMedium;
            textBox1.Font = Fonts.ConsoleMedium;

            AdjustLayout();

            Program.ToolTip.SetToolTip(value_btn, Program.Localization.Strings.Tips.ClickToEdit);
            Program.ToolTip.SetToolTip(reset, Program.Localization.Strings.Tips.ClickToReset);
            Program.ToolTip.SetToolTip(textBox1, $"• {Program.Localization.Strings.Tips.PressEnterToUseValue}\r\n• {Program.Localization.Strings.Tips.PressEscToDismissEditing}");
        }

        public event EventHandler ValueChanged;
        public new event EventHandler Scroll;

        public float Value
        {
            get => trackBar1.Value;
            set
            {
                trackBar1.Value = value;
                value_btn.Text = Math.Round(Math.Max(Math.Min(value, Maximum), Minimum), 2).ToString();
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }

        public float Minimum
        {
            get => trackBar1.Minimum;
            set
            {
                if (trackBar1.Minimum != value)
                {
                    trackBar1.Minimum = value;
                }
            }
        }

        public float Maximum
        {
            get => trackBar1.Maximum;
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

        private void AdjustLayout()
        {
            int innerPadding = trackBar1.Left;
            int width = TextRenderer.MeasureText(value_btn.Text ?? "0", value_btn.Font).Width + 10;
            value_btn.Width = width;
            value_btn.Left = Width - width;
            reset.Left = value_btn.Left - reset.Width - innerPadding;
            trackBar1.Width = reset.Left - trackBar1.Left - innerPadding;
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
            //string response = InputBox(Program.modifiedLang.InputValue, button.Text, Program.modifiedLang.ItMustBeNumerical);
            //object value = Math.Max(Math.Min(Conversion.Val(response), Maximum), Minimum);
            //button.Text = value.ToString();

            //if (_animateChanges)
            //{
            //    FluentTransitions.Transition.With(this, nameof(Value), (int)value).CriticalDamp(Program.AnimationSpan);
            //}
            //else
            //{
            //    Value = (int)value
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
                            Transition.With(this, nameof(Value), (int)value).CriticalDamp(Program.AnimationSpan);
                        }
                        else
                        {
                            Value = (int)value;
                        }
                    }
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            if (_animateChanges)
            {
                Transition.With(this, nameof(Value), Math.Min(Math.Max(defaultValue, Minimum), Maximum)).CriticalDamp(Program.AnimationSpan);
            }
            else
            {
                Value = Math.Min(Math.Max(defaultValue, Minimum), Maximum);
            }
        }

        private void value_btn_TextChanged(object sender, EventArgs e)
        {
            AdjustLayout();
        }

        private void reset_LocationChanged(object sender, EventArgs e)
        {
            textBox1.Left = reset.Left;
            textBox1.Width = value_btn.Right - textBox1.Left;
        }

        private void TrackBarX_Load(object sender, EventArgs e)
        {
            AdjustLayout();
        }
    }
}
