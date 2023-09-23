Imports System.Reflection
Imports System.Threading
Imports Newtonsoft.Json.Linq
Public Class Lang_JSON_GUI

#Region "Variables and events"
    Public Event ControlSelection(sender As Object, e As EventArgs)
    Private _SelectedItem As Control
    Private FormsList As New List(Of Form)

    Private LangFile As String
    Private Lang As New Localizer

    Private EditingTag As Boolean = True
    Private AllowEditing As Boolean = False
    Private _Form As Form
    Private ITypes As IEnumerable(Of Type) = Assembly.GetCallingAssembly().GetTypes().Where(Function(t) GetType(Form).IsAssignableFrom(t))

    Private Th As Thread

    Private SearchText As String
#End Region

#Region "Helpers subs/functions"
    Sub OpenFile()
        Lang = New Localizer
        Lang.LoadLanguageFromJSON(LangFile)

        Label9.Text = LangFile

        XenonTextBox3.Text = Lang.Lang
        XenonTextBox4.Text = Lang.LangCode
        XenonTextBox5.Text = Lang.Name
        XenonTextBox6.Text = Lang.TranslationVersion
        XenonTextBox7.Text = Lang.AppVer
        XenonRadioButton2.Checked = Lang.RightToLeft

        LoadGlobalStrings()

        LoadAllMiniFormsIntoList()
    End Sub

    Sub LoadGlobalStrings()
        data.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        data.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        data.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        data.Columns(0).DefaultCellStyle.Font = My.Application.ConsoleFontMedium
        data.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        data.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True

        Dim row_index As Integer = 0
        Dim rows As New List(Of DataGridViewRow)()
        rows.Clear()

        Dim JObject As JObject = JToken.Parse(IO.File.ReadAllText(LangFile))("Global Strings")


        For Each [property] As PropertyInfo In My.Lang.[GetType].GetProperties

            If Not String.IsNullOrWhiteSpace([property].GetValue(My.Lang)) _
                 And Not [property].Name.ToLower = "Name".ToLower _
                 And Not [property].Name.ToLower = "TranslationVersion".ToLower _
                 And Not [property].Name.ToLower = "Lang".ToLower _
                 And Not [property].Name.ToLower = "LangCode".ToLower _
                 And Not [property].Name.ToLower = "AppVer".ToLower _
                 And Not [property].Name.ToLower = "RightToLeft".ToLower Then

                Dim row As New DataGridViewRow()
                row.CreateCells(data)

                row.Cells(0).Value = [property].Name.ToLower
                row.Cells(0).ReadOnly = True

                row.Cells(2).Value = [property].GetValue(My.Lang).ToString
                row.Cells(2).ReadOnly = True


                If JObject([property].Name.ToLower) IsNot Nothing Then
                    row.Cells(1).Value = JObject([property].Name.ToLower).ToString
                    row.Cells(1).ReadOnly = False

                    If row.Cells(2).Value.ToString.ToLower.Trim = row.Cells(1).Value.ToString.ToLower.Trim Then
                        row.Cells(1).Style.BackColor = My.Style.Colors.NotTranslatedColor
                    End If

                Else
                    row.Cells(1).Style.BackColor = My.Style.Colors.NotTranslatedColor
                    row.Cells(1).Value = ""
                    row.Cells(1).ReadOnly = False
                End If

                rows.Add(row)
                row_index += 1
            End If
        Next

        data.Rows.Clear()
        data.Rows.AddRange(rows.ToArray)
    End Sub

    Sub LoadAllMiniFormsIntoList()
        ProgressBar2.Value = 0
        ProgressBar2.Maximum = ITypes.Count * ProgressBar2.Step * 2
        ProgressBar2.Visible = True

        ProgressBar1.Value = 0
        ProgressBar1.Maximum = ITypes.Count * ProgressBar2.Step * 2
        ProgressBar1.Visible = True

        XenonComboBox1.Visible = False
        XenonButton1.Visible = False

        FormsList.Clear()
        XenonComboBox1.Items.Clear()

        If Th IsNot Nothing AndAlso Th.IsAlive Then Th.Abort()
        Th = New Thread(AddressOf LoadAllMiniFormsIntoList_Thread) With {.Priority = ThreadPriority.Highest, .IsBackground = True}
        Th.Start()
    End Sub

    Sub LoadAllMiniFormsIntoList_Thread()
        Dim i As Integer = 0
        For Each f In ITypes

            Using ins As Form = DirectCast(Activator.CreateInstance(f), Form), ins_nonmodified As Form = DirectCast(Activator.CreateInstance(f), Form)
                If ins.Controls.Count > 0 Then
                    ins.LoadLanguage(Lang)

                    ProgressBar1.PerformStepMethod2
                    ProgressBar2.PerformStepMethod2

                    FormsList.Add(CreateMiniForm(ins, ins_nonmodified))

                    ProgressBar1.PerformStepMethod2
                    ProgressBar2.PerformStepMethod2

                Else
                    ProgressBar1.PerformStepMethod2
                    ProgressBar2.PerformStepMethod2
                    ProgressBar1.PerformStepMethod2
                    ProgressBar2.PerformStepMethod2
                End If

                i += 1
                Label5.SetText(String.Format(My.Lang.Lang_LoadingChildrenForms, Math.Round((i / ITypes.Count) * 100)))
            End Using
        Next

        FormsList = FormsList.OrderBy(Function(o) o.Name).ToList()
        XenonComboBox1.Items.Clear()
        For Each f In FormsList
            XenonComboBox1.Items.Add(f.Name)
        Next

        XenonComboBox1.Visible = True
        XenonButton1.Visible = True
        ProgressBar2.Visible = False
        ProgressBar2.Value = 0
        ProgressBar1.Visible = False
        ProgressBar1.Value = 0

        Label5.SetText(My.Lang.Lang_ChooseAForm)
    End Sub

    Function CreateMiniForm(Form As Form, OriginalForm As Form) As Form
        Dim Child As New Form With {
           .Name = Form.Name,
           .Text = Form.Text,
           .Icon = Form.Icon,
           .ControlBox = True, .MinimizeBox = True, .MaximizeBox = True, .FormBorderStyle = FormBorderStyle.Sizable,
           .Padding = Form.Padding,
           .BackColor = Form.BackColor,
           .ForeColor = Form.ForeColor,
           .Font = Form.Font,
           .Dock = Form.Dock,
           .Size = Form.Size,
           .Margin = Form.Margin,
           .RightToLeft = Form.RightToLeft,
           .RightToLeftLayout = Form.RightToLeftLayout,
           .ShowIcon = Form.ShowIcon,
           .ShowInTaskbar = False,
           .TopLevel = False
       }

        If Form.FormBorderStyle = FormBorderStyle.None Then Child.Size += New Size(4 * 4 + 2, 24 * 2 - 6)

        PopulateSubControls(Form, Child, OriginalForm)

        AddHandler Child.Load, AddressOf Child_Load

        AddHandler Child.Click, AddressOf TextControlSelected

        AddHandler Child.FormClosing, AddressOf Child_Closing

        Return Child
    End Function

    Sub PopulateSubControls(From As Control, [To] As Control, OriginalForm As Form)

        For Each ctrl As Control In From.Controls

            If ctrl.HasChildren Then

                If TypeOf ctrl Is TabControl Then
                    Dim tabs As New TabControl With {
                       .Name = ctrl.Name,
                       .Text = ctrl.Text,
                       .Tag = ctrl.Tag,
                       .Anchor = ctrl.Anchor,
                       .BackColor = ctrl.BackColor,
                       .ForeColor = Color.Black,
                       .Size = ctrl.Size,
                       .Location = ctrl.Location,
                       .Dock = ctrl.Dock,
                       .Font = ctrl.Font,
                       .Alignment = CType(ctrl, TabControl).Alignment}

                    For i = 0 To CType(ctrl, TabControl).TabPages.Count - 1
                        Dim TP As New TabPage With {
                           .Name = CType(ctrl, TabControl).TabPages.Item(i).Name,
                           .Text = CType(ctrl, TabControl).TabPages.Item(i).Text,
                           .Tag = CType(ctrl, TabControl).TabPages.Item(i).Tag,
                           .BackColor = CType(ctrl, TabControl).TabPages.Item(i).BackColor,
                           .ForeColor = CType(ctrl, TabControl).TabPages.Item(i).ForeColor,
                           .Size = CType(ctrl, TabControl).TabPages.Item(i).Size,
                           .Location = CType(ctrl, TabControl).TabPages.Item(i).Location,
                           .Padding = CType(ctrl, TabControl).TabPages.Item(i).Padding,
                           .Font = CType(ctrl, TabControl).TabPages.Item(i).Font,
                           .AutoScroll = True}

                        AddHandler TP.Click, AddressOf TabPageClicked

                        PopulateSubControls(CType(ctrl, TabControl).TabPages.Item(i), TP, OriginalForm)

                        tabs.TabPages.Add(TP)
                    Next

                    AddHandler tabs.Selected, AddressOf TabControlSelected

                    [To].Controls.Add(tabs)

                ElseIf TypeOf ctrl Is UI.Simulation.Window Then
                    Dim c As New UI.Controllers.TextTranslationItem With {
                    .Name = ctrl.Name,
                    .Text = ctrl.Text,
                    .Text_English = ctrl.Text,
                    .Tag = ctrl.Tag,
                    .Tag_English = ctrl.Tag,
                    .Anchor = ctrl.Anchor,
                    .Padding = ctrl.Padding,
                    .Font = ctrl.Font,
                    .Dock = ctrl.Dock,
                    .Size = ctrl.Size,
                    .Margin = ctrl.Margin,
                    .RightToLeft = ctrl.RightToLeft,
                    .Location = ctrl.Location,
                    .TextAlign = ContentAlignment.TopLeft}

                    AddHandler c.GotFocus, AddressOf TextControlSelected

                    [To].Controls.Add(c)

                Else
                    Dim pnl As New Panel With {
                   .Name = ctrl.Name,
                   .Text = ctrl.Text,
                   .Tag = ctrl.Tag,
                   .BackColor = Color.Transparent,
                   .ForeColor = ctrl.ForeColor,
                   .Size = ctrl.Size,
                   .Location = ctrl.Location,
                   .Anchor = ctrl.Anchor,
                   .Dock = ctrl.Dock,
                   .Font = ctrl.Font,
                   .BorderStyle = BorderStyle.None}

                    PopulateSubControls(ctrl, pnl, OriginalForm)

                    [To].Controls.Add(pnl)

                    If TypeOf pnl.Parent Is TabPage Then AddHandler pnl.MouseDown, AddressOf ParentTabPageClicked

                End If
            Else

                Dim Condition0 As Boolean = Not IsNumeric(ctrl.Text) AndAlso Not String.IsNullOrWhiteSpace(ctrl.Text) AndAlso ctrl.Text.Count > 1
                Dim Condition1 As Boolean = Not IsNumeric(ctrl.Tag) AndAlso Not String.IsNullOrWhiteSpace(ctrl.Tag) AndAlso ctrl.Tag.ToString.Count > 1
                Dim Condition2 As Boolean = TypeOf ctrl IsNot TextBox AndAlso TypeOf ctrl IsNot UI.WP.TextBox AndAlso TypeOf ctrl IsNot UI.WP.SeparatorH _
                                            AndAlso TypeOf ctrl IsNot UI.WP.SeparatorV AndAlso TypeOf ctrl IsNot UI.WP.NumericUpDown AndAlso TypeOf ctrl IsNot UI.WP.Trackbar
                Dim Condition3 As Boolean = TypeOf ctrl Is PictureBox AndAlso CType(ctrl, PictureBox).Image IsNot Nothing

                If (Condition0 Or Condition1) AndAlso Condition2 Then

                    Dim c As New UI.Controllers.TextTranslationItem With {
                    .Name = ctrl.Name,
                    .Text = ctrl.Text,
                    .Text_English = OriginalForm.Controls.Find(ctrl.Name, True).First.Text,
                    .Tag = ctrl.Tag,
                    .Tag_English = OriginalForm.Controls.Find(ctrl.Name, True).First.Tag,
                    .Anchor = ctrl.Anchor,
                    .Padding = ctrl.Padding,
                    .Font = ctrl.Font,
                    .Dock = ctrl.Dock,
                    .Size = ctrl.Size,
                    .Margin = ctrl.Margin,
                    .RightToLeft = ctrl.RightToLeft,
                    .Location = ctrl.Location}

                    If TypeOf ctrl Is Label Then
                        With CType(ctrl, Label)
                            c.TextAlign = .TextAlign
                            c.ImageAlign = .ImageAlign
                        End With

                    ElseIf TypeOf ctrl Is UI.WP.Button Then
                        With CType(ctrl, UI.WP.Button)
                            c.TextAlign = .TextAlign
                            c.ImageAlign = .ImageAlign
                            c.Image = .Image
                        End With

                    ElseIf TypeOf ctrl Is Button Then
                        With CType(ctrl, Button)
                            c.TextAlign = .TextAlign
                            c.ImageAlign = .ImageAlign
                            c.Image = .Image
                        End With

                    ElseIf TypeOf ctrl Is UI.WP.RadioImage Then
                        With CType(ctrl, UI.WP.RadioImage)
                            c.Text = If(.ShowText, .Text, "")
                            c.Image = .Image
                        End With

                    ElseIf TypeOf ctrl Is UI.WP.CheckBox OrElse TypeOf ctrl Is UI.WP.RadioButton Then
                        c.TextAlign = ContentAlignment.MiddleLeft

                    ElseIf TypeOf ctrl Is UI.WP.AlertBox Then
                        With CType(ctrl, UI.WP.AlertBox)
                            c.Image = Nothing
                            c.TextAlign = ContentAlignment.MiddleLeft
                        End With

                    End If


                    AddHandler c.GotFocus, AddressOf TextControlSelected

                    [To].Controls.Add(c)

                ElseIf Condition3 Then

                    Dim c As New PictureBox With {
                    .Name = ctrl.Name,
                    .Text = ctrl.Text,
                    .Tag = ctrl.Tag,
                    .Padding = ctrl.Padding,
                    .Font = ctrl.Font,
                    .Dock = ctrl.Dock,
                    .Size = ctrl.Size,
                    .Margin = ctrl.Margin,
                    .RightToLeft = ctrl.RightToLeft,
                    .Location = ctrl.Location,
                    .Image = CType(ctrl, PictureBox).Image,
                    .SizeMode = CType(ctrl, PictureBox).SizeMode}

                    [To].Controls.Add(c)

                End If

            End If
        Next

    End Sub
