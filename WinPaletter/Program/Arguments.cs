using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        public class Options
        {
            [Option('l', "exportlanguage", Required = false, HelpText = "Export a JSON English language file.")]
            public bool ExportLang { get; set; }

            [Option('u', "uninstall", Required = false, HelpText = "Uninstall WinPaletter.")]
            public bool Uninstall { get; set; }

            [Option('q', "uninstall-quiet", Required = false, HelpText = "Uninstall WinPaletter without asking for confirmation.")]
            public bool Uninstall_Quiet { get; set; }

            [Option('a', "apply", Required = false, HelpText = "Apply a WinPaletter theme file.")]
            public string Apply { get; set; }

            [Option('e', "edit", Required = false, HelpText = "Edit a WinPaletter theme file.")]
            public string Edit { get; set; }

            [Option('?', "help", Required = false, HelpText = "Commands lines help.")]
            public bool Help { get; set; }

            [Option('v', "version", Required = false, HelpText = "Show version.")]
            public bool Version { get; set; }

            [Value(0, MetaName = "inputFiles", HelpText = "Input file paths.", Default = new string[] { }, Hidden = true)]
            public IEnumerable<string> InputFiles { get; set; }

            [Usage(ApplicationAlias = "WinPaletter")]
            public static IEnumerable<Example> Examples
            {
                get
                {
                    return new List<Example>()
                    {
                        new Example("Apply a theme from command line", new Options { Apply = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Theme.wpth" }),
                        new Example("Edit a theme from command line", new Options { Edit = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Theme.wpth" }),
                    };
                }
            }
        }

        public static bool ArgsCanSkipUserLogin
        {
            get
            {
                IEnumerable<string> args = Environment.GetCommandLineArgs().Skip(1).ToArray();

                bool value = false;

                Parser.Default.ParseArguments<Options>(args)
                  .WithParsed<Options>(o =>
                  {
                      if (o.Help || o.Version || o.Uninstall || o.Uninstall_Quiet)
                      {
                          value = true;
                      }
                      else if (o.Apply != null && System.IO.File.Exists(o.Apply))
                      {
                          value = true;
                      }
                      else if (o.Apply == null && o.Edit == null && o.InputFiles.Where(s => System.IO.File.Exists(s) && System.IO.Path.GetExtension(s).ToLower() == ".wpth").Count() > 0)
                      {
                          value = true;
                      }
                  });

                return value;
            }
        }

        private static void ExecuteArgs(string[] args = null, bool canExit = true)
        {
            bool shouldExit = false;
            bool displayHelp = false;

            Parser parser = new Parser(config => config.HelpWriter = null);

            ParserResult<Options> result = parser.ParseArguments<Options>(args ?? Environment.GetCommandLineArgs().Skip(1).ToArray()).WithParsed<Options>(o =>
            {
                if (o.Help)
                {
                    displayHelp = true;
                    shouldExit = true;
                }

                if (o.Version)
                {
                    MsgBox($"WinPaletter version: {Program.Version}");
                    shouldExit = true;
                }

                if (o.ExportLang)
                {
                    Lang.ExportJSON($"language-en {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second} {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.json");
                    MsgBox(Lang.LngExported, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    shouldExit = true;
                }

                if (o.Uninstall)
                {
                    Forms.Uninstall.ShowDialog();
                    shouldExit = true;
                }

                if (o.Uninstall_Quiet)
                {
                    Uninstall_Quiet();
                    shouldExit = true;
                }

                if (o.Apply != null && System.IO.File.Exists(o.Apply))
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, o.Apply))
                    {
                        TMx.Save(Theme.Manager.Source.Registry);
                        if (Settings.ThemeApplyingBehavior.AutoRestartExplorer) RestartExplorer();
                        shouldExit = true;
                    }
                }

                if (o.Edit != null && System.IO.File.Exists(o.Edit))
                {
                    TM = new(Theme.Manager.Source.File, o.Edit);
                    TM_Original = (Theme.Manager)TM.Clone();
                    Forms.Home.OpenFileDialog1.FileName = o.Edit;
                    Forms.Home.SaveFileDialog1.FileName = o.Edit;
                    Forms.Home.LoadFromTM(TM);
                    shouldExit = false;
                }

                IEnumerable<string> files = o.InputFiles.Where(s => System.IO.File.Exists(s));

                if (files.Count() > 0)
                {
                    foreach (string file in files)
                    {
                        if (System.IO.Path.GetExtension(file).ToLower() == ".wpth")
                        {
                            if (Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt)
                            {
                                ExternalLink = true;
                                ExternalLink_File = file;
                                shouldExit = false;
                            }
                            else
                            {
                                using (Theme.Manager TMx = new(Theme.Manager.Source.File, file))
                                {
                                    TMx.Save(Theme.Manager.Source.Registry);
                                    if (Settings.ThemeApplyingBehavior.AutoRestartExplorer) RestartExplorer();
                                    shouldExit = true;
                                }
                            }
                        }

                        else if (System.IO.Path.GetExtension(file).ToLower() == ".wpsf")
                        {
                            Forms.SettingsX._External = true;
                            Forms.SettingsX._File = file;
                            Forms.SettingsX.ShowDialog();
                            shouldExit = true;
                        }
                    }
                }
            });

            if (displayHelp)
            {
                HelpText helpText = HelpText.AutoBuild(result, h =>
                {
                    h.AdditionalNewLineAfterOption = false;
                    h.AddDashesToOption = true;
                    h.MaximumDisplayWidth = 1000;
                    h.Heading = $"{Application.ProductName} {Program.Version}";
                    h.AddPreOptionsLine(" ");
                    return HelpText.DefaultParsingErrorsHandler(parser.ParseArguments<Options>(args ?? Environment.GetCommandLineArgs().Skip(1).ToArray()), h);
                }, e => e);

                Forms.ArgsHelp.TextBox1.Text = helpText;
                Forms.ArgsHelp.ShowDialog();
            }

            if (canExit && shouldExit) Program.ForceExit();
        }
    }
}