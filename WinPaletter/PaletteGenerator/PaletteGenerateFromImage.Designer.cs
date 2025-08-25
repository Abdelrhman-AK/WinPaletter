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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerateFromImage));
            this.groupBox6 = new WinPaletter.UI.WP.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ignoreWhiteColorsToggle = new WinPaletter.UI.WP.Toggle();
            this.Trackbar2 = new WinPaletter.UI.Controllers.TrackBarX();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.Trackbar1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.groupBox14 = new WinPaletter.UI.WP.GroupBox();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioImage();
            this.Label2 = new System.Windows.Forms.Label();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.GroupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.RadioButton6 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton3 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton5 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton7 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton4 = new WinPaletter.UI.WP.RadioButton();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ImgPaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.groupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.ignoreWhiteColorsToggle);
            this.groupBox6.Controls.Add(this.Trackbar2);
            this.groupBox6.Controls.Add(this.PictureBox8);
            this.groupBox6.Controls.Add(this.Trackbar1);
            this.groupBox6.Controls.Add(this.PictureBox9);
            this.groupBox6.Controls.Add(this.Label7);
            this.groupBox6.Controls.Add(this.PictureBox7);
            this.groupBox6.Controls.Add(this.Label6);
            this.groupBox6.Controls.Add(this.pictureBox4);
            this.groupBox6.Enabled = false;
            this.groupBox6.Location = new System.Drawing.Point(12, 120);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(660, 107);
            this.groupBox6.TabIndex = 176;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(41, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 24);
            this.label8.TabIndex = 174;
            this.label8.Text = "Ignore white colors";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ignoreWhiteColorsToggle
            // 
            this.ignoreWhiteColorsToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ignoreWhiteColorsToggle.Checked = false;
            this.ignoreWhiteColorsToggle.DarkLight_Toggler = false;
            this.ignoreWhiteColorsToggle.Location = new System.Drawing.Point(607, 74);
            this.ignoreWhiteColorsToggle.Name = "ignoreWhiteColorsToggle";
            this.ignoreWhiteColorsToggle.Size = new System.Drawing.Size(40, 20);
            this.ignoreWhiteColorsToggle.TabIndex = 173;
            this.ignoreWhiteColorsToggle.CheckedChanged += new System.EventHandler(this.ignoreWhiteColorsToggle_CheckedChanged);
            // 
            // Trackbar2
            // 
            this.Trackbar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trackbar2.AnimateChanges = true;
            this.Trackbar2.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar2.DefaultValue = 10;
            this.Trackbar2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Trackbar2.Location = new System.Drawing.Point(158, 42);
            this.Trackbar2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Trackbar2.Maximum = 100;
            this.Trackbar2.Minimum = 0;
            this.Trackbar2.Name = "Trackbar2";
            this.Trackbar2.Size = new System.Drawing.Size(492, 24);
            this.Trackbar2.TabIndex = 172;
            this.Trackbar2.Value = 10;
            this.Trackbar2.ValueChanged += new System.EventHandler(this.trackBarX2_ValueChanged);
            // 
            // PictureBox8
            // 
            this.PictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(11, 12);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 149;
            this.PictureBox8.TabStop = false;
            // 
            // Trackbar1
            // 
            this.Trackbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trackbar1.AnimateChanges = true;
            this.Trackbar1.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar1.DefaultValue = 13;
            this.Trackbar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Trackbar1.Location = new System.Drawing.Point(158, 12);
            this.Trackbar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Trackbar1.Maximum = 100;
            this.Trackbar1.Minimum = 13;
            this.Trackbar1.Name = "Trackbar1";
            this.Trackbar1.Size = new System.Drawing.Size(492, 24);
            this.Trackbar1.TabIndex = 171;
            this.Trackbar1.Value = 13;
            this.Trackbar1.ValueChanged += new System.EventHandler(this.trackBarX1_ValueChanged);
            // 
            // PictureBox9
            // 
            this.PictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(11, 42);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 154;
            this.PictureBox9.TabStop = false;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(41, 42);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(110, 24);
            this.Label7.TabIndex = 155;
            this.Label7.Text = "Quality";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            this.PictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(11, 72);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 150;
            this.PictureBox7.TabStop = false;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(41, 12);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(110, 24);
            this.Label6.TabIndex = 151;
            this.Label6.Text = "Maximum colors";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(11, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 142;
            this.pictureBox4.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TextBox1);
            this.groupBox2.Controls.Add(this.Button4);
            this.groupBox2.Controls.Add(this.pictureBox6);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 48);
            this.groupBox2.TabIndex = 175;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 34);
            this.label5.TabIndex = 141;
            this.label5.Text = "Image file:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(145, 12);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(464, 24);
            this.TextBox1.TabIndex = 137;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Button4
            // 
            this.Button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Button4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button4.ImageGlyphEnabled = true;
            this.Button4.Location = new System.Drawing.Point(616, 12);
            this.Button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(34, 24);
            this.Button4.TabIndex = 138;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(11, 12);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(24, 24);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox6.TabIndex = 142;
            this.pictureBox6.TabStop = false;
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.BackColor = System.Drawing.Color.Transparent;
            this.groupBox14.Controls.Add(this.RadioButton1);
            this.groupBox14.Controls.Add(this.Label2);
            this.groupBox14.Controls.Add(this.RadioButton2);
            this.groupBox14.Controls.Add(this.PictureBox2);
            this.groupBox14.Location = new System.Drawing.Point(12, 12);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(660, 48);
            this.groupBox14.TabIndex = 174;
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RadioButton1.Checked = false;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Image = ((System.Drawing.Image)(resources.GetObject("RadioButton1.Image")));
            this.RadioButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.Location = new System.Drawing.Point(373, 5);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(167, 38);
            this.RadioButton1.TabIndex = 139;
            this.RadioButton1.Text = "Current wallpaper";
            this.RadioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(41, 7);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(326, 34);
            this.Label2.TabIndex = 141;
            this.Label2.Text = "Select a source first:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Image = ((System.Drawing.Image)(resources.GetObject("RadioButton2.Image")));
            this.RadioButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.Location = new System.Drawing.Point(546, 5);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(110, 38);
            this.RadioButton2.TabIndex = 140;
            this.RadioButton2.Text = "Image";
            this.RadioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // PictureBox2
            // 
            this.PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(11, 12);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 142;
            this.PictureBox2.TabStop = false;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox4.Controls.Add(this.Label3);
            this.GroupBox4.Controls.Add(this.PictureBox1);
            this.GroupBox4.Controls.Add(this.RadioButton6);
            this.GroupBox4.Controls.Add(this.RadioButton3);
            this.GroupBox4.Controls.Add(this.RadioButton5);
            this.GroupBox4.Controls.Add(this.RadioButton7);
            this.GroupBox4.Controls.Add(this.RadioButton4);
            this.GroupBox4.Enabled = false;
            this.GroupBox4.Location = new System.Drawing.Point(12, 233);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(660, 108);
            this.GroupBox4.TabIndex = 169;
            this.GroupBox4.Text = "GroupBox4";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(41, 12);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(70, 24);
            this.Label3.TabIndex = 169;
            this.Label3.Text = "Options:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(11, 12);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 163;
            this.PictureBox1.TabStop = false;
            // 
            // RadioButton6
            // 
            this.RadioButton6.Checked = false;
            this.RadioButton6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton6.ForeColor = System.Drawing.Color.White;
            this.RadioButton6.Location = new System.Drawing.Point(363, 42);
            this.RadioButton6.Name = "RadioButton6";
            this.RadioButton6.Size = new System.Drawing.Size(240, 24);
            this.RadioButton6.TabIndex = 168;
            this.RadioButton6.Text = "Make colors darker";
            this.RadioButton6.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton3
            // 
            this.RadioButton3.Checked = true;
            this.RadioButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton3.ForeColor = System.Drawing.Color.White;
            this.RadioButton3.Location = new System.Drawing.Point(117, 12);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(240, 24);
            this.RadioButton3.TabIndex = 164;
            this.RadioButton3.Text = "Don\'t change colors brightness";
            this.RadioButton3.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton5
            // 
            this.RadioButton5.Checked = false;
            this.RadioButton5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton5.ForeColor = System.Drawing.Color.White;
            this.RadioButton5.Location = new System.Drawing.Point(117, 42);
            this.RadioButton5.Name = "RadioButton5";
            this.RadioButton5.Size = new System.Drawing.Size(240, 24);
            this.RadioButton5.TabIndex = 166;
            this.RadioButton5.Text = "Make colors brighter";
            this.RadioButton5.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton7
            // 
            this.RadioButton7.Checked = false;
            this.RadioButton7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton7.ForeColor = System.Drawing.Color.White;
            this.RadioButton7.Location = new System.Drawing.Point(363, 72);
            this.RadioButton7.Name = "RadioButton7";
            this.RadioButton7.Size = new System.Drawing.Size(240, 24);
            this.RadioButton7.TabIndex = 167;
            this.RadioButton7.Text = "Make colors extremely dark";
            this.RadioButton7.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton4
            // 
            this.RadioButton4.Checked = false;
            this.RadioButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton4.ForeColor = System.Drawing.Color.White;
            this.RadioButton4.Location = new System.Drawing.Point(117, 72);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(240, 24);
            this.RadioButton4.TabIndex = 165;
            this.RadioButton4.Text = "Make colors extremely bright";
            this.RadioButton4.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.PictureBox5);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.ImgPaletteContainer);
            this.GroupBox1.Enabled = false;
            this.GroupBox1.Location = new System.Drawing.Point(12, 354);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(660, 178);
            this.GroupBox1.TabIndex = 164;
            this.GroupBox1.Text = "GroupBox1";
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
            this.Label1.Size = new System.Drawing.Size(601, 24);
            this.Label1.TabIndex = 143;
            this.Label1.Text = "Extracted palette:";
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
            this.ImgPaletteContainer.Size = new System.Drawing.Size(637, 119);
            this.ImgPaletteContainer.TabIndex = 145;
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Enabled = false;
            this.Separator1.Location = new System.Drawing.Point(12, 347);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(660, 1);
            this.Separator1.TabIndex = 161;
            this.Separator1.TabStop = false;
            this.Separator1.Text = "Separator1";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Controls.Add(this.AlertBox1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 709);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(684, 48);
            this.bottom_buttons.TabIndex = 172;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Enabled = false;
            this.Button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.CustomColorOnHover)));
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(513, 8);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(165, 32);
            this.Button1.TabIndex = 146;
            this.Button1.Text = "Distribute randomly";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Enabled = false;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(407, 8);
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
            this.Button3.Location = new System.Drawing.Point(301, 8);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(100, 32);
            this.Button3.TabIndex = 158;
            this.Button3.Text = "Cancel";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = true;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Enabled = false;
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(8, 8);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(287, 32);
            this.AlertBox1.TabIndex = 159;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "Adjust colors after closing to improve accessibility";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox5.Controls.Add(this.pictureBox3);
            this.groupBox5.Controls.Add(this.listBox1);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(12, 538);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(660, 162);
            this.groupBox5.TabIndex = 171;
            this.groupBox5.Text = "groupBox5";
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
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(11, 46);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(637, 105);
            this.listBox1.TabIndex = 170;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(41, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(604, 24);
            this.label4.TabIndex = 143;
            this.label4.Text = "Trials (select an item to restore it):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PaletteGenerateFromImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(684, 757);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.groupBox5);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteGenerateFromImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate a palette from image";
            this.Load += new System.EventHandler(this.PaletteGenerateFromImage_Load);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.groupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal UI.WP.RadioImage RadioButton2;
        internal UI.WP.RadioImage RadioButton1;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox1;
        internal Label Label1;
        internal FlowLayoutPanel ImgPaletteContainer;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal PictureBox PictureBox8;
        internal PictureBox PictureBox7;
        internal Label Label6;
        internal Label Label7;
        internal PictureBox PictureBox9;
        internal UI.WP.Button Button3;
        internal UI.WP.AlertBox AlertBox1;
        internal PictureBox PictureBox5;
        internal UI.WP.SeparatorH Separator1;
        internal PictureBox PictureBox1;
        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.RadioButton RadioButton6;
        internal UI.WP.RadioButton RadioButton7;
        internal UI.WP.RadioButton RadioButton5;
        internal UI.WP.RadioButton RadioButton4;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.WP.GroupBox GroupBox4;
        internal Label Label3;
        private ListBox listBox1;
        internal UI.WP.GroupBox groupBox5;
        internal PictureBox pictureBox3;
        internal Label label4;
        private UI.Controllers.TrackBarX Trackbar2;
        private UI.Controllers.TrackBarX Trackbar1;
        private UI.WP.GroupBox bottom_buttons;
        private UI.WP.GroupBox groupBox14;
        private UI.WP.GroupBox groupBox2;
        internal Label label5;
        internal PictureBox pictureBox6;
        private UI.WP.GroupBox groupBox6;
        internal PictureBox pictureBox4;
        internal Label label8;
        internal UI.WP.Toggle ignoreWhiteColorsToggle;
    }
}
