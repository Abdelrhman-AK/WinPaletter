using System;
using System.Drawing;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// LogonUI structure for Windows 7/8.1
    /// </summary>
    public struct LogonUI7 : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>
        /// Source of LogonUI background image
        /// <code>
        /// Default
        /// Wallpaper
        /// CustomImage
        /// SolidColor
        /// </code>
        /// </summary>
        public Sources Mode;

        /// <summary>LogonUI background image path. Used if 'Mode' is 'CustomImage'</summary>
        public string ImagePath;

        /// <summary>LogonUI background color. Used if 'Mode' is 'SolidColor'</summary>
        public Color Color;

        /// <summary>LogonUI background blur enabled or not</summary>
        public bool Blur;

        /// <summary>LogonUI background blur intensity</summary>
        public int Blur_Intensity;

        /// <summary>LogonUI background grayscale effect enabled or not</summary>
        public bool Grayscale;

        /// <summary>LogonUI background noise effect enabled or not</summary>
        public bool Noise;

        /// <summary>LogonUI background noise type. It can be acrylic noise or Aero glass reflection</summary>
        public BitmapExtensions.NoiseMode Noise_Mode;

        /// <summary>LogonUI background noise intensity</summary>
        public int Noise_Intensity;

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
        /// Loads Windows 7/8.1 LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows 7/8.1 LogonUI data structure</param>
        public void Load(LogonUI7 @default)
        {
            if (OS.W7 | OS.W8x)
            {

                ImagePath = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", string.Empty).ToString();
                Color = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Color", Color.Black.ToArgb())));
                Blur = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur", false));
                Blur_Intensity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur_Intensity", 0));
                Grayscale = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Grayscale", false));
                Noise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise", false));
                Noise_Mode = (BitmapExtensions.NoiseMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Mode", BitmapExtensions.NoiseMode.Acrylic));
                Noise_Intensity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Intensity", 0));

                if (OS.W7)
                {
                    bool b1 = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", false));
                    bool b2 = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", false));
                    Enabled = b1 | b2;
                    Mode = (Sources)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", Sources.Default));
                }
            }

            else
            {
                Enabled = @default.Enabled;
                Mode = @default.Mode;
                ImagePath = @default.ImagePath;
                Color = @default.Color;
                Blur = @default.Blur;
                Blur_Intensity = @default.Blur_Intensity;
                Grayscale = @default.Grayscale;
                Noise = @default.Noise;
                Noise_Mode = @default.Noise_Mode;
                Noise_Intensity = @default.Noise_Intensity;
            }
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
