<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LicenseForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LicenseForm))
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.TextBox1 = New WinPaletter.UI.WP.TextBox()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Button2.LineSize = 1
        Me.Button2.Location = New System.Drawing.Point(572, 425)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 34)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Reject"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Nothing
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(486, 425)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 34)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Accept"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 416)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(640, 1)
        Me.Separator1.TabIndex = 1
        Me.Separator1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox1.DrawOnGlass = False
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(12, 12)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.SelectionLength = 0
        Me.TextBox1.SelectionStart = 0
        Me.TextBox1.Size = New System.Drawing.Size(640, 396)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox1.UseSystemPasswordChar = False
        Me.TextBox1.WordWrap = True
        '
        'LicenseForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(664, 471)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.TextBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LicenseForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MIT License"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TextBox1 As UI.WP.TextBox
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
End Class
