using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ScreenSaver_Editor : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenSaver_Editor));
            OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog2 = new OpenFileDialog();
            OpenThemeDialog = new OpenFileDialog();
            GroupBox1 = new UI.WP.GroupBox();
            PictureBox4 = new PictureBox();
            Trackbar5 = new UI.WP.Trackbar();
            Trackbar5.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar5_Scroll);
            PictureBox17 = new PictureBox();
            CheckBox1 = new UI.WP.CheckBox();
            Label4 = new Label();
            PictureBox1 = new PictureBox();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Label3 = new Label();
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            previewContainer = new UI.WP.GroupBox();
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click);
            Button13 = new UI.WP.Button();
            Button13.Click += new EventHandler(Button13_Click);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            pnl_preview = new Panel();
            PictureBox41 = new PictureBox();
            Label19 = new Label();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            GroupBox12 = new UI.WP.GroupBox();
            Button259 = new UI.WP.Button();
            Button259.Click += new EventHandler(Button259_Click);
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label12 = new Label();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            ScrSvrEnabled = new UI.WP.Toggle();
            ScrSvrEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(ScrSvrEnabled_CheckedChanged);
            checker_img = new PictureBox();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            previewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // OpenFileDialog2
            // 
            OpenFileDialog2.DefaultExt = "wpt";
            OpenFileDialog2.Filter = "Screen Saver (*.scr)|*.scr";
            // 
            // OpenThemeDialog
            // 
            OpenThemeDialog.Filter = "Windows Theme (*.theme)|*.theme|All Files (*.*)|*.*";
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(PictureBox4);
            GroupBox1.Controls.Add(Trackbar5);
            GroupBox1.Controls.Add(PictureBox17);
            GroupBox1.Controls.Add(CheckBox1);
            GroupBox1.Controls.Add(Label4);
            GroupBox1.Controls.Add(PictureBox1);
            GroupBox1.Controls.Add(Button4);
            GroupBox1.Controls.Add(Button1);
            GroupBox1.Controls.Add(Label3);
            GroupBox1.Controls.Add(TextBox1);
            GroupBox1.Location = new Point(12, 408);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(750, 90);
            GroupBox1.TabIndex = 226;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(3, 3);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 217;
            PictureBox4.TabStop = false;
            // 
            // Trackbar5
            // 
            Trackbar5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar5.LargeChange = 10;
            Trackbar5.Location = new Point(129, 36);
            Trackbar5.Maximum = 3600;
            Trackbar5.Minimum = 60;
            Trackbar5.Name = "Trackbar5";
            Trackbar5.Size = new Size(535, 19);
            Trackbar5.SmallChange = 1;
            Trackbar5.TabIndex = 214;
            Trackbar5.Value = 60;
            // 
            // PictureBox17
            // 
            PictureBox17.BackColor = Color.Transparent;
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(3, 33);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(24, 24);
            PictureBox17.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox17.TabIndex = 212;
            PictureBox17.TabStop = false;
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
            CheckBox1.Size = new Size(713, 24);
            CheckBox1.TabIndex = 223;
            CheckBox1.Text = "On resume, password protect";
            // 
            // Label4
            // 
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(33, 33);
            Label4.Name = "Label4";
            Label4.Size = new Size(90, 24);
            Label4.TabIndex = 213;
            Label4.Text = "Timeout (sec):";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(3, 63);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 222;
            PictureBox1.TabStop = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = null;
            Button4.LineColor = Color.FromArgb(0, 81, 210);
            Button4.Location = new Point(670, 33);
            Button4.Name = "Button4";
            Button4.Size = new Size(76, 24);
            Button4.TabIndex = 215;
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(43, 43, 43);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.LineColor = Color.FromArgb(184, 153, 68);
            Button1.Location = new Point(712, 3);
            Button1.Margin = new Padding(4, 3, 4, 3);
            Button1.Name = "Button1";
            Button1.Size = new Size(34, 24);
            Button1.TabIndex = 219;
            Button1.UseVisualStyleBackColor = false;
            // 
            // Label3
            // 
            Label3.Location = new Point(33, 3);
            Label3.Name = "Label3";
            Label3.Size = new Size(90, 24);
            Label3.TabIndex = 216;
            Label3.Text = "File:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(129, 3);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(576, 24);
            TextBox1.TabIndex = 218;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // previewContainer
            // 
            previewContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            previewContainer.BackColor = Color.FromArgb(34, 34, 34);
            previewContainer.Controls.Add(Button14);
            previewContainer.Controls.Add(Button13);
            previewContainer.Controls.Add(Button6);
            previewContainer.Controls.Add(Button5);
            previewContainer.Controls.Add(pnl_preview);
            previewContainer.Controls.Add(PictureBox41);
            previewContainer.Controls.Add(Label19);
            previewContainer.Location = new Point(12, 57);
            previewContainer.Margin = new Padding(4, 3, 4, 3);
            previewContainer.Name = "previewContainer";
            previewContainer.Padding = new Padding(1);
            previewContainer.Size = new Size(750, 345);
            previewContainer.TabIndex = 211;
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(43, 43, 43);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = null;
            Button14.ImageAlign = ContentAlignment.MiddleRight;
            Button14.LineColor = Color.FromArgb(0, 66, 119);
            Button14.Location = new Point(425, 8);
            Button14.Name = "Button14";
            Button14.Size = new Size(150, 24);
            Button14.TabIndex = 225;
            Button14.Text = "Configure its settings";
            Button14.UseVisualStyleBackColor = false;
            // 
            // Button13
            // 
            Button13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button13.BackColor = Color.FromArgb(43, 43, 43);
            Button13.DrawOnGlass = false;
            Button13.Font = new Font("Segoe UI", 9.0f);
            Button13.ForeColor = Color.White;
            Button13.Image = (Image)resources.GetObject("Button13.Image");
            Button13.ImageAlign = ContentAlignment.MiddleLeft;
            Button13.LineColor = Color.FromArgb(1, 45, 77);
            Button13.Location = new Point(651, 8);
            Button13.Name = "Button13";
            Button13.Size = new Size(90, 24);
            Button13.TabIndex = 224;
            Button13.Text = "Fullscreen";
            Button13.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.LineColor = Color.FromArgb(117, 3, 34);
            Button6.Location = new Point(581, 8);
            Button6.Name = "Button6";
            Button6.Size = new Size(29, 24);
            Button6.TabIndex = 223;
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(43, 43, 43);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.LineColor = Color.FromArgb(1, 45, 77);
            Button5.Location = new Point(616, 8);
            Button5.Name = "Button5";
            Button5.Size = new Size(29, 24);
            Button5.TabIndex = 222;
            Button5.UseVisualStyleBackColor = false;
            // 
            // pnl_preview
            // 
            pnl_preview.BackColor = Color.Black;
            pnl_preview.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview.BorderStyle = BorderStyle.FixedSingle;
            pnl_preview.Location = new Point(111, 42);
            pnl_preview.Name = "pnl_preview";
            pnl_preview.Size = new Size(528, 297);
            pnl_preview.TabIndex = 2;
            // 
            // PictureBox41
            // 
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(4, 4);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(35, 32);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 4;
            PictureBox41.TabStop = false;
            // 
            // Label19
            // 
            Label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label19.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label19.Location = new Point(45, 5);
            Label19.Name = "Label19";
            Label19.Size = new Size(374, 31);
            Label19.TabIndex = 3;
            Label19.Text = "Preview";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
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
            Button10.Location = new Point(461, 510);
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
            Button7.Location = new Point(375, 510);
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
            Button8.Location = new Point(582, 510);
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
            GroupBox12.Controls.Add(Button259);
            GroupBox12.Controls.Add(Button9);
            GroupBox12.Controls.Add(Label12);
            GroupBox12.Controls.Add(Button11);
            GroupBox12.Controls.Add(Button12);
            GroupBox12.Controls.Add(ScrSvrEnabled);
            GroupBox12.Controls.Add(checker_img);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(750, 39);
            GroupBox12.TabIndex = 201;
            // 
            // Button259
            // 
            Button259.BackColor = Color.FromArgb(43, 43, 43);
            Button259.DrawOnGlass = false;
            Button259.Font = new Font("Segoe UI", 9.0f);
            Button259.ForeColor = Color.White;
            Button259.Image = (Image)resources.GetObject("Button259.Image");
            Button259.ImageAlign = ContentAlignment.MiddleLeft;
            Button259.LineColor = Color.FromArgb(151, 111, 91);
            Button259.Location = new Point(223, 5);
            Button259.Name = "Button259";
            Button259.Size = new Size(144, 29);
            Button259.TabIndex = 114;
            Button259.Text = "Classic .theme file";
            Button259.UseVisualStyleBackColor = false;
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
            Button9.Location = new Point(370, 5);
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
            Button12.Location = new Point(499, 5);
            Button12.Name = "Button12";
            Button12.Size = new Size(135, 29);
            Button12.TabIndex = 108;
            Button12.Text = "Default Windows";
            Button12.UseVisualStyleBackColor = false;
            // 
            // ScrSvrEnabled
            // 
            ScrSvrEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScrSvrEnabled.BackColor = Color.FromArgb(43, 43, 43);
            ScrSvrEnabled.Checked = false;
            ScrSvrEnabled.DarkLight_Toggler = false;
            ScrSvrEnabled.Location = new Point(705, 9);
            ScrSvrEnabled.Name = "ScrSvrEnabled";
            ScrSvrEnabled.Size = new Size(40, 20);
            ScrSvrEnabled.TabIndex = 85;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = Properties.Resources.checker_disabled;
            checker_img.Location = new Point(664, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // ScreenSaver_Editor
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(774, 556);
            Controls.Add(GroupBox1);
            Controls.Add(previewContainer);
            Controls.Add(Button10);
            Controls.Add(Button7);
            Controls.Add(Button8);
            Controls.Add(GroupBox12);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ScreenSaver_Editor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Screen Saver";
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            previewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            Load += new EventHandler(ScreenSaver_Editor_Load);
            FormClosing += new FormClosingEventHandler(ScreenSaver_Editor_FormClosing);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle ScrSvrEnabled;
        internal PictureBox checker_img;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal UI.WP.GroupBox previewContainer;
        internal Panel pnl_preview;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal UI.WP.Button Button4;
        internal Label Label4;
        internal PictureBox PictureBox17;
        internal UI.WP.Trackbar Trackbar5;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox1;
        internal PictureBox PictureBox4;
        internal Label Label3;
        internal PictureBox PictureBox1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.Button Button14;
        internal UI.WP.Button Button13;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button5;
        internal OpenFileDialog OpenFileDialog2;
        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.Button Button259;
        internal OpenFileDialog OpenThemeDialog;
    }
}