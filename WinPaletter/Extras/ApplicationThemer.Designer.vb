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
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.Button8 = New WinPaletter.UI.WP.Button()
        Me.GroupBox12 = New WinPaletter.UI.WP.GroupBox()
        Me.Button9 = New WinPaletter.UI.WP.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button11 = New WinPaletter.UI.WP.Button()
        Me.Button12 = New WinPaletter.UI.WP.Button()
        Me.AppThemeEnabled = New UI.WP.Toggle()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.appearance_list = New WinPaletter.UI.WP.ComboBox()
        Me.Button10 = New WinPaletter.UI.WP.Button()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.AlertBox2 = New WinPaletter.UI.WP.AlertBox()
        CType(Me.PictureBox46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox43, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox12.SuspendLayout()
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
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button7.DrawOnGlass = False
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Image = Nothing
        Me.Button7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button7.Location = New System.Drawing.Point(210, 315)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(80, 34)
        Me.Button7.TabIndex = 212
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
        Me.Button8.Location = New System.Drawing.Point(417, 315)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(180, 34)
        Me.Button8.TabIndex = 211
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
        Me.GroupBox12.Controls.Add(Me.AppThemeEnabled)
        Me.GroupBox12.Controls.Add(Me.checker_img)
        Me.GroupBox12.Controls.Add(Me.Label25)
        Me.GroupBox12.Controls.Add(Me.appearance_list)
        Me.GroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(585, 70)
        Me.GroupBox12.TabIndex = 202
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button9.DrawOnGlass = False
        Me.Button9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button9.ForeColor = System.Drawing.Color.White
        Me.Button9.Image = CType(resources.GetObject("Button9.Image"), System.Drawing.Image)
        Me.Button9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button9.LineColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.Button9.Location = New System.Drawing.Point(223, 5)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(126, 29)
        Me.Button9.TabIndex = 112
        Me.Button9.Text = "Current applied"
        Me.Button9.UseVisualStyleBackColor = False
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
        'Button11
        '
        Me.Button11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button11.DrawOnGlass = False
        Me.Button11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button11.ForeColor = System.Drawing.Color.White
        Me.Button11.Image = CType(resources.GetObject("Button11.Image"), System.Drawing.Image)
        Me.Button11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button11.LineColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.Button11.Location = New System.Drawing.Point(85, 5)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(135, 29)
        Me.Button11.TabIndex = 110
        Me.Button11.Text = "WinPaletter theme"
        Me.Button11.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button12.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button12.DrawOnGlass = False
        Me.Button12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button12.ForeColor = System.Drawing.Color.White
        Me.Button12.Image = Nothing
        Me.Button12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button12.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.Button12.Location = New System.Drawing.Point(352, 5)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(135, 29)
        Me.Button12.TabIndex = 108
        Me.Button12.Text = "Default Windows"
        Me.Button12.UseVisualStyleBackColor = False
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
        Me.Button10.Location = New System.Drawing.Point(296, 315)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(115, 34)
        Me.Button10.TabIndex = 228
        Me.Button10.Text = "Quick apply"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = Nothing
        Me.AlertBox1.Location = New System.Drawing.Point(12, 219)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(585, 22)
        Me.AlertBox1.TabIndex = 229
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "To preview changes, enable the toggle above"
        '
        'AlertBox2
        '
        Me.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.AlertBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.AlertBox2.CenterText = False
        Me.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox2.Image = Nothing
        Me.AlertBox2.Location = New System.Drawing.Point(12, 248)
        Me.AlertBox2.Name = "AlertBox2"
        Me.AlertBox2.Size = New System.Drawing.Size(585, 60)
        Me.AlertBox2.TabIndex = 230
        Me.AlertBox2.TabStop = False
        Me.AlertBox2.Text = resources.GetString("AlertBox2.Text")
        '
        'ApplicationThemer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(609, 361)
        Me.Controls.Add(Me.AlertBox2)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.Button10)
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
        Me.Name = "ApplicationThemer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WinPaletter application theme"
        CType(Me.PictureBox46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox43, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox12.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox12 As UI.WP.GroupBox
    Friend WithEvents Button9 As UI.WP.Button
    Friend WithEvents Label12 As Label
    Friend WithEvents Button11 As UI.WP.Button
    Friend WithEvents Button12 As UI.WP.Button
    Friend WithEvents AppThemeEnabled As UI.WP.Toggle
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents Button8 As UI.WP.Button
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
    Friend WithEvents Button10 As UI.WP.Button
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
    Friend WithEvents AlertBox2 As UI.WP.AlertBox
End Class
