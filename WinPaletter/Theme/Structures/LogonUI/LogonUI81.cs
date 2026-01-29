using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Serilog.Events;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// LogonUI structure for Windows 8.1
    /// </summary>
    public class LogonUI81 : ManagerBase<LogonUI81>
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Source of LogonUI background image
        /// <code>
        /// Default
        /// Wallpaper
        /// CustomImage
        /// SolidColor
        /// </code>
        /// </summary>
        public Sources Mode { get; set; } = Sources.Default;

        /// <summary>LogonUI background image path. Used if 'Source' is 'CustomImage'</summary>
        public string ImagePath { get; set; } = @"C:\Windows\Web\Wallpaper\Windows\img0.jpg";

        /// <summary>LogonUI background color. Used if 'Source' is 'SolidColor'</summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>LogonUI background blur enabled or not</summary>
        public bool Blur { get; set; } = false;

        /// <summary>LogonUI background blur intensity</summary>
        public int Blur_Intensity { get; set; } = 0;

        /// <summary>LogonUI background grayscale effect enabled or not</summary>
        public bool Grayscale { get; set; } = false;

        /// <summary>LogonUI background noise effect enabled or not</summary>
        public bool Noise { get; set; } = false;

        /// <summary>LogonUI background noise type. It can be acrylic noise or Aero glass reflection</summary>
        public BitmapExtensions.NoiseMode Noise_Mode { get; set; } = BitmapExtensions.NoiseMode.Acrylic;

        /// <summary>LogonUI background noise intensity</summary>
        public int Noise_Intensity { get; set; } = 0;

        /// <summary>Disable lock screen for Windows 8x</summary>
        public bool NoLockScreen { get; set; } = false;

        /// <summary>Lock screen stock background image ID for Windows 8x</summary>
        public int LockScreenSystemID { get; set; } = 0;

        /// <summary>LogonUI background color ID for Windows 8 only. It can be any number from 0 to 24.</summary>
        public int LogonUI_ID { get; set; } = 0;

        /// <summary>
        /// Enumeration for LogonUI background sources
        /// <code>
        /// Default
        /// Wallpaper
        /// CustomImage
        /// SolidColor
        /// </code>
        /// </summary>
        public enum Sources
        {
            ///
            Default,
            ///
            Wallpaper,
            ///
            CustomImage,
            ///
            SolidColor
        }

        /// <summary>
        /// Creates a new Windows 8.1 LogonUI data structure with default values
        /// </summary>
        public LogonUI81() { }

        /// <summary>
        /// Loads Windows 8.1 LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows 8.1 LogonUI data structure</param>
        public void Load(LogonUI81 @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows 8.1 lock screen preferences from registry.");

            Enabled = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", string.Empty, @default.Enabled);
            ImagePath = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "ImagePath", string.Empty);
            Color = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Color", Color.Black);
            Blur = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur", false);
            Blur_Intensity = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur_Intensity", 0);
            Grayscale = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Grayscale", false);
            Noise = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise", false);
            Noise_Mode = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Mode", BitmapExtensions.NoiseMode.Acrylic);
            Noise_Intensity = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Intensity", 0);
            Mode = ReadReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Mode", Sources.Default);

            LockScreenSystemID = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", 0);
            NoLockScreen = ReadReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", false);
            LogonUI_ID = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0);
        }

        /// <summary>
        /// Apply Windows 8.1 LogonUI screen
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows 8.1 lock screen data into registry.");

            SaveToggleState(treeView);

            WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", Enabled ? 1 : 0);
            WriteReg(treeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", Enabled ? 1 : 0);

            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Mode", (int)Mode);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "ImagePath", ImagePath, RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Color", Color.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur", Blur ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur_Intensity", Blur_Intensity);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Grayscale", Grayscale ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise", Noise ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Mode", (int)Noise_Mode);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Intensity", Noise_Intensity);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Metro_LockScreenSystemID", LockScreenSystemID);

            if (Enabled)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Saving Windows 8.1 lock screen extended data into registry.");

                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "NoChangingLockScreen", 0);

                bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
                bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

                string lockimg = $@"{SysPaths.appData}\LockScreen.png";
                WriteReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen ? 1 : 0);
                WriteReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String);

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI_ID);

                Bitmap bmp;

                if (ReportProgress_Detailed) ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.GetInstanceLogonUIImg, Program.Localization.Strings.Aspects.LockScreen), "info");

                switch (Mode)
                {
                    case Sources.Default:
                        {
                            string syslock = string.Empty;

                            if (File.Exists($@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.png"))
                            {
                                syslock = $@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.png";
                            }

                            else if (File.Exists($@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.jpg"))
                            {
                                syslock = $@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.jpg";
                            }

                            if (File.Exists(syslock))
                            {
                                bmp = BitmapMgr.Load(syslock);
                            }
                            else
                            {
                                bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            }

                            Program.Log?.Write(LogEventLevel.Information, $"Image to be used as lock screen is `{syslock ?? "null"}` with ID `{LockScreenSystemID.ToString() ?? "null"}`.");

                            break;
                        }

                    case Sources.CustomImage:
                        {
                            if (File.Exists(ImagePath))
                            {
                                bmp = BitmapMgr.Load(ImagePath);
                            }
                            else
                            {
                                bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            }

                            Program.Log?.Write(LogEventLevel.Information, $"Custom image to be used as lock screen is `{ImagePath ?? "null"}`.");

                            break;
                        }

                    case Sources.SolidColor:
                        {
                            bmp = Color.ToBitmap(Screen.PrimaryScreen.Bounds.Size);

                            Program.Log?.Write(LogEventLevel.Information, $"Solid color to be used as lock screen background is `{Color}`.");

                            break;
                        }

                    case Sources.Wallpaper:
                        {
                            using (Bitmap b = new(Program.AppliedWallpaper))
                            {
                                bmp = (Bitmap)b.Clone();
                            }

                            Program.Log?.Write(LogEventLevel.Information, $"Using current wallpaper as a lock screen.");

                            break;
                        }

                    default:
                        {
                            bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);

                            Program.Log?.Write(LogEventLevel.Information, $"Black color is used as lock screen background as a default fallback option.");

                            break;
                        }

                }

                if (ReportProgress)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Actions.RenderingImage_MayNotRespond, Program.Localization.Strings.Aspects.LockScreen), "info");

                if (ReportProgress)
                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Actions.RenderingImage, Program.Localization.Strings.Aspects.LogonUI)}", "info");

                if (Grayscale)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.GrayscaleLogonUIImg, Program.Localization.Strings.Aspects.LockScreen), "apply");

                    Program.Log?.Write(LogEventLevel.Information, $"Grayscaling lock screen image.");
                    bmp = bmp.Grayscale();
                }

                if (Blur)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.BlurringLogonUIImg, Program.Localization.Strings.Aspects.LockScreen), "apply");

                    Program.Log?.Write(LogEventLevel.Information, $"Blurring lock screen image with radius `{Blur_Intensity}`.");

                    bmp = bmp.Blur(Blur_Intensity);
                }

                if (Noise)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.NoiseLogonUIImg, Program.Localization.Strings.Aspects.LockScreen), "apply");

                    Program.Log?.Write(LogEventLevel.Information, $"Generating noise effect for lock screen image with intensity `{Noise_Intensity}`, and type `{Noise_Mode}`.");

                    bmp = bmp.Noise(Noise_Mode, Noise_Intensity / 100f);
                }

                if (File.Exists(lockimg)) File.Delete(lockimg);

                if (ReportProgress_Detailed)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.LogonUIImgSaved, Program.Localization.Strings.Aspects.LockScreen, lockimg), "info");

                Program.Log?.Write(LogEventLevel.Information, $"Generating lock screen image is done.");

                bmp.Save(lockimg);
            }
        }

        /// <summary>
        /// Saves Windows8.1 toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", string.Empty, Enabled);
        }
    }
}