#End Region

#Region "Child form events"
    Sub Child_Load(sender As Object, e As EventArgs)
        ApplyStyle(CType(sender, Form))
    End Sub

    Sub Child_Closing(sender As Object, e As FormClosingEventArgs)
        FormsList(XenonComboBox1.SelectedIndex) = _Form
        SplitContainer1.Panel2Collapsed = True
        XenonGroupBox4.Visible = True
        _Form.Hide()
        e.Cancel = True
    End Sub
#End Region

#Region "Language item control events"
    Sub TextControlSelected(sender As Object, e As EventArgs)
        If TypeOf _SelectedItem Is UI.Controllers.TextTranslationItem Then
            With CType(_SelectedItem, UI.Controllers.TextTranslationItem)
                .Pressed = False
                .Invalidate()
            End With
        End If

        _SelectedItem = sender
        _SelectedItem.Focus()
        RaiseEvent ControlSelection(_SelectedItem, New EventArgs)
    End Sub

    Sub TabControlSelected(sender As Object, e As TabControlEventArgs)
        If TypeOf _SelectedItem Is UI.Controllers.TextTranslationItem Then
            With CType(_SelectedItem, UI.Controllers.TextTranslationItem)
                .Pressed = False
                .Invalidate()
            End With
        End If

        _SelectedItem = e.TabPage
        _SelectedItem.Focus()
        RaiseEvent ControlSelection(_SelectedItem, New EventArgs)
    End Sub

    Sub TabPageClicked(sender As Object, e As EventArgs)
        If TypeOf _SelectedItem Is UI.Controllers.TextTranslationItem Then
            With CType(_SelectedItem, UI.Controllers.TextTranslationItem)
                .Pressed = False
                .Invalidate()
            End With
        End If

        _SelectedItem = sender
        _SelectedItem.Focus()
        RaiseEvent ControlSelection(_SelectedItem, New EventArgs)
    End Sub

    Sub ParentTabPageClicked(sender As Object, e As EventArgs)
        If TypeOf _SelectedItem Is UI.Controllers.TextTranslationItem Then
            With CType(_SelectedItem, UI.Controllers.TextTranslationItem)
                .Pressed = False
                .Invalidate()
            End With
        End If

        _SelectedItem = CType(CType(sender, Control).Parent, TabPage)
        _SelectedItem.Focus()
        RaiseEvent ControlSelection(_SelectedItem, New EventArgs)
    End Sub

    Private Sub Lang_JSON_GUI_ControlSelection(sender As Object, e As EventArgs) Handles Me.ControlSelection
        AllowEditing = False

        Label4.Text = sender.Name

        If Not String.IsNullOrWhiteSpace(sender.Text) Then
            XenonTextBox1.Text = sender.Text
            EditingTag = False

        ElseIf sender.Tag IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(sender.Tag.ToString) Then
            XenonTextBox1.Text = sender.Tag.ToString
            EditingTag = True

        Else
            XenonTextBox1.Text = ""
            EditingTag = False

        End If

        If TypeOf sender Is UI.Controllers.TextTranslationItem Then

            With CType(sender, UI.Controllers.TextTranslationItem)

                If Not String.IsNullOrWhiteSpace(.Text_English) Then
                    XenonTextBox2.Text = .Text_English
                    EditingTag = False

                ElseIf Not String.IsNullOrWhiteSpace(.Tag_English) Then
                    XenonTextBox2.Text = .Tag_English
                    EditingTag = True

                Else
                    XenonTextBox2.Text = ""
                    EditingTag = False

                End If

            End With

        End If

        AllowEditing = True
    End Sub
