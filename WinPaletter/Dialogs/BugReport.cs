using FluentTransitions;
using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Runtime;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Properties;
using WinPaletter.Theme;

namespace WinPaletter
{

    public partial class BugReport
    {
        public BugReport()
        {
            InitializeComponent();
        }

        bool collapsed = true;
        int previousHeight = 540;
        private bool isThrowing = false;
        private Exception exception;

        private int CollapsedHeight
        {
            get
            {
                int upperPaddingDifference = separatorH1.Bottom - AnimatedBox1.Bottom;

                return separatorH1.Bottom + upperPaddingDifference + bottom_buttons.Height + 40;
            }
        }

        private void Center()
        {
            Location = new(Location.X - 35, Location.Y - 35);

            Task.Delay(10).ContinueWith(_ =>
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    Rectangle area = Screen.FromControl(this).WorkingArea;
                    int targetX = (area.Width / 2) - (Width / 2);
                    int targetY = (area.Height / 2) - (Height / 2);

                    Transition
                        .With(this, nameof(Left), targetX)
                        .With(this, nameof(Top), targetY)
                        .Spring(TimeSpan.FromSeconds(0.75));
                }));
            });
        }

        private void BugReport_Load(object sender, EventArgs e)
        {
            this.Localize();
            ApplyStyle(this);

            Color c = PictureBox1.Image.AverageColor().CB(Program.Style.DarkMode ? -0.35f : 0.35f);
            AnimatedBox1.BackColor = c;

            TreeView1.Font = Fonts.ConsoleMedium;

            Forms.GlassWindow.Show();

            foreach (Label lbl in AnimatedBox1.Controls.OfType<Label>()) lbl.ForeColor = Color.White;

            CustomSystemSounds.Exclamation.Play();

            Height = CollapsedHeight;

            Rectangle area = Screen.FromControl(this).WorkingArea;
            int targetX = (area.Width / 2) - (Width / 2);
            int targetY = (area.Height / 2) - (Height / 2);

            Location = new Point(targetX, targetY);

            BringToFront();

            Center();
        }

        public void AddData(string str, Exception ex, TreeView treeView)
        {
            if (ex is not null && ex.Data is not null && ex.Data.Keys is not null && ex.Data.Keys.Count > 0)
            {
                TreeNodeCollection temp = treeView?.Nodes?.Add($"{str} data").Nodes;
                foreach (DictionaryEntry x in ex.Data)
                {
                    if (x.Value is Exception nestedEx)
                    {
                        temp?.Add($"{x.Key} = Exception:");
                        AddException($"Data Exception ({x.Key})", nestedEx, treeView);
                    }
                    else
                    {
                        temp?.Add($"{x.Key} = {x.Value}");
                    }
                }
            }
        }

        public void AddException(string str, Exception ex, TreeView treeView, int win32Error = 0)
        {
            if (ex is null) return;

            // Message
            treeView?.Nodes?.Add($"{str} message").Nodes?.Add(ex?.Message ?? "No message included");

            // Exception type
            treeView?.Nodes?.Add("Exception type").Nodes?.Add(ex?.GetType().ToString());

            // Win32 error
            if (win32Error != 0)
                treeView?.Nodes?.Add($"{str} Marshal.GetLastWin32Error").Nodes?.Add(win32Error.ToString());

            // Stack trace
            if (!string.IsNullOrWhiteSpace(ex?.StackTrace))
            {
                TreeNode stackNode = treeView?.Nodes?.Add($"{str} stack trace");
                foreach (string line in ex.StackTrace.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
                    stackNode?.Nodes?.Add(line.Trim());
            }

            // Exception Data
            AddData(str, ex, treeView);

            // Target site / method info
            if (ex.TargetSite is not null)
            {
                TreeNode targetNode = treeView.Nodes?.Add($"{str} target method");
                targetNode?.Nodes?.Add($@"Target Site: {ex.Source}.{ex.TargetSite.Name}()");
                targetNode?.Nodes?.Add($"Member Type: {ex.TargetSite.MemberType}");
                if (ex.TargetSite.DeclaringType is not null)
                    targetNode?.Nodes?.Add($"Declaring Type: {ex.TargetSite.DeclaringType.FullName}");

                var parameters = ex.TargetSite.GetParameters();
                if (parameters.Length > 0)
                {
                    TreeNode paramsNode = targetNode?.Nodes?.Add("Parameters");
                    foreach (var param in parameters)
                        paramsNode?.Nodes?.Add($"{param.ParameterType.Name} {param.Name}");
                }
            }

            // Assembly information
            if (ex.TargetSite?.Module?.Assembly is not null)
            {
                treeView.Nodes?.Add($"{str} assembly").Nodes?.Add(ex.TargetSite.Module.Assembly.FullName);
                if (!string.IsNullOrWhiteSpace(ex.TargetSite.Module.Assembly.Location))
                    treeView.Nodes?.Add($"{str} assembly's file").Nodes?.Add(ex.TargetSite.Module.Assembly.Location);
            }

            // Source property
            if (!string.IsNullOrWhiteSpace(ex.Source))
                treeView.Nodes?.Add($"{str} source").Nodes?.Add(ex.Source);

            // HRESULT
            treeView.Nodes?.Add($"{str} HRESULT").Nodes?.Add(ex.HResult.ToString());

            // Help link
            if (!string.IsNullOrWhiteSpace(ex.HelpLink))
                treeView.Nodes?.Add($"{str} Microsoft help link").Nodes?.Add(ex.HelpLink);

            // Timestamp
            treeView.Nodes?.Add($"{str} timestamp").Nodes?.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            // Inner exception (recursive)
            if (ex.InnerException is not null)
            {
                TreeNode innerNode = treeView.Nodes?.Add($"{str} inner exception");
                AddException("Inner Exception", ex.InnerException, treeView);
            }

            // ReflectionTypeLoadException LoaderExceptions
            if (ex is ReflectionTypeLoadException rtlEx && rtlEx.LoaderExceptions?.Length > 0)
            {
                TreeNode loaderNode = treeView.Nodes?.Add($"{str} loader exceptions");
                foreach (var le in rtlEx.LoaderExceptions)
                    loaderNode?.Nodes?.Add(le?.Message ?? "[No message]");
            }

            // AggregateException inner exceptions
            if (ex is AggregateException aggEx && aggEx.InnerExceptions?.Count > 0)
            {
                TreeNode aggNode = treeView.Nodes?.Add($"{str} aggregate inner exceptions");
                foreach (var ie in aggEx.InnerExceptions)
                    AddException("Aggregate Inner", ie, treeView);
            }

            // Common derived exception properties
            switch (ex)
            {
                case FileNotFoundException fnf:
                    treeView.Nodes?.Add($"{str} file name").Nodes?.Add(fnf.FileName ?? "[Unknown]");
                    treeView.Nodes?.Add($"{str} fusion log").Nodes?.Add(fnf.FusionLog ?? "[None]");
                    break;
                case Win32Exception w32:
                    treeView.Nodes?.Add($"{str} native error code").Nodes?.Add(w32.NativeErrorCode.ToString());
                    break;
                case IOException ioEx when ioEx.HResult != 0:
                    treeView.Nodes?.Add($"{str} HResult detail").Nodes?.Add(ioEx.HResult.ToString());
                    break;
            }
        }

        public static IEnumerable<Exception> GetAllInnerExceptions(Exception exception)
        {
            List<Exception> exceptions = [];

            while (exception != null)
            {
                exceptions.Add(exception);
                exception = exception.InnerException;
            }

            return exceptions;
        }

        [DebuggerHidden]
        public void Throw(Exception ex, bool noRecovery = false, int win32Error = -1)
        {
            if (ex == null) return;
            exception = ex;

            if (InvokeRequired)
            {
                // Use BeginInvoke to avoid blocking background threads
                Invoke(new Action(() => SafeThrowInner(ex, noRecovery, win32Error)));
            }
            else
            {
                SafeThrowInner(ex, noRecovery, win32Error);
            }
        }

        private void SafeThrowInner(Exception ex, bool noRecovery, int win32Error)
        {
            if (isThrowing) return; // prevent reentry
            isThrowing = true;

            try
            {
                ThrowInner(ex, noRecovery, win32Error);
            }
            finally
            {
                isThrowing = false;
            }
        }

        private void ThrowInner(Exception ex, bool noRecovery, int win32Error = -1)
        {
            // Try is used to avoid loop in case of error in the backup function
            try
            {
                // If theme backup option is enabled, backup it before throwing error
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnExError && Program.TM is not null)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnExceptionError", $"{Program.TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    Program.TM.Save(Manager.Source.File, filename);
                }
            }
            catch { }

            if (win32Error == -1) { win32Error = Marshal.GetLastWin32Error(); }

            Label7.Text = ex.GetType().FullName + (!string.IsNullOrWhiteSpace(ex?.Message) ? $": {ex?.Message}" : string.Empty);

            AlertBox1.Visible = noRecovery;
            TreeView1.Nodes?.Clear();

            TreeNode n = TreeView1?.Nodes.Add("Information");
            n?.Nodes.Add("OS").Nodes.Add($"{OS.Name_English}, {OS.Build}, {OS.Architecture_English}");

#if DEBUG
            n?.Nodes.Add("WinPaletter version").Nodes.Add($"{Program.Version}{(Program.IsBeta ? $", {Program.Localization.Strings.General.Beta}" : string.Empty)}, Build: Debug");
#else
            n?.Nodes.Add("WinPaletter version").Nodes.Add($"{Program.Version}{(Program.IsBeta ? $", {Program.Localization.Strings.General.Beta}" : string.Empty)}, Build: Release");
#endif

            n?.Nodes.Add("WinPaletter language").Nodes.Add(Program.Localization.Information.Lang);
            n?.Nodes.Add("Debugger is attached?").Nodes.Add(Debugger.IsAttached ? Program.Localization.Strings.General.Yes : Program.Localization.Strings.General.No);

            if (ex is not null)
            {
                AddException("Exception", ex, TreeView1);

                if (ex?.GetBaseException() is not null && ex?.GetBaseException() != ex)
                {
                    Exception baseEx = ex.GetBaseException();
                    AddException("Base exception", baseEx, TreeView1, win32Error);

                    IEnumerable<Exception> exceptions = GetAllInnerExceptions(baseEx);
                    if (exceptions.Count() > 0)
                    {
                        int i = 0;
                        foreach (Exception sub_ex in exceptions)
                        {
                            AddException($"Base exception: sub inner exception {i}", sub_ex.InnerException, TreeView1, win32Error);
                            i++;
                        }
                    }
                }

                if (ex?.InnerException is not null)
                {
                    AddException("Inner exception", ex.InnerException, TreeView1, win32Error);

                    IEnumerable<Exception> exceptions = GetAllInnerExceptions(ex.InnerException);
                    if (exceptions.Count() > 0)
                    {
                        int i = 0;
                        foreach (Exception sub_ex in exceptions)
                        {
                            AddException($"Sub inner exception {i}", sub_ex.InnerException, TreeView1, win32Error);
                            i++;
                        }
                    }
                }
            }

            if (win32Error != 0)
            {
                Win32Exception win32Exception = new(win32Error);
                if (win32Exception != null) { AddException("Win32 exception", win32Exception, TreeView1, win32Error); }
            }
            // Loaded Assemblies (limited to top 20 to avoid overwhelming)
            TreeNode assembliesNode = TreeView1.Nodes?.Add($"Loaded assemblies");
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var assemblyNode = assembliesNode?.Nodes?.Add(assembly.GetName().Name);
                    assemblyNode?.Nodes?.Add($"Version: {assembly.GetName().Version}");
                    assemblyNode?.Nodes?.Add($"Location: {assembly.Location ?? "[Dynamic]"}");
                    assemblyNode?.Nodes?.Add($"Is Dynamic: {assembly.IsDynamic}");
                    assemblyNode?.Nodes?.Add($"Is Fully Trusted: {assembly.IsFullyTrusted}");

                    // Get referenced assemblies (first 3)
                    var referenced = assembly.GetReferencedAssemblies().Take(3);
                    if (referenced.Any())
                    {
                        var refNode = assemblyNode?.Nodes?.Add("Referenced Assemblies (first 3)");
                        foreach (var refAssembly in referenced)
                        {
                            refNode?.Nodes?.Add($"{refAssembly.Name} v{refAssembly.Version}");
                        }
                    }
                }
                catch
                {
                    // Ignore assemblies that can't be inspected
                    assembliesNode?.Nodes?.Add($"[Unable to inspect: {assembly.FullName?.Split(',')[0] ?? "Unknown"}]");
                }
            }
            assembliesNode?.Nodes?.Add($"Total Assemblies Loaded: {AppDomain.CurrentDomain.GetAssemblies().Length}");

            // Process information
            TreeNode processNode = TreeView1.Nodes?.Add($"Process info");
            using (Process process = Process.GetCurrentProcess())
            {
                processNode?.Nodes?.Add($"Process ID: {process.Id}");
                processNode?.Nodes?.Add($"Process Name: {process.ProcessName}");
                processNode?.Nodes?.Add($"Memory Usage: {process.WorkingSet64 / 1024 / 1024} MB");
                processNode?.Nodes?.Add($"Peak Memory: {process.PeakWorkingSet64 / 1024 / 1024} MB");
                processNode?.Nodes?.Add($"Total Processor Time: {process.TotalProcessorTime}");
                processNode?.Nodes?.Add($"User Processor Time: {process.UserProcessorTime}");
                processNode?.Nodes?.Add($"Virtual Memory Size: {process.VirtualMemorySize64 / 1024 / 1024} MB");
                processNode?.Nodes?.Add($"Private Memory Size: {process.PrivateMemorySize64 / 1024 / 1024} MB");
                processNode?.Nodes?.Add($"Handle Count: {process.HandleCount}");
                processNode?.Nodes?.Add($"Thread Count: {process.Threads.Count}");
            }

            // Thread information
            TreeNode threadNode = TreeView1.Nodes?.Add($"Thread info");
            threadNode?.Nodes?.Add($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            threadNode?.Nodes?.Add($"Thread Name: {Thread.CurrentThread.Name ?? "[Unnamed]"}");
            threadNode?.Nodes?.Add($"Thread State: {Thread.CurrentThread.ThreadState}");
            threadNode?.Nodes?.Add($"Is ThreadPool Thread: {Thread.CurrentThread.IsThreadPoolThread}");
            threadNode?.Nodes?.Add($"Is Background Thread: {Thread.CurrentThread.IsBackground}");
            threadNode?.Nodes?.Add($"Priority: {Thread.CurrentThread.Priority}");
            threadNode?.Nodes?.Add($"Apartment State: {Thread.CurrentThread.GetApartmentState()}");

            // Runtime/CLR information
            TreeNode runtimeNode = TreeView1.Nodes?.Add($"Runtime info");
            runtimeNode?.Nodes?.Add($"CLR Version: {Environment.Version}");
            runtimeNode?.Nodes?.Add($"Runtime Directory: {RuntimeEnvironment.GetRuntimeDirectory()}");
            runtimeNode?.Nodes?.Add($"Total Memory (GC): {GC.GetTotalMemory(false) / 1024 / 1024} MB");
            runtimeNode?.Nodes?.Add($"GC Collection Count (Gen 0): {GC.CollectionCount(0)}");
            runtimeNode?.Nodes?.Add($"GC Collection Count (Gen 1): {GC.CollectionCount(1)}");
            runtimeNode?.Nodes?.Add($"GC Collection Count (Gen 2): {GC.CollectionCount(2)}");
            runtimeNode?.Nodes?.Add($"Max Generation: {GC.MaxGeneration}");
            runtimeNode?.Nodes?.Add($"GC Latency Mode: {GCSettings.LatencyMode}");
            runtimeNode?.Nodes?.Add($"GC Server Mode: {GCSettings.IsServerGC}");

            // Application Domain information
            TreeNode appDomainNode = TreeView1.Nodes?.Add($"Application domain");
            appDomainNode?.Nodes?.Add($"Name: {AppDomain.CurrentDomain.FriendlyName}");
            appDomainNode?.Nodes?.Add($"Base Directory: {AppDomain.CurrentDomain.BaseDirectory}");

            // Environment information
            TreeNode envNode = TreeView1.Nodes?.Add($"Environment");
            envNode?.Nodes?.Add($"OS Version: {Environment.OSVersion}");
            envNode?.Nodes?.Add($"Processor Count: {Environment.ProcessorCount}");
            envNode?.Nodes?.Add($"64-bit OS: {Environment.Is64BitOperatingSystem}");
            envNode?.Nodes?.Add($"64-bit Process: {Environment.Is64BitProcess}");
            envNode?.Nodes?.Add($"System Page Size: {Environment.SystemPageSize}");

            // Culture and regional information
            TreeNode cultureNode = TreeView1.Nodes?.Add($"Culture info");
            cultureNode?.Nodes?.Add($"Current Culture: {Thread.CurrentThread.CurrentCulture.Name}");
            cultureNode?.Nodes?.Add($"Current UI Culture: {Thread.CurrentThread.CurrentUICulture.Name}");
            cultureNode?.Nodes?.Add($"Time Zone: {TimeZoneInfo.Local.StandardName}");

            // Windows-specific information
            try
            {
                TreeNode windowsNode = TreeView1.Nodes?.Add($"More Windows info");

                // Windows version via WMI (if available)
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    windowsNode?.Nodes?.Add($"Product Name: {ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "Unknown")}");
                    windowsNode?.Nodes?.Add($"Current Build: {ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", "Unknown")}");
                    windowsNode?.Nodes?.Add($"Current Build Number: {ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", "Unknown")}");
                    windowsNode?.Nodes?.Add($"Release ID: {ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "Unknown")}");
                    windowsNode?.Nodes?.Add($"Display Version: {ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", "Unknown")}");
                }
            }
            catch { }

            TreeView1.ExpandAll();

            n?.Collapse();
            assembliesNode?.Collapse();

            TreeView1.SelectedNode = TreeView1.Nodes[0];

            if (!Directory.Exists($@"{SysPaths.appData}\Reports")) Directory.CreateDirectory($@"{SysPaths.appData}\Reports");

            string exLogPath = $@"{SysPaths.appData}\Reports\{DateTime.Now:HHmmss_ddMMyy}.txt";

            File.WriteAllText(exLogPath, GetDetails());

            Program.Log?.Write(LogEventLevel.Error, $"{ex}:\r\n{GetDetails().Trim()}");

            Program.Log?.Write(LogEventLevel.Information, $"Exception error full details text file is saved as '{exLogPath}'");

            button10.Visible = Debugger.IsAttached;

            ShowDialog();

            Forms.GlassWindow.Close();

            if (DialogResult == DialogResult.Abort) Program.ExitAfterException = true; else Program.ExitAfterException = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                DialogResult = DialogResult.Abort;
                Close();
                Program.ForceExit();
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                DialogResult = DialogResult.OK;
                Close();
            });
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                Forms.GlassWindow.Close();
                Process.Start(Links.Issues);
            });
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                Clipboard.SetText(GetDetails());
            });
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(dlg.FileName, TreeView1.ToJSON());
                    }
                }
            });
        }

        public string GetDetails()
        {
            StringBuilder SB = new();
            SB.Clear();
            SB.AppendLine("```JSON");
            SB.AppendLine(TreeView1.ToJSON());
            SB.AppendLine("```");
            return SB.ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                if (Directory.Exists($@"{SysPaths.appData}\Reports"))
                {
                    Forms.GlassWindow.Close();
                    Process.Start($@"{SysPaths.appData}\Reports");
                }
                else
                {
                    MsgBox(string.Format(Program.Localization.Strings.Messages.Bug_NoReport, $@"{SysPaths.appData}\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode is not null) Clipboard.SetText(TreeView1?.SelectedNode?.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                Forms.SOS.ShowDialog();
            });
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            var btn = sender as UI.WP.Button;
            if (btn == null) return;

            if (collapsed)
            {
                collapsed = false;
                GroupBox3.Visible = false;
                btn.ImageGlyph = Resources.Glyph_Up;

                // Disable manual resizing when expanded
                FormBorderStyle = FormBorderStyle.Sizable;

                // Animate Height and Top
                Transition
                    .With(this, nameof(Height), previousHeight)
                    .With(this, nameof(Top), Top - (previousHeight - Height) / 2)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                Program.Animator.HideSync(label2);

                // Wait for 1/6 of animation before showing GroupBox3
                await Task.Delay(Program.AnimationDuration / 6);
                Program.Animator.Show(GroupBox3);
            }
            else
            {
                collapsed = true;
                previousHeight = Height;
                btn.ImageGlyph = Resources.Glyph_Down;

                // Disable manual resizing when info is collapsed
                FormBorderStyle = FormBorderStyle.FixedSingle;

                Program.Animator.Hide(GroupBox3);
                Program.Animator.Show(label2);

                // Animate back to collapsed height
                Transition
                    .With(this, nameof(Height), CollapsedHeight)
                    .With(this, nameof(Top), Top + (Height - CollapsedHeight) / 2)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Forms.MainForm.BeginInvoke(() =>
            {
                using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = string.IsNullOrWhiteSpace(Forms.Home.File) ? Program.TM.Info.ThemeName + ".wpth" : Forms.Home.File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Forms.Home.File = dlg.FileNames[0];
                        Program.TM.Save(Manager.Source.File, Forms.Home.File);
                        Forms.Home.Text = Path.GetFileName(Forms.Home.File);
                        Forms.Home.LoadFromTM(Program.TM);
                    }
                }
            });
        }

        [DebuggerHidden]
        private void button10_Click(object sender, EventArgs e)
        {
            if (exception is not null && Debugger.IsAttached) ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}