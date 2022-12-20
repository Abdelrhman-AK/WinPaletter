<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Saving_ex_list
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
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.SuspendLayout()
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = Nothing
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(435, 375)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(130, 30)
        Me.XenonButton1.TabIndex = 1
        Me.XenonButton1.Text = "Close"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(139, 375)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(290, 30)
        Me.XenonButton2.TabIndex = 2
        Me.XenonButton2.Text = "Elicit selected error (Show Exception Error Details)"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView1.ForeColor = System.Drawing.Color.White
        Me.TreeView1.FullRowSelect = True
        Me.TreeView1.Indent = 17
        Me.TreeView1.ItemHeight = 28
        Me.TreeView1.Location = New System.Drawing.Point(12, 12)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.ShowLines = False
        Me.TreeView1.Size = New System.Drawing.Size(553, 354)
        Me.TreeView1.TabIndex = 6
        '
        'Saving_ex_list
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(577, 417)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Saving_ex_list"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Errors"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents TreeView1 As TreeView
End Class
