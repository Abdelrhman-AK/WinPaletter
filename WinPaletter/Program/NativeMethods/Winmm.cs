using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for functions in the winmm.dll library.
    /// </summary>
    public class Winmm
    {
        private const string _winmm = "winmm.dll";

        /// <summary>
        /// Sends a command string to the MCI device specified in the command.
        /// </summary>
        /// <param name="command">The command string to be sent to the MCI device.</param>
        /// <param name="buffer">A Buffer that receives return information.</param>
        /// <param name="bufferSize">The size, in characters, of the Buffer.</param>
        /// <param name="hwndCallback">A handle to the callback window if the "notify" flag is specified in the command.</param>
        /// <returns>Returns zero if successful; otherwise, an error code.</returns>
        [DllImport(_winmm)]
        public static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        [DllImport(_winmm, SetLastError = true)]
        public static extern uint timeSetEvent(uint uDelay, uint uResolution, TimerCallback lpTimeProc, UIntPtr dwUser, uint fuEvent);

        [DllImport(_winmm, SetLastError = true)]
        public static extern uint timeKillEvent(uint uTimerID);

        [DllImport(_winmm)]
        public static extern uint timeBeginPeriod(uint uPeriod);

        [DllImport(_winmm)]
        public static extern uint timeEndPeriod(uint uPeriod);

        public delegate void TimerCallback(uint id, uint msg, UIntPtr user, UIntPtr dw1, UIntPtr dw2);

        private const uint TIME_PERIODIC = 1;
        private const uint TIME_KILL_SYNCHRONOUS = 0x0100;

        /// <summary>
        /// High-resolution timer based on the Windows multimedia timer API (winmm.dll).
        /// Ticks fire on a dedicated worker thread and are not subject to the UI thread's message queue backlog, unlike System.Windows.Forms.Timer which relies on WM_TIMER.
        /// </summary>
        public sealed class MultimediaTimer : IDisposable
        {
            private readonly TimerCallback _callback;
            private uint _timerId;
            private bool _isRunning;
            private readonly uint _resolutionMs;

            /// <summary>Raised on the multimedia timer worker thread. Do not touch UI controls directly here.</summary>
            public event EventHandler Tick;

            public MultimediaTimer(uint intervalMs, uint resolutionMs = 1)
            {
                IntervalMs = intervalMs;
                _resolutionMs = resolutionMs;
                _callback = OnTimerFired;
            }

            public uint IntervalMs { get; set; }

            public void Start()
            {
                if (_isRunning) return;

                timeBeginPeriod(_resolutionMs);
                _timerId = timeSetEvent(IntervalMs, _resolutionMs, _callback, UIntPtr.Zero, TIME_PERIODIC | TIME_KILL_SYNCHRONOUS);
                _isRunning = _timerId != 0;
            }

            public void Stop()
            {
                if (!_isRunning) return;

                timeKillEvent(_timerId);
                timeEndPeriod(_resolutionMs);
                _isRunning = false;
            }

            private void OnTimerFired(uint id, uint msg, UIntPtr user, UIntPtr dw1, UIntPtr dw2)
            {
                Tick?.Invoke(this, EventArgs.Empty);
            }

            public void Dispose()
            {
                Stop();
            }
        }
    }
}
