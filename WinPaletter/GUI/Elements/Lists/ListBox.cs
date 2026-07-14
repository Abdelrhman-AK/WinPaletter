using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("Themed ListBox for WinPaletter UI")]
    public class ListBox : System.Windows.Forms.ListBox
    {
        private const int SB_VERT = 1;
        private const int SIF_RANGE = 0x0001;
        private const int SIF_PAGE = 0x0002;
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

        public ListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.None;
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 24;
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color BackColor { get => Color.Transparent; set { } }

        private static readonly TextureBrush NoiseHover = new(Resources.Noise.Fade(0.9f));

        private bool CanAnimate => !DesignMode && Program.Style.Animations && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private GraphicsPath _cachedRoundPath;
        private Size _cachedRoundPathSize;

        // Cached snapshot of the parent region behind this control.
        // Rebuilt on move, resize, or parent invalidation — never inside OnDrawItem.
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

            // Invalidate only the item that lost selection and the item that gained it.
            // Calling Invalidate() on the whole control forces all items to redraw sequentially via WM_DRAWITEM, which causes visible tearing between passes.
            if (_previousSelectedIndex >= 0 && _previousSelectedIndex < Items.Count) Invalidate(GetItemRectangle(_previousSelectedIndex));
            if (SelectedIndex >= 0 && SelectedIndex < Items.Count) Invalidate(GetItemRectangle(SelectedIndex));

            _previousSelectedIndex = SelectedIndex;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || Items.Count == 0) return;

            Graphics G = e.Graphics;
            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath round = GetRoundPath();

            // TRANSPARENCY: Blit the stable parent snapshot instead of re-invoking InvokePaintBackground/InvokePaint per item. This is what eliminates the
            // selection flicker — the snapshot does not change during a paint cycle, so all items see the same background without any tearing between them.
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
            Rectangle selectionRect = new(e.Bounds.X + 4, e.Bounds.Y + 3, e.Bounds.Width - 9, e.Bounds.Height - 6);
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            if (isSelected)
            {
                GraphicsState selState = G.Save();
                G.SetClip(round, CombineMode.Intersect);

                using (LinearGradientBrush selectedBrush = new(selectionRect, scheme.Colors.Line_Checked, scheme.Colors.Line_Checked_Hover, LinearGradientMode.ForwardDiagonal))
                using (LinearGradientBrush selectedBrushPen = new(selectionRect, scheme.Colors.Line_Checked_Hover, scheme.Colors.ForeColor_Accent, LinearGradientMode.ForwardDiagonal))
                using (Pen P = new(selectedBrushPen))
                {
                    G.FillRoundedRect(selectedBrush, selectionRect);
                    G.FillRoundedRect(NoiseHover, selectionRect);
                    G.DrawRoundedRect(P, selectionRect);
                }

                G.Restore(selState);
            }

            // Text rendering.
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;
            Color textColor = isSelected ? scheme.Colors.ForeColor_Accent : ForeColor;
            Rectangle textRect = new(e.Bounds.X + 5, e.Bounds.Y, e.Bounds.Width - 10, e.Bounds.Height);
            TextRenderer.DrawText(G, GetItemText(Items[e.Index]), Font, textRect, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)User32.WindowsMessage.EraseBkgnd)
            {
                m.Result = (IntPtr)1;
                return;
            }

            base.WndProc(ref m);

            if (m.Msg == (int)User32.WindowsMessage.Paint)
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