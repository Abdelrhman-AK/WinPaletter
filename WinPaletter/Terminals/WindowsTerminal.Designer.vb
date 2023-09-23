<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WindowsTerminal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WindowsTerminal))
        Me.ImgDlg = New System.Windows.Forms.OpenFileDialog()
        Me.SaveJSONDlg = New System.Windows.Forms.SaveFileDialog()
        Me.OpenWPTHDlg = New System.Windows.Forms.OpenFileDialog()
        Me.OpenJSONDlg = New System.Windows.Forms.OpenFileDialog()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.Button11 = New WinPaletter.UI.WP.Button()
        Me.Button9 = New WinPaletter.UI.WP.Button()
        Me.Button8 = New WinPaletter.UI.WP.Button()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.TerEnabled = New UI.WP.Toggle()
        Me.Button21 = New WinPaletter.UI.WP.Button()
        Me.Button19 = New WinPaletter.UI.WP.Button()
        Me.TerEditThemeName = New WinPaletter.UI.WP.Button()
        Me.Button3 = New WinPaletter.UI.WP.Button()
        Me.TerThemes = New WinPaletter.UI.WP.ComboBox()
        Me.TerThemesContainer = New System.Windows.Forms.Panel()
        Me.TerMode = New UI.WP.Toggle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.TerTitlebarActive = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerTitlebarInactive = New WinPaletter.UI.Controllers.ColorItem()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TerTabActive = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerTabInactive = New WinPaletter.UI.Controllers.ColorItem()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.GroupBox22 = New WinPaletter.UI.WP.GroupBox()
        Me.Terminal1 = New WinPaletter.UI.Simulation.WinTerminal()
        Me.Button15 = New WinPaletter.UI.WP.Button()
        Me.PictureBox27 = New System.Windows.Forms.PictureBox()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Terminal2 = New WinPaletter.UI.Simulation.WinTerminal()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.TerOpacityBar = New WinPaletter.UI.WP.Trackbar()
        Me.PictureBox40 = New System.Windows.Forms.PictureBox()
        Me.Button5 = New WinPaletter.UI.WP.Button()
        Me.TerAcrylic = New UI.WP.CheckBox()
        Me.Button16 = New WinPaletter.UI.WP.Button()
        Me.TerBackImage = New WinPaletter.UI.WP.TextBox()
        Me.TerImageOpacity = New WinPaletter.UI.WP.Trackbar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.Button20 = New WinPaletter.UI.WP.Button()
        Me.Button17 = New WinPaletter.UI.WP.Button()
        Me.Button4 = New WinPaletter.UI.WP.Button()
        Me.Button12 = New WinPaletter.UI.WP.Button()
        Me.TerSchemes = New WinPaletter.UI.WP.ComboBox()
        Me.TerCursor = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerWhiteB = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.TerBlue = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.TerSelection = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.TerWhite = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerForeground = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerCyanB = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.TerCyan = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerGreen = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.TerBackground = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerYellow = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerGreenB = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.TerBlack = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerYellowB = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.TerBlackB = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerPurple = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.TerPurpleB = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerBlueB = New WinPaletter.UI.Controllers.ColorItem()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.TerRedB = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerRed = New WinPaletter.UI.Controllers.ColorItem()
        Me.TerCursorHeightBar = New WinPaletter.UI.WP.Trackbar()
        Me.TerCursorStyle = New WinPaletter.UI.WP.ComboBox()
        Me.Button10 = New WinPaletter.UI.WP.Button()
        Me.GroupBox13 = New WinPaletter.UI.WP.GroupBox()
        Me.Button6 = New WinPaletter.UI.WP.Button()
        Me.Button18 = New WinPaletter.UI.WP.Button()
        Me.Button14 = New WinPaletter.UI.WP.Button()
        Me.PictureBox25 = New System.Windows.Forms.PictureBox()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.TerProfiles = New WinPaletter.UI.WP.ComboBox()
        Me.Button13 = New WinPaletter.UI.WP.Button()
        Me.TerFontSizeBar = New WinPaletter.UI.WP.Trackbar()
        Me.TerFontWeight = New WinPaletter.UI.WP.ComboBox()
        Me.GroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.Button22 = New WinPaletter.UI.WP.Button()
        Me.Separator2 = New WinPaletter.UI.WP.SeparatorH()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New WinPaletter.UI.WP.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox29 = New System.Windows.Forms.PictureBox()
        Me.PictureBox30 = New System.Windows.Forms.PictureBox()
        Me.PictureBox31 = New System.Windows.Forms.PictureBox()
        Me.PictureBox34 = New System.Windows.Forms.PictureBox()
        Me.PictureBox26 = New System.Windows.Forms.PictureBox()
        Me.PictureBox28 = New System.Windows.Forms.PictureBox()
        Me.PictureBox23 = New System.Windows.Forms.PictureBox()
        Me.PictureBox22 = New System.Windows.Forms.PictureBox()
        Me.Separator3 = New WinPaletter.UI.WP.SeparatorH()
        Me.PictureBox21 = New System.Windows.Forms.PictureBox()
        Me.PictureBox20 = New System.Windows.Forms.PictureBox()
        Me.PictureBox19 = New System.Windows.Forms.PictureBox()
        Me.PictureBox15 = New System.Windows.Forms.PictureBox()
        Me.PictureBox14 = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox37 = New System.Windows.Forms.PictureBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New WinPaletter.UI.WP.GroupBox()
        Me.TerFontName = New System.Windows.Forms.Label()
        Me.Button23 = New WinPaletter.UI.WP.Button()
        Me.TerFontSizeVal = New WinPaletter.UI.WP.Button()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox34 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.TerCursorHeightVal = New WinPaletter.UI.WP.Button()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.GroupBox12 = New WinPaletter.UI.WP.GroupBox()
        Me.TerOpacityVal = New WinPaletter.UI.WP.Button()
        Me.TerImageOpacityVal = New WinPaletter.UI.WP.Button()
        Me.PictureBox13 = New System.Windows.Forms.PictureBox()
        Me.PictureBox16 = New System.Windows.Forms.PictureBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.TerThemesContainer.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox22.SuspendLayout()
        CType(Me.PictureBox27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox40, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox13.SuspendLayout()
        CType(Me.PictureBox25, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox37, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox34.SuspendLayout()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImgDlg
        '
        Me.ImgDlg.Filter = "Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All Files (*.*)|*.*"
        '
        'SaveJSONDlg
        '
        Me.SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*"
        '
        'OpenWPTHDlg
        '
        Me.OpenWPTHDlg.Filter = "WinPaletter Theme File (*.wpth)|*.wpth|All files (*.*)|*.*"
        '
        'OpenJSONDlg
        '
        Me.OpenJSONDlg.Filter = "JSON File (*.json)|*.json"
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 98)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(984, 1)
        Me.Separator1.TabIndex = 198
        Me.Separator1.TabStop = False
        Me.Separator1.Text = "Separator1"
        '
        'Button11
        '
        Me.Button11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button11.DrawOnGlass = False
        Me.Button11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button11.ForeColor = System.Drawing.Color.White
        Me.Button11.Image = Nothing
        Me.Button11.LineColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.Button11.LineSize = 1
        Me.Button11.Location = New System.Drawing.Point(609, 46)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(210, 30)
        Me.Button11.TabIndex = 200
        Me.Button11.Text = "Open ""Settings.json"" in editor"
        Me.Button11.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button9.DrawOnGlass = False
        Me.Button9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button9.ForeColor = System.Drawing.Color.White
        Me.Button9.Image = Nothing
        Me.Button9.LineColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.Button9.LineSize = 1
        Me.Button9.Location = New System.Drawing.Point(823, 46)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(154, 30)
        Me.Button9.TabIndex = 199
        Me.Button9.Text = "Backup ""Settings.json"""
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button8.DrawOnGlass = False
        Me.Button8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button8.ForeColor = System.Drawing.Color.White
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button8.LineColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.Button8.LineSize = 1
        Me.Button8.Location = New System.Drawing.Point(85, 6)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(136, 29)
        Me.Button8.TabIndex = 110
        Me.Button8.Text = "WinPaletter theme"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = CType(resources.GetObject("AlertBox1.Image"), System.Drawing.Image)
        Me.AlertBox1.Location = New System.Drawing.Point(4, 46)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(600, 30)
        Me.AlertBox1.TabIndex = 198
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "You should create a backup to Terminal settings file ""settings.json"" to avoid und" &
    "esired actions or errors."
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button7.DrawOnGlass = False
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"), System.Drawing.Image)
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button7.LineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.Button7.LineSize = 1
        Me.Button7.Location = New System.Drawing.Point(371, 6)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(85, 29)
        Me.Button7.TabIndex = 109
        Me.Button7.Text = "JSON file"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'TerEnabled
        '
        Me.TerEnabled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerEnabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerEnabled.Checked = False
        Me.TerEnabled.DarkLight_Toggler = False
        Me.TerEnabled.Location = New System.Drawing.Point(934, 10)
        Me.TerEnabled.Name = "TerEnabled"
        Me.TerEnabled.Size = New System.Drawing.Size(40, 20)
        Me.TerEnabled.TabIndex = 85
        '
        'Button21
        '
        Me.Button21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button21.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button21.DrawOnGlass = False
        Me.Button21.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button21.ForeColor = System.Drawing.Color.White
        Me.Button21.Image = CType(resources.GetObject("Button21.Image"), System.Drawing.Image)
        Me.Button21.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button21.LineColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Button21.LineSize = 1
        Me.Button21.Location = New System.Drawing.Point(108, 6)
        Me.Button21.Name = "Button21"
        Me.Button21.Size = New System.Drawing.Size(87, 24)
        Me.Button21.TabIndex = 218
        Me.Button21.Text = "Copycat"
        Me.Button21.UseVisualStyleBackColor = False
        '
        'Button19
        '
        Me.Button19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button19.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button19.DrawOnGlass = False
        Me.Button19.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button19.ForeColor = System.Drawing.Color.White
        Me.Button19.Image = CType(resources.GetObject("Button19.Image"), System.Drawing.Image)
        Me.Button19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button19.LineColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(145, Byte), Integer))
        Me.Button19.LineSize = 1
        Me.Button19.Location = New System.Drawing.Point(197, 6)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(76, 24)
        Me.Button19.TabIndex = 216
        Me.Button19.Text = "Clone"
        Me.Button19.UseVisualStyleBackColor = False
        '
        'TerEditThemeName
        '
        Me.TerEditThemeName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerEditThemeName.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerEditThemeName.DrawOnGlass = False
        Me.TerEditThemeName.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerEditThemeName.ForeColor = System.Drawing.Color.White
        Me.TerEditThemeName.Image = CType(resources.GetObject("TerEditThemeName.Image"), System.Drawing.Image)
        Me.TerEditThemeName.LineColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.TerEditThemeName.LineSize = 1
        Me.TerEditThemeName.Location = New System.Drawing.Point(275, 6)
        Me.TerEditThemeName.Name = "TerEditThemeName"
        Me.TerEditThemeName.Size = New System.Drawing.Size(35, 24)
        Me.TerEditThemeName.TabIndex = 212
        Me.TerEditThemeName.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button3.DrawOnGlass = False
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.LineColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button3.LineSize = 1
        Me.Button3.Location = New System.Drawing.Point(312, 6)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(35, 24)
        Me.Button3.TabIndex = 211
        Me.Button3.UseVisualStyleBackColor = False
        '
        'TerThemes
        '
        Me.TerThemes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerThemes.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TerThemes.CustomFont = False
        Me.TerThemes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TerThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TerThemes.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerThemes.ForeColor = System.Drawing.Color.White
        Me.TerThemes.FormattingEnabled = True
        Me.TerThemes.ItemHeight = 20
        Me.TerThemes.Location = New System.Drawing.Point(5, 33)
        Me.TerThemes.Name = "TerThemes"
        Me.TerThemes.Size = New System.Drawing.Size(342, 26)
        Me.TerThemes.TabIndex = 210
        '
        'TerThemesContainer
        '
        Me.TerThemesContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerThemesContainer.Controls.Add(Me.TerMode)
        Me.TerThemesContainer.Controls.Add(Me.Label1)
        Me.TerThemesContainer.Controls.Add(Me.PictureBox7)
        Me.TerThemesContainer.Controls.Add(Me.TerTitlebarActive)
        Me.TerThemesContainer.Controls.Add(Me.TerTitlebarInactive)
        Me.TerThemesContainer.Controls.Add(Me.PictureBox3)
        Me.TerThemesContainer.Controls.Add(Me.Label6)
        Me.TerThemesContainer.Controls.Add(Me.TerTabActive)
        Me.TerThemesContainer.Controls.Add(Me.TerTabInactive)
        Me.TerThemesContainer.Controls.Add(Me.PictureBox4)
        Me.TerThemesContainer.Controls.Add(Me.PictureBox2)
        Me.TerThemesContainer.Controls.Add(Me.PictureBox1)
        Me.TerThemesContainer.Controls.Add(Me.Label5)
        Me.TerThemesContainer.Controls.Add(Me.Label10)
        Me.TerThemesContainer.Controls.Add(Me.Label9)
        Me.TerThemesContainer.Enabled = False
        Me.TerThemesContainer.Location = New System.Drawing.Point(3, 60)
        Me.TerThemesContainer.Name = "TerThemesContainer"
        Me.TerThemesContainer.Size = New System.Drawing.Size(344, 149)
        Me.TerThemesContainer.TabIndex = 213
        '
        'TerMode
        '
        Me.TerMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerMode.Checked = False
        Me.TerMode.DarkLight_Toggler = True
        Me.TerMode.Location = New System.Drawing.Point(302, 123)
        Me.TerMode.Name = "TerMode"
        Me.TerMode.Size = New System.Drawing.Size(40, 20)
        Me.TerMode.TabIndex = 209
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(253, 24)
        Me.Label1.TabIndex = 224
        Me.Label1.Text = "Dark\Light mode"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(3, 123)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 223
        Me.PictureBox7.TabStop = False
        '
        'TerTitlebarActive
        '
        Me.TerTitlebarActive.AllowDrop = True
        Me.TerTitlebarActive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerTitlebarActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTitlebarActive.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTitlebarActive.DontShowInfo = False
        Me.TerTitlebarActive.Location = New System.Drawing.Point(242, 3)
        Me.TerTitlebarActive.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerTitlebarActive.Name = "TerTitlebarActive"
        Me.TerTitlebarActive.Size = New System.Drawing.Size(100, 24)
        Me.TerTitlebarActive.TabIndex = 198
        '
        'TerTitlebarInactive
        '
        Me.TerTitlebarInactive.AllowDrop = True
        Me.TerTitlebarInactive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerTitlebarInactive.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTitlebarInactive.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTitlebarInactive.DontShowInfo = False
        Me.TerTitlebarInactive.Location = New System.Drawing.Point(242, 33)
        Me.TerTitlebarInactive.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerTitlebarInactive.Name = "TerTitlebarInactive"
        Me.TerTitlebarInactive.Size = New System.Drawing.Size(100, 24)
        Me.TerTitlebarInactive.TabIndex = 200
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(3, 93)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 222
        Me.PictureBox3.TabStop = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(33, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(202, 24)
        Me.Label6.TabIndex = 197
        Me.Label6.Text = "Active title"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerTabActive
        '
        Me.TerTabActive.AllowDrop = True
        Me.TerTabActive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerTabActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTabActive.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTabActive.DontShowInfo = False
        Me.TerTabActive.Location = New System.Drawing.Point(242, 63)
        Me.TerTabActive.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerTabActive.Name = "TerTabActive"
        Me.TerTabActive.Size = New System.Drawing.Size(100, 24)
        Me.TerTabActive.TabIndex = 203
        '
        'TerTabInactive
        '
        Me.TerTabInactive.AllowDrop = True
        Me.TerTabInactive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerTabInactive.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTabInactive.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTabInactive.DontShowInfo = False
        Me.TerTabInactive.Location = New System.Drawing.Point(242, 93)
        Me.TerTabInactive.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerTabInactive.Name = "TerTabInactive"
        Me.TerTabInactive.Size = New System.Drawing.Size(100, 24)
        Me.TerTabInactive.TabIndex = 205
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(3, 63)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 221
        Me.PictureBox4.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 33)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 220
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 219
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(33, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(202, 24)
        Me.Label5.TabIndex = 199
        Me.Label5.Text = "Inactive title"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(33, 63)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(202, 24)
        Me.Label10.TabIndex = 202
        Me.Label10.Text = "Active tab"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(33, 93)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(202, 24)
        Me.Label9.TabIndex = 204
        Me.Label9.Text = "Inactive tab"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button2.LineSize = 1
        Me.Button2.Location = New System.Drawing.Point(700, 600)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 34)
        Me.Button2.TabIndex = 106
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'GroupBox22
        '
        Me.GroupBox22.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox22.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox22.Controls.Add(Me.Terminal1)
        Me.GroupBox22.Controls.Add(Me.Button15)
        Me.GroupBox22.Controls.Add(Me.PictureBox27)
        Me.GroupBox22.Controls.Add(Me.Label141)
        Me.GroupBox22.Controls.Add(Me.Terminal2)
        Me.GroupBox22.Location = New System.Drawing.Point(479, 106)
        Me.GroupBox22.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox22.Size = New System.Drawing.Size(517, 301)
        Me.GroupBox22.TabIndex = 195
        '
        'Terminal1
        '
        Me.Terminal1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Terminal1.BackImage = Nothing
        Me.Terminal1.Color_Background = System.Drawing.Color.Black
        Me.Terminal1.Color_Cursor = System.Drawing.Color.White
        Me.Terminal1.Color_Foreground = System.Drawing.Color.White
        Me.Terminal1.Color_Selection = System.Drawing.Color.Gray
        Me.Terminal1.Color_TabFocused = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Terminal1.Color_TabUnFocused = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Terminal1.Color_Titlebar = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Terminal1.Color_Titlebar_Unfocused = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Terminal1.CursorHeight = 25
        Me.Terminal1.CursorType = WinPaletter.UI.Simulation.WinTerminal.CursorShape_Enum.bar
        Me.Terminal1.Font = New System.Drawing.Font("Cascadia Mono", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Terminal1.IsFocused = True
        Me.Terminal1.Light = False
        Me.Terminal1.Location = New System.Drawing.Point(6, 45)
        Me.Terminal1.Name = "Terminal1"
        Me.Terminal1.Opacity = 0.15!
        Me.Terminal1.OpacityBackImage = 100.0!
        Me.Terminal1.PreviewVersion = True
        Me.Terminal1.Size = New System.Drawing.Size(483, 226)
        Me.Terminal1.TabColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Terminal1.TabIcon = Nothing
        Me.Terminal1.TabIconButItIsString = ""
        Me.Terminal1.TabIndex = 95
        Me.Terminal1.TabTitle = "Command Prompt"
        Me.Terminal1.UseAcrylic = False
        Me.Terminal1.UseAcrylicOnTitlebar = False
        '
        'Button15
        '
        Me.Button15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button15.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button15.DrawOnGlass = False
        Me.Button15.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button15.ForeColor = System.Drawing.Color.White
        Me.Button15.Image = Nothing
        Me.Button15.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button15.LineSize = 1
        Me.Button15.Location = New System.Drawing.Point(356, 7)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(154, 30)
        Me.Button15.TabIndex = 94
        Me.Button15.Text = "Open Terminal for testing"
        Me.Button15.UseVisualStyleBackColor = False
        '
        'PictureBox27
        '
        Me.PictureBox27.Image = CType(resources.GetObject("PictureBox27.Image"), System.Drawing.Image)
        Me.PictureBox27.Location = New System.Drawing.Point(6, 5)
        Me.PictureBox27.Name = "PictureBox27"
        Me.PictureBox27.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox27.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox27.TabIndex = 4
        Me.PictureBox27.TabStop = False
        '
        'Label141
        '
        Me.Label141.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label141.Location = New System.Drawing.Point(47, 5)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(231, 35)
        Me.Label141.TabIndex = 3
        Me.Label141.Text = "Preview"
        Me.Label141.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Terminal2
        '
        Me.Terminal2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Terminal2.BackImage = Nothing
        Me.Terminal2.Color_Background = System.Drawing.Color.Black
        Me.Terminal2.Color_Cursor = System.Drawing.Color.White
        Me.Terminal2.Color_Foreground = System.Drawing.Color.White
        Me.Terminal2.Color_Selection = System.Drawing.Color.Gray
        Me.Terminal2.Color_TabFocused = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Terminal2.Color_TabUnFocused = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Terminal2.Color_Titlebar = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Terminal2.Color_Titlebar_Unfocused = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Terminal2.CursorHeight = 25
        Me.Terminal2.CursorType = WinPaletter.UI.Simulation.WinTerminal.CursorShape_Enum.bar
        Me.Terminal2.Font = New System.Drawing.Font("Cascadia Mono", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Terminal2.IsFocused = False
        Me.Terminal2.Light = False
        Me.Terminal2.Location = New System.Drawing.Point(54, 132)
        Me.Terminal2.Name = "Terminal2"
        Me.Terminal2.Opacity = 100.0!
        Me.Terminal2.OpacityBackImage = 0!
        Me.Terminal2.PreviewVersion = True
        Me.Terminal2.Size = New System.Drawing.Size(454, 160)
        Me.Terminal2.TabColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Terminal2.TabIcon = Nothing
        Me.Terminal2.TabIconButItIsString = ""
        Me.Terminal2.TabIndex = 96
        Me.Terminal2.TabTitle = ""
        Me.Terminal2.UseAcrylic = False
        Me.Terminal2.UseAcrylicOnTitlebar = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(916, 600)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 34)
        Me.Button1.TabIndex = 105
        Me.Button1.Text = "Load"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TerOpacityBar
        '
        Me.TerOpacityBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerOpacityBar.LargeChange = 10
        Me.TerOpacityBar.Location = New System.Drawing.Point(107, 94)
        Me.TerOpacityBar.Maximum = 100
        Me.TerOpacityBar.Minimum = 0
        Me.TerOpacityBar.Name = "TerOpacityBar"
        Me.TerOpacityBar.Size = New System.Drawing.Size(197, 19)
        Me.TerOpacityBar.SmallChange = 1
        Me.TerOpacityBar.TabIndex = 208
        Me.TerOpacityBar.Value = 100
        '
        'PictureBox40
        '
        Me.PictureBox40.Image = CType(resources.GetObject("PictureBox40.Image"), System.Drawing.Image)
        Me.PictureBox40.Location = New System.Drawing.Point(6, 92)
        Me.PictureBox40.Name = "PictureBox40"
        Me.PictureBox40.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox40.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox40.TabIndex = 206
        Me.PictureBox40.TabStop = False
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button5.DrawOnGlass = False
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Image = Nothing
        Me.Button5.LineColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Button5.LineSize = 1
        Me.Button5.Location = New System.Drawing.Point(188, 33)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(116, 25)
        Me.Button5.TabIndex = 197
        Me.Button5.Text = "Set as wallpaper"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'TerAcrylic
        '
        Me.TerAcrylic.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.TerAcrylic.Checked = False
        Me.TerAcrylic.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerAcrylic.ForeColor = System.Drawing.Color.White
        Me.TerAcrylic.Location = New System.Drawing.Point(35, 91)
        Me.TerAcrylic.Name = "TerAcrylic"
        Me.TerAcrylic.Size = New System.Drawing.Size(66, 24)
        Me.TerAcrylic.TabIndex = 207
        Me.TerAcrylic.Text = "Acrylic Back"
        '
        'Button16
        '
        Me.Button16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button16.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button16.DrawOnGlass = False
        Me.Button16.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button16.ForeColor = System.Drawing.Color.White
        Me.Button16.Image = CType(resources.GetObject("Button16.Image"), System.Drawing.Image)
        Me.Button16.LineColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.Button16.LineSize = 1
        Me.Button16.Location = New System.Drawing.Point(306, 33)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(39, 25)
        Me.Button16.TabIndex = 192
        Me.Button16.UseVisualStyleBackColor = False
        '
        'TerBackImage
        '
        Me.TerBackImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerBackImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TerBackImage.DrawOnGlass = False
        Me.TerBackImage.ForeColor = System.Drawing.Color.White
        Me.TerBackImage.Location = New System.Drawing.Point(122, 6)
        Me.TerBackImage.MaxLength = 32767
        Me.TerBackImage.Multiline = False
        Me.TerBackImage.Name = "TerBackImage"
        Me.TerBackImage.ReadOnly = False
        Me.TerBackImage.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TerBackImage.SelectedText = ""
        Me.TerBackImage.SelectionLength = 0
        Me.TerBackImage.SelectionStart = 0
        Me.TerBackImage.Size = New System.Drawing.Size(224, 24)
        Me.TerBackImage.TabIndex = 191
        Me.TerBackImage.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TerBackImage.UseSystemPasswordChar = False
        Me.TerBackImage.WordWrap = True
        '
        'TerImageOpacity
        '
        Me.TerImageOpacity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerImageOpacity.LargeChange = 10
        Me.TerImageOpacity.Location = New System.Drawing.Point(107, 65)
        Me.TerImageOpacity.Maximum = 100
        Me.TerImageOpacity.Minimum = 0
        Me.TerImageOpacity.Name = "TerImageOpacity"
        Me.TerImageOpacity.Size = New System.Drawing.Size(197, 19)
        Me.TerImageOpacity.SmallChange = 1
        Me.TerImageOpacity.TabIndex = 187
        Me.TerImageOpacity.Value = 100
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(36, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 24)
        Me.Label2.TabIndex = 196
        Me.Label2.Text = "Background:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox1.Checked = False
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(12, 605)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(332, 24)
        Me.CheckBox1.TabIndex = 104
        Me.CheckBox1.Text = "Allow non monospace fonts (causes wrong renderering)"
        '
        'Button20
        '
        Me.Button20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button20.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button20.DrawOnGlass = False
        Me.Button20.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button20.ForeColor = System.Drawing.Color.White
        Me.Button20.Image = CType(resources.GetObject("Button20.Image"), System.Drawing.Image)
        Me.Button20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button20.LineColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Button20.LineSize = 1
        Me.Button20.Location = New System.Drawing.Point(108, 6)
        Me.Button20.Name = "Button20"
        Me.Button20.Size = New System.Drawing.Size(87, 24)
        Me.Button20.TabIndex = 217
        Me.Button20.Text = "Copycat"
        Me.Button20.UseVisualStyleBackColor = False
        '
        'Button17
        '
        Me.Button17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button17.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button17.DrawOnGlass = False
        Me.Button17.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button17.ForeColor = System.Drawing.Color.White
        Me.Button17.Image = CType(resources.GetObject("Button17.Image"), System.Drawing.Image)
        Me.Button17.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button17.LineColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(145, Byte), Integer))
        Me.Button17.LineSize = 1
        Me.Button17.Location = New System.Drawing.Point(197, 6)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(76, 24)
        Me.Button17.TabIndex = 214
        Me.Button17.Text = "Clone"
        Me.Button17.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button4.DrawOnGlass = False
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.LineColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.Button4.LineSize = 1
        Me.Button4.Location = New System.Drawing.Point(275, 6)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(35, 24)
        Me.Button4.TabIndex = 213
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button12.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button12.DrawOnGlass = False
        Me.Button12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button12.ForeColor = System.Drawing.Color.White
        Me.Button12.Image = CType(resources.GetObject("Button12.Image"), System.Drawing.Image)
        Me.Button12.LineColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button12.LineSize = 1
        Me.Button12.Location = New System.Drawing.Point(312, 6)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(35, 24)
        Me.Button12.TabIndex = 129
        Me.Button12.UseVisualStyleBackColor = False
        '
        'TerSchemes
        '
        Me.TerSchemes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerSchemes.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TerSchemes.CustomFont = False
        Me.TerSchemes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TerSchemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TerSchemes.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerSchemes.ForeColor = System.Drawing.Color.White
        Me.TerSchemes.FormattingEnabled = True
        Me.TerSchemes.ItemHeight = 20
        Me.TerSchemes.Location = New System.Drawing.Point(5, 33)
        Me.TerSchemes.Name = "TerSchemes"
        Me.TerSchemes.Size = New System.Drawing.Size(342, 26)
        Me.TerSchemes.TabIndex = 117
        '
        'TerCursor
        '
        Me.TerCursor.AllowDrop = True
        Me.TerCursor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerCursor.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerCursor.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerCursor.DontShowInfo = False
        Me.TerCursor.Location = New System.Drawing.Point(245, 153)
        Me.TerCursor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerCursor.Name = "TerCursor"
        Me.TerCursor.Size = New System.Drawing.Size(100, 24)
        Me.TerCursor.TabIndex = 125
        '
        'TerWhiteB
        '
        Me.TerWhiteB.AllowDrop = True
        Me.TerWhiteB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerWhiteB.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.TerWhiteB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.TerWhiteB.DontShowInfo = False
        Me.TerWhiteB.Location = New System.Drawing.Point(245, 401)
        Me.TerWhiteB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerWhiteB.Name = "TerWhiteB"
        Me.TerWhiteB.Size = New System.Drawing.Size(100, 24)
        Me.TerWhiteB.TabIndex = 115
        '
        'Label150
        '
        Me.Label150.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label150.BackColor = System.Drawing.Color.Transparent
        Me.Label150.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label150.Location = New System.Drawing.Point(36, 153)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(202, 24)
        Me.Label150.TabIndex = 124
        Me.Label150.Text = "Cursor"
        Me.Label150.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerBlue
        '
        Me.TerBlue.AllowDrop = True
        Me.TerBlue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerBlue.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.TerBlue.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.TerBlue.DontShowInfo = False
        Me.TerBlue.Location = New System.Drawing.Point(140, 222)
        Me.TerBlue.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerBlue.Name = "TerBlue"
        Me.TerBlue.Size = New System.Drawing.Size(100, 24)
        Me.TerBlue.TabIndex = 101
        '
        'Label151
        '
        Me.Label151.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label151.BackColor = System.Drawing.Color.Transparent
        Me.Label151.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.Location = New System.Drawing.Point(36, 401)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(97, 24)
        Me.Label151.TabIndex = 91
        Me.Label151.Text = "White"
        Me.Label151.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerSelection
        '
        Me.TerSelection.AllowDrop = True
        Me.TerSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerSelection.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerSelection.DontShowInfo = False
        Me.TerSelection.Location = New System.Drawing.Point(245, 123)
        Me.TerSelection.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerSelection.Name = "TerSelection"
        Me.TerSelection.Size = New System.Drawing.Size(100, 24)
        Me.TerSelection.TabIndex = 123
        '
        'Label152
        '
        Me.Label152.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label152.BackColor = System.Drawing.Color.Transparent
        Me.Label152.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label152.Location = New System.Drawing.Point(36, 123)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(202, 24)
        Me.Label152.TabIndex = 122
        Me.Label152.Text = "Selection"
        Me.Label152.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label147
        '
        Me.Label147.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label147.BackColor = System.Drawing.Color.Transparent
        Me.Label147.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.Location = New System.Drawing.Point(36, 93)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(202, 24)
        Me.Label147.TabIndex = 120
        Me.Label147.Text = "Foreground"
        Me.Label147.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerWhite
        '
        Me.TerWhite.AllowDrop = True
        Me.TerWhite.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerWhite.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.TerWhite.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.TerWhite.DontShowInfo = False
        Me.TerWhite.Location = New System.Drawing.Point(140, 401)
        Me.TerWhite.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerWhite.Name = "TerWhite"
        Me.TerWhite.Size = New System.Drawing.Size(100, 24)
        Me.TerWhite.TabIndex = 107
        '
        'TerForeground
        '
        Me.TerForeground.AllowDrop = True
        Me.TerForeground.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerForeground.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerForeground.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerForeground.DontShowInfo = False
        Me.TerForeground.Location = New System.Drawing.Point(245, 93)
        Me.TerForeground.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerForeground.Name = "TerForeground"
        Me.TerForeground.Size = New System.Drawing.Size(100, 24)
        Me.TerForeground.TabIndex = 121
        '
        'TerCyanB
        '
        Me.TerCyanB.AllowDrop = True
        Me.TerCyanB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerCyanB.BackColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.TerCyanB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.TerCyanB.DontShowInfo = False
        Me.TerCyanB.Location = New System.Drawing.Point(245, 251)
        Me.TerCyanB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerCyanB.Name = "TerCyanB"
        Me.TerCyanB.Size = New System.Drawing.Size(100, 24)
        Me.TerCyanB.TabIndex = 111
        '
        'Label145
        '
        Me.Label145.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label145.BackColor = System.Drawing.Color.Transparent
        Me.Label145.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label145.Location = New System.Drawing.Point(36, 251)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(97, 24)
        Me.Label145.TabIndex = 87
        Me.Label145.Text = "Cyan"
        Me.Label145.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerCyan
        '
        Me.TerCyan.AllowDrop = True
        Me.TerCyan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerCyan.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.TerCyan.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.TerCyan.DontShowInfo = False
        Me.TerCyan.Location = New System.Drawing.Point(140, 251)
        Me.TerCyan.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerCyan.Name = "TerCyan"
        Me.TerCyan.Size = New System.Drawing.Size(100, 24)
        Me.TerCyan.TabIndex = 103
        '
        'TerGreen
        '
        Me.TerGreen.AllowDrop = True
        Me.TerGreen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerGreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(19, Byte), Integer), CType(CType(161, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.TerGreen.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(19, Byte), Integer), CType(CType(161, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.TerGreen.DontShowInfo = False
        Me.TerGreen.Location = New System.Drawing.Point(140, 281)
        Me.TerGreen.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerGreen.Name = "TerGreen"
        Me.TerGreen.Size = New System.Drawing.Size(100, 24)
        Me.TerGreen.TabIndex = 102
        '
        'Label144
        '
        Me.Label144.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label144.BackColor = System.Drawing.Color.Transparent
        Me.Label144.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label144.Location = New System.Drawing.Point(36, 281)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(97, 24)
        Me.Label144.TabIndex = 86
        Me.Label144.Text = "Green"
        Me.Label144.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerBackground
        '
        Me.TerBackground.AllowDrop = True
        Me.TerBackground.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerBackground.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerBackground.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerBackground.DontShowInfo = False
        Me.TerBackground.Location = New System.Drawing.Point(245, 63)
        Me.TerBackground.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerBackground.Name = "TerBackground"
        Me.TerBackground.Size = New System.Drawing.Size(100, 24)
        Me.TerBackground.TabIndex = 119
        '
        'TerYellow
        '
        Me.TerYellow.AllowDrop = True
        Me.TerYellow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerYellow.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TerYellow.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TerYellow.DontShowInfo = False
        Me.TerYellow.Location = New System.Drawing.Point(140, 371)
        Me.TerYellow.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerYellow.Name = "TerYellow"
        Me.TerYellow.Size = New System.Drawing.Size(100, 24)
        Me.TerYellow.TabIndex = 106
        '
        'TerGreenB
        '
        Me.TerGreenB.AllowDrop = True
        Me.TerGreenB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerGreenB.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerGreenB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerGreenB.DontShowInfo = False
        Me.TerGreenB.Location = New System.Drawing.Point(245, 281)
        Me.TerGreenB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerGreenB.Name = "TerGreenB"
        Me.TerGreenB.Size = New System.Drawing.Size(100, 24)
        Me.TerGreenB.TabIndex = 110
        '
        'Label143
        '
        Me.Label143.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label143.BackColor = System.Drawing.Color.Transparent
        Me.Label143.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label143.Location = New System.Drawing.Point(36, 63)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(202, 24)
        Me.Label143.TabIndex = 118
        Me.Label143.Text = "Background"
        Me.Label143.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label149
        '
        Me.Label149.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label149.BackColor = System.Drawing.Color.Transparent
        Me.Label149.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label149.Location = New System.Drawing.Point(36, 371)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(97, 24)
        Me.Label149.TabIndex = 90
        Me.Label149.Text = "Yellow"
        Me.Label149.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerBlack
        '
        Me.TerBlack.AllowDrop = True
        Me.TerBlack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerBlack.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerBlack.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerBlack.DontShowInfo = False
        Me.TerBlack.Location = New System.Drawing.Point(140, 191)
        Me.TerBlack.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerBlack.Name = "TerBlack"
        Me.TerBlack.Size = New System.Drawing.Size(100, 24)
        Me.TerBlack.TabIndex = 100
        '
        'TerYellowB
        '
        Me.TerYellowB.AllowDrop = True
        Me.TerYellowB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerYellowB.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.TerYellowB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.TerYellowB.DontShowInfo = False
        Me.TerYellowB.Location = New System.Drawing.Point(245, 371)
        Me.TerYellowB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerYellowB.Name = "TerYellowB"
        Me.TerYellowB.Size = New System.Drawing.Size(100, 24)
        Me.TerYellowB.TabIndex = 114
        '
        'Label142
        '
        Me.Label142.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label142.BackColor = System.Drawing.Color.Transparent
        Me.Label142.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label142.Location = New System.Drawing.Point(36, 221)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(97, 24)
        Me.Label142.TabIndex = 85
        Me.Label142.Text = "Blue"
        Me.Label142.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerBlackB
        '
        Me.TerBlackB.AllowDrop = True
        Me.TerBlackB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerBlackB.BackColor = System.Drawing.Color.FromArgb(CType(CType(118, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.TerBlackB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(118, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.TerBlackB.DontShowInfo = False
        Me.TerBlackB.Location = New System.Drawing.Point(245, 191)
        Me.TerBlackB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerBlackB.Name = "TerBlackB"
        Me.TerBlackB.Size = New System.Drawing.Size(100, 24)
        Me.TerBlackB.TabIndex = 108
        '
        'TerPurple
        '
        Me.TerPurple.AllowDrop = True
        Me.TerPurple.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerPurple.BackColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.TerPurple.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.TerPurple.DontShowInfo = False
        Me.TerPurple.Location = New System.Drawing.Point(140, 341)
        Me.TerPurple.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerPurple.Name = "TerPurple"
        Me.TerPurple.Size = New System.Drawing.Size(100, 24)
        Me.TerPurple.TabIndex = 105
        '
        'Label140
        '
        Me.Label140.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label140.BackColor = System.Drawing.Color.Transparent
        Me.Label140.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label140.Location = New System.Drawing.Point(36, 191)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(97, 24)
        Me.Label140.TabIndex = 4
        Me.Label140.Text = "Black"
        Me.Label140.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerPurpleB
        '
        Me.TerPurpleB.AllowDrop = True
        Me.TerPurpleB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerPurpleB.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.TerPurpleB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.TerPurpleB.DontShowInfo = False
        Me.TerPurpleB.Location = New System.Drawing.Point(245, 341)
        Me.TerPurpleB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerPurpleB.Name = "TerPurpleB"
        Me.TerPurpleB.Size = New System.Drawing.Size(100, 24)
        Me.TerPurpleB.TabIndex = 113
        '
        'TerBlueB
        '
        Me.TerBlueB.AllowDrop = True
        Me.TerBlueB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerBlueB.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TerBlueB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TerBlueB.DontShowInfo = False
        Me.TerBlueB.Location = New System.Drawing.Point(245, 222)
        Me.TerBlueB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerBlueB.Name = "TerBlueB"
        Me.TerBlueB.Size = New System.Drawing.Size(100, 24)
        Me.TerBlueB.TabIndex = 109
        '
        'Label146
        '
        Me.Label146.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label146.BackColor = System.Drawing.Color.Transparent
        Me.Label146.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label146.Location = New System.Drawing.Point(36, 311)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(97, 24)
        Me.Label146.TabIndex = 88
        Me.Label146.Text = "Red"
        Me.Label146.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label148
        '
        Me.Label148.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label148.BackColor = System.Drawing.Color.Transparent
        Me.Label148.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label148.Location = New System.Drawing.Point(36, 341)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(97, 24)
        Me.Label148.TabIndex = 89
        Me.Label148.Text = "Purple"
        Me.Label148.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerRedB
        '
        Me.TerRedB.AllowDrop = True
        Me.TerRedB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerRedB.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.TerRedB.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.TerRedB.DontShowInfo = False
        Me.TerRedB.Location = New System.Drawing.Point(245, 311)
        Me.TerRedB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerRedB.Name = "TerRedB"
        Me.TerRedB.Size = New System.Drawing.Size(100, 24)
        Me.TerRedB.TabIndex = 112
        '
        'TerRed
        '
        Me.TerRed.AllowDrop = True
        Me.TerRed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerRed.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(31, Byte), Integer))
        Me.TerRed.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(31, Byte), Integer))
        Me.TerRed.DontShowInfo = False
        Me.TerRed.Location = New System.Drawing.Point(140, 311)
        Me.TerRed.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerRed.Name = "TerRed"
        Me.TerRed.Size = New System.Drawing.Size(100, 24)
        Me.TerRed.TabIndex = 104
        '
        'TerCursorHeightBar
        '
        Me.TerCursorHeightBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerCursorHeightBar.LargeChange = 1
        Me.TerCursorHeightBar.Location = New System.Drawing.Point(96, 39)
        Me.TerCursorHeightBar.Maximum = 100
        Me.TerCursorHeightBar.Minimum = 1
        Me.TerCursorHeightBar.Name = "TerCursorHeightBar"
        Me.TerCursorHeightBar.Size = New System.Drawing.Size(210, 19)
        Me.TerCursorHeightBar.SmallChange = 1
        Me.TerCursorHeightBar.TabIndex = 102
        Me.TerCursorHeightBar.Value = 20
        '
        'TerCursorStyle
        '
        Me.TerCursorStyle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerCursorStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerCursorStyle.CustomFont = False
        Me.TerCursorStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TerCursorStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TerCursorStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerCursorStyle.ForeColor = System.Drawing.Color.White
        Me.TerCursorStyle.FormattingEnabled = True
        Me.TerCursorStyle.ItemHeight = 20
        Me.TerCursorStyle.Items.AddRange(New Object() {"Bar", "Double Underscore", "Empty Box", "Filled Box", "Underscore", "Vintage"})
        Me.TerCursorStyle.Location = New System.Drawing.Point(96, 5)
        Me.TerCursorStyle.Name = "TerCursorStyle"
        Me.TerCursorStyle.Size = New System.Drawing.Size(250, 26)
        Me.TerCursorStyle.TabIndex = 110
        '
        'Button10
        '
        Me.Button10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button10.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button10.DrawOnGlass = False
        Me.Button10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button10.ForeColor = System.Drawing.Color.White
        Me.Button10.Image = CType(resources.GetObject("Button10.Image"), System.Drawing.Image)
        Me.Button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button10.LineColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Button10.LineSize = 1
        Me.Button10.Location = New System.Drawing.Point(786, 600)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(124, 34)
        Me.Button10.TabIndex = 107
        Me.Button10.Text = "Quick apply"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'GroupBox13
        '
        Me.GroupBox13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox13.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox13.Controls.Add(Me.Button6)
        Me.GroupBox13.Controls.Add(Me.Button18)
        Me.GroupBox13.Controls.Add(Me.Button14)
        Me.GroupBox13.Controls.Add(Me.PictureBox25)
        Me.GroupBox13.Controls.Add(Me.Label155)
        Me.GroupBox13.Controls.Add(Me.TerProfiles)
        Me.GroupBox13.Controls.Add(Me.Button13)
        Me.GroupBox13.Location = New System.Drawing.Point(12, 106)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(460, 40)
        Me.GroupBox13.TabIndex = 117
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button6.DrawOnGlass = False
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.LineColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Button6.LineSize = 1
        Me.Button6.Location = New System.Drawing.Point(328, 5)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(30, 30)
        Me.Button6.TabIndex = 216
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button18
        '
        Me.Button18.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button18.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button18.DrawOnGlass = False
        Me.Button18.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button18.ForeColor = System.Drawing.Color.White
        Me.Button18.Image = CType(resources.GetObject("Button18.Image"), System.Drawing.Image)
        Me.Button18.LineColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(145, Byte), Integer))
        Me.Button18.LineSize = 1
        Me.Button18.Location = New System.Drawing.Point(360, 5)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(30, 30)
        Me.Button18.TabIndex = 215
        Me.Button18.UseVisualStyleBackColor = False
        '
        'Button14
        '
        Me.Button14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button14.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button14.DrawOnGlass = False
        Me.Button14.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button14.ForeColor = System.Drawing.Color.White
        Me.Button14.Image = CType(resources.GetObject("Button14.Image"), System.Drawing.Image)
        Me.Button14.LineColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.Button14.LineSize = 1
        Me.Button14.Location = New System.Drawing.Point(392, 5)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(30, 30)
        Me.Button14.TabIndex = 179
        Me.Button14.UseVisualStyleBackColor = False
        '
        'PictureBox25
        '
        Me.PictureBox25.Image = CType(resources.GetObject("PictureBox25.Image"), System.Drawing.Image)
        Me.PictureBox25.Location = New System.Drawing.Point(4, 5)
        Me.PictureBox25.Name = "PictureBox25"
        Me.PictureBox25.Size = New System.Drawing.Size(35, 31)
        Me.PictureBox25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox25.TabIndex = 83
        Me.PictureBox25.TabStop = False
        '
        'Label155
        '
        Me.Label155.BackColor = System.Drawing.Color.Transparent
        Me.Label155.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label155.Location = New System.Drawing.Point(41, 5)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(62, 31)
        Me.Label155.TabIndex = 84
        Me.Label155.Text = "Profiles:"
        Me.Label155.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerProfiles
        '
        Me.TerProfiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerProfiles.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TerProfiles.CustomFont = False
        Me.TerProfiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TerProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TerProfiles.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerProfiles.ForeColor = System.Drawing.Color.White
        Me.TerProfiles.FormattingEnabled = True
        Me.TerProfiles.ItemHeight = 20
        Me.TerProfiles.Location = New System.Drawing.Point(108, 7)
        Me.TerProfiles.Name = "TerProfiles"
        Me.TerProfiles.Size = New System.Drawing.Size(216, 26)
        Me.TerProfiles.TabIndex = 118
        '
        'Button13
        '
        Me.Button13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button13.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button13.DrawOnGlass = False
        Me.Button13.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button13.ForeColor = System.Drawing.Color.White
        Me.Button13.Image = CType(resources.GetObject("Button13.Image"), System.Drawing.Image)
        Me.Button13.LineColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button13.LineSize = 1
        Me.Button13.Location = New System.Drawing.Point(424, 5)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(30, 30)
        Me.Button13.TabIndex = 140
        Me.Button13.UseVisualStyleBackColor = False
        '
        'TerFontSizeBar
        '
        Me.TerFontSizeBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerFontSizeBar.LargeChange = 10
        Me.TerFontSizeBar.Location = New System.Drawing.Point(96, 69)
        Me.TerFontSizeBar.Maximum = 48
        Me.TerFontSizeBar.Minimum = 5
        Me.TerFontSizeBar.Name = "TerFontSizeBar"
        Me.TerFontSizeBar.Size = New System.Drawing.Size(210, 19)
        Me.TerFontSizeBar.SmallChange = 1
        Me.TerFontSizeBar.TabIndex = 101
        Me.TerFontSizeBar.Value = 5
        '
        'TerFontWeight
        '
        Me.TerFontWeight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerFontWeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerFontWeight.CustomFont = False
        Me.TerFontWeight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TerFontWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TerFontWeight.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerFontWeight.ForeColor = System.Drawing.Color.White
        Me.TerFontWeight.FormattingEnabled = True
        Me.TerFontWeight.ItemHeight = 20
        Me.TerFontWeight.Items.AddRange(New Object() {"Thin", "Extra Light", "Light", "Semi Light", "Normal", "Medium", "Semi Bold", "Bold", "Extra Bold", "Black", "Extra Black"})
        Me.TerFontWeight.Location = New System.Drawing.Point(96, 35)
        Me.TerFontWeight.Name = "TerFontWeight"
        Me.TerFontWeight.Size = New System.Drawing.Size(250, 26)
        Me.TerFontWeight.TabIndex = 99
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.Button22)
        Me.GroupBox3.Controls.Add(Me.Separator2)
        Me.GroupBox3.Controls.Add(Me.Button11)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Button9)
        Me.GroupBox3.Controls.Add(Me.checker_img)
        Me.GroupBox3.Controls.Add(Me.AlertBox1)
        Me.GroupBox3.Controls.Add(Me.Button8)
        Me.GroupBox3.Controls.Add(Me.Button7)
        Me.GroupBox3.Controls.Add(Me.TerEnabled)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(984, 81)
        Me.GroupBox3.TabIndex = 199
        '
        'Button22
        '
        Me.Button22.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button22.DrawOnGlass = False
        Me.Button22.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button22.ForeColor = System.Drawing.Color.White
        Me.Button22.Image = CType(resources.GetObject("Button22.Image"), System.Drawing.Image)
        Me.Button22.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button22.LineColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.Button22.LineSize = 1
        Me.Button22.Location = New System.Drawing.Point(223, 6)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(146, 29)
        Me.Button22.TabIndex = 202
        Me.Button22.Text = "Current applied one"
        Me.Button22.UseVisualStyleBackColor = False
        '
        'Separator2
        '
        Me.Separator2.AlternativeLook = False
        Me.Separator2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator2.Location = New System.Drawing.Point(4, 40)
        Me.Separator2.Name = "Separator2"
        Me.Separator2.Size = New System.Drawing.Size(973, 1)
        Me.Separator2.TabIndex = 201
        Me.Separator2.TabStop = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 32)
        Me.Label11.TabIndex = 111
        Me.Label11.Text = "Open from:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'checker_img
        '
        Me.checker_img.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checker_img.Image = Global.WinPaletter.My.Resources.Resources.checker_disabled
        Me.checker_img.Location = New System.Drawing.Point(891, 5)
        Me.checker_img.Name = "checker_img"
        Me.checker_img.Size = New System.Drawing.Size(35, 31)
        Me.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.checker_img.TabIndex = 83
        Me.checker_img.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TabControl1.AllowDrop = True
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabControl1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TabControl1.ItemSize = New System.Drawing.Size(35, 100)
        Me.TabControl1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.TabControl1.Location = New System.Drawing.Point(10, 148)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(471, 454)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 201
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Location = New System.Drawing.Point(104, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(363, 446)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Colors"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox4.Controls.Add(Me.TerWhiteB)
        Me.GroupBox4.Controls.Add(Me.PictureBox29)
        Me.GroupBox4.Controls.Add(Me.TerWhite)
        Me.GroupBox4.Controls.Add(Me.Label151)
        Me.GroupBox4.Controls.Add(Me.TerYellow)
        Me.GroupBox4.Controls.Add(Me.TerYellowB)
        Me.GroupBox4.Controls.Add(Me.TerGreen)
        Me.GroupBox4.Controls.Add(Me.TerCyanB)
        Me.GroupBox4.Controls.Add(Me.TerGreenB)
        Me.GroupBox4.Controls.Add(Me.TerPurple)
        Me.GroupBox4.Controls.Add(Me.PictureBox30)
        Me.GroupBox4.Controls.Add(Me.TerPurpleB)
        Me.GroupBox4.Controls.Add(Me.TerCyan)
        Me.GroupBox4.Controls.Add(Me.PictureBox31)
        Me.GroupBox4.Controls.Add(Me.TerRedB)
        Me.GroupBox4.Controls.Add(Me.PictureBox34)
        Me.GroupBox4.Controls.Add(Me.TerRed)
        Me.GroupBox4.Controls.Add(Me.PictureBox26)
        Me.GroupBox4.Controls.Add(Me.PictureBox28)
        Me.GroupBox4.Controls.Add(Me.PictureBox23)
        Me.GroupBox4.Controls.Add(Me.TerBlue)
        Me.GroupBox4.Controls.Add(Me.Label149)
        Me.GroupBox4.Controls.Add(Me.Label145)
        Me.GroupBox4.Controls.Add(Me.PictureBox22)
        Me.GroupBox4.Controls.Add(Me.Label144)
        Me.GroupBox4.Controls.Add(Me.Separator3)
        Me.GroupBox4.Controls.Add(Me.TerCursor)
        Me.GroupBox4.Controls.Add(Me.PictureBox21)
        Me.GroupBox4.Controls.Add(Me.Label146)
        Me.GroupBox4.Controls.Add(Me.Label148)
        Me.GroupBox4.Controls.Add(Me.PictureBox20)
        Me.GroupBox4.Controls.Add(Me.Label150)
        Me.GroupBox4.Controls.Add(Me.PictureBox19)
        Me.GroupBox4.Controls.Add(Me.TerSelection)
        Me.GroupBox4.Controls.Add(Me.PictureBox15)
        Me.GroupBox4.Controls.Add(Me.TerSchemes)
        Me.GroupBox4.Controls.Add(Me.TerForeground)
        Me.GroupBox4.Controls.Add(Me.Button20)
        Me.GroupBox4.Controls.Add(Me.Label142)
        Me.GroupBox4.Controls.Add(Me.TerBlack)
        Me.GroupBox4.Controls.Add(Me.Label152)
        Me.GroupBox4.Controls.Add(Me.PictureBox14)
        Me.GroupBox4.Controls.Add(Me.TerBlueB)
        Me.GroupBox4.Controls.Add(Me.TerBlackB)
        Me.GroupBox4.Controls.Add(Me.Label147)
        Me.GroupBox4.Controls.Add(Me.Button17)
        Me.GroupBox4.Controls.Add(Me.Button4)
        Me.GroupBox4.Controls.Add(Me.Button12)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.Label140)
        Me.GroupBox4.Controls.Add(Me.Label143)
        Me.GroupBox4.Controls.Add(Me.TerBackground)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(351, 432)
        Me.GroupBox4.TabIndex = 87
        '
        'PictureBox29
        '
        Me.PictureBox29.Image = CType(resources.GetObject("PictureBox29.Image"), System.Drawing.Image)
        Me.PictureBox29.Location = New System.Drawing.Point(6, 401)
        Me.PictureBox29.Name = "PictureBox29"
        Me.PictureBox29.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox29.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox29.TabIndex = 233
        Me.PictureBox29.TabStop = False
        '
        'PictureBox30
        '
        Me.PictureBox30.Image = CType(resources.GetObject("PictureBox30.Image"), System.Drawing.Image)
        Me.PictureBox30.Location = New System.Drawing.Point(6, 371)
        Me.PictureBox30.Name = "PictureBox30"
        Me.PictureBox30.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox30.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox30.TabIndex = 232
        Me.PictureBox30.TabStop = False
        '
        'PictureBox31
        '
        Me.PictureBox31.Image = CType(resources.GetObject("PictureBox31.Image"), System.Drawing.Image)
        Me.PictureBox31.Location = New System.Drawing.Point(6, 341)
        Me.PictureBox31.Name = "PictureBox31"
        Me.PictureBox31.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox31.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox31.TabIndex = 231
        Me.PictureBox31.TabStop = False
        '
        'PictureBox34
        '
        Me.PictureBox34.Image = CType(resources.GetObject("PictureBox34.Image"), System.Drawing.Image)
        Me.PictureBox34.Location = New System.Drawing.Point(6, 311)
        Me.PictureBox34.Name = "PictureBox34"
        Me.PictureBox34.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox34.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox34.TabIndex = 230
        Me.PictureBox34.TabStop = False
        '
        'PictureBox26
        '
        Me.PictureBox26.Image = CType(resources.GetObject("PictureBox26.Image"), System.Drawing.Image)
        Me.PictureBox26.Location = New System.Drawing.Point(6, 281)
        Me.PictureBox26.Name = "PictureBox26"
        Me.PictureBox26.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox26.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox26.TabIndex = 229
        Me.PictureBox26.TabStop = False
        '
        'PictureBox28
        '
        Me.PictureBox28.Image = CType(resources.GetObject("PictureBox28.Image"), System.Drawing.Image)
        Me.PictureBox28.Location = New System.Drawing.Point(6, 251)
        Me.PictureBox28.Name = "PictureBox28"
        Me.PictureBox28.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox28.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox28.TabIndex = 228
        Me.PictureBox28.TabStop = False
        '
        'PictureBox23
        '
        Me.PictureBox23.Image = CType(resources.GetObject("PictureBox23.Image"), System.Drawing.Image)
        Me.PictureBox23.Location = New System.Drawing.Point(6, 221)
        Me.PictureBox23.Name = "PictureBox23"
        Me.PictureBox23.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox23.TabIndex = 225
        Me.PictureBox23.TabStop = False
        '
        'PictureBox22
        '
        Me.PictureBox22.Image = CType(resources.GetObject("PictureBox22.Image"), System.Drawing.Image)
        Me.PictureBox22.Location = New System.Drawing.Point(6, 191)
        Me.PictureBox22.Name = "PictureBox22"
        Me.PictureBox22.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox22.TabIndex = 223
        Me.PictureBox22.TabStop = False
        '
        'Separator3
        '
        Me.Separator3.AlternativeLook = False
        Me.Separator3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator3.Location = New System.Drawing.Point(6, 184)
        Me.Separator3.Name = "Separator3"
        Me.Separator3.Size = New System.Drawing.Size(341, 1)
        Me.Separator3.TabIndex = 222
        Me.Separator3.TabStop = False
        '
        'PictureBox21
        '
        Me.PictureBox21.Image = CType(resources.GetObject("PictureBox21.Image"), System.Drawing.Image)
        Me.PictureBox21.Location = New System.Drawing.Point(6, 153)
        Me.PictureBox21.Name = "PictureBox21"
        Me.PictureBox21.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox21.TabIndex = 221
        Me.PictureBox21.TabStop = False
        '
        'PictureBox20
        '
        Me.PictureBox20.Image = CType(resources.GetObject("PictureBox20.Image"), System.Drawing.Image)
        Me.PictureBox20.Location = New System.Drawing.Point(6, 123)
        Me.PictureBox20.Name = "PictureBox20"
        Me.PictureBox20.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox20.TabIndex = 220
        Me.PictureBox20.TabStop = False
        '
        'PictureBox19
        '
        Me.PictureBox19.Image = CType(resources.GetObject("PictureBox19.Image"), System.Drawing.Image)
        Me.PictureBox19.Location = New System.Drawing.Point(6, 93)
        Me.PictureBox19.Name = "PictureBox19"
        Me.PictureBox19.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox19.TabIndex = 219
        Me.PictureBox19.TabStop = False
        '
        'PictureBox15
        '
        Me.PictureBox15.Image = CType(resources.GetObject("PictureBox15.Image"), System.Drawing.Image)
        Me.PictureBox15.Location = New System.Drawing.Point(6, 63)
        Me.PictureBox15.Name = "PictureBox15"
        Me.PictureBox15.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox15.TabIndex = 218
        Me.PictureBox15.TabStop = False
        '
        'PictureBox14
        '
        Me.PictureBox14.Image = CType(resources.GetObject("PictureBox14.Image"), System.Drawing.Image)
        Me.PictureBox14.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox14.Name = "PictureBox14"
        Me.PictureBox14.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox14.TabIndex = 201
        Me.PictureBox14.TabStop = False
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(36, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 24)
        Me.Label19.TabIndex = 84
        Me.Label19.Text = "Scheme:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage5.Controls.Add(Me.GroupBox2)
        Me.TabPage5.Location = New System.Drawing.Point(104, 4)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(363, 446)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Theme"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.TerThemes)
        Me.GroupBox2.Controls.Add(Me.Button21)
        Me.GroupBox2.Controls.Add(Me.TerThemesContainer)
        Me.GroupBox2.Controls.Add(Me.PictureBox37)
        Me.GroupBox2.Controls.Add(Me.Button19)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.TerEditThemeName)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(351, 212)
        Me.GroupBox2.TabIndex = 88
        '
        'PictureBox37
        '
        Me.PictureBox37.Image = CType(resources.GetObject("PictureBox37.Image"), System.Drawing.Image)
        Me.PictureBox37.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox37.Name = "PictureBox37"
        Me.PictureBox37.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox37.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox37.TabIndex = 201
        Me.PictureBox37.TabStop = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(36, 6)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(65, 24)
        Me.Label20.TabIndex = 84
        Me.Label20.Text = "Theme:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Location = New System.Drawing.Point(104, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(363, 446)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Fonts"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox5.Controls.Add(Me.TerFontName)
        Me.GroupBox5.Controls.Add(Me.Button23)
        Me.GroupBox5.Controls.Add(Me.TerFontSizeVal)
        Me.GroupBox5.Controls.Add(Me.PictureBox5)
        Me.GroupBox5.Controls.Add(Me.TerFontWeight)
        Me.GroupBox5.Controls.Add(Me.TerFontSizeBar)
        Me.GroupBox5.Controls.Add(Me.Label61)
        Me.GroupBox5.Controls.Add(Me.PictureBox8)
        Me.GroupBox5.Controls.Add(Me.Label35)
        Me.GroupBox5.Controls.Add(Me.PictureBox9)
        Me.GroupBox5.Controls.Add(Me.Label59)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(351, 97)
        Me.GroupBox5.TabIndex = 98
        '
        'TerFontName
        '
        Me.TerFontName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerFontName.BackColor = System.Drawing.Color.Transparent
        Me.TerFontName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TerFontName.Location = New System.Drawing.Point(96, 6)
        Me.TerFontName.Name = "TerFontName"
        Me.TerFontName.Size = New System.Drawing.Size(210, 24)
        Me.TerFontName.TabIndex = 138
        Me.TerFontName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button23
        '
        Me.Button23.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button23.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button23.DrawOnGlass = False
        Me.Button23.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button23.ForeColor = System.Drawing.Color.White
        Me.Button23.Image = Nothing
        Me.Button23.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button23.LineSize = 1
        Me.Button23.Location = New System.Drawing.Point(316, 6)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(30, 24)
        Me.Button23.TabIndex = 137
        Me.Button23.Text = "..."
        Me.Button23.UseVisualStyleBackColor = False
        '
        'TerFontSizeVal
        '
        Me.TerFontSizeVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerFontSizeVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerFontSizeVal.DrawOnGlass = False
        Me.TerFontSizeVal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerFontSizeVal.ForeColor = System.Drawing.Color.White
        Me.TerFontSizeVal.Image = Nothing
        Me.TerFontSizeVal.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.TerFontSizeVal.LineSize = 1
        Me.TerFontSizeVal.Location = New System.Drawing.Point(312, 66)
        Me.TerFontSizeVal.Name = "TerFontSizeVal"
        Me.TerFontSizeVal.Size = New System.Drawing.Size(34, 24)
        Me.TerFontSizeVal.TabIndex = 132
        Me.TerFontSizeVal.UseVisualStyleBackColor = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 99
        Me.PictureBox5.TabStop = False
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(36, 36)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(54, 24)
        Me.Label61.TabIndex = 97
        Me.Label61.Text = "Weight:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(6, 36)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox8.TabIndex = 100
        Me.PictureBox8.TabStop = False
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(36, 66)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(54, 24)
        Me.Label35.TabIndex = 103
        Me.Label35.Text = "Size:"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(6, 66)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox9.TabIndex = 101
        Me.PictureBox9.TabStop = False
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.Transparent
        Me.Label59.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(36, 6)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(54, 24)
        Me.Label59.TabIndex = 84
        Me.Label59.Text = "Font:"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.GroupBox34)
        Me.TabPage3.Location = New System.Drawing.Point(104, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(363, 446)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Cursor"
        '
        'GroupBox34
        '
        Me.GroupBox34.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox34.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox34.Controls.Add(Me.PictureBox10)
        Me.GroupBox34.Controls.Add(Me.TerCursorHeightVal)
        Me.GroupBox34.Controls.Add(Me.Label60)
        Me.GroupBox34.Controls.Add(Me.PictureBox11)
        Me.GroupBox34.Controls.Add(Me.Label14)
        Me.GroupBox34.Controls.Add(Me.TerCursorHeightBar)
        Me.GroupBox34.Controls.Add(Me.TerCursorStyle)
        Me.GroupBox34.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox34.Name = "GroupBox34"
        Me.GroupBox34.Size = New System.Drawing.Size(351, 67)
        Me.GroupBox34.TabIndex = 99
        '
        'PictureBox10
        '
        Me.PictureBox10.Image = CType(resources.GetObject("PictureBox10.Image"), System.Drawing.Image)
        Me.PictureBox10.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox10.TabIndex = 104
        Me.PictureBox10.TabStop = False
        '
        'TerCursorHeightVal
        '
        Me.TerCursorHeightVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerCursorHeightVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerCursorHeightVal.DrawOnGlass = False
        Me.TerCursorHeightVal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerCursorHeightVal.ForeColor = System.Drawing.Color.White
        Me.TerCursorHeightVal.Image = Nothing
        Me.TerCursorHeightVal.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.TerCursorHeightVal.LineSize = 1
        Me.TerCursorHeightVal.Location = New System.Drawing.Point(312, 36)
        Me.TerCursorHeightVal.Name = "TerCursorHeightVal"
        Me.TerCursorHeightVal.Size = New System.Drawing.Size(34, 24)
        Me.TerCursorHeightVal.TabIndex = 133
        Me.TerCursorHeightVal.UseVisualStyleBackColor = False
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.Transparent
        Me.Label60.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(36, 36)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(54, 24)
        Me.Label60.TabIndex = 111
        Me.Label60.Text = "Size:"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox11
        '
        Me.PictureBox11.Image = CType(resources.GetObject("PictureBox11.Image"), System.Drawing.Image)
        Me.PictureBox11.Location = New System.Drawing.Point(6, 36)
        Me.PictureBox11.Name = "PictureBox11"
        Me.PictureBox11.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox11.TabIndex = 105
        Me.PictureBox11.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(36, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(54, 24)
        Me.Label14.TabIndex = 109
        Me.Label14.Text = "Style:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.GroupBox12)
        Me.TabPage4.Location = New System.Drawing.Point(104, 4)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(363, 446)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Background"
        '
        'GroupBox12
        '
        Me.GroupBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox12.Controls.Add(Me.TerOpacityVal)
        Me.GroupBox12.Controls.Add(Me.TerOpacityBar)
        Me.GroupBox12.Controls.Add(Me.TerImageOpacityVal)
        Me.GroupBox12.Controls.Add(Me.PictureBox13)
        Me.GroupBox12.Controls.Add(Me.PictureBox40)
        Me.GroupBox12.Controls.Add(Me.PictureBox16)
        Me.GroupBox12.Controls.Add(Me.TerAcrylic)
        Me.GroupBox12.Controls.Add(Me.Button5)
        Me.GroupBox12.Controls.Add(Me.TerBackImage)
        Me.GroupBox12.Controls.Add(Me.Label57)
        Me.GroupBox12.Controls.Add(Me.Button16)
        Me.GroupBox12.Controls.Add(Me.Label2)
        Me.GroupBox12.Controls.Add(Me.TerImageOpacity)
        Me.GroupBox12.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(351, 122)
        Me.GroupBox12.TabIndex = 100
        '
        'TerOpacityVal
        '
        Me.TerOpacityVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerOpacityVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerOpacityVal.DrawOnGlass = False
        Me.TerOpacityVal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerOpacityVal.ForeColor = System.Drawing.Color.White
        Me.TerOpacityVal.Image = Nothing
        Me.TerOpacityVal.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.TerOpacityVal.LineSize = 1
        Me.TerOpacityVal.Location = New System.Drawing.Point(311, 91)
        Me.TerOpacityVal.Name = "TerOpacityVal"
        Me.TerOpacityVal.Size = New System.Drawing.Size(34, 24)
        Me.TerOpacityVal.TabIndex = 135
        Me.TerOpacityVal.UseVisualStyleBackColor = False
        '
        'TerImageOpacityVal
        '
        Me.TerImageOpacityVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerImageOpacityVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.TerImageOpacityVal.DrawOnGlass = False
        Me.TerImageOpacityVal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerImageOpacityVal.ForeColor = System.Drawing.Color.White
        Me.TerImageOpacityVal.Image = Nothing
        Me.TerImageOpacityVal.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.TerImageOpacityVal.LineSize = 1
        Me.TerImageOpacityVal.Location = New System.Drawing.Point(311, 62)
        Me.TerImageOpacityVal.Name = "TerImageOpacityVal"
        Me.TerImageOpacityVal.Size = New System.Drawing.Size(34, 24)
        Me.TerImageOpacityVal.TabIndex = 134
        Me.TerImageOpacityVal.UseVisualStyleBackColor = False
        '
        'PictureBox13
        '
        Me.PictureBox13.Image = CType(resources.GetObject("PictureBox13.Image"), System.Drawing.Image)
        Me.PictureBox13.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox13.Name = "PictureBox13"
        Me.PictureBox13.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox13.TabIndex = 106
        Me.PictureBox13.TabStop = False
        '
        'PictureBox16
        '
        Me.PictureBox16.Image = CType(resources.GetObject("PictureBox16.Image"), System.Drawing.Image)
        Me.PictureBox16.Location = New System.Drawing.Point(6, 62)
        Me.PictureBox16.Name = "PictureBox16"
        Me.PictureBox16.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox16.TabIndex = 126
        Me.PictureBox16.TabStop = False
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.Transparent
        Me.Label57.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(36, 62)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(65, 24)
        Me.Label57.TabIndex = 119
        Me.Label57.Text = "Opacity:"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FontDialog1
        '
        Me.FontDialog1.FixedPitchOnly = True
        Me.FontDialog1.ShowEffects = False
        '
        'WindowsTerminal
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1008, 646)
        Me.Controls.Add(Me.GroupBox22)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.GroupBox13)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WindowsTerminal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Windows Terminal"
        Me.TerThemesContainer.ResumeLayout(False)
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox22.ResumeLayout(False)
        CType(Me.PictureBox27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox40, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox13.ResumeLayout(False)
        CType(Me.PictureBox25, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.PictureBox37, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox34.ResumeLayout(False)
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox22 As UI.WP.GroupBox
    Friend WithEvents Button15 As UI.WP.Button
    Friend WithEvents PictureBox27 As PictureBox
    Friend WithEvents Label141 As Label
    Friend WithEvents Button16 As UI.WP.Button
    Friend WithEvents TerBackImage As UI.WP.TextBox
    Friend WithEvents TerImageOpacity As UI.WP.Trackbar
    Friend WithEvents Button12 As UI.WP.Button
    Friend WithEvents TerSchemes As UI.WP.ComboBox
    Friend WithEvents TerCursor As UI.Controllers.ColorItem
    Friend WithEvents TerWhiteB As UI.Controllers.ColorItem
    Friend WithEvents Label150 As Label
    Friend WithEvents TerBlue As UI.Controllers.ColorItem
    Friend WithEvents Label151 As Label
    Friend WithEvents TerSelection As UI.Controllers.ColorItem
    Friend WithEvents Label152 As Label
    Friend WithEvents Label147 As Label
    Friend WithEvents TerWhite As UI.Controllers.ColorItem
    Friend WithEvents TerForeground As UI.Controllers.ColorItem
    Friend WithEvents TerCyanB As UI.Controllers.ColorItem
    Friend WithEvents Label145 As Label
    Friend WithEvents TerCyan As UI.Controllers.ColorItem
    Friend WithEvents TerGreen As UI.Controllers.ColorItem
    Friend WithEvents Label144 As Label
    Friend WithEvents TerBackground As UI.Controllers.ColorItem
    Friend WithEvents TerYellow As UI.Controllers.ColorItem
    Friend WithEvents TerGreenB As UI.Controllers.ColorItem
    Friend WithEvents Label143 As Label
    Friend WithEvents Label149 As Label
    Friend WithEvents TerBlack As UI.Controllers.ColorItem
    Friend WithEvents TerYellowB As UI.Controllers.ColorItem
    Friend WithEvents Label142 As Label
    Friend WithEvents TerBlackB As UI.Controllers.ColorItem
    Friend WithEvents TerPurple As UI.Controllers.ColorItem
    Friend WithEvents Label140 As Label
    Friend WithEvents TerPurpleB As UI.Controllers.ColorItem
    Friend WithEvents TerBlueB As UI.Controllers.ColorItem
    Friend WithEvents Label146 As Label
    Friend WithEvents Label148 As Label
    Friend WithEvents TerRedB As UI.Controllers.ColorItem
    Friend WithEvents TerRed As UI.Controllers.ColorItem
    Friend WithEvents TerCursorHeightBar As UI.WP.Trackbar
    Friend WithEvents TerCursorStyle As UI.WP.ComboBox
    Friend WithEvents GroupBox13 As UI.WP.GroupBox
    Friend WithEvents Button14 As UI.WP.Button
    Friend WithEvents PictureBox25 As PictureBox
    Friend WithEvents Label155 As Label
    Friend WithEvents TerProfiles As UI.WP.ComboBox
    Friend WithEvents Button13 As UI.WP.Button
    Friend WithEvents TerFontSizeBar As UI.WP.Trackbar
    Friend WithEvents TerFontWeight As UI.WP.ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
    Friend WithEvents Button10 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Terminal1 As UI.Simulation.WinTerminal
    Friend WithEvents Terminal2 As UI.Simulation.WinTerminal
    Friend WithEvents Label9 As Label
    Friend WithEvents TerTabInactive As UI.Controllers.ColorItem
    Friend WithEvents TerTabActive As UI.Controllers.ColorItem
    Friend WithEvents Label10 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TerTitlebarInactive As UI.Controllers.ColorItem
    Friend WithEvents TerTitlebarActive As UI.Controllers.ColorItem
    Friend WithEvents Label6 As Label
    Friend WithEvents Button3 As UI.WP.Button
    Friend WithEvents TerThemes As UI.WP.ComboBox
    Friend WithEvents TerMode As UI.WP.Toggle
    Friend WithEvents TerEditThemeName As UI.WP.Button
    Friend WithEvents TerThemesContainer As Panel
    Friend WithEvents Button4 As UI.WP.Button
    Friend WithEvents PictureBox40 As PictureBox
    Friend WithEvents TerAcrylic As UI.WP.CheckBox
    Friend WithEvents TerOpacityBar As UI.WP.Trackbar
    Friend WithEvents Button5 As UI.WP.Button
    Friend WithEvents TerEnabled As UI.WP.Toggle
    Friend WithEvents ImgDlg As OpenFileDialog
    Friend WithEvents Button8 As UI.WP.Button
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
    Friend WithEvents Button9 As UI.WP.Button
    Friend WithEvents Button11 As UI.WP.Button
    Friend WithEvents SaveJSONDlg As SaveFileDialog
    Friend WithEvents OpenWPTHDlg As OpenFileDialog
    Friend WithEvents OpenJSONDlg As OpenFileDialog
    Friend WithEvents Button17 As UI.WP.Button
    Friend WithEvents Button18 As UI.WP.Button
    Friend WithEvents Button19 As UI.WP.Button
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents Button6 As UI.WP.Button
    Friend WithEvents Button20 As UI.WP.Button
    Friend WithEvents Button21 As UI.WP.Button
    Friend WithEvents GroupBox3 As UI.WP.GroupBox
    Friend WithEvents Separator2 As UI.WP.SeparatorH
    Friend WithEvents Label11 As Label
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents Button22 As UI.WP.Button
    Friend WithEvents TabControl1 As UI.WP.TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GroupBox4 As UI.WP.GroupBox
    Friend WithEvents PictureBox23 As PictureBox
    Friend WithEvents PictureBox22 As PictureBox
    Friend WithEvents Separator3 As UI.WP.SeparatorH
    Friend WithEvents PictureBox21 As PictureBox
    Friend WithEvents PictureBox20 As PictureBox
    Friend WithEvents PictureBox19 As PictureBox
    Friend WithEvents PictureBox15 As PictureBox
    Friend WithEvents PictureBox14 As PictureBox
    Friend WithEvents Label19 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox5 As UI.WP.GroupBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents Label61 As Label
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents Label35 As Label
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents Label59 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents GroupBox34 As UI.WP.GroupBox
    Friend WithEvents Label60 As Label
    Friend WithEvents PictureBox11 As PictureBox
    Friend WithEvents Label14 As Label
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents GroupBox12 As UI.WP.GroupBox
    Friend WithEvents PictureBox13 As PictureBox
    Friend WithEvents PictureBox16 As PictureBox
    Friend WithEvents Label57 As Label
    Friend WithEvents PictureBox29 As PictureBox
    Friend WithEvents PictureBox30 As PictureBox
    Friend WithEvents PictureBox31 As PictureBox
    Friend WithEvents PictureBox34 As PictureBox
    Friend WithEvents PictureBox26 As PictureBox
    Friend WithEvents PictureBox28 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents GroupBox2 As UI.WP.GroupBox
    Friend WithEvents PictureBox37 As PictureBox
    Friend WithEvents Label20 As Label
    Friend WithEvents TerFontSizeVal As UI.WP.Button
    Friend WithEvents TerCursorHeightVal As UI.WP.Button
    Friend WithEvents TerOpacityVal As UI.WP.Button
    Friend WithEvents TerImageOpacityVal As UI.WP.Button
    Friend WithEvents PictureBox10 As PictureBox
    Friend WithEvents TerFontName As Label
    Friend WithEvents Button23 As UI.WP.Button
    Friend WithEvents FontDialog1 As FontDialog
End Class
