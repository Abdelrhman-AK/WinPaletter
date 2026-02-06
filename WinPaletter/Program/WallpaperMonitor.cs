using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.Theme.Structures;

namespace WinPaletter
{
    internal partial class Program
    {
        internal static partial class WallpaperMonitor
        {
            private static FileSystemWatcher wallpaperWatcher;
            private static DateTime lastFireTime = DateTime.MinValue;
            private static readonly TimeSpan debounceInterval = TimeSpan.FromMilliseconds(120);
            private static readonly object sync = new();

            private static ManagementEventWatcher wmiDesktopWatcher;
            private static ManagementEventWatcher wmiColorWatcher;
            private static ManagementEventWatcher wmiWallpaperXPVista;
            private static ManagementEventWatcher wmiBackgroundTypeWatcher;

            private static readonly string ThemesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Themes");

            public class WallpaperSnapshot
            {
                // System / registry state
                public string Path;
                public Color BackgroundColor;
                public Theme.Structures.Wallpaper.WallpaperStyles WallpaperStyle;

                // Cached preview info
                public Bitmap Thumbnail;
                public PreviewHelpers.WindowStyle WindowStyle;

                public override bool Equals(object obj)
                {
                    return obj is WallpaperSnapshot other &&
                           Path == other.Path &&
                           BackgroundColor.ToArgb() == other.BackgroundColor.ToArgb() &&
                           WallpaperStyle == other.WallpaperStyle;
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(Path, BackgroundColor.ToArgb(), WallpaperStyle, WindowStyle, Thumbnail);
                }
            }

            private static WallpaperSnapshot lastState;

            /// <summary>
            /// Gets or sets the cached wallpaper data for the current theme manager wallpaper session.
            /// </summary>
            private static WallpaperSnapshot CachedWallpaper = new();

            public static void Start()
            {
                if (!OS.WXP && !OS.WVista) StartWallpaperFileWatcher();
                else StartWmiWallpaperWatcher_XP_Vista();

                StartWmiWatchers();

                if (OS.WXP || OS.WVista) SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;

                // Then setup debounce for subsequent changes
                DebounceFire();
            }

            public static void Stop()
            {
                wallpaperWatcher?.Dispose();
                wallpaperWatcher = null;

                wmiDesktopWatcher?.Stop();
                wmiDesktopWatcher?.Dispose();
                wmiDesktopWatcher = null;

                wmiColorWatcher?.Stop();
                wmiColorWatcher?.Dispose();
                wmiColorWatcher = null;

                wmiWallpaperXPVista?.Stop();
                wmiWallpaperXPVista?.Dispose();
                wmiWallpaperXPVista = null;

                SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
            }

            private static void StartWallpaperFileWatcher()
            {
                if (!Directory.Exists(ThemesPath)) return;

                wallpaperWatcher = new FileSystemWatcher
                {
                    Path = ThemesPath,
                    Filter = "TranscodedWallpaper",
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
                };

                wallpaperWatcher.Changed += (_, _) => DebounceFire();
                wallpaperWatcher.Created += (_, _) => DebounceFire();
                wallpaperWatcher.Renamed += (_, _) => DebounceFire();
                wallpaperWatcher.EnableRaisingEvents = true;
            }

            private static void StartWmiWatchers()
            {
                string sid = User.Identity.User.Value;
                string desktopKey = $@"{sid}\Control Panel\Desktop";
                string colorsKey = $@"{sid}\Control Panel\Colors";
                string explorerWallpapersKey = $@"{sid}\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers";

                // Watch WallpaperStyle and TileWallpaper
                wmiDesktopWatcher = CreateRegistryWatcher([(desktopKey, "WallpaperStyle"), (desktopKey, "TileWallpaper")]);

                // Watch Background color
                wmiColorWatcher = CreateRegistryWatcher([(colorsKey, "Background")]);

                // Watch system wallpaper type changes
                wmiBackgroundTypeWatcher = CreateRegistryWatcher([(explorerWallpapersKey, "BackgroundType")]);

                // Start existing watchers
                wmiDesktopWatcher?.Start();
                wmiColorWatcher?.Start();
                wmiBackgroundTypeWatcher?.Start();
            }

