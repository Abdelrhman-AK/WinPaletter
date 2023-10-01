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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Wallpaper_Editor));
            OpenImgDlg = new OpenFileDialog();
            OpenFileDialog1 = new OpenFileDialog();
            GroupBox12 = new UI.WP.GroupBox();
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label12 = new Label();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            WallpaperEnabled = new UI.WP.Toggle();
            WallpaperEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(WallpaperEnabled_CheckedChanged);
            checker_img = new PictureBox();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            Label21 = new Label();
            source_wallpapertone = new UI.WP.RadioImage();
            source_wallpapertone.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Source_pic_CheckedChanged);
            AlertBox1 = new UI.WP.AlertBox();
            Separator1 = new UI.WP.SeparatorH();
            PictureBox2 = new PictureBox();
            Label14 = new Label();
            Label2 = new Label();
            PictureBox1 = new PictureBox();
            Label5 = new Label();
            source_slideshow = new UI.WP.RadioImage();
            source_slideshow.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Source_slideshow_CheckedChanged);
            Label1 = new Label();
            source_color = new UI.WP.RadioImage();
            source_color.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Source_pic_CheckedChanged);
            Label24 = new Label();
            source_pic = new UI.WP.RadioImage();
            source_pic.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Source_pic_CheckedChanged);
            Panel2 = new Panel();
            style_fill = new UI.WP.RadioImage();
            style_fill.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Style_fill_CheckedChanged);
            style_center = new UI.WP.RadioImage();
            style_center.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Style_fill_CheckedChanged);
            Label10 = new Label();
            style_tile = new UI.WP.RadioImage();
            style_tile.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Style_fill_CheckedChanged);
            Label6 = new Label();
            Label7 = new Label();
            style_fit = new UI.WP.RadioImage();
            style_fit.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Style_fill_CheckedChanged);
            style_stretch = new UI.WP.RadioImage();
            style_stretch.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Style_fill_CheckedChanged);
            Label8 = new Label();
            Label9 = new Label();
            TabPage2 = new TabPage();
            Separator4 = new UI.WP.SeparatorH();
            AlertBox2 = new UI.WP.AlertBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Label15 = new Label();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            PictureBox3 = new PictureBox();
            color_pick = new UI.Controllers.ColorItem();
            color_pick.DragDrop += new DragEventHandler(Color_pick_DragDrop);
            color_pick.Click += new EventHandler(Color_pick_Click);
            PictureBox4 = new PictureBox();
            Label4 = new Label();
            TabPage5 = new TabPage();
            Button21 = new UI.WP.Button();
            Button21.Click += new EventHandler(Button21_Click);
            Button22 = new UI.WP.Button();
            Button22.Click += new EventHandler(Button22_Click);
            Button23 = new UI.WP.Button();
            Button23.Click += new EventHandler(Button23_Click);
            Button20 = new UI.WP.Button();
            Button20.Click += new EventHandler(Button20_Click);
            AlertBox3 = new UI.WP.AlertBox();
            Separator2 = new UI.WP.SeparatorH();
            Button15 = new UI.WP.Button();
            Button15.Click += new EventHandler(Button15_Click);
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click);
            Button19 = new UI.WP.Button();
            Button19.Click += new EventHandler(Button19_Click);
            TextBox3 = new UI.WP.TextBox();
            TextBox3.TextChanged += new EventHandler(TextBox3_TextChanged);
            PictureBox8 = new PictureBox();
            Label16 = new Label();
            LB = new UI.WP.Button();
            LB.Click += new EventHandler(LB_Click);
            SB = new UI.WP.Button();
            SB.Click += new EventHandler(SB_Click);
            HB = new UI.WP.Button();
            HB.Click += new EventHandler(HB_Click);
            PictureBox9 = new PictureBox();
            Label17 = new Label();
            PictureBox10 = new PictureBox();
            Label18 = new Label();
            PictureBox11 = new PictureBox();
            Label20 = new Label();
            LBar = new UI.WP.ColorBar();
            LBar.Scroll += new UI.WP.ColorBar.ScrollEventHandler(ColorBar3_Scroll);
            SBar = new UI.WP.ColorBar();
            SBar.Scroll += new UI.WP.ColorBar.ScrollEventHandler(ColorBar2_Scroll_1);
            HBar = new UI.WP.ColorBar();
            HBar.Scroll += new UI.WP.ColorBar.ScrollEventHandler(ColorBar1_Scroll);
            TabPage3 = new TabPage();
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Label13 = new Label();
            CheckBox3 = new UI.WP.CheckBox();
            PictureBox7 = new PictureBox();
            PictureBox16 = new PictureBox();
            MD = new UI.WP.Button();
            MD.Click += new EventHandler(MD_Click);
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            Label11 = new Label();
            Separator3 = new UI.WP.SeparatorH();
            RadioButton2 = new UI.WP.RadioButton();
            RadioButton2.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton1_CheckedChanged);
            RadioButton1 = new UI.WP.RadioButton();
            RadioButton1.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton1_CheckedChanged);
            PictureBox6 = new PictureBox();
            Button17 = new UI.WP.Button();
            Button17.Click += new EventHandler(Button17_Click);
            Button18 = new UI.WP.Button();
            Button18.Click += new EventHandler(Button18_Click);
            ListBox1 = new ListBox();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            TextBox2 = new UI.WP.TextBox();
            TextBox2.TextChanged += new EventHandler(TextBox2_TextChanged);
            PictureBox5 = new PictureBox();
            FolderBrowserDialog1 = new FolderBrowserDialog();
            previewContainer = new UI.WP.GroupBox();
            Panel1 = new Panel();
            Label3 = new Label();
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click);
            Button13 = new UI.WP.Button();
            Button13.Click += new EventHandler(Button13_Click);
            pnl_preview = new PictureBox();
            PictureBox41 = new PictureBox();
            Label19 = new Label();
            SaveFileDialog2 = new SaveFileDialog();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            Panel2.SuspendLayout();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            previewContainer.SuspendLayout();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnl_preview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            SuspendLayout();
            // 
            // OpenImgDlg
            // 
            OpenImgDlg.Filter = "Images (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif|All Files (*.*)|*.*";
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(Button9);
            GroupBox12.Controls.Add(Label12);
            GroupBox12.Controls.Add(Button11);
            GroupBox12.Controls.Add(Button12);
            GroupBox12.Controls.Add(WallpaperEnabled);
            GroupBox12.Controls.Add(checker_img);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(1230, 39);
            GroupBox12.TabIndex = 201;
            // 
            // Button9
            // 
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.ImageAlign = ContentAlignment.MiddleRight;
            Button9.LineColor = Color.FromArgb(90, 134, 117);
            Button9.Location = new Point(222, 5);
            Button9.Name = "Button9";
            Button9.Size = new Size(126, 29);
            Button9.TabIndex = 112;
            Button9.Text = "Current applied";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label12.Location = new Point(4, 4);
            Label12.Name = "Label12";
            Label12.Size = new Size(75, 31);
            Label12.TabIndex = 111;
            Label12.Text = "Open from:";
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            Button11.BackColor = Color.FromArgb(43, 43, 43);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.ImageAlign = ContentAlignment.MiddleRight;
            Button11.LineColor = Color.FromArgb(113, 122, 131);
            Button11.Location = new Point(85, 5);
            Button11.Name = "Button11";
            Button11.Size = new Size(135, 29);
            Button11.TabIndex = 110;
            Button11.Text = "WinPaletter theme";
            Button11.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button12.BackColor = Color.FromArgb(43, 43, 43);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = null;
            Button12.ImageAlign = ContentAlignment.MiddleRight;
            Button12.LineColor = Color.FromArgb(0, 66, 119);
            Button12.Location = new Point(351, 5);
            Button12.Name = "Button12";
            Button12.Size = new Size(135, 29);
            Button12.TabIndex = 108;
            Button12.Text = "Default Windows";
            Button12.UseVisualStyleBackColor = false;
            // 
            // WallpaperEnabled
            // 
            WallpaperEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            WallpaperEnabled.BackColor = Color.FromArgb(43, 43, 43);
            WallpaperEnabled.Checked = false;
            WallpaperEnabled.DarkLight_Toggler = false;
            WallpaperEnabled.Location = new Point(1184, 9);
            WallpaperEnabled.Name = "WallpaperEnabled";
            WallpaperEnabled.Size = new Size(40, 20);
            WallpaperEnabled.TabIndex = 85;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = My.Resources.checker_disabled;
            checker_img.Location = new Point(1143, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // Button10
            // 
            Button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button10.BackColor = Color.FromArgb(34, 34, 34);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = (Image)resources.GetObject("Button10.Image");
            Button10.ImageAlign = ContentAlignment.MiddleLeft;
            Button10.LineColor = Color.FromArgb(36, 81, 110);
            Button10.Location = new Point(941, 411);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 213;
            Button10.Text = "Quick apply";
            Button10.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.LineColor = Color.FromArgb(199, 49, 61);
            Button7.Location = new Point(855, 411);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 212;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button8.BackColor = Color.FromArgb(34, 34, 34);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleLeft;
            Button8.LineColor = Color.FromArgb(52, 20, 64);
            Button8.Location = new Point(1062, 411);
            Button8.Name = "Button8";
            Button8.Size = new Size(180, 34);
            Button8.TabIndex = 211;
            Button8.Text = "Load into current theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.AllowDrop = true;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage5);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ItemSize = new Size(30, 140);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(12, 57);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(692, 348);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 214;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(Label21);
            TabPage1.Controls.Add(source_wallpapertone);
            TabPage1.Controls.Add(AlertBox1);
            TabPage1.Controls.Add(Separator1);
            TabPage1.Controls.Add(PictureBox2);
            TabPage1.Controls.Add(Label14);
            TabPage1.Controls.Add(Label2);
            TabPage1.Controls.Add(PictureBox1);
            TabPage1.Controls.Add(Label5);
            TabPage1.Controls.Add(source_slideshow);
            TabPage1.Controls.Add(Label1);
            TabPage1.Controls.Add(source_color);
            TabPage1.Controls.Add(Label24);
            TabPage1.Controls.Add(source_pic);
            TabPage1.Controls.Add(Panel2);
            TabPage1.Location = new Point(144, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(544, 340);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Source & style";
            // 
            // Label21
            // 
            Label21.AutoEllipsis = true;
            Label21.BackColor = Color.Transparent;
            Label21.Font = new Font("Segoe UI", 9.0f);
            Label21.Location = new Point(196, 79);
            Label21.Name = "Label21";
            Label21.Size = new Size(80, 27);
            Label21.TabIndex = 162;
            Label21.Text = "Tone";
            Label21.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // source_wallpapertone
            // 
            source_wallpapertone.Checked = false;
            source_wallpapertone.Font = new Font("Segoe UI", 9.0f);
            source_wallpapertone.ForeColor = Color.White;
            source_wallpapertone.Image = (Image)resources.GetObject("source_wallpapertone.Image");
            source_wallpapertone.Location = new Point(196, 9);
            source_wallpapertone.Name = "source_wallpapertone";
            source_wallpapertone.ShowText = false;
            source_wallpapertone.Size = new Size(80, 64);
            source_wallpapertone.TabIndex = 161;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(6, 258);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(532, 76);
            AlertBox1.TabIndex = 160;
            AlertBox1.TabStop = false;
            AlertBox1.Text = resources.GetString("AlertBox1.Text");
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(6, 112);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(532, 1);
            Separator1.TabIndex = 158;
            Separator1.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(6, 149);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 149;
            PictureBox2.TabStop = false;
            // 
            // Label14
            // 
            Label14.Location = new Point(36, 149);
            Label14.Name = "Label14";
            Label14.Size = new Size(65, 24);
            Label14.TabIndex = 148;
            Label14.Text = "Style:";
            Label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            Label2.AutoEllipsis = true;
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f);
            Label2.Location = new Point(368, 79);
            Label2.Name = "Label2";
            Label2.Size = new Size(80, 27);
            Label2.TabIndex = 40;
            Label2.Text = "Slideshow";
            Label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 29);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 147;
            PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            Label5.Location = new Point(36, 29);
            Label5.Name = "Label5";
            Label5.Size = new Size(65, 24);
            Label5.TabIndex = 146;
            Label5.Text = "Source:";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // source_slideshow
            // 
            source_slideshow.Checked = false;
            source_slideshow.Font = new Font("Segoe UI", 9.0f);
            source_slideshow.ForeColor = Color.White;
            source_slideshow.Image = (Image)resources.GetObject("source_slideshow.Image");
            source_slideshow.Location = new Point(368, 9);
            source_slideshow.Name = "source_slideshow";
            source_slideshow.ShowText = false;
            source_slideshow.Size = new Size(80, 64);
            source_slideshow.TabIndex = 39;
            // 
            // Label1
            // 
            Label1.AutoEllipsis = true;
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f);
            Label1.Location = new Point(282, 79);
            Label1.Name = "Label1";
            Label1.Size = new Size(80, 27);
            Label1.TabIndex = 38;
            Label1.Text = "Color";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // source_color
            // 
            source_color.Checked = false;
            source_color.Font = new Font("Segoe UI", 9.0f);
            source_color.ForeColor = Color.White;
            source_color.Image = (Image)resources.GetObject("source_color.Image");
            source_color.Location = new Point(282, 9);
            source_color.Name = "source_color";
            source_color.ShowText = false;
            source_color.Size = new Size(80, 64);
            source_color.TabIndex = 37;
            // 
            // Label24
            // 
            Label24.AutoEllipsis = true;
            Label24.BackColor = Color.Transparent;
            Label24.Font = new Font("Segoe UI", 9.0f);
            Label24.Location = new Point(110, 79);
            Label24.Name = "Label24";
            Label24.Size = new Size(80, 27);
            Label24.TabIndex = 36;
            Label24.Text = "Picture";
            Label24.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // source_pic
            // 
            source_pic.Checked = false;
            source_pic.Font = new Font("Segoe UI", 9.0f);
            source_pic.ForeColor = Color.White;
            source_pic.Image = (Image)resources.GetObject("source_pic.Image");
            source_pic.Location = new Point(110, 9);
            source_pic.Name = "source_pic";
            source_pic.ShowText = false;
            source_pic.Size = new Size(80, 64);
            source_pic.TabIndex = 35;
            // 
            // Panel2
            // 
            Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel2.BackColor = Color.Transparent;
            Panel2.Controls.Add(style_fill);
            Panel2.Controls.Add(style_center);
            Panel2.Controls.Add(Label10);
            Panel2.Controls.Add(style_tile);
            Panel2.Controls.Add(Label6);
            Panel2.Controls.Add(Label7);
            Panel2.Controls.Add(style_fit);
            Panel2.Controls.Add(style_stretch);
            Panel2.Controls.Add(Label8);
            Panel2.Controls.Add(Label9);
            Panel2.Location = new Point(107, 119);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(431, 108);
            Panel2.TabIndex = 159;
            // 
            // style_fill
            // 
            style_fill.Checked = true;
            style_fill.Font = new Font("Segoe UI", 9.0f);
            style_fill.ForeColor = Color.White;
            style_fill.Image = (Image)resources.GetObject("style_fill.Image");
            style_fill.Location = new Point(3, 3);
            style_fill.Name = "style_fill";
            style_fill.ShowText = false;
            style_fill.Size = new Size(80, 64);
            style_fill.TabIndex = 154;
            // 
            // style_center
            // 
            style_center.Checked = false;
            style_center.Font = new Font("Segoe UI", 9.0f);
            style_center.ForeColor = Color.White;
            style_center.Image = (Image)resources.GetObject("style_center.Image");
            style_center.Location = new Point(261, 3);
            style_center.Name = "style_center";
            style_center.ShowText = false;
            style_center.Size = new Size(80, 64);
            style_center.TabIndex = 150;
            // 
            // Label10
            // 
            Label10.AutoEllipsis = true;
            Label10.BackColor = Color.Transparent;
            Label10.Font = new Font("Segoe UI", 9.0f);
            Label10.Location = new Point(89, 73);
            Label10.Name = "Label10";
            Label10.Size = new Size(80, 27);
            Label10.TabIndex = 157;
            Label10.Text = "Fit";
            Label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // style_tile
            // 
            style_tile.Checked = false;
            style_tile.Font = new Font("Segoe UI", 9.0f);
            style_tile.ForeColor = Color.White;
            style_tile.Image = (Image)resources.GetObject("style_tile.Image");
            style_tile.Location = new Point(347, 3);
            style_tile.Name = "style_tile";
            style_tile.ShowText = false;
            style_tile.Size = new Size(80, 64);
            style_tile.TabIndex = 148;
            // 
            // Label6
            // 
            Label6.AutoEllipsis = true;
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f);
            Label6.Location = new Point(347, 73);
            Label6.Name = "Label6";
            Label6.Size = new Size(80, 27);
            Label6.TabIndex = 149;
            Label6.Text = "Tile";
            Label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label7
            // 
            Label7.AutoEllipsis = true;
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 9.0f);
            Label7.Location = new Point(261, 73);
            Label7.Name = "Label7";
            Label7.Size = new Size(80, 27);
            Label7.TabIndex = 151;
            Label7.Text = "Centered";
            Label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // style_fit
            // 
            style_fit.Checked = false;
            style_fit.Font = new Font("Segoe UI", 9.0f);
            style_fit.ForeColor = Color.White;
            style_fit.Image = (Image)resources.GetObject("style_fit.Image");
            style_fit.Location = new Point(89, 3);
            style_fit.Name = "style_fit";
            style_fit.ShowText = false;
            style_fit.Size = new Size(80, 64);
            style_fit.TabIndex = 156;
            // 
            // style_stretch
            // 
            style_stretch.Checked = false;
            style_stretch.Font = new Font("Segoe UI", 9.0f);
            style_stretch.ForeColor = Color.White;
            style_stretch.Image = (Image)resources.GetObject("style_stretch.Image");
            style_stretch.Location = new Point(175, 3);
            style_stretch.Name = "style_stretch";
            style_stretch.ShowText = false;
            style_stretch.Size = new Size(80, 64);
            style_stretch.TabIndex = 152;
            // 
            // Label8
            // 
            Label8.AutoEllipsis = true;
            Label8.BackColor = Color.Transparent;
            Label8.Font = new Font("Segoe UI", 9.0f);
            Label8.Location = new Point(175, 73);
            Label8.Name = "Label8";
            Label8.Size = new Size(80, 27);
            Label8.TabIndex = 153;
            Label8.Text = "Stretch";
            Label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label9
            // 
            Label9.AutoEllipsis = true;
            Label9.BackColor = Color.Transparent;
            Label9.Font = new Font("Segoe UI", 9.0f);
            Label9.Location = new Point(3, 73);
            Label9.Name = "Label9";
            Label9.Size = new Size(80, 27);
            Label9.TabIndex = 155;
            Label9.Text = "Fill";
            Label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(Separator4);
            TabPage2.Controls.Add(AlertBox2);
            TabPage2.Controls.Add(Button3);
            TabPage2.Controls.Add(Button2);
            TabPage2.Controls.Add(Label15);
            TabPage2.Controls.Add(Button1);
            TabPage2.Controls.Add(TextBox1);
            TabPage2.Controls.Add(PictureBox3);
            TabPage2.Controls.Add(color_pick);
            TabPage2.Controls.Add(PictureBox4);
            TabPage2.Controls.Add(Label4);
            TabPage2.Location = new Point(144, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(544, 340);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Wallpaper";
            // 
            // Separator4
            // 
            Separator4.AlternativeLook = false;
            Separator4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator4.Location = new Point(6, 66);
            Separator4.Name = "Separator4";
            Separator4.Size = new Size(531, 1);
            Separator4.TabIndex = 162;
            Separator4.TabStop = false;
            // 
            // AlertBox2
            // 
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox2.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox2.CenterText = false;
            AlertBox2.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox2.Font = new Font("Segoe UI", 9.0f);
            AlertBox2.Image = null;
            AlertBox2.Location = new Point(6, 106);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(531, 22);
            AlertBox2.TabIndex = 161;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "This color is shared with Classic Colors (Window Objects > Background color)";
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.ImageAlign = ContentAlignment.MiddleRight;
            Button3.LineColor = Color.FromArgb(0, 66, 119);
            Button3.Location = new Point(452, 36);
            Button3.Name = "Button3";
            Button3.Size = new Size(85, 24);
            Button3.TabIndex = 144;
            Button3.Text = "Get default";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(43, 43, 43);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.ImageAlign = ContentAlignment.MiddleRight;
            Button2.LineColor = Color.FromArgb(0, 66, 119);
            Button2.Location = new Point(361, 36);
            Button2.Name = "Button2";
            Button2.Size = new Size(85, 24);
            Button2.TabIndex = 143;
            Button2.Text = "Get current";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Label15
            // 
            Label15.Location = new Point(36, 73);
            Label15.Name = "Label15";
            Label15.Size = new Size(65, 25);
            Label15.TabIndex = 140;
            Label15.Text = "Color:";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(43, 43, 43);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.LineColor = Color.FromArgb(184, 153, 68);
            Button1.Location = new Point(503, 6);
            Button1.Margin = new Padding(4, 3, 4, 3);
            Button1.Name = "Button1";
            Button1.Size = new Size(34, 24);
            Button1.TabIndex = 142;
            Button1.UseVisualStyleBackColor = false;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(107, 6);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(389, 24);
            TextBox1.TabIndex = 141;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(6, 73);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 94;
            PictureBox3.TabStop = false;
            // 
            // color_pick
            // 
            color_pick.AllowDrop = true;
            color_pick.BackColor = Color.FromArgb(47, 47, 48);
            color_pick.DefaultColor = Color.Black;
            color_pick.DontShowInfo = false;
            color_pick.Location = new Point(107, 73);
            color_pick.Margin = new Padding(4, 3, 4, 3);
            color_pick.Name = "color_pick";
            color_pick.Size = new Size(97, 25);
            color_pick.TabIndex = 93;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(6, 6);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 140;
            PictureBox4.TabStop = false;
            // 
            // Label4
            // 
            Label4.Location = new Point(36, 6);
            Label4.Name = "Label4";
            Label4.Size = new Size(65, 24);
            Label4.TabIndex = 139;
            Label4.Text = "Image:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(Button21);
            TabPage5.Controls.Add(Button22);
            TabPage5.Controls.Add(Button23);
            TabPage5.Controls.Add(Button20);
            TabPage5.Controls.Add(AlertBox3);
            TabPage5.Controls.Add(Separator2);
            TabPage5.Controls.Add(Button15);
            TabPage5.Controls.Add(Button16);
            TabPage5.Controls.Add(Button19);
            TabPage5.Controls.Add(TextBox3);
            TabPage5.Controls.Add(PictureBox8);
            TabPage5.Controls.Add(Label16);
            TabPage5.Controls.Add(LB);
            TabPage5.Controls.Add(SB);
            TabPage5.Controls.Add(HB);
            TabPage5.Controls.Add(PictureBox9);
            TabPage5.Controls.Add(Label17);
            TabPage5.Controls.Add(PictureBox10);
            TabPage5.Controls.Add(Label18);
            TabPage5.Controls.Add(PictureBox11);
            TabPage5.Controls.Add(Label20);
            TabPage5.Controls.Add(LBar);
            TabPage5.Controls.Add(SBar);
            TabPage5.Controls.Add(HBar);
            TabPage5.Location = new Point(144, 4);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(3);
            TabPage5.Size = new Size(544, 340);
            TabPage5.TabIndex = 4;
            TabPage5.Text = "Wallpaper Tone";
            // 
            // Button21
            // 
            Button21.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button21.BackColor = Color.FromArgb(43, 43, 43);
            Button21.DrawOnGlass = false;
            Button21.Font = new Font("Segoe UI", 9.0f);
            Button21.ForeColor = Color.White;
            Button21.Image = (Image)resources.GetObject("Button21.Image");
            Button21.LineColor = Color.FromArgb(15, 74, 100);
            Button21.Location = new Point(502, 133);
            Button21.Name = "Button21";
            Button21.Size = new Size(34, 24);
            Button21.TabIndex = 211;
            Button21.UseVisualStyleBackColor = false;
            // 
            // Button22
            // 
            Button22.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button22.BackColor = Color.FromArgb(43, 43, 43);
            Button22.DrawOnGlass = false;
            Button22.Font = new Font("Segoe UI", 9.0f);
            Button22.ForeColor = Color.White;
            Button22.Image = (Image)resources.GetObject("Button22.Image");
            Button22.LineColor = Color.FromArgb(15, 74, 100);
            Button22.Location = new Point(502, 103);
            Button22.Name = "Button22";
            Button22.Size = new Size(34, 24);
            Button22.TabIndex = 210;
            Button22.UseVisualStyleBackColor = false;
            // 
            // Button23
            // 
            Button23.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button23.BackColor = Color.FromArgb(43, 43, 43);
            Button23.DrawOnGlass = false;
            Button23.Font = new Font("Segoe UI", 9.0f);
            Button23.ForeColor = Color.White;
            Button23.Image = (Image)resources.GetObject("Button23.Image");
            Button23.LineColor = Color.FromArgb(15, 74, 100);
            Button23.Location = new Point(502, 73);
            Button23.Name = "Button23";
            Button23.Size = new Size(34, 24);
            Button23.TabIndex = 209;
            Button23.UseVisualStyleBackColor = false;
            // 
            // Button20
            // 
            Button20.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button20.BackColor = Color.FromArgb(43, 43, 43);
            Button20.DrawOnGlass = false;
            Button20.Font = new Font("Segoe UI", 9.0f);
            Button20.ForeColor = Color.White;
            Button20.Image = (Image)resources.GetObject("Button20.Image");
            Button20.ImageAlign = ContentAlignment.MiddleLeft;
            Button20.LineColor = Color.FromArgb(69, 110, 129);
            Button20.Location = new Point(151, 36);
            Button20.Name = "Button20";
            Button20.Size = new Size(204, 24);
            Button20.TabIndex = 208;
            Button20.Text = "Save modified wallpaper as ...";
            Button20.UseVisualStyleBackColor = false;
            // 
            // AlertBox3
            // 
            AlertBox3.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox3.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox3.CenterText = true;
            AlertBox3.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox3.Font = new Font("Segoe UI", 9.0f);
            AlertBox3.Image = null;
            AlertBox3.Location = new Point(6, 163);
            AlertBox3.Name = "AlertBox3";
            AlertBox3.Size = new Size(532, 43);
            AlertBox3.TabIndex = 207;
            AlertBox3.TabStop = false;
            AlertBox3.Text = null;
            // 
            // Separator2
            // 
            Separator2.AlternativeLook = false;
            Separator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator2.Location = new Point(6, 66);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(530, 1);
            Separator2.TabIndex = 158;
            Separator2.TabStop = false;
            // 
            // Button15
            // 
            Button15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button15.BackColor = Color.FromArgb(43, 43, 43);
            Button15.DrawOnGlass = false;
            Button15.Font = new Font("Segoe UI", 9.0f);
            Button15.ForeColor = Color.White;
            Button15.Image = null;
            Button15.ImageAlign = ContentAlignment.MiddleRight;
            Button15.LineColor = Color.FromArgb(0, 66, 119);
            Button15.Location = new Point(452, 36);
            Button15.Name = "Button15";
            Button15.Size = new Size(85, 24);
            Button15.TabIndex = 157;
            Button15.Text = "Get default";
            Button15.UseVisualStyleBackColor = false;
            // 
            // Button16
            // 
            Button16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button16.BackColor = Color.FromArgb(43, 43, 43);
            Button16.DrawOnGlass = false;
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = null;
            Button16.ImageAlign = ContentAlignment.MiddleRight;
            Button16.LineColor = Color.FromArgb(0, 66, 119);
            Button16.Location = new Point(361, 36);
            Button16.Name = "Button16";
            Button16.Size = new Size(85, 24);
            Button16.TabIndex = 156;
            Button16.Text = "Get current";
            Button16.UseVisualStyleBackColor = false;
            // 
            // Button19
            // 
            Button19.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button19.BackColor = Color.FromArgb(43, 43, 43);
            Button19.DrawOnGlass = false;
            Button19.Font = new Font("Segoe UI", 9.0f);
            Button19.ForeColor = Color.White;
            Button19.Image = (Image)resources.GetObject("Button19.Image");
            Button19.LineColor = Color.FromArgb(184, 153, 68);
            Button19.Location = new Point(503, 6);
            Button19.Margin = new Padding(4, 3, 4, 3);
            Button19.Name = "Button19";
            Button19.Size = new Size(34, 24);
            Button19.TabIndex = 155;
            Button19.UseVisualStyleBackColor = false;
            // 
            // TextBox3
            // 
            TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox3.BackColor = Color.FromArgb(55, 55, 55);
            TextBox3.DrawOnGlass = false;
            TextBox3.ForeColor = Color.White;
            TextBox3.Location = new Point(107, 6);
            TextBox3.MaxLength = 32767;
            TextBox3.Multiline = false;
            TextBox3.Name = "TextBox3";
            TextBox3.ReadOnly = false;
            TextBox3.Scrollbars = ScrollBars.None;
            TextBox3.SelectedText = "";
            TextBox3.SelectionLength = 0;
            TextBox3.SelectionStart = 0;
            TextBox3.Size = new Size(389, 24);
            TextBox3.TabIndex = 154;
            TextBox3.TextAlign = HorizontalAlignment.Left;
            TextBox3.UseSystemPasswordChar = false;
            TextBox3.WordWrap = true;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(6, 6);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 153;
            PictureBox8.TabStop = false;
            // 
            // Label16
            // 
            Label16.Location = new Point(36, 6);
            Label16.Name = "Label16";
            Label16.Size = new Size(65, 24);
            Label16.TabIndex = 152;
            Label16.Text = "Image:";
            Label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LB
            // 
            LB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LB.BackColor = Color.FromArgb(43, 43, 43);
            LB.DrawOnGlass = false;
            LB.Font = new Font("Segoe UI", 9.0f);
            LB.ForeColor = Color.White;
            LB.Image = null;
            LB.LineColor = Color.FromArgb(0, 81, 210);
            LB.Location = new Point(462, 133);
            LB.Name = "LB";
            LB.Size = new Size(34, 24);
            LB.TabIndex = 151;
            LB.UseVisualStyleBackColor = false;
            // 
            // SB
            // 
            SB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SB.BackColor = Color.FromArgb(43, 43, 43);
            SB.DrawOnGlass = false;
            SB.Font = new Font("Segoe UI", 9.0f);
            SB.ForeColor = Color.White;
            SB.Image = null;
            SB.LineColor = Color.FromArgb(0, 81, 210);
            SB.Location = new Point(462, 103);
            SB.Name = "SB";
            SB.Size = new Size(34, 24);
            SB.TabIndex = 150;
            SB.UseVisualStyleBackColor = false;
            // 
            // HB
            // 
            HB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            HB.BackColor = Color.FromArgb(43, 43, 43);
            HB.DrawOnGlass = false;
            HB.Font = new Font("Segoe UI", 9.0f);
            HB.ForeColor = Color.White;
            HB.Image = null;
            HB.LineColor = Color.FromArgb(0, 81, 210);
            HB.Location = new Point(462, 73);
            HB.Name = "HB";
            HB.Size = new Size(34, 24);
            HB.TabIndex = 149;
            HB.UseVisualStyleBackColor = false;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(6, 103);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 148;
            PictureBox9.TabStop = false;
            // 
            // Label17
            // 
            Label17.Location = new Point(36, 103);
            Label17.Name = "Label17";
            Label17.Size = new Size(65, 24);
            Label17.TabIndex = 147;
            Label17.Text = "Saturation:";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(6, 133);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 146;
            PictureBox10.TabStop = false;
            // 
            // Label18
            // 
            Label18.Location = new Point(36, 133);
            Label18.Name = "Label18";
            Label18.Size = new Size(65, 24);
            Label18.TabIndex = 145;
            Label18.Text = "Lightness:";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(6, 73);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 144;
            PictureBox11.TabStop = false;
            // 
            // Label20
            // 
            Label20.Location = new Point(36, 73);
            Label20.Name = "Label20";
            Label20.Size = new Size(65, 24);
            Label20.TabIndex = 143;
            Label20.Text = "Hue:";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LBar
            // 
            LBar.AccentColor = Color.FromArgb(106, 163, 255);
            LBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LBar.H = 0;
            LBar.L = 0f;
            LBar.LargeChange = 10;
            LBar.Location = new Point(107, 136);
            LBar.Maximum = 200;
            LBar.Minimum = 0;
            LBar.Mode = UI.WP.ColorBar.ModesList.Light;
            LBar.Name = "LBar";
            LBar.S = 1.0f;
            LBar.Size = new Size(349, 19);
            LBar.SmallChange = 1;
            LBar.TabIndex = 142;
            LBar.Value = 100;
            // 
            // SBar
            // 
            SBar.AccentColor = Color.FromArgb(106, 163, 255);
            SBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SBar.H = 0;
            SBar.L = 0f;
            SBar.LargeChange = 10;
            SBar.Location = new Point(107, 106);
            SBar.Maximum = 200;
            SBar.Minimum = 0;
            SBar.Mode = UI.WP.ColorBar.ModesList.Saturation;
            SBar.Name = "SBar";
            SBar.S = 1.0f;
            SBar.Size = new Size(349, 19);
            SBar.SmallChange = 1;
            SBar.TabIndex = 141;
            SBar.Value = 100;
            // 
            // HBar
            // 
            HBar.AccentColor = Color.FromArgb(106, 163, 255);
            HBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            HBar.H = 0;
            HBar.L = 0f;
            HBar.LargeChange = 10;
            HBar.Location = new Point(107, 76);
            HBar.Maximum = 360;
            HBar.Minimum = 0;
            HBar.Mode = UI.WP.ColorBar.ModesList.Hue;
            HBar.Name = "HBar";
            HBar.S = 1.0f;
            HBar.Size = new Size(349, 19);
            HBar.SmallChange = 1;
            HBar.TabIndex = 140;
            HBar.Value = 180;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(Button6);
            TabPage3.Controls.Add(Button5);
            TabPage3.Controls.Add(Label13);
            TabPage3.Controls.Add(CheckBox3);
            TabPage3.Controls.Add(PictureBox7);
            TabPage3.Controls.Add(PictureBox16);
            TabPage3.Controls.Add(MD);
            TabPage3.Controls.Add(Trackbar1);
            TabPage3.Controls.Add(Label11);
            TabPage3.Controls.Add(Separator3);
            TabPage3.Controls.Add(RadioButton2);
            TabPage3.Controls.Add(RadioButton1);
            TabPage3.Controls.Add(PictureBox6);
            TabPage3.Controls.Add(Button17);
            TabPage3.Controls.Add(Button18);
            TabPage3.Controls.Add(ListBox1);
            TabPage3.Controls.Add(Button4);
            TabPage3.Controls.Add(TextBox2);
            TabPage3.Controls.Add(PictureBox5);
            TabPage3.Location = new Point(144, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(544, 340);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Slideshow";
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button6.BackColor = Color.FromArgb(34, 34, 34);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.LineColor = Color.FromArgb(14, 65, 104);
            Button6.Location = new Point(503, 92);
            Button6.Name = "Button6";
            Button6.Size = new Size(34, 24);
            Button6.TabIndex = 185;
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(34, 34, 34);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.LineColor = Color.FromArgb(14, 65, 104);
            Button5.Location = new Point(503, 64);
            Button5.Name = "Button5";
            Button5.Size = new Size(34, 24);
            Button5.TabIndex = 184;
            Button5.UseVisualStyleBackColor = false;
            // 
            // Label13
            // 
            Label13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label13.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Label13.Location = new Point(163, 150);
            Label13.Name = "Label13";
            Label13.Size = new Size(374, 24);
            Label13.TabIndex = 183;
            Label13.Text = "Images in this list must be from the same folder";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox3
            // 
            CheckBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox3.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox3.Checked = false;
            CheckBox3.Font = new Font("Segoe UI", 9.0f);
            CheckBox3.ForeColor = Color.White;
            CheckBox3.Location = new Point(36, 225);
            CheckBox3.Name = "CheckBox3";
            CheckBox3.Size = new Size(501, 24);
            CheckBox3.TabIndex = 182;
            CheckBox3.Text = "Shuffle";
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(6, 225);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 181;
            PictureBox7.TabStop = false;
            // 
            // PictureBox16
            // 
            PictureBox16.BackColor = Color.Transparent;
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(6, 195);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(24, 24);
            PictureBox16.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox16.TabIndex = 180;
            PictureBox16.TabStop = false;
            // 
            // MD
            // 
            MD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MD.BackColor = Color.FromArgb(34, 34, 34);
            MD.DrawOnGlass = false;
            MD.Font = new Font("Segoe UI", 9.0f);
            MD.ForeColor = Color.White;
            MD.Image = null;
            MD.LineColor = Color.FromArgb(0, 81, 210);
            MD.Location = new Point(473, 195);
            MD.Name = "MD";
            MD.Size = new Size(64, 24);
            MD.TabIndex = 179;
            MD.UseVisualStyleBackColor = false;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar1.LargeChange = 1000;
            Trackbar1.Location = new Point(157, 198);
            Trackbar1.Maximum = 36000000;
            Trackbar1.Minimum = 10000;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(310, 19);
            Trackbar1.SmallChange = 100;
            Trackbar1.TabIndex = 178;
            Trackbar1.Value = 10000;
            // 
            // Label11
            // 
            Label11.Location = new Point(36, 195);
            Label11.Name = "Label11";
            Label11.Size = new Size(119, 24);
            Label11.TabIndex = 176;
            Label11.Text = "Change every (ms):";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Separator3
            // 
            Separator3.AlternativeLook = false;
            Separator3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator3.Location = new Point(6, 182);
            Separator3.Name = "Separator3";
            Separator3.Size = new Size(531, 1);
            Separator3.TabIndex = 175;
            Separator3.TabStop = false;
            // 
            // RadioButton2
            // 
            RadioButton2.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Location = new Point(36, 36);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(119, 24);
            RadioButton2.TabIndex = 174;
            RadioButton2.Text = "List of images:";
            // 
            // RadioButton1
            // 
            RadioButton1.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton1.Checked = false;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Location = new Point(36, 6);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(119, 24);
            RadioButton1.TabIndex = 173;
            RadioButton1.Text = "Folder:";
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(6, 36);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 172;
            PictureBox6.TabStop = false;
            // 
            // Button17
            // 
            Button17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button17.BackColor = Color.FromArgb(34, 34, 34);
            Button17.DrawOnGlass = false;
            Button17.Font = new Font("Segoe UI", 9.0f);
            Button17.ForeColor = Color.White;
            Button17.Image = (Image)resources.GetObject("Button17.Image");
            Button17.LineColor = Color.FromArgb(151, 44, 52);
            Button17.Location = new Point(503, 120);
            Button17.Name = "Button17";
            Button17.Size = new Size(34, 24);
            Button17.TabIndex = 170;
            Button17.UseVisualStyleBackColor = false;
            // 
            // Button18
            // 
            Button18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button18.BackColor = Color.FromArgb(34, 34, 34);
            Button18.DrawOnGlass = false;
            Button18.Font = new Font("Segoe UI", 9.0f);
            Button18.ForeColor = Color.White;
            Button18.Image = (Image)resources.GetObject("Button18.Image");
            Button18.LineColor = Color.FromArgb(29, 103, 64);
            Button18.Location = new Point(503, 36);
            Button18.Name = "Button18";
            Button18.Size = new Size(34, 24);
            Button18.TabIndex = 169;
            Button18.UseVisualStyleBackColor = false;
            // 
            // ListBox1
            // 
            ListBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ListBox1.BackColor = Color.FromArgb(30, 30, 30);
            ListBox1.BorderStyle = BorderStyle.FixedSingle;
            ListBox1.ForeColor = Color.White;
            ListBox1.FormattingEnabled = true;
            ListBox1.ItemHeight = 15;
            ListBox1.Location = new Point(161, 36);
            ListBox1.Name = "ListBox1";
            ListBox1.SelectionMode = SelectionMode.MultiSimple;
            ListBox1.Size = new Size(335, 107);
            ListBox1.TabIndex = 168;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.LineColor = Color.FromArgb(184, 153, 68);
            Button4.Location = new Point(503, 6);
            Button4.Margin = new Padding(4, 3, 4, 3);
            Button4.Name = "Button4";
            Button4.Size = new Size(34, 24);
            Button4.TabIndex = 167;
            Button4.UseVisualStyleBackColor = false;
            // 
            // TextBox2
            // 
            TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(161, 6);
            TextBox2.MaxLength = 32767;
            TextBox2.Multiline = false;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = false;
            TextBox2.Scrollbars = ScrollBars.None;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(335, 24);
            TextBox2.TabIndex = 166;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(6, 6);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 165;
            PictureBox5.TabStop = false;
            // 
            // previewContainer
            // 
            previewContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            previewContainer.BackColor = Color.FromArgb(34, 34, 34);
            previewContainer.Controls.Add(Panel1);
            previewContainer.Controls.Add(pnl_preview);
            previewContainer.Controls.Add(PictureBox41);
            previewContainer.Controls.Add(Label19);
            previewContainer.Location = new Point(706, 61);
            previewContainer.Margin = new Padding(4, 3, 4, 3);
            previewContainer.Name = "previewContainer";
            previewContainer.Padding = new Padding(1);
            previewContainer.Size = new Size(536, 340);
            previewContainer.TabIndex = 215;
            // 
            // Panel1
            // 
            Panel1.Controls.Add(Label3);
            Panel1.Controls.Add(Button14);
            Panel1.Controls.Add(Button13);
            Panel1.Location = new Point(318, 4);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(214, 32);
            Panel1.TabIndex = 218;
            Panel1.Visible = false;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Location = new Point(3, 4);
            Label3.Name = "Label3";
            Label3.Size = new Size(130, 24);
            Label3.TabIndex = 177;
            Label3.Text = "0/0";
            Label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(34, 34, 34);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = (Image)resources.GetObject("Button14.Image");
            Button14.LineColor = Color.FromArgb(96, 42, 126);
            Button14.Location = new Point(139, 3);
            Button14.Name = "Button14";
            Button14.Size = new Size(34, 26);
            Button14.TabIndex = 171;
            Button14.UseVisualStyleBackColor = false;
            // 
            // Button13
            // 
            Button13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button13.BackColor = Color.FromArgb(34, 34, 34);
            Button13.DrawOnGlass = false;
            Button13.Font = new Font("Segoe UI", 9.0f);
            Button13.ForeColor = Color.White;
            Button13.Image = (Image)resources.GetObject("Button13.Image");
            Button13.LineColor = Color.FromArgb(96, 42, 126);
            Button13.Location = new Point(177, 3);
            Button13.Name = "Button13";
            Button13.Size = new Size(34, 26);
            Button13.TabIndex = 170;
            Button13.UseVisualStyleBackColor = false;
            // 
            // pnl_preview
            // 
            pnl_preview.Location = new Point(4, 39);
            pnl_preview.Name = "pnl_preview";
            pnl_preview.Size = new Size(528, 297);
            pnl_preview.TabIndex = 217;
            pnl_preview.TabStop = false;
            // 
            // PictureBox41
            // 
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(4, 4);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(35, 32);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 4;
            PictureBox41.TabStop = false;
            // 
            // Label19
            // 
            Label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label19.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label19.Location = new Point(45, 5);
            Label19.Name = "Label19";
            Label19.Size = new Size(270, 31);
            Label19.TabIndex = 3;
            Label19.Text = "Preview";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SaveFileDialog2
            // 
            SaveFileDialog2.DefaultExt = "wpt";
            SaveFileDialog2.Filter = "PNG File|*.png";
            // 
            // Wallpaper_Editor
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(1254, 457);
            Controls.Add(TabControl1);
            Controls.Add(previewContainer);
            Controls.Add(Button10);
            Controls.Add(Button7);
            Controls.Add(Button8);
            Controls.Add(GroupBox12);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Wallpaper_Editor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wallpaper";
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Panel2.ResumeLayout(false);
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            previewContainer.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnl_preview).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            Load += new EventHandler(Wallpaper_Editor_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

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