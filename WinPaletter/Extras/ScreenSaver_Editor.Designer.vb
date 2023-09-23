<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScreenSaver_Editor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScreenSaver_Editor))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenThemeDialog = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1 = New WinPaletter.UI.WP.GroupBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Trackbar5 = New WinPaletter.UI.WP.Trackbar()
        Me.PictureBox17 = New System.Windows.Forms.PictureBox()
        Me.CheckBox1 = New UI.WP.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button4 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox1 = New WinPaletter.UI.WP.TextBox()
        Me.previewContainer = New WinPaletter.UI.WP.GroupBox()
        Me.Button14 = New WinPaletter.UI.WP.Button()
        Me.Button13 = New WinPaletter.UI.WP.Button()
        Me.Button6 = New WinPaletter.UI.WP.Button()
        Me.Button5 = New WinPaletter.UI.WP.Button()
        Me.pnl_preview = New System.Windows.Forms.Panel()
        Me.PictureBox41 = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Button10 = New WinPaletter.UI.WP.Button()
        Me.Button7 = New WinPaletter.UI.WP.Button()
        Me.Button8 = New WinPaletter.UI.WP.Button()
        Me.GroupBox12 = New WinPaletter.UI.WP.GroupBox()
        Me.Button259 = New WinPaletter.UI.WP.Button()
        Me.Button9 = New WinPaletter.UI.WP.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button11 = New WinPaletter.UI.WP.Button()
        Me.Button12 = New WinPaletter.UI.WP.Button()
        Me.ScrSvrEnabled = New UI.WP.Toggle()
        Me.checker_img = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.previewContainer.SuspendLayout()
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox12.SuspendLayout()
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "wpt"
        Me.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.DefaultExt = "wpt"
        Me.OpenFileDialog2.Filter = "Screen Saver (*.scr)|*.scr"
        '
        'OpenThemeDialog
        '
        Me.OpenThemeDialog.Filter = "Windows Theme (*.theme)|*.theme|All Files (*.*)|*.*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.PictureBox4)
        Me.GroupBox1.Controls.Add(Me.Trackbar5)
        Me.GroupBox1.Controls.Add(Me.PictureBox17)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 408)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(750, 90)
        Me.GroupBox1.TabIndex = 226
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 217
        Me.PictureBox4.TabStop = False
        '
        'Trackbar5
        '
        Me.Trackbar5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Trackbar5.LargeChange = 10
        Me.Trackbar5.Location = New System.Drawing.Point(129, 36)
        Me.Trackbar5.Maximum = 3600
        Me.Trackbar5.Minimum = 60
        Me.Trackbar5.Name = "Trackbar5"
        Me.Trackbar5.Size = New System.Drawing.Size(535, 19)
        Me.Trackbar5.SmallChange = 1
        Me.Trackbar5.TabIndex = 214
        Me.Trackbar5.Value = 60
        '
        'PictureBox17
        '
        Me.PictureBox17.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox17.Image = CType(resources.GetObject("PictureBox17.Image"), System.Drawing.Image)
        Me.PictureBox17.Location = New System.Drawing.Point(3, 33)
        Me.PictureBox17.Name = "PictureBox17"
        Me.PictureBox17.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox17.TabIndex = 212
        Me.PictureBox17.TabStop = False
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.CheckBox1.Checked = False
        Me.CheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(33, 63)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(713, 24)
        Me.CheckBox1.TabIndex = 223
        Me.CheckBox1.Text = "On resume, password protect"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(33, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 24)
        Me.Label4.TabIndex = 213
        Me.Label4.Text = "Timeout (sec):"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 63)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 222
        Me.PictureBox1.TabStop = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button4.DrawOnGlass = False
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Image = Nothing
        Me.Button4.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.Button4.LineSize = 1
        Me.Button4.Location = New System.Drawing.Point(670, 33)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(76, 24)
        Me.Button4.TabIndex = 215
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(712, 3)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(34, 24)
        Me.Button1.TabIndex = 219
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(33, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 24)
        Me.Label3.TabIndex = 216
        Me.Label3.Text = "File:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TextBox1.DrawOnGlass = False
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(129, 3)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Multiline = False
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = False
        Me.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.SelectionLength = 0
        Me.TextBox1.SelectionStart = 0
        Me.TextBox1.Size = New System.Drawing.Size(576, 24)
        Me.TextBox1.TabIndex = 218
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBox1.UseSystemPasswordChar = False
        Me.TextBox1.WordWrap = True
        '
        'previewContainer
        '
        Me.previewContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.previewContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.previewContainer.Controls.Add(Me.Button14)
        Me.previewContainer.Controls.Add(Me.Button13)
        Me.previewContainer.Controls.Add(Me.Button6)
        Me.previewContainer.Controls.Add(Me.Button5)
        Me.previewContainer.Controls.Add(Me.pnl_preview)
        Me.previewContainer.Controls.Add(Me.PictureBox41)
        Me.previewContainer.Controls.Add(Me.Label19)
        Me.previewContainer.Location = New System.Drawing.Point(12, 57)
        Me.previewContainer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.previewContainer.Name = "previewContainer"
        Me.previewContainer.Padding = New System.Windows.Forms.Padding(1)
        Me.previewContainer.Size = New System.Drawing.Size(750, 345)
        Me.previewContainer.TabIndex = 211
        '
        'Button14
        '
        Me.Button14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button14.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button14.DrawOnGlass = False
        Me.Button14.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button14.ForeColor = System.Drawing.Color.White
        Me.Button14.Image = Nothing
        Me.Button14.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button14.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.Button14.LineSize = 1
        Me.Button14.Location = New System.Drawing.Point(425, 8)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(150, 24)
        Me.Button14.TabIndex = 225
        Me.Button14.Text = "Configure its settings"
        Me.Button14.UseVisualStyleBackColor = False
        '
        'Button13
        '
        Me.Button13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button13.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button13.DrawOnGlass = False
        Me.Button13.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button13.ForeColor = System.Drawing.Color.White
        Me.Button13.Image = CType(resources.GetObject("Button13.Image"), System.Drawing.Image)
        Me.Button13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button13.LineColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Button13.LineSize = 1
        Me.Button13.Location = New System.Drawing.Point(651, 8)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(90, 24)
        Me.Button13.TabIndex = 224
        Me.Button13.Text = "Fullscreen"
        Me.Button13.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button6.DrawOnGlass = False
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.LineColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(3, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button6.LineSize = 1
        Me.Button6.Location = New System.Drawing.Point(581, 8)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(29, 24)
        Me.Button6.TabIndex = 223
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button5.DrawOnGlass = False
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.LineColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Button5.LineSize = 1
        Me.Button5.Location = New System.Drawing.Point(616, 8)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(29, 24)
        Me.Button5.TabIndex = 222
        Me.Button5.UseVisualStyleBackColor = False
        '
        'pnl_preview
        '
        Me.pnl_preview.BackColor = System.Drawing.Color.Black
        Me.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_preview.Location = New System.Drawing.Point(111, 42)
        Me.pnl_preview.Name = "pnl_preview"
        Me.pnl_preview.Size = New System.Drawing.Size(528, 297)
        Me.pnl_preview.TabIndex = 2
        '
        'PictureBox41
        '
        Me.PictureBox41.Image = CType(resources.GetObject("PictureBox41.Image"), System.Drawing.Image)
        Me.PictureBox41.Location = New System.Drawing.Point(4, 4)
        Me.PictureBox41.Name = "PictureBox41"
        Me.PictureBox41.Size = New System.Drawing.Size(35, 32)
        Me.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox41.TabIndex = 4
        Me.PictureBox41.TabStop = False
        '
        'Label19
        '
        Me.Label19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(45, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(374, 31)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "Preview"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button10
        '
        Me.Button10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button10.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button10.DrawOnGlass = False
        Me.Button10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button10.ForeColor = System.Drawing.Color.White
        Me.Button10.Image = CType(resources.GetObject("Button10.Image"), System.Drawing.Image)
        Me.Button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button10.LineColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Button10.LineSize = 1
        Me.Button10.Location = New System.Drawing.Point(461, 510)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(115, 34)
        Me.Button10.TabIndex = 210
        Me.Button10.Text = "Quick apply"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button7.DrawOnGlass = False
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Image = Nothing
        Me.Button7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button7.LineSize = 1
        Me.Button7.Location = New System.Drawing.Point(375, 510)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(80, 34)
        Me.Button7.TabIndex = 209
        Me.Button7.Text = "Cancel"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button8.DrawOnGlass = False
        Me.Button8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button8.ForeColor = System.Drawing.Color.White
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button8.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button8.LineSize = 1
        Me.Button8.Location = New System.Drawing.Point(582, 510)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(180, 34)
        Me.Button8.TabIndex = 208
        Me.Button8.Text = "Load into current theme"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'GroupBox12
        '
        Me.GroupBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.GroupBox12.Controls.Add(Me.Button259)
        Me.GroupBox12.Controls.Add(Me.Button9)
        Me.GroupBox12.Controls.Add(Me.Label12)
        Me.GroupBox12.Controls.Add(Me.Button11)
        Me.GroupBox12.Controls.Add(Me.Button12)
        Me.GroupBox12.Controls.Add(Me.ScrSvrEnabled)
        Me.GroupBox12.Controls.Add(Me.checker_img)
        Me.GroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(750, 39)
        Me.GroupBox12.TabIndex = 201
        '
        'Button259
        '
        Me.Button259.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button259.DrawOnGlass = False
        Me.Button259.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button259.ForeColor = System.Drawing.Color.White
        Me.Button259.Image = CType(resources.GetObject("Button259.Image"), System.Drawing.Image)
        Me.Button259.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button259.LineColor = System.Drawing.Color.FromArgb(CType(CType(151, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.Button259.LineSize = 1
        Me.Button259.Location = New System.Drawing.Point(223, 5)
        Me.Button259.Name = "Button259"
        Me.Button259.Size = New System.Drawing.Size(144, 29)
        Me.Button259.TabIndex = 114
        Me.Button259.Text = "Classic .theme file"
        Me.Button259.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button9.DrawOnGlass = False
        Me.Button9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button9.ForeColor = System.Drawing.Color.White
        Me.Button9.Image = CType(resources.GetObject("Button9.Image"), System.Drawing.Image)
        Me.Button9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button9.LineColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.Button9.LineSize = 1
        Me.Button9.Location = New System.Drawing.Point(370, 5)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(126, 29)
        Me.Button9.TabIndex = 112
        Me.Button9.Text = "Current applied"
        Me.Button9.UseVisualStyleBackColor = False
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
        'Button11
        '
        Me.Button11.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button11.DrawOnGlass = False
        Me.Button11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button11.ForeColor = System.Drawing.Color.White
        Me.Button11.Image = CType(resources.GetObject("Button11.Image"), System.Drawing.Image)
        Me.Button11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button11.LineColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.Button11.LineSize = 1
        Me.Button11.Location = New System.Drawing.Point(85, 5)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(135, 29)
        Me.Button11.TabIndex = 110
        Me.Button11.Text = "WinPaletter theme"
        Me.Button11.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button12.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Button12.DrawOnGlass = False
        Me.Button12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button12.ForeColor = System.Drawing.Color.White
        Me.Button12.Image = Nothing
        Me.Button12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button12.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.Button12.LineSize = 1
        Me.Button12.Location = New System.Drawing.Point(499, 5)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(135, 29)
        Me.Button12.TabIndex = 108
        Me.Button12.Text = "Default Windows"
        Me.Button12.UseVisualStyleBackColor = False
        '
        'ScrSvrEnabled
        '
        Me.ScrSvrEnabled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScrSvrEnabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ScrSvrEnabled.Checked = False
        Me.ScrSvrEnabled.DarkLight_Toggler = False
        Me.ScrSvrEnabled.Location = New System.Drawing.Point(705, 9)
        Me.ScrSvrEnabled.Name = "ScrSvrEnabled"
        Me.ScrSvrEnabled.Size = New System.Drawing.Size(40, 20)
        Me.ScrSvrEnabled.TabIndex = 85
        '
        'checker_img
        '
        Me.checker_img.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checker_img.Image = Global.WinPaletter.My.Resources.Resources.checker_disabled
        Me.checker_img.Location = New System.Drawing.Point(664, 4)
        Me.checker_img.Name = "checker_img"
        Me.checker_img.Size = New System.Drawing.Size(35, 31)
        Me.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.checker_img.TabIndex = 83
        Me.checker_img.TabStop = False
        '
        'ScreenSaver_Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(774, 556)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.previewContainer)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.GroupBox12)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ScreenSaver_Editor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Screen Saver"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.previewContainer.ResumeLayout(False)
        CType(Me.PictureBox41, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox12.ResumeLayout(False)
        CType(Me.checker_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox12 As UI.WP.GroupBox
    Friend WithEvents Button9 As UI.WP.Button
    Friend WithEvents Label12 As Label
    Friend WithEvents Button11 As UI.WP.Button
    Friend WithEvents Button12 As UI.WP.Button
    Friend WithEvents ScrSvrEnabled As UI.WP.Toggle
    Friend WithEvents checker_img As PictureBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button10 As UI.WP.Button
    Friend WithEvents Button7 As UI.WP.Button
    Friend WithEvents Button8 As UI.WP.Button
    Friend WithEvents previewContainer As UI.WP.GroupBox
    Friend WithEvents pnl_preview As Panel
    Friend WithEvents PictureBox41 As PictureBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Button4 As UI.WP.Button
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox17 As PictureBox
    Friend WithEvents Trackbar5 As UI.WP.Trackbar
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents TextBox1 As UI.WP.TextBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents CheckBox1 As UI.WP.CheckBox
    Friend WithEvents Button14 As UI.WP.Button
    Friend WithEvents Button13 As UI.WP.Button
    Friend WithEvents Button6 As UI.WP.Button
    Friend WithEvents Button5 As UI.WP.Button
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents GroupBox1 As UI.WP.GroupBox
    Friend WithEvents Button259 As UI.WP.Button
    Friend WithEvents OpenThemeDialog As OpenFileDialog
End Class
