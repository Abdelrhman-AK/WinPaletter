using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Updates
    {
        private readonly DownloadManager DM = new();

        /// <summary>
        /// The URL of the update executable.
        /// </summary>
        public string url = null;

        /// <summary>
        /// The version of the update.
        /// </summary>
        public string ver;

        private int StableInt, BetaInt, UpdateChannel;
        private string OldName;

        /// <summary>
        /// The size of the update executable.
        /// </summary>
        public decimal UpdateSize;

        /// <summary>
        /// The release date of the update.
        /// </summary>
        public DateTime ReleaseDate;
        private bool _Shown = false;

        /// <summary>
        /// The list of updates.
        /// </summary>
        public List<string> ls = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="Updates"/> class.
        /// </summary>
        public Updates()
        {
            InitializeComponent();
            DM.DownloadProgressChanged += UC_DownloadProgressChanged;
            DM.DownloadFileCompleted += UC_DownloadFileCompleted;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            // Reset updates flag in MainForm
            Forms.Home.NotifyUpdates.Visible = false;

            // That means that MainForm didn't check for updates, so we need to check here
            if (url is null)
            {
                Cursor = Cursors.AppStarting;

                Program.Animator.HideSync(AlertBox2, true);
                Program.Animator.HideSync(Button1, true);
                Program.Animator.HideSync(Button3, true);
                Program.Animator.HideSync(Panel1, true);
                ProgressBar1.Visible = false;
                ProgressBar1.Value = 0;

                StableInt = 0;
                BetaInt = 0;
                UpdateChannel = 0;

                try
                {
                    if (Program.IsNetworkAvailable)
                    {
                        Label17.SetText(Program.Lang.Strings.General.Checking);

                        string response = await DM.ReadStringAsync(Links.Updates);

                        // Split the response into lines
                        ls = [.. response.Split('\n')];

                        foreach (string updateInfo in ls.Where(update => !string.IsNullOrEmpty(update) && !update.StartsWith("#")))
                        {
                            string[] updateParts = updateInfo.Split(' ');

                            if (updateParts.Length >= 5)
                            {
                                if (updateParts[0] == "Stable") StableInt = ls.IndexOf(updateInfo);
                                if (updateParts[0] == "Beta") BetaInt = ls.IndexOf(updateInfo);
                            }
                        }

                        // Get the update channel.
                        UpdateChannel = Program.Settings.Updates.Channel == Settings.Structures.Updates.Channels.Stable ? StableInt : BetaInt;

                        // Get the version of the update (it is the second element in the line) and compare it with the current version.
                        ver = ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[1];

                        if (new Version(ver) > new Version(Program.Version))
                        {
                            // Get the URL of the update executable, the size of the update executable and the release date of the update.
                            url = ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[4];
                            UpdateSize = Conversions.ToDecimal(ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[2]);
                            ReleaseDate = DateTime.FromBinary(Conversions.ToLong(ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[3]));

                            // Hide the update information.
                            Label7.Text = $"{UpdateSize} {Program.Lang.Strings.General.MBSizeUnit}";
                            Label9.Text = ReleaseDate.ToLongDateString();

                            LinkLabel3.Visible = true;

                            Program.Animator.Show(Panel1, true);
                            Button1.Text = Program.Lang.Strings.General.DoAction;
                            AlertBox2.Text = $"{Program.Lang.Strings.Updates.NewUpdate}. {Program.Lang.Strings.General.Version} {ver}";
                            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Indigo;
                        }
                        else
                        {
                            // No update available.
                            Label7.Text = string.Empty;
                            Label9.Text = string.Empty;
                            url = null;
                            Button1.Text = Program.Lang.Strings.Updates.CheckForUpdates;
                            AlertBox2.Text = string.Format(Program.Lang.Strings.Updates.NoUpdateAvailable);
                            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Success;
                        }

                        Label17.SetText(Text);
                    }
                    else
                    {
                        //HandleNoNetwork();
                    }
                }
                catch (Exception ex) // An error occurred while checking for updates.
                {
                    Forms.BugReport.ThrowError(ex);
                }

                Program.Animator.Show(AlertBox2, true);
                Program.Animator.Show(Button1, true);
                Program.Animator.ShowSync(Button3, true);

                Cursor = Cursors.Default;
            }
            else
            {
                // There is an update available that is already checked in MainForm, so we need to download it.
                ProgressBar1.Visible = false;
                ProgressBar1.Value = 0;

                if (RadioButton1.Checked)
                {
                    Button1.Enabled = false;
                    Panel1.Enabled = false;
                    ProgressBar1.Visible = true;
                    OldName = System.Reflection.Assembly.GetExecutingAssembly().Location;

                    try
                    {
                        if (System.IO.File.Exists("oldWinpaletter.trash")) FileSystem.Kill("oldWinpaletter.trash");
                        if (System.IO.File.Exists("oldWinpaletter_2.trash")) FileSystem.Kill("oldWinpaletter_2.trash");
                    }
                    catch { } // Couldn't delete old executable files (may be in use)

                    System.IO.File.Move(OldName, "oldWinpaletter.trash");
                    await DM.DownloadFileAsync(url, OldName);
                }

                // Download the update executable.
                if (RadioButton2.Checked)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.EXE, FileName = $"WinPaletter ({ver})", Title = Program.Lang.Strings.Extensions.SaveUpdateEXE })
                    {
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            Panel1.Enabled = false;
                            Button1.Enabled = false;
                            ProgressBar1.Visible = true;
                            await DM.DownloadFileAsync(url, dlg.FileName);
                        }
                        else
                        {
                            ProgressBar1.Visible = false;
                        }
                    }
                }

                // Open the download page, if the user wants to download the update manually.
                if (RadioButton3.Checked)
                {
                    Process.Start(Links.Releases);
                }

            }

        }

        private void Updates_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            LinkLabel3.Visible = false;

            string format = Program.Lang.Information.RightToLeft ? "{1}: {0}" : "{0} {1}";
            Label3.Text = string.Format(format, (Program.Settings.Updates.Channel == Settings.Structures.Updates.Channels.Stable) ? Program.Lang.Strings.General.Stable : Program.Lang.Strings.General.Beta, Program.Lang.Strings.General.Channel);

            CheckBox1.Checked = Program.Settings.Updates.AutoCheck;

            _Shown = false;

            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox2.Visible = false;

            Panel1.Visible = false;
            Label7.Text = string.Empty;
            Label9.Text = string.Empty;
            url = null;

            Button1.Enabled = true;
            Panel1.Enabled = true;
            Button1.Text = Program.Lang.Strings.Updates.CheckForUpdates;
            Label2.Text = Program.Version;
            Label2.Font = Fonts.ConsoleMedium;
            Label7.Font = Fonts.ConsoleMedium;
            Label9.Font = Fonts.ConsoleMedium;

            // Process data from MainForm if it is available
            if (ls.Count > 0)
            {
                StableInt = 0;
                BetaInt = 0;
                UpdateChannel = 0;

                foreach (string updateInfo in ls.Where(update => !string.IsNullOrEmpty(update) && !update.StartsWith("#")))
                {
                    string[] updateParts = updateInfo.Split(' ');

                    if (updateParts.Length >= 5)
                    {
                        if (updateParts[0] == "Stable") StableInt = ls.IndexOf(updateInfo);
                        if (updateParts[0] == "Beta") BetaInt = ls.IndexOf(updateInfo);
                    }
                }

                UpdateChannel = Program.Settings.Updates.Channel == Settings.Structures.Updates.Channels.Stable ? StableInt : BetaInt;

                // Get the version of the update (it is the second element in the line) and compare it with the current version.
                ver = ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[1];
                if (new Version(ver) > new Version(Program.Version))
                {
                    url = ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[4];
                    UpdateSize = Conversions.ToDecimal(ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[2]);
                    ReleaseDate = DateTime.FromBinary(Conversions.ToLong(ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[3]));

                    Label7.Text = $"{UpdateSize} {Program.Lang.Strings.General.MBSizeUnit}";
                    Label9.Text = $"{ReleaseDate.ToLongDateString()} {ReleaseDate.ToLongTimeString()}";

                    LinkLabel3.Visible = true;

                    Program.Animator.Show(Panel1, true);
                    Button1.Text = Program.Lang.Strings.General.DoAction;
                    AlertBox2.Text = $"{Program.Lang.Strings.Updates.NewUpdate}. {Program.Lang.Strings.General.Version} {ver}";
                    AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Indigo;

                    Program.Animator.Show(AlertBox2, true);
                    Program.Animator.ShowSync(Button1, true);
                }
                else
                {
                    Label7.Text = string.Empty;
                    Label9.Text = string.Empty;
                    url = null;
                    Button1.Text = Program.Lang.Strings.Updates.CheckForUpdates;
                    AlertBox2.Text = string.Format(Program.Lang.Strings.Updates.NoUpdateAvailable);
                    AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Success;
                }
            }

            // Hide a warning if the user is using Windows WXP or Windows Vista as they don't support TLS 1.2.
            if (OS.WXP || OS.WVista)
            {
                AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
                AlertBox2.Visible = true;
                AlertBox2.Text = string.Format(Program.Lang.Strings.Updates.NoTLS12, OS.WXP ? Program.Lang.Strings.Windows.WXP : Program.Lang.Strings.Windows.WVista);
            }
        }

        private void Label3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.SettingsX);
        }

        private void Updates_Shown(object sender, EventArgs e)
        {
            _Shown = true;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.Settings.Updates.AutoCheck = CheckBox1.Checked;
                Program.Settings.Updates.Save();
            }
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Links.Changelog);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UC_DownloadProgressChanged(object sender, DownloadManager.DownloadProgressEventArgs e)
        {
            ProgressBar1.Value = (int)e.ProgressPercentage;
        }

        private void UC_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Button1.Enabled = true;
            Panel1.Enabled = true;
            ProgressBar1.Visible = false;
            ProgressBar1.Value = 0;
            if (RadioButton2.Checked)
                MsgBox(Program.Lang.Strings.Updates.Downloaded, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (RadioButton1.Checked & !Disturbed)
            {
                Process.Start(OldName);
                Program.ForceExit();
            }
        }

        private void Updates_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisturbActions();
            ls.Clear();
        }

        private void Updates_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisturbActions();
            ls.Clear();
        }

        private bool Disturbed = false;

        /// <summary>
        /// Stop the download if the user closes the form.
        /// </summary>
        public void DisturbActions()
        {
            if (DM.IsBusy)
            {
                Disturbed = true;
                DM.StopDownload();

                // Restore the old executable file if the user cancels the download.
                if (RadioButton1.Checked)
                {
                    try
                    {
                        if (System.IO.File.Exists("oldWinpaletter_2.trash")) FileSystem.Kill("oldWinpaletter_2.trash");
                    }
                    catch { } // Couldn't delete old executable files (may be in use)

                    System.IO.File.Move(OldName, "oldWinpaletter_2.trash");
                    System.IO.File.Move("oldWinpaletter.trash", OldName.Split('\\').Last());

                    try
                    {
                        if (System.IO.File.Exists("oldWinpaletter_2.trash")) FileSystem.Kill("oldWinpaletter_2.trash");
                        if (System.IO.File.Exists("oldWinpaletter.trash")) FileSystem.Kill("oldWinpaletter.trash");
                    }
                    catch { } // Couldn't delete old executable files (may be in use)
                }
            }
        }
    }
}