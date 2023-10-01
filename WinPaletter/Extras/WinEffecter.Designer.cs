using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class WinEffecter : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(WinEffecter));
            OpenFileDialog1 = new OpenFileDialog();
            Timer1 = new Timer(components);
            Timer1.Tick += new EventHandler(Timer1_Tick);
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            PictureBox27 = new PictureBox();
            CheckBox21 = new UI.WP.CheckBox();
            PictureBox15 = new PictureBox();
            PictureBox2 = new PictureBox();
            CheckBox11 = new UI.WP.CheckBox();
            CheckBox1 = new UI.WP.CheckBox();
            PictureBox4 = new PictureBox();
            CheckBox2 = new UI.WP.CheckBox();
            CheckBox3 = new UI.WP.CheckBox();
            PictureBox3 = new PictureBox();
            TabPage2 = new TabPage();
            PictureBox14 = new PictureBox();
            PictureBox5 = new PictureBox();
            CheckBox4 = new UI.WP.CheckBox();
            CheckBox10 = new UI.WP.CheckBox();
            TabPage3 = new TabPage();
            CheckBox12 = new UI.WP.CheckBox();
            PictureBox1 = new PictureBox();
            MD = new UI.WP.Button();
            MD.Click += new EventHandler(MD_Click);
            PictureBox7 = new PictureBox();
            CheckBox6 = new UI.WP.CheckBox();
            Label6 = new Label();
            CheckBox5 = new UI.WP.CheckBox();
            PictureBox16 = new PictureBox();
            PictureBox6 = new PictureBox();
            ComboBox1 = new UI.WP.ComboBox();
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            TabPage4 = new TabPage();
            PictureBox9 = new PictureBox();
            PictureBox10 = new PictureBox();
            CheckBox7 = new UI.WP.CheckBox();
            CheckBox8 = new UI.WP.CheckBox();
            TabPage5 = new TabPage();
            PictureBox23 = new PictureBox();
            CheckBox17 = new UI.WP.CheckBox();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Label4 = new Label();
            PictureBox17 = new PictureBox();
            Trackbar5 = new UI.WP.Trackbar();
            Trackbar5.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar5_Scroll);
            ComboBox2 = new UI.WP.ComboBox();
            PictureBox12 = new PictureBox();
            CheckBox9 = new UI.WP.CheckBox();
            TabPage6 = new TabPage();
            ButtonR1 = new UI.Retro.ButtonR();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Label2 = new Label();
            PictureBox11 = new PictureBox();
            Trackbar3 = new UI.WP.Trackbar();
            Trackbar3.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar3_Scroll);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Label1 = new Label();
            PictureBox8 = new PictureBox();
            Trackbar2 = new UI.WP.Trackbar();
            Trackbar2.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar2_Scroll);
            TabPage7 = new TabPage();
            Panel1 = new Panel();
            Panel2 = new Panel();
            Label7 = new Label();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Label3 = new Label();
            PictureBox13 = new PictureBox();
            Trackbar4 = new UI.WP.Trackbar();
            Trackbar4.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar4_Scroll);
            TabPage8 = new TabPage();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Label5 = new Label();
            PictureBox20 = new PictureBox();
            Trackbar6 = new UI.WP.Trackbar();
            Trackbar6.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar6_Scroll);
            PictureBox19 = new PictureBox();
            CheckBox14 = new UI.WP.CheckBox();
            PictureBox18 = new PictureBox();
            CheckBox13 = new UI.WP.CheckBox();
            TabPage11 = new TabPage();
            AlertBox3 = new UI.WP.AlertBox();
            AlertBox2 = new UI.WP.AlertBox();
            AlertBox1 = new UI.WP.AlertBox();
            RadioButton3 = new UI.WP.RadioButton();
            PictureBox30 = new PictureBox();
            PictureBox26 = new PictureBox();
            RadioButton2 = new UI.WP.RadioButton();
            CheckBox20 = new UI.WP.CheckBox();
            CheckBox23 = new UI.WP.CheckBox();
            RadioButton1 = new UI.WP.RadioButton();
            PictureBox29 = new PictureBox();
            Label8 = new Label();
            TabPage10 = new TabPage();
            PictureBox28 = new PictureBox();
            CheckBox22 = new UI.WP.CheckBox();
            PictureBox22 = new PictureBox();
            CheckBox16 = new UI.WP.CheckBox();
            TabPage12 = new TabPage();
            AlertBox4 = new UI.WP.AlertBox();
            RadioImage7 = new UI.WP.RadioImage();
            RadioImage7.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage7_CheckedChanged);
            RadioImage6 = new UI.WP.RadioImage();
            RadioImage6.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage6_CheckedChanged);
            RadioImage5 = new UI.WP.RadioImage();
            RadioImage5.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage5_CheckedChanged);
            RadioImage4 = new UI.WP.RadioImage();
            RadioImage4.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage4_CheckedChanged);
            RadioImage3 = new UI.WP.RadioImage();
            RadioImage3.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage3_CheckedChanged);
            RadioImage2 = new UI.WP.RadioImage();
            RadioImage2.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage2_CheckedChanged);
            RadioImage1 = new UI.WP.RadioImage();
            RadioImage1.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage1_CheckedChanged);
            Panel3 = new Panel();
            Panel24 = new Panel();
            Label15 = new Label();
            P3 = new Panel();
            P2 = new Panel();
            P1 = new Panel();
            Panel20 = new Panel();
            Label14 = new Label();
            B3 = new Panel();
            B2 = new Panel();
            B1 = new Panel();
            Panel16 = new Panel();
            Label13 = new Label();
            G3 = new Panel();
            G2 = new Panel();
            G1 = new Panel();
            Panel12 = new Panel();
            Label11 = new Label();
            Y3 = new Panel();
            Y2 = new Panel();
            Y1 = new Panel();
            Panel8 = new Panel();
            Label10 = new Label();
            O3 = new Panel();
            O2 = new Panel();
            O1 = new Panel();
            Panel4 = new Panel();
            Label9 = new Label();
            R3 = new Panel();
            R2 = new Panel();
            R1 = new Panel();
            PictureBox33 = new PictureBox();
            PictureBox32 = new PictureBox();
            TabPage9 = new TabPage();
            PictureBox35 = new PictureBox();
            CheckBox26 = new UI.WP.CheckBox();
            PictureBox34 = new PictureBox();
            CheckBox25 = new UI.WP.CheckBox();
            AlertBox6 = new UI.WP.AlertBox();
            AlertBox5 = new UI.WP.AlertBox();
            PictureBox31 = new PictureBox();
            CheckBox24 = new UI.WP.CheckBox();
            PictureBox21 = new PictureBox();
            CheckBox15 = new UI.WP.CheckBox();
            PictureBox24 = new PictureBox();
            CheckBox18 = new UI.WP.CheckBox();
            PictureBox25 = new PictureBox();
            CheckBox19 = new UI.WP.CheckBox();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            GroupBox12 = new UI.WP.GroupBox();
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label12 = new Label();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            EffectsEnabled = new UI.WP.Toggle();
            EffectsEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(EffectsEnabled_CheckedChanged);
            checker_img = new PictureBox();
            CheckBox27 = new UI.WP.CheckBox();
            PictureBox36 = new PictureBox();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            TabPage7.SuspendLayout();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            TabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).BeginInit();
            TabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).BeginInit();
            TabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            TabPage12.SuspendLayout();
            Panel3.SuspendLayout();
            Panel24.SuspendLayout();
            Panel20.SuspendLayout();
            Panel16.SuspendLayout();
            Panel12.SuspendLayout();
            Panel8.SuspendLayout();
            Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox32).BeginInit();
            TabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox35).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).BeginInit();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // Timer1
            // 
            Timer1.Enabled = true;
            Timer1.Interval = 500;
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Controls.Add(TabPage4);
            TabControl1.Controls.Add(TabPage5);
            TabControl1.Controls.Add(TabPage6);
            TabControl1.Controls.Add(TabPage7);
            TabControl1.Controls.Add(TabPage8);
            TabControl1.Controls.Add(TabPage11);
            TabControl1.Controls.Add(TabPage10);
            TabControl1.Controls.Add(TabPage12);
            TabControl1.Controls.Add(TabPage9);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ItemSize = new Size(30, 170);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(12, 57);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(769, 392);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 209;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(CheckBox27);
            TabPage1.Controls.Add(PictureBox36);
            TabPage1.Controls.Add(PictureBox27);
            TabPage1.Controls.Add(CheckBox21);
            TabPage1.Controls.Add(PictureBox15);
            TabPage1.Controls.Add(PictureBox2);
            TabPage1.Controls.Add(CheckBox11);
            TabPage1.Controls.Add(CheckBox1);
            TabPage1.Controls.Add(PictureBox4);
            TabPage1.Controls.Add(CheckBox2);
            TabPage1.Controls.Add(CheckBox3);
            TabPage1.Controls.Add(PictureBox3);
            TabPage1.Location = new Point(174, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(591, 384);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Window";
            // 
            // PictureBox27
            // 
            PictureBox27.BackColor = Color.Transparent;
            PictureBox27.Image = (Image)resources.GetObject("PictureBox27.Image");
            PictureBox27.Location = new Point(6, 156);
            PictureBox27.Name = "PictureBox27";
            PictureBox27.Size = new Size(24, 24);
            PictureBox27.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox27.TabIndex = 228;
            PictureBox27.TabStop = false;
            // 
            // CheckBox21
            // 
            CheckBox21.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox21.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox21.Checked = false;
            CheckBox21.Font = new Font("Segoe UI", 9.0f);
            CheckBox21.ForeColor = Color.White;
            CheckBox21.Location = new Point(36, 156);
            CheckBox21.Name = "CheckBox21";
            CheckBox21.Size = new Size(549, 24);
            CheckBox21.TabIndex = 227;
            CheckBox21.Text = "Shake a window to minimize other windows (Windows 7 and later)";
            // 
            // PictureBox15
            // 
            PictureBox15.BackColor = Color.Transparent;
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(6, 126);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox15.TabIndex = 9;
            PictureBox15.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.BackColor = Color.Transparent;
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(6, 6);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox2.TabIndex = 3;
            PictureBox2.TabStop = false;
            // 
            // CheckBox11
            // 
            CheckBox11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox11.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox11.Checked = false;
            CheckBox11.Font = new Font("Segoe UI", 9.0f);
            CheckBox11.ForeColor = Color.White;
            CheckBox11.Location = new Point(36, 126);
            CheckBox11.Name = "CheckBox11";
            CheckBox11.Size = new Size(549, 24);
            CheckBox11.TabIndex = 8;
            CheckBox11.Text = "Show window contents while dragging";
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(36, 6);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(549, 24);
            CheckBox1.TabIndex = 2;
            CheckBox1.Text = @"Animations (Open\Close\Minimize\Maximize\...)";
            // 
            // PictureBox4
            // 
            PictureBox4.BackColor = Color.Transparent;
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(6, 96);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox4.TabIndex = 7;
            PictureBox4.TabStop = false;
            // 
            // CheckBox2
            // 
            CheckBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = false;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(36, 66);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(549, 24);
            CheckBox2.TabIndex = 4;
            CheckBox2.Text = "Borders shadow (Windows Vista and later)";
            // 
            // CheckBox3
            // 
            CheckBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox3.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox3.Checked = false;
            CheckBox3.Font = new Font("Segoe UI", 9.0f);
            CheckBox3.ForeColor = Color.White;
            CheckBox3.Location = new Point(36, 96);
            CheckBox3.Name = "CheckBox3";
            CheckBox3.Size = new Size(549, 24);
            CheckBox3.TabIndex = 6;
            CheckBox3.Text = "UI effects";
            // 
            // PictureBox3
            // 
            PictureBox3.BackColor = Color.Transparent;
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(6, 66);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox3.TabIndex = 5;
            PictureBox3.TabStop = false;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(PictureBox14);
            TabPage2.Controls.Add(PictureBox5);
            TabPage2.Controls.Add(CheckBox4);
            TabPage2.Controls.Add(CheckBox10);
            TabPage2.Location = new Point(174, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(591, 384);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Icons";
            // 
            // PictureBox14
            // 
            PictureBox14.BackColor = Color.Transparent;
            PictureBox14.Image = (Image)resources.GetObject("PictureBox14.Image");
            PictureBox14.Location = new Point(6, 36);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox14.TabIndex = 9;
            PictureBox14.TabStop = false;
            // 
            // PictureBox5
            // 
            PictureBox5.BackColor = Color.Transparent;
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(6, 6);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox5.TabIndex = 7;
            PictureBox5.TabStop = false;
            // 
            // CheckBox4
            // 
            CheckBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox4.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox4.Checked = false;
            CheckBox4.Font = new Font("Segoe UI", 9.0f);
            CheckBox4.ForeColor = Color.White;
            CheckBox4.Location = new Point(36, 6);
            CheckBox4.Name = "CheckBox4";
            CheckBox4.Size = new Size(549, 24);
            CheckBox4.TabIndex = 6;
            CheckBox4.Text = "Drop shadow behind desktop icons label";
            // 
            // CheckBox10
            // 
            CheckBox10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox10.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox10.Checked = false;
            CheckBox10.Font = new Font("Segoe UI", 9.0f);
            CheckBox10.ForeColor = Color.White;
            CheckBox10.Location = new Point(36, 36);
            CheckBox10.Name = "CheckBox10";
            CheckBox10.Size = new Size(549, 24);
            CheckBox10.TabIndex = 8;
            CheckBox10.Text = "Translucent selection rectangle";
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(CheckBox12);
            TabPage3.Controls.Add(PictureBox1);
            TabPage3.Controls.Add(MD);
            TabPage3.Controls.Add(PictureBox7);
            TabPage3.Controls.Add(CheckBox6);
            TabPage3.Controls.Add(Label6);
            TabPage3.Controls.Add(CheckBox5);
            TabPage3.Controls.Add(PictureBox16);
            TabPage3.Controls.Add(PictureBox6);
            TabPage3.Controls.Add(ComboBox1);
            TabPage3.Controls.Add(Trackbar1);
            TabPage3.Location = new Point(174, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(591, 384);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Menus";
            // 
            // CheckBox12
            // 
            CheckBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox12.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox12.Checked = false;
            CheckBox12.Font = new Font("Segoe UI", 9.0f);
            CheckBox12.ForeColor = Color.White;
            CheckBox12.Location = new Point(36, 126);
            CheckBox12.Name = "CheckBox12";
            CheckBox12.Size = new Size(549, 24);
            CheckBox12.TabIndex = 132;
            CheckBox12.Text = "Underline an item in menus as a shortcut for keyboard when I press ALT";
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 126);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 133;
            PictureBox1.TabStop = false;
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
            MD.Location = new Point(521, 66);
            MD.Name = "MD";
            MD.Size = new Size(64, 24);
            MD.TabIndex = 131;
            MD.UseVisualStyleBackColor = false;
            // 
            // PictureBox7
            // 
            PictureBox7.BackColor = Color.Transparent;
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(6, 6);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox7.TabIndex = 3;
            PictureBox7.TabStop = false;
            // 
            // CheckBox6
            // 
            CheckBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox6.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox6.Checked = false;
            CheckBox6.Font = new Font("Segoe UI", 9.0f);
            CheckBox6.ForeColor = Color.White;
            CheckBox6.Location = new Point(36, 6);
            CheckBox6.Name = "CheckBox6";
            CheckBox6.Size = new Size(549, 24);
            CheckBox6.TabIndex = 2;
            CheckBox6.Text = "Animation enabled";
            // 
            // Label6
            // 
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.Location = new Point(36, 66);
            Label6.Name = "Label6";
            Label6.Size = new Size(172, 24);
            Label6.TabIndex = 112;
            Label6.Text = "Menu show delay (seconds):";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox5
            // 
            CheckBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox5.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox5.Checked = false;
            CheckBox5.Font = new Font("Segoe UI", 9.0f);
            CheckBox5.ForeColor = Color.White;
            CheckBox5.Location = new Point(36, 96);
            CheckBox5.Name = "CheckBox5";
            CheckBox5.Size = new Size(549, 24);
            CheckBox5.TabIndex = 4;
            CheckBox5.Text = "Fade the selected item after choosing a menu item";
            // 
            // PictureBox16
            // 
            PictureBox16.BackColor = Color.Transparent;
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(6, 66);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(24, 24);
            PictureBox16.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox16.TabIndex = 10;
            PictureBox16.TabStop = false;
            // 
            // PictureBox6
            // 
            PictureBox6.BackColor = Color.Transparent;
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(6, 96);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox6.TabIndex = 5;
            PictureBox6.TabStop = false;
            // 
            // ComboBox1
            // 
            ComboBox1.BackColor = Color.FromArgb(43, 43, 43);
            ComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Items.AddRange(new object[] { "Fade", "Slide" });
            ComboBox1.Location = new Point(36, 36);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(187, 26);
            ComboBox1.TabIndex = 6;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar1.LargeChange = 10;
            Trackbar1.Location = new Point(214, 69);
            Trackbar1.Maximum = 5000;
            Trackbar1.Minimum = 1;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(301, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 113;
            Trackbar1.Value = 1;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(PictureBox9);
            TabPage4.Controls.Add(PictureBox10);
            TabPage4.Controls.Add(CheckBox7);
            TabPage4.Controls.Add(CheckBox8);
            TabPage4.Location = new Point(174, 4);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(591, 384);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "Selection controls";
            // 
            // PictureBox9
            // 
            PictureBox9.BackColor = Color.Transparent;
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(6, 36);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox9.TabIndex = 5;
            PictureBox9.TabStop = false;
            // 
            // PictureBox10
            // 
            PictureBox10.BackColor = Color.Transparent;
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(6, 6);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox10.TabIndex = 3;
            PictureBox10.TabStop = false;
            // 
            // CheckBox7
            // 
            CheckBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox7.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox7.Checked = false;
            CheckBox7.Font = new Font("Segoe UI", 9.0f);
            CheckBox7.ForeColor = Color.White;
            CheckBox7.Location = new Point(36, 36);
            CheckBox7.Name = "CheckBox7";
            CheckBox7.Size = new Size(549, 24);
            CheckBox7.TabIndex = 4;
            CheckBox7.Text = "Listbox smooth scrolling";
            // 
            // CheckBox8
            // 
            CheckBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox8.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox8.Checked = false;
            CheckBox8.Font = new Font("Segoe UI", 9.0f);
            CheckBox8.ForeColor = Color.White;
            CheckBox8.Location = new Point(36, 6);
            CheckBox8.Name = "CheckBox8";
            CheckBox8.Size = new Size(549, 24);
            CheckBox8.TabIndex = 2;
            CheckBox8.Text = "Combobox animation";
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(PictureBox23);
            TabPage5.Controls.Add(CheckBox17);
            TabPage5.Controls.Add(Button4);
            TabPage5.Controls.Add(Label4);
            TabPage5.Controls.Add(PictureBox17);
            TabPage5.Controls.Add(Trackbar5);
            TabPage5.Controls.Add(ComboBox2);
            TabPage5.Controls.Add(PictureBox12);
            TabPage5.Controls.Add(CheckBox9);
            TabPage5.Location = new Point(174, 4);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(3);
            TabPage5.Size = new Size(591, 384);
            TabPage5.TabIndex = 4;
            TabPage5.Text = "Tooltips & notifications";
            // 
            // PictureBox23
            // 
            PictureBox23.BackColor = Color.Transparent;
            PictureBox23.Image = (Image)resources.GetObject("PictureBox23.Image");
            PictureBox23.Location = new Point(6, 96);
            PictureBox23.Name = "PictureBox23";
            PictureBox23.Size = new Size(24, 24);
            PictureBox23.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox23.TabIndex = 218;
            PictureBox23.TabStop = false;
            // 
            // CheckBox17
            // 
            CheckBox17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox17.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox17.Checked = false;
            CheckBox17.Font = new Font("Segoe UI", 9.0f);
            CheckBox17.ForeColor = Color.White;
            CheckBox17.Location = new Point(36, 96);
            CheckBox17.Name = "CheckBox17";
            CheckBox17.Size = new Size(549, 24);
            CheckBox17.TabIndex = 217;
            CheckBox17.Text = "Balloon notifications for Windows 10 (If Windows 11, you should use ExplorerPatch" + "er)";
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(34, 34, 34);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = null;
            Button4.LineColor = Color.FromArgb(0, 81, 210);
            Button4.Location = new Point(521, 66);
            Button4.Name = "Button4";
            Button4.Size = new Size(64, 24);
            Button4.TabIndex = 143;
            Button4.UseVisualStyleBackColor = false;
            // 
            // Label4
            // 
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(36, 66);
            Label4.Name = "Label4";
            Label4.Size = new Size(159, 24);
            Label4.TabIndex = 141;
            Label4.Text = "Notification duration (sec):";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox17
            // 
            PictureBox17.BackColor = Color.Transparent;
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(6, 66);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(24, 24);
            PictureBox17.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox17.TabIndex = 140;
            PictureBox17.TabStop = false;
            // 
            // Trackbar5
            // 
            Trackbar5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar5.LargeChange = 10;
            Trackbar5.Location = new Point(201, 69);
            Trackbar5.Maximum = 100;
            Trackbar5.Minimum = 1;
            Trackbar5.Name = "Trackbar5";
            Trackbar5.Size = new Size(314, 19);
            Trackbar5.SmallChange = 1;
            Trackbar5.TabIndex = 142;
            Trackbar5.Value = 1;
            // 
            // ComboBox2
            // 
            ComboBox2.BackColor = Color.FromArgb(43, 43, 43);
            ComboBox2.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox2.Font = new Font("Segoe UI", 9.0f);
            ComboBox2.ForeColor = Color.White;
            ComboBox2.FormattingEnabled = true;
            ComboBox2.ItemHeight = 20;
            ComboBox2.Items.AddRange(new object[] { "Fade", "Slide" });
            ComboBox2.Location = new Point(36, 36);
            ComboBox2.Name = "ComboBox2";
            ComboBox2.Size = new Size(187, 26);
            ComboBox2.TabIndex = 6;
            // 
            // PictureBox12
            // 
            PictureBox12.BackColor = Color.Transparent;
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(6, 6);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox12.TabIndex = 3;
            PictureBox12.TabStop = false;
            // 
            // CheckBox9
            // 
            CheckBox9.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox9.Checked = false;
            CheckBox9.Font = new Font("Segoe UI", 9.0f);
            CheckBox9.ForeColor = Color.White;
            CheckBox9.Location = new Point(36, 6);
            CheckBox9.Name = "CheckBox9";
            CheckBox9.Size = new Size(549, 24);
            CheckBox9.TabIndex = 2;
            CheckBox9.Text = "Tooltip animation enabled";
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(ButtonR1);
            TabPage6.Controls.Add(Button2);
            TabPage6.Controls.Add(Label2);
            TabPage6.Controls.Add(PictureBox11);
            TabPage6.Controls.Add(Trackbar3);
            TabPage6.Controls.Add(Button1);
            TabPage6.Controls.Add(Label1);
            TabPage6.Controls.Add(PictureBox8);
            TabPage6.Controls.Add(Trackbar2);
            TabPage6.Location = new Point(174, 4);
            TabPage6.Name = "TabPage6";
            TabPage6.Padding = new Padding(3);
            TabPage6.Size = new Size(591, 384);
            TabPage6.TabIndex = 5;
            TabPage6.Text = "Focus rectangle";
            // 
            // ButtonR1
            // 
            ButtonR1.AppearsAsPressed = false;
            ButtonR1.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR1.ButtonDkShadow = Color.Black;
            ButtonR1.ButtonHilight = Color.White;
            ButtonR1.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR1.FocusRectHeight = 1;
            ButtonR1.FocusRectWidth = 1;
            ButtonR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            ButtonR1.ForeColor = Color.Black;
            ButtonR1.HatchBrush = false;
            ButtonR1.Image = null;
            ButtonR1.Location = new Point(225, 79);
            ButtonR1.Name = "ButtonR1";
            ButtonR1.Size = new Size(140, 45);
            ButtonR1.TabIndex = 140;
            ButtonR1.Text = "Click to test";
            ButtonR1.UseItAsScrollbar = false;
            ButtonR1.UseVisualStyleBackColor = false;
            ButtonR1.WindowFrame = Color.Black;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(0, 81, 210);
            Button2.Location = new Point(550, 36);
            Button2.Name = "Button2";
            Button2.Size = new Size(34, 24);
            Button2.TabIndex = 139;
            Button2.UseVisualStyleBackColor = false;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(36, 36);
            Label2.Name = "Label2";
            Label2.Size = new Size(56, 24);
            Label2.TabIndex = 137;
            Label2.Text = "Height:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            PictureBox11.BackColor = Color.Transparent;
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(6, 36);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox11.TabIndex = 136;
            PictureBox11.TabStop = false;
            // 
            // Trackbar3
            // 
            Trackbar3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar3.LargeChange = 10;
            Trackbar3.Location = new Point(98, 39);
            Trackbar3.Maximum = 4;
            Trackbar3.Minimum = 1;
            Trackbar3.Name = "Trackbar3";
            Trackbar3.Size = new Size(446, 19);
            Trackbar3.SmallChange = 1;
            Trackbar3.TabIndex = 138;
            Trackbar3.Value = 1;
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.LineColor = Color.FromArgb(0, 81, 210);
            Button1.Location = new Point(550, 6);
            Button1.Name = "Button1";
            Button1.Size = new Size(34, 24);
            Button1.TabIndex = 135;
            Button1.UseVisualStyleBackColor = false;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(36, 6);
            Label1.Name = "Label1";
            Label1.Size = new Size(56, 24);
            Label1.TabIndex = 133;
            Label1.Text = "Width:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox8
            // 
            PictureBox8.BackColor = Color.Transparent;
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(6, 6);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox8.TabIndex = 132;
            PictureBox8.TabStop = false;
            // 
            // Trackbar2
            // 
            Trackbar2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar2.LargeChange = 10;
            Trackbar2.Location = new Point(98, 9);
            Trackbar2.Maximum = 4;
            Trackbar2.Minimum = 1;
            Trackbar2.Name = "Trackbar2";
            Trackbar2.Size = new Size(446, 19);
            Trackbar2.SmallChange = 1;
            Trackbar2.TabIndex = 134;
            Trackbar2.Value = 1;
            // 
            // TabPage7
            // 
            TabPage7.BackColor = Color.FromArgb(25, 25, 25);
            TabPage7.Controls.Add(Panel1);
            TabPage7.Controls.Add(Button3);
            TabPage7.Controls.Add(Label3);
            TabPage7.Controls.Add(PictureBox13);
            TabPage7.Controls.Add(Trackbar4);
            TabPage7.Location = new Point(174, 4);
            TabPage7.Name = "TabPage7";
            TabPage7.Padding = new Padding(3);
            TabPage7.Size = new Size(591, 384);
            TabPage7.TabIndex = 6;
            TabPage7.Text = "Text cursor (caret)";
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.FromArgb(255, 255, 254);
            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(Panel2);
            Panel1.Controls.Add(Label7);
            Panel1.Location = new Point(94, 45);
            Panel1.Name = "Panel1";
            Panel1.Padding = new Padding(1, 3, 0, 3);
            Panel1.Size = new Size(402, 25);
            Panel1.TabIndex = 140;
            // 
            // Panel2
            // 
            Panel2.BackColor = Color.FromArgb(0, 0, 1);
            Panel2.Dock = DockStyle.Left;
            Panel2.Location = new Point(235, 3);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(5, 15);
            Panel2.TabIndex = 141;
            // 
            // Label7
            // 
            Label7.BackColor = Color.Transparent;
            Label7.Dock = DockStyle.Left;
            Label7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.ForeColor = Color.FromArgb(0, 0, 1);
            Label7.Location = new Point(1, 3);
            Label7.Name = "Label7";
            Label7.Size = new Size(234, 15);
            Label7.TabIndex = 0;
            Label7.Text = "This is a text inside a textbox followed by a caret";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(0, 81, 210);
            Button3.Location = new Point(550, 6);
            Button3.Name = "Button3";
            Button3.Size = new Size(34, 24);
            Button3.TabIndex = 139;
            Button3.UseVisualStyleBackColor = false;
            // 
            // Label3
            // 
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(36, 6);
            Label3.Name = "Label3";
            Label3.Size = new Size(56, 24);
            Label3.TabIndex = 137;
            Label3.Text = "Width:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox13
            // 
            PictureBox13.BackColor = Color.Transparent;
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(6, 6);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(24, 24);
            PictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox13.TabIndex = 136;
            PictureBox13.TabStop = false;
            // 
            // Trackbar4
            // 
            Trackbar4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar4.LargeChange = 10;
            Trackbar4.Location = new Point(98, 9);
            Trackbar4.Maximum = 40;
            Trackbar4.Minimum = 1;
            Trackbar4.Name = "Trackbar4";
            Trackbar4.Size = new Size(446, 19);
            Trackbar4.SmallChange = 1;
            Trackbar4.TabIndex = 138;
            Trackbar4.Value = 1;
            // 
            // TabPage8
            // 
            TabPage8.BackColor = Color.FromArgb(25, 25, 25);
            TabPage8.Controls.Add(Button5);
            TabPage8.Controls.Add(Label5);
            TabPage8.Controls.Add(PictureBox20);
            TabPage8.Controls.Add(Trackbar6);
            TabPage8.Controls.Add(PictureBox19);
            TabPage8.Controls.Add(CheckBox14);
            TabPage8.Controls.Add(PictureBox18);
            TabPage8.Controls.Add(CheckBox13);
            TabPage8.Location = new Point(174, 4);
            TabPage8.Name = "TabPage8";
            TabPage8.Padding = new Padding(3);
            TabPage8.Size = new Size(591, 384);
            TabPage8.TabIndex = 7;
            TabPage8.Text = "Active Windows tracking";
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(34, 34, 34);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = null;
            Button5.LineColor = Color.FromArgb(0, 81, 210);
            Button5.Location = new Point(521, 66);
            Button5.Name = "Button5";
            Button5.Size = new Size(64, 24);
            Button5.TabIndex = 143;
            Button5.UseVisualStyleBackColor = false;
            // 
            // Label5
            // 
            Label5.BackColor = Color.Transparent;
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5.Location = new Point(36, 66);
            Label5.Name = "Label5";
            Label5.Size = new Size(203, 24);
            Label5.TabIndex = 141;
            Label5.Text = "Delay (before changing focus) (ms):";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox20
            // 
            PictureBox20.BackColor = Color.Transparent;
            PictureBox20.Image = (Image)resources.GetObject("PictureBox20.Image");
            PictureBox20.Location = new Point(6, 66);
            PictureBox20.Name = "PictureBox20";
            PictureBox20.Size = new Size(24, 24);
            PictureBox20.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox20.TabIndex = 140;
            PictureBox20.TabStop = false;
            // 
            // Trackbar6
            // 
            Trackbar6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar6.LargeChange = 10;
            Trackbar6.Location = new Point(245, 69);
            Trackbar6.Maximum = 5000;
            Trackbar6.Minimum = 1;
            Trackbar6.Name = "Trackbar6";
            Trackbar6.Size = new Size(270, 19);
            Trackbar6.SmallChange = 1;
            Trackbar6.TabIndex = 142;
            Trackbar6.Value = 1;
            // 
            // PictureBox19
            // 
            PictureBox19.BackColor = Color.Transparent;
            PictureBox19.Image = (Image)resources.GetObject("PictureBox19.Image");
            PictureBox19.Location = new Point(6, 36);
            PictureBox19.Name = "PictureBox19";
            PictureBox19.Size = new Size(24, 24);
            PictureBox19.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox19.TabIndex = 7;
            PictureBox19.TabStop = false;
            // 
            // CheckBox14
            // 
            CheckBox14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox14.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox14.Checked = false;
            CheckBox14.Font = new Font("Segoe UI", 9.0f);
            CheckBox14.ForeColor = Color.White;
            CheckBox14.Location = new Point(36, 36);
            CheckBox14.Name = "CheckBox14";
            CheckBox14.Size = new Size(549, 24);
            CheckBox14.TabIndex = 6;
            CheckBox14.Text = "Bring activated window to top";
            // 
            // PictureBox18
            // 
            PictureBox18.BackColor = Color.Transparent;
            PictureBox18.Image = (Image)resources.GetObject("PictureBox18.Image");
            PictureBox18.Location = new Point(6, 6);
            PictureBox18.Name = "PictureBox18";
            PictureBox18.Size = new Size(24, 24);
            PictureBox18.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox18.TabIndex = 5;
            PictureBox18.TabStop = false;
            // 
            // CheckBox13
            // 
            CheckBox13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox13.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox13.Checked = false;
            CheckBox13.Font = new Font("Segoe UI", 9.0f);
            CheckBox13.ForeColor = Color.White;
            CheckBox13.Location = new Point(36, 6);
            CheckBox13.Name = "CheckBox13";
            CheckBox13.Size = new Size(549, 24);
            CheckBox13.TabIndex = 4;
            CheckBox13.Text = "Enabled (focus follows the cursor)";
            // 
            // TabPage11
            // 
            TabPage11.BackColor = Color.FromArgb(25, 25, 25);
            TabPage11.Controls.Add(AlertBox3);
            TabPage11.Controls.Add(AlertBox2);
            TabPage11.Controls.Add(AlertBox1);
            TabPage11.Controls.Add(RadioButton3);
            TabPage11.Controls.Add(PictureBox30);
            TabPage11.Controls.Add(PictureBox26);
            TabPage11.Controls.Add(RadioButton2);
            TabPage11.Controls.Add(CheckBox20);
            TabPage11.Controls.Add(CheckBox23);
            TabPage11.Controls.Add(RadioButton1);
            TabPage11.Controls.Add(PictureBox29);
            TabPage11.Controls.Add(Label8);
            TabPage11.Location = new Point(174, 4);
            TabPage11.Name = "TabPage11";
            TabPage11.Padding = new Padding(3);
            TabPage11.Size = new Size(591, 384);
            TabPage11.TabIndex = 10;
            TabPage11.Text = "Explorer (controls options)";
            // 
            // AlertBox3
            // 
            AlertBox3.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox3.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox3.CenterText = false;
            AlertBox3.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox3.Font = new Font("Segoe UI", 9.0f);
            AlertBox3.Image = null;
            AlertBox3.Location = new Point(73, 258);
            AlertBox3.Name = "AlertBox3";
            AlertBox3.Size = new Size(512, 24);
            AlertBox3.TabIndex = 238;
            AlertBox3.TabStop = false;
            AlertBox3.Text = @"This will disable navigation bar in Open\Save dialogs, anywhere else requires Exp" + "lorerPatcher";
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
            AlertBox2.Location = new Point(73, 181);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(512, 40);
            AlertBox2.TabIndex = 237;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "Due to limitations in Windows 11, styles can't be changed by registry. So you sho" + "uld use ExplorerPatcher or StartAllBack to force apply bar style";
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(73, 154);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(512, 24);
            AlertBox1.TabIndex = 236;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "Default* is Command bar in Windows 11, Ribbon in Windows 10 and 8.1";
            // 
            // RadioButton3
            // 
            RadioButton3.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton3.Checked = false;
            RadioButton3.Font = new Font("Segoe UI", 9.0f);
            RadioButton3.ForeColor = Color.White;
            RadioButton3.Location = new Point(48, 123);
            RadioButton3.Name = "RadioButton3";
            RadioButton3.Size = new Size(537, 24);
            RadioButton3.TabIndex = 233;
            RadioButton3.Text = "Windows 7 Explorer bar style";
            // 
            // PictureBox30
            // 
            PictureBox30.BackColor = Color.Transparent;
            PictureBox30.Image = (Image)resources.GetObject("PictureBox30.Image");
            PictureBox30.Location = new Point(6, 228);
            PictureBox30.Name = "PictureBox30";
            PictureBox30.Size = new Size(24, 24);
            PictureBox30.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox30.TabIndex = 228;
            PictureBox30.TabStop = false;
            // 
            // PictureBox26
            // 
            PictureBox26.BackColor = Color.Transparent;
            PictureBox26.Image = (Image)resources.GetObject("PictureBox26.Image");
            PictureBox26.Location = new Point(6, 6);
            PictureBox26.Name = "PictureBox26";
            PictureBox26.Size = new Size(24, 24);
            PictureBox26.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox26.TabIndex = 222;
            PictureBox26.TabStop = false;
            // 
            // RadioButton2
            // 
            RadioButton2.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Location = new Point(48, 93);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(537, 24);
            RadioButton2.TabIndex = 232;
            RadioButton2.Text = "Ribbon";
            // 
            // CheckBox20
            // 
            CheckBox20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox20.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox20.Checked = false;
            CheckBox20.Font = new Font("Segoe UI", 9.0f);
            CheckBox20.ForeColor = Color.White;
            CheckBox20.Location = new Point(36, 6);
            CheckBox20.Name = "CheckBox20";
            CheckBox20.Size = new Size(549, 24);
            CheckBox20.TabIndex = 221;
            CheckBox20.Text = "SysListView32 view in Explorer (For Windows 7 and later)";
            // 
            // CheckBox23
            // 
            CheckBox23.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox23.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox23.Checked = false;
            CheckBox23.Font = new Font("Segoe UI", 9.0f);
            CheckBox23.ForeColor = Color.White;
            CheckBox23.Location = new Point(36, 228);
            CheckBox23.Name = "CheckBox23";
            CheckBox23.Size = new Size(549, 24);
            CheckBox23.TabIndex = 227;
            CheckBox23.Text = "Disable navigation bar";
            // 
            // RadioButton1
            // 
            RadioButton1.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton1.Checked = false;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Location = new Point(48, 63);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(537, 24);
            RadioButton1.TabIndex = 231;
            RadioButton1.Text = "Default*";
            // 
            // PictureBox29
            // 
            PictureBox29.BackColor = Color.Transparent;
            PictureBox29.Image = (Image)resources.GetObject("PictureBox29.Image");
            PictureBox29.Location = new Point(6, 36);
            PictureBox29.Name = "PictureBox29";
            PictureBox29.Size = new Size(24, 24);
            PictureBox29.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox29.TabIndex = 229;
            PictureBox29.TabStop = false;
            // 
            // Label8
            // 
            Label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label8.BackColor = Color.Transparent;
            Label8.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8.Location = new Point(36, 36);
            Label8.Name = "Label8";
            Label8.Size = new Size(549, 24);
            Label8.TabIndex = 230;
            Label8.Text = "Explorer bar:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage10
            // 
            TabPage10.BackColor = Color.FromArgb(25, 25, 25);
            TabPage10.Controls.Add(PictureBox28);
            TabPage10.Controls.Add(CheckBox22);
            TabPage10.Controls.Add(PictureBox22);
            TabPage10.Controls.Add(CheckBox16);
            TabPage10.Location = new Point(174, 4);
            TabPage10.Name = "TabPage10";
            TabPage10.Padding = new Padding(3);
            TabPage10.Size = new Size(591, 384);
            TabPage10.TabIndex = 9;
            TabPage10.Text = "For Windows 11 only";
            // 
            // PictureBox28
            // 
            PictureBox28.BackColor = Color.Transparent;
            PictureBox28.Image = (Image)resources.GetObject("PictureBox28.Image");
            PictureBox28.Location = new Point(6, 36);
            PictureBox28.Name = "PictureBox28";
            PictureBox28.Size = new Size(24, 24);
            PictureBox28.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox28.TabIndex = 228;
            PictureBox28.TabStop = false;
            // 
            // CheckBox22
            // 
            CheckBox22.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox22.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox22.Checked = false;
            CheckBox22.Font = new Font("Segoe UI", 9.0f);
            CheckBox22.ForeColor = Color.White;
            CheckBox22.Location = new Point(36, 36);
            CheckBox22.Name = "CheckBox22";
            CheckBox22.Size = new Size(549, 24);
            CheckBox22.TabIndex = 227;
            CheckBox22.Text = "Spinning dots in boot screen";
            // 
            // PictureBox22
            // 
            PictureBox22.BackColor = Color.Transparent;
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(6, 6);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(24, 24);
            PictureBox22.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox22.TabIndex = 214;
            PictureBox22.TabStop = false;
            // 
            // CheckBox16
            // 
            CheckBox16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox16.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox16.Checked = false;
            CheckBox16.Font = new Font("Segoe UI", 9.0f);
            CheckBox16.ForeColor = Color.White;
            CheckBox16.Location = new Point(36, 6);
            CheckBox16.Name = "CheckBox16";
            CheckBox16.Size = new Size(549, 24);
            CheckBox16.TabIndex = 213;
            CheckBox16.Text = "Classic context menu (may require explorer restart)";
            // 
            // TabPage12
            // 
            TabPage12.BackColor = Color.FromArgb(25, 25, 25);
            TabPage12.Controls.Add(AlertBox4);
            TabPage12.Controls.Add(RadioImage7);
            TabPage12.Controls.Add(RadioImage6);
            TabPage12.Controls.Add(RadioImage5);
            TabPage12.Controls.Add(RadioImage4);
            TabPage12.Controls.Add(RadioImage3);
            TabPage12.Controls.Add(RadioImage2);
            TabPage12.Controls.Add(RadioImage1);
            TabPage12.Controls.Add(Panel3);
            TabPage12.Controls.Add(PictureBox33);
            TabPage12.Controls.Add(PictureBox32);
            TabPage12.Location = new Point(174, 4);
            TabPage12.Name = "TabPage12";
            TabPage12.Padding = new Padding(3);
            TabPage12.Size = new Size(591, 384);
            TabPage12.TabIndex = 11;
            TabPage12.Text = "Color filters (accessibility)";
            // 
            // AlertBox4
            // 
            AlertBox4.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox4.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox4.CenterText = false;
            AlertBox4.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox4.Font = new Font("Segoe UI", 9.0f);
            AlertBox4.Image = null;
            AlertBox4.Location = new Point(6, 337);
            AlertBox4.Name = "AlertBox4";
            AlertBox4.Size = new Size(579, 40);
            AlertBox4.TabIndex = 239;
            AlertBox4.TabStop = false;
            AlertBox4.Text = "This is for Windows 10 & 11 and requires logoff. The colors of preview may be inc" + "orrect if you applied a color filter (The correct ones are previewed when normal" + " filter is applied). ";
            // 
            // RadioImage7
            // 
            RadioImage7.Checked = false;
            RadioImage7.Font = new Font("Segoe UI", 9.0f);
            RadioImage7.ForeColor = Color.White;
            RadioImage7.Image = null;
            RadioImage7.Location = new Point(309, 7);
            RadioImage7.Name = "RadioImage7";
            RadioImage7.ShowText = true;
            RadioImage7.Size = new Size(123, 25);
            RadioImage7.TabIndex = 6;
            RadioImage7.Text = "Inverted";
            RadioImage7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage6
            // 
            RadioImage6.Checked = false;
            RadioImage6.Font = new Font("Segoe UI", 9.0f);
            RadioImage6.ForeColor = Color.White;
            RadioImage6.Image = null;
            RadioImage6.Location = new Point(309, 38);
            RadioImage6.Name = "RadioImage6";
            RadioImage6.ShowText = true;
            RadioImage6.Size = new Size(123, 25);
            RadioImage6.TabIndex = 5;
            RadioImage6.Text = "Grayscale inverted";
            RadioImage6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage5
            // 
            RadioImage5.Checked = false;
            RadioImage5.Font = new Font("Segoe UI", 9.0f);
            RadioImage5.ForeColor = Color.White;
            RadioImage5.Image = null;
            RadioImage5.Location = new Point(180, 38);
            RadioImage5.Name = "RadioImage5";
            RadioImage5.ShowText = true;
            RadioImage5.Size = new Size(123, 25);
            RadioImage5.TabIndex = 4;
            RadioImage5.Text = "Grayscale";
            RadioImage5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage4
            // 
            RadioImage4.Checked = false;
            RadioImage4.Font = new Font("Segoe UI", 9.0f);
            RadioImage4.ForeColor = Color.White;
            RadioImage4.Image = null;
            RadioImage4.Location = new Point(180, 131);
            RadioImage4.Name = "RadioImage4";
            RadioImage4.ShowText = true;
            RadioImage4.Size = new Size(252, 25);
            RadioImage4.TabIndex = 3;
            RadioImage4.Text = "Blue-yellow (tritanopia)";
            RadioImage4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage3
            // 
            RadioImage3.Checked = false;
            RadioImage3.Font = new Font("Segoe UI", 9.0f);
            RadioImage3.ForeColor = Color.White;
            RadioImage3.Image = null;
            RadioImage3.Location = new Point(180, 100);
            RadioImage3.Name = "RadioImage3";
            RadioImage3.ShowText = true;
            RadioImage3.Size = new Size(252, 25);
            RadioImage3.TabIndex = 2;
            RadioImage3.Text = "Red-green (red weak, protanopia)";
            RadioImage3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage2
            // 
            RadioImage2.Checked = false;
            RadioImage2.Font = new Font("Segoe UI", 9.0f);
            RadioImage2.ForeColor = Color.White;
            RadioImage2.Image = null;
            RadioImage2.Location = new Point(180, 69);
            RadioImage2.Name = "RadioImage2";
            RadioImage2.ShowText = true;
            RadioImage2.Size = new Size(252, 25);
            RadioImage2.TabIndex = 1;
            RadioImage2.Text = "Red-green (green weak, deuteranopia)";
            RadioImage2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage1
            // 
            RadioImage1.Checked = false;
            RadioImage1.Font = new Font("Segoe UI", 9.0f);
            RadioImage1.ForeColor = Color.White;
            RadioImage1.Image = null;
            RadioImage1.Location = new Point(180, 7);
            RadioImage1.Name = "RadioImage1";
            RadioImage1.ShowText = true;
            RadioImage1.Size = new Size(123, 25);
            RadioImage1.TabIndex = 0;
            RadioImage1.Text = "Normal";
            RadioImage1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Panel3
            // 
            Panel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Panel3.BackColor = Color.Transparent;
            Panel3.Controls.Add(Panel24);
            Panel3.Controls.Add(Panel20);
            Panel3.Controls.Add(Panel16);
            Panel3.Controls.Add(Panel12);
            Panel3.Controls.Add(Panel8);
            Panel3.Controls.Add(Panel4);
            Panel3.Location = new Point(438, 162);
            Panel3.Name = "Panel3";
            Panel3.Size = new Size(147, 168);
            Panel3.TabIndex = 86;
            // 
            // Panel24
            // 
            Panel24.BackColor = Color.Transparent;
            Panel24.Controls.Add(Label15);
            Panel24.Controls.Add(P3);
            Panel24.Controls.Add(P2);
            Panel24.Controls.Add(P1);
            Panel24.Dock = DockStyle.Top;
            Panel24.Location = new Point(0, 140);
            Panel24.Name = "Panel24";
            Panel24.Size = new Size(147, 28);
            Panel24.TabIndex = 92;
            // 
            // Label15
            // 
            Label15.BackColor = Color.Transparent;
            Label15.Dock = DockStyle.Fill;
            Label15.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label15.Location = new Point(84, 0);
            Label15.Name = "Label15";
            Label15.Padding = new Padding(5, 0, 0, 0);
            Label15.Size = new Size(63, 28);
            Label15.TabIndex = 112;
            Label15.Text = "Purple";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // P3
            // 
            P3.BackColor = Color.FromArgb(217, 195, 233);
            P3.Dock = DockStyle.Left;
            P3.Location = new Point(56, 0);
            P3.Name = "P3";
            P3.Size = new Size(28, 28);
            P3.TabIndex = 89;
            // 
            // P2
            // 
            P2.BackColor = Color.FromArgb(195, 156, 219);
            P2.Dock = DockStyle.Left;
            P2.Location = new Point(28, 0);
            P2.Name = "P2";
            P2.Size = new Size(28, 28);
            P2.TabIndex = 88;
            // 
            // P1
            // 
            P1.BackColor = Color.FromArgb(165, 39, 200);
            P1.Dock = DockStyle.Left;
            P1.Location = new Point(0, 0);
            P1.Name = "P1";
            P1.Size = new Size(28, 28);
            P1.TabIndex = 87;
            // 
            // Panel20
            // 
            Panel20.BackColor = Color.Transparent;
            Panel20.Controls.Add(Label14);
            Panel20.Controls.Add(B3);
            Panel20.Controls.Add(B2);
            Panel20.Controls.Add(B1);
            Panel20.Dock = DockStyle.Top;
            Panel20.Location = new Point(0, 112);
            Panel20.Name = "Panel20";
            Panel20.Size = new Size(147, 28);
            Panel20.TabIndex = 91;
            // 
            // Label14
            // 
            Label14.BackColor = Color.Transparent;
            Label14.Dock = DockStyle.Fill;
            Label14.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label14.Location = new Point(84, 0);
            Label14.Name = "Label14";
            Label14.Padding = new Padding(5, 0, 0, 0);
            Label14.Size = new Size(63, 28);
            Label14.TabIndex = 112;
            Label14.Text = "Blue";
            Label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // B3
            // 
            B3.BackColor = Color.FromArgb(118, 170, 248);
            B3.Dock = DockStyle.Left;
            B3.Location = new Point(56, 0);
            B3.Name = "B3";
            B3.Size = new Size(28, 28);
            B3.TabIndex = 89;
            // 
            // B2
            // 
            B2.BackColor = Color.FromArgb(55, 119, 245);
            B2.Dock = DockStyle.Left;
            B2.Location = new Point(28, 0);
            B2.Name = "B2";
            B2.Size = new Size(28, 28);
            B2.TabIndex = 88;
            // 
            // B1
            // 
            B1.BackColor = Color.FromArgb(29, 65, 211);
            B1.Dock = DockStyle.Left;
            B1.Location = new Point(0, 0);
            B1.Name = "B1";
            B1.Size = new Size(28, 28);
            B1.TabIndex = 87;
            // 
            // Panel16
            // 
            Panel16.BackColor = Color.Transparent;
            Panel16.Controls.Add(Label13);
            Panel16.Controls.Add(G3);
            Panel16.Controls.Add(G2);
            Panel16.Controls.Add(G1);
            Panel16.Dock = DockStyle.Top;
            Panel16.Location = new Point(0, 84);
            Panel16.Name = "Panel16";
            Panel16.Size = new Size(147, 28);
            Panel16.TabIndex = 90;
            // 
            // Label13
            // 
            Label13.BackColor = Color.Transparent;
            Label13.Dock = DockStyle.Fill;
            Label13.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label13.Location = new Point(84, 0);
            Label13.Name = "Label13";
            Label13.Padding = new Padding(5, 0, 0, 0);
            Label13.Size = new Size(63, 28);
            Label13.TabIndex = 112;
            Label13.Text = "Green";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // G3
            // 
            G3.BackColor = Color.FromArgb(163, 228, 166);
            G3.Dock = DockStyle.Left;
            G3.Location = new Point(56, 0);
            G3.Name = "G3";
            G3.Size = new Size(28, 28);
            G3.TabIndex = 89;
            // 
            // G2
            // 
            G2.BackColor = Color.FromArgb(117, 213, 113);
            G2.Dock = DockStyle.Left;
            G2.Location = new Point(28, 0);
            G2.Name = "G2";
            G2.Size = new Size(28, 28);
            G2.TabIndex = 88;
            // 
            // G1
            // 
            G1.BackColor = Color.FromArgb(57, 122, 47);
            G1.Dock = DockStyle.Left;
            G1.Location = new Point(0, 0);
            G1.Name = "G1";
            G1.Size = new Size(28, 28);
            G1.TabIndex = 87;
            // 
            // Panel12
            // 
            Panel12.BackColor = Color.Transparent;
            Panel12.Controls.Add(Label11);
            Panel12.Controls.Add(Y3);
            Panel12.Controls.Add(Y2);
            Panel12.Controls.Add(Y1);
            Panel12.Dock = DockStyle.Top;
            Panel12.Location = new Point(0, 56);
            Panel12.Name = "Panel12";
            Panel12.Size = new Size(147, 28);
            Panel12.TabIndex = 89;
            // 
            // Label11
            // 
            Label11.BackColor = Color.Transparent;
            Label11.Dock = DockStyle.Fill;
            Label11.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label11.Location = new Point(84, 0);
            Label11.Name = "Label11";
            Label11.Padding = new Padding(5, 0, 0, 0);
            Label11.Size = new Size(63, 28);
            Label11.TabIndex = 112;
            Label11.Text = "Yellow";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Y3
            // 
            Y3.BackColor = Color.FromArgb(250, 224, 121);
            Y3.Dock = DockStyle.Left;
            Y3.Location = new Point(56, 0);
            Y3.Name = "Y3";
            Y3.Size = new Size(28, 28);
            Y3.TabIndex = 89;
            // 
            // Y2
            // 
            Y2.BackColor = Color.FromArgb(248, 205, 72);
            Y2.Dock = DockStyle.Left;
            Y2.Location = new Point(28, 0);
            Y2.Name = "Y2";
            Y2.Size = new Size(28, 28);
            Y2.TabIndex = 88;
            // 
            // Y1
            // 
            Y1.BackColor = Color.FromArgb(231, 181, 64);
            Y1.Dock = DockStyle.Left;
            Y1.Location = new Point(0, 0);
            Y1.Name = "Y1";
            Y1.Size = new Size(28, 28);
            Y1.TabIndex = 87;
            // 
            // Panel8
            // 
            Panel8.BackColor = Color.Transparent;
            Panel8.Controls.Add(Label10);
            Panel8.Controls.Add(O3);
            Panel8.Controls.Add(O2);
            Panel8.Controls.Add(O1);
            Panel8.Dock = DockStyle.Top;
            Panel8.Location = new Point(0, 28);
            Panel8.Name = "Panel8";
            Panel8.Size = new Size(147, 28);
            Panel8.TabIndex = 88;
            // 
            // Label10
            // 
            Label10.BackColor = Color.Transparent;
            Label10.Dock = DockStyle.Fill;
            Label10.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label10.Location = new Point(84, 0);
            Label10.Name = "Label10";
            Label10.Padding = new Padding(5, 0, 0, 0);
            Label10.Size = new Size(63, 28);
            Label10.TabIndex = 112;
            Label10.Text = "Orange";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // O3
            // 
            O3.BackColor = Color.FromArgb(247, 193, 114);
            O3.Dock = DockStyle.Left;
            O3.Location = new Point(56, 0);
            O3.Name = "O3";
            O3.Size = new Size(28, 28);
            O3.TabIndex = 89;
            // 
            // O2
            // 
            O2.BackColor = Color.FromArgb(239, 153, 58);
            O2.Dock = DockStyle.Left;
            O2.Location = new Point(28, 0);
            O2.Name = "O2";
            O2.Size = new Size(28, 28);
            O2.TabIndex = 88;
            // 
            // O1
            // 
            O1.BackColor = Color.FromArgb(220, 96, 44);
            O1.Dock = DockStyle.Left;
            O1.Location = new Point(0, 0);
            O1.Name = "O1";
            O1.Size = new Size(28, 28);
            O1.TabIndex = 87;
            // 
            // Panel4
            // 
            Panel4.BackColor = Color.Transparent;
            Panel4.Controls.Add(Label9);
            Panel4.Controls.Add(R3);
            Panel4.Controls.Add(R2);
            Panel4.Controls.Add(R1);
            Panel4.Dock = DockStyle.Top;
            Panel4.Location = new Point(0, 0);
            Panel4.Name = "Panel4";
            Panel4.Size = new Size(147, 28);
            Panel4.TabIndex = 87;
            // 
            // Label9
            // 
            Label9.BackColor = Color.Transparent;
            Label9.Dock = DockStyle.Fill;
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label9.Location = new Point(84, 0);
            Label9.Name = "Label9";
            Label9.Padding = new Padding(5, 0, 0, 0);
            Label9.Size = new Size(63, 28);
            Label9.TabIndex = 112;
            Label9.Text = "Red";
            Label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // R3
            // 
            R3.BackColor = Color.FromArgb(239, 142, 133);
            R3.Dock = DockStyle.Left;
            R3.Location = new Point(56, 0);
            R3.Name = "R3";
            R3.Size = new Size(28, 28);
            R3.TabIndex = 89;
            // 
            // R2
            // 
            R2.BackColor = Color.FromArgb(233, 80, 63);
            R2.Dock = DockStyle.Left;
            R2.Location = new Point(28, 0);
            R2.Name = "R2";
            R2.Size = new Size(28, 28);
            R2.TabIndex = 88;
            // 
            // R1
            // 
            R1.BackColor = Color.FromArgb(204, 50, 47);
            R1.Dock = DockStyle.Left;
            R1.Location = new Point(0, 0);
            R1.Name = "R1";
            R1.Size = new Size(28, 28);
            R1.TabIndex = 87;
            // 
            // PictureBox33
            // 
            PictureBox33.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PictureBox33.Image = My.Resources.CF_Img_Normal;
            PictureBox33.Location = new Point(180, 162);
            PictureBox33.Name = "PictureBox33";
            PictureBox33.Size = new Size(252, 168);
            PictureBox33.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox33.TabIndex = 85;
            PictureBox33.TabStop = false;
            // 
            // PictureBox32
            // 
            PictureBox32.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PictureBox32.Image = My.Resources.CF_Pie_Normal;
            PictureBox32.Location = new Point(6, 162);
            PictureBox32.Name = "PictureBox32";
            PictureBox32.Size = new Size(168, 168);
            PictureBox32.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox32.TabIndex = 84;
            PictureBox32.TabStop = false;
            // 
            // TabPage9
            // 
            TabPage9.BackColor = Color.FromArgb(25, 25, 25);
            TabPage9.Controls.Add(PictureBox35);
            TabPage9.Controls.Add(CheckBox26);
            TabPage9.Controls.Add(PictureBox34);
            TabPage9.Controls.Add(CheckBox25);
            TabPage9.Controls.Add(AlertBox6);
            TabPage9.Controls.Add(AlertBox5);
            TabPage9.Controls.Add(PictureBox31);
            TabPage9.Controls.Add(CheckBox24);
            TabPage9.Controls.Add(PictureBox21);
            TabPage9.Controls.Add(CheckBox15);
            TabPage9.Controls.Add(PictureBox24);
            TabPage9.Controls.Add(CheckBox18);
            TabPage9.Controls.Add(PictureBox25);
            TabPage9.Controls.Add(CheckBox19);
            TabPage9.Location = new Point(174, 4);
            TabPage9.Name = "TabPage9";
            TabPage9.Padding = new Padding(3);
            TabPage9.Size = new Size(591, 384);
            TabPage9.TabIndex = 8;
            TabPage9.Text = "Miscellaneous";
            // 
            // PictureBox35
            // 
            PictureBox35.BackColor = Color.Transparent;
            PictureBox35.Image = (Image)resources.GetObject("PictureBox35.Image");
            PictureBox35.Location = new Point(6, 231);
            PictureBox35.Name = "PictureBox35";
            PictureBox35.Size = new Size(24, 24);
            PictureBox35.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox35.TabIndex = 245;
            PictureBox35.TabStop = false;
            // 
            // CheckBox26
            // 
            CheckBox26.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox26.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox26.Checked = false;
            CheckBox26.Font = new Font("Segoe UI", 9.0f);
            CheckBox26.ForeColor = Color.White;
            CheckBox26.Location = new Point(36, 231);
            CheckBox26.Name = "CheckBox26";
            CheckBox26.Size = new Size(549, 24);
            CheckBox26.TabIndex = 244;
            CheckBox26.Text = "Enable Windows 7 taskbar volume mixer for Windows 10 only";
            // 
            // PictureBox34
            // 
            PictureBox34.BackColor = Color.Transparent;
            PictureBox34.Image = (Image)resources.GetObject("PictureBox34.Image");
            PictureBox34.Location = new Point(6, 171);
            PictureBox34.Name = "PictureBox34";
            PictureBox34.Size = new Size(24, 24);
            PictureBox34.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox34.TabIndex = 243;
            PictureBox34.TabStop = false;
            // 
            // CheckBox25
            // 
            CheckBox25.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox25.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox25.Checked = false;
            CheckBox25.Font = new Font("Segoe UI", 9.0f);
            CheckBox25.ForeColor = Color.White;
            CheckBox25.Location = new Point(36, 171);
            CheckBox25.Name = "CheckBox25";
            CheckBox25.Size = new Size(549, 24);
            CheckBox25.TabIndex = 242;
            CheckBox25.Text = "Full screen start menu (for Windows 10 and requires logoff and logon)";
            // 
            // AlertBox6
            // 
            AlertBox6.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox6.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox6.CenterText = false;
            AlertBox6.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox6.Font = new Font("Segoe UI", 9.0f);
            AlertBox6.Image = null;
            AlertBox6.Location = new Point(36, 94);
            AlertBox6.Name = "AlertBox6";
            AlertBox6.Size = new Size(549, 40);
            AlertBox6.TabIndex = 241;
            AlertBox6.TabStop = false;
            AlertBox6.Text = "If you are using Windows 11 lower than Moment 3 update, use ExplorerPatcher to ap" + "ply this effect forcibly";
            // 
            // AlertBox5
            // 
            AlertBox5.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox5.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox5.CenterText = false;
            AlertBox5.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox5.Font = new Font("Segoe UI", 9.0f);
            AlertBox5.Image = null;
            AlertBox5.Location = new Point(36, 66);
            AlertBox5.Name = "AlertBox5";
            AlertBox5.Size = new Size(115, 22);
            AlertBox5.TabIndex = 240;
            AlertBox5.TabStop = false;
            AlertBox5.Text = "Uses more power";
            // 
            // PictureBox31
            // 
            PictureBox31.BackColor = Color.Transparent;
            PictureBox31.Image = (Image)resources.GetObject("PictureBox31.Image");
            PictureBox31.Location = new Point(6, 201);
            PictureBox31.Name = "PictureBox31";
            PictureBox31.Size = new Size(24, 24);
            PictureBox31.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox31.TabIndex = 228;
            PictureBox31.TabStop = false;
            // 
            // CheckBox24
            // 
            CheckBox24.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox24.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox24.Checked = false;
            CheckBox24.Font = new Font("Segoe UI", 9.0f);
            CheckBox24.ForeColor = Color.White;
            CheckBox24.Location = new Point(36, 201);
            CheckBox24.Name = "CheckBox24";
            CheckBox24.Size = new Size(549, 24);
            CheckBox24.TabIndex = 227;
            CheckBox24.Text = "Automatic hide scroll bars in modern Windows apps (Windows 10 and later)";
            // 
            // PictureBox21
            // 
            PictureBox21.BackColor = Color.Transparent;
            PictureBox21.Image = (Image)resources.GetObject("PictureBox21.Image");
            PictureBox21.Location = new Point(6, 6);
            PictureBox21.Name = "PictureBox21";
            PictureBox21.Size = new Size(24, 24);
            PictureBox21.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox21.TabIndex = 145;
            PictureBox21.TabStop = false;
            // 
            // CheckBox15
            // 
            CheckBox15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox15.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox15.Checked = false;
            CheckBox15.Font = new Font("Segoe UI", 9.0f);
            CheckBox15.ForeColor = Color.White;
            CheckBox15.Location = new Point(36, 6);
            CheckBox15.Name = "CheckBox15";
            CheckBox15.Size = new Size(549, 24);
            CheckBox15.TabIndex = 144;
            CheckBox15.Text = "Snap cursor to default button";
            // 
            // PictureBox24
            // 
            PictureBox24.BackColor = Color.Transparent;
            PictureBox24.Image = (Image)resources.GetObject("PictureBox24.Image");
            PictureBox24.Location = new Point(6, 141);
            PictureBox24.Name = "PictureBox24";
            PictureBox24.Size = new Size(24, 24);
            PictureBox24.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox24.TabIndex = 226;
            PictureBox24.TabStop = false;
            // 
            // CheckBox18
            // 
            CheckBox18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox18.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox18.Checked = false;
            CheckBox18.Font = new Font("Segoe UI", 9.0f);
            CheckBox18.ForeColor = Color.White;
            CheckBox18.Location = new Point(36, 141);
            CheckBox18.Name = "CheckBox18";
            CheckBox18.Size = new Size(549, 24);
            CheckBox18.TabIndex = 225;
            CheckBox18.Text = "Paint Windows version in desktop (Might require Windows restart)";
            // 
            // PictureBox25
            // 
            PictureBox25.BackColor = Color.Transparent;
            PictureBox25.Image = (Image)resources.GetObject("PictureBox25.Image");
            PictureBox25.Location = new Point(6, 36);
            PictureBox25.Name = "PictureBox25";
            PictureBox25.Size = new Size(24, 24);
            PictureBox25.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox25.TabIndex = 224;
            PictureBox25.TabStop = false;
            // 
            // CheckBox19
            // 
            CheckBox19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox19.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox19.Checked = false;
            CheckBox19.Font = new Font("Segoe UI", 9.0f);
            CheckBox19.ForeColor = Color.White;
            CheckBox19.Location = new Point(36, 36);
            CheckBox19.Name = "CheckBox19";
            CheckBox19.Size = new Size(549, 24);
            CheckBox19.TabIndex = 223;
            CheckBox19.Text = "Show seconds in taskbar tray clock (Windows 10 or Windows 11 Moment 3 update)";
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
            Button10.Location = new Point(480, 455);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 207;
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
            Button7.Location = new Point(394, 455);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 206;
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
            Button8.Location = new Point(601, 455);
            Button8.Name = "Button8";
            Button8.Size = new Size(180, 34);
            Button8.TabIndex = 205;
            Button8.Text = "Load into current theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(Button9);
            GroupBox12.Controls.Add(Label12);
            GroupBox12.Controls.Add(Button11);
            GroupBox12.Controls.Add(Button12);
            GroupBox12.Controls.Add(EffectsEnabled);
            GroupBox12.Controls.Add(checker_img);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(769, 39);
            GroupBox12.TabIndex = 200;
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
            Button9.Location = new Point(222, 5);
            Button9.Name = "Button9";
            Button9.Size = new Size(126, 29);
            Button9.TabIndex = 112;
            Button9.Text = "Current applied";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            Label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
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
            Button11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
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
            Button12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button12.BackColor = Color.FromArgb(43, 43, 43);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = null;
            Button12.ImageAlign = ContentAlignment.MiddleRight;
            Button12.LineColor = Color.FromArgb(0, 66, 119);
            Button12.Location = new Point(351, 5);
            Button12.Name = "Button12";
            Button12.Size = new Size(130, 29);
            Button12.TabIndex = 108;
            Button12.Text = "Default Windows";
            Button12.UseVisualStyleBackColor = false;
            // 
            // EffectsEnabled
            // 
            EffectsEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            EffectsEnabled.BackColor = Color.FromArgb(43, 43, 43);
            EffectsEnabled.Checked = false;
            EffectsEnabled.DarkLight_Toggler = false;
            EffectsEnabled.Location = new Point(721, 9);
            EffectsEnabled.Name = "EffectsEnabled";
            EffectsEnabled.Size = new Size(40, 20);
            EffectsEnabled.TabIndex = 85;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = (Image)resources.GetObject("checker_img.Image");
            checker_img.Location = new Point(680, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // CheckBox27
            // 
            CheckBox27.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox27.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox27.Checked = false;
            CheckBox27.Font = new Font("Segoe UI", 9.0f);
            CheckBox27.ForeColor = Color.White;
            CheckBox27.Location = new Point(36, 36);
            CheckBox27.Name = "CheckBox27";
            CheckBox27.Size = new Size(549, 24);
            CheckBox27.TabIndex = 229;
            CheckBox27.Text = "Animate controls and elements inside window";
            // 
            // PictureBox36
            // 
            PictureBox36.BackColor = Color.Transparent;
            PictureBox36.Image = (Image)resources.GetObject("PictureBox36.Image");
            PictureBox36.Location = new Point(6, 36);
            PictureBox36.Name = "PictureBox36";
            PictureBox36.Size = new Size(24, 24);
            PictureBox36.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox36.TabIndex = 230;
            PictureBox36.TabStop = false;
            // 
            // WinEffecter
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(793, 501);
            Controls.Add(TabControl1);
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
            Name = "WinEffecter";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Windows Effects";
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox27).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox23).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            TabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            TabPage7.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            TabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox20).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).EndInit();
            TabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox30).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).EndInit();
            TabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox28).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            TabPage12.ResumeLayout(false);
            Panel3.ResumeLayout(false);
            Panel24.ResumeLayout(false);
            Panel20.ResumeLayout(false);
            Panel16.ResumeLayout(false);
            Panel12.ResumeLayout(false);
            Panel8.ResumeLayout(false);
            Panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox33).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox32).EndInit();
            TabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox35).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).EndInit();
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).EndInit();
            Load += new EventHandler(WinEffecter_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle EffectsEnabled;
        internal PictureBox checker_img;
        internal OpenFileDialog OpenFileDialog1;
        internal PictureBox PictureBox4;
        internal UI.WP.CheckBox CheckBox3;
        internal PictureBox PictureBox3;
        internal UI.WP.CheckBox CheckBox2;
        internal PictureBox PictureBox2;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.ComboBox ComboBox1;
        internal PictureBox PictureBox6;
        internal UI.WP.CheckBox CheckBox5;
        internal PictureBox PictureBox7;
        internal UI.WP.CheckBox CheckBox6;
        internal PictureBox PictureBox9;
        internal UI.WP.CheckBox CheckBox7;
        internal PictureBox PictureBox10;
        internal UI.WP.CheckBox CheckBox8;
        internal UI.WP.ComboBox ComboBox2;
        internal PictureBox PictureBox12;
        internal UI.WP.CheckBox CheckBox9;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal PictureBox PictureBox5;
        internal UI.WP.CheckBox CheckBox4;
        internal PictureBox PictureBox14;
        internal UI.WP.CheckBox CheckBox10;
        internal PictureBox PictureBox15;
        internal UI.WP.CheckBox CheckBox11;
        internal Label Label6;
        internal PictureBox PictureBox16;
        internal UI.WP.Trackbar Trackbar1;
        internal UI.WP.Button MD;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal UI.WP.CheckBox CheckBox12;
        internal PictureBox PictureBox1;
        internal UI.WP.Button Button2;
        internal Label Label2;
        internal PictureBox PictureBox11;
        internal UI.WP.Trackbar Trackbar3;
        internal UI.WP.Button Button1;
        internal Label Label1;
        internal PictureBox PictureBox8;
        internal UI.WP.Trackbar Trackbar2;
        internal TabPage TabPage7;
        internal UI.WP.Button Button3;
        internal Label Label3;
        internal PictureBox PictureBox13;
        internal UI.WP.Trackbar Trackbar4;
        internal UI.WP.Button Button4;
        internal Label Label4;
        internal PictureBox PictureBox17;
        internal UI.WP.Trackbar Trackbar5;
        internal TabPage TabPage8;
        internal PictureBox PictureBox18;
        internal UI.WP.CheckBox CheckBox13;
        internal PictureBox PictureBox19;
        internal UI.WP.CheckBox CheckBox14;
        internal UI.WP.Button Button5;
        internal Label Label5;
        internal PictureBox PictureBox20;
        internal UI.WP.Trackbar Trackbar6;
        internal PictureBox PictureBox21;
        internal UI.WP.CheckBox CheckBox15;
        internal PictureBox PictureBox22;
        internal UI.WP.CheckBox CheckBox16;
        internal PictureBox PictureBox23;
        internal UI.WP.CheckBox CheckBox17;
        internal TabPage TabPage9;
        internal PictureBox PictureBox24;
        internal UI.WP.CheckBox CheckBox18;
        internal PictureBox PictureBox25;
        internal UI.WP.CheckBox CheckBox19;
        internal PictureBox PictureBox26;
        internal UI.WP.CheckBox CheckBox20;
        internal UI.Retro.ButtonR ButtonR1;
        internal Panel Panel1;
        internal Panel Panel2;
        internal Label Label7;
        internal Timer Timer1;
        internal PictureBox PictureBox27;
        internal UI.WP.CheckBox CheckBox21;
        internal PictureBox PictureBox28;
        internal UI.WP.CheckBox CheckBox22;
        internal TabPage TabPage10;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton1;
        internal Label Label8;
        internal PictureBox PictureBox29;
        internal PictureBox PictureBox30;
        internal UI.WP.CheckBox CheckBox23;
        internal TabPage TabPage11;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.AlertBox AlertBox2;
        internal UI.WP.AlertBox AlertBox3;
        internal PictureBox PictureBox31;
        internal UI.WP.CheckBox CheckBox24;
        internal TabPage TabPage12;
        internal PictureBox PictureBox33;
        internal PictureBox PictureBox32;
        internal UI.WP.RadioImage RadioImage7;
        internal UI.WP.RadioImage RadioImage6;
        internal UI.WP.RadioImage RadioImage5;
        internal UI.WP.RadioImage RadioImage4;
        internal UI.WP.RadioImage RadioImage3;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.WP.RadioImage RadioImage1;
        internal Panel Panel3;
        internal Panel Panel24;
        internal Label Label15;
        internal Panel P3;
        internal Panel P2;
        internal Panel P1;
        internal Panel Panel20;
        internal Label Label14;
        internal Panel B3;
        internal Panel B2;
        internal Panel B1;
        internal Panel Panel16;
        internal Label Label13;
        internal Panel G3;
        internal Panel G2;
        internal Panel G1;
        internal Panel Panel12;
        internal Label Label11;
        internal Panel Y3;
        internal Panel Y2;
        internal Panel Y1;
        internal Panel Panel8;
        internal Label Label10;
        internal Panel O3;
        internal Panel O2;
        internal Panel O1;
        internal Panel Panel4;
        internal Label Label9;
        internal Panel R3;
        internal Panel R2;
        internal Panel R1;
        internal UI.WP.AlertBox AlertBox4;
        internal UI.WP.AlertBox AlertBox6;
        internal UI.WP.AlertBox AlertBox5;
        internal PictureBox PictureBox34;
        internal UI.WP.CheckBox CheckBox25;
        internal PictureBox PictureBox35;
        internal UI.WP.CheckBox CheckBox26;
        internal UI.WP.CheckBox CheckBox27;
        internal PictureBox PictureBox36;
    }
}