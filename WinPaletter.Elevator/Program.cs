using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace WinPaletter.Elevator
{
    internal class Program
    {
        #region Variables
        static internal Program p = new Program();
        static System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();
        static bool exitSystem = false;

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler ConsoleHandler;
        static Process WP;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }
        #endregion

        #region Native methods
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        #endregion

        #region File watcher
        static Thread th;

        static public void Monitor(string WorkingDirectory)
        {
            if (!System.IO.Directory.Exists(WorkingDirectory))
                System.IO.Directory.CreateDirectory(WorkingDirectory);

            CreateFileWatcher(WorkingDirectory);
        }

        public static void CreateFileWatcher(string path)
        {
            watcher.Path = path;
            watcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
            watcher.Filter = "command";
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("All commands must be console that terminate by themselves and don't require user interaction");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Listening to change in '{path}\\command' to execute it elevated");

            Console.WriteLine();
            Console.ResetColor();
        }

        private static void OnChanged(object source, System.IO.FileSystemEventArgs e)
        {
            if (th != null && th.IsAlive)
            {
                Stopwatch sw = new Stopwatch();
                sw.Reset();
                sw.Start();
                while (th.IsAlive && sw.ElapsedMilliseconds < 20000) { }
                sw.Stop();
            }

            th = new Thread(() =>
            {
                IntPtr intPtr = IntPtr.Zero;
                Wow64DisableWow64FsRedirection(ref intPtr);

                if (!System.IO.File.Exists(e.FullPath))
                    return;

                string args = "";

                try
                {
                    args = System.IO.File.ReadAllText(e.FullPath);

                    if (!string.IsNullOrWhiteSpace(args))
                    {
                        try
                        {
                            if (!args.ToLower().StartsWith("echo ", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"> Executing ");
                                Console.ResetColor();
                                Console.Write($"{args}");
                                Console.WriteLine("");

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"> Output: ");
                                Console.ResetColor();
                                int result = Interaction.Shell(args, AppWinStyle.Hide, true);

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write($"> Completed with code: ");
                                Console.ResetColor();
                                Console.Write($"{result}");
                                Console.WriteLine("");
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine(DrawInConsoleBox(args.Remove(0, "echo ".Count())));
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"> Couldn't execute ");
                            Console.ResetColor();
                            Console.Write($"{args}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("");
                            Console.Write($"{ex.Message}\r\n{ex.StackTrace}");
                            Console.ResetColor();
                            Console.WriteLine("");
                        }
                    }

                    try { System.IO.File.Delete(e.FullPath); } catch { }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    DrawInConsoleBox($"{ex.Message}\r\n\r\n{ex.StackTrace}");
                    Console.ResetColor();

                    if (th != null && th.IsAlive)
                        th.Abort();
                }

                Wow64RevertWow64FsRedirection(IntPtr.Zero);
            });

            th.Priority = ThreadPriority.Highest;
            th.Start();
        }
        #endregion

        #region Console exit handler
        private static bool Handler(CtrlType sig)
        {
            Exit();

            //allow main to run off
            exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion

        #region Helpers
        public static string DrawInConsoleBox(string s)
        {
            string ulCorner = "╔";
            string llCorner = "╚";
            string urCorner = "╗";
            string lrCorner = "╝";
            string vertical = "║";
            string horizontal = "═";

            string[] lines = s.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


            int longest = 0;
            foreach (string line in lines)
            {
                if (line.Length > longest)
                    longest = line.Length;
            }
            int width = longest + 2; // 1 space on each side


            string h = string.Empty;
            for (int i = 0; i < width; i++)
                h += horizontal;

            // box top
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ulCorner + h + urCorner);

            // box contents
            foreach (string line in lines)
            {
                double dblSpaces = (((double)width - (double)line.Length) / (double)2);
                int iSpaces = Convert.ToInt32(dblSpaces);

                if (dblSpaces > iSpaces) // not an even amount of chars
                {
                    iSpaces += 1; // round up to next whole number
                }

                string beginSpacing = "";
                string endSpacing = "";
                for (int i = 0; i < iSpaces; i++)
                {
                    beginSpacing += " ";

                    if (!(iSpaces > dblSpaces && i == iSpaces - 1)) // if there is an extra space somewhere, it should be in the beginning
                    {
                        endSpacing += " ";
                    }
                }
                // add the text line to the box
                sb.AppendLine(vertical + beginSpacing + line + endSpacing + vertical);
            }

            // box bottom
            sb.AppendLine(llCorner + h + lrCorner);

            // the finished box
            return sb.ToString();
        }
        #endregion

        static void Main(string[] args)
        {
            ConsoleHandler += new EventHandler(Handler);
            SetConsoleCtrlHandler(ConsoleHandler, true);

            //start multi threaded console
            p.Start(args);

            //hold the console so it doesn't run off the end
            while (!exitSystem)
            {
                Thread.Sleep(250);
            }
        }

        public void Start(string[] args)
        {
            Process[] Processes = Process.GetProcessesByName("WinPaletter");
            if (Processes.Count() > 0)
            {
                WP = Processes[0];
                WP.Exited += WinPaletter_Exit;
            }

            if (args.Count() > 0 && args[0] != null && !watcher.EnableRaisingEvents)
                Monitor(args[0].Replace("\"", ""));

            Main(Console.ReadLine().Split(' '));
        }

        static void Exit()
        {
            watcher.EnableRaisingEvents = false;
            watcher.Changed -= OnChanged;
            watcher.Created -= OnChanged;
            watcher.Dispose();
            Wow64RevertWow64FsRedirection(IntPtr.Zero);
        }

        static void WinPaletter_Exit(object sender, EventArgs e)
        {
            Exit();
            if (WP != null) { WP.Exited -= WinPaletter_Exit; }
        }
    }
}
