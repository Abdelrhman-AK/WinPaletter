<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Updates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Updates))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.LinkLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.XenonRadioButton2 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton3 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton1 = New WinPaletter.XenonRadioButton()
        Me.XenonAlertBox2 = New WinPaletter.XenonAlertBox()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(195, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "1.0.0.0"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(82, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(117, 25)
        Me.Label17.TabIndex = 14
        Me.Label17.Text = "WinPaletter"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DodgerBlue
        Me.Label3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Label3.LinkColor = System.Drawing.Color.DodgerBlue
        Me.Label3.Location = New System.Drawing.Point(87, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(148, 24)
        Me.Label3.TabIndex = 17
        Me.Label3.TabStop = True
        Me.Label3.Text = "Stable Channel"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.XenonRadioButton2)
        Me.Panel1.Controls.Add(Me.XenonRadioButton3)
        Me.Panel1.Controls.Add(Me.XenonRadioButton1)
        Me.Panel1.Location = New System.Drawing.Point(12, 134)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(555, 89)
        Me.Panel1.TabIndex = 18
        Me.Panel1.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 229)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(555, 18)
        Me.ProgressBar1.TabIndex = 3
        Me.ProgressBar1.Visible = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.FileName = "WinPaletter"
        Me.SaveFileDialog1.Filter = "Executable File|*.exe"
        '
        'XenonRadioButton2
        '
        Me.XenonRadioButton2.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonRadioButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonRadioButton2.Checked = False
        Me.XenonRadioButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton2.Location = New System.Drawing.Point(3, 32)
        Me.XenonRadioButton2.Name = "XenonRadioButton2"
        Me.XenonRadioButton2.Size = New System.Drawing.Size(134, 23)
        Me.XenonRadioButton2.TabIndex = 2
        Me.XenonRadioButton2.Text = "Save download as..."
        '
        'XenonRadioButton3
        '
        Me.XenonRadioButton3.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonRadioButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonRadioButton3.Checked = False
        Me.XenonRadioButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton3.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton3.Location = New System.Drawing.Point(3, 61)
        Me.XenonRadioButton3.Name = "XenonRadioButton3"
        Me.XenonRadioButton3.Size = New System.Drawing.Size(202, 23)
        Me.XenonRadioButton3.TabIndex = 1
        Me.XenonRadioButton3.Text = "Just Download from the browser"
        '
        'XenonRadioButton1
        '
        Me.XenonRadioButton1.AccentColor = System.Drawing.Color.DodgerBlue
        Me.XenonRadioButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonRadioButton1.Checked = True
        Me.XenonRadioButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton1.Location = New System.Drawing.Point(3, 3)
        Me.XenonRadioButton1.Name = "XenonRadioButton1"
        Me.XenonRadioButton1.Size = New System.Drawing.Size(451, 23)
        Me.XenonRadioButton1.TabIndex = 0
        Me.XenonRadioButton1.Text = "Download then Close the current executable and replace it by the new update"
        '
        'XenonAlertBox2
        '
        Me.XenonAlertBox2.AlertStyle = WinPaletter.XenonAlertBox.Style.Warning
        Me.XenonAlertBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.XenonAlertBox2.Border = True
        Me.XenonAlertBox2.CanClose = WinPaletter.XenonAlertBox.Close.No
        Me.XenonAlertBox2.CenterText = True
        Me.XenonAlertBox2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XenonAlertBox2.Image = Nothing
        Me.XenonAlertBox2.Location = New System.Drawing.Point(12, 96)
        Me.XenonAlertBox2.Name = "XenonAlertBox2"
        Me.XenonAlertBox2.Size = New System.Drawing.Size(555, 32)
        Me.XenonAlertBox2.TabIndex = 12
        Me.XenonAlertBox2.TabStop = False
        Me.XenonAlertBox2.Text = "Update Avaliable"
        Me.XenonAlertBox2.Visible = False
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 86)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(555, 1)
        Me.XenonSeparator1.TabIndex = 8
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = Nothing
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(275, 287)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(104, 36)
        Me.XenonButton3.TabIndex = 2
        Me.XenonButton3.Text = "Cancel"
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(13, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(385, 287)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(182, 36)
        Me.XenonButton1.TabIndex = 0
        Me.XenonButton1.Text = "Check for updates"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'Updates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(579, 334)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.XenonAlertBox2)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.XenonButton3)
        Me.Controls.Add(Me.XenonButton1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Updates"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Updates"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents XenonSeparator1 As XenonSeparator
    Friend WithEvents XenonAlertBox2 As XenonAlertBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label3 As LinkLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents XenonRadioButton2 As XenonRadioButton
    Friend WithEvents XenonRadioButton3 As XenonRadioButton
    Friend WithEvents XenonRadioButton1 As XenonRadioButton
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
