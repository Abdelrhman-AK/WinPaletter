<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Lang_ReplaceText
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lang_ReplaceText))
        Me.PictureBox25 = New System.Windows.Forms.PictureBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.PictureBox24 = New System.Windows.Forms.PictureBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.XenonTextBox3 = New WinPaletter.UI.WP.TextBox()
        Me.XenonTextBox4 = New WinPaletter.UI.WP.TextBox()
        Me.XenonButton7 = New WinPaletter.UI.WP.Button()
        Me.XenonButton2 = New WinPaletter.UI.WP.Button()
        Me.XenonCheckBox1 = New UI.WP.CheckBox()
        Me.XenonCheckBox2 = New UI.WP.CheckBox()
        CType(Me.PictureBox25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox24, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox25
        '
        Me.PictureBox25.Image = CType(resources.GetObject("PictureBox25.Image"), System.Drawing.Image)
        Me.PictureBox25.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox25.Name = "PictureBox25"
        Me.PictureBox25.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox25.TabIndex = 63
        Me.PictureBox25.TabStop = False
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(42, 12)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(109, 24)
        Me.Label20.TabIndex = 64
        Me.Label20.Text = "Find what:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox24
        '
        Me.PictureBox24.Image = CType(resources.GetObject("PictureBox24.Image"), System.Drawing.Image)
        Me.PictureBox24.Location = New System.Drawing.Point(12, 42)
        Me.PictureBox24.Name = "PictureBox24"
        Me.PictureBox24.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox24.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox24.TabIndex = 65
        Me.PictureBox24.TabStop = False
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(42, 42)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(109, 24)
        Me.Label18.TabIndex = 66
        Me.Label18.Text = "Replace with:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonTextBox3
        '
        Me.XenonTextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox3.DrawOnGlass = False
        Me.XenonTextBox3.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox3.Location = New System.Drawing.Point(157, 12)
        Me.XenonTextBox3.MaxLength = 32767
        Me.XenonTextBox3.Multiline = False
        Me.XenonTextBox3.Name = "XenonTextBox3"
        Me.XenonTextBox3.ReadOnly = False
        Me.XenonTextBox3.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox3.SelectedText = ""
        Me.XenonTextBox3.SelectionLength = 0
        Me.XenonTextBox3.SelectionStart = 0
        Me.XenonTextBox3.Size = New System.Drawing.Size(275, 24)
        Me.XenonTextBox3.TabIndex = 67
        Me.XenonTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox3.UseSystemPasswordChar = False
        Me.XenonTextBox3.WordWrap = True
        '
        'XenonTextBox4
        '
        Me.XenonTextBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox4.DrawOnGlass = False
        Me.XenonTextBox4.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox4.Location = New System.Drawing.Point(157, 42)
        Me.XenonTextBox4.MaxLength = 32767
        Me.XenonTextBox4.Multiline = False
        Me.XenonTextBox4.Name = "XenonTextBox4"
        Me.XenonTextBox4.ReadOnly = False
        Me.XenonTextBox4.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox4.SelectedText = ""
        Me.XenonTextBox4.SelectionLength = 0
        Me.XenonTextBox4.SelectionStart = 0
        Me.XenonTextBox4.Size = New System.Drawing.Size(275, 24)
        Me.XenonTextBox4.TabIndex = 68
        Me.XenonTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox4.UseSystemPasswordChar = False
        Me.XenonTextBox4.WordWrap = True
        '
        'XenonButton7
        '
        Me.XenonButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton7.DrawOnGlass = False
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = Nothing
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(266, 175)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton7.TabIndex = 207
        Me.XenonButton7.Text = "Cancel"
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.DrawOnGlass = False
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(352, 175)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton2.TabIndex = 206
        Me.XenonButton2.Text = "Replace"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox1.Checked = False
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(157, 72)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(190, 23)
        Me.XenonCheckBox1.TabIndex = 208
        Me.XenonCheckBox1.Text = "Match case"
        '
        'XenonCheckBox2
        '
        Me.XenonCheckBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox2.Checked = False
        Me.XenonCheckBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox2.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox2.Location = New System.Drawing.Point(157, 101)
        Me.XenonCheckBox2.Name = "XenonCheckBox2"
        Me.XenonCheckBox2.Size = New System.Drawing.Size(190, 23)
        Me.XenonCheckBox2.TabIndex = 209
        Me.XenonCheckBox2.Text = "Match whole word"
        '
        'Lang_ReplaceText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(444, 221)
        Me.Controls.Add(Me.XenonCheckBox2)
        Me.Controls.Add(Me.XenonCheckBox1)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.PictureBox25)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.PictureBox24)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.XenonTextBox3)
        Me.Controls.Add(Me.XenonTextBox4)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Lang_ReplaceText"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Replace"
        CType(Me.PictureBox25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox24, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox25 As PictureBox
    Friend WithEvents Label20 As Label
    Friend WithEvents PictureBox24 As PictureBox
    Friend WithEvents Label18 As Label
    Friend WithEvents XenonTextBox3 As UI.WP.TextBox
    Friend WithEvents XenonTextBox4 As UI.WP.TextBox
    Friend WithEvents XenonButton7 As UI.WP.Button
    Friend WithEvents XenonButton2 As UI.WP.Button
    Friend WithEvents XenonCheckBox1 As UI.WP.CheckBox
    Friend WithEvents XenonCheckBox2 As UI.WP.CheckBox
End Class
