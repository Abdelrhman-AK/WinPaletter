using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ColorPickerDlg : BorderlessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPickerDlg));
            this.ColorEditorManager1 = new Cyotek.Windows.Forms.ColorEditorManager();
            this.ColorEditor1 = new Cyotek.Windows.Forms.ColorEditor();
            this.ColorGrid1 = new Cyotek.Windows.Forms.ColorGrid();
            this.ColorWheel1 = new Cyotek.Windows.Forms.ColorWheel();
            this.ScreenColorPicker1 = new Cyotek.Windows.Forms.ScreenColorPicker();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.ProgressBar1 = new WinPaletter.UI.WP.ProgressBar();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.ImgPaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.Label4 = new System.Windows.Forms.Label();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.TabControl1 = new WinPaletter.UI.WP.TabControl();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.effect_MaterialExpressive = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox26 = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.effect_Material = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox23 = new System.Windows.Forms.PictureBox();
            this.label22 = new System.Windows.Forms.Label();
            this.effect_macOS = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox25 = new System.Windows.Forms.PictureBox();
            this.label24 = new System.Windows.Forms.Label();
            this.effect_brightness = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox24 = new System.Windows.Forms.PictureBox();
            this.trackBarX3 = new WinPaletter.UI.Controllers.TrackBarX();
            this.label23 = new System.Windows.Forms.Label();
            this.effect_analogus_previous = new WinPaletter.UI.Controllers.ColorItem();
            this.effect_sepia = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox22 = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.effect_grayscale = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox21 = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.effect_monochrome = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.effect_256 = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox19 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.effect_invert = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.effect_rotateHue = new WinPaletter.UI.Controllers.ColorItem();
            this.effect_desaturate = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.trackBar5 = new WinPaletter.UI.Controllers.TrackBarX();
            this.trackBar4 = new WinPaletter.UI.Controllers.TrackBarX();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.effect_analogus_next = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.trackBar3 = new WinPaletter.UI.Controllers.TrackBarX();
            this.label13 = new System.Windows.Forms.Label();
            this.effect_light = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.trackBar2 = new WinPaletter.UI.Controllers.TrackBarX();
            this.label12 = new System.Windows.Forms.Label();
            this.effect_dark = new WinPaletter.UI.Controllers.ColorItem();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.TabPage9 = new System.Windows.Forms.TabPage();
            this.Label5 = new System.Windows.Forms.Label();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.TabControl2 = new WinPaletter.UI.WP.TabControl();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.trackBarX2 = new WinPaletter.UI.Controllers.TrackBarX();
            this.trackBarX1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.PictureBox12 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.CheckBox2 = new WinPaletter.UI.WP.CheckBox();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioImage();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioImage();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.ComboBox1 = new WinPaletter.UI.WP.ComboBox();
            this.PictureBox33 = new System.Windows.Forms.PictureBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.TextBox2 = new WinPaletter.UI.WP.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.ThemePaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.ComboBox2 = new WinPaletter.UI.WP.ComboBox();
            this.PaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.Label9 = new System.Windows.Forms.Label();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage5.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.TabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.TabControl2.SuspendLayout();
            this.TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).BeginInit();
            this.TabPage8.SuspendLayout();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // ColorEditorManager1
            // 
            this.ColorEditorManager1.Color = System.Drawing.Color.Empty;
            this.ColorEditorManager1.ColorEditor = this.ColorEditor1;
            this.ColorEditorManager1.ColorGrid = this.ColorGrid1;
            this.ColorEditorManager1.ColorWheel = this.ColorWheel1;
            this.ColorEditorManager1.ScreenColorPicker = this.ScreenColorPicker1;
            this.ColorEditorManager1.ColorChanged += new System.EventHandler(this.Change_Color_Preview);
            // 
            // ColorEditor1
            // 
            this.ColorEditor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ColorEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorEditor1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorEditor1.Location = new System.Drawing.Point(3, 3);
            this.ColorEditor1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorEditor1.Name = "ColorEditor1";
            this.ColorEditor1.Size = new System.Drawing.Size(282, 258);
            this.ColorEditor1.TabIndex = 0;
            // 
            // ColorGrid1
            // 
            this.ColorGrid1.AutoAddColors = false;
            this.ColorGrid1.CellBorderColor = System.Drawing.Color.DimGray;
            this.ColorGrid1.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
            this.ColorGrid1.CellSize = new System.Drawing.Size(15, 15);
            this.ColorGrid1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ColorGrid1.Columns = 15;
            this.ColorGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorGrid1.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.ColorGrid1.Location = new System.Drawing.Point(3, 3);
            this.ColorGrid1.Name = "ColorGrid1";
            this.ColorGrid1.Size = new System.Drawing.Size(282, 232);
            this.ColorGrid1.TabIndex = 1;
            // 
            // ColorWheel1
            // 
            this.ColorWheel1.Alpha = 1D;
            this.ColorWheel1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ColorWheel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorWheel1.Location = new System.Drawing.Point(3, 3);
            this.ColorWheel1.Name = "ColorWheel1";
            this.ColorWheel1.Size = new System.Drawing.Size(282, 258);
            this.ColorWheel1.TabIndex = 2;
            // 
            // ScreenColorPicker1
            // 
            this.ScreenColorPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenColorPicker1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ScreenColorPicker1.Location = new System.Drawing.Point(7, 7);
            this.ScreenColorPicker1.Name = "ScreenColorPicker1";
            this.ScreenColorPicker1.Size = new System.Drawing.Size(132, 34);
            this.ScreenColorPicker1.Text = "Drag and release for a screen pixel color pick";
            this.ScreenColorPicker1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScreenColorPicker1_MouseDown);
            this.ScreenColorPicker1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScreenColorPicker1_MouseUp);
            // 
            // Button6
            // 
            this.Button6.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(112)))), ((int)(((byte)(200)))));
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = null;
            this.Button6.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Play;
            this.Button6.ImageGlyphEnabled = true;
            this.Button6.Location = new System.Drawing.Point(202, 2);
            this.Button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(75, 26);
            this.Button6.TabIndex = 28;
            this.Button6.Text = "Extract";
            this.Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(33, 118);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(110, 24);
            this.Label7.TabIndex = 27;
            this.Label7.Text = "Quality";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(3, 118);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 26;
            this.PictureBox9.TabStop = false;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Checked = true;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(33, 173);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(244, 24);
            this.CheckBox1.TabIndex = 9;
            this.CheckBox1.Text = "Ignore white colors";
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(33, 60);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(110, 24);
            this.Label6.TabIndex = 23;
            this.Label6.Text = "Maximum colors";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(3, 173);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 22;
            this.PictureBox7.TabStop = false;
            // 
            // PictureBox8
            // 
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(3, 60);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 21;
            this.PictureBox8.TabStop = false;
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Appearance = WinPaletter.UI.WP.ProgressBar.ProgressBarAppearance.Bar;
            this.ProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBar1.Location = new System.Drawing.Point(3, 30);
            this.ProgressBar1.MarqueeAnimationSpeed = 20;
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(274, 6);
            this.ProgressBar1.State = WinPaletter.UI.WP.ProgressBar.ProgressBarState.Normal;
            this.ProgressBar1.Step = 1;
            this.ProgressBar1.Style = WinPaletter.UI.WP.ProgressBar.ProgressBarStyle.Marquee;
            this.ProgressBar1.TabIndex = 9;
            this.ProgressBar1.TaskbarBroadcast = true;
            this.ProgressBar1.Visible = false;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(3, 3);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 16;
            this.PictureBox5.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(33, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(168, 24);
            this.Label3.TabIndex = 15;
            this.Label3.Text = "Palette";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button4
            // 
            this.Button4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button4.ImageGlyphEnabled = true;
            this.Button4.Location = new System.Drawing.Point(243, 33);
            this.Button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(34, 24);
            this.Button4.TabIndex = 10;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click_1);
            // 
            // TextBox1
            // 
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(86, 33);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(150, 24);
            this.TextBox1.TabIndex = 9;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            // 
            // ImgPaletteContainer
            // 
            this.ImgPaletteContainer.AutoScroll = true;
            this.ImgPaletteContainer.Location = new System.Drawing.Point(3, 30);
            this.ImgPaletteContainer.Name = "ImgPaletteContainer";
            this.ImgPaletteContainer.Size = new System.Drawing.Size(274, 197);
            this.ImgPaletteContainer.TabIndex = 12;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(3, 30);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(274, 197);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "Extracting palette from image depends on your device\'s performance, maximum palet" +
    "te colors number, image quality and its resolution ...";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.Button3.Location = new System.Drawing.Point(157, 7);
            this.Button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(80, 34);
            this.Button3.TabIndex = 5;
            this.Button3.Text = "Cancel";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
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
            this.Button2.Location = new System.Drawing.Point(245, 7);
            this.Button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 4;
            this.Button2.Text = "Select";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TabControl1
            // 
            this.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabControl1.AllowDrop = true;
            this.TabControl1.Controls.Add(this.TabPage5);
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage6);
            this.TabControl1.Controls.Add(this.tabPage10);
            this.TabControl1.Controls.Add(this.TabPage9);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TabControl1.ImageList = this.ImageList1;
            this.TabControl1.ItemSize = new System.Drawing.Size(30, 36);
            this.TabControl1.Location = new System.Drawing.Point(1, 1);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(332, 272);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.TabIndex = 9;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage5.Controls.Add(this.ColorEditor1);
            this.TabPage5.Location = new System.Drawing.Point(40, 4);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(288, 264);
            this.TabPage5.TabIndex = 4;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage1.Controls.Add(this.ColorGrid1);
            this.TabPage1.Controls.Add(this.Button1);
            this.TabPage1.Location = new System.Drawing.Point(40, 4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(288, 264);
            this.TabPage1.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(3, 235);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(282, 26);
            this.Button1.TabIndex = 5;
            this.Button1.Text = "Open from app palette (e.g. Photoshop)";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage6.Controls.Add(this.ColorWheel1);
            this.TabPage6.Location = new System.Drawing.Point(40, 4);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage6.Size = new System.Drawing.Size(288, 264);
            this.TabPage6.TabIndex = 5;
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage10.Controls.Add(this.panel1);
            this.tabPage10.Controls.Add(this.label10);
            this.tabPage10.Controls.Add(this.pictureBox4);
            this.tabPage10.Location = new System.Drawing.Point(40, 4);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(288, 264);
            this.tabPage10.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.effect_MaterialExpressive);
            this.panel1.Controls.Add(this.pictureBox26);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.effect_Material);
            this.panel1.Controls.Add(this.pictureBox23);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.effect_macOS);
            this.panel1.Controls.Add(this.pictureBox25);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.effect_brightness);
            this.panel1.Controls.Add(this.pictureBox24);
            this.panel1.Controls.Add(this.trackBarX3);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.effect_analogus_previous);
            this.panel1.Controls.Add(this.effect_sepia);
            this.panel1.Controls.Add(this.pictureBox22);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.effect_grayscale);
            this.panel1.Controls.Add(this.pictureBox21);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.effect_monochrome);
            this.panel1.Controls.Add(this.pictureBox20);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.effect_256);
            this.panel1.Controls.Add(this.pictureBox19);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.effect_invert);
            this.panel1.Controls.Add(this.pictureBox17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.effect_rotateHue);
            this.panel1.Controls.Add(this.effect_desaturate);
            this.panel1.Controls.Add(this.pictureBox15);
            this.panel1.Controls.Add(this.pictureBox16);
            this.panel1.Controls.Add(this.trackBar5);
            this.panel1.Controls.Add(this.trackBar4);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.effect_analogus_next);
            this.panel1.Controls.Add(this.pictureBox14);
            this.panel1.Controls.Add(this.trackBar3);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.effect_light);
            this.panel1.Controls.Add(this.pictureBox13);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.effect_dark);
            this.panel1.Controls.Add(this.pictureBox6);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(6, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 225);
            this.panel1.TabIndex = 56;
            // 
            // effect_MaterialExpressive
            // 
            this.effect_MaterialExpressive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_MaterialExpressive.BackColor = System.Drawing.Color.Transparent;
            this.effect_MaterialExpressive.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_MaterialExpressive.DontShowInfo = false;
            this.effect_MaterialExpressive.Location = new System.Drawing.Point(176, 506);
            this.effect_MaterialExpressive.Name = "effect_MaterialExpressive";
            this.effect_MaterialExpressive.Size = new System.Drawing.Size(100, 24);
            this.effect_MaterialExpressive.TabIndex = 93;
            this.effect_MaterialExpressive.Text = "colorItem8";
            this.effect_MaterialExpressive.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox26
            // 
            this.pictureBox26.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox26.Image")));
            this.pictureBox26.Location = new System.Drawing.Point(3, 506);
            this.pictureBox26.Name = "pictureBox26";
            this.pictureBox26.Size = new System.Drawing.Size(24, 24);
            this.pictureBox26.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox26.TabIndex = 92;
            this.pictureBox26.TabStop = false;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(33, 506);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(135, 24);
            this.label25.TabIndex = 91;
            this.label25.Text = "Material (2025) color:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_Material
            // 
            this.effect_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_Material.BackColor = System.Drawing.Color.Transparent;
            this.effect_Material.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_Material.DontShowInfo = false;
            this.effect_Material.Location = new System.Drawing.Point(176, 476);
            this.effect_Material.Name = "effect_Material";
            this.effect_Material.Size = new System.Drawing.Size(100, 24);
            this.effect_Material.TabIndex = 90;
            this.effect_Material.Text = "colorItem8";
            this.effect_Material.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox23
            // 
            this.pictureBox23.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox23.Image")));
            this.pictureBox23.Location = new System.Drawing.Point(3, 476);
            this.pictureBox23.Name = "pictureBox23";
            this.pictureBox23.Size = new System.Drawing.Size(24, 24);
            this.pictureBox23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox23.TabIndex = 89;
            this.pictureBox23.TabStop = false;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(33, 476);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(135, 24);
            this.label22.TabIndex = 88;
            this.label22.Text = "Material (2015) color:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_macOS
            // 
            this.effect_macOS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_macOS.BackColor = System.Drawing.Color.Transparent;
            this.effect_macOS.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_macOS.DontShowInfo = false;
            this.effect_macOS.Location = new System.Drawing.Point(176, 446);
            this.effect_macOS.Name = "effect_macOS";
            this.effect_macOS.Size = new System.Drawing.Size(100, 24);
            this.effect_macOS.TabIndex = 87;
            this.effect_macOS.Text = "colorItem7";
            this.effect_macOS.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox25
            // 
            this.pictureBox25.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox25.Image")));
            this.pictureBox25.Location = new System.Drawing.Point(3, 446);
            this.pictureBox25.Name = "pictureBox25";
            this.pictureBox25.Size = new System.Drawing.Size(24, 24);
            this.pictureBox25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox25.TabIndex = 86;
            this.pictureBox25.TabStop = false;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(33, 446);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(135, 24);
            this.label24.TabIndex = 85;
            this.label24.Text = "macOS color:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_brightness
            // 
            this.effect_brightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_brightness.BackColor = System.Drawing.Color.Transparent;
            this.effect_brightness.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_brightness.DontShowInfo = false;
            this.effect_brightness.Location = new System.Drawing.Point(176, 30);
            this.effect_brightness.Name = "effect_brightness";
            this.effect_brightness.Size = new System.Drawing.Size(100, 24);
            this.effect_brightness.TabIndex = 84;
            this.effect_brightness.Text = "colorItem1";
            this.effect_brightness.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox24
            // 
            this.pictureBox24.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox24.Image")));
            this.pictureBox24.Location = new System.Drawing.Point(3, 3);
            this.pictureBox24.Name = "pictureBox24";
            this.pictureBox24.Size = new System.Drawing.Size(24, 24);
            this.pictureBox24.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox24.TabIndex = 82;
            this.pictureBox24.TabStop = false;
            // 
            // trackBarX3
            // 
            this.trackBarX3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX3.AnimateChanges = true;
            this.trackBarX3.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX3.DefaultValue = 0;
            this.trackBarX3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX3.Location = new System.Drawing.Point(4, 30);
            this.trackBarX3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX3.Maximum = 100;
            this.trackBarX3.Minimum = 0;
            this.trackBarX3.Name = "trackBarX3";
            this.trackBarX3.Size = new System.Drawing.Size(165, 24);
            this.trackBarX3.TabIndex = 83;
            this.trackBarX3.Value = 50;
            this.trackBarX3.ValueChanged += new System.EventHandler(this.trackBarX3_ValueChanged);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(33, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(224, 24);
            this.label23.TabIndex = 81;
            this.label23.Text = "Change brightness:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_analogus_previous
            // 
            this.effect_analogus_previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_analogus_previous.BackColor = System.Drawing.Color.Transparent;
            this.effect_analogus_previous.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_analogus_previous.DontShowInfo = false;
            this.effect_analogus_previous.Location = new System.Drawing.Point(70, 218);
            this.effect_analogus_previous.Name = "effect_analogus_previous";
            this.effect_analogus_previous.Size = new System.Drawing.Size(100, 24);
            this.effect_analogus_previous.TabIndex = 80;
            this.effect_analogus_previous.Text = "colorItem3";
            this.effect_analogus_previous.Click += new System.EventHandler(this.effects_Click);
            // 
            // effect_sepia
            // 
            this.effect_sepia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_sepia.BackColor = System.Drawing.Color.Transparent;
            this.effect_sepia.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_sepia.DontShowInfo = false;
            this.effect_sepia.Location = new System.Drawing.Point(176, 386);
            this.effect_sepia.Name = "effect_sepia";
            this.effect_sepia.Size = new System.Drawing.Size(100, 24);
            this.effect_sepia.TabIndex = 76;
            this.effect_sepia.Text = "colorItem11";
            this.effect_sepia.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox22
            // 
            this.pictureBox22.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox22.Image")));
            this.pictureBox22.Location = new System.Drawing.Point(3, 386);
            this.pictureBox22.Name = "pictureBox22";
            this.pictureBox22.Size = new System.Drawing.Size(24, 24);
            this.pictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox22.TabIndex = 75;
            this.pictureBox22.TabStop = false;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(33, 386);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(135, 24);
            this.label21.TabIndex = 74;
            this.label21.Text = "Sepia:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_grayscale
            // 
            this.effect_grayscale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_grayscale.BackColor = System.Drawing.Color.Transparent;
            this.effect_grayscale.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_grayscale.DontShowInfo = false;
            this.effect_grayscale.Location = new System.Drawing.Point(176, 566);
            this.effect_grayscale.Name = "effect_grayscale";
            this.effect_grayscale.Size = new System.Drawing.Size(100, 24);
            this.effect_grayscale.TabIndex = 73;
            this.effect_grayscale.Text = "colorItem10";
            this.effect_grayscale.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox21
            // 
            this.pictureBox21.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox21.Image")));
            this.pictureBox21.Location = new System.Drawing.Point(3, 566);
            this.pictureBox21.Name = "pictureBox21";
            this.pictureBox21.Size = new System.Drawing.Size(24, 24);
            this.pictureBox21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox21.TabIndex = 72;
            this.pictureBox21.TabStop = false;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(33, 566);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(135, 24);
            this.label20.TabIndex = 71;
            this.label20.Text = "Grayscale:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_monochrome
            // 
            this.effect_monochrome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_monochrome.BackColor = System.Drawing.Color.Transparent;
            this.effect_monochrome.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_monochrome.DontShowInfo = false;
            this.effect_monochrome.Location = new System.Drawing.Point(176, 536);
            this.effect_monochrome.Name = "effect_monochrome";
            this.effect_monochrome.Size = new System.Drawing.Size(100, 24);
            this.effect_monochrome.TabIndex = 70;
            this.effect_monochrome.Text = "colorItem9";
            this.effect_monochrome.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox20
            // 
            this.pictureBox20.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox20.Image")));
            this.pictureBox20.Location = new System.Drawing.Point(3, 536);
            this.pictureBox20.Name = "pictureBox20";
            this.pictureBox20.Size = new System.Drawing.Size(24, 24);
            this.pictureBox20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox20.TabIndex = 69;
            this.pictureBox20.TabStop = false;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(33, 536);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(135, 24);
            this.label19.TabIndex = 68;
            this.label19.Text = "Monochrome:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_256
            // 
            this.effect_256.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_256.BackColor = System.Drawing.Color.Transparent;
            this.effect_256.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_256.DontShowInfo = false;
            this.effect_256.Location = new System.Drawing.Point(176, 416);
            this.effect_256.Name = "effect_256";
            this.effect_256.Size = new System.Drawing.Size(100, 24);
            this.effect_256.TabIndex = 67;
            this.effect_256.Text = "colorItem8";
            this.effect_256.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox19
            // 
            this.pictureBox19.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox19.Image")));
            this.pictureBox19.Location = new System.Drawing.Point(3, 416);
            this.pictureBox19.Name = "pictureBox19";
            this.pictureBox19.Size = new System.Drawing.Size(24, 24);
            this.pictureBox19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox19.TabIndex = 66;
            this.pictureBox19.TabStop = false;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(33, 416);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(135, 24);
            this.label18.TabIndex = 65;
            this.label18.Text = "256\'s color:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_invert
            // 
            this.effect_invert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_invert.BackColor = System.Drawing.Color.Transparent;
            this.effect_invert.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_invert.DontShowInfo = false;
            this.effect_invert.Location = new System.Drawing.Point(176, 356);
            this.effect_invert.Name = "effect_invert";
            this.effect_invert.Size = new System.Drawing.Size(100, 24);
            this.effect_invert.TabIndex = 61;
            this.effect_invert.Text = "colorItem6";
            this.effect_invert.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox17
            // 
            this.pictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox17.Image")));
            this.pictureBox17.Location = new System.Drawing.Point(3, 356);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(24, 24);
            this.pictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox17.TabIndex = 60;
            this.pictureBox17.TabStop = false;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(33, 356);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(135, 24);
            this.label16.TabIndex = 59;
            this.label16.Text = "Invert:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_rotateHue
            // 
            this.effect_rotateHue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_rotateHue.BackColor = System.Drawing.Color.Transparent;
            this.effect_rotateHue.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_rotateHue.DontShowInfo = false;
            this.effect_rotateHue.Location = new System.Drawing.Point(176, 326);
            this.effect_rotateHue.Name = "effect_rotateHue";
            this.effect_rotateHue.Size = new System.Drawing.Size(100, 24);
            this.effect_rotateHue.TabIndex = 58;
            this.effect_rotateHue.Text = "colorItem5";
            this.effect_rotateHue.Click += new System.EventHandler(this.effects_Click);
            // 
            // effect_desaturate
            // 
            this.effect_desaturate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_desaturate.BackColor = System.Drawing.Color.Transparent;
            this.effect_desaturate.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_desaturate.DontShowInfo = false;
            this.effect_desaturate.Location = new System.Drawing.Point(176, 272);
            this.effect_desaturate.Name = "effect_desaturate";
            this.effect_desaturate.Size = new System.Drawing.Size(100, 24);
            this.effect_desaturate.TabIndex = 54;
            this.effect_desaturate.Text = "colorItem4";
            this.effect_desaturate.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox15
            // 
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(3, 245);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(24, 24);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox15.TabIndex = 52;
            this.pictureBox15.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox16.Image")));
            this.pictureBox16.Location = new System.Drawing.Point(3, 299);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(24, 24);
            this.pictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox16.TabIndex = 56;
            this.pictureBox16.TabStop = false;
            // 
            // trackBar5
            // 
            this.trackBar5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar5.AnimateChanges = true;
            this.trackBar5.BackColor = System.Drawing.Color.Transparent;
            this.trackBar5.DefaultValue = 0;
            this.trackBar5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBar5.Location = new System.Drawing.Point(4, 326);
            this.trackBar5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar5.Maximum = 360;
            this.trackBar5.Minimum = 0;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(165, 24);
            this.trackBar5.TabIndex = 57;
            this.trackBar5.Value = 180;
            this.trackBar5.ValueChanged += new System.EventHandler(this.trackBar5_ValueChanged);
            // 
            // trackBar4
            // 
            this.trackBar4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar4.AnimateChanges = true;
            this.trackBar4.BackColor = System.Drawing.Color.Transparent;
            this.trackBar4.DefaultValue = 0;
            this.trackBar4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBar4.Location = new System.Drawing.Point(4, 272);
            this.trackBar4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar4.Maximum = 100;
            this.trackBar4.Minimum = 0;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(165, 24);
            this.trackBar4.TabIndex = 53;
            this.trackBar4.Value = 50;
            this.trackBar4.ValueChanged += new System.EventHandler(this.trackBar4_ValueChanged);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(33, 245);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(224, 24);
            this.label14.TabIndex = 51;
            this.label14.Text = "Desaturate:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(33, 299);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(224, 24);
            this.label15.TabIndex = 55;
            this.label15.Text = "Rotate hue:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_analogus_next
            // 
            this.effect_analogus_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_analogus_next.BackColor = System.Drawing.Color.Transparent;
            this.effect_analogus_next.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_analogus_next.DontShowInfo = false;
            this.effect_analogus_next.Location = new System.Drawing.Point(176, 218);
            this.effect_analogus_next.Name = "effect_analogus_next";
            this.effect_analogus_next.Size = new System.Drawing.Size(100, 24);
            this.effect_analogus_next.TabIndex = 50;
            this.effect_analogus_next.Text = "colorItem3";
            this.effect_analogus_next.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox14
            // 
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.Location = new System.Drawing.Point(3, 163);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(24, 24);
            this.pictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox14.TabIndex = 48;
            this.pictureBox14.TabStop = false;
            // 
            // trackBar3
            // 
            this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar3.AnimateChanges = true;
            this.trackBar3.BackColor = System.Drawing.Color.Transparent;
            this.trackBar3.DefaultValue = 0;
            this.trackBar3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBar3.Location = new System.Drawing.Point(4, 190);
            this.trackBar3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar3.Maximum = 360;
            this.trackBar3.Minimum = 0;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(271, 24);
            this.trackBar3.TabIndex = 49;
            this.trackBar3.Value = 180;
            this.trackBar3.ValueChanged += new System.EventHandler(this.trackBar3_ValueChanged);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(33, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(224, 24);
            this.label13.TabIndex = 47;
            this.label13.Text = "Analogus:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_light
            // 
            this.effect_light.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_light.BackColor = System.Drawing.Color.Transparent;
            this.effect_light.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_light.DontShowInfo = false;
            this.effect_light.Location = new System.Drawing.Point(176, 136);
            this.effect_light.Name = "effect_light";
            this.effect_light.Size = new System.Drawing.Size(100, 24);
            this.effect_light.TabIndex = 46;
            this.effect_light.Text = "colorItem2";
            this.effect_light.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox13
            // 
            this.pictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox13.Image")));
            this.pictureBox13.Location = new System.Drawing.Point(3, 109);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(24, 24);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox13.TabIndex = 44;
            this.pictureBox13.TabStop = false;
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.AnimateChanges = true;
            this.trackBar2.BackColor = System.Drawing.Color.Transparent;
            this.trackBar2.DefaultValue = 0;
            this.trackBar2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBar2.Location = new System.Drawing.Point(4, 136);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Minimum = 0;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(165, 24);
            this.trackBar2.TabIndex = 45;
            this.trackBar2.Value = 50;
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(33, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(224, 24);
            this.label12.TabIndex = 43;
            this.label12.Text = "Light:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effect_dark
            // 
            this.effect_dark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effect_dark.BackColor = System.Drawing.Color.Transparent;
            this.effect_dark.DefaultBackColor = System.Drawing.Color.Black;
            this.effect_dark.DontShowInfo = false;
            this.effect_dark.Location = new System.Drawing.Point(176, 84);
            this.effect_dark.Name = "effect_dark";
            this.effect_dark.Size = new System.Drawing.Size(100, 24);
            this.effect_dark.TabIndex = 42;
            this.effect_dark.Text = "colorItem1";
            this.effect_dark.Click += new System.EventHandler(this.effects_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(3, 57);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(24, 24);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox6.TabIndex = 40;
            this.pictureBox6.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AnimateChanges = true;
            this.trackBar1.BackColor = System.Drawing.Color.Transparent;
            this.trackBar1.DefaultValue = 0;
            this.trackBar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBar1.Location = new System.Drawing.Point(4, 84);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 0;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(165, 24);
            this.trackBar1.TabIndex = 41;
            this.trackBar1.Value = 50;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(33, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(224, 24);
            this.label11.TabIndex = 39;
            this.label11.Text = "Dark:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(36, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(249, 24);
            this.label10.TabIndex = 37;
            this.label10.Text = "Colors effects:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(6, 6);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 38;
            this.pictureBox4.TabStop = false;
            // 
            // TabPage9
            // 
            this.TabPage9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage9.Controls.Add(this.Label5);
            this.TabPage9.Controls.Add(this.PictureBox3);
            this.TabPage9.Controls.Add(this.FlowLayoutPanel1);
            this.TabPage9.Location = new System.Drawing.Point(40, 4);
            this.TabPage9.Name = "TabPage9";
            this.TabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage9.Size = new System.Drawing.Size(288, 264);
            this.TabPage9.TabIndex = 6;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(36, 6);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(249, 24);
            this.Label5.TabIndex = 35;
            this.Label5.Text = "History for current color item:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(6, 6);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 36;
            this.PictureBox3.TabStop = false;
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.AutoScroll = true;
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(5, 36);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(278, 222);
            this.FlowLayoutPanel1.TabIndex = 34;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage2.Controls.Add(this.TabControl2);
            this.TabPage2.ForeColor = System.Drawing.Color.White;
            this.TabPage2.Location = new System.Drawing.Point(40, 4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(288, 264);
            this.TabPage2.TabIndex = 1;
            // 
            // TabControl2
            // 
            this.TabControl2.Controls.Add(this.TabPage7);
            this.TabControl2.Controls.Add(this.TabPage8);
            this.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TabControl2.ItemSize = new System.Drawing.Size(80, 26);
            this.TabControl2.Location = new System.Drawing.Point(0, 0);
            this.TabControl2.Name = "TabControl2";
            this.TabControl2.SelectedIndex = 0;
            this.TabControl2.Size = new System.Drawing.Size(288, 264);
            this.TabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl2.TabIndex = 134;
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage7.Controls.Add(this.trackBarX2);
            this.TabPage7.Controls.Add(this.trackBarX1);
            this.TabPage7.Controls.Add(this.PictureBox2);
            this.TabPage7.Controls.Add(this.PictureBox12);
            this.TabPage7.Controls.Add(this.Label2);
            this.TabPage7.Controls.Add(this.CheckBox2);
            this.TabPage7.Controls.Add(this.RadioButton2);
            this.TabPage7.Controls.Add(this.RadioButton1);
            this.TabPage7.Controls.Add(this.PictureBox8);
            this.TabPage7.Controls.Add(this.Button4);
            this.TabPage7.Controls.Add(this.PictureBox7);
            this.TabPage7.Controls.Add(this.Label7);
            this.TabPage7.Controls.Add(this.TextBox1);
            this.TabPage7.Controls.Add(this.PictureBox9);
            this.TabPage7.Controls.Add(this.Label6);
            this.TabPage7.Controls.Add(this.CheckBox1);
            this.TabPage7.Location = new System.Drawing.Point(4, 30);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(280, 230);
            this.TabPage7.TabIndex = 0;
            this.TabPage7.Text = "Options";
            // 
            // trackBarX2
            // 
            this.trackBarX2.AnimateChanges = true;
            this.trackBarX2.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX2.DefaultValue = 10;
            this.trackBarX2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX2.Location = new System.Drawing.Point(36, 145);
            this.trackBarX2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX2.Maximum = 100;
            this.trackBarX2.Minimum = 0;
            this.trackBarX2.Name = "trackBarX2";
            this.trackBarX2.Size = new System.Drawing.Size(241, 24);
            this.trackBarX2.TabIndex = 138;
            this.trackBarX2.Value = 10;
            // 
            // trackBarX1
            // 
            this.trackBarX1.AnimateChanges = true;
            this.trackBarX1.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX1.DefaultValue = 15;
            this.trackBarX1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX1.Location = new System.Drawing.Point(36, 87);
            this.trackBarX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX1.Maximum = 100;
            this.trackBarX1.Minimum = 5;
            this.trackBarX1.Name = "trackBarX1";
            this.trackBarX1.Size = new System.Drawing.Size(241, 24);
            this.trackBarX1.TabIndex = 137;
            this.trackBarX1.Value = 15;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(3, 3);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 136;
            this.PictureBox2.TabStop = false;
            // 
            // PictureBox12
            // 
            this.PictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox12.Image")));
            this.PictureBox12.Location = new System.Drawing.Point(3, 203);
            this.PictureBox12.Name = "PictureBox12";
            this.PictureBox12.Size = new System.Drawing.Size(24, 24);
            this.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox12.TabIndex = 133;
            this.PictureBox12.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(33, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(47, 24);
            this.Label2.TabIndex = 135;
            this.Label2.Text = "Source";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckBox2
            // 
            this.CheckBox2.Checked = true;
            this.CheckBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox2.ForeColor = System.Drawing.Color.White;
            this.CheckBox2.Location = new System.Drawing.Point(33, 203);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(244, 24);
            this.CheckBox2.TabIndex = 132;
            this.CheckBox2.Text = "Accelerate extraction process";
            // 
            // RadioButton2
            // 
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Image = null;
            this.RadioButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.Location = new System.Drawing.Point(211, 3);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(66, 24);
            this.RadioButton2.TabIndex = 133;
            this.RadioButton2.Text = "Image";
            this.RadioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // RadioButton1
            // 
            this.RadioButton1.Checked = true;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Image = null;
            this.RadioButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.Location = new System.Drawing.Point(86, 3);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(119, 24);
            this.RadioButton1.TabIndex = 132;
            this.RadioButton1.Text = "Current wallpaper";
            this.RadioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage8.Controls.Add(this.PictureBox5);
            this.TabPage8.Controls.Add(this.Button6);
            this.TabPage8.Controls.Add(this.ProgressBar1);
            this.TabPage8.Controls.Add(this.Label3);
            this.TabPage8.Controls.Add(this.ImgPaletteContainer);
            this.TabPage8.Controls.Add(this.Label4);
            this.TabPage8.Location = new System.Drawing.Point(4, 30);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(280, 230);
            this.TabPage8.TabIndex = 1;
            this.TabPage8.Text = "Result";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.Button7);
            this.TabPage3.Controls.Add(this.PictureBox10);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.ComboBox1);
            this.TabPage3.Controls.Add(this.PictureBox33);
            this.TabPage3.Controls.Add(this.Label29);
            this.TabPage3.Controls.Add(this.TextBox2);
            this.TabPage3.Controls.Add(this.Label1);
            this.TabPage3.Controls.Add(this.PictureBox1);
            this.TabPage3.Controls.Add(this.ThemePaletteContainer);
            this.TabPage3.Location = new System.Drawing.Point(40, 4);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(288, 264);
            this.TabPage3.TabIndex = 2;
            // 
            // Button7
            // 
            this.Button7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button7.ImageGlyphEnabled = true;
            this.Button7.Location = new System.Drawing.Point(247, 36);
            this.Button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(35, 24);
            this.Button7.TabIndex = 30;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // PictureBox10
            // 
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(6, 36);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(24, 24);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 82;
            this.PictureBox10.TabStop = false;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(36, 36);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(50, 24);
            this.Label8.TabIndex = 79;
            this.Label8.Text = "File:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComboBox1
            // 
            this.ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ComboBox1.ForeColor = System.Drawing.Color.White;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.ItemHeight = 20;
            this.ComboBox1.Location = new System.Drawing.Point(92, 65);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(190, 26);
            this.ComboBox1.TabIndex = 77;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // PictureBox33
            // 
            this.PictureBox33.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox33.Image")));
            this.PictureBox33.Location = new System.Drawing.Point(6, 66);
            this.PictureBox33.Name = "PictureBox33";
            this.PictureBox33.Size = new System.Drawing.Size(24, 24);
            this.PictureBox33.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox33.TabIndex = 76;
            this.PictureBox33.TabStop = false;
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.Transparent;
            this.Label29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(36, 66);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(50, 24);
            this.Label29.TabIndex = 75;
            this.Label29.Text = "Preset:";
            this.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox2
            // 
            this.TextBox2.ForeColor = System.Drawing.Color.White;
            this.TextBox2.Location = new System.Drawing.Point(92, 36);
            this.TextBox2.MaxLength = 32767;
            this.TextBox2.Multiline = false;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = false;
            this.TextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox2.SelectedText = "";
            this.TextBox2.SelectionLength = 0;
            this.TextBox2.SelectionStart = 0;
            this.TextBox2.Size = new System.Drawing.Size(148, 24);
            this.TextBox2.TabIndex = 29;
            this.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox2.UseSystemPasswordChar = false;
            this.TextBox2.WordWrap = true;
            this.TextBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(36, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(249, 24);
            this.Label1.TabIndex = 32;
            this.Label1.Text = "Theme\\Visual Styles or presets to palette";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(6, 6);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 33;
            this.PictureBox1.TabStop = false;
            // 
            // ThemePaletteContainer
            // 
            this.ThemePaletteContainer.AutoScroll = true;
            this.ThemePaletteContainer.Location = new System.Drawing.Point(6, 97);
            this.ThemePaletteContainer.Name = "ThemePaletteContainer";
            this.ThemePaletteContainer.Size = new System.Drawing.Size(278, 161);
            this.ThemePaletteContainer.TabIndex = 31;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage4.Controls.Add(this.ComboBox2);
            this.TabPage4.Controls.Add(this.PaletteContainer);
            this.TabPage4.Controls.Add(this.Label9);
            this.TabPage4.Controls.Add(this.PictureBox11);
            this.TabPage4.Location = new System.Drawing.Point(40, 4);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(288, 264);
            this.TabPage4.TabIndex = 3;
            // 
            // ComboBox2
            // 
            this.ComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ComboBox2.ForeColor = System.Drawing.Color.White;
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.ItemHeight = 20;
            this.ComboBox2.Items.AddRange(new object[] {
            "From current theme",
            "Default Windows 12 theme",
            "Default Windows 11 theme",
            "Default Windows 10 theme",
            "Default Windows 8.1 theme",
            "Default Windows 8 theme",
            "Default Windows 7 theme",
            "Default Windows Vista theme",
            "Default Windows XP theme"});
            this.ComboBox2.Location = new System.Drawing.Point(6, 36);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(276, 26);
            this.ComboBox2.TabIndex = 49;
            this.ComboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            // 
            // PaletteContainer
            // 
            this.PaletteContainer.AutoScroll = true;
            this.PaletteContainer.Location = new System.Drawing.Point(6, 68);
            this.PaletteContainer.Name = "PaletteContainer";
            this.PaletteContainer.Size = new System.Drawing.Size(278, 190);
            this.PaletteContainer.TabIndex = 48;
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(36, 6);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(246, 24);
            this.Label9.TabIndex = 34;
            this.Label9.Text = "These are all colors used in current theme";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(6, 6);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(24, 24);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 35;
            this.PictureBox11.TabStop = false;
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "icons8_slider_16px.png");
            this.ImageList1.Images.SetKeyName(1, "icons8_delicious_16px.png");
            this.ImageList1.Images.SetKeyName(2, "icons8_RGB_Color_Wheel_16px.png");
            this.ImageList1.Images.SetKeyName(3, "icons8_fantasy_16px.png");
            this.ImageList1.Images.SetKeyName(4, "icons8_time_machine_16px.png");
            this.ImageList1.Images.SetKeyName(5, "icons8_image_16px.png");
            this.ImageList1.Images.SetKeyName(6, "16.png");
            this.ImageList1.Images.SetKeyName(7, "16.png");
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.ScreenColorPicker1);
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(1, 279);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(332, 48);
            this.bottom_buttons.TabIndex = 120;
            // 
            // ColorPickerDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(334, 328);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.bottom_buttons);
            this.Name = "ColorPickerDlg";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Color Picker";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ColorPickerDlg_FormClosed);
            this.Load += new System.EventHandler(this.ColorPicker_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorPicker_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorPicker_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage5.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.TabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabControl2.ResumeLayout(false);
            this.TabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).EndInit();
            this.TabPage8.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal Cyotek.Windows.Forms.ColorEditor ColorEditor1;
        internal Cyotek.Windows.Forms.ColorEditorManager ColorEditorManager1;
        internal Cyotek.Windows.Forms.ColorGrid ColorGrid1;
        internal Cyotek.Windows.Forms.ColorWheel ColorWheel1;
        internal Cyotek.Windows.Forms.ScreenColorPicker ScreenColorPicker1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox1;
        internal FlowLayoutPanel ImgPaletteContainer;
        internal PictureBox PictureBox5;
        internal Label Label3;
        internal UI.WP.ProgressBar ProgressBar1;
        internal Label Label4;
        internal UI.WP.CheckBox CheckBox1;
        internal Label Label6;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox8;
        internal Label Label7;
        internal PictureBox PictureBox9;
        internal UI.WP.Button Button6;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.Button Button7;
        internal Label Label1;
        internal PictureBox PictureBox1;
        internal FlowLayoutPanel ThemePaletteContainer;
        internal Label Label8;
        internal UI.WP.ComboBox ComboBox1;
        internal PictureBox PictureBox33;
        internal Label Label29;
        internal PictureBox PictureBox10;
        internal TabPage TabPage4;
        internal UI.WP.ComboBox ComboBox2;
        internal FlowLayoutPanel PaletteContainer;
        internal Label Label9;
        internal PictureBox PictureBox11;
        internal UI.WP.CheckBox CheckBox2;
        internal PictureBox PictureBox12;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal UI.WP.TabControl TabControl2;
        internal TabPage TabPage7;
        internal TabPage TabPage8;
        internal UI.WP.RadioImage RadioButton2;
        internal UI.WP.RadioImage RadioButton1;
        internal Label Label2;
        internal PictureBox PictureBox2;
        internal ImageList ImageList1;
        internal TabPage TabPage9;
        internal Label Label5;
        internal PictureBox PictureBox3;
        internal FlowLayoutPanel FlowLayoutPanel1;
        private UI.WP.GroupBox bottom_buttons;
        private UI.Controllers.TrackBarX trackBarX1;
        private UI.Controllers.TrackBarX trackBarX2;
        private TabPage tabPage10;
        internal Label label10;
        internal PictureBox pictureBox4;
        private UI.Controllers.ColorItem effect_dark;
        private UI.Controllers.TrackBarX trackBar1;
        internal Label label11;
        internal PictureBox pictureBox6;
        private Panel panel1;
        private UI.Controllers.ColorItem effect_light;
        internal PictureBox pictureBox13;
        private UI.Controllers.TrackBarX trackBar2;
        internal Label label12;
        private UI.Controllers.ColorItem effect_analogus_next;
        internal PictureBox pictureBox14;
        private UI.Controllers.TrackBarX trackBar3;
        internal Label label13;
        private UI.Controllers.ColorItem effect_rotateHue;
        private UI.Controllers.ColorItem effect_desaturate;
        internal PictureBox pictureBox15;
        internal PictureBox pictureBox16;
        private UI.Controllers.TrackBarX trackBar5;
        private UI.Controllers.TrackBarX trackBar4;
        internal Label label14;
        internal Label label15;
        internal PictureBox pictureBox17;
        internal Label label16;
        private UI.Controllers.ColorItem effect_invert;
        private UI.Controllers.ColorItem effect_256;
        internal PictureBox pictureBox19;
        internal Label label18;
        private UI.Controllers.ColorItem effect_monochrome;
        internal PictureBox pictureBox20;
        internal Label label19;
        private UI.Controllers.ColorItem effect_grayscale;
        internal PictureBox pictureBox21;
        internal Label label20;
        private UI.Controllers.ColorItem effect_sepia;
        internal PictureBox pictureBox22;
        internal Label label21;
        private UI.Controllers.ColorItem effect_analogus_previous;
        private UI.Controllers.ColorItem effect_brightness;
        internal PictureBox pictureBox24;
        private UI.Controllers.TrackBarX trackBarX3;
        internal Label label23;
        private UI.Controllers.ColorItem effect_MaterialExpressive;
        internal PictureBox pictureBox26;
        internal Label label25;
        private UI.Controllers.ColorItem effect_Material;
        internal PictureBox pictureBox23;
        internal Label label22;
        private UI.Controllers.ColorItem effect_macOS;
        internal PictureBox pictureBox25;
        internal Label label24;
    }
}