#End Region

#Region "Search and replace"
    Private Sub data_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles data.CellPainting
        If e.Value Is Nothing OrElse SearchText Is Nothing OrElse String.IsNullOrWhiteSpace(SearchText) OrElse e.RowIndex < 0 Then Return

        If e.ColumnIndex = 2 And Not XenonCheckBox1.Checked Then Return
        If e.ColumnIndex = 0 And Not XenonCheckBox2.Checked Then Return

        Dim sf As StringFormat = StringFormat.GenericTypographic
        sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.MeasureTrailingSpaces Or StringFormatFlags.DisplayFormatControl

        Dim text As String = e.Value.ToString()
        Dim textSize As SizeF = e.Graphics.MeasureString(text, Font, e.CellBounds.Width, sf)
        Dim keyPos = text.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase)

        If keyPos >= 0 Then
            e.PaintBackground(e.CellBounds, True)

            Dim br As New SolidBrush(e.CellStyle.ForeColor)

            Dim textMetricSize As New SizeF(0, 0)
            If keyPos >= 1 Then
                Dim textMetric = text.Substring(0, keyPos)
                textMetricSize = e.Graphics.MeasureString(textMetric, Font, e.CellBounds.Width, sf)
            End If

            Dim keySize As SizeF = e.Graphics.MeasureString(text.Substring(keyPos, SearchText.Length), Font, e.CellBounds.Width, sf)
            Dim left As Single = e.CellBounds.Left + If(keyPos <= 0, 0, textMetricSize.Width) '+ 2
            Dim keyRect As New RectangleF(left, e.CellBounds.Top + 1, keySize.Width, keySize.Height)

            Dim fillBrush = New SolidBrush(My.Style.Colors.SearchColor)
            e.Graphics.FillRectangle(fillBrush, keyRect)
            fillBrush.Dispose()

            e.Graphics.DrawString(text, Font, br, e.CellBounds, sf)
            e.Handled = True

            br.Dispose()

        Else
            Return
        End If

    End Sub

    Private Sub XenonTextBox8_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox8.TextChanged
        SearchText = XenonTextBox8.Text.ToLower.Trim

        data.ClearSelection()

        If String.IsNullOrWhiteSpace(SearchText) Then
            data.Refresh()
            Exit Sub
        End If

        For r = 0 To data.Rows.Count - 1
            data.Item(1, r).Selected = data.Item(1, r).Value.ToString.ToLower.Trim.Contains(SearchText)
            If XenonCheckBox1.Checked Then data.Item(2, r).Selected = data.Item(2, r).Value.ToString.ToLower.Trim.Contains(SearchText)
            If XenonCheckBox2.Checked Then data.Item(0, r).Selected = data.Item(0, r).Value.ToString.ToLower.Trim.Contains(SearchText)
        Next

        If data.SelectedCells IsNot Nothing AndAlso data.SelectedCells.Count > 0 Then data.FirstDisplayedScrollingRowIndex = data.SelectedCells(0).RowIndex

        data.Refresh()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        XenonTextBox8.Text = Lang_ReplaceText.Replace(data, 1, XenonTextBox8.Text)
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        XenonTextBox9.Text = Lang_ReplaceText.Replace(_Form, XenonTextBox9.Text)
    End Sub

    Private Sub XenonTextBox9_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox9.TextChanged
        For Each ctrl In _Form.GetAllControls
            If TypeOf ctrl Is UI.Controllers.TextTranslationItem Then
                With DirectCast(ctrl, UI.Controllers.TextTranslationItem)
                    .SearchHilight = XenonTextBox9.Text
                End With
            End If
        Next
    End Sub
