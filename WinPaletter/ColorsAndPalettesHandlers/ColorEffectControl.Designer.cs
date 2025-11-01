namespace WinPaletter.UI.AdvancedControls
{
    partial class ColorEffectControl
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
            this.groupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.colorItem1 = new WinPaletter.UI.Controllers.ColorItem();
            this.label2 = new System.Windows.Forms.Label();
            this.toggle = new WinPaletter.UI.WP.Toggle();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar = new WinPaletter.UI.Controllers.TrackBarX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.colorItem1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.toggle);
            this.groupBox1.Controls.Add(this.pictureBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBar);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.Text = "groupBox1";
            // 
            // colorItem1
            // 
            this.colorItem1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.colorItem1.BackColor = System.Drawing.Color.Transparent;
            this.colorItem1.DefaultBackColor = System.Drawing.Color.Transparent;
            this.colorItem1.DontShowInfo = false;
            this.colorItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorItem1.Location = new System.Drawing.Point(249, 27);
            this.colorItem1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.colorItem1.Name = "colorItem1";
            this.colorItem1.Size = new System.Drawing.Size(90, 24);
            this.colorItem1.TabIndex = 130;
            this.colorItem1.Visible = false;
            this.colorItem1.BackColorChanged += new System.EventHandler(this.colorItem1_BackColorChanged);
            this.colorItem1.Click += new System.EventHandler(this.colorItem1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(75, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 20);
            this.label2.TabIndex = 129;
            this.label2.Text = "Description";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle
            // 
            this.toggle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle.Checked = false;
            this.toggle.DarkLight_Toggler = false;
            this.toggle.Location = new System.Drawing.Point(347, 29);
            this.toggle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toggle.Name = "toggle";
            this.toggle.Size = new System.Drawing.Size(40, 20);
            this.toggle.TabIndex = 127;
            this.toggle.Text = "toggle1";
            this.toggle.CheckedChanged += new System.EventHandler(this.toggle_CheckedChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox.Location = new System.Drawing.Point(9, 10);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(58, 58);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 126;
            this.pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(75, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 20);
            this.label1.TabIndex = 125;
            this.label1.Text = "Effect";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.AnimateChanges = true;
            this.trackBar.BackColor = System.Drawing.Color.Transparent;
            this.trackBar.DefaultValue = 50;
            this.trackBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBar.Location = new System.Drawing.Point(75, 51);
            this.trackBar.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.trackBar.Maximum = 100;
            this.trackBar.Minimum = 0;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(264, 20);
            this.trackBar.TabIndex = 128;
            this.trackBar.Value = 50;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.VisibleChanged += new System.EventHandler(this.trackBar_VisibleChanged);
            // 
            // ColorEffectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ColorEffectControl";
            this.Size = new System.Drawing.Size(400, 80);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.WP.GroupBox groupBox1;
        private UI.Controllers.TrackBarX trackBar;
        private UI.WP.Toggle toggle;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private UI.Controllers.ColorItem colorItem1;
    }
}
