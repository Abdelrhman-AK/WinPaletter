using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Assets;

namespace WinPaletter.UI.AdvancedControls
{
    public partial class ColorEffectMiniControl : UserControl
    {
        public ColorEffectMiniControl()
        {
            InitializeComponent();
            trackBar.PerformLayout();
        }

        /// <summary>
        /// Occurs when the color send button is clicked.
        /// </summary>
        /// <remarks>This event is triggered when the user interacts with the color send button, allowing
        /// subscribers to handle the action.</remarks>
        public event EventHandler SendColorClick;

        private ColorEffect _colorEffect;

        /// <summary>
        /// Gets or sets the associated <see cref="ColorEffect"/> instance bound to this control.
        /// </summary>
        public ColorEffect ColorEffect
        {
            get => _colorEffect;
            set
            {
                if (_colorEffect != value)
                {
                    _colorEffect = value;
                    UpdateUIFromEffect();
                }
            }
        }

        /// <summary>
        /// Updates the UI elements to reflect the current effect configuration.
        /// </summary>
        private void UpdateUIFromEffect()
        {
            if (_colorEffect == null) return;

            pictureBox.Image = _colorEffect.SmallImage;
            label1.Text = _colorEffect.Name;

            trackBar.Visible = _colorEffect.HasScrollbar;
            trackBar.Minimum = (int)_colorEffect.ScrollbarMin;
            trackBar.Maximum = (int)_colorEffect.ScrollbarMax;
            trackBar.Value = (int)_colorEffect.ScrollbarValue;
            trackBar.DefaultValue = (int)_colorEffect.DefaultValue;

            colorItem1.BackColor = _colorEffect.Apply(_colorEffect.InputColor ?? Color.Empty);

            colorItem2.BackColor = _colorEffect.SecondaryColor;
            colorItem2.Visible = _colorEffect.SecondaryColor != Color.Empty;
        }


        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_colorEffect == null) return;

            _colorEffect.ScrollbarValue = (sender as UI.Controllers.TrackBarX)?.Value ?? 0;
            colorItem1.BackColor = _colorEffect.Apply(_colorEffect.InputColor ?? Color.Empty);
        }

        private void colorItem1_Click(object sender, EventArgs e)
        {
            SendColorClick?.Invoke(this, new ColorEventArgs(colorItem1.BackColor));
        }

        public class ColorEventArgs : EventArgs
        {
            public Color Color { get; set; }

            public ColorEventArgs(Color color)
            { this.Color = color; }
        }

        private void trackBar_VisibleChanged(object sender, EventArgs e)
        {
            // Distance from top of form to bottom of groupBox
            int groupBoxBottomRelativeToForm = groupBox1.Bottom;

            // Extra space below groupBox to the bottom of the form
            int extraPadding = this.Height - groupBoxBottomRelativeToForm;

            if (trackBar.Visible)
            {
                // Expand form to include the trackBar
                this.Height = trackBar.Bottom + extraPadding * 2;
            }
            else
            {
                // Shrink form to align with top of trackBar
                this.Height = trackBar.Top + extraPadding;
            }
        }

        private void colorItem2_VisibleChanged(object sender, EventArgs e)
        {
            trackBar.Width = colorItem2.Visible ? colorItem1.Left - trackBar.Left - 5 : colorItem1.Right - trackBar.Left;
        }

        private void colorItem2_Click(object sender, EventArgs e)
        {
            Dictionary<Control, string[]> dict = new()
            {
                { colorItem2, new[] { nameof(colorItem2.BackColor) } }
            };

            colorItem2.BackColor = Forms.ColorPickerDlg.Pick(dict);

            _colorEffect.SecondaryColor = colorItem2.BackColor;

            colorItem1.BackColor = _colorEffect.Apply(_colorEffect.InputColor ?? Color.Empty);
        }
    }
}
