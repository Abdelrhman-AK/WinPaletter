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
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ImgPaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.RadioButton6 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton3 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton5 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton4 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton7 = new WinPaletter.UI.WP.RadioButton();
            this.groupBox6 = new WinPaletter.UI.WP.GroupBox();
            this.SelectedColor = new WinPaletter.UI.Controllers.ColorItem();
            this.Trackbar1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.label8 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.toggle1 = new WinPaletter.UI.WP.Toggle();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.groupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.PictureBox5);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.ImgPaletteContainer);
            this.GroupBox1.Location = new System.Drawing.Point(12, 246);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(660, 178);
            this.GroupBox1.TabIndex = 179;
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
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.pictureBox11);
            this.groupBox3.Controls.Add(this.RadioButton6);
            this.groupBox3.Controls.Add(this.RadioButton3);
            this.groupBox3.Controls.Add(this.RadioButton5);
            this.groupBox3.Controls.Add(this.RadioButton4);
            this.groupBox3.Controls.Add(this.RadioButton7);
            this.groupBox3.Location = new System.Drawing.Point(12, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 108);
            this.groupBox3.TabIndex = 178;
            this.groupBox3.Text = "groupBox3";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(41, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 24);
            this.label9.TabIndex = 169;
            this.label9.Text = "Options:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox11
            // 
            this.pictureBox11.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(11, 12);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(24, 24);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox11.TabIndex = 163;
            this.pictureBox11.TabStop = false;
            // 
            // RadioButton6
            // 
            this.RadioButton6.Checked = false;
            this.RadioButton6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton6.ForeColor = System.Drawing.Color.White;
            this.RadioButton6.Location = new System.Drawing.Point(384, 42);
            this.RadioButton6.Name = "RadioButton6";
            this.RadioButton6.Size = new System.Drawing.Size(265, 24);
            this.RadioButton6.TabIndex = 168;
            this.RadioButton6.Text = "Make colors darker";
            this.RadioButton6.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton3
            // 
            this.RadioButton3.Checked = true;
            this.RadioButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton3.ForeColor = System.Drawing.Color.White;
            this.RadioButton3.Location = new System.Drawing.Point(113, 12);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(265, 24);
            this.RadioButton3.TabIndex = 164;
            this.RadioButton3.Text = "Don\'t change colors brightness";
            this.RadioButton3.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton5
            // 
            this.RadioButton5.Checked = false;
            this.RadioButton5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton5.ForeColor = System.Drawing.Color.White;
            this.RadioButton5.Location = new System.Drawing.Point(113, 42);
            this.RadioButton5.Name = "RadioButton5";
            this.RadioButton5.Size = new System.Drawing.Size(265, 24);
            this.RadioButton5.TabIndex = 166;
            this.RadioButton5.Text = "Make colors brighter";
            this.RadioButton5.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton4
            // 
            this.RadioButton4.Checked = false;
            this.RadioButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton4.ForeColor = System.Drawing.Color.White;
            this.RadioButton4.Location = new System.Drawing.Point(113, 72);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(265, 24);
            this.RadioButton4.TabIndex = 165;
            this.RadioButton4.Text = "Make colors extremely bright";
            this.RadioButton4.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton7
            // 
            this.RadioButton7.Checked = false;
            this.RadioButton7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton7.ForeColor = System.Drawing.Color.White;
            this.RadioButton7.Location = new System.Drawing.Point(384, 72);
            this.RadioButton7.Name = "RadioButton7";
            this.RadioButton7.Size = new System.Drawing.Size(265, 24);
            this.RadioButton7.TabIndex = 167;
            this.RadioButton7.Text = "Make colors extremely dark";
            this.RadioButton7.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.SelectedColor);
            this.groupBox6.Controls.Add(this.Trackbar1);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.Label2);
            this.groupBox6.Controls.Add(this.PictureBox2);
            this.groupBox6.Controls.Add(this.toggle1);
            this.groupBox6.Controls.Add(this.Label7);
            this.groupBox6.Controls.Add(this.PictureBox7);
            this.groupBox6.Controls.Add(this.pictureBox4);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(660, 107);
            this.groupBox6.TabIndex = 177;
            // 
            // SelectedColor
            // 
            this.SelectedColor.AllowDrop = true;
            this.SelectedColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(38)))), ((int)(((byte)(53)))));
            this.SelectedColor.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(38)))), ((int)(((byte)(53)))));
            this.SelectedColor.DontShowInfo = false;
            this.SelectedColor.Location = new System.Drawing.Point(550, 12);
            this.SelectedColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectedColor.Name = "SelectedColor";
            this.SelectedColor.Size = new System.Drawing.Size(100, 24);
            this.SelectedColor.TabIndex = 154;
            this.SelectedColor.ContextMenuMadeColorChangeInvoker += new WinPaletter.UI.Controllers.ColorItem.ContextMenuMadeColorChange(this.SelectedColor_ContextMenuMadeColorChangeInvoker);
            this.SelectedColor.BackColorChanged += new System.EventHandler(this.SelectedColor_BackColorChanged);
            this.SelectedColor.Click += new System.EventHandler(this.SelectedColor_Click);
            this.SelectedColor.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectedColor_DragDrop);
            // 
            // Trackbar1
            // 
            this.Trackbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trackbar1.AnimateChanges = true;
            this.Trackbar1.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar1.DefaultValue = 13;
            this.Trackbar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Trackbar1.Location = new System.Drawing.Point(270, 42);
            this.Trackbar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Trackbar1.Maximum = 100;
            this.Trackbar1.Minimum = 13;
            this.Trackbar1.Name = "Trackbar1";
            this.Trackbar1.Size = new System.Drawing.Size(380, 24);
            this.Trackbar1.TabIndex = 170;
            this.Trackbar1.Value = 13;
            this.Trackbar1.ValueChanged += new System.EventHandler(this.trackBarX1_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(41, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(560, 24);
            this.label8.TabIndex = 174;
            this.label8.Text = "Include inverted shades of the selected color to increase the variety of the extr" +
    "acted palette";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(41, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(66, 24);
            this.Label2.TabIndex = 141;
            this.Label2.Text = "Color:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // toggle1
            // 
            this.toggle1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggle1.Checked = false;
            this.toggle1.DarkLight_Toggler = false;
            this.toggle1.Location = new System.Drawing.Point(607, 74);
            this.toggle1.Name = "toggle1";
            this.toggle1.Size = new System.Drawing.Size(40, 20);
            this.toggle1.TabIndex = 173;
            this.toggle1.CheckedChanged += new System.EventHandler(this.toggle1_CheckedChanged);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(41, 42);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(222, 24);
            this.Label7.TabIndex = 155;
            this.Label7.Text = "Minimum number of extracted colors:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(11, 72);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 156;
            this.PictureBox7.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(11, 42);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 149;
            this.pictureBox4.TabStop = false;
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 239);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(660, 1);
            this.Separator1.TabIndex = 161;
            this.Separator1.TabStop = false;
            this.Separator1.Text = "Separator1";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.AlertBox1);
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 603);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(684, 48);
            this.bottom_buttons.TabIndex = 173;
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
            this.AlertBox1.TabIndex = 160;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "Adjust colors after closing to improve accessibility";
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
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
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(407, 8);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(100, 32);
            this.Button2.TabIndex = 147;
            this.Button2.Text = "Done";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.None;
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
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox5.Controls.Add(this.pictureBox3);
            this.groupBox5.Controls.Add(this.listBox1);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(12, 430);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(660, 162);
            this.groupBox5.TabIndex = 180;
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
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
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
            // PaletteGenerateFromColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(684, 651);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.groupBox5);
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
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.RadioButton RadioButton6;
        internal UI.WP.RadioButton RadioButton7;
        internal UI.WP.RadioButton RadioButton5;
        internal UI.WP.RadioButton RadioButton4;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.Controllers.ColorItem SelectedColor;
        internal PictureBox PictureBox7;
        private UI.Controllers.TrackBarX Trackbar1;
        private UI.WP.GroupBox bottom_buttons;
        private UI.WP.GroupBox groupBox6;
        internal Label label8;
        internal UI.WP.Toggle toggle1;
        internal PictureBox pictureBox4;
        internal Label Label7;
        internal UI.WP.GroupBox groupBox3;
        internal Label label9;
        internal PictureBox pictureBox11;
        internal UI.WP.GroupBox GroupBox1;
        internal PictureBox PictureBox5;
        internal Label Label1;
        internal FlowLayoutPanel ImgPaletteContainer;
        internal UI.WP.GroupBox groupBox5;
        internal PictureBox pictureBox3;
        private ListBox listBox1;
        internal Label label4;
        internal UI.WP.AlertBox AlertBox1;
    }
}
