<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Store_SearchFilter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Store_SearchFilter))
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox16 = New System.Windows.Forms.PictureBox()
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.CheckBox2 = New UI.WP.CheckBox()
        Me.CheckBox3 = New UI.WP.CheckBox()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Nothing
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(269, 245)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 34)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Save filter"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Image = Nothing
        Me.Button7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button7.LineSize = 1
        Me.Button7.Location = New System.Drawing.Point(183, 245)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(80, 34)
        Me.Button7.TabIndex = 207
        Me.Button7.Text = "Cancel"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(42, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(172, 24)
        Me.Label6.TabIndex = 209
        Me.Label6.Text = "Search through:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox16
        '
        Me.PictureBox16.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox16.Image = CType(resources.GetObject("PictureBox16.Image"), System.Drawing.Image)
        Me.PictureBox16.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox16.Name = "PictureBox16"
        Me.PictureBox16.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox16.TabIndex = 208
        Me.PictureBox16.TabStop = False
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox1.Checked = True
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(45, 39)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(314, 24)
        Me.CheckBox1.TabIndex = 210
        Me.CheckBox1.Text = "Themes names"
        '
        'CheckBox2
        '
        Me.CheckBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox2.Checked = True
        Me.CheckBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox2.ForeColor = System.Drawing.Color.White
        Me.CheckBox2.Location = New System.Drawing.Point(45, 99)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(314, 24)
        Me.CheckBox2.TabIndex = 211
        Me.CheckBox2.Text = "Authors names"
        '
        'CheckBox3
        '
        Me.CheckBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox3.Checked = True
        Me.CheckBox3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox3.ForeColor = System.Drawing.Color.White
        Me.CheckBox3.Location = New System.Drawing.Point(45, 69)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(314, 24)
        Me.CheckBox3.TabIndex = 212
        Me.CheckBox3.Text = "Themes descriptions"
        '
        'Store_SearchFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(371, 291)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PictureBox16)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Store_SearchFilter"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search filter"
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox16 As PictureBox
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
    Friend WithEvents CheckBox2 As UI.WP.CheckBox
    Friend WithEvents CheckBox3 As UI.WP.CheckBox
End Class
