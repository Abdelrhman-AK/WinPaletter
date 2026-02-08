using FluentTransitions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Theme.Structures;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    public partial class Updates
    {
        private readonly DownloadManager DM = new();

        private static UpdatesInfo CurrentUpdateStatic;
        private UpdatesInfo CurrentUpdate => CurrentUpdateStatic;

        private bool _Shown;
        private bool Disturbed;

        public sealed class UpdatesInfo
        {
            public Settings.Structures.Updates.Channels Channel { get; private set; } = Settings.Structures.Updates.Channels.Stable;
            public Version Version { get; private set; }
            public float Size { get; private set; }
            public DateTime ReleaseDate { get; private set; }
            public string Url { get; private set; }

            public UpdatesInfo(string rawData)
            {
                if (!TryParse(rawData, out UpdatesInfo info)) throw new FormatException("Invalid update line format");

                Channel = info.Channel;
                Version = info.Version;
                Size = info.Size;
                ReleaseDate = info.ReleaseDate;
                Url = info.Url;
            }

            public static bool TryParse(string rawData, out UpdatesInfo info)
            {
                info = null;
                if (string.IsNullOrWhiteSpace(rawData) || rawData.StartsWith("#")) return false;

                string[] parts = rawData.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 5) return false;

                if (!Enum.TryParse<Settings.Structures.Updates.Channels>(parts[0], true, out var channel))
                    channel = Settings.Structures.Updates.Channels.Stable;

                if (!Version.TryParse(parts[1], out Version version)) return false;
                if (!float.TryParse(parts[2], out float size)) return false;
                if (!long.TryParse(parts[3], out long binDate)) return false;
                string url = parts[4];
                if (string.IsNullOrWhiteSpace(url)) return false;

                info = new UpdatesInfo
                {
                    Channel = channel,
                    Version = version,
                    Size = size,
                    ReleaseDate = DateTime.FromBinary(binDate),
                    Url = url
                };

                return true;
            }

            public UpdatesInfo() { }
        }

        public Updates()
        {
            InitializeComponent();
            DM.DownloadProgressChanged += UC_DownloadProgressChanged;
            DM.DownloadFileCompleted += UC_DownloadFileCompleted;
        }

        public void PreloadUpdateInfo(UpdatesInfo info)
        {
            if (info is null) return;

            // Store globally
            CurrentUpdateStatic = info;

            ApplyUpdateInfoUI(info);
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            Forms.Home.NotifyUpdates.Visible = false;

            if (!Program.IsNetworkAvailable)
            {
                Program.Animator.HideSync(tablessControl1);
                tablessControl1.SelectedIndex = 1;
                Program.Animator.ShowSync(tablessControl1);
                return;
            }
            else if (CurrentUpdate is null || CurrentUpdate.Version <= new Version(Program.Version))
            {
                await CheckForUpdatesAsync();
                return;
            }

            await PerformUpdateActionAsync(false);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Forms.Home.NotifyUpdates.Visible = false;

            if (CurrentUpdate is null)
            {
                await CheckForUpdatesAsync();
                return;
            }

            await PerformUpdateActionAsync(true);
        }

        private async Task CheckForUpdatesAsync()
        {
            SetBusyState(true);
            ResetUpdateUI();

            try
            {
                if (!Program.IsNetworkAvailable)
                {
                    Program.Animator.HideSync(tablessControl1);
                    tablessControl1.SelectedIndex = 1;
                    Program.Animator.ShowSync(tablessControl1);
                    return;
                }

                ProgressBar1.Value = 0;
                Program.Animator.ShowSync(ProgressBar1);
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Marquee;

                UpdatesInfo info = await FetchLatestUpdateAsync();

                if (info is not null/* && info.Version > Program.Version*/)
                {
                    ApplyUpdateInfoUI(info);
                }
                else
                {
                    ResetUpdateUI();
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.Throw(ex);
            }
            finally
            {
                ProgressBar1.Value = 0;
                Program.Animator.HideSync(ProgressBar1);
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Continuous;

                Label17.SetText(Text);
                SetBusyState(false);
            }
        }

        private async Task PerformUpdateActionAsync(bool saveAs)
        {
            Program.Animator.HideSync(ProgressBar1);

            if (saveAs)
            {
                using SaveFileDialog dlg = new()
                {
                    Filter = Program.Filters.EXE,
                    FileName = $"WinPaletter ({CurrentUpdate.Version})",
                    Title = Program.Localization.Strings.Extensions.SaveUpdateEXE
                };

                if (dlg.ShowDialog() != DialogResult.OK) return;

                SetBusyState(true);
                ProgressBar1.Value = 0;
                Program.Animator.ShowSync(ProgressBar1);
                await DM.DownloadFileAsync(CurrentUpdate.Url, dlg.FileName);
                return;
            }
            else
            {
                SetBusyState(true);
                ProgressBar1.Value = 0;
                Program.Animator.ShowSync(ProgressBar1);

                string currentExe = Assembly.GetExecutingAssembly().Location;
                string dir = Path.GetDirectoryName(currentExe);
                string tempExe = Path.Combine(dir, "WinPaletter.new.exe");

                try
                {
                    if (File.Exists(tempExe)) File.Delete(tempExe);

                    await DM.DownloadFileAsync(CurrentUpdate.Url, tempExe);

                    if (!File.Exists(tempExe) || new FileInfo(tempExe).Length == 0 || Disturbed)
                    {
                        MsgBox(Program.Localization.Strings.Updates.CouldNotDownload, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SafeSwapExecutable(tempExe);

                    // Causes different app versions not to load, so delete it
                    if (System.IO.File.Exists("WinPaletter.exe.config")) System.IO.File.Delete("WinPaletter.exe.config");

                    if (MsgBox(Program.Localization.Strings.Updates.Downloaded, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(currentExe);
                        Program.ForceExit();
                    }
                }
                catch (Exception ex)
                {
                    Forms.BugReport.Throw(ex);
                    RestoreOldExecutable();
                }
            }
        }

        private async Task<UpdatesInfo> FetchLatestUpdateAsync()
        {
            string response = await DM.ReadStringAsync(Links.Updates);
            string[] lines = response.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (!UpdatesInfo.TryParse(line, out UpdatesInfo info)) continue;
                if (info.Channel != Program.Settings.Updates.Channel) continue;
                return info;
            }

            return null;
        }

        private void ApplyUpdateInfoUI(UpdatesInfo info)
        {
            // Keep static copy
            CurrentUpdateStatic = info;

            Transition.With(release_lbl, nameof(release_lbl.Text), info.Version.ToString())
                      .With(channel_lbl, nameof(channel_lbl.Text), info.Channel == Settings.Structures.Updates.Channels.Stable ? Program.Localization.Strings.General.Stable : Program.Localization.Strings.General.Beta)
                      .With(releasedate_lbl, nameof(releasedate_lbl.Text), info.ReleaseDate.ToLongDateString())
                      .With(size_lbl, nameof(size_lbl.Text), $"{info.Size} {Program.Localization.Strings.General.MBSizeUnit}")
                      .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));

            button2.Enabled = info.Version > new Version(Program.Version);
            groupBox55.UseDecorationPattern = info.Version > new Version(Program.Version);
            pictureBox24.Visible = info.Version > new Version(Program.Version);

            Button1.Text = info.Version > new Version(Program.Version) ? Program.Localization.Strings.Updates.Update : Program.Localization.Strings.Updates.CheckForUpdates;
        }

        private void ResetUpdateUI()
        {
            Transition.With(release_lbl, nameof(release_lbl.Text), "0.0.0.0")
                      .With(channel_lbl, nameof(channel_lbl.Text), "-")
                      .With(releasedate_lbl, nameof(releasedate_lbl.Text), DateTime.MinValue.ToLongDateString())
                      .With(size_lbl, nameof(size_lbl.Text), 0.ToStringFileSize())
                      .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));

            groupBox55.UseDecorationPattern = false;
            button2.Enabled = false;
            pictureBox24.Visible = false;

            Button1.Text = Program.Localization.Strings.Updates.CheckForUpdates;
        }

        private void SetBusyState(bool busy)
        {
            Cursor = busy ? System.Windows.Forms.Cursors.AppStarting : System.Windows.Forms.Cursors.Default;
            Button1.Enabled = !busy;
            button2.Enabled = !busy;
            if (!busy)
            {
                ProgressBar1.Value = 0;
                Program.Animator.HideSync(ProgressBar1);
            }
        }

        private void SafeSwapExecutable(string newExe)
        {
            string currentExe = Assembly.GetExecutingAssembly().Location;
            string dir = Path.GetDirectoryName(currentExe);
            string backupExe = Path.Combine(dir, Program.GetUniqueFileName(dir, "oldWinPaletter.trash"));

            if (File.Exists(backupExe)) File.Delete(backupExe);

            File.Move(currentExe, backupExe);

            try
            {
                File.Move(newExe, currentExe);
            }
            catch
            {
                if (File.Exists(backupExe))
                    File.Move(backupExe, currentExe);
                throw;
            }
        }

        private void RestoreOldExecutable()
        {
            try
            {
                string exe = Assembly.GetExecutingAssembly().Location;
                string bak = Path.Combine(Path.GetDirectoryName(exe), "oldWinPaletter.trash");

                if (!File.Exists(exe) && File.Exists(bak))
                    File.Move(bak, exe);
            }
            catch { }
        }

        private void UC_DownloadProgressChanged(object sender, DownloadManager.DownloadProgressEventArgs e)
        {
            ProgressBar1.Value = (int)e.ProgressPercentage;
        }

        private void UC_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (Disturbed) return;
            ProgressBar1.Value = 0;
            SetBusyState(false);
            Program.Animator.HideSync(ProgressBar1);
        }

        private void Updates_Load(object sender, EventArgs e)
        {
            toggle1.Checked = Program.Settings.Updates.AutoCheck;

            release_lbl.Font = Fonts.ConsoleLarge;
            channel_lbl.Font = Fonts.ConsoleLarge;
            releasedate_lbl.Font = Fonts.ConsoleLarge;
            size_lbl.Font = Fonts.ConsoleLarge;

            // Restore previous fetched update if available
            if (CurrentUpdateStatic != null)
                ApplyUpdateInfoUI(CurrentUpdateStatic);
            else
                ResetUpdateUI();

            if (!Program.IsNetworkAvailable)
            {
                tablessControl1.SelectedIndex = 1;
            }
        }

        private void Updates_Shown(object sender, EventArgs e) => _Shown = true;

        private void Updates_FormClosing(object sender, FormClosingEventArgs e) => DisturbActions();

        private void Updates_FormClosed(object sender, FormClosedEventArgs e) => DisturbActions();

        private void DisturbActions()
        {
            if (!DM.IsBusy) return;
            Disturbed = true;
            DM.StopDownload(true);
            RestoreOldExecutable();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 0;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisturbActions();
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DisturbActions();
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (OS.WXP)
            {
                Program.SendCommand($"{SysPaths.Windows}\\Network Diagnostic\\xpnetdiag.exe", false);
            }
            else if (OS.WVista || OS.W7 || OS.W8x || OS.W10)
            {
                Process.Start($"{SysPaths.System32}\\msdt.exe", $"-path {SysPaths.Windows}\\diagnostics\\system\\networking -ep NetworkDiagnosticsPNI");
            }
            else
            {
                Program.SendCommand($"{SysPaths.Explorer} \"ms-contact-support:///?ActivationType=NetworkDiagnostics\"", false);
            }
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (!_Shown) return;
            Program.Settings.Updates.AutoCheck = toggle1.Checked;
            Program.Settings.Updates.Save();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Releases);
        }

        private void noNetworkPanel1_RetryClicked(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 0;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void noNetworkPanel1_CloseClicked(object sender, EventArgs e)
        {
            DisturbActions();
            Close();
        }
    }
}