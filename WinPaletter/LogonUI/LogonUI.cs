using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

            tabs_preview_1.SelectedIndex = Program.WindowStyle == PreviewHelpers.WindowStyle.W10 ? 1 : 0;
            pictureBox1.Image = Program.WindowStyle == PreviewHelpers.WindowStyle.W10 ? Assets.Win10Preview.LockScreen : Assets.Win11Preview.LockScreen;

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
            string mostRecentFile = null;

            string defaultLockScreen = GetReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization", "LockScreenImage", $"{PathsExt.Windows}\\Web\\Screen\\img100.jpg") as string;

            // Get the path to the current user's lock screen image
            string lockScreenPath = System.IO.Path.Combine(PathsExt.LocalAppData, "Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets");

            if (System.IO.Directory.Exists(lockScreenPath))
            {
                // Get the list of files in the lock screen folder
                string[] files = System.IO.Directory.GetFiles(lockScreenPath);

                if (files.Count() > 0)
                {
                    // Find the most recently accessed file (assuming it's the lock screen image)
                    mostRecentFile = files.OrderByDescending(System.IO.File.GetLastAccessTime).FirstOrDefault();
                }
            }

            if (mostRecentFile != null && System.IO.File.Exists(mostRecentFile))
            {
                return Bitmap_Mgr.Load(mostRecentFile).Resize(tabs_preview_1.Size);
            }
            else if (System.IO.File.Exists(defaultLockScreen))
            {
                return Bitmap_Mgr.Load(defaultLockScreen).Resize(tabs_preview_1.Size);
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