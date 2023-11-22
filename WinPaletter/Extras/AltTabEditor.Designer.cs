using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class AltTabEditor : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AltTabEditor));
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.AlertBox2 = new WinPaletter.UI.WP.AlertBox();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.GroupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.EP_Alert = new WinPaletter.UI.WP.AlertBox();
            this.opacity_btn = new WinPaletter.UI.WP.Button();
            this.Trackbar1 = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox13 = new System.Windows.Forms.PictureBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.RadioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage1 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.tabs_preview_1 = new WinPaletter.UI.WP.TablessControl();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.pnl_preview1 = new System.Windows.Forms.Panel();
            this.WinElement1 = new WinPaletter.UI.Simulation.WinElement();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Classic_Preview1 = new System.Windows.Forms.Panel();
            this.PanelRRaised1 = new WinPaletter.UI.Retro.PanelRaisedR();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.PanelR1 = new WinPaletter.UI.Retro.PanelR();
            this.LabelR1 = new WinPaletter.UI.WP.LabelAlt();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.AltTabEnabled = new WinPaletter.UI.WP.Toggle();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.previewContainer.SuspendLayout();
            this.tabs_preview_1.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.pnl_preview1.SuspendLayout();
            this.TabPage7.SuspendLayout();
            this.Classic_Preview1.SuspendLayout();
            this.PanelRRaised1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.PanelR1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // AlertBox2
            // 
            this.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox2.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox2.CenterText = false;
            this.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox2.Image = null;
            this.AlertBox2.Location = new System.Drawing.Point(16, 289);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(322, 24);
            this.AlertBox2.TabIndex = 216;
            this.AlertBox2.TabStop = false;
            this.AlertBox2.Text = "Sometimes, you should logoff and logon to apply effects";
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(15, 410);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(290, 24);
            this.AlertBox1.TabIndex = 215;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "Applying in Windows 7 may require a device restart";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox4.Controls.Add(this.EP_Alert);
            this.GroupBox4.Controls.Add(this.opacity_btn);
            this.GroupBox4.Controls.Add(this.Trackbar1);
            this.GroupBox4.Controls.Add(this.PictureBox13);
            this.GroupBox4.Controls.Add(this.Label4);
            this.GroupBox4.Location = new System.Drawing.Point(12, 167);
            this.GroupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(330, 116);
            this.GroupBox4.TabIndex = 213;
            // 
            // EP_Alert
            // 
            this.EP_Alert.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.EP_Alert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EP_Alert.BackColor = System.Drawing.Color.Transparent;
            this.EP_Alert.CenterText = false;
            this.EP_Alert.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.EP_Alert.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.EP_Alert.Image = null;
            this.EP_Alert.Location = new System.Drawing.Point(4, 72);
            this.EP_Alert.Name = "EP_Alert";
            this.EP_Alert.Size = new System.Drawing.Size(322, 40);
            this.EP_Alert.TabIndex = 214;
            this.EP_Alert.TabStop = false;
            this.EP_Alert.Text = "Opacity controling can be allowed in Windows 11 too if ExplorerPatcher is install" +
    "ed";
            // 
            // opacity_btn
            // 
            this.opacity_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.opacity_btn.CustomColor = System.Drawing.Color.Empty;
            this.opacity_btn.DrawOnGlass = false;
            this.opacity_btn.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.opacity_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.opacity_btn.ForeColor = System.Drawing.Color.White;
            this.opacity_btn.Image = null;
            this.opacity_btn.Location = new System.Drawing.Point(290, 43);
            this.opacity_btn.Name = "opacity_btn";
            this.opacity_btn.Size = new System.Drawing.Size(34, 24);
            this.opacity_btn.TabIndex = 131;
            this.opacity_btn.UseVisualStyleBackColor = false;
            this.opacity_btn.Click += new System.EventHandler(this.Opacity_btn_Click);
            // 
            // Trackbar1
            // 
            this.Trackbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trackbar1.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar1.LargeChange = 10;
            this.Trackbar1.Location = new System.Drawing.Point(7, 46);
            this.Trackbar1.Maximum = 100;
            this.Trackbar1.Minimum = 0;
            this.Trackbar1.Name = "Trackbar1";
            this.Trackbar1.Size = new System.Drawing.Size(277, 19);
            this.Trackbar1.SmallChange = 1;
            this.Trackbar1.TabIndex = 130;
            this.Trackbar1.Value = 17;
            this.Trackbar1.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.Trackbar1_Scroll);
            // 
            // PictureBox13
            // 
            this.PictureBox13.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox13.Image")));
            this.PictureBox13.Location = new System.Drawing.Point(3, 3);
            this.PictureBox13.Name = "PictureBox13";
            this.PictureBox13.Size = new System.Drawing.Size(35, 35);
            this.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox13.TabIndex = 1;
            this.PictureBox13.TabStop = false;
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label4.Location = new System.Drawing.Point(40, 3);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(287, 35);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Opacity (for Windows 10)";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.Label2);
            this.GroupBox3.Controls.Add(this.Label1);
            this.GroupBox3.Controls.Add(this.RadioImage2);
            this.GroupBox3.Controls.Add(this.RadioImage1);
            this.GroupBox3.Controls.Add(this.PictureBox11);
            this.GroupBox3.Controls.Add(this.Label3);
            this.GroupBox3.Location = new System.Drawing.Point(12, 54);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(330, 110);
            this.GroupBox3.TabIndex = 212;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(163, 86);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(69, 18);
            this.Label2.TabIndex = 113;
            this.Label2.Text = "Classic NT";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(82, 86);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 18);
            this.Label1.TabIndex = 112;
            this.Label1.Text = "Default";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioImage2
            // 
            this.RadioImage2.Checked = false;
            this.RadioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage2.ForeColor = System.Drawing.Color.White;
            this.RadioImage2.Image = null;
            this.RadioImage2.ImageWithText = false;
            this.RadioImage2.Location = new System.Drawing.Point(163, 43);
            this.RadioImage2.Name = "RadioImage2";
            this.RadioImage2.ShowText = false;
            this.RadioImage2.Size = new System.Drawing.Size(69, 40);
            this.RadioImage2.TabIndex = 3;
            this.RadioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage2.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.RadioImage2_CheckedChanged);
            // 
            // RadioImage1
            // 
            this.RadioImage1.Checked = false;
            this.RadioImage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage1.ForeColor = System.Drawing.Color.White;
            this.RadioImage1.Image = null;
            this.RadioImage1.ImageWithText = false;
            this.RadioImage1.Location = new System.Drawing.Point(82, 43);
            this.RadioImage1.Name = "RadioImage1";
            this.RadioImage1.ShowText = false;
            this.RadioImage1.Size = new System.Drawing.Size(69, 40);
            this.RadioImage1.TabIndex = 2;
            this.RadioImage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage1.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.RadioImage1_CheckedChanged);
            // 
            // PictureBox11
            // 
            this.PictureBox11.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(3, 3);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(35, 35);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 1;
            this.PictureBox11.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label3.Location = new System.Drawing.Point(40, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(287, 35);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Style";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.tabs_preview_1);
            this.previewContainer.Controls.Add(this.PictureBox41);
            this.previewContainer.Controls.Add(this.Label19);
            this.previewContainer.Location = new System.Drawing.Point(345, 54);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(536, 340);
            this.previewContainer.TabIndex = 211;
            // 
            // tabs_preview_1
            // 
            this.tabs_preview_1.Controls.Add(this.TabPage6);
            this.tabs_preview_1.Controls.Add(this.TabPage7);
            this.tabs_preview_1.Location = new System.Drawing.Point(4, 39);
            this.tabs_preview_1.Name = "tabs_preview_1";
            this.tabs_preview_1.SelectedIndex = 0;
            this.tabs_preview_1.Size = new System.Drawing.Size(528, 297);
            this.tabs_preview_1.TabIndex = 120;
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage6.Controls.Add(this.pnl_preview1);
            this.TabPage6.Location = new System.Drawing.Point(4, 24);
            this.TabPage6.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(520, 269);
            this.TabPage6.TabIndex = 0;
            this.TabPage6.Text = "0";
            // 
            // pnl_preview1
            // 
            this.pnl_preview1.BackColor = System.Drawing.Color.Black;
            this.pnl_preview1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_preview1.Controls.Add(this.WinElement1);
            this.pnl_preview1.Location = new System.Drawing.Point(0, 0);
            this.pnl_preview1.Name = "pnl_preview1";
            this.pnl_preview1.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview1.TabIndex = 2;
            // 
            // WinElement1
            // 
            this.WinElement1.ActionCenterButton_Hover = System.Drawing.Color.Empty;
            this.WinElement1.ActionCenterButton_Normal = System.Drawing.Color.Empty;
            this.WinElement1.ActionCenterButton_Pressed = System.Drawing.Color.Empty;
            this.WinElement1.AppBackground = System.Drawing.Color.Empty;
            this.WinElement1.AppUnderline = System.Drawing.Color.Empty;
            this.WinElement1.BackColor = System.Drawing.Color.Transparent;
            this.WinElement1.BackColorAlpha = 130;
            this.WinElement1.Background = System.Drawing.Color.Empty;
            this.WinElement1.Background2 = System.Drawing.Color.Empty;
            this.WinElement1.BlurPower = 8;
            this.WinElement1.DarkMode = true;
            this.WinElement1.LinkColor = System.Drawing.Color.Empty;
            this.WinElement1.Location = new System.Drawing.Point(39, 73);
            this.WinElement1.Name = "WinElement1";
            this.WinElement1.NoisePower = 0.15F;
            this.WinElement1.Shadow = true;
            this.WinElement1.Size = new System.Drawing.Size(450, 150);
            this.WinElement1.StartColor = System.Drawing.Color.Empty;
            this.WinElement1.Style = WinPaletter.UI.Simulation.WinElement.Styles.AltTab11;
            this.WinElement1.SuspendRefresh = false;
            this.WinElement1.TabIndex = 0;
            this.WinElement1.Transparency = true;
            this.WinElement1.UseWin11ORB_WithWin10 = false;
            this.WinElement1.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.WinElement1.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.WinElement1.Win7ColorBal = 0;
            this.WinElement1.Win7GlowBal = 0;
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage7.Controls.Add(this.Classic_Preview1);
            this.TabPage7.Location = new System.Drawing.Point(4, 24);
            this.TabPage7.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(520, 269);
            this.TabPage7.TabIndex = 1;
            this.TabPage7.Text = "1";
            // 
            // Classic_Preview1
            // 
            this.Classic_Preview1.BackColor = System.Drawing.Color.Black;
            this.Classic_Preview1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Classic_Preview1.Controls.Add(this.PanelRRaised1);
            this.Classic_Preview1.Location = new System.Drawing.Point(0, 0);
            this.Classic_Preview1.Name = "Classic_Preview1";
            this.Classic_Preview1.Size = new System.Drawing.Size(528, 297);
            this.Classic_Preview1.TabIndex = 3;
            // 
            // PanelRRaised1
            // 
            this.PanelRRaised1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PanelRRaised1.ButtonDkShadow = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.PanelRRaised1.ButtonHilight = System.Drawing.Color.White;
            this.PanelRRaised1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.PanelRRaised1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.PanelRRaised1.Controls.Add(this.PictureBox3);
            this.PanelRRaised1.Controls.Add(this.Panel1);
            this.PanelRRaised1.Controls.Add(this.PictureBox2);
            this.PanelRRaised1.Controls.Add(this.PanelR1);
            this.PanelRRaised1.Flat = false;
            this.PanelRRaised1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.PanelRRaised1.ForeColor = System.Drawing.Color.Black;
            this.PanelRRaised1.Location = new System.Drawing.Point(99, 92);
            this.PanelRRaised1.Name = "PanelRRaised1";
            this.PanelRRaised1.Size = new System.Drawing.Size(330, 112);
            this.PanelRRaised1.Style2 = true;
            this.PanelRRaised1.TabIndex = 2;
            this.PanelRRaised1.UseItAsWin7Taskbar = false;
            // 
            // PictureBox3
            // 
            this.PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox3.Image = global::WinPaletter.Properties.Resources.SampleApp_Active;
            this.PictureBox3.Location = new System.Drawing.Point(190, 20);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(35, 35);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 5;
            this.PictureBox3.TabStop = false;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Panel2);
            this.Panel1.Location = new System.Drawing.Point(105, 17);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(40, 40);
            this.Panel1.TabIndex = 4;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.PictureBox1);
            this.Panel2.Location = new System.Drawing.Point(2, 2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Panel2.Size = new System.Drawing.Size(36, 36);
            this.Panel2.TabIndex = 5;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox1.Image = global::WinPaletter.Properties.Resources.SampleApp_Active;
            this.PictureBox1.Location = new System.Drawing.Point(3, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(30, 30);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 2;
            this.PictureBox1.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = global::WinPaletter.Properties.Resources.SampleApp_Active;
            this.PictureBox2.Location = new System.Drawing.Point(149, 20);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(35, 35);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 3;
            this.PictureBox2.TabStop = false;
            // 
            // PanelR1
            // 
            this.PanelR1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PanelR1.ButtonDkShadow = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.PanelR1.ButtonHilight = System.Drawing.Color.White;
            this.PanelR1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.PanelR1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.PanelR1.Controls.Add(this.LabelR1);
            this.PanelR1.Flat = false;
            this.PanelR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.PanelR1.ForeColor = System.Drawing.Color.Black;
            this.PanelR1.Location = new System.Drawing.Point(14, 72);
            this.PanelR1.Name = "PanelR1";
            this.PanelR1.Size = new System.Drawing.Size(302, 29);
            this.PanelR1.Style2 = true;
            this.PanelR1.TabIndex = 0;
            // 
            // LabelR1
            // 
            this.LabelR1.BackColor = System.Drawing.Color.Transparent;
            this.LabelR1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelR1.DrawOnGlass = false;
            this.LabelR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR1.ForeColor = System.Drawing.Color.Black;
            this.LabelR1.Location = new System.Drawing.Point(0, 0);
            this.LabelR1.Name = "LabelR1";
            this.LabelR1.Size = new System.Drawing.Size(302, 29);
            this.LabelR1.TabIndex = 0;
            this.LabelR1.Text = "Application";
            this.LabelR1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox41
            // 
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(4, 4);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(35, 32);
            this.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox41.TabIndex = 4;
            this.PictureBox41.TabStop = false;
            // 
            // Label19
            // 
            this.Label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(45, 5);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(270, 31);
            this.Label19.TabIndex = 3;
            this.Label19.Text = "Preview";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.Button10.Location = new System.Drawing.Point(580, 405);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(115, 34);
            this.Button10.TabIndex = 210;
            this.Button10.Text = "Quick apply";
            this.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.DrawOnGlass = false;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.Location = new System.Drawing.Point(494, 405);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(80, 34);
            this.Button7.TabIndex = 209;
            this.Button7.Text = "Cancel";
            this.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button8.CustomColor = System.Drawing.Color.Empty;
            this.Button8.DrawOnGlass = false;
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.Location = new System.Drawing.Point(701, 405);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(180, 34);
            this.Button8.TabIndex = 208;
            this.Button8.Text = "Load into current theme";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.Button9);
            this.GroupBox12.Controls.Add(this.Label12);
            this.GroupBox12.Controls.Add(this.Button11);
            this.GroupBox12.Controls.Add(this.Button12);
            this.GroupBox12.Controls.Add(this.AltTabEnabled);
            this.GroupBox12.Controls.Add(this.checker_img);
            this.GroupBox12.Location = new System.Drawing.Point(12, 12);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(869, 39);
            this.GroupBox12.TabIndex = 201;
            // 
            // Button9
            // 
            this.Button9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button9.CustomColor = System.Drawing.Color.Empty;
            this.Button9.DrawOnGlass = false;
            this.Button9.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.Location = new System.Drawing.Point(222, 5);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(126, 29);
            this.Button9.TabIndex = 112;
            this.Button9.Text = "Current applied";
            this.Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Label12
            // 
            this.Label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(4, 4);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(75, 31);
            this.Label12.TabIndex = 111;
            this.Label12.Text = "Open from:";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            this.Button11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button11.CustomColor = System.Drawing.Color.Empty;
            this.Button11.DrawOnGlass = false;
            this.Button11.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = ((System.Drawing.Image)(resources.GetObject("Button11.Image")));
            this.Button11.Location = new System.Drawing.Point(85, 5);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(135, 29);
            this.Button11.TabIndex = 110;
            this.Button11.Text = "WinPaletter theme";
            this.Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button12
            // 
            this.Button12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button12.CustomColor = System.Drawing.Color.Empty;
            this.Button12.DrawOnGlass = false;
            this.Button12.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = null;
            this.Button12.Location = new System.Drawing.Point(351, 5);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(130, 29);
            this.Button12.TabIndex = 108;
            this.Button12.Text = "Default Windows";
            this.Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // AltTabEnabled
            // 
            this.AltTabEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AltTabEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.AltTabEnabled.Checked = false;
            this.AltTabEnabled.DarkLight_Toggler = false;
            this.AltTabEnabled.Location = new System.Drawing.Point(822, 9);
            this.AltTabEnabled.Name = "AltTabEnabled";
            this.AltTabEnabled.Size = new System.Drawing.Size(40, 20);
            this.AltTabEnabled.TabIndex = 85;
            this.AltTabEnabled.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.AltTabEnabled_CheckedChanged);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(781, 4);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // AltTabEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(893, 451);
            this.Controls.Add(this.AlertBox2);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.previewContainer);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.GroupBox12);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AltTabEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Windows Switcher (Alt+Tab)";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.AltTabEditor_Load);
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.previewContainer.ResumeLayout(false);
            this.tabs_preview_1.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.pnl_preview1.ResumeLayout(false);
            this.TabPage7.ResumeLayout(false);
            this.Classic_Preview1.ResumeLayout(false);
            this.PanelRRaised1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.PanelR1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.ResumeLayout(false);

        }

        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle AltTabEnabled;
        internal PictureBox checker_img;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal UI.WP.GroupBox previewContainer;
        internal UI.WP.TablessControl tabs_preview_1;
        internal TabPage TabPage6;
        internal Panel pnl_preview1;
        internal TabPage TabPage7;
        internal Panel Classic_Preview1;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal UI.WP.GroupBox GroupBox4;
        internal PictureBox PictureBox13;
        internal Label Label4;
        internal UI.WP.GroupBox GroupBox3;
        internal PictureBox PictureBox11;
        internal Label Label3;
        internal UI.WP.Button opacity_btn;
        internal UI.WP.Trackbar Trackbar1;
        internal UI.WP.RadioImage RadioImage1;
        internal Label Label2;
        internal Label Label1;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.Simulation.WinElement WinElement1;
        internal Panel Panel1;
        internal Panel Panel2;
        internal PictureBox PictureBox1;
        internal PictureBox PictureBox2;
        internal UI.Retro.PanelR PanelR1;
        internal UI.WP.LabelAlt LabelR1;
        internal UI.Retro.PanelRaisedR PanelRRaised1;
        internal PictureBox PictureBox3;
        internal UI.WP.AlertBox EP_Alert;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.AlertBox AlertBox2;
    }
}