using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    public struct Windows7 : ICloneable
    {
        public Color ColorizationColor;
        public Color ColorizationAfterglow;
        public bool EnableAeroPeek;
        public bool AlwaysHibernateThumbnails;
        public int ColorizationColorBalance;
        public int ColorizationAfterglowBalance;
        public int ColorizationBlurBalance;
        public int ColorizationGlassReflectionIntensity;
        public Themes Theme;

        public static bool operator ==(Windows7 First, Windows7 Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(Windows7 First, Windows7 Second)
        {
            return !First.Equals(Second);
        }

        public enum Themes
        {
            Aero,
            AeroLite,
            AeroOpaque,
            Basic,
            Classic
        }

        public void Load(Windows7 _DefWin)
        {
            if (Program.W7 | Program.W8 | Program.W81)
            {
                object y;

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance);
                ColorizationColorBalance = Convert.ToInt32(y);

                if (Program.W7)
                {
                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", _DefWin.ColorizationAfterglow.ToArgb());
                    ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", _DefWin.ColorizationAfterglowBalance);
                    ColorizationAfterglowBalance = Convert.ToInt32(y);

                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", _DefWin.ColorizationBlurBalance);
                    ColorizationBlurBalance = Convert.ToInt32(y);

                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", _DefWin.ColorizationGlassReflectionIntensity);
                    ColorizationGlassReflectionIntensity = Convert.ToInt32(y);

                    bool Com = default, Opaque;
                    Dwmapi.DwmIsCompositionEnabled(ref Com);

                    Opaque = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false));

                    bool Classic = false;

                    try
                    {
                        var stringThemeName = new System.Text.StringBuilder(260);
                        UxTheme.GetCurrentThemeName(stringThemeName, 260, null, 0, null, 0);
                        Classic = string.IsNullOrWhiteSpace(stringThemeName.ToString()) | !System.IO.File.Exists(stringThemeName.ToString());
                    }
                    catch
                    {
                        Classic = false;
                    }

                    if (Classic)
                    {
                        Theme = Themes.Classic;
                    }
                    else if (Com)
                    {
                        if (!Opaque)
                            Theme = Themes.Aero;
                        else
                            Theme = Themes.AeroOpaque;
                    }
                    else
                    {
                        Theme = Themes.Basic;
                    }

                }

                EnableAeroPeek = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", _DefWin.EnableAeroPeek));

                AlwaysHibernateThumbnails = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", _DefWin.AlwaysHibernateThumbnails));
            }

            else
            {
                ColorizationColor = _DefWin.ColorizationColor;
                ColorizationColorBalance = _DefWin.ColorizationColorBalance;
                ColorizationAfterglow = _DefWin.ColorizationAfterglow;
                ColorizationAfterglowBalance = _DefWin.ColorizationAfterglowBalance;
                ColorizationBlurBalance = _DefWin.ColorizationBlurBalance;
                ColorizationGlassReflectionIntensity = _DefWin.ColorizationGlassReflectionIntensity;
                Theme = _DefWin.Theme;
                EnableAeroPeek = _DefWin.EnableAeroPeek;
                AlwaysHibernateThumbnails = _DefWin.AlwaysHibernateThumbnails;
            }
        }

        public void Apply(TreeView TreeView = null)
        {
            switch (Theme)
            {
                case Themes.Aero:
                    {
                        UxTheme.EnableTheming(1);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                        UxTheme.SetSystemVisualStyle(Program.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", Program.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                        break;
                    }

                case Themes.AeroOpaque:
                    {
                        UxTheme.EnableTheming(1);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                        UxTheme.SetSystemVisualStyle(Program.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", Program.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1);
                        break;
                    }

                case Themes.Basic:
                    {
                        UxTheme.EnableTheming(1);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                        UxTheme.SetSystemVisualStyle(Program.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", Program.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                        break;
                    }

                case Themes.Classic:
                    {
                        UxTheme.EnableTheming(0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 0), "dll");
                        break;
                    }

            }

            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", ColorizationAfterglow.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", ColorizationAfterglowBalance);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", ColorizationBlurBalance);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", ColorizationGlassReflectionIntensity);

            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", EnableAeroPeek.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", AlwaysHibernateThumbnails.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableWindowColorization", 1);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
