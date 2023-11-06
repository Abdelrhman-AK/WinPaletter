namespace WinPaletter
{
    partial class UserSwitch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSwitch));
            this.checkBox1 = new WinPaletter.UI.WP.CheckBox();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.groupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.alertBox2 = new WinPaletter.UI.WP.AlertBox();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.checkBox1.Checked = false;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(12, 435);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(559, 24);
            this.checkBox1.TabIndex = 138;
            this.checkBox1.Text = "Show system profiles (EXPERIMENTAL - DO IT AT YOUR OWN RISK!)";
            this.checkBox1.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.checkBox1_CheckedChanged);
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.Color = System.Drawing.Color.DodgerBlue;
            this.AnimatedBox1.Color1 = System.Drawing.Color.DodgerBlue;
            this.AnimatedBox1.Color2 = System.Drawing.Color.Crimson;
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.title);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(825, 48);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.SwapColors;
            this.AnimatedBox1.TabIndex = 135;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(7, 7);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(35, 35);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(48, 7);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(765, 35);
            this.title.TabIndex = 85;
            this.title.Text = "Please select a Windows user on whose registry, WinPaletter should get and set da" +
    "ta.";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3);
            this.groupBox1.Size = new System.Drawing.Size(801, 242);
            this.groupBox1.TabIndex = 131;
            this.groupBox1.Text = "groupBox1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(795, 236);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button2.DrawOnGlass = false;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Button2.Location = new System.Drawing.Point(577, 430);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(115, 34);
            this.Button2.TabIndex = 130;
            this.Button2.Text = "Close";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button1.DrawOnGlass = false;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(54)))), ((int)(((byte)(73)))));
            this.Button1.Location = new System.Drawing.Point(698, 430);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(115, 34);
            this.Button1.TabIndex = 129;
            this.Button1.Text = "Switch";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // alertBox2
            // 
            this.alertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.alertBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.alertBox2.CenterText = false;
            this.alertBox2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.alertBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.alertBox2.Image = null;
            this.alertBox2.Location = new System.Drawing.Point(12, 306);
            this.alertBox2.Name = "alertBox2";
            this.alertBox2.Size = new System.Drawing.Size(801, 63);
            this.alertBox2.TabIndex = 139;
            this.alertBox2.TabStop = false;
            this.alertBox2.Text = resources.GetString("alertBox2.Text");
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(12, 375);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(801, 48);
            this.AlertBox1.TabIndex = 137;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "There are some theme items shared between all users (saved in HKEY_LOCAL_MACHINE)" +
    ". So some items you modify in a user might affect other users.";
            // 
            // UserSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(825, 476);
            this.Controls.Add(this.alertBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.AnimatedBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(841, 449);
            this.Name = "UserSwitch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User switch";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UserSwitch_Load);
            this.AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label title;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        private UI.WP.GroupBox groupBox1;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        internal UI.WP.AnimatedBox AnimatedBox1;
        private UI.WP.CheckBox checkBox1;
        internal UI.WP.AlertBox alertBox2;
        internal UI.WP.AlertBox AlertBox1;
    }
}