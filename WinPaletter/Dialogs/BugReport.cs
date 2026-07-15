using FluentTransitions;
using Serilog.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
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

        // Cross-thread dialog coordination:
        // Multiple background threads can call Throw() at nearly the same time. Without this, each call would create its own BugReport instance and try to ShowDialog() independently,
        // which either (a) runs on the wrong thread because a brand-new Form's InvokeRequired is unreliable before its handle exists, or (b) opens a second modal dialog
        // nested on top of the first. Both symptoms look like "clicking buttons does nothing".
        private static readonly object _dialogLock = new();
        private static bool _isDialogShowing = false;
        private static readonly Queue<(Exception Ex, bool NoRecovery, int Win32Error)> _pendingQueue = new();
        private static readonly Dictionary<string, DateTime> _fingerprintLastShown = [];
        private static readonly TimeSpan _dedupeWindow = TimeSpan.FromSeconds(3);

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
            {
                TreeNode win32Node = treeView?.Nodes?.Add($"{str} Marshal.GetLastWin32Error");
                win32Node?.Nodes?.Add(win32Error.ToString());
                win32Node?.Nodes?.Add($"Resolved message: {NativeMethods.Kernel32.GetErrorMessage(win32Error)}");
            }

            // HRESULT-based lookup (covers COM/HRESULT style codes that Win32Exception alone misses)
            if (ex.HResult != 0)
            {
                treeView?.Nodes?.Add($"{str} HRESULT resolved message").Nodes?.Add(NativeMethods.Kernel32.GetErrorMessage(ex.HResult));
            }

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

                ParameterInfo[] parameters = ex.TargetSite.GetParameters();
                if (parameters.Length > 0)
                {
                    TreeNode paramsNode = targetNode?.Nodes?.Add("Parameters");
                    foreach (ParameterInfo param in parameters)
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
                foreach (Exception le in rtlEx.LoaderExceptions)
                    loaderNode?.Nodes?.Add(le?.Message ?? "[No message]");
            }

            // AggregateException inner exceptions
            if (ex is AggregateException aggEx && aggEx.InnerExceptions?.Count > 0)
            {
                TreeNode aggNode = treeView.Nodes?.Add($"{str} aggregate inner exceptions");
                foreach (Exception ie in aggEx.InnerExceptions)
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
                    treeView.Nodes?.Add($"{str} native error resolved message").Nodes?.Add(NativeMethods.Kernel32.GetErrorMessage(w32.NativeErrorCode));
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

        /// <summary>
        /// Builds a short, stable signature for an exception so repeated identical errors (e.g. from a tight retry loop) don't spawn a fresh modal dialog every single time.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string ComputeFingerprint(Exception ex)
        {
            string topFrame = string.Empty;

            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                topFrame = ex.StackTrace.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
            }

            return $"{ex.GetType().FullName}|{ex.TargetSite?.DeclaringType?.FullName}.{ex.TargetSite?.Name}|{topFrame}";
        }

        [DebuggerHidden]
        public void Throw(Exception ex, bool noRecovery = false, int win32Error = -1)
        {
            if (ex == null) return;

            exception = ex;

            Control uiControl = Forms.MainForm;

            // IMPORTANT: the previous implementation checked `this.InvokeRequired`, but `this` is a brand-new Form whose window handle has not been created yet.
            // InvokeRequired on a control without a handle does not reliably detect the thread mismatch, so ShowDialog()
            // could end up running on the throwing background thread instead of the UI thread. A WinForms dialog shown on a thread other than the one running
            // Application.Run() gets its own disconnected message pump, which is what made the whole app (including button clicks) feel unresponsive.
            // Marshalling through Forms.MainForm, which is guaranteed to own the real UI thread once the app has started, fixes that.
            if (uiControl is not null && uiControl.IsHandleCreated)
            {
                if (uiControl.InvokeRequired)
                {
                    uiControl.BeginInvoke(new Action(() => HandleThrow(ex, noRecovery, win32Error)));
                }
                else
                {
                    HandleThrow(ex, noRecovery, win32Error);
                }
            }
            else
            {
                // MainForm isn't ready yet (a very early startup crash). There is no safe UI thread to marshal to, so fall back to showing on the calling thread and log that this
                // happened, since it's a strong signal something is wrong upstream.
                Program.Log?.Write(LogEventLevel.Warning, "BugReport.Throw was called before Forms.MainForm was ready; showing on the calling thread.");
                HandleThrow(ex, noRecovery, win32Error);
            }
        }

        // Runs on the UI thread. Decides whether to show the dialog now, queue it behind one that's
        // already showing, or suppress it as a duplicate.
        private void HandleThrow(Exception ex, bool noRecovery, int win32Error)
        {
            string fingerprint = ComputeFingerprint(ex);

            lock (_dialogLock)
            {
                if (_fingerprintLastShown.TryGetValue(fingerprint, out DateTime lastShown) && DateTime.UtcNow - lastShown < _dedupeWindow)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"Duplicate exception suppressed within dedupe window: {fingerprint}");
                    return;
                }

                _fingerprintLastShown[fingerprint] = DateTime.UtcNow;

                if (_isDialogShowing)
                {
                    // Never call ShowDialog() a second time while one is already modal - that opens a nested modal dialog on top of the first, which is what previously made the
                    // first dialog's buttons appear frozen. Queue it instead.
                    _pendingQueue.Enqueue((ex, noRecovery, win32Error));
                    return;
                }

                _isDialogShowing = true;
            }

            SafeThrowInner(ex, noRecovery, win32Error);
        }

        private void SafeThrowInner(Exception ex, bool noRecovery, int win32Error)
        {
            if (isThrowing) return; // prevent reentry on this instance
            isThrowing = true;

            try
            {
                ThrowInner(ex, noRecovery, win32Error);
            }
            finally
            {
                isThrowing = false;

                (Exception Ex, bool NoRecovery, int Win32Error)? next = null;

                lock (_dialogLock)
                {
                    _isDialogShowing = false;

                    if (_pendingQueue.Count > 0)
                    {
                        next = _pendingQueue.Dequeue();
                        _isDialogShowing = true;
                    }
                }

                if (next is not null)
                {
                    // Show the next queued exception with a fresh dialog instance, after this one
                    // has fully closed.
                    BugReport nextReport = new();
                    nextReport.SafeThrowInner(next.Value.Ex, next.Value.NoRecovery, next.Value.Win32Error);
                }
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
                    string filename = Program.GetUniqueFileName(SysPaths.ThemesBackup_OnExceptionError, $"{Program.TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
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

            // .NET Framework release key (more precise than Environment.Version, e.g. distinguishes 4.8 vs 4.8.1)
            try
            {
                object release = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", "Release", null);
                if (release is int releaseKey)
                {
                    runtimeNode?.Nodes?.Add($".NET Framework release key: {releaseKey}");
                }
            }
            catch { }

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
            envNode?.Nodes?.Add($"Machine Name: {Environment.MachineName}");
            envNode?.Nodes?.Add($"Command Line: {Environment.CommandLine}");
            envNode?.Nodes?.Add($"Current Directory: {Environment.CurrentDirectory}");

            // Culture and regional information
            TreeNode cultureNode = TreeView1.Nodes?.Add($"Culture info");
            cultureNode?.Nodes?.Add($"Current Culture: {Thread.CurrentThread.CurrentCulture.Name}");
            cultureNode?.Nodes?.Add($"Current UI Culture: {Thread.CurrentThread.CurrentUICulture.Name}");
            cultureNode?.Nodes?.Add($"Time Zone: {TimeZoneInfo.Local.StandardName}");

            // Display / DPI information - useful for layout and scaling related bugs
            TreeNode displayNode = TreeView1.Nodes?.Add("Display info");
            displayNode?.Nodes?.Add($"DPI: {DeviceDpi} ({DeviceDpi / 96.0:P0} scaling)");
            int screenIndex = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                TreeNode screenNode = displayNode?.Nodes?.Add($"Screen {screenIndex}{(screen.Primary ? " (primary)" : string.Empty)}");
                screenNode?.Nodes?.Add($"Bounds: {screen.Bounds}");
                screenNode?.Nodes?.Add($"Working area: {screen.WorkingArea}");
                screenNode?.Nodes?.Add($"Bits per pixel: {screen.BitsPerPixel}");
                screenIndex++;
            }

            // Network availability - helps rule in/out connectivity issues for network-related errors
            TreeNode networkNode = TreeView1.Nodes?.Add("Network info");
            networkNode?.Nodes?.Add($"Network available: {NetworkInterface.GetIsNetworkAvailable()}");

            // Loaded native modules - helps spot conflicting shell extensions / overlays (e.g. RTSS, antivirus hooks)
            try
            {
                TreeNode modulesNode = TreeView1.Nodes?.Add("Loaded native modules (first 20)");
                using Process process = Process.GetCurrentProcess();
                int count = 0;
                foreach (ProcessModule module in process.Modules)
                {
                    if (count >= 20) break;
                    modulesNode?.Nodes?.Add($"{module.ModuleName} v{module.FileVersionInfo.FileVersion ?? "unknown"}");
                    count++;
                    module.Dispose();
                }
            }
            catch { }

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

        // NOTE ON THE BUTTON HANDLERS BELOW:
        // The previous version wrapped every handler's body in `Forms.MainForm.BeginInvoke(...)`.
        // That was unnecessary and part of the unresponsiveness problem: by the time this dialog is
        // visible and a user can click a button, we are already executing on the correct UI thread
        // (Throw()/HandleThrow() above guarantee that). Re-marshalling through BeginInvoke queued the
        // actual click logic as a *second*, asynchronous hop and silently swallowed any exception
        // thrown inside it (fire-and-forget), which could make a button appear to do nothing at all.
        // The handlers now run their logic directly and report failures instead of swallowing them.

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Abort;
                Close();
                Program.ForceExit();
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"BugReport Button2_Click (abort) failed: {ex}");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"BugReport Button1_Click (continue) failed: {ex}");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.GlassWindow.Close();
                Process.Start(Links.Issues);
            }
            catch (Exception ex)
            {
                // Opening a browser can fail (no default browser configured, sandboxed environment,
                // etc.). Falling back to the clipboard means the user can still get to the link.
                Program.Log?.Write(LogEventLevel.Error, $"BugReport Button5_Click (open issues) failed: {ex}");
                try { Clipboard.SetText(Links.Issues); } catch { }
                MsgBox($"Couldn't open the browser automatically. The issues page link was copied to your clipboard instead.\n\n{Links.Issues}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(GetDetails());
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"BugReport Button3_Click (copy details) failed: {ex}");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, TreeView1.ToJSON());
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"BugReport Button4_Click (save JSON) failed: {ex}");
                MsgBox($"Couldn't save the report file:\n{ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
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
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"BugReport Button6_Click (open reports folder) failed: {ex}");
            }
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode is not null) Clipboard.SetText(TreeView1?.SelectedNode?.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.SOS.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"BugReport button7_Click (SOS) failed: {ex}");
            }
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
                    .CriticalDamp(Program.AnimationSpan);

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
                    .CriticalDamp(Program.AnimationSpan);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = string.IsNullOrWhiteSpace(Forms.Home.File) ? Program.TM.Info.ThemeName + ".wpth" : Forms.Home.File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Forms.Home.File = dlg.FileNames[0];
                    Program.TM.Save(Manager.Source.File, Forms.Home.File);
                    Forms.Home.Text = Path.GetFileName(Forms.Home.File);
                    Forms.Home.LoadFromTM(Program.TM);
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"BugReport button9_Click (save theme) failed: {ex}");
                MsgBox($"Couldn't save the theme file:\n{ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DebuggerHidden]
        private void button10_Click(object sender, EventArgs e)
        {
            if (exception is not null && Debugger.IsAttached) ExceptionDispatchInfo.Capture(exception).Throw();
        }

        /// <summary>
        /// Manual test harness for BugReport. Wire these up to hidden debug buttons, a debug menu, or call them one at a time from the Immediate Window while the app is running.
        /// <br></br>Each method is meant to be run in isolation so the resulting dialog(s) can be inspected on their own.
        /// </summary>
        public static class Tests
        {
            /// <summary>
            /// Baseline case: a plain exception thrown and caught on the UI thread. Confirms the dialog shows normally, the treeview populates, and none of the queueing logic gets involved.
            /// </summary>
            public static void Test_SimpleExceptionOnUIThread()
            {
                try
                {
                    throw new InvalidOperationException("Test_SimpleExceptionOnUIThread: manual test exception.");
                }
                catch (Exception ex)
                {
                    new BugReport().Throw(ex);
                }
            }

            /// <summary>
            /// Confirms <c>Throw()</c> correctly marshals to Forms.MainForm when called from a background thread, instead of showing the dialog on the wrong thread.
            /// This is the scenario that used to leave the dialog's buttons unresponsive.
            /// </summary>
            public static void Test_ExceptionFromBackgroundThread()
            {
                Thread worker = new(() =>
                {
                    try
                    {
                        throw new InvalidOperationException("Test_ExceptionFromBackgroundThread: thrown from a non-UI thread.");
                    }
                    catch (Exception ex)
                    {
                        new BugReport().Throw(ex);
                    }
                })
                {
                    IsBackground = true,
                    Name = "BugReportTests.BackgroundThread"
                };

                worker.Start();
            }

            /// <summary>
            /// Fires several distinct exceptions from several threads at (roughly) the same instant. Expect exactly one modal dialog visible at a time, with 
            /// the rest appearing one after another as each prior dialog is closed - never two dialogs stacked/nested at once.
            /// </summary>
            /// <param name="threadCount"></param>
            public static void Test_ConcurrentExceptionsAreQueued(int threadCount = 4)
            {
                for (int i = 0; i < threadCount; i++)
                {
                    int index = i;

                    Thread worker = new(() =>
                    {
                        try
                        {
                            throw new InvalidOperationException($"Test_ConcurrentExceptionsAreQueued: exception #{index} from thread {Thread.CurrentThread.ManagedThreadId}.");
                        }
                        catch (Exception ex)
                        {
                            new BugReport().Throw(ex);
                        }
                    })
                    {
                        IsBackground = true,
                        Name = $"BugReportTests.ConcurrentThread{index}"
                    };

                    worker.Start();
                }
            }

            /// <summary>
            /// Throws the exact same exception signature repeatedly within a short window. Only the first call should produce a visible dialog; the rest should be
            /// logged as suppressed duplicates (check the log output, not a second dialog appearing).
            /// </summary>
            /// <param name="repeatCount"></param>
            public static void Test_DuplicateExceptionIsDeduped(int repeatCount = 5)
            {
                for (int i = 0; i < repeatCount; i++)
                {
                    try
                    {
                        throw new InvalidOperationException("Test_DuplicateExceptionIsDeduped: repeated exception.");
                    }
                    catch (Exception ex)
                    {
                        new BugReport().Throw(ex);
                    }
                }
            }

            /// <summary>
            /// Exercises the Win32Exception branch and the SystemErrorLookup node next to it.
            /// <br><c>2 = ERROR_FILE_NOT_FOUND</c>, a code the base system message table resolves on its own.</br>
            /// </summary>
            public static void Test_Win32ErrorCode()
            {
                const int errorFileNotFound = 2;

                try
                {
                    throw new Win32Exception(errorFileNotFound, "Test_Win32ErrorCode: simulated file-not-found.");
                }
                catch (Exception ex)
                {
                    new BugReport().Throw(ex, false, errorFileNotFound);
                }
            }

            /// <summary>
            /// Exercises the HRESULT lookup path with a CAPI-style error code, the kind that Win32Exception.Message alone would not resolve but the crypt32.dll module walk should.
            /// <br><c>CRYPT_E_NOT_FOUND = 0x80092004</c>.</br>
            /// </summary>
            public static void Test_HResultFromCryptoApi()
            {
                const int cryptENotFound = unchecked((int)0x80092004);

                InvalidOperationException ex = new("Test_HResultFromCryptoApi: simulated CAPI failure.");

                typeof(Exception)
                    .GetField("_HResult", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(ex, cryptENotFound);

                new BugReport().Throw(ex);
            }

            /// <summary>
            /// Confirms the recursive InnerException walk (<c>AddException</c> calling itself) renders a multi-level chain correctly.
            /// </summary>
            public static void Test_NestedInnerExceptions()
            {
                Exception innermost = new ArgumentNullException("someParameter", "Test_NestedInnerExceptions: innermost cause.");
                Exception middle = new InvalidOperationException("Test_NestedInnerExceptions: middle layer.", innermost);
                Exception outer = new ApplicationException("Test_NestedInnerExceptions: outer layer.", middle);

                new BugReport().Throw(outer);
            }

            /// <summary>
            /// Exercises the AggregateException branch, e.g. what you'd see from a faulted <c>Task.WhenAll</c>.
            /// </summary>
            /// <exception cref="InvalidOperationException"></exception>
            /// <exception cref="TimeoutException"></exception>
            public static void Test_AggregateException()
            {
                try
                {
                    Task first = Task.Run(() => throw new InvalidOperationException("Test_AggregateException: first faulted task."));
                    Task second = Task.Run(() => throw new TimeoutException("Test_AggregateException: second faulted task."));

                    Task.WaitAll(first, second);
                }
                catch (AggregateException ex)
                {
                    new BugReport().Throw(ex);
                }
            }

            /// <summary>
            /// Exercises <c>AddData()</c>: populates Exception.Data with a mix of simple values and a nested exception, and confirms both render as separate nodes.
            /// </summary>
            public static void Test_ExceptionWithData()
            {
                Exception nested = new InvalidOperationException("Test_ExceptionWithData: nested exception stored in Data.");

                Exception ex = new InvalidOperationException("Test_ExceptionWithData: exception carrying extra data.");
                ex.Data["UserAction"] = "Clicked Apply";
                ex.Data["ThemeName"] = "Test Theme";
                ex.Data["NestedException"] = nested;

                new BugReport().Throw(ex);
            }

            /// <summary>
            /// Bypasses BugReport entirely and calls <c>SystemErrorLookup</c> directly, so the certutil-style module-walking logic can be checked in isolation without opening the full dialog.
            /// <br>Prints the resolved message for a base system code and a CAPI-specific code.</br>
            /// </summary>
            public static void Test_SystemErrorLookupDirect()
            {
                const int errorFileNotFound = 2;
                const int cryptENotFound = unchecked((int)0x80092004);

                string baseSystemResult = NativeMethods.Kernel32.GetErrorMessage(errorFileNotFound);
                string capiResult = NativeMethods.Kernel32.GetErrorMessage(cryptENotFound);
                string unknownResult = NativeMethods.Kernel32.GetErrorMessage(unchecked((int)0xFFFFFFF0));

                string message = "Base system code (2):\n" + baseSystemResult +
                                  "\n\nCAPI code (0x80092004):\n" + capiResult +
                                  "\n\nUnrecognized code (0xFFFFFFF0):\n" + unknownResult;

                MessageBox.Show(message, "SystemErrorLookup test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            /// <summary>
            /// Runs every dialog-producing test back to back with short delays, useful as a smoke test after touching <see cref="BugReport"/>.
            /// <br>Watch that each dialog appears one at a time and closes cleanly before the next shows up.</br>
            /// </summary>
            public static async void Test_RunAllSequentially()
            {
                Test_SimpleExceptionOnUIThread();
                await Task.Delay(500);

                Test_ExceptionFromBackgroundThread();
                await Task.Delay(1500);

                Test_Win32ErrorCode();
                await Task.Delay(500);

                Test_HResultFromCryptoApi();
                await Task.Delay(500);

                Test_NestedInnerExceptions();
                await Task.Delay(500);

                Test_AggregateException();
                await Task.Delay(500);

                Test_ExceptionWithData();
                await Task.Delay(500);

                Test_ConcurrentExceptionsAreQueued();
                await Task.Delay(3000);

                Test_DuplicateExceptionIsDeduped();
                await Task.Delay(3000);

                Test_SystemErrorLookupDirect();
            }
        }
    }
}