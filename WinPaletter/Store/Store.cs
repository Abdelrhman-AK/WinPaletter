using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Simulation;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    /// <summary>
    /// WinPaletter Store form
    /// </summary>
    public partial class Store
    {
        #region Variables

        private bool StartedAsOnlineOrOffline = true;
        private bool FinishedLoadingInitialTMs;
        private readonly Dictionary<string, Manager> TMList = [];

        private readonly int w = 365;
        private readonly int h = 150;

        /// <summary>
        /// The selected store item
        /// </summary>
        public StoreItem selectedItem;

        private bool _Shown = false;
        private readonly List<CursorControl> AnimateList = [];
        private float Angle = 180f;
        private float Increment = 5f;
        private int Cycles = 0;
        private readonly DownloadManager DM = new();

        private bool ApplyOrEditToggle = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Store"/> class.
        /// </summary>
        public Store()
        {
            InitializeComponent();
            this.Disposed += (s, e) => Config.DarkModeChanged -= SetAspectsIcons;
        }
        #endregion

        #region Preview Methods

        /// <summary>
        /// Adjusts the preview of the store form with the specified theme manager.
        /// </summary>
        /// <param name="TM"></param>
        public void Adjust_Preview(Manager TM)
        {
            windowsDesktop1.WindowStyle = Program.WindowStyle;
            windowsDesktop1.BackgroundImage = Program.FetchSuitableWallpaper(TM, Program.WindowStyle);
            windowsDesktop1.LoadFromTM(TM);
            windowsDesktop1.LoadClassicColors(TM.Win32);
            retroDesktopColors1.LoadColors(TM);
            retroDesktopColors1.LoadMetrics(TM);
        }

        /// <summary>
        /// Apply Command Prompt preview with the specified console settings.
        /// </summary>
        /// <param name="CMD"></param>
        /// <param name="Console"></param>
        /// <param name="PS"></param>
        public void ApplyCMDPreview(WinCMD CMD, Theme.Structures.Console Console, bool PS)
        {
            CMD.CMD_ColorTable00 = Console.ColorTable00;
            CMD.CMD_ColorTable01 = Console.ColorTable01;
            CMD.CMD_ColorTable02 = Console.ColorTable02;
            CMD.CMD_ColorTable03 = Console.ColorTable03;
            CMD.CMD_ColorTable04 = Console.ColorTable04;
            CMD.CMD_ColorTable05 = Console.ColorTable05;
            CMD.CMD_ColorTable06 = Console.ColorTable06;
            CMD.CMD_ColorTable07 = Console.ColorTable07;
            CMD.CMD_ColorTable08 = Console.ColorTable08;
            CMD.CMD_ColorTable09 = Console.ColorTable09;
            CMD.CMD_ColorTable10 = Console.ColorTable10;
            CMD.CMD_ColorTable11 = Console.ColorTable11;
            CMD.CMD_ColorTable12 = Console.ColorTable12;
            CMD.CMD_ColorTable13 = Console.ColorTable13;
            CMD.CMD_ColorTable14 = Console.ColorTable14;
            CMD.CMD_ColorTable15 = Console.ColorTable15;
            CMD.CMD_PopupForeground = Console.PopupForeground;
            CMD.CMD_PopupBackground = Console.PopupBackground;
            CMD.CMD_ScreenColorsForeground = Console.ScreenColorsForeground;
            CMD.CMD_ScreenColorsBackground = Console.ScreenColorsBackground;

            if (!Console.FontRaster)
            {
                GDI32.LogFont logFont = new()
                {
                    lfFaceName = Console.FaceName,
                    lfHeight = -Console.PixelHeight,
                    lfWidth = Console.PixelWidth,
                    lfWeight = Console.FontWeight
                };

                try
                {
                    CMD.Font = Font.FromLogFont(logFont);
                }
                catch
                {

                }
            }

            CMD.PowerShell = PS;
            CMD.Raster = Console.FontRaster;

            string key = $"{Console.PixelWidth}x{Console.PixelHeight}";
            if (key == "4x6") CMD.RasterSize = WinCMD.Raster_Sizes._4x6;
            else if (key == "6x8") CMD.RasterSize = WinCMD.Raster_Sizes._6x8;
            else if (key == "6x9") CMD.RasterSize = WinCMD.Raster_Sizes._6x8;
            else if (key == "8x8") CMD.RasterSize = WinCMD.Raster_Sizes._8x8;
            else if (key == "8x9") CMD.RasterSize = WinCMD.Raster_Sizes._8x8;
            else if (key == "16x8") CMD.RasterSize = WinCMD.Raster_Sizes._16x8;
            else if (key == "5x12") CMD.RasterSize = WinCMD.Raster_Sizes._5x12;
            else if (key == "7x12") CMD.RasterSize = WinCMD.Raster_Sizes._7x12;
            else if (key == "8x12") CMD.RasterSize = WinCMD.Raster_Sizes._8x12;
            else if (key == "16x12") CMD.RasterSize = WinCMD.Raster_Sizes._16x12;
            else if (key == "12x16") CMD.RasterSize = WinCMD.Raster_Sizes._12x16;
            else if (key == "10x18") CMD.RasterSize = WinCMD.Raster_Sizes._10x18;
            else CMD.RasterSize = WinCMD.Raster_Sizes._8x12; // default

            CMD.Refresh();
        }

        /// <summary>
        /// Load cursors preview from the specified theme manager.
        /// </summary>
        /// <param name="TM"></param>
        public void LoadCursorsFromTM(Manager TM)
        {
            CursorTM_to_Cursor(Arrow, TM.Cursors.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursors.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursors.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursors.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursors.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursors.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursors.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursors.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursors.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursors.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursors.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursors.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursors.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursors.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursors.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursors.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursors.Cursor_Cross);

            foreach (CursorControl i in Cursors_Container.Controls)
            {
                i.Prop_Scale = TM.Cursors.Size / 32f;
                i.Width = (int)(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }
        }

        /// <summary>
        /// Converts the specified theme manager cursor to the specified cursor control.
        /// </summary>
        /// <param name="CursorControl"></param>
        /// <param name="Cursor"></param>
        public void CursorTM_to_Cursor(CursorControl CursorControl, Theme.Structures.Cursor Cursor)
        {
            CursorControl.Prop_ArrowStyle = Cursor.ArrowStyle;
            CursorControl.Prop_CircleStyle = Cursor.CircleStyle;
            CursorControl.Prop_PrimaryColor1 = Cursor.PrimaryColor1;
            CursorControl.Prop_PrimaryColor2 = Cursor.PrimaryColor2;
            CursorControl.Prop_PrimaryColorGradient = Cursor.PrimaryColorGradient;
            CursorControl.Prop_PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode;
            CursorControl.Prop_PrimaryNoise = Cursor.PrimaryColorNoise;
            CursorControl.Prop_PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity;
            CursorControl.Prop_SecondaryColor1 = Cursor.SecondaryColor1;
            CursorControl.Prop_SecondaryColor2 = Cursor.SecondaryColor2;
            CursorControl.Prop_SecondaryColorGradient = Cursor.SecondaryColorGradient;
            CursorControl.Prop_SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode;
            CursorControl.Prop_SecondaryNoise = Cursor.SecondaryColorNoise;
            CursorControl.Prop_SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity;
            CursorControl.Prop_LoadingCircleBack1 = Cursor.LoadingCircleBack1;
            CursorControl.Prop_LoadingCircleBack2 = Cursor.LoadingCircleBack2;
            CursorControl.Prop_LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient;
            CursorControl.Prop_LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode;
            CursorControl.Prop_LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity;
            CursorControl.Prop_LoadingCircleHot1 = Cursor.LoadingCircleHot1;
            CursorControl.Prop_LoadingCircleHot2 = Cursor.LoadingCircleHot2;
            CursorControl.Prop_LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient;
            CursorControl.Prop_LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode;
            CursorControl.Prop_LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity;
            CursorControl.Prop_Shadow_Enabled = Cursor.Shadow_Enabled;
            CursorControl.Prop_Shadow_Color = Cursor.Shadow_Color;
            CursorControl.Prop_Shadow_Blur = Cursor.Shadow_Blur;
            CursorControl.Prop_Shadow_Opacity = Cursor.Shadow_Opacity;
            CursorControl.Prop_Shadow_OffsetX = Cursor.Shadow_OffsetX;
            CursorControl.Prop_Shadow_OffsetY = Cursor.Shadow_OffsetY;
        }

        #endregion

        #region Store form events

        private async void Store_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;         // Prevent exception error of cross-thread

            RemoveAllStoreItems(store_container);

            Tabs.SelectedIndex = 0;
            back_btn.Visible = false;

            using (Bitmap bmp = User.ProfilePicture.Resize(24, 24))
            {
                Invoke(() =>
                {
                    avatar_btn.Image = bmp.ToCircular();
                });
            }

            CenterToScreen();

            FinishedLoadingInitialTMs = false;
            _Shown = false;

            this.LoadLanguage();
            ApplyStyle(this, true);
            this.DropEffect(default, true, DWM.BackdropStyles.Acrylic);

            ThemesFetcher.RunWorkerAsync();

            this.DoubleBuffer();

            Apply_btn.Image = Forms.Home.apply_btn.Image;
            labelAlt2.Text = string.Format(Program.Lang.Strings.Store.WontWork_Protocol, OS.WXP ? Program.Lang.Strings.Windows.WXP : Program.Lang.Strings.Windows.WVista);
            if (OS.WXP || OS.WVista) Tabs.SelectedIndex = 4;

            themeSize_lbl.Font = Fonts.Console;
            respacksize_lbl.Font = Fonts.Console;
            desc_txt.Font = Fonts.ConsoleLarge;
            ver_lbl.Font = Fonts.Console;

            os_12.Image = Assets.Store.DesignedFor12;
            os_11.Image = Assets.Store.DesignedFor11;
            os_10.Image = Assets.Store.DesignedFor10;
            os_81.Image = Assets.Store.DesignedFor81;
            os_8.Image = Assets.Store.DesignedFor8;
            os_7.Image = Assets.Store.DesignedFor7;
            os_vista.Image = Assets.Store.DesignedForVista;
            os_xp.Image = Assets.Store.DesignedForXP;

            SetAspectsIcons();

            if (User.GitHub_LoggedIn)
            {
                UpdateLoginData();
            }

            groupBox4.UpdatePattern(Program.TM.Info.Pattern);

            Config.DarkModeChanged += SetAspectsIcons;
        }

        void SetAspectsIcons()
        {
            aspect_winTheme.Image?.Dispose();
            aspect_winTheme.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_WindowsTheme : Assets.Store.StoreAspect_WindowsTheme.Invert();

            aspect_lockScreen.Image?.Dispose();
            aspect_lockScreen.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_LockScreen : Assets.Store.StoreAspect_LockScreen.Invert();

            aspect_classicColors.Image?.Dispose();
            aspect_classicColors.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_ClassicColors : Assets.Store.StoreAspect_ClassicColors.Invert();

            aspect_cursors.Image?.Dispose();
            aspect_cursors.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Cursors : Assets.Store.StoreAspect_Cursors.Invert();

            aspect_Metrics.Image?.Dispose();
            aspect_Metrics.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Metrics : Assets.Store.StoreAspect_Metrics.Invert();

            aspect_cmd.Image?.Dispose();
            aspect_cmd.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_CMD : Assets.Store.StoreAspect_CMD.Invert();

            aspect_ps86.Image?.Dispose();
            aspect_ps86.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_PS : Assets.Store.StoreAspect_PS.Invert();

            aspect_ps64.Image?.Dispose();
            aspect_ps64.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_PS64 : Assets.Store.StoreAspect_PS64.Invert();

            aspect_terminal.Image?.Dispose();
            aspect_terminal.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Terminal : Assets.Store.StoreAspect_Terminal.Invert();

            aspect_terminalPreview.Image?.Dispose();
            aspect_terminalPreview.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_TerminalPreview : Assets.Store.StoreAspect_TerminalPreview.Invert();

            aspect_wallpaper.Image?.Dispose();
            aspect_wallpaper.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Wallpaper : Assets.Store.StoreAspect_Wallpaper.Invert();

            aspect_effects.Image?.Dispose();
            aspect_effects.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Effects : Assets.Store.StoreAspect_Effects.Invert();

            aspect_sounds.Image?.Dispose();
            aspect_sounds.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Sounds : Assets.Store.StoreAspect_Sounds.Invert();

            aspect_screenSaver.Image?.Dispose();
            aspect_screenSaver.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_ScreenSaver : Assets.Store.StoreAspect_ScreenSaver.Invert();

            aspect_altTab.Image?.Dispose();
            aspect_altTab.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_AltTab : Assets.Store.StoreAspect_AltTab.Invert();

            aspect_icons.Image?.Dispose();
            aspect_icons.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Icons : Assets.Store.StoreAspect_Icons.Invert();

            aspect_accessibility.Image?.Dispose();
            aspect_accessibility.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_Accessibility : Assets.Store.StoreAspect_Accessibility.Invert();

            aspect_winPaletterAppTheme.Image?.Dispose();
            aspect_winPaletterAppTheme.Image = Program.Style.DarkMode ? Assets.Store.StoreAspect_WinPaletterAppTheme : Assets.Store.StoreAspect_WinPaletterAppTheme.Invert();
        }

        private void Store_Shown(object sender, EventArgs e)
        {
            _Shown = true;
            if (Program.Settings.Store.ShowNewXPIntro) Forms.Store_Intro_New.ShowDialog();
        }

        private void Store_FormClosing(object sender, FormClosingEventArgs e)
        {
            ThemesFetcher.CancelAsync();
            RemoveAllStoreItems(store_container);
            Tabs.SelectedIndex = 0;
        }

        private void Store_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Manager TMx in TMList.Values) TMx?.Dispose();
            TMList.Clear();
            selectedItem?.Dispose();
            foreach (CursorControl i in AnimateList) i?.Dispose();
            AnimateList.Clear();
            DM?.Dispose();
        }

        private void Store_ParentChanged(object sender, EventArgs e)
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

        #endregion

        #region Backgroundworkers to load Store themes managers

        /// <summary>
        /// Fetches themes from the online store.
        /// </summary>
        private void OnlineMode()
        {
            // Flush DNS cache to ensure no outdated records are used
            Dnsapi.DnsFlushResolverCache();

            // Initialize empty lists for storing responses, repository URLs, theme items, and theme data
            List<string> response = [];
            List<string> repos_list = [];
            List<string> items = [];
            List<ThemeData> allThemes = []; // Store all themes to be process later

            // Ping the online repositories to check their availability
            foreach (string DB in Program.Settings.Store.Online_Repositories)
            {
                string repoUrl = DB.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? DB : $"https://{DB}";

                // If repository is reachable, add it to the list
                if (Program.Ping(repoUrl))
                {
                    repos_list.Add(repoUrl);
                }
            }

            // Iterate through each reachable repository
            foreach (string DB in repos_list)
            {
                // Generate a folder name based on the repository URL
                string reposName;
                if (DB.ToUpper().Contains("GITHUB.COM"))
                {
                    string[] x = DB.Replace("https://", string.Empty).Replace("http://", string.Empty).Split('/');
                    reposName = $"{x[1]}_{x[2]}";
                    reposName = string.Join("_", reposName.Split(Path.GetInvalidFileNameChars()));
                }
                else
                {
                    reposName = string.Join("_", DB.Replace("https://", string.Empty).Replace("http://", string.Empty).Split(Path.GetInvalidFileNameChars()));
                }

                // Access the repository and fetch the theme data
                response.Clear();
                response = [.. DM.ReadString(DB).Split('\n')];
                items.Clear();

                // Filter valid theme items based on a certain format
                foreach (string item in response)
                {
                    // Check if the item is valid and if it contains theme data
                    if (item.Contains("|") && item.Split('|').Length >= 3 && item.Split('|').All(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        items.Add(item);
                    }
                }

                // Collect theme data into a list for processing later
                foreach (string item in items)
                {
                    string[] itemDetails = item.Split('|');
                    allThemes.Add(new ThemeData
                    {
                        URL_ThemeFile = itemDetails[2],
                        URL_PackFile = itemDetails.Length == 4 ? itemDetails[3] : string.Empty,
                        MD5_ThemeFile = itemDetails[0].ToUpper(),
                        MD5_PackFile = itemDetails[1].ToUpper(),
                        ReposName = reposName,
                        DB = DB
                    });
                }
            }

            // Now process all themes collected from all repositories
            ThemesFetcher.ReportProgress(0);
            BeginInvoke(() => ProgressBar1.Visible = true);

            int i = 0;
            int totalProgress = allThemes.Count;

            // Loop through the collected theme data
            foreach (ThemeData theme in allThemes)
            {
                // Create a folder for storing the theme
                if (!Directory.Exists(theme.FolderPath)) Directory.CreateDirectory(theme.FolderPath);

                string fileName = theme.FileName;

                // Download the theme file if it doesn't exist or if MD5 mismatch occurs
                DownloadThemeFile(theme);

                // Create StoreItem if the theme is valid and of correct format
                if (File.Exists(theme.FullPath) && Manager.GetEdition(theme.FullPath) == Manager.Editions.JSON)
                {
                    AddThemeToStore(theme);
                }

                // Update progress bar
                i++;
                if (totalProgress > 0) ThemesFetcher.ReportProgress((int)(i / (float)totalProgress * 100f));
            }

            // Hide progress bar when done
            BeginInvoke(() => ProgressBar1.Visible = false);

            if (store_container.Controls.Count == 0 || allThemes.Count == 0)
            {
                Tabs.SelectedIndex = 3;
            }

            // Mark the process as finished
            FinishedLoadingInitialTMs = true;
        }

        /// <summary>
        /// Represents the theme data to be processed later.
        /// </summary>
        private class ThemeData
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ThemeData"/> class.
            /// </summary>
            public ThemeData()
            {

            }

            /// <summary>
            /// MD5 hash of the theme file
            /// </summary>
            public string MD5_ThemeFile { get; set; }

            /// <summary>
            /// MD5 hash of the pack file
            /// </summary>
            public string MD5_PackFile { get; set; }

            /// <summary>
            /// URL of the theme file
            /// </summary>
            public string URL_ThemeFile
            {
                get => url_themeFile;
                set
                {
                    if (value != url_themeFile)
                    {
                        url_themeFile = value;
                        fileName = value.Replace("?raw=true", string.Empty).Split('/').Last();
                        dir = ValidFolderPath();
                    }
                }
            }
            private string url_themeFile;

            /// <summary>
            /// URL of the pack file
            /// </summary>
            public string URL_PackFile { get; set; }

            /// <summary>
            /// Name of the repository
            /// </summary>
            public string ReposName
            {
                get => reposName;
                set
                {
                    if (value != reposName)
                    {
                        reposName = value;
                        dir = ValidFolderPath();
                    }
                }
            }
            private string reposName;

            /// <summary>
            /// URL of the repository
            /// </summary>
            public string DB { get; set; }

            /// <summary>
            /// Returns the correct file name from the URL.
            /// </summary>
            public string FileName => fileName;
            private string fileName;

            /// <summary>
            /// Returns the correct folder path for storing the theme file.
            /// </summary>
            public string FolderPath => dir;
            private string dir;

            /// <summary>
            /// Returns the full path of the theme file.
            /// </summary>
            public string FullPath => $"{FolderPath}\\{FileName}";

            /// <summary>
            /// Returns the correct folder path for storing the theme file.
            /// </summary>
            /// <returns></returns>
            private string ValidFolderPath()
            {
                string temp = URL_ThemeFile.Replace("?raw=true", string.Empty);
                string fileName = temp.Split('/').Last();
                temp = temp.Replace($"/{fileName}", string.Empty);
                string folderName = temp.Split('/').Last();
                string dir = SysPaths.StoreCache;

                if (!string.IsNullOrWhiteSpace(folderName)) dir += $@"\{ReposName}\{folderName}";

                return dir;
            }
        }

        /// <summary>
        /// Download the theme file if not present or if MD5 mismatch is found.
        /// </summary>
        private void DownloadThemeFile(ThemeData theme)
        {
            if (File.Exists(theme.FullPath))
            {
                // If the theme file exists, check its MD5 hash
                if ((Program.CalculateMD5(theme.FullPath) ?? string.Empty) != theme.MD5_ThemeFile)
                {
                    File.Delete(theme.FullPath); // Delete the old file if MD5 doesn't match
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, string.Format(Program.Lang.Strings.Store.UpdateTheme, theme.FileName, theme.URL_ThemeFile));
                    Download(theme);
                }
            }
            else
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, string.Format(Program.Lang.Strings.Store.DownloadTheme, theme.FileName, theme.URL_ThemeFile));
                Download(theme);
            }
        }

        /// <summary>
        /// Attempt to download the file from the given URL.
        /// </summary>
        private void Download(ThemeData theme)
        {
            DM.DownloadFile(theme.URL_ThemeFile, theme.FullPath);
        }

        /// <summary>
        /// Add the theme to the store container.
        /// </summary>
        private void AddThemeToStore(ThemeData theme)
        {
            try
            {
                using (Manager TM = new(Manager.Source.File, theme.FullPath, true, true))
                {
                    StoreItem storeItem = new()
                    {
                        FileName = theme.FullPath,
                        TM = TM,
                        MD5_ThemeFile = theme.MD5_ThemeFile,
                        MD5_PackFile = theme.MD5_PackFile,
                        DoneByWinPaletter = theme.DB.ToUpper() == Links.Store_MainDB.ToUpper(),
                        Size = new(w, h),
                        URL_ThemeFile = theme.URL_ThemeFile,
                        URL_PackFile = theme.URL_PackFile
                    };

                    if (storeItem.DoneByWinPaletter) storeItem.TM.Info.Author = Application.ProductName;

                    storeItem.Click += StoreItem_Clicked;
                    storeItem.ThemeManagerChanged += StoreItem_ThemeManagerChanged;

                    BeginInvoke(() => store_container.Controls.Add(storeItem));
                }
            }
            catch
            {
                // Ignore errors when adding theme to store
            }
        }

        /// <summary>
        /// Loads themes from specified folders in offline mode, updating the UI to reflect progress.
        /// </summary>
        /// <remarks>This method processes theme files with the ".wpth" extension found in the specified
        /// folders. It updates the UI to show progress and loads each theme into the application. If a theme cannot be
        /// loaded, it is silently ignored. The method also updates the UI to reflect the loading status and
        /// progress.</remarks>
        /// <param name="folders">An array of folder paths to search for theme files. If <see langword="null"/>, the default offline
        /// directories specified in the application settings are used.</param>
        private void OfflineMode(string[] folders = null)
        {
            BeginInvoke(() =>
            {
                ProgressBar1.Visible = true;
                store_container.Visible = false;
            });

            int i = 0;
            int allProgress = 0;

            if (folders == null) folders = Program.Settings.Store.Offline_Directories;

            foreach (string folder in folders)
            {
                if (Directory.Exists(folder))
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Accessing themes from folder \"{folder}\"");
                    allProgress += Directory.GetFiles(folder, "*.wpth", Program.Settings.Store.Offline_SubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Count();
                }
            }

            allProgress *= 2;

            foreach (string folder in folders)
            {
                if (Directory.Exists(folder))
                {
                    foreach (string file in Directory.GetFiles(folder, "*.wpth", Program.Settings.Store.Offline_SubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            if (!TMList.ContainsKey(file))
                            {

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Enumerating themes: \"{file}\"");

                                using (Manager TMx = new(Manager.Source.File, file, true, true))
                                {
                                    TMList.Add(file, TMx);
                                }
                            }
                        }
                        catch { } // Ignore a theme couldn't be loaded as a store item (offline mode)

                        i += 1;

                        if (allProgress > 0)
                            ProgressBar1.Value = Math.Max(Math.Min((int)(i / (float)allProgress * 100f), ProgressBar1.Maximum), ProgressBar1.Minimum);
                    }
                }
            }

            foreach (KeyValuePair<string, Manager> StoreItem in TMList)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading theme \"{StoreItem.Value.Info.ThemeName}\"");

                StoreItem ctrl = new()
                {
                    FileName = StoreItem.Key,
                    TM = StoreItem.Value,
                    MD5_ThemeFile = Program.CalculateMD5(StoreItem.Key),
                    DoneByWinPaletter = false,
                    Size = new(w, h),
                    URL_ThemeFile = new FileInfo(StoreItem.Key).FullName
                };

                if (ctrl.DoneByWinPaletter)
                    ctrl.TM.Info.Author = Application.ProductName;

                ctrl.Click += StoreItem_Clicked;
                ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;

                BeginInvoke(new Action(() => store_container.Controls.Add(ctrl)));

                i += 1;

                if (allProgress > 0)
                    ProgressBar1.Value = Math.Max(Math.Min((int)(i / (float)allProgress * 100f), ProgressBar1.Maximum), ProgressBar1.Minimum);
            }

            BeginInvoke(() =>
            {
                ProgressBar1.Visible = false;
                store_container.Visible = true;
            });

            TMList.Clear();

            FinishedLoadingInitialTMs = true;
        }

        private void FilesFetcher_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Program.Settings.Store.Online_or_Offline)
            {
                if (!Program.IsNetworkAvailable)
                {
                    StartedAsOnlineOrOffline = false;
                    Tabs.SelectedIndex = 3;
                }
                else
                {
                    StartedAsOnlineOrOffline = true;
                    OnlineMode();
                }
            }
            else
            {
                StartedAsOnlineOrOffline = false;
                OfflineMode();
            }
        }

        private void FilesFetcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar1.Value = Math.Max(Math.Min(e.ProgressPercentage, ProgressBar1.Maximum), ProgressBar1.Minimum);
        }

        private void FilesFetcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        #endregion

        #region Store item events

        /// <summary>
        /// Occurs when the store item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoreItem_Clicked(object sender, EventArgs e)
        {
            switch ((e as MouseEventArgs).Button)
            {
                case MouseButtons.Right:
                    {
                        // Hide a hover preview
                        using (WindowsDesktop windowsDesktop = new() { Size = windowsDesktop1.Size })
                        {
                            Forms.Store_Hover.Close();
                            selectedItem = (StoreItem)sender;
                            Manager TMx = selectedItem.TM;
                            Forms.Store_Hover.Show();

                            windowsDesktop.WindowStyle = Program.WindowStyle;
                            windowsDesktop.BackgroundImage = Program.FetchSuitableWallpaper(TMx, Program.WindowStyle);
                            windowsDesktop.LoadFromTM(TMx);
                            windowsDesktop.LoadClassicColors(TMx.Win32);

                            windowsDesktop.Classic = false;
                            Forms.Store_Hover.img0 = windowsDesktop.ToBitmap();
                            windowsDesktop.Classic = true;
                            Forms.Store_Hover.img1 = windowsDesktop.ToBitmap();
                            Forms.Store_Hover.BackgroundImage = Forms.Store_Hover.img0;
                        }
                        break;
                    }

                default:
                    selectedItem = sender as StoreItem;
                    Cursor = Cursors.AppStarting;
                    groupBox5.UpdatePattern(selectedItem.TM.Info.Pattern);

                    Program.Animator.ShowSync(back_btn);
                    Program.Animator.HideSync(Tabs);
                    search_panel.Visible = false;

                    labelAlt3.Text = selectedItem.TM.Info.ThemeName;
                    author_lbl.Text = selectedItem.TM.Info.Author;
                    ver_lbl.Text = selectedItem.TM.Info.ThemeVersion;

                    Config style = new(selectedItem.TM.AppTheme.AccentColor, selectedItem.TM.AppTheme.SecondaryColor, selectedItem.TM.AppTheme.TertiaryColor, selectedItem.TM.AppTheme.DisabledColor, selectedItem.TM.AppTheme.BackColor, selectedItem.TM.AppTheme.DisabledBackColor, selectedItem.TM.AppTheme.DarkMode, selectedItem.TM.AppTheme.RoundCorners, selectedItem.TM.AppTheme.Animations);
                    Color accentForeColor = style.Schemes.Main.Colors.ForeColor_Accent;
                    Color accentBackColor = style.Schemes.Main.Colors.BackColor;

                    accentForeColor = Program.Style.DarkMode
                        ? (accentForeColor.IsDark() ? accentForeColor.Light() : accentForeColor)
                        : (!accentForeColor.IsDark() ? accentForeColor.Dark() : accentForeColor);

                    accentBackColor = Program.Style.DarkMode
                        ? (accentBackColor.IsDark() ? accentBackColor.Light() : accentBackColor)
                        : (!accentBackColor.IsDark() ? accentBackColor.Dark() : accentBackColor);

                    back_btn.CustomColor = style.Schemes.Main.Colors.Accent;
                    author_lbl.ForeColor = accentForeColor;
                    themeSize_lbl.ForeColor = accentForeColor;
                    respacksize_lbl.ForeColor = accentForeColor;
                    ver_lbl.ForeColor = accentBackColor;
                    ver_lbl.BackColor = accentForeColor;

                    FlowLayoutPanel1.ScrollControlIntoView(windowsDesktop1);

                    // Start processing the preview from the selected theme
                    Adjust_Preview(selectedItem.TM);
                    ApplyCMDPreview(CMD1, selectedItem.TM.CommandPrompt, false);
                    ApplyCMDPreview(CMD2, selectedItem.TM.PowerShellx86, true);
                    ApplyCMDPreview(CMD3, selectedItem.TM.PowerShellx64, true);
                    LoadCursorsFromTM(selectedItem.TM);

                    foreach (CursorControl i in Cursors_Container.Controls.OfType<CursorControl>().Where(i => i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy))
                    {
                        AnimateList.Add(i);
                    }

                    themeSize_lbl.Text = new FileInfo(selectedItem.FileName).Length.ToStringFileSize();

                    // Get if there is a pack file and its size to be calculated in a separate thread to make loading faster and doesn't freeze the UI
                    if (!string.IsNullOrWhiteSpace(selectedItem.MD5_PackFile) && selectedItem.MD5_PackFile != "0")
                    {
                        Task.Run(() =>
                        {
                            Invoke(() => progressBar_ResPack.Visible = true);
                            Invoke(() => respacksize_lbl.Visible = false);

                            long Pack_Size = DM.GetFileSizeFromUrl(selectedItem.URL_PackFile);

                            respacksize_lbl.SetText(Pack_Size > 0L ?
                                string.Format(Program.Lang.Strings.Store.ResourcesPackSize, Pack_Size.ToStringFileSize()) :
                                Program.Lang.Strings.Store.NoResourcesPack);

                            Invoke(() => progressBar_ResPack.Visible = false);
                            Invoke(() => respacksize_lbl.Visible = true);
                        });
                    }
                    else
                    {
                        respacksize_lbl.Text = Program.Lang.Strings.Store.NoResourcesPack;
                    }

                    string description = selectedItem.TM.Info.Description;
                    string[] lines = description.Split(["\r\n", "\n"], StringSplitOptions.None);

                    // process each line separately
                    for (int i = 0; i < lines.Length; i++)
                    {
                        // split line into words, remove words starting with #
                        string[] words = Regex.Split(lines[i], @"(\s+)"); // preserve spaces in array
                        lines[i] = string.Concat(
                            words.Where(w => !Regex.IsMatch(w, @"^\s*#")) // remove words starting with #
                        ).TrimEnd(); // trim only line-ending spaces
                    }

                    // join lines back with original line breaks
                    desc_txt.Text = string.Join(Environment.NewLine, lines).Trim();

                    // Extract tags from description
                    List<Control> toRemove = [.. flowLayoutPanel5.Controls.Cast<Control>().Where(c => c.Tag is not null)];

                    foreach (Control ctrl in toRemove)
                    {
                        flowLayoutPanel5.Controls.Remove(ctrl);
                        ctrl.Dispose();
                    }

                    bool foundATag = false;
                    string[] tags = selectedItem.TM.Info.Description.Split([' ', '\r', '\n', '\t'], StringSplitOptions.RemoveEmptyEntries);
                    foreach (string tag in tags)
                    {
                        if (tag.StartsWith("#"))
                        {
                            foundATag = true;

                            Label tagLabel = new()
                            {
                                Font = Fonts.Console,
                                ForeColor = accentBackColor,
                                BackColor = accentForeColor,
                                AutoSize = true,
                                TextAlign = ContentAlignment.TopCenter,
                                Text = tag.Remove(0, 1),
                                Anchor = AnchorStyles.Left,
                                Tag = 0,
                            };

                            flowLayoutPanel5.Controls.Add(tagLabel);
                        }
                    }
                    separatorV1.Visible = foundATag;

                    // Check if the theme is designed for the current version of WinPaletter or not and show an alert if not
                    if (Program.Version.CompareTo(selectedItem.TM.Info.AppVersion) != -1)
                    {
                        VersionAlert_lbl.Visible = false;
                    }
                    else
                    {
                        VersionAlert_lbl.Visible = true;
                        VersionAlert_lbl.Text = string.Format(Program.Lang.Strings.Store.LowAppVersionAlert, selectedItem.TM.Info.AppVersion, Program.Version);
                    }

                    // Get the list of supported OS

                    os_12.Visible = selectedItem.TM.Info.DesignedFor_Win12;
                    os_11.Visible = selectedItem.TM.Info.DesignedFor_Win11;
                    os_10.Visible = selectedItem.TM.Info.DesignedFor_Win10;
                    os_81.Visible = selectedItem.TM.Info.DesignedFor_Win81;
                    os_8.Visible = selectedItem.TM.Info.DesignedFor_Win8;
                    os_7.Visible = selectedItem.TM.Info.DesignedFor_Win7;
                    os_vista.Visible = selectedItem.TM.Info.DesignedFor_WinVista;
                    os_xp.Visible = selectedItem.TM.Info.DesignedFor_WinXP;

                    // Get Windows aspects that will be modified
                    aspect_winTheme.Visible = selectedItem.TM.Windows12.Enabled && OS.W12
                                           || selectedItem.TM.Windows11.Enabled && OS.W11
                                           || selectedItem.TM.Windows10.Enabled && OS.W10
                                           || selectedItem.TM.Windows81.Enabled && OS.W81
                                           || selectedItem.TM.Windows8.Enabled && OS.W8
                                           || selectedItem.TM.Windows7.Enabled && OS.W7
                                           || selectedItem.TM.WindowsVista.Enabled && OS.WVista
                                           || selectedItem.TM.WindowsXP.Enabled && OS.WXP;

                    aspect_lockScreen.Visible = selectedItem.TM.LogonUI12.Enabled && OS.W12
                                             || selectedItem.TM.LogonUI11.Enabled && OS.W11
                                             || selectedItem.TM.LogonUI10.Enabled && OS.W10
                                             || selectedItem.TM.LogonUI81.Enabled && OS.W81
                                             || selectedItem.TM.LogonUI7.Enabled && OS.W7
                                             || selectedItem.TM.LogonUIXP.Enabled && OS.WXP;

                    aspect_classicColors.Visible = selectedItem.TM.Win32.Enabled;
                    aspect_cursors.Visible = selectedItem.TM.Cursors.Enabled;
                    aspect_Metrics.Visible = selectedItem.TM.MetricsFonts.Enabled;
                    aspect_cmd.Visible = selectedItem.TM.CommandPrompt.Enabled;
                    aspect_ps86.Visible = selectedItem.TM.PowerShellx86.Enabled;
                    aspect_ps64.Visible = selectedItem.TM.PowerShellx64.Enabled;
                    aspect_terminal.Visible = selectedItem.TM.Terminal.Enabled;
                    aspect_terminalPreview.Visible = selectedItem.TM.TerminalPreview.Enabled;
                    aspect_wallpaper.Visible = selectedItem.TM.Wallpaper.Enabled;
                    aspect_effects.Visible = selectedItem.TM.WindowsEffects.Enabled;
                    aspect_sounds.Visible = selectedItem.TM.Sounds.Enabled;
                    aspect_screenSaver.Visible = selectedItem.TM.ScreenSaver.Enabled;
                    aspect_altTab.Visible = selectedItem.TM.AltTab.Enabled;
                    aspect_icons.Visible = selectedItem.TM.Icons.Enabled;
                    aspect_accessibility.Visible = selectedItem.TM.Accessibility.Enabled;
                    aspect_winPaletterAppTheme.Visible = selectedItem.TM.AppTheme.Enabled;

                    if (Program.WindowStyle == PreviewHelpers.WindowStyle.W12)
                        windowsDesktop1.Visible = selectedItem.TM.Windows12.Enabled;
                    else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W11)
                        windowsDesktop1.Visible = selectedItem.TM.Windows11.Enabled;
                    else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W10)
                        windowsDesktop1.Visible = selectedItem.TM.Windows10.Enabled;
                    else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W81)
                        windowsDesktop1.Visible = selectedItem.TM.Windows81.Enabled;
                    else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W8)
                        windowsDesktop1.Visible = selectedItem.TM.Windows8.Enabled;
                    else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W7)
                        windowsDesktop1.Visible = selectedItem.TM.Windows7.Enabled;
                    else if (Program.WindowStyle == PreviewHelpers.WindowStyle.WVista)
                        windowsDesktop1.Visible = selectedItem.TM.WindowsVista.Enabled;
                    else if (Program.WindowStyle == PreviewHelpers.WindowStyle.WXP)
                        windowsDesktop1.Visible = selectedItem.TM.WindowsXP.Enabled;

                    retroDesktopColors1.Visible = selectedItem.TM.Win32.Enabled;
                    CMD1.Visible = selectedItem.TM.CommandPrompt.Enabled;
                    CMD2.Visible = selectedItem.TM.PowerShellx86.Enabled;
                    CMD3.Visible = selectedItem.TM.PowerShellx64.Enabled;
                    Panel1.Visible = selectedItem.TM.Cursors.Enabled;

                    Tabs.SelectedIndex = 1;

                    Program.Animator.ShowSync(Tabs);

                    Cursor = Cursors.Default;
                    break;
            }
        }

        // Occurs when the theme manager of the store item is changed
        private void StoreItem_ThemeManagerChanged(object sender, EventArgs e)
        {
            if (FinishedLoadingInitialTMs)
            {
                Adjust_Preview((sender as StoreItem).TM);
            }
        }

        #endregion

        #region Methods\Functions

        #region    Store

        // Apply the theme of the selected store item
        private void Apply_Theme()
        {
            ref Settings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
            Appearance.CustomColors = selectedItem.TM.AppTheme.Enabled;
            Appearance.BackColor = selectedItem.TM.AppTheme.BackColor;
            Appearance.AccentColor = selectedItem.TM.AppTheme.AccentColor;
            Appearance.CustomTheme_DarkMode = selectedItem.TM.AppTheme.DarkMode;
            Appearance.RoundedCorners = selectedItem.TM.AppTheme.RoundCorners;
            ApplyStyle(null, true);

            using (Manager TMx = new(Manager.Source.File, selectedItem.FileName, false, true))
            {
                if (selectedItem.DoneByWinPaletter)
                    TMx.Info.Author = Application.CompanyName;

                Forms.ThemeLog.Apply_Theme(TMx, true);

                Program.TM_Original = TMx.Clone();
            }
        }

        // After a pack is doenloaded, there are two remaining action; either apply the theme or edit it.
        private void DoActionsAfterPackDownload()
        {
            if (ApplyOrEditToggle)
            {
                // Apply button is pressed
                Forms.Store_CPToggles.TM = selectedItem.TM;
                if (Forms.Store_CPToggles.ShowDialog() == DialogResult.OK)
                {
                    Apply_Theme();
                    if (selectedItem.DoneByWinPaletter) Program.TM.Info.Author = Application.CompanyName;
                    Program.TM = selectedItem.TM;
                    Program.TM_Original = Program.TM.Clone();
                    Forms.Home.LoadFromTM(Program.TM);
                    Forms.Home.Text = Path.GetFileName(selectedItem.FileName);
                    UpdateTitlebarColors();
                }
            }
            else
            {
                if (Forms.MainForm.ExitWithChangedFileResponse())
                {
                    // Edit button is pressed
                    Forms.MainForm.tabsContainer1.SelectedIndex = 0;
                    Program.TM_Original = Program.TM.Clone();
                    Program.TM = new(Manager.Source.File, selectedItem.FileName, false, true);
                    if (selectedItem.DoneByWinPaletter) Program.TM.Info.Author = Application.CompanyName;
                    Forms.Home.Text = Path.GetFileName(selectedItem.FileName);
                    ApplyStyle(Forms.Home);
                    Forms.Home.LoadFromTM(Program.TM);
                }
            }
        }

        /// <summary>
        /// Remove all themes in the store as a cleanup process
        /// </summary>
        /// <param name="Container"></param>
        private void RemoveAllStoreItems(FlowLayoutPanel Container)
        {
            int count = Container.Controls.Count - 1;
            for (int x = 0; x <= count; x++)
            {
                if (Container.Controls[0] is StoreItem storeItem && storeItem != null)
                {
                    storeItem.MouseClick -= StoreItem_Clicked;
                    storeItem.ThemeManagerChanged -= StoreItem_ThemeManagerChanged;

                    Container.Controls.Remove(storeItem);
                    storeItem?.Dispose();
                }
            }

            Container.Controls.Clear();
        }

        /// <summary>
        /// Do a search from the provided textbox and checked filters.
        /// </summary>
        private void PerformSearch()
        {
            // Normalize the search text: trim, remove all whitespace, upper-case
            string searchText = string.IsNullOrWhiteSpace(search_box.Text)
                ? string.Empty
                : new string(search_box.Text.Trim().Where(c => !char.IsWhiteSpace(c)).ToArray())
                    .ToUpperInvariant();

            if (string.IsNullOrEmpty(searchText))
                return;

            // Clear previous results
            RemoveAllStoreItems(search_results);

            int foundCount = 0;

            foreach (var item in store_container.Controls.OfType<StoreItem>())
            {
                // Normalize fields once per item
                string themeName = new string([.. item.TM.Info.ThemeName.Trim().Where(c => !char.IsWhiteSpace(c))]).ToUpperInvariant();
                string author = new string([.. item.TM.Info.Author.Trim().Where(c => !char.IsWhiteSpace(c))]).ToUpperInvariant();
                string desc = new string([.. item.TM.Info.Description.Trim().Where(c => !char.IsWhiteSpace(c))]).ToUpperInvariant();

                bool match =
                    (Program.Settings.Store.Search_ThemeNames && themeName.Contains(searchText)) ||
                    (Program.Settings.Store.Search_AuthorsNames && author.Contains(searchText)) ||
                    (Program.Settings.Store.Search_Descriptions && desc.Contains(searchText));

                if (!match) continue;

                foundCount++;

                // Create result control
                var ctrl = new StoreItem
                {
                    FileName = item.FileName,
                    TM = item.TM,
                    MD5_ThemeFile = Program.CalculateMD5(item.FileName),
                    DoneByWinPaletter = item.DoneByWinPaletter,
                    Size = new Size(w, h),
                    URL_ThemeFile = item.URL_ThemeFile
                };

                if (ctrl.DoneByWinPaletter)
                    ctrl.TM.Info.Author = Application.ProductName;

                ctrl.Click += StoreItem_Clicked;
                ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;

                BeginInvoke(new Action(() => search_results.Controls.Add(ctrl)));
            }

            titlebar_lbl.Text = string.Format(Program.Lang.Strings.Store.SearchCount, foundCount);
            Tabs.SelectTab(2);
        }

        #endregion

        #region    Helpers
        /// <summary>
        /// Change titlebar forecolors to make them suitable with theme
        /// </summary>
        private void UpdateTitlebarColors()
        {
            titlebar_lbl.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
            search_box.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
        }
        #endregion

        #endregion

        #region Timers
        private void Cursor_Timer_Tick(object sender, EventArgs e)
        {
            if (!_Shown) return;

            foreach (CursorControl i in AnimateList)
            {
                Increment = i.Prop_LoadingCircleHot_AnimationSpeed / 2f;

                i.Angle = Angle;
                i.Refresh();

                if (Angle + Increment >= 360f)
                    Angle = 0f;
                Angle += Increment;

                if (Angle == 180f & Cycles >= numericUpDown1.Value - 1)
                {
                    i.Angle = 180f;
                    Cursor_Timer.Enabled = false;
                    Cursor_Timer.Stop();
                }
                else if (Angle == 180f)
                {
                    Cycles += 1;
                }
            }
        }
        #endregion

        #region Buttons Events
        private void Back_btn_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(Tabs);

            RemoveAllStoreItems(search_results);

            titlebar_lbl.Font = new("Segoe UI", titlebar_lbl.Font.Size, titlebar_lbl.Font.Style);
            Tabs.SelectedIndex = 0;

            titlebar_lbl.Text = string.Empty;
            Program.Animator.ShowSync(Tabs);
        }

        #region    Applying row
        private void Apply_Edit_btn_Click(object sender, EventArgs e)
        {
            ApplyOrEditToggle = sender == Apply_btn;

            if (!string.IsNullOrWhiteSpace(selectedItem.TM.Info.License))
            {
                Forms.Store_ThemeLicense.TextBox1.Text = selectedItem.TM.Info.License;
                if (!(Forms.Store_ThemeLicense.ShowDialog() == DialogResult.OK))
                    return;
            }

            if (StartedAsOnlineOrOffline)
            {
                // Online mode

                string temp = selectedItem.URL_PackFile.Replace("?raw=true", string.Empty);
                string FileName = temp.Split('/').Last();
                temp = temp.Replace($"/{FileName}", string.Empty);
                string FolderName = temp.Split('/').Last();
                string Dir;
                if (File.Exists(selectedItem.FileName))
                {
                    Dir = new FileInfo(selectedItem.FileName).Directory.FullName;
                }
                else
                {
                    Dir = selectedItem.FileName.Replace($@"\{selectedItem.FileName.Split('\\').Last()}", string.Empty);
                }
                if (!Directory.Exists(Dir))
                    Directory.CreateDirectory(Dir);

                if (selectedItem.MD5_PackFile != "0")
                {
                    if (File.Exists($@"{Dir}\{FileName}") && (Program.CalculateMD5($"{Dir}\\{FileName}") ?? string.Empty) != (selectedItem.MD5_PackFile ?? string.Empty) || !File.Exists($@"{Dir}\{FileName}"))
                    {
                        Forms.Store_DownloadProgress.URL = selectedItem.URL_PackFile;
                        Forms.Store_DownloadProgress.File = $@"{Dir}\{FileName}";
                        Forms.Store_DownloadProgress.ThemeName = selectedItem.TM.Info.ThemeName;
                        Forms.Store_DownloadProgress.ThemeVersion = selectedItem.TM.Info.ThemeVersion;
                        if (Forms.Store_DownloadProgress.ShowDialog() == DialogResult.OK) DoActionsAfterPackDownload();
                    }
                    else
                    {
                        DoActionsAfterPackDownload();
                    }
                }

                else
                {
                    if (File.Exists($@"{Dir}\{FileName}"))
                    {
                        try
                        {
                            File.Delete($@"{Dir}\{FileName}");
                        }
                        catch { } // Couldn't delete the File
                    }

                    DoActionsAfterPackDownload();
                }
            }
            else
            {
                // Offline mode, do actions directly.
                DoActionsAfterPackDownload();
            }

        }

        private void RestartExplorer_Click(object sender, EventArgs e)
        {
            Program.RestartExplorer();
        }

        #endregion

        #region    Search
        private void Search_btn_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void Search_filter_btn_Click(object sender, EventArgs e)
        {
            Forms.Store_SearchFilter.ShowDialog();
        }

        #endregion

        #region    Cursors
        private void Cur_anim_btn_Click(object sender, EventArgs e)
        {
            Angle = 180f;
            Cycles = 0;
            Cursor_Timer.Enabled = true;
            Cursor_Timer.Start();
        }
        #endregion

        #endregion

        #region Major Tab
        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tabs.SelectedIndex != 0)
            {
                Program.Animator.ShowSync(back_btn);
            }
            else
            {
                Program.Animator.HideSync(back_btn);
            }

            search_panel.Visible = Tabs.SelectedIndex == 0 | Tabs.SelectedIndex == 2;
        }

        #endregion

        #region Others

        private void Search_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') PerformSearch();
        }

        private Point newPoint = new();
        private Point oldPoint = new();

        private void CustomTitlebar_MouseDown(object sender, MouseEventArgs e)
        {
            if (Parent is not TabPage) oldPoint = MousePosition - (Size)Location;
        }

        private void CustomTitlebar_MouseMove(object sender, MouseEventArgs e)
        {
            if (Parent is not TabPage && e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)oldPoint;
                Location = newPoint;
            }
        }

        /// <summary>
        /// Save the theme as a file to be used later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(selectedItem.TM.Info.License))
            {
                Forms.Store_ThemeLicense.TextBox1.Text = selectedItem.TM.Info.License;
                if (!(Forms.Store_ThemeLicense.ShowDialog() == DialogResult.OK))
                    return;
            }

            string selectedPath = string.Empty;

            if (!OS.WXP)
            {
                using (VistaFolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) selectedPath = FD.SelectedPath;
                }
            }
            else
            {
                using (FolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) selectedPath = FD.SelectedPath;
                }
            }

            if (!string.IsNullOrWhiteSpace(selectedPath) && Directory.Exists(selectedPath))
            {
                string themeFileName = new FileInfo(selectedItem.FileName).Name;
                string filename = $"{selectedPath}\\{themeFileName}";

                if (!Directory.Exists(selectedPath)) Directory.CreateDirectory(selectedPath);
                if (File.Exists(filename)) File.Delete(filename);

                File.Copy(selectedItem.FileName, filename);

                if (selectedItem.MD5_PackFile != "0")
                {
                    string themepackfilename = $@"{selectedPath}\{new FileInfo(selectedItem.FileName).Name}";
                    themepackfilename = themepackfilename.Replace(themepackfilename.Split('.').Last(), "wptp");

                    Forms.Store_DownloadProgress.URL = selectedItem.URL_PackFile;
                    Forms.Store_DownloadProgress.File = themepackfilename;
                    Forms.Store_DownloadProgress.ThemeName = selectedItem.TM.Info.ThemeName;
                    Forms.Store_DownloadProgress.ThemeVersion = selectedItem.TM.Info.ThemeVersion;
                    Forms.Store_DownloadProgress.ShowDialog();
                }
            }
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            StartedAsOnlineOrOffline = false;
            RemoveAllStoreItems(store_container);
            Tabs.SelectedIndex = 0;
            OfflineMode([SysPaths.StoreCache]);
            Cursor = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            StartedAsOnlineOrOffline = false;
            RemoveAllStoreItems(store_container);
            Tabs.SelectedIndex = 0;
            OfflineMode();
            Cursor = Cursors.Default;
        }

        private void titlebarExtender1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.SettingsX);
            Forms.SettingsX.TabControl1.SelectedIndex = 6;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Tabs.SelectedIndex = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void labelAlt4_Click(object sender, EventArgs e)
        {
            if (selectedItem.DoneByWinPaletter)
            {
                Process.Start(Links.RepositoryURL);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(selectedItem.TM.Info.AuthorSocialMediaLink)) return;

                if (MsgBox(Program.Lang.Strings.Store.AuthorURLRedirect, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, selectedItem.TM.Info.AuthorSocialMediaLink) == DialogResult.Yes)
                {
                    string url = selectedItem.TM.Info.AuthorSocialMediaLink;
                    if (!url.StartsWith("https://") || !url.StartsWith("http://")) url = $"https://{url}";
                    Process.Start(url);
                }
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            //progressBar2.Visible = true;
            //await loginManager.StartLoggingInAsync().ConfigureAwait(false);

            using (GitHubLogin logIn = new())
            {
                logIn.ShowDialog();
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            await Program.GitHub.SignOutAsync();
        }

        async void UpdateLoginData()
        {
            Bitmap avatar_bmp = null;

            if (User.GitHub_LoggedIn)
            {
                // Wait for avatar to exist
                if (User.GitHub_Avatar is null)
                {
                    await User.DownloadAvatarAsync();
                }

                if (User.GitHub_Avatar != null)
                {
                    avatar_bmp = User.GitHub_Avatar;
                }
            }

            if (avatar_bmp is null)
            {
                avatar_bmp = User.ProfilePicture;
            }

            using (Bitmap bmp = avatar_bmp.Resize(24, 24))
            {
                Invoke(() =>
                {
                    avatar_btn.Image = bmp.ToCircular();
                });
            }
        }
    }
}