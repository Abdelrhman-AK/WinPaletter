<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ColorPicker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ColorPicker))
        Me.ColorEditorManager1 = New Cyotek.Windows.Forms.ColorEditorManager()
        Me.ColorEditor1 = New Cyotek.Windows.Forms.ColorEditor()
        Me.ColorGrid1 = New Cyotek.Windows.Forms.ColorGrid()
        Me.ColorWheel1 = New Cyotek.Windows.Forms.ColorWheel()
        Me.ScreenColorPicker1 = New Cyotek.Windows.Forms.ScreenColorPicker()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.XenonGroupBox3 = New WinPaletter.XenonGroupBox()
        Me.XenonButton6 = New WinPaletter.XenonButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.XenonNumericUpDown2 = New WinPaletter.XenonNumericUpDown()
        Me.XenonSeparator2 = New WinPaletter.XenonSeparator()
        Me.XenonCheckBox1 = New WinPaletter.XenonCheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.XenonNumericUpDown1 = New WinPaletter.XenonNumericUpDown()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.XenonButton4 = New WinPaletter.XenonButton()
        Me.TextBox1 = New WinPaletter.XenonTextBox()
        Me.XenonRadioButton2 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton1 = New WinPaletter.XenonRadioButton()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ImgPaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.XenonGroupBox1 = New WinPaletter.XenonGroupBox()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonGroupBox2 = New WinPaletter.XenonGroupBox()
        Me.XenonButton5 = New WinPaletter.XenonButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonButton8 = New WinPaletter.XenonButton()
        Me.XenonGroupBox3.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox1.SuspendLayout()
        Me.XenonGroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColorEditorManager1
        '
        Me.ColorEditorManager1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorEditorManager1.ColorEditor = Me.ColorEditor1
        Me.ColorEditorManager1.ColorGrid = Me.ColorGrid1
        Me.ColorEditorManager1.ColorWheel = Me.ColorWheel1
        Me.ColorEditorManager1.ScreenColorPicker = Me.ScreenColorPicker1
        '
        'ColorEditor1
        '
        Me.ColorEditor1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ColorEditor1.Location = New System.Drawing.Point(2, 2)
        Me.ColorEditor1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ColorEditor1.Name = "ColorEditor1"
        Me.ColorEditor1.ShowAlphaChannel = False
        Me.ColorEditor1.Size = New System.Drawing.Size(277, 238)
        Me.ColorEditor1.TabIndex = 0
        '
        'ColorGrid1
        '
        Me.ColorGrid1.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None
        Me.ColorGrid1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorGrid1.Location = New System.Drawing.Point(13, 58)
        Me.ColorGrid1.Name = "ColorGrid1"
        Me.ColorGrid1.Size = New System.Drawing.Size(247, 180)
        Me.ColorGrid1.TabIndex = 1
        Me.ColorGrid1.Visible = False
        '
        'ColorWheel1
        '
        Me.ColorWheel1.Alpha = 1.0R
        Me.ColorWheel1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorWheel1.Lightness = 0.37647059559822083R
        Me.ColorWheel1.Location = New System.Drawing.Point(13, 67)
        Me.ColorWheel1.Name = "ColorWheel1"
        Me.ColorWheel1.Size = New System.Drawing.Size(243, 233)
        Me.ColorWheel1.TabIndex = 2
        '
        'ScreenColorPicker1
        '
        Me.ScreenColorPicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScreenColorPicker1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ScreenColorPicker1.Location = New System.Drawing.Point(134, 654)
        Me.ScreenColorPicker1.Name = "ScreenColorPicker1"
        Me.ScreenColorPicker1.Size = New System.Drawing.Size(206, 34)
        Me.ScreenColorPicker1.Text = "Drag me and release to pick a color"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp|All Files|*.*"
        '
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton6)
        Me.XenonGroupBox3.Controls.Add(Me.Label7)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox9)
        Me.XenonGroupBox3.Controls.Add(Me.XenonNumericUpDown2)
        Me.XenonGroupBox3.Controls.Add(Me.XenonSeparator2)
        Me.XenonGroupBox3.Controls.Add(Me.XenonCheckBox1)
        Me.XenonGroupBox3.Controls.Add(Me.Label6)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox7)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox8)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox6)
        Me.XenonGroupBox3.Controls.Add(Me.Label5)
        Me.XenonGroupBox3.Controls.Add(Me.ProgressBar1)
        Me.XenonGroupBox3.Controls.Add(Me.XenonNumericUpDown1)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox5)
        Me.XenonGroupBox3.Controls.Add(Me.Label3)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox4)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox3)
        Me.XenonGroupBox3.Controls.Add(Me.XenonSeparator1)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton4)
        Me.XenonGroupBox3.Controls.Add(Me.TextBox1)
        Me.XenonGroupBox3.Controls.Add(Me.XenonRadioButton2)
        Me.XenonGroupBox3.Controls.Add(Me.XenonRadioButton1)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox2)
        Me.XenonGroupBox3.Controls.Add(Me.Label2)
        Me.XenonGroupBox3.Controls.Add(Me.ImgPaletteContainer)
        Me.XenonGroupBox3.Controls.Add(Me.Label4)
        Me.XenonGroupBox3.CustomColor = False
        Me.XenonGroupBox3.LineColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.XenonGroupBox3.LineSize = 1
        Me.XenonGroupBox3.Location = New System.Drawing.Point(13, 58)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(247, 455)
        Me.XenonGroupBox3.TabIndex = 8
        Me.XenonGroupBox3.Visible = False
        '
        'XenonButton6
        '
        Me.XenonButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.XenonButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton6.ForeColor = System.Drawing.Color.White
        Me.XenonButton6.Image = CType(resources.GetObject("XenonButton6.Image"), System.Drawing.Image)
        Me.XenonButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton6.LineColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.XenonButton6.LineSize = 1
        Me.XenonButton6.Location = New System.Drawing.Point(137, 237)
        Me.XenonButton6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton6.Name = "XenonButton6"
        Me.XenonButton6.Size = New System.Drawing.Size(105, 24)
        Me.XenonButton6.TabIndex = 28
        Me.XenonButton6.Text = "Extract"
        Me.XenonButton6.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(66, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 24)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Quality"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(36, 176)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox9.TabIndex = 26
        Me.PictureBox9.TabStop = False
        '
        'XenonNumericUpDown2
        '
        Me.XenonNumericUpDown2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonNumericUpDown2.EnabledCalc = True
        Me.XenonNumericUpDown2.LineColor = System.Drawing.Color.DodgerBlue
        Me.XenonNumericUpDown2.Location = New System.Drawing.Point(172, 176)
        Me.XenonNumericUpDown2.Max = 100
        Me.XenonNumericUpDown2.Min = 5
        Me.XenonNumericUpDown2.Name = "XenonNumericUpDown2"
        Me.XenonNumericUpDown2.Size = New System.Drawing.Size(70, 24)
        Me.XenonNumericUpDown2.TabIndex = 25
        Me.XenonNumericUpDown2.Text = "XenonNumericUpDown2"
        Me.XenonNumericUpDown2.UpDownStep = 1
        Me.XenonNumericUpDown2.Value = 10
        '
        'XenonSeparator2
        '
        Me.XenonSeparator2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator2.Location = New System.Drawing.Point(6, 232)
        Me.XenonSeparator2.Name = "XenonSeparator2"
        Me.XenonSeparator2.Size = New System.Drawing.Size(236, 1)
        Me.XenonSeparator2.TabIndex = 24
        Me.XenonSeparator2.TabStop = False
        Me.XenonSeparator2.Text = "XenonSeparator2"
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.XenonCheckBox1.Checked = True
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(66, 204)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(174, 24)
        Me.XenonCheckBox1.TabIndex = 9
        Me.XenonCheckBox1.Text = "Ignore White Colors"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(66, 148)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 24)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Maximum Colors"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(36, 204)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 22
        Me.PictureBox7.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(36, 148)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox8.TabIndex = 21
        Me.PictureBox8.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(3, 121)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 20
        Me.PictureBox6.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(33, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 24)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Options"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(92, 245)
        Me.ProgressBar1.MarqueeAnimationSpeed = 20
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(148, 8)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 9
        Me.ProgressBar1.Visible = False
        '
        'XenonNumericUpDown1
        '
        Me.XenonNumericUpDown1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonNumericUpDown1.EnabledCalc = True
        Me.XenonNumericUpDown1.LineColor = System.Drawing.Color.DodgerBlue
        Me.XenonNumericUpDown1.Location = New System.Drawing.Point(172, 148)
        Me.XenonNumericUpDown1.Max = 100
        Me.XenonNumericUpDown1.Min = 5
        Me.XenonNumericUpDown1.Name = "XenonNumericUpDown1"
        Me.XenonNumericUpDown1.Size = New System.Drawing.Size(70, 24)
        Me.XenonNumericUpDown1.TabIndex = 17
        Me.XenonNumericUpDown1.Text = "XenonNumericUpDown1"
        Me.XenonNumericUpDown1.UpDownStep = 1
        Me.XenonNumericUpDown1.Value = 15
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(3, 237)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 16
        Me.PictureBox5.TabStop = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(33, 237)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 24)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Palette"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(36, 58)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 14
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(36, 30)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 13
        Me.PictureBox3.TabStop = False
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(6, 116)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(236, 1)
        Me.XenonSeparator1.TabIndex = 11
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonButton4
        '
        Me.XenonButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(206, 87)
        Me.XenonButton4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(36, 23)
        Me.XenonButton4.TabIndex = 10
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Hint = Nothing
        Me.TextBox1.LineColor = System.Drawing.Color.DodgerBlue
        Me.TextBox1.Location = New System.Drawing.Point(66, 87)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Multiline = False
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = False
        Me.TextBox1.Size = New System.Drawing.Size(138, 24)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox1.UseSystemPasswordChar = False
        '
        'XenonRadioButton2
        '
        Me.XenonRadioButton2.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonRadioButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.XenonRadioButton2.Checked = False
        Me.XenonRadioButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton2.Location = New System.Drawing.Point(66, 58)
        Me.XenonRadioButton2.Name = "XenonRadioButton2"
        Me.XenonRadioButton2.Size = New System.Drawing.Size(142, 24)
        Me.XenonRadioButton2.TabIndex = 8
        Me.XenonRadioButton2.Text = "Browse for an image"
        '
        'XenonRadioButton1
        '
        Me.XenonRadioButton1.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonRadioButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.XenonRadioButton1.Checked = True
        Me.XenonRadioButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton1.Location = New System.Drawing.Point(66, 30)
        Me.XenonRadioButton1.Name = "XenonRadioButton1"
        Me.XenonRadioButton1.Size = New System.Drawing.Size(142, 24)
        Me.XenonRadioButton1.TabIndex = 7
        Me.XenonRadioButton1.Text = "Current Wallpaper"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 24)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Select a mode"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImgPaletteContainer
        '
        Me.ImgPaletteContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ImgPaletteContainer.AutoScroll = True
        Me.ImgPaletteContainer.Location = New System.Drawing.Point(6, 267)
        Me.ImgPaletteContainer.Name = "ImgPaletteContainer"
        Me.ImgPaletteContainer.Size = New System.Drawing.Size(236, 181)
        Me.ImgPaletteContainer.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 267)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(236, 181)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Extracting palette from image depends on your device's performance, maximum palet" &
    "te colors number, image quality and its resolution ..."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'XenonGroupBox1
        '
        Me.XenonGroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.XenonGroupBox1.Controls.Add(Me.ColorEditor1)
        Me.XenonGroupBox1.CustomColor = False
        Me.XenonGroupBox1.LineColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.XenonGroupBox1.LineSize = 1
        Me.XenonGroupBox1.Location = New System.Drawing.Point(264, 58)
        Me.XenonGroupBox1.Name = "XenonGroupBox1"
        Me.XenonGroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 5, 2)
        Me.XenonGroupBox1.Size = New System.Drawing.Size(284, 242)
        Me.XenonGroupBox1.TabIndex = 7
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = Nothing
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(347, 654)
        Me.XenonButton3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(99, 34)
        Me.XenonButton3.TabIndex = 5
        Me.XenonButton3.Text = "Cancel"
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(449, 654)
        Me.XenonButton2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(99, 34)
        Me.XenonButton2.TabIndex = 4
        Me.XenonButton2.Text = "Select"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonGroupBox2
        '
        Me.XenonGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.XenonGroupBox2.Controls.Add(Me.XenonButton5)
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox1)
        Me.XenonGroupBox2.Controls.Add(Me.Label1)
        Me.XenonGroupBox2.Controls.Add(Me.XenonButton1)
        Me.XenonGroupBox2.Controls.Add(Me.XenonButton8)
        Me.XenonGroupBox2.CustomColor = False
        Me.XenonGroupBox2.LineColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.XenonGroupBox2.LineSize = 1
        Me.XenonGroupBox2.Location = New System.Drawing.Point(13, 12)
        Me.XenonGroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonGroupBox2.Name = "XenonGroupBox2"
        Me.XenonGroupBox2.Size = New System.Drawing.Size(535, 40)
        Me.XenonGroupBox2.TabIndex = 3
        Me.XenonGroupBox2.Text = "XenonGroupBox2"
        '
        'XenonButton5
        '
        Me.XenonButton5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.XenonButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton5.ForeColor = System.Drawing.Color.White
        Me.XenonButton5.Image = Nothing
        Me.XenonButton5.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton5.LineSize = 1
        Me.XenonButton5.Location = New System.Drawing.Point(223, 3)
        Me.XenonButton5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton5.Name = "XenonButton5"
        Me.XenonButton5.Size = New System.Drawing.Size(143, 34)
        Me.XenonButton5.TabIndex = 6
        Me.XenonButton5.Text = "Get Palette from Image"
        Me.XenonButton5.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(44, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 35)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Methods"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = Nothing
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(367, 3)
        Me.XenonButton1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(77, 34)
        Me.XenonButton1.TabIndex = 1
        Me.XenonButton1.Text = "Color Grid"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonButton8
        '
        Me.XenonButton8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton8.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.XenonButton8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton8.ForeColor = System.Drawing.Color.White
        Me.XenonButton8.Image = Nothing
        Me.XenonButton8.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton8.LineSize = 1
        Me.XenonButton8.Location = New System.Drawing.Point(445, 3)
        Me.XenonButton8.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton8.Name = "XenonButton8"
        Me.XenonButton8.Size = New System.Drawing.Size(87, 34)
        Me.XenonButton8.TabIndex = 0
        Me.XenonButton8.Text = "Color Wheel"
        Me.XenonButton8.UseVisualStyleBackColor = False
        '
        'ColorPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(561, 701)
        Me.Controls.Add(Me.XenonGroupBox3)
        Me.Controls.Add(Me.XenonGroupBox1)
        Me.Controls.Add(Me.ColorGrid1)
        Me.Controls.Add(Me.XenonButton3)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonGroupBox2)
        Me.Controls.Add(Me.ScreenColorPicker1)
        Me.Controls.Add(Me.ColorWheel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "ColorPicker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Color Picker"
        Me.XenonGroupBox3.ResumeLayout(False)
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox1.ResumeLayout(False)
        Me.XenonGroupBox2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ColorEditor1 As Cyotek.Windows.Forms.ColorEditor
    Friend WithEvents ColorEditorManager1 As Cyotek.Windows.Forms.ColorEditorManager
    Friend WithEvents ColorGrid1 As Cyotek.Windows.Forms.ColorGrid
    Friend WithEvents ColorWheel1 As Cyotek.Windows.Forms.ColorWheel
    Friend WithEvents ScreenColorPicker1 As Cyotek.Windows.Forms.ScreenColorPicker
    Friend WithEvents XenonGroupBox2 As XenonGroupBox
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonButton8 As XenonButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents XenonButton5 As XenonButton
    Friend WithEvents XenonGroupBox1 As XenonGroupBox
    Friend WithEvents XenonGroupBox3 As XenonGroupBox
    Friend WithEvents XenonButton4 As XenonButton
    Friend WithEvents TextBox1 As XenonTextBox
    Friend WithEvents XenonRadioButton2 As XenonRadioButton
    Friend WithEvents XenonRadioButton1 As XenonRadioButton
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents XenonSeparator1 As XenonSeparator
    Friend WithEvents ImgPaletteContainer As FlowLayoutPanel
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents XenonNumericUpDown1 As XenonNumericUpDown
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label4 As Label
    Friend WithEvents XenonCheckBox1 As XenonCheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents XenonSeparator2 As XenonSeparator
    Friend WithEvents Label7 As Label
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents XenonNumericUpDown2 As XenonNumericUpDown
    Friend WithEvents XenonButton6 As XenonButton
End Class
