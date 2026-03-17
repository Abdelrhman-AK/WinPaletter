using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Class responsible for managing Windows 11 colors and appearance
    /// </summary>
    public class Windows11 : Windows10xBase<Windows11>
    {
        public Windows11() : base("11") { }

        public Windows11(string _signature) : base(_signature) { }

        protected override void SetDefaultColors()
        {
            Color_Index0 = Color.FromArgb(153, 235, 255);
            Color_Index1 = Color.FromArgb(76, 194, 255);
            Color_Index2 = Color.FromArgb(0, 145, 248);
            Color_Index3 = Color.FromArgb(0, 120, 212);
            Color_Index4 = Color.FromArgb(0, 103, 192);
            Color_Index5 = Color.FromArgb(0, 62, 146);
            Color_Index6 = Color.FromArgb(0, 26, 104);
            Color_Index7 = Color.FromArgb(247, 99, 12);

            Titlebar_Active = Color.FromArgb(0, 120, 212);
            Titlebar_Inactive = Color.FromArgb(32, 32, 32);
            StartMenu_Accent = Color.FromArgb(0, 103, 192);
        }

        protected override void ApplySpecific(TreeView treeView = null)
        {

        }

        protected override void LoadSpecific(Windows10xBase<Windows11> @default)
        {

        }

        /// <summary>
        /// Handles version-specific conversions to the target type
        /// </summary>
        protected override void HandleConversionTo<TTarget>(TTarget target)
        {
            if (target is Windows10 targetWin10)
            {
                // When converting to Windows 10, set Windows 10 specific properties to defaults
                targetWin10.IncreaseTBTransparency = false;
                targetWin10.TB_Blur = true;
            }
            // When converting to Windows 12, no special handling needed as they share the same properties
        }

        /// <summary>
        /// Explicit conversion from Windows11 to Windows10
        /// </summary>
        public static explicit operator Windows10(Windows11 win11)
        {
            return win11.ConvertTo<Windows10>();
        }

        /// <summary>
        /// Converts this Windows11 instance to Windows10
        /// </summary>
        public Windows10 AsWindows10() => ConvertTo<Windows10>();
    }
}