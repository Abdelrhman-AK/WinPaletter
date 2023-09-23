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
        Me.XenonButton4 = New WinPaletter.UI.WP.Button()
        Me.XenonSeparator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.XenonButton3 = New WinPaletter.UI.WP.Button()
        Me.XenonAlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.XenonCheckBox1 = New UI.WP.CheckBox()
        Me.XenonAnimatedBox1 = New WinPaletter.UI.WP.AnimatedBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.XenonButton2 = New WinPaletter.UI.WP.Button()
        Me.XenonButton1 = New WinPaletter.UI.WP.Button()
        Me.XenonGroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.XenonTreeView1 = New WinPaletter.UI.WP.TreeView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.XenonAnimatedBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox3.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Text File (*.txt)|*.txt"
        '
        'XenonButton4
        '
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonButton4.DrawOnGlass = False
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(359, 400)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(220, 30)
        Me.XenonButton4.TabIndex = 127
        Me.XenonButton4.Text = "Restore PE file integrity (health)"
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.AlternativeLook = False
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 440)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(850, 1)
        Me.XenonSeparator1.TabIndex = 126
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonButton3
        '
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonButton3.DrawOnGlass = False
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = CType(resources.GetObject("XenonButton3.Image"), System.Drawing.Image)
        Me.XenonButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(13, 400)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(340, 30)
        Me.XenonButton3.TabIndex = 125
        Me.XenonButton3.Text = "Know more about Windows Security (Defender) issue"
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive
        Me.XenonAlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(6, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.XenonAlertBox1.CenterText = False
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = CType(resources.GetObject("XenonAlertBox1.Image"), System.Drawing.Image)
        Me.XenonAlertBox1.Location = New System.Drawing.Point(12, 346)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(850, 48)
        Me.XenonAlertBox1.TabIndex = 124
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = resources.GetString("XenonAlertBox1.Text")
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox1.Checked = False
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(12, 453)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(608, 24)
        Me.XenonCheckBox1.TabIndex = 123
        Me.XenonCheckBox1.Text = "Always ignore this dialog and do action selected in Settings > Theme applying beh" &
    "avior > PE pathcing"
        '
        'XenonAnimatedBox1
        '
        Me.XenonAnimatedBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(4, Byte), Integer))
        Me.XenonAnimatedBox1.Color = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(4, Byte), Integer))
        Me.XenonAnimatedBox1.Color1 = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(4, Byte), Integer))
        Me.XenonAnimatedBox1.Color2 = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.XenonAnimatedBox1.Controls.Add(Me.PictureBox1)
        Me.XenonAnimatedBox1.Controls.Add(Me.Label7)
        Me.XenonAnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XenonAnimatedBox1.Location = New System.Drawing.Point(0, 0)
        Me.XenonAnimatedBox1.Name = "XenonAnimatedBox1"
        Me.XenonAnimatedBox1.Size = New System.Drawing.Size(874, 48)
        Me.XenonAnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors
        Me.XenonAnimatedBox1.TabIndex = 121
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
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.DrawOnGlass = False
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(626, 448)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(115, 34)
        Me.XenonButton2.TabIndex = 117
        Me.XenonButton2.Text = "Don't modify"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.DrawOnGlass = False
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(747, 448)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(115, 34)
        Me.XenonButton1.TabIndex = 116
        Me.XenonButton1.Text = "Modify"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.XenonTreeView1)
        Me.XenonGroupBox3.Controls.Add(Me.Label4)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox4)
        Me.XenonGroupBox3.Location = New System.Drawing.Point(13, 59)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(850, 280)
        Me.XenonGroupBox3.TabIndex = 114
        '
        'XenonTreeView1
        '
        Me.XenonTreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.XenonTreeView1.Indent = 15
        Me.XenonTreeView1.ItemHeight = 20
        Me.XenonTreeView1.Location = New System.Drawing.Point(3, 32)
        Me.XenonTreeView1.Name = "XenonTreeView1"
        Me.XenonTreeView1.Size = New System.Drawing.Size(843, 245)
        Me.XenonTreeView1.TabIndex = 122
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
        Me.Controls.Add(Me.XenonButton4)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.XenonButton3)
        Me.Controls.Add(Me.XenonAlertBox1)
        Me.Controls.Add(Me.XenonCheckBox1)
        Me.Controls.Add(Me.XenonAnimatedBox1)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton1)
        Me.Controls.Add(Me.XenonGroupBox3)
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
        Me.XenonAnimatedBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox3.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents XenonGroupBox3 As UI.WP.GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents XenonButton1 As UI.WP.Button
    Friend WithEvents XenonButton2 As UI.WP.Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents XenonTreeView1 As UI.WP.TreeView
    Friend WithEvents XenonAnimatedBox1 As UI.WP.AnimatedBox
    Friend WithEvents XenonCheckBox1 As UI.WP.CheckBox
    Friend WithEvents XenonAlertBox1 As UI.WP.AlertBox
    Friend WithEvents XenonButton3 As UI.WP.Button
    Friend WithEvents XenonSeparator1 As UI.WP.SeparatorH
    Friend WithEvents XenonButton4 As UI.WP.Button
End Class
