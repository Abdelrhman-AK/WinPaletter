<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SubMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SubMenu))
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.XenonButton8 = New WinPaletter.XenonButton()
        Me.XenonButton7 = New WinPaletter.XenonButton()
        Me.XenonButton6 = New WinPaletter.XenonButton()
        Me.XenonTrackbar3 = New WinPaletter.XenonTrackbar()
        Me.XenonTrackbar2 = New WinPaletter.XenonTrackbar()
        Me.XenonTrackbar1 = New WinPaletter.XenonTrackbar()
        Me.XenonSeparator2 = New WinPaletter.XenonSeparator()
        Me.XenonButton4 = New WinPaletter.XenonButton()
        Me.XenonSeparator3 = New WinPaletter.XenonSeparator()
        Me.InvertedColor = New WinPaletter.XenonCP()
        Me.DefaultColor = New WinPaletter.XenonCP()
        Me.MainColor = New WinPaletter.XenonCP()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonComboBox1 = New WinPaletter.XenonComboBox()
        Me.XenonButton5 = New WinPaletter.XenonButton()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(7, 149)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(37, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 24)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Inverted"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(37, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 24)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Default"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(7, 96)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 13
        Me.PictureBox5.TabStop = False
        '
        'PaletteContainer
        '
        Me.PaletteContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PaletteContainer.AutoScroll = True
        Me.PaletteContainer.Location = New System.Drawing.Point(200, 41)
        Me.PaletteContainer.Name = "PaletteContainer"
        Me.PaletteContainer.Size = New System.Drawing.Size(207, 158)
        Me.PaletteContainer.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(37, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 24)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "Current"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(7, 42)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 53
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.XenonButton2)
        Me.Panel1.Controls.Add(Me.XenonButton1)
        Me.Panel1.Controls.Add(Me.XenonButton3)
        Me.Panel1.Controls.Add(Me.XenonComboBox1)
        Me.Panel1.Controls.Add(Me.XenonButton5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(413, 37)
        Me.Panel1.TabIndex = 64
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.Location = New System.Drawing.Point(6, 198)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(176, 1)
        Me.XenonSeparator1.TabIndex = 68
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonButton8
        '
        Me.XenonButton8.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.XenonButton8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton8.ForeColor = System.Drawing.Color.White
        Me.XenonButton8.Image = CType(resources.GetObject("XenonButton8.Image"), System.Drawing.Image)
        Me.XenonButton8.LineColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.XenonButton8.LineSize = 1
        Me.XenonButton8.Location = New System.Drawing.Point(157, 176)
        Me.XenonButton8.Name = "XenonButton8"
        Me.XenonButton8.Size = New System.Drawing.Size(25, 19)
        Me.XenonButton8.TabIndex = 63
        Me.XenonButton8.UseVisualStyleBackColor = False
        '
        'XenonButton7
        '
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = CType(resources.GetObject("XenonButton7.Image"), System.Drawing.Image)
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(157, 123)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(25, 19)
        Me.XenonButton7.TabIndex = 62
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonButton6
        '
        Me.XenonButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.XenonButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton6.ForeColor = System.Drawing.Color.White
        Me.XenonButton6.Image = CType(resources.GetObject("XenonButton6.Image"), System.Drawing.Image)
        Me.XenonButton6.LineColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.XenonButton6.LineSize = 1
        Me.XenonButton6.Location = New System.Drawing.Point(157, 68)
        Me.XenonButton6.Name = "XenonButton6"
        Me.XenonButton6.Size = New System.Drawing.Size(25, 19)
        Me.XenonButton6.TabIndex = 61
        Me.XenonButton6.UseVisualStyleBackColor = False
        '
        'XenonTrackbar3
        '
        Me.XenonTrackbar3.LargeChange = 10
        Me.XenonTrackbar3.Location = New System.Drawing.Point(6, 176)
        Me.XenonTrackbar3.Maximum = 200
        Me.XenonTrackbar3.Minimum = 0
        Me.XenonTrackbar3.Name = "XenonTrackbar3"
        Me.XenonTrackbar3.Size = New System.Drawing.Size(145, 19)
        Me.XenonTrackbar3.SmallChange = 1
        Me.XenonTrackbar3.TabIndex = 60
        Me.XenonTrackbar3.Text = "XenonTrackbar3"
        Me.XenonTrackbar3.Value = 100
        '
        'XenonTrackbar2
        '
        Me.XenonTrackbar2.LargeChange = 10
        Me.XenonTrackbar2.Location = New System.Drawing.Point(6, 123)
        Me.XenonTrackbar2.Maximum = 200
        Me.XenonTrackbar2.Minimum = 0
        Me.XenonTrackbar2.Name = "XenonTrackbar2"
        Me.XenonTrackbar2.Size = New System.Drawing.Size(145, 19)
        Me.XenonTrackbar2.SmallChange = 1
        Me.XenonTrackbar2.TabIndex = 59
        Me.XenonTrackbar2.Text = "XenonTrackbar2"
        Me.XenonTrackbar2.Value = 100
        '
        'XenonTrackbar1
        '
        Me.XenonTrackbar1.LargeChange = 10
        Me.XenonTrackbar1.Location = New System.Drawing.Point(6, 68)
        Me.XenonTrackbar1.Maximum = 200
        Me.XenonTrackbar1.Minimum = 0
        Me.XenonTrackbar1.Name = "XenonTrackbar1"
        Me.XenonTrackbar1.Size = New System.Drawing.Size(145, 19)
        Me.XenonTrackbar1.SmallChange = 1
        Me.XenonTrackbar1.TabIndex = 57
        Me.XenonTrackbar1.Text = "XenonTrackbar1"
        Me.XenonTrackbar1.Value = 100
        '
        'XenonSeparator2
        '
        Me.XenonSeparator2.Location = New System.Drawing.Point(6, 146)
        Me.XenonSeparator2.Name = "XenonSeparator2"
        Me.XenonSeparator2.Size = New System.Drawing.Size(176, 1)
        Me.XenonSeparator2.TabIndex = 48
        Me.XenonSeparator2.TabStop = False
        Me.XenonSeparator2.Text = "XenonSeparator2"
        '
        'XenonButton4
        '
        Me.XenonButton4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = Nothing
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(185, 42)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(12, 158)
        Me.XenonButton4.TabIndex = 45
        Me.XenonButton4.Text = ">"
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'XenonSeparator3
        '
        Me.XenonSeparator3.Location = New System.Drawing.Point(6, 91)
        Me.XenonSeparator3.Name = "XenonSeparator3"
        Me.XenonSeparator3.Size = New System.Drawing.Size(176, 1)
        Me.XenonSeparator3.TabIndex = 44
        Me.XenonSeparator3.TabStop = False
        Me.XenonSeparator3.Text = "XenonSeparator3"
        '
        'InvertedColor
        '
        Me.InvertedColor.BackColor = System.Drawing.Color.Crimson
        Me.InvertedColor.DefaultColor = System.Drawing.Color.Black
        Me.InvertedColor.ForceNoNerd = False
        Me.InvertedColor.Location = New System.Drawing.Point(97, 152)
        Me.InvertedColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.InvertedColor.Name = "InvertedColor"
        Me.InvertedColor.Size = New System.Drawing.Size(85, 20)
        Me.InvertedColor.TabIndex = 25
        Me.InvertedColor.Text = "XenonGroupBox15"
        '
        'DefaultColor
        '
        Me.DefaultColor.BackColor = System.Drawing.Color.Crimson
        Me.DefaultColor.DefaultColor = System.Drawing.Color.Black
        Me.DefaultColor.ForceNoNerd = False
        Me.DefaultColor.Location = New System.Drawing.Point(97, 99)
        Me.DefaultColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DefaultColor.Name = "DefaultColor"
        Me.DefaultColor.Size = New System.Drawing.Size(85, 20)
        Me.DefaultColor.TabIndex = 19
        Me.DefaultColor.Text = "XenonGroupBox15"
        '
        'MainColor
        '
        Me.MainColor.BackColor = System.Drawing.Color.Crimson
        Me.MainColor.DefaultColor = System.Drawing.Color.Black
        Me.MainColor.ForceNoNerd = False
        Me.MainColor.Location = New System.Drawing.Point(97, 44)
        Me.MainColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MainColor.Name = "MainColor"
        Me.MainColor.Size = New System.Drawing.Size(85, 20)
        Me.MainColor.TabIndex = 4
        Me.MainColor.Text = "XenonGroupBox15"
        '
        'XenonButton2
        '
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = CType(resources.GetObject("XenonButton2.Image"), System.Drawing.Image)
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(4, 4)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(30, 30)
        Me.XenonButton2.TabIndex = 1
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(36, 4)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(30, 30)
        Me.XenonButton1.TabIndex = 0
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonButton3
        '
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XenonButton3.Enabled = False
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = CType(resources.GetObject("XenonButton3.Image"), System.Drawing.Image)
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(68, 4)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(30, 30)
        Me.XenonButton3.TabIndex = 2
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'XenonComboBox1
        '
        Me.XenonComboBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonComboBox1.CustomFont = False
        Me.XenonComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.XenonComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.XenonComboBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonComboBox1.ForeColor = System.Drawing.Color.White
        Me.XenonComboBox1.FormattingEnabled = True
        Me.XenonComboBox1.Items.AddRange(New Object() {"Your Current Palette", "Windows 11 Palette", "Windows 10 Palette", "Windows 8.1 Palette", "Windows 7 Palette"})
        Me.XenonComboBox1.Location = New System.Drawing.Point(200, 7)
        Me.XenonComboBox1.Name = "XenonComboBox1"
        Me.XenonComboBox1.Size = New System.Drawing.Size(207, 24)
        Me.XenonComboBox1.TabIndex = 47
        '
        'XenonButton5
        '
        Me.XenonButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XenonButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton5.ForeColor = System.Drawing.Color.White
        Me.XenonButton5.Image = CType(resources.GetObject("XenonButton5.Image"), System.Drawing.Image)
        Me.XenonButton5.LineColor = System.Drawing.Color.FromArgb(CType(CType(149, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonButton5.LineSize = 1
        Me.XenonButton5.Location = New System.Drawing.Point(100, 4)
        Me.XenonButton5.Name = "XenonButton5"
        Me.XenonButton5.Size = New System.Drawing.Size(30, 30)
        Me.XenonButton5.TabIndex = 55
        Me.XenonButton5.UseVisualStyleBackColor = False
        '
        'SubMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(413, 205)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.XenonButton8)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonButton6)
        Me.Controls.Add(Me.XenonTrackbar3)
        Me.Controls.Add(Me.XenonTrackbar2)
        Me.Controls.Add(Me.XenonTrackbar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.XenonSeparator2)
        Me.Controls.Add(Me.PaletteContainer)
        Me.Controls.Add(Me.XenonButton4)
        Me.Controls.Add(Me.XenonSeparator3)
        Me.Controls.Add(Me.InvertedColor)
        Me.Controls.Add(Me.DefaultColor)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.MainColor)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SubMenu"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sub Menu"
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents MainColor As XenonCP
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents DefaultColor As XenonCP
    Friend WithEvents InvertedColor As XenonCP
    Friend WithEvents XenonSeparator3 As XenonSeparator
    Friend WithEvents XenonButton4 As XenonButton
    Friend WithEvents PaletteContainer As FlowLayoutPanel
    Friend WithEvents XenonComboBox1 As XenonComboBox
    Friend WithEvents XenonSeparator2 As XenonSeparator
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents XenonButton5 As XenonButton
    Friend WithEvents XenonTrackbar1 As XenonTrackbar
    Friend WithEvents XenonTrackbar2 As XenonTrackbar
    Friend WithEvents XenonTrackbar3 As XenonTrackbar
    Friend WithEvents XenonButton6 As XenonButton
    Friend WithEvents XenonButton7 As XenonButton
    Friend WithEvents XenonButton8 As XenonButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents XenonSeparator1 As XenonSeparator
End Class
