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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMD));
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.OpenWPTHDlg = new System.Windows.Forms.OpenFileDialog();
            this.Separator2 = new WinPaletter.UI.WP.SeparatorH();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.CMDEnabled = new WinPaletter.UI.WP.Toggle();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox15 = new System.Windows.Forms.PictureBox();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.CMD_AccentBackgroundLbl = new System.Windows.Forms.Label();
            this.CMD_PopupForegroundLbl = new System.Windows.Forms.Label();
            this.Label50 = new System.Windows.Forms.Label();
            this.CMD_AccentForegroundLbl = new System.Windows.Forms.Label();
            this.CMD_PopupBackgroundLbl = new System.Windows.Forms.Label();
            this.CMD_AccentBackgroundBar = new WinPaletter.UI.WP.Trackbar();
            this.CMD_PopupBackgroundBar = new WinPaletter.UI.WP.Trackbar();
            this.CMD_AccentForegroundBar = new WinPaletter.UI.WP.Trackbar();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label49 = new System.Windows.Forms.Label();
            this.CMD_PopupForegroundBar = new WinPaletter.UI.WP.Trackbar();
            this.Label6 = new System.Windows.Forms.Label();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.CMD_OpacityVal = new WinPaletter.UI.WP.Button();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.PictureBox13 = new System.Windows.Forms.PictureBox();
            this.CMD_LineSelection = new WinPaletter.UI.WP.CheckBox();
            this.CMD_EnhancedTerminal = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox12 = new System.Windows.Forms.PictureBox();
            this.CMD_TerminalScrolling = new WinPaletter.UI.WP.CheckBox();
            this.CMD_OpacityBar = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.Label57 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.GroupBox34 = new WinPaletter.UI.WP.GroupBox();
            this.CMD_PreviewCUR_Val = new WinPaletter.UI.WP.Button();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.CMD_CursorStyle = new WinPaletter.UI.WP.ComboBox();
            this.Label60 = new System.Windows.Forms.Label();
            this.CMD_CursorSizeBar = new WinPaletter.UI.WP.Trackbar();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.CMD_PreviewCUR = new System.Windows.Forms.Panel();
            this.CMD_PreviewCUR2 = new System.Windows.Forms.Panel();
            this.CMD_PreviewCursorInner = new System.Windows.Forms.Panel();
            this.CMD_CursorColor = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.GroupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.FontName = new System.Windows.Forms.Label();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.CMD_FontSizeVal = new WinPaletter.UI.WP.Button();
            this.CMD_RasterToggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label58 = new System.Windows.Forms.Label();
            this.CMD_FontSizeBar = new WinPaletter.UI.WP.Trackbar();
            this.CMD_FontWeightBox = new WinPaletter.UI.WP.ComboBox();
            this.Label61 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Button25 = new WinPaletter.UI.WP.Button();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Label59 = new System.Windows.Forms.Label();
            this.RasterList = new WinPaletter.UI.WP.ComboBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox14 = new System.Windows.Forms.PictureBox();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.ColorTable00 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.ColorTable01 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label20 = new System.Windows.Forms.Label();
            this.ColorTable02 = new WinPaletter.UI.Controllers.ColorItem();
            this.ColorTable03 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.ColorTable04 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.ColorTable05 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label25 = new System.Windows.Forms.Label();
            this.ColorTable06 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.ColorTable15 = new WinPaletter.UI.Controllers.ColorItem();
            this.ColorTable07 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label23 = new System.Windows.Forms.Label();
            this.ColorTable08 = new WinPaletter.UI.Controllers.ColorItem();
            this.ColorTable14 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.ColorTable09 = new WinPaletter.UI.Controllers.ColorItem();
            this.ColorTable13 = new WinPaletter.UI.Controllers.ColorItem();
            this.Label29 = new System.Windows.Forms.Label();
            this.ColorTable12 = new WinPaletter.UI.Controllers.ColorItem();
            this.ColorTable10 = new WinPaletter.UI.Controllers.ColorItem();
            this.ColorTable11 = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox8 = new WinPaletter.UI.WP.GroupBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.CMD1 = new WinPaletter.UI.Simulation.WinCMD();
            this.Label41 = new System.Windows.Forms.Label();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TabControl1 = new WinPaletter.UI.WP.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Label3 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.FontDialog1 = new System.Windows.Forms.FontDialog();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).BeginInit();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            this.CMD_PreviewCUR.SuspendLayout();
            this.CMD_PreviewCUR2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).BeginInit();
            this.GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "0.png");
            this.ImageList1.Images.SetKeyName(1, "1.png");
            this.ImageList1.Images.SetKeyName(2, "3.png");
            // 
            // OpenWPTHDlg
            // 
            this.OpenWPTHDlg.Filter = "WinPaletter Theme File (*.wpth)|*.wpth";
            // 
            // Separator2
            // 
            this.Separator2.AlternativeLook = false;
            this.Separator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator2.BackColor = System.Drawing.Color.Transparent;
            this.Separator2.Location = new System.Drawing.Point(12, 57);
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(896, 1);
            this.Separator2.TabIndex = 199;
            this.Separator2.TabStop = false;
            this.Separator2.Text = "Separator2";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.Button3);
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Controls.Add(this.Button8);
            this.GroupBox3.Controls.Add(this.Button6);
            this.GroupBox3.Controls.Add(this.CMDEnabled);
            this.GroupBox3.Controls.Add(this.checker_img);
            this.GroupBox3.Location = new System.Drawing.Point(12, 12);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(896, 39);
            this.GroupBox3.TabIndex = 198;
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.DrawOnGlass = false;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.Location = new System.Drawing.Point(222, 5);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(142, 29);
            this.Button3.TabIndex = 112;
            this.Button3.Text = "Current applied";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(4, 4);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 31);
            this.Label4.TabIndex = 111;
            this.Label4.Text = "Open from:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button8.CustomColor = System.Drawing.Color.Empty;
            this.Button8.DrawOnGlass = false;
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.Location = new System.Drawing.Point(85, 5);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(135, 29);
            this.Button8.TabIndex = 110;
            this.Button8.Text = "WinPaletter theme";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.DrawOnGlass = false;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = null;
            this.Button6.Location = new System.Drawing.Point(366, 5);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(139, 29);
            this.Button6.TabIndex = 108;
            this.Button6.Text = "Default Windows";
            this.Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // CMDEnabled
            // 
            this.CMDEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMDEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.CMDEnabled.Checked = false;
            this.CMDEnabled.DarkLight_Toggler = false;
            this.CMDEnabled.Location = new System.Drawing.Point(847, 9);
            this.CMDEnabled.Name = "CMDEnabled";
            this.CMDEnabled.Size = new System.Drawing.Size(40, 20);
            this.CMDEnabled.TabIndex = 85;
            this.CMDEnabled.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.CMDEnabled_CheckedChanged);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(804, 4);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox2.Controls.Add(this.PictureBox15);
            this.GroupBox2.Controls.Add(this.Separator1);
            this.GroupBox2.Controls.Add(this.CMD_AccentBackgroundLbl);
            this.GroupBox2.Controls.Add(this.CMD_PopupForegroundLbl);
            this.GroupBox2.Controls.Add(this.Label50);
            this.GroupBox2.Controls.Add(this.CMD_AccentForegroundLbl);
            this.GroupBox2.Controls.Add(this.CMD_PopupBackgroundLbl);
            this.GroupBox2.Controls.Add(this.CMD_AccentBackgroundBar);
            this.GroupBox2.Controls.Add(this.CMD_PopupBackgroundBar);
            this.GroupBox2.Controls.Add(this.CMD_AccentForegroundBar);
            this.GroupBox2.Controls.Add(this.Label18);
            this.GroupBox2.Controls.Add(this.Label17);
            this.GroupBox2.Controls.Add(this.Label49);
            this.GroupBox2.Controls.Add(this.CMD_PopupForegroundBar);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Location = new System.Drawing.Point(6, 298);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(301, 245);
            this.GroupBox2.TabIndex = 102;
            // 
            // PictureBox15
            // 
            this.PictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox15.Image")));
            this.PictureBox15.Location = new System.Drawing.Point(6, 7);
            this.PictureBox15.Name = "PictureBox15";
            this.PictureBox15.Size = new System.Drawing.Size(24, 24);
            this.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox15.TabIndex = 202;
            this.PictureBox15.TabStop = false;
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(9, 136);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(285, 1);
            this.Separator1.TabIndex = 101;
            this.Separator1.TabStop = false;
            // 
            // CMD_AccentBackgroundLbl
            // 
            this.CMD_AccentBackgroundLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_AccentBackgroundLbl.BackColor = System.Drawing.Color.Gray;
            this.CMD_AccentBackgroundLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_AccentBackgroundLbl.Location = new System.Drawing.Point(243, 83);
            this.CMD_AccentBackgroundLbl.Name = "CMD_AccentBackgroundLbl";
            this.CMD_AccentBackgroundLbl.Size = new System.Drawing.Size(50, 20);
            this.CMD_AccentBackgroundLbl.TabIndex = 88;
            this.CMD_AccentBackgroundLbl.Text = "0";
            this.CMD_AccentBackgroundLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_PopupForegroundLbl
            // 
            this.CMD_PopupForegroundLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_PopupForegroundLbl.BackColor = System.Drawing.Color.Gray;
            this.CMD_PopupForegroundLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_PopupForegroundLbl.Location = new System.Drawing.Point(243, 148);
            this.CMD_PopupForegroundLbl.Name = "CMD_PopupForegroundLbl";
            this.CMD_PopupForegroundLbl.Size = new System.Drawing.Size(50, 20);
            this.CMD_PopupForegroundLbl.TabIndex = 87;
            this.CMD_PopupForegroundLbl.Text = "0";
            this.CMD_PopupForegroundLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label50
            // 
            this.Label50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label50.BackColor = System.Drawing.Color.Transparent;
            this.Label50.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label50.Location = new System.Drawing.Point(5, 82);
            this.Label50.Name = "Label50";
            this.Label50.Size = new System.Drawing.Size(231, 22);
            this.Label50.TabIndex = 85;
            this.Label50.Text = "Background:";
            this.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_AccentForegroundLbl
            // 
            this.CMD_AccentForegroundLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_AccentForegroundLbl.BackColor = System.Drawing.Color.Gray;
            this.CMD_AccentForegroundLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_AccentForegroundLbl.Location = new System.Drawing.Point(243, 36);
            this.CMD_AccentForegroundLbl.Name = "CMD_AccentForegroundLbl";
            this.CMD_AccentForegroundLbl.Size = new System.Drawing.Size(50, 20);
            this.CMD_AccentForegroundLbl.TabIndex = 87;
            this.CMD_AccentForegroundLbl.Text = "0";
            this.CMD_AccentForegroundLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_PopupBackgroundLbl
            // 
            this.CMD_PopupBackgroundLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_PopupBackgroundLbl.BackColor = System.Drawing.Color.Gray;
            this.CMD_PopupBackgroundLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_PopupBackgroundLbl.Location = new System.Drawing.Point(243, 195);
            this.CMD_PopupBackgroundLbl.Name = "CMD_PopupBackgroundLbl";
            this.CMD_PopupBackgroundLbl.Size = new System.Drawing.Size(50, 20);
            this.CMD_PopupBackgroundLbl.TabIndex = 88;
            this.CMD_PopupBackgroundLbl.Text = "0";
            this.CMD_PopupBackgroundLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_AccentBackgroundBar
            // 
            this.CMD_AccentBackgroundBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_AccentBackgroundBar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_AccentBackgroundBar.LargeChange = 10;
            this.CMD_AccentBackgroundBar.Location = new System.Drawing.Point(8, 107);
            this.CMD_AccentBackgroundBar.Maximum = 15;
            this.CMD_AccentBackgroundBar.Minimum = 0;
            this.CMD_AccentBackgroundBar.Name = "CMD_AccentBackgroundBar";
            this.CMD_AccentBackgroundBar.Size = new System.Drawing.Size(289, 19);
            this.CMD_AccentBackgroundBar.SmallChange = 1;
            this.CMD_AccentBackgroundBar.TabIndex = 86;
            this.CMD_AccentBackgroundBar.Value = 0;
            this.CMD_AccentBackgroundBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CMD_AccentBackgroundBar_Scroll);
            // 
            // CMD_PopupBackgroundBar
            // 
            this.CMD_PopupBackgroundBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_PopupBackgroundBar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_PopupBackgroundBar.LargeChange = 10;
            this.CMD_PopupBackgroundBar.Location = new System.Drawing.Point(8, 219);
            this.CMD_PopupBackgroundBar.Maximum = 15;
            this.CMD_PopupBackgroundBar.Minimum = 0;
            this.CMD_PopupBackgroundBar.Name = "CMD_PopupBackgroundBar";
            this.CMD_PopupBackgroundBar.Size = new System.Drawing.Size(289, 19);
            this.CMD_PopupBackgroundBar.SmallChange = 1;
            this.CMD_PopupBackgroundBar.TabIndex = 86;
            this.CMD_PopupBackgroundBar.Value = 0;
            this.CMD_PopupBackgroundBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CMD_PopupBackgroundBar_Scroll);
            // 
            // CMD_AccentForegroundBar
            // 
            this.CMD_AccentForegroundBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_AccentForegroundBar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_AccentForegroundBar.LargeChange = 10;
            this.CMD_AccentForegroundBar.Location = new System.Drawing.Point(8, 60);
            this.CMD_AccentForegroundBar.Maximum = 15;
            this.CMD_AccentForegroundBar.Minimum = 0;
            this.CMD_AccentForegroundBar.Name = "CMD_AccentForegroundBar";
            this.CMD_AccentForegroundBar.Size = new System.Drawing.Size(289, 19);
            this.CMD_AccentForegroundBar.SmallChange = 1;
            this.CMD_AccentForegroundBar.TabIndex = 84;
            this.CMD_AccentForegroundBar.Value = 0;
            this.CMD_AccentForegroundBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CMD_AccentForegroundBar_Scroll);
            // 
            // Label18
            // 
            this.Label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(5, 194);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(231, 22);
            this.Label18.TabIndex = 85;
            this.Label18.Text = "Pop-up background:";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label17
            // 
            this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(5, 147);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(231, 22);
            this.Label17.TabIndex = 83;
            this.Label17.Text = "Pop-up foreground:";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label49
            // 
            this.Label49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label49.BackColor = System.Drawing.Color.Transparent;
            this.Label49.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label49.Location = new System.Drawing.Point(5, 35);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(231, 22);
            this.Label49.TabIndex = 83;
            this.Label49.Text = "Foreground:";
            this.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_PopupForegroundBar
            // 
            this.CMD_PopupForegroundBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_PopupForegroundBar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_PopupForegroundBar.LargeChange = 10;
            this.CMD_PopupForegroundBar.Location = new System.Drawing.Point(8, 172);
            this.CMD_PopupForegroundBar.Maximum = 15;
            this.CMD_PopupForegroundBar.Minimum = 0;
            this.CMD_PopupForegroundBar.Name = "CMD_PopupForegroundBar";
            this.CMD_PopupForegroundBar.Size = new System.Drawing.Size(289, 19);
            this.CMD_PopupForegroundBar.SmallChange = 1;
            this.CMD_PopupForegroundBar.TabIndex = 84;
            this.CMD_PopupForegroundBar.Value = 0;
            this.CMD_PopupForegroundBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CommandPrompt_PopupForegroundBar_Scroll);
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(36, 7);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(256, 24);
            this.Label6.TabIndex = 84;
            this.Label6.Text = "Accents:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(12, 621);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(603, 24);
            this.CheckBox1.TabIndex = 100;
            this.CheckBox1.Text = "Allow non monospace fonts (causes wrong renderering in enhanced terminal, not use" +
    "d in legacy terminal)";
            this.CheckBox1.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox1_CheckedChanged);
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.CMD_OpacityVal);
            this.GroupBox12.Controls.Add(this.PictureBox10);
            this.GroupBox12.Controls.Add(this.PictureBox13);
            this.GroupBox12.Controls.Add(this.CMD_LineSelection);
            this.GroupBox12.Controls.Add(this.CMD_EnhancedTerminal);
            this.GroupBox12.Controls.Add(this.PictureBox12);
            this.GroupBox12.Controls.Add(this.CMD_TerminalScrolling);
            this.GroupBox12.Controls.Add(this.CMD_OpacityBar);
            this.GroupBox12.Controls.Add(this.PictureBox11);
            this.GroupBox12.Controls.Add(this.Label57);
            this.GroupBox12.Location = new System.Drawing.Point(6, 6);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(301, 150);
            this.GroupBox12.TabIndex = 100;
            // 
            // CMD_OpacityVal
            // 
            this.CMD_OpacityVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_OpacityVal.CustomColor = System.Drawing.Color.Empty;
            this.CMD_OpacityVal.DrawOnGlass = false;
            this.CMD_OpacityVal.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.CMD_OpacityVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_OpacityVal.ForeColor = System.Drawing.Color.White;
            this.CMD_OpacityVal.Image = null;
            this.CMD_OpacityVal.Location = new System.Drawing.Point(262, 120);
            this.CMD_OpacityVal.Name = "CMD_OpacityVal";
            this.CMD_OpacityVal.Size = new System.Drawing.Size(34, 24);
            this.CMD_OpacityVal.TabIndex = 133;
            this.CMD_OpacityVal.UseVisualStyleBackColor = false;
            this.CMD_OpacityVal.Click += new System.EventHandler(this.CMD_OpacityVal_Click);
            // 
            // PictureBox10
            // 
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(6, 6);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(24, 24);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 106;
            this.PictureBox10.TabStop = false;
            // 
            // PictureBox13
            // 
            this.PictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox13.Image")));
            this.PictureBox13.Location = new System.Drawing.Point(6, 96);
            this.PictureBox13.Name = "PictureBox13";
            this.PictureBox13.Size = new System.Drawing.Size(24, 24);
            this.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox13.TabIndex = 126;
            this.PictureBox13.TabStop = false;
            // 
            // CMD_LineSelection
            // 
            this.CMD_LineSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CMD_LineSelection.Checked = false;
            this.CMD_LineSelection.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_LineSelection.ForeColor = System.Drawing.Color.White;
            this.CMD_LineSelection.Location = new System.Drawing.Point(36, 36);
            this.CMD_LineSelection.Name = "CMD_LineSelection";
            this.CMD_LineSelection.Size = new System.Drawing.Size(155, 24);
            this.CMD_LineSelection.TabIndex = 122;
            this.CMD_LineSelection.Text = "Line selection";
            // 
            // CMD_EnhancedTerminal
            // 
            this.CMD_EnhancedTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CMD_EnhancedTerminal.Checked = false;
            this.CMD_EnhancedTerminal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_EnhancedTerminal.ForeColor = System.Drawing.Color.White;
            this.CMD_EnhancedTerminal.Location = new System.Drawing.Point(36, 6);
            this.CMD_EnhancedTerminal.Name = "CMD_EnhancedTerminal";
            this.CMD_EnhancedTerminal.Size = new System.Drawing.Size(155, 24);
            this.CMD_EnhancedTerminal.TabIndex = 118;
            this.CMD_EnhancedTerminal.Text = "Enhanced terminal";
            // 
            // PictureBox12
            // 
            this.PictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox12.Image")));
            this.PictureBox12.Location = new System.Drawing.Point(6, 66);
            this.PictureBox12.Name = "PictureBox12";
            this.PictureBox12.Size = new System.Drawing.Size(24, 24);
            this.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox12.TabIndex = 125;
            this.PictureBox12.TabStop = false;
            // 
            // CMD_TerminalScrolling
            // 
            this.CMD_TerminalScrolling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CMD_TerminalScrolling.Checked = false;
            this.CMD_TerminalScrolling.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_TerminalScrolling.ForeColor = System.Drawing.Color.White;
            this.CMD_TerminalScrolling.Location = new System.Drawing.Point(36, 66);
            this.CMD_TerminalScrolling.Name = "CMD_TerminalScrolling";
            this.CMD_TerminalScrolling.Size = new System.Drawing.Size(155, 24);
            this.CMD_TerminalScrolling.TabIndex = 123;
            this.CMD_TerminalScrolling.Text = "Terminal scrolling";
            // 
            // CMD_OpacityBar
            // 
            this.CMD_OpacityBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_OpacityBar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_OpacityBar.LargeChange = 10;
            this.CMD_OpacityBar.Location = new System.Drawing.Point(39, 123);
            this.CMD_OpacityBar.Maximum = 255;
            this.CMD_OpacityBar.Minimum = 0;
            this.CMD_OpacityBar.Name = "CMD_OpacityBar";
            this.CMD_OpacityBar.Size = new System.Drawing.Size(217, 19);
            this.CMD_OpacityBar.SmallChange = 1;
            this.CMD_OpacityBar.TabIndex = 120;
            this.CMD_OpacityBar.Value = 0;
            this.CMD_OpacityBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CMD_OpacityBar_Scroll);
            // 
            // PictureBox11
            // 
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(6, 36);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(24, 24);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 124;
            this.PictureBox11.TabStop = false;
            // 
            // Label57
            // 
            this.Label57.BackColor = System.Drawing.Color.Transparent;
            this.Label57.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label57.Location = new System.Drawing.Point(36, 96);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(104, 24);
            this.Label57.TabIndex = 119;
            this.Label57.Text = "Window opacity:";
            this.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.DarkOrange;
            this.Label5.Location = new System.Drawing.Point(6, 159);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(301, 48);
            this.Label5.TabIndex = 111;
            this.Label5.Text = "Tweaks are for Windows 10 1909 and later";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox34
            // 
            this.GroupBox34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox34.Controls.Add(this.CMD_PreviewCUR_Val);
            this.GroupBox34.Controls.Add(this.PictureBox7);
            this.GroupBox34.Controls.Add(this.CMD_CursorStyle);
            this.GroupBox34.Controls.Add(this.Label60);
            this.GroupBox34.Controls.Add(this.CMD_CursorSizeBar);
            this.GroupBox34.Controls.Add(this.Label1);
            this.GroupBox34.Controls.Add(this.PictureBox9);
            this.GroupBox34.Controls.Add(this.Label2);
            this.GroupBox34.Controls.Add(this.CMD_PreviewCUR);
            this.GroupBox34.Controls.Add(this.CMD_CursorColor);
            this.GroupBox34.Controls.Add(this.PictureBox8);
            this.GroupBox34.Location = new System.Drawing.Point(6, 6);
            this.GroupBox34.Name = "GroupBox34";
            this.GroupBox34.Size = new System.Drawing.Size(301, 97);
            this.GroupBox34.TabIndex = 99;
            // 
            // CMD_PreviewCUR_Val
            // 
            this.CMD_PreviewCUR_Val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_PreviewCUR_Val.CustomColor = System.Drawing.Color.Empty;
            this.CMD_PreviewCUR_Val.DrawOnGlass = false;
            this.CMD_PreviewCUR_Val.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.CMD_PreviewCUR_Val.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_PreviewCUR_Val.ForeColor = System.Drawing.Color.White;
            this.CMD_PreviewCUR_Val.Image = null;
            this.CMD_PreviewCUR_Val.Location = new System.Drawing.Point(262, 66);
            this.CMD_PreviewCUR_Val.Name = "CMD_PreviewCUR_Val";
            this.CMD_PreviewCUR_Val.Size = new System.Drawing.Size(34, 24);
            this.CMD_PreviewCUR_Val.TabIndex = 132;
            this.CMD_PreviewCUR_Val.UseVisualStyleBackColor = false;
            this.CMD_PreviewCUR_Val.Click += new System.EventHandler(this.CMD_PreviewCUR_Val_Click);
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(6, 6);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 103;
            this.PictureBox7.TabStop = false;
            // 
            // CMD_CursorStyle
            // 
            this.CMD_CursorStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_CursorStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.CMD_CursorStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CMD_CursorStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMD_CursorStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_CursorStyle.ForeColor = System.Drawing.Color.White;
            this.CMD_CursorStyle.FormattingEnabled = true;
            this.CMD_CursorStyle.ItemHeight = 20;
            this.CMD_CursorStyle.Items.AddRange(new object[] {
            "Default",
            "Legacy",
            "Underscore",
            "Empty Box",
            "Vertical Bar",
            "Solid Box"});
            this.CMD_CursorStyle.Location = new System.Drawing.Point(97, 35);
            this.CMD_CursorStyle.Name = "CMD_CursorStyle";
            this.CMD_CursorStyle.Size = new System.Drawing.Size(199, 26);
            this.CMD_CursorStyle.TabIndex = 110;
            this.CMD_CursorStyle.SelectedIndexChanged += new System.EventHandler(this.CMD_CursorStyle_SelectedIndexChanged_1);
            // 
            // Label60
            // 
            this.Label60.BackColor = System.Drawing.Color.Transparent;
            this.Label60.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label60.Location = new System.Drawing.Point(36, 66);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(54, 24);
            this.Label60.TabIndex = 111;
            this.Label60.Text = "Size:";
            this.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_CursorSizeBar
            // 
            this.CMD_CursorSizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_CursorSizeBar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_CursorSizeBar.LargeChange = 1;
            this.CMD_CursorSizeBar.Location = new System.Drawing.Point(97, 69);
            this.CMD_CursorSizeBar.Maximum = 100;
            this.CMD_CursorSizeBar.Minimum = 20;
            this.CMD_CursorSizeBar.Name = "CMD_CursorSizeBar";
            this.CMD_CursorSizeBar.Size = new System.Drawing.Size(159, 19);
            this.CMD_CursorSizeBar.SmallChange = 1;
            this.CMD_CursorSizeBar.TabIndex = 102;
            this.CMD_CursorSizeBar.Value = 20;
            this.CMD_CursorSizeBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CMD_CursorSizeBar_Scroll);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(36, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(54, 24);
            this.Label1.TabIndex = 108;
            this.Label1.Text = "Color:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(6, 66);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 105;
            this.PictureBox9.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(36, 36);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(54, 24);
            this.Label2.TabIndex = 109;
            this.Label2.Text = "Style:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_PreviewCUR
            // 
            this.CMD_PreviewCUR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_PreviewCUR.BackColor = System.Drawing.Color.Black;
            this.CMD_PreviewCUR.Controls.Add(this.CMD_PreviewCUR2);
            this.CMD_PreviewCUR.Location = new System.Drawing.Point(247, 6);
            this.CMD_PreviewCUR.Name = "CMD_PreviewCUR";
            this.CMD_PreviewCUR.Size = new System.Drawing.Size(49, 24);
            this.CMD_PreviewCUR.TabIndex = 103;
            // 
            // CMD_PreviewCUR2
            // 
            this.CMD_PreviewCUR2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CMD_PreviewCUR2.BackColor = System.Drawing.Color.White;
            this.CMD_PreviewCUR2.Controls.Add(this.CMD_PreviewCursorInner);
            this.CMD_PreviewCUR2.Location = new System.Drawing.Point(3, 16);
            this.CMD_PreviewCUR2.Name = "CMD_PreviewCUR2";
            this.CMD_PreviewCUR2.Padding = new System.Windows.Forms.Padding(1);
            this.CMD_PreviewCUR2.Size = new System.Drawing.Size(8, 5);
            this.CMD_PreviewCUR2.TabIndex = 104;
            // 
            // CMD_PreviewCursorInner
            // 
            this.CMD_PreviewCursorInner.BackColor = System.Drawing.Color.Transparent;
            this.CMD_PreviewCursorInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CMD_PreviewCursorInner.Location = new System.Drawing.Point(1, 1);
            this.CMD_PreviewCursorInner.Name = "CMD_PreviewCursorInner";
            this.CMD_PreviewCursorInner.Size = new System.Drawing.Size(6, 3);
            this.CMD_PreviewCursorInner.TabIndex = 106;
            // 
            // CMD_CursorColor
            // 
            this.CMD_CursorColor.AllowDrop = true;
            this.CMD_CursorColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_CursorColor.BackColor = System.Drawing.Color.White;
            this.CMD_CursorColor.DefaultColor = System.Drawing.Color.White;
            this.CMD_CursorColor.DontShowInfo = false;
            this.CMD_CursorColor.Location = new System.Drawing.Point(97, 6);
            this.CMD_CursorColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CMD_CursorColor.Name = "CMD_CursorColor";
            this.CMD_CursorColor.Size = new System.Drawing.Size(143, 24);
            this.CMD_CursorColor.TabIndex = 107;
            this.CMD_CursorColor.Click += new System.EventHandler(this.CMD_CursorColor_Click);
            this.CMD_CursorColor.DragDrop += new System.Windows.Forms.DragEventHandler(this.CMD_CursorColor_Click);
            // 
            // PictureBox8
            // 
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(6, 36);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 104;
            this.PictureBox8.TabStop = false;
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button10.CustomColor = System.Drawing.Color.Empty;
            this.Button10.DrawOnGlass = false;
            this.Button10.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = ((System.Drawing.Image)(resources.GetObject("Button10.Image")));
            this.Button10.Location = new System.Drawing.Point(709, 615);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(115, 34);
            this.Button10.TabIndex = 82;
            this.Button10.Text = "Quick apply";
            this.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox4.Controls.Add(this.FontName);
            this.GroupBox4.Controls.Add(this.Button5);
            this.GroupBox4.Controls.Add(this.CMD_FontSizeVal);
            this.GroupBox4.Controls.Add(this.CMD_RasterToggle);
            this.GroupBox4.Controls.Add(this.PictureBox1);
            this.GroupBox4.Controls.Add(this.Label58);
            this.GroupBox4.Controls.Add(this.CMD_FontSizeBar);
            this.GroupBox4.Controls.Add(this.CMD_FontWeightBox);
            this.GroupBox4.Controls.Add(this.Label61);
            this.GroupBox4.Controls.Add(this.PictureBox6);
            this.GroupBox4.Controls.Add(this.PictureBox3);
            this.GroupBox4.Controls.Add(this.Label35);
            this.GroupBox4.Controls.Add(this.Button25);
            this.GroupBox4.Controls.Add(this.PictureBox4);
            this.GroupBox4.Controls.Add(this.Label59);
            this.GroupBox4.Controls.Add(this.RasterList);
            this.GroupBox4.Location = new System.Drawing.Point(6, 6);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(301, 128);
            this.GroupBox4.TabIndex = 98;
            // 
            // FontName
            // 
            this.FontName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FontName.BackColor = System.Drawing.Color.Transparent;
            this.FontName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontName.Location = new System.Drawing.Point(96, 6);
            this.FontName.Name = "FontName";
            this.FontName.Size = new System.Drawing.Size(172, 24);
            this.FontName.TabIndex = 133;
            this.FontName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FontName.FontChanged += new System.EventHandler(this.CMD_FontsBox_SelectedIndexChanged);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.DrawOnGlass = false;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.Location = new System.Drawing.Point(274, 6);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(21, 24);
            this.Button5.TabIndex = 132;
            this.Button5.Text = "...";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // CMD_FontSizeVal
            // 
            this.CMD_FontSizeVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_FontSizeVal.CustomColor = System.Drawing.Color.Empty;
            this.CMD_FontSizeVal.DrawOnGlass = false;
            this.CMD_FontSizeVal.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.CMD_FontSizeVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_FontSizeVal.ForeColor = System.Drawing.Color.White;
            this.CMD_FontSizeVal.Image = null;
            this.CMD_FontSizeVal.Location = new System.Drawing.Point(261, 66);
            this.CMD_FontSizeVal.Name = "CMD_FontSizeVal";
            this.CMD_FontSizeVal.Size = new System.Drawing.Size(34, 24);
            this.CMD_FontSizeVal.TabIndex = 131;
            this.CMD_FontSizeVal.UseVisualStyleBackColor = false;
            this.CMD_FontSizeVal.Click += new System.EventHandler(this.CMD_FontSizeVal_Click);
            // 
            // CMD_RasterToggle
            // 
            this.CMD_RasterToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_RasterToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.CMD_RasterToggle.Checked = false;
            this.CMD_RasterToggle.DarkLight_Toggler = false;
            this.CMD_RasterToggle.Location = new System.Drawing.Point(255, 98);
            this.CMD_RasterToggle.Name = "CMD_RasterToggle";
            this.CMD_RasterToggle.Size = new System.Drawing.Size(40, 20);
            this.CMD_RasterToggle.TabIndex = 95;
            this.CMD_RasterToggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.CMD_RasterToggle_CheckedChanged);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(6, 6);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 99;
            this.PictureBox1.TabStop = false;
            // 
            // Label58
            // 
            this.Label58.BackColor = System.Drawing.Color.Transparent;
            this.Label58.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label58.Location = new System.Drawing.Point(36, 96);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(108, 24);
            this.Label58.TabIndex = 94;
            this.Label58.Text = "Raster font ?";
            this.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMD_FontSizeBar
            // 
            this.CMD_FontSizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_FontSizeBar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_FontSizeBar.LargeChange = 10;
            this.CMD_FontSizeBar.Location = new System.Drawing.Point(97, 69);
            this.CMD_FontSizeBar.Maximum = 48;
            this.CMD_FontSizeBar.Minimum = 5;
            this.CMD_FontSizeBar.Name = "CMD_FontSizeBar";
            this.CMD_FontSizeBar.Size = new System.Drawing.Size(158, 19);
            this.CMD_FontSizeBar.SmallChange = 1;
            this.CMD_FontSizeBar.TabIndex = 101;
            this.CMD_FontSizeBar.Value = 5;
            this.CMD_FontSizeBar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CMD_FontSizeBar_Scroll);
            // 
            // CMD_FontWeightBox
            // 
            this.CMD_FontWeightBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_FontWeightBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.CMD_FontWeightBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CMD_FontWeightBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMD_FontWeightBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CMD_FontWeightBox.ForeColor = System.Drawing.Color.White;
            this.CMD_FontWeightBox.FormattingEnabled = true;
            this.CMD_FontWeightBox.ItemHeight = 20;
            this.CMD_FontWeightBox.Items.AddRange(new object[] {
            "Don\'t Care",
            "Thin",
            "Extra Light",
            "Light",
            "Normal",
            "Medium",
            "Semi Bold",
            "Bold",
            "Extra Bold",
            "Heavy"});
            this.CMD_FontWeightBox.Location = new System.Drawing.Point(96, 35);
            this.CMD_FontWeightBox.Name = "CMD_FontWeightBox";
            this.CMD_FontWeightBox.Size = new System.Drawing.Size(172, 26);
            this.CMD_FontWeightBox.TabIndex = 99;
            this.CMD_FontWeightBox.SelectedIndexChanged += new System.EventHandler(this.CMD_FontWeightBox_SelectedIndexChanged);
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
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(6, 96);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(24, 24);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 102;
            this.PictureBox6.TabStop = false;
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(6, 36);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 100;
            this.PictureBox3.TabStop = false;
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
            // Button25
            // 
            this.Button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button25.CustomColor = System.Drawing.Color.Empty;
            this.Button25.DrawOnGlass = false;
            this.Button25.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button25.ForeColor = System.Drawing.Color.White;
            this.Button25.Image = null;
            this.Button25.Location = new System.Drawing.Point(274, 36);
            this.Button25.Name = "Button25";
            this.Button25.Size = new System.Drawing.Size(21, 24);
            this.Button25.TabIndex = 105;
            this.Button25.Text = "?";
            this.Button25.UseVisualStyleBackColor = false;
            this.Button25.Click += new System.EventHandler(this.Button25_Click);
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(6, 66);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 101;
            this.PictureBox4.TabStop = false;
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
            // RasterList
            // 
            this.RasterList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RasterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.RasterList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.RasterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RasterList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RasterList.ForeColor = System.Drawing.Color.White;
            this.RasterList.FormattingEnabled = true;
            this.RasterList.ItemHeight = 20;
            this.RasterList.Items.AddRange(new object[] {
            "4x6",
            "6x8",
            "6x9",
            "8x8",
            "8x9",
            "16x8",
            "5x12",
            "7x12",
            "8x12",
            "16x12",
            "12x16",
            "10x18"});
            this.RasterList.Location = new System.Drawing.Point(96, 65);
            this.RasterList.Name = "RasterList";
            this.RasterList.Size = new System.Drawing.Size(199, 26);
            this.RasterList.TabIndex = 104;
            this.RasterList.Visible = false;
            this.RasterList.SelectedIndexChanged += new System.EventHandler(this.RasterList_SelectedIndexChanged);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.DrawOnGlass = false;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.Location = new System.Drawing.Point(623, 615);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 66;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.PictureBox14);
            this.GroupBox1.Controls.Add(this.Label31);
            this.GroupBox1.Controls.Add(this.Label19);
            this.GroupBox1.Controls.Add(this.ColorTable00);
            this.GroupBox1.Controls.Add(this.Label7);
            this.GroupBox1.Controls.Add(this.Label32);
            this.GroupBox1.Controls.Add(this.ColorTable01);
            this.GroupBox1.Controls.Add(this.Label20);
            this.GroupBox1.Controls.Add(this.ColorTable02);
            this.GroupBox1.Controls.Add(this.ColorTable03);
            this.GroupBox1.Controls.Add(this.Label33);
            this.GroupBox1.Controls.Add(this.Label21);
            this.GroupBox1.Controls.Add(this.Label22);
            this.GroupBox1.Controls.Add(this.ColorTable04);
            this.GroupBox1.Controls.Add(this.Label26);
            this.GroupBox1.Controls.Add(this.Label34);
            this.GroupBox1.Controls.Add(this.ColorTable05);
            this.GroupBox1.Controls.Add(this.Label25);
            this.GroupBox1.Controls.Add(this.ColorTable06);
            this.GroupBox1.Controls.Add(this.Label24);
            this.GroupBox1.Controls.Add(this.Label27);
            this.GroupBox1.Controls.Add(this.ColorTable15);
            this.GroupBox1.Controls.Add(this.ColorTable07);
            this.GroupBox1.Controls.Add(this.Label23);
            this.GroupBox1.Controls.Add(this.ColorTable08);
            this.GroupBox1.Controls.Add(this.ColorTable14);
            this.GroupBox1.Controls.Add(this.Label28);
            this.GroupBox1.Controls.Add(this.Label30);
            this.GroupBox1.Controls.Add(this.ColorTable09);
            this.GroupBox1.Controls.Add(this.ColorTable13);
            this.GroupBox1.Controls.Add(this.Label29);
            this.GroupBox1.Controls.Add(this.ColorTable12);
            this.GroupBox1.Controls.Add(this.ColorTable10);
            this.GroupBox1.Controls.Add(this.ColorTable11);
            this.GroupBox1.Location = new System.Drawing.Point(6, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(301, 286);
            this.GroupBox1.TabIndex = 87;
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
            // Label31
            // 
            this.Label31.BackColor = System.Drawing.Color.Transparent;
            this.Label31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label31.Location = new System.Drawing.Point(144, 253);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(38, 25);
            this.Label31.TabIndex = 99;
            this.Label31.Text = "15 (F)";
            this.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label19
            // 
            this.Label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label19.BackColor = System.Drawing.Color.Transparent;
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(36, 6);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(261, 24);
            this.Label19.TabIndex = 84;
            this.Label19.Text = "Tables:";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ColorTable00
            // 
            this.ColorTable00.AllowDrop = true;
            this.ColorTable00.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ColorTable00.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ColorTable00.DontShowInfo = false;
            this.ColorTable00.Location = new System.Drawing.Point(28, 36);
            this.ColorTable00.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable00.Name = "ColorTable00";
            this.ColorTable00.Size = new System.Drawing.Size(105, 25);
            this.ColorTable00.TabIndex = 3;
            this.ColorTable00.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable00.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(7, 36);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(14, 25);
            this.Label7.TabIndex = 4;
            this.Label7.Text = "0";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label32
            // 
            this.Label32.BackColor = System.Drawing.Color.Transparent;
            this.Label32.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label32.Location = new System.Drawing.Point(144, 222);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(38, 25);
            this.Label32.TabIndex = 98;
            this.Label32.Text = "14 (E)";
            this.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable01
            // 
            this.ColorTable01.AllowDrop = true;
            this.ColorTable01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(218)))));
            this.ColorTable01.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(218)))));
            this.ColorTable01.DontShowInfo = false;
            this.ColorTable01.Location = new System.Drawing.Point(28, 67);
            this.ColorTable01.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable01.Name = "ColorTable01";
            this.ColorTable01.Size = new System.Drawing.Size(105, 25);
            this.ColorTable01.TabIndex = 5;
            this.ColorTable01.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable01.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(7, 67);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(14, 25);
            this.Label20.TabIndex = 85;
            this.Label20.Text = "1";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable02
            // 
            this.ColorTable02.AllowDrop = true;
            this.ColorTable02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(161)))), ((int)(((byte)(14)))));
            this.ColorTable02.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(161)))), ((int)(((byte)(14)))));
            this.ColorTable02.DontShowInfo = false;
            this.ColorTable02.Location = new System.Drawing.Point(28, 98);
            this.ColorTable02.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable02.Name = "ColorTable02";
            this.ColorTable02.Size = new System.Drawing.Size(105, 25);
            this.ColorTable02.TabIndex = 7;
            this.ColorTable02.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable02.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // ColorTable03
            // 
            this.ColorTable03.AllowDrop = true;
            this.ColorTable03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(150)))), ((int)(((byte)(221)))));
            this.ColorTable03.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(150)))), ((int)(((byte)(221)))));
            this.ColorTable03.DontShowInfo = false;
            this.ColorTable03.Location = new System.Drawing.Point(28, 129);
            this.ColorTable03.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable03.Name = "ColorTable03";
            this.ColorTable03.Size = new System.Drawing.Size(105, 25);
            this.ColorTable03.TabIndex = 9;
            this.ColorTable03.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable03.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label33
            // 
            this.Label33.BackColor = System.Drawing.Color.Transparent;
            this.Label33.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(144, 192);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(38, 25);
            this.Label33.TabIndex = 97;
            this.Label33.Text = "13 (D)";
            this.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.Transparent;
            this.Label21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.Location = new System.Drawing.Point(7, 98);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(14, 25);
            this.Label21.TabIndex = 86;
            this.Label21.Text = "2";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.Color.Transparent;
            this.Label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label22.Location = new System.Drawing.Point(7, 129);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(14, 25);
            this.Label22.TabIndex = 87;
            this.Label22.Text = "3";
            this.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable04
            // 
            this.ColorTable04.AllowDrop = true;
            this.ColorTable04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(15)))), ((int)(((byte)(31)))));
            this.ColorTable04.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(15)))), ((int)(((byte)(31)))));
            this.ColorTable04.DontShowInfo = false;
            this.ColorTable04.Location = new System.Drawing.Point(28, 160);
            this.ColorTable04.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable04.Name = "ColorTable04";
            this.ColorTable04.Size = new System.Drawing.Size(105, 25);
            this.ColorTable04.TabIndex = 11;
            this.ColorTable04.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable04.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.Transparent;
            this.Label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label26.Location = new System.Drawing.Point(7, 160);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(14, 25);
            this.Label26.TabIndex = 88;
            this.Label26.Text = "4";
            this.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label34
            // 
            this.Label34.BackColor = System.Drawing.Color.Transparent;
            this.Label34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(144, 160);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(38, 25);
            this.Label34.TabIndex = 96;
            this.Label34.Text = "12 (C)";
            this.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable05
            // 
            this.ColorTable05.AllowDrop = true;
            this.ColorTable05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(23)))), ((int)(((byte)(152)))));
            this.ColorTable05.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(23)))), ((int)(((byte)(152)))));
            this.ColorTable05.DontShowInfo = false;
            this.ColorTable05.Location = new System.Drawing.Point(28, 191);
            this.ColorTable05.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable05.Name = "ColorTable05";
            this.ColorTable05.Size = new System.Drawing.Size(105, 25);
            this.ColorTable05.TabIndex = 13;
            this.ColorTable05.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable05.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.Transparent;
            this.Label25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(7, 192);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(14, 25);
            this.Label25.TabIndex = 89;
            this.Label25.Text = "5";
            this.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable06
            // 
            this.ColorTable06.AllowDrop = true;
            this.ColorTable06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(156)))), ((int)(((byte)(0)))));
            this.ColorTable06.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(156)))), ((int)(((byte)(0)))));
            this.ColorTable06.DontShowInfo = false;
            this.ColorTable06.Location = new System.Drawing.Point(28, 222);
            this.ColorTable06.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable06.Name = "ColorTable06";
            this.ColorTable06.Size = new System.Drawing.Size(105, 25);
            this.ColorTable06.TabIndex = 15;
            this.ColorTable06.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable06.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.Transparent;
            this.Label24.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label24.Location = new System.Drawing.Point(7, 222);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(14, 25);
            this.Label24.TabIndex = 90;
            this.Label24.Text = "6";
            this.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.Transparent;
            this.Label27.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(144, 129);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(38, 25);
            this.Label27.TabIndex = 95;
            this.Label27.Text = "11 (B)";
            this.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable15
            // 
            this.ColorTable15.AllowDrop = true;
            this.ColorTable15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ColorTable15.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ColorTable15.DontShowInfo = false;
            this.ColorTable15.Location = new System.Drawing.Point(189, 253);
            this.ColorTable15.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable15.Name = "ColorTable15";
            this.ColorTable15.Size = new System.Drawing.Size(105, 25);
            this.ColorTable15.TabIndex = 33;
            this.ColorTable15.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable15.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // ColorTable07
            // 
            this.ColorTable07.AllowDrop = true;
            this.ColorTable07.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ColorTable07.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ColorTable07.DontShowInfo = false;
            this.ColorTable07.Location = new System.Drawing.Point(28, 253);
            this.ColorTable07.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable07.Name = "ColorTable07";
            this.ColorTable07.Size = new System.Drawing.Size(105, 25);
            this.ColorTable07.TabIndex = 17;
            this.ColorTable07.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable07.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.Transparent;
            this.Label23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(7, 253);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(14, 25);
            this.Label23.TabIndex = 91;
            this.Label23.Text = "7";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable08
            // 
            this.ColorTable08.AllowDrop = true;
            this.ColorTable08.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this.ColorTable08.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this.ColorTable08.DontShowInfo = false;
            this.ColorTable08.Location = new System.Drawing.Point(189, 36);
            this.ColorTable08.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable08.Name = "ColorTable08";
            this.ColorTable08.Size = new System.Drawing.Size(105, 25);
            this.ColorTable08.TabIndex = 19;
            this.ColorTable08.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable08.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // ColorTable14
            // 
            this.ColorTable14.AllowDrop = true;
            this.ColorTable14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(165)))));
            this.ColorTable14.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(165)))));
            this.ColorTable14.DontShowInfo = false;
            this.ColorTable14.Location = new System.Drawing.Point(189, 222);
            this.ColorTable14.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable14.Name = "ColorTable14";
            this.ColorTable14.Size = new System.Drawing.Size(105, 25);
            this.ColorTable14.TabIndex = 31;
            this.ColorTable14.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable14.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label28
            // 
            this.Label28.BackColor = System.Drawing.Color.Transparent;
            this.Label28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label28.Location = new System.Drawing.Point(144, 98);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(38, 25);
            this.Label28.TabIndex = 94;
            this.Label28.Text = "10 (A)";
            this.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label30
            // 
            this.Label30.BackColor = System.Drawing.Color.Transparent;
            this.Label30.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label30.Location = new System.Drawing.Point(144, 36);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(38, 25);
            this.Label30.TabIndex = 92;
            this.Label30.Text = "8";
            this.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable09
            // 
            this.ColorTable09.AllowDrop = true;
            this.ColorTable09.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.ColorTable09.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.ColorTable09.DontShowInfo = false;
            this.ColorTable09.Location = new System.Drawing.Point(189, 67);
            this.ColorTable09.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable09.Name = "ColorTable09";
            this.ColorTable09.Size = new System.Drawing.Size(105, 25);
            this.ColorTable09.TabIndex = 21;
            this.ColorTable09.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable09.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // ColorTable13
            // 
            this.ColorTable13.AllowDrop = true;
            this.ColorTable13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(158)))));
            this.ColorTable13.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(158)))));
            this.ColorTable13.DontShowInfo = false;
            this.ColorTable13.Location = new System.Drawing.Point(189, 191);
            this.ColorTable13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable13.Name = "ColorTable13";
            this.ColorTable13.Size = new System.Drawing.Size(105, 25);
            this.ColorTable13.TabIndex = 29;
            this.ColorTable13.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable13.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.Transparent;
            this.Label29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(144, 67);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(38, 25);
            this.Label29.TabIndex = 93;
            this.Label29.Text = "9";
            this.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorTable12
            // 
            this.ColorTable12.AllowDrop = true;
            this.ColorTable12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(72)))), ((int)(((byte)(86)))));
            this.ColorTable12.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(72)))), ((int)(((byte)(86)))));
            this.ColorTable12.DontShowInfo = false;
            this.ColorTable12.Location = new System.Drawing.Point(189, 160);
            this.ColorTable12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable12.Name = "ColorTable12";
            this.ColorTable12.Size = new System.Drawing.Size(105, 25);
            this.ColorTable12.TabIndex = 27;
            this.ColorTable12.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable12.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // ColorTable10
            // 
            this.ColorTable10.AllowDrop = true;
            this.ColorTable10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(198)))), ((int)(((byte)(12)))));
            this.ColorTable10.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(198)))), ((int)(((byte)(12)))));
            this.ColorTable10.DontShowInfo = false;
            this.ColorTable10.Location = new System.Drawing.Point(189, 98);
            this.ColorTable10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable10.Name = "ColorTable10";
            this.ColorTable10.Size = new System.Drawing.Size(105, 25);
            this.ColorTable10.TabIndex = 23;
            this.ColorTable10.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable10.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // ColorTable11
            // 
            this.ColorTable11.AllowDrop = true;
            this.ColorTable11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.ColorTable11.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.ColorTable11.DontShowInfo = false;
            this.ColorTable11.Location = new System.Drawing.Point(189, 129);
            this.ColorTable11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ColorTable11.Name = "ColorTable11";
            this.ColorTable11.Size = new System.Drawing.Size(105, 25);
            this.ColorTable11.TabIndex = 25;
            this.ColorTable11.Click += new System.EventHandler(this.ColorTable00_Click);
            this.ColorTable11.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorTable00_Click);
            // 
            // GroupBox8
            // 
            this.GroupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox8.Controls.Add(this.Button4);
            this.GroupBox8.Controls.Add(this.PictureBox41);
            this.GroupBox8.Controls.Add(this.CMD1);
            this.GroupBox8.Controls.Add(this.Label41);
            this.GroupBox8.Location = new System.Drawing.Point(436, 63);
            this.GroupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox8.Size = new System.Drawing.Size(472, 293);
            this.GroupBox8.TabIndex = 91;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.DrawOnGlass = false;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.Location = new System.Drawing.Point(234, 7);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(230, 30);
            this.Button4.TabIndex = 94;
            this.Button4.Text = "Open Command Prompt for testing";
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // PictureBox41
            // 
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(6, 6);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(35, 35);
            this.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox41.TabIndex = 4;
            this.PictureBox41.TabStop = false;
            // 
            // CMD1
            // 
            this.CMD1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD1.BackColor = System.Drawing.Color.Black;
            this.CMD1.CMD_ColorTable00 = System.Drawing.Color.Black;
            this.CMD1.CMD_ColorTable01 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable02 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable03 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable04 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable05 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable06 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable07 = System.Drawing.Color.White;
            this.CMD1.CMD_ColorTable08 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable09 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable10 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable11 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable12 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable13 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable14 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable15 = System.Drawing.Color.White;
            this.CMD1.CMD_PopupBackground = 5;
            this.CMD1.CMD_PopupForeground = 15;
            this.CMD1.CMD_ScreenColorsBackground = 0;
            this.CMD1.CMD_ScreenColorsForeground = 7;
            this.CMD1.CustomTerminal = false;
            this.CMD1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD1.Location = new System.Drawing.Point(4, 45);
            this.CMD1.Name = "CMD1";
            this.CMD1.PowerShell = false;
            this.CMD1.Raster = false;
            this.CMD1.RasterSize = WinPaletter.UI.Simulation.WinCMD.Raster_Sizes._8x12;
            this.CMD1.Size = new System.Drawing.Size(464, 244);
            this.CMD1.TabIndex = 90;
            // 
            // Label41
            // 
            this.Label41.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label41.Location = new System.Drawing.Point(47, 6);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(121, 35);
            this.Label41.TabIndex = 3;
            this.Label41.Text = "Preview";
            this.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.DrawOnGlass = false;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(830, 615);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 34);
            this.Button1.TabIndex = 65;
            this.Button1.Text = "Load";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TabControl1
            // 
            this.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabControl1.AllowDrop = true;
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TabControl1.ItemSize = new System.Drawing.Size(35, 100);
            this.TabControl1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.TabControl1.Location = new System.Drawing.Point(12, 60);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(421, 560);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.TabIndex = 200;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage1.Controls.Add(this.GroupBox2);
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Location = new System.Drawing.Point(104, 4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(313, 552);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Colors";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage2.Controls.Add(this.GroupBox4);
            this.TabPage2.Location = new System.Drawing.Point(104, 4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(313, 552);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Fonts";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.Label3);
            this.TabPage3.Controls.Add(this.GroupBox34);
            this.TabPage3.Location = new System.Drawing.Point(104, 4);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(313, 552);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Cursor";
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.DarkOrange;
            this.Label3.Location = new System.Drawing.Point(6, 106);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(301, 48);
            this.Label3.TabIndex = 112;
            this.Label3.Text = "Cursor color and style are for Windows 10 1909 and later";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage4.Controls.Add(this.Label5);
            this.TabPage4.Controls.Add(this.GroupBox12);
            this.TabPage4.Location = new System.Drawing.Point(104, 4);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(313, 552);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Tweaks";
            // 
            // FontDialog1
            // 
            this.FontDialog1.FixedPitchOnly = true;
            this.FontDialog1.ShowEffects = false;
            // 
            // CMD
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(922, 661);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.Separator2);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.GroupBox8);
            this.Controls.Add(this.Button1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CMD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terminals";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.CMD_Load);
            this.Shown += new System.EventHandler(this.CMD_Shown);
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).EndInit();
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            this.CMD_PreviewCUR.ResumeLayout(false);
            this.CMD_PreviewCUR2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).EndInit();
            this.GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.TabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

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