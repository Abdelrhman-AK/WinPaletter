﻿using Microsoft.Win32;
using System;

namespace WinPaletter
{
    public class ExplorerPatcher
    {
        public static bool IsInstalled = false;
        public bool UseStart10 = false;
        public bool UseTaskbar10 = false;
        public bool TaskbarButton10 = false;
        public StartStyles StartStyle;

        public enum StartStyles
        {
            NotRounded,
            RoundedCornersFloatingMenu,
            RoundedCornersDockedMenu
        }

        public ExplorerPatcher()
        {

            try
            {
                if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\ExplorerPatcher") is not null)
                {
                    IsInstalled = true;
                }
                else
                {
                    IsInstalled = false;
                }
            }
            catch
            { }
            finally
            {
                Microsoft.Win32.Registry.CurrentUser.Close();
            }

            if (!Program.Settings.ExplorerPatcher.Enabled_Force)
            {

                if (IsInstalled && (OS.W12 || OS.W11))
                {
                    UseStart10 = Convert.ToBoolean(Reg_IO.GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Start_ShowClassicMode", 0));
                    try
                    {
                        {
                            RegistryKey temp = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\ExplorerPatcher");
                            UseTaskbar10 = Convert.ToBoolean(temp.GetValue("OldTaskbar", true));
                            TaskbarButton10 = (int)temp.GetValue("OrbStyle", 0) == 0;
                            StartStyle = (StartStyles)Convert.ToInt32(temp.GetValue("StartUI_EnableRoundedCorners", StartStyles.NotRounded));
                        }
                    }
                    finally
                    {
                        Microsoft.Win32.Registry.CurrentUser.Close();
                    }
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

        public static bool IsAllowed()
        {
            bool condition0 = (OS.W12 || OS.W11) && Program.Settings.ExplorerPatcher.Enabled && IsInstalled;
            bool condition1 = Program.Settings.ExplorerPatcher.Enabled_Force;

            return condition0 || condition1;
        }
    }
}