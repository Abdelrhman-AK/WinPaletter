using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Wallpaper_Editor : AspectsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wallpaper_Editor));
            this.source_wallpapertone = new WinPaletter.UI.WP.RadioImage();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.source_slideshow = new WinPaletter.UI.WP.RadioImage();
            this.source_color = new WinPaletter.UI.WP.RadioImage();
            this.source_pic = new WinPaletter.UI.WP.RadioImage();
            this.style_fill = new WinPaletter.UI.WP.RadioImage();
            this.style_center = new WinPaletter.UI.WP.RadioImage();
            this.style_tile = new WinPaletter.UI.WP.RadioImage();
            this.style_fit = new WinPaletter.UI.WP.RadioImage();
            this.style_stretch = new WinPaletter.UI.WP.RadioImage();
            this.AlertBox2 = new WinPaletter.UI.WP.AlertBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.color_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.LBar = new WinPaletter.UI.WP.ColorBarX();
            this.SBar = new WinPaletter.UI.WP.ColorBarX();
            this.HBar = new WinPaletter.UI.WP.ColorBarX();
            this.Button20 = new WinPaletter.UI.WP.Button();
            this.AlertBox3 = new WinPaletter.UI.WP.AlertBox();
            this.Separator2 = new WinPaletter.UI.WP.SeparatorH();
            this.Button15 = new WinPaletter.UI.WP.Button();
            this.Button16 = new WinPaletter.UI.WP.Button();
            this.Button19 = new WinPaletter.UI.WP.Button();
            this.TextBox3 = new WinPaletter.UI.WP.TextBox();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.trackBarX1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.CheckBox3 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Separator3 = new WinPaletter.UI.WP.SeparatorH();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioButton();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.Button17 = new WinPaletter.UI.WP.Button();
            this.Button18 = new WinPaletter.UI.WP.Button();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.TextBox2 = new WinPaletter.UI.WP.TextBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.pnl_preview = new System.Windows.Forms.PictureBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button14 = new WinPaletter.UI.WP.Button();
            this.Button13 = new WinPaletter.UI.WP.Button();
            this.groupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tablessControl1 = new WinPaletter.UI.WP.TablessControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.previewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_preview)).BeginInit();
            this.Panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tablessControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.titlebarExtender1.Size = new System.Drawing.Size(1169, 52);
            // 
            // source_wallpapertone
            // 
            this.source_wallpapertone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.source_wallpapertone.Checked = false;
            this.source_wallpapertone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_wallpapertone.ForeColor = System.Drawing.Color.White;
            this.source_wallpapertone.Image = ((System.Drawing.Image)(resources.GetObject("source_wallpapertone.Image")));
            this.source_wallpapertone.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_wallpapertone.Location = new System.Drawing.Point(306, 50);
            this.source_wallpapertone.Name = "source_wallpapertone";
            this.source_wallpapertone.Size = new System.Drawing.Size(110, 80);
            this.source_wallpapertone.TabIndex = 161;
            this.source_wallpapertone.Text = "Wallpaper Tone";
            this.source_wallpapertone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_wallpapertone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.source_wallpapertone.CheckedChanged += new System.EventHandler(this.source_wallpapertone_CheckedChanged);
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(622, 371);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(534, 104);
            this.AlertBox1.TabIndex = 160;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = resources.GetString("AlertBox1.Text");
            // 
            // source_slideshow
            // 
            this.source_slideshow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.source_slideshow.Checked = false;
            this.source_slideshow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_slideshow.ForeColor = System.Drawing.Color.White;
            this.source_slideshow.Image = ((System.Drawing.Image)(resources.GetObject("source_slideshow.Image")));
            this.source_slideshow.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_slideshow.Location = new System.Drawing.Point(422, 50);
            this.source_slideshow.Name = "source_slideshow";
            this.source_slideshow.Size = new System.Drawing.Size(110, 80);
            this.source_slideshow.TabIndex = 39;
            this.source_slideshow.Text = "Slideshow";
            this.source_slideshow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_slideshow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.source_slideshow.CheckedChanged += new System.EventHandler(this.Source_slideshow_CheckedChanged);
            // 
            // source_color
            // 
            this.source_color.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.source_color.Checked = false;
            this.source_color.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_color.ForeColor = System.Drawing.Color.White;
            this.source_color.Image = ((System.Drawing.Image)(resources.GetObject("source_color.Image")));
            this.source_color.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_color.Location = new System.Drawing.Point(190, 50);
            this.source_color.Name = "source_color";
            this.source_color.Size = new System.Drawing.Size(110, 80);
            this.source_color.TabIndex = 37;
            this.source_color.Text = "Solid Color";
            this.source_color.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_color.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.source_color.CheckedChanged += new System.EventHandler(this.source_color_CheckedChanged);
            // 
            // source_pic
            // 
            this.source_pic.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.source_pic.Checked = false;
            this.source_pic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_pic.ForeColor = System.Drawing.Color.White;
            this.source_pic.Image = ((System.Drawing.Image)(resources.GetObject("source_pic.Image")));
            this.source_pic.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_pic.Location = new System.Drawing.Point(74, 50);
            this.source_pic.Name = "source_pic";
            this.source_pic.Size = new System.Drawing.Size(110, 80);
            this.source_pic.TabIndex = 35;
            this.source_pic.Text = "Picture";
            this.source_pic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_pic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.source_pic.CheckedChanged += new System.EventHandler(this.source_pic_CheckedChanged);
            // 
            // style_fill
            // 
            this.style_fill.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.style_fill.Checked = true;
            this.style_fill.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_fill.ForeColor = System.Drawing.Color.White;
            this.style_fill.Image = ((System.Drawing.Image)(resources.GetObject("style_fill.Image")));
            this.style_fill.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_fill.Location = new System.Drawing.Point(17, 50);
            this.style_fill.Name = "style_fill";
            this.style_fill.Size = new System.Drawing.Size(110, 80);
            this.style_fill.TabIndex = 154;
            this.style_fill.Text = "Fill";
            this.style_fill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_fill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.style_fill.CheckedChanged += new System.EventHandler(this.Style_fill_CheckedChanged);
            // 
            // style_center
            // 
            this.style_center.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.style_center.Checked = false;
            this.style_center.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_center.ForeColor = System.Drawing.Color.White;
            this.style_center.Image = ((System.Drawing.Image)(resources.GetObject("style_center.Image")));
            this.style_center.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_center.Location = new System.Drawing.Point(365, 50);
            this.style_center.Name = "style_center";
            this.style_center.Size = new System.Drawing.Size(110, 80);
            this.style_center.TabIndex = 150;
            this.style_center.Text = "Center";
            this.style_center.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_center.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.style_center.CheckedChanged += new System.EventHandler(this.Style_fill_CheckedChanged);
            // 
            // style_tile
            // 
            this.style_tile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.style_tile.Checked = false;
            this.style_tile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_tile.ForeColor = System.Drawing.Color.White;
            this.style_tile.Image = ((System.Drawing.Image)(resources.GetObject("style_tile.Image")));
            this.style_tile.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_tile.Location = new System.Drawing.Point(481, 50);
            this.style_tile.Name = "style_tile";
            this.style_tile.Size = new System.Drawing.Size(110, 80);
            this.style_tile.TabIndex = 148;
            this.style_tile.Text = "Tile";
            this.style_tile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_tile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.style_tile.CheckedChanged += new System.EventHandler(this.Style_fill_CheckedChanged);
            // 
            // style_fit
            // 
            this.style_fit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.style_fit.Checked = false;
            this.style_fit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_fit.ForeColor = System.Drawing.Color.White;
            this.style_fit.Image = ((System.Drawing.Image)(resources.GetObject("style_fit.Image")));
            this.style_fit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_fit.Location = new System.Drawing.Point(133, 50);
            this.style_fit.Name = "style_fit";
            this.style_fit.Size = new System.Drawing.Size(110, 80);
            this.style_fit.TabIndex = 156;
            this.style_fit.Text = "Fit";
            this.style_fit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_fit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.style_fit.CheckedChanged += new System.EventHandler(this.Style_fill_CheckedChanged);
            // 
            // style_stretch
            // 
            this.style_stretch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.style_stretch.Checked = false;
            this.style_stretch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_stretch.ForeColor = System.Drawing.Color.White;
            this.style_stretch.Image = ((System.Drawing.Image)(resources.GetObject("style_stretch.Image")));
            this.style_stretch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_stretch.Location = new System.Drawing.Point(249, 50);
            this.style_stretch.Name = "style_stretch";
            this.style_stretch.Size = new System.Drawing.Size(110, 80);
            this.style_stretch.TabIndex = 152;
            this.style_stretch.Text = "Stretch";
            this.style_stretch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_stretch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.style_stretch.CheckedChanged += new System.EventHandler(this.Style_fill_CheckedChanged);
            // 
            // AlertBox2
            // 
            this.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox2.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox2.CenterText = false;
            this.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox2.Image = null;
            this.AlertBox2.Location = new System.Drawing.Point(13, 77);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(578, 28);
            this.AlertBox2.TabIndex = 161;
            this.AlertBox2.TabStop = false;
            this.AlertBox2.Text = "This color is shared with Classic Colors (Window Objects > Background color)";
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button3.ImageGlyph = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.Location = new System.Drawing.Point(323, 74);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(272, 25);
            this.Button3.TabIndex = 144;
            this.Button3.Text = "Get path of the default Windows Wallpaper";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(323, 105);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(272, 24);
            this.Button2.TabIndex = 143;
            this.Button2.Text = "Get path of current wallpaper";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Label15
            // 
            this.Label15.Location = new System.Drawing.Point(43, 44);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(65, 25);
            this.Label15.TabIndex = 140;
            this.Label15.Text = "Color:";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.Button1.Location = new System.Drawing.Point(561, 44);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(34, 24);
            this.Button1.TabIndex = 142;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(114, 44);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(440, 24);
            this.TextBox1.TabIndex = 141;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(13, 44);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 94;
            this.PictureBox3.TabStop = false;
            // 
            // color_pick
            // 
            this.color_pick.AllowDrop = true;
            this.color_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.color_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.color_pick.DefaultBackColor = System.Drawing.Color.Black;
            this.color_pick.DontShowInfo = false;
            this.color_pick.Location = new System.Drawing.Point(494, 44);
            this.color_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.color_pick.Name = "color_pick";
            this.color_pick.Size = new System.Drawing.Size(97, 25);
            this.color_pick.TabIndex = 93;
            this.color_pick.ContextMenuMadeColorChangeInvoker += new WinPaletter.UI.Controllers.ColorItem.ContextMenuMadeColorChange(this.color_pick_ContextMenuMadeColorChangeInvoker);
            this.color_pick.Click += new System.EventHandler(this.Color_pick_Click);
            this.color_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.Color_pick_DragDrop);
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
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(43, 44);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 24);
            this.Label4.TabIndex = 139;
            this.Label4.Text = "Image:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LBar
            // 
            this.LBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.LBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBar.AnimateChanges = false;
            this.LBar.BackColor = System.Drawing.Color.Transparent;
            this.LBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBar.Location = new System.Drawing.Point(168, 205);
            this.LBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.LBar.Mode = WinPaletter.UI.WP.ColorBar.ModesList.Light;
            this.LBar.Name = "LBar";
            this.LBar.Size = new System.Drawing.Size(427, 24);
            this.LBar.TabIndex = 214;
            this.LBar.Value = 0;
            this.LBar.ValueChanged += new System.EventHandler(this.colorBarX3_ValueChanged);
            // 
            // SBar
            // 
            this.SBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.SBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SBar.AnimateChanges = false;
            this.SBar.BackColor = System.Drawing.Color.Transparent;
            this.SBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SBar.Location = new System.Drawing.Point(168, 175);
            this.SBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SBar.Mode = WinPaletter.UI.WP.ColorBar.ModesList.Saturation;
            this.SBar.Name = "SBar";
            this.SBar.Size = new System.Drawing.Size(427, 24);
            this.SBar.TabIndex = 213;
            this.SBar.Value = 0;
            this.SBar.ValueChanged += new System.EventHandler(this.colorBarX2_ValueChanged);
            // 
            // HBar
            // 
            this.HBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.HBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HBar.AnimateChanges = false;
            this.HBar.BackColor = System.Drawing.Color.Transparent;
            this.HBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HBar.Location = new System.Drawing.Point(168, 145);
            this.HBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.HBar.Mode = WinPaletter.UI.WP.ColorBar.ModesList.Hue;
            this.HBar.Name = "HBar";
            this.HBar.Size = new System.Drawing.Size(427, 24);
            this.HBar.TabIndex = 212;
            this.HBar.Value = 0;
            this.HBar.ValueChanged += new System.EventHandler(this.colorBarX1_ValueChanged);
            // 
            // Button20
            // 
            this.Button20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button20.CustomColor = System.Drawing.Color.Empty;
            this.Button20.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button20.ForeColor = System.Drawing.Color.White;
            this.Button20.Image = ((System.Drawing.Image)(resources.GetObject("Button20.Image")));
            this.Button20.ImageGlyph = null;
            this.Button20.ImageGlyphEnabled = false;
            this.Button20.Location = new System.Drawing.Point(117, 74);
            this.Button20.Name = "Button20";
            this.Button20.Size = new System.Drawing.Size(200, 56);
            this.Button20.TabIndex = 208;
            this.Button20.Text = "Save modified wallpaper as ...";
            this.Button20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button20.UseVisualStyleBackColor = false;
            this.Button20.Click += new System.EventHandler(this.Button20_Click);
            // 
            // AlertBox3
            // 
            this.AlertBox3.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox3.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox3.CenterText = true;
            this.AlertBox3.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox3.Image = null;
            this.AlertBox3.Location = new System.Drawing.Point(13, 238);
            this.AlertBox3.Name = "AlertBox3";
            this.AlertBox3.Size = new System.Drawing.Size(582, 43);
            this.AlertBox3.TabIndex = 207;
            this.AlertBox3.TabStop = false;
            this.AlertBox3.Text = null;
            // 
            // Separator2
            // 
            this.Separator2.AlternativeLook = false;
            this.Separator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator2.BackColor = System.Drawing.Color.Transparent;
            this.Separator2.Location = new System.Drawing.Point(13, 138);
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(582, 1);
            this.Separator2.TabIndex = 158;
            this.Separator2.TabStop = false;
            // 
            // Button15
            // 
            this.Button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button15.CustomColor = System.Drawing.Color.Empty;
            this.Button15.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button15.ForeColor = System.Drawing.Color.White;
            this.Button15.Image = null;
            this.Button15.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button15.ImageGlyph = null;
            this.Button15.ImageGlyphEnabled = false;
            this.Button15.Location = new System.Drawing.Point(323, 74);
            this.Button15.Name = "Button15";
            this.Button15.Size = new System.Drawing.Size(272, 25);
            this.Button15.TabIndex = 157;
            this.Button15.Text = "Get path of the default Windows Wallpaper";
            this.Button15.UseVisualStyleBackColor = false;
            this.Button15.Click += new System.EventHandler(this.Button15_Click);
            // 
            // Button16
            // 
            this.Button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button16.CustomColor = System.Drawing.Color.Empty;
            this.Button16.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button16.ForeColor = System.Drawing.Color.White;
            this.Button16.Image = null;
            this.Button16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button16.ImageGlyph = null;
            this.Button16.ImageGlyphEnabled = false;
            this.Button16.Location = new System.Drawing.Point(323, 105);
            this.Button16.Name = "Button16";
            this.Button16.Size = new System.Drawing.Size(272, 25);
            this.Button16.TabIndex = 156;
            this.Button16.Text = "Get path of current wallpaper";
            this.Button16.UseVisualStyleBackColor = false;
            this.Button16.Click += new System.EventHandler(this.Button16_Click);
            // 
            // Button19
            // 
            this.Button19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button19.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button19.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button19.ForeColor = System.Drawing.Color.White;
            this.Button19.Image = null;
            this.Button19.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button19.ImageGlyphEnabled = true;
            this.Button19.Location = new System.Drawing.Point(561, 44);
            this.Button19.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button19.Name = "Button19";
            this.Button19.Size = new System.Drawing.Size(34, 24);
            this.Button19.TabIndex = 155;
            this.Button19.UseVisualStyleBackColor = false;
            this.Button19.Click += new System.EventHandler(this.Button19_Click);
            // 
            // TextBox3
            // 
            this.TextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox3.ForeColor = System.Drawing.Color.White;
            this.TextBox3.Location = new System.Drawing.Point(114, 44);
            this.TextBox3.MaxLength = 32767;
            this.TextBox3.Multiline = false;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.ReadOnly = false;
            this.TextBox3.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox3.SelectedText = "";
            this.TextBox3.SelectionLength = 0;
            this.TextBox3.SelectionStart = 0;
            this.TextBox3.Size = new System.Drawing.Size(440, 24);
            this.TextBox3.TabIndex = 154;
            this.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox3.UseSystemPasswordChar = false;
            this.TextBox3.WordWrap = true;
            this.TextBox3.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
            // 
            // PictureBox8
            // 
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(13, 44);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 153;
            this.PictureBox8.TabStop = false;
            // 
            // Label16
            // 
            this.Label16.Location = new System.Drawing.Point(43, 44);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(65, 24);
            this.Label16.TabIndex = 152;
            this.Label16.Text = "Image:";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(13, 173);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 148;
            this.PictureBox9.TabStop = false;
            // 
            // Label17
            // 
            this.Label17.Location = new System.Drawing.Point(43, 175);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(118, 24);
            this.Label17.TabIndex = 147;
            this.Label17.Text = "Saturation:";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox10
            // 
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(13, 203);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(24, 24);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 146;
            this.PictureBox10.TabStop = false;
            // 
            // Label18
            // 
            this.Label18.Location = new System.Drawing.Point(43, 205);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(118, 24);
            this.Label18.TabIndex = 145;
            this.Label18.Text = "Lightness:";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(13, 143);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(24, 24);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 144;
            this.PictureBox11.TabStop = false;
            // 
            // Label20
            // 
            this.Label20.Location = new System.Drawing.Point(43, 145);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(118, 24);
            this.Label20.TabIndex = 143;
            this.Label20.Text = "Hue:";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBarX1
            // 
            this.trackBarX1.AnimateChanges = true;
            this.trackBarX1.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX1.DefaultValue = 10000;
            this.trackBarX1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX1.Location = new System.Drawing.Point(217, 233);
            this.trackBarX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX1.Maximum = 36000000;
            this.trackBarX1.Minimum = 10000;
            this.trackBarX1.Name = "trackBarX1";
            this.trackBarX1.Size = new System.Drawing.Size(370, 24);
            this.trackBarX1.TabIndex = 186;
            this.trackBarX1.Value = 10000;
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = null;
            this.Button6.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Down;
            this.Button6.ImageGlyphEnabled = true;
            this.Button6.Location = new System.Drawing.Point(561, 130);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(34, 24);
            this.Button6.TabIndex = 185;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Up;
            this.Button5.ImageGlyphEnabled = true;
            this.Button5.Location = new System.Drawing.Point(561, 102);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(34, 24);
            this.Button5.TabIndex = 184;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Label13
            // 
            this.Label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(307, 188);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(288, 24);
            this.Label13.TabIndex = 183;
            this.Label13.Text = "Images in this list must be from the same folder";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckBox3
            // 
            this.CheckBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox3.Checked = false;
            this.CheckBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox3.ForeColor = System.Drawing.Color.White;
            this.CheckBox3.Location = new System.Drawing.Point(43, 263);
            this.CheckBox3.Name = "CheckBox3";
            this.CheckBox3.Size = new System.Drawing.Size(119, 24);
            this.CheckBox3.TabIndex = 182;
            this.CheckBox3.Text = "Shuffle images";
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(13, 263);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 181;
            this.PictureBox7.TabStop = false;
            // 
            // PictureBox16
            // 
            this.PictureBox16.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox16.Image")));
            this.PictureBox16.Location = new System.Drawing.Point(13, 233);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(24, 24);
            this.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox16.TabIndex = 180;
            this.PictureBox16.TabStop = false;
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(43, 233);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(167, 24);
            this.Label11.TabIndex = 176;
            this.Label11.Text = "Change every ... milliseconds:";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Separator3
            // 
            this.Separator3.AlternativeLook = false;
            this.Separator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator3.BackColor = System.Drawing.Color.Transparent;
            this.Separator3.Location = new System.Drawing.Point(13, 220);
            this.Separator3.Name = "Separator3";
            this.Separator3.Size = new System.Drawing.Size(574, 1);
            this.Separator3.TabIndex = 175;
            this.Separator3.TabStop = false;
            // 
            // RadioButton2
            // 
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Location = new System.Drawing.Point(43, 74);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(119, 24);
            this.RadioButton2.TabIndex = 174;
            this.RadioButton2.Text = "List of images:";
            this.RadioButton2.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton1_CheckedChanged);
            // 
            // RadioButton1
            // 
            this.RadioButton1.Checked = false;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Location = new System.Drawing.Point(43, 44);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(119, 24);
            this.RadioButton1.TabIndex = 173;
            this.RadioButton1.Text = "Folder:";
            this.RadioButton1.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton1_CheckedChanged);
            // 
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(13, 74);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(24, 24);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 172;
            this.PictureBox6.TabStop = false;
            // 
            // Button17
            // 
            this.Button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button17.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.Button17.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button17.ForeColor = System.Drawing.Color.White;
            this.Button17.Image = null;
            this.Button17.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Remove;
            this.Button17.ImageGlyphEnabled = true;
            this.Button17.Location = new System.Drawing.Point(561, 158);
            this.Button17.Name = "Button17";
            this.Button17.Size = new System.Drawing.Size(34, 24);
            this.Button17.TabIndex = 170;
            this.Button17.UseVisualStyleBackColor = false;
            this.Button17.Click += new System.EventHandler(this.Button17_Click);
            // 
            // Button18
            // 
            this.Button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button18.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(202)))), ((int)(((byte)(228)))));
            this.Button18.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button18.ForeColor = System.Drawing.Color.White;
            this.Button18.Image = null;
            this.Button18.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.Button18.ImageGlyphEnabled = true;
            this.Button18.Location = new System.Drawing.Point(561, 74);
            this.Button18.Name = "Button18";
            this.Button18.Size = new System.Drawing.Size(34, 24);
            this.Button18.TabIndex = 169;
            this.Button18.UseVisualStyleBackColor = false;
            this.Button18.Click += new System.EventHandler(this.Button18_Click);
            // 
            // ListBox1
            // 
            this.ListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListBox1.ForeColor = System.Drawing.Color.White;
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 15;
            this.ListBox1.Location = new System.Drawing.Point(168, 74);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ListBox1.Size = new System.Drawing.Size(386, 107);
            this.ListBox1.TabIndex = 168;
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
            this.Button4.Location = new System.Drawing.Point(561, 44);
            this.Button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(34, 24);
            this.Button4.TabIndex = 167;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // TextBox2
            // 
            this.TextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox2.ForeColor = System.Drawing.Color.White;
            this.TextBox2.Location = new System.Drawing.Point(168, 44);
            this.TextBox2.MaxLength = 32767;
            this.TextBox2.Multiline = false;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = false;
            this.TextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox2.SelectedText = "";
            this.TextBox2.SelectionLength = 0;
            this.TextBox2.SelectionStart = 0;
            this.TextBox2.Size = new System.Drawing.Size(386, 24);
            this.TextBox2.TabIndex = 166;
            this.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox2.UseSystemPasswordChar = false;
            this.TextBox2.WordWrap = true;
            this.TextBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(13, 44);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 165;
            this.PictureBox5.TabStop = false;
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.pnl_preview);
            this.previewContainer.Location = new System.Drawing.Point(622, 62);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(534, 303);
            this.previewContainer.TabIndex = 215;
            // 
            // pnl_preview
            // 
            this.pnl_preview.Location = new System.Drawing.Point(3, 3);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 217;
            this.pnl_preview.TabStop = false;
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Button14);
            this.Panel1.Controls.Add(this.Button13);
            this.Panel1.Location = new System.Drawing.Point(883, 482);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(273, 32);
            this.Panel1.TabIndex = 218;
            this.Panel1.Visible = false;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Location = new System.Drawing.Point(3, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(189, 24);
            this.Label3.TabIndex = 177;
            this.Label3.Text = "0/0";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.Button14.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = null;
            this.Button14.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Back;
            this.Button14.ImageGlyphEnabled = true;
            this.Button14.Location = new System.Drawing.Point(198, 3);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(34, 26);
            this.Button14.TabIndex = 171;
            this.Button14.UseVisualStyleBackColor = false;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.Button13.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button13.ForeColor = System.Drawing.Color.White;
            this.Button13.Image = null;
            this.Button13.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Forward;
            this.Button13.ImageGlyphEnabled = true;
            this.Button13.Location = new System.Drawing.Point(236, 3);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(34, 26);
            this.Button13.TabIndex = 170;
            this.Button13.UseVisualStyleBackColor = false;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.pictureBox12);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.source_slideshow);
            this.groupBox1.Controls.Add(this.source_wallpapertone);
            this.groupBox1.Controls.Add(this.source_color);
            this.groupBox1.Controls.Add(this.source_pic);
            this.groupBox1.Location = new System.Drawing.Point(7, 206);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 138);
            this.groupBox1.TabIndex = 219;
            this.groupBox1.Text = "groupBox1";
            // 
            // pictureBox12
            // 
            this.pictureBox12.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(7, 7);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(30, 30);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox12.TabIndex = 163;
            this.pictureBox12.TabStop = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(43, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(536, 30);
            this.label12.TabIndex = 162;
            this.label12.Text = "Wallpaper source";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.style_center);
            this.groupBox2.Controls.Add(this.style_tile);
            this.groupBox2.Controls.Add(this.style_fill);
            this.groupBox2.Controls.Add(this.pictureBox13);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.style_fit);
            this.groupBox2.Controls.Add(this.style_stretch);
            this.groupBox2.Location = new System.Drawing.Point(7, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 138);
            this.groupBox2.TabIndex = 220;
            this.groupBox2.Text = "groupBox2";
            // 
            // pictureBox13
            // 
            this.pictureBox13.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox13.Image")));
            this.pictureBox13.Location = new System.Drawing.Point(7, 7);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(30, 30);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox13.TabIndex = 163;
            this.pictureBox13.TabStop = false;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(43, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(536, 30);
            this.label19.TabIndex = 162;
            this.label19.Text = "Wallpaper style";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.Button3);
            this.groupBox3.Controls.Add(this.PictureBox4);
            this.groupBox3.Controls.Add(this.Button2);
            this.groupBox3.Controls.Add(this.Label4);
            this.groupBox3.Controls.Add(this.TextBox1);
            this.groupBox3.Controls.Add(this.Button1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(600, 140);
            this.groupBox3.TabIndex = 221;
            this.groupBox3.Text = "groupBox3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 163;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(528, 30);
            this.label1.TabIndex = 162;
            this.label1.Text = "Options of \'Picture\' source";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tablessControl1
            // 
            this.tablessControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablessControl1.Controls.Add(this.tabPage1);
            this.tablessControl1.Controls.Add(this.tabPage4);
            this.tablessControl1.Controls.Add(this.tabPage6);
            this.tablessControl1.Controls.Add(this.tabPage7);
            this.tablessControl1.Location = new System.Drawing.Point(7, 350);
            this.tablessControl1.Name = "tablessControl1";
            this.tablessControl1.SelectedIndex = 0;
            this.tablessControl1.Size = new System.Drawing.Size(608, 357);
            this.tablessControl1.TabIndex = 222;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(600, 329);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "0";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(600, 329);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "1";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.AlertBox2);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.Label15);
            this.groupBox4.Controls.Add(this.PictureBox3);
            this.groupBox4.Controls.Add(this.color_pick);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(600, 115);
            this.groupBox4.TabIndex = 222;
            this.groupBox4.Text = "groupBox4";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(7, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 163;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(528, 30);
            this.label2.TabIndex = 162;
            this.label2.Text = "Options of \'Solid Color\' source";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage6.Controls.Add(this.groupBox5);
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(600, 329);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "2";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.LBar);
            this.groupBox5.Controls.Add(this.pictureBox14);
            this.groupBox5.Controls.Add(this.SBar);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.HBar);
            this.groupBox5.Controls.Add(this.PictureBox8);
            this.groupBox5.Controls.Add(this.Button20);
            this.groupBox5.Controls.Add(this.Label20);
            this.groupBox5.Controls.Add(this.AlertBox3);
            this.groupBox5.Controls.Add(this.PictureBox11);
            this.groupBox5.Controls.Add(this.Separator2);
            this.groupBox5.Controls.Add(this.Label18);
            this.groupBox5.Controls.Add(this.Button15);
            this.groupBox5.Controls.Add(this.PictureBox10);
            this.groupBox5.Controls.Add(this.Button16);
            this.groupBox5.Controls.Add(this.Label17);
            this.groupBox5.Controls.Add(this.Button19);
            this.groupBox5.Controls.Add(this.PictureBox9);
            this.groupBox5.Controls.Add(this.TextBox3);
            this.groupBox5.Controls.Add(this.Label16);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(600, 290);
            this.groupBox5.TabIndex = 223;
            this.groupBox5.Text = "groupBox5";
            // 
            // pictureBox14
            // 
            this.pictureBox14.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.Location = new System.Drawing.Point(7, 7);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(30, 30);
            this.pictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox14.TabIndex = 163;
            this.pictureBox14.TabStop = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(43, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(528, 30);
            this.label5.TabIndex = 162;
            this.label5.Text = "WinPaletter Wallpaper Tone";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage7.Controls.Add(this.groupBox6);
            this.tabPage7.Location = new System.Drawing.Point(4, 24);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(600, 329);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "3";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.trackBarX1);
            this.groupBox6.Controls.Add(this.pictureBox15);
            this.groupBox6.Controls.Add(this.Button6);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.Button5);
            this.groupBox6.Controls.Add(this.PictureBox5);
            this.groupBox6.Controls.Add(this.Label13);
            this.groupBox6.Controls.Add(this.TextBox2);
            this.groupBox6.Controls.Add(this.CheckBox3);
            this.groupBox6.Controls.Add(this.Button4);
            this.groupBox6.Controls.Add(this.PictureBox7);
            this.groupBox6.Controls.Add(this.ListBox1);
            this.groupBox6.Controls.Add(this.PictureBox16);
            this.groupBox6.Controls.Add(this.Button18);
            this.groupBox6.Controls.Add(this.Label11);
            this.groupBox6.Controls.Add(this.Button17);
            this.groupBox6.Controls.Add(this.Separator3);
            this.groupBox6.Controls.Add(this.PictureBox6);
            this.groupBox6.Controls.Add(this.RadioButton2);
            this.groupBox6.Controls.Add(this.RadioButton1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(600, 295);
            this.groupBox6.TabIndex = 223;
            this.groupBox6.Text = "groupBox6";
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(7, 7);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(30, 30);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox15.TabIndex = 163;
            this.pictureBox15.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(43, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(528, 30);
            this.label6.TabIndex = 162;
            this.label6.Text = "Options of \'Slideshow\' source";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Wallpaper_Editor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1169, 761);
            this.Controls.Add(this.tablessControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.previewContainer);
            this.Controls.Add(this.AlertBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Wallpaper_Editor";
            this.Text = "Wallpaper";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Wallpaper_Editor_Load);
            this.Controls.SetChildIndex(this.AlertBox1, 0);
            this.Controls.SetChildIndex(this.previewContainer, 0);
            this.Controls.SetChildIndex(this.Panel1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.tablessControl1, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.previewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_preview)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tablessControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.ResumeLayout(false);

        }
        internal UI.WP.RadioImage source_slideshow;
        internal UI.WP.RadioImage source_color;
        internal UI.WP.RadioImage source_pic;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox1;
        internal PictureBox PictureBox4;
        internal Label Label4;
        internal UI.WP.RadioImage style_fit;
        internal UI.WP.RadioImage style_fill;
        internal UI.WP.RadioImage style_stretch;
        internal UI.WP.RadioImage style_center;
        internal UI.WP.RadioImage style_tile;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox2;
        internal PictureBox PictureBox5;
        internal ListBox ListBox1;
        internal PictureBox PictureBox6;
        internal UI.WP.Button Button17;
        internal UI.WP.Button Button18;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton1;
        internal Label Label11;
        internal UI.WP.SeparatorH Separator3;
        internal PictureBox PictureBox16;
        internal UI.WP.CheckBox CheckBox3;
        internal PictureBox PictureBox7;
        internal Label Label13;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button5;
        internal UI.WP.GroupBox previewContainer;
        internal PictureBox pnl_preview;
        internal Panel Panel1;
        internal UI.WP.Button Button14;
        internal UI.WP.Button Button13;
        internal Label Label3;
        internal UI.WP.AlertBox AlertBox1;
        internal Label Label15;
        internal PictureBox PictureBox3;
        internal UI.Controllers.ColorItem color_pick;
        internal UI.WP.AlertBox AlertBox2;
        internal UI.WP.RadioImage source_wallpapertone;
        internal UI.WP.SeparatorH Separator2;
        internal UI.WP.Button Button15;
        internal UI.WP.Button Button16;
        internal UI.WP.Button Button19;
        internal UI.WP.TextBox TextBox3;
        internal PictureBox PictureBox8;
        internal Label Label16;
        internal PictureBox PictureBox9;
        internal Label Label17;
        internal PictureBox PictureBox10;
        internal Label Label18;
        internal PictureBox PictureBox11;
        internal Label Label20;
        internal UI.WP.AlertBox AlertBox3;
        internal UI.WP.Button Button20;
        private UI.Controllers.TrackBarX trackBarX1;
        private UI.WP.ColorBarX SBar;
        private UI.WP.ColorBarX HBar;
        internal UI.WP.ColorBarX LBar;
        private UI.WP.GroupBox groupBox1;
        internal PictureBox pictureBox12;
        internal Label label12;
        private UI.WP.GroupBox groupBox2;
        internal PictureBox pictureBox13;
        internal Label label19;
        private UI.WP.GroupBox groupBox3;
        internal PictureBox pictureBox1;
        internal Label label1;
        private UI.WP.TablessControl tablessControl1;
        private TabPage tabPage1;
        private TabPage tabPage4;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private UI.WP.GroupBox groupBox4;
        internal PictureBox pictureBox2;
        internal Label label2;
        private UI.WP.GroupBox groupBox5;
        internal PictureBox pictureBox14;
        internal Label label5;
        private UI.WP.GroupBox groupBox6;
        internal PictureBox pictureBox15;
        internal Label label6;
    }
}
