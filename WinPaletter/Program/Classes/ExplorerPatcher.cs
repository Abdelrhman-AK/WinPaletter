using System;

namespace WinPaletter
{
    /// <summary>
    /// ExplorerPatcher helper class
    /// </summary>
    public class ExplorerPatcher
    {
        /// <summary>
        /// Is ExplorerPatcher installed
        /// </summary>
        public static bool IsInstalled = false;

        /// <summary>
        /// Use Windows 10 Start Menu style
        /// </summary>
        public bool UseStart10 = false;

        /// <summary>
        /// Use Windows 10 Taskbar style
        /// </summary>
        public bool UseTaskbar10 = false;

        /// <summary>
        /// Use Windows 10 start button style
        /// </summary>
        public bool TaskbarButton10 = false;

        /// <summary>
        /// Start menu style
        /// </summary>
        public StartStyles StartStyle;

        /// <summary>
        /// Start menu styles enumeration
        /// </summary>
        public enum StartStyles
        {
            /// <summary>
            /// No rounded corners (standard)
            /// </summary>
            NotRounded,
            /// <summary>
            /// Rounded corners and floating menu
            /// </summary>
            RoundedCornersFloatingMenu,

            /// <summary>
            /// Rounded corners and docked menu (not floating)
            /// </summary>
            RoundedCornersDockedMenu
        }

        /// <summary>
        /// Create a new instance of ExplorerPatcher helper class
        /// </summary>
        public ExplorerPatcher()
        {
            try
            {
                IsInstalled = RegKeyExists("HKEY_CURRENT_USER\\Software\\ExplorerPatcher");
            }
            catch
            {
                IsInstalled = false;
            }

            if (!Program.Settings.ExplorerPatcher.Enabled_Force)
            {
                if (IsInstalled && (OS.W12 || OS.W11))
                {
                    UseStart10 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Start_ShowClassicMode", 0));
                    try
                    {
                        UseTaskbar10 = Convert.ToBoolean(GetReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "OldTaskbar", true));
                        TaskbarButton10 = (int)GetReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "OrbStyle", 0) == 0;
                        StartStyle = (StartStyles)Convert.ToInt32(GetReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "StartUI_EnableRoundedCorners", StartStyles.NotRounded));
                    }
                    catch { }
                }
                else
                {
                    UseStart10 = false;
                    UseTaskbar10 = false;
                    TaskbarButton10 = false;
                    StartStyle = StartStyles.NotRounded;
                }
            }

            else
            {
                UseStart10 = Program.Settings.ExplorerPatcher.UseStart10;
                UseTaskbar10 = Program.Settings.ExplorerPatcher.UseTaskbar10;
                TaskbarButton10 = Program.Settings.ExplorerPatcher.TaskbarButton10;
                StartStyle = Program.Settings.ExplorerPatcher.StartStyle;
            }


        }

        /// <summary>
        /// Allow or disallow the use of the ExplorerPatcher helper class
        /// </summary>
        /// <returns></returns>
        public static bool IsAllowed()
        {
            bool condition0 = (OS.W12 || OS.W11) && Program.Settings.ExplorerPatcher.Enabled && IsInstalled;
            bool condition1 = Program.Settings.ExplorerPatcher.Enabled_Force;

            return condition0 || condition1;
        }
    }
}