using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class Home : Form
    {
        private bool RaiseUpdate = false;
        private string ver = string.Empty;
        private int StableInt, BetaInt, UpdateChannel;
        private int ChannelFixer;
        private List<string> Updates_ls = new();
        public string file = string.Empty;

        public Size oldMainFormMinSize;

        public Home()
        {
            InitializeComponent();
            using (MainForm formIcon = new()) { Icon = formIcon.Icon; }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            Forms.MainForm.LoggingOff = false;

            NotifyUpdates.Icon = Icon;
            groupBox1.UseSharpStyle = true;
            labelAlt1.Font = new(Fonts.Title, labelAlt1.Font.Size, labelAlt1.Font.Style);
            labelAlt2.Font = new(Fonts.Title, labelAlt2.Font.Size, labelAlt2.Font.Style);
            labelAlt3.Font = Fonts.Console;

            oldMainFormMinSize = Forms.MainForm.MinimumSize;

            if (!Program.Elevated) apply_btn.Image = Properties.Resources.WP_Admin;
            flowLayoutPanel3.DoubleBuffer();

            processCompactMode();
            LoadOSData();
            LoadData();
            LoadFromTM(Program.TM);

            foreach (UI.WP.Button button in titlebarExtender2.GetAllControls().OfType<UI.WP.Button>())
            {
                button.MouseEnter += (s, e) => FluentTransitions.Transition.With(tip_label, nameof(tip_label.Text), (s as UI.WP.Button).Tag ?? string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                button.MouseLeave += (s, e) => FluentTransitions.Transition.With(tip_label, nameof(tip_label.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }

            foreach (UI.WP.Button button in groupBox1.GetAllControls().OfType<UI.WP.Button>())
            {
                button.MouseEnter += (s, e) => FluentTransitions.Transition.With(tip_label, nameof(tip_label.Text), (s as UI.WP.Button).Tag ?? string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                button.MouseLeave += (s, e) => FluentTransitions.Transition.With(tip_label, nameof(tip_label.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }

            foreach (UI.WP.Card card in flowLayoutPanel1.Controls)
            {
                card.MouseEnter += (s, e) => FluentTransitions.Transition.With(panel1, nameof(panel1.BackColor), Program.Style.DarkMode ? (s as UI.WP.Card).Color.Dark(0.7f) : (s as UI.WP.Card).Color.CB(0.7f)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                card.MouseLeave += (s, e) => FluentTransitions.Transition.With(panel1, nameof(panel1.BackColor), BackColor).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Settings.General.CompactAspects = !Program.Settings.General.CompactAspects;
            processCompactMode();
        }

        private void processCompactMode()
        {
            foreach (UI.WP.Card card in flowLayoutPanel1.Controls)
            {
                card.Compact = Program.Settings.General.CompactAspects;
                card.Size = card.Compact ? new(144, 130) : new(277, 130);
            }

            Forms.MainForm.MinimumSize = Program.Settings.General.CompactAspects ? new(925, 660) : oldMainFormMinSize;

            button1.ImageGlyph = Program.Settings.General.CompactAspects ? Properties.Resources.Glyph_Expand : Properties.Resources.Glyph_Compact;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;

            if (!Forms.MainForm.LoggingOff)
            {
                if (Forms.Home.Parent is TabPage && Forms.MainForm.tabsContainer1.TabsCount > 1)
                {
                    if (MsgBox(Program.Lang.OpenTabs_Close, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
                }

                if (!e.Cancel)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.file, Title = Program.Lang.Filter_SaveWinPaletterTheme })
                    {
                        bool result = Forms.MainForm.ExitWithChangedFileResponse(); //dlg,
                                                                                    //() => Forms.ThemeLog.Apply_Theme(Program.TM, false, true),
                                                                                    //() => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime, false, true),
                                                                                    //() => { using (Manager TMx = Default.Get()) { Forms.ThemeLog.Apply_Theme(TMx, false, true); } }
                                                                                    //);

                        e.Cancel = !result;
                    }
                }
            }

            if (!e.Cancel)
            {
                Forms.MainForm.tabsContainer1.TabControl.Visible = false;

                Forms.MainForm.closeSignalReceivedFromHomePage = true;
                Forms.MainForm.Close();

                //Reset to false to make sure that main form reshow confirmation dialog if a previous dialog returend 'No'.
                Forms.MainForm.closeSignalReceivedFromHomePage = false;
            }
        }

        public void LoadData()
        {
            userButton.Tag = User.UserName;
            userButton.Image = User.ProfilePicture.Resize(26, 26);
        }

        private void LoadOSData()
        {
            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    card1.Image = Assets.Banners.Win12;
                    winEdition.Image = Assets.WinLogos.Win12;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_Win12);
                    break;

                case WindowStyle.W11:
                    card1.Image = Assets.Banners.Win11;
                    winEdition.Image = Assets.WinLogos.Win11;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_Win11);
                    break;

                case WindowStyle.W10:
                    card1.Image = Assets.Banners.Win10;
                    winEdition.Image = Assets.WinLogos.Win10;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_Win10);
                    break;

                case WindowStyle.W81:
                    card1.Image = Assets.Banners.Win81;
                    winEdition.Image = Assets.WinLogos.Win81;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_Win8);
                    break;

                case WindowStyle.W7:
                    card1.Image = Assets.Banners.WinOld;
                    winEdition.Image = Assets.WinLogos.Win7;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_Win7);
                    break;

                case WindowStyle.WVista:
                    card1.Image = Assets.Banners.WinOld;
                    winEdition.Image = Assets.WinLogos.WinVista;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_WinVista);
                    break;

                case WindowStyle.WXP:
                    card1.Image = Assets.Banners.WinOld;
                    winEdition.Image = Assets.WinLogos.WinXP;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_WinXP);
                    break;

                default:
                    card1.Image = Assets.Banners.Win12;
                    winEdition.Image = Assets.WinLogos.Win12;
                    winEdition.Tag = string.Format(Program.Lang.OS_PreviewingAs, Program.Lang.OS_Win12);
                    break;
            }

            card12.Visible = Program.WindowStyle != WindowStyle.WXP && Program.WindowStyle != WindowStyle.WVista;
            card3.Visible = Program.WindowStyle != WindowStyle.WVista;
            //card14.Visible = Program.WindowStyle != WindowStyle.WXP;
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            labelAlt1.Text = $"{TM.Info.ThemeName}";
            labelAlt2.Text = $"{Program.Lang.By} {TM.Info.Author}";
            labelAlt3.Text = TM.Info.ThemeVersion;
            groupBox1.UpdatePattern(TM.Info.Pattern);
        }

        public async void AutoUpdatesCheck()
        {
            if (OS.WXP || OS.WVista) return;

            StableInt = 0;
            BetaInt = 0;
            UpdateChannel = 0;
            ChannelFixer = Program.Settings.Updates.Channel == Settings.Structures.Updates.Channels.Stable ? 0 : 1;

            if (Program.IsNetworkAvailable)
            {
                try
                {
                    using (DownloadManager DM = new())
                    {
                        RaiseUpdate = false;
                        ver = string.Empty;

                        string result = await DM.ReadStringAsync(Links.Updates);
                        Updates_ls = result.Split(new char[] { '\r', '\n' }).ToList();

                        foreach (string updateInfo in Updates_ls.Where(update => !string.IsNullOrEmpty(update) && !update.StartsWith("#")))
                        {
                            string[] updateParts = updateInfo.Split(' ');

                            if (updateParts.Length >= 2)
                            {
                                if ((updateParts[0] ?? "stable").ToLower() == "stable") StableInt = Updates_ls.IndexOf(updateInfo);
                                if ((updateParts[0] ?? "stable").ToLower() == "beta") BetaInt = Updates_ls.IndexOf(updateInfo);
                            }
                        }

                        UpdateChannel = (ChannelFixer == 0) ? StableInt : BetaInt;

                        ver = Updates_ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[1];

                        RaiseUpdate = !string.IsNullOrEmpty(ver) && ver.CompareTo(Program.Version) == 1;
                    }
                }
                catch { } // Ignore any exception here (silent startup, only notify if there is already an update)
            }

            try
            {
                Invoke(() =>
                {
                    if (RaiseUpdate)
                    {
                        Forms.Updates.ls = Updates_ls;
                        NotifyUpdates.Visible = true;
                        Button5.ImageGlyph = Properties.Resources.Glyph_Update_Dot;
                        NotifyUpdates.ShowBalloonTip(10000, Application.ProductName, $"{Program.Lang.NewUpdate}. {Program.Lang.Version} {ver}", ToolTipIcon.Info);
                    }
                });
            }
            catch { } // An exception may be thrown if the form is closed before the Invoke is executed
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            User.Login(true);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.About);
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.WikiURL);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Whatsnew);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.RescueTools);
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            if (OS.WXP)
            {
                if (MsgBox(string.Format(Program.Lang.Store_WontWork_Protocol, Program.Lang.OS_WinXP), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }
            else if (OS.WVista)
            {
                if (MsgBox(string.Format(Program.Lang.Store_WontWork_Protocol, Program.Lang.OS_WinVista), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }

            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Store);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Updates);
            Button5.ImageGlyph = Properties.Resources.Glyph_Update;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.SettingsX);
        }

        private void card1_Click(object sender, EventArgs e)
        {
            Form form;

            if (Program.WindowStyle == WindowStyle.W12)
            {
                form = Forms.Win12Colors;
            }
            else if (Program.WindowStyle == WindowStyle.W11)
            {
                form = Forms.Win11Colors;
            }
            else if (Program.WindowStyle == WindowStyle.W10)
            {
                form = Forms.Win10Colors;
            }
            else if (Program.WindowStyle == WindowStyle.W81)
            {
                form = Forms.Win81Colors;
            }
            else if (Program.WindowStyle == WindowStyle.W7)
            {
                form = Forms.Win7Colors;
            }
            else if (Program.WindowStyle == WindowStyle.WVista)
            {
                form = Forms.WinVistaColors;
            }
            else if (Program.WindowStyle == WindowStyle.WXP)
            {
                form = Forms.WinXPColors;
            }
            else
            {
                form = Forms.Win11Colors;
            }

            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(form);
        }

        private void card2_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Win32UI);
        }

        private void card3_Click(object sender, EventArgs e)
        {

            if (Program.WindowStyle == WindowStyle.W12 || Program.WindowStyle == WindowStyle.W11 || Program.WindowStyle == WindowStyle.W10)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.LogonUI);
            }
            else if (Program.WindowStyle == WindowStyle.W81 | Program.WindowStyle == WindowStyle.W7)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.LogonUI7);
            }
            else if (Program.WindowStyle == WindowStyle.WXP)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.LogonUIXP);
            }
            else if (Program.WindowStyle == WindowStyle.WVista)
            {
                MsgBox(Program.Lang.VistaLogonNotSupported, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.LogonUI);
            }
        }

        private void card6_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.CursorsStudio);
        }

        private void card5_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Metrics_Fonts);
        }

        private void card4_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.TerminalsDashboard.ShowDialog();
        }

        private void card8_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.W12)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W12;
            }
            else if (Program.WindowStyle == WindowStyle.W11)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W11;
            }
            else if (Program.WindowStyle == WindowStyle.W10)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W10;
            }
            else if (Program.WindowStyle == WindowStyle.W81)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W81;
            }
            else if (Program.WindowStyle == WindowStyle.W7)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W7;
            }
            else if (Program.WindowStyle == WindowStyle.WVista)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_WVista;
            }
            else if (Program.WindowStyle == WindowStyle.WXP)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_WXP;
            }
            else
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W12;
            }

            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Wallpaper_Editor);
        }

        private void card9_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.WinEffecter);
        }

        private void card7_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Sounds_Editor);
        }

        private void card11_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.ScreenSaver_Editor);
        }

        private void card12_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle != WindowStyle.WXP && Program.WindowStyle != WindowStyle.WVista)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.AltTabEditor);
            }
            else
            {
                if (Program.WindowStyle == WindowStyle.WXP)
                    MsgBox(string.Format(Program.Lang.AltTab_Unsupported, Program.Lang.OS_WinXP), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (Program.WindowStyle == WindowStyle.WVista)
                    MsgBox(string.Format(Program.Lang.AltTab_Unsupported, Program.Lang.OS_WinVista), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void card10_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.ApplicationThemer.FixLanguageDarkModeBug = false;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.ApplicationThemer);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Forms.MainForm.ExitWithChangedFileResponse())
            {
                Program.TM = new(Theme.Manager.Source.Registry);
                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                file = null;
                Text = Application.ProductName;
                LoadFromTM(Program.TM);
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (Forms.MainForm.ExitWithChangedFileResponse())
            {
                Program.TM = (Theme.Manager)Theme.Default.Get().Clone();
                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                file = null;
                Text = Application.ProductName;
                LoadFromTM(Program.TM);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Forms.MainForm.ExitWithChangedFileResponse())
            {
                using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = file, Title = Program.Lang.Filter_OpenWinPaletterTheme })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnThemeLoad)
                        {
                            string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeOpen", $"{Program.TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                            Program.TM.Save(Source.File, filename);
                        }

                        file = dlg.FileName;
                        Program.TM = new(Theme.Manager.Source.File, dlg.FileName);
                        Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                        Text = System.IO.Path.GetFileName(file);
                        LoadFromTM(Program.TM);
                    }
                }
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = file, Title = Program.Lang.Filter_SaveWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    file = dlg.FileNames[0];
                    Program.TM.Save(Theme.Manager.Source.File, file);
                    Text = System.IO.Path.GetFileName(file);
                    LoadFromTM(Program.TM);
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = file, Title = Program.Lang.Filter_SaveWinPaletterTheme })
            {
                if (!System.IO.File.Exists(dlg.FileName))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        file = dlg.FileNames[0];
                        Program.TM.Save(Theme.Manager.Source.File, file);
                        Text = System.IO.Path.GetFileName(file);
                        LoadFromTM(Program.TM);
                    }
                }
                else
                {
                    file = dlg.FileNames[0];
                    Program.TM.Save(Theme.Manager.Source.File, file);
                    Text = System.IO.Path.GetFileName(file);
                    LoadFromTM(Program.TM);
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.EditInfo);
        }

        private void btn_history_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.BackupThemes_List);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Process.Start(Links.PayPal);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            Forms.MainForm.LoggingOff = false;

            if (MsgBox(Program.Lang.LogoffQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.LogoffAlert1, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists($@"{SysPaths.System32}\logoff.exe"))
                {
                    Forms.MainForm.LoggingOff = true;
                    Interaction.Shell($@"{SysPaths.System32}\logoff.exe", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.LogoffNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            Program.RestartExplorer();
        }

        private void apply_btn_Click(object sender, EventArgs e)
        {
            Forms.ThemeLog.Apply_Theme();
        }

        private void Dashboard_Shown(object sender, EventArgs e)
        {
            if (Program.Settings.Updates.AutoCheck) Task.Run(AutoUpdatesCheck);
        }

        private void NotifyUpdates_BalloonTipClicked(object sender, EventArgs e)
        {
            NotifyUpdates.Visible = false;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Updates);
        }

        private void winEdition_Click(object sender, EventArgs e)
        {
            if (Forms.OS_Dashboard.ShowDialog() == DialogResult.OK) LoadOSData();
        }

        private void panel1_BackColorChanged(object sender, EventArgs e)
        {
            groupBox1.Refresh();
        }

        private void Home_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.Start(Links.Wiki.WikiURL);
        }

        private void card13_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.IconsStudio);
        }

        private void card14_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.VisualStyles);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        ////new UI.Style.SchemeEditor().Show();
        //}

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        private void Dashboard_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }
    }
}