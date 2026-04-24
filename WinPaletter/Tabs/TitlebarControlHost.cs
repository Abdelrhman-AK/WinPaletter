using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// Hosts an arbitrary WinForms <see cref="Control"/> inside the titlebar caption band
    /// by carving a rectangle out of the non-client area via <c>WM_NCCALCSIZE</c>.
    /// <br></br>
    /// How it works: 
    /// Windows computes the client rect by calling WM_NCCALCSIZE.  By intercepting
    /// that message we can extend the client area upward into the caption band by
    /// exactly <see cref="SystemInformation.CaptionHeight"/> pixels.  The hosted
    /// control is then placed at client-coordinate y=0, right-aligned left of the
    /// native control-box.  The rest of the widened top strip is painted to match
    /// the caption background so it blends with the NC frame.
    ///<br></br>
    /// The rest of the caption remains true non-client area. DwmDefWindowProc (or DefWindowProc on XP/Vista classic) handles the control-box hit tests so Min / Max / Close buttons work exactly as before.
    /// <br></br>
    /// Usage:<br></br>
    /// <c>
    ///   _host = new TitlebarControlHost(this);<br></br>
    ///   _host.SetControl(myButton, controlWidth: 90);<br></br>
    ///   ...<br></br>
    ///   _host.Dispose();<br></br>
    /// </c>
    /// </summary>
    public sealed class TitlebarControlHost : IDisposable
    {
        /// <summary>
        /// Represents a delegate instance for querying whether Desktop Window Manager (DWM) composition is enabled on
        /// the system.
        /// </summary>
        /// <remarks>This field provides access to the native DWM composition status function. It is
        /// typically used when interoperating with Windows APIs to determine if DWM composition effects, such as Aero
        /// Glass, are currently active.</remarks>
        private static readonly DWMAPI.FnDwmIsCompositionEnabled _dwmIsEnabled;

        /// <summary>
        /// Represents a delegate instance for the default window procedure used by Desktop Window Manager (DWM)
        /// operations.
        /// </summary>
        /// <remarks>This field provides access to the function pointer for the DWM default window
        /// procedure, which can be used when handling custom window messages in interop scenarios. It is typically used
        /// in advanced Windows API integrations.</remarks>
        private static readonly DWMAPI.FnDwmDefWindowProc _dwmDefWndProc;

        static TitlebarControlHost()
        {
            IntPtr hLib = Kernel32.LoadLibrary("dwmapi.dll");
            if (hLib == IntPtr.Zero) return;

            IntPtr pE = Kernel32.GetProcAddress(hLib, "DwmIsCompositionEnabled");
            IntPtr pD = Kernel32.GetProcAddress(hLib, "DwmDefWindowProc");

            if (pE != IntPtr.Zero) _dwmIsEnabled = Marshal.GetDelegateForFunctionPointer<DWMAPI.FnDwmIsCompositionEnabled>(pE);
            if (pD != IntPtr.Zero) _dwmDefWndProc = Marshal.GetDelegateForFunctionPointer<DWMAPI.FnDwmDefWindowProc>(pD);
        }

        private static bool IsDwmEnabled()
        {
            if (_dwmIsEnabled == null) return false;
            _dwmIsEnabled(out bool en);
            return en;
        }

        private const int WM_PAINT = 0x000F;
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_ACTIVATE = 0x0006;
        private const int WM_SIZE = 0x0005;
        private const int WM_NCACTIVATE = 0x0086;
        private const int WM_DWMCOMPOSITIONCHANGED = 0x031E;

        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private const int HTMINBUTTON = 8;
        private const int HTMAXBUTTON = 9;
        private const int HTCLOSE = 20;

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const uint SWP_NOOWNERZORDER = 0x0200;

        private readonly Form _form;
        private readonly WndSub _sub;
        private Control _control;
        private int _controlWidth;
        private bool _disposed;

        // Pixel gap between the right edge of the hosted control and the left edge of the native control-box buttons.
        private const int RightGap = 4;


        public TitlebarControlHost(Form form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form));
            _sub = new WndSub(this);

            _form.HandleCreated += OnHandleCreated;
            _form.HandleDestroyed += OnHandleDestroyed;

            if (_form.IsHandleCreated) _sub.Attach(_form.Handle);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _form.HandleCreated -= OnHandleCreated;
            _form.HandleDestroyed -= OnHandleDestroyed;

            RemoveControl();
            _sub.Detach();
        }

        /// <summary>
        /// Embeds <paramref name="control"/> into the titlebar caption band.
        /// The control is right-aligned, left of the native Min/Max/Close buttons.
        /// Its height is automatically fitted to the caption height.
        /// </summary>
        /// <param name="control">The control to embed. Must not be null.</param>
        /// <param name="controlWidth">Width in pixels of the slot reserved in the caption.</param>
        public void SetControl(Control control, int controlWidth)
        {
            if (controlWidth <= 0) throw new ArgumentOutOfRangeException(nameof(controlWidth));

            RemoveControl();
            _control = control ?? throw new ArgumentNullException(nameof(control));
            _controlWidth = controlWidth;

            if (_form.IsHandleCreated) EmbedControl();
        }

        /// <summary>
        /// Removes the hosted control and restores the caption to its original size.
        /// </summary>
        public void RemoveControl()
        {
            if (_control == null) return;

            _form.Controls.Remove(_control);
            _control = null;
            _controlWidth = 0;

            if (_form.IsHandleCreated) TriggerFrameChange();
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            _sub.Attach(_form.Handle);
            if (_control != null) EmbedControl();
        }

        private void OnHandleDestroyed(object sender, EventArgs e)
        {
            _sub.Detach();
        }

        private void EmbedControl()
        {
            if (!_form.Controls.Contains(_control)) _form.Controls.Add(_control);

            TriggerFrameChange();
        }

        /// <summary>
        /// Forces Windows to re-run WM_NCCALCSIZE so our carved client rect is
        /// applied and the NC frame is repainted accordingly.
        /// </summary>
        private void TriggerFrameChange()
        {
            User32.SetWindowPos(_form.Handle, IntPtr.Zero, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOZORDER | SWP_NOACTIVATE | SWP_FRAMECHANGED | SWP_NOOWNERZORDER);
        }

        private int TopBorderThickness => _form.FormBorderStyle == FormBorderStyle.None ? 0 : SystemInformation.FrameBorderSize.Height;

        private int ControlBoxWidth
        {
            get
            {
                int count = 1; // Close always present
                if (_form.MinimizeBox) count++;
                if (_form.MaximizeBox) count++;
                return SystemInformation.CaptionButtonSize.Width * count;
            }
        }

        /// <summary>
        /// Extends the client rect upward by <see cref="CarvedHeight"/> pixels
        /// so the top strip of what was the caption band becomes client area.
        ///
        /// Normal DefWindowProc transform (sizable window, no menu):
        ///   client.Left   = window.Left   + frameBorderW
        ///   client.Top    = window.Top    + frameBorderH + captionHeight
        ///   client.Right  = window.Right  - frameBorderW
        ///   client.Bottom = window.Bottom - frameBorderH
        ///
        /// We let DefWindowProc run first, then shift client.Top upward by
        /// captionHeight.  The resulting extra row at the top of the client area
        /// is where we place the hosted control and the caption-background panel.
        /// </summary>
        private void HandleNcCalcSize(ref Message m)
        {
            // Let Windows compute the standard client rect.
            _sub.BaseWndProc(ref m);

            if (_control == null) return;

            // Extend client.Top upward to absorb the caption band.
            User32.NCCALCSIZE_PARAMS p = Marshal.PtrToStructure<User32.NCCALCSIZE_PARAMS>(m.LParam);
            p.rgrc0.top -= SystemInformation.CaptionHeight;
            Marshal.StructureToPtr(p, m.LParam, true);

            m.Result = IntPtr.Zero;
        }

        private void HandleNcHitTest(ref Message m)
        {
            // Give DWM first look so control-box hover effects and Aero Snap zones are handled natively.
            if (_dwmDefWndProc != null)
            {
                _dwmDefWndProc(_form.Handle, WM_NCHITTEST,
                    m.WParam, m.LParam, out IntPtr dwmHit);

                int code = dwmHit.ToInt32();
                if (code == HTMINBUTTON || code == HTMAXBUTTON || code == HTCLOSE)
                {
                    m.Result = dwmHit;
                    return;
                }
            }

            Point screenPt = LParamToPoint(m.LParam);
            User32.GetWindowRect(_form.Handle, out UxTheme.RECT wr);

            int border = TopBorderThickness;
            int captionH = SystemInformation.CaptionHeight;

            int captionTop = wr.top + border;
            int captionBottom = captionTop + captionH;

            // Outside caption band — let DefWindowProc handle resize borders etc.
            if (screenPt.Y < captionTop || screenPt.Y >= captionBottom)
            {
                _sub.BaseWndProc(ref m);
                return;
            }

            // Inside caption band — is cursor over the hosted-control slot?
            if (_control != null)
            {
                int slotRight = wr.right - ControlBoxWidth;
                int slotLeft = slotRight - _controlWidth - RightGap;

                if (screenPt.X >= slotLeft && screenPt.X < slotRight)
                {
                    m.Result = (IntPtr)HTCLIENT;
                    return;
                }
            }

            // Remaining caption → drag.
            m.Result = (IntPtr)HTCAPTION;
        }

        /// <summary>
        /// Positions the hosted control and the caption-background fill panel
        /// within the carved client strip (client y = 0 .. CarvedHeight).
        /// </summary>
        private void RepositionControl()
        {
            if (_control == null || !_form.IsHandleCreated) return;

            int captionH = SystemInformation.CaptionHeight;
            int ctrlH = Math.Max(1, captionH - 2); // 1 px top/bottom padding
            int clientW = _form.ClientSize.Width;

            // Right-align the control, leaving RightGap before the control box.
            // In client coordinates: x = clientWidth - controlWidth. (The native control-box is in the NC area, outside ClientSize.)
            _control.SetBounds(clientW - _controlWidth, (captionH - ctrlH) / 2, _controlWidth, ctrlH);
        }

        /// <summary>
        /// Paints the left portion of the carved strip (y = 0 .. CarvedHeight)
        /// to blend with the NC caption frame.
        /// <br></br>
        /// • DWM enabled  → fill black at alpha=0 so the DWM glass bleeds through.
        /// <br></br>
        /// • DWM disabled → fill SystemColors.ActiveCaption / InactiveCaption.
        /// </summary>
        private void PaintCaptionStrip()
        {
            if (_control == null || !_form.IsHandleCreated) return;

            int captionH = SystemInformation.CaptionHeight;
            int clientW = _form.ClientSize.Width;
            int slotLeft = clientW - _controlWidth; // left edge of the control slot

            // Fill everything left of the hosted control.
            Rectangle fillRect = new(0, 0, slotLeft, captionH);
            if (fillRect.Width <= 0 || fillRect.Height <= 0) return;

            using (Graphics G = _form.CreateGraphics())
            {
                if (IsDwmEnabled())
                {
                    // Alpha=0 black lets DWM glass render through the client area.
                    G.FillRectangle(Brushes.Black, fillRect);
                }
                else
                {
                    bool active = _form.ContainsFocus;
                    Color caption = active ? SystemColors.ActiveCaption : SystemColors.InactiveCaption;

                    using (SolidBrush b = new(caption)) G.FillRectangle(b, fillRect);
                }
            }
        }

        private static Point LParamToPoint(IntPtr lParam)
        {
            int raw = lParam.ToInt32();
            return new Point((short)(raw & 0xFFFF), (short)((raw >> 16) & 0xFFFF));
        }

        private sealed class WndSub : NativeWindow
        {
            private readonly TitlebarControlHost _host;

            internal WndSub(TitlebarControlHost host) { _host = host; }

            internal void Attach(IntPtr hwnd)
            {
                if (Handle == IntPtr.Zero) AssignHandle(hwnd);
            }

            internal void Detach()
            {
                if (Handle != IntPtr.Zero) ReleaseHandle();
            }

            /// <summary>Calls the original (pre-subclass) window procedure.</summary>
            internal void BaseWndProc(ref Message m) { DefWndProc(ref m); }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case WM_NCCALCSIZE:
                        if (m.WParam != IntPtr.Zero)
                        {
                            _host.HandleNcCalcSize(ref m);
                            _host.RepositionControl();
                            return;
                        }
                        break;

                    case WM_NCHITTEST:
                        _host.HandleNcHitTest(ref m);
                        return;

                    case WM_SIZE:
                        base.WndProc(ref m);
                        _host.RepositionControl();
                        _host.PaintCaptionStrip();
                        return;

                    case WM_PAINT:
                        base.WndProc(ref m);
                        _host.PaintCaptionStrip();
                        return;

                    case WM_ACTIVATE:
                    case WM_NCACTIVATE:
                        base.WndProc(ref m);
                        _host.PaintCaptionStrip();
                        return;

                    case WM_DWMCOMPOSITIONCHANGED:
                        base.WndProc(ref m);
                        _host.TriggerFrameChange();
                        return;
                }

                base.WndProc(ref m);
            }
        }
    }
}