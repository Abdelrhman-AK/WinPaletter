<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WindowsTerminalDecide
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WindowsTerminalDecide))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New WinPaletter.UI.WP.Button()
        Me.Button1 = New WinPaletter.UI.WP.Button()
        Me.RadioImage2 = New WinPaletter.UI.WP.RadioImage()
        Me.RadioImage1 = New WinPaletter.UI.WP.RadioImage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AlertBox1 = New WinPaletter.UI.WP.AlertBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(53, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(344, 89)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "WinPaletter saves the Windows Terminal Stable and Preview in separete areas in th" &
    "e theme file." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "From which one do you want to be imported into current open edi" &
    "tor?"
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.Transparent
        Me.Label144.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label144.Location = New System.Drawing.Point(131, 197)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(64, 20)
        Me.Label144.TabIndex = 88
        Me.Label144.Text = "Stable"
        Me.Label144.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(213, 197)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 20)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "Preview"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.Button2.LineSize = 1
        Me.Button2.Location = New System.Drawing.Point(231, 305)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 34)
        Me.Button2.TabIndex = 108
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Nothing
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.LineColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.LineSize = 1
        Me.Button1.Location = New System.Drawing.Point(317, 305)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 34)
        Me.Button1.TabIndex = 107
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'RadioImage2
        '
        Me.RadioImage2.Checked = False
        Me.RadioImage2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage2.ForeColor = System.Drawing.Color.White
        Me.RadioImage2.Image = CType(resources.GetObject("RadioImage2.Image"), System.Drawing.Image)
        Me.RadioImage2.Location = New System.Drawing.Point(213, 130)
        Me.RadioImage2.Name = "RadioImage2"
        Me.RadioImage2.ShowText = False
        Me.RadioImage2.Size = New System.Drawing.Size(64, 64)
        Me.RadioImage2.TabIndex = 87
        '
        'RadioImage1
        '
        Me.RadioImage1.Checked = False
        Me.RadioImage1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioImage1.ForeColor = System.Drawing.Color.White
        Me.RadioImage1.Image = CType(resources.GetObject("RadioImage1.Image"), System.Drawing.Image)
        Me.RadioImage1.Location = New System.Drawing.Point(131, 130)
        Me.RadioImage1.Name = "RadioImage1"
        Me.RadioImage1.ShowText = False
        Me.RadioImage1.Size = New System.Drawing.Size(64, 64)
        Me.RadioImage1.TabIndex = 86
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(409, 110)
        Me.Panel1.TabIndex = 109
        '
        'AlertBox1
        '
        Me.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive
        Me.AlertBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.AlertBox1.CenterText = False
        Me.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.AlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.AlertBox1.Image = Global.WinPaletter.My.Resources.notify_warning
        Me.AlertBox1.Location = New System.Drawing.Point(12, 248)
        Me.AlertBox1.Name = "AlertBox1"
        Me.AlertBox1.Size = New System.Drawing.Size(385, 49)
        Me.AlertBox1.TabIndex = 199
        Me.AlertBox1.TabStop = False
        Me.AlertBox1.Text = "This will override the already existing terminal settings (but won't delete them " &
    "even if they are not visible)"
        '
        'WindowsTerminalDecide
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(409, 351)
        Me.Controls.Add(Me.AlertBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label144)
        Me.Controls.Add(Me.RadioImage2)
        Me.Controls.Add(Me.RadioImage1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WindowsTerminalDecide"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Decide"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents RadioImage1 As UI.WP.RadioImage
    Friend WithEvents RadioImage2 As UI.WP.RadioImage
    Friend WithEvents Label144 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button2 As UI.WP.Button
    Friend WithEvents Button1 As UI.WP.Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents AlertBox1 As UI.WP.AlertBox
End Class
