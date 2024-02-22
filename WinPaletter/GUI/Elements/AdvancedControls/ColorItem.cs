using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.Controllers
{
    [DefaultEvent("Click")]
    public class ColorItem : Panel
    {
        public ColorItem()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Rect = new(0, 0, Width - 1, Height - 1);
            RectInner = new(1, 1, Width - 3, Height - 3);
            Rect_DefColor = new(7, (Height - 7) / 2, 7, 7);
            Timer2 = new() { Enabled = false, Interval = 1 };

            alpha = 0;
            Timer2_factor = 0;

            Text = string.Empty;
            ColorsHistory.Clear();
            Timer2.Tick += Timer2_Tick;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public bool ColorPickerOpened = false;
        public List<Color> ColorsHistory = new();
        private Color LineColor;
        public bool PauseColorsHistory = false;

        private Rectangle Rect;
        private Rectangle RectInner;
        private Rectangle Rect_DefColor;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties        
        public Color DefaultBackColor { get; set; } = Color.Black;
        public bool DontShowInfo { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        [DefaultValue("")]
        public override string Text { get; set; } = string.Empty;

        #endregion

        #region Drag and drop
        private bool SwapNotCopy = false;
        private bool InitializeDrag = false;
        private Point mousePosition_beforeDrag, mousePosition_afterDrag;
        private AfterDropEffects AfterDropEffect = AfterDropEffects.None;
        private bool DragDefaultColor = false;
        private Color DraggedColor;
        private bool DragDropMouseHovering = false;
        private bool MakeAfterDropEffect = false;
        private Color BeforeDropColor;
        private Point BeforeDropMousePosition;
        private bool HoverOverDefColorDot = false;

        public enum AfterDropEffects
        {
            None,
            Invert,
            Darker,
            Lighter,
            Mix
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            mousePosition_afterDrag = MousePosition;

            if (InitializeDrag && mousePosition_beforeDrag != mousePosition_afterDrag)
            {
                DragDefaultColor = CanRaiseEventsForDefColorDot();
                DoDragDrop(this, DragDropEffects.Copy);
            }

            if (!DesignMode && Program.Settings.NerdStats.DotDefaultChangedIndicator)
            {
                HoverOverDefColorDot = CanRaiseEventsForDefColorDot();
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (AllowDrop && Program.Settings.NerdStats.DragAndDrop)
            {

                BeforeDropColor = BackColor;
                BeforeDropMousePosition = PointToClient(MousePosition);
                Timer2_factor = 0;
                MakeAfterDropEffect = true;
                Timer2.Enabled = true;
                Timer2.Start();

                if (!SwapNotCopy)
                {
                    e.Effect = DragDropEffects.Copy;
                    if (!((ColorItem)e.Data.GetData(GetType().FullName)).DragDefaultColor)
                    {
                        BackColor = ((ColorItem)e.Data.GetData(GetType().FullName)).BackColor;
                    }
                    else
                    {
                        BackColor = ((ColorItem)e.Data.GetData(GetType().FullName)).DefaultBackColor;
                    }
                }

                else
                {
                    e.Effect = DragDropEffects.Link;
                    Color Color_From = ((ColorItem)e.Data.GetData(GetType().FullName)).BackColor;

                    Color Color_To;
                    switch (AfterDropEffect)
                    {
                        case AfterDropEffects.Invert:
                            {
                                Color_To = base.BackColor.Invert();
                                break;
                            }

                        case AfterDropEffects.Darker:
                            {
                                Color_To = base.BackColor.Dark();
                                break;
                            }

                        case AfterDropEffects.Lighter:
                            {
                                Color_To = base.BackColor.Light();
                                break;
                            }

                        default:
                            {
                                Color_To = BackColor;
                                break;
                            }

                    }

                    BackColor = Color_From;
                    ((ColorItem)e.Data.GetData(GetType().FullName)).BackColor = Color_To;

                }

                switch (AfterDropEffect)
                {
                    case AfterDropEffects.Invert:
                        {
                            BackColor = base.BackColor.Invert();
                            break;
                        }

                    case AfterDropEffects.Darker:
                        {
                            BackColor = base.BackColor.Dark();
                            break;
                        }

                    case AfterDropEffects.Lighter:
                        {
                            BackColor = base.BackColor.Light();
                            break;
                        }

                    case AfterDropEffects.Mix:
                        {
                            BackColor = base.BackColor.Blend(BeforeDropColor, 100d);
                            break;
                        }

                }

                ColorClipboard.CopiedColor = BackColor;

                base.OnDragDrop(e);
            }

            DragDropMouseHovering = false;
            Refresh();

        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (AllowDrop && Program.Settings.NerdStats.DragAndDrop)
            {
                DragDropMouseHovering = true;

                e.Effect = DragDropEffects.Copy;

                SwapNotCopy = false;
                AfterDropEffect = AfterDropEffects.None;

                if ((e.KeyState & 32) == 32)
                {
                    // Alt is pressed
                    AfterDropEffect = AfterDropEffects.Invert;
                }

                if ((e.KeyState & 16) == 16)
                {
                    // Middle mouse button is pressed

                }

                if ((e.KeyState & 8) == 8)
                {
                    // Ctrl is pressed
                    AfterDropEffect = AfterDropEffects.Darker;

                }

                if ((e.KeyState & 4) == 4)
                {
                    // Shift is pressed
                    AfterDropEffect = AfterDropEffects.Lighter;

                }

                if ((e.KeyState & 32 + 8) == 32 + 8)
                {
                    // Ctrl+Alt are pressed
                    AfterDropEffect = AfterDropEffects.Mix;

                }

                if ((e.KeyState & 2) == 2)
                {
                    // Right mouse button is pressed
                    SwapNotCopy = true;
                }

                if ((e.KeyState & 1) == 1)
                {
                    // Left mouse button is pressed

                }

                if (!((ColorItem)e.Data.GetData(GetType().FullName)).DragDefaultColor)
                {
                    DraggedColor = ((ColorItem)e.Data.GetData(GetType().FullName)).BackColor;
                }
                else
                {
                    DraggedColor = ((ColorItem)e.Data.GetData(GetType().FullName)).DefaultBackColor;
                }
            }

            else
            {
                DragDropMouseHovering = false;
                Refresh();

                e.Effect = DragDropEffects.None;
            }

            base.OnDragEnter(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            DragDropMouseHovering = false;
            Refresh();
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (AllowDrop && Program.Settings.NerdStats.DragAndDrop)
            {
                DragDropMouseHovering = true;
                Refresh();
                base.OnDragOver(e);
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        #endregion

        #region Methods

        public void UpdateColorsHistory()
        {
            if (!PauseColorsHistory)
            {
                if (ColorsHistory.Count > 0)
                {
                    if (ColorsHistory.Last() != BackColor) ColorsHistory.Add(BackColor);
                }
                else
                {
                    ColorsHistory.Add(BackColor);
                }
            }
        }

        public Size GetMiniColorItemSize()
        {
            return new Size(Program.Settings.NerdStats.Enabled ? 80 : 30, 24);
        }

        public bool CanRaiseEventsForDefColorDot()
        {
            return Program.Settings.NerdStats.DotDefaultChangedIndicator && Rect_DefColor.Contains(PointToClient(MousePosition)) && BackColor != DefaultBackColor;
        }

        #endregion

        #region Events/Overrides

        protected override void OnSizeChanged(EventArgs e)
        {
            Rect = new(0, 0, Width - 1, Height - 1);
            RectInner = new(1, 1, Width - 3, Height - 3);
            Rect_DefColor = new(7, (Height - 7) / 2, 7, 7);

            base.OnSizeChanged(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            UpdateColorsHistory();

            base.OnBackColorChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mousePosition_beforeDrag = MousePosition;
            InitializeDrag = Program.Settings.NerdStats.DragAndDrop;
            State = MouseState.Down;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            InitializeDrag = false;
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            InitializeDrag = false;
            HoverOverDefColorDot = false;
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Timer2?.Dispose();
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        #endregion

        #region Animator
        private int _alpha = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
        }

        private Timer Timer2;
        private int Timer2_factor = 0;

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (!DesignMode && MakeAfterDropEffect)
            {
                Timer2_factor = (int)Math.Round(Timer2_factor + Math.Min(Width, Height) * 3.5d);
                System.Threading.Thread.Sleep(1);
                Refresh();
            }
            else
            {
                Timer2_factor = 0;
                MakeAfterDropEffect = false;
                Refresh();
                Timer2.Enabled = false;
                Timer2.Stop();
            }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {


            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            G.Clear(this.GetParentColor());

            if (Enabled)
            {
                using (Config.Colors_Collection colors = new(BackColor, default, BackColor.IsDark()))
                {
                    switch (State)
                    {
                        case MouseState.None:
                            {
                                LineColor = Color.FromArgb(255, colors.Accent);
                                break;
                            }

                        case MouseState.Over:
                            {
                                LineColor = Color.FromArgb(255, colors.Line_Checked_Hover);
                                break;
                            }

                        case MouseState.Down:
                            {
                                LineColor = Color.FromArgb(255, colors.Line_Checked);
                                break;
                            }
                    }
                }

                if (BackColor.A < 255)
                {
                    using (TextureBrush br = new(Properties.Resources.BackgroundOpacity)) { G.FillRoundedRect(br, RectInner); }

                    using (Bitmap b = Properties.Resources.BackgroundOpacity.Fade(alpha / 255f))
                    using (TextureBrush br = new(b))
                    {
                        G.FillRoundedRect(br, Rect);
                    }
                }

                if (!DesignMode && MakeAfterDropEffect && CanAnimate)
                {
                    // Make ripple effect on dropping a color

                    using (SolidBrush br = new(BeforeDropColor))
                    {
                        G.FillRoundedRect(br, RectInner);
                    }

                    GraphicsPath path = Program.Style.RoundedCorners ? RectInner.Round(Program.Style.Radius) : new GraphicsPath();
                    if (!Program.Style.RoundedCorners) { path.AddRectangle(RectInner); }
                    {
                        Region reg = new(path);
                        G.Clip = reg;
                        int i = Math.Max(Width, Height) + Timer2_factor;
                        Point px = BeforeDropMousePosition;
                        Rectangle MouseCircle = new((int)Math.Round(px.X - 0.5d * i), (int)Math.Round(px.Y - 0.5d * i), i, i);
                        GraphicsPath gp = new();
                        gp.AddEllipse(MouseCircle);
                        PathGradientBrush pgb = new(gp)
                        {
                            CenterPoint = px,
                            CenterColor = BackColor,
                            SurroundColors = new Color[] { Color.Transparent }
                        };
                        G.FillEllipse(pgb, MouseCircle);

                        G.ResetClip();

                        if (i / 2d > Width * Height)
                        {
                            Timer2.Enabled = false;
                            Timer2.Stop();
                            Timer2_factor = 0;
                            MakeAfterDropEffect = false;
                            Invalidate();
                        }
                    }
                    path.Dispose();

                    using (Pen P = new(LineColor))
                    {
                        G.DrawRoundedRect_LikeW11(P, RectInner);
                    }
                }

                else if (!DesignMode && DragDropMouseHovering && CanAnimate)
                {
                    // Make circle hover effect on dragging over a color

                    using (SolidBrush br = new(BackColor)) { G.FillRoundedRect(br, Rect); }

                    using (GraphicsPath path = Rect.Round(Program.Style.Radius))
                    {
                        Region reg = new(path);
                        G.Clip = reg;
                        int i = Math.Max(Width, Height);
                        Point px = PointToClient(MousePosition);
                        Rectangle MouseCircle = new((int)Math.Round(px.X - 0.5d * i), (int)Math.Round(px.Y - 0.5d * i), i, i);
                        GraphicsPath gp = new();
                        gp.AddEllipse(MouseCircle);
                        PathGradientBrush pgb = new(gp)
                        {
                            CenterPoint = px,
                            CenterColor = DraggedColor,
                            SurroundColors = new Color[] { Color.Transparent }
                        };
                        G.FillEllipse(pgb, MouseCircle);
                        G.ResetClip();
                    }

                    using (Pen P = new(base.BackColor.IsDark() ? Color.White : Color.Black, 1.5f) { DashStyle = DashStyle.Dot }) { G.DrawRoundedRect_LikeW11(P, Rect); }
                }

                else
                {
                    // Normal appearance

                    using (SolidBrush br = new(BackColor)) { G.FillRoundedRect(br, RectInner); }

                    using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha / 255d * BackColor.A), BackColor))) { G.FillRoundedRect(br, Rect); }

                    using (Pen P = new(Color.FromArgb((int)Math.Round((255 - alpha) / 255d * BackColor.A), LineColor))) { G.DrawRoundedRect_LikeW11(P, RectInner); }

                    using (Pen P = new(Color.FromArgb((int)Math.Round(alpha / 255d * BackColor.A), LineColor))) { G.DrawRoundedRect_LikeW11(P, Rect); }
                }

                if (!DesignMode && Program.Settings.NerdStats.DotDefaultChangedIndicator)
                {
                    using (SolidBrush br = new(DefaultBackColor))
                    {
                        int L = 7;
                        int Y = (int)Math.Round(RectInner.Y + (RectInner.Height - L) / 2d);
                        Rectangle DefDotRect;

                        if (!HoverOverDefColorDot) { DefDotRect = new(L, Y, L, L); }
                        else { DefDotRect = new(L - 1, Y - 1, L + 2, L + 2); }

                        G.FillEllipse(br, DefDotRect);
                    }
                }
            }

            else
            {
                using (SolidBrush br = new(Program.Style.Schemes.Disabled.Colors.Back(parentLevel)))
                {
                    G.FillRoundedRect(br, RectInner);
                }
            }

            if (!DesignMode)
            {
                if (Program.Settings.NerdStats.Enabled & !DontShowInfo)
                {
                    G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

                    Color TargetColor = Enabled ? (!HoverOverDefColorDot | !Program.Settings.NerdStats.DotDefaultChangedIndicator ? BackColor : DefaultBackColor) : Program.Style.Schemes.Disabled.Colors.Line(parentLevel);
                    Color FC0 = TargetColor.IsDark() ? LineColor.LightLight() : LineColor.Dark(0.9f);
                    Color FC1 = TargetColor.IsDark() ? LineColor.LightLight() : LineColor.Dark(0.9f);

                    FC0 = Color.FromArgb(Program.Settings.NerdStats.MoreLabelTransparency ? 75 : 125, FC0);
                    FC1 = Color.FromArgb(alpha, FC1);

                    Rectangle RectX = Rect; RectX.Y += 1;

                    ColorsExtensions.ColorFormat CF = ColorsExtensions.ColorFormat.HEX;
                    if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.HEX)
                        CF = ColorsExtensions.ColorFormat.HEX;
                    if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.RGB)
                        CF = ColorsExtensions.ColorFormat.RGB;
                    if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.HSL)
                        CF = ColorsExtensions.ColorFormat.HSL;
                    if (Program.Settings.NerdStats.Type == Settings.Structures.NerdStats.Formats.Dec)
                        CF = ColorsExtensions.ColorFormat.Dec;

                    string S = Enabled ? TargetColor.ReturnFormat(CF, Program.Settings.NerdStats.ShowHexHash, !(TargetColor.A == 255)) : Program.Lang.Disabled;

                    Font F = Program.Settings.NerdStats.UseWindowsMonospacedFont ? new Font(FontFamily.GenericMonospace.Name, 8.5f, FontStyle.Regular) : Fonts.Console;

                    using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                    {
                        using (SolidBrush br = new(FC0)) { G.DrawString(S, F, br, RectX, sf); }
                        using (SolidBrush br = new(FC1)) { G.DrawString(S, F, br, RectX, sf); }
                    }

                    using (StringFormat sf = ContentAlignment.MiddleRight.ToStringFormat())
                    {
                        using (SolidBrush br = new(FC0)) { G.DrawString(ColorPickerOpened ? "▼" : string.Empty, F, br, new Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), sf); }
                        using (SolidBrush br = new(FC1)) { G.DrawString(ColorPickerOpened ? "▼" : string.Empty, F, br, new Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), sf); }
                    }
                }
            }

            base.OnPaint(e);
        }
    }
}