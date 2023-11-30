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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationThemer));
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
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
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.AppThemeEnabled = new WinPaletter.UI.WP.Toggle();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.appearance_list = new WinPaletter.UI.WP.ComboBox();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.AlertBox2 = new WinPaletter.UI.WP.AlertBox();
            this.testControl5 = new WinPaletter.UI.WP.TestControl();
            this.testControl4 = new WinPaletter.UI.WP.TestControl();
            this.testControl3 = new WinPaletter.UI.WP.TestControl();
            this.testControl2 = new WinPaletter.UI.WP.TestControl();
            this.testControl1 = new WinPaletter.UI.WP.TestControl();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox43)).BeginInit();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // Label29
            // 
            this.Label29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(42, 181);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(130, 24);
            this.Label29.TabIndex = 226;
            this.Label29.Text = "Background color:";
            this.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox46
            // 
            this.PictureBox46.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox46.Image")));
            this.PictureBox46.Location = new System.Drawing.Point(12, 181);
            this.PictureBox46.Name = "PictureBox46";
            this.PictureBox46.Size = new System.Drawing.Size(24, 24);
            this.PictureBox46.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox46.TabIndex = 225;
            this.PictureBox46.TabStop = false;
            // 
            // Label28
            // 
            this.Label28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label28.Location = new System.Drawing.Point(42, 151);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(130, 24);
            this.Label28.TabIndex = 223;
            this.Label28.Text = "Accent color:";
            this.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox45
            // 
            this.PictureBox45.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox45.Image")));
            this.PictureBox45.Location = new System.Drawing.Point(12, 151);
            this.PictureBox45.Name = "PictureBox45";
            this.PictureBox45.Size = new System.Drawing.Size(24, 24);
            this.PictureBox45.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox45.TabIndex = 222;
            this.PictureBox45.TabStop = false;
            // 
            // PictureBox44
            // 
            this.PictureBox44.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox44.Image")));
            this.PictureBox44.Location = new System.Drawing.Point(12, 121);
            this.PictureBox44.Name = "PictureBox44";
            this.PictureBox44.Size = new System.Drawing.Size(24, 24);
            this.PictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox44.TabIndex = 220;
            this.PictureBox44.TabStop = false;
            // 
            // PictureBox43
            // 
            this.PictureBox43.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox43.Image")));
            this.PictureBox43.Location = new System.Drawing.Point(12, 91);
            this.PictureBox43.Name = "PictureBox43";
            this.PictureBox43.Size = new System.Drawing.Size(24, 24);
            this.PictureBox43.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox43.TabIndex = 218;
            this.PictureBox43.TabStop = false;
            // 
            // BackColorPick
            // 
            this.BackColorPick.AllowDrop = true;
            this.BackColorPick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BackColorPick.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BackColorPick.DontShowInfo = false;
            this.BackColorPick.Location = new System.Drawing.Point(178, 181);
            this.BackColorPick.Name = "BackColorPick";
            this.BackColorPick.Size = new System.Drawing.Size(112, 24);
            this.BackColorPick.TabIndex = 227;
            this.BackColorPick.Click += new System.EventHandler(this.BackColorPick_Click);
            this.BackColorPick.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccentColor_BackColorPick_DragDrop);
            // 
            // AccentColor
            // 
            this.AccentColor.AllowDrop = true;
            this.AccentColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AccentColor.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AccentColor.DontShowInfo = false;
            this.AccentColor.Location = new System.Drawing.Point(178, 151);
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
            this.RoundedCorners.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.RoundedCorners.Checked = true;
            this.RoundedCorners.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RoundedCorners.ForeColor = System.Drawing.Color.White;
            this.RoundedCorners.Location = new System.Drawing.Point(42, 121);
            this.RoundedCorners.Name = "RoundedCorners";
            this.RoundedCorners.Size = new System.Drawing.Size(319, 24);
            this.RoundedCorners.TabIndex = 221;
            this.RoundedCorners.Text = "Rounded corners";
            this.RoundedCorners.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckedChanged);
            // 
            // appearance_dark
            // 
            this.appearance_dark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appearance_dark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.appearance_dark.Checked = true;
            this.appearance_dark.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.appearance_dark.ForeColor = System.Drawing.Color.White;
            this.appearance_dark.Location = new System.Drawing.Point(42, 91);
            this.appearance_dark.Name = "appearance_dark";
            this.appearance_dark.Size = new System.Drawing.Size(319, 24);
            this.appearance_dark.TabIndex = 219;
            this.appearance_dark.Text = "Dark mode";
            this.appearance_dark.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckedChanged);
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.Location = new System.Drawing.Point(485, 314);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(80, 34);
            this.Button7.TabIndex = 212;
            this.Button7.Text = "Cancel";
            this.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button8.CustomColor = System.Drawing.Color.Empty;
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.Location = new System.Drawing.Point(692, 314);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(180, 34);
            this.Button8.TabIndex = 211;
            this.Button8.Text = "Load into current theme";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.Button9);
            this.GroupBox12.Controls.Add(this.Label12);
            this.GroupBox12.Controls.Add(this.Button11);
            this.GroupBox12.Controls.Add(this.Button12);
            this.GroupBox12.Controls.Add(this.AppThemeEnabled);
            this.GroupBox12.Controls.Add(this.checker_img);
            this.GroupBox12.Controls.Add(this.Label25);
            this.GroupBox12.Controls.Add(this.appearance_list);
            this.GroupBox12.Location = new System.Drawing.Point(12, 12);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(860, 70);
            this.GroupBox12.TabIndex = 202;
            // 
            // Button9
            // 
            this.Button9.CustomColor = System.Drawing.Color.Empty;
            this.Button9.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.Location = new System.Drawing.Point(223, 5);
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
            this.Button12.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = ((System.Drawing.Image)(resources.GetObject("Button12.Image")));
            this.Button12.Location = new System.Drawing.Point(352, 5);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(144, 29);
            this.Button12.TabIndex = 108;
            this.Button12.Text = "Default application";
            this.Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // AppThemeEnabled
            // 
            this.AppThemeEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppThemeEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.AppThemeEnabled.Checked = false;
            this.AppThemeEnabled.DarkLight_Toggler = false;
            this.AppThemeEnabled.Location = new System.Drawing.Point(814, 25);
            this.AppThemeEnabled.Name = "AppThemeEnabled";
            this.AppThemeEnabled.Size = new System.Drawing.Size(40, 20);
            this.AppThemeEnabled.TabIndex = 85;
            this.AppThemeEnabled.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.AppThemeEnabled_CheckedChanged);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(773, 20);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // Label25
            // 
            this.Label25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(4, 40);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(75, 24);
            this.Label25.TabIndex = 215;
            this.Label25.Text = "Scheme:";
            this.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // appearance_list
            // 
            this.appearance_list.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.appearance_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appearance_list.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.appearance_list.ForeColor = System.Drawing.Color.White;
            this.appearance_list.FormattingEnabled = true;
            this.appearance_list.ItemHeight = 20;
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
            this.appearance_list.Location = new System.Drawing.Point(85, 38);
            this.appearance_list.Name = "appearance_list";
            this.appearance_list.Size = new System.Drawing.Size(411, 26);
            this.appearance_list.TabIndex = 216;
            this.appearance_list.SelectedIndexChanged += new System.EventHandler(this.Appearance_list_SelectedIndexChanged);
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button10.CustomColor = System.Drawing.Color.Empty;
            this.Button10.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = ((System.Drawing.Image)(resources.GetObject("Button10.Image")));
            this.Button10.Location = new System.Drawing.Point(571, 314);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(115, 34);
            this.Button10.TabIndex = 228;
            this.Button10.Text = "Quick apply";
            this.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click_1);
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(12, 218);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(860, 22);
            this.AlertBox1.TabIndex = 229;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "To preview changes, enable the toggle above";
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
            this.AlertBox2.Location = new System.Drawing.Point(12, 247);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(860, 60);
            this.AlertBox2.TabIndex = 230;
            this.AlertBox2.TabStop = false;
            this.AlertBox2.Text = resources.GetString("AlertBox2.Text");
            // 
            // testControl5
            // 
            this.testControl5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.testControl5.BackColor = System.Drawing.Color.Transparent;
            this.testControl5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl5.Location = new System.Drawing.Point(572, 101);
            this.testControl5.Name = "testControl5";
            this.testControl5.Size = new System.Drawing.Size(96, 96);
            this.testControl5.State = WinPaletter.UI.WP.TestControl.States.Max;
            this.testControl5.TabIndex = 235;
            this.testControl5.Text = "Max back color for a control with its border";
            // 
            // testControl4
            // 
            this.testControl4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.testControl4.BackColor = System.Drawing.Color.Transparent;
            this.testControl4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl4.Location = new System.Drawing.Point(470, 101);
            this.testControl4.Name = "testControl4";
            this.testControl4.Size = new System.Drawing.Size(96, 96);
            this.testControl4.State = WinPaletter.UI.WP.TestControl.States.Hover;
            this.testControl4.TabIndex = 234;
            this.testControl4.Text = "Hover back color for a control with its border";
            // 
            // testControl3
            // 
            this.testControl3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.testControl3.BackColor = System.Drawing.Color.Transparent;
            this.testControl3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl3.Location = new System.Drawing.Point(776, 101);
            this.testControl3.Name = "testControl3";
            this.testControl3.Size = new System.Drawing.Size(96, 96);
            this.testControl3.State = WinPaletter.UI.WP.TestControl.States.CheckedHover;
            this.testControl3.TabIndex = 233;
            this.testControl3.Text = "Hover on checked control back color with its border";
            // 
            // testControl2
            // 
            this.testControl2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.testControl2.BackColor = System.Drawing.Color.Transparent;
            this.testControl2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl2.Location = new System.Drawing.Point(674, 101);
            this.testControl2.Name = "testControl2";
            this.testControl2.Size = new System.Drawing.Size(96, 96);
            this.testControl2.State = WinPaletter.UI.WP.TestControl.States.Checked;
            this.testControl2.TabIndex = 232;
            this.testControl2.Text = "Checked control back color with its border";
            // 
            // testControl1
            // 
            this.testControl1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.testControl1.BackColor = System.Drawing.Color.Transparent;
            this.testControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.testControl1.Location = new System.Drawing.Point(368, 101);
            this.testControl1.Name = "testControl1";
            this.testControl1.Size = new System.Drawing.Size(96, 96);
            this.testControl1.State = WinPaletter.UI.WP.TestControl.States.None;
            this.testControl1.TabIndex = 231;
            this.testControl1.Text = "Back color for a control with its border";
            // 
            // ApplicationThemer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(884, 360);
            this.Controls.Add(this.testControl5);
            this.Controls.Add(this.testControl4);
            this.Controls.Add(this.testControl3);
            this.Controls.Add(this.testControl2);
            this.Controls.Add(this.testControl1);
            this.Controls.Add(this.AlertBox2);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.BackColorPick);
            this.Controls.Add(this.Label29);
            this.Controls.Add(this.PictureBox46);
            this.Controls.Add(this.AccentColor);
            this.Controls.Add(this.Label28);
            this.Controls.Add(this.PictureBox45);
            this.Controls.Add(this.RoundedCorners);
            this.Controls.Add(this.PictureBox44);
            this.Controls.Add(this.appearance_dark);
            this.Controls.Add(this.PictureBox43);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.GroupBox12);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationThemer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinPaletter application theme";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ApplicationThemer_FormClosing);
            this.Load += new System.EventHandler(this.ApplicationThemer_Editor_Load);
            this.Shown += new System.EventHandler(this.ApplicationThemer_Shown);
            this.BackColorChanged += new System.EventHandler(this.ApplicationThemer_BackColorChanged);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox43)).EndInit();
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.ResumeLayout(false);

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
        public UI.WP.TestControl testControl5;
        public UI.WP.TestControl testControl4;
        public UI.WP.TestControl testControl3;
        public UI.WP.TestControl testControl2;
        public UI.WP.TestControl testControl1;
    }
}
