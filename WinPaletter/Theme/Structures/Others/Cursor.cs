using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for a single WinPaletter themed cursor inside <see cref="Cursors"/> structure instance.
    /// </summary>
    public class Cursor : ManagerBase<Cursor>
    {
        /// <summary>Use a cursor file instead of rendering a new style using WinPaletter</summary>
        public bool UseFromFile { get; set; } = false;

        /// <summary>Used cursor File</summary>
        public string File { get; set; } = string.Empty;

        /// <summary>Style of main arrow</summary>
        public Paths.ArrowStyle ArrowStyle { get; set; } = Paths.ArrowStyle.Aero;

        /// <summary>Style of appwait/busy cursors</summary>
        public Paths.CircleStyle CircleStyle { get; set; } = Paths.CircleStyle.Aero;

        /// <summary>BackColor for cursor</summary>
        public Color PrimaryColor1 { get; set; } = Color.White;

        /// <summary>
        /// Second BackColor for cursor
        /// <br>- Used as gradience, when <c>PrimaryColorGradient=true;</c></br>
        /// </summary>
        public Color PrimaryColor2 { get; set; } = Color.White;

        /// <summary>Enable BackColor gradience for cursor</summary>
        public bool PrimaryColorGradient { get; set; } = false;

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
        public Paths.GradientMode PrimaryColorGradientMode { get; set; } = Paths.GradientMode.Vertical;

        /// <summary>Enable BackColor noise for cursor</summary>
        public bool PrimaryColorNoise { get; set; } = false;

        /// <summary>Opacity of BackColor noise for cursor</summary>
        public float PrimaryColorNoiseOpacity { get; set; } = 0.25f;

        /// <summary>Line color for cursor</summary>
        public Color SecondaryColor1 { get; set; } = Color.FromArgb(24, 24, 36);

        /// <summary>
        /// Second line color for cursor
        /// <br>- Used as gradience, when <c>SecondaryColorGradient=true;</c></br>
        /// </summary>
        public Color SecondaryColor2 { get; set; } = Color.FromArgb(66, 67, 77);

        /// <summary>Enable line color gradience for cursor</summary>
        public bool SecondaryColorGradient { get; set; } = true;

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
        public Paths.GradientMode SecondaryColorGradientMode { get; set; } = Paths.GradientMode.Horizontal;

        /// <summary>Enable line noise for cursor</summary>
        public bool SecondaryColorNoise { get; set; } = false;

        /// <summary>Opacity of line noise for cursor</summary>
        public float SecondaryColorNoiseOpacity { get; set; } = 0.25f;

        /// <summary>
        /// Specifies the thickness of the border.
        /// </summary>
        /// <remarks>The value represents the thickness of the border in device-independent units (DIUs).
        /// A value of <see langword="1.0f"/> corresponds to a border thickness of 1 DIU.</remarks>
        public float BorderThickness { get; set; } = 1.0f;

        /// <summary>BackColor of spinning circle (if cursor is appwait or busy)</summary>
        public Color LoadingCircleBack1 { get; set; } = Color.FromArgb(42, 151, 243);

        /// <summary>
        /// Second BackColor of spinning circle (if cursor is appwait or busy)
        /// <br>- Used as gradience, when <c>LoadingCircleBackGradient=true;</c></br>
        /// </summary>
        public Color LoadingCircleBack2 { get; set; } = Color.FromArgb(42, 151, 243);

        /// <summary>Enable gradience in background of spinning circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleBackGradient { get; set; } = false;

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
        public Paths.GradientMode LoadingCircleBackGradientMode { get; set; } = Paths.GradientMode.Circle;

        /// <summary>Enable noise in background of spinning circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleBackNoise { get; set; } = false;

        /// <summary>Opacity noise in background of spinning circle (if cursor is appwait or busy)</summary>
        public float LoadingCircleBackNoiseOpacity { get; set; } = 0.25f;

        /// <summary>Color of rotating part of circle (if cursor is appwait or busy)</summary>
        public Color LoadingCircleHot1 { get; set; } = Color.FromArgb(37, 204, 255);

        /// <summary>
        /// Second color of rotating part of circle (if cursor is appwait or busy)
        /// <br>- Used as gradience, when LoadingCircleHotGradient=true;</br>
        /// </summary>
        public Color LoadingCircleHot2 { get; set; } = Color.FromArgb(37, 204, 255);

        /// <summary>Enable gradience in rotating part of circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleHotGradient { get; set; } = false;

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
        public Paths.GradientMode LoadingCircleHotGradientMode { get; set; } = Paths.GradientMode.Circle;

        /// <summary>Enable noise in rotating part of circle (if cursor is appwait or busy)</summary>
        public bool LoadingCircleHotNoise { get; set; } = false;

        /// <summary>Opacity noise in rotating part of circle (if cursor is appwait or busy</summary>
        public float LoadingCircleHotNoiseOpacity { get; set; } = 0.25f;

        /// <summary>
        /// Represents the animation speed of the loading circle.
        /// </summary>
        /// <remarks>The value determines how quickly the loading circle animates.  A higher value results
        /// in faster animation, while a lower value slows it down.</remarks>
        public int LoadingCircleHot_AnimationSpeed { get; set; } = 10;

        /// <summary>Enable custom cursor shadow (rendered by WinPaletter not Windows)</summary>
        public bool Shadow_Enabled { get; set; } = false;

        /// <summary>Custom cursor shadow color</summary>
        public Color Shadow_Color { get; set; } = Color.Black;

        /// <summary>Custom cursor shadow blur intensity</summary>
        public int Shadow_Blur { get; set; } = 5;

        /// <summary>Custom cursor shadow opacity</summary>
        public float Shadow_Opacity { get; set; } = 0.3f;

        /// <summary>Custom cursor shadow x offset in coordinates (location)</summary>
        public int Shadow_OffsetX { get; set; } = 2;

        /// <summary>Custom cursor shadow y offset in coordinates (location)</summary>
        public int Shadow_OffsetY { get; set; } = 2;

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
            Program.Log?.Write(LogEventLevel.Information, $"Cursor to be loaded: `{subKey}`");

            UseFromFile = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "UseFromFile", false);
            File = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "File", string.Empty);

            ArrowStyle = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "ArrowStyle", Paths.ArrowStyle.Aero);
            CircleStyle = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "CircleStyle", Paths.CircleStyle.Aero);

            PrimaryColor1 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor1", Color.White);
            PrimaryColor2 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor2", Color.White);
            SecondaryColor1 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor1", subKey.ToLower() != "none" ? Color.Black : Color.Red);
            SecondaryColor2 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor2", subKey.ToLower() != "none" ? Color.FromArgb(64, 65, 75) : Color.Red);
            LoadingCircleBack1 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack1", Color.FromArgb(42, 151, 243));
            LoadingCircleBack2 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack2", Color.FromArgb(42, 151, 243));
            LoadingCircleHot1 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot1", Color.FromArgb(37, 204, 255));
            LoadingCircleHot2 = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot2", Color.FromArgb(37, 204, 255));

            PrimaryColorGradient = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradient", false);
            SecondaryColorGradient = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradient", true);
            LoadingCircleBackGradient = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradient", false);
            LoadingCircleHotGradient = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradient", false);

            PrimaryColorNoise = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoise", false);
            SecondaryColorNoise = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoise", false);
            LoadingCircleBackNoise = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoise", false);
            LoadingCircleHotNoise = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoise", false);

            PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradientMode", "circle"));
            SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradientMode", "vertical"));
            LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradientMode", "circle"));
            LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradientMode", "circle"));
            LoadingCircleHot_AnimationSpeed = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot_AnimationSpeed", 10);

            PrimaryColorNoiseOpacity = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoiseOpacity", 25f) / 100f;
            SecondaryColorNoiseOpacity = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoiseOpacity", 25f) / 100f;
            LoadingCircleBackNoiseOpacity = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoiseOpacity", 25f) / 100f;
            LoadingCircleHotNoiseOpacity = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoiseOpacity", 25f) / 100f;
            BorderThickness = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "BorderThickness", 1000f) / 1000f;

            Shadow_Enabled = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Enabled", false);
            Shadow_Color = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Color", Color.Black);
            Shadow_Blur = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Blur", 5);
            Shadow_Opacity = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Opacity", 30f) / 100f;
            Shadow_OffsetX = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetX", 2);
            Shadow_OffsetY = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetY", 2);
        }

        /// <summary>
        /// ApplyToTM data of WinPaletter themed cursors into registry
        /// </summary>
        /// <param name="subKey">Subkey of cursor inside registry key HKEY_CURRENT_USER\Software\WinPaletter\Cursors</param>
        /// <param name="Cursor">WinPaletter themed cursor structure</param>
        /// <param name="treeView">TreeView used for theme log</param>
        public static void Save_Cursors_To_Registry(string subKey, Cursor Cursor, TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Cursor to be saved: `{subKey}`");

            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "UseFromFile", Cursor.UseFromFile);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "File", Cursor.File, RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "ArrowStyle", (int)Cursor.ArrowStyle);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "CircleStyle", (int)Cursor.CircleStyle);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor1", Cursor.PrimaryColor1.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColor2", Cursor.PrimaryColor2.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradient", Cursor.PrimaryColorGradient ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorGradientMode", Paths.ReturnStringFromGradientMode(Cursor.PrimaryColorGradientMode), RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoise", Cursor.PrimaryColorNoise ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "PrimaryColorNoiseOpacity", Cursor.PrimaryColorNoiseOpacity * 100f);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor1", Cursor.SecondaryColor1.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColor2", Cursor.SecondaryColor2.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradient", Cursor.SecondaryColorGradient ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorGradientMode", Paths.ReturnStringFromGradientMode(Cursor.SecondaryColorGradientMode), RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoise", Cursor.SecondaryColorNoise ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "SecondaryColorNoiseOpacity", Cursor.SecondaryColorNoiseOpacity * 100f);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "BorderThickness", Cursor.BorderThickness * 1000f);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack1", Cursor.LoadingCircleBack1.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBack2", Cursor.LoadingCircleBack2.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradient", Cursor.LoadingCircleBackGradient ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackGradientMode", Paths.ReturnStringFromGradientMode(Cursor.LoadingCircleBackGradientMode), RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoise", Cursor.LoadingCircleBackNoise ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleBackNoiseOpacity", Cursor.LoadingCircleBackNoiseOpacity * 100f);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot1", Cursor.LoadingCircleHot1.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot2", Cursor.LoadingCircleHot2.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradient", Cursor.LoadingCircleHotGradient ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotGradientMode", Paths.ReturnStringFromGradientMode(Cursor.LoadingCircleHotGradientMode), RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoise", Cursor.LoadingCircleHotNoise ? 1 : 0);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHotNoiseOpacity", Cursor.LoadingCircleHotNoiseOpacity * 100f);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "LoadingCircleHot_AnimationSpeed", Cursor.LoadingCircleHot_AnimationSpeed);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Enabled", Cursor.Shadow_Enabled);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Color", Cursor.Shadow_Color.ToArgb());
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Blur", Cursor.Shadow_Blur);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_Opacity", (int)(Cursor.Shadow_Opacity * 100f));
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetX", Cursor.Shadow_OffsetX);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\{subKey}", "Shadow_OffsetY", Cursor.Shadow_OffsetY);
        }
    }
}
