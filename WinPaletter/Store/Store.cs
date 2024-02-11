using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    public partial class Store
    {

        #region Variables
        private bool StartedAsOnlineOrOffline = true;
        private bool FinishedLoadingInitialTMs;
        private Dictionary<string, Theme.Manager> TMList = new();

        private readonly int w = (int)Math.Round(528d * 0.6d);
        private readonly int h = (int)Math.Round(297d * 0.6d);

        private UI.Controllers.StoreItem hoveredItem;
        public UI.Controllers.StoreItem selectedItem;

        private bool _Shown = false;
        private readonly List<UI.Controllers.CursorControl> AnimateList = new();
        private float Angle = 180f;
        private readonly float Increment = 5f;
        private int Cycles = 0;
        private DownloadManager DM = new();

        private bool ApplyOrEditToggle = true;
        private Settings.Structures.Appearance oldAppearance;

        public Store()
        {
            InitializeComponent();
        }
        #endregion

        #region Preview Methods

        public void Adjust_Preview(Theme.Manager TM)
        {
            windowsDesktop1.WindowStyle = Program.WindowStyle;
            windowsDesktop1.BackgroundImage = Program.FetchSuitableWallpaper(TM, Program.WindowStyle);
            windowsDesktop1.LoadFromTM(TM);
            windowsDesktop1.LoadClassicColors(TM.Win32);
            retroDesktopColors1.LoadColors(TM);
            retroDesktopColors1.LoadMetrics(TM);
        }

        public void ApplyCMDPreview(UI.Simulation.WinCMD CMD, Theme.Structures.Console Console, bool PS)
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
                Font temp = Font.FromLogFont(new NativeMethods.GDI32.LogFont() { lfFaceName = Console.FaceName, lfWeight = Console.FontWeight });
                CMD.Font = new(temp.FontFamily, (int)Math.Round(Console.FontSize / 65536d), temp.Style);
            }

            CMD.PowerShell = PS;
            CMD.Raster = Console.FontRaster;
            switch (Console.FontSize)
            {
                case 393220:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._4x6;
                        break;
                    }

                case 524294:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._6x8;
                        break;
                    }


                case 524296:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case 524304:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x8;
                        break;
                    }

                case 786437:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._5x12;
                        break;
                    }

                case 786439:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._7x12;
                        break;
                    }

                case 0:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

                case 786448:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x12;
                        break;
                    }

                case 1048588:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._12x16;
                        break;
                    }

                case 1179658:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._10x18;
                        break;
                    }

                default:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

            }

            CMD.Refresh();
        }

        public void LoadCursorsFromTM(Theme.Manager TM)
        {
            CursorTM_to_Cursor(Arrow, TM.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursor_Cross);
        }

        public void CursorTM_to_Cursor(UI.Controllers.CursorControl CursorControl, Theme.Structures.Cursor Cursor)
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

        private void Store_Load(object sender, EventArgs e)
        {
            RemoveAllStoreItems(store_container);

            Tabs.SelectedIndex = 0;

            oldAppearance.RoundedCorners = Program.Settings.Appearance.RoundedCorners;
            oldAppearance.BackColor = Program.Settings.Appearance.BackColor;
            oldAppearance.AccentColor = Program.Settings.Appearance.AccentColor;
            oldAppearance.SecondaryColor = Program.Settings.Appearance.SecondaryColor;
            oldAppearance.TertiaryColor = Program.Settings.Appearance.TertiaryColor;
            oldAppearance.DisabledBackColor = Program.Settings.Appearance.DisabledBackColor;
            oldAppearance.DisabledColor = Program.Settings.Appearance.DisabledColor;
            oldAppearance.Animations = Program.Settings.Appearance.Animations;
            oldAppearance.CustomColors = Program.Settings.Appearance.CustomColors;
            oldAppearance.CustomTheme_DarkMode = Program.Settings.Appearance.CustomTheme_DarkMode;

            CenterToScreen();

            FinishedLoadingInitialTMs = false;
            _Shown = false;

            this.LoadLanguage();
            ApplyStyle(this, true);

            ThemesFetcher.RunWorkerAsync();

            CheckForIllegalCrossThreadCalls = false;         // Prevent exception error of cross-thread

            this.DoubleBuffer();

            Apply_btn.Image = Forms.Home.apply_btn.Image;
            RestartExplorer.Image = Forms.Home.restartExplorer_btn.Image;

            windowsDesktop1.BackgroundImage = Program.Wallpaper;

            Status_lbl.Font = Fonts.ConsoleMedium;
            themeSize_lbl.Font = Fonts.ConsoleLarge;
            respacksize_lbl.Font = Fonts.ConsoleLarge;
            desc_txt.Font = Fonts.ConsoleLarge;
            Theme_MD5_lbl.Font = Fonts.Console;
        }

        private void Store_Shown(object sender, EventArgs e)
        {
            _Shown = true;
            if (Program.Settings.Store.ShowTips) Forms.Store_Intro.ShowDialog();
        }

        private void Store_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;

            // To prevent effect of a store theme on the other forms
            Program.Style.RenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Program.Settings.Appearance = new();
            Program.Settings.Appearance.Load();

            GetRoundedCorners();
            GetDarkMode();
            ApplyStyle(this, true);

            Program.Settings.Appearance.CustomColors = oldAppearance.CustomColors;

            Status_pnl.Visible = true;

            ThemesFetcher.CancelAsync();
            store_container.Visible = false;
            RemoveAllStoreItems(store_container);
            store_container.Visible = true;
            Tabs.SelectedIndex = 0;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status_lbl.SetText(string.Empty);
        }

        private void Store_ParentChanged(object sender, EventArgs e)
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

        #endregion

        #region Backgroundworkers to load Store themes managers
        public void OnlineMode()
        {
            Dnsapi.DnsFlushResolverCache();

            List<string> response = new();
            response.Clear();
            List<string> repos_list = new();
            repos_list.Clear();
            List<string> items = new();
            items.Clear();

            // Check by Ping if repos DB URL is accessible or not
            foreach (string DB in Program.Settings.Store.Online_Repositories)
            {
                string var = string.Empty;

                if (!DB.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    var = $"https://{DB}";
                else
                    var = DB;

                Status_lbl.SetText(string.Format(Program.Lang.Store_Ping, var));

                if (Program.Ping(var))
                {
                    repos_list.Add(var);
                }
                else
                {
                    Status_lbl.SetText(string.Format(Program.Lang.Store_PingFailed, var));
                }

            }

            // Loop through all valid repos DBs
            foreach (string DB in repos_list)
            {

                // Try to generate a folder name dependent on the URL
                string reposName;
                if (DB.ToUpper().Contains("GITHUB.COM"))
                {
                    string[] x = DB.Replace("https://", string.Empty).Replace("http://", string.Empty).Split('/');
                    reposName = $"{x[1]}_{x[2]}";
                    reposName = string.Join("_", reposName.Split(System.IO.Path.GetInvalidFileNameChars()));
                }
                else
                {
                    reposName = string.Join("_", DB.Replace("https://", string.Empty).Replace("http://", string.Empty).Split(System.IO.Path.GetInvalidFileNameChars()));
                }

                // Get text of the DB from URL
                Status_lbl.SetText(string.Format(Program.Lang.Store_Accessing, DB));
                response.Clear();
                response = DM.ReadString(DB).CList();
                items.Clear();

                // Add valid lines (Correct format) in a themes list
                foreach (string item in response)
                {
                    bool valid = true;
                    if (item.Contains("|") && item.Split('|').Count() >= 3)
                    {
                        foreach (string x in item.Split('|'))
                        {
                            if (string.IsNullOrWhiteSpace(x))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        valid = false;
                    }

                    if (valid)
                        items.Add(item);

                }

                ThemesFetcher.ReportProgress(0);
                BeginInvoke(new Action(() => ProgressBar1.Visible = true));

                int i = 0;
                int allProgress = items.Count * 2;

                // Loop through valid lines from the themes list
                foreach (string item in items)
                {
                    string[] item_splitted = item.Split('|');

                    string MD5_ThemeFile = item_splitted[0].ToUpper();
                    string MD5_PackFile = item_splitted[1].ToUpper();
                    string URL_ThemeFile = item_splitted[2];
                    string URL_PackFile = string.Empty;
                    if (item_splitted.Count() == 4)
                        URL_PackFile = item_splitted[3];

                    // Create a folder inside AppData folder
                    string temp = URL_ThemeFile.Replace("?raw=true", string.Empty);
                    string FileName = temp.Split('/').Last();
                    temp = temp.Replace($"/{FileName}", string.Empty);
                    string FolderName = temp.Split('/').Last();
                    string Dir = PathsExt.StoreCache;
                    if (!string.IsNullOrWhiteSpace(FolderName))
                        Dir += $@"\{reposName}\{FolderName}";
                    if (!System.IO.Directory.Exists(Dir))
                        System.IO.Directory.CreateDirectory(Dir);

                    Status_lbl.SetText(string.Empty);

                    // Download the theme (*.wpth)
                    if (System.IO.File.Exists($@"{Dir}\{FileName}"))
                    {
                        // If it exists, check MD5, if it is changed, redownload the theme
                        if ((CalculateMD5($@"{Dir}\{FileName}") ?? string.Empty) != (MD5_ThemeFile ?? string.Empty))
                        {
                            System.IO.File.Delete($@"{Dir}\{FileName}");
                            Status_lbl.SetText(string.Format(Program.Lang.Store_UpdateTheme, FileName, URL_ThemeFile));
                            try
                            {
                                DM.DownloadFile(URL_ThemeFile, $"{Dir}\\{FileName}");
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        Status_lbl.SetText(string.Format(Program.Lang.Store_DownloadTheme, FileName, URL_ThemeFile));
                        try
                        {
                            DM.DownloadFile(URL_ThemeFile, $"{Dir}\\{FileName}");
                        }
                        catch { }
                    }

                    i += 1;
                    if (allProgress > 0) ThemesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));

                    // Convert themes managers into StoreItems, and exclude the old formats of OldFormat
                    if (System.IO.File.Exists($@"{Dir}\{FileName}") && Manager.GetFormat($@"{Dir}\{FileName}") == Manager.Format.JSON)
                    {
                        try
                        {
                            Status_lbl.SetText(string.Format(Program.Lang.Store_LoadingTheme, FileName));

                            using (Theme.Manager TM = new(Theme.Manager.Source.File, $@"{Dir}\{FileName}", true, true))
                            {
                                UI.Controllers.StoreItem ctrl = new()
                                {
                                    FileName = $@"{Dir}\{FileName}",
                                    TM = TM,
                                    MD5_ThemeFile = MD5_ThemeFile,
                                    MD5_PackFile = MD5_PackFile,
                                    DoneByWinPaletter = (DB.ToUpper() ?? string.Empty) == (Properties.Resources.Link_StoreMainDB.ToUpper() ?? string.Empty),
                                    Size = new(w, h),
                                    URL_ThemeFile = URL_ThemeFile,
                                    URL_PackFile = URL_PackFile
                                };

                                if (ctrl.DoneByWinPaletter)
                                    ctrl.TM.Info.Author = Application.ProductName;

                                ctrl.Click += StoreItem_Clicked;
                                ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;
                                ctrl.MouseEnter += StoreItem_MouseEnter;
                                ctrl.MouseLeave += StoreItem_MouseLeave;

                                BeginInvoke(new Action(() => store_container.Controls.Add(ctrl)));
                            }
                        }
                        catch (Exception) { }
                    }

                    Status_lbl.SetText(string.Empty);

                    i += 1;

                    if (allProgress > 0)
                        ThemesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));

                }

                // Finalizing
                BeginInvoke(new Action(() => ProgressBar1.Visible = false));

                TMList.Clear();
            }

            FinishedLoadingInitialTMs = true;
        }

        public void OfflineMode()
        {
            BeginInvoke(new Action(() =>
                {
                    ProgressBar1.Visible = true;
                    store_container.Visible = false;
                }));

            int i = 0;
            int allProgress = 0;


            foreach (string folder in Program.Settings.Store.Offline_Directories)
            {

                if (System.IO.Directory.Exists(folder))
                {
                    Status_lbl.SetText($"Accessing themes from folder \"{folder}\"");
                    allProgress += System.IO.Directory.GetFiles(folder, "*.wpth", Program.Settings.Store.Offline_SubFolders ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly).Count();
                }

            }

            allProgress *= 2;

            foreach (string folder in Program.Settings.Store.Offline_Directories)
            {

                if (System.IO.Directory.Exists(folder))
                {

                    foreach (string file in System.IO.Directory.GetFiles(folder, "*.wpth", Program.Settings.Store.Offline_SubFolders ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly))
                    {

                        try
                        {
                            if (!TMList.ContainsKey(file))
                            {

                                Status_lbl.SetText($"Enumerating themes: \"{file}\"");

                                using (Theme.Manager TMx = new(Theme.Manager.Source.File, file, true, true))
                                {
                                    TMList.Add(file, TMx);
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }

                        i += 1;

                        if (allProgress > 0)
                            ThemesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));
                    }
                }
            }


            foreach (KeyValuePair<string, Manager> StoreItem in TMList)
            {
                Status_lbl.SetText($"Loading theme \"{StoreItem.Value.Info.ThemeName}\"");

                UI.Controllers.StoreItem ctrl = new()
                {
                    FileName = StoreItem.Key,
                    TM = StoreItem.Value,
                    MD5_ThemeFile = CalculateMD5(StoreItem.Key),
                    DoneByWinPaletter = false,
                    Size = new(w, h),
                    URL_ThemeFile = new System.IO.FileInfo(StoreItem.Key).FullName
                };

                if (ctrl.DoneByWinPaletter)
                    ctrl.TM.Info.Author = Application.ProductName;

                ctrl.Click += StoreItem_Clicked;
                ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;
                ctrl.MouseEnter += StoreItem_MouseEnter;
                ctrl.MouseLeave += StoreItem_MouseLeave;

                BeginInvoke(new Action(() => store_container.Controls.Add(ctrl)));

                i += 1;

                if (allProgress > 0)
                    ThemesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));
            }

            BeginInvoke(new Action(() =>
                {
                    ProgressBar1.Visible = false;
                    store_container.Visible = true;
                }));

            Status_lbl.SetText(string.Empty);

            TMList.Clear();

            FinishedLoadingInitialTMs = true;
        }

        private void FilesFetcher_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (Program.Settings.Store.Online_or_Offline)
            {
                if (!Program.IsNetworkAvailable)
                {
                    Status_lbl.SetText(Program.Lang.Store_NoNetwork);

                    if (MsgBox(Program.Lang.Store_NoNetwork, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.Store_TryOffline) == DialogResult.Yes)
                    {
                        StartedAsOnlineOrOffline = false;
                        OfflineMode();
                        return;
                    }
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

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Program.Animator.HideSync(Status_pnl);
        }

        private void FilesFetcher_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                ProgressBar1.Value = Math.Max(Math.Min(e.ProgressPercentage, ProgressBar1.Maximum), ProgressBar1.Minimum);
            }
            catch
            {
            }
        }

        private void FilesFetcher_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        #endregion

        #region Store item events

        public void StoreItem_Clicked(object sender, EventArgs e)
        {
            switch (((MouseEventArgs)e).Button)
            {
                case MouseButtons.Right:
                    {
                        {
                            StoreItem temp = (UI.Controllers.StoreItem)sender;
                            Forms.Store_Hover.Close();

                            selectedItem = (UI.Controllers.StoreItem)sender;

                            Forms.Store_Hover.Show();

                            Adjust_Preview(temp.TM);
                            windowsDesktop1.Classic = false;
                            Forms.Store_Hover.img0 = windowsDesktop1.ToBitmap();
                            windowsDesktop1.Classic = true;
                            Forms.Store_Hover.img1 = windowsDesktop1.ToBitmap();
                            Forms.Store_Hover.BackgroundImage = Forms.Store_Hover.img0;
                        }

                        break;
                    }

                default:
                    {
                        selectedItem = (UI.Controllers.StoreItem)sender;
                        Cursor = Cursors.AppStarting;
                        StoreItem1.TM = selectedItem.TM;
                        StoreItem1.DoneByWinPaletter = selectedItem.DoneByWinPaletter;
                        Theme_MD5_lbl.Text = $"MD5: {selectedItem.MD5_ThemeFile}";

                        {
                            UI.Controllers.StoreItem StoreItem = selectedItem;
                            Program.Animator.HideSync(Tabs);
                            search_panel.Visible = false;

                            Titlebar_lbl.Text = $"{StoreItem.TM.Info.ThemeName} - {Program.Lang.By} {StoreItem.TM.Info.Author}";
                            if (Fonts.Exists(StoreItem.TM.MetricsFonts.CaptionFont.Name))
                            {
                                Titlebar_lbl.Font = new(StoreItem.TM.MetricsFonts.CaptionFont.Name, Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style);
                            }
                            else
                            {
                                Titlebar_lbl.Font = new("Segoe UI", Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style);
                            }

                            if (StoreItem.TM.AppTheme.Enabled)
                            {
                                Program.Settings.Appearance.CustomColors = StoreItem.TM.AppTheme.Enabled;
                                Program.Settings.Appearance.CustomTheme_DarkMode = StoreItem.TM.AppTheme.DarkMode;
                                Program.Settings.Appearance.RoundedCorners = StoreItem.TM.AppTheme.RoundCorners;
                                Program.Settings.Appearance.BackColor = StoreItem.TM.AppTheme.BackColor;
                                Program.Settings.Appearance.AccentColor = StoreItem.TM.AppTheme.AccentColor;
                                Program.Settings.Appearance.SecondaryColor = StoreItem.TM.AppTheme.SecondaryColor;
                                Program.Settings.Appearance.TertiaryColor = StoreItem.TM.AppTheme.TertiaryColor;
                                Program.Settings.Appearance.DisabledBackColor = StoreItem.TM.AppTheme.DisabledBackColor;
                                Program.Settings.Appearance.DisabledColor = StoreItem.TM.AppTheme.DisabledColor;
                                Program.Settings.Appearance.Animations = StoreItem.TM.AppTheme.Animations;

                                ApplyStyle(this, true);
                            }

                            if (StoreItem.TM.AppTheme.Enabled)
                            {
                                Label14.ForeColor = StoreItem.TM.AppTheme.DarkMode ? Color.White.CB((float)-0.3d) : Color.Black.CB(0.3f);
                            }
                            else
                            {
                                Label14.ForeColor = Program.Style.DarkMode ? Color.White.CB((float)-0.3d) : Color.Black.CB(0.3f);
                            }

                            back_btn.CustomColor = StoreItem.TM.Info.Color2;

                            Label6.ForeColor = Label14.ForeColor;
                            Theme_MD5_lbl.ForeColor = Label14.ForeColor;

                            FlowLayoutPanel1.ScrollControlIntoView(windowsDesktop1);

                            Adjust_Preview(StoreItem.TM);
                            ApplyCMDPreview(CMD1, StoreItem.TM.CommandPrompt, false);
                            ApplyCMDPreview(CMD2, StoreItem.TM.PowerShellx86, true);
                            ApplyCMDPreview(CMD3, StoreItem.TM.PowerShellx64, true);
                            LoadCursorsFromTM(StoreItem.TM);
                            Program.Style.RenderingHint = StoreItem.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                            foreach (UI.Controllers.CursorControl i in Cursors_Container.Controls.OfType<CursorControl>()
                                .Where(i => i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy))
                            {
                                AnimateList.Add(i);
                            }

                            themeSize_lbl.Text = new System.IO.FileInfo(StoreItem.FileName).Length.SizeString();

                            if (!string.IsNullOrWhiteSpace(StoreItem.MD5_PackFile) && StoreItem.MD5_PackFile != "0")
                            {
                                Task.Run(() =>
                                        {
                                            respacksize_lbl.SetText(Program.Lang.Store_Calculating);
                                            long Pack_Size = GetFileSizeFromURL(StoreItem.URL_PackFile);
                                            respacksize_lbl.SetText(Pack_Size > 0L ? Pack_Size.SizeString() : 0.SizeString());
                                        });
                            }
                            else
                            {
                                respacksize_lbl.Text = 0.SizeString();
                            }

                            desc_txt.Text = StoreItem.TM.Info.Description;

                            if (Program.Version.CompareTo(StoreItem.TM.Info.AppVersion) != -1)
                            {
                                VersionAlert_lbl.Visible = false;
                            }
                            else
                            {
                                VersionAlert_lbl.Visible = true;
                                VersionAlert_lbl.Text = string.Format(Program.Lang.Store_LowAppVersionAlert, StoreItem.TM.Info.AppVersion, Program.Version);
                            }

                            List<string> os_list = new();
                            os_list.Clear();

                            //if (StoreItem.TM.Info.DesignedFor_Win12) os_list.Add(Program.Lang.OS_Win12);

                            if (StoreItem.TM.Info.DesignedFor_Win11) os_list.Add(Program.Lang.OS_Win11);

                            if (StoreItem.TM.Info.DesignedFor_Win11) os_list.Add(Program.Lang.OS_Win11);

                            if (StoreItem.TM.Info.DesignedFor_Win10) os_list.Add(Program.Lang.OS_Win10);

                            if (StoreItem.TM.Info.DesignedFor_Win81) os_list.Add(Program.Lang.OS_Win81);

                            if (StoreItem.TM.Info.DesignedFor_Win7) os_list.Add(Program.Lang.OS_Win7);

                            if (StoreItem.TM.Info.DesignedFor_WinVista) os_list.Add(Program.Lang.OS_WinVista);

                            if (StoreItem.TM.Info.DesignedFor_WinXP) os_list.Add(Program.Lang.OS_WinXP);

                            string os_format = string.Empty;
                            if (os_list.Count == 1)
                            {
                                os_format = os_list[0];
                            }
                            else if (os_list.Count == 2)
                            {
                                os_format = $"{os_list[0]} && {os_list[1]}";
                            }
                            else if (os_list.Count > 2)
                            {
                                for (int i = 0, loopTo = os_list.Count - 3; i <= loopTo; i++)
                                    os_format += $"{os_list[i]}, ";
                                os_format += $"{os_list[os_list.Count - 2]} && {os_list[os_list.Count - 1]}";
                            }
                            SupportedOS_lbl.Text = os_format;
                            if (os_list.Count < 6)
                            {
                                Label26.Text = Program.Lang.Store_ThemeDesignedFor0;
                            }
                            else
                            {
                                Label26.Text = Program.Lang.Store_ThemeDesignedFor1;
                            }

                            if (StoreItem.TM.AppTheme.Enabled)
                            {
                                desc_txt.ForeColor = StoreItem.TM.AppTheme.DarkMode ? Color.White : Color.Black;
                            }
                            else
                            {
                                desc_txt.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
                            }

                            CMD1.Visible = StoreItem.TM.CommandPrompt.Enabled;
                            CMD2.Visible = StoreItem.TM.PowerShellx86.Enabled;
                            CMD3.Visible = StoreItem.TM.PowerShellx64.Enabled;
                            Panel1.Visible = StoreItem.TM.Cursor_Enabled;
                            author_url_button.Visible = !string.IsNullOrWhiteSpace(StoreItem.TM.Info.AuthorSocialMediaLink);

                            Tabs.SelectedIndex = 1;

                            Program.Animator.ShowSync(Tabs);
                        }

                        Cursor = Cursors.Default;
                        break;
                    }

            }
        }

        public void StoreItem_MouseEnter(object sender, EventArgs e)
        {
            hoveredItem = (UI.Controllers.StoreItem)sender;

        }

        public void StoreItem_MouseLeave(object sender, EventArgs e)
        {

        }

        public void StoreItem_ThemeManagerChanged(object sender, EventArgs e)
        {
            if (FinishedLoadingInitialTMs)
            {
                {
                    StoreItem temp = (UI.Controllers.StoreItem)sender;
                    Adjust_Preview(temp.TM);
                    temp.Refresh();
                }
            }
        }
        #endregion

        #region Methods\Functions

        #region    Store
        public void Apply_Theme()
        {
            ref Settings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
            Appearance.CustomColors = selectedItem.TM.AppTheme.Enabled;
            Appearance.BackColor = selectedItem.TM.AppTheme.BackColor;
            Appearance.AccentColor = selectedItem.TM.AppTheme.AccentColor;
            Appearance.CustomTheme_DarkMode = selectedItem.TM.AppTheme.DarkMode;
            Appearance.RoundedCorners = selectedItem.TM.AppTheme.RoundCorners;
            oldAppearance = Appearance;
            ApplyStyle(null, true);

            using (Theme.Manager TMx = new(Theme.Manager.Source.File, selectedItem.FileName, false, true))
            {
                if (selectedItem.DoneByWinPaletter)
                    TMx.Info.Author = Application.CompanyName;

                Forms.ThemeLog.Apply_Theme(TMx, true);

                Program.TM_Original = (Theme.Manager)TMx.Clone();
            }
        }

        public void DoActionsAfterPackDownload()
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
                    Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                    Forms.Home.LoadFromTM(Program.TM);
                    Forms.Home.Text = System.IO.Path.GetFileName(selectedItem.FileName);
                    UpdateTitlebarColors();
                }
            }
            else
            {
                // Edit button is pressed
                Forms.MainForm.tabsContainer1.SelectedIndex = 0;

                using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.file, Title = Program.Lang.Filter_SaveWinPaletterTheme })
                {
                    Forms.MainForm.ExitWithChangedFileResponse(dlg, null, null, null);
                }

                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                Program.TM = new(Theme.Manager.Source.File, selectedItem.FileName, false, true);
                if (selectedItem.DoneByWinPaletter) Program.TM.Info.Author = Application.CompanyName;
                Forms.Home.Text = System.IO.Path.GetFileName(selectedItem.FileName);
                Forms.Home.LoadFromTM(Program.TM);
            }
        }

        public void RemoveAllStoreItems(FlowLayoutPanel Container)
        {
            int count = Container.Controls.Count - 1;
            for (int x = 0; x <= count; x++)
            {
                if (Container.Controls[0] is StoreItem storeItem && storeItem != null)
                {
                    storeItem.MouseClick -= StoreItem_Clicked;
                    storeItem.ThemeManagerChanged -= StoreItem_ThemeManagerChanged;
                    storeItem.MouseEnter -= StoreItem_MouseEnter;
                    storeItem.MouseLeave -= StoreItem_MouseLeave;
                }

                Container.Controls[0].Dispose();
            }
            Container.Controls.Clear();
        }

        public void PerformSearch()
        {
            string search_text = search_box.Text.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper();

            if (string.IsNullOrWhiteSpace(search_text))
                return;

            Dictionary<string, UI.Controllers.StoreItem> lst = new();
            lst.Clear();

            foreach (StoreItem st_itm in store_container.Controls.OfType<UI.Controllers.StoreItem>())
                lst.Add(st_itm.FileName, st_itm);

            RemoveAllStoreItems(search_results);

            int found_sum = 0;

            foreach (KeyValuePair<string, StoreItem> st_item in lst)
            {
                if ((Program.Settings.Store.Search_ThemeNames && st_item.Value.TM.Info.ThemeName.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper().Contains(search_text)) | (Program.Settings.Store.Search_AuthorsNames && st_item.Value.TM.Info.Author.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper().Contains(search_text)) | (Program.Settings.Store.Search_Descriptions && st_item.Value.TM.Info.Description.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper().Contains(search_text)))

                {

                    found_sum += 1;

                    UI.Controllers.StoreItem ctrl = new()
                    {
                        FileName = st_item.Key,
                        TM = st_item.Value.TM,
                        MD5_ThemeFile = CalculateMD5(st_item.Key),
                        DoneByWinPaletter = st_item.Value.DoneByWinPaletter,
                        Size = new(w, h),
                        URL_ThemeFile = st_item.Value.URL_ThemeFile
                    };

                    if (ctrl.DoneByWinPaletter)
                        ctrl.TM.Info.Author = Application.ProductName;

                    ctrl.Click += StoreItem_Clicked;
                    ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;
                    ctrl.MouseEnter += StoreItem_MouseEnter;
                    ctrl.MouseLeave += StoreItem_MouseLeave;

                    BeginInvoke(new Action(() => search_results.Controls.Add(ctrl)));

                }
            }

            Titlebar_lbl.Text = $"Search results ({found_sum})";

            Tabs.SelectedIndex = 2;

            lst.Clear();
        }
        #endregion

        #region    Helpers
        private string CalculateMD5(string path)
        {
            if (System.IO.File.Exists(path))
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(System.IO.File.ReadAllBytes(path));
                    string result = BitConverter.ToString(hash).Replace("-", string.Empty);
                    return result.ToUpper();
                }
            }
            else
            {
                return "0";
            }

        }

        public long GetFileSizeFromURL(string url)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    long result = 0L;
                    WebRequest req = WebRequest.Create(url);
                    req.Method = "HEAD";
                    long contentLength = default;

                    using (WebResponse resp = req.GetResponse())
                    {

                        if (long.TryParse(resp.Headers.Get("Content-Length"), out contentLength))
                        {
                            result = contentLength;
                        }
                    }

                    return result;
                }
                else
                {
                    return 0L;
                }
            }
            catch
            {
                return 0L;
            }
        }

        public void UpdateTitlebarColors()
        {
            Titlebar_lbl.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
            search_box.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
        }
        #endregion

        #endregion

        #region Timers
        private void Cursor_Timer_Tick(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            try
            {
                foreach (UI.Controllers.CursorControl i in AnimateList)
                {
                    i.Angle = Angle;
                    i.Refresh();

                    if (Angle + Increment >= 360f)
                        Angle = 0f;
                    Angle += Increment;

                    if (Angle == 180f & Cycles >= 2)
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
            catch
            {
            }
        }
        #endregion

        #region Buttons Events
        private void Back_btn_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(Tabs);
            Program.Style.RenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (selectedItem is not null && selectedItem.TM.AppTheme.Enabled)
            {
                Program.Settings.Appearance = new();
                Program.Settings.Appearance.Load();

                GetRoundedCorners();
                GetDarkMode();
                ApplyStyle(this, true);
            }

            RemoveAllStoreItems(search_results);

            Titlebar_lbl.Font = new("Segoe UI", Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style);
            Tabs.SelectedIndex = 0;
            Program.Animator.HideSync(back_btn);

            Titlebar_lbl.Text = string.Empty;
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
                string temp = selectedItem.URL_PackFile.Replace("?raw=true", string.Empty);
                string FileName = temp.Split('/').Last();
                temp = temp.Replace($"/{FileName}", string.Empty);
                string FolderName = temp.Split('/').Last();
                string Dir;
                if (System.IO.File.Exists(selectedItem.FileName))
                {
                    Dir = new System.IO.FileInfo(selectedItem.FileName).Directory.FullName;
                }
                else
                {
                    Dir = selectedItem.FileName.Replace($@"\{selectedItem.FileName.Split('\\').Last()}", string.Empty);
                }
                if (!System.IO.Directory.Exists(Dir))
                    System.IO.Directory.CreateDirectory(Dir);

                if (selectedItem.MD5_PackFile != "0")
                {
                    if (System.IO.File.Exists($@"{Dir}\{FileName}") && (CalculateMD5($@"{Dir}\{FileName}") ?? string.Empty) != (selectedItem.MD5_PackFile ?? string.Empty) || !System.IO.File.Exists($@"{Dir}\{FileName}"))
                    {
                        try
                        {
                            Forms.Store_DownloadProgress.URL = selectedItem.URL_PackFile;
                            Forms.Store_DownloadProgress.File = $@"{Dir}\{FileName}";
                            Forms.Store_DownloadProgress.ThemeName = selectedItem.TM.Info.ThemeName;
                            Forms.Store_DownloadProgress.ThemeVersion = selectedItem.TM.Info.ThemeVersion;
                            if (Forms.Store_DownloadProgress.ShowDialog() == DialogResult.OK)
                                DoActionsAfterPackDownload();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        DoActionsAfterPackDownload();
                    }
                }

                else
                {
                    if (System.IO.File.Exists($@"{Dir}\{FileName}"))
                    {
                        try
                        {
                            FileSystem.Kill($@"{Dir}\{FileName}");
                        }
                        catch { }
                    }

                    DoActionsAfterPackDownload();
                }
            }
            else
            {
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

        private void Cur_tip_btn_Click(object sender, EventArgs e)
        {
            Program.ToolTip.ToolTipText = Program.Lang.ScalingTip;
            Program.ToolTip.ToolTipTitle = Program.Lang.Tip;
            Program.ToolTip.Image = Assets.Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 7000);
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
        private void CursorsSize_Bar_Scroll(object sender)
        {
            if (!_Shown) return;

            foreach (UI.Controllers.CursorControl i in Cursors_Container.Controls)
            {
                i.Prop_Scale = (float)((UI.WP.TrackBar)sender).Value / 100f;
                i.Width = (int)Math.Round(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }

            Label17.Text = $"{Program.Lang.Scaling} ({(float)((UI.WP.TrackBar)sender).Value / 100f}x)";
        }

        private void Search_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                PerformSearch();
        }

        private Point newPoint = new();
        private Point oldPoint = new();

        private void CustomTitlebar_MouseDown(object sender, MouseEventArgs e)
        {
            oldPoint = MousePosition - (Size)Location;
        }

        private void CustomTitlebar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)oldPoint;
                Location = newPoint;
            }
        }

        private void Author_url_button_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.Store_AuthorURLRedirect, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, selectedItem.TM.Info.AuthorSocialMediaLink) == DialogResult.Yes)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(selectedItem.TM.Info.AuthorSocialMediaLink))
                        Process.Start(selectedItem.TM.Info.AuthorSocialMediaLink);
                }
                catch
                {
                }
            }
        }

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
                using (Ookii.Dialogs.WinForms.VistaFolderBrowserDialog FD = new())
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

            if (!string.IsNullOrWhiteSpace(selectedPath) && System.IO.Directory.Exists(selectedPath))
            {
                string filename = $@"{selectedPath}\{new System.IO.FileInfo(selectedItem.FileName).Name}";

                if (!System.IO.Directory.Exists(selectedPath))
                    System.IO.Directory.CreateDirectory(selectedPath);
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);

                System.IO.File.Copy(selectedItem.FileName, filename);

                if (selectedItem.MD5_PackFile != "0")
                {
                    string themepackfilename = $@"{selectedPath}\{new System.IO.FileInfo(selectedItem.FileName).Name}";
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
    }
}