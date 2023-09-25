<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TerminalInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TerminalInfo))
        Me.TerTabTitle = New WinPaletter.UI.WP.TextBox()
        Me.PictureBox47 = New System.Windows.Forms.PictureBox()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.TerName = New WinPaletter.UI.WP.TextBox()
        Me.PictureBox38 = New System.Windows.Forms.PictureBox()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.PictureBox28 = New System.Windows.Forms.PictureBox()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Button11 = New WinPaletter.UI.WP.Button()
        Me.TerTabIcon = New WinPaletter.UI.WP.TextBox()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.PictureBox36 = New System.Windows.Forms.PictureBox()
        Me.TerTabColor = New WinPaletter.UI.Controllers.ColorItem()
        Me.PictureBox40 = New System.Windows.Forms.PictureBox()
        Me.TerAcrylic = New UI.WP.CheckBox()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.Separator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        CType(Me.PictureBox47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox40, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TerTabTitle
        '
        Me.TerTabTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerTabTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TerTabTitle.DrawOnGlass = False
        Me.TerTabTitle.ForeColor = System.Drawing.Color.White
        Me.TerTabTitle.Location = New System.Drawing.Point(120, 42)
        Me.TerTabTitle.MaxLength = 32767
        Me.TerTabTitle.Multiline = False
        Me.TerTabTitle.Name = "TerTabTitle"
        Me.TerTabTitle.ReadOnly = False
        Me.TerTabTitle.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TerTabTitle.SelectedText = ""
        Me.TerTabTitle.SelectionLength = 0
        Me.TerTabTitle.SelectionStart = 0
        Me.TerTabTitle.Size = New System.Drawing.Size(328, 24)
        Me.TerTabTitle.TabIndex = 195
        Me.TerTabTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TerTabTitle.UseSystemPasswordChar = False
        Me.TerTabTitle.WordWrap = True
        '
        'PictureBox47
        '
        Me.PictureBox47.Image = CType(resources.GetObject("PictureBox47.Image"), System.Drawing.Image)
        Me.PictureBox47.Location = New System.Drawing.Point(12, 42)
        Me.PictureBox47.Name = "PictureBox47"
        Me.PictureBox47.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox47.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox47.TabIndex = 194
        Me.PictureBox47.TabStop = False
        '
        'Label174
        '
        Me.Label174.BackColor = System.Drawing.Color.Transparent
        Me.Label174.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label174.Location = New System.Drawing.Point(42, 42)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(54, 24)
        Me.Label174.TabIndex = 193
        Me.Label174.Text = "Tab title:"
        Me.Label174.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TerName
        '
        Me.TerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TerName.DrawOnGlass = False
        Me.TerName.ForeColor = System.Drawing.Color.White
        Me.TerName.Location = New System.Drawing.Point(120, 12)
        Me.TerName.MaxLength = 32767
        Me.TerName.Multiline = False
        Me.TerName.Name = "TerName"
        Me.TerName.ReadOnly = False
        Me.TerName.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TerName.SelectedText = ""
        Me.TerName.SelectionLength = 0
        Me.TerName.SelectionStart = 0
        Me.TerName.Size = New System.Drawing.Size(328, 24)
        Me.TerName.TabIndex = 192
        Me.TerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TerName.UseSystemPasswordChar = False
        Me.TerName.WordWrap = True
        '
        'PictureBox38
        '
        Me.PictureBox38.Image = CType(resources.GetObject("PictureBox38.Image"), System.Drawing.Image)
        Me.PictureBox38.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox38.Name = "PictureBox38"
        Me.PictureBox38.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox38.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox38.TabIndex = 191
        Me.PictureBox38.TabStop = False
        '
        'Label164
        '
        Me.Label164.BackColor = System.Drawing.Color.Transparent
        Me.Label164.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label164.Location = New System.Drawing.Point(42, 12)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(72, 24)
        Me.Label164.TabIndex = 190
        Me.Label164.Text = "Name:"
        Me.Label164.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox28
        '
        Me.PictureBox28.Image = CType(resources.GetObject("PictureBox28.Image"), System.Drawing.Image)
        Me.PictureBox28.Location = New System.Drawing.Point(12, 72)
        Me.PictureBox28.Name = "PictureBox28"
        Me.PictureBox28.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox28.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox28.TabIndex = 187
        Me.PictureBox28.TabStop = False
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.Transparent
        Me.Label153.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label153.Location = New System.Drawing.Point(42, 72)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(62, 24)
        Me.Label153.TabIndex = 186
        Me.Label153.Text = "Tab icon:"
        Me.Label153.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button11
        '
        Me.Button11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button11.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button11.DrawOnGlass = False
        Me.Button11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button11.ForeColor = System.Drawing.Color.White
        Me.Button11.Image = CType(resources.GetObject("Button11.Image"), System.Drawing.Image)
        Me.Button11.LineColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.Button11.Location = New System.Drawing.Point(416, 72)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(32, 24)
        Me.Button11.TabIndex = 189
        Me.Button11.UseVisualStyleBackColor = False
        '
        'TerTabIcon
        '
        Me.TerTabIcon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TerTabIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TerTabIcon.DrawOnGlass = False
        Me.TerTabIcon.ForeColor = System.Drawing.Color.White
        Me.TerTabIcon.Location = New System.Drawing.Point(120, 72)
        Me.TerTabIcon.MaxLength = 32767
        Me.TerTabIcon.Multiline = False
        Me.TerTabIcon.Name = "TerTabIcon"
        Me.TerTabIcon.ReadOnly = False
        Me.TerTabIcon.Scrollbars = System.Windows.Forms.ScrollBars.None
        Me.TerTabIcon.SelectedText = ""
        Me.TerTabIcon.SelectionLength = 0
        Me.TerTabIcon.SelectionStart = 0
        Me.TerTabIcon.Size = New System.Drawing.Size(290, 24)
        Me.TerTabIcon.TabIndex = 188
        Me.TerTabIcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TerTabIcon.UseSystemPasswordChar = False
        Me.TerTabIcon.WordWrap = True
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.Transparent
        Me.Label166.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label166.Location = New System.Drawing.Point(42, 102)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(75, 24)
        Me.Label166.TabIndex = 197
        Me.Label166.Text = "Tab color:"
        Me.Label166.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox36
        '
        Me.PictureBox36.Image = CType(resources.GetObject("PictureBox36.Image"), System.Drawing.Image)
        Me.PictureBox36.Location = New System.Drawing.Point(12, 102)
        Me.PictureBox36.Name = "PictureBox36"
        Me.PictureBox36.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox36.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox36.TabIndex = 198
        Me.PictureBox36.TabStop = False
        '
        'TerTabColor
        '
        Me.TerTabColor.AllowDrop = True
        Me.TerTabColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTabColor.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.TerTabColor.DontShowInfo = False
        Me.TerTabColor.Location = New System.Drawing.Point(120, 101)
        Me.TerTabColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TerTabColor.Name = "TerTabColor"
        Me.TerTabColor.Size = New System.Drawing.Size(132, 25)
        Me.TerTabColor.TabIndex = 196
        '
        'PictureBox40
        '
        Me.PictureBox40.Image = CType(resources.GetObject("PictureBox40.Image"), System.Drawing.Image)
        Me.PictureBox40.Location = New System.Drawing.Point(12, 132)
        Me.PictureBox40.Name = "PictureBox40"
        Me.PictureBox40.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox40.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox40.TabIndex = 201
        Me.PictureBox40.TabStop = False
        '
        'TerAcrylic
        '
        Me.TerAcrylic.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TerAcrylic.Checked = False
        Me.TerAcrylic.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TerAcrylic.ForeColor = System.Drawing.Color.White
        Me.TerAcrylic.Location = New System.Drawing.Point(45, 131)
        Me.TerAcrylic.Name = "TerAcrylic"
        Me.TerAcrylic.Size = New System.Drawing.Size(184, 24)
        Me.TerAcrylic.TabIndex = 202
        Me.TerAcrylic.Text = "Acrylic titlebar (All profiles)"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button2.DrawOnGlass = False
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(283, 212)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 34)
        Me.Button2.TabIndex = 206
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button1.DrawOnGlass = False
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(369, 212)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 34)
        Me.Button1.TabIndex = 205
        Me.Button1.Text = "Load"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Separator1
        '
        Me.Separator1.AlternativeLook = False
        Me.Separator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Separator1.Location = New System.Drawing.Point(12, 164)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(436, 1)
        Me.Separator1.TabIndex = 207
        Me.Separator1.TabStop = False
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = Global.WinPaletter.My.Resources.Resources.notify_info
        Me.AlertBox1.Location = New System.Drawing.Point(12, 174)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(437, 32)
        Me.AlertBox1.TabIndex = 209
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "Tab Icon can be a file path or emoji/symbol from font ""Segoe Fluent Icons"""
        '
        'TerminalInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(461, 258)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.Separator1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox40)
        Me.Controls.Add(Me.TerAcrylic)
        Me.Controls.Add(Me.Label166)
        Me.Controls.Add(Me.PictureBox36)
        Me.Controls.Add(Me.TerTabColor)
        Me.Controls.Add(Me.TerTabTitle)
        Me.Controls.Add(Me.PictureBox47)
        Me.Controls.Add(Me.Label174)
        Me.Controls.Add(Me.TerName)
        Me.Controls.Add(Me.PictureBox38)
        Me.Controls.Add(Me.Label164)
        Me.Controls.Add(Me.PictureBox28)
        Me.Controls.Add(Me.Label153)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.TerTabIcon)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TerminalInfo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Terminal info"
        CType(Me.PictureBox47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox40, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TerTabTitle As UI.WP.TextBox
    Friend WithEvents PictureBox47 As PictureBox
    Friend WithEvents Label174 As Label
    Friend WithEvents TerName As UI.WP.TextBox
    Friend WithEvents PictureBox38 As PictureBox
    Friend WithEvents Label164 As Label
    Friend WithEvents PictureBox28 As PictureBox
    Friend WithEvents Label153 As Label
    Friend WithEvents Button11 As UI.WP.Button
    Friend WithEvents TerTabIcon As UI.WP.TextBox
    Friend WithEvents Label166 As Label
    Friend WithEvents PictureBox36 As PictureBox
    Friend WithEvents TerTabColor As UI.Controllers.ColorItem
    Friend WithEvents PictureBox40 As PictureBox
    Friend WithEvents TerAcrylic As UI.WP.CheckBox
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Separator1 As UI.WP.SeparatorH
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
End Class
