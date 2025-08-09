using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for a single WinPaletter themed cursor inside <see cref="Cursors"/> structure instance.
    /// </summary>
    public class Cursor : ICloneable
    {
        /// <summary>Use a cursor file instead of rendering a new style using WinPaletter</summary>
        public bool UseFromFile = false;

        /// <summary>Used cursor File</summary>
        public string File = string.Empty;

        /// <summary>Style of main arrow</summary>
        public Paths.ArrowStyle ArrowStyle = Paths.ArrowStyle.Aero;

        /// <summary>Style of appwait/busy cursors</summary>
        public Paths.CircleStyle CircleStyle = Paths.CircleStyle.Aero;

        /// <summary>BackColor for cursor</summary>
        public Color PrimaryColor1 = Color.White;

        /// <summary>
        /// Second BackColor for cursor
        /// <br>- Used as gradience, when <c>PrimaryColorGradient=true;</c></br>
        /// </summary>
        public Color PrimaryColor2 = Color.White;

        /// <summary>Enable BackColor gradience for cursor</summary>
        public bool PrimaryColorGradient = false;

        /// <summary>
        /// BackColor gradience effect for cursor
        /// <code>
        /// Vertical
        /// Horizontal
        /// ForwardDiagonal
        /// BackwardDiagonal
        /// Circle (Animated with loading when cursor is appwait or busy)
        /// </code>
        /// </summary>
        public Paths.GradientMode PrimaryColorGradientMode = Paths.GradientMode.Vertical;

        /// <summary>Enable BackColor noise for cursor</summary>
        public bool PrimaryColorNoise = false;

        /// <summary>Opacity of BackColor noise for cursor</summary>
        public float PrimaryColorNoiseOpacity = 0.25f;

        /// <summary>Line color for cursor</summary>
        public Color SecondaryColor1 = Color.FromArgb(24, 24, 36);

        /// <summary>
        /// Second line color for cursor
        /// <br>- Used as gradience, when <c>SecondaryColorGradient=true;</c></br>
        /// </summary>
        public Color SecondaryColor2 = Color.FromArgb(66, 67, 77);

        /// <summary>Enable line color gradience for cursor</summary>
        public bool SecondaryColorGradient = true;

        /// <summary>
        /// Line color gradience effect for cursor
        /// <code>
        /// Vertical
        /// Horizontal
        /// ForwardDiagonal
        /// BackwardDiagonal
        /// Circle (Animated with loading when cursor is appwait or busy)
        /// </code>
        /// </summary>
        public Paths.GradientMode SecondaryColorGradientMode = Paths.GradientMode.Horizontal;

        /// <summary>Enable line noise for cursor</summary>
        public bool SecondaryColorNoise = false;

        /// <summary>Opacity of line noise for cursor</summary>
        public float SecondaryColorNoiseOpacity = 0.25f;

        /// <summary>
        /// Specifies the thickness of the border.
        /// </summary>
        /// <remarks>The value represents the thickness of the border in device-independent units (DIUs).
        /// A value of <see langword="1.0f"/> corresponds to a border thickness of 1 DIU.</remarks>
        public float BorderThickness = 1.0f;

        /// <summary>BackColor of spinning circle (if cursor is appwait or busy)</summary>
        public Color LoadingCircleBack1 = Color.FromArgb(42, 151, 243);

        /// <summary>
        /// Second BackColor of spinning circle (if cursor is appwait or busy)
        /// <br>- Used as gradience, when <c>LoadingCircleBackGradient=true;</c></br>
        /// </summary>
        public Color LoadingCircleBack2 = Color.FromArgb(42, 151, 243);

        /// <summary>Enable gradience in background of spinning circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleBackGradient = false;

        /// <summary>
        /// Gradience in the background of spinning circle (if cursor is appwait or busy)
        /// <code>
        /// Vertical
        /// Horizontal
        /// ForwardDiagonal
        /// BackwardDiagonal
        /// Circle (Animated with loading when cursor is appwait or busy)
        /// </code>
        /// </summary>
        public Paths.GradientMode LoadingCircleBackGradientMode = Paths.GradientMode.Circle;

        /// <summary>Enable noise in background of spinning circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleBackNoise = false;

        /// <summary>Opacity noise in background of spinning circle (if cursor is appwait or busy)</summary>
        public float LoadingCircleBackNoiseOpacity = 0.25f;

        /// <summary>Color of rotating part of circle (if cursor is appwait or busy)</summary>
        public Color LoadingCircleHot1 = Color.FromArgb(37, 204, 255);

        /// <summary>
        /// Second color of rotating part of circle (if cursor is appwait or busy)
        /// <br>- Used as gradience, when LoadingCircleHotGradient=true;</br>
        /// </summary>
        public Color LoadingCircleHot2 = Color.FromArgb(37, 204, 255);

        /// <summary>Enable gradience in rotating part of circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleHotGradient = false;

        /// <summary>
        /// Gradience in background of spinning circle (if cursor is appwait or busy)
        /// <code>
        /// Vertical
        /// Horizontal
        /// ForwardDiagonal
        /// BackwardDiagonal
        /// Circle (Animated with loading when cursor is appwait or busy)
        /// </code>
        /// </summary>
        public Paths.GradientMode LoadingCircleHotGradientMode = Paths.GradientMode.Circle;

        /// <summary>Enable noise in rotating part of circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleHotNoise = false;

        /// <summary>Opacity noise in rotating part of circle (if cursor is appwait or busy</summary>
        public float LoadingCircleHotNoiseOpacity = 0.25f;

        /// <summary>
        /// Represents the animation speed of the loading circle.
        /// </summary>
        /// <remarks>The value determines how quickly the loading circle animates.  A higher value results
        /// in faster animation, while a lower value slows it down.</remarks>
        public int LoadingCircleHot_AnimationSpeed = 10;

        /// <summary>Enable custom cursor shadow (rendered by WinPaletter not Windows)</summary>
        public bool Shadow_Enabled = false;

        /// <summary>Custom cursor shadow color</summary>
        public Color Shadow_Color = Color.Black;

        /// <summary>Custom cursor shadow blur intensity</summary>
        public int Shadow_Blur = 5;

        /// <summary>Custom cursor shadow opacity</summary>
        public float Shadow_Opacity = 0.3f;

        /// <summary>Custom cursor shadow x offset in coordinates (location)</summary>
        public int Shadow_OffsetX = 2;

        /// <summary>Custom cursor shadow y offset in coordinates (location)</summary>
        public int Shadow_OffsetY = 2;

        /// <summary>
        /// Create a new of Cursors with default values
        /// </summary>
        public Cursor() { }

        /// <summary>
        /// Loads WinPaletter themed cursors data from registry
        /// </summary>
        /// <param name="subKey">Subkey of cursor inside registry key HKEY_CURRENT_USER\Software\WinPaletter\Cursors</param>
        public void Load(string subKey)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Cursor to be loaded: `{subKey}`");

            UseFromFile = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "UseFromFile", false));
            File = GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "File", string.Empty).ToString();

            ArrowStyle = (Paths.ArrowStyle)Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "ArrowStyle", Paths.ArrowStyle.Aero));
            CircleStyle = (Paths.CircleStyle)Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "CircleStyle", Paths.CircleStyle.Aero));

            PrimaryColor1 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor1", Color.White.ToArgb())));
            PrimaryColor2 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor2", Color.White.ToArgb())));
            SecondaryColor1 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor1", (subKey.ToLower() != "none" ? Color.Black : Color.Red).ToArgb())));
            SecondaryColor2 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor2", (subKey.ToLower() != "none" ? Color.FromArgb(64, 65, 75) : Color.Red).ToArgb())));
            LoadingCircleBack1 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb())));
            LoadingCircleBack2 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb())));
            LoadingCircleHot1 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb())));
            LoadingCircleHot2 = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb())));

            PrimaryColorGradient = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradient", false));
            SecondaryColorGradient = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradient", true));
            LoadingCircleBackGradient = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradient", false));
            LoadingCircleHotGradient = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradient", false));

            PrimaryColorNoise = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoise", false));
            SecondaryColorNoise = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoise", false));
            LoadingCircleBackNoise = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoise", false));
            LoadingCircleHotNoise = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoise", false));

            PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradientMode", "circle").ToString());
            SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradientMode", "vertical").ToString());
            LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradientMode", "circle").ToString());
            LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradientMode", "circle").ToString());
            LoadingCircleHot_AnimationSpeed = Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot_AnimationSpeed", 10));

            PrimaryColorNoiseOpacity = float.Parse(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoiseOpacity", 25).ToString()) / 100;
            SecondaryColorNoiseOpacity = float.Parse(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoiseOpacity", 25).ToString()) / 100;
            LoadingCircleBackNoiseOpacity = float.Parse(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoiseOpacity", 25).ToString()) / 100;
            LoadingCircleHotNoiseOpacity = float.Parse(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoiseOpacity", 25).ToString()) / 100;
            BorderThickness = float.Parse(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "BorderThickness", 1000f).ToString()) / 1000f;

            Shadow_Enabled = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Enabled", false));
            Shadow_Color = Color.FromArgb(Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Color", Color.Black.ToArgb())));
            Shadow_Blur = Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Blur", 5));
            Shadow_Opacity = float.Parse(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Opacity", 30).ToString()) / 100;
            Shadow_OffsetX = Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetX", 2));
            Shadow_OffsetY = Convert.ToInt32(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetY", 2));
        }

        /// <summary>
        /// ApplyToTM data of WinPaletter themed cursors into registry
        /// </summary>
        /// <param name="subKey">Subkey of cursor inside registry key HKEY_CURRENT_USER\Software\WinPaletter\Cursors</param>
        /// <param name="Cursor">WinPaletter themed cursor structure</param>
        /// <param name="treeView">TreeView used for theme log</param>
        public static void Save_Cursors_To_Registry(string subKey, Cursor Cursor, TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Cursor to be saved: `{subKey}`");

            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "UseFromFile", Cursor.UseFromFile);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "File", Cursor.File, RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "ArrowStyle", (int)Cursor.ArrowStyle);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "CircleStyle", (int)Cursor.CircleStyle);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor1", Cursor.PrimaryColor1.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor2", Cursor.PrimaryColor2.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradient", Cursor.PrimaryColorGradient ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradientMode", Paths.ReturnStringFromGradientMode(Cursor.PrimaryColorGradientMode), RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoise", Cursor.PrimaryColorNoise ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoiseOpacity", Cursor.PrimaryColorNoiseOpacity * 100f);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor1", Cursor.SecondaryColor1.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor2", Cursor.SecondaryColor2.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradient", Cursor.SecondaryColorGradient ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradientMode", Paths.ReturnStringFromGradientMode(Cursor.SecondaryColorGradientMode), RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoise", Cursor.SecondaryColorNoise ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoiseOpacity", Cursor.SecondaryColorNoiseOpacity * 100f);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "BorderThickness", Cursor.BorderThickness * 1000f);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack1", Cursor.LoadingCircleBack1.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack2", Cursor.LoadingCircleBack2.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradient", Cursor.LoadingCircleBackGradient ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradientMode", Paths.ReturnStringFromGradientMode(Cursor.LoadingCircleBackGradientMode), RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoise", Cursor.LoadingCircleBackNoise ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoiseOpacity", Cursor.LoadingCircleBackNoiseOpacity * 100f);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot1", Cursor.LoadingCircleHot1.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot2", Cursor.LoadingCircleHot2.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradient", Cursor.LoadingCircleHotGradient ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradientMode", Paths.ReturnStringFromGradientMode(Cursor.LoadingCircleHotGradientMode), RegistryValueKind.String);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoise", Cursor.LoadingCircleHotNoise ? 1 : 0);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoiseOpacity", Cursor.LoadingCircleHotNoiseOpacity * 100f);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot_AnimationSpeed", Cursor.LoadingCircleHot_AnimationSpeed);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Enabled", Cursor.Shadow_Enabled);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Color", Cursor.Shadow_Color.ToArgb());
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Blur", Cursor.Shadow_Blur);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Opacity", (int)Math.Round(Cursor.Shadow_Opacity * 100f));
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetX", Cursor.Shadow_OffsetX);
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetY", Cursor.Shadow_OffsetY);
        }

        /// <summary>Operator to check if two Cursor structures are equal</summary>
        public static bool operator ==(Cursor First, Cursor Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Cursor structures are not equal</summary>
        public static bool operator !=(Cursor First, Cursor Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Cursor structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Cursor structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Cursor structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
