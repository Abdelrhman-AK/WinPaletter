namespace WinPaletter.UI.Controllers
{
    partial class TrackBarX
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar1 = new WinPaletter.UI.WP.TrackBar();
            this.reset = new WinPaletter.UI.WP.Button();
            this.value_btn = new WinPaletter.UI.WP.Button();
            this.textBox1 = new WinPaletter.UI.WP.TextBox();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.BackColor = System.Drawing.Color.Transparent;
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(5, 6);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 0;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(255, 19);
            this.trackBar1.SmallChange = 1;
            this.trackBar1.TabIndex = 138;
            this.trackBar1.Text = "trackBar2";
            this.trackBar1.Value = 0;
            this.trackBar1.Scroll += new WinPaletter.UI.WP.TrackBar.ScrollEventHandler(this.trackBar1_Scroll);
            // 
            // reset
            // 
            this.reset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reset.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.reset.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.reset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.reset.ForeColor = System.Drawing.Color.White;
            this.reset.Image = null;
            this.reset.ImageGlyphEnabled = true;
            this.reset.ImageGlyph = global::WinPaletter.Properties.Resources.Vector_Reset;
            this.reset.Location = new System.Drawing.Point(266, 0);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(34, 31);
            this.reset.TabIndex = 137;
            this.reset.UseVisualStyleBackColor = false;
            this.reset.LocationChanged += new System.EventHandler(this.reset_LocationChanged);
            this.reset.Click += new System.EventHandler(this.reset_Click);
            this.reset.MouseEnter += new System.EventHandler(this.reset_MouseEnter);
            this.reset.MouseLeave += new System.EventHandler(this.reset_MouseLeave);
            // 
            // value_btn
            // 
            this.value_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.value_btn.CustomColor = System.Drawing.Color.Empty;
            this.value_btn.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.value_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.value_btn.ForeColor = System.Drawing.Color.White;
            this.value_btn.Image = null;
            this.value_btn.ImageGlyphEnabled = true;
            this.value_btn.ImageGlyph = null;
            this.value_btn.Location = new System.Drawing.Point(306, 0);
            this.value_btn.Name = "value_btn";
            this.value_btn.Size = new System.Drawing.Size(34, 31);
            this.value_btn.TabIndex = 136;
            this.value_btn.UseVisualStyleBackColor = false;
            this.value_btn.TextChanged += new System.EventHandler(this.value_btn_TextChanged);
            this.value_btn.Click += new System.EventHandler(this.value_btn_Click);
            this.value_btn.MouseEnter += new System.EventHandler(this.value_btn_MouseEnter);
            this.value_btn.MouseLeave += new System.EventHandler(this.value_btn_MouseLeave);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(266, 3);
            this.textBox1.MaxLength = 32767;
            this.textBox1.Multiline = false;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = false;
            this.textBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.Size = new System.Drawing.Size(74, 24);
            this.textBox1.TabIndex = 139;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox1.UseSystemPasswordChar = false;
            this.textBox1.Visible = false;
            this.textBox1.WordWrap = true;
            this.textBox1.KeyboardPress += new WinPaletter.UI.WP.TextBox.KeyboardPressEventHandler(this.textBox1_KeyboardPress);
            this.textBox1.MouseEnter += new System.EventHandler(this.textBox1_MouseEnter);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            // 
            // TrackBarX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.value_btn);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TrackBarX";
            this.Size = new System.Drawing.Size(340, 31);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.WP.TrackBar trackBar1;
        internal UI.WP.Button reset;
        internal UI.WP.Button value_btn;
        private WP.TextBox textBox1;
    }
}
