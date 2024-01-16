using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        public static bool ArgsCanSkipUserLogin
        {
            get
            {
                IEnumerable<string> args = Environment.GetCommandLineArgs().Skip(1);

                foreach (string arg in args)
                {
                    if (arg.ToLower() == "/exportlanguage")
                    {
                        return true;
                    }

                    else if (arg.StartsWith("/convert:", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    else if (arg.StartsWith("/convert-list:", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    else if (!arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/convert:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/convert-list:", StringComparison.OrdinalIgnoreCase))
                    {
                        if (System.IO.Path.GetExtension(arg).ToLower() == ".wpth")
                        {
                            return true;
                        }
                    }

                    else if (arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private static void ExecuteArgs()
        {
            foreach (string arg in Environment.GetCommandLineArgs().Skip(1))
            {
                if (arg.ToLower() == "/exportlanguage")
                {
                    Lang.ExportJSON($"language-en {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second} {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.json");
                    MsgBox(Lang.LngExported, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.ForceExit();
                    break;
                }

                else if (arg.ToLower() == "/uninstall")
                {
                    Forms.Uninstall.ShowDialog();
                    Program.ForceExit();
                    break;
                }

                else if (arg.ToLower() == "/uninstall-quiet")
                {
                    Uninstall_Quiet();
                    break;
                }

                else if (!arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase)
                      && !arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase)
                      && !arg.StartsWith("/convert:", StringComparison.OrdinalIgnoreCase)
                      && !arg.StartsWith("/convert-list:", StringComparison.OrdinalIgnoreCase))
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
                            using (Theme.Manager TMx = new(Theme.Manager.Source.File, arg))
                            {
                                TMx.Save(Theme.Manager.Source.Registry);
                                if (Settings.ThemeApplyingBehavior.AutoRestartExplorer) RestartExplorer();
                            }

                            Program.ForceExit();
                        }
                    }

                    else if (System.IO.Path.GetExtension(arg).ToLower() == ".wpsf")
                    {
                        Forms.SettingsX._External = true;
                        Forms.SettingsX._File = arg;
                        Forms.SettingsX.ShowDialog();
                        Program.ForceExit();
                    }
                }

                else if (arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase))
                {
                    string File = arg.Remove(0, "/apply:".Count()).Replace("\"", string.Empty);

                    if (System.IO.File.Exists(File))
                    {
                        using (Theme.Manager TMx = new(Theme.Manager.Source.File, File))
                        {
                            TMx.Save(Theme.Manager.Source.Registry);
                            if (Settings.ThemeApplyingBehavior.AutoRestartExplorer) RestartExplorer();
                        }
                    }

                    Program.ForceExit();
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

                        else if (!arg.StartsWith("/apply:", StringComparison.OrdinalIgnoreCase) & !arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase))
                        {
                            if (System.IO.Path.GetExtension(arg).ToLower() == ".wpth")
                            {
                                Forms.ComplexSave.GetResponse(Forms.Dashboard.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

                                using (Theme.Manager TMx = new(Theme.Manager.Source.File, arg))
                                {
                                    Forms.Dashboard.OpenFileDialog1.FileName = arg;
                                    Forms.Dashboard.SaveFileDialog1.FileName = arg;

                                    Forms.Dashboard.LoadFromTM(TMx);

                                    if (!Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt)
                                    {
                                        Forms.ThemeLog.Apply_Theme(TMx);
                                    }
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
                                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, File)) { Forms.ThemeLog.Apply_Theme(TMx); }
                                }
                            }

                            if (arg.StartsWith("/edit:", StringComparison.OrdinalIgnoreCase))
                            {
                                string File = arg.Remove(0, "/edit:".Count());
                                File = File.Replace("\"", string.Empty);

                                Forms.ComplexSave.GetResponse(Forms.Dashboard.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

                                TM = new(Theme.Manager.Source.File, File);
                                TM_Original = (Theme.Manager)TM.Clone();
                                Forms.Dashboard.OpenFileDialog1.FileName = File;
                                Forms.Dashboard.SaveFileDialog1.FileName = File;
                                Forms.Dashboard.LoadFromTM(TM);
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