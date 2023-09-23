<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogonUIXP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogonUIXP))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1 = New WinPaletter.UI.WP.GroupBox()
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.color_pick = New WinPaletter.UI.Controllers.ColorItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioImage2 = New WinPaletter.UI.WP.RadioImage()
        Me.RadioImage1 = New WinPaletter.UI.WP.RadioImage()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Button10 = New WinPaletter.UI.WP.Button()
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.Button8 = New WinPaletter.UI.WP.Button()
        Me.GroupBox12 = New WinPaletter.UI.WP.GroupBox()
        Me.Toggle1 = New UI.WP.Toggle()
        Me.Button9 = New WinPaletter.UI.WP.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button11 = New WinPaletter.UI.WP.Button()
        Me.Button12 = New WinPaletter.UI.WP.Button()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox12.SuspendLayout()
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.PictureBox7)
        Me.GroupBox1.Controls.Add(Me.color_pick)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 313)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(583, 104)
        Me.GroupBox1.TabIndex = 212
        '
        'CheckBox1
        '
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.CheckBox1.Checked = False
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(79, 73)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(269, 25)
        Me.CheckBox1.TabIndex = 115
        Me.CheckBox1.Text = "Show more options (e.g. shutdown button)"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(48, 73)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 114
        Me.PictureBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(79, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 25)
        Me.Label3.TabIndex = 113
        Me.Label3.Text = "Background color:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(48, 42)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 94
        Me.PictureBox7.TabStop = False
        '
        'color_pick
        '
        Me.color_pick.AllowDrop = True
        Me.color_pick.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.color_pick.DefaultColor = System.Drawing.Color.Black
        Me.color_pick.DontShowInfo = False
        Me.color_pick.Location = New System.Drawing.Point(195, 42)
        Me.color_pick.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.color_pick.Name = "color_pick"
        Me.color_pick.Size = New System.Drawing.Size(100, 25)
        Me.color_pick.TabIndex = 93
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(44, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(536, 35)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Tweaks specific for Windows 2000 mode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.GroupBox3.Controls.Add(Me.PictureBox6)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(583, 250)
        Me.GroupBox3.TabIndex = 211
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(294, 217)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 22)
        Me.Label2.TabIndex = 113
        Me.Label2.Text = "Windows 2000"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(118, 217)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "Windows XP"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadioImage2
        '
        Me.RadioImage2.Checked = False
        Me.RadioImage2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage2.ForeColor = System.Drawing.Color.White
        Me.RadioImage2.Image = CType(resources.GetObject("RadioImage2.Image"), System.Drawing.Image)
        Me.RadioImage2.Location = New System.Drawing.Point(294, 44)
        Me.RadioImage2.Name = "RadioImage2"
        Me.RadioImage2.ShowText = False
        Me.RadioImage2.Size = New System.Drawing.Size(170, 170)
        Me.RadioImage2.TabIndex = 3
        Me.RadioImage2.Text = "RadioImage2"
        '
        'RadioImage1
        '
        Me.RadioImage1.Checked = False
        Me.RadioImage1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage1.ForeColor = System.Drawing.Color.White
        Me.RadioImage1.Image = CType(resources.GetObject("RadioImage1.Image"), System.Drawing.Image)
        Me.RadioImage1.Location = New System.Drawing.Point(118, 44)
        Me.RadioImage1.Name = "RadioImage1"
        Me.RadioImage1.ShowText = False
        Me.RadioImage1.Size = New System.Drawing.Size(170, 170)
        Me.RadioImage1.TabIndex = 2
        Me.RadioImage1.Text = "RadioImage1"
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 1
        Me.PictureBox6.TabStop = False
        '
        'Label13
        '
        Me.Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(44, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(536, 35)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "LogonUI screen type"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Button10.Location = New System.Drawing.Point(294, 429)
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
        Me.Button7.Location = New System.Drawing.Point(208, 429)
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
        Me.Button8.Location = New System.Drawing.Point(415, 429)
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
        Me.GroupBox12.Controls.Add(Me.Toggle1)
        Me.GroupBox12.Controls.Add(Me.Button9)
        Me.GroupBox12.Controls.Add(Me.Label12)
        Me.GroupBox12.Controls.Add(Me.Button11)
        Me.GroupBox12.Controls.Add(Me.Button12)
        Me.GroupBox12.Controls.Add(Me.checker_img)
        Me.GroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(583, 39)
        Me.GroupBox12.TabIndex = 202
        '
        'Toggle1
        '
        Me.Toggle1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Toggle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Toggle1.Checked = False
        Me.Toggle1.DarkLight_Toggler = False
        Me.Toggle1.Location = New System.Drawing.Point(538, 9)
        Me.Toggle1.Name = "Toggle1"
        Me.Toggle1.Size = New System.Drawing.Size(40, 20)
        Me.Toggle1.TabIndex = 82
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
        Me.Button12.Size = New System.Drawing.Size(135, 29)
        Me.Button12.TabIndex = 108
        Me.Button12.Text = "Default Windows"
        Me.Button12.UseVisualStyleBackColor = False
        '
        'checker_img
        '
        Me.checker_img.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checker_img.Image = Global.WinPaletter.My.Resources.Resources.checker_disabled
        Me.checker_img.Location = New System.Drawing.Point(497, 4)
        Me.checker_img.Name = "checker_img"
        Me.checker_img.Size = New System.Drawing.Size(35, 31)
        Me.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.checker_img.TabIndex = 83
        Me.checker_img.TabStop = False
        '
        'LogonUIXP
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(607, 475)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.GroupBox12)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LogonUIXP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LogonUI - Windows XP"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox12.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox12 As UI.WP.GroupBox
    Friend WithEvents Toggle1 As UI.WP.Toggle
    Friend WithEvents Button9 As UI.WP.Button
    Friend WithEvents Label12 As Label
    Friend WithEvents Button11 As UI.WP.Button
    Friend WithEvents Button12 As UI.WP.Button
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents Button10 As UI.WP.Button
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents Button8 As UI.WP.Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents GroupBox3 As UI.WP.GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents RadioImage2 As UI.WP.RadioImage
    Friend WithEvents RadioImage1 As UI.WP.RadioImage
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label13 As Label
    Friend WithEvents GroupBox1 As UI.WP.GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents color_pick As UI.Controllers.ColorItem
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label3 As Label
End Class
