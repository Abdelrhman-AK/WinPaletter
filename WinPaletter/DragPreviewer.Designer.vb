<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dragPreviewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dragPreviewer))
        Me.pnl_preview = New System.Windows.Forms.Panel()
        Me.ActionCenter = New WinPaletter.XenonAcrylic()
        Me.XenonWindow1 = New WinPaletter.XenonWindow()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.XenonWindow2 = New WinPaletter.XenonWindow()
        Me.start = New WinPaletter.XenonAcrylic()
        Me.taskbar = New WinPaletter.XenonAcrylic()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnl_preview.SuspendLayout()
        Me.XenonWindow1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnl_preview
        '
        Me.pnl_preview.AllowDrop = True
        Me.pnl_preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_preview.Controls.Add(Me.ActionCenter)
        Me.pnl_preview.Controls.Add(Me.XenonWindow1)
        Me.pnl_preview.Controls.Add(Me.XenonWindow2)
        Me.pnl_preview.Controls.Add(Me.start)
        Me.pnl_preview.Controls.Add(Me.taskbar)
        Me.pnl_preview.Location = New System.Drawing.Point(375, 296)
        Me.pnl_preview.Name = "pnl_preview"
        Me.pnl_preview.Size = New System.Drawing.Size(528, 297)
        Me.pnl_preview.TabIndex = 3
        Me.pnl_preview.Visible = False
        '
        'ActionCenter
        '
        Me.ActionCenter.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.ActionCenter.ActionCenterButton_Normal = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ActionCenter.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.ActionCenter.AppBackground = System.Drawing.Color.Empty
        Me.ActionCenter.AppUnderline = System.Drawing.Color.Empty
        Me.ActionCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.ActionCenter.BackColorAlpha = CType(50, Byte)
        Me.ActionCenter.BlurPower = 8
        Me.ActionCenter.Borders = True
        Me.ActionCenter.DarkMode = True
        Me.ActionCenter.DropShadow = True
        Me.ActionCenter.LinkColor = System.Drawing.Color.Empty
        Me.ActionCenter.Location = New System.Drawing.Point(398, 161)
        Me.ActionCenter.Name = "ActionCenter"
        Me.ActionCenter.NoisePower = New Decimal(New Integer() {3, 0, 0, 65536})
        Me.ActionCenter.Padding = New System.Windows.Forms.Padding(2)
        Me.ActionCenter.Radius = 5
        Me.ActionCenter.RoundedCorners = True
        Me.ActionCenter.SearchBoxAccent = System.Drawing.Color.Empty
        Me.ActionCenter.Size = New System.Drawing.Size(120, 85)
        Me.ActionCenter.StartColor = System.Drawing.Color.Empty
        Me.ActionCenter.TabIndex = 5
        Me.ActionCenter.Transparency = True
        Me.ActionCenter.UseItAsActionCenter = True
        Me.ActionCenter.UseItAsStartMenu = False
        Me.ActionCenter.UseItAsTaskbar = False
        Me.ActionCenter.UseItAsTaskbar_Version = WinPaletter.XenonAcrylic.TaskbarVersion.Eleven
        '
        'XenonWindow1
        '
        Me.XenonWindow1.AccentColor_Active = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.XenonWindow1.AccentColor_Enabled = True
        Me.XenonWindow1.AccentColor_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.XenonWindow1.Active = True
        Me.XenonWindow1.Controls.Add(Me.Label12)
        Me.XenonWindow1.Controls.Add(Me.Panel3)
        Me.XenonWindow1.DarkMode = True
        Me.XenonWindow1.DropShadow = True
        Me.XenonWindow1.Location = New System.Drawing.Point(148, 46)
        Me.XenonWindow1.Name = "XenonWindow1"
        Me.XenonWindow1.Padding = New System.Windows.Forms.Padding(2)
        Me.XenonWindow1.Radius = 5
        Me.XenonWindow1.RoundedCorners = True
        Me.XenonWindow1.Size = New System.Drawing.Size(189, 148)
        Me.XenonWindow1.TabIndex = 2
        Me.XenonWindow1.Text = "App Preview"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Brown
        Me.Label12.Location = New System.Drawing.Point(5, 119)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(126, 25)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Link Preview"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Location = New System.Drawing.Point(5, 27)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel3.Size = New System.Drawing.Size(179, 39)
        Me.Panel3.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(59, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 37)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "This is a setting icon"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Segoe MDL2 Assets", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 37)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = ""
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'XenonWindow2
        '
        Me.XenonWindow2.AccentColor_Active = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.XenonWindow2.AccentColor_Enabled = True
        Me.XenonWindow2.AccentColor_Inactive = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.XenonWindow2.Active = False
        Me.XenonWindow2.DarkMode = True
        Me.XenonWindow2.DropShadow = True
        Me.XenonWindow2.Location = New System.Drawing.Point(148, 200)
        Me.XenonWindow2.Name = "XenonWindow2"
        Me.XenonWindow2.Padding = New System.Windows.Forms.Padding(2)
        Me.XenonWindow2.Radius = 5
        Me.XenonWindow2.RoundedCorners = True
        Me.XenonWindow2.Size = New System.Drawing.Size(189, 46)
        Me.XenonWindow2.TabIndex = 3
        Me.XenonWindow2.Text = "Inactive app"
        '
        'start
        '
        Me.start.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.start.ActionCenterButton_Normal = System.Drawing.Color.Empty
        Me.start.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.start.AppBackground = System.Drawing.Color.Empty
        Me.start.AppUnderline = System.Drawing.Color.Empty
        Me.start.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.start.BackColorAlpha = CType(150, Byte)
        Me.start.BlurPower = 6
        Me.start.Borders = True
        Me.start.DarkMode = True
        Me.start.DropShadow = True
        Me.start.LinkColor = System.Drawing.Color.Empty
        Me.start.Location = New System.Drawing.Point(7, 46)
        Me.start.Name = "start"
        Me.start.NoisePower = New Decimal(New Integer() {3, 0, 0, 65536})
        Me.start.Padding = New System.Windows.Forms.Padding(2)
        Me.start.Radius = 5
        Me.start.RoundedCorners = True
        Me.start.SearchBoxAccent = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.start.Size = New System.Drawing.Size(135, 200)
        Me.start.StartColor = System.Drawing.Color.Empty
        Me.start.TabIndex = 1
        Me.start.Transparency = True
        Me.start.UseItAsActionCenter = False
        Me.start.UseItAsStartMenu = True
        Me.start.UseItAsTaskbar = False
        Me.start.UseItAsTaskbar_Version = WinPaletter.XenonAcrylic.TaskbarVersion.Eleven
        '
        'taskbar
        '
        Me.taskbar.ActionCenterButton_Hover = System.Drawing.Color.Empty
        Me.taskbar.ActionCenterButton_Normal = System.Drawing.Color.Empty
        Me.taskbar.ActionCenterButton_Pressed = System.Drawing.Color.Empty
        Me.taskbar.AppBackground = System.Drawing.Color.Empty
        Me.taskbar.AppUnderline = System.Drawing.Color.Empty
        Me.taskbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.taskbar.BackColorAlpha = CType(130, Byte)
        Me.taskbar.BlurPower = 15
        Me.taskbar.Borders = False
        Me.taskbar.DarkMode = True
        Me.taskbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.taskbar.DropShadow = True
        Me.taskbar.LinkColor = System.Drawing.Color.Empty
        Me.taskbar.Location = New System.Drawing.Point(0, 253)
        Me.taskbar.Name = "taskbar"
        Me.taskbar.NoisePower = New Decimal(New Integer() {15, 0, 0, 131072})
        Me.taskbar.Radius = 5
        Me.taskbar.RoundedCorners = False
        Me.taskbar.SearchBoxAccent = System.Drawing.Color.Empty
        Me.taskbar.Size = New System.Drawing.Size(526, 42)
        Me.taskbar.StartColor = System.Drawing.Color.Empty
        Me.taskbar.TabIndex = 0
        Me.taskbar.Transparency = True
        Me.taskbar.UseItAsActionCenter = False
        Me.taskbar.UseItAsStartMenu = False
        Me.taskbar.UseItAsTaskbar = True
        Me.taskbar.UseItAsTaskbar_Version = WinPaletter.XenonAcrylic.TaskbarVersion.Eleven
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(384, 216)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'dragPreviewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Fuchsia
        Me.ClientSize = New System.Drawing.Size(408, 328)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.pnl_preview)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dragPreviewer"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "dragPreviewer"
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.pnl_preview.ResumeLayout(False)
        Me.XenonWindow1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnl_preview As Panel
    Friend WithEvents ActionCenter As XenonAcrylic
    Friend WithEvents XenonWindow1 As XenonWindow
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents XenonWindow2 As XenonWindow
    Friend WithEvents start As XenonAcrylic
    Friend WithEvents taskbar As XenonAcrylic
    Friend WithEvents PictureBox1 As PictureBox
End Class
