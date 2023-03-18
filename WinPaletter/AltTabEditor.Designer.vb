<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AltTabEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AltTabEditor))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.XenonGroupBox4 = New WinPaletter.XenonGroupBox()
        Me.EP_Alert = New WinPaletter.XenonAlertBox()
        Me.opacity_btn = New WinPaletter.XenonButton()
        Me.XenonTrackbar1 = New WinPaletter.XenonTrackbar()
        Me.PictureBox13 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RetroPanel1 = New WinPaletter.RetroPanel()
        Me.RetroLabel1 = New WinPaletter.RetroLabel()
        Me.XenonGroupBox3 = New WinPaletter.XenonGroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.XenonRadioImage2 = New WinPaletter.XenonRadioImage()
        Me.XenonRadioImage1 = New WinPaletter.XenonRadioImage()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.previewContainer = New WinPaletter.XenonGroupBox()
        Me.tabs_preview_1 = New WinPaletter.TablessControl()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.pnl_preview1 = New System.Windows.Forms.Panel()
        Me.XenonWinElement1 = New WinPaletter.XenonWinElement()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Classic_Preview1 = New System.Windows.Forms.Panel()
        Me.RetroPanelRaised1 = New WinPaletter.RetroPanelRaised()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox41 = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.XenonButton10 = New WinPaletter.XenonButton()
        Me.XenonButton7 = New WinPaletter.XenonButton()
        Me.XenonButton8 = New WinPaletter.XenonButton()
        Me.XenonGroupBox12 = New WinPaletter.XenonGroupBox()
        Me.XenonButton9 = New WinPaletter.XenonButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.XenonButton11 = New WinPaletter.XenonButton()
        Me.XenonButton12 = New WinPaletter.XenonButton()
        Me.AltTabEnabled = New WinPaletter.XenonToggle()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.XenonAlertBox1 = New WinPaletter.XenonAlertBox()
        Me.XenonAlertBox2 = New WinPaletter.XenonAlertBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox4.SuspendLayout()
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RetroPanel1.SuspendLayout()
        Me.XenonGroupBox3.SuspendLayout()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.previewContainer.SuspendLayout()
        Me.tabs_preview_1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.pnl_preview1.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.Classic_Preview1.SuspendLayout()
        Me.RetroPanelRaised1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox12.SuspendLayout()
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(105, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(40, 40)
        Me.Panel1.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Location = New System.Drawing.Point(2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(36, 36)
        Me.Panel2.TabIndex = 5
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = Global.WinPaletter.My.Resources.Resources.ActiveApp_Taskbar
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 30)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = Global.WinPaletter.My.Resources.Resources.ActiveApp_Taskbar
        Me.PictureBox2.Location = New System.Drawing.Point(149, 20)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'XenonGroupBox4
        '
        Me.XenonGroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox4.Controls.Add(Me.EP_Alert)
        Me.XenonGroupBox4.Controls.Add(Me.opacity_btn)
        Me.XenonGroupBox4.Controls.Add(Me.XenonTrackbar1)
        Me.XenonGroupBox4.Controls.Add(Me.PictureBox13)
        Me.XenonGroupBox4.Controls.Add(Me.Label4)
        Me.XenonGroupBox4.Location = New System.Drawing.Point(12, 167)
        Me.XenonGroupBox4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonGroupBox4.Name = "XenonGroupBox4"
        Me.XenonGroupBox4.Size = New System.Drawing.Size(330, 128)
        Me.XenonGroupBox4.TabIndex = 213
        '
        'EP_Alert
        '
        Me.EP_Alert.AlertStyle = WinPaletter.XenonAlertBox.Style.Simple
        Me.EP_Alert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EP_Alert.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.EP_Alert.CenterText = True
        Me.EP_Alert.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.EP_Alert.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.EP_Alert.Image = Nothing
        Me.EP_Alert.Location = New System.Drawing.Point(4, 72)
        Me.EP_Alert.Name = "EP_Alert"
        Me.EP_Alert.Size = New System.Drawing.Size(322, 52)
        Me.EP_Alert.TabIndex = 214
        Me.EP_Alert.TabStop = False
        Me.EP_Alert.Text = "Opacity controling can be allowed in Windows 11 too if ExplorerPatcher is install" &
    "ed"
        '
        'opacity_btn
        '
        Me.opacity_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.opacity_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.opacity_btn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.opacity_btn.ForeColor = System.Drawing.Color.White
        Me.opacity_btn.Image = Nothing
        Me.opacity_btn.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.opacity_btn.LineSize = 1
        Me.opacity_btn.Location = New System.Drawing.Point(290, 43)
        Me.opacity_btn.Name = "opacity_btn"
        Me.opacity_btn.Size = New System.Drawing.Size(34, 24)
        Me.opacity_btn.TabIndex = 131
        Me.opacity_btn.UseVisualStyleBackColor = False
        '
        'XenonTrackbar1
        '
        Me.XenonTrackbar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTrackbar1.LargeChange = 10
        Me.XenonTrackbar1.Location = New System.Drawing.Point(7, 46)
        Me.XenonTrackbar1.Maximum = 100
        Me.XenonTrackbar1.Minimum = 0
        Me.XenonTrackbar1.Name = "XenonTrackbar1"
        Me.XenonTrackbar1.Size = New System.Drawing.Size(277, 19)
        Me.XenonTrackbar1.SmallChange = 1
        Me.XenonTrackbar1.TabIndex = 130
        Me.XenonTrackbar1.Value = 17
        '
        'PictureBox13
        '
        Me.PictureBox13.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox13.Image = CType(resources.GetObject("PictureBox13.Image"), System.Drawing.Image)
        Me.PictureBox13.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox13.Name = "PictureBox13"
        Me.PictureBox13.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox13.TabIndex = 1
        Me.PictureBox13.TabStop = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(40, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(287, 35)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Opacity (for Windows 10)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RetroPanel1
        '
        Me.RetroPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroPanel1.ButtonDkShadow = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.RetroPanel1.ButtonHilight = System.Drawing.Color.White
        Me.RetroPanel1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.RetroPanel1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroPanel1.Controls.Add(Me.RetroLabel1)
        Me.RetroPanel1.Flat = False
        Me.RetroPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroPanel1.ForeColor = System.Drawing.Color.Black
        Me.RetroPanel1.Location = New System.Drawing.Point(14, 72)
        Me.RetroPanel1.Name = "RetroPanel1"
        Me.RetroPanel1.Size = New System.Drawing.Size(302, 29)
        Me.RetroPanel1.Style2 = True
        Me.RetroPanel1.TabIndex = 0
        '
        'RetroLabel1
        '
        Me.RetroLabel1.BackColor = System.Drawing.Color.Transparent
        Me.RetroLabel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RetroLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel1.ForeColor = System.Drawing.Color.Black
        Me.RetroLabel1.Location = New System.Drawing.Point(0, 0)
        Me.RetroLabel1.Name = "RetroLabel1"
        Me.RetroLabel1.Size = New System.Drawing.Size(302, 29)
        Me.RetroLabel1.TabIndex = 0
        Me.RetroLabel1.Text = "Application"
        Me.RetroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.Label2)
        Me.XenonGroupBox3.Controls.Add(Me.Label1)
        Me.XenonGroupBox3.Controls.Add(Me.XenonRadioImage2)
        Me.XenonGroupBox3.Controls.Add(Me.XenonRadioImage1)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox11)
        Me.XenonGroupBox3.Controls.Add(Me.Label3)
        Me.XenonGroupBox3.Location = New System.Drawing.Point(12, 54)
        Me.XenonGroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(330, 110)
        Me.XenonGroupBox3.TabIndex = 212
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(163, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 18)
        Me.Label2.TabIndex = 113
        Me.Label2.Text = "Classic NT"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(82, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 18)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "Default"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'XenonRadioImage2
        '
        Me.XenonRadioImage2.Checked = False
        Me.XenonRadioImage2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioImage2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioImage2.Image = Nothing
        Me.XenonRadioImage2.Location = New System.Drawing.Point(163, 43)
        Me.XenonRadioImage2.Name = "XenonRadioImage2"
        Me.XenonRadioImage2.ShowText = False
        Me.XenonRadioImage2.Size = New System.Drawing.Size(69, 40)
        Me.XenonRadioImage2.TabIndex = 3
        '
        'XenonRadioImage1
        '
        Me.XenonRadioImage1.Checked = False
        Me.XenonRadioImage1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioImage1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioImage1.Image = Nothing
        Me.XenonRadioImage1.Location = New System.Drawing.Point(82, 43)
        Me.XenonRadioImage1.Name = "XenonRadioImage1"
        Me.XenonRadioImage1.ShowText = False
        Me.XenonRadioImage1.Size = New System.Drawing.Size(69, 40)
        Me.XenonRadioImage1.TabIndex = 2
        '
        'PictureBox11
        '
        Me.PictureBox11.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox11.Image = CType(resources.GetObject("PictureBox11.Image"), System.Drawing.Image)
        Me.PictureBox11.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox11.Name = "PictureBox11"
        Me.PictureBox11.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox11.TabIndex = 1
        Me.PictureBox11.TabStop = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(40, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(287, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Style"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'previewContainer
        '
        Me.previewContainer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.previewContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.previewContainer.Controls.Add(Me.tabs_preview_1)
        Me.previewContainer.Controls.Add(Me.PictureBox41)
        Me.previewContainer.Controls.Add(Me.Label19)
        Me.previewContainer.Location = New System.Drawing.Point(345, 54)
        Me.previewContainer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.previewContainer.Name = "previewContainer"
        Me.previewContainer.Padding = New System.Windows.Forms.Padding(1)
        Me.previewContainer.Size = New System.Drawing.Size(536, 340)
        Me.previewContainer.TabIndex = 211
        '
        'tabs_preview_1
        '
        Me.tabs_preview_1.Controls.Add(Me.TabPage6)
        Me.tabs_preview_1.Controls.Add(Me.TabPage7)
        Me.tabs_preview_1.Location = New System.Drawing.Point(4, 39)
        Me.tabs_preview_1.Name = "tabs_preview_1"
        Me.tabs_preview_1.SelectedIndex = 0
        Me.tabs_preview_1.Size = New System.Drawing.Size(528, 297)
        Me.tabs_preview_1.TabIndex = 120
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage6.Controls.Add(Me.pnl_preview1)
        Me.TabPage6.Location = New System.Drawing.Point(4, 24)
        Me.TabPage6.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(520, 269)
        Me.TabPage6.TabIndex = 0
        Me.TabPage6.Text = "0"
        '
        'pnl_preview1
        '
        Me.pnl_preview1.BackColor = System.Drawing.Color.Black
        Me.pnl_preview1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_preview1.Controls.Add(Me.XenonWinElement1)
        Me.pnl_preview1.Location = New System.Drawing.Point(0, 0)
        Me.pnl_preview1.Name = "pnl_preview1"
        Me.pnl_preview1.Size = New System.Drawing.Size(528, 297)
        Me.pnl_preview1.TabIndex = 2
        '
        'XenonWinElement1
        '
        Me.XenonWinElement1.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.XenonWinElement1.ActionCenterButton_Normal = System.Drawing.Color.Empty
        Me.XenonWinElement1.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.XenonWinElement1.AppBackground = System.Drawing.Color.Empty
        Me.XenonWinElement1.AppUnderline = System.Drawing.Color.Empty
        Me.XenonWinElement1.BackColor2 = System.Drawing.Color.Empty
        Me.XenonWinElement1.BackColorAlpha = 130
        Me.XenonWinElement1.BlurPower = 8
        Me.XenonWinElement1.DarkMode = True
        Me.XenonWinElement1.LinkColor = System.Drawing.Color.Empty
        Me.XenonWinElement1.Location = New System.Drawing.Point(39, 73)
        Me.XenonWinElement1.Name = "XenonWinElement1"
        Me.XenonWinElement1.NoisePower = 0.15!
        Me.XenonWinElement1.SearchBoxAccent = System.Drawing.Color.Empty
        Me.XenonWinElement1.Shadow = True
        Me.XenonWinElement1.Size = New System.Drawing.Size(450, 150)
        Me.XenonWinElement1.StartColor = System.Drawing.Color.Empty
        Me.XenonWinElement1.Style = WinPaletter.XenonWinElement.Styles.AltTab11
        Me.XenonWinElement1.TabIndex = 0
        Me.XenonWinElement1.Transparency = True
        Me.XenonWinElement1.UseWin11ORB_WithWin10 = False
        Me.XenonWinElement1.UseWin11RoundedCorners_WithWin10_Level1 = False
        Me.XenonWinElement1.UseWin11RoundedCorners_WithWin10_Level2 = False
        Me.XenonWinElement1.Win7ColorBal = 0
        Me.XenonWinElement1.Win7GlowBal = 0
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage7.Controls.Add(Me.Classic_Preview1)
        Me.TabPage7.Location = New System.Drawing.Point(4, 24)
        Me.TabPage7.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(520, 269)
        Me.TabPage7.TabIndex = 1
        Me.TabPage7.Text = "1"
        '
        'Classic_Preview1
        '
        Me.Classic_Preview1.BackColor = System.Drawing.Color.Black
        Me.Classic_Preview1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Classic_Preview1.Controls.Add(Me.RetroPanelRaised1)
        Me.Classic_Preview1.Location = New System.Drawing.Point(0, 0)
        Me.Classic_Preview1.Name = "Classic_Preview1"
        Me.Classic_Preview1.Size = New System.Drawing.Size(528, 297)
        Me.Classic_Preview1.TabIndex = 3
        '
        'RetroPanelRaised1
        '
        Me.RetroPanelRaised1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RetroPanelRaised1.ButtonDkShadow = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.RetroPanelRaised1.ButtonHilight = System.Drawing.Color.White
        Me.RetroPanelRaised1.ButtonLight = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.RetroPanelRaised1.ButtonShadow = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RetroPanelRaised1.Controls.Add(Me.PictureBox3)
        Me.RetroPanelRaised1.Controls.Add(Me.Panel1)
        Me.RetroPanelRaised1.Controls.Add(Me.PictureBox2)
        Me.RetroPanelRaised1.Controls.Add(Me.RetroPanel1)
        Me.RetroPanelRaised1.Flat = False
        Me.RetroPanelRaised1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroPanelRaised1.ForeColor = System.Drawing.Color.Black
        Me.RetroPanelRaised1.Location = New System.Drawing.Point(99, 92)
        Me.RetroPanelRaised1.Name = "RetroPanelRaised1"
        Me.RetroPanelRaised1.Size = New System.Drawing.Size(330, 112)
        Me.RetroPanelRaised1.Style2 = True
        Me.RetroPanelRaised1.TabIndex = 2
        Me.RetroPanelRaised1.UseItAsWin7Taskbar = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Image = Global.WinPaletter.My.Resources.Resources.ActiveApp_Taskbar
        Me.PictureBox3.Location = New System.Drawing.Point(190, 20)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 5
        Me.PictureBox3.TabStop = False
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
        Me.Label19.Size = New System.Drawing.Size(270, 31)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "Preview"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton10
        '
        Me.XenonButton10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton10.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton10.ForeColor = System.Drawing.Color.White
        Me.XenonButton10.Image = CType(resources.GetObject("XenonButton10.Image"), System.Drawing.Image)
        Me.XenonButton10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton10.LineColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.XenonButton10.LineSize = 1
        Me.XenonButton10.Location = New System.Drawing.Point(563, 405)
        Me.XenonButton10.Name = "XenonButton10"
        Me.XenonButton10.Size = New System.Drawing.Size(124, 30)
        Me.XenonButton10.TabIndex = 210
        Me.XenonButton10.Text = "Quick apply"
        Me.XenonButton10.UseVisualStyleBackColor = False
        '
        'XenonButton7
        '
        Me.XenonButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = Nothing
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(477, 405)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(80, 30)
        Me.XenonButton7.TabIndex = 209
        Me.XenonButton7.Text = "Cancel"
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonButton8
        '
        Me.XenonButton8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton8.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton8.ForeColor = System.Drawing.Color.White
        Me.XenonButton8.Image = CType(resources.GetObject("XenonButton8.Image"), System.Drawing.Image)
        Me.XenonButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton8.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XenonButton8.LineSize = 1
        Me.XenonButton8.Location = New System.Drawing.Point(693, 405)
        Me.XenonButton8.Name = "XenonButton8"
        Me.XenonButton8.Size = New System.Drawing.Size(188, 30)
        Me.XenonButton8.TabIndex = 208
        Me.XenonButton8.Text = "Load into current theme"
        Me.XenonButton8.UseVisualStyleBackColor = False
        '
        'XenonGroupBox12
        '
        Me.XenonGroupBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox12.Controls.Add(Me.XenonButton9)
        Me.XenonGroupBox12.Controls.Add(Me.Label12)
        Me.XenonGroupBox12.Controls.Add(Me.XenonButton11)
        Me.XenonGroupBox12.Controls.Add(Me.XenonButton12)
        Me.XenonGroupBox12.Controls.Add(Me.AltTabEnabled)
        Me.XenonGroupBox12.Controls.Add(Me.checker_img)
        Me.XenonGroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.XenonGroupBox12.Name = "XenonGroupBox12"
        Me.XenonGroupBox12.Size = New System.Drawing.Size(869, 39)
        Me.XenonGroupBox12.TabIndex = 201
        '
        'XenonButton9
        '
        Me.XenonButton9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.XenonButton9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton9.ForeColor = System.Drawing.Color.White
        Me.XenonButton9.Image = CType(resources.GetObject("XenonButton9.Image"), System.Drawing.Image)
        Me.XenonButton9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton9.LineColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.XenonButton9.LineSize = 1
        Me.XenonButton9.Location = New System.Drawing.Point(222, 5)
        Me.XenonButton9.Name = "XenonButton9"
        Me.XenonButton9.Size = New System.Drawing.Size(126, 29)
        Me.XenonButton9.TabIndex = 112
        Me.XenonButton9.Text = "Current applied"
        Me.XenonButton9.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 31)
        Me.Label12.TabIndex = 111
        Me.Label12.Text = "Open from:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton11
        '
        Me.XenonButton11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.XenonButton11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton11.ForeColor = System.Drawing.Color.White
        Me.XenonButton11.Image = CType(resources.GetObject("XenonButton11.Image"), System.Drawing.Image)
        Me.XenonButton11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton11.LineColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.XenonButton11.LineSize = 1
        Me.XenonButton11.Location = New System.Drawing.Point(85, 5)
        Me.XenonButton11.Name = "XenonButton11"
        Me.XenonButton11.Size = New System.Drawing.Size(135, 29)
        Me.XenonButton11.TabIndex = 110
        Me.XenonButton11.Text = "WinPaletter theme"
        Me.XenonButton11.UseVisualStyleBackColor = False
        '
        'XenonButton12
        '
        Me.XenonButton12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.XenonButton12.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton12.ForeColor = System.Drawing.Color.White
        Me.XenonButton12.Image = Nothing
        Me.XenonButton12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton12.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.XenonButton12.LineSize = 1
        Me.XenonButton12.Location = New System.Drawing.Point(351, 5)
        Me.XenonButton12.Name = "XenonButton12"
        Me.XenonButton12.Size = New System.Drawing.Size(130, 29)
        Me.XenonButton12.TabIndex = 108
        Me.XenonButton12.Text = "Default Windows"
        Me.XenonButton12.UseVisualStyleBackColor = False
        '
        'AltTabEnabled
        '
        Me.AltTabEnabled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AltTabEnabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.AltTabEnabled.Checked = False
        Me.AltTabEnabled.DarkLight_Toggler = False
        Me.AltTabEnabled.Location = New System.Drawing.Point(822, 9)
        Me.AltTabEnabled.Name = "AltTabEnabled"
        Me.AltTabEnabled.Size = New System.Drawing.Size(40, 20)
        Me.AltTabEnabled.TabIndex = 85
        '
        'checker_img
        '
        Me.checker_img.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checker_img.Image = Global.WinPaletter.My.Resources.Resources.checker_disabled
        Me.checker_img.Location = New System.Drawing.Point(781, 4)
        Me.checker_img.Name = "checker_img"
        Me.checker_img.Size = New System.Drawing.Size(35, 31)
        Me.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.checker_img.TabIndex = 83
        Me.checker_img.TabStop = False
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.XenonAlertBox.Style.Warning
        Me.XenonAlertBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.XenonAlertBox1.CenterText = True
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Nothing
        Me.XenonAlertBox1.Location = New System.Drawing.Point(16, 405)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(455, 30)
        Me.XenonAlertBox1.TabIndex = 215
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = "Applying in Windows 7 may require a device restart"
        '
        'XenonAlertBox2
        '
        Me.XenonAlertBox2.AlertStyle = WinPaletter.XenonAlertBox.Style.Simple
        Me.XenonAlertBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonAlertBox2.CenterText = True
        Me.XenonAlertBox2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox2.Image = Nothing
        Me.XenonAlertBox2.Location = New System.Drawing.Point(16, 301)
        Me.XenonAlertBox2.Name = "XenonAlertBox2"
        Me.XenonAlertBox2.Size = New System.Drawing.Size(322, 30)
        Me.XenonAlertBox2.TabIndex = 216
        Me.XenonAlertBox2.TabStop = False
        Me.XenonAlertBox2.Text = "Sometimes, you should logoff and logon to apply effects"
        '
        'AltTabEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(893, 447)
        Me.Controls.Add(Me.XenonAlertBox2)
        Me.Controls.Add(Me.XenonAlertBox1)
        Me.Controls.Add(Me.XenonGroupBox4)
        Me.Controls.Add(Me.XenonGroupBox3)
        Me.Controls.Add(Me.previewContainer)
        Me.Controls.Add(Me.XenonButton10)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonButton8)
        Me.Controls.Add(Me.XenonGroupBox12)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AltTabEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Windows Switcher (Alt+Tab)"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox4.ResumeLayout(False)
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RetroPanel1.ResumeLayout(False)
        Me.XenonGroupBox3.ResumeLayout(False)
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.previewContainer.ResumeLayout(False)
        Me.tabs_preview_1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.pnl_preview1.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.Classic_Preview1.ResumeLayout(False)
        Me.RetroPanelRaised1.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox12.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents XenonGroupBox12 As XenonGroupBox
    Friend WithEvents XenonButton9 As XenonButton
    Friend WithEvents Label12 As Label
    Friend WithEvents XenonButton11 As XenonButton
    Friend WithEvents XenonButton12 As XenonButton
    Friend WithEvents AltTabEnabled As XenonToggle
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents XenonButton10 As XenonButton
    Friend WithEvents XenonButton7 As XenonButton
    Friend WithEvents XenonButton8 As XenonButton
    Friend WithEvents previewContainer As XenonGroupBox
    Friend WithEvents tabs_preview_1 As TablessControl
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents pnl_preview1 As Panel
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents Classic_Preview1 As Panel
    Friend WithEvents PictureBox41 As PictureBox
    Friend WithEvents Label19 As Label
    Friend WithEvents XenonGroupBox4 As XenonGroupBox
    Friend WithEvents PictureBox13 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents XenonGroupBox3 As XenonGroupBox
    Friend WithEvents PictureBox11 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents opacity_btn As XenonButton
    Friend WithEvents XenonTrackbar1 As XenonTrackbar
    Friend WithEvents XenonRadioImage1 As XenonRadioImage
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents XenonRadioImage2 As XenonRadioImage
    Friend WithEvents XenonWinElement1 As XenonWinElement
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents RetroPanel1 As RetroPanel
    Friend WithEvents RetroLabel1 As RetroLabel
    Friend WithEvents RetroPanelRaised1 As RetroPanelRaised
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents EP_Alert As XenonAlertBox
    Friend WithEvents XenonAlertBox1 As XenonAlertBox
    Friend WithEvents XenonAlertBox2 As XenonAlertBox
End Class
