<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PaletteGenerateDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PaletteGenerateDashboard))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.XenonButton4 = New WinPaletter.UI.WP.Button()
        Me.XenonButton1 = New WinPaletter.UI.WP.Button()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.BackColor = System.Drawing.Color.Black
        '
        'XenonButton4
        '
        Me.XenonButton4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton4.DrawOnGlass = False
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(31, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(5, 45)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(253, 32)
        Me.XenonButton4.TabIndex = 90
        Me.XenonButton4.Text = "Generate a palette from a color"
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton1.DrawOnGlass = False
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(5, 7)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(253, 32)
        Me.XenonButton1.TabIndex = 86
        Me.XenonButton1.Text = "Generate a palette from an image"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'PaletteGenerateDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(263, 83)
        Me.ControlBox = False
        Me.Controls.Add(Me.XenonButton4)
        Me.Controls.Add(Me.XenonButton1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PaletteGenerateDashboard"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Terminals Dashboard"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XenonButton1 As UI.WP.Button
    Friend WithEvents XenonButton4 As UI.WP.Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
