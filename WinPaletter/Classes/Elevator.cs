using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace WinPaletter
{
    public class Elevator
    {

        public readonly Process WPElevator = new()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/c {PathsExt.CMDElevator} {PathsExt.ElevatorDir}",
                WorkingDirectory = PathsExt.appData,
                UseShellExecute = true,
                Verb = "runas"
            }
        };

        public void Start()
        {
            List<Process> List = ProgramsRunning(PathsExt.CMDElevator);
            foreach (Process p in List)
                p.Kill();

            Thread.Sleep(100);

            try
            {
                if (System.IO.File.Exists(PathsExt.CMDElevator))
                {
                    System.IO.File.Delete(PathsExt.CMDElevator);
                }
                System.IO.File.WriteAllBytes(PathsExt.CMDElevator, Properties.Resources.WinPaletter_Elevator);
            }
            catch { }

            if (!System.IO.Directory.Exists($"{PathsExt.ElevatorDir}"))
                System.IO.Directory.CreateDirectory($"{PathsExt.ElevatorDir}");

            System.IO.File.WriteAllText($"{PathsExt.ElevatorDir}\\command", "");

            WPElevator.StartInfo.WindowStyle = Program.Settings.Services.ShowWPElevatorConsole ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;
            WPElevator.StartInfo.CreateNoWindow = !Program.Settings.Services.ShowWPElevatorConsole;
            WPElevator.StartInfo.Arguments = $"/c {PathsExt.CMDElevator} {PathsExt.ElevatorDir}";

            WPElevator.Start();

            Thread.Sleep(500);
        }

        public void Exit()
        {
            foreach (Process p in ProgramsRunning(PathsExt.CMDElevator))
                p.Kill();
        }

        public void SendCommand(string command, bool Wait = true, bool runas = true, bool IgnoreTimeout = false)
        {
            if (Program.Settings.Services.DontUseWPElevatorConsole || OS.WXP || Program.Elevated)
            {
                using (var process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = command.Split(' ')[0],
                        Verb = OS.WXP || !runas ? "" : "runas",
                        Arguments = command.Split(' ').Count() > 0 ? string.Join(" ", command.Split(' ').Skip(1)) : "",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = true
                    }
                })
                {
                    process.Start();

                    if (Wait)
                        process.WaitForExit();
                }
            }
            else
            {
                if (ProgramsRunning(PathsExt.CMDElevator).Count() == 0)
                {
                    Start();
                    Thread.Sleep(500);
                }

                if (!System.IO.Directory.Exists($"{PathsExt.ElevatorDir}"))
                    System.IO.Directory.CreateDirectory($"{PathsExt.ElevatorDir}");

                try
                {
                    System.IO.File.WriteAllText($"{PathsExt.ElevatorDir}\\command", command);

                    if (Wait)
                    {
                        Stopwatch sw = new();
                        if (!IgnoreTimeout)
                        {
                            sw.Reset();
                            sw.Start();
                        }
                        while (System.IO.File.Exists($"{PathsExt.ElevatorDir}\\command") && sw.ElapsedMilliseconds < 15000) { }
                        sw.Stop();
                    }
                }
                catch { }
            }
        }

        public static List<Process> ProgramsRunning(string FullPath)
        {
            System.Collections.Generic.List<Process> processes = new();
            string FileName = System.IO.Path.GetFileNameWithoutExtension(FullPath).ToLower();

            foreach (Process p in Process.GetProcessesByName(FileName))
            {
                if (FullPath.ToLower() == NativeMethods.Kernel32.GetProcessFilename(p).ToLower())
                    processes.Add(p);
            }

            return processes;
        }
    }
}
