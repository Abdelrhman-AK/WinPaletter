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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI7));
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenImgDlg = new System.Windows.Forms.OpenFileDialog();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.Toggle1 = new WinPaletter.UI.WP.Toggle();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.ttl_h = new WinPaletter.UI.WP.Button();
            this.Trackbar2 = new WinPaletter.UI.WP.Trackbar();
            this.Trackbar1 = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.ComboBox1 = new WinPaletter.UI.WP.ComboBox();
            this.CheckBox6 = new WinPaletter.UI.WP.CheckBox();
            this.CheckBox7 = new WinPaletter.UI.WP.CheckBox();
            this.CheckBox8 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.GroupBox8 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.Label41 = new System.Windows.Forms.Label();
            this.pnl_preview = new System.Windows.Forms.Panel();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.color_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.RadioButton3 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioButton();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioButton();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.RadioButton4 = new WinPaletter.UI.WP.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.pnl_preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // OpenImgDlg
            // 
            this.OpenImgDlg.Filter = "Images (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All Files (*.*)|*.*";
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.Toggle1);
            this.GroupBox12.Controls.Add(this.Button9);
            this.GroupBox12.Controls.Add(this.Label12);
            this.GroupBox12.Controls.Add(this.Button11);
            this.GroupBox12.Controls.Add(this.Button12);
            this.GroupBox12.Controls.Add(this.checker_img);
            this.GroupBox12.Location = new System.Drawing.Point(12, 12);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(860, 39);
            this.GroupBox12.TabIndex = 201;
            // 
            // Toggle1
            // 
            this.Toggle1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Toggle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Toggle1.Checked = false;
            this.Toggle1.DarkLight_Toggler = false;
            this.Toggle1.Location = new System.Drawing.Point(815, 9);
            this.Toggle1.Name = "Toggle1";
            this.Toggle1.Size = new System.Drawing.Size(40, 20);
            this.Toggle1.TabIndex = 82;
            this.Toggle1.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.Toggle1_CheckedChanged);
            // 
            // Button9
            // 
            this.Button9.CustomColor = System.Drawing.Color.Empty;
            this.Button9.DrawOnGlass = false;
            this.Button9.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.Location = new System.Drawing.Point(222, 5);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(126, 29);
            this.Button9.TabIndex = 112;
            this.Button9.Text = "Current applied";
            this.Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(4, 4);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(75, 31);
            this.Label12.TabIndex = 111;
            this.Label12.Text = "Open from:";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            this.Button11.CustomColor = System.Drawing.Color.Empty;
            this.Button11.DrawOnGlass = false;
            this.Button11.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = ((System.Drawing.Image)(resources.GetObject("Button11.Image")));
            this.Button11.Location = new System.Drawing.Point(85, 5);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(135, 29);
            this.Button11.TabIndex = 110;
            this.Button11.Text = "WinPaletter theme";
            this.Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button12
            // 
            this.Button12.CustomColor = System.Drawing.Color.Empty;
            this.Button12.DrawOnGlass = false;
            this.Button12.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = null;
            this.Button12.Location = new System.Drawing.Point(351, 5);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(135, 29);
            this.Button12.TabIndex = 108;
            this.Button12.Text = "Default Windows";
            this.Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(774, 4);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.DrawOnGlass = false;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.Location = new System.Drawing.Point(603, 407);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 66;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.DrawOnGlass = false;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(689, 407);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(180, 34);
            this.Button1.TabIndex = 65;
            this.Button1.Text = "Load into current theme";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.Button4);
            this.GroupBox3.Controls.Add(this.ttl_h);
            this.GroupBox3.Controls.Add(this.Trackbar2);
            this.GroupBox3.Controls.Add(this.Trackbar1);
            this.GroupBox3.Controls.Add(this.PictureBox10);
            this.GroupBox3.Controls.Add(this.PictureBox9);
            this.GroupBox3.Controls.Add(this.PictureBox8);
            this.GroupBox3.Controls.Add(this.ComboBox1);
            this.GroupBox3.Controls.Add(this.CheckBox6);
            this.GroupBox3.Controls.Add(this.CheckBox7);
            this.GroupBox3.Controls.Add(this.CheckBox8);
            this.GroupBox3.Controls.Add(this.PictureBox3);
            this.GroupBox3.Controls.Add(this.Label2);
            this.GroupBox3.Location = new System.Drawing.Point(12, 242);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(322, 154);
            this.GroupBox3.TabIndex = 18;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.DrawOnGlass = false;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.Location = new System.Drawing.Point(281, 95);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(34, 25);
            this.Button4.TabIndex = 131;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // ttl_h
            // 
            this.ttl_h.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ttl_h.CustomColor = System.Drawing.Color.Empty;
            this.ttl_h.DrawOnGlass = false;
            this.ttl_h.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.ttl_h.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ttl_h.ForeColor = System.Drawing.Color.White;
            this.ttl_h.Image = null;
            this.ttl_h.Location = new System.Drawing.Point(282, 64);
            this.ttl_h.Name = "ttl_h";
            this.ttl_h.Size = new System.Drawing.Size(34, 25);
            this.ttl_h.TabIndex = 130;
            this.ttl_h.UseVisualStyleBackColor = false;
            this.ttl_h.Click += new System.EventHandler(this.ttl_h_Click);
            // 
            // Trackbar2
            // 
            this.Trackbar2.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar2.LargeChange = 10;
            this.Trackbar2.Location = new System.Drawing.Point(150, 98);
            this.Trackbar2.Maximum = 100;
            this.Trackbar2.Minimum = 0;
            this.Trackbar2.Name = "Trackbar2";
            this.Trackbar2.Size = new System.Drawing.Size(127, 19);
            this.Trackbar2.SmallChange = 1;
            this.Trackbar2.TabIndex = 93;
            this.Trackbar2.Value = 50;
            this.Trackbar2.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.NumericUpDown2_Click);
            // 
            // Trackbar1
            // 
            this.Trackbar1.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar1.LargeChange = 5;
            this.Trackbar1.Location = new System.Drawing.Point(150, 67);
            this.Trackbar1.Maximum = 35;
            this.Trackbar1.Minimum = 0;
            this.Trackbar1.Name = "Trackbar1";
            this.Trackbar1.Size = new System.Drawing.Size(128, 19);
            this.Trackbar1.SmallChange = 1;
            this.Trackbar1.TabIndex = 67;
            this.Trackbar1.Value = 1;
            this.Trackbar1.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.Trackbar1_Scroll);
            // 
            // PictureBox10
            // 
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(42, 95);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(25, 25);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 92;
            this.PictureBox10.TabStop = false;
            // 
            // PictureBox9
            // 
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(42, 64);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(25, 25);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 91;
            this.PictureBox9.TabStop = false;
            // 
            // PictureBox8
            // 
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(42, 33);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(25, 25);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 90;
            this.PictureBox8.TabStop = false;
            // 
            // ComboBox1
            // 
            this.ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ComboBox1.ForeColor = System.Drawing.Color.White;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.ItemHeight = 20;
            this.ComboBox1.Items.AddRange(new object[] {
            "Acrylic (Looks Like Windows 10/11)",
            "Aero"});
            this.ComboBox1.Location = new System.Drawing.Point(73, 124);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(243, 26);
            this.ComboBox1.TabIndex = 85;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // CheckBox6
            // 
            this.CheckBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CheckBox6.Checked = false;
            this.CheckBox6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox6.ForeColor = System.Drawing.Color.White;
            this.CheckBox6.Location = new System.Drawing.Point(73, 95);
            this.CheckBox6.Name = "CheckBox6";
            this.CheckBox6.Size = new System.Drawing.Size(74, 25);
            this.CheckBox6.TabIndex = 84;
            this.CheckBox6.Text = "Noise";
            this.CheckBox6.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox6_CheckedChanged);
            // 
            // CheckBox7
            // 
            this.CheckBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CheckBox7.Checked = false;
            this.CheckBox7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox7.ForeColor = System.Drawing.Color.White;
            this.CheckBox7.Location = new System.Drawing.Point(73, 64);
            this.CheckBox7.Name = "CheckBox7";
            this.CheckBox7.Size = new System.Drawing.Size(74, 25);
            this.CheckBox7.TabIndex = 83;
            this.CheckBox7.Text = "Blurred";
            this.CheckBox7.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox7_CheckedChanged);
            // 
            // CheckBox8
            // 
            this.CheckBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CheckBox8.Checked = false;
            this.CheckBox8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox8.ForeColor = System.Drawing.Color.White;
            this.CheckBox8.Location = new System.Drawing.Point(73, 33);
            this.CheckBox8.Name = "CheckBox8";
            this.CheckBox8.Size = new System.Drawing.Size(243, 25);
            this.CheckBox8.TabIndex = 82;
            this.CheckBox8.Text = "Gray-scale";
            this.CheckBox8.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox8_CheckedChanged);
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(3, 3);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(25, 25);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 17;
            this.PictureBox3.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(32, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(287, 25);
            this.Label2.TabIndex = 81;
            this.Label2.Text = "Effects";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox8
            // 
            this.GroupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox8.Controls.Add(this.PictureBox41);
            this.GroupBox8.Controls.Add(this.Label41);
            this.GroupBox8.Controls.Add(this.pnl_preview);
            this.GroupBox8.Location = new System.Drawing.Point(337, 54);
            this.GroupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox8.Size = new System.Drawing.Size(536, 342);
            this.GroupBox8.TabIndex = 15;
            // 
            // PictureBox41
            // 
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(4, 4);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(35, 35);
            this.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox41.TabIndex = 4;
            this.PictureBox41.TabStop = false;
            // 
            // Label41
            // 
            this.Label41.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label41.Location = new System.Drawing.Point(45, 4);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(142, 35);
            this.Label41.TabIndex = 3;
            this.Label41.Text = "Preview";
            this.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_preview
            // 
            this.pnl_preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_preview.Controls.Add(this.PictureBox11);
            this.pnl_preview.Location = new System.Drawing.Point(4, 41);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 2;
            // 
            // PictureBox11
            // 
            this.PictureBox11.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(0, 0);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(528, 297);
            this.PictureBox11.TabIndex = 0;
            this.PictureBox11.TabStop = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox2.Controls.Add(this.Button3);
            this.GroupBox2.Controls.Add(this.PictureBox7);
            this.GroupBox2.Controls.Add(this.PictureBox6);
            this.GroupBox2.Controls.Add(this.PictureBox5);
            this.GroupBox2.Controls.Add(this.PictureBox4);
            this.GroupBox2.Controls.Add(this.color_pick);
            this.GroupBox2.Controls.Add(this.TextBox1);
            this.GroupBox2.Controls.Add(this.RadioButton3);
            this.GroupBox2.Controls.Add(this.RadioButton2);
            this.GroupBox2.Controls.Add(this.Button7);
            this.GroupBox2.Controls.Add(this.RadioButton1);
            this.GroupBox2.Controls.Add(this.PictureBox2);
            this.GroupBox2.Controls.Add(this.RadioButton4);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Location = new System.Drawing.Point(12, 54);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(322, 185);
            this.GroupBox2.TabIndex = 17;
            // 
            // Button3
            // 
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.DrawOnGlass = false;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.Location = new System.Drawing.Point(221, 31);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(97, 25);
            this.Button3.TabIndex = 93;
            this.Button3.Text = "Choose";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Visible = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(42, 93);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(25, 25);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 92;
            this.PictureBox7.TabStop = false;
            // 
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(42, 124);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(25, 25);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 91;
            this.PictureBox6.TabStop = false;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(42, 62);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(25, 25);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 90;
            this.PictureBox5.TabStop = false;
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(42, 31);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(25, 25);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 89;
            this.PictureBox4.TabStop = false;
            // 
            // color_pick
            // 
            this.color_pick.AllowDrop = true;
            this.color_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.color_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.color_pick.DefaultColor = System.Drawing.Color.Black;
            this.color_pick.DontShowInfo = false;
            this.color_pick.Location = new System.Drawing.Point(219, 93);
            this.color_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.color_pick.Name = "color_pick";
            this.color_pick.Size = new System.Drawing.Size(97, 25);
            this.color_pick.TabIndex = 88;
            this.color_pick.Click += new System.EventHandler(this.Color_pick_Click);
            this.color_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.Color_pick_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(98, 155);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(178, 24);
            this.TextBox1.TabIndex = 86;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // RadioButton3
            // 
            this.RadioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.RadioButton3.Checked = false;
            this.RadioButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton3.ForeColor = System.Drawing.Color.White;
            this.RadioButton3.Location = new System.Drawing.Point(73, 93);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(139, 25);
            this.RadioButton3.TabIndex = 85;
            this.RadioButton3.Text = "Solid color";
            this.RadioButton3.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Location = new System.Drawing.Point(73, 62);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(243, 25);
            this.RadioButton2.TabIndex = 83;
            this.RadioButton2.Text = "Current wallpaper";
            this.RadioButton2.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton2_CheckedChanged);
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.DrawOnGlass = false;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = ((System.Drawing.Image)(resources.GetObject("Button7.Image")));
            this.Button7.Location = new System.Drawing.Point(281, 155);
            this.Button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(35, 24);
            this.Button7.TabIndex = 87;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.RadioButton1.Checked = true;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Location = new System.Drawing.Point(73, 31);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(139, 25);
            this.RadioButton1.TabIndex = 82;
            this.RadioButton1.Text = "System default";
            this.RadioButton1.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton1_CheckedChanged);
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(3, 3);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(25, 25);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 17;
            this.PictureBox2.TabStop = false;
            // 
            // RadioButton4
            // 
            this.RadioButton4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.RadioButton4.Checked = false;
            this.RadioButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton4.ForeColor = System.Drawing.Color.White;
            this.RadioButton4.Location = new System.Drawing.Point(73, 124);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(243, 25);
            this.RadioButton4.TabIndex = 84;
            this.RadioButton4.Text = "Custom image";
            this.RadioButton4.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton4_CheckedChanged);
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(32, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(287, 25);
            this.Label1.TabIndex = 81;
            this.Label1.Text = "Source";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LogonUI7
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(884, 453);
            this.Controls.Add(this.GroupBox12);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox8);
            this.Controls.Add(this.GroupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonUI7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogonUI";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.LogonUI7_Load);
            this.Shown += new System.EventHandler(this.LogonUI7_Shown);
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.pnl_preview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.ResumeLayout(false);

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