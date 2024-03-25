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
    /// LogonUI structure for Windows 7/8.1
    /// </summary>
    public struct LogonUI7 : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

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
        /// Creates a new Windows 7/8.1 LogonUI data structure with default values
        /// </summary>
        public LogonUI7() { }

        /// <summary>
        /// Loads Windows 7/8.1 LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows 7/8.1 LogonUI data structure</param>
        /// <param name="edition">Windows edition</param>
        public void Load(string edition, LogonUI7 @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", string.Empty, @default.Enabled));
            ImagePath = GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "ImagePath", string.Empty).ToString();
            Color = Color.FromArgb(Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Color", Color.Black.ToArgb())));
            Blur = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Blur", false));
            Blur_Intensity = Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Blur_Intensity", 0));
            Grayscale = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Grayscale", false));
            Noise = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Noise", false));
            Noise_Mode = (BitmapExtensions.NoiseMode)Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Noise_Mode", BitmapExtensions.NoiseMode.Acrylic));
            Noise_Intensity = Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Noise_Intensity", 0));
            Mode = (Sources)Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Mode", Sources.Default));

            LockScreenSystemID = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", 0));
            NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", false));
            LogonUI_ID = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0));
        }

        /// <summary>
        /// Apply Windows 7/8.1 LogonUI screen
        /// </summary>
        /// <param name="edition">Registry subkey to store data in WinPaletter registry (HKEY_CURRENT_USER\Software\WinPaletter)</param>
        /// <param name="treeView">treeView used to show applying log</param>
        /// <param name="applyAsWin8x">Apply as Windows 8x</param>
        public void Apply(string edition, bool applyAsWin8x = false, TreeView treeView = null)
        {
            SaveToggleState(edition, treeView);

            EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", Enabled ? 1 : 0);
            EditReg(treeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", Enabled ? 1 : 0);

            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Mode", (int)Mode);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "ImagePath", ImagePath, RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Color", Color.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Blur", Blur ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Blur_Intensity", Blur_Intensity);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Grayscale", Grayscale ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Noise", Noise ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Noise_Mode", (int)Noise_Mode);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Noise_Intensity", Noise_Intensity);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", "Metro_LockScreenSystemID", LockScreenSystemID);

            if (Enabled)
            {
                if (applyAsWin8x) SaveAsWin8(treeView); // Save as Windows 8x
                else SaveAsWin7(treeView);              // Save as Windows 7
            }
        }

        /// <summary>
        /// Saves Windows7/8.1 toggle state into registry
        /// </summary>
        public void SaveToggleState(string edition, TreeView treeView = null)
        {
            EditReg(treeView, @$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\{edition}", string.Empty, Enabled);
        }

        private void SaveAsWin7(TreeView treeView = null)
        {
            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

            IntPtr wow64Value = IntPtr.Zero;
            Kernel32.Wow64DisableWow64FsRedirection(ref wow64Value);

            string DirX = $@"{SysPaths.System32}\oobe\info\backgrounds";

            Directory.CreateDirectory(DirX);

            foreach (string fileX in System.IO.Directory.GetFiles(DirX))
            {
                try { System.IO.File.Delete(fileX); }
                catch { } // Couldn't delete a logonUI background file
            }

            List<Bitmap> bmpList = new();
            bmpList.Clear();

            if (ReportProgress_Detailed) ThemeLog.AddNode(treeView, Program.Lang.Verbose_GetInstanceLogonUIImg, "info");

            switch (Mode)
            {
                case Theme.Structures.LogonUI7.Sources.Default:
                    {
                        for (int i = 5031; i <= 5043; i += +1)
                            bmpList.Add(PE.GetPNG(SysPaths.imageres, i, "IMAGE", Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height));
                        break;
                    }

                case Theme.Structures.LogonUI7.Sources.CustomImage:
                    {
                        if (System.IO.File.Exists(ImagePath))
                        {
                            bmpList.Add(Bitmap_Mgr.Load(ImagePath).Resize(Screen.PrimaryScreen.Bounds.Size));
                        }
                        else
                        {
                            bmpList.Add(Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size));
                        }

                        break;
                    }

                case Theme.Structures.LogonUI7.Sources.SolidColor:
                    {
                        bmpList.Add(Color.ToBitmap(Screen.PrimaryScreen.Bounds.Size));
                        break;
                    }

                case Theme.Structures.LogonUI7.Sources.Wallpaper:
                    {
                        using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
                        {
                            bmpList.Add((Bitmap)b.Resize(Screen.PrimaryScreen.Bounds.Size).Clone());
                        }

                        break;
                    }

            }

            if (ReportProgress)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.TM_RenderingCustomLogonUI_MayNotRespond), "info");

            for (int x = 0, loopTo = bmpList.Count - 1; x <= loopTo; x++)
            {
                if (ReportProgress)
                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_RenderingCustomLogonUI_Progress} {bmpList[x].Width}x{bmpList[x].Height} ({x + 1}/{bmpList.Count})", "info");

                if (Grayscale)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, Program.Lang.Verbose_GrayscaleLogonUIImg, "apply");

                    bmpList[x] = bmpList[x].Grayscale();
                }

                if (Blur)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, Program.Lang.Verbose_BlurringLogonUIImg, "apply");

                    ImageProcessor.ImageFactory imgF = new();

                    using (Bitmap b = new(bmpList[x]))
                    {
                        imgF.Load(b);
                        imgF.GaussianBlur(Blur_Intensity);
                        bmpList[x] = (Bitmap)imgF.Image;
                    }
                }

                if (Noise)
                {
                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, Program.Lang.Verbose_NoiseLogonUIImg, "apply");

                    bmpList[x] = bmpList[x].Noise(Noise_Mode, (float)(Noise_Intensity / 100d));
                }
            }

            if (bmpList.Count == 1)
            {
                if (Program.Elevated)
                {
                    bmpList[0].Save($@"{DirX}\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    bmpList[0].Save($@"{SysPaths.appData}\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Reg_IO.Move_File($@"{SysPaths.appData}\backgroundDefault.jpg", $@"{DirX}\backgroundDefault.jpg");
                }

                if (ReportProgress_Detailed)
                    ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_LogonUIImgSaved, $@"{DirX}\backgroundDefault.jpg"), "info");
            }
            else
            {
                for (int x = 0; x <= bmpList.Count - 1; x++)
                {
                    if (Program.Elevated)
                    {
                        bmpList[x].Save($"{DirX}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        bmpList[x].Save($"{SysPaths.appData}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Reg_IO.Move_File($"{SysPaths.appData}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", $"{DirX}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}");
                    }

                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_LogonUIImgNUMSaved, $"{DirX}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", x + 1), "info");

                }
            }

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
        }

        private void SaveAsWin8(TreeView treeView = null)
        {
            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

            string lockimg = $@"{SysPaths.appData}\LockScreen.png";
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen ? 1 : 0);
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String);

            EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI_ID);

            Bitmap bmp;

            if (ReportProgress_Detailed) ThemeLog.AddNode(treeView, Program.Lang.Verbose_GetInstanceLockScreenImg, "info");

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

                        break;
                    }

                case Theme.Structures.LogonUI7.Sources.CustomImage:
                    {
                        if (System.IO.File.Exists(ImagePath))
                        {
                            bmp = Bitmap_Mgr.Load(ImagePath);
                        }
                        else
                        {
                            bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                        }

                        break;
                    }

                case Theme.Structures.LogonUI7.Sources.SolidColor:
                    {
                        bmp = Color.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                        break;
                    }

                case Theme.Structures.LogonUI7.Sources.Wallpaper:
                    {
                        using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
                        {
                            bmp = (Bitmap)b.Clone();
                        }

                        break;
                    }

                default:
                    {
                        bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                        break;
                    }

            }

            if (ReportProgress)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.TM_RenderingCustomLogonUI_MayNotRespond), "info");

            if (ReportProgress)
                ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_RenderingCustomLogonUI}", "info");

            if (Grayscale)
            {
                if (ReportProgress_Detailed)
                    ThemeLog.AddNode(treeView, Program.Lang.Verbose_GrayscaleLockScreenImg, "apply");
                bmp = bmp.Grayscale();
            }

            if (Blur)
            {
                if (ReportProgress_Detailed)
                    ThemeLog.AddNode(treeView, Program.Lang.Verbose_BlurringLockScreenImg, "apply");
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
                    ThemeLog.AddNode(treeView, Program.Lang.Verbose_NoiseLockScreenImg, "apply");
                bmp = bmp.Noise(Noise_Mode, Noise_Intensity / 100f);
            }

            if (System.IO.File.Exists(lockimg))
                System.IO.File.Delete(lockimg);

            if (ReportProgress_Detailed)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_LockScreenImgSaved, lockimg), "info");

            bmp.Save(lockimg);
        }

        /// <summary>Operator to check if two LogonUI7 structures are equal</summary>
        public static bool operator ==(LogonUI7 First, LogonUI7 Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two LogonUI7 structures are not equal</summary>
        public static bool operator !=(LogonUI7 First, LogonUI7 Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones LogonUI7 structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two LogonUI7 structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Get hash code of LogonUI7 structure
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
