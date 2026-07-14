using System;
using System.Collections.Generic;
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
                parms.ExStyle |= (int)Win32Control.ControlExtendedStyles.Composited;
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

        public void AddImagesToSmallImageList(IEnumerable<(Image image, string key)> images)
        {
            if (images == null || SmallImageList == null) return;

            var canvasSize = SmallImageList.ImageSize;

            foreach (var (image, key) in images)
            {
                if (image == null) continue;

                int x = (canvasSize.Width - image.Width) / 2;   // horizontal center
                int y = (canvasSize.Height - image.Height) / 2; // vertical center

                Bitmap padded = new(canvasSize.Width, canvasSize.Height);
                using (Graphics g = Graphics.FromImage(padded))
                {
                    g.Clear(Color.Transparent);
                    g.DrawImage(image, x, y, image.Width, image.Height);
                }

                if (!string.IsNullOrEmpty(key))
                    SmallImageList.Images.Add(key, padded);
                else
                    SmallImageList.Images.Add(padded);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode) return;

            RestoreHeaderSubclass(); // clean up any stale subclass from a previous handle
            SetControlTheme(Handle, CtrlTheme.DarkExplorer);
            SubclassHeader();
        }
        
        #region Header Subclass

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (DesignMode) return;
            if (!IsHandleCreated) return;

            // Windows resets the visual theme when the control is disabled,
            // so we forcefully reapply it regardless of Enabled state.
            SetControlTheme(Handle, CtrlTheme.DarkExplorer);
            RestoreHeaderSubclass();
            SubclassHeader();
        }

        private void RestoreHeaderSubclass()
        {
            if (headerHandle == IntPtr.Zero || oldHeaderProc == IntPtr.Zero) return;

            User32.SetWindowLongPtr(headerHandle, (int)NativeMethods.User32.WindowsLongs.WndProc, oldHeaderProc);
            oldHeaderProc = IntPtr.Zero;
            headerHandle = IntPtr.Zero;
            newHeaderProc = null;
        }

        private void SubclassHeader()
        {
            headerHandle = User32.SendMessage(Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            if (headerHandle != IntPtr.Zero)
            {
                // Process modern Explorer theme for smoother rendering
                SetControlTheme(Handle, CtrlTheme.DarkExplorer);

                // Enable double-buffering at the window level to avoid flicker
                Win32Control.AppendExtendedStyle(headerHandle, Win32Control.ControlExtendedStyles.Composited);

                newHeaderProc = new User32.WndProcDelegate(HeaderWndProc);
                oldHeaderProc = User32.SetWindowLongPtr(headerHandle, (int)NativeMethods.User32.WindowsLongs.WndProc, Marshal.GetFunctionPointerForDelegate(newHeaderProc));

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

            if (msg == (int)User32.WindowsMessage.Paint && Program.Style.DarkMode)
            {
                User32.GetClientRect(hWnd, out UxTheme.RECT rect);
                using (Bitmap bmp = new(rect.right - rect.left, rect.bottom - rect.top))
                using (Graphics G = Graphics.FromImage(bmp))
                {
                    // Draw header background
                    G.Clear(Enabled ? Program.Style.Schemes.Main.Colors.Button : Program.Style.Schemes.Disabled.Colors.Button);

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
                            using (Pen splitterPen = new(Enabled ? Program.Style.Schemes.Main.Colors.Line_Hover() : Program.Style.Schemes.Disabled.Colors.Line_Hover()))
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

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)User32.WindowsMessage.KillFocus) return;

            if (m.Msg == (int)User32.WindowsMessage.Paint && !Enabled && Program.Style.DarkMode)
            {
                // Let Windows do its default paint first into the window DC
                base.WndProc(ref m);

                // Then overdraw with our dark disabled colors on top
                using (Graphics G = Graphics.FromHwnd(Handle))
                {
                    // Cover the entire client area with our dark background
                    Rectangle client = ClientRectangle;
                    using (SolidBrush bgBrush = new(Program.Style.Schemes.Disabled.Colors.Button)) G.FillRectangle(bgBrush, client);

                    // Redraw each item manually
                    for (int i = 0; i < Items.Count; i++)
                    {
                        ListViewItem item = Items[i];
                        Rectangle itemBounds = item.GetBounds(ItemBoundsPortion.Entire);
                        if (!client.IntersectsWith(itemBounds)) continue;

                        // Alternating row tint
                        Color rowColor = (i % 2 == 0) ? Program.Style.Schemes.Disabled.Colors.Button : Program.Style.Schemes.Disabled.Colors.Button_Over;

                        using (SolidBrush rowBrush = new(rowColor)) G.FillRectangle(rowBrush, itemBounds);

                        for (int j = 0; j < Columns.Count; j++)
                        {
                            // Build column bounds manually from Entire item row + column offsets
                            Rectangle entireBounds = item.GetBounds(ItemBoundsPortion.Entire);

                            int colX = 0;
                            for (int k = 0; k < j; k++) colX += Columns[k].Width;

                            Rectangle subBounds = new(colX, entireBounds.Top, Columns[j].Width, entireBounds.Height);

                            if (subBounds.IsEmpty) continue;

                            // Draw image on column 0 only
                            if (j == 0 && SmallImageList != null)
                            {
                                Image img = null;

                                if (!string.IsNullOrEmpty(item.ImageKey) && SmallImageList.Images.ContainsKey(item.ImageKey))
                                    img = SmallImageList.Images[item.ImageKey];
                                else if (item.ImageIndex >= 0 && item.ImageIndex < SmallImageList.Images.Count)
                                    img = SmallImageList.Images[item.ImageIndex];

                                if (img != null)
                                {
                                    // Image: vertically centered, left-aligned inside column
                                    int imgX = subBounds.Left + 4;
                                    int imgY = subBounds.Top + (subBounds.Height - img.Height) / 2;
                                    Rectangle imgRect = new(imgX, imgY, img.Width, img.Height);

                                    using (Bitmap grayscale = img.Grayscale()) G.DrawImage(grayscale, imgRect);

                                    // Shrink text bounds to start after image + 1px gap
                                    subBounds = new(imgRect.Right + 1, subBounds.Top, subBounds.Width - imgRect.Width - 4, subBounds.Height);
                                }
                                else
                                {
                                    // No image: add small left padding to match native indent
                                    subBounds = new(subBounds.Left + 2, subBounds.Top, subBounds.Width - 2, subBounds.Height);
                                }
                            }

                            string text = item.SubItems.Count > j ? item.SubItems[j].Text : string.Empty;

                            TextRenderer.DrawText(G, text, Font, subBounds, Program.Style.Schemes.Disabled.Colors.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
                        }
                    }

                    // Redraw column header dividers on body (vertical grid lines)
                    if (View == View.Details)
                    {
                        int x = 0;
                        using (Pen linePen = new(Program.Style.Schemes.Disabled.Colors.Line_Hover()))
                        {
                            for (int i = 0; i < Columns.Count - 1; i++)
                            {
                                x += Columns[i].Width;
                                G.DrawLine(linePen, x - 1, 0, x - 1, client.Height);
                            }
                        }
                    }
                }

                return; // already handled
            }

            base.WndProc(ref m);
        }

        #endregion
    }
}