using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ApplicationThemer : AspectsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationThemer));
            this.Label29 = new System.Windows.Forms.Label();
            this.PictureBox46 = new System.Windows.Forms.PictureBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.PictureBox45 = new System.Windows.Forms.PictureBox();
            this.PictureBox44 = new System.Windows.Forms.PictureBox();
            this.PictureBox43 = new System.Windows.Forms.PictureBox();
            this.BackColorPick = new WinPaletter.UI.Controllers.ColorItem();
            this.AccentColor = new WinPaletter.UI.Controllers.ColorItem();
            this.RoundedCorners = new WinPaletter.UI.WP.CheckBox();
            this.appearance_dark = new WinPaletter.UI.WP.CheckBox();
            this.appearance_list = new WinPaletter.UI.WP.ComboBox();
            this.AlertBox2 = new WinPaletter.UI.WP.AlertBox();
            this.testControl5 = new WinPaletter.UI.WP.TestControl();
            this.testControl4 = new WinPaletter.UI.WP.TestControl();
            this.testControl3 = new WinPaletter.UI.WP.TestControl();
            this.testControl2 = new WinPaletter.UI.WP.TestControl();
            this.testControl1 = new WinPaletter.UI.WP.TestControl();
            this.groupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new WinPaletter.UI.WP.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.separatorV2 = new WinPaletter.UI.WP.SeparatorV();
            this.testControl10 = new WinPaletter.UI.WP.TestControl();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.testControl11 = new WinPaletter.UI.WP.TestControl();
            this.testControl12 = new WinPaletter.UI.WP.TestControl();
            this.colorItem4 = new WinPaletter.UI.Controllers.ColorItem();
            this.testControl13 = new WinPaletter.UI.WP.TestControl();
            this.testControl8 = new WinPaletter.UI.WP.TestControl();
            this.testControl14 = new WinPaletter.UI.WP.TestControl();
            this.testControl9 = new WinPaletter.UI.WP.TestControl();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.colorItem3 = new WinPaletter.UI.Controllers.ColorItem();
            this.testControl6 = new WinPaletter.UI.WP.TestControl();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.testControl7 = new WinPaletter.UI.WP.TestControl();
            this.colorItem2 = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.colorItem1 = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox43)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.Size = new System.Drawing.Size(1022, 52);
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.Transparent;
            this.Label29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(39, 39);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(208, 24);
            this.Label29.TabIndex = 226;
            this.Label29.Text = "Background color:";
            this.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox46
            // 
            this.PictureBox46.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox46.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox46.Image")));
            this.PictureBox46.Location = new System.Drawing.Point(9, 39);
            this.PictureBox46.Name = "PictureBox46";
            this.PictureBox46.Size = new System.Drawing.Size(24, 24);
            this.PictureBox46.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox46.TabIndex = 225;
            this.PictureBox46.TabStop = false;
            // 
            // Label28
            // 
            this.Label28.BackColor = System.Drawing.Color.Transparent;
            this.Label28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label28.Location = new System.Drawing.Point(39, 99);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(208, 24);
            this.Label28.TabIndex = 223;
            this.Label28.Text = "Accent color:";
            this.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox45
            // 
            this.PictureBox45.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox45.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox45.Image")));
            this.PictureBox45.Location = new System.Drawing.Point(9, 99);
            this.PictureBox45.Name = "PictureBox45";
            this.PictureBox45.Size = new System.Drawing.Size(24, 24);
            this.PictureBox45.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox45.TabIndex = 222;
            this.PictureBox45.TabStop = false;
            // 
            // PictureBox44
            // 
            this.PictureBox44.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox44.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox44.Image")));
            this.PictureBox44.Location = new System.Drawing.Point(9, 69);
            this.PictureBox44.Name = "PictureBox44";
            this.PictureBox44.Size = new System.Drawing.Size(24, 24);
            this.PictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox44.TabIndex = 220;
            this.PictureBox44.TabStop = false;
            // 
            // PictureBox43
            // 
            this.PictureBox43.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox43.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox43.Image")));
            this.PictureBox43.Location = new System.Drawing.Point(9, 39);
            this.PictureBox43.Name = "PictureBox43";
            this.PictureBox43.Size = new System.Drawing.Size(24, 24);
            this.PictureBox43.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox43.TabIndex = 218;
            this.PictureBox43.TabStop = false;
            // 
            // BackColorPick
            // 
            this.BackColorPick.AllowDrop = true;
            this.BackColorPick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BackColorPick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.BackColorPick.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.BackColorPick.DontShowInfo = false;
            this.BackColorPick.Location = new System.Drawing.Point(705, 39);
            this.BackColorPick.Name = "BackColorPick";
            this.BackColorPick.Size = new System.Drawing.Size(112, 24);
            this.BackColorPick.TabIndex = 227;
            this.BackColorPick.Click += new System.EventHandler(this.BackColorPick_Click);
            this.BackColorPick.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccentColor_BackColorPick_DragDrop);
            // 
            // AccentColor
            // 
            this.AccentColor.AllowDrop = true;
            this.AccentColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AccentColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(175)))));
            this.AccentColor.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(175)))));
            this.AccentColor.DontShowInfo = false;
            this.AccentColor.Location = new System.Drawing.Point(705, 99);
            this.AccentColor.Name = "AccentColor";
            this.AccentColor.Size = new System.Drawing.Size(112, 24);
            this.AccentColor.TabIndex = 224;
            this.AccentColor.Click += new System.EventHandler(this.AccentColor_Click);
            this.AccentColor.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccentColor_BackColorPick_DragDrop);
            // 
            // RoundedCorners
            // 
            this.RoundedCorners.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoundedCorners.Checked = true;
            this.RoundedCorners.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RoundedCorners.ForeColor = System.Drawing.Color.White;
            this.RoundedCorners.Location = new System.Drawing.Point(39, 69);
            this.RoundedCorners.Name = "RoundedCorners";
            this.RoundedCorners.Size = new System.Drawing.Size(959, 24);
            this.RoundedCorners.TabIndex = 221;
            this.RoundedCorners.Text = "Rounded corners";
            this.RoundedCorners.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckedChanged);
            // 
            // appearance_dark
            // 
            this.appearance_dark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appearance_dark.Checked = true;
            this.appearance_dark.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.appearance_dark.ForeColor = System.Drawing.Color.White;
            this.appearance_dark.Location = new System.Drawing.Point(39, 39);
            this.appearance_dark.Name = "appearance_dark";
            this.appearance_dark.Size = new System.Drawing.Size(959, 24);
            this.appearance_dark.TabIndex = 219;
            this.appearance_dark.Text = "Dark mode";
            this.appearance_dark.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckedChanged);
            // 
            // appearance_list
            // 
            this.appearance_list.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appearance_list.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.appearance_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appearance_list.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.appearance_list.ForeColor = System.Drawing.Color.White;
            this.appearance_list.FormattingEnabled = true;
            this.appearance_list.ItemHeight = 24;
            this.appearance_list.Items.AddRange(new object[] {
            "Default Dark",
            "Default Light",
            "AMOLED",
            "Extreme White",
            "GitHub Dark",
            "GitHub Light",
            "Reddit Dark",
            "Reddit Light",
            "Discord Dark",
            "Discord Light"});
            this.appearance_list.Location = new System.Drawing.Point(284, 3);
            this.appearance_list.Name = "appearance_list";
            this.appearance_list.Size = new System.Drawing.Size(714, 30);
            this.appearance_list.TabIndex = 216;
            this.appearance_list.SelectedIndexChanged += new System.EventHandler(this.Appearance_list_SelectedIndexChanged);
            // 
            // AlertBox2
            // 
            this.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox2.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox2.CenterText = false;
            this.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox2.Image = null;
            this.AlertBox2.Location = new System.Drawing.Point(12, 475);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(998, 49);
            this.AlertBox2.TabIndex = 230;
            this.AlertBox2.TabStop = false;
            this.AlertBox2.Text = resources.GetString("AlertBox2.Text");
            // 
            // testControl5
            // 
            this.testControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl5.BackColor = System.Drawing.Color.Transparent;
            this.testControl5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl5.Location = new System.Drawing.Point(945, 39);
            this.testControl5.Name = "testControl5";
            this.testControl5.Size = new System.Drawing.Size(51, 24);
            this.testControl5.State = WinPaletter.UI.WP.TestControl.States.Max;
            this.testControl5.TabIndex = 235;
            // 
            // testControl4
            // 
            this.testControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl4.BackColor = System.Drawing.Color.Transparent;
            this.testControl4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl4.Location = new System.Drawing.Point(888, 39);
            this.testControl4.Name = "testControl4";
            this.testControl4.Size = new System.Drawing.Size(51, 24);
            this.testControl4.State = WinPaletter.UI.WP.TestControl.States.Hover;
            this.testControl4.TabIndex = 234;
            // 
            // testControl3
            // 
            this.testControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl3.BackColor = System.Drawing.Color.Transparent;
            this.testControl3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl3.Location = new System.Drawing.Point(916, 99);
            this.testControl3.Name = "testControl3";
            this.testControl3.Size = new System.Drawing.Size(79, 24);
            this.testControl3.State = WinPaletter.UI.WP.TestControl.States.CheckedHover;
            this.testControl3.TabIndex = 233;
            // 
            // testControl2
            // 
            this.testControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl2.BackColor = System.Drawing.Color.Transparent;
            this.testControl2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl2.Location = new System.Drawing.Point(831, 99);
            this.testControl2.Name = "testControl2";
            this.testControl2.Size = new System.Drawing.Size(79, 24);
            this.testControl2.State = WinPaletter.UI.WP.TestControl.States.Checked;
            this.testControl2.TabIndex = 232;
            // 
            // testControl1
            // 
            this.testControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl1.BackColor = System.Drawing.Color.Transparent;
            this.testControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl1.Location = new System.Drawing.Point(831, 39);
            this.testControl1.Name = "testControl1";
            this.testControl1.Size = new System.Drawing.Size(51, 24);
            this.testControl1.State = WinPaletter.UI.WP.TestControl.States.None;
            this.testControl1.TabIndex = 231;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.pictureBox3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.appearance_list);
            this.groupBox2.Location = new System.Drawing.Point(9, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1001, 37);
            this.groupBox2.TabIndex = 236;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 90;
            this.pictureBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(39, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(239, 31);
            this.label5.TabIndex = 84;
            this.label5.Text = "Choose a scheme for a quick change:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.pictureBox1);
            this.GroupBox1.Controls.Add(this.checkBox1);
            this.GroupBox1.Controls.Add(this.pictureBox2);
            this.GroupBox1.Controls.Add(this.label2);
            this.GroupBox1.Controls.Add(this.PictureBox43);
            this.GroupBox1.Controls.Add(this.appearance_dark);
            this.GroupBox1.Controls.Add(this.PictureBox44);
            this.GroupBox1.Controls.Add(this.RoundedCorners);
            this.GroupBox1.Location = new System.Drawing.Point(9, 105);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(1001, 130);
            this.GroupBox1.TabIndex = 237;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 99);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 222;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Checked = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(39, 99);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(959, 24);
            this.checkBox1.TabIndex = 223;
            this.checkBox1.Text = "Animations";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(959, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Basic options";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.separatorV2);
            this.groupBox3.Controls.Add(this.testControl10);
            this.groupBox3.Controls.Add(this.pictureBox8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.testControl11);
            this.groupBox3.Controls.Add(this.testControl12);
            this.groupBox3.Controls.Add(this.colorItem4);
            this.groupBox3.Controls.Add(this.testControl13);
            this.groupBox3.Controls.Add(this.testControl8);
            this.groupBox3.Controls.Add(this.testControl14);
            this.groupBox3.Controls.Add(this.testControl9);
            this.groupBox3.Controls.Add(this.pictureBox6);
            this.groupBox3.Controls.Add(this.pictureBox7);
            this.groupBox3.Controls.Add(this.colorItem3);
            this.groupBox3.Controls.Add(this.testControl6);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.testControl7);
            this.groupBox3.Controls.Add(this.colorItem2);
            this.groupBox3.Controls.Add(this.pictureBox4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.colorItem1);
            this.groupBox3.Controls.Add(this.pictureBox5);
            this.groupBox3.Controls.Add(this.testControl5);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.testControl4);
            this.groupBox3.Controls.Add(this.PictureBox46);
            this.groupBox3.Controls.Add(this.testControl3);
            this.groupBox3.Controls.Add(this.Label29);
            this.groupBox3.Controls.Add(this.testControl2);
            this.groupBox3.Controls.Add(this.BackColorPick);
            this.groupBox3.Controls.Add(this.testControl1);
            this.groupBox3.Controls.Add(this.PictureBox45);
            this.groupBox3.Controls.Add(this.Label28);
            this.groupBox3.Controls.Add(this.AccentColor);
            this.groupBox3.Location = new System.Drawing.Point(9, 241);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1001, 220);
            this.groupBox3.TabIndex = 238;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(831, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 30);
            this.label8.TabIndex = 251;
            this.label8.Text = "Preview";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // separatorV2
            // 
            this.separatorV2.AlternativeLook = false;
            this.separatorV2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorV2.BackColor = System.Drawing.Color.Transparent;
            this.separatorV2.Location = new System.Drawing.Point(824, 4);
            this.separatorV2.Name = "separatorV2";
            this.separatorV2.Size = new System.Drawing.Size(1, 212);
            this.separatorV2.TabIndex = 250;
            this.separatorV2.TabStop = false;
            this.separatorV2.Text = "separatorV2";
            // 
            // testControl10
            // 
            this.testControl10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl10.BackColor = System.Drawing.Color.Transparent;
            this.testControl10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl10.Location = new System.Drawing.Point(945, 69);
            this.testControl10.Name = "testControl10";
            this.testControl10.Size = new System.Drawing.Size(51, 24);
            this.testControl10.State = WinPaletter.UI.WP.TestControl.States.Max;
            this.testControl10.TabIndex = 249;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(9, 69);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(24, 24);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox8.TabIndex = 237;
            this.pictureBox8.TabStop = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(39, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 24);
            this.label7.TabIndex = 238;
            this.label7.Text = "Disabled background colors:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // testControl11
            // 
            this.testControl11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl11.BackColor = System.Drawing.Color.Transparent;
            this.testControl11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl11.Location = new System.Drawing.Point(888, 69);
            this.testControl11.Name = "testControl11";
            this.testControl11.Size = new System.Drawing.Size(51, 24);
            this.testControl11.State = WinPaletter.UI.WP.TestControl.States.Hover;
            this.testControl11.TabIndex = 248;
            // 
            // testControl12
            // 
            this.testControl12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl12.BackColor = System.Drawing.Color.Transparent;
            this.testControl12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl12.Location = new System.Drawing.Point(916, 189);
            this.testControl12.Name = "testControl12";
            this.testControl12.Size = new System.Drawing.Size(79, 24);
            this.testControl12.State = WinPaletter.UI.WP.TestControl.States.CheckedHover;
            this.testControl12.TabIndex = 246;
            // 
            // colorItem4
            // 
            this.colorItem4.AllowDrop = true;
            this.colorItem4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.colorItem4.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.colorItem4.DontShowInfo = false;
            this.colorItem4.Location = new System.Drawing.Point(705, 69);
            this.colorItem4.Name = "colorItem4";
            this.colorItem4.Size = new System.Drawing.Size(112, 24);
            this.colorItem4.TabIndex = 239;
            this.colorItem4.Click += new System.EventHandler(this.colorItem4_Click);
            this.colorItem4.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccentColor_BackColorPick_DragDrop);
            // 
            // testControl13
            // 
            this.testControl13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl13.BackColor = System.Drawing.Color.Transparent;
            this.testControl13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl13.Location = new System.Drawing.Point(831, 189);
            this.testControl13.Name = "testControl13";
            this.testControl13.Size = new System.Drawing.Size(79, 24);
            this.testControl13.State = WinPaletter.UI.WP.TestControl.States.Checked;
            this.testControl13.TabIndex = 245;
            // 
            // testControl8
            // 
            this.testControl8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl8.BackColor = System.Drawing.Color.Transparent;
            this.testControl8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl8.Location = new System.Drawing.Point(916, 159);
            this.testControl8.Name = "testControl8";
            this.testControl8.Size = new System.Drawing.Size(79, 24);
            this.testControl8.State = WinPaletter.UI.WP.TestControl.States.CheckedHover;
            this.testControl8.TabIndex = 242;
            // 
            // testControl14
            // 
            this.testControl14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl14.BackColor = System.Drawing.Color.Transparent;
            this.testControl14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl14.Location = new System.Drawing.Point(831, 69);
            this.testControl14.Name = "testControl14";
            this.testControl14.Size = new System.Drawing.Size(51, 24);
            this.testControl14.State = WinPaletter.UI.WP.TestControl.States.None;
            this.testControl14.TabIndex = 247;
            // 
            // testControl9
            // 
            this.testControl9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl9.BackColor = System.Drawing.Color.Transparent;
            this.testControl9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl9.Location = new System.Drawing.Point(831, 159);
            this.testControl9.Name = "testControl9";
            this.testControl9.Size = new System.Drawing.Size(79, 24);
            this.testControl9.State = WinPaletter.UI.WP.TestControl.States.Checked;
            this.testControl9.TabIndex = 241;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(9, 159);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(24, 24);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox6.TabIndex = 231;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(9, 189);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(24, 24);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox7.TabIndex = 234;
            this.pictureBox7.TabStop = false;
            // 
            // colorItem3
            // 
            this.colorItem3.AllowDrop = true;
            this.colorItem3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.colorItem3.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.colorItem3.DontShowInfo = false;
            this.colorItem3.Location = new System.Drawing.Point(705, 189);
            this.colorItem3.Name = "colorItem3";
            this.colorItem3.Size = new System.Drawing.Size(112, 24);
            this.colorItem3.TabIndex = 236;
            this.colorItem3.Click += new System.EventHandler(this.colorItem3_Click);
            this.colorItem3.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccentColor_BackColorPick_DragDrop);
            // 
            // testControl6
            // 
            this.testControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl6.BackColor = System.Drawing.Color.Transparent;
            this.testControl6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl6.Location = new System.Drawing.Point(916, 129);
            this.testControl6.Name = "testControl6";
            this.testControl6.Size = new System.Drawing.Size(79, 24);
            this.testControl6.State = WinPaletter.UI.WP.TestControl.States.CheckedHover;
            this.testControl6.TabIndex = 240;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(39, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(208, 24);
            this.label6.TabIndex = 235;
            this.label6.Text = "Disabled items colors:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(39, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 24);
            this.label4.TabIndex = 232;
            this.label4.Text = "Tertiary color (for info, tips, ...):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // testControl7
            // 
            this.testControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testControl7.BackColor = System.Drawing.Color.Transparent;
            this.testControl7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl7.Location = new System.Drawing.Point(831, 129);
            this.testControl7.Name = "testControl7";
            this.testControl7.Size = new System.Drawing.Size(79, 24);
            this.testControl7.State = WinPaletter.UI.WP.TestControl.States.Checked;
            this.testControl7.TabIndex = 239;
            // 
            // colorItem2
            // 
            this.colorItem2.AllowDrop = true;
            this.colorItem2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(21)))));
            this.colorItem2.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(21)))));
            this.colorItem2.DontShowInfo = false;
            this.colorItem2.Location = new System.Drawing.Point(705, 159);
            this.colorItem2.Name = "colorItem2";
            this.colorItem2.Size = new System.Drawing.Size(112, 24);
            this.colorItem2.TabIndex = 233;
            this.colorItem2.Click += new System.EventHandler(this.colorItem2_Click);
            this.colorItem2.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccentColor_BackColorPick_DragDrop);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(9, 129);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 228;
            this.pictureBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 24);
            this.label3.TabIndex = 229;
            this.label3.Text = "Secondary color (for errors):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorItem1
            // 
            this.colorItem1.AllowDrop = true;
            this.colorItem1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(0)))), ((int)(((byte)(31)))));
            this.colorItem1.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(0)))), ((int)(((byte)(31)))));
            this.colorItem1.DontShowInfo = false;
            this.colorItem1.Location = new System.Drawing.Point(705, 129);
            this.colorItem1.Name = "colorItem1";
            this.colorItem1.Size = new System.Drawing.Size(112, 24);
            this.colorItem1.TabIndex = 230;
            this.colorItem1.Click += new System.EventHandler(this.colorItem1_Click);
            this.colorItem1.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccentColor_BackColorPick_DragDrop);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(3, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(30, 30);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox5.TabIndex = 1;
            this.pictureBox5.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(778, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Colors";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ApplicationThemer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1022, 586);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.AlertBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationThemer";
            this.Text = "WinPaletter application theme";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ApplicationThemer_FormClosing);
            this.Load += new System.EventHandler(this.ApplicationThemer_Editor_Load);
            this.Controls.SetChildIndex(this.AlertBox2, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.GroupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox43)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }
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
        internal UI.WP.AlertBox AlertBox2;
        public UI.WP.TestControl testControl5;
        public UI.WP.TestControl testControl4;
        public UI.WP.TestControl testControl3;
        public UI.WP.TestControl testControl2;
        public UI.WP.TestControl testControl1;
        private UI.WP.GroupBox groupBox2;
        internal PictureBox pictureBox3;
        internal Label label5;
        internal UI.WP.GroupBox GroupBox1;
        internal PictureBox pictureBox1;
        internal UI.WP.CheckBox checkBox1;
        internal PictureBox pictureBox2;
        internal Label label2;
        internal UI.WP.GroupBox groupBox3;
        internal PictureBox pictureBox6;
        internal Label label4;
        internal UI.Controllers.ColorItem colorItem2;
        internal PictureBox pictureBox4;
        internal Label label3;
        internal UI.Controllers.ColorItem colorItem1;
        internal PictureBox pictureBox5;
        internal Label label1;
        public UI.WP.TestControl testControl6;
        public UI.WP.TestControl testControl7;
        public UI.WP.TestControl testControl8;
        public UI.WP.TestControl testControl9;
        internal PictureBox pictureBox7;
        internal Label label6;
        internal UI.Controllers.ColorItem colorItem3;
        internal PictureBox pictureBox8;
        internal Label label7;
        internal UI.Controllers.ColorItem colorItem4;
        public UI.WP.TestControl testControl12;
        public UI.WP.TestControl testControl13;
        public UI.WP.TestControl testControl10;
        public UI.WP.TestControl testControl11;
        public UI.WP.TestControl testControl14;
        private UI.WP.SeparatorV separatorV2;
        internal Label label8;
    }
}
