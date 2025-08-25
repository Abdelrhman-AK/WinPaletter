﻿using FluentTransitions;
using Microsoft.VisualBasic;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.Theme;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    /// <summary>
    /// Home page of WinPaletter.
    /// </summary>
    public partial class Home : Form
    {
        private bool RaiseUpdate = false;
        private string ver = string.Empty;
        private int StableInt, BetaInt, UpdateChannel;
        private int ChannelFixer;
        private List<string> Updates_ls = [];

        /// <summary>
        /// File path of the theme File.
        /// </summary>
        public string File { get; set; } = string.Empty;

        /// <summary>
        /// Old minimum size of the main form.
        /// </summary>
        public Size oldMainFormMinSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        public Home()
        {
            InitializeComponent();
            Icon = FormsExtensions.Icon<MainForm>();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            // Reset the logging off flag to false.
            Forms.MainForm.LoggingOff = false;

            NotifyUpdates.Icon = Icon;
            groupBox1.UseSharpStyle = true;
            labelAlt1.Font = new(Fonts.Title, labelAlt1.Font.Size, labelAlt1.Font.Style);
            labelAlt2.Font = new(Fonts.Title, labelAlt2.Font.Size, labelAlt2.Font.Style);
            labelAlt3.Font = Fonts.Console;

            oldMainFormMinSize = Forms.MainForm.MinimumSize;

            // Double buffer the flow layout panels to prevent flickering.
            flowLayoutPanel3.DoubleBuffer();

            // Set the compact mode it it is enabled.
            processCompactMode();

            // Load strings and images to cards according to current OS or selected OS from <see cref="Program.WindowStyle"/>.
            LoadOSData();

            // Load data of current user and get his profile picture.
            LoadData();

            // Load theme data from the theme manager.
            LoadFromTM(Program.TM);

            // Add animations to toolbar buttons and cards.
            foreach (UI.WP.Button button in titlebarExtender2.GetAllControls().OfType<UI.WP.Button>())
            {
                button.MouseEnter += (s, e) => Transition.With(tip_label, nameof(tip_label.Text), ((s as UI.WP.Button).Tag ?? string.Empty).ToString()).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                button.MouseLeave += (s, e) => Transition.With(tip_label, nameof(tip_label.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }

            foreach (Card card in flowLayoutPanel1.Controls)
            {
                card.MouseEnter += (s, e) => Transition.With(panel1, nameof(panel1.BackColor), Program.Style.DarkMode ? (s as Card).Color.Dark(0.7f) : (s as Card).Color.CB(0.7f)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                card.MouseLeave += (s, e) => Transition.With(panel1, nameof(panel1.BackColor), BackColor).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Settings.General.CompactAspects = !Program.Settings.General.CompactAspects;
            processCompactMode();
        }

        /// <summary>
        /// Process the compact mode for the home page, suitable for small screens.
        /// </summary>
        private void processCompactMode()
        {
            foreach (Card card in flowLayoutPanel1.Controls)
            {
                card.Compact = Program.Settings.General.CompactAspects;
                card.Size = card.Compact ? new(144, 130) : new(277, 130);
            }

            Forms.MainForm.MinimumSize = Program.Settings.General.CompactAspects ? new(925, 660) : oldMainFormMinSize;

            button1.ImageGlyph = Program.Settings.General.CompactAspects ? Resources.Glyph_Expand : Resources.Glyph_Compact;
        }

        /// <summary>
        /// Void to handle the form closing event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;

            if (!Forms.MainForm.LoggingOff)
            {
                if (Forms.Home.Parent is TabPage && Forms.MainForm.tabsContainer1.TabsCount > 1)
                {
                    if (MsgBox(Program.Lang.Strings.Messages.OpenTabs_Close, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
                }

                if (!e.Cancel)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.File, Title = Program.Lang.Strings.Extensions.SaveWinPaletterTheme })
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

            // Continue with the closing event if the user has not cancelled it.
            if (!e.Cancel)
            {
                Forms.MainForm.tabsContainer1.TabControl.Visible = false;

                Forms.MainForm.closeSignalReceivedFromHomePage = true;
                Forms.MainForm.Close();

                //Reset to false to make sure that main form reshow confirmation dialog if a previous dialog returend 'No'.
                Forms.MainForm.closeSignalReceivedFromHomePage = false;
            }
        }

        /// <summary>
        /// Load the user data to the user button.
        /// </summary>
        public void LoadData()
        {
            userButton.Tag = User.Name;
            userButton.Image = User.ProfilePicture.Resize(26, 26);
        }

        /// <summary>
        /// Load the OS data to the cards.
        /// </summary>
        private void LoadOSData()
        {
            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    card1.Image = Themes_Banners.Theme_12;
                    winEdition.Image = WinLogos.Win12;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.W12);
                    break;

                case WindowStyle.W11:
                    card1.Image = Themes_Banners.Theme_11;
                    winEdition.Image = WinLogos.Win11;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.W11);
                    break;

                case WindowStyle.W10:
                    card1.Image = Themes_Banners.Theme_10;
                    winEdition.Image = WinLogos.Win10;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.W10);
                    break;

                case WindowStyle.W81:
                    card1.Image = Themes_Banners.Theme_8_1;
                    winEdition.Image = WinLogos.Win8_1;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.W8);
                    break;

                case WindowStyle.W8:
                    card1.Image = Themes_Banners.Theme_8;
                    winEdition.Image = WinLogos.Win8;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.W8);
                    break;

                case WindowStyle.W7:
                    card1.Image = Themes_Banners.Theme_7;
                    winEdition.Image = WinLogos.Win7;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.W7);
                    break;

                case WindowStyle.WVista:
                    card1.Image = Themes_Banners.Theme_Vista;
                    winEdition.Image = WinLogos.WinVista;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.WVista);
                    break;

                case WindowStyle.WXP:
                    card1.Image = Themes_Banners.Theme_XP;
                    winEdition.Image = WinLogos.WinXP;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.WXP);
                    break;

                default:
                    card1.Image = Themes_Banners.Theme_12;
                    winEdition.Image = WinLogos.Win12;
                    winEdition.Tag = string.Format(Program.Lang.Strings.Tips.OS_PreviewingAs, Program.Lang.Strings.Windows.W12);
                    break;
            }

            card12.Visible = Program.WindowStyle != WindowStyle.WXP && Program.WindowStyle != WindowStyle.WVista;
            card3.Visible = Program.WindowStyle != WindowStyle.WVista && Program.WindowStyle != WindowStyle.W8;
            //card14.Visible = Program.WindowStyle != WindowStyle.WXP;
        }

        /// <summary>
        /// Load the theme data from the theme manager.
        /// </summary>
        /// <param name="TM"></param>
        public void LoadFromTM(Manager TM)
        {
            labelAlt1.Text = $"{TM.Info.ThemeName}";
            labelAlt2.Text = $"{Program.Lang.Strings.General.By} {TM.Info.Author}";
            labelAlt3.Text = TM.Info.ThemeVersion;
            groupBox1.UpdatePattern(TM.Info.Pattern);
        }

        /// <summary>
        /// Check for updates automatically when the form is loaded.
        /// </summary>
        public async void AutoUpdatesCheck()
        {
            // Do not check for updates on Windows XP and Vista as the update server does not support these versions (Unsupported TLS version).
            if (OS.WXP || OS.WVista) return;

            // Reset the update channel variables.
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
                        Updates_ls = [.. result.Split(['\r', '\n'])];

                        foreach (string updateInfo in Updates_ls.Where(update => !string.IsNullOrEmpty(update) && !update.StartsWith("#")))
                        {
                            // Split the update info into parts. An update info is a line that has spaces.
                            string[] updateParts = updateInfo.Split(' ');

                            if (updateParts.Length >= 2)
                            {
                                // Check if the update is for the stable or beta channel.
                                if ((updateParts[0] ?? "stable").ToLower() == "stable") StableInt = Updates_ls.IndexOf(updateInfo);
                                if ((updateParts[0] ?? "stable").ToLower() == "beta") BetaInt = Updates_ls.IndexOf(updateInfo);
                            }
                        }

                        // Determine the update channel to use.
                        UpdateChannel = (ChannelFixer == 0) ? StableInt : BetaInt;

                        // Get the version of the update.
                        ver = Updates_ls.ElementAtOrDefault(UpdateChannel)?.Split(' ')[1];

                        // Check if the update is newer than the current version.
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
                        // Pass the updates list to the updates form to save time.
                        Forms.Updates.ls = Updates_ls;
                        NotifyUpdates.Visible = true;

                        // Change the update button image to the update notification image that has an indicator dot.
                        Button5.ImageGlyph = Resources.Glyph_Update_Dot;

                        // Hide the update notification.
                        NotifyUpdates.ShowBalloonTip(10000, Application.ProductName, $"{Program.Lang.Strings.Updates.NewUpdate}. {Program.Lang.Strings.General.Version} {ver}", ToolTipIcon.Info);
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
            Process.Start(Links.Releases);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Forms.SOS.Show();
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            if (OS.WXP)
            {
                if (MsgBox(string.Format(Program.Lang.Strings.Store.WontWork_Protocol, Program.Lang.Strings.Windows.WXP), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }
            else if (OS.WVista)
            {
                if (MsgBox(string.Format(Program.Lang.Strings.Store.WontWork_Protocol, Program.Lang.Strings.Windows.WVista), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }

            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Store);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Updates);
            Button5.ImageGlyph = Resources.Glyph_Update;
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
            else if (Program.WindowStyle == WindowStyle.W8)
            {
                form = Forms.Win8Colors;
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

                if (Program.WindowStyle == WindowStyle.W12)
                    Forms.LogonUI.LogonUI10x = Program.TM.LogonUI12; // Use the LogonUI12 from the theme manager
                else if (Program.WindowStyle == WindowStyle.W11)
                    Forms.LogonUI.LogonUI10x = Program.TM.LogonUI11; // Use the LogonUI11 from the theme manager
                else if (Program.WindowStyle == WindowStyle.W10)
                    Forms.LogonUI.LogonUI10x = Program.TM.LogonUI10; // Use the LogonUI10 from the theme manager

                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.LogonUI);
            }
            else if (Program.WindowStyle == WindowStyle.W81 || Program.WindowStyle == WindowStyle.W8)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.LogonUI81);
            }
            else if (Program.WindowStyle == WindowStyle.W7)
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
                MsgBox(Program.Lang.Strings.Messages.VistaLogonNotSupported, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            else if (Program.WindowStyle == WindowStyle.W8)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W8;
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
                    MsgBox(string.Format(Program.Lang.Strings.Messages.AltTab_Unsupported, Program.Lang.Strings.Windows.WXP), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (Program.WindowStyle == WindowStyle.WVista)
                    MsgBox(string.Format(Program.Lang.Strings.Messages.AltTab_Unsupported, Program.Lang.Strings.Windows.WVista), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                Program.TM = new(Source.Registry);
                Program.TM_Original = (Manager)Program.TM.Clone();
                File = null;
                Text = Application.ProductName;
                LoadFromTM(Program.TM);
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (Forms.MainForm.ExitWithChangedFileResponse())
            {
                Program.TM = (Manager)Default.Get().Clone();
                Program.TM_Original = (Manager)Program.TM.Clone();
                File = null;
                Text = Application.ProductName;
                LoadFromTM(Program.TM);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Forms.MainForm.ExitWithChangedFileResponse())
            {
                using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = File, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnThemeLoad)
                        {
                            string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeOpen", $"{Program.TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                            Program.TM.Save(Source.File, filename);
                        }

                        File = dlg.FileName;
                        Program.TM = new(Source.File, dlg.FileName);
                        Program.TM_Original = (Manager)Program.TM.Clone();
                        Text = Path.GetFileName(File);
                        LoadFromTM(Program.TM);
                    }
                }
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = string.IsNullOrWhiteSpace(File) ? Program.TM.Info.ThemeName + ".wpth" : File, Title = Program.Lang.Strings.Extensions.SaveWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File = dlg.FileNames[0];
                    Program.TM.Save(Source.File, File);
                    Text = Path.GetFileName(File);
                    LoadFromTM(Program.TM);
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = string.IsNullOrWhiteSpace(File) ? Program.TM.Info.ThemeName + ".wpth" : File, Title = Program.Lang.Strings.Extensions.SaveWinPaletterTheme })
            {
                if (!System.IO.File.Exists(dlg.FileName))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        File = dlg.FileNames[0];
                        Program.TM.Save(Source.File, File);
                        Text = Path.GetFileName(File);
                        LoadFromTM(Program.TM);
                    }
                }
                else
                {
                    File = dlg.FileNames[0];
                    Program.TM.Save(Source.File, File);
                    Text = Path.GetFileName(File);
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

        private void Button13_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            Forms.MainForm.LoggingOff = false;

            if (MsgBox(OS.WXP || OS.WVista || OS.W7 ? Program.Lang.Strings.Messages.LogoffQuestion : Program.Lang.Strings.Messages.SignOutQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.Strings.Messages.LogoffAlert1) == DialogResult.Yes)
            {
                // Disable the file system redirection to access the system32 folder.
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists($@"{SysPaths.System32}\logoff.exe"))
                {
                    // Set the logging off flag to true to make WinPaletter exits without confirmation.
                    Forms.MainForm.LoggingOff = true;
                    Interaction.Shell($@"{SysPaths.System32}\logoff.exe", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.Strings.Messages.LogoffNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void Home_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.WikiURL);
        }

        private void card13_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.IconsStudio);
        }

        private void card15_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.AccessibilityEditor);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Logs);
        }

        private void winEdition_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        // When the parent of the dashboard is changed, check if it is a tab page to hide the pin button.
        private void Dashboard_ParentChanged(object sender, EventArgs e)
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
    }
}