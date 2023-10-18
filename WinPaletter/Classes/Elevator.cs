using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace WinPaletter
{
    public class Elevator
    {
        private static string elevator_path = $"{Program.PATH_appData}\\Elevator";

        private readonly Process WPElevator = new()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/c {Program.PATH_WPElevator} {elevator_path}",
                WorkingDirectory = Program.PATH_appData,
                UseShellExecute = true,
                Verb = "runas"
            }
        };

        public void Start()
        {
            foreach (Process p in ProgramsRunning(Program.PATH_WPElevator))
                p.Kill();

            Thread.Sleep(100);

            try
            {
                if (System.IO.File.Exists(Program.PATH_WPElevator))
                {
                    System.IO.File.Delete(Program.PATH_WPElevator);
                }
                System.IO.File.WriteAllBytes(Program.PATH_WPElevator, Properties.Resources.WinPaletter_Elevator);
            }
            catch { }

            if (!System.IO.Directory.Exists($"{elevator_path}"))
                System.IO.Directory.CreateDirectory($"{elevator_path}");

            System.IO.File.WriteAllText($"{elevator_path}\\command", "");

            WPElevator.StartInfo.WindowStyle = Program.Settings.Miscellaneous.ShowWPElevatorConsole ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;
            WPElevator.StartInfo.CreateNoWindow = !Program.Settings.Miscellaneous.ShowWPElevatorConsole;
            WPElevator.StartInfo.Arguments = $"/c {Program.PATH_WPElevator} {elevator_path}";

            WPElevator.Start();
        }

        public void Exit()
        {
            foreach (Process p in ProgramsRunning(Program.PATH_WPElevator))
                p.Kill();
        }

        public void SendCommand(string command, bool DelayWait = true, bool runas = true)
        {
            if (Program.Settings.Miscellaneous.DontUseWPElevatorConsole || Program.WXP || Program.isElevated)
            {
                using (var process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = command.Split(' ')[0],
                        Verb = Program.WXP || !runas ? "" : "runas",
                        Arguments = command.Split(' ').Count() > 0 ? string.Join(" ", command.Split(' ').Skip(1)) : "",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = true
                    }
                })
                {
                    process.Start();

                    if (DelayWait)
                        process.WaitForExit();
                }
            }
            else
            {
                if (ProgramsRunning(Program.PATH_WPElevator).Count() == 0)
                {
                    Start();
                    Thread.Sleep(500);
                }

                if (!System.IO.Directory.Exists($"{elevator_path}"))
                    System.IO.Directory.CreateDirectory($"{elevator_path}");

                try
                { System.IO.File.WriteAllText($"{elevator_path}\\command", command); }
                catch { }

                if (DelayWait)
                    Thread.Sleep(200);
            }
        }

        public System.Collections.Generic.List<Process> ProgramsRunning(string FullPath)
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