#End Region

    Private Sub Lang_JSON_GUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckForIllegalCrossThreadCalls = False         'Prevent exception error of cross-thread

        Icon = Lang_JSON_Manage.Icon
        LangFile = ""
        XenonAlertBox1.Visible = True
        XenonGroupBox8.Visible = False
        XenonTabControl1.Visible = False
        SplitContainer1.Panel2Collapsed = True
        XenonGroupBox4.Visible = True

        LoadLanguage
        ApplyStyle(Me)

        Label4.Font = My.Application.ConsoleFontMedium
        Label9.Font = Label4.Font
        Label5.Text = My.Lang.Lang_ChooseAForm
        data.DoubleBuffer

        Refresh()
    End Sub

    Private Sub Lang_JSON_GUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Th IsNot Nothing AndAlso Th.IsAlive Then Th.Abort()
        For Each f In FormsList
            f.Dispose()
        Next
        FormsList.Clear()
        _Form = Nothing
        _SelectedItem = Nothing
        Lang.Dispose()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        If XenonComboBox1.SelectedItem IsNot Nothing AndAlso XenonComboBox1.Items.Count > 0 Then
            _Form = FormsList(XenonComboBox1.SelectedIndex)
            _Form.Show()
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.Panel1.Controls.Add(_Form)
        End If

        XenonGroupBox4.Visible = False
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        If AllowEditing AndAlso _SelectedItem IsNot Nothing Then
            If Not EditingTag Then
                _SelectedItem.Text = XenonTextBox1.Text
            Else
                _SelectedItem.Tag = XenonTextBox1.Text

            End If
        End If

    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FontDialog1.Font = XenonTextBox1.Font

        If FontDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Font = FontDialog1.Font
            XenonTextBox2.Font = FontDialog1.Font
            data.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            XenonAlertBox1.Visible = False
            XenonGroupBox8.Visible = True
            XenonTabControl1.Visible = False
            Cursor = Cursors.WaitCursor
            LangFile = OpenJSONDlg.FileName
            OpenFile()
            Cursor = Cursors.Default
            XenonTabControl1.Visible = True
        End If
    End Sub

    Private Sub XenonRadioButton1_CheckedChanged(sender As Object) Handles XenonRadioButton1.CheckedChanged
        XenonTextBox1.RightToLeft = If(XenonRadioButton2.Checked, RightToLeft.Yes, RightToLeft.No)
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        XenonTextBox7.Text = My.AppVersion
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If Lang_Add_Snippet.ShowDialog = DialogResult.OK Then
            XenonTextBox3.Text = Lang_Add_Snippet._Result
        End If
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        If Lang_Add_Snippet.ShowDialog = DialogResult.OK Then
            XenonTextBox4.Text = Lang_Add_Snippet._Result
        End If
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        XenonTextBox5.Text = Environment.UserName
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            XenonAlertBox1.Visible = False
            XenonTabControl1.Visible = False
            Cursor = Cursors.WaitCursor
            Using LangX As New Localizer
                LangX.ExportJSON(SaveJSONDlg.FileName)
            End Using
            LangFile = SaveJSONDlg.FileName
            OpenFile()
            Cursor = Cursors.Default
            XenonTabControl1.Visible = True
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor
            Using LangX As New Localizer
                LangX.ExportJSON(SaveJSONDlg.FileName)
            End Using
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Lang.ExportJSON(LangFile, FormsList.ToArray)

        Dim JObj As JObject = JToken.Parse(IO.File.ReadAllText(LangFile))

        Dim j_info As New JObject From {
            {"Name".ToLower, XenonTextBox5.Text},
            {"TranslationVersion".ToLower, XenonTextBox6.Text},
            {"Lang".ToLower, XenonTextBox3.Text},
            {"LangCode".ToLower, XenonTextBox4.Text},
            {"AppVer".ToLower, XenonTextBox7.Text},
            {"RightToLeft".ToLower, XenonRadioButton2.Checked}
        }

        Dim j_globalstrings As New JObject()
        For r = 0 To data.Rows.Count - 1
            j_globalstrings(data.Item(0, r).Value.ToString.ToLower) = data.Item(1, r).Value.ToString
        Next

        JObj("Information") = j_info
        JObj("Global Strings") = j_globalstrings

        IO.File.WriteAllText(LangFile, JObj.ToString)

        MsgBox(My.Lang.LangSaved, MsgBoxStyle.Information)
    End Sub

    Private Sub data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles data.CellEndEdit
        If data.Item(1, e.RowIndex).Value.ToString.ToLower.Trim <> data.Item(2, e.RowIndex).Value.ToString.ToLower.Trim AndAlso Not String.IsNullOrWhiteSpace(data.Item(1, e.RowIndex).Value.ToString.ToLower.Trim) Then
            data.Item(1, e.RowIndex).Style.BackColor = data.Item(2, e.RowIndex).Style.BackColor
        Else
            data.Item(1, e.RowIndex).Style.BackColor = My.Style.Colors.NotTranslatedColor
        End If
    End Sub

    Private Sub XenonTabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonTabControl1.SelectedIndexChanged
        If ProgressBar1.Visible Then
            ProgressBar2.Visible = XenonTabControl1.SelectedIndex <> 2
        Else
            ProgressBar2.Visible = False
        End If
    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged, XenonCheckBox2.CheckedChanged
        data.Refresh()
    End Sub
End Class
