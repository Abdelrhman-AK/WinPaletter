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
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.CheckBox2 = New UI.WP.CheckBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.RadioImage1 = New WinPaletter.UI.WP.RadioImage()
        Me.RadioImage2 = New WinPaletter.UI.WP.RadioImage()
        Me.RadioImage3 = New WinPaletter.UI.WP.RadioImage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.Button6 = New WinPaletter.UI.WP.Button()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.AlertBox4 = New WinPaletter.UI.WP.AlertBox()
        Me.CheckBox3 = New UI.WP.CheckBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.AnimatedBox1 = New WinPaletter.UI.WP.AnimatedBox()
        Me.AlertBox2 = New WinPaletter.UI.WP.AlertBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AnimatedBox1.SuspendLayout()
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
        'CheckBox1
        '
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox1.Checked = True
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(59, 125)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(178, 24)
        Me.CheckBox1.TabIndex = 8
        Me.CheckBox1.Text = "Delete registry associations"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(29, 185)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'CheckBox2
        '
        Me.CheckBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox2.Checked = True
        Me.CheckBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox2.ForeColor = System.Drawing.Color.White
        Me.CheckBox2.Location = New System.Drawing.Point(59, 185)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(411, 24)
        Me.CheckBox2.TabIndex = 10
        Me.CheckBox2.Text = "Delete application data: %localappdata%\Abdelrhman-AK\WinPaletter"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(29, 292)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 11
        Me.PictureBox3.TabStop = False
        '
        'RadioImage1
        '
        Me.RadioImage1.Checked = True
        Me.RadioImage1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage1.ForeColor = System.Drawing.Color.White
        Me.RadioImage1.Image = Nothing
        Me.RadioImage1.Location = New System.Drawing.Point(156, 292)
        Me.RadioImage1.Name = "RadioImage1"
        Me.RadioImage1.ShowText = True
        Me.RadioImage1.Size = New System.Drawing.Size(280, 24)
        Me.RadioImage1.TabIndex = 12
        Me.RadioImage1.Text = "Do nothing"
        '
        'RadioImage2
        '
        Me.RadioImage2.Checked = False
        Me.RadioImage2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage2.ForeColor = System.Drawing.Color.White
        Me.RadioImage2.Image = Nothing
        Me.RadioImage2.Location = New System.Drawing.Point(156, 322)
        Me.RadioImage2.Name = "RadioImage2"
        Me.RadioImage2.ShowText = True
        Me.RadioImage2.Size = New System.Drawing.Size(280, 24)
        Me.RadioImage2.TabIndex = 13
        Me.RadioImage2.Text = "Restore from a theme file you backed-up before"
        '
        'RadioImage3
        '
        Me.RadioImage3.Checked = False
        Me.RadioImage3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage3.ForeColor = System.Drawing.Color.White
        Me.RadioImage3.Image = Nothing
        Me.RadioImage3.Location = New System.Drawing.Point(156, 352)
        Me.RadioImage3.Name = "RadioImage3"
        Me.RadioImage3.ShowText = True
        Me.RadioImage3.Size = New System.Drawing.Size(280, 24)
        Me.RadioImage3.TabIndex = 14
        Me.RadioImage3.Text = "Restore to default Windows"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(59, 292)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 24)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Theme restore:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 387)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(671, 1)
        Me.Separator1.TabIndex = 16
        Me.Separator1.TabStop = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.LineColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Button6.Location = New System.Drawing.Point(578, 400)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(105, 34)
        Me.Button6.TabIndex = 21
        Me.Button6.Text = "Uninstall"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(492, 400)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 34)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'AlertBox4
        '
        Me.AlertBox4.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.AlertBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.AlertBox4.CenterText = False
        Me.AlertBox4.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox4.Image = Nothing
        Me.AlertBox4.Location = New System.Drawing.Point(59, 215)
        Me.AlertBox4.Name = "AlertBox4"
        Me.AlertBox4.Size = New System.Drawing.Size(624, 22)
        Me.AlertBox4.TabIndex = 23
        Me.AlertBox4.TabStop = False
        Me.AlertBox4.Text = "Note: This will reset cursors to Aero as the custom cursors are rendered and save" &
    "d there"
        '
        'CheckBox3
        '
        Me.CheckBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.CheckBox3.Checked = True
        Me.CheckBox3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox3.ForeColor = System.Drawing.Color.White
        Me.CheckBox3.Location = New System.Drawing.Point(59, 155)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(188, 24)
        Me.CheckBox3.TabIndex = 25
        Me.CheckBox3.Text = "Delete WinPaletter's settings"
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(29, 155)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 24
        Me.PictureBox4.TabStop = False
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.AlertBox1.CenterText = True
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = Nothing
        Me.AlertBox1.Location = New System.Drawing.Point(12, 405)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(474, 24)
        Me.AlertBox1.TabIndex = 26
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "WinPaletter won't be able to delete itself, so delete the exe file after uninstal" &
    "l"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'AnimatedBox1
        '
        Me.AnimatedBox1.Color = System.Drawing.Color.Maroon
        Me.AnimatedBox1.Color1 = System.Drawing.Color.Maroon
        Me.AnimatedBox1.Color2 = System.Drawing.Color.Crimson
        Me.AnimatedBox1.Controls.Add(Me.Label2)
        Me.AnimatedBox1.Controls.Add(Me.PictureBox1)
        Me.AnimatedBox1.Controls.Add(Me.Label1)
        Me.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.AnimatedBox1.Location = New System.Drawing.Point(0, 0)
        Me.AnimatedBox1.Name = "AnimatedBox1"
        Me.AnimatedBox1.Size = New System.Drawing.Size(695, 86)
        Me.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors
        Me.AnimatedBox1.TabIndex = 27
        '
        'AlertBox2
        '
        Me.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple
        Me.AlertBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.AlertBox2.CenterText = False
        Me.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox2.Image = Nothing
        Me.AlertBox2.Location = New System.Drawing.Point(59, 243)
        Me.AlertBox2.Name = "AlertBox2"
        Me.AlertBox2.Size = New System.Drawing.Size(624, 40)
        Me.AlertBox2.TabIndex = 28
        Me.AlertBox2.TabStop = False
        Me.AlertBox2.Text = "Note: Windows (Vista and later) Startup backup wave file is located there. If you" &
    " delete this folder, you won't be able to restore this sound. Restore this sound" &
    " first then you can do an uninstall."
        '
        'Uninstall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(695, 446)
        Me.Controls.Add(Me.AlertBox2)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.AlertBox4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.RadioImage3)
        Me.Controls.Add(Me.RadioImage2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.RadioImage1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.AnimatedBox1)
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
        Me.AnimatedBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents CheckBox2 As UI.WP.CheckBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents RadioImage1 As UI.WP.RadioImage
    Friend WithEvents RadioImage2 As UI.WP.RadioImage
    Friend WithEvents RadioImage3 As UI.WP.RadioImage
    Friend WithEvents Label4 As Label
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents Button6 As UI.WP.Button
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents AlertBox4 As UI.WP.AlertBox
    Friend WithEvents CheckBox3 As UI.WP.CheckBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents AnimatedBox1 As UI.WP.AnimatedBox
    Friend WithEvents AlertBox2 As UI.WP.AlertBox
End Class
