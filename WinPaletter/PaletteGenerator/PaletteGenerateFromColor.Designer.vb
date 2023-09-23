<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PaletteGenerateFromColor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PaletteGenerateFromColor))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox4 = New WinPaletter.UI.WP.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.RadioButton6 = New WinPaletter.UI.WP.RadioButton()
        Me.RadioButton3 = New WinPaletter.UI.WP.RadioButton()
        Me.RadioButton4 = New WinPaletter.UI.WP.RadioButton()
        Me.RadioButton7 = New WinPaletter.UI.WP.RadioButton()
        Me.RadioButton5 = New WinPaletter.UI.WP.RadioButton()
        Me.GroupBox2 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.SelectedColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.val1 = New WinPaletter.UI.WP.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Trackbar1 = New WinPaletter.UI.WP.Trackbar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImgPaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.Button3 = New WinPaletter.UI.WP.Button()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp|All Files|*.*"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.PictureBox1)
        Me.GroupBox4.Controls.Add(Me.RadioButton6)
        Me.GroupBox4.Controls.Add(Me.RadioButton3)
        Me.GroupBox4.Controls.Add(Me.RadioButton4)
        Me.GroupBox4.Controls.Add(Me.RadioButton7)
        Me.GroupBox4.Controls.Add(Me.RadioButton5)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 109)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(610, 118)
        Me.GroupBox4.TabIndex = 169
        Me.GroupBox4.Text = "GroupBox4"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(33, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(305, 24)
        Me.Label3.TabIndex = 169
        Me.Label3.Text = "Options for extracted colors brightness:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 163
        Me.PictureBox1.TabStop = False
        '
        'RadioButton6
        '
        Me.RadioButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.RadioButton6.Checked = False
        Me.RadioButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton6.ForeColor = System.Drawing.Color.White
        Me.RadioButton6.Location = New System.Drawing.Point(328, 60)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(277, 24)
        Me.RadioButton6.TabIndex = 168
        Me.RadioButton6.Text = "Make colors darker"
        '
        'RadioButton3
        '
        Me.RadioButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.RadioButton3.Checked = True
        Me.RadioButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton3.ForeColor = System.Drawing.Color.White
        Me.RadioButton3.Location = New System.Drawing.Point(45, 30)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(277, 24)
        Me.RadioButton3.TabIndex = 164
        Me.RadioButton3.Text = "Don't change colors brightness"
        '
        'RadioButton4
        '
        Me.RadioButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.RadioButton4.Checked = False
        Me.RadioButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton4.ForeColor = System.Drawing.Color.White
        Me.RadioButton4.Location = New System.Drawing.Point(45, 90)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(277, 24)
        Me.RadioButton4.TabIndex = 165
        Me.RadioButton4.Text = "Make colors extremely bright"
        '
        'RadioButton7
        '
        Me.RadioButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.RadioButton7.Checked = False
        Me.RadioButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton7.ForeColor = System.Drawing.Color.White
        Me.RadioButton7.Location = New System.Drawing.Point(328, 90)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(277, 24)
        Me.RadioButton7.TabIndex = 167
        Me.RadioButton7.Text = "Make colors extremely dark"
        '
        'RadioButton5
        '
        Me.RadioButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.RadioButton5.Checked = False
        Me.RadioButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton5.ForeColor = System.Drawing.Color.White
        Me.RadioButton5.Location = New System.Drawing.Point(45, 60)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(277, 24)
        Me.RadioButton5.TabIndex = 166
        Me.RadioButton5.Text = "Make colors brighter"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.PictureBox7)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Controls.Add(Me.SelectedColor)
        Me.GroupBox2.Controls.Add(Me.PictureBox8)
        Me.GroupBox2.Controls.Add(Me.val1)
        Me.GroupBox2.Controls.Add(Me.PictureBox2)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Trackbar1)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(610, 91)
        Me.GroupBox2.TabIndex = 165
        Me.GroupBox2.Text = "GroupBox2"
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(3, 63)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 156
        Me.PictureBox7.TabStop = False
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.CheckBox1.Checked = False
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(33, 63)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(572, 24)
        Me.CheckBox1.TabIndex = 155
        Me.CheckBox1.Text = "Include inverted degrees of selected color to increase variety of extracted palet" &
    "te"
        '
        'SelectedColor
        '
        Me.SelectedColor.AllowDrop = True
        Me.SelectedColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.SelectedColor.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.SelectedColor.DontShowInfo = False
        Me.SelectedColor.Location = New System.Drawing.Point(106, 3)
        Me.SelectedColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.SelectedColor.Name = "SelectedColor"
        Me.SelectedColor.Size = New System.Drawing.Size(100, 24)
        Me.SelectedColor.TabIndex = 154
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(3, 33)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox8.TabIndex = 149
        Me.PictureBox8.TabStop = False
        '
        'val1
        '
        Me.val1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.val1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.val1.DrawOnGlass = False
        Me.val1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.val1.ForeColor = System.Drawing.Color.White
        Me.val1.Image = Nothing
        Me.val1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.val1.LineSize = 1
        Me.val1.Location = New System.Drawing.Point(572, 33)
        Me.val1.Name = "val1"
        Me.val1.Size = New System.Drawing.Size(34, 24)
        Me.val1.TabIndex = 153
        Me.val1.UseVisualStyleBackColor = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 142
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 24)
        Me.Label2.TabIndex = 141
        Me.Label2.Text = "Color"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Trackbar1
        '
        Me.Trackbar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Trackbar1.LargeChange = 10
        Me.Trackbar1.Location = New System.Drawing.Point(277, 36)
        Me.Trackbar1.Maximum = 100
        Me.Trackbar1.Minimum = 13
        Me.Trackbar1.Name = "Trackbar1"
        Me.Trackbar1.Size = New System.Drawing.Size(289, 19)
        Me.Trackbar1.SmallChange = 1
        Me.Trackbar1.TabIndex = 152
        Me.Trackbar1.Value = 13
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(33, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(238, 24)
        Me.Label6.TabIndex = 151
        Me.Label6.Text = "Minimum number of extracted colors"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.PictureBox5)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ImgPaletteContainer)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 240)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(610, 190)
        Me.GroupBox1.TabIndex = 164
        Me.GroupBox1.Text = "GroupBox1"
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 160
        Me.PictureBox5.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(572, 24)
        Me.Label1.TabIndex = 143
        Me.Label1.Text = "Extracted palette:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImgPaletteContainer
        '
        Me.ImgPaletteContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ImgPaletteContainer.AutoScroll = True
        Me.ImgPaletteContainer.Location = New System.Drawing.Point(3, 31)
        Me.ImgPaletteContainer.Name = "ImgPaletteContainer"
        Me.ImgPaletteContainer.Padding = New System.Windows.Forms.Padding(3)
        Me.ImgPaletteContainer.Size = New System.Drawing.Size(604, 156)
        Me.ImgPaletteContainer.TabIndex = 145
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 233)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(610, 1)
        Me.Separator1.TabIndex = 161
        Me.Separator1.TabStop = False
        Me.Separator1.Text = "Separator1"
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = Nothing
        Me.AlertBox1.Location = New System.Drawing.Point(12, 437)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(610, 24)
        Me.AlertBox1.TabIndex = 159
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "You may need to readjust colors after closing to make your theme colors better in" &
    " accessibility"
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Button3.DrawOnGlass = False
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = Nothing
        Me.Button3.LineColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Button3.LineSize = 1
        Me.Button3.Location = New System.Drawing.Point(245, 467)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(100, 32)
        Me.Button3.TabIndex = 158
        Me.Button3.Text = "Cancel"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Button2.LineSize = 1
        Me.Button2.Location = New System.Drawing.Point(351, 467)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 32)
        Me.Button2.TabIndex = 147
        Me.Button2.Text = "Done"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(457, 467)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(165, 32)
        Me.Button1.TabIndex = 146
        Me.Button1.Text = "Distribute randomly"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PaletteGenerateFromColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(634, 511)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PaletteGenerateFromColor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate a palette from color"
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ImgPaletteContainer As FlowLayoutPanel
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents val1 As UI.WP.Button
    Friend WithEvents Trackbar1 As UI.WP.Trackbar
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Button3 As UI.WP.Button
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents GroupBox1 As UI.WP.GroupBox
    Friend WithEvents GroupBox2 As UI.WP.GroupBox
    Friend WithEvents RadioButton6 As UI.WP.RadioButton
    Friend WithEvents RadioButton7 As UI.WP.RadioButton
    Friend WithEvents RadioButton5 As UI.WP.RadioButton
    Friend WithEvents RadioButton4 As UI.WP.RadioButton
    Friend WithEvents RadioButton3 As UI.WP.RadioButton
    Friend WithEvents GroupBox4 As UI.WP.GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents SelectedColor As UI.Controllers.ColorItem
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
End Class
