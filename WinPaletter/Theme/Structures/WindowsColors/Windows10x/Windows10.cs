using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Class responsible for managing Windows 10 colors and appearance
    /// </summary>
    public class Windows10 : Windows10xBase<Windows10>
    {
        /// <summary>Increase transparency of taskbar (and removes blur)</summary>
        public bool IncreaseTBTransparency { get; set; } = false;

        /// <summary>Make taskbar blur. If false, it will reduce blur power.</summary>
        public bool TB_Blur { get; set; } = true;

        public Windows10() : base("10") { }

        protected override void SetDefaultColors()
        {
            Color_Index0 = Color.FromArgb(166, 216, 255);
            Color_Index1 = Color.FromArgb(118, 185, 237);
            Color_Index2 = Color.FromArgb(66, 156, 227);
            Color_Index3 = Color.FromArgb(0, 120, 215);
            Color_Index4 = Color.FromArgb(0, 90, 158);
            Color_Index5 = Color.FromArgb(0, 66, 117);
            Color_Index6 = Color.FromArgb(0, 38, 66);
            Color_Index7 = Color.FromArgb(247, 99, 12);

            WinMode_Light = false;
            Titlebar_Active = Color.FromArgb(0, 120, 215);
            Titlebar_Inactive = Color.FromArgb(43, 43, 43);
            StartMenu_Accent = Color.FromArgb(0, 90, 158);
        }

        protected override void LoadSpecific(Windows10xBase<Windows10> @default)
        {
            IncreaseTBTransparency = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", (@default as Windows10).IncreaseTBTransparency);
            TB_Blur = !(ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!(@default as Windows10).TB_Blur) ? 1 : 0) == 1);
        }

        protected override void ApplySpecific(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", IncreaseTBTransparency ? 1 : 0);
            WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!TB_Blur) ? 1 : 0);
        }

        /// <summary>
        /// Handles version-specific conversions to the target type
        /// </summary>
        protected override void HandleConversionTo<TTarget>(TTarget target)
        {
            if (target is Windows10 targetWin10)
            {
                // Copy Windows 10 specific properties when converting to Windows 10
                targetWin10.IncreaseTBTransparency = this.IncreaseTBTransparency;
                targetWin10.TB_Blur = this.TB_Blur;
            }
            // When converting to Windows 11 or 12, Windows 10 specific properties are ignored
            // They'll keep their default values in the target
        }

        /// <summary>
        /// Explicit conversion from Windows10 to Windows11
        /// </summary>
        public static explicit operator Windows11(Windows10 win10)
        {
            return win10.ConvertTo<Windows11>();
        }

        /// <summary>
        /// Converts this Windows10 instance to Windows11
        /// </summary>
        public Windows11 AsWindows11() => ConvertTo<Windows11>();
    }
}