using System.Drawing;
using System.Linq;

namespace WinPaletter.Theme
{
    public static partial class Default
    {
        public static Manager WindowsXP()
        {
            Manager TM = new(Manager.Source.Empty);

            {
                ref Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows XP (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.Version;
            }

            {
                ref Structures.Win32UI Win32 = ref TM.Win32;
                Win32.ActiveBorder = Color.FromArgb(212, 208, 200);
                Win32.ActiveTitle = Color.FromArgb(0, 84, 227);
                Win32.AppWorkspace = Color.FromArgb(128, 128, 128);
                Win32.Background = Color.FromArgb(0, 78, 152);
                Win32.ButtonAlternateFace = Color.FromArgb(181, 181, 181);
                Win32.ButtonDkShadow = Color.FromArgb(113, 111, 100);
                Win32.ButtonFace = Color.FromArgb(236, 233, 216);
                Win32.ButtonHilight = Color.FromArgb(255, 255, 255);
                Win32.ButtonLight = Color.FromArgb(241, 239, 226);
                Win32.ButtonShadow = Color.FromArgb(172, 168, 153);
                Win32.ButtonText = Color.FromArgb(0, 0, 0);
                Win32.GradientActiveTitle = Color.FromArgb(61, 149, 255);
                Win32.GradientInactiveTitle = Color.FromArgb(157, 185, 235);
                Win32.GrayText = Color.FromArgb(172, 168, 153);
                Win32.HilightText = Color.FromArgb(255, 255, 255);
                Win32.HotTrackingColor = Color.FromArgb(0, 0, 128);
                Win32.InactiveBorder = Color.FromArgb(212, 208, 200);
                Win32.InactiveTitle = Color.FromArgb(212, 208, 200);
                Win32.InactiveTitleText = Color.FromArgb(216, 228, 248);
                Win32.InfoText = Color.FromArgb(0, 0, 0);
                Win32.InfoWindow = Color.FromArgb(255, 255, 225);
                Win32.Menu = Color.FromArgb(255, 255, 255);
                Win32.MenuBar = Color.FromArgb(236, 233, 216);
                Win32.MenuText = Color.FromArgb(0, 0, 0);
                Win32.Scrollbar = Color.FromArgb(212, 208, 200);
                Win32.TitleText = Color.FromArgb(255, 255, 255);
                Win32.Window = Color.FromArgb(255, 255, 255);
                Win32.WindowFrame = Color.FromArgb(0, 0, 0);
                Win32.WindowText = Color.FromArgb(0, 0, 0);
                Win32.Hilight = Color.FromArgb(49, 106, 197);
                Win32.MenuHilight = Color.FromArgb(49, 106, 197);
                Win32.Desktop = Color.FromArgb(0, 0, 0);
            }

            {
                ref Structures.Console CMD = ref TM.CommandPrompt;
                CMD.ColorTable00 = Color.FromArgb(12, 12, 12);
                CMD.ColorTable01 = Color.FromArgb(0, 55, 218);
                CMD.ColorTable02 = Color.FromArgb(19, 161, 14);
                CMD.ColorTable03 = Color.FromArgb(58, 150, 221);
                CMD.ColorTable04 = Color.FromArgb(197, 15, 31);
                CMD.ColorTable05 = Color.FromArgb(136, 23, 152);
                CMD.ColorTable06 = Color.FromArgb(193, 156, 0);
                CMD.ColorTable07 = Color.FromArgb(204, 204, 204);
                CMD.ColorTable08 = Color.FromArgb(118, 118, 118);
                CMD.ColorTable09 = Color.FromArgb(59, 120, 255);
                CMD.ColorTable10 = Color.FromArgb(22, 198, 12);
                CMD.ColorTable11 = Color.FromArgb(97, 214, 214);
                CMD.ColorTable12 = Color.FromArgb(231, 72, 86);
                CMD.ColorTable13 = Color.FromArgb(180, 0, 158);
                CMD.ColorTable14 = Color.FromArgb(249, 241, 165);
                CMD.ColorTable15 = Color.FromArgb(242, 242, 242);
                CMD.PopupBackground = 15;
                CMD.PopupForeground = 5;
                CMD.ScreenColorsForeground = 7;
                CMD.ScreenColorsBackground = 0;
                CMD.FaceName = "Consolas";
                CMD.FontSize = 18 * 65536;
                CMD.FontRaster = true;
                CMD.W10_1909_ForceV2 = false;
            }

            {
                ref Structures.Console PS86 = ref TM.PowerShellx86;
                PS86.ColorTable00 = Color.FromArgb(12, 12, 12);
                PS86.ColorTable01 = Color.FromArgb(0, 55, 218);
                PS86.ColorTable02 = Color.FromArgb(19, 161, 14);
                PS86.ColorTable03 = Color.FromArgb(58, 150, 221);
                PS86.ColorTable04 = Color.FromArgb(197, 15, 31);
                PS86.ColorTable05 = Color.FromArgb(1, 36, 86);
                PS86.ColorTable06 = Color.FromArgb(238, 237, 240);
                PS86.ColorTable07 = Color.FromArgb(204, 204, 204);
                PS86.ColorTable08 = Color.FromArgb(118, 118, 118);
                PS86.ColorTable09 = Color.FromArgb(59, 120, 255);
                PS86.ColorTable10 = Color.FromArgb(22, 198, 12);
                PS86.ColorTable11 = Color.FromArgb(97, 214, 214);
                PS86.ColorTable12 = Color.FromArgb(231, 72, 86);
                PS86.ColorTable13 = Color.FromArgb(180, 0, 158);
                PS86.ColorTable14 = Color.FromArgb(249, 241, 165);
                PS86.ColorTable15 = Color.FromArgb(242, 242, 242);
                PS86.PopupBackground = 15;
                PS86.PopupForeground = 3;
                PS86.ScreenColorsForeground = 6;
                PS86.ScreenColorsBackground = 5;
                PS86.FaceName = "Consolas";
                PS86.FontSize = 14 * 65536;
                PS86.FontRaster = true;
                PS86.W10_1909_ForceV2 = false;
            }

            {
                ref Structures.Console PS64 = ref TM.PowerShellx64;
                PS64.ColorTable00 = Color.FromArgb(12, 12, 12);
                PS64.ColorTable01 = Color.FromArgb(0, 55, 218);
                PS64.ColorTable02 = Color.FromArgb(19, 161, 14);
                PS64.ColorTable03 = Color.FromArgb(58, 150, 221);
                PS64.ColorTable04 = Color.FromArgb(197, 15, 31);
                PS64.ColorTable05 = Color.FromArgb(1, 36, 86);
                PS64.ColorTable06 = Color.FromArgb(238, 237, 240);
                PS64.ColorTable07 = Color.FromArgb(204, 204, 204);
                PS64.ColorTable08 = Color.FromArgb(118, 118, 118);
                PS64.ColorTable09 = Color.FromArgb(59, 120, 255);
                PS64.ColorTable10 = Color.FromArgb(22, 198, 12);
                PS64.ColorTable11 = Color.FromArgb(97, 214, 214);
                PS64.ColorTable12 = Color.FromArgb(231, 72, 86);
                PS64.ColorTable13 = Color.FromArgb(180, 0, 158);
                PS64.ColorTable14 = Color.FromArgb(249, 241, 165);
                PS64.ColorTable15 = Color.FromArgb(242, 242, 242);
                PS64.PopupBackground = 15;
                PS64.PopupForeground = 3;
                PS64.ScreenColorsForeground = 6;
                PS64.ScreenColorsBackground = 5;
                PS64.FaceName = "Consolas";
                PS64.FontSize = 14 * 65536;
                PS64.FontRaster = true;
                PS64.W10_1909_ForceV2 = false;
            }

            {
                ref Structures.MetricsFonts MetricsFonts = ref TM.MetricsFonts;
                MetricsFonts.BorderWidth = 0;
                MetricsFonts.CaptionHeight = 25;
                MetricsFonts.CaptionWidth = 18;
                MetricsFonts.IconSpacing = 75;
                MetricsFonts.IconVerticalSpacing = 75;
                MetricsFonts.MenuHeight = 19;
                MetricsFonts.MenuWidth = 18;
                MetricsFonts.PaddedBorderWidth = 0;
                MetricsFonts.ScrollHeight = 17;
                MetricsFonts.ScrollWidth = 17;
                MetricsFonts.SmCaptionHeight = 17;
                MetricsFonts.SmCaptionWidth = 17;
                MetricsFonts.DesktopIconSize = 48;
                MetricsFonts.ShellIconSize = 32;
                MetricsFonts.Fonts_SingleBitPP = true;
                MetricsFonts.CaptionFont = new("Trebuchet MS", 9.75f, FontStyle.Bold);
                MetricsFonts.SmCaptionFont = new("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.IconFont = new("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.MenuFont = new("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.MessageFont = new("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.StatusFont = new("Tahoma", 8.25f, FontStyle.Regular);
            }

            TM.Cursors.Shadow = true;
            {
                ref Structures.Cursor Arrow = ref TM.Cursors.Cursor_Arrow;
                Arrow.ArrowStyle = Paths.ArrowStyle.Classic;
                Arrow.CircleStyle = Paths.CircleStyle.Classic;
                Arrow.PrimaryColor1 = Color.White;
                Arrow.PrimaryColor2 = Color.White;
                Arrow.SecondaryColor1 = Color.Black;
                Arrow.SecondaryColor2 = Color.Black;
                Arrow.PrimaryColorGradient = false;
                Arrow.SecondaryColorGradient = false;
                Arrow.LoadingCircleBack1 = Color.White;
                Arrow.LoadingCircleBack2 = Color.White;
                Arrow.LoadingCircleHot1 = Color.Black;
                Arrow.LoadingCircleHot2 = Color.Black;
                Arrow.LoadingCircleBackGradient = false;
                Arrow.LoadingCircleHotGradient = false;
                Arrow.Shadow_Enabled = false;
                Arrow.Shadow_Color = Color.Black;
                Arrow.Shadow_Blur = 5;
                Arrow.Shadow_Opacity = 0.3f;
                Arrow.Shadow_OffsetX = 2;
                Arrow.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Help = ref TM.Cursors.Cursor_Help;
                Help.ArrowStyle = Paths.ArrowStyle.Classic;
                Help.CircleStyle = Paths.CircleStyle.Classic;
                Help.PrimaryColor1 = Color.White;
                Help.PrimaryColor2 = Color.White;
                Help.SecondaryColor1 = Color.Black;
                Help.SecondaryColor2 = Color.Black;
                Help.PrimaryColorGradient = false;
                Help.SecondaryColorGradient = false;
                Help.LoadingCircleBack1 = Color.White;
                Help.LoadingCircleBack2 = Color.White;
                Help.LoadingCircleHot1 = Color.Black;
                Help.LoadingCircleHot2 = Color.Black;
                Help.LoadingCircleBackGradient = false;
                Help.LoadingCircleHotGradient = false;
                Help.Shadow_Enabled = false;
                Help.Shadow_Color = Color.Black;
                Help.Shadow_Blur = 5;
                Help.Shadow_Opacity = 0.3f;
                Help.Shadow_OffsetX = 2;
                Help.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor AppLoading = ref TM.Cursors.Cursor_AppLoading;
                AppLoading.ArrowStyle = Paths.ArrowStyle.Classic;
                AppLoading.CircleStyle = Paths.CircleStyle.Classic;
                AppLoading.PrimaryColor1 = Color.White;
                AppLoading.PrimaryColor2 = Color.White;
                AppLoading.SecondaryColor1 = Color.Black;
                AppLoading.SecondaryColor2 = Color.Black;
                AppLoading.LoadingCircleBack1 = Color.White;
                AppLoading.LoadingCircleBack2 = Color.White;
                AppLoading.LoadingCircleHot1 = Color.Black;
                AppLoading.LoadingCircleHot2 = Color.Black;
                AppLoading.LoadingCircleBackGradient = false;
                AppLoading.LoadingCircleHotGradient = false;
                AppLoading.PrimaryColorGradient = false;
                AppLoading.SecondaryColorGradient = false;
                AppLoading.Shadow_Enabled = false;
                AppLoading.Shadow_Color = Color.Black;
                AppLoading.Shadow_Blur = 5;
                AppLoading.Shadow_Opacity = 0.3f;
                AppLoading.Shadow_OffsetX = 2;
                AppLoading.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Busy = ref TM.Cursors.Cursor_Busy;
                Busy.ArrowStyle = Paths.ArrowStyle.Classic;
                Busy.CircleStyle = Paths.CircleStyle.Classic;
                Busy.PrimaryColor1 = Color.White;
                Busy.PrimaryColor2 = Color.White;
                Busy.SecondaryColor1 = Color.Black;
                Busy.SecondaryColor2 = Color.Black;
                Busy.LoadingCircleBack1 = Color.White;
                Busy.LoadingCircleBack2 = Color.White;
                Busy.LoadingCircleHot1 = Color.Black;
                Busy.LoadingCircleHot2 = Color.Black;
                Busy.LoadingCircleBackGradient = false;
                Busy.LoadingCircleHotGradient = false;
                Busy.PrimaryColorGradient = false;
                Busy.SecondaryColorGradient = false;
                Busy.Shadow_Enabled = false;
                Busy.Shadow_Color = Color.Black;
                Busy.Shadow_Blur = 5;
                Busy.Shadow_Opacity = 0.3f;
                Busy.Shadow_OffsetX = 2;
                Busy.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Up = ref TM.Cursors.Cursor_Up;
                Up.ArrowStyle = Paths.ArrowStyle.Classic;
                Up.CircleStyle = Paths.CircleStyle.Classic;
                Up.PrimaryColor1 = Color.White;
                Up.PrimaryColor2 = Color.White;
                Up.SecondaryColor1 = Color.Black;
                Up.SecondaryColor2 = Color.Black;
                Up.PrimaryColorGradient = false;
                Up.SecondaryColorGradient = false;
                Up.LoadingCircleBack1 = Color.White;
                Up.LoadingCircleBack2 = Color.White;
                Up.LoadingCircleHot1 = Color.Black;
                Up.LoadingCircleHot2 = Color.Black;
                Up.LoadingCircleBackGradient = false;
                Up.LoadingCircleHotGradient = false;
                Up.Shadow_Enabled = false;
                Up.Shadow_Color = Color.Black;
                Up.Shadow_Blur = 5;
                Up.Shadow_Opacity = 0.3f;
                Up.Shadow_OffsetX = 2;
                Up.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor NS = ref TM.Cursors.Cursor_NS;
                NS.ArrowStyle = Paths.ArrowStyle.Classic;
                NS.CircleStyle = Paths.CircleStyle.Classic;
                NS.PrimaryColor1 = Color.White;
                NS.PrimaryColor2 = Color.White;
                NS.SecondaryColor1 = Color.Black;
                NS.SecondaryColor2 = Color.Black;
                NS.PrimaryColorGradient = false;
                NS.SecondaryColorGradient = false;
                NS.LoadingCircleBack1 = Color.White;
                NS.LoadingCircleBack2 = Color.White;
                NS.LoadingCircleHot1 = Color.Black;
                NS.LoadingCircleHot2 = Color.Black;
                NS.LoadingCircleBackGradient = false;
                NS.LoadingCircleHotGradient = false;
                NS.Shadow_Enabled = false;
                NS.Shadow_Color = Color.Black;
                NS.Shadow_Blur = 5;
                NS.Shadow_Opacity = 0.3f;
                NS.Shadow_OffsetX = 2;
                NS.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor EW = ref TM.Cursors.Cursor_EW;
                EW.ArrowStyle = Paths.ArrowStyle.Classic;
                EW.CircleStyle = Paths.CircleStyle.Classic;
                EW.PrimaryColor1 = Color.White;
                EW.PrimaryColor2 = Color.White;
                EW.SecondaryColor1 = Color.Black;
                EW.SecondaryColor2 = Color.Black;
                EW.PrimaryColorGradient = false;
                EW.SecondaryColorGradient = false;
                EW.LoadingCircleBack1 = Color.White;
                EW.LoadingCircleBack2 = Color.White;
                EW.LoadingCircleHot1 = Color.Black;
                EW.LoadingCircleHot2 = Color.Black;
                EW.LoadingCircleBackGradient = false;
                EW.LoadingCircleHotGradient = false;
                EW.Shadow_Enabled = false;
                EW.Shadow_Color = Color.Black;
                EW.Shadow_Blur = 5;
                EW.Shadow_Opacity = 0.3f;
                EW.Shadow_OffsetX = 2;
                EW.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor NESW = ref TM.Cursors.Cursor_NESW;
                NESW.ArrowStyle = Paths.ArrowStyle.Classic;
                NESW.CircleStyle = Paths.CircleStyle.Classic;
                NESW.PrimaryColor1 = Color.White;
                NESW.PrimaryColor2 = Color.White;
                NESW.SecondaryColor1 = Color.Black;
                NESW.SecondaryColor2 = Color.Black;
                NESW.PrimaryColorGradient = false;
                NESW.SecondaryColorGradient = false;
                NESW.LoadingCircleBack1 = Color.White;
                NESW.LoadingCircleBack2 = Color.White;
                NESW.LoadingCircleHot1 = Color.Black;
                NESW.LoadingCircleHot2 = Color.Black;
                NESW.LoadingCircleBackGradient = false;
                NESW.LoadingCircleHotGradient = false;
                NESW.Shadow_Enabled = false;
                NESW.Shadow_Color = Color.Black;
                NESW.Shadow_Blur = 5;
                NESW.Shadow_Opacity = 0.3f;
                NESW.Shadow_OffsetX = 2;
                NESW.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor NWSE = ref TM.Cursors.Cursor_NWSE;
                NWSE.ArrowStyle = Paths.ArrowStyle.Classic;
                NWSE.CircleStyle = Paths.CircleStyle.Classic;
                NWSE.PrimaryColor1 = Color.White;
                NWSE.PrimaryColor2 = Color.White;
                NWSE.SecondaryColor1 = Color.Black;
                NWSE.SecondaryColor2 = Color.Black;
                NWSE.PrimaryColorGradient = false;
                NWSE.SecondaryColorGradient = false;
                NWSE.LoadingCircleBack1 = Color.White;
                NWSE.LoadingCircleBack2 = Color.White;
                NWSE.LoadingCircleHot1 = Color.Black;
                NWSE.LoadingCircleHot2 = Color.Black;
                NWSE.LoadingCircleBackGradient = false;
                NWSE.LoadingCircleHotGradient = false;
                NWSE.Shadow_Enabled = false;
                NWSE.Shadow_Color = Color.Black;
                NWSE.Shadow_Blur = 5;
                NWSE.Shadow_Opacity = 0.3f;
                NWSE.Shadow_OffsetX = 2;
                NWSE.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Move = ref TM.Cursors.Cursor_Move;
                Move.ArrowStyle = Paths.ArrowStyle.Classic;
                Move.CircleStyle = Paths.CircleStyle.Classic;
                Move.PrimaryColor1 = Color.White;
                Move.PrimaryColor2 = Color.White;
                Move.SecondaryColor1 = Color.Black;
                Move.SecondaryColor2 = Color.Black;
                Move.PrimaryColorGradient = false;
                Move.SecondaryColorGradient = false;
                Move.LoadingCircleBack1 = Color.White;
                Move.LoadingCircleBack2 = Color.White;
                Move.LoadingCircleHot1 = Color.Black;
                Move.LoadingCircleHot2 = Color.Black;
                Move.LoadingCircleBackGradient = false;
                Move.LoadingCircleHotGradient = false;
                Move.Shadow_Enabled = false;
                Move.Shadow_Color = Color.Black;
                Move.Shadow_Blur = 5;
                Move.Shadow_Opacity = 0.3f;
                Move.Shadow_OffsetX = 2;
                Move.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor None = ref TM.Cursors.Cursor_None;
                None.ArrowStyle = Paths.ArrowStyle.Classic;
                None.CircleStyle = Paths.CircleStyle.Classic;
                None.PrimaryColor1 = Color.Transparent;
                None.PrimaryColor2 = Color.Transparent;
                None.SecondaryColor1 = Color.Black;
                None.SecondaryColor2 = Color.Black;
                None.PrimaryColorGradient = false;
                None.SecondaryColorGradient = false;
                None.LoadingCircleBack1 = Color.White;
                None.LoadingCircleBack2 = Color.White;
                None.LoadingCircleHot1 = Color.Black;
                None.LoadingCircleHot2 = Color.Black;
                None.LoadingCircleBackGradient = false;
                None.LoadingCircleHotGradient = false;
                None.Shadow_Enabled = false;
                None.Shadow_Color = Color.Black;
                None.Shadow_Blur = 5;
                None.Shadow_Opacity = 0.3f;
                None.Shadow_OffsetX = 2;
                None.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Pen = ref TM.Cursors.Cursor_Pen;
                Pen.ArrowStyle = Paths.ArrowStyle.Classic;
                Pen.CircleStyle = Paths.CircleStyle.Classic;
                Pen.PrimaryColor1 = Color.White;
                Pen.PrimaryColor2 = Color.White;
                Pen.SecondaryColor1 = Color.Black;
                Pen.SecondaryColor2 = Color.Black;
                Pen.PrimaryColorGradient = false;
                Pen.SecondaryColorGradient = false;
                Pen.LoadingCircleBack1 = Color.White;
                Pen.LoadingCircleBack2 = Color.White;
                Pen.LoadingCircleHot1 = Color.Black;
                Pen.LoadingCircleHot2 = Color.Black;
                Pen.LoadingCircleBackGradient = false;
                Pen.LoadingCircleHotGradient = false;
                Pen.Shadow_Enabled = false;
                Pen.Shadow_Color = Color.Black;
                Pen.Shadow_Blur = 5;
                Pen.Shadow_Opacity = 0.3f;
                Pen.Shadow_OffsetX = 2;
                Pen.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor IBeam = ref TM.Cursors.Cursor_IBeam;
                IBeam.ArrowStyle = Paths.ArrowStyle.Classic;
                IBeam.CircleStyle = Paths.CircleStyle.Classic;
                IBeam.PrimaryColor1 = Color.White;
                IBeam.PrimaryColor2 = Color.White;
                IBeam.SecondaryColor1 = Color.Black;
                IBeam.SecondaryColor2 = Color.Black;
                IBeam.PrimaryColorGradient = false;
                IBeam.SecondaryColorGradient = false;
                IBeam.LoadingCircleBack1 = Color.White;
                IBeam.LoadingCircleBack2 = Color.White;
                IBeam.LoadingCircleHot1 = Color.Black;
                IBeam.LoadingCircleHot2 = Color.Black;
                IBeam.LoadingCircleBackGradient = false;
                IBeam.LoadingCircleHotGradient = false;
                IBeam.Shadow_Enabled = false;
                IBeam.Shadow_Color = Color.Black;
                IBeam.Shadow_Blur = 5;
                IBeam.Shadow_Opacity = 0.3f;
                IBeam.Shadow_OffsetX = 2;
                IBeam.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Cross = ref TM.Cursors.Cursor_Cross;
                Cross.ArrowStyle = Paths.ArrowStyle.Classic;
                Cross.CircleStyle = Paths.CircleStyle.Classic;
                Cross.PrimaryColor1 = Color.White;
                Cross.PrimaryColor2 = Color.White;
                Cross.SecondaryColor1 = Color.Black;
                Cross.SecondaryColor2 = Color.Black;
                Cross.PrimaryColorGradient = false;
                Cross.SecondaryColorGradient = false;
                Cross.LoadingCircleBack1 = Color.White;
                Cross.LoadingCircleBack2 = Color.White;
                Cross.LoadingCircleHot1 = Color.Black;
                Cross.LoadingCircleHot2 = Color.Black;
                Cross.LoadingCircleBackGradient = false;
                Cross.LoadingCircleHotGradient = false;
                Cross.Shadow_Enabled = false;
                Cross.Shadow_Color = Color.Black;
                Cross.Shadow_Blur = 5;
                Cross.Shadow_Opacity = 0.3f;
                Cross.Shadow_OffsetX = 2;
                Cross.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Link = ref TM.Cursors.Cursor_Link;
                Link.ArrowStyle = Paths.ArrowStyle.Classic;
                Link.CircleStyle = Paths.CircleStyle.Classic;
                Link.PrimaryColor1 = Color.White;
                Link.PrimaryColor2 = Color.White;
                Link.SecondaryColor1 = Color.Black;
                Link.SecondaryColor2 = Color.Black;
                Link.PrimaryColorGradient = false;
                Link.SecondaryColorGradient = false;
                Link.LoadingCircleBack1 = Color.White;
                Link.LoadingCircleBack2 = Color.White;
                Link.LoadingCircleHot1 = Color.Black;
                Link.LoadingCircleHot2 = Color.Black;
                Link.LoadingCircleBackGradient = false;
                Link.LoadingCircleHotGradient = false;
                Link.Shadow_Enabled = false;
                Link.Shadow_Color = Color.Black;
                Link.Shadow_Blur = 5;
                Link.Shadow_Opacity = 0.3f;
                Link.Shadow_OffsetX = 2;
                Link.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Pin = ref TM.Cursors.Cursor_Pin;
                Pin.ArrowStyle = Paths.ArrowStyle.Classic;
                Pin.CircleStyle = Paths.CircleStyle.Classic;
                Pin.PrimaryColor1 = Color.White;
                Pin.PrimaryColor2 = Color.White;
                Pin.SecondaryColor1 = Color.Black;
                Pin.SecondaryColor2 = Color.Black;
                Pin.PrimaryColorGradient = false;
                Pin.SecondaryColorGradient = false;
                Pin.LoadingCircleBack1 = Color.White;
                Pin.LoadingCircleBack2 = Color.White;
                Pin.LoadingCircleHot1 = Color.Black;
                Pin.LoadingCircleHot2 = Color.Black;
                Pin.LoadingCircleBackGradient = false;
                Pin.LoadingCircleHotGradient = false;
                Pin.Shadow_Enabled = false;
                Pin.Shadow_Color = Color.Black;
                Pin.Shadow_Blur = 5;
                Pin.Shadow_Opacity = 0.3f;
                Pin.Shadow_OffsetX = 2;
                Pin.Shadow_OffsetY = 2;
            }

            {
                ref Structures.Cursor Person = ref TM.Cursors.Cursor_Person;
                Person.ArrowStyle = Paths.ArrowStyle.Classic;
                Person.CircleStyle = Paths.CircleStyle.Classic;
                Person.PrimaryColor1 = Color.White;
                Person.PrimaryColor2 = Color.White;
                Person.SecondaryColor1 = Color.Black;
                Person.SecondaryColor2 = Color.Black;
                Person.PrimaryColorGradient = false;
                Person.SecondaryColorGradient = false;
                Person.LoadingCircleBack1 = Color.White;
                Person.LoadingCircleBack2 = Color.White;
                Person.LoadingCircleHot1 = Color.Black;
                Person.LoadingCircleHot2 = Color.Black;
                Person.LoadingCircleBackGradient = false;
                Person.LoadingCircleHotGradient = false;
                Person.Shadow_Enabled = false;
                Person.Shadow_Color = Color.Black;
                Person.Shadow_Blur = 5;
                Person.Shadow_Opacity = 0.3f;
                Person.Shadow_OffsetX = 2;
                Person.Shadow_OffsetY = 2;
            }

            {
                ref Structures.WinEffects WinEffects = ref TM.WindowsEffects;
                WinEffects.ShakeToMinimize = false;
                WinEffects.BalloonNotifications = true;
                WinEffects.PaintDesktopVersion = false;
                WinEffects.ShowSecondsInSystemClock = false;
                WinEffects.Win11ClassicContextMenu = false;
                WinEffects.SysListView32 = true;
            }

            {
                ref Structures.Icons Icons = ref TM.Icons;
                Icons.Computer = $"{SysPaths.Explorer},0";
                Icons.User = $"{SysPaths.System32}\\mydocs.dll,0";
                Icons.Network = $"{SysPaths.System32}\\shell32.dll,17";
                Icons.RecycleBinEmpty = $"{SysPaths.System32}\\shell32.dll,31";
                Icons.RecycleBinFull = $"{SysPaths.System32}\\shell32.dll,32";
                Icons.ControlPanel = $"{SysPaths.System32}\\shell32.dll,21";
            }

            TM.Terminal = new(string.Empty, WinTerminal.Mode.Empty);
            TM.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty);

            TM.Cursors.Enabled = true;
            TM.Cursors.Shadow = true;
            TM.Cursors.Cursor_Arrow.UseFromFile = false;
            TM.Cursors.Cursor_AppLoading.UseFromFile = false;
            TM.Cursors.Cursor_Busy.UseFromFile = false;
            TM.Cursors.Cursor_Help.UseFromFile = false;
            TM.Cursors.Cursor_Pen.UseFromFile = false;
            TM.Cursors.Cursor_None.UseFromFile = false;
            TM.Cursors.Cursor_Move.UseFromFile = false;
            TM.Cursors.Cursor_Up.UseFromFile = false;
            TM.Cursors.Cursor_NS.UseFromFile = false;
            TM.Cursors.Cursor_EW.UseFromFile = false;
            TM.Cursors.Cursor_NESW.UseFromFile = false;
            TM.Cursors.Cursor_NWSE.UseFromFile = false;
            TM.Cursors.Cursor_Link.UseFromFile = false;
            TM.Cursors.Cursor_Pin.UseFromFile = false;
            TM.Cursors.Cursor_Person.UseFromFile = false;
            TM.Cursors.Cursor_IBeam.UseFromFile = false;
            TM.Cursors.Cursor_Cross.UseFromFile = false;
            TM.Cursors.Cursor_Arrow.File = string.Empty;
            TM.Cursors.Cursor_AppLoading.File = string.Empty;
            TM.Cursors.Cursor_Busy.File = string.Empty;
            TM.Cursors.Cursor_Help.File = string.Empty;
            TM.Cursors.Cursor_Pen.File = string.Empty;
            TM.Cursors.Cursor_None.File = string.Empty;
            TM.Cursors.Cursor_Move.File = string.Empty;
            TM.Cursors.Cursor_Up.File = string.Empty;
            TM.Cursors.Cursor_NS.File = string.Empty;
            TM.Cursors.Cursor_EW.File = string.Empty;
            TM.Cursors.Cursor_NESW.File = string.Empty;
            TM.Cursors.Cursor_NWSE.File = string.Empty;
            TM.Cursors.Cursor_Link.File = string.Empty;
            TM.Cursors.Cursor_Pin.File = string.Empty;
            TM.Cursors.Cursor_Person.File = string.Empty;
            TM.Cursors.Cursor_IBeam.File = string.Empty;
            TM.Cursors.Cursor_Cross.File = string.Empty;

            {
                ref Structures.ScreenSaver ScreenSaver = ref TM.ScreenSaver;
                ScreenSaver.Enabled = true;
                ScreenSaver.IsSecure = false;
                ScreenSaver.TimeOut = 60;
                ScreenSaver.File = $@"{SysPaths.System32}\logon.scr";
            }

            {
                ref Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = string.Empty;
                Sounds.Snd_Win_Default = $@"{SysPaths.Windows}\media\Windows XP Ding.wav";
                Sounds.Snd_Win_AppGPFault = string.Empty;
                Sounds.Snd_Win_CCSelect = string.Empty;
                Sounds.Snd_Win_ChangeTheme = string.Empty;
                Sounds.Snd_Win_Close = string.Empty;
                Sounds.Snd_Win_CriticalBatteryAlarm = $@"{SysPaths.Windows}\media\Windows XP Battery Critical.wav";
                Sounds.Snd_Win_DeviceConnect = $@"{SysPaths.Windows}\media\Windows XP Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = $@"{SysPaths.Windows}\media\Windows XP Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = $@"{SysPaths.Windows}\media\Windows XP Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = string.Empty;
                Sounds.Snd_Win_LowBatteryAlarm = $@"{SysPaths.Windows}\media\Windows XP Battery Low.wav";
                Sounds.Snd_Win_MailBeep = $@"{SysPaths.Windows}\media\Windows XP Notify.wav";
                Sounds.Snd_Win_Maximize = string.Empty;
                Sounds.Snd_Win_MenuCommand = string.Empty;
                Sounds.Snd_Win_MenuPopup = string.Empty;
                Sounds.Snd_Win_MessageNudge = string.Empty;
                Sounds.Snd_Win_Minimize = string.Empty;
                Sounds.Snd_Win_Notification_Default = string.Empty;
                Sounds.Snd_Win_Notification_IM = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm2 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm3 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm4 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm5 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm6 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm7 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm8 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm9 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Alarm10 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call2 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call3 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call4 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call5 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call6 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call7 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call8 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call9 = string.Empty;
                Sounds.Snd_Win_Notification_Looping_Call10 = string.Empty;
                Sounds.Snd_Win_Notification_Mail = string.Empty;
                Sounds.Snd_Win_Notification_Proximity = string.Empty;
                Sounds.Snd_Win_Notification_Reminder = string.Empty;
                Sounds.Snd_Win_Notification_SMS = string.Empty;
                Sounds.Snd_Win_Open = string.Empty;
                Sounds.Snd_Win_PrintComplete = string.Empty;
                Sounds.Snd_Win_ProximityConnection = string.Empty;
                Sounds.Snd_Win_RestoreDown = string.Empty;
                Sounds.Snd_Win_RestoreUp = string.Empty;
                Sounds.Snd_Win_ShowBand = string.Empty;
                Sounds.Snd_Win_SystemAsterisk = $@"{SysPaths.Windows}\media\Windows XP Error.wav";
                Sounds.Snd_Win_SystemExclamation = $@"{SysPaths.Windows}\media\Windows XP Exclamation.wav";
                Sounds.Snd_Win_SystemExit = $@"{SysPaths.Windows}\media\Windows XP Shutdown.wav";
                Sounds.Snd_Win_SystemStart = $@"{SysPaths.Windows}\media\Windows XP Startup.wav";
                Sounds.Snd_Win_SystemHand = $@"{SysPaths.Windows}\media\Windows XP Critical Stop.wav";
                Sounds.Snd_Win_SystemNotification = $@"{SysPaths.Windows}\media\Windows XP Balloon.wav";
                Sounds.Snd_Win_SystemQuestion = string.Empty;
                Sounds.Snd_Win_WindowsLogoff = $@"{SysPaths.Windows}\media\Windows XP Logoff Sound.wav";
                Sounds.Snd_Win_WindowsLogon = $@"{SysPaths.Windows}\media\Windows XP Logon Sound.wav";
                Sounds.Snd_Win_WindowsUAC = string.Empty;
                Sounds.Snd_Win_WindowsUnlock = string.Empty;
                Sounds.Snd_Explorer_ActivatingDocument = string.Empty;
                Sounds.Snd_Explorer_BlockedPopup = $@"{SysPaths.Windows}\media\Windows Pop-up Blocked.wav";
                Sounds.Snd_Explorer_EmptyRecycleBin = $@"{SysPaths.Windows}\media\Windows XP Recycle.wav";
                Sounds.Snd_Explorer_FeedDiscovered = $@"{SysPaths.Windows}\media\Windows Feed Discovered.wav";
                Sounds.Snd_Explorer_MoveMenuItem = string.Empty;
                Sounds.Snd_Explorer_Navigating = $@"{SysPaths.Windows}\media\Windows Navigation Start.wav";
                Sounds.Snd_Explorer_SecurityBand = $@"{SysPaths.Windows}\media\Windows Information Bar.wav";
                Sounds.Snd_Explorer_SearchProviderDiscovered = string.Empty;
                Sounds.Snd_Explorer_FaxError = $@"{SysPaths.Windows}\media\ding.wav";
                Sounds.Snd_Explorer_FaxLineRings = $@"{SysPaths.Windows}\media\ringin.wav";
                Sounds.Snd_Explorer_FaxNew = $@"{SysPaths.Windows}\media\notify.wav";
                Sounds.Snd_Explorer_FaxSent = $@"{SysPaths.Windows}\media\tada.wav";
                Sounds.Snd_NetMeeting_PersonJoins = $@"{SysPaths.ProgramFiles}\NetMeeting\Blip.wav";
                Sounds.Snd_NetMeeting_PersonLeaves = $@"{SysPaths.ProgramFiles}\NetMeeting\Blip.wav";
                Sounds.Snd_NetMeeting_ReceiveCall = $@"{SysPaths.Windows}\media\Windows XP RingIn.wav";
                Sounds.Snd_NetMeeting_ReceiveRequestToJoin = $@"{SysPaths.Windows}\media\Windows XP RingIn.wav";
                Sounds.Snd_SpeechRec_DisNumbersSound = string.Empty;
                Sounds.Snd_SpeechRec_HubOffSound = string.Empty;
                Sounds.Snd_SpeechRec_HubOnSound = string.Empty;
                Sounds.Snd_SpeechRec_HubSleepSound = string.Empty;
                Sounds.Snd_SpeechRec_MisrecoSound = string.Empty;
                Sounds.Snd_SpeechRec_PanelSound = string.Empty;
            }

            {
                ref Structures.Wallpaper Wallpaper = ref TM.Wallpaper;
                Wallpaper.ImageFile = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
                Wallpaper.WallpaperStyle = Structures.Wallpaper.WallpaperStyles.Stretched;
            }

            return TM;
        }
    }
}