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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(AltTabEditor));
            OpenFileDialog1 = new OpenFileDialog();
            AlertBox2 = new UI.WP.AlertBox();
            AlertBox1 = new UI.WP.AlertBox();
            GroupBox4 = new UI.WP.GroupBox();
            EP_Alert = new UI.WP.AlertBox();
            opacity_btn = new UI.WP.Button();
            opacity_btn.Click += new EventHandler(Opacity_btn_Click);
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            PictureBox13 = new PictureBox();
            Label4 = new Label();
            GroupBox3 = new UI.WP.GroupBox();
            Label2 = new Label();
            Label1 = new Label();
            RadioImage2 = new UI.WP.RadioImage();
            RadioImage2.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage2_CheckedChanged);
            RadioImage1 = new UI.WP.RadioImage();
            RadioImage1.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(RadioImage1_CheckedChanged);
            PictureBox11 = new PictureBox();
            Label3 = new Label();
            previewContainer = new UI.WP.GroupBox();
            tabs_preview_1 = new UI.WP.TablessControl();
            TabPage6 = new TabPage();
            pnl_preview1 = new Panel();
            WinElement1 = new UI.Simulation.WinElement();
            TabPage7 = new TabPage();
            Classic_Preview1 = new Panel();
            PanelRRaised1 = new UI.Retro.PanelRaisedR();
            PictureBox3 = new PictureBox();
            Panel1 = new Panel();
            Panel2 = new Panel();
            PictureBox1 = new PictureBox();
            PictureBox2 = new PictureBox();
            PanelR1 = new UI.Retro.PanelR();
            LabelR1 = new UI.WP.LabelAlt();
            PictureBox41 = new PictureBox();
            Label19 = new Label();
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
            AltTabEnabled = new UI.WP.Toggle();
            AltTabEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(AltTabEnabled_CheckedChanged);
            checker_img = new PictureBox();
            GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            previewContainer.SuspendLayout();
            tabs_preview_1.SuspendLayout();
            TabPage6.SuspendLayout();
            pnl_preview1.SuspendLayout();
            TabPage7.SuspendLayout();
            Classic_Preview1.SuspendLayout();
            PanelRRaised1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            Panel1.SuspendLayout();
            Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            PanelR1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
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
            AlertBox2.Location = new Point(16, 289);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(322, 20);
            AlertBox2.TabIndex = 216;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "Sometimes, you should logoff and logon to apply effects";
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            AlertBox1.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(15, 413);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(324, 20);
            AlertBox1.TabIndex = 215;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "Applying in Windows 7 may require a device restart";
            // 
            // GroupBox4
            // 
            GroupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox4.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox4.Controls.Add(EP_Alert);
            GroupBox4.Controls.Add(opacity_btn);
            GroupBox4.Controls.Add(Trackbar1);
            GroupBox4.Controls.Add(PictureBox13);
            GroupBox4.Controls.Add(Label4);
            GroupBox4.Location = new Point(12, 167);
            GroupBox4.Margin = new Padding(4, 3, 4, 3);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Size = new Size(330, 116);
            GroupBox4.TabIndex = 213;
            // 
            // EP_Alert
            // 
            EP_Alert.AlertStyle = UI.WP.AlertBox.Style.Simple;
            EP_Alert.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            EP_Alert.BackColor = Color.FromArgb(50, 50, 50);
            EP_Alert.CenterText = false;
            EP_Alert.CustomColor = Color.FromArgb(0, 81, 210);
            EP_Alert.Font = new Font("Segoe UI", 9.0f);
            EP_Alert.Image = null;
            EP_Alert.Location = new Point(4, 72);
            EP_Alert.Name = "EP_Alert";
            EP_Alert.Size = new Size(322, 40);
            EP_Alert.TabIndex = 214;
            EP_Alert.TabStop = false;
            EP_Alert.Text = "Opacity controling can be allowed in Windows 11 too if ExplorerPatcher is install" + "ed";
            // 
            // opacity_btn
            // 
            opacity_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            opacity_btn.BackColor = Color.FromArgb(43, 43, 43);
            opacity_btn.DrawOnGlass = false;
            opacity_btn.Font = new Font("Segoe UI", 9.0f);
            opacity_btn.ForeColor = Color.White;
            opacity_btn.Image = null;
            opacity_btn.LineColor = Color.FromArgb(0, 81, 210);
            opacity_btn.Location = new Point(290, 43);
            opacity_btn.Name = "opacity_btn";
            opacity_btn.Size = new Size(34, 24);
            opacity_btn.TabIndex = 131;
            opacity_btn.UseVisualStyleBackColor = false;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar1.LargeChange = 10;
            Trackbar1.Location = new Point(7, 46);
            Trackbar1.Maximum = 100;
            Trackbar1.Minimum = 0;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(277, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 130;
            Trackbar1.Value = 17;
            // 
            // PictureBox13
            // 
            PictureBox13.BackColor = Color.Transparent;
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(3, 3);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(35, 35);
            PictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox13.TabIndex = 1;
            PictureBox13.TabStop = false;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label4.Location = new Point(40, 3);
            Label4.Name = "Label4";
            Label4.Size = new Size(287, 35);
            Label4.TabIndex = 0;
            Label4.Text = "Opacity (for Windows 10)";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(Label2);
            GroupBox3.Controls.Add(Label1);
            GroupBox3.Controls.Add(RadioImage2);
            GroupBox3.Controls.Add(RadioImage1);
            GroupBox3.Controls.Add(PictureBox11);
            GroupBox3.Controls.Add(Label3);
            GroupBox3.Location = new Point(12, 54);
            GroupBox3.Margin = new Padding(4, 3, 4, 3);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(330, 110);
            GroupBox3.TabIndex = 212;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(163, 86);
            Label2.Name = "Label2";
            Label2.Size = new Size(69, 18);
            Label2.TabIndex = 113;
            Label2.Text = "Classic NT";
            Label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(82, 86);
            Label1.Name = "Label1";
            Label1.Size = new Size(69, 18);
            Label1.TabIndex = 112;
            Label1.Text = "Default";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioImage2
            // 
            RadioImage2.Checked = false;
            RadioImage2.Font = new Font("Segoe UI", 9.0f);
            RadioImage2.ForeColor = Color.White;
            RadioImage2.Image = null;
            RadioImage2.Location = new Point(163, 43);
            RadioImage2.Name = "RadioImage2";
            RadioImage2.ShowText = false;
            RadioImage2.Size = new Size(69, 40);
            RadioImage2.TabIndex = 3;
            // 
            // RadioImage1
            // 
            RadioImage1.Checked = false;
            RadioImage1.Font = new Font("Segoe UI", 9.0f);
            RadioImage1.ForeColor = Color.White;
            RadioImage1.Image = null;
            RadioImage1.Location = new Point(82, 43);
            RadioImage1.Name = "RadioImage1";
            RadioImage1.ShowText = false;
            RadioImage1.Size = new Size(69, 40);
            RadioImage1.TabIndex = 2;
            // 
            // PictureBox11
            // 
            PictureBox11.BackColor = Color.Transparent;
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(3, 3);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(35, 35);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 1;
            PictureBox11.TabStop = false;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label3.Location = new Point(40, 3);
            Label3.Name = "Label3";
            Label3.Size = new Size(287, 35);
            Label3.TabIndex = 0;
            Label3.Text = "Style";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // previewContainer
            // 
            previewContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            previewContainer.BackColor = Color.FromArgb(34, 34, 34);
            previewContainer.Controls.Add(tabs_preview_1);
            previewContainer.Controls.Add(PictureBox41);
            previewContainer.Controls.Add(Label19);
            previewContainer.Location = new Point(345, 54);
            previewContainer.Margin = new Padding(4, 3, 4, 3);
            previewContainer.Name = "previewContainer";
            previewContainer.Padding = new Padding(1);
            previewContainer.Size = new Size(536, 340);
            previewContainer.TabIndex = 211;
            // 
            // tabs_preview_1
            // 
            tabs_preview_1.Controls.Add(TabPage6);
            tabs_preview_1.Controls.Add(TabPage7);
            tabs_preview_1.Location = new Point(4, 39);
            tabs_preview_1.Name = "tabs_preview_1";
            tabs_preview_1.SelectedIndex = 0;
            tabs_preview_1.Size = new Size(528, 297);
            tabs_preview_1.TabIndex = 120;
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(pnl_preview1);
            TabPage6.Location = new Point(4, 24);
            TabPage6.Margin = new Padding(0);
            TabPage6.Name = "TabPage6";
            TabPage6.Size = new Size(520, 269);
            TabPage6.TabIndex = 0;
            TabPage6.Text = "0";
            // 
            // pnl_preview1
            // 
            pnl_preview1.BackColor = Color.Black;
            pnl_preview1.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview1.Controls.Add(WinElement1);
            pnl_preview1.Location = new Point(0, 0);
            pnl_preview1.Name = "pnl_preview1";
            pnl_preview1.Size = new Size(528, 297);
            pnl_preview1.TabIndex = 2;
            // 
            // WinElement1
            // 
            WinElement1.ActionCenterButton_Hover = Color.Empty;
            WinElement1.ActionCenterButton_Normal = Color.Empty;
            WinElement1.ActionCenterButton_Pressed = Color.Empty;
            WinElement1.AppBackground = Color.Empty;
            WinElement1.AppUnderline = Color.Empty;
            WinElement1.BackColor = Color.Transparent;
            WinElement1.BackColorAlpha = 130;
            WinElement1.Background = Color.Empty;
            WinElement1.Background2 = Color.Empty;
            WinElement1.BlurPower = 8;
            WinElement1.DarkMode = true;
            WinElement1.LinkColor = Color.Empty;
            WinElement1.Location = new Point(39, 73);
            WinElement1.Name = "WinElement1";
            WinElement1.NoisePower = 0.15f;
            WinElement1.Shadow = true;
            WinElement1.Size = new Size(450, 150);
            WinElement1.StartColor = Color.Empty;
            WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab11;
            WinElement1.SuspendRefresh = false;
            WinElement1.TabIndex = 0;
            WinElement1.Transparency = true;
            WinElement1.UseWin11ORB_WithWin10 = false;
            WinElement1.UseWin11RoundedCorners_WithWin10_Level1 = false;
            WinElement1.UseWin11RoundedCorners_WithWin10_Level2 = false;
            WinElement1.Win7ColorBal = 0;
            WinElement1.Win7GlowBal = 0;
            // 
            // TabPage7
            // 
            TabPage7.BackColor = Color.FromArgb(25, 25, 25);
            TabPage7.Controls.Add(Classic_Preview1);
            TabPage7.Location = new Point(4, 24);
            TabPage7.Margin = new Padding(0);
            TabPage7.Name = "TabPage7";
            TabPage7.Size = new Size(520, 269);
            TabPage7.TabIndex = 1;
            TabPage7.Text = "1";
            // 
            // Classic_Preview1
            // 
            Classic_Preview1.BackColor = Color.Black;
            Classic_Preview1.BackgroundImageLayout = ImageLayout.Center;
            Classic_Preview1.Controls.Add(PanelRRaised1);
            Classic_Preview1.Location = new Point(0, 0);
            Classic_Preview1.Name = "Classic_Preview1";
            Classic_Preview1.Size = new Size(528, 297);
            Classic_Preview1.TabIndex = 3;
            // 
            // PanelRRaised1
            // 
            PanelRRaised1.BackColor = Color.FromArgb(192, 192, 192);
            PanelRRaised1.ButtonDkShadow = Color.FromArgb(105, 105, 105);
            PanelRRaised1.ButtonHilight = Color.White;
            PanelRRaised1.ButtonLight = Color.FromArgb(227, 227, 227);
            PanelRRaised1.ButtonShadow = Color.FromArgb(128, 128, 128);
            PanelRRaised1.Controls.Add(PictureBox3);
            PanelRRaised1.Controls.Add(Panel1);
            PanelRRaised1.Controls.Add(PictureBox2);
            PanelRRaised1.Controls.Add(PanelR1);
            PanelRRaised1.Flat = false;
            PanelRRaised1.Font = new Font("Microsoft Sans Serif", 8.0f);
            PanelRRaised1.ForeColor = Color.Black;
            PanelRRaised1.Location = new Point(99, 92);
            PanelRRaised1.Name = "PanelRRaised1";
            PanelRRaised1.Size = new Size(330, 112);
            PanelRRaised1.Style2 = true;
            PanelRRaised1.TabIndex = 2;
            PanelRRaised1.UseItAsWin7Taskbar = false;
            // 
            // PictureBox3
            // 
            PictureBox3.BackColor = Color.Transparent;
            PictureBox3.Image = Properties.Resources.SampleApp_Active;
            PictureBox3.Location = new Point(190, 20);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(35, 35);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 5;
            PictureBox3.TabStop = false;
            // 
            // Panel1
            // 
            Panel1.Controls.Add(Panel2);
            Panel1.Location = new Point(105, 17);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(40, 40);
            Panel1.TabIndex = 4;
            // 
            // Panel2
            // 
            Panel2.Controls.Add(PictureBox1);
            Panel2.Location = new Point(2, 2);
            Panel2.Name = "Panel2";
            Panel2.Padding = new Padding(3);
            Panel2.Size = new Size(36, 36);
            Panel2.TabIndex = 5;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Dock = DockStyle.Fill;
            PictureBox1.Image = Properties.Resources.SampleApp_Active;
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(30, 30);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 2;
            PictureBox1.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.BackColor = Color.Transparent;
            PictureBox2.Image = Properties.Resources.SampleApp_Active;
            PictureBox2.Location = new Point(149, 20);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(35, 35);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 3;
            PictureBox2.TabStop = false;
            // 
            // PanelR1
            // 
            PanelR1.BackColor = Color.FromArgb(192, 192, 192);
            PanelR1.ButtonDkShadow = Color.FromArgb(105, 105, 105);
            PanelR1.ButtonHilight = Color.White;
            PanelR1.ButtonLight = Color.FromArgb(227, 227, 227);
            PanelR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            PanelR1.Controls.Add(LabelR1);
            PanelR1.Flat = false;
            PanelR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            PanelR1.ForeColor = Color.Black;
            PanelR1.Location = new Point(14, 72);
            PanelR1.Name = "PanelR1";
            PanelR1.Size = new Size(302, 29);
            PanelR1.Style2 = true;
            PanelR1.TabIndex = 0;
            // 
            // LabelR1
            // 
            LabelR1.BackColor = Color.Transparent;
            LabelR1.Dock = DockStyle.Fill;
            LabelR1.DrawOnGlass = false;
            LabelR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR1.ForeColor = Color.Black;
            LabelR1.Location = new Point(0, 0);
            LabelR1.Name = "LabelR1";
            LabelR1.Size = new Size(302, 29);
            LabelR1.TabIndex = 0;
            LabelR1.Text = "Application";
            LabelR1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox41
            // 
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(4, 4);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(35, 32);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 4;
            PictureBox41.TabStop = false;
            // 
            // Label19
            // 
            Label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label19.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label19.Location = new Point(45, 5);
            Label19.Name = "Label19";
            Label19.Size = new Size(270, 31);
            Label19.TabIndex = 3;
            Label19.Text = "Preview";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
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
            Button10.Location = new Point(580, 405);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 210;
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
            Button7.Location = new Point(494, 405);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 209;
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
            Button8.Location = new Point(701, 405);
            Button8.Name = "Button8";
            Button8.Size = new Size(180, 34);
            Button8.TabIndex = 208;
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
            GroupBox12.Controls.Add(AltTabEnabled);
            GroupBox12.Controls.Add(checker_img);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(869, 39);
            GroupBox12.TabIndex = 201;
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
            // AltTabEnabled
            // 
            AltTabEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AltTabEnabled.BackColor = Color.FromArgb(43, 43, 43);
            AltTabEnabled.Checked = false;
            AltTabEnabled.DarkLight_Toggler = false;
            AltTabEnabled.Location = new Point(822, 9);
            AltTabEnabled.Name = "AltTabEnabled";
            AltTabEnabled.Size = new Size(40, 20);
            AltTabEnabled.TabIndex = 85;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = Properties.Resources.checker_disabled;
            checker_img.Location = new Point(781, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // AltTabEditor
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(893, 451);
            Controls.Add(AlertBox2);
            Controls.Add(AlertBox1);
            Controls.Add(GroupBox4);
            Controls.Add(GroupBox3);
            Controls.Add(previewContainer);
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
            Name = "AltTabEditor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Windows Switcher (Alt+Tab)";
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            previewContainer.ResumeLayout(false);
            tabs_preview_1.ResumeLayout(false);
            TabPage6.ResumeLayout(false);
            pnl_preview1.ResumeLayout(false);
            TabPage7.ResumeLayout(false);
            Classic_Preview1.ResumeLayout(false);
            PanelRRaised1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            Panel1.ResumeLayout(false);
            Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            PanelR1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            Load += new EventHandler(AltTabEditor_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

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