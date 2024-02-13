using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class LogonUI
    {
        Bitmap back_unblurred = null;
        Bitmap back_blurred = null;
        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-LogonUI-screen#windows-11--10");
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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Filter_OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                LoadFromTM(TMx);
            }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle))
            {
                LoadFromTM(TMx);
            }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                TMx.LogonUI10x.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void LogonUI_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.LockScreen,
                Enabled = Program.TM.LogonUI10x.Enabled,
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

            tabs_preview_1.SelectedIndex = OS.W11 ? 0 : 1;
            pictureBox1.Image = OS.W11 ? Assets.Win11Preview.LockScreen : Assets.Win10Preview.LockScreen;

            label1.Text = DateTime.Now.ToString("h:mm");
            label2.Text = DateTime.Now.ToString("dddd, MMMM d");
            label3.Text = DateTime.Now.ToString("h:mm");
            label4.Text = DateTime.Now.ToString("dddd, MMMM d");

            if (!Fonts.Exists("Segoe UI Variable"))
            {
                label1.Font = new("Segoe UI", label1.Font.Size, label1.Font.Style);
                label2.Font = new("Segoe UI", label2.Font.Size, label2.Font.Style);
            }

            back_unblurred = CaptureLockScreen();
            back_blurred = back_unblurred.Blur(9).Noise(BitmapExtensions.NoiseMode.Acrylic, 0.7f);

            LoadFromTM(Program.TM);

            // Make them all black after ApplyStyle(this);
            for (int i = 0; i <= tabs_preview_1.TabCount - 1; i++) { tabs_preview_1.TabPages[i].BackColor = Color.Black; }

            UpdatePreview();

            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            LogonUI_Acrylic_Toggle.Checked = !TM.LogonUI10x.DisableAcrylicBackgroundOnLogon;
            LogonUI_Background_Toggle.Checked = !TM.LogonUI10x.DisableLogonBackgroundImage;
            LogonUI_Lockscreen_Toggle.Checked = TM.LogonUI10x.NoLockScreen;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.LogonUI10x.DisableAcrylicBackgroundOnLogon = !LogonUI_Acrylic_Toggle.Checked;
            TM.LogonUI10x.DisableLogonBackgroundImage = !LogonUI_Background_Toggle.Checked;
            TM.LogonUI10x.NoLockScreen = LogonUI_Lockscreen_Toggle.Checked;
        }

        private void UpdatePreview()
        {
            tabs_preview_1.TabPages[0].BackgroundImage = LogonUI_Background_Toggle.Checked ? back_unblurred : null;
            tabs_preview_1.TabPages[1].BackgroundImage = LogonUI_Background_Toggle.Checked ? back_unblurred : null;
            tabs_preview_1.TabPages[2].BackgroundImage = LogonUI_Background_Toggle.Checked ? LogonUI_Acrylic_Toggle.Checked ? back_blurred : back_unblurred : null;
        }

        Bitmap CaptureLockScreen()
        {
            // Get the path to the current user's lock screen image
            string lockScreenPath = Path.Combine(PathsExt.LocalAppData, "Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets");

            // Get the list of files in the lock screen folder
            string[] files = Directory.GetFiles(lockScreenPath);

            // Find the most recent file (assuming it's the lock screen image)
            string mostRecentFile = GetMostRecentFile(files);

            if (mostRecentFile != null && System.IO.File.Exists(mostRecentFile))
            {
                return Bitmap_Mgr.Load(mostRecentFile).Resize(tabs_preview_1.Size);
            }
            else if (System.IO.File.Exists(PathsExt.Windows + "\\Web\\Screen\\img100.jpg"))
            { 
                return Bitmap_Mgr.Load(PathsExt.Windows + "\\Web\\Screen\\img100.jpg").Resize(tabs_preview_1.Size);
            }
            else
            {
                return Program.Wallpaper;
            }
        }

        static string GetMostRecentFile(string[] files)
        {
            DateTime lastWriteTime = DateTime.MinValue;
            string mostRecentFile = null;

            foreach (string file in files)
            {
                DateTime writeTime = File.GetLastWriteTime(file);

                if (writeTime > lastWriteTime)
                {
                    lastWriteTime = writeTime;
                    mostRecentFile = file;
                }
            }

            return mostRecentFile;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!LogonUI_Lockscreen_Toggle.Checked)
            {
                tabs_preview_1.SelectedIndex = tabs_preview_1.SelectedIndex == 0 || tabs_preview_1.SelectedIndex == 1 ? tabs_preview_1.TabCount - 1 : OS.W11 ? 0 : 1;
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