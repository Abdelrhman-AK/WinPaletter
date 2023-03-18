<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Uninstall
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Uninstall))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.XenonCheckBox1 = New WinPaletter.XenonCheckBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.XenonCheckBox2 = New WinPaletter.XenonCheckBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.XenonRadioImage1 = New WinPaletter.XenonRadioImage()
        Me.XenonRadioImage2 = New WinPaletter.XenonRadioImage()
        Me.XenonRadioImage3 = New WinPaletter.XenonRadioImage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.XenonButton6 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonAlertBox4 = New WinPaletter.XenonAlertBox()
        Me.XenonCheckBox3 = New WinPaletter.XenonCheckBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.XenonAlertBox1 = New WinPaletter.XenonAlertBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.XenonAnimatedBox1 = New WinPaletter.XenonAnimatedBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonAnimatedBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(89, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(603, 24)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "This wizard will help you delete WinPaletter's data made during your usage"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(89, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(445, 35)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "WinPaletter uninstaller"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(80, 80)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(421, 28)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Choose operations to be done:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(29, 125)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 7
        Me.PictureBox6.TabStop = False
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox1.Checked = True
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(59, 125)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(178, 24)
        Me.XenonCheckBox1.TabIndex = 8
        Me.XenonCheckBox1.Text = "Delete registry associations"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(29, 201)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'XenonCheckBox2
        '
        Me.XenonCheckBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox2.Checked = True
        Me.XenonCheckBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox2.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox2.Location = New System.Drawing.Point(59, 201)
        Me.XenonCheckBox2.Name = "XenonCheckBox2"
        Me.XenonCheckBox2.Size = New System.Drawing.Size(411, 24)
        Me.XenonCheckBox2.TabIndex = 10
        Me.XenonCheckBox2.Text = "Delete application data: %localappdata%\Abdelrhman-AK\WinPaletter"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(29, 272)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 11
        Me.PictureBox3.TabStop = False
        '
        'XenonRadioImage1
        '
        Me.XenonRadioImage1.Checked = True
        Me.XenonRadioImage1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioImage1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioImage1.Image = Nothing
        Me.XenonRadioImage1.Location = New System.Drawing.Point(156, 272)
        Me.XenonRadioImage1.Name = "XenonRadioImage1"
        Me.XenonRadioImage1.ShowText = True
        Me.XenonRadioImage1.Size = New System.Drawing.Size(280, 24)
        Me.XenonRadioImage1.TabIndex = 12
        Me.XenonRadioImage1.Text = "Do nothing"
        '
        'XenonRadioImage2
        '
        Me.XenonRadioImage2.Checked = False
        Me.XenonRadioImage2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioImage2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioImage2.Image = Nothing
        Me.XenonRadioImage2.Location = New System.Drawing.Point(156, 302)
        Me.XenonRadioImage2.Name = "XenonRadioImage2"
        Me.XenonRadioImage2.ShowText = True
        Me.XenonRadioImage2.Size = New System.Drawing.Size(280, 24)
        Me.XenonRadioImage2.TabIndex = 13
        Me.XenonRadioImage2.Text = "Restore from a theme file you backed-up before"
        '
        'XenonRadioImage3
        '
        Me.XenonRadioImage3.Checked = False
        Me.XenonRadioImage3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioImage3.ForeColor = System.Drawing.Color.White
        Me.XenonRadioImage3.Image = Nothing
        Me.XenonRadioImage3.Location = New System.Drawing.Point(156, 332)
        Me.XenonRadioImage3.Name = "XenonRadioImage3"
        Me.XenonRadioImage3.ShowText = True
        Me.XenonRadioImage3.Size = New System.Drawing.Size(280, 24)
        Me.XenonRadioImage3.TabIndex = 14
        Me.XenonRadioImage3.Text = "Restore to default Windows"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(59, 272)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 24)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Theme restore:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.AlternativeLook = False
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 379)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(671, 1)
        Me.XenonSeparator1.TabIndex = 16
        Me.XenonSeparator1.TabStop = False
        '
        'XenonButton6
        '
        Me.XenonButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton6.ForeColor = System.Drawing.Color.White
        Me.XenonButton6.Image = CType(resources.GetObject("XenonButton6.Image"), System.Drawing.Image)
        Me.XenonButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton6.LineColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.XenonButton6.LineSize = 1
        Me.XenonButton6.Location = New System.Drawing.Point(567, 391)
        Me.XenonButton6.Name = "XenonButton6"
        Me.XenonButton6.Size = New System.Drawing.Size(116, 35)
        Me.XenonButton6.TabIndex = 21
        Me.XenonButton6.Text = "Uninstall"
        Me.XenonButton6.UseVisualStyleBackColor = False
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
        Me.XenonButton2.Location = New System.Drawing.Point(445, 391)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(116, 35)
        Me.XenonButton2.TabIndex = 22
        Me.XenonButton2.Text = "Cancel"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonAlertBox4
        '
        Me.XenonAlertBox4.AlertStyle = WinPaletter.XenonAlertBox.Style.Simple
        Me.XenonAlertBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.XenonAlertBox4.CenterText = False
        Me.XenonAlertBox4.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox4.Image = Nothing
        Me.XenonAlertBox4.Location = New System.Drawing.Point(59, 231)
        Me.XenonAlertBox4.Name = "XenonAlertBox4"
        Me.XenonAlertBox4.Size = New System.Drawing.Size(484, 24)
        Me.XenonAlertBox4.TabIndex = 23
        Me.XenonAlertBox4.TabStop = False
        Me.XenonAlertBox4.Text = "Note: This will reset cursors to Aero as the custom cursors are rendered and save" &
    "d there"
        '
        'XenonCheckBox3
        '
        Me.XenonCheckBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox3.Checked = True
        Me.XenonCheckBox3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox3.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox3.Location = New System.Drawing.Point(59, 163)
        Me.XenonCheckBox3.Name = "XenonCheckBox3"
        Me.XenonCheckBox3.Size = New System.Drawing.Size(188, 24)
        Me.XenonCheckBox3.TabIndex = 25
        Me.XenonCheckBox3.Text = "Delete WinPaletter's settings"
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(29, 163)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 24
        Me.PictureBox4.TabStop = False
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.XenonAlertBox.Style.Warning
        Me.XenonAlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.XenonAlertBox1.CenterText = True
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Nothing
        Me.XenonAlertBox1.Location = New System.Drawing.Point(12, 396)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(424, 24)
        Me.XenonAlertBox1.TabIndex = 26
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = "WinPaletter won't be able to delete itself, so delete the exe file after uninstal" &
    "l"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'XenonAnimatedBox1
        '
        Me.XenonAnimatedBox1.Color = System.Drawing.Color.Maroon
        Me.XenonAnimatedBox1.Color1 = System.Drawing.Color.Maroon
        Me.XenonAnimatedBox1.Color2 = System.Drawing.Color.Crimson
        Me.XenonAnimatedBox1.Controls.Add(Me.Label2)
        Me.XenonAnimatedBox1.Controls.Add(Me.PictureBox1)
        Me.XenonAnimatedBox1.Controls.Add(Me.Label1)
        Me.XenonAnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XenonAnimatedBox1.Location = New System.Drawing.Point(0, 0)
        Me.XenonAnimatedBox1.Name = "XenonAnimatedBox1"
        Me.XenonAnimatedBox1.Size = New System.Drawing.Size(695, 86)
        Me.XenonAnimatedBox1.Style = WinPaletter.XenonAnimatedBox.ColorsStyle.MixedColors
        Me.XenonAnimatedBox1.TabIndex = 27
        '
        'Uninstall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(695, 438)
        Me.Controls.Add(Me.XenonAlertBox1)
        Me.Controls.Add(Me.XenonCheckBox3)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.XenonAlertBox4)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton6)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.XenonRadioImage3)
        Me.Controls.Add(Me.XenonRadioImage2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.XenonRadioImage1)
        Me.Controls.Add(Me.XenonCheckBox1)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.XenonCheckBox2)
        Me.Controls.Add(Me.XenonAnimatedBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Uninstall"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Uninstall"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonAnimatedBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents XenonCheckBox1 As XenonCheckBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents XenonCheckBox2 As XenonCheckBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents XenonRadioImage1 As XenonRadioImage
    Friend WithEvents XenonRadioImage2 As XenonRadioImage
    Friend WithEvents XenonRadioImage3 As XenonRadioImage
    Friend WithEvents Label4 As Label
    Friend WithEvents XenonSeparator1 As XenonSeparator
    Friend WithEvents XenonButton6 As XenonButton
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonAlertBox4 As XenonAlertBox
    Friend WithEvents XenonCheckBox3 As XenonCheckBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents XenonAlertBox1 As XenonAlertBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents XenonAnimatedBox1 As XenonAnimatedBox
End Class
