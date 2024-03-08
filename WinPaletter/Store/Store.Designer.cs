using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store : Form
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
            this.Status_pnl = new System.Windows.Forms.Panel();
            this.Status_lbl = new System.Windows.Forms.Label();
            this.Tabs = new WinPaletter.UI.WP.TablessControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.store_container = new System.Windows.Forms.FlowLayoutPanel();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.VersionAlert_lbl = new WinPaletter.UI.WP.AlertBox();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.SupportedOS_lbl = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.PictureBox14 = new System.Windows.Forms.PictureBox();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Theme_MD5_lbl = new System.Windows.Forms.Label();
            this.desc_txt = new WinPaletter.UI.WP.TextBox();
            this.author_url_button = new WinPaletter.UI.WP.Button();
            this.RestartExplorer = new WinPaletter.UI.WP.Button();
            this.SeparatorVertical1 = new WinPaletter.UI.WP.SeparatorV();
            this.StoreItem1 = new WinPaletter.UI.Controllers.StoreItem();
            this.themeSize_lbl = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Apply_btn = new WinPaletter.UI.WP.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.Edit_btn = new WinPaletter.UI.WP.Button();
            this.respacksize_lbl = new System.Windows.Forms.Label();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.windowsDesktop1 = new WinPaletter.Templates.WindowsDesktop();
            this.retroDesktopColors1 = new WinPaletter.Templates.RetroDesktopColors();
            this.CMD1 = new WinPaletter.UI.Simulation.WinCMD();
            this.CMD2 = new WinPaletter.UI.Simulation.WinCMD();
            this.CMD3 = new WinPaletter.UI.Simulation.WinCMD();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cursors_Container = new System.Windows.Forms.FlowLayoutPanel();
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
            this.PictureBox12 = new System.Windows.Forms.PictureBox();
            this.CursorsSize_Bar = new WinPaletter.UI.WP.TrackBar();
            this.Label17 = new System.Windows.Forms.Label();
            this.cur_anim_btn = new WinPaletter.UI.WP.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.search_results = new System.Windows.Forms.FlowLayoutPanel();
            this.ProgressBar1 = new WinPaletter.UI.WP.ProgressBar();
            this.titlebarExtender1 = new WinPaletter.Tabs.TitlebarExtender();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pin_button = new WinPaletter.UI.WP.Button();
            this.search_panel = new System.Windows.Forms.Panel();
            this.search_btn = new WinPaletter.UI.WP.Button();
            this.search_box = new WinPaletter.UI.WP.TextBox();
            this.search_filter_btn = new WinPaletter.UI.WP.Button();
            this.Titlebar_lbl = new WinPaletter.UI.WP.LabelAlt();
            this.back_btn = new WinPaletter.UI.WP.Button();
            this.Status_pnl.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.previewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.FlowLayoutPanel1.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Cursors_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).BeginInit();
            this.TabPage5.SuspendLayout();
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
            // Status_pnl
            // 
            this.Status_pnl.BackColor = System.Drawing.Color.Transparent;
            this.Status_pnl.Controls.Add(this.Status_lbl);
            this.Status_pnl.Controls.Add(this.ProgressBar1);
            this.Status_pnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Status_pnl.Location = new System.Drawing.Point(0, 697);
            this.Status_pnl.Name = "Status_pnl";
            this.Status_pnl.Padding = new System.Windows.Forms.Padding(3);
            this.Status_pnl.Size = new System.Drawing.Size(1329, 24);
            this.Status_pnl.TabIndex = 6;
            // 
            // Status_lbl
            // 
            this.Status_lbl.AutoEllipsis = true;
            this.Status_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Status_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status_lbl.Location = new System.Drawing.Point(3, 3);
            this.Status_lbl.Name = "Status_lbl";
            this.Status_lbl.Size = new System.Drawing.Size(1095, 18);
            this.Status_lbl.TabIndex = 39;
            this.Status_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.TabPage1);
            this.Tabs.Controls.Add(this.TabPage3);
            this.Tabs.Controls.Add(this.TabPage5);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 52);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1329, 645);
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
            this.TabPage1.Size = new System.Drawing.Size(1321, 617);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "0";
            // 
            // store_container
            // 
            this.store_container.AutoScroll = true;
            this.store_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.store_container.Location = new System.Drawing.Point(10, 10);
            this.store_container.Name = "store_container";
            this.store_container.Size = new System.Drawing.Size(1301, 597);
            this.store_container.TabIndex = 3;
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.VersionAlert_lbl);
            this.TabPage3.Controls.Add(this.GroupBox3);
            this.TabPage3.Controls.Add(this.GroupBox1);
            this.TabPage3.Controls.Add(this.previewContainer);
            this.TabPage3.Location = new System.Drawing.Point(4, 24);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(10);
            this.TabPage3.Size = new System.Drawing.Size(1321, 617);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "1";
            // 
            // VersionAlert_lbl
            // 
            this.VersionAlert_lbl.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.VersionAlert_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionAlert_lbl.BackColor = System.Drawing.Color.Transparent;
            this.VersionAlert_lbl.CenterText = true;
            this.VersionAlert_lbl.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.VersionAlert_lbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VersionAlert_lbl.Image = ((System.Drawing.Image)(resources.GetObject("VersionAlert_lbl.Image")));
            this.VersionAlert_lbl.Location = new System.Drawing.Point(399, 450);
            this.VersionAlert_lbl.Name = "VersionAlert_lbl";
            this.VersionAlert_lbl.Size = new System.Drawing.Size(908, 34);
            this.VersionAlert_lbl.TabIndex = 140;
            this.VersionAlert_lbl.TabStop = false;
            this.VersionAlert_lbl.Text = "0";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.SupportedOS_lbl);
            this.GroupBox3.Controls.Add(this.Label26);
            this.GroupBox3.Controls.Add(this.PictureBox14);
            this.GroupBox3.Location = new System.Drawing.Point(399, 389);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(908, 55);
            this.GroupBox3.TabIndex = 140;
            this.GroupBox3.Text = "GroupBox3";
            // 
            // SupportedOS_lbl
            // 
            this.SupportedOS_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SupportedOS_lbl.BackColor = System.Drawing.Color.Transparent;
            this.SupportedOS_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupportedOS_lbl.Location = new System.Drawing.Point(33, 27);
            this.SupportedOS_lbl.Name = "SupportedOS_lbl";
            this.SupportedOS_lbl.Size = new System.Drawing.Size(871, 24);
            this.SupportedOS_lbl.TabIndex = 8;
            this.SupportedOS_lbl.Text = "0";
            this.SupportedOS_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label26
            // 
            this.Label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label26.BackColor = System.Drawing.Color.Transparent;
            this.Label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label26.Location = new System.Drawing.Point(33, 3);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(871, 24);
            this.Label26.TabIndex = 4;
            this.Label26.Text = "0";
            this.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox14
            // 
            this.PictureBox14.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox14.Image")));
            this.PictureBox14.Location = new System.Drawing.Point(3, 3);
            this.PictureBox14.Name = "PictureBox14";
            this.PictureBox14.Size = new System.Drawing.Size(24, 24);
            this.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox14.TabIndex = 0;
            this.PictureBox14.TabStop = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Controls.Add(this.Theme_MD5_lbl);
            this.GroupBox1.Controls.Add(this.desc_txt);
            this.GroupBox1.Controls.Add(this.author_url_button);
            this.GroupBox1.Controls.Add(this.RestartExplorer);
            this.GroupBox1.Controls.Add(this.SeparatorVertical1);
            this.GroupBox1.Controls.Add(this.StoreItem1);
            this.GroupBox1.Controls.Add(this.themeSize_lbl);
            this.GroupBox1.Controls.Add(this.Label14);
            this.GroupBox1.Controls.Add(this.Apply_btn);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.Edit_btn);
            this.GroupBox1.Controls.Add(this.respacksize_lbl);
            this.GroupBox1.Location = new System.Drawing.Point(13, 13);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(380, 573);
            this.GroupBox1.TabIndex = 139;
            this.GroupBox1.Text = "GroupBox1";
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(193, 234);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(156, 34);
            this.Button1.TabIndex = 146;
            this.Button1.Text = "Save as ...";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Theme_MD5_lbl
            // 
            this.Theme_MD5_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Theme_MD5_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Theme_MD5_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Theme_MD5_lbl.Location = new System.Drawing.Point(14, 537);
            this.Theme_MD5_lbl.Name = "Theme_MD5_lbl";
            this.Theme_MD5_lbl.Size = new System.Drawing.Size(322, 24);
            this.Theme_MD5_lbl.TabIndex = 145;
            this.Theme_MD5_lbl.Text = "0";
            this.Theme_MD5_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // desc_txt
            // 
            this.desc_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.desc_txt.ForeColor = System.Drawing.Color.White;
            this.desc_txt.Location = new System.Drawing.Point(14, 382);
            this.desc_txt.MaxLength = 32767;
            this.desc_txt.Multiline = true;
            this.desc_txt.Name = "desc_txt";
            this.desc_txt.ReadOnly = true;
            this.desc_txt.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.desc_txt.SelectedText = "";
            this.desc_txt.SelectionLength = 0;
            this.desc_txt.SelectionStart = 0;
            this.desc_txt.Size = new System.Drawing.Size(352, 145);
            this.desc_txt.TabIndex = 7;
            this.desc_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.desc_txt.UseSystemPasswordChar = false;
            this.desc_txt.WordWrap = true;
            // 
            // author_url_button
            // 
            this.author_url_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.author_url_button.CustomColor = System.Drawing.Color.Empty;
            this.author_url_button.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.author_url_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.author_url_button.ForeColor = System.Drawing.Color.White;
            this.author_url_button.Image = null;
            this.author_url_button.ImageGlyph = null;
            this.author_url_button.ImageGlyphEnabled = false;
            this.author_url_button.Location = new System.Drawing.Point(342, 537);
            this.author_url_button.Name = "author_url_button";
            this.author_url_button.Size = new System.Drawing.Size(24, 24);
            this.author_url_button.TabIndex = 144;
            this.author_url_button.Text = "↗";
            this.author_url_button.UseVisualStyleBackColor = false;
            this.author_url_button.Click += new System.EventHandler(this.Author_url_button_Click);
            // 
            // RestartExplorer
            // 
            this.RestartExplorer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RestartExplorer.CustomColor = System.Drawing.Color.Empty;
            this.RestartExplorer.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.RestartExplorer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RestartExplorer.ForeColor = System.Drawing.Color.White;
            this.RestartExplorer.Image = null;
            this.RestartExplorer.ImageGlyph = null;
            this.RestartExplorer.ImageGlyphEnabled = false;
            this.RestartExplorer.Location = new System.Drawing.Point(31, 274);
            this.RestartExplorer.Name = "RestartExplorer";
            this.RestartExplorer.Size = new System.Drawing.Size(318, 34);
            this.RestartExplorer.TabIndex = 138;
            this.RestartExplorer.Text = "Restart Explorer";
            this.RestartExplorer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RestartExplorer.UseVisualStyleBackColor = false;
            this.RestartExplorer.Click += new System.EventHandler(this.RestartExplorer_Click);
            // 
            // SeparatorVertical1
            // 
            this.SeparatorVertical1.AlternativeLook = false;
            this.SeparatorVertical1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SeparatorVertical1.BackColor = System.Drawing.Color.Transparent;
            this.SeparatorVertical1.Location = new System.Drawing.Point(190, 320);
            this.SeparatorVertical1.Name = "SeparatorVertical1";
            this.SeparatorVertical1.Size = new System.Drawing.Size(1, 46);
            this.SeparatorVertical1.TabIndex = 143;
            this.SeparatorVertical1.TabStop = false;
            this.SeparatorVertical1.Text = "SeparatorVertical1";
            // 
            // StoreItem1
            // 
            this.StoreItem1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.StoreItem1.DoneByWinPaletter = false;
            this.StoreItem1.FileName = null;
            this.StoreItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StoreItem1.Location = new System.Drawing.Point(32, 7);
            this.StoreItem1.MD5_PackFile = null;
            this.StoreItem1.MD5_ThemeFile = null;
            this.StoreItem1.Name = "StoreItem1";
            this.StoreItem1.Size = new System.Drawing.Size(317, 178);
            this.StoreItem1.TabIndex = 142;
            this.StoreItem1.TM = null;
            this.StoreItem1.URL_PackFile = null;
            this.StoreItem1.URL_ThemeFile = null;
            // 
            // themeSize_lbl
            // 
            this.themeSize_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.themeSize_lbl.BackColor = System.Drawing.Color.Transparent;
            this.themeSize_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themeSize_lbl.Location = new System.Drawing.Point(46, 320);
            this.themeSize_lbl.Name = "themeSize_lbl";
            this.themeSize_lbl.Size = new System.Drawing.Size(138, 24);
            this.themeSize_lbl.TabIndex = 13;
            this.themeSize_lbl.Text = "0";
            this.themeSize_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label14
            // 
            this.Label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(46, 342);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(138, 24);
            this.Label14.TabIndex = 6;
            this.Label14.Text = "Size";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Apply_btn
            // 
            this.Apply_btn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Apply_btn.CustomColor = System.Drawing.Color.Empty;
            this.Apply_btn.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Apply_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Apply_btn.ForeColor = System.Drawing.Color.White;
            this.Apply_btn.Image = null;
            this.Apply_btn.ImageGlyph = null;
            this.Apply_btn.ImageGlyphEnabled = false;
            this.Apply_btn.Location = new System.Drawing.Point(31, 194);
            this.Apply_btn.Name = "Apply_btn";
            this.Apply_btn.Size = new System.Drawing.Size(318, 34);
            this.Apply_btn.TabIndex = 134;
            this.Apply_btn.Text = "Apply";
            this.Apply_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Apply_btn.UseVisualStyleBackColor = false;
            this.Apply_btn.Click += new System.EventHandler(this.Apply_Edit_btn_Click);
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(197, 342);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(138, 24);
            this.Label6.TabIndex = 15;
            this.Label6.Text = "Resources pack size";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Edit_btn
            // 
            this.Edit_btn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Edit_btn.CustomColor = System.Drawing.Color.Empty;
            this.Edit_btn.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Edit_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Edit_btn.ForeColor = System.Drawing.Color.White;
            this.Edit_btn.Image = ((System.Drawing.Image)(resources.GetObject("Edit_btn.Image")));
            this.Edit_btn.ImageGlyph = null;
            this.Edit_btn.ImageGlyphEnabled = false;
            this.Edit_btn.Location = new System.Drawing.Point(31, 234);
            this.Edit_btn.Name = "Edit_btn";
            this.Edit_btn.Size = new System.Drawing.Size(156, 34);
            this.Edit_btn.TabIndex = 137;
            this.Edit_btn.Text = "Edit";
            this.Edit_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Edit_btn.UseVisualStyleBackColor = false;
            this.Edit_btn.Click += new System.EventHandler(this.Apply_Edit_btn_Click);
            // 
            // respacksize_lbl
            // 
            this.respacksize_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.respacksize_lbl.BackColor = System.Drawing.Color.Transparent;
            this.respacksize_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respacksize_lbl.Location = new System.Drawing.Point(197, 320);
            this.respacksize_lbl.Name = "respacksize_lbl";
            this.respacksize_lbl.Size = new System.Drawing.Size(138, 24);
            this.respacksize_lbl.TabIndex = 16;
            this.respacksize_lbl.Text = "0";
            this.respacksize_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.PictureBox41);
            this.previewContainer.Controls.Add(this.Label19);
            this.previewContainer.Controls.Add(this.FlowLayoutPanel1);
            this.previewContainer.Location = new System.Drawing.Point(399, 13);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(908, 370);
            this.previewContainer.TabIndex = 131;
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
            this.Label19.Size = new System.Drawing.Size(859, 31);
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
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(900, 324);
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
            this.windowsDesktop1.Win7Alpha = 100;
            this.windowsDesktop1.Win7ColorBal = 100;
            this.windowsDesktop1.Win7GlowBal = 100;
            this.windowsDesktop1.Win7Noise = 1F;
            this.windowsDesktop1.Window = System.Drawing.Color.Empty;
            this.windowsDesktop1.WindowFrame = System.Drawing.Color.Empty;
            this.windowsDesktop1.Windows_10x_Theme = WinPaletter.Theme.Structures.Windows10x.Themes.Aero;
            this.windowsDesktop1.Windows_7_8_Theme = WinPaletter.Theme.Structures.Windows7.Themes.Aero;
            this.windowsDesktop1.WindowStyle = WinPaletter.PreviewHelpers.WindowStyle.W11;
            this.windowsDesktop1.WindowsXPTheme = WinPaletter.Theme.Structures.WindowsXP.Themes.LunaBlue;
            this.windowsDesktop1.WindowsXPThemeColorScheme = null;
            this.windowsDesktop1.WindowsXPThemePath = null;
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
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Cursors_Container);
            this.Panel1.Controls.Add(this.PictureBox12);
            this.Panel1.Controls.Add(this.CursorsSize_Bar);
            this.Panel1.Controls.Add(this.Label17);
            this.Panel1.Controls.Add(this.cur_anim_btn);
            this.Panel1.Location = new System.Drawing.Point(2677, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(528, 297);
            this.Panel1.TabIndex = 140;
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
            this.Cursors_Container.Size = new System.Drawing.Size(520, 256);
            this.Cursors_Container.TabIndex = 67;
            // 
            // Arrow
            // 
            this.Arrow.Location = new System.Drawing.Point(7, 7);
            this.Arrow.Name = "Arrow";
            this.Arrow.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Arrow.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Arrow.Prop_Cursor = WinPaletter.Paths.CursorType.Arrow;
            this.Arrow.Prop_File = "";
            this.Arrow.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Arrow.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Arrow.Prop_LoadingCircleBackGradient = false;
            this.Arrow.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Arrow.Prop_LoadingCircleBackNoise = false;
            this.Arrow.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Help.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Help.Prop_Cursor = WinPaletter.Paths.CursorType.Help;
            this.Help.Prop_File = "";
            this.Help.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Help.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Help.Prop_LoadingCircleBackGradient = false;
            this.Help.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Help.Prop_LoadingCircleBackNoise = false;
            this.Help.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.AppLoading.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.AppLoading.Prop_Cursor = WinPaletter.Paths.CursorType.AppLoading;
            this.AppLoading.Prop_File = "";
            this.AppLoading.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.AppLoading.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.AppLoading.Prop_LoadingCircleBackGradient = false;
            this.AppLoading.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.AppLoading.Prop_LoadingCircleBackNoise = false;
            this.AppLoading.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Busy.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Busy.Prop_Cursor = WinPaletter.Paths.CursorType.Busy;
            this.Busy.Prop_File = "";
            this.Busy.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Busy.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Busy.Prop_LoadingCircleBackGradient = false;
            this.Busy.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Circle;
            this.Busy.Prop_LoadingCircleBackNoise = false;
            this.Busy.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Move_Cur.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Move_Cur.Prop_Cursor = WinPaletter.Paths.CursorType.Move;
            this.Move_Cur.Prop_File = "";
            this.Move_Cur.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Move_Cur.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Move_Cur.Prop_LoadingCircleBackGradient = false;
            this.Move_Cur.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Move_Cur.Prop_LoadingCircleBackNoise = false;
            this.Move_Cur.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Up.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Up.Prop_Cursor = WinPaletter.Paths.CursorType.Up;
            this.Up.Prop_File = "";
            this.Up.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Up.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Up.Prop_LoadingCircleBackGradient = false;
            this.Up.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Up.Prop_LoadingCircleBackNoise = false;
            this.Up.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.NS.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.NS.Prop_Cursor = WinPaletter.Paths.CursorType.NS;
            this.NS.Prop_File = "";
            this.NS.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NS.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NS.Prop_LoadingCircleBackGradient = false;
            this.NS.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NS.Prop_LoadingCircleBackNoise = false;
            this.NS.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.EW.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.EW.Prop_Cursor = WinPaletter.Paths.CursorType.EW;
            this.EW.Prop_File = "";
            this.EW.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.EW.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.EW.Prop_LoadingCircleBackGradient = false;
            this.EW.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.EW.Prop_LoadingCircleBackNoise = false;
            this.EW.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.NESW.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.NESW.Prop_Cursor = WinPaletter.Paths.CursorType.NESW;
            this.NESW.Prop_File = "";
            this.NESW.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NESW.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NESW.Prop_LoadingCircleBackGradient = false;
            this.NESW.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NESW.Prop_LoadingCircleBackNoise = false;
            this.NESW.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.NWSE.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.NWSE.Prop_Cursor = WinPaletter.Paths.CursorType.NWSE;
            this.NWSE.Prop_File = "";
            this.NWSE.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NWSE.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.NWSE.Prop_LoadingCircleBackGradient = false;
            this.NWSE.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.NWSE.Prop_LoadingCircleBackNoise = false;
            this.NWSE.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Pen.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Pen.Prop_Cursor = WinPaletter.Paths.CursorType.Pen;
            this.Pen.Prop_File = "";
            this.Pen.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pen.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pen.Prop_LoadingCircleBackGradient = false;
            this.Pen.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pen.Prop_LoadingCircleBackNoise = false;
            this.Pen.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.None.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.None.Prop_Cursor = WinPaletter.Paths.CursorType.None;
            this.None.Prop_File = "";
            this.None.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.None.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.None.Prop_LoadingCircleBackGradient = false;
            this.None.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.None.Prop_LoadingCircleBackNoise = false;
            this.None.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Link.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Link.Prop_Cursor = WinPaletter.Paths.CursorType.Link;
            this.Link.Prop_File = "";
            this.Link.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Link.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Link.Prop_LoadingCircleBackGradient = false;
            this.Link.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Link.Prop_LoadingCircleBackNoise = false;
            this.Link.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Pin.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Pin.Prop_Cursor = WinPaletter.Paths.CursorType.Pin;
            this.Pin.Prop_File = "";
            this.Pin.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pin.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Pin.Prop_LoadingCircleBackGradient = false;
            this.Pin.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Pin.Prop_LoadingCircleBackNoise = false;
            this.Pin.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Person.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Person.Prop_Cursor = WinPaletter.Paths.CursorType.Person;
            this.Person.Prop_File = "";
            this.Person.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Person.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Person.Prop_LoadingCircleBackGradient = false;
            this.Person.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Person.Prop_LoadingCircleBackNoise = false;
            this.Person.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.IBeam.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.IBeam.Prop_Cursor = WinPaletter.Paths.CursorType.IBeam;
            this.IBeam.Prop_File = "";
            this.IBeam.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.IBeam.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.IBeam.Prop_LoadingCircleBackGradient = false;
            this.IBeam.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.IBeam.Prop_LoadingCircleBackNoise = false;
            this.IBeam.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            this.Cross.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Cross.Prop_Cursor = WinPaletter.Paths.CursorType.Cross;
            this.Cross.Prop_File = "";
            this.Cross.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Cross.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.Cross.Prop_LoadingCircleBackGradient = false;
            this.Cross.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal;
            this.Cross.Prop_LoadingCircleBackNoise = false;
            this.Cross.Prop_LoadingCircleBackNoiseOpacity = 0.25F;
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
            // PictureBox12
            // 
            this.PictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox12.Image")));
            this.PictureBox12.Location = new System.Drawing.Point(6, 265);
            this.PictureBox12.Name = "PictureBox12";
            this.PictureBox12.Size = new System.Drawing.Size(24, 24);
            this.PictureBox12.TabIndex = 70;
            this.PictureBox12.TabStop = false;
            // 
            // CursorsSize_Bar
            // 
            this.CursorsSize_Bar.BackColor = System.Drawing.Color.Transparent;
            this.CursorsSize_Bar.LargeChange = 50;
            this.CursorsSize_Bar.Location = new System.Drawing.Point(141, 268);
            this.CursorsSize_Bar.Maximum = 320;
            this.CursorsSize_Bar.Minimum = 100;
            this.CursorsSize_Bar.Name = "CursorsSize_Bar";
            this.CursorsSize_Bar.Size = new System.Drawing.Size(213, 19);
            this.CursorsSize_Bar.SmallChange = 20;
            this.CursorsSize_Bar.TabIndex = 68;
            this.CursorsSize_Bar.Value = 100;
            this.CursorsSize_Bar.Scroll += new WinPaletter.UI.WP.TrackBar.ScrollEventHandler(this.CursorsSize_Bar_Scroll);
            // 
            // Label17
            // 
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(36, 265);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(99, 24);
            this.Label17.TabIndex = 69;
            this.Label17.Text = "Scaling (1x)";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cur_anim_btn
            // 
            this.cur_anim_btn.CustomColor = System.Drawing.Color.Empty;
            this.cur_anim_btn.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.cur_anim_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cur_anim_btn.ForeColor = System.Drawing.Color.White;
            this.cur_anim_btn.Image = ((System.Drawing.Image)(resources.GetObject("cur_anim_btn.Image")));
            this.cur_anim_btn.ImageGlyph = null;
            this.cur_anim_btn.ImageGlyphEnabled = false;
            this.cur_anim_btn.Location = new System.Drawing.Point(360, 265);
            this.cur_anim_btn.Name = "cur_anim_btn";
            this.cur_anim_btn.Size = new System.Drawing.Size(160, 24);
            this.cur_anim_btn.TabIndex = 72;
            this.cur_anim_btn.Text = "Animate (3 Cycles)";
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
            this.TabPage5.Size = new System.Drawing.Size(1321, 617);
            this.TabPage5.TabIndex = 3;
            this.TabPage5.Text = "2";
            // 
            // search_results
            // 
            this.search_results.AutoScroll = true;
            this.search_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.search_results.Location = new System.Drawing.Point(10, 10);
            this.search_results.Name = "search_results";
            this.search_results.Size = new System.Drawing.Size(1301, 597);
            this.search_results.TabIndex = 4;
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.AnimationDuration = 1000;
            this.ProgressBar1.Appearance = WinPaletter.UI.WP.ProgressBar.ProgressBarAppearance.Bar;
            this.ProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ProgressBar1.Location = new System.Drawing.Point(1098, 3);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(228, 18);
            this.ProgressBar1.State = WinPaletter.UI.WP.ProgressBar.ProgressBarState.Normal;
            this.ProgressBar1.Style = WinPaletter.UI.WP.ProgressBar.ProgressBarStyle.Continuous;
            this.ProgressBar1.TabIndex = 43;
            this.ProgressBar1.TaskbarBroadcast = true;
            this.ProgressBar1.Visible = false;
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.flowLayoutPanel2);
            this.titlebarExtender1.Controls.Add(this.Titlebar_lbl);
            this.titlebarExtender1.Controls.Add(this.back_btn);
            this.titlebarExtender1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebarExtender1.DropDWMEffect = false;
            this.titlebarExtender1.Location = new System.Drawing.Point(0, 0);
            this.titlebarExtender1.Name = "titlebarExtender1";
            this.titlebarExtender1.Size = new System.Drawing.Size(1329, 52);
            this.titlebarExtender1.TabIndex = 116;
            this.titlebarExtender1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel2.Controls.Add(this.pin_button);
            this.flowLayoutPanel2.Controls.Add(this.search_panel);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(930, 8);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(395, 36);
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
            this.pin_button.Location = new System.Drawing.Point(358, 6);
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
            this.search_panel.Location = new System.Drawing.Point(9, 3);
            this.search_panel.Name = "search_panel";
            this.search_panel.Size = new System.Drawing.Size(343, 30);
            this.search_panel.TabIndex = 42;
            // 
            // search_btn
            // 
            this.search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.search_btn.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(217)))), ((int)(((byte)(251)))));
            this.search_btn.Flag = WinPaletter.UI.WP.Button.Flags.AlwaysCustomColor;
            this.search_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.search_btn.ForeColor = System.Drawing.Color.White;
            this.search_btn.Image = null;
            this.search_btn.ImageGlyph = global::WinPaletter.Properties.Resources.Vector_Search;
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
            this.search_filter_btn.Flag = WinPaletter.UI.WP.Button.Flags.AlwaysCustomColor;
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
            // Titlebar_lbl
            // 
            this.Titlebar_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Titlebar_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Titlebar_lbl.DrawOnGlass = false;
            this.Titlebar_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Titlebar_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titlebar_lbl.Location = new System.Drawing.Point(57, 6);
            this.Titlebar_lbl.Name = "Titlebar_lbl";
            this.Titlebar_lbl.Size = new System.Drawing.Size(867, 40);
            this.Titlebar_lbl.TabIndex = 38;
            this.Titlebar_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Titlebar_lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseDown);
            this.Titlebar_lbl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseMove);
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
            this.back_btn.Visible = false;
            this.back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Store
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1329, 721);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.Status_pnl);
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
            this.Load += new System.EventHandler(this.Store_Load);
            this.Shown += new System.EventHandler(this.Store_Shown);
            this.ParentChanged += new System.EventHandler(this.Store_ParentChanged);
            this.Status_pnl.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.previewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.FlowLayoutPanel1.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Cursors_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.titlebarExtender1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.search_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal UI.WP.TablessControl Tabs;
        internal TabPage TabPage1;
        internal FlowLayoutPanel store_container;
        internal TabPage TabPage3;
        internal UI.WP.Button back_btn;
        internal System.ComponentModel.BackgroundWorker ThemesFetcher;
        internal UI.WP.LabelAlt Titlebar_lbl;
        internal Label themeSize_lbl;
        internal Label Label14;
        internal UI.WP.GroupBox previewContainer;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal Label SupportedOS_lbl;
        internal Label Label26;
        internal PictureBox PictureBox14;
        internal UI.WP.Button Apply_btn;
        internal UI.WP.Button Edit_btn;
        internal UI.WP.Button RestartExplorer;
        internal UI.WP.TextBox search_box;
        internal TabPage TabPage5;
        internal FlowLayoutPanel search_results;
        internal UI.WP.Button search_btn;
        internal UI.WP.Button search_filter_btn;
        internal UI.Simulation.WinCMD CMD1;
        internal UI.Simulation.WinCMD CMD2;
        internal UI.Simulation.WinCMD CMD3;
        internal UI.WP.Button cur_anim_btn;
        internal FlowLayoutPanel Cursors_Container;
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
        internal PictureBox PictureBox12;
        internal Label Label17;
        internal UI.WP.TrackBar CursorsSize_Bar;
        internal Timer Cursor_Timer;
        internal Panel search_panel;
        internal UI.WP.ProgressBar ProgressBar1;
        internal Panel Status_pnl;
        internal Label Status_lbl;
        internal Label respacksize_lbl;
        internal Label Label6;
        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.Controllers.StoreItem StoreItem1;
        internal UI.WP.SeparatorV SeparatorVertical1;
        internal Panel Panel1;
        internal FlowLayoutPanel FlowLayoutPanel1;
        internal UI.WP.TextBox desc_txt;
        internal UI.WP.AlertBox VersionAlert_lbl;
        internal UI.WP.Button author_url_button;
        internal UI.WP.Button Button1;
        internal Label Theme_MD5_lbl;
        private Templates.RetroDesktopColors retroDesktopColors1;
        private Templates.WindowsDesktop windowsDesktop1;
        public Tabs.TitlebarExtender titlebarExtender1;
        internal UI.WP.Button pin_button;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}
