using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct AltTab : ICloneable
    {
        public bool Enabled;
        public Styles Style;
        public int Win10Opacity;

        public enum Styles
        {
            Default,
            ClassicNT,
            Placeholder,
            EP_Win10
        }

        public void Load(AltTab _DefAltTab)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", "", _DefAltTab.Enabled));
            Style = (Styles)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", _DefAltTab.Style));
            Win10Opacity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", _DefAltTab.Win10Opacity));
            if (Win10Opacity == default)
                Win10Opacity = _DefAltTab.Win10Opacity;
        }

        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", "", Enabled);

            if (Enabled)
            {
                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", Style);
                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", Win10Opacity);
            }
        }

        public static bool operator ==(AltTab First, AltTab Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(AltTab First, AltTab Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
