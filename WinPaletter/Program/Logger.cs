using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Specifies which registry sub-categories are included when calling <see cref="Logger.WriteReg"/>.
    /// </summary>
    [Flags]
    public enum RegScope
    {
        None = 0,
        Read = 1 << 0,
        Write = 1 << 1,
        Delete = 1 << 2,
        Error = 1 << 3,
        All = Read | Write | Delete | Error,
    }

    /// <summary>
    /// Application-wide logger. Wraps Serilog and optionally surfaces messages on the main-form
    /// status label.
    /// </summary>
    public sealed class Logger : IDisposable
    {
        // volatile ensures the double-checked lock is safe across CPU cores/compilers.
        private static volatile bool _initialized;
        private static readonly object _initLock = new();
        private static Serilog.Core.Logger _log;

        private static readonly object _statusLock = new();
        private static System.Threading.Timer _statusResetTimer;
        private const int ResetMs = 3000;

        private bool _disposed;

        /// <summary>Initialises the logger instance (calls <see cref="Initialize"/> once).</summary>
        public Logger()
        {
            Initialize();
            UI.Style.Config.DarkModeChanged += StyleChanged;
            UI.Style.Config.SchemeChanged += StyleChanged;
        }

        private void StyleChanged()
        {
            MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm is null || mainForm.IsDisposed) return;

            ApplyStatusColor(mainForm, LogEventLevel.Information); 
        }

        /// <summary>
        /// Configures the underlying Serilog sink. Safe to call multiple times; only the first
        /// call has any effect.
        /// </summary>
        public static void Initialize()
        {
            // Fast-path: avoid the lock when already initialised.
            if (_initialized || !Program.Settings.AppLog.SaveInLogFile) return;

            lock (_initLock)
            {
                if (_initialized) return;   // re-check inside lock

                Directory.CreateDirectory(SysPaths.LogsDir); // no-op if already exists

                _log = new LoggerConfiguration()
                    .Enrich.WithProperty("App", "WinPaletter")
                    .WriteTo.File(
                        new JsonFormatter(),
                        Program.LogFile,
                        rollingInterval: RollingInterval.Infinite,
                        shared: false,
                        fileSizeLimitBytes: null)
                    .CreateLogger();

                _initialized = true;
            }
        }

        /// <summary>
        /// Writes a log entry at the specified <paramref name="level"/> and optionally attaches an
        /// exception.
        /// </summary>
        public void Write(LogEventLevel level, string messageTemplate, Exception ex = null)
        {
            if (string.IsNullOrWhiteSpace(messageTemplate)) return;

            // Normalise newlines so single-line entries remain readable in a status label.
            messageTemplate = messageTemplate
                .Replace("\r\n", ", ")
                .Replace('\r', ' ')
                .Replace('\n', ' ')
                .Trim();

#if DEBUG
            var debugLine = ex is null
                ? $"[{DateTime.Now:O}] [{level}] {messageTemplate}\r\n"
                : $"[{DateTime.Now:O}] [{level}] {messageTemplate}; {ex.Message}\r\n";
            Debugger.Log((int)level, level.ToString(), debugLine);
#endif

            // Lazily initialise in case the settings were flipped after construction.
            Initialize();

            if (!Program.Settings.AppLog.Enabled) return;

            string time = DateTime.Now.ToString("HH:mm:ss");

            string statusText = ex is null
                ? $"[{time}] [{level}] {messageTemplate}"
                : $"[{time}] [{level}] {messageTemplate}; {ex.Message}";

            UpdateStatusLabel(level, statusText);

            if (!Program.Settings.AppLog.SaveInLogFile) return;

            // Use Serilog structured properties instead of embedding values into the template
            // string — this keeps JSON output queryable by field rather than as raw text.
            if (ex is null)
                _log?.Write(level, "[{Timestamp}] {Message}", DateTime.Now, messageTemplate);
            else
                _log?.Write(level, ex, "[{Timestamp}] {Message}", DateTime.Now, messageTemplate);
        }

        /// <inheritdoc cref="Write"/>
        public void Debug(string messageTemplate) => Write(LogEventLevel.Debug, messageTemplate);

        /// <summary>
        /// Writes a registry-related log entry when <c>AppLog.Reg</c> is enabled and the entry
        /// falls within <paramref name="scope"/>.
        /// </summary>
        /// <param name="scope">
        /// Bitwise combination of <see cref="RegScope"/> values that gate this entry.
        /// Pass <see cref="RegScope.All"/> to respect only the top-level <c>Reg</c> switch.
        /// </param>
        public void WriteReg(LogEventLevel level, string messageTemplate, RegScope scope = RegScope.All, Exception ex = null)
        {
            if (!Program.Settings.AppLog.Reg) return;

            bool allowed =
                (scope.HasFlag(RegScope.Read) && Program.Settings.AppLog.RegRead) ||
                (scope.HasFlag(RegScope.Write) && Program.Settings.AppLog.RegWrite) ||
                (scope.HasFlag(RegScope.Delete) && Program.Settings.AppLog.RegDelete) ||
                (scope.HasFlag(RegScope.Error) && Program.Settings.AppLog.Reg);

            if (allowed) Write(level, messageTemplate, ex);
        }

        private static void InvokeOnMainForm(Action action)
        {
            MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm is null || mainForm.IsDisposed) return;

            try
            {
                if (mainForm.InvokeRequired)
                    mainForm.Invoke(action);
                else
                    action();
            }
            catch (ObjectDisposedException) { /* form disposed mid-invoke — harmless */ }
        }

        private static Color StatusColor(LogEventLevel level) => level switch
        {
            LogEventLevel.Warning => Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover,
            LogEventLevel.Error => Program.Style.Schemes.Secondary.Colors.Back_Checked_Hover,
            LogEventLevel.Fatal => Program.Style.Schemes.Secondary.Colors.Back_Checked_Hover,
            _ => Program.Style.Schemes.Main.Colors.Back(),
        };

        private static void ApplyStatusColor(MainForm mainForm, LogEventLevel level)
        {
            var color = StatusColor(level);
            if (Program.Style.Animations)
                FluentTransitions.Transition
                    .With(mainForm.Status_lbl, nameof(mainForm.Status_lbl.BackColor), color)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            else
                mainForm.Status_lbl.BackColor = color;
        }

        private void UpdateStatusLabel(LogEventLevel level, string message)
        {
            if (!Program.Settings.AppLog.StatusPanel) return;

            InvokeOnMainForm(() =>
            {
                MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                if (mainForm is null || mainForm.IsDisposed) return;

                mainForm.Status_lbl.Text = message;
                ApplyStatusColor(mainForm, level);
            });

            // (Re-)arm the reset timer — debounces rapid successive messages.
            lock (_statusLock)
            {
                if (_statusResetTimer is null)
                    _statusResetTimer = new System.Threading.Timer(StatusResetTimer_Callback, null, ResetMs, Timeout.Infinite);
                else
                    _statusResetTimer.Change(ResetMs, Timeout.Infinite);
            }
        }

        private static void StatusResetTimer_Callback(object _)
        {
            if (!Program.Settings.AppLog.AutoHideLog) return;

            InvokeOnMainForm(() =>
            {
                MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                if (mainForm is null || mainForm.IsDisposed) return;

                if (Program.Style.Animations)
                    FluentTransitions.Transition
                        .With(mainForm.Status_lbl, nameof(mainForm.Status_lbl.Text), string.Empty)
                        .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                else
                    mainForm.Status_lbl.Text = string.Empty;

                ApplyStatusColor(mainForm, default);
            });
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            lock (_statusLock)
            {
                _statusResetTimer?.Dispose();
                _statusResetTimer = null;
            }

            // Allow re-initialisation if a new Logger is constructed later in the same process.
            lock (_initLock)
            {
                _log?.Dispose();
                _log = null;
                _initialized = false;
            }
        }
    }
}