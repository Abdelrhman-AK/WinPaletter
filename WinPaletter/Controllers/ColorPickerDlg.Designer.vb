<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ColorPickerDlg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ColorPickerDlg))
        Me.ColorEditorManager1 = New Cyotek.Windows.Forms.ColorEditorManager()
        Me.ColorEditor1 = New Cyotek.Windows.Forms.ColorEditor()
        Me.ColorGrid1 = New Cyotek.Windows.Forms.ColorGrid()
        Me.ColorWheel1 = New Cyotek.Windows.Forms.ColorWheel()
        Me.ScreenColorPicker1 = New Cyotek.Windows.Forms.ScreenColorPicker()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.XenonButton6 = New WinPaletter.XenonButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.XenonNumericUpDown2 = New WinPaletter.XenonNumericUpDown()
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
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonTabControl1 = New WinPaletter.XenonTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.XenonSeparator2 = New WinPaletter.XenonSeparator()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.XenonSeparator3 = New WinPaletter.XenonSeparator()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.XenonButton9 = New WinPaletter.XenonButton()
        Me.XenonComboBox1 = New WinPaletter.XenonComboBox()
        Me.PictureBox33 = New System.Windows.Forms.PictureBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.XenonButton5 = New WinPaletter.XenonButton()
        Me.XenonTextBox1 = New WinPaletter.XenonTextBox()
        Me.XenonButton7 = New WinPaletter.XenonButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ThemePaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.XenonComboBox2 = New WinPaletter.XenonComboBox()
        Me.PaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenThemeDialog = New System.Windows.Forms.OpenFileDialog()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonTabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColorEditorManager1
        '
        Me.ColorEditorManager1.ColorEditor = Me.ColorEditor1
        Me.ColorEditorManager1.ColorGrid = Me.ColorGrid1
        Me.ColorEditorManager1.ColorWheel = Me.ColorWheel1
        Me.ColorEditorManager1.ScreenColorPicker = Me.ScreenColorPicker1
        '
        'ColorEditor1
        '
        Me.ColorEditor1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorEditor1.Location = New System.Drawing.Point(217, 35)
        Me.ColorEditor1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ColorEditor1.Name = "ColorEditor1"
        Me.ColorEditor1.Size = New System.Drawing.Size(235, 267)
        Me.ColorEditor1.TabIndex = 0
        '
        'ColorGrid1
        '
        Me.ColorGrid1.AutoAddColors = False
        Me.ColorGrid1.CellBorderColor = System.Drawing.Color.DimGray
        Me.ColorGrid1.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None
        Me.ColorGrid1.CellSize = New System.Drawing.Size(12, 13)
        Me.ColorGrid1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorGrid1.Columns = 13
        Me.ColorGrid1.Location = New System.Drawing.Point(8, 35)
        Me.ColorGrid1.Name = "ColorGrid1"
        Me.ColorGrid1.Size = New System.Drawing.Size(202, 223)
        Me.ColorGrid1.TabIndex = 1
        '
        'ColorWheel1
        '
        Me.ColorWheel1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorWheel1.Location = New System.Drawing.Point(217, 308)
        Me.ColorWheel1.Name = "ColorWheel1"
        Me.ColorWheel1.Size = New System.Drawing.Size(235, 198)
        Me.ColorWheel1.TabIndex = 2
        '
        'ScreenColorPicker1
        '
        Me.ScreenColorPicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScreenColorPicker1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ScreenColorPicker1.Location = New System.Drawing.Point(12, 559)
        Me.ScreenColorPicker1.Name = "ScreenColorPicker1"
        Me.ScreenColorPicker1.Size = New System.Drawing.Size(234, 34)
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
        'XenonButton6
        '
        Me.XenonButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton6.ForeColor = System.Drawing.Color.White
        Me.XenonButton6.Image = CType(resources.GetObject("XenonButton6.Image"), System.Drawing.Image)
        Me.XenonButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton6.LineColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.XenonButton6.LineSize = 1
        Me.XenonButton6.Location = New System.Drawing.Point(351, 277)
        Me.XenonButton6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton6.Name = "XenonButton6"
        Me.XenonButton6.Size = New System.Drawing.Size(99, 35)
        Me.XenonButton6.TabIndex = 28
        Me.XenonButton6.Text = "Extract"
        Me.XenonButton6.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(80, 210)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(265, 24)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Quality"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(50, 210)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox9.TabIndex = 26
        Me.PictureBox9.TabStop = False
        '
        'XenonNumericUpDown2
        '
        Me.XenonNumericUpDown2.EnabledCalc = True
        Me.XenonNumericUpDown2.Location = New System.Drawing.Point(351, 210)
        Me.XenonNumericUpDown2.Max = 100
        Me.XenonNumericUpDown2.Min = 5
        Me.XenonNumericUpDown2.Name = "XenonNumericUpDown2"
        Me.XenonNumericUpDown2.Size = New System.Drawing.Size(99, 24)
        Me.XenonNumericUpDown2.TabIndex = 25
        Me.XenonNumericUpDown2.Text = "XenonNumericUpDown2"
        Me.XenonNumericUpDown2.UpDownStep = 1
        Me.XenonNumericUpDown2.Value = 10
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox1.Checked = True
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(83, 240)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(367, 24)
        Me.XenonCheckBox1.TabIndex = 9
        Me.XenonCheckBox1.Text = "Ignore white colors"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(80, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(265, 24)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Maximum colors"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(50, 240)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 22
        Me.PictureBox7.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(50, 180)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox8.TabIndex = 21
        Me.PictureBox8.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(6, 142)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 20
        Me.PictureBox6.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(47, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(406, 35)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Options"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(50, 315)
        Me.ProgressBar1.MarqueeAnimationSpeed = 20
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(400, 5)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 9
        Me.ProgressBar1.Visible = False
        '
        'XenonNumericUpDown1
        '
        Me.XenonNumericUpDown1.EnabledCalc = True
        Me.XenonNumericUpDown1.Location = New System.Drawing.Point(351, 180)
        Me.XenonNumericUpDown1.Max = 100
        Me.XenonNumericUpDown1.Min = 5
        Me.XenonNumericUpDown1.Name = "XenonNumericUpDown1"
        Me.XenonNumericUpDown1.Size = New System.Drawing.Size(99, 24)
        Me.XenonNumericUpDown1.TabIndex = 17
        Me.XenonNumericUpDown1.Text = "XenonNumericUpDown1"
        Me.XenonNumericUpDown1.UpDownStep = 1
        Me.XenonNumericUpDown1.Value = 15
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(6, 277)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 16
        Me.PictureBox5.TabStop = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(47, 277)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(406, 35)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Palette"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(50, 74)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 14
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(50, 44)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 13
        Me.PictureBox3.TabStop = False
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.AlternativeLook = False
        Me.XenonSeparator1.Location = New System.Drawing.Point(6, 133)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(447, 1)
        Me.XenonSeparator1.TabIndex = 11
        Me.XenonSeparator1.TabStop = False
        '
        'XenonButton4
        '
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(351, 104)
        Me.XenonButton4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(99, 24)
        Me.XenonButton4.TabIndex = 10
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(80, 104)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Multiline = False
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = False
        Me.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.SelectionLength = 0
        Me.TextBox1.SelectionStart = 0
        Me.TextBox1.Size = New System.Drawing.Size(264, 24)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox1.UseSystemPasswordChar = False
        Me.TextBox1.WordWrap = True
        '
        'XenonRadioButton2
        '
        Me.XenonRadioButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonRadioButton2.Checked = False
        Me.XenonRadioButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton2.Location = New System.Drawing.Point(80, 74)
        Me.XenonRadioButton2.Name = "XenonRadioButton2"
        Me.XenonRadioButton2.Size = New System.Drawing.Size(370, 24)
        Me.XenonRadioButton2.TabIndex = 8
        Me.XenonRadioButton2.Text = "Browse for an image"
        '
        'XenonRadioButton1
        '
        Me.XenonRadioButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonRadioButton1.Checked = True
        Me.XenonRadioButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton1.Location = New System.Drawing.Point(80, 44)
        Me.XenonRadioButton1.Name = "XenonRadioButton1"
        Me.XenonRadioButton1.Size = New System.Drawing.Size(370, 24)
        Me.XenonRadioButton1.TabIndex = 7
        Me.XenonRadioButton1.Text = "Current wallpaper"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(47, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(406, 35)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Select a mode"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImgPaletteContainer
        '
        Me.ImgPaletteContainer.AutoScroll = True
        Me.ImgPaletteContainer.Location = New System.Drawing.Point(6, 325)
        Me.ImgPaletteContainer.Name = "ImgPaletteContainer"
        Me.ImgPaletteContainer.Size = New System.Drawing.Size(445, 154)
        Me.ImgPaletteContainer.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 325)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(445, 154)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Extracting palette from image depends on your device's performance, maximum palet" &
    "te colors number, image quality and its resolution ..."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = Nothing
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(253, 559)
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
        Me.XenonButton2.Location = New System.Drawing.Point(355, 559)
        Me.XenonButton2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(99, 34)
        Me.XenonButton2.TabIndex = 4
        Me.XenonButton2.Text = "Select"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonTabControl1
        '
        Me.XenonTabControl1.Controls.Add(Me.TabPage1)
        Me.XenonTabControl1.Controls.Add(Me.TabPage2)
        Me.XenonTabControl1.Controls.Add(Me.TabPage3)
        Me.XenonTabControl1.Controls.Add(Me.TabPage4)
        Me.XenonTabControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XenonTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.XenonTabControl1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonTabControl1.ItemSize = New System.Drawing.Size(110, 30)
        Me.XenonTabControl1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XenonTabControl1.Multiline = True
        Me.XenonTabControl1.Name = "XenonTabControl1"
        Me.XenonTabControl1.SelectedIndex = 0
        Me.XenonTabControl1.Size = New System.Drawing.Size(467, 554)
        Me.XenonTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.XenonTabControl1.TabIndex = 9
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.XenonButton1)
        Me.TabPage1.Controls.Add(Me.ColorWheel1)
        Me.TabPage1.Controls.Add(Me.ColorEditor1)
        Me.TabPage1.Controls.Add(Me.ColorGrid1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 34)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(459, 516)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Common"
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(4, 4)
        Me.XenonButton1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(344, 26)
        Me.XenonButton1.TabIndex = 5
        Me.XenonButton1.Text = "Open from external app palette (e.g. Photoshop palettes)"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.XenonButton6)
        Me.TabPage2.Controls.Add(Me.XenonSeparator2)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.PictureBox9)
        Me.TabPage2.Controls.Add(Me.XenonNumericUpDown2)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.XenonRadioButton1)
        Me.TabPage2.Controls.Add(Me.XenonCheckBox1)
        Me.TabPage2.Controls.Add(Me.XenonRadioButton2)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.TextBox1)
        Me.TabPage2.Controls.Add(Me.PictureBox7)
        Me.TabPage2.Controls.Add(Me.XenonButton4)
        Me.TabPage2.Controls.Add(Me.PictureBox8)
        Me.TabPage2.Controls.Add(Me.XenonSeparator1)
        Me.TabPage2.Controls.Add(Me.PictureBox6)
        Me.TabPage2.Controls.Add(Me.PictureBox3)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.PictureBox4)
        Me.TabPage2.Controls.Add(Me.ProgressBar1)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.XenonNumericUpDown1)
        Me.TabPage2.Controls.Add(Me.PictureBox5)
        Me.TabPage2.Controls.Add(Me.ImgPaletteContainer)
        Me.TabPage2.Controls.Add(Me.PictureBox2)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.Location = New System.Drawing.Point(4, 34)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(459, 516)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "From Image"
        '
        'XenonSeparator2
        '
        Me.XenonSeparator2.AlternativeLook = False
        Me.XenonSeparator2.Location = New System.Drawing.Point(6, 270)
        Me.XenonSeparator2.Name = "XenonSeparator2"
        Me.XenonSeparator2.Size = New System.Drawing.Size(447, 1)
        Me.XenonSeparator2.TabIndex = 29
        Me.XenonSeparator2.TabStop = False
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.PictureBox10)
        Me.TabPage3.Controls.Add(Me.XenonSeparator3)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.XenonButton9)
        Me.TabPage3.Controls.Add(Me.XenonComboBox1)
        Me.TabPage3.Controls.Add(Me.PictureBox33)
        Me.TabPage3.Controls.Add(Me.Label29)
        Me.TabPage3.Controls.Add(Me.XenonButton5)
        Me.TabPage3.Controls.Add(Me.XenonTextBox1)
        Me.TabPage3.Controls.Add(Me.XenonButton7)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Controls.Add(Me.PictureBox1)
        Me.TabPage3.Controls.Add(Me.ThemePaletteContainer)
        Me.TabPage3.Location = New System.Drawing.Point(4, 34)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(459, 516)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "From Theme"
        '
        'PictureBox10
        '
        Me.PictureBox10.Image = CType(resources.GetObject("PictureBox10.Image"), System.Drawing.Image)
        Me.PictureBox10.Location = New System.Drawing.Point(17, 48)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox10.TabIndex = 82
        Me.PictureBox10.TabStop = False
        '
        'XenonSeparator3
        '
        Me.XenonSeparator3.AlternativeLook = False
        Me.XenonSeparator3.Location = New System.Drawing.Point(17, 78)
        Me.XenonSeparator3.Name = "XenonSeparator3"
        Me.XenonSeparator3.Size = New System.Drawing.Size(434, 1)
        Me.XenonSeparator3.TabIndex = 81
        Me.XenonSeparator3.TabStop = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(47, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 24)
        Me.Label8.TabIndex = 79
        Me.Label8.Text = "Theme file:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton9
        '
        Me.XenonButton9.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton9.ForeColor = System.Drawing.Color.White
        Me.XenonButton9.Image = CType(resources.GetObject("XenonButton9.Image"), System.Drawing.Image)
        Me.XenonButton9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton9.LineColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.XenonButton9.LineSize = 1
        Me.XenonButton9.Location = New System.Drawing.Point(374, 85)
        Me.XenonButton9.Name = "XenonButton9"
        Me.XenonButton9.Size = New System.Drawing.Size(79, 24)
        Me.XenonButton9.TabIndex = 78
        Me.XenonButton9.Text = "Load it"
        Me.XenonButton9.UseVisualStyleBackColor = False
        '
        'XenonComboBox1
        '
        Me.XenonComboBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonComboBox1.CustomFont = False
        Me.XenonComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.XenonComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.XenonComboBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonComboBox1.ForeColor = System.Drawing.Color.White
        Me.XenonComboBox1.FormattingEnabled = True
        Me.XenonComboBox1.ItemHeight = 20
        Me.XenonComboBox1.Location = New System.Drawing.Point(142, 85)
        Me.XenonComboBox1.Name = "XenonComboBox1"
        Me.XenonComboBox1.Size = New System.Drawing.Size(226, 26)
        Me.XenonComboBox1.TabIndex = 77
        '
        'PictureBox33
        '
        Me.PictureBox33.Image = CType(resources.GetObject("PictureBox33.Image"), System.Drawing.Image)
        Me.PictureBox33.Location = New System.Drawing.Point(17, 85)
        Me.PictureBox33.Name = "PictureBox33"
        Me.PictureBox33.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox33.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox33.TabIndex = 76
        Me.PictureBox33.TabStop = False
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(47, 85)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(89, 24)
        Me.Label29.TabIndex = 75
        Me.Label29.Text = "Choose preset:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton5
        '
        Me.XenonButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton5.ForeColor = System.Drawing.Color.White
        Me.XenonButton5.Image = CType(resources.GetObject("XenonButton5.Image"), System.Drawing.Image)
        Me.XenonButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton5.LineColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.XenonButton5.LineSize = 1
        Me.XenonButton5.Location = New System.Drawing.Point(374, 48)
        Me.XenonButton5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton5.Name = "XenonButton5"
        Me.XenonButton5.Size = New System.Drawing.Size(79, 24)
        Me.XenonButton5.TabIndex = 34
        Me.XenonButton5.Text = "Extract"
        Me.XenonButton5.UseVisualStyleBackColor = False
        '
        'XenonTextBox1
        '
        Me.XenonTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox1.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox1.Location = New System.Drawing.Point(142, 48)
        Me.XenonTextBox1.MaxLength = 32767
        Me.XenonTextBox1.Multiline = False
        Me.XenonTextBox1.Name = "XenonTextBox1"
        Me.XenonTextBox1.ReadOnly = False
        Me.XenonTextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox1.SelectedText = ""
        Me.XenonTextBox1.SelectionLength = 0
        Me.XenonTextBox1.SelectionStart = 0
        Me.XenonTextBox1.Size = New System.Drawing.Size(184, 24)
        Me.XenonTextBox1.TabIndex = 29
        Me.XenonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox1.UseSystemPasswordChar = False
        Me.XenonTextBox1.WordWrap = True
        '
        'XenonButton7
        '
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = CType(resources.GetObject("XenonButton7.Image"), System.Drawing.Image)
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(333, 48)
        Me.XenonButton7.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(35, 24)
        Me.XenonButton7.TabIndex = 30
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(47, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(406, 35)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Classic Theme to Palette"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 33
        Me.PictureBox1.TabStop = False
        '
        'ThemePaletteContainer
        '
        Me.ThemePaletteContainer.AutoScroll = True
        Me.ThemePaletteContainer.Location = New System.Drawing.Point(6, 115)
        Me.ThemePaletteContainer.Name = "ThemePaletteContainer"
        Me.ThemePaletteContainer.Size = New System.Drawing.Size(449, 395)
        Me.ThemePaletteContainer.TabIndex = 31
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.XenonComboBox2)
        Me.TabPage4.Controls.Add(Me.PaletteContainer)
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.PictureBox11)
        Me.TabPage4.Location = New System.Drawing.Point(4, 34)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(459, 516)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "All used colors"
        '
        'XenonComboBox2
        '
        Me.XenonComboBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonComboBox2.CustomFont = False
        Me.XenonComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.XenonComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.XenonComboBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonComboBox2.ForeColor = System.Drawing.Color.White
        Me.XenonComboBox2.FormattingEnabled = True
        Me.XenonComboBox2.ItemHeight = 20
        Me.XenonComboBox2.Items.AddRange(New Object() {"Your Current Palette", "Windows 11 Palette", "Windows 10 Palette", "Windows 8.1 Palette", "Windows 7 Palette", "Windows Vista Palette", "Windows XP Palette"})
        Me.XenonComboBox2.Location = New System.Drawing.Point(8, 47)
        Me.XenonComboBox2.Name = "XenonComboBox2"
        Me.XenonComboBox2.Size = New System.Drawing.Size(442, 26)
        Me.XenonComboBox2.TabIndex = 49
        '
        'PaletteContainer
        '
        Me.PaletteContainer.AutoScroll = True
        Me.PaletteContainer.Location = New System.Drawing.Point(8, 77)
        Me.PaletteContainer.Name = "PaletteContainer"
        Me.PaletteContainer.Size = New System.Drawing.Size(442, 433)
        Me.PaletteContainer.TabIndex = 48
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(47, 6)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(406, 35)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "These are all colors used in palette"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox11
        '
        Me.PictureBox11.Image = CType(resources.GetObject("PictureBox11.Image"), System.Drawing.Image)
        Me.PictureBox11.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox11.Name = "PictureBox11"
        Me.PictureBox11.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox11.TabIndex = 35
        Me.PictureBox11.TabStop = False
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.Filter = resources.GetString("OpenFileDialog2.Filter")
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = resources.GetString("SaveFileDialog1.Filter")
        '
        'OpenThemeDialog
        '
        Me.OpenThemeDialog.Filter = "Windows Theme (*.theme)|*.theme|All Files (*.*)|*.*"
        '
        'ColorPickerDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(467, 606)
        Me.Controls.Add(Me.XenonTabControl1)
        Me.Controls.Add(Me.XenonButton3)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.ScreenColorPicker1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MinimumSize = New System.Drawing.Size(16, 645)
        Me.Name = "ColorPickerDlg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Color Picker"
        Me.TopMost = True
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonTabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ColorEditor1 As Cyotek.Windows.Forms.ColorEditor
    Friend WithEvents ColorEditorManager1 As Cyotek.Windows.Forms.ColorEditorManager
    Friend WithEvents ColorGrid1 As Cyotek.Windows.Forms.ColorGrid
    Friend WithEvents ColorWheel1 As Cyotek.Windows.Forms.ColorWheel
    Friend WithEvents ScreenColorPicker1 As Cyotek.Windows.Forms.ScreenColorPicker
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonButton3 As XenonButton
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
    Friend WithEvents Label7 As Label
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents XenonNumericUpDown2 As XenonNumericUpDown
    Friend WithEvents XenonButton6 As XenonButton
    Friend WithEvents XenonTabControl1 As XenonTabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents XenonSeparator2 As XenonSeparator
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents XenonTextBox1 As XenonTextBox
    Friend WithEvents XenonButton7 As XenonButton
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ThemePaletteContainer As FlowLayoutPanel
    Friend WithEvents OpenThemeDialog As OpenFileDialog
    Friend WithEvents XenonButton5 As XenonButton
    Friend WithEvents XenonSeparator3 As XenonSeparator
    Friend WithEvents Label8 As Label
    Friend WithEvents XenonButton9 As XenonButton
    Friend WithEvents XenonComboBox1 As XenonComboBox
    Friend WithEvents PictureBox33 As PictureBox
    Friend WithEvents Label29 As Label
    Friend WithEvents PictureBox10 As PictureBox
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents XenonComboBox2 As XenonComboBox
    Friend WithEvents PaletteContainer As FlowLayoutPanel
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox11 As PictureBox
End Class