            private static void StartWmiWallpaperWatcher_XP_Vista()
            {
                string sid = User.Identity.User.Value;
                string desktopKey = $@"{sid}\Control Panel\Desktop";
                wmiWallpaperXPVista = CreateRegistryWatcher([(desktopKey, "Wallpaper")]);
                wmiWallpaperXPVista.Start();
            }

            private static ManagementEventWatcher CreateRegistryWatcher((string keyPath, string valueName)[] values)
            {
                string[] conditions = [.. values.Select(v => $"(KeyPath='{v.keyPath.Replace(@"\", @"\\")}' AND ValueName='{v.valueName}')")];

                string query = "SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND (" + string.Join(" OR ", conditions) + ")";
                ManagementEventWatcher watcher = new(new WqlEventQuery(query));
                watcher.EventArrived += (_, _) => DebounceFire();
                return watcher;
            }

            [STAThread]
            public static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
            {
                if (e.Category == UserPreferenceCategory.General || e.Category == UserPreferenceCategory.Desktop || e.Category == UserPreferenceCategory.Color) DebounceFire();
            }

            [STAThread]
            private static void DebounceFire()
            {
                var now = DateTime.Now;
                if ((now - lastFireTime) > debounceInterval)
                {
                    lastFireTime = now;
                    FireWallpaperChanged();
                }
            }

            [STAThread]
            private static void FireWallpaperChanged()
            {
                WallpaperSnapshot current = ReadState();

                // Only fire if the state actually changed
                if (lastState != null && lastState.Path == current.Path && lastState.BackgroundColor.ToArgb() == current.BackgroundColor.ToArgb() && lastState.WallpaperStyle == current.WallpaperStyle)
                    return;

                lastState = current;

                AppliedWallpaper = BitmapMgr.Load(current.Path);

                Program.SystemWallpaperChanged?.Invoke(null, current);
            }

            private static WallpaperSnapshot ReadState()
            {
                string path = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty) ?? string.Empty;
                string bgStr = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Background", "255 255 255") ?? "255 255 255";

