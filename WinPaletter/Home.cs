using FluentTransitions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Dialogs;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.Theme;
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
        private static Size _card_Expanded = new(277, 130);
        private static Size _card_Compact = new(144, 130);
        private static Size _form_Compact_Minimum = new(925, 660);
        private static Size _avatar_Big = new(32, 32);
        private static Size _avatar_Small = new(16, 16);

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
            Config.DarkModeChanged += Home_Localized;

            Icon = Properties.Resources.Icon;
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

            foreach (Card card in flowLayoutPanel1.Controls.OfType<Card>())
            {
                card.MouseEnter += (s, e) => Transition.With(panel1, nameof(panel1.BackColor), Program.Style.DarkMode ? (s as Card).Color.Dark(0.7f) : (s as Card).Color.CB(0.7f)).CriticalDamp(Program.AnimationSpan);
                card.MouseLeave += (s, e) => Transition.With(panel1, nameof(panel1.BackColor), BackColor).CriticalDamp(Program.AnimationSpan);
            }
        }

        private void Home_Localized()
        {
            if (this is null || !IsHandleCreated || !IsShown) return;

            var T = Program.Localization.Strings.Tips;

            Program.ToolTip.SetToolTip(pin_button, string.Format(T.PinToTabs_Title, Text), T.PinToTabs_Desc, pin_button.Image);
            Program.ToolTip.SetToolTip(Button3, T.NewTheme_Title, T.NewTheme_Desc, Button3.ImageGlyph);
            Program.ToolTip.SetToolTip(Button20, T.RestoreDefault_Title, T.RestoreDefault_Desc, Button20.ImageGlyph);
            Program.ToolTip.SetToolTip(Button2, T.OpenTheme_Title, T.OpenTheme_Desc, Button2.ImageGlyph);
            Program.ToolTip.SetToolTip(Button7, T.SaveTheme_Title, T.SaveTheme_Desc, Button7.ImageGlyph);
            Program.ToolTip.SetToolTip(Button9, T.SaveAsTheme_Title, T.SaveAsTheme_Desc, Button9.ImageGlyph);
            Program.ToolTip.SetToolTip(Button10, T.EditThemeInfo_Title, T.EditThemeInfo_Desc, Button10.ImageGlyph);
            Program.ToolTip.SetToolTip(btn_history, T.Backups_Title, T.Backups_Desc, btn_history.ImageGlyph);
            Program.ToolTip.SetToolTip(button14, T.WallStudio_Title, T.WallStudio_Desc, button14.ImageGlyph);
            Program.ToolTip.SetToolTip(Button11, T.Settings_Title, T.Settings_Desc, Button11.ImageGlyph);
            Program.ToolTip.SetToolTip(Button5, T.Updates_Title, T.Updates_Desc, Button5.ImageGlyph);
            Program.ToolTip.SetToolTip(Button31, T.Store_Title, T.Store_Desc, Button31.ImageGlyph);
            Program.ToolTip.SetToolTip(button15, T.GitHubManager_Title, T.GitHubManager_Desc, button15.ImageGlyph);
            Program.ToolTip.SetToolTip(button8, T.SOS_Title, T.SOS_Desc, button8.ImageGlyph);
            Program.ToolTip.SetToolTip(button1, T.LayoutToggle_Title, T.LayoutToggle_Desc, button1.ImageGlyph);
            Program.ToolTip.SetToolTip(Button39, T.Wiki_Title, T.Wiki_Desc, Button39.ImageGlyph);
            Program.ToolTip.SetToolTip(Button6, T.Releases_Title, T.Releases_Desc, Button6.ImageGlyph);
            Program.ToolTip.SetToolTip(button4, T.Logs_Title, T.Logs_Desc, button4.ImageGlyph);
            Program.ToolTip.SetToolTip(Button12, T.About_Title, T.About_Desc, Button12.ImageGlyph);
            Program.ToolTip.SetToolTip(button16, T.RestorePoints_Title, T.RestorePoints_Desc, button16.ImageGlyph);

            LoadOSData_ToolTip();
            LoadData(true);
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
                card.Size = card.Compact ? _card_Compact : _card_Expanded;
            }

            Forms.MainForm.MinimumSize = Program.Settings.General.CompactAspects ? _form_Compact_Minimum : oldMainFormMinSize;

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

        private Bitmap CreateAvatarImage(Bitmap avatar, Bitmap userProfilePicture)
        {
            if (avatar == null) return null;

            using (Bitmap resizedAvatar = avatar.Resize(_avatar_Big))
            using (Bitmap resizedUser = userProfilePicture?.Resize(_avatar_Small))
            {
                if (resizedAvatar == null) return null;

                using (Bitmap circularAvatar = resizedAvatar.ToCircular(Program.Style.Schemes.Main.Colors.ForeColor_Accent))
                using (Bitmap circularUser = resizedUser?.ToCircular())
                {
                    if (circularAvatar == null) return null;

                    if (circularUser != null)
                    {
                        PointF overlayPoint = new(circularAvatar.Width - circularUser.Width, circularAvatar.Height - circularUser.Height);
                        return circularAvatar.Overlay(circularUser, overlayPoint);
                    }
                    else
                    {
                        return (Bitmap)circularAvatar.Clone();
                    }
                }
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
                Image old = userButton.Image;
                userButton.Image = null;
                old?.Dispose();
                return;
            }

            Bitmap newImage = CreateAvatarImage(avatar, User.ProfilePicture);

            Image oldImage = userButton.Image;
            userButton.Image = newImage;
            oldImage?.Dispose();
        }

        /// <summary>
        /// Load the user data to the user button.
        /// </summary>
        public void LoadData(bool localizationReload = false)
        {
            string namePart;
            string descPart = Program.Localization.Strings.Tips.UserButton_Desc;

            if (isLoggedIn)
            {
                Task.Run(() =>
                {
                    Transition.With(tip_label, nameof(tip_label.Text), $"{Program.Localization.Strings.General.Welcome}, {User.GitHub.Login}").CriticalDamp(Program.AnimationSpan_Quick);

                    System.Threading.Thread.Sleep(3500);

                    Transition.With(tip_label, nameof(tip_label.Text), string.Empty).CriticalDamp(Program.AnimationSpan_Quick);
                });

                namePart = $"{User.GitHub.Login}@{User.Name}";
                descPart = $"\r\n•{Program.Localization.Strings.Tips.UserButton_WinAccountPart}: {User.Name}\r\n•{Program.Localization.Strings.Tips.UserButton_GitHubPart}: {User.GitHub.Login}\r\n\r\n{descPart}";

                if (!localizationReload) UpdateUserButtonAvatar(null, null);
            }
            else
            {
                namePart = User.Name;

                if (!localizationReload)
                {
                    using (Bitmap resized = User.ProfilePicture?.Resize(32, 32))
                    {
                        Image oldImage = userButton.Image;
                        userButton.Image = resized?.ToCircular();
                        oldImage?.Dispose();
                    }
                }
            }

            Forms.MainForm.Invoke(() => Program.ToolTip.SetToolTip(userButton, namePart, descPart, userButton.Image));
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
                    break;

                case WindowStyle.W11:
                    card1.Image = Themes_Banners.Theme_11;
                    card3.Image = Themes_Banners.LogonUI_11;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win11;
                    break;

                case WindowStyle.W10:
                    card1.Image = Themes_Banners.Theme_10;
                    card3.Image = Themes_Banners.LogonUI_10;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win10;
                    break;

                case WindowStyle.W81:
                    card1.Image = Themes_Banners.Theme_8_1;
                    card3.Image = Themes_Banners.LogonUI_8x;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win8_1;
                    break;

                case WindowStyle.W8:
                    card1.Image = Themes_Banners.Theme_8;
                    card3.Image = Themes_Banners.LogonUI_8x;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win8;
                    break;

                case WindowStyle.W7:
                    card1.Image = Themes_Banners.Theme_7;
                    card3.Image = Themes_Banners.LogonUI_7;
                    card3.Text = Program.Localization.Strings.Aspects.LogonUI;
                    card3.Tag = Program.Localization.Strings.Aspects.LogonUI_Description;
                    winEdition.Image = WinLogos.Win7;
                    break;

                case WindowStyle.WVista:
                    card1.Image = Themes_Banners.Theme_Vista;
                    card3.Image = Themes_Banners.LogonUI_Vista;
                    card3.Text = Program.Localization.Strings.Aspects.LogonUI;
                    card3.Tag = Program.Localization.Strings.Aspects.LogonUI_Description;
                    winEdition.Image = WinLogos.WinVista;
                    break;

                case WindowStyle.WXP:
                    card1.Image = Themes_Banners.Theme_XP;
                    card3.Image = Themes_Banners.LogonUI_XP;
                    card3.Text = Program.Localization.Strings.Aspects.LogonUI;
                    card3.Tag = Program.Localization.Strings.Aspects.LogonUI_Description;
                    winEdition.Image = WinLogos.WinXP;
                    break;

                default:
                    card1.Image = Themes_Banners.Theme_11;
                    card3.Image = Themes_Banners.LogonUI_11;
                    card3.Text = Program.Localization.Strings.Aspects.LockScreen;
                    card3.Tag = Program.Localization.Strings.Aspects.LockScreen_Description;
                    winEdition.Image = WinLogos.Win11;
                    break;
            }

            card12.Visible = Program.WindowStyle != WindowStyle.WXP && Program.WindowStyle != WindowStyle.WVista;
            card3.Visible = Program.WindowStyle != WindowStyle.WVista && Program.WindowStyle != WindowStyle.W8;
            //card14.Visible = Program.WindowStyle != WindowStyle.WXP;

            LoadOSData_ToolTip();
        }

        private void LoadOSData_ToolTip()
        {
            string OS;

            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    OS = Program.Localization.Strings.Windows.W12;
                    break;

                case WindowStyle.W11:
                    OS = Program.Localization.Strings.Windows.W11;
                    break;

                case WindowStyle.W10:
                    OS = Program.Localization.Strings.Windows.W10;
                    break;

                case WindowStyle.W81:
                    OS = Program.Localization.Strings.Windows.W81;
                    break;

                case WindowStyle.W8:
                    OS = Program.Localization.Strings.Windows.W8;
                    break;

                case WindowStyle.W7:
                    OS = Program.Localization.Strings.Windows.W7;
                    break;

                case WindowStyle.WVista:
                    OS = Program.Localization.Strings.Windows.WVista;
                    break;

                case WindowStyle.WXP:
                    OS = Program.Localization.Strings.Windows.WXP;
                    break;

                default:
                    OS = Program.Localization.Strings.Windows.W11;
                    break;
            }

            Program.ToolTip.SetToolTip(winEdition, OS, string.Format(Program.Localization.Strings.Tips.OS_PreviewingAs, OS), winEdition.Image);
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
        /// <summary>
        /// Automatically check for updates when the form is loaded.
        /// Downloads and parses update info once, always passes it to the Updates form.
        /// Shows notification only if a newer version is available.
        /// </summary>
        public async void AutoUpdatesCheck()
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Starting automatic updates check.");

            if (Program.Settings.Updates.LimitDailyCheck)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Daily update check limit enabled.");

                DateTime configuredLastChecked = Program.Settings.Updates.LastUpdateChecked;
                DateTime lastChecked = configuredLastChecked > DateTime.Today ? DateTime.Today : configuredLastChecked;

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"LastUpdateChecked from settings: {configuredLastChecked}. Validated value: {lastChecked}");

                DateTime today = DateTime.Today;

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Last update checked DateTime value: {lastChecked}");

                bool alreadyCheckedToday = lastChecked.Date >= today;

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Comparing LastUpdateChecked ({lastChecked.Date}) with Today ({today}). Result: {alreadyCheckedToday}");

                if (alreadyCheckedToday)
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Automatic updates check skipped because it was already checked today.");
                    return;
                }
            }

            UpdatesInfo latestUpdate = null;
            bool hasNewerVersion = false;
            bool checkSuccessful = false;

            if (Program.IsNetworkAvailable)
            {
                try
                {
                    using DownloadManager DM = new();

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Getting updates information.");

                    string response = await DM.ReadStringAsync(Links.Updates);
                    string[] lines = response.Split(["\n"], StringSplitOptions.RemoveEmptyEntries);

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Updates information are got successfully.");

                    Settings.Structures.Updates.Channels targetChannel = Program.Settings.Updates.Channel;

                    foreach (string line in lines)
                    {
                        if (!UpdatesInfo.TryParse(line, out UpdatesInfo info)) continue;

                        if (info.Channel != targetChannel) continue;

                        latestUpdate = info;
                        hasNewerVersion = info.Version > new Version(Program.Version);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Update matched. Version: {info.Version}, Channel: {info.Channel}, Newer version: {hasNewerVersion}");

                        break;
                    }

                    checkSuccessful = true;

                    if (Program.Settings.Updates.LimitDailyCheck)
                    {
                        DateTime saveDateTime = DateTime.Now;

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Setting LastUpdateChecked from {Program.Settings.Updates.LastUpdateChecked} to {saveDateTime}");

                        Program.Settings.Updates.LastUpdateChecked = saveDateTime;

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"LastUpdateChecked after setting: {Program.Settings.Updates.LastUpdateChecked}");

                        Program.Settings.Updates.Save();

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Update settings saved. Saved LastUpdateChecked: {Program.Settings.Updates.LastUpdateChecked}");
                    }
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error while checking updates: {ex}");
                }
            }
            else
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Warning, "Update check skipped because network is unavailable.");
            }

            if (!checkSuccessful)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Warning, "Automatic updates check failed.");
                return;
            }

            try
            {
                Invoke(() =>
                {
                    if (latestUpdate is not null)
                    {
                        Forms.Updates.PreloadUpdateInfo(latestUpdate);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Preloaded update information. Version: {latestUpdate.Version}");
                    }

                    if (hasNewerVersion)
                    {
                        Forms.MainForm.AddTab(Forms.Updates);

                        NotifyUpdates.Visible = true;
                        Button5.ImageGlyph = Resources.Glyph_Update_Dot;

                        NotifyUpdates.ShowBalloonTip(10000, Application.ProductName,
                            $"{Program.Localization.Strings.Updates.NewUpdate} ({Program.Localization.Strings.General.Version} {latestUpdate.Version})",
                            ToolTipIcon.Info);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"New update notification shown. Version: {latestUpdate.Version}");
                    }
                    else
                    {
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "No newer update found.");
                    }
                });
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error while displaying update notification: {ex}");
            }
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            User.Login(true);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Forms.MainForm.AddTab(Forms.About);
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

            Forms.MainForm.AddTab(Forms.Store);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Forms.MainForm.AddTab(Forms.Updates);
            Button5.ImageGlyph = Resources.Glyph_Update;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Forms.MainForm.AddTab(Forms.SettingsX);
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
            Forms.MainForm.AddTab(form);
        }

        private void card2_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.AddTab(Forms.Win32UI);
        }

        private void card3_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.W12 || Program.WindowStyle == WindowStyle.W11 || Program.WindowStyle == WindowStyle.W10)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.AddTab(Forms.LogonUI);
            }
            else if (Program.WindowStyle == WindowStyle.W81 || Program.WindowStyle == WindowStyle.W8)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.AddTab(Forms.LogonUI81);
            }
            else if (Program.WindowStyle == WindowStyle.W7)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.AddTab(Forms.LogonUI7);
            }
            else if (Program.WindowStyle == WindowStyle.WXP)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.AddTab(Forms.LogonUIXP);
            }
            else if (Program.WindowStyle == WindowStyle.WVista)
            {
                MsgBox(Program.Localization.Strings.Messages.VistaLogonNotSupported, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.AddTab(Forms.LogonUI);
            }
        }

        private void card6_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.AddTab(Forms.CursorsStudio);
        }

        private void card5_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.AddTab(Forms.Metrics_Fonts);
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
            Forms.MainForm.AddTab(Forms.Wallpaper_Editor);
        }

        private void card9_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.AddTab(Forms.WinEffecter);
        }

        private void card7_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.AddTab(Forms.Sounds_Editor);
        }

        private void card11_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.AddTab(Forms.ScreenSaver_Editor);
        }

        private void card12_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle != WindowStyle.WXP && Program.WindowStyle != WindowStyle.WVista)
            {
                Forms.MainForm.BackgroundImage = (sender as Card).Image;
                Forms.MainForm.AddTab(Forms.AltTabEditor);
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
            Forms.MainForm.AddTab(Forms.ApplicationThemer);
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
                            string filename = Program.GetUniqueFileName(SysPaths.ThemesBackup_OnThemeOpen, $"{Program.TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
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
            using (SaveFileDialog dlg = new() { Filter = $"{Program.Filters.WinPaletterTheme}|{Program.Filters.WinPaletterTheme_Uncompressed}", FileName = string.IsNullOrWhiteSpace(File) ? Program.TM.Info.ThemeName + ".wpth" : File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File = dlg.FileNames[0];
                    Program.TM.Save(Source.File, File, ignoreCompression: dlg.FilterIndex == 2);
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
            Forms.MainForm.AddTab(Forms.EditInfo);
        }

        private void btn_history_Click(object sender, EventArgs e)
        {
            Forms.MainForm.AddTab(Forms.BackupThemes_List);
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
            Forms.MainForm.AddTab(Forms.Updates);
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
            Forms.MainForm.AddTab(Forms.IconsStudio);
        }

        private void card15_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BackgroundImage = (sender as Card).Image;
            Forms.MainForm.AddTab(Forms.AccessibilityEditor);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(SysPaths.LogsDir);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Forms.WallStudio.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Forms.MainForm.AddTab(Forms.GitHub_Mgr);
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.AddTab(this);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SystemRestorePoints rp = new();
            Forms.MainForm.AddTab(rp);
        }
    }
}