using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Class for managing tasks in the Task Scheduler.
    /// </summary>
    public class Tasks
    {
        /// <summary>
        /// Types of tasks.
        /// </summary>
        public enum TaskType
        {
            /// <summary>
            /// Shutdown task.
            /// </summary>
            Shutdown,
            /// <summary>
            /// Logoff task.
            /// </summary>
            Logoff,
            /// <summary>
            /// Logon task.
            /// </summary>
            Logon,
            /// <summary>
            /// Unlock task.
            /// </summary>
            Unlock,
            /// <summary>
            /// Charger connected task.
            /// </summary>
            ChargerConnected
        }

        /// <summary>
        /// Delete a task from the Task Scheduler.
        /// </summary>
        /// <param name="TaskType"></param>
        /// <param name="treeView"></param>
        public static void Delete(TaskType TaskType, TreeView treeView = null)
        {
            switch (TaskType)
            {
                case TaskType.Shutdown:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.DeleteTask, @"WinPaletter\Shutdown"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Shutdown /F");
                        break;
                    }

                case TaskType.Logoff:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.DeleteTask, @"WinPaletter\Logoff"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Logoff /F");
                        break;
                    }

                case TaskType.Logon:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.DeleteTask, @"WinPaletter\Logon"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Logon /F");
                        break;
                    }

                case TaskType.Unlock:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.DeleteTask, @"WinPaletter\Unlock"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Unlock /F");
                        break;
                    }

                case TaskType.ChargerConnected:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.DeleteTask, @"WinPaletter\ChargerConnected"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\ChargerConnected /F");
                        break;
                    }
            }
        }
    }
}
