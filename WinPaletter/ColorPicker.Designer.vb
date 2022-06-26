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
        Me.ScreenColorPicker1 = New Cyotek.Windows.Forms.ScreenColorPicker()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.XenonTabControl1 = New WinPaletter.XenonTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ColorGrid1 = New Cyotek.Windows.Forms.ColorGrid()
        Me.ColorEditor1 = New Cyotek.Windows.Forms.ColorEditor()
        Me.ColorWheel1 = New Cyotek.Windows.Forms.ColorWheel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.XenonButton6 = New WinPaletter.XenonButton()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.XenonNumericUpDown2 = New WinPaletter.XenonNumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.XenonRadioButton1 = New WinPaletter.XenonRadioButton()
        Me.XenonCheckBox1 = New WinPaletter.XenonCheckBox()
        Me.XenonRadioButton2 = New WinPaletter.XenonRadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.XenonButton4 = New WinPaletter.XenonButton()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.XenonNumericUpDown1 = New WinPaletter.XenonNumericUpDown()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New WinPaletter.XenonTextBox()
        Me.ImgPaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton14 = New WinPaletter.XenonButton()
        Me.XenonTabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'ScreenColorPicker1
        '
        Me.ScreenColorPicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScreenColorPicker1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ScreenColorPicker1.Location = New System.Drawing.Point(43, 551)
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
        'XenonTabControl1
        '
        Me.XenonTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTabControl1.Controls.Add(Me.TabPage1)
        Me.XenonTabControl1.Controls.Add(Me.TabPage2)
        Me.XenonTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.XenonTabControl1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonTabControl1.ItemSize = New System.Drawing.Size(120, 30)
        Me.XenonTabControl1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonTabControl1.Location = New System.Drawing.Point(12, 12)
        Me.XenonTabControl1.Multiline = True
        Me.XenonTabControl1.Name = "XenonTabControl1"
        Me.XenonTabControl1.SelectedIndex = 0
        Me.XenonTabControl1.Size = New System.Drawing.Size(446, 533)
        Me.XenonTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.XenonTabControl1.TabIndex = 31
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.ColorEditor1)
        Me.TabPage1.Controls.Add(Me.ColorWheel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 34)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(438, 495)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Color Controls"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.ColorGrid1)
        Me.Panel1.Location = New System.Drawing.Point(6, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(185, 482)
        Me.Panel1.TabIndex = 31
        '
        'ColorGrid1
        '
        Me.ColorGrid1.AllowDrop = True
        Me.ColorGrid1.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None
        Me.ColorGrid1.CellSize = New System.Drawing.Size(13, 15)
        Me.ColorGrid1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorGrid1.Columns = 10
        Me.ColorGrid1.Location = New System.Drawing.Point(0, 0)
        Me.ColorGrid1.Name = "ColorGrid1"
        Me.ColorGrid1.Palette = Cyotek.Windows.Forms.ColorPalette.WebSafe
        Me.ColorGrid1.Size = New System.Drawing.Size(158, 424)
        Me.ColorGrid1.Spacing = New System.Drawing.Size(2, 2)
        Me.ColorGrid1.TabIndex = 1
        '
        'ColorEditor1
        '
        Me.ColorEditor1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ColorEditor1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorEditor1.Location = New System.Drawing.Point(198, 6)
        Me.ColorEditor1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ColorEditor1.Name = "ColorEditor1"
        Me.ColorEditor1.NubColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ColorEditor1.NubOutlineColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.ColorEditor1.ShowAlphaChannel = False
        Me.ColorEditor1.Size = New System.Drawing.Size(233, 232)
        Me.ColorEditor1.TabIndex = 0
        '
        'ColorWheel1
        '
        Me.ColorWheel1.Alpha = 1.0R
        Me.ColorWheel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ColorWheel1.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ColorWheel1.ColorStep = 1
        Me.ColorWheel1.Lightness = 0.37647059559822083R
        Me.ColorWheel1.LineColor = System.Drawing.Color.White
        Me.ColorWheel1.Location = New System.Drawing.Point(198, 256)
        Me.ColorWheel1.Name = "ColorWheel1"
        Me.ColorWheel1.SelectionSize = 13
        Me.ColorWheel1.Size = New System.Drawing.Size(234, 232)
        Me.ColorWheel1.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.XenonButton6)
        Me.TabPage2.Controls.Add(Me.PictureBox2)
        Me.TabPage2.Controls.Add(Me.PictureBox9)
        Me.TabPage2.Controls.Add(Me.XenonNumericUpDown2)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.XenonRadioButton1)
        Me.TabPage2.Controls.Add(Me.XenonCheckBox1)
        Me.TabPage2.Controls.Add(Me.XenonRadioButton2)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.PictureBox7)
        Me.TabPage2.Controls.Add(Me.XenonButton4)
        Me.TabPage2.Controls.Add(Me.PictureBox8)
        Me.TabPage2.Controls.Add(Me.PictureBox6)
        Me.TabPage2.Controls.Add(Me.PictureBox3)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.PictureBox4)
        Me.TabPage2.Controls.Add(Me.ProgressBar1)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.XenonNumericUpDown1)
        Me.TabPage2.Controls.Add(Me.PictureBox5)
        Me.TabPage2.Controls.Add(Me.TextBox1)
        Me.TabPage2.Controls.Add(Me.ImgPaletteContainer)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 34)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(438, 495)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Palette from image"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(69, 183)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 24)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Quality"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton6
        '
        Me.XenonButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton6.ForeColor = System.Drawing.Color.White
        Me.XenonButton6.Image = CType(resources.GetObject("XenonButton6.Image"), System.Drawing.Image)
        Me.XenonButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton6.LineColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.XenonButton6.LineSize = 1
        Me.XenonButton6.Location = New System.Drawing.Point(-5852, 252)
        Me.XenonButton6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton6.Name = "XenonButton6"
        Me.XenonButton6.Size = New System.Drawing.Size(133, 24)
        Me.XenonButton6.TabIndex = 28
        Me.XenonButton6.Text = "Extract"
        Me.XenonButton6.UseVisualStyleBackColor = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(39, 183)
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
        Me.XenonNumericUpDown2.Location = New System.Drawing.Point(-6070, 183)
        Me.XenonNumericUpDown2.Max = 100
        Me.XenonNumericUpDown2.Min = 5
        Me.XenonNumericUpDown2.Name = "XenonNumericUpDown2"
        Me.XenonNumericUpDown2.Size = New System.Drawing.Size(70, 24)
        Me.XenonNumericUpDown2.TabIndex = 25
        Me.XenonNumericUpDown2.Text = "XenonNumericUpDown2"
        Me.XenonNumericUpDown2.UpDownStep = 1
        Me.XenonNumericUpDown2.Value = 10
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(36, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 24)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Select a mode"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonRadioButton1
        '
        Me.XenonRadioButton1.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonRadioButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.XenonRadioButton1.Checked = True
        Me.XenonRadioButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton1.Location = New System.Drawing.Point(69, 33)
        Me.XenonRadioButton1.Name = "XenonRadioButton1"
        Me.XenonRadioButton1.Size = New System.Drawing.Size(142, 24)
        Me.XenonRadioButton1.TabIndex = 7
        Me.XenonRadioButton1.Text = "Current Wallpaper"
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.XenonCheckBox1.Checked = True
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(69, 211)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(174, 24)
        Me.XenonCheckBox1.TabIndex = 9
        Me.XenonCheckBox1.Text = "Ignore White Colors"
        '
        'XenonRadioButton2
        '
        Me.XenonRadioButton2.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonRadioButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.XenonRadioButton2.Checked = False
        Me.XenonRadioButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton2.Location = New System.Drawing.Point(69, 61)
        Me.XenonRadioButton2.Name = "XenonRadioButton2"
        Me.XenonRadioButton2.Size = New System.Drawing.Size(142, 24)
        Me.XenonRadioButton2.TabIndex = 8
        Me.XenonRadioButton2.Text = "Browse for an image"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(69, 155)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 24)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Maximum Colors"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(39, 211)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 22
        Me.PictureBox7.TabStop = False
        '
        'XenonButton4
        '
        Me.XenonButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(-5755, 91)
        Me.XenonButton4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(36, 24)
        Me.XenonButton4.TabIndex = 10
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(39, 155)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox8.TabIndex = 21
        Me.PictureBox8.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(6, 128)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 20
        Me.PictureBox6.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(39, 33)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 13
        Me.PictureBox3.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(36, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 24)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Options"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(39, 61)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 14
        Me.PictureBox4.TabStop = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(95, 260)
        Me.ProgressBar1.MarqueeAnimationSpeed = 20
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(291, 8)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 9
        Me.ProgressBar1.Visible = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(36, 252)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 24)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Palette"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonNumericUpDown1
        '
        Me.XenonNumericUpDown1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonNumericUpDown1.EnabledCalc = True
        Me.XenonNumericUpDown1.LineColor = System.Drawing.Color.DodgerBlue
        Me.XenonNumericUpDown1.Location = New System.Drawing.Point(-6070, 155)
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
        Me.PictureBox5.Location = New System.Drawing.Point(6, 252)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 16
        Me.PictureBox5.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Hint = Nothing
        Me.TextBox1.LineColor = System.Drawing.Color.DodgerBlue
        Me.TextBox1.Location = New System.Drawing.Point(69, 91)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Multiline = False
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = False
        Me.TextBox1.Size = New System.Drawing.Size(0, 24)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox1.UseSystemPasswordChar = False
        '
        'ImgPaletteContainer
        '
        Me.ImgPaletteContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ImgPaletteContainer.AutoScroll = True
        Me.ImgPaletteContainer.Location = New System.Drawing.Point(39, 279)
        Me.ImgPaletteContainer.Name = "ImgPaletteContainer"
        Me.ImgPaletteContainer.Size = New System.Drawing.Size(0, 0)
        Me.ImgPaletteContainer.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(39, 279)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 0)
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
        Me.XenonButton3.Location = New System.Drawing.Point(256, 551)
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
        Me.XenonButton2.Location = New System.Drawing.Point(358, 551)
        Me.XenonButton2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(99, 34)
        Me.XenonButton2.TabIndex = 4
        Me.XenonButton2.Text = "Select"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton14
        '
        Me.XenonButton14.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.XenonButton14.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton14.ForeColor = System.Drawing.Color.White
        Me.XenonButton14.Image = Nothing
        Me.XenonButton14.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton14.LineSize = 1
        Me.XenonButton14.Location = New System.Drawing.Point(12, 551)
        Me.XenonButton14.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton14.Name = "XenonButton14"
        Me.XenonButton14.Size = New System.Drawing.Size(115, 34)
        Me.XenonButton14.TabIndex = 30
        Me.XenonButton14.Text = "Load PAL"
        Me.XenonButton14.UseVisualStyleBackColor = False
        '
        'ColorPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(470, 598)
        Me.Controls.Add(Me.XenonTabControl1)
        Me.Controls.Add(Me.XenonButton3)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton14)
        Me.Controls.Add(Me.ScreenColorPicker1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "ColorPicker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Color Picker"
        Me.XenonTabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents XenonButton14 As XenonButton
    Friend WithEvents XenonTabControl1 As XenonTabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel1 As Panel
End Class
