using Serilog;
using Serilog.Formatting.Json;
using System.IO;

namespace WinPaletter
{
    public static class LoggerInitializer
    {
        private static bool _initialized = false;
        private static readonly object _lock = new();

        public static void Initialize()
        {
            if (_initialized) return;

            lock (_lock)
            {
                if (_initialized) return;    // double-check inside lock

                // Make sure log directory exists
                if (!Directory.Exists(SysPaths.LogsDir)) Directory.CreateDirectory(SysPaths.LogsDir);

                // Configure Serilog once at startup
                Program.Log = new LoggerConfiguration()
                    .WriteTo.File(new JsonFormatter(),
                                  Program.LogFile,
                                  rollingInterval: RollingInterval.Infinite, // no auto-rolling
                                  shared: false,                             // exclusive lock
                                  fileSizeLimitBytes: null)                  // optional: disable size limit
                    .CreateLogger();

                _initialized = true;
            }
        }
    }
}