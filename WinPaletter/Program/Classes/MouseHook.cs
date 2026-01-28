using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public class MouseHook : IDisposable
    {
        #region Constants & Delegates
        private const int WH_MOUSE_LL = 14;
        private const int WM_MOUSEMOVE = 0x0200;

        public delegate void MouseMoveHandler(object sender, MouseEventArgs e);
        public event MouseMoveHandler MouseMoved;

        private NativeMethods.User32.LowLevelMouseProc _hookProc;

        private IntPtr _hookHandle = IntPtr.Zero;
        #endregion

        #region Properties
        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    if (_enabled)
                        SetHook();
                    else
                        Unhook();
                }
            }
        }
        #endregion

        #region Constructor
        public MouseHook()
        {
            _hookProc = HookCallback;
        }

        public MouseHook(Control control)
        {
            _hookProc = HookCallback;

            if (control != null)
            {
                // Auto-disable when control is disabled
                control.EnabledChanged += (s, e) =>
                {
                    Enabled = control.Enabled;
                };

                // Auto dispose hook when control is disposed
                control.Disposed += (s, e) =>
                {
                    Dispose();
                };
            }
        }
        #endregion

        #region Hook Logic
        private void SetHook()
        {
            if (_hookHandle != IntPtr.Zero) return;

            using ProcessModule curModule = Process.GetCurrentProcess().MainModule;
            _hookHandle = User32.SetWindowsHookEx(WH_MOUSE_LL, _hookProc, Kernel32.GetModuleHandle(curModule.ModuleName), 0);
        }

        private void Unhook()
        {
            if (_hookHandle != IntPtr.Zero)
            {
                User32.UnhookWindowsHookEx(_hookHandle);
                _hookHandle = IntPtr.Zero;
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_MOUSEMOVE)
            {
                if (MouseMoved != null)
                {
                    var hookStruct = Marshal.PtrToStructure<User32.MSLLHOOKSTRUCT>(lParam);
                    var args = new MouseEventArgs(MouseButtons.None, 0, hookStruct.pt.x, hookStruct.pt.y, 0);

                    MouseMoved?.Invoke(this, args);
                }
            }

            return User32.CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }
        #endregion

        #region Cleanup
        public void Dispose()
        {
            Unhook();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
