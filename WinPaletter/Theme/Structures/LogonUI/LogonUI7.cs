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
        public Modes Mode;

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


        public static bool operator ==(LogonUI7 First, LogonUI7 Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(LogonUI7 First, LogonUI7 Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public enum Modes
        {
            Default,
            Wallpaper,
            CustomImage,
            SolidColor
        }

        public void Load(LogonUI7 _DefLogonUI)
        {
            if (OS.W7 | OS.W8 | OS.W81)
            {

                ImagePath = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", "").ToString();
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
                    Mode = (Modes)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", Modes.Default));
                }
            }

            else
            {
                Enabled = _DefLogonUI.Enabled;
                Mode = _DefLogonUI.Mode;
                ImagePath = _DefLogonUI.ImagePath;
                Color = _DefLogonUI.Color;
                Blur = _DefLogonUI.Blur;
                Blur_Intensity = _DefLogonUI.Blur_Intensity;
                Grayscale = _DefLogonUI.Grayscale;
                Noise = _DefLogonUI.Noise;
                Noise_Mode = _DefLogonUI.Noise_Mode;
                Noise_Intensity = _DefLogonUI.Noise_Intensity;
            }
        }
    }
}
