using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ColorPickerDlg : Form
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPickerDlg));
            ColorEditorManager1 = new Cyotek.Windows.Forms.ColorEditorManager();
            ColorEditorManager1.ColorChanged += new EventHandler(Change_Color_Preview);
            ColorEditor1 = new Cyotek.Windows.Forms.ColorEditor();
            ColorGrid1 = new Cyotek.Windows.Forms.ColorGrid();
            ColorWheel1 = new Cyotek.Windows.Forms.ColorWheel();
            ScreenColorPicker1 = new Cyotek.Windows.Forms.ScreenColorPicker();
            ScreenColorPicker1.MouseDown += new MouseEventHandler(ScreenColorPicker1_MouseDown);
            ScreenColorPicker1.MouseUp += new MouseEventHandler(ScreenColorPicker1_MouseUp);
            BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            BackgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(BackgroundWorker1_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            OpenFileDialog1 = new OpenFileDialog();
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Label7 = new Label();
            PictureBox9 = new PictureBox();
            CheckBox1 = new UI.WP.CheckBox();
            Label6 = new Label();
            PictureBox7 = new PictureBox();
            PictureBox8 = new PictureBox();
            ProgressBar1 = new ProgressBar();
            PictureBox5 = new PictureBox();
            Label3 = new Label();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click_1);
            TextBox1 = new UI.WP.TextBox();
            ImgPaletteContainer = new FlowLayoutPanel();
            Label4 = new Label();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            TabControl1 = new UI.WP.TabControl();
            TabPage5 = new TabPage();
            TabPage1 = new TabPage();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click_1);
            TabPage6 = new TabPage();
            TabPage9 = new TabPage();
            Label5 = new Label();
            PictureBox3 = new PictureBox();
            FlowLayoutPanel1 = new FlowLayoutPanel();
            TabPage2 = new TabPage();
            TabControl2 = new UI.WP.TabControl();
            TabPage7 = new TabPage();
            PictureBox2 = new PictureBox();
            PictureBox12 = new PictureBox();
            Label2 = new Label();
            CheckBox2 = new UI.WP.CheckBox();
            RadioButton2 = new UI.WP.RadioImage();
            RadioButton1 = new UI.WP.RadioImage();
            val2 = new UI.WP.Button();
            val2.Click += new EventHandler(Button8_Click);
            val1 = new UI.WP.Button();
            val1.Click += new EventHandler(Ttl_h_Click);
            Trackbar2 = new UI.WP.Trackbar();
            Trackbar2.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar2_Scroll);
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            TabPage8 = new TabPage();
            TabPage3 = new TabPage();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            PictureBox10 = new PictureBox();
            Label8 = new Label();
            ComboBox1 = new UI.WP.ComboBox();
            ComboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            PictureBox33 = new PictureBox();
            Label29 = new Label();
            TextBox2 = new UI.WP.TextBox();
            TextBox2.TextChanged += new EventHandler(TextBox1_TextChanged);
            Label1 = new Label();
            PictureBox1 = new PictureBox();
            ThemePaletteContainer = new FlowLayoutPanel();
            TabPage4 = new TabPage();
            ComboBox2 = new UI.WP.ComboBox();
            ComboBox2.SelectedIndexChanged += new EventHandler(ComboBox2_SelectedIndexChanged);
            PaletteContainer = new FlowLayoutPanel();
            Label9 = new Label();
            PictureBox11 = new PictureBox();
            ImageList1 = new ImageList(components);
            OpenFileDialog2 = new OpenFileDialog();
            SaveFileDialog1 = new SaveFileDialog();
            OpenThemeDialog = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            TabControl1.SuspendLayout();
            TabPage5.SuspendLayout();
            TabPage1.SuspendLayout();
            TabPage6.SuspendLayout();
            TabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            TabPage2.SuspendLayout();
            TabControl2.SuspendLayout();
            TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            TabPage8.SuspendLayout();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            SuspendLayout();
            // 
            // ColorEditorManager1
            // 
            ColorEditorManager1.ColorEditor = ColorEditor1;
            ColorEditorManager1.ColorGrid = ColorGrid1;
            ColorEditorManager1.ColorWheel = ColorWheel1;
            ColorEditorManager1.ScreenColorPicker = ScreenColorPicker1;
            // 
            // ColorEditor1
            // 
            ColorEditor1.Color = Color.FromArgb(192, 0, 0);
            ColorEditor1.Dock = DockStyle.Fill;
            ColorEditor1.Location = new Point(3, 3);
            ColorEditor1.Margin = new Padding(4, 3, 4, 3);
            ColorEditor1.Name = "ColorEditor1";
            ColorEditor1.Size = new Size(282, 258);
            ColorEditor1.TabIndex = 0;
            // 
            // ColorGrid1
            // 
            ColorGrid1.AutoAddColors = false;
            ColorGrid1.AutoSize = false;
            ColorGrid1.CellBorderColor = Color.DimGray;
            ColorGrid1.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
            ColorGrid1.CellSize = new Size(15, 15);
            ColorGrid1.Color = Color.FromArgb(192, 0, 0);
            ColorGrid1.Columns = 15;
            ColorGrid1.Dock = DockStyle.Fill;
            ColorGrid1.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            ColorGrid1.Location = new Point(3, 3);
            ColorGrid1.Name = "ColorGrid1";
            ColorGrid1.Size = new Size(282, 232);
            ColorGrid1.TabIndex = 1;
            // 
            // ColorWheel1
            // 
            ColorWheel1.Color = Color.FromArgb(192, 0, 0);
            ColorWheel1.Dock = DockStyle.Fill;
            ColorWheel1.Location = new Point(3, 3);
            ColorWheel1.Name = "ColorWheel1";
            ColorWheel1.Size = new Size(282, 258);
            ColorWheel1.TabIndex = 2;
            // 
            // ScreenColorPicker1
            // 
            ScreenColorPicker1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ScreenColorPicker1.Color = Color.FromArgb(192, 0, 0);
            ScreenColorPicker1.Location = new Point(12, 273);
            ScreenColorPicker1.Name = "ScreenColorPicker1";
            ScreenColorPicker1.Size = new Size(132, 34);
            ScreenColorPicker1.Text = "Drag and release for a screen pixel color pick";
            // 
            // BackgroundWorker1
            // 
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp|All Files|*.*";
            // 
            // Button6
            // 
            Button6.BackColor = Color.FromArgb(34, 34, 34);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.ImageAlign = ContentAlignment.MiddleRight;
            Button6.LineColor = Color.FromArgb(1, 45, 77);
            Button6.Location = new Point(208, 3);
            Button6.Margin = new Padding(4, 3, 4, 3);
            Button6.Name = "Button6";
            Button6.Size = new Size(69, 24);
            Button6.TabIndex = 28;
            Button6.Text = "Extract";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Label7
            // 
            Label7.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.Location = new Point(33, 96);
            Label7.Name = "Label7";
            Label7.Size = new Size(110, 24);
            Label7.TabIndex = 27;
            Label7.Text = "Quality";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(3, 96);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 26;
            PictureBox9.TabStop = false;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(33, 126);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(244, 24);
            CheckBox1.TabIndex = 9;
            CheckBox1.Text = "Ignore white colors";
            // 
            // Label6
            // 
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.Location = new Point(33, 66);
            Label6.Name = "Label6";
            Label6.Size = new Size(110, 24);
            Label6.TabIndex = 23;
            Label6.Text = "Maximum colors";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(3, 126);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 22;
            PictureBox7.TabStop = false;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(3, 66);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 21;
            PictureBox8.TabStop = false;
            // 
            // ProgressBar1
            // 
            ProgressBar1.Location = new Point(3, 30);
            ProgressBar1.MarqueeAnimationSpeed = 20;
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(274, 6);
            ProgressBar1.Step = 1;
            ProgressBar1.Style = ProgressBarStyle.Marquee;
            ProgressBar1.TabIndex = 9;
            ProgressBar1.Visible = false;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(3, 3);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 16;
            PictureBox5.TabStop = false;
            // 
            // Label3
            // 
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(33, 3);
            Label3.Name = "Label3";
            Label3.Size = new Size(168, 24);
            Label3.TabIndex = 15;
            Label3.Text = "Palette";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button4
            // 
            Button4.BackColor = Color.FromArgb(34, 34, 34);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.LineColor = Color.FromArgb(184, 153, 68);
            Button4.Location = new Point(243, 33);
            Button4.Margin = new Padding(4, 3, 4, 3);
            Button4.Name = "Button4";
            Button4.Size = new Size(34, 24);
            Button4.TabIndex = 10;
            Button4.UseVisualStyleBackColor = false;
            // 
            // TextBox1
            // 
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(86, 33);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(150, 24);
            TextBox1.TabIndex = 9;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // ImgPaletteContainer
            // 
            ImgPaletteContainer.AutoScroll = true;
            ImgPaletteContainer.Location = new Point(3, 30);
            ImgPaletteContainer.Name = "ImgPaletteContainer";
            ImgPaletteContainer.Size = new Size(274, 197);
            ImgPaletteContainer.TabIndex = 12;
            // 
            // Label4
            // 
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Italic, GraphicsUnit.Point, 0);
            Label4.Location = new Point(3, 30);
            Label4.Name = "Label4";
            Label4.Size = new Size(274, 197);
            Label4.TabIndex = 18;
            Label4.Text = "Extracting palette from image depends on your device's performance, maximum palet" + "te colors number, image quality and its resolution ...";
            Label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(33, 33, 35);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(199, 49, 61);
            Button3.Location = new Point(151, 273);
            Button3.Margin = new Padding(4, 3, 4, 3);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 34);
            Button3.TabIndex = 5;
            Button3.Text = "Cancel";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(33, 33, 35);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(0, 81, 210);
            Button2.Location = new Point(239, 273);
            Button2.Margin = new Padding(4, 3, 4, 3);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 4;
            Button2.Text = "Select";
            Button2.UseVisualStyleBackColor = false;
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.AllowDrop = true;
            TabControl1.Controls.Add(TabPage5);
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage6);
            TabControl1.Controls.Add(TabPage9);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Controls.Add(TabPage4);
            TabControl1.Dock = DockStyle.Top;
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ImageList = ImageList1;
            TabControl1.ItemSize = new Size(30, 36);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(0, 0);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(332, 272);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 9;
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(ColorEditor1);
            TabPage5.Location = new Point(40, 4);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(3);
            TabPage5.Size = new Size(288, 264);
            TabPage5.TabIndex = 4;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(ColorGrid1);
            TabPage1.Controls.Add(Button1);
            TabPage1.Location = new Point(40, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(288, 264);
            TabPage1.TabIndex = 0;
            // 
            // Button12
            // 
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.Dock = DockStyle.Bottom;
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleRight;
            Button1.LineColor = Color.FromArgb(95, 98, 82);
            Button1.Location = new Point(3, 235);
            Button1.Margin = new Padding(4, 3, 4, 3);
            Button1.Name = "Button1";
            Button1.Size = new Size(282, 26);
            Button1.TabIndex = 5;
            Button1.Text = "Open from app palette (e.g. Photoshop)";
            Button1.UseVisualStyleBackColor = false;
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(ColorWheel1);
            TabPage6.Location = new Point(40, 4);
            TabPage6.Name = "TabPage6";
            TabPage6.Padding = new Padding(3);
            TabPage6.Size = new Size(288, 264);
            TabPage6.TabIndex = 5;
            // 
            // TabPage9
            // 
            TabPage9.BackColor = Color.FromArgb(25, 25, 25);
            TabPage9.Controls.Add(Label5);
            TabPage9.Controls.Add(PictureBox3);
            TabPage9.Controls.Add(FlowLayoutPanel1);
            TabPage9.Location = new Point(40, 4);
            TabPage9.Name = "TabPage9";
            TabPage9.Padding = new Padding(3);
            TabPage9.Size = new Size(288, 264);
            TabPage9.TabIndex = 6;
            // 
            // Label5
            // 
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(35, 6);
            Label5.Name = "Label5";
            Label5.Size = new Size(249, 24);
            Label5.TabIndex = 35;
            Label5.Text = "History for current color item:";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(5, 6);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 36;
            PictureBox3.TabStop = false;
            // 
            // FlowLayoutPanel1
            // 
            FlowLayoutPanel1.AutoScroll = true;
            FlowLayoutPanel1.Location = new Point(5, 36);
            FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            FlowLayoutPanel1.Size = new Size(278, 222);
            FlowLayoutPanel1.TabIndex = 34;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(TabControl2);
            TabPage2.ForeColor = Color.White;
            TabPage2.Location = new Point(40, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Size = new Size(288, 264);
            TabPage2.TabIndex = 1;
            // 
            // TabControl2
            // 
            TabControl2.Controls.Add(TabPage7);
            TabControl2.Controls.Add(TabPage8);
            TabControl2.Dock = DockStyle.Fill;
            TabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl2.Font = new Font("Segoe UI", 9.0f);
            TabControl2.ItemSize = new Size(80, 26);
            TabControl2.LineColor = Color.FromArgb(0, 81, 210);
            TabControl2.Location = new Point(0, 0);
            TabControl2.Name = "TabControl2";
            TabControl2.SelectedIndex = 0;
            TabControl2.Size = new Size(288, 264);
            TabControl2.SizeMode = TabSizeMode.Fixed;
            TabControl2.TabIndex = 134;
            // 
            // TabPage7
            // 
            TabPage7.BackColor = Color.FromArgb(25, 25, 25);
            TabPage7.Controls.Add(PictureBox2);
            TabPage7.Controls.Add(PictureBox12);
            TabPage7.Controls.Add(Label2);
            TabPage7.Controls.Add(CheckBox2);
            TabPage7.Controls.Add(RadioButton2);
            TabPage7.Controls.Add(RadioButton1);
            TabPage7.Controls.Add(val2);
            TabPage7.Controls.Add(val1);
            TabPage7.Controls.Add(Trackbar2);
            TabPage7.Controls.Add(Trackbar1);
            TabPage7.Controls.Add(PictureBox8);
            TabPage7.Controls.Add(Button4);
            TabPage7.Controls.Add(PictureBox7);
            TabPage7.Controls.Add(Label7);
            TabPage7.Controls.Add(TextBox1);
            TabPage7.Controls.Add(PictureBox9);
            TabPage7.Controls.Add(Label6);
            TabPage7.Controls.Add(CheckBox1);
            TabPage7.Location = new Point(4, 30);
            TabPage7.Name = "TabPage7";
            TabPage7.Size = new Size(280, 230);
            TabPage7.TabIndex = 0;
            TabPage7.Text = "Options";
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(3, 3);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 136;
            PictureBox2.TabStop = false;
            // 
            // PictureBox12
            // 
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(3, 156);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox12.TabIndex = 133;
            PictureBox12.TabStop = false;
            // 
            // Label2
            // 
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(33, 3);
            Label2.Name = "Label2";
            Label2.Size = new Size(47, 24);
            Label2.TabIndex = 135;
            Label2.Text = "Source";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox2
            // 
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = true;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(33, 156);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(244, 24);
            CheckBox2.TabIndex = 132;
            CheckBox2.Text = "Try to accelerate the process";
            // 
            // RadioButton2
            // 
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Image = null;
            RadioButton2.Location = new Point(211, 3);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.ShowText = true;
            RadioButton2.Size = new Size(66, 24);
            RadioButton2.TabIndex = 133;
            RadioButton2.Text = "Image";
            // 
            // RadioButton1
            // 
            RadioButton1.Checked = true;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Image = null;
            RadioButton1.Location = new Point(86, 3);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.ShowText = true;
            RadioButton1.Size = new Size(119, 24);
            RadioButton1.TabIndex = 132;
            RadioButton1.Text = "Current wallpaper";
            // 
            // val2
            // 
            val2.BackColor = Color.FromArgb(34, 34, 34);
            val2.DrawOnGlass = false;
            val2.Font = new Font("Segoe UI", 9.0f);
            val2.ForeColor = Color.White;
            val2.Image = null;
            val2.LineColor = Color.FromArgb(0, 81, 210);
            val2.Location = new Point(243, 93);
            val2.Name = "val2";
            val2.Size = new Size(34, 24);
            val2.TabIndex = 131;
            val2.UseVisualStyleBackColor = false;
            // 
            // val1
            // 
            val1.BackColor = Color.FromArgb(34, 34, 34);
            val1.DrawOnGlass = false;
            val1.Font = new Font("Segoe UI", 9.0f);
            val1.ForeColor = Color.White;
            val1.Image = null;
            val1.LineColor = Color.FromArgb(0, 81, 210);
            val1.Location = new Point(243, 63);
            val1.Name = "val1";
            val1.Size = new Size(34, 24);
            val1.TabIndex = 130;
            val1.UseVisualStyleBackColor = false;
            // 
            // Trackbar2
            // 
            Trackbar2.LargeChange = 10;
            Trackbar2.Location = new Point(146, 96);
            Trackbar2.Maximum = 100;
            Trackbar2.Minimum = 0;
            Trackbar2.Name = "Trackbar2";
            Trackbar2.Size = new Size(91, 19);
            Trackbar2.SmallChange = 1;
            Trackbar2.TabIndex = 31;
            Trackbar2.Text = "Trackbar2";
            Trackbar2.Value = 10;
            // 
            // Trackbar1
            // 
            Trackbar1.LargeChange = 10;
            Trackbar1.Location = new Point(146, 66);
            Trackbar1.Maximum = 100;
            Trackbar1.Minimum = 5;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(91, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 30;
            Trackbar1.Text = "Trackbar1";
            Trackbar1.Value = 15;
            // 
            // TabPage8
            // 
            TabPage8.BackColor = Color.FromArgb(25, 25, 25);
            TabPage8.Controls.Add(PictureBox5);
            TabPage8.Controls.Add(Button6);
            TabPage8.Controls.Add(ProgressBar1);
            TabPage8.Controls.Add(Label3);
            TabPage8.Controls.Add(ImgPaletteContainer);
            TabPage8.Controls.Add(Label4);
            TabPage8.Location = new Point(4, 30);
            TabPage8.Name = "TabPage8";
            TabPage8.Size = new Size(0, 58);
            TabPage8.TabIndex = 1;
            TabPage8.Text = "Result";
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(Button7);
            TabPage3.Controls.Add(PictureBox10);
            TabPage3.Controls.Add(Label8);
            TabPage3.Controls.Add(ComboBox1);
            TabPage3.Controls.Add(PictureBox33);
            TabPage3.Controls.Add(Label29);
            TabPage3.Controls.Add(TextBox2);
            TabPage3.Controls.Add(Label1);
            TabPage3.Controls.Add(PictureBox1);
            TabPage3.Controls.Add(ThemePaletteContainer);
            TabPage3.Location = new Point(40, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(288, 264);
            TabPage3.TabIndex = 2;
            // 
            // Button7
            // 
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = (Image)resources.GetObject("Button7.Image");
            Button7.LineColor = Color.FromArgb(184, 153, 68);
            Button7.Location = new Point(247, 36);
            Button7.Margin = new Padding(4, 3, 4, 3);
            Button7.Name = "Button7";
            Button7.Size = new Size(35, 24);
            Button7.TabIndex = 30;
            Button7.UseVisualStyleBackColor = false;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(6, 36);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 82;
            PictureBox10.TabStop = false;
            // 
            // Label8
            // 
            Label8.BackColor = Color.Transparent;
            Label8.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8.Location = new Point(36, 36);
            Label8.Name = "Label8";
            Label8.Size = new Size(50, 24);
            Label8.TabIndex = 79;
            Label8.Text = "File:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ComboBox1
            // 
            ComboBox1.BackColor = Color.FromArgb(55, 55, 55);
            ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Location = new Point(92, 65);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(190, 26);
            ComboBox1.TabIndex = 77;
            // 
            // PictureBox33
            // 
            PictureBox33.Image = (Image)resources.GetObject("PictureBox33.Image");
            PictureBox33.Location = new Point(6, 66);
            PictureBox33.Name = "PictureBox33";
            PictureBox33.Size = new Size(24, 24);
            PictureBox33.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox33.TabIndex = 76;
            PictureBox33.TabStop = false;
            // 
            // Label29
            // 
            Label29.BackColor = Color.Transparent;
            Label29.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label29.Location = new Point(36, 66);
            Label29.Name = "Label29";
            Label29.Size = new Size(50, 24);
            Label29.TabIndex = 75;
            Label29.Text = "Preset:";
            Label29.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox2
            // 
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(92, 36);
            TextBox2.MaxLength = 32767;
            TextBox2.Multiline = false;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = false;
            TextBox2.Scrollbars = ScrollBars.None;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(148, 24);
            TextBox2.TabIndex = 29;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // Label1
            // 
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(36, 6);
            Label1.Name = "Label1";
            Label1.Size = new Size(249, 24);
            Label1.TabIndex = 32;
            Label1.Text = @"Theme\Visual Styles or presets to palette";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 6);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 33;
            PictureBox1.TabStop = false;
            // 
            // ThemePaletteContainer
            // 
            ThemePaletteContainer.AutoScroll = true;
            ThemePaletteContainer.Location = new Point(6, 97);
            ThemePaletteContainer.Name = "ThemePaletteContainer";
            ThemePaletteContainer.Size = new Size(278, 161);
            ThemePaletteContainer.TabIndex = 31;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(ComboBox2);
            TabPage4.Controls.Add(PaletteContainer);
            TabPage4.Controls.Add(Label9);
            TabPage4.Controls.Add(PictureBox11);
            TabPage4.Location = new Point(40, 4);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(288, 264);
            TabPage4.TabIndex = 3;
            // 
            // ComboBox2
            // 
            ComboBox2.BackColor = Color.FromArgb(43, 43, 43);
            ComboBox2.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox2.Font = new Font("Segoe UI", 9.0f);
            ComboBox2.ForeColor = Color.White;
            ComboBox2.FormattingEnabled = true;
            ComboBox2.ItemHeight = 20;
            ComboBox2.Items.AddRange(new object[] { "From current theme", "Default Windows 11 theme", "Default Windows 10 theme", "Default Windows 8.1 theme", "Default Windows 7 theme", "Default Windows Vista theme", "Default Windows XP theme" });
            ComboBox2.Location = new Point(6, 36);
            ComboBox2.Name = "ComboBox2";
            ComboBox2.Size = new Size(276, 26);
            ComboBox2.TabIndex = 49;
            // 
            // PaletteContainer
            // 
            PaletteContainer.AutoScroll = true;
            PaletteContainer.Location = new Point(6, 68);
            PaletteContainer.Name = "PaletteContainer";
            PaletteContainer.Size = new Size(278, 190);
            PaletteContainer.TabIndex = 48;
            // 
            // Label9
            // 
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label9.Location = new Point(36, 6);
            Label9.Name = "Label9";
            Label9.Size = new Size(246, 24);
            Label9.TabIndex = 34;
            Label9.Text = "These are all colors used in current theme";
            Label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(6, 6);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 35;
            PictureBox11.TabStop = false;
            // 
            // ImageList1
            // 
            ImageList1.ImageStream = (ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
            ImageList1.TransparentColor = Color.Transparent;
            ImageList1.Images.SetKeyName(0, "icons8_slider_16px.png");
            ImageList1.Images.SetKeyName(1, "icons8_delicious_16px.png");
            ImageList1.Images.SetKeyName(2, "icons8_RGB_Color_Wheel_16px.png");
            ImageList1.Images.SetKeyName(3, "icons8_time_machine_16px.png");
            ImageList1.Images.SetKeyName(4, "icons8_image_16px.png");
            ImageList1.Images.SetKeyName(5, "16.png");
            ImageList1.Images.SetKeyName(6, "Logo16.png");
            // 
            // OpenFileDialog2
            // 
            OpenFileDialog2.Filter = resources.GetString("OpenFileDialog2.Filter");
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.Filter = resources.GetString("SaveFileDialog1.Filter");
            // 
            // OpenThemeDialog
            // 
            OpenThemeDialog.Filter = "Windows Theme (*.theme)|*.theme|Visual Styles File (*.msstyles)|*.msstyles|All Fi" + "les (*.*)|*.*";
            // 
            // ColorPickerDlg
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(332, 315);
            Controls.Add(TabControl1);
            Controls.Add(Button3);
            Controls.Add(Button2);
            Controls.Add(ScreenColorPicker1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "ColorPickerDlg";
            StartPosition = FormStartPosition.Manual;
            Text = "Color Picker";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage5.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            TabPage6.ResumeLayout(false);
            TabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            TabPage2.ResumeLayout(false);
            TabControl2.ResumeLayout(false);
            TabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            TabPage8.ResumeLayout(false);
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox33).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            MouseDown += new MouseEventHandler(ColorPicker_MouseDown);
            MouseMove += new MouseEventHandler(ColorPicker_MouseMove);
            Load += new EventHandler(ColorPicker_Load);
            FormClosing += new FormClosingEventHandler(ColorPicker_FormClosing);
            FormClosed += new FormClosedEventHandler(ColorPickerDlg_FormClosed);
            ResumeLayout(false);

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
        internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
        internal ProgressBar ProgressBar1;
        internal Label Label4;
        internal UI.WP.CheckBox CheckBox1;
        internal Label Label6;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox8;
        internal OpenFileDialog OpenFileDialog1;
        internal Label Label7;
        internal PictureBox PictureBox9;
        internal UI.WP.Button Button6;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal UI.WP.Button Button1;
        internal OpenFileDialog OpenFileDialog2;
        internal SaveFileDialog SaveFileDialog1;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.Button Button7;
        internal Label Label1;
        internal PictureBox PictureBox1;
        internal FlowLayoutPanel ThemePaletteContainer;
        internal OpenFileDialog OpenThemeDialog;
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
        internal UI.WP.Trackbar Trackbar2;
        internal UI.WP.Trackbar Trackbar1;
        internal UI.WP.Button val1;
        internal UI.WP.Button val2;
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
    }
}