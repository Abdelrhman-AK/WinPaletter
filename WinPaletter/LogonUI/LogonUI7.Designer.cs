using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUI7 : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI7));
            OpenFileDialog1 = new OpenFileDialog();
            OpenImgDlg = new OpenFileDialog();
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
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            GroupBox3 = new UI.WP.GroupBox();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            ttl_h = new UI.WP.Button();
            ttl_h.Click += new EventHandler(ttl_h_Click);
            Trackbar2 = new UI.WP.Trackbar();
            Trackbar2.Scroll += new UI.WP.Trackbar.ScrollEventHandler(NumericUpDown2_Click);
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            PictureBox10 = new PictureBox();
            PictureBox9 = new PictureBox();
            PictureBox8 = new PictureBox();
            ComboBox1 = new UI.WP.ComboBox();
            ComboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            CheckBox6 = new UI.WP.CheckBox();
            CheckBox6.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox6_CheckedChanged);
            CheckBox7 = new UI.WP.CheckBox();
            CheckBox7.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox7_CheckedChanged);
            CheckBox8 = new UI.WP.CheckBox();
            CheckBox8.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox8_CheckedChanged);
            PictureBox3 = new PictureBox();
            Label2 = new Label();
            GroupBox8 = new UI.WP.GroupBox();
            PictureBox41 = new PictureBox();
            Label41 = new Label();
            pnl_preview = new Panel();
            PictureBox11 = new PictureBox();
            GroupBox2 = new UI.WP.GroupBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            PictureBox7 = new PictureBox();
            PictureBox6 = new PictureBox();
            PictureBox5 = new PictureBox();
            PictureBox4 = new PictureBox();
            color_pick = new UI.Controllers.ColorItem();
            color_pick.Click += new EventHandler(Color_pick_Click);
            color_pick.DragDrop += new DragEventHandler(Color_pick_Click);
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            RadioButton3 = new UI.WP.RadioButton();
            RadioButton3.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton3_CheckedChanged);
            RadioButton2 = new UI.WP.RadioButton();
            RadioButton2.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton2_CheckedChanged);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            RadioButton1 = new UI.WP.RadioButton();
            RadioButton1.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton1_CheckedChanged);
            PictureBox2 = new PictureBox();
            RadioButton4 = new UI.WP.RadioButton();
            RadioButton4.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton4_CheckedChanged);
            Label1 = new Label();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            pnl_preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // OpenImgDlg
            // 
            OpenImgDlg.Filter = "Images (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All Files (*.*)|*.*";
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
            GroupBox12.Size = new Size(860, 39);
            GroupBox12.TabIndex = 201;
            // 
            // Toggle1
            // 
            Toggle1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Toggle1.BackColor = Color.FromArgb(43, 43, 43);
            Toggle1.Checked = false;
            Toggle1.DarkLight_Toggler = false;
            Toggle1.Location = new Point(815, 9);
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
            checker_img.Location = new Point(774, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
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
            Button2.Location = new Point(603, 407);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 66;
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
            Button1.Location = new Point(689, 407);
            Button1.Name = "Button1";
            Button1.Size = new Size(180, 34);
            Button1.TabIndex = 65;
            Button1.Text = "Load into current theme";
            Button1.UseVisualStyleBackColor = false;
            // 
            // GroupBox3
            // 
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(Button4);
            GroupBox3.Controls.Add(ttl_h);
            GroupBox3.Controls.Add(Trackbar2);
            GroupBox3.Controls.Add(Trackbar1);
            GroupBox3.Controls.Add(PictureBox10);
            GroupBox3.Controls.Add(PictureBox9);
            GroupBox3.Controls.Add(PictureBox8);
            GroupBox3.Controls.Add(ComboBox1);
            GroupBox3.Controls.Add(CheckBox6);
            GroupBox3.Controls.Add(CheckBox7);
            GroupBox3.Controls.Add(CheckBox8);
            GroupBox3.Controls.Add(PictureBox3);
            GroupBox3.Controls.Add(Label2);
            GroupBox3.Location = new Point(12, 242);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(322, 154);
            GroupBox3.TabIndex = 18;
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
            Button4.Location = new Point(281, 95);
            Button4.Name = "Button4";
            Button4.Size = new Size(34, 25);
            Button4.TabIndex = 131;
            Button4.UseVisualStyleBackColor = false;
            // 
            // ttl_h
            // 
            ttl_h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ttl_h.BackColor = Color.FromArgb(43, 43, 43);
            ttl_h.DrawOnGlass = false;
            ttl_h.Font = new Font("Segoe UI", 9.0f);
            ttl_h.ForeColor = Color.White;
            ttl_h.Image = null;
            ttl_h.LineColor = Color.FromArgb(0, 81, 210);
            ttl_h.Location = new Point(282, 64);
            ttl_h.Name = "ttl_h";
            ttl_h.Size = new Size(34, 25);
            ttl_h.TabIndex = 130;
            ttl_h.UseVisualStyleBackColor = false;
            // 
            // Trackbar2
            // 
            Trackbar2.LargeChange = 10;
            Trackbar2.Location = new Point(150, 98);
            Trackbar2.Maximum = 100;
            Trackbar2.Minimum = 0;
            Trackbar2.Name = "Trackbar2";
            Trackbar2.Size = new Size(127, 19);
            Trackbar2.SmallChange = 1;
            Trackbar2.TabIndex = 93;
            Trackbar2.Value = 50;
            // 
            // Trackbar1
            // 
            Trackbar1.LargeChange = 5;
            Trackbar1.Location = new Point(150, 67);
            Trackbar1.Maximum = 35;
            Trackbar1.Minimum = 0;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(128, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 67;
            Trackbar1.Value = 1;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(42, 95);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(25, 25);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 92;
            PictureBox10.TabStop = false;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(42, 64);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(25, 25);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 91;
            PictureBox9.TabStop = false;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(42, 33);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(25, 25);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 90;
            PictureBox8.TabStop = false;
            // 
            // ComboBox1
            // 
            ComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox1.BackColor = Color.FromArgb(43, 43, 43);
            ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Items.AddRange(new object[] { "Acrylic (Looks Like Windows 10/11)", "Aero" });
            ComboBox1.Location = new Point(73, 124);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(243, 26);
            ComboBox1.TabIndex = 85;
            // 
            // CheckBox6
            // 
            CheckBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox6.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox6.Checked = false;
            CheckBox6.Font = new Font("Segoe UI", 9.0f);
            CheckBox6.ForeColor = Color.White;
            CheckBox6.Location = new Point(73, 95);
            CheckBox6.Name = "CheckBox6";
            CheckBox6.Size = new Size(74, 25);
            CheckBox6.TabIndex = 84;
            CheckBox6.Text = "Noise";
            // 
            // CheckBox7
            // 
            CheckBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox7.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox7.Checked = false;
            CheckBox7.Font = new Font("Segoe UI", 9.0f);
            CheckBox7.ForeColor = Color.White;
            CheckBox7.Location = new Point(73, 64);
            CheckBox7.Name = "CheckBox7";
            CheckBox7.Size = new Size(74, 25);
            CheckBox7.TabIndex = 83;
            CheckBox7.Text = "Blurred";
            // 
            // CheckBox8
            // 
            CheckBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox8.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox8.Checked = false;
            CheckBox8.Font = new Font("Segoe UI", 9.0f);
            CheckBox8.ForeColor = Color.White;
            CheckBox8.Location = new Point(73, 33);
            CheckBox8.Name = "CheckBox8";
            CheckBox8.Size = new Size(243, 25);
            CheckBox8.TabIndex = 82;
            CheckBox8.Text = "Gray-scale";
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(3, 3);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(25, 25);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 17;
            PictureBox3.TabStop = false;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(32, 3);
            Label2.Name = "Label2";
            Label2.Size = new Size(287, 25);
            Label2.TabIndex = 81;
            Label2.Text = "Effects";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox8
            // 
            GroupBox8.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox8.Controls.Add(PictureBox41);
            GroupBox8.Controls.Add(Label41);
            GroupBox8.Controls.Add(pnl_preview);
            GroupBox8.Location = new Point(337, 54);
            GroupBox8.Margin = new Padding(4, 3, 4, 3);
            GroupBox8.Name = "GroupBox8";
            GroupBox8.Padding = new Padding(1);
            GroupBox8.Size = new Size(536, 342);
            GroupBox8.TabIndex = 15;
            // 
            // PictureBox41
            // 
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(4, 4);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(35, 35);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 4;
            PictureBox41.TabStop = false;
            // 
            // Label41
            // 
            Label41.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label41.Location = new Point(45, 4);
            Label41.Name = "Label41";
            Label41.Size = new Size(142, 35);
            Label41.TabIndex = 3;
            Label41.Text = "Preview";
            Label41.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnl_preview
            // 
            pnl_preview.BackColor = Color.FromArgb(224, 224, 224);
            pnl_preview.BackgroundImageLayout = ImageLayout.Stretch;
            pnl_preview.Controls.Add(PictureBox11);
            pnl_preview.Location = new Point(4, 41);
            pnl_preview.Name = "pnl_preview";
            pnl_preview.Size = new Size(528, 297);
            pnl_preview.TabIndex = 2;
            // 
            // PictureBox11
            // 
            PictureBox11.BackColor = Color.Transparent;
            PictureBox11.Dock = DockStyle.Fill;
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(0, 0);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(528, 297);
            PictureBox11.TabIndex = 0;
            PictureBox11.TabStop = false;
            // 
            // GroupBox2
            // 
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(Button3);
            GroupBox2.Controls.Add(PictureBox7);
            GroupBox2.Controls.Add(PictureBox6);
            GroupBox2.Controls.Add(PictureBox5);
            GroupBox2.Controls.Add(PictureBox4);
            GroupBox2.Controls.Add(color_pick);
            GroupBox2.Controls.Add(TextBox1);
            GroupBox2.Controls.Add(RadioButton3);
            GroupBox2.Controls.Add(RadioButton2);
            GroupBox2.Controls.Add(Button7);
            GroupBox2.Controls.Add(RadioButton1);
            GroupBox2.Controls.Add(PictureBox2);
            GroupBox2.Controls.Add(RadioButton4);
            GroupBox2.Controls.Add(Label1);
            GroupBox2.Location = new Point(12, 54);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(322, 185);
            GroupBox2.TabIndex = 17;
            // 
            // Button3
            // 
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(0, 81, 210);
            Button3.Location = new Point(221, 31);
            Button3.Name = "Button3";
            Button3.Size = new Size(97, 25);
            Button3.TabIndex = 93;
            Button3.Text = "Choose";
            Button3.UseVisualStyleBackColor = false;
            Button3.Visible = false;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(42, 93);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(25, 25);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 92;
            PictureBox7.TabStop = false;
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(42, 124);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(25, 25);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 91;
            PictureBox6.TabStop = false;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(42, 62);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(25, 25);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 90;
            PictureBox5.TabStop = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(42, 31);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(25, 25);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 89;
            PictureBox4.TabStop = false;
            // 
            // color_pick
            // 
            color_pick.AllowDrop = true;
            color_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            color_pick.BackColor = Color.FromArgb(47, 47, 48);
            color_pick.DefaultColor = Color.Black;
            color_pick.DontShowInfo = false;
            color_pick.Location = new Point(219, 93);
            color_pick.Margin = new Padding(4, 3, 4, 3);
            color_pick.Name = "color_pick";
            color_pick.Size = new Size(97, 25);
            color_pick.TabIndex = 88;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(98, 155);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(178, 24);
            TextBox1.TabIndex = 86;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // RadioButton3
            // 
            RadioButton3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton3.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton3.Checked = false;
            RadioButton3.Font = new Font("Segoe UI", 9.0f);
            RadioButton3.ForeColor = Color.White;
            RadioButton3.Location = new Point(73, 93);
            RadioButton3.Name = "RadioButton3";
            RadioButton3.Size = new Size(139, 25);
            RadioButton3.TabIndex = 85;
            RadioButton3.Text = "Solid color";
            // 
            // RadioButton2
            // 
            RadioButton2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton2.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Location = new Point(73, 62);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(243, 25);
            RadioButton2.TabIndex = 83;
            RadioButton2.Text = "Current wallpaper";
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(43, 43, 43);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = (Image)resources.GetObject("Button7.Image");
            Button7.LineColor = Color.FromArgb(184, 153, 68);
            Button7.Location = new Point(281, 155);
            Button7.Margin = new Padding(4, 3, 4, 3);
            Button7.Name = "Button7";
            Button7.Size = new Size(35, 24);
            Button7.TabIndex = 87;
            Button7.UseVisualStyleBackColor = false;
            // 
            // RadioButton1
            // 
            RadioButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton1.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton1.Checked = true;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Location = new Point(73, 31);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(139, 25);
            RadioButton1.TabIndex = 82;
            RadioButton1.Text = "System default";
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(3, 3);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(25, 25);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 17;
            PictureBox2.TabStop = false;
            // 
            // RadioButton4
            // 
            RadioButton4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton4.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton4.Checked = false;
            RadioButton4.Font = new Font("Segoe UI", 9.0f);
            RadioButton4.ForeColor = Color.White;
            RadioButton4.Location = new Point(73, 124);
            RadioButton4.Name = "RadioButton4";
            RadioButton4.Size = new Size(243, 25);
            RadioButton4.TabIndex = 84;
            RadioButton4.Text = "Custom image";
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(32, 3);
            Label1.Name = "Label1";
            Label1.Size = new Size(287, 25);
            Label1.TabIndex = 81;
            Label1.Text = "Source";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LogonUI7
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(884, 453);
            Controls.Add(GroupBox12);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(GroupBox3);
            Controls.Add(GroupBox8);
            Controls.Add(GroupBox2);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogonUI7";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogonUI";
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            pnl_preview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            Load += new EventHandler(LogonUI7_Load);
            Shown += new EventHandler(LogonUI7_Shown);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox8;
        internal Panel pnl_preview;
        internal PictureBox PictureBox41;
        internal Label Label41;
        internal UI.WP.Toggle Toggle1;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.WP.RadioButton RadioButton4;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton1;
        internal PictureBox PictureBox2;
        internal Label Label1;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.Button Button7;
        internal UI.Controllers.ColorItem color_pick;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.ComboBox ComboBox1;
        internal UI.WP.CheckBox CheckBox6;
        internal UI.WP.CheckBox CheckBox7;
        internal UI.WP.CheckBox CheckBox8;
        internal PictureBox PictureBox3;
        internal Label Label2;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox6;
        internal PictureBox PictureBox5;
        internal PictureBox PictureBox4;
        internal PictureBox PictureBox10;
        internal PictureBox PictureBox9;
        internal PictureBox PictureBox8;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal PictureBox PictureBox11;
        internal UI.WP.Trackbar Trackbar1;
        internal UI.WP.Trackbar Trackbar2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Button ttl_h;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal PictureBox checker_img;
        internal OpenFileDialog OpenFileDialog1;
        internal OpenFileDialog OpenImgDlg;
    }
}