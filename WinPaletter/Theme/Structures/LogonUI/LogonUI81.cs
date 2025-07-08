using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// LogonUI structure for Windows 8.1
    /// </summary>
    public class LogonUI81 : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = true;

        /// <summary>
        /// Source of LogonUI background image
        /// <code>
        /// Default
        /// Wallpaper
        /// CustomImage
        /// SolidColor
        /// </code>
        /// </summary>
        public Sources Mode = Sources.Default;

        /// <summary>LogonUI background image path. Used if 'Source' is 'CustomImage'</summary>
        public string ImagePath = @"C:\Windows\Web\Wallpaper\Windows\img0.jpg";

        /// <summary>LogonUI background color. Used if 'Source' is 'SolidColor'</summary>
        public Color Color = Color.Black;

        /// <summary>LogonUI background blur enabled or not</summary>
        public bool Blur = false;

        /// <summary>LogonUI background blur intensity</summary>
        public int Blur_Intensity = 0;

        /// <summary>LogonUI background grayscale effect enabled or not</summary>
        public bool Grayscale = false;

        /// <summary>LogonUI background noise effect enabled or not</summary>
        public bool Noise = false;

        /// <summary>LogonUI background noise type. It can be acrylic noise or Aero glass reflection</summary>
        public BitmapExtensions.NoiseMode Noise_Mode = BitmapExtensions.NoiseMode.Acrylic;

        /// <summary>LogonUI background noise intensity</summary>
        public int Noise_Intensity = 0;

        /// <summary>Disable lock screen for Windows 8x</summary>
        public bool NoLockScreen = false;

        /// <summary>Lock screen stock background image ID for Windows 8x</summary>
        public int LockScreenSystemID = 0;

        /// <summary>LogonUI background color ID for Windows 8 only. It can be any number from 0 to 24.</summary>
        public int LogonUI_ID = 0;

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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows 8.1 lock screen preferences from registry.");

            Enabled = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", string.Empty, @default.Enabled));
            ImagePath = GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "ImagePath", string.Empty).ToString();
            Color = Color.FromArgb(Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Color", Color.Black.ToArgb())));
            Blur = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur", false));
            Blur_Intensity = Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur_Intensity", 0));
            Grayscale = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Grayscale", false));
            Noise = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise", false));
            Noise_Mode = (BitmapExtensions.NoiseMode)Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Mode", BitmapExtensions.NoiseMode.Acrylic));
            Noise_Intensity = Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Intensity", 0));
            Mode = (Sources)Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Mode", Sources.Default));

            LockScreenSystemID = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", 0));
            NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", false));
            LogonUI_ID = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0));
        }

        /// <summary>
        /// Apply Windows 8.1 LogonUI screen
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows 8.1 lock screen data into registry.");

            SaveToggleState(treeView);

            EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", Enabled ? 1 : 0);
            EditReg(treeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", Enabled ? 1 : 0);

            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Mode", (int)Mode);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "ImagePath", ImagePath, RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Color", Color.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur", Blur ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Blur_Intensity", Blur_Intensity);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Grayscale", Grayscale ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise", Noise ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Mode", (int)Noise_Mode);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Noise_Intensity", Noise_Intensity);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", "Metro_LockScreenSystemID", LockScreenSystemID);

            if (Enabled)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows 8.1 lock screen extended data into registry.");

                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "NoChangingLockScreen", 0);

                bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
                bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

                string lockimg = $@"{SysPaths.appData}\LockScreen.png";
                EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen ? 1 : 0);
                EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String);

                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI_ID);

                Bitmap bmp;

                if (ReportProgress_Detailed) ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.GetInstanceLogonUIImg, Program.Lang.Strings.Aspects.LockScreen), "info");

                switch (Mode)
                {
                    case Sources.Default:
                        {
                            string syslock = string.Empty;

                            if (System.IO.File.Exists($@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.png"))
                            {
                                syslock = $@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.png";
                            }

                            else if (System.IO.File.Exists($@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.jpg"))
                            {
                                syslock = $@"{SysPaths.Windows}\Web\Screen\img10{LockScreenSystemID}.jpg";
                            }

                            if (System.IO.File.Exists(syslock))
                            {
                                bmp = Bitmap_Mgr.Load(syslock);
                            }
                            else
                            {
                                bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            }

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Image to be used as lock screen is `{syslock ?? "null"}` with ID `{LockScreenSystemID.ToString() ?? "null"}`.");

                            break;
                        }

                    case Theme.Structures.LogonUI81.Sources.CustomImage:
                        {
                            if (System.IO.File.Exists(ImagePath))
                            {
                                bmp = Bitmap_Mgr.Load(ImagePath);
                            }
                            else
                            {
                                bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            }

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Custom image to be used as lock screen is `{ImagePath ?? "null"}`.");

                            break;
                        }

                    case Theme.Structures.LogonUI81.Sources.SolidColor:
                        {
                            bmp = Color.ToBitmap(Screen.PrimaryScreen.Bounds.Size);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Solid color to be used as lock screen background is `{Color}`.");

                            break;
                        }

                    case Theme.Structures.LogonUI81.Sources.Wallpaper:
                        {
                            using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
                            {
                                bmp = (Bitmap)b.Clone();
                            }

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Using current wallpaper as a lock screen.");

                            break;
                        }

                    default:
                        {
                            bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Black color is used as lock screen background as a default fallback option.");

                            break;
                        }

                }

                if (ReportProgress)
                    ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Actions.RenderingImage_MayNotRespond, Program.Lang.Strings.Aspects.LockScreen), "info");

                if (ReportProgress)
                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Actions.RenderingImage, Program.Lang.Strings.Aspects.LogonUI)}", "info");

                if (Grayscale)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.GrayscaleLogonUIImg, Program.Lang.Strings.Aspects.LockScreen), "apply");

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Grayscaling lock screen image.");
                    bmp = bmp.Grayscale();
                }

                if (Blur)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.BlurringLogonUIImg, Program.Lang.Strings.Aspects.LockScreen), "apply");

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Blurring lock screen image with radius `{Blur_Intensity}`.");

                    ImageProcessor.ImageFactory ImgF = new();
                    using (Bitmap b = new(bmp))
                    {
                        ImgF.Load(b);
                        ImgF.GaussianBlur(Blur_Intensity);
                        bmp = (Bitmap)ImgF.Image;
                    }

                }

                if (Noise)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.NoiseLogonUIImg, Program.Lang.Strings.Aspects.LockScreen), "apply");

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Generating noise effect for lock screen image with intensity `{Noise_Intensity}`, and type `{Noise_Mode}`.");

                    bmp = bmp.Noise(Noise_Mode, Noise_Intensity / 100f);
                }

                if (System.IO.File.Exists(lockimg)) System.IO.File.Delete(lockimg);

                if (ReportProgress_Detailed)
                    ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.LogonUIImgSaved, Program.Lang.Strings.Aspects.LockScreen, lockimg), "info");

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Generating lock screen image is done.");

                bmp.Save(lockimg);
            }
        }

        /// <summary>
        /// Saves Windows8.1 toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\8.1", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two LogonUI81 structures are equal</summary>
        public static bool operator ==(LogonUI81 First, LogonUI81 Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two LogonUI81 structures are not equal</summary>
        public static bool operator !=(LogonUI81 First, LogonUI81 Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones LogonUI81 structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two LogonUI81 structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Get hash code of LogonUI81 structure
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
