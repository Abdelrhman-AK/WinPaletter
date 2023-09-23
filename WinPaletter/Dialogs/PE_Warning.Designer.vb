<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PE_Warning
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PE_Warning))
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Button4 = New WinPaletter.UI.WP.Button()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.Button3 = New WinPaletter.UI.WP.Button()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.AnimatedBox1 = New WinPaletter.UI.WP.AnimatedBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.GroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.TreeView1 = New WinPaletter.UI.WP.TreeView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.AnimatedBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Text File (*.txt)|*.txt"
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Button4.DrawOnGlass = False
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.LineColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.Button4.LineSize = 1
        Me.Button4.Location = New System.Drawing.Point(359, 400)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(220, 30)
        Me.Button4.TabIndex = 127
        Me.Button4.Text = "Restore PE file integrity (health)"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 440)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(850, 1)
        Me.Separator1.TabIndex = 126
        Me.Separator1.TabStop = False
        Me.Separator1.Text = "Separator1"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Button3.DrawOnGlass = False
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.LineColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.Button3.LineSize = 1
        Me.Button3.Location = New System.Drawing.Point(13, 400)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(340, 30)
        Me.Button3.TabIndex = 125
        Me.Button3.Text = "Know more about Windows Security (Defender) issue"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(6, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = CType(resources.GetObject("AlertBox1.Image"), System.Drawing.Image)
        Me.AlertBox1.Location = New System.Drawing.Point(12, 346)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(850, 48)
        Me.AlertBox1.TabIndex = 124
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = resources.GetString("AlertBox1.Text")
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox1.Checked = False
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(12, 453)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(608, 24)
        Me.CheckBox1.TabIndex = 123
        Me.CheckBox1.Text = "Always ignore this dialog and do action selected in Settings > Theme applying beh" &
    "avior > PE pathcing"
        '
        'AnimatedBox1
        '
        Me.AnimatedBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(4, Byte), Integer))
        Me.AnimatedBox1.Color = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(4, Byte), Integer))
        Me.AnimatedBox1.Color1 = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(4, Byte), Integer))
        Me.AnimatedBox1.Color2 = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.AnimatedBox1.Controls.Add(Me.PictureBox1)
        Me.AnimatedBox1.Controls.Add(Me.Label7)
        Me.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.AnimatedBox1.Location = New System.Drawing.Point(0, 0)
        Me.AnimatedBox1.Name = "AnimatedBox1"
        Me.AnimatedBox1.Size = New System.Drawing.Size(874, 48)
        Me.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors
        Me.AnimatedBox1.TabIndex = 121
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
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(47, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(817, 35)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "WinPaletter will modify a system PE file and this will change its resources and i" &
    "ntegrity."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Button2.Location = New System.Drawing.Point(626, 448)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 34)
        Me.Button2.TabIndex = 117
        Me.Button2.Text = "Don't modify"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(747, 448)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 34)
        Me.Button1.TabIndex = 116
        Me.Button1.Text = "Modify"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.TreeView1)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.PictureBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 59)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(850, 280)
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
        Me.TreeView1.Size = New System.Drawing.Size(843, 245)
        Me.TreeView1.TabIndex = 122
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(33, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(813, 24)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Action details:"
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
        'PE_Warning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(874, 494)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.AnimatedBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(645, 435)
        Me.Name = "PE_Warning"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PE resources editor"
        Me.TopMost = True
        Me.AnimatedBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBox3 As UI.WP.GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents TreeView1 As UI.WP.TreeView
    Friend WithEvents AnimatedBox1 As UI.WP.AnimatedBox
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
    Friend WithEvents Button3 As UI.WP.Button
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents Button4 As UI.WP.Button
End Class
