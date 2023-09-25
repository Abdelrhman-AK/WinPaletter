<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LogonUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogonUI))
        Me.GroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.GroupBox21 = New WinPaletter.UI.WP.GroupBox()
        Me.LogonUI_Lockscreen_Toggle = New UI.WP.Toggle()
        Me.PictureBox22 = New System.Windows.Forms.PictureBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox19 = New WinPaletter.UI.WP.GroupBox()
        Me.LogonUI_Background_Toggle = New UI.WP.Toggle()
        Me.PictureBox16 = New System.Windows.Forms.PictureBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.GroupBox17 = New WinPaletter.UI.WP.GroupBox()
        Me.LogonUI_Acrylic_Toggle = New UI.WP.Toggle()
        Me.PictureBox15 = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.GroupBox12 = New WinPaletter.UI.WP.GroupBox()
        Me.Button9 = New WinPaletter.UI.WP.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button11 = New WinPaletter.UI.WP.Button()
        Me.Button12 = New WinPaletter.UI.WP.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox21.SuspendLayout()
        CType(Me.PictureBox22, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox19.SuspendLayout()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox17.SuspendLayout()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox12.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.GroupBox21)
        Me.GroupBox3.Controls.Add(Me.GroupBox19)
        Me.GroupBox3.Controls.Add(Me.GroupBox17)
        Me.GroupBox3.Controls.Add(Me.PictureBox6)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(495, 135)
        Me.GroupBox3.TabIndex = 15
        '
        'GroupBox21
        '
        Me.GroupBox21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox21.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.GroupBox21.Controls.Add(Me.LogonUI_Lockscreen_Toggle)
        Me.GroupBox21.Controls.Add(Me.PictureBox22)
        Me.GroupBox21.Controls.Add(Me.Label20)
        Me.GroupBox21.Location = New System.Drawing.Point(3, 103)
        Me.GroupBox21.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox21.Name = "GroupBox21"
        Me.GroupBox21.Size = New System.Drawing.Size(489, 29)
        Me.GroupBox21.TabIndex = 12
        '
        'LogonUI_Lockscreen_Toggle
        '
        Me.LogonUI_Lockscreen_Toggle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogonUI_Lockscreen_Toggle.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.LogonUI_Lockscreen_Toggle.Checked = False
        Me.LogonUI_Lockscreen_Toggle.DarkLight_Toggler = False
        Me.LogonUI_Lockscreen_Toggle.Location = New System.Drawing.Point(445, 4)
        Me.LogonUI_Lockscreen_Toggle.Name = "LogonUI_Lockscreen_Toggle"
        Me.LogonUI_Lockscreen_Toggle.Size = New System.Drawing.Size(40, 20)
        Me.LogonUI_Lockscreen_Toggle.TabIndex = 16
        '
        'PictureBox22
        '
        Me.PictureBox22.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox22.Image = CType(resources.GetObject("PictureBox22.Image"), System.Drawing.Image)
        Me.PictureBox22.Location = New System.Drawing.Point(3, 1)
        Me.PictureBox22.Name = "PictureBox22"
        Me.PictureBox22.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox22.TabIndex = 4
        Me.PictureBox22.TabStop = False
        '
        'Label20
        '
        Me.Label20.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(36, 2)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(396, 24)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = "Lockscreen"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox19
        '
        Me.GroupBox19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox19.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.GroupBox19.Controls.Add(Me.LogonUI_Background_Toggle)
        Me.GroupBox19.Controls.Add(Me.PictureBox16)
        Me.GroupBox19.Controls.Add(Me.Label18)
        Me.GroupBox19.Location = New System.Drawing.Point(3, 72)
        Me.GroupBox19.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(489, 29)
        Me.GroupBox19.TabIndex = 11
        '
        'LogonUI_Background_Toggle
        '
        Me.LogonUI_Background_Toggle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogonUI_Background_Toggle.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.LogonUI_Background_Toggle.Checked = False
        Me.LogonUI_Background_Toggle.DarkLight_Toggler = False
        Me.LogonUI_Background_Toggle.Location = New System.Drawing.Point(445, 4)
        Me.LogonUI_Background_Toggle.Name = "LogonUI_Background_Toggle"
        Me.LogonUI_Background_Toggle.Size = New System.Drawing.Size(40, 20)
        Me.LogonUI_Background_Toggle.TabIndex = 16
        '
        'PictureBox16
        '
        Me.PictureBox16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox16.Image = CType(resources.GetObject("PictureBox16.Image"), System.Drawing.Image)
        Me.PictureBox16.Location = New System.Drawing.Point(3, 1)
        Me.PictureBox16.Name = "PictureBox16"
        Me.PictureBox16.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox16.TabIndex = 4
        Me.PictureBox16.TabStop = False
        '
        'Label18
        '
        Me.Label18.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(36, 2)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(396, 24)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "LogonUI background"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox17
        '
        Me.GroupBox17.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox17.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.GroupBox17.Controls.Add(Me.LogonUI_Acrylic_Toggle)
        Me.GroupBox17.Controls.Add(Me.PictureBox15)
        Me.GroupBox17.Controls.Add(Me.Label16)
        Me.GroupBox17.Location = New System.Drawing.Point(3, 41)
        Me.GroupBox17.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(489, 29)
        Me.GroupBox17.TabIndex = 10
        '
        'LogonUI_Acrylic_Toggle
        '
        Me.LogonUI_Acrylic_Toggle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogonUI_Acrylic_Toggle.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.LogonUI_Acrylic_Toggle.Checked = False
        Me.LogonUI_Acrylic_Toggle.DarkLight_Toggler = False
        Me.LogonUI_Acrylic_Toggle.Location = New System.Drawing.Point(445, 4)
        Me.LogonUI_Acrylic_Toggle.Name = "LogonUI_Acrylic_Toggle"
        Me.LogonUI_Acrylic_Toggle.Size = New System.Drawing.Size(40, 20)
        Me.LogonUI_Acrylic_Toggle.TabIndex = 16
        '
        'PictureBox15
        '
        Me.PictureBox15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox15.Image = CType(resources.GetObject("PictureBox15.Image"), System.Drawing.Image)
        Me.PictureBox15.Location = New System.Drawing.Point(3, 1)
        Me.PictureBox15.Name = "PictureBox15"
        Me.PictureBox15.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox15.TabIndex = 4
        Me.PictureBox15.TabStop = False
        '
        'Label16
        '
        Me.Label16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(36, 2)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(396, 24)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Acrylic"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(44, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(198, 35)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "LogonUI and LockScreen"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Button2.Location = New System.Drawing.Point(241, 215)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 34)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 204)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(495, 1)
        Me.Separator1.TabIndex = 18
        Me.Separator1.TabStop = False
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
        Me.GroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(495, 39)
        Me.GroupBox12.TabIndex = 201
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
        Me.Button12.Location = New System.Drawing.Point(351, 5)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(135, 29)
        Me.Button12.TabIndex = 108
        Me.Button12.Text = "Default Windows"
        Me.Button12.UseVisualStyleBackColor = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
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
        Me.Button1.Location = New System.Drawing.Point(327, 215)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(180, 34)
        Me.Button1.TabIndex = 202
        Me.Button1.Text = "Load into current theme"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'LogonUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(519, 261)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox12)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LogonUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LogonUI"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox21.ResumeLayout(False)
        CType(Me.PictureBox22, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox19.ResumeLayout(False)
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox17.ResumeLayout(False)
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox12.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As UI.WP.GroupBox
    Friend WithEvents GroupBox21 As UI.WP.GroupBox
    Friend WithEvents LogonUI_Lockscreen_Toggle As UI.WP.Toggle
    Friend WithEvents PictureBox22 As PictureBox
    Friend WithEvents Label20 As Label
    Friend WithEvents GroupBox19 As UI.WP.GroupBox
    Friend WithEvents LogonUI_Background_Toggle As UI.WP.Toggle
    Friend WithEvents PictureBox16 As PictureBox
    Friend WithEvents Label18 As Label
    Friend WithEvents GroupBox17 As UI.WP.GroupBox
    Friend WithEvents LogonUI_Acrylic_Toggle As UI.WP.Toggle
    Friend WithEvents PictureBox15 As PictureBox
    Friend WithEvents Label16 As Label
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents GroupBox12 As UI.WP.GroupBox
    Friend WithEvents Button9 As UI.WP.Button
    Friend WithEvents Label12 As Label
    Friend WithEvents Button11 As UI.WP.Button
    Friend WithEvents Button12 As UI.WP.Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button1 As UI.WP.Button
End Class
