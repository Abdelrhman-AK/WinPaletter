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
            this.Button6.ImageGlyphEnabled = true;
            this.Button6.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Play;
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
            this.ProgressBar1.AnimationDuration = 1000;
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
            this.Button4.ImageGlyphEnabled = true;
            this.Button4.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
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
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.ImageGlyph = null;
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
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.ImageGlyph = null;
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
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.ImageGlyph = null;
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
            this.Label5.Location = new System.Drawing.Point(35, 6);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(249, 24);
            this.Label5.TabIndex = 35;
            this.Label5.Text = "History for current color item:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(5, 6);
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
            this.Button7.ImageGlyphEnabled = true;
            this.Button7.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
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
            "Default Windows 11 theme",
            "Default Windows 10 theme",
            "Default Windows 8.1 theme",
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
            this.ImageList1.Images.SetKeyName(3, "icons8_time_machine_16px.png");
            this.ImageList1.Images.SetKeyName(4, "icons8_image_16px.png");
            this.ImageList1.Images.SetKeyName(5, "16.png");
            this.ImageList1.Images.SetKeyName(6, "16.png");
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
            this.CloseOnLostFocus = false;
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.bottom_buttons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}
