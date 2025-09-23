using Serilog.Events;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Apply Command Prompt preferences
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply_CommandPrompt(TreeView treeView = null)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, "Applying Command Prompt preferences...");

            Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", string.Empty, "Terminal_CMD_Enabled", CommandPrompt, treeView);
            if (Program.Settings.ThemeApplyingBehavior.CMD_OverrideUserPreferences)
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_cmd.exe", "Terminal_CMD_Enabled", CommandPrompt, treeView);

            if (Program.Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            {
                Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", string.Empty, "Terminal_CMD_Enabled", CommandPrompt, treeView);
                Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_cmd.exe", "Terminal_CMD_Enabled", CommandPrompt, treeView);
            }
        }

        /// <summary>
        /// Apply PowerShell x86 preferences
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply_PowerShell86(TreeView treeView = null)
        {
            if (PowerShellx86.Enabled & Directory.Exists($@"{Environment.GetEnvironmentVariable("WINDIR")}\System32\WindowsPowerShell\v1.0"))
            {
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, "Applying PowerShell x86 preferences...");

                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "Terminal_PS_32_Enabled", PowerShellx86, treeView);
                if (Program.Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "Terminal_PS_32_Enabled", PowerShellx86, treeView);
                }
            }
        }

        /// <summary>
        /// Apply PowerShell x64 preferences
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply_PowerShell64(TreeView treeView = null)
        {
            if (PowerShellx64.Enabled & Directory.Exists($@"{Environment.GetEnvironmentVariable("WINDIR")}\SysWOW64\WindowsPowerShell\v1.0"))
            {
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, "Applying PowerShell x64 preferences...");

                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "Terminal_PS_64_Enabled", PowerShellx64, treeView);
                if (Program.Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "Terminal_PS_64_Enabled", PowerShellx64, treeView);
                }
            }
        }
    }
}