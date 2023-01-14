<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Lang_JSON_Update
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lang_JSON_Update))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.XenonTextBox1 = New WinPaletter.XenonTextBox()
        Me.XenonTextBox2 = New WinPaletter.XenonTextBox()
        Me.XenonButton8 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonButton5 = New WinPaletter.XenonButton()
        Me.XenonAlertBox7 = New WinPaletter.XenonAlertBox()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.XenonButton7 = New WinPaletter.XenonButton()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.SaveJSONDlg = New System.Windows.Forms.SaveFileDialog()
        Me.OpenJSONDlg = New System.Windows.Forms.OpenFileDialog()
        Me.XenonAlertBox1 = New WinPaletter.XenonAlertBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(42, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 24)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Old JSON File:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 42)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 15
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(42, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 24)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "New JSON File:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonTextBox1
        '
        Me.XenonTextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox1.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox1.Hint = Nothing
        Me.XenonTextBox1.LineColor = System.Drawing.Color.DodgerBlue
        Me.XenonTextBox1.Location = New System.Drawing.Point(154, 12)
        Me.XenonTextBox1.MaxLength = 32767
        Me.XenonTextBox1.Multiline = False
        Me.XenonTextBox1.Name = "XenonTextBox1"
        Me.XenonTextBox1.ReadOnly = False
        Me.XenonTextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox1.SelectedText = ""
        Me.XenonTextBox1.SelectionLength = 0
        Me.XenonTextBox1.SelectionStart = 0
        Me.XenonTextBox1.Size = New System.Drawing.Size(515, 24)
        Me.XenonTextBox1.TabIndex = 16
        Me.XenonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox1.UseSystemPasswordChar = False
        Me.XenonTextBox1.WordWrap = True
        '
        'XenonTextBox2
        '
        Me.XenonTextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox2.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox2.Hint = Nothing
        Me.XenonTextBox2.LineColor = System.Drawing.Color.DodgerBlue
        Me.XenonTextBox2.Location = New System.Drawing.Point(154, 42)
        Me.XenonTextBox2.MaxLength = 32767
        Me.XenonTextBox2.Multiline = False
        Me.XenonTextBox2.Name = "XenonTextBox2"
        Me.XenonTextBox2.ReadOnly = False
        Me.XenonTextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox2.SelectedText = ""
        Me.XenonTextBox2.SelectionLength = 0
        Me.XenonTextBox2.SelectionStart = 0
        Me.XenonTextBox2.Size = New System.Drawing.Size(352, 24)
        Me.XenonTextBox2.TabIndex = 17
        Me.XenonTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox2.UseSystemPasswordChar = False
        Me.XenonTextBox2.WordWrap = True
        '
        'XenonButton8
        '
        Me.XenonButton8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton8.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton8.ForeColor = System.Drawing.Color.White
        Me.XenonButton8.Image = CType(resources.GetObject("XenonButton8.Image"), System.Drawing.Image)
        Me.XenonButton8.LineColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonButton8.LineSize = 1
        Me.XenonButton8.Location = New System.Drawing.Point(675, 12)
        Me.XenonButton8.Name = "XenonButton8"
        Me.XenonButton8.Size = New System.Drawing.Size(40, 24)
        Me.XenonButton8.TabIndex = 111
        Me.XenonButton8.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(675, 42)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(40, 24)
        Me.XenonButton1.TabIndex = 112
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonButton5
        '
        Me.XenonButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton5.ForeColor = System.Drawing.Color.White
        Me.XenonButton5.Image = CType(resources.GetObject("XenonButton5.Image"), System.Drawing.Image)
        Me.XenonButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton5.LineColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.XenonButton5.LineSize = 1
        Me.XenonButton5.Location = New System.Drawing.Point(512, 42)
        Me.XenonButton5.Name = "XenonButton5"
        Me.XenonButton5.Size = New System.Drawing.Size(157, 24)
        Me.XenonButton5.TabIndex = 113
        Me.XenonButton5.Text = "Generate New (English)"
        Me.XenonButton5.UseVisualStyleBackColor = False
        '
        'XenonAlertBox7
        '
        Me.XenonAlertBox7.AlertStyle = WinPaletter.XenonAlertBox.Style.Notice
        Me.XenonAlertBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(91, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.XenonAlertBox7.CanClose = WinPaletter.XenonAlertBox.Close.No
        Me.XenonAlertBox7.CenterText = True
        Me.XenonAlertBox7.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox7.Image = Nothing
        Me.XenonAlertBox7.Location = New System.Drawing.Point(12, 126)
        Me.XenonAlertBox7.Name = "XenonAlertBox7"
        Me.XenonAlertBox7.Size = New System.Drawing.Size(703, 52)
        Me.XenonAlertBox7.TabIndex = 114
        Me.XenonAlertBox7.TabStop = False
        Me.XenonAlertBox7.Text = resources.GetString("XenonAlertBox7.Text")
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 72)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(703, 1)
        Me.XenonSeparator1.TabIndex = 119
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonButton7
        '
        Me.XenonButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = Nothing
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(505, 191)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(96, 29)
        Me.XenonButton7.TabIndex = 203
        Me.XenonButton7.Text = "Cancel"
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = CType(resources.GetObject("XenonButton3.Image"), System.Drawing.Image)
        Me.XenonButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(607, 191)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(108, 29)
        Me.XenonButton3.TabIndex = 202
        Me.XenonButton3.Text = "Save As ..."
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'SaveJSONDlg
        '
        Me.SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*"
        '
        'OpenJSONDlg
        '
        Me.OpenJSONDlg.Filter = "JSON File (*.json)|*.json"
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.XenonAlertBox.Style.Warning
        Me.XenonAlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.XenonAlertBox1.CanClose = WinPaletter.XenonAlertBox.Close.No
        Me.XenonAlertBox1.CenterText = True
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Nothing
        Me.XenonAlertBox1.Location = New System.Drawing.Point(12, 91)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(703, 29)
        Me.XenonAlertBox1.TabIndex = 204
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = "This feature is experimental, always create backups before starting. If any abnor" &
    "mal results happened, post an issue in GitHub"
        '
        'Lang_JSON_Update
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(727, 232)
        Me.Controls.Add(Me.XenonAlertBox1)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonButton3)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.XenonAlertBox7)
        Me.Controls.Add(Me.XenonButton5)
        Me.Controls.Add(Me.XenonButton1)
        Me.Controls.Add(Me.XenonButton8)
        Me.Controls.Add(Me.XenonTextBox2)
        Me.Controls.Add(Me.XenonTextBox1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI Historic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Lang_JSON_Update"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Language JSON File"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents XenonTextBox1 As XenonTextBox
    Friend WithEvents XenonTextBox2 As XenonTextBox
    Friend WithEvents XenonButton8 As XenonButton
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonButton5 As XenonButton
    Friend WithEvents XenonAlertBox7 As XenonAlertBox
    Friend WithEvents XenonSeparator1 As XenonSeparator
    Friend WithEvents XenonButton7 As XenonButton
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents SaveJSONDlg As SaveFileDialog
    Friend WithEvents OpenJSONDlg As OpenFileDialog
    Friend WithEvents XenonAlertBox1 As XenonAlertBox
End Class
