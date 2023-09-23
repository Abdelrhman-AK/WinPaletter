<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ApplicationThemer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApplicationThemer))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.PictureBox46 = New System.Windows.Forms.PictureBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.PictureBox45 = New System.Windows.Forms.PictureBox()
        Me.PictureBox44 = New System.Windows.Forms.PictureBox()
        Me.PictureBox43 = New System.Windows.Forms.PictureBox()
        Me.BackColorPick = New WinPaletter.UI.Controllers.ColorItem()
        Me.AccentColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.RoundedCorners = New UI.WP.CheckBox()
        Me.appearance_dark = New UI.WP.CheckBox()
        Me.XenonButton7 = New WinPaletter.UI.WP.Button()
        Me.XenonButton8 = New WinPaletter.UI.WP.Button()
        Me.XenonGroupBox12 = New WinPaletter.UI.WP.GroupBox()
        Me.XenonButton9 = New WinPaletter.UI.WP.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.XenonButton11 = New WinPaletter.UI.WP.Button()
        Me.XenonButton12 = New WinPaletter.UI.WP.Button()
        Me.AppThemeEnabled = New UI.WP.Toggle()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.appearance_list = New WinPaletter.UI.WP.ComboBox()
        Me.XenonButton10 = New WinPaletter.UI.WP.Button()
        Me.XenonAlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.XenonAlertBox2 = New WinPaletter.UI.WP.AlertBox()
        CType(Me.PictureBox46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox43, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox12.SuspendLayout()
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(42, 181)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(130, 24)
        Me.Label29.TabIndex = 226
        Me.Label29.Text = "Background color:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox46
        '
        Me.PictureBox46.Image = CType(resources.GetObject("PictureBox46.Image"), System.Drawing.Image)
        Me.PictureBox46.Location = New System.Drawing.Point(12, 181)
        Me.PictureBox46.Name = "PictureBox46"
        Me.PictureBox46.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox46.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox46.TabIndex = 225
        Me.PictureBox46.TabStop = False
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(42, 151)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(130, 24)
        Me.Label28.TabIndex = 223
        Me.Label28.Text = "Accent color:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox45
        '
        Me.PictureBox45.Image = CType(resources.GetObject("PictureBox45.Image"), System.Drawing.Image)
        Me.PictureBox45.Location = New System.Drawing.Point(12, 151)
        Me.PictureBox45.Name = "PictureBox45"
        Me.PictureBox45.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox45.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox45.TabIndex = 222
        Me.PictureBox45.TabStop = False
        '
        'PictureBox44
        '
        Me.PictureBox44.Image = CType(resources.GetObject("PictureBox44.Image"), System.Drawing.Image)
        Me.PictureBox44.Location = New System.Drawing.Point(12, 121)
        Me.PictureBox44.Name = "PictureBox44"
        Me.PictureBox44.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox44.TabIndex = 220
        Me.PictureBox44.TabStop = False
        '
        'PictureBox43
        '
        Me.PictureBox43.Image = CType(resources.GetObject("PictureBox43.Image"), System.Drawing.Image)
        Me.PictureBox43.Location = New System.Drawing.Point(12, 91)
        Me.PictureBox43.Name = "PictureBox43"
        Me.PictureBox43.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox43.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox43.TabIndex = 218
        Me.PictureBox43.TabStop = False
        '
        'BackColorPick
        '
        Me.BackColorPick.AllowDrop = True
        Me.BackColorPick.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.BackColorPick.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.BackColorPick.DontShowInfo = False
        Me.BackColorPick.Location = New System.Drawing.Point(178, 181)
        Me.BackColorPick.Name = "BackColorPick"
        Me.BackColorPick.Size = New System.Drawing.Size(112, 24)
        Me.BackColorPick.TabIndex = 227
        '
        'AccentColor
        '
        Me.AccentColor.AllowDrop = True
        Me.AccentColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AccentColor.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AccentColor.DontShowInfo = False
        Me.AccentColor.Location = New System.Drawing.Point(178, 151)
        Me.AccentColor.Name = "AccentColor"
        Me.AccentColor.Size = New System.Drawing.Size(112, 24)
        Me.AccentColor.TabIndex = 224
        '
        'RoundedCorners
        '
        Me.RoundedCorners.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RoundedCorners.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.RoundedCorners.Checked = True
        Me.RoundedCorners.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RoundedCorners.ForeColor = System.Drawing.Color.White
        Me.RoundedCorners.Location = New System.Drawing.Point(42, 121)
        Me.RoundedCorners.Name = "RoundedCorners"
        Me.RoundedCorners.Size = New System.Drawing.Size(555, 24)
        Me.RoundedCorners.TabIndex = 221
        Me.RoundedCorners.Text = "Rounded corners"
        '
        'appearance_dark
        '
        Me.appearance_dark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.appearance_dark.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.appearance_dark.Checked = True
        Me.appearance_dark.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.appearance_dark.ForeColor = System.Drawing.Color.White
        Me.appearance_dark.Location = New System.Drawing.Point(42, 91)
        Me.appearance_dark.Name = "appearance_dark"
        Me.appearance_dark.Size = New System.Drawing.Size(555, 24)
        Me.appearance_dark.TabIndex = 219
        Me.appearance_dark.Text = "Dark mode"
        '
        'XenonButton7
        '
        Me.XenonButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton7.DrawOnGlass = False
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = Nothing
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(210, 315)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton7.TabIndex = 212
        Me.XenonButton7.Text = "Cancel"
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonButton8
        '
        Me.XenonButton8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton8.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton8.DrawOnGlass = False
        Me.XenonButton8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton8.ForeColor = System.Drawing.Color.White
        Me.XenonButton8.Image = CType(resources.GetObject("XenonButton8.Image"), System.Drawing.Image)
        Me.XenonButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton8.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XenonButton8.LineSize = 1
        Me.XenonButton8.Location = New System.Drawing.Point(417, 315)
        Me.XenonButton8.Name = "XenonButton8"
        Me.XenonButton8.Size = New System.Drawing.Size(180, 34)
        Me.XenonButton8.TabIndex = 211
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
        Me.XenonGroupBox12.Controls.Add(Me.AppThemeEnabled)
        Me.XenonGroupBox12.Controls.Add(Me.checker_img)
        Me.XenonGroupBox12.Controls.Add(Me.Label25)
        Me.XenonGroupBox12.Controls.Add(Me.appearance_list)
        Me.XenonGroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.XenonGroupBox12.Name = "XenonGroupBox12"
        Me.XenonGroupBox12.Size = New System.Drawing.Size(585, 70)
        Me.XenonGroupBox12.TabIndex = 202
        '
        'XenonButton9
        '
        Me.XenonButton9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton9.DrawOnGlass = False
        Me.XenonButton9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton9.ForeColor = System.Drawing.Color.White
        Me.XenonButton9.Image = CType(resources.GetObject("XenonButton9.Image"), System.Drawing.Image)
        Me.XenonButton9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton9.LineColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.XenonButton9.LineSize = 1
        Me.XenonButton9.Location = New System.Drawing.Point(223, 5)
        Me.XenonButton9.Name = "XenonButton9"
        Me.XenonButton9.Size = New System.Drawing.Size(126, 29)
        Me.XenonButton9.TabIndex = 112
        Me.XenonButton9.Text = "Current applied"
        Me.XenonButton9.UseVisualStyleBackColor = False
        '
        'Label12
        '
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
        Me.XenonButton11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton11.DrawOnGlass = False
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
        Me.XenonButton12.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton12.DrawOnGlass = False
        Me.XenonButton12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton12.ForeColor = System.Drawing.Color.White
        Me.XenonButton12.Image = Nothing
        Me.XenonButton12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton12.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.XenonButton12.LineSize = 1
        Me.XenonButton12.Location = New System.Drawing.Point(352, 5)
        Me.XenonButton12.Name = "XenonButton12"
        Me.XenonButton12.Size = New System.Drawing.Size(135, 29)
        Me.XenonButton12.TabIndex = 108
        Me.XenonButton12.Text = "Default Windows"
        Me.XenonButton12.UseVisualStyleBackColor = False
        '
        'AppThemeEnabled
        '
        Me.AppThemeEnabled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AppThemeEnabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.AppThemeEnabled.Checked = False
        Me.AppThemeEnabled.DarkLight_Toggler = False
        Me.AppThemeEnabled.Location = New System.Drawing.Point(539, 25)
        Me.AppThemeEnabled.Name = "AppThemeEnabled"
        Me.AppThemeEnabled.Size = New System.Drawing.Size(40, 20)
        Me.AppThemeEnabled.TabIndex = 85
        '
        'checker_img
        '
        Me.checker_img.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checker_img.Image = Global.WinPaletter.My.Resources.Resources.checker_disabled
        Me.checker_img.Location = New System.Drawing.Point(498, 20)
        Me.checker_img.Name = "checker_img"
        Me.checker_img.Size = New System.Drawing.Size(35, 31)
        Me.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.checker_img.TabIndex = 83
        Me.checker_img.TabStop = False
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(4, 40)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(75, 24)
        Me.Label25.TabIndex = 215
        Me.Label25.Text = "Scheme:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'appearance_list
        '
        Me.appearance_list.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.appearance_list.CustomFont = False
        Me.appearance_list.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.appearance_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.appearance_list.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.appearance_list.ForeColor = System.Drawing.Color.White
        Me.appearance_list.FormattingEnabled = True
        Me.appearance_list.ItemHeight = 20
        Me.appearance_list.Items.AddRange(New Object() {"Default Dark", "Default Light", "AMOLED", "Extreme White", "GitHub Dark", "GitHub Light", "Reddit Dark", "Reddit Light", "Discord Dark", "Discord Light"})
        Me.appearance_list.Location = New System.Drawing.Point(85, 38)
        Me.appearance_list.Name = "appearance_list"
        Me.appearance_list.Size = New System.Drawing.Size(402, 26)
        Me.appearance_list.TabIndex = 216
        '
        'XenonButton10
        '
        Me.XenonButton10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton10.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton10.DrawOnGlass = False
        Me.XenonButton10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton10.ForeColor = System.Drawing.Color.White
        Me.XenonButton10.Image = CType(resources.GetObject("XenonButton10.Image"), System.Drawing.Image)
        Me.XenonButton10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton10.LineColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.XenonButton10.LineSize = 1
        Me.XenonButton10.Location = New System.Drawing.Point(296, 315)
        Me.XenonButton10.Name = "XenonButton10"
        Me.XenonButton10.Size = New System.Drawing.Size(115, 34)
        Me.XenonButton10.TabIndex = 228
        Me.XenonButton10.Text = "Quick apply"
        Me.XenonButton10.UseVisualStyleBackColor = False
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.XenonAlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonAlertBox1.CenterText = False
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Nothing
        Me.XenonAlertBox1.Location = New System.Drawing.Point(12, 219)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(585, 22)
        Me.XenonAlertBox1.TabIndex = 229
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = "To preview changes, enable the toggle above"
        '
        'XenonAlertBox2
        '
        Me.XenonAlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.XenonAlertBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonAlertBox2.CenterText = False
        Me.XenonAlertBox2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox2.Image = Nothing
        Me.XenonAlertBox2.Location = New System.Drawing.Point(12, 248)
        Me.XenonAlertBox2.Name = "XenonAlertBox2"
        Me.XenonAlertBox2.Size = New System.Drawing.Size(585, 60)
        Me.XenonAlertBox2.TabIndex = 230
        Me.XenonAlertBox2.TabStop = False
        Me.XenonAlertBox2.Text = resources.GetString("XenonAlertBox2.Text")
        '
        'ApplicationThemer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(609, 361)
        Me.Controls.Add(Me.XenonAlertBox2)
        Me.Controls.Add(Me.XenonAlertBox1)
        Me.Controls.Add(Me.XenonButton10)
        Me.Controls.Add(Me.BackColorPick)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.PictureBox46)
        Me.Controls.Add(Me.AccentColor)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.PictureBox45)
        Me.Controls.Add(Me.RoundedCorners)
        Me.Controls.Add(Me.PictureBox44)
        Me.Controls.Add(Me.appearance_dark)
        Me.Controls.Add(Me.PictureBox43)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonButton8)
        Me.Controls.Add(Me.XenonGroupBox12)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ApplicationThemer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WinPaletter application theme"
        CType(Me.PictureBox46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox43, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox12.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents XenonGroupBox12 As UI.WP.GroupBox
    Friend WithEvents XenonButton9 As UI.WP.Button
    Friend WithEvents Label12 As Label
    Friend WithEvents XenonButton11 As UI.WP.Button
    Friend WithEvents XenonButton12 As UI.WP.Button
    Friend WithEvents AppThemeEnabled As UI.WP.Toggle
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents XenonButton7 As UI.WP.Button
    Friend WithEvents XenonButton8 As UI.WP.Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents BackColorPick As UI.Controllers.ColorItem
    Friend WithEvents Label29 As Label
    Friend WithEvents PictureBox46 As PictureBox
    Friend WithEvents AccentColor As UI.Controllers.ColorItem
    Friend WithEvents Label28 As Label
    Friend WithEvents PictureBox45 As PictureBox
    Friend WithEvents RoundedCorners As UI.WP.CheckBox
    Friend WithEvents PictureBox44 As PictureBox
    Friend WithEvents appearance_dark As UI.WP.CheckBox
    Friend WithEvents PictureBox43 As PictureBox
    Friend WithEvents appearance_list As UI.WP.ComboBox
    Friend WithEvents Label25 As Label
    Friend WithEvents XenonButton10 As UI.WP.Button
    Friend WithEvents XenonAlertBox1 As UI.WP.AlertBox
    Friend WithEvents XenonAlertBox2 As UI.WP.AlertBox
End Class
