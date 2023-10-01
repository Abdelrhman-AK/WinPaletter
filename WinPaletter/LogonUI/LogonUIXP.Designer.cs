using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUIXP : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUIXP));
            OpenFileDialog1 = new OpenFileDialog();
            GroupBox1 = new UI.WP.GroupBox();
            CheckBox1 = new UI.WP.CheckBox();
            PictureBox2 = new PictureBox();
            Label3 = new Label();
            PictureBox7 = new PictureBox();
            color_pick = new UI.Controllers.ColorItem();
            color_pick.Click += new EventHandler(color_pick_Click);
            color_pick.DragDrop += new DragEventHandler(color_pick_Click);
            PictureBox1 = new PictureBox();
            Label5 = new Label();
            GroupBox3 = new UI.WP.GroupBox();
            Label2 = new Label();
            Label1 = new Label();
            RadioImage2 = new UI.WP.RadioImage();
            RadioImage1 = new UI.WP.RadioImage();
            PictureBox6 = new PictureBox();
            Label13 = new Label();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            GroupBox12 = new UI.WP.GroupBox();
            Toggle1 = new UI.WP.Toggle();
            Toggle1.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(Toggle1_CheckedChanged);
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label12 = new Label();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            checker_img = new PictureBox();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(CheckBox1);
            GroupBox1.Controls.Add(PictureBox2);
            GroupBox1.Controls.Add(Label3);
            GroupBox1.Controls.Add(PictureBox7);
            GroupBox1.Controls.Add(color_pick);
            GroupBox1.Controls.Add(PictureBox1);
            GroupBox1.Controls.Add(Label5);
            GroupBox1.Location = new Point(12, 313);
            GroupBox1.Margin = new Padding(4, 3, 4, 3);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(583, 104);
            GroupBox1.TabIndex = 212;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(79, 73);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(269, 25);
            CheckBox1.TabIndex = 115;
            CheckBox1.Text = "Show more options (e.g. shutdown button)";
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(48, 73);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(25, 25);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 114;
            PictureBox2.TabStop = false;
            // 
            // Label3
            // 
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(79, 42);
            Label3.Name = "Label3";
            Label3.Size = new Size(109, 25);
            Label3.TabIndex = 113;
            Label3.Text = "Background color:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(48, 42);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(25, 25);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 94;
            PictureBox7.TabStop = false;
            // 
            // color_pick
            // 
            color_pick.AllowDrop = true;
            color_pick.BackColor = Color.FromArgb(47, 47, 48);
            color_pick.DefaultColor = Color.Black;
            color_pick.DontShowInfo = false;
            color_pick.Location = new Point(195, 42);
            color_pick.Margin = new Padding(4, 3, 4, 3);
            color_pick.Name = "color_pick";
            color_pick.Size = new Size(100, 25);
            color_pick.TabIndex = 93;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(35, 35);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 1;
            PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(44, 3);
            Label5.Name = "Label5";
            Label5.Size = new Size(536, 35);
            Label5.TabIndex = 0;
            Label5.Text = "Tweaks specific for Windows 2000 mode";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(Label2);
            GroupBox3.Controls.Add(Label1);
            GroupBox3.Controls.Add(RadioImage2);
            GroupBox3.Controls.Add(RadioImage1);
            GroupBox3.Controls.Add(PictureBox6);
            GroupBox3.Controls.Add(Label13);
            GroupBox3.Location = new Point(12, 57);
            GroupBox3.Margin = new Padding(4, 3, 4, 3);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(583, 250);
            GroupBox3.TabIndex = 211;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(294, 217);
            Label2.Name = "Label2";
            Label2.Size = new Size(170, 22);
            Label2.TabIndex = 113;
            Label2.Text = "Windows 2000";
            Label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(118, 217);
            Label1.Name = "Label1";
            Label1.Size = new Size(170, 22);
            Label1.TabIndex = 112;
            Label1.Text = "Windows XP";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage2
            // 
            RadioImage2.Checked = false;
            RadioImage2.Font = new Font("Segoe UI", 9.0f);
            RadioImage2.ForeColor = Color.White;
            RadioImage2.Image = (Image)resources.GetObject("RadioImage2.Image");
            RadioImage2.Location = new Point(294, 44);
            RadioImage2.Name = "RadioImage2";
            RadioImage2.ShowText = false;
            RadioImage2.Size = new Size(170, 170);
            RadioImage2.TabIndex = 3;
            RadioImage2.Text = "RadioImage2";
            // 
            // RadioImage1
            // 
            RadioImage1.Checked = false;
            RadioImage1.Font = new Font("Segoe UI", 9.0f);
            RadioImage1.ForeColor = Color.White;
            RadioImage1.Image = (Image)resources.GetObject("RadioImage1.Image");
            RadioImage1.Location = new Point(118, 44);
            RadioImage1.Name = "RadioImage1";
            RadioImage1.ShowText = false;
            RadioImage1.Size = new Size(170, 170);
            RadioImage1.TabIndex = 2;
            RadioImage1.Text = "RadioImage1";
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(3, 3);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(35, 35);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 1;
            PictureBox6.TabStop = false;
            // 
            // Label13
            // 
            Label13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label13.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label13.Location = new Point(44, 3);
            Label13.Name = "Label13";
            Label13.Size = new Size(536, 35);
            Label13.TabIndex = 0;
            Label13.Text = "LogonUI screen type";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button10
            // 
            Button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button10.BackColor = Color.FromArgb(34, 34, 34);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = (Image)resources.GetObject("Button10.Image");
            Button10.ImageAlign = ContentAlignment.MiddleLeft;
            Button10.LineColor = Color.FromArgb(36, 81, 110);
            Button10.Location = new Point(294, 429);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 210;
            Button10.Text = "Quick apply";
            Button10.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.LineColor = Color.FromArgb(199, 49, 61);
            Button7.Location = new Point(208, 429);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 209;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button8.BackColor = Color.FromArgb(34, 34, 34);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleLeft;
            Button8.LineColor = Color.FromArgb(52, 20, 64);
            Button8.Location = new Point(415, 429);
            Button8.Name = "Button8";
            Button8.Size = new Size(180, 34);
            Button8.TabIndex = 208;
            Button8.Text = "Load into current theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(Toggle1);
            GroupBox12.Controls.Add(Button9);
            GroupBox12.Controls.Add(Label12);
            GroupBox12.Controls.Add(Button11);
            GroupBox12.Controls.Add(Button12);
            GroupBox12.Controls.Add(checker_img);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(583, 39);
            GroupBox12.TabIndex = 202;
            // 
            // Toggle1
            // 
            Toggle1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Toggle1.BackColor = Color.FromArgb(43, 43, 43);
            Toggle1.Checked = false;
            Toggle1.DarkLight_Toggler = false;
            Toggle1.Location = new Point(538, 9);
            Toggle1.Name = "Toggle1";
            Toggle1.Size = new Size(40, 20);
            Toggle1.TabIndex = 82;
            // 
            // Button9
            // 
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.ImageAlign = ContentAlignment.MiddleRight;
            Button9.LineColor = Color.FromArgb(90, 134, 117);
            Button9.Location = new Point(222, 5);
            Button9.Name = "Button9";
            Button9.Size = new Size(126, 29);
            Button9.TabIndex = 112;
            Button9.Text = "Current applied";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label12.Location = new Point(4, 4);
            Label12.Name = "Label12";
            Label12.Size = new Size(75, 31);
            Label12.TabIndex = 111;
            Label12.Text = "Open from:";
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            Button11.BackColor = Color.FromArgb(43, 43, 43);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.ImageAlign = ContentAlignment.MiddleRight;
            Button11.LineColor = Color.FromArgb(113, 122, 131);
            Button11.Location = new Point(85, 5);
            Button11.Name = "Button11";
            Button11.Size = new Size(135, 29);
            Button11.TabIndex = 110;
            Button11.Text = "WinPaletter theme";
            Button11.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button12.BackColor = Color.FromArgb(43, 43, 43);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = null;
            Button12.ImageAlign = ContentAlignment.MiddleRight;
            Button12.LineColor = Color.FromArgb(0, 66, 119);
            Button12.Location = new Point(351, 5);
            Button12.Name = "Button12";
            Button12.Size = new Size(135, 29);
            Button12.TabIndex = 108;
            Button12.Text = "Default Windows";
            Button12.UseVisualStyleBackColor = false;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = My.Resources.checker_disabled;
            checker_img.Location = new Point(497, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // LogonUIXP
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(607, 475);
            Controls.Add(GroupBox1);
            Controls.Add(GroupBox3);
            Controls.Add(Button10);
            Controls.Add(Button7);
            Controls.Add(Button8);
            Controls.Add(GroupBox12);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogonUIXP";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogonUI - Windows XP";
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            Load += new EventHandler(LogonUIXP_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Toggle Toggle1;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal PictureBox checker_img;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.GroupBox GroupBox3;
        internal Label Label2;
        internal Label Label1;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.WP.RadioImage RadioImage1;
        internal PictureBox PictureBox6;
        internal Label Label13;
        internal UI.WP.GroupBox GroupBox1;
        internal PictureBox PictureBox1;
        internal Label Label5;
        internal PictureBox PictureBox7;
        internal UI.Controllers.ColorItem color_pick;
        internal UI.WP.CheckBox CheckBox1;
        internal PictureBox PictureBox2;
        internal Label Label3;
    }
}