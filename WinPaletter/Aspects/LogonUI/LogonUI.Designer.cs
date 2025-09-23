using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUI : AspectsTemplate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI));
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.LogonUI_Lockscreen_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.LogonUI_Background_Toggle = new WinPaletter.UI.WP.Toggle();
            this.Label13 = new System.Windows.Forms.Label();
            this.PictureBox22 = new System.Windows.Forms.PictureBox();
            this.PictureBox15 = new System.Windows.Forms.PictureBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.LogonUI_Acrylic_Toggle = new WinPaletter.UI.WP.Toggle();
            this.Label18 = new System.Windows.Forms.Label();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.button7 = new WinPaletter.UI.WP.Button();
            this.tabs_preview_1 = new WinPaletter.UI.WP.TablessControl();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.button5 = new WinPaletter.UI.WP.Button();
            this.button4 = new WinPaletter.UI.WP.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new WinPaletter.UI.WP.TextBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            this.previewContainer.SuspendLayout();
            this.tabs_preview_1.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.titlebarExtender1.Size = new System.Drawing.Size(1126, 52);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.LogonUI_Lockscreen_Toggle);
            this.GroupBox3.Controls.Add(this.PictureBox6);
            this.GroupBox3.Controls.Add(this.LogonUI_Background_Toggle);
            this.GroupBox3.Controls.Add(this.Label13);
            this.GroupBox3.Controls.Add(this.PictureBox22);
            this.GroupBox3.Controls.Add(this.PictureBox15);
            this.GroupBox3.Controls.Add(this.Label20);
            this.GroupBox3.Controls.Add(this.Label16);
            this.GroupBox3.Controls.Add(this.LogonUI_Acrylic_Toggle);
            this.GroupBox3.Controls.Add(this.Label18);
            this.GroupBox3.Controls.Add(this.PictureBox16);
            this.GroupBox3.Location = new System.Drawing.Point(7, 262);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(562, 130);
            this.GroupBox3.TabIndex = 15;
            // 
            // LogonUI_Lockscreen_Toggle
            // 
            this.LogonUI_Lockscreen_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogonUI_Lockscreen_Toggle.Checked = false;
            this.LogonUI_Lockscreen_Toggle.DarkLight_Toggler = false;
            this.LogonUI_Lockscreen_Toggle.Location = new System.Drawing.Point(515, 101);
            this.LogonUI_Lockscreen_Toggle.Name = "LogonUI_Lockscreen_Toggle";
            this.LogonUI_Lockscreen_Toggle.Size = new System.Drawing.Size(40, 20);
            this.LogonUI_Lockscreen_Toggle.TabIndex = 16;
            this.LogonUI_Lockscreen_Toggle.CheckedChanged += new System.EventHandler(this.LogonUI_Lockscreen_Toggle_CheckedChanged);
            // 
            // PictureBox6
            // 
            this.PictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(3, 3);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(30, 30);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 1;
            this.PictureBox6.TabStop = false;
            // 
            // LogonUI_Background_Toggle
            // 
            this.LogonUI_Background_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogonUI_Background_Toggle.Checked = false;
            this.LogonUI_Background_Toggle.DarkLight_Toggler = false;
            this.LogonUI_Background_Toggle.Location = new System.Drawing.Point(515, 71);
            this.LogonUI_Background_Toggle.Name = "LogonUI_Background_Toggle";
            this.LogonUI_Background_Toggle.Size = new System.Drawing.Size(40, 20);
            this.LogonUI_Background_Toggle.TabIndex = 16;
            this.LogonUI_Background_Toggle.CheckedChanged += new System.EventHandler(this.LogonUI_Background_Toggle_CheckedChanged);
            // 
            // Label13
            // 
            this.Label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label13.BackColor = System.Drawing.Color.Transparent;
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(39, 3);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(520, 30);
            this.Label13.TabIndex = 0;
            this.Label13.Text = "Preferences";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox22
            // 
            this.PictureBox22.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox22.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox22.Image")));
            this.PictureBox22.Location = new System.Drawing.Point(9, 99);
            this.PictureBox22.Name = "PictureBox22";
            this.PictureBox22.Size = new System.Drawing.Size(24, 24);
            this.PictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox22.TabIndex = 4;
            this.PictureBox22.TabStop = false;
            // 
            // PictureBox15
            // 
            this.PictureBox15.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox15.Image")));
            this.PictureBox15.Location = new System.Drawing.Point(9, 39);
            this.PictureBox15.Name = "PictureBox15";
            this.PictureBox15.Size = new System.Drawing.Size(24, 24);
            this.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox15.TabIndex = 4;
            this.PictureBox15.TabStop = false;
            // 
            // Label20
            // 
            this.Label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(39, 99);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(470, 24);
            this.Label20.TabIndex = 13;
            this.Label20.Text = "Make this screen always unlocked and show user login screen only";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label16
            // 
            this.Label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(39, 39);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(470, 24);
            this.Label16.TabIndex = 13;
            this.Label16.Text = "Acrylic effect on user password and login";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LogonUI_Acrylic_Toggle
            // 
            this.LogonUI_Acrylic_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogonUI_Acrylic_Toggle.Checked = false;
            this.LogonUI_Acrylic_Toggle.DarkLight_Toggler = false;
            this.LogonUI_Acrylic_Toggle.Location = new System.Drawing.Point(515, 41);
            this.LogonUI_Acrylic_Toggle.Name = "LogonUI_Acrylic_Toggle";
            this.LogonUI_Acrylic_Toggle.Size = new System.Drawing.Size(40, 20);
            this.LogonUI_Acrylic_Toggle.TabIndex = 16;
            this.LogonUI_Acrylic_Toggle.CheckedChanged += new System.EventHandler(this.LogonUI_Acrylic_Toggle_CheckedChanged);
            // 
            // Label18
            // 
            this.Label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(39, 69);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(470, 24);
            this.Label18.TabIndex = 13;
            this.Label18.Text = "Show lock screen background";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox16
            // 
            this.PictureBox16.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox16.Image")));
            this.PictureBox16.Location = new System.Drawing.Point(9, 69);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(24, 24);
            this.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox16.TabIndex = 4;
            this.PictureBox16.TabStop = false;
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.button7);
            this.previewContainer.Controls.Add(this.tabs_preview_1);
            this.previewContainer.Location = new System.Drawing.Point(577, 58);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(536, 345);
            this.previewContainer.TabIndex = 131;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.CustomColor = System.Drawing.Color.Empty;
            this.button7.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button7.Image = null;
            this.button7.ImageGlyph = null;
            this.button7.ImageGlyphEnabled = false;
            this.button7.Location = new System.Drawing.Point(310, 308);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(222, 32);
            this.button7.TabIndex = 205;
            this.button7.Text = "Switch locked\\user login preview";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // tabs_preview_1
            // 
            this.tabs_preview_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs_preview_1.Controls.Add(this.TabPage6);
            this.tabs_preview_1.Controls.Add(this.tabPage1);
            this.tabs_preview_1.Controls.Add(this.tabPage12);
            this.tabs_preview_1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabs_preview_1.ItemSize = new System.Drawing.Size(50, 0);
            this.tabs_preview_1.Location = new System.Drawing.Point(4, 5);
            this.tabs_preview_1.Multiline = true;
            this.tabs_preview_1.Name = "tabs_preview_1";
            this.tabs_preview_1.SelectedIndex = 0;
            this.tabs_preview_1.Size = new System.Drawing.Size(528, 297);
            this.tabs_preview_1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabs_preview_1.TabIndex = 120;
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.Black;
            this.TabPage6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TabPage6.Controls.Add(this.label2);
            this.TabPage6.Controls.Add(this.label1);
            this.TabPage6.Location = new System.Drawing.Point(4, 24);
            this.TabPage6.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(520, 269);
            this.TabPage6.TabIndex = 0;
            this.TabPage6.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Display", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(445, 42);
            this.label2.TabIndex = 1;
            this.label2.Text = "0";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Small Semibol", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 84);
            this.label1.TabIndex = 0;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(520, 269);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 38F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(514, 62);
            this.label3.TabIndex = 2;
            this.label3.Text = "0";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 206);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(9, 3, 0, 0);
            this.label4.Size = new System.Drawing.Size(514, 60);
            this.label4.TabIndex = 3;
            this.label4.Text = "0";
            // 
            // tabPage12
            // 
            this.tabPage12.BackColor = System.Drawing.Color.Black;
            this.tabPage12.Controls.Add(this.pictureBox1);
            this.tabPage12.Location = new System.Drawing.Point(4, 24);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Size = new System.Drawing.Size(520, 269);
            this.tabPage12.TabIndex = 2;
            this.tabPage12.Text = "2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(520, 269);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.pictureBox3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.Button3);
            this.groupBox2.Controls.Add(this.PictureBox4);
            this.groupBox2.Controls.Add(this.Button2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.Button1);
            this.groupBox2.Location = new System.Drawing.Point(7, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(562, 198);
            this.groupBox2.TabIndex = 222;
            this.groupBox2.Text = "groupBox2";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.CustomColor = System.Drawing.Color.Empty;
            this.button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = null;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.ImageGlyph = null;
            this.button5.ImageGlyphEnabled = false;
            this.button5.Location = new System.Drawing.Point(114, 105);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(443, 25);
            this.button5.TabIndex = 165;
            this.button5.Text = "Get path of the default Lock Screen";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.CustomColor = System.Drawing.Color.Empty;
            this.button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = null;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.ImageGlyph = null;
            this.button4.ImageGlyphEnabled = false;
            this.button4.Location = new System.Drawing.Point(114, 167);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(443, 24);
            this.button4.TabIndex = 164;
            this.button4.Text = "Let Windows handles Lock Screen image";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, 7);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 163;
            this.pictureBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(43, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(490, 30);
            this.label6.TabIndex = 162;
            this.label6.Text = "Change lock screen image (machine-wide change)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button3.ImageGlyph = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.Location = new System.Drawing.Point(114, 74);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(443, 25);
            this.Button3.TabIndex = 144;
            this.Button3.Text = "Get path of the default Windows Wallpaper";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(13, 44);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 140;
            this.PictureBox4.TabStop = false;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(114, 136);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(443, 24);
            this.Button2.TabIndex = 143;
            this.Button2.Text = "Get path of current wallpaper";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(43, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 24);
            this.label7.TabIndex = 139;
            this.label7.Text = "Image:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(114, 44);
            this.textBox1.MaxLength = 32767;
            this.textBox1.Multiline = false;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = false;
            this.textBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.Size = new System.Drawing.Size(402, 24);
            this.textBox1.TabIndex = 141;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox1.UseSystemPasswordChar = false;
            this.textBox1.WordWrap = true;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button1.ImageGlyphEnabled = true;
            this.Button1.Location = new System.Drawing.Point(523, 44);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(34, 24);
            this.Button1.TabIndex = 142;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // LogonUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CanOpenColorsEffects = true;
            this.ClientSize = new System.Drawing.Size(1126, 694);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.previewContainer);
            this.Controls.Add(this.GroupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonUI";
            this.Text = "Lock screen";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.LogonUI_Load);
            this.Controls.SetChildIndex(this.GroupBox3, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
            this.Controls.SetChildIndex(this.previewContainer, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            this.previewContainer.ResumeLayout(false);
            this.tabs_preview_1.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.Toggle LogonUI_Lockscreen_Toggle;
        internal PictureBox PictureBox22;
        internal Label Label20;
        internal UI.WP.Toggle LogonUI_Background_Toggle;
        internal PictureBox PictureBox16;
        internal Label Label18;
        internal UI.WP.Toggle LogonUI_Acrylic_Toggle;
        internal PictureBox PictureBox15;
        internal Label Label16;
        internal PictureBox PictureBox6;
        internal Label Label13;
        internal UI.WP.GroupBox previewContainer;
        internal UI.WP.TablessControl tabs_preview_1;
        internal TabPage TabPage6;
        private TabPage tabPage12;
        private UI.WP.Button button7;
        private Label label2;
        private Label label1;
        private Timer timer1;
        private PictureBox pictureBox1;
        private TabPage tabPage1;
        private Label label4;
        private Label label3;
        private UI.WP.GroupBox groupBox2;
        internal PictureBox pictureBox3;
        internal Label label6;
        internal UI.WP.Button Button3;
        internal PictureBox PictureBox4;
        internal UI.WP.Button Button2;
        internal Label label7;
        internal UI.WP.TextBox textBox1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button button4;
        internal UI.WP.Button button5;
    }
}
