using System;
using System.Diagnostics;
using System.Drawing;
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
            this.FilesFetcher = new System.ComponentModel.BackgroundWorker();
            this.Titlebar_panel = new System.Windows.Forms.Panel();
            this.search_panel = new System.Windows.Forms.Panel();
            this.search_btn = new WinPaletter.UI.WP.Button();
            this.search_box = new WinPaletter.UI.WP.TextBox();
            this.search_filter_btn = new WinPaletter.UI.WP.Button();
            this.Titlebar_lbl = new WinPaletter.UI.WP.LabelAlt();
            this.back_btn = new WinPaletter.UI.WP.Button();
            this.Titlebar_img = new System.Windows.Forms.PictureBox();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
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
            this.tabs_preview = new WinPaletter.UI.WP.TablessControl();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.pnl_preview = new System.Windows.Forms.Panel();
            this.WXP_Alert2 = new WinPaletter.UI.WP.AlertBox();
            this.ActionCenter = new WinPaletter.UI.Simulation.WinElement();
            this.start = new WinPaletter.UI.Simulation.WinElement();
            this.taskbar = new WinPaletter.UI.Simulation.WinElement();
            this.Window2 = new WinPaletter.UI.Simulation.Window();
            this.Window1 = new WinPaletter.UI.Simulation.Window();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label8 = new WinPaletter.UI.WP.LabelAlt();
            this.setting_icon_preview = new WinPaletter.UI.WP.LabelAlt();
            this.lnk_preview = new WinPaletter.UI.WP.LabelAlt();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.pnl_preview_classic = new System.Windows.Forms.Panel();
            this.ClassicWindow1 = new WinPaletter.UI.Retro.WindowR();
            this.ClassicWindow2 = new WinPaletter.UI.Retro.WindowR();
            this.ClassicTaskbar = new WinPaletter.UI.Retro.PanelRaisedR();
            this.ButtonR4 = new WinPaletter.UI.Retro.ButtonR();
            this.ButtonR3 = new WinPaletter.UI.Retro.ButtonR();
            this.ButtonR2 = new WinPaletter.UI.Retro.ButtonR();
            this.ClassicColorsPreview = new System.Windows.Forms.Panel();
            this.RetroShadow1 = new WinPaletter.UI.WP.TransparentPictureBox();
            this.Menu_Window = new WinPaletter.UI.Retro.WindowR();
            this.menucontainer3 = new System.Windows.Forms.Panel();
            this.LabelR9 = new WinPaletter.UI.WP.LabelAlt();
            this.highlight = new System.Windows.Forms.Panel();
            this.menuhilight = new System.Windows.Forms.Panel();
            this.LabelR5 = new WinPaletter.UI.WP.LabelAlt();
            this.menucontainer1 = new System.Windows.Forms.Panel();
            this.LabelR6 = new WinPaletter.UI.WP.LabelAlt();
            this.WindowR3 = new WinPaletter.UI.Retro.WindowR();
            this.ButtonR5 = new WinPaletter.UI.Retro.ButtonR();
            this.LabelR4 = new WinPaletter.UI.WP.LabelAlt();
            this.LabelR13 = new WinPaletter.UI.WP.LabelAlt();
            this.WindowR2 = new WinPaletter.UI.Retro.WindowR();
            this.TextBoxR1 = new WinPaletter.UI.Retro.TextBoxR();
            this.menucontainer0 = new System.Windows.Forms.Panel();
            this.PanelR1 = new WinPaletter.UI.Retro.PanelR();
            this.LabelR3 = new WinPaletter.UI.WP.LabelAlt();
            this.LabelR2 = new WinPaletter.UI.WP.LabelAlt();
            this.LabelR1 = new WinPaletter.UI.WP.LabelAlt();
            this.WindowR1 = new WinPaletter.UI.Retro.WindowR();
            this.WindowR4 = new WinPaletter.UI.Retro.WindowR();
            this.programcontainer = new System.Windows.Forms.Panel();
            this.PanelR2 = new WinPaletter.UI.Retro.ScrollBarR();
            this.ButtonR12 = new WinPaletter.UI.Retro.ButtonR();
            this.ButtonR11 = new WinPaletter.UI.Retro.ButtonR();
            this.ButtonR10 = new WinPaletter.UI.Retro.ButtonR();
            this.CMD1 = new WinPaletter.UI.Simulation.WinCMD();
            this.CMD2 = new WinPaletter.UI.Simulation.WinCMD();
            this.CMD3 = new WinPaletter.UI.Simulation.WinCMD();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.cur_anim_btn = new WinPaletter.UI.WP.Button();
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
            this.cur_tip_btn = new WinPaletter.UI.WP.Button();
            this.PictureBox12 = new System.Windows.Forms.PictureBox();
            this.CursorsSize_Bar = new WinPaletter.UI.WP.Trackbar();
            this.Label17 = new System.Windows.Forms.Label();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.search_results = new System.Windows.Forms.FlowLayoutPanel();
            this.Titlebar_panel.SuspendLayout();
            this.search_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Titlebar_img)).BeginInit();
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
            this.tabs_preview.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.pnl_preview.SuspendLayout();
            this.Window1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.pnl_preview_classic.SuspendLayout();
            this.ClassicTaskbar.SuspendLayout();
            this.ClassicColorsPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RetroShadow1)).BeginInit();
            this.Menu_Window.SuspendLayout();
            this.menucontainer3.SuspendLayout();
            this.highlight.SuspendLayout();
            this.menuhilight.SuspendLayout();
            this.menucontainer1.SuspendLayout();
            this.WindowR3.SuspendLayout();
            this.WindowR2.SuspendLayout();
            this.menucontainer0.SuspendLayout();
            this.PanelR1.SuspendLayout();
            this.WindowR4.SuspendLayout();
            this.programcontainer.SuspendLayout();
            this.PanelR2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Cursors_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // FilesFetcher
            // 
            this.FilesFetcher.WorkerReportsProgress = true;
            this.FilesFetcher.WorkerSupportsCancellation = true;
            this.FilesFetcher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FilesFetcher_DoWork);
            this.FilesFetcher.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.FilesFetcher_ProgressChanged);
            this.FilesFetcher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.FilesFetcher_RunWorkerCompleted);
            // 
            // Titlebar_panel
            // 
            this.Titlebar_panel.Controls.Add(this.search_panel);
            this.Titlebar_panel.Controls.Add(this.Titlebar_lbl);
            this.Titlebar_panel.Controls.Add(this.back_btn);
            this.Titlebar_panel.Controls.Add(this.Titlebar_img);
            this.Titlebar_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Titlebar_panel.Location = new System.Drawing.Point(0, 0);
            this.Titlebar_panel.Name = "Titlebar_panel";
            this.Titlebar_panel.Size = new System.Drawing.Size(1329, 70);
            this.Titlebar_panel.TabIndex = 5;
            this.Titlebar_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseDown);
            this.Titlebar_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseMove);
            // 
            // search_panel
            // 
            this.search_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.search_panel.BackColor = System.Drawing.Color.Transparent;
            this.search_panel.Controls.Add(this.search_btn);
            this.search_panel.Controls.Add(this.search_box);
            this.search_panel.Controls.Add(this.search_filter_btn);
            this.search_panel.Location = new System.Drawing.Point(979, 17);
            this.search_panel.Name = "search_panel";
            this.search_panel.Size = new System.Drawing.Size(343, 30);
            this.search_panel.TabIndex = 42;
            // 
            // search_btn
            // 
            this.search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.search_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.search_btn.DrawOnGlass = true;
            this.search_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.search_btn.ForeColor = System.Drawing.Color.White;
            this.search_btn.Image = ((System.Drawing.Image)(resources.GetObject("search_btn.Image")));
            this.search_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(111)))), ((int)(((byte)(122)))));
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
            this.search_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.search_box.DrawOnGlass = true;
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
            this.search_filter_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.search_filter_btn.DrawOnGlass = true;
            this.search_filter_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.search_filter_btn.ForeColor = System.Drawing.Color.White;
            this.search_filter_btn.Image = ((System.Drawing.Image)(resources.GetObject("search_filter_btn.Image")));
            this.search_filter_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(39)))), ((int)(((byte)(28)))));
            this.search_filter_btn.Location = new System.Drawing.Point(270, 3);
            this.search_filter_btn.Name = "search_filter_btn";
            this.search_filter_btn.Size = new System.Drawing.Size(32, 24);
            this.search_filter_btn.TabIndex = 41;
            this.search_filter_btn.UseVisualStyleBackColor = false;
            this.search_filter_btn.Click += new System.EventHandler(this.Search_filter_btn_Click);
            // 
            // Titlebar_lbl
            // 
            this.Titlebar_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Titlebar_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Titlebar_lbl.DrawOnGlass = true;
            this.Titlebar_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Titlebar_lbl.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titlebar_lbl.Location = new System.Drawing.Point(78, 1);
            this.Titlebar_lbl.Name = "Titlebar_lbl";
            this.Titlebar_lbl.Size = new System.Drawing.Size(895, 64);
            this.Titlebar_lbl.TabIndex = 38;
            this.Titlebar_lbl.Text = "WinPaletter Store";
            this.Titlebar_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Titlebar_lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseDown);
            this.Titlebar_lbl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseMove);
            // 
            // back_btn
            // 
            this.back_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.back_btn.DrawOnGlass = true;
            this.back_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.back_btn.ForeColor = System.Drawing.Color.White;
            this.back_btn.Image = null;
            this.back_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.back_btn.Location = new System.Drawing.Point(8, 0);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(64, 64);
            this.back_btn.TabIndex = 36;
            this.back_btn.UseVisualStyleBackColor = false;
            this.back_btn.Visible = false;
            this.back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Titlebar_img
            // 
            this.Titlebar_img.BackColor = System.Drawing.Color.Transparent;
            this.Titlebar_img.Image = ((System.Drawing.Image)(resources.GetObject("Titlebar_img.Image")));
            this.Titlebar_img.Location = new System.Drawing.Point(5, 0);
            this.Titlebar_img.Name = "Titlebar_img";
            this.Titlebar_img.Size = new System.Drawing.Size(64, 64);
            this.Titlebar_img.TabIndex = 37;
            this.Titlebar_img.TabStop = false;
            this.Titlebar_img.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseDown);
            this.Titlebar_img.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomTitlebar_MouseMove);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ProgressBar1.Location = new System.Drawing.Point(1098, 3);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(228, 18);
            this.ProgressBar1.TabIndex = 43;
            this.ProgressBar1.Visible = false;
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
            this.Tabs.Location = new System.Drawing.Point(0, 70);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1329, 627);
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
            this.TabPage1.Size = new System.Drawing.Size(1321, 599);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "0";
            // 
            // store_container
            // 
            this.store_container.AutoScroll = true;
            this.store_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.store_container.Location = new System.Drawing.Point(10, 10);
            this.store_container.Name = "store_container";
            this.store_container.Size = new System.Drawing.Size(1301, 579);
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
            this.TabPage3.Size = new System.Drawing.Size(1321, 599);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "1";
            // 
            // VersionAlert_lbl
            // 
            this.VersionAlert_lbl.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.VersionAlert_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionAlert_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(72)))), ((int)(((byte)(6)))));
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
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button1.DrawOnGlass = false;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(111)))), ((int)(((byte)(122)))));
            this.Button1.Location = new System.Drawing.Point(193, 234);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(156, 34);
            this.Button1.TabIndex = 146;
            this.Button1.Text = "Save as ...";
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
            this.desc_txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.desc_txt.DrawOnGlass = false;
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
            this.author_url_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.author_url_button.DrawOnGlass = false;
            this.author_url_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.author_url_button.ForeColor = System.Drawing.Color.White;
            this.author_url_button.Image = null;
            this.author_url_button.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
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
            this.RestartExplorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.RestartExplorer.DrawOnGlass = false;
            this.RestartExplorer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RestartExplorer.ForeColor = System.Drawing.Color.White;
            this.RestartExplorer.Image = null;
            this.RestartExplorer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RestartExplorer.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(127)))), ((int)(((byte)(79)))));
            this.RestartExplorer.Location = new System.Drawing.Point(31, 274);
            this.RestartExplorer.Name = "RestartExplorer";
            this.RestartExplorer.Size = new System.Drawing.Size(318, 34);
            this.RestartExplorer.TabIndex = 138;
            this.RestartExplorer.Text = "Restart Explorer";
            this.RestartExplorer.UseVisualStyleBackColor = false;
            this.RestartExplorer.Click += new System.EventHandler(this.RestartExplorer_Click);
            // 
            // SeparatorVertical1
            // 
            this.SeparatorVertical1.AlternativeLook = false;
            this.SeparatorVertical1.Anchor = System.Windows.Forms.AnchorStyles.Top;
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
            this.Apply_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Apply_btn.DrawOnGlass = false;
            this.Apply_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Apply_btn.ForeColor = System.Drawing.Color.White;
            this.Apply_btn.Image = null;
            this.Apply_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Apply_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.Apply_btn.Location = new System.Drawing.Point(31, 194);
            this.Apply_btn.Name = "Apply_btn";
            this.Apply_btn.Size = new System.Drawing.Size(318, 34);
            this.Apply_btn.TabIndex = 134;
            this.Apply_btn.Text = "Apply";
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
            this.Edit_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Edit_btn.DrawOnGlass = false;
            this.Edit_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Edit_btn.ForeColor = System.Drawing.Color.White;
            this.Edit_btn.Image = ((System.Drawing.Image)(resources.GetObject("Edit_btn.Image")));
            this.Edit_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Edit_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(39)))), ((int)(((byte)(12)))));
            this.Edit_btn.Location = new System.Drawing.Point(31, 234);
            this.Edit_btn.Name = "Edit_btn";
            this.Edit_btn.Size = new System.Drawing.Size(156, 34);
            this.Edit_btn.TabIndex = 137;
            this.Edit_btn.Text = "Edit";
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
            this.FlowLayoutPanel1.Controls.Add(this.tabs_preview);
            this.FlowLayoutPanel1.Controls.Add(this.ClassicColorsPreview);
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
            // tabs_preview
            // 
            this.tabs_preview.Controls.Add(this.TabPage4);
            this.tabs_preview.Controls.Add(this.TabPage6);
            this.tabs_preview.Location = new System.Drawing.Point(3, 3);
            this.tabs_preview.Name = "tabs_preview";
            this.tabs_preview.SelectedIndex = 0;
            this.tabs_preview.Size = new System.Drawing.Size(528, 297);
            this.tabs_preview.TabIndex = 140;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage4.Controls.Add(this.pnl_preview);
            this.TabPage4.Location = new System.Drawing.Point(4, 24);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(520, 269);
            this.TabPage4.TabIndex = 0;
            this.TabPage4.Text = "0";
            // 
            // pnl_preview
            // 
            this.pnl_preview.BackColor = System.Drawing.Color.Black;
            this.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_preview.Controls.Add(this.WXP_Alert2);
            this.pnl_preview.Controls.Add(this.ActionCenter);
            this.pnl_preview.Controls.Add(this.start);
            this.pnl_preview.Controls.Add(this.taskbar);
            this.pnl_preview.Controls.Add(this.Window2);
            this.pnl_preview.Controls.Add(this.Window1);
            this.pnl_preview.Location = new System.Drawing.Point(0, 0);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 2;
            // 
            // WXP_Alert2
            // 
            this.WXP_Alert2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.WXP_Alert2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WXP_Alert2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.WXP_Alert2.CenterText = true;
            this.WXP_Alert2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.WXP_Alert2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Alert2.Image = null;
            this.WXP_Alert2.Location = new System.Drawing.Point(7, 6);
            this.WXP_Alert2.Name = "WXP_Alert2";
            this.WXP_Alert2.Size = new System.Drawing.Size(22, 22);
            this.WXP_Alert2.TabIndex = 54;
            this.WXP_Alert2.TabStop = false;
            this.WXP_Alert2.Text = null;
            this.WXP_Alert2.Visible = false;
            // 
            // ActionCenter
            // 
            this.ActionCenter.ActionCenterButton_Hover = System.Drawing.Color.Empty;
            this.ActionCenter.ActionCenterButton_Normal = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.ActionCenter.ActionCenterButton_Pressed = System.Drawing.Color.Empty;
            this.ActionCenter.AppBackground = System.Drawing.Color.Empty;
            this.ActionCenter.AppUnderline = System.Drawing.Color.Empty;
            this.ActionCenter.BackColor = System.Drawing.Color.Transparent;
            this.ActionCenter.BackColorAlpha = 50;
            this.ActionCenter.Background = System.Drawing.Color.Empty;
            this.ActionCenter.Background2 = System.Drawing.Color.Empty;
            this.ActionCenter.BlurPower = 8;
            this.ActionCenter.DarkMode = true;
            this.ActionCenter.LinkColor = System.Drawing.Color.Empty;
            this.ActionCenter.Location = new System.Drawing.Point(400, 165);
            this.ActionCenter.Name = "ActionCenter";
            this.ActionCenter.NoisePower = 0.2F;
            this.ActionCenter.Padding = new System.Windows.Forms.Padding(2);
            this.ActionCenter.Shadow = true;
            this.ActionCenter.Size = new System.Drawing.Size(120, 85);
            this.ActionCenter.StartColor = System.Drawing.Color.Empty;
            this.ActionCenter.Style = WinPaletter.UI.Simulation.WinElement.Styles.ActionCenter11;
            this.ActionCenter.SuspendRefresh = false;
            this.ActionCenter.TabIndex = 5;
            this.ActionCenter.Transparency = true;
            this.ActionCenter.UseWin11ORB_WithWin10 = false;
            this.ActionCenter.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.ActionCenter.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.ActionCenter.Win7ColorBal = 100;
            this.ActionCenter.Win7GlowBal = 100;
            // 
            // start
            // 
            this.start.ActionCenterButton_Hover = System.Drawing.Color.Empty;
            this.start.ActionCenterButton_Normal = System.Drawing.Color.Empty;
            this.start.ActionCenterButton_Pressed = System.Drawing.Color.Empty;
            this.start.AppBackground = System.Drawing.Color.Empty;
            this.start.AppUnderline = System.Drawing.Color.Empty;
            this.start.BackColor = System.Drawing.Color.Transparent;
            this.start.BackColorAlpha = 150;
            this.start.Background = System.Drawing.Color.Empty;
            this.start.Background2 = System.Drawing.Color.Empty;
            this.start.BlurPower = 7;
            this.start.DarkMode = true;
            this.start.LinkColor = System.Drawing.Color.Empty;
            this.start.Location = new System.Drawing.Point(7, 50);
            this.start.Name = "start";
            this.start.NoisePower = 0.2F;
            this.start.Padding = new System.Windows.Forms.Padding(2);
            this.start.Shadow = true;
            this.start.Size = new System.Drawing.Size(135, 200);
            this.start.StartColor = System.Drawing.Color.Empty;
            this.start.Style = WinPaletter.UI.Simulation.WinElement.Styles.Start11;
            this.start.SuspendRefresh = false;
            this.start.TabIndex = 1;
            this.start.Transparency = true;
            this.start.UseWin11ORB_WithWin10 = false;
            this.start.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.start.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.start.Win7ColorBal = 100;
            this.start.Win7GlowBal = 100;
            // 
            // taskbar
            // 
            this.taskbar.ActionCenterButton_Hover = System.Drawing.Color.Empty;
            this.taskbar.ActionCenterButton_Normal = System.Drawing.Color.Empty;
            this.taskbar.ActionCenterButton_Pressed = System.Drawing.Color.Empty;
            this.taskbar.AppBackground = System.Drawing.Color.Empty;
            this.taskbar.AppUnderline = System.Drawing.Color.Empty;
            this.taskbar.BackColor = System.Drawing.Color.Transparent;
            this.taskbar.BackColorAlpha = 130;
            this.taskbar.Background = System.Drawing.Color.Empty;
            this.taskbar.Background2 = System.Drawing.Color.Empty;
            this.taskbar.BlurPower = 12;
            this.taskbar.DarkMode = true;
            this.taskbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.taskbar.LinkColor = System.Drawing.Color.Empty;
            this.taskbar.Location = new System.Drawing.Point(0, 253);
            this.taskbar.Name = "taskbar";
            this.taskbar.NoisePower = 0.2F;
            this.taskbar.Shadow = true;
            this.taskbar.Size = new System.Drawing.Size(526, 42);
            this.taskbar.StartColor = System.Drawing.Color.Empty;
            this.taskbar.Style = WinPaletter.UI.Simulation.WinElement.Styles.Taskbar11;
            this.taskbar.SuspendRefresh = false;
            this.taskbar.TabIndex = 0;
            this.taskbar.Transparency = true;
            this.taskbar.UseWin11ORB_WithWin10 = false;
            this.taskbar.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.taskbar.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.taskbar.Win7ColorBal = 100;
            this.taskbar.Win7GlowBal = 100;
            // 
            // Window2
            // 
            this.Window2.AccentColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(100)))));
            this.Window2.AccentColor_Enabled = true;
            this.Window2.AccentColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window2.AccentColor2_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window2.AccentColor2_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window2.Active = false;
            this.Window2.BackColor = System.Drawing.Color.Transparent;
            this.Window2.DarkMode = true;
            this.Window2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Window2.Location = new System.Drawing.Point(172, 160);
            this.Window2.Metrics_BorderWidth = 1;
            this.Window2.Metrics_CaptionHeight = 22;
            this.Window2.Metrics_PaddedBorderWidth = 4;
            this.Window2.Name = "Window2";
            this.Window2.Padding = new System.Windows.Forms.Padding(4, 40, 4, 4);
            this.Window2.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.Window2.Radius = 5;
            this.Window2.Shadow = true;
            this.Window2.Size = new System.Drawing.Size(189, 85);
            this.Window2.SuspendRefresh = false;
            this.Window2.TabIndex = 3;
            this.Window2.Text = "Inactive app";
            this.Window2.ToolWindow = false;
            this.Window2.Win7Alpha = 100;
            this.Window2.Win7ColorBal = 100;
            this.Window2.Win7GlowBal = 100;
            this.Window2.Win7Noise = 1F;
            this.Window2.WinVista = false;
            // 
            // Window1
            // 
            this.Window1.AccentColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window1.AccentColor_Enabled = true;
            this.Window1.AccentColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window1.AccentColor2_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window1.AccentColor2_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window1.Active = true;
            this.Window1.BackColor = System.Drawing.Color.Transparent;
            this.Window1.Controls.Add(this.Panel3);
            this.Window1.Controls.Add(this.lnk_preview);
            this.Window1.DarkMode = true;
            this.Window1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Window1.Location = new System.Drawing.Point(172, 13);
            this.Window1.Metrics_BorderWidth = 1;
            this.Window1.Metrics_CaptionHeight = 22;
            this.Window1.Metrics_PaddedBorderWidth = 4;
            this.Window1.Name = "Window1";
            this.Window1.Padding = new System.Windows.Forms.Padding(4, 40, 4, 4);
            this.Window1.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.Window1.Radius = 5;
            this.Window1.Shadow = true;
            this.Window1.Size = new System.Drawing.Size(189, 147);
            this.Window1.SuspendRefresh = false;
            this.Window1.TabIndex = 2;
            this.Window1.Text = "App preview";
            this.Window1.ToolWindow = false;
            this.Window1.Win7Alpha = 100;
            this.Window1.Win7ColorBal = 100;
            this.Window1.Win7GlowBal = 100;
            this.Window1.Win7Noise = 1F;
            this.Window1.WinVista = false;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Transparent;
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.setting_icon_preview);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel3.Location = new System.Drawing.Point(4, 40);
            this.Panel3.Name = "Panel3";
            this.Panel3.Padding = new System.Windows.Forms.Padding(1);
            this.Panel3.Size = new System.Drawing.Size(181, 78);
            this.Panel3.TabIndex = 0;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label8.DrawOnGlass = false;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label8.Location = new System.Drawing.Point(1, 46);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(179, 31);
            this.Label8.TabIndex = 15;
            this.Label8.Text = "This is a setting icon";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // setting_icon_preview
            // 
            this.setting_icon_preview.BackColor = System.Drawing.Color.Transparent;
            this.setting_icon_preview.Dock = System.Windows.Forms.DockStyle.Top;
            this.setting_icon_preview.DrawOnGlass = false;
            this.setting_icon_preview.Font = new System.Drawing.Font("Segoe MDL2 Assets", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setting_icon_preview.Location = new System.Drawing.Point(1, 1);
            this.setting_icon_preview.Name = "setting_icon_preview";
            this.setting_icon_preview.Size = new System.Drawing.Size(179, 45);
            this.setting_icon_preview.TabIndex = 14;
            this.setting_icon_preview.Text = "";
            this.setting_icon_preview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnk_preview
            // 
            this.lnk_preview.BackColor = System.Drawing.Color.Transparent;
            this.lnk_preview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnk_preview.DrawOnGlass = false;
            this.lnk_preview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnk_preview.ForeColor = System.Drawing.Color.Brown;
            this.lnk_preview.Location = new System.Drawing.Point(4, 118);
            this.lnk_preview.Name = "lnk_preview";
            this.lnk_preview.Size = new System.Drawing.Size(181, 25);
            this.lnk_preview.TabIndex = 16;
            this.lnk_preview.Text = "Settings link preview";
            this.lnk_preview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage6.Controls.Add(this.pnl_preview_classic);
            this.TabPage6.Location = new System.Drawing.Point(4, 24);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage6.Size = new System.Drawing.Size(520, 269);
            this.TabPage6.TabIndex = 1;
            this.TabPage6.Text = "1";
            // 
            // pnl_preview_classic
            // 
            this.pnl_preview_classic.BackColor = System.Drawing.Color.Black;
            this.pnl_preview_classic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_preview_classic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_preview_classic.Controls.Add(this.ClassicWindow1);
            this.pnl_preview_classic.Controls.Add(this.ClassicWindow2);
            this.pnl_preview_classic.Controls.Add(this.ClassicTaskbar);
            this.pnl_preview_classic.Location = new System.Drawing.Point(0, 0);
            this.pnl_preview_classic.Name = "pnl_preview_classic";
            this.pnl_preview_classic.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview_classic.TabIndex = 34;
            // 
            // ClassicWindow1
            // 
            this.ClassicWindow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ButtonDkShadow = System.Drawing.Color.Black;
            this.ClassicWindow1.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ButtonHilight = System.Drawing.Color.White;
            this.ClassicWindow1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClassicWindow1.ButtonText = System.Drawing.Color.Black;
            this.ClassicWindow1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.ClassicWindow1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(132)))), ((int)(((byte)(208)))));
            this.ClassicWindow1.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ColorGradient = true;
            this.ClassicWindow1.ControlBox = true;
            this.ClassicWindow1.Flat = false;
            this.ClassicWindow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ClassicWindow1.ForeColor = System.Drawing.Color.White;
            this.ClassicWindow1.Location = new System.Drawing.Point(176, 20);
            this.ClassicWindow1.MaximizeBox = false;
            this.ClassicWindow1.Metrics_BorderWidth = 0;
            this.ClassicWindow1.Metrics_CaptionHeight = 18;
            this.ClassicWindow1.Metrics_CaptionWidth = 18;
            this.ClassicWindow1.Metrics_PaddedBorderWidth = 0;
            this.ClassicWindow1.MinimizeBox = false;
            this.ClassicWindow1.Name = "ClassicWindow1";
            this.ClassicWindow1.Padding = new System.Windows.Forms.Padding(4, 22, 4, 4);
            this.ClassicWindow1.Size = new System.Drawing.Size(181, 146);
            this.ClassicWindow1.TabIndex = 4;
            this.ClassicWindow1.Text = "App preview";
            this.ClassicWindow1.UseItAsMenu = false;
            // 
            // ClassicWindow2
            // 
            this.ClassicWindow2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ButtonDkShadow = System.Drawing.Color.Black;
            this.ClassicWindow2.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ButtonHilight = System.Drawing.Color.White;
            this.ClassicWindow2.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClassicWindow2.ButtonText = System.Drawing.Color.Black;
            this.ClassicWindow2.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClassicWindow2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.ClassicWindow2.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ColorGradient = true;
            this.ClassicWindow2.ControlBox = true;
            this.ClassicWindow2.Flat = false;
            this.ClassicWindow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ClassicWindow2.ForeColor = System.Drawing.Color.White;
            this.ClassicWindow2.Location = new System.Drawing.Point(176, 172);
            this.ClassicWindow2.MaximizeBox = false;
            this.ClassicWindow2.Metrics_BorderWidth = 0;
            this.ClassicWindow2.Metrics_CaptionHeight = 18;
            this.ClassicWindow2.Metrics_CaptionWidth = 18;
            this.ClassicWindow2.Metrics_PaddedBorderWidth = 0;
            this.ClassicWindow2.MinimizeBox = false;
            this.ClassicWindow2.Name = "ClassicWindow2";
            this.ClassicWindow2.Padding = new System.Windows.Forms.Padding(4, 22, 4, 4);
            this.ClassicWindow2.Size = new System.Drawing.Size(181, 60);
            this.ClassicWindow2.TabIndex = 5;
            this.ClassicWindow2.Text = "Inactive app";
            this.ClassicWindow2.UseItAsMenu = false;
            // 
            // ClassicTaskbar
            // 
            this.ClassicTaskbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicTaskbar.ButtonDkShadow = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ClassicTaskbar.ButtonHilight = System.Drawing.Color.White;
            this.ClassicTaskbar.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.ClassicTaskbar.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClassicTaskbar.Controls.Add(this.ButtonR4);
            this.ClassicTaskbar.Controls.Add(this.ButtonR3);
            this.ClassicTaskbar.Controls.Add(this.ButtonR2);
            this.ClassicTaskbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ClassicTaskbar.Flat = false;
            this.ClassicTaskbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ClassicTaskbar.ForeColor = System.Drawing.Color.Black;
            this.ClassicTaskbar.Location = new System.Drawing.Point(0, 251);
            this.ClassicTaskbar.Name = "ClassicTaskbar";
            this.ClassicTaskbar.Size = new System.Drawing.Size(526, 44);
            this.ClassicTaskbar.Style2 = false;
            this.ClassicTaskbar.TabIndex = 0;
            this.ClassicTaskbar.UseItAsWin7Taskbar = true;
            // 
            // ButtonR4
            // 
            this.ButtonR4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonR4.AppearsAsPressed = false;
            this.ButtonR4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR4.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR4.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR4.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR4.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR4.FocusRectHeight = 1;
            this.ButtonR4.FocusRectWidth = 1;
            this.ButtonR4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonR4.ForeColor = System.Drawing.Color.Black;
            this.ButtonR4.HatchBrush = false;
            this.ButtonR4.Image = null;
            this.ButtonR4.Location = new System.Drawing.Point(113, 4);
            this.ButtonR4.Name = "ButtonR4";
            this.ButtonR4.Size = new System.Drawing.Size(48, 38);
            this.ButtonR4.TabIndex = 2;
            this.ButtonR4.UseItAsScrollbar = false;
            this.ButtonR4.UseVisualStyleBackColor = false;
            this.ButtonR4.WindowFrame = System.Drawing.Color.Black;
            // 
            // ButtonR3
            // 
            this.ButtonR3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonR3.AppearsAsPressed = true;
            this.ButtonR3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR3.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR3.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR3.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR3.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR3.FocusRectHeight = 1;
            this.ButtonR3.FocusRectWidth = 1;
            this.ButtonR3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonR3.ForeColor = System.Drawing.Color.Black;
            this.ButtonR3.HatchBrush = false;
            this.ButtonR3.Image = null;
            this.ButtonR3.Location = new System.Drawing.Point(63, 4);
            this.ButtonR3.Name = "ButtonR3";
            this.ButtonR3.Size = new System.Drawing.Size(48, 38);
            this.ButtonR3.TabIndex = 1;
            this.ButtonR3.UseItAsScrollbar = false;
            this.ButtonR3.UseVisualStyleBackColor = false;
            this.ButtonR3.WindowFrame = System.Drawing.Color.Black;
            // 
            // ButtonR2
            // 
            this.ButtonR2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonR2.AppearsAsPressed = false;
            this.ButtonR2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR2.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR2.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR2.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR2.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR2.FocusRectHeight = 1;
            this.ButtonR2.FocusRectWidth = 1;
            this.ButtonR2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonR2.ForeColor = System.Drawing.Color.Black;
            this.ButtonR2.HatchBrush = false;
            this.ButtonR2.Image = null;
            this.ButtonR2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonR2.Location = new System.Drawing.Point(2, 4);
            this.ButtonR2.Name = "ButtonR2";
            this.ButtonR2.Size = new System.Drawing.Size(52, 38);
            this.ButtonR2.TabIndex = 0;
            this.ButtonR2.Text = "Start";
            this.ButtonR2.UseItAsScrollbar = false;
            this.ButtonR2.UseVisualStyleBackColor = false;
            this.ButtonR2.WindowFrame = System.Drawing.Color.Black;
            // 
            // ClassicColorsPreview
            // 
            this.ClassicColorsPreview.BackColor = System.Drawing.Color.Teal;
            this.ClassicColorsPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClassicColorsPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClassicColorsPreview.Controls.Add(this.RetroShadow1);
            this.ClassicColorsPreview.Controls.Add(this.Menu_Window);
            this.ClassicColorsPreview.Controls.Add(this.WindowR3);
            this.ClassicColorsPreview.Controls.Add(this.LabelR13);
            this.ClassicColorsPreview.Controls.Add(this.WindowR2);
            this.ClassicColorsPreview.Controls.Add(this.WindowR1);
            this.ClassicColorsPreview.Controls.Add(this.WindowR4);
            this.ClassicColorsPreview.Location = new System.Drawing.Point(537, 3);
            this.ClassicColorsPreview.Name = "ClassicColorsPreview";
            this.ClassicColorsPreview.Size = new System.Drawing.Size(528, 297);
            this.ClassicColorsPreview.TabIndex = 43;
            // 
            // RetroShadow1
            // 
            this.RetroShadow1.BackColor = System.Drawing.Color.Transparent;
            this.RetroShadow1.Location = new System.Drawing.Point(112, 225);
            this.RetroShadow1.Name = "RetroShadow1";
            this.RetroShadow1.Size = new System.Drawing.Size(115, 66);
            this.RetroShadow1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RetroShadow1.TabIndex = 7;
            this.RetroShadow1.TabStop = false;
            // 
            // Menu_Window
            // 
            this.Menu_Window.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Menu_Window.ButtonDkShadow = System.Drawing.Color.Black;
            this.Menu_Window.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Menu_Window.ButtonHilight = System.Drawing.Color.White;
            this.Menu_Window.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Menu_Window.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Menu_Window.ButtonText = System.Drawing.Color.Black;
            this.Menu_Window.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.Menu_Window.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(132)))), ((int)(((byte)(208)))));
            this.Menu_Window.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Menu_Window.ColorGradient = true;
            this.Menu_Window.ControlBox = true;
            this.Menu_Window.Controls.Add(this.menucontainer3);
            this.Menu_Window.Controls.Add(this.highlight);
            this.Menu_Window.Controls.Add(this.menucontainer1);
            this.Menu_Window.Flat = false;
            this.Menu_Window.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Menu_Window.ForeColor = System.Drawing.Color.Black;
            this.Menu_Window.Location = new System.Drawing.Point(300, 151);
            this.Menu_Window.MaximizeBox = true;
            this.Menu_Window.Metrics_BorderWidth = 1;
            this.Menu_Window.Metrics_CaptionHeight = 22;
            this.Menu_Window.Metrics_CaptionWidth = 0;
            this.Menu_Window.Metrics_PaddedBorderWidth = 4;
            this.Menu_Window.MinimizeBox = true;
            this.Menu_Window.Name = "Menu_Window";
            this.Menu_Window.Padding = new System.Windows.Forms.Padding(3, 3, 5, 5);
            this.Menu_Window.Size = new System.Drawing.Size(115, 66);
            this.Menu_Window.TabIndex = 4;
            this.Menu_Window.Text = "New Window";
            this.Menu_Window.UseItAsMenu = true;
            this.Menu_Window.LocationChanged += new System.EventHandler(this.Menu_Window_SizeChanged);
            this.Menu_Window.SizeChanged += new System.EventHandler(this.Menu_Window_SizeChanged);
            // 
            // menucontainer3
            // 
            this.menucontainer3.BackColor = System.Drawing.Color.Transparent;
            this.menucontainer3.Controls.Add(this.LabelR9);
            this.menucontainer3.Dock = System.Windows.Forms.DockStyle.Top;
            this.menucontainer3.Location = new System.Drawing.Point(3, 43);
            this.menucontainer3.Name = "menucontainer3";
            this.menucontainer3.Padding = new System.Windows.Forms.Padding(21, 0, 0, 0);
            this.menucontainer3.Size = new System.Drawing.Size(107, 20);
            this.menucontainer3.TabIndex = 12;
            // 
            // LabelR9
            // 
            this.LabelR9.BackColor = System.Drawing.Color.Transparent;
            this.LabelR9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelR9.DrawOnGlass = false;
            this.LabelR9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR9.ForeColor = System.Drawing.Color.DimGray;
            this.LabelR9.Location = new System.Drawing.Point(21, 0);
            this.LabelR9.Name = "LabelR9";
            this.LabelR9.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.LabelR9.Size = new System.Drawing.Size(86, 20);
            this.LabelR9.TabIndex = 3;
            this.LabelR9.Text = "Disabled item";
            this.LabelR9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // highlight
            // 
            this.highlight.BackColor = System.Drawing.Color.Navy;
            this.highlight.Controls.Add(this.menuhilight);
            this.highlight.Dock = System.Windows.Forms.DockStyle.Top;
            this.highlight.Location = new System.Drawing.Point(3, 23);
            this.highlight.Name = "highlight";
            this.highlight.Size = new System.Drawing.Size(107, 20);
            this.highlight.TabIndex = 10;
            // 
            // menuhilight
            // 
            this.menuhilight.BackColor = System.Drawing.Color.Navy;
            this.menuhilight.Controls.Add(this.LabelR5);
            this.menuhilight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuhilight.Location = new System.Drawing.Point(0, 0);
            this.menuhilight.Name = "menuhilight";
            this.menuhilight.Padding = new System.Windows.Forms.Padding(21, 0, 1, 0);
            this.menuhilight.Size = new System.Drawing.Size(107, 20);
            this.menuhilight.TabIndex = 11;
            // 
            // LabelR5
            // 
            this.LabelR5.BackColor = System.Drawing.Color.Transparent;
            this.LabelR5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelR5.DrawOnGlass = false;
            this.LabelR5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR5.ForeColor = System.Drawing.Color.White;
            this.LabelR5.Location = new System.Drawing.Point(21, 0);
            this.LabelR5.Name = "LabelR5";
            this.LabelR5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.LabelR5.Size = new System.Drawing.Size(85, 20);
            this.LabelR5.TabIndex = 3;
            this.LabelR5.Text = "Hovered item";
            this.LabelR5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menucontainer1
            // 
            this.menucontainer1.BackColor = System.Drawing.Color.Transparent;
            this.menucontainer1.Controls.Add(this.LabelR6);
            this.menucontainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menucontainer1.Location = new System.Drawing.Point(3, 3);
            this.menucontainer1.Name = "menucontainer1";
            this.menucontainer1.Padding = new System.Windows.Forms.Padding(21, 0, 0, 0);
            this.menucontainer1.Size = new System.Drawing.Size(107, 20);
            this.menucontainer1.TabIndex = 6;
            // 
            // LabelR6
            // 
            this.LabelR6.BackColor = System.Drawing.Color.Transparent;
            this.LabelR6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelR6.DrawOnGlass = false;
            this.LabelR6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR6.ForeColor = System.Drawing.Color.Black;
            this.LabelR6.Location = new System.Drawing.Point(21, 0);
            this.LabelR6.Name = "LabelR6";
            this.LabelR6.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.LabelR6.Size = new System.Drawing.Size(86, 20);
            this.LabelR6.TabIndex = 3;
            this.LabelR6.Text = "Menu item";
            this.LabelR6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WindowR3
            // 
            this.WindowR3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR3.ButtonDkShadow = System.Drawing.Color.Black;
            this.WindowR3.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR3.ButtonHilight = System.Drawing.Color.White;
            this.WindowR3.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR3.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.WindowR3.ButtonText = System.Drawing.Color.Black;
            this.WindowR3.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.WindowR3.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(132)))), ((int)(((byte)(208)))));
            this.WindowR3.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR3.ColorGradient = true;
            this.WindowR3.ControlBox = false;
            this.WindowR3.Controls.Add(this.ButtonR5);
            this.WindowR3.Controls.Add(this.LabelR4);
            this.WindowR3.Flat = false;
            this.WindowR3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.WindowR3.ForeColor = System.Drawing.Color.White;
            this.WindowR3.Location = new System.Drawing.Point(215, 185);
            this.WindowR3.MaximizeBox = true;
            this.WindowR3.Metrics_BorderWidth = 0;
            this.WindowR3.Metrics_CaptionHeight = 18;
            this.WindowR3.Metrics_CaptionWidth = 18;
            this.WindowR3.Metrics_PaddedBorderWidth = 0;
            this.WindowR3.MinimizeBox = true;
            this.WindowR3.Name = "WindowR3";
            this.WindowR3.Padding = new System.Windows.Forms.Padding(4, 22, 4, 4);
            this.WindowR3.Size = new System.Drawing.Size(147, 80);
            this.WindowR3.TabIndex = 2;
            this.WindowR3.Text = "Message box";
            this.WindowR3.UseItAsMenu = false;
            // 
            // ButtonR5
            // 
            this.ButtonR5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonR5.AppearsAsPressed = false;
            this.ButtonR5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR5.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR5.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR5.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR5.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR5.FocusRectHeight = 1;
            this.ButtonR5.FocusRectWidth = 1;
            this.ButtonR5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonR5.ForeColor = System.Drawing.Color.Black;
            this.ButtonR5.HatchBrush = false;
            this.ButtonR5.Image = null;
            this.ButtonR5.Location = new System.Drawing.Point(37, 49);
            this.ButtonR5.Name = "ButtonR5";
            this.ButtonR5.Size = new System.Drawing.Size(75, 23);
            this.ButtonR5.TabIndex = 2;
            this.ButtonR5.Text = "OK";
            this.ButtonR5.UseItAsScrollbar = false;
            this.ButtonR5.UseVisualStyleBackColor = false;
            this.ButtonR5.WindowFrame = System.Drawing.Color.Black;
            // 
            // LabelR4
            // 
            this.LabelR4.AutoSize = true;
            this.LabelR4.BackColor = System.Drawing.Color.Transparent;
            this.LabelR4.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelR4.DrawOnGlass = false;
            this.LabelR4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR4.ForeColor = System.Drawing.Color.Black;
            this.LabelR4.Location = new System.Drawing.Point(4, 22);
            this.LabelR4.Name = "LabelR4";
            this.LabelR4.Padding = new System.Windows.Forms.Padding(4);
            this.LabelR4.Size = new System.Drawing.Size(78, 21);
            this.LabelR4.TabIndex = 1;
            this.LabelR4.Text = "Message text";
            this.LabelR4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelR13
            // 
            this.LabelR13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelR13.AutoSize = true;
            this.LabelR13.BackColor = System.Drawing.Color.White;
            this.LabelR13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelR13.DrawOnGlass = false;
            this.LabelR13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR13.ForeColor = System.Drawing.Color.Black;
            this.LabelR13.Location = new System.Drawing.Point(287, 47);
            this.LabelR13.Name = "LabelR13";
            this.LabelR13.Size = new System.Drawing.Size(79, 15);
            this.LabelR13.TabIndex = 5;
            this.LabelR13.Text = "This is a tooltip";
            // 
            // WindowR2
            // 
            this.WindowR2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR2.ButtonDkShadow = System.Drawing.Color.Black;
            this.WindowR2.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR2.ButtonHilight = System.Drawing.Color.White;
            this.WindowR2.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR2.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.WindowR2.ButtonText = System.Drawing.Color.Black;
            this.WindowR2.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.WindowR2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(132)))), ((int)(((byte)(208)))));
            this.WindowR2.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR2.ColorGradient = true;
            this.WindowR2.ControlBox = true;
            this.WindowR2.Controls.Add(this.TextBoxR1);
            this.WindowR2.Controls.Add(this.menucontainer0);
            this.WindowR2.Flat = false;
            this.WindowR2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.WindowR2.ForeColor = System.Drawing.Color.White;
            this.WindowR2.Location = new System.Drawing.Point(195, 110);
            this.WindowR2.MaximizeBox = true;
            this.WindowR2.Metrics_BorderWidth = 0;
            this.WindowR2.Metrics_CaptionHeight = 18;
            this.WindowR2.Metrics_CaptionWidth = 18;
            this.WindowR2.Metrics_PaddedBorderWidth = 0;
            this.WindowR2.MinimizeBox = true;
            this.WindowR2.Name = "WindowR2";
            this.WindowR2.Padding = new System.Windows.Forms.Padding(4, 22, 4, 4);
            this.WindowR2.Size = new System.Drawing.Size(196, 120);
            this.WindowR2.TabIndex = 1;
            this.WindowR2.Text = "Active window";
            this.WindowR2.UseItAsMenu = false;
            // 
            // TextBoxR1
            // 
            this.TextBoxR1.BackColor = System.Drawing.Color.White;
            this.TextBoxR1.ButtonDkShadow = System.Drawing.Color.Black;
            this.TextBoxR1.ButtonHilight = System.Drawing.Color.White;
            this.TextBoxR1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TextBoxR1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TextBoxR1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.TextBoxR1.ForeColor = System.Drawing.Color.Black;
            this.TextBoxR1.Location = new System.Drawing.Point(4, 40);
            this.TextBoxR1.MaxLength = 32767;
            this.TextBoxR1.Multiline = true;
            this.TextBoxR1.Name = "TextBoxR1";
            this.TextBoxR1.ReadOnly = true;
            this.TextBoxR1.Size = new System.Drawing.Size(188, 76);
            this.TextBoxR1.TabIndex = 3;
            this.TextBoxR1.Text = "Window text";
            this.TextBoxR1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBoxR1.UseSystemPasswordChar = false;
            // 
            // menucontainer0
            // 
            this.menucontainer0.BackColor = System.Drawing.Color.Silver;
            this.menucontainer0.Controls.Add(this.PanelR1);
            this.menucontainer0.Controls.Add(this.LabelR2);
            this.menucontainer0.Controls.Add(this.LabelR1);
            this.menucontainer0.Dock = System.Windows.Forms.DockStyle.Top;
            this.menucontainer0.Location = new System.Drawing.Point(4, 22);
            this.menucontainer0.Name = "menucontainer0";
            this.menucontainer0.Size = new System.Drawing.Size(188, 18);
            this.menucontainer0.TabIndex = 5;
            // 
            // PanelR1
            // 
            this.PanelR1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PanelR1.ButtonDkShadow = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.PanelR1.ButtonHilight = System.Drawing.Color.White;
            this.PanelR1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.PanelR1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.PanelR1.Controls.Add(this.LabelR3);
            this.PanelR1.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelR1.Flat = false;
            this.PanelR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.PanelR1.ForeColor = System.Drawing.Color.Black;
            this.PanelR1.Location = new System.Drawing.Point(88, 0);
            this.PanelR1.Name = "PanelR1";
            this.PanelR1.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.PanelR1.Size = new System.Drawing.Size(53, 18);
            this.PanelR1.Style2 = false;
            this.PanelR1.TabIndex = 2;
            // 
            // LabelR3
            // 
            this.LabelR3.BackColor = System.Drawing.Color.Transparent;
            this.LabelR3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelR3.DrawOnGlass = false;
            this.LabelR3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR3.ForeColor = System.Drawing.Color.Black;
            this.LabelR3.Location = new System.Drawing.Point(1, 3);
            this.LabelR3.Name = "LabelR3";
            this.LabelR3.Size = new System.Drawing.Size(51, 12);
            this.LabelR3.TabIndex = 1;
            this.LabelR3.Text = "Selected";
            this.LabelR3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelR2
            // 
            this.LabelR2.BackColor = System.Drawing.Color.Transparent;
            this.LabelR2.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelR2.DrawOnGlass = false;
            this.LabelR2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LabelR2.Location = new System.Drawing.Point(40, 0);
            this.LabelR2.Name = "LabelR2";
            this.LabelR2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.LabelR2.Size = new System.Drawing.Size(48, 18);
            this.LabelR2.TabIndex = 1;
            this.LabelR2.Text = "Disabled";
            this.LabelR2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelR1
            // 
            this.LabelR1.BackColor = System.Drawing.Color.Transparent;
            this.LabelR1.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelR1.DrawOnGlass = false;
            this.LabelR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LabelR1.ForeColor = System.Drawing.Color.Black;
            this.LabelR1.Location = new System.Drawing.Point(0, 0);
            this.LabelR1.Name = "LabelR1";
            this.LabelR1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.LabelR1.Size = new System.Drawing.Size(40, 18);
            this.LabelR1.TabIndex = 0;
            this.LabelR1.Text = "Normal";
            this.LabelR1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WindowR1
            // 
            this.WindowR1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR1.ButtonDkShadow = System.Drawing.Color.Black;
            this.WindowR1.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR1.ButtonHilight = System.Drawing.Color.White;
            this.WindowR1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.WindowR1.ButtonText = System.Drawing.Color.Black;
            this.WindowR1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.WindowR1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.WindowR1.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR1.ColorGradient = true;
            this.WindowR1.ControlBox = true;
            this.WindowR1.Flat = false;
            this.WindowR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.WindowR1.ForeColor = System.Drawing.Color.White;
            this.WindowR1.Location = new System.Drawing.Point(179, 77);
            this.WindowR1.MaximizeBox = true;
            this.WindowR1.Metrics_BorderWidth = 0;
            this.WindowR1.Metrics_CaptionHeight = 18;
            this.WindowR1.Metrics_CaptionWidth = 18;
            this.WindowR1.Metrics_PaddedBorderWidth = 0;
            this.WindowR1.MinimizeBox = true;
            this.WindowR1.Name = "WindowR1";
            this.WindowR1.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.WindowR1.Size = new System.Drawing.Size(180, 112);
            this.WindowR1.TabIndex = 0;
            this.WindowR1.Text = "Inactive window";
            this.WindowR1.UseItAsMenu = false;
            // 
            // WindowR4
            // 
            this.WindowR4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR4.ButtonDkShadow = System.Drawing.Color.Black;
            this.WindowR4.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR4.ButtonHilight = System.Drawing.Color.White;
            this.WindowR4.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR4.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.WindowR4.ButtonText = System.Drawing.Color.Black;
            this.WindowR4.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.WindowR4.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(132)))), ((int)(((byte)(208)))));
            this.WindowR4.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.WindowR4.ColorGradient = true;
            this.WindowR4.ControlBox = true;
            this.WindowR4.Controls.Add(this.programcontainer);
            this.WindowR4.Flat = false;
            this.WindowR4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.WindowR4.ForeColor = System.Drawing.Color.White;
            this.WindowR4.Location = new System.Drawing.Point(139, 30);
            this.WindowR4.MaximizeBox = false;
            this.WindowR4.Metrics_BorderWidth = 0;
            this.WindowR4.Metrics_CaptionHeight = 18;
            this.WindowR4.Metrics_CaptionWidth = 18;
            this.WindowR4.Metrics_PaddedBorderWidth = 0;
            this.WindowR4.MinimizeBox = false;
            this.WindowR4.Name = "WindowR4";
            this.WindowR4.Padding = new System.Windows.Forms.Padding(4, 22, 4, 4);
            this.WindowR4.Size = new System.Drawing.Size(156, 132);
            this.WindowR4.TabIndex = 3;
            this.WindowR4.Text = "Program container";
            this.WindowR4.UseItAsMenu = false;
            // 
            // programcontainer
            // 
            this.programcontainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.programcontainer.Controls.Add(this.PanelR2);
            this.programcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programcontainer.Location = new System.Drawing.Point(4, 22);
            this.programcontainer.Name = "programcontainer";
            this.programcontainer.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.programcontainer.Size = new System.Drawing.Size(148, 106);
            this.programcontainer.TabIndex = 4;
            // 
            // PanelR2
            // 
            this.PanelR2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PanelR2.ButtonHilight = System.Drawing.Color.White;
            this.PanelR2.Controls.Add(this.ButtonR12);
            this.PanelR2.Controls.Add(this.ButtonR11);
            this.PanelR2.Controls.Add(this.ButtonR10);
            this.PanelR2.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelR2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.PanelR2.ForeColor = System.Drawing.Color.Black;
            this.PanelR2.Location = new System.Drawing.Point(0, 0);
            this.PanelR2.Name = "PanelR2";
            this.PanelR2.Size = new System.Drawing.Size(16, 106);
            this.PanelR2.TabIndex = 0;
            // 
            // ButtonR12
            // 
            this.ButtonR12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonR12.AppearsAsPressed = false;
            this.ButtonR12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR12.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR12.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR12.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR12.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR12.FocusRectHeight = 1;
            this.ButtonR12.FocusRectWidth = 1;
            this.ButtonR12.Font = new System.Drawing.Font("Marlett", 6F);
            this.ButtonR12.ForeColor = System.Drawing.Color.Black;
            this.ButtonR12.HatchBrush = false;
            this.ButtonR12.Image = null;
            this.ButtonR12.Location = new System.Drawing.Point(0, 29);
            this.ButtonR12.Name = "ButtonR12";
            this.ButtonR12.Size = new System.Drawing.Size(16, 31);
            this.ButtonR12.TabIndex = 7;
            this.ButtonR12.UseItAsScrollbar = true;
            this.ButtonR12.UseVisualStyleBackColor = false;
            this.ButtonR12.WindowFrame = System.Drawing.Color.Black;
            // 
            // ButtonR11
            // 
            this.ButtonR11.AppearsAsPressed = false;
            this.ButtonR11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR11.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR11.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR11.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR11.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonR11.FocusRectHeight = 1;
            this.ButtonR11.FocusRectWidth = 1;
            this.ButtonR11.Font = new System.Drawing.Font("Marlett", 8.7F, System.Drawing.FontStyle.Bold);
            this.ButtonR11.ForeColor = System.Drawing.Color.Black;
            this.ButtonR11.HatchBrush = false;
            this.ButtonR11.Image = null;
            this.ButtonR11.Location = new System.Drawing.Point(0, 92);
            this.ButtonR11.Name = "ButtonR11";
            this.ButtonR11.Size = new System.Drawing.Size(16, 14);
            this.ButtonR11.TabIndex = 6;
            this.ButtonR11.Text = "u";
            this.ButtonR11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ButtonR11.UseItAsScrollbar = false;
            this.ButtonR11.UseVisualStyleBackColor = false;
            this.ButtonR11.WindowFrame = System.Drawing.Color.Black;
            // 
            // ButtonR10
            // 
            this.ButtonR10.AppearsAsPressed = false;
            this.ButtonR10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR10.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR10.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR10.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR10.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR10.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonR10.FocusRectHeight = 1;
            this.ButtonR10.FocusRectWidth = 1;
            this.ButtonR10.Font = new System.Drawing.Font("Marlett", 8.7F, System.Drawing.FontStyle.Bold);
            this.ButtonR10.ForeColor = System.Drawing.Color.Black;
            this.ButtonR10.HatchBrush = false;
            this.ButtonR10.Image = null;
            this.ButtonR10.Location = new System.Drawing.Point(0, 0);
            this.ButtonR10.Name = "ButtonR10";
            this.ButtonR10.Size = new System.Drawing.Size(16, 14);
            this.ButtonR10.TabIndex = 5;
            this.ButtonR10.Text = "t";
            this.ButtonR10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ButtonR10.UseItAsScrollbar = false;
            this.ButtonR10.UseVisualStyleBackColor = false;
            this.ButtonR10.WindowFrame = System.Drawing.Color.Black;
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
            this.CMD1.Location = new System.Drawing.Point(1071, 3);
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
            this.CMD2.Location = new System.Drawing.Point(1605, 3);
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
            this.CMD3.Location = new System.Drawing.Point(2139, 3);
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
            this.Panel1.Controls.Add(this.cur_anim_btn);
            this.Panel1.Controls.Add(this.Cursors_Container);
            this.Panel1.Controls.Add(this.cur_tip_btn);
            this.Panel1.Controls.Add(this.PictureBox12);
            this.Panel1.Controls.Add(this.CursorsSize_Bar);
            this.Panel1.Controls.Add(this.Label17);
            this.Panel1.Location = new System.Drawing.Point(2673, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(528, 297);
            this.Panel1.TabIndex = 140;
            // 
            // cur_anim_btn
            // 
            this.cur_anim_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.cur_anim_btn.DrawOnGlass = false;
            this.cur_anim_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cur_anim_btn.ForeColor = System.Drawing.Color.White;
            this.cur_anim_btn.Image = ((System.Drawing.Image)(resources.GetObject("cur_anim_btn.Image")));
            this.cur_anim_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cur_anim_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(99)))), ((int)(((byte)(126)))));
            this.cur_anim_btn.Location = new System.Drawing.Point(356, 267);
            this.cur_anim_btn.Name = "cur_anim_btn";
            this.cur_anim_btn.Size = new System.Drawing.Size(141, 21);
            this.cur_anim_btn.TabIndex = 72;
            this.cur_anim_btn.Text = "Animate (3 Cycles)";
            this.cur_anim_btn.UseVisualStyleBackColor = false;
            this.cur_anim_btn.Click += new System.EventHandler(this.Cur_anim_btn_Click);
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
            this.Cursors_Container.Size = new System.Drawing.Size(520, 218);
            this.Cursors_Container.TabIndex = 67;
            // 
            // Arrow
            // 
            this.Arrow.Location = new System.Drawing.Point(7, 7);
            this.Arrow.Name = "Arrow";
            this.Arrow.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero;
            this.Arrow.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero;
            this.Arrow.Prop_Cursor = WinPaletter.Paths.CursorType.Arrow;
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
            this.Cross.Size = new System.Drawing.Size(64, 64);
            this.Cross.TabIndex = 20;
            // 
            // cur_tip_btn
            // 
            this.cur_tip_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.cur_tip_btn.DrawOnGlass = false;
            this.cur_tip_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cur_tip_btn.ForeColor = System.Drawing.Color.White;
            this.cur_tip_btn.Image = null;
            this.cur_tip_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.cur_tip_btn.Location = new System.Drawing.Point(500, 267);
            this.cur_tip_btn.Name = "cur_tip_btn";
            this.cur_tip_btn.Size = new System.Drawing.Size(20, 21);
            this.cur_tip_btn.TabIndex = 71;
            this.cur_tip_btn.Text = "?";
            this.cur_tip_btn.UseVisualStyleBackColor = false;
            this.cur_tip_btn.Click += new System.EventHandler(this.Cur_tip_btn_Click);
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
            this.CursorsSize_Bar.LargeChange = 50;
            this.CursorsSize_Bar.Location = new System.Drawing.Point(127, 268);
            this.CursorsSize_Bar.Maximum = 320;
            this.CursorsSize_Bar.Minimum = 100;
            this.CursorsSize_Bar.Name = "CursorsSize_Bar";
            this.CursorsSize_Bar.Size = new System.Drawing.Size(223, 19);
            this.CursorsSize_Bar.SmallChange = 20;
            this.CursorsSize_Bar.TabIndex = 68;
            this.CursorsSize_Bar.Value = 100;
            this.CursorsSize_Bar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.CursorsSize_Bar_Scroll);
            // 
            // Label17
            // 
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(36, 265);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(85, 24);
            this.Label17.TabIndex = 69;
            this.Label17.Text = "Scaling (1x)";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage5.Controls.Add(this.search_results);
            this.TabPage5.Location = new System.Drawing.Point(4, 24);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(10);
            this.TabPage5.Size = new System.Drawing.Size(1321, 599);
            this.TabPage5.TabIndex = 3;
            this.TabPage5.Text = "2";
            // 
            // search_results
            // 
            this.search_results.AutoScroll = true;
            this.search_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.search_results.Location = new System.Drawing.Point(10, 10);
            this.search_results.Name = "search_results";
            this.search_results.Size = new System.Drawing.Size(1301, 579);
            this.search_results.TabIndex = 4;
            // 
            // Store
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1329, 721);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.Titlebar_panel);
            this.Controls.Add(this.Status_pnl);
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
            this.Titlebar_panel.ResumeLayout(false);
            this.search_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Titlebar_img)).EndInit();
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
            this.tabs_preview.ResumeLayout(false);
            this.TabPage4.ResumeLayout(false);
            this.pnl_preview.ResumeLayout(false);
            this.Window1.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.pnl_preview_classic.ResumeLayout(false);
            this.ClassicTaskbar.ResumeLayout(false);
            this.ClassicColorsPreview.ResumeLayout(false);
            this.ClassicColorsPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RetroShadow1)).EndInit();
            this.Menu_Window.ResumeLayout(false);
            this.menucontainer3.ResumeLayout(false);
            this.highlight.ResumeLayout(false);
            this.menuhilight.ResumeLayout(false);
            this.menucontainer1.ResumeLayout(false);
            this.WindowR3.ResumeLayout(false);
            this.WindowR3.PerformLayout();
            this.WindowR2.ResumeLayout(false);
            this.menucontainer0.ResumeLayout(false);
            this.PanelR1.ResumeLayout(false);
            this.WindowR4.ResumeLayout(false);
            this.programcontainer.ResumeLayout(false);
            this.PanelR2.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Cursors_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal UI.WP.TablessControl Tabs;
        internal Panel pnl_preview;
        internal UI.WP.AlertBox WXP_Alert2;
        internal UI.Simulation.WinElement ActionCenter;
        internal UI.Simulation.WinElement start;
        internal UI.Simulation.WinElement taskbar;
        internal UI.Simulation.Window Window2;
        internal UI.Simulation.Window Window1;
        internal Panel Panel3;
        internal UI.WP.LabelAlt Label8;
        internal UI.WP.LabelAlt setting_icon_preview;
        internal UI.WP.LabelAlt lnk_preview;
        internal Panel pnl_preview_classic;
        internal UI.Retro.WindowR ClassicWindow1;
        internal UI.Retro.WindowR ClassicWindow2;
        internal UI.Retro.PanelRaisedR ClassicTaskbar;
        internal UI.Retro.ButtonR ButtonR4;
        internal UI.Retro.ButtonR ButtonR3;
        internal UI.Retro.ButtonR ButtonR2;
        internal TabPage TabPage1;
        internal FlowLayoutPanel store_container;
        internal TabPage TabPage3;
        internal UI.WP.Button back_btn;
        internal System.ComponentModel.BackgroundWorker FilesFetcher;
        internal Panel Titlebar_panel;
        internal UI.WP.LabelAlt Titlebar_lbl;
        internal PictureBox Titlebar_img;
        internal Label themeSize_lbl;
        internal Label Label14;
        internal UI.WP.GroupBox previewContainer;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal Label SupportedOS_lbl;
        internal Label Label26;
        internal PictureBox PictureBox14;
        internal UI.WP.Button Apply_btn;
        internal Panel ClassicColorsPreview;
        internal UI.WP.TransparentPictureBox RetroShadow1;
        internal UI.Retro.WindowR Menu_Window;
        internal Panel menucontainer3;
        internal UI.WP.LabelAlt LabelR9;
        internal Panel highlight;
        internal Panel menuhilight;
        internal UI.WP.LabelAlt LabelR5;
        internal Panel menucontainer1;
        internal UI.WP.LabelAlt LabelR6;
        internal UI.Retro.WindowR WindowR3;
        internal UI.Retro.ButtonR ButtonR5;
        internal UI.WP.LabelAlt LabelR4;
        internal UI.WP.LabelAlt LabelR13;
        internal UI.Retro.WindowR WindowR2;
        internal UI.Retro.TextBoxR TextBoxR1;
        internal Panel menucontainer0;
        internal UI.Retro.PanelR PanelR1;
        internal UI.WP.LabelAlt LabelR3;
        internal UI.WP.LabelAlt LabelR2;
        internal UI.WP.LabelAlt LabelR1;
        internal UI.Retro.WindowR WindowR1;
        internal UI.Retro.WindowR WindowR4;
        internal Panel programcontainer;
        internal UI.Retro.ScrollBarR PanelR2;
        internal UI.Retro.ButtonR ButtonR12;
        internal UI.Retro.ButtonR ButtonR11;
        internal UI.Retro.ButtonR ButtonR10;
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
        internal UI.WP.Button cur_tip_btn;
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
        internal UI.WP.Trackbar CursorsSize_Bar;
        internal Timer Cursor_Timer;
        internal Panel search_panel;
        internal ProgressBar ProgressBar1;
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
        internal UI.WP.TablessControl tabs_preview;
        internal TabPage TabPage4;
        internal TabPage TabPage6;
        internal UI.WP.TextBox desc_txt;
        internal UI.WP.AlertBox VersionAlert_lbl;
        internal UI.WP.Button author_url_button;
        internal UI.WP.Button Button1;
        internal Label Theme_MD5_lbl;
    }
}