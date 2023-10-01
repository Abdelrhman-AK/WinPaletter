using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class WindowsTerminal : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsTerminal));
            ImgDlg = new OpenFileDialog();
            SaveJSONDlg = new SaveFileDialog();
            OpenWPTHDlg = new OpenFileDialog();
            OpenJSONDlg = new OpenFileDialog();
            Separator1 = new UI.WP.SeparatorH();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            AlertBox1 = new UI.WP.AlertBox();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            TerEnabled = new UI.WP.Toggle();
            TerEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(TerEnabled_CheckedChanged);
            Button21 = new UI.WP.Button();
            Button21.Click += new EventHandler(Button21_Click);
            Button19 = new UI.WP.Button();
            Button19.Click += new EventHandler(Button19_Click);
            TerEditThemeName = new UI.WP.Button();
            TerEditThemeName.Click += new EventHandler(TerEditThemeName_Click);
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            TerThemes = new UI.WP.ComboBox();
            TerThemes.SelectedIndexChanged += new EventHandler(TerThemes_SelectedIndexChanged);
            TerThemesContainer = new Panel();
            TerMode = new UI.WP.Toggle();
            TerMode.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(TerMode_CheckedChanged);
            Label1 = new Label();
            PictureBox7 = new PictureBox();
            TerTitlebarActive = new UI.Controllers.ColorItem();
            TerTitlebarActive.Click += new EventHandler(ColorMainsClick);
            TerTitlebarActive.DragDrop += new DragEventHandler(ColorMainsClick);
            TerTitlebarInactive = new UI.Controllers.ColorItem();
            TerTitlebarInactive.Click += new EventHandler(ColorMainsClick);
            TerTitlebarInactive.DragDrop += new DragEventHandler(ColorMainsClick);
            PictureBox3 = new PictureBox();
            Label6 = new Label();
            TerTabActive = new UI.Controllers.ColorItem();
            TerTabActive.Click += new EventHandler(ColorMainsClick);
            TerTabActive.DragDrop += new DragEventHandler(ColorMainsClick);
            TerTabInactive = new UI.Controllers.ColorItem();
            TerTabInactive.Click += new EventHandler(ColorMainsClick);
            TerTabInactive.DragDrop += new DragEventHandler(ColorMainsClick);
            PictureBox4 = new PictureBox();
            PictureBox2 = new PictureBox();
            PictureBox1 = new PictureBox();
            Label5 = new Label();
            Label10 = new Label();
            Label9 = new Label();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            GroupBox22 = new UI.WP.GroupBox();
            Terminal1 = new UI.Simulation.WinTerminal();
            Button15 = new UI.WP.Button();
            Button15.Click += new EventHandler(Button15_Click);
            PictureBox27 = new PictureBox();
            Label141 = new Label();
            Terminal2 = new UI.Simulation.WinTerminal();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            TerOpacityBar = new UI.WP.Trackbar();
            TerOpacityBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(TerAcrylicBar_Scroll);
            PictureBox40 = new PictureBox();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            TerAcrylic = new UI.WP.CheckBox();
            TerAcrylic.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(TerAcrylic_CheckedChanged);
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click);
            TerBackImage = new UI.WP.TextBox();
            TerBackImage.TextChanged += new EventHandler(TerBackImage_TextChanged);
            TerImageOpacity = new UI.WP.Trackbar();
            TerImageOpacity.Scroll += new UI.WP.Trackbar.ScrollEventHandler(TerImageOpacity_Scroll);
            Label2 = new Label();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            Button20 = new UI.WP.Button();
            Button20.Click += new EventHandler(Button20_Click);
            Button17 = new UI.WP.Button();
            Button17.Click += new EventHandler(Button17_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            TerSchemes = new UI.WP.ComboBox();
            TerSchemes.SelectedIndexChanged += new EventHandler(TerSchemes_SelectedIndexChanged);
            TerCursor = new UI.Controllers.ColorItem();
            TerCursor.Click += new EventHandler(ColorMainsClick);
            TerCursor.DragDrop += new DragEventHandler(ColorMainsClick);
            TerWhiteB = new UI.Controllers.ColorItem();
            TerWhiteB.Click += new EventHandler(ColorClick);
            TerWhiteB.DragDrop += new DragEventHandler(ColorClick);
            Label150 = new Label();
            TerBlue = new UI.Controllers.ColorItem();
            TerBlue.Click += new EventHandler(ColorClick);
            TerBlue.DragDrop += new DragEventHandler(ColorClick);
            Label151 = new Label();
            TerSelection = new UI.Controllers.ColorItem();
            TerSelection.Click += new EventHandler(ColorMainsClick);
            TerSelection.DragDrop += new DragEventHandler(ColorMainsClick);
            Label152 = new Label();
            Label147 = new Label();
            TerWhite = new UI.Controllers.ColorItem();
            TerWhite.Click += new EventHandler(ColorClick);
            TerWhite.DragDrop += new DragEventHandler(ColorClick);
            TerForeground = new UI.Controllers.ColorItem();
            TerForeground.Click += new EventHandler(ColorMainsClick);
            TerForeground.DragDrop += new DragEventHandler(ColorMainsClick);
            TerCyanB = new UI.Controllers.ColorItem();
            TerCyanB.Click += new EventHandler(ColorClick);
            TerCyanB.DragDrop += new DragEventHandler(ColorClick);
            Label145 = new Label();
            TerCyan = new UI.Controllers.ColorItem();
            TerCyan.Click += new EventHandler(ColorClick);
            TerCyan.DragDrop += new DragEventHandler(ColorClick);
            TerGreen = new UI.Controllers.ColorItem();
            TerGreen.Click += new EventHandler(ColorClick);
            TerGreen.DragDrop += new DragEventHandler(ColorClick);
            Label144 = new Label();
            TerBackground = new UI.Controllers.ColorItem();
            TerBackground.Click += new EventHandler(ColorMainsClick);
            TerBackground.DragDrop += new DragEventHandler(ColorMainsClick);
            TerYellow = new UI.Controllers.ColorItem();
            TerYellow.Click += new EventHandler(ColorClick);
            TerYellow.DragDrop += new DragEventHandler(ColorClick);
            TerGreenB = new UI.Controllers.ColorItem();
            TerGreenB.Click += new EventHandler(ColorClick);
            TerGreenB.DragDrop += new DragEventHandler(ColorClick);
            Label143 = new Label();
            Label149 = new Label();
            TerBlack = new UI.Controllers.ColorItem();
            TerBlack.Click += new EventHandler(ColorClick);
            TerBlack.DragDrop += new DragEventHandler(ColorClick);
            TerYellowB = new UI.Controllers.ColorItem();
            TerYellowB.Click += new EventHandler(ColorClick);
            TerYellowB.DragDrop += new DragEventHandler(ColorClick);
            Label142 = new Label();
            TerBlackB = new UI.Controllers.ColorItem();
            TerBlackB.Click += new EventHandler(ColorClick);
            TerBlackB.DragDrop += new DragEventHandler(ColorClick);
            TerPurple = new UI.Controllers.ColorItem();
            TerPurple.Click += new EventHandler(ColorClick);
            TerPurple.DragDrop += new DragEventHandler(ColorClick);
            Label140 = new Label();
            TerPurpleB = new UI.Controllers.ColorItem();
            TerPurpleB.Click += new EventHandler(ColorClick);
            TerPurpleB.DragDrop += new DragEventHandler(ColorClick);
            TerBlueB = new UI.Controllers.ColorItem();
            TerBlueB.Click += new EventHandler(ColorClick);
            TerBlueB.DragDrop += new DragEventHandler(ColorClick);
            Label146 = new Label();
            Label148 = new Label();
            TerRedB = new UI.Controllers.ColorItem();
            TerRedB.Click += new EventHandler(ColorClick);
            TerRedB.DragDrop += new DragEventHandler(ColorClick);
            TerRed = new UI.Controllers.ColorItem();
            TerRed.Click += new EventHandler(ColorClick);
            TerRed.DragDrop += new DragEventHandler(ColorClick);
            TerCursorHeightBar = new UI.WP.Trackbar();
            TerCursorHeightBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(TerCursorHeightBar_Scroll);
            TerCursorStyle = new UI.WP.ComboBox();
            TerCursorStyle.SelectedIndexChanged += new EventHandler(TerCursorStyle_SelectedIndexChanged);
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            GroupBox13 = new UI.WP.GroupBox();
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button18 = new UI.WP.Button();
            Button18.Click += new EventHandler(Button18_Click);
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click);
            PictureBox25 = new PictureBox();
            Label155 = new Label();
            TerProfiles = new UI.WP.ComboBox();
            TerProfiles.SelectedIndexChanged += new EventHandler(TerProfiles_SelectedIndexChanged);
            Button13 = new UI.WP.Button();
            Button13.Click += new EventHandler(Button13_Click);
            TerFontSizeBar = new UI.WP.Trackbar();
            TerFontSizeBar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(TerFontSizeBar_Scroll);
            TerFontWeight = new UI.WP.ComboBox();
            TerFontWeight.SelectedIndexChanged += new EventHandler(TerFontWeight_SelectedIndexChanged);
            GroupBox3 = new UI.WP.GroupBox();
            Button22 = new UI.WP.Button();
            Button22.Click += new EventHandler(Button22_Click);
            Separator2 = new UI.WP.SeparatorH();
            Label11 = new Label();
            checker_img = new PictureBox();
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            GroupBox4 = new UI.WP.GroupBox();
            PictureBox29 = new PictureBox();
            PictureBox30 = new PictureBox();
            PictureBox31 = new PictureBox();
            PictureBox34 = new PictureBox();
            PictureBox26 = new PictureBox();
            PictureBox28 = new PictureBox();
            PictureBox23 = new PictureBox();
            PictureBox22 = new PictureBox();
            Separator3 = new UI.WP.SeparatorH();
            PictureBox21 = new PictureBox();
            PictureBox20 = new PictureBox();
            PictureBox19 = new PictureBox();
            PictureBox15 = new PictureBox();
            PictureBox14 = new PictureBox();
            Label19 = new Label();
            TabPage5 = new TabPage();
            GroupBox2 = new UI.WP.GroupBox();
            PictureBox37 = new PictureBox();
            Label20 = new Label();
            TabPage2 = new TabPage();
            GroupBox5 = new UI.WP.GroupBox();
            TerFontName = new Label();
            Button23 = new UI.WP.Button();
            Button23.Click += new EventHandler(Button23_Click);
            TerFontSizeVal = new UI.WP.Button();
            TerFontSizeVal.Click += new EventHandler(TerFontSizeVal_Click);
            PictureBox5 = new PictureBox();
            Label61 = new Label();
            PictureBox8 = new PictureBox();
            Label35 = new Label();
            PictureBox9 = new PictureBox();
            Label59 = new Label();
            TabPage3 = new TabPage();
            GroupBox34 = new UI.WP.GroupBox();
            PictureBox10 = new PictureBox();
            TerCursorHeightVal = new UI.WP.Button();
            TerCursorHeightVal.Click += new EventHandler(TerCursorHeightVal_Click);
            Label60 = new Label();
            PictureBox11 = new PictureBox();
            Label14 = new Label();
            TabPage4 = new TabPage();
            GroupBox12 = new UI.WP.GroupBox();
            TerOpacityVal = new UI.WP.Button();
            TerOpacityVal.Click += new EventHandler(TerOpacityVal_Click);
            TerImageOpacityVal = new UI.WP.Button();
            TerImageOpacityVal.Click += new EventHandler(TerImageOpacityVal_Click);
            PictureBox13 = new PictureBox();
            PictureBox16 = new PictureBox();
            Label57 = new Label();
            FontDialog1 = new FontDialog();
            TerThemesContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            GroupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).BeginInit();
            GroupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            TabPage5.SuspendLayout();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox37).BeginInit();
            TabPage2.SuspendLayout();
            GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            TabPage3.SuspendLayout();
            GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            TabPage4.SuspendLayout();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            SuspendLayout();
            // 
            // ImgDlg
            // 
            ImgDlg.Filter = "Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All Files (*.*)|*.*";
            // 
            // SaveJSONDlg
            // 
            SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*";
            // 
            // OpenWPTHDlg
            // 
            OpenWPTHDlg.Filter = "WinPaletter Theme File (*.wpth)|*.wpth|All files (*.*)|*.*";
            // 
            // OpenJSONDlg
            // 
            OpenJSONDlg.Filter = "JSON File (*.json)|*.json";
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 98);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(984, 1);
            Separator1.TabIndex = 198;
            Separator1.TabStop = false;
            Separator1.Text = "Separator1";
            // 
            // Button11
            // 
            Button11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button11.BackColor = Color.FromArgb(43, 43, 43);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = null;
            Button11.LineColor = Color.FromArgb(58, 150, 221);
            Button11.Location = new Point(609, 46);
            Button11.Name = "Button11";
            Button11.Size = new Size(210, 30);
            Button11.TabIndex = 200;
            Button11.Text = "Open \"Settings.json\" in editor";
            Button11.UseVisualStyleBackColor = false;
            // 
            // Button9
            // 
            Button9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = null;
            Button9.LineColor = Color.FromArgb(58, 150, 221);
            Button9.Location = new Point(823, 46);
            Button9.Name = "Button9";
            Button9.Size = new Size(154, 30);
            Button9.TabIndex = 199;
            Button9.Text = "Backup \"Settings.json\"";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.BackColor = Color.FromArgb(43, 43, 43);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleRight;
            Button8.LineColor = Color.FromArgb(113, 122, 131);
            Button8.Location = new Point(85, 6);
            Button8.Name = "Button8";
            Button8.Size = new Size(136, 29);
            Button8.TabIndex = 110;
            Button8.Text = "WinPaletter theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(68, 50, 2);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = (Image)resources.GetObject("AlertBox1.Image");
            AlertBox1.Location = new Point(4, 46);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(600, 30);
            AlertBox1.TabIndex = 198;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "You should create a backup to Terminal settings file \"settings.json\" to avoid und" + "esired actions or errors.";
            // 
            // Button7
            // 
            Button7.BackColor = Color.FromArgb(43, 43, 43);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = (Image)resources.GetObject("Button7.Image");
            Button7.ImageAlign = ContentAlignment.MiddleRight;
            Button7.LineColor = Color.FromArgb(159, 160, 153);
            Button7.Location = new Point(371, 6);
            Button7.Name = "Button7";
            Button7.Size = new Size(85, 29);
            Button7.TabIndex = 109;
            Button7.Text = "JSON file";
            Button7.UseVisualStyleBackColor = false;
            // 
            // TerEnabled
            // 
            TerEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerEnabled.BackColor = Color.FromArgb(43, 43, 43);
            TerEnabled.Checked = false;
            TerEnabled.DarkLight_Toggler = false;
            TerEnabled.Location = new Point(934, 10);
            TerEnabled.Name = "TerEnabled";
            TerEnabled.Size = new Size(40, 20);
            TerEnabled.TabIndex = 85;
            // 
            // Button21
            // 
            Button21.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button21.BackColor = Color.FromArgb(43, 43, 43);
            Button21.DrawOnGlass = false;
            Button21.Font = new Font("Segoe UI", 9.0f);
            Button21.ForeColor = Color.White;
            Button21.Image = (Image)resources.GetObject("Button21.Image");
            Button21.ImageAlign = ContentAlignment.MiddleLeft;
            Button21.LineColor = Color.FromArgb(34, 114, 139);
            Button21.Location = new Point(108, 6);
            Button21.Name = "Button21";
            Button21.Size = new Size(87, 24);
            Button21.TabIndex = 218;
            Button21.Text = "Copycat";
            Button21.UseVisualStyleBackColor = false;
            // 
            // Button19
            // 
            Button19.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button19.BackColor = Color.FromArgb(43, 43, 43);
            Button19.DrawOnGlass = false;
            Button19.Font = new Font("Segoe UI", 9.0f);
            Button19.ForeColor = Color.White;
            Button19.Image = (Image)resources.GetObject("Button19.Image");
            Button19.ImageAlign = ContentAlignment.MiddleLeft;
            Button19.LineColor = Color.FromArgb(37, 123, 145);
            Button19.Location = new Point(197, 6);
            Button19.Name = "Button19";
            Button19.Size = new Size(76, 24);
            Button19.TabIndex = 216;
            Button19.Text = "Clone";
            Button19.UseVisualStyleBackColor = false;
            // 
            // TerEditThemeName
            // 
            TerEditThemeName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerEditThemeName.BackColor = Color.FromArgb(43, 43, 43);
            TerEditThemeName.DrawOnGlass = false;
            TerEditThemeName.Font = new Font("Segoe UI", 9.0f);
            TerEditThemeName.ForeColor = Color.White;
            TerEditThemeName.Image = (Image)resources.GetObject("TerEditThemeName.Image");
            TerEditThemeName.LineColor = Color.FromArgb(56, 46, 14);
            TerEditThemeName.Location = new Point(275, 6);
            TerEditThemeName.Name = "TerEditThemeName";
            TerEditThemeName.Size = new Size(35, 24);
            TerEditThemeName.TabIndex = 212;
            TerEditThemeName.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.LineColor = Color.FromArgb(28, 103, 64);
            Button3.Location = new Point(312, 6);
            Button3.Name = "Button3";
            Button3.Size = new Size(35, 24);
            Button3.TabIndex = 211;
            Button3.UseVisualStyleBackColor = false;
            // 
            // TerThemes
            // 
            TerThemes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerThemes.BackColor = Color.FromArgb(55, 55, 55);
            TerThemes.DrawMode = DrawMode.OwnerDrawFixed;
            TerThemes.DropDownStyle = ComboBoxStyle.DropDownList;
            TerThemes.Font = new Font("Segoe UI", 9.0f);
            TerThemes.ForeColor = Color.White;
            TerThemes.FormattingEnabled = true;
            TerThemes.ItemHeight = 20;
            TerThemes.Location = new Point(5, 33);
            TerThemes.Name = "TerThemes";
            TerThemes.Size = new Size(342, 26);
            TerThemes.TabIndex = 210;
            // 
            // TerThemesContainer
            // 
            TerThemesContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TerThemesContainer.Controls.Add(TerMode);
            TerThemesContainer.Controls.Add(Label1);
            TerThemesContainer.Controls.Add(PictureBox7);
            TerThemesContainer.Controls.Add(TerTitlebarActive);
            TerThemesContainer.Controls.Add(TerTitlebarInactive);
            TerThemesContainer.Controls.Add(PictureBox3);
            TerThemesContainer.Controls.Add(Label6);
            TerThemesContainer.Controls.Add(TerTabActive);
            TerThemesContainer.Controls.Add(TerTabInactive);
            TerThemesContainer.Controls.Add(PictureBox4);
            TerThemesContainer.Controls.Add(PictureBox2);
            TerThemesContainer.Controls.Add(PictureBox1);
            TerThemesContainer.Controls.Add(Label5);
            TerThemesContainer.Controls.Add(Label10);
            TerThemesContainer.Controls.Add(Label9);
            TerThemesContainer.Enabled = false;
            TerThemesContainer.Location = new Point(3, 60);
            TerThemesContainer.Name = "TerThemesContainer";
            TerThemesContainer.Size = new Size(344, 149);
            TerThemesContainer.TabIndex = 213;
            // 
            // TerMode
            // 
            TerMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerMode.BackColor = Color.FromArgb(43, 43, 43);
            TerMode.Checked = false;
            TerMode.DarkLight_Toggler = true;
            TerMode.Location = new Point(302, 123);
            TerMode.Name = "TerMode";
            TerMode.Size = new Size(40, 20);
            TerMode.TabIndex = 209;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(33, 123);
            Label1.Name = "Label1";
            Label1.Size = new Size(253, 24);
            Label1.TabIndex = 224;
            Label1.Text = @"Dark\Light mode";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(3, 123);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 223;
            PictureBox7.TabStop = false;
            // 
            // TerTitlebarActive
            // 
            TerTitlebarActive.AllowDrop = true;
            TerTitlebarActive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerTitlebarActive.BackColor = Color.FromArgb(12, 12, 12);
            TerTitlebarActive.DefaultColor = Color.FromArgb(12, 12, 12);
            TerTitlebarActive.DontShowInfo = false;
            TerTitlebarActive.Location = new Point(242, 3);
            TerTitlebarActive.Margin = new Padding(4, 3, 4, 3);
            TerTitlebarActive.Name = "TerTitlebarActive";
            TerTitlebarActive.Size = new Size(100, 24);
            TerTitlebarActive.TabIndex = 198;
            // 
            // TerTitlebarInactive
            // 
            TerTitlebarInactive.AllowDrop = true;
            TerTitlebarInactive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerTitlebarInactive.BackColor = Color.FromArgb(12, 12, 12);
            TerTitlebarInactive.DefaultColor = Color.FromArgb(12, 12, 12);
            TerTitlebarInactive.DontShowInfo = false;
            TerTitlebarInactive.Location = new Point(242, 33);
            TerTitlebarInactive.Margin = new Padding(4, 3, 4, 3);
            TerTitlebarInactive.Name = "TerTitlebarInactive";
            TerTitlebarInactive.Size = new Size(100, 24);
            TerTitlebarInactive.TabIndex = 200;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(3, 93);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 222;
            PictureBox3.TabStop = false;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.Location = new Point(33, 3);
            Label6.Name = "Label6";
            Label6.Size = new Size(202, 24);
            Label6.TabIndex = 197;
            Label6.Text = "Active title";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerTabActive
            // 
            TerTabActive.AllowDrop = true;
            TerTabActive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerTabActive.BackColor = Color.FromArgb(12, 12, 12);
            TerTabActive.DefaultColor = Color.FromArgb(12, 12, 12);
            TerTabActive.DontShowInfo = false;
            TerTabActive.Location = new Point(242, 63);
            TerTabActive.Margin = new Padding(4, 3, 4, 3);
            TerTabActive.Name = "TerTabActive";
            TerTabActive.Size = new Size(100, 24);
            TerTabActive.TabIndex = 203;
            // 
            // TerTabInactive
            // 
            TerTabInactive.AllowDrop = true;
            TerTabInactive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerTabInactive.BackColor = Color.FromArgb(12, 12, 12);
            TerTabInactive.DefaultColor = Color.FromArgb(12, 12, 12);
            TerTabInactive.DontShowInfo = false;
            TerTabInactive.Location = new Point(242, 93);
            TerTabInactive.Margin = new Padding(4, 3, 4, 3);
            TerTabInactive.Name = "TerTabInactive";
            TerTabInactive.Size = new Size(100, 24);
            TerTabInactive.TabIndex = 205;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(3, 63);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 221;
            PictureBox4.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(3, 33);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 220;
            PictureBox2.TabStop = false;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 219;
            PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.BackColor = Color.Transparent;
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5.Location = new Point(33, 33);
            Label5.Name = "Label5";
            Label5.Size = new Size(202, 24);
            Label5.TabIndex = 199;
            Label5.Text = "Inactive title";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            Label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label10.BackColor = Color.Transparent;
            Label10.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label10.Location = new Point(33, 63);
            Label10.Name = "Label10";
            Label10.Size = new Size(202, 24);
            Label10.TabIndex = 202;
            Label10.Text = "Active tab";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label9.BackColor = Color.Transparent;
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label9.Location = new Point(33, 93);
            Label9.Name = "Label9";
            Label9.Size = new Size(202, 24);
            Label9.TabIndex = 204;
            Label9.Text = "Inactive tab";
            Label9.TextAlign = ContentAlignment.MiddleLeft;
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
            Button2.Location = new Point(700, 600);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 106;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // GroupBox22
            // 
            GroupBox22.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox22.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox22.Controls.Add(Terminal1);
            GroupBox22.Controls.Add(Button15);
            GroupBox22.Controls.Add(PictureBox27);
            GroupBox22.Controls.Add(Label141);
            GroupBox22.Controls.Add(Terminal2);
            GroupBox22.Location = new Point(479, 106);
            GroupBox22.Margin = new Padding(4, 3, 4, 3);
            GroupBox22.Name = "GroupBox22";
            GroupBox22.Padding = new Padding(1);
            GroupBox22.Size = new Size(517, 301);
            GroupBox22.TabIndex = 195;
            // 
            // Terminal1
            // 
            Terminal1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Terminal1.BackImage = null;
            Terminal1.Color_Background = Color.Black;
            Terminal1.Color_Cursor = Color.White;
            Terminal1.Color_Foreground = Color.White;
            Terminal1.Color_Selection = Color.Gray;
            Terminal1.Color_TabFocused = Color.FromArgb(0, 0, 0);
            Terminal1.Color_TabUnFocused = Color.FromArgb(250, 0, 0);
            Terminal1.Color_Titlebar = Color.FromArgb(46, 46, 46);
            Terminal1.Color_Titlebar_Unfocused = Color.FromArgb(46, 46, 46);
            Terminal1.CursorHeight = 25;
            Terminal1.CursorType = UI.Simulation.WinTerminal.CursorShape_Enum.bar;
            Terminal1.Font = new Font("Cascadia Mono", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Terminal1.IsFocused = true;
            Terminal1.Light = false;
            Terminal1.Location = new Point(6, 45);
            Terminal1.Name = "Terminal1";
            Terminal1.Opacity = 0.15f;
            Terminal1.OpacityBackImage = 100.0f;
            Terminal1.PreviewVersion = true;
            Terminal1.Size = new Size(483, 226);
            Terminal1.TabColor = Color.FromArgb(0, 0, 0, 0);
            Terminal1.TabIcon = null;
            Terminal1.TabIconButItIsString = "";
            Terminal1.TabIndex = 95;
            Terminal1.TabTitle = "Command Prompt";
            Terminal1.UseAcrylic = false;
            Terminal1.UseAcrylicOnTitlebar = false;
            // 
            // Button15
            // 
            Button15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button15.BackColor = Color.FromArgb(43, 43, 43);
            Button15.DrawOnGlass = false;
            Button15.Font = new Font("Segoe UI", 9.0f);
            Button15.ForeColor = Color.White;
            Button15.Image = null;
            Button15.LineColor = Color.FromArgb(199, 49, 61);
            Button15.Location = new Point(356, 7);
            Button15.Name = "Button15";
            Button15.Size = new Size(154, 30);
            Button15.TabIndex = 94;
            Button15.Text = "Open Terminal for testing";
            Button15.UseVisualStyleBackColor = false;
            // 
            // PictureBox27
            // 
            PictureBox27.Image = (Image)resources.GetObject("PictureBox27.Image");
            PictureBox27.Location = new Point(6, 5);
            PictureBox27.Name = "PictureBox27";
            PictureBox27.Size = new Size(35, 35);
            PictureBox27.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox27.TabIndex = 4;
            PictureBox27.TabStop = false;
            // 
            // Label141
            // 
            Label141.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label141.Location = new Point(47, 5);
            Label141.Name = "Label141";
            Label141.Size = new Size(231, 35);
            Label141.TabIndex = 3;
            Label141.Text = "Preview";
            Label141.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Terminal2
            // 
            Terminal2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Terminal2.BackImage = null;
            Terminal2.Color_Background = Color.Black;
            Terminal2.Color_Cursor = Color.White;
            Terminal2.Color_Foreground = Color.White;
            Terminal2.Color_Selection = Color.Gray;
            Terminal2.Color_TabFocused = Color.FromArgb(0, 0, 0);
            Terminal2.Color_TabUnFocused = Color.FromArgb(250, 0, 0);
            Terminal2.Color_Titlebar = Color.FromArgb(46, 46, 46);
            Terminal2.Color_Titlebar_Unfocused = Color.FromArgb(0, 64, 64);
            Terminal2.CursorHeight = 25;
            Terminal2.CursorType = UI.Simulation.WinTerminal.CursorShape_Enum.bar;
            Terminal2.Font = new Font("Cascadia Mono", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Terminal2.IsFocused = false;
            Terminal2.Light = false;
            Terminal2.Location = new Point(54, 132);
            Terminal2.Name = "Terminal2";
            Terminal2.Opacity = 100.0f;
            Terminal2.OpacityBackImage = 0f;
            Terminal2.PreviewVersion = true;
            Terminal2.Size = new Size(454, 160);
            Terminal2.TabColor = Color.FromArgb(0, 0, 0, 0);
            Terminal2.TabIcon = null;
            Terminal2.TabIconButItIsString = "";
            Terminal2.TabIndex = 96;
            Terminal2.TabTitle = "";
            Terminal2.UseAcrylic = false;
            Terminal2.UseAcrylicOnTitlebar = false;
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
            Button1.Location = new Point(916, 600);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 34);
            Button1.TabIndex = 105;
            Button1.Text = "Load";
            Button1.UseVisualStyleBackColor = false;
            // 
            // TerOpacityBar
            // 
            TerOpacityBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerOpacityBar.LargeChange = 10;
            TerOpacityBar.Location = new Point(107, 94);
            TerOpacityBar.Maximum = 100;
            TerOpacityBar.Minimum = 0;
            TerOpacityBar.Name = "TerOpacityBar";
            TerOpacityBar.Size = new Size(197, 19);
            TerOpacityBar.SmallChange = 1;
            TerOpacityBar.TabIndex = 208;
            TerOpacityBar.Value = 100;
            // 
            // PictureBox40
            // 
            PictureBox40.Image = (Image)resources.GetObject("PictureBox40.Image");
            PictureBox40.Location = new Point(6, 92);
            PictureBox40.Name = "PictureBox40";
            PictureBox40.Size = new Size(24, 24);
            PictureBox40.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox40.TabIndex = 206;
            PictureBox40.TabStop = false;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(43, 43, 43);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = null;
            Button5.LineColor = Color.FromArgb(36, 81, 110);
            Button5.Location = new Point(188, 33);
            Button5.Name = "Button5";
            Button5.Size = new Size(116, 25);
            Button5.TabIndex = 197;
            Button5.Text = "Set as wallpaper";
            Button5.UseVisualStyleBackColor = false;
            // 
            // TerAcrylic
            // 
            TerAcrylic.BackColor = Color.FromArgb(34, 34, 34);
            TerAcrylic.Checked = false;
            TerAcrylic.Font = new Font("Segoe UI", 9.0f);
            TerAcrylic.ForeColor = Color.White;
            TerAcrylic.Location = new Point(35, 91);
            TerAcrylic.Name = "TerAcrylic";
            TerAcrylic.Size = new Size(66, 24);
            TerAcrylic.TabIndex = 207;
            TerAcrylic.Text = "Acrylic Back";
            // 
            // Button16
            // 
            Button16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button16.BackColor = Color.FromArgb(43, 43, 43);
            Button16.DrawOnGlass = false;
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = (Image)resources.GetObject("Button16.Image");
            Button16.LineColor = Color.FromArgb(164, 125, 25);
            Button16.Location = new Point(306, 33);
            Button16.Name = "Button16";
            Button16.Size = new Size(39, 25);
            Button16.TabIndex = 192;
            Button16.UseVisualStyleBackColor = false;
            // 
            // TerBackImage
            // 
            TerBackImage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerBackImage.BackColor = Color.FromArgb(55, 55, 55);
            TerBackImage.DrawOnGlass = false;
            TerBackImage.ForeColor = Color.White;
            TerBackImage.Location = new Point(122, 6);
            TerBackImage.MaxLength = 32767;
            TerBackImage.Multiline = false;
            TerBackImage.Name = "TerBackImage";
            TerBackImage.ReadOnly = false;
            TerBackImage.Scrollbars = ScrollBars.None;
            TerBackImage.SelectedText = "";
            TerBackImage.SelectionLength = 0;
            TerBackImage.SelectionStart = 0;
            TerBackImage.Size = new Size(224, 24);
            TerBackImage.TabIndex = 191;
            TerBackImage.TextAlign = HorizontalAlignment.Left;
            TerBackImage.UseSystemPasswordChar = false;
            TerBackImage.WordWrap = true;
            // 
            // TerImageOpacity
            // 
            TerImageOpacity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerImageOpacity.LargeChange = 10;
            TerImageOpacity.Location = new Point(107, 65);
            TerImageOpacity.Maximum = 100;
            TerImageOpacity.Minimum = 0;
            TerImageOpacity.Name = "TerImageOpacity";
            TerImageOpacity.Size = new Size(197, 19);
            TerImageOpacity.SmallChange = 1;
            TerImageOpacity.TabIndex = 187;
            TerImageOpacity.Value = 100;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(36, 6);
            Label2.Name = "Label2";
            Label2.Size = new Size(80, 24);
            Label2.TabIndex = 196;
            Label2.Text = "Background:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(12, 605);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(332, 24);
            CheckBox1.TabIndex = 104;
            CheckBox1.Text = "Allow non monospace fonts (causes wrong renderering)";
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
            Button20.LineColor = Color.FromArgb(34, 114, 139);
            Button20.Location = new Point(108, 6);
            Button20.Name = "Button20";
            Button20.Size = new Size(87, 24);
            Button20.TabIndex = 217;
            Button20.Text = "Copycat";
            Button20.UseVisualStyleBackColor = false;
            // 
            // Button17
            // 
            Button17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button17.BackColor = Color.FromArgb(43, 43, 43);
            Button17.DrawOnGlass = false;
            Button17.Font = new Font("Segoe UI", 9.0f);
            Button17.ForeColor = Color.White;
            Button17.Image = (Image)resources.GetObject("Button17.Image");
            Button17.ImageAlign = ContentAlignment.MiddleLeft;
            Button17.LineColor = Color.FromArgb(37, 123, 145);
            Button17.Location = new Point(197, 6);
            Button17.Name = "Button17";
            Button17.Size = new Size(76, 24);
            Button17.TabIndex = 214;
            Button17.Text = "Clone";
            Button17.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.LineColor = Color.FromArgb(56, 46, 14);
            Button4.Location = new Point(275, 6);
            Button4.Name = "Button4";
            Button4.Size = new Size(35, 24);
            Button4.TabIndex = 213;
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button12.BackColor = Color.FromArgb(43, 43, 43);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = (Image)resources.GetObject("Button12.Image");
            Button12.LineColor = Color.FromArgb(28, 103, 64);
            Button12.Location = new Point(312, 6);
            Button12.Name = "Button12";
            Button12.Size = new Size(35, 24);
            Button12.TabIndex = 129;
            Button12.UseVisualStyleBackColor = false;
            // 
            // TerSchemes
            // 
            TerSchemes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerSchemes.BackColor = Color.FromArgb(55, 55, 55);
            TerSchemes.DrawMode = DrawMode.OwnerDrawFixed;
            TerSchemes.DropDownStyle = ComboBoxStyle.DropDownList;
            TerSchemes.Font = new Font("Segoe UI", 9.0f);
            TerSchemes.ForeColor = Color.White;
            TerSchemes.FormattingEnabled = true;
            TerSchemes.ItemHeight = 20;
            TerSchemes.Location = new Point(5, 33);
            TerSchemes.Name = "TerSchemes";
            TerSchemes.Size = new Size(342, 26);
            TerSchemes.TabIndex = 117;
            // 
            // TerCursor
            // 
            TerCursor.AllowDrop = true;
            TerCursor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerCursor.BackColor = Color.FromArgb(12, 12, 12);
            TerCursor.DefaultColor = Color.FromArgb(12, 12, 12);
            TerCursor.DontShowInfo = false;
            TerCursor.Location = new Point(245, 153);
            TerCursor.Margin = new Padding(4, 3, 4, 3);
            TerCursor.Name = "TerCursor";
            TerCursor.Size = new Size(100, 24);
            TerCursor.TabIndex = 125;
            // 
            // TerWhiteB
            // 
            TerWhiteB.AllowDrop = true;
            TerWhiteB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerWhiteB.BackColor = Color.FromArgb(242, 242, 242);
            TerWhiteB.DefaultColor = Color.FromArgb(242, 242, 242);
            TerWhiteB.DontShowInfo = false;
            TerWhiteB.Location = new Point(245, 401);
            TerWhiteB.Margin = new Padding(4, 3, 4, 3);
            TerWhiteB.Name = "TerWhiteB";
            TerWhiteB.Size = new Size(100, 24);
            TerWhiteB.TabIndex = 115;
            // 
            // Label150
            // 
            Label150.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label150.BackColor = Color.Transparent;
            Label150.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label150.Location = new Point(36, 153);
            Label150.Name = "Label150";
            Label150.Size = new Size(202, 24);
            Label150.TabIndex = 124;
            Label150.Text = "Cursor";
            Label150.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerBlue
            // 
            TerBlue.AllowDrop = true;
            TerBlue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerBlue.BackColor = Color.FromArgb(0, 55, 218);
            TerBlue.DefaultColor = Color.FromArgb(0, 55, 218);
            TerBlue.DontShowInfo = false;
            TerBlue.Location = new Point(140, 222);
            TerBlue.Margin = new Padding(4, 3, 4, 3);
            TerBlue.Name = "TerBlue";
            TerBlue.Size = new Size(100, 24);
            TerBlue.TabIndex = 101;
            // 
            // Label151
            // 
            Label151.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label151.BackColor = Color.Transparent;
            Label151.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label151.Location = new Point(36, 401);
            Label151.Name = "Label151";
            Label151.Size = new Size(97, 24);
            Label151.TabIndex = 91;
            Label151.Text = "White";
            Label151.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerSelection
            // 
            TerSelection.AllowDrop = true;
            TerSelection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerSelection.BackColor = Color.FromArgb(12, 12, 12);
            TerSelection.DefaultColor = Color.FromArgb(12, 12, 12);
            TerSelection.DontShowInfo = false;
            TerSelection.Location = new Point(245, 123);
            TerSelection.Margin = new Padding(4, 3, 4, 3);
            TerSelection.Name = "TerSelection";
            TerSelection.Size = new Size(100, 24);
            TerSelection.TabIndex = 123;
            // 
            // Label152
            // 
            Label152.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label152.BackColor = Color.Transparent;
            Label152.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label152.Location = new Point(36, 123);
            Label152.Name = "Label152";
            Label152.Size = new Size(202, 24);
            Label152.TabIndex = 122;
            Label152.Text = "Selection";
            Label152.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label147
            // 
            Label147.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label147.BackColor = Color.Transparent;
            Label147.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label147.Location = new Point(36, 93);
            Label147.Name = "Label147";
            Label147.Size = new Size(202, 24);
            Label147.TabIndex = 120;
            Label147.Text = "Foreground";
            Label147.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerWhite
            // 
            TerWhite.AllowDrop = true;
            TerWhite.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerWhite.BackColor = Color.FromArgb(204, 204, 204);
            TerWhite.DefaultColor = Color.FromArgb(204, 204, 204);
            TerWhite.DontShowInfo = false;
            TerWhite.Location = new Point(140, 401);
            TerWhite.Margin = new Padding(4, 3, 4, 3);
            TerWhite.Name = "TerWhite";
            TerWhite.Size = new Size(100, 24);
            TerWhite.TabIndex = 107;
            // 
            // TerForeground
            // 
            TerForeground.AllowDrop = true;
            TerForeground.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerForeground.BackColor = Color.FromArgb(12, 12, 12);
            TerForeground.DefaultColor = Color.FromArgb(12, 12, 12);
            TerForeground.DontShowInfo = false;
            TerForeground.Location = new Point(245, 93);
            TerForeground.Margin = new Padding(4, 3, 4, 3);
            TerForeground.Name = "TerForeground";
            TerForeground.Size = new Size(100, 24);
            TerForeground.TabIndex = 121;
            // 
            // TerCyanB
            // 
            TerCyanB.AllowDrop = true;
            TerCyanB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerCyanB.BackColor = Color.FromArgb(97, 214, 214);
            TerCyanB.DefaultColor = Color.FromArgb(97, 214, 214);
            TerCyanB.DontShowInfo = false;
            TerCyanB.Location = new Point(245, 251);
            TerCyanB.Margin = new Padding(4, 3, 4, 3);
            TerCyanB.Name = "TerCyanB";
            TerCyanB.Size = new Size(100, 24);
            TerCyanB.TabIndex = 111;
            // 
            // Label145
            // 
            Label145.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label145.BackColor = Color.Transparent;
            Label145.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label145.Location = new Point(36, 251);
            Label145.Name = "Label145";
            Label145.Size = new Size(97, 24);
            Label145.TabIndex = 87;
            Label145.Text = "Cyan";
            Label145.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerCyan
            // 
            TerCyan.AllowDrop = true;
            TerCyan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerCyan.BackColor = Color.FromArgb(58, 150, 221);
            TerCyan.DefaultColor = Color.FromArgb(58, 150, 221);
            TerCyan.DontShowInfo = false;
            TerCyan.Location = new Point(140, 251);
            TerCyan.Margin = new Padding(4, 3, 4, 3);
            TerCyan.Name = "TerCyan";
            TerCyan.Size = new Size(100, 24);
            TerCyan.TabIndex = 103;
            // 
            // TerGreen
            // 
            TerGreen.AllowDrop = true;
            TerGreen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerGreen.BackColor = Color.FromArgb(19, 161, 14);
            TerGreen.DefaultColor = Color.FromArgb(19, 161, 14);
            TerGreen.DontShowInfo = false;
            TerGreen.Location = new Point(140, 281);
            TerGreen.Margin = new Padding(4, 3, 4, 3);
            TerGreen.Name = "TerGreen";
            TerGreen.Size = new Size(100, 24);
            TerGreen.TabIndex = 102;
            // 
            // Label144
            // 
            Label144.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label144.BackColor = Color.Transparent;
            Label144.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label144.Location = new Point(36, 281);
            Label144.Name = "Label144";
            Label144.Size = new Size(97, 24);
            Label144.TabIndex = 86;
            Label144.Text = "Green";
            Label144.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerBackground
            // 
            TerBackground.AllowDrop = true;
            TerBackground.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerBackground.BackColor = Color.FromArgb(12, 12, 12);
            TerBackground.DefaultColor = Color.FromArgb(12, 12, 12);
            TerBackground.DontShowInfo = false;
            TerBackground.Location = new Point(245, 63);
            TerBackground.Margin = new Padding(4, 3, 4, 3);
            TerBackground.Name = "TerBackground";
            TerBackground.Size = new Size(100, 24);
            TerBackground.TabIndex = 119;
            // 
            // TerYellow
            // 
            TerYellow.AllowDrop = true;
            TerYellow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerYellow.BackColor = Color.FromArgb(193, 156, 0);
            TerYellow.DefaultColor = Color.FromArgb(193, 156, 0);
            TerYellow.DontShowInfo = false;
            TerYellow.Location = new Point(140, 371);
            TerYellow.Margin = new Padding(4, 3, 4, 3);
            TerYellow.Name = "TerYellow";
            TerYellow.Size = new Size(100, 24);
            TerYellow.TabIndex = 106;
            // 
            // TerGreenB
            // 
            TerGreenB.AllowDrop = true;
            TerGreenB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerGreenB.BackColor = Color.FromArgb(22, 198, 12);
            TerGreenB.DefaultColor = Color.FromArgb(22, 198, 12);
            TerGreenB.DontShowInfo = false;
            TerGreenB.Location = new Point(245, 281);
            TerGreenB.Margin = new Padding(4, 3, 4, 3);
            TerGreenB.Name = "TerGreenB";
            TerGreenB.Size = new Size(100, 24);
            TerGreenB.TabIndex = 110;
            // 
            // Label143
            // 
            Label143.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label143.BackColor = Color.Transparent;
            Label143.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label143.Location = new Point(36, 63);
            Label143.Name = "Label143";
            Label143.Size = new Size(202, 24);
            Label143.TabIndex = 118;
            Label143.Text = "Background";
            Label143.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label149
            // 
            Label149.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label149.BackColor = Color.Transparent;
            Label149.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label149.Location = new Point(36, 371);
            Label149.Name = "Label149";
            Label149.Size = new Size(97, 24);
            Label149.TabIndex = 90;
            Label149.Text = "Yellow";
            Label149.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerBlack
            // 
            TerBlack.AllowDrop = true;
            TerBlack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerBlack.BackColor = Color.FromArgb(12, 12, 12);
            TerBlack.DefaultColor = Color.FromArgb(12, 12, 12);
            TerBlack.DontShowInfo = false;
            TerBlack.Location = new Point(140, 191);
            TerBlack.Margin = new Padding(4, 3, 4, 3);
            TerBlack.Name = "TerBlack";
            TerBlack.Size = new Size(100, 24);
            TerBlack.TabIndex = 100;
            // 
            // TerYellowB
            // 
            TerYellowB.AllowDrop = true;
            TerYellowB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerYellowB.BackColor = Color.FromArgb(249, 241, 165);
            TerYellowB.DefaultColor = Color.FromArgb(249, 241, 165);
            TerYellowB.DontShowInfo = false;
            TerYellowB.Location = new Point(245, 371);
            TerYellowB.Margin = new Padding(4, 3, 4, 3);
            TerYellowB.Name = "TerYellowB";
            TerYellowB.Size = new Size(100, 24);
            TerYellowB.TabIndex = 114;
            // 
            // Label142
            // 
            Label142.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label142.BackColor = Color.Transparent;
            Label142.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label142.Location = new Point(36, 221);
            Label142.Name = "Label142";
            Label142.Size = new Size(97, 24);
            Label142.TabIndex = 85;
            Label142.Text = "Blue";
            Label142.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerBlackB
            // 
            TerBlackB.AllowDrop = true;
            TerBlackB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerBlackB.BackColor = Color.FromArgb(118, 118, 118);
            TerBlackB.DefaultColor = Color.FromArgb(118, 118, 118);
            TerBlackB.DontShowInfo = false;
            TerBlackB.Location = new Point(245, 191);
            TerBlackB.Margin = new Padding(4, 3, 4, 3);
            TerBlackB.Name = "TerBlackB";
            TerBlackB.Size = new Size(100, 24);
            TerBlackB.TabIndex = 108;
            // 
            // TerPurple
            // 
            TerPurple.AllowDrop = true;
            TerPurple.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerPurple.BackColor = Color.FromArgb(136, 23, 152);
            TerPurple.DefaultColor = Color.FromArgb(136, 23, 152);
            TerPurple.DontShowInfo = false;
            TerPurple.Location = new Point(140, 341);
            TerPurple.Margin = new Padding(4, 3, 4, 3);
            TerPurple.Name = "TerPurple";
            TerPurple.Size = new Size(100, 24);
            TerPurple.TabIndex = 105;
            // 
            // Label140
            // 
            Label140.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label140.BackColor = Color.Transparent;
            Label140.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label140.Location = new Point(36, 191);
            Label140.Name = "Label140";
            Label140.Size = new Size(97, 24);
            Label140.TabIndex = 4;
            Label140.Text = "Black";
            Label140.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerPurpleB
            // 
            TerPurpleB.AllowDrop = true;
            TerPurpleB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerPurpleB.BackColor = Color.FromArgb(180, 0, 158);
            TerPurpleB.DefaultColor = Color.FromArgb(180, 0, 158);
            TerPurpleB.DontShowInfo = false;
            TerPurpleB.Location = new Point(245, 341);
            TerPurpleB.Margin = new Padding(4, 3, 4, 3);
            TerPurpleB.Name = "TerPurpleB";
            TerPurpleB.Size = new Size(100, 24);
            TerPurpleB.TabIndex = 113;
            // 
            // TerBlueB
            // 
            TerBlueB.AllowDrop = true;
            TerBlueB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerBlueB.BackColor = Color.FromArgb(59, 120, 255);
            TerBlueB.DefaultColor = Color.FromArgb(59, 120, 255);
            TerBlueB.DontShowInfo = false;
            TerBlueB.Location = new Point(245, 222);
            TerBlueB.Margin = new Padding(4, 3, 4, 3);
            TerBlueB.Name = "TerBlueB";
            TerBlueB.Size = new Size(100, 24);
            TerBlueB.TabIndex = 109;
            // 
            // Label146
            // 
            Label146.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label146.BackColor = Color.Transparent;
            Label146.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label146.Location = new Point(36, 311);
            Label146.Name = "Label146";
            Label146.Size = new Size(97, 24);
            Label146.TabIndex = 88;
            Label146.Text = "Red";
            Label146.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label148
            // 
            Label148.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label148.BackColor = Color.Transparent;
            Label148.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label148.Location = new Point(36, 341);
            Label148.Name = "Label148";
            Label148.Size = new Size(97, 24);
            Label148.TabIndex = 89;
            Label148.Text = "Purple";
            Label148.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerRedB
            // 
            TerRedB.AllowDrop = true;
            TerRedB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerRedB.BackColor = Color.FromArgb(231, 72, 86);
            TerRedB.DefaultColor = Color.FromArgb(231, 72, 86);
            TerRedB.DontShowInfo = false;
            TerRedB.Location = new Point(245, 311);
            TerRedB.Margin = new Padding(4, 3, 4, 3);
            TerRedB.Name = "TerRedB";
            TerRedB.Size = new Size(100, 24);
            TerRedB.TabIndex = 112;
            // 
            // TerRed
            // 
            TerRed.AllowDrop = true;
            TerRed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerRed.BackColor = Color.FromArgb(197, 15, 31);
            TerRed.DefaultColor = Color.FromArgb(197, 15, 31);
            TerRed.DontShowInfo = false;
            TerRed.Location = new Point(140, 311);
            TerRed.Margin = new Padding(4, 3, 4, 3);
            TerRed.Name = "TerRed";
            TerRed.Size = new Size(100, 24);
            TerRed.TabIndex = 104;
            // 
            // TerCursorHeightBar
            // 
            TerCursorHeightBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerCursorHeightBar.LargeChange = 1;
            TerCursorHeightBar.Location = new Point(96, 39);
            TerCursorHeightBar.Maximum = 100;
            TerCursorHeightBar.Minimum = 1;
            TerCursorHeightBar.Name = "TerCursorHeightBar";
            TerCursorHeightBar.Size = new Size(210, 19);
            TerCursorHeightBar.SmallChange = 1;
            TerCursorHeightBar.TabIndex = 102;
            TerCursorHeightBar.Value = 20;
            // 
            // TerCursorStyle
            // 
            TerCursorStyle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerCursorStyle.BackColor = Color.FromArgb(43, 43, 43);
            TerCursorStyle.DrawMode = DrawMode.OwnerDrawFixed;
            TerCursorStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            TerCursorStyle.Font = new Font("Segoe UI", 9.0f);
            TerCursorStyle.ForeColor = Color.White;
            TerCursorStyle.FormattingEnabled = true;
            TerCursorStyle.ItemHeight = 20;
            TerCursorStyle.Items.AddRange(new object[] { "Bar", "Double Underscore", "Empty Box", "Filled Box", "Underscore", "Vintage" });
            TerCursorStyle.Location = new Point(96, 5);
            TerCursorStyle.Name = "TerCursorStyle";
            TerCursorStyle.Size = new Size(250, 26);
            TerCursorStyle.TabIndex = 110;
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
            Button10.Location = new Point(786, 600);
            Button10.Name = "Button10";
            Button10.Size = new Size(124, 34);
            Button10.TabIndex = 107;
            Button10.Text = "Quick apply";
            Button10.UseVisualStyleBackColor = false;
            // 
            // GroupBox13
            // 
            GroupBox13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox13.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox13.Controls.Add(Button6);
            GroupBox13.Controls.Add(Button18);
            GroupBox13.Controls.Add(Button14);
            GroupBox13.Controls.Add(PictureBox25);
            GroupBox13.Controls.Add(Label155);
            GroupBox13.Controls.Add(TerProfiles);
            GroupBox13.Controls.Add(Button13);
            GroupBox13.Location = new Point(12, 106);
            GroupBox13.Name = "GroupBox13";
            GroupBox13.Size = new Size(460, 40);
            GroupBox13.TabIndex = 117;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.LineColor = Color.FromArgb(34, 114, 139);
            Button6.Location = new Point(328, 5);
            Button6.Name = "Button6";
            Button6.Size = new Size(30, 30);
            Button6.TabIndex = 216;
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button18
            // 
            Button18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button18.BackColor = Color.FromArgb(43, 43, 43);
            Button18.DrawOnGlass = false;
            Button18.Font = new Font("Segoe UI", 9.0f);
            Button18.ForeColor = Color.White;
            Button18.Image = (Image)resources.GetObject("Button18.Image");
            Button18.LineColor = Color.FromArgb(37, 123, 145);
            Button18.Location = new Point(360, 5);
            Button18.Name = "Button18";
            Button18.Size = new Size(30, 30);
            Button18.TabIndex = 215;
            Button18.UseVisualStyleBackColor = false;
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(43, 43, 43);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = (Image)resources.GetObject("Button14.Image");
            Button14.LineColor = Color.FromArgb(56, 46, 14);
            Button14.Location = new Point(392, 5);
            Button14.Name = "Button14";
            Button14.Size = new Size(30, 30);
            Button14.TabIndex = 179;
            Button14.UseVisualStyleBackColor = false;
            // 
            // PictureBox25
            // 
            PictureBox25.Image = (Image)resources.GetObject("PictureBox25.Image");
            PictureBox25.Location = new Point(4, 5);
            PictureBox25.Name = "PictureBox25";
            PictureBox25.Size = new Size(35, 31);
            PictureBox25.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox25.TabIndex = 83;
            PictureBox25.TabStop = false;
            // 
            // Label155
            // 
            Label155.BackColor = Color.Transparent;
            Label155.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label155.Location = new Point(41, 5);
            Label155.Name = "Label155";
            Label155.Size = new Size(62, 31);
            Label155.TabIndex = 84;
            Label155.Text = "Profiles:";
            Label155.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerProfiles
            // 
            TerProfiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerProfiles.BackColor = Color.FromArgb(55, 55, 55);
            TerProfiles.DrawMode = DrawMode.OwnerDrawFixed;
            TerProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            TerProfiles.Font = new Font("Segoe UI", 9.0f);
            TerProfiles.ForeColor = Color.White;
            TerProfiles.FormattingEnabled = true;
            TerProfiles.ItemHeight = 20;
            TerProfiles.Location = new Point(108, 7);
            TerProfiles.Name = "TerProfiles";
            TerProfiles.Size = new Size(216, 26);
            TerProfiles.TabIndex = 118;
            // 
            // Button13
            // 
            Button13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button13.BackColor = Color.FromArgb(43, 43, 43);
            Button13.DrawOnGlass = false;
            Button13.Font = new Font("Segoe UI", 9.0f);
            Button13.ForeColor = Color.White;
            Button13.Image = (Image)resources.GetObject("Button13.Image");
            Button13.LineColor = Color.FromArgb(28, 103, 64);
            Button13.Location = new Point(424, 5);
            Button13.Name = "Button13";
            Button13.Size = new Size(30, 30);
            Button13.TabIndex = 140;
            Button13.UseVisualStyleBackColor = false;
            // 
            // TerFontSizeBar
            // 
            TerFontSizeBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerFontSizeBar.LargeChange = 10;
            TerFontSizeBar.Location = new Point(96, 69);
            TerFontSizeBar.Maximum = 48;
            TerFontSizeBar.Minimum = 5;
            TerFontSizeBar.Name = "TerFontSizeBar";
            TerFontSizeBar.Size = new Size(210, 19);
            TerFontSizeBar.SmallChange = 1;
            TerFontSizeBar.TabIndex = 101;
            TerFontSizeBar.Value = 5;
            // 
            // TerFontWeight
            // 
            TerFontWeight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerFontWeight.BackColor = Color.FromArgb(43, 43, 43);
            TerFontWeight.DrawMode = DrawMode.OwnerDrawFixed;
            TerFontWeight.DropDownStyle = ComboBoxStyle.DropDownList;
            TerFontWeight.Font = new Font("Segoe UI", 9.0f);
            TerFontWeight.ForeColor = Color.White;
            TerFontWeight.FormattingEnabled = true;
            TerFontWeight.ItemHeight = 20;
            TerFontWeight.Items.AddRange(new object[] { "Thin", "Extra Light", "Light", "Semi Light", "Normal", "Medium", "Semi Bold", "Bold", "Extra Bold", "Black", "Extra Black" });
            TerFontWeight.Location = new Point(96, 35);
            TerFontWeight.Name = "TerFontWeight";
            TerFontWeight.Size = new Size(250, 26);
            TerFontWeight.TabIndex = 99;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(Button22);
            GroupBox3.Controls.Add(Separator2);
            GroupBox3.Controls.Add(Button11);
            GroupBox3.Controls.Add(Label11);
            GroupBox3.Controls.Add(Button9);
            GroupBox3.Controls.Add(checker_img);
            GroupBox3.Controls.Add(AlertBox1);
            GroupBox3.Controls.Add(Button8);
            GroupBox3.Controls.Add(Button7);
            GroupBox3.Controls.Add(TerEnabled);
            GroupBox3.Location = new Point(12, 12);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(984, 81);
            GroupBox3.TabIndex = 199;
            // 
            // Button22
            // 
            Button22.BackColor = Color.FromArgb(43, 43, 43);
            Button22.DrawOnGlass = false;
            Button22.Font = new Font("Segoe UI", 9.0f);
            Button22.ForeColor = Color.White;
            Button22.Image = (Image)resources.GetObject("Button22.Image");
            Button22.ImageAlign = ContentAlignment.MiddleRight;
            Button22.LineColor = Color.FromArgb(90, 134, 117);
            Button22.Location = new Point(223, 6);
            Button22.Name = "Button22";
            Button22.Size = new Size(146, 29);
            Button22.TabIndex = 202;
            Button22.Text = "Current applied one";
            Button22.UseVisualStyleBackColor = false;
            // 
            // Separator2
            // 
            Separator2.AlternativeLook = false;
            Separator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator2.Location = new Point(4, 40);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(973, 1);
            Separator2.TabIndex = 201;
            Separator2.TabStop = false;
            // 
            // Label11
            // 
            Label11.BackColor = Color.Transparent;
            Label11.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label11.Location = new Point(4, 4);
            Label11.Name = "Label11";
            Label11.Size = new Size(75, 32);
            Label11.TabIndex = 111;
            Label11.Text = "Open from:";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = My.Resources.checker_disabled;
            checker_img.Location = new Point(891, 5);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.AllowDrop = true;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage5);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Controls.Add(TabPage4);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ItemSize = new Size(35, 100);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(10, 148);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(471, 454);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 201;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(GroupBox4);
            TabPage1.Location = new Point(104, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(363, 446);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Colors";
            // 
            // GroupBox4
            // 
            GroupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox4.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox4.Controls.Add(TerWhiteB);
            GroupBox4.Controls.Add(PictureBox29);
            GroupBox4.Controls.Add(TerWhite);
            GroupBox4.Controls.Add(Label151);
            GroupBox4.Controls.Add(TerYellow);
            GroupBox4.Controls.Add(TerYellowB);
            GroupBox4.Controls.Add(TerGreen);
            GroupBox4.Controls.Add(TerCyanB);
            GroupBox4.Controls.Add(TerGreenB);
            GroupBox4.Controls.Add(TerPurple);
            GroupBox4.Controls.Add(PictureBox30);
            GroupBox4.Controls.Add(TerPurpleB);
            GroupBox4.Controls.Add(TerCyan);
            GroupBox4.Controls.Add(PictureBox31);
            GroupBox4.Controls.Add(TerRedB);
            GroupBox4.Controls.Add(PictureBox34);
            GroupBox4.Controls.Add(TerRed);
            GroupBox4.Controls.Add(PictureBox26);
            GroupBox4.Controls.Add(PictureBox28);
            GroupBox4.Controls.Add(PictureBox23);
            GroupBox4.Controls.Add(TerBlue);
            GroupBox4.Controls.Add(Label149);
            GroupBox4.Controls.Add(Label145);
            GroupBox4.Controls.Add(PictureBox22);
            GroupBox4.Controls.Add(Label144);
            GroupBox4.Controls.Add(Separator3);
            GroupBox4.Controls.Add(TerCursor);
            GroupBox4.Controls.Add(PictureBox21);
            GroupBox4.Controls.Add(Label146);
            GroupBox4.Controls.Add(Label148);
            GroupBox4.Controls.Add(PictureBox20);
            GroupBox4.Controls.Add(Label150);
            GroupBox4.Controls.Add(PictureBox19);
            GroupBox4.Controls.Add(TerSelection);
            GroupBox4.Controls.Add(PictureBox15);
            GroupBox4.Controls.Add(TerSchemes);
            GroupBox4.Controls.Add(TerForeground);
            GroupBox4.Controls.Add(Button20);
            GroupBox4.Controls.Add(Label142);
            GroupBox4.Controls.Add(TerBlack);
            GroupBox4.Controls.Add(Label152);
            GroupBox4.Controls.Add(PictureBox14);
            GroupBox4.Controls.Add(TerBlueB);
            GroupBox4.Controls.Add(TerBlackB);
            GroupBox4.Controls.Add(Label147);
            GroupBox4.Controls.Add(Button17);
            GroupBox4.Controls.Add(Button4);
            GroupBox4.Controls.Add(Button12);
            GroupBox4.Controls.Add(Label19);
            GroupBox4.Controls.Add(Label140);
            GroupBox4.Controls.Add(Label143);
            GroupBox4.Controls.Add(TerBackground);
            GroupBox4.Location = new Point(6, 6);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Size = new Size(351, 432);
            GroupBox4.TabIndex = 87;
            // 
            // PictureBox29
            // 
            PictureBox29.Image = (Image)resources.GetObject("PictureBox29.Image");
            PictureBox29.Location = new Point(6, 401);
            PictureBox29.Name = "PictureBox29";
            PictureBox29.Size = new Size(24, 24);
            PictureBox29.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox29.TabIndex = 233;
            PictureBox29.TabStop = false;
            // 
            // PictureBox30
            // 
            PictureBox30.Image = (Image)resources.GetObject("PictureBox30.Image");
            PictureBox30.Location = new Point(6, 371);
            PictureBox30.Name = "PictureBox30";
            PictureBox30.Size = new Size(24, 24);
            PictureBox30.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox30.TabIndex = 232;
            PictureBox30.TabStop = false;
            // 
            // PictureBox31
            // 
            PictureBox31.Image = (Image)resources.GetObject("PictureBox31.Image");
            PictureBox31.Location = new Point(6, 341);
            PictureBox31.Name = "PictureBox31";
            PictureBox31.Size = new Size(24, 24);
            PictureBox31.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox31.TabIndex = 231;
            PictureBox31.TabStop = false;
            // 
            // PictureBox34
            // 
            PictureBox34.Image = (Image)resources.GetObject("PictureBox34.Image");
            PictureBox34.Location = new Point(6, 311);
            PictureBox34.Name = "PictureBox34";
            PictureBox34.Size = new Size(24, 24);
            PictureBox34.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox34.TabIndex = 230;
            PictureBox34.TabStop = false;
            // 
            // PictureBox26
            // 
            PictureBox26.Image = (Image)resources.GetObject("PictureBox26.Image");
            PictureBox26.Location = new Point(6, 281);
            PictureBox26.Name = "PictureBox26";
            PictureBox26.Size = new Size(24, 24);
            PictureBox26.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox26.TabIndex = 229;
            PictureBox26.TabStop = false;
            // 
            // PictureBox28
            // 
            PictureBox28.Image = (Image)resources.GetObject("PictureBox28.Image");
            PictureBox28.Location = new Point(6, 251);
            PictureBox28.Name = "PictureBox28";
            PictureBox28.Size = new Size(24, 24);
            PictureBox28.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox28.TabIndex = 228;
            PictureBox28.TabStop = false;
            // 
            // PictureBox23
            // 
            PictureBox23.Image = (Image)resources.GetObject("PictureBox23.Image");
            PictureBox23.Location = new Point(6, 221);
            PictureBox23.Name = "PictureBox23";
            PictureBox23.Size = new Size(24, 24);
            PictureBox23.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox23.TabIndex = 225;
            PictureBox23.TabStop = false;
            // 
            // PictureBox22
            // 
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(6, 191);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(24, 24);
            PictureBox22.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox22.TabIndex = 223;
            PictureBox22.TabStop = false;
            // 
            // Separator3
            // 
            Separator3.AlternativeLook = false;
            Separator3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator3.Location = new Point(6, 184);
            Separator3.Name = "Separator3";
            Separator3.Size = new Size(341, 1);
            Separator3.TabIndex = 222;
            Separator3.TabStop = false;
            // 
            // PictureBox21
            // 
            PictureBox21.Image = (Image)resources.GetObject("PictureBox21.Image");
            PictureBox21.Location = new Point(6, 153);
            PictureBox21.Name = "PictureBox21";
            PictureBox21.Size = new Size(24, 24);
            PictureBox21.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox21.TabIndex = 221;
            PictureBox21.TabStop = false;
            // 
            // PictureBox20
            // 
            PictureBox20.Image = (Image)resources.GetObject("PictureBox20.Image");
            PictureBox20.Location = new Point(6, 123);
            PictureBox20.Name = "PictureBox20";
            PictureBox20.Size = new Size(24, 24);
            PictureBox20.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox20.TabIndex = 220;
            PictureBox20.TabStop = false;
            // 
            // PictureBox19
            // 
            PictureBox19.Image = (Image)resources.GetObject("PictureBox19.Image");
            PictureBox19.Location = new Point(6, 93);
            PictureBox19.Name = "PictureBox19";
            PictureBox19.Size = new Size(24, 24);
            PictureBox19.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox19.TabIndex = 219;
            PictureBox19.TabStop = false;
            // 
            // PictureBox15
            // 
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(6, 63);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox15.TabIndex = 218;
            PictureBox15.TabStop = false;
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
            // Label19
            // 
            Label19.BackColor = Color.Transparent;
            Label19.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label19.Location = new Point(36, 6);
            Label19.Name = "Label19";
            Label19.Size = new Size(65, 24);
            Label19.TabIndex = 84;
            Label19.Text = "Scheme:";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(GroupBox2);
            TabPage5.Location = new Point(104, 4);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(3);
            TabPage5.Size = new Size(363, 446);
            TabPage5.TabIndex = 4;
            TabPage5.Text = "Theme";
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(TerThemes);
            GroupBox2.Controls.Add(Button21);
            GroupBox2.Controls.Add(TerThemesContainer);
            GroupBox2.Controls.Add(PictureBox37);
            GroupBox2.Controls.Add(Button19);
            GroupBox2.Controls.Add(Label20);
            GroupBox2.Controls.Add(TerEditThemeName);
            GroupBox2.Controls.Add(Button3);
            GroupBox2.Location = new Point(6, 6);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(351, 212);
            GroupBox2.TabIndex = 88;
            // 
            // PictureBox37
            // 
            PictureBox37.Image = (Image)resources.GetObject("PictureBox37.Image");
            PictureBox37.Location = new Point(6, 6);
            PictureBox37.Name = "PictureBox37";
            PictureBox37.Size = new Size(24, 24);
            PictureBox37.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox37.TabIndex = 201;
            PictureBox37.TabStop = false;
            // 
            // Label20
            // 
            Label20.BackColor = Color.Transparent;
            Label20.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label20.Location = new Point(36, 6);
            Label20.Name = "Label20";
            Label20.Size = new Size(65, 24);
            Label20.TabIndex = 84;
            Label20.Text = "Theme:";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(GroupBox5);
            TabPage2.Location = new Point(104, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(363, 446);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Fonts";
            // 
            // GroupBox5
            // 
            GroupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox5.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox5.Controls.Add(TerFontName);
            GroupBox5.Controls.Add(Button23);
            GroupBox5.Controls.Add(TerFontSizeVal);
            GroupBox5.Controls.Add(PictureBox5);
            GroupBox5.Controls.Add(TerFontWeight);
            GroupBox5.Controls.Add(TerFontSizeBar);
            GroupBox5.Controls.Add(Label61);
            GroupBox5.Controls.Add(PictureBox8);
            GroupBox5.Controls.Add(Label35);
            GroupBox5.Controls.Add(PictureBox9);
            GroupBox5.Controls.Add(Label59);
            GroupBox5.Location = new Point(6, 6);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Size = new Size(351, 97);
            GroupBox5.TabIndex = 98;
            // 
            // TerFontName
            // 
            TerFontName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerFontName.BackColor = Color.Transparent;
            TerFontName.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            TerFontName.Location = new Point(96, 6);
            TerFontName.Name = "TerFontName";
            TerFontName.Size = new Size(210, 24);
            TerFontName.TabIndex = 138;
            TerFontName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button23
            // 
            Button23.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button23.BackColor = Color.FromArgb(43, 43, 43);
            Button23.DrawOnGlass = false;
            Button23.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Button23.ForeColor = Color.White;
            Button23.Image = null;
            Button23.LineColor = Color.FromArgb(199, 49, 61);
            Button23.Location = new Point(316, 6);
            Button23.Name = "Button23";
            Button23.Size = new Size(30, 24);
            Button23.TabIndex = 137;
            Button23.Text = "...";
            Button23.UseVisualStyleBackColor = false;
            // 
            // TerFontSizeVal
            // 
            TerFontSizeVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerFontSizeVal.BackColor = Color.FromArgb(43, 43, 43);
            TerFontSizeVal.DrawOnGlass = false;
            TerFontSizeVal.Font = new Font("Segoe UI", 9.0f);
            TerFontSizeVal.ForeColor = Color.White;
            TerFontSizeVal.Image = null;
            TerFontSizeVal.LineColor = Color.FromArgb(0, 81, 210);
            TerFontSizeVal.Location = new Point(312, 66);
            TerFontSizeVal.Name = "TerFontSizeVal";
            TerFontSizeVal.Size = new Size(34, 24);
            TerFontSizeVal.TabIndex = 132;
            TerFontSizeVal.UseVisualStyleBackColor = false;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(6, 6);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 99;
            PictureBox5.TabStop = false;
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
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(6, 36);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 100;
            PictureBox8.TabStop = false;
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
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(6, 66);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 101;
            PictureBox9.TabStop = false;
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
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(GroupBox34);
            TabPage3.Location = new Point(104, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(363, 446);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Cursor";
            // 
            // GroupBox34
            // 
            GroupBox34.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox34.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox34.Controls.Add(PictureBox10);
            GroupBox34.Controls.Add(TerCursorHeightVal);
            GroupBox34.Controls.Add(Label60);
            GroupBox34.Controls.Add(PictureBox11);
            GroupBox34.Controls.Add(Label14);
            GroupBox34.Controls.Add(TerCursorHeightBar);
            GroupBox34.Controls.Add(TerCursorStyle);
            GroupBox34.Location = new Point(6, 6);
            GroupBox34.Name = "GroupBox34";
            GroupBox34.Size = new Size(351, 67);
            GroupBox34.TabIndex = 99;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(6, 6);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 104;
            PictureBox10.TabStop = false;
            // 
            // TerCursorHeightVal
            // 
            TerCursorHeightVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerCursorHeightVal.BackColor = Color.FromArgb(43, 43, 43);
            TerCursorHeightVal.DrawOnGlass = false;
            TerCursorHeightVal.Font = new Font("Segoe UI", 9.0f);
            TerCursorHeightVal.ForeColor = Color.White;
            TerCursorHeightVal.Image = null;
            TerCursorHeightVal.LineColor = Color.FromArgb(0, 81, 210);
            TerCursorHeightVal.Location = new Point(312, 36);
            TerCursorHeightVal.Name = "TerCursorHeightVal";
            TerCursorHeightVal.Size = new Size(34, 24);
            TerCursorHeightVal.TabIndex = 133;
            TerCursorHeightVal.UseVisualStyleBackColor = false;
            // 
            // Label60
            // 
            Label60.BackColor = Color.Transparent;
            Label60.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label60.Location = new Point(36, 36);
            Label60.Name = "Label60";
            Label60.Size = new Size(54, 24);
            Label60.TabIndex = 111;
            Label60.Text = "Size:";
            Label60.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(6, 36);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 105;
            PictureBox11.TabStop = false;
            // 
            // Label14
            // 
            Label14.BackColor = Color.Transparent;
            Label14.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label14.Location = new Point(36, 6);
            Label14.Name = "Label14";
            Label14.Size = new Size(54, 24);
            Label14.TabIndex = 109;
            Label14.Text = "Style:";
            Label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(GroupBox12);
            TabPage4.Location = new Point(104, 4);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(363, 446);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "Background";
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(TerOpacityVal);
            GroupBox12.Controls.Add(TerOpacityBar);
            GroupBox12.Controls.Add(TerImageOpacityVal);
            GroupBox12.Controls.Add(PictureBox13);
            GroupBox12.Controls.Add(PictureBox40);
            GroupBox12.Controls.Add(PictureBox16);
            GroupBox12.Controls.Add(TerAcrylic);
            GroupBox12.Controls.Add(Button5);
            GroupBox12.Controls.Add(TerBackImage);
            GroupBox12.Controls.Add(Label57);
            GroupBox12.Controls.Add(Button16);
            GroupBox12.Controls.Add(Label2);
            GroupBox12.Controls.Add(TerImageOpacity);
            GroupBox12.Location = new Point(6, 6);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(351, 122);
            GroupBox12.TabIndex = 100;
            // 
            // TerOpacityVal
            // 
            TerOpacityVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerOpacityVal.BackColor = Color.FromArgb(43, 43, 43);
            TerOpacityVal.DrawOnGlass = false;
            TerOpacityVal.Font = new Font("Segoe UI", 9.0f);
            TerOpacityVal.ForeColor = Color.White;
            TerOpacityVal.Image = null;
            TerOpacityVal.LineColor = Color.FromArgb(0, 81, 210);
            TerOpacityVal.Location = new Point(311, 91);
            TerOpacityVal.Name = "TerOpacityVal";
            TerOpacityVal.Size = new Size(34, 24);
            TerOpacityVal.TabIndex = 135;
            TerOpacityVal.UseVisualStyleBackColor = false;
            // 
            // TerImageOpacityVal
            // 
            TerImageOpacityVal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TerImageOpacityVal.BackColor = Color.FromArgb(43, 43, 43);
            TerImageOpacityVal.DrawOnGlass = false;
            TerImageOpacityVal.Font = new Font("Segoe UI", 9.0f);
            TerImageOpacityVal.ForeColor = Color.White;
            TerImageOpacityVal.Image = null;
            TerImageOpacityVal.LineColor = Color.FromArgb(0, 81, 210);
            TerImageOpacityVal.Location = new Point(311, 62);
            TerImageOpacityVal.Name = "TerImageOpacityVal";
            TerImageOpacityVal.Size = new Size(34, 24);
            TerImageOpacityVal.TabIndex = 134;
            TerImageOpacityVal.UseVisualStyleBackColor = false;
            // 
            // PictureBox13
            // 
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(6, 6);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(24, 24);
            PictureBox13.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox13.TabIndex = 106;
            PictureBox13.TabStop = false;
            // 
            // PictureBox16
            // 
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(6, 62);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(24, 24);
            PictureBox16.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox16.TabIndex = 126;
            PictureBox16.TabStop = false;
            // 
            // Label57
            // 
            Label57.BackColor = Color.Transparent;
            Label57.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label57.Location = new Point(36, 62);
            Label57.Name = "Label57";
            Label57.Size = new Size(65, 24);
            Label57.TabIndex = 119;
            Label57.Text = "Opacity:";
            Label57.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FontDialog1
            // 
            FontDialog1.FixedPitchOnly = true;
            FontDialog1.ShowEffects = false;
            // 
            // WindowsTerminal
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(1008, 646);
            Controls.Add(GroupBox22);
            Controls.Add(TabControl1);
            Controls.Add(GroupBox3);
            Controls.Add(Separator1);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(CheckBox1);
            Controls.Add(Button10);
            Controls.Add(GroupBox13);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WindowsTerminal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Windows Terminal";
            TerThemesContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox27).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).EndInit();
            GroupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox25).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox29).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            TabPage5.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox37).EndInit();
            TabPage2.ResumeLayout(false);
            GroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            TabPage3.ResumeLayout(false);
            GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            TabPage4.ResumeLayout(false);
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            Load += new EventHandler(WindowsTerminal_Load);
            Shown += new EventHandler(WindowsTerminal_Shown);
            FormClosing += new FormClosingEventHandler(WindowsTerminal_FormClosing);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }
        internal UI.WP.GroupBox GroupBox22;
        internal UI.WP.Button Button15;
        internal PictureBox PictureBox27;
        internal Label Label141;
        internal UI.WP.Button Button16;
        internal UI.WP.TextBox TerBackImage;
        internal UI.WP.Trackbar TerImageOpacity;
        internal UI.WP.Button Button12;
        internal UI.WP.ComboBox TerSchemes;
        internal UI.Controllers.ColorItem TerCursor;
        internal UI.Controllers.ColorItem TerWhiteB;
        internal Label Label150;
        internal UI.Controllers.ColorItem TerBlue;
        internal Label Label151;
        internal UI.Controllers.ColorItem TerSelection;
        internal Label Label152;
        internal Label Label147;
        internal UI.Controllers.ColorItem TerWhite;
        internal UI.Controllers.ColorItem TerForeground;
        internal UI.Controllers.ColorItem TerCyanB;
        internal Label Label145;
        internal UI.Controllers.ColorItem TerCyan;
        internal UI.Controllers.ColorItem TerGreen;
        internal Label Label144;
        internal UI.Controllers.ColorItem TerBackground;
        internal UI.Controllers.ColorItem TerYellow;
        internal UI.Controllers.ColorItem TerGreenB;
        internal Label Label143;
        internal Label Label149;
        internal UI.Controllers.ColorItem TerBlack;
        internal UI.Controllers.ColorItem TerYellowB;
        internal Label Label142;
        internal UI.Controllers.ColorItem TerBlackB;
        internal UI.Controllers.ColorItem TerPurple;
        internal Label Label140;
        internal UI.Controllers.ColorItem TerPurpleB;
        internal UI.Controllers.ColorItem TerBlueB;
        internal Label Label146;
        internal Label Label148;
        internal UI.Controllers.ColorItem TerRedB;
        internal UI.Controllers.ColorItem TerRed;
        internal UI.WP.Trackbar TerCursorHeightBar;
        internal UI.WP.ComboBox TerCursorStyle;
        internal UI.WP.GroupBox GroupBox13;
        internal UI.WP.Button Button14;
        internal PictureBox PictureBox25;
        internal Label Label155;
        internal UI.WP.ComboBox TerProfiles;
        internal UI.WP.Button Button13;
        internal UI.WP.Trackbar TerFontSizeBar;
        internal UI.WP.ComboBox TerFontWeight;
        internal Label Label2;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal UI.Simulation.WinTerminal Terminal1;
        internal UI.Simulation.WinTerminal Terminal2;
        internal Label Label9;
        internal UI.Controllers.ColorItem TerTabInactive;
        internal UI.Controllers.ColorItem TerTabActive;
        internal Label Label10;
        internal Label Label5;
        internal UI.Controllers.ColorItem TerTitlebarInactive;
        internal UI.Controllers.ColorItem TerTitlebarActive;
        internal Label Label6;
        internal UI.WP.Button Button3;
        internal UI.WP.ComboBox TerThemes;
        internal UI.WP.Toggle TerMode;
        internal UI.WP.Button TerEditThemeName;
        internal Panel TerThemesContainer;
        internal UI.WP.Button Button4;
        internal PictureBox PictureBox40;
        internal UI.WP.CheckBox TerAcrylic;
        internal UI.WP.Trackbar TerOpacityBar;
        internal UI.WP.Button Button5;
        internal UI.WP.Toggle TerEnabled;
        internal OpenFileDialog ImgDlg;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button7;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.Button Button9;
        internal UI.WP.Button Button11;
        internal SaveFileDialog SaveJSONDlg;
        internal OpenFileDialog OpenWPTHDlg;
        internal OpenFileDialog OpenJSONDlg;
        internal UI.WP.Button Button17;
        internal UI.WP.Button Button18;
        internal UI.WP.Button Button19;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button20;
        internal UI.WP.Button Button21;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.SeparatorH Separator2;
        internal Label Label11;
        internal PictureBox checker_img;
        internal UI.WP.Button Button22;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal UI.WP.GroupBox GroupBox4;
        internal PictureBox PictureBox23;
        internal PictureBox PictureBox22;
        internal UI.WP.SeparatorH Separator3;
        internal PictureBox PictureBox21;
        internal PictureBox PictureBox20;
        internal PictureBox PictureBox19;
        internal PictureBox PictureBox15;
        internal PictureBox PictureBox14;
        internal Label Label19;
        internal TabPage TabPage2;
        internal UI.WP.GroupBox GroupBox5;
        internal PictureBox PictureBox5;
        internal Label Label61;
        internal PictureBox PictureBox8;
        internal Label Label35;
        internal PictureBox PictureBox9;
        internal Label Label59;
        internal TabPage TabPage3;
        internal UI.WP.GroupBox GroupBox34;
        internal Label Label60;
        internal PictureBox PictureBox11;
        internal Label Label14;
        internal TabPage TabPage4;
        internal UI.WP.GroupBox GroupBox12;
        internal PictureBox PictureBox13;
        internal PictureBox PictureBox16;
        internal Label Label57;
        internal PictureBox PictureBox29;
        internal PictureBox PictureBox30;
        internal PictureBox PictureBox31;
        internal PictureBox PictureBox34;
        internal PictureBox PictureBox26;
        internal PictureBox PictureBox28;
        internal Label Label1;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox3;
        internal PictureBox PictureBox4;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox1;
        internal TabPage TabPage5;
        internal UI.WP.GroupBox GroupBox2;
        internal PictureBox PictureBox37;
        internal Label Label20;
        internal UI.WP.Button TerFontSizeVal;
        internal UI.WP.Button TerCursorHeightVal;
        internal UI.WP.Button TerOpacityVal;
        internal UI.WP.Button TerImageOpacityVal;
        internal PictureBox PictureBox10;
        internal Label TerFontName;
        internal UI.WP.Button Button23;
        internal FontDialog FontDialog1;
    }
}