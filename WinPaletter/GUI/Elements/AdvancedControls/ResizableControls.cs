using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.Controllers
{
    public class ResizableControlHook : IDisposable
    {
        private readonly Control control;
        private bool isResizing;
        private Point lastMousePos;
        private readonly int borderWidth = 4;
        private bool isMouseOver;

        public ResizableControlHook(Control control)
        {
            this.control = control;
            control.MouseDown += Control_MouseDown;
            control.MouseMove += Control_MouseMove;
            control.MouseUp += Control_MouseUp;
            control.MouseEnter += Control_MouseEnter;
            control.MouseLeave += Control_MouseLeave;
            control.Paint += Control_Paint;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsOnBorder(e.Location))
            {
                isResizing = true;
                lastMousePos = e.Location;
                control.Capture = true;
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;

                if (IsResizableFromLeft())
                {
                    control.Width -= deltaX;
                    control.Left += deltaX;
                }

                if (IsResizableFromTop())
                {
                    control.Height -= deltaY;
                    control.Top += deltaY;
                }

                if (IsResizableFromRight())
                    control.Width += deltaX;

                if (IsResizableFromBottom())
                    control.Height += deltaY;

                lastMousePos = e.Location;
            }
            else
            {
                UpdateCursor(e.Location);
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            control.Capture = false;
        }

        private void UpdateCursor(Point mouseLocation)
        {
            if (IsResizableFromLeft() && mouseLocation.X <= borderWidth)
            {
                control.Cursor = Cursors.SizeWE;
            }
            else if (IsResizableFromTop() && mouseLocation.Y <= borderWidth)
            {
                control.Cursor = Cursors.SizeNS;
            }
            else if (IsResizableFromRight() && mouseLocation.X >= control.Width - borderWidth)
            {
                control.Cursor = Cursors.SizeWE;
            }
            else if (IsResizableFromBottom() && mouseLocation.Y >= control.Height - borderWidth)
            {
                control.Cursor = Cursors.SizeNS;
            }
            else
            {
                control.Cursor = Cursors.Default;
            }
        }

        private bool IsOnBorder(Point mouseLocation)
        {
            return IsResizableFromLeft() && mouseLocation.X <= borderWidth ||
                   IsResizableFromTop() && mouseLocation.Y <= borderWidth ||
                   IsResizableFromRight() && mouseLocation.X >= control.Width - borderWidth ||
                   IsResizableFromBottom() && mouseLocation.Y >= control.Height - borderWidth;
        }

        private bool IsResizableFromLeft()
        {
            return (control.Dock & DockStyle.Left) != DockStyle.Left;
        }

        private bool IsResizableFromTop()
        {
            return (control.Dock & DockStyle.Top) != DockStyle.Top;
        }

        private bool IsResizableFromRight()
        {
            return (control.Dock & DockStyle.Right) != DockStyle.Right;
        }

        private bool IsResizableFromBottom()
        {
            return (control.Dock & DockStyle.Bottom) != DockStyle.Bottom;
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            isMouseOver = true;
            control.Invalidate();
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            isMouseOver = false;
            control.Invalidate();
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            PaintHoveredBorder(e);
        }

        public void PaintHoveredBorder(PaintEventArgs e)
        {
            if (isMouseOver)
            {
                using (Pen pen = new(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
                {
                    e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, control.Width - 1, control.Height - 1));
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, control.Width - 1, control.Height - 1));
                }
            }
        }

        public void Dispose()
        {
            control.MouseDown -= Control_MouseDown;
            control.MouseMove -= Control_MouseMove;
            control.MouseUp -= Control_MouseUp;
            control.MouseEnter -= Control_MouseEnter;
            control.MouseLeave -= Control_MouseLeave;
            control.Paint -= Control_Paint;
        }
    }
}

namespace WinPaletter.UI.WP
{
    public class ResizableHScrollBar : HScrollBar
    {
        private const int WM_NCHITTEST = 0x84;

        private enum HitTestValues
        {
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTBOTTOM = 15,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && ResizeEnabled)
            {
                Point cursorPos = PointToClient(Cursor.Position);

                if (Dock == DockStyle.Top)
                {
                    if (IsOnBottomBorder(cursorPos)) m.Result = (IntPtr)HitTestValues.HTBOTTOM;
                }
                else if (Dock == DockStyle.Bottom)
                {
                    if (IsOnTopBorder(cursorPos)) m.Result = (IntPtr)HitTestValues.HTTOP;
                }
                else
                {
                    if (IsOnLeftBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTLEFT;
                    }
                    else if (IsOnRightBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTRIGHT;
                    }
                    else if (IsOnTopBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTTOP;
                    }
                    else if (IsOnBottomBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTBOTTOM;
                    }
                    else if (IsOnTopLeftBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTTOPLEFT;
                    }
                    else if (IsOnTopRightBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTTOPRIGHT;
                    }
                    else if (IsOnBottomLeftBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTBOTTOMLEFT;
                    }
                    else if (IsOnBottomRightBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTBOTTOMRIGHT;
                    }
                }
            }
        }

