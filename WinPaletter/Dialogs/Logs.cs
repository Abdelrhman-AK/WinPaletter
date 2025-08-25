using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;

namespace WinPaletter.Dialogs
{
    public partial class Logs : Form
    {
        public Logs()
        {
            InitializeComponent();
        }

        BindingList<SerilogJsonLogEntry> entries = [];
        BindingList<SerilogJsonLogEntry> filteredLogs = [];
        BindingSource bindingSource = [];
        SearchManager<SerilogJsonLogEntry> searchManager;

        private void Logs_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();

            data.Font = Fonts.ConsoleMedium;
            textBox1.Font = Fonts.ConsoleMedium;
            label4.Font = Fonts.ConsoleMedium;
            label5.Font = Fonts.ConsoleMedium;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { InitialDirectory = SysPaths.Logs, Filter = Program.Filters.JSON + "|" + Program.Filters.Text + "|" + Program.Filters.All })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    entries.Clear();

                    using (FileStream fs = new(dlg.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new(fs))
                    {
                        string line = string.Empty;
                        while ((line = sr.ReadLine()) != null)
                        {
                            try
                            {
                                SerilogJsonLogEntry entry = JsonConvert.DeserializeObject<SerilogJsonLogEntry>(line);
                                if (entry != null) entries.Add(entry);
                            }
                            catch /*(JsonException ex)*/ // Ignore parsing non-JSON lines
                            { }
                        }
                    }

                    ProcessEntries(entries);

                    filteredLogs = [.. entries.ToList()];
                    ApplyFilters();

                    bindingSource.DataSource = filteredLogs;
                    data.DataSource = bindingSource;

                    GroupBox8.Visible = true;
                    Label9.Text = dlg.FileName;
                    splitContainer1.Visible = true;

                    Cursor = Cursors.Default;
                }
            }
        }

        void ProcessEntries(BindingList<SerilogJsonLogEntry> entries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                SerilogJsonLogEntry entry = entries[i];

                switch (entry.Level)
                {
                    case "Information":
                        entry.Icon = Notifications.Info;
                        break;
                    case "Error":
                        entry.Icon = Notifications.Error;
                        break;
                    case "Fatal":
                        entry.Icon = Notifications.Error;
                        break;
                    case "Warning":
                        entry.Icon = Notifications.Warning;
                        break;
                    case "Debug":
                        entry.Icon = Notifications.DLL;
                        break;
                    case "Verbose":
                        entry.Icon = Notifications.DLL;
                        break;
                    default:
                        entry.Icon = Notifications.Info;
                        break;
                }
                entries[i] = entry;
            }

            int infoCount = entries.Count(x => x.Level != "Error");
            int errorCount = entries.Count(x => x.Level == "Error");

            progressBar1.Maximum = entries.Count;
            progressBar1.Value = infoCount;

            progressBar2.Maximum = entries.Count;
            progressBar2.Value = errorCount;

            label1.Text = $"{infoCount} info";
            label2.Text = $"{errorCount} errors";

            searchManager = new(data, TextBox8, Program.Style.Schemes.Tertiary.Colors.ForeColor_Accent, Program.Style.Schemes.Tertiary.Colors.Back_Checked_Hover, entries);
        }

        private void data_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected && e?.Row is not null && e.Row?.DataBoundItem is not null)
            {
                SerilogJsonLogEntry entry = (e.Row.DataBoundItem as SerilogJsonLogEntry);

                label4.Text = entry.Timestamp.ToString();
                label5.Text = entry.Level;
                textBox1.Text = entry.MessageTemplate;
            }
            else
            {
                label4.Text = string.Empty;
                label5.Text = string.Empty;
                textBox1.Text = string.Empty;
            }
        }

        private void Logs_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        private void ApplyFilters()
        {
            Cursor = Cursors.AppStarting;

            data.SuspendLayout();
            data.Visible = false;
            data.Enabled = false;

            var selectedLevels = new List<string>();
            if (checkBox1.Checked) selectedLevels.Add("Information");
            if (checkBox2.Checked) selectedLevels.Add("Error");
            if (checkBox3.Checked) selectedLevels.Add("Fatal");
            if (checkBox4.Checked) selectedLevels.Add("Warning");
            if (checkBox5.Checked) selectedLevels.Add("Debug");
            if (checkBox6.Checked) selectedLevels.Add("Verbose");

            var result = entries
                .Where(log => selectedLevels.Contains(log.Level))
                .ToList();

            filteredLogs.Clear();
            foreach (var item in result)
                filteredLogs.Add(item);

            data.Enabled = true;
            data.Visible = true;
            data.ResumeLayout();

            Cursor = Cursors.Default;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (data.Rows[e.RowIndex].DataBoundItem is SerilogJsonLogEntry entry)
            {
                if (entry.Level == "Error" || entry.Level == "Fatal")
                {
                    e.CellStyle.BackColor = Program.Style.Schemes.Secondary.Colors.Back_Checked;
                    e.CellStyle.ForeColor = Program.Style.Schemes.Secondary.Colors.ForeColor_Accent;
                }
                else if (entry.Level == "Warning")
                {
                    e.CellStyle.BackColor = Program.Style.Schemes.Tertiary.Colors.Back_Checked;
                    e.CellStyle.ForeColor = Program.Style.Schemes.Tertiary.Colors.ForeColor_Accent;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Label9.Text))
            {
                using (SaveFileDialog dlg = new() { Filter = Program.Filters.Text })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new(dlg.FileName))
                        {
                            foreach (var entry in filteredLogs)
                            {
                                sw.WriteLine($"{entry.Timestamp} [{entry.Level}] {entry.MessageTemplate}");
                            }
                        }
                        Program.ToolTip.Show((UI.WP.Button)sender, Program.Lang.Strings.General.Done, string.Format(Program.Lang.Strings.Messages.LogSaved, dlg.FileName), null, new Point(2, -((UI.WP.Button)sender).Height - 5));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Label9.Text))
            {
                using (FileStream fs = new(Label9.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new(fs))
                {
                    string content = sr.ReadToEnd();
                    Clipboard.SetText(content);
                    Program.ToolTip.Show((UI.WP.Button)sender, Program.Lang.Strings.General.Done, Program.Lang.Strings.Messages.LogToClipboard, null, new Point(2, -((UI.WP.Button)sender).Height - 5));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(Label9.Text))
            {
                string extension = Path.GetExtension(Label9.Text).ToLowerInvariant();

                using (SaveFileDialog dlg = new() { Filter = $"*{extension}|*{extension}" })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(Label9.Text, dlg.FileName, true);
                        Program.ToolTip.Show((UI.WP.Button)sender, Program.Lang.Strings.General.Done, string.Format(Program.Lang.Strings.Messages.LogCopied, dlg.FileName), null, new Point(2, -((UI.WP.Button)sender).Height - 5));
                    }
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class SerilogJsonLogEntry
    {
        /// <summary>
        /// Represents the icon associated with the log entry.
        /// </summary>
        public Bitmap Icon { get; set; }

        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("Level")]
        public string Level { get; set; }

        [JsonProperty("MessageTemplate")]
        public string MessageTemplate { get; set; }

        //[JsonProperty("RenderedMessage")]
        //public string RenderedMessage { get; set; }

        //[JsonProperty("TraceId")]
        //public string TraceId { get; set; }

        //[JsonProperty("SpanId")]
        //public string SpanId { get; set; }

        //[JsonProperty("Exception")]
        //public string Exception { get; set; }

        //// Properties is a dictionary of name → value, where value can be anything
        //[JsonProperty("Properties")]
        //public Dictionary<string, JToken> Properties { get; set; }

        //[JsonProperty("Renderings")]
        //public Dictionary<string, List<RenderingInfo>> Renderings { get; set; }
    }

    //public class RenderingInfo
    //{
    //    [JsonProperty("Format")]
    //    public string Format { get; set; }

    //    [JsonProperty("Rendering")]
    //    public string Rendering { get; set; }
    //}
}
