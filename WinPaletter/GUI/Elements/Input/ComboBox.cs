using FluentTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("Themed ComboBox for WinPaletter UI")]
    public class ComboBox : System.Windows.Forms.ComboBox
    {
        public ComboBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);

            // Do NOT set DoubleBuffered = true together with AllPaintingInWmPaint + UserPaint; it conflicts and causes the white-flash.
            DoubleBuffered = false;

            BackColor = Color.Transparent;
            Size = new Size(190, 27);
            DrawMode = DrawMode.OwnerDrawVariable;
            ItemHeight = 20;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9f);

            _alpha = 0;
            _alpha2 = 255;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color BackColor { get; set; }

        private static TextureBrush _noiseBrush = new(Resources.Noise.Fade(0.3f));
        private TextureBrush _noiseBrush2 = new(Resources.Noise.Fade(0.9f));

        private bool CanAnimate => !DesignMode && Program.Style.Animations && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private int _parentLevel;

        private IntPtr _listBoxBrush = IntPtr.Zero;
        private IntPtr _lastThemedListBox = IntPtr.Zero;

        protected override void WndProc(ref Message m)
        {
            const int WM_ERASEBKGND = 0x0014;
            const int WM_CTLCOLORLISTBOX = 0x0134;

            if (m.Msg == WM_ERASEBKGND)
            {
                m.Result = (IntPtr)1;
                return;
            }

            // Let base handle all messages first
            if (m.Msg == WM_CTLCOLORLISTBOX)
            {
                // Call base first to apply dark mode theme
                base.WndProc(ref m);

                // Apply dark mode to the list box and its scrollbar
                ApplyDarkModeToListBox(m.LParam);

                // Then override the background/foreground colors
                IntPtr hdc = m.WParam;
                Config.Scheme scheme = Program.Style.Schemes.Main;
                Color bgColor = scheme.Colors.Back(_parentLevel);

                NativeMethods.GDI32.SetBkColor(hdc, ColorTranslator.ToWin32(bgColor));
                NativeMethods.GDI32.SetTextColor(hdc, ColorTranslator.ToWin32(ForeColor));

                // Delete previous brush to prevent memory leak
                if (_listBoxBrush != IntPtr.Zero)
                {
                    NativeMethods.GDI32.DeleteObject(_listBoxBrush);
                }

                _listBoxBrush = NativeMethods.GDI32.CreateSolidBrush(ColorTranslator.ToWin32(bgColor));
                m.Result = _listBoxBrush;
                return;
            }

            base.WndProc(ref m);
        }

        private void ApplyDarkModeToListBox(IntPtr listBoxHwnd)
        {
            if (listBoxHwnd == IntPtr.Zero || listBoxHwnd == _lastThemedListBox) return;
            _lastThemedListBox = listBoxHwnd;

            SetControlTheme(listBoxHwnd, Program.Style.DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Explorer);

            // Also theme the scrollbar that lives inside the list pop-up.
            NativeMethods.User32.EnumChildWindows(listBoxHwnd, (child, _) =>
            {
                var sb = new System.Text.StringBuilder(64);
                NativeMethods.User32.GetClassName(child, sb, sb.Capacity);
                if (sb.ToString().Equals("ScrollBar", StringComparison.OrdinalIgnoreCase))
                {
                    SetControlTheme(child, Program.Style.DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Explorer);
                }
                return true;
            }, IntPtr.Zero);
        }

        protected override void Dispose(bool disposing)
        {
            // Clean up the brush when the control is disposed
            if (_listBoxBrush != IntPtr.Zero)
            {
                NativeMethods.GDI32.DeleteObject(_listBoxBrush);
                _listBoxBrush = IntPtr.Zero;
            }

            if (disposing)
            {
                _noiseBrush?.Dispose();
                _noiseBrush2?.Dispose();
            }

            base.Dispose(disposing);
        }

        private readonly Point[] _trianglePoints = new Point[3];

        private void DrawTriangle(Color color, Point p1, Point p2, Point p3, Graphics g)
        {
            _trianglePoints[0] = p1;
            _trianglePoints[1] = p2;
            _trianglePoints[2] = p3;
            using (SolidBrush br = new(color))
            {
                g.FillPolygon(br, _trianglePoints);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) DroppedDown = !DroppedDown;
            base.OnKeyDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (CanAnimate) Transition.With(this, nameof(alpha), 255).CriticalDamp(Program.AnimationSpan);
            else alpha = 255;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (CanAnimate) Transition.With(this, nameof(alpha), 0).CriticalDamp(Program.AnimationSpan);
            else alpha = 0;

            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (CanAnimate) Transition.With(this, nameof(alpha), 0).CriticalDamp(Program.AnimationSpan);
            else alpha = 0;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (SelectedIndex < Items.Count - 1 && e.Delta <= -240) SelectedIndex += 1;
            }
            else
            {
                if (SelectedIndex > 0 && e.Delta >= 240) SelectedIndex -= 1;
            }

            base.OnMouseWheel(e);
        }

        protected override void OnDropDown(EventArgs e)
        {
            if (CanAnimate) Transition.With(this, nameof(alpha2), 0).CriticalDamp(Program.AnimationSpan);
            else alpha2 = 0;

            base.OnDropDown(e);
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            if (CanAnimate) Transition.With(this, nameof(alpha2), 255).CriticalDamp(Program.AnimationSpan);
            else alpha2 = 255;

            _lastThemedListBox = IntPtr.Zero; // Reset so next open is re-themed.
            base.OnDropDownClosed(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            _parentLevel = this.Level();
        }

        private int _alpha;
        private int _alpha2;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha2
        {
            get => _alpha2;
            set { _alpha2 = value; Invalidate(); }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) { base.OnDrawItem(e); return; }

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            // Fill background — enlarge by 1 px on each side to avoid hairline gaps.
            Rectangle bgRect = new(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width + 2, e.Bounds.Height + 2);

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            bool isFocused = (e.State & DrawItemState.Focus) == DrawItemState.Focus;
            bool isHot = (e.State & DrawItemState.HotLight) == DrawItemState.HotLight;

            Rectangle wideRect = new(e.Bounds.X - 3, e.Bounds.Y - 3, e.Bounds.Width + 6, e.Bounds.Height + 6);
            using (SolidBrush br = new(scheme.Colors.Back(_parentLevel)))
            {
                G.FillRectangle(br, wideRect);
            }

            if (isSelected)
            {
                Rectangle hoverRect = e.Bounds;
                hoverRect.Inflate(-1, 0);

                using (LinearGradientBrush lgb0 = new(hoverRect, scheme.Colors.Line_Checked, scheme.Colors.Line_Checked_Hover, LinearGradientMode.ForwardDiagonal))
                using (LinearGradientBrush lgb1 = new(hoverRect, scheme.Colors.Accent, scheme.Colors.ForeColor_Accent, LinearGradientMode.ForwardDiagonal))
                using (Pen P = new(lgb1))
                {
                    G.FillRoundedRect(lgb0, hoverRect);
                    G.FillRoundedRect(_noiseBrush2, hoverRect);
                    G.DrawRoundedRectBeveled(P, hoverRect);
                }
            }

            Rectangle textRect = new(e.Bounds.X + 2, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height);

            using (StringFormat sf = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
            using (SolidBrush br = new(ForeColor))
            {
                G.DrawString(GetItemText(Items[e.Index]), e.Font, br, textRect, sf);
            }

            Invalidate();
            base.OnDrawItem(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Paint the parent chain background into the buffer (not onto the screen DC). Using a PaintEventArgs that wraps the buffer Graphics keeps InvokePaintBackground from ever touching the real HDC.
            using (PaintEventArgs bufferBgArgs = new(G, e.ClipRectangle))
            {
                InvokePaintBackground(this, bufferBgArgs);
            }

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle outerRect = new(0, 0, Width - 1, Height - 1);
            Rectangle innerRect = new(1, 1, Width - 3, Height - 3);

            // For DropDown style the native edit box lives inside the bounds; reserve space on the left so owner-drawn chrome does not overlap it.
            Rectangle textRect = DropDownStyle == ComboBoxStyle.DropDown ? 
                new Rectangle(0, 0, Width - 1, Height - 1) /* edit box handles text */  :
                new Rectangle(5, 0, Width - 1, Height - 1);

            Color fadeInColor = Color.FromArgb(_alpha, scheme.Colors.Line_Checked);
            Color fadeOutColor = Color.FromArgb(255 - _alpha, scheme.Colors.Line_Hover(_parentLevel + 5));

            Color backColor0 = Program.Style.DarkMode ? scheme.Colors.Back_Hover(_parentLevel) : scheme.Colors.Back(_parentLevel);
            Color backColor1 = Program.Style.DarkMode ? scheme.Colors.Back(_parentLevel) : scheme.Colors.Back_Hover(_parentLevel);
            Color hoverColor0 = Program.Style.DarkMode ? Color.FromArgb(_alpha, scheme.Colors.Back_Checked) : Color.FromArgb(_alpha, scheme.Colors.Back_Checked_Hover);
            Color hoverColor1 = Program.Style.DarkMode ? Color.FromArgb(_alpha, scheme.Colors.Back_Checked_Hover) : Color.FromArgb(_alpha, scheme.Colors.Back_Checked);

            using (LinearGradientBrush lgb0 = new(innerRect, backColor0, backColor1, LinearGradientMode.Vertical))
            using (LinearGradientBrush lgb1 = new(outerRect, hoverColor0, hoverColor1, LinearGradientMode.Horizontal))
            {
                G.FillRoundedRect(lgb0, innerRect);
                G.FillRoundedRect(lgb1, outerRect);
            }

            G.FillRoundedRect(_noiseBrush, innerRect);

            using (Pen p = new(fadeInColor)) { G.DrawRoundedRectBeveled(p, outerRect); }
            using (Pen p = new(fadeOutColor)) { G.DrawRoundedRectBeveled(p, innerRect); }

            if (Focused) G.DrawRoundedRect(scheme.Pens.Line_Checked, innerRect);

            // Draw the drop arrow only when the style uses one.
            // For Simple style the list is always shown; no arrow is needed.
            if (DropDownStyle != ComboBoxStyle.Simple)
            {
                int arrowH = 5;
                int arrowY1 = (Height - arrowH) / 2 - 1;
                int arrowY2 = arrowY1 + arrowH;

                Point topLeft = new(Width - 18, arrowY1);
                Point topRight = new(Width - 10, arrowY1);
                Point bottomLeft = new(Width - 18, arrowY2);
                Point bottomRight = new(Width - 10, arrowY2);
                Point centerBot = new(Width - 14, arrowY2);
                Point centerTop = new(Width - 14, arrowY1);

                Color tri1 = Color.FromArgb(255 - _alpha2, !Focused ? ForeColor : scheme.Colors.Line_Checked_Hover);
                DrawTriangle(tri1, topLeft, centerBot, topRight, G);

                Color tri2 = Color.FromArgb(_alpha2, scheme.Colors.Line_Checked_Hover);
                DrawTriangle(tri2, bottomLeft, centerTop, bottomRight, G);
            }

            // Draw selected text only for DropDownList and Simple styles.
            // For DropDown the native edit control renders the text itself.
            if (DropDownStyle != ComboBoxStyle.DropDown)
            {
                using (StringFormat sf = new()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                })
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString((SelectedItem ?? string.Empty).ToString(), Font, br, textRect, sf);
                }
            }
        }
    }
}