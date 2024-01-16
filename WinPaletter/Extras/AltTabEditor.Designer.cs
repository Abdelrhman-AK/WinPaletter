using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class AltTabEditor : AspectsTemplate
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
            this.Trackbar1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.EP_Alert = new WinPaletter.UI.WP.AlertBox();
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
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.Size = new System.Drawing.Size(893, 52);
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
            this.AlertBox2.Location = new System.Drawing.Point(9, 296);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(328, 24);
            this.AlertBox2.TabIndex = 216;
            this.AlertBox2.TabStop = false;
            this.AlertBox2.Text = "Sometimes, you should logoff and logon to apply effects";
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(9, 326);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(328, 24);
            this.AlertBox1.TabIndex = 215;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "Applying in Windows 7 may require a device restart";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox4.Controls.Add(this.Trackbar1);
            this.GroupBox4.Controls.Add(this.EP_Alert);
            this.GroupBox4.Controls.Add(this.PictureBox13);
            this.GroupBox4.Controls.Add(this.Label4);
            this.GroupBox4.Location = new System.Drawing.Point(9, 174);
            this.GroupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(328, 116);
            this.GroupBox4.TabIndex = 213;
            // 
            // Trackbar1
            // 
            this.Trackbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trackbar1.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar1.DefaultValue = 100;
            this.Trackbar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Trackbar1.Location = new System.Drawing.Point(4, 39);
            this.Trackbar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Trackbar1.Maximum = 100;
            this.Trackbar1.Minimum = 0;
            this.Trackbar1.Name = "Trackbar1";
            this.Trackbar1.Size = new System.Drawing.Size(319, 24);
            this.Trackbar1.TabIndex = 217;
            this.Trackbar1.Value = 100;
            this.Trackbar1.ValueChanged += new System.EventHandler(this.trackBarX1_ValueChanged);
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
            this.EP_Alert.Location = new System.Drawing.Point(4, 70);
            this.EP_Alert.Name = "EP_Alert";
            this.EP_Alert.Size = new System.Drawing.Size(320, 40);
            this.EP_Alert.TabIndex = 214;
            this.EP_Alert.TabStop = false;
            this.EP_Alert.Text = "Opacity control can also be enabled in Windows 11 by using ExplorerPatcher.";
            // 
            // PictureBox13
            // 
            this.PictureBox13.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox13.Image")));
            this.PictureBox13.Location = new System.Drawing.Point(3, 3);
            this.PictureBox13.Name = "PictureBox13";
            this.PictureBox13.Size = new System.Drawing.Size(30, 30);
            this.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox13.TabIndex = 1;
            this.PictureBox13.TabStop = false;
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(39, 3);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(285, 30);
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
            this.GroupBox3.Location = new System.Drawing.Point(9, 58);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(328, 110);
            this.GroupBox3.TabIndex = 212;
            // 
            // Label2
            // 
            this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(170, 86);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(69, 18);
            this.Label2.TabIndex = 113;
            this.Label2.Text = "Classic NT";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(89, 86);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 18);
            this.Label1.TabIndex = 112;
            this.Label1.Text = "Default";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioImage2
            // 
            this.RadioImage2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RadioImage2.Checked = false;
            this.RadioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage2.ForeColor = System.Drawing.Color.White;
            this.RadioImage2.Image = null;
            this.RadioImage2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage2.Location = new System.Drawing.Point(170, 43);
            this.RadioImage2.Name = "RadioImage2";
            this.RadioImage2.Size = new System.Drawing.Size(69, 40);
            this.RadioImage2.TabIndex = 3;
            this.RadioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RadioImage2.CheckedChanged += new System.EventHandler(this.RadioImage2_CheckedChanged);
            // 
            // RadioImage1
            // 
            this.RadioImage1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RadioImage1.Checked = false;
            this.RadioImage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage1.ForeColor = System.Drawing.Color.White;
            this.RadioImage1.Image = null;
            this.RadioImage1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage1.Location = new System.Drawing.Point(89, 43);
            this.RadioImage1.Name = "RadioImage1";
            this.RadioImage1.Size = new System.Drawing.Size(69, 40);
            this.RadioImage1.TabIndex = 2;
            this.RadioImage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RadioImage1.CheckedChanged += new System.EventHandler(this.RadioImage1_CheckedChanged);
            // 
            // PictureBox11
            // 
            this.PictureBox11.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(3, 3);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(30, 30);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 1;
            this.PictureBox11.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(39, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(285, 30);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Style";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.tabs_preview_1);
            this.previewContainer.Location = new System.Drawing.Point(344, 58);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(536, 306);
            this.previewContainer.TabIndex = 211;
            // 
            // tabs_preview_1
            // 
            this.tabs_preview_1.Controls.Add(this.TabPage6);
            this.tabs_preview_1.Controls.Add(this.TabPage7);
            this.tabs_preview_1.Location = new System.Drawing.Point(4, 4);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AltTabEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Windows Switcher (Alt+Tab)";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.AltTabEditor_Load);
            this.Controls.SetChildIndex(this.previewContainer, 0);
            this.Controls.SetChildIndex(this.GroupBox3, 0);
            this.Controls.SetChildIndex(this.GroupBox4, 0);
            this.Controls.SetChildIndex(this.AlertBox1, 0);
            this.Controls.SetChildIndex(this.AlertBox2, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
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
            this.ResumeLayout(false);

        }

        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.GroupBox previewContainer;
        internal UI.WP.TablessControl tabs_preview_1;
        internal TabPage TabPage6;
        internal Panel pnl_preview1;
        internal TabPage TabPage7;
        internal Panel Classic_Preview1;
        internal UI.WP.GroupBox GroupBox4;
        internal PictureBox PictureBox13;
        internal Label Label4;
        internal UI.WP.GroupBox GroupBox3;
        internal PictureBox PictureBox11;
        internal Label Label3;
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
        private UI.Controllers.TrackBarX Trackbar1;
    }
}
