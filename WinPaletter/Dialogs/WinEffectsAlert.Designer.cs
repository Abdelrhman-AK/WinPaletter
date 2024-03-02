namespace WinPaletter.Dialogs
{
    partial class WinEffectsAlert
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinEffectsAlert));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.checkBox1 = new WinPaletter.UI.WP.CheckBox();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.bottom_buttons.SuspendLayout();
            this.AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(863, 23);
            this.label1.TabIndex = 127;
            this.label1.Text = "You should consider one or more of the following points:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(844, 80);
            this.label2.TabIndex = 128;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(863, 23);
            this.label3.TabIndex = 129;
            this.label3.Text = "If you don\'t use any third-party tools, ignore this alert and agree to these note" +
    "s.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(12, 59);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(860, 34);
            this.Button2.TabIndex = 117;
            this.Button2.Text = "I disagree, don\'t apply Windows effects";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.ErrorOnHover)));
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(12, 14);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(860, 34);
            this.Button1.TabIndex = 116;
            this.Button1.Text = "I agree with the previous notes, and I am resposnible for the conflict between Wi" +
    "nPaletter\'s Windows Effects and third-party tools";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.checkBox1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 221);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(884, 140);
            this.bottom_buttons.TabIndex = 125;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Checked = false;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(12, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(860, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Never show this dialog again (Windows Effects will be applied without any alert n" +
    "ext time)";
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(36)))), ((int)(((byte)(1)))));
            this.AnimatedBox1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(36)))), ((int)(((byte)(1)))));
            this.AnimatedBox1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(121)))), ((int)(((byte)(2)))));
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label7);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(884, 60);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors;
            this.AnimatedBox1.TabIndex = 124;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(12, 13);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(35, 35);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(53, 13);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(826, 35);
            this.Label7.TabIndex = 85;
            this.Label7.Text = "Customizing Windows Effects is for advanced users and may override preferences se" +
    "t in third-party tools such as ExplorerPatcher.";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WinEffectsAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(884, 361);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.AnimatedBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinEffectsAlert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Effects alert";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinEffectsAlert_FormClosing);
            this.Load += new System.EventHandler(this.WinEffectsAlert_Load);
            this.bottom_buttons.ResumeLayout(false);
            this.AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.WP.GroupBox bottom_buttons;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        private UI.WP.CheckBox checkBox1;
        internal System.Windows.Forms.Label label3;
    }
}