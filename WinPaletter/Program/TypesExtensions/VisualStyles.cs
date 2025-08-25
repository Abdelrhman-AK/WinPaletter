using libmsstyle;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WinPaletter.Theme.Structures;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for the <see cref="VisualStyle"/> class and other classes in the <see cref="libmsstyle"/> namespace.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Get the <see cref="StyleClass"/> object for the specified class name.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static KeyValuePair<int, StyleClass> Class(this VisualStyle visualStyle, string ClassName = "sysmetrics")
        {
            KeyValuePair<int, StyleClass> cls = new();

            foreach (KeyValuePair<int, StyleClass> c in visualStyle.Classes)
            {
                if (c.Value.ClassName.ToLower() == ClassName.ToLower()) return c;
            }

            return cls;
        }

        /// <summary>
        /// Get the <see cref="StylePart"/> objects for the specified class name.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static Dictionary<int, StylePart> Parts(this VisualStyle visualStyle, string ClassName = "sysmetrics")
        {
            return visualStyle.Class(ClassName).Value.Parts;
        }

        /// <summary>
        /// Get the <see cref="StylePart"/> object for the specified class name and part ID.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="ClassName"></param>
        /// <param name="PartID"></param>
        /// <returns></returns>
        public static KeyValuePair<int, StylePart> Part(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0)
        {
            KeyValuePair<int, StylePart> part = new();

            foreach (KeyValuePair<int, StylePart> p in visualStyle.Class(ClassName).Value.Parts)
            {
                if (p.Value.PartId == PartID) return p;
            }

            return part;
        }

        /// <summary>
        /// Get the <see cref="StyleState"/> objects for the specified class name and part ID.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="ClassName"></param>
        /// <param name="PartID"></param>
        /// <returns></returns>
        public static Dictionary<int, StyleState> States(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0)
        {
            return visualStyle.Part(ClassName, PartID).Value.States;
        }

        /// <summary>
        /// Get the <see cref="StyleState"/> object for the specified class name, part ID, and state ID.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="ClassName"></param>
        /// <param name="PartID"></param>
        /// <param name="StateID"></param>
        /// <returns></returns>
        public static KeyValuePair<int, StyleState> State(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0, int StateID = 0)
        {
            foreach (KeyValuePair<int, StyleState> s in visualStyle.Part(ClassName, PartID).Value.States)
            {
                if (s.Value.StateId == StateID) return s;
            }

            return new KeyValuePair<int, StyleState>();
        }

        /// <summary>
        /// Get the <see cref="StyleProperty"/> objects for the specified class name, part ID, and state ID.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="ClassName"></param>
        /// <param name="PartID"></param>
        /// <param name="StateID"></param>
        /// <returns></returns>
        public static List<StyleProperty> Properties(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0, int StateID = 0)
        {
            return visualStyle.State(ClassName, PartID, StateID).Value.Properties;
        }

        /// <summary>
        /// Get WinPaletter's <see cref="Theme.Structures.Win32UI"/> structure from the specified <see cref="VisualStyle"/> object.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="PartID"></param>
        /// <param name="StateID"></param>
        /// <returns></returns>
        public static Theme.Structures.Win32UI ClassicColors(this VisualStyle visualStyle, int PartID = 0, int StateID = 0)
        {
            List<StyleProperty> properties = visualStyle.Properties("sysmetrics", PartID, StateID);

            Theme.Structures.Win32UI classicColors = new()
            {
                ActiveBorder = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.ACTIVEBORDER).FirstOrDefault().GetValue(),
                ActiveTitle = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.ACTIVECAPTION).FirstOrDefault().GetValue(),
                AppWorkspace = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.APPWORKSPACE).FirstOrDefault().GetValue(),
                ButtonFace = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.BTNFACE).FirstOrDefault().GetValue(),
                ButtonHilight = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.BTNHIGHLIGHT).FirstOrDefault().GetValue(),
                ButtonShadow = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.BTNSHADOW).FirstOrDefault().GetValue(),
                ButtonText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.BTNTEXT).FirstOrDefault().GetValue(),
                ButtonDkShadow = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.DKSHADOW3D).FirstOrDefault().GetValue(),
                ButtonLight = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.LIGHT3D).FirstOrDefault().GetValue(),
                TitleText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.CAPTIONTEXT).FirstOrDefault().GetValue(),
                GradientActiveTitle = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.GRADIENTACTIVECAPTION).FirstOrDefault().GetValue(),
                GradientInactiveTitle = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.GRADIENTINACTIVECAPTION).FirstOrDefault().GetValue(),
                GrayText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.GRAYTEXT).FirstOrDefault().GetValue(),
                Hilight = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.HIGHLIGHT).FirstOrDefault().GetValue(),
                HilightText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.HIGHLIGHTTEXT).FirstOrDefault().GetValue(),
                HotTrackingColor = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.HOTTRACKING).FirstOrDefault().GetValue(),
                InactiveBorder = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.INACTIVEBORDER).FirstOrDefault().GetValue(),
                InactiveTitle = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.INACTIVECAPTION).FirstOrDefault().GetValue(),
                InactiveTitleText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.INACTIVECAPTIONTEXT).FirstOrDefault().GetValue(),
                InfoText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.INFOTEXT).FirstOrDefault().GetValue(),
                InfoWindow = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.INFOBK).FirstOrDefault().GetValue(),
                Menu = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.MENU).FirstOrDefault().GetValue(),
                MenuHilight = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.MENUHILIGHT).FirstOrDefault().GetValue(),
                MenuText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.MENUTEXT).FirstOrDefault().GetValue(),
                Scrollbar = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.SCROLLBAR).FirstOrDefault().GetValue(),
                Window = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.WINDOW).FirstOrDefault().GetValue(),
                WindowFrame = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.WINDOWFRAME).FirstOrDefault().GetValue(),
                WindowText = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.WINDOWTEXT).FirstOrDefault().GetValue(),
                Background = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.BACKGROUND).FirstOrDefault().GetValue(),
                ButtonAlternateFace = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.BUTTONALTERNATEFACE).FirstOrDefault().GetValue(),
                Desktop = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.BACKGROUND).FirstOrDefault().GetValue(),
                MenuBar = (Color)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.MENUBAR).FirstOrDefault().GetValue(),
                EnableGradient = true,
                EnableTheming = (bool)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.FLATMENUS).FirstOrDefault().GetValue()
            };

            return classicColors;
        }

        /// <summary>
        /// Get WinPaletter's <see cref="Theme.Structures.MetricsFonts"/> structure from the specified <see cref="VisualStyle"/> object.
        /// </summary>
        /// <param name="visualStyle"></param>
        /// <param name="PartID"></param>
        /// <param name="StateID"></param>
        /// <returns></returns>
        public static MetricsFonts MetricsFonts(this VisualStyle visualStyle, int PartID = 0, int StateID = 0)
        {
            StylePart cp = visualStyle.Class("sysmetrics").Value.Parts.FirstOrDefault().Value;
            StyleState state = cp.States.FirstOrDefault().Value;
            Dictionary<int, string> fonts = StringTable.FilterFonts(visualStyle.PreferredStringTable);

            MetricsFonts metricsFonts = new()
            {
                CaptionHeight = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.CAPTIONBARHEIGHT).FirstOrDefault().GetValue(),
                CaptionWidth = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.CAPTIONBARWIDTH).FirstOrDefault().GetValue(),
                BorderWidth = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.SIZINGBORDERWIDTH).FirstOrDefault().GetValue(),
                PaddedBorderWidth = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.PADDEDBORDERWIDTH).FirstOrDefault().GetValue(),
                ScrollHeight = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.SCROLLBARHEIGHT).FirstOrDefault().GetValue(),
                ScrollWidth = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.SCROLLBARWIDTH).FirstOrDefault().GetValue(),
                SmCaptionHeight = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.SMCAPTIONBARHEIGHT).FirstOrDefault().GetValue(),
                SmCaptionWidth = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.SMCAPTIONBARWIDTH).FirstOrDefault().GetValue(),
                MenuWidth = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.MENUBARWIDTH).FirstOrDefault().GetValue(),
                MenuHeight = (int)state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.MENUBARHEIGHT).FirstOrDefault().GetValue(),
                DesktopIconSize = Program.TM.MetricsFonts.DesktopIconSize,
                IconSpacing = Program.TM.MetricsFonts.IconSpacing,
                IconVerticalSpacing = Program.TM.MetricsFonts.IconVerticalSpacing,
                ShellIconSize = Program.TM.MetricsFonts.ShellIconSize,
                ShellSmallIconSize = Program.TM.MetricsFonts.ShellSmallIconSize,
                Fonts_SingleBitPP = Program.TM.MetricsFonts.Fonts_SingleBitPP,
            };

            StyleProperty p = state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.CAPTIONFONT).FirstOrDefault();
            metricsFonts.CaptionFont = ParseFontFromString(fonts.Where(f => f.Key == (int)p.GetValue()).FirstOrDefault().Value);

            p = state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.ICONTITLEFONT).FirstOrDefault();
            metricsFonts.IconFont = ParseFontFromString(fonts.Where(f => f.Key == (int)p.GetValue()).FirstOrDefault().Value);

            p = state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.MENUFONT).FirstOrDefault();
            metricsFonts.MenuFont = ParseFontFromString(fonts.Where(f => f.Key == (int)p.GetValue()).FirstOrDefault().Value);

            p = state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.MSGBOXFONT).FirstOrDefault();
            metricsFonts.MessageFont = ParseFontFromString(fonts.Where(f => f.Key == (int)p.GetValue()).FirstOrDefault().Value);

            p = state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.SMALLCAPTIONFONT).FirstOrDefault();
            metricsFonts.SmCaptionFont = ParseFontFromString(fonts.Where(f => f.Key == (int)p.GetValue()).FirstOrDefault().Value);

            p = state.Properties.Where(p => p.Header.nameID == (int)IDENTIFIER.STATUSFONT).FirstOrDefault();
            metricsFonts.StatusFont = ParseFontFromString(fonts.Where(f => f.Key == (int)p.GetValue()).FirstOrDefault().Value);

            return metricsFonts;
        }

        /// <summary>
        /// Parse a font string from a string embedded in a Windows Visual Styles to a <see cref="Font"/> object.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static Font ParseFontFromString(string inputString)
        {
            Font defaultFont = new("Segoe UI", 9f, FontStyle.Regular);

            try
            {
                FontStyle fontStyle = FontStyle.Regular;

                string[] parts = inputString.Split(',');

                if (parts.Length < 2) return defaultFont;

                string family = parts[0].Trim();
                float size = float.Parse(parts[1].Trim());

                if (parts.Length >= 3)
                {
                    string styleStr = parts[2].Trim().ToLower();
                    if (styleStr == "bold") fontStyle |= FontStyle.Bold;
                    else if (styleStr == "italic") fontStyle |= FontStyle.Italic;
                    else if (styleStr == "strikeout") fontStyle |= FontStyle.Strikeout;
                    else if (styleStr == "underline") fontStyle |= FontStyle.Underline;
                }

                return new Font(family, size, fontStyle);
            }
            catch // Couldn't parse the font string, return the default font
            {
                return defaultFont;
            }
        }
    }
}