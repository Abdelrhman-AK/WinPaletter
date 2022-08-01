<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.CursorControl1 = New WinPaletter.CursorControl()
        Me.XenonTextBox1 = New WinPaletter.XenonTextBox()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonButton1 = New WinPaletter.XenonButton()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'CursorControl1
        '
        Me.CursorControl1.Location = New System.Drawing.Point(224, 46)
        Me.CursorControl1.Name = "CursorControl1"
        Me.CursorControl1.Prop_BackColor1 = System.Drawing.Color.White
        Me.CursorControl1.Prop_BackColor2 = System.Drawing.Color.White
        Me.CursorControl1.Prop_BackColorGradient = False
        Me.CursorControl1.Prop_BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.CursorControl1.Prop_Cursor = WinPaletter.Paths.CursorType.AppLoading
        Me.CursorControl1.Prop_LineColor1 = System.Drawing.Color.RoyalBlue
        Me.CursorControl1.Prop_LineColor2 = System.Drawing.Color.Lime
        Me.CursorControl1.Prop_LineColorGradient = True
        Me.CursorControl1.Prop_LineColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.CursorControl1.Prop_LineThickness = 1.0!
        Me.CursorControl1.Prop_LoadingCircleBack1 = System.Drawing.Color.Maroon
        Me.CursorControl1.Prop_LoadingCircleBack2 = System.Drawing.Color.Maroon
        Me.CursorControl1.Prop_LoadingCircleBackGradient = True
        Me.CursorControl1.Prop_LoadingCircleBackGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.CursorControl1.Prop_LoadingCircleHot1 = System.Drawing.Color.Red
        Me.CursorControl1.Prop_LoadingCircleHot2 = System.Drawing.Color.Red
        Me.CursorControl1.Prop_LoadingCircleHotGradient = False
        Me.CursorControl1.Prop_LoadingCircleHotGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.CursorControl1.Prop_Scale = 1.2!
        Me.CursorControl1.Size = New System.Drawing.Size(169, 151)
        Me.CursorControl1.TabIndex = 5
        Me.CursorControl1.Text = "CursorControl1"
        '
        'XenonTextBox1
        '
        Me.XenonTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.XenonTextBox1.ForeColor = System.Drawing.Color.White
        Me.XenonTextBox1.Hint = Nothing
        Me.XenonTextBox1.LineColor = System.Drawing.Color.DodgerBlue
        Me.XenonTextBox1.Location = New System.Drawing.Point(12, 128)
        Me.XenonTextBox1.MaxLength = 32767
        Me.XenonTextBox1.Multiline = False
        Me.XenonTextBox1.Name = "XenonTextBox1"
        Me.XenonTextBox1.ReadOnly = False
        Me.XenonTextBox1.Size = New System.Drawing.Size(100, 24)
        Me.XenonTextBox1.TabIndex = 4
        Me.XenonTextBox1.Text = "XenonTextBox1"
        Me.XenonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.XenonTextBox1.UseSystemPasswordChar = False
        '
        'XenonButton2
        '
        Me.XenonButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton2.ForeColor = System.Drawing.Color.White
        Me.XenonButton2.Image = Nothing
        Me.XenonButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton2.LineSize = 1
        Me.XenonButton2.Location = New System.Drawing.Point(12, 70)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(100, 52)
        Me.XenonButton2.TabIndex = 3
        Me.XenonButton2.Text = "XenonButton2"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'XenonButton1
        '
        Me.XenonButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.XenonButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.XenonButton1.ForeColor = System.Drawing.Color.White
        Me.XenonButton1.Image = Nothing
        Me.XenonButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonButton1.LineSize = 1
        Me.XenonButton1.Location = New System.Drawing.Point(12, 12)
        Me.XenonButton1.Name = "XenonButton1"
        Me.XenonButton1.Size = New System.Drawing.Size(100, 52)
        Me.XenonButton1.TabIndex = 1
        Me.XenonButton1.Text = "XenonButton1"
        Me.XenonButton1.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(471, 357)
        Me.Controls.Add(Me.CursorControl1)
        Me.Controls.Add(Me.XenonTextBox1)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonButton1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XenonButton1 As XenonButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents XenonButton2 As XenonButton
    Friend WithEvents XenonTextBox1 As XenonTextBox
    Friend WithEvents CursorControl1 As CursorControl
End Class
