using libmsstyle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WinPaletter.TypesExtensions
{
    public static class Extensions
    {
        public static KeyValuePair<int, StyleClass> Class(this VisualStyle visualStyle, string ClassName = "sysmetrics")
        {
            KeyValuePair<int, StyleClass> cls = new();

            foreach (KeyValuePair<int, StyleClass> c in visualStyle.Classes)
            {
                if (c.Value.ClassName.ToLower() == ClassName.ToLower()) return c;
            }

            return cls;
        }

        public static Dictionary<int, StylePart> Parts(this VisualStyle visualStyle, string ClassName = "sysmetrics")
        {
            return visualStyle.Class(ClassName).Value.Parts;
        }

        public static KeyValuePair<int, StylePart> Part(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0)
        {
            KeyValuePair<int, StylePart> part = new();

            foreach (KeyValuePair<int, StylePart> p in visualStyle.Class(ClassName).Value.Parts)
            {
                if (p.Value.PartId == PartID) return p;
            }

            return part;
        }

        public static Dictionary<int, StyleState> States(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0)
        {
            return visualStyle.Part(ClassName, PartID).Value.States;
        }

        public static KeyValuePair<int, StyleState> State(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0, int StateID = 0)
        {
            foreach (KeyValuePair<int, StyleState> s in visualStyle.Part(ClassName, PartID).Value.States)
            {
                if (s.Value.StateId == StateID) return s;
            }

            return new KeyValuePair<int, StyleState>();
        }

        public static List<StyleProperty> Properties(this VisualStyle visualStyle, string ClassName = "sysmetrics", int PartID = 0, int StateID = 0)
        {
            return visualStyle.State(ClassName, PartID, StateID).Value.Properties;
        }

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

        public static Theme.Structures.MetricsFonts MetricsFonts(this VisualStyle visualStyle, int PartID = 0, int StateID = 0)
        {
            List<StyleProperty> properties = visualStyle.Properties("sysmetrics", PartID, StateID);


            MsgBox(properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.CAPTIONFONT).FirstOrDefault().GetValueAsString());

            Theme.Structures.MetricsFonts metricsFonts = new()
            {
                CaptionHeight = (int)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.CAPTIONBARHEIGHT).FirstOrDefault().GetValue(),
                ScrollHeight = (int)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.SCROLLBARHEIGHT).FirstOrDefault().GetValue(),
                ScrollWidth = (int)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.SCROLLBARWIDTH).FirstOrDefault().GetValue(),
                SmCaptionHeight = (int)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.SMCAPTIONBARHEIGHT).FirstOrDefault().GetValue(),
                SmCaptionWidth = (int)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.SMCAPTIONBARWIDTH).FirstOrDefault().GetValue()
                //CaptionFont = (Font)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.CAPTIONFONT).FirstOrDefault().GetValue(),
                //IconFont = (Font)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.ICONTITLEFONT).FirstOrDefault().GetValue(),
                //MenuFont = (Font)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.MENUFONT).FirstOrDefault().GetValue(),
                //SmCaptionFont = (Font)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.SMALLCAPTIONFONT).FirstOrDefault().GetValue(),
                //MessageFont = (Font)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.MSGBOXFONT).FirstOrDefault().GetValue(),
                //StatusFont = (Font)properties.Where(i => (IDENTIFIER)i.Header.nameID == IDENTIFIER.STATUSFONT).FirstOrDefault().GetValue()
            };

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                metricsFonts.CaptionFont = TMx.MetricsFonts.CaptionFont;
                metricsFonts.SmCaptionFont = TMx.MetricsFonts.SmCaptionFont;
                metricsFonts.IconFont = TMx.MetricsFonts.IconFont;
                metricsFonts.MenuFont = TMx.MetricsFonts.MenuFont;
                metricsFonts.MessageFont = TMx.MetricsFonts.MessageFont;
                metricsFonts.StatusFont = TMx.MetricsFonts.StatusFont;
            }

            return metricsFonts;
        }
    }
}