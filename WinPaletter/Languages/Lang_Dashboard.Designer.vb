<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Lang_Dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lang_Dashboard))
        Me.XenonGroupBox1 = New WinPaletter.UI.WP.GroupBox()
        Me.XenonButton1 = New WinPaletter.UI.WP.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.XenonGroupBox2 = New WinPaletter.UI.WP.GroupBox()
        Me.XenonButton2 = New WinPaletter.UI.WP.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.XenonButton7 = New WinPaletter.UI.WP.Button()
        Me.XenonGroupBox3 = New WinPaletter.UI.WP.GroupBox()
        Me.XenonButton3 = New WinPaletter.UI.WP.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.XenonSeparator1 = New WinPaletter.UI.WP.SeparatorH()
        Me.XenonAlertBox1 = New WinPaletter.UI.WP.AlertBox()
        Me.XenonAlertBox2 = New WinPaletter.UI.WP.AlertBox()
        Me.XenonGroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XenonGroupBox3.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XenonGroupBox1
        '
        Me.XenonGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox1.Controls.Add(Me.XenonButton1)
        Me.XenonGroupBox1.Controls.Add(Me.Label2)
        Me.XenonGroupBox1.Controls.Add(Me.Label1)
        Me.XenonGroupBox1.Controls.Add(Me.PictureBox1)
        Me.XenonGroupBox1.Location = New System.Drawing.Point(12, 89)
        Me.XenonGroupBox1.Name = "XenonGroupBox1"
        Me.XenonGroupBox1.Size = New System.Drawing.Size(610, 64)
        Me.XenonGroupBox1.TabIndex = 0
        '
        'XenonButton1
        '
        Me.XenonButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton1.DrawOnGlass = False
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = Nothing
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(521, 20)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(75, 24)
        Me.XenonButton1.TabIndex = 4
        Me.XenonButton1.Text = "Go"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(75, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(440, 35)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "This will help you serialize JSON language file into simple tree nodes, making it" &
    " easier to modify language files (If you can't use a text editor to modify JSON " &
    "files)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(65, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(413, 19)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Create\Modify language files"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(58, 58)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'XenonGroupBox2
        '
        Me.XenonGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox2.Controls.Add(Me.XenonButton2)
        Me.XenonGroupBox2.Controls.Add(Me.Label3)
        Me.XenonGroupBox2.Controls.Add(Me.Label4)
        Me.XenonGroupBox2.Controls.Add(Me.PictureBox2)
        Me.XenonGroupBox2.Location = New System.Drawing.Point(12, 159)
        Me.XenonGroupBox2.Name = "XenonGroupBox2"
        Me.XenonGroupBox2.Size = New System.Drawing.Size(610, 64)
        Me.XenonGroupBox2.TabIndex = 1
        '
        'XenonButton2
        '
        Me.XenonButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton2.DrawOnGlass = False
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(521, 20)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(75, 24)
        Me.XenonButton2.TabIndex = 4
        Me.XenonButton2.Text = "Go"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(75, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(440, 35)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "It will be helpful if a new version of WinPaletter has been released and you want" &
    " to update the JSON file with the new text entries included in the new version"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(65, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(413, 19)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Update JSON language file"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(58, 58)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'XenonButton7
        '
        Me.XenonButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton7.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton7.DrawOnGlass = False
        Me.XenonButton7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton7.ForeColor = System.Drawing.Color.White
        Me.XenonButton7.Image = Nothing
        Me.XenonButton7.LineColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.XenonButton7.LineSize = 1
        Me.XenonButton7.Location = New System.Drawing.Point(542, 305)
        Me.XenonButton7.Name = "XenonButton7"
        Me.XenonButton7.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton7.TabIndex = 202
        Me.XenonButton7.Text = "Cancel"
        Me.XenonButton7.UseVisualStyleBackColor = False
        '
        'XenonGroupBox3
        '
        Me.XenonGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonGroupBox3.Controls.Add(Me.XenonAlertBox1)
        Me.XenonGroupBox3.Controls.Add(Me.XenonButton3)
        Me.XenonGroupBox3.Controls.Add(Me.Label5)
        Me.XenonGroupBox3.Controls.Add(Me.Label6)
        Me.XenonGroupBox3.Controls.Add(Me.PictureBox3)
        Me.XenonGroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.XenonGroupBox3.Name = "XenonGroupBox3"
        Me.XenonGroupBox3.Size = New System.Drawing.Size(610, 64)
        Me.XenonGroupBox3.TabIndex = 203
        '
        'XenonButton3
        '
        Me.XenonButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.XenonButton3.DrawOnGlass = False
        Me.XenonButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton3.ForeColor = System.Drawing.Color.White
        Me.XenonButton3.Image = Nothing
        Me.XenonButton3.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton3.LineSize = 1
        Me.XenonButton3.Location = New System.Drawing.Point(521, 20)
        Me.XenonButton3.Name = "XenonButton3"
        Me.XenonButton3.Size = New System.Drawing.Size(75, 24)
        Me.XenonButton3.TabIndex = 4
        Me.XenonButton3.Text = "Go"
        Me.XenonButton3.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(75, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(440, 35)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "This will help you create, modify and update languages JSON files by showing mini" &
    "-forms that you can edit so that you can see all text items in real time"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(65, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(230, 19)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "GUI language editor (experimental)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(58, 58)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 1
        Me.PictureBox3.TabStop = False
        '
        'XenonSeparator1
        '
        Me.XenonSeparator1.AlternativeLook = False
        Me.XenonSeparator1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonSeparator1.Location = New System.Drawing.Point(12, 82)
        Me.XenonSeparator1.Name = "XenonSeparator1"
        Me.XenonSeparator1.Size = New System.Drawing.Size(610, 1)
        Me.XenonSeparator1.TabIndex = 204
        Me.XenonSeparator1.TabStop = False
        '
        'XenonAlertBox1
        '
        Me.XenonAlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Success
        Me.XenonAlertBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.XenonAlertBox1.CenterText = True
        Me.XenonAlertBox1.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox1.Image = Nothing
        Me.XenonAlertBox1.Location = New System.Drawing.Point(302, 5)
        Me.XenonAlertBox1.Name = "XenonAlertBox1"
        Me.XenonAlertBox1.Size = New System.Drawing.Size(46, 19)
        Me.XenonAlertBox1.TabIndex = 205
        Me.XenonAlertBox1.TabStop = False
        Me.XenonAlertBox1.Text = "NEW"
        '
        'XenonAlertBox2
        '
        Me.XenonAlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning
        Me.XenonAlertBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonAlertBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.XenonAlertBox2.CenterText = False
        Me.XenonAlertBox2.CustomColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonAlertBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonAlertBox2.Image = Nothing
        Me.XenonAlertBox2.Location = New System.Drawing.Point(12, 229)
        Me.XenonAlertBox2.Name = "XenonAlertBox2"
        Me.XenonAlertBox2.Size = New System.Drawing.Size(610, 40)
        Me.XenonAlertBox2.TabIndex = 206
        Me.XenonAlertBox2.TabStop = False
        Me.XenonAlertBox2.Text = "The last two features might be removed in the future if GUI language editor perfo" &
    "rmed better after its development"
        '
        'Lang_Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(634, 351)
        Me.Controls.Add(Me.XenonAlertBox2)
        Me.Controls.Add(Me.XenonSeparator1)
        Me.Controls.Add(Me.XenonGroupBox3)
        Me.Controls.Add(Me.XenonButton7)
        Me.Controls.Add(Me.XenonGroupBox2)
        Me.Controls.Add(Me.XenonGroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Lang_Dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Language dashboard"
        Me.XenonGroupBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox2.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XenonGroupBox3.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents XenonGroupBox1 As UI.WP.GroupBox
    Friend WithEvents XenonButton1 As UI.WP.Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents XenonGroupBox2 As UI.WP.GroupBox
    Friend WithEvents XenonButton2 As UI.WP.Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents XenonButton7 As UI.WP.Button
    Friend WithEvents XenonGroupBox3 As UI.WP.GroupBox
    Friend WithEvents XenonAlertBox1 As UI.WP.AlertBox
    Friend WithEvents XenonButton3 As UI.WP.Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents XenonSeparator1 As UI.WP.SeparatorH
    Friend WithEvents XenonAlertBox2 As UI.WP.AlertBox
End Class
