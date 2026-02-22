using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class PaletteGenerator : UI.WP.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerator));
            this.groupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toggle1 = new WinPaletter.UI.WP.Toggle();
            this.panel14 = new System.Windows.Forms.Panel();
            this.smoothPanel1 = new WinPaletter.UI.WP.SmoothPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox14 = new WinPaletter.UI.WP.GroupBox();
            this.checkBox2 = new WinPaletter.UI.WP.CheckBox();
            this.radioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.tablessControl1 = new WinPaletter.UI.WP.TablessControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox19 = new WinPaletter.UI.WP.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.button5 = new WinPaletter.UI.WP.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.button7 = new WinPaletter.UI.WP.Button();
            this.button6 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioImage();
            this.Label2 = new System.Windows.Forms.Label();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ImgPaletteContainer = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.checkBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.button11 = new WinPaletter.UI.WP.Button();
            this.groupBox4.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.tablessControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox4.Controls.Add(this.button11);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.toggle1);
            this.groupBox4.Controls.Add(this.panel14);
            this.groupBox4.Controls.Add(this.pictureBox3);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(12, 182);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(486, 247);
            this.groupBox4.TabIndex = 177;
            this.groupBox4.Text = "groupBox4";
            this.groupBox4.UseDecorationPattern = false;
            this.groupBox4.UseSharpStyle = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(463, 24);
            this.label3.TabIndex = 164;
            this.label3.Text = "0";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle1
            // 
            this.toggle1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggle1.Checked = false;
            this.toggle1.DarkLight_Toggler = false;
            this.toggle1.Location = new System.Drawing.Point(435, 14);
            this.toggle1.Name = "toggle1";
            this.toggle1.Size = new System.Drawing.Size(40, 20);
            this.toggle1.TabIndex = 163;
            this.toggle1.Text = "toggle1";
            this.toggle1.CheckedChanged += new System.EventHandler(this.toggle1_CheckedChanged);
            // 
            // panel14
            // 
            this.panel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel14.Controls.Add(this.smoothPanel1);
            this.panel14.Enabled = false;
            this.panel14.Location = new System.Drawing.Point(11, 46);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(1);
            this.panel14.Size = new System.Drawing.Size(463, 165);
            this.panel14.TabIndex = 162;
            // 
            // smoothPanel1
            // 
            this.smoothPanel1.AutoScroll = true;
            this.smoothPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smoothPanel1.Location = new System.Drawing.Point(1, 1);
            this.smoothPanel1.Name = "smoothPanel1";
            this.smoothPanel1.Size = new System.Drawing.Size(461, 163);
            this.smoothPanel1.TabIndex = 0;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(11, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 160;
            this.pictureBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(244, 24);
            this.label6.TabIndex = 143;
            this.label6.Text = "Apply an effect to the generated palette:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.BackColor = System.Drawing.Color.Transparent;
            this.groupBox14.Controls.Add(this.checkBox2);
            this.groupBox14.Controls.Add(this.radioImage2);
            this.groupBox14.Controls.Add(this.tablessControl1);
            this.groupBox14.Controls.Add(this.RadioButton1);
            this.groupBox14.Controls.Add(this.Label2);
            this.groupBox14.Controls.Add(this.RadioButton2);
            this.groupBox14.Controls.Add(this.PictureBox2);
            this.groupBox14.Location = new System.Drawing.Point(12, 12);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(486, 164);
            this.groupBox14.TabIndex = 174;
            this.groupBox14.UseDecorationPattern = false;
            this.groupBox14.UseSharpStyle = false;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.Checked = false;
            this.checkBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox2.ForeColor = System.Drawing.Color.White;
            this.checkBox2.Location = new System.Drawing.Point(11, 133);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(470, 24);
            this.checkBox2.TabIndex = 142;
            this.checkBox2.Text = "Normalize colors using brightness, not color values";
            this.checkBox2.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.checkBox2_CheckedChanged);
            // 
            // radioImage2
            // 
            this.radioImage2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioImage2.Checked = true;
            this.radioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage2.ForeColor = System.Drawing.Color.White;
            this.radioImage2.Image = ((System.Drawing.Image)(resources.GetObject("radioImage2.Image")));
            this.radioImage2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage2.Location = new System.Drawing.Point(114, 5);
            this.radioImage2.Name = "radioImage2";
            this.radioImage2.Size = new System.Drawing.Size(165, 38);
            this.radioImage2.TabIndex = 144;
            this.radioImage2.Text = "Current preferences";
            this.radioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radioImage2.CheckedChanged += new System.EventHandler(this.radioImage2_CheckedChanged);
            // 
            // tablessControl1
            // 
            this.tablessControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablessControl1.Controls.Add(this.tabPage3);
            this.tablessControl1.Controls.Add(this.tabPage1);
            this.tablessControl1.Controls.Add(this.tabPage2);
            this.tablessControl1.Location = new System.Drawing.Point(11, 49);
            this.tablessControl1.Multiline = true;
            this.tablessControl1.Name = "tablessControl1";
            this.tablessControl1.SelectedIndex = 0;
            this.tablessControl1.Size = new System.Drawing.Size(470, 77);
            this.tablessControl1.TabIndex = 176;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage3.Controls.Add(this.groupBox19);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(462, 49);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "0";
            // 
            // groupBox19
            // 
            this.groupBox19.BackColor = System.Drawing.Color.Transparent;
            this.groupBox19.Controls.Add(this.label31);
            this.groupBox19.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox19.Location = new System.Drawing.Point(0, 0);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(462, 77);
            this.groupBox19.TabIndex = 176;
            this.groupBox19.UseDecorationPattern = false;
            this.groupBox19.UseSharpStyle = false;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(462, 77);
            this.label31.TabIndex = 141;
            this.label31.Text = "The palette is loaded based on the current aspect. Select a different source to c" +
    "hange the palette variation.";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(462, 49);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.flowLayoutPanel1);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 77);
            this.groupBox3.TabIndex = 176;
            this.groupBox3.UseDecorationPattern = false;
            this.groupBox3.UseSharpStyle = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(441, 20);
            this.label4.TabIndex = 147;
            this.label4.Text = "Left-click a color to edit, or right-click to remove it.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 39);
            this.flowLayoutPanel1.TabIndex = 146;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button5.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.button5.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = null;
            this.button5.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.button5.ImageGlyphEnabled = true;
            this.button5.Location = new System.Drawing.Point(418, 14);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(34, 24);
            this.button5.TabIndex = 138;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(462, 49);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.TextBox1);
            this.groupBox2.Controls.Add(this.Button4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 77);
            this.groupBox2.TabIndex = 175;
            this.groupBox2.UseDecorationPattern = false;
            this.groupBox2.UseSharpStyle = false;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.button7.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = null;
            this.button7.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.button7.ImageGlyphEnabled = true;
            this.button7.Location = new System.Drawing.Point(246, 42);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(206, 24);
            this.button7.TabIndex = 144;
            this.button7.Text = "Use the applied wallpaper";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.button6.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = null;
            this.button6.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.button6.ImageGlyphEnabled = true;
            this.button6.Location = new System.Drawing.Point(9, 42);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(229, 24);
            this.button6.TabIndex = 143;
            this.button6.Text = "Use wallpaper from current theme";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(12, 12);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(399, 24);
            this.TextBox1.TabIndex = 137;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button4.ImageGlyphEnabled = true;
            this.Button4.Location = new System.Drawing.Point(418, 12);
            this.Button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(34, 24);
            this.Button4.TabIndex = 138;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton1.Checked = false;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Image = ((System.Drawing.Image)(resources.GetObject("RadioButton1.Image")));
            this.RadioButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.Location = new System.Drawing.Point(285, 5);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(95, 38);
            this.RadioButton1.TabIndex = 139;
            this.RadioButton1.Text = "Colors";
            this.RadioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(41, 7);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(67, 34);
            this.Label2.TabIndex = 141;
            this.Label2.Text = "Sources:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Image = ((System.Drawing.Image)(resources.GetObject("RadioButton2.Image")));
            this.RadioButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.Location = new System.Drawing.Point(386, 5);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(95, 38);
            this.RadioButton2.TabIndex = 140;
            this.RadioButton2.Text = "Image";
            this.RadioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(11, 12);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 142;
            this.PictureBox2.TabStop = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.PictureBox5);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.ImgPaletteContainer);
            this.GroupBox1.Location = new System.Drawing.Point(12, 435);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(486, 133);
            this.GroupBox1.TabIndex = 164;
            this.GroupBox1.UseDecorationPattern = false;
            this.GroupBox1.UseSharpStyle = false;
            // 
            // PictureBox5
            // 
            this.PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(11, 12);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 160;
            this.PictureBox5.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(41, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(433, 24);
            this.Label1.TabIndex = 143;
            this.Label1.Text = "Generated palette (view-only):";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ImgPaletteContainer
            // 
            this.ImgPaletteContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImgPaletteContainer.AutoScroll = true;
            this.ImgPaletteContainer.Location = new System.Drawing.Point(11, 46);
            this.ImgPaletteContainer.Name = "ImgPaletteContainer";
            this.ImgPaletteContainer.Padding = new System.Windows.Forms.Padding(3);
            this.ImgPaletteContainer.Size = new System.Drawing.Size(463, 74);
            this.ImgPaletteContainer.TabIndex = 145;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.checkBox1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 577);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(510, 48);
            this.bottom_buttons.TabIndex = 172;
            this.bottom_buttons.UseDecorationPattern = false;
            this.bottom_buttons.UseSharpStyle = false;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Checked = false;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(12, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(280, 24);
            this.checkBox1.TabIndex = 159;
            this.checkBox1.Text = "Show me the generated palette";
            this.checkBox1.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.checkBox1_CheckedChanged);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(404, 8);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(100, 32);
            this.Button2.TabIndex = 147;
            this.Button2.Text = "OK";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyph = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.Location = new System.Drawing.Point(298, 8);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(100, 32);
            this.Button3.TabIndex = 158;
            this.Button3.Text = "Cancel";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.CustomColor = System.Drawing.Color.Empty;
            this.button11.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button11.Image = null;
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button11.ImageGlyph = null;
            this.button11.ImageGlyphEnabled = false;
            this.button11.Location = new System.Drawing.Point(291, 12);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(138, 24);
            this.button11.TabIndex = 231;
            this.button11.Text = "Uncheck all effects";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // PaletteGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(510, 625);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteGenerator";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Palette Generator";
            this.Load += new System.EventHandler(this.PaletteGenerateFromImage_Load);
            this.groupBox4.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.tablessControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal UI.WP.RadioImage RadioButton2;
        internal UI.WP.RadioImage RadioButton1;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox1;
        internal Label Label1;
        internal UI.WP.SmoothFlowLayoutPanel ImgPaletteContainer;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal PictureBox PictureBox5;
        internal UI.WP.GroupBox GroupBox1;
        private UI.WP.GroupBox bottom_buttons;
        private UI.WP.GroupBox groupBox14;
        private UI.WP.GroupBox groupBox2;
        private UI.WP.TablessControl tablessControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private UI.WP.GroupBox groupBox3;
        internal UI.WP.SmoothFlowLayoutPanel flowLayoutPanel1;
        internal UI.WP.Button button5;
        internal UI.WP.RadioImage radioImage2;
        internal UI.WP.Button button6;
        internal UI.WP.Button button7;
        internal Label label4;
        internal UI.WP.GroupBox groupBox4;
        internal PictureBox pictureBox3;
        internal Label label6;
        private UI.WP.Toggle toggle1;
        private TabPage tabPage3;
        private UI.WP.GroupBox groupBox19;
        internal Label label31;
        private UI.WP.CheckBox checkBox1;
        private Panel panel14;
        private UI.WP.SmoothPanel smoothPanel1;
        private UI.WP.CheckBox checkBox2;
        internal Label label3;
        internal UI.WP.Button button11;
    }
}
