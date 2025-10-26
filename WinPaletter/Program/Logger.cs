﻿using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace WinPaletter
{
    public class Logger : IDisposable
    {
        private static bool _initialized = false;
        private static readonly object _lock = new();
        private static Serilog.Core.Logger _log;
        private static readonly object _statusLock = new object();
        private static System.Threading.Timer _statusResetTimer;
        private const int ResetMs = 3000;
        private bool _disposed;

        public Logger()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (_initialized) return;

            lock (_lock)
            {
                if (_initialized) return;    // double-check inside lock

                // Make sure log directory exists
                if (!Directory.Exists(SysPaths.LogsDir)) Directory.CreateDirectory(SysPaths.LogsDir);

                // Configure Serilog once at startup
                _log = new LoggerConfiguration()
                    .WriteTo.File(new JsonFormatter(),
                                  Program.LogFile,
                                  rollingInterval: RollingInterval.Infinite, // no auto-rolling
                                  shared: false,                             // exclusive lock
                                  fileSizeLimitBytes: null)                  // optional: disable size limit
                    .CreateLogger();

                _initialized = true;
            }
        }

        void UpdateStatusColor(LogEventLevel level)
        {
            if (Forms.MainForm == null || Forms.MainForm.IsDisposed) return;

            Color color = level switch
            {
                LogEventLevel.Warning => Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover,
                LogEventLevel.Error => Program.Style.Schemes.Secondary.Colors.Back_Checked_Hover,
                LogEventLevel.Fatal => Program.Style.Schemes.Secondary.Colors.Back_Checked_Hover,
                _ => Program.Style.Schemes.Main.Colors.Back(),
            };

            if (Program.Style.Animations)
            {
                FluentTransitions.Transition.With(Forms.MainForm.Status_lbl, nameof(Forms.MainForm.Status_lbl.BackColor), color).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                Forms.MainForm.Status_lbl.BackColor = color;
            }
        }

        private void UpdateStatusLabel(LogEventLevel level, string message)
        {
            if (Forms.MainForm == null || Forms.MainForm.IsDisposed) return;

            // Update UI text (marshal to UI thread if needed)
            if (Forms.MainForm.InvokeRequired)
            {
                Forms.MainForm.Invoke(() =>
                {
                    Forms.MainForm.Status_lbl.Text = message;
                    UpdateStatusColor(level);
                });
            }
            else
            {
                Forms.MainForm.Status_lbl.Text = message;
                UpdateStatusColor(level);
            }

            // Ensure timer exists and reset its due time to ResetMs
            lock (_statusLock)
            {
                if (_statusResetTimer == null)
                {
                    // callback runs on threadpool; will clear UI using Invoke
                    _statusResetTimer = new System.Threading.Timer(StatusResetTimer_Callback, null, ResetMs, Timeout.Infinite);
                }
                else
                {
                    // Reset the countdown: due time = ResetMs, period = Infinite (one-shot)
                    _statusResetTimer.Change(ResetMs, Timeout.Infinite);
                }
            }
        }

        private void StatusResetTimer_Callback(object state)
        {
            // Called on threadpool thread: marshal to UI thread to clear label
            if (Forms.MainForm == null || Forms.MainForm.IsDisposed) return;

            try
            {
                if (Forms.MainForm.InvokeRequired)
                {
                    Forms.MainForm.Invoke(() =>
                    {
                        if (Program.Style.Animations) FluentTransitions.Transition.With(Forms.MainForm.Status_lbl, nameof(Forms.MainForm.Status_lbl.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                        else Forms.MainForm.Status_lbl.Text = string.Empty;
                        UpdateStatusColor(default);
                    });
                }
                else
                {
                    if (Program.Style.Animations) FluentTransitions.Transition.With(Forms.MainForm.Status_lbl, nameof(Forms.MainForm.Status_lbl.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                    else Forms.MainForm.Status_lbl.Text = string.Empty;
                    UpdateStatusColor(default);
                }
            }
            catch (ObjectDisposedException)
            {
                // Form disposed while invoking — ignore
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            lock (_statusLock)
            {
                if (_statusResetTimer != null)
                {
                    try
                    {
                        _statusResetTimer.Dispose();
                    }
                    catch { /* ignore */ }
                    _statusResetTimer = null;
                }
            }
        }

        public void Write(LogEventLevel level, string messageTemplate, System.Exception ex = null)
        {
            if (!_initialized) Initialize();

            if (Program.Settings.AppLog.Enabled)
            {
                if (ex is null)
                {
                    UpdateStatusLabel(level, $"[{level}] {messageTemplate}");
                    _log?.Write(level, messageTemplate);
                }
                else
                {
                    UpdateStatusLabel(level, $"[{level}] {messageTemplate}; {ex.Message}");
                    _log?.Write(level, $"{messageTemplate}; {ex.Message}", ex);
                }
            }
        }

        public void Debug(string messageTemplate)
        {
            Write(LogEventLevel.Debug, messageTemplate);
        }

        public void WriteReg(LogEventLevel level, string messageTemplate, System.Exception ex = null)
        {
            if (Program.Settings.AppLog.Reg) Write(level, messageTemplate, ex);
        }


        public void WriteRegRead(LogEventLevel level, string messageTemplate, System.Exception ex = null)
        {
            if (Program.Settings.AppLog.Reg && Program.Settings.AppLog.RegRead) Write(level, messageTemplate, ex);
        }

        public void WriteRegWrite(LogEventLevel level, string messageTemplate, System.Exception ex = null)
        {
            if (Program.Settings.AppLog.Reg && Program.Settings.AppLog.RegWrite) Write(level, messageTemplate, ex);
        }

        public void WriteRegDel(LogEventLevel level, string messageTemplate, System.Exception ex = null)
        {
            if (Program.Settings.AppLog.Reg && Program.Settings.AppLog.RegDelete) Write(level, messageTemplate, ex);
        }
    }
}