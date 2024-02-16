﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [DefaultEvent("ValueChanged")]
    public partial class ColorBarX : UserControl
    {
        public ColorBarX()
        {
            InitializeComponent();
            ResetMaxMin();
            value_btn.Text = Value.ToString();
        }

        public event EventHandler ValueChanged;
        public event EventHandler Scroll;

        public int Value
        {
            get => colorBar1.Value;
            set
            {
                colorBar1.Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Color AccentColor
        {
            get => colorBar1.AccentColor;
            set
            {
                if (value != colorBar1.AccentColor)
                {
                    colorBar1.AccentColor = value;
                    Refresh();
                }
            }
        }

        private void ResetMaxMin()
        {
            switch (Mode)
            {
                case ColorBar.ModesList.Hue:
                    colorBar1.Maximum = 360;
                    colorBar1.Minimum = 0;
                    break;

                case ColorBar.ModesList.Saturation:
                    colorBar1.Maximum = 100;
                    colorBar1.Minimum = 0;
                    break;

                case ColorBar.ModesList.Light:
                    colorBar1.Maximum = 100;
                    colorBar1.Minimum = 0;
                    break;
            }
        }

        public UI.WP.ColorBar.ModesList Mode
        {
            get { return colorBar1.Mode; }
            set
            {
                if (colorBar1.Mode != value)
                {
                    colorBar1.Mode = value;

                    ResetMaxMin();
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

        private void value_btn_Click(object sender, EventArgs e)
        {
            //string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            //((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), colorBar1.Maximum), colorBar1.Minimum).ToString();

            //if (_animateChanges)
            //{
            //    FluentTransitions.Transition.With(this, nameof(Value), (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text))).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            //}
            //else
            //{
            //    Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
            //}

            textBox1.Text = Value.ToString();
            textBox1.BringToFront();
            Program.Animator.ShowSync(textBox1);
            textBox1.Focus();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            switch (colorBar1.Mode)
            {
                case UI.WP.ColorBar.ModesList.Hue:
                    Value = 0;
                    break;

                case UI.WP.ColorBar.ModesList.Saturation:
                    Value = 50;
                    break;

                case UI.WP.ColorBar.ModesList.Light:
                    Value = 50;
                    break;
            }
        }

        private void colorBar1_Scroll_1(object sender)
        {
            value_btn.Text = colorBar1.Value.ToString();
            Value = colorBar1.Value;
            textBox1.Text = Value.ToString();
            Scroll?.Invoke(this, EventArgs.Empty);
        }

        private void reset_LocationChanged(object sender, EventArgs e)
        {
            textBox1.Left = reset.Left;
            textBox1.Width = value_btn.Right - textBox1.Left;
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
                        value_btn.Text = value.ToString();

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
