using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme;

namespace WinPaletter
{
    public partial class LogonUI
    {
        Bitmap back_unblurred = null;
        Bitmap back_blurred = null;

        /// <summary>
        /// Represents the LogonUI structure for version 10.x of the theme.
        /// </summary>
        /// <remarks>This field provides access to the LogonUI configuration specific to version 10.x of
        /// the theme. It is used to define and manage the appearance and behavior of the logon user
        /// interface.</remarks>
        public Theme.Structures.LogonUI10x LogonUI10x;

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.LogonUI_10x);
        }

        public LogonUI()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            back_unblurred?.Dispose();
            back_blurred?.Dispose();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Manager.Source.File, dlg.FileName))
                    {
                        if (Program.WindowStyle == PreviewHelpers.WindowStyle.W12)
                            LoadFromTM(TMx.LogonUI12);
                        else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W11)
                            LoadFromTM(TMx.LogonUI11);
                        else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W10)
                            LoadFromTM(TMx.LogonUI10);
                        else
                            LoadFromTM(TMx.LogonUI12);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            using (Manager TMx = new(Manager.Source.Registry))
            {
                if (Program.WindowStyle == PreviewHelpers.WindowStyle.W12)
                    LoadFromTM(TMx.LogonUI12);
                else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W11)
                    LoadFromTM(TMx.LogonUI11);
                else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W10)
                    LoadFromTM(TMx.LogonUI10);
                else
                    LoadFromTM(TMx.LogonUI12);
            }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(Program.WindowStyle))
            {
                if (Program.WindowStyle == PreviewHelpers.WindowStyle.W12)
                    LoadFromTM(TMx.LogonUI12);
                else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W11)
                    LoadFromTM(TMx.LogonUI11);
                else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W10)
                    LoadFromTM(TMx.LogonUI10);
                else
                    LoadFromTM(TMx.LogonUI12);
            }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(ref LogonUI10x);
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Manager TMx = new(Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Manager.Source.File, filename);
                }

                ApplyToTM(ref LogonUI10x);

                if (Program.WindowStyle == PreviewHelpers.WindowStyle.W12)
                {
                    TMx.LogonUI12.Apply("12");
                    ApplyToTM(ref Program.TM.LogonUI12);
                    ApplyToTM(ref Program.TM_Original.LogonUI12);
                }
                else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W11)
                {
                    TMx.LogonUI11.Apply("11");
                    ApplyToTM(ref Program.TM.LogonUI11);
                    ApplyToTM(ref Program.TM_Original.LogonUI11);
                }
                else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W10)
                {
                    TMx.LogonUI10.Apply("10x");
                    ApplyToTM(ref Program.TM.LogonUI10);
                    ApplyToTM(ref Program.TM_Original.LogonUI10);
                }
                else
                {
                    TMx.LogonUI12.Apply("12");
                    ApplyToTM(ref Program.TM.LogonUI12);
                    ApplyToTM(ref Program.TM_Original.LogonUI12);
                }
            }

            Cursor = Cursors.Default;
        }

        private void LogonUI_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.LockScreen,
                Enabled = LogonUI10x.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
            };

            LoadData(data);

            tabs_preview_1.SelectedIndex = Program.WindowStyle == PreviewHelpers.WindowStyle.W10 ? 1 : 0;
            pictureBox1.Image = Program.WindowStyle == PreviewHelpers.WindowStyle.W10 ? Win10Preview.LockScreen : Win11Preview.LockScreen;

            label1.Text = DateTime.Now.ToString("h:mm");
            label2.Text = DateTime.Now.ToString("dddd, MMMM d");
            label3.Text = DateTime.Now.ToString("h:mm");
            label4.Text = DateTime.Now.ToString("dddd, MMMM d");

            if (!Fonts.Exists("Segoe UI Variable Small Semibol"))
            {
                label1.Font = new("Segoe UI", label1.Font.Size, label1.Font.Style);
                label2.Font = new("Segoe UI", label2.Font.Size, label2.Font.Style);
            }

            back_unblurred = CaptureLockScreen();
            back_blurred = back_unblurred.Blur(7).Noise(BitmapExtensions.NoiseMode.Acrylic, 0.7f);

            LoadFromTM(LogonUI10x);

            // Make them all black after ApplyStyle(this);
            for (int i = 0; i <= tabs_preview_1.TabCount - 1; i++) { tabs_preview_1.TabPages[i].BackColor = Color.Black; }

            UpdatePreview();

            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
        }

        public void LoadFromTM(Theme.Structures.LogonUI10x logonUI)
        {
            AspectEnabled = logonUI.Enabled;

            LogonUI_Acrylic_Toggle.Checked = !logonUI.DisableAcrylicBackgroundOnLogon;
            LogonUI_Background_Toggle.Checked = !logonUI.DisableLogonBackgroundImage;
            LogonUI_Lockscreen_Toggle.Checked = logonUI.NoLockScreen;
        }

        public void ApplyToTM(ref Theme.Structures.LogonUI10x logonUI)
        {
            logonUI.Enabled = AspectEnabled;

            logonUI.DisableAcrylicBackgroundOnLogon = !LogonUI_Acrylic_Toggle.Checked;
            logonUI.DisableLogonBackgroundImage = !LogonUI_Background_Toggle.Checked;
            logonUI.NoLockScreen = LogonUI_Lockscreen_Toggle.Checked;
        }

        private void UpdatePreview()
        {
            tabs_preview_1.TabPages[0].BackgroundImage = LogonUI_Background_Toggle.Checked ? back_unblurred : null;
            tabs_preview_1.TabPages[1].BackgroundImage = LogonUI_Background_Toggle.Checked ? back_unblurred : null;
            tabs_preview_1.TabPages[2].BackgroundImage = LogonUI_Background_Toggle.Checked ? LogonUI_Acrylic_Toggle.Checked ? back_blurred : back_unblurred : null;
        }

        Bitmap CaptureLockScreen()
        {
            string mostRecentFile = null;

            string defaultLockScreen = GetReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization", "LockScreenImage", $"{SysPaths.Windows}\\Web\\Screen\\img100.jpg") as string;

            // Get the path to the current user's lock screen image
            string lockScreenPath = Path.Combine(SysPaths.LocalAppData, "Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets");

            if (Directory.Exists(lockScreenPath))
            {
                // Get the list of files in the lock screen folder
                string[] files = Directory.GetFiles(lockScreenPath);

                if (files.Count() > 0)
                {
                    // Find the most recently accessed File (assuming it's the lock screen image)
                    mostRecentFile = files.OrderByDescending(File.GetLastAccessTime).FirstOrDefault();
                }
            }

            if (mostRecentFile != null && File.Exists(mostRecentFile))
            {
                return BitmapMgr.Load(mostRecentFile).Resize(tabs_preview_1.Size);
            }
            else if (File.Exists(defaultLockScreen))
            {
                return BitmapMgr.Load(defaultLockScreen).Resize(tabs_preview_1.Size);
            }
            else
            {
                return Program.Wallpaper;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!LogonUI_Lockscreen_Toggle.Checked)
            {
                tabs_preview_1.SelectedIndex = tabs_preview_1.SelectedIndex == 0 || tabs_preview_1.SelectedIndex == 1 ? tabs_preview_1.TabCount - 1 : Program.WindowStyle == PreviewHelpers.WindowStyle.W10 ? 1 : 0;
            }
            else
            {
                tabs_preview_1.SelectedIndex = tabs_preview_1.TabCount - 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("h:mm");
            label2.Text = DateTime.Now.ToString("dddd, MMMM d");
            label3.Text = DateTime.Now.ToString("h:mm");
            label4.Text = DateTime.Now.ToString("dddd, MMMM d");
        }

        private void LogonUI_Background_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void LogonUI_Acrylic_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void LogonUI_Lockscreen_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (LogonUI_Lockscreen_Toggle.Checked && tabs_preview_1.SelectedIndex != tabs_preview_1.TabCount - 1) { tabs_preview_1.SelectedIndex = tabs_preview_1.TabCount - 1; }
        }
    }
}