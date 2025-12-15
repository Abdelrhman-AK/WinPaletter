using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.WP
{
    [Description("ListView fixed to respect dark/light mode")]
    public class ListView : System.Windows.Forms.ListView
    {
        private const int LVM_GETHEADER = 0x1000 + 31;
        private const int GWL_WNDPROC = -4;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_COMPOSITED = 0x02000000;
        private IntPtr headerHandle;
        private IntPtr oldHeaderProc = IntPtr.Zero;
        private User32.WndProcDelegate newHeaderProc;

        public ListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            GridLines = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.Style |= 0x80; // Enable double-buffering style for ListView itself
                return parms;
            }
        }

        private int _rowHeight = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Description("Changes row height in Details view only when SmallImageList is assigned")]
        public int RowHeight
        {
            get => _rowHeight;
            set
            {
                if (_rowHeight == value) return;

                _rowHeight = value;
                ApplyRowHeight();
            }
        }

        public new ImageList SmallImageList
        {
            get => base.SmallImageList;
            set
            {
                base.SmallImageList?.Dispose();
                base.SmallImageList = value;
                ApplyRowHeight(); // re-apply when list changes
            }
        }

        private void ApplyRowHeight()
        {
            if (DesignMode) return;
            if (View != View.Details) return;
            if (SmallImageList == null) return;
            if (_rowHeight <= 0) return;

            var oldList = SmallImageList;
            var paddingHorizontal = 1; // small horizontal padding
            var newList = new ImageList
            {
                ImageSize = new(oldList.ImageSize.Width + paddingHorizontal * 2, _rowHeight),
                ColorDepth = oldList.ColorDepth
            };

            for (int i = 0; i < oldList.Images.Count; i++)
            {
                using (Image img = oldList.Images[i])
                {
                    string key = oldList.Images.Keys.Count > i ? oldList.Images.Keys[i] : null;

                    int extraHeight = _rowHeight - img.Height;
                    int paddedWidth = img.Width + paddingHorizontal * 2;

                    Bitmap padded = new(paddedWidth, _rowHeight);
                    using (Graphics G = Graphics.FromImage(padded))
                    {
                        G.Clear(Color.Transparent);
                        G.DrawImage(img, paddingHorizontal, extraHeight / 2, img.Width, img.Height); // left padding, vertical centered
                    }

                    if (!string.IsNullOrEmpty(key)) newList.Images.Add(key, padded);
                    else newList.Images.Add(padded);
                }
            }

            base.SmallImageList?.Dispose();
            base.SmallImageList = newList;
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode) return;

            UxTheme.SetWindowTheme(Handle, "Explorer", null);
            SubclassHeader();
        }

        #region Header Subclass

        private void SubclassHeader()
        {
            headerHandle = User32.SendMessage(Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            if (headerHandle != IntPtr.Zero)
            {
                // Apply modern Explorer theme for smoother rendering
                UxTheme.SetWindowTheme(headerHandle, "Explorer", null);

                // Enable double-buffering at the window level to avoid flicker
                int exStyle = User32.GetWindowLong(headerHandle, GWL_EXSTYLE);
                User32.SetWindowLong(headerHandle, GWL_EXSTYLE, exStyle | WS_EX_COMPOSITED);

                newHeaderProc = new User32.WndProcDelegate(HeaderWndProc);
                oldHeaderProc = User32.SetWindowLongPtr(headerHandle, GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(newHeaderProc));

                InvalidateHeader();
            }
        }

        private void InvalidateHeader()
        {
            if (headerHandle != IntPtr.Zero) User32.InvalidateRect(headerHandle, IntPtr.Zero, true);
        }

        private IntPtr HeaderWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (DesignMode) return User32.CallWindowProc(oldHeaderProc, hWnd, msg, wParam, lParam);

            const int WM_PAINT = 0x000F;

            if (msg == WM_PAINT && Program.Style.DarkMode)
            {
                User32.GetClientRect(hWnd, out UxTheme.RECT rect);
                using (Bitmap bmp = new(rect.right - rect.left, rect.bottom - rect.top))
                using (Graphics G = Graphics.FromImage(bmp))
                {
                    // Draw header background
                    G.Clear(Program.Style.Schemes.Main.Colors.Button);

                    int x = 0;

                    for (int i = 0; i < Columns.Count; i++)
                    {
                        int colWidth = Columns[i].Width;
                        Rectangle cellRect = new(x, 0, colWidth, rect.bottom - rect.top);

                        // Draw column text
                        TextRenderer.DrawText(G, Columns[i].Text, Font, cellRect, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);

                        // Draw splitter line (except after last column)
                        if (i < Columns.Count - 1)
                        {
                            using (Pen splitterPen = new(Program.Style.Schemes.Main.Colors.Line_Hover()))
                            {
                                int splitterX = x + colWidth - 2; // right edge of the column
                                G.DrawLine(splitterPen, splitterX, 2, splitterX, rect.bottom - 2);
                            }
                        }

                        x += colWidth;
                    }

                    // Blit to header HWND
                    using (Graphics hdc = Graphics.FromHwnd(hWnd))
                    {
                        hdc.DrawImageUnscaled(bmp, 0, 0);
                    }
                }

                return IntPtr.Zero; // skip default painting
            }

            return User32.CallWindowProc(oldHeaderProc, hWnd, msg, wParam, lParam);
        }

        #endregion
    }
}