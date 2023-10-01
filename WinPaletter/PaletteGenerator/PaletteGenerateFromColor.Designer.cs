using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class PaletteGenerateFromColor : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerateFromColor));
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
            GroupBox2 = new UI.WP.GroupBox();
            PictureBox7 = new PictureBox();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            SelectedColor = new UI.Controllers.ColorItem();
            SelectedColor.DragDrop += new DragEventHandler(SelectedColor_DragDrop);
            SelectedColor.Click += new EventHandler(SelectedColor_Click);
            SelectedColor.BackColorChanged += new EventHandler(SelectedColor_BackColorChanged);
            PictureBox8 = new PictureBox();
            val1 = new UI.WP.Button();
            val1.Click += new EventHandler(val1_Click);
            PictureBox2 = new PictureBox();
            Label2 = new Label();
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            Label6 = new Label();
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
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
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
            GroupBox4.Location = new Point(12, 109);
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
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(PictureBox7);
            GroupBox2.Controls.Add(CheckBox1);
            GroupBox2.Controls.Add(SelectedColor);
            GroupBox2.Controls.Add(PictureBox8);
            GroupBox2.Controls.Add(val1);
            GroupBox2.Controls.Add(PictureBox2);
            GroupBox2.Controls.Add(Label2);
            GroupBox2.Controls.Add(Trackbar1);
            GroupBox2.Controls.Add(Label6);
            GroupBox2.Location = new Point(12, 12);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(610, 91);
            GroupBox2.TabIndex = 165;
            GroupBox2.Text = "GroupBox2";
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(3, 63);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 156;
            PictureBox7.TabStop = false;
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(33, 63);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(572, 24);
            CheckBox1.TabIndex = 155;
            CheckBox1.Text = "Include inverted degrees of selected color to increase variety of extracted palet" + "te";
            // 
            // SelectedColor
            // 
            SelectedColor.AllowDrop = true;
            SelectedColor.BackColor = Color.FromArgb(231, 38, 53);
            SelectedColor.DefaultColor = Color.FromArgb(231, 38, 53);
            SelectedColor.DontShowInfo = false;
            SelectedColor.Location = new Point(106, 3);
            SelectedColor.Margin = new Padding(4, 3, 4, 3);
            SelectedColor.Name = "SelectedColor";
            SelectedColor.Size = new Size(100, 24);
            SelectedColor.TabIndex = 154;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(3, 33);
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
            val1.Location = new Point(572, 33);
            val1.Name = "val1";
            val1.Size = new Size(34, 24);
            val1.TabIndex = 153;
            val1.UseVisualStyleBackColor = false;
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
            // Label2
            // 
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(33, 3);
            Label2.Name = "Label2";
            Label2.Size = new Size(66, 24);
            Label2.TabIndex = 141;
            Label2.Text = "Color";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar1.LargeChange = 10;
            Trackbar1.Location = new Point(277, 36);
            Trackbar1.Maximum = 100;
            Trackbar1.Minimum = 13;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(289, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 152;
            Trackbar1.Value = 13;
            // 
            // Label6
            // 
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(33, 33);
            Label6.Name = "Label6";
            Label6.Size = new Size(238, 24);
            Label6.TabIndex = 151;
            Label6.Text = "Minimum number of extracted colors";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(PictureBox5);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(ImgPaletteContainer);
            GroupBox1.Location = new Point(12, 240);
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
            Separator1.Location = new Point(12, 233);
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
            AlertBox1.Location = new Point(12, 437);
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
            Button3.Location = new Point(245, 467);
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
            Button2.Location = new Point(351, 467);
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
            Button1.Location = new Point(457, 467);
            Button1.Name = "Button1";
            Button1.Size = new Size(165, 32);
            Button1.TabIndex = 146;
            Button1.Text = "Distribute randomly";
            Button1.UseVisualStyleBackColor = false;
            // 
            // PaletteGenerateFromColor
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(634, 511);
            Controls.Add(GroupBox4);
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
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PaletteGenerateFromColor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Generate a palette from color";
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            Load += new EventHandler(PaletteGenerateFromImage_Load);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal Label Label1;
        internal FlowLayoutPanel ImgPaletteContainer;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button val1;
        internal UI.WP.Trackbar Trackbar1;
        internal PictureBox PictureBox8;
        internal Label Label6;
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
        internal UI.WP.GroupBox GroupBox4;
        internal Label Label3;
        internal UI.Controllers.ColorItem SelectedColor;
        internal PictureBox PictureBox7;
        internal UI.WP.CheckBox CheckBox1;
    }
}