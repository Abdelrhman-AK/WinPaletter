using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Lang_JSON_GUI
    {

        #region Variables and events
        public static event EventHandler ControlSelection;

        private Control _SelectedItem;
        private List<Form> FormsList = new List<Form>();

        private string LangFile;
        private Localizer Lang = new Localizer();

        private bool EditingTag = true;
        private bool AllowEditing = false;
        private Form _Form;
        private IEnumerable<Type> ITypes = Assembly.GetCallingAssembly().GetTypes().Where(t => typeof(Form).IsAssignableFrom(t));

        private Thread Th;

        private string SearchText;

        public Lang_JSON_GUI()
        {
            InitializeComponent();
        }


        #endregion

        #region Helpers Voids/functions
        public void OpenFile()
        {
            Lang = new Localizer();
            Lang.Load(LangFile);

            Label9.Text = LangFile;

            TextBox3.Text = Lang.Lang;
            TextBox4.Text = Lang.LangCode;
            TextBox5.Text = Lang.Name;
            TextBox6.Text = Lang.TranslationVersion;
            TextBox7.Text = Lang.AppVer;
            RadioButton2.Checked = Lang.RightToLeft;

            LoadGlobalStrings();

            LoadAllMiniFormsIntoList();
        }

        public void LoadGlobalStrings()
        {
            data.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            data.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            data.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            data.Columns[0].DefaultCellStyle.Font = Fonts.ConsoleMedium;
            data.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            data.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            int row_index = 0;
            var rows = new List<DataGridViewRow>();
            rows.Clear();

            JObject JObject = (JObject)JToken.Parse(System.IO.File.ReadAllText(LangFile))["Global Strings"];

            foreach (PropertyInfo property in Program.Lang.GetType().GetProperties())
            {

                if (!string.IsNullOrWhiteSpace(property.GetValue(Program.Lang).ToString()) & !((property.Name.ToLower() ?? "") == ("Name".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("TranslationVersion".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("Lang".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("LangCode".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("AppVer".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("RightToLeft".ToLower() ?? "")))
                {

                    var row = new DataGridViewRow();
                    row.CreateCells(data);

                    row.Cells[0].Value = property.Name.ToLower();
                    row.Cells[0].ReadOnly = true;

                    row.Cells[2].Value = property.GetValue(Program.Lang).ToString();
                    row.Cells[2].ReadOnly = true;


                    if (JObject[property.Name.ToLower()] is not null)
                    {
                        row.Cells[1].Value = JObject[property.Name.ToLower()].ToString();
                        row.Cells[1].ReadOnly = false;

                        if ((row.Cells[2].Value.ToString().ToLower().Trim() ?? "") == (row.Cells[1].Value.ToString().ToLower().Trim() ?? ""))
                        {
                            row.Cells[1].Style.BackColor = Program.Style.Colors.NotTranslatedColor;
                        }
                        else if ((row.Cells[2].Value.ToString() ?? "").Contains("{") || (row.Cells[1].Value.ToString() ?? "").Contains("{"))
                        {
                            int count1 = (row.Cells[1].Value ?? "").ToString().Count(c => c == '{');
                            int count2 = (row.Cells[2].Value ?? "").ToString().Count(c => c == '{');
                            if (count1 != count2)
                            {
                                row.Cells[1].Style.BackColor = Program.Style.Colors.NotTranslatedColor;
                            }
                        }
                    }

                    else
                    {
                        row.Cells[1].Style.BackColor = Program.Style.Colors.NotTranslatedColor;
                        row.Cells[1].Value = "";
                        row.Cells[1].ReadOnly = false;
                    }

                    rows.Add(row);
                    row_index += 1;
                }
            }

            data.Rows.Clear();
            data.Rows.AddRange(rows.ToArray());
        }

        public void LoadAllMiniFormsIntoList()
        {
            ProgressBar2.Value = 0;
            ProgressBar2.Maximum = ITypes.Count() * ProgressBar2.Step * 2;
            ProgressBar2.Visible = true;

            ProgressBar1.Value = 0;
            ProgressBar1.Maximum = ITypes.Count() * ProgressBar2.Step * 2;
            ProgressBar1.Visible = true;

            ComboBox1.Visible = false;
            Button1.Visible = false;

            FormsList.Clear();
            ComboBox1.Items.Clear();

            if (Th is not null && Th.IsAlive)
                Th.Abort();
            Th = new Thread(LoadAllMiniFormsIntoList_Thread) { Priority = ThreadPriority.Highest, IsBackground = true };
            Th.Start();
        }

        public void LoadAllMiniFormsIntoList_Thread()
        {
            int i = 0;
            foreach (var f in ITypes)
            {

                using (Form ins = (Form)Activator.CreateInstance(f))
                using (Form ins_nonmodified = (Form)Activator.CreateInstance(f))
                {
                    if (ins.Controls.Count > 0)
                    {
                        ins.LoadLanguage(Lang);

                        ProgressBar1.PerformStepMethod2();
                        ProgressBar2.PerformStepMethod2();

                        FormsList.Add(CreateMiniForm(ins, ins_nonmodified));

                        ProgressBar1.PerformStepMethod2();
                        ProgressBar2.PerformStepMethod2();
                    }

                    else
                    {
                        ProgressBar1.PerformStepMethod2();
                        ProgressBar2.PerformStepMethod2();
                        ProgressBar1.PerformStepMethod2();
                        ProgressBar2.PerformStepMethod2();
                    }

                    i += 1;
                    Label5.SetText(string.Format(Program.Lang.Lang_LoadingChildrenForms, Math.Round(i / (double)ITypes.Count() * 100d)));
                }
            }

            FormsList = FormsList.OrderBy(o => o.Name).ToList();
            ComboBox1.Items.Clear();
            foreach (var f in FormsList)
                ComboBox1.Items.Add(f.Name);

            ComboBox1.Visible = true;
            Button1.Visible = true;
            ProgressBar2.Visible = false;
            ProgressBar2.Value = 0;
            ProgressBar1.Visible = false;
            ProgressBar1.Value = 0;

            Label5.SetText(Program.Lang.Lang_ChooseAForm);
        }

        public Form CreateMiniForm(Form Form, Form OriginalForm)
        {
            Form Child = new Form()
            {
                Name = Form.Name,
                Text = Form.Text,
                Icon = Form.Icon,
                ControlBox = true,
                MinimizeBox = true,
                MaximizeBox = true,
                FormBorderStyle = FormBorderStyle.Sizable,
                Padding = Form.Padding,
                BackColor = Form.BackColor,
                ForeColor = Form.ForeColor,
                Font = Form.Font,
                Dock = Form.Dock,
                Size = Form.Size,
                Margin = Form.Margin,
                RightToLeft = Form.RightToLeft,
                RightToLeftLayout = Form.RightToLeftLayout,
                ShowIcon = Form.ShowIcon,
                ShowInTaskbar = false,
                TopLevel = false
            };

            if (Form.FormBorderStyle == FormBorderStyle.None)
                Child.Size += new Size(4 * 4 + 2, 24 * 2 - 6);

            PopulateSubControls(Form, Child, OriginalForm);

            Child.Load += Child_Load;

            Child.Click += TextControlSelected;

            Child.FormClosing += Child_Closing;

            return Child;
        }

        public void PopulateSubControls(Control From, Control To, Form OriginalForm)
        {

            foreach (Control ctrl in From.Controls)
            {

                if (ctrl.HasChildren)
                {

                    if (ctrl is TabControl)
                    {
                        var tabs = new TabControl()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Tag = (string)(ctrl.Tag ?? ""),
                            Anchor = ctrl.Anchor,
                            BackColor = ctrl.BackColor,
                            ForeColor = Color.Black,
                            Size = ctrl.Size,
                            Location = ctrl.Location,
                            Dock = ctrl.Dock,
                            Font = ctrl.Font,
                            Alignment = ((TabControl)ctrl).Alignment
                        };

                        for (int i = 0, loopTo = ((TabControl)ctrl).TabPages.Count - 1; i <= loopTo; i++)
                        {
                            var TP = new TabPage()
                            {
                                Name = ((TabControl)ctrl).TabPages[i].Name,
                                Text = ((TabControl)ctrl).TabPages[i].Text,
                                Tag = (string)(((TabControl)ctrl).TabPages[i].Tag ?? ""),
                                BackColor = ((TabControl)ctrl).TabPages[i].BackColor,
                                ForeColor = ((TabControl)ctrl).TabPages[i].ForeColor,
                                Size = ((TabControl)ctrl).TabPages[i].Size,
                                Location = ((TabControl)ctrl).TabPages[i].Location,
                                Padding = ((TabControl)ctrl).TabPages[i].Padding,
                                Font = ((TabControl)ctrl).TabPages[i].Font,
                                AutoScroll = true
                            };

                            TP.Click += TabPageClicked;

                            PopulateSubControls(((TabControl)ctrl).TabPages[i], TP, OriginalForm);

                            tabs.TabPages.Add(TP);
                        }

                        tabs.Selected += TabControlSelected;

                        To.Controls.Add(tabs);
                    }

                    else if (ctrl is UI.Simulation.Window)
                    {
                        var c = new UI.Controllers.TextTranslationItem()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Text_English = ctrl.Text,
                            Tag = ctrl.Tag ?? "",
                            Tag_English = (string)(ctrl.Tag ?? ""),
                            Anchor = ctrl.Anchor,
                            Padding = ctrl.Padding,
                            Font = ctrl.Font,
                            Dock = ctrl.Dock,
                            Size = ctrl.Size,
                            Margin = ctrl.Margin,
                            RightToLeft = ctrl.RightToLeft,
                            Location = ctrl.Location,
                            TextAlign = ContentAlignment.TopLeft
                        };

                        c.GotFocus += TextControlSelected;

                        To.Controls.Add(c);
                    }

                    else
                    {
                        var pnl = new Panel()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Tag = (string)(ctrl.Tag ?? ""),
                            BackColor = Color.Transparent,
                            ForeColor = ctrl.ForeColor,
                            Size = ctrl.Size,
                            Location = ctrl.Location,
                            Anchor = ctrl.Anchor,
                            Dock = ctrl.Dock,
                            Font = ctrl.Font,
                            BorderStyle = BorderStyle.None
                        };

                        PopulateSubControls(ctrl, pnl, OriginalForm);

                        To.Controls.Add(pnl);

                        if (pnl.Parent is TabPage)
                            pnl.MouseDown += ParentTabPageClicked;

                    }
                }
                else
                {
                    bool Condition0 = ctrl.Text is not null && !ctrl.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(ctrl.Text) && ctrl.Text.Count() > 1;
                    bool Condition1 = ctrl.Tag is not null && !ctrl.Tag.ToString().All(char.IsDigit) && !string.IsNullOrWhiteSpace(ctrl.Tag.ToString()) && ctrl.Tag.ToString().Count() > 1;
                    bool Condition2 = ctrl is not TextBox && ctrl is not UI.WP.TextBox && ctrl is not UI.WP.SeparatorH && ctrl is not UI.WP.SeparatorV && ctrl is not UI.WP.NumericUpDown && ctrl is not UI.WP.Trackbar;
                    bool Condition3 = ctrl is PictureBox && ((PictureBox)ctrl).Image is not null;

                    if (Condition0 | Condition1 && Condition2)
                    {
                        var c = new UI.Controllers.TextTranslationItem()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Text_English = OriginalForm.Controls.Find(ctrl.Name, true).First().Text,
                            Tag = ctrl.Tag ?? "",
                            Tag_English = (string)(OriginalForm.Controls.Find(ctrl.Name, true).First().Tag ?? ""),
                            Anchor = ctrl.Anchor,
                            Padding = ctrl.Padding,
                            Font = ctrl.Font,
                            Dock = ctrl.Dock,
                            Size = ctrl.Size,
                            Margin = ctrl.Margin,
                            RightToLeft = ctrl.RightToLeft,
                            Location = ctrl.Location
                        };

                        if (ctrl is Label)
                        {
                            {
                                var temp = (Label)ctrl;
                                c.TextAlign = temp.TextAlign;
                                c.ImageAlign = temp.ImageAlign;
                            }
                        }

                        else if (ctrl is UI.WP.Button)
                        {
                            {
                                var temp1 = (UI.WP.Button)ctrl;
                                c.TextAlign = temp1.TextAlign;
                                c.ImageAlign = temp1.ImageAlign;
                                c.Image = temp1.Image;
                            }
                        }

                        else if (ctrl is Button)
                        {
                            {
                                var temp2 = (Button)ctrl;
                                c.TextAlign = temp2.TextAlign;
                                c.ImageAlign = temp2.ImageAlign;
                                c.Image = temp2.Image;
                            }
                        }

                        else if (ctrl is UI.WP.RadioImage)
                        {
                            {
                                var temp3 = (UI.WP.RadioImage)ctrl;
                                c.Text = temp3.ShowText ? temp3.Text : "";
                                c.Image = temp3.Image;
                            }
                        }

                        else if (ctrl is UI.WP.CheckBox || ctrl is UI.WP.RadioButton)
                        {
                            c.TextAlign = ContentAlignment.MiddleLeft;
                        }

                        else if (ctrl is UI.WP.AlertBox)
                        {
                            {
                                var temp4 = (UI.WP.AlertBox)ctrl;
                                c.Image = null;
                                c.TextAlign = ContentAlignment.MiddleLeft;
                            }

                        }


                        c.GotFocus += TextControlSelected;

                        To.Controls.Add(c);
                    }

                    else if (Condition3)
                    {

                        var c = new PictureBox()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Tag = (string)(ctrl.Tag ?? ""),
                            Padding = ctrl.Padding,
                            Font = ctrl.Font,
                            Dock = ctrl.Dock,
                            Size = ctrl.Size,
                            Margin = ctrl.Margin,
                            RightToLeft = ctrl.RightToLeft,
                            Location = ctrl.Location,
                            Image = ((PictureBox)ctrl).Image,
                            SizeMode = ((PictureBox)ctrl).SizeMode
                        };

                        To.Controls.Add(c);

                    }

                }
            }

        }
        #endregion

        #region Child form events
        public void Child_Load(object sender, EventArgs e)
        {
            ApplyStyle((Form)sender);
        }

        public void Child_Closing(object sender, FormClosingEventArgs e)
        {
            FormsList[ComboBox1.SelectedIndex] = _Form;
            SplitContainer1.Panel2Collapsed = true;
            GroupBox4.Visible = true;
            _Form.Hide();
            e.Cancel = true;
        }
        #endregion

        #region Language item control events
        public void TextControlSelected(object sender, EventArgs e)
        {
            if (_SelectedItem is UI.Controllers.TextTranslationItem)
            {
                {
                    var temp = (UI.Controllers.TextTranslationItem)_SelectedItem;
                    temp.Pressed = false;
                    temp.Invalidate();
                }
            }

            _SelectedItem = (Control)sender;
            _SelectedItem.Focus();
            ControlSelection?.Invoke(_SelectedItem, new EventArgs());
        }

        public void TabControlSelected(object sender, TabControlEventArgs e)
        {
            if (_SelectedItem is UI.Controllers.TextTranslationItem)
            {
                {
                    var temp = (UI.Controllers.TextTranslationItem)_SelectedItem;
                    temp.Pressed = false;
                    temp.Invalidate();
                }
            }

            _SelectedItem = e.TabPage;
            _SelectedItem.Focus();
            ControlSelection?.Invoke(_SelectedItem, new EventArgs());
        }

        public void TabPageClicked(object sender, EventArgs e)
        {
            if (_SelectedItem is UI.Controllers.TextTranslationItem)
            {
                {
                    var temp = (UI.Controllers.TextTranslationItem)_SelectedItem;
                    temp.Pressed = false;
                    temp.Invalidate();
                }
            }

            _SelectedItem = (Control)sender;
            _SelectedItem.Focus();
            ControlSelection?.Invoke(_SelectedItem, new EventArgs());
        }

        public void ParentTabPageClicked(object sender, EventArgs e)
        {
            if (_SelectedItem is UI.Controllers.TextTranslationItem)
            {
                {
                    var temp = (UI.Controllers.TextTranslationItem)_SelectedItem;
                    temp.Pressed = false;
                    temp.Invalidate();
                }
            }

            _SelectedItem = (TabPage)((Control)sender).Parent;
            _SelectedItem.Focus();
            ControlSelection?.Invoke(_SelectedItem, new EventArgs());
        }

        private void Lang_JSON_GUI_ControlSelection(object sender, EventArgs e)
        {
            AllowEditing = false;

            Label4.Text = ((Control)sender).Name;

            if (!string.IsNullOrWhiteSpace(((Control)sender).Text))
            {
                TextBox1.Text = ((Control)sender).Text;
                EditingTag = false;
            }

            else if (((Control)sender).Tag is not null && !string.IsNullOrWhiteSpace(((Control)sender).Tag.ToString()))
            {
                TextBox1.Text = ((Control)sender).Tag.ToString();
                EditingTag = true;
            }

            else
            {
                TextBox1.Text = "";
                EditingTag = false;
            }

            if (sender is UI.Controllers.TextTranslationItem)
            {

                {
                    var temp = (UI.Controllers.TextTranslationItem)sender;

                    if (!string.IsNullOrWhiteSpace(temp.Text_English))
                    {
                        TextBox2.Text = temp.Text_English;
                        EditingTag = false;
                    }

                    else if (!string.IsNullOrWhiteSpace(temp.Tag_English))
                    {
                        TextBox2.Text = temp.Tag_English;
                        EditingTag = true;
                    }

                    else
                    {
                        TextBox2.Text = "";
                        EditingTag = false;

                    }

                }

            }

            AllowEditing = true;
        }
        #endregion

        #region Search and replace
        private void data_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.Value is null || SearchText is null || string.IsNullOrWhiteSpace(SearchText) || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 2 & !CheckBox1.Checked)
                return;
            if (e.ColumnIndex == 0 & !CheckBox2.Checked)
                return;

            var sf = StringFormat.GenericTypographic;
            sf.FormatFlags = sf.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.DisplayFormatControl;

            string text = e.Value.ToString();
            var textSize = e.Graphics.MeasureString(text, Font, e.CellBounds.Width, sf);
            int keyPos = text.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase);

            if (keyPos >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                var br = new SolidBrush(e.CellStyle.ForeColor);

                var textMetricSize = new SizeF(0f, 0f);
                if (keyPos >= 1)
                {
                    string textMetric = text.Substring(0, keyPos);
                    textMetricSize = e.Graphics.MeasureString(textMetric, Font, e.CellBounds.Width, sf);
                }

                var keySize = e.Graphics.MeasureString(text.Substring(keyPos, SearchText.Length), Font, e.CellBounds.Width, sf);
                float left = e.CellBounds.Left + (keyPos <= 0 ? 0f : textMetricSize.Width); // + 2
                var keyRect = new RectangleF(left, e.CellBounds.Top + 1, keySize.Width, keySize.Height);

                var fillBrush = new SolidBrush(Program.Style.Colors.SearchColor);
                e.Graphics.FillRectangle(fillBrush, keyRect);
                fillBrush.Dispose();

                e.Graphics.DrawString(text, Font, br, e.CellBounds, sf);
                e.Handled = true;

                br.Dispose();
            }

            else
            {
                return;
            }

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            SearchText = TextBox8.Text.ToLower().Trim();

            data.ClearSelection();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                data.Refresh();
                return;
            }

            for (int r = 0, loopTo = data.Rows.Count - 1; r <= loopTo; r++)
            {
                data[1, r].Selected = data[1, r].Value.ToString().ToLower().Trim().Contains(SearchText);
                if (CheckBox1.Checked)
                    data[2, r].Selected = data[2, r].Value.ToString().ToLower().Trim().Contains(SearchText);
                if (CheckBox2.Checked)
                    data[0, r].Selected = data[0, r].Value.ToString().ToLower().Trim().Contains(SearchText);
            }

            if (data.SelectedCells is not null && data.SelectedCells.Count > 0)
                data.FirstDisplayedScrollingRowIndex = data.SelectedCells[0].RowIndex;

            data.Refresh();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            TextBox8.Text = Forms.Lang_ReplaceText.Replace(data, 1, TextBox8.Text);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            TextBox9.Text = Forms.Lang_ReplaceText.Replace(_Form, TextBox9.Text);
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            foreach (var ctrl in _Form.GetAllControls())
            {
                if (ctrl is UI.Controllers.TextTranslationItem)
                {
                    {
                        var temp = (UI.Controllers.TextTranslationItem)ctrl;
                        temp.SearchHighlight = TextBox9.Text;
                    }
                }
            }
        }
        #endregion

        private void Lang_JSON_GUI_Load(object sender, EventArgs e)
        {

            CheckForIllegalCrossThreadCalls = false;         // Prevent exception error of cross-thread

            Icon = Forms.Lang_JSON_Manage.Icon;
            LangFile = "";
            AlertBox1.Visible = true;
            GroupBox8.Visible = false;
            TabControl1.Visible = false;
            SplitContainer1.Panel2Collapsed = true;
            GroupBox4.Visible = true;

            this.LoadLanguage();
            ApplyStyle(this);

            Label4.Font = Fonts.ConsoleMedium;
            Label9.Font = Label4.Font;
            Label5.Text = Program.Lang.Lang_ChooseAForm;
            data.DoubleBuffer();

            ControlSelection += Lang_JSON_GUI_ControlSelection;

            Refresh();
        }

        private void Lang_JSON_GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            ControlSelection -= Lang_JSON_GUI_ControlSelection;

            if (Th is not null && Th.IsAlive)
                Th.Abort();
            foreach (var f in FormsList)
                f.Dispose();
            FormsList.Clear();
            _Form = null;
            _SelectedItem = null;
            Lang.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (ComboBox1.SelectedItem is not null && ComboBox1.Items.Count > 0)
            {
                _Form = FormsList[ComboBox1.SelectedIndex];
                _Form.Show();
                SplitContainer1.Panel2Collapsed = false;
                SplitContainer1.Panel1.Controls.Add(_Form);
            }

            GroupBox4.Visible = false;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (AllowEditing && _SelectedItem is not null)
            {
                if (!EditingTag)
                {
                    _SelectedItem.Text = TextBox1.Text;
                }
                else
                {
                    _SelectedItem.Tag = TextBox1.Text;
                }
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = TextBox1.Font;

            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Font = FontDialog1.Font;
                TextBox2.Font = FontDialog1.Font;
                data.Font = FontDialog1.Font;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (OpenJSONDlg.ShowDialog() == DialogResult.OK)
            {
                AlertBox1.Visible = false;
                GroupBox8.Visible = true;
                TabControl1.Visible = false;
                Cursor = Cursors.WaitCursor;
                LangFile = OpenJSONDlg.FileName;
                OpenFile();
                Cursor = Cursors.Default;
                TabControl1.Visible = true;
            }
        }

        private void RadioButton1_CheckedChanged(object sender)
        {
            TextBox1.RightToLeft = RadioButton2.Checked ? RightToLeft.Yes : RightToLeft.No;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            TextBox7.Text = Program.Version;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (Forms.Lang_Add_Snippet.ShowDialog() == DialogResult.OK)
            {
                TextBox3.Text = Forms.Lang_Add_Snippet._Result;
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (Forms.Lang_Add_Snippet.ShowDialog() == DialogResult.OK)
            {
                TextBox4.Text = Forms.Lang_Add_Snippet._Result;
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            TextBox5.Text = Environment.UserName;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (SaveJSONDlg.ShowDialog() == DialogResult.OK)
            {
                AlertBox1.Visible = false;
                TabControl1.Visible = false;
                Cursor = Cursors.WaitCursor;
                using (var LangX = new Localizer())
                {
                    LangX.ExportJSON(SaveJSONDlg.FileName);
                }
                LangFile = SaveJSONDlg.FileName;
                OpenFile();
                Cursor = Cursors.Default;
                TabControl1.Visible = true;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (SaveJSONDlg.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                using (var LangX = new Localizer())
                {
                    LangX.ExportJSON(SaveJSONDlg.FileName);
                }
                Cursor = Cursors.Default;
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(LangFile))
            {
                Lang.ExportJSON(LangFile, FormsList.ToArray());

                JObject JObj = (JObject)JToken.Parse(System.IO.File.ReadAllText(LangFile));

                var j_info = new JObject() { { "Name".ToLower(), TextBox5.Text ?? "" }, { "TranslationVersion".ToLower(), TextBox6.Text ?? "" }, { "Lang".ToLower(), TextBox3.Text ?? "" }, { "LangCode".ToLower(), TextBox4.Text ?? "" }, { "AppVer".ToLower(), TextBox7.Text ?? "" }, { "RightToLeft".ToLower(), RadioButton2.Checked } };

                var j_globalstrings = new JObject();
                for (int r = 0, loopTo = data.Rows.Count - 1; r <= loopTo; r++)
                    j_globalstrings[data[0, r].Value.ToString().ToLower()] = (data[1, r].Value ?? "").ToString();

                JObj["Information"] = j_info;
                JObj["Global Strings"] = j_globalstrings;

                System.IO.File.WriteAllText(LangFile, JObj.ToString());

                MsgBox(Program.Lang.LangSaved, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (((data[1, e.RowIndex].Value ?? "").ToString().ToLower().Trim()) != ((data[2, e.RowIndex].Value ?? "").ToString().ToLower().Trim()) && !string.IsNullOrWhiteSpace((data[1, e.RowIndex].Value ?? "").ToString().ToLower().Trim()))
            {
                if ((data[1, e.RowIndex].Value ?? "").ToString().Contains("{") || (data[1, e.RowIndex].Value ?? "").ToString().Contains("{"))
                {
                    int count1 = (data[1, e.RowIndex].Value ?? "").ToString().Count(c => c == '{');
                    int count2 = (data[2, e.RowIndex].Value ?? "").ToString().Count(c => c == '{');
                    if (count1 != count2)
                    {
                        data[1, e.RowIndex].Style.BackColor = Program.Style.Colors.NotTranslatedColor;
                    }
                    else
                    {
                        data[1, e.RowIndex].Style.BackColor = data[2, e.RowIndex].Style.BackColor;
                    }
                }
                else
                {
                    data[1, e.RowIndex].Style.BackColor = data[2, e.RowIndex].Style.BackColor;
                }
            }
            else
            {
                data[1, e.RowIndex].Style.BackColor = Program.Style.Colors.NotTranslatedColor;
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProgressBar1.Visible)
            {
                ProgressBar2.Visible = TabControl1.SelectedIndex != 2;
            }
            else
            {
                ProgressBar2.Visible = false;
            }
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            data.Refresh();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Language-creation");
        }
    }
}