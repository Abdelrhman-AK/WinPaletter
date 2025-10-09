using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Controllers
{
    public class Magnifier : Control
    {
        public Magnifier()
        {
            DoubleBuffered = true;
        }

        #region Helpers

        float DPI = Program.GetWindowsScreenScalingFactor(false);

        private Bitmap GetScreen()
        {
            if (!DesignMode)
            {
                Point mousePosition = new((int)(Cursor.Position.X * DPI), (int)(Cursor.Position.Y * DPI));
                Rectangle sourceRectangle = new(mousePosition.X - Width / _zoom, mousePosition.Y - Height / _zoom, Width, Height);
                Rectangle magnifiedRectangle = new(Width / (_zoom * 2), Height / (_zoom * 2), Width / _zoom, Height / _zoom);

                using (Bitmap bmpScreenshot = new(Width, Height))
                using (Graphics G = Graphics.FromImage(bmpScreenshot))
                {
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    G.CopyFromScreen(sourceRectangle.Location, Point.Empty, sourceRectangle.Size);

                    return bmpScreenshot.Clone(magnifiedRectangle, PixelFormat.Format32bppRgb);
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Properties

        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                if (value != base.Enabled)
                {
                    base.Enabled = value;

                    if (!DesignMode && value)
                    {
                        DPI = Program.GetWindowsScreenScalingFactor(false);
                        mouseHookProc = MouseHookCallback;
                        mouseHook = SetMouseHook(mouseHookProc);
                    }
                    else if (!DesignMode)
                    {
                        User32.UnhookWindowsHookEx(mouseHook);
                    }

                    Invalidate();
                }
            }
        }

        private int _zoom = 3;
        public int Zoom
        {
            get => _zoom;
            set
            {
                if (value > 0)
                {
                    _zoom = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region Mouse hock

        private const int WH_MOUSE_LL = 14;
        private const int WM_MOUSEMOVE = 0x0200;
        private User32.LowLevelMouseProc mouseHookProc;
        private IntPtr mouseHook;

        private IntPtr SetMouseHook(User32.LowLevelMouseProc proc)
        {
            using (ProcessModule curModule = Process.GetCurrentProcess().MainModule)
            {
                return User32.SetWindowsHookEx(WH_MOUSE_LL, proc,
                    Kernel32.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_MOUSEMOVE)
            {
                // NativeMethods.User32.MSLLHOOKSTRUCT hookStruct = (NativeMethods.User32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeMethods.User32.MSLLHOOKSTRUCT));
                // Handle mouse movement here

                Task.Run(() =>
                {
                    Thread.Sleep(10);
                    Invoke(() => Invalidate());
                }
                );
            }

            return User32.CallNextHookEx(mouseHook, nCode, wParam, lParam);
        }

        protected override void Dispose(bool disposing)
        {
            User32.UnhookWindowsHookEx(mouseHook);
            base.Dispose(disposing);
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.InterpolationMode = InterpolationMode.NearestNeighbor;
            G.CompositingQuality = CompositingQuality.HighQuality;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            if (!DesignMode && Enabled)
            {
                Bitmap screen = GetScreen();

                if (screen != null)
                {
                    G.DrawImage(screen, 0, 0, Width - 1, Height + 1);

                    G.DrawRectangle(Pens.Red, new(1 + (Width - 4) / 2, 1 + (Height - 4) / 2, 4, 4));
                }
                else
                {
                    G.Clear(Color.Black);
                }
            }

            using (Pen P = new(Color.WhiteSmoke))
            {
                P.DashStyle = DashStyle.Dash;

                G.DrawRectangle(P, 0, 0, Width - 1, Height - 1);
            }

            base.OnPaint(e);


        }
    }
}