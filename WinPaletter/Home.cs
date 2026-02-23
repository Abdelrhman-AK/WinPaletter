using FluentTransitions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.Theme;
using WinPaletter.Theme.Structures;
using WinPaletter.TypesExtensions;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.Theme.Manager;
using static WinPaletter.Updates;

namespace WinPaletter
{
    /// <summary>
    /// Home page of WinPaletter.
    /// </summary>
    public partial class Home : UI.WP.Form
    {
        private bool isLoggedIn = false;

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
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Reset the logging off flag to false.
            Forms.MainForm.LoggingOff = false;

            isLoggedIn = User.GitHub_LoggedIn;
            GitHub.Events.GitHubUserSwitch += User_GitHubUserSwitch;
            GitHub.Events.GitHubAvatarUpdated += UpdateUserButtonAvatar;

            NotifyUpdates.Icon = Icon;

            labelAlt1.Font = new(Fonts.Title, labelAlt1.Font.Size, labelAlt1.Font.Style);
            labelAlt2.Font = new(Fonts.Title, labelAlt2.Font.Size, labelAlt2.Font.Style);
            labelAlt3.Font = Fonts.Console;

            oldMainFormMinSize = Forms.MainForm.MinimumSize;

            Button28.Text = OS.WXP || OS.WVista || OS.W7 ? Program.Localization.Strings.General.Logoff : Program.Localization.Strings.General.SignOut;

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

            foreach (Card card in flowLayoutPanel1.Controls.OfType<Card>())
            {
                card.MouseEnter += (s, e) => Transition.With(panel1, nameof(panel1.BackColor), Program.Style.DarkMode ? (s as Card).Color.Dark(0.7f) : (s as Card).Color.CB(0.7f)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                card.MouseLeave += (s, e) => Transition.With(panel1, nameof(panel1.BackColor), BackColor).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
        }

        private void User_GitHubUserSwitch(GitHub.Events.GitHubUserChangeEventArgs e)
        {
            isLoggedIn = e.IsLoggedIn;
            LoadData();
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
            foreach (Card card in flowLayoutPanel1.Controls.OfType<Card>())
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
                    if (MsgBox(Program.Localization.Strings.Messages.OpenTabs_Close, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
                }

                if (!e.Cancel)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
                    {
                        bool result = Forms.MainForm.ExitWithChangedFileResponse();
                        e.Cancel = !result;
                    }
                }
            }

            // Continue with the closing event if the user has not cancelled it.
            if (!e.Cancel)
            {
                //User.GitHubUserSwitch -= User_GitHubUserSwitch;

                Forms.MainForm.tabsContainer1.TabControl.Visible = false;

                Forms.MainForm.closeSignalReceivedFromHomePage = true;
                Forms.MainForm.Close();

                //Reset to false to make sure that main form reshow confirmation dialog if a previous dialog returend 'No'.
                Forms.MainForm.closeSignalReceivedFromHomePage = false;
            }
        }

        private void UpdateUserButtonAvatar(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => UpdateUserButtonAvatar(sender, e));
                return;
            }

            Bitmap avatar = User.GitHub_Avatar ?? User.ProfilePicture;

            if (avatar == null || !avatar.IsValid())
            {
                userButton.Image = null;
                return;
            }

