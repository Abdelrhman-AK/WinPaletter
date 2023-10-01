using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class CMD : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CMD));
            ImageList1 = new ImageList(components);
            OpenWPTHDlg = new OpenFileDialog();
            Separator2 = new UI.WP.SeparatorH();
            GroupBox3 = new UI.WP.GroupBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Label4 = new Label();
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            CMDEnabled = new UI.WP.Toggle();
            CMDEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(CMDEnabled_CheckedChanged);
            checker_img = new PictureBox();
            GroupBox2 = new UI.WP.GroupBox();
            PictureBox15 = new PictureBox();
            Separator1 = new UI.WP.SeparatorH();
            CMD_AccentBackgroundLbl = new Label();
            CMD_PopupForegroundLbl = new Label();
            Label50 = new Label();
            CMD_AccentForegroundLbl = new Label();
            CMD_PopupBackgroundLbl = new Label();
            CMD_AccentBackgroundBar = new UI.WP.Trackbar();
            CMD_AccentBackgroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CMD_AccentBackgroundBar_Scroll);
            CMD_PopupBackgroundBar = new UI.WP.Trackbar();
            CMD_PopupBackgroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CMD_PopupBackgroundBar_Scroll);
            CMD_AccentForegroundBar = new UI.WP.Trackbar();
            CMD_AccentForegroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CMD_AccentForegroundBar_Scroll);
            Label18 = new Label();
            Label17 = new Label();
            Label49 = new Label();
            CMD_PopupForegroundBar = new UI.WP.Trackbar();
            CMD_PopupForegroundBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CommandPrompt_PopupForegroundBar_Scroll);
            Label6 = new Label();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            GroupBox12 = new UI.WP.GroupBox();
            CMD_OpacityVal = new UI.WP.Button();
            CMD_OpacityVal.Click += new EventHandler(CMD_OpacityVal_Click);
            PictureBox10 = new PictureBox();
            PictureBox13 = new PictureBox();
            CMD_LineSelection = new UI.WP.CheckBox();
            CMD_EnhancedTerminal = new UI.WP.CheckBox();
            PictureBox12 = new PictureBox();
            CMD_TerminalScrolling = new UI.WP.CheckBox();
            CMD_OpacityBar = new UI.WP.Trackbar();
            CMD_OpacityBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CMD_OpacityBar_Scroll);
            PictureBox11 = new PictureBox();
            Label57 = new Label();
            Label5 = new Label();
            GroupBox34 = new UI.WP.GroupBox();
            CMD_PreviewCUR_Val = new UI.WP.Button();
            CMD_PreviewCUR_Val.Click += new EventHandler(CMD_PreviewCUR_Val_Click);
            PictureBox7 = new PictureBox();
            CMD_CursorStyle = new UI.WP.ComboBox();
            CMD_CursorStyle.SelectedIndexChanged += new EventHandler(CMD_CursorStyle_SelectedIndexChanged_1);
            Label60 = new Label();
            CMD_CursorSizeBar = new UI.WP.Trackbar();
            CMD_CursorSizeBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CMD_CursorSizeBar_Scroll);
            Label1 = new Label();
            PictureBox9 = new PictureBox();
            Label2 = new Label();
            CMD_PreviewCUR = new Panel();
            CMD_PreviewCUR2 = new Panel();
            CMD_PreviewCursorInner = new Panel();
            CMD_CursorColor = new UI.Controllers.ColorItem();
            CMD_CursorColor.Click += new EventHandler(CMD_CursorColor_Click);
            CMD_CursorColor.DragDrop += new DragEventHandler(CMD_CursorColor_Click);
            PictureBox8 = new PictureBox();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            GroupBox4 = new UI.WP.GroupBox();
            FontName = new Label();
            FontName.FontChanged += new EventHandler(CMD_FontsBox_SelectedIndexChanged);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            CMD_FontSizeVal = new UI.WP.Button();
            CMD_FontSizeVal.Click += new EventHandler(CMD_FontSizeVal_Click);
            CMD_RasterToggle = new UI.WP.Toggle();
            CMD_RasterToggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(CMD_RasterToggle_CheckedChanged);
            PictureBox1 = new PictureBox();
            Label58 = new Label();
            CMD_FontSizeBar = new UI.WP.Trackbar();
            CMD_FontSizeBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CMD_FontSizeBar_Scroll);
            CMD_FontWeightBox = new UI.WP.ComboBox();
            CMD_FontWeightBox.SelectedIndexChanged += new EventHandler(CMD_FontWeightBox_SelectedIndexChanged);
            Label61 = new Label();
            PictureBox6 = new PictureBox();
            PictureBox3 = new PictureBox();
            Label35 = new Label();
            Button25 = new UI.WP.Button();
            Button25.Click += new EventHandler(Button25_Click);
            PictureBox4 = new PictureBox();
            Label59 = new Label();
            RasterList = new UI.WP.ComboBox();
            RasterList.SelectedIndexChanged += new EventHandler(RasterList_SelectedIndexChanged);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            GroupBox1 = new UI.WP.GroupBox();
            PictureBox14 = new PictureBox();
            Label31 = new Label();
            Label19 = new Label();
            ColorTable00 = new UI.Controllers.ColorItem();
            ColorTable00.Click += new EventHandler(ColorTable00_Click);
            ColorTable00.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label7 = new Label();
            Label32 = new Label();
            ColorTable01 = new UI.Controllers.ColorItem();
            ColorTable01.Click += new EventHandler(ColorTable00_Click);
            ColorTable01.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label20 = new Label();
            ColorTable02 = new UI.Controllers.ColorItem();
            ColorTable02.Click += new EventHandler(ColorTable00_Click);
            ColorTable02.DragDrop += new DragEventHandler(ColorTable00_Click);
            ColorTable03 = new UI.Controllers.ColorItem();
            ColorTable03.Click += new EventHandler(ColorTable00_Click);
            ColorTable03.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label33 = new Label();
            Label21 = new Label();
            Label22 = new Label();
            ColorTable04 = new UI.Controllers.ColorItem();
            ColorTable04.Click += new EventHandler(ColorTable00_Click);
            ColorTable04.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label26 = new Label();
            Label34 = new Label();
            ColorTable05 = new UI.Controllers.ColorItem();
            ColorTable05.Click += new EventHandler(ColorTable00_Click);
            ColorTable05.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label25 = new Label();
            ColorTable06 = new UI.Controllers.ColorItem();
            ColorTable06.Click += new EventHandler(ColorTable00_Click);
            ColorTable06.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label24 = new Label();
            Label27 = new Label();
            ColorTable15 = new UI.Controllers.ColorItem();
            ColorTable15.Click += new EventHandler(ColorTable00_Click);
            ColorTable15.DragDrop += new DragEventHandler(ColorTable00_Click);
            ColorTable07 = new UI.Controllers.ColorItem();
            ColorTable07.Click += new EventHandler(ColorTable00_Click);
            ColorTable07.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label23 = new Label();
            ColorTable08 = new UI.Controllers.ColorItem();
            ColorTable08.Click += new EventHandler(ColorTable00_Click);
            ColorTable08.DragDrop += new DragEventHandler(ColorTable00_Click);
            ColorTable14 = new UI.Controllers.ColorItem();
            ColorTable14.Click += new EventHandler(ColorTable00_Click);
            ColorTable14.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label28 = new Label();
            Label30 = new Label();
            ColorTable09 = new UI.Controllers.ColorItem();
            ColorTable09.Click += new EventHandler(ColorTable00_Click);
            ColorTable09.DragDrop += new DragEventHandler(ColorTable00_Click);
            ColorTable13 = new UI.Controllers.ColorItem();
            ColorTable13.Click += new EventHandler(ColorTable00_Click);
            ColorTable13.DragDrop += new DragEventHandler(ColorTable00_Click);
            Label29 = new Label();
            ColorTable12 = new UI.Controllers.ColorItem();
            ColorTable12.Click += new EventHandler(ColorTable00_Click);
            ColorTable12.DragDrop += new DragEventHandler(ColorTable00_Click);
            ColorTable10 = new UI.Controllers.ColorItem();
            ColorTable10.Click += new EventHandler(ColorTable00_Click);
            ColorTable10.DragDrop += new DragEventHandler(ColorTable00_Click);
            ColorTable11 = new UI.Controllers.ColorItem();
            ColorTable11.Click += new EventHandler(ColorTable00_Click);
            ColorTable11.DragDrop += new DragEventHandler(ColorTable00_Click);
            GroupBox8 = new UI.WP.GroupBox();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            PictureBox41 = new PictureBox();
            CMD1 = new UI.Simulation.WinCMD();
            Label41 = new Label();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            TabPage2 = new TabPage();
            TabPage3 = new TabPage();
            Label3 = new Label();
            TabPage4 = new TabPage();
            FontDialog1 = new FontDialog();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            CMD_PreviewCUR.SuspendLayout();
            CMD_PreviewCUR2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            TabPage2.SuspendLayout();
            TabPage3.SuspendLayout();
            TabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // ImageList1
            // 
            ImageList1.ImageStream = (ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
            ImageList1.TransparentColor = Color.Transparent;
            ImageList1.Images.SetKeyName(0, "0.png");
            ImageList1.Images.SetKeyName(1, "1.png");
            ImageList1.Images.SetKeyName(2, "3.png");
            // 
            // OpenWPTHDlg
            // 
            OpenWPTHDlg.Filter = "WinPaletter Theme File (*.wpth)|*.wpth";
            // 
            // Separator2
            // 
            Separator2.AlternativeLook = false;
            Separator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator2.Location = new Point(12, 57);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(896, 1);
            Separator2.TabIndex = 199;
            Separator2.TabStop = false;
            Separator2.Text = "Separator2";
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(Button3);
            GroupBox3.Controls.Add(Label4);
            GroupBox3.Controls.Add(Button8);
            GroupBox3.Controls.Add(Button6);
            GroupBox3.Controls.Add(CMDEnabled);
            GroupBox3.Controls.Add(checker_img);
            GroupBox3.Location = new Point(12, 12);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(896, 39);
            GroupBox3.TabIndex = 198;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.ImageAlign = ContentAlignment.MiddleLeft;
            Button3.LineColor = Color.FromArgb(90, 134, 117);
            Button3.Location = new Point(222, 5);
            Button3.Name = "Button3";
            Button3.Size = new Size(142, 29);
            Button3.TabIndex = 112;
            Button3.Text = "Current applied";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4.Location = new Point(4, 4);
            Label4.Name = "Label4";
            Label4.Size = new Size(75, 31);
            Label4.TabIndex = 111;
            Label4.Text = "Open from:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button8.BackColor = Color.FromArgb(43, 43, 43);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleRight;
            Button8.LineColor = Color.FromArgb(113, 122, 131);
            Button8.Location = new Point(85, 5);
            Button8.Name = "Button8";
            Button8.Size = new Size(135, 29);
            Button8.TabIndex = 110;
            Button8.Text = "WinPaletter theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = null;
            Button6.ImageAlign = ContentAlignment.MiddleLeft;
            Button6.LineColor = Color.FromArgb(0, 66, 119);
            Button6.Location = new Point(366, 5);
            Button6.Name = "Button6";
            Button6.Size = new Size(139, 29);
            Button6.TabIndex = 108;
            Button6.Text = "Default Windows";
            Button6.UseVisualStyleBackColor = false;
            // 
            // CMDEnabled
            // 
            CMDEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMDEnabled.BackColor = Color.FromArgb(43, 43, 43);
            CMDEnabled.Checked = false;
            CMDEnabled.DarkLight_Toggler = false;
            CMDEnabled.Location = new Point(847, 9);
            CMDEnabled.Name = "CMDEnabled";
            CMDEnabled.Size = new Size(40, 20);
            CMDEnabled.TabIndex = 85;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = My.Resources.checker_disabled;
            checker_img.Location = new Point(804, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(PictureBox15);
            GroupBox2.Controls.Add(Separator1);
            GroupBox2.Controls.Add(CMD_AccentBackgroundLbl);
            GroupBox2.Controls.Add(CMD_PopupForegroundLbl);
            GroupBox2.Controls.Add(Label50);
            GroupBox2.Controls.Add(CMD_AccentForegroundLbl);
            GroupBox2.Controls.Add(CMD_PopupBackgroundLbl);
            GroupBox2.Controls.Add(CMD_AccentBackgroundBar);
            GroupBox2.Controls.Add(CMD_PopupBackgroundBar);
            GroupBox2.Controls.Add(CMD_AccentForegroundBar);
            GroupBox2.Controls.Add(Label18);
            GroupBox2.Controls.Add(Label17);
            GroupBox2.Controls.Add(Label49);
            GroupBox2.Controls.Add(CMD_PopupForegroundBar);
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
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(9, 136);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(285, 1);
            Separator1.TabIndex = 101;
            Separator1.TabStop = false;
            // 
            // CMD_AccentBackgroundLbl
            // 
            CMD_AccentBackgroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_AccentBackgroundLbl.BackColor = Color.Gray;
            CMD_AccentBackgroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CMD_AccentBackgroundLbl.Location = new Point(243, 83);
            CMD_AccentBackgroundLbl.Name = "CMD_AccentBackgroundLbl";
            CMD_AccentBackgroundLbl.Size = new Size(50, 20);
            CMD_AccentBackgroundLbl.TabIndex = 88;
            CMD_AccentBackgroundLbl.Text = "0";
            CMD_AccentBackgroundLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CMD_PopupForegroundLbl
            // 
            CMD_PopupForegroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_PopupForegroundLbl.BackColor = Color.Gray;
            CMD_PopupForegroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CMD_PopupForegroundLbl.Location = new Point(243, 148);
            CMD_PopupForegroundLbl.Name = "CMD_PopupForegroundLbl";
            CMD_PopupForegroundLbl.Size = new Size(50, 20);
            CMD_PopupForegroundLbl.TabIndex = 87;
            CMD_PopupForegroundLbl.Text = "0";
            CMD_PopupForegroundLbl.TextAlign = ContentAlignment.MiddleLeft;
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
            // CMD_AccentForegroundLbl
            // 
            CMD_AccentForegroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_AccentForegroundLbl.BackColor = Color.Gray;
            CMD_AccentForegroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CMD_AccentForegroundLbl.Location = new Point(243, 36);
            CMD_AccentForegroundLbl.Name = "CMD_AccentForegroundLbl";
            CMD_AccentForegroundLbl.Size = new Size(50, 20);
            CMD_AccentForegroundLbl.TabIndex = 87;
            CMD_AccentForegroundLbl.Text = "0";
            CMD_AccentForegroundLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CMD_PopupBackgroundLbl
            // 
            CMD_PopupBackgroundLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_PopupBackgroundLbl.BackColor = Color.Gray;
            CMD_PopupBackgroundLbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CMD_PopupBackgroundLbl.Location = new Point(243, 195);
            CMD_PopupBackgroundLbl.Name = "CMD_PopupBackgroundLbl";
            CMD_PopupBackgroundLbl.Size = new Size(50, 20);
            CMD_PopupBackgroundLbl.TabIndex = 88;
            CMD_PopupBackgroundLbl.Text = "0";
            CMD_PopupBackgroundLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CMD_AccentBackgroundBar
            // 
            CMD_AccentBackgroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_AccentBackgroundBar.LargeChange = 10;
            CMD_AccentBackgroundBar.Location = new Point(8, 107);
            CMD_AccentBackgroundBar.Maximum = 15;
            CMD_AccentBackgroundBar.Minimum = 0;
            CMD_AccentBackgroundBar.Name = "CMD_AccentBackgroundBar";
            CMD_AccentBackgroundBar.Size = new Size(289, 19);
            CMD_AccentBackgroundBar.SmallChange = 1;
            CMD_AccentBackgroundBar.TabIndex = 86;
            CMD_AccentBackgroundBar.Value = 0;
            // 
            // CMD_PopupBackgroundBar
            // 
            CMD_PopupBackgroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_PopupBackgroundBar.LargeChange = 10;
            CMD_PopupBackgroundBar.Location = new Point(8, 219);
            CMD_PopupBackgroundBar.Maximum = 15;
            CMD_PopupBackgroundBar.Minimum = 0;
            CMD_PopupBackgroundBar.Name = "CMD_PopupBackgroundBar";
            CMD_PopupBackgroundBar.Size = new Size(289, 19);
            CMD_PopupBackgroundBar.SmallChange = 1;
            CMD_PopupBackgroundBar.TabIndex = 86;
            CMD_PopupBackgroundBar.Value = 0;
            // 
            // CMD_AccentForegroundBar
            // 
            CMD_AccentForegroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_AccentForegroundBar.LargeChange = 10;
            CMD_AccentForegroundBar.Location = new Point(8, 60);
            CMD_AccentForegroundBar.Maximum = 15;
            CMD_AccentForegroundBar.Minimum = 0;
            CMD_AccentForegroundBar.Name = "CMD_AccentForegroundBar";
            CMD_AccentForegroundBar.Size = new Size(289, 19);
            CMD_AccentForegroundBar.SmallChange = 1;
            CMD_AccentForegroundBar.TabIndex = 84;
            CMD_AccentForegroundBar.Value = 0;
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
            // CMD_PopupForegroundBar
            // 
            CMD_PopupForegroundBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_PopupForegroundBar.LargeChange = 10;
            CMD_PopupForegroundBar.Location = new Point(8, 172);
            CMD_PopupForegroundBar.Maximum = 15;
            CMD_PopupForegroundBar.Minimum = 0;
            CMD_PopupForegroundBar.Name = "CMD_PopupForegroundBar";
            CMD_PopupForegroundBar.Size = new Size(289, 19);
            CMD_PopupForegroundBar.SmallChange = 1;
            CMD_PopupForegroundBar.TabIndex = 84;
            CMD_PopupForegroundBar.Value = 0;
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
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(12, 621);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(603, 24);
            CheckBox1.TabIndex = 100;
            CheckBox1.Text = "Allow non monospace fonts (causes wrong renderering in enhanced terminal, not use" + "d in legacy terminal)";
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(CMD_OpacityVal);
            GroupBox12.Controls.Add(PictureBox10);
            GroupBox12.Controls.Add(PictureBox13);
            GroupBox12.Controls.Add(CMD_LineSelection);
            GroupBox12.Controls.Add(CMD_EnhancedTerminal);
            GroupBox12.Controls.Add(PictureBox12);
            GroupBox12.Controls.Add(CMD_TerminalScrolling);
            GroupBox12.Controls.Add(CMD_OpacityBar);
            GroupBox12.Controls.Add(PictureBox11);
            GroupBox12.Controls.Add(Label57);
            GroupBox12.Location = new Point(6, 6);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(301, 150);
            GroupBox12.TabIndex = 100;
            // 
            // CMD_OpacityVal
            // 
            CMD_OpacityVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_OpacityVal.BackColor = Color.FromArgb(43, 43, 43);
            CMD_OpacityVal.DrawOnGlass = false;
            CMD_OpacityVal.Font = new Font("Segoe UI", 9.0f);
            CMD_OpacityVal.ForeColor = Color.White;
            CMD_OpacityVal.Image = null;
            CMD_OpacityVal.LineColor = Color.FromArgb(0, 81, 210);
            CMD_OpacityVal.Location = new Point(262, 120);
            CMD_OpacityVal.Name = "CMD_OpacityVal";
            CMD_OpacityVal.Size = new Size(34, 24);
            CMD_OpacityVal.TabIndex = 133;
            CMD_OpacityVal.UseVisualStyleBackColor = false;
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
            // CMD_LineSelection
            // 
            CMD_LineSelection.BackColor = Color.FromArgb(34, 34, 34);
            CMD_LineSelection.Checked = false;
            CMD_LineSelection.Font = new Font("Segoe UI", 9.0f);
            CMD_LineSelection.ForeColor = Color.White;
            CMD_LineSelection.Location = new Point(36, 36);
            CMD_LineSelection.Name = "CMD_LineSelection";
            CMD_LineSelection.Size = new Size(155, 24);
            CMD_LineSelection.TabIndex = 122;
            CMD_LineSelection.Text = "Line selection";
            // 
            // CMD_EnhancedTerminal
            // 
            CMD_EnhancedTerminal.BackColor = Color.FromArgb(34, 34, 34);
            CMD_EnhancedTerminal.Checked = false;
            CMD_EnhancedTerminal.Font = new Font("Segoe UI", 9.0f);
            CMD_EnhancedTerminal.ForeColor = Color.White;
            CMD_EnhancedTerminal.Location = new Point(36, 6);
            CMD_EnhancedTerminal.Name = "CMD_EnhancedTerminal";
            CMD_EnhancedTerminal.Size = new Size(155, 24);
            CMD_EnhancedTerminal.TabIndex = 118;
            CMD_EnhancedTerminal.Text = "Enhanced terminal";
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
            // CMD_TerminalScrolling
            // 
            CMD_TerminalScrolling.BackColor = Color.FromArgb(34, 34, 34);
            CMD_TerminalScrolling.Checked = false;
            CMD_TerminalScrolling.Font = new Font("Segoe UI", 9.0f);
            CMD_TerminalScrolling.ForeColor = Color.White;
            CMD_TerminalScrolling.Location = new Point(36, 66);
            CMD_TerminalScrolling.Name = "CMD_TerminalScrolling";
            CMD_TerminalScrolling.Size = new Size(155, 24);
            CMD_TerminalScrolling.TabIndex = 123;
            CMD_TerminalScrolling.Text = "Terminal scrolling";
            // 
            // CMD_OpacityBar
            // 
            CMD_OpacityBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_OpacityBar.LargeChange = 10;
            CMD_OpacityBar.Location = new Point(39, 123);
            CMD_OpacityBar.Maximum = 255;
            CMD_OpacityBar.Minimum = 0;
            CMD_OpacityBar.Name = "CMD_OpacityBar";
            CMD_OpacityBar.Size = new Size(217, 19);
            CMD_OpacityBar.SmallChange = 1;
            CMD_OpacityBar.TabIndex = 120;
            CMD_OpacityBar.Value = 0;
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
            // GroupBox34
            // 
            GroupBox34.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox34.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox34.Controls.Add(CMD_PreviewCUR_Val);
            GroupBox34.Controls.Add(PictureBox7);
            GroupBox34.Controls.Add(CMD_CursorStyle);
            GroupBox34.Controls.Add(Label60);
            GroupBox34.Controls.Add(CMD_CursorSizeBar);
            GroupBox34.Controls.Add(Label1);
            GroupBox34.Controls.Add(PictureBox9);
            GroupBox34.Controls.Add(Label2);
            GroupBox34.Controls.Add(CMD_PreviewCUR);
            GroupBox34.Controls.Add(CMD_CursorColor);
            GroupBox34.Controls.Add(PictureBox8);
            GroupBox34.Location = new Point(6, 6);
            GroupBox34.Name = "GroupBox34";
            GroupBox34.Size = new Size(301, 97);
            GroupBox34.TabIndex = 99;
            // 
            // CMD_PreviewCUR_Val
            // 
            CMD_PreviewCUR_Val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_PreviewCUR_Val.BackColor = Color.FromArgb(43, 43, 43);
            CMD_PreviewCUR_Val.DrawOnGlass = false;
            CMD_PreviewCUR_Val.Font = new Font("Segoe UI", 9.0f);
            CMD_PreviewCUR_Val.ForeColor = Color.White;
            CMD_PreviewCUR_Val.Image = null;
            CMD_PreviewCUR_Val.LineColor = Color.FromArgb(0, 81, 210);
            CMD_PreviewCUR_Val.Location = new Point(262, 66);
            CMD_PreviewCUR_Val.Name = "CMD_PreviewCUR_Val";
            CMD_PreviewCUR_Val.Size = new Size(34, 24);
            CMD_PreviewCUR_Val.TabIndex = 132;
            CMD_PreviewCUR_Val.UseVisualStyleBackColor = false;
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
            // CMD_CursorStyle
            // 
            CMD_CursorStyle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_CursorStyle.BackColor = Color.FromArgb(43, 43, 43);
            CMD_CursorStyle.DrawMode = DrawMode.OwnerDrawFixed;
            CMD_CursorStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            CMD_CursorStyle.Font = new Font("Segoe UI", 9.0f);
            CMD_CursorStyle.ForeColor = Color.White;
            CMD_CursorStyle.FormattingEnabled = true;
            CMD_CursorStyle.ItemHeight = 20;
            CMD_CursorStyle.Items.AddRange(new object[] { "Default", "Legacy", "Underscore", "Empty Box", "Vertical Bar", "Solid Box" });
            CMD_CursorStyle.Location = new Point(97, 35);
            CMD_CursorStyle.Name = "CMD_CursorStyle";
            CMD_CursorStyle.Size = new Size(199, 26);
            CMD_CursorStyle.TabIndex = 110;
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
            // CMD_CursorSizeBar
            // 
            CMD_CursorSizeBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_CursorSizeBar.LargeChange = 1;
            CMD_CursorSizeBar.Location = new Point(97, 69);
            CMD_CursorSizeBar.Maximum = 100;
            CMD_CursorSizeBar.Minimum = 20;
            CMD_CursorSizeBar.Name = "CMD_CursorSizeBar";
            CMD_CursorSizeBar.Size = new Size(159, 19);
            CMD_CursorSizeBar.SmallChange = 1;
            CMD_CursorSizeBar.TabIndex = 102;
            CMD_CursorSizeBar.Value = 20;
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
            // CMD_PreviewCUR
            // 
            CMD_PreviewCUR.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_PreviewCUR.BackColor = Color.Black;
            CMD_PreviewCUR.Controls.Add(CMD_PreviewCUR2);
            CMD_PreviewCUR.Location = new Point(247, 6);
            CMD_PreviewCUR.Name = "CMD_PreviewCUR";
            CMD_PreviewCUR.Size = new Size(49, 24);
            CMD_PreviewCUR.TabIndex = 103;
            // 
            // CMD_PreviewCUR2
            // 
            CMD_PreviewCUR2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CMD_PreviewCUR2.BackColor = Color.White;
            CMD_PreviewCUR2.Controls.Add(CMD_PreviewCursorInner);
            CMD_PreviewCUR2.Location = new Point(3, 16);
            CMD_PreviewCUR2.Name = "CMD_PreviewCUR2";
            CMD_PreviewCUR2.Padding = new Padding(1);
            CMD_PreviewCUR2.Size = new Size(8, 5);
            CMD_PreviewCUR2.TabIndex = 104;
            // 
            // CMD_PreviewCursorInner
            // 
            CMD_PreviewCursorInner.BackColor = Color.Transparent;
            CMD_PreviewCursorInner.Dock = DockStyle.Fill;
            CMD_PreviewCursorInner.Location = new Point(1, 1);
            CMD_PreviewCursorInner.Name = "CMD_PreviewCursorInner";
            CMD_PreviewCursorInner.Size = new Size(6, 3);
            CMD_PreviewCursorInner.TabIndex = 106;
            // 
            // CMD_CursorColor
            // 
            CMD_CursorColor.AllowDrop = true;
            CMD_CursorColor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_CursorColor.BackColor = Color.White;
            CMD_CursorColor.DefaultColor = Color.White;
            CMD_CursorColor.DontShowInfo = false;
            CMD_CursorColor.Location = new Point(97, 6);
            CMD_CursorColor.Margin = new Padding(4, 3, 4, 3);
            CMD_CursorColor.Name = "CMD_CursorColor";
            CMD_CursorColor.Size = new Size(143, 24);
            CMD_CursorColor.TabIndex = 107;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(6, 36);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 104;
            PictureBox8.TabStop = false;
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
            Button10.Location = new Point(709, 615);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 82;
            Button10.Text = "Quick apply";
            Button10.UseVisualStyleBackColor = false;
            // 
            // GroupBox4
            // 
            GroupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox4.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox4.Controls.Add(FontName);
            GroupBox4.Controls.Add(Button5);
            GroupBox4.Controls.Add(CMD_FontSizeVal);
            GroupBox4.Controls.Add(CMD_RasterToggle);
            GroupBox4.Controls.Add(PictureBox1);
            GroupBox4.Controls.Add(Label58);
            GroupBox4.Controls.Add(CMD_FontSizeBar);
            GroupBox4.Controls.Add(CMD_FontWeightBox);
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
            FontName.TabIndex = 133;
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
            Button5.TabIndex = 132;
            Button5.Text = "...";
            Button5.UseVisualStyleBackColor = false;
            // 
            // CMD_FontSizeVal
            // 
            CMD_FontSizeVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_FontSizeVal.BackColor = Color.FromArgb(43, 43, 43);
            CMD_FontSizeVal.DrawOnGlass = false;
            CMD_FontSizeVal.Font = new Font("Segoe UI", 9.0f);
            CMD_FontSizeVal.ForeColor = Color.White;
            CMD_FontSizeVal.Image = null;
            CMD_FontSizeVal.LineColor = Color.FromArgb(0, 81, 210);
            CMD_FontSizeVal.Location = new Point(261, 66);
            CMD_FontSizeVal.Name = "CMD_FontSizeVal";
            CMD_FontSizeVal.Size = new Size(34, 24);
            CMD_FontSizeVal.TabIndex = 131;
            CMD_FontSizeVal.UseVisualStyleBackColor = false;
            // 
            // CMD_RasterToggle
            // 
            CMD_RasterToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CMD_RasterToggle.BackColor = Color.FromArgb(43, 43, 43);
            CMD_RasterToggle.Checked = false;
            CMD_RasterToggle.DarkLight_Toggler = false;
            CMD_RasterToggle.Location = new Point(255, 98);
            CMD_RasterToggle.Name = "CMD_RasterToggle";
            CMD_RasterToggle.Size = new Size(40, 20);
            CMD_RasterToggle.TabIndex = 95;
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
            // CMD_FontSizeBar
            // 
            CMD_FontSizeBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_FontSizeBar.LargeChange = 10;
            CMD_FontSizeBar.Location = new Point(97, 69);
            CMD_FontSizeBar.Maximum = 48;
            CMD_FontSizeBar.Minimum = 5;
            CMD_FontSizeBar.Name = "CMD_FontSizeBar";
            CMD_FontSizeBar.Size = new Size(158, 19);
            CMD_FontSizeBar.SmallChange = 1;
            CMD_FontSizeBar.TabIndex = 101;
            CMD_FontSizeBar.Value = 5;
            // 
            // CMD_FontWeightBox
            // 
            CMD_FontWeightBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CMD_FontWeightBox.BackColor = Color.FromArgb(43, 43, 43);
            CMD_FontWeightBox.DrawMode = DrawMode.OwnerDrawFixed;
            CMD_FontWeightBox.DropDownStyle = ComboBoxStyle.DropDownList;
            CMD_FontWeightBox.Font = new Font("Segoe UI", 9.0f);
            CMD_FontWeightBox.ForeColor = Color.White;
            CMD_FontWeightBox.FormattingEnabled = true;
            CMD_FontWeightBox.ItemHeight = 20;
            CMD_FontWeightBox.Items.AddRange(new object[] { "Don't Care", "Thin", "Extra Light", "Light", "Normal", "Medium", "Semi Bold", "Bold", "Extra Bold", "Heavy" });
            CMD_FontWeightBox.Location = new Point(96, 35);
            CMD_FontWeightBox.Name = "CMD_FontWeightBox";
            CMD_FontWeightBox.Size = new Size(172, 26);
            CMD_FontWeightBox.TabIndex = 99;
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
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(623, 615);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 66;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(PictureBox14);
            GroupBox1.Controls.Add(Label31);
            GroupBox1.Controls.Add(Label19);
            GroupBox1.Controls.Add(ColorTable00);
            GroupBox1.Controls.Add(Label7);
            GroupBox1.Controls.Add(Label32);
            GroupBox1.Controls.Add(ColorTable01);
            GroupBox1.Controls.Add(Label20);
            GroupBox1.Controls.Add(ColorTable02);
            GroupBox1.Controls.Add(ColorTable03);
            GroupBox1.Controls.Add(Label33);
            GroupBox1.Controls.Add(Label21);
            GroupBox1.Controls.Add(Label22);
            GroupBox1.Controls.Add(ColorTable04);
            GroupBox1.Controls.Add(Label26);
            GroupBox1.Controls.Add(Label34);
            GroupBox1.Controls.Add(ColorTable05);
            GroupBox1.Controls.Add(Label25);
            GroupBox1.Controls.Add(ColorTable06);
            GroupBox1.Controls.Add(Label24);
            GroupBox1.Controls.Add(Label27);
            GroupBox1.Controls.Add(ColorTable15);
            GroupBox1.Controls.Add(ColorTable07);
            GroupBox1.Controls.Add(Label23);
            GroupBox1.Controls.Add(ColorTable08);
            GroupBox1.Controls.Add(ColorTable14);
            GroupBox1.Controls.Add(Label28);
            GroupBox1.Controls.Add(Label30);
            GroupBox1.Controls.Add(ColorTable09);
            GroupBox1.Controls.Add(ColorTable13);
            GroupBox1.Controls.Add(Label29);
            GroupBox1.Controls.Add(ColorTable12);
            GroupBox1.Controls.Add(ColorTable10);
            GroupBox1.Controls.Add(ColorTable11);
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
            // ColorTable00
            // 
            ColorTable00.AllowDrop = true;
            ColorTable00.BackColor = Color.FromArgb(12, 12, 12);
            ColorTable00.DefaultColor = Color.FromArgb(12, 12, 12);
            ColorTable00.DontShowInfo = false;
            ColorTable00.Location = new Point(28, 36);
            ColorTable00.Margin = new Padding(4, 3, 4, 3);
            ColorTable00.Name = "ColorTable00";
            ColorTable00.Size = new Size(105, 25);
            ColorTable00.TabIndex = 3;
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
            // ColorTable01
            // 
            ColorTable01.AllowDrop = true;
            ColorTable01.BackColor = Color.FromArgb(0, 55, 218);
            ColorTable01.DefaultColor = Color.FromArgb(0, 55, 218);
            ColorTable01.DontShowInfo = false;
            ColorTable01.Location = new Point(28, 67);
            ColorTable01.Margin = new Padding(4, 3, 4, 3);
            ColorTable01.Name = "ColorTable01";
            ColorTable01.Size = new Size(105, 25);
            ColorTable01.TabIndex = 5;
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
            // ColorTable02
            // 
            ColorTable02.AllowDrop = true;
            ColorTable02.BackColor = Color.FromArgb(19, 161, 14);
            ColorTable02.DefaultColor = Color.FromArgb(19, 161, 14);
            ColorTable02.DontShowInfo = false;
            ColorTable02.Location = new Point(28, 98);
            ColorTable02.Margin = new Padding(4, 3, 4, 3);
            ColorTable02.Name = "ColorTable02";
            ColorTable02.Size = new Size(105, 25);
            ColorTable02.TabIndex = 7;
            // 
            // ColorTable03
            // 
            ColorTable03.AllowDrop = true;
            ColorTable03.BackColor = Color.FromArgb(58, 150, 221);
            ColorTable03.DefaultColor = Color.FromArgb(58, 150, 221);
            ColorTable03.DontShowInfo = false;
            ColorTable03.Location = new Point(28, 129);
            ColorTable03.Margin = new Padding(4, 3, 4, 3);
            ColorTable03.Name = "ColorTable03";
            ColorTable03.Size = new Size(105, 25);
            ColorTable03.TabIndex = 9;
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
            // ColorTable04
            // 
            ColorTable04.AllowDrop = true;
            ColorTable04.BackColor = Color.FromArgb(197, 15, 31);
            ColorTable04.DefaultColor = Color.FromArgb(197, 15, 31);
            ColorTable04.DontShowInfo = false;
            ColorTable04.Location = new Point(28, 160);
            ColorTable04.Margin = new Padding(4, 3, 4, 3);
            ColorTable04.Name = "ColorTable04";
            ColorTable04.Size = new Size(105, 25);
            ColorTable04.TabIndex = 11;
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
            // ColorTable05
            // 
            ColorTable05.AllowDrop = true;
            ColorTable05.BackColor = Color.FromArgb(136, 23, 152);
            ColorTable05.DefaultColor = Color.FromArgb(136, 23, 152);
            ColorTable05.DontShowInfo = false;
            ColorTable05.Location = new Point(28, 191);
            ColorTable05.Margin = new Padding(4, 3, 4, 3);
            ColorTable05.Name = "ColorTable05";
            ColorTable05.Size = new Size(105, 25);
            ColorTable05.TabIndex = 13;
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
            // ColorTable06
            // 
            ColorTable06.AllowDrop = true;
            ColorTable06.BackColor = Color.FromArgb(193, 156, 0);
            ColorTable06.DefaultColor = Color.FromArgb(193, 156, 0);
            ColorTable06.DontShowInfo = false;
            ColorTable06.Location = new Point(28, 222);
            ColorTable06.Margin = new Padding(4, 3, 4, 3);
            ColorTable06.Name = "ColorTable06";
            ColorTable06.Size = new Size(105, 25);
            ColorTable06.TabIndex = 15;
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
            // ColorTable15
            // 
            ColorTable15.AllowDrop = true;
            ColorTable15.BackColor = Color.FromArgb(242, 242, 242);
            ColorTable15.DefaultColor = Color.FromArgb(242, 242, 242);
            ColorTable15.DontShowInfo = false;
            ColorTable15.Location = new Point(189, 253);
            ColorTable15.Margin = new Padding(4, 3, 4, 3);
            ColorTable15.Name = "ColorTable15";
            ColorTable15.Size = new Size(105, 25);
            ColorTable15.TabIndex = 33;
            // 
            // ColorTable07
            // 
            ColorTable07.AllowDrop = true;
            ColorTable07.BackColor = Color.FromArgb(204, 204, 204);
            ColorTable07.DefaultColor = Color.FromArgb(204, 204, 204);
            ColorTable07.DontShowInfo = false;
            ColorTable07.Location = new Point(28, 253);
            ColorTable07.Margin = new Padding(4, 3, 4, 3);
            ColorTable07.Name = "ColorTable07";
            ColorTable07.Size = new Size(105, 25);
            ColorTable07.TabIndex = 17;
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
            // ColorTable08
            // 
            ColorTable08.AllowDrop = true;
            ColorTable08.BackColor = Color.FromArgb(118, 118, 118);
            ColorTable08.DefaultColor = Color.FromArgb(118, 118, 118);
            ColorTable08.DontShowInfo = false;
            ColorTable08.Location = new Point(189, 36);
            ColorTable08.Margin = new Padding(4, 3, 4, 3);
            ColorTable08.Name = "ColorTable08";
            ColorTable08.Size = new Size(105, 25);
            ColorTable08.TabIndex = 19;
            // 
            // ColorTable14
            // 
            ColorTable14.AllowDrop = true;
            ColorTable14.BackColor = Color.FromArgb(249, 241, 165);
            ColorTable14.DefaultColor = Color.FromArgb(249, 241, 165);
            ColorTable14.DontShowInfo = false;
            ColorTable14.Location = new Point(189, 222);
            ColorTable14.Margin = new Padding(4, 3, 4, 3);
            ColorTable14.Name = "ColorTable14";
            ColorTable14.Size = new Size(105, 25);
            ColorTable14.TabIndex = 31;
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
            // ColorTable09
            // 
            ColorTable09.AllowDrop = true;
            ColorTable09.BackColor = Color.FromArgb(59, 120, 255);
            ColorTable09.DefaultColor = Color.FromArgb(59, 120, 255);
            ColorTable09.DontShowInfo = false;
            ColorTable09.Location = new Point(189, 67);
            ColorTable09.Margin = new Padding(4, 3, 4, 3);
            ColorTable09.Name = "ColorTable09";
            ColorTable09.Size = new Size(105, 25);
            ColorTable09.TabIndex = 21;
            // 
            // ColorTable13
            // 
            ColorTable13.AllowDrop = true;
            ColorTable13.BackColor = Color.FromArgb(180, 0, 158);
            ColorTable13.DefaultColor = Color.FromArgb(180, 0, 158);
            ColorTable13.DontShowInfo = false;
            ColorTable13.Location = new Point(189, 191);
            ColorTable13.Margin = new Padding(4, 3, 4, 3);
            ColorTable13.Name = "ColorTable13";
            ColorTable13.Size = new Size(105, 25);
            ColorTable13.TabIndex = 29;
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
            // ColorTable12
            // 
            ColorTable12.AllowDrop = true;
            ColorTable12.BackColor = Color.FromArgb(231, 72, 86);
            ColorTable12.DefaultColor = Color.FromArgb(231, 72, 86);
            ColorTable12.DontShowInfo = false;
            ColorTable12.Location = new Point(189, 160);
            ColorTable12.Margin = new Padding(4, 3, 4, 3);
            ColorTable12.Name = "ColorTable12";
            ColorTable12.Size = new Size(105, 25);
            ColorTable12.TabIndex = 27;
            // 
            // ColorTable10
            // 
            ColorTable10.AllowDrop = true;
            ColorTable10.BackColor = Color.FromArgb(22, 198, 12);
            ColorTable10.DefaultColor = Color.FromArgb(22, 198, 12);
            ColorTable10.DontShowInfo = false;
            ColorTable10.Location = new Point(189, 98);
            ColorTable10.Margin = new Padding(4, 3, 4, 3);
            ColorTable10.Name = "ColorTable10";
            ColorTable10.Size = new Size(105, 25);
            ColorTable10.TabIndex = 23;
            // 
            // ColorTable11
            // 
            ColorTable11.AllowDrop = true;
            ColorTable11.BackColor = Color.FromArgb(97, 214, 214);
            ColorTable11.DefaultColor = Color.FromArgb(97, 214, 214);
            ColorTable11.DontShowInfo = false;
            ColorTable11.Location = new Point(189, 129);
            ColorTable11.Margin = new Padding(4, 3, 4, 3);
            ColorTable11.Name = "ColorTable11";
            ColorTable11.Size = new Size(105, 25);
            ColorTable11.TabIndex = 25;
            // 
            // GroupBox8
            // 
            GroupBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox8.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox8.Controls.Add(Button4);
            GroupBox8.Controls.Add(PictureBox41);
            GroupBox8.Controls.Add(CMD1);
            GroupBox8.Controls.Add(Label41);
            GroupBox8.Location = new Point(436, 63);
            GroupBox8.Margin = new Padding(4, 3, 4, 3);
            GroupBox8.Name = "GroupBox8";
            GroupBox8.Padding = new Padding(1);
            GroupBox8.Size = new Size(472, 293);
            GroupBox8.TabIndex = 91;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = null;
            Button4.LineColor = Color.FromArgb(0, 66, 119);
            Button4.Location = new Point(234, 7);
            Button4.Name = "Button4";
            Button4.Size = new Size(230, 30);
            Button4.TabIndex = 94;
            Button4.Text = "Open Command Prompt for testing";
            Button4.UseVisualStyleBackColor = false;
            // 
            // PictureBox41
            // 
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(6, 6);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(35, 35);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 4;
            PictureBox41.TabStop = false;
            // 
            // CMD1
            // 
            CMD1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            CMD1.BackColor = Color.Black;
            CMD1.CMD_ColorTable00 = Color.Black;
            CMD1.CMD_ColorTable01 = Color.Empty;
            CMD1.CMD_ColorTable02 = Color.Empty;
            CMD1.CMD_ColorTable03 = Color.Empty;
            CMD1.CMD_ColorTable04 = Color.Empty;
            CMD1.CMD_ColorTable05 = Color.Empty;
            CMD1.CMD_ColorTable06 = Color.Empty;
            CMD1.CMD_ColorTable07 = Color.White;
            CMD1.CMD_ColorTable08 = Color.Empty;
            CMD1.CMD_ColorTable09 = Color.Empty;
            CMD1.CMD_ColorTable10 = Color.Empty;
            CMD1.CMD_ColorTable11 = Color.Empty;
            CMD1.CMD_ColorTable12 = Color.Empty;
            CMD1.CMD_ColorTable13 = Color.Empty;
            CMD1.CMD_ColorTable14 = Color.Empty;
            CMD1.CMD_ColorTable15 = Color.White;
            CMD1.CMD_PopupBackground = 5;
            CMD1.CMD_PopupForeground = 15;
            CMD1.CMD_ScreenColorsBackground = 0;
            CMD1.CMD_ScreenColorsForeground = 7;
            CMD1.CustomTerminal = false;
            CMD1.Font = new Font("Consolas", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CMD1.Location = new Point(4, 45);
            CMD1.Name = "CMD1";
            CMD1.PowerShell = false;
            CMD1.Raster = false;
            CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
            CMD1.Size = new Size(464, 244);
            CMD1.TabIndex = 90;
            // 
            // Label41
            // 
            Label41.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label41.Location = new Point(47, 6);
            Label41.Name = "Label41";
            Label41.Size = new Size(121, 35);
            Label41.TabIndex = 3;
            Label41.Text = "Preview";
            Label41.TextAlign = ContentAlignment.MiddleLeft;
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
            Button1.LineColor = Color.FromArgb(52, 20, 64);
            Button1.Location = new Point(830, 615);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 34);
            Button1.TabIndex = 65;
            Button1.Text = "Load";
            Button1.UseVisualStyleBackColor = false;
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
            TabControl1.Location = new Point(12, 60);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(421, 560);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 200;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(GroupBox2);
            TabPage1.Controls.Add(GroupBox1);
            TabPage1.Location = new Point(104, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(313, 552);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Colors";
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(GroupBox4);
            TabPage2.Location = new Point(104, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(313, 552);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Fonts";
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(Label3);
            TabPage3.Controls.Add(GroupBox34);
            TabPage3.Location = new Point(104, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(313, 552);
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
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(Label5);
            TabPage4.Controls.Add(GroupBox12);
            TabPage4.Location = new Point(104, 4);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(313, 552);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "Tweaks";
            // 
            // FontDialog1
            // 
            FontDialog1.FixedPitchOnly = true;
            FontDialog1.ShowEffects = false;
            // 
            // CMD
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(922, 661);
            Controls.Add(TabControl1);
            Controls.Add(Separator2);
            Controls.Add(GroupBox3);
            Controls.Add(CheckBox1);
            Controls.Add(Button10);
            Controls.Add(Button2);
            Controls.Add(GroupBox8);
            Controls.Add(Button1);
            Cursor = Cursors.Arrow;
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CMD";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Terminals";
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            CMD_PreviewCUR.ResumeLayout(false);
            CMD_PreviewCUR2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            TabPage2.ResumeLayout(false);
            TabPage3.ResumeLayout(false);
            TabPage4.ResumeLayout(false);
            Load += new EventHandler(CMD_Load);
            Shown += new EventHandler(CMD_Shown);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.Controllers.ColorItem ColorTable00;
        internal Label Label7;
        internal UI.Controllers.ColorItem ColorTable01;
        internal UI.Controllers.ColorItem ColorTable03;
        internal UI.Controllers.ColorItem ColorTable02;
        internal UI.Controllers.ColorItem ColorTable07;
        internal UI.Controllers.ColorItem ColorTable06;
        internal UI.Controllers.ColorItem ColorTable05;
        internal UI.Controllers.ColorItem ColorTable04;
        internal UI.Controllers.ColorItem ColorTable15;
        internal UI.Controllers.ColorItem ColorTable14;
        internal UI.Controllers.ColorItem ColorTable13;
        internal UI.Controllers.ColorItem ColorTable12;
        internal UI.Controllers.ColorItem ColorTable11;
        internal UI.Controllers.ColorItem ColorTable10;
        internal UI.Controllers.ColorItem ColorTable09;
        internal UI.Controllers.ColorItem ColorTable08;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button10;
        internal Label Label17;
        internal UI.WP.Trackbar CMD_PopupForegroundBar;
        internal UI.WP.Trackbar CMD_PopupBackgroundBar;
        internal Label Label18;
        internal UI.WP.GroupBox GroupBox1;
        internal Label Label31;
        internal Label Label32;
        internal Label Label33;
        internal Label Label34;
        internal Label Label27;
        internal Label Label28;
        internal Label Label29;
        internal Label Label30;
        internal Label Label23;
        internal Label Label24;
        internal Label Label25;
        internal Label Label26;
        internal Label Label22;
        internal Label Label21;
        internal Label Label20;
        internal Label Label19;
        internal Label CMD_PopupForegroundLbl;
        internal Label CMD_PopupBackgroundLbl;
        internal UI.Simulation.WinCMD CMD1;
        internal UI.WP.GroupBox GroupBox8;
        internal PictureBox PictureBox41;
        internal Label Label41;
        internal Label CMD_AccentForegroundLbl;
        internal Label CMD_AccentBackgroundLbl;
        internal Label Label6;
        internal UI.WP.Trackbar CMD_AccentBackgroundBar;
        internal Label Label49;
        internal Label Label50;
        internal UI.WP.Trackbar CMD_AccentForegroundBar;
        internal UI.WP.GroupBox GroupBox4;
        internal Label Label59;
        internal UI.WP.Toggle CMD_RasterToggle;
        internal Label Label58;
        internal UI.WP.Button Button4;
        internal UI.WP.ComboBox CMD_FontWeightBox;
        internal UI.WP.Trackbar CMD_FontSizeBar;
        internal UI.WP.GroupBox GroupBox34;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.Trackbar CMD_CursorSizeBar;
        internal Panel CMD_PreviewCUR;
        internal Panel CMD_PreviewCUR2;
        internal Label Label61;
        internal Panel CMD_PreviewCursorInner;
        internal Label Label2;
        internal Label Label1;
        internal UI.Controllers.ColorItem CMD_CursorColor;
        internal UI.WP.ComboBox CMD_CursorStyle;
        internal Label Label5;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.CheckBox CMD_TerminalScrolling;
        internal UI.WP.CheckBox CMD_LineSelection;
        internal UI.WP.Trackbar CMD_OpacityBar;
        internal Label Label57;
        internal UI.WP.CheckBox CMD_EnhancedTerminal;
        internal UI.WP.GroupBox GroupBox2;
        internal Label Label60;
        internal Label Label35;
        internal UI.WP.SeparatorH Separator1;
        internal ImageList ImageList1;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button6;
        internal UI.WP.Toggle CMDEnabled;
        internal PictureBox checker_img;
        internal Label Label4;
        internal OpenFileDialog OpenWPTHDlg;
        internal UI.WP.SeparatorH Separator2;
        internal UI.WP.ComboBox RasterList;
        internal UI.WP.Button Button25;
        internal UI.WP.Button Button3;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal PictureBox PictureBox6;
        internal PictureBox PictureBox4;
        internal PictureBox PictureBox3;
        internal PictureBox PictureBox1;
        internal TabPage TabPage3;
        internal PictureBox PictureBox15;
        internal PictureBox PictureBox10;
        internal PictureBox PictureBox13;
        internal PictureBox PictureBox12;
        internal PictureBox PictureBox11;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox9;
        internal PictureBox PictureBox8;
        internal PictureBox PictureBox14;
        internal Label Label3;
        internal TabPage TabPage4;
        internal UI.WP.Button CMD_FontSizeVal;
        internal UI.WP.Button CMD_PreviewCUR_Val;
        internal UI.WP.Button CMD_OpacityVal;
        internal Label FontName;
        internal UI.WP.Button Button5;
        internal FontDialog FontDialog1;
    }
}