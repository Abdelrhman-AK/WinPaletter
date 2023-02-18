<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BugReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BugReport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.XenonButton5 = New WinPaletter.XenonButton()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.XenonSeparator1 = New WinPaletter.XenonSeparator()
        Me.XenonGroupBox3 = New WinPaletter.XenonGroupBox()
        Me.XenonTreeView1 = New WinPaletter.XenonTreeView()
        Me.XenonButton6 = New WinPaletter.XenonButton()
        Me.XenonButton4 = New WinPaletter.XenonButton()
        Me.XenonButton3 = New WinPaletter.XenonButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.XenonGroupBox1 = New WinPaletter.XenonGroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox3.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox1.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Maroon
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(715, 59)
        Me.Panel1.TabIndex = 110
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
        Me.Label7.Size = New System.Drawing.Size(650, 35)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "There is an exception error. Please Try again or report this in GitHub issues."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Text File (*.txt)|*.txt"
        '
        'XenonButton5
        '
        Me.XenonButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton5.ForeColor = System.Drawing.Color.White
        Me.XenonButton5.Image = Nothing
        Me.XenonButton5.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton5.LineSize = 1
        Me.XenonButton5.Location = New System.Drawing.Point(406, 429)
        Me.XenonButton5.Name = "XenonButton5"
        Me.XenonButton5.Size = New System.Drawing.Size(91, 34)
        Me.XenonButton5.TabIndex = 120
        Me.XenonButton5.Text = "Github Issues"
        Me.XenonButton5.UseVisualStyleBackColor = False
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(503, 429)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(116, 34)
        Me.XenonButton2.TabIndex = 117
        Me.XenonButton2.Text = "Exit Application"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = Nothing
        Me.XenonButton1.LineColor = System.Drawing.Color.Green
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(625, 429)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(78, 34)
        Me.XenonButton1.TabIndex = 116
        Me.XenonButton1.Text = "Continue"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 422)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(691, 1)
        Me.XenonSeparator1.TabIndex = 115
        Me.XenonSeparator1.TabStop = False
        Me.XenonSeparator1.Text = "XenonSeparator1"
        '
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.XenonTreeView1)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton6)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton4)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton3)
        Me.XenonGroupBox3.Controls.Add(Me.Label4)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox4)
        Me.XenonGroupBox3.Location = New System.Drawing.Point(12, 135)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(691, 281)
        Me.XenonGroupBox3.TabIndex = 114
        '
        'XenonTreeView1
        '
        Me.XenonTreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.XenonTreeView1.Indent = 15
        Me.XenonTreeView1.ItemHeight = 20
        Me.XenonTreeView1.Location = New System.Drawing.Point(3, 32)
        Me.XenonTreeView1.Name = "XenonTreeView1"
        Me.XenonTreeView1.Size = New System.Drawing.Size(684, 246)
        Me.XenonTreeView1.TabIndex = 122
        '
        'XenonButton6
        '
        Me.XenonButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton6.ForeColor = System.Drawing.Color.White
        Me.XenonButton6.Image = Nothing
        Me.XenonButton6.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton6.LineSize = 1
        Me.XenonButton6.Location = New System.Drawing.Point(587, 5)
        Me.XenonButton6.Name = "XenonButton6"
        Me.XenonButton6.Size = New System.Drawing.Size(100, 24)
        Me.XenonButton6.TabIndex = 121
        Me.XenonButton6.Text = "Previous Reports"
        Me.XenonButton6.UseVisualStyleBackColor = False
        '
        'XenonButton4
        '
        Me.XenonButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton4.ForeColor = System.Drawing.Color.White
        Me.XenonButton4.Image = Nothing
        Me.XenonButton4.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton4.LineSize = 1
        Me.XenonButton4.Location = New System.Drawing.Point(475, 5)
        Me.XenonButton4.Name = "XenonButton4"
        Me.XenonButton4.Size = New System.Drawing.Size(110, 24)
        Me.XenonButton4.TabIndex = 119
        Me.XenonButton4.Text = "Save Details as ..."
        Me.XenonButton4.UseVisualStyleBackColor = False
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = Nothing
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(393, 5)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(80, 24)
        Me.XenonButton3.TabIndex = 118
        Me.XenonButton3.Text = "Copy Details"
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(33, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(345, 24)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Error Details:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(3, 5)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 1
        Me.PictureBox4.TabStop = False
        '
        'XenonGroupBox1
        '
        Me.XenonGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox1.Controls.Add(Me.Label3)
        Me.XenonGroupBox1.Controls.Add(Me.Label5)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox5)
        Me.XenonGroupBox1.Controls.Add(Me.Label2)
        Me.XenonGroupBox1.Controls.Add(Me.Label1)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox2)
        Me.XenonGroupBox1.Location = New System.Drawing.Point(12, 71)
        Me.XenonGroupBox1.Name = "XenonGroupBox1"
        Me.XenonGroupBox1.Size = New System.Drawing.Size(691, 58)
        Me.XenonGroupBox1.TabIndex = 111
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(159, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(529, 24)
        Me.Label3.TabIndex = 90
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(33, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(124, 24)
        Me.Label5.TabIndex = 89
        Me.Label5.Text = "WinPaletter Version:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(3, 30)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 88
        Me.PictureBox5.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(159, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(529, 24)
        Me.Label2.TabIndex = 87
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 24)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "OS Information:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'BugReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(715, 475)
        Me.Controls.Add(Me.XenonButton5)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton1)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.XenonGroupBox3)
        Me.Controls.Add(Me.XenonGroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(645, 435)
        Me.Name = "BugReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exception Error"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox3.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox1.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents XenonGroupBox1 As XenonGroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents XenonGroupBox3 As XenonGroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents XenonSeparator1 As XenonSeparator
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonButton3 As XenonButton
    Friend WithEvents XenonButton4 As XenonButton
    Friend WithEvents XenonButton5 As XenonButton
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents XenonButton6 As XenonButton
    Friend WithEvents XenonTreeView1 As XenonTreeView
End Class
