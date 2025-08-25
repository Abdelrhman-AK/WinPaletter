using CommandLine;
using CommandLine.Text;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Theme;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// Command line arguments options.
        /// </summary>
        public class Options
        {
            /// <summary>
            /// Export a JSON English language file.
            /// </summary>
            [Option('l', "exportlanguage", Required = false, HelpText = "Export a JSON English language file.")]
            public bool ExportLang { get; set; }

            /// <summary>
            /// Uninstall WinPaletter.
            /// </summary>
            [Option('u', "uninstall", Required = false, HelpText = "Uninstall WinPaletter.")]
            public bool Uninstall { get; set; }

            /// <summary>
            /// Uninstall WinPaletter without asking for confirmation.
            /// </summary>
            [Option('q', "uninstall-quiet", Required = false, HelpText = "Uninstall WinPaletter without asking for confirmation.")]
            public bool Uninstall_Quiet { get; set; }

            /// <summary>
            /// Run WinPaletter setup wizard.
            /// </summary>
            [Option('x', "Setup", Required = false, HelpText = "Run WinPaletter setup wizard.")]
            public bool Setup { get; set; }

            /// <summary>
            /// Apply a WinPaletter theme file.
            /// </summary>
            [Option('a', "apply", Required = false, HelpText = "Apply a WinPaletter theme file.")]
            public string Apply { get; set; }

            /// <summary>
            /// A switch to apply theme file silently. Requires -a "file" or --apply "file" before -s or --silent.
            /// </summary>
            [Option('s', "silent", Required = false, HelpText = "A switch to apply theme file silently. Requires -a \"file\" or --apply \"file\" before -s or --silent.")]
            public bool SilentApply { get; set; }

            /// <summary>
            /// Edit a WinPaletter theme file.
            /// </summary>
            [Option('e', "edit", Required = false, HelpText = "Edit a WinPaletter theme file.")]
            public string Edit { get; set; }

            /// <summary>
            /// Arguments help.
            /// </summary>
            [Option('?', "help", Required = false, HelpText = "Commands lines help.")]
            public bool Help { get; set; }

            /// <summary>
            /// Start SOS form to fix issues.
            /// </summary>
            [Option('f', "SOS", Required = false, HelpText = "Start SOS form to fix issues.")]
            public bool SOS { get; set; }

            /// <summary>
            /// Hide version.
            /// </summary>
            [Option('v', "version", Required = false, HelpText = "Show version.")]
            public bool Version { get; set; }

            /// <summary>
            /// Input file paths to be processed.
            /// </summary>
            [Value(0, MetaName = "inputFiles", HelpText = "Input file paths.", Default = new string[] { }, Hidden = true)]
            public IEnumerable<string> InputFiles { get; set; }

            /// <summary>
            /// Examples of how to use the command line arguments.
            /// </summary>
            [Usage(ApplicationAlias = "WinPaletter")]
            public static IEnumerable<Example> Examples
            {
                get
                {
                    return
                    [
                        new Example("Apply a theme from command line", new Options { Apply = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Theme.wpth" }),
                        new Example("Edit a theme from command line", new Options { Edit = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Theme.wpth" }),
                    ];
                }
            }
        }

        /// <summary>
        /// Check if the command line arguments can skip the user login.
        /// </summary>
        public static bool ArgsCanSkipUserLogin
        {
            get
            {
                IEnumerable<string> args = [.. Environment.GetCommandLineArgs().Skip(1)];

                bool value = false;

                Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(o =>
                  {
                      if (o.Help || o.Version || o.Uninstall || o.Uninstall_Quiet || o.SOS)
                      {
                          // Skip user login when help, version, uninstall, uninstall-quiet or SOS is requested. User login is then not needed.
                          value = true;
                      }
                      else if (o.Apply != null && File.Exists(o.Apply))
                      {
                          // Skip user login when applying a theme file. User login is then not needed and the theme will be applied to the current user.
                          value = true;
                      }
                      else if (o.Apply == null && o.Edit == null && o.InputFiles.Where(s => File.Exists(s) && Path.GetExtension(s).ToLower() == ".wpth").Count() > 0)
                      {
                          // Skip user login when opening a theme file. User login is then not needed and the theme will be opened for the current user.
                          value = true;
                      }
                  });

                return value;
            }
        }

        /// <summary>
        /// Execute the command line arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="canExit"></param>
        private static void ExecuteArgs(string[] args = null, bool canExit = true)
        {
            // Check if the command line arguments are null or empty to avoid unnecessary processing.
            // Count 1 is for the executable name.
            if (Environment.GetCommandLineArgs().Count() <= 1) return;

            // Check if the command line arguments can skip the user login.
            bool shouldExit = false;
            bool displayHelp = false;

            Program.Log?.Write(LogEventLevel.Information, $"Command line arguments: {string.Join(" ", args ?? Environment.GetCommandLineArgs().Skip(1))}");

            using (Parser parser = new(config => config.HelpWriter = null))
            {
                ParserResult<Options> result = parser.ParseArguments<Options>(args ?? [.. Environment.GetCommandLineArgs().Skip(1)]).WithParsed(o =>
                {
                    if (o.Help)
                    {
                        displayHelp = true;
                        shouldExit = true;
                        Log?.Write(LogEventLevel.Information, "Command line arguments help requested.");
                    }

                    if (o.Version)
                    {
                        MsgBox($"WinPaletter version: {Program.Version}");
                        shouldExit = true;
                        Log?.Write(LogEventLevel.Information, "Command line arguments version requested.");
                    }

                    if (o.ExportLang)
                    {
                        Lang.Save($"language-en {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second} {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.json");
                        MsgBox(Lang.Strings.Languages.Exported, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        shouldExit = true;
                        Log?.Write(LogEventLevel.Information, "Command line arguments export language requested.");
                    }

                    if (o.Setup)
                    {
                        Forms.Setup.ShowDialog();
                        shouldExit = false;
                        Log?.Write(LogEventLevel.Information, "Command line arguments setup requested.");
                    }

                    if (o.Uninstall)
                    {
                        Forms.Uninstall.ShowDialog();
                        shouldExit = true;
                        Log?.Write(LogEventLevel.Information, "Command line arguments uninstall requested.");
                    }

                    if (o.Uninstall_Quiet)
                    {
                        Uninstall_Quiet();
                        shouldExit = true;
                        Log?.Write(LogEventLevel.Information, "Command line arguments uninstall quiet requested.");
                    }

                    if (o.SOS)
                    {
                        Forms.SOS.ShowDialog();
                        shouldExit = true;
                        Log?.Write(LogEventLevel.Information, "Command line arguments SOS requested.");
                    }

                    if (o.Apply != null)
                    {
                        Log?.Write(LogEventLevel.Information, $"Command line arguments apply requested: {o.Apply}");
                        if (o.SilentApply) Log?.Write(LogEventLevel.Information, $"Command line arguments apply requested silently: {o.Apply}.");

                        if (File.Exists(o.Apply))
                        {
                            using (Manager TMx = new(Manager.Source.File, o.Apply, false, o.SilentApply))
                            {
                                Forms.Home.Text = Path.GetFileName(o.Apply);
                                Program.TM = TMx.Clone() as Manager;
                                TMx.Save(Manager.Source.Registry, string.Empty, null, false, o.SilentApply);
                                if (Settings.ThemeApplyingBehavior.AutoRestartExplorer) RestartExplorer();

                                ExternalLink = true;
                                ExternalLink_File = o.Apply;
                            }
                        }

                        shouldExit = true;
                    }

                    if (o.Edit != null && File.Exists(o.Edit))
                    {
                        Log?.Write(LogEventLevel.Information, $"Command line arguments edit requested: {o.Edit}");

                        TM = new(Manager.Source.File, o.Edit);
                        TM_Original = (Manager)TM.Clone();
                        Forms.Home.File = o.Edit;
                        Forms.Home.LoadFromTM(TM);
                        Forms.Home.Text = Path.GetFileName(o.Edit);

                        ExternalLink = true;
                        ExternalLink_File = o.Edit;
                        shouldExit = false;
                    }

                    IEnumerable<string> files = o.InputFiles.Where(s => File.Exists(s));

                    if (files.Count() > 0)
                    {
                        foreach (string file in files)
                        {
                            if (Path.GetExtension(file).ToLower() == ".wpth")
                            {
                                Log?.Write(LogEventLevel.Information, $"Command line arguments open requested: {file}");

                                if (Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt)
                                {
                                    Forms.Home.Text = Path.GetFileName(file);
                                    TM = new(Manager.Source.File, file, false);
                                    TM_Original = (Manager)TM.Clone();
                                    Forms.Home.File = file;
                                    Forms.Home.LoadFromTM(TM);

                                    ExternalLink = true;
                                    ExternalLink_File = file;

                                    shouldExit = false;
                                }
                                else
                                {
                                    Log?.Write(LogEventLevel.Information, $"Command line arguments apply requested: {file}");

                                    using (Manager TMx = new(Manager.Source.File, file, o.SilentApply))
                                    {
                                        TMx.Save(Manager.Source.Registry, string.Empty, null, false, o.SilentApply);
                                        if (Settings.ThemeApplyingBehavior.AutoRestartExplorer) RestartExplorer();
                                        Forms.Home.Text = Path.GetFileName(file);
                                        shouldExit = true;
                                    }
                                }
                            }

                            else if (Path.GetExtension(file).ToLower() == ".wpsf")
                            {
                                Log?.Write(LogEventLevel.Information, $"Command line arguments settings requested: {file}");

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

                    Log?.Write(LogEventLevel.Information, "Command line arguments help requested: {helpText}", helpText);

                    Forms.ArgsHelp.TextBox1.Text = helpText;
                    Forms.ArgsHelp.ShowDialog();
                }
            }

            // Exit the program if an argument requires it.

            if (canExit && shouldExit)
            {
                Log?.Write(LogEventLevel.Information, "Command line arguments exit requested (No GUI interaction is required for arguments).");
                Program.ForceExit();
            }
        }
    }
}