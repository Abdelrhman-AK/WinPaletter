using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ComplexSave : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComplexSave));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.RadioImage3 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage1 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.RadioImage7 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage4 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage5 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage6 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.CheckBox2 = new WinPaletter.UI.WP.CheckBox();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.AnimatedBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(9, 12);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(35, 35);
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // Label17
            // 
            this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(50, 12);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(438, 35);
            this.Label17.TabIndex = 1;
            this.Label17.Text = "Current theme has been changed. Choose what you want.";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.RadioImage3);
            this.GroupBox1.Controls.Add(this.RadioImage2);
            this.GroupBox1.Controls.Add(this.RadioImage1);
            this.GroupBox1.Controls.Add(this.PictureBox2);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 65);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(476, 165);
            this.GroupBox1.TabIndex = 3;
            this.GroupBox1.Text = "GroupBox1";
            // 
            // RadioImage3
            // 
            this.RadioImage3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioImage3.Checked = true;
            this.RadioImage3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage3.ForeColor = System.Drawing.Color.White;
            this.RadioImage3.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage3.Image")));
            this.RadioImage3.ImageWithText = true;
            this.RadioImage3.Location = new System.Drawing.Point(3, 125);
            this.RadioImage3.Name = "RadioImage3";
            this.RadioImage3.ShowText = true;
            this.RadioImage3.Size = new System.Drawing.Size(470, 35);
            this.RadioImage3.TabIndex = 9;
            this.RadioImage3.Text = "Don\'t save";
            this.RadioImage3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioImage2
            // 
            this.RadioImage2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioImage2.Checked = false;
            this.RadioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage2.ForeColor = System.Drawing.Color.White;
            this.RadioImage2.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage2.Image")));
            this.RadioImage2.ImageWithText = true;
            this.RadioImage2.Location = new System.Drawing.Point(3, 84);
            this.RadioImage2.Name = "RadioImage2";
            this.RadioImage2.ShowText = true;
            this.RadioImage2.Size = new System.Drawing.Size(470, 35);
            this.RadioImage2.TabIndex = 7;
            this.RadioImage2.Text = "Save as ...";
            this.RadioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioImage1
            // 
            this.RadioImage1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioImage1.Checked = false;
            this.RadioImage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage1.ForeColor = System.Drawing.Color.White;
            this.RadioImage1.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage1.Image")));
            this.RadioImage1.ImageWithText = true;
            this.RadioImage1.Location = new System.Drawing.Point(3, 43);
            this.RadioImage1.Name = "RadioImage1";
            this.RadioImage1.ShowText = true;
            this.RadioImage1.Size = new System.Drawing.Size(470, 35);
            this.RadioImage1.TabIndex = 5;
            this.RadioImage1.Text = "Save";
            this.RadioImage1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(3, 3);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(35, 35);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 1;
            this.PictureBox2.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(44, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(429, 35);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Theme file";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox2.Controls.Add(this.RadioImage7);
            this.GroupBox2.Controls.Add(this.RadioImage4);
            this.GroupBox2.Controls.Add(this.RadioImage5);
            this.GroupBox2.Controls.Add(this.RadioImage6);
            this.GroupBox2.Controls.Add(this.PictureBox3);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Location = new System.Drawing.Point(12, 236);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(476, 205);
            this.GroupBox2.TabIndex = 4;
            this.GroupBox2.Text = "GroupBox2";
            // 
            // RadioImage7
            // 
            this.RadioImage7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioImage7.Checked = false;
            this.RadioImage7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage7.ForeColor = System.Drawing.Color.White;
            this.RadioImage7.Image = null;
            this.RadioImage7.ImageWithText = true;
            this.RadioImage7.Location = new System.Drawing.Point(3, 125);
            this.RadioImage7.Name = "RadioImage7";
            this.RadioImage7.ShowText = true;
            this.RadioImage7.Size = new System.Drawing.Size(470, 35);
            this.RadioImage7.TabIndex = 17;
            this.RadioImage7.Text = "Default Windows";
            this.RadioImage7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioImage4
            // 
            this.RadioImage4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioImage4.Checked = false;
            this.RadioImage4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage4.ForeColor = System.Drawing.Color.White;
            this.RadioImage4.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage4.Image")));
            this.RadioImage4.ImageWithText = true;
            this.RadioImage4.Location = new System.Drawing.Point(3, 166);
            this.RadioImage4.Name = "RadioImage4";
            this.RadioImage4.ShowText = true;
            this.RadioImage4.Size = new System.Drawing.Size(470, 35);
            this.RadioImage4.TabIndex = 15;
            this.RadioImage4.Text = "Don\'t apply";
            this.RadioImage4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioImage5
            // 
            this.RadioImage5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioImage5.Checked = false;
            this.RadioImage5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage5.ForeColor = System.Drawing.Color.White;
            this.RadioImage5.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage5.Image")));
            this.RadioImage5.ImageWithText = true;
            this.RadioImage5.Location = new System.Drawing.Point(3, 84);
            this.RadioImage5.Name = "RadioImage5";
            this.RadioImage5.ShowText = true;
            this.RadioImage5.Size = new System.Drawing.Size(470, 35);
            this.RadioImage5.TabIndex = 13;
            this.RadioImage5.Text = "First theme (undo applied changes)";
            this.RadioImage5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioImage6
            // 
            this.RadioImage6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioImage6.Checked = true;
            this.RadioImage6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage6.ForeColor = System.Drawing.Color.White;
            this.RadioImage6.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage6.Image")));
            this.RadioImage6.ImageWithText = true;
            this.RadioImage6.Location = new System.Drawing.Point(3, 43);
            this.RadioImage6.Name = "RadioImage6";
            this.RadioImage6.ShowText = true;
            this.RadioImage6.Size = new System.Drawing.Size(470, 35);
            this.RadioImage6.TabIndex = 11;
            this.RadioImage6.Text = "Current theme";
            this.RadioImage6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(3, 3);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(35, 35);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox3.TabIndex = 1;
            this.PictureBox3.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(44, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(429, 35);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Apply";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Button1.DrawOnGlass = false;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.Location = new System.Drawing.Point(406, 453);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 34);
            this.Button1.TabIndex = 5;
            this.Button1.Text = "Do actions";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Button2.DrawOnGlass = false;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.Location = new System.Drawing.Point(320, 453);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 6;
            this.Button2.Text = "Close";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Button3.DrawOnGlass = false;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.Location = new System.Drawing.Point(234, 453);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(80, 34);
            this.Button3.TabIndex = 7;
            this.Button3.Text = "Cancel";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // CheckBox2
            // 
            this.CheckBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox2.Checked = true;
            this.CheckBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox2.ForeColor = System.Drawing.Color.White;
            this.CheckBox2.Location = new System.Drawing.Point(12, 459);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(143, 23);
            this.CheckBox2.TabIndex = 9;
            this.CheckBox2.Text = "Always show this";
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.Transparent;
            this.AnimatedBox1.Color1 = System.Drawing.Color.DodgerBlue;
            this.AnimatedBox1.Color2 = System.Drawing.Color.Crimson;
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label17);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(499, 59);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.SwapColors;
            this.AnimatedBox1.TabIndex = 10;
            this.AnimatedBox1.Text = "AnimatedBox1";
            // 
            // ComplexSave
            // 
            this.AcceptButton = this.Button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(499, 499);
            this.Controls.Add(this.AnimatedBox1);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComplexSave";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Theme operations";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComplexSave_FormClosing);
            this.Load += new System.EventHandler(this.ComplexSave_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.AnimatedBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal Label Label17;
        internal UI.WP.GroupBox GroupBox1;
        internal PictureBox PictureBox2;
        internal Label Label1;
        internal UI.WP.GroupBox GroupBox2;
        internal PictureBox PictureBox3;
        internal Label Label2;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.RadioImage RadioImage1;
        internal UI.WP.RadioImage RadioImage3;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.WP.RadioImage RadioImage7;
        internal UI.WP.RadioImage RadioImage4;
        internal UI.WP.RadioImage RadioImage5;
        internal UI.WP.RadioImage RadioImage6;
        internal UI.WP.AnimatedBox AnimatedBox1;
    }
}