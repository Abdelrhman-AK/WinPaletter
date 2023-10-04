using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ApplicationThemer : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationThemer));
            OpenFileDialog1 = new OpenFileDialog();
            Label29 = new Label();
            PictureBox46 = new PictureBox();
            Label28 = new Label();
            PictureBox45 = new PictureBox();
            PictureBox44 = new PictureBox();
            PictureBox43 = new PictureBox();
            BackColorPick = new UI.Controllers.ColorItem();
            BackColorPick.DragDrop += new DragEventHandler(AccentColor_BackColorPick_DragDrop);
            BackColorPick.Click += new EventHandler(BackColorPick_Click);
            AccentColor = new UI.Controllers.ColorItem();
            AccentColor.DragDrop += new DragEventHandler(AccentColor_BackColorPick_DragDrop);
            AccentColor.Click += new EventHandler(AccentColor_Click);
            RoundedCorners = new UI.WP.CheckBox();
            RoundedCorners.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckedChanged);
            appearance_dark = new UI.WP.CheckBox();
            appearance_dark.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckedChanged);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            GroupBox12 = new UI.WP.GroupBox();
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label12 = new Label();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            AppThemeEnabled = new UI.WP.Toggle();
            AppThemeEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(AppThemeEnabled_CheckedChanged);
            checker_img = new PictureBox();
            Label25 = new Label();
            appearance_list = new UI.WP.ComboBox();
            appearance_list.SelectedIndexChanged += new EventHandler(Appearance_list_SelectedIndexChanged);
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click_1);
            AlertBox1 = new UI.WP.AlertBox();
            AlertBox2 = new UI.WP.AlertBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox45).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox44).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox43).BeginInit();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // Label29
            // 
            Label29.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label29.Location = new Point(42, 181);
            Label29.Name = "Label29";
            Label29.Size = new Size(130, 24);
            Label29.TabIndex = 226;
            Label29.Text = "Background color:";
            Label29.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox46
            // 
            PictureBox46.Image = (Image)resources.GetObject("PictureBox46.Image");
            PictureBox46.Location = new Point(12, 181);
            PictureBox46.Name = "PictureBox46";
            PictureBox46.Size = new Size(24, 24);
            PictureBox46.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox46.TabIndex = 225;
            PictureBox46.TabStop = false;
            // 
            // Label28
            // 
            Label28.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label28.Location = new Point(42, 151);
            Label28.Name = "Label28";
            Label28.Size = new Size(130, 24);
            Label28.TabIndex = 223;
            Label28.Text = "Accent color:";
            Label28.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox45
            // 
            PictureBox45.Image = (Image)resources.GetObject("PictureBox45.Image");
            PictureBox45.Location = new Point(12, 151);
            PictureBox45.Name = "PictureBox45";
            PictureBox45.Size = new Size(24, 24);
            PictureBox45.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox45.TabIndex = 222;
            PictureBox45.TabStop = false;
            // 
            // PictureBox44
            // 
            PictureBox44.Image = (Image)resources.GetObject("PictureBox44.Image");
            PictureBox44.Location = new Point(12, 121);
            PictureBox44.Name = "PictureBox44";
            PictureBox44.Size = new Size(24, 24);
            PictureBox44.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox44.TabIndex = 220;
            PictureBox44.TabStop = false;
            // 
            // PictureBox43
            // 
            PictureBox43.Image = (Image)resources.GetObject("PictureBox43.Image");
            PictureBox43.Location = new Point(12, 91);
            PictureBox43.Name = "PictureBox43";
            PictureBox43.Size = new Size(24, 24);
            PictureBox43.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox43.TabIndex = 218;
            PictureBox43.TabStop = false;
            // 
            // BackColorPick
            // 
            BackColorPick.AllowDrop = true;
            BackColorPick.BackColor = Color.FromArgb(25, 25, 25);
            BackColorPick.DefaultColor = Color.FromArgb(25, 25, 25);
            BackColorPick.DontShowInfo = false;
            BackColorPick.Location = new Point(178, 181);
            BackColorPick.Name = "BackColorPick";
            BackColorPick.Size = new Size(112, 24);
            BackColorPick.TabIndex = 227;
            // 
            // AccentColor
            // 
            AccentColor.AllowDrop = true;
            AccentColor.BackColor = Color.FromArgb(0, 81, 210);
            AccentColor.DefaultColor = Color.FromArgb(0, 81, 210);
            AccentColor.DontShowInfo = false;
            AccentColor.Location = new Point(178, 151);
            AccentColor.Name = "AccentColor";
            AccentColor.Size = new Size(112, 24);
            AccentColor.TabIndex = 224;
            // 
            // RoundedCorners
            // 
            RoundedCorners.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RoundedCorners.BackColor = Color.FromArgb(25, 25, 25);
            RoundedCorners.Checked = true;
            RoundedCorners.Font = new Font("Segoe UI", 9.0f);
            RoundedCorners.ForeColor = Color.White;
            RoundedCorners.Location = new Point(42, 121);
            RoundedCorners.Name = "RoundedCorners";
            RoundedCorners.Size = new Size(555, 24);
            RoundedCorners.TabIndex = 221;
            RoundedCorners.Text = "Rounded corners";
            // 
            // appearance_dark
            // 
            appearance_dark.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            appearance_dark.BackColor = Color.FromArgb(25, 25, 25);
            appearance_dark.Checked = true;
            appearance_dark.Font = new Font("Segoe UI", 9.0f);
            appearance_dark.ForeColor = Color.White;
            appearance_dark.Location = new Point(42, 91);
            appearance_dark.Name = "appearance_dark";
            appearance_dark.Size = new Size(555, 24);
            appearance_dark.TabIndex = 219;
            appearance_dark.Text = "Dark mode";
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
            Button7.Location = new Point(210, 315);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 212;
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
            Button8.Location = new Point(417, 315);
            Button8.Name = "Button8";
            Button8.Size = new Size(180, 34);
            Button8.TabIndex = 211;
            Button8.Text = "Load into current theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(Button9);
            GroupBox12.Controls.Add(Label12);
            GroupBox12.Controls.Add(Button11);
            GroupBox12.Controls.Add(Button12);
            GroupBox12.Controls.Add(AppThemeEnabled);
            GroupBox12.Controls.Add(checker_img);
            GroupBox12.Controls.Add(Label25);
            GroupBox12.Controls.Add(appearance_list);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(585, 70);
            GroupBox12.TabIndex = 202;
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
            Button9.Location = new Point(223, 5);
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
            Button12.Location = new Point(352, 5);
            Button12.Name = "Button12";
            Button12.Size = new Size(135, 29);
            Button12.TabIndex = 108;
            Button12.Text = "Default Windows";
            Button12.UseVisualStyleBackColor = false;
            // 
            // AppThemeEnabled
            // 
            AppThemeEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AppThemeEnabled.BackColor = Color.FromArgb(43, 43, 43);
            AppThemeEnabled.Checked = false;
            AppThemeEnabled.DarkLight_Toggler = false;
            AppThemeEnabled.Location = new Point(539, 25);
            AppThemeEnabled.Name = "AppThemeEnabled";
            AppThemeEnabled.Size = new Size(40, 20);
            AppThemeEnabled.TabIndex = 85;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = Properties.Resources.checker_disabled;
            checker_img.Location = new Point(498, 20);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // Label25
            // 
            Label25.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label25.Location = new Point(4, 40);
            Label25.Name = "Label25";
            Label25.Size = new Size(75, 24);
            Label25.TabIndex = 215;
            Label25.Text = "Scheme:";
            Label25.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // appearance_list
            // 
            appearance_list.BackColor = Color.FromArgb(43, 43, 43);
            appearance_list.DrawMode = DrawMode.OwnerDrawVariable;
            appearance_list.DropDownStyle = ComboBoxStyle.DropDownList;
            appearance_list.Font = new Font("Segoe UI", 9.0f);
            appearance_list.ForeColor = Color.White;
            appearance_list.FormattingEnabled = true;
            appearance_list.ItemHeight = 20;
            appearance_list.Items.AddRange(new object[] { "Default Dark", "Default Light", "AMOLED", "Extreme White", "GitHub Dark", "GitHub Light", "Reddit Dark", "Reddit Light", "Discord Dark", "Discord Light" });
            appearance_list.Location = new Point(85, 38);
            appearance_list.Name = "appearance_list";
            appearance_list.Size = new Size(402, 26);
            appearance_list.TabIndex = 216;
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
            Button10.Location = new Point(296, 315);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 228;
            Button10.Text = "Quick apply";
            Button10.UseVisualStyleBackColor = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(12, 219);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(585, 22);
            AlertBox1.TabIndex = 229;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "To preview changes, enable the toggle above";
            // 
            // AlertBox2
            // 
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox2.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox2.CenterText = false;
            AlertBox2.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox2.Font = new Font("Segoe UI", 9.0f);
            AlertBox2.Image = null;
            AlertBox2.Location = new Point(12, 248);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(585, 60);
            AlertBox2.TabIndex = 230;
            AlertBox2.TabStop = false;
            AlertBox2.Text = resources.GetString("AlertBox2.Text");
            // 
            // ApplicationThemer
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(609, 361);
            Controls.Add(AlertBox2);
            Controls.Add(AlertBox1);
            Controls.Add(Button10);
            Controls.Add(BackColorPick);
            Controls.Add(Label29);
            Controls.Add(PictureBox46);
            Controls.Add(AccentColor);
            Controls.Add(Label28);
            Controls.Add(PictureBox45);
            Controls.Add(RoundedCorners);
            Controls.Add(PictureBox44);
            Controls.Add(appearance_dark);
            Controls.Add(PictureBox43);
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
            Name = "ApplicationThemer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinPaletter application theme";
            ((System.ComponentModel.ISupportInitialize)PictureBox46).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox45).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox44).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox43).EndInit();
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            Load += new EventHandler(ApplicationThemer_Editor_Load);
            Shown += new EventHandler(ApplicationThemer_Shown);
            FormClosing += new FormClosingEventHandler(ApplicationThemer_FormClosing);
            BackColorChanged += new EventHandler(ApplicationThemer_BackColorChanged);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle AppThemeEnabled;
        internal PictureBox checker_img;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.Controllers.ColorItem BackColorPick;
        internal Label Label29;
        internal PictureBox PictureBox46;
        internal UI.Controllers.ColorItem AccentColor;
        internal Label Label28;
        internal PictureBox PictureBox45;
        internal UI.WP.CheckBox RoundedCorners;
        internal PictureBox PictureBox44;
        internal UI.WP.CheckBox appearance_dark;
        internal PictureBox PictureBox43;
        internal UI.WP.ComboBox appearance_list;
        internal Label Label25;
        internal UI.WP.Button Button10;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.AlertBox AlertBox2;
    }
}