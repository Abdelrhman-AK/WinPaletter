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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsTerminal));
            this.ImgDlg = new System.Windows.Forms.OpenFileDialog();
            this.SaveJSONDlg = new System.Windows.Forms.SaveFileDialog();
            this.OpenWPTHDlg = new System.Windows.Forms.OpenFileDialog();
            this.OpenJSONDlg = new System.Windows.Forms.OpenFileDialog();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.TerEnabled = new WinPaletter.UI.WP.Toggle();
            this.Button21 = new WinPaletter.UI.WP.Button();
            this.Button19 = new WinPaletter.UI.WP.Button();
            this.TerEditThemeName = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.TerThemes = new WinPaletter.UI.WP.ComboBox();
            this.TerThemesContainer = new System.Windows.Forms.Panel();
            this.TerMode = new WinPaletter.UI.WP.Toggle();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.TerTitlebarActive = new WinPaletter.UI.Controllers.ColorItem();
            this.TerTitlebarInactive = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.TerTabActive = new WinPaletter.UI.Controllers.ColorItem();
            this.TerTabInactive = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.GroupBox22 = new WinPaletter.UI.WP.GroupBox();
            this.Terminal1 = new WinPaletter.UI.Simulation.WinTerminal();
            this.Button15 = new WinPaletter.UI.WP.Button();
            this.PictureBox27 = new System.Windows.Forms.PictureBox();
            this.Label141 = new System.Windows.Forms.Label();
            this.Terminal2 = new WinPaletter.UI.Simulation.WinTerminal();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TerOpacityBar = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox40 = new System.Windows.Forms.PictureBox();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.TerAcrylic = new WinPaletter.UI.WP.CheckBox();
            this.Button16 = new WinPaletter.UI.WP.Button();
            this.TerBackImage = new WinPaletter.UI.WP.TextBox();
            this.TerImageOpacity = new WinPaletter.UI.WP.Trackbar();
            this.Label2 = new System.Windows.Forms.Label();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Button20 = new WinPaletter.UI.WP.Button();
            this.Button17 = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.TerSchemes = new WinPaletter.UI.WP.ComboBox();
            this.TerCursor = new WinPaletter.UI.Controllers.ColorItem();
            this.TerWhiteB = new WinPaletter.UI.Controllers.ColorItem();
            this.Label150 = new System.Windows.Forms.Label();
            this.TerBlue = new WinPaletter.UI.Controllers.ColorItem();
            this.Label151 = new System.Windows.Forms.Label();
            this.TerSelection = new WinPaletter.UI.Controllers.ColorItem();
            this.Label152 = new System.Windows.Forms.Label();
            this.Label147 = new System.Windows.Forms.Label();
            this.TerWhite = new WinPaletter.UI.Controllers.ColorItem();
            this.TerForeground = new WinPaletter.UI.Controllers.ColorItem();
            this.TerCyanB = new WinPaletter.UI.Controllers.ColorItem();
            this.Label145 = new System.Windows.Forms.Label();
            this.TerCyan = new WinPaletter.UI.Controllers.ColorItem();
            this.TerGreen = new WinPaletter.UI.Controllers.ColorItem();
            this.Label144 = new System.Windows.Forms.Label();
            this.TerBackground = new WinPaletter.UI.Controllers.ColorItem();
            this.TerYellow = new WinPaletter.UI.Controllers.ColorItem();
            this.TerGreenB = new WinPaletter.UI.Controllers.ColorItem();
            this.Label143 = new System.Windows.Forms.Label();
            this.Label149 = new System.Windows.Forms.Label();
            this.TerBlack = new WinPaletter.UI.Controllers.ColorItem();
            this.TerYellowB = new WinPaletter.UI.Controllers.ColorItem();
            this.Label142 = new System.Windows.Forms.Label();
            this.TerBlackB = new WinPaletter.UI.Controllers.ColorItem();
            this.TerPurple = new WinPaletter.UI.Controllers.ColorItem();
            this.Label140 = new System.Windows.Forms.Label();
            this.TerPurpleB = new WinPaletter.UI.Controllers.ColorItem();
            this.TerBlueB = new WinPaletter.UI.Controllers.ColorItem();
            this.Label146 = new System.Windows.Forms.Label();
            this.Label148 = new System.Windows.Forms.Label();
            this.TerRedB = new WinPaletter.UI.Controllers.ColorItem();
            this.TerRed = new WinPaletter.UI.Controllers.ColorItem();
            this.TerCursorHeightBar = new WinPaletter.UI.WP.Trackbar();
            this.TerCursorStyle = new WinPaletter.UI.WP.ComboBox();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.GroupBox13 = new WinPaletter.UI.WP.GroupBox();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button18 = new WinPaletter.UI.WP.Button();
            this.Button14 = new WinPaletter.UI.WP.Button();
            this.PictureBox25 = new System.Windows.Forms.PictureBox();
            this.Label155 = new System.Windows.Forms.Label();
            this.TerProfiles = new WinPaletter.UI.WP.ComboBox();
            this.Button13 = new WinPaletter.UI.WP.Button();
            this.TerFontSizeBar = new WinPaletter.UI.WP.Trackbar();
            this.TerFontWeight = new WinPaletter.UI.WP.ComboBox();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.Button22 = new WinPaletter.UI.WP.Button();
            this.Separator2 = new WinPaletter.UI.WP.SeparatorH();
            this.Label11 = new System.Windows.Forms.Label();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.TabControl1 = new WinPaletter.UI.WP.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.GroupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox29 = new System.Windows.Forms.PictureBox();
            this.PictureBox30 = new System.Windows.Forms.PictureBox();
            this.PictureBox31 = new System.Windows.Forms.PictureBox();
            this.PictureBox34 = new System.Windows.Forms.PictureBox();
            this.PictureBox26 = new System.Windows.Forms.PictureBox();
            this.PictureBox28 = new System.Windows.Forms.PictureBox();
            this.PictureBox23 = new System.Windows.Forms.PictureBox();
            this.PictureBox22 = new System.Windows.Forms.PictureBox();
            this.Separator3 = new WinPaletter.UI.WP.SeparatorH();
            this.PictureBox21 = new System.Windows.Forms.PictureBox();
            this.PictureBox20 = new System.Windows.Forms.PictureBox();
            this.PictureBox19 = new System.Windows.Forms.PictureBox();
            this.PictureBox15 = new System.Windows.Forms.PictureBox();
            this.PictureBox14 = new System.Windows.Forms.PictureBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox37 = new System.Windows.Forms.PictureBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.GroupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.TerFontName = new System.Windows.Forms.Label();
            this.Button23 = new WinPaletter.UI.WP.Button();
            this.TerFontSizeVal = new WinPaletter.UI.WP.Button();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label61 = new System.Windows.Forms.Label();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.Label59 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.GroupBox34 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.TerCursorHeightVal = new WinPaletter.UI.WP.Button();
            this.Label60 = new System.Windows.Forms.Label();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.TerOpacityVal = new WinPaletter.UI.WP.Button();
            this.TerImageOpacityVal = new WinPaletter.UI.WP.Button();
            this.PictureBox13 = new System.Windows.Forms.PictureBox();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.Label57 = new System.Windows.Forms.Label();
            this.FontDialog1 = new System.Windows.Forms.FontDialog();
            this.TerThemesContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).BeginInit();
            this.GroupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox37)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            this.SuspendLayout();
            // 
            // ImgDlg
            // 
            this.ImgDlg.Filter = "Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All Files (*.*)|*.*";
            // 
            // SaveJSONDlg
            // 
            this.SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*";
            // 
            // OpenWPTHDlg
            // 
            this.OpenWPTHDlg.Filter = "WinPaletter Theme File (*.wpth)|*.wpth|All files (*.*)|*.*";
            // 
            // OpenJSONDlg
            // 
            this.OpenJSONDlg.Filter = "JSON File (*.json)|*.json";
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 98);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(984, 1);
            this.Separator1.TabIndex = 198;
            this.Separator1.TabStop = false;
            this.Separator1.Text = "Separator1";
            // 
            // Button11
            // 
            this.Button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button11.CustomColor = System.Drawing.Color.Empty;
            this.Button11.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = null;
            this.Button11.Location = new System.Drawing.Point(609, 46);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(210, 30);
            this.Button11.TabIndex = 200;
            this.Button11.Text = "Open \"Settings.json\" in editor";
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button9
            // 
            this.Button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button9.CustomColor = System.Drawing.Color.Empty;
            this.Button9.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = null;
            this.Button9.Location = new System.Drawing.Point(823, 46);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(154, 30);
            this.Button9.TabIndex = 199;
            this.Button9.Text = "Backup \"Settings.json\"";
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Button8
            // 
            this.Button8.CustomColor = System.Drawing.Color.Empty;
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.Location = new System.Drawing.Point(85, 6);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(136, 29);
            this.Button8.TabIndex = 110;
            this.Button8.Text = "WinPaletter theme";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = ((System.Drawing.Image)(resources.GetObject("AlertBox1.Image")));
            this.AlertBox1.Location = new System.Drawing.Point(4, 46);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(600, 30);
            this.AlertBox1.TabIndex = 198;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "You should create a backup to Terminal settings file \"settings.json\" to avoid und" +
    "esired actions or errors.";
            // 
            // Button7
            // 
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = ((System.Drawing.Image)(resources.GetObject("Button7.Image")));
            this.Button7.Location = new System.Drawing.Point(371, 6);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(85, 29);
            this.Button7.TabIndex = 109;
            this.Button7.Text = "JSON file";
            this.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // TerEnabled
            // 
            this.TerEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.TerEnabled.Checked = false;
            this.TerEnabled.DarkLight_Toggler = false;
            this.TerEnabled.Location = new System.Drawing.Point(934, 10);
            this.TerEnabled.Name = "TerEnabled";
            this.TerEnabled.Size = new System.Drawing.Size(40, 20);
            this.TerEnabled.TabIndex = 85;
            this.TerEnabled.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.TerEnabled_CheckedChanged);
            // 
            // Button21
            // 
            this.Button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button21.CustomColor = System.Drawing.Color.Empty;
            this.Button21.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button21.ForeColor = System.Drawing.Color.White;
            this.Button21.Image = ((System.Drawing.Image)(resources.GetObject("Button21.Image")));
            this.Button21.Location = new System.Drawing.Point(108, 6);
            this.Button21.Name = "Button21";
            this.Button21.Size = new System.Drawing.Size(87, 24);
            this.Button21.TabIndex = 218;
            this.Button21.Text = "Copycat";
            this.Button21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button21.UseVisualStyleBackColor = false;
            this.Button21.Click += new System.EventHandler(this.Button21_Click);
            // 
            // Button19
            // 
            this.Button19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button19.CustomColor = System.Drawing.Color.Empty;
            this.Button19.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button19.ForeColor = System.Drawing.Color.White;
            this.Button19.Image = ((System.Drawing.Image)(resources.GetObject("Button19.Image")));
            this.Button19.Location = new System.Drawing.Point(197, 6);
            this.Button19.Name = "Button19";
            this.Button19.Size = new System.Drawing.Size(76, 24);
            this.Button19.TabIndex = 216;
            this.Button19.Text = "Clone";
            this.Button19.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button19.UseVisualStyleBackColor = false;
            this.Button19.Click += new System.EventHandler(this.Button19_Click);
            // 
            // TerEditThemeName
            // 
            this.TerEditThemeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerEditThemeName.CustomColor = System.Drawing.Color.Empty;
            this.TerEditThemeName.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.TerEditThemeName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerEditThemeName.ForeColor = System.Drawing.Color.White;
            this.TerEditThemeName.Image = ((System.Drawing.Image)(resources.GetObject("TerEditThemeName.Image")));
            this.TerEditThemeName.Location = new System.Drawing.Point(275, 6);
            this.TerEditThemeName.Name = "TerEditThemeName";
            this.TerEditThemeName.Size = new System.Drawing.Size(35, 24);
            this.TerEditThemeName.TabIndex = 212;
            this.TerEditThemeName.UseVisualStyleBackColor = false;
            this.TerEditThemeName.Click += new System.EventHandler(this.TerEditThemeName_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.Location = new System.Drawing.Point(312, 6);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(35, 24);
            this.Button3.TabIndex = 211;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // TerThemes
            // 
            this.TerThemes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerThemes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.TerThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TerThemes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerThemes.ForeColor = System.Drawing.Color.White;
            this.TerThemes.FormattingEnabled = true;
            this.TerThemes.ItemHeight = 20;
            this.TerThemes.Location = new System.Drawing.Point(5, 33);
            this.TerThemes.Name = "TerThemes";
            this.TerThemes.Size = new System.Drawing.Size(342, 26);
            this.TerThemes.TabIndex = 210;
            this.TerThemes.SelectedIndexChanged += new System.EventHandler(this.TerThemes_SelectedIndexChanged);
            // 
            // TerThemesContainer
            // 
            this.TerThemesContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerThemesContainer.Controls.Add(this.TerMode);
            this.TerThemesContainer.Controls.Add(this.Label1);
            this.TerThemesContainer.Controls.Add(this.PictureBox7);
            this.TerThemesContainer.Controls.Add(this.TerTitlebarActive);
            this.TerThemesContainer.Controls.Add(this.TerTitlebarInactive);
            this.TerThemesContainer.Controls.Add(this.PictureBox3);
            this.TerThemesContainer.Controls.Add(this.Label6);
            this.TerThemesContainer.Controls.Add(this.TerTabActive);
            this.TerThemesContainer.Controls.Add(this.TerTabInactive);
            this.TerThemesContainer.Controls.Add(this.PictureBox4);
            this.TerThemesContainer.Controls.Add(this.PictureBox2);
            this.TerThemesContainer.Controls.Add(this.PictureBox1);
            this.TerThemesContainer.Controls.Add(this.Label5);
            this.TerThemesContainer.Controls.Add(this.Label10);
            this.TerThemesContainer.Controls.Add(this.Label9);
            this.TerThemesContainer.Enabled = false;
            this.TerThemesContainer.Location = new System.Drawing.Point(3, 60);
            this.TerThemesContainer.Name = "TerThemesContainer";
            this.TerThemesContainer.Size = new System.Drawing.Size(344, 149);
            this.TerThemesContainer.TabIndex = 213;
            // 
            // TerMode
            // 
            this.TerMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.TerMode.Checked = false;
            this.TerMode.DarkLight_Toggler = true;
            this.TerMode.Location = new System.Drawing.Point(302, 123);
            this.TerMode.Name = "TerMode";
            this.TerMode.Size = new System.Drawing.Size(40, 20);
            this.TerMode.TabIndex = 209;
            this.TerMode.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.TerMode_CheckedChanged);
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(33, 123);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(253, 24);
            this.Label1.TabIndex = 224;
            this.Label1.Text = "Dark\\Light mode";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(3, 123);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 223;
            this.PictureBox7.TabStop = false;
            // 
            // TerTitlebarActive
            // 
            this.TerTitlebarActive.AllowDrop = true;
            this.TerTitlebarActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerTitlebarActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTitlebarActive.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTitlebarActive.DontShowInfo = false;
            this.TerTitlebarActive.Location = new System.Drawing.Point(242, 3);
            this.TerTitlebarActive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerTitlebarActive.Name = "TerTitlebarActive";
            this.TerTitlebarActive.Size = new System.Drawing.Size(100, 24);
            this.TerTitlebarActive.TabIndex = 198;
            this.TerTitlebarActive.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerTitlebarActive.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // TerTitlebarInactive
            // 
            this.TerTitlebarInactive.AllowDrop = true;
            this.TerTitlebarInactive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerTitlebarInactive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTitlebarInactive.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTitlebarInactive.DontShowInfo = false;
            this.TerTitlebarInactive.Location = new System.Drawing.Point(242, 33);
            this.TerTitlebarInactive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerTitlebarInactive.Name = "TerTitlebarInactive";
            this.TerTitlebarInactive.Size = new System.Drawing.Size(100, 24);
            this.TerTitlebarInactive.TabIndex = 200;
            this.TerTitlebarInactive.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerTitlebarInactive.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(3, 93);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 222;
            this.PictureBox3.TabStop = false;
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(33, 3);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(202, 24);
            this.Label6.TabIndex = 197;
            this.Label6.Text = "Active title";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerTabActive
            // 
            this.TerTabActive.AllowDrop = true;
            this.TerTabActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerTabActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTabActive.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTabActive.DontShowInfo = false;
            this.TerTabActive.Location = new System.Drawing.Point(242, 63);
            this.TerTabActive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerTabActive.Name = "TerTabActive";
            this.TerTabActive.Size = new System.Drawing.Size(100, 24);
            this.TerTabActive.TabIndex = 203;
            this.TerTabActive.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerTabActive.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // TerTabInactive
            // 
            this.TerTabInactive.AllowDrop = true;
            this.TerTabInactive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerTabInactive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTabInactive.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTabInactive.DontShowInfo = false;
            this.TerTabInactive.Location = new System.Drawing.Point(242, 93);
            this.TerTabInactive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerTabInactive.Name = "TerTabInactive";
            this.TerTabInactive.Size = new System.Drawing.Size(100, 24);
            this.TerTabInactive.TabIndex = 205;
            this.TerTabInactive.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerTabInactive.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(3, 63);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 221;
            this.PictureBox4.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(3, 33);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 220;
            this.PictureBox2.TabStop = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 219;
            this.PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(33, 33);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(202, 24);
            this.Label5.TabIndex = 199;
            this.Label5.Text = "Inactive title";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            this.Label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label10.BackColor = System.Drawing.Color.Transparent;
            this.Label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(33, 63);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(202, 24);
            this.Label10.TabIndex = 202;
            this.Label10.Text = "Active tab";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(33, 93);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(202, 24);
            this.Label9.TabIndex = 204;
            this.Label9.Text = "Inactive tab";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.Location = new System.Drawing.Point(700, 600);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 106;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // GroupBox22
            // 
            this.GroupBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox22.Controls.Add(this.Terminal1);
            this.GroupBox22.Controls.Add(this.Button15);
            this.GroupBox22.Controls.Add(this.PictureBox27);
            this.GroupBox22.Controls.Add(this.Label141);
            this.GroupBox22.Controls.Add(this.Terminal2);
            this.GroupBox22.Location = new System.Drawing.Point(479, 106);
            this.GroupBox22.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox22.Name = "GroupBox22";
            this.GroupBox22.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox22.Size = new System.Drawing.Size(517, 301);
            this.GroupBox22.TabIndex = 195;
            // 
            // Terminal1
            // 
            this.Terminal1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Terminal1.BackImage = null;
            this.Terminal1.Color_Background = System.Drawing.Color.Black;
            this.Terminal1.Color_Cursor = System.Drawing.Color.White;
            this.Terminal1.Color_Foreground = System.Drawing.Color.White;
            this.Terminal1.Color_Selection = System.Drawing.Color.Gray;
            this.Terminal1.Color_TabFocused = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Terminal1.Color_TabUnFocused = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Terminal1.Color_Titlebar = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.Terminal1.Color_Titlebar_Unfocused = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.Terminal1.CursorHeight = 25;
            this.Terminal1.CursorType = WinPaletter.UI.Simulation.WinTerminal.CursorShape_Enum.bar;
            this.Terminal1.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Terminal1.IsFocused = true;
            this.Terminal1.Light = false;
            this.Terminal1.Location = new System.Drawing.Point(6, 45);
            this.Terminal1.Name = "Terminal1";
            this.Terminal1.Opacity = 0.15F;
            this.Terminal1.OpacityBackImage = 100F;
            this.Terminal1.PreviewVersion = true;
            this.Terminal1.Size = new System.Drawing.Size(483, 226);
            this.Terminal1.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Terminal1.TabIcon = null;
            this.Terminal1.TabIconButItIsString = "";
            this.Terminal1.TabIndex = 95;
            this.Terminal1.TabTitle = "Command Prompt";
            this.Terminal1.UseAcrylic = false;
            this.Terminal1.UseAcrylicOnTitlebar = false;
            // 
            // Button15
            // 
            this.Button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button15.CustomColor = System.Drawing.Color.Empty;
            this.Button15.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button15.ForeColor = System.Drawing.Color.White;
            this.Button15.Image = null;
            this.Button15.Location = new System.Drawing.Point(356, 7);
            this.Button15.Name = "Button15";
            this.Button15.Size = new System.Drawing.Size(154, 30);
            this.Button15.TabIndex = 94;
            this.Button15.Text = "Open Terminal for testing";
            this.Button15.UseVisualStyleBackColor = false;
            this.Button15.Click += new System.EventHandler(this.Button15_Click);
            // 
            // PictureBox27
            // 
            this.PictureBox27.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox27.Image")));
            this.PictureBox27.Location = new System.Drawing.Point(6, 5);
            this.PictureBox27.Name = "PictureBox27";
            this.PictureBox27.Size = new System.Drawing.Size(35, 35);
            this.PictureBox27.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox27.TabIndex = 4;
            this.PictureBox27.TabStop = false;
            // 
            // Label141
            // 
            this.Label141.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label141.Location = new System.Drawing.Point(47, 5);
            this.Label141.Name = "Label141";
            this.Label141.Size = new System.Drawing.Size(231, 35);
            this.Label141.TabIndex = 3;
            this.Label141.Text = "Preview";
            this.Label141.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Terminal2
            // 
            this.Terminal2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Terminal2.BackImage = null;
            this.Terminal2.Color_Background = System.Drawing.Color.Black;
            this.Terminal2.Color_Cursor = System.Drawing.Color.White;
            this.Terminal2.Color_Foreground = System.Drawing.Color.White;
            this.Terminal2.Color_Selection = System.Drawing.Color.Gray;
            this.Terminal2.Color_TabFocused = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Terminal2.Color_TabUnFocused = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Terminal2.Color_Titlebar = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.Terminal2.Color_Titlebar_Unfocused = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Terminal2.CursorHeight = 25;
            this.Terminal2.CursorType = WinPaletter.UI.Simulation.WinTerminal.CursorShape_Enum.bar;
            this.Terminal2.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Terminal2.IsFocused = false;
            this.Terminal2.Light = false;
            this.Terminal2.Location = new System.Drawing.Point(54, 132);
            this.Terminal2.Name = "Terminal2";
            this.Terminal2.Opacity = 100F;
            this.Terminal2.OpacityBackImage = 0F;
            this.Terminal2.PreviewVersion = true;
            this.Terminal2.Size = new System.Drawing.Size(454, 160);
            this.Terminal2.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Terminal2.TabIcon = null;
            this.Terminal2.TabIconButItIsString = "";
            this.Terminal2.TabIndex = 96;
            this.Terminal2.TabTitle = "";
            this.Terminal2.UseAcrylic = false;
            this.Terminal2.UseAcrylicOnTitlebar = false;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(916, 600);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 34);
            this.Button1.TabIndex = 105;
            this.Button1.Text = "Load";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TerOpacityBar
            // 
            this.TerOpacityBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerOpacityBar.BackColor = System.Drawing.Color.Transparent;
            this.TerOpacityBar.LargeChange = 10;
            this.TerOpacityBar.Location = new System.Drawing.Point(107, 94);
            this.TerOpacityBar.Maximum = 100;
            this.TerOpacityBar.Minimum = 0;
            this.TerOpacityBar.Name = "TerOpacityBar";
            this.TerOpacityBar.Size = new System.Drawing.Size(197, 19);
            this.TerOpacityBar.SmallChange = 1;
            this.TerOpacityBar.TabIndex = 208;
            this.TerOpacityBar.Value = 100;
            this.TerOpacityBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.TerAcrylicBar_Scroll);
            // 
            // PictureBox40
            // 
            this.PictureBox40.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox40.Image")));
            this.PictureBox40.Location = new System.Drawing.Point(6, 92);
            this.PictureBox40.Name = "PictureBox40";
            this.PictureBox40.Size = new System.Drawing.Size(24, 24);
            this.PictureBox40.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox40.TabIndex = 206;
            this.PictureBox40.TabStop = false;
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.Location = new System.Drawing.Point(188, 33);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(116, 25);
            this.Button5.TabIndex = 197;
            this.Button5.Text = "Set as wallpaper";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // TerAcrylic
            // 
            this.TerAcrylic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.TerAcrylic.Checked = false;
            this.TerAcrylic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerAcrylic.ForeColor = System.Drawing.Color.White;
            this.TerAcrylic.Location = new System.Drawing.Point(35, 91);
            this.TerAcrylic.Name = "TerAcrylic";
            this.TerAcrylic.Size = new System.Drawing.Size(66, 24);
            this.TerAcrylic.TabIndex = 207;
            this.TerAcrylic.Text = "Acrylic Back";
            this.TerAcrylic.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.TerAcrylic_CheckedChanged);
            // 
            // Button16
            // 
            this.Button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button16.CustomColor = System.Drawing.Color.Empty;
            this.Button16.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button16.ForeColor = System.Drawing.Color.White;
            this.Button16.Image = ((System.Drawing.Image)(resources.GetObject("Button16.Image")));
            this.Button16.Location = new System.Drawing.Point(306, 33);
            this.Button16.Name = "Button16";
            this.Button16.Size = new System.Drawing.Size(39, 25);
            this.Button16.TabIndex = 192;
            this.Button16.UseVisualStyleBackColor = false;
            this.Button16.Click += new System.EventHandler(this.Button16_Click);
            // 
            // TerBackImage
            // 
            this.TerBackImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerBackImage.ForeColor = System.Drawing.Color.White;
            this.TerBackImage.Location = new System.Drawing.Point(122, 6);
            this.TerBackImage.MaxLength = 32767;
            this.TerBackImage.Multiline = false;
            this.TerBackImage.Name = "TerBackImage";
            this.TerBackImage.ReadOnly = false;
            this.TerBackImage.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TerBackImage.SelectedText = "";
            this.TerBackImage.SelectionLength = 0;
            this.TerBackImage.SelectionStart = 0;
            this.TerBackImage.Size = new System.Drawing.Size(224, 24);
            this.TerBackImage.TabIndex = 191;
            this.TerBackImage.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TerBackImage.UseSystemPasswordChar = false;
            this.TerBackImage.WordWrap = true;
            this.TerBackImage.TextChanged += new System.EventHandler(this.TerBackImage_TextChanged);
            // 
            // TerImageOpacity
            // 
            this.TerImageOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerImageOpacity.BackColor = System.Drawing.Color.Transparent;
            this.TerImageOpacity.LargeChange = 10;
            this.TerImageOpacity.Location = new System.Drawing.Point(107, 65);
            this.TerImageOpacity.Maximum = 100;
            this.TerImageOpacity.Minimum = 0;
            this.TerImageOpacity.Name = "TerImageOpacity";
            this.TerImageOpacity.Size = new System.Drawing.Size(197, 19);
            this.TerImageOpacity.SmallChange = 1;
            this.TerImageOpacity.TabIndex = 187;
            this.TerImageOpacity.Value = 100;
            this.TerImageOpacity.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.TerImageOpacity_Scroll);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(36, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(80, 24);
            this.Label2.TabIndex = 196;
            this.Label2.Text = "Background:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(12, 605);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(332, 24);
            this.CheckBox1.TabIndex = 104;
            this.CheckBox1.Text = "Allow non monospace fonts (causes wrong renderering)";
            this.CheckBox1.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox1_CheckedChanged);
            // 
            // Button20
            // 
            this.Button20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button20.CustomColor = System.Drawing.Color.Empty;
            this.Button20.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button20.ForeColor = System.Drawing.Color.White;
            this.Button20.Image = ((System.Drawing.Image)(resources.GetObject("Button20.Image")));
            this.Button20.Location = new System.Drawing.Point(108, 6);
            this.Button20.Name = "Button20";
            this.Button20.Size = new System.Drawing.Size(87, 24);
            this.Button20.TabIndex = 217;
            this.Button20.Text = "Copycat";
            this.Button20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button20.UseVisualStyleBackColor = false;
            this.Button20.Click += new System.EventHandler(this.Button20_Click);
            // 
            // Button17
            // 
            this.Button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button17.CustomColor = System.Drawing.Color.Empty;
            this.Button17.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button17.ForeColor = System.Drawing.Color.White;
            this.Button17.Image = ((System.Drawing.Image)(resources.GetObject("Button17.Image")));
            this.Button17.Location = new System.Drawing.Point(197, 6);
            this.Button17.Name = "Button17";
            this.Button17.Size = new System.Drawing.Size(76, 24);
            this.Button17.TabIndex = 214;
            this.Button17.Text = "Clone";
            this.Button17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button17.UseVisualStyleBackColor = false;
            this.Button17.Click += new System.EventHandler(this.Button17_Click);
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.Location = new System.Drawing.Point(275, 6);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(35, 24);
            this.Button4.TabIndex = 213;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button12
            // 
            this.Button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button12.CustomColor = System.Drawing.Color.Empty;
            this.Button12.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = ((System.Drawing.Image)(resources.GetObject("Button12.Image")));
            this.Button12.Location = new System.Drawing.Point(312, 6);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(35, 24);
            this.Button12.TabIndex = 129;
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // TerSchemes
            // 
            this.TerSchemes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerSchemes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.TerSchemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TerSchemes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerSchemes.ForeColor = System.Drawing.Color.White;
            this.TerSchemes.FormattingEnabled = true;
            this.TerSchemes.ItemHeight = 20;
            this.TerSchemes.Location = new System.Drawing.Point(5, 33);
            this.TerSchemes.Name = "TerSchemes";
            this.TerSchemes.Size = new System.Drawing.Size(342, 26);
            this.TerSchemes.TabIndex = 117;
            this.TerSchemes.SelectedIndexChanged += new System.EventHandler(this.TerSchemes_SelectedIndexChanged);
            // 
            // TerCursor
            // 
            this.TerCursor.AllowDrop = true;
            this.TerCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerCursor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerCursor.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerCursor.DontShowInfo = false;
            this.TerCursor.Location = new System.Drawing.Point(245, 153);
            this.TerCursor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerCursor.Name = "TerCursor";
            this.TerCursor.Size = new System.Drawing.Size(100, 24);
            this.TerCursor.TabIndex = 125;
            this.TerCursor.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerCursor.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // TerWhiteB
            // 
            this.TerWhiteB.AllowDrop = true;
            this.TerWhiteB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerWhiteB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.TerWhiteB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.TerWhiteB.DontShowInfo = false;
            this.TerWhiteB.Location = new System.Drawing.Point(245, 401);
            this.TerWhiteB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerWhiteB.Name = "TerWhiteB";
            this.TerWhiteB.Size = new System.Drawing.Size(100, 24);
            this.TerWhiteB.TabIndex = 115;
            this.TerWhiteB.Click += new System.EventHandler(this.ColorClick);
            this.TerWhiteB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label150
            // 
            this.Label150.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label150.BackColor = System.Drawing.Color.Transparent;
            this.Label150.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label150.Location = new System.Drawing.Point(36, 153);
            this.Label150.Name = "Label150";
            this.Label150.Size = new System.Drawing.Size(202, 24);
            this.Label150.TabIndex = 124;
            this.Label150.Text = "Cursor";
            this.Label150.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerBlue
            // 
            this.TerBlue.AllowDrop = true;
            this.TerBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerBlue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(218)))));
            this.TerBlue.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(218)))));
            this.TerBlue.DontShowInfo = false;
            this.TerBlue.Location = new System.Drawing.Point(140, 222);
            this.TerBlue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerBlue.Name = "TerBlue";
            this.TerBlue.Size = new System.Drawing.Size(100, 24);
            this.TerBlue.TabIndex = 101;
            this.TerBlue.Click += new System.EventHandler(this.ColorClick);
            this.TerBlue.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label151
            // 
            this.Label151.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label151.BackColor = System.Drawing.Color.Transparent;
            this.Label151.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label151.Location = new System.Drawing.Point(36, 401);
            this.Label151.Name = "Label151";
            this.Label151.Size = new System.Drawing.Size(97, 24);
            this.Label151.TabIndex = 91;
            this.Label151.Text = "White";
            this.Label151.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerSelection
            // 
            this.TerSelection.AllowDrop = true;
            this.TerSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerSelection.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerSelection.DontShowInfo = false;
            this.TerSelection.Location = new System.Drawing.Point(245, 123);
            this.TerSelection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerSelection.Name = "TerSelection";
            this.TerSelection.Size = new System.Drawing.Size(100, 24);
            this.TerSelection.TabIndex = 123;
            this.TerSelection.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerSelection.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // Label152
            // 
            this.Label152.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label152.BackColor = System.Drawing.Color.Transparent;
            this.Label152.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label152.Location = new System.Drawing.Point(36, 123);
            this.Label152.Name = "Label152";
            this.Label152.Size = new System.Drawing.Size(202, 24);
            this.Label152.TabIndex = 122;
            this.Label152.Text = "Selection";
            this.Label152.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label147
            // 
            this.Label147.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label147.BackColor = System.Drawing.Color.Transparent;
            this.Label147.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label147.Location = new System.Drawing.Point(36, 93);
            this.Label147.Name = "Label147";
            this.Label147.Size = new System.Drawing.Size(202, 24);
            this.Label147.TabIndex = 120;
            this.Label147.Text = "Foreground";
            this.Label147.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerWhite
            // 
            this.TerWhite.AllowDrop = true;
            this.TerWhite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerWhite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.TerWhite.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.TerWhite.DontShowInfo = false;
            this.TerWhite.Location = new System.Drawing.Point(140, 401);
            this.TerWhite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerWhite.Name = "TerWhite";
            this.TerWhite.Size = new System.Drawing.Size(100, 24);
            this.TerWhite.TabIndex = 107;
            this.TerWhite.Click += new System.EventHandler(this.ColorClick);
            this.TerWhite.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerForeground
            // 
            this.TerForeground.AllowDrop = true;
            this.TerForeground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerForeground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerForeground.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerForeground.DontShowInfo = false;
            this.TerForeground.Location = new System.Drawing.Point(245, 93);
            this.TerForeground.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerForeground.Name = "TerForeground";
            this.TerForeground.Size = new System.Drawing.Size(100, 24);
            this.TerForeground.TabIndex = 121;
            this.TerForeground.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerForeground.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // TerCyanB
            // 
            this.TerCyanB.AllowDrop = true;
            this.TerCyanB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerCyanB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.TerCyanB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.TerCyanB.DontShowInfo = false;
            this.TerCyanB.Location = new System.Drawing.Point(245, 251);
            this.TerCyanB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerCyanB.Name = "TerCyanB";
            this.TerCyanB.Size = new System.Drawing.Size(100, 24);
            this.TerCyanB.TabIndex = 111;
            this.TerCyanB.Click += new System.EventHandler(this.ColorClick);
            this.TerCyanB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label145
            // 
            this.Label145.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label145.BackColor = System.Drawing.Color.Transparent;
            this.Label145.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label145.Location = new System.Drawing.Point(36, 251);
            this.Label145.Name = "Label145";
            this.Label145.Size = new System.Drawing.Size(97, 24);
            this.Label145.TabIndex = 87;
            this.Label145.Text = "Cyan";
            this.Label145.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerCyan
            // 
            this.TerCyan.AllowDrop = true;
            this.TerCyan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerCyan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(150)))), ((int)(((byte)(221)))));
            this.TerCyan.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(150)))), ((int)(((byte)(221)))));
            this.TerCyan.DontShowInfo = false;
            this.TerCyan.Location = new System.Drawing.Point(140, 251);
            this.TerCyan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerCyan.Name = "TerCyan";
            this.TerCyan.Size = new System.Drawing.Size(100, 24);
            this.TerCyan.TabIndex = 103;
            this.TerCyan.Click += new System.EventHandler(this.ColorClick);
            this.TerCyan.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerGreen
            // 
            this.TerGreen.AllowDrop = true;
            this.TerGreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(161)))), ((int)(((byte)(14)))));
            this.TerGreen.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(161)))), ((int)(((byte)(14)))));
            this.TerGreen.DontShowInfo = false;
            this.TerGreen.Location = new System.Drawing.Point(140, 281);
            this.TerGreen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerGreen.Name = "TerGreen";
            this.TerGreen.Size = new System.Drawing.Size(100, 24);
            this.TerGreen.TabIndex = 102;
            this.TerGreen.Click += new System.EventHandler(this.ColorClick);
            this.TerGreen.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label144
            // 
            this.Label144.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label144.BackColor = System.Drawing.Color.Transparent;
            this.Label144.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label144.Location = new System.Drawing.Point(36, 281);
            this.Label144.Name = "Label144";
            this.Label144.Size = new System.Drawing.Size(97, 24);
            this.Label144.TabIndex = 86;
            this.Label144.Text = "Green";
            this.Label144.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerBackground
            // 
            this.TerBackground.AllowDrop = true;
            this.TerBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerBackground.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerBackground.DontShowInfo = false;
            this.TerBackground.Location = new System.Drawing.Point(245, 63);
            this.TerBackground.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerBackground.Name = "TerBackground";
            this.TerBackground.Size = new System.Drawing.Size(100, 24);
            this.TerBackground.TabIndex = 119;
            this.TerBackground.Click += new System.EventHandler(this.ColorMainsClick);
            this.TerBackground.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMainsClick);
            // 
            // TerYellow
            // 
            this.TerYellow.AllowDrop = true;
            this.TerYellow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerYellow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(156)))), ((int)(((byte)(0)))));
            this.TerYellow.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(156)))), ((int)(((byte)(0)))));
            this.TerYellow.DontShowInfo = false;
            this.TerYellow.Location = new System.Drawing.Point(140, 371);
            this.TerYellow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerYellow.Name = "TerYellow";
            this.TerYellow.Size = new System.Drawing.Size(100, 24);
            this.TerYellow.TabIndex = 106;
            this.TerYellow.Click += new System.EventHandler(this.ColorClick);
            this.TerYellow.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerGreenB
            // 
            this.TerGreenB.AllowDrop = true;
            this.TerGreenB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerGreenB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(198)))), ((int)(((byte)(12)))));
            this.TerGreenB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(198)))), ((int)(((byte)(12)))));
            this.TerGreenB.DontShowInfo = false;
            this.TerGreenB.Location = new System.Drawing.Point(245, 281);
            this.TerGreenB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerGreenB.Name = "TerGreenB";
            this.TerGreenB.Size = new System.Drawing.Size(100, 24);
            this.TerGreenB.TabIndex = 110;
            this.TerGreenB.Click += new System.EventHandler(this.ColorClick);
            this.TerGreenB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label143
            // 
            this.Label143.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label143.BackColor = System.Drawing.Color.Transparent;
            this.Label143.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label143.Location = new System.Drawing.Point(36, 63);
            this.Label143.Name = "Label143";
            this.Label143.Size = new System.Drawing.Size(202, 24);
            this.Label143.TabIndex = 118;
            this.Label143.Text = "Background";
            this.Label143.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label149
            // 
            this.Label149.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label149.BackColor = System.Drawing.Color.Transparent;
            this.Label149.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label149.Location = new System.Drawing.Point(36, 371);
            this.Label149.Name = "Label149";
            this.Label149.Size = new System.Drawing.Size(97, 24);
            this.Label149.TabIndex = 90;
            this.Label149.Text = "Yellow";
            this.Label149.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerBlack
            // 
            this.TerBlack.AllowDrop = true;
            this.TerBlack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerBlack.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerBlack.DontShowInfo = false;
            this.TerBlack.Location = new System.Drawing.Point(140, 191);
            this.TerBlack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerBlack.Name = "TerBlack";
            this.TerBlack.Size = new System.Drawing.Size(100, 24);
            this.TerBlack.TabIndex = 100;
            this.TerBlack.Click += new System.EventHandler(this.ColorClick);
            this.TerBlack.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerYellowB
            // 
            this.TerYellowB.AllowDrop = true;
            this.TerYellowB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerYellowB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(165)))));
            this.TerYellowB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(165)))));
            this.TerYellowB.DontShowInfo = false;
            this.TerYellowB.Location = new System.Drawing.Point(245, 371);
            this.TerYellowB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerYellowB.Name = "TerYellowB";
            this.TerYellowB.Size = new System.Drawing.Size(100, 24);
            this.TerYellowB.TabIndex = 114;
            this.TerYellowB.Click += new System.EventHandler(this.ColorClick);
            this.TerYellowB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label142
            // 
            this.Label142.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label142.BackColor = System.Drawing.Color.Transparent;
            this.Label142.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label142.Location = new System.Drawing.Point(36, 221);
            this.Label142.Name = "Label142";
            this.Label142.Size = new System.Drawing.Size(97, 24);
            this.Label142.TabIndex = 85;
            this.Label142.Text = "Blue";
            this.Label142.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerBlackB
            // 
            this.TerBlackB.AllowDrop = true;
            this.TerBlackB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerBlackB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this.TerBlackB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this.TerBlackB.DontShowInfo = false;
            this.TerBlackB.Location = new System.Drawing.Point(245, 191);
            this.TerBlackB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerBlackB.Name = "TerBlackB";
            this.TerBlackB.Size = new System.Drawing.Size(100, 24);
            this.TerBlackB.TabIndex = 108;
            this.TerBlackB.Click += new System.EventHandler(this.ColorClick);
            this.TerBlackB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerPurple
            // 
            this.TerPurple.AllowDrop = true;
            this.TerPurple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerPurple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(23)))), ((int)(((byte)(152)))));
            this.TerPurple.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(23)))), ((int)(((byte)(152)))));
            this.TerPurple.DontShowInfo = false;
            this.TerPurple.Location = new System.Drawing.Point(140, 341);
            this.TerPurple.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerPurple.Name = "TerPurple";
            this.TerPurple.Size = new System.Drawing.Size(100, 24);
            this.TerPurple.TabIndex = 105;
            this.TerPurple.Click += new System.EventHandler(this.ColorClick);
            this.TerPurple.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label140
            // 
            this.Label140.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label140.BackColor = System.Drawing.Color.Transparent;
            this.Label140.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label140.Location = new System.Drawing.Point(36, 191);
            this.Label140.Name = "Label140";
            this.Label140.Size = new System.Drawing.Size(97, 24);
            this.Label140.TabIndex = 4;
            this.Label140.Text = "Black";
            this.Label140.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerPurpleB
            // 
            this.TerPurpleB.AllowDrop = true;
            this.TerPurpleB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerPurpleB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(158)))));
            this.TerPurpleB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(158)))));
            this.TerPurpleB.DontShowInfo = false;
            this.TerPurpleB.Location = new System.Drawing.Point(245, 341);
            this.TerPurpleB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerPurpleB.Name = "TerPurpleB";
            this.TerPurpleB.Size = new System.Drawing.Size(100, 24);
            this.TerPurpleB.TabIndex = 113;
            this.TerPurpleB.Click += new System.EventHandler(this.ColorClick);
            this.TerPurpleB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerBlueB
            // 
            this.TerBlueB.AllowDrop = true;
            this.TerBlueB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerBlueB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.TerBlueB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.TerBlueB.DontShowInfo = false;
            this.TerBlueB.Location = new System.Drawing.Point(245, 222);
            this.TerBlueB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerBlueB.Name = "TerBlueB";
            this.TerBlueB.Size = new System.Drawing.Size(100, 24);
            this.TerBlueB.TabIndex = 109;
            this.TerBlueB.Click += new System.EventHandler(this.ColorClick);
            this.TerBlueB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // Label146
            // 
            this.Label146.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label146.BackColor = System.Drawing.Color.Transparent;
            this.Label146.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label146.Location = new System.Drawing.Point(36, 311);
            this.Label146.Name = "Label146";
            this.Label146.Size = new System.Drawing.Size(97, 24);
            this.Label146.TabIndex = 88;
            this.Label146.Text = "Red";
            this.Label146.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label148
            // 
            this.Label148.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label148.BackColor = System.Drawing.Color.Transparent;
            this.Label148.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label148.Location = new System.Drawing.Point(36, 341);
            this.Label148.Name = "Label148";
            this.Label148.Size = new System.Drawing.Size(97, 24);
            this.Label148.TabIndex = 89;
            this.Label148.Text = "Purple";
            this.Label148.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerRedB
            // 
            this.TerRedB.AllowDrop = true;
            this.TerRedB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerRedB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(72)))), ((int)(((byte)(86)))));
            this.TerRedB.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(72)))), ((int)(((byte)(86)))));
            this.TerRedB.DontShowInfo = false;
            this.TerRedB.Location = new System.Drawing.Point(245, 311);
            this.TerRedB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerRedB.Name = "TerRedB";
            this.TerRedB.Size = new System.Drawing.Size(100, 24);
            this.TerRedB.TabIndex = 112;
            this.TerRedB.Click += new System.EventHandler(this.ColorClick);
            this.TerRedB.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerRed
            // 
            this.TerRed.AllowDrop = true;
            this.TerRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerRed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(15)))), ((int)(((byte)(31)))));
            this.TerRed.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(15)))), ((int)(((byte)(31)))));
            this.TerRed.DontShowInfo = false;
            this.TerRed.Location = new System.Drawing.Point(140, 311);
            this.TerRed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerRed.Name = "TerRed";
            this.TerRed.Size = new System.Drawing.Size(100, 24);
            this.TerRed.TabIndex = 104;
            this.TerRed.Click += new System.EventHandler(this.ColorClick);
            this.TerRed.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorClick);
            // 
            // TerCursorHeightBar
            // 
            this.TerCursorHeightBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerCursorHeightBar.BackColor = System.Drawing.Color.Transparent;
            this.TerCursorHeightBar.LargeChange = 1;
            this.TerCursorHeightBar.Location = new System.Drawing.Point(96, 39);
            this.TerCursorHeightBar.Maximum = 100;
            this.TerCursorHeightBar.Minimum = 1;
            this.TerCursorHeightBar.Name = "TerCursorHeightBar";
            this.TerCursorHeightBar.Size = new System.Drawing.Size(210, 19);
            this.TerCursorHeightBar.SmallChange = 1;
            this.TerCursorHeightBar.TabIndex = 102;
            this.TerCursorHeightBar.Value = 20;
            this.TerCursorHeightBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.TerCursorHeightBar_Scroll);
            // 
            // TerCursorStyle
            // 
            this.TerCursorStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerCursorStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.TerCursorStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TerCursorStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerCursorStyle.ForeColor = System.Drawing.Color.White;
            this.TerCursorStyle.FormattingEnabled = true;
            this.TerCursorStyle.ItemHeight = 20;
            this.TerCursorStyle.Items.AddRange(new object[] {
            "Bar",
            "Double Underscore",
            "Empty Box",
            "Filled Box",
            "Underscore",
            "Vintage"});
            this.TerCursorStyle.Location = new System.Drawing.Point(96, 5);
            this.TerCursorStyle.Name = "TerCursorStyle";
            this.TerCursorStyle.Size = new System.Drawing.Size(250, 26);
            this.TerCursorStyle.TabIndex = 110;
            this.TerCursorStyle.SelectedIndexChanged += new System.EventHandler(this.TerCursorStyle_SelectedIndexChanged);
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button10.CustomColor = System.Drawing.Color.Empty;
            this.Button10.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = ((System.Drawing.Image)(resources.GetObject("Button10.Image")));
            this.Button10.Location = new System.Drawing.Point(786, 600);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(124, 34);
            this.Button10.TabIndex = 107;
            this.Button10.Text = "Quick apply";
            this.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // GroupBox13
            // 
            this.GroupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox13.Controls.Add(this.Button6);
            this.GroupBox13.Controls.Add(this.Button18);
            this.GroupBox13.Controls.Add(this.Button14);
            this.GroupBox13.Controls.Add(this.PictureBox25);
            this.GroupBox13.Controls.Add(this.Label155);
            this.GroupBox13.Controls.Add(this.TerProfiles);
            this.GroupBox13.Controls.Add(this.Button13);
            this.GroupBox13.Location = new System.Drawing.Point(12, 106);
            this.GroupBox13.Name = "GroupBox13";
            this.GroupBox13.Size = new System.Drawing.Size(460, 40);
            this.GroupBox13.TabIndex = 117;
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.Location = new System.Drawing.Point(328, 5);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(30, 30);
            this.Button6.TabIndex = 216;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button18
            // 
            this.Button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button18.CustomColor = System.Drawing.Color.Empty;
            this.Button18.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button18.ForeColor = System.Drawing.Color.White;
            this.Button18.Image = ((System.Drawing.Image)(resources.GetObject("Button18.Image")));
            this.Button18.Location = new System.Drawing.Point(360, 5);
            this.Button18.Name = "Button18";
            this.Button18.Size = new System.Drawing.Size(30, 30);
            this.Button18.TabIndex = 215;
            this.Button18.UseVisualStyleBackColor = false;
            this.Button18.Click += new System.EventHandler(this.Button18_Click);
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.CustomColor = System.Drawing.Color.Empty;
            this.Button14.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = ((System.Drawing.Image)(resources.GetObject("Button14.Image")));
            this.Button14.Location = new System.Drawing.Point(392, 5);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(30, 30);
            this.Button14.TabIndex = 179;
            this.Button14.UseVisualStyleBackColor = false;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // PictureBox25
            // 
            this.PictureBox25.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox25.Image")));
            this.PictureBox25.Location = new System.Drawing.Point(4, 5);
            this.PictureBox25.Name = "PictureBox25";
            this.PictureBox25.Size = new System.Drawing.Size(35, 31);
            this.PictureBox25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox25.TabIndex = 83;
            this.PictureBox25.TabStop = false;
            // 
            // Label155
            // 
            this.Label155.BackColor = System.Drawing.Color.Transparent;
            this.Label155.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label155.Location = new System.Drawing.Point(41, 5);
            this.Label155.Name = "Label155";
            this.Label155.Size = new System.Drawing.Size(62, 31);
            this.Label155.TabIndex = 84;
            this.Label155.Text = "Profiles:";
            this.Label155.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerProfiles
            // 
            this.TerProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerProfiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.TerProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TerProfiles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerProfiles.ForeColor = System.Drawing.Color.White;
            this.TerProfiles.FormattingEnabled = true;
            this.TerProfiles.ItemHeight = 20;
            this.TerProfiles.Location = new System.Drawing.Point(108, 7);
            this.TerProfiles.Name = "TerProfiles";
            this.TerProfiles.Size = new System.Drawing.Size(216, 26);
            this.TerProfiles.TabIndex = 118;
            this.TerProfiles.SelectedIndexChanged += new System.EventHandler(this.TerProfiles_SelectedIndexChanged);
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.CustomColor = System.Drawing.Color.Empty;
            this.Button13.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button13.ForeColor = System.Drawing.Color.White;
            this.Button13.Image = ((System.Drawing.Image)(resources.GetObject("Button13.Image")));
            this.Button13.Location = new System.Drawing.Point(424, 5);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(30, 30);
            this.Button13.TabIndex = 140;
            this.Button13.UseVisualStyleBackColor = false;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // TerFontSizeBar
            // 
            this.TerFontSizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerFontSizeBar.BackColor = System.Drawing.Color.Transparent;
            this.TerFontSizeBar.LargeChange = 10;
            this.TerFontSizeBar.Location = new System.Drawing.Point(96, 69);
            this.TerFontSizeBar.Maximum = 48;
            this.TerFontSizeBar.Minimum = 5;
            this.TerFontSizeBar.Name = "TerFontSizeBar";
            this.TerFontSizeBar.Size = new System.Drawing.Size(210, 19);
            this.TerFontSizeBar.SmallChange = 1;
            this.TerFontSizeBar.TabIndex = 101;
            this.TerFontSizeBar.Value = 5;
            this.TerFontSizeBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.TerFontSizeBar_Scroll);
            // 
            // TerFontWeight
            // 
            this.TerFontWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerFontWeight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.TerFontWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TerFontWeight.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerFontWeight.ForeColor = System.Drawing.Color.White;
            this.TerFontWeight.FormattingEnabled = true;
            this.TerFontWeight.ItemHeight = 20;
            this.TerFontWeight.Items.AddRange(new object[] {
            "Thin",
            "Extra Light",
            "Light",
            "Semi Light",
            "Normal",
            "Medium",
            "Semi Bold",
            "Bold",
            "Extra Bold",
            "Black",
            "Extra Black"});
            this.TerFontWeight.Location = new System.Drawing.Point(96, 35);
            this.TerFontWeight.Name = "TerFontWeight";
            this.TerFontWeight.Size = new System.Drawing.Size(250, 26);
            this.TerFontWeight.TabIndex = 99;
            this.TerFontWeight.SelectedIndexChanged += new System.EventHandler(this.TerFontWeight_SelectedIndexChanged);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.Button22);
            this.GroupBox3.Controls.Add(this.Separator2);
            this.GroupBox3.Controls.Add(this.Button11);
            this.GroupBox3.Controls.Add(this.Label11);
            this.GroupBox3.Controls.Add(this.Button9);
            this.GroupBox3.Controls.Add(this.checker_img);
            this.GroupBox3.Controls.Add(this.AlertBox1);
            this.GroupBox3.Controls.Add(this.Button8);
            this.GroupBox3.Controls.Add(this.Button7);
            this.GroupBox3.Controls.Add(this.TerEnabled);
            this.GroupBox3.Location = new System.Drawing.Point(12, 12);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(984, 81);
            this.GroupBox3.TabIndex = 199;
            // 
            // Button22
            // 
            this.Button22.CustomColor = System.Drawing.Color.Empty;
            this.Button22.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button22.ForeColor = System.Drawing.Color.White;
            this.Button22.Image = ((System.Drawing.Image)(resources.GetObject("Button22.Image")));
            this.Button22.Location = new System.Drawing.Point(223, 6);
            this.Button22.Name = "Button22";
            this.Button22.Size = new System.Drawing.Size(146, 29);
            this.Button22.TabIndex = 202;
            this.Button22.Text = "Current applied one";
            this.Button22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button22.UseVisualStyleBackColor = false;
            this.Button22.Click += new System.EventHandler(this.Button22_Click);
            // 
            // Separator2
            // 
            this.Separator2.AlternativeLook = false;
            this.Separator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator2.BackColor = System.Drawing.Color.Transparent;
            this.Separator2.Location = new System.Drawing.Point(4, 40);
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(973, 1);
            this.Separator2.TabIndex = 201;
            this.Separator2.TabStop = false;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(4, 4);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(75, 32);
            this.Label11.TabIndex = 111;
            this.Label11.Text = "Open from:";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(891, 5);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // TabControl1
            // 
            this.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabControl1.AllowDrop = true;
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage5);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TabControl1.ItemSize = new System.Drawing.Size(35, 100);
            this.TabControl1.Location = new System.Drawing.Point(10, 148);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(471, 454);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.TabIndex = 201;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage1.Controls.Add(this.GroupBox4);
            this.TabPage1.Location = new System.Drawing.Point(104, 4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(363, 446);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Colors";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox4.Controls.Add(this.TerWhiteB);
            this.GroupBox4.Controls.Add(this.PictureBox29);
            this.GroupBox4.Controls.Add(this.TerWhite);
            this.GroupBox4.Controls.Add(this.Label151);
            this.GroupBox4.Controls.Add(this.TerYellow);
            this.GroupBox4.Controls.Add(this.TerYellowB);
            this.GroupBox4.Controls.Add(this.TerGreen);
            this.GroupBox4.Controls.Add(this.TerCyanB);
            this.GroupBox4.Controls.Add(this.TerGreenB);
            this.GroupBox4.Controls.Add(this.TerPurple);
            this.GroupBox4.Controls.Add(this.PictureBox30);
            this.GroupBox4.Controls.Add(this.TerPurpleB);
            this.GroupBox4.Controls.Add(this.TerCyan);
            this.GroupBox4.Controls.Add(this.PictureBox31);
            this.GroupBox4.Controls.Add(this.TerRedB);
            this.GroupBox4.Controls.Add(this.PictureBox34);
            this.GroupBox4.Controls.Add(this.TerRed);
            this.GroupBox4.Controls.Add(this.PictureBox26);
            this.GroupBox4.Controls.Add(this.PictureBox28);
            this.GroupBox4.Controls.Add(this.PictureBox23);
            this.GroupBox4.Controls.Add(this.TerBlue);
            this.GroupBox4.Controls.Add(this.Label149);
            this.GroupBox4.Controls.Add(this.Label145);
            this.GroupBox4.Controls.Add(this.PictureBox22);
            this.GroupBox4.Controls.Add(this.Label144);
            this.GroupBox4.Controls.Add(this.Separator3);
            this.GroupBox4.Controls.Add(this.TerCursor);
            this.GroupBox4.Controls.Add(this.PictureBox21);
            this.GroupBox4.Controls.Add(this.Label146);
            this.GroupBox4.Controls.Add(this.Label148);
            this.GroupBox4.Controls.Add(this.PictureBox20);
            this.GroupBox4.Controls.Add(this.Label150);
            this.GroupBox4.Controls.Add(this.PictureBox19);
            this.GroupBox4.Controls.Add(this.TerSelection);
            this.GroupBox4.Controls.Add(this.PictureBox15);
            this.GroupBox4.Controls.Add(this.TerSchemes);
            this.GroupBox4.Controls.Add(this.TerForeground);
            this.GroupBox4.Controls.Add(this.Button20);
            this.GroupBox4.Controls.Add(this.Label142);
            this.GroupBox4.Controls.Add(this.TerBlack);
            this.GroupBox4.Controls.Add(this.Label152);
            this.GroupBox4.Controls.Add(this.PictureBox14);
            this.GroupBox4.Controls.Add(this.TerBlueB);
            this.GroupBox4.Controls.Add(this.TerBlackB);
            this.GroupBox4.Controls.Add(this.Label147);
            this.GroupBox4.Controls.Add(this.Button17);
            this.GroupBox4.Controls.Add(this.Button4);
            this.GroupBox4.Controls.Add(this.Button12);
            this.GroupBox4.Controls.Add(this.Label19);
            this.GroupBox4.Controls.Add(this.Label140);
            this.GroupBox4.Controls.Add(this.Label143);
            this.GroupBox4.Controls.Add(this.TerBackground);
            this.GroupBox4.Location = new System.Drawing.Point(6, 6);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(351, 432);
            this.GroupBox4.TabIndex = 87;
            // 
            // PictureBox29
            // 
            this.PictureBox29.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox29.Image")));
            this.PictureBox29.Location = new System.Drawing.Point(6, 401);
            this.PictureBox29.Name = "PictureBox29";
            this.PictureBox29.Size = new System.Drawing.Size(24, 24);
            this.PictureBox29.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox29.TabIndex = 233;
            this.PictureBox29.TabStop = false;
            // 
            // PictureBox30
            // 
            this.PictureBox30.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox30.Image")));
            this.PictureBox30.Location = new System.Drawing.Point(6, 371);
            this.PictureBox30.Name = "PictureBox30";
            this.PictureBox30.Size = new System.Drawing.Size(24, 24);
            this.PictureBox30.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox30.TabIndex = 232;
            this.PictureBox30.TabStop = false;
            // 
            // PictureBox31
            // 
            this.PictureBox31.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox31.Image")));
            this.PictureBox31.Location = new System.Drawing.Point(6, 341);
            this.PictureBox31.Name = "PictureBox31";
            this.PictureBox31.Size = new System.Drawing.Size(24, 24);
            this.PictureBox31.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox31.TabIndex = 231;
            this.PictureBox31.TabStop = false;
            // 
            // PictureBox34
            // 
            this.PictureBox34.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox34.Image")));
            this.PictureBox34.Location = new System.Drawing.Point(6, 311);
            this.PictureBox34.Name = "PictureBox34";
            this.PictureBox34.Size = new System.Drawing.Size(24, 24);
            this.PictureBox34.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox34.TabIndex = 230;
            this.PictureBox34.TabStop = false;
            // 
            // PictureBox26
            // 
            this.PictureBox26.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox26.Image")));
            this.PictureBox26.Location = new System.Drawing.Point(6, 281);
            this.PictureBox26.Name = "PictureBox26";
            this.PictureBox26.Size = new System.Drawing.Size(24, 24);
            this.PictureBox26.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox26.TabIndex = 229;
            this.PictureBox26.TabStop = false;
            // 
            // PictureBox28
            // 
            this.PictureBox28.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox28.Image")));
            this.PictureBox28.Location = new System.Drawing.Point(6, 251);
            this.PictureBox28.Name = "PictureBox28";
            this.PictureBox28.Size = new System.Drawing.Size(24, 24);
            this.PictureBox28.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox28.TabIndex = 228;
            this.PictureBox28.TabStop = false;
            // 
            // PictureBox23
            // 
            this.PictureBox23.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox23.Image")));
            this.PictureBox23.Location = new System.Drawing.Point(6, 221);
            this.PictureBox23.Name = "PictureBox23";
            this.PictureBox23.Size = new System.Drawing.Size(24, 24);
            this.PictureBox23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox23.TabIndex = 225;
            this.PictureBox23.TabStop = false;
            // 
            // PictureBox22
            // 
            this.PictureBox22.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox22.Image")));
            this.PictureBox22.Location = new System.Drawing.Point(6, 191);
            this.PictureBox22.Name = "PictureBox22";
            this.PictureBox22.Size = new System.Drawing.Size(24, 24);
            this.PictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox22.TabIndex = 223;
            this.PictureBox22.TabStop = false;
            // 
            // Separator3
            // 
            this.Separator3.AlternativeLook = false;
            this.Separator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator3.BackColor = System.Drawing.Color.Transparent;
            this.Separator3.Location = new System.Drawing.Point(6, 184);
            this.Separator3.Name = "Separator3";
            this.Separator3.Size = new System.Drawing.Size(341, 1);
            this.Separator3.TabIndex = 222;
            this.Separator3.TabStop = false;
            // 
            // PictureBox21
            // 
            this.PictureBox21.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox21.Image")));
            this.PictureBox21.Location = new System.Drawing.Point(6, 153);
            this.PictureBox21.Name = "PictureBox21";
            this.PictureBox21.Size = new System.Drawing.Size(24, 24);
            this.PictureBox21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox21.TabIndex = 221;
            this.PictureBox21.TabStop = false;
            // 
            // PictureBox20
            // 
            this.PictureBox20.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox20.Image")));
            this.PictureBox20.Location = new System.Drawing.Point(6, 123);
            this.PictureBox20.Name = "PictureBox20";
            this.PictureBox20.Size = new System.Drawing.Size(24, 24);
            this.PictureBox20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox20.TabIndex = 220;
            this.PictureBox20.TabStop = false;
            // 
            // PictureBox19
            // 
            this.PictureBox19.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox19.Image")));
            this.PictureBox19.Location = new System.Drawing.Point(6, 93);
            this.PictureBox19.Name = "PictureBox19";
            this.PictureBox19.Size = new System.Drawing.Size(24, 24);
            this.PictureBox19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox19.TabIndex = 219;
            this.PictureBox19.TabStop = false;
            // 
            // PictureBox15
            // 
            this.PictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox15.Image")));
            this.PictureBox15.Location = new System.Drawing.Point(6, 63);
            this.PictureBox15.Name = "PictureBox15";
            this.PictureBox15.Size = new System.Drawing.Size(24, 24);
            this.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox15.TabIndex = 218;
            this.PictureBox15.TabStop = false;
            // 
            // PictureBox14
            // 
            this.PictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox14.Image")));
            this.PictureBox14.Location = new System.Drawing.Point(6, 6);
            this.PictureBox14.Name = "PictureBox14";
            this.PictureBox14.Size = new System.Drawing.Size(24, 24);
            this.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox14.TabIndex = 201;
            this.PictureBox14.TabStop = false;
            // 
            // Label19
            // 
            this.Label19.BackColor = System.Drawing.Color.Transparent;
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(36, 6);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(65, 24);
            this.Label19.TabIndex = 84;
            this.Label19.Text = "Scheme:";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage5.Controls.Add(this.GroupBox2);
            this.TabPage5.Location = new System.Drawing.Point(104, 4);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(363, 446);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Theme";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox2.Controls.Add(this.TerThemes);
            this.GroupBox2.Controls.Add(this.Button21);
            this.GroupBox2.Controls.Add(this.TerThemesContainer);
            this.GroupBox2.Controls.Add(this.PictureBox37);
            this.GroupBox2.Controls.Add(this.Button19);
            this.GroupBox2.Controls.Add(this.Label20);
            this.GroupBox2.Controls.Add(this.TerEditThemeName);
            this.GroupBox2.Controls.Add(this.Button3);
            this.GroupBox2.Location = new System.Drawing.Point(6, 6);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(351, 212);
            this.GroupBox2.TabIndex = 88;
            // 
            // PictureBox37
            // 
            this.PictureBox37.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox37.Image")));
            this.PictureBox37.Location = new System.Drawing.Point(6, 6);
            this.PictureBox37.Name = "PictureBox37";
            this.PictureBox37.Size = new System.Drawing.Size(24, 24);
            this.PictureBox37.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox37.TabIndex = 201;
            this.PictureBox37.TabStop = false;
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(36, 6);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(65, 24);
            this.Label20.TabIndex = 84;
            this.Label20.Text = "Theme:";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage2.Controls.Add(this.GroupBox5);
            this.TabPage2.Location = new System.Drawing.Point(104, 4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(363, 446);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Fonts";
            // 
            // GroupBox5
            // 
            this.GroupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox5.Controls.Add(this.TerFontName);
            this.GroupBox5.Controls.Add(this.Button23);
            this.GroupBox5.Controls.Add(this.TerFontSizeVal);
            this.GroupBox5.Controls.Add(this.PictureBox5);
            this.GroupBox5.Controls.Add(this.TerFontWeight);
            this.GroupBox5.Controls.Add(this.TerFontSizeBar);
            this.GroupBox5.Controls.Add(this.Label61);
            this.GroupBox5.Controls.Add(this.PictureBox8);
            this.GroupBox5.Controls.Add(this.Label35);
            this.GroupBox5.Controls.Add(this.PictureBox9);
            this.GroupBox5.Controls.Add(this.Label59);
            this.GroupBox5.Location = new System.Drawing.Point(6, 6);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(351, 97);
            this.GroupBox5.TabIndex = 98;
            // 
            // TerFontName
            // 
            this.TerFontName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerFontName.BackColor = System.Drawing.Color.Transparent;
            this.TerFontName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TerFontName.Location = new System.Drawing.Point(96, 6);
            this.TerFontName.Name = "TerFontName";
            this.TerFontName.Size = new System.Drawing.Size(210, 24);
            this.TerFontName.TabIndex = 138;
            this.TerFontName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button23
            // 
            this.Button23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button23.CustomColor = System.Drawing.Color.Empty;
            this.Button23.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button23.ForeColor = System.Drawing.Color.White;
            this.Button23.Image = null;
            this.Button23.Location = new System.Drawing.Point(316, 6);
            this.Button23.Name = "Button23";
            this.Button23.Size = new System.Drawing.Size(30, 24);
            this.Button23.TabIndex = 137;
            this.Button23.Text = "...";
            this.Button23.UseVisualStyleBackColor = false;
            this.Button23.Click += new System.EventHandler(this.Button23_Click);
            // 
            // TerFontSizeVal
            // 
            this.TerFontSizeVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerFontSizeVal.CustomColor = System.Drawing.Color.Empty;
            this.TerFontSizeVal.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.TerFontSizeVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerFontSizeVal.ForeColor = System.Drawing.Color.White;
            this.TerFontSizeVal.Image = null;
            this.TerFontSizeVal.Location = new System.Drawing.Point(312, 66);
            this.TerFontSizeVal.Name = "TerFontSizeVal";
            this.TerFontSizeVal.Size = new System.Drawing.Size(34, 24);
            this.TerFontSizeVal.TabIndex = 132;
            this.TerFontSizeVal.UseVisualStyleBackColor = false;
            this.TerFontSizeVal.Click += new System.EventHandler(this.TerFontSizeVal_Click);
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(6, 6);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 99;
            this.PictureBox5.TabStop = false;
            // 
            // Label61
            // 
            this.Label61.BackColor = System.Drawing.Color.Transparent;
            this.Label61.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label61.Location = new System.Drawing.Point(36, 36);
            this.Label61.Name = "Label61";
            this.Label61.Size = new System.Drawing.Size(54, 24);
            this.Label61.TabIndex = 97;
            this.Label61.Text = "Weight:";
            this.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox8
            // 
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(6, 36);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 100;
            this.PictureBox8.TabStop = false;
            // 
            // Label35
            // 
            this.Label35.BackColor = System.Drawing.Color.Transparent;
            this.Label35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(36, 66);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(54, 24);
            this.Label35.TabIndex = 103;
            this.Label35.Text = "Size:";
            this.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(6, 66);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 101;
            this.PictureBox9.TabStop = false;
            // 
            // Label59
            // 
            this.Label59.BackColor = System.Drawing.Color.Transparent;
            this.Label59.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label59.Location = new System.Drawing.Point(36, 6);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(54, 24);
            this.Label59.TabIndex = 84;
            this.Label59.Text = "Font:";
            this.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.GroupBox34);
            this.TabPage3.Location = new System.Drawing.Point(104, 4);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(363, 446);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Cursor";
            // 
            // GroupBox34
            // 
            this.GroupBox34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox34.Controls.Add(this.PictureBox10);
            this.GroupBox34.Controls.Add(this.TerCursorHeightVal);
            this.GroupBox34.Controls.Add(this.Label60);
            this.GroupBox34.Controls.Add(this.PictureBox11);
            this.GroupBox34.Controls.Add(this.Label14);
            this.GroupBox34.Controls.Add(this.TerCursorHeightBar);
            this.GroupBox34.Controls.Add(this.TerCursorStyle);
            this.GroupBox34.Location = new System.Drawing.Point(6, 6);
            this.GroupBox34.Name = "GroupBox34";
            this.GroupBox34.Size = new System.Drawing.Size(351, 67);
            this.GroupBox34.TabIndex = 99;
            // 
            // PictureBox10
            // 
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(6, 6);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(24, 24);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 104;
            this.PictureBox10.TabStop = false;
            // 
            // TerCursorHeightVal
            // 
            this.TerCursorHeightVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerCursorHeightVal.CustomColor = System.Drawing.Color.Empty;
            this.TerCursorHeightVal.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.TerCursorHeightVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerCursorHeightVal.ForeColor = System.Drawing.Color.White;
            this.TerCursorHeightVal.Image = null;
            this.TerCursorHeightVal.Location = new System.Drawing.Point(312, 36);
            this.TerCursorHeightVal.Name = "TerCursorHeightVal";
            this.TerCursorHeightVal.Size = new System.Drawing.Size(34, 24);
            this.TerCursorHeightVal.TabIndex = 133;
            this.TerCursorHeightVal.UseVisualStyleBackColor = false;
            this.TerCursorHeightVal.Click += new System.EventHandler(this.TerCursorHeightVal_Click);
            // 
            // Label60
            // 
            this.Label60.BackColor = System.Drawing.Color.Transparent;
            this.Label60.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label60.Location = new System.Drawing.Point(36, 36);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(54, 24);
            this.Label60.TabIndex = 111;
            this.Label60.Text = "Size:";
            this.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(6, 36);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(24, 24);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 105;
            this.PictureBox11.TabStop = false;
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(36, 6);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(54, 24);
            this.Label14.TabIndex = 109;
            this.Label14.Text = "Style:";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage4.Controls.Add(this.GroupBox12);
            this.TabPage4.Location = new System.Drawing.Point(104, 4);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(363, 446);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Background";
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.TerOpacityVal);
            this.GroupBox12.Controls.Add(this.TerOpacityBar);
            this.GroupBox12.Controls.Add(this.TerImageOpacityVal);
            this.GroupBox12.Controls.Add(this.PictureBox13);
            this.GroupBox12.Controls.Add(this.PictureBox40);
            this.GroupBox12.Controls.Add(this.PictureBox16);
            this.GroupBox12.Controls.Add(this.TerAcrylic);
            this.GroupBox12.Controls.Add(this.Button5);
            this.GroupBox12.Controls.Add(this.TerBackImage);
            this.GroupBox12.Controls.Add(this.Label57);
            this.GroupBox12.Controls.Add(this.Button16);
            this.GroupBox12.Controls.Add(this.Label2);
            this.GroupBox12.Controls.Add(this.TerImageOpacity);
            this.GroupBox12.Location = new System.Drawing.Point(6, 6);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(351, 122);
            this.GroupBox12.TabIndex = 100;
            // 
            // TerOpacityVal
            // 
            this.TerOpacityVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerOpacityVal.CustomColor = System.Drawing.Color.Empty;
            this.TerOpacityVal.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.TerOpacityVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerOpacityVal.ForeColor = System.Drawing.Color.White;
            this.TerOpacityVal.Image = null;
            this.TerOpacityVal.Location = new System.Drawing.Point(311, 91);
            this.TerOpacityVal.Name = "TerOpacityVal";
            this.TerOpacityVal.Size = new System.Drawing.Size(34, 24);
            this.TerOpacityVal.TabIndex = 135;
            this.TerOpacityVal.UseVisualStyleBackColor = false;
            this.TerOpacityVal.Click += new System.EventHandler(this.TerOpacityVal_Click);
            // 
            // TerImageOpacityVal
            // 
            this.TerImageOpacityVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TerImageOpacityVal.CustomColor = System.Drawing.Color.Empty;
            this.TerImageOpacityVal.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.TerImageOpacityVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerImageOpacityVal.ForeColor = System.Drawing.Color.White;
            this.TerImageOpacityVal.Image = null;
            this.TerImageOpacityVal.Location = new System.Drawing.Point(311, 62);
            this.TerImageOpacityVal.Name = "TerImageOpacityVal";
            this.TerImageOpacityVal.Size = new System.Drawing.Size(34, 24);
            this.TerImageOpacityVal.TabIndex = 134;
            this.TerImageOpacityVal.UseVisualStyleBackColor = false;
            this.TerImageOpacityVal.Click += new System.EventHandler(this.TerImageOpacityVal_Click);
            // 
            // PictureBox13
            // 
            this.PictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox13.Image")));
            this.PictureBox13.Location = new System.Drawing.Point(6, 6);
            this.PictureBox13.Name = "PictureBox13";
            this.PictureBox13.Size = new System.Drawing.Size(24, 24);
            this.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox13.TabIndex = 106;
            this.PictureBox13.TabStop = false;
            // 
            // PictureBox16
            // 
            this.PictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox16.Image")));
            this.PictureBox16.Location = new System.Drawing.Point(6, 62);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(24, 24);
            this.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox16.TabIndex = 126;
            this.PictureBox16.TabStop = false;
            // 
            // Label57
            // 
            this.Label57.BackColor = System.Drawing.Color.Transparent;
            this.Label57.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label57.Location = new System.Drawing.Point(36, 62);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(65, 24);
            this.Label57.TabIndex = 119;
            this.Label57.Text = "Opacity:";
            this.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FontDialog1
            // 
            this.FontDialog1.FixedPitchOnly = true;
            this.FontDialog1.ShowEffects = false;
            // 
            // WindowsTerminal
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1008, 646);
            this.Controls.Add(this.GroupBox22);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.GroupBox13);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowsTerminal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Terminal";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowsTerminal_FormClosing);
            this.Load += new System.EventHandler(this.WindowsTerminal_Load);
            this.Shown += new System.EventHandler(this.WindowsTerminal_Shown);
            this.TerThemesContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).EndInit();
            this.GroupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox37)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            this.ResumeLayout(false);

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
