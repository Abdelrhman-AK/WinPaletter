using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ExternalTerminal : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalTerminal));
            GroupBox73 = new UI.WP.GroupBox();
            PictureBox23 = new PictureBox();
            CMD4 = new UI.Simulation.WinCMD();
            Label137 = new Label();
            GroupBox51 = new UI.WP.GroupBox();
            Separator1 = new UI.WP.SeparatorH();
            Label4 = new Label();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            ComboBox1 = new UI.WP.ComboBox();
            PictureBox17 = new PictureBox();
            Label102 = new Label();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            CheckBox1 = new UI.WP.CheckBox();
            Separator2 = new UI.WP.SeparatorH();
            OpenWPTHDlg = new OpenFileDialog();
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            GroupBox2 = new UI.WP.GroupBox();
            PictureBox15 = new PictureBox();
            Separator3 = new UI.WP.SeparatorH();
            ExtTerminal_AccentBackgroundLbl = new Label();
            ExtTerminal_PopupForegroundLbl = new Label();
            Label50 = new Label();
            ExtTerminal_AccentForegroundLbl = new Label();
            ExtTerminal_PopupBackgroundLbl = new Label();
            ExtTerminal_AccentBackgroundBar = new UI.WP.Trackbar();
            ExtTerminal_AccentBackgroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(ExtTerminal_AccentBackgroundBar_Scroll);
            ExtTerminal_PopupBackgroundBar = new UI.WP.Trackbar();
            ExtTerminal_PopupBackgroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(ExtTerminal_PopupBackgroundBar_Scroll);
            ExtTerminal_AccentForegroundBar = new UI.WP.Trackbar();
            ExtTerminal_AccentForegroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(ExtTerminal_AccentForegroundBar_Scroll);
            Label18 = new Label();
            Label17 = new Label();
            Label49 = new Label();
            ExtTerminal_PopupForegroundBar = new UI.WP.Trackbar();
            ExtTerminal_PopupForegroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(ExtTerminal_PopupForegroundBar_Scroll);
            Label6 = new Label();
            GroupBox1 = new UI.WP.GroupBox();
            PictureBox14 = new PictureBox();
            Label31 = new Label();
            Label19 = new Label();
            ExtTerminal_ColorTable00 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable00.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable00.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label7 = new Label();
            Label32 = new Label();
            ExtTerminal_ColorTable01 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable01.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable01.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label20 = new Label();
            ExtTerminal_ColorTable02 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable02.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable02.DragDrop += new DragEventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable03 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable03.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable03.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label33 = new Label();
            Label21 = new Label();
            Label22 = new Label();
            ExtTerminal_ColorTable04 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable04.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable04.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label26 = new Label();
            Label34 = new Label();
            ExtTerminal_ColorTable05 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable05.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable05.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label25 = new Label();
            ExtTerminal_ColorTable06 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable06.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable06.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label24 = new Label();
            Label27 = new Label();
            ExtTerminal_ColorTable15 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable15.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable15.DragDrop += new DragEventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable07 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable07.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable07.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label23 = new Label();
            ExtTerminal_ColorTable08 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable08.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable08.DragDrop += new DragEventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable14 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable14.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable14.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label28 = new Label();
            Label30 = new Label();
            ExtTerminal_ColorTable09 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable09.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable09.DragDrop += new DragEventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable13 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable13.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable13.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label29 = new Label();
            ExtTerminal_ColorTable12 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable12.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable12.DragDrop += new DragEventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable10 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable10.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable10.DragDrop += new DragEventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable11 = new UI.Controllers.ColorItem();
            ExtTerminal_ColorTable11.Click += new EventHandler(ColorTable00_Click);
            ExtTerminal_ColorTable11.DragDrop += new DragEventHandler(ColorTable00_Click);
            TabPage2 = new TabPage();
            GroupBox4 = new UI.WP.GroupBox();
            FontName = new Label();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            ExtTerminal_FontSizeVal = new UI.WP.Button();
            ExtTerminal_FontSizeVal.Click += new EventHandler(ExtTerminal_FontSizeVal_Click);
            ExtTerminal_RasterToggle = new UI.WP.Toggle();
            ExtTerminal_RasterToggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(ExtTerminal_RasterToggle_CheckedChanged);
            PictureBox1 = new PictureBox();
            Label58 = new Label();
            ExtTerminal_FontSizeBar = new UI.WP.Trackbar();
            ExtTerminal_FontSizeBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(ExtTerminal_FontSizeBar_Scroll);
            ExtTerminal_FontWeightBox = new UI.WP.ComboBox();
            ExtTerminal_FontWeightBox.SelectedIndexChanged += new EventHandler(ExtTerminal_FontWeightBox_SelectedIndexChanged);
            Label61 = new Label();
            PictureBox6 = new PictureBox();
            PictureBox3 = new PictureBox();
            Label35 = new Label();
            Button25 = new UI.WP.Button();
            PictureBox4 = new PictureBox();
            Label59 = new Label();
            RasterList = new UI.WP.ComboBox();
            RasterList.SelectedIndexChanged += new EventHandler(RasterList_SelectedIndexChanged);
            TabPage3 = new TabPage();
            Label3 = new Label();
            GroupBox34 = new UI.WP.GroupBox();
            PictureBox8 = new PictureBox();
            ExtTerminal_PreviewCUR_Val = new UI.WP.Button();
            ExtTerminal_PreviewCUR_Val.Click += new EventHandler(ExtTerminal_PreviewCUR_Val_Click);
            PictureBox7 = new PictureBox();
            ExtTerminal_CursorStyle = new UI.WP.ComboBox();
            ExtTerminal_CursorStyle.SelectedIndexChanged += new EventHandler(ExtTerminal_CursorStyle_SelectedIndexChanged_1);
            Label60 = new Label();
            ExtTerminal_CursorSizeBar = new UI.WP.Trackbar();
            ExtTerminal_CursorSizeBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(ExtTerminal_CursorSizeBar_Scroll);
            Label1 = new Label();
            PictureBox9 = new PictureBox();
            Label2 = new Label();
            ExtTerminal_PreviewCUR = new Panel();
            ExtTerminal_PreviewCUR2 = new Panel();
            ExtTerminal_PreviewCursorInner = new Panel();
            ExtTerminal_CursorColor = new UI.Controllers.ColorItem();
            ExtTerminal_CursorColor.Click += new EventHandler(ExtTerminal_CursorColor_Click);
            ExtTerminal_CursorColor.DragDrop += new DragEventHandler(ExtTerminal_CursorColor_Click);
            TabPage4 = new TabPage();
            Label5 = new Label();
            GroupBox12 = new UI.WP.GroupBox();
            ExtTerminal_OpacityVal = new UI.WP.Button();
            ExtTerminal_OpacityVal.Click += new EventHandler(ExtTerminal_OpacityVal_Click);
            PictureBox10 = new PictureBox();
            PictureBox13 = new PictureBox();
            ExtTerminal_LineSelection = new UI.WP.CheckBox();
            ExtTerminal_EnhancedTerminal = new UI.WP.CheckBox();
            PictureBox12 = new PictureBox();
            ExtTerminal_TerminalScrolling = new UI.WP.CheckBox();
            ExtTerminal_OpacityBar = new UI.WP.Trackbar();
            ExtTerminal_OpacityBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(ExtTerminal_OpacityBar_Scroll);
            PictureBox11 = new PictureBox();
            Label57 = new Label();
            FontDialog1 = new FontDialog();
            GroupBox73.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).BeginInit();
            GroupBox51.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            TabPage2.SuspendLayout();
            GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            TabPage3.SuspendLayout();
            GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ExtTerminal_PreviewCUR.SuspendLayout();
            ExtTerminal_PreviewCUR2.SuspendLayout();
            TabPage4.SuspendLayout();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            SuspendLayout();
            // 
            // GroupBox73
            // 
            GroupBox73.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox73.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox73.Controls.Add(PictureBox23);
            GroupBox73.Controls.Add(CMD4);
            GroupBox73.Controls.Add(Label137);
            GroupBox73.Location = new Point(435, 135);
            GroupBox73.Margin = new Padding(4, 3, 4, 3);
            GroupBox73.Name = "GroupBox73";
            GroupBox73.Padding = new Padding(1);
            GroupBox73.Size = new Size(455, 278);
            GroupBox73.TabIndex = 117;
            // 
            // PictureBox23
            // 
            PictureBox23.Image = (Image)resources.GetObject("PictureBox23.Image");
            PictureBox23.Location = new Point(6, 6);
            PictureBox23.Name = "PictureBox23";
            PictureBox23.Size = new Size(35, 35);
            PictureBox23.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox23.TabIndex = 4;
            PictureBox23.TabStop = false;
            // 
            // CMD4
            // 
            CMD4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            CMD4.BackColor = Color.Black;
            CMD4.CMD_ColorTable00 = Color.Black;
            CMD4.CMD_ColorTable01 = Color.Empty;
            CMD4.CMD_ColorTable02 = Color.Empty;
            CMD4.CMD_ColorTable03 = Color.Empty;
            CMD4.CMD_ColorTable04 = Color.Empty;
            CMD4.CMD_ColorTable05 = Color.Empty;
            CMD4.CMD_ColorTable06 = Color.Empty;
            CMD4.CMD_ColorTable07 = Color.White;
            CMD4.CMD_ColorTable08 = Color.Empty;
            CMD4.CMD_ColorTable09 = Color.Empty;
            CMD4.CMD_ColorTable10 = Color.Empty;
            CMD4.CMD_ColorTable11 = Color.Empty;
            CMD4.CMD_ColorTable12 = Color.Empty;
            CMD4.CMD_ColorTable13 = Color.Empty;
            CMD4.CMD_ColorTable14 = Color.Empty;
            CMD4.CMD_ColorTable15 = Color.White;
            CMD4.CMD_PopupBackground = 5;
            CMD4.CMD_PopupForeground = 15;
            CMD4.CMD_ScreenColorsBackground = 0;
            CMD4.CMD_ScreenColorsForeground = 7;
            CMD4.CustomTerminal = true;
            CMD4.Font = new Font("Cascadia Mono", 18.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CMD4.Location = new Point(4, 45);
            CMD4.Name = "CMD4";
            CMD4.PowerShell = false;
            CMD4.Raster = false;
            CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
            CMD4.Size = new Size(447, 229);
            CMD4.TabIndex = 90;
            // 
            // Label137
            // 
            Label137.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label137.Location = new Point(47, 6);
            Label137.Name = "Label137";
            Label137.Size = new Size(231, 35);
            Label137.TabIndex = 3;
            Label137.Text = "Preview";
            Label137.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox51
            // 
            GroupBox51.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox51.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox51.Controls.Add(Separator1);
            GroupBox51.Controls.Add(Label4);
            GroupBox51.Controls.Add(Button3);
            GroupBox51.Controls.Add(Button8);
            GroupBox51.Controls.Add(Button4);
            GroupBox51.Controls.Add(Button1);
            GroupBox51.Controls.Add(Button7);
            GroupBox51.Controls.Add(Button6);
            GroupBox51.Controls.Add(ComboBox1);
            GroupBox51.Controls.Add(PictureBox17);
            GroupBox51.Controls.Add(Label102);
            GroupBox51.Location = new Point(11, 12);
            GroupBox51.Name = "GroupBox51";
            GroupBox51.Size = new Size(879, 109);
            GroupBox51.TabIndex = 115;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(6, 72);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(868, 1);
            Separator1.TabIndex = 201;
            Separator1.TabStop = false;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4.Location = new Point(7, 78);
            Label4.Name = "Label4";
            Label4.Size = new Size(72, 25);
            Label4.TabIndex = 116;
            Label4.Text = "Open from:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.ImageAlign = ContentAlignment.MiddleLeft;
            Button3.LineColor = Color.FromArgb(90, 134, 117);
            Button3.Location = new Point(224, 78);
            Button3.Name = "Button3";
            Button3.Size = new Size(133, 25);
            Button3.TabIndex = 115;
            Button3.Text = "Current applied";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button8.BackColor = Color.FromArgb(43, 43, 43);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleLeft;
            Button8.LineColor = Color.FromArgb(113, 122, 131);
            Button8.Location = new Point(84, 78);
            Button8.Name = "Button8";
            Button8.Size = new Size(138, 25);
            Button8.TabIndex = 114;
            Button8.Text = "WinPaletter theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = null;
            Button4.ImageAlign = ContentAlignment.MiddleLeft;
            Button4.LineColor = Color.FromArgb(0, 66, 119);
            Button4.Location = new Point(359, 78);
            Button4.Name = "Button4";
            Button4.Size = new Size(145, 25);
            Button4.TabIndex = 113;
            Button4.Text = "Default Windows";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(43, 43, 43);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(36, 81, 110);
            Button1.Location = new Point(678, 43);
            Button1.Name = "Button1";
            Button1.Size = new Size(64, 24);
            Button1.TabIndex = 102;
            Button1.Text = "New";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(43, 43, 43);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.ImageAlign = ContentAlignment.MiddleLeft;
            Button7.LineColor = Color.FromArgb(36, 81, 110);
            Button7.Location = new Point(811, 43);
            Button7.Name = "Button7";
            Button7.Size = new Size(64, 24);
            Button7.TabIndex = 101;
            Button7.Text = "Select";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = null;
            Button6.ImageAlign = ContentAlignment.MiddleLeft;
            Button6.LineColor = Color.FromArgb(36, 81, 110);
            Button6.Location = new Point(745, 43);
            Button6.Name = "Button6";
            Button6.Size = new Size(64, 24);
            Button6.TabIndex = 89;
            Button6.Text = "Refresh";
            Button6.UseVisualStyleBackColor = false;
            // 
            // ComboBox1
            // 
            ComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox1.BackColor = Color.FromArgb(55, 55, 55);
            ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Location = new Point(36, 43);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(639, 26);
            ComboBox1.TabIndex = 100;
            // 
            // PictureBox17
            // 
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(3, 3);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(24, 24);
            PictureBox17.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox17.TabIndex = 99;
            PictureBox17.TabStop = false;
            // 
            // Label102
            // 
            Label102.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label102.BackColor = Color.Transparent;
            Label102.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label102.ForeColor = Color.Gold;
            Label102.Location = new Point(33, 5);
            Label102.Name = "Label102";
            Label102.Size = new Size(841, 37);
            Label102.TabIndex = 98;
            Label102.Text = "This is a single session modification for another unmentioned terminal (not autom" + @"ated\not modified every time you apply a palette theme\i.e. you modify it manual" + "ly from here).";
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
            Button10.Location = new Point(774, 685);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 124;
            Button10.Text = "Manual apply";
            Button10.UseVisualStyleBackColor = false;
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
            Button2.Location = new Point(688, 685);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 123;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(14, 690);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(668, 24);
            CheckBox1.TabIndex = 125;
            CheckBox1.Text = "Allow non monospace Fonts (causes wrong font renderering in enhanced terminal, wo" + "n't be used in legacy terminal)";
            // 
            // Separator2
            // 
            Separator2.AlternativeLook = false;
            Separator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator2.Location = new Point(11, 128);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(879, 1);
            Separator2.TabIndex = 200;
            Separator2.TabStop = false;
            Separator2.Text = "Separator2";
            // 
            // OpenWPTHDlg
            // 
            OpenWPTHDlg.Filter = "WinPaletter Theme File (*.wpth)|*.wpth";
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.AllowDrop = true;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Controls.Add(TabPage4);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ItemSize = new Size(35, 100);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(11, 131);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(421, 558);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 201;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(GroupBox2);
            TabPage1.Controls.Add(GroupBox1);
            TabPage1.Location = new Point(104, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(313, 550);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Colors";
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(PictureBox15);
            GroupBox2.Controls.Add(Separator3);
            GroupBox2.Controls.Add(ExtTerminal_AccentBackgroundLbl);
            GroupBox2.Controls.Add(ExtTerminal_PopupForegroundLbl);
            GroupBox2.Controls.Add(Label50);
            GroupBox2.Controls.Add(ExtTerminal_AccentForegroundLbl);
            GroupBox2.Controls.Add(ExtTerminal_PopupBackgroundLbl);
            GroupBox2.Controls.Add(ExtTerminal_AccentBackgroundBar);
            GroupBox2.Controls.Add(ExtTerminal_PopupBackgroundBar);
            GroupBox2.Controls.Add(ExtTerminal_AccentForegroundBar);
            GroupBox2.Controls.Add(Label18);
            GroupBox2.Controls.Add(Label17);
            GroupBox2.Controls.Add(Label49);
            GroupBox2.Controls.Add(ExtTerminal_PopupForegroundBar);
            GroupBox2.Controls.Add(Label6);
            GroupBox2.Location = new Point(6, 298);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(301, 245);
            GroupBox2.TabIndex = 102;
            // 
            // PictureBox15
            // 
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(6, 7);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox15.TabIndex = 202;
            PictureBox15.TabStop = false;
            // 
            // Separator3
            // 
            Separator3.AlternativeLook = false;
            Separator3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator3.Location = new Point(9, 136);
            Separator3.Name = "Separator3";
            Separator3.Size = new Size(285, 1);
            Separator3.TabIndex = 101;
            Separator3.TabStop = false;
            // 
            // ExtTerminal_AccentBackgroundLbl
            // 
            ExtTerminal_AccentBackgroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_AccentBackgroundLbl.BackColor = Color.Gray;
            ExtTerminal_AccentBackgroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ExtTerminal_AccentBackgroundLbl.Location = new Point(242, 84);
            ExtTerminal_AccentBackgroundLbl.Name = "ExtTerminal_AccentBackgroundLbl";
            ExtTerminal_AccentBackgroundLbl.Size = new Size(50, 20);
            ExtTerminal_AccentBackgroundLbl.TabIndex = 88;
            ExtTerminal_AccentBackgroundLbl.Text = "0";
            ExtTerminal_AccentBackgroundLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_PopupForegroundLbl
            // 
            ExtTerminal_PopupForegroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_PopupForegroundLbl.BackColor = Color.Gray;
            ExtTerminal_PopupForegroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ExtTerminal_PopupForegroundLbl.Location = new Point(242, 149);
            ExtTerminal_PopupForegroundLbl.Name = "ExtTerminal_PopupForegroundLbl";
            ExtTerminal_PopupForegroundLbl.Size = new Size(50, 20);
            ExtTerminal_PopupForegroundLbl.TabIndex = 87;
            ExtTerminal_PopupForegroundLbl.Text = "0";
            ExtTerminal_PopupForegroundLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label50
            // 
            Label50.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label50.BackColor = Color.Transparent;
            Label50.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label50.Location = new Point(5, 82);
            Label50.Name = "Label50";
            Label50.Size = new Size(231, 22);
            Label50.TabIndex = 85;
            Label50.Text = "Background:";
            Label50.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_AccentForegroundLbl
            // 
            ExtTerminal_AccentForegroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_AccentForegroundLbl.BackColor = Color.Gray;
            ExtTerminal_AccentForegroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ExtTerminal_AccentForegroundLbl.Location = new Point(242, 37);
            ExtTerminal_AccentForegroundLbl.Name = "ExtTerminal_AccentForegroundLbl";
            ExtTerminal_AccentForegroundLbl.Size = new Size(50, 20);
            ExtTerminal_AccentForegroundLbl.TabIndex = 87;
            ExtTerminal_AccentForegroundLbl.Text = "0";
            ExtTerminal_AccentForegroundLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_PopupBackgroundLbl
            // 
            ExtTerminal_PopupBackgroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_PopupBackgroundLbl.BackColor = Color.Gray;
            ExtTerminal_PopupBackgroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ExtTerminal_PopupBackgroundLbl.Location = new Point(242, 196);
            ExtTerminal_PopupBackgroundLbl.Name = "ExtTerminal_PopupBackgroundLbl";
            ExtTerminal_PopupBackgroundLbl.Size = new Size(50, 20);
            ExtTerminal_PopupBackgroundLbl.TabIndex = 88;
            ExtTerminal_PopupBackgroundLbl.Text = "0";
            ExtTerminal_PopupBackgroundLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_AccentBackgroundBar
            // 
            ExtTerminal_AccentBackgroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_AccentBackgroundBar.LargeChange = 10;
            ExtTerminal_AccentBackgroundBar.Location = new Point(8, 107);
            ExtTerminal_AccentBackgroundBar.Maximum = 15;
            ExtTerminal_AccentBackgroundBar.Minimum = 0;
            ExtTerminal_AccentBackgroundBar.Name = "ExtTerminal_AccentBackgroundBar";
            ExtTerminal_AccentBackgroundBar.Size = new Size(289, 19);
            ExtTerminal_AccentBackgroundBar.SmallChange = 1;
            ExtTerminal_AccentBackgroundBar.TabIndex = 86;
            ExtTerminal_AccentBackgroundBar.Value = 0;
            // 
            // ExtTerminal_PopupBackgroundBar
            // 
            ExtTerminal_PopupBackgroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_PopupBackgroundBar.LargeChange = 10;
            ExtTerminal_PopupBackgroundBar.Location = new Point(8, 219);
            ExtTerminal_PopupBackgroundBar.Maximum = 15;
            ExtTerminal_PopupBackgroundBar.Minimum = 0;
            ExtTerminal_PopupBackgroundBar.Name = "ExtTerminal_PopupBackgroundBar";
            ExtTerminal_PopupBackgroundBar.Size = new Size(289, 19);
            ExtTerminal_PopupBackgroundBar.SmallChange = 1;
            ExtTerminal_PopupBackgroundBar.TabIndex = 86;
            ExtTerminal_PopupBackgroundBar.Value = 0;
            // 
            // ExtTerminal_AccentForegroundBar
            // 
            ExtTerminal_AccentForegroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_AccentForegroundBar.LargeChange = 10;
            ExtTerminal_AccentForegroundBar.Location = new Point(8, 60);
            ExtTerminal_AccentForegroundBar.Maximum = 15;
            ExtTerminal_AccentForegroundBar.Minimum = 0;
            ExtTerminal_AccentForegroundBar.Name = "ExtTerminal_AccentForegroundBar";
            ExtTerminal_AccentForegroundBar.Size = new Size(289, 19);
            ExtTerminal_AccentForegroundBar.SmallChange = 1;
            ExtTerminal_AccentForegroundBar.TabIndex = 84;
            ExtTerminal_AccentForegroundBar.Value = 0;
            // 
            // Label18
            // 
            Label18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label18.BackColor = Color.Transparent;
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label18.Location = new Point(5, 194);
            Label18.Name = "Label18";
            Label18.Size = new Size(231, 22);
            Label18.TabIndex = 85;
            Label18.Text = "Pop-up background:";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label17
            // 
            Label17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label17.BackColor = Color.Transparent;
            Label17.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label17.Location = new Point(5, 147);
            Label17.Name = "Label17";
            Label17.Size = new Size(231, 22);
            Label17.TabIndex = 83;
            Label17.Text = "Pop-up foreground:";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label49
            // 
            Label49.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label49.BackColor = Color.Transparent;
            Label49.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label49.Location = new Point(5, 35);
            Label49.Name = "Label49";
            Label49.Size = new Size(231, 22);
            Label49.TabIndex = 83;
            Label49.Text = "Foreground:";
            Label49.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_PopupForegroundBar
            // 
            ExtTerminal_PopupForegroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_PopupForegroundBar.LargeChange = 10;
            ExtTerminal_PopupForegroundBar.Location = new Point(8, 172);
            ExtTerminal_PopupForegroundBar.Maximum = 15;
            ExtTerminal_PopupForegroundBar.Minimum = 0;
            ExtTerminal_PopupForegroundBar.Name = "ExtTerminal_PopupForegroundBar";
            ExtTerminal_PopupForegroundBar.Size = new Size(289, 19);
            ExtTerminal_PopupForegroundBar.SmallChange = 1;
            ExtTerminal_PopupForegroundBar.TabIndex = 84;
            ExtTerminal_PopupForegroundBar.Value = 0;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(36, 7);
            Label6.Name = "Label6";
            Label6.Size = new Size(256, 24);
            Label6.TabIndex = 84;
            Label6.Text = "Accents:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(PictureBox14);
            GroupBox1.Controls.Add(Label31);
            GroupBox1.Controls.Add(Label19);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable00);
            GroupBox1.Controls.Add(Label7);
            GroupBox1.Controls.Add(Label32);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable01);
            GroupBox1.Controls.Add(Label20);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable02);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable03);
            GroupBox1.Controls.Add(Label33);
            GroupBox1.Controls.Add(Label21);
            GroupBox1.Controls.Add(Label22);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable04);
            GroupBox1.Controls.Add(Label26);
            GroupBox1.Controls.Add(Label34);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable05);
            GroupBox1.Controls.Add(Label25);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable06);
            GroupBox1.Controls.Add(Label24);
            GroupBox1.Controls.Add(Label27);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable15);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable07);
            GroupBox1.Controls.Add(Label23);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable08);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable14);
            GroupBox1.Controls.Add(Label28);
            GroupBox1.Controls.Add(Label30);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable09);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable13);
            GroupBox1.Controls.Add(Label29);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable12);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable10);
            GroupBox1.Controls.Add(ExtTerminal_ColorTable11);
            GroupBox1.Location = new Point(6, 6);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(301, 286);
            GroupBox1.TabIndex = 87;
            // 
            // PictureBox14
            // 
            PictureBox14.Image = (Image)resources.GetObject("PictureBox14.Image");
            PictureBox14.Location = new Point(6, 6);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox14.TabIndex = 201;
            PictureBox14.TabStop = false;
            // 
            // Label31
            // 
            Label31.BackColor = Color.Transparent;
            Label31.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label31.Location = new Point(144, 253);
            Label31.Name = "Label31";
            Label31.Size = new Size(38, 25);
            Label31.TabIndex = 99;
            Label31.Text = "15 (F)";
            Label31.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label19
            // 
            Label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label19.BackColor = Color.Transparent;
            Label19.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label19.Location = new Point(36, 6);
            Label19.Name = "Label19";
            Label19.Size = new Size(261, 24);
            Label19.TabIndex = 84;
            Label19.Text = "Tables:";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_ColorTable00
            // 
            ExtTerminal_ColorTable00.AllowDrop = true;
            ExtTerminal_ColorTable00.BackColor = Color.FromArgb(12, 12, 12);
            ExtTerminal_ColorTable00.DefaultColor = Color.FromArgb(12, 12, 12);
            ExtTerminal_ColorTable00.DontShowInfo = false;
            ExtTerminal_ColorTable00.Location = new Point(28, 36);
            ExtTerminal_ColorTable00.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable00.Name = "ExtTerminal_ColorTable00";
            ExtTerminal_ColorTable00.Size = new Size(105, 25);
            ExtTerminal_ColorTable00.TabIndex = 3;
            // 
            // Label7
            // 
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.Location = new Point(7, 36);
            Label7.Name = "Label7";
            Label7.Size = new Size(14, 25);
            Label7.TabIndex = 4;
            Label7.Text = "0";
            Label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label32
            // 
            Label32.BackColor = Color.Transparent;
            Label32.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label32.Location = new Point(144, 222);
            Label32.Name = "Label32";
            Label32.Size = new Size(38, 25);
            Label32.TabIndex = 98;
            Label32.Text = "14 (E)";
            Label32.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable01
            // 
            ExtTerminal_ColorTable01.AllowDrop = true;
            ExtTerminal_ColorTable01.BackColor = Color.FromArgb(0, 55, 218);
            ExtTerminal_ColorTable01.DefaultColor = Color.FromArgb(0, 55, 218);
            ExtTerminal_ColorTable01.DontShowInfo = false;
            ExtTerminal_ColorTable01.Location = new Point(28, 67);
            ExtTerminal_ColorTable01.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable01.Name = "ExtTerminal_ColorTable01";
            ExtTerminal_ColorTable01.Size = new Size(105, 25);
            ExtTerminal_ColorTable01.TabIndex = 5;
            // 
            // Label20
            // 
            Label20.BackColor = Color.Transparent;
            Label20.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label20.Location = new Point(7, 67);
            Label20.Name = "Label20";
            Label20.Size = new Size(14, 25);
            Label20.TabIndex = 85;
            Label20.Text = "1";
            Label20.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable02
            // 
            ExtTerminal_ColorTable02.AllowDrop = true;
            ExtTerminal_ColorTable02.BackColor = Color.FromArgb(19, 161, 14);
            ExtTerminal_ColorTable02.DefaultColor = Color.FromArgb(19, 161, 14);
            ExtTerminal_ColorTable02.DontShowInfo = false;
            ExtTerminal_ColorTable02.Location = new Point(28, 98);
            ExtTerminal_ColorTable02.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable02.Name = "ExtTerminal_ColorTable02";
            ExtTerminal_ColorTable02.Size = new Size(105, 25);
            ExtTerminal_ColorTable02.TabIndex = 7;
            // 
            // ExtTerminal_ColorTable03
            // 
            ExtTerminal_ColorTable03.AllowDrop = true;
            ExtTerminal_ColorTable03.BackColor = Color.FromArgb(58, 150, 221);
            ExtTerminal_ColorTable03.DefaultColor = Color.FromArgb(58, 150, 221);
            ExtTerminal_ColorTable03.DontShowInfo = false;
            ExtTerminal_ColorTable03.Location = new Point(28, 129);
            ExtTerminal_ColorTable03.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable03.Name = "ExtTerminal_ColorTable03";
            ExtTerminal_ColorTable03.Size = new Size(105, 25);
            ExtTerminal_ColorTable03.TabIndex = 9;
            // 
            // Label33
            // 
            Label33.BackColor = Color.Transparent;
            Label33.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label33.Location = new Point(144, 192);
            Label33.Name = "Label33";
            Label33.Size = new Size(38, 25);
            Label33.TabIndex = 97;
            Label33.Text = "13 (D)";
            Label33.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label21
            // 
            Label21.BackColor = Color.Transparent;
            Label21.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label21.Location = new Point(7, 98);
            Label21.Name = "Label21";
            Label21.Size = new Size(14, 25);
            Label21.TabIndex = 86;
            Label21.Text = "2";
            Label21.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label22
            // 
            Label22.BackColor = Color.Transparent;
            Label22.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label22.Location = new Point(7, 129);
            Label22.Name = "Label22";
            Label22.Size = new Size(14, 25);
            Label22.TabIndex = 87;
            Label22.Text = "3";
            Label22.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable04
            // 
            ExtTerminal_ColorTable04.AllowDrop = true;
            ExtTerminal_ColorTable04.BackColor = Color.FromArgb(197, 15, 31);
            ExtTerminal_ColorTable04.DefaultColor = Color.FromArgb(197, 15, 31);
            ExtTerminal_ColorTable04.DontShowInfo = false;
            ExtTerminal_ColorTable04.Location = new Point(28, 160);
            ExtTerminal_ColorTable04.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable04.Name = "ExtTerminal_ColorTable04";
            ExtTerminal_ColorTable04.Size = new Size(105, 25);
            ExtTerminal_ColorTable04.TabIndex = 11;
            // 
            // Label26
            // 
            Label26.BackColor = Color.Transparent;
            Label26.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label26.Location = new Point(7, 160);
            Label26.Name = "Label26";
            Label26.Size = new Size(14, 25);
            Label26.TabIndex = 88;
            Label26.Text = "4";
            Label26.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label34
            // 
            Label34.BackColor = Color.Transparent;
            Label34.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label34.Location = new Point(144, 160);
            Label34.Name = "Label34";
            Label34.Size = new Size(38, 25);
            Label34.TabIndex = 96;
            Label34.Text = "12 (C)";
            Label34.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable05
            // 
            ExtTerminal_ColorTable05.AllowDrop = true;
            ExtTerminal_ColorTable05.BackColor = Color.FromArgb(136, 23, 152);
            ExtTerminal_ColorTable05.DefaultColor = Color.FromArgb(136, 23, 152);
            ExtTerminal_ColorTable05.DontShowInfo = false;
            ExtTerminal_ColorTable05.Location = new Point(28, 191);
            ExtTerminal_ColorTable05.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable05.Name = "ExtTerminal_ColorTable05";
            ExtTerminal_ColorTable05.Size = new Size(105, 25);
            ExtTerminal_ColorTable05.TabIndex = 13;
            // 
            // Label25
            // 
            Label25.BackColor = Color.Transparent;
            Label25.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label25.Location = new Point(7, 192);
            Label25.Name = "Label25";
            Label25.Size = new Size(14, 25);
            Label25.TabIndex = 89;
            Label25.Text = "5";
            Label25.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable06
            // 
            ExtTerminal_ColorTable06.AllowDrop = true;
            ExtTerminal_ColorTable06.BackColor = Color.FromArgb(193, 156, 0);
            ExtTerminal_ColorTable06.DefaultColor = Color.FromArgb(193, 156, 0);
            ExtTerminal_ColorTable06.DontShowInfo = false;
            ExtTerminal_ColorTable06.Location = new Point(28, 222);
            ExtTerminal_ColorTable06.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable06.Name = "ExtTerminal_ColorTable06";
            ExtTerminal_ColorTable06.Size = new Size(105, 25);
            ExtTerminal_ColorTable06.TabIndex = 15;
            // 
            // Label24
            // 
            Label24.BackColor = Color.Transparent;
            Label24.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label24.Location = new Point(7, 222);
            Label24.Name = "Label24";
            Label24.Size = new Size(14, 25);
            Label24.TabIndex = 90;
            Label24.Text = "6";
            Label24.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label27
            // 
            Label27.BackColor = Color.Transparent;
            Label27.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label27.Location = new Point(144, 129);
            Label27.Name = "Label27";
            Label27.Size = new Size(38, 25);
            Label27.TabIndex = 95;
            Label27.Text = "11 (B)";
            Label27.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable15
            // 
            ExtTerminal_ColorTable15.AllowDrop = true;
            ExtTerminal_ColorTable15.BackColor = Color.FromArgb(242, 242, 242);
            ExtTerminal_ColorTable15.DefaultColor = Color.FromArgb(242, 242, 242);
            ExtTerminal_ColorTable15.DontShowInfo = false;
            ExtTerminal_ColorTable15.Location = new Point(189, 253);
            ExtTerminal_ColorTable15.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable15.Name = "ExtTerminal_ColorTable15";
            ExtTerminal_ColorTable15.Size = new Size(105, 25);
            ExtTerminal_ColorTable15.TabIndex = 33;
            // 
            // ExtTerminal_ColorTable07
            // 
            ExtTerminal_ColorTable07.AllowDrop = true;
            ExtTerminal_ColorTable07.BackColor = Color.FromArgb(204, 204, 204);
            ExtTerminal_ColorTable07.DefaultColor = Color.FromArgb(204, 204, 204);
            ExtTerminal_ColorTable07.DontShowInfo = false;
            ExtTerminal_ColorTable07.Location = new Point(28, 253);
            ExtTerminal_ColorTable07.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable07.Name = "ExtTerminal_ColorTable07";
            ExtTerminal_ColorTable07.Size = new Size(105, 25);
            ExtTerminal_ColorTable07.TabIndex = 17;
            // 
            // Label23
            // 
            Label23.BackColor = Color.Transparent;
            Label23.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label23.Location = new Point(7, 253);
            Label23.Name = "Label23";
            Label23.Size = new Size(14, 25);
            Label23.TabIndex = 91;
            Label23.Text = "7";
            Label23.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable08
            // 
            ExtTerminal_ColorTable08.AllowDrop = true;
            ExtTerminal_ColorTable08.BackColor = Color.FromArgb(118, 118, 118);
            ExtTerminal_ColorTable08.DefaultColor = Color.FromArgb(118, 118, 118);
            ExtTerminal_ColorTable08.DontShowInfo = false;
            ExtTerminal_ColorTable08.Location = new Point(189, 36);
            ExtTerminal_ColorTable08.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable08.Name = "ExtTerminal_ColorTable08";
            ExtTerminal_ColorTable08.Size = new Size(105, 25);
            ExtTerminal_ColorTable08.TabIndex = 19;
            // 
            // ExtTerminal_ColorTable14
            // 
            ExtTerminal_ColorTable14.AllowDrop = true;
            ExtTerminal_ColorTable14.BackColor = Color.FromArgb(249, 241, 165);
            ExtTerminal_ColorTable14.DefaultColor = Color.FromArgb(249, 241, 165);
            ExtTerminal_ColorTable14.DontShowInfo = false;
            ExtTerminal_ColorTable14.Location = new Point(189, 222);
            ExtTerminal_ColorTable14.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable14.Name = "ExtTerminal_ColorTable14";
            ExtTerminal_ColorTable14.Size = new Size(105, 25);
            ExtTerminal_ColorTable14.TabIndex = 31;
            // 
            // Label28
            // 
            Label28.BackColor = Color.Transparent;
            Label28.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label28.Location = new Point(144, 98);
            Label28.Name = "Label28";
            Label28.Size = new Size(38, 25);
            Label28.TabIndex = 94;
            Label28.Text = "10 (A)";
            Label28.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Label30
            // 
            Label30.BackColor = Color.Transparent;
            Label30.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label30.Location = new Point(144, 36);
            Label30.Name = "Label30";
            Label30.Size = new Size(38, 25);
            Label30.TabIndex = 92;
            Label30.Text = "8";
            Label30.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable09
            // 
            ExtTerminal_ColorTable09.AllowDrop = true;
            ExtTerminal_ColorTable09.BackColor = Color.FromArgb(59, 120, 255);
            ExtTerminal_ColorTable09.DefaultColor = Color.FromArgb(59, 120, 255);
            ExtTerminal_ColorTable09.DontShowInfo = false;
            ExtTerminal_ColorTable09.Location = new Point(189, 67);
            ExtTerminal_ColorTable09.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable09.Name = "ExtTerminal_ColorTable09";
            ExtTerminal_ColorTable09.Size = new Size(105, 25);
            ExtTerminal_ColorTable09.TabIndex = 21;
            // 
            // ExtTerminal_ColorTable13
            // 
            ExtTerminal_ColorTable13.AllowDrop = true;
            ExtTerminal_ColorTable13.BackColor = Color.FromArgb(180, 0, 158);
            ExtTerminal_ColorTable13.DefaultColor = Color.FromArgb(180, 0, 158);
            ExtTerminal_ColorTable13.DontShowInfo = false;
            ExtTerminal_ColorTable13.Location = new Point(189, 191);
            ExtTerminal_ColorTable13.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable13.Name = "ExtTerminal_ColorTable13";
            ExtTerminal_ColorTable13.Size = new Size(105, 25);
            ExtTerminal_ColorTable13.TabIndex = 29;
            // 
            // Label29
            // 
            Label29.BackColor = Color.Transparent;
            Label29.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label29.Location = new Point(144, 67);
            Label29.Name = "Label29";
            Label29.Size = new Size(38, 25);
            Label29.TabIndex = 93;
            Label29.Text = "9";
            Label29.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ExtTerminal_ColorTable12
            // 
            ExtTerminal_ColorTable12.AllowDrop = true;
            ExtTerminal_ColorTable12.BackColor = Color.FromArgb(231, 72, 86);
            ExtTerminal_ColorTable12.DefaultColor = Color.FromArgb(231, 72, 86);
            ExtTerminal_ColorTable12.DontShowInfo = false;
            ExtTerminal_ColorTable12.Location = new Point(189, 160);
            ExtTerminal_ColorTable12.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable12.Name = "ExtTerminal_ColorTable12";
            ExtTerminal_ColorTable12.Size = new Size(105, 25);
            ExtTerminal_ColorTable12.TabIndex = 27;
            // 
            // ExtTerminal_ColorTable10
            // 
            ExtTerminal_ColorTable10.AllowDrop = true;
            ExtTerminal_ColorTable10.BackColor = Color.FromArgb(22, 198, 12);
            ExtTerminal_ColorTable10.DefaultColor = Color.FromArgb(22, 198, 12);
            ExtTerminal_ColorTable10.DontShowInfo = false;
            ExtTerminal_ColorTable10.Location = new Point(189, 98);
            ExtTerminal_ColorTable10.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable10.Name = "ExtTerminal_ColorTable10";
            ExtTerminal_ColorTable10.Size = new Size(105, 25);
            ExtTerminal_ColorTable10.TabIndex = 23;
            // 
            // ExtTerminal_ColorTable11
            // 
            ExtTerminal_ColorTable11.AllowDrop = true;
            ExtTerminal_ColorTable11.BackColor = Color.FromArgb(97, 214, 214);
            ExtTerminal_ColorTable11.DefaultColor = Color.FromArgb(97, 214, 214);
            ExtTerminal_ColorTable11.DontShowInfo = false;
            ExtTerminal_ColorTable11.Location = new Point(189, 129);
            ExtTerminal_ColorTable11.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_ColorTable11.Name = "ExtTerminal_ColorTable11";
            ExtTerminal_ColorTable11.Size = new Size(105, 25);
            ExtTerminal_ColorTable11.TabIndex = 25;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(GroupBox4);
            TabPage2.Location = new Point(104, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(313, 550);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Fonts";
            // 
            // GroupBox4
            // 
            GroupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox4.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox4.Controls.Add(FontName);
            GroupBox4.Controls.Add(Button5);
            GroupBox4.Controls.Add(ExtTerminal_FontSizeVal);
            GroupBox4.Controls.Add(ExtTerminal_RasterToggle);
            GroupBox4.Controls.Add(PictureBox1);
            GroupBox4.Controls.Add(Label58);
            GroupBox4.Controls.Add(ExtTerminal_FontSizeBar);
            GroupBox4.Controls.Add(ExtTerminal_FontWeightBox);
            GroupBox4.Controls.Add(Label61);
            GroupBox4.Controls.Add(PictureBox6);
            GroupBox4.Controls.Add(PictureBox3);
            GroupBox4.Controls.Add(Label35);
            GroupBox4.Controls.Add(Button25);
            GroupBox4.Controls.Add(PictureBox4);
            GroupBox4.Controls.Add(Label59);
            GroupBox4.Controls.Add(RasterList);
            GroupBox4.Location = new Point(6, 6);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Size = new Size(301, 128);
            GroupBox4.TabIndex = 98;
            // 
            // FontName
            // 
            FontName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FontName.BackColor = Color.Transparent;
            FontName.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FontName.Location = new Point(96, 6);
            FontName.Name = "FontName";
            FontName.Size = new Size(172, 24);
            FontName.TabIndex = 136;
            FontName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(43, 43, 43);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Button5.ForeColor = Color.White;
            Button5.Image = null;
            Button5.LineColor = Color.FromArgb(199, 49, 61);
            Button5.Location = new Point(274, 6);
            Button5.Name = "Button5";
            Button5.Size = new Size(21, 24);
            Button5.TabIndex = 135;
            Button5.Text = "...";
            Button5.UseVisualStyleBackColor = false;
            // 
            // ExtTerminal_FontSizeVal
            // 
            ExtTerminal_FontSizeVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_FontSizeVal.BackColor = Color.FromArgb(43, 43, 43);
            ExtTerminal_FontSizeVal.DrawOnGlass = false;
            ExtTerminal_FontSizeVal.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_FontSizeVal.ForeColor = Color.White;
            ExtTerminal_FontSizeVal.Image = null;
            ExtTerminal_FontSizeVal.LineColor = Color.FromArgb(0, 81, 210);
            ExtTerminal_FontSizeVal.Location = new Point(261, 66);
            ExtTerminal_FontSizeVal.Name = "ExtTerminal_FontSizeVal";
            ExtTerminal_FontSizeVal.Size = new Size(34, 24);
            ExtTerminal_FontSizeVal.TabIndex = 134;
            ExtTerminal_FontSizeVal.UseVisualStyleBackColor = false;
            // 
            // ExtTerminal_RasterToggle
            // 
            ExtTerminal_RasterToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_RasterToggle.BackColor = Color.FromArgb(43, 43, 43);
            ExtTerminal_RasterToggle.Checked = false;
            ExtTerminal_RasterToggle.DarkLight_Toggler = false;
            ExtTerminal_RasterToggle.Location = new Point(255, 98);
            ExtTerminal_RasterToggle.Name = "ExtTerminal_RasterToggle";
            ExtTerminal_RasterToggle.Size = new Size(40, 20);
            ExtTerminal_RasterToggle.TabIndex = 95;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 6);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 99;
            PictureBox1.TabStop = false;
            // 
            // Label58
            // 
            Label58.BackColor = Color.Transparent;
            Label58.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label58.Location = new Point(36, 96);
            Label58.Name = "Label58";
            Label58.Size = new Size(108, 24);
            Label58.TabIndex = 94;
            Label58.Text = "Raster font ?";
            Label58.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_FontSizeBar
            // 
            ExtTerminal_FontSizeBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_FontSizeBar.LargeChange = 10;
            ExtTerminal_FontSizeBar.Location = new Point(97, 69);
            ExtTerminal_FontSizeBar.Maximum = 48;
            ExtTerminal_FontSizeBar.Minimum = 5;
            ExtTerminal_FontSizeBar.Name = "ExtTerminal_FontSizeBar";
            ExtTerminal_FontSizeBar.Size = new Size(158, 19);
            ExtTerminal_FontSizeBar.SmallChange = 1;
            ExtTerminal_FontSizeBar.TabIndex = 101;
            ExtTerminal_FontSizeBar.Value = 5;
            // 
            // ExtTerminal_FontWeightBox
            // 
            ExtTerminal_FontWeightBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_FontWeightBox.BackColor = Color.FromArgb(43, 43, 43);
            ExtTerminal_FontWeightBox.DrawMode = DrawMode.OwnerDrawFixed;
            ExtTerminal_FontWeightBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ExtTerminal_FontWeightBox.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_FontWeightBox.ForeColor = Color.White;
            ExtTerminal_FontWeightBox.FormattingEnabled = true;
            ExtTerminal_FontWeightBox.ItemHeight = 20;
            ExtTerminal_FontWeightBox.Items.AddRange(new object[] { "Don't Care", "Thin", "Extra Light", "Light", "Normal", "Medium", "Semi Bold", "Bold", "Extra Bold", "Heavy" });
            ExtTerminal_FontWeightBox.Location = new Point(96, 35);
            ExtTerminal_FontWeightBox.Name = "ExtTerminal_FontWeightBox";
            ExtTerminal_FontWeightBox.Size = new Size(172, 26);
            ExtTerminal_FontWeightBox.TabIndex = 99;
            // 
            // Label61
            // 
            Label61.BackColor = Color.Transparent;
            Label61.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label61.Location = new Point(36, 36);
            Label61.Name = "Label61";
            Label61.Size = new Size(54, 24);
            Label61.TabIndex = 97;
            Label61.Text = "Weight:";
            Label61.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(6, 96);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 102;
            PictureBox6.TabStop = false;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(6, 36);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 100;
            PictureBox3.TabStop = false;
            // 
            // Label35
            // 
            Label35.BackColor = Color.Transparent;
            Label35.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label35.Location = new Point(36, 66);
            Label35.Name = "Label35";
            Label35.Size = new Size(54, 24);
            Label35.TabIndex = 103;
            Label35.Text = "Size:";
            Label35.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button25
            // 
            Button25.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button25.BackColor = Color.FromArgb(43, 43, 43);
            Button25.DrawOnGlass = false;
            Button25.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Button25.ForeColor = Color.White;
            Button25.Image = null;
            Button25.LineColor = Color.FromArgb(199, 49, 61);
            Button25.Location = new Point(274, 36);
            Button25.Name = "Button25";
            Button25.Size = new Size(21, 24);
            Button25.TabIndex = 105;
            Button25.Text = "?";
            Button25.UseVisualStyleBackColor = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(6, 66);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 101;
            PictureBox4.TabStop = false;
            // 
            // Label59
            // 
            Label59.BackColor = Color.Transparent;
            Label59.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label59.Location = new Point(36, 6);
            Label59.Name = "Label59";
            Label59.Size = new Size(54, 24);
            Label59.TabIndex = 84;
            Label59.Text = "Font:";
            Label59.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RasterList
            // 
            RasterList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RasterList.BackColor = Color.FromArgb(43, 43, 43);
            RasterList.DrawMode = DrawMode.OwnerDrawVariable;
            RasterList.DropDownStyle = ComboBoxStyle.DropDownList;
            RasterList.Font = new Font("Segoe UI", 9.0f);
            RasterList.ForeColor = Color.White;
            RasterList.FormattingEnabled = true;
            RasterList.ItemHeight = 20;
            RasterList.Items.AddRange(new object[] { "4x6", "6x8", "6x9", "8x8", "8x9", "16x8", "5x12", "7x12", "8x12", "16x12", "12x16", "10x18" });
            RasterList.Location = new Point(96, 65);
            RasterList.Name = "RasterList";
            RasterList.Size = new Size(199, 26);
            RasterList.TabIndex = 104;
            RasterList.Visible = false;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(Label3);
            TabPage3.Controls.Add(GroupBox34);
            TabPage3.Location = new Point(104, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(313, 550);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Cursor";
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.ForeColor = Color.DarkOrange;
            Label3.Location = new Point(6, 106);
            Label3.Name = "Label3";
            Label3.Size = new Size(301, 48);
            Label3.TabIndex = 112;
            Label3.Text = "Cursor color and style are for Windows 10 1909 and later";
            Label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GroupBox34
            // 
            GroupBox34.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox34.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox34.Controls.Add(PictureBox8);
            GroupBox34.Controls.Add(ExtTerminal_PreviewCUR_Val);
            GroupBox34.Controls.Add(PictureBox7);
            GroupBox34.Controls.Add(ExtTerminal_CursorStyle);
            GroupBox34.Controls.Add(Label60);
            GroupBox34.Controls.Add(ExtTerminal_CursorSizeBar);
            GroupBox34.Controls.Add(Label1);
            GroupBox34.Controls.Add(PictureBox9);
            GroupBox34.Controls.Add(Label2);
            GroupBox34.Controls.Add(ExtTerminal_PreviewCUR);
            GroupBox34.Controls.Add(ExtTerminal_CursorColor);
            GroupBox34.Location = new Point(6, 6);
            GroupBox34.Name = "GroupBox34";
            GroupBox34.Size = new Size(301, 97);
            GroupBox34.TabIndex = 99;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(6, 35);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 136;
            PictureBox8.TabStop = false;
            // 
            // ExtTerminal_PreviewCUR_Val
            // 
            ExtTerminal_PreviewCUR_Val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_PreviewCUR_Val.BackColor = Color.FromArgb(43, 43, 43);
            ExtTerminal_PreviewCUR_Val.DrawOnGlass = false;
            ExtTerminal_PreviewCUR_Val.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_PreviewCUR_Val.ForeColor = Color.White;
            ExtTerminal_PreviewCUR_Val.Image = null;
            ExtTerminal_PreviewCUR_Val.LineColor = Color.FromArgb(0, 81, 210);
            ExtTerminal_PreviewCUR_Val.Location = new Point(262, 66);
            ExtTerminal_PreviewCUR_Val.Name = "ExtTerminal_PreviewCUR_Val";
            ExtTerminal_PreviewCUR_Val.Size = new Size(34, 24);
            ExtTerminal_PreviewCUR_Val.TabIndex = 135;
            ExtTerminal_PreviewCUR_Val.UseVisualStyleBackColor = false;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(6, 6);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 103;
            PictureBox7.TabStop = false;
            // 
            // ExtTerminal_CursorStyle
            // 
            ExtTerminal_CursorStyle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_CursorStyle.BackColor = Color.FromArgb(43, 43, 43);
            ExtTerminal_CursorStyle.DrawMode = DrawMode.OwnerDrawFixed;
            ExtTerminal_CursorStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            ExtTerminal_CursorStyle.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_CursorStyle.ForeColor = Color.White;
            ExtTerminal_CursorStyle.FormattingEnabled = true;
            ExtTerminal_CursorStyle.ItemHeight = 20;
            ExtTerminal_CursorStyle.Items.AddRange(new object[] { "Default", "Legacy", "Underscore", "Empty Box", "Vertical Bar", "Solid Box" });
            ExtTerminal_CursorStyle.Location = new Point(97, 35);
            ExtTerminal_CursorStyle.Name = "ExtTerminal_CursorStyle";
            ExtTerminal_CursorStyle.Size = new Size(199, 26);
            ExtTerminal_CursorStyle.TabIndex = 110;
            // 
            // Label60
            // 
            Label60.BackColor = Color.Transparent;
            Label60.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label60.Location = new Point(36, 66);
            Label60.Name = "Label60";
            Label60.Size = new Size(54, 24);
            Label60.TabIndex = 111;
            Label60.Text = "Size:";
            Label60.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_CursorSizeBar
            // 
            ExtTerminal_CursorSizeBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_CursorSizeBar.LargeChange = 1;
            ExtTerminal_CursorSizeBar.Location = new Point(97, 69);
            ExtTerminal_CursorSizeBar.Maximum = 100;
            ExtTerminal_CursorSizeBar.Minimum = 20;
            ExtTerminal_CursorSizeBar.Name = "ExtTerminal_CursorSizeBar";
            ExtTerminal_CursorSizeBar.Size = new Size(159, 19);
            ExtTerminal_CursorSizeBar.SmallChange = 1;
            ExtTerminal_CursorSizeBar.TabIndex = 102;
            ExtTerminal_CursorSizeBar.Value = 20;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(36, 6);
            Label1.Name = "Label1";
            Label1.Size = new Size(54, 24);
            Label1.TabIndex = 108;
            Label1.Text = "Color:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(6, 66);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 105;
            PictureBox9.TabStop = false;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(36, 36);
            Label2.Name = "Label2";
            Label2.Size = new Size(54, 24);
            Label2.TabIndex = 109;
            Label2.Text = "Style:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ExtTerminal_PreviewCUR
            // 
            ExtTerminal_PreviewCUR.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_PreviewCUR.BackColor = Color.Black;
            ExtTerminal_PreviewCUR.Controls.Add(ExtTerminal_PreviewCUR2);
            ExtTerminal_PreviewCUR.Location = new Point(247, 6);
            ExtTerminal_PreviewCUR.Name = "ExtTerminal_PreviewCUR";
            ExtTerminal_PreviewCUR.Size = new Size(49, 24);
            ExtTerminal_PreviewCUR.TabIndex = 103;
            // 
            // ExtTerminal_PreviewCUR2
            // 
            ExtTerminal_PreviewCUR2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ExtTerminal_PreviewCUR2.BackColor = Color.White;
            ExtTerminal_PreviewCUR2.Controls.Add(ExtTerminal_PreviewCursorInner);
            ExtTerminal_PreviewCUR2.Location = new Point(3, 16);
            ExtTerminal_PreviewCUR2.Name = "ExtTerminal_PreviewCUR2";
            ExtTerminal_PreviewCUR2.Padding = new Padding(1);
            ExtTerminal_PreviewCUR2.Size = new Size(8, 5);
            ExtTerminal_PreviewCUR2.TabIndex = 104;
            // 
            // ExtTerminal_PreviewCursorInner
            // 
            ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent;
            ExtTerminal_PreviewCursorInner.Dock = DockStyle.Fill;
            ExtTerminal_PreviewCursorInner.Location = new Point(1, 1);
            ExtTerminal_PreviewCursorInner.Name = "ExtTerminal_PreviewCursorInner";
            ExtTerminal_PreviewCursorInner.Size = new Size(6, 3);
            ExtTerminal_PreviewCursorInner.TabIndex = 106;
            // 
            // ExtTerminal_CursorColor
            // 
            ExtTerminal_CursorColor.AllowDrop = true;
            ExtTerminal_CursorColor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_CursorColor.BackColor = Color.White;
            ExtTerminal_CursorColor.DefaultColor = Color.White;
            ExtTerminal_CursorColor.DontShowInfo = false;
            ExtTerminal_CursorColor.Location = new Point(97, 6);
            ExtTerminal_CursorColor.Margin = new Padding(4, 3, 4, 3);
            ExtTerminal_CursorColor.Name = "ExtTerminal_CursorColor";
            ExtTerminal_CursorColor.Size = new Size(143, 24);
            ExtTerminal_CursorColor.TabIndex = 107;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(Label5);
            TabPage4.Controls.Add(GroupBox12);
            TabPage4.Location = new Point(104, 4);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(313, 550);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "Tweaks";
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.BackColor = Color.Transparent;
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5.ForeColor = Color.DarkOrange;
            Label5.Location = new Point(6, 159);
            Label5.Name = "Label5";
            Label5.Size = new Size(301, 48);
            Label5.TabIndex = 111;
            Label5.Text = "Tweaks are for Windows 10 1909 and later";
            Label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(ExtTerminal_OpacityVal);
            GroupBox12.Controls.Add(PictureBox10);
            GroupBox12.Controls.Add(PictureBox13);
            GroupBox12.Controls.Add(ExtTerminal_LineSelection);
            GroupBox12.Controls.Add(ExtTerminal_EnhancedTerminal);
            GroupBox12.Controls.Add(PictureBox12);
            GroupBox12.Controls.Add(ExtTerminal_TerminalScrolling);
            GroupBox12.Controls.Add(ExtTerminal_OpacityBar);
            GroupBox12.Controls.Add(PictureBox11);
            GroupBox12.Controls.Add(Label57);
            GroupBox12.Location = new Point(6, 6);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(301, 150);
            GroupBox12.TabIndex = 100;
            // 
            // ExtTerminal_OpacityVal
            // 
            ExtTerminal_OpacityVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExtTerminal_OpacityVal.BackColor = Color.FromArgb(43, 43, 43);
            ExtTerminal_OpacityVal.DrawOnGlass = false;
            ExtTerminal_OpacityVal.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_OpacityVal.ForeColor = Color.White;
            ExtTerminal_OpacityVal.Image = null;
            ExtTerminal_OpacityVal.LineColor = Color.FromArgb(0, 81, 210);
            ExtTerminal_OpacityVal.Location = new Point(261, 120);
            ExtTerminal_OpacityVal.Name = "ExtTerminal_OpacityVal";
            ExtTerminal_OpacityVal.Size = new Size(34, 24);
            ExtTerminal_OpacityVal.TabIndex = 136;
            ExtTerminal_OpacityVal.UseVisualStyleBackColor = false;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(6, 6);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 106;
            PictureBox10.TabStop = false;
            // 
            // PictureBox13
            // 
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(6, 96);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(24, 24);
            PictureBox13.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox13.TabIndex = 126;
            PictureBox13.TabStop = false;
            // 
            // ExtTerminal_LineSelection
            // 
            ExtTerminal_LineSelection.BackColor = Color.FromArgb(34, 34, 34);
            ExtTerminal_LineSelection.Checked = false;
            ExtTerminal_LineSelection.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_LineSelection.ForeColor = Color.White;
            ExtTerminal_LineSelection.Location = new Point(36, 36);
            ExtTerminal_LineSelection.Name = "ExtTerminal_LineSelection";
            ExtTerminal_LineSelection.Size = new Size(155, 24);
            ExtTerminal_LineSelection.TabIndex = 122;
            ExtTerminal_LineSelection.Text = "Line selection";
            // 
            // ExtTerminal_EnhancedTerminal
            // 
            ExtTerminal_EnhancedTerminal.BackColor = Color.FromArgb(34, 34, 34);
            ExtTerminal_EnhancedTerminal.Checked = false;
            ExtTerminal_EnhancedTerminal.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_EnhancedTerminal.ForeColor = Color.White;
            ExtTerminal_EnhancedTerminal.Location = new Point(36, 6);
            ExtTerminal_EnhancedTerminal.Name = "ExtTerminal_EnhancedTerminal";
            ExtTerminal_EnhancedTerminal.Size = new Size(155, 24);
            ExtTerminal_EnhancedTerminal.TabIndex = 118;
            ExtTerminal_EnhancedTerminal.Text = "Enhanced terminal";
            // 
            // PictureBox12
            // 
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(6, 66);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox12.TabIndex = 125;
            PictureBox12.TabStop = false;
            // 
            // ExtTerminal_TerminalScrolling
            // 
            ExtTerminal_TerminalScrolling.BackColor = Color.FromArgb(34, 34, 34);
            ExtTerminal_TerminalScrolling.Checked = false;
            ExtTerminal_TerminalScrolling.Font = new Font("Segoe UI", 9.0f);
            ExtTerminal_TerminalScrolling.ForeColor = Color.White;
            ExtTerminal_TerminalScrolling.Location = new Point(36, 66);
            ExtTerminal_TerminalScrolling.Name = "ExtTerminal_TerminalScrolling";
            ExtTerminal_TerminalScrolling.Size = new Size(155, 24);
            ExtTerminal_TerminalScrolling.TabIndex = 123;
            ExtTerminal_TerminalScrolling.Text = "Terminal scrolling";
            // 
            // ExtTerminal_OpacityBar
            // 
            ExtTerminal_OpacityBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExtTerminal_OpacityBar.LargeChange = 10;
            ExtTerminal_OpacityBar.Location = new Point(39, 123);
            ExtTerminal_OpacityBar.Maximum = 255;
            ExtTerminal_OpacityBar.Minimum = 0;
            ExtTerminal_OpacityBar.Name = "ExtTerminal_OpacityBar";
            ExtTerminal_OpacityBar.Size = new Size(216, 19);
            ExtTerminal_OpacityBar.SmallChange = 1;
            ExtTerminal_OpacityBar.TabIndex = 120;
            ExtTerminal_OpacityBar.Value = 0;
            // 
            // PictureBox11
            // 
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(6, 36);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 124;
            PictureBox11.TabStop = false;
            // 
            // Label57
            // 
            Label57.BackColor = Color.Transparent;
            Label57.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label57.Location = new Point(36, 96);
            Label57.Name = "Label57";
            Label57.Size = new Size(104, 24);
            Label57.TabIndex = 119;
            Label57.Text = "Window opacity:";
            Label57.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FontDialog1
            // 
            FontDialog1.FixedPitchOnly = true;
            FontDialog1.ShowEffects = false;
            // 
            // ExternalTerminal
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(901, 731);
            Controls.Add(TabControl1);
            Controls.Add(Separator2);
            Controls.Add(CheckBox1);
            Controls.Add(Button10);
            Controls.Add(Button2);
            Controls.Add(GroupBox73);
            Controls.Add(GroupBox51);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExternalTerminal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "External Terminal";
            GroupBox73.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox23).EndInit();
            GroupBox51.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            TabPage2.ResumeLayout(false);
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            TabPage3.ResumeLayout(false);
            GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ExtTerminal_PreviewCUR.ResumeLayout(false);
            ExtTerminal_PreviewCUR2.ResumeLayout(false);
            TabPage4.ResumeLayout(false);
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            Load += new EventHandler(ExternalTerminal_Load);
            Shown += new EventHandler(ExternalTerminal_Shown);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }
        internal UI.WP.GroupBox GroupBox73;
        internal PictureBox PictureBox23;
        internal UI.Simulation.WinCMD CMD4;
        internal Label Label137;
        internal UI.WP.GroupBox GroupBox51;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button6;
        internal UI.WP.ComboBox ComboBox1;
        internal PictureBox PictureBox17;
        internal Label Label102;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button2;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.Button Button1;
        internal UI.WP.SeparatorH Separator2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button4;
        internal Label Label4;
        internal OpenFileDialog OpenWPTHDlg;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal UI.WP.GroupBox GroupBox2;
        internal PictureBox PictureBox15;
        internal UI.WP.SeparatorH Separator3;
        internal Label ExtTerminal_AccentBackgroundLbl;
        internal Label ExtTerminal_PopupForegroundLbl;
        internal Label Label50;
        internal Label ExtTerminal_AccentForegroundLbl;
        internal Label ExtTerminal_PopupBackgroundLbl;
        internal UI.WP.Trackbar ExtTerminal_AccentBackgroundBar;
        internal UI.WP.Trackbar ExtTerminal_PopupBackgroundBar;
        internal UI.WP.Trackbar ExtTerminal_AccentForegroundBar;
        internal Label Label18;
        internal Label Label17;
        internal Label Label49;
        internal UI.WP.Trackbar ExtTerminal_PopupForegroundBar;
        internal Label Label6;
        internal UI.WP.GroupBox GroupBox1;
        internal PictureBox PictureBox14;
        internal Label Label31;
        internal Label Label19;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable00;
        internal Label Label7;
        internal Label Label32;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable01;
        internal Label Label20;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable02;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable03;
        internal Label Label33;
        internal Label Label21;
        internal Label Label22;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable04;
        internal Label Label26;
        internal Label Label34;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable05;
        internal Label Label25;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable06;
        internal Label Label24;
        internal Label Label27;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable15;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable07;
        internal Label Label23;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable08;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable14;
        internal Label Label28;
        internal Label Label30;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable09;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable13;
        internal Label Label29;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable12;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable10;
        internal UI.Controllers.ColorItem ExtTerminal_ColorTable11;
        internal TabPage TabPage2;
        internal UI.WP.GroupBox GroupBox4;
        internal UI.WP.Toggle ExtTerminal_RasterToggle;
        internal PictureBox PictureBox1;
        internal Label Label58;
        internal UI.WP.Trackbar ExtTerminal_FontSizeBar;
        internal UI.WP.ComboBox ExtTerminal_FontWeightBox;
        internal Label Label61;
        internal PictureBox PictureBox6;
        internal PictureBox PictureBox3;
        internal Label Label35;
        internal UI.WP.Button Button25;
        internal PictureBox PictureBox4;
        internal Label Label59;
        internal UI.WP.ComboBox RasterList;
        internal TabPage TabPage3;
        internal Label Label3;
        internal UI.WP.GroupBox GroupBox34;
        internal PictureBox PictureBox7;
        internal UI.WP.ComboBox ExtTerminal_CursorStyle;
        internal Label Label60;
        internal UI.WP.Trackbar ExtTerminal_CursorSizeBar;
        internal Label Label1;
        internal PictureBox PictureBox9;
        internal Label Label2;
        internal Panel ExtTerminal_PreviewCUR;
        internal Panel ExtTerminal_PreviewCUR2;
        internal Panel ExtTerminal_PreviewCursorInner;
        internal UI.Controllers.ColorItem ExtTerminal_CursorColor;
        internal TabPage TabPage4;
        internal Label Label5;
        internal UI.WP.GroupBox GroupBox12;
        internal PictureBox PictureBox10;
        internal PictureBox PictureBox13;
        internal UI.WP.CheckBox ExtTerminal_LineSelection;
        internal UI.WP.CheckBox ExtTerminal_EnhancedTerminal;
        internal PictureBox PictureBox12;
        internal UI.WP.CheckBox ExtTerminal_TerminalScrolling;
        internal UI.WP.Trackbar ExtTerminal_OpacityBar;
        internal PictureBox PictureBox11;
        internal Label Label57;
        internal UI.WP.Button ExtTerminal_FontSizeVal;
        internal UI.WP.Button ExtTerminal_PreviewCUR_Val;
        internal UI.WP.Button ExtTerminal_OpacityVal;
        internal PictureBox PictureBox8;
        internal Label FontName;
        internal UI.WP.Button Button5;
        internal FontDialog FontDialog1;
    }
}