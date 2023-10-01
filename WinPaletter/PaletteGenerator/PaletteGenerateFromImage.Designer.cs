using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class PaletteGenerateFromImage : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerateFromImage));
            OpenFileDialog1 = new OpenFileDialog();
            GroupBox4 = new UI.WP.GroupBox();
            Label3 = new Label();
            PictureBox1 = new PictureBox();
            RadioButton6 = new UI.WP.RadioButton();
            RadioButton6.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton3_CheckedChanged);
            RadioButton3 = new UI.WP.RadioButton();
            RadioButton3.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton3_CheckedChanged);
            RadioButton4 = new UI.WP.RadioButton();
            RadioButton4.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton3_CheckedChanged);
            RadioButton7 = new UI.WP.RadioButton();
            RadioButton7.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton3_CheckedChanged);
            RadioButton5 = new UI.WP.RadioButton();
            RadioButton5.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton3_CheckedChanged);
            GroupBox3 = new UI.WP.GroupBox();
            PictureBox8 = new PictureBox();
            val1 = new UI.WP.Button();
            val1.Click += new EventHandler(val1_Click);
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            PictureBox9 = new PictureBox();
            Label7 = new Label();
            PictureBox7 = new PictureBox();
            Trackbar2 = new UI.WP.Trackbar();
            Trackbar2.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar2_Scroll);
            Label6 = new Label();
            val2 = new UI.WP.Button();
            val2.Click += new EventHandler(val2_Click);
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            GroupBox2 = new UI.WP.GroupBox();
            PictureBox2 = new PictureBox();
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            RadioButton1 = new UI.WP.RadioImage();
            RadioButton1.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioButton1_CheckedChanged);
            RadioButton2 = new UI.WP.RadioImage();
            RadioButton2.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioButton2_CheckedChanged);
            Label2 = new Label();
            GroupBox1 = new UI.WP.GroupBox();
            PictureBox5 = new PictureBox();
            Label1 = new Label();
            ImgPaletteContainer = new FlowLayoutPanel();
            Separator1 = new UI.WP.SeparatorH();
            AlertBox1 = new UI.WP.AlertBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp|All Files|*.*";
            // 
            // GroupBox4
            // 
            GroupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox4.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox4.Controls.Add(Label3);
            GroupBox4.Controls.Add(PictureBox1);
            GroupBox4.Controls.Add(RadioButton6);
            GroupBox4.Controls.Add(RadioButton3);
            GroupBox4.Controls.Add(RadioButton4);
            GroupBox4.Controls.Add(RadioButton7);
            GroupBox4.Controls.Add(RadioButton5);
            GroupBox4.Location = new Point(12, 145);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Size = new Size(610, 118);
            GroupBox4.TabIndex = 169;
            GroupBox4.Text = "GroupBox4";
            // 
            // Label3
            // 
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(33, 3);
            Label3.Name = "Label3";
            Label3.Size = new Size(305, 24);
            Label3.TabIndex = 169;
            Label3.Text = "Options for extracted colors brightness:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 163;
            PictureBox1.TabStop = false;
            // 
            // RadioButton6
            // 
            RadioButton6.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton6.Checked = false;
            RadioButton6.Font = new Font("Segoe UI", 9.0f);
            RadioButton6.ForeColor = Color.White;
            RadioButton6.Location = new Point(328, 60);
            RadioButton6.Name = "RadioButton6";
            RadioButton6.Size = new Size(277, 24);
            RadioButton6.TabIndex = 168;
            RadioButton6.Text = "Make colors darker";
            // 
            // RadioButton3
            // 
            RadioButton3.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton3.Checked = true;
            RadioButton3.Font = new Font("Segoe UI", 9.0f);
            RadioButton3.ForeColor = Color.White;
            RadioButton3.Location = new Point(45, 30);
            RadioButton3.Name = "RadioButton3";
            RadioButton3.Size = new Size(277, 24);
            RadioButton3.TabIndex = 164;
            RadioButton3.Text = "Don't change colors brightness";
            // 
            // RadioButton4
            // 
            RadioButton4.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton4.Checked = false;
            RadioButton4.Font = new Font("Segoe UI", 9.0f);
            RadioButton4.ForeColor = Color.White;
            RadioButton4.Location = new Point(45, 90);
            RadioButton4.Name = "RadioButton4";
            RadioButton4.Size = new Size(277, 24);
            RadioButton4.TabIndex = 165;
            RadioButton4.Text = "Make colors extremely bright";
            // 
            // RadioButton7
            // 
            RadioButton7.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton7.Checked = false;
            RadioButton7.Font = new Font("Segoe UI", 9.0f);
            RadioButton7.ForeColor = Color.White;
            RadioButton7.Location = new Point(328, 90);
            RadioButton7.Name = "RadioButton7";
            RadioButton7.Size = new Size(277, 24);
            RadioButton7.TabIndex = 167;
            RadioButton7.Text = "Make colors extremely dark";
            // 
            // RadioButton5
            // 
            RadioButton5.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton5.Checked = false;
            RadioButton5.Font = new Font("Segoe UI", 9.0f);
            RadioButton5.ForeColor = Color.White;
            RadioButton5.Location = new Point(45, 60);
            RadioButton5.Name = "RadioButton5";
            RadioButton5.Size = new Size(277, 24);
            RadioButton5.TabIndex = 166;
            RadioButton5.Text = "Make colors brighter";
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(PictureBox8);
            GroupBox3.Controls.Add(val1);
            GroupBox3.Controls.Add(Trackbar1);
            GroupBox3.Controls.Add(PictureBox9);
            GroupBox3.Controls.Add(Label7);
            GroupBox3.Controls.Add(PictureBox7);
            GroupBox3.Controls.Add(Trackbar2);
            GroupBox3.Controls.Add(Label6);
            GroupBox3.Controls.Add(val2);
            GroupBox3.Controls.Add(CheckBox1);
            GroupBox3.Location = new Point(12, 48);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(610, 91);
            GroupBox3.TabIndex = 166;
            GroupBox3.Text = "GroupBox3";
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(3, 3);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 149;
            PictureBox8.TabStop = false;
            // 
            // val1
            // 
            val1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            val1.BackColor = Color.FromArgb(34, 34, 34);
            val1.DrawOnGlass = false;
            val1.Font = new Font("Segoe UI", 9.0f);
            val1.ForeColor = Color.White;
            val1.Image = null;
            val1.LineColor = Color.FromArgb(0, 81, 210);
            val1.Location = new Point(572, 3);
            val1.Name = "val1";
            val1.Size = new Size(34, 24);
            val1.TabIndex = 153;
            val1.UseVisualStyleBackColor = false;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar1.LargeChange = 10;
            Trackbar1.Location = new Point(149, 6);
            Trackbar1.Maximum = 100;
            Trackbar1.Minimum = 13;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(417, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 152;
            Trackbar1.Value = 13;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(3, 33);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 154;
            PictureBox9.TabStop = false;
            // 
            // Label7
            // 
            Label7.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label7.Location = new Point(33, 33);
            Label7.Name = "Label7";
            Label7.Size = new Size(110, 24);
            Label7.TabIndex = 155;
            Label7.Text = "Quality";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(3, 63);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 150;
            PictureBox7.TabStop = false;
            // 
            // Trackbar2
            // 
            Trackbar2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar2.LargeChange = 10;
            Trackbar2.Location = new Point(149, 36);
            Trackbar2.Maximum = 100;
            Trackbar2.Minimum = 0;
            Trackbar2.Name = "Trackbar2";
            Trackbar2.Size = new Size(417, 19);
            Trackbar2.SmallChange = 1;
            Trackbar2.TabIndex = 156;
            Trackbar2.Value = 10;
            // 
            // Label6
            // 
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(33, 3);
            Label6.Name = "Label6";
            Label6.Size = new Size(110, 24);
            Label6.TabIndex = 151;
            Label6.Text = "Maximum colors";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // val2
            // 
            val2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            val2.BackColor = Color.FromArgb(34, 34, 34);
            val2.DrawOnGlass = false;
            val2.Font = new Font("Segoe UI", 9.0f);
            val2.ForeColor = Color.White;
            val2.Image = null;
            val2.LineColor = Color.FromArgb(0, 81, 210);
            val2.Location = new Point(572, 33);
            val2.Name = "val2";
            val2.Size = new Size(34, 24);
            val2.TabIndex = 157;
            val2.UseVisualStyleBackColor = false;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(33, 63);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(180, 24);
            CheckBox1.TabIndex = 148;
            CheckBox1.Text = "Ignore white colors";
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(PictureBox2);
            GroupBox2.Controls.Add(TextBox1);
            GroupBox2.Controls.Add(Button4);
            GroupBox2.Controls.Add(RadioButton1);
            GroupBox2.Controls.Add(RadioButton2);
            GroupBox2.Controls.Add(Label2);
            GroupBox2.Location = new Point(12, 12);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(610, 30);
            GroupBox2.TabIndex = 165;
            GroupBox2.Text = "GroupBox2";
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(3, 3);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 142;
            PictureBox2.TabStop = false;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(283, 3);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(282, 24);
            TextBox1.TabIndex = 137;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(34, 34, 34);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.LineColor = Color.FromArgb(184, 153, 68);
            Button4.Location = new Point(572, 3);
            Button4.Margin = new Padding(4, 3, 4, 3);
            Button4.Name = "Button4";
            Button4.Size = new Size(34, 24);
            Button4.TabIndex = 138;
            Button4.UseVisualStyleBackColor = false;
            // 
            // RadioButton1
            // 
            RadioButton1.Checked = true;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Image = null;
            RadioButton1.Location = new Point(86, 3);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.ShowText = true;
            RadioButton1.Size = new Size(119, 24);
            RadioButton1.TabIndex = 139;
            RadioButton1.Text = "Current wallpaper";
            // 
            // RadioButton2
            // 
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Image = null;
            RadioButton2.Location = new Point(211, 3);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.ShowText = true;
            RadioButton2.Size = new Size(66, 24);
            RadioButton2.TabIndex = 140;
            RadioButton2.Text = "Image";
            // 
            // Label2
            // 
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(33, 3);
            Label2.Name = "Label2";
            Label2.Size = new Size(47, 24);
            Label2.TabIndex = 141;
            Label2.Text = "Source";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(PictureBox5);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(ImgPaletteContainer);
            GroupBox1.Location = new Point(12, 276);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(610, 190);
            GroupBox1.TabIndex = 164;
            GroupBox1.Text = "GroupBox1";
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(3, 3);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 160;
            PictureBox5.TabStop = false;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(33, 3);
            Label1.Name = "Label1";
            Label1.Size = new Size(572, 24);
            Label1.TabIndex = 143;
            Label1.Text = "Extracted palette:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ImgPaletteContainer
            // 
            ImgPaletteContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            ImgPaletteContainer.AutoScroll = true;
            ImgPaletteContainer.Location = new Point(3, 31);
            ImgPaletteContainer.Name = "ImgPaletteContainer";
            ImgPaletteContainer.Padding = new Padding(3);
            ImgPaletteContainer.Size = new Size(604, 156);
            ImgPaletteContainer.TabIndex = 145;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 269);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(610, 1);
            Separator1.TabIndex = 161;
            Separator1.TabStop = false;
            Separator1.Text = "Separator1";
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(12, 474);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(610, 24);
            AlertBox1.TabIndex = 159;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "You may need to readjust colors after closing to make your theme colors better in" + " accessibility";
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(50, 50, 50);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(215, 20, 20);
            Button3.Location = new Point(245, 506);
            Button3.Name = "Button3";
            Button3.Size = new Size(100, 32);
            Button3.TabIndex = 158;
            Button3.Text = "Cancel";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(50, 50, 50);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(0, 81, 210);
            Button2.Location = new Point(351, 506);
            Button2.Name = "Button2";
            Button2.Size = new Size(100, 32);
            Button2.TabIndex = 147;
            Button2.Text = "Done";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(50, 50, 50);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(45, 46, 57);
            Button1.Location = new Point(457, 506);
            Button1.Name = "Button1";
            Button1.Size = new Size(165, 32);
            Button1.TabIndex = 146;
            Button1.Text = "Distribute randomly";
            Button1.UseVisualStyleBackColor = false;
            // 
            // PaletteGenerateFromImage
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(634, 550);
            Controls.Add(GroupBox4);
            Controls.Add(GroupBox3);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            Controls.Add(Separator1);
            Controls.Add(AlertBox1);
            Controls.Add(Button3);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PaletteGenerateFromImage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Generate a palette from image";
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            Load += new EventHandler(PaletteGenerateFromImage_Load);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal UI.WP.RadioImage RadioButton2;
        internal UI.WP.RadioImage RadioButton1;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox1;
        internal Label Label1;
        internal FlowLayoutPanel ImgPaletteContainer;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button val1;
        internal UI.WP.Trackbar Trackbar1;
        internal PictureBox PictureBox8;
        internal PictureBox PictureBox7;
        internal Label Label6;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.Button val2;
        internal UI.WP.Trackbar Trackbar2;
        internal Label Label7;
        internal PictureBox PictureBox9;
        internal UI.WP.Button Button3;
        internal UI.WP.AlertBox AlertBox1;
        internal PictureBox PictureBox5;
        internal UI.WP.SeparatorH Separator1;
        internal PictureBox PictureBox1;
        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.RadioButton RadioButton6;
        internal UI.WP.RadioButton RadioButton7;
        internal UI.WP.RadioButton RadioButton5;
        internal UI.WP.RadioButton RadioButton4;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.GroupBox GroupBox4;
        internal Label Label3;
    }
}