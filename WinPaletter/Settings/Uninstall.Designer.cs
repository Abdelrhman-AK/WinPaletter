using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Uninstall : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Uninstall));
            Label2 = new Label();
            Label1 = new Label();
            PictureBox1 = new PictureBox();
            Label3 = new Label();
            PictureBox6 = new PictureBox();
            CheckBox1 = new UI.WP.CheckBox();
            PictureBox2 = new PictureBox();
            CheckBox2 = new UI.WP.CheckBox();
            PictureBox3 = new PictureBox();
            RadioImage1 = new UI.WP.RadioImage();
            RadioImage2 = new UI.WP.RadioImage();
            RadioImage3 = new UI.WP.RadioImage();
            Label4 = new Label();
            Separator1 = new UI.WP.SeparatorH();
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            AlertBox4 = new UI.WP.AlertBox();
            CheckBox3 = new UI.WP.CheckBox();
            PictureBox4 = new PictureBox();
            AlertBox1 = new UI.WP.AlertBox();
            OpenFileDialog1 = new OpenFileDialog();
            AnimatedBox1 = new UI.WP.AnimatedBox();
            AlertBox2 = new UI.WP.AlertBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            AnimatedBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(89, 47);
            Label2.Name = "Label2";
            Label2.Size = new Size(603, 24);
            Label2.TabIndex = 2;
            Label2.Text = "This wizard will help you delete WinPaletter's data made during your usage";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label1.Location = new Point(89, 12);
            Label1.Name = "Label1";
            Label1.Size = new Size(445, 35);
            Label1.TabIndex = 1;
            Label1.Text = "WinPaletter uninstaller";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(80, 80);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 1;
            PictureBox1.TabStop = false;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(12, 94);
            Label3.Name = "Label3";
            Label3.Size = new Size(421, 28);
            Label3.TabIndex = 2;
            Label3.Text = "Choose operations to be done:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(29, 125);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 7;
            PictureBox6.TabStop = false;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(59, 125);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(178, 24);
            CheckBox1.TabIndex = 8;
            CheckBox1.Text = "Delete registry associations";
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(29, 185);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 9;
            PictureBox2.TabStop = false;
            // 
            // CheckBox2
            // 
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = true;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(59, 185);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(411, 24);
            CheckBox2.TabIndex = 10;
            CheckBox2.Text = @"Delete application data: %localappdata%\Abdelrhman-AK\WinPaletter";
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(29, 292);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 11;
            PictureBox3.TabStop = false;
            // 
            // RadioImage1
            // 
            RadioImage1.Checked = true;
            RadioImage1.Font = new Font("Segoe UI", 9.0f);
            RadioImage1.ForeColor = Color.White;
            RadioImage1.Image = null;
            RadioImage1.Location = new Point(156, 292);
            RadioImage1.Name = "RadioImage1";
            RadioImage1.ShowText = true;
            RadioImage1.Size = new Size(280, 24);
            RadioImage1.TabIndex = 12;
            RadioImage1.Text = "Do nothing";
            // 
            // RadioImage2
            // 
            RadioImage2.Checked = false;
            RadioImage2.Font = new Font("Segoe UI", 9.0f);
            RadioImage2.ForeColor = Color.White;
            RadioImage2.Image = null;
            RadioImage2.Location = new Point(156, 322);
            RadioImage2.Name = "RadioImage2";
            RadioImage2.ShowText = true;
            RadioImage2.Size = new Size(280, 24);
            RadioImage2.TabIndex = 13;
            RadioImage2.Text = "Restore from a theme file you backed-up before";
            // 
            // RadioImage3
            // 
            RadioImage3.Checked = false;
            RadioImage3.Font = new Font("Segoe UI", 9.0f);
            RadioImage3.ForeColor = Color.White;
            RadioImage3.Image = null;
            RadioImage3.Location = new Point(156, 352);
            RadioImage3.Name = "RadioImage3";
            RadioImage3.ShowText = true;
            RadioImage3.Size = new Size(280, 24);
            RadioImage3.TabIndex = 14;
            RadioImage3.Text = "Restore to default Windows";
            // 
            // Label4
            // 
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(59, 292);
            Label4.Name = "Label4";
            Label4.Size = new Size(91, 24);
            Label4.TabIndex = 15;
            Label4.Text = "Theme restore:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 387);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(671, 1);
            Separator1.TabIndex = 16;
            Separator1.TabStop = false;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button6.BackColor = Color.FromArgb(34, 34, 34);
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.ImageAlign = ContentAlignment.MiddleLeft;
            Button6.LineColor = Color.FromArgb(126, 88, 59);
            Button6.Location = new Point(578, 400);
            Button6.Name = "Button6";
            Button6.Size = new Size(105, 34);
            Button6.TabIndex = 21;
            Button6.Text = "Uninstall";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(492, 400);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 22;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // AlertBox4
            // 
            AlertBox4.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox4.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox4.CenterText = false;
            AlertBox4.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox4.Font = new Font("Segoe UI", 9.0f);
            AlertBox4.Image = null;
            AlertBox4.Location = new Point(59, 215);
            AlertBox4.Name = "AlertBox4";
            AlertBox4.Size = new Size(624, 22);
            AlertBox4.TabIndex = 23;
            AlertBox4.TabStop = false;
            AlertBox4.Text = "Note: This will reset cursors to Aero as the custom cursors are rendered and save" + "d there";
            // 
            // CheckBox3
            // 
            CheckBox3.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox3.Checked = true;
            CheckBox3.Font = new Font("Segoe UI", 9.0f);
            CheckBox3.ForeColor = Color.White;
            CheckBox3.Location = new Point(59, 155);
            CheckBox3.Name = "CheckBox3";
            CheckBox3.Size = new Size(188, 24);
            CheckBox3.TabIndex = 25;
            CheckBox3.Text = "Delete WinPaletter's settings";
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(29, 155);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 24;
            PictureBox4.TabStop = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(87, 71, 71);
            AlertBox1.CenterText = true;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(12, 405);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(474, 24);
            AlertBox1.TabIndex = 26;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "WinPaletter won't be able to delete itself, so delete the exe file after uninstal" + "l";
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // AnimatedBox1
            // 
            AnimatedBox1.Color = Color.Maroon;
            AnimatedBox1.Color1 = Color.Maroon;
            AnimatedBox1.Color2 = Color.Crimson;
            AnimatedBox1.Controls.Add(Label2);
            AnimatedBox1.Controls.Add(PictureBox1);
            AnimatedBox1.Controls.Add(Label1);
            AnimatedBox1.Dock = DockStyle.Top;
            AnimatedBox1.Location = new Point(0, 0);
            AnimatedBox1.Name = "AnimatedBox1";
            AnimatedBox1.Size = new Size(695, 86);
            AnimatedBox1.Style = UI.WP.AnimatedBox.Styles.MixedColors;
            AnimatedBox1.TabIndex = 27;
            // 
            // AlertBox2
            // 
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox2.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox2.CenterText = false;
            AlertBox2.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox2.Font = new Font("Segoe UI", 9.0f);
            AlertBox2.Image = null;
            AlertBox2.Location = new Point(59, 243);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(624, 40);
            AlertBox2.TabIndex = 28;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "Note: Windows (Vista and later) Startup backup wave file is located there. If you" + " delete this folder, you won't be able to restore this sound. Restore this sound" + " first then you can do an uninstall.";
            // 
            // Uninstall
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(695, 446);
            Controls.Add(AlertBox2);
            Controls.Add(AlertBox1);
            Controls.Add(CheckBox3);
            Controls.Add(PictureBox4);
            Controls.Add(AlertBox4);
            Controls.Add(Button2);
            Controls.Add(Button6);
            Controls.Add(Separator1);
            Controls.Add(Label4);
            Controls.Add(RadioImage3);
            Controls.Add(RadioImage2);
            Controls.Add(Label3);
            Controls.Add(RadioImage1);
            Controls.Add(CheckBox1);
            Controls.Add(PictureBox3);
            Controls.Add(PictureBox6);
            Controls.Add(PictureBox2);
            Controls.Add(CheckBox2);
            Controls.Add(AnimatedBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Uninstall";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Uninstall";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            AnimatedBox1.ResumeLayout(false);
            Load += new EventHandler(Uninstall_Load);
            ResumeLayout(false);

        }
        internal PictureBox PictureBox1;
        internal Label Label2;
        internal Label Label1;
        internal Label Label3;
        internal PictureBox PictureBox6;
        internal UI.WP.CheckBox CheckBox1;
        internal PictureBox PictureBox2;
        internal UI.WP.CheckBox CheckBox2;
        internal PictureBox PictureBox3;
        internal UI.WP.RadioImage RadioImage1;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.WP.RadioImage RadioImage3;
        internal Label Label4;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button2;
        internal UI.WP.AlertBox AlertBox4;
        internal UI.WP.CheckBox CheckBox3;
        internal PictureBox PictureBox4;
        internal UI.WP.AlertBox AlertBox1;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal UI.WP.AlertBox AlertBox2;
    }
}