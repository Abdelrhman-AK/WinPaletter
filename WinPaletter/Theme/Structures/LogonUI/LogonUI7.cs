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
    /// LogonUI structure for Windows 7
    /// </summary>
    public class LogonUI7 : ICloneable
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
        /// Creates a new Windows 7 LogonUI data structure with default values
        /// </summary>
        public LogonUI7() { }

        /// <summary>
        /// Loads Windows 7 LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows 7 LogonUI data structure</param>
        /// <param name="edition">Windows edition</param>
        public void Load(LogonUI7 @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows 7 LogonUI screen preferences from registry.");

            Enabled = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", string.Empty, @default.Enabled));
            ImagePath = GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "ImagePath", string.Empty).ToString();
            Color = Color.FromArgb(Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Color", Color.Black.ToArgb())));
            Blur = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Blur", false));
            Blur_Intensity = Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Blur_Intensity", 0));
            Grayscale = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Grayscale", false));
            Noise = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Noise", false));
            Noise_Mode = (BitmapExtensions.NoiseMode)Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Noise_Mode", BitmapExtensions.NoiseMode.Acrylic));
            Noise_Intensity = Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Noise_Intensity", 0));
            Mode = (Sources)Convert.ToInt32(GetReg(@$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Mode", Sources.Default));
        }

        /// <summary>
        /// Apply Windows 7 LogonUI screen
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows 7 LogonUI screen data into registry.");

            SaveToggleState(treeView);

            EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", Enabled ? 1 : 0);
            EditReg(treeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", Enabled ? 1 : 0);

            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Mode", (int)Mode);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "ImagePath", ImagePath, RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Color", Color.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Blur", Blur ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Blur_Intensity", Blur_Intensity);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Grayscale", Grayscale ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Noise", Noise ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Noise_Mode", (int)Noise_Mode);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", "Noise_Intensity", Noise_Intensity);

            if (Enabled)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows 7 LogonUI screen extended data into registry.");

                bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
                bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

                IntPtr wow64Value = IntPtr.Zero;

                // Disable WOW64 redirection to access system32 folder correctly
                Kernel32.Wow64DisableWow64FsRedirection(ref wow64Value);

                string DirX = $@"{SysPaths.System32}\oobe\info\backgrounds";

                Directory.CreateDirectory(DirX);

                foreach (string fileX in System.IO.Directory.GetFiles(DirX))
                {
                    try { System.IO.File.Delete(fileX); }
                    catch { } // Couldn't delete a logonUI background File
                }

                List<Bitmap> bmpList = [];
                bmpList.Clear();

                if (ReportProgress_Detailed) ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.GetInstanceLogonUIImg, Program.Lang.Strings.Aspects.LogonUI), "info");

                switch (Mode)
                {
                    case Theme.Structures.LogonUI7.Sources.Default:
                        {
                            // Windows 7 Default LogonUI Backgrounds are stored in imageres.dll from index 5031 to 5043
                            for (int i = 5031; i <= 5043; i += +1)
                            {
                                bmpList.Add(PE.GetPNG(SysPaths.imageres, i, "IMAGE", Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height));
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Extracting default Windows 7 LogonUI background with ID `{i}` from `{SysPaths.imageres}`.");
                            }

                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.CustomImage:
                        {
                            if (System.IO.File.Exists(ImagePath))
                            {
                                bmpList.Add(BitmapMgr.Load(ImagePath).Resize(Screen.PrimaryScreen.Bounds.Size));
                            }
                            else
                            {
                                bmpList.Add(Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size));
                            }

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Custom image `{ImagePath ?? "null"}` will be used as a Windows 7 LogonUI background.");

                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.SolidColor:
                        {
                            bmpList.Add(Color.ToBitmap(Screen.PrimaryScreen.Bounds.Size));
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Solid color to be used as LogonUI screen background is `{Color}`.");
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.Wallpaper:
                        {
                            using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
                            {
                                bmpList.Add((Bitmap)b.Resize(Screen.PrimaryScreen.Bounds.Size).Clone());
                            }

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Using current wallpaper as a LogonUI screen.");

                            break;
                        }

                }

                if (ReportProgress)
                    ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Actions.RenderingImage_MayNotRespond, Program.Lang.Strings.Aspects.LogonUI), "info");

                for (int x = 0, loopTo = bmpList.Count - 1; x <= loopTo; x++)
                {
                    if (ReportProgress)
                        ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Actions.RenderingImages, Program.Lang.Strings.Aspects.LogonUI)} {bmpList[x].Width}x{bmpList[x].Height} ({x + 1}/{bmpList.Count})", "info");

                    if (Grayscale)
                    {
                        if (ReportProgress_Detailed)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.GrayscaleLogonUIImg, Program.Lang.Strings.Aspects.LogonUI), "apply");

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Grayscaling LogonUI screen image number `{x}`.");

                        bmpList[x] = bmpList[x].Grayscale();
                    }

                    if (Blur)
                    {
                        if (ReportProgress_Detailed)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.BlurringLogonUIImg, Program.Lang.Strings.Aspects.LogonUI), "apply");

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Blurring LogonUI screen image number `{x}` with radius `{Blur_Intensity}`.");

                        using (Bitmap b = new(bmpList[x]))
                        {
                            bmpList[x] = b.Blur(Blur_Intensity);
                        }
                    }

                    if (Noise)
                    {
                        if (ReportProgress_Detailed)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.NoiseLogonUIImg, Program.Lang.Strings.Aspects.LogonUI), "apply");

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Generating noise effect for LogonUI screen image number `{x}` with intensity `{Noise_Intensity}`, and type `{Noise_Mode}`.");

                        bmpList[x] = bmpList[x].Noise(Noise_Mode, (float)(Noise_Intensity / 100d));
                    }
                }

                if (bmpList.Count == 1)
                {
                    if (Program.Elevated)
                    {
                        bmpList[0].Save($@"{DirX}\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"LogonUI screen image saved to `{DirX}\\backgroundDefault.jpg`.");
                    }
                    else
                    {
                        bmpList[0].Save($@"{SysPaths.appData}\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Reg_IO.Move_File($@"{SysPaths.appData}\backgroundDefault.jpg", $@"{DirX}\backgroundDefault.jpg");

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"LogonUI screen image saved to `{SysPaths.appData}\\backgroundDefault.jpg` and moved to `{DirX}\\backgroundDefault.jpg`.");
                    }

                    if (ReportProgress_Detailed)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.LogonUIImgSaved, Program.Lang.Strings.Aspects.LogonUI, $@"{DirX}\backgroundDefault.jpg"), "info");
                }
                else
                {
                    for (int x = 0; x <= bmpList.Count - 1; x++)
                    {
                        if (Program.Elevated)
                        {
                            bmpList[x].Save($"{DirX}{$@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg"}", System.Drawing.Imaging.ImageFormat.Jpeg);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"LogonUI screen image number `{x}` saved to `{DirX}\\background{bmpList[x].Width}x{bmpList[x].Height}.jpg`.");
                        }
                        else
                        {
                            bmpList[x].Save($"{SysPaths.appData}{$@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg"}", System.Drawing.Imaging.ImageFormat.Jpeg);
                            Reg_IO.Move_File($"{SysPaths.appData}{$@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg"}", $"{DirX}{$@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg"}");

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"LogonUI screen image number `{x}` saved to `{SysPaths.appData}\\background{bmpList[x].Width}x{bmpList[x].Height}.jpg` and moved to `{DirX}\\background{bmpList[x].Width}x{bmpList[x].Height}.jpg`.");
                        }

                        if (ReportProgress_Detailed)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.LogonUIImgNUMSaved, $"{DirX}{$@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg"}", x + 1), "info");
                    }
                }

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Generating LogonUI screen image\\s is done.");

                // Restore WOW64 redirection
                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        /// <summary>
        /// Saves Windows7 toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @$"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\7", string.Empty, Enabled);
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
        public object Clone()
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
