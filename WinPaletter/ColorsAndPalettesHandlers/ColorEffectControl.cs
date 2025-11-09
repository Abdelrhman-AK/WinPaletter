using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;

namespace WinPaletter.UI.AdvancedControls
{
    public partial class ColorEffectControl : UserControl
    {
        bool _intialized = false;

        public ColorEffectControl()
        {
            InitializeComponent();
            trackBar.PerformLayout();
        }

        /// <summary>
        /// Adjusts control layout dynamically based on visible elements and their measured sizes.
        /// </summary>
        public void AdjustLayout()
        {
            SuspendLayout();

            // Measure label heights and total stack height
            int nameHeight = label1.Height;
            int descHeight = label2.Height;
            int labelsTotalHeight = nameHeight + descHeight;
            int trackBarHeight = trackBar.Visible ? trackBar.Height : 0; // 6px margin if shown

            int paddingTop = pictureBox.Top;
            int paddingBetweenLabels = label2.Top - label1.Bottom;

            if (trackBar.Visible)
            {
                // Place name + desc above the trackbar
                int labelTop = paddingTop;
                label1.Location = new Point(label1.Location.X, labelTop);
                label2.Location = new Point(label2.Location.X, label1.Bottom + paddingBetweenLabels);

                // Place trackbar below description with small gap
                trackBar.Location = new Point(trackBar.Location.X, label2.Bottom + paddingBetweenLabels);
            }
            else
            {
                // Center both labels vertically together inside the groupbox
                int labelTop = (groupBox1.Height - (labelsTotalHeight + paddingBetweenLabels)) / 2;
                label1.Location = new Point(label1.Location.X, labelTop);
                label2.Location = new Point(label2.Location.X, label1.Bottom + paddingBetweenLabels);
            }

            ResumeLayout();
        }

        /// <summary>
        /// Occurs whenever the user changes any property that should trigger reprocessing the color effect.
        /// </summary>
        public event EventHandler ProcessColorEffect;

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

            toggle.Checked = _colorEffect.Checked;

            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl != toggle && ctrl != pictureBox) ctrl.Enabled = toggle.Checked;
            }

            pictureBox.Image = _colorEffect.Image ?? _colorEffect.SmallImage;
            label1.Text = _colorEffect.Name;
            label2.Text = _colorEffect.Description;

            trackBar.Visible = _colorEffect.HasScrollbar;
            trackBar.Minimum = (int)_colorEffect.ScrollbarMin;
            trackBar.Maximum = (int)_colorEffect.ScrollbarMax;
            trackBar.Value = (int)_colorEffect.ScrollbarValue;
            trackBar.DefaultValue = (int)_colorEffect.DefaultValue;

            colorItem1.BackColor = _colorEffect.SecondaryColor;
            colorItem1.Visible = _colorEffect.SecondaryColor != Color.Empty;
        }


        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_colorEffect == null) return;

            _colorEffect.ScrollbarValue = (sender as UI.Controllers.TrackBarX)?.Value ?? 0;
            ProcessColorEffect?.Invoke(this, EventArgs.Empty);
        }

        private void toggle_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl != sender && ctrl != pictureBox) ctrl.Enabled = (sender as Toggle).Checked;
            }

            if (_colorEffect == null) return;

            _colorEffect.Checked = (sender as UI.WP.Toggle)?.Checked ?? false;
            ProcessColorEffect?.Invoke(this, EventArgs.Empty);
        }

        private void colorItem1_Click(object sender, EventArgs e)
        {
            UI.Controllers.ColorItem colorItem = sender as UI.Controllers.ColorItem;

            Dictionary<Control, string[]> dict = new()
            {
                { colorItem, new[] { nameof(colorItem.BackColor) } }
            };

            _intialized = true;

            colorItem.BackColor = Forms.ColorPickerDlg.Pick(dict);
        }

        private void colorItem1_BackColorChanged(object sender, EventArgs e)
        {
            if (_colorEffect == null) return;

            if (_intialized)
            {
                _colorEffect.SecondaryColor = (sender as UI.Controllers.ColorItem).BackColor;

                colorItem1.Visible = _colorEffect.SecondaryColor != Color.Empty;

                ProcessColorEffect?.Invoke(this, EventArgs.Empty);
            }
        }

        private void trackBar_VisibleChanged(object sender, EventArgs e)
        {
            AdjustLayout();
        }
    }
}
