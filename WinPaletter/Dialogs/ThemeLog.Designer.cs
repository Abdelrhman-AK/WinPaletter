namespace WinPaletter.Dialogs
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
            this.animatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).BeginInit();
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
            this.TreeView1.Location = new System.Drawing.Point(12, 64);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.ShowLines = false;
            this.TreeView1.Size = new System.Drawing.Size(710, 493);
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
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.button1.DrawOnGlass = false;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(56)))), ((int)(((byte)(61)))));
            this.button1.Location = new System.Drawing.Point(12, 565);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 34);
            this.button1.TabIndex = 32;
            this.button1.Text = "Rescue tools";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Button25
            // 
            this.Button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button25.DrawOnGlass = false;
            this.Button25.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button25.ForeColor = System.Drawing.Color.White;
            this.Button25.Image = ((System.Drawing.Image)(resources.GetObject("Button25.Image")));
            this.Button25.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button25.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(107)))), ((int)(((byte)(111)))));
            this.Button25.Location = new System.Drawing.Point(350, 565);
            this.Button25.Name = "Button25";
            this.Button25.Size = new System.Drawing.Size(120, 34);
            this.Button25.TabIndex = 30;
            this.Button25.Text = "Stop timer";
            this.Button25.UseVisualStyleBackColor = false;
            this.Button25.Visible = false;
            this.Button25.Click += new System.EventHandler(this.Button25_Click);
            // 
            // Button22
            // 
            this.Button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button22.DrawOnGlass = false;
            this.Button22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button22.ForeColor = System.Drawing.Color.White;
            this.Button22.Image = ((System.Drawing.Image)(resources.GetObject("Button22.Image")));
            this.Button22.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button22.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(109)))), ((int)(((byte)(147)))));
            this.Button22.Location = new System.Drawing.Point(476, 565);
            this.Button22.Name = "Button22";
            this.Button22.Size = new System.Drawing.Size(120, 34);
            this.Button22.TabIndex = 29;
            this.Button22.Text = "Export details";
            this.Button22.UseVisualStyleBackColor = false;
            this.Button22.Visible = false;
            this.Button22.Click += new System.EventHandler(this.Button22_Click);
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button14.DrawOnGlass = false;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = ((System.Drawing.Image)(resources.GetObject("Button14.Image")));
            this.Button14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button14.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(49)))), ((int)(((byte)(57)))));
            this.Button14.Location = new System.Drawing.Point(224, 565);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(120, 34);
            this.Button14.TabIndex = 27;
            this.Button14.Text = "Show errors";
            this.Button14.UseVisualStyleBackColor = false;
            this.Button14.Visible = false;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button8.DrawOnGlass = false;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button8.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(102)))), ((int)(((byte)(61)))));
            this.Button8.Location = new System.Drawing.Point(602, 565);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(120, 34);
            this.Button8.TabIndex = 26;
            this.Button8.Text = "OK";
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // animatedBox1
            // 
            this.animatedBox1.Color = System.Drawing.Color.DodgerBlue;
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
            // ThemeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(734, 611);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Button25);
            this.Controls.Add(this.Button22);
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.Button14);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.animatedBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ThemeLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Theme log";
            this.Load += new System.EventHandler(this.ThemeLog_Load);
            this.animatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).EndInit();
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
    }
}