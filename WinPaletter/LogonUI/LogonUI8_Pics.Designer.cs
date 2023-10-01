using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUI8_Pics : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI8_Pics));
            img4 = new UI.WP.RadioImage();
            img2 = new UI.WP.RadioImage();
            img3 = new UI.WP.RadioImage();
            img1 = new UI.WP.RadioImage();
            img0 = new UI.WP.RadioImage();
            PictureBox1 = new PictureBox();
            Label1 = new Label();
            img5 = new UI.WP.RadioImage();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // img4
            // 
            img4.Checked = false;
            img4.Font = new Font("Segoe UI", 9.0f);
            img4.ForeColor = Color.White;
            img4.Image = (Image)resources.GetObject("img4.Image");
            img4.Location = new Point(146, 187);
            img4.Name = "img4";
            img4.ShowText = false;
            img4.Size = new Size(128, 128);
            img4.TabIndex = 41;
            // 
            // img2
            // 
            img2.Checked = false;
            img2.Font = new Font("Segoe UI", 9.0f);
            img2.ForeColor = Color.White;
            img2.Image = (Image)resources.GetObject("img2.Image");
            img2.Location = new Point(280, 53);
            img2.Name = "img2";
            img2.ShowText = false;
            img2.Size = new Size(128, 128);
            img2.TabIndex = 40;
            // 
            // img3
            // 
            img3.Checked = false;
            img3.Font = new Font("Segoe UI", 9.0f);
            img3.ForeColor = Color.White;
            img3.Image = (Image)resources.GetObject("img3.Image");
            img3.Location = new Point(12, 187);
            img3.Name = "img3";
            img3.ShowText = false;
            img3.Size = new Size(128, 128);
            img3.TabIndex = 39;
            // 
            // img1
            // 
            img1.Checked = false;
            img1.Font = new Font("Segoe UI", 9.0f);
            img1.ForeColor = Color.White;
            img1.Image = (Image)resources.GetObject("img1.Image");
            img1.Location = new Point(146, 53);
            img1.Name = "img1";
            img1.ShowText = false;
            img1.Size = new Size(128, 128);
            img1.TabIndex = 38;
            // 
            // img0
            // 
            img0.Checked = true;
            img0.Font = new Font("Segoe UI", 9.0f);
            img0.ForeColor = Color.White;
            img0.Image = (Image)resources.GetObject("img0.Image");
            img0.Location = new Point(12, 53);
            img0.Name = "img0";
            img0.ShowText = false;
            img0.Size = new Size(128, 128);
            img0.TabIndex = 37;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(12, 12);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(35, 35);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 36;
            PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label1.ForeColor = Color.White;
            Label1.Location = new Point(53, 12);
            Label1.Name = "Label1";
            Label1.Size = new Size(358, 35);
            Label1.TabIndex = 35;
            Label1.Text = "Default backgrounds";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // img5
            // 
            img5.Checked = false;
            img5.Font = new Font("Segoe UI", 9.0f);
            img5.ForeColor = Color.White;
            img5.Image = (Image)resources.GetObject("img5.Image");
            img5.Location = new Point(280, 187);
            img5.Name = "img5";
            img5.ShowText = false;
            img5.Size = new Size(128, 128);
            img5.TabIndex = 42;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(245, 330);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 68;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(52, 20, 64);
            Button1.Location = new Point(331, 330);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 34);
            Button1.TabIndex = 67;
            Button1.Text = "Load";
            Button1.UseVisualStyleBackColor = false;
            // 
            // LogonUI8_Pics
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(423, 376);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(img5);
            Controls.Add(img4);
            Controls.Add(img2);
            Controls.Add(img3);
            Controls.Add(img1);
            Controls.Add(img0);
            Controls.Add(PictureBox1);
            Controls.Add(Label1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogonUI8_Pics";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogonUI - Windows 8 defaults";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(LogonUI8_Pics_Load);
            ResumeLayout(false);

        }

        internal UI.WP.RadioImage img4;
        internal UI.WP.RadioImage img2;
        internal UI.WP.RadioImage img3;
        internal UI.WP.RadioImage img1;
        internal UI.WP.RadioImage img0;
        internal PictureBox PictureBox1;
        internal Label Label1;
        internal UI.WP.RadioImage img5;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
    }
}