namespace WinPaletter.Dialogs
{
    partial class ArgsHelp
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
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.button1 = new WinPaletter.UI.WP.Button();
            this.button2 = new WinPaletter.UI.WP.Button();
            this.SuspendLayout();
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 502);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(560, 1);
            this.Separator1.TabIndex = 3;
            this.Separator1.TabStop = false;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(12, 12);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(560, 482);
            this.TextBox1.TabIndex = 2;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.CustomColor = System.Drawing.Color.Empty;
            this.button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.Image = null;
            this.button1.ImageGlyphEnabled = false;
            this.button1.ImageGlyph = null;
            this.button1.Location = new System.Drawing.Point(458, 511);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.CustomColor = System.Drawing.Color.Empty;
            this.button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.Image = null;
            this.button2.ImageGlyphEnabled = false;
            this.button2.ImageGlyph = null;
            this.button2.Location = new System.Drawing.Point(282, 511);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 38);
            this.button2.TabIndex = 5;
            this.button2.Text = "Save as *.txt";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ArgsHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.TextBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ArgsHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Command line help";
            this.Load += new System.EventHandler(this.ArgsHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.TextBox TextBox1;
        private UI.WP.Button button1;
        private UI.WP.Button button2;
    }
}