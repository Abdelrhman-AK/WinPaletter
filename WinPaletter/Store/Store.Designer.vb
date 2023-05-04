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
        Me.search_btn = New WinPaletter.XenonButton()
        Me.search_box = New WinPaletter.XenonTextBox()
        Me.search_filter_btn = New WinPaletter.XenonButton()
        Me.Titlebar_lbl = New System.Windows.Forms.Label()
        Me.back_btn = New WinPaletter.XenonButton()
        Me.Titlebar_img = New System.Windows.Forms.PictureBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Log_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Cursor_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Tabs = New WinPaletter.TablessControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.store_container = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.RestartExplorer = New WinPaletter.XenonButton()
        Me.Edit_btn = New WinPaletter.XenonButton()
        Me.Apply_btn = New WinPaletter.XenonButton()
        Me.XenonGroupBox3 = New WinPaletter.XenonGroupBox()
        Me.SupportedOS_lbl = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.PictureBox14 = New System.Windows.Forms.PictureBox()
        Me.previewContainer = New WinPaletter.XenonGroupBox()
        Me.ShowPS64_btn = New WinPaletter.XenonButton()
        Me.ShowPS86_btn = New WinPaletter.XenonButton()
        Me.ShowCursors_btn = New WinPaletter.XenonButton()
        Me.tabs_preview = New WinPaletter.TablessControl()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.pnl_preview = New System.Windows.Forms.Panel()
        Me.WXP_Alert2 = New WinPaletter.XenonAlertBox()
        Me.ActionCenter = New WinPaletter.XenonWinElement()
        Me.start = New WinPaletter.XenonWinElement()
        Me.taskbar = New WinPaletter.XenonWinElement()
        Me.XenonWindow2 = New WinPaletter.XenonWindow()
        Me.XenonWindow1 = New WinPaletter.XenonWindow()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.setting_icon_preview = New System.Windows.Forms.Label()
        Me.lnk_preview = New System.Windows.Forms.Label()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.pnl_preview_classic = New System.Windows.Forms.Panel()
        Me.ClassicWindow1 = New WinPaletter.RetroWindow()
        Me.ClassicWindow2 = New WinPaletter.RetroWindow()
        Me.ClassicTaskbar = New WinPaletter.RetroPanelRaised()
        Me.RetroButton4 = New WinPaletter.RetroButton()
        Me.RetroButton3 = New WinPaletter.RetroButton()
        Me.RetroButton2 = New WinPaletter.RetroButton()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.ClassicColorsPreview = New System.Windows.Forms.Panel()
        Me.RetroShadow1 = New WinPaletter.TransparentPictureBox()
        Me.Menu_Window = New WinPaletter.RetroWindow()
        Me.menucontainer3 = New System.Windows.Forms.Panel()
        Me.RetroLabel9 = New WinPaletter.RetroLabel()
        Me.highlight = New System.Windows.Forms.Panel()
        Me.menuhilight = New System.Windows.Forms.Panel()
        Me.RetroLabel5 = New WinPaletter.RetroLabel()
        Me.menucontainer1 = New System.Windows.Forms.Panel()
        Me.RetroLabel6 = New WinPaletter.RetroLabel()
        Me.RetroWindow3 = New WinPaletter.RetroWindow()
        Me.RetroButton5 = New WinPaletter.RetroButton()
        Me.RetroLabel4 = New WinPaletter.RetroLabel()
        Me.RetroLabel13 = New WinPaletter.RetroLabel()
        Me.RetroWindow2 = New WinPaletter.RetroWindow()
        Me.RetroTextBox1 = New WinPaletter.RetroTextBox()
        Me.menucontainer0 = New System.Windows.Forms.Panel()
        Me.RetroPanel1 = New WinPaletter.RetroPanel()
        Me.RetroLabel3 = New WinPaletter.RetroLabel()
        Me.RetroLabel2 = New WinPaletter.RetroLabel()
        Me.RetroLabel1 = New WinPaletter.RetroLabel()
        Me.RetroWindow1 = New WinPaletter.RetroWindow()
        Me.RetroWindow4 = New WinPaletter.RetroWindow()
        Me.programcontainer = New System.Windows.Forms.Panel()
        Me.RetroPanel2 = New WinPaletter.RetroScrollBar()
        Me.RetroButton12 = New WinPaletter.RetroButton()
        Me.RetroButton11 = New WinPaletter.RetroButton()
        Me.RetroButton10 = New WinPaletter.RetroButton()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.XenonCMD1 = New WinPaletter.XenonCMD()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.XenonCMD2 = New WinPaletter.XenonCMD()
        Me.TabPage10 = New System.Windows.Forms.TabPage()
        Me.XenonCMD3 = New WinPaletter.XenonCMD()
        Me.TabPage11 = New System.Windows.Forms.TabPage()
        Me.cur_anim_btn = New WinPaletter.XenonButton()
        Me.cur_tip_btn = New WinPaletter.XenonButton()
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
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CursorsSize_Bar = New WinPaletter.XenonTrackbar()
        Me.desc_txt = New WinPaletter.XenonTextBox()
        Me.ShowClassic_btn = New WinPaletter.XenonButton()
        Me.PictureBox41 = New System.Windows.Forms.PictureBox()
        Me.ShowCMD_btn = New WinPaletter.XenonButton()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Switch_M_C_btn = New WinPaletter.XenonButton()
        Me.PictureBox13 = New System.Windows.Forms.PictureBox()
        Me.XenonGroupBox2 = New WinPaletter.XenonGroupBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.XenonGroupBox1 = New WinPaletter.XenonGroupBox()
        Me.themeSize_lbl = New System.Windows.Forms.Label()
        Me.author_lbl = New System.Windows.Forms.Label()
        Me.theme_ver_lbl = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.MD5_lbl = New System.Windows.Forms.Label()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.theme_name_lbl = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.search_results = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.StopTimer_btn = New WinPaletter.XenonButton()
        Me.ExportDetails_btn = New WinPaletter.XenonButton()
        Me.log_lbl = New System.Windows.Forms.Label()
        Me.ShowErrors_btn = New WinPaletter.XenonButton()
        Me.ok_btn = New WinPaletter.XenonButton()
        Me.log = New System.Windows.Forms.TreeView()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.log_header = New System.Windows.Forms.Label()
        Me.PictureBox36 = New System.Windows.Forms.PictureBox()
        Me.Status_pnl = New System.Windows.Forms.Panel()
        Me.Status_lbl = New System.Windows.Forms.Label()
        Me.Author_link = New System.Windows.Forms.Label()
        Me.Download_Link = New System.Windows.Forms.Label()
        Me.Titlebar_panel.SuspendLayout()
        Me.search_panel.SuspendLayout()
        CType(Me.Titlebar_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.XenonGroupBox3.SuspendLayout()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.previewContainer.SuspendLayout()
        Me.tabs_preview.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.pnl_preview.SuspendLayout()
        Me.XenonWindow1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.pnl_preview_classic.SuspendLayout()
        Me.ClassicTaskbar.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.ClassicColorsPreview.SuspendLayout()
        CType(Me.RetroShadow1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Menu_Window.SuspendLayout()
        Me.menucontainer3.SuspendLayout()
        Me.highlight.SuspendLayout()
        Me.menuhilight.SuspendLayout()
        Me.menucontainer1.SuspendLayout()
        Me.RetroWindow3.SuspendLayout()
        Me.RetroWindow2.SuspendLayout()
        Me.menucontainer0.SuspendLayout()
        Me.RetroPanel1.SuspendLayout()
        Me.RetroWindow4.SuspendLayout()
        Me.programcontainer.SuspendLayout()
        Me.RetroPanel2.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.TabPage9.SuspendLayout()
        Me.TabPage10.SuspendLayout()
        Me.TabPage11.SuspendLayout()
        Me.Cursors_Container.SuspendLayout()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox2.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox1.SuspendLayout()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox36, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Status_pnl.SuspendLayout()
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
        Me.Titlebar_panel.Size = New System.Drawing.Size(1254, 70)
        Me.Titlebar_panel.TabIndex = 5
        '
        'search_panel
        '
        Me.search_panel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.search_panel.BackColor = System.Drawing.Color.Transparent
        Me.search_panel.Controls.Add(Me.search_btn)
        Me.search_panel.Controls.Add(Me.search_box)
        Me.search_panel.Controls.Add(Me.search_filter_btn)
        Me.search_panel.Location = New System.Drawing.Point(899, 17)
        Me.search_panel.Name = "search_panel"
        Me.search_panel.Size = New System.Drawing.Size(343, 30)
        Me.search_panel.TabIndex = 42
        '
        'search_btn
        '
        Me.search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.search_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.search_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.search_btn.ForeColor = System.Drawing.Color.White
        Me.search_btn.Image = CType(resources.GetObject("search_btn.Image"), System.Drawing.Image)
        Me.search_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.search_btn.LineSize = 1
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
        Me.search_box.ForeColor = System.Drawing.Color.White
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
        Me.search_filter_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.search_filter_btn.ForeColor = System.Drawing.Color.White
        Me.search_filter_btn.Image = CType(resources.GetObject("search_filter_btn.Image"), System.Drawing.Image)
        Me.search_filter_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.search_filter_btn.LineSize = 1
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
        Me.Titlebar_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Titlebar_lbl.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Titlebar_lbl.Location = New System.Drawing.Point(75, 14)
        Me.Titlebar_lbl.Name = "Titlebar_lbl"
        Me.Titlebar_lbl.Size = New System.Drawing.Size(818, 37)
        Me.Titlebar_lbl.TabIndex = 38
        Me.Titlebar_lbl.Text = "WinPaletter Store (Beta)"
        Me.Titlebar_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'back_btn
        '
        Me.back_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.back_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.back_btn.ForeColor = System.Drawing.Color.White
        Me.back_btn.Image = Nothing
        Me.back_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.back_btn.LineSize = 1
        Me.back_btn.Location = New System.Drawing.Point(5, 0)
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
        Me.ProgressBar1.Location = New System.Drawing.Point(1023, 3)
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
        Me.Cursor_Timer.Interval = 30
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
        Me.Tabs.Size = New System.Drawing.Size(1254, 627)
        Me.Tabs.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.store_container)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage1.Size = New System.Drawing.Size(1246, 599)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "0"
        '
        'store_container
        '
        Me.store_container.AutoScroll = True
        Me.store_container.Dock = System.Windows.Forms.DockStyle.Fill
        Me.store_container.Location = New System.Drawing.Point(10, 10)
        Me.store_container.Name = "store_container"
        Me.store_container.Size = New System.Drawing.Size(1226, 579)
        Me.store_container.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.RestartExplorer)
        Me.TabPage3.Controls.Add(Me.Edit_btn)
        Me.TabPage3.Controls.Add(Me.Apply_btn)
        Me.TabPage3.Controls.Add(Me.XenonGroupBox3)
        Me.TabPage3.Controls.Add(Me.previewContainer)
        Me.TabPage3.Controls.Add(Me.XenonGroupBox2)
        Me.TabPage3.Controls.Add(Me.XenonGroupBox1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage3.Size = New System.Drawing.Size(1246, 599)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "1"
        '
        'RestartExplorer
        '
        Me.RestartExplorer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RestartExplorer.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.RestartExplorer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RestartExplorer.ForeColor = System.Drawing.Color.White
        Me.RestartExplorer.Image = Nothing
        Me.RestartExplorer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RestartExplorer.LineColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.RestartExplorer.LineSize = 1
        Me.RestartExplorer.Location = New System.Drawing.Point(886, 552)
        Me.RestartExplorer.Name = "RestartExplorer"
        Me.RestartExplorer.Size = New System.Drawing.Size(140, 34)
        Me.RestartExplorer.TabIndex = 138
        Me.RestartExplorer.Text = "Restart Explorer"
        Me.RestartExplorer.UseVisualStyleBackColor = False
        '
        'Edit_btn
        '
        Me.Edit_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Edit_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Edit_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Edit_btn.ForeColor = System.Drawing.Color.White
        Me.Edit_btn.Image = CType(resources.GetObject("Edit_btn.Image"), System.Drawing.Image)
        Me.Edit_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Edit_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.Edit_btn.LineSize = 1
        Me.Edit_btn.Location = New System.Drawing.Point(1032, 552)
        Me.Edit_btn.Name = "Edit_btn"
        Me.Edit_btn.Size = New System.Drawing.Size(100, 34)
        Me.Edit_btn.TabIndex = 137
        Me.Edit_btn.Text = "Edit"
        Me.Edit_btn.UseVisualStyleBackColor = False
        '
        'Apply_btn
        '
        Me.Apply_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Apply_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Apply_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Apply_btn.ForeColor = System.Drawing.Color.White
        Me.Apply_btn.Image = Nothing
        Me.Apply_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Apply_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.Apply_btn.LineSize = 1
        Me.Apply_btn.Location = New System.Drawing.Point(1138, 552)
        Me.Apply_btn.Name = "Apply_btn"
        Me.Apply_btn.Size = New System.Drawing.Size(100, 34)
        Me.Apply_btn.TabIndex = 134
        Me.Apply_btn.Text = "Apply"
        Me.Apply_btn.UseVisualStyleBackColor = False
        '
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.SupportedOS_lbl)
        Me.XenonGroupBox3.Controls.Add(Me.Label26)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox14)
        Me.XenonGroupBox3.Location = New System.Drawing.Point(13, 235)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(681, 55)
        Me.XenonGroupBox3.TabIndex = 132
        '
        'SupportedOS_lbl
        '
        Me.SupportedOS_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SupportedOS_lbl.BackColor = System.Drawing.Color.Transparent
        Me.SupportedOS_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SupportedOS_lbl.Location = New System.Drawing.Point(33, 27)
        Me.SupportedOS_lbl.Name = "SupportedOS_lbl"
        Me.SupportedOS_lbl.Size = New System.Drawing.Size(645, 24)
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
        Me.Label26.Size = New System.Drawing.Size(645, 24)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "This theme can be applied to all operation systems, but it was designed specifica" &
    "lly for:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox14
        '
        Me.PictureBox14.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox14.Image = CType(resources.GetObject("PictureBox14.Image"), System.Drawing.Image)
        Me.PictureBox14.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox14.Name = "PictureBox14"
        Me.PictureBox14.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox14.TabIndex = 0
        Me.PictureBox14.TabStop = False
        '
        'previewContainer
        '
        Me.previewContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.previewContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.previewContainer.Controls.Add(Me.ShowPS64_btn)
        Me.previewContainer.Controls.Add(Me.ShowPS86_btn)
        Me.previewContainer.Controls.Add(Me.ShowCursors_btn)
        Me.previewContainer.Controls.Add(Me.tabs_preview)
        Me.previewContainer.Controls.Add(Me.desc_txt)
        Me.previewContainer.Controls.Add(Me.ShowClassic_btn)
        Me.previewContainer.Controls.Add(Me.PictureBox41)
        Me.previewContainer.Controls.Add(Me.ShowCMD_btn)
        Me.previewContainer.Controls.Add(Me.Label24)
        Me.previewContainer.Controls.Add(Me.Label19)
        Me.previewContainer.Controls.Add(Me.Switch_M_C_btn)
        Me.previewContainer.Controls.Add(Me.PictureBox13)
        Me.previewContainer.Location = New System.Drawing.Point(701, 13)
        Me.previewContainer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.previewContainer.Name = "previewContainer"
        Me.previewContainer.Padding = New System.Windows.Forms.Padding(1)
        Me.previewContainer.Size = New System.Drawing.Size(536, 533)
        Me.previewContainer.TabIndex = 131
        '
        'ShowPS64_btn
        '
        Me.ShowPS64_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowPS64_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ShowPS64_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ShowPS64_btn.ForeColor = System.Drawing.Color.White
        Me.ShowPS64_btn.Image = Nothing
        Me.ShowPS64_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ShowPS64_btn.LineSize = 1
        Me.ShowPS64_btn.Location = New System.Drawing.Point(393, 377)
        Me.ShowPS64_btn.Name = "ShowPS64_btn"
        Me.ShowPS64_btn.Size = New System.Drawing.Size(139, 26)
        Me.ShowPS64_btn.TabIndex = 138
        Me.ShowPS64_btn.Text = "PowerShell x64"
        Me.ShowPS64_btn.UseVisualStyleBackColor = False
        '
        'ShowPS86_btn
        '
        Me.ShowPS86_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowPS86_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ShowPS86_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ShowPS86_btn.ForeColor = System.Drawing.Color.White
        Me.ShowPS86_btn.Image = Nothing
        Me.ShowPS86_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ShowPS86_btn.LineSize = 1
        Me.ShowPS86_btn.Location = New System.Drawing.Point(248, 377)
        Me.ShowPS86_btn.Name = "ShowPS86_btn"
        Me.ShowPS86_btn.Size = New System.Drawing.Size(139, 26)
        Me.ShowPS86_btn.TabIndex = 137
        Me.ShowPS86_btn.Text = "PowerShell x86"
        Me.ShowPS86_btn.UseVisualStyleBackColor = False
        '
        'ShowCursors_btn
        '
        Me.ShowCursors_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowCursors_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ShowCursors_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ShowCursors_btn.ForeColor = System.Drawing.Color.White
        Me.ShowCursors_btn.Image = Nothing
        Me.ShowCursors_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ShowCursors_btn.LineSize = 1
        Me.ShowCursors_btn.Location = New System.Drawing.Point(393, 345)
        Me.ShowCursors_btn.Name = "ShowCursors_btn"
        Me.ShowCursors_btn.Size = New System.Drawing.Size(139, 26)
        Me.ShowCursors_btn.TabIndex = 136
        Me.ShowCursors_btn.Text = "Cursors"
        Me.ShowCursors_btn.UseVisualStyleBackColor = False
        '
        'tabs_preview
        '
        Me.tabs_preview.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabs_preview.Controls.Add(Me.TabPage6)
        Me.tabs_preview.Controls.Add(Me.TabPage7)
        Me.tabs_preview.Controls.Add(Me.TabPage4)
        Me.tabs_preview.Controls.Add(Me.TabPage8)
        Me.tabs_preview.Controls.Add(Me.TabPage9)
        Me.tabs_preview.Controls.Add(Me.TabPage10)
        Me.tabs_preview.Controls.Add(Me.TabPage11)
        Me.tabs_preview.Location = New System.Drawing.Point(4, 42)
        Me.tabs_preview.Name = "tabs_preview"
        Me.tabs_preview.SelectedIndex = 0
        Me.tabs_preview.Size = New System.Drawing.Size(528, 297)
        Me.tabs_preview.TabIndex = 42
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage6.Controls.Add(Me.pnl_preview)
        Me.TabPage6.Location = New System.Drawing.Point(4, 24)
        Me.TabPage6.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(520, 269)
        Me.TabPage6.TabIndex = 0
        Me.TabPage6.Text = "0"
        '
        'pnl_preview
        '
        Me.pnl_preview.BackColor = System.Drawing.Color.Black
        Me.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_preview.Controls.Add(Me.WXP_Alert2)
        Me.pnl_preview.Controls.Add(Me.ActionCenter)
        Me.pnl_preview.Controls.Add(Me.start)
        Me.pnl_preview.Controls.Add(Me.taskbar)
        Me.pnl_preview.Controls.Add(Me.XenonWindow2)
        Me.pnl_preview.Controls.Add(Me.XenonWindow1)
        Me.pnl_preview.Location = New System.Drawing.Point(0, 0)
        Me.pnl_preview.Name = "pnl_preview"
        Me.pnl_preview.Size = New System.Drawing.Size(528, 297)
        Me.pnl_preview.TabIndex = 2
        '
        'WXP_Alert2
        '
        Me.WXP_Alert2.AlertStyle = WinPaletter.XenonAlertBox.Style.Warning
        Me.WXP_Alert2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WXP_Alert2.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.WXP_Alert2.CenterText = True
        Me.WXP_Alert2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.WXP_Alert2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.WXP_Alert2.Image = Nothing
        Me.WXP_Alert2.Location = New System.Drawing.Point(7, 8)
        Me.WXP_Alert2.Name = "WXP_Alert2"
        Me.WXP_Alert2.Size = New System.Drawing.Size(24, 22)
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
        Me.ActionCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.ActionCenter.BackColor2 = System.Drawing.Color.Empty
        Me.ActionCenter.BackColorAlpha = 50
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
        Me.ActionCenter.Style = WinPaletter.XenonWinElement.Styles.ActionCenter11
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
        Me.start.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.start.BackColor2 = System.Drawing.Color.Empty
        Me.start.BackColorAlpha = 150
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
        Me.start.Style = WinPaletter.XenonWinElement.Styles.Start11
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
        Me.taskbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.taskbar.BackColor2 = System.Drawing.Color.Empty
        Me.taskbar.BackColorAlpha = 130
        Me.taskbar.BlurPower = 12
        Me.taskbar.DarkMode = True
        Me.taskbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.taskbar.LinkColor = System.Drawing.Color.Empty
        Me.taskbar.Location = New System.Drawing.Point(0, 255)
        Me.taskbar.Name = "taskbar"
        Me.taskbar.NoisePower = 0.2!
        Me.taskbar.Shadow = True
        Me.taskbar.Size = New System.Drawing.Size(528, 42)
        Me.taskbar.StartColor = System.Drawing.Color.Empty
        Me.taskbar.Style = WinPaletter.XenonWinElement.Styles.Taskbar11
        Me.taskbar.TabIndex = 0
        Me.taskbar.Transparency = True
        Me.taskbar.UseWin11ORB_WithWin10 = False
        Me.taskbar.UseWin11RoundedCorners_WithWin10_Level1 = False
        Me.taskbar.UseWin11RoundedCorners_WithWin10_Level2 = False
        Me.taskbar.Win7ColorBal = 100
        Me.taskbar.Win7GlowBal = 100
        '
        'XenonWindow2
        '
        Me.XenonWindow2.AccentColor_Active = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.XenonWindow2.AccentColor_Enabled = True
        Me.XenonWindow2.AccentColor_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.XenonWindow2.AccentColor2_Active = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.XenonWindow2.AccentColor2_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.XenonWindow2.Active = False
        Me.XenonWindow2.BackColor = System.Drawing.Color.Transparent
        Me.XenonWindow2.DarkMode = True
        Me.XenonWindow2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonWindow2.Location = New System.Drawing.Point(172, 160)
        Me.XenonWindow2.Metrics_BorderWidth = 1
        Me.XenonWindow2.Metrics_CaptionHeight = 22
        Me.XenonWindow2.Metrics_PaddedBorderWidth = 4
        Me.XenonWindow2.Name = "XenonWindow2"
        Me.XenonWindow2.Padding = New System.Windows.Forms.Padding(4, 40, 4, 4)
        Me.XenonWindow2.Preview = WinPaletter.XenonWindow.Preview_Enum.W11
        Me.XenonWindow2.Radius = 5
        Me.XenonWindow2.Shadow = True
        Me.XenonWindow2.Size = New System.Drawing.Size(189, 85)
        Me.XenonWindow2.TabIndex = 3
        Me.XenonWindow2.Text = "Inactive app"
        Me.XenonWindow2.ToolWindow = False
        Me.XenonWindow2.Win7Alpha = 100
        Me.XenonWindow2.Win7ColorBal = 100
        Me.XenonWindow2.Win7GlowBal = 100
        Me.XenonWindow2.Win7Noise = 1.0!
        Me.XenonWindow2.WinVista = False
        '
        'XenonWindow1
        '
        Me.XenonWindow1.AccentColor_Active = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.XenonWindow1.AccentColor_Enabled = True
        Me.XenonWindow1.AccentColor_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.XenonWindow1.AccentColor2_Active = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.XenonWindow1.AccentColor2_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.XenonWindow1.Active = True
        Me.XenonWindow1.BackColor = System.Drawing.Color.Transparent
        Me.XenonWindow1.Controls.Add(Me.Panel3)
        Me.XenonWindow1.Controls.Add(Me.lnk_preview)
        Me.XenonWindow1.DarkMode = True
        Me.XenonWindow1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XenonWindow1.Location = New System.Drawing.Point(172, 13)
        Me.XenonWindow1.Metrics_BorderWidth = 1
        Me.XenonWindow1.Metrics_CaptionHeight = 22
        Me.XenonWindow1.Metrics_PaddedBorderWidth = 4
        Me.XenonWindow1.Name = "XenonWindow1"
        Me.XenonWindow1.Padding = New System.Windows.Forms.Padding(4, 40, 4, 4)
        Me.XenonWindow1.Preview = WinPaletter.XenonWindow.Preview_Enum.W11
        Me.XenonWindow1.Radius = 5
        Me.XenonWindow1.Shadow = True
        Me.XenonWindow1.Size = New System.Drawing.Size(189, 147)
        Me.XenonWindow1.TabIndex = 2
        Me.XenonWindow1.Text = "App Preview"
        Me.XenonWindow1.ToolWindow = False
        Me.XenonWindow1.Win7Alpha = 100
        Me.XenonWindow1.Win7ColorBal = 100
        Me.XenonWindow1.Win7GlowBal = 100
        Me.XenonWindow1.Win7Noise = 1.0!
        Me.XenonWindow1.WinVista = False
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
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(1, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(179, 31)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "This is a setting icon"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'setting_icon_preview
        '
        Me.setting_icon_preview.BackColor = System.Drawing.Color.Transparent
        Me.setting_icon_preview.Dock = System.Windows.Forms.DockStyle.Top
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
        Me.lnk_preview.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lnk_preview.ForeColor = System.Drawing.Color.Brown
        Me.lnk_preview.Location = New System.Drawing.Point(4, 118)
        Me.lnk_preview.Name = "lnk_preview"
        Me.lnk_preview.Size = New System.Drawing.Size(181, 25)
        Me.lnk_preview.TabIndex = 16
        Me.lnk_preview.Text = "Settings link preview"
        Me.lnk_preview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage7.Controls.Add(Me.pnl_preview_classic)
        Me.TabPage7.Location = New System.Drawing.Point(4, 24)
        Me.TabPage7.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(520, 269)
        Me.TabPage7.TabIndex = 1
        Me.TabPage7.Text = "1"
        '
        'pnl_preview_classic
        '
        Me.pnl_preview_classic.BackColor = System.Drawing.Color.Black
        Me.pnl_preview_classic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
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
        Me.ClassicWindow1.TitlebarText = "Active App"
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
        Me.ClassicWindow2.TitlebarText = "Inactive App"
        Me.ClassicWindow2.UseItAsMenu = False
        '
        'ClassicTaskbar
        '
        Me.ClassicTaskbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClassicTaskbar.ButtonDkShadow = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.ClassicTaskbar.ButtonHilight = System.Drawing.Color.White
        Me.ClassicTaskbar.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.ClassicTaskbar.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClassicTaskbar.Controls.Add(Me.RetroButton4)
        Me.ClassicTaskbar.Controls.Add(Me.RetroButton3)
        Me.ClassicTaskbar.Controls.Add(Me.RetroButton2)
        Me.ClassicTaskbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ClassicTaskbar.Flat = False
        Me.ClassicTaskbar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.ClassicTaskbar.ForeColor = System.Drawing.Color.Black
        Me.ClassicTaskbar.Location = New System.Drawing.Point(0, 253)
        Me.ClassicTaskbar.Name = "ClassicTaskbar"
        Me.ClassicTaskbar.Size = New System.Drawing.Size(528, 44)
        Me.ClassicTaskbar.Style2 = False
        Me.ClassicTaskbar.TabIndex = 0
        Me.ClassicTaskbar.UseItAsWin7Taskbar = True
        '
        'RetroButton4
        '
        Me.RetroButton4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RetroButton4.AppearsAsPressed = False
        Me.RetroButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton4.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroButton4.ButtonHilight = System.Drawing.Color.White
        Me.RetroButton4.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton4.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroButton4.FocusRectHeight = 1
        Me.RetroButton4.FocusRectWidth = 1
        Me.RetroButton4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RetroButton4.ForeColor = System.Drawing.Color.Black
        Me.RetroButton4.HatchBrush = False
        Me.RetroButton4.Image = Nothing
        Me.RetroButton4.Location = New System.Drawing.Point(113, 4)
        Me.RetroButton4.Name = "RetroButton4"
        Me.RetroButton4.Size = New System.Drawing.Size(48, 38)
        Me.RetroButton4.TabIndex = 2
        Me.RetroButton4.UseItAsScrollbar = False
        Me.RetroButton4.UseVisualStyleBackColor = False
        Me.RetroButton4.WindowFrame = System.Drawing.Color.Black
        '
        'RetroButton3
        '
        Me.RetroButton3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RetroButton3.AppearsAsPressed = True
        Me.RetroButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton3.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroButton3.ButtonHilight = System.Drawing.Color.White
        Me.RetroButton3.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton3.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroButton3.FocusRectHeight = 1
        Me.RetroButton3.FocusRectWidth = 1
        Me.RetroButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RetroButton3.ForeColor = System.Drawing.Color.Black
        Me.RetroButton3.HatchBrush = False
        Me.RetroButton3.Image = Nothing
        Me.RetroButton3.Location = New System.Drawing.Point(63, 4)
        Me.RetroButton3.Name = "RetroButton3"
        Me.RetroButton3.Size = New System.Drawing.Size(48, 38)
        Me.RetroButton3.TabIndex = 1
        Me.RetroButton3.UseItAsScrollbar = False
        Me.RetroButton3.UseVisualStyleBackColor = False
        Me.RetroButton3.WindowFrame = System.Drawing.Color.Black
        '
        'RetroButton2
        '
        Me.RetroButton2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RetroButton2.AppearsAsPressed = False
        Me.RetroButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton2.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroButton2.ButtonHilight = System.Drawing.Color.White
        Me.RetroButton2.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton2.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroButton2.FocusRectHeight = 1
        Me.RetroButton2.FocusRectWidth = 1
        Me.RetroButton2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RetroButton2.ForeColor = System.Drawing.Color.Black
        Me.RetroButton2.HatchBrush = False
        Me.RetroButton2.Image = Nothing
        Me.RetroButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RetroButton2.Location = New System.Drawing.Point(2, 4)
        Me.RetroButton2.Name = "RetroButton2"
        Me.RetroButton2.Size = New System.Drawing.Size(52, 38)
        Me.RetroButton2.TabIndex = 0
        Me.RetroButton2.Text = "Start"
        Me.RetroButton2.UseItAsScrollbar = False
        Me.RetroButton2.UseVisualStyleBackColor = False
        Me.RetroButton2.WindowFrame = System.Drawing.Color.Black
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.ClassicColorsPreview)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(520, 269)
        Me.TabPage4.TabIndex = 2
        Me.TabPage4.Text = "2"
        '
        'ClassicColorsPreview
        '
        Me.ClassicColorsPreview.BackColor = System.Drawing.Color.Teal
        Me.ClassicColorsPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClassicColorsPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ClassicColorsPreview.Controls.Add(Me.RetroShadow1)
        Me.ClassicColorsPreview.Controls.Add(Me.Menu_Window)
        Me.ClassicColorsPreview.Controls.Add(Me.RetroWindow3)
        Me.ClassicColorsPreview.Controls.Add(Me.RetroLabel13)
        Me.ClassicColorsPreview.Controls.Add(Me.RetroWindow2)
        Me.ClassicColorsPreview.Controls.Add(Me.RetroWindow1)
        Me.ClassicColorsPreview.Controls.Add(Me.RetroWindow4)
        Me.ClassicColorsPreview.Location = New System.Drawing.Point(0, 0)
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
        Me.Menu_Window.TitlebarText = "New Window"
        Me.Menu_Window.UseItAsMenu = True
        '
        'menucontainer3
        '
        Me.menucontainer3.BackColor = System.Drawing.Color.Transparent
        Me.menucontainer3.Controls.Add(Me.RetroLabel9)
        Me.menucontainer3.Dock = System.Windows.Forms.DockStyle.Top
        Me.menucontainer3.Location = New System.Drawing.Point(3, 43)
        Me.menucontainer3.Name = "menucontainer3"
        Me.menucontainer3.Padding = New System.Windows.Forms.Padding(21, 0, 0, 0)
        Me.menucontainer3.Size = New System.Drawing.Size(107, 20)
        Me.menucontainer3.TabIndex = 12
        '
        'RetroLabel9
        '
        Me.RetroLabel9.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RetroLabel9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel9.ForeColor = System.Drawing.Color.DimGray
        Me.RetroLabel9.Location = New System.Drawing.Point(21, 0)
        Me.RetroLabel9.Name = "RetroLabel9"
        Me.RetroLabel9.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.RetroLabel9.Size = New System.Drawing.Size(86, 20)
        Me.RetroLabel9.TabIndex = 3
        Me.RetroLabel9.Text = "Disabled item"
        Me.RetroLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.menuhilight.Controls.Add(Me.RetroLabel5)
        Me.menuhilight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.menuhilight.Location = New System.Drawing.Point(0, 0)
        Me.menuhilight.Name = "menuhilight"
        Me.menuhilight.Padding = New System.Windows.Forms.Padding(21, 0, 1, 0)
        Me.menuhilight.Size = New System.Drawing.Size(107, 20)
        Me.menuhilight.TabIndex = 11
        '
        'RetroLabel5
        '
        Me.RetroLabel5.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RetroLabel5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel5.ForeColor = System.Drawing.Color.White
        Me.RetroLabel5.Location = New System.Drawing.Point(21, 0)
        Me.RetroLabel5.Name = "RetroLabel5"
        Me.RetroLabel5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.RetroLabel5.Size = New System.Drawing.Size(85, 20)
        Me.RetroLabel5.TabIndex = 3
        Me.RetroLabel5.Text = "Hovered item"
        Me.RetroLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'menucontainer1
        '
        Me.menucontainer1.BackColor = System.Drawing.Color.Transparent
        Me.menucontainer1.Controls.Add(Me.RetroLabel6)
        Me.menucontainer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.menucontainer1.Location = New System.Drawing.Point(3, 3)
        Me.menucontainer1.Name = "menucontainer1"
        Me.menucontainer1.Padding = New System.Windows.Forms.Padding(21, 0, 0, 0)
        Me.menucontainer1.Size = New System.Drawing.Size(107, 20)
        Me.menucontainer1.TabIndex = 6
        '
        'RetroLabel6
        '
        Me.RetroLabel6.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RetroLabel6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel6.ForeColor = System.Drawing.Color.Black
        Me.RetroLabel6.Location = New System.Drawing.Point(21, 0)
        Me.RetroLabel6.Name = "RetroLabel6"
        Me.RetroLabel6.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.RetroLabel6.Size = New System.Drawing.Size(86, 20)
        Me.RetroLabel6.TabIndex = 3
        Me.RetroLabel6.Text = "Menu item"
        Me.RetroLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RetroWindow3
        '
        Me.RetroWindow3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow3.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroWindow3.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow3.ButtonHilight = System.Drawing.Color.White
        Me.RetroWindow3.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow3.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroWindow3.ButtonText = System.Drawing.Color.Black
        Me.RetroWindow3.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroWindow3.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.RetroWindow3.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow3.ColorGradient = True
        Me.RetroWindow3.ControlBox = False
        Me.RetroWindow3.Controls.Add(Me.RetroButton5)
        Me.RetroWindow3.Controls.Add(Me.RetroLabel4)
        Me.RetroWindow3.Flat = False
        Me.RetroWindow3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroWindow3.ForeColor = System.Drawing.Color.White
        Me.RetroWindow3.Location = New System.Drawing.Point(215, 185)
        Me.RetroWindow3.MaximizeBox = True
        Me.RetroWindow3.Metrics_BorderWidth = 0
        Me.RetroWindow3.Metrics_CaptionHeight = 18
        Me.RetroWindow3.Metrics_CaptionWidth = 18
        Me.RetroWindow3.Metrics_PaddedBorderWidth = 0
        Me.RetroWindow3.MinimizeBox = True
        Me.RetroWindow3.Name = "RetroWindow3"
        Me.RetroWindow3.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.RetroWindow3.Size = New System.Drawing.Size(147, 80)
        Me.RetroWindow3.TabIndex = 2
        Me.RetroWindow3.TitlebarText = "Message Box"
        Me.RetroWindow3.UseItAsMenu = False
        '
        'RetroButton5
        '
        Me.RetroButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RetroButton5.AppearsAsPressed = False
        Me.RetroButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton5.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroButton5.ButtonHilight = System.Drawing.Color.White
        Me.RetroButton5.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton5.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroButton5.FocusRectHeight = 1
        Me.RetroButton5.FocusRectWidth = 1
        Me.RetroButton5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RetroButton5.ForeColor = System.Drawing.Color.Black
        Me.RetroButton5.HatchBrush = False
        Me.RetroButton5.Image = Nothing
        Me.RetroButton5.Location = New System.Drawing.Point(37, 49)
        Me.RetroButton5.Name = "RetroButton5"
        Me.RetroButton5.Size = New System.Drawing.Size(75, 23)
        Me.RetroButton5.TabIndex = 2
        Me.RetroButton5.Text = "OK"
        Me.RetroButton5.UseItAsScrollbar = False
        Me.RetroButton5.UseVisualStyleBackColor = False
        Me.RetroButton5.WindowFrame = System.Drawing.Color.Black
        '
        'RetroLabel4
        '
        Me.RetroLabel4.AutoSize = True
        Me.RetroLabel4.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.RetroLabel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel4.ForeColor = System.Drawing.Color.Black
        Me.RetroLabel4.Location = New System.Drawing.Point(4, 22)
        Me.RetroLabel4.Name = "RetroLabel4"
        Me.RetroLabel4.Padding = New System.Windows.Forms.Padding(4)
        Me.RetroLabel4.Size = New System.Drawing.Size(82, 21)
        Me.RetroLabel4.TabIndex = 1
        Me.RetroLabel4.Text = "Message Text"
        Me.RetroLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RetroLabel13
        '
        Me.RetroLabel13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RetroLabel13.AutoSize = True
        Me.RetroLabel13.BackColor = System.Drawing.Color.White
        Me.RetroLabel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RetroLabel13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel13.ForeColor = System.Drawing.Color.Black
        Me.RetroLabel13.Location = New System.Drawing.Point(287, 47)
        Me.RetroLabel13.Name = "RetroLabel13"
        Me.RetroLabel13.Size = New System.Drawing.Size(79, 15)
        Me.RetroLabel13.TabIndex = 5
        Me.RetroLabel13.Text = "This is a tooltip"
        Me.RetroLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RetroWindow2
        '
        Me.RetroWindow2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow2.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroWindow2.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow2.ButtonHilight = System.Drawing.Color.White
        Me.RetroWindow2.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow2.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroWindow2.ButtonText = System.Drawing.Color.Black
        Me.RetroWindow2.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroWindow2.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.RetroWindow2.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow2.ColorGradient = True
        Me.RetroWindow2.ControlBox = True
        Me.RetroWindow2.Controls.Add(Me.RetroTextBox1)
        Me.RetroWindow2.Controls.Add(Me.menucontainer0)
        Me.RetroWindow2.Flat = False
        Me.RetroWindow2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroWindow2.ForeColor = System.Drawing.Color.White
        Me.RetroWindow2.Location = New System.Drawing.Point(195, 110)
        Me.RetroWindow2.MaximizeBox = True
        Me.RetroWindow2.Metrics_BorderWidth = 0
        Me.RetroWindow2.Metrics_CaptionHeight = 18
        Me.RetroWindow2.Metrics_CaptionWidth = 18
        Me.RetroWindow2.Metrics_PaddedBorderWidth = 0
        Me.RetroWindow2.MinimizeBox = True
        Me.RetroWindow2.Name = "RetroWindow2"
        Me.RetroWindow2.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.RetroWindow2.Size = New System.Drawing.Size(196, 120)
        Me.RetroWindow2.TabIndex = 1
        Me.RetroWindow2.TitlebarText = "Active Window"
        Me.RetroWindow2.UseItAsMenu = False
        '
        'RetroTextBox1
        '
        Me.RetroTextBox1.BackColor = System.Drawing.Color.White
        Me.RetroTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RetroTextBox1.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroTextBox1.ButtonHilight = System.Drawing.Color.White
        Me.RetroTextBox1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroTextBox1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RetroTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroTextBox1.ForeColor = System.Drawing.Color.Black
        Me.RetroTextBox1.Location = New System.Drawing.Point(4, 40)
        Me.RetroTextBox1.MaxLength = 32767
        Me.RetroTextBox1.Multiline = True
        Me.RetroTextBox1.Name = "RetroTextBox1"
        Me.RetroTextBox1.ReadOnly = True
        Me.RetroTextBox1.Size = New System.Drawing.Size(188, 76)
        Me.RetroTextBox1.Style = WinPaletter.RetroTextBox.RoundingStyle.Normal
        Me.RetroTextBox1.TabIndex = 3
        Me.RetroTextBox1.Text = "Window Text"
        Me.RetroTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.RetroTextBox1.UseSystemPasswordChar = False
        '
        'menucontainer0
        '
        Me.menucontainer0.BackColor = System.Drawing.Color.Silver
        Me.menucontainer0.Controls.Add(Me.RetroPanel1)
        Me.menucontainer0.Controls.Add(Me.RetroLabel2)
        Me.menucontainer0.Controls.Add(Me.RetroLabel1)
        Me.menucontainer0.Dock = System.Windows.Forms.DockStyle.Top
        Me.menucontainer0.Location = New System.Drawing.Point(4, 22)
        Me.menucontainer0.Name = "menucontainer0"
        Me.menucontainer0.Size = New System.Drawing.Size(188, 18)
        Me.menucontainer0.TabIndex = 5
        '
        'RetroPanel1
        '
        Me.RetroPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroPanel1.ButtonDkShadow = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.RetroPanel1.ButtonHilight = System.Drawing.Color.White
        Me.RetroPanel1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.RetroPanel1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroPanel1.Controls.Add(Me.RetroLabel3)
        Me.RetroPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.RetroPanel1.Flat = False
        Me.RetroPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroPanel1.ForeColor = System.Drawing.Color.Black
        Me.RetroPanel1.Location = New System.Drawing.Point(88, 0)
        Me.RetroPanel1.Name = "RetroPanel1"
        Me.RetroPanel1.Padding = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.RetroPanel1.Size = New System.Drawing.Size(53, 18)
        Me.RetroPanel1.Style2 = False
        Me.RetroPanel1.TabIndex = 2
        '
        'RetroLabel3
        '
        Me.RetroLabel3.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RetroLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel3.ForeColor = System.Drawing.Color.Black
        Me.RetroLabel3.Location = New System.Drawing.Point(1, 3)
        Me.RetroLabel3.Name = "RetroLabel3"
        Me.RetroLabel3.Size = New System.Drawing.Size(51, 12)
        Me.RetroLabel3.TabIndex = 1
        Me.RetroLabel3.Text = "Selected"
        Me.RetroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RetroLabel2
        '
        Me.RetroLabel2.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.RetroLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RetroLabel2.Location = New System.Drawing.Point(40, 0)
        Me.RetroLabel2.Name = "RetroLabel2"
        Me.RetroLabel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.RetroLabel2.Size = New System.Drawing.Size(48, 18)
        Me.RetroLabel2.TabIndex = 1
        Me.RetroLabel2.Text = "Disabled"
        Me.RetroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RetroLabel1
        '
        Me.RetroLabel1.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.RetroLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel1.ForeColor = System.Drawing.Color.Black
        Me.RetroLabel1.Location = New System.Drawing.Point(0, 0)
        Me.RetroLabel1.Name = "RetroLabel1"
        Me.RetroLabel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.RetroLabel1.Size = New System.Drawing.Size(40, 18)
        Me.RetroLabel1.TabIndex = 0
        Me.RetroLabel1.Text = "Normal"
        Me.RetroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RetroWindow1
        '
        Me.RetroWindow1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow1.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroWindow1.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow1.ButtonHilight = System.Drawing.Color.White
        Me.RetroWindow1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroWindow1.ButtonText = System.Drawing.Color.Black
        Me.RetroWindow1.Color1 = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RetroWindow1.Color2 = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.RetroWindow1.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow1.ColorGradient = True
        Me.RetroWindow1.ControlBox = True
        Me.RetroWindow1.Flat = False
        Me.RetroWindow1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroWindow1.ForeColor = System.Drawing.Color.White
        Me.RetroWindow1.Location = New System.Drawing.Point(179, 77)
        Me.RetroWindow1.MaximizeBox = True
        Me.RetroWindow1.Metrics_BorderWidth = 0
        Me.RetroWindow1.Metrics_CaptionHeight = 18
        Me.RetroWindow1.Metrics_CaptionWidth = 18
        Me.RetroWindow1.Metrics_PaddedBorderWidth = 0
        Me.RetroWindow1.MinimizeBox = True
        Me.RetroWindow1.Name = "RetroWindow1"
        Me.RetroWindow1.Padding = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.RetroWindow1.Size = New System.Drawing.Size(180, 112)
        Me.RetroWindow1.TabIndex = 0
        Me.RetroWindow1.Text = "New Window"
        Me.RetroWindow1.TitlebarText = "Inactive Window"
        Me.RetroWindow1.UseItAsMenu = False
        '
        'RetroWindow4
        '
        Me.RetroWindow4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow4.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroWindow4.ButtonFace = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow4.ButtonHilight = System.Drawing.Color.White
        Me.RetroWindow4.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow4.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroWindow4.ButtonText = System.Drawing.Color.Black
        Me.RetroWindow4.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroWindow4.Color2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.RetroWindow4.ColorBorder = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroWindow4.ColorGradient = True
        Me.RetroWindow4.ControlBox = True
        Me.RetroWindow4.Controls.Add(Me.programcontainer)
        Me.RetroWindow4.Flat = False
        Me.RetroWindow4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroWindow4.ForeColor = System.Drawing.Color.White
        Me.RetroWindow4.Location = New System.Drawing.Point(139, 30)
        Me.RetroWindow4.MaximizeBox = False
        Me.RetroWindow4.Metrics_BorderWidth = 0
        Me.RetroWindow4.Metrics_CaptionHeight = 18
        Me.RetroWindow4.Metrics_CaptionWidth = 18
        Me.RetroWindow4.Metrics_PaddedBorderWidth = 0
        Me.RetroWindow4.MinimizeBox = False
        Me.RetroWindow4.Name = "RetroWindow4"
        Me.RetroWindow4.Padding = New System.Windows.Forms.Padding(4, 22, 4, 4)
        Me.RetroWindow4.Size = New System.Drawing.Size(156, 132)
        Me.RetroWindow4.TabIndex = 3
        Me.RetroWindow4.TitlebarText = "Program Container"
        Me.RetroWindow4.UseItAsMenu = False
        '
        'programcontainer
        '
        Me.programcontainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.programcontainer.Controls.Add(Me.RetroPanel2)
        Me.programcontainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.programcontainer.Location = New System.Drawing.Point(4, 22)
        Me.programcontainer.Name = "programcontainer"
        Me.programcontainer.Padding = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.programcontainer.Size = New System.Drawing.Size(148, 106)
        Me.programcontainer.TabIndex = 4
        '
        'RetroPanel2
        '
        Me.RetroPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroPanel2.ButtonHilight = System.Drawing.Color.White
        Me.RetroPanel2.Controls.Add(Me.RetroButton12)
        Me.RetroPanel2.Controls.Add(Me.RetroButton11)
        Me.RetroPanel2.Controls.Add(Me.RetroButton10)
        Me.RetroPanel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.RetroPanel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroPanel2.ForeColor = System.Drawing.Color.Black
        Me.RetroPanel2.Location = New System.Drawing.Point(0, 0)
        Me.RetroPanel2.Name = "RetroPanel2"
        Me.RetroPanel2.Size = New System.Drawing.Size(16, 106)
        Me.RetroPanel2.TabIndex = 0
        '
        'RetroButton12
        '
        Me.RetroButton12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RetroButton12.AppearsAsPressed = False
        Me.RetroButton12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton12.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroButton12.ButtonHilight = System.Drawing.Color.White
        Me.RetroButton12.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton12.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroButton12.FocusRectHeight = 1
        Me.RetroButton12.FocusRectWidth = 1
        Me.RetroButton12.Font = New System.Drawing.Font("Marlett", 6.0!)
        Me.RetroButton12.ForeColor = System.Drawing.Color.Black
        Me.RetroButton12.HatchBrush = False
        Me.RetroButton12.Image = Nothing
        Me.RetroButton12.Location = New System.Drawing.Point(0, 29)
        Me.RetroButton12.Name = "RetroButton12"
        Me.RetroButton12.Size = New System.Drawing.Size(16, 31)
        Me.RetroButton12.TabIndex = 7
        Me.RetroButton12.UseItAsScrollbar = True
        Me.RetroButton12.UseVisualStyleBackColor = False
        Me.RetroButton12.WindowFrame = System.Drawing.Color.Black
        '
        'RetroButton11
        '
        Me.RetroButton11.AppearsAsPressed = False
        Me.RetroButton11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton11.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroButton11.ButtonHilight = System.Drawing.Color.White
        Me.RetroButton11.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton11.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroButton11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RetroButton11.FocusRectHeight = 1
        Me.RetroButton11.FocusRectWidth = 1
        Me.RetroButton11.Font = New System.Drawing.Font("Marlett", 8.7!, System.Drawing.FontStyle.Bold)
        Me.RetroButton11.ForeColor = System.Drawing.Color.Black
        Me.RetroButton11.HatchBrush = False
        Me.RetroButton11.Image = Nothing
        Me.RetroButton11.Location = New System.Drawing.Point(0, 92)
        Me.RetroButton11.Name = "RetroButton11"
        Me.RetroButton11.Size = New System.Drawing.Size(16, 14)
        Me.RetroButton11.TabIndex = 6
        Me.RetroButton11.Text = "u"
        Me.RetroButton11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RetroButton11.UseItAsScrollbar = False
        Me.RetroButton11.UseVisualStyleBackColor = False
        Me.RetroButton11.WindowFrame = System.Drawing.Color.Black
        '
        'RetroButton10
        '
        Me.RetroButton10.AppearsAsPressed = False
        Me.RetroButton10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton10.ButtonDkShadow = System.Drawing.Color.Black
        Me.RetroButton10.ButtonHilight = System.Drawing.Color.White
        Me.RetroButton10.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroButton10.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroButton10.Dock = System.Windows.Forms.DockStyle.Top
        Me.RetroButton10.FocusRectHeight = 1
        Me.RetroButton10.FocusRectWidth = 1
        Me.RetroButton10.Font = New System.Drawing.Font("Marlett", 8.7!, System.Drawing.FontStyle.Bold)
        Me.RetroButton10.ForeColor = System.Drawing.Color.Black
        Me.RetroButton10.HatchBrush = False
        Me.RetroButton10.Image = Nothing
        Me.RetroButton10.Location = New System.Drawing.Point(0, 0)
        Me.RetroButton10.Name = "RetroButton10"
        Me.RetroButton10.Size = New System.Drawing.Size(16, 14)
        Me.RetroButton10.TabIndex = 5
        Me.RetroButton10.Text = "t"
        Me.RetroButton10.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.RetroButton10.UseItAsScrollbar = False
        Me.RetroButton10.UseVisualStyleBackColor = False
        Me.RetroButton10.WindowFrame = System.Drawing.Color.Black
        '
        'TabPage8
        '
        Me.TabPage8.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage8.Controls.Add(Me.XenonCMD1)
        Me.TabPage8.Location = New System.Drawing.Point(4, 24)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(520, 269)
        Me.TabPage8.TabIndex = 3
        Me.TabPage8.Text = "3"
        '
        'XenonCMD1
        '
        Me.XenonCMD1.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_PopupBackground = 5
        Me.XenonCMD1.CMD_PopupForeground = 15
        Me.XenonCMD1.CMD_ScreenColorsBackground = 0
        Me.XenonCMD1.CMD_ScreenColorsForeground = 7
        Me.XenonCMD1.CustomTerminal = False
        Me.XenonCMD1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XenonCMD1.Location = New System.Drawing.Point(0, 0)
        Me.XenonCMD1.Name = "XenonCMD1"
        Me.XenonCMD1.PowerShell = False
        Me.XenonCMD1.Raster = True
        Me.XenonCMD1.RasterSize = WinPaletter.XenonCMD.Raster_Sizes._8x12
        Me.XenonCMD1.Size = New System.Drawing.Size(520, 269)
        Me.XenonCMD1.TabIndex = 1
        '
        'TabPage9
        '
        Me.TabPage9.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage9.Controls.Add(Me.XenonCMD2)
        Me.TabPage9.Location = New System.Drawing.Point(4, 24)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Size = New System.Drawing.Size(520, 269)
        Me.TabPage9.TabIndex = 4
        Me.TabPage9.Text = "4"
        '
        'XenonCMD2
        '
        Me.XenonCMD2.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_PopupBackground = 5
        Me.XenonCMD2.CMD_PopupForeground = 15
        Me.XenonCMD2.CMD_ScreenColorsBackground = 0
        Me.XenonCMD2.CMD_ScreenColorsForeground = 7
        Me.XenonCMD2.CustomTerminal = False
        Me.XenonCMD2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XenonCMD2.Location = New System.Drawing.Point(0, 0)
        Me.XenonCMD2.Name = "XenonCMD2"
        Me.XenonCMD2.PowerShell = False
        Me.XenonCMD2.Raster = True
        Me.XenonCMD2.RasterSize = WinPaletter.XenonCMD.Raster_Sizes._8x12
        Me.XenonCMD2.Size = New System.Drawing.Size(520, 269)
        Me.XenonCMD2.TabIndex = 2
        '
        'TabPage10
        '
        Me.TabPage10.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage10.Controls.Add(Me.XenonCMD3)
        Me.TabPage10.Location = New System.Drawing.Point(4, 24)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Size = New System.Drawing.Size(520, 269)
        Me.TabPage10.TabIndex = 5
        Me.TabPage10.Text = "5"
        '
        'XenonCMD3
        '
        Me.XenonCMD3.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_PopupBackground = 5
        Me.XenonCMD3.CMD_PopupForeground = 15
        Me.XenonCMD3.CMD_ScreenColorsBackground = 0
        Me.XenonCMD3.CMD_ScreenColorsForeground = 7
        Me.XenonCMD3.CustomTerminal = False
        Me.XenonCMD3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XenonCMD3.Location = New System.Drawing.Point(0, 0)
        Me.XenonCMD3.Name = "XenonCMD3"
        Me.XenonCMD3.PowerShell = False
        Me.XenonCMD3.Raster = True
        Me.XenonCMD3.RasterSize = WinPaletter.XenonCMD.Raster_Sizes._8x12
        Me.XenonCMD3.Size = New System.Drawing.Size(520, 269)
        Me.XenonCMD3.TabIndex = 2
        '
        'TabPage11
        '
        Me.TabPage11.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage11.Controls.Add(Me.cur_anim_btn)
        Me.TabPage11.Controls.Add(Me.cur_tip_btn)
        Me.TabPage11.Controls.Add(Me.Cursors_Container)
        Me.TabPage11.Controls.Add(Me.PictureBox12)
        Me.TabPage11.Controls.Add(Me.Label17)
        Me.TabPage11.Controls.Add(Me.CursorsSize_Bar)
        Me.TabPage11.Location = New System.Drawing.Point(4, 24)
        Me.TabPage11.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage11.Name = "TabPage11"
        Me.TabPage11.Size = New System.Drawing.Size(520, 269)
        Me.TabPage11.TabIndex = 6
        Me.TabPage11.Text = "6"
        '
        'cur_anim_btn
        '
        Me.cur_anim_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cur_anim_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.cur_anim_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cur_anim_btn.ForeColor = System.Drawing.Color.White
        Me.cur_anim_btn.Image = CType(resources.GetObject("cur_anim_btn.Image"), System.Drawing.Image)
        Me.cur_anim_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cur_anim_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cur_anim_btn.LineSize = 1
        Me.cur_anim_btn.Location = New System.Drawing.Point(353, 254)
        Me.cur_anim_btn.Name = "cur_anim_btn"
        Me.cur_anim_btn.Size = New System.Drawing.Size(141, 21)
        Me.cur_anim_btn.TabIndex = 72
        Me.cur_anim_btn.Text = "Animate (3 Cycles)"
        Me.cur_anim_btn.UseVisualStyleBackColor = False
        '
        'cur_tip_btn
        '
        Me.cur_tip_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cur_tip_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.cur_tip_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cur_tip_btn.ForeColor = System.Drawing.Color.White
        Me.cur_tip_btn.Image = Nothing
        Me.cur_tip_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.cur_tip_btn.LineSize = 1
        Me.cur_tip_btn.Location = New System.Drawing.Point(497, 254)
        Me.cur_tip_btn.Name = "cur_tip_btn"
        Me.cur_tip_btn.Size = New System.Drawing.Size(20, 21)
        Me.cur_tip_btn.TabIndex = 71
        Me.cur_tip_btn.Text = "?"
        Me.cur_tip_btn.UseVisualStyleBackColor = False
        '
        'Cursors_Container
        '
        Me.Cursors_Container.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.Cursors_Container.Location = New System.Drawing.Point(3, 4)
        Me.Cursors_Container.Name = "Cursors_Container"
        Me.Cursors_Container.Padding = New System.Windows.Forms.Padding(4, 4, 0, 4)
        Me.Cursors_Container.Size = New System.Drawing.Size(514, 234)
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
        'PictureBox12
        '
        Me.PictureBox12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox12.Image = CType(resources.GetObject("PictureBox12.Image"), System.Drawing.Image)
        Me.PictureBox12.Location = New System.Drawing.Point(3, 252)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox12.TabIndex = 70
        Me.PictureBox12.TabStop = False
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(33, 252)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 24)
        Me.Label17.TabIndex = 69
        Me.Label17.Text = "Scaling (1x)"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CursorsSize_Bar
        '
        Me.CursorsSize_Bar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CursorsSize_Bar.LargeChange = 50
        Me.CursorsSize_Bar.Location = New System.Drawing.Point(124, 255)
        Me.CursorsSize_Bar.Maximum = 320
        Me.CursorsSize_Bar.Minimum = 100
        Me.CursorsSize_Bar.Name = "CursorsSize_Bar"
        Me.CursorsSize_Bar.Size = New System.Drawing.Size(223, 19)
        Me.CursorsSize_Bar.SmallChange = 20
        Me.CursorsSize_Bar.TabIndex = 68
        Me.CursorsSize_Bar.Value = 100
        '
        'desc_txt
        '
        Me.desc_txt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.desc_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.desc_txt.ForeColor = System.Drawing.Color.White
        Me.desc_txt.Location = New System.Drawing.Point(4, 443)
        Me.desc_txt.MaxLength = 32767
        Me.desc_txt.Multiline = True
        Me.desc_txt.Name = "desc_txt"
        Me.desc_txt.ReadOnly = True
        Me.desc_txt.Scrollbars = System.Windows.Forms.ScrollBars.Vertical
        Me.desc_txt.SelectedText = ""
        Me.desc_txt.SelectionLength = 0
        Me.desc_txt.SelectionStart = 0
        Me.desc_txt.Size = New System.Drawing.Size(528, 86)
        Me.desc_txt.TabIndex = 7
        Me.desc_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.desc_txt.UseSystemPasswordChar = False
        Me.desc_txt.WordWrap = True
        '
        'ShowClassic_btn
        '
        Me.ShowClassic_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowClassic_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ShowClassic_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ShowClassic_btn.ForeColor = System.Drawing.Color.White
        Me.ShowClassic_btn.Image = Nothing
        Me.ShowClassic_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ShowClassic_btn.LineSize = 1
        Me.ShowClassic_btn.Location = New System.Drawing.Point(248, 345)
        Me.ShowClassic_btn.Name = "ShowClassic_btn"
        Me.ShowClassic_btn.Size = New System.Drawing.Size(139, 26)
        Me.ShowClassic_btn.TabIndex = 135
        Me.ShowClassic_btn.Text = "Classic Colors elements"
        Me.ShowClassic_btn.UseVisualStyleBackColor = False
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
        'ShowCMD_btn
        '
        Me.ShowCMD_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowCMD_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ShowCMD_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ShowCMD_btn.ForeColor = System.Drawing.Color.White
        Me.ShowCMD_btn.Image = Nothing
        Me.ShowCMD_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ShowCMD_btn.LineSize = 1
        Me.ShowCMD_btn.Location = New System.Drawing.Point(103, 377)
        Me.ShowCMD_btn.Name = "ShowCMD_btn"
        Me.ShowCMD_btn.Size = New System.Drawing.Size(139, 26)
        Me.ShowCMD_btn.TabIndex = 3
        Me.ShowCMD_btn.Text = "Command Prompt"
        Me.ShowCMD_btn.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(34, 416)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(97, 24)
        Me.Label24.TabIndex = 4
        Me.Label24.Text = "Description:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(45, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(270, 31)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "Preview"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Switch_M_C_btn
        '
        Me.Switch_M_C_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Switch_M_C_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Switch_M_C_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Switch_M_C_btn.ForeColor = System.Drawing.Color.White
        Me.Switch_M_C_btn.Image = Nothing
        Me.Switch_M_C_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Switch_M_C_btn.LineSize = 1
        Me.Switch_M_C_btn.Location = New System.Drawing.Point(103, 345)
        Me.Switch_M_C_btn.Name = "Switch_M_C_btn"
        Me.Switch_M_C_btn.Size = New System.Drawing.Size(139, 26)
        Me.Switch_M_C_btn.TabIndex = 6
        Me.Switch_M_C_btn.Text = "Switch modern\classic"
        Me.Switch_M_C_btn.UseVisualStyleBackColor = False
        '
        'PictureBox13
        '
        Me.PictureBox13.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox13.Image = CType(resources.GetObject("PictureBox13.Image"), System.Drawing.Image)
        Me.PictureBox13.Location = New System.Drawing.Point(4, 416)
        Me.PictureBox13.Name = "PictureBox13"
        Me.PictureBox13.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox13.TabIndex = 0
        Me.PictureBox13.TabStop = False
        '
        'XenonGroupBox2
        '
        Me.XenonGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox2.Controls.Add(Me.Download_Link)
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox5)
        Me.XenonGroupBox2.Controls.Add(Me.Author_link)
        Me.XenonGroupBox2.Controls.Add(Me.Label5)
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox7)
        Me.XenonGroupBox2.Controls.Add(Me.Label15)
        Me.XenonGroupBox2.Location = New System.Drawing.Point(13, 169)
        Me.XenonGroupBox2.Name = "XenonGroupBox2"
        Me.XenonGroupBox2.Size = New System.Drawing.Size(681, 60)
        Me.XenonGroupBox2.TabIndex = 13
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.TabIndex = 3
        Me.PictureBox5.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(33, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 24)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Author's link:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox7
        '
        Me.PictureBox7.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(3, 33)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.TabIndex = 2
        Me.PictureBox7.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(33, 33)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(97, 24)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Download URL:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonGroupBox1
        '
        Me.XenonGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox1.Controls.Add(Me.themeSize_lbl)
        Me.XenonGroupBox1.Controls.Add(Me.author_lbl)
        Me.XenonGroupBox1.Controls.Add(Me.theme_ver_lbl)
        Me.XenonGroupBox1.Controls.Add(Me.Label14)
        Me.XenonGroupBox1.Controls.Add(Me.MD5_lbl)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox8)
        Me.XenonGroupBox1.Controls.Add(Me.theme_name_lbl)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox9)
        Me.XenonGroupBox1.Controls.Add(Me.Label4)
        Me.XenonGroupBox1.Controls.Add(Me.Label3)
        Me.XenonGroupBox1.Controls.Add(Me.Label2)
        Me.XenonGroupBox1.Controls.Add(Me.Label16)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox4)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox3)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox2)
        Me.XenonGroupBox1.Location = New System.Drawing.Point(13, 13)
        Me.XenonGroupBox1.Name = "XenonGroupBox1"
        Me.XenonGroupBox1.Size = New System.Drawing.Size(681, 150)
        Me.XenonGroupBox1.TabIndex = 0
        '
        'themeSize_lbl
        '
        Me.themeSize_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.themeSize_lbl.BackColor = System.Drawing.Color.Transparent
        Me.themeSize_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.themeSize_lbl.Location = New System.Drawing.Point(136, 123)
        Me.themeSize_lbl.Name = "themeSize_lbl"
        Me.themeSize_lbl.Size = New System.Drawing.Size(542, 24)
        Me.themeSize_lbl.TabIndex = 13
        Me.themeSize_lbl.Text = "0"
        Me.themeSize_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'author_lbl
        '
        Me.author_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.author_lbl.BackColor = System.Drawing.Color.Transparent
        Me.author_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.author_lbl.Location = New System.Drawing.Point(136, 63)
        Me.author_lbl.Name = "author_lbl"
        Me.author_lbl.Size = New System.Drawing.Size(542, 24)
        Me.author_lbl.TabIndex = 10
        Me.author_lbl.Text = "0"
        Me.author_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'theme_ver_lbl
        '
        Me.theme_ver_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.theme_ver_lbl.BackColor = System.Drawing.Color.Transparent
        Me.theme_ver_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.theme_ver_lbl.Location = New System.Drawing.Point(136, 33)
        Me.theme_ver_lbl.Name = "theme_ver_lbl"
        Me.theme_ver_lbl.Size = New System.Drawing.Size(542, 24)
        Me.theme_ver_lbl.TabIndex = 9
        Me.theme_ver_lbl.Text = "0"
        Me.theme_ver_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(33, 123)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(97, 24)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Theme size:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MD5_lbl
        '
        Me.MD5_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MD5_lbl.BackColor = System.Drawing.Color.Transparent
        Me.MD5_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MD5_lbl.Location = New System.Drawing.Point(136, 93)
        Me.MD5_lbl.Name = "MD5_lbl"
        Me.MD5_lbl.Size = New System.Drawing.Size(542, 24)
        Me.MD5_lbl.TabIndex = 8
        Me.MD5_lbl.Text = "0"
        Me.MD5_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox8
        '
        Me.PictureBox8.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(3, 123)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox8.TabIndex = 1
        Me.PictureBox8.TabStop = False
        '
        'theme_name_lbl
        '
        Me.theme_name_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.theme_name_lbl.BackColor = System.Drawing.Color.Transparent
        Me.theme_name_lbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.theme_name_lbl.Location = New System.Drawing.Point(136, 3)
        Me.theme_name_lbl.Name = "theme_name_lbl"
        Me.theme_name_lbl.Size = New System.Drawing.Size(542, 24)
        Me.theme_name_lbl.TabIndex = 8
        Me.theme_name_lbl.Text = "0"
        Me.theme_name_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox9
        '
        Me.PictureBox9.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(3, 93)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox9.TabIndex = 0
        Me.PictureBox9.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(33, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 24)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Author:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(33, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 24)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Version:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 24)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Theme name:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(33, 93)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(97, 24)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "MD5:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(3, 33)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.TabIndex = 2
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(3, 63)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.TabIndex = 1
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage5.Controls.Add(Me.search_results)
        Me.TabPage5.Location = New System.Drawing.Point(4, 24)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage5.Size = New System.Drawing.Size(1246, 599)
        Me.TabPage5.TabIndex = 3
        Me.TabPage5.Text = "2"
        '
        'search_results
        '
        Me.search_results.AutoScroll = True
        Me.search_results.Dock = System.Windows.Forms.DockStyle.Fill
        Me.search_results.Location = New System.Drawing.Point(10, 10)
        Me.search_results.Name = "search_results"
        Me.search_results.Size = New System.Drawing.Size(1226, 579)
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
        Me.TabPage2.Controls.Add(Me.XenonSeparator1)
        Me.TabPage2.Controls.Add(Me.log_header)
        Me.TabPage2.Controls.Add(Me.PictureBox36)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPage2.Size = New System.Drawing.Size(1246, 599)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "3"
        '
        'StopTimer_btn
        '
        Me.StopTimer_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StopTimer_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.StopTimer_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.StopTimer_btn.ForeColor = System.Drawing.Color.White
        Me.StopTimer_btn.Image = Nothing
        Me.StopTimer_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.StopTimer_btn.LineSize = 1
        Me.StopTimer_btn.Location = New System.Drawing.Point(1005, 555)
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
        Me.ExportDetails_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ExportDetails_btn.ForeColor = System.Drawing.Color.White
        Me.ExportDetails_btn.Image = Nothing
        Me.ExportDetails_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ExportDetails_btn.LineSize = 1
        Me.ExportDetails_btn.Location = New System.Drawing.Point(1087, 555)
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
        Me.log_lbl.Location = New System.Drawing.Point(13, 555)
        Me.log_lbl.Name = "log_lbl"
        Me.log_lbl.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.log_lbl.Size = New System.Drawing.Size(902, 34)
        Me.log_lbl.TabIndex = 29
        Me.log_lbl.Text = "Error\s happened. Press on 'Show errors' for details"
        Me.log_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.log_lbl.Visible = False
        '
        'ShowErrors_btn
        '
        Me.ShowErrors_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowErrors_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ShowErrors_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ShowErrors_btn.ForeColor = System.Drawing.Color.White
        Me.ShowErrors_btn.Image = Nothing
        Me.ShowErrors_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ShowErrors_btn.LineSize = 1
        Me.ShowErrors_btn.Location = New System.Drawing.Point(921, 555)
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
        Me.ok_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ok_btn.ForeColor = System.Drawing.Color.White
        Me.ok_btn.Image = Nothing
        Me.ok_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ok_btn.LineSize = 1
        Me.ok_btn.Location = New System.Drawing.Point(1174, 555)
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
        Me.log.Size = New System.Drawing.Size(1220, 488)
        Me.log.TabIndex = 26
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.AlternativeLook = False
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(13, 54)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(1220, 1)
        Me.XenonSeparator1.TabIndex = 25
        Me.XenonSeparator1.TabStop = False
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
        'Status_pnl
        '
        Me.Status_pnl.BackColor = System.Drawing.Color.Transparent
        Me.Status_pnl.Controls.Add(Me.Status_lbl)
        Me.Status_pnl.Controls.Add(Me.ProgressBar1)
        Me.Status_pnl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Status_pnl.Location = New System.Drawing.Point(0, 697)
        Me.Status_pnl.Name = "Status_pnl"
        Me.Status_pnl.Padding = New System.Windows.Forms.Padding(3)
        Me.Status_pnl.Size = New System.Drawing.Size(1254, 24)
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
        Me.Status_lbl.Size = New System.Drawing.Size(1020, 18)
        Me.Status_lbl.TabIndex = 39
        Me.Status_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Author_link
        '
        Me.Author_link.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Author_link.BackColor = System.Drawing.Color.Transparent
        Me.Author_link.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Author_link.Location = New System.Drawing.Point(136, 3)
        Me.Author_link.Name = "Author_link"
        Me.Author_link.Size = New System.Drawing.Size(542, 24)
        Me.Author_link.TabIndex = 139
        Me.Author_link.Text = "0"
        Me.Author_link.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Download_Link
        '
        Me.Download_Link.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download_Link.BackColor = System.Drawing.Color.Transparent
        Me.Download_Link.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Download_Link.Location = New System.Drawing.Point(136, 33)
        Me.Download_Link.Name = "Download_Link"
        Me.Download_Link.Size = New System.Drawing.Size(542, 24)
        Me.Download_Link.TabIndex = 140
        Me.Download_Link.Text = "0"
        Me.Download_Link.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Store
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1254, 721)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.Titlebar_panel)
        Me.Controls.Add(Me.Status_pnl)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.MinimumSize = New System.Drawing.Size(1270, 760)
        Me.Name = "Store"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WinPaletter Store (Beta)"
        Me.Titlebar_panel.ResumeLayout(False)
        Me.search_panel.ResumeLayout(False)
        CType(Me.Titlebar_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.XenonGroupBox3.ResumeLayout(False)
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.previewContainer.ResumeLayout(False)
        Me.tabs_preview.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.pnl_preview.ResumeLayout(False)
        Me.XenonWindow1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.pnl_preview_classic.ResumeLayout(False)
        Me.ClassicTaskbar.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.ClassicColorsPreview.ResumeLayout(False)
        Me.ClassicColorsPreview.PerformLayout()
        CType(Me.RetroShadow1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Menu_Window.ResumeLayout(False)
        Me.menucontainer3.ResumeLayout(False)
        Me.highlight.ResumeLayout(False)
        Me.menuhilight.ResumeLayout(False)
        Me.menucontainer1.ResumeLayout(False)
        Me.RetroWindow3.ResumeLayout(False)
        Me.RetroWindow3.PerformLayout()
        Me.RetroWindow2.ResumeLayout(False)
        Me.menucontainer0.ResumeLayout(False)
        Me.RetroPanel1.ResumeLayout(False)
        Me.RetroWindow4.ResumeLayout(False)
        Me.programcontainer.ResumeLayout(False)
        Me.RetroPanel2.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.TabPage9.ResumeLayout(False)
        Me.TabPage10.ResumeLayout(False)
        Me.TabPage11.ResumeLayout(False)
        Me.Cursors_Container.ResumeLayout(False)
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox2.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox1.ResumeLayout(False)
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.PictureBox36, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Status_pnl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tabs As TablessControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents tabs_preview As TablessControl
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents pnl_preview As Panel
    Friend WithEvents WXP_Alert2 As XenonAlertBox
    Friend WithEvents ActionCenter As XenonWinElement
    Friend WithEvents start As XenonWinElement
    Friend WithEvents taskbar As XenonWinElement
    Friend WithEvents XenonWindow2 As XenonWindow
    Friend WithEvents XenonWindow1 As XenonWindow
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents setting_icon_preview As Label
    Friend WithEvents lnk_preview As Label
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents pnl_preview_classic As Panel
    Friend WithEvents ClassicWindow1 As RetroWindow
    Friend WithEvents ClassicWindow2 As RetroWindow
    Friend WithEvents ClassicTaskbar As RetroPanelRaised
    Friend WithEvents RetroButton4 As RetroButton
    Friend WithEvents RetroButton3 As RetroButton
    Friend WithEvents RetroButton2 As RetroButton
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents store_container As FlowLayoutPanel
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents back_btn As XenonButton
    Friend WithEvents FilesFetcher As System.ComponentModel.BackgroundWorker
    Friend WithEvents Titlebar_panel As Panel
    Friend WithEvents Titlebar_lbl As Label
    Friend WithEvents Titlebar_img As PictureBox
    Friend WithEvents XenonGroupBox1 As XenonGroupBox
    Friend WithEvents themeSize_lbl As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents author_lbl As Label
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents theme_ver_lbl As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents MD5_lbl As Label
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents theme_name_lbl As Label
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents ShowCMD_btn As XenonButton
    Friend WithEvents Switch_M_C_btn As XenonButton
    Friend WithEvents Label24 As Label
    Friend WithEvents PictureBox13 As PictureBox
    Friend WithEvents XenonGroupBox2 As XenonGroupBox
    Friend WithEvents previewContainer As XenonGroupBox
    Friend WithEvents desc_txt As XenonTextBox
    Friend WithEvents PictureBox41 As PictureBox
    Friend WithEvents Label19 As Label
    Friend WithEvents XenonGroupBox3 As XenonGroupBox
    Friend WithEvents SupportedOS_lbl As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents PictureBox14 As PictureBox
    Friend WithEvents Apply_btn As XenonButton
    Friend WithEvents ClassicColorsPreview As Panel
    Friend WithEvents RetroShadow1 As TransparentPictureBox
    Friend WithEvents Menu_Window As RetroWindow
    Friend WithEvents menucontainer3 As Panel
    Friend WithEvents RetroLabel9 As RetroLabel
    Friend WithEvents highlight As Panel
    Friend WithEvents menuhilight As Panel
    Friend WithEvents RetroLabel5 As RetroLabel
    Friend WithEvents menucontainer1 As Panel
    Friend WithEvents RetroLabel6 As RetroLabel
    Friend WithEvents RetroWindow3 As RetroWindow
    Friend WithEvents RetroButton5 As RetroButton
    Friend WithEvents RetroLabel4 As RetroLabel
    Friend WithEvents RetroLabel13 As RetroLabel
    Friend WithEvents RetroWindow2 As RetroWindow
    Friend WithEvents RetroTextBox1 As RetroTextBox
    Friend WithEvents menucontainer0 As Panel
    Friend WithEvents RetroPanel1 As RetroPanel
    Friend WithEvents RetroLabel3 As RetroLabel
    Friend WithEvents RetroLabel2 As RetroLabel
    Friend WithEvents RetroLabel1 As RetroLabel
    Friend WithEvents RetroWindow1 As RetroWindow
    Friend WithEvents RetroWindow4 As RetroWindow
    Friend WithEvents programcontainer As Panel
    Friend WithEvents RetroPanel2 As RetroScrollBar
    Friend WithEvents RetroButton12 As RetroButton
    Friend WithEvents RetroButton11 As RetroButton
    Friend WithEvents RetroButton10 As RetroButton
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents ShowClassic_btn As XenonButton
    Friend WithEvents StopTimer_btn As XenonButton
    Friend WithEvents ExportDetails_btn As XenonButton
    Friend WithEvents log_lbl As Label
    Friend WithEvents ShowErrors_btn As XenonButton
    Friend WithEvents ok_btn As XenonButton
    Friend WithEvents log As TreeView
    Friend WithEvents XenonSeparator1 As XenonSeparator
    Friend WithEvents log_header As Label
    Friend WithEvents PictureBox36 As PictureBox
    Friend WithEvents Log_Timer As Timer
    Friend WithEvents Edit_btn As XenonButton
    Friend WithEvents ShowCursors_btn As XenonButton
    Friend WithEvents RestartExplorer As XenonButton
    Friend WithEvents search_box As XenonTextBox
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents search_results As FlowLayoutPanel
    Friend WithEvents search_btn As XenonButton
    Friend WithEvents search_filter_btn As XenonButton
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents TabPage9 As TabPage
    Friend WithEvents TabPage10 As TabPage
    Friend WithEvents XenonCMD1 As XenonCMD
    Friend WithEvents XenonCMD2 As XenonCMD
    Friend WithEvents XenonCMD3 As XenonCMD
    Friend WithEvents ShowPS64_btn As XenonButton
    Friend WithEvents ShowPS86_btn As XenonButton
    Friend WithEvents TabPage11 As TabPage
    Friend WithEvents cur_anim_btn As XenonButton
    Friend WithEvents cur_tip_btn As XenonButton
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
    Friend WithEvents CursorsSize_Bar As XenonTrackbar
    Friend WithEvents Cursor_Timer As Timer
    Friend WithEvents search_panel As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Status_pnl As Panel
    Friend WithEvents Status_lbl As Label
    Friend WithEvents Download_Link As Label
    Friend WithEvents Author_link As Label
End Class