            using (Bitmap resizedAvatar = avatar?.Resize(32, 32))
            using (Bitmap resizedUser = User.ProfilePicture?.Resize(16, 16))
            using (Bitmap circularAvatar = resizedAvatar?.ToCircular(Program.Style.Schemes.Main.Colors.ForeColor_Accent))
            using (Bitmap circularUser = resizedUser?.ToCircular())
            {
                Bitmap finalImage;

                if (circularUser is not null)
                {
                    PointF overlayPoint = new(circularAvatar.Width - circularUser.Width, circularAvatar.Height - circularUser.Height);
                    finalImage = circularAvatar.Overlay(circularUser, overlayPoint);
                }
                else
                {
                    finalImage = (Bitmap)circularAvatar.Clone();
                }

                Image old = userButton.Image;
                userButton.Image = finalImage;
                old?.Dispose();
            }
        }

        /// <summary>
        /// Load the user data to the user button.
        /// </summary>
        public void LoadData()
        {
            if (isLoggedIn)
            {
                Task.Run(() =>
                {
                    Transition.With(tip_label, nameof(tip_label.Text), $"{Program.Localization.Strings.General.Welcome}, {User.GitHub.Login}").CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));

                    System.Threading.Thread.Sleep(3500);

                    Transition.With(tip_label, nameof(tip_label.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                });

                userButton.Tag = User.GitHub.Login + " > " + User.Name;

                UpdateUserButtonAvatar(null, null);
            }
            else
            {
                userButton.Tag = User.Name;

                using (Bitmap resized = User.ProfilePicture?.Resize(32, 32))
                {
                    userButton.Image = resized?.ToCircular();
                }
            }
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
                    card3.Image = Themes_Banners.LogonUI_12;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win12;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.W12);
                    break;

                case WindowStyle.W11:
                    card1.Image = Themes_Banners.Theme_11;
                    card3.Image = Themes_Banners.LogonUI_11;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win11;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.W11);
                    break;

                case WindowStyle.W10:
                    card1.Image = Themes_Banners.Theme_10;
                    card3.Image = Themes_Banners.LogonUI_10;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win10;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.W10);
                    break;

                case WindowStyle.W81:
                    card1.Image = Themes_Banners.Theme_8_1;
                    card3.Image = Themes_Banners.LogonUI_8x;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win8_1;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.W81);
                    break;

                case WindowStyle.W8:
                    card1.Image = Themes_Banners.Theme_8;
                    card3.Image = Themes_Banners.LogonUI_8x;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win8;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.W8);
                    break;

                case WindowStyle.W7:
                    card1.Image = Themes_Banners.Theme_7;
                    card3.Image = Themes_Banners.LogonUI_7;
                    card3.Text = Program.Localization.Strings.Aspects.LogonUI;
                    card3.Tag = Program.Localization.Strings.Aspects.LogonUI_Description;
                    winEdition.Image = WinLogos.Win7;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.W7);
                    break;

                case WindowStyle.WVista:
                    card1.Image = Themes_Banners.Theme_Vista;
                    card3.Image = Themes_Banners.LogonUI_Vista;
                    card3.Text = Program.Localization.Strings.Aspects.LogonUI;
                    card3.Tag = Program.Localization.Strings.Aspects.LogonUI_Description;
                    winEdition.Image = WinLogos.WinVista;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.WVista);
                    break;

                case WindowStyle.WXP:
                    card1.Image = Themes_Banners.Theme_XP;
                    card3.Image = Themes_Banners.LogonUI_XP;
                    card3.Text = Program.Localization.Strings.Aspects.LogonUI;
                    card3.Tag = Program.Localization.Strings.Aspects.LogonUI_Description;
                    winEdition.Image = WinLogos.WinXP;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.WXP);
                    break;

                default:
                    card1.Image = Themes_Banners.Theme_11;
                    card3.Image = Themes_Banners.LogonUI_11;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win11;
                    winEdition.Tag = string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, Program.Localization.Strings.Windows.W11);
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
            labelAlt2.Text = $"{Program.Localization.Strings.General.By} {TM.Info.Author}";
            labelAlt3.Text = TM.Info.ThemeVersion;
            groupBox1.UpdatePattern(TM.Info.Pattern);
            Program.Style.Pattern = TM.Info.Pattern;
        }

        /// <summary>
        /// Automatically check for updates when the form is loaded.
        /// Downloads and parses update info once, always passes it to the Updates form.
        /// Shows notification only if a newer version is available.
        /// </summary>
        public async void AutoUpdatesCheck()
        {
            UpdatesInfo latestUpdate = null;
            bool hasNewerVersion = false;

            if (Program.IsNetworkAvailable)
            {
                try
                {
                    using DownloadManager DM = new();
                    string response = await DM.ReadStringAsync(Links.Updates);
                    string[] lines = response.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    Settings.Structures.Updates.Channels targetChannel = Program.Settings.Updates.Channel;

                    // Find the first valid update for the target channel
                    foreach (string line in lines)
                    {
                        if (!UpdatesInfo.TryParse(line, out UpdatesInfo info)) continue;
                        if (info.Channel != targetChannel) continue;

                        latestUpdate = info;
                        hasNewerVersion = info.Version > new Version(Program.Version);
                        break; // first match per channel wins
                    }
                }
                catch
                {
                    // silent: auto-check must never block or crash UI
                }
            }

            // Always pass info to the Updates form, but notify only if newer
            try
            {
                Invoke(() =>
                {
                    if (latestUpdate is not null)
                    {
                        Forms.Updates.PreloadUpdateInfo(latestUpdate);
                    }

                    if (hasNewerVersion)
                    {
                        Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Updates);

                        NotifyUpdates.Visible = true;
                        Button5.ImageGlyph = Resources.Glyph_Update_Dot;

                        NotifyUpdates.ShowBalloonTip(
                            10000,
                            Application.ProductName,
                            $"{Program.Localization.Strings.Updates.NewUpdate} ({Program.Localization.Strings.General.Version} {latestUpdate.Version})",
                            ToolTipIcon.Info
                        );
                    }
                });
            }
            catch
            {
                // Form may be closing — safely ignore
            }
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
            if (Program.Settings.Store.ShowNewXPIntro) Forms.Store_Intro_New.ShowDialog();

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
            System.Windows.Forms.Form form;

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
                MsgBox(Program.Localization.Strings.Messages.VistaLogonNotSupported, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            Forms.TerminalsDashboard.ShowDialog(card4.Size, card4.PointToScreen(Point.Empty));
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
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W11;
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
                    MsgBox(string.Format(Program.Localization.Strings.Messages.AltTab_Unsupported, Program.Localization.Strings.Windows.WXP), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (Program.WindowStyle == WindowStyle.WVista)
                    MsgBox(string.Format(Program.Localization.Strings.Messages.AltTab_Unsupported, Program.Localization.Strings.Windows.WVista), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                Program.TM_Original = Program.TM.Clone();
                File = null;
                Text = Application.ProductName;
                LoadFromTM(Program.TM);
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (Forms.MainForm.ExitWithChangedFileResponse())
            {
                Program.TM = Default.FromCurrentOS.Clone();
                Program.TM_Original = Program.TM.Clone();
                File = null;
                Text = Application.ProductName;
                LoadFromTM(Program.TM);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Forms.MainForm.ExitWithChangedFileResponse())
            {
                using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = File, Title = Program.Localization.Strings.Extensions.OpenWinPaletterTheme })
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
                        Program.TM_Original = Program.TM.Clone();
                        Text = Path.GetFileName(File);
                        LoadFromTM(Program.TM);
                    }
                }
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = string.IsNullOrWhiteSpace(File) ? Program.TM.Info.ThemeName + ".wpth" : File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
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
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = string.IsNullOrWhiteSpace(File) ? Program.TM.Info.ThemeName + ".wpth" : File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
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

            if (MsgBox(OS.WXP || OS.WVista || OS.W7 ? Program.Localization.Strings.Messages.LogoffQuestion : Program.Localization.Strings.Messages.SignOutQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Localization.Strings.Messages.LogoffAlert1) == DialogResult.Yes)
            {
                // Disable the file system redirection to access the system32 folder.
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists($@"{SysPaths.System32}\logoff.exe"))
                {
                    // Set the logging off flag to true to make WinPaletter exits without confirmation.
                    Forms.MainForm.LoggingOff = true;
                    Program.SendCommand($@"{SysPaths.System32}\logoff.exe", false);
                }
                else
                {
                    MsgBox(string.Format(Program.Localization.Strings.Messages.LogoffNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (Forms.OS_Dashboard.ShowDialog(winEdition.Size, winEdition.PointToScreen(Point.Empty)) == DialogResult.OK) LoadOSData();
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
            Process.Start(SysPaths.LogsDir);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            Forms.WallStudio.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.GitHub_Mgr);
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

        private async void button16_Click(object sender, EventArgs e)
        {
            SimulateHugeSpikeTest();
        }

        // Fixed simulation that won't freeze the UI
        private async void SimulateHugeSpikeTest()
        {
            // Disable the button if you have one
            // btnTest.Enabled = false;

            progressGraph1.Clear();

            Random rand = new Random();
            double progress = 0;

            // Base speed in KB/s
            double baseSpeed = 50 * 1024; // 50 KB/s base speed

            // Run the simulation in a background task
            await System.Threading.Tasks.Task.Run(async () =>
            {
                while (progress <= 100)
                {
                    double speed;

                    // Normal operation in KB/s range
                    if (progress < 30)
                    {
                        // Phase 1: Normal KB/s speeds (20-100 KB/s)
                        speed = baseSpeed * (0.4 + rand.NextDouble() * 1.6);
                    }
                    else if (progress < 40)
                    {
                        // Phase 2: Transition zone - small spike to MB/s
                        if (progress > 32 && progress < 38)
                        {
                            // HUGE SPIKE - jumps to MB/s range
                            double spikeFactor = 50 + (rand.NextDouble() * 100); // 50-150x increase
                            speed = baseSpeed * spikeFactor; // This will be in MB/s range
                        }
                        else
                        {
                            speed = baseSpeed * (0.4 + rand.NextDouble() * 1.6);
                        }
                    }
                    else if (progress < 60)
                    {
                        // Phase 3: Back to KB/s
                        speed = baseSpeed * (0.5 + rand.NextDouble() * 1.5);

                        // Occasional mini-spikes to low MB/s
                        if (rand.NextDouble() < 0.1)
                        {
                            speed = baseSpeed * (20 + rand.NextDouble() * 30); // Small MB/s spikes
                        }
                    }
                    else if (progress < 75)
                    {
                        // Phase 4: Another huge spike zone
                        if (progress > 62 && progress < 70)
                        {
                            // Multiple spikes in succession
                            double spikeFactor = 80 + (rand.NextDouble() * 120); // 80-200x increase
                            speed = baseSpeed * spikeFactor; // High MB/s range
                        }
                        else
                        {
                            speed = baseSpeed * (0.6 + rand.NextDouble() * 1.4);
                        }
                    }
                    else
                    {
                        // Phase 5: Ending with variable speeds
                        speed = baseSpeed * (0.3 + rand.NextDouble() * 3.0);

                        // Final huge spike near the end
                        if (progress > 90 && progress < 95 && rand.NextDouble() < 0.3)
                        {
                            speed = baseSpeed * (150 + rand.NextDouble() * 100); // Massive spike
                        }
                    }

                    // Ensure speed is positive
                    speed = Math.Max(baseSpeed * 0.1, speed);

                    // Use Invoke to update the control on the UI thread
                    this.Invoke(new Action(() =>
                    {
                        progressGraph1.Add(progress, speed);
                    }));

                    // Progress increases based on speed (faster speed = faster progress)
                    double progressIncrement;
                    if (speed > baseSpeed * 50) // If in MB/s range
                    {
                        progressIncrement = 2.0; // Fast progress during spikes
                    }
                    else
                    {
                        progressIncrement = (speed / baseSpeed) * 0.3;
                    }

                    progress += Math.Max(0.2, progressIncrement);

                    // Small delay to prevent CPU overuse, but keep UI responsive
                    await System.Threading.Tasks.Task.Delay(20);
                }
            });

            // Re-enable the button
            // btnTest.Enabled = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            progressGraph1.State = ProgressGraph.ProgressBarState.Normal;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            progressGraph1.State = ProgressGraph.ProgressBarState.Error;
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            progressGraph1.State = ProgressGraph.ProgressBarState.Pause;
        }
    }
}