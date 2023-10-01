using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class CursorsStudio : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CursorsStudio));
            Timer1 = new Timer(components);
            Timer1.Tick += new EventHandler(Timer1_Tick);
            OpenFileDialog1 = new OpenFileDialog();
            GroupBox10 = new UI.WP.GroupBox();
            CheckBox11 = new UI.WP.CheckBox();
            CheckBox11.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox11_CheckedChanged);
            PictureBox24 = new PictureBox();
            Button19 = new UI.WP.Button();
            Button19.Click += new EventHandler(Button19_Click);
            Trackbar10 = new UI.WP.Trackbar();
            Trackbar10.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar10_Scroll);
            Button18 = new UI.WP.Button();
            Button18.Click += new EventHandler(Button18_Click);
            Trackbar9 = new UI.WP.Trackbar();
            Trackbar9.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar9_Scroll);
            Label22 = new Label();
            PictureBox20 = new PictureBox();
            Button17 = new UI.WP.Button();
            Button17.Click += new EventHandler(Button17_Click);
            Trackbar8 = new UI.WP.Trackbar();
            Trackbar8.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar8_Scroll);
            PictureBox19 = new PictureBox();
            Label26 = new Label();
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click);
            ColorItem1 = new UI.Controllers.ColorItem();
            ColorItem1.Click += new EventHandler(ColorItem1_Click);
            ColorItem1.DragDrop += new DragEventHandler(ColorItem1_Click);
            Trackbar7 = new UI.WP.Trackbar();
            Trackbar7.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar7_Scroll);
            PictureBox16 = new PictureBox();
            Label14 = new Label();
            Label24 = new Label();
            Label25 = new Label();
            PictureBox17 = new PictureBox();
            PictureBox18 = new PictureBox();
            GroupBox9 = new UI.WP.GroupBox();
            PictureBox13 = new PictureBox();
            Label6 = new Label();
            PictureBox9 = new PictureBox();
            trails_btn = new UI.WP.Button();
            trails_btn.Click += new EventHandler(Ttl_h_Click);
            PictureBox22 = new PictureBox();
            Label11 = new Label();
            CheckBox9 = new UI.WP.CheckBox();
            PictureBox21 = new PictureBox();
            Trackbar2 = new UI.WP.Trackbar();
            Trackbar2.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar2_Scroll);
            CheckBox10 = new UI.WP.CheckBox();
            GroupBox6 = new UI.WP.GroupBox();
            Label8 = new Label();
            PictureBox14 = new PictureBox();
            Label10 = new Label();
            PictureBox15 = new PictureBox();
            PictureBox11 = new PictureBox();
            Label7 = new Label();
            ComboBox5 = new UI.WP.ComboBox();
            ComboBox5.SelectedIndexChanged += new EventHandler(ComboBox5_SelectedIndexChanged);
            ComboBox6 = new UI.WP.ComboBox();
            ComboBox6.SelectedIndexChanged += new EventHandler(ComboBox6_SelectedIndexChanged);
            GroupBox5 = new UI.WP.GroupBox();
            PictureBox3 = new PictureBox();
            Label13 = new Label();
            Label4 = new Label();
            GroupBox7 = new UI.WP.GroupBox();
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click);
            Trackbar5 = new UI.WP.Trackbar();
            Trackbar5.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar5_Scroll);
            CircleColor1 = new UI.Controllers.ColorItem();
            CircleColor1.Click += new EventHandler(GroupBox10_Click);
            CircleColor1.DragDrop += new DragEventHandler(GroupBox10_Click);
            CheckBox7 = new UI.WP.CheckBox();
            CheckBox7.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox7_CheckedChanged);
            CircleColor2 = new UI.Controllers.ColorItem();
            CircleColor2.Click += new EventHandler(GroupBox9_Click);
            CircleColor2.DragDrop += new DragEventHandler(GroupBox9_Click);
            ComboBox4 = new UI.WP.ComboBox();
            ComboBox4.SelectedIndexChanged += new EventHandler(ComboBox4_SelectedIndexChanged);
            CheckBox8 = new UI.WP.CheckBox();
            CheckBox8.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox8_CheckedChanged);
            Label9 = new Label();
            GroupBox8 = new UI.WP.GroupBox();
            Button15 = new UI.WP.Button();
            Button15.Click += new EventHandler(Button15_Click);
            Trackbar6 = new UI.WP.Trackbar();
            Trackbar6.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar6_Scroll);
            LoadingColor1 = new UI.Controllers.ColorItem();
            LoadingColor1.Click += new EventHandler(GroupBox8_Click);
            LoadingColor1.DragDrop += new DragEventHandler(GroupBox8_Click);
            ComboBox3 = new UI.WP.ComboBox();
            ComboBox3.SelectedIndexChanged += new EventHandler(ComboBox3_SelectedIndexChanged);
            CheckBox2 = new UI.WP.CheckBox();
            CheckBox2.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox2_CheckedChanged);
            LoadingColor2 = new UI.Controllers.ColorItem();
            LoadingColor2.Click += new EventHandler(GroupBox7_Click);
            LoadingColor2.DragDrop += new DragEventHandler(GroupBox7_Click);
            CheckBox6 = new UI.WP.CheckBox();
            CheckBox6.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox6_CheckedChanged);
            Label19 = new Label();
            Label20 = new Label();
            PictureBox7 = new PictureBox();
            PictureBox8 = new PictureBox();
            Label21 = new Label();
            PictureBox10 = new PictureBox();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            GroupBox13 = new UI.WP.GroupBox();
            checker_img = new PictureBox();
            Toggle1 = new UI.WP.Toggle();
            Toggle1.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(Toggle1_CheckedChanged);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label12 = new Label();
            GroupBox2 = new UI.WP.GroupBox();
            PictureBox6 = new PictureBox();
            Label2 = new Label();
            Label18 = new Label();
            GroupBox4 = new UI.WP.GroupBox();
            Button13 = new UI.WP.Button();
            Button13.Click += new EventHandler(Button13_Click);
            Trackbar4 = new UI.WP.Trackbar();
            Trackbar4.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar4_Scroll);
            CheckBox4 = new UI.WP.CheckBox();
            CheckBox4.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox4_CheckedChanged);
            CheckBox3 = new UI.WP.CheckBox();
            CheckBox3.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox3_CheckedChanged);
            SecondaryColor1 = new UI.Controllers.ColorItem();
            SecondaryColor1.Click += new EventHandler(GroupBox5_Click);
            SecondaryColor1.DragDrop += new DragEventHandler(GroupBox5_Click);
            SecondaryColor2 = new UI.Controllers.ColorItem();
            SecondaryColor2.Click += new EventHandler(GroupBox4_Click);
            SecondaryColor2.DragDrop += new DragEventHandler(GroupBox4_Click);
            ComboBox2 = new UI.WP.ComboBox();
            ComboBox2.SelectedIndexChanged += new EventHandler(ComboBox2_SelectedIndexChanged);
            Label17 = new Label();
            GroupBox3 = new UI.WP.GroupBox();
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            Trackbar3 = new UI.WP.Trackbar();
            Trackbar3.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar3_Scroll);
            PrimaryColor2 = new UI.Controllers.ColorItem();
            PrimaryColor2.Click += new EventHandler(GroupBox3_Click);
            PrimaryColor2.DragDrop += new DragEventHandler(GroupBox3_Click);
            PrimaryColor1 = new UI.Controllers.ColorItem();
            PrimaryColor1.Click += new EventHandler(TaskbarFrontAndFoldersOnStart_picker_Click);
            PrimaryColor1.DragDrop += new DragEventHandler(TaskbarFrontAndFoldersOnStart_picker_Click);
            ComboBox1 = new UI.WP.ComboBox();
            ComboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            CheckBox5 = new UI.WP.CheckBox();
            CheckBox5.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox5_CheckedChanged);
            Label16 = new Label();
            Label15 = new Label();
            PictureBox5 = new PictureBox();
            PictureBox4 = new PictureBox();
            Label3 = new Label();
            PictureBox2 = new PictureBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            GroupBox1 = new UI.WP.GroupBox();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Label1 = new Label();
            FlowLayoutPanel1 = new FlowLayoutPanel();
            Arrow = new CursorControl();
            Help = new CursorControl();
            AppLoading = new CursorControl();
            Busy = new CursorControl();
            Move_Cur = new CursorControl();
            Up = new CursorControl();
            NS = new CursorControl();
            EW = new CursorControl();
            NESW = new CursorControl();
            NWSE = new CursorControl();
            Pen = new CursorControl();
            None = new CursorControl();
            Link = new CursorControl();
            Pin = new CursorControl();
            Person = new CursorControl();
            IBeam = new CursorControl();
            Cross = new CursorControl();
            PictureBox12 = new PictureBox();
            Label5 = new Label();
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            AlertBox1 = new UI.WP.AlertBox();
            GroupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).BeginInit();
            GroupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).BeginInit();
            GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            GroupBox7.SuspendLayout();
            GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            GroupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            GroupBox4.SuspendLayout();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            GroupBox1.SuspendLayout();
            FlowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            SuspendLayout();
            // 
            // Timer1
            // 
            Timer1.Interval = 30;
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // GroupBox10
            // 
            GroupBox10.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox10.Controls.Add(CheckBox11);
            GroupBox10.Controls.Add(PictureBox24);
            GroupBox10.Controls.Add(Button19);
            GroupBox10.Controls.Add(Trackbar10);
            GroupBox10.Controls.Add(Button18);
            GroupBox10.Controls.Add(Trackbar9);
            GroupBox10.Controls.Add(Label22);
            GroupBox10.Controls.Add(PictureBox20);
            GroupBox10.Controls.Add(Button17);
            GroupBox10.Controls.Add(Trackbar8);
            GroupBox10.Controls.Add(PictureBox19);
            GroupBox10.Controls.Add(Label26);
            GroupBox10.Controls.Add(Button16);
            GroupBox10.Controls.Add(ColorItem1);
            GroupBox10.Controls.Add(Trackbar7);
            GroupBox10.Controls.Add(PictureBox16);
            GroupBox10.Controls.Add(Label14);
            GroupBox10.Controls.Add(Label24);
            GroupBox10.Controls.Add(Label25);
            GroupBox10.Controls.Add(PictureBox17);
            GroupBox10.Controls.Add(PictureBox18);
            GroupBox10.Enabled = false;
            GroupBox10.Location = new Point(12, 391);
            GroupBox10.Name = "GroupBox10";
            GroupBox10.Size = new Size(552, 132);
            GroupBox10.TabIndex = 136;
            // 
            // CheckBox11
            // 
            CheckBox11.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox11.Checked = false;
            CheckBox11.Font = new Font("Segoe UI", 9.0f);
            CheckBox11.ForeColor = Color.White;
            CheckBox11.Location = new Point(62, 40);
            CheckBox11.Name = "CheckBox11";
            CheckBox11.Size = new Size(78, 24);
            CheckBox11.TabIndex = 150;
            CheckBox11.Text = "Enabled";
            // 
            // PictureBox24
            // 
            PictureBox24.Image = (Image)resources.GetObject("PictureBox24.Image");
            PictureBox24.Location = new Point(32, 40);
            PictureBox24.Name = "PictureBox24";
            PictureBox24.Size = new Size(24, 24);
            PictureBox24.TabIndex = 149;
            PictureBox24.TabStop = false;
            // 
            // Button19
            // 
            Button19.BackColor = Color.FromArgb(43, 43, 43);
            Button19.DrawOnGlass = false;
            Button19.Font = new Font("Segoe UI", 9.0f);
            Button19.ForeColor = Color.White;
            Button19.Image = null;
            Button19.LineColor = Color.FromArgb(0, 81, 210);
            Button19.Location = new Point(510, 101);
            Button19.Name = "Button19";
            Button19.Size = new Size(34, 24);
            Button19.TabIndex = 148;
            Button19.UseVisualStyleBackColor = false;
            // 
            // Trackbar10
            // 
            Trackbar10.LargeChange = 2;
            Trackbar10.Location = new Point(304, 104);
            Trackbar10.Maximum = 5;
            Trackbar10.Minimum = 0;
            Trackbar10.Name = "Trackbar10";
            Trackbar10.Size = new Size(160, 19);
            Trackbar10.SmallChange = 1;
            Trackbar10.TabIndex = 147;
            Trackbar10.Value = 0;
            // 
            // Button18
            // 
            Button18.BackColor = Color.FromArgb(43, 43, 43);
            Button18.DrawOnGlass = false;
            Button18.Font = new Font("Segoe UI", 9.0f);
            Button18.ForeColor = Color.White;
            Button18.Image = null;
            Button18.LineColor = Color.FromArgb(0, 81, 210);
            Button18.Location = new Point(470, 101);
            Button18.Name = "Button18";
            Button18.Size = new Size(34, 24);
            Button18.TabIndex = 144;
            Button18.UseVisualStyleBackColor = false;
            // 
            // Trackbar9
            // 
            Trackbar9.LargeChange = 2;
            Trackbar9.Location = new Point(137, 104);
            Trackbar9.Maximum = 5;
            Trackbar9.Minimum = 0;
            Trackbar9.Name = "Trackbar9";
            Trackbar9.Size = new Size(160, 19);
            Trackbar9.SmallChange = 1;
            Trackbar9.TabIndex = 143;
            Trackbar9.Value = 0;
            // 
            // Label22
            // 
            Label22.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label22.Location = new Point(62, 101);
            Label22.Name = "Label22";
            Label22.Size = new Size(69, 24);
            Label22.TabIndex = 142;
            Label22.Text = "Offset X, Y";
            Label22.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox20
            // 
            PictureBox20.Image = (Image)resources.GetObject("PictureBox20.Image");
            PictureBox20.Location = new Point(32, 101);
            PictureBox20.Name = "PictureBox20";
            PictureBox20.Size = new Size(24, 24);
            PictureBox20.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox20.TabIndex = 141;
            PictureBox20.TabStop = false;
            // 
            // Button17
            // 
            Button17.BackColor = Color.FromArgb(43, 43, 43);
            Button17.DrawOnGlass = false;
            Button17.Font = new Font("Segoe UI", 9.0f);
            Button17.ForeColor = Color.White;
            Button17.Image = null;
            Button17.LineColor = Color.FromArgb(0, 81, 210);
            Button17.Location = new Point(511, 70);
            Button17.Name = "Button17";
            Button17.Size = new Size(34, 24);
            Button17.TabIndex = 140;
            Button17.UseVisualStyleBackColor = false;
            // 
            // Trackbar8
            // 
            Trackbar8.LargeChange = 2;
            Trackbar8.Location = new Point(410, 73);
            Trackbar8.Maximum = 100;
            Trackbar8.Minimum = 0;
            Trackbar8.Name = "Trackbar8";
            Trackbar8.Size = new Size(95, 19);
            Trackbar8.SmallChange = 1;
            Trackbar8.TabIndex = 139;
            Trackbar8.Value = 0;
            // 
            // PictureBox19
            // 
            PictureBox19.Image = (Image)resources.GetObject("PictureBox19.Image");
            PictureBox19.Location = new Point(307, 40);
            PictureBox19.Name = "PictureBox19";
            PictureBox19.Size = new Size(24, 24);
            PictureBox19.TabIndex = 9;
            PictureBox19.TabStop = false;
            // 
            // Label26
            // 
            Label26.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label26.Location = new Point(337, 40);
            Label26.Name = "Label26";
            Label26.Size = new Size(69, 24);
            Label26.TabIndex = 12;
            Label26.Text = "Color";
            Label26.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button16
            // 
            Button16.BackColor = Color.FromArgb(43, 43, 43);
            Button16.DrawOnGlass = false;
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = null;
            Button16.LineColor = Color.FromArgb(0, 81, 210);
            Button16.Location = new Point(238, 71);
            Button16.Name = "Button16";
            Button16.Size = new Size(34, 24);
            Button16.TabIndex = 138;
            Button16.UseVisualStyleBackColor = false;
            // 
            // ColorItem1
            // 
            ColorItem1.AllowDrop = true;
            ColorItem1.BackColor = Color.FromArgb(47, 47, 48);
            ColorItem1.DefaultColor = Color.White;
            ColorItem1.DontShowInfo = false;
            ColorItem1.Location = new Point(413, 42);
            ColorItem1.Margin = new Padding(4, 3, 4, 3);
            ColorItem1.Name = "ColorItem1";
            ColorItem1.Size = new Size(95, 21);
            ColorItem1.TabIndex = 77;
            // 
            // Trackbar7
            // 
            Trackbar7.LargeChange = 2;
            Trackbar7.Location = new Point(137, 74);
            Trackbar7.Maximum = 10;
            Trackbar7.Minimum = 0;
            Trackbar7.Name = "Trackbar7";
            Trackbar7.Size = new Size(95, 19);
            Trackbar7.SmallChange = 1;
            Trackbar7.TabIndex = 137;
            Trackbar7.Value = 0;
            // 
            // PictureBox16
            // 
            PictureBox16.BackColor = Color.Transparent;
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(3, 3);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(35, 35);
            PictureBox16.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox16.TabIndex = 76;
            PictureBox16.TabStop = false;
            // 
            // Label14
            // 
            Label14.BackColor = Color.Transparent;
            Label14.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label14.Location = new Point(40, 3);
            Label14.Name = "Label14";
            Label14.Size = new Size(507, 35);
            Label14.TabIndex = 75;
            Label14.Text = "Custom shadow";
            Label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label24
            // 
            Label24.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label24.Location = new Point(62, 71);
            Label24.Name = "Label24";
            Label24.Size = new Size(69, 24);
            Label24.TabIndex = 69;
            Label24.Text = "Blur Power";
            Label24.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label25
            // 
            Label25.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label25.Location = new Point(337, 70);
            Label25.Name = "Label25";
            Label25.Size = new Size(67, 24);
            Label25.TabIndex = 68;
            Label25.Text = "Opacity";
            Label25.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox17
            // 
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(307, 70);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(24, 24);
            PictureBox17.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox17.TabIndex = 54;
            PictureBox17.TabStop = false;
            // 
            // PictureBox18
            // 
            PictureBox18.Image = (Image)resources.GetObject("PictureBox18.Image");
            PictureBox18.Location = new Point(32, 71);
            PictureBox18.Name = "PictureBox18";
            PictureBox18.Size = new Size(24, 24);
            PictureBox18.TabIndex = 53;
            PictureBox18.TabStop = false;
            // 
            // GroupBox9
            // 
            GroupBox9.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox9.Controls.Add(PictureBox13);
            GroupBox9.Controls.Add(Label6);
            GroupBox9.Controls.Add(PictureBox9);
            GroupBox9.Controls.Add(trails_btn);
            GroupBox9.Controls.Add(PictureBox22);
            GroupBox9.Controls.Add(Label11);
            GroupBox9.Controls.Add(CheckBox9);
            GroupBox9.Controls.Add(PictureBox21);
            GroupBox9.Controls.Add(Trackbar2);
            GroupBox9.Controls.Add(CheckBox10);
            GroupBox9.Location = new Point(12, 526);
            GroupBox9.Name = "GroupBox9";
            GroupBox9.Size = new Size(552, 102);
            GroupBox9.TabIndex = 135;
            // 
            // PictureBox13
            // 
            PictureBox13.BackColor = Color.Transparent;
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(3, 3);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(35, 35);
            PictureBox13.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox13.TabIndex = 134;
            PictureBox13.TabStop = false;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label6.Location = new Point(40, 3);
            Label6.Name = "Label6";
            Label6.Size = new Size(509, 35);
            Label6.TabIndex = 133;
            Label6.Text = "Miscellaneous";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(32, 43);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 84;
            PictureBox9.TabStop = false;
            // 
            // trails_btn
            // 
            trails_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            trails_btn.BackColor = Color.FromArgb(43, 43, 43);
            trails_btn.DrawOnGlass = false;
            trails_btn.Font = new Font("Segoe UI", 9.0f);
            trails_btn.ForeColor = Color.White;
            trails_btn.Image = null;
            trails_btn.LineColor = Color.FromArgb(0, 81, 210);
            trails_btn.Location = new Point(510, 73);
            trails_btn.Name = "trails_btn";
            trails_btn.Size = new Size(34, 24);
            trails_btn.TabIndex = 130;
            trails_btn.UseVisualStyleBackColor = false;
            // 
            // PictureBox22
            // 
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(307, 43);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(24, 24);
            PictureBox22.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox22.TabIndex = 132;
            PictureBox22.TabStop = false;
            // 
            // Label11
            // 
            Label11.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label11.Location = new Point(62, 73);
            Label11.Name = "Label11";
            Label11.Size = new Size(53, 24);
            Label11.TabIndex = 86;
            Label11.Text = "Trails";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox9
            // 
            CheckBox9.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox9.Checked = false;
            CheckBox9.Font = new Font("Segoe UI", 9.0f);
            CheckBox9.ForeColor = Color.White;
            CheckBox9.Location = new Point(62, 43);
            CheckBox9.Name = "CheckBox9";
            CheckBox9.Size = new Size(235, 24);
            CheckBox9.TabIndex = 83;
            CheckBox9.Text = "Make cursors have a shadow";
            // 
            // PictureBox21
            // 
            PictureBox21.Image = (Image)resources.GetObject("PictureBox21.Image");
            PictureBox21.Location = new Point(32, 73);
            PictureBox21.Name = "PictureBox21";
            PictureBox21.Size = new Size(24, 24);
            PictureBox21.TabIndex = 87;
            PictureBox21.TabStop = false;
            // 
            // Trackbar2
            // 
            Trackbar2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar2.LargeChange = 2;
            Trackbar2.Location = new Point(121, 76);
            Trackbar2.Maximum = 16;
            Trackbar2.Minimum = 0;
            Trackbar2.Name = "Trackbar2";
            Trackbar2.Size = new Size(383, 19);
            Trackbar2.SmallChange = 1;
            Trackbar2.TabIndex = 85;
            Trackbar2.Value = 0;
            // 
            // CheckBox10
            // 
            CheckBox10.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox10.Checked = false;
            CheckBox10.Font = new Font("Segoe UI", 9.0f);
            CheckBox10.ForeColor = Color.White;
            CheckBox10.Location = new Point(337, 43);
            CheckBox10.Name = "CheckBox10";
            CheckBox10.Size = new Size(207, 24);
            CheckBox10.TabIndex = 131;
            CheckBox10.Text = "Cursor tracking (Sonar)";
            // 
            // GroupBox6
            // 
            GroupBox6.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox6.Controls.Add(Label8);
            GroupBox6.Controls.Add(PictureBox14);
            GroupBox6.Controls.Add(Label10);
            GroupBox6.Controls.Add(PictureBox15);
            GroupBox6.Controls.Add(PictureBox11);
            GroupBox6.Controls.Add(Label7);
            GroupBox6.Controls.Add(ComboBox5);
            GroupBox6.Controls.Add(ComboBox6);
            GroupBox6.Enabled = false;
            GroupBox6.Location = new Point(12, 55);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Size = new Size(552, 69);
            GroupBox6.TabIndex = 134;
            // 
            // Label8
            // 
            Label8.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8.Location = new Point(345, 40);
            Label8.Name = "Label8";
            Label8.Size = new Size(50, 24);
            Label8.TabIndex = 76;
            Label8.Text = "Busy:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox14
            // 
            PictureBox14.Image = (Image)resources.GetObject("PictureBox14.Image");
            PictureBox14.Location = new Point(315, 40);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.TabIndex = 75;
            PictureBox14.TabStop = false;
            // 
            // Label10
            // 
            Label10.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label10.Location = new Point(74, 40);
            Label10.Name = "Label10";
            Label10.Size = new Size(59, 24);
            Label10.TabIndex = 74;
            Label10.Text = "Arrow:";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox15
            // 
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(44, 40);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.TabIndex = 73;
            PictureBox15.TabStop = false;
            // 
            // PictureBox11
            // 
            PictureBox11.BackColor = Color.Transparent;
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(3, 4);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(35, 35);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 72;
            PictureBox11.TabStop = false;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label7.Location = new Point(40, 4);
            Label7.Name = "Label7";
            Label7.Size = new Size(509, 35);
            Label7.TabIndex = 71;
            Label7.Text = "Style (For selected cursor)";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ComboBox5
            // 
            ComboBox5.BackColor = Color.FromArgb(43, 43, 43);
            ComboBox5.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox5.Font = new Font("Segoe UI", 9.0f);
            ComboBox5.ForeColor = Color.White;
            ComboBox5.FormattingEnabled = true;
            ComboBox5.ItemHeight = 20;
            ComboBox5.Items.AddRange(new object[] { "Aero", "Modern", "Classic" });
            ComboBox5.Location = new Point(136, 39);
            ComboBox5.Name = "ComboBox5";
            ComboBox5.Size = new Size(148, 26);
            ComboBox5.TabIndex = 65;
            // 
            // ComboBox6
            // 
            ComboBox6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ComboBox6.BackColor = Color.FromArgb(43, 43, 43);
            ComboBox6.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox6.Font = new Font("Segoe UI", 9.0f);
            ComboBox6.ForeColor = Color.White;
            ComboBox6.FormattingEnabled = true;
            ComboBox6.ItemHeight = 20;
            ComboBox6.Items.AddRange(new object[] { "Aero", "Dot", "Classic" });
            ComboBox6.Location = new Point(399, 39);
            ComboBox6.Name = "ComboBox6";
            ComboBox6.Size = new Size(148, 26);
            ComboBox6.TabIndex = 68;
            // 
            // GroupBox5
            // 
            GroupBox5.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox5.Controls.Add(PictureBox3);
            GroupBox5.Controls.Add(Label13);
            GroupBox5.Controls.Add(Label4);
            GroupBox5.Controls.Add(GroupBox7);
            GroupBox5.Controls.Add(Label9);
            GroupBox5.Controls.Add(GroupBox8);
            GroupBox5.Controls.Add(Label19);
            GroupBox5.Controls.Add(Label20);
            GroupBox5.Controls.Add(PictureBox7);
            GroupBox5.Controls.Add(PictureBox8);
            GroupBox5.Controls.Add(Label21);
            GroupBox5.Controls.Add(PictureBox10);
            GroupBox5.Enabled = false;
            GroupBox5.Location = new Point(12, 259);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Size = new Size(552, 129);
            GroupBox5.TabIndex = 133;
            // 
            // PictureBox3
            // 
            PictureBox3.BackColor = Color.Transparent;
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(3, 3);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(35, 35);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 76;
            PictureBox3.TabStop = false;
            // 
            // Label13
            // 
            Label13.BackColor = Color.Transparent;
            Label13.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label13.Location = new Point(40, 3);
            Label13.Name = "Label13";
            Label13.Size = new Size(126, 35);
            Label13.TabIndex = 75;
            Label13.Text = "Loading";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(345, 12);
            Label4.Name = "Label4";
            Label4.Size = new Size(203, 24);
            Label4.TabIndex = 72;
            Label4.Text = "Rotating part";
            Label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GroupBox7
            // 
            GroupBox7.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox7.Controls.Add(Button14);
            GroupBox7.Controls.Add(Trackbar5);
            GroupBox7.Controls.Add(CircleColor1);
            GroupBox7.Controls.Add(CheckBox7);
            GroupBox7.Controls.Add(CircleColor2);
            GroupBox7.Controls.Add(ComboBox4);
            GroupBox7.Controls.Add(CheckBox8);
            GroupBox7.Location = new Point(139, 39);
            GroupBox7.Name = "GroupBox7";
            GroupBox7.Size = new Size(203, 86);
            GroupBox7.TabIndex = 71;
            GroupBox7.Text = "GroupBox7";
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(51, 51, 51);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = null;
            Button14.LineColor = Color.FromArgb(0, 81, 210);
            Button14.Location = new Point(165, 59);
            Button14.Name = "Button14";
            Button14.Size = new Size(34, 24);
            Button14.TabIndex = 136;
            Button14.UseVisualStyleBackColor = false;
            // 
            // Trackbar5
            // 
            Trackbar5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar5.LargeChange = 2;
            Trackbar5.Location = new Point(33, 63);
            Trackbar5.Maximum = 100;
            Trackbar5.Minimum = 0;
            Trackbar5.Name = "Trackbar5";
            Trackbar5.Size = new Size(126, 19);
            Trackbar5.SmallChange = 1;
            Trackbar5.TabIndex = 135;
            Trackbar5.Value = 0;
            // 
            // CircleColor1
            // 
            CircleColor1.AllowDrop = true;
            CircleColor1.BackColor = Color.FromArgb(47, 47, 48);
            CircleColor1.DefaultColor = Color.FromArgb(42, 151, 243);
            CircleColor1.DontShowInfo = false;
            CircleColor1.Location = new Point(5, 4);
            CircleColor1.Margin = new Padding(4, 3, 4, 3);
            CircleColor1.Name = "CircleColor1";
            CircleColor1.Size = new Size(95, 21);
            CircleColor1.TabIndex = 11;
            // 
            // CheckBox7
            // 
            CheckBox7.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox7.Checked = false;
            CheckBox7.Font = new Font("Segoe UI", 9.0f);
            CheckBox7.ForeColor = Color.White;
            CheckBox7.Location = new Point(3, 60);
            CheckBox7.Name = "CheckBox7";
            CheckBox7.Size = new Size(24, 24);
            CheckBox7.TabIndex = 49;
            CheckBox7.Text = null;
            // 
            // CircleColor2
            // 
            CircleColor2.AllowDrop = true;
            CircleColor2.BackColor = Color.FromArgb(47, 47, 48);
            CircleColor2.DefaultColor = Color.FromArgb(42, 151, 243);
            CircleColor2.DontShowInfo = false;
            CircleColor2.Location = new Point(104, 4);
            CircleColor2.Margin = new Padding(4, 3, 4, 3);
            CircleColor2.Name = "CircleColor2";
            CircleColor2.Size = new Size(95, 21);
            CircleColor2.TabIndex = 13;
            // 
            // ComboBox4
            // 
            ComboBox4.BackColor = Color.FromArgb(0, 43, 113);
            ComboBox4.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox4.Font = new Font("Segoe UI", 9.0f);
            ComboBox4.ForeColor = Color.White;
            ComboBox4.FormattingEnabled = true;
            ComboBox4.ItemHeight = 20;
            ComboBox4.Items.AddRange(new object[] { "Vertical", "Horizontal", "Forward Diagonal", "Backward Diagonal", "Circle" });
            ComboBox4.Location = new Point(33, 30);
            ComboBox4.Name = "ComboBox4";
            ComboBox4.Size = new Size(166, 26);
            ComboBox4.TabIndex = 17;
            // 
            // CheckBox8
            // 
            CheckBox8.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox8.Checked = false;
            CheckBox8.Font = new Font("Segoe UI", 9.0f);
            CheckBox8.ForeColor = Color.White;
            CheckBox8.Location = new Point(3, 31);
            CheckBox8.Name = "CheckBox8";
            CheckBox8.Size = new Size(24, 24);
            CheckBox8.TabIndex = 15;
            CheckBox8.Text = null;
            // 
            // Label9
            // 
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label9.Location = new Point(139, 12);
            Label9.Name = "Label9";
            Label9.Size = new Size(203, 24);
            Label9.TabIndex = 70;
            Label9.Text = "Background";
            Label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GroupBox8
            // 
            GroupBox8.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox8.Controls.Add(Button15);
            GroupBox8.Controls.Add(Trackbar6);
            GroupBox8.Controls.Add(LoadingColor1);
            GroupBox8.Controls.Add(ComboBox3);
            GroupBox8.Controls.Add(CheckBox2);
            GroupBox8.Controls.Add(LoadingColor2);
            GroupBox8.Controls.Add(CheckBox6);
            GroupBox8.Location = new Point(345, 39);
            GroupBox8.Name = "GroupBox8";
            GroupBox8.Size = new Size(203, 86);
            GroupBox8.TabIndex = 68;
            GroupBox8.Text = "GroupBox8";
            // 
            // Button15
            // 
            Button15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button15.BackColor = Color.FromArgb(51, 51, 51);
            Button15.DrawOnGlass = false;
            Button15.Font = new Font("Segoe UI", 9.0f);
            Button15.ForeColor = Color.White;
            Button15.Image = null;
            Button15.LineColor = Color.FromArgb(0, 81, 210);
            Button15.Location = new Point(165, 59);
            Button15.Name = "Button15";
            Button15.Size = new Size(34, 24);
            Button15.TabIndex = 138;
            Button15.UseVisualStyleBackColor = false;
            // 
            // Trackbar6
            // 
            Trackbar6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar6.LargeChange = 2;
            Trackbar6.Location = new Point(33, 63);
            Trackbar6.Maximum = 100;
            Trackbar6.Minimum = 0;
            Trackbar6.Name = "Trackbar6";
            Trackbar6.Size = new Size(126, 19);
            Trackbar6.SmallChange = 1;
            Trackbar6.TabIndex = 137;
            Trackbar6.Value = 0;
            // 
            // LoadingColor1
            // 
            LoadingColor1.AllowDrop = true;
            LoadingColor1.BackColor = Color.FromArgb(47, 47, 48);
            LoadingColor1.DefaultColor = Color.FromArgb(37, 204, 255);
            LoadingColor1.DontShowInfo = false;
            LoadingColor1.Location = new Point(5, 4);
            LoadingColor1.Margin = new Padding(4, 3, 4, 3);
            LoadingColor1.Name = "LoadingColor1";
            LoadingColor1.Size = new Size(95, 21);
            LoadingColor1.TabIndex = 31;
            // 
            // ComboBox3
            // 
            ComboBox3.BackColor = Color.FromArgb(0, 43, 113);
            ComboBox3.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox3.Font = new Font("Segoe UI", 9.0f);
            ComboBox3.ForeColor = Color.White;
            ComboBox3.FormattingEnabled = true;
            ComboBox3.ItemHeight = 20;
            ComboBox3.Items.AddRange(new object[] { "Vertical", "Horizontal", "Forward Diagonal", "Backward Diagonal", "Circle" });
            ComboBox3.Location = new Point(33, 30);
            ComboBox3.Name = "ComboBox3";
            ComboBox3.Size = new Size(166, 26);
            ComboBox3.TabIndex = 35;
            // 
            // CheckBox2
            // 
            CheckBox2.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox2.Checked = false;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(3, 31);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(24, 24);
            CheckBox2.TabIndex = 52;
            CheckBox2.Text = null;
            // 
            // LoadingColor2
            // 
            LoadingColor2.AllowDrop = true;
            LoadingColor2.BackColor = Color.FromArgb(47, 47, 48);
            LoadingColor2.DefaultColor = Color.FromArgb(37, 204, 255);
            LoadingColor2.DontShowInfo = false;
            LoadingColor2.Location = new Point(104, 4);
            LoadingColor2.Margin = new Padding(4, 3, 4, 3);
            LoadingColor2.Name = "LoadingColor2";
            LoadingColor2.Size = new Size(95, 21);
            LoadingColor2.TabIndex = 33;
            // 
            // CheckBox6
            // 
            CheckBox6.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox6.Checked = false;
            CheckBox6.Font = new Font("Segoe UI", 9.0f);
            CheckBox6.ForeColor = Color.White;
            CheckBox6.Location = new Point(3, 60);
            CheckBox6.Name = "CheckBox6";
            CheckBox6.Size = new Size(24, 24);
            CheckBox6.TabIndex = 59;
            CheckBox6.Text = null;
            // 
            // Label19
            // 
            Label19.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label19.Location = new Point(62, 70);
            Label19.Name = "Label19";
            Label19.Size = new Size(69, 24);
            Label19.TabIndex = 69;
            Label19.Text = "Gradience";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label20
            // 
            Label20.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label20.Location = new Point(62, 99);
            Label20.Name = "Label20";
            Label20.Size = new Size(69, 24);
            Label20.TabIndex = 68;
            Label20.Text = "Noise";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(32, 99);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 54;
            PictureBox7.TabStop = false;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(32, 70);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.TabIndex = 53;
            PictureBox8.TabStop = false;
            // 
            // Label21
            // 
            Label21.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label21.Location = new Point(62, 41);
            Label21.Name = "Label21";
            Label21.Size = new Size(69, 24);
            Label21.TabIndex = 12;
            Label21.Text = "Color";
            Label21.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(32, 41);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.TabIndex = 9;
            PictureBox10.TabStop = false;
            // 
            // Button11
            // 
            Button11.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button11.BackColor = Color.FromArgb(34, 34, 34);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.ImageAlign = ContentAlignment.MiddleLeft;
            Button11.LineColor = Color.FromArgb(36, 81, 110);
            Button11.Location = new Point(574, 642);
            Button11.Name = "Button11";
            Button11.Size = new Size(115, 34);
            Button11.TabIndex = 82;
            Button11.Text = "Quick apply";
            Button11.UseVisualStyleBackColor = false;
            // 
            // GroupBox13
            // 
            GroupBox13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox13.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox13.Controls.Add(checker_img);
            GroupBox13.Controls.Add(Toggle1);
            GroupBox13.Controls.Add(Button7);
            GroupBox13.Controls.Add(Button8);
            GroupBox13.Controls.Add(Button9);
            GroupBox13.Controls.Add(Label12);
            GroupBox13.Location = new Point(12, 12);
            GroupBox13.Name = "GroupBox13";
            GroupBox13.Size = new Size(863, 39);
            GroupBox13.TabIndex = 78;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.BackColor = Color.Transparent;
            checker_img.Image = (Image)resources.GetObject("checker_img.Image");
            checker_img.Location = new Point(776, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 84;
            checker_img.TabStop = false;
            // 
            // Toggle1
            // 
            Toggle1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Toggle1.BackColor = Color.FromArgb(43, 43, 43);
            Toggle1.Checked = true;
            Toggle1.DarkLight_Toggler = false;
            Toggle1.Location = new Point(816, 9);
            Toggle1.Name = "Toggle1";
            Toggle1.Size = new Size(40, 20);
            Toggle1.TabIndex = 77;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button7.BackColor = Color.FromArgb(43, 43, 43);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = (Image)resources.GetObject("Button7.Image");
            Button7.ImageAlign = ContentAlignment.MiddleRight;
            Button7.LineColor = Color.FromArgb(113, 122, 131);
            Button7.Location = new Point(82, 5);
            Button7.Name = "Button7";
            Button7.Size = new Size(146, 29);
            Button7.TabIndex = 69;
            Button7.Text = "WinPaletter Theme";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button8.BackColor = Color.FromArgb(43, 43, 43);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = null;
            Button8.ImageAlign = ContentAlignment.MiddleRight;
            Button8.LineColor = Color.FromArgb(0, 66, 119);
            Button8.Location = new Point(366, 5);
            Button8.Name = "Button8";
            Button8.Size = new Size(134, 29);
            Button8.TabIndex = 68;
            Button8.Text = "Default Windows";
            Button8.UseVisualStyleBackColor = false;
            // 
            // Button9
            // 
            Button9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.ImageAlign = ContentAlignment.MiddleRight;
            Button9.LineColor = Color.FromArgb(90, 134, 117);
            Button9.Location = new Point(230, 5);
            Button9.Name = "Button9";
            Button9.Size = new Size(134, 29);
            Button9.TabIndex = 67;
            Button9.Text = "Current applied";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            Label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label12.Location = new Point(5, 5);
            Label12.Name = "Label12";
            Label12.Size = new Size(71, 28);
            Label12.TabIndex = 4;
            Label12.Text = "Open from:";
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox2
            // 
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(PictureBox6);
            GroupBox2.Controls.Add(Label2);
            GroupBox2.Controls.Add(Label18);
            GroupBox2.Controls.Add(GroupBox4);
            GroupBox2.Controls.Add(Label17);
            GroupBox2.Controls.Add(GroupBox3);
            GroupBox2.Controls.Add(Label16);
            GroupBox2.Controls.Add(Label15);
            GroupBox2.Controls.Add(PictureBox5);
            GroupBox2.Controls.Add(PictureBox4);
            GroupBox2.Controls.Add(Label3);
            GroupBox2.Controls.Add(PictureBox2);
            GroupBox2.Enabled = false;
            GroupBox2.Location = new Point(12, 127);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(552, 129);
            GroupBox2.TabIndex = 8;
            // 
            // PictureBox6
            // 
            PictureBox6.BackColor = Color.Transparent;
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(3, 4);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(35, 35);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 74;
            PictureBox6.TabStop = false;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label2.Location = new Point(40, 4);
            Label2.Name = "Label2";
            Label2.Size = new Size(126, 35);
            Label2.TabIndex = 73;
            Label2.Text = "Cursor";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label18
            // 
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label18.Location = new Point(345, 10);
            Label18.Name = "Label18";
            Label18.Size = new Size(203, 24);
            Label18.TabIndex = 72;
            Label18.Text = "Border";
            Label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GroupBox4
            // 
            GroupBox4.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox4.Controls.Add(Button13);
            GroupBox4.Controls.Add(Trackbar4);
            GroupBox4.Controls.Add(CheckBox4);
            GroupBox4.Controls.Add(CheckBox3);
            GroupBox4.Controls.Add(SecondaryColor1);
            GroupBox4.Controls.Add(SecondaryColor2);
            GroupBox4.Controls.Add(ComboBox2);
            GroupBox4.Location = new Point(345, 40);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Size = new Size(203, 86);
            GroupBox4.TabIndex = 71;
            GroupBox4.Text = "GroupBox4";
            // 
            // Button13
            // 
            Button13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button13.BackColor = Color.FromArgb(51, 51, 51);
            Button13.DrawOnGlass = false;
            Button13.Font = new Font("Segoe UI", 9.0f);
            Button13.ForeColor = Color.White;
            Button13.Image = null;
            Button13.LineColor = Color.FromArgb(0, 81, 210);
            Button13.Location = new Point(165, 59);
            Button13.Name = "Button13";
            Button13.Size = new Size(34, 24);
            Button13.TabIndex = 134;
            Button13.UseVisualStyleBackColor = false;
            // 
            // Trackbar4
            // 
            Trackbar4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar4.LargeChange = 2;
            Trackbar4.Location = new Point(33, 63);
            Trackbar4.Maximum = 100;
            Trackbar4.Minimum = 0;
            Trackbar4.Name = "Trackbar4";
            Trackbar4.Size = new Size(126, 19);
            Trackbar4.SmallChange = 1;
            Trackbar4.TabIndex = 133;
            Trackbar4.Value = 0;
            // 
            // CheckBox4
            // 
            CheckBox4.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox4.Checked = false;
            CheckBox4.Font = new Font("Segoe UI", 9.0f);
            CheckBox4.ForeColor = Color.White;
            CheckBox4.Location = new Point(3, 31);
            CheckBox4.Name = "CheckBox4";
            CheckBox4.Size = new Size(24, 24);
            CheckBox4.TabIndex = 52;
            CheckBox4.Text = null;
            // 
            // CheckBox3
            // 
            CheckBox3.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox3.Checked = false;
            CheckBox3.Font = new Font("Segoe UI", 9.0f);
            CheckBox3.ForeColor = Color.White;
            CheckBox3.Location = new Point(3, 60);
            CheckBox3.Name = "CheckBox3";
            CheckBox3.Size = new Size(24, 24);
            CheckBox3.TabIndex = 59;
            CheckBox3.Text = null;
            // 
            // SecondaryColor1
            // 
            SecondaryColor1.AllowDrop = true;
            SecondaryColor1.BackColor = Color.FromArgb(47, 47, 48);
            SecondaryColor1.DefaultColor = Color.FromArgb(64, 65, 75);
            SecondaryColor1.DontShowInfo = false;
            SecondaryColor1.Location = new Point(5, 4);
            SecondaryColor1.Margin = new Padding(4, 3, 4, 3);
            SecondaryColor1.Name = "SecondaryColor1";
            SecondaryColor1.Size = new Size(95, 21);
            SecondaryColor1.TabIndex = 31;
            // 
            // SecondaryColor2
            // 
            SecondaryColor2.AllowDrop = true;
            SecondaryColor2.BackColor = Color.FromArgb(47, 47, 48);
            SecondaryColor2.DefaultColor = Color.FromArgb(64, 65, 75);
            SecondaryColor2.DontShowInfo = false;
            SecondaryColor2.Location = new Point(104, 4);
            SecondaryColor2.Margin = new Padding(4, 3, 4, 3);
            SecondaryColor2.Name = "SecondaryColor2";
            SecondaryColor2.Size = new Size(95, 21);
            SecondaryColor2.TabIndex = 33;
            // 
            // ComboBox2
            // 
            ComboBox2.BackColor = Color.FromArgb(0, 43, 113);
            ComboBox2.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox2.Font = new Font("Segoe UI", 9.0f);
            ComboBox2.ForeColor = Color.White;
            ComboBox2.FormattingEnabled = true;
            ComboBox2.ItemHeight = 20;
            ComboBox2.Items.AddRange(new object[] { "Vertical", "Horizontal", "Forward Diagonal", "Backward Diagonal", "Circle" });
            ComboBox2.Location = new Point(33, 30);
            ComboBox2.Name = "ComboBox2";
            ComboBox2.Size = new Size(166, 26);
            ComboBox2.TabIndex = 35;
            // 
            // Label17
            // 
            Label17.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label17.Location = new Point(139, 10);
            Label17.Name = "Label17";
            Label17.Size = new Size(203, 24);
            Label17.TabIndex = 70;
            Label17.Text = "Background";
            Label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GroupBox3
            // 
            GroupBox3.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox3.Controls.Add(Button12);
            GroupBox3.Controls.Add(Trackbar3);
            GroupBox3.Controls.Add(PrimaryColor2);
            GroupBox3.Controls.Add(PrimaryColor1);
            GroupBox3.Controls.Add(ComboBox1);
            GroupBox3.Controls.Add(CheckBox1);
            GroupBox3.Controls.Add(CheckBox5);
            GroupBox3.Location = new Point(139, 40);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(203, 86);
            GroupBox3.TabIndex = 68;
            GroupBox3.Text = "GroupBox3";
            // 
            // Button12
            // 
            Button12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button12.BackColor = Color.FromArgb(51, 51, 51);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = null;
            Button12.LineColor = Color.FromArgb(0, 81, 210);
            Button12.Location = new Point(165, 59);
            Button12.Name = "Button12";
            Button12.Size = new Size(34, 24);
            Button12.TabIndex = 132;
            Button12.UseVisualStyleBackColor = false;
            // 
            // Trackbar3
            // 
            Trackbar3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar3.LargeChange = 2;
            Trackbar3.Location = new Point(33, 63);
            Trackbar3.Maximum = 100;
            Trackbar3.Minimum = 0;
            Trackbar3.Name = "Trackbar3";
            Trackbar3.Size = new Size(126, 19);
            Trackbar3.SmallChange = 1;
            Trackbar3.TabIndex = 131;
            Trackbar3.Value = 0;
            // 
            // PrimaryColor2
            // 
            PrimaryColor2.AllowDrop = true;
            PrimaryColor2.BackColor = Color.FromArgb(47, 47, 48);
            PrimaryColor2.DefaultColor = Color.White;
            PrimaryColor2.DontShowInfo = false;
            PrimaryColor2.Location = new Point(104, 4);
            PrimaryColor2.Margin = new Padding(4, 3, 4, 3);
            PrimaryColor2.Name = "PrimaryColor2";
            PrimaryColor2.Size = new Size(95, 21);
            PrimaryColor2.TabIndex = 13;
            // 
            // PrimaryColor1
            // 
            PrimaryColor1.AllowDrop = true;
            PrimaryColor1.BackColor = Color.FromArgb(47, 47, 48);
            PrimaryColor1.DefaultColor = Color.White;
            PrimaryColor1.DontShowInfo = false;
            PrimaryColor1.Location = new Point(5, 4);
            PrimaryColor1.Margin = new Padding(4, 3, 4, 3);
            PrimaryColor1.Name = "PrimaryColor1";
            PrimaryColor1.Size = new Size(95, 21);
            PrimaryColor1.TabIndex = 11;
            // 
            // ComboBox1
            // 
            ComboBox1.BackColor = Color.FromArgb(0, 43, 113);
            ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Items.AddRange(new object[] { "Vertical", "Horizontal", "Forward Diagonal", "Backward Diagonal", "Circle" });
            ComboBox1.Location = new Point(33, 30);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(166, 26);
            ComboBox1.TabIndex = 17;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(3, 31);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(24, 24);
            CheckBox1.TabIndex = 15;
            CheckBox1.Text = null;
            // 
            // CheckBox5
            // 
            CheckBox5.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox5.Checked = false;
            CheckBox5.Font = new Font("Segoe UI", 9.0f);
            CheckBox5.ForeColor = Color.White;
            CheckBox5.Location = new Point(3, 60);
            CheckBox5.Name = "CheckBox5";
            CheckBox5.Size = new Size(24, 24);
            CheckBox5.TabIndex = 49;
            CheckBox5.Text = null;
            // 
            // Label16
            // 
            Label16.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label16.Location = new Point(62, 71);
            Label16.Name = "Label16";
            Label16.Size = new Size(69, 24);
            Label16.TabIndex = 69;
            Label16.Text = "Gradience";
            Label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            Label15.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label15.Location = new Point(62, 100);
            Label15.Name = "Label15";
            Label15.Size = new Size(69, 24);
            Label15.TabIndex = 68;
            Label15.Text = "Noise";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(32, 100);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 54;
            PictureBox5.TabStop = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(32, 71);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.TabIndex = 53;
            PictureBox4.TabStop = false;
            // 
            // Label3
            // 
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(62, 42);
            Label3.Name = "Label3";
            Label3.Size = new Size(69, 24);
            Label3.TabIndex = 12;
            Label3.Text = "Color";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(32, 42);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.TabIndex = 9;
            PictureBox2.TabStop = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(199, 49, 61);
            Button3.Location = new Point(488, 642);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 34);
            Button3.TabIndex = 66;
            Button3.Text = "Cancel";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(34, 34, 34);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.ImageAlign = ContentAlignment.MiddleLeft;
            Button4.LineColor = Color.FromArgb(52, 20, 64);
            Button4.Location = new Point(695, 642);
            Button4.Name = "Button4";
            Button4.Size = new Size(180, 34);
            Button4.TabIndex = 65;
            Button4.Text = "Load into current theme";
            Button4.UseVisualStyleBackColor = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(Button5);
            GroupBox1.Controls.Add(Button10);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(FlowLayoutPanel1);
            GroupBox1.Controls.Add(PictureBox12);
            GroupBox1.Controls.Add(Label5);
            GroupBox1.Controls.Add(Trackbar1);
            GroupBox1.Controls.Add(Button6);
            GroupBox1.Controls.Add(Button2);
            GroupBox1.Controls.Add(Button1);
            GroupBox1.Location = new Point(567, 55);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(3);
            GroupBox1.Size = new Size(308, 468);
            GroupBox1.TabIndex = 7;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button5.BackColor = Color.FromArgb(43, 43, 43);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.ImageAlign = ContentAlignment.MiddleLeft;
            Button5.LineColor = Color.FromArgb(25, 99, 126);
            Button5.Location = new Point(149, 434);
            Button5.Name = "Button5";
            Button5.Size = new Size(152, 28);
            Button5.TabIndex = 66;
            Button5.Text = "Animate (3 Cycles)";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Button10
            // 
            Button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button10.BackColor = Color.FromArgb(43, 43, 43);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = null;
            Button10.LineColor = Color.FromArgb(199, 49, 61);
            Button10.Location = new Point(280, 406);
            Button10.Name = "Button10";
            Button10.Size = new Size(20, 21);
            Button10.TabIndex = 58;
            Button10.Text = "?";
            Button10.UseVisualStyleBackColor = false;
            // 
            // Label1
            // 
            Label1.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(10, 6);
            Label1.Name = "Label1";
            Label1.Size = new Size(291, 30);
            Label1.TabIndex = 8;
            Label1.Text = "Select a cursor to edit its properties";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FlowLayoutPanel1
            // 
            FlowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            FlowLayoutPanel1.AutoScroll = true;
            FlowLayoutPanel1.Controls.Add(Arrow);
            FlowLayoutPanel1.Controls.Add(Help);
            FlowLayoutPanel1.Controls.Add(AppLoading);
            FlowLayoutPanel1.Controls.Add(Busy);
            FlowLayoutPanel1.Controls.Add(Move_Cur);
            FlowLayoutPanel1.Controls.Add(Up);
            FlowLayoutPanel1.Controls.Add(NS);
            FlowLayoutPanel1.Controls.Add(EW);
            FlowLayoutPanel1.Controls.Add(NESW);
            FlowLayoutPanel1.Controls.Add(NWSE);
            FlowLayoutPanel1.Controls.Add(Pen);
            FlowLayoutPanel1.Controls.Add(None);
            FlowLayoutPanel1.Controls.Add(Link);
            FlowLayoutPanel1.Controls.Add(Pin);
            FlowLayoutPanel1.Controls.Add(Person);
            FlowLayoutPanel1.Controls.Add(IBeam);
            FlowLayoutPanel1.Controls.Add(Cross);
            FlowLayoutPanel1.Location = new Point(3, 41);
            FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            FlowLayoutPanel1.Padding = new Padding(4, 4, 0, 4);
            FlowLayoutPanel1.Size = new Size(302, 358);
            FlowLayoutPanel1.TabIndex = 6;
            // 
            // Arrow
            // 
            Arrow.Location = new Point(7, 7);
            Arrow.Name = "Arrow";
            Arrow.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Arrow.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Arrow.Prop_Cursor = Paths.CursorType.Arrow;
            Arrow.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Arrow.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Arrow.Prop_LoadingCircleBackGradient = false;
            Arrow.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_LoadingCircleBackNoise = false;
            Arrow.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Arrow.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Arrow.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Arrow.Prop_LoadingCircleHotGradient = false;
            Arrow.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_LoadingCircleHotNoise = false;
            Arrow.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Arrow.Prop_PrimaryColor1 = Color.White;
            Arrow.Prop_PrimaryColor2 = Color.White;
            Arrow.Prop_PrimaryColorGradient = false;
            Arrow.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_PrimaryNoise = false;
            Arrow.Prop_PrimaryNoiseOpacity = 0.25f;
            Arrow.Prop_Scale = 1.0f;
            Arrow.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Arrow.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Arrow.Prop_SecondaryColorGradient = false;
            Arrow.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_SecondaryNoise = false;
            Arrow.Prop_SecondaryNoiseOpacity = 0.25f;
            Arrow.Prop_Shadow_Blur = 5;
            Arrow.Prop_Shadow_Color = Color.Black;
            Arrow.Prop_Shadow_Enabled = false;
            Arrow.Prop_Shadow_OffsetX = 2;
            Arrow.Prop_Shadow_OffsetY = 2;
            Arrow.Prop_Shadow_Opacity = 0.3f;
            Arrow.Size = new Size(64, 64);
            Arrow.TabIndex = 5;
            // 
            // Help
            // 
            Help.Location = new Point(77, 7);
            Help.Name = "Help";
            Help.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Help.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Help.Prop_Cursor = Paths.CursorType.Help;
            Help.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Help.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Help.Prop_LoadingCircleBackGradient = false;
            Help.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_LoadingCircleBackNoise = false;
            Help.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Help.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Help.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Help.Prop_LoadingCircleHotGradient = false;
            Help.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_LoadingCircleHotNoise = false;
            Help.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Help.Prop_PrimaryColor1 = Color.White;
            Help.Prop_PrimaryColor2 = Color.White;
            Help.Prop_PrimaryColorGradient = false;
            Help.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_PrimaryNoise = false;
            Help.Prop_PrimaryNoiseOpacity = 0.25f;
            Help.Prop_Scale = 1.0f;
            Help.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Help.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Help.Prop_SecondaryColorGradient = false;
            Help.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_SecondaryNoise = false;
            Help.Prop_SecondaryNoiseOpacity = 0.25f;
            Help.Prop_Shadow_Blur = 5;
            Help.Prop_Shadow_Color = Color.Black;
            Help.Prop_Shadow_Enabled = false;
            Help.Prop_Shadow_OffsetX = 2;
            Help.Prop_Shadow_OffsetY = 2;
            Help.Prop_Shadow_Opacity = 0.3f;
            Help.Size = new Size(64, 64);
            Help.TabIndex = 6;
            // 
            // AppLoading
            // 
            AppLoading.Location = new Point(147, 7);
            AppLoading.Name = "AppLoading";
            AppLoading.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            AppLoading.Prop_CircleStyle = Paths.CircleStyle.Aero;
            AppLoading.Prop_Cursor = Paths.CursorType.AppLoading;
            AppLoading.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            AppLoading.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            AppLoading.Prop_LoadingCircleBackGradient = false;
            AppLoading.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_LoadingCircleBackNoise = false;
            AppLoading.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            AppLoading.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            AppLoading.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            AppLoading.Prop_LoadingCircleHotGradient = false;
            AppLoading.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_LoadingCircleHotNoise = false;
            AppLoading.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            AppLoading.Prop_PrimaryColor1 = Color.White;
            AppLoading.Prop_PrimaryColor2 = Color.White;
            AppLoading.Prop_PrimaryColorGradient = false;
            AppLoading.Prop_PrimaryColorGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_PrimaryNoise = false;
            AppLoading.Prop_PrimaryNoiseOpacity = 0.25f;
            AppLoading.Prop_Scale = 1.0f;
            AppLoading.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            AppLoading.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            AppLoading.Prop_SecondaryColorGradient = false;
            AppLoading.Prop_SecondaryColorGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_SecondaryNoise = false;
            AppLoading.Prop_SecondaryNoiseOpacity = 0.25f;
            AppLoading.Prop_Shadow_Blur = 5;
            AppLoading.Prop_Shadow_Color = Color.Black;
            AppLoading.Prop_Shadow_Enabled = false;
            AppLoading.Prop_Shadow_OffsetX = 2;
            AppLoading.Prop_Shadow_OffsetY = 2;
            AppLoading.Prop_Shadow_Opacity = 0.3f;
            AppLoading.Size = new Size(64, 64);
            AppLoading.TabIndex = 6;
            // 
            // Busy
            // 
            Busy.Location = new Point(217, 7);
            Busy.Name = "Busy";
            Busy.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Busy.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Busy.Prop_Cursor = Paths.CursorType.Busy;
            Busy.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Busy.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Busy.Prop_LoadingCircleBackGradient = false;
            Busy.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_LoadingCircleBackNoise = false;
            Busy.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Busy.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Busy.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Busy.Prop_LoadingCircleHotGradient = false;
            Busy.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_LoadingCircleHotNoise = false;
            Busy.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Busy.Prop_PrimaryColor1 = Color.White;
            Busy.Prop_PrimaryColor2 = Color.White;
            Busy.Prop_PrimaryColorGradient = false;
            Busy.Prop_PrimaryColorGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_PrimaryNoise = false;
            Busy.Prop_PrimaryNoiseOpacity = 0.25f;
            Busy.Prop_Scale = 1.0f;
            Busy.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Busy.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Busy.Prop_SecondaryColorGradient = false;
            Busy.Prop_SecondaryColorGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_SecondaryNoise = false;
            Busy.Prop_SecondaryNoiseOpacity = 0.25f;
            Busy.Prop_Shadow_Blur = 5;
            Busy.Prop_Shadow_Color = Color.Black;
            Busy.Prop_Shadow_Enabled = false;
            Busy.Prop_Shadow_OffsetX = 2;
            Busy.Prop_Shadow_OffsetY = 2;
            Busy.Prop_Shadow_Opacity = 0.3f;
            Busy.Size = new Size(64, 64);
            Busy.TabIndex = 7;
            // 
            // Move_Cur
            // 
            Move_Cur.Location = new Point(7, 77);
            Move_Cur.Name = "Move_Cur";
            Move_Cur.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Move_Cur.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Move_Cur.Prop_Cursor = Paths.CursorType.Move;
            Move_Cur.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Move_Cur.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Move_Cur.Prop_LoadingCircleBackGradient = false;
            Move_Cur.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_LoadingCircleBackNoise = false;
            Move_Cur.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Move_Cur.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Move_Cur.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Move_Cur.Prop_LoadingCircleHotGradient = false;
            Move_Cur.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_LoadingCircleHotNoise = false;
            Move_Cur.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Move_Cur.Prop_PrimaryColor1 = Color.White;
            Move_Cur.Prop_PrimaryColor2 = Color.White;
            Move_Cur.Prop_PrimaryColorGradient = false;
            Move_Cur.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_PrimaryNoise = false;
            Move_Cur.Prop_PrimaryNoiseOpacity = 0.25f;
            Move_Cur.Prop_Scale = 1.0f;
            Move_Cur.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Move_Cur.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Move_Cur.Prop_SecondaryColorGradient = false;
            Move_Cur.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_SecondaryNoise = false;
            Move_Cur.Prop_SecondaryNoiseOpacity = 0.25f;
            Move_Cur.Prop_Shadow_Blur = 5;
            Move_Cur.Prop_Shadow_Color = Color.Black;
            Move_Cur.Prop_Shadow_Enabled = false;
            Move_Cur.Prop_Shadow_OffsetX = 2;
            Move_Cur.Prop_Shadow_OffsetY = 2;
            Move_Cur.Prop_Shadow_Opacity = 0.3f;
            Move_Cur.Size = new Size(64, 64);
            Move_Cur.TabIndex = 8;
            // 
            // Up
            // 
            Up.Location = new Point(77, 77);
            Up.Name = "Up";
            Up.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Up.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Up.Prop_Cursor = Paths.CursorType.Up;
            Up.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Up.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Up.Prop_LoadingCircleBackGradient = false;
            Up.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_LoadingCircleBackNoise = false;
            Up.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Up.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Up.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Up.Prop_LoadingCircleHotGradient = false;
            Up.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_LoadingCircleHotNoise = false;
            Up.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Up.Prop_PrimaryColor1 = Color.White;
            Up.Prop_PrimaryColor2 = Color.White;
            Up.Prop_PrimaryColorGradient = false;
            Up.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_PrimaryNoise = false;
            Up.Prop_PrimaryNoiseOpacity = 0.25f;
            Up.Prop_Scale = 1.0f;
            Up.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Up.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Up.Prop_SecondaryColorGradient = false;
            Up.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_SecondaryNoise = false;
            Up.Prop_SecondaryNoiseOpacity = 0.25f;
            Up.Prop_Shadow_Blur = 5;
            Up.Prop_Shadow_Color = Color.Black;
            Up.Prop_Shadow_Enabled = false;
            Up.Prop_Shadow_OffsetX = 2;
            Up.Prop_Shadow_OffsetY = 2;
            Up.Prop_Shadow_Opacity = 0.3f;
            Up.Size = new Size(64, 64);
            Up.TabIndex = 9;
            // 
            // NS
            // 
            NS.Location = new Point(147, 77);
            NS.Name = "NS";
            NS.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            NS.Prop_CircleStyle = Paths.CircleStyle.Aero;
            NS.Prop_Cursor = Paths.CursorType.NS;
            NS.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            NS.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            NS.Prop_LoadingCircleBackGradient = false;
            NS.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_LoadingCircleBackNoise = false;
            NS.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            NS.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            NS.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            NS.Prop_LoadingCircleHotGradient = false;
            NS.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_LoadingCircleHotNoise = false;
            NS.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            NS.Prop_PrimaryColor1 = Color.White;
            NS.Prop_PrimaryColor2 = Color.White;
            NS.Prop_PrimaryColorGradient = false;
            NS.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_PrimaryNoise = false;
            NS.Prop_PrimaryNoiseOpacity = 0.25f;
            NS.Prop_Scale = 1.0f;
            NS.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            NS.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            NS.Prop_SecondaryColorGradient = false;
            NS.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_SecondaryNoise = false;
            NS.Prop_SecondaryNoiseOpacity = 0.25f;
            NS.Prop_Shadow_Blur = 5;
            NS.Prop_Shadow_Color = Color.Black;
            NS.Prop_Shadow_Enabled = false;
            NS.Prop_Shadow_OffsetX = 2;
            NS.Prop_Shadow_OffsetY = 2;
            NS.Prop_Shadow_Opacity = 0.3f;
            NS.Size = new Size(64, 64);
            NS.TabIndex = 10;
            // 
            // EW
            // 
            EW.Location = new Point(217, 77);
            EW.Name = "EW";
            EW.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            EW.Prop_CircleStyle = Paths.CircleStyle.Aero;
            EW.Prop_Cursor = Paths.CursorType.EW;
            EW.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            EW.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            EW.Prop_LoadingCircleBackGradient = false;
            EW.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_LoadingCircleBackNoise = false;
            EW.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            EW.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            EW.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            EW.Prop_LoadingCircleHotGradient = false;
            EW.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_LoadingCircleHotNoise = false;
            EW.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            EW.Prop_PrimaryColor1 = Color.White;
            EW.Prop_PrimaryColor2 = Color.White;
            EW.Prop_PrimaryColorGradient = false;
            EW.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_PrimaryNoise = false;
            EW.Prop_PrimaryNoiseOpacity = 0.25f;
            EW.Prop_Scale = 1.0f;
            EW.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            EW.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            EW.Prop_SecondaryColorGradient = false;
            EW.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_SecondaryNoise = false;
            EW.Prop_SecondaryNoiseOpacity = 0.25f;
            EW.Prop_Shadow_Blur = 5;
            EW.Prop_Shadow_Color = Color.Black;
            EW.Prop_Shadow_Enabled = false;
            EW.Prop_Shadow_OffsetX = 2;
            EW.Prop_Shadow_OffsetY = 2;
            EW.Prop_Shadow_Opacity = 0.3f;
            EW.Size = new Size(64, 64);
            EW.TabIndex = 11;
            // 
            // NESW
            // 
            NESW.Location = new Point(7, 147);
            NESW.Name = "NESW";
            NESW.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            NESW.Prop_CircleStyle = Paths.CircleStyle.Aero;
            NESW.Prop_Cursor = Paths.CursorType.NESW;
            NESW.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            NESW.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            NESW.Prop_LoadingCircleBackGradient = false;
            NESW.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_LoadingCircleBackNoise = false;
            NESW.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            NESW.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            NESW.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            NESW.Prop_LoadingCircleHotGradient = false;
            NESW.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_LoadingCircleHotNoise = false;
            NESW.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            NESW.Prop_PrimaryColor1 = Color.White;
            NESW.Prop_PrimaryColor2 = Color.White;
            NESW.Prop_PrimaryColorGradient = false;
            NESW.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_PrimaryNoise = false;
            NESW.Prop_PrimaryNoiseOpacity = 0.25f;
            NESW.Prop_Scale = 1.0f;
            NESW.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            NESW.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            NESW.Prop_SecondaryColorGradient = false;
            NESW.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_SecondaryNoise = false;
            NESW.Prop_SecondaryNoiseOpacity = 0.25f;
            NESW.Prop_Shadow_Blur = 5;
            NESW.Prop_Shadow_Color = Color.Black;
            NESW.Prop_Shadow_Enabled = false;
            NESW.Prop_Shadow_OffsetX = 2;
            NESW.Prop_Shadow_OffsetY = 2;
            NESW.Prop_Shadow_Opacity = 0.3f;
            NESW.Size = new Size(64, 64);
            NESW.TabIndex = 12;
            // 
            // NWSE
            // 
            NWSE.Location = new Point(77, 147);
            NWSE.Name = "NWSE";
            NWSE.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            NWSE.Prop_CircleStyle = Paths.CircleStyle.Aero;
            NWSE.Prop_Cursor = Paths.CursorType.NWSE;
            NWSE.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            NWSE.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            NWSE.Prop_LoadingCircleBackGradient = false;
            NWSE.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_LoadingCircleBackNoise = false;
            NWSE.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            NWSE.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            NWSE.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            NWSE.Prop_LoadingCircleHotGradient = false;
            NWSE.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_LoadingCircleHotNoise = false;
            NWSE.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            NWSE.Prop_PrimaryColor1 = Color.White;
            NWSE.Prop_PrimaryColor2 = Color.White;
            NWSE.Prop_PrimaryColorGradient = false;
            NWSE.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_PrimaryNoise = false;
            NWSE.Prop_PrimaryNoiseOpacity = 0.25f;
            NWSE.Prop_Scale = 1.0f;
            NWSE.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            NWSE.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            NWSE.Prop_SecondaryColorGradient = false;
            NWSE.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_SecondaryNoise = false;
            NWSE.Prop_SecondaryNoiseOpacity = 0.25f;
            NWSE.Prop_Shadow_Blur = 5;
            NWSE.Prop_Shadow_Color = Color.Black;
            NWSE.Prop_Shadow_Enabled = false;
            NWSE.Prop_Shadow_OffsetX = 2;
            NWSE.Prop_Shadow_OffsetY = 2;
            NWSE.Prop_Shadow_Opacity = 0.3f;
            NWSE.Size = new Size(64, 64);
            NWSE.TabIndex = 13;
            // 
            // Pen
            // 
            Pen.Location = new Point(147, 147);
            Pen.Name = "Pen";
            Pen.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Pen.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Pen.Prop_Cursor = Paths.CursorType.Pen;
            Pen.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Pen.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Pen.Prop_LoadingCircleBackGradient = false;
            Pen.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_LoadingCircleBackNoise = false;
            Pen.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Pen.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Pen.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Pen.Prop_LoadingCircleHotGradient = false;
            Pen.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_LoadingCircleHotNoise = false;
            Pen.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Pen.Prop_PrimaryColor1 = Color.White;
            Pen.Prop_PrimaryColor2 = Color.White;
            Pen.Prop_PrimaryColorGradient = false;
            Pen.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_PrimaryNoise = false;
            Pen.Prop_PrimaryNoiseOpacity = 0.25f;
            Pen.Prop_Scale = 1.0f;
            Pen.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Pen.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Pen.Prop_SecondaryColorGradient = false;
            Pen.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_SecondaryNoise = false;
            Pen.Prop_SecondaryNoiseOpacity = 0.25f;
            Pen.Prop_Shadow_Blur = 5;
            Pen.Prop_Shadow_Color = Color.Black;
            Pen.Prop_Shadow_Enabled = false;
            Pen.Prop_Shadow_OffsetX = 2;
            Pen.Prop_Shadow_OffsetY = 2;
            Pen.Prop_Shadow_Opacity = 0.3f;
            Pen.Size = new Size(64, 64);
            Pen.TabIndex = 14;
            // 
            // None
            // 
            None.Location = new Point(217, 147);
            None.Name = "None";
            None.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            None.Prop_CircleStyle = Paths.CircleStyle.Aero;
            None.Prop_Cursor = Paths.CursorType.None;
            None.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            None.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            None.Prop_LoadingCircleBackGradient = false;
            None.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_LoadingCircleBackNoise = false;
            None.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            None.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            None.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            None.Prop_LoadingCircleHotGradient = false;
            None.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_LoadingCircleHotNoise = false;
            None.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            None.Prop_PrimaryColor1 = Color.White;
            None.Prop_PrimaryColor2 = Color.White;
            None.Prop_PrimaryColorGradient = false;
            None.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_PrimaryNoise = false;
            None.Prop_PrimaryNoiseOpacity = 0.25f;
            None.Prop_Scale = 1.0f;
            None.Prop_SecondaryColor1 = Color.Red;
            None.Prop_SecondaryColor2 = Color.Red;
            None.Prop_SecondaryColorGradient = false;
            None.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_SecondaryNoise = false;
            None.Prop_SecondaryNoiseOpacity = 0.25f;
            None.Prop_Shadow_Blur = 5;
            None.Prop_Shadow_Color = Color.Black;
            None.Prop_Shadow_Enabled = false;
            None.Prop_Shadow_OffsetX = 2;
            None.Prop_Shadow_OffsetY = 2;
            None.Prop_Shadow_Opacity = 0.3f;
            None.Size = new Size(64, 64);
            None.TabIndex = 15;
            // 
            // Link
            // 
            Link.Location = new Point(7, 217);
            Link.Name = "Link";
            Link.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Link.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Link.Prop_Cursor = Paths.CursorType.Link;
            Link.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Link.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Link.Prop_LoadingCircleBackGradient = false;
            Link.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_LoadingCircleBackNoise = false;
            Link.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Link.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Link.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Link.Prop_LoadingCircleHotGradient = false;
            Link.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_LoadingCircleHotNoise = false;
            Link.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Link.Prop_PrimaryColor1 = Color.White;
            Link.Prop_PrimaryColor2 = Color.White;
            Link.Prop_PrimaryColorGradient = false;
            Link.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_PrimaryNoise = false;
            Link.Prop_PrimaryNoiseOpacity = 0.25f;
            Link.Prop_Scale = 1.0f;
            Link.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Link.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Link.Prop_SecondaryColorGradient = false;
            Link.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_SecondaryNoise = false;
            Link.Prop_SecondaryNoiseOpacity = 0.25f;
            Link.Prop_Shadow_Blur = 5;
            Link.Prop_Shadow_Color = Color.Black;
            Link.Prop_Shadow_Enabled = false;
            Link.Prop_Shadow_OffsetX = 2;
            Link.Prop_Shadow_OffsetY = 2;
            Link.Prop_Shadow_Opacity = 0.3f;
            Link.Size = new Size(64, 64);
            Link.TabIndex = 16;
            // 
            // Pin
            // 
            Pin.Location = new Point(77, 217);
            Pin.Name = "Pin";
            Pin.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Pin.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Pin.Prop_Cursor = Paths.CursorType.Pin;
            Pin.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Pin.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Pin.Prop_LoadingCircleBackGradient = false;
            Pin.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_LoadingCircleBackNoise = false;
            Pin.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Pin.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Pin.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Pin.Prop_LoadingCircleHotGradient = false;
            Pin.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_LoadingCircleHotNoise = false;
            Pin.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Pin.Prop_PrimaryColor1 = Color.White;
            Pin.Prop_PrimaryColor2 = Color.White;
            Pin.Prop_PrimaryColorGradient = false;
            Pin.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_PrimaryNoise = false;
            Pin.Prop_PrimaryNoiseOpacity = 0.25f;
            Pin.Prop_Scale = 1.0f;
            Pin.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Pin.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Pin.Prop_SecondaryColorGradient = false;
            Pin.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_SecondaryNoise = false;
            Pin.Prop_SecondaryNoiseOpacity = 0.25f;
            Pin.Prop_Shadow_Blur = 5;
            Pin.Prop_Shadow_Color = Color.Black;
            Pin.Prop_Shadow_Enabled = false;
            Pin.Prop_Shadow_OffsetX = 2;
            Pin.Prop_Shadow_OffsetY = 2;
            Pin.Prop_Shadow_Opacity = 0.3f;
            Pin.Size = new Size(64, 64);
            Pin.TabIndex = 17;
            // 
            // Person
            // 
            Person.Location = new Point(147, 217);
            Person.Name = "Person";
            Person.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Person.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Person.Prop_Cursor = Paths.CursorType.Person;
            Person.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Person.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Person.Prop_LoadingCircleBackGradient = false;
            Person.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_LoadingCircleBackNoise = false;
            Person.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Person.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Person.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Person.Prop_LoadingCircleHotGradient = false;
            Person.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_LoadingCircleHotNoise = false;
            Person.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Person.Prop_PrimaryColor1 = Color.White;
            Person.Prop_PrimaryColor2 = Color.White;
            Person.Prop_PrimaryColorGradient = false;
            Person.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_PrimaryNoise = false;
            Person.Prop_PrimaryNoiseOpacity = 0.25f;
            Person.Prop_Scale = 1.0f;
            Person.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Person.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Person.Prop_SecondaryColorGradient = false;
            Person.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_SecondaryNoise = false;
            Person.Prop_SecondaryNoiseOpacity = 0.25f;
            Person.Prop_Shadow_Blur = 5;
            Person.Prop_Shadow_Color = Color.Black;
            Person.Prop_Shadow_Enabled = false;
            Person.Prop_Shadow_OffsetX = 2;
            Person.Prop_Shadow_OffsetY = 2;
            Person.Prop_Shadow_Opacity = 0.3f;
            Person.Size = new Size(64, 64);
            Person.TabIndex = 18;
            // 
            // IBeam
            // 
            IBeam.Location = new Point(217, 217);
            IBeam.Name = "IBeam";
            IBeam.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            IBeam.Prop_CircleStyle = Paths.CircleStyle.Aero;
            IBeam.Prop_Cursor = Paths.CursorType.IBeam;
            IBeam.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            IBeam.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            IBeam.Prop_LoadingCircleBackGradient = false;
            IBeam.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_LoadingCircleBackNoise = false;
            IBeam.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            IBeam.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            IBeam.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            IBeam.Prop_LoadingCircleHotGradient = false;
            IBeam.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_LoadingCircleHotNoise = false;
            IBeam.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            IBeam.Prop_PrimaryColor1 = Color.White;
            IBeam.Prop_PrimaryColor2 = Color.White;
            IBeam.Prop_PrimaryColorGradient = false;
            IBeam.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_PrimaryNoise = false;
            IBeam.Prop_PrimaryNoiseOpacity = 0.25f;
            IBeam.Prop_Scale = 1.0f;
            IBeam.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            IBeam.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            IBeam.Prop_SecondaryColorGradient = false;
            IBeam.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_SecondaryNoise = false;
            IBeam.Prop_SecondaryNoiseOpacity = 0.25f;
            IBeam.Prop_Shadow_Blur = 5;
            IBeam.Prop_Shadow_Color = Color.Black;
            IBeam.Prop_Shadow_Enabled = false;
            IBeam.Prop_Shadow_OffsetX = 2;
            IBeam.Prop_Shadow_OffsetY = 2;
            IBeam.Prop_Shadow_Opacity = 0.3f;
            IBeam.Size = new Size(64, 64);
            IBeam.TabIndex = 19;
            // 
            // Cross
            // 
            Cross.Location = new Point(7, 287);
            Cross.Name = "Cross";
            Cross.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Cross.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Cross.Prop_Cursor = Paths.CursorType.Cross;
            Cross.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Cross.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Cross.Prop_LoadingCircleBackGradient = false;
            Cross.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_LoadingCircleBackNoise = false;
            Cross.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Cross.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Cross.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Cross.Prop_LoadingCircleHotGradient = false;
            Cross.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_LoadingCircleHotNoise = false;
            Cross.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Cross.Prop_PrimaryColor1 = Color.White;
            Cross.Prop_PrimaryColor2 = Color.White;
            Cross.Prop_PrimaryColorGradient = false;
            Cross.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_PrimaryNoise = false;
            Cross.Prop_PrimaryNoiseOpacity = 0.25f;
            Cross.Prop_Scale = 1.0f;
            Cross.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Cross.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Cross.Prop_SecondaryColorGradient = false;
            Cross.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_SecondaryNoise = false;
            Cross.Prop_SecondaryNoiseOpacity = 0.25f;
            Cross.Prop_Shadow_Blur = 5;
            Cross.Prop_Shadow_Color = Color.Black;
            Cross.Prop_Shadow_Enabled = false;
            Cross.Prop_Shadow_OffsetX = 2;
            Cross.Prop_Shadow_OffsetY = 2;
            Cross.Prop_Shadow_Opacity = 0.3f;
            Cross.Size = new Size(64, 64);
            Cross.TabIndex = 20;
            // 
            // PictureBox12
            // 
            PictureBox12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(7, 404);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.TabIndex = 57;
            PictureBox12.TabStop = false;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(37, 404);
            Label5.Name = "Label5";
            Label5.Size = new Size(85, 24);
            Label5.TabIndex = 56;
            Label5.Text = "Scaling (1x)";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Trackbar1.LargeChange = 50;
            Trackbar1.Location = new Point(128, 407);
            Trackbar1.Maximum = 320;
            Trackbar1.Minimum = 100;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(146, 19);
            Trackbar1.SmallChange = 20;
            Trackbar1.TabIndex = 9;
            Trackbar1.Value = 100;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Enabled = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.ImageAlign = ContentAlignment.MiddleLeft;
            Button6.LineColor = Color.FromArgb(99, 148, 153);
            Button6.Location = new Point(73, 434);
            Button6.Name = "Button6";
            Button6.Size = new Size(70, 28);
            Button6.TabIndex = 67;
            Button6.Text = "to all";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button2.BackColor = Color.FromArgb(43, 43, 43);
            Button2.DrawOnGlass = false;
            Button2.Enabled = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = (Image)resources.GetObject("Button2.Image");
            Button2.LineColor = Color.FromArgb(99, 148, 153);
            Button2.Location = new Point(40, 434);
            Button2.Name = "Button2";
            Button2.Size = new Size(27, 28);
            Button2.TabIndex = 11;
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button1.BackColor = Color.FromArgb(43, 43, 43);
            Button1.DrawOnGlass = false;
            Button1.Enabled = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.LineColor = Color.FromArgb(38, 120, 143);
            Button1.Location = new Point(7, 434);
            Button1.Name = "Button1";
            Button1.Size = new Size(27, 28);
            Button1.TabIndex = 10;
            Button1.UseVisualStyleBackColor = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(567, 526);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(308, 102);
            AlertBox1.TabIndex = 137;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "- Shadow in \"Miscellaneous\" part is rendered by Windows, while custom shadow is r" + "endered by WinPaletter itself." + '\r' + '\n' + "- The more you enable custom shadow, the more Wi" + "nPaletter will take to render.";
            // 
            // CursorsStudio
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(887, 688);
            Controls.Add(AlertBox1);
            Controls.Add(GroupBox10);
            Controls.Add(GroupBox9);
            Controls.Add(GroupBox6);
            Controls.Add(GroupBox5);
            Controls.Add(Button11);
            Controls.Add(GroupBox13);
            Controls.Add(GroupBox2);
            Controls.Add(Button3);
            Controls.Add(Button4);
            Controls.Add(GroupBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CursorsStudio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cursors Studio";
            GroupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox24).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).EndInit();
            GroupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).EndInit();
            GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            GroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            GroupBox7.ResumeLayout(false);
            GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            GroupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            GroupBox4.ResumeLayout(false);
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            GroupBox1.ResumeLayout(false);
            FlowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            Load += new EventHandler(Form1_Load);
            Shown += new EventHandler(CursorsStudio_Shown);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(CursorsStudio_HelpButtonClicked);
            ResumeLayout(false);

        }
        internal CursorControl Arrow;
        internal FlowLayoutPanel FlowLayoutPanel1;
        internal CursorControl Help;
        internal CursorControl AppLoading;
        internal CursorControl Busy;
        internal CursorControl Move_Cur;
        internal CursorControl Up;
        internal CursorControl NS;
        internal CursorControl EW;
        internal CursorControl NESW;
        internal CursorControl NWSE;
        internal CursorControl Pen;
        internal CursorControl None;
        internal CursorControl Link;
        internal CursorControl Pin;
        internal CursorControl Person;
        internal CursorControl IBeam;
        internal CursorControl Cross;
        internal UI.WP.GroupBox GroupBox1;
        internal Label Label1;
        internal UI.WP.GroupBox GroupBox2;
        internal PictureBox PictureBox2;
        internal UI.Controllers.ColorItem PrimaryColor2;
        internal Label Label3;
        internal UI.Controllers.ColorItem PrimaryColor1;
        internal UI.WP.ComboBox ComboBox1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox5;
        internal UI.WP.ComboBox ComboBox2;
        internal UI.Controllers.ColorItem SecondaryColor2;
        internal UI.Controllers.ColorItem SecondaryColor1;
        internal UI.WP.CheckBox CheckBox3;
        internal PictureBox PictureBox5;
        internal PictureBox PictureBox4;
        internal UI.WP.CheckBox CheckBox4;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.CheckBox CheckBox6;
        internal UI.WP.CheckBox CheckBox7;
        internal UI.WP.ComboBox ComboBox3;
        internal UI.Controllers.ColorItem LoadingColor2;
        internal UI.Controllers.ColorItem LoadingColor1;
        internal UI.WP.ComboBox ComboBox4;
        internal UI.WP.CheckBox CheckBox8;
        internal UI.Controllers.ColorItem CircleColor2;
        internal UI.Controllers.ColorItem CircleColor1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Trackbar Trackbar1;
        internal PictureBox PictureBox12;
        internal Label Label5;
        internal UI.WP.Button Button5;
        internal Timer Timer1;
        internal UI.WP.Button Button6;
        internal UI.WP.Toggle Toggle1;
        internal UI.WP.GroupBox GroupBox13;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button10;
        internal PictureBox checker_img;
        internal UI.WP.Button Button11;
        internal PictureBox PictureBox9;
        internal UI.WP.CheckBox CheckBox9;
        internal UI.WP.Trackbar Trackbar2;
        internal Label Label11;
        internal PictureBox PictureBox21;
        internal UI.WP.Button trails_btn;
        internal PictureBox PictureBox22;
        internal UI.WP.CheckBox CheckBox10;
        internal UI.WP.ComboBox ComboBox5;
        internal UI.WP.ComboBox ComboBox6;
        internal Label Label18;
        internal UI.WP.GroupBox GroupBox4;
        internal Label Label17;
        internal UI.WP.GroupBox GroupBox3;
        internal Label Label16;
        internal Label Label15;
        internal UI.WP.GroupBox GroupBox5;
        internal Label Label4;
        internal UI.WP.GroupBox GroupBox7;
        internal Label Label9;
        internal UI.WP.GroupBox GroupBox8;
        internal Label Label19;
        internal Label Label20;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox8;
        internal Label Label21;
        internal PictureBox PictureBox10;
        internal UI.WP.GroupBox GroupBox6;
        internal UI.WP.GroupBox GroupBox9;
        internal PictureBox PictureBox6;
        internal Label Label2;
        internal PictureBox PictureBox3;
        internal Label Label13;
        internal Label Label8;
        internal PictureBox PictureBox14;
        internal Label Label10;
        internal PictureBox PictureBox15;
        internal PictureBox PictureBox11;
        internal Label Label7;
        internal PictureBox PictureBox13;
        internal Label Label6;
        internal UI.WP.Button Button12;
        internal UI.WP.Trackbar Trackbar3;
        internal UI.WP.Button Button13;
        internal UI.WP.Trackbar Trackbar4;
        internal UI.WP.Button Button14;
        internal UI.WP.Trackbar Trackbar5;
        internal UI.WP.Button Button15;
        internal UI.WP.Trackbar Trackbar6;
        internal UI.WP.GroupBox GroupBox10;
        internal UI.Controllers.ColorItem ColorItem1;
        internal PictureBox PictureBox16;
        internal Label Label14;
        internal Label Label24;
        internal Label Label25;
        internal PictureBox PictureBox17;
        internal PictureBox PictureBox18;
        internal Label Label26;
        internal PictureBox PictureBox19;
        internal UI.WP.CheckBox CheckBox11;
        internal PictureBox PictureBox24;
        internal UI.WP.Button Button19;
        internal UI.WP.Trackbar Trackbar10;
        internal UI.WP.Button Button18;
        internal UI.WP.Trackbar Trackbar9;
        internal Label Label22;
        internal PictureBox PictureBox20;
        internal UI.WP.Button Button17;
        internal UI.WP.Trackbar Trackbar8;
        internal UI.WP.Button Button16;
        internal UI.WP.Trackbar Trackbar7;
        internal UI.WP.AlertBox AlertBox1;
    }
}