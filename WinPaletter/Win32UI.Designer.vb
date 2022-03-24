<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Win32UI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Win32UI))
        Me.pnl12 = New WinPaletter.XenonGroupBox()
        Me.PictureBox23 = New System.Windows.Forms.PictureBox()
        Me.lbl12 = New System.Windows.Forms.Label()
        Me.ActiveBorder_picker = New WinPaletter.XenonGroupBox()
        Me.pnl11 = New WinPaletter.XenonGroupBox()
        Me.PictureBox28 = New System.Windows.Forms.PictureBox()
        Me.lbl11 = New System.Windows.Forms.Label()
        Me.MenuHilight_picker = New WinPaletter.XenonGroupBox()
        Me.pnl10 = New WinPaletter.XenonGroupBox()
        Me.PictureBox29 = New System.Windows.Forms.PictureBox()
        Me.lbl10 = New System.Windows.Forms.Label()
        Me.HotTrackingColor_picker = New WinPaletter.XenonGroupBox()
        Me.pnl9 = New WinPaletter.XenonGroupBox()
        Me.PictureBox30 = New System.Windows.Forms.PictureBox()
        Me.lbl9 = New System.Windows.Forms.Label()
        Me.Hilight_picker = New WinPaletter.XenonGroupBox()
        Me.pnl12.SuspendLayout()
        CType(Me.PictureBox23, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl11.SuspendLayout()
        CType(Me.PictureBox28, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl10.SuspendLayout()
        CType(Me.PictureBox29, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl9.SuspendLayout()
        CType(Me.PictureBox30, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnl12
        '
        Me.pnl12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl12.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.pnl12.Controls.Add(Me.PictureBox23)
        Me.pnl12.Controls.Add(Me.lbl12)
        Me.pnl12.Controls.Add(Me.ActiveBorder_picker)
        Me.pnl12.CustomColor = False
        Me.pnl12.LineColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pnl12.LineSize = 1
        Me.pnl12.Location = New System.Drawing.Point(13, 108)
        Me.pnl12.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl12.Name = "pnl12"
        Me.pnl12.Size = New System.Drawing.Size(442, 30)
        Me.pnl12.TabIndex = 32
        Me.pnl12.Text = "XenonGroupBox27"
        '
        'PictureBox23
        '
        Me.PictureBox23.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox23.Image = CType(resources.GetObject("PictureBox23.Image"), System.Drawing.Image)
        Me.PictureBox23.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox23.Name = "PictureBox23"
        Me.PictureBox23.Size = New System.Drawing.Size(30, 24)
        Me.PictureBox23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox23.TabIndex = 4
        Me.PictureBox23.TabStop = False
        '
        'lbl12
        '
        Me.lbl12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl12.BackColor = System.Drawing.Color.Transparent
        Me.lbl12.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl12.Location = New System.Drawing.Point(36, 6)
        Me.lbl12.Name = "lbl12"
        Me.lbl12.Size = New System.Drawing.Size(347, 16)
        Me.lbl12.TabIndex = 3
        Me.lbl12.Text = "ActiveBorder"
        Me.lbl12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ActiveBorder_picker
        '
        Me.ActiveBorder_picker.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ActiveBorder_picker.CustomColor = True
        Me.ActiveBorder_picker.LineColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.ActiveBorder_picker.LineSize = 1
        Me.ActiveBorder_picker.Location = New System.Drawing.Point(388, 4)
        Me.ActiveBorder_picker.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ActiveBorder_picker.Name = "ActiveBorder_picker"
        Me.ActiveBorder_picker.Size = New System.Drawing.Size(50, 22)
        Me.ActiveBorder_picker.TabIndex = 2
        Me.ActiveBorder_picker.Text = "XenonGroupBox23"
        '
        'pnl11
        '
        Me.pnl11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl11.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.pnl11.Controls.Add(Me.PictureBox28)
        Me.pnl11.Controls.Add(Me.lbl11)
        Me.pnl11.Controls.Add(Me.MenuHilight_picker)
        Me.pnl11.CustomColor = False
        Me.pnl11.LineColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pnl11.LineSize = 1
        Me.pnl11.Location = New System.Drawing.Point(13, 76)
        Me.pnl11.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl11.Name = "pnl11"
        Me.pnl11.Size = New System.Drawing.Size(442, 30)
        Me.pnl11.TabIndex = 31
        Me.pnl11.Text = "XenonGroupBox31"
        '
        'PictureBox28
        '
        Me.PictureBox28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox28.Image = CType(resources.GetObject("PictureBox28.Image"), System.Drawing.Image)
        Me.PictureBox28.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox28.Name = "PictureBox28"
        Me.PictureBox28.Size = New System.Drawing.Size(30, 24)
        Me.PictureBox28.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox28.TabIndex = 4
        Me.PictureBox28.TabStop = False
        '
        'lbl11
        '
        Me.lbl11.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl11.BackColor = System.Drawing.Color.Transparent
        Me.lbl11.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl11.Location = New System.Drawing.Point(36, 6)
        Me.lbl11.Name = "lbl11"
        Me.lbl11.Size = New System.Drawing.Size(347, 16)
        Me.lbl11.TabIndex = 3
        Me.lbl11.Text = "MenuHilight"
        Me.lbl11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuHilight_picker
        '
        Me.MenuHilight_picker.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.MenuHilight_picker.CustomColor = True
        Me.MenuHilight_picker.LineColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.MenuHilight_picker.LineSize = 1
        Me.MenuHilight_picker.Location = New System.Drawing.Point(388, 4)
        Me.MenuHilight_picker.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MenuHilight_picker.Name = "MenuHilight_picker"
        Me.MenuHilight_picker.Size = New System.Drawing.Size(50, 22)
        Me.MenuHilight_picker.TabIndex = 2
        Me.MenuHilight_picker.Text = "XenonGroupBox31"
        '
        'pnl10
        '
        Me.pnl10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl10.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.pnl10.Controls.Add(Me.PictureBox29)
        Me.pnl10.Controls.Add(Me.lbl10)
        Me.pnl10.Controls.Add(Me.HotTrackingColor_picker)
        Me.pnl10.CustomColor = False
        Me.pnl10.LineColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pnl10.LineSize = 1
        Me.pnl10.Location = New System.Drawing.Point(13, 44)
        Me.pnl10.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl10.Name = "pnl10"
        Me.pnl10.Size = New System.Drawing.Size(442, 30)
        Me.pnl10.TabIndex = 30
        Me.pnl10.Text = "XenonGroupBox21"
        '
        'PictureBox29
        '
        Me.PictureBox29.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox29.Image = CType(resources.GetObject("PictureBox29.Image"), System.Drawing.Image)
        Me.PictureBox29.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox29.Name = "PictureBox29"
        Me.PictureBox29.Size = New System.Drawing.Size(30, 24)
        Me.PictureBox29.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox29.TabIndex = 4
        Me.PictureBox29.TabStop = False
        '
        'lbl10
        '
        Me.lbl10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl10.BackColor = System.Drawing.Color.Transparent
        Me.lbl10.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl10.Location = New System.Drawing.Point(36, 6)
        Me.lbl10.Name = "lbl10"
        Me.lbl10.Size = New System.Drawing.Size(347, 16)
        Me.lbl10.TabIndex = 3
        Me.lbl10.Text = "HotTrackingColor"
        Me.lbl10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'HotTrackingColor_picker
        '
        Me.HotTrackingColor_picker.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.HotTrackingColor_picker.CustomColor = True
        Me.HotTrackingColor_picker.LineColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.HotTrackingColor_picker.LineSize = 1
        Me.HotTrackingColor_picker.Location = New System.Drawing.Point(388, 4)
        Me.HotTrackingColor_picker.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.HotTrackingColor_picker.Name = "HotTrackingColor_picker"
        Me.HotTrackingColor_picker.Size = New System.Drawing.Size(50, 22)
        Me.HotTrackingColor_picker.TabIndex = 2
        Me.HotTrackingColor_picker.Text = "XenonGroupBox23"
        '
        'pnl9
        '
        Me.pnl9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl9.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.pnl9.Controls.Add(Me.PictureBox30)
        Me.pnl9.Controls.Add(Me.lbl9)
        Me.pnl9.Controls.Add(Me.Hilight_picker)
        Me.pnl9.CustomColor = False
        Me.pnl9.LineColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pnl9.LineSize = 1
        Me.pnl9.Location = New System.Drawing.Point(13, 12)
        Me.pnl9.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl9.Name = "pnl9"
        Me.pnl9.Size = New System.Drawing.Size(442, 30)
        Me.pnl9.TabIndex = 29
        Me.pnl9.Text = "XenonGroupBox25"
        '
        'PictureBox30
        '
        Me.PictureBox30.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox30.Image = CType(resources.GetObject("PictureBox30.Image"), System.Drawing.Image)
        Me.PictureBox30.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox30.Name = "PictureBox30"
        Me.PictureBox30.Size = New System.Drawing.Size(30, 24)
        Me.PictureBox30.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox30.TabIndex = 4
        Me.PictureBox30.TabStop = False
        '
        'lbl9
        '
        Me.lbl9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl9.BackColor = System.Drawing.Color.Transparent
        Me.lbl9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl9.Location = New System.Drawing.Point(36, 6)
        Me.lbl9.Name = "lbl9"
        Me.lbl9.Size = New System.Drawing.Size(347, 16)
        Me.lbl9.TabIndex = 3
        Me.lbl9.Text = "Hilight"
        Me.lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Hilight_picker
        '
        Me.Hilight_picker.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Hilight_picker.CustomColor = True
        Me.Hilight_picker.LineColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.Hilight_picker.LineSize = 1
        Me.Hilight_picker.Location = New System.Drawing.Point(388, 4)
        Me.Hilight_picker.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Hilight_picker.Name = "Hilight_picker"
        Me.Hilight_picker.Size = New System.Drawing.Size(50, 22)
        Me.Hilight_picker.TabIndex = 2
        Me.Hilight_picker.Text = "XenonGroupBox19"
        '
        'Win32UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(606, 470)
        Me.Controls.Add(Me.pnl12)
        Me.Controls.Add(Me.pnl11)
        Me.Controls.Add(Me.pnl10)
        Me.Controls.Add(Me.pnl9)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Win32UI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Win32UI"
        Me.pnl12.ResumeLayout(False)
        CType(Me.PictureBox23, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl11.ResumeLayout(False)
        CType(Me.PictureBox28, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl10.ResumeLayout(False)
        CType(Me.PictureBox29, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl9.ResumeLayout(False)
        CType(Me.PictureBox30, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnl12 As XenonGroupBox
    Friend WithEvents PictureBox23 As PictureBox
    Friend WithEvents lbl12 As Label
    Friend WithEvents ActiveBorder_picker As XenonGroupBox
    Friend WithEvents pnl11 As XenonGroupBox
    Friend WithEvents PictureBox28 As PictureBox
    Friend WithEvents lbl11 As Label
    Friend WithEvents MenuHilight_picker As XenonGroupBox
    Friend WithEvents pnl10 As XenonGroupBox
    Friend WithEvents PictureBox29 As PictureBox
    Friend WithEvents lbl10 As Label
    Friend WithEvents HotTrackingColor_picker As XenonGroupBox
    Friend WithEvents pnl9 As XenonGroupBox
    Friend WithEvents PictureBox30 As PictureBox
    Friend WithEvents lbl9 As Label
    Friend WithEvents Hilight_picker As XenonGroupBox
End Class
