using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("Themed CheckedListBox for WinPaletter UI")]
    public class CheckedListBox : System.Windows.Forms.CheckedListBox
    {
        private const int SB_VERT = 1;
        private const int SIF_RANGE = 0x0001;
        private const int SIF_PAGE = 0x0002;
        private const int WM_PAINT = 0x000F;
        private const int WM_ERASEBKGND = 0x0014;
        private const int LB_SETITEMHEIGHT = 0x01A0;

        // Size of the checkbox glyph area (matches ItemHeight rhythm).
        private const int CheckBoxSize = 16;
        // Left padding before the checkbox.
        private const int CheckBoxLeft = 4;
        // Gap between the checkbox right edge and the text.
        private const int CheckBoxTextGap = 6;

        private int _previousSelectedIndex = -1;

        private bool IsVerticalScrollBarVisible()
        {
            NativeMethods.User32.SCROLLINFO si = new();
            si.cbSize = Marshal.SizeOf(si);
            si.fMask = SIF_RANGE | SIF_PAGE;

            if (NativeMethods.User32.GetScrollInfo(Handle, SB_VERT, ref si))
            {
                return (si.nMax - si.nMin) >= si.nPage;
            }
            return false;
        }

        public CheckedListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.None;
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 24;
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
            CheckOnClick = true;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color BackColor { get => Color.Transparent; set { } }

        private int _itemHeight = 24;

        [Browsable(true), Category("Appearance"), Description("The height of each item in the list."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override int ItemHeight
        {
            get => _itemHeight;
            set
            {
                _itemHeight = value;
                // The base setter triggers RecreateHandle which resets ItemHeight to the font metric. Skip it entirely and write directly to the HWND when available.
                if (IsHandleCreated) NativeMethods.User32.SendMessage(Handle, LB_SETITEMHEIGHT, IntPtr.Zero, new IntPtr(_itemHeight));
            }
        }

        private static readonly TextureBrush NoiseHover = new(Resources.Noise.Fade(0.9f));

        private bool CanAnimate => !DesignMode && Program.Style.Animations && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private GraphicsPath _cachedRoundPath;
        private Size _cachedRoundPathSize;

        private Bitmap _parentSnapshot;
        private int _parentLevel;

        private GraphicsPath GetRoundPath()
        {
            if (_cachedRoundPath == null || _cachedRoundPathSize != Size)
            {
                _cachedRoundPath?.Dispose();
                Rectangle totalRect = new(0, 0, Width - 1, Height - 1);
                _cachedRoundPath = Program.Style.RoundedCorners ? totalRect.Round() : new GraphicsPath();
                _cachedRoundPathSize = Size;
            }
            return _cachedRoundPath;
        }

        private void RebuildParentSnapshot()
        {
            if (Parent == null || Width <= 0 || Height <= 0) return;

            _parentSnapshot?.Dispose();
            _parentSnapshot = new Bitmap(Width, Height);

            using (Graphics G = Graphics.FromImage(_parentSnapshot))
            {
                G.TranslateTransform(-Left, -Top);
                PaintEventArgs pea = new(G, new Rectangle(Left, Top, Width, Height));
                InvokePaintBackground(Parent, pea);
                InvokePaint(Parent, pea);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _cachedRoundPath?.Dispose();
            _cachedRoundPath = null;
            RebuildParentSnapshot();
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            RebuildParentSnapshot();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // ItemHeight assignment is ignored by CheckedListBox after handle creation because
            // the base class recalculates it from font metrics internally. The only reliable way
            // to override it is to send LB_SETITEMHEIGHT directly to the underlying HWND.
            // wParam = 0 (all items share the same height in OwnerDrawFixed mode).
            NativeMethods.User32.SendMessage(Handle, LB_SETITEMHEIGHT, IntPtr.Zero, new IntPtr(_itemHeight));
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            _parentLevel = this.Level();

            if (Parent != null)
                Parent.Invalidated += (s, args) => RebuildParentSnapshot();

            RebuildParentSnapshot();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if (_previousSelectedIndex >= 0 && _previousSelectedIndex < Items.Count) Invalidate(GetItemRectangle(_previousSelectedIndex));
            if (SelectedIndex >= 0 && SelectedIndex < Items.Count) Invalidate(GetItemRectangle(SelectedIndex));

            _previousSelectedIndex = SelectedIndex;
        }

        protected override void OnItemCheck(ItemCheckEventArgs ice)
        {
            base.OnItemCheck(ice);

            // Invalidate only the toggled item so the checkbox redraws without a full repaint.
            if (ice.Index >= 0 && ice.Index < Items.Count)
                Invalidate(GetItemRectangle(ice.Index));
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || Items.Count == 0) return;

            Graphics G = e.Graphics;
            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath round = GetRoundPath();

            // Blit the stable parent snapshot as background.
            if (_parentSnapshot != null)
            {
                GraphicsState snapState = G.Save();
                G.SetClip(e.Bounds);

                if (e.Index == Items.Count - 1)
                {
                    Rectangle rest = new(0, GetItemRectangle(e.Index).Bottom, Width, Height);
                    G.SetClip(rest, CombineMode.Union);
                }

                G.DrawImage(_parentSnapshot, Point.Empty);
                G.Restore(snapState);
            }

            // Theme background.
            Color backColor = scheme.Colors.Back(_parentLevel);
            if (e.Index == 0 || e.Index == Items.Count - 1)
            {
                using (SolidBrush bgBrush = new(backColor))
                {
                    GraphicsState inner = G.Save();

                    Rectangle fillArea = e.Bounds;
                    if (e.Index == Items.Count - 1)
                        fillArea = Rectangle.Union(e.Bounds, new Rectangle(0, e.Bounds.Bottom, Width, Height - e.Bounds.Bottom));

                    G.SetClip(fillArea);
                    G.FillPath(bgBrush, round);
                    G.Restore(inner);
                }
            }
            else
            {
                using (SolidBrush bgBrush = new(backColor))
                {
                    Rectangle beyoundBackRect = new(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width + 2, e.Bounds.Height + 2);
                    G.FillRectangle(bgBrush, beyoundBackRect);
                }
            }

            // Selection highlight.
            int padding = CheckBoxLeft + CheckBoxSize + CheckBoxTextGap / 2 + 1;
            Rectangle selectionRect = new(e.Bounds.X + padding, e.Bounds.Y + 3, e.Bounds.Width - padding - CheckBoxLeft - 1, e.Bounds.Height - 6);

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            if (isSelected)
            {
                GraphicsState selState = G.Save();
                G.SetClip(round, CombineMode.Intersect);

                using (LinearGradientBrush selectedBrush = new(selectionRect, scheme.Colors.Line(_parentLevel), scheme.Colors.Line_Hover(_parentLevel), LinearGradientMode.ForwardDiagonal))
                using (LinearGradientBrush selectedBrushPen = new(selectionRect, scheme.Colors.Line_Hover(_parentLevel), scheme.Colors.ForeColor, LinearGradientMode.ForwardDiagonal))
                using (Pen P = new(selectedBrushPen))
                {
                    G.FillRoundedRect(selectedBrush, selectionRect);
                    G.FillRoundedRect(NoiseHover, selectionRect);
                    G.DrawRoundedRect(P, selectionRect);
                }

                G.Restore(selState);
            }

            // Checkbox glyph.
            CheckState checkState = GetItemCheckState(e.Index);
            int checkTop = e.Bounds.Y + (e.Bounds.Height - CheckBoxSize) / 2;
            Rectangle checkRect = new(e.Bounds.X + CheckBoxLeft, checkTop, CheckBoxSize, CheckBoxSize);
            DrawCheckGlyph(G, scheme, checkRect, checkState, isSelected);

            // Text rendering.
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;
            int textLeft = e.Bounds.X + CheckBoxLeft + CheckBoxSize + CheckBoxTextGap;
            Rectangle textRect = new(textLeft, e.Bounds.Y, e.Bounds.Width - textLeft + e.Bounds.X - 4, e.Bounds.Height);
            TextRenderer.DrawText(G, GetItemText(Items[e.Index]), Font, textRect, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
        }

        // Draws a themed checkbox glyph consistent with the WinPaletter scheme.
        // Checked    -> filled accent box with a checkmark.
        // Indeterminate -> filled accent box with a dash.
        // Unchecked  -> bordered empty box.
        private void DrawCheckGlyph(Graphics G, Config.Scheme scheme, Rectangle rect, CheckState state, bool itemSelected)
        {
            GraphicsPath boxPath = rect.Round(3);

            if (state == CheckState.Checked || state == CheckState.Indeterminate)
            {
                // Filled background using the accent gradient, clipped to the rounded box.
                using (LinearGradientBrush fillBrush = new(rect, scheme.Colors.Line_Checked, scheme.Colors.Line_Checked_Hover, LinearGradientMode.ForwardDiagonal))
                {
                    G.FillPath(fillBrush, boxPath);
                }

                // Noise texture overlay for consistency with the selection style.
                G.FillPath(NoiseHover, boxPath);

                // Border using the accent pen.
                using (LinearGradientBrush borderBrush = new(rect, scheme.Colors.Line_Checked_Hover, scheme.Colors.ForeColor_Accent, LinearGradientMode.ForwardDiagonal))
                using (Pen borderPen = new(borderBrush))
                {
                    G.DrawPath(borderPen, boxPath);
                }

                // Glyph: checkmark or dash, drawn in the accent foreground color.
                Color glyphColor = scheme.Colors.ForeColor_Accent;

                if (state == CheckState.Checked)
                {
                    // Checkmark: two-segment polyline.
                    int cx = rect.X;
                    int cy = rect.Y;
                    int w = rect.Width;
                    int h = rect.Height;

                    // Points are proportional to the glyph rect so they scale with CheckBoxSize.
                    PointF p1 = new(cx + w * 0.18f, cy + h * 0.50f);
                    PointF p2 = new(cx + w * 0.42f, cy + h * 0.72f);
                    PointF p3 = new(cx + w * 0.82f, cy + h * 0.28f);

                    using (Pen glyphPen = new(glyphColor, 1.7f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
                    {
                        G.DrawLines(glyphPen, new PointF[] { p1, p2, p3 });
                    }
                }
                else
                {
                    // Indeterminate: horizontal dash.
                    float dashY = rect.Y + rect.Height / 2f;
                    float dashX1 = rect.X + rect.Width * 0.22f;
                    float dashX2 = rect.X + rect.Width * 0.78f;

                    using (Pen glyphPen = new(glyphColor, 1.7f) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                    {
                        G.DrawLine(glyphPen, dashX1, dashY, dashX2, dashY);
                    }
                }
            }
            else
            {
                // Unchecked: transparent fill + themed border.
                Color borderColor = itemSelected ? scheme.Colors.Line_Checked : scheme.Colors.Line_Hover(_parentLevel);

                using (SolidBrush emptyFill = new(Color.FromArgb(30, scheme.Colors.Back(_parentLevel))))
                {
                    G.FillPath(emptyFill, boxPath);
                }

                using (Pen borderPen = new(borderColor))
                {
                    G.DrawPath(borderPen, boxPath);
                }
            }

            boxPath.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ERASEBKGND)
            {
                m.Result = (IntPtr)1;
                return;
            }

            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                using (Graphics G = Graphics.FromHwnd(Handle))
                {
                    G.SmoothingMode = SmoothingMode.AntiAlias;

                    int scrollWidth = IsVerticalScrollBarVisible() ? SystemInformation.VerticalScrollBarWidth : 0;
                    int drawWidth = Width - scrollWidth;

                    Rectangle rect = new(0, 0, drawWidth - 1, Height - 1);

                    Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

                    if (Items.Count == 0)
                    {
                        if (_parentSnapshot != null)
                        {
                            G.DrawImage(_parentSnapshot, Point.Empty);
                        }
                        else if (Parent != null)
                        {
                            GraphicsState state = G.Save();
                            G.TranslateTransform(-Left, -Top);
                            PaintEventArgs pea = new(G, new Rectangle(Left, Top, Parent.Width, Parent.Height));
                            InvokePaintBackground(Parent, pea);
                            InvokePaint(Parent, pea);
                            G.Restore(state);
                        }

                        GraphicsPath round = GetRoundPath();
                        using (SolidBrush bgBrush = new(scheme.Colors.Back(_parentLevel)))
                        {
                            G.FillPath(bgBrush, round);
                        }
                    }

                    using (Pen P = new(scheme.Colors.Line_Hover(_parentLevel)))
                    {
                        G.DrawRoundedRect(P, rect);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cachedRoundPath?.Dispose();
                _cachedRoundPath = null;
                _parentSnapshot?.Dispose();
                _parentSnapshot = null;
            }
            base.Dispose(disposing);
        }
    }
}