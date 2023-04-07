<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Store_ConsolesPreview
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
        Me.XenonTabControl1 = New WinPaletter.XenonTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.XenonCMD1 = New WinPaletter.XenonCMD()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.XenonCMD2 = New WinPaletter.XenonCMD()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.XenonCMD3 = New WinPaletter.XenonCMD()
        Me.XenonButton2 = New WinPaletter.XenonButton()
        Me.XenonTabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
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
        Me.XenonTabControl1.ItemSize = New System.Drawing.Size(30, 170)
        Me.XenonTabControl1.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.XenonTabControl1.Location = New System.Drawing.Point(12, 12)
        Me.XenonTabControl1.Multiline = True
        Me.XenonTabControl1.Name = "XenonTabControl1"
        Me.XenonTabControl1.SelectedIndex = 0
        Me.XenonTabControl1.Size = New System.Drawing.Size(771, 333)
        Me.XenonTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.XenonTabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.XenonCMD1)
        Me.TabPage1.Location = New System.Drawing.Point(174, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(593, 325)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Command Prompt"
        '
        'XenonCMD1
        '
        Me.XenonCMD1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonCMD1.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.XenonCMD1.CMD_PopupBackground = 5
        Me.XenonCMD1.CMD_PopupForeground = 15
        Me.XenonCMD1.CMD_ScreenColorsBackground = 0
        Me.XenonCMD1.CMD_ScreenColorsForeground = 7
        Me.XenonCMD1.CustomTerminal = False
        Me.XenonCMD1.Location = New System.Drawing.Point(6, 6)
        Me.XenonCMD1.Name = "XenonCMD1"
        Me.XenonCMD1.PowerShell = False
        Me.XenonCMD1.Raster = True
        Me.XenonCMD1.RasterSize = WinPaletter.XenonCMD.Raster_Sizes._8x12
        Me.XenonCMD1.Size = New System.Drawing.Size(581, 313)
        Me.XenonCMD1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.XenonCMD2)
        Me.TabPage2.Location = New System.Drawing.Point(174, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(593, 325)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "PowerShell x86"
        '
        'XenonCMD2
        '
        Me.XenonCMD2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonCMD2.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.XenonCMD2.CMD_PopupBackground = 5
        Me.XenonCMD2.CMD_PopupForeground = 15
        Me.XenonCMD2.CMD_ScreenColorsBackground = 0
        Me.XenonCMD2.CMD_ScreenColorsForeground = 7
        Me.XenonCMD2.CustomTerminal = False
        Me.XenonCMD2.Location = New System.Drawing.Point(6, 6)
        Me.XenonCMD2.Name = "XenonCMD2"
        Me.XenonCMD2.PowerShell = False
        Me.XenonCMD2.Raster = True
        Me.XenonCMD2.RasterSize = WinPaletter.XenonCMD.Raster_Sizes._8x12
        Me.XenonCMD2.Size = New System.Drawing.Size(581, 311)
        Me.XenonCMD2.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.XenonCMD3)
        Me.TabPage3.Location = New System.Drawing.Point(174, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(593, 325)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "PowerShell x64"
        '
        'XenonCMD3
        '
        Me.XenonCMD3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XenonCMD3.CMD_ColorTable00 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable01 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable02 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable03 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable04 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable05 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable06 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable07 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable08 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable09 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable10 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable11 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable12 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable13 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable14 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_ColorTable15 = System.Drawing.Color.Empty
        Me.XenonCMD3.CMD_PopupBackground = 5
        Me.XenonCMD3.CMD_PopupForeground = 15
        Me.XenonCMD3.CMD_ScreenColorsBackground = 0
        Me.XenonCMD3.CMD_ScreenColorsForeground = 7
        Me.XenonCMD3.CustomTerminal = False
        Me.XenonCMD3.Location = New System.Drawing.Point(6, 6)
        Me.XenonCMD3.Name = "XenonCMD3"
        Me.XenonCMD3.PowerShell = False
        Me.XenonCMD3.Raster = True
        Me.XenonCMD3.RasterSize = WinPaletter.XenonCMD.Raster_Sizes._8x12
        Me.XenonCMD3.Size = New System.Drawing.Size(581, 311)
        Me.XenonCMD3.TabIndex = 1
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
        Me.XenonButton2.Location = New System.Drawing.Point(703, 351)
        Me.XenonButton2.Name = "XenonButton2"
        Me.XenonButton2.Size = New System.Drawing.Size(80, 34)
        Me.XenonButton2.TabIndex = 107
        Me.XenonButton2.Text = "Cancel"
        Me.XenonButton2.UseVisualStyleBackColor = False
        '
        'Store_ConsolesPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(795, 397)
        Me.Controls.Add(Me.XenonButton2)
        Me.Controls.Add(Me.XenonTabControl1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Store_ConsolesPreview"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consoles Preview"
        Me.XenonTabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents XenonTabControl1 As XenonTabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents XenonCMD1 As XenonCMD
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents XenonCMD2 As XenonCMD
    Friend WithEvents XenonCMD3 As XenonCMD
    Friend WithEvents XenonButton2 As XenonButton
End Class
