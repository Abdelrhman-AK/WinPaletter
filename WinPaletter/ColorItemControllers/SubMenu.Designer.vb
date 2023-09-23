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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Separator4 = New WinPaletter.UI.WP.SeparatorH()
        Me.Button9 = New WinPaletter.UI.WP.Button()
        Me.Trackbar4 = New WinPaletter.UI.WP.Trackbar()
        Me.PreviousColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.Button8 = New WinPaletter.UI.WP.Button()
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.Button6 = New WinPaletter.UI.WP.Button()
        Me.Trackbar3 = New WinPaletter.UI.WP.Trackbar()
        Me.Trackbar2 = New WinPaletter.UI.WP.Trackbar()
        Me.Trackbar1 = New WinPaletter.UI.WP.Trackbar()
        Me.Separator2 = New WinPaletter.UI.WP.SeparatorH()
        Me.Button4 = New WinPaletter.UI.WP.Button()
        Me.Separator3 = New WinPaletter.UI.WP.SeparatorH()
        Me.InvertedColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.DefaultColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.MainColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.Button10 = New WinPaletter.UI.WP.Button()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Button3 = New WinPaletter.UI.WP.Button()
        Me.Button5 = New WinPaletter.UI.WP.Button()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(7, 209)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(37, 209)
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
        Me.PaletteContainer.Location = New System.Drawing.Point(203, 43)
        Me.PaletteContainer.Name = "PaletteContainer"
        Me.PaletteContainer.Size = New System.Drawing.Size(202, 214)
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
        Me.Panel1.Controls.Add(Me.Button10)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(410, 37)
        Me.Panel1.TabIndex = 64
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(200, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(207, 24)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Colors history:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(37, 153)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 24)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "Previous"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(7, 153)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 69
        Me.PictureBox2.TabStop = False
        '
        'Separator4
        '
        Me.Separator4.AlternativeLook = False
        Me.Separator4.Location = New System.Drawing.Point(6, 202)
        Me.Separator4.Name = "Separator4"
        Me.Separator4.Size = New System.Drawing.Size(176, 1)
        Me.Separator4.TabIndex = 74
        Me.Separator4.TabStop = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button9.DrawOnGlass = False
        Me.Button9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button9.ForeColor = System.Drawing.Color.White
        Me.Button9.Image = CType(resources.GetObject("Button9.Image"), System.Drawing.Image)
        Me.Button9.LineColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.Button9.LineSize = 1
        Me.Button9.Location = New System.Drawing.Point(157, 180)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(25, 19)
        Me.Button9.TabIndex = 73
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Trackbar4
        '
        Me.Trackbar4.LargeChange = 10
        Me.Trackbar4.Location = New System.Drawing.Point(6, 180)
        Me.Trackbar4.Maximum = 200
        Me.Trackbar4.Minimum = 0
        Me.Trackbar4.Name = "Trackbar4"
        Me.Trackbar4.Size = New System.Drawing.Size(145, 19)
        Me.Trackbar4.SmallChange = 1
        Me.Trackbar4.TabIndex = 72
        Me.Trackbar4.Value = 100
        '
        'PreviousColor
        '
        Me.PreviousColor.BackColor = System.Drawing.Color.Crimson
        Me.PreviousColor.DefaultColor = System.Drawing.Color.Black
        Me.PreviousColor.DontShowInfo = False
        Me.PreviousColor.Location = New System.Drawing.Point(97, 156)
        Me.PreviousColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PreviousColor.Name = "PreviousColor"
        Me.PreviousColor.Size = New System.Drawing.Size(85, 20)
        Me.PreviousColor.TabIndex = 71
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button8.DrawOnGlass = False
        Me.Button8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button8.ForeColor = System.Drawing.Color.White
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.LineColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.Button8.LineSize = 1
        Me.Button8.Location = New System.Drawing.Point(157, 236)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(25, 19)
        Me.Button8.TabIndex = 63
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button7.DrawOnGlass = False
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"), System.Drawing.Image)
        Me.Button7.LineColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.Button7.LineSize = 1
        Me.Button7.Location = New System.Drawing.Point(157, 123)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(25, 19)
        Me.Button7.TabIndex = 62
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button6.DrawOnGlass = False
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.LineColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.Button6.LineSize = 1
        Me.Button6.Location = New System.Drawing.Point(157, 68)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(25, 19)
        Me.Button6.TabIndex = 61
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Trackbar3
        '
        Me.Trackbar3.LargeChange = 10
        Me.Trackbar3.Location = New System.Drawing.Point(6, 236)
        Me.Trackbar3.Maximum = 200
        Me.Trackbar3.Minimum = 0
        Me.Trackbar3.Name = "Trackbar3"
        Me.Trackbar3.Size = New System.Drawing.Size(145, 19)
        Me.Trackbar3.SmallChange = 1
        Me.Trackbar3.TabIndex = 60
        Me.Trackbar3.Value = 100
        '
        'Trackbar2
        '
        Me.Trackbar2.LargeChange = 10
        Me.Trackbar2.Location = New System.Drawing.Point(6, 123)
        Me.Trackbar2.Maximum = 200
        Me.Trackbar2.Minimum = 0
        Me.Trackbar2.Name = "Trackbar2"
        Me.Trackbar2.Size = New System.Drawing.Size(145, 19)
        Me.Trackbar2.SmallChange = 1
        Me.Trackbar2.TabIndex = 59
        Me.Trackbar2.Value = 100
        '
        'Trackbar1
        '
        Me.Trackbar1.LargeChange = 10
        Me.Trackbar1.Location = New System.Drawing.Point(6, 68)
        Me.Trackbar1.Maximum = 200
        Me.Trackbar1.Minimum = 0
        Me.Trackbar1.Name = "Trackbar1"
        Me.Trackbar1.Size = New System.Drawing.Size(145, 19)
        Me.Trackbar1.SmallChange = 1
        Me.Trackbar1.TabIndex = 57
        Me.Trackbar1.Value = 100
        '
        'Separator2
        '
        Me.Separator2.AlternativeLook = False
        Me.Separator2.Location = New System.Drawing.Point(6, 146)
        Me.Separator2.Name = "Separator2"
        Me.Separator2.Size = New System.Drawing.Size(176, 1)
        Me.Separator2.TabIndex = 48
        Me.Separator2.TabStop = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button4.DrawOnGlass = False
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Image = Nothing
        Me.Button4.LineColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Button4.LineSize = 1
        Me.Button4.Location = New System.Drawing.Point(187, 42)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(12, 215)
        Me.Button4.TabIndex = 45
        Me.Button4.Text = ">"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Separator3
        '
        Me.Separator3.AlternativeLook = False
        Me.Separator3.Location = New System.Drawing.Point(6, 91)
        Me.Separator3.Name = "Separator3"
        Me.Separator3.Size = New System.Drawing.Size(176, 1)
        Me.Separator3.TabIndex = 44
        Me.Separator3.TabStop = False
        '
        'InvertedColor
        '
        Me.InvertedColor.BackColor = System.Drawing.Color.Crimson
        Me.InvertedColor.DefaultColor = System.Drawing.Color.Black
        Me.InvertedColor.DontShowInfo = False
        Me.InvertedColor.Location = New System.Drawing.Point(97, 212)
        Me.InvertedColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.InvertedColor.Name = "InvertedColor"
        Me.InvertedColor.Size = New System.Drawing.Size(85, 20)
        Me.InvertedColor.TabIndex = 25
        '
        'DefaultColor
        '
        Me.DefaultColor.BackColor = System.Drawing.Color.Crimson
        Me.DefaultColor.DefaultColor = System.Drawing.Color.Black
        Me.DefaultColor.DontShowInfo = False
        Me.DefaultColor.Location = New System.Drawing.Point(97, 99)
        Me.DefaultColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DefaultColor.Name = "DefaultColor"
        Me.DefaultColor.Size = New System.Drawing.Size(85, 20)
        Me.DefaultColor.TabIndex = 19
        '
        'MainColor
        '
        Me.MainColor.BackColor = System.Drawing.Color.Crimson
        Me.MainColor.DefaultColor = System.Drawing.Color.Black
        Me.MainColor.DontShowInfo = False
        Me.MainColor.Location = New System.Drawing.Point(97, 44)
        Me.MainColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MainColor.Name = "MainColor"
        Me.MainColor.Size = New System.Drawing.Size(85, 20)
        Me.MainColor.TabIndex = 4
        '
        'Button10
        '
        Me.Button10.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button10.DrawOnGlass = False
        Me.Button10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button10.ForeColor = System.Drawing.Color.White
        Me.Button10.Image = CType(resources.GetObject("Button10.Image"), System.Drawing.Image)
        Me.Button10.LineColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(89, Byte), Integer))
        Me.Button10.LineSize = 1
        Me.Button10.Location = New System.Drawing.Point(100, 4)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(30, 30)
        Me.Button10.TabIndex = 57
        Me.Button10.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Button2.LineSize = 1
        Me.Button2.Location = New System.Drawing.Point(4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(30, 30)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(36, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 30)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button3.DrawOnGlass = False
        Me.Button3.Enabled = False
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.LineColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Button3.LineSize = 1
        Me.Button3.Location = New System.Drawing.Point(68, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(30, 30)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.Button5.DrawOnGlass = False
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.LineColor = System.Drawing.Color.FromArgb(CType(CType(149, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Button5.LineSize = 1
        Me.Button5.Location = New System.Drawing.Point(132, 4)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(30, 30)
        Me.Button5.TabIndex = 55
        Me.Button5.UseVisualStyleBackColor = False
        '
        'SubMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(410, 262)
        Me.Controls.Add(Me.Separator4)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Trackbar4)
        Me.Controls.Add(Me.PreviousColor)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Trackbar3)
        Me.Controls.Add(Me.Trackbar2)
        Me.Controls.Add(Me.Trackbar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Separator2)
        Me.Controls.Add(Me.PaletteContainer)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Separator3)
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
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents Button3 As UI.WP.Button
    Friend WithEvents MainColor As UI.Controllers.ColorItem
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents DefaultColor As UI.Controllers.ColorItem
    Friend WithEvents InvertedColor As UI.Controllers.ColorItem
    Friend WithEvents Separator3 As UI.WP.SeparatorH
    Friend WithEvents Button4 As UI.WP.Button
    Friend WithEvents PaletteContainer As FlowLayoutPanel
    Friend WithEvents Separator2 As UI.WP.SeparatorH
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button5 As UI.WP.Button
    Friend WithEvents Trackbar1 As UI.WP.Trackbar
    Friend WithEvents Trackbar2 As UI.WP.Trackbar
    Friend WithEvents Trackbar3 As UI.WP.Trackbar
    Friend WithEvents Button6 As UI.WP.Button
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents Button8 As UI.WP.Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Separator4 As UI.WP.SeparatorH
    Friend WithEvents Button9 As UI.WP.Button
    Friend WithEvents Trackbar4 As UI.WP.Trackbar
    Friend WithEvents PreviousColor As UI.Controllers.ColorItem
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Button10 As UI.WP.Button
End Class
