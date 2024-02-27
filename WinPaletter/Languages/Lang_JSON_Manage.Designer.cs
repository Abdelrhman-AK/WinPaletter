using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_JSON_Manage : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_JSON_Manage));
            this.Label5 = new System.Windows.Forms.Label();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.TextBox3 = new WinPaletter.UI.WP.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.SeparatorVertical1 = new WinPaletter.UI.WP.SeparatorV();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.TextBox2 = new WinPaletter.UI.WP.TextBox();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.titlebarExtender1 = new WinPaletter.Tabs.TitlebarExtender();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pin_button = new WinPaletter.UI.WP.Button();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox2.SuspendLayout();
            this.bottom_buttons.SuspendLayout();
            this.titlebarExtender1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(12, 10);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(457, 27);
            this.Label5.TabIndex = 202;
            this.Label5.Text = "Numbers in curly brackets should be left unchanged, for example: {0}";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.ImageGlyphEnabled = false;
            this.Button7.ImageGlyph = null;
            this.Button7.Location = new System.Drawing.Point(534, 7);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(90, 34);
            this.Button7.TabIndex = 201;
            this.Button7.Text = "Cancel";
            this.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = ((System.Drawing.Image)(resources.GetObject("Button2.Image")));
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.ImageGlyph = null;
            this.Button2.Location = new System.Drawing.Point(741, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(110, 34);
            this.Button2.TabIndex = 200;
            this.Button2.Text = "Save as ...";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button6
            // 
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.ImageGlyphEnabled = false;
            this.Button6.ImageGlyph = null;
            this.Button6.Location = new System.Drawing.Point(601, 3);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(160, 34);
            this.Button6.TabIndex = 113;
            this.Button6.Text = "Change preview font";
            this.Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button5
            // 
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = ((System.Drawing.Image)(resources.GetObject("Button5.Image")));
            this.Button5.ImageGlyphEnabled = false;
            this.Button5.ImageGlyph = null;
            this.Button5.Location = new System.Drawing.Point(395, 3);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(200, 34);
            this.Button5.TabIndex = 112;
            this.Button5.Text = "Generate new (English) only";
            this.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button8
            // 
            this.Button8.CustomColor = System.Drawing.Color.Empty;
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.ImageGlyphEnabled = false;
            this.Button8.ImageGlyph = null;
            this.Button8.Location = new System.Drawing.Point(43, 3);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(105, 34);
            this.Button8.TabIndex = 110;
            this.Button8.Text = "Open from";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Button4
            // 
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.ImageGlyphEnabled = false;
            this.Button4.ImageGlyph = null;
            this.Button4.Location = new System.Drawing.Point(154, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(235, 34);
            this.Button4.TabIndex = 111;
            this.Button4.Text = "Generate new (English) and open It";
            this.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.Button11);
            this.GroupBox1.Controls.Add(this.Button10);
            this.GroupBox1.Controls.Add(this.Button9);
            this.GroupBox1.Controls.Add(this.PictureBox3);
            this.GroupBox1.Controls.Add(this.TextBox3);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.PictureBox2);
            this.GroupBox1.Controls.Add(this.PictureBox1);
            this.GroupBox1.Controls.Add(this.SeparatorVertical1);
            this.GroupBox1.Controls.Add(this.Button3);
            this.GroupBox1.Controls.Add(this.TextBox2);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Controls.Add(this.TextBox1);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.TreeView1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 60);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(833, 504);
            this.GroupBox1.TabIndex = 7;
            // 
            // Button11
            // 
            this.Button11.CustomColor = System.Drawing.Color.Empty;
            this.Button11.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = null;
            this.Button11.ImageGlyphEnabled = false;
            this.Button11.ImageGlyph = null;
            this.Button11.Location = new System.Drawing.Point(319, 40);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(73, 23);
            this.Button11.TabIndex = 21;
            this.Button11.Text = "Collapse all";
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button10
            // 
            this.Button10.CustomColor = System.Drawing.Color.Empty;
            this.Button10.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = null;
            this.Button10.ImageGlyphEnabled = false;
            this.Button10.ImageGlyph = null;
            this.Button10.Location = new System.Drawing.Point(247, 40);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(66, 23);
            this.Button10.TabIndex = 20;
            this.Button10.Text = "Expand all";
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click_1);
            // 
            // Button9
            // 
            this.Button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button9.CustomColor = System.Drawing.Color.Empty;
            this.Button9.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.ImageGlyphEnabled = false;
            this.Button9.ImageGlyph = null;
            this.Button9.Location = new System.Drawing.Point(567, 466);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(134, 28);
            this.Button9.TabIndex = 19;
            this.Button9.Text = "Language snippets";
            this.Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(406, 99);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 15;
            this.PictureBox3.TabStop = false;
            // 
            // TextBox3
            // 
            this.TextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox3.ForeColor = System.Drawing.Color.White;
            this.TextBox3.Location = new System.Drawing.Point(439, 129);
            this.TextBox3.MaxLength = 32767;
            this.TextBox3.Multiline = true;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.ReadOnly = true;
            this.TextBox3.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox3.SelectedText = "";
            this.TextBox3.SelectionLength = 0;
            this.TextBox3.SelectionStart = 0;
            this.TextBox3.Size = new System.Drawing.Size(387, 119);
            this.TextBox3.TabIndex = 14;
            this.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox3.UseSystemPasswordChar = false;
            this.TextBox3.WordWrap = true;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(436, 99);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(390, 24);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "Old value:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(406, 258);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 12;
            this.PictureBox2.TabStop = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(406, 40);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 11;
            this.PictureBox1.TabStop = false;
            // 
            // SeparatorVertical1
            // 
            this.SeparatorVertical1.AlternativeLook = false;
            this.SeparatorVertical1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SeparatorVertical1.BackColor = System.Drawing.Color.Transparent;
            this.SeparatorVertical1.Location = new System.Drawing.Point(398, 38);
            this.SeparatorVertical1.Name = "SeparatorVertical1";
            this.SeparatorVertical1.Size = new System.Drawing.Size(1, 456);
            this.SeparatorVertical1.TabIndex = 10;
            this.SeparatorVertical1.TabStop = false;
            this.SeparatorVertical1.Text = "SeparatorVertical1";
            // 
            // Button3
            // 
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.ImageGlyph = null;
            this.Button3.Location = new System.Drawing.Point(209, 40);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(32, 23);
            this.Button3.TabIndex = 9;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // TextBox2
            // 
            this.TextBox2.ForeColor = System.Drawing.Color.White;
            this.TextBox2.Location = new System.Drawing.Point(7, 40);
            this.TextBox2.MaxLength = 32767;
            this.TextBox2.Multiline = true;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = false;
            this.TextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox2.SelectedText = "";
            this.TextBox2.SelectionLength = 0;
            this.TextBox2.SelectionStart = 0;
            this.TextBox2.Size = new System.Drawing.Size(196, 23);
            this.TextBox2.TabIndex = 8;
            this.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox2.UseSystemPasswordChar = false;
            this.TextBox2.WordWrap = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Location = new System.Drawing.Point(7, 8);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.GroupBox2.Size = new System.Drawing.Size(819, 24);
            this.GroupBox2.TabIndex = 7;
            // 
            // Label6
            // 
            this.Label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(2, 2);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(815, 20);
            this.Label6.TabIndex = 114;
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.ImageGlyph = null;
            this.Button1.Location = new System.Drawing.Point(707, 466);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(119, 28);
            this.Button1.TabIndex = 4;
            this.Button1.Text = "Submit change";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(439, 288);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(387, 172);
            this.TextBox1.TabIndex = 3;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(436, 64);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(390, 24);
            this.Label4.TabIndex = 2;
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(436, 258);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(390, 24);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "New value:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(436, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(390, 24);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Variable:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TreeView1
            // 
            this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView1.ForeColor = System.Drawing.Color.White;
            this.TreeView1.FullRowSelect = true;
            this.TreeView1.ItemHeight = 20;
            this.TreeView1.LabelEdit = true;
            this.TreeView1.Location = new System.Drawing.Point(7, 69);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.ShowLines = false;
            this.TreeView1.Size = new System.Drawing.Size(385, 425);
            this.TreeView1.TabIndex = 6;
            this.TreeView1.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView1_BeforeLabelEdit);
            this.TreeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView1_AfterLabelEdit);
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // Button12
            // 
            this.Button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button12.CustomColor = System.Drawing.Color.Empty;
            this.Button12.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = ((System.Drawing.Image)(resources.GetObject("Button12.Image")));
            this.Button12.ImageGlyphEnabled = false;
            this.Button12.ImageGlyph = null;
            this.Button12.Location = new System.Drawing.Point(630, 7);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(105, 34);
            this.Button12.TabIndex = 212;
            this.Button12.Text = "Help";
            this.Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Label5);
            this.bottom_buttons.Controls.Add(this.Button12);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button7);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 574);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(857, 48);
            this.bottom_buttons.TabIndex = 213;
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.flowLayoutPanel1);
            this.titlebarExtender1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebarExtender1.DropDWMEffect = false;
            this.titlebarExtender1.Location = new System.Drawing.Point(0, 0);
            this.titlebarExtender1.Name = "titlebarExtender1";
            this.titlebarExtender1.Size = new System.Drawing.Size(857, 54);
            this.titlebarExtender1.TabIndex = 214;
            this.titlebarExtender1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.pin_button);
            this.flowLayoutPanel1.Controls.Add(this.Button8);
            this.flowLayoutPanel1.Controls.Add(this.Button4);
            this.flowLayoutPanel1.Controls.Add(this.Button5);
            this.flowLayoutPanel1.Controls.Add(this.Button6);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(836, 40);
            this.flowLayoutPanel1.TabIndex = 124;
            // 
            // pin_button
            // 
            this.pin_button.CustomColor = System.Drawing.Color.Empty;
            this.pin_button.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.pin_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pin_button.ForeColor = System.Drawing.Color.White;
            this.pin_button.Image = ((System.Drawing.Image)(resources.GetObject("pin_button.Image")));
            this.pin_button.ImageGlyphEnabled = false;
            this.pin_button.ImageGlyph = null;
            this.pin_button.Location = new System.Drawing.Point(3, 3);
            this.pin_button.Name = "pin_button";
            this.pin_button.Size = new System.Drawing.Size(34, 34);
            this.pin_button.TabIndex = 125;
            this.pin_button.UseVisualStyleBackColor = false;
            this.pin_button.Visible = false;
            this.pin_button.Click += new System.EventHandler(this.pin_button_Click);
            // 
            // Lang_JSON_Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(857, 622);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.titlebarExtender1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Lang_JSON_Manage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language editor";
            this.Load += new System.EventHandler(this.LangJSON_Manage_Load);
            this.ParentChanged += new System.EventHandler(this.Lang_JSON_Manage_ParentChanged);
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            this.bottom_buttons.ResumeLayout(false);
            this.titlebarExtender1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal TreeView TreeView1;
        internal UI.WP.GroupBox GroupBox1;
        internal Label Label4;
        internal Label Label2;
        internal Label Label1;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox1;
        internal Label Label6;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button2;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.Button Button3;
        internal UI.WP.SeparatorV SeparatorVertical1;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox1;
        internal PictureBox PictureBox3;
        internal UI.WP.TextBox TextBox3;
        internal Label Label3;
        internal UI.WP.Button Button9;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button10;
        internal Label Label5;
        internal UI.WP.Button Button12;
        private UI.WP.GroupBox bottom_buttons;
        public Tabs.TitlebarExtender titlebarExtender1;
        private FlowLayoutPanel flowLayoutPanel1;
        internal UI.WP.Button pin_button;
    }
}
