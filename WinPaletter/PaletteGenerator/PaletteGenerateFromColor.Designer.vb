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
        Me.XenonGroupBox4 = New WinPaletter.UI.WP.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.XenonRadioButton6 = New WinPaletter.UI.WP.RadioButton()
        Me.XenonRadioButton3 = New WinPaletter.UI.WP.RadioButton()
        Me.XenonRadioButton4 = New WinPaletter.UI.WP.RadioButton()
        Me.XenonRadioButton7 = New WinPaletter.UI.WP.RadioButton()
        Me.XenonRadioButton5 = New WinPaletter.UI.WP.RadioButton()
        Me.XenonGroupBox2 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.XenonCheckBox1 = New UI.WP.CheckBox()
        Me.SelectedColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.val1 = New WinPaletter.UI.WP.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.XenonTrackbar1 = New WinPaletter.UI.WP.Trackbar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.XenonGroupBox1 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImgPaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.XenonSeparator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.XenonAlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.XenonButton3 = New WinPaletter.UI.WP.Button()
        Me.XenonButton2 = New WinPaletter.UI.WP.Button()
        Me.XenonButton1 = New WinPaletter.UI.WP.Button()
        Me.XenonGroupBox4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox2.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox1.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp|All Files|*.*"
        '
        'XenonGroupBox4
        '
        Me.XenonGroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox4.Controls.Add(Me.Label3)
        Me.XenonGroupBox4.Controls.Add(Me.PictureBox1)
        Me.XenonGroupBox4.Controls.Add(Me.XenonRadioButton6)
        Me.XenonGroupBox4.Controls.Add(Me.XenonRadioButton3)
        Me.XenonGroupBox4.Controls.Add(Me.XenonRadioButton4)
        Me.XenonGroupBox4.Controls.Add(Me.XenonRadioButton7)
        Me.XenonGroupBox4.Controls.Add(Me.XenonRadioButton5)
        Me.XenonGroupBox4.Location = New System.Drawing.Point(12, 109)
        Me.XenonGroupBox4.Name = "XenonGroupBox4"
        Me.XenonGroupBox4.Size = New System.Drawing.Size(610, 118)
        Me.XenonGroupBox4.TabIndex = 169
        Me.XenonGroupBox4.Text = "XenonGroupBox4"
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
        'XenonRadioButton6
        '
        Me.XenonRadioButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonRadioButton6.Checked = False
        Me.XenonRadioButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton6.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton6.Location = New System.Drawing.Point(328, 60)
        Me.XenonRadioButton6.Name = "XenonRadioButton6"
        Me.XenonRadioButton6.Size = New System.Drawing.Size(277, 24)
        Me.XenonRadioButton6.TabIndex = 168
        Me.XenonRadioButton6.Text = "Make colors darker"
        '
        'XenonRadioButton3
        '
        Me.XenonRadioButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonRadioButton3.Checked = True
        Me.XenonRadioButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton3.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton3.Location = New System.Drawing.Point(45, 30)
        Me.XenonRadioButton3.Name = "XenonRadioButton3"
        Me.XenonRadioButton3.Size = New System.Drawing.Size(277, 24)
        Me.XenonRadioButton3.TabIndex = 164
        Me.XenonRadioButton3.Text = "Don't change colors brightness"
        '
        'XenonRadioButton4
        '
        Me.XenonRadioButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonRadioButton4.Checked = False
        Me.XenonRadioButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton4.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton4.Location = New System.Drawing.Point(45, 90)
        Me.XenonRadioButton4.Name = "XenonRadioButton4"
        Me.XenonRadioButton4.Size = New System.Drawing.Size(277, 24)
        Me.XenonRadioButton4.TabIndex = 165
        Me.XenonRadioButton4.Text = "Make colors extremely bright"
        '
        'XenonRadioButton7
        '
        Me.XenonRadioButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonRadioButton7.Checked = False
        Me.XenonRadioButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton7.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton7.Location = New System.Drawing.Point(328, 90)
        Me.XenonRadioButton7.Name = "XenonRadioButton7"
        Me.XenonRadioButton7.Size = New System.Drawing.Size(277, 24)
        Me.XenonRadioButton7.TabIndex = 167
        Me.XenonRadioButton7.Text = "Make colors extremely dark"
        '
        'XenonRadioButton5
        '
        Me.XenonRadioButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonRadioButton5.Checked = False
        Me.XenonRadioButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton5.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton5.Location = New System.Drawing.Point(45, 60)
        Me.XenonRadioButton5.Name = "XenonRadioButton5"
        Me.XenonRadioButton5.Size = New System.Drawing.Size(277, 24)
        Me.XenonRadioButton5.TabIndex = 166
        Me.XenonRadioButton5.Text = "Make colors brighter"
        '
        'XenonGroupBox2
        '
        Me.XenonGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox7)
        Me.XenonGroupBox2.Controls.Add(Me.XenonCheckBox1)
        Me.XenonGroupBox2.Controls.Add(Me.SelectedColor)
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox8)
        Me.XenonGroupBox2.Controls.Add(Me.val1)
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox2)
        Me.XenonGroupBox2.Controls.Add(Me.Label2)
        Me.XenonGroupBox2.Controls.Add(Me.XenonTrackbar1)
        Me.XenonGroupBox2.Controls.Add(Me.Label6)
        Me.XenonGroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.XenonGroupBox2.Name = "XenonGroupBox2"
        Me.XenonGroupBox2.Size = New System.Drawing.Size(610, 91)
        Me.XenonGroupBox2.TabIndex = 165
        Me.XenonGroupBox2.Text = "XenonGroupBox2"
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
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonCheckBox1.Checked = False
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(33, 63)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(572, 24)
        Me.XenonCheckBox1.TabIndex = 155
        Me.XenonCheckBox1.Text = "Include inverted degrees of selected color to increase variety of extracted palet" &
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
        'XenonTrackbar1
        '
        Me.XenonTrackbar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTrackbar1.LargeChange = 10
        Me.XenonTrackbar1.Location = New System.Drawing.Point(277, 36)
        Me.XenonTrackbar1.Maximum = 100
        Me.XenonTrackbar1.Minimum = 13
        Me.XenonTrackbar1.Name = "XenonTrackbar1"
        Me.XenonTrackbar1.Size = New System.Drawing.Size(289, 19)
        Me.XenonTrackbar1.SmallChange = 1
        Me.XenonTrackbar1.TabIndex = 152
        Me.XenonTrackbar1.Value = 13
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
        'XenonGroupBox1
        '
        Me.XenonGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox5)
        Me.XenonGroupBox1.Controls.Add(Me.Label1)
        Me.XenonGroupBox1.Controls.Add(Me.ImgPaletteContainer)
        Me.XenonGroupBox1.Location = New System.Drawing.Point(12, 240)
        Me.XenonGroupBox1.Name = "XenonGroupBox1"
        Me.XenonGroupBox1.Size = New System.Drawing.Size(610, 190)
        Me.XenonGroupBox1.TabIndex = 164
        Me.XenonGroupBox1.Text = "XenonGroupBox1"
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
        'XenonSeparator1
        '
        Me.XenonSeparator1.AlternativeLook = False
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 233)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(610, 1)
        Me.XenonSeparator1.TabIndex = 161
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.XenonAlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonAlertBox1.CenterText = False
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Nothing
        Me.XenonAlertBox1.Location = New System.Drawing.Point(12, 437)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(610, 24)
        Me.XenonAlertBox1.TabIndex = 159
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = "You may need to readjust colors after closing to make your theme colors better in" &
    " accessibility"
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonButton3.DrawOnGlass = False
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = Nothing
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(245, 467)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(100, 32)
        Me.XenonButton3.TabIndex = 158
        Me.XenonButton3.Text = "Cancel"
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonButton2.DrawOnGlass = False
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(351, 467)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(100, 32)
        Me.XenonButton2.TabIndex = 147
        Me.XenonButton2.Text = "Done"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonButton1.DrawOnGlass = False
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(457, 467)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(165, 32)
        Me.XenonButton1.TabIndex = 146
        Me.XenonButton1.Text = "Distribute randomly"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'PaletteGenerateFromColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(634, 511)
        Me.Controls.Add(Me.XenonGroupBox4)
        Me.Controls.Add(Me.XenonGroupBox2)
        Me.Controls.Add(Me.XenonGroupBox1)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.XenonAlertBox1)
        Me.Controls.Add(Me.XenonButton3)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PaletteGenerateFromColor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate a palette from color"
        Me.XenonGroupBox4.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox2.ResumeLayout(False)
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox1.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ImgPaletteContainer As FlowLayoutPanel
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents XenonButton1 As UI.WP.Button
    Friend WithEvents XenonButton2 As UI.WP.Button
    Friend WithEvents val1 As UI.WP.Button
    Friend WithEvents XenonTrackbar1 As UI.WP.Trackbar
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents XenonButton3 As UI.WP.Button
    Friend WithEvents XenonAlertBox1 As UI.WP.AlertBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents XenonSeparator1 As UI.WP.SeparatorH
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents XenonGroupBox1 As UI.WP.GroupBox
    Friend WithEvents XenonGroupBox2 As UI.WP.GroupBox
    Friend WithEvents XenonRadioButton6 As UI.WP.RadioButton
    Friend WithEvents XenonRadioButton7 As UI.WP.RadioButton
    Friend WithEvents XenonRadioButton5 As UI.WP.RadioButton
    Friend WithEvents XenonRadioButton4 As UI.WP.RadioButton
    Friend WithEvents XenonRadioButton3 As UI.WP.RadioButton
    Friend WithEvents XenonGroupBox4 As UI.WP.GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents SelectedColor As UI.Controllers.ColorItem
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents XenonCheckBox1 As UI.WP.CheckBox
End Class
