<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Store
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Store))
        Me.FilesFetcher = New System.ComponentModel.BackgroundWorker()
        Me.Titlebar_panel = New System.Windows.Forms.Panel()
        Me.search_panel = New System.Windows.Forms.Panel()
        Me.search_btn = New WinPaletter.UI.WP.Button()
        Me.search_box = New WinPaletter.UI.WP.TextBox()
        Me.search_filter_btn = New WinPaletter.UI.WP.Button()
        Me.Titlebar_lbl = New WinPaletter.UI.WP.LabelAlt()
        Me.back_btn = New WinPaletter.UI.WP.Button()
        Me.Titlebar_img = New System.Windows.Forms.PictureBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Log_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Cursor_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Status_pnl = New System.Windows.Forms.Panel()
        Me.Status_lbl = New System.Windows.Forms.Label()
        Me.Tabs = New WinPaletter.UI.WP.TablessControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.store_container = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.VersionAlert_lbl = New WinPaletter.UI.WP.AlertBox()
        Me.GroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.SupportedOS_lbl = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.PictureBox14 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New WinPaletter.UI.WP.GroupBox()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Theme_MD5_lbl = New System.Windows.Forms.Label()
        Me.desc_txt = New WinPaletter.UI.WP.TextBox()
        Me.author_url_button = New WinPaletter.UI.WP.Button()
        Me.RestartExplorer = New WinPaletter.UI.WP.Button()
        Me.SeparatorVertical1 = New WinPaletter.UI.WP.SeparatorV()
        Me.StoreItem1 = New WinPaletter.UI.Controllers.StoreItem()
        Me.themeSize_lbl = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Apply_btn = New WinPaletter.UI.WP.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Edit_btn = New WinPaletter.UI.WP.Button()
        Me.respacksize_lbl = New System.Windows.Forms.Label()
        Me.previewContainer = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox41 = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.tabs_preview = New WinPaletter.UI.WP.TablessControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.pnl_preview = New System.Windows.Forms.Panel()
        Me.WXP_Alert2 = New WinPaletter.UI.WP.AlertBox()
        Me.ActionCenter = New WinPaletter.UI.Simulation.WinElement()
        Me.start = New WinPaletter.UI.Simulation.WinElement()
        Me.taskbar = New WinPaletter.UI.Simulation.WinElement()
        Me.Window2 = New WinPaletter.UI.Simulation.Window()
        Me.Window1 = New WinPaletter.UI.Simulation.Window()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label8 = New WinPaletter.UI.WP.LabelAlt()
        Me.setting_icon_preview = New WinPaletter.UI.WP.LabelAlt()
        Me.lnk_preview = New WinPaletter.UI.WP.LabelAlt()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.pnl_preview_classic = New System.Windows.Forms.Panel()
        Me.ClassicWindow1 = New WinPaletter.UI.Retro.WindowR()
        Me.ClassicWindow2 = New WinPaletter.UI.Retro.WindowR()
        Me.ClassicTaskbar = New WinPaletter.UI.Retro.PanelRaisedR()
        Me.ButtonR4 = New WinPaletter.UI.Retro.ButtonR()
        Me.ButtonR3 = New WinPaletter.UI.Retro.ButtonR()
        Me.ButtonR2 = New WinPaletter.UI.Retro.ButtonR()
        Me.ClassicColorsPreview = New System.Windows.Forms.Panel()
        Me.RetroShadow1 = New WinPaletter.UI.WP.TransparentPictureBox()
        Me.Menu_Window = New WinPaletter.UI.Retro.WindowR()
        Me.menucontainer3 = New System.Windows.Forms.Panel()
        Me.LabelR9 = New WinPaletter.UI.WP.LabelAlt()
        Me.highlight = New System.Windows.Forms.Panel()
        Me.menuhilight = New System.Windows.Forms.Panel()
        Me.LabelR5 = New WinPaletter.UI.WP.LabelAlt()
        Me.menucontainer1 = New System.Windows.Forms.Panel()
        Me.LabelR6 = New WinPaletter.UI.WP.LabelAlt()
        Me.WindowR3 = New WinPaletter.UI.Retro.WindowR()
        Me.ButtonR5 = New WinPaletter.UI.Retro.ButtonR()
        Me.LabelR4 = New WinPaletter.UI.WP.LabelAlt()
        Me.LabelR13 = New WinPaletter.UI.WP.LabelAlt()
        Me.WindowR2 = New WinPaletter.UI.Retro.WindowR()
        Me.TextBoxR1 = New WinPaletter.UI.Retro.TextBoxR()
        Me.menucontainer0 = New System.Windows.Forms.Panel()
        Me.PanelR1 = New WinPaletter.UI.Retro.PanelR()
        Me.LabelR3 = New WinPaletter.UI.WP.LabelAlt()
        Me.LabelR2 = New WinPaletter.UI.WP.LabelAlt()
        Me.LabelR1 = New WinPaletter.UI.WP.LabelAlt()
        Me.WindowR1 = New WinPaletter.UI.Retro.WindowR()
        Me.WindowR4 = New WinPaletter.UI.Retro.WindowR()
        Me.programcontainer = New System.Windows.Forms.Panel()
        Me.PanelR2 = New WinPaletter.UI.Retro.ScrollBarR()
        Me.ButtonR12 = New WinPaletter.UI.Retro.ButtonR()
        Me.ButtonR11 = New WinPaletter.UI.Retro.ButtonR()
        Me.ButtonR10 = New WinPaletter.UI.Retro.ButtonR()
        Me.CMD1 = New WinPaletter.UI.Simulation.WinCMD()
        Me.CMD2 = New WinPaletter.UI.Simulation.WinCMD()
        Me.CMD3 = New WinPaletter.UI.Simulation.WinCMD()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cur_anim_btn = New WinPaletter.UI.WP.Button()
        Me.Cursors_Container = New System.Windows.Forms.FlowLayoutPanel()
        Me.Arrow = New WinPaletter.CursorControl()
        Me.Help = New WinPaletter.CursorControl()
        Me.AppLoading = New WinPaletter.CursorControl()
        Me.Busy = New WinPaletter.CursorControl()
        Me.Move_Cur = New WinPaletter.CursorControl()
        Me.Up = New WinPaletter.CursorControl()
        Me.NS = New WinPaletter.CursorControl()
        Me.EW = New WinPaletter.CursorControl()
        Me.NESW = New WinPaletter.CursorControl()
        Me.NWSE = New WinPaletter.CursorControl()
        Me.Pen = New WinPaletter.CursorControl()
        Me.None = New WinPaletter.CursorControl()
        Me.Link = New WinPaletter.CursorControl()
        Me.Pin = New WinPaletter.CursorControl()
        Me.Person = New WinPaletter.CursorControl()
        Me.IBeam = New WinPaletter.CursorControl()
        Me.Cross = New WinPaletter.CursorControl()
        Me.cur_tip_btn = New WinPaletter.UI.WP.Button()
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.CursorsSize_Bar = New WinPaletter.UI.WP.Trackbar()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.search_results = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.StopTimer_btn = New WinPaletter.UI.WP.Button()
        Me.ExportDetails_btn = New WinPaletter.UI.WP.Button()
        Me.log_lbl = New System.Windows.Forms.Label()
        Me.ShowErrors_btn = New WinPaletter.UI.WP.Button()
        Me.ok_btn = New WinPaletter.UI.WP.Button()
        Me.log = New System.Windows.Forms.TreeView()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.log_header = New System.Windows.Forms.Label()
        Me.PictureBox36 = New System.Windows.Forms.PictureBox()
        Me.Titlebar_panel.SuspendLayout()
        Me.search_panel.SuspendLayout()
        CType(Me.Titlebar_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Status_pnl.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.previewContainer.SuspendLayout()
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.tabs_preview.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.pnl_preview.SuspendLayout()
        Me.Window1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.pnl_preview_classic.SuspendLayout()
        Me.ClassicTaskbar.SuspendLayout()
        Me.ClassicColorsPreview.SuspendLayout()
        CType(Me.RetroShadow1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Menu_Window.SuspendLayout()
        Me.menucontainer3.SuspendLayout()
        Me.highlight.SuspendLayout()
        Me.menuhilight.SuspendLayout()
        Me.menucontainer1.SuspendLayout()
        Me.WindowR3.SuspendLayout()
        Me.WindowR2.SuspendLayout()
        Me.menucontainer0.SuspendLayout()
        Me.PanelR1.SuspendLayout()
        Me.WindowR4.SuspendLayout()
        Me.programcontainer.SuspendLayout()
        Me.PanelR2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Cursors_Container.SuspendLayout()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox36, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FilesFetcher
        '
        Me.FilesFetcher.WorkerReportsProgress = True
        Me.FilesFetcher.WorkerSupportsCancellation = True
        '
        'Titlebar_panel
        '
        Me.Titlebar_panel.Controls.Add(Me.search_panel)
        Me.Titlebar_panel.Controls.Add(Me.Titlebar_lbl)
        Me.Titlebar_panel.Controls.Add(Me.back_btn)
        Me.Titlebar_panel.Controls.Add(Me.Titlebar_img)
        Me.Titlebar_panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.Titlebar_panel.Location = New System.Drawing.Point(0, 0)
        Me.Titlebar_panel.Name = "Titlebar_panel"
        Me.Titlebar_panel.Size = New System.Drawing.Size(1329, 70)
        Me.Titlebar_panel.TabIndex = 5
        '
        'search_panel
        '
        Me.search_panel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.search_panel.BackColor = System.Drawing.Color.Transparent
        Me.search_panel.Controls.Add(Me.search_btn)
        Me.search_panel.Controls.Add(Me.search_box)
        Me.search_panel.Controls.Add(Me.search_filter_btn)
        Me.search_panel.Location = New System.Drawing.Point(979, 17)
        Me.search_panel.Name = "search_panel"
        Me.search_panel.Size = New System.Drawing.Size(343, 30)
        Me.search_panel.TabIndex = 42
        '
        'search_btn
        '
        Me.search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.search_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.search_btn.DrawOnGlass = True
        Me.search_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.search_btn.ForeColor = System.Drawing.Color.White
        Me.search_btn.Image = CType(resources.GetObject("search_btn.Image"), System.Drawing.Image)
        Me.search_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.search_btn.Location = New System.Drawing.Point(308, 3)
        Me.search_btn.Name = "search_btn"
        Me.search_btn.Size = New System.Drawing.Size(32, 24)
        Me.search_btn.TabIndex = 40
        Me.search_btn.UseVisualStyleBackColor = False
        '
        'search_box
        '
        Me.search_box.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.search_box.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.search_box.DrawOnGlass = True
        Me.search_box.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.search_box.Location = New System.Drawing.Point(3, 3)
        Me.search_box.MaxLength = 32767
        Me.search_box.Multiline = False
        Me.search_box.Name = "search_box"
        Me.search_box.ReadOnly = False
        Me.search_box.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.search_box.SelectedText = ""
        Me.search_box.SelectionLength = 0
        Me.search_box.SelectionStart = 0
        Me.search_box.Size = New System.Drawing.Size(261, 24)
        Me.search_box.TabIndex = 39
        Me.search_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.search_box.UseSystemPasswordChar = False
        Me.search_box.WordWrap = True
        '
        'search_filter_btn
        '
        Me.search_filter_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.search_filter_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.search_filter_btn.DrawOnGlass = True
        Me.search_filter_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.search_filter_btn.ForeColor = System.Drawing.Color.White
        Me.search_filter_btn.Image = CType(resources.GetObject("search_filter_btn.Image"), System.Drawing.Image)
        Me.search_filter_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.search_filter_btn.Location = New System.Drawing.Point(270, 3)
        Me.search_filter_btn.Name = "search_filter_btn"
        Me.search_filter_btn.Size = New System.Drawing.Size(32, 24)
        Me.search_filter_btn.TabIndex = 41
        Me.search_filter_btn.UseVisualStyleBackColor = False
        '
        'Titlebar_lbl
        '
        Me.Titlebar_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Titlebar_lbl.BackColor = System.Drawing.Color.Transparent
        Me.Titlebar_lbl.DrawOnGlass = True
        Me.Titlebar_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Titlebar_lbl.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Titlebar_lbl.Location = New System.Drawing.Point(78, 1)
        Me.Titlebar_lbl.Name = "Titlebar_lbl"
        Me.Titlebar_lbl.Size = New System.Drawing.Size(895, 64)
        Me.Titlebar_lbl.TabIndex = 38
        Me.Titlebar_lbl.Text = "WinPaletter Store"
        Me.Titlebar_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'back_btn
        '
        Me.back_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.back_btn.DrawOnGlass = True
        Me.back_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.back_btn.ForeColor = System.Drawing.Color.White
        Me.back_btn.Image = Nothing
        Me.back_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.back_btn.Location = New System.Drawing.Point(8, 0)
        Me.back_btn.Name = "back_btn"
        Me.back_btn.Size = New System.Drawing.Size(64, 64)
        Me.back_btn.TabIndex = 36
        Me.back_btn.UseVisualStyleBackColor = False
        Me.back_btn.Visible = False
        '
        'Titlebar_img
        '
        Me.Titlebar_img.BackColor = System.Drawing.Color.Transparent
        Me.Titlebar_img.Image = CType(resources.GetObject("Titlebar_img.Image"), System.Drawing.Image)
        Me.Titlebar_img.Location = New System.Drawing.Point(5, 0)
        Me.Titlebar_img.Name = "Titlebar_img"
        Me.Titlebar_img.Size = New System.Drawing.Size(64, 64)
        Me.Titlebar_img.TabIndex = 37
        Me.Titlebar_img.TabStop = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ProgressBar1.Location = New System.Drawing.Point(1098, 3)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(228, 18)
        Me.ProgressBar1.TabIndex = 43
        Me.ProgressBar1.Visible = False
        '
        'Log_Timer
        '
        Me.Log_Timer.Interval = 1000
        '
        'Cursor_Timer
        '
        Me.Cursor_Timer.Interval = 35
        '
        'Status_pnl
        '
        Me.Status_pnl.BackColor = System.Drawing.Color.Transparent
        Me.Status_pnl.Controls.Add(Me.Status_lbl)
        Me.Status_pnl.Controls.Add(Me.ProgressBar1)
        Me.Status_pnl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Status_pnl.Location = New System.Drawing.Point(0, 697)
        Me.Status_pnl.Name = "Status_pnl"
        Me.Status_pnl.Padding = New System.Windows.Forms.Padding(3)
        Me.Status_pnl.Size = New System.Drawing.Size(1329, 24)
        Me.Status_pnl.TabIndex = 6
        '
        'Status_lbl
        '
        Me.Status_lbl.AutoEllipsis = True
        Me.Status_lbl.BackColor = System.Drawing.Color.Transparent
        Me.Status_lbl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Status_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Status_lbl.Location = New System.Drawing.Point(3, 3)
        Me.Status_lbl.Name = "Status_lbl"
        Me.Status_lbl.Size = New System.Drawing.Size(1095, 18)
        Me.Status_lbl.TabIndex = 39
        Me.Status_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Tabs
        '
        Me.Tabs.Controls.Add(Me.TabPage1)
        Me.Tabs.Controls.Add(Me.TabPage3)
        Me.Tabs.Controls.Add(Me.TabPage5)
        Me.Tabs.Controls.Add(Me.TabPage2)
        Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabs.Location = New System.Drawing.Point(0, 70)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(1329, 627)
        Me.Tabs.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.store_container)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage1.Size = New System.Drawing.Size(1321, 599)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "0"
        '
        'store_container
        '
        Me.store_container.AutoScroll = True
        Me.store_container.Dock = System.Windows.Forms.DockStyle.Fill
        Me.store_container.Location = New System.Drawing.Point(10, 10)
        Me.store_container.Name = "store_container"
        Me.store_container.Size = New System.Drawing.Size(1301, 579)
        Me.store_container.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.VersionAlert_lbl)
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Controls.Add(Me.GroupBox1)
        Me.TabPage3.Controls.Add(Me.previewContainer)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage3.Size = New System.Drawing.Size(1321, 599)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "1"
        '
        'VersionAlert_lbl
        '
        Me.VersionAlert_lbl.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive
        Me.VersionAlert_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionAlert_lbl.BackColor = System.Drawing.Color.FromArgb(CType(CType(91, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(6, Byte), Integer))
        Me.VersionAlert_lbl.CenterText = True
        Me.VersionAlert_lbl.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.VersionAlert_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.VersionAlert_lbl.Image = CType(resources.GetObject("VersionAlert_lbl.Image"), System.Drawing.Image)
        Me.VersionAlert_lbl.Location = New System.Drawing.Point(399, 450)
        Me.VersionAlert_lbl.Name = "VersionAlert_lbl"
        Me.VersionAlert_lbl.Size = New System.Drawing.Size(908, 34)
        Me.VersionAlert_lbl.TabIndex = 140
        Me.VersionAlert_lbl.TabStop = False
        Me.VersionAlert_lbl.Text = "0"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.SupportedOS_lbl)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.PictureBox14)
        Me.GroupBox3.Location = New System.Drawing.Point(399, 389)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(908, 55)
        Me.GroupBox3.TabIndex = 140
        Me.GroupBox3.Text = "GroupBox3"
        '
        'SupportedOS_lbl
        '
        Me.SupportedOS_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SupportedOS_lbl.BackColor = System.Drawing.Color.Transparent
        Me.SupportedOS_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SupportedOS_lbl.Location = New System.Drawing.Point(33, 27)
        Me.SupportedOS_lbl.Name = "SupportedOS_lbl"
        Me.SupportedOS_lbl.Size = New System.Drawing.Size(871, 24)
        Me.SupportedOS_lbl.TabIndex = 8
        Me.SupportedOS_lbl.Text = "0"
        Me.SupportedOS_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label26
        '
        Me.Label26.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(33, 3)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(871, 24)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "0"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox14
        '
        Me.PictureBox14.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox14.Image = CType(resources.GetObject("PictureBox14.Image"), System.Drawing.Image)
        Me.PictureBox14.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox14.Name = "PictureBox14"
        Me.PictureBox14.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox14.TabIndex = 0
        Me.PictureBox14.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Theme_MD5_lbl)
        Me.GroupBox1.Controls.Add(Me.desc_txt)
        Me.GroupBox1.Controls.Add(Me.author_url_button)
        Me.GroupBox1.Controls.Add(Me.RestartExplorer)
        Me.GroupBox1.Controls.Add(Me.SeparatorVertical1)
        Me.GroupBox1.Controls.Add(Me.StoreItem1)
        Me.GroupBox1.Controls.Add(Me.themeSize_lbl)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Apply_btn)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Edit_btn)
        Me.GroupBox1.Controls.Add(Me.respacksize_lbl)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(380, 573)
        Me.GroupBox1.TabIndex = 139
        Me.GroupBox1.Text = "GroupBox1"
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(193, 234)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 34)
        Me.Button1.TabIndex = 146
        Me.Button1.Text = "Save as ..."
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Theme_MD5_lbl
        '
        Me.Theme_MD5_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Theme_MD5_lbl.BackColor = System.Drawing.Color.Transparent
        Me.Theme_MD5_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Theme_MD5_lbl.Location = New System.Drawing.Point(14, 537)
        Me.Theme_MD5_lbl.Name = "Theme_MD5_lbl"
        Me.Theme_MD5_lbl.Size = New System.Drawing.Size(322, 24)
        Me.Theme_MD5_lbl.TabIndex = 145
        Me.Theme_MD5_lbl.Text = "0"
        Me.Theme_MD5_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'desc_txt
        '
        Me.desc_txt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.desc_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.desc_txt.DrawOnGlass = False
        Me.desc_txt.ForeColor = System.Drawing.Color.White
        Me.desc_txt.Location = New System.Drawing.Point(14, 382)
        Me.desc_txt.MaxLength = 32767
        Me.desc_txt.Multiline = True
        Me.desc_txt.Name = "desc_txt"
        Me.desc_txt.ReadOnly = True
        Me.desc_txt.Scrollbars = System.Windows.Forms.ScrollBars.Vertical
        Me.desc_txt.SelectedText = ""
        Me.desc_txt.SelectionLength = 0
        Me.desc_txt.SelectionStart = 0
        Me.desc_txt.Size = New System.Drawing.Size(352, 145)
        Me.desc_txt.TabIndex = 7
        Me.desc_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.desc_txt.UseSystemPasswordChar = False
        Me.desc_txt.WordWrap = True
        '
        'author_url_button
        '
        Me.author_url_button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.author_url_button.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.author_url_button.DrawOnGlass = False
        Me.author_url_button.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.author_url_button.ForeColor = System.Drawing.Color.White
        Me.author_url_button.Image = Nothing
        Me.author_url_button.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.author_url_button.Location = New System.Drawing.Point(342, 537)
        Me.author_url_button.Name = "author_url_button"
        Me.author_url_button.Size = New System.Drawing.Size(24, 24)
        Me.author_url_button.TabIndex = 144
        Me.author_url_button.Text = "↗"
        Me.author_url_button.UseVisualStyleBackColor = False
        '
        'RestartExplorer
        '
        Me.RestartExplorer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RestartExplorer.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.RestartExplorer.DrawOnGlass = False
        Me.RestartExplorer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RestartExplorer.ForeColor = System.Drawing.Color.White
        Me.RestartExplorer.Image = Nothing
        Me.RestartExplorer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RestartExplorer.LineColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.RestartExplorer.Location = New System.Drawing.Point(31, 274)
        Me.RestartExplorer.Name = "RestartExplorer"
        Me.RestartExplorer.Size = New System.Drawing.Size(318, 34)
        Me.RestartExplorer.TabIndex = 138
        Me.RestartExplorer.Text = "Restart Explorer"
        Me.RestartExplorer.UseVisualStyleBackColor = False
        '
        'SeparatorVertical1
        '
        Me.SeparatorVertical1.AlternativeLook = False
        Me.SeparatorVertical1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SeparatorVertical1.Location = New System.Drawing.Point(190, 320)
        Me.SeparatorVertical1.Name = "SeparatorVertical1"
        Me.SeparatorVertical1.Size = New System.Drawing.Size(1, 46)
        Me.SeparatorVertical1.TabIndex = 143
        Me.SeparatorVertical1.TabStop = False
        Me.SeparatorVertical1.Text = "SeparatorVertical1"
        '
        'StoreItem1
        '
        Me.StoreItem1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.StoreItem1.CP = Nothing
        Me.StoreItem1.DoneByWinPaletter = False
        Me.StoreItem1.FileName = Nothing
        Me.StoreItem1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.StoreItem1.Location = New System.Drawing.Point(32, 7)
        Me.StoreItem1.MD5_PackFile = Nothing
        Me.StoreItem1.MD5_ThemeFile = Nothing
        Me.StoreItem1.Name = "StoreItem1"
        Me.StoreItem1.Size = New System.Drawing.Size(317, 178)
        Me.StoreItem1.TabIndex = 142
        Me.StoreItem1.URL_PackFile = Nothing
        Me.StoreItem1.URL_ThemeFile = Nothing
        '
        'themeSize_lbl
        '
        Me.themeSize_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.themeSize_lbl.BackColor = System.Drawing.Color.Transparent
        Me.themeSize_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.themeSize_lbl.Location = New System.Drawing.Point(46, 320)
        Me.themeSize_lbl.Name = "themeSize_lbl"
        Me.themeSize_lbl.Size = New System.Drawing.Size(138, 24)
        Me.themeSize_lbl.TabIndex = 13
        Me.themeSize_lbl.Text = "0"
        Me.themeSize_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(46, 342)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(138, 24)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Size"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Apply_btn
        '
        Me.Apply_btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Apply_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Apply_btn.DrawOnGlass = False
        Me.Apply_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Apply_btn.ForeColor = System.Drawing.Color.White
        Me.Apply_btn.Image = Nothing
        Me.Apply_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Apply_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.Apply_btn.Location = New System.Drawing.Point(31, 194)
        Me.Apply_btn.Name = "Apply_btn"
        Me.Apply_btn.Size = New System.Drawing.Size(318, 34)
        Me.Apply_btn.TabIndex = 134
        Me.Apply_btn.Text = "Apply"
        Me.Apply_btn.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(197, 342)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 24)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Resources pack size"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Edit_btn
        '
        Me.Edit_btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Edit_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Edit_btn.DrawOnGlass = False
        Me.Edit_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Edit_btn.ForeColor = System.Drawing.Color.White
        Me.Edit_btn.Image = CType(resources.GetObject("Edit_btn.Image"), System.Drawing.Image)
        Me.Edit_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Edit_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.Edit_btn.Location = New System.Drawing.Point(31, 234)
        Me.Edit_btn.Name = "Edit_btn"
        Me.Edit_btn.Size = New System.Drawing.Size(156, 34)
        Me.Edit_btn.TabIndex = 137
        Me.Edit_btn.Text = "Edit"
        Me.Edit_btn.UseVisualStyleBackColor = False
        '
        'respacksize_lbl
        '
        Me.respacksize_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.respacksize_lbl.BackColor = System.Drawing.Color.Transparent
        Me.respacksize_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.respacksize_lbl.Location = New System.Drawing.Point(197, 320)
        Me.respacksize_lbl.Name = "respacksize_lbl"
        Me.respacksize_lbl.Size = New System.Drawing.Size(138, 24)
        Me.respacksize_lbl.TabIndex = 16
        Me.respacksize_lbl.Text = "0"
        Me.respacksize_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'previewContainer
        '
        Me.previewContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.previewContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.previewContainer.Controls.Add(Me.PictureBox41)
        Me.previewContainer.Controls.Add(Me.Label19)
        Me.previewContainer.Controls.Add(Me.FlowLayoutPanel1)
        Me.previewContainer.Location = New System.Drawing.Point(399, 13)
        Me.previewContainer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.previewContainer.Name = "previewContainer"
        Me.previewContainer.Padding = New System.Windows.Forms.Padding(1)
        Me.previewContainer.Size = New System.Drawing.Size(908, 370)
        Me.previewContainer.TabIndex = 131
        '
        'PictureBox41
        '
        Me.PictureBox41.Image = CType(resources.GetObject("PictureBox41.Image"), System.Drawing.Image)
        Me.PictureBox41.Location = New System.Drawing.Point(4, 4)
        Me.PictureBox41.Name = "PictureBox41"
        Me.PictureBox41.Size = New System.Drawing.Size(35, 32)
        Me.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox41.TabIndex = 4
        Me.PictureBox41.TabStop = False
        '
        'Label19
        '
        Me.Label19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(45, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(859, 31)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "Preview"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Controls.Add(Me.tabs_preview)
        Me.FlowLayoutPanel1.Controls.Add(Me.ClassicColorsPreview)
        Me.FlowLayoutPanel1.Controls.Add(Me.CMD1)
        Me.FlowLayoutPanel1.Controls.Add(Me.CMD2)
        Me.FlowLayoutPanel1.Controls.Add(Me.CMD3)
        Me.FlowLayoutPanel1.Controls.Add(Me.Panel1)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(4, 41)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(900, 324)
        Me.FlowLayoutPanel1.TabIndex = 139
        '
        'tabs_preview
        '
        Me.tabs_preview.Controls.Add(Me.TabPage4)
        Me.tabs_preview.Controls.Add(Me.TabPage6)
        Me.tabs_preview.Location = New System.Drawing.Point(3, 3)
        Me.tabs_preview.Name = "tabs_preview"
        Me.tabs_preview.SelectedIndex = 0
        Me.tabs_preview.Size = New System.Drawing.Size(528, 297)
        Me.tabs_preview.TabIndex = 140
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.pnl_preview)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(520, 269)
        Me.TabPage4.TabIndex = 0
        Me.TabPage4.Text = "0"
        '
        'pnl_preview
        '
        Me.pnl_preview.BackColor = System.Drawing.Color.Black
        Me.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_preview.Controls.Add(Me.WXP_Alert2)
        Me.pnl_preview.Controls.Add(Me.ActionCenter)
        Me.pnl_preview.Controls.Add(Me.start)
        Me.pnl_preview.Controls.Add(Me.taskbar)
        Me.pnl_preview.Controls.Add(Me.Window2)
        Me.pnl_preview.Controls.Add(Me.Window1)
        Me.pnl_preview.Location = New System.Drawing.Point(0, 0)
        Me.pnl_preview.Name = "pnl_preview"
        Me.pnl_preview.Size = New System.Drawing.Size(528, 297)
        Me.pnl_preview.TabIndex = 2
        '
        'WXP_Alert2
        '
        Me.WXP_Alert2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning
        Me.WXP_Alert2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WXP_Alert2.BackColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.WXP_Alert2.CenterText = True
        Me.WXP_Alert2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.WXP_Alert2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.WXP_Alert2.Image = Nothing
        Me.WXP_Alert2.Location = New System.Drawing.Point(7, 6)
        Me.WXP_Alert2.Name = "WXP_Alert2"
        Me.WXP_Alert2.Size = New System.Drawing.Size(22, 22)
        Me.WXP_Alert2.TabIndex = 54
        Me.WXP_Alert2.TabStop = False
        Me.WXP_Alert2.Text = Nothing
        Me.WXP_Alert2.Visible = False
        '
        'ActionCenter
        '
        Me.ActionCenter.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.ActionCenter.ActionCenterButton_Normal = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ActionCenter.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.ActionCenter.AppBackground = System.Drawing.Color.Empty
        Me.ActionCenter.AppUnderline = System.Drawing.Color.Empty
        Me.ActionCenter.BackColor = System.Drawing.Color.Transparent
        Me.ActionCenter.BackColorAlpha = 50
        Me.ActionCenter.Background = System.Drawing.Color.Empty
        Me.ActionCenter.Background2 = System.Drawing.Color.Empty
        Me.ActionCenter.BlurPower = 8
        Me.ActionCenter.DarkMode = True
        Me.ActionCenter.LinkColor = System.Drawing.Color.Empty
        Me.ActionCenter.Location = New System.Drawing.Point(400, 165)
        Me.ActionCenter.Name = "ActionCenter"
        Me.ActionCenter.NoisePower = 0.2!
        Me.ActionCenter.Padding = New System.Windows.Forms.Padding(2)
        Me.ActionCenter.Shadow = True
        Me.ActionCenter.Size = New System.Drawing.Size(120, 85)
        Me.ActionCenter.StartColor = System.Drawing.Color.Empty
        Me.ActionCenter.Style = WinPaletter.UI.Simulation.WinElement.Styles.ActionCenter11
        Me.ActionCenter.SuspendRefresh = False
        Me.ActionCenter.TabIndex = 5
        Me.ActionCenter.Transparency = True
        Me.ActionCenter.UseWin11ORB_WithWin10 = False
        Me.ActionCenter.UseWin11RoundedCorners_WithWin10_Level1 = False
        Me.ActionCenter.UseWin11RoundedCorners_WithWin10_Level2 = False
        Me.ActionCenter.Win7ColorBal = 100
        Me.ActionCenter.Win7GlowBal = 100
        '
        'start
        '
        Me.start.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.start.ActionCenterButton_Normal = System.Drawing.Color.Empty
        Me.start.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.start.AppBackground = System.Drawing.Color.Empty
        Me.start.AppUnderline = System.Drawing.Color.Empty
        Me.start.BackColor = System.Drawing.Color.Transparent
        Me.start.BackColorAlpha = 150
        Me.start.Background = System.Drawing.Color.Empty
        Me.start.Background2 = System.Drawing.Color.Empty
        Me.start.BlurPower = 7
        Me.start.DarkMode = True
        Me.start.LinkColor = System.Drawing.Color.Empty
        Me.start.Location = New System.Drawing.Point(7, 50)
        Me.start.Name = "start"
        Me.start.NoisePower = 0.2!
        Me.start.Padding = New System.Windows.Forms.Padding(2)
        Me.start.Shadow = True
        Me.start.Size = New System.Drawing.Size(135, 200)
        Me.start.StartColor = System.Drawing.Color.Empty
        Me.start.Style = WinPaletter.UI.Simulation.WinElement.Styles.Start11
        Me.start.SuspendRefresh = False
        Me.start.TabIndex = 1
        Me.start.Transparency = True
        Me.start.UseWin11ORB_WithWin10 = False
        Me.start.UseWin11RoundedCorners_WithWin10_Level1 = False
        Me.start.UseWin11RoundedCorners_WithWin10_Level2 = False
        Me.start.Win7ColorBal = 100
        Me.start.Win7GlowBal = 100
        '
        'taskbar
        '
        Me.taskbar.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.taskbar.ActionCenterButton_Normal = System.Drawing.Color.Empty
        Me.taskbar.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.taskbar.AppBackground = System.Drawing.Color.Empty
        Me.taskbar.AppUnderline = System.Drawing.Color.Empty
        Me.taskbar.BackColor = System.Drawing.Color.Transparent
        Me.taskbar.BackColorAlpha = 130
        Me.taskbar.Background = System.Drawing.Color.Empty
        Me.taskbar.Background2 = System.Drawing.Color.Empty
        Me.taskbar.BlurPower = 12
        Me.taskbar.DarkMode = True
        Me.taskbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.taskbar.LinkColor = System.Drawing.Color.Empty
        Me.taskbar.Location = New System.Drawing.Point(0, 253)
        Me.taskbar.Name = "taskbar"
        Me.taskbar.NoisePower = 0.2!
        Me.taskbar.Shadow = True
        Me.taskbar.Size = New System.Drawing.Size(526, 42)
        Me.taskbar.StartColor = System.Drawing.Color.Empty
        Me.taskbar.Style = WinPaletter.UI.Simulation.WinElement.Styles.Taskbar11
        Me.taskbar.SuspendRefresh = False
        Me.taskbar.TabIndex = 0
        Me.taskbar.Transparency = True
        Me.taskbar.UseWin11ORB_WithWin10 = False
        Me.taskbar.UseWin11RoundedCorners_WithWin10_Level1 = False
        Me.taskbar.UseWin11RoundedCorners_WithWin10_Level2 = False
        Me.taskbar.Win7ColorBal = 100
        Me.taskbar.Win7GlowBal = 100
        '
        'Window2
        '
        Me.Window2.AccentColor_Active = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.Window2.AccentColor_Enabled = True
        Me.Window2.AccentColor_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.Window2.AccentColor2_Active = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.Window2.AccentColor2_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.Window2.Active = False
        Me.Window2.BackColor = System.Drawing.Color.Transparent
        Me.Window2.DarkMode = True
        Me.Window2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Window2.Location = New System.Drawing.Point(172, 160)
        Me.Window2.Metrics_BorderWidth = 1
        Me.Window2.Metrics_CaptionHeight = 22
        Me.Window2.Metrics_PaddedBorderWidth = 4
        Me.Window2.Name = "Window2"
        Me.Window2.Padding = New System.Windows.Forms.Padding(4, 40, 4, 4)
        Me.Window2.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11
        Me.Window2.Radius = 5
        Me.Window2.Shadow = True
        Me.Window2.Size = New System.Drawing.Size(189, 85)
        Me.Window2.SuspendRefresh = False
        Me.Window2.TabIndex = 3
        Me.Window2.Text = "Inactive app"
        Me.Window2.ToolWindow = False
        Me.Window2.Win7Alpha = 100
        Me.Window2.Win7ColorBal = 100
        Me.Window2.Win7GlowBal = 100
        Me.Window2.Win7Noise = 1.0!
        Me.Window2.WinVista = False
        '
        'Window1
        '
        Me.Window1.AccentColor_Active = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.Window1.AccentColor_Enabled = True
        Me.Window1.AccentColor_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.Window1.AccentColor2_Active = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.Window1.AccentColor2_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.Window1.Active = True
        Me.Window1.BackColor = System.Drawing.Color.Transparent
        Me.Window1.Controls.Add(Me.Panel3)
        Me.Window1.Controls.Add(Me.lnk_preview)
        Me.Window1.DarkMode = True
        Me.Window1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Window1.Location = New System.Drawing.Point(172, 13)
        Me.Window1.Metrics_BorderWidth = 1
        Me.Window1.Metrics_CaptionHeight = 22
        Me.Window1.Metrics_PaddedBorderWidth = 4
        Me.Window1.Name = "Window1"
        Me.Window1.Padding = New System.Windows.Forms.Padding(4, 40, 4, 4)
        Me.Window1.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11
        Me.Window1.Radius = 5
        Me.Window1.Shadow = True
        Me.Window1.Size = New System.Drawing.Size(189, 147)
        Me.Window1.SuspendRefresh = False
        Me.Window1.TabIndex = 2
        Me.Window1.Text = "App preview"
        Me.Window1.ToolWindow = False
        Me.Window1.Win7Alpha = 100
        Me.Window1.Win7ColorBal = 100
        Me.Window1.Win7GlowBal = 100
        Me.Window1.Win7Noise = 1.0!
        Me.Window1.WinVista = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.setting_icon_preview)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(4, 40)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel3.Size = New System.Drawing.Size(181, 78)
        Me.Panel3.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.DrawOnGlass = False
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(1, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(179, 31)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "This is a setting icon"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'setting_icon_preview
        '
        Me.setting_icon_preview.BackColor = System.Drawing.Color.Transparent
        Me.setting_icon_preview.Dock = System.Windows.Forms.DockStyle.Top
        Me.setting_icon_preview.DrawOnGlass = False
        Me.setting_icon_preview.Font = New System.Drawing.Font("Segoe MDL2 Assets", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.setting_icon_preview.Location = New System.Drawing.Point(1, 1)
        Me.setting_icon_preview.Name = "setting_icon_preview"
        Me.setting_icon_preview.Size = New System.Drawing.Size(179, 45)
        Me.setting_icon_preview.TabIndex = 14
        Me.setting_icon_preview.Text = ""
        Me.setting_icon_preview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lnk_preview
        '
        Me.lnk_preview.BackColor = System.Drawing.Color.Transparent
        Me.lnk_preview.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lnk_preview.DrawOnGlass = False
        Me.lnk_preview.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lnk_preview.ForeColor = System.Drawing.Color.Brown
        Me.lnk_preview.Location = New System.Drawing.Point(4, 118)
        Me.lnk_preview.Name = "lnk_preview"
        Me.lnk_preview.Size = New System.Drawing.Size(181, 25)
        Me.lnk_preview.TabIndex = 16
        Me.lnk_preview.Text = "Settings link preview"
        Me.lnk_preview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage6.Controls.Add(Me.pnl_preview_classic)
        Me.TabPage6.Location = New System.Drawing.Point(4, 24)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(520, 269)
        Me.TabPage6.TabIndex = 1
        Me.TabPage6.Text = "1"
        '
        'pnl_preview_classic
        '
        Me.pnl_preview_classic.BackColor = System.Drawing.Color.Black
        Me.pnl_preview_classic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_preview_classic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_preview_classic.Controls.Add(Me.ClassicWindow1)
        Me.pnl_preview_classic.Controls.Add(Me.ClassicWindow2)
        Me.pnl_preview_classic.Controls.Add(Me.ClassicTaskbar)
        Me.pnl_preview_classic.Location = New System.Drawing.Point(0, 0)
        Me.pnl_preview_classic.Name = "pnl_preview_classic"
        Me.pnl_preview_classic.Size = New System.Drawing.Size(528, 297)
        Me.pnl_preview_classic.TabIndex = 34
        '
        'ClassicWindow1
        '
        Me.ClassicWindow1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow1.ButtonDkShadow = System.Drawing.Color.Black
        Me.ClassicWindow1.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow1.ButtonHilight = System.Drawing.Color.White
        Me.ClassicWindow1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClassicWindow1.ButtonText = System.Drawing.Color.Black
        Me.ClassicWindow1.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClassicWindow1.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ClassicWindow1.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow1.ColorGradient = True
        Me.ClassicWindow1.ControlBox = True
        Me.ClassicWindow1.Flat = False
        Me.ClassicWindow1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.ClassicWindow1.ForeColor = System.Drawing.Color.White
        Me.ClassicWindow1.Location = New System.Drawing.Point(176, 20)
        Me.ClassicWindow1.MaximizeBox = False
        Me.ClassicWindow1.Metrics_BorderWidth = 0
        Me.ClassicWindow1.Metrics_CaptionHeight = 18
        Me.ClassicWindow1.Metrics_CaptionWidth = 18
        Me.ClassicWindow1.Metrics_PaddedBorderWidth = 0
        Me.ClassicWindow1.MinimizeBox = False
        Me.ClassicWindow1.Name = "ClassicWindow1"
        Me.ClassicWindow1.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.ClassicWindow1.Size = New System.Drawing.Size(181, 146)
        Me.ClassicWindow1.TabIndex = 4
        Me.ClassicWindow1.Text = "App preview"
        Me.ClassicWindow1.UseItAsMenu = False
        '
        'ClassicWindow2
        '
        Me.ClassicWindow2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow2.ButtonDkShadow = System.Drawing.Color.Black
        Me.ClassicWindow2.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow2.ButtonHilight = System.Drawing.Color.White
        Me.ClassicWindow2.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow2.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClassicWindow2.ButtonText = System.Drawing.Color.Black
        Me.ClassicWindow2.Color1 = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ClassicWindow2.Color2 = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.ClassicWindow2.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicWindow2.ColorGradient = True
        Me.ClassicWindow2.ControlBox = True
        Me.ClassicWindow2.Flat = False
        Me.ClassicWindow2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.ClassicWindow2.ForeColor = System.Drawing.Color.White
        Me.ClassicWindow2.Location = New System.Drawing.Point(176, 172)
        Me.ClassicWindow2.MaximizeBox = False
        Me.ClassicWindow2.Metrics_BorderWidth = 0
        Me.ClassicWindow2.Metrics_CaptionHeight = 18
        Me.ClassicWindow2.Metrics_CaptionWidth = 18
        Me.ClassicWindow2.Metrics_PaddedBorderWidth = 0
        Me.ClassicWindow2.MinimizeBox = False
        Me.ClassicWindow2.Name = "ClassicWindow2"
        Me.ClassicWindow2.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.ClassicWindow2.Size = New System.Drawing.Size(181, 60)
        Me.ClassicWindow2.TabIndex = 5
        Me.ClassicWindow2.Text = "Inactive app"
        Me.ClassicWindow2.UseItAsMenu = False
        '
        'ClassicTaskbar
        '
        Me.ClassicTaskbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicTaskbar.ButtonDkShadow = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.ClassicTaskbar.ButtonHilight = System.Drawing.Color.White
        Me.ClassicTaskbar.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.ClassicTaskbar.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClassicTaskbar.Controls.Add(Me.ButtonR4)
        Me.ClassicTaskbar.Controls.Add(Me.ButtonR3)
        Me.ClassicTaskbar.Controls.Add(Me.ButtonR2)
        Me.ClassicTaskbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ClassicTaskbar.Flat = False
        Me.ClassicTaskbar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.ClassicTaskbar.ForeColor = System.Drawing.Color.Black
        Me.ClassicTaskbar.Location = New System.Drawing.Point(0, 251)
        Me.ClassicTaskbar.Name = "ClassicTaskbar"
        Me.ClassicTaskbar.Size = New System.Drawing.Size(526, 44)
        Me.ClassicTaskbar.Style2 = False
        Me.ClassicTaskbar.TabIndex = 0
        Me.ClassicTaskbar.UseItAsWin7Taskbar = True
        '
        'ButtonR4
        '
        Me.ButtonR4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonR4.AppearsAsPressed = False
        Me.ButtonR4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR4.ButtonDkShadow = System.Drawing.Color.Black
        Me.ButtonR4.ButtonHilight = System.Drawing.Color.White
        Me.ButtonR4.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR4.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonR4.FocusRectHeight = 1
        Me.ButtonR4.FocusRectWidth = 1
        Me.ButtonR4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonR4.ForeColor = System.Drawing.Color.Black
        Me.ButtonR4.HatchBrush = False
        Me.ButtonR4.Image = Nothing
        Me.ButtonR4.Location = New System.Drawing.Point(113, 4)
        Me.ButtonR4.Name = "ButtonR4"
        Me.ButtonR4.Size = New System.Drawing.Size(48, 38)
        Me.ButtonR4.TabIndex = 2
        Me.ButtonR4.UseItAsScrollbar = False
        Me.ButtonR4.UseVisualStyleBackColor = False
        Me.ButtonR4.WindowFrame = System.Drawing.Color.Black
        '
        'ButtonR3
        '
        Me.ButtonR3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonR3.AppearsAsPressed = True
        Me.ButtonR3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR3.ButtonDkShadow = System.Drawing.Color.Black
        Me.ButtonR3.ButtonHilight = System.Drawing.Color.White
        Me.ButtonR3.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR3.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonR3.FocusRectHeight = 1
        Me.ButtonR3.FocusRectWidth = 1
        Me.ButtonR3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonR3.ForeColor = System.Drawing.Color.Black
        Me.ButtonR3.HatchBrush = False
        Me.ButtonR3.Image = Nothing
        Me.ButtonR3.Location = New System.Drawing.Point(63, 4)
        Me.ButtonR3.Name = "ButtonR3"
        Me.ButtonR3.Size = New System.Drawing.Size(48, 38)
        Me.ButtonR3.TabIndex = 1
        Me.ButtonR3.UseItAsScrollbar = False
        Me.ButtonR3.UseVisualStyleBackColor = False
        Me.ButtonR3.WindowFrame = System.Drawing.Color.Black
        '
        'ButtonR2
        '
        Me.ButtonR2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonR2.AppearsAsPressed = False
        Me.ButtonR2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR2.ButtonDkShadow = System.Drawing.Color.Black
        Me.ButtonR2.ButtonHilight = System.Drawing.Color.White
        Me.ButtonR2.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR2.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonR2.FocusRectHeight = 1
        Me.ButtonR2.FocusRectWidth = 1
        Me.ButtonR2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonR2.ForeColor = System.Drawing.Color.Black
        Me.ButtonR2.HatchBrush = False
        Me.ButtonR2.Image = Nothing
        Me.ButtonR2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonR2.Location = New System.Drawing.Point(2, 4)
        Me.ButtonR2.Name = "ButtonR2"
        Me.ButtonR2.Size = New System.Drawing.Size(52, 38)
        Me.ButtonR2.TabIndex = 0
        Me.ButtonR2.Text = "Start"
        Me.ButtonR2.UseItAsScrollbar = False
        Me.ButtonR2.UseVisualStyleBackColor = False
        Me.ButtonR2.WindowFrame = System.Drawing.Color.Black
        '
        'ClassicColorsPreview
        '
        Me.ClassicColorsPreview.BackColor = System.Drawing.Color.Teal
        Me.ClassicColorsPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClassicColorsPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ClassicColorsPreview.Controls.Add(Me.RetroShadow1)
        Me.ClassicColorsPreview.Controls.Add(Me.Menu_Window)
        Me.ClassicColorsPreview.Controls.Add(Me.WindowR3)
        Me.ClassicColorsPreview.Controls.Add(Me.LabelR13)
        Me.ClassicColorsPreview.Controls.Add(Me.WindowR2)
        Me.ClassicColorsPreview.Controls.Add(Me.WindowR1)
        Me.ClassicColorsPreview.Controls.Add(Me.WindowR4)
        Me.ClassicColorsPreview.Location = New System.Drawing.Point(537, 3)
        Me.ClassicColorsPreview.Name = "ClassicColorsPreview"
        Me.ClassicColorsPreview.Size = New System.Drawing.Size(528, 297)
        Me.ClassicColorsPreview.TabIndex = 43
        '
        'RetroShadow1
        '
        Me.RetroShadow1.BackColor = System.Drawing.Color.Transparent
        Me.RetroShadow1.Location = New System.Drawing.Point(112, 225)
        Me.RetroShadow1.Name = "RetroShadow1"
        Me.RetroShadow1.Size = New System.Drawing.Size(115, 66)
        Me.RetroShadow1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.RetroShadow1.TabIndex = 7
        Me.RetroShadow1.TabStop = False
        '
        'Menu_Window
        '
        Me.Menu_Window.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Menu_Window.ButtonDkShadow = System.Drawing.Color.Black
        Me.Menu_Window.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Menu_Window.ButtonHilight = System.Drawing.Color.White
        Me.Menu_Window.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Menu_Window.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Menu_Window.ButtonText = System.Drawing.Color.Black
        Me.Menu_Window.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Menu_Window.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.Menu_Window.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Menu_Window.ColorGradient = True
        Me.Menu_Window.ControlBox = True
        Me.Menu_Window.Controls.Add(Me.menucontainer3)
        Me.Menu_Window.Controls.Add(Me.highlight)
        Me.Menu_Window.Controls.Add(Me.menucontainer1)
        Me.Menu_Window.Flat = False
        Me.Menu_Window.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Menu_Window.ForeColor = System.Drawing.Color.Black
        Me.Menu_Window.Location = New System.Drawing.Point(300, 151)
        Me.Menu_Window.MaximizeBox = True
        Me.Menu_Window.Metrics_BorderWidth = 1
        Me.Menu_Window.Metrics_CaptionHeight = 22
        Me.Menu_Window.Metrics_CaptionWidth = 0
        Me.Menu_Window.Metrics_PaddedBorderWidth = 4
        Me.Menu_Window.MinimizeBox = True
        Me.Menu_Window.Name = "Menu_Window"
        Me.Menu_Window.Padding = New System.Windows.Forms.Padding(3, 3, 5, 5)
        Me.Menu_Window.Size = New System.Drawing.Size(115, 66)
        Me.Menu_Window.TabIndex = 4
        Me.Menu_Window.Text = "New Window"
        Me.Menu_Window.UseItAsMenu = True
        '
        'menucontainer3
        '
        Me.menucontainer3.BackColor = System.Drawing.Color.Transparent
        Me.menucontainer3.Controls.Add(Me.LabelR9)
        Me.menucontainer3.Dock = System.Windows.Forms.DockStyle.Top
        Me.menucontainer3.Location = New System.Drawing.Point(3, 43)
        Me.menucontainer3.Name = "menucontainer3"
        Me.menucontainer3.Padding = New System.Windows.Forms.Padding(21, 0, 0, 0)
        Me.menucontainer3.Size = New System.Drawing.Size(107, 20)
        Me.menucontainer3.TabIndex = 12
        '
        'LabelR9
        '
        Me.LabelR9.BackColor = System.Drawing.Color.Transparent
        Me.LabelR9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelR9.DrawOnGlass = False
        Me.LabelR9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR9.ForeColor = System.Drawing.Color.DimGray
        Me.LabelR9.Location = New System.Drawing.Point(21, 0)
        Me.LabelR9.Name = "LabelR9"
        Me.LabelR9.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.LabelR9.Size = New System.Drawing.Size(86, 20)
        Me.LabelR9.TabIndex = 3
        Me.LabelR9.Text = "Disabled item"
        Me.LabelR9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'highlight
        '
        Me.highlight.BackColor = System.Drawing.Color.Navy
        Me.highlight.Controls.Add(Me.menuhilight)
        Me.highlight.Dock = System.Windows.Forms.DockStyle.Top
        Me.highlight.Location = New System.Drawing.Point(3, 23)
        Me.highlight.Name = "highlight"
        Me.highlight.Size = New System.Drawing.Size(107, 20)
        Me.highlight.TabIndex = 10
        '
        'menuhilight
        '
        Me.menuhilight.BackColor = System.Drawing.Color.Navy
        Me.menuhilight.Controls.Add(Me.LabelR5)
        Me.menuhilight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.menuhilight.Location = New System.Drawing.Point(0, 0)
        Me.menuhilight.Name = "menuhilight"
        Me.menuhilight.Padding = New System.Windows.Forms.Padding(21, 0, 1, 0)
        Me.menuhilight.Size = New System.Drawing.Size(107, 20)
        Me.menuhilight.TabIndex = 11
        '
        'LabelR5
        '
        Me.LabelR5.BackColor = System.Drawing.Color.Transparent
        Me.LabelR5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelR5.DrawOnGlass = False
        Me.LabelR5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR5.ForeColor = System.Drawing.Color.White
        Me.LabelR5.Location = New System.Drawing.Point(21, 0)
        Me.LabelR5.Name = "LabelR5"
        Me.LabelR5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.LabelR5.Size = New System.Drawing.Size(85, 20)
        Me.LabelR5.TabIndex = 3
        Me.LabelR5.Text = "Hovered item"
        Me.LabelR5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'menucontainer1
        '
        Me.menucontainer1.BackColor = System.Drawing.Color.Transparent
        Me.menucontainer1.Controls.Add(Me.LabelR6)
        Me.menucontainer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.menucontainer1.Location = New System.Drawing.Point(3, 3)
        Me.menucontainer1.Name = "menucontainer1"
        Me.menucontainer1.Padding = New System.Windows.Forms.Padding(21, 0, 0, 0)
        Me.menucontainer1.Size = New System.Drawing.Size(107, 20)
        Me.menucontainer1.TabIndex = 6
        '
        'LabelR6
        '
        Me.LabelR6.BackColor = System.Drawing.Color.Transparent
        Me.LabelR6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelR6.DrawOnGlass = False
        Me.LabelR6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR6.ForeColor = System.Drawing.Color.Black
        Me.LabelR6.Location = New System.Drawing.Point(21, 0)
        Me.LabelR6.Name = "LabelR6"
        Me.LabelR6.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.LabelR6.Size = New System.Drawing.Size(86, 20)
        Me.LabelR6.TabIndex = 3
        Me.LabelR6.Text = "Menu item"
        Me.LabelR6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WindowR3
        '
        Me.WindowR3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR3.ButtonDkShadow = System.Drawing.Color.Black
        Me.WindowR3.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR3.ButtonHilight = System.Drawing.Color.White
        Me.WindowR3.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR3.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.WindowR3.ButtonText = System.Drawing.Color.Black
        Me.WindowR3.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.WindowR3.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.WindowR3.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR3.ColorGradient = True
        Me.WindowR3.ControlBox = False
        Me.WindowR3.Controls.Add(Me.ButtonR5)
        Me.WindowR3.Controls.Add(Me.LabelR4)
        Me.WindowR3.Flat = False
        Me.WindowR3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.WindowR3.ForeColor = System.Drawing.Color.White
        Me.WindowR3.Location = New System.Drawing.Point(215, 185)
        Me.WindowR3.MaximizeBox = True
        Me.WindowR3.Metrics_BorderWidth = 0
        Me.WindowR3.Metrics_CaptionHeight = 18
        Me.WindowR3.Metrics_CaptionWidth = 18
        Me.WindowR3.Metrics_PaddedBorderWidth = 0
        Me.WindowR3.MinimizeBox = True
        Me.WindowR3.Name = "WindowR3"
        Me.WindowR3.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.WindowR3.Size = New System.Drawing.Size(147, 80)
        Me.WindowR3.TabIndex = 2
        Me.WindowR3.Text = "Message box"
        Me.WindowR3.UseItAsMenu = False
        '
        'ButtonR5
        '
        Me.ButtonR5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonR5.AppearsAsPressed = False
        Me.ButtonR5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR5.ButtonDkShadow = System.Drawing.Color.Black
        Me.ButtonR5.ButtonHilight = System.Drawing.Color.White
        Me.ButtonR5.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR5.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonR5.FocusRectHeight = 1
        Me.ButtonR5.FocusRectWidth = 1
        Me.ButtonR5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonR5.ForeColor = System.Drawing.Color.Black
        Me.ButtonR5.HatchBrush = False
        Me.ButtonR5.Image = Nothing
        Me.ButtonR5.Location = New System.Drawing.Point(37, 49)
        Me.ButtonR5.Name = "ButtonR5"
        Me.ButtonR5.Size = New System.Drawing.Size(75, 23)
        Me.ButtonR5.TabIndex = 2
        Me.ButtonR5.Text = "OK"
        Me.ButtonR5.UseItAsScrollbar = False
        Me.ButtonR5.UseVisualStyleBackColor = False
        Me.ButtonR5.WindowFrame = System.Drawing.Color.Black
        '
        'LabelR4
        '
        Me.LabelR4.AutoSize = True
        Me.LabelR4.BackColor = System.Drawing.Color.Transparent
        Me.LabelR4.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelR4.DrawOnGlass = False
        Me.LabelR4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR4.ForeColor = System.Drawing.Color.Black
        Me.LabelR4.Location = New System.Drawing.Point(4, 22)
        Me.LabelR4.Name = "LabelR4"
        Me.LabelR4.Padding = New System.Windows.Forms.Padding(4)
        Me.LabelR4.Size = New System.Drawing.Size(78, 21)
        Me.LabelR4.TabIndex = 1
        Me.LabelR4.Text = "Message text"
        Me.LabelR4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelR13
        '
        Me.LabelR13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelR13.AutoSize = True
        Me.LabelR13.BackColor = System.Drawing.Color.White
        Me.LabelR13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelR13.DrawOnGlass = False
        Me.LabelR13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR13.ForeColor = System.Drawing.Color.Black
        Me.LabelR13.Location = New System.Drawing.Point(287, 47)
        Me.LabelR13.Name = "LabelR13"
        Me.LabelR13.Size = New System.Drawing.Size(79, 15)
        Me.LabelR13.TabIndex = 5
        Me.LabelR13.Text = "This is a tooltip"
        '
        'WindowR2
        '
        Me.WindowR2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR2.ButtonDkShadow = System.Drawing.Color.Black
        Me.WindowR2.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR2.ButtonHilight = System.Drawing.Color.White
        Me.WindowR2.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR2.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.WindowR2.ButtonText = System.Drawing.Color.Black
        Me.WindowR2.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.WindowR2.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.WindowR2.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR2.ColorGradient = True
        Me.WindowR2.ControlBox = True
        Me.WindowR2.Controls.Add(Me.TextBoxR1)
        Me.WindowR2.Controls.Add(Me.menucontainer0)
        Me.WindowR2.Flat = False
        Me.WindowR2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.WindowR2.ForeColor = System.Drawing.Color.White
        Me.WindowR2.Location = New System.Drawing.Point(195, 110)
        Me.WindowR2.MaximizeBox = True
        Me.WindowR2.Metrics_BorderWidth = 0
        Me.WindowR2.Metrics_CaptionHeight = 18
        Me.WindowR2.Metrics_CaptionWidth = 18
        Me.WindowR2.Metrics_PaddedBorderWidth = 0
        Me.WindowR2.MinimizeBox = True
        Me.WindowR2.Name = "WindowR2"
        Me.WindowR2.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.WindowR2.Size = New System.Drawing.Size(196, 120)
        Me.WindowR2.TabIndex = 1
        Me.WindowR2.Text = "Active window"
        Me.WindowR2.UseItAsMenu = False
        '
        'TextBoxR1
        '
        Me.TextBoxR1.BackColor = System.Drawing.Color.White
        Me.TextBoxR1.ButtonDkShadow = System.Drawing.Color.Black
        Me.TextBoxR1.ButtonHilight = System.Drawing.Color.White
        Me.TextBoxR1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxR1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TextBoxR1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxR1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.TextBoxR1.ForeColor = System.Drawing.Color.Black
        Me.TextBoxR1.Location = New System.Drawing.Point(4, 40)
        Me.TextBoxR1.MaxLength = 32767
        Me.TextBoxR1.Multiline = True
        Me.TextBoxR1.Name = "TextBoxR1"
        Me.TextBoxR1.ReadOnly = True
        Me.TextBoxR1.Size = New System.Drawing.Size(188, 76)
        Me.TextBoxR1.TabIndex = 3
        Me.TextBoxR1.Text = "Window text"
        Me.TextBoxR1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBoxR1.UseSystemPasswordChar = False
        '
        'menucontainer0
        '
        Me.menucontainer0.BackColor = System.Drawing.Color.Silver
        Me.menucontainer0.Controls.Add(Me.PanelR1)
        Me.menucontainer0.Controls.Add(Me.LabelR2)
        Me.menucontainer0.Controls.Add(Me.LabelR1)
        Me.menucontainer0.Dock = System.Windows.Forms.DockStyle.Top
        Me.menucontainer0.Location = New System.Drawing.Point(4, 22)
        Me.menucontainer0.Name = "menucontainer0"
        Me.menucontainer0.Size = New System.Drawing.Size(188, 18)
        Me.menucontainer0.TabIndex = 5
        '
        'PanelR1
        '
        Me.PanelR1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PanelR1.ButtonDkShadow = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.PanelR1.ButtonHilight = System.Drawing.Color.White
        Me.PanelR1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.PanelR1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.PanelR1.Controls.Add(Me.LabelR3)
        Me.PanelR1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelR1.Flat = False
        Me.PanelR1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.PanelR1.ForeColor = System.Drawing.Color.Black
        Me.PanelR1.Location = New System.Drawing.Point(88, 0)
        Me.PanelR1.Name = "PanelR1"
        Me.PanelR1.Padding = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.PanelR1.Size = New System.Drawing.Size(53, 18)
        Me.PanelR1.Style2 = False
        Me.PanelR1.TabIndex = 2
        '
        'LabelR3
        '
        Me.LabelR3.BackColor = System.Drawing.Color.Transparent
        Me.LabelR3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelR3.DrawOnGlass = False
        Me.LabelR3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR3.ForeColor = System.Drawing.Color.Black
        Me.LabelR3.Location = New System.Drawing.Point(1, 3)
        Me.LabelR3.Name = "LabelR3"
        Me.LabelR3.Size = New System.Drawing.Size(51, 12)
        Me.LabelR3.TabIndex = 1
        Me.LabelR3.Text = "Selected"
        Me.LabelR3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelR2
        '
        Me.LabelR2.BackColor = System.Drawing.Color.Transparent
        Me.LabelR2.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelR2.DrawOnGlass = False
        Me.LabelR2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabelR2.Location = New System.Drawing.Point(40, 0)
        Me.LabelR2.Name = "LabelR2"
        Me.LabelR2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LabelR2.Size = New System.Drawing.Size(48, 18)
        Me.LabelR2.TabIndex = 1
        Me.LabelR2.Text = "Disabled"
        Me.LabelR2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelR1
        '
        Me.LabelR1.BackColor = System.Drawing.Color.Transparent
        Me.LabelR1.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelR1.DrawOnGlass = False
        Me.LabelR1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.LabelR1.ForeColor = System.Drawing.Color.Black
        Me.LabelR1.Location = New System.Drawing.Point(0, 0)
        Me.LabelR1.Name = "LabelR1"
        Me.LabelR1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LabelR1.Size = New System.Drawing.Size(40, 18)
        Me.LabelR1.TabIndex = 0
        Me.LabelR1.Text = "Normal"
        Me.LabelR1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WindowR1
        '
        Me.WindowR1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR1.ButtonDkShadow = System.Drawing.Color.Black
        Me.WindowR1.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR1.ButtonHilight = System.Drawing.Color.White
        Me.WindowR1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.WindowR1.ButtonText = System.Drawing.Color.Black
        Me.WindowR1.Color1 = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.WindowR1.Color2 = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.WindowR1.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR1.ColorGradient = True
        Me.WindowR1.ControlBox = True
        Me.WindowR1.Flat = False
        Me.WindowR1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.WindowR1.ForeColor = System.Drawing.Color.White
        Me.WindowR1.Location = New System.Drawing.Point(179, 77)
        Me.WindowR1.MaximizeBox = True
        Me.WindowR1.Metrics_BorderWidth = 0
        Me.WindowR1.Metrics_CaptionHeight = 18
        Me.WindowR1.Metrics_CaptionWidth = 18
        Me.WindowR1.Metrics_PaddedBorderWidth = 0
        Me.WindowR1.MinimizeBox = True
        Me.WindowR1.Name = "WindowR1"
        Me.WindowR1.Padding = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.WindowR1.Size = New System.Drawing.Size(180, 112)
        Me.WindowR1.TabIndex = 0
        Me.WindowR1.Text = "Inactive window"
        Me.WindowR1.UseItAsMenu = False
        '
        'WindowR4
        '
        Me.WindowR4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR4.ButtonDkShadow = System.Drawing.Color.Black
        Me.WindowR4.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR4.ButtonHilight = System.Drawing.Color.White
        Me.WindowR4.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR4.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.WindowR4.ButtonText = System.Drawing.Color.Black
        Me.WindowR4.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.WindowR4.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.WindowR4.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.WindowR4.ColorGradient = True
        Me.WindowR4.ControlBox = True
        Me.WindowR4.Controls.Add(Me.programcontainer)
        Me.WindowR4.Flat = False
        Me.WindowR4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.WindowR4.ForeColor = System.Drawing.Color.White
        Me.WindowR4.Location = New System.Drawing.Point(139, 30)
        Me.WindowR4.MaximizeBox = False
        Me.WindowR4.Metrics_BorderWidth = 0
        Me.WindowR4.Metrics_CaptionHeight = 18
        Me.WindowR4.Metrics_CaptionWidth = 18
        Me.WindowR4.Metrics_PaddedBorderWidth = 0
        Me.WindowR4.MinimizeBox = False
        Me.WindowR4.Name = "WindowR4"
        Me.WindowR4.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.WindowR4.Size = New System.Drawing.Size(156, 132)
        Me.WindowR4.TabIndex = 3
        Me.WindowR4.Text = "Program container"
        Me.WindowR4.UseItAsMenu = False
        '
        'programcontainer
        '
        Me.programcontainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.programcontainer.Controls.Add(Me.PanelR2)
        Me.programcontainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.programcontainer.Location = New System.Drawing.Point(4, 22)
        Me.programcontainer.Name = "programcontainer"
        Me.programcontainer.Padding = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.programcontainer.Size = New System.Drawing.Size(148, 106)
        Me.programcontainer.TabIndex = 4
        '
        'PanelR2
        '
        Me.PanelR2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PanelR2.ButtonHilight = System.Drawing.Color.White
        Me.PanelR2.Controls.Add(Me.ButtonR12)
        Me.PanelR2.Controls.Add(Me.ButtonR11)
        Me.PanelR2.Controls.Add(Me.ButtonR10)
        Me.PanelR2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelR2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.PanelR2.ForeColor = System.Drawing.Color.Black
        Me.PanelR2.Location = New System.Drawing.Point(0, 0)
        Me.PanelR2.Name = "PanelR2"
        Me.PanelR2.Size = New System.Drawing.Size(16, 106)
        Me.PanelR2.TabIndex = 0
        '
        'ButtonR12
        '
        Me.ButtonR12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonR12.AppearsAsPressed = False
        Me.ButtonR12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR12.ButtonDkShadow = System.Drawing.Color.Black
        Me.ButtonR12.ButtonHilight = System.Drawing.Color.White
        Me.ButtonR12.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR12.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonR12.FocusRectHeight = 1
        Me.ButtonR12.FocusRectWidth = 1
        Me.ButtonR12.Font = New System.Drawing.Font("Marlett", 6.0!)
        Me.ButtonR12.ForeColor = System.Drawing.Color.Black
        Me.ButtonR12.HatchBrush = False
        Me.ButtonR12.Image = Nothing
        Me.ButtonR12.Location = New System.Drawing.Point(0, 29)
        Me.ButtonR12.Name = "ButtonR12"
        Me.ButtonR12.Size = New System.Drawing.Size(16, 31)
        Me.ButtonR12.TabIndex = 7
        Me.ButtonR12.UseItAsScrollbar = True
        Me.ButtonR12.UseVisualStyleBackColor = False
        Me.ButtonR12.WindowFrame = System.Drawing.Color.Black
        '
        'ButtonR11
        '
        Me.ButtonR11.AppearsAsPressed = False
        Me.ButtonR11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR11.ButtonDkShadow = System.Drawing.Color.Black
        Me.ButtonR11.ButtonHilight = System.Drawing.Color.White
        Me.ButtonR11.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR11.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonR11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonR11.FocusRectHeight = 1
        Me.ButtonR11.FocusRectWidth = 1
        Me.ButtonR11.Font = New System.Drawing.Font("Marlett", 8.7!, System.Drawing.FontStyle.Bold)
        Me.ButtonR11.ForeColor = System.Drawing.Color.Black
        Me.ButtonR11.HatchBrush = False
        Me.ButtonR11.Image = Nothing
        Me.ButtonR11.Location = New System.Drawing.Point(0, 92)
        Me.ButtonR11.Name = "ButtonR11"
        Me.ButtonR11.Size = New System.Drawing.Size(16, 14)
        Me.ButtonR11.TabIndex = 6
        Me.ButtonR11.Text = "u"
        Me.ButtonR11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonR11.UseItAsScrollbar = False
        Me.ButtonR11.UseVisualStyleBackColor = False
        Me.ButtonR11.WindowFrame = System.Drawing.Color.Black
        '
        'ButtonR10
        '
        Me.ButtonR10.AppearsAsPressed = False
        Me.ButtonR10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR10.ButtonDkShadow = System.Drawing.Color.Black
        Me.ButtonR10.ButtonHilight = System.Drawing.Color.White
        Me.ButtonR10.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonR10.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonR10.Dock = System.Windows.Forms.DockStyle.Top
        Me.ButtonR10.FocusRectHeight = 1
        Me.ButtonR10.FocusRectWidth = 1
        Me.ButtonR10.Font = New System.Drawing.Font("Marlett", 8.7!, System.Drawing.FontStyle.Bold)
        Me.ButtonR10.ForeColor = System.Drawing.Color.Black
        Me.ButtonR10.HatchBrush = False
        Me.ButtonR10.Image = Nothing
        Me.ButtonR10.Location = New System.Drawing.Point(0, 0)
        Me.ButtonR10.Name = "ButtonR10"
        Me.ButtonR10.Size = New System.Drawing.Size(16, 14)
        Me.ButtonR10.TabIndex = 5
        Me.ButtonR10.Text = "t"
        Me.ButtonR10.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ButtonR10.UseItAsScrollbar = False
        Me.ButtonR10.UseVisualStyleBackColor = False
        Me.ButtonR10.WindowFrame = System.Drawing.Color.Black
        '
        'CMD1
        '
        Me.CMD1.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.CMD1.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.CMD1.CMD_PopupBackground = 5
        Me.CMD1.CMD_PopupForeground = 15
        Me.CMD1.CMD_ScreenColorsBackground = 0
        Me.CMD1.CMD_ScreenColorsForeground = 7
        Me.CMD1.CustomTerminal = False
        Me.CMD1.Location = New System.Drawing.Point(1071, 3)
        Me.CMD1.Name = "CMD1"
        Me.CMD1.PowerShell = False
        Me.CMD1.Raster = True
        Me.CMD1.RasterSize = WinPaletter.UI.Simulation.WinCMD.Raster_Sizes._8x12
        Me.CMD1.Size = New System.Drawing.Size(528, 297)
        Me.CMD1.TabIndex = 1
        '
        'CMD2
        '
        Me.CMD2.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.CMD2.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.CMD2.CMD_PopupBackground = 5
        Me.CMD2.CMD_PopupForeground = 15
        Me.CMD2.CMD_ScreenColorsBackground = 0
        Me.CMD2.CMD_ScreenColorsForeground = 7
        Me.CMD2.CustomTerminal = False
        Me.CMD2.Location = New System.Drawing.Point(1605, 3)
        Me.CMD2.Name = "CMD2"
        Me.CMD2.PowerShell = False
        Me.CMD2.Raster = True
        Me.CMD2.RasterSize = WinPaletter.UI.Simulation.WinCMD.Raster_Sizes._8x12
        Me.CMD2.Size = New System.Drawing.Size(528, 297)
        Me.CMD2.TabIndex = 2
        '
        'CMD3
        '
        Me.CMD3.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.CMD3.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.CMD3.CMD_PopupBackground = 5
        Me.CMD3.CMD_PopupForeground = 15
        Me.CMD3.CMD_ScreenColorsBackground = 0
        Me.CMD3.CMD_ScreenColorsForeground = 7
        Me.CMD3.CustomTerminal = False
        Me.CMD3.Location = New System.Drawing.Point(2139, 3)
        Me.CMD3.Name = "CMD3"
        Me.CMD3.PowerShell = False
        Me.CMD3.Raster = True
        Me.CMD3.RasterSize = WinPaletter.UI.Simulation.WinCMD.Raster_Sizes._8x12
        Me.CMD3.Size = New System.Drawing.Size(528, 297)
        Me.CMD3.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cur_anim_btn)
        Me.Panel1.Controls.Add(Me.Cursors_Container)
        Me.Panel1.Controls.Add(Me.cur_tip_btn)
        Me.Panel1.Controls.Add(Me.PictureBox12)
        Me.Panel1.Controls.Add(Me.CursorsSize_Bar)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Location = New System.Drawing.Point(2673, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(528, 297)
        Me.Panel1.TabIndex = 140
        '
        'cur_anim_btn
        '
        Me.cur_anim_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.cur_anim_btn.DrawOnGlass = False
        Me.cur_anim_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cur_anim_btn.ForeColor = System.Drawing.Color.White
        Me.cur_anim_btn.Image = CType(resources.GetObject("cur_anim_btn.Image"), System.Drawing.Image)
        Me.cur_anim_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cur_anim_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cur_anim_btn.Location = New System.Drawing.Point(356, 267)
        Me.cur_anim_btn.Name = "cur_anim_btn"
        Me.cur_anim_btn.Size = New System.Drawing.Size(141, 21)
        Me.cur_anim_btn.TabIndex = 72
        Me.cur_anim_btn.Text = "Animate (3 Cycles)"
        Me.cur_anim_btn.UseVisualStyleBackColor = False
        '
        'Cursors_Container
        '
        Me.Cursors_Container.AutoScroll = True
        Me.Cursors_Container.Controls.Add(Me.Arrow)
        Me.Cursors_Container.Controls.Add(Me.Help)
        Me.Cursors_Container.Controls.Add(Me.AppLoading)
        Me.Cursors_Container.Controls.Add(Me.Busy)
        Me.Cursors_Container.Controls.Add(Me.Move_Cur)
        Me.Cursors_Container.Controls.Add(Me.Up)
        Me.Cursors_Container.Controls.Add(Me.NS)
        Me.Cursors_Container.Controls.Add(Me.EW)
        Me.Cursors_Container.Controls.Add(Me.NESW)
        Me.Cursors_Container.Controls.Add(Me.NWSE)
        Me.Cursors_Container.Controls.Add(Me.Pen)
        Me.Cursors_Container.Controls.Add(Me.None)
        Me.Cursors_Container.Controls.Add(Me.Link)
        Me.Cursors_Container.Controls.Add(Me.Pin)
        Me.Cursors_Container.Controls.Add(Me.Person)
        Me.Cursors_Container.Controls.Add(Me.IBeam)
        Me.Cursors_Container.Controls.Add(Me.Cross)
        Me.Cursors_Container.Dock = System.Windows.Forms.DockStyle.Top
        Me.Cursors_Container.Location = New System.Drawing.Point(3, 3)
        Me.Cursors_Container.Name = "Cursors_Container"
        Me.Cursors_Container.Padding = New System.Windows.Forms.Padding(4, 4, 0, 4)
        Me.Cursors_Container.Size = New System.Drawing.Size(520, 218)
        Me.Cursors_Container.TabIndex = 67
        '
        'Arrow
        '
        Me.Arrow.Location = New System.Drawing.Point(7, 7)
        Me.Arrow.Name = "Arrow"
        Me.Arrow.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Arrow.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Arrow.Prop_Cursor = WinPaletter.Paths.CursorType.Arrow
        Me.Arrow.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Arrow.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Arrow.Prop_LoadingCircleBackGradient = False
        Me.Arrow.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Arrow.Prop_LoadingCircleBackNoise = False
        Me.Arrow.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Arrow.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Arrow.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Arrow.Prop_LoadingCircleHotGradient = False
        Me.Arrow.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Arrow.Prop_LoadingCircleHotNoise = False
        Me.Arrow.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Arrow.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Arrow.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Arrow.Prop_PrimaryColorGradient = False
        Me.Arrow.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Arrow.Prop_PrimaryNoise = False
        Me.Arrow.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Arrow.Prop_Scale = 1.0!
        Me.Arrow.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Arrow.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Arrow.Prop_SecondaryColorGradient = False
        Me.Arrow.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Arrow.Prop_SecondaryNoise = False
        Me.Arrow.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Arrow.Prop_Shadow_Blur = 5
        Me.Arrow.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Arrow.Prop_Shadow_Enabled = False
        Me.Arrow.Prop_Shadow_OffsetX = 2
        Me.Arrow.Prop_Shadow_OffsetY = 2
        Me.Arrow.Prop_Shadow_Opacity = 0.3!
        Me.Arrow.Size = New System.Drawing.Size(64, 64)
        Me.Arrow.TabIndex = 5
        '
        'Help
        '
        Me.Help.Location = New System.Drawing.Point(77, 7)
        Me.Help.Name = "Help"
        Me.Help.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Help.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Help.Prop_Cursor = WinPaletter.Paths.CursorType.Help
        Me.Help.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Help.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Help.Prop_LoadingCircleBackGradient = False
        Me.Help.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Help.Prop_LoadingCircleBackNoise = False
        Me.Help.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Help.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Help.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Help.Prop_LoadingCircleHotGradient = False
        Me.Help.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Help.Prop_LoadingCircleHotNoise = False
        Me.Help.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Help.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Help.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Help.Prop_PrimaryColorGradient = False
        Me.Help.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Help.Prop_PrimaryNoise = False
        Me.Help.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Help.Prop_Scale = 1.0!
        Me.Help.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Help.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Help.Prop_SecondaryColorGradient = False
        Me.Help.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Help.Prop_SecondaryNoise = False
        Me.Help.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Help.Prop_Shadow_Blur = 5
        Me.Help.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Help.Prop_Shadow_Enabled = False
        Me.Help.Prop_Shadow_OffsetX = 2
        Me.Help.Prop_Shadow_OffsetY = 2
        Me.Help.Prop_Shadow_Opacity = 0.3!
        Me.Help.Size = New System.Drawing.Size(64, 64)
        Me.Help.TabIndex = 6
        '
        'AppLoading
        '
        Me.AppLoading.Location = New System.Drawing.Point(147, 7)
        Me.AppLoading.Name = "AppLoading"
        Me.AppLoading.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.AppLoading.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.AppLoading.Prop_Cursor = WinPaletter.Paths.CursorType.AppLoading
        Me.AppLoading.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.AppLoading.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.AppLoading.Prop_LoadingCircleBackGradient = False
        Me.AppLoading.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.AppLoading.Prop_LoadingCircleBackNoise = False
        Me.AppLoading.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.AppLoading.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AppLoading.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AppLoading.Prop_LoadingCircleHotGradient = False
        Me.AppLoading.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.AppLoading.Prop_LoadingCircleHotNoise = False
        Me.AppLoading.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.AppLoading.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.AppLoading.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.AppLoading.Prop_PrimaryColorGradient = False
        Me.AppLoading.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.AppLoading.Prop_PrimaryNoise = False
        Me.AppLoading.Prop_PrimaryNoiseOpacity = 0.25!
        Me.AppLoading.Prop_Scale = 1.0!
        Me.AppLoading.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.AppLoading.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.AppLoading.Prop_SecondaryColorGradient = False
        Me.AppLoading.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.AppLoading.Prop_SecondaryNoise = False
        Me.AppLoading.Prop_SecondaryNoiseOpacity = 0.25!
        Me.AppLoading.Prop_Shadow_Blur = 5
        Me.AppLoading.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.AppLoading.Prop_Shadow_Enabled = False
        Me.AppLoading.Prop_Shadow_OffsetX = 2
        Me.AppLoading.Prop_Shadow_OffsetY = 2
        Me.AppLoading.Prop_Shadow_Opacity = 0.3!
        Me.AppLoading.Size = New System.Drawing.Size(64, 64)
        Me.AppLoading.TabIndex = 6
        '
        'Busy
        '
        Me.Busy.Location = New System.Drawing.Point(217, 7)
        Me.Busy.Name = "Busy"
        Me.Busy.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Busy.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Busy.Prop_Cursor = WinPaletter.Paths.CursorType.Busy
        Me.Busy.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Busy.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Busy.Prop_LoadingCircleBackGradient = False
        Me.Busy.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.Busy.Prop_LoadingCircleBackNoise = False
        Me.Busy.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Busy.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Busy.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Busy.Prop_LoadingCircleHotGradient = False
        Me.Busy.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.Busy.Prop_LoadingCircleHotNoise = False
        Me.Busy.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Busy.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Busy.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Busy.Prop_PrimaryColorGradient = False
        Me.Busy.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.Busy.Prop_PrimaryNoise = False
        Me.Busy.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Busy.Prop_Scale = 1.0!
        Me.Busy.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Busy.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Busy.Prop_SecondaryColorGradient = False
        Me.Busy.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Circle
        Me.Busy.Prop_SecondaryNoise = False
        Me.Busy.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Busy.Prop_Shadow_Blur = 5
        Me.Busy.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Busy.Prop_Shadow_Enabled = False
        Me.Busy.Prop_Shadow_OffsetX = 2
        Me.Busy.Prop_Shadow_OffsetY = 2
        Me.Busy.Prop_Shadow_Opacity = 0.3!
        Me.Busy.Size = New System.Drawing.Size(64, 64)
        Me.Busy.TabIndex = 7
        '
        'Move_Cur
        '
        Me.Move_Cur.Location = New System.Drawing.Point(287, 7)
        Me.Move_Cur.Name = "Move_Cur"
        Me.Move_Cur.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Move_Cur.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Move_Cur.Prop_Cursor = WinPaletter.Paths.CursorType.Move
        Me.Move_Cur.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Move_Cur.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Move_Cur.Prop_LoadingCircleBackGradient = False
        Me.Move_Cur.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Move_Cur.Prop_LoadingCircleBackNoise = False
        Me.Move_Cur.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Move_Cur.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Move_Cur.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Move_Cur.Prop_LoadingCircleHotGradient = False
        Me.Move_Cur.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Move_Cur.Prop_LoadingCircleHotNoise = False
        Me.Move_Cur.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Move_Cur.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Move_Cur.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Move_Cur.Prop_PrimaryColorGradient = False
        Me.Move_Cur.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Move_Cur.Prop_PrimaryNoise = False
        Me.Move_Cur.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Move_Cur.Prop_Scale = 1.0!
        Me.Move_Cur.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Move_Cur.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Move_Cur.Prop_SecondaryColorGradient = False
        Me.Move_Cur.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Move_Cur.Prop_SecondaryNoise = False
        Me.Move_Cur.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Move_Cur.Prop_Shadow_Blur = 5
        Me.Move_Cur.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Move_Cur.Prop_Shadow_Enabled = False
        Me.Move_Cur.Prop_Shadow_OffsetX = 2
        Me.Move_Cur.Prop_Shadow_OffsetY = 2
        Me.Move_Cur.Prop_Shadow_Opacity = 0.3!
        Me.Move_Cur.Size = New System.Drawing.Size(64, 64)
        Me.Move_Cur.TabIndex = 8
        '
        'Up
        '
        Me.Up.Location = New System.Drawing.Point(357, 7)
        Me.Up.Name = "Up"
        Me.Up.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Up.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Up.Prop_Cursor = WinPaletter.Paths.CursorType.Up
        Me.Up.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Up.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Up.Prop_LoadingCircleBackGradient = False
        Me.Up.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Up.Prop_LoadingCircleBackNoise = False
        Me.Up.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Up.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Up.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Up.Prop_LoadingCircleHotGradient = False
        Me.Up.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Up.Prop_LoadingCircleHotNoise = False
        Me.Up.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Up.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Up.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Up.Prop_PrimaryColorGradient = False
        Me.Up.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Up.Prop_PrimaryNoise = False
        Me.Up.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Up.Prop_Scale = 1.0!
        Me.Up.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Up.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Up.Prop_SecondaryColorGradient = False
        Me.Up.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Up.Prop_SecondaryNoise = False
        Me.Up.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Up.Prop_Shadow_Blur = 5
        Me.Up.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Up.Prop_Shadow_Enabled = False
        Me.Up.Prop_Shadow_OffsetX = 2
        Me.Up.Prop_Shadow_OffsetY = 2
        Me.Up.Prop_Shadow_Opacity = 0.3!
        Me.Up.Size = New System.Drawing.Size(64, 64)
        Me.Up.TabIndex = 9
        '
        'NS
        '
        Me.NS.Location = New System.Drawing.Point(427, 7)
        Me.NS.Name = "NS"
        Me.NS.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.NS.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.NS.Prop_Cursor = WinPaletter.Paths.CursorType.NS
        Me.NS.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.NS.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.NS.Prop_LoadingCircleBackGradient = False
        Me.NS.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NS.Prop_LoadingCircleBackNoise = False
        Me.NS.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.NS.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NS.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NS.Prop_LoadingCircleHotGradient = False
        Me.NS.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NS.Prop_LoadingCircleHotNoise = False
        Me.NS.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.NS.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.NS.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.NS.Prop_PrimaryColorGradient = False
        Me.NS.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NS.Prop_PrimaryNoise = False
        Me.NS.Prop_PrimaryNoiseOpacity = 0.25!
        Me.NS.Prop_Scale = 1.0!
        Me.NS.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.NS.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.NS.Prop_SecondaryColorGradient = False
        Me.NS.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NS.Prop_SecondaryNoise = False
        Me.NS.Prop_SecondaryNoiseOpacity = 0.25!
        Me.NS.Prop_Shadow_Blur = 5
        Me.NS.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.NS.Prop_Shadow_Enabled = False
        Me.NS.Prop_Shadow_OffsetX = 2
        Me.NS.Prop_Shadow_OffsetY = 2
        Me.NS.Prop_Shadow_Opacity = 0.3!
        Me.NS.Size = New System.Drawing.Size(64, 64)
        Me.NS.TabIndex = 10
        '
        'EW
        '
        Me.EW.Location = New System.Drawing.Point(7, 77)
        Me.EW.Name = "EW"
        Me.EW.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.EW.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.EW.Prop_Cursor = WinPaletter.Paths.CursorType.EW
        Me.EW.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.EW.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.EW.Prop_LoadingCircleBackGradient = False
        Me.EW.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.EW.Prop_LoadingCircleBackNoise = False
        Me.EW.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.EW.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.EW.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.EW.Prop_LoadingCircleHotGradient = False
        Me.EW.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.EW.Prop_LoadingCircleHotNoise = False
        Me.EW.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.EW.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.EW.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.EW.Prop_PrimaryColorGradient = False
        Me.EW.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.EW.Prop_PrimaryNoise = False
        Me.EW.Prop_PrimaryNoiseOpacity = 0.25!
        Me.EW.Prop_Scale = 1.0!
        Me.EW.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.EW.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.EW.Prop_SecondaryColorGradient = False
        Me.EW.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.EW.Prop_SecondaryNoise = False
        Me.EW.Prop_SecondaryNoiseOpacity = 0.25!
        Me.EW.Prop_Shadow_Blur = 5
        Me.EW.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.EW.Prop_Shadow_Enabled = False
        Me.EW.Prop_Shadow_OffsetX = 2
        Me.EW.Prop_Shadow_OffsetY = 2
        Me.EW.Prop_Shadow_Opacity = 0.3!
        Me.EW.Size = New System.Drawing.Size(64, 64)
        Me.EW.TabIndex = 11
        '
        'NESW
        '
        Me.NESW.Location = New System.Drawing.Point(77, 77)
        Me.NESW.Name = "NESW"
        Me.NESW.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.NESW.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.NESW.Prop_Cursor = WinPaletter.Paths.CursorType.NESW
        Me.NESW.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.NESW.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.NESW.Prop_LoadingCircleBackGradient = False
        Me.NESW.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NESW.Prop_LoadingCircleBackNoise = False
        Me.NESW.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.NESW.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NESW.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NESW.Prop_LoadingCircleHotGradient = False
        Me.NESW.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NESW.Prop_LoadingCircleHotNoise = False
        Me.NESW.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.NESW.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.NESW.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.NESW.Prop_PrimaryColorGradient = False
        Me.NESW.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NESW.Prop_PrimaryNoise = False
        Me.NESW.Prop_PrimaryNoiseOpacity = 0.25!
        Me.NESW.Prop_Scale = 1.0!
        Me.NESW.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.NESW.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.NESW.Prop_SecondaryColorGradient = False
        Me.NESW.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NESW.Prop_SecondaryNoise = False
        Me.NESW.Prop_SecondaryNoiseOpacity = 0.25!
        Me.NESW.Prop_Shadow_Blur = 5
        Me.NESW.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.NESW.Prop_Shadow_Enabled = False
        Me.NESW.Prop_Shadow_OffsetX = 2
        Me.NESW.Prop_Shadow_OffsetY = 2
        Me.NESW.Prop_Shadow_Opacity = 0.3!
        Me.NESW.Size = New System.Drawing.Size(64, 64)
        Me.NESW.TabIndex = 12
        '
        'NWSE
        '
        Me.NWSE.Location = New System.Drawing.Point(147, 77)
        Me.NWSE.Name = "NWSE"
        Me.NWSE.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.NWSE.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.NWSE.Prop_Cursor = WinPaletter.Paths.CursorType.NWSE
        Me.NWSE.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.NWSE.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.NWSE.Prop_LoadingCircleBackGradient = False
        Me.NWSE.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NWSE.Prop_LoadingCircleBackNoise = False
        Me.NWSE.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.NWSE.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NWSE.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NWSE.Prop_LoadingCircleHotGradient = False
        Me.NWSE.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NWSE.Prop_LoadingCircleHotNoise = False
        Me.NWSE.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.NWSE.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.NWSE.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.NWSE.Prop_PrimaryColorGradient = False
        Me.NWSE.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NWSE.Prop_PrimaryNoise = False
        Me.NWSE.Prop_PrimaryNoiseOpacity = 0.25!
        Me.NWSE.Prop_Scale = 1.0!
        Me.NWSE.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.NWSE.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.NWSE.Prop_SecondaryColorGradient = False
        Me.NWSE.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.NWSE.Prop_SecondaryNoise = False
        Me.NWSE.Prop_SecondaryNoiseOpacity = 0.25!
        Me.NWSE.Prop_Shadow_Blur = 5
        Me.NWSE.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.NWSE.Prop_Shadow_Enabled = False
        Me.NWSE.Prop_Shadow_OffsetX = 2
        Me.NWSE.Prop_Shadow_OffsetY = 2
        Me.NWSE.Prop_Shadow_Opacity = 0.3!
        Me.NWSE.Size = New System.Drawing.Size(64, 64)
        Me.NWSE.TabIndex = 13
        '
        'Pen
        '
        Me.Pen.Location = New System.Drawing.Point(217, 77)
        Me.Pen.Name = "Pen"
        Me.Pen.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Pen.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Pen.Prop_Cursor = WinPaletter.Paths.CursorType.Pen
        Me.Pen.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Pen.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Pen.Prop_LoadingCircleBackGradient = False
        Me.Pen.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pen.Prop_LoadingCircleBackNoise = False
        Me.Pen.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Pen.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Pen.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Pen.Prop_LoadingCircleHotGradient = False
        Me.Pen.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pen.Prop_LoadingCircleHotNoise = False
        Me.Pen.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Pen.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Pen.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Pen.Prop_PrimaryColorGradient = False
        Me.Pen.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pen.Prop_PrimaryNoise = False
        Me.Pen.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Pen.Prop_Scale = 1.0!
        Me.Pen.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Pen.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Pen.Prop_SecondaryColorGradient = False
        Me.Pen.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pen.Prop_SecondaryNoise = False
        Me.Pen.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Pen.Prop_Shadow_Blur = 5
        Me.Pen.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Pen.Prop_Shadow_Enabled = False
        Me.Pen.Prop_Shadow_OffsetX = 2
        Me.Pen.Prop_Shadow_OffsetY = 2
        Me.Pen.Prop_Shadow_Opacity = 0.3!
        Me.Pen.Size = New System.Drawing.Size(64, 64)
        Me.Pen.TabIndex = 14
        '
        'None
        '
        Me.None.Location = New System.Drawing.Point(287, 77)
        Me.None.Name = "None"
        Me.None.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.None.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.None.Prop_Cursor = WinPaletter.Paths.CursorType.None
        Me.None.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.None.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.None.Prop_LoadingCircleBackGradient = False
        Me.None.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.None.Prop_LoadingCircleBackNoise = False
        Me.None.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.None.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.None.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.None.Prop_LoadingCircleHotGradient = False
        Me.None.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.None.Prop_LoadingCircleHotNoise = False
        Me.None.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.None.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.None.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.None.Prop_PrimaryColorGradient = False
        Me.None.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.None.Prop_PrimaryNoise = False
        Me.None.Prop_PrimaryNoiseOpacity = 0.25!
        Me.None.Prop_Scale = 1.0!
        Me.None.Prop_SecondaryColor1 = System.Drawing.Color.Red
        Me.None.Prop_SecondaryColor2 = System.Drawing.Color.Red
        Me.None.Prop_SecondaryColorGradient = False
        Me.None.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.None.Prop_SecondaryNoise = False
        Me.None.Prop_SecondaryNoiseOpacity = 0.25!
        Me.None.Prop_Shadow_Blur = 5
        Me.None.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.None.Prop_Shadow_Enabled = False
        Me.None.Prop_Shadow_OffsetX = 2
        Me.None.Prop_Shadow_OffsetY = 2
        Me.None.Prop_Shadow_Opacity = 0.3!
        Me.None.Size = New System.Drawing.Size(64, 64)
        Me.None.TabIndex = 15
        '
        'Link
        '
        Me.Link.Location = New System.Drawing.Point(357, 77)
        Me.Link.Name = "Link"
        Me.Link.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Link.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Link.Prop_Cursor = WinPaletter.Paths.CursorType.Link
        Me.Link.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Link.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Link.Prop_LoadingCircleBackGradient = False
        Me.Link.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Link.Prop_LoadingCircleBackNoise = False
        Me.Link.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Link.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Link.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Link.Prop_LoadingCircleHotGradient = False
        Me.Link.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Link.Prop_LoadingCircleHotNoise = False
        Me.Link.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Link.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Link.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Link.Prop_PrimaryColorGradient = False
        Me.Link.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Link.Prop_PrimaryNoise = False
        Me.Link.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Link.Prop_Scale = 1.0!
        Me.Link.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Link.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Link.Prop_SecondaryColorGradient = False
        Me.Link.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Link.Prop_SecondaryNoise = False
        Me.Link.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Link.Prop_Shadow_Blur = 5
        Me.Link.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Link.Prop_Shadow_Enabled = False
        Me.Link.Prop_Shadow_OffsetX = 2
        Me.Link.Prop_Shadow_OffsetY = 2
        Me.Link.Prop_Shadow_Opacity = 0.3!
        Me.Link.Size = New System.Drawing.Size(64, 64)
        Me.Link.TabIndex = 16
        '
        'Pin
        '
        Me.Pin.Location = New System.Drawing.Point(427, 77)
        Me.Pin.Name = "Pin"
        Me.Pin.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Pin.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Pin.Prop_Cursor = WinPaletter.Paths.CursorType.Pin
        Me.Pin.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Pin.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Pin.Prop_LoadingCircleBackGradient = False
        Me.Pin.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pin.Prop_LoadingCircleBackNoise = False
        Me.Pin.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Pin.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Pin.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Pin.Prop_LoadingCircleHotGradient = False
        Me.Pin.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pin.Prop_LoadingCircleHotNoise = False
        Me.Pin.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Pin.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Pin.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Pin.Prop_PrimaryColorGradient = False
        Me.Pin.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pin.Prop_PrimaryNoise = False
        Me.Pin.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Pin.Prop_Scale = 1.0!
        Me.Pin.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Pin.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Pin.Prop_SecondaryColorGradient = False
        Me.Pin.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Pin.Prop_SecondaryNoise = False
        Me.Pin.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Pin.Prop_Shadow_Blur = 5
        Me.Pin.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Pin.Prop_Shadow_Enabled = False
        Me.Pin.Prop_Shadow_OffsetX = 2
        Me.Pin.Prop_Shadow_OffsetY = 2
        Me.Pin.Prop_Shadow_Opacity = 0.3!
        Me.Pin.Size = New System.Drawing.Size(64, 64)
        Me.Pin.TabIndex = 17
        '
        'Person
        '
        Me.Person.Location = New System.Drawing.Point(7, 147)
        Me.Person.Name = "Person"
        Me.Person.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Person.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Person.Prop_Cursor = WinPaletter.Paths.CursorType.Person
        Me.Person.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Person.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Person.Prop_LoadingCircleBackGradient = False
        Me.Person.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Person.Prop_LoadingCircleBackNoise = False
        Me.Person.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Person.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Person.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Person.Prop_LoadingCircleHotGradient = False
        Me.Person.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Person.Prop_LoadingCircleHotNoise = False
        Me.Person.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Person.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Person.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Person.Prop_PrimaryColorGradient = False
        Me.Person.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Person.Prop_PrimaryNoise = False
        Me.Person.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Person.Prop_Scale = 1.0!
        Me.Person.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Person.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Person.Prop_SecondaryColorGradient = False
        Me.Person.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Person.Prop_SecondaryNoise = False
        Me.Person.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Person.Prop_Shadow_Blur = 5
        Me.Person.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Person.Prop_Shadow_Enabled = False
        Me.Person.Prop_Shadow_OffsetX = 2
        Me.Person.Prop_Shadow_OffsetY = 2
        Me.Person.Prop_Shadow_Opacity = 0.3!
        Me.Person.Size = New System.Drawing.Size(64, 64)
        Me.Person.TabIndex = 18
        '
        'IBeam
        '
        Me.IBeam.Location = New System.Drawing.Point(77, 147)
        Me.IBeam.Name = "IBeam"
        Me.IBeam.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.IBeam.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.IBeam.Prop_Cursor = WinPaletter.Paths.CursorType.IBeam
        Me.IBeam.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.IBeam.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.IBeam.Prop_LoadingCircleBackGradient = False
        Me.IBeam.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.IBeam.Prop_LoadingCircleBackNoise = False
        Me.IBeam.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.IBeam.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IBeam.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IBeam.Prop_LoadingCircleHotGradient = False
        Me.IBeam.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.IBeam.Prop_LoadingCircleHotNoise = False
        Me.IBeam.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.IBeam.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.IBeam.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.IBeam.Prop_PrimaryColorGradient = False
        Me.IBeam.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.IBeam.Prop_PrimaryNoise = False
        Me.IBeam.Prop_PrimaryNoiseOpacity = 0.25!
        Me.IBeam.Prop_Scale = 1.0!
        Me.IBeam.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.IBeam.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.IBeam.Prop_SecondaryColorGradient = False
        Me.IBeam.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.IBeam.Prop_SecondaryNoise = False
        Me.IBeam.Prop_SecondaryNoiseOpacity = 0.25!
        Me.IBeam.Prop_Shadow_Blur = 5
        Me.IBeam.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.IBeam.Prop_Shadow_Enabled = False
        Me.IBeam.Prop_Shadow_OffsetX = 2
        Me.IBeam.Prop_Shadow_OffsetY = 2
        Me.IBeam.Prop_Shadow_Opacity = 0.3!
        Me.IBeam.Size = New System.Drawing.Size(64, 64)
        Me.IBeam.TabIndex = 19
        '
        'Cross
        '
        Me.Cross.Location = New System.Drawing.Point(147, 147)
        Me.Cross.Name = "Cross"
        Me.Cross.Prop_ArrowStyle = WinPaletter.Paths.ArrowStyle.Aero
        Me.Cross.Prop_CircleStyle = WinPaletter.Paths.CircleStyle.Aero
        Me.Cross.Prop_Cursor = WinPaletter.Paths.CursorType.Cross
        Me.Cross.Prop_LoadingCircleBack1 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Cross.Prop_LoadingCircleBack2 = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Cross.Prop_LoadingCircleBackGradient = False
        Me.Cross.Prop_LoadingCircleBackGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Cross.Prop_LoadingCircleBackNoise = False
        Me.Cross.Prop_LoadingCircleBackNoiseOpacity = 0.25!
        Me.Cross.Prop_LoadingCircleHot1 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Cross.Prop_LoadingCircleHot2 = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Cross.Prop_LoadingCircleHotGradient = False
        Me.Cross.Prop_LoadingCircleHotGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Cross.Prop_LoadingCircleHotNoise = False
        Me.Cross.Prop_LoadingCircleHotNoiseOpacity = 0.25!
        Me.Cross.Prop_PrimaryColor1 = System.Drawing.Color.White
        Me.Cross.Prop_PrimaryColor2 = System.Drawing.Color.White
        Me.Cross.Prop_PrimaryColorGradient = False
        Me.Cross.Prop_PrimaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Cross.Prop_PrimaryNoise = False
        Me.Cross.Prop_PrimaryNoiseOpacity = 0.25!
        Me.Cross.Prop_Scale = 1.0!
        Me.Cross.Prop_SecondaryColor1 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Cross.Prop_SecondaryColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Cross.Prop_SecondaryColorGradient = False
        Me.Cross.Prop_SecondaryColorGradientMode = WinPaletter.Paths.GradientMode.Horizontal
        Me.Cross.Prop_SecondaryNoise = False
        Me.Cross.Prop_SecondaryNoiseOpacity = 0.25!
        Me.Cross.Prop_Shadow_Blur = 5
        Me.Cross.Prop_Shadow_Color = System.Drawing.Color.Black
        Me.Cross.Prop_Shadow_Enabled = False
        Me.Cross.Prop_Shadow_OffsetX = 2
        Me.Cross.Prop_Shadow_OffsetY = 2
        Me.Cross.Prop_Shadow_Opacity = 0.3!
        Me.Cross.Size = New System.Drawing.Size(64, 64)
        Me.Cross.TabIndex = 20
        '
        'cur_tip_btn
        '
        Me.cur_tip_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.cur_tip_btn.DrawOnGlass = False
        Me.cur_tip_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cur_tip_btn.ForeColor = System.Drawing.Color.White
        Me.cur_tip_btn.Image = Nothing
        Me.cur_tip_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.cur_tip_btn.Location = New System.Drawing.Point(500, 267)
        Me.cur_tip_btn.Name = "cur_tip_btn"
        Me.cur_tip_btn.Size = New System.Drawing.Size(20, 21)
        Me.cur_tip_btn.TabIndex = 71
        Me.cur_tip_btn.Text = "?"
        Me.cur_tip_btn.UseVisualStyleBackColor = False
        '
        'PictureBox12
        '
        Me.PictureBox12.Image = CType(resources.GetObject("PictureBox12.Image"), System.Drawing.Image)
        Me.PictureBox12.Location = New System.Drawing.Point(6, 265)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox12.TabIndex = 70
        Me.PictureBox12.TabStop = False
        '
        'CursorsSize_Bar
        '
        Me.CursorsSize_Bar.LargeChange = 50
        Me.CursorsSize_Bar.Location = New System.Drawing.Point(127, 268)
        Me.CursorsSize_Bar.Maximum = 320
        Me.CursorsSize_Bar.Minimum = 100
        Me.CursorsSize_Bar.Name = "CursorsSize_Bar"
        Me.CursorsSize_Bar.Size = New System.Drawing.Size(223, 19)
        Me.CursorsSize_Bar.SmallChange = 20
        Me.CursorsSize_Bar.TabIndex = 68
        Me.CursorsSize_Bar.Value = 100
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(36, 265)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 24)
        Me.Label17.TabIndex = 69
        Me.Label17.Text = "Scaling (1x)"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage5.Controls.Add(Me.search_results)
        Me.TabPage5.Location = New System.Drawing.Point(4, 24)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage5.Size = New System.Drawing.Size(1321, 599)
        Me.TabPage5.TabIndex = 3
        Me.TabPage5.Text = "2"
        '
        'search_results
        '
        Me.search_results.AutoScroll = True
        Me.search_results.Dock = System.Windows.Forms.DockStyle.Fill
        Me.search_results.Location = New System.Drawing.Point(10, 10)
        Me.search_results.Name = "search_results"
        Me.search_results.Size = New System.Drawing.Size(1301, 579)
        Me.search_results.TabIndex = 4
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.StopTimer_btn)
        Me.TabPage2.Controls.Add(Me.ExportDetails_btn)
        Me.TabPage2.Controls.Add(Me.log_lbl)
        Me.TabPage2.Controls.Add(Me.ShowErrors_btn)
        Me.TabPage2.Controls.Add(Me.ok_btn)
        Me.TabPage2.Controls.Add(Me.log)
        Me.TabPage2.Controls.Add(Me.Separator1)
        Me.TabPage2.Controls.Add(Me.log_header)
        Me.TabPage2.Controls.Add(Me.PictureBox36)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage2.Size = New System.Drawing.Size(1321, 599)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "3"
        '
        'StopTimer_btn
        '
        Me.StopTimer_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StopTimer_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.StopTimer_btn.DrawOnGlass = False
        Me.StopTimer_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.StopTimer_btn.ForeColor = System.Drawing.Color.White
        Me.StopTimer_btn.Image = Nothing
        Me.StopTimer_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.StopTimer_btn.Location = New System.Drawing.Point(1080, 552)
        Me.StopTimer_btn.Name = "StopTimer_btn"
        Me.StopTimer_btn.Size = New System.Drawing.Size(80, 34)
        Me.StopTimer_btn.TabIndex = 31
        Me.StopTimer_btn.Text = "Stop timer"
        Me.StopTimer_btn.UseVisualStyleBackColor = False
        Me.StopTimer_btn.Visible = False
        '
        'ExportDetails_btn
        '
        Me.ExportDetails_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExportDetails_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ExportDetails_btn.DrawOnGlass = False
        Me.ExportDetails_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ExportDetails_btn.ForeColor = System.Drawing.Color.White
        Me.ExportDetails_btn.Image = Nothing
        Me.ExportDetails_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ExportDetails_btn.Location = New System.Drawing.Point(1162, 552)
        Me.ExportDetails_btn.Name = "ExportDetails_btn"
        Me.ExportDetails_btn.Size = New System.Drawing.Size(85, 34)
        Me.ExportDetails_btn.TabIndex = 30
        Me.ExportDetails_btn.Text = "Export details"
        Me.ExportDetails_btn.UseVisualStyleBackColor = False
        Me.ExportDetails_btn.Visible = False
        '
        'log_lbl
        '
        Me.log_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.log_lbl.BackColor = System.Drawing.Color.Transparent
        Me.log_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.log_lbl.Location = New System.Drawing.Point(13, 552)
        Me.log_lbl.Name = "log_lbl"
        Me.log_lbl.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.log_lbl.Size = New System.Drawing.Size(977, 34)
        Me.log_lbl.TabIndex = 29
        Me.log_lbl.Text = "Error\s happened. Press on 'Show errors' for details"
        Me.log_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.log_lbl.Visible = False
        '
        'ShowErrors_btn
        '
        Me.ShowErrors_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowErrors_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ShowErrors_btn.DrawOnGlass = False
        Me.ShowErrors_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ShowErrors_btn.ForeColor = System.Drawing.Color.White
        Me.ShowErrors_btn.Image = Nothing
        Me.ShowErrors_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ShowErrors_btn.Location = New System.Drawing.Point(996, 552)
        Me.ShowErrors_btn.Name = "ShowErrors_btn"
        Me.ShowErrors_btn.Size = New System.Drawing.Size(82, 34)
        Me.ShowErrors_btn.TabIndex = 28
        Me.ShowErrors_btn.Text = "Show errors"
        Me.ShowErrors_btn.UseVisualStyleBackColor = False
        Me.ShowErrors_btn.Visible = False
        '
        'ok_btn
        '
        Me.ok_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ok_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ok_btn.DrawOnGlass = False
        Me.ok_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ok_btn.ForeColor = System.Drawing.Color.White
        Me.ok_btn.Image = Nothing
        Me.ok_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ok_btn.Location = New System.Drawing.Point(1249, 552)
        Me.ok_btn.Name = "ok_btn"
        Me.ok_btn.Size = New System.Drawing.Size(59, 34)
        Me.ok_btn.TabIndex = 27
        Me.ok_btn.Text = "OK"
        Me.ok_btn.UseVisualStyleBackColor = False
        '
        'log
        '
        Me.log.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.log.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.log.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.log.ForeColor = System.Drawing.Color.White
        Me.log.FullRowSelect = True
        Me.log.ItemHeight = 28
        Me.log.Location = New System.Drawing.Point(13, 61)
        Me.log.Name = "log"
        Me.log.ShowLines = False
        Me.log.Size = New System.Drawing.Size(1295, 485)
        Me.log.TabIndex = 26
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(13, 54)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(1220, 1)
        Me.Separator1.TabIndex = 25
        Me.Separator1.TabStop = False
        '
        'log_header
        '
        Me.log_header.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.log_header.BackColor = System.Drawing.Color.Transparent
        Me.log_header.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold)
        Me.log_header.Location = New System.Drawing.Point(54, 13)
        Me.log_header.Name = "log_header"
        Me.log_header.Size = New System.Drawing.Size(1179, 35)
        Me.log_header.TabIndex = 24
        Me.log_header.Text = "Log"
        Me.log_header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox36
        '
        Me.PictureBox36.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox36.Image = CType(resources.GetObject("PictureBox36.Image"), System.Drawing.Image)
        Me.PictureBox36.Location = New System.Drawing.Point(13, 13)
        Me.PictureBox36.Name = "PictureBox36"
        Me.PictureBox36.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox36.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox36.TabIndex = 23
        Me.PictureBox36.TabStop = False
        '
        'Store
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1329, 721)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.Titlebar_panel)
        Me.Controls.Add(Me.Status_pnl)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.MinimumSize = New System.Drawing.Size(1130, 680)
        Me.Name = "Store"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WinPaletter Store"
        Me.Titlebar_panel.ResumeLayout(False)
        Me.search_panel.ResumeLayout(False)
        CType(Me.Titlebar_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Status_pnl.ResumeLayout(False)
        Me.Tabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.previewContainer.ResumeLayout(False)
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.tabs_preview.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.pnl_preview.ResumeLayout(False)
        Me.Window1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.pnl_preview_classic.ResumeLayout(False)
        Me.ClassicTaskbar.ResumeLayout(False)
        Me.ClassicColorsPreview.ResumeLayout(False)
        Me.ClassicColorsPreview.PerformLayout()
        CType(Me.RetroShadow1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Menu_Window.ResumeLayout(False)
        Me.menucontainer3.ResumeLayout(False)
        Me.highlight.ResumeLayout(False)
        Me.menuhilight.ResumeLayout(False)
        Me.menucontainer1.ResumeLayout(False)
        Me.WindowR3.ResumeLayout(False)
        Me.WindowR3.PerformLayout()
        Me.WindowR2.ResumeLayout(False)
        Me.menucontainer0.ResumeLayout(False)
        Me.PanelR1.ResumeLayout(False)
        Me.WindowR4.ResumeLayout(False)
        Me.programcontainer.ResumeLayout(False)
        Me.PanelR2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Cursors_Container.ResumeLayout(False)
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.PictureBox36, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tabs As UI.WP.TablessControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents pnl_preview As Panel
    Friend WithEvents WXP_Alert2 As UI.WP.AlertBox
    Friend WithEvents ActionCenter As UI.Simulation.WinElement
    Friend WithEvents start As UI.Simulation.WinElement
    Friend WithEvents taskbar As UI.Simulation.WinElement
    Friend WithEvents Window2 As UI.Simulation.Window
    Friend WithEvents Window1 As UI.Simulation.Window
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label8 As UI.WP.LabelAlt
    Friend WithEvents setting_icon_preview As UI.WP.LabelAlt
    Friend WithEvents lnk_preview As UI.WP.LabelAlt
    Friend WithEvents pnl_preview_classic As Panel
    Friend WithEvents ClassicWindow1 As UI.Retro.WindowR
    Friend WithEvents ClassicWindow2 As UI.Retro.WindowR
    Friend WithEvents ClassicTaskbar As UI.Retro.PanelRaisedR
    Friend WithEvents ButtonR4 As UI.Retro.ButtonR
    Friend WithEvents ButtonR3 As UI.Retro.ButtonR
    Friend WithEvents ButtonR2 As UI.Retro.ButtonR
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents store_container As FlowLayoutPanel
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents back_btn As UI.WP.Button
    Friend WithEvents FilesFetcher As System.ComponentModel.BackgroundWorker
    Friend WithEvents Titlebar_panel As Panel
    Friend WithEvents Titlebar_lbl As UI.WP.LabelAlt
    Friend WithEvents Titlebar_img As PictureBox
    Friend WithEvents themeSize_lbl As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents previewContainer As UI.WP.GroupBox
    Friend WithEvents PictureBox41 As PictureBox
    Friend WithEvents Label19 As Label
    Friend WithEvents SupportedOS_lbl As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents PictureBox14 As PictureBox
    Friend WithEvents Apply_btn As UI.WP.Button
    Friend WithEvents ClassicColorsPreview As Panel
    Friend WithEvents RetroShadow1 As UI.WP.TransparentPictureBox
    Friend WithEvents Menu_Window As UI.Retro.WindowR
    Friend WithEvents menucontainer3 As Panel
    Friend WithEvents LabelR9 As UI.WP.LabelAlt
    Friend WithEvents highlight As Panel
    Friend WithEvents menuhilight As Panel
    Friend WithEvents LabelR5 As UI.WP.LabelAlt
    Friend WithEvents menucontainer1 As Panel
    Friend WithEvents LabelR6 As UI.WP.LabelAlt
    Friend WithEvents WindowR3 As UI.Retro.WindowR
    Friend WithEvents ButtonR5 As UI.Retro.ButtonR
    Friend WithEvents LabelR4 As UI.WP.LabelAlt
    Friend WithEvents LabelR13 As UI.WP.LabelAlt
    Friend WithEvents WindowR2 As UI.Retro.WindowR
    Friend WithEvents TextBoxR1 As UI.Retro.TextBoxR
    Friend WithEvents menucontainer0 As Panel
    Friend WithEvents PanelR1 As UI.Retro.PanelR
    Friend WithEvents LabelR3 As UI.WP.LabelAlt
    Friend WithEvents LabelR2 As UI.WP.LabelAlt
    Friend WithEvents LabelR1 As UI.WP.LabelAlt
    Friend WithEvents WindowR1 As UI.Retro.WindowR
    Friend WithEvents WindowR4 As UI.Retro.WindowR
    Friend WithEvents programcontainer As Panel
    Friend WithEvents PanelR2 As UI.Retro.ScrollBarR
    Friend WithEvents ButtonR12 As UI.Retro.ButtonR
    Friend WithEvents ButtonR11 As UI.Retro.ButtonR
    Friend WithEvents ButtonR10 As UI.Retro.ButtonR
    Friend WithEvents StopTimer_btn As UI.WP.Button
    Friend WithEvents ExportDetails_btn As UI.WP.Button
    Friend WithEvents log_lbl As Label
    Friend WithEvents ShowErrors_btn As UI.WP.Button
    Friend WithEvents ok_btn As UI.WP.Button
    Friend WithEvents log As Windows.Forms.TreeView
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents log_header As Label
    Friend WithEvents PictureBox36 As PictureBox
    Friend WithEvents Log_Timer As Timer
    Friend WithEvents Edit_btn As UI.WP.Button
    Friend WithEvents RestartExplorer As UI.WP.Button
    Friend WithEvents search_box As UI.WP.TextBox
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents search_results As FlowLayoutPanel
    Friend WithEvents search_btn As UI.WP.Button
    Friend WithEvents search_filter_btn As UI.WP.Button
    Friend WithEvents CMD1 As UI.Simulation.WinCMD
    Friend WithEvents CMD2 As UI.Simulation.WinCMD
    Friend WithEvents CMD3 As UI.Simulation.WinCMD
    Friend WithEvents cur_anim_btn As UI.WP.Button
    Friend WithEvents cur_tip_btn As UI.WP.Button
    Friend WithEvents Cursors_Container As FlowLayoutPanel
    Friend WithEvents Arrow As CursorControl
    Friend WithEvents Help As CursorControl
    Friend WithEvents AppLoading As CursorControl
    Friend WithEvents Busy As CursorControl
    Friend WithEvents Move_Cur As CursorControl
    Friend WithEvents Up As CursorControl
    Friend WithEvents NS As CursorControl
    Friend WithEvents EW As CursorControl
    Friend WithEvents NESW As CursorControl
    Friend WithEvents NWSE As CursorControl
    Friend WithEvents Pen As CursorControl
    Friend WithEvents None As CursorControl
    Friend WithEvents Link As CursorControl
    Friend WithEvents Pin As CursorControl
    Friend WithEvents Person As CursorControl
    Friend WithEvents IBeam As CursorControl
    Friend WithEvents Cross As CursorControl
    Friend WithEvents PictureBox12 As PictureBox
    Friend WithEvents Label17 As Label
    Friend WithEvents CursorsSize_Bar As UI.WP.Trackbar
    Friend WithEvents Cursor_Timer As Timer
    Friend WithEvents search_panel As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Status_pnl As Panel
    Friend WithEvents Status_lbl As Label
    Friend WithEvents respacksize_lbl As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox1 As UI.WP.GroupBox
    Friend WithEvents GroupBox3 As UI.WP.GroupBox
    Friend WithEvents StoreItem1 As UI.Controllers.StoreItem
    Friend WithEvents SeparatorVertical1 As UI.WP.SeparatorV
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents tabs_preview As UI.WP.TablessControl
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents desc_txt As UI.WP.TextBox
    Friend WithEvents VersionAlert_lbl As UI.WP.AlertBox
    Friend WithEvents author_url_button As UI.WP.Button
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Theme_MD5_lbl As Label
End Class
