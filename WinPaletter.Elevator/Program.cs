using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace WinPaletter.Elevator
{
    internal class Program
    {
        static System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();

        static public void Monitor(string WorkingDirectory)
        {
            if (!System.IO.Directory.Exists(WorkingDirectory))
                System.IO.Directory.CreateDirectory(WorkingDirectory);

            CreateFileWatcher(WorkingDirectory);
        }

        public static void CreateFileWatcher(string path)
        {
            watcher.Path = path;
            /* Watch for changes in LastAccess and LastWrite times, and the renaming of files or directories. */
            watcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;

            // Only watch text files.
            watcher.Filter = "command";

            // Add event handlers.
            watcher.Changed += new System.IO.FileSystemEventHandler(OnChanged);
            watcher.Created += new System.IO.FileSystemEventHandler(OnChanged);
            watcher.Deleted += new System.IO.FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(DrawInConsoleBox("All commands must be console that terminate by themselves and don't require user interaction"));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"- Listening to change in '{path}\\command' to execute it elevated");

            Console.WriteLine();
            Console.ResetColor();
        }

        private static void OnChanged(object source, System.IO.FileSystemEventArgs e)
        {
            Thread th = new Thread(() =>
            {
                IntPtr intPtr = IntPtr.Zero;
                Wow64DisableWow64FsRedirection(ref intPtr);

                string args = System.IO.File.ReadAllText(e.FullPath);

                if (!string.IsNullOrWhiteSpace(args))
                {
                    try
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

                Wow64RevertWow64FsRedirection(IntPtr.Zero);
            });

            th.Priority = ThreadPriority.Highest;
            th.Start();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        static void Main(string[] args)
        {
            if (args.Count() > 0 && args[0] != null && !watcher.EnableRaisingEvents)
                Monitor(args[0].Replace("\"", ""));

            Main(Console.ReadLine().Split(' '));
        }
        
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
    }
}
