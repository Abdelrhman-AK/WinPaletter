<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TerminalsDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TerminalsDashboard))
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button5 = New WinPaletter.UI.WP.Button()
        Me.Button6 = New WinPaletter.UI.WP.Button()
        Me.Separator3 = New WinPaletter.UI.WP.SeparatorH()
        Me.Button3 = New WinPaletter.UI.WP.Button()
        Me.Button4 = New WinPaletter.UI.WP.Button()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.SeparatorVertical1 = New WinPaletter.UI.WP.SeparatorV()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(6, 7)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(149, 19)
        Me.Label49.TabIndex = 84
        Me.Label49.Text = "Consoles:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(170, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 19)
        Me.Label2.TabIndex = 92
        Me.Label2.Text = "Windows Terminal:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(304, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(18, 18)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 97
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "It is effective for Windows 10 and Windows 11 (If you have installed Windows Term" &
        "inal from the Store)")
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button5.DrawOnGlass = False
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.LineColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Button5.Location = New System.Drawing.Point(173, 68)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(149, 27)
        Me.Button5.TabIndex = 95
        Me.Button5.Text = "Preview"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button6.DrawOnGlass = False
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button6.LineColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Button6.Location = New System.Drawing.Point(173, 37)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(149, 27)
        Me.Button6.TabIndex = 94
        Me.Button6.Text = "Stable"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Separator3
        '
        Me.Separator3.AlternativeLook = False
        Me.Separator3.Location = New System.Drawing.Point(173, 30)
        Me.Separator3.Name = "Separator3"
        Me.Separator3.Size = New System.Drawing.Size(149, 1)
        Me.Separator3.TabIndex = 93
        Me.Separator3.TabStop = False
        Me.Separator3.Text = "Separator3"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button3.DrawOnGlass = False
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.LineColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(7, 98)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(149, 27)
        Me.Button3.TabIndex = 91
        Me.Button3.Text = "PowerShell x64"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button4.DrawOnGlass = False
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.LineColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(7, 67)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(149, 27)
        Me.Button4.TabIndex = 90
        Me.Button4.Text = "PowerShell x86"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(7, 129)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(149, 27)
        Me.Button2.TabIndex = 87
        Me.Button2.Text = "External"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(7, 36)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(149, 27)
        Me.Button1.TabIndex = 86
        Me.Button1.Text = "Command Prompt"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Location = New System.Drawing.Point(7, 30)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(149, 1)
        Me.Separator1.TabIndex = 85
        Me.Separator1.TabStop = False
        '
        'SeparatorVertical1
        '
        Me.SeparatorVertical1.AlternativeLook = False
        Me.SeparatorVertical1.Location = New System.Drawing.Point(162, 7)
        Me.SeparatorVertical1.Name = "SeparatorVertical1"
        Me.SeparatorVertical1.Size = New System.Drawing.Size(1, 149)
        Me.SeparatorVertical1.TabIndex = 102
        Me.SeparatorVertical1.TabStop = False
        '
        'TerminalsDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(331, 163)
        Me.ControlBox = False
        Me.Controls.Add(Me.SeparatorVertical1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Separator3)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.Label49)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TerminalsDashboard"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Terminals Dashboard"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label49 As Label
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents Button3 As UI.WP.Button
    Friend WithEvents Button4 As UI.WP.Button
    Friend WithEvents Button5 As UI.WP.Button
    Friend WithEvents Button6 As UI.WP.Button
    Friend WithEvents Separator3 As UI.WP.SeparatorH
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SeparatorVertical1 As UI.WP.SeparatorV
End Class
