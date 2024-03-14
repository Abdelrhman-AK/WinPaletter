﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.UI.Controllers
{
    public class Magnifier : Control
    {
        public Magnifier()
        {
            DoubleBuffered = true;
        }

        #region Helpers

        private Bitmap GetScreen()
        {
            if (!DesignMode)
            {
                Point mousePosition = Cursor.Position;
                Rectangle sourceRectangle = new(mousePosition.X - Width / _zoom, mousePosition.Y - Height / _zoom, Width, Height);
                Rectangle magnifiedRectangle = new(Width / (_zoom * 2), Height / (_zoom * 2), Width / _zoom, Height / _zoom);

                using (Bitmap bmpScreenshot = new(Width, Height))
                using (Graphics G = Graphics.FromImage(bmpScreenshot))
                {
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    G.CopyFromScreen(sourceRectangle.Location, Point.Empty, sourceRectangle.Size);

                    return bmpScreenshot.Clone(magnifiedRectangle, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
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
                        mouseHookProc = MouseHookCallback;
                        mouseHook = SetMouseHook(mouseHookProc);
                    }
                    else if (!DesignMode)
                    {
                        NativeMethods.User32.UnhookWindowsHookEx(mouseHook);
                    }

                    Invalidate();
                }
            }
        }

        private int _zoom = 3;
        public int Zoom
        {
            get { return _zoom; }
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
        private NativeMethods.User32.LowLevelMouseProc mouseHookProc;
        private IntPtr mouseHook;

        private IntPtr SetMouseHook(NativeMethods.User32.LowLevelMouseProc proc)
        {
            using (ProcessModule curModule = Process.GetCurrentProcess().MainModule)
            {
                return NativeMethods.User32.SetWindowsHookEx(WH_MOUSE_LL, proc,
                    NativeMethods.Kernel32.GetModuleHandle(curModule.ModuleName), 0);
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
                    this.Invoke(() => Invalidate());
                }
                );
            }

            return NativeMethods.User32.CallNextHookEx(mouseHook, nCode, wParam, lParam);
        }

        protected override void Dispose(bool disposing)
        {
            NativeMethods.User32.UnhookWindowsHookEx(mouseHook);
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