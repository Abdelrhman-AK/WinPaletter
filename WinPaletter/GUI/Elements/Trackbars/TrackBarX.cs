using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
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
                value_btn.Text = value.ToString();

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
            value_btn.Text = trackBar1.Value.ToString();
            Value = trackBar1.Value;
            Scroll?.Invoke(this, new EventArgs());
        }

        private void value_btn_Click(object sender, EventArgs e)
        {
            UI.WP.Button button = sender as UI.WP.Button;
            string response = InputBox(Program.Lang.InputValue, button.Text, Program.Lang.ItMustBeNumerical);
            object value = Math.Max(Math.Min(Conversion.Val(response), Maximum), Minimum);
            button.Text = value.ToString();

            if (_animateChanges)
            {
                FluentTransitions.Transition.With(this, nameof(Value), Convert.ToInt32(value)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                Value = Convert.ToInt32(value);
            }
        }

        private void undo_Click(object sender, EventArgs e)
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
            undo.Left = value_btn.Left - undo.Width - innerPadding;
            trackBar1.Width = undo.Left - trackBar1.Left - innerPadding;
        }
    }
}
