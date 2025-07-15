using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows WXP appearance
    /// </summary>
    public class WindowsXP : ICloneable
    {
        /// <summary> Controls if Windows WXP themes editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles = new();

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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows XP appearance preferences from registry and UxTheme.GetCurrentVS()");

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsXP", string.Empty, @default.Enabled));

            VisualStyles.Load("XP", @default.VisualStyles);
        }

        /// <summary>
        /// Saves WindowsXP data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows XP appearance preferences into registry and by using UxTheme.EnableTheming and UxTheme.SetSystemVisualStyle");

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
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsXP", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two WindowsXP structures are equal</summary>
        public static bool operator ==(WindowsXP First, WindowsXP Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two WindowsXP structures are not equal</summary>
        public static bool operator !=(WindowsXP First, WindowsXP Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones WindowsXP structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two WindowsXP structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of WindowsXP structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
