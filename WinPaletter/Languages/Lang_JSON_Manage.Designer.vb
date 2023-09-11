<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Lang_JSON_Manage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lang_JSON_Manage))
        Me.SaveJSONDlg = New System.Windows.Forms.SaveFileDialog()
        Me.OpenJSONDlg = New System.Windows.Forms.OpenFileDialog()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.XenonButton7 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonGroupBox3 = New WinPaletter.XenonGroupBox()
        Me.XenonButton6 = New WinPaletter.XenonButton()
        Me.XenonButton5 = New WinPaletter.XenonButton()
        Me.XenonButton8 = New WinPaletter.XenonButton()
        Me.XenonButton4 = New WinPaletter.XenonButton()
        Me.XenonGroupBox1 = New WinPaletter.XenonGroupBox()
        Me.XenonButton11 = New WinPaletter.XenonButton()
        Me.XenonButton10 = New WinPaletter.XenonButton()
        Me.XenonButton9 = New WinPaletter.XenonButton()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.XenonTextBox3 = New WinPaletter.XenonTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.XenonSeparatorVertical1 = New WinPaletter.XenonSeparatorVertical()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonTextBox2 = New WinPaletter.XenonTextBox()
        Me.XenonGroupBox2 = New WinPaletter.XenonGroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonTextBox1 = New WinPaletter.XenonTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.XenonGroupBox3.SuspendLayout()
        Me.XenonGroupBox1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SaveJSONDlg
        '
        Me.SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*"
        '
        'OpenJSONDlg
        '
        Me.OpenJSONDlg.Filter = "JSON File (*.json)|*.json"
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 579)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(645, 29)
        Me.Label5.TabIndex = 202
        Me.Label5.Text = "Numbers in curly brackets should be left unchanged, for example: {0}"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton7
        '
        Me.XenonButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton7.DrawOnGlass = False
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = Nothing
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(664, 576)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton7.TabIndex = 201
        Me.XenonButton7.Text = "Cancel"
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.DrawOnGlass = False
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = CType(resources.GetObject("XenonButton2.Image"), System.Drawing.Image)
        Me.XenonButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(750, 576)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(95, 34)
        Me.XenonButton2.TabIndex = 200
        Me.XenonButton2.Text = "Save as ..."
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton6)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton5)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton8)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton4)
        Me.XenonGroupBox3.Location = New System.Drawing.Point(12, 13)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(833, 41)
        Me.XenonGroupBox3.TabIndex = 199
        '
        'XenonButton6
        '
        Me.XenonButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton6.DrawOnGlass = False
        Me.XenonButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton6.ForeColor = System.Drawing.Color.White
        Me.XenonButton6.Image = CType(resources.GetObject("XenonButton6.Image"), System.Drawing.Image)
        Me.XenonButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton6.LineColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.XenonButton6.LineSize = 1
        Me.XenonButton6.Location = New System.Drawing.Point(540, 6)
        Me.XenonButton6.Name = "XenonButton6"
        Me.XenonButton6.Size = New System.Drawing.Size(146, 29)
        Me.XenonButton6.TabIndex = 113
        Me.XenonButton6.Text = "Change preview font"
        Me.XenonButton6.UseVisualStyleBackColor = False
        '
        'XenonButton5
        '
        Me.XenonButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton5.DrawOnGlass = False
        Me.XenonButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton5.ForeColor = System.Drawing.Color.White
        Me.XenonButton5.Image = CType(resources.GetObject("XenonButton5.Image"), System.Drawing.Image)
        Me.XenonButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton5.LineColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.XenonButton5.LineSize = 1
        Me.XenonButton5.Location = New System.Drawing.Point(344, 6)
        Me.XenonButton5.Name = "XenonButton5"
        Me.XenonButton5.Size = New System.Drawing.Size(190, 29)
        Me.XenonButton5.TabIndex = 112
        Me.XenonButton5.Text = "Generate new (English) only"
        Me.XenonButton5.UseVisualStyleBackColor = False
        '
        'XenonButton8
        '
        Me.XenonButton8.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton8.DrawOnGlass = False
        Me.XenonButton8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton8.ForeColor = System.Drawing.Color.White
        Me.XenonButton8.Image = CType(resources.GetObject("XenonButton8.Image"), System.Drawing.Image)
        Me.XenonButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton8.LineColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonButton8.LineSize = 1
        Me.XenonButton8.Location = New System.Drawing.Point(7, 6)
        Me.XenonButton8.Name = "XenonButton8"
        Me.XenonButton8.Size = New System.Drawing.Size(102, 29)
        Me.XenonButton8.TabIndex = 110
        Me.XenonButton8.Text = "Open from"
        Me.XenonButton8.UseVisualStyleBackColor = False
        '
        'XenonButton4
        '
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton4.DrawOnGlass = False
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(115, 6)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(223, 29)
        Me.XenonButton4.TabIndex = 111
        Me.XenonButton4.Text = "Generate new (English) and open It"
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'XenonGroupBox1
        '
        Me.XenonGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox1.Controls.Add(Me.XenonButton11)
        Me.XenonGroupBox1.Controls.Add(Me.XenonButton10)
        Me.XenonGroupBox1.Controls.Add(Me.XenonButton9)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox3)
        Me.XenonGroupBox1.Controls.Add(Me.XenonTextBox3)
        Me.XenonGroupBox1.Controls.Add(Me.Label3)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox2)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox1)
        Me.XenonGroupBox1.Controls.Add(Me.XenonSeparatorVertical1)
        Me.XenonGroupBox1.Controls.Add(Me.XenonButton3)
        Me.XenonGroupBox1.Controls.Add(Me.XenonTextBox2)
        Me.XenonGroupBox1.Controls.Add(Me.XenonGroupBox2)
        Me.XenonGroupBox1.Controls.Add(Me.XenonButton1)
        Me.XenonGroupBox1.Controls.Add(Me.XenonTextBox1)
        Me.XenonGroupBox1.Controls.Add(Me.Label4)
        Me.XenonGroupBox1.Controls.Add(Me.Label2)
        Me.XenonGroupBox1.Controls.Add(Me.Label1)
        Me.XenonGroupBox1.Controls.Add(Me.TreeView1)
        Me.XenonGroupBox1.Location = New System.Drawing.Point(12, 60)
        Me.XenonGroupBox1.Name = "XenonGroupBox1"
        Me.XenonGroupBox1.Size = New System.Drawing.Size(833, 504)
        Me.XenonGroupBox1.TabIndex = 7
        '
        'XenonButton11
        '
        Me.XenonButton11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton11.DrawOnGlass = False
        Me.XenonButton11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton11.ForeColor = System.Drawing.Color.White
        Me.XenonButton11.Image = Nothing
        Me.XenonButton11.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton11.LineSize = 1
        Me.XenonButton11.Location = New System.Drawing.Point(319, 40)
        Me.XenonButton11.Name = "XenonButton11"
        Me.XenonButton11.Size = New System.Drawing.Size(73, 23)
        Me.XenonButton11.TabIndex = 21
        Me.XenonButton11.Text = "Collapse all"
        Me.XenonButton11.UseVisualStyleBackColor = False
        '
        'XenonButton10
        '
        Me.XenonButton10.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton10.DrawOnGlass = False
        Me.XenonButton10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton10.ForeColor = System.Drawing.Color.White
        Me.XenonButton10.Image = Nothing
        Me.XenonButton10.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton10.LineSize = 1
        Me.XenonButton10.Location = New System.Drawing.Point(247, 40)
        Me.XenonButton10.Name = "XenonButton10"
        Me.XenonButton10.Size = New System.Drawing.Size(66, 23)
        Me.XenonButton10.TabIndex = 20
        Me.XenonButton10.Text = "Expand all"
        Me.XenonButton10.UseVisualStyleBackColor = False
        '
        'XenonButton9
        '
        Me.XenonButton9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton9.DrawOnGlass = False
        Me.XenonButton9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton9.ForeColor = System.Drawing.Color.White
        Me.XenonButton9.Image = CType(resources.GetObject("XenonButton9.Image"), System.Drawing.Image)
        Me.XenonButton9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton9.LineColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.XenonButton9.LineSize = 1
        Me.XenonButton9.Location = New System.Drawing.Point(567, 466)
        Me.XenonButton9.Name = "XenonButton9"
        Me.XenonButton9.Size = New System.Drawing.Size(134, 28)
        Me.XenonButton9.TabIndex = 19
        Me.XenonButton9.Text = "Language snippets"
        Me.XenonButton9.UseVisualStyleBackColor = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(406, 99)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 15
        Me.PictureBox3.TabStop = False
        '
        'XenonTextBox3
        '
        Me.XenonTextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox3.DrawOnGlass = False
        Me.XenonTextBox3.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox3.Location = New System.Drawing.Point(439, 129)
        Me.XenonTextBox3.MaxLength = 32767
        Me.XenonTextBox3.Multiline = True
        Me.XenonTextBox3.Name = "XenonTextBox3"
        Me.XenonTextBox3.ReadOnly = True
        Me.XenonTextBox3.Scrollbars = System.Windows.Forms.ScrollBars.Vertical
        Me.XenonTextBox3.SelectedText = ""
        Me.XenonTextBox3.SelectionLength = 0
        Me.XenonTextBox3.SelectionStart = 0
        Me.XenonTextBox3.Size = New System.Drawing.Size(387, 119)
        Me.XenonTextBox3.TabIndex = 14
        Me.XenonTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox3.UseSystemPasswordChar = False
        Me.XenonTextBox3.WordWrap = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(436, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(390, 24)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Old value:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(406, 258)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 12
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(406, 40)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'XenonSeparatorVertical1
        '
        Me.XenonSeparatorVertical1.AlternativeLook = False
        Me.XenonSeparatorVertical1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparatorVertical1.Location = New System.Drawing.Point(398, 38)
        Me.XenonSeparatorVertical1.Name = "XenonSeparatorVertical1"
        Me.XenonSeparatorVertical1.Size = New System.Drawing.Size(1, 456)
        Me.XenonSeparatorVertical1.TabIndex = 10
        Me.XenonSeparatorVertical1.TabStop = False
        Me.XenonSeparatorVertical1.Text = "XenonSeparatorVertical1"
        '
        'XenonButton3
        '
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton3.DrawOnGlass = False
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = CType(resources.GetObject("XenonButton3.Image"), System.Drawing.Image)
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(209, 40)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(32, 23)
        Me.XenonButton3.TabIndex = 9
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'XenonTextBox2
        '
        Me.XenonTextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox2.DrawOnGlass = False
        Me.XenonTextBox2.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox2.Location = New System.Drawing.Point(7, 40)
        Me.XenonTextBox2.MaxLength = 32767
        Me.XenonTextBox2.Multiline = True
        Me.XenonTextBox2.Name = "XenonTextBox2"
        Me.XenonTextBox2.ReadOnly = False
        Me.XenonTextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox2.SelectedText = ""
        Me.XenonTextBox2.SelectionLength = 0
        Me.XenonTextBox2.SelectionStart = 0
        Me.XenonTextBox2.Size = New System.Drawing.Size(196, 23)
        Me.XenonTextBox2.TabIndex = 8
        Me.XenonTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox2.UseSystemPasswordChar = False
        Me.XenonTextBox2.WordWrap = True
        '
        'XenonGroupBox2
        '
        Me.XenonGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonGroupBox2.Controls.Add(Me.Label6)
        Me.XenonGroupBox2.Location = New System.Drawing.Point(7, 8)
        Me.XenonGroupBox2.Name = "XenonGroupBox2"
        Me.XenonGroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.XenonGroupBox2.Size = New System.Drawing.Size(819, 24)
        Me.XenonGroupBox2.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(2, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(815, 20)
        Me.Label6.TabIndex = 114
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton1.DrawOnGlass = False
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(84, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(707, 466)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(119, 28)
        Me.XenonButton1.TabIndex = 4
        Me.XenonButton1.Text = "Submit change"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonTextBox1
        '
        Me.XenonTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox1.DrawOnGlass = False
        Me.XenonTextBox1.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox1.Location = New System.Drawing.Point(439, 288)
        Me.XenonTextBox1.MaxLength = 32767
        Me.XenonTextBox1.Multiline = True
        Me.XenonTextBox1.Name = "XenonTextBox1"
        Me.XenonTextBox1.ReadOnly = False
        Me.XenonTextBox1.Scrollbars = System.Windows.Forms.ScrollBars.Vertical
        Me.XenonTextBox1.SelectedText = ""
        Me.XenonTextBox1.SelectionLength = 0
        Me.XenonTextBox1.SelectionStart = 0
        Me.XenonTextBox1.Size = New System.Drawing.Size(387, 172)
        Me.XenonTextBox1.TabIndex = 3
        Me.XenonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox1.UseSystemPasswordChar = False
        Me.XenonTextBox1.WordWrap = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(436, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(390, 24)
        Me.Label4.TabIndex = 2
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(436, 258)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(390, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "New value:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(436, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(390, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Variable:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView1.ForeColor = System.Drawing.Color.White
        Me.TreeView1.FullRowSelect = True
        Me.TreeView1.ItemHeight = 20
        Me.TreeView1.LabelEdit = True
        Me.TreeView1.Location = New System.Drawing.Point(7, 69)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.ShowLines = False
        Me.TreeView1.Size = New System.Drawing.Size(385, 425)
        Me.TreeView1.TabIndex = 6
        '
        'Lang_JSON_Manage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(857, 622)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonGroupBox3)
        Me.Controls.Add(Me.XenonGroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "Lang_JSON_Manage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Language editor"
        Me.XenonGroupBox3.ResumeLayout(False)
        Me.XenonGroupBox1.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents XenonGroupBox1 As XenonGroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonTextBox1 As XenonTextBox
    Friend WithEvents XenonGroupBox3 As XenonGroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents XenonButton8 As XenonButton
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents SaveJSONDlg As SaveFileDialog
    Friend WithEvents OpenJSONDlg As OpenFileDialog
    Friend WithEvents XenonGroupBox2 As XenonGroupBox
    Friend WithEvents XenonTextBox2 As XenonTextBox
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents XenonSeparatorVertical1 As XenonSeparatorVertical
    Friend WithEvents XenonButton4 As XenonButton
    Friend WithEvents XenonButton5 As XenonButton
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents XenonTextBox3 As XenonTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents XenonButton9 As XenonButton
    Friend WithEvents XenonButton6 As XenonButton
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents XenonButton7 As XenonButton
    Friend WithEvents XenonButton11 As XenonButton
    Friend WithEvents XenonButton10 As XenonButton
    Friend WithEvents Label5 As Label
End Class
