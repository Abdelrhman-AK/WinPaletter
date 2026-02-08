using System.Diagnostics;
using System.Windows.Forms;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store : UI.WP.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Store));
            this.ThemesFetcher = new System.ComponentModel.BackgroundWorker();
            this.Cursor_Timer = new System.Windows.Forms.Timer(this.components);
            this.Tabs = new WinPaletter.UI.WP.TablessControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.store_container = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelAlt3 = new WinPaletter.UI.WP.LabelAlt();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.author_lbl = new WinPaletter.UI.WP.LabelAlt();
            this.separatorV3 = new WinPaletter.UI.WP.SeparatorV();
            this.ver_lbl = new System.Windows.Forms.Label();
            this.separatorV1 = new WinPaletter.UI.WP.SeparatorV();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.os_12 = new System.Windows.Forms.PictureBox();
            this.os_11 = new System.Windows.Forms.PictureBox();
            this.os_10 = new System.Windows.Forms.PictureBox();
            this.os_81 = new System.Windows.Forms.PictureBox();
            this.os_8 = new System.Windows.Forms.PictureBox();
            this.os_7 = new System.Windows.Forms.PictureBox();
            this.os_vista = new System.Windows.Forms.PictureBox();
            this.os_xp = new System.Windows.Forms.PictureBox();
            this.separatorV2 = new WinPaletter.UI.WP.SeparatorV();
            this.aspect_winTheme = new System.Windows.Forms.PictureBox();
            this.aspect_lockScreen = new System.Windows.Forms.PictureBox();
            this.aspect_classicColors = new System.Windows.Forms.PictureBox();
            this.aspect_cursors = new System.Windows.Forms.PictureBox();
            this.aspect_Metrics = new System.Windows.Forms.PictureBox();
            this.aspect_cmd = new System.Windows.Forms.PictureBox();
            this.aspect_ps86 = new System.Windows.Forms.PictureBox();
            this.aspect_ps64 = new System.Windows.Forms.PictureBox();
            this.aspect_terminal = new System.Windows.Forms.PictureBox();
            this.aspect_terminalPreview = new System.Windows.Forms.PictureBox();
            this.aspect_wallpaper = new System.Windows.Forms.PictureBox();
            this.aspect_effects = new System.Windows.Forms.PictureBox();
            this.aspect_sounds = new System.Windows.Forms.PictureBox();
            this.aspect_screenSaver = new System.Windows.Forms.PictureBox();
            this.aspect_altTab = new System.Windows.Forms.PictureBox();
            this.aspect_icons = new System.Windows.Forms.PictureBox();
            this.aspect_accessibility = new System.Windows.Forms.PictureBox();
            this.aspect_winPaletterAppTheme = new System.Windows.Forms.PictureBox();
            this.separatorV4 = new WinPaletter.UI.WP.SeparatorV();
            this.lbl_hint = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.themeSize_lbl = new System.Windows.Forms.Label();
            this.SeparatorVertical1 = new WinPaletter.UI.WP.SeparatorV();
            this.respacksize_lbl = new System.Windows.Forms.Label();
            this.progressBar_ResPack = new WinPaletter.UI.WP.ProgressBar();
            this.Apply_btn = new WinPaletter.UI.WP.Button();
            this.desc_txt = new WinPaletter.UI.WP.TextBox();
            this.VersionAlert_lbl = new WinPaletter.UI.WP.AlertBox();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.FlowLayoutPanel1 = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.windowsDesktop1 = new WinPaletter.Templates.WindowsDesktop();
            this.retroDesktopColors1 = new WinPaletter.Templates.RetroDesktopColors();
            this.CMD1 = new WinPaletter.UI.Simulation.WinCMD();
            this.CMD2 = new WinPaletter.UI.Simulation.WinCMD();
            this.CMD3 = new WinPaletter.UI.Simulation.WinCMD();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new WinPaletter.UI.WP.NumericUpDown();
            this.Cursors_Container = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.Arrow = new WinPaletter.UI.Controllers.CursorControl();
            this.Help = new WinPaletter.UI.Controllers.CursorControl();
            this.AppLoading = new WinPaletter.UI.Controllers.CursorControl();
            this.Busy = new WinPaletter.UI.Controllers.CursorControl();
            this.Move_Cur = new WinPaletter.UI.Controllers.CursorControl();
            this.Up = new WinPaletter.UI.Controllers.CursorControl();
            this.NS = new WinPaletter.UI.Controllers.CursorControl();
            this.EW = new WinPaletter.UI.Controllers.CursorControl();
            this.NESW = new WinPaletter.UI.Controllers.CursorControl();
            this.NWSE = new WinPaletter.UI.Controllers.CursorControl();
            this.Pen = new WinPaletter.UI.Controllers.CursorControl();
            this.None = new WinPaletter.UI.Controllers.CursorControl();
            this.Link = new WinPaletter.UI.Controllers.CursorControl();
            this.Pin = new WinPaletter.UI.Controllers.CursorControl();
            this.Person = new WinPaletter.UI.Controllers.CursorControl();
            this.IBeam = new WinPaletter.UI.Controllers.CursorControl();
            this.Cross = new WinPaletter.UI.Controllers.CursorControl();
            this.cur_anim_btn = new WinPaletter.UI.WP.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.search_results = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.banner4 = new WinPaletter.UI.WP.Banner();
            this.groupBox11 = new WinPaletter.UI.WP.GroupBox();
            this.button2 = new WinPaletter.UI.WP.Button();
            this.button3 = new WinPaletter.UI.WP.Button();
            this.button4 = new WinPaletter.UI.WP.Button();
            this.button5 = new WinPaletter.UI.WP.Button();
            this.groupBox7 = new WinPaletter.UI.WP.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.banner3 = new WinPaletter.UI.WP.Banner();
            this.groupBox8 = new WinPaletter.UI.WP.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.banner2 = new WinPaletter.UI.WP.Banner();
            this.groupBox9 = new WinPaletter.UI.WP.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.banner1 = new WinPaletter.UI.WP.Banner();
            this.groupBox10 = new WinPaletter.UI.WP.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.banner7 = new WinPaletter.UI.WP.Banner();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.groupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.button6 = new WinPaletter.UI.WP.Button();
            this.button7 = new WinPaletter.UI.WP.Button();
            this.banner6 = new WinPaletter.UI.WP.Banner();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.banner5 = new WinPaletter.UI.WP.Banner();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.button1 = new WinPaletter.UI.WP.Button();
            this.button8 = new WinPaletter.UI.WP.Button();
            this.button9 = new WinPaletter.UI.WP.Button();
            this.button10 = new WinPaletter.UI.WP.Button();
            this.smoothFlowLayoutPanel1 = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.toggle_theme = new WinPaletter.UI.WP.CheckImage();
            this.toggle_classicColors = new WinPaletter.UI.WP.CheckImage();
            this.toggle_lockScreen = new WinPaletter.UI.WP.CheckImage();
            this.toggle_cursors = new WinPaletter.UI.WP.CheckImage();
            this.toggle_metrics = new WinPaletter.UI.WP.CheckImage();
            this.toggle_cmd = new WinPaletter.UI.WP.CheckImage();
            this.toggle_ps86 = new WinPaletter.UI.WP.CheckImage();
            this.toggle_ps64 = new WinPaletter.UI.WP.CheckImage();
            this.toggle_terminal = new WinPaletter.UI.WP.CheckImage();
            this.toggle_terminalPreview = new WinPaletter.UI.WP.CheckImage();
            this.toggle_wallpaper = new WinPaletter.UI.WP.CheckImage();
            this.toggle_effects = new WinPaletter.UI.WP.CheckImage();
            this.toggle_sounds = new WinPaletter.UI.WP.CheckImage();
            this.toggle_screenSaver = new WinPaletter.UI.WP.CheckImage();
            this.toggle_altTab = new WinPaletter.UI.WP.CheckImage();
            this.toggle_icons = new WinPaletter.UI.WP.CheckImage();
            this.toggle_accessibility = new WinPaletter.UI.WP.CheckImage();
            this.toggle_winPaletterTheme = new WinPaletter.UI.WP.CheckImage();
            this.titlebarExtender1 = new WinPaletter.Tabs.TitlebarExtender();
            this.titlebar_lbl = new WinPaletter.UI.WP.LabelAlt();
            this.avatar_btn = new WinPaletter.UI.WP.Button();
            this.flowLayoutPanel2 = new WinPaletter.UI.WP.SmoothFlowLayoutPanel();
            this.pin_button = new WinPaletter.UI.WP.Button();
            this.search_panel = new System.Windows.Forms.Panel();
            this.search_btn = new WinPaletter.UI.WP.Button();
            this.search_box = new WinPaletter.UI.WP.TextBox();
            this.search_filter_btn = new WinPaletter.UI.WP.Button();
            this.ProgressBar1 = new WinPaletter.UI.WP.ProgressBar();
            this.back_btn = new WinPaletter.UI.WP.Button();
            this.Tabs.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.os_12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_81)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_vista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_xp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_winTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_lockScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_classicColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_cursors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_Metrics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_cmd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_ps86)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_ps64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_terminal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_terminalPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_wallpaper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_effects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_sounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_screenSaver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_altTab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_icons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_accessibility)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_winPaletterAppTheme)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.previewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.FlowLayoutPanel1.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Cursors_Container.SuspendLayout();
            this.TabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.bottom_buttons.SuspendLayout();
            this.smoothFlowLayoutPanel1.SuspendLayout();
            this.titlebarExtender1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.search_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ThemesFetcher
            // 
            this.ThemesFetcher.WorkerReportsProgress = true;
            this.ThemesFetcher.WorkerSupportsCancellation = true;
            this.ThemesFetcher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FilesFetcher_DoWork);
            this.ThemesFetcher.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.FilesFetcher_ProgressChanged);
            this.ThemesFetcher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.FilesFetcher_RunWorkerCompleted);
            // 
            // Cursor_Timer
            // 
            this.Cursor_Timer.Interval = 35;
            this.Cursor_Timer.Tick += new System.EventHandler(this.Cursor_Timer_Tick);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.TabPage1);
            this.Tabs.Controls.Add(this.TabPage3);
            this.Tabs.Controls.Add(this.TabPage5);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Controls.Add(this.tabPage4);
            this.Tabs.Controls.Add(this.tabPage6);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 52);
            this.Tabs.Multiline = true;
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1329, 669);
            this.Tabs.TabIndex = 4;
            this.Tabs.SelectedIndexChanged += new System.EventHandler(this.Tabs_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage1.Controls.Add(this.store_container);
            this.TabPage1.Location = new System.Drawing.Point(4, 24);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(10);
            this.TabPage1.Size = new System.Drawing.Size(1321, 641);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "0";
            // 
            // store_container
            // 
            this.store_container.AutoScroll = true;
            this.store_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.store_container.Location = new System.Drawing.Point(10, 10);
            this.store_container.Name = "store_container";
            this.store_container.Size = new System.Drawing.Size(1301, 621);
            this.store_container.TabIndex = 3;
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.groupBox5);
            this.TabPage3.Controls.Add(this.desc_txt);
            this.TabPage3.Controls.Add(this.VersionAlert_lbl);
            this.TabPage3.Controls.Add(this.previewContainer);
            this.TabPage3.Location = new System.Drawing.Point(4, 24);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(10);
            this.TabPage3.Size = new System.Drawing.Size(1321, 641);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "1";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.flowLayoutPanel6);
            this.groupBox5.Controls.Add(this.flowLayoutPanel5);
            this.groupBox5.Controls.Add(this.flowLayoutPanel4);
            this.groupBox5.Controls.Add(this.flowLayoutPanel3);
            this.groupBox5.Controls.Add(this.Apply_btn);
            this.groupBox5.Location = new System.Drawing.Point(10, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1301, 130);
            this.groupBox5.TabIndex = 142;
            this.groupBox5.Text = "groupBox5";
            this.groupBox5.UseDecorationPattern = true;
            this.groupBox5.UseSharpStyle = false;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel6.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel6.Controls.Add(this.panel4);
            this.flowLayoutPanel6.Controls.Add(this.labelAlt3);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(2, 11);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(1090, 30);
            this.flowLayoutPanel6.TabIndex = 153;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1, 20);
            this.panel4.TabIndex = 15;
            // 
            // labelAlt3
            // 
            this.labelAlt3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAlt3.AutoSize = true;
            this.labelAlt3.BackColor = System.Drawing.Color.Transparent;
            this.labelAlt3.DrawOnGlass = false;
            this.labelAlt3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelAlt3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlt3.Location = new System.Drawing.Point(10, 0);
            this.labelAlt3.Name = "labelAlt3";
            this.labelAlt3.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.labelAlt3.Size = new System.Drawing.Size(28, 25);
            this.labelAlt3.TabIndex = 39;
            this.labelAlt3.Text = "0";
            this.labelAlt3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel5.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel5.Controls.Add(this.panel3);
            this.flowLayoutPanel5.Controls.Add(this.author_lbl);
            this.flowLayoutPanel5.Controls.Add(this.separatorV3);
            this.flowLayoutPanel5.Controls.Add(this.ver_lbl);
            this.flowLayoutPanel5.Controls.Add(this.separatorV1);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(4, 40);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(1088, 25);
            this.flowLayoutPanel5.TabIndex = 152;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 20);
            this.panel3.TabIndex = 15;
            // 
            // author_lbl
            // 
            this.author_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.author_lbl.AutoSize = true;
            this.author_lbl.BackColor = System.Drawing.Color.Transparent;
            this.author_lbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.author_lbl.DrawOnGlass = false;
            this.author_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.author_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author_lbl.Location = new System.Drawing.Point(10, 5);
            this.author_lbl.Name = "author_lbl";
            this.author_lbl.Size = new System.Drawing.Size(13, 15);
            this.author_lbl.TabIndex = 151;
            this.author_lbl.Text = "0";
            this.author_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.author_lbl.Click += new System.EventHandler(this.labelAlt4_Click);
            // 
            // separatorV3
            // 
            this.separatorV3.AlternativeLook = false;
            this.separatorV3.BackColor = System.Drawing.Color.Transparent;
            this.separatorV3.Location = new System.Drawing.Point(29, 3);
            this.separatorV3.Name = "separatorV3";
            this.separatorV3.Size = new System.Drawing.Size(1, 18);
            this.separatorV3.TabIndex = 154;
            this.separatorV3.TabStop = false;
            this.separatorV3.Text = "separatorV3";
            // 
            // ver_lbl
            // 
            this.ver_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ver_lbl.AutoSize = true;
            this.ver_lbl.BackColor = System.Drawing.Color.Transparent;
            this.ver_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ver_lbl.Location = new System.Drawing.Point(36, 5);
            this.ver_lbl.Name = "ver_lbl";
            this.ver_lbl.Size = new System.Drawing.Size(13, 15);
            this.ver_lbl.TabIndex = 155;
            this.ver_lbl.Text = "0";
            this.ver_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // separatorV1
            // 
            this.separatorV1.AlternativeLook = false;
            this.separatorV1.BackColor = System.Drawing.Color.Transparent;
            this.separatorV1.Location = new System.Drawing.Point(55, 3);
            this.separatorV1.Name = "separatorV1";
            this.separatorV1.Size = new System.Drawing.Size(1, 18);
            this.separatorV1.TabIndex = 152;
            this.separatorV1.TabStop = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel4.Controls.Add(this.os_12);
            this.flowLayoutPanel4.Controls.Add(this.os_11);
            this.flowLayoutPanel4.Controls.Add(this.os_10);
            this.flowLayoutPanel4.Controls.Add(this.os_81);
            this.flowLayoutPanel4.Controls.Add(this.os_8);
            this.flowLayoutPanel4.Controls.Add(this.os_7);
            this.flowLayoutPanel4.Controls.Add(this.os_vista);
            this.flowLayoutPanel4.Controls.Add(this.os_xp);
            this.flowLayoutPanel4.Controls.Add(this.separatorV2);
            this.flowLayoutPanel4.Controls.Add(this.aspect_winTheme);
            this.flowLayoutPanel4.Controls.Add(this.aspect_lockScreen);
            this.flowLayoutPanel4.Controls.Add(this.aspect_classicColors);
            this.flowLayoutPanel4.Controls.Add(this.aspect_cursors);
            this.flowLayoutPanel4.Controls.Add(this.aspect_Metrics);
            this.flowLayoutPanel4.Controls.Add(this.aspect_cmd);
            this.flowLayoutPanel4.Controls.Add(this.aspect_ps86);
            this.flowLayoutPanel4.Controls.Add(this.aspect_ps64);
            this.flowLayoutPanel4.Controls.Add(this.aspect_terminal);
            this.flowLayoutPanel4.Controls.Add(this.aspect_terminalPreview);
            this.flowLayoutPanel4.Controls.Add(this.aspect_wallpaper);
            this.flowLayoutPanel4.Controls.Add(this.aspect_effects);
            this.flowLayoutPanel4.Controls.Add(this.aspect_sounds);
            this.flowLayoutPanel4.Controls.Add(this.aspect_screenSaver);
            this.flowLayoutPanel4.Controls.Add(this.aspect_altTab);
            this.flowLayoutPanel4.Controls.Add(this.aspect_icons);
            this.flowLayoutPanel4.Controls.Add(this.aspect_accessibility);
            this.flowLayoutPanel4.Controls.Add(this.aspect_winPaletterAppTheme);
            this.flowLayoutPanel4.Controls.Add(this.separatorV4);
            this.flowLayoutPanel4.Controls.Add(this.lbl_hint);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(11, 89);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1081, 30);
            this.flowLayoutPanel4.TabIndex = 150;
            // 
            // os_12
            // 
            this.os_12.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_12.Location = new System.Drawing.Point(3, 3);
            this.os_12.Name = "os_12";
            this.os_12.Size = new System.Drawing.Size(24, 24);
            this.os_12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_12.TabIndex = 150;
            this.os_12.TabStop = false;
            this.os_12.Tag = "Modifies Windows 12\'s preferences";
            // 
            // os_11
            // 
            this.os_11.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_11.Location = new System.Drawing.Point(33, 3);
            this.os_11.Name = "os_11";
            this.os_11.Size = new System.Drawing.Size(24, 24);
            this.os_11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_11.TabIndex = 151;
            this.os_11.TabStop = false;
            this.os_11.Tag = "Modifies Windows 11\'s preferences";
            // 
            // os_10
            // 
            this.os_10.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_10.Location = new System.Drawing.Point(63, 3);
            this.os_10.Name = "os_10";
            this.os_10.Size = new System.Drawing.Size(24, 24);
            this.os_10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_10.TabIndex = 152;
            this.os_10.TabStop = false;
            this.os_10.Tag = "Modifies Windows 10\'s preferences";
            // 
            // os_81
            // 
            this.os_81.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_81.Location = new System.Drawing.Point(93, 3);
            this.os_81.Name = "os_81";
            this.os_81.Size = new System.Drawing.Size(24, 24);
            this.os_81.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_81.TabIndex = 153;
            this.os_81.TabStop = false;
            this.os_81.Tag = "Modifies Windows 8.1\'s preferences";
            // 
            // os_8
            // 
            this.os_8.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_8.Location = new System.Drawing.Point(123, 3);
            this.os_8.Name = "os_8";
            this.os_8.Size = new System.Drawing.Size(24, 24);
            this.os_8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_8.TabIndex = 154;
            this.os_8.TabStop = false;
            this.os_8.Tag = "Modifies Windows 8\'s preferences";
            // 
            // os_7
            // 
            this.os_7.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_7.Location = new System.Drawing.Point(153, 3);
            this.os_7.Name = "os_7";
            this.os_7.Size = new System.Drawing.Size(24, 24);
            this.os_7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_7.TabIndex = 155;
            this.os_7.TabStop = false;
            this.os_7.Tag = "Modifies Windows 7\'s preferences";
            // 
            // os_vista
            // 
            this.os_vista.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_vista.Location = new System.Drawing.Point(183, 3);
            this.os_vista.Name = "os_vista";
            this.os_vista.Size = new System.Drawing.Size(24, 24);
            this.os_vista.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_vista.TabIndex = 156;
            this.os_vista.TabStop = false;
            this.os_vista.Tag = "Modifies Windows Vista\'s preferences";
            // 
            // os_xp
            // 
            this.os_xp.Cursor = System.Windows.Forms.Cursors.Help;
            this.os_xp.Location = new System.Drawing.Point(213, 3);
            this.os_xp.Name = "os_xp";
            this.os_xp.Size = new System.Drawing.Size(24, 24);
            this.os_xp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.os_xp.TabIndex = 157;
            this.os_xp.TabStop = false;
            this.os_xp.Tag = "Modifies Windows XP\'s preferences";
            // 
            // separatorV2
            // 
            this.separatorV2.AlternativeLook = false;
            this.separatorV2.BackColor = System.Drawing.Color.Transparent;
            this.separatorV2.Location = new System.Drawing.Point(243, 3);
            this.separatorV2.Name = "separatorV2";
            this.separatorV2.Size = new System.Drawing.Size(1, 24);
            this.separatorV2.TabIndex = 158;
            this.separatorV2.TabStop = false;
            this.separatorV2.Text = "separatorV2";
            // 
            // aspect_winTheme
            // 
            this.aspect_winTheme.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_winTheme.Location = new System.Drawing.Point(250, 3);
            this.aspect_winTheme.Name = "aspect_winTheme";
            this.aspect_winTheme.Size = new System.Drawing.Size(24, 24);
            this.aspect_winTheme.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_winTheme.TabIndex = 159;
            this.aspect_winTheme.TabStop = false;
            this.aspect_winTheme.Tag = "Modifies Windows Theme";
            // 
            // aspect_lockScreen
            // 
            this.aspect_lockScreen.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_lockScreen.Location = new System.Drawing.Point(280, 3);
            this.aspect_lockScreen.Name = "aspect_lockScreen";
            this.aspect_lockScreen.Size = new System.Drawing.Size(24, 24);
            this.aspect_lockScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_lockScreen.TabIndex = 160;
            this.aspect_lockScreen.TabStop = false;
            this.aspect_lockScreen.Tag = "Modifies Lock Screen";
            // 
            // aspect_classicColors
            // 
            this.aspect_classicColors.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_classicColors.Location = new System.Drawing.Point(310, 3);
            this.aspect_classicColors.Name = "aspect_classicColors";
            this.aspect_classicColors.Size = new System.Drawing.Size(24, 24);
            this.aspect_classicColors.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_classicColors.TabIndex = 161;
            this.aspect_classicColors.TabStop = false;
            this.aspect_classicColors.Tag = "Modifies Classic Colors";
            // 
            // aspect_cursors
            // 
            this.aspect_cursors.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_cursors.Location = new System.Drawing.Point(340, 3);
            this.aspect_cursors.Name = "aspect_cursors";
            this.aspect_cursors.Size = new System.Drawing.Size(24, 24);
            this.aspect_cursors.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_cursors.TabIndex = 162;
            this.aspect_cursors.TabStop = false;
            this.aspect_cursors.Tag = "Modifies Cursors";
            // 
            // aspect_Metrics
            // 
            this.aspect_Metrics.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_Metrics.Location = new System.Drawing.Point(370, 3);
            this.aspect_Metrics.Name = "aspect_Metrics";
            this.aspect_Metrics.Size = new System.Drawing.Size(24, 24);
            this.aspect_Metrics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_Metrics.TabIndex = 163;
            this.aspect_Metrics.TabStop = false;
            this.aspect_Metrics.Tag = "Modifies Metrics & Fonts";
            // 
            // aspect_cmd
            // 
            this.aspect_cmd.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_cmd.Location = new System.Drawing.Point(400, 3);
            this.aspect_cmd.Name = "aspect_cmd";
            this.aspect_cmd.Size = new System.Drawing.Size(24, 24);
            this.aspect_cmd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_cmd.TabIndex = 164;
            this.aspect_cmd.TabStop = false;
            this.aspect_cmd.Tag = "Modifies Command Prompt\'s appearance";
            // 
            // aspect_ps86
            // 
            this.aspect_ps86.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_ps86.Location = new System.Drawing.Point(430, 3);
            this.aspect_ps86.Name = "aspect_ps86";
            this.aspect_ps86.Size = new System.Drawing.Size(24, 24);
            this.aspect_ps86.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_ps86.TabIndex = 165;
            this.aspect_ps86.TabStop = false;
            this.aspect_ps86.Tag = "Modifies PowerShell x86\'s appearance";
            // 
            // aspect_ps64
            // 
            this.aspect_ps64.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_ps64.Location = new System.Drawing.Point(460, 3);
            this.aspect_ps64.Name = "aspect_ps64";
            this.aspect_ps64.Size = new System.Drawing.Size(24, 24);
            this.aspect_ps64.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_ps64.TabIndex = 166;
            this.aspect_ps64.TabStop = false;
            this.aspect_ps64.Tag = "Modifies PowerShell x64\'s appearance";
            // 
            // aspect_terminal
            // 
            this.aspect_terminal.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_terminal.Location = new System.Drawing.Point(490, 3);
            this.aspect_terminal.Name = "aspect_terminal";
            this.aspect_terminal.Size = new System.Drawing.Size(24, 24);
            this.aspect_terminal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_terminal.TabIndex = 167;
            this.aspect_terminal.TabStop = false;
            this.aspect_terminal.Tag = "Modifies Microsoft Terminal\'s appearance";
            // 
            // aspect_terminalPreview
            // 
            this.aspect_terminalPreview.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_terminalPreview.Location = new System.Drawing.Point(520, 3);
            this.aspect_terminalPreview.Name = "aspect_terminalPreview";
            this.aspect_terminalPreview.Size = new System.Drawing.Size(24, 24);
            this.aspect_terminalPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_terminalPreview.TabIndex = 168;
            this.aspect_terminalPreview.TabStop = false;
            this.aspect_terminalPreview.Tag = "Modifies Microsoft Terminal Preview\'s appearance";
            // 
            // aspect_wallpaper
            // 
            this.aspect_wallpaper.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_wallpaper.Location = new System.Drawing.Point(550, 3);
            this.aspect_wallpaper.Name = "aspect_wallpaper";
            this.aspect_wallpaper.Size = new System.Drawing.Size(24, 24);
            this.aspect_wallpaper.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_wallpaper.TabIndex = 169;
            this.aspect_wallpaper.TabStop = false;
            this.aspect_wallpaper.Tag = "Modifies Wallpaper";
            // 
            // aspect_effects
            // 
            this.aspect_effects.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_effects.Location = new System.Drawing.Point(580, 3);
            this.aspect_effects.Name = "aspect_effects";
            this.aspect_effects.Size = new System.Drawing.Size(24, 24);
            this.aspect_effects.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_effects.TabIndex = 170;
            this.aspect_effects.TabStop = false;
            this.aspect_effects.Tag = "Modifies Windows Effects";
            // 
            // aspect_sounds
            // 
            this.aspect_sounds.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_sounds.Location = new System.Drawing.Point(610, 3);
            this.aspect_sounds.Name = "aspect_sounds";
            this.aspect_sounds.Size = new System.Drawing.Size(24, 24);
            this.aspect_sounds.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_sounds.TabIndex = 171;
            this.aspect_sounds.TabStop = false;
            this.aspect_sounds.Tag = "Modifies Sounds schemes";
            // 
            // aspect_screenSaver
            // 
            this.aspect_screenSaver.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_screenSaver.Location = new System.Drawing.Point(640, 3);
            this.aspect_screenSaver.Name = "aspect_screenSaver";
            this.aspect_screenSaver.Size = new System.Drawing.Size(24, 24);
            this.aspect_screenSaver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_screenSaver.TabIndex = 172;
            this.aspect_screenSaver.TabStop = false;
            this.aspect_screenSaver.Tag = "Modifies Screen Saver";
            // 
            // aspect_altTab
            // 
            this.aspect_altTab.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_altTab.Location = new System.Drawing.Point(670, 3);
            this.aspect_altTab.Name = "aspect_altTab";
            this.aspect_altTab.Size = new System.Drawing.Size(24, 24);
            this.aspect_altTab.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_altTab.TabIndex = 173;
            this.aspect_altTab.TabStop = false;
            this.aspect_altTab.Tag = "Modifies Windows Switcher (Alt+Tab)\'s appearamce";
            // 
            // aspect_icons
            // 
            this.aspect_icons.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_icons.Location = new System.Drawing.Point(700, 3);
            this.aspect_icons.Name = "aspect_icons";
            this.aspect_icons.Size = new System.Drawing.Size(24, 24);
            this.aspect_icons.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_icons.TabIndex = 174;
            this.aspect_icons.TabStop = false;
            this.aspect_icons.Tag = "Modifies Windows Icons";
            // 
            // aspect_accessibility
            // 
            this.aspect_accessibility.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_accessibility.Location = new System.Drawing.Point(730, 3);
            this.aspect_accessibility.Name = "aspect_accessibility";
            this.aspect_accessibility.Size = new System.Drawing.Size(24, 24);
            this.aspect_accessibility.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_accessibility.TabIndex = 175;
            this.aspect_accessibility.TabStop = false;
            this.aspect_accessibility.Tag = "Modifies Accessibility";
            // 
            // aspect_winPaletterAppTheme
            // 
            this.aspect_winPaletterAppTheme.Cursor = System.Windows.Forms.Cursors.Help;
            this.aspect_winPaletterAppTheme.Location = new System.Drawing.Point(760, 3);
            this.aspect_winPaletterAppTheme.Name = "aspect_winPaletterAppTheme";
            this.aspect_winPaletterAppTheme.Size = new System.Drawing.Size(24, 24);
            this.aspect_winPaletterAppTheme.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.aspect_winPaletterAppTheme.TabIndex = 176;
            this.aspect_winPaletterAppTheme.TabStop = false;
            this.aspect_winPaletterAppTheme.Tag = "Modifies WinPaletter Application theme";
            // 
            // separatorV4
            // 
            this.separatorV4.AlternativeLook = false;
            this.separatorV4.BackColor = System.Drawing.Color.Transparent;
            this.separatorV4.Location = new System.Drawing.Point(790, 3);
            this.separatorV4.Name = "separatorV4";
            this.separatorV4.Size = new System.Drawing.Size(1, 24);
            this.separatorV4.TabIndex = 177;
            this.separatorV4.TabStop = false;
            this.separatorV4.Text = "separatorV4";
            // 
            // lbl_hint
            // 
            this.lbl_hint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_hint.AutoSize = true;
            this.lbl_hint.BackColor = System.Drawing.Color.Transparent;
            this.lbl_hint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hint.Location = new System.Drawing.Point(797, 7);
            this.lbl_hint.Name = "lbl_hint";
            this.lbl_hint.Size = new System.Drawing.Size(10, 15);
            this.lbl_hint.TabIndex = 178;
            this.lbl_hint.Text = " ";
            this.lbl_hint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel3.Controls.Add(this.panel2);
            this.flowLayoutPanel3.Controls.Add(this.themeSize_lbl);
            this.flowLayoutPanel3.Controls.Add(this.SeparatorVertical1);
            this.flowLayoutPanel3.Controls.Add(this.respacksize_lbl);
            this.flowLayoutPanel3.Controls.Add(this.progressBar_ResPack);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(4, 63);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(1088, 25);
            this.flowLayoutPanel3.TabIndex = 149;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 20);
            this.panel2.TabIndex = 14;
            // 
            // themeSize_lbl
            // 
            this.themeSize_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.themeSize_lbl.AutoSize = true;
            this.themeSize_lbl.BackColor = System.Drawing.Color.Transparent;
            this.themeSize_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themeSize_lbl.Location = new System.Drawing.Point(10, 5);
            this.themeSize_lbl.Name = "themeSize_lbl";
            this.themeSize_lbl.Size = new System.Drawing.Size(13, 15);
            this.themeSize_lbl.TabIndex = 13;
            this.themeSize_lbl.Text = "0";
            this.themeSize_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SeparatorVertical1
            // 
            this.SeparatorVertical1.AlternativeLook = false;
            this.SeparatorVertical1.BackColor = System.Drawing.Color.Transparent;
            this.SeparatorVertical1.Location = new System.Drawing.Point(29, 3);
            this.SeparatorVertical1.Name = "SeparatorVertical1";
            this.SeparatorVertical1.Size = new System.Drawing.Size(1, 18);
            this.SeparatorVertical1.TabIndex = 143;
            this.SeparatorVertical1.TabStop = false;
            this.SeparatorVertical1.Text = "SeparatorVertical1";
            // 
            // respacksize_lbl
            // 
            this.respacksize_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.respacksize_lbl.AutoSize = true;
            this.respacksize_lbl.BackColor = System.Drawing.Color.Transparent;
            this.respacksize_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respacksize_lbl.Location = new System.Drawing.Point(36, 5);
            this.respacksize_lbl.Name = "respacksize_lbl";
            this.respacksize_lbl.Size = new System.Drawing.Size(13, 15);
            this.respacksize_lbl.TabIndex = 16;
            this.respacksize_lbl.Text = "0";
            this.respacksize_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar_ResPack
            // 
            this.progressBar_ResPack.Appearance = WinPaletter.UI.WP.ProgressBar.ProgressBarAppearance.Circle;
            this.progressBar_ResPack.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_ResPack.Location = new System.Drawing.Point(55, 3);
            this.progressBar_ResPack.Name = "progressBar_ResPack";
            this.progressBar_ResPack.Size = new System.Drawing.Size(18, 18);
            this.progressBar_ResPack.State = WinPaletter.UI.WP.ProgressBar.ProgressBarState.Normal;
            this.progressBar_ResPack.Style = WinPaletter.UI.WP.ProgressBar.ProgressBarStyle.Marquee;
            this.progressBar_ResPack.TabIndex = 148;
            this.progressBar_ResPack.TaskbarBroadcast = true;
            this.progressBar_ResPack.Visible = false;
            // 
            // Apply_btn
            // 
            this.Apply_btn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Apply_btn.CustomColor = System.Drawing.Color.Empty;
            this.Apply_btn.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Apply_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Apply_btn.ForeColor = System.Drawing.Color.White;
            this.Apply_btn.Image = null;
            this.Apply_btn.ImageGlyph = null;
            this.Apply_btn.ImageGlyphEnabled = false;
            this.Apply_btn.Location = new System.Drawing.Point(1101, 44);
            this.Apply_btn.Name = "Apply_btn";
            this.Apply_btn.Size = new System.Drawing.Size(165, 42);
            this.Apply_btn.TabIndex = 134;
            this.Apply_btn.Text = "Apply";
            this.Apply_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Apply_btn.UseVisualStyleBackColor = false;
            this.Apply_btn.Click += new System.EventHandler(this.Apply_Edit_btn_Click);
            // 
            // desc_txt
            // 
            this.desc_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.desc_txt.Location = new System.Drawing.Point(10, 522);
            this.desc_txt.MaxLength = 32767;
            this.desc_txt.Multiline = true;
            this.desc_txt.Name = "desc_txt";
            this.desc_txt.ReadOnly = true;
            this.desc_txt.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.desc_txt.SelectedText = "";
            this.desc_txt.SelectionLength = 0;
            this.desc_txt.SelectionStart = 0;
            this.desc_txt.Size = new System.Drawing.Size(1301, 71);
            this.desc_txt.TabIndex = 147;
            this.desc_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.desc_txt.UseSystemPasswordChar = false;
            this.desc_txt.WordWrap = true;
            // 
            // VersionAlert_lbl
            // 
            this.VersionAlert_lbl.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.VersionAlert_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionAlert_lbl.BackColor = System.Drawing.Color.Transparent;
            this.VersionAlert_lbl.CenterText = false;
            this.VersionAlert_lbl.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.VersionAlert_lbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VersionAlert_lbl.Image = ((System.Drawing.Image)(resources.GetObject("VersionAlert_lbl.Image")));
            this.VersionAlert_lbl.Location = new System.Drawing.Point(10, 599);
            this.VersionAlert_lbl.Name = "VersionAlert_lbl";
            this.VersionAlert_lbl.Size = new System.Drawing.Size(1301, 34);
            this.VersionAlert_lbl.TabIndex = 140;
            this.VersionAlert_lbl.TabStop = false;
            this.VersionAlert_lbl.Text = "0";
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.PictureBox41);
            this.previewContainer.Controls.Add(this.Label19);
            this.previewContainer.Controls.Add(this.FlowLayoutPanel1);
            this.previewContainer.Location = new System.Drawing.Point(10, 146);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(1301, 370);
            this.previewContainer.TabIndex = 131;
            this.previewContainer.UseDecorationPattern = false;
            this.previewContainer.UseSharpStyle = false;
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
            this.Label19.Size = new System.Drawing.Size(1252, 31);
            this.Label19.TabIndex = 3;
            this.Label19.Text = "Preview";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayoutPanel1.AutoScroll = true;
            this.FlowLayoutPanel1.Controls.Add(this.windowsDesktop1);
            this.FlowLayoutPanel1.Controls.Add(this.retroDesktopColors1);
            this.FlowLayoutPanel1.Controls.Add(this.CMD1);
            this.FlowLayoutPanel1.Controls.Add(this.CMD2);
            this.FlowLayoutPanel1.Controls.Add(this.CMD3);
            this.FlowLayoutPanel1.Controls.Add(this.Panel1);
            this.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(4, 41);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(1293, 324);
            this.FlowLayoutPanel1.TabIndex = 139;
            // 
            // windowsDesktop1
            // 
            this.windowsDesktop1.AccentLevel = WinPaletter.Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            this.windowsDesktop1.ActiveBorder = System.Drawing.Color.Empty;
            this.windowsDesktop1.ActiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.AfterGlowColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.windowsDesktop1.AfterGlowColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.windowsDesktop1.BackColor = System.Drawing.SystemColors.Desktop;
            this.windowsDesktop1.Background = System.Drawing.Color.Empty;
            this.windowsDesktop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.windowsDesktop1.ButtonDkShadow = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonFace = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonHilight = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonLight = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonShadow = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonText = System.Drawing.Color.Empty;
            this.windowsDesktop1.Classic = false;
            this.windowsDesktop1.Color1 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color2 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color3 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color4 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color5 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color6 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color7 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color8 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color9 = System.Drawing.Color.Empty;
            this.windowsDesktop1.DarkMode_App = true;
            this.windowsDesktop1.DarkMode_Win = true;
            this.windowsDesktop1.EnableEditingColors = false;
            this.windowsDesktop1.EnableGradient = true;
            this.windowsDesktop1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowsDesktop1.GradientActiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.GradientInactiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.GrayText = System.Drawing.Color.Empty;
            this.windowsDesktop1.InactiveBorder = System.Drawing.Color.Empty;
            this.windowsDesktop1.InactiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.InactiveTitleText = System.Drawing.Color.Empty;
            this.windowsDesktop1.IncreaseTBTransparency = false;
            this.windowsDesktop1.Location = new System.Drawing.Point(4, 3);
            this.windowsDesktop1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.windowsDesktop1.Metrics_BorderWidth = 1;
            this.windowsDesktop1.Metrics_CaptionFont = new System.Drawing.Font("Segoe UI", 9F);
            this.windowsDesktop1.Metrics_CaptionHeight = 22;
            this.windowsDesktop1.Metrics_CaptionWidth = 22;
            this.windowsDesktop1.Metrics_PaddedBorderWidth = 4;
            this.windowsDesktop1.Name = "windowsDesktop1";
            this.windowsDesktop1.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.windowsDesktop1.resVS = null;
            this.windowsDesktop1.Shadow = true;
            this.windowsDesktop1.Size = new System.Drawing.Size(528, 297);
            this.windowsDesktop1.TabIndex = 141;
            this.windowsDesktop1.TB_Blur = false;
            this.windowsDesktop1.TitlebarColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.windowsDesktop1.TitlebarColor_Enabled = false;
            this.windowsDesktop1.TitlebarColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.windowsDesktop1.TitleText = System.Drawing.Color.Empty;
            this.windowsDesktop1.Transparency = true;
            this.windowsDesktop1.UseWin11ORB_WithWin10 = false;
            this.windowsDesktop1.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.windowsDesktop1.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.windowsDesktop1.VisualStyles = WinPaletter.Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;
            this.windowsDesktop1.VisualStylesColorScheme = null;
            this.windowsDesktop1.VisualStylesPath = null;
            this.windowsDesktop1.Win7Alpha = 100;
            this.windowsDesktop1.Win7ColorBal = 100;
            this.windowsDesktop1.Win7GlowBal = 100;
            this.windowsDesktop1.Win7Noise = 1F;
            this.windowsDesktop1.Window = System.Drawing.Color.Empty;
            this.windowsDesktop1.WindowFrame = System.Drawing.Color.Empty;
            this.windowsDesktop1.WindowStyle = WinPaletter.PreviewHelpers.WindowStyle.W11;
            this.windowsDesktop1.WindowText = System.Drawing.Color.Empty;
            this.windowsDesktop1.WinVista = false;
            // 
            // retroDesktopColors1
            // 
            this.retroDesktopColors1.ActiveBorder = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ActiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.AppWorkspace = System.Drawing.Color.Empty;
            this.retroDesktopColors1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.retroDesktopColors1.Background = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonAlternateFace = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonDkShadow = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonFace = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonHilight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonLight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonShadow = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Desktop = System.Drawing.Color.Empty;
            this.retroDesktopColors1.EnableEditingColors = false;
            this.retroDesktopColors1.EnableGradient = true;
            this.retroDesktopColors1.EnableTheming = false;
            this.retroDesktopColors1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retroDesktopColors1.GradientActiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.GradientInactiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.GrayText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Hilight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.HilightText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.HotTrackingColor = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InactiveBorder = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InactiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InactiveTitleText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InfoText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InfoWindow = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Location = new System.Drawing.Point(540, 3);
            this.retroDesktopColors1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.retroDesktopColors1.Menu = System.Drawing.Color.Empty;
            this.retroDesktopColors1.MenuBar = System.Drawing.Color.Empty;
            this.retroDesktopColors1.MenuHilight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.MenuText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Name = "retroDesktopColors1";
            this.retroDesktopColors1.Scrollbar = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Size = new System.Drawing.Size(528, 297);
            this.retroDesktopColors1.TabIndex = 0;
            this.retroDesktopColors1.TitleText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Window = System.Drawing.Color.Empty;
            this.retroDesktopColors1.WindowFrame = System.Drawing.Color.Empty;
            this.retroDesktopColors1.WindowText = System.Drawing.Color.Empty;
            // 
            // CMD1
            // 
            this.CMD1.Alpha = 255;
            this.CMD1.BackColor = System.Drawing.Color.Transparent;
            this.CMD1.CMD_ColorTable00 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable01 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable02 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable03 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable04 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable05 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable06 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable07 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable08 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable09 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable10 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable11 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable12 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable13 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable14 = System.Drawing.Color.Empty;
            this.CMD1.CMD_ColorTable15 = System.Drawing.Color.Empty;
            this.CMD1.CMD_PopupBackground = 5;
            this.CMD1.CMD_PopupForeground = 15;
            this.CMD1.CMD_ScreenColorsBackground = 0;
            this.CMD1.CMD_ScreenColorsForeground = 7;
            this.CMD1.CustomTerminal = false;
            this.CMD1.Location = new System.Drawing.Point(1075, 3);
            this.CMD1.Name = "CMD1";
            this.CMD1.PowerShell = false;
            this.CMD1.Raster = true;
            this.CMD1.RasterSize = WinPaletter.UI.Simulation.WinCMD.Raster_Sizes._8x12;
            this.CMD1.Size = new System.Drawing.Size(528, 297);
            this.CMD1.TabIndex = 1;
            // 
            // CMD2
            // 
            this.CMD2.Alpha = 255;
            this.CMD2.BackColor = System.Drawing.Color.Transparent;
            this.CMD2.CMD_ColorTable00 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable01 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable02 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable03 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable04 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable05 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable06 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable07 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable08 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable09 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable10 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable11 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable12 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable13 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable14 = System.Drawing.Color.Empty;
            this.CMD2.CMD_ColorTable15 = System.Drawing.Color.Empty;
            this.CMD2.CMD_PopupBackground = 5;
            this.CMD2.CMD_PopupForeground = 15;
            this.CMD2.CMD_ScreenColorsBackground = 0;
            this.CMD2.CMD_ScreenColorsForeground = 7;
            this.CMD2.CustomTerminal = false;
            this.CMD2.Location = new System.Drawing.Point(1609, 3);
            this.CMD2.Name = "CMD2";
            this.CMD2.PowerShell = false;
            this.CMD2.Raster = true;
            this.CMD2.RasterSize = WinPaletter.UI.Simulation.WinCMD.Raster_Sizes._8x12;
            this.CMD2.Size = new System.Drawing.Size(528, 297);
            this.CMD2.TabIndex = 2;
            // 
            // CMD3
            // 
            this.CMD3.Alpha = 255;
            this.CMD3.BackColor = System.Drawing.Color.Transparent;
            this.CMD3.CMD_ColorTable00 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable01 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable02 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable03 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable04 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable05 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable06 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable07 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable08 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable09 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable10 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable11 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable12 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable13 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable14 = System.Drawing.Color.Empty;
            this.CMD3.CMD_ColorTable15 = System.Drawing.Color.Empty;
            this.CMD3.CMD_PopupBackground = 5;
            this.CMD3.CMD_PopupForeground = 15;
            this.CMD3.CMD_ScreenColorsBackground = 0;
            this.CMD3.CMD_ScreenColorsForeground = 7;
            this.CMD3.CustomTerminal = false;
            this.CMD3.Location = new System.Drawing.Point(2143, 3);
            this.CMD3.Name = "CMD3";
            this.CMD3.PowerShell = false;
            this.CMD3.Raster = true;
            this.CMD3.RasterSize = WinPaletter.UI.Simulation.WinCMD.Raster_Sizes._8x12;
            this.CMD3.Size = new System.Drawing.Size(528, 297);
            this.CMD3.TabIndex = 2;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.numericUpDown1);
            this.Panel1.Controls.Add(this.Cursors_Container);
            this.Panel1.Controls.Add(this.cur_anim_btn);
            this.Panel1.Location = new System.Drawing.Point(2677, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(528, 297);
            this.Panel1.TabIndex = 140;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDown1.BackColor = System.Drawing.Color.Transparent;
            this.numericUpDown1.Location = new System.Drawing.Point(476, 265);
            this.numericUpDown1.Maximum = 20;
            this.numericUpDown1.Minimum = 3;
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 24);
            this.numericUpDown1.TabIndex = 73;
            this.numericUpDown1.UpDownStep = 1;
            this.numericUpDown1.Value = 3;
            // 
            // Cursors_Container
            // 
            this.Cursors_Container.AutoScroll = true;
            this.Cursors_Container.Controls.Add(this.Arrow);
            this.Cursors_Container.Controls.Add(this.Help);
            this.Cursors_Container.Controls.Add(this.AppLoading);
            this.Cursors_Container.Controls.Add(this.Busy);
            this.Cursors_Container.Controls.Add(this.Move_Cur);
            this.Cursors_Container.Controls.Add(this.Up);
            this.Cursors_Container.Controls.Add(this.NS);
            this.Cursors_Container.Controls.Add(this.EW);
            this.Cursors_Container.Controls.Add(this.NESW);
            this.Cursors_Container.Controls.Add(this.NWSE);
            this.Cursors_Container.Controls.Add(this.Pen);
            this.Cursors_Container.Controls.Add(this.None);
            this.Cursors_Container.Controls.Add(this.Link);
            this.Cursors_Container.Controls.Add(this.Pin);
            this.Cursors_Container.Controls.Add(this.Person);
            this.Cursors_Container.Controls.Add(this.IBeam);
            this.Cursors_Container.Controls.Add(this.Cross);
            this.Cursors_Container.Dock = System.Windows.Forms.DockStyle.Top;
            this.Cursors_Container.Location = new System.Drawing.Point(3, 3);
            this.Cursors_Container.Name = "Cursors_Container";
            this.Cursors_Container.Padding = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Cursors_Container.Size = new System.Drawing.Size(522, 256);
            this.Cursors_Container.TabIndex = 67;
            // 
            // Arrow
            // 
            this.Arrow.Location = new System.Drawing.Point(7, 7);
            this.Arrow.Name = "Arrow";
            this.Arrow.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Arrow.Prop_BorderThickness = 1F;
            this.Arrow.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Arrow.Prop_Cursor = WinPaletter.Paths.CursorType.Arrow;
            this.Arrow.Prop_File = "";
            this.Arrow.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Arrow.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Arrow.Prop_LoadingCircleBackGradient = false;
            this.Arrow.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Arrow.Prop_LoadingCircleBackNoise = false;
            this.Arrow.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Arrow.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Arrow.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Arrow.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Arrow.Prop_LoadingCircleHotGradient = false;
            this.Arrow.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Arrow.Prop_LoadingCircleHotNoise = false;
            this.Arrow.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Arrow.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Arrow.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Arrow.Prop_PrimaryColorGradient = false;
            this.Arrow.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Arrow.Prop_PrimaryNoise = false;
            this.Arrow.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Arrow.Prop_Scale = 1F;
            this.Arrow.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Arrow.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Arrow.Prop_SecondaryColorGradient = false;
            this.Arrow.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Arrow.Prop_SecondaryNoise = false;
            this.Arrow.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Arrow.Prop_Shadow_Blur = 5;
            this.Arrow.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Arrow.Prop_Shadow_Enabled = false;
            this.Arrow.Prop_Shadow_OffsetX = 2;
            this.Arrow.Prop_Shadow_OffsetY = 2;
            this.Arrow.Prop_Shadow_Opacity = 0.3F;
            this.Arrow.Prop_UseFromFile = false;
            this.Arrow.Size = new System.Drawing.Size(64, 64);
            this.Arrow.TabIndex = 5;
            // 
            // Help
            // 
            this.Help.Location = new System.Drawing.Point(77, 7);
            this.Help.Name = "Help";
            this.Help.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Help.Prop_BorderThickness = 1F;
            this.Help.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Help.Prop_Cursor = WinPaletter.Paths.CursorType.Help;
            this.Help.Prop_File = "";
            this.Help.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Help.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Help.Prop_LoadingCircleBackGradient = false;
            this.Help.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Help.Prop_LoadingCircleBackNoise = false;
            this.Help.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Help.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Help.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Help.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Help.Prop_LoadingCircleHotGradient = false;
            this.Help.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Help.Prop_LoadingCircleHotNoise = false;
            this.Help.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Help.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Help.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Help.Prop_PrimaryColorGradient = false;
            this.Help.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Help.Prop_PrimaryNoise = false;
            this.Help.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Help.Prop_Scale = 1F;
            this.Help.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Help.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Help.Prop_SecondaryColorGradient = false;
            this.Help.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Help.Prop_SecondaryNoise = false;
            this.Help.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Help.Prop_Shadow_Blur = 5;
            this.Help.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Help.Prop_Shadow_Enabled = false;
            this.Help.Prop_Shadow_OffsetX = 2;
            this.Help.Prop_Shadow_OffsetY = 2;
            this.Help.Prop_Shadow_Opacity = 0.3F;
            this.Help.Prop_UseFromFile = false;
            this.Help.Size = new System.Drawing.Size(64, 64);
            this.Help.TabIndex = 6;
            // 
            // AppLoading
            // 
            this.AppLoading.Location = new System.Drawing.Point(147, 7);
            this.AppLoading.Name = "AppLoading";
            this.AppLoading.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.AppLoading.Prop_BorderThickness = 1F;
            this.AppLoading.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.AppLoading.Prop_Cursor = WinPaletter.Paths.CursorType.AppLoading;
            this.AppLoading.Prop_File = "";
            this.AppLoading.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.AppLoading.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.AppLoading.Prop_LoadingCircleBackGradient = false;
            this.AppLoading.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.AppLoading.Prop_LoadingCircleBackNoise = false;
            this.AppLoading.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.AppLoading.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.AppLoading.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.AppLoading.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.AppLoading.Prop_LoadingCircleHotGradient = false;
            this.AppLoading.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.AppLoading.Prop_LoadingCircleHotNoise = false;
            this.AppLoading.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.AppLoading.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.AppLoading.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.AppLoading.Prop_PrimaryColorGradient = false;
            this.AppLoading.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.AppLoading.Prop_PrimaryNoise = false;
            this.AppLoading.Prop_PrimaryNoiseOpacity = 0.25F;
            this.AppLoading.Prop_Scale = 1F;
            this.AppLoading.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.AppLoading.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.AppLoading.Prop_SecondaryColorGradient = false;
            this.AppLoading.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.AppLoading.Prop_SecondaryNoise = false;
            this.AppLoading.Prop_SecondaryNoiseOpacity = 0.25F;
            this.AppLoading.Prop_Shadow_Blur = 5;
            this.AppLoading.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.AppLoading.Prop_Shadow_Enabled = false;
            this.AppLoading.Prop_Shadow_OffsetX = 2;
            this.AppLoading.Prop_Shadow_OffsetY = 2;
            this.AppLoading.Prop_Shadow_Opacity = 0.3F;
            this.AppLoading.Prop_UseFromFile = false;
            this.AppLoading.Size = new System.Drawing.Size(64, 64);
            this.AppLoading.TabIndex = 6;
            // 
            // Busy
            // 
            this.Busy.Location = new System.Drawing.Point(217, 7);
            this.Busy.Name = "Busy";
            this.Busy.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Busy.Prop_BorderThickness = 1F;
            this.Busy.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Busy.Prop_Cursor = WinPaletter.Paths.CursorType.Busy;
            this.Busy.Prop_File = "";
            this.Busy.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Busy.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Busy.Prop_LoadingCircleBackGradient = false;
            this.Busy.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.Busy.Prop_LoadingCircleBackNoise = false;
            this.Busy.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Busy.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Busy.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Busy.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Busy.Prop_LoadingCircleHotGradient = false;
            this.Busy.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.Busy.Prop_LoadingCircleHotNoise = false;
            this.Busy.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Busy.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Busy.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Busy.Prop_PrimaryColorGradient = false;
            this.Busy.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.Busy.Prop_PrimaryNoise = false;
            this.Busy.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Busy.Prop_Scale = 1F;
            this.Busy.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Busy.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Busy.Prop_SecondaryColorGradient = false;
            this.Busy.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.Busy.Prop_SecondaryNoise = false;
            this.Busy.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Busy.Prop_Shadow_Blur = 5;
            this.Busy.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Busy.Prop_Shadow_Enabled = false;
            this.Busy.Prop_Shadow_OffsetX = 2;
            this.Busy.Prop_Shadow_OffsetY = 2;
            this.Busy.Prop_Shadow_Opacity = 0.3F;
            this.Busy.Prop_UseFromFile = false;
            this.Busy.Size = new System.Drawing.Size(64, 64);
            this.Busy.TabIndex = 7;
            // 
            // Move_Cur
            // 
            this.Move_Cur.Location = new System.Drawing.Point(287, 7);
            this.Move_Cur.Name = "Move_Cur";
            this.Move_Cur.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Move_Cur.Prop_BorderThickness = 1F;
            this.Move_Cur.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Move_Cur.Prop_Cursor = WinPaletter.Paths.CursorType.Move;
            this.Move_Cur.Prop_File = "";
            this.Move_Cur.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Move_Cur.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Move_Cur.Prop_LoadingCircleBackGradient = false;
            this.Move_Cur.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Move_Cur.Prop_LoadingCircleBackNoise = false;
            this.Move_Cur.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Move_Cur.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Move_Cur.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Move_Cur.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Move_Cur.Prop_LoadingCircleHotGradient = false;
            this.Move_Cur.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Move_Cur.Prop_LoadingCircleHotNoise = false;
            this.Move_Cur.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Move_Cur.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Move_Cur.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Move_Cur.Prop_PrimaryColorGradient = false;
            this.Move_Cur.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Move_Cur.Prop_PrimaryNoise = false;
            this.Move_Cur.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Move_Cur.Prop_Scale = 1F;
            this.Move_Cur.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Move_Cur.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Move_Cur.Prop_SecondaryColorGradient = false;
            this.Move_Cur.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Move_Cur.Prop_SecondaryNoise = false;
            this.Move_Cur.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Move_Cur.Prop_Shadow_Blur = 5;
            this.Move_Cur.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Move_Cur.Prop_Shadow_Enabled = false;
            this.Move_Cur.Prop_Shadow_OffsetX = 2;
            this.Move_Cur.Prop_Shadow_OffsetY = 2;
            this.Move_Cur.Prop_Shadow_Opacity = 0.3F;
            this.Move_Cur.Prop_UseFromFile = false;
            this.Move_Cur.Size = new System.Drawing.Size(64, 64);
            this.Move_Cur.TabIndex = 8;
            // 
            // Up
            // 
            this.Up.Location = new System.Drawing.Point(357, 7);
            this.Up.Name = "Up";
            this.Up.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Up.Prop_BorderThickness = 1F;
            this.Up.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Up.Prop_Cursor = WinPaletter.Paths.CursorType.Up;
            this.Up.Prop_File = "";
            this.Up.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Up.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Up.Prop_LoadingCircleBackGradient = false;
            this.Up.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Up.Prop_LoadingCircleBackNoise = false;
            this.Up.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Up.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Up.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Up.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Up.Prop_LoadingCircleHotGradient = false;
            this.Up.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Up.Prop_LoadingCircleHotNoise = false;
            this.Up.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Up.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Up.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Up.Prop_PrimaryColorGradient = false;
            this.Up.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Up.Prop_PrimaryNoise = false;
            this.Up.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Up.Prop_Scale = 1F;
            this.Up.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Up.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Up.Prop_SecondaryColorGradient = false;
            this.Up.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Up.Prop_SecondaryNoise = false;
            this.Up.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Up.Prop_Shadow_Blur = 5;
            this.Up.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Up.Prop_Shadow_Enabled = false;
            this.Up.Prop_Shadow_OffsetX = 2;
            this.Up.Prop_Shadow_OffsetY = 2;
            this.Up.Prop_Shadow_Opacity = 0.3F;
            this.Up.Prop_UseFromFile = false;
            this.Up.Size = new System.Drawing.Size(64, 64);
            this.Up.TabIndex = 9;
            // 
            // NS
            // 
            this.NS.Location = new System.Drawing.Point(427, 7);
            this.NS.Name = "NS";
            this.NS.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.NS.Prop_BorderThickness = 1F;
            this.NS.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.NS.Prop_Cursor = WinPaletter.Paths.CursorType.NS;
            this.NS.Prop_File = "";
            this.NS.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NS.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NS.Prop_LoadingCircleBackGradient = false;
            this.NS.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NS.Prop_LoadingCircleBackNoise = false;
            this.NS.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.NS.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.NS.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NS.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NS.Prop_LoadingCircleHotGradient = false;
            this.NS.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NS.Prop_LoadingCircleHotNoise = false;
            this.NS.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.NS.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.NS.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.NS.Prop_PrimaryColorGradient = false;
            this.NS.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NS.Prop_PrimaryNoise = false;
            this.NS.Prop_PrimaryNoiseOpacity = 0.25F;
            this.NS.Prop_Scale = 1F;
            this.NS.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.NS.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.NS.Prop_SecondaryColorGradient = false;
            this.NS.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NS.Prop_SecondaryNoise = false;
            this.NS.Prop_SecondaryNoiseOpacity = 0.25F;
            this.NS.Prop_Shadow_Blur = 5;
            this.NS.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.NS.Prop_Shadow_Enabled = false;
            this.NS.Prop_Shadow_OffsetX = 2;
            this.NS.Prop_Shadow_OffsetY = 2;
            this.NS.Prop_Shadow_Opacity = 0.3F;
            this.NS.Prop_UseFromFile = false;
            this.NS.Size = new System.Drawing.Size(64, 64);
            this.NS.TabIndex = 10;
            // 
            // EW
            // 
            this.EW.Location = new System.Drawing.Point(7, 77);
            this.EW.Name = "EW";
            this.EW.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.EW.Prop_BorderThickness = 1F;
            this.EW.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.EW.Prop_Cursor = WinPaletter.Paths.CursorType.EW;
            this.EW.Prop_File = "";
            this.EW.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.EW.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.EW.Prop_LoadingCircleBackGradient = false;
            this.EW.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.EW.Prop_LoadingCircleBackNoise = false;
            this.EW.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.EW.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.EW.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.EW.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.EW.Prop_LoadingCircleHotGradient = false;
            this.EW.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.EW.Prop_LoadingCircleHotNoise = false;
            this.EW.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.EW.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.EW.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.EW.Prop_PrimaryColorGradient = false;
            this.EW.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.EW.Prop_PrimaryNoise = false;
            this.EW.Prop_PrimaryNoiseOpacity = 0.25F;
            this.EW.Prop_Scale = 1F;
            this.EW.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.EW.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.EW.Prop_SecondaryColorGradient = false;
            this.EW.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.EW.Prop_SecondaryNoise = false;
            this.EW.Prop_SecondaryNoiseOpacity = 0.25F;
            this.EW.Prop_Shadow_Blur = 5;
            this.EW.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.EW.Prop_Shadow_Enabled = false;
            this.EW.Prop_Shadow_OffsetX = 2;
            this.EW.Prop_Shadow_OffsetY = 2;
            this.EW.Prop_Shadow_Opacity = 0.3F;
            this.EW.Prop_UseFromFile = false;
            this.EW.Size = new System.Drawing.Size(64, 64);
            this.EW.TabIndex = 11;
            // 
            // NESW
            // 
            this.NESW.Location = new System.Drawing.Point(77, 77);
            this.NESW.Name = "NESW";
            this.NESW.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.NESW.Prop_BorderThickness = 1F;
            this.NESW.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.NESW.Prop_Cursor = WinPaletter.Paths.CursorType.NESW;
            this.NESW.Prop_File = "";
            this.NESW.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NESW.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NESW.Prop_LoadingCircleBackGradient = false;
            this.NESW.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NESW.Prop_LoadingCircleBackNoise = false;
            this.NESW.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.NESW.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.NESW.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NESW.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NESW.Prop_LoadingCircleHotGradient = false;
            this.NESW.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NESW.Prop_LoadingCircleHotNoise = false;
            this.NESW.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.NESW.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.NESW.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.NESW.Prop_PrimaryColorGradient = false;
            this.NESW.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NESW.Prop_PrimaryNoise = false;
            this.NESW.Prop_PrimaryNoiseOpacity = 0.25F;
            this.NESW.Prop_Scale = 1F;
            this.NESW.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.NESW.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.NESW.Prop_SecondaryColorGradient = false;
            this.NESW.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NESW.Prop_SecondaryNoise = false;
            this.NESW.Prop_SecondaryNoiseOpacity = 0.25F;
            this.NESW.Prop_Shadow_Blur = 5;
            this.NESW.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.NESW.Prop_Shadow_Enabled = false;
            this.NESW.Prop_Shadow_OffsetX = 2;
            this.NESW.Prop_Shadow_OffsetY = 2;
            this.NESW.Prop_Shadow_Opacity = 0.3F;
            this.NESW.Prop_UseFromFile = false;
            this.NESW.Size = new System.Drawing.Size(64, 64);
            this.NESW.TabIndex = 12;
            // 
            // NWSE
            // 
            this.NWSE.Location = new System.Drawing.Point(147, 77);
            this.NWSE.Name = "NWSE";
            this.NWSE.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.NWSE.Prop_BorderThickness = 1F;
            this.NWSE.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.NWSE.Prop_Cursor = WinPaletter.Paths.CursorType.NWSE;
            this.NWSE.Prop_File = "";
            this.NWSE.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NWSE.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NWSE.Prop_LoadingCircleBackGradient = false;
            this.NWSE.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NWSE.Prop_LoadingCircleBackNoise = false;
            this.NWSE.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.NWSE.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.NWSE.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NWSE.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NWSE.Prop_LoadingCircleHotGradient = false;
            this.NWSE.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NWSE.Prop_LoadingCircleHotNoise = false;
            this.NWSE.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.NWSE.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.NWSE.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.NWSE.Prop_PrimaryColorGradient = false;
            this.NWSE.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NWSE.Prop_PrimaryNoise = false;
            this.NWSE.Prop_PrimaryNoiseOpacity = 0.25F;
            this.NWSE.Prop_Scale = 1F;
            this.NWSE.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.NWSE.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.NWSE.Prop_SecondaryColorGradient = false;
            this.NWSE.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NWSE.Prop_SecondaryNoise = false;
            this.NWSE.Prop_SecondaryNoiseOpacity = 0.25F;
            this.NWSE.Prop_Shadow_Blur = 5;
            this.NWSE.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.NWSE.Prop_Shadow_Enabled = false;
            this.NWSE.Prop_Shadow_OffsetX = 2;
            this.NWSE.Prop_Shadow_OffsetY = 2;
            this.NWSE.Prop_Shadow_Opacity = 0.3F;
            this.NWSE.Prop_UseFromFile = false;
            this.NWSE.Size = new System.Drawing.Size(64, 64);
            this.NWSE.TabIndex = 13;
            // 
            // Pen
            // 
            this.Pen.Location = new System.Drawing.Point(217, 77);
            this.Pen.Name = "Pen";
            this.Pen.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Pen.Prop_BorderThickness = 1F;
            this.Pen.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Pen.Prop_Cursor = WinPaletter.Paths.CursorType.Pen;
            this.Pen.Prop_File = "";
            this.Pen.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pen.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pen.Prop_LoadingCircleBackGradient = false;
            this.Pen.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pen.Prop_LoadingCircleBackNoise = false;
            this.Pen.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Pen.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Pen.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Pen.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Pen.Prop_LoadingCircleHotGradient = false;
            this.Pen.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pen.Prop_LoadingCircleHotNoise = false;
            this.Pen.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Pen.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Pen.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Pen.Prop_PrimaryColorGradient = false;
            this.Pen.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pen.Prop_PrimaryNoise = false;
            this.Pen.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Pen.Prop_Scale = 1F;
            this.Pen.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Pen.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Pen.Prop_SecondaryColorGradient = false;
            this.Pen.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pen.Prop_SecondaryNoise = false;
            this.Pen.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Pen.Prop_Shadow_Blur = 5;
            this.Pen.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Pen.Prop_Shadow_Enabled = false;
            this.Pen.Prop_Shadow_OffsetX = 2;
            this.Pen.Prop_Shadow_OffsetY = 2;
            this.Pen.Prop_Shadow_Opacity = 0.3F;
            this.Pen.Prop_UseFromFile = false;
            this.Pen.Size = new System.Drawing.Size(64, 64);
            this.Pen.TabIndex = 14;
            // 
            // None
            // 
            this.None.Location = new System.Drawing.Point(287, 77);
            this.None.Name = "None";
            this.None.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.None.Prop_BorderThickness = 1F;
            this.None.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.None.Prop_Cursor = WinPaletter.Paths.CursorType.None;
            this.None.Prop_File = "";
            this.None.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.None.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.None.Prop_LoadingCircleBackGradient = false;
            this.None.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.None.Prop_LoadingCircleBackNoise = false;
            this.None.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.None.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.None.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.None.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.None.Prop_LoadingCircleHotGradient = false;
            this.None.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.None.Prop_LoadingCircleHotNoise = false;
            this.None.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.None.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.None.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.None.Prop_PrimaryColorGradient = false;
            this.None.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.None.Prop_PrimaryNoise = false;
            this.None.Prop_PrimaryNoiseOpacity = 0.25F;
            this.None.Prop_Scale = 1F;
            this.None.Prop_SecondaryColor1 = System.Drawing.Color.Red;
            this.None.Prop_SecondaryColor2 = System.Drawing.Color.Red;
            this.None.Prop_SecondaryColorGradient = false;
            this.None.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.None.Prop_SecondaryNoise = false;
            this.None.Prop_SecondaryNoiseOpacity = 0.25F;
            this.None.Prop_Shadow_Blur = 5;
            this.None.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.None.Prop_Shadow_Enabled = false;
            this.None.Prop_Shadow_OffsetX = 2;
            this.None.Prop_Shadow_OffsetY = 2;
            this.None.Prop_Shadow_Opacity = 0.3F;
            this.None.Prop_UseFromFile = false;
            this.None.Size = new System.Drawing.Size(64, 64);
            this.None.TabIndex = 15;
            // 
            // Link
            // 
            this.Link.Location = new System.Drawing.Point(357, 77);
            this.Link.Name = "Link";
            this.Link.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Link.Prop_BorderThickness = 1F;
            this.Link.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Link.Prop_Cursor = WinPaletter.Paths.CursorType.Link;
            this.Link.Prop_File = "";
            this.Link.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Link.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Link.Prop_LoadingCircleBackGradient = false;
            this.Link.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Link.Prop_LoadingCircleBackNoise = false;
            this.Link.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Link.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Link.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Link.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Link.Prop_LoadingCircleHotGradient = false;
            this.Link.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Link.Prop_LoadingCircleHotNoise = false;
            this.Link.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Link.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Link.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Link.Prop_PrimaryColorGradient = false;
            this.Link.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Link.Prop_PrimaryNoise = false;
            this.Link.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Link.Prop_Scale = 1F;
            this.Link.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Link.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Link.Prop_SecondaryColorGradient = false;
            this.Link.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Link.Prop_SecondaryNoise = false;
            this.Link.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Link.Prop_Shadow_Blur = 5;
            this.Link.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Link.Prop_Shadow_Enabled = false;
            this.Link.Prop_Shadow_OffsetX = 2;
            this.Link.Prop_Shadow_OffsetY = 2;
            this.Link.Prop_Shadow_Opacity = 0.3F;
            this.Link.Prop_UseFromFile = false;
            this.Link.Size = new System.Drawing.Size(64, 64);
            this.Link.TabIndex = 16;
            // 
            // Pin
            // 
            this.Pin.Location = new System.Drawing.Point(427, 77);
            this.Pin.Name = "Pin";
            this.Pin.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Pin.Prop_BorderThickness = 1F;
            this.Pin.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Pin.Prop_Cursor = WinPaletter.Paths.CursorType.Pin;
            this.Pin.Prop_File = "";
            this.Pin.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pin.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pin.Prop_LoadingCircleBackGradient = false;
            this.Pin.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pin.Prop_LoadingCircleBackNoise = false;
            this.Pin.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Pin.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Pin.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Pin.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Pin.Prop_LoadingCircleHotGradient = false;
            this.Pin.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pin.Prop_LoadingCircleHotNoise = false;
            this.Pin.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Pin.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Pin.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Pin.Prop_PrimaryColorGradient = false;
            this.Pin.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pin.Prop_PrimaryNoise = false;
            this.Pin.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Pin.Prop_Scale = 1F;
            this.Pin.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Pin.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Pin.Prop_SecondaryColorGradient = false;
            this.Pin.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pin.Prop_SecondaryNoise = false;
            this.Pin.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Pin.Prop_Shadow_Blur = 5;
            this.Pin.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Pin.Prop_Shadow_Enabled = false;
            this.Pin.Prop_Shadow_OffsetX = 2;
            this.Pin.Prop_Shadow_OffsetY = 2;
            this.Pin.Prop_Shadow_Opacity = 0.3F;
            this.Pin.Prop_UseFromFile = false;
            this.Pin.Size = new System.Drawing.Size(64, 64);
            this.Pin.TabIndex = 17;
            // 
            // Person
            // 
            this.Person.Location = new System.Drawing.Point(7, 147);
            this.Person.Name = "Person";
            this.Person.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Person.Prop_BorderThickness = 1F;
            this.Person.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Person.Prop_Cursor = WinPaletter.Paths.CursorType.Person;
            this.Person.Prop_File = "";
            this.Person.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Person.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Person.Prop_LoadingCircleBackGradient = false;
            this.Person.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Person.Prop_LoadingCircleBackNoise = false;
            this.Person.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Person.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Person.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Person.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Person.Prop_LoadingCircleHotGradient = false;
            this.Person.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Person.Prop_LoadingCircleHotNoise = false;
            this.Person.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Person.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Person.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Person.Prop_PrimaryColorGradient = false;
            this.Person.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Person.Prop_PrimaryNoise = false;
            this.Person.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Person.Prop_Scale = 1F;
            this.Person.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Person.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Person.Prop_SecondaryColorGradient = false;
            this.Person.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Person.Prop_SecondaryNoise = false;
            this.Person.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Person.Prop_Shadow_Blur = 5;
            this.Person.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Person.Prop_Shadow_Enabled = false;
            this.Person.Prop_Shadow_OffsetX = 2;
            this.Person.Prop_Shadow_OffsetY = 2;
            this.Person.Prop_Shadow_Opacity = 0.3F;
            this.Person.Prop_UseFromFile = false;
            this.Person.Size = new System.Drawing.Size(64, 64);
            this.Person.TabIndex = 18;
            // 
            // IBeam
            // 
            this.IBeam.Location = new System.Drawing.Point(77, 147);
            this.IBeam.Name = "IBeam";
            this.IBeam.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.IBeam.Prop_BorderThickness = 1F;
            this.IBeam.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.IBeam.Prop_Cursor = WinPaletter.Paths.CursorType.IBeam;
            this.IBeam.Prop_File = "";
            this.IBeam.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.IBeam.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.IBeam.Prop_LoadingCircleBackGradient = false;
            this.IBeam.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.IBeam.Prop_LoadingCircleBackNoise = false;
            this.IBeam.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.IBeam.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.IBeam.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.IBeam.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.IBeam.Prop_LoadingCircleHotGradient = false;
            this.IBeam.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.IBeam.Prop_LoadingCircleHotNoise = false;
            this.IBeam.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.IBeam.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.IBeam.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.IBeam.Prop_PrimaryColorGradient = false;
            this.IBeam.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.IBeam.Prop_PrimaryNoise = false;
            this.IBeam.Prop_PrimaryNoiseOpacity = 0.25F;
            this.IBeam.Prop_Scale = 1F;
            this.IBeam.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.IBeam.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.IBeam.Prop_SecondaryColorGradient = false;
            this.IBeam.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.IBeam.Prop_SecondaryNoise = false;
            this.IBeam.Prop_SecondaryNoiseOpacity = 0.25F;
            this.IBeam.Prop_Shadow_Blur = 5;
            this.IBeam.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.IBeam.Prop_Shadow_Enabled = false;
            this.IBeam.Prop_Shadow_OffsetX = 2;
            this.IBeam.Prop_Shadow_OffsetY = 2;
            this.IBeam.Prop_Shadow_Opacity = 0.3F;
            this.IBeam.Prop_UseFromFile = false;
            this.IBeam.Size = new System.Drawing.Size(64, 64);
            this.IBeam.TabIndex = 19;
            // 
            // Cross
            // 
            this.Cross.Location = new System.Drawing.Point(147, 147);
            this.Cross.Name = "Cross";
            this.Cross.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Cross.Prop_BorderThickness = 1F;
            this.Cross.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Cross.Prop_Cursor = WinPaletter.Paths.CursorType.Cross;
            this.Cross.Prop_File = "";
            this.Cross.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Cross.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Cross.Prop_LoadingCircleBackGradient = false;
            this.Cross.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Cross.Prop_LoadingCircleBackNoise = false;
            this.Cross.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
            this.Cross.Prop_LoadingCircleHot_AnimationSpeed = 10;
            this.Cross.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Cross.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Cross.Prop_LoadingCircleHotGradient = false;
            this.Cross.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Cross.Prop_LoadingCircleHotNoise = false;
            this.Cross.Prop_LoadingCircleHotNoiseOpacity = 0.25F;
            this.Cross.Prop_PrimaryColor1 = System.Drawing.Color.White;
            this.Cross.Prop_PrimaryColor2 = System.Drawing.Color.White;
            this.Cross.Prop_PrimaryColorGradient = false;
            this.Cross.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Cross.Prop_PrimaryNoise = false;
            this.Cross.Prop_PrimaryNoiseOpacity = 0.25F;
            this.Cross.Prop_Scale = 1F;
            this.Cross.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Cross.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.Cross.Prop_SecondaryColorGradient = false;
            this.Cross.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Cross.Prop_SecondaryNoise = false;
            this.Cross.Prop_SecondaryNoiseOpacity = 0.25F;
            this.Cross.Prop_Shadow_Blur = 5;
            this.Cross.Prop_Shadow_Color = System.Drawing.Color.Black;
            this.Cross.Prop_Shadow_Enabled = false;
            this.Cross.Prop_Shadow_OffsetX = 2;
            this.Cross.Prop_Shadow_OffsetY = 2;
            this.Cross.Prop_Shadow_Opacity = 0.3F;
            this.Cross.Prop_UseFromFile = false;
            this.Cross.Size = new System.Drawing.Size(64, 64);
            this.Cross.TabIndex = 20;
            // 
            // cur_anim_btn
            // 
            this.cur_anim_btn.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.cur_anim_btn.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.cur_anim_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cur_anim_btn.ForeColor = System.Drawing.Color.White;
            this.cur_anim_btn.Image = null;
            this.cur_anim_btn.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("cur_anim_btn.ImageGlyph")));
            this.cur_anim_btn.ImageGlyphEnabled = true;
            this.cur_anim_btn.Location = new System.Drawing.Point(369, 265);
            this.cur_anim_btn.Name = "cur_anim_btn";
            this.cur_anim_btn.Size = new System.Drawing.Size(101, 24);
            this.cur_anim_btn.TabIndex = 72;
            this.cur_anim_btn.Text = "Animate";
            this.cur_anim_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cur_anim_btn.UseVisualStyleBackColor = false;
            this.cur_anim_btn.Click += new System.EventHandler(this.Cur_anim_btn_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage5.Controls.Add(this.search_results);
            this.TabPage5.Location = new System.Drawing.Point(4, 24);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(10);
            this.TabPage5.Size = new System.Drawing.Size(1321, 641);
            this.TabPage5.TabIndex = 3;
            this.TabPage5.Text = "2";
            // 
            // search_results
            // 
            this.search_results.AutoScroll = true;
            this.search_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.search_results.Location = new System.Drawing.Point(10, 10);
            this.search_results.Name = "search_results";
            this.search_results.Size = new System.Drawing.Size(1301, 621);
            this.search_results.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage2.Controls.Add(this.banner4);
            this.tabPage2.Controls.Add(this.groupBox11);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.banner3);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.banner2);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.banner1);
            this.tabPage2.Controls.Add(this.groupBox10);
            this.tabPage2.Controls.Add(this.banner7);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1321, 641);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "3";
            // 
            // banner4
            // 
            this.banner4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.banner4.BackColor = System.Drawing.Color.Transparent;
            this.banner4.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner4.Image = ((System.Drawing.Bitmap)(resources.GetObject("banner4.Image")));
            this.banner4.Location = new System.Drawing.Point(10, 10);
            this.banner4.Name = "banner4";
            this.banner4.Size = new System.Drawing.Size(1301, 68);
            this.banner4.TabIndex = 215;
            this.banner4.TabStop = false;
            this.banner4.Text = "WinPaletter was unable to connect to the servers and failed to fetch themes.";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.Transparent;
            this.groupBox11.Controls.Add(this.button2);
            this.groupBox11.Controls.Add(this.button3);
            this.groupBox11.Controls.Add(this.button4);
            this.groupBox11.Controls.Add(this.button5);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox11.Location = new System.Drawing.Point(3, 586);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(1315, 52);
            this.groupBox11.TabIndex = 214;
            this.groupBox11.UseDecorationPattern = false;
            this.groupBox11.UseSharpStyle = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.CustomColor = System.Drawing.Color.Empty;
            this.button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageGlyph = null;
            this.button2.ImageGlyphEnabled = false;
            this.button2.Location = new System.Drawing.Point(1028, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(282, 42);
            this.button2.TabIndex = 11;
            this.button2.Text = "Troubleshoot network connection";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.CustomColor = System.Drawing.Color.Empty;
            this.button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageGlyph = null;
            this.button3.ImageGlyphEnabled = false;
            this.button3.Location = new System.Drawing.Point(740, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(282, 42);
            this.button3.TabIndex = 12;
            this.button3.Text = "Load themes from the downloaded cache";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.CustomColor = System.Drawing.Color.Empty;
            this.button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageGlyph = null;
            this.button4.ImageGlyphEnabled = false;
            this.button4.Location = new System.Drawing.Point(452, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(282, 42);
            this.button4.TabIndex = 13;
            this.button4.Text = "Offline store mode (list themes in a folder)";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.CustomColor = System.Drawing.Color.Empty;
            this.button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageGlyph = null;
            this.button5.ImageGlyphEnabled = false;
            this.button5.Location = new System.Drawing.Point(407, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(39, 42);
            this.button5.TabIndex = 15;
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox7.BackColor = System.Drawing.Color.Transparent;
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Location = new System.Drawing.Point(368, 383);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(626, 61);
            this.groupBox7.TabIndex = 178;
            this.groupBox7.UseDecorationPattern = false;
            this.groupBox7.UseSharpStyle = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(606, 22);
            this.label7.TabIndex = 150;
            this.label7.Text = "Check Network Settings";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(597, 18);
            this.label8.TabIndex = 151;
            this.label8.Text = "- Go to Settings → Network && Internet → Status, or click \'Troubleshoot network c" +
    "onnection\'.";
            // 
            // banner3
            // 
            this.banner3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.banner3.BackColor = System.Drawing.Color.Transparent;
            this.banner3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner3.Image = null;
            this.banner3.Location = new System.Drawing.Point(327, 383);
            this.banner3.Name = "banner3";
            this.banner3.Size = new System.Drawing.Size(35, 61);
            this.banner3.TabIndex = 177;
            this.banner3.TabStop = false;
            this.banner3.Text = "4";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox8.BackColor = System.Drawing.Color.Transparent;
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Location = new System.Drawing.Point(368, 316);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(626, 61);
            this.groupBox8.TabIndex = 176;
            this.groupBox8.UseDecorationPattern = false;
            this.groupBox8.UseSharpStyle = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(606, 22);
            this.label5.TabIndex = 150;
            this.label5.Text = "Check for service outages";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(597, 18);
            this.label6.TabIndex = 151;
            this.label6.Text = "- Check if GitHub is accessible, visit your ISP’s website (using another device i" +
    "f needed), or contact their support.";
            // 
            // banner2
            // 
            this.banner2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.banner2.BackColor = System.Drawing.Color.Transparent;
            this.banner2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner2.Image = null;
            this.banner2.Location = new System.Drawing.Point(327, 316);
            this.banner2.Name = "banner2";
            this.banner2.Size = new System.Drawing.Size(35, 61);
            this.banner2.TabIndex = 175;
            this.banner2.TabStop = false;
            this.banner2.Text = "3";
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox9.BackColor = System.Drawing.Color.Transparent;
            this.groupBox9.Controls.Add(this.label11);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Location = new System.Drawing.Point(368, 236);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(626, 74);
            this.groupBox9.TabIndex = 174;
            this.groupBox9.UseDecorationPattern = false;
            this.groupBox9.UseSharpStyle = false;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(606, 22);
            this.label11.TabIndex = 150;
            this.label11.Text = "Restart your router, modem, or internet connection";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(16, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(597, 31);
            this.label12.TabIndex = 151;
            this.label12.Text = "- Power off the router or modem, wait about 10 seconds, then turn it back on.\r\n- " +
    "For Wi-Fi, you can also try reconnecting to the network or switching to a differ" +
    "ent network.\r\n";
            // 
            // banner1
            // 
            this.banner1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.banner1.BackColor = System.Drawing.Color.Transparent;
            this.banner1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner1.Image = null;
            this.banner1.Location = new System.Drawing.Point(327, 236);
            this.banner1.Name = "banner1";
            this.banner1.Size = new System.Drawing.Size(35, 74);
            this.banner1.TabIndex = 173;
            this.banner1.TabStop = false;
            this.banner1.Text = "2";
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox10.BackColor = System.Drawing.Color.Transparent;
            this.groupBox10.Controls.Add(this.label13);
            this.groupBox10.Location = new System.Drawing.Point(368, 196);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(626, 34);
            this.groupBox10.TabIndex = 172;
            this.groupBox10.UseDecorationPattern = false;
            this.groupBox10.UseSharpStyle = false;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(607, 22);
            this.label13.TabIndex = 150;
            this.label13.Text = "Check your Ethernet or Wi-Fi connection";
            // 
            // banner7
            // 
            this.banner7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.banner7.BackColor = System.Drawing.Color.Transparent;
            this.banner7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner7.Image = null;
            this.banner7.Location = new System.Drawing.Point(327, 196);
            this.banner7.Name = "banner7";
            this.banner7.Size = new System.Drawing.Size(35, 34);
            this.banner7.TabIndex = 171;
            this.banner7.TabStop = false;
            this.banner7.Text = "1";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage4.Controls.Add(this.TextBox1);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.banner6);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1321, 641);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "4";
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(10, 86);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(1301, 495);
            this.TextBox1.TabIndex = 219;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 590);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1315, 48);
            this.groupBox1.TabIndex = 218;
            this.groupBox1.UseDecorationPattern = false;
            this.groupBox1.UseSharpStyle = false;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.CustomColor = System.Drawing.Color.Empty;
            this.button6.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = null;
            this.button6.ImageGlyph = null;
            this.button6.ImageGlyphEnabled = false;
            this.button6.Location = new System.Drawing.Point(1229, 7);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(80, 34);
            this.button6.TabIndex = 7;
            this.button6.Text = "Reject";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.CustomColor = System.Drawing.Color.Empty;
            this.button7.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = null;
            this.button7.ImageGlyph = null;
            this.button7.ImageGlyphEnabled = false;
            this.button7.Location = new System.Drawing.Point(1143, 7);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(80, 34);
            this.button7.TabIndex = 6;
            this.button7.Text = "Accept";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // banner6
            // 
            this.banner6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.banner6.BackColor = System.Drawing.Color.Transparent;
            this.banner6.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner6.Image = ((System.Drawing.Bitmap)(resources.GetObject("banner6.Image")));
            this.banner6.Location = new System.Drawing.Point(10, 10);
            this.banner6.Name = "banner6";
            this.banner6.Size = new System.Drawing.Size(1301, 68);
            this.banner6.TabIndex = 217;
            this.banner6.TabStop = false;
            this.banner6.Text = "Theme credits\\license";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage6.Controls.Add(this.banner5);
            this.tabPage6.Controls.Add(this.bottom_buttons);
            this.tabPage6.Controls.Add(this.smoothFlowLayoutPanel1);
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1321, 641);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "5";
            // 
            // banner5
            // 
            this.banner5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.banner5.BackColor = System.Drawing.Color.Transparent;
            this.banner5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner5.Image = ((System.Drawing.Bitmap)(resources.GetObject("banner5.Image")));
            this.banner5.Location = new System.Drawing.Point(10, 10);
            this.banner5.Name = "banner5";
            this.banner5.Size = new System.Drawing.Size(1301, 68);
            this.banner5.TabIndex = 216;
            this.banner5.TabStop = false;
            this.banner5.Text = "These aspects will be edited during theme apply process. To prevent unintended ac" +
    "tions, uncheck aspects you want not to be modified.";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.button1);
            this.bottom_buttons.Controls.Add(this.button8);
            this.bottom_buttons.Controls.Add(this.button9);
            this.bottom_buttons.Controls.Add(this.button10);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(3, 590);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(1315, 48);
            this.bottom_buttons.TabIndex = 213;
            this.bottom_buttons.UseDecorationPattern = false;
            this.bottom_buttons.UseSharpStyle = false;
            // 
            // button1
            // 
            this.button1.CustomColor = System.Drawing.Color.Empty;
            this.button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = null;
            this.button1.ImageGlyph = null;
            this.button1.ImageGlyphEnabled = false;
            this.button1.Location = new System.Drawing.Point(122, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 34);
            this.button1.TabIndex = 214;
            this.button1.Text = "Unselect all";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.CustomColor = System.Drawing.Color.Empty;
            this.button8.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Image = null;
            this.button8.ImageGlyph = null;
            this.button8.ImageGlyphEnabled = false;
            this.button8.Location = new System.Drawing.Point(991, 7);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(97, 34);
            this.button8.TabIndex = 208;
            this.button8.Text = "Cancel";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.CustomColor = System.Drawing.Color.Empty;
            this.button9.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = null;
            this.button9.ImageGlyph = null;
            this.button9.ImageGlyphEnabled = false;
            this.button9.Location = new System.Drawing.Point(8, 7);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(108, 34);
            this.button9.TabIndex = 213;
            this.button9.Text = "Select all";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.CustomColor = System.Drawing.Color.Empty;
            this.button10.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Image = null;
            this.button10.ImageGlyph = null;
            this.button10.ImageGlyphEnabled = false;
            this.button10.Location = new System.Drawing.Point(1094, 7);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(215, 34);
            this.button10.TabIndex = 1;
            this.button10.Text = "Proceed with current selections";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // smoothFlowLayoutPanel1
            // 
            this.smoothFlowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.smoothFlowLayoutPanel1.AutoScroll = true;
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_theme);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_classicColors);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_lockScreen);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_cursors);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_metrics);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_cmd);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_ps86);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_ps64);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_terminal);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_terminalPreview);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_wallpaper);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_effects);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_sounds);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_screenSaver);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_altTab);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_icons);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_accessibility);
            this.smoothFlowLayoutPanel1.Controls.Add(this.toggle_winPaletterTheme);
            this.smoothFlowLayoutPanel1.Location = new System.Drawing.Point(11, 84);
            this.smoothFlowLayoutPanel1.Name = "smoothFlowLayoutPanel1";
            this.smoothFlowLayoutPanel1.Size = new System.Drawing.Size(1300, 498);
            this.smoothFlowLayoutPanel1.TabIndex = 16;
            // 
            // toggle_theme
            // 
            this.toggle_theme.Checked = true;
            this.toggle_theme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_theme.ForeColor = System.Drawing.Color.White;
            this.toggle_theme.Image = null;
            this.toggle_theme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_theme.Location = new System.Drawing.Point(3, 3);
            this.toggle_theme.Name = "toggle_theme";
            this.toggle_theme.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_theme.Size = new System.Drawing.Size(370, 75);
            this.toggle_theme.TabIndex = 215;
            this.toggle_theme.Text = "Windows Theme\r\n- Control Windows\' look using accent colors and\r\nvisual styles";
            this.toggle_theme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_theme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_classicColors
            // 
            this.toggle_classicColors.Checked = true;
            this.toggle_classicColors.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_classicColors.ForeColor = System.Drawing.Color.White;
            this.toggle_classicColors.Image = ((System.Drawing.Image)(resources.GetObject("toggle_classicColors.Image")));
            this.toggle_classicColors.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_classicColors.Location = new System.Drawing.Point(379, 3);
            this.toggle_classicColors.Name = "toggle_classicColors";
            this.toggle_classicColors.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_classicColors.Size = new System.Drawing.Size(370, 75);
            this.toggle_classicColors.TabIndex = 216;
            this.toggle_classicColors.Text = "Classic Colors\r\n- Revamp the look of classic Windows elements,\r\nincluding 3D obje" +
    "cts and more";
            this.toggle_classicColors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_classicColors.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_lockScreen
            // 
            this.toggle_lockScreen.Checked = true;
            this.toggle_lockScreen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_lockScreen.ForeColor = System.Drawing.Color.White;
            this.toggle_lockScreen.Image = null;
            this.toggle_lockScreen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_lockScreen.Location = new System.Drawing.Point(755, 3);
            this.toggle_lockScreen.Name = "toggle_lockScreen";
            this.toggle_lockScreen.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_lockScreen.Size = new System.Drawing.Size(370, 75);
            this.toggle_lockScreen.TabIndex = 217;
            this.toggle_lockScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_lockScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_cursors
            // 
            this.toggle_cursors.Checked = true;
            this.toggle_cursors.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_cursors.ForeColor = System.Drawing.Color.White;
            this.toggle_cursors.Image = ((System.Drawing.Image)(resources.GetObject("toggle_cursors.Image")));
            this.toggle_cursors.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_cursors.Location = new System.Drawing.Point(3, 84);
            this.toggle_cursors.Name = "toggle_cursors";
            this.toggle_cursors.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_cursors.Size = new System.Drawing.Size(370, 75);
            this.toggle_cursors.TabIndex = 218;
            this.toggle_cursors.Text = "Cursors\r\n- Customize colors, styles, backgrounds, borders, and\r\nloading circles";
            this.toggle_cursors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_cursors.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_metrics
            // 
            this.toggle_metrics.Checked = true;
            this.toggle_metrics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_metrics.ForeColor = System.Drawing.Color.White;
            this.toggle_metrics.Image = ((System.Drawing.Image)(resources.GetObject("toggle_metrics.Image")));
            this.toggle_metrics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_metrics.Location = new System.Drawing.Point(379, 84);
            this.toggle_metrics.Name = "toggle_metrics";
            this.toggle_metrics.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_metrics.Size = new System.Drawing.Size(370, 75);
            this.toggle_metrics.TabIndex = 232;
            this.toggle_metrics.Text = "Metrics && Fonts\r\n- Adjust font styles, sizes, and layout metrics for\r\noptimal re" +
    "adability and aesthetics";
            this.toggle_metrics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_metrics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_cmd
            // 
            this.toggle_cmd.Checked = true;
            this.toggle_cmd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_cmd.ForeColor = System.Drawing.Color.White;
            this.toggle_cmd.Image = ((System.Drawing.Image)(resources.GetObject("toggle_cmd.Image")));
            this.toggle_cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_cmd.Location = new System.Drawing.Point(755, 84);
            this.toggle_cmd.Name = "toggle_cmd";
            this.toggle_cmd.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_cmd.Size = new System.Drawing.Size(370, 75);
            this.toggle_cmd.TabIndex = 219;
            this.toggle_cmd.Text = "Command Prompt\r\n- Personalize your terminal\'s colors and appearance";
            this.toggle_cmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_cmd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_ps86
            // 
            this.toggle_ps86.Checked = true;
            this.toggle_ps86.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_ps86.ForeColor = System.Drawing.Color.White;
            this.toggle_ps86.Image = ((System.Drawing.Image)(resources.GetObject("toggle_ps86.Image")));
            this.toggle_ps86.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_ps86.Location = new System.Drawing.Point(3, 165);
            this.toggle_ps86.Name = "toggle_ps86";
            this.toggle_ps86.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_ps86.Size = new System.Drawing.Size(370, 75);
            this.toggle_ps86.TabIndex = 220;
            this.toggle_ps86.Text = "PowerShell x86\r\n- Personalize your terminal\'s colors and appearance";
            this.toggle_ps86.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_ps86.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_ps64
            // 
            this.toggle_ps64.Checked = true;
            this.toggle_ps64.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_ps64.ForeColor = System.Drawing.Color.White;
            this.toggle_ps64.Image = ((System.Drawing.Image)(resources.GetObject("toggle_ps64.Image")));
            this.toggle_ps64.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_ps64.Location = new System.Drawing.Point(379, 165);
            this.toggle_ps64.Name = "toggle_ps64";
            this.toggle_ps64.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_ps64.Size = new System.Drawing.Size(370, 75);
            this.toggle_ps64.TabIndex = 221;
            this.toggle_ps64.Text = "PowerShell x64\r\n- Personalize your terminal\'s colors and appearance";
            this.toggle_ps64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_ps64.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_terminal
            // 
            this.toggle_terminal.Checked = true;
            this.toggle_terminal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_terminal.ForeColor = System.Drawing.Color.White;
            this.toggle_terminal.Image = ((System.Drawing.Image)(resources.GetObject("toggle_terminal.Image")));
            this.toggle_terminal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_terminal.Location = new System.Drawing.Point(755, 165);
            this.toggle_terminal.Name = "toggle_terminal";
            this.toggle_terminal.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_terminal.Size = new System.Drawing.Size(370, 75);
            this.toggle_terminal.TabIndex = 222;
            this.toggle_terminal.Text = "Microsoft Terminal\r\n- Personalize your terminal\'s colors and appearance";
            this.toggle_terminal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_terminal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_terminalPreview
            // 
            this.toggle_terminalPreview.Checked = true;
            this.toggle_terminalPreview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_terminalPreview.ForeColor = System.Drawing.Color.White;
            this.toggle_terminalPreview.Image = ((System.Drawing.Image)(resources.GetObject("toggle_terminalPreview.Image")));
            this.toggle_terminalPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_terminalPreview.Location = new System.Drawing.Point(3, 246);
            this.toggle_terminalPreview.Name = "toggle_terminalPreview";
            this.toggle_terminalPreview.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_terminalPreview.Size = new System.Drawing.Size(370, 75);
            this.toggle_terminalPreview.TabIndex = 223;
            this.toggle_terminalPreview.Text = "Microsoft Terminal Preview\r\n- Personalize your terminal\'s colors and appearance";
            this.toggle_terminalPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_terminalPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_wallpaper
            // 
            this.toggle_wallpaper.Checked = true;
            this.toggle_wallpaper.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_wallpaper.ForeColor = System.Drawing.Color.White;
            this.toggle_wallpaper.Image = ((System.Drawing.Image)(resources.GetObject("toggle_wallpaper.Image")));
            this.toggle_wallpaper.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_wallpaper.Location = new System.Drawing.Point(379, 246);
            this.toggle_wallpaper.Name = "toggle_wallpaper";
            this.toggle_wallpaper.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_wallpaper.Size = new System.Drawing.Size(370, 75);
            this.toggle_wallpaper.TabIndex = 224;
            this.toggle_wallpaper.Text = "Wallpaper\r\n- Revitalize your screen with an image";
            this.toggle_wallpaper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_wallpaper.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_effects
            // 
            this.toggle_effects.Checked = true;
            this.toggle_effects.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_effects.ForeColor = System.Drawing.Color.White;
            this.toggle_effects.Image = ((System.Drawing.Image)(resources.GetObject("toggle_effects.Image")));
            this.toggle_effects.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_effects.Location = new System.Drawing.Point(755, 246);
            this.toggle_effects.Name = "toggle_effects";
            this.toggle_effects.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_effects.Size = new System.Drawing.Size(370, 75);
            this.toggle_effects.TabIndex = 225;
            this.toggle_effects.Text = "Windows Effects\r\n- Change effects of your Windows, including\r\nanimations and shad" +
    "ows";
            this.toggle_effects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_effects.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_sounds
            // 
            this.toggle_sounds.Checked = true;
            this.toggle_sounds.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_sounds.ForeColor = System.Drawing.Color.White;
            this.toggle_sounds.Image = ((System.Drawing.Image)(resources.GetObject("toggle_sounds.Image")));
            this.toggle_sounds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_sounds.Location = new System.Drawing.Point(3, 327);
            this.toggle_sounds.Name = "toggle_sounds";
            this.toggle_sounds.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_sounds.Size = new System.Drawing.Size(370, 75);
            this.toggle_sounds.TabIndex = 226;
            this.toggle_sounds.Text = "Sounds\r\n- Immerse yourself in a customized auditory experience\r\nwith personalized" +
    " sounds";
            this.toggle_sounds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_sounds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_screenSaver
            // 
            this.toggle_screenSaver.Checked = true;
            this.toggle_screenSaver.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_screenSaver.ForeColor = System.Drawing.Color.White;
            this.toggle_screenSaver.Image = ((System.Drawing.Image)(resources.GetObject("toggle_screenSaver.Image")));
            this.toggle_screenSaver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_screenSaver.Location = new System.Drawing.Point(379, 327);
            this.toggle_screenSaver.Name = "toggle_screenSaver";
            this.toggle_screenSaver.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_screenSaver.Size = new System.Drawing.Size(370, 75);
            this.toggle_screenSaver.TabIndex = 227;
            this.toggle_screenSaver.Text = "Screen Saver\r\n- Use a personalized screensaver for the best\r\nCRT monitor experien" +
    "ce";
            this.toggle_screenSaver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_screenSaver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_altTab
            // 
            this.toggle_altTab.Checked = true;
            this.toggle_altTab.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_altTab.ForeColor = System.Drawing.Color.White;
            this.toggle_altTab.Image = ((System.Drawing.Image)(resources.GetObject("toggle_altTab.Image")));
            this.toggle_altTab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_altTab.Location = new System.Drawing.Point(755, 327);
            this.toggle_altTab.Name = "toggle_altTab";
            this.toggle_altTab.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_altTab.Size = new System.Drawing.Size(370, 75);
            this.toggle_altTab.TabIndex = 228;
            this.toggle_altTab.Text = "Windows Switcher\r\n- Modify preferences of Windows Switcher (Alt+Tab)";
            this.toggle_altTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_altTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_icons
            // 
            this.toggle_icons.Checked = true;
            this.toggle_icons.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_icons.ForeColor = System.Drawing.Color.White;
            this.toggle_icons.Image = ((System.Drawing.Image)(resources.GetObject("toggle_icons.Image")));
            this.toggle_icons.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_icons.Location = new System.Drawing.Point(3, 408);
            this.toggle_icons.Name = "toggle_icons";
            this.toggle_icons.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_icons.Size = new System.Drawing.Size(370, 75);
            this.toggle_icons.TabIndex = 229;
            this.toggle_icons.Text = "Windows Icons\r\n- Replace common Windows icons without patching\r\nsystem files";
            this.toggle_icons.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_icons.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_accessibility
            // 
            this.toggle_accessibility.Checked = true;
            this.toggle_accessibility.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_accessibility.ForeColor = System.Drawing.Color.White;
            this.toggle_accessibility.Image = ((System.Drawing.Image)(resources.GetObject("toggle_accessibility.Image")));
            this.toggle_accessibility.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_accessibility.Location = new System.Drawing.Point(379, 408);
            this.toggle_accessibility.Name = "toggle_accessibility";
            this.toggle_accessibility.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_accessibility.Size = new System.Drawing.Size(370, 75);
            this.toggle_accessibility.TabIndex = 230;
            this.toggle_accessibility.Text = "Accessibility\r\n- Enable High Contrast Mode and edit color filters";
            this.toggle_accessibility.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_accessibility.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // toggle_winPaletterTheme
            // 
            this.toggle_winPaletterTheme.Checked = true;
            this.toggle_winPaletterTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggle_winPaletterTheme.ForeColor = System.Drawing.Color.White;
            this.toggle_winPaletterTheme.Image = ((System.Drawing.Image)(resources.GetObject("toggle_winPaletterTheme.Image")));
            this.toggle_winPaletterTheme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toggle_winPaletterTheme.Location = new System.Drawing.Point(755, 408);
            this.toggle_winPaletterTheme.Name = "toggle_winPaletterTheme";
            this.toggle_winPaletterTheme.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toggle_winPaletterTheme.Size = new System.Drawing.Size(370, 75);
            this.toggle_winPaletterTheme.TabIndex = 231;
            this.toggle_winPaletterTheme.Text = "WinPaletter Application Theme\r\n- Modify the look of WinPaletter to make it suits " +
    "your\r\ntheme";
            this.toggle_winPaletterTheme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggle_winPaletterTheme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.titlebar_lbl);
            this.titlebarExtender1.Controls.Add(this.avatar_btn);
            this.titlebarExtender1.Controls.Add(this.flowLayoutPanel2);
            this.titlebarExtender1.Controls.Add(this.back_btn);
            this.titlebarExtender1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebarExtender1.Flag = WinPaletter.Tabs.TitlebarExtender.Flags.Tabs_Extended;
            this.titlebarExtender1.Location = new System.Drawing.Point(0, 0);
            this.titlebarExtender1.Name = "titlebarExtender1";
            this.titlebarExtender1.Size = new System.Drawing.Size(1329, 52);
            this.titlebarExtender1.TabIndex = 116;
            this.titlebarExtender1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // titlebar_lbl
            // 
            this.titlebar_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titlebar_lbl.BackColor = System.Drawing.Color.Transparent;
            this.titlebar_lbl.DrawOnGlass = false;
            this.titlebar_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titlebar_lbl.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titlebar_lbl.Location = new System.Drawing.Point(53, 8);
            this.titlebar_lbl.Name = "titlebar_lbl";
            this.titlebar_lbl.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.titlebar_lbl.Size = new System.Drawing.Size(789, 36);
            this.titlebar_lbl.TabIndex = 131;
            this.titlebar_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // avatar_btn
            // 
            this.avatar_btn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.avatar_btn.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(210)))));
            this.avatar_btn.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.avatar_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.avatar_btn.ForeColor = System.Drawing.Color.White;
            this.avatar_btn.Image = null;
            this.avatar_btn.ImageGlyph = null;
            this.avatar_btn.ImageGlyphEnabled = false;
            this.avatar_btn.Location = new System.Drawing.Point(1284, 6);
            this.avatar_btn.Name = "avatar_btn";
            this.avatar_btn.Size = new System.Drawing.Size(40, 40);
            this.avatar_btn.TabIndex = 130;
            this.avatar_btn.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel2.Controls.Add(this.pin_button);
            this.flowLayoutPanel2.Controls.Add(this.search_panel);
            this.flowLayoutPanel2.Controls.Add(this.ProgressBar1);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(852, 8);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(430, 36);
            this.flowLayoutPanel2.TabIndex = 129;
            // 
            // pin_button
            // 
            this.pin_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pin_button.CustomColor = System.Drawing.Color.Empty;
            this.pin_button.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.pin_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pin_button.ForeColor = System.Drawing.Color.White;
            this.pin_button.Image = ((System.Drawing.Image)(resources.GetObject("pin_button.Image")));
            this.pin_button.ImageGlyph = null;
            this.pin_button.ImageGlyphEnabled = false;
            this.pin_button.Location = new System.Drawing.Point(393, 6);
            this.pin_button.Name = "pin_button";
            this.pin_button.Size = new System.Drawing.Size(34, 24);
            this.pin_button.TabIndex = 128;
            this.pin_button.UseVisualStyleBackColor = false;
            this.pin_button.Visible = false;
            this.pin_button.Click += new System.EventHandler(this.pin_button_Click);
            // 
            // search_panel
            // 
            this.search_panel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.search_panel.BackColor = System.Drawing.Color.Transparent;
            this.search_panel.Controls.Add(this.search_btn);
            this.search_panel.Controls.Add(this.search_box);
            this.search_panel.Controls.Add(this.search_filter_btn);
            this.search_panel.Location = new System.Drawing.Point(44, 3);
            this.search_panel.Name = "search_panel";
            this.search_panel.Size = new System.Drawing.Size(343, 30);
            this.search_panel.TabIndex = 42;
            // 
            // search_btn
            // 
            this.search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.search_btn.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(217)))), ((int)(((byte)(251)))));
            this.search_btn.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.search_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.search_btn.ForeColor = System.Drawing.Color.White;
            this.search_btn.Image = null;
            this.search_btn.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Search;
            this.search_btn.ImageGlyphEnabled = true;
            this.search_btn.Location = new System.Drawing.Point(308, 3);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(32, 24);
            this.search_btn.TabIndex = 40;
            this.search_btn.UseVisualStyleBackColor = false;
            this.search_btn.Click += new System.EventHandler(this.Search_btn_Click);
            // 
            // search_box
            // 
            this.search_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.search_box.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.search_box.Location = new System.Drawing.Point(3, 3);
            this.search_box.MaxLength = 32767;
            this.search_box.Multiline = false;
            this.search_box.Name = "search_box";
            this.search_box.ReadOnly = false;
            this.search_box.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.search_box.SelectedText = "";
            this.search_box.SelectionLength = 0;
            this.search_box.SelectionStart = 0;
            this.search_box.Size = new System.Drawing.Size(261, 24);
            this.search_box.TabIndex = 39;
            this.search_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.search_box.UseSystemPasswordChar = false;
            this.search_box.WordWrap = true;
            this.search_box.KeyboardPress += new WinPaletter.UI.WP.TextBox.KeyboardPressEventHandler(this.Search_box_KeyPress);
            // 
            // search_filter_btn
            // 
            this.search_filter_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.search_filter_btn.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(108)))), ((int)(((byte)(71)))));
            this.search_filter_btn.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.search_filter_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.search_filter_btn.ForeColor = System.Drawing.Color.White;
            this.search_filter_btn.Image = null;
            this.search_filter_btn.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("search_filter_btn.ImageGlyph")));
            this.search_filter_btn.ImageGlyphEnabled = true;
            this.search_filter_btn.Location = new System.Drawing.Point(270, 3);
            this.search_filter_btn.Name = "search_filter_btn";
            this.search_filter_btn.Size = new System.Drawing.Size(32, 24);
            this.search_filter_btn.TabIndex = 41;
            this.search_filter_btn.UseVisualStyleBackColor = false;
            this.search_filter_btn.Click += new System.EventHandler(this.Search_filter_btn_Click);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ProgressBar1.Appearance = WinPaletter.UI.WP.ProgressBar.ProgressBarAppearance.Circle;
            this.ProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBar1.Location = new System.Drawing.Point(8, 3);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(30, 30);
            this.ProgressBar1.State = WinPaletter.UI.WP.ProgressBar.ProgressBarState.Normal;
            this.ProgressBar1.Style = WinPaletter.UI.WP.ProgressBar.ProgressBarStyle.Marquee;
            this.ProgressBar1.TabIndex = 43;
            this.ProgressBar1.TaskbarBroadcast = true;
            // 
            // back_btn
            // 
            this.back_btn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.back_btn.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(210)))));
            this.back_btn.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.back_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.back_btn.ForeColor = System.Drawing.Color.White;
            this.back_btn.Image = null;
            this.back_btn.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("back_btn.ImageGlyph")));
            this.back_btn.ImageGlyphEnabled = true;
            this.back_btn.Location = new System.Drawing.Point(7, 6);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(40, 40);
            this.back_btn.TabIndex = 36;
            this.back_btn.UseVisualStyleBackColor = false;
            this.back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Store
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1329, 721);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.titlebarExtender1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.MinimumSize = new System.Drawing.Size(1130, 680);
            this.Name = "Store";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinPaletter Store";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Store_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Store_FormClosed);
            this.Load += new System.EventHandler(this.Store_Load);
            this.Shown += new System.EventHandler(this.Store_Shown);
            this.ParentChanged += new System.EventHandler(this.Store_ParentChanged);
            this.Tabs.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.os_12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_81)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_vista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os_xp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_winTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_lockScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_classicColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_cursors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_Metrics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_cmd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_ps86)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_ps64)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_terminal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_terminalPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_wallpaper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_effects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_sounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_screenSaver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_altTab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_icons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_accessibility)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspect_winPaletterAppTheme)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.previewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.FlowLayoutPanel1.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Cursors_Container.ResumeLayout(false);
            this.TabPage5.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.bottom_buttons.ResumeLayout(false);
            this.smoothFlowLayoutPanel1.ResumeLayout(false);
            this.titlebarExtender1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.search_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal UI.WP.TablessControl Tabs;
        internal TabPage TabPage1;
        internal SmoothFlowLayoutPanel store_container;
        internal TabPage TabPage3;
        internal UI.WP.Button back_btn;
        internal System.ComponentModel.BackgroundWorker ThemesFetcher;
        internal Label themeSize_lbl;
        internal UI.WP.GroupBox previewContainer;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal UI.WP.Button Apply_btn;
        internal UI.WP.TextBox search_box;
        internal TabPage TabPage5;
        internal SmoothFlowLayoutPanel search_results;
        internal UI.WP.Button search_btn;
        internal UI.WP.Button search_filter_btn;
        internal UI.Simulation.WinCMD CMD1;
        internal UI.Simulation.WinCMD CMD2;
        internal UI.Simulation.WinCMD CMD3;
        internal UI.WP.Button cur_anim_btn;
        internal SmoothFlowLayoutPanel Cursors_Container;
        internal UI.Controllers.CursorControl Arrow;
        internal UI.Controllers.CursorControl Help;
        internal UI.Controllers.CursorControl AppLoading;
        internal UI.Controllers.CursorControl Busy;
        internal UI.Controllers.CursorControl Move_Cur;
        internal UI.Controllers.CursorControl Up;
        internal UI.Controllers.CursorControl NS;
        internal UI.Controllers.CursorControl EW;
        internal UI.Controllers.CursorControl NESW;
        internal UI.Controllers.CursorControl NWSE;
        internal UI.Controllers.CursorControl Pen;
        internal UI.Controllers.CursorControl None;
        internal UI.Controllers.CursorControl Link;
        internal UI.Controllers.CursorControl Pin;
        internal UI.Controllers.CursorControl Person;
        internal UI.Controllers.CursorControl IBeam;
        internal UI.Controllers.CursorControl Cross;
        internal Timer Cursor_Timer;
        internal Panel search_panel;
        internal UI.WP.ProgressBar ProgressBar1;
        internal Label respacksize_lbl;
        internal UI.WP.SeparatorV SeparatorVertical1;
        internal Panel Panel1;
        internal SmoothFlowLayoutPanel FlowLayoutPanel1;
        internal UI.WP.AlertBox VersionAlert_lbl;
        private Templates.RetroDesktopColors retroDesktopColors1;
        private Templates.WindowsDesktop windowsDesktop1;
        public Tabs.TitlebarExtender titlebarExtender1;
        internal UI.WP.Button pin_button;
        private SmoothFlowLayoutPanel flowLayoutPanel2;
        private UI.WP.NumericUpDown numericUpDown1;
        private TabPage tabPage2;
        private UI.WP.Button button4;
        private UI.WP.Button button3;
        private UI.WP.Button button2;
        private UI.WP.Button button5;
        private TabPage tabPage4;
        private UI.WP.TextBox desc_txt;
        private UI.WP.GroupBox groupBox5;
        internal LabelAlt labelAlt3;
        private UI.WP.ProgressBar progressBar_ResPack;
        private FlowLayoutPanel flowLayoutPanel3;
        private Panel panel2;
        internal PictureBox os_12;
        internal PictureBox os_11;
        internal PictureBox os_10;
        internal PictureBox os_81;
        internal PictureBox os_8;
        internal PictureBox os_7;
        internal PictureBox os_vista;
        internal PictureBox os_xp;
        private FlowLayoutPanel flowLayoutPanel4;
        internal LabelAlt author_lbl;
        private FlowLayoutPanel flowLayoutPanel5;
        private Panel panel3;
        internal SeparatorV separatorV1;
        internal SeparatorV separatorV2;
        internal PictureBox aspect_winTheme;
        internal PictureBox aspect_lockScreen;
        internal PictureBox aspect_classicColors;
        internal PictureBox aspect_cursors;
        internal PictureBox aspect_Metrics;
        internal PictureBox aspect_cmd;
        internal PictureBox aspect_ps86;
        internal PictureBox aspect_ps64;
        internal PictureBox aspect_terminal;
        internal PictureBox aspect_terminalPreview;
        internal PictureBox aspect_wallpaper;
        internal PictureBox aspect_effects;
        internal PictureBox aspect_sounds;
        internal PictureBox aspect_screenSaver;
        internal PictureBox aspect_altTab;
        internal PictureBox aspect_icons;
        internal PictureBox aspect_accessibility;
        internal PictureBox aspect_winPaletterAppTheme;
        internal SeparatorV separatorV3;
        internal Label ver_lbl;
        private FlowLayoutPanel flowLayoutPanel6;
        private Panel panel4;
        internal LabelAlt titlebar_lbl;
        internal UI.WP.Button avatar_btn;
        private TabPage tabPage6;
        private UI.WP.GroupBox bottom_buttons;
        internal UI.WP.Button button1;
        internal UI.WP.Button button8;
        internal UI.WP.Button button9;
        internal UI.WP.Button button10;
        internal SmoothFlowLayoutPanel smoothFlowLayoutPanel1;
        private CheckImage toggle_theme;
        private CheckImage toggle_classicColors;
        private CheckImage toggle_lockScreen;
        private CheckImage toggle_cursors;
        private CheckImage toggle_cmd;
        private CheckImage toggle_ps86;
        private CheckImage toggle_ps64;
        private CheckImage toggle_terminal;
        private CheckImage toggle_terminalPreview;
        private CheckImage toggle_wallpaper;
        private CheckImage toggle_effects;
        private CheckImage toggle_sounds;
        private CheckImage toggle_screenSaver;
        private CheckImage toggle_altTab;
        private CheckImage toggle_icons;
        private CheckImage toggle_accessibility;
        private CheckImage toggle_winPaletterTheme;
        private CheckImage toggle_metrics;
        internal SeparatorV separatorV4;
        internal Label lbl_hint;
        private UI.WP.GroupBox groupBox7;
        internal Label label7;
        internal Label label8;
        private Banner banner3;
        private UI.WP.GroupBox groupBox8;
        internal Label label5;
        internal Label label6;
        private Banner banner2;
        private UI.WP.GroupBox groupBox9;
        internal Label label11;
        internal Label label12;
        private Banner banner1;
        private UI.WP.GroupBox groupBox10;
        internal Label label13;
        private Banner banner7;
        private UI.WP.GroupBox groupBox11;
        private Banner banner4;
        private Banner banner6;
        private Banner banner5;
        private UI.WP.GroupBox groupBox1;
        internal UI.WP.Button button6;
        internal UI.WP.Button button7;
        internal UI.WP.TextBox TextBox1;
    }
}
