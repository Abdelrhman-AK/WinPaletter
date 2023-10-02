using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct Cursor : ICloneable
    {
        public Paths.ArrowStyle ArrowStyle;
        public Paths.CircleStyle CircleStyle;
        public Color PrimaryColor1;
        public Color PrimaryColor2;
        public bool PrimaryColorGradient;
        public Paths.GradientMode PrimaryColorGradientMode;
        public bool PrimaryColorNoise;
        public float PrimaryColorNoiseOpacity;
        public Color SecondaryColor1;
        public Color SecondaryColor2;
        public bool SecondaryColorGradient;
        public Paths.GradientMode SecondaryColorGradientMode;
        public bool SecondaryColorNoise;
        public float SecondaryColorNoiseOpacity;
        public Color LoadingCircleBack1;
        public Color LoadingCircleBack2;
        public bool LoadingCircleBackGradient;
        public Paths.GradientMode LoadingCircleBackGradientMode;
        public bool LoadingCircleBackNoise;
        public float LoadingCircleBackNoiseOpacity;
        public Color LoadingCircleHot1;
        public Color LoadingCircleHot2;
        public bool LoadingCircleHotGradient;
        public Paths.GradientMode LoadingCircleHotGradientMode;
        public bool LoadingCircleHotNoise;
        public float LoadingCircleHotNoiseOpacity;
        public bool Shadow_Enabled;
        public Color Shadow_Color;
        public int Shadow_Blur;
        public float Shadow_Opacity;
        public int Shadow_OffsetX;
        public int Shadow_OffsetY;

        public void Load(string subKey)
        {
            ArrowStyle = (Paths.ArrowStyle)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "ArrowStyle", Paths.ArrowStyle.Aero));
            CircleStyle = (Paths.CircleStyle)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "CircleStyle", Paths.CircleStyle.Aero));

            PrimaryColor1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor1", Color.White.ToArgb())));
            PrimaryColor2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor2", Color.White.ToArgb())));
            SecondaryColor1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor1", (subKey.ToLower() != "none" ? Color.Black : Color.Red).ToArgb())));
            SecondaryColor2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor2", (subKey.ToLower() != "none" ? Color.FromArgb(64, 65, 75) : Color.Red).ToArgb())));
            LoadingCircleBack1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb())));
            LoadingCircleBack2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb())));
            LoadingCircleHot1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb())));
            LoadingCircleHot2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb())));

            PrimaryColorGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradient", false));
            SecondaryColorGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradient", true));
            LoadingCircleBackGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradient", false));
            LoadingCircleHotGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradient", false));

            PrimaryColorNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoise", false));
            SecondaryColorNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoise", false));
            LoadingCircleBackNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoise", false));
            LoadingCircleHotNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoise", false));

            PrimaryColorGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradientMode", (int)Paths.GradientMode.Circle));
            SecondaryColorGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradientMode", (int)Paths.GradientMode.Vertical));
            LoadingCircleBackGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradientMode", (int)Paths.GradientMode.Circle));
            LoadingCircleHotGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradientMode", (int)Paths.GradientMode.Circle));

            PrimaryColorNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoiseOpacity", 25).ToString()) / 100;
            SecondaryColorNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoiseOpacity", 25).ToString()) / 100;
            LoadingCircleBackNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoiseOpacity", 25).ToString()) / 100;
            LoadingCircleHotNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoiseOpacity", 25).ToString()) / 100;

            Shadow_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Enabled", false));
            Shadow_Color = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Color", Color.Black.ToArgb())));
            Shadow_Blur = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Blur", 5));
            Shadow_Opacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Opacity", 30).ToString()) / 100;
            Shadow_OffsetX = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetX", 2));
            Shadow_OffsetY = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetY", 2));
        }
        public static void Save_Cursors_To_Registry(string subKey, Cursor Cursor, TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "ArrowStyle", (int)Cursor.ArrowStyle);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "CircleStyle", (int)Cursor.CircleStyle);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor1", Cursor.PrimaryColor1.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor2", Cursor.PrimaryColor2.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradient", Cursor.PrimaryColorGradient.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradientMode", (int)Cursor.PrimaryColorGradientMode);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoise", Cursor.PrimaryColorNoise.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoiseOpacity", Cursor.PrimaryColorNoiseOpacity * 100f);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor1", Cursor.SecondaryColor1.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor2", Cursor.SecondaryColor2.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradient", Cursor.SecondaryColorGradient.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradientMode", (int)Cursor.SecondaryColorGradientMode);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoise", Cursor.SecondaryColorNoise.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoiseOpacity", Cursor.SecondaryColorNoiseOpacity * 100f);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack1", Cursor.LoadingCircleBack1.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack2", Cursor.LoadingCircleBack2.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradient", Cursor.LoadingCircleBackGradient.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradientMode", (int)Cursor.LoadingCircleBackGradientMode);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoise", Cursor.LoadingCircleBackNoise.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoiseOpacity", Cursor.LoadingCircleBackNoiseOpacity * 100f);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot1", Cursor.LoadingCircleHot1.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot2", Cursor.LoadingCircleHot2.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradient", Cursor.LoadingCircleHotGradient.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradientMode", (int)Cursor.LoadingCircleHotGradientMode);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoise", Cursor.LoadingCircleHotNoise.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoiseOpacity", Cursor.LoadingCircleHotNoiseOpacity * 100f);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Enabled", Cursor.Shadow_Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Color", Cursor.Shadow_Color.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Blur", Cursor.Shadow_Blur);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Opacity", (int)Math.Round(Cursor.Shadow_Opacity * 100f));
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetX", Cursor.Shadow_OffsetX);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetY", Cursor.Shadow_OffsetY);
        }

        public static bool operator ==(Cursor First, Cursor Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(Cursor First, Cursor Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
