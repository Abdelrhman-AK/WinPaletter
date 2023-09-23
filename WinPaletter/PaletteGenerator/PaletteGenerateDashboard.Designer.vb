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
        Me.Button4 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.BackColor = System.Drawing.Color.Black
        '
        'Button4
        '
        Me.Button4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Button4.DrawOnGlass = False
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.LineColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(31, Byte), Integer))
        Me.Button4.LineSize = 1
        Me.Button4.Location = New System.Drawing.Point(5, 45)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(253, 32)
        Me.Button4.TabIndex = 90
        Me.Button4.Text = "Generate a palette from a color"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(5, 7)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(253, 32)
        Me.Button1.TabIndex = 86
        Me.Button1.Text = "Generate a palette from an image"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PaletteGenerateDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(263, 83)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button1)
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
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button4 As UI.WP.Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
