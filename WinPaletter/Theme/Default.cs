using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme
{
    public partial class Default 
    {
        public Default()
        {

        }

        public static Theme.Manager Get(WindowStyle PreviewStyle)
        {
            Theme.Manager _Def;

            if (PreviewStyle == WindowStyle.W11)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }
            }

            else if (PreviewStyle == WindowStyle.W10)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows10();
                }
            }

            else if (PreviewStyle == WindowStyle.W81)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows81();
                }
            }

            else if (PreviewStyle == WindowStyle.W7)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows7();
                }
            }

            else if (PreviewStyle == WindowStyle.WVista)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsVista();
                }
            }

            else if (PreviewStyle == WindowStyle.WXP)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsXP();
                }
            }

            else
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }

            }

            return _Def;
        }

        public static Theme.Manager Get()
        {
            Theme.Manager _Def;

            if (OS.W11 | OS.W12)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }
            }

            else if (OS.W10)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows10();
                }
            }

            else if (OS.W81)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows81();
                }
            }

            else if (OS.W7)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows7();
                }
            }

            else if (OS.WVista)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsVista();
                }
            }

            else if (OS.WXP)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsXP();
                }
            }

            else
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }

            }

            return _Def;
        }
    }
}