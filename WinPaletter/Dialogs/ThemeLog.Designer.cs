namespace WinPaletter
{
    partial class ThemeLog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemeLog));
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new WinPaletter.UI.WP.Button();
            this.Button25 = new WinPaletter.UI.WP.Button();
            this.Button22 = new WinPaletter.UI.WP.Button();
            this.Button14 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.animatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.PictureBox36 = new System.Windows.Forms.PictureBox();
            this.log_lbl = new System.Windows.Forms.Label();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.animatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView1
            // 
            this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView1.ForeColor = System.Drawing.Color.White;
            this.TreeView1.FullRowSelect = true;
            this.TreeView1.ItemHeight = 28;
            this.TreeView1.Location = new System.Drawing.Point(12, 63);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.ShowLines = false;
            this.TreeView1.Size = new System.Drawing.Size(710, 489);
            this.TreeView1.TabIndex = 25;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.CustomColor = System.Drawing.Color.DarkGreen;
            this.button1.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = null;
            this.button1.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("button1.ImageGlyph")));
            this.button1.ImageGlyphEnabled = true;
            this.button1.Location = new System.Drawing.Point(9, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 34);
            this.button1.TabIndex = 32;
            this.button1.Text = "Rescue tools";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Button25
            // 
            this.Button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button25.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(3)))), ((int)(((byte)(13)))));
            this.Button25.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button25.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button25.ForeColor = System.Drawing.Color.White;
            this.Button25.Image = null;
            this.Button25.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button25.ImageGlyph")));
            this.Button25.ImageGlyphEnabled = true;
            this.Button25.Location = new System.Drawing.Point(148, 3);
            this.Button25.Name = "Button25";
            this.Button25.Size = new System.Drawing.Size(120, 34);
            this.Button25.TabIndex = 30;
            this.Button25.Text = "Stop timer";
            this.Button25.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button25.UseVisualStyleBackColor = false;
            this.Button25.Visible = false;
            this.Button25.Click += new System.EventHandler(this.Button25_Click);
            // 
            // Button22
            // 
            this.Button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button22.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(137)))), ((int)(((byte)(219)))));
            this.Button22.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button22.ForeColor = System.Drawing.Color.White;
            this.Button22.Image = null;
            this.Button22.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button22.ImageGlyph")));
            this.Button22.ImageGlyphEnabled = true;
            this.Button22.Location = new System.Drawing.Point(274, 3);
            this.Button22.Name = "Button22";
            this.Button22.Size = new System.Drawing.Size(120, 34);
            this.Button22.TabIndex = 29;
            this.Button22.Text = "Export details";
            this.Button22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button22.UseVisualStyleBackColor = false;
            this.Button22.Visible = false;
            this.Button22.Click += new System.EventHandler(this.Button22_Click);
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            this.Button14.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = null;
            this.Button14.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button14.ImageGlyph")));
            this.Button14.ImageGlyphEnabled = true;
            this.Button14.Location = new System.Drawing.Point(22, 3);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(120, 34);
            this.Button14.TabIndex = 27;
            this.Button14.Text = "Show errors";
            this.Button14.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button14.UseVisualStyleBackColor = false;
            this.Button14.Visible = false;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button8.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(84)))), ((int)(((byte)(43)))));
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = null;
            this.Button8.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button8.ImageGlyph")));
            this.Button8.ImageGlyphEnabled = true;
            this.Button8.Location = new System.Drawing.Point(400, 3);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(120, 34);
            this.Button8.TabIndex = 26;
            this.Button8.Text = "OK";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Visible = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // animatedBox1
            // 
            this.animatedBox1.BackColor = System.Drawing.Color.Transparent;
            this.animatedBox1.Color1 = System.Drawing.Color.DodgerBlue;
            this.animatedBox1.Color2 = System.Drawing.Color.Crimson;
            this.animatedBox1.Controls.Add(this.PictureBox36);
            this.animatedBox1.Controls.Add(this.log_lbl);
            this.animatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.animatedBox1.Location = new System.Drawing.Point(0, 0);
            this.animatedBox1.Name = "animatedBox1";
            this.animatedBox1.Size = new System.Drawing.Size(734, 57);
            this.animatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.SwapColors;
            this.animatedBox1.TabIndex = 31;
            this.animatedBox1.Text = "animatedBox1";
            // 
            // PictureBox36
            // 
            this.PictureBox36.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox36.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox36.Image")));
            this.PictureBox36.Location = new System.Drawing.Point(12, 12);
            this.PictureBox36.Name = "PictureBox36";
            this.PictureBox36.Size = new System.Drawing.Size(35, 35);
            this.PictureBox36.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox36.TabIndex = 23;
            this.PictureBox36.TabStop = false;
            // 
            // log_lbl
            // 
            this.log_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log_lbl.BackColor = System.Drawing.Color.Transparent;
            this.log_lbl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.log_lbl.Location = new System.Drawing.Point(53, 12);
            this.log_lbl.Name = "log_lbl";
            this.log_lbl.Size = new System.Drawing.Size(669, 35);
            this.log_lbl.TabIndex = 24;
            this.log_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.flowLayoutPanel1);
            this.bottom_buttons.Controls.Add(this.button1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 563);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(734, 48);
            this.bottom_buttons.TabIndex = 119;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.Button8);
            this.flowLayoutPanel1.Controls.Add(this.Button22);
            this.flowLayoutPanel1.Controls.Add(this.Button25);
            this.flowLayoutPanel1.Controls.Add(this.Button14);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(208, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(523, 40);
            this.flowLayoutPanel1.TabIndex = 120;
            // 
            // ThemeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(734, 611);
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.animatedBox1);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ThemeLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Theme log";
            this.Load += new System.EventHandler(this.ThemeLog_Load);
            this.animatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal UI.WP.Button Button25;
        internal UI.WP.Button Button22;
        internal UI.WP.Button Button14;
        internal UI.WP.Button Button8;
        internal System.Windows.Forms.TreeView TreeView1;
        internal System.Windows.Forms.Label log_lbl;
        internal System.Windows.Forms.PictureBox PictureBox36;
        private UI.WP.AnimatedBox animatedBox1;
        private System.Windows.Forms.Timer timer1;
        internal UI.WP.Button button1;
        private UI.WP.GroupBox bottom_buttons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
