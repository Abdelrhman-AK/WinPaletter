using System.Windows.Forms;

namespace WinPaletter
{
    public class Tasks
    {
        public enum TaskType
        {
            Shutdown,
            Logoff,
            Logon,
            Unlock,
            ChargerConnected
        }

        public static void Delete(TaskType TaskType, TreeView treeView = null)
        {
            switch (TaskType)
            {
                case TaskType.Shutdown:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_DeleteTask, @"WinPaletter\Shutdown"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Shutdown /F");
                        break;
                    }

                case TaskType.Logoff:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_DeleteTask, @"WinPaletter\Logoff"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Logoff /F");
                        break;
                    }

                case TaskType.Logon:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_DeleteTask, @"WinPaletter\Logon"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Logon /F");
                        break;
                    }

                case TaskType.Unlock:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_DeleteTask, @"WinPaletter\Unlock"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\Unlock /F");
                        break;
                    }

                case TaskType.ChargerConnected:
                    {
                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_DeleteTask, @"WinPaletter\ChargerConnected"), "task_remove");
                        Program.SendCommand(@$"{SysPaths.SchTasks} /Delete /TN WinPaletter\ChargerConnected /F");
                        break;
                    }
            }
        }
    }
}
