<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WindowsTerminalCopycat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WindowsTerminalCopycat))
        Me.Label163 = New System.Windows.Forms.Label()
        Me.PictureBox33 = New System.Windows.Forms.PictureBox()
        Me.XenonComboBox1 = New WinPaletter.XenonComboBox()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        CType(Me.PictureBox33, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label163
        '
        Me.Label163.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label163.BackColor = System.Drawing.Color.Transparent
        Me.Label163.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label163.Location = New System.Drawing.Point(42, 12)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(418, 24)
        Me.Label163.TabIndex = 191
        Me.Label163.Text = "Copycat from:"
        Me.Label163.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox33
        '
        Me.PictureBox33.Image = CType(resources.GetObject("PictureBox33.Image"), System.Drawing.Image)
        Me.PictureBox33.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox33.Name = "PictureBox33"
        Me.PictureBox33.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox33.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox33.TabIndex = 192
        Me.PictureBox33.TabStop = False
        '
        'XenonComboBox1
        '
        Me.XenonComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonComboBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonComboBox1.CustomFont = False
        Me.XenonComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.XenonComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.XenonComboBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonComboBox1.ForeColor = System.Drawing.Color.White
        Me.XenonComboBox1.FormattingEnabled = True
        Me.XenonComboBox1.ItemHeight = 20
        Me.XenonComboBox1.Location = New System.Drawing.Point(45, 38)
        Me.XenonComboBox1.Name = "XenonComboBox1"
        Me.XenonComboBox1.Size = New System.Drawing.Size(415, 26)
        Me.XenonComboBox1.TabIndex = 193
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(294, 75)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton2.TabIndex = 195
        Me.XenonButton2.Text = "Cancel"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = Nothing
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(380, 75)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton1.TabIndex = 194
        Me.XenonButton1.Text = "OK"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'WindowsTerminalCopycat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(472, 121)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton1)
        Me.Controls.Add(Me.XenonComboBox1)
        Me.Controls.Add(Me.Label163)
        Me.Controls.Add(Me.PictureBox33)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WindowsTerminalCopycat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Copycat"
        CType(Me.PictureBox33, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label163 As Label
    Friend WithEvents PictureBox33 As PictureBox
    Friend WithEvents XenonComboBox1 As XenonComboBox
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonButton1 As XenonButton
End Class
