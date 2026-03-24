using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Manages WinPaletter tasks in the Windows Task Scheduler.
    /// </summary>
    public static class Tasks
    {
        /// <summary>
        /// Types of task triggers.
        /// </summary>
        public enum TaskType
        {
            /// <summary>Fires when the system shuts down (Event 1074).</summary>
            Shutdown,
            /// <summary>Fires when the current user logs off (Event 4634 LogonType 2).</summary>
            Logoff,
            /// <summary>Fires when any user logs on interactively.</summary>
            Logon,
            /// <summary>Fires when the workstation is unlocked (Security Event 4624 LogonType 7).</summary>
            Unlock,
            /// <summary>Fires when a charger is connected (Kernel-Power Event 105).</summary>
            ChargerConnected,
            /// <summary>Fires when the system resumes from sleep or hibernate (Power-Troubleshooter Event 1).</summary>
            Resume,
        }

        // The task folder under Task Scheduler — all tasks are created inside this folder.
        private static readonly string TaskFolder = Application.ProductName;

        // Ensures every task name is rooted under TaskFolder.
        // All public methods pass their taskName through this before any schtasks call.
        private static string EnsureRooted(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return TaskFolder;

            if (taskName.Equals(TaskFolder, StringComparison.OrdinalIgnoreCase))
                return TaskFolder;

            if (taskName.StartsWith(TaskFolder + @"\", StringComparison.OrdinalIgnoreCase))
                return taskName;

            return $@"{TaskFolder}\{taskName}";
        }

        private static string BuildTriggerXml(TaskType type)
        {
            switch (type)
            {
                case TaskType.Logon:
                    return "<LogonTrigger><Enabled>true</Enabled></LogonTrigger>";

                case TaskType.Unlock:
                    return
                        "<EventTrigger>" +
                        "<Enabled>true</Enabled>" +
                        "<Subscription>" +
                        "&lt;QueryList&gt;" +
                        "&lt;Query Id='0' Path='Security'&gt;" +
                        "&lt;Select Path='Security'&gt;" +
                        "*[System[(EventID=4624)] and EventData[Data[@Name='LogonType']='7'] and EventData[Data[@Name='TargetUserSid']='" + User.SID + "']]" +
                        "&lt;/Select&gt;" +
                        "&lt;/Query&gt;" +
                        "&lt;/QueryList&gt;" +
                        "</Subscription>" +
                        "<Delay>PT3S</Delay>" +
                        "</EventTrigger>";

                case TaskType.Logoff:
                    return
                        "<EventTrigger>" +
                        "<Enabled>true</Enabled>" +
                        "<Subscription>" +
                        "&lt;QueryList&gt;" +
                        "&lt;Query Id='0' Path='Security'&gt;" +
                        "&lt;Select Path='Security'&gt;" +
                        "*[System[(EventID=4634)] and EventData[Data[@Name='LogonType']='2'] and EventData[Data[@Name='TargetUserSid']='" + User.SID + "']]" +
                        "&lt;/Select&gt;" +
                        "&lt;/Query&gt;" +
                        "&lt;/QueryList&gt;" +
                        "</Subscription>" +
                        "</EventTrigger>";

                case TaskType.Shutdown:
                    return
                        "<EventTrigger>" +
                        "<Enabled>true</Enabled>" +
                        "<Subscription>" +
                        "&lt;QueryList&gt;" +
                        "&lt;Query Id='0' Path='System'&gt;" +
                        "&lt;Select Path='System'&gt;" +
                        "*[System[Provider[@Name='User32'] and (EventID=1074)]]" +
                        "&lt;/Select&gt;" +
                        "&lt;/Query&gt;" +
                        "&lt;/QueryList&gt;" +
                        "</Subscription>" +
                        "</EventTrigger>";

                case TaskType.ChargerConnected:
                    return
                        "<EventTrigger>" +
                        "<Enabled>true</Enabled>" +
                        "<Subscription>" +
                        "&lt;QueryList&gt;" +
                        "&lt;Query Id='0' Path='System'&gt;" +
                        "&lt;Select Path='System'&gt;" +
                        "*[System[Provider[@Name='Microsoft-Windows-Kernel-Power'] and (EventID=105)]]" +
                        "&lt;/Select&gt;" +
                        "&lt;/Query&gt;" +
                        "&lt;/QueryList&gt;" +
                        "</Subscription>" +
                        "</EventTrigger>";

                case TaskType.Resume:
                    return
                        "<EventTrigger>" +
                        "<Enabled>true</Enabled>" +
                        "<Subscription>" +
                        "&lt;QueryList&gt;" +
                        "&lt;Query Id='0' Path='System'&gt;" +
                        "&lt;Select Path='System'&gt;" +
                        "*[System[Provider[@Name='Microsoft-Windows-Power-Troubleshooter'] and (EventID=1)]]" +
                        "&lt;/Select&gt;" +
                        "&lt;/Query&gt;" +
                        "&lt;/QueryList&gt;" +
                        "</Subscription>" +
                        "<Delay>PT3S</Delay>" +
                        "</EventTrigger>";

                default:
                    return "<LogonTrigger><Enabled>true</Enabled></LogonTrigger>";
            }
        }

        /// <summary>
        /// Creates a Task Scheduler task that runs WinPaletter on the given trigger.
        /// </summary>
        public static void Create(TaskType type, string taskName, string arguments = "", TreeView treeView = null)
        {
            taskName = EnsureRooted(taskName);

            if (Exists(taskName))
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Tasks.Create: '{taskName}' already exists, skipping creation.");
                return;
            }

            string exe = Application.ExecutablePath;
            string trigger = BuildTriggerXml(type);
            string userId = User.SID;

            string xml = $@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Task version=""1.4"" xmlns=""http://schemas.microsoft.com/windows/2004/02/mit/task"">
  <RegistrationInfo>
    <Description>WinPaletter — {taskName}</Description>
    <Author>{TaskFolder}</Author>
  </RegistrationInfo>
  <Triggers>
    {trigger}
  </Triggers>
  <Principals>
    <Principal id=""Author"">
      <UserId>{userId}</UserId>
      <LogonType>InteractiveToken</LogonType>
      <RunLevel>HighestAvailable</RunLevel>
    </Principal>
  </Principals>
  <Settings>
    <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
    <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
    <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
    <AllowHardTerminate>true</AllowHardTerminate>
    <StartWhenAvailable>true</StartWhenAvailable>
    <ExecutionTimeLimit>PT1M</ExecutionTimeLimit>
    <Priority>4</Priority>
    <Hidden>false</Hidden>
  </Settings>
  <Actions>
    <Exec>
      <Command>{exe}</Command>
      <Arguments>{arguments}</Arguments>
    </Exec>
  </Actions>
</Task>";

            string tempXml = Path.Combine(Path.GetTempPath(), $"WinPaletter_{type}.xml");

            try
            {
                File.WriteAllText(tempXml, xml, Encoding.Unicode);

                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.CreateTask, taskName), "task_add");

                Program.SendCommand($@"{SysPaths.SchTasks} /Create /TN ""{taskName}"" /XML ""{tempXml}"" /F");

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Tasks.Create: '{taskName}' trigger={type} args='{arguments}'");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Tasks.Create failed for '{taskName}': {ex.Message}");
            }
            finally
            {
                try { File.Delete(tempXml); } catch { }
            }
        }

        /// <summary>
        /// Deletes the task with the given name from the Task Scheduler.
        /// </summary>
        public static void Delete(string taskName, TreeView treeView = null)
        {
            taskName = EnsureRooted(taskName);

            try
            {
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.DeleteTask, taskName), "task_remove");

                Program.SendCommand($@"{SysPaths.SchTasks} /Delete /TN ""{taskName}"" /F");

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Tasks.Delete: '{taskName}'");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Tasks.Delete failed for '{taskName}': {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes every task under the WinPaletter folder in the Task Scheduler.
        /// </summary>
        public static void DeleteAll(TreeView treeView = null)
        {
            try
            {
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.DeleteTask, TaskFolder), "task_remove");

                Program.SendCommand($@"{SysPaths.SchTasks} /Delete /TN ""{TaskFolder}"" /F");

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Tasks.DeleteAll: Deleted all WinPaletter tasks.");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Tasks.DeleteAll failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns true if a task with the given name exists in the Task Scheduler.
        /// </summary>
        public static bool Exists(string taskName)
        {
            taskName = EnsureRooted(taskName);

            try
            {
                using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                {
                    process.StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = SysPaths.SchTasks,
                        Arguments = $@"/Query /TN ""{taskName}""",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                    };

                    process.Start();
                    process.WaitForExit();

                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Enables or disables an existing task by name without deleting it.
        /// </summary>
        public static void SetEnabled(string taskName, bool enabled, TreeView treeView = null)
        {
            taskName = EnsureRooted(taskName);
            string state = enabled ? "ENABLE" : "DISABLE";

            try
            {
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, $"{state}: {taskName}", enabled ? "task_add" : "task_remove");

                Program.SendCommand($@"{SysPaths.SchTasks} /Change /TN ""{taskName}"" /{state}");

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Tasks.SetEnabled: {state} '{taskName}'");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Tasks.SetEnabled failed for '{taskName}': {ex.Message}");
            }
        }

        /// <summary>
        /// Runs an existing task immediately by name without waiting for its trigger.
        /// </summary>
        public static void RunNow(string taskName, TreeView treeView = null)
        {
            taskName = EnsureRooted(taskName);

            try
            {
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, $"Run now: {taskName}", "task_add");

                Program.SendCommand($@"{SysPaths.SchTasks} /Run /TN ""{taskName}""");

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Tasks.RunNow: '{taskName}'");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Tasks.RunNow failed for '{taskName}': {ex.Message}");
            }
        }
    }
}