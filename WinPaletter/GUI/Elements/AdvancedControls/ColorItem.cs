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
            Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            RectInner = new Rectangle(1, 1, Width - 3, Height - 3);
            Rect_DefColor = new Rectangle(0, 0, Height, Height);
            Timer1 = new Timer() { Enabled = false, Interval = 1 };
            Timer2 = new Timer() { Enabled = false, Interval = 1 };
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            Text = "";
            ColorsHistory.Clear();
            MouseEnter += ColorItem_MouseEnter;
            MouseLeave += ColorItem_MouseLeave;
            HandleCreated += ColorItem_HandleCreated;
            Timer1.Tick += Timer1_Tick;
            Timer2.Tick += Timer2_Tick;
        }

        #region Variables

        public bool ColorPickerOpened = false;
        public List<Color> ColorsHistory = new List<Color>();
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
        public Color DefaultColor { get; set; } = Color.Black;
        public bool DontShowInfo { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        [DefaultValue("")]
        public override string Text { get; set; } = "";

        #endregion

        #region Drag and drop
        private bool SwapNotCopy = false;
        private bool InitializeDrag = false;
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

            if (InitializeDrag)
            {
                DragDefaultColor = CanRaiseEventsForDefColorDot();
                DoDragDrop(this, DragDropEffects.Copy);
            }

            InitializeDrag = false;

            if (!DesignMode && My.Env.Settings.NerdStats.DotDefaultChangedIndicator)
            {
                HoverOverDefColorDot = CanRaiseEventsForDefColorDot();
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (AllowDrop && My.Env.Settings.NerdStats.DragAndDrop)
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
                        BackColor = ((ColorItem)e.Data.GetData(GetType().FullName)).DefaultColor;
                    }
                }

                else
                {
                    e.Effect = DragDropEffects.Link;
                    var Color_From = ((ColorItem)e.Data.GetData(GetType().FullName)).BackColor;

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

                My.MyProject.Application.CopiedColor = BackColor;

                My.MyProject.Forms.ColorInfoDragDrop.Close();

                base.OnDragDrop(e);
            }

            DragDropMouseHovering = false;
            Refresh();

        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (AllowDrop && My.Env.Settings.NerdStats.DragAndDrop)
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

                if (!SwapNotCopy)
                {
                    switch (AfterDropEffect)
                    {
                        case AfterDropEffects.Invert:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Copy_Invert;
                                break;
                            }

                        case AfterDropEffects.Darker:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Copy_Darker;
                                break;
                            }

                        case AfterDropEffects.Lighter:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Copy_Lighter;
                                break;
                            }

                        case AfterDropEffects.Mix:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Copy_Mix;
                                break;
                            }

                        default:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Copy;
                                break;
                            }
                    }
                }

                else
                {
                    switch (AfterDropEffect)
                    {
                        case AfterDropEffects.Invert:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Swap_Invert;
                                break;
                            }

                        case AfterDropEffects.Darker:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Swap_Darker;
                                break;
                            }

                        case AfterDropEffects.Lighter:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Swap_Lighter;
                                break;
                            }

                        default:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Label1.Text = My.Env.Lang.ColorItem_Swap;
                                break;
                            }

                    }

                }

                if (!((ColorItem)e.Data.GetData(GetType().FullName)).DragDefaultColor)
                {
                    DraggedColor = ((ColorItem)e.Data.GetData(GetType().FullName)).BackColor;
                }
                else
                {
                    DraggedColor = ((ColorItem)e.Data.GetData(GetType().FullName)).DefaultColor;
                }

                switch (AfterDropEffect)
                {
                    case AfterDropEffects.Invert:
                        {
                            My.MyProject.Forms.ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Invert();
                            break;
                        }

                    case AfterDropEffects.Darker:
                        {
                            My.MyProject.Forms.ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Dark();
                            break;
                        }

                    case AfterDropEffects.Lighter:
                        {
                            My.MyProject.Forms.ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Light();
                            break;
                        }

                    case AfterDropEffects.Mix:
                        {
                            My.MyProject.Forms.ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Blend(BackColor, 100d);
                            break;
                        }

                    default:
                        {
                            My.MyProject.Forms.ColorInfoDragDrop.Color_From.BackColor = DraggedColor;
                            break;
                        }

                }

                DraggedColor = My.MyProject.Forms.ColorInfoDragDrop.Color_From.BackColor;

                if (!SwapNotCopy)
                {
                    My.MyProject.Forms.ColorInfoDragDrop.Color_To.BackColor = BackColor;
                }
                else
                {
                    switch (AfterDropEffect)
                    {
                        case AfterDropEffects.Invert:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Color_To.BackColor = base.BackColor.Invert();
                                break;
                            }

                        case AfterDropEffects.Darker:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Color_To.BackColor = base.BackColor.Dark();
                                break;
                            }

                        case AfterDropEffects.Lighter:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Color_To.BackColor = base.BackColor.Light();
                                break;
                            }

                        default:
                            {
                                My.MyProject.Forms.ColorInfoDragDrop.Color_To.BackColor = BackColor;
                                break;
                            }

                    }
                }

                if (My.Env.Settings.NerdStats.DragAndDropColorsGuide)
                {
                    My.MyProject.Forms.ColorInfoDragDrop.Location = new Point(e.X + 15, e.Y + 15);
                    My.MyProject.Forms.ColorInfoDragDrop.Visible = true;
                }
            }

            else
            {
                DragDropMouseHovering = false;
                Refresh();

                e.Effect = DragDropEffects.None;
                My.MyProject.Forms.ColorInfoDragDrop.Visible = false;
            }

            base.OnDragEnter(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            DragDropMouseHovering = false;
            Refresh();
            My.MyProject.Forms.ColorInfoDragDrop.Visible = false;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (AllowDrop && My.Env.Settings.NerdStats.DragAndDrop)
            {
                DragDropMouseHovering = true;
                Refresh();
                base.OnDragOver(e);
                My.MyProject.Forms.ColorInfoDragDrop.Location = new Point(e.X + 15, e.Y + 15);
            }
            else
            {
                e.Effect = DragDropEffects.None;
                My.MyProject.Forms.ColorInfoDragDrop.Visible = false;
            }

        }

        #endregion

        #region Voids/Functions

        public void UpdateColorsHistory()
        {
            if (!PauseColorsHistory)
            {
                if (ColorsHistory.Count > 0)
                {
                    if (ColorsHistory.Last() != BackColor)
                        ColorsHistory.Add(BackColor);
                }
                else
                {
                    ColorsHistory.Add(BackColor);
                }
            }
        }

        public Size GetMiniColorItemSize()
        {
            return new Size(My.Env.Settings.NerdStats.Enabled ? 80 : 30, 24);
        }

        public bool CanRaiseEventsForDefColorDot()
        {
            return My.Env.Settings.NerdStats.DotDefaultChangedIndicator && Rect_DefColor.Contains(PointToClient(MousePosition)) && BackColor != DefaultColor;
        }

        #endregion

        #region Events

        protected override void OnBackColorChanged(EventArgs e)
        {
            UpdateColorsHistory();
            base.OnBackColorChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            InitializeDrag = My.Env.Settings.NerdStats.DragAndDrop;
            State = MouseState.Down;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            InitializeDrag = false;
            State = MouseState.Over;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();

            base.OnMouseUp(e);
        }

        private void ColorItem_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void ColorItem_MouseLeave(object sender, EventArgs e)
        {
            InitializeDrag = false;
            HoverOverDefColorDot = false;
            State = MouseState.None;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void ColorItem_HandleCreated(object sender, EventArgs e)
        {
            alpha = 0;
            Timer2_factor = 0;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            RectInner = new Rectangle(1, 1, Width - 3, Height - 3);
            Rect_DefColor = new Rectangle(0, 0, Height, Height);
            base.OnSizeChanged(e);
        }

        #endregion

        #region Animators
        private int alpha;
        private readonly int Factor = 15;
        private Timer Timer1, Timer2;
        private int Timer2_factor = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                if (State == MouseState.Over)
                {
                    if (alpha + Factor <= 255)
                    {
                        alpha += Factor;
                    }
                    else if (alpha + Factor > 255)
                    {
                        alpha = 255;
                        Timer1.Enabled = false;
                        Timer1.Stop();
                    }

                    if (!Timer2.Enabled)
                    {
                        System.Threading.Thread.Sleep(1);
                        Refresh();
                    }
                }

                if (!(State == MouseState.Over))
                {
                    if (alpha - Factor >= 0)
                    {
                        alpha -= Factor;
                    }
                    else if (alpha - Factor < 0)
                    {
                        alpha = 0;
                        Timer1.Enabled = false;
                        Timer1.Stop();
                    }

                    if (!Timer2.Enabled)
                    {
                        System.Threading.Thread.Sleep(1);
                        Refresh();
                    }
                }
            }
        }

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            DoubleBuffered = true;

            int R = 5;

            G.Clear(this.GetParentColor());

            switch (State)
            {
                case MouseState.None:
                    {
                        LineColor = base.BackColor.IsDark() ? base.BackColor.CB(0.05f) : base.BackColor.CB((float)-0.05d);
                        break;
                    }

                case MouseState.Over:
                    {
                        LineColor = base.BackColor.IsDark() ? base.BackColor.CB(0.15f) : base.BackColor.CB((float)-0.15d);
                        break;
                    }

                case MouseState.Down:
                    {
                        LineColor = base.BackColor.IsDark() ? base.BackColor.CB(0.1f) : base.BackColor.CB((float)-0.1d);
                        break;
                    }

            }

            LineColor = Color.FromArgb(255, LineColor);

            if (BackColor.A < 255)
            {
                using (var br = new TextureBrush(Properties.Resources.BackgroundOpacity))
                {
                    G.FillRoundedRect(br, RectInner, R);
                }
                using (var br = new TextureBrush(Properties.Resources.BackgroundOpacity.Fade(alpha / 255d)))
                {
                    G.FillRoundedRect(br, Rect, R);
                }
            }

            if (!DesignMode && MakeAfterDropEffect && My.Env.Settings.NerdStats.DragAndDropRippleEffect)
            {
                // Make ripple effect on dropping a color

                using (var br = new SolidBrush(BeforeDropColor))
                {
                    G.FillRoundedRect(br, RectInner, R);
                }

                using (var path = RectInner.Round(R))
                {
                    var reg = new Region(path);
                    G.Clip = reg;
                    int i = Math.Max(Width, Height) + Timer2_factor;
                    var px = BeforeDropMousePosition;
                    var MouseCircle = new Rectangle((int)Math.Round(px.X - 0.5d * i), (int)Math.Round(px.Y - 0.5d * i), i, i);
                    var gp = new GraphicsPath();
                    gp.AddEllipse(MouseCircle);
                    var pgb = new PathGradientBrush(gp)
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

                using (var P = new Pen(LineColor))
                {
                    G.DrawRoundedRect_LikeW11(P, RectInner, R);
                }
            }

            else if (!DesignMode && DragDropMouseHovering && My.Env.Settings.NerdStats.DragAndDropRippleEffect)
            {
                // Make circle hover effect on dragging over a color

                using (var br = new SolidBrush(BackColor))
                {
                    G.FillRoundedRect(br, Rect, R);
                }

                using (var path = Rect.Round(R))
                {
                    var reg = new Region(path);
                    G.Clip = reg;
                    int i = Math.Max(Width, Height);
                    var px = PointToClient(MousePosition);
                    var MouseCircle = new Rectangle((int)Math.Round(px.X - 0.5d * i), (int)Math.Round(px.Y - 0.5d * i), i, i);
                    var gp = new GraphicsPath();
                    gp.AddEllipse(MouseCircle);
                    var pgb = new PathGradientBrush(gp)
                    {
                        CenterPoint = px,
                        CenterColor = DraggedColor,
                        SurroundColors = new Color[] { Color.Transparent }
                    };
                    G.FillEllipse(pgb, MouseCircle);
                    G.ResetClip();
                }

                using (var P = new Pen(base.BackColor.IsDark() ? Color.White : Color.Black, 1.5f) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRoundedRect_LikeW11(P, Rect, R);
                }
            }

            else
            {
                // Normal appearance

                using (var br = new SolidBrush(BackColor))
                {
                    G.FillRoundedRect(br, RectInner, R);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha / 255d * BackColor.A), BackColor)))
                {
                    G.FillRoundedRect(br, Rect, R);
                }

                using (var P = new Pen(Color.FromArgb(alpha, LineColor)))
                {
                    G.DrawRoundedRect_LikeW11(P, Rect, R);
                }
                using (var P = new Pen(Color.FromArgb(255 - alpha, LineColor)))
                {
                    G.DrawRoundedRect_LikeW11(P, RectInner, R);
                }
            }

            try
            {
                if (!DesignMode && My.Env.Settings.NerdStats.DotDefaultChangedIndicator)
                {
                    using (var br = new SolidBrush(DefaultColor))
                    {

                        int L = Math.Max(6, RectInner.Height - 10);
                        int Y = (int)Math.Round(RectInner.Y + (RectInner.Height - L) / 2d);
                        Rectangle DefDotRect;

                        if (!HoverOverDefColorDot)
                        {
                            DefDotRect = new Rectangle(Y, Y, L, L);
                        }
                        else
                        {
                            DefDotRect = new Rectangle(Y - 1, Y - 1, L + 2, L + 2);
                        }

                        G.FillEllipse(new SolidBrush(DefaultColor), DefDotRect);
                    }
                }
            }
            catch
            {
            }

            if (!DesignMode)
            {
                if (My.Env.Settings.NerdStats.Enabled & !DontShowInfo)
                {
                    G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

                    var TargetColor = !HoverOverDefColorDot | !My.Env.Settings.NerdStats.DotDefaultChangedIndicator ? BackColor : DefaultColor;
                    var FC0 = TargetColor.IsDark() ? LineColor.LightLight() : LineColor.Dark(0.9f);
                    var FC1 = TargetColor.IsDark() ? LineColor.LightLight() : LineColor.Dark(0.9f);

                    FC0 = Color.FromArgb(My.Env.Settings.NerdStats.MoreLabelTransparency ? 75 : 125, FC0);
                    FC1 = Color.FromArgb(alpha, FC1);

                    var RectX = Rect;
                    RectX.Y += 1;

                    var CF = ColorsExtensions.ColorFormat.HEX;
                    if (My.Env.Settings.NerdStats.Type == WPSettings.Structures.NerdStats.Formats.HEX)
                        CF = ColorsExtensions.ColorFormat.HEX;
                    if (My.Env.Settings.NerdStats.Type == WPSettings.Structures.NerdStats.Formats.RGB)
                        CF = ColorsExtensions.ColorFormat.RGB;
                    if (My.Env.Settings.NerdStats.Type == WPSettings.Structures.NerdStats.Formats.HSL)
                        CF = ColorsExtensions.ColorFormat.HSL;
                    if (My.Env.Settings.NerdStats.Type == WPSettings.Structures.NerdStats.Formats.Dec)
                        CF = ColorsExtensions.ColorFormat.Dec;

                    string S = TargetColor.ReturnFormat(CF, My.Env.Settings.NerdStats.ShowHexHash, !(TargetColor.A == 255));
                    Font F;

                    if (!My.Env.Settings.NerdStats.UseWindowsMonospacedFont)
                    {
                        F = My.MyProject.Application.ConsoleFont;
                    }
                    else
                    {
                        F = new Font(FontFamily.GenericMonospace.Name, 8.5f, FontStyle.Regular);
                    }

                    using (var br = new SolidBrush(FC0))
                    {
                        G.DrawString(S, F, br, RectX, ContentAlignment.MiddleCenter.ToStringFormat());
                    }
                    using (var br = new SolidBrush(FC1))
                    {
                        G.DrawString(S, F, br, RectX, ContentAlignment.MiddleCenter.ToStringFormat());
                    }

                    if (ColorPickerOpened)
                    {
                        using (var br = new SolidBrush(FC0))
                        {
                            G.DrawString("▼", F, br, new Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), ContentAlignment.MiddleRight.ToStringFormat());
                        }
                        using (var br = new SolidBrush(FC1))
                        {
                            G.DrawString("▼", F, br, new Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), ContentAlignment.MiddleRight.ToStringFormat());
                        }
                    }

                }
            }

        }

    }

}