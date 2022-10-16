<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewExtTerminal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewExtTerminal))
        Me.PictureBox17 = New System.Windows.Forms.PictureBox()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonAlertBox1 = New WinPaletter.XenonAlertBox()
        Me.XenonButton16 = New WinPaletter.XenonButton()
        Me.XenonTextBox1 = New WinPaletter.XenonTextBox()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox17
        '
        Me.PictureBox17.Image = CType(resources.GetObject("PictureBox17.Image"), System.Drawing.Image)
        Me.PictureBox17.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox17.Name = "PictureBox17"
        Me.PictureBox17.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox17.TabIndex = 101
        Me.PictureBox17.TabStop = False
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.Transparent
        Me.Label102.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.ForeColor = System.Drawing.Color.White
        Me.Label102.Location = New System.Drawing.Point(42, 12)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(39, 24)
        Me.Label102.TabIndex = 100
        Me.Label102.Text = "Path:"
        Me.Label102.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Executable File (*.exe)|*.exe"
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(229, 105)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(80, 30)
        Me.XenonButton2.TabIndex = 201
        Me.XenonButton2.Text = "Cancel"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(315, 105)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(165, 30)
        Me.XenonButton1.TabIndex = 200
        Me.XenonButton1.Text = "Add to registry entry"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.XenonAlertBox.Style.Adaptive
        Me.XenonAlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.XenonAlertBox1.CanClose = WinPaletter.XenonAlertBox.Close.No
        Me.XenonAlertBox1.CenterText = False
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Global.WinPaletter.My.Resources.Resources.notify_warning
        Me.XenonAlertBox1.Location = New System.Drawing.Point(12, 44)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(468, 49)
        Me.XenonAlertBox1.TabIndex = 199
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = "This feature is experimental, you should be sure that you are selecting a console" &
    " application not WinForm\Desktop application, and in system drive (C:\)"
        '
        'XenonButton16
        '
        Me.XenonButton16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton16.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton16.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton16.ForeColor = System.Drawing.Color.White
        Me.XenonButton16.Image = CType(resources.GetObject("XenonButton16.Image"), System.Drawing.Image)
        Me.XenonButton16.LineColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonButton16.LineSize = 1
        Me.XenonButton16.Location = New System.Drawing.Point(450, 12)
        Me.XenonButton16.Name = "XenonButton16"
        Me.XenonButton16.Size = New System.Drawing.Size(30, 24)
        Me.XenonButton16.TabIndex = 193
        Me.XenonButton16.UseVisualStyleBackColor = False
        '
        'XenonTextBox1
        '
        Me.XenonTextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox1.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox1.Hint = Nothing
        Me.XenonTextBox1.LineColor = System.Drawing.Color.DodgerBlue
        Me.XenonTextBox1.Location = New System.Drawing.Point(87, 12)
        Me.XenonTextBox1.MaxLength = 32767
        Me.XenonTextBox1.Multiline = False
        Me.XenonTextBox1.Name = "XenonTextBox1"
        Me.XenonTextBox1.ReadOnly = False
        Me.XenonTextBox1.Size = New System.Drawing.Size(357, 24)
        Me.XenonTextBox1.TabIndex = 102
        Me.XenonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox1.UseSystemPasswordChar = False
        '
        'NewExtTerminal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(492, 147)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton1)
        Me.Controls.Add(Me.XenonAlertBox1)
        Me.Controls.Add(Me.XenonButton16)
        Me.Controls.Add(Me.XenonTextBox1)
        Me.Controls.Add(Me.PictureBox17)
        Me.Controls.Add(Me.Label102)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewExtTerminal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New External Terminal"
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox17 As PictureBox
    Friend WithEvents Label102 As Label
    Friend WithEvents XenonTextBox1 As XenonTextBox
    Friend WithEvents XenonButton16 As XenonButton
    Friend WithEvents XenonAlertBox1 As XenonAlertBox
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