                int.TryParse(ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", "0"), out int wallpaperStyleInt);
                int.TryParse(ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0"), out int tileWallpaper);

                Theme.Structures.Wallpaper.WallpaperStyles wallpaperStyle;
                if (tileWallpaper == 1)
                    wallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Tile;
                else
                {
                    if (wallpaperStyleInt == 0) wallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Centered;
                    else if (wallpaperStyleInt == 2) wallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Stretched;
                    else if (wallpaperStyleInt == 6) wallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fit;
                    else if (wallpaperStyleInt == 10) wallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fill;
                    else wallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fill;
                }

                WallpaperSnapshot snapshot = new()
                {
                    Path = path,
                    BackgroundColor = bgStr.ToColorFromWin32(),
                    WallpaperStyle = wallpaperStyle,
                    Thumbnail = null,
                    WindowStyle = Program.WindowStyle
                };

                // Load thumbnail if requested
                if (File.Exists(path))
                {
                    snapshot.Thumbnail = BitmapMgr.Thumbnail(path, PreviewSize.Width * 2, PreviewSize.Height * 2);
                }
                else
                {
                    snapshot.Thumbnail = null;
                }

                return snapshot;
            }

            /// <summary>
            /// Get the suitable wallpaper according to the theme manager preferences and preview configuration
            /// </summary>
            /// <param name="TM"></param>
            /// <param name="previewConfig"></param>
            /// <returns></returns>
            public static Bitmap FetchSuitableWallpaper(Manager themeMgr = null, PreviewHelpers.WindowStyle previewConfig = (PreviewHelpers.WindowStyle)(-1), Wallpaper.WallpaperStyles wallpaperStyle = (Wallpaper.WallpaperStyles)(-1))
            {
                themeMgr ??= TM;
                bool useThemeMgr = themeMgr.Wallpaper.Enabled;
                if (previewConfig == (PreviewHelpers.WindowStyle)(-1)) previewConfig = WindowStyle;

                if (wallpaperStyle == (Wallpaper.WallpaperStyles)(-1)) wallpaperStyle = useThemeMgr ? themeMgr.Wallpaper.WallpaperStyle : lastState.WallpaperStyle;

                // Return cache if it matches
                if (CachedWallpaper.Thumbnail != null)
                {
                    bool matches = useThemeMgr
                        ? CachedWallpaper.Path == themeMgr.Wallpaper.ImageFile && CachedWallpaper.WindowStyle == previewConfig && CachedWallpaper.WallpaperStyle == wallpaperStyle
                        : CachedWallpaper.Path == lastState.Path && CachedWallpaper.WindowStyle == previewConfig && CachedWallpaper.WallpaperStyle == wallpaperStyle;

                    if (matches)
                    {
                        Log?.Write(LogEventLevel.Information, $"Fetched cached wallpaper for {previewConfig} (useThemeMgr={useThemeMgr})");
                        return CachedWallpaper.Thumbnail;
                    }
                }

                Log?.Write(LogEventLevel.Information, $"Fetching suitable wallpaper for {previewConfig} (useThemeMgr={useThemeMgr})");

                Bitmap wallpaper;

                if (useThemeMgr)
                {
                    // Try tinted wallpaper first
                    wallpaper = GetTintedWallpaper(themeMgr, previewConfig);
                    if (wallpaper is null)
                    {
                        switch (themeMgr.Wallpaper.WallpaperType)
                        {
                            case Theme.Structures.Wallpaper.WallpaperTypes.SlideShow:
                                wallpaper = FetchSlideShowWallpaper(themeMgr);
                                break;

                            case Theme.Structures.Wallpaper.WallpaperTypes.SolidColor:
                                wallpaper = null;
                                break;

                            default:
                                wallpaper = BitmapMgr.Load(themeMgr.Wallpaper.ImageFile);
                                break;
                        }
                    }

                    // Cache source path from theme manager
                    CachedWallpaper.Path = themeMgr.Wallpaper.ImageFile;
                    CachedWallpaper.BackgroundColor = themeMgr.Win32.Background;
                }
                else
                {
                    // Fallback to system-applied wallpaper
                    if (wallpaperStyle != Wallpaper.WallpaperStyles.Tile)
                    {
                        wallpaper = BitmapMgr.Thumbnail(lastState.Path, PreviewSize.Width * 2, PreviewSize.Height * 2);
                    }
                    else
                    {
                        using (Bitmap bmp = BitmapMgr.Load(lastState.Path))
                        {
                            if (bmp is not null)
                            {
                                float screenWidth = Screen.PrimaryScreen.Bounds.Width;
                                float screenHeight = Screen.PrimaryScreen.Bounds.Height;

                                float targetWidth = PreviewSize.Width * 2;
                                float targetHeight = PreviewSize.Height * 2;

                                float resizedWidth = (targetWidth / screenWidth) * bmp.Width;
                                float resizedHeight = (targetHeight / screenHeight) * bmp.Height;

                                wallpaper = bmp.GetThumbnailImage((int)resizedWidth, (int)resizedHeight, () => false, IntPtr.Zero) as Bitmap;
                            }
                            else wallpaper = null;
                        }
                    }

                    // Cache source path from system
                    CachedWallpaper.Path = lastState.Path;
                    CachedWallpaper.BackgroundColor = lastState.BackgroundColor;
                }

                // Apply style if wallpaper is available
                if (wallpaper != null)
                {
                    using (PictureBox picbox = new() { Size = PreviewSize, BackColor = CachedWallpaper.BackgroundColor })
                    {
                        wallpaper = ApplyStyleToWallpaper(wallpaper, picbox.Size, wallpaperStyle);
                    }
                }

                // Finalize cache
                CachedWallpaper.Thumbnail = wallpaper;
                CachedWallpaper.WallpaperStyle = wallpaperStyle;
                CachedWallpaper.WindowStyle = previewConfig;

                return CachedWallpaper.Thumbnail;
            }

            private static Bitmap GetTintedWallpaper(Manager TM, PreviewHelpers.WindowStyle previewConfig)
            {
                var mapping = new System.Collections.Generic.Dictionary<PreviewHelpers.WindowStyle, WallpaperTone>
                {
                    [PreviewHelpers.WindowStyle.WXP] = TM.WallpaperTone_WXP,
                    [PreviewHelpers.WindowStyle.WVista] = TM.WallpaperTone_WVista,
                    [PreviewHelpers.WindowStyle.W7] = TM.WallpaperTone_W7,
                    [PreviewHelpers.WindowStyle.W8] = TM.WallpaperTone_W8,
                    [PreviewHelpers.WindowStyle.W81] = TM.WallpaperTone_W81,
                    [PreviewHelpers.WindowStyle.W10] = TM.WallpaperTone_W10,
                    [PreviewHelpers.WindowStyle.W11] = TM.WallpaperTone_W11,
                    [PreviewHelpers.WindowStyle.W12] = TM.WallpaperTone_W12
                };

                if (mapping.TryGetValue(previewConfig, out var tone) && tone.Enabled)
                {
                    Log?.Write(LogEventLevel.Information, $"Fetching tinted wallpaper for {previewConfig}");
                    return PreviewHelpers.GetTintedWallpaper(tone);
                }

                return null;
            }

            private static Bitmap FetchSlideShowWallpaper(Manager TM)
            {
                string[] imageFiles;

                if (TM.Wallpaper.SlideShow_Folder_or_ImagesList && Directory.Exists(TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath))
                {
                    imageFiles = Directory.EnumerateFiles(TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath, "*.*", SearchOption.TopDirectoryOnly)
                        .Where(f => f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                                    f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                    f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                    f.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                }
                else
                {
                    imageFiles = TM.Wallpaper.Wallpaper_Slideshow_Images ?? Array.Empty<string>();
                }

                if (imageFiles.Length > 0 && File.Exists(imageFiles[0]))
                {
                    Log?.Write(LogEventLevel.Information, $"Fetching first image from slideshow: {imageFiles[0]}");
                    return BitmapMgr.Load(imageFiles[0]);
                }

                return AppliedWallpaper;
            }

            /// <summary>
            /// Get the wallpaper with the specified style
            /// </summary>
            /// <param name="wallpaper"></param>
            /// <param name="targetSize"></param>
            /// <param name="wallpaperStyle"></param>
            /// <returns></returns>
            public static Bitmap ApplyStyleToWallpaper(Bitmap wallpaper, Size targetSize, Wallpaper.WallpaperStyles wallpaperStyle)
            {
                float scaleW = 1;
                float scaleH = 1;

                // Rescale the wallpaper according to the screen size
                if (wallpaper.Width > Screen.PrimaryScreen.Bounds.Size.Width || wallpaper.Height > Screen.PrimaryScreen.Bounds.Size.Height)
                {
                    scaleW = Screen.PrimaryScreen.Bounds.Width / targetSize.Width;
                    scaleH = Screen.PrimaryScreen.Bounds.Height / targetSize.Height;
                }

                // Resize the wallpaper according to the scale and preview area siz
                wallpaper = wallpaper.Thumbnail((int)(wallpaper.Width / scaleW), (int)(wallpaper.Height / scaleH));

                Program.Log?.Write(LogEventLevel.Information, $"Rescaling wallpaper preview to {wallpaper.Width}x{wallpaper.Height} and adjusting its style");

                // Apply the wallpaper style
                Bitmap result = wallpaperStyle switch
                {
                    Theme.Structures.Wallpaper.WallpaperStyles.Fill => wallpaper.FillInSize(targetSize),
                    Theme.Structures.Wallpaper.WallpaperStyles.Fit => wallpaper,
                    Theme.Structures.Wallpaper.WallpaperStyles.Stretched => wallpaper,
                    Theme.Structures.Wallpaper.WallpaperStyles.Centered => wallpaper,
                    Theme.Structures.Wallpaper.WallpaperStyles.Tile => wallpaper.Tile(targetSize),
                    _ => wallpaper
                };

                return result;
            }
        }
    }
}
