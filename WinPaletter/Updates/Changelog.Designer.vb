<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Changelog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Changelog))
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.PictureBox18 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.XenonTextBox1 = New WinPaletter.XenonTextBox()
        Me.XenonCheckBox1 = New WinPaletter.XenonCheckBox()
        CType(Me.PictureBox18, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 525)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(884, 12)
        Me.ProgressBar1.TabIndex = 1
        Me.ProgressBar1.Visible = False
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeView1.FullRowSelect = True
        Me.TreeView1.ItemHeight = 28
        Me.TreeView1.Location = New System.Drawing.Point(12, 12)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.ShowLines = False
        Me.TreeView1.Size = New System.Drawing.Size(860, 464)
        Me.TreeView1.TabIndex = 2
        '
        'PictureBox18
        '
        Me.PictureBox18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox18.Image = CType(resources.GetObject("PictureBox18.Image"), System.Drawing.Image)
        Me.PictureBox18.Location = New System.Drawing.Point(12, 492)
        Me.PictureBox18.Name = "PictureBox18"
        Me.PictureBox18.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox18.TabIndex = 14
        Me.PictureBox18.TabStop = False
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(45, 493)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 23)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Search for a version:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'XenonTextBox1
        '
        Me.XenonTextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.XenonTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox1.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox1.Hint = Nothing
        Me.XenonTextBox1.Location = New System.Drawing.Point(179, 492)
        Me.XenonTextBox1.MaxLength = 32767
        Me.XenonTextBox1.Multiline = False
        Me.XenonTextBox1.Name = "XenonTextBox1"
        Me.XenonTextBox1.ReadOnly = False
        Me.XenonTextBox1.Size = New System.Drawing.Size(107, 24)
        Me.XenonTextBox1.TabIndex = 16
        Me.XenonTextBox1.Text = "1.0.0.0"
        Me.XenonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox1.UseSystemPasswordChar = False
        '
        'XenonCheckBox1
        '
        Me.XenonCheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.XenonCheckBox1.Checked = False
        Me.XenonCheckBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonCheckBox1.ForeColor = System.Drawing.Color.White
        Me.XenonCheckBox1.Location = New System.Drawing.Point(729, 492)
        Me.XenonCheckBox1.Name = "XenonCheckBox1"
        Me.XenonCheckBox1.Size = New System.Drawing.Size(143, 23)
        Me.XenonCheckBox1.TabIndex = 3
        Me.XenonCheckBox1.Text = "Include Beta Channel"
        '
        'Changelog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(884, 537)
        Me.Controls.Add(Me.XenonTextBox1)
        Me.Controls.Add(Me.PictureBox18)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.XenonCheckBox1)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(466, 400)
        Me.Name = "Changelog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Changelog"
        Me.TopMost = True
        CType(Me.PictureBox18, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents XenonCheckBox1 As XenonCheckBox
    Friend WithEvents PictureBox18 As PictureBox
    Friend WithEvents Label9 As Label
    Friend WithEvents XenonTextBox1 As XenonTextBox
End Class
