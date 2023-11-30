using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Wallpaper_Editor : Form
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
            this.OpenImgDlg = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.WallpaperEnabled = new WinPaletter.UI.WP.Toggle();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.TabControl1 = new WinPaletter.UI.WP.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Label21 = new System.Windows.Forms.Label();
            this.source_wallpapertone = new WinPaletter.UI.WP.RadioImage();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.source_slideshow = new WinPaletter.UI.WP.RadioImage();
            this.Label1 = new System.Windows.Forms.Label();
            this.source_color = new WinPaletter.UI.WP.RadioImage();
            this.Label24 = new System.Windows.Forms.Label();
            this.source_pic = new WinPaletter.UI.WP.RadioImage();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.style_fill = new WinPaletter.UI.WP.RadioImage();
            this.style_center = new WinPaletter.UI.WP.RadioImage();
            this.Label10 = new System.Windows.Forms.Label();
            this.style_tile = new WinPaletter.UI.WP.RadioImage();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.style_fit = new WinPaletter.UI.WP.RadioImage();
            this.style_stretch = new WinPaletter.UI.WP.RadioImage();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Separator4 = new WinPaletter.UI.WP.SeparatorH();
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
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Button21 = new WinPaletter.UI.WP.Button();
            this.Button22 = new WinPaletter.UI.WP.Button();
            this.Button23 = new WinPaletter.UI.WP.Button();
            this.Button20 = new WinPaletter.UI.WP.Button();
            this.AlertBox3 = new WinPaletter.UI.WP.AlertBox();
            this.Separator2 = new WinPaletter.UI.WP.SeparatorH();
            this.Button15 = new WinPaletter.UI.WP.Button();
            this.Button16 = new WinPaletter.UI.WP.Button();
            this.Button19 = new WinPaletter.UI.WP.Button();
            this.TextBox3 = new WinPaletter.UI.WP.TextBox();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.LB = new WinPaletter.UI.WP.Button();
            this.SB = new WinPaletter.UI.WP.Button();
            this.HB = new WinPaletter.UI.WP.Button();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.LBar = new WinPaletter.UI.WP.ColorBar();
            this.SBar = new WinPaletter.UI.WP.ColorBar();
            this.HBar = new WinPaletter.UI.WP.ColorBar();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.CheckBox3 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.MD = new WinPaletter.UI.WP.Button();
            this.Trackbar1 = new WinPaletter.UI.WP.Trackbar();
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
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button14 = new WinPaletter.UI.WP.Button();
            this.Button13 = new WinPaletter.UI.WP.Button();
            this.pnl_preview = new System.Windows.Forms.PictureBox();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.SaveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.Panel2.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.previewContainer.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenImgDlg
            // 
            this.OpenImgDlg.Filter = "Images (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif|All Files (*.*)|*.*";
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
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
            this.GroupBox12.Controls.Add(this.WallpaperEnabled);
            this.GroupBox12.Controls.Add(this.checker_img);
            this.GroupBox12.Location = new System.Drawing.Point(12, 12);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(1230, 39);
            this.GroupBox12.TabIndex = 201;
            // 
            // Button9
            // 
            this.Button9.CustomColor = System.Drawing.Color.Empty;
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
            // WallpaperEnabled
            // 
            this.WallpaperEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WallpaperEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.WallpaperEnabled.Checked = false;
            this.WallpaperEnabled.DarkLight_Toggler = false;
            this.WallpaperEnabled.Location = new System.Drawing.Point(1184, 9);
            this.WallpaperEnabled.Name = "WallpaperEnabled";
            this.WallpaperEnabled.Size = new System.Drawing.Size(40, 20);
            this.WallpaperEnabled.TabIndex = 85;
            this.WallpaperEnabled.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.WallpaperEnabled_CheckedChanged);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(1143, 4);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button10.CustomColor = System.Drawing.Color.Empty;
            this.Button10.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = ((System.Drawing.Image)(resources.GetObject("Button10.Image")));
            this.Button10.Location = new System.Drawing.Point(941, 411);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(115, 34);
            this.Button10.TabIndex = 213;
            this.Button10.Text = "Quick apply";
            this.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.Location = new System.Drawing.Point(855, 411);
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
            this.Button8.Location = new System.Drawing.Point(1062, 411);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(180, 34);
            this.Button8.TabIndex = 211;
            this.Button8.Text = "Load into current theme";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // TabControl1
            // 
            this.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabControl1.AllowDrop = true;
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage5);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TabControl1.ItemSize = new System.Drawing.Size(30, 140);
            this.TabControl1.Location = new System.Drawing.Point(12, 57);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(692, 348);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.TabIndex = 214;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage1.Controls.Add(this.Label21);
            this.TabPage1.Controls.Add(this.source_wallpapertone);
            this.TabPage1.Controls.Add(this.AlertBox1);
            this.TabPage1.Controls.Add(this.Separator1);
            this.TabPage1.Controls.Add(this.PictureBox2);
            this.TabPage1.Controls.Add(this.Label14);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.PictureBox1);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.source_slideshow);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.source_color);
            this.TabPage1.Controls.Add(this.Label24);
            this.TabPage1.Controls.Add(this.source_pic);
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Location = new System.Drawing.Point(144, 4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(544, 340);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Source & style";
            // 
            // Label21
            // 
            this.Label21.AutoEllipsis = true;
            this.Label21.BackColor = System.Drawing.Color.Transparent;
            this.Label21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label21.Location = new System.Drawing.Point(196, 79);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(80, 27);
            this.Label21.TabIndex = 162;
            this.Label21.Text = "Tone";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // source_wallpapertone
            // 
            this.source_wallpapertone.Checked = false;
            this.source_wallpapertone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_wallpapertone.ForeColor = System.Drawing.Color.White;
            this.source_wallpapertone.Image = ((System.Drawing.Image)(resources.GetObject("source_wallpapertone.Image")));
            this.source_wallpapertone.Location = new System.Drawing.Point(196, 9);
            this.source_wallpapertone.Name = "source_wallpapertone";
            this.source_wallpapertone.Size = new System.Drawing.Size(80, 64);
            this.source_wallpapertone.TabIndex = 161;
            this.source_wallpapertone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_wallpapertone.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Source_pic_CheckedChanged);
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
            this.AlertBox1.Location = new System.Drawing.Point(6, 258);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(532, 76);
            this.AlertBox1.TabIndex = 160;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = resources.GetString("AlertBox1.Text");
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(6, 112);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(532, 1);
            this.Separator1.TabIndex = 158;
            this.Separator1.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(6, 149);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 149;
            this.PictureBox2.TabStop = false;
            // 
            // Label14
            // 
            this.Label14.Location = new System.Drawing.Point(36, 149);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(65, 24);
            this.Label14.TabIndex = 148;
            this.Label14.Text = "Style:";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.AutoEllipsis = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label2.Location = new System.Drawing.Point(368, 79);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(80, 27);
            this.Label2.TabIndex = 40;
            this.Label2.Text = "Slideshow";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(6, 29);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 147;
            this.PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(36, 29);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(65, 24);
            this.Label5.TabIndex = 146;
            this.Label5.Text = "Source:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // source_slideshow
            // 
            this.source_slideshow.Checked = false;
            this.source_slideshow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_slideshow.ForeColor = System.Drawing.Color.White;
            this.source_slideshow.Image = ((System.Drawing.Image)(resources.GetObject("source_slideshow.Image")));
            this.source_slideshow.Location = new System.Drawing.Point(368, 9);
            this.source_slideshow.Name = "source_slideshow";
            this.source_slideshow.Size = new System.Drawing.Size(80, 64);
            this.source_slideshow.TabIndex = 39;
            this.source_slideshow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_slideshow.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Source_slideshow_CheckedChanged);
            // 
            // Label1
            // 
            this.Label1.AutoEllipsis = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label1.Location = new System.Drawing.Point(282, 79);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(80, 27);
            this.Label1.TabIndex = 38;
            this.Label1.Text = "Color";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // source_color
            // 
            this.source_color.Checked = false;
            this.source_color.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_color.ForeColor = System.Drawing.Color.White;
            this.source_color.Image = ((System.Drawing.Image)(resources.GetObject("source_color.Image")));
            this.source_color.Location = new System.Drawing.Point(282, 9);
            this.source_color.Name = "source_color";
            this.source_color.Size = new System.Drawing.Size(80, 64);
            this.source_color.TabIndex = 37;
            this.source_color.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_color.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Source_pic_CheckedChanged);
            // 
            // Label24
            // 
            this.Label24.AutoEllipsis = true;
            this.Label24.BackColor = System.Drawing.Color.Transparent;
            this.Label24.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label24.Location = new System.Drawing.Point(110, 79);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(80, 27);
            this.Label24.TabIndex = 36;
            this.Label24.Text = "Picture";
            this.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // source_pic
            // 
            this.source_pic.Checked = false;
            this.source_pic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.source_pic.ForeColor = System.Drawing.Color.White;
            this.source_pic.Image = ((System.Drawing.Image)(resources.GetObject("source_pic.Image")));
            this.source_pic.Location = new System.Drawing.Point(110, 9);
            this.source_pic.Name = "source_pic";
            this.source_pic.Size = new System.Drawing.Size(80, 64);
            this.source_pic.TabIndex = 35;
            this.source_pic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.source_pic.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Source_pic_CheckedChanged);
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.Controls.Add(this.style_fill);
            this.Panel2.Controls.Add(this.style_center);
            this.Panel2.Controls.Add(this.Label10);
            this.Panel2.Controls.Add(this.style_tile);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.style_fit);
            this.Panel2.Controls.Add(this.style_stretch);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Controls.Add(this.Label9);
            this.Panel2.Location = new System.Drawing.Point(107, 119);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(431, 108);
            this.Panel2.TabIndex = 159;
            // 
            // style_fill
            // 
            this.style_fill.Checked = true;
            this.style_fill.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_fill.ForeColor = System.Drawing.Color.White;
            this.style_fill.Image = ((System.Drawing.Image)(resources.GetObject("style_fill.Image")));
            this.style_fill.Location = new System.Drawing.Point(3, 3);
            this.style_fill.Name = "style_fill";
            this.style_fill.Size = new System.Drawing.Size(80, 64);
            this.style_fill.TabIndex = 154;
            this.style_fill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_fill.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Style_fill_CheckedChanged);
            // 
            // style_center
            // 
            this.style_center.Checked = false;
            this.style_center.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_center.ForeColor = System.Drawing.Color.White;
            this.style_center.Image = ((System.Drawing.Image)(resources.GetObject("style_center.Image")));
            this.style_center.Location = new System.Drawing.Point(261, 3);
            this.style_center.Name = "style_center";
            this.style_center.Size = new System.Drawing.Size(80, 64);
            this.style_center.TabIndex = 150;
            this.style_center.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_center.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Style_fill_CheckedChanged);
            // 
            // Label10
            // 
            this.Label10.AutoEllipsis = true;
            this.Label10.BackColor = System.Drawing.Color.Transparent;
            this.Label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label10.Location = new System.Drawing.Point(89, 73);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(80, 27);
            this.Label10.TabIndex = 157;
            this.Label10.Text = "Fit";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // style_tile
            // 
            this.style_tile.Checked = false;
            this.style_tile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_tile.ForeColor = System.Drawing.Color.White;
            this.style_tile.Image = ((System.Drawing.Image)(resources.GetObject("style_tile.Image")));
            this.style_tile.Location = new System.Drawing.Point(347, 3);
            this.style_tile.Name = "style_tile";
            this.style_tile.Size = new System.Drawing.Size(80, 64);
            this.style_tile.TabIndex = 148;
            this.style_tile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_tile.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Style_fill_CheckedChanged);
            // 
            // Label6
            // 
            this.Label6.AutoEllipsis = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label6.Location = new System.Drawing.Point(347, 73);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(80, 27);
            this.Label6.TabIndex = 149;
            this.Label6.Text = "Tile";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label7
            // 
            this.Label7.AutoEllipsis = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label7.Location = new System.Drawing.Point(261, 73);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(80, 27);
            this.Label7.TabIndex = 151;
            this.Label7.Text = "Centered";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // style_fit
            // 
            this.style_fit.Checked = false;
            this.style_fit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_fit.ForeColor = System.Drawing.Color.White;
            this.style_fit.Image = ((System.Drawing.Image)(resources.GetObject("style_fit.Image")));
            this.style_fit.Location = new System.Drawing.Point(89, 3);
            this.style_fit.Name = "style_fit";
            this.style_fit.Size = new System.Drawing.Size(80, 64);
            this.style_fit.TabIndex = 156;
            this.style_fit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_fit.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Style_fill_CheckedChanged);
            // 
            // style_stretch
            // 
            this.style_stretch.Checked = false;
            this.style_stretch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.style_stretch.ForeColor = System.Drawing.Color.White;
            this.style_stretch.Image = ((System.Drawing.Image)(resources.GetObject("style_stretch.Image")));
            this.style_stretch.Location = new System.Drawing.Point(175, 3);
            this.style_stretch.Name = "style_stretch";
            this.style_stretch.Size = new System.Drawing.Size(80, 64);
            this.style_stretch.TabIndex = 152;
            this.style_stretch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.style_stretch.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Style_fill_CheckedChanged);
            // 
            // Label8
            // 
            this.Label8.AutoEllipsis = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label8.Location = new System.Drawing.Point(175, 73);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(80, 27);
            this.Label8.TabIndex = 153;
            this.Label8.Text = "Stretch";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label9
            // 
            this.Label9.AutoEllipsis = true;
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label9.Location = new System.Drawing.Point(3, 73);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(80, 27);
            this.Label9.TabIndex = 155;
            this.Label9.Text = "Fill";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage2.Controls.Add(this.Separator4);
            this.TabPage2.Controls.Add(this.AlertBox2);
            this.TabPage2.Controls.Add(this.Button3);
            this.TabPage2.Controls.Add(this.Button2);
            this.TabPage2.Controls.Add(this.Label15);
            this.TabPage2.Controls.Add(this.Button1);
            this.TabPage2.Controls.Add(this.TextBox1);
            this.TabPage2.Controls.Add(this.PictureBox3);
            this.TabPage2.Controls.Add(this.color_pick);
            this.TabPage2.Controls.Add(this.PictureBox4);
            this.TabPage2.Controls.Add(this.Label4);
            this.TabPage2.Location = new System.Drawing.Point(144, 4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(544, 340);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Wallpaper";
            // 
            // Separator4
            // 
            this.Separator4.AlternativeLook = false;
            this.Separator4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator4.BackColor = System.Drawing.Color.Transparent;
            this.Separator4.Location = new System.Drawing.Point(6, 66);
            this.Separator4.Name = "Separator4";
            this.Separator4.Size = new System.Drawing.Size(531, 1);
            this.Separator4.TabIndex = 162;
            this.Separator4.TabStop = false;
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
            this.AlertBox2.Location = new System.Drawing.Point(6, 106);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(531, 22);
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
            this.Button3.Location = new System.Drawing.Point(452, 36);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(85, 24);
            this.Button3.TabIndex = 144;
            this.Button3.Text = "Get default";
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
            this.Button2.Location = new System.Drawing.Point(361, 36);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(85, 24);
            this.Button2.TabIndex = 143;
            this.Button2.Text = "Get current";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Label15
            // 
            this.Label15.Location = new System.Drawing.Point(36, 73);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(65, 25);
            this.Label15.TabIndex = 140;
            this.Label15.Text = "Color:";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(503, 6);
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
            this.TextBox1.Location = new System.Drawing.Point(107, 6);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(389, 24);
            this.TextBox1.TabIndex = 141;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(6, 73);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 94;
            this.PictureBox3.TabStop = false;
            // 
            // color_pick
            // 
            this.color_pick.AllowDrop = true;
            this.color_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.color_pick.DefaultBackColor = System.Drawing.Color.Black;
            this.color_pick.DontShowInfo = false;
            this.color_pick.Location = new System.Drawing.Point(107, 73);
            this.color_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.color_pick.Name = "color_pick";
            this.color_pick.Size = new System.Drawing.Size(97, 25);
            this.color_pick.TabIndex = 93;
            this.color_pick.Click += new System.EventHandler(this.Color_pick_Click);
            this.color_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.Color_pick_DragDrop);
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(6, 6);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 140;
            this.PictureBox4.TabStop = false;
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(36, 6);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 24);
            this.Label4.TabIndex = 139;
            this.Label4.Text = "Image:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage5.Controls.Add(this.Button21);
            this.TabPage5.Controls.Add(this.Button22);
            this.TabPage5.Controls.Add(this.Button23);
            this.TabPage5.Controls.Add(this.Button20);
            this.TabPage5.Controls.Add(this.AlertBox3);
            this.TabPage5.Controls.Add(this.Separator2);
            this.TabPage5.Controls.Add(this.Button15);
            this.TabPage5.Controls.Add(this.Button16);
            this.TabPage5.Controls.Add(this.Button19);
            this.TabPage5.Controls.Add(this.TextBox3);
            this.TabPage5.Controls.Add(this.PictureBox8);
            this.TabPage5.Controls.Add(this.Label16);
            this.TabPage5.Controls.Add(this.LB);
            this.TabPage5.Controls.Add(this.SB);
            this.TabPage5.Controls.Add(this.HB);
            this.TabPage5.Controls.Add(this.PictureBox9);
            this.TabPage5.Controls.Add(this.Label17);
            this.TabPage5.Controls.Add(this.PictureBox10);
            this.TabPage5.Controls.Add(this.Label18);
            this.TabPage5.Controls.Add(this.PictureBox11);
            this.TabPage5.Controls.Add(this.Label20);
            this.TabPage5.Controls.Add(this.LBar);
            this.TabPage5.Controls.Add(this.SBar);
            this.TabPage5.Controls.Add(this.HBar);
            this.TabPage5.Location = new System.Drawing.Point(144, 4);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(544, 340);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Wallpaper Tone";
            // 
            // Button21
            // 
            this.Button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button21.CustomColor = System.Drawing.Color.Empty;
            this.Button21.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button21.ForeColor = System.Drawing.Color.White;
            this.Button21.Image = ((System.Drawing.Image)(resources.GetObject("Button21.Image")));
            this.Button21.Location = new System.Drawing.Point(502, 133);
            this.Button21.Name = "Button21";
            this.Button21.Size = new System.Drawing.Size(34, 24);
            this.Button21.TabIndex = 211;
            this.Button21.UseVisualStyleBackColor = false;
            this.Button21.Click += new System.EventHandler(this.Button21_Click);
            // 
            // Button22
            // 
            this.Button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button22.CustomColor = System.Drawing.Color.Empty;
            this.Button22.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button22.ForeColor = System.Drawing.Color.White;
            this.Button22.Image = ((System.Drawing.Image)(resources.GetObject("Button22.Image")));
            this.Button22.Location = new System.Drawing.Point(502, 103);
            this.Button22.Name = "Button22";
            this.Button22.Size = new System.Drawing.Size(34, 24);
            this.Button22.TabIndex = 210;
            this.Button22.UseVisualStyleBackColor = false;
            this.Button22.Click += new System.EventHandler(this.Button22_Click);
            // 
            // Button23
            // 
            this.Button23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button23.CustomColor = System.Drawing.Color.Empty;
            this.Button23.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button23.ForeColor = System.Drawing.Color.White;
            this.Button23.Image = ((System.Drawing.Image)(resources.GetObject("Button23.Image")));
            this.Button23.Location = new System.Drawing.Point(502, 73);
            this.Button23.Name = "Button23";
            this.Button23.Size = new System.Drawing.Size(34, 24);
            this.Button23.TabIndex = 209;
            this.Button23.UseVisualStyleBackColor = false;
            this.Button23.Click += new System.EventHandler(this.Button23_Click);
            // 
            // Button20
            // 
            this.Button20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button20.CustomColor = System.Drawing.Color.Empty;
            this.Button20.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button20.ForeColor = System.Drawing.Color.White;
            this.Button20.Image = ((System.Drawing.Image)(resources.GetObject("Button20.Image")));
            this.Button20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button20.Location = new System.Drawing.Point(151, 36);
            this.Button20.Name = "Button20";
            this.Button20.Size = new System.Drawing.Size(204, 24);
            this.Button20.TabIndex = 208;
            this.Button20.Text = "Save modified wallpaper as ...";
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
            this.AlertBox3.Location = new System.Drawing.Point(6, 163);
            this.AlertBox3.Name = "AlertBox3";
            this.AlertBox3.Size = new System.Drawing.Size(532, 43);
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
            this.Separator2.Location = new System.Drawing.Point(6, 66);
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(530, 1);
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
            this.Button15.Location = new System.Drawing.Point(452, 36);
            this.Button15.Name = "Button15";
            this.Button15.Size = new System.Drawing.Size(85, 24);
            this.Button15.TabIndex = 157;
            this.Button15.Text = "Get default";
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
            this.Button16.Location = new System.Drawing.Point(361, 36);
            this.Button16.Name = "Button16";
            this.Button16.Size = new System.Drawing.Size(85, 24);
            this.Button16.TabIndex = 156;
            this.Button16.Text = "Get current";
            this.Button16.UseVisualStyleBackColor = false;
            this.Button16.Click += new System.EventHandler(this.Button16_Click);
            // 
            // Button19
            // 
            this.Button19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button19.CustomColor = System.Drawing.Color.Empty;
            this.Button19.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button19.ForeColor = System.Drawing.Color.White;
            this.Button19.Image = ((System.Drawing.Image)(resources.GetObject("Button19.Image")));
            this.Button19.Location = new System.Drawing.Point(503, 6);
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
            this.TextBox3.Location = new System.Drawing.Point(107, 6);
            this.TextBox3.MaxLength = 32767;
            this.TextBox3.Multiline = false;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.ReadOnly = false;
            this.TextBox3.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox3.SelectedText = "";
            this.TextBox3.SelectionLength = 0;
            this.TextBox3.SelectionStart = 0;
            this.TextBox3.Size = new System.Drawing.Size(389, 24);
            this.TextBox3.TabIndex = 154;
            this.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox3.UseSystemPasswordChar = false;
            this.TextBox3.WordWrap = true;
            this.TextBox3.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
            // 
            // PictureBox8
            // 
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(6, 6);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 153;
            this.PictureBox8.TabStop = false;
            // 
            // Label16
            // 
            this.Label16.Location = new System.Drawing.Point(36, 6);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(65, 24);
            this.Label16.TabIndex = 152;
            this.Label16.Text = "Image:";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LB
            // 
            this.LB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LB.CustomColor = System.Drawing.Color.Empty;
            this.LB.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.LB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LB.ForeColor = System.Drawing.Color.White;
            this.LB.Image = null;
            this.LB.Location = new System.Drawing.Point(462, 133);
            this.LB.Name = "LB";
            this.LB.Size = new System.Drawing.Size(34, 24);
            this.LB.TabIndex = 151;
            this.LB.UseVisualStyleBackColor = false;
            this.LB.Click += new System.EventHandler(this.LB_Click);
            // 
            // SB
            // 
            this.SB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SB.CustomColor = System.Drawing.Color.Empty;
            this.SB.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.SB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SB.ForeColor = System.Drawing.Color.White;
            this.SB.Image = null;
            this.SB.Location = new System.Drawing.Point(462, 103);
            this.SB.Name = "SB";
            this.SB.Size = new System.Drawing.Size(34, 24);
            this.SB.TabIndex = 150;
            this.SB.UseVisualStyleBackColor = false;
            this.SB.Click += new System.EventHandler(this.SB_Click);
            // 
            // HB
            // 
            this.HB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HB.CustomColor = System.Drawing.Color.Empty;
            this.HB.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.HB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.HB.ForeColor = System.Drawing.Color.White;
            this.HB.Image = null;
            this.HB.Location = new System.Drawing.Point(462, 73);
            this.HB.Name = "HB";
            this.HB.Size = new System.Drawing.Size(34, 24);
            this.HB.TabIndex = 149;
            this.HB.UseVisualStyleBackColor = false;
            this.HB.Click += new System.EventHandler(this.HB_Click);
            // 
            // PictureBox9
            // 
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(6, 103);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 148;
            this.PictureBox9.TabStop = false;
            // 
            // Label17
            // 
            this.Label17.Location = new System.Drawing.Point(36, 103);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(65, 24);
            this.Label17.TabIndex = 147;
            this.Label17.Text = "Saturation:";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox10
            // 
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(6, 133);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(24, 24);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 146;
            this.PictureBox10.TabStop = false;
            // 
            // Label18
            // 
            this.Label18.Location = new System.Drawing.Point(36, 133);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(65, 24);
            this.Label18.TabIndex = 145;
            this.Label18.Text = "Lightness:";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(6, 73);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(24, 24);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 144;
            this.PictureBox11.TabStop = false;
            // 
            // Label20
            // 
            this.Label20.Location = new System.Drawing.Point(36, 73);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(65, 24);
            this.Label20.TabIndex = 143;
            this.Label20.Text = "Hue:";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LBar
            // 
            this.LBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(163)))), ((int)(((byte)(255)))));
            this.LBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBar.BackColor = System.Drawing.Color.Transparent;
            this.LBar.H = 0;
            this.LBar.L = 0F;
            this.LBar.LargeChange = 10;
            this.LBar.Location = new System.Drawing.Point(107, 136);
            this.LBar.Maximum = 200;
            this.LBar.Minimum = 0;
            this.LBar.Mode = WinPaletter.UI.WP.ColorBar.ModesList.Light;
            this.LBar.Name = "LBar";
            this.LBar.S = 1F;
            this.LBar.Size = new System.Drawing.Size(349, 19);
            this.LBar.SmallChange = 1;
            this.LBar.TabIndex = 142;
            this.LBar.Value = 100;
            this.LBar.Scroll += new WinPaletter.UI.WP.ColorBar.ScrollEventHandler(this.ColorBar3_Scroll);
            // 
            // SBar
            // 
            this.SBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(163)))), ((int)(((byte)(255)))));
            this.SBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SBar.BackColor = System.Drawing.Color.Transparent;
            this.SBar.H = 0;
            this.SBar.L = 0F;
            this.SBar.LargeChange = 10;
            this.SBar.Location = new System.Drawing.Point(107, 106);
            this.SBar.Maximum = 200;
            this.SBar.Minimum = 0;
            this.SBar.Mode = WinPaletter.UI.WP.ColorBar.ModesList.Saturation;
            this.SBar.Name = "SBar";
            this.SBar.S = 1F;
            this.SBar.Size = new System.Drawing.Size(349, 19);
            this.SBar.SmallChange = 1;
            this.SBar.TabIndex = 141;
            this.SBar.Value = 100;
            this.SBar.Scroll += new WinPaletter.UI.WP.ColorBar.ScrollEventHandler(this.ColorBar2_Scroll_1);
            // 
            // HBar
            // 
            this.HBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(163)))), ((int)(((byte)(255)))));
            this.HBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HBar.BackColor = System.Drawing.Color.Transparent;
            this.HBar.H = 0;
            this.HBar.L = 0F;
            this.HBar.LargeChange = 10;
            this.HBar.Location = new System.Drawing.Point(107, 76);
            this.HBar.Maximum = 360;
            this.HBar.Minimum = 0;
            this.HBar.Mode = WinPaletter.UI.WP.ColorBar.ModesList.Hue;
            this.HBar.Name = "HBar";
            this.HBar.S = 1F;
            this.HBar.Size = new System.Drawing.Size(349, 19);
            this.HBar.SmallChange = 1;
            this.HBar.TabIndex = 140;
            this.HBar.Value = 180;
            this.HBar.Scroll += new WinPaletter.UI.WP.ColorBar.ScrollEventHandler(this.ColorBar1_Scroll);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.Button6);
            this.TabPage3.Controls.Add(this.Button5);
            this.TabPage3.Controls.Add(this.Label13);
            this.TabPage3.Controls.Add(this.CheckBox3);
            this.TabPage3.Controls.Add(this.PictureBox7);
            this.TabPage3.Controls.Add(this.PictureBox16);
            this.TabPage3.Controls.Add(this.MD);
            this.TabPage3.Controls.Add(this.Trackbar1);
            this.TabPage3.Controls.Add(this.Label11);
            this.TabPage3.Controls.Add(this.Separator3);
            this.TabPage3.Controls.Add(this.RadioButton2);
            this.TabPage3.Controls.Add(this.RadioButton1);
            this.TabPage3.Controls.Add(this.PictureBox6);
            this.TabPage3.Controls.Add(this.Button17);
            this.TabPage3.Controls.Add(this.Button18);
            this.TabPage3.Controls.Add(this.ListBox1);
            this.TabPage3.Controls.Add(this.Button4);
            this.TabPage3.Controls.Add(this.TextBox2);
            this.TabPage3.Controls.Add(this.PictureBox5);
            this.TabPage3.Location = new System.Drawing.Point(144, 4);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(544, 340);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Slideshow";
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.Location = new System.Drawing.Point(503, 92);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(34, 24);
            this.Button6.TabIndex = 185;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = ((System.Drawing.Image)(resources.GetObject("Button5.Image")));
            this.Button5.Location = new System.Drawing.Point(503, 64);
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
            this.Label13.Location = new System.Drawing.Point(163, 150);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(374, 24);
            this.Label13.TabIndex = 183;
            this.Label13.Text = "Images in this list must be from the same folder";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckBox3
            // 
            this.CheckBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox3.Checked = false;
            this.CheckBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox3.ForeColor = System.Drawing.Color.White;
            this.CheckBox3.Location = new System.Drawing.Point(36, 225);
            this.CheckBox3.Name = "CheckBox3";
            this.CheckBox3.Size = new System.Drawing.Size(501, 24);
            this.CheckBox3.TabIndex = 182;
            this.CheckBox3.Text = "Shuffle";
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(6, 225);
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
            this.PictureBox16.Location = new System.Drawing.Point(6, 195);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(24, 24);
            this.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox16.TabIndex = 180;
            this.PictureBox16.TabStop = false;
            // 
            // MD
            // 
            this.MD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MD.CustomColor = System.Drawing.Color.Empty;
            this.MD.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.MD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MD.ForeColor = System.Drawing.Color.White;
            this.MD.Image = null;
            this.MD.Location = new System.Drawing.Point(473, 195);
            this.MD.Name = "MD";
            this.MD.Size = new System.Drawing.Size(64, 24);
            this.MD.TabIndex = 179;
            this.MD.UseVisualStyleBackColor = false;
            this.MD.Click += new System.EventHandler(this.MD_Click);
            // 
            // Trackbar1
            // 
            this.Trackbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trackbar1.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar1.LargeChange = 1000;
            this.Trackbar1.Location = new System.Drawing.Point(157, 198);
            this.Trackbar1.Maximum = 36000000;
            this.Trackbar1.Minimum = 10000;
            this.Trackbar1.Name = "Trackbar1";
            this.Trackbar1.Size = new System.Drawing.Size(310, 19);
            this.Trackbar1.SmallChange = 100;
            this.Trackbar1.TabIndex = 178;
            this.Trackbar1.Value = 10000;
            this.Trackbar1.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.Trackbar1_Scroll);
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(36, 195);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(119, 24);
            this.Label11.TabIndex = 176;
            this.Label11.Text = "Change every (ms):";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Separator3
            // 
            this.Separator3.AlternativeLook = false;
            this.Separator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator3.BackColor = System.Drawing.Color.Transparent;
            this.Separator3.Location = new System.Drawing.Point(6, 182);
            this.Separator3.Name = "Separator3";
            this.Separator3.Size = new System.Drawing.Size(531, 1);
            this.Separator3.TabIndex = 175;
            this.Separator3.TabStop = false;
            // 
            // RadioButton2
            // 
            this.RadioButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Location = new System.Drawing.Point(36, 36);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(119, 24);
            this.RadioButton2.TabIndex = 174;
            this.RadioButton2.Text = "List of images:";
            this.RadioButton2.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton1_CheckedChanged);
            // 
            // RadioButton1
            // 
            this.RadioButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.RadioButton1.Checked = false;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Location = new System.Drawing.Point(36, 6);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(119, 24);
            this.RadioButton1.TabIndex = 173;
            this.RadioButton1.Text = "Folder:";
            this.RadioButton1.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton1_CheckedChanged);
            // 
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(6, 36);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(24, 24);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 172;
            this.PictureBox6.TabStop = false;
            // 
            // Button17
            // 
            this.Button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button17.CustomColor = System.Drawing.Color.Empty;
            this.Button17.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button17.ForeColor = System.Drawing.Color.White;
            this.Button17.Image = ((System.Drawing.Image)(resources.GetObject("Button17.Image")));
            this.Button17.Location = new System.Drawing.Point(503, 120);
            this.Button17.Name = "Button17";
            this.Button17.Size = new System.Drawing.Size(34, 24);
            this.Button17.TabIndex = 170;
            this.Button17.UseVisualStyleBackColor = false;
            this.Button17.Click += new System.EventHandler(this.Button17_Click);
            // 
            // Button18
            // 
            this.Button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button18.CustomColor = System.Drawing.Color.Empty;
            this.Button18.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button18.ForeColor = System.Drawing.Color.White;
            this.Button18.Image = ((System.Drawing.Image)(resources.GetObject("Button18.Image")));
            this.Button18.Location = new System.Drawing.Point(503, 36);
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
            this.ListBox1.Location = new System.Drawing.Point(161, 36);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ListBox1.Size = new System.Drawing.Size(335, 107);
            this.ListBox1.TabIndex = 168;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.Location = new System.Drawing.Point(503, 6);
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
            this.TextBox2.Location = new System.Drawing.Point(161, 6);
            this.TextBox2.MaxLength = 32767;
            this.TextBox2.Multiline = false;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = false;
            this.TextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox2.SelectedText = "";
            this.TextBox2.SelectionLength = 0;
            this.TextBox2.SelectionStart = 0;
            this.TextBox2.Size = new System.Drawing.Size(335, 24);
            this.TextBox2.TabIndex = 166;
            this.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox2.UseSystemPasswordChar = false;
            this.TextBox2.WordWrap = true;
            this.TextBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(6, 6);
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
            this.previewContainer.Controls.Add(this.Panel1);
            this.previewContainer.Controls.Add(this.pnl_preview);
            this.previewContainer.Controls.Add(this.PictureBox41);
            this.previewContainer.Controls.Add(this.Label19);
            this.previewContainer.Location = new System.Drawing.Point(706, 61);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(536, 340);
            this.previewContainer.TabIndex = 215;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Button14);
            this.Panel1.Controls.Add(this.Button13);
            this.Panel1.Location = new System.Drawing.Point(318, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(214, 32);
            this.Panel1.TabIndex = 218;
            this.Panel1.Visible = false;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Location = new System.Drawing.Point(3, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(130, 24);
            this.Label3.TabIndex = 177;
            this.Label3.Text = "0/0";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.CustomColor = System.Drawing.Color.Empty;
            this.Button14.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = ((System.Drawing.Image)(resources.GetObject("Button14.Image")));
            this.Button14.Location = new System.Drawing.Point(139, 3);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(34, 26);
            this.Button14.TabIndex = 171;
            this.Button14.UseVisualStyleBackColor = false;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.CustomColor = System.Drawing.Color.Empty;
            this.Button13.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button13.ForeColor = System.Drawing.Color.White;
            this.Button13.Image = ((System.Drawing.Image)(resources.GetObject("Button13.Image")));
            this.Button13.Location = new System.Drawing.Point(177, 3);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(34, 26);
            this.Button13.TabIndex = 170;
            this.Button13.UseVisualStyleBackColor = false;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // pnl_preview
            // 
            this.pnl_preview.Location = new System.Drawing.Point(4, 39);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 217;
            this.pnl_preview.TabStop = false;
            // 
            // PictureBox41
            // 
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(4, 4);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(35, 32);
            this.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox41.TabIndex = 4;
            this.PictureBox41.TabStop = false;
            // 
            // Label19
            // 
            this.Label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(45, 5);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(270, 31);
            this.Label19.TabIndex = 3;
            this.Label19.Text = "Preview";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SaveFileDialog2
            // 
            this.SaveFileDialog2.DefaultExt = "wpt";
            this.SaveFileDialog2.Filter = "PNG File|*.png";
            // 
            // Wallpaper_Editor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1254, 457);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.previewContainer);
            this.Controls.Add(this.Button10);
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
            this.Name = "Wallpaper_Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wallpaper";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Wallpaper_Editor_Load);
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.previewContainer.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.ResumeLayout(false);

        }

        internal OpenFileDialog OpenImgDlg;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle WallpaperEnabled;
        internal PictureBox checker_img;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal Label Label2;
        internal UI.WP.RadioImage source_slideshow;
        internal Label Label1;
        internal UI.WP.RadioImage source_color;
        internal Label Label24;
        internal UI.WP.RadioImage source_pic;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox1;
        internal PictureBox PictureBox4;
        internal Label Label4;
        internal Label Label10;
        internal UI.WP.RadioImage style_fit;
        internal Label Label9;
        internal UI.WP.RadioImage style_fill;
        internal Label Label8;
        internal UI.WP.RadioImage style_stretch;
        internal Label Label7;
        internal UI.WP.RadioImage style_center;
        internal Label Label6;
        internal UI.WP.RadioImage style_tile;
        internal PictureBox PictureBox1;
        internal Label Label5;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox2;
        internal PictureBox PictureBox5;
        internal FolderBrowserDialog FolderBrowserDialog1;
        internal ListBox ListBox1;
        internal PictureBox PictureBox6;
        internal UI.WP.Button Button17;
        internal UI.WP.Button Button18;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton1;
        internal Label Label11;
        internal UI.WP.SeparatorH Separator3;
        internal UI.WP.Button MD;
        internal UI.WP.Trackbar Trackbar1;
        internal PictureBox PictureBox16;
        internal UI.WP.CheckBox CheckBox3;
        internal PictureBox PictureBox7;
        internal Label Label13;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button5;
        internal UI.WP.GroupBox previewContainer;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal PictureBox pnl_preview;
        internal Panel Panel1;
        internal UI.WP.Button Button14;
        internal UI.WP.Button Button13;
        internal Label Label3;
        internal UI.WP.SeparatorH Separator1;
        internal PictureBox PictureBox2;
        internal Label Label14;
        internal Panel Panel2;
        internal UI.WP.AlertBox AlertBox1;
        internal Label Label15;
        internal PictureBox PictureBox3;
        internal UI.Controllers.ColorItem color_pick;
        internal UI.WP.AlertBox AlertBox2;
        internal TabPage TabPage5;
        internal Label Label21;
        internal UI.WP.RadioImage source_wallpapertone;
        internal UI.WP.SeparatorH Separator2;
        internal UI.WP.Button Button15;
        internal UI.WP.Button Button16;
        internal UI.WP.Button Button19;
        internal UI.WP.TextBox TextBox3;
        internal PictureBox PictureBox8;
        internal Label Label16;
        internal UI.WP.Button LB;
        internal UI.WP.Button SB;
        internal UI.WP.Button HB;
        internal PictureBox PictureBox9;
        internal Label Label17;
        internal PictureBox PictureBox10;
        internal Label Label18;
        internal PictureBox PictureBox11;
        internal Label Label20;
        internal UI.WP.ColorBar LBar;
        internal UI.WP.ColorBar SBar;
        internal UI.WP.ColorBar HBar;
        internal UI.WP.AlertBox AlertBox3;
        internal UI.WP.Button Button20;
        internal SaveFileDialog SaveFileDialog2;
        internal UI.WP.Button Button21;
        internal UI.WP.Button Button22;
        internal UI.WP.Button Button23;
        internal UI.WP.SeparatorH Separator4;
    }
}
