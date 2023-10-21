using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    public struct WindowsVista : ICloneable
    {
        public Color ColorizationColor;
        public byte Alpha;
        public Windows7.Themes Theme;

        public static bool operator ==(WindowsVista First, WindowsVista Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(WindowsVista First, WindowsVista Second)
        {
            return !First.Equals(Second);
        }

        public void Load(WindowsVista _DefWin)
        {
            if (OS.WVista)
            {
                object y;

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));
                Alpha = Color.FromArgb(Convert.ToInt32(y)).A;

                bool Opaque = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false));

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
                    Theme = Windows7.Themes.Classic;
                }
                else if (DWMAPI.IsCompositionEnabled())
                {
                    Theme = !Opaque ? Windows7.Themes.Aero : Windows7.Themes.AeroOpaque;
                }
                else
                {
                    Theme = Windows7.Themes.Basic;
                }
            }


            else
            {
                ColorizationColor = _DefWin.ColorizationColor;
                Alpha = _DefWin.Alpha;
                Theme = _DefWin.Theme;
            }
        }

        public void Apply(TreeView TreeView = null)
        {
            switch (Theme)
            {
                case Windows7.Themes.Aero:
                    {
                        UxTheme.EnableTheming(1);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                        UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                        break;
                    }

                case Windows7.Themes.AeroOpaque:
                    {
                        UxTheme.EnableTheming(1);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                        UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1);
                        break;
                    }

                case Windows7.Themes.Basic:
                    {
                        UxTheme.EnableTheming(1);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                        UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                        break;
                    }

                case Windows7.Themes.Classic:
                    {
                        UxTheme.EnableTheming(0);
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 0), "dll");
                        break;
                    }

            }

            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Color.FromArgb(Alpha, ColorizationColor).ToArgb());

        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
