using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        private static void ExecuteArgs()
        {
            foreach (string arg in Environment.GetCommandLineArgs().Skip(1))
            {
                if (arg.ToLower() == "/exportlanguage")
                {
                    Lang.ExportJSON($"language-en {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second} {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.json");
                    MsgBox(Lang.LngExported, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    using Process Prc = Process.GetCurrentProcess();
                    Prc.Kill();
                    break;
                }

                else if (arg.ToLower() == "/uninstall")
                {
                    Forms.Uninstall.ShowDialog();
                    using Process Prc = Process.GetCurrentProcess();
                    Prc.Kill();
                    break;
                }

                else if (arg.ToLower() == "/uninstall-quiet")
                {
                    Uninstall_Quiet();
                    break;
                }

                else if (arg.StartsWith("/convert:", StringComparison.OrdinalIgnoreCase))
                {
                    CMD_Convert(arg, true);
                }

                else if (arg.StartsWith("/convert-list:", StringComparison.OrdinalIgnoreCase))
                {
                    CMD_Convert_List(arg, true);

                }

                else if (!arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/convert:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/convert-list:", StringComparison.OrdinalIgnoreCase))
                {
                    if (System.IO.Path.GetExtension(arg).ToLower() == ".wpth")
                    {
                        if (Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt)
                        {
                            ExternalLink = true;
                            ExternalLink_File = arg;
                        }
                        else
                        {
                            Theme.Manager TMx = new(Theme.Manager.Source.File, arg);
                            TMx.Save(Theme.Manager.Source.Registry, arg);
                            if (Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                                RestartExplorer();
                            using Process Prc = Process.GetCurrentProcess();
                            Prc.Kill();
                        }
                    }

                    if (System.IO.Path.GetExtension(arg).ToLower() == ".wpsf")
                    {
                        Forms.SettingsX._External = true;
                        Forms.SettingsX._File = arg;
                        Forms.SettingsX.ShowDialog();
                        using Process Prc = Process.GetCurrentProcess();
                        Prc.Kill();
                    }
                }

                else if (arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase))
                {
                    string File = arg.Remove(0, "/apply:".Count());
                    File = File.Replace("\"", string.Empty);
                    if (System.IO.File.Exists(File))
                    {
                        Theme.Manager TMx = new(Theme.Manager.Source.File, File);
                        TMx.Save(Theme.Manager.Source.Registry);
                        if (Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                            RestartExplorer();
                        using Process Prc = Process.GetCurrentProcess();
                        Prc.Kill();
                    }
                }

                else if (arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase))
                {
                    string File = arg.Remove(0, "/edit:".Count());
                    File = File.Replace("\"", string.Empty);
                    ExternalLink = true;
                    ExternalLink_File = File;
                }
            }
        }

        private static void ExecuteArgs_ProgramStarted(string[] args)
        {
            if (args.Count() > 0)
            {
                foreach (string arg in args)
                {
                    try
                    {
                        if ((arg.ToLower() ?? string.Empty) == ("/exportlanguage".ToLower() ?? string.Empty))
                        {
                            MsgBox(Lang.LngShouldClose, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else if (arg.ToLower() == "/uninstall")
                        {
                            Forms.Uninstall.ShowDialog();
                        }

                        else if (arg.ToLower() == "/uninstall-quiet")
                        {
                            Uninstall_Quiet();
                        }

                        else if (arg.StartsWith("/convert:", StringComparison.OrdinalIgnoreCase))
                        {
                            CMD_Convert(arg, false);
                        }

                        else if (arg.StartsWith("/convert-list:", StringComparison.OrdinalIgnoreCase))
                        {
                            CMD_Convert_List(arg, false);
                        }

                        else if (!arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase))
                        {
                            if (System.IO.Path.GetExtension(arg).ToLower() == ".wpth")
                            {
                                Forms.ComplexSave.GetResponse(Forms.MainFrm.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

                                TM = new(Theme.Manager.Source.File, arg);
                                TM_Original = (Theme.Manager)TM.Clone();
                                Forms.MainFrm.OpenFileDialog1.FileName = arg;
                                Forms.MainFrm.SaveFileDialog1.FileName = arg;
                                Forms.MainFrm.LoadFromTM(TM);
                                Forms.MainFrm.ApplyColorsToElements(TM);

                                if (!Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt)
                                {
                                    Forms.ThemeLog.Apply_Theme();
                                }
                            }

                            if (System.IO.Path.GetExtension(arg).ToLower() == ".wpsf")
                            {
                                Forms.SettingsX._External = true;
                                Forms.SettingsX._File = arg;
                                Forms.SettingsX.ShowDialog();
                            }
                        }

                        else
                        {
                            if (arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase))
                            {
                                string File = arg.Remove(0, "/apply:".Count());
                                File = File.Replace("\"", string.Empty);
                                if (System.IO.File.Exists(File))
                                {
                                    Theme.Manager TMx = new(Theme.Manager.Source.File, File);
                                    TMx.Save(Theme.Manager.Source.Registry);
                                    if (Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                                        RestartExplorer();
                                }
                            }

                            if (arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase))
                            {
                                string File = arg.Remove(0, "/edit:".Count());
                                File = File.Replace("\"", string.Empty);

                                Forms.ComplexSave.GetResponse(Forms.MainFrm.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

                                TM = new(Theme.Manager.Source.File, File);
                                TM_Original = (Theme.Manager)TM.Clone();
                                Forms.MainFrm.OpenFileDialog1.FileName = File;
                                Forms.MainFrm.SaveFileDialog1.FileName = File;
                                Forms.MainFrm.LoadFromTM(TM);
                                Forms.MainFrm.ApplyColorsToElements(TM);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Forms.BugReport.ThrowError(ex);
                        BringApplicationToFront();
                    }
                }
            }
            else
            {
                BringApplicationToFront();
            }
        }
    }
}