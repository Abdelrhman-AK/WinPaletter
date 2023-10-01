using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class EditInfo : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(EditInfo));
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            AlertBox2 = new UI.WP.AlertBox();
            PictureBox2 = new PictureBox();
            Label2 = new Label();
            Label1 = new Label();
            TextBox4 = new UI.WP.TextBox();
            TextBox4.TextChanged += new EventHandler(TextBox4_TextChanged);
            PictureBox1 = new PictureBox();
            TextBox5 = new UI.WP.TextBox();
            Label3 = new Label();
            PictureBox4 = new PictureBox();
            PictureBox3 = new PictureBox();
            Label4 = new Label();
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            PictureBox5 = new PictureBox();
            TextBox2 = new UI.WP.TextBox();
            TextBox2.TextChanged += new EventHandler(TextBox2_TextChanged);
            Label5 = new Label();
            TextBox3 = new UI.WP.TextBox();
            Separator1 = new UI.WP.SeparatorH();
            TabPage3 = new TabPage();
            Separator4 = new UI.WP.SeparatorH();
            AlertBox21 = new UI.WP.AlertBox();
            CheckBox7 = new UI.WP.CheckBox();
            PictureBox42 = new PictureBox();
            AlertBox3 = new UI.WP.AlertBox();
            Label14 = new Label();
            PictureBox18 = new PictureBox();
            TextBox6 = new UI.WP.TextBox();
            TabPage2 = new TabPage();
            Label13 = new Label();
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            Label12 = new Label();
            PictureBox16 = new PictureBox();
            Label11 = new Label();
            PictureBox17 = new PictureBox();
            Separator3 = new UI.WP.SeparatorH();
            Label10 = new Label();
            PictureBox15 = new PictureBox();
            Label9 = new Label();
            StoreItem1 = new UI.Controllers.StoreItem();
            PictureBox14 = new PictureBox();
            PictureBox13 = new PictureBox();
            PictureBox12 = new PictureBox();
            PictureBox11 = new PictureBox();
            PictureBox10 = new PictureBox();
            PictureBox9 = new PictureBox();
            CheckBox6 = new UI.WP.CheckBox();
            CheckBox6.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox6_CheckedChanged);
            CheckBox5 = new UI.WP.CheckBox();
            CheckBox5.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox5_CheckedChanged);
            CheckBox4 = new UI.WP.CheckBox();
            CheckBox4.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox4_CheckedChanged);
            CheckBox3 = new UI.WP.CheckBox();
            CheckBox3.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox3_CheckedChanged);
            CheckBox2 = new UI.WP.CheckBox();
            CheckBox2.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox2_CheckedChanged);
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            PictureBox8 = new PictureBox();
            Label8 = new Label();
            color2 = new UI.Controllers.ColorItem();
            color2.DragDrop += new DragEventHandler(Color1_2_DragDrop);
            color2.Click += new EventHandler(Color2_Click);
            color1 = new UI.Controllers.ColorItem();
            color1.DragDrop += new DragEventHandler(Color1_2_DragDrop);
            color1.Click += new EventHandler(Color1_Click);
            Separator2 = new UI.WP.SeparatorH();
            PictureBox6 = new PictureBox();
            Label6 = new Label();
            Label7 = new Label();
            PictureBox7 = new PictureBox();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox42).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).BeginInit();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            SuspendLayout();
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ItemSize = new Size(30, 150);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(12, 12);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(840, 393);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 26;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(AlertBox2);
            TabPage1.Controls.Add(PictureBox2);
            TabPage1.Controls.Add(Label2);
            TabPage1.Controls.Add(Label1);
            TabPage1.Controls.Add(TextBox4);
            TabPage1.Controls.Add(PictureBox1);
            TabPage1.Controls.Add(TextBox5);
            TabPage1.Controls.Add(Label3);
            TabPage1.Controls.Add(PictureBox4);
            TabPage1.Controls.Add(PictureBox3);
            TabPage1.Controls.Add(Label4);
            TabPage1.Controls.Add(TextBox1);
            TabPage1.Controls.Add(PictureBox5);
            TabPage1.Controls.Add(TextBox2);
            TabPage1.Controls.Add(Label5);
            TabPage1.Controls.Add(TextBox3);
            TabPage1.Controls.Add(Separator1);
            TabPage1.Location = new Point(154, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(682, 385);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Main info";
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
            AlertBox2.Location = new Point(170, 275);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(506, 22);
            AlertBox2.TabIndex = 138;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "You can include tags in descriptions to make search in store easier";
            // 
            // PictureBox2
            // 
            PictureBox2.BackgroundImageLayout = ImageLayout.Center;
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(6, 6);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 8;
            PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f);
            Label2.Location = new Point(36, 6);
            Label2.Name = "Label2";
            Label2.Size = new Size(124, 24);
            Label2.TabIndex = 9;
            Label2.Text = "Theme name:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f);
            Label1.Location = new Point(36, 36);
            Label1.Name = "Label1";
            Label1.Size = new Size(124, 24);
            Label1.TabIndex = 11;
            Label1.Text = "Theme version:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox4
            // 
            TextBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox4.BackColor = Color.FromArgb(55, 55, 55);
            TextBox4.DrawOnGlass = false;
            TextBox4.ForeColor = Color.White;
            TextBox4.Location = new Point(170, 312);
            TextBox4.MaxLength = 32767;
            TextBox4.Multiline = false;
            TextBox4.Name = "TextBox4";
            TextBox4.ReadOnly = false;
            TextBox4.Scrollbars = ScrollBars.None;
            TextBox4.SelectedText = "";
            TextBox4.SelectionLength = 0;
            TextBox4.SelectionStart = 0;
            TextBox4.Size = new Size(506, 24);
            TextBox4.TabIndex = 23;
            TextBox4.TextAlign = HorizontalAlignment.Left;
            TextBox4.UseSystemPasswordChar = false;
            TextBox4.WordWrap = true;
            // 
            // PictureBox1
            // 
            PictureBox1.BackgroundImageLayout = ImageLayout.Center;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 36);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 10;
            PictureBox1.TabStop = false;
            // 
            // TextBox5
            // 
            TextBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox5.BackColor = Color.FromArgb(55, 55, 55);
            TextBox5.DrawOnGlass = false;
            TextBox5.ForeColor = Color.White;
            TextBox5.Location = new Point(170, 342);
            TextBox5.MaxLength = 32767;
            TextBox5.Multiline = false;
            TextBox5.Name = "TextBox5";
            TextBox5.ReadOnly = false;
            TextBox5.Scrollbars = ScrollBars.None;
            TextBox5.SelectedText = "";
            TextBox5.SelectionLength = 0;
            TextBox5.SelectionStart = 0;
            TextBox5.Size = new Size(506, 24);
            TextBox5.TabIndex = 22;
            TextBox5.TextAlign = HorizontalAlignment.Left;
            TextBox5.UseSystemPasswordChar = false;
            TextBox5.WordWrap = true;
            // 
            // Label3
            // 
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 9.0f);
            Label3.Location = new Point(36, 66);
            Label3.Name = "Label3";
            Label3.Size = new Size(124, 24);
            Label3.TabIndex = 13;
            Label3.Text = "Theme description:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox4
            // 
            PictureBox4.BackgroundImageLayout = ImageLayout.Center;
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(6, 342);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 20;
            PictureBox4.TabStop = false;
            // 
            // PictureBox3
            // 
            PictureBox3.BackgroundImageLayout = ImageLayout.Center;
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(6, 66);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 12;
            PictureBox3.TabStop = false;
            // 
            // Label4
            // 
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f);
            Label4.Location = new Point(36, 342);
            Label4.Name = "Label4";
            Label4.Size = new Size(124, 24);
            Label4.TabIndex = 21;
            Label4.Text = "Social media link:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(170, 6);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(506, 24);
            TextBox1.TabIndex = 14;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // PictureBox5
            // 
            PictureBox5.BackgroundImageLayout = ImageLayout.Center;
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(6, 312);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 18;
            PictureBox5.TabStop = false;
            // 
            // TextBox2
            // 
            TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(170, 36);
            TextBox2.MaxLength = 12;
            TextBox2.Multiline = false;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = false;
            TextBox2.Scrollbars = ScrollBars.None;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(506, 24);
            TextBox2.TabIndex = 15;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // Label5
            // 
            Label5.BackColor = Color.Transparent;
            Label5.Font = new Font("Segoe UI", 9.0f);
            Label5.Location = new Point(36, 312);
            Label5.Name = "Label5";
            Label5.Size = new Size(124, 24);
            Label5.TabIndex = 19;
            Label5.Text = "Author:";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox3
            // 
            TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox3.BackColor = Color.FromArgb(55, 55, 55);
            TextBox3.DrawOnGlass = false;
            TextBox3.ForeColor = Color.White;
            TextBox3.Location = new Point(170, 67);
            TextBox3.MaxLength = 32767;
            TextBox3.Multiline = true;
            TextBox3.Name = "TextBox3";
            TextBox3.ReadOnly = false;
            TextBox3.Scrollbars = ScrollBars.Vertical;
            TextBox3.SelectedText = "";
            TextBox3.SelectionLength = 0;
            TextBox3.SelectionStart = 0;
            TextBox3.Size = new Size(506, 202);
            TextBox3.TabIndex = 16;
            TextBox3.TextAlign = HorizontalAlignment.Left;
            TextBox3.UseSystemPasswordChar = false;
            TextBox3.WordWrap = true;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(6, 303);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(669, 1);
            Separator1.TabIndex = 17;
            Separator1.TabStop = false;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(Separator4);
            TabPage3.Controls.Add(AlertBox21);
            TabPage3.Controls.Add(CheckBox7);
            TabPage3.Controls.Add(PictureBox42);
            TabPage3.Controls.Add(AlertBox3);
            TabPage3.Controls.Add(Label14);
            TabPage3.Controls.Add(PictureBox18);
            TabPage3.Controls.Add(TextBox6);
            TabPage3.Location = new Point(154, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(682, 385);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Theme Resources Pack";
            // 
            // Separator4
            // 
            Separator4.AlternativeLook = false;
            Separator4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator4.Location = new Point(6, 85);
            Separator4.Name = "Separator4";
            Separator4.Size = new Size(670, 1);
            Separator4.TabIndex = 143;
            Separator4.TabStop = false;
            Separator4.Text = "Separator4";
            // 
            // AlertBox21
            // 
            AlertBox21.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox21.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox21.BackColor = Color.FromArgb(125, 20, 30);
            AlertBox21.CenterText = false;
            AlertBox21.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox21.Font = new Font("Segoe UI", 9.0f);
            AlertBox21.Image = null;
            AlertBox21.Location = new Point(40, 36);
            AlertBox21.Name = "AlertBox21";
            AlertBox21.Size = new Size(636, 40);
            AlertBox21.TabIndex = 142;
            AlertBox21.TabStop = false;
            AlertBox21.Text = resources.GetString("AlertBox21.Text");
            // 
            // CheckBox7
            // 
            CheckBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox7.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox7.Checked = false;
            CheckBox7.Font = new Font("Segoe UI", 9.0f);
            CheckBox7.ForeColor = Color.White;
            CheckBox7.Location = new Point(36, 6);
            CheckBox7.Name = "CheckBox7";
            CheckBox7.Size = new Size(640, 24);
            CheckBox7.TabIndex = 141;
            CheckBox7.Text = "Make saving this theme as a file exports a resources pack that contains files use" + "d in theme";
            // 
            // PictureBox42
            // 
            PictureBox42.Image = (Image)resources.GetObject("PictureBox42.Image");
            PictureBox42.Location = new Point(6, 6);
            PictureBox42.Name = "PictureBox42";
            PictureBox42.Size = new Size(24, 24);
            PictureBox42.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox42.TabIndex = 140;
            PictureBox42.TabStop = false;
            // 
            // AlertBox3
            // 
            AlertBox3.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox3.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox3.CenterText = false;
            AlertBox3.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox3.Font = new Font("Segoe UI", 9.0f);
            AlertBox3.Image = null;
            AlertBox3.Location = new Point(6, 357);
            AlertBox3.Name = "AlertBox3";
            AlertBox3.Size = new Size(670, 22);
            AlertBox3.TabIndex = 139;
            AlertBox3.TabStop = false;
            AlertBox3.Text = "This is used especially in WinPaletter Store, if a user didn't accept this, the t" + "heme won't be applied";
            // 
            // Label14
            // 
            Label14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label14.BackColor = Color.Transparent;
            Label14.Font = new Font("Segoe UI", 9.0f);
            Label14.Location = new Point(36, 92);
            Label14.Name = "Label14";
            Label14.Size = new Size(640, 24);
            Label14.TabIndex = 18;
            Label14.Text = @"Type here the credits or license\s of any resource you used in the theme (images," + " audios, screensavers, ...)";
            Label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox18
            // 
            PictureBox18.BackgroundImageLayout = ImageLayout.Center;
            PictureBox18.Image = (Image)resources.GetObject("PictureBox18.Image");
            PictureBox18.Location = new Point(6, 92);
            PictureBox18.Name = "PictureBox18";
            PictureBox18.Size = new Size(24, 24);
            PictureBox18.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox18.TabIndex = 17;
            PictureBox18.TabStop = false;
            // 
            // TextBox6
            // 
            TextBox6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TextBox6.BackColor = Color.FromArgb(55, 55, 55);
            TextBox6.DrawOnGlass = false;
            TextBox6.ForeColor = Color.White;
            TextBox6.Location = new Point(6, 122);
            TextBox6.MaxLength = 32767;
            TextBox6.Multiline = true;
            TextBox6.Name = "TextBox6";
            TextBox6.ReadOnly = false;
            TextBox6.Scrollbars = ScrollBars.Vertical;
            TextBox6.SelectedText = "";
            TextBox6.SelectionLength = 0;
            TextBox6.SelectionStart = 0;
            TextBox6.Size = new Size(670, 229);
            TextBox6.TabIndex = 19;
            TextBox6.TextAlign = HorizontalAlignment.Left;
            TextBox6.UseSystemPasswordChar = false;
            TextBox6.WordWrap = true;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(Label13);
            TabPage2.Controls.Add(Trackbar1);
            TabPage2.Controls.Add(Label12);
            TabPage2.Controls.Add(PictureBox16);
            TabPage2.Controls.Add(Label11);
            TabPage2.Controls.Add(PictureBox17);
            TabPage2.Controls.Add(Separator3);
            TabPage2.Controls.Add(Label10);
            TabPage2.Controls.Add(PictureBox15);
            TabPage2.Controls.Add(Label9);
            TabPage2.Controls.Add(StoreItem1);
            TabPage2.Controls.Add(PictureBox14);
            TabPage2.Controls.Add(PictureBox13);
            TabPage2.Controls.Add(PictureBox12);
            TabPage2.Controls.Add(PictureBox11);
            TabPage2.Controls.Add(PictureBox10);
            TabPage2.Controls.Add(PictureBox9);
            TabPage2.Controls.Add(CheckBox6);
            TabPage2.Controls.Add(CheckBox5);
            TabPage2.Controls.Add(CheckBox4);
            TabPage2.Controls.Add(CheckBox3);
            TabPage2.Controls.Add(CheckBox2);
            TabPage2.Controls.Add(CheckBox1);
            TabPage2.Controls.Add(PictureBox8);
            TabPage2.Controls.Add(Label8);
            TabPage2.Controls.Add(color2);
            TabPage2.Controls.Add(color1);
            TabPage2.Controls.Add(Separator2);
            TabPage2.Controls.Add(PictureBox6);
            TabPage2.Controls.Add(Label6);
            TabPage2.Controls.Add(Label7);
            TabPage2.Controls.Add(PictureBox7);
            TabPage2.Location = new Point(154, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(682, 385);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Store item info";
            // 
            // Label13
            // 
            Label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label13.BackColor = Color.Transparent;
            Label13.Font = new Font("Segoe UI", 9.0f);
            Label13.Location = new Point(351, 6);
            Label13.Name = "Label13";
            Label13.Size = new Size(324, 54);
            Label13.TabIndex = 46;
            Label13.Text = "Descriptive colors should give the user an idea about the most used or main color" + "s in the theme";
            Label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar1.LargeChange = 2;
            Trackbar1.Location = new Point(166, 69);
            Trackbar1.Maximum = 10;
            Trackbar1.Minimum = 0;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(509, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 44;
            Trackbar1.Text = "Trackbar1";
            Trackbar1.Value = 1;
            // 
            // Label12
            // 
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f);
            Label12.Location = new Point(36, 66);
            Label12.Name = "Label12";
            Label12.Size = new Size(124, 24);
            Label12.TabIndex = 43;
            Label12.Text = "Background pattern:";
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox16
            // 
            PictureBox16.BackgroundImageLayout = ImageLayout.Center;
            PictureBox16.Image = My.Resources.Store_DoneByWinPaletter;
            PictureBox16.Location = new Point(6, 325);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(24, 24);
            PictureBox16.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox16.TabIndex = 39;
            PictureBox16.TabStop = false;
            // 
            // Label11
            // 
            Label11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label11.BackColor = Color.Transparent;
            Label11.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label11.Location = new Point(36, 325);
            Label11.Name = "Label11";
            Label11.Size = new Size(639, 24);
            Label11.TabIndex = 40;
            Label11.Text = "This icon means that the theme is designed by WinPaletter developer";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox17
            // 
            PictureBox17.BackgroundImageLayout = ImageLayout.Center;
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(6, 66);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(24, 24);
            PictureBox17.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox17.TabIndex = 42;
            PictureBox17.TabStop = false;
            // 
            // Separator3
            // 
            Separator3.AlternativeLook = false;
            Separator3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator3.Location = new Point(6, 316);
            Separator3.Name = "Separator3";
            Separator3.Size = new Size(669, 1);
            Separator3.TabIndex = 41;
            Separator3.TabStop = false;
            // 
            // Label10
            // 
            Label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label10.BackColor = Color.Transparent;
            Label10.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label10.Location = new Point(36, 355);
            Label10.Name = "Label10";
            Label10.Size = new Size(639, 24);
            Label10.TabIndex = 38;
            Label10.Text = "This icon means that the theme is designed by a user (not by WinPaletter develope" + "r)";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox15
            // 
            PictureBox15.BackgroundImageLayout = ImageLayout.Center;
            PictureBox15.Image = My.Resources.Store_DoneByUser;
            PictureBox15.Location = new Point(6, 355);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox15.TabIndex = 37;
            PictureBox15.TabStop = false;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label9.BackColor = Color.Transparent;
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label9.Location = new Point(349, 129);
            Label9.Name = "Label9";
            Label9.Size = new Size(317, 35);
            Label9.TabIndex = 36;
            Label9.Text = "This is the look of this theme as an item inside WinPaletter Store";
            Label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StoreItem1
            // 
            StoreItem1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            StoreItem1.CP = null;
            StoreItem1.DoneByWinPaletter = false;
            StoreItem1.FileName = null;
            StoreItem1.Font = new Font("Segoe UI", 9.0f);
            StoreItem1.Location = new Point(349, 177);
            StoreItem1.MD5_PackFile = null;
            StoreItem1.MD5_ThemeFile = null;
            StoreItem1.Name = "StoreItem1";
            StoreItem1.Size = new Size(317, 109);
            StoreItem1.TabIndex = 35;
            StoreItem1.URL_PackFile = null;
            StoreItem1.URL_ThemeFile = null;
            // 
            // PictureBox14
            // 
            PictureBox14.BackgroundImageLayout = ImageLayout.Center;
            PictureBox14.Image = My.Resources.Store_DesignedForXP;
            PictureBox14.Location = new Point(42, 284);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox14.TabIndex = 34;
            PictureBox14.TabStop = false;
            // 
            // PictureBox13
            // 
            PictureBox13.BackgroundImageLayout = ImageLayout.Center;
            PictureBox13.Image = My.Resources.Store_DesignedForVista;
            PictureBox13.Location = new Point(42, 254);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(24, 24);
            PictureBox13.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox13.TabIndex = 33;
            PictureBox13.TabStop = false;
            // 
            // PictureBox12
            // 
            PictureBox12.BackgroundImageLayout = ImageLayout.Center;
            PictureBox12.Image = My.Resources.Store_DesignedFor7;
            PictureBox12.Location = new Point(42, 224);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox12.TabIndex = 32;
            PictureBox12.TabStop = false;
            // 
            // PictureBox11
            // 
            PictureBox11.BackgroundImageLayout = ImageLayout.Center;
            PictureBox11.Image = My.Resources.Store_DesignedFor8;
            PictureBox11.Location = new Point(42, 194);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 31;
            PictureBox11.TabStop = false;
            // 
            // PictureBox10
            // 
            PictureBox10.BackgroundImageLayout = ImageLayout.Center;
            PictureBox10.Image = My.Resources.Store_DesignedFor10;
            PictureBox10.Location = new Point(42, 164);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 30;
            PictureBox10.TabStop = false;
            // 
            // PictureBox9
            // 
            PictureBox9.BackgroundImageLayout = ImageLayout.Center;
            PictureBox9.Image = My.Resources.Store_DesignedFor11;
            PictureBox9.Location = new Point(42, 134);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 29;
            PictureBox9.TabStop = false;
            // 
            // CheckBox6
            // 
            CheckBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox6.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox6.Checked = true;
            CheckBox6.Font = new Font("Segoe UI", 9.0f);
            CheckBox6.ForeColor = Color.White;
            CheckBox6.Location = new Point(72, 285);
            CheckBox6.Name = "CheckBox6";
            CheckBox6.Size = new Size(241, 22);
            CheckBox6.TabIndex = 28;
            CheckBox6.Text = "Windows XP";
            // 
            // CheckBox5
            // 
            CheckBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox5.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox5.Checked = true;
            CheckBox5.Font = new Font("Segoe UI", 9.0f);
            CheckBox5.ForeColor = Color.White;
            CheckBox5.Location = new Point(72, 255);
            CheckBox5.Name = "CheckBox5";
            CheckBox5.Size = new Size(241, 22);
            CheckBox5.TabIndex = 27;
            CheckBox5.Text = "Windows Vista";
            // 
            // CheckBox4
            // 
            CheckBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox4.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox4.Checked = true;
            CheckBox4.Font = new Font("Segoe UI", 9.0f);
            CheckBox4.ForeColor = Color.White;
            CheckBox4.Location = new Point(72, 225);
            CheckBox4.Name = "CheckBox4";
            CheckBox4.Size = new Size(241, 22);
            CheckBox4.TabIndex = 26;
            CheckBox4.Text = "Windows 7";
            // 
            // CheckBox3
            // 
            CheckBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox3.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox3.Checked = true;
            CheckBox3.Font = new Font("Segoe UI", 9.0f);
            CheckBox3.ForeColor = Color.White;
            CheckBox3.Location = new Point(72, 195);
            CheckBox3.Name = "CheckBox3";
            CheckBox3.Size = new Size(241, 22);
            CheckBox3.TabIndex = 25;
            CheckBox3.Text = "Windows 8.1";
            // 
            // CheckBox2
            // 
            CheckBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = true;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(72, 165);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(241, 22);
            CheckBox2.TabIndex = 24;
            CheckBox2.Text = "Windows 10";
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(72, 135);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(241, 22);
            CheckBox1.TabIndex = 23;
            CheckBox1.Text = "Windows 11";
            // 
            // PictureBox8
            // 
            PictureBox8.BackgroundImageLayout = ImageLayout.Center;
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(6, 105);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 21;
            PictureBox8.TabStop = false;
            // 
            // Label8
            // 
            Label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label8.BackColor = Color.Transparent;
            Label8.Font = new Font("Segoe UI", 9.0f);
            Label8.Location = new Point(39, 109);
            Label8.Name = "Label8";
            Label8.Size = new Size(636, 18);
            Label8.TabIndex = 22;
            Label8.Text = "This theme is designed specifically for:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // color2
            // 
            color2.AllowDrop = true;
            color2.BackColor = Color.FromArgb(199, 49, 61);
            color2.DefaultColor = Color.FromArgb(199, 49, 61);
            color2.DontShowInfo = false;
            color2.Location = new Point(167, 36);
            color2.Margin = new Padding(4, 3, 4, 3);
            color2.Name = "color2";
            color2.Size = new Size(121, 24);
            color2.TabIndex = 20;
            // 
            // color1
            // 
            color1.AllowDrop = true;
            color1.BackColor = Color.FromArgb(0, 81, 210);
            color1.DefaultColor = Color.FromArgb(0, 81, 210);
            color1.DontShowInfo = false;
            color1.Location = new Point(167, 6);
            color1.Margin = new Padding(4, 3, 4, 3);
            color1.Name = "color1";
            color1.Size = new Size(121, 24);
            color1.TabIndex = 19;
            // 
            // Separator2
            // 
            Separator2.AlternativeLook = false;
            Separator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator2.Location = new Point(6, 98);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(669, 1);
            Separator2.TabIndex = 18;
            Separator2.TabStop = false;
            // 
            // PictureBox6
            // 
            PictureBox6.BackgroundImageLayout = ImageLayout.Center;
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(6, 6);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 12;
            PictureBox6.TabStop = false;
            // 
            // Label6
            // 
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f);
            Label6.Location = new Point(36, 6);
            Label6.Name = "Label6";
            Label6.Size = new Size(124, 24);
            Label6.TabIndex = 13;
            Label6.Text = "Descriptive color 1:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 9.0f);
            Label7.Location = new Point(36, 36);
            Label7.Name = "Label7";
            Label7.Size = new Size(124, 24);
            Label7.TabIndex = 15;
            Label7.Text = "Descriptive color 2:";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            PictureBox7.BackgroundImageLayout = ImageLayout.Center;
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(6, 36);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 14;
            PictureBox7.TabStop = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(586, 411);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 25;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(14, 69, 94);
            Button1.Location = new Point(672, 411);
            Button1.Name = "Button1";
            Button1.Size = new Size(180, 34);
            Button1.TabIndex = 24;
            Button1.Text = "Load into current theme";
            Button1.UseVisualStyleBackColor = false;
            // 
            // EditInfo
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(864, 457);
            Controls.Add(TabControl1);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Font = new Font("Segoe UI", 9.0f);
            ForeColor = Color.White;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(420, 400);
            Name = "EditInfo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Edit info";
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox42).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).EndInit();
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            Load += new EventHandler(EditInfo_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal PictureBox PictureBox1;
        internal Label Label1;
        internal PictureBox PictureBox3;
        internal Label Label3;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.TextBox TextBox3;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.TextBox TextBox4;
        internal UI.WP.TextBox TextBox5;
        internal PictureBox PictureBox4;
        internal Label Label4;
        internal PictureBox PictureBox5;
        internal Label Label5;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal UI.WP.SeparatorH Separator2;
        internal PictureBox PictureBox6;
        internal Label Label6;
        internal Label Label7;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox14;
        internal PictureBox PictureBox13;
        internal PictureBox PictureBox12;
        internal PictureBox PictureBox11;
        internal PictureBox PictureBox10;
        internal PictureBox PictureBox9;
        internal UI.WP.CheckBox CheckBox6;
        internal UI.WP.CheckBox CheckBox5;
        internal UI.WP.CheckBox CheckBox4;
        internal UI.WP.CheckBox CheckBox3;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.CheckBox CheckBox1;
        internal PictureBox PictureBox8;
        internal Label Label8;
        internal UI.Controllers.ColorItem color2;
        internal UI.Controllers.ColorItem color1;
        internal UI.Controllers.StoreItem StoreItem1;
        internal Label Label9;
        internal UI.WP.SeparatorH Separator3;
        internal Label Label11;
        internal PictureBox PictureBox16;
        internal Label Label10;
        internal PictureBox PictureBox15;
        internal UI.WP.Trackbar Trackbar1;
        internal Label Label12;
        internal PictureBox PictureBox17;
        internal UI.WP.AlertBox AlertBox2;
        internal Label Label13;
        internal TabPage TabPage3;
        internal UI.WP.AlertBox AlertBox3;
        internal Label Label14;
        internal PictureBox PictureBox18;
        internal UI.WP.TextBox TextBox6;
        internal UI.WP.SeparatorH Separator4;
        internal UI.WP.AlertBox AlertBox21;
        internal UI.WP.CheckBox CheckBox7;
        internal PictureBox PictureBox42;
    }
}