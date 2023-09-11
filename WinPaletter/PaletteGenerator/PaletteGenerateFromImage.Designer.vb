<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PaletteGenerateFromImage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PaletteGenerateFromImage))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.XenonGroupBox4 = New WinPaletter.XenonGroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.XenonRadioButton6 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton3 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton4 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton7 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton5 = New WinPaletter.XenonRadioButton()
        Me.XenonGroupBox3 = New WinPaletter.XenonGroupBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.val1 = New WinPaletter.XenonButton()
        Me.XenonTrackbar1 = New WinPaletter.XenonTrackbar()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.XenonTrackbar2 = New WinPaletter.XenonTrackbar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.val2 = New WinPaletter.XenonButton()
        Me.XenonCheckBox1 = New WinPaletter.XenonCheckBox()
        Me.XenonGroupBox2 = New WinPaletter.XenonGroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New WinPaletter.XenonTextBox()
        Me.XenonButton4 = New WinPaletter.XenonButton()
        Me.XenonRadioButton1 = New WinPaletter.XenonRadioImage()
        Me.XenonRadioButton2 = New WinPaletter.XenonRadioImage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.XenonGroupBox1 = New WinPaletter.XenonGroupBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImgPaletteContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.XenonAlertBox1 = New WinPaletter.XenonAlertBox()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonGroupBox4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox3.SuspendLayout()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox2.SuspendLayout()
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
        Me.XenonGroupBox4.Location = New System.Drawing.Point(12, 145)
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
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox8)
        Me.XenonGroupBox3.Controls.Add(Me.val1)
        Me.XenonGroupBox3.Controls.Add(Me.XenonTrackbar1)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox9)
        Me.XenonGroupBox3.Controls.Add(Me.Label7)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox7)
        Me.XenonGroupBox3.Controls.Add(Me.XenonTrackbar2)
        Me.XenonGroupBox3.Controls.Add(Me.Label6)
        Me.XenonGroupBox3.Controls.Add(Me.val2)
        Me.XenonGroupBox3.Controls.Add(Me.XenonCheckBox1)
        Me.XenonGroupBox3.Location = New System.Drawing.Point(12, 48)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(610, 91)
        Me.XenonGroupBox3.TabIndex = 166
        Me.XenonGroupBox3.Text = "XenonGroupBox3"
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(3, 3)
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
        Me.val1.Location = New System.Drawing.Point(572, 3)
        Me.val1.Name = "val1"
        Me.val1.Size = New System.Drawing.Size(34, 24)
        Me.val1.TabIndex = 153
        Me.val1.UseVisualStyleBackColor = False
        '
        'XenonTrackbar1
        '
        Me.XenonTrackbar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTrackbar1.LargeChange = 10
        Me.XenonTrackbar1.Location = New System.Drawing.Point(149, 6)
        Me.XenonTrackbar1.Maximum = 100
        Me.XenonTrackbar1.Minimum = 13
        Me.XenonTrackbar1.Name = "XenonTrackbar1"
        Me.XenonTrackbar1.Size = New System.Drawing.Size(417, 19)
        Me.XenonTrackbar1.SmallChange = 1
        Me.XenonTrackbar1.TabIndex = 152
        Me.XenonTrackbar1.Value = 13
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(3, 33)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox9.TabIndex = 154
        Me.PictureBox9.TabStop = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(33, 33)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 24)
        Me.Label7.TabIndex = 155
        Me.Label7.Text = "Quality"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(3, 63)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 150
        Me.PictureBox7.TabStop = False
        '
        'XenonTrackbar2
        '
        Me.XenonTrackbar2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTrackbar2.LargeChange = 10
        Me.XenonTrackbar2.Location = New System.Drawing.Point(149, 36)
        Me.XenonTrackbar2.Maximum = 100
        Me.XenonTrackbar2.Minimum = 0
        Me.XenonTrackbar2.Name = "XenonTrackbar2"
        Me.XenonTrackbar2.Size = New System.Drawing.Size(417, 19)
        Me.XenonTrackbar2.SmallChange = 1
        Me.XenonTrackbar2.TabIndex = 156
        Me.XenonTrackbar2.Value = 10
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(33, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 24)
        Me.Label6.TabIndex = 151
        Me.Label6.Text = "Maximum colors"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'val2
        '
        Me.val2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.val2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.val2.DrawOnGlass = False
        Me.val2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.val2.ForeColor = System.Drawing.Color.White
        Me.val2.Image = Nothing
        Me.val2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.val2.LineSize = 1
        Me.val2.Location = New System.Drawing.Point(572, 33)
        Me.val2.Name = "val2"
        Me.val2.Size = New System.Drawing.Size(34, 24)
        Me.val2.TabIndex = 157
        Me.val2.UseVisualStyleBackColor = False
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonCheckBox1.Checked = True
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(33, 63)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(180, 24)
        Me.XenonCheckBox1.TabIndex = 148
        Me.XenonCheckBox1.Text = "Ignore white colors"
        '
        'XenonGroupBox2
        '
        Me.XenonGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox2)
        Me.XenonGroupBox2.Controls.Add(Me.TextBox1)
        Me.XenonGroupBox2.Controls.Add(Me.XenonButton4)
        Me.XenonGroupBox2.Controls.Add(Me.XenonRadioButton1)
        Me.XenonGroupBox2.Controls.Add(Me.XenonRadioButton2)
        Me.XenonGroupBox2.Controls.Add(Me.Label2)
        Me.XenonGroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.XenonGroupBox2.Name = "XenonGroupBox2"
        Me.XenonGroupBox2.Size = New System.Drawing.Size(610, 30)
        Me.XenonGroupBox2.TabIndex = 165
        Me.XenonGroupBox2.Text = "XenonGroupBox2"
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
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox1.DrawOnGlass = False
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(283, 3)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Multiline = False
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = False
        Me.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.SelectionLength = 0
        Me.TextBox1.SelectionStart = 0
        Me.TextBox1.Size = New System.Drawing.Size(282, 24)
        Me.TextBox1.TabIndex = 137
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox1.UseSystemPasswordChar = False
        Me.TextBox1.WordWrap = True
        '
        'XenonButton4
        '
        Me.XenonButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton4.DrawOnGlass = False
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(572, 3)
        Me.XenonButton4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(34, 24)
        Me.XenonButton4.TabIndex = 138
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'XenonRadioButton1
        '
        Me.XenonRadioButton1.Checked = True
        Me.XenonRadioButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton1.Image = Nothing
        Me.XenonRadioButton1.Location = New System.Drawing.Point(86, 3)
        Me.XenonRadioButton1.Name = "XenonRadioButton1"
        Me.XenonRadioButton1.ShowText = True
        Me.XenonRadioButton1.Size = New System.Drawing.Size(119, 24)
        Me.XenonRadioButton1.TabIndex = 139
        Me.XenonRadioButton1.Text = "Current wallpaper"
        '
        'XenonRadioButton2
        '
        Me.XenonRadioButton2.Checked = False
        Me.XenonRadioButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton2.Image = Nothing
        Me.XenonRadioButton2.Location = New System.Drawing.Point(211, 3)
        Me.XenonRadioButton2.Name = "XenonRadioButton2"
        Me.XenonRadioButton2.ShowText = True
        Me.XenonRadioButton2.Size = New System.Drawing.Size(66, 24)
        Me.XenonRadioButton2.TabIndex = 140
        Me.XenonRadioButton2.Text = "Image"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 24)
        Me.Label2.TabIndex = 141
        Me.Label2.Text = "Source"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonGroupBox1
        '
        Me.XenonGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox5)
        Me.XenonGroupBox1.Controls.Add(Me.Label1)
        Me.XenonGroupBox1.Controls.Add(Me.ImgPaletteContainer)
        Me.XenonGroupBox1.Location = New System.Drawing.Point(12, 276)
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
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 269)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(610, 1)
        Me.XenonSeparator1.TabIndex = 161
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.XenonAlertBox.Style.Simple
        Me.XenonAlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonAlertBox1.CenterText = False
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Nothing
        Me.XenonAlertBox1.Location = New System.Drawing.Point(12, 474)
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
        Me.XenonButton3.Location = New System.Drawing.Point(245, 506)
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
        Me.XenonButton2.Location = New System.Drawing.Point(351, 506)
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
        Me.XenonButton1.Location = New System.Drawing.Point(457, 506)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(165, 32)
        Me.XenonButton1.TabIndex = 146
        Me.XenonButton1.Text = "Distribute randomly"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'PaletteGenerateFromImage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(634, 550)
        Me.Controls.Add(Me.XenonGroupBox4)
        Me.Controls.Add(Me.XenonGroupBox3)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PaletteGenerateFromImage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate a palette from image"
        Me.XenonGroupBox4.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox3.ResumeLayout(False)
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox2.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox1.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents XenonRadioButton2 As XenonRadioImage
    Friend WithEvents XenonRadioButton1 As XenonRadioImage
    Friend WithEvents XenonButton4 As XenonButton
    Friend WithEvents TextBox1 As XenonTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ImgPaletteContainer As FlowLayoutPanel
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents val1 As XenonButton
    Friend WithEvents XenonTrackbar1 As XenonTrackbar
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents XenonCheckBox1 As XenonCheckBox
    Friend WithEvents val2 As XenonButton
    Friend WithEvents XenonTrackbar2 As XenonTrackbar
    Friend WithEvents Label7 As Label
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents XenonAlertBox1 As XenonAlertBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents XenonSeparator1 As XenonSeparator
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents XenonGroupBox1 As XenonGroupBox
    Friend WithEvents XenonGroupBox2 As XenonGroupBox
    Friend WithEvents XenonRadioButton6 As XenonRadioButton
    Friend WithEvents XenonRadioButton7 As XenonRadioButton
    Friend WithEvents XenonRadioButton5 As XenonRadioButton
    Friend WithEvents XenonRadioButton4 As XenonRadioButton
    Friend WithEvents XenonRadioButton3 As XenonRadioButton
    Friend WithEvents XenonGroupBox3 As XenonGroupBox
    Friend WithEvents XenonGroupBox4 As XenonGroupBox
    Friend WithEvents Label3 As Label
End Class
