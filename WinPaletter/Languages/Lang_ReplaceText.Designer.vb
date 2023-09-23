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
        Me.TextBox3 = New WinPaletter.UI.WP.TextBox()
        Me.TextBox4 = New WinPaletter.UI.WP.TextBox()
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.CheckBox2 = New UI.WP.CheckBox()
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
        'TextBox3
        '
        Me.TextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox3.DrawOnGlass = False
        Me.TextBox3.ForeColor = System.Drawing.Color.White
        Me.TextBox3.Location = New System.Drawing.Point(157, 12)
        Me.TextBox3.MaxLength = 32767
        Me.TextBox3.Multiline = False
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = False
        Me.TextBox3.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TextBox3.SelectedText = ""
        Me.TextBox3.SelectionLength = 0
        Me.TextBox3.SelectionStart = 0
        Me.TextBox3.Size = New System.Drawing.Size(275, 24)
        Me.TextBox3.TabIndex = 67
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox3.UseSystemPasswordChar = False
        Me.TextBox3.WordWrap = True
        '
        'TextBox4
        '
        Me.TextBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox4.DrawOnGlass = False
        Me.TextBox4.ForeColor = System.Drawing.Color.White
        Me.TextBox4.Location = New System.Drawing.Point(157, 42)
        Me.TextBox4.MaxLength = 32767
        Me.TextBox4.Multiline = False
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = False
        Me.TextBox4.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TextBox4.SelectedText = ""
        Me.TextBox4.SelectionLength = 0
        Me.TextBox4.SelectionStart = 0
        Me.TextBox4.Size = New System.Drawing.Size(275, 24)
        Me.TextBox4.TabIndex = 68
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox4.UseSystemPasswordChar = False
        Me.TextBox4.WordWrap = True
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button7.DrawOnGlass = False
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Image = Nothing
        Me.Button7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button7.LineSize = 1
        Me.Button7.Location = New System.Drawing.Point(266, 175)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(80, 34)
        Me.Button7.TabIndex = 207
        Me.Button7.Text = "Cancel"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Button2.LineSize = 1
        Me.Button2.Location = New System.Drawing.Point(352, 175)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 34)
        Me.Button2.TabIndex = 206
        Me.Button2.Text = "Replace"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'CheckBox1
        '
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox1.Checked = False
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(157, 72)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(190, 23)
        Me.CheckBox1.TabIndex = 208
        Me.CheckBox1.Text = "Match case"
        '
        'CheckBox2
        '
        Me.CheckBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox2.Checked = False
        Me.CheckBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox2.ForeColor = System.Drawing.Color.White
        Me.CheckBox2.Location = New System.Drawing.Point(157, 101)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(190, 23)
        Me.CheckBox2.TabIndex = 209
        Me.CheckBox2.Text = "Match whole word"
        '
        'Lang_ReplaceText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(444, 221)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox25)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.PictureBox24)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox4)
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
    Friend WithEvents TextBox3 As UI.WP.TextBox
    Friend WithEvents TextBox4 As UI.WP.TextBox
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
    Friend WithEvents CheckBox2 As UI.WP.CheckBox
End Class
