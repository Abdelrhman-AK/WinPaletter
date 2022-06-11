<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.SuspendLayout()
        '
        'XenonButton1
        '
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = Nothing
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(318, 96)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(183, 58)
        Me.XenonButton1.TabIndex = 0
        Me.XenonButton1.Text = "XenonButton1"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.XenonButton1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents XenonButton1 As XenonButton
End Class
