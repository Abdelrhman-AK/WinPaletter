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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerateFromColor));
            this.GroupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.RadioButton6 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton3 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton4 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton7 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton5 = new WinPaletter.UI.WP.RadioButton();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.Trackbar1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.SelectedColor = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ImgPaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.groupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
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
            this.GroupBox4.Controls.Add(this.RadioButton4);
            this.GroupBox4.Controls.Add(this.RadioButton7);
            this.GroupBox4.Controls.Add(this.RadioButton5);
            this.GroupBox4.Location = new System.Drawing.Point(12, 140);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(520, 118);
            this.GroupBox4.TabIndex = 169;
            this.GroupBox4.Text = "GroupBox4";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(33, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(305, 24);
            this.Label3.TabIndex = 169;
            this.Label3.Text = "Options for extracted colors brightness:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 3);
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
            this.RadioButton6.Location = new System.Drawing.Point(283, 60);
            this.RadioButton6.Name = "RadioButton6";
            this.RadioButton6.Size = new System.Drawing.Size(232, 24);
            this.RadioButton6.TabIndex = 168;
            this.RadioButton6.Text = "Make colors darker";
            this.RadioButton6.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton3
            // 
            this.RadioButton3.Checked = true;
            this.RadioButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton3.ForeColor = System.Drawing.Color.White;
            this.RadioButton3.Location = new System.Drawing.Point(45, 30);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(232, 24);
            this.RadioButton3.TabIndex = 164;
            this.RadioButton3.Text = "Don\'t change colors brightness";
            this.RadioButton3.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton4
            // 
            this.RadioButton4.Checked = false;
            this.RadioButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton4.ForeColor = System.Drawing.Color.White;
            this.RadioButton4.Location = new System.Drawing.Point(45, 90);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(232, 24);
            this.RadioButton4.TabIndex = 165;
            this.RadioButton4.Text = "Make colors extremely bright";
            this.RadioButton4.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton7
            // 
            this.RadioButton7.Checked = false;
            this.RadioButton7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton7.ForeColor = System.Drawing.Color.White;
            this.RadioButton7.Location = new System.Drawing.Point(283, 90);
            this.RadioButton7.Name = "RadioButton7";
            this.RadioButton7.Size = new System.Drawing.Size(232, 24);
            this.RadioButton7.TabIndex = 167;
            this.RadioButton7.Text = "Make colors extremely dark";
            this.RadioButton7.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton5
            // 
            this.RadioButton5.Checked = false;
            this.RadioButton5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton5.ForeColor = System.Drawing.Color.White;
            this.RadioButton5.Location = new System.Drawing.Point(45, 60);
            this.RadioButton5.Name = "RadioButton5";
            this.RadioButton5.Size = new System.Drawing.Size(232, 24);
            this.RadioButton5.TabIndex = 166;
            this.RadioButton5.Text = "Make colors brighter";
            this.RadioButton5.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox2.Controls.Add(this.Trackbar1);
            this.GroupBox2.Controls.Add(this.PictureBox7);
            this.GroupBox2.Controls.Add(this.CheckBox1);
            this.GroupBox2.Controls.Add(this.SelectedColor);
            this.GroupBox2.Controls.Add(this.PictureBox8);
            this.GroupBox2.Controls.Add(this.PictureBox2);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Location = new System.Drawing.Point(12, 12);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(520, 122);
            this.GroupBox2.TabIndex = 165;
            this.GroupBox2.Text = "GroupBox2";
            // 
            // Trackbar1
            // 
            this.Trackbar1.AnimateChanges = true;
            this.Trackbar1.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar1.DefaultValue = 13;
            this.Trackbar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Trackbar1.Location = new System.Drawing.Point(36, 65);
            this.Trackbar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Trackbar1.Maximum = 100;
            this.Trackbar1.Minimum = 13;
            this.Trackbar1.Name = "Trackbar1";
            this.Trackbar1.Size = new System.Drawing.Size(479, 24);
            this.Trackbar1.TabIndex = 170;
            this.Trackbar1.Value = 13;
            this.Trackbar1.ValueChanged += new System.EventHandler(this.trackBarX1_ValueChanged);
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(3, 94);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 156;
            this.PictureBox7.TabStop = false;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(33, 94);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(482, 24);
            this.CheckBox1.TabIndex = 155;
            this.CheckBox1.Text = "Include inverted degrees of selected color to increase variety of extracted palet" +
    "te";
            this.CheckBox1.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox1_CheckedChanged);
            // 
            // SelectedColor
            // 
            this.SelectedColor.AllowDrop = true;
            this.SelectedColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(38)))), ((int)(((byte)(53)))));
            this.SelectedColor.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(38)))), ((int)(((byte)(53)))));
            this.SelectedColor.DontShowInfo = false;
            this.SelectedColor.Location = new System.Drawing.Point(106, 3);
            this.SelectedColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectedColor.Name = "SelectedColor";
            this.SelectedColor.Size = new System.Drawing.Size(100, 24);
            this.SelectedColor.TabIndex = 154;
            this.SelectedColor.BackColorChanged += new System.EventHandler(this.SelectedColor_BackColorChanged);
            this.SelectedColor.Click += new System.EventHandler(this.SelectedColor_Click);
            this.SelectedColor.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectedColor_DragDrop);
            // 
            // PictureBox8
            // 
            this.PictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(3, 38);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 149;
            this.PictureBox8.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(3, 3);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 142;
            this.PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(33, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(66, 24);
            this.Label2.TabIndex = 141;
            this.Label2.Text = "Color:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(33, 38);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(244, 24);
            this.Label6.TabIndex = 151;
            this.Label6.Text = "Minimum number of extracted colors:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.PictureBox5);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.ImgPaletteContainer);
            this.GroupBox1.Location = new System.Drawing.Point(12, 271);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(520, 162);
            this.GroupBox1.TabIndex = 164;
            this.GroupBox1.Text = "GroupBox1";
            // 
            // PictureBox5
            // 
            this.PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(3, 3);
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
            this.Label1.Location = new System.Drawing.Point(33, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(482, 24);
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
            this.ImgPaletteContainer.Location = new System.Drawing.Point(3, 31);
            this.ImgPaletteContainer.Name = "ImgPaletteContainer";
            this.ImgPaletteContainer.Padding = new System.Windows.Forms.Padding(3);
            this.ImgPaletteContainer.Size = new System.Drawing.Size(514, 128);
            this.ImgPaletteContainer.TabIndex = 145;
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 264);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(520, 1);
            this.Separator1.TabIndex = 161;
            this.Separator1.TabStop = false;
            this.Separator1.Text = "Separator1";
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
            this.AlertBox1.Location = new System.Drawing.Point(12, 561);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(520, 29);
            this.AlertBox1.TabIndex = 159;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "You may need to readjust colors after closing to improve your theme\'s accessibili" +
    "ty";
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageAsVector = false;
            this.Button3.ImageVector = null;
            this.Button3.Location = new System.Drawing.Point(155, 597);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(100, 32);
            this.Button3.TabIndex = 158;
            this.Button3.Text = "Cancel";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageAsVector = false;
            this.Button2.ImageVector = null;
            this.Button2.Location = new System.Drawing.Point(261, 597);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(100, 32);
            this.Button2.TabIndex = 147;
            this.Button2.Text = "Done";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.CustomColorOnHover)));
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageAsVector = false;
            this.Button1.ImageVector = null;
            this.Button1.Location = new System.Drawing.Point(367, 597);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(165, 32);
            this.Button1.TabIndex = 146;
            this.Button1.Text = "Distribute randomly";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox5.Controls.Add(this.pictureBox3);
            this.groupBox5.Controls.Add(this.listBox1);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(12, 439);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(520, 113);
            this.groupBox5.TabIndex = 172;
            this.groupBox5.Text = "groupBox5";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
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
            this.listBox1.Location = new System.Drawing.Point(3, 33);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(514, 75);
            this.listBox1.TabIndex = 170;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(482, 24);
            this.label4.TabIndex = 143;
            this.label4.Text = "Trials (select an item to restore it):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PaletteGenerateFromColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(544, 641);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteGenerateFromColor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate a palette from color";
            this.Load += new System.EventHandler(this.PaletteGenerateFromImage_Load);
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal Label Label1;
        internal FlowLayoutPanel ImgPaletteContainer;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
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
        internal UI.WP.GroupBox groupBox5;
        internal PictureBox pictureBox3;
        private ListBox listBox1;
        internal Label label4;
        private UI.Controllers.TrackBarX Trackbar1;
    }
}
