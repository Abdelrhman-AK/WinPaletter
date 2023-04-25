<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Wallpaper_Editor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wallpaper_Editor))
        Me.OpenImgDlg = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.XenonGroupBox12 = New WinPaletter.XenonGroupBox()
        Me.XenonButton9 = New WinPaletter.XenonButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.XenonButton11 = New WinPaletter.XenonButton()
        Me.XenonButton12 = New WinPaletter.XenonButton()
        Me.WallpaperEnabled = New WinPaletter.XenonToggle()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.XenonButton10 = New WinPaletter.XenonButton()
        Me.XenonButton7 = New WinPaletter.XenonButton()
        Me.XenonButton8 = New WinPaletter.XenonButton()
        Me.XenonTabControl1 = New WinPaletter.XenonTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.source_spotlight = New WinPaletter.XenonRadioImage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.source_slideshow = New WinPaletter.XenonRadioImage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.source_color = New WinPaletter.XenonRadioImage()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.source_pic = New WinPaletter.XenonRadioImage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.style_fit = New WinPaletter.XenonRadioImage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.style_fill = New WinPaletter.XenonRadioImage()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.style_stretch = New WinPaletter.XenonRadioImage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.style_center = New WinPaletter.XenonRadioImage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.style_tile = New WinPaletter.XenonRadioImage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonTextBox1 = New WinPaletter.XenonTextBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.XenonCheckBox3 = New WinPaletter.XenonCheckBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox16 = New System.Windows.Forms.PictureBox()
        Me.MD = New WinPaletter.XenonButton()
        Me.XenonTrackbar1 = New WinPaletter.XenonTrackbar()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.XenonSeparator3 = New WinPaletter.XenonSeparator()
        Me.XenonRadioButton2 = New WinPaletter.XenonRadioButton()
        Me.XenonRadioButton1 = New WinPaletter.XenonRadioButton()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.XenonButton17 = New WinPaletter.XenonButton()
        Me.XenonButton18 = New WinPaletter.XenonButton()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.XenonButton4 = New WinPaletter.XenonButton()
        Me.XenonTextBox2 = New WinPaletter.XenonTextBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.XenonGroupBox12.SuspendLayout()
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonTabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenImgDlg
        '
        Me.OpenImgDlg.Filter = "Images (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All Files (*.*)|*.*"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'XenonGroupBox12
        '
        Me.XenonGroupBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox12.Controls.Add(Me.XenonButton9)
        Me.XenonGroupBox12.Controls.Add(Me.Label12)
        Me.XenonGroupBox12.Controls.Add(Me.XenonButton11)
        Me.XenonGroupBox12.Controls.Add(Me.XenonButton12)
        Me.XenonGroupBox12.Controls.Add(Me.WallpaperEnabled)
        Me.XenonGroupBox12.Controls.Add(Me.checker_img)
        Me.XenonGroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.XenonGroupBox12.Name = "XenonGroupBox12"
        Me.XenonGroupBox12.Size = New System.Drawing.Size(793, 39)
        Me.XenonGroupBox12.TabIndex = 201
        '
        'XenonButton9
        '
        Me.XenonButton9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton9.ForeColor = System.Drawing.Color.White
        Me.XenonButton9.Image = CType(resources.GetObject("XenonButton9.Image"), System.Drawing.Image)
        Me.XenonButton9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton9.LineColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.XenonButton9.LineSize = 1
        Me.XenonButton9.Location = New System.Drawing.Point(222, 5)
        Me.XenonButton9.Name = "XenonButton9"
        Me.XenonButton9.Size = New System.Drawing.Size(126, 29)
        Me.XenonButton9.TabIndex = 112
        Me.XenonButton9.Text = "Current applied"
        Me.XenonButton9.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 31)
        Me.Label12.TabIndex = 111
        Me.Label12.Text = "Open from:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton11
        '
        Me.XenonButton11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton11.ForeColor = System.Drawing.Color.White
        Me.XenonButton11.Image = CType(resources.GetObject("XenonButton11.Image"), System.Drawing.Image)
        Me.XenonButton11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton11.LineColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.XenonButton11.LineSize = 1
        Me.XenonButton11.Location = New System.Drawing.Point(85, 5)
        Me.XenonButton11.Name = "XenonButton11"
        Me.XenonButton11.Size = New System.Drawing.Size(135, 29)
        Me.XenonButton11.TabIndex = 110
        Me.XenonButton11.Text = "WinPaletter theme"
        Me.XenonButton11.UseVisualStyleBackColor = False
        '
        'XenonButton12
        '
        Me.XenonButton12.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton12.ForeColor = System.Drawing.Color.White
        Me.XenonButton12.Image = Nothing
        Me.XenonButton12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton12.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.XenonButton12.LineSize = 1
        Me.XenonButton12.Location = New System.Drawing.Point(351, 5)
        Me.XenonButton12.Name = "XenonButton12"
        Me.XenonButton12.Size = New System.Drawing.Size(135, 29)
        Me.XenonButton12.TabIndex = 108
        Me.XenonButton12.Text = "Default Windows"
        Me.XenonButton12.UseVisualStyleBackColor = False
        '
        'WallpaperEnabled
        '
        Me.WallpaperEnabled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WallpaperEnabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.WallpaperEnabled.Checked = False
        Me.WallpaperEnabled.DarkLight_Toggler = False
        Me.WallpaperEnabled.Location = New System.Drawing.Point(748, 9)
        Me.WallpaperEnabled.Name = "WallpaperEnabled"
        Me.WallpaperEnabled.Size = New System.Drawing.Size(40, 20)
        Me.WallpaperEnabled.TabIndex = 85
        '
        'checker_img
        '
        Me.checker_img.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checker_img.Image = Global.WinPaletter.My.Resources.Resources.checker_disabled
        Me.checker_img.Location = New System.Drawing.Point(707, 4)
        Me.checker_img.Name = "checker_img"
        Me.checker_img.Size = New System.Drawing.Size(35, 31)
        Me.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.checker_img.TabIndex = 83
        Me.checker_img.TabStop = False
        '
        'XenonButton10
        '
        Me.XenonButton10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton10.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton10.ForeColor = System.Drawing.Color.White
        Me.XenonButton10.Image = CType(resources.GetObject("XenonButton10.Image"), System.Drawing.Image)
        Me.XenonButton10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton10.LineColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.XenonButton10.LineSize = 1
        Me.XenonButton10.Location = New System.Drawing.Point(504, 367)
        Me.XenonButton10.Name = "XenonButton10"
        Me.XenonButton10.Size = New System.Drawing.Size(115, 34)
        Me.XenonButton10.TabIndex = 213
        Me.XenonButton10.Text = "Quick apply"
        Me.XenonButton10.UseVisualStyleBackColor = False
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
        Me.XenonButton7.Location = New System.Drawing.Point(418, 367)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton7.TabIndex = 212
        Me.XenonButton7.Text = "Cancel"
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonButton8
        '
        Me.XenonButton8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton8.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton8.ForeColor = System.Drawing.Color.White
        Me.XenonButton8.Image = CType(resources.GetObject("XenonButton8.Image"), System.Drawing.Image)
        Me.XenonButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.XenonButton8.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XenonButton8.LineSize = 1
        Me.XenonButton8.Location = New System.Drawing.Point(625, 367)
        Me.XenonButton8.Name = "XenonButton8"
        Me.XenonButton8.Size = New System.Drawing.Size(180, 34)
        Me.XenonButton8.TabIndex = 211
        Me.XenonButton8.Text = "Load into current theme"
        Me.XenonButton8.UseVisualStyleBackColor = False
        '
        'XenonTabControl1
        '
        Me.XenonTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.XenonTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTabControl1.Controls.Add(Me.TabPage1)
        Me.XenonTabControl1.Controls.Add(Me.TabPage2)
        Me.XenonTabControl1.Controls.Add(Me.TabPage3)
        Me.XenonTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.XenonTabControl1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonTabControl1.ItemSize = New System.Drawing.Size(30, 140)
        Me.XenonTabControl1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonTabControl1.Location = New System.Drawing.Point(12, 57)
        Me.XenonTabControl1.Multiline = True
        Me.XenonTabControl1.Name = "XenonTabControl1"
        Me.XenonTabControl1.SelectedIndex = 0
        Me.XenonTabControl1.Size = New System.Drawing.Size(793, 304)
        Me.XenonTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.XenonTabControl1.TabIndex = 214
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.source_spotlight)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.source_slideshow)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.source_color)
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.source_pic)
        Me.TabPage1.Location = New System.Drawing.Point(144, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(645, 296)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Source"
        '
        'Label3
        '
        Me.Label3.AutoEllipsis = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(457, 170)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 27)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Windows Spotlight"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'source_spotlight
        '
        Me.source_spotlight.Checked = False
        Me.source_spotlight.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.source_spotlight.ForeColor = System.Drawing.Color.White
        Me.source_spotlight.Image = CType(resources.GetObject("source_spotlight.Image"), System.Drawing.Image)
        Me.source_spotlight.Location = New System.Drawing.Point(488, 100)
        Me.source_spotlight.Name = "source_spotlight"
        Me.source_spotlight.ShowText = False
        Me.source_spotlight.Size = New System.Drawing.Size(64, 64)
        Me.source_spotlight.TabIndex = 41
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(325, 170)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 27)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Slideshow"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'source_slideshow
        '
        Me.source_slideshow.Checked = False
        Me.source_slideshow.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.source_slideshow.ForeColor = System.Drawing.Color.White
        Me.source_slideshow.Image = CType(resources.GetObject("source_slideshow.Image"), System.Drawing.Image)
        Me.source_slideshow.Location = New System.Drawing.Point(356, 100)
        Me.source_slideshow.Name = "source_slideshow"
        Me.source_slideshow.ShowText = False
        Me.source_slideshow.Size = New System.Drawing.Size(64, 64)
        Me.source_slideshow.TabIndex = 39
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(193, 170)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 27)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Color"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'source_color
        '
        Me.source_color.Checked = False
        Me.source_color.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.source_color.ForeColor = System.Drawing.Color.White
        Me.source_color.Image = CType(resources.GetObject("source_color.Image"), System.Drawing.Image)
        Me.source_color.Location = New System.Drawing.Point(224, 100)
        Me.source_color.Name = "source_color"
        Me.source_color.ShowText = False
        Me.source_color.Size = New System.Drawing.Size(64, 64)
        Me.source_color.TabIndex = 37
        '
        'Label24
        '
        Me.Label24.AutoEllipsis = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(61, 170)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(126, 27)
        Me.Label24.TabIndex = 36
        Me.Label24.Text = "Picture"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'source_pic
        '
        Me.source_pic.Checked = False
        Me.source_pic.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.source_pic.ForeColor = System.Drawing.Color.White
        Me.source_pic.Image = CType(resources.GetObject("source_pic.Image"), System.Drawing.Image)
        Me.source_pic.Location = New System.Drawing.Point(92, 100)
        Me.source_pic.Name = "source_pic"
        Me.source_pic.ShowText = False
        Me.source_pic.Size = New System.Drawing.Size(64, 64)
        Me.source_pic.TabIndex = 35
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.style_fit)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.style_fill)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.style_stretch)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.style_center)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.style_tile)
        Me.TabPage2.Controls.Add(Me.PictureBox1)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.XenonButton3)
        Me.TabPage2.Controls.Add(Me.XenonButton2)
        Me.TabPage2.Controls.Add(Me.XenonButton1)
        Me.TabPage2.Controls.Add(Me.XenonTextBox1)
        Me.TabPage2.Controls.Add(Me.PictureBox4)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Location = New System.Drawing.Point(144, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(645, 296)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Wallpaper"
        '
        'Label10
        '
        Me.Label10.AutoEllipsis = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(193, 111)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 27)
        Me.Label10.TabIndex = 157
        Me.Label10.Text = "Fit"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'style_fit
        '
        Me.style_fit.Checked = False
        Me.style_fit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.style_fit.ForeColor = System.Drawing.Color.White
        Me.style_fit.Image = CType(resources.GetObject("style_fit.Image"), System.Drawing.Image)
        Me.style_fit.Location = New System.Drawing.Point(193, 41)
        Me.style_fit.Name = "style_fit"
        Me.style_fit.ShowText = False
        Me.style_fit.Size = New System.Drawing.Size(80, 64)
        Me.style_fit.TabIndex = 156
        '
        'Label9
        '
        Me.Label9.AutoEllipsis = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(107, 111)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 27)
        Me.Label9.TabIndex = 155
        Me.Label9.Text = "Fill"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'style_fill
        '
        Me.style_fill.Checked = True
        Me.style_fill.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.style_fill.ForeColor = System.Drawing.Color.White
        Me.style_fill.Image = CType(resources.GetObject("style_fill.Image"), System.Drawing.Image)
        Me.style_fill.Location = New System.Drawing.Point(107, 41)
        Me.style_fill.Name = "style_fill"
        Me.style_fill.ShowText = False
        Me.style_fill.Size = New System.Drawing.Size(80, 64)
        Me.style_fill.TabIndex = 154
        '
        'Label8
        '
        Me.Label8.AutoEllipsis = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(279, 111)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 27)
        Me.Label8.TabIndex = 153
        Me.Label8.Text = "Stretch"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'style_stretch
        '
        Me.style_stretch.Checked = False
        Me.style_stretch.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.style_stretch.ForeColor = System.Drawing.Color.White
        Me.style_stretch.Image = CType(resources.GetObject("style_stretch.Image"), System.Drawing.Image)
        Me.style_stretch.Location = New System.Drawing.Point(279, 41)
        Me.style_stretch.Name = "style_stretch"
        Me.style_stretch.ShowText = False
        Me.style_stretch.Size = New System.Drawing.Size(80, 64)
        Me.style_stretch.TabIndex = 152
        '
        'Label7
        '
        Me.Label7.AutoEllipsis = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(365, 111)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 27)
        Me.Label7.TabIndex = 151
        Me.Label7.Text = "Centered"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'style_center
        '
        Me.style_center.Checked = False
        Me.style_center.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.style_center.ForeColor = System.Drawing.Color.White
        Me.style_center.Image = CType(resources.GetObject("style_center.Image"), System.Drawing.Image)
        Me.style_center.Location = New System.Drawing.Point(365, 41)
        Me.style_center.Name = "style_center"
        Me.style_center.ShowText = False
        Me.style_center.Size = New System.Drawing.Size(80, 64)
        Me.style_center.TabIndex = 150
        '
        'Label6
        '
        Me.Label6.AutoEllipsis = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(451, 111)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 27)
        Me.Label6.TabIndex = 149
        Me.Label6.Text = "Tile"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'style_tile
        '
        Me.style_tile.Checked = False
        Me.style_tile.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.style_tile.ForeColor = System.Drawing.Color.White
        Me.style_tile.Image = CType(resources.GetObject("style_tile.Image"), System.Drawing.Image)
        Me.style_tile.Location = New System.Drawing.Point(451, 41)
        Me.style_tile.Name = "style_tile"
        Me.style_tile.ShowText = False
        Me.style_tile.Size = New System.Drawing.Size(80, 64)
        Me.style_tile.TabIndex = 148
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 63)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 147
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(36, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 24)
        Me.Label5.TabIndex = 146
        Me.Label5.Text = "Style:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = Nothing
        Me.XenonButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(512, 6)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(85, 24)
        Me.XenonButton3.TabIndex = 144
        Me.XenonButton3.Text = "Get default"
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(421, 6)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(85, 24)
        Me.XenonButton2.TabIndex = 143
        Me.XenonButton2.Text = "Get current"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = CType(resources.GetObject("XenonButton1.Image"), System.Drawing.Image)
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(604, 6)
        Me.XenonButton1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(34, 24)
        Me.XenonButton1.TabIndex = 142
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonTextBox1
        '
        Me.XenonTextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox1.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox1.Location = New System.Drawing.Point(107, 6)
        Me.XenonTextBox1.MaxLength = 32767
        Me.XenonTextBox1.Multiline = False
        Me.XenonTextBox1.Name = "XenonTextBox1"
        Me.XenonTextBox1.ReadOnly = False
        Me.XenonTextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox1.SelectedText = ""
        Me.XenonTextBox1.SelectionLength = 0
        Me.XenonTextBox1.SelectionStart = 0
        Me.XenonTextBox1.Size = New System.Drawing.Size(308, 24)
        Me.XenonTextBox1.TabIndex = 141
        Me.XenonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox1.UseSystemPasswordChar = False
        Me.XenonTextBox1.WordWrap = True
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 140
        Me.PictureBox4.TabStop = False
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(36, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 24)
        Me.Label4.TabIndex = 139
        Me.Label4.Text = "Image:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.Label13)
        Me.TabPage3.Controls.Add(Me.XenonCheckBox3)
        Me.TabPage3.Controls.Add(Me.PictureBox7)
        Me.TabPage3.Controls.Add(Me.PictureBox16)
        Me.TabPage3.Controls.Add(Me.MD)
        Me.TabPage3.Controls.Add(Me.XenonTrackbar1)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.XenonSeparator3)
        Me.TabPage3.Controls.Add(Me.XenonRadioButton2)
        Me.TabPage3.Controls.Add(Me.XenonRadioButton1)
        Me.TabPage3.Controls.Add(Me.PictureBox6)
        Me.TabPage3.Controls.Add(Me.XenonButton17)
        Me.TabPage3.Controls.Add(Me.XenonButton18)
        Me.TabPage3.Controls.Add(Me.ListBox1)
        Me.TabPage3.Controls.Add(Me.XenonButton4)
        Me.TabPage3.Controls.Add(Me.XenonTextBox2)
        Me.TabPage3.Controls.Add(Me.PictureBox5)
        Me.TabPage3.Location = New System.Drawing.Point(144, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(645, 296)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Slideshow"
        '
        'Label13
        '
        Me.Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.Location = New System.Drawing.Point(163, 132)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(475, 24)
        Me.Label13.TabIndex = 183
        Me.Label13.Text = "Images in the list must be from the same folder"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonCheckBox3
        '
        Me.XenonCheckBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonCheckBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox3.Checked = False
        Me.XenonCheckBox3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox3.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox3.Location = New System.Drawing.Point(36, 202)
        Me.XenonCheckBox3.Name = "XenonCheckBox3"
        Me.XenonCheckBox3.Size = New System.Drawing.Size(602, 24)
        Me.XenonCheckBox3.TabIndex = 182
        Me.XenonCheckBox3.Text = "Shuffle"
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(6, 202)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 181
        Me.PictureBox7.TabStop = False
        '
        'PictureBox16
        '
        Me.PictureBox16.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox16.Image = CType(resources.GetObject("PictureBox16.Image"), System.Drawing.Image)
        Me.PictureBox16.Location = New System.Drawing.Point(6, 172)
        Me.PictureBox16.Name = "PictureBox16"
        Me.PictureBox16.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox16.TabIndex = 180
        Me.PictureBox16.TabStop = False
        '
        'MD
        '
        Me.MD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MD.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.MD.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MD.ForeColor = System.Drawing.Color.White
        Me.MD.Image = Nothing
        Me.MD.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.MD.LineSize = 1
        Me.MD.Location = New System.Drawing.Point(574, 172)
        Me.MD.Name = "MD"
        Me.MD.Size = New System.Drawing.Size(64, 24)
        Me.MD.TabIndex = 179
        Me.MD.UseVisualStyleBackColor = False
        '
        'XenonTrackbar1
        '
        Me.XenonTrackbar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTrackbar1.LargeChange = 1000
        Me.XenonTrackbar1.Location = New System.Drawing.Point(161, 175)
        Me.XenonTrackbar1.Maximum = 36000000
        Me.XenonTrackbar1.Minimum = 10000
        Me.XenonTrackbar1.Name = "XenonTrackbar1"
        Me.XenonTrackbar1.Size = New System.Drawing.Size(407, 19)
        Me.XenonTrackbar1.SmallChange = 100
        Me.XenonTrackbar1.TabIndex = 178
        Me.XenonTrackbar1.Value = 10000
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(36, 172)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(119, 24)
        Me.Label11.TabIndex = 176
        Me.Label11.Text = "Change every (ms):"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonSeparator3
        '
        Me.XenonSeparator3.AlternativeLook = False
        Me.XenonSeparator3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator3.Location = New System.Drawing.Point(6, 165)
        Me.XenonSeparator3.Name = "XenonSeparator3"
        Me.XenonSeparator3.Size = New System.Drawing.Size(632, 1)
        Me.XenonSeparator3.TabIndex = 175
        Me.XenonSeparator3.TabStop = False
        '
        'XenonRadioButton2
        '
        Me.XenonRadioButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonRadioButton2.Checked = False
        Me.XenonRadioButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton2.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton2.Location = New System.Drawing.Point(36, 36)
        Me.XenonRadioButton2.Name = "XenonRadioButton2"
        Me.XenonRadioButton2.Size = New System.Drawing.Size(119, 24)
        Me.XenonRadioButton2.TabIndex = 174
        Me.XenonRadioButton2.Text = "List of images:"
        '
        'XenonRadioButton1
        '
        Me.XenonRadioButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonRadioButton1.Checked = False
        Me.XenonRadioButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonRadioButton1.ForeColor = System.Drawing.Color.White
        Me.XenonRadioButton1.Location = New System.Drawing.Point(36, 6)
        Me.XenonRadioButton1.Name = "XenonRadioButton1"
        Me.XenonRadioButton1.Size = New System.Drawing.Size(119, 24)
        Me.XenonRadioButton1.TabIndex = 173
        Me.XenonRadioButton1.Text = "Folder:"
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(6, 36)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 172
        Me.PictureBox6.TabStop = False
        '
        'XenonButton17
        '
        Me.XenonButton17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton17.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton17.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton17.ForeColor = System.Drawing.Color.White
        Me.XenonButton17.Image = CType(resources.GetObject("XenonButton17.Image"), System.Drawing.Image)
        Me.XenonButton17.LineColor = System.Drawing.Color.FromArgb(CType(CType(151, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.XenonButton17.LineSize = 1
        Me.XenonButton17.Location = New System.Drawing.Point(604, 85)
        Me.XenonButton17.Name = "XenonButton17"
        Me.XenonButton17.Size = New System.Drawing.Size(34, 44)
        Me.XenonButton17.TabIndex = 170
        Me.XenonButton17.UseVisualStyleBackColor = False
        '
        'XenonButton18
        '
        Me.XenonButton18.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton18.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton18.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton18.ForeColor = System.Drawing.Color.White
        Me.XenonButton18.Image = CType(resources.GetObject("XenonButton18.Image"), System.Drawing.Image)
        Me.XenonButton18.LineColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XenonButton18.LineSize = 1
        Me.XenonButton18.Location = New System.Drawing.Point(604, 36)
        Me.XenonButton18.Name = "XenonButton18"
        Me.XenonButton18.Size = New System.Drawing.Size(34, 44)
        Me.XenonButton18.TabIndex = 169
        Me.XenonButton18.UseVisualStyleBackColor = False
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox1.ForeColor = System.Drawing.Color.White
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(161, 36)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.ListBox1.Size = New System.Drawing.Size(437, 92)
        Me.ListBox1.TabIndex = 168
        '
        'XenonButton4
        '
        Me.XenonButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = CType(resources.GetObject("XenonButton4.Image"), System.Drawing.Image)
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(604, 6)
        Me.XenonButton4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(34, 24)
        Me.XenonButton4.TabIndex = 167
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'XenonTextBox2
        '
        Me.XenonTextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox2.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox2.Location = New System.Drawing.Point(161, 6)
        Me.XenonTextBox2.MaxLength = 32767
        Me.XenonTextBox2.Multiline = False
        Me.XenonTextBox2.Name = "XenonTextBox2"
        Me.XenonTextBox2.ReadOnly = False
        Me.XenonTextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.XenonTextBox2.SelectedText = ""
        Me.XenonTextBox2.SelectionLength = 0
        Me.XenonTextBox2.SelectionStart = 0
        Me.XenonTextBox2.Size = New System.Drawing.Size(436, 24)
        Me.XenonTextBox2.TabIndex = 166
        Me.XenonTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox2.UseSystemPasswordChar = False
        Me.XenonTextBox2.WordWrap = True
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 165
        Me.PictureBox5.TabStop = False
        '
        'Wallpaper_Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(817, 413)
        Me.Controls.Add(Me.XenonTabControl1)
        Me.Controls.Add(Me.XenonButton10)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonButton8)
        Me.Controls.Add(Me.XenonGroupBox12)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Wallpaper_Editor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Wallpaper"
        Me.XenonGroupBox12.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonTabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents OpenImgDlg As OpenFileDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents XenonGroupBox12 As XenonGroupBox
    Friend WithEvents XenonButton9 As XenonButton
    Friend WithEvents Label12 As Label
    Friend WithEvents XenonButton11 As XenonButton
    Friend WithEvents XenonButton12 As XenonButton
    Friend WithEvents WallpaperEnabled As XenonToggle
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents XenonButton10 As XenonButton
    Friend WithEvents XenonButton7 As XenonButton
    Friend WithEvents XenonButton8 As XenonButton
    Friend WithEvents XenonTabControl1 As XenonTabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents source_spotlight As XenonRadioImage
    Friend WithEvents Label2 As Label
    Friend WithEvents source_slideshow As XenonRadioImage
    Friend WithEvents Label1 As Label
    Friend WithEvents source_color As XenonRadioImage
    Friend WithEvents Label24 As Label
    Friend WithEvents source_pic As XenonRadioImage
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonTextBox1 As XenonTextBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents style_fit As XenonRadioImage
    Friend WithEvents Label9 As Label
    Friend WithEvents style_fill As XenonRadioImage
    Friend WithEvents Label8 As Label
    Friend WithEvents style_stretch As XenonRadioImage
    Friend WithEvents Label7 As Label
    Friend WithEvents style_center As XenonRadioImage
    Friend WithEvents Label6 As Label
    Friend WithEvents style_tile As XenonRadioImage
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents XenonButton4 As XenonButton
    Friend WithEvents XenonTextBox2 As XenonTextBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents XenonButton17 As XenonButton
    Friend WithEvents XenonButton18 As XenonButton
    Friend WithEvents XenonRadioButton2 As XenonRadioButton
    Friend WithEvents XenonRadioButton1 As XenonRadioButton
    Friend WithEvents Label11 As Label
    Friend WithEvents XenonSeparator3 As XenonSeparator
    Friend WithEvents MD As XenonButton
    Friend WithEvents XenonTrackbar1 As XenonTrackbar
    Friend WithEvents PictureBox16 As PictureBox
    Friend WithEvents XenonCheckBox3 As XenonCheckBox
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents Label13 As Label
End Class