        private bool IsOnLeftBorder(Point cursorPos)
        {
            return cursorPos.X <= ResizeBorderWidth;
        }

        private bool IsOnRightBorder(Point cursorPos)
        {
            return cursorPos.X >= Width - ResizeBorderWidth;
        }

        private bool IsOnTopBorder(Point cursorPos)
        {
            return cursorPos.Y <= ResizeBorderHeight;
        }

        private bool IsOnBottomBorder(Point cursorPos)
        {
            return cursorPos.Y >= Height - ResizeBorderHeight;
        }

        private bool IsOnTopLeftBorder(Point cursorPos)
        {
            return IsOnLeftBorder(cursorPos) && IsOnTopBorder(cursorPos);
        }

        private bool IsOnTopRightBorder(Point cursorPos)
        {
            return IsOnRightBorder(cursorPos) && IsOnTopBorder(cursorPos);
        }

        private bool IsOnBottomLeftBorder(Point cursorPos)
        {
            return IsOnLeftBorder(cursorPos) && IsOnBottomBorder(cursorPos);
        }

        private bool IsOnBottomRightBorder(Point cursorPos)
        {
            return IsOnRightBorder(cursorPos) && IsOnBottomBorder(cursorPos);
        }

        public bool ResizeEnabled { get; set; } = true;

        private const int ResizeBorderWidth = 5; // Adjust the value as needed
        private const int ResizeBorderHeight = 5; // Adjust the value as needed
    }

    public class ResizableVScrollBar : VScrollBar
    {
        private const int WM_NCHITTEST = 0x84;

        private enum HitTestValues
        {
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTBOTTOM = 15,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && ResizeEnabled)
            {
                Point cursorPos = PointToClient(Cursor.Position);

                if (Dock == DockStyle.Left)
                {
                    if (IsOnRightBorder(cursorPos)) m.Result = (IntPtr)HitTestValues.HTRIGHT;
                }
                else if (Dock == DockStyle.Right)
                {
                    if (IsOnLeftBorder(cursorPos)) m.Result = (IntPtr)HitTestValues.HTLEFT;
                }
                else
                {
                    if (IsOnLeftBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTLEFT;
                    }
                    else if (IsOnRightBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTRIGHT;
                    }
                    else if (IsOnTopBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTTOP;
                    }
                    else if (IsOnBottomBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTBOTTOM;
                    }
                    else if (IsOnTopLeftBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTTOPLEFT;
                    }
                    else if (IsOnTopRightBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTTOPRIGHT;
                    }
                    else if (IsOnBottomLeftBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTBOTTOMLEFT;
                    }
                    else if (IsOnBottomRightBorder(cursorPos))
                    {
                        m.Result = (IntPtr)HitTestValues.HTBOTTOMRIGHT;
                    }
                }
            }
        }

        private bool IsOnLeftBorder(Point cursorPos)
        {
            return cursorPos.X <= ResizeBorderWidth;
        }

        private bool IsOnRightBorder(Point cursorPos)
        {
            return cursorPos.X >= Width - ResizeBorderWidth;
        }

        private bool IsOnTopBorder(Point cursorPos)
        {
            return cursorPos.Y <= ResizeBorderHeight;
        }

        private bool IsOnBottomBorder(Point cursorPos)
        {
            return cursorPos.Y >= Height - ResizeBorderHeight;
        }

        private bool IsOnTopLeftBorder(Point cursorPos)
        {
            return IsOnLeftBorder(cursorPos) && IsOnTopBorder(cursorPos);
        }

        private bool IsOnTopRightBorder(Point cursorPos)
        {
            return IsOnRightBorder(cursorPos) && IsOnTopBorder(cursorPos);
        }

        private bool IsOnBottomLeftBorder(Point cursorPos)
        {
            return IsOnLeftBorder(cursorPos) && IsOnBottomBorder(cursorPos);
        }

        private bool IsOnBottomRightBorder(Point cursorPos)
        {
            return IsOnRightBorder(cursorPos) && IsOnBottomBorder(cursorPos);
        }

        public bool ResizeEnabled { get; set; } = true;

        private const int ResizeBorderWidth = 5; // Adjust the value as needed
        private const int ResizeBorderHeight = 5; // Adjust the value as needed
    }
}