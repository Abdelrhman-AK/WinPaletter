<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AltTabEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AltTabEditor))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.AlertBox2 = New WinPaletter.UI.WP.AlertBox()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.GroupBox4 = New WinPaletter.UI.WP.GroupBox()
        Me.EP_Alert = New WinPaletter.UI.WP.AlertBox()
        Me.opacity_btn = New WinPaletter.UI.WP.Button()
        Me.Trackbar1 = New WinPaletter.UI.WP.Trackbar()
        Me.PictureBox13 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioImage2 = New WinPaletter.UI.WP.RadioImage()
        Me.RadioImage1 = New WinPaletter.UI.WP.RadioImage()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.previewContainer = New WinPaletter.UI.WP.GroupBox()
        Me.tabs_preview_1 = New WinPaletter.UI.WP.TablessControl()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.pnl_preview1 = New System.Windows.Forms.Panel()
        Me.WinElement1 = New WinPaletter.UI.Simulation.WinElement()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Classic_Preview1 = New System.Windows.Forms.Panel()
        Me.RetroPanelRaised1 = New WinPaletter.UI.Retro.PanelRaisedR()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.RetroPanel1 = New WinPaletter.UI.Retro.PanelR()
        Me.RetroLabel1 = New WinPaletter.UI.WP.LabelAlt()
        Me.PictureBox41 = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Button10 = New WinPaletter.UI.WP.Button()
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.Button8 = New WinPaletter.UI.WP.Button()
        Me.GroupBox12 = New WinPaletter.UI.WP.GroupBox()
        Me.Button9 = New WinPaletter.UI.WP.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button11 = New WinPaletter.UI.WP.Button()
        Me.Button12 = New WinPaletter.UI.WP.Button()
        Me.AltTabEnabled = New UI.WP.Toggle()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.previewContainer.SuspendLayout()
        Me.tabs_preview_1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.pnl_preview1.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.Classic_Preview1.SuspendLayout()
        Me.RetroPanelRaised1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RetroPanel1.SuspendLayout()
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox12.SuspendLayout()
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'AlertBox2
        '
        Me.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.AlertBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.AlertBox2.CenterText = False
        Me.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox2.Image = Nothing
        Me.AlertBox2.Location = New System.Drawing.Point(16, 289)
        Me.AlertBox2.Name = "AlertBox2"
        Me.AlertBox2.Size = New System.Drawing.Size(322, 20)
        Me.AlertBox2.TabIndex = 216
        Me.AlertBox2.TabStop = False
        Me.AlertBox2.Text = "Sometimes, you should logoff and logon to apply effects"
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.AlertBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = Nothing
        Me.AlertBox1.Location = New System.Drawing.Point(15, 413)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(324, 20)
        Me.AlertBox1.TabIndex = 215
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "Applying in Windows 7 may require a device restart"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox4.Controls.Add(Me.EP_Alert)
        Me.GroupBox4.Controls.Add(Me.opacity_btn)
        Me.GroupBox4.Controls.Add(Me.Trackbar1)
        Me.GroupBox4.Controls.Add(Me.PictureBox13)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 167)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(330, 116)
        Me.GroupBox4.TabIndex = 213
        '
        'EP_Alert
        '
        Me.EP_Alert.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.EP_Alert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EP_Alert.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.EP_Alert.CenterText = False
        Me.EP_Alert.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.EP_Alert.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.EP_Alert.Image = Nothing
        Me.EP_Alert.Location = New System.Drawing.Point(4, 72)
        Me.EP_Alert.Name = "EP_Alert"
        Me.EP_Alert.Size = New System.Drawing.Size(322, 40)
        Me.EP_Alert.TabIndex = 214
        Me.EP_Alert.TabStop = False
        Me.EP_Alert.Text = "Opacity controling can be allowed in Windows 11 too if ExplorerPatcher is install" &
    "ed"
        '
        'opacity_btn
        '
        Me.opacity_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.opacity_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.opacity_btn.DrawOnGlass = False
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
        'Trackbar1
        '
        Me.Trackbar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Trackbar1.LargeChange = 10
        Me.Trackbar1.Location = New System.Drawing.Point(7, 46)
        Me.Trackbar1.Maximum = 100
        Me.Trackbar1.Minimum = 0
        Me.Trackbar1.Name = "Trackbar1"
        Me.Trackbar1.Size = New System.Drawing.Size(277, 19)
        Me.Trackbar1.SmallChange = 1
        Me.Trackbar1.TabIndex = 130
        Me.Trackbar1.Value = 17
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
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.RadioImage2)
        Me.GroupBox3.Controls.Add(Me.RadioImage1)
        Me.GroupBox3.Controls.Add(Me.PictureBox11)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 54)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(330, 110)
        Me.GroupBox3.TabIndex = 212
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
        'RadioImage2
        '
        Me.RadioImage2.Checked = False
        Me.RadioImage2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage2.ForeColor = System.Drawing.Color.White
        Me.RadioImage2.Image = Nothing
        Me.RadioImage2.Location = New System.Drawing.Point(163, 43)
        Me.RadioImage2.Name = "RadioImage2"
        Me.RadioImage2.ShowText = False
        Me.RadioImage2.Size = New System.Drawing.Size(69, 40)
        Me.RadioImage2.TabIndex = 3
        '
        'RadioImage1
        '
        Me.RadioImage1.Checked = False
        Me.RadioImage1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage1.ForeColor = System.Drawing.Color.White
        Me.RadioImage1.Image = Nothing
        Me.RadioImage1.Location = New System.Drawing.Point(82, 43)
        Me.RadioImage1.Name = "RadioImage1"
        Me.RadioImage1.ShowText = False
        Me.RadioImage1.Size = New System.Drawing.Size(69, 40)
        Me.RadioImage1.TabIndex = 2
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
        Me.pnl_preview1.Controls.Add(Me.WinElement1)
        Me.pnl_preview1.Location = New System.Drawing.Point(0, 0)
        Me.pnl_preview1.Name = "pnl_preview1"
        Me.pnl_preview1.Size = New System.Drawing.Size(528, 297)
        Me.pnl_preview1.TabIndex = 2
        '
        'WinElement1
        '
        Me.WinElement1.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.WinElement1.ActionCenterButton_Normal = System.Drawing.Color.Empty
        Me.WinElement1.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.WinElement1.AppBackground = System.Drawing.Color.Empty
        Me.WinElement1.AppUnderline = System.Drawing.Color.Empty
        Me.WinElement1.BackColor = System.Drawing.Color.Transparent
        Me.WinElement1.BackColorAlpha = 130
        Me.WinElement1.Background = System.Drawing.Color.Empty
        Me.WinElement1.Background2 = System.Drawing.Color.Empty
        Me.WinElement1.BlurPower = 8
        Me.WinElement1.DarkMode = True
        Me.WinElement1.LinkColor = System.Drawing.Color.Empty
        Me.WinElement1.Location = New System.Drawing.Point(39, 73)
        Me.WinElement1.Name = "WinElement1"
        Me.WinElement1.NoisePower = 0.15!
        Me.WinElement1.Shadow = True
        Me.WinElement1.Size = New System.Drawing.Size(450, 150)
        Me.WinElement1.StartColor = System.Drawing.Color.Empty
        Me.WinElement1.Style = WinPaletter.UI.Simulation.WinElement.Styles.AltTab11
        Me.WinElement1.SuspendRefresh = False
        Me.WinElement1.TabIndex = 0
        Me.WinElement1.Transparency = True
        Me.WinElement1.UseWin11ORB_WithWin10 = False
        Me.WinElement1.UseWin11RoundedCorners_WithWin10_Level1 = False
        Me.WinElement1.UseWin11RoundedCorners_WithWin10_Level2 = False
        Me.WinElement1.Win7ColorBal = 0
        Me.WinElement1.Win7GlowBal = 0
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
        Me.PictureBox3.Image = Global.WinPaletter.My.Resources.Resources.SampleApp_Active
        Me.PictureBox3.Location = New System.Drawing.Point(190, 20)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 5
        Me.PictureBox3.TabStop = False
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
        Me.PictureBox1.Image = Global.WinPaletter.My.Resources.Resources.SampleApp_Active
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
        Me.PictureBox2.Image = Global.WinPaletter.My.Resources.Resources.SampleApp_Active
        Me.PictureBox2.Location = New System.Drawing.Point(149, 20)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
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
        Me.RetroLabel1.DrawOnGlass = False
        Me.RetroLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.RetroLabel1.ForeColor = System.Drawing.Color.Black
        Me.RetroLabel1.Location = New System.Drawing.Point(0, 0)
        Me.RetroLabel1.Name = "RetroLabel1"
        Me.RetroLabel1.Size = New System.Drawing.Size(302, 29)
        Me.RetroLabel1.TabIndex = 0
        Me.RetroLabel1.Text = "Application"
        Me.RetroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Button10.Location = New System.Drawing.Point(580, 405)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(115, 34)
        Me.Button10.TabIndex = 210
        Me.Button10.Text = "Quick apply"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button7.DrawOnGlass = False
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Image = Nothing
        Me.Button7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button7.LineSize = 1
        Me.Button7.Location = New System.Drawing.Point(494, 405)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(80, 34)
        Me.Button7.TabIndex = 209
        Me.Button7.Text = "Cancel"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button8.DrawOnGlass = False
        Me.Button8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button8.ForeColor = System.Drawing.Color.White
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button8.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button8.LineSize = 1
        Me.Button8.Location = New System.Drawing.Point(701, 405)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(180, 34)
        Me.Button8.TabIndex = 208
        Me.Button8.Text = "Load into current theme"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'GroupBox12
        '
        Me.GroupBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox12.Controls.Add(Me.Button9)
        Me.GroupBox12.Controls.Add(Me.Label12)
        Me.GroupBox12.Controls.Add(Me.Button11)
        Me.GroupBox12.Controls.Add(Me.Button12)
        Me.GroupBox12.Controls.Add(Me.AltTabEnabled)
        Me.GroupBox12.Controls.Add(Me.checker_img)
        Me.GroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(869, 39)
        Me.GroupBox12.TabIndex = 201
        '
        'Button9
        '
        Me.Button9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button9.DrawOnGlass = False
        Me.Button9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button9.ForeColor = System.Drawing.Color.White
        Me.Button9.Image = CType(resources.GetObject("Button9.Image"), System.Drawing.Image)
        Me.Button9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button9.LineColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.Button9.LineSize = 1
        Me.Button9.Location = New System.Drawing.Point(222, 5)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(126, 29)
        Me.Button9.TabIndex = 112
        Me.Button9.Text = "Current applied"
        Me.Button9.UseVisualStyleBackColor = False
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
        'Button11
        '
        Me.Button11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button11.DrawOnGlass = False
        Me.Button11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button11.ForeColor = System.Drawing.Color.White
        Me.Button11.Image = CType(resources.GetObject("Button11.Image"), System.Drawing.Image)
        Me.Button11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button11.LineColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.Button11.LineSize = 1
        Me.Button11.Location = New System.Drawing.Point(85, 5)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(135, 29)
        Me.Button11.TabIndex = 110
        Me.Button11.Text = "WinPaletter theme"
        Me.Button11.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button12.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button12.DrawOnGlass = False
        Me.Button12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button12.ForeColor = System.Drawing.Color.White
        Me.Button12.Image = Nothing
        Me.Button12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button12.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.Button12.LineSize = 1
        Me.Button12.Location = New System.Drawing.Point(351, 5)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(130, 29)
        Me.Button12.TabIndex = 108
        Me.Button12.Text = "Default Windows"
        Me.Button12.UseVisualStyleBackColor = False
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
        'AltTabEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(893, 451)
        Me.Controls.Add(Me.AlertBox2)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.previewContainer)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.GroupBox12)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AltTabEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Windows Switcher (Alt+Tab)"
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.previewContainer.ResumeLayout(False)
        Me.tabs_preview_1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.pnl_preview1.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.Classic_Preview1.ResumeLayout(False)
        Me.RetroPanelRaised1.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RetroPanel1.ResumeLayout(False)
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox12.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents GroupBox12 As UI.WP.GroupBox
    Friend WithEvents Button9 As UI.WP.Button
    Friend WithEvents Label12 As Label
    Friend WithEvents Button11 As UI.WP.Button
    Friend WithEvents Button12 As UI.WP.Button
    Friend WithEvents AltTabEnabled As UI.WP.Toggle
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents Button10 As UI.WP.Button
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents Button8 As UI.WP.Button
    Friend WithEvents previewContainer As UI.WP.GroupBox
    Friend WithEvents tabs_preview_1 As UI.WP.TablessControl
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents pnl_preview1 As Panel
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents Classic_Preview1 As Panel
    Friend WithEvents PictureBox41 As PictureBox
    Friend WithEvents Label19 As Label
    Friend WithEvents GroupBox4 As UI.WP.GroupBox
    Friend WithEvents PictureBox13 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox3 As UI.WP.GroupBox
    Friend WithEvents PictureBox11 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents opacity_btn As UI.WP.Button
    Friend WithEvents Trackbar1 As UI.WP.Trackbar
    Friend WithEvents RadioImage1 As UI.WP.RadioImage
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents RadioImage2 As UI.WP.RadioImage
    Friend WithEvents WinElement1 As UI.Simulation.WinElement
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents RetroPanel1 As UI.Retro.PanelR
    Friend WithEvents RetroLabel1 As UI.WP.LabelAlt
    Friend WithEvents RetroPanelRaised1 As UI.Retro.PanelRaisedR
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents EP_Alert As UI.WP.AlertBox
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
    Friend WithEvents AlertBox2 As UI.WP.AlertBox
End Class
