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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Store));
            FilesFetcher = new System.ComponentModel.BackgroundWorker();
            FilesFetcher.DoWork += new System.ComponentModel.DoWorkEventHandler(FilesFetcher_DoWork);
            FilesFetcher.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(FilesFetcher_ProgressChanged);
            FilesFetcher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(FilesFetcher_RunWorkerCompleted);
            Titlebar_panel = new Panel();
            Titlebar_panel.MouseDown += new MouseEventHandler(CustomTitlebar_MouseDown);
            Titlebar_panel.MouseMove += new MouseEventHandler(CustomTitlebar_MouseMove);
            search_panel = new Panel();
            search_btn = new UI.WP.Button();
            search_btn.Click += new EventHandler(Search_btn_Click);
            search_box = new UI.WP.TextBox();
            search_box.KeyboardPress += new UI.WP.TextBox.KeyboardPressEventHandler(Search_box_KeyPress);
            search_filter_btn = new UI.WP.Button();
            search_filter_btn.Click += new EventHandler(Search_filter_btn_Click);
            Titlebar_lbl = new UI.WP.LabelAlt();
            Titlebar_lbl.MouseDown += new MouseEventHandler(CustomTitlebar_MouseDown);
            Titlebar_lbl.MouseMove += new MouseEventHandler(CustomTitlebar_MouseMove);
            back_btn = new UI.WP.Button();
            back_btn.Click += new EventHandler(Back_btn_Click);
            Titlebar_img = new PictureBox();
            Titlebar_img.MouseDown += new MouseEventHandler(CustomTitlebar_MouseDown);
            Titlebar_img.MouseMove += new MouseEventHandler(CustomTitlebar_MouseMove);
            ProgressBar1 = new ProgressBar();
            Log_Timer = new Timer(components);
            Log_Timer.Tick += new EventHandler(Log_Timer_Tick);
            Cursor_Timer = new Timer(components);
            Cursor_Timer.Tick += new EventHandler(Cursor_Timer_Tick);
            Status_pnl = new Panel();
            Status_lbl = new Label();
            Tabs = new UI.WP.TablessControl();
            Tabs.SelectedIndexChanged += new EventHandler(Tabs_SelectedIndexChanged);
            TabPage1 = new TabPage();
            store_container = new FlowLayoutPanel();
            TabPage3 = new TabPage();
            VersionAlert_lbl = new UI.WP.AlertBox();
            GroupBox3 = new UI.WP.GroupBox();
            SupportedOS_lbl = new Label();
            Label26 = new Label();
            PictureBox14 = new PictureBox();
            GroupBox1 = new UI.WP.GroupBox();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Theme_MD5_lbl = new Label();
            desc_txt = new UI.WP.TextBox();
            author_url_button = new UI.WP.Button();
            author_url_button.Click += new EventHandler(Author_url_button_Click);
            RestartExplorer = new UI.WP.Button();
            RestartExplorer.Click += new EventHandler(RestartExplorer_Click);
            SeparatorVertical1 = new UI.WP.SeparatorV();
            StoreItem1 = new UI.Controllers.StoreItem();
            themeSize_lbl = new Label();
            Label14 = new Label();
            Apply_btn = new UI.WP.Button();
            Apply_btn.Click += new EventHandler(Apply_Edit_btn_Click);
            Label6 = new Label();
            Edit_btn = new UI.WP.Button();
            Edit_btn.Click += new EventHandler(Apply_Edit_btn_Click);
            respacksize_lbl = new Label();
            previewContainer = new UI.WP.GroupBox();
            PictureBox41 = new PictureBox();
            Label19 = new Label();
            FlowLayoutPanel1 = new FlowLayoutPanel();
            tabs_preview = new UI.WP.TablessControl();
            TabPage4 = new TabPage();
            pnl_preview = new Panel();
            WXP_Alert2 = new UI.WP.AlertBox();
            ActionCenter = new UI.Simulation.WinElement();
            start = new UI.Simulation.WinElement();
            taskbar = new UI.Simulation.WinElement();
            Window2 = new UI.Simulation.Window();
            Window1 = new UI.Simulation.Window();
            Panel3 = new Panel();
            Label8 = new UI.WP.LabelAlt();
            setting_icon_preview = new UI.WP.LabelAlt();
            lnk_preview = new UI.WP.LabelAlt();
            TabPage6 = new TabPage();
            pnl_preview_classic = new Panel();
            ClassicWindow1 = new UI.Retro.WindowR();
            ClassicWindow2 = new UI.Retro.WindowR();
            ClassicTaskbar = new UI.Retro.PanelRaisedR();
            ButtonR4 = new UI.Retro.ButtonR();
            ButtonR3 = new UI.Retro.ButtonR();
            ButtonR2 = new UI.Retro.ButtonR();
            ClassicColorsPreview = new Panel();
            RetroShadow1 = new UI.WP.TransparentPictureBox();
            Menu_Window = new UI.Retro.WindowR();
            Menu_Window.SizeChanged += new EventHandler(Menu_Window_SizeChanged);
            Menu_Window.LocationChanged += new EventHandler(Menu_Window_SizeChanged);
            menucontainer3 = new Panel();
            LabelR9 = new UI.WP.LabelAlt();
            highlight = new Panel();
            menuhilight = new Panel();
            LabelR5 = new UI.WP.LabelAlt();
            menucontainer1 = new Panel();
            LabelR6 = new UI.WP.LabelAlt();
            WindowR3 = new UI.Retro.WindowR();
            ButtonR5 = new UI.Retro.ButtonR();
            LabelR4 = new UI.WP.LabelAlt();
            LabelR13 = new UI.WP.LabelAlt();
            WindowR2 = new UI.Retro.WindowR();
            TextBoxR1 = new UI.Retro.TextBoxR();
            menucontainer0 = new Panel();
            PanelR1 = new UI.Retro.PanelR();
            LabelR3 = new UI.WP.LabelAlt();
            LabelR2 = new UI.WP.LabelAlt();
            LabelR1 = new UI.WP.LabelAlt();
            WindowR1 = new UI.Retro.WindowR();
            WindowR4 = new UI.Retro.WindowR();
            programcontainer = new Panel();
            PanelR2 = new UI.Retro.ScrollBarR();
            ButtonR12 = new UI.Retro.ButtonR();
            ButtonR11 = new UI.Retro.ButtonR();
            ButtonR10 = new UI.Retro.ButtonR();
            CMD1 = new UI.Simulation.WinCMD();
            CMD2 = new UI.Simulation.WinCMD();
            CMD3 = new UI.Simulation.WinCMD();
            Panel1 = new Panel();
            cur_anim_btn = new UI.WP.Button();
            cur_anim_btn.Click += new EventHandler(Cur_anim_btn_Click);
            Cursors_Container = new FlowLayoutPanel();
            Arrow = new CursorControl();
            Help = new CursorControl();
            AppLoading = new CursorControl();
            Busy = new CursorControl();
            Move_Cur = new CursorControl();
            Up = new CursorControl();
            NS = new CursorControl();
            EW = new CursorControl();
            NESW = new CursorControl();
            NWSE = new CursorControl();
            Pen = new CursorControl();
            None = new CursorControl();
            Link = new CursorControl();
            Pin = new CursorControl();
            Person = new CursorControl();
            IBeam = new CursorControl();
            Cross = new CursorControl();
            cur_tip_btn = new UI.WP.Button();
            cur_tip_btn.Click += new EventHandler(Cur_tip_btn_Click);
            PictureBox12 = new PictureBox();
            CursorsSize_Bar = new UI.WP.Trackbar();
            CursorsSize_Bar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(CursorsSize_Bar_Scroll);
            Label17 = new Label();
            TabPage5 = new TabPage();
            search_results = new FlowLayoutPanel();
            TabPage2 = new TabPage();
            StopTimer_btn = new UI.WP.Button();
            StopTimer_btn.Click += new EventHandler(StopTimer_btn_Click);
            ExportDetails_btn = new UI.WP.Button();
            ExportDetails_btn.Click += new EventHandler(ExportDetails_btn_Click);
            log_lbl = new Label();
            ShowErrors_btn = new UI.WP.Button();
            ShowErrors_btn.Click += new EventHandler(ShowErrors_btn_Click);
            ok_btn = new UI.WP.Button();
            ok_btn.Click += new EventHandler(Ok_btn_Click);
            log = new TreeView();
            Separator1 = new UI.WP.SeparatorH();
            log_header = new Label();
            PictureBox36 = new PictureBox();
            Titlebar_panel.SuspendLayout();
            search_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Titlebar_img).BeginInit();
            Status_pnl.SuspendLayout();
            Tabs.SuspendLayout();
            TabPage1.SuspendLayout();
            TabPage3.SuspendLayout();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            GroupBox1.SuspendLayout();
            previewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            FlowLayoutPanel1.SuspendLayout();
            tabs_preview.SuspendLayout();
            TabPage4.SuspendLayout();
            pnl_preview.SuspendLayout();
            Window1.SuspendLayout();
            Panel3.SuspendLayout();
            TabPage6.SuspendLayout();
            pnl_preview_classic.SuspendLayout();
            ClassicTaskbar.SuspendLayout();
            ClassicColorsPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RetroShadow1).BeginInit();
            Menu_Window.SuspendLayout();
            menucontainer3.SuspendLayout();
            highlight.SuspendLayout();
            menuhilight.SuspendLayout();
            menucontainer1.SuspendLayout();
            WindowR3.SuspendLayout();
            WindowR2.SuspendLayout();
            menucontainer0.SuspendLayout();
            PanelR1.SuspendLayout();
            WindowR4.SuspendLayout();
            programcontainer.SuspendLayout();
            PanelR2.SuspendLayout();
            Panel1.SuspendLayout();
            Cursors_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            TabPage5.SuspendLayout();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).BeginInit();
            SuspendLayout();
            // 
            // FilesFetcher
            // 
            FilesFetcher.WorkerReportsProgress = true;
            FilesFetcher.WorkerSupportsCancellation = true;
            // 
            // Titlebar_panel
            // 
            Titlebar_panel.Controls.Add(search_panel);
            Titlebar_panel.Controls.Add(Titlebar_lbl);
            Titlebar_panel.Controls.Add(back_btn);
            Titlebar_panel.Controls.Add(Titlebar_img);
            Titlebar_panel.Dock = DockStyle.Top;
            Titlebar_panel.Location = new Point(0, 0);
            Titlebar_panel.Name = "Titlebar_panel";
            Titlebar_panel.Size = new Size(1329, 70);
            Titlebar_panel.TabIndex = 5;
            // 
            // search_panel
            // 
            search_panel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            search_panel.BackColor = Color.Transparent;
            search_panel.Controls.Add(search_btn);
            search_panel.Controls.Add(search_box);
            search_panel.Controls.Add(search_filter_btn);
            search_panel.Location = new Point(979, 17);
            search_panel.Name = "search_panel";
            search_panel.Size = new Size(343, 30);
            search_panel.TabIndex = 42;
            // 
            // search_btn
            // 
            search_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            search_btn.BackColor = Color.FromArgb(34, 34, 34);
            search_btn.DrawOnGlass = true;
            search_btn.Font = new Font("Segoe UI", 9.0f);
            search_btn.ForeColor = Color.White;
            search_btn.Image = (Image)resources.GetObject("search_btn.Image");
            search_btn.LineColor = Color.FromArgb(59, 111, 122);
            search_btn.Location = new Point(308, 3);
            search_btn.Name = "search_btn";
            search_btn.Size = new Size(32, 24);
            search_btn.TabIndex = 40;
            search_btn.UseVisualStyleBackColor = false;
            // 
            // search_box
            // 
            search_box.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            search_box.BackColor = Color.FromArgb(55, 55, 55);
            search_box.DrawOnGlass = true;
            search_box.ForeColor = Color.FromArgb(255, 255, 255);
            search_box.Location = new Point(3, 3);
            search_box.MaxLength = 32767;
            search_box.Multiline = false;
            search_box.Name = "search_box";
            search_box.ReadOnly = false;
            search_box.Scrollbars = ScrollBars.None;
            search_box.SelectedText = "";
            search_box.SelectionLength = 0;
            search_box.SelectionStart = 0;
            search_box.Size = new Size(261, 24);
            search_box.TabIndex = 39;
            search_box.TextAlign = HorizontalAlignment.Left;
            search_box.UseSystemPasswordChar = false;
            search_box.WordWrap = true;
            // 
            // search_filter_btn
            // 
            search_filter_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            search_filter_btn.BackColor = Color.FromArgb(34, 34, 34);
            search_filter_btn.DrawOnGlass = true;
            search_filter_btn.Font = new Font("Segoe UI", 9.0f);
            search_filter_btn.ForeColor = Color.White;
            search_filter_btn.Image = (Image)resources.GetObject("search_filter_btn.Image");
            search_filter_btn.LineColor = Color.FromArgb(79, 39, 28);
            search_filter_btn.Location = new Point(270, 3);
            search_filter_btn.Name = "search_filter_btn";
            search_filter_btn.Size = new Size(32, 24);
            search_filter_btn.TabIndex = 41;
            search_filter_btn.UseVisualStyleBackColor = false;
            // 
            // Titlebar_lbl
            // 
            Titlebar_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Titlebar_lbl.BackColor = Color.Transparent;
            Titlebar_lbl.DrawOnGlass = true;
            Titlebar_lbl.FlatStyle = FlatStyle.Flat;
            Titlebar_lbl.Font = new Font("Segoe UI", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Titlebar_lbl.Location = new Point(78, 1);
            Titlebar_lbl.Name = "Titlebar_lbl";
            Titlebar_lbl.Size = new Size(895, 64);
            Titlebar_lbl.TabIndex = 38;
            Titlebar_lbl.Text = "WinPaletter Store";
            Titlebar_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // back_btn
            // 
            back_btn.BackColor = Color.FromArgb(34, 34, 34);
            back_btn.DrawOnGlass = true;
            back_btn.Font = new Font("Segoe UI", 9.0f);
            back_btn.ForeColor = Color.White;
            back_btn.Image = null;
            back_btn.LineColor = Color.FromArgb(47, 47, 47);
            back_btn.Location = new Point(8, 0);
            back_btn.Name = "back_btn";
            back_btn.Size = new Size(64, 64);
            back_btn.TabIndex = 36;
            back_btn.UseVisualStyleBackColor = false;
            back_btn.Visible = false;
            // 
            // Titlebar_img
            // 
            Titlebar_img.BackColor = Color.Transparent;
            Titlebar_img.Image = (Image)resources.GetObject("Titlebar_img.Image");
            Titlebar_img.Location = new Point(5, 0);
            Titlebar_img.Name = "Titlebar_img";
            Titlebar_img.Size = new Size(64, 64);
            Titlebar_img.TabIndex = 37;
            Titlebar_img.TabStop = false;
            // 
            // ProgressBar1
            // 
            ProgressBar1.Dock = DockStyle.Right;
            ProgressBar1.Location = new Point(1098, 3);
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(228, 18);
            ProgressBar1.TabIndex = 43;
            ProgressBar1.Visible = false;
            // 
            // Log_Timer
            // 
            Log_Timer.Interval = 1000;
            // 
            // Cursor_Timer
            // 
            Cursor_Timer.Interval = 35;
            // 
            // Status_pnl
            // 
            Status_pnl.BackColor = Color.Transparent;
            Status_pnl.Controls.Add(Status_lbl);
            Status_pnl.Controls.Add(ProgressBar1);
            Status_pnl.Dock = DockStyle.Bottom;
            Status_pnl.Location = new Point(0, 697);
            Status_pnl.Name = "Status_pnl";
            Status_pnl.Padding = new Padding(3);
            Status_pnl.Size = new Size(1329, 24);
            Status_pnl.TabIndex = 6;
            // 
            // Status_lbl
            // 
            Status_lbl.AutoEllipsis = true;
            Status_lbl.BackColor = Color.Transparent;
            Status_lbl.Dock = DockStyle.Fill;
            Status_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Status_lbl.Location = new Point(3, 3);
            Status_lbl.Name = "Status_lbl";
            Status_lbl.Size = new Size(1095, 18);
            Status_lbl.TabIndex = 39;
            Status_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Tabs
            // 
            Tabs.Controls.Add(TabPage1);
            Tabs.Controls.Add(TabPage3);
            Tabs.Controls.Add(TabPage5);
            Tabs.Controls.Add(TabPage2);
            Tabs.Dock = DockStyle.Fill;
            Tabs.Location = new Point(0, 70);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            Tabs.Size = new Size(1329, 627);
            Tabs.TabIndex = 4;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(store_container);
            TabPage1.Location = new Point(4, 24);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(10);
            TabPage1.Size = new Size(1321, 599);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "0";
            // 
            // store_container
            // 
            store_container.AutoScroll = true;
            store_container.Dock = DockStyle.Fill;
            store_container.Location = new Point(10, 10);
            store_container.Name = "store_container";
            store_container.Size = new Size(1301, 579);
            store_container.TabIndex = 3;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(VersionAlert_lbl);
            TabPage3.Controls.Add(GroupBox3);
            TabPage3.Controls.Add(GroupBox1);
            TabPage3.Controls.Add(previewContainer);
            TabPage3.Location = new Point(4, 24);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(10);
            TabPage3.Size = new Size(1321, 599);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "1";
            // 
            // VersionAlert_lbl
            // 
            VersionAlert_lbl.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            VersionAlert_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            VersionAlert_lbl.BackColor = Color.FromArgb(91, 72, 6);
            VersionAlert_lbl.CenterText = true;
            VersionAlert_lbl.CustomColor = Color.FromArgb(0, 81, 210);
            VersionAlert_lbl.Font = new Font("Segoe UI", 9.0f);
            VersionAlert_lbl.Image = (Image)resources.GetObject("VersionAlert_lbl.Image");
            VersionAlert_lbl.Location = new Point(399, 450);
            VersionAlert_lbl.Name = "VersionAlert_lbl";
            VersionAlert_lbl.Size = new Size(908, 34);
            VersionAlert_lbl.TabIndex = 140;
            VersionAlert_lbl.TabStop = false;
            VersionAlert_lbl.Text = "0";
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(SupportedOS_lbl);
            GroupBox3.Controls.Add(Label26);
            GroupBox3.Controls.Add(PictureBox14);
            GroupBox3.Location = new Point(399, 389);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(908, 55);
            GroupBox3.TabIndex = 140;
            GroupBox3.Text = "GroupBox3";
            // 
            // SupportedOS_lbl
            // 
            SupportedOS_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SupportedOS_lbl.BackColor = Color.Transparent;
            SupportedOS_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            SupportedOS_lbl.Location = new Point(33, 27);
            SupportedOS_lbl.Name = "SupportedOS_lbl";
            SupportedOS_lbl.Size = new Size(871, 24);
            SupportedOS_lbl.TabIndex = 8;
            SupportedOS_lbl.Text = "0";
            SupportedOS_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label26
            // 
            Label26.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label26.BackColor = Color.Transparent;
            Label26.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label26.Location = new Point(33, 3);
            Label26.Name = "Label26";
            Label26.Size = new Size(871, 24);
            Label26.TabIndex = 4;
            Label26.Text = "0";
            Label26.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox14
            // 
            PictureBox14.BackColor = Color.Transparent;
            PictureBox14.Image = (Image)resources.GetObject("PictureBox14.Image");
            PictureBox14.Location = new Point(3, 3);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox14.TabIndex = 0;
            PictureBox14.TabStop = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(Button1);
            GroupBox1.Controls.Add(Theme_MD5_lbl);
            GroupBox1.Controls.Add(desc_txt);
            GroupBox1.Controls.Add(author_url_button);
            GroupBox1.Controls.Add(RestartExplorer);
            GroupBox1.Controls.Add(SeparatorVertical1);
            GroupBox1.Controls.Add(StoreItem1);
            GroupBox1.Controls.Add(themeSize_lbl);
            GroupBox1.Controls.Add(Label14);
            GroupBox1.Controls.Add(Apply_btn);
            GroupBox1.Controls.Add(Label6);
            GroupBox1.Controls.Add(Edit_btn);
            GroupBox1.Controls.Add(respacksize_lbl);
            GroupBox1.Location = new Point(13, 13);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(380, 573);
            GroupBox1.TabIndex = 139;
            GroupBox1.Text = "GroupBox1";
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Top;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleRight;
            Button1.LineColor = Color.FromArgb(75, 111, 122);
            Button1.Location = new Point(193, 234);
            Button1.Name = "Button1";
            Button1.Size = new Size(156, 34);
            Button1.TabIndex = 146;
            Button1.Text = "Save as ...";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Theme_MD5_lbl
            // 
            Theme_MD5_lbl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Theme_MD5_lbl.BackColor = Color.Transparent;
            Theme_MD5_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Theme_MD5_lbl.Location = new Point(14, 537);
            Theme_MD5_lbl.Name = "Theme_MD5_lbl";
            Theme_MD5_lbl.Size = new Size(322, 24);
            Theme_MD5_lbl.TabIndex = 145;
            Theme_MD5_lbl.Text = "0";
            Theme_MD5_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // desc_txt
            // 
            desc_txt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            desc_txt.BackColor = Color.FromArgb(55, 55, 55);
            desc_txt.DrawOnGlass = false;
            desc_txt.ForeColor = Color.White;
            desc_txt.Location = new Point(14, 382);
            desc_txt.MaxLength = 32767;
            desc_txt.Multiline = true;
            desc_txt.Name = "desc_txt";
            desc_txt.ReadOnly = true;
            desc_txt.Scrollbars = ScrollBars.Vertical;
            desc_txt.SelectedText = "";
            desc_txt.SelectionLength = 0;
            desc_txt.SelectionStart = 0;
            desc_txt.Size = new Size(352, 145);
            desc_txt.TabIndex = 7;
            desc_txt.TextAlign = HorizontalAlignment.Left;
            desc_txt.UseSystemPasswordChar = false;
            desc_txt.WordWrap = true;
            // 
            // author_url_button
            // 
            author_url_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            author_url_button.BackColor = Color.FromArgb(50, 50, 50);
            author_url_button.DrawOnGlass = false;
            author_url_button.Font = new Font("Segoe UI", 9.0f);
            author_url_button.ForeColor = Color.White;
            author_url_button.Image = null;
            author_url_button.LineColor = Color.FromArgb(0, 81, 210);
            author_url_button.Location = new Point(342, 537);
            author_url_button.Name = "author_url_button";
            author_url_button.Size = new Size(24, 24);
            author_url_button.TabIndex = 144;
            author_url_button.Text = "↗";
            author_url_button.UseVisualStyleBackColor = false;
            // 
            // RestartExplorer
            // 
            RestartExplorer.Anchor = AnchorStyles.Top;
            RestartExplorer.BackColor = Color.FromArgb(34, 34, 34);
            RestartExplorer.DrawOnGlass = false;
            RestartExplorer.Font = new Font("Segoe UI", 9.0f);
            RestartExplorer.ForeColor = Color.White;
            RestartExplorer.Image = null;
            RestartExplorer.ImageAlign = ContentAlignment.MiddleRight;
            RestartExplorer.LineColor = Color.FromArgb(112, 127, 79);
            RestartExplorer.Location = new Point(31, 274);
            RestartExplorer.Name = "RestartExplorer";
            RestartExplorer.Size = new Size(318, 34);
            RestartExplorer.TabIndex = 138;
            RestartExplorer.Text = "Restart Explorer";
            RestartExplorer.UseVisualStyleBackColor = false;
            // 
            // SeparatorVertical1
            // 
            SeparatorVertical1.AlternativeLook = false;
            SeparatorVertical1.Anchor = AnchorStyles.Top;
            SeparatorVertical1.Location = new Point(190, 320);
            SeparatorVertical1.Name = "SeparatorVertical1";
            SeparatorVertical1.Size = new Size(1, 46);
            SeparatorVertical1.TabIndex = 143;
            SeparatorVertical1.TabStop = false;
            SeparatorVertical1.Text = "SeparatorVertical1";
            // 
            // StoreItem1
            // 
            StoreItem1.Anchor = AnchorStyles.Top;
            StoreItem1.TM = null;
            StoreItem1.DoneByWinPaletter = false;
            StoreItem1.FileName = null;
            StoreItem1.Font = new Font("Segoe UI", 9.0f);
            StoreItem1.Location = new Point(32, 7);
            StoreItem1.MD5_PackFile = null;
            StoreItem1.MD5_ThemeFile = null;
            StoreItem1.Name = "StoreItem1";
            StoreItem1.Size = new Size(317, 178);
            StoreItem1.TabIndex = 142;
            StoreItem1.URL_PackFile = null;
            StoreItem1.URL_ThemeFile = null;
            // 
            // themeSize_lbl
            // 
            themeSize_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            themeSize_lbl.BackColor = Color.Transparent;
            themeSize_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            themeSize_lbl.Location = new Point(46, 320);
            themeSize_lbl.Name = "themeSize_lbl";
            themeSize_lbl.Size = new Size(138, 24);
            themeSize_lbl.TabIndex = 13;
            themeSize_lbl.Text = "0";
            themeSize_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label14
            // 
            Label14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label14.BackColor = Color.Transparent;
            Label14.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label14.Location = new Point(46, 342);
            Label14.Name = "Label14";
            Label14.Size = new Size(138, 24);
            Label14.TabIndex = 6;
            Label14.Text = "Size";
            Label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Apply_btn
            // 
            Apply_btn.Anchor = AnchorStyles.Top;
            Apply_btn.BackColor = Color.FromArgb(34, 34, 34);
            Apply_btn.DrawOnGlass = false;
            Apply_btn.Font = new Font("Segoe UI", 9.0f);
            Apply_btn.ForeColor = Color.White;
            Apply_btn.Image = null;
            Apply_btn.ImageAlign = ContentAlignment.MiddleRight;
            Apply_btn.LineColor = Color.FromArgb(95, 96, 96);
            Apply_btn.Location = new Point(31, 194);
            Apply_btn.Name = "Apply_btn";
            Apply_btn.Size = new Size(318, 34);
            Apply_btn.TabIndex = 134;
            Apply_btn.Text = "Apply";
            Apply_btn.UseVisualStyleBackColor = false;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.Location = new Point(197, 342);
            Label6.Name = "Label6";
            Label6.Size = new Size(138, 24);
            Label6.TabIndex = 15;
            Label6.Text = "Resources pack size";
            Label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Edit_btn
            // 
            Edit_btn.Anchor = AnchorStyles.Top;
            Edit_btn.BackColor = Color.FromArgb(34, 34, 34);
            Edit_btn.DrawOnGlass = false;
            Edit_btn.Font = new Font("Segoe UI", 9.0f);
            Edit_btn.ForeColor = Color.White;
            Edit_btn.Image = (Image)resources.GetObject("Edit_btn.Image");
            Edit_btn.ImageAlign = ContentAlignment.MiddleRight;
            Edit_btn.LineColor = Color.FromArgb(48, 39, 12);
            Edit_btn.Location = new Point(31, 234);
            Edit_btn.Name = "Edit_btn";
            Edit_btn.Size = new Size(156, 34);
            Edit_btn.TabIndex = 137;
            Edit_btn.Text = "Edit";
            Edit_btn.UseVisualStyleBackColor = false;
            // 
            // respacksize_lbl
            // 
            respacksize_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            respacksize_lbl.BackColor = Color.Transparent;
            respacksize_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            respacksize_lbl.Location = new Point(197, 320);
            respacksize_lbl.Name = "respacksize_lbl";
            respacksize_lbl.Size = new Size(138, 24);
            respacksize_lbl.TabIndex = 16;
            respacksize_lbl.Text = "0";
            respacksize_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // previewContainer
            // 
            previewContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            previewContainer.BackColor = Color.FromArgb(34, 34, 34);
            previewContainer.Controls.Add(PictureBox41);
            previewContainer.Controls.Add(Label19);
            previewContainer.Controls.Add(FlowLayoutPanel1);
            previewContainer.Location = new Point(399, 13);
            previewContainer.Margin = new Padding(4, 3, 4, 3);
            previewContainer.Name = "previewContainer";
            previewContainer.Padding = new Padding(1);
            previewContainer.Size = new Size(908, 370);
            previewContainer.TabIndex = 131;
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
            Label19.Size = new Size(859, 31);
            Label19.TabIndex = 3;
            Label19.Text = "Preview";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FlowLayoutPanel1
            // 
            FlowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FlowLayoutPanel1.AutoScroll = true;
            FlowLayoutPanel1.Controls.Add(tabs_preview);
            FlowLayoutPanel1.Controls.Add(ClassicColorsPreview);
            FlowLayoutPanel1.Controls.Add(CMD1);
            FlowLayoutPanel1.Controls.Add(CMD2);
            FlowLayoutPanel1.Controls.Add(CMD3);
            FlowLayoutPanel1.Controls.Add(Panel1);
            FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            FlowLayoutPanel1.Location = new Point(4, 41);
            FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            FlowLayoutPanel1.Size = new Size(900, 324);
            FlowLayoutPanel1.TabIndex = 139;
            // 
            // tabs_preview
            // 
            tabs_preview.Controls.Add(TabPage4);
            tabs_preview.Controls.Add(TabPage6);
            tabs_preview.Location = new Point(3, 3);
            tabs_preview.Name = "tabs_preview";
            tabs_preview.SelectedIndex = 0;
            tabs_preview.Size = new Size(528, 297);
            tabs_preview.TabIndex = 140;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(pnl_preview);
            TabPage4.Location = new Point(4, 24);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(520, 269);
            TabPage4.TabIndex = 0;
            TabPage4.Text = "0";
            // 
            // pnl_preview
            // 
            pnl_preview.BackColor = Color.Black;
            pnl_preview.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview.BorderStyle = BorderStyle.FixedSingle;
            pnl_preview.Controls.Add(WXP_Alert2);
            pnl_preview.Controls.Add(ActionCenter);
            pnl_preview.Controls.Add(start);
            pnl_preview.Controls.Add(taskbar);
            pnl_preview.Controls.Add(Window2);
            pnl_preview.Controls.Add(Window1);
            pnl_preview.Location = new Point(0, 0);
            pnl_preview.Name = "pnl_preview";
            pnl_preview.Size = new Size(528, 297);
            pnl_preview.TabIndex = 2;
            // 
            // WXP_Alert2
            // 
            WXP_Alert2.AlertStyle = UI.WP.AlertBox.Style.Warning;
            WXP_Alert2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WXP_Alert2.BackColor = Color.FromArgb(125, 20, 30);
            WXP_Alert2.CenterText = true;
            WXP_Alert2.CustomColor = Color.FromArgb(0, 81, 210);
            WXP_Alert2.Font = new Font("Segoe UI", 9.0f);
            WXP_Alert2.Image = null;
            WXP_Alert2.Location = new Point(7, 6);
            WXP_Alert2.Name = "WXP_Alert2";
            WXP_Alert2.Size = new Size(22, 22);
            WXP_Alert2.TabIndex = 54;
            WXP_Alert2.TabStop = false;
            WXP_Alert2.Text = null;
            WXP_Alert2.Visible = false;
            // 
            // ActionCenter
            // 
            ActionCenter.ActionCenterButton_Hover = Color.Empty;
            ActionCenter.ActionCenterButton_Normal = Color.FromArgb(0, 120, 212);
            ActionCenter.ActionCenterButton_Pressed = Color.Empty;
            ActionCenter.AppBackground = Color.Empty;
            ActionCenter.AppUnderline = Color.Empty;
            ActionCenter.BackColor = Color.Transparent;
            ActionCenter.BackColorAlpha = 50;
            ActionCenter.Background = Color.Empty;
            ActionCenter.Background2 = Color.Empty;
            ActionCenter.BlurPower = 8;
            ActionCenter.DarkMode = true;
            ActionCenter.LinkColor = Color.Empty;
            ActionCenter.Location = new Point(400, 165);
            ActionCenter.Name = "ActionCenter";
            ActionCenter.NoisePower = 0.2f;
            ActionCenter.Padding = new Padding(2);
            ActionCenter.Shadow = true;
            ActionCenter.Size = new Size(120, 85);
            ActionCenter.StartColor = Color.Empty;
            ActionCenter.Style = UI.Simulation.WinElement.Styles.ActionCenter11;
            ActionCenter.SuspendRefresh = false;
            ActionCenter.TabIndex = 5;
            ActionCenter.Transparency = true;
            ActionCenter.UseWin11ORB_WithWin10 = false;
            ActionCenter.UseWin11RoundedCorners_WithWin10_Level1 = false;
            ActionCenter.UseWin11RoundedCorners_WithWin10_Level2 = false;
            ActionCenter.Win7ColorBal = 100;
            ActionCenter.Win7GlowBal = 100;
            // 
            // start
            // 
            start.ActionCenterButton_Hover = Color.Empty;
            start.ActionCenterButton_Normal = Color.Empty;
            start.ActionCenterButton_Pressed = Color.Empty;
            start.AppBackground = Color.Empty;
            start.AppUnderline = Color.Empty;
            start.BackColor = Color.Transparent;
            start.BackColorAlpha = 150;
            start.Background = Color.Empty;
            start.Background2 = Color.Empty;
            start.BlurPower = 7;
            start.DarkMode = true;
            start.LinkColor = Color.Empty;
            start.Location = new Point(7, 50);
            start.Name = "start";
            start.NoisePower = 0.2f;
            start.Padding = new Padding(2);
            start.Shadow = true;
            start.Size = new Size(135, 200);
            start.StartColor = Color.Empty;
            start.Style = UI.Simulation.WinElement.Styles.Start11;
            start.SuspendRefresh = false;
            start.TabIndex = 1;
            start.Transparency = true;
            start.UseWin11ORB_WithWin10 = false;
            start.UseWin11RoundedCorners_WithWin10_Level1 = false;
            start.UseWin11RoundedCorners_WithWin10_Level2 = false;
            start.Win7ColorBal = 100;
            start.Win7GlowBal = 100;
            // 
            // taskbar
            // 
            taskbar.ActionCenterButton_Hover = Color.Empty;
            taskbar.ActionCenterButton_Normal = Color.Empty;
            taskbar.ActionCenterButton_Pressed = Color.Empty;
            taskbar.AppBackground = Color.Empty;
            taskbar.AppUnderline = Color.Empty;
            taskbar.BackColor = Color.Transparent;
            taskbar.BackColorAlpha = 130;
            taskbar.Background = Color.Empty;
            taskbar.Background2 = Color.Empty;
            taskbar.BlurPower = 12;
            taskbar.DarkMode = true;
            taskbar.Dock = DockStyle.Bottom;
            taskbar.LinkColor = Color.Empty;
            taskbar.Location = new Point(0, 253);
            taskbar.Name = "taskbar";
            taskbar.NoisePower = 0.2f;
            taskbar.Shadow = true;
            taskbar.Size = new Size(526, 42);
            taskbar.StartColor = Color.Empty;
            taskbar.Style = UI.Simulation.WinElement.Styles.Taskbar11;
            taskbar.SuspendRefresh = false;
            taskbar.TabIndex = 0;
            taskbar.Transparency = true;
            taskbar.UseWin11ORB_WithWin10 = false;
            taskbar.UseWin11RoundedCorners_WithWin10_Level1 = false;
            taskbar.UseWin11RoundedCorners_WithWin10_Level2 = false;
            taskbar.Win7ColorBal = 100;
            taskbar.Win7GlowBal = 100;
            // 
            // Window2
            // 
            Window2.AccentColor_Active = Color.FromArgb(10, 10, 100);
            Window2.AccentColor_Enabled = true;
            Window2.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window2.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window2.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window2.Active = false;
            Window2.BackColor = Color.Transparent;
            Window2.DarkMode = true;
            Window2.Font = new Font("Segoe UI", 9.0f);
            Window2.Location = new Point(172, 160);
            Window2.Metrics_BorderWidth = 1;
            Window2.Metrics_CaptionHeight = 22;
            Window2.Metrics_PaddedBorderWidth = 4;
            Window2.Name = "Window2";
            Window2.Padding = new Padding(4, 40, 4, 4);
            Window2.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window2.Radius = 5;
            Window2.Shadow = true;
            Window2.Size = new Size(189, 85);
            Window2.SuspendRefresh = false;
            Window2.TabIndex = 3;
            Window2.Text = "Inactive app";
            Window2.ToolWindow = false;
            Window2.Win7Alpha = 100;
            Window2.Win7ColorBal = 100;
            Window2.Win7GlowBal = 100;
            Window2.Win7Noise = 1.0f;
            Window2.WinVista = false;
            // 
            // Window1
            // 
            Window1.AccentColor_Active = Color.FromArgb(0, 120, 212);
            Window1.AccentColor_Enabled = true;
            Window1.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window1.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window1.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window1.Active = true;
            Window1.BackColor = Color.Transparent;
            Window1.Controls.Add(Panel3);
            Window1.Controls.Add(lnk_preview);
            Window1.DarkMode = true;
            Window1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Window1.Location = new Point(172, 13);
            Window1.Metrics_BorderWidth = 1;
            Window1.Metrics_CaptionHeight = 22;
            Window1.Metrics_PaddedBorderWidth = 4;
            Window1.Name = "Window1";
            Window1.Padding = new Padding(4, 40, 4, 4);
            Window1.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window1.Radius = 5;
            Window1.Shadow = true;
            Window1.Size = new Size(189, 147);
            Window1.SuspendRefresh = false;
            Window1.TabIndex = 2;
            Window1.Text = "App preview";
            Window1.ToolWindow = false;
            Window1.Win7Alpha = 100;
            Window1.Win7ColorBal = 100;
            Window1.Win7GlowBal = 100;
            Window1.Win7Noise = 1.0f;
            Window1.WinVista = false;
            // 
            // Panel3
            // 
            Panel3.BackColor = Color.Transparent;
            Panel3.Controls.Add(Label8);
            Panel3.Controls.Add(setting_icon_preview);
            Panel3.Dock = DockStyle.Fill;
            Panel3.Location = new Point(4, 40);
            Panel3.Name = "Panel3";
            Panel3.Padding = new Padding(1);
            Panel3.Size = new Size(181, 78);
            Panel3.TabIndex = 0;
            // 
            // Label8
            // 
            Label8.BackColor = Color.Transparent;
            Label8.Dock = DockStyle.Fill;
            Label8.DrawOnGlass = false;
            Label8.Font = new Font("Segoe UI", 9.0f);
            Label8.Location = new Point(1, 46);
            Label8.Name = "Label8";
            Label8.Size = new Size(179, 31);
            Label8.TabIndex = 15;
            Label8.Text = "This is a setting icon";
            Label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // setting_icon_preview
            // 
            setting_icon_preview.BackColor = Color.Transparent;
            setting_icon_preview.Dock = DockStyle.Top;
            setting_icon_preview.DrawOnGlass = false;
            setting_icon_preview.Font = new Font("Segoe MDL2 Assets", 21.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            setting_icon_preview.Location = new Point(1, 1);
            setting_icon_preview.Name = "setting_icon_preview";
            setting_icon_preview.Size = new Size(179, 45);
            setting_icon_preview.TabIndex = 14;
            setting_icon_preview.Text = "";
            setting_icon_preview.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lnk_preview
            // 
            lnk_preview.BackColor = Color.Transparent;
            lnk_preview.Dock = DockStyle.Bottom;
            lnk_preview.DrawOnGlass = false;
            lnk_preview.Font = new Font("Segoe UI", 9.0f);
            lnk_preview.ForeColor = Color.Brown;
            lnk_preview.Location = new Point(4, 118);
            lnk_preview.Name = "lnk_preview";
            lnk_preview.Size = new Size(181, 25);
            lnk_preview.TabIndex = 16;
            lnk_preview.Text = "Settings link preview";
            lnk_preview.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(pnl_preview_classic);
            TabPage6.Location = new Point(4, 24);
            TabPage6.Name = "TabPage6";
            TabPage6.Padding = new Padding(3);
            TabPage6.Size = new Size(520, 269);
            TabPage6.TabIndex = 1;
            TabPage6.Text = "1";
            // 
            // pnl_preview_classic
            // 
            pnl_preview_classic.BackColor = Color.Black;
            pnl_preview_classic.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview_classic.BorderStyle = BorderStyle.FixedSingle;
            pnl_preview_classic.Controls.Add(ClassicWindow1);
            pnl_preview_classic.Controls.Add(ClassicWindow2);
            pnl_preview_classic.Controls.Add(ClassicTaskbar);
            pnl_preview_classic.Location = new Point(0, 0);
            pnl_preview_classic.Name = "pnl_preview_classic";
            pnl_preview_classic.Size = new Size(528, 297);
            pnl_preview_classic.TabIndex = 34;
            // 
            // ClassicWindow1
            // 
            ClassicWindow1.BackColor = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ButtonDkShadow = Color.Black;
            ClassicWindow1.ButtonFace = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ButtonHilight = Color.White;
            ClassicWindow1.ButtonLight = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ButtonShadow = Color.FromArgb(128, 128, 128);
            ClassicWindow1.ButtonText = Color.Black;
            ClassicWindow1.Color1 = Color.FromArgb(0, 0, 128);
            ClassicWindow1.Color2 = Color.FromArgb(16, 132, 208);
            ClassicWindow1.ColorBorder = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ColorGradient = true;
            ClassicWindow1.ControlBox = true;
            ClassicWindow1.Flat = false;
            ClassicWindow1.Font = new Font("Microsoft Sans Serif", 8.0f);
            ClassicWindow1.ForeColor = Color.White;
            ClassicWindow1.Location = new Point(176, 20);
            ClassicWindow1.MaximizeBox = false;
            ClassicWindow1.Metrics_BorderWidth = 0;
            ClassicWindow1.Metrics_CaptionHeight = 18;
            ClassicWindow1.Metrics_CaptionWidth = 18;
            ClassicWindow1.Metrics_PaddedBorderWidth = 0;
            ClassicWindow1.MinimizeBox = false;
            ClassicWindow1.Name = "ClassicWindow1";
            ClassicWindow1.Padding = new Padding(4, 22, 4, 4);
            ClassicWindow1.Size = new Size(181, 146);
            ClassicWindow1.TabIndex = 4;
            ClassicWindow1.Text = "App preview";
            ClassicWindow1.UseItAsMenu = false;
            // 
            // ClassicWindow2
            // 
            ClassicWindow2.BackColor = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ButtonDkShadow = Color.Black;
            ClassicWindow2.ButtonFace = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ButtonHilight = Color.White;
            ClassicWindow2.ButtonLight = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ButtonShadow = Color.FromArgb(128, 128, 128);
            ClassicWindow2.ButtonText = Color.Black;
            ClassicWindow2.Color1 = Color.FromArgb(51, 51, 51);
            ClassicWindow2.Color2 = Color.FromArgb(181, 181, 181);
            ClassicWindow2.ColorBorder = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ColorGradient = true;
            ClassicWindow2.ControlBox = true;
            ClassicWindow2.Flat = false;
            ClassicWindow2.Font = new Font("Microsoft Sans Serif", 8.0f);
            ClassicWindow2.ForeColor = Color.White;
            ClassicWindow2.Location = new Point(176, 172);
            ClassicWindow2.MaximizeBox = false;
            ClassicWindow2.Metrics_BorderWidth = 0;
            ClassicWindow2.Metrics_CaptionHeight = 18;
            ClassicWindow2.Metrics_CaptionWidth = 18;
            ClassicWindow2.Metrics_PaddedBorderWidth = 0;
            ClassicWindow2.MinimizeBox = false;
            ClassicWindow2.Name = "ClassicWindow2";
            ClassicWindow2.Padding = new Padding(4, 22, 4, 4);
            ClassicWindow2.Size = new Size(181, 60);
            ClassicWindow2.TabIndex = 5;
            ClassicWindow2.Text = "Inactive app";
            ClassicWindow2.UseItAsMenu = false;
            // 
            // ClassicTaskbar
            // 
            ClassicTaskbar.BackColor = Color.FromArgb(192, 192, 192);
            ClassicTaskbar.ButtonDkShadow = Color.FromArgb(105, 105, 105);
            ClassicTaskbar.ButtonHilight = Color.White;
            ClassicTaskbar.ButtonLight = Color.FromArgb(227, 227, 227);
            ClassicTaskbar.ButtonShadow = Color.FromArgb(128, 128, 128);
            ClassicTaskbar.Controls.Add(ButtonR4);
            ClassicTaskbar.Controls.Add(ButtonR3);
            ClassicTaskbar.Controls.Add(ButtonR2);
            ClassicTaskbar.Dock = DockStyle.Bottom;
            ClassicTaskbar.Flat = false;
            ClassicTaskbar.Font = new Font("Microsoft Sans Serif", 8.0f);
            ClassicTaskbar.ForeColor = Color.Black;
            ClassicTaskbar.Location = new Point(0, 251);
            ClassicTaskbar.Name = "ClassicTaskbar";
            ClassicTaskbar.Size = new Size(526, 44);
            ClassicTaskbar.Style2 = false;
            ClassicTaskbar.TabIndex = 0;
            ClassicTaskbar.UseItAsWin7Taskbar = true;
            // 
            // ButtonR4
            // 
            ButtonR4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonR4.AppearsAsPressed = false;
            ButtonR4.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR4.ButtonDkShadow = Color.Black;
            ButtonR4.ButtonHilight = Color.White;
            ButtonR4.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR4.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR4.FocusRectHeight = 1;
            ButtonR4.FocusRectWidth = 1;
            ButtonR4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ButtonR4.ForeColor = Color.Black;
            ButtonR4.HatchBrush = false;
            ButtonR4.Image = null;
            ButtonR4.Location = new Point(113, 4);
            ButtonR4.Name = "ButtonR4";
            ButtonR4.Size = new Size(48, 38);
            ButtonR4.TabIndex = 2;
            ButtonR4.UseItAsScrollbar = false;
            ButtonR4.UseVisualStyleBackColor = false;
            ButtonR4.WindowFrame = Color.Black;
            // 
            // ButtonR3
            // 
            ButtonR3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonR3.AppearsAsPressed = true;
            ButtonR3.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR3.ButtonDkShadow = Color.Black;
            ButtonR3.ButtonHilight = Color.White;
            ButtonR3.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR3.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR3.FocusRectHeight = 1;
            ButtonR3.FocusRectWidth = 1;
            ButtonR3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonR3.ForeColor = Color.Black;
            ButtonR3.HatchBrush = false;
            ButtonR3.Image = null;
            ButtonR3.Location = new Point(63, 4);
            ButtonR3.Name = "ButtonR3";
            ButtonR3.Size = new Size(48, 38);
            ButtonR3.TabIndex = 1;
            ButtonR3.UseItAsScrollbar = false;
            ButtonR3.UseVisualStyleBackColor = false;
            ButtonR3.WindowFrame = Color.Black;
            // 
            // ButtonR2
            // 
            ButtonR2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonR2.AppearsAsPressed = false;
            ButtonR2.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR2.ButtonDkShadow = Color.Black;
            ButtonR2.ButtonHilight = Color.White;
            ButtonR2.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR2.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR2.FocusRectHeight = 1;
            ButtonR2.FocusRectWidth = 1;
            ButtonR2.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonR2.ForeColor = Color.Black;
            ButtonR2.HatchBrush = false;
            ButtonR2.Image = null;
            ButtonR2.ImageAlign = ContentAlignment.MiddleRight;
            ButtonR2.Location = new Point(2, 4);
            ButtonR2.Name = "ButtonR2";
            ButtonR2.Size = new Size(52, 38);
            ButtonR2.TabIndex = 0;
            ButtonR2.Text = "Start";
            ButtonR2.UseItAsScrollbar = false;
            ButtonR2.UseVisualStyleBackColor = false;
            ButtonR2.WindowFrame = Color.Black;
            // 
            // ClassicColorsPreview
            // 
            ClassicColorsPreview.BackColor = Color.Teal;
            ClassicColorsPreview.BackgroundImageLayout = ImageLayout.Center;
            ClassicColorsPreview.BorderStyle = BorderStyle.FixedSingle;
            ClassicColorsPreview.Controls.Add(RetroShadow1);
            ClassicColorsPreview.Controls.Add(Menu_Window);
            ClassicColorsPreview.Controls.Add(WindowR3);
            ClassicColorsPreview.Controls.Add(LabelR13);
            ClassicColorsPreview.Controls.Add(WindowR2);
            ClassicColorsPreview.Controls.Add(WindowR1);
            ClassicColorsPreview.Controls.Add(WindowR4);
            ClassicColorsPreview.Location = new Point(537, 3);
            ClassicColorsPreview.Name = "ClassicColorsPreview";
            ClassicColorsPreview.Size = new Size(528, 297);
            ClassicColorsPreview.TabIndex = 43;
            // 
            // RetroShadow1
            // 
            RetroShadow1.BackColor = Color.Transparent;
            RetroShadow1.Location = new Point(112, 225);
            RetroShadow1.Name = "RetroShadow1";
            RetroShadow1.Size = new Size(115, 66);
            RetroShadow1.SizeMode = PictureBoxSizeMode.StretchImage;
            RetroShadow1.TabIndex = 7;
            RetroShadow1.TabStop = false;
            // 
            // Menu_Window
            // 
            Menu_Window.BackColor = Color.FromArgb(192, 192, 192);
            Menu_Window.ButtonDkShadow = Color.Black;
            Menu_Window.ButtonFace = Color.FromArgb(192, 192, 192);
            Menu_Window.ButtonHilight = Color.White;
            Menu_Window.ButtonLight = Color.FromArgb(192, 192, 192);
            Menu_Window.ButtonShadow = Color.FromArgb(128, 128, 128);
            Menu_Window.ButtonText = Color.Black;
            Menu_Window.Color1 = Color.FromArgb(0, 0, 128);
            Menu_Window.Color2 = Color.FromArgb(16, 132, 208);
            Menu_Window.ColorBorder = Color.FromArgb(192, 192, 192);
            Menu_Window.ColorGradient = true;
            Menu_Window.ControlBox = true;
            Menu_Window.Controls.Add(menucontainer3);
            Menu_Window.Controls.Add(highlight);
            Menu_Window.Controls.Add(menucontainer1);
            Menu_Window.Flat = false;
            Menu_Window.Font = new Font("Microsoft Sans Serif", 8.0f);
            Menu_Window.ForeColor = Color.Black;
            Menu_Window.Location = new Point(300, 151);
            Menu_Window.MaximizeBox = true;
            Menu_Window.Metrics_BorderWidth = 1;
            Menu_Window.Metrics_CaptionHeight = 22;
            Menu_Window.Metrics_CaptionWidth = 0;
            Menu_Window.Metrics_PaddedBorderWidth = 4;
            Menu_Window.MinimizeBox = true;
            Menu_Window.Name = "Menu_Window";
            Menu_Window.Padding = new Padding(3, 3, 5, 5);
            Menu_Window.Size = new Size(115, 66);
            Menu_Window.TabIndex = 4;
            Menu_Window.Text = "New Window";
            Menu_Window.UseItAsMenu = true;
            // 
            // menucontainer3
            // 
            menucontainer3.BackColor = Color.Transparent;
            menucontainer3.Controls.Add(LabelR9);
            menucontainer3.Dock = DockStyle.Top;
            menucontainer3.Location = new Point(3, 43);
            menucontainer3.Name = "menucontainer3";
            menucontainer3.Padding = new Padding(21, 0, 0, 0);
            menucontainer3.Size = new Size(107, 20);
            menucontainer3.TabIndex = 12;
            // 
            // LabelR9
            // 
            LabelR9.BackColor = Color.Transparent;
            LabelR9.Dock = DockStyle.Fill;
            LabelR9.DrawOnGlass = false;
            LabelR9.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR9.ForeColor = Color.DimGray;
            LabelR9.Location = new Point(21, 0);
            LabelR9.Name = "LabelR9";
            LabelR9.Padding = new Padding(0, 0, 0, 2);
            LabelR9.Size = new Size(86, 20);
            LabelR9.TabIndex = 3;
            LabelR9.Text = "Disabled item";
            LabelR9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // highlight
            // 
            highlight.BackColor = Color.Navy;
            highlight.Controls.Add(menuhilight);
            highlight.Dock = DockStyle.Top;
            highlight.Location = new Point(3, 23);
            highlight.Name = "highlight";
            highlight.Size = new Size(107, 20);
            highlight.TabIndex = 10;
            // 
            // menuhilight
            // 
            menuhilight.BackColor = Color.Navy;
            menuhilight.Controls.Add(LabelR5);
            menuhilight.Dock = DockStyle.Fill;
            menuhilight.Location = new Point(0, 0);
            menuhilight.Name = "menuhilight";
            menuhilight.Padding = new Padding(21, 0, 1, 0);
            menuhilight.Size = new Size(107, 20);
            menuhilight.TabIndex = 11;
            // 
            // LabelR5
            // 
            LabelR5.BackColor = Color.Transparent;
            LabelR5.Dock = DockStyle.Fill;
            LabelR5.DrawOnGlass = false;
            LabelR5.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR5.ForeColor = Color.White;
            LabelR5.Location = new Point(21, 0);
            LabelR5.Name = "LabelR5";
            LabelR5.Padding = new Padding(0, 0, 0, 2);
            LabelR5.Size = new Size(85, 20);
            LabelR5.TabIndex = 3;
            LabelR5.Text = "Hovered item";
            LabelR5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // menucontainer1
            // 
            menucontainer1.BackColor = Color.Transparent;
            menucontainer1.Controls.Add(LabelR6);
            menucontainer1.Dock = DockStyle.Top;
            menucontainer1.Location = new Point(3, 3);
            menucontainer1.Name = "menucontainer1";
            menucontainer1.Padding = new Padding(21, 0, 0, 0);
            menucontainer1.Size = new Size(107, 20);
            menucontainer1.TabIndex = 6;
            // 
            // LabelR6
            // 
            LabelR6.BackColor = Color.Transparent;
            LabelR6.Dock = DockStyle.Fill;
            LabelR6.DrawOnGlass = false;
            LabelR6.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR6.ForeColor = Color.Black;
            LabelR6.Location = new Point(21, 0);
            LabelR6.Name = "LabelR6";
            LabelR6.Padding = new Padding(0, 0, 0, 2);
            LabelR6.Size = new Size(86, 20);
            LabelR6.TabIndex = 3;
            LabelR6.Text = "Menu item";
            LabelR6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WindowR3
            // 
            WindowR3.BackColor = Color.FromArgb(192, 192, 192);
            WindowR3.ButtonDkShadow = Color.Black;
            WindowR3.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR3.ButtonHilight = Color.White;
            WindowR3.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR3.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR3.ButtonText = Color.Black;
            WindowR3.Color1 = Color.FromArgb(0, 0, 128);
            WindowR3.Color2 = Color.FromArgb(16, 132, 208);
            WindowR3.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR3.ColorGradient = true;
            WindowR3.ControlBox = false;
            WindowR3.Controls.Add(ButtonR5);
            WindowR3.Controls.Add(LabelR4);
            WindowR3.Flat = false;
            WindowR3.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR3.ForeColor = Color.White;
            WindowR3.Location = new Point(215, 185);
            WindowR3.MaximizeBox = true;
            WindowR3.Metrics_BorderWidth = 0;
            WindowR3.Metrics_CaptionHeight = 18;
            WindowR3.Metrics_CaptionWidth = 18;
            WindowR3.Metrics_PaddedBorderWidth = 0;
            WindowR3.MinimizeBox = true;
            WindowR3.Name = "WindowR3";
            WindowR3.Padding = new Padding(4, 22, 4, 4);
            WindowR3.Size = new Size(147, 80);
            WindowR3.TabIndex = 2;
            WindowR3.Text = "Message box";
            WindowR3.UseItAsMenu = false;
            // 
            // ButtonR5
            // 
            ButtonR5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonR5.AppearsAsPressed = false;
            ButtonR5.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR5.ButtonDkShadow = Color.Black;
            ButtonR5.ButtonHilight = Color.White;
            ButtonR5.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR5.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR5.FocusRectHeight = 1;
            ButtonR5.FocusRectWidth = 1;
            ButtonR5.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonR5.ForeColor = Color.Black;
            ButtonR5.HatchBrush = false;
            ButtonR5.Image = null;
            ButtonR5.Location = new Point(37, 49);
            ButtonR5.Name = "ButtonR5";
            ButtonR5.Size = new Size(75, 23);
            ButtonR5.TabIndex = 2;
            ButtonR5.Text = "OK";
            ButtonR5.UseItAsScrollbar = false;
            ButtonR5.UseVisualStyleBackColor = false;
            ButtonR5.WindowFrame = Color.Black;
            // 
            // LabelR4
            // 
            LabelR4.AutoSize = true;
            LabelR4.BackColor = Color.Transparent;
            LabelR4.Dock = DockStyle.Top;
            LabelR4.DrawOnGlass = false;
            LabelR4.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR4.ForeColor = Color.Black;
            LabelR4.Location = new Point(4, 22);
            LabelR4.Name = "LabelR4";
            LabelR4.Padding = new Padding(4);
            LabelR4.Size = new Size(78, 21);
            LabelR4.TabIndex = 1;
            LabelR4.Text = "Message text";
            LabelR4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelR13
            // 
            LabelR13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LabelR13.AutoSize = true;
            LabelR13.BackColor = Color.White;
            LabelR13.BorderStyle = BorderStyle.FixedSingle;
            LabelR13.DrawOnGlass = false;
            LabelR13.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR13.ForeColor = Color.Black;
            LabelR13.Location = new Point(287, 47);
            LabelR13.Name = "LabelR13";
            LabelR13.Size = new Size(79, 15);
            LabelR13.TabIndex = 5;
            LabelR13.Text = "This is a tooltip";
            // 
            // WindowR2
            // 
            WindowR2.BackColor = Color.FromArgb(192, 192, 192);
            WindowR2.ButtonDkShadow = Color.Black;
            WindowR2.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR2.ButtonHilight = Color.White;
            WindowR2.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR2.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR2.ButtonText = Color.Black;
            WindowR2.Color1 = Color.FromArgb(0, 0, 128);
            WindowR2.Color2 = Color.FromArgb(16, 132, 208);
            WindowR2.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR2.ColorGradient = true;
            WindowR2.ControlBox = true;
            WindowR2.Controls.Add(TextBoxR1);
            WindowR2.Controls.Add(menucontainer0);
            WindowR2.Flat = false;
            WindowR2.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR2.ForeColor = Color.White;
            WindowR2.Location = new Point(195, 110);
            WindowR2.MaximizeBox = true;
            WindowR2.Metrics_BorderWidth = 0;
            WindowR2.Metrics_CaptionHeight = 18;
            WindowR2.Metrics_CaptionWidth = 18;
            WindowR2.Metrics_PaddedBorderWidth = 0;
            WindowR2.MinimizeBox = true;
            WindowR2.Name = "WindowR2";
            WindowR2.Padding = new Padding(4, 22, 4, 4);
            WindowR2.Size = new Size(196, 120);
            WindowR2.TabIndex = 1;
            WindowR2.Text = "Active window";
            WindowR2.UseItAsMenu = false;
            // 
            // TextBoxR1
            // 
            TextBoxR1.BackColor = Color.White;
            TextBoxR1.ButtonDkShadow = Color.Black;
            TextBoxR1.ButtonHilight = Color.White;
            TextBoxR1.ButtonLight = Color.FromArgb(192, 192, 192);
            TextBoxR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            TextBoxR1.Dock = DockStyle.Fill;
            TextBoxR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            TextBoxR1.ForeColor = Color.Black;
            TextBoxR1.Location = new Point(4, 40);
            TextBoxR1.MaxLength = 32767;
            TextBoxR1.Multiline = true;
            TextBoxR1.Name = "TextBoxR1";
            TextBoxR1.ReadOnly = true;
            TextBoxR1.Size = new Size(188, 76);
            TextBoxR1.TabIndex = 3;
            TextBoxR1.Text = "Window text";
            TextBoxR1.TextAlign = HorizontalAlignment.Left;
            TextBoxR1.UseSystemPasswordChar = false;
            // 
            // menucontainer0
            // 
            menucontainer0.BackColor = Color.Silver;
            menucontainer0.Controls.Add(PanelR1);
            menucontainer0.Controls.Add(LabelR2);
            menucontainer0.Controls.Add(LabelR1);
            menucontainer0.Dock = DockStyle.Top;
            menucontainer0.Location = new Point(4, 22);
            menucontainer0.Name = "menucontainer0";
            menucontainer0.Size = new Size(188, 18);
            menucontainer0.TabIndex = 5;
            // 
            // PanelR1
            // 
            PanelR1.BackColor = Color.FromArgb(192, 192, 192);
            PanelR1.ButtonDkShadow = Color.FromArgb(105, 105, 105);
            PanelR1.ButtonHilight = Color.White;
            PanelR1.ButtonLight = Color.FromArgb(227, 227, 227);
            PanelR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            PanelR1.Controls.Add(LabelR3);
            PanelR1.Dock = DockStyle.Left;
            PanelR1.Flat = false;
            PanelR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            PanelR1.ForeColor = Color.Black;
            PanelR1.Location = new Point(88, 0);
            PanelR1.Name = "PanelR1";
            PanelR1.Padding = new Padding(1, 3, 1, 3);
            PanelR1.Size = new Size(53, 18);
            PanelR1.Style2 = false;
            PanelR1.TabIndex = 2;
            // 
            // LabelR3
            // 
            LabelR3.BackColor = Color.Transparent;
            LabelR3.Dock = DockStyle.Fill;
            LabelR3.DrawOnGlass = false;
            LabelR3.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR3.ForeColor = Color.Black;
            LabelR3.Location = new Point(1, 3);
            LabelR3.Name = "LabelR3";
            LabelR3.Size = new Size(51, 12);
            LabelR3.TabIndex = 1;
            LabelR3.Text = "Selected";
            LabelR3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelR2
            // 
            LabelR2.BackColor = Color.Transparent;
            LabelR2.Dock = DockStyle.Left;
            LabelR2.DrawOnGlass = false;
            LabelR2.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR2.ForeColor = Color.FromArgb(64, 64, 64);
            LabelR2.Location = new Point(40, 0);
            LabelR2.Name = "LabelR2";
            LabelR2.Padding = new Padding(0, 0, 0, 1);
            LabelR2.Size = new Size(48, 18);
            LabelR2.TabIndex = 1;
            LabelR2.Text = "Disabled";
            LabelR2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelR1
            // 
            LabelR1.BackColor = Color.Transparent;
            LabelR1.Dock = DockStyle.Left;
            LabelR1.DrawOnGlass = false;
            LabelR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            LabelR1.ForeColor = Color.Black;
            LabelR1.Location = new Point(0, 0);
            LabelR1.Name = "LabelR1";
            LabelR1.Padding = new Padding(0, 0, 0, 1);
            LabelR1.Size = new Size(40, 18);
            LabelR1.TabIndex = 0;
            LabelR1.Text = "Normal";
            LabelR1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WindowR1
            // 
            WindowR1.BackColor = Color.FromArgb(192, 192, 192);
            WindowR1.ButtonDkShadow = Color.Black;
            WindowR1.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR1.ButtonHilight = Color.White;
            WindowR1.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR1.ButtonText = Color.Black;
            WindowR1.Color1 = Color.FromArgb(51, 51, 51);
            WindowR1.Color2 = Color.FromArgb(181, 181, 181);
            WindowR1.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR1.ColorGradient = true;
            WindowR1.ControlBox = true;
            WindowR1.Flat = false;
            WindowR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR1.ForeColor = Color.White;
            WindowR1.Location = new Point(179, 77);
            WindowR1.MaximizeBox = true;
            WindowR1.Metrics_BorderWidth = 0;
            WindowR1.Metrics_CaptionHeight = 18;
            WindowR1.Metrics_CaptionWidth = 18;
            WindowR1.Metrics_PaddedBorderWidth = 0;
            WindowR1.MinimizeBox = true;
            WindowR1.Name = "WindowR1";
            WindowR1.Padding = new Padding(4, 0, 4, 4);
            WindowR1.Size = new Size(180, 112);
            WindowR1.TabIndex = 0;
            WindowR1.Text = "Inactive window";
            WindowR1.UseItAsMenu = false;
            // 
            // WindowR4
            // 
            WindowR4.BackColor = Color.FromArgb(192, 192, 192);
            WindowR4.ButtonDkShadow = Color.Black;
            WindowR4.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR4.ButtonHilight = Color.White;
            WindowR4.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR4.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR4.ButtonText = Color.Black;
            WindowR4.Color1 = Color.FromArgb(0, 0, 128);
            WindowR4.Color2 = Color.FromArgb(16, 132, 208);
            WindowR4.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR4.ColorGradient = true;
            WindowR4.ControlBox = true;
            WindowR4.Controls.Add(programcontainer);
            WindowR4.Flat = false;
            WindowR4.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR4.ForeColor = Color.White;
            WindowR4.Location = new Point(139, 30);
            WindowR4.MaximizeBox = false;
            WindowR4.Metrics_BorderWidth = 0;
            WindowR4.Metrics_CaptionHeight = 18;
            WindowR4.Metrics_CaptionWidth = 18;
            WindowR4.Metrics_PaddedBorderWidth = 0;
            WindowR4.MinimizeBox = false;
            WindowR4.Name = "WindowR4";
            WindowR4.Padding = new Padding(4, 22, 4, 4);
            WindowR4.Size = new Size(156, 132);
            WindowR4.TabIndex = 3;
            WindowR4.Text = "Program container";
            WindowR4.UseItAsMenu = false;
            // 
            // programcontainer
            // 
            programcontainer.BackColor = Color.FromArgb(64, 64, 64);
            programcontainer.Controls.Add(PanelR2);
            programcontainer.Dock = DockStyle.Fill;
            programcontainer.Location = new Point(4, 22);
            programcontainer.Name = "programcontainer";
            programcontainer.Padding = new Padding(0, 0, 1, 0);
            programcontainer.Size = new Size(148, 106);
            programcontainer.TabIndex = 4;
            // 
            // PanelR2
            // 
            PanelR2.BackColor = Color.FromArgb(192, 192, 192);
            PanelR2.ButtonHilight = Color.White;
            PanelR2.Controls.Add(ButtonR12);
            PanelR2.Controls.Add(ButtonR11);
            PanelR2.Controls.Add(ButtonR10);
            PanelR2.Dock = DockStyle.Left;
            PanelR2.Font = new Font("Microsoft Sans Serif", 8.0f);
            PanelR2.ForeColor = Color.Black;
            PanelR2.Location = new Point(0, 0);
            PanelR2.Name = "PanelR2";
            PanelR2.Size = new Size(16, 106);
            PanelR2.TabIndex = 0;
            // 
            // ButtonR12
            // 
            ButtonR12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            ButtonR12.AppearsAsPressed = false;
            ButtonR12.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR12.ButtonDkShadow = Color.Black;
            ButtonR12.ButtonHilight = Color.White;
            ButtonR12.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR12.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR12.FocusRectHeight = 1;
            ButtonR12.FocusRectWidth = 1;
            ButtonR12.Font = new Font("Marlett", 6.0f);
            ButtonR12.ForeColor = Color.Black;
            ButtonR12.HatchBrush = false;
            ButtonR12.Image = null;
            ButtonR12.Location = new Point(0, 29);
            ButtonR12.Name = "ButtonR12";
            ButtonR12.Size = new Size(16, 31);
            ButtonR12.TabIndex = 7;
            ButtonR12.UseItAsScrollbar = true;
            ButtonR12.UseVisualStyleBackColor = false;
            ButtonR12.WindowFrame = Color.Black;
            // 
            // ButtonR11
            // 
            ButtonR11.AppearsAsPressed = false;
            ButtonR11.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR11.ButtonDkShadow = Color.Black;
            ButtonR11.ButtonHilight = Color.White;
            ButtonR11.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR11.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR11.Dock = DockStyle.Bottom;
            ButtonR11.FocusRectHeight = 1;
            ButtonR11.FocusRectWidth = 1;
            ButtonR11.Font = new Font("Marlett", 8.7f, FontStyle.Bold);
            ButtonR11.ForeColor = Color.Black;
            ButtonR11.HatchBrush = false;
            ButtonR11.Image = null;
            ButtonR11.Location = new Point(0, 92);
            ButtonR11.Name = "ButtonR11";
            ButtonR11.Size = new Size(16, 14);
            ButtonR11.TabIndex = 6;
            ButtonR11.Text = "u";
            ButtonR11.TextAlign = ContentAlignment.TopCenter;
            ButtonR11.UseItAsScrollbar = false;
            ButtonR11.UseVisualStyleBackColor = false;
            ButtonR11.WindowFrame = Color.Black;
            // 
            // ButtonR10
            // 
            ButtonR10.AppearsAsPressed = false;
            ButtonR10.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR10.ButtonDkShadow = Color.Black;
            ButtonR10.ButtonHilight = Color.White;
            ButtonR10.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR10.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR10.Dock = DockStyle.Top;
            ButtonR10.FocusRectHeight = 1;
            ButtonR10.FocusRectWidth = 1;
            ButtonR10.Font = new Font("Marlett", 8.7f, FontStyle.Bold);
            ButtonR10.ForeColor = Color.Black;
            ButtonR10.HatchBrush = false;
            ButtonR10.Image = null;
            ButtonR10.Location = new Point(0, 0);
            ButtonR10.Name = "ButtonR10";
            ButtonR10.Size = new Size(16, 14);
            ButtonR10.TabIndex = 5;
            ButtonR10.Text = "t";
            ButtonR10.TextAlign = ContentAlignment.BottomCenter;
            ButtonR10.UseItAsScrollbar = false;
            ButtonR10.UseVisualStyleBackColor = false;
            ButtonR10.WindowFrame = Color.Black;
            // 
            // CMD1
            // 
            CMD1.CMD_ColorTable00 = Color.Empty;
            CMD1.CMD_ColorTable01 = Color.Empty;
            CMD1.CMD_ColorTable02 = Color.Empty;
            CMD1.CMD_ColorTable03 = Color.Empty;
            CMD1.CMD_ColorTable04 = Color.Empty;
            CMD1.CMD_ColorTable05 = Color.Empty;
            CMD1.CMD_ColorTable06 = Color.Empty;
            CMD1.CMD_ColorTable07 = Color.Empty;
            CMD1.CMD_ColorTable08 = Color.Empty;
            CMD1.CMD_ColorTable09 = Color.Empty;
            CMD1.CMD_ColorTable10 = Color.Empty;
            CMD1.CMD_ColorTable11 = Color.Empty;
            CMD1.CMD_ColorTable12 = Color.Empty;
            CMD1.CMD_ColorTable13 = Color.Empty;
            CMD1.CMD_ColorTable14 = Color.Empty;
            CMD1.CMD_ColorTable15 = Color.Empty;
            CMD1.CMD_PopupBackground = 5;
            CMD1.CMD_PopupForeground = 15;
            CMD1.CMD_ScreenColorsBackground = 0;
            CMD1.CMD_ScreenColorsForeground = 7;
            CMD1.CustomTerminal = false;
            CMD1.Location = new Point(1071, 3);
            CMD1.Name = "CMD1";
            CMD1.PowerShell = false;
            CMD1.Raster = true;
            CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
            CMD1.Size = new Size(528, 297);
            CMD1.TabIndex = 1;
            // 
            // CMD2
            // 
            CMD2.CMD_ColorTable00 = Color.Empty;
            CMD2.CMD_ColorTable01 = Color.Empty;
            CMD2.CMD_ColorTable02 = Color.Empty;
            CMD2.CMD_ColorTable03 = Color.Empty;
            CMD2.CMD_ColorTable04 = Color.Empty;
            CMD2.CMD_ColorTable05 = Color.Empty;
            CMD2.CMD_ColorTable06 = Color.Empty;
            CMD2.CMD_ColorTable07 = Color.Empty;
            CMD2.CMD_ColorTable08 = Color.Empty;
            CMD2.CMD_ColorTable09 = Color.Empty;
            CMD2.CMD_ColorTable10 = Color.Empty;
            CMD2.CMD_ColorTable11 = Color.Empty;
            CMD2.CMD_ColorTable12 = Color.Empty;
            CMD2.CMD_ColorTable13 = Color.Empty;
            CMD2.CMD_ColorTable14 = Color.Empty;
            CMD2.CMD_ColorTable15 = Color.Empty;
            CMD2.CMD_PopupBackground = 5;
            CMD2.CMD_PopupForeground = 15;
            CMD2.CMD_ScreenColorsBackground = 0;
            CMD2.CMD_ScreenColorsForeground = 7;
            CMD2.CustomTerminal = false;
            CMD2.Location = new Point(1605, 3);
            CMD2.Name = "CMD2";
            CMD2.PowerShell = false;
            CMD2.Raster = true;
            CMD2.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
            CMD2.Size = new Size(528, 297);
            CMD2.TabIndex = 2;
            // 
            // CMD3
            // 
            CMD3.CMD_ColorTable00 = Color.Empty;
            CMD3.CMD_ColorTable01 = Color.Empty;
            CMD3.CMD_ColorTable02 = Color.Empty;
            CMD3.CMD_ColorTable03 = Color.Empty;
            CMD3.CMD_ColorTable04 = Color.Empty;
            CMD3.CMD_ColorTable05 = Color.Empty;
            CMD3.CMD_ColorTable06 = Color.Empty;
            CMD3.CMD_ColorTable07 = Color.Empty;
            CMD3.CMD_ColorTable08 = Color.Empty;
            CMD3.CMD_ColorTable09 = Color.Empty;
            CMD3.CMD_ColorTable10 = Color.Empty;
            CMD3.CMD_ColorTable11 = Color.Empty;
            CMD3.CMD_ColorTable12 = Color.Empty;
            CMD3.CMD_ColorTable13 = Color.Empty;
            CMD3.CMD_ColorTable14 = Color.Empty;
            CMD3.CMD_ColorTable15 = Color.Empty;
            CMD3.CMD_PopupBackground = 5;
            CMD3.CMD_PopupForeground = 15;
            CMD3.CMD_ScreenColorsBackground = 0;
            CMD3.CMD_ScreenColorsForeground = 7;
            CMD3.CustomTerminal = false;
            CMD3.Location = new Point(2139, 3);
            CMD3.Name = "CMD3";
            CMD3.PowerShell = false;
            CMD3.Raster = true;
            CMD3.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
            CMD3.Size = new Size(528, 297);
            CMD3.TabIndex = 2;
            // 
            // Panel1
            // 
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(cur_anim_btn);
            Panel1.Controls.Add(Cursors_Container);
            Panel1.Controls.Add(cur_tip_btn);
            Panel1.Controls.Add(PictureBox12);
            Panel1.Controls.Add(CursorsSize_Bar);
            Panel1.Controls.Add(Label17);
            Panel1.Location = new Point(2673, 3);
            Panel1.Name = "Panel1";
            Panel1.Padding = new Padding(3);
            Panel1.Size = new Size(528, 297);
            Panel1.TabIndex = 140;
            // 
            // cur_anim_btn
            // 
            cur_anim_btn.BackColor = Color.FromArgb(34, 34, 34);
            cur_anim_btn.DrawOnGlass = false;
            cur_anim_btn.Font = new Font("Segoe UI", 9.0f);
            cur_anim_btn.ForeColor = Color.White;
            cur_anim_btn.Image = (Image)resources.GetObject("cur_anim_btn.Image");
            cur_anim_btn.ImageAlign = ContentAlignment.MiddleLeft;
            cur_anim_btn.LineColor = Color.FromArgb(25, 99, 126);
            cur_anim_btn.Location = new Point(356, 267);
            cur_anim_btn.Name = "cur_anim_btn";
            cur_anim_btn.Size = new Size(141, 21);
            cur_anim_btn.TabIndex = 72;
            cur_anim_btn.Text = "Animate (3 Cycles)";
            cur_anim_btn.UseVisualStyleBackColor = false;
            // 
            // Cursors_Container
            // 
            Cursors_Container.AutoScroll = true;
            Cursors_Container.Controls.Add(Arrow);
            Cursors_Container.Controls.Add(Help);
            Cursors_Container.Controls.Add(AppLoading);
            Cursors_Container.Controls.Add(Busy);
            Cursors_Container.Controls.Add(Move_Cur);
            Cursors_Container.Controls.Add(Up);
            Cursors_Container.Controls.Add(NS);
            Cursors_Container.Controls.Add(EW);
            Cursors_Container.Controls.Add(NESW);
            Cursors_Container.Controls.Add(NWSE);
            Cursors_Container.Controls.Add(Pen);
            Cursors_Container.Controls.Add(None);
            Cursors_Container.Controls.Add(Link);
            Cursors_Container.Controls.Add(Pin);
            Cursors_Container.Controls.Add(Person);
            Cursors_Container.Controls.Add(IBeam);
            Cursors_Container.Controls.Add(Cross);
            Cursors_Container.Dock = DockStyle.Top;
            Cursors_Container.Location = new Point(3, 3);
            Cursors_Container.Name = "Cursors_Container";
            Cursors_Container.Padding = new Padding(4, 4, 0, 4);
            Cursors_Container.Size = new Size(520, 218);
            Cursors_Container.TabIndex = 67;
            // 
            // Arrow
            // 
            Arrow.Location = new Point(7, 7);
            Arrow.Name = "Arrow";
            Arrow.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Arrow.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Arrow.Prop_Cursor = Paths.CursorType.Arrow;
            Arrow.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Arrow.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Arrow.Prop_LoadingCircleBackGradient = false;
            Arrow.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_LoadingCircleBackNoise = false;
            Arrow.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Arrow.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Arrow.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Arrow.Prop_LoadingCircleHotGradient = false;
            Arrow.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_LoadingCircleHotNoise = false;
            Arrow.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Arrow.Prop_PrimaryColor1 = Color.White;
            Arrow.Prop_PrimaryColor2 = Color.White;
            Arrow.Prop_PrimaryColorGradient = false;
            Arrow.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_PrimaryNoise = false;
            Arrow.Prop_PrimaryNoiseOpacity = 0.25f;
            Arrow.Prop_Scale = 1.0f;
            Arrow.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Arrow.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Arrow.Prop_SecondaryColorGradient = false;
            Arrow.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Arrow.Prop_SecondaryNoise = false;
            Arrow.Prop_SecondaryNoiseOpacity = 0.25f;
            Arrow.Prop_Shadow_Blur = 5;
            Arrow.Prop_Shadow_Color = Color.Black;
            Arrow.Prop_Shadow_Enabled = false;
            Arrow.Prop_Shadow_OffsetX = 2;
            Arrow.Prop_Shadow_OffsetY = 2;
            Arrow.Prop_Shadow_Opacity = 0.3f;
            Arrow.Size = new Size(64, 64);
            Arrow.TabIndex = 5;
            // 
            // Help
            // 
            Help.Location = new Point(77, 7);
            Help.Name = "Help";
            Help.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Help.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Help.Prop_Cursor = Paths.CursorType.Help;
            Help.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Help.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Help.Prop_LoadingCircleBackGradient = false;
            Help.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_LoadingCircleBackNoise = false;
            Help.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Help.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Help.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Help.Prop_LoadingCircleHotGradient = false;
            Help.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_LoadingCircleHotNoise = false;
            Help.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Help.Prop_PrimaryColor1 = Color.White;
            Help.Prop_PrimaryColor2 = Color.White;
            Help.Prop_PrimaryColorGradient = false;
            Help.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_PrimaryNoise = false;
            Help.Prop_PrimaryNoiseOpacity = 0.25f;
            Help.Prop_Scale = 1.0f;
            Help.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Help.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Help.Prop_SecondaryColorGradient = false;
            Help.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Help.Prop_SecondaryNoise = false;
            Help.Prop_SecondaryNoiseOpacity = 0.25f;
            Help.Prop_Shadow_Blur = 5;
            Help.Prop_Shadow_Color = Color.Black;
            Help.Prop_Shadow_Enabled = false;
            Help.Prop_Shadow_OffsetX = 2;
            Help.Prop_Shadow_OffsetY = 2;
            Help.Prop_Shadow_Opacity = 0.3f;
            Help.Size = new Size(64, 64);
            Help.TabIndex = 6;
            // 
            // AppLoading
            // 
            AppLoading.Location = new Point(147, 7);
            AppLoading.Name = "AppLoading";
            AppLoading.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            AppLoading.Prop_CircleStyle = Paths.CircleStyle.Aero;
            AppLoading.Prop_Cursor = Paths.CursorType.AppLoading;
            AppLoading.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            AppLoading.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            AppLoading.Prop_LoadingCircleBackGradient = false;
            AppLoading.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_LoadingCircleBackNoise = false;
            AppLoading.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            AppLoading.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            AppLoading.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            AppLoading.Prop_LoadingCircleHotGradient = false;
            AppLoading.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_LoadingCircleHotNoise = false;
            AppLoading.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            AppLoading.Prop_PrimaryColor1 = Color.White;
            AppLoading.Prop_PrimaryColor2 = Color.White;
            AppLoading.Prop_PrimaryColorGradient = false;
            AppLoading.Prop_PrimaryColorGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_PrimaryNoise = false;
            AppLoading.Prop_PrimaryNoiseOpacity = 0.25f;
            AppLoading.Prop_Scale = 1.0f;
            AppLoading.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            AppLoading.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            AppLoading.Prop_SecondaryColorGradient = false;
            AppLoading.Prop_SecondaryColorGradientMode = Paths.GradientMode.Circle;
            AppLoading.Prop_SecondaryNoise = false;
            AppLoading.Prop_SecondaryNoiseOpacity = 0.25f;
            AppLoading.Prop_Shadow_Blur = 5;
            AppLoading.Prop_Shadow_Color = Color.Black;
            AppLoading.Prop_Shadow_Enabled = false;
            AppLoading.Prop_Shadow_OffsetX = 2;
            AppLoading.Prop_Shadow_OffsetY = 2;
            AppLoading.Prop_Shadow_Opacity = 0.3f;
            AppLoading.Size = new Size(64, 64);
            AppLoading.TabIndex = 6;
            // 
            // Busy
            // 
            Busy.Location = new Point(217, 7);
            Busy.Name = "Busy";
            Busy.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Busy.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Busy.Prop_Cursor = Paths.CursorType.Busy;
            Busy.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Busy.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Busy.Prop_LoadingCircleBackGradient = false;
            Busy.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_LoadingCircleBackNoise = false;
            Busy.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Busy.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Busy.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Busy.Prop_LoadingCircleHotGradient = false;
            Busy.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_LoadingCircleHotNoise = false;
            Busy.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Busy.Prop_PrimaryColor1 = Color.White;
            Busy.Prop_PrimaryColor2 = Color.White;
            Busy.Prop_PrimaryColorGradient = false;
            Busy.Prop_PrimaryColorGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_PrimaryNoise = false;
            Busy.Prop_PrimaryNoiseOpacity = 0.25f;
            Busy.Prop_Scale = 1.0f;
            Busy.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Busy.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Busy.Prop_SecondaryColorGradient = false;
            Busy.Prop_SecondaryColorGradientMode = Paths.GradientMode.Circle;
            Busy.Prop_SecondaryNoise = false;
            Busy.Prop_SecondaryNoiseOpacity = 0.25f;
            Busy.Prop_Shadow_Blur = 5;
            Busy.Prop_Shadow_Color = Color.Black;
            Busy.Prop_Shadow_Enabled = false;
            Busy.Prop_Shadow_OffsetX = 2;
            Busy.Prop_Shadow_OffsetY = 2;
            Busy.Prop_Shadow_Opacity = 0.3f;
            Busy.Size = new Size(64, 64);
            Busy.TabIndex = 7;
            // 
            // Move_Cur
            // 
            Move_Cur.Location = new Point(287, 7);
            Move_Cur.Name = "Move_Cur";
            Move_Cur.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Move_Cur.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Move_Cur.Prop_Cursor = Paths.CursorType.Move;
            Move_Cur.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Move_Cur.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Move_Cur.Prop_LoadingCircleBackGradient = false;
            Move_Cur.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_LoadingCircleBackNoise = false;
            Move_Cur.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Move_Cur.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Move_Cur.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Move_Cur.Prop_LoadingCircleHotGradient = false;
            Move_Cur.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_LoadingCircleHotNoise = false;
            Move_Cur.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Move_Cur.Prop_PrimaryColor1 = Color.White;
            Move_Cur.Prop_PrimaryColor2 = Color.White;
            Move_Cur.Prop_PrimaryColorGradient = false;
            Move_Cur.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_PrimaryNoise = false;
            Move_Cur.Prop_PrimaryNoiseOpacity = 0.25f;
            Move_Cur.Prop_Scale = 1.0f;
            Move_Cur.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Move_Cur.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Move_Cur.Prop_SecondaryColorGradient = false;
            Move_Cur.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Move_Cur.Prop_SecondaryNoise = false;
            Move_Cur.Prop_SecondaryNoiseOpacity = 0.25f;
            Move_Cur.Prop_Shadow_Blur = 5;
            Move_Cur.Prop_Shadow_Color = Color.Black;
            Move_Cur.Prop_Shadow_Enabled = false;
            Move_Cur.Prop_Shadow_OffsetX = 2;
            Move_Cur.Prop_Shadow_OffsetY = 2;
            Move_Cur.Prop_Shadow_Opacity = 0.3f;
            Move_Cur.Size = new Size(64, 64);
            Move_Cur.TabIndex = 8;
            // 
            // Up
            // 
            Up.Location = new Point(357, 7);
            Up.Name = "Up";
            Up.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Up.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Up.Prop_Cursor = Paths.CursorType.Up;
            Up.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Up.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Up.Prop_LoadingCircleBackGradient = false;
            Up.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_LoadingCircleBackNoise = false;
            Up.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Up.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Up.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Up.Prop_LoadingCircleHotGradient = false;
            Up.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_LoadingCircleHotNoise = false;
            Up.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Up.Prop_PrimaryColor1 = Color.White;
            Up.Prop_PrimaryColor2 = Color.White;
            Up.Prop_PrimaryColorGradient = false;
            Up.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_PrimaryNoise = false;
            Up.Prop_PrimaryNoiseOpacity = 0.25f;
            Up.Prop_Scale = 1.0f;
            Up.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Up.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Up.Prop_SecondaryColorGradient = false;
            Up.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Up.Prop_SecondaryNoise = false;
            Up.Prop_SecondaryNoiseOpacity = 0.25f;
            Up.Prop_Shadow_Blur = 5;
            Up.Prop_Shadow_Color = Color.Black;
            Up.Prop_Shadow_Enabled = false;
            Up.Prop_Shadow_OffsetX = 2;
            Up.Prop_Shadow_OffsetY = 2;
            Up.Prop_Shadow_Opacity = 0.3f;
            Up.Size = new Size(64, 64);
            Up.TabIndex = 9;
            // 
            // NS
            // 
            NS.Location = new Point(427, 7);
            NS.Name = "NS";
            NS.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            NS.Prop_CircleStyle = Paths.CircleStyle.Aero;
            NS.Prop_Cursor = Paths.CursorType.NS;
            NS.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            NS.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            NS.Prop_LoadingCircleBackGradient = false;
            NS.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_LoadingCircleBackNoise = false;
            NS.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            NS.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            NS.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            NS.Prop_LoadingCircleHotGradient = false;
            NS.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_LoadingCircleHotNoise = false;
            NS.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            NS.Prop_PrimaryColor1 = Color.White;
            NS.Prop_PrimaryColor2 = Color.White;
            NS.Prop_PrimaryColorGradient = false;
            NS.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_PrimaryNoise = false;
            NS.Prop_PrimaryNoiseOpacity = 0.25f;
            NS.Prop_Scale = 1.0f;
            NS.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            NS.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            NS.Prop_SecondaryColorGradient = false;
            NS.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            NS.Prop_SecondaryNoise = false;
            NS.Prop_SecondaryNoiseOpacity = 0.25f;
            NS.Prop_Shadow_Blur = 5;
            NS.Prop_Shadow_Color = Color.Black;
            NS.Prop_Shadow_Enabled = false;
            NS.Prop_Shadow_OffsetX = 2;
            NS.Prop_Shadow_OffsetY = 2;
            NS.Prop_Shadow_Opacity = 0.3f;
            NS.Size = new Size(64, 64);
            NS.TabIndex = 10;
            // 
            // EW
            // 
            EW.Location = new Point(7, 77);
            EW.Name = "EW";
            EW.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            EW.Prop_CircleStyle = Paths.CircleStyle.Aero;
            EW.Prop_Cursor = Paths.CursorType.EW;
            EW.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            EW.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            EW.Prop_LoadingCircleBackGradient = false;
            EW.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_LoadingCircleBackNoise = false;
            EW.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            EW.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            EW.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            EW.Prop_LoadingCircleHotGradient = false;
            EW.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_LoadingCircleHotNoise = false;
            EW.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            EW.Prop_PrimaryColor1 = Color.White;
            EW.Prop_PrimaryColor2 = Color.White;
            EW.Prop_PrimaryColorGradient = false;
            EW.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_PrimaryNoise = false;
            EW.Prop_PrimaryNoiseOpacity = 0.25f;
            EW.Prop_Scale = 1.0f;
            EW.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            EW.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            EW.Prop_SecondaryColorGradient = false;
            EW.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            EW.Prop_SecondaryNoise = false;
            EW.Prop_SecondaryNoiseOpacity = 0.25f;
            EW.Prop_Shadow_Blur = 5;
            EW.Prop_Shadow_Color = Color.Black;
            EW.Prop_Shadow_Enabled = false;
            EW.Prop_Shadow_OffsetX = 2;
            EW.Prop_Shadow_OffsetY = 2;
            EW.Prop_Shadow_Opacity = 0.3f;
            EW.Size = new Size(64, 64);
            EW.TabIndex = 11;
            // 
            // NESW
            // 
            NESW.Location = new Point(77, 77);
            NESW.Name = "NESW";
            NESW.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            NESW.Prop_CircleStyle = Paths.CircleStyle.Aero;
            NESW.Prop_Cursor = Paths.CursorType.NESW;
            NESW.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            NESW.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            NESW.Prop_LoadingCircleBackGradient = false;
            NESW.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_LoadingCircleBackNoise = false;
            NESW.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            NESW.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            NESW.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            NESW.Prop_LoadingCircleHotGradient = false;
            NESW.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_LoadingCircleHotNoise = false;
            NESW.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            NESW.Prop_PrimaryColor1 = Color.White;
            NESW.Prop_PrimaryColor2 = Color.White;
            NESW.Prop_PrimaryColorGradient = false;
            NESW.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_PrimaryNoise = false;
            NESW.Prop_PrimaryNoiseOpacity = 0.25f;
            NESW.Prop_Scale = 1.0f;
            NESW.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            NESW.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            NESW.Prop_SecondaryColorGradient = false;
            NESW.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            NESW.Prop_SecondaryNoise = false;
            NESW.Prop_SecondaryNoiseOpacity = 0.25f;
            NESW.Prop_Shadow_Blur = 5;
            NESW.Prop_Shadow_Color = Color.Black;
            NESW.Prop_Shadow_Enabled = false;
            NESW.Prop_Shadow_OffsetX = 2;
            NESW.Prop_Shadow_OffsetY = 2;
            NESW.Prop_Shadow_Opacity = 0.3f;
            NESW.Size = new Size(64, 64);
            NESW.TabIndex = 12;
            // 
            // NWSE
            // 
            NWSE.Location = new Point(147, 77);
            NWSE.Name = "NWSE";
            NWSE.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            NWSE.Prop_CircleStyle = Paths.CircleStyle.Aero;
            NWSE.Prop_Cursor = Paths.CursorType.NWSE;
            NWSE.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            NWSE.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            NWSE.Prop_LoadingCircleBackGradient = false;
            NWSE.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_LoadingCircleBackNoise = false;
            NWSE.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            NWSE.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            NWSE.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            NWSE.Prop_LoadingCircleHotGradient = false;
            NWSE.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_LoadingCircleHotNoise = false;
            NWSE.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            NWSE.Prop_PrimaryColor1 = Color.White;
            NWSE.Prop_PrimaryColor2 = Color.White;
            NWSE.Prop_PrimaryColorGradient = false;
            NWSE.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_PrimaryNoise = false;
            NWSE.Prop_PrimaryNoiseOpacity = 0.25f;
            NWSE.Prop_Scale = 1.0f;
            NWSE.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            NWSE.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            NWSE.Prop_SecondaryColorGradient = false;
            NWSE.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            NWSE.Prop_SecondaryNoise = false;
            NWSE.Prop_SecondaryNoiseOpacity = 0.25f;
            NWSE.Prop_Shadow_Blur = 5;
            NWSE.Prop_Shadow_Color = Color.Black;
            NWSE.Prop_Shadow_Enabled = false;
            NWSE.Prop_Shadow_OffsetX = 2;
            NWSE.Prop_Shadow_OffsetY = 2;
            NWSE.Prop_Shadow_Opacity = 0.3f;
            NWSE.Size = new Size(64, 64);
            NWSE.TabIndex = 13;
            // 
            // Pen
            // 
            Pen.Location = new Point(217, 77);
            Pen.Name = "Pen";
            Pen.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Pen.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Pen.Prop_Cursor = Paths.CursorType.Pen;
            Pen.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Pen.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Pen.Prop_LoadingCircleBackGradient = false;
            Pen.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_LoadingCircleBackNoise = false;
            Pen.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Pen.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Pen.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Pen.Prop_LoadingCircleHotGradient = false;
            Pen.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_LoadingCircleHotNoise = false;
            Pen.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Pen.Prop_PrimaryColor1 = Color.White;
            Pen.Prop_PrimaryColor2 = Color.White;
            Pen.Prop_PrimaryColorGradient = false;
            Pen.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_PrimaryNoise = false;
            Pen.Prop_PrimaryNoiseOpacity = 0.25f;
            Pen.Prop_Scale = 1.0f;
            Pen.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Pen.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Pen.Prop_SecondaryColorGradient = false;
            Pen.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pen.Prop_SecondaryNoise = false;
            Pen.Prop_SecondaryNoiseOpacity = 0.25f;
            Pen.Prop_Shadow_Blur = 5;
            Pen.Prop_Shadow_Color = Color.Black;
            Pen.Prop_Shadow_Enabled = false;
            Pen.Prop_Shadow_OffsetX = 2;
            Pen.Prop_Shadow_OffsetY = 2;
            Pen.Prop_Shadow_Opacity = 0.3f;
            Pen.Size = new Size(64, 64);
            Pen.TabIndex = 14;
            // 
            // None
            // 
            None.Location = new Point(287, 77);
            None.Name = "None";
            None.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            None.Prop_CircleStyle = Paths.CircleStyle.Aero;
            None.Prop_Cursor = Paths.CursorType.None;
            None.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            None.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            None.Prop_LoadingCircleBackGradient = false;
            None.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_LoadingCircleBackNoise = false;
            None.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            None.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            None.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            None.Prop_LoadingCircleHotGradient = false;
            None.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_LoadingCircleHotNoise = false;
            None.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            None.Prop_PrimaryColor1 = Color.White;
            None.Prop_PrimaryColor2 = Color.White;
            None.Prop_PrimaryColorGradient = false;
            None.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_PrimaryNoise = false;
            None.Prop_PrimaryNoiseOpacity = 0.25f;
            None.Prop_Scale = 1.0f;
            None.Prop_SecondaryColor1 = Color.Red;
            None.Prop_SecondaryColor2 = Color.Red;
            None.Prop_SecondaryColorGradient = false;
            None.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            None.Prop_SecondaryNoise = false;
            None.Prop_SecondaryNoiseOpacity = 0.25f;
            None.Prop_Shadow_Blur = 5;
            None.Prop_Shadow_Color = Color.Black;
            None.Prop_Shadow_Enabled = false;
            None.Prop_Shadow_OffsetX = 2;
            None.Prop_Shadow_OffsetY = 2;
            None.Prop_Shadow_Opacity = 0.3f;
            None.Size = new Size(64, 64);
            None.TabIndex = 15;
            // 
            // Link
            // 
            Link.Location = new Point(357, 77);
            Link.Name = "Link";
            Link.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Link.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Link.Prop_Cursor = Paths.CursorType.Link;
            Link.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Link.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Link.Prop_LoadingCircleBackGradient = false;
            Link.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_LoadingCircleBackNoise = false;
            Link.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Link.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Link.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Link.Prop_LoadingCircleHotGradient = false;
            Link.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_LoadingCircleHotNoise = false;
            Link.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Link.Prop_PrimaryColor1 = Color.White;
            Link.Prop_PrimaryColor2 = Color.White;
            Link.Prop_PrimaryColorGradient = false;
            Link.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_PrimaryNoise = false;
            Link.Prop_PrimaryNoiseOpacity = 0.25f;
            Link.Prop_Scale = 1.0f;
            Link.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Link.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Link.Prop_SecondaryColorGradient = false;
            Link.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Link.Prop_SecondaryNoise = false;
            Link.Prop_SecondaryNoiseOpacity = 0.25f;
            Link.Prop_Shadow_Blur = 5;
            Link.Prop_Shadow_Color = Color.Black;
            Link.Prop_Shadow_Enabled = false;
            Link.Prop_Shadow_OffsetX = 2;
            Link.Prop_Shadow_OffsetY = 2;
            Link.Prop_Shadow_Opacity = 0.3f;
            Link.Size = new Size(64, 64);
            Link.TabIndex = 16;
            // 
            // Pin
            // 
            Pin.Location = new Point(427, 77);
            Pin.Name = "Pin";
            Pin.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Pin.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Pin.Prop_Cursor = Paths.CursorType.Pin;
            Pin.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Pin.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Pin.Prop_LoadingCircleBackGradient = false;
            Pin.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_LoadingCircleBackNoise = false;
            Pin.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Pin.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Pin.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Pin.Prop_LoadingCircleHotGradient = false;
            Pin.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_LoadingCircleHotNoise = false;
            Pin.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Pin.Prop_PrimaryColor1 = Color.White;
            Pin.Prop_PrimaryColor2 = Color.White;
            Pin.Prop_PrimaryColorGradient = false;
            Pin.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_PrimaryNoise = false;
            Pin.Prop_PrimaryNoiseOpacity = 0.25f;
            Pin.Prop_Scale = 1.0f;
            Pin.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Pin.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Pin.Prop_SecondaryColorGradient = false;
            Pin.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Pin.Prop_SecondaryNoise = false;
            Pin.Prop_SecondaryNoiseOpacity = 0.25f;
            Pin.Prop_Shadow_Blur = 5;
            Pin.Prop_Shadow_Color = Color.Black;
            Pin.Prop_Shadow_Enabled = false;
            Pin.Prop_Shadow_OffsetX = 2;
            Pin.Prop_Shadow_OffsetY = 2;
            Pin.Prop_Shadow_Opacity = 0.3f;
            Pin.Size = new Size(64, 64);
            Pin.TabIndex = 17;
            // 
            // Person
            // 
            Person.Location = new Point(7, 147);
            Person.Name = "Person";
            Person.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Person.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Person.Prop_Cursor = Paths.CursorType.Person;
            Person.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Person.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Person.Prop_LoadingCircleBackGradient = false;
            Person.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_LoadingCircleBackNoise = false;
            Person.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Person.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Person.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Person.Prop_LoadingCircleHotGradient = false;
            Person.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_LoadingCircleHotNoise = false;
            Person.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Person.Prop_PrimaryColor1 = Color.White;
            Person.Prop_PrimaryColor2 = Color.White;
            Person.Prop_PrimaryColorGradient = false;
            Person.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_PrimaryNoise = false;
            Person.Prop_PrimaryNoiseOpacity = 0.25f;
            Person.Prop_Scale = 1.0f;
            Person.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Person.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Person.Prop_SecondaryColorGradient = false;
            Person.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Person.Prop_SecondaryNoise = false;
            Person.Prop_SecondaryNoiseOpacity = 0.25f;
            Person.Prop_Shadow_Blur = 5;
            Person.Prop_Shadow_Color = Color.Black;
            Person.Prop_Shadow_Enabled = false;
            Person.Prop_Shadow_OffsetX = 2;
            Person.Prop_Shadow_OffsetY = 2;
            Person.Prop_Shadow_Opacity = 0.3f;
            Person.Size = new Size(64, 64);
            Person.TabIndex = 18;
            // 
            // IBeam
            // 
            IBeam.Location = new Point(77, 147);
            IBeam.Name = "IBeam";
            IBeam.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            IBeam.Prop_CircleStyle = Paths.CircleStyle.Aero;
            IBeam.Prop_Cursor = Paths.CursorType.IBeam;
            IBeam.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            IBeam.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            IBeam.Prop_LoadingCircleBackGradient = false;
            IBeam.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_LoadingCircleBackNoise = false;
            IBeam.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            IBeam.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            IBeam.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            IBeam.Prop_LoadingCircleHotGradient = false;
            IBeam.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_LoadingCircleHotNoise = false;
            IBeam.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            IBeam.Prop_PrimaryColor1 = Color.White;
            IBeam.Prop_PrimaryColor2 = Color.White;
            IBeam.Prop_PrimaryColorGradient = false;
            IBeam.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_PrimaryNoise = false;
            IBeam.Prop_PrimaryNoiseOpacity = 0.25f;
            IBeam.Prop_Scale = 1.0f;
            IBeam.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            IBeam.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            IBeam.Prop_SecondaryColorGradient = false;
            IBeam.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            IBeam.Prop_SecondaryNoise = false;
            IBeam.Prop_SecondaryNoiseOpacity = 0.25f;
            IBeam.Prop_Shadow_Blur = 5;
            IBeam.Prop_Shadow_Color = Color.Black;
            IBeam.Prop_Shadow_Enabled = false;
            IBeam.Prop_Shadow_OffsetX = 2;
            IBeam.Prop_Shadow_OffsetY = 2;
            IBeam.Prop_Shadow_Opacity = 0.3f;
            IBeam.Size = new Size(64, 64);
            IBeam.TabIndex = 19;
            // 
            // Cross
            // 
            Cross.Location = new Point(147, 147);
            Cross.Name = "Cross";
            Cross.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
            Cross.Prop_CircleStyle = Paths.CircleStyle.Aero;
            Cross.Prop_Cursor = Paths.CursorType.Cross;
            Cross.Prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
            Cross.Prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
            Cross.Prop_LoadingCircleBackGradient = false;
            Cross.Prop_LoadingCircleBackGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_LoadingCircleBackNoise = false;
            Cross.Prop_LoadingCircleBackNoiseOpacity = 0.25f;
            Cross.Prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
            Cross.Prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
            Cross.Prop_LoadingCircleHotGradient = false;
            Cross.Prop_LoadingCircleHotGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_LoadingCircleHotNoise = false;
            Cross.Prop_LoadingCircleHotNoiseOpacity = 0.25f;
            Cross.Prop_PrimaryColor1 = Color.White;
            Cross.Prop_PrimaryColor2 = Color.White;
            Cross.Prop_PrimaryColorGradient = false;
            Cross.Prop_PrimaryColorGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_PrimaryNoise = false;
            Cross.Prop_PrimaryNoiseOpacity = 0.25f;
            Cross.Prop_Scale = 1.0f;
            Cross.Prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
            Cross.Prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
            Cross.Prop_SecondaryColorGradient = false;
            Cross.Prop_SecondaryColorGradientMode = Paths.GradientMode.Horizontal;
            Cross.Prop_SecondaryNoise = false;
            Cross.Prop_SecondaryNoiseOpacity = 0.25f;
            Cross.Prop_Shadow_Blur = 5;
            Cross.Prop_Shadow_Color = Color.Black;
            Cross.Prop_Shadow_Enabled = false;
            Cross.Prop_Shadow_OffsetX = 2;
            Cross.Prop_Shadow_OffsetY = 2;
            Cross.Prop_Shadow_Opacity = 0.3f;
            Cross.Size = new Size(64, 64);
            Cross.TabIndex = 20;
            // 
            // cur_tip_btn
            // 
            cur_tip_btn.BackColor = Color.FromArgb(34, 34, 34);
            cur_tip_btn.DrawOnGlass = false;
            cur_tip_btn.Font = new Font("Segoe UI", 9.0f);
            cur_tip_btn.ForeColor = Color.White;
            cur_tip_btn.Image = null;
            cur_tip_btn.LineColor = Color.FromArgb(199, 49, 61);
            cur_tip_btn.Location = new Point(500, 267);
            cur_tip_btn.Name = "cur_tip_btn";
            cur_tip_btn.Size = new Size(20, 21);
            cur_tip_btn.TabIndex = 71;
            cur_tip_btn.Text = "?";
            cur_tip_btn.UseVisualStyleBackColor = false;
            // 
            // PictureBox12
            // 
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(6, 265);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.TabIndex = 70;
            PictureBox12.TabStop = false;
            // 
            // CursorsSize_Bar
            // 
            CursorsSize_Bar.LargeChange = 50;
            CursorsSize_Bar.Location = new Point(127, 268);
            CursorsSize_Bar.Maximum = 320;
            CursorsSize_Bar.Minimum = 100;
            CursorsSize_Bar.Name = "CursorsSize_Bar";
            CursorsSize_Bar.Size = new Size(223, 19);
            CursorsSize_Bar.SmallChange = 20;
            CursorsSize_Bar.TabIndex = 68;
            CursorsSize_Bar.Value = 100;
            // 
            // Label17
            // 
            Label17.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label17.Location = new Point(36, 265);
            Label17.Name = "Label17";
            Label17.Size = new Size(85, 24);
            Label17.TabIndex = 69;
            Label17.Text = "Scaling (1x)";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(search_results);
            TabPage5.Location = new Point(4, 24);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(10);
            TabPage5.Size = new Size(1321, 599);
            TabPage5.TabIndex = 3;
            TabPage5.Text = "2";
            // 
            // search_results
            // 
            search_results.AutoScroll = true;
            search_results.Dock = DockStyle.Fill;
            search_results.Location = new Point(10, 10);
            search_results.Name = "search_results";
            search_results.Size = new Size(1301, 579);
            search_results.TabIndex = 4;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(StopTimer_btn);
            TabPage2.Controls.Add(ExportDetails_btn);
            TabPage2.Controls.Add(log_lbl);
            TabPage2.Controls.Add(ShowErrors_btn);
            TabPage2.Controls.Add(ok_btn);
            TabPage2.Controls.Add(log);
            TabPage2.Controls.Add(Separator1);
            TabPage2.Controls.Add(log_header);
            TabPage2.Controls.Add(PictureBox36);
            TabPage2.Location = new Point(4, 24);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(10);
            TabPage2.Size = new Size(1321, 599);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "3";
            // 
            // StopTimer_btn
            // 
            StopTimer_btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StopTimer_btn.BackColor = Color.FromArgb(34, 34, 34);
            StopTimer_btn.DrawOnGlass = false;
            StopTimer_btn.Font = new Font("Segoe UI", 9.0f);
            StopTimer_btn.ForeColor = Color.White;
            StopTimer_btn.Image = null;
            StopTimer_btn.LineColor = Color.FromArgb(0, 81, 210);
            StopTimer_btn.Location = new Point(1080, 552);
            StopTimer_btn.Name = "StopTimer_btn";
            StopTimer_btn.Size = new Size(80, 34);
            StopTimer_btn.TabIndex = 31;
            StopTimer_btn.Text = "Stop timer";
            StopTimer_btn.UseVisualStyleBackColor = false;
            StopTimer_btn.Visible = false;
            // 
            // ExportDetails_btn
            // 
            ExportDetails_btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ExportDetails_btn.BackColor = Color.FromArgb(34, 34, 34);
            ExportDetails_btn.DrawOnGlass = false;
            ExportDetails_btn.Font = new Font("Segoe UI", 9.0f);
            ExportDetails_btn.ForeColor = Color.White;
            ExportDetails_btn.Image = null;
            ExportDetails_btn.LineColor = Color.FromArgb(0, 81, 210);
            ExportDetails_btn.Location = new Point(1162, 552);
            ExportDetails_btn.Name = "ExportDetails_btn";
            ExportDetails_btn.Size = new Size(85, 34);
            ExportDetails_btn.TabIndex = 30;
            ExportDetails_btn.Text = "Export details";
            ExportDetails_btn.UseVisualStyleBackColor = false;
            ExportDetails_btn.Visible = false;
            // 
            // log_lbl
            // 
            log_lbl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            log_lbl.BackColor = Color.Transparent;
            log_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold);
            log_lbl.Location = new Point(13, 552);
            log_lbl.Name = "log_lbl";
            log_lbl.Padding = new Padding(5, 0, 0, 0);
            log_lbl.Size = new Size(977, 34);
            log_lbl.TabIndex = 29;
            log_lbl.Text = @"Error\s happened. Press on 'Show errors' for details";
            log_lbl.TextAlign = ContentAlignment.MiddleLeft;
            log_lbl.Visible = false;
            // 
            // ShowErrors_btn
            // 
            ShowErrors_btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ShowErrors_btn.BackColor = Color.FromArgb(34, 34, 34);
            ShowErrors_btn.DrawOnGlass = false;
            ShowErrors_btn.Font = new Font("Segoe UI", 9.0f);
            ShowErrors_btn.ForeColor = Color.White;
            ShowErrors_btn.Image = null;
            ShowErrors_btn.LineColor = Color.FromArgb(0, 81, 210);
            ShowErrors_btn.Location = new Point(996, 552);
            ShowErrors_btn.Name = "ShowErrors_btn";
            ShowErrors_btn.Size = new Size(82, 34);
            ShowErrors_btn.TabIndex = 28;
            ShowErrors_btn.Text = "Show errors";
            ShowErrors_btn.UseVisualStyleBackColor = false;
            ShowErrors_btn.Visible = false;
            // 
            // ok_btn
            // 
            ok_btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ok_btn.BackColor = Color.FromArgb(34, 34, 34);
            ok_btn.DrawOnGlass = false;
            ok_btn.Font = new Font("Segoe UI", 9.0f);
            ok_btn.ForeColor = Color.White;
            ok_btn.Image = null;
            ok_btn.LineColor = Color.FromArgb(0, 81, 210);
            ok_btn.Location = new Point(1249, 552);
            ok_btn.Name = "ok_btn";
            ok_btn.Size = new Size(59, 34);
            ok_btn.TabIndex = 27;
            ok_btn.Text = "OK";
            ok_btn.UseVisualStyleBackColor = false;
            // 
            // log
            // 
            log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            log.BackColor = Color.FromArgb(35, 35, 35);
            log.BorderStyle = BorderStyle.None;
            log.ForeColor = Color.White;
            log.FullRowSelect = true;
            log.ItemHeight = 28;
            log.Location = new Point(13, 61);
            log.Name = "log";
            log.ShowLines = false;
            log.Size = new Size(1295, 485);
            log.TabIndex = 26;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(13, 54);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(1220, 1);
            Separator1.TabIndex = 25;
            Separator1.TabStop = false;
            // 
            // log_header
            // 
            log_header.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            log_header.BackColor = Color.Transparent;
            log_header.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            log_header.Location = new Point(54, 13);
            log_header.Name = "log_header";
            log_header.Size = new Size(1179, 35);
            log_header.TabIndex = 24;
            log_header.Text = "Log";
            log_header.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox36
            // 
            PictureBox36.BackColor = Color.Transparent;
            PictureBox36.Image = (Image)resources.GetObject("PictureBox36.Image");
            PictureBox36.Location = new Point(13, 13);
            PictureBox36.Name = "PictureBox36";
            PictureBox36.Size = new Size(35, 35);
            PictureBox36.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox36.TabIndex = 23;
            PictureBox36.TabStop = false;
            // 
            // Store
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(1329, 721);
            Controls.Add(Tabs);
            Controls.Add(Titlebar_panel);
            Controls.Add(Status_pnl);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 2, 5, 2);
            MinimumSize = new Size(1130, 680);
            Name = "Store";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinPaletter Store";
            Titlebar_panel.ResumeLayout(false);
            search_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Titlebar_img).EndInit();
            Status_pnl.ResumeLayout(false);
            Tabs.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            TabPage3.ResumeLayout(false);
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            GroupBox1.ResumeLayout(false);
            previewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            FlowLayoutPanel1.ResumeLayout(false);
            tabs_preview.ResumeLayout(false);
            TabPage4.ResumeLayout(false);
            pnl_preview.ResumeLayout(false);
            Window1.ResumeLayout(false);
            Panel3.ResumeLayout(false);
            TabPage6.ResumeLayout(false);
            pnl_preview_classic.ResumeLayout(false);
            ClassicTaskbar.ResumeLayout(false);
            ClassicColorsPreview.ResumeLayout(false);
            ClassicColorsPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RetroShadow1).EndInit();
            Menu_Window.ResumeLayout(false);
            menucontainer3.ResumeLayout(false);
            highlight.ResumeLayout(false);
            menuhilight.ResumeLayout(false);
            menucontainer1.ResumeLayout(false);
            WindowR3.ResumeLayout(false);
            WindowR3.PerformLayout();
            WindowR2.ResumeLayout(false);
            menucontainer0.ResumeLayout(false);
            PanelR1.ResumeLayout(false);
            WindowR4.ResumeLayout(false);
            programcontainer.ResumeLayout(false);
            PanelR2.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            Cursors_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            TabPage5.ResumeLayout(false);
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox36).EndInit();
            Load += new EventHandler(Store_Load);
            Shown += new EventHandler(Store_Shown);
            FormClosing += new FormClosingEventHandler(Store_FormClosing);
            ResumeLayout(false);

        }
        internal UI.WP.TablessControl Tabs;
        internal TabPage TabPage2;
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
        internal UI.WP.Button StopTimer_btn;
        internal UI.WP.Button ExportDetails_btn;
        internal Label log_lbl;
        internal UI.WP.Button ShowErrors_btn;
        internal UI.WP.Button ok_btn;
        internal TreeView log;
        internal UI.WP.SeparatorH Separator1;
        internal Label log_header;
        internal PictureBox PictureBox36;
        internal Timer Log_Timer;
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
        internal CursorControl Arrow;
        internal CursorControl Help;
        internal CursorControl AppLoading;
        internal CursorControl Busy;
        internal CursorControl Move_Cur;
        internal CursorControl Up;
        internal CursorControl NS;
        internal CursorControl EW;
        internal CursorControl NESW;
        internal CursorControl NWSE;
        internal CursorControl Pen;
        internal CursorControl None;
        internal CursorControl Link;
        internal CursorControl Pin;
        internal CursorControl Person;
        internal CursorControl IBeam;
        internal CursorControl Cross;
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