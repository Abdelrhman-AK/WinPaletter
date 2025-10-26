using Serilog.Events;
using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows XP appearance
    /// </summary>
    public class WindowsXP : ManagerBase<WindowsXP>
    {
        /// <summary> Controls if Windows XP themes editing is enabled or not </summary> 
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles { get; set; } = new();

        /// <summary>
        /// Creates WindowsXP data structure with default values
        /// </summary>
        public WindowsXP() { }

        /// <summary>
        /// Loads WindowsXP data from registry
        /// </summary>
        /// <param name="default">Default WindowsXP data structure</param>
        public void Load(WindowsXP @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows XP appearance preferences from registry and UxTheme.GetCurrentVS()");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsXP", string.Empty, @default.Enabled);

            VisualStyles.Load("XP", @default.VisualStyles);
        }

        /// <summary>
        /// Saves WindowsXP data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows XP appearance preferences into registry and by using UxTheme.EnableTheming and UxTheme.SetSystemVisualStyle");

            SaveToggleState(treeView);

            if (Enabled)
            {
                VisualStyles.Apply("XP", treeView);
            }
        }

        /// <summary>
        /// Saves WindowsXP toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsXP", string.Empty, Enabled);
        }
    }
}
