<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BugReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BugReport))
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.AnimatedBox1 = New WinPaletter.UI.WP.AnimatedBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button5 = New WinPaletter.UI.WP.Button()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.GroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.TreeView1 = New WinPaletter.UI.WP.TreeView()
        Me.Button6 = New WinPaletter.UI.WP.Button()
        Me.Button4 = New WinPaletter.UI.WP.Button()
        Me.Button3 = New WinPaletter.UI.WP.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.AnimatedBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Text File (*.txt)|*.txt"
        '
        'AnimatedBox1
        '
        Me.AnimatedBox1.BackColor = System.Drawing.Color.Maroon
        Me.AnimatedBox1.Color = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.AnimatedBox1.Color1 = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.AnimatedBox1.Color2 = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer))
        Me.AnimatedBox1.Controls.Add(Me.Label3)
        Me.AnimatedBox1.Controls.Add(Me.PictureBox1)
        Me.AnimatedBox1.Controls.Add(Me.Label5)
        Me.AnimatedBox1.Controls.Add(Me.Label7)
        Me.AnimatedBox1.Controls.Add(Me.PictureBox5)
        Me.AnimatedBox1.Controls.Add(Me.PictureBox2)
        Me.AnimatedBox1.Controls.Add(Me.Label2)
        Me.AnimatedBox1.Controls.Add(Me.Label1)
        Me.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.AnimatedBox1.Location = New System.Drawing.Point(0, 0)
        Me.AnimatedBox1.Name = "AnimatedBox1"
        Me.AnimatedBox1.Size = New System.Drawing.Size(794, 110)
        Me.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors
        Me.AnimatedBox1.TabIndex = 121
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(217, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(567, 24)
        Me.Label3.TabIndex = 90
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(47, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(164, 24)
        Me.Label5.TabIndex = 89
        Me.Label5.Text = "WinPaletter version:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(47, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(737, 35)
        Me.Label7.TabIndex = 85
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(17, 79)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 88
        Me.PictureBox5.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(17, 49)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(217, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(567, 24)
        Me.Label2.TabIndex = 87
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(47, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 24)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "OS information:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button5.DrawOnGlass = False
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Image = Nothing
        Me.Button5.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Button5.LineSize = 1
        Me.Button5.Location = New System.Drawing.Point(425, 396)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(115, 34)
        Me.Button5.TabIndex = 120
        Me.Button5.Text = "GitHub issues"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Button2.LineSize = 1
        Me.Button2.Location = New System.Drawing.Point(546, 396)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 34)
        Me.Button2.TabIndex = 117
        Me.Button2.Text = "Exit application"
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
        Me.Button1.LineColor = System.Drawing.Color.Green
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(667, 396)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 34)
        Me.Button1.TabIndex = 116
        Me.Button1.Text = "Continue"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 118)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(770, 1)
        Me.Separator1.TabIndex = 115
        Me.Separator1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.TreeView1)
        Me.GroupBox3.Controls.Add(Me.Button6)
        Me.GroupBox3.Controls.Add(Me.Button4)
        Me.GroupBox3.Controls.Add(Me.Button3)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.PictureBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 128)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(770, 255)
        Me.GroupBox3.TabIndex = 114
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView1.Indent = 15
        Me.TreeView1.ItemHeight = 20
        Me.TreeView1.Location = New System.Drawing.Point(3, 32)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(763, 220)
        Me.TreeView1.TabIndex = 122
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button6.DrawOnGlass = False
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Image = Nothing
        Me.Button6.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Button6.LineSize = 1
        Me.Button6.Location = New System.Drawing.Point(666, 6)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(100, 22)
        Me.Button6.TabIndex = 121
        Me.Button6.Text = "Previous reports"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button4.DrawOnGlass = False
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.LineColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Button4.LineSize = 1
        Me.Button4.Location = New System.Drawing.Point(629, 6)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(35, 22)
        Me.Button4.TabIndex = 119
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button3.DrawOnGlass = False
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.LineColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.Button3.LineSize = 1
        Me.Button3.Location = New System.Drawing.Point(592, 6)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(35, 22)
        Me.Button3.TabIndex = 118
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(33, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(424, 24)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Error details:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(3, 5)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 1
        Me.PictureBox4.TabStop = False
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = Nothing
        Me.AlertBox1.Location = New System.Drawing.Point(15, 396)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(404, 34)
        Me.AlertBox1.TabIndex = 122
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "You won't be able to recover from this error. Switch to Release mode"
        Me.AlertBox1.Visible = False
        '
        'BugReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(794, 442)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.AnimatedBox1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(645, 435)
        Me.Name = "BugReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exception Error"
        Me.TopMost = True
        Me.AnimatedBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents GroupBox3 As UI.WP.GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents Button3 As UI.WP.Button
    Friend WithEvents Button4 As UI.WP.Button
    Friend WithEvents Button5 As UI.WP.Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Button6 As UI.WP.Button
    Friend WithEvents TreeView1 As UI.WP.TreeView
    Friend WithEvents AnimatedBox1 As UI.WP.AnimatedBox
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
End Class
