using FluentTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using static WinPaletter.Settings.Structures.NerdStats;

namespace WinPaletter.UI.Controllers
{
    [DefaultEvent("Click")]
    public class ColorItem : Control
    {
        // Static shared context menu (one instance for the entire app)
        private static readonly WP.ContextMenuStrip _sharedContextMenu;
        private static readonly ToolStripMenuItem _darken;
        private static readonly ToolStripMenuItem _lighten;
        private static readonly ToolStripMenuItem _invert;
        private static readonly ToolStripMenuItem _copy;
        private static readonly ToolStripMenuItem _paste;
        private static readonly ToolStripMenuItem _cut;
        private static readonly ToolStripMenuItem _delete;
        private static readonly ToolStripMenuItem _reset;
        private static readonly ToolStripMenuItem _blend;
        private static readonly ToolStripMenuItem _someEffects;
        private static readonly ToolStripMenuItem _previousColor;
        private static readonly ToolStripSeparator _sep0;
        private static readonly ToolStripSeparator _sep1;
        private static readonly ToolStripSeparator _sep2;
        private static readonly ToolStripMenuItem _copyAsHex;
        private static readonly ToolStripMenuItem _copyAsRGB;
        private static readonly ToolStripMenuItem _copyAsHSL;
        private static readonly ToolStripMenuItem _copyAsDecimal;
        private static readonly ToolStripMenuItem _copyAsRGBPercent;
        private static readonly ToolStripMenuItem _copyAsARGB;
        private static readonly ToolStripMenuItem _copyAsHSLA;
        private static readonly ToolStripMenuItem _copyAsHSV;
        private static readonly ToolStripMenuItem _copyAsCMYK;
        private static readonly ToolStripMenuItem _copyAsWin32;
        private static readonly ToolStripMenuItem _copyAsKnownName;
        private static readonly ToolStripMenuItem _copyAsCSS;

        // Cached images
        private static Image _pasteImageEnabled;
        private static Image _pasteImageDisabled;
        private static Image _blendImageEnabled;
        private static Image _blendImageDisabled;
        private static Image _prevColorImageEnabled;
        private static Image _prevColorImageDisabled;

        // The ColorItem that most recently opened the shared menu
        private static ColorItem _activeTarget;

        // Static shared font
        private static readonly Font _genericMonospacedFont;
        private new Font Font => Program.Settings.NerdStats.UseWindowsMonospacedFont ? _genericMonospacedFont : Fonts.Console;

        private static readonly StringFormat chevronSF = ContentAlignment.MiddleRight.ToStringFormat();
        private static readonly StringFormat middleCenter = ContentAlignment.MiddleCenter.ToStringFormat();

        // Static constructor
        static ColorItem()
        {
            _genericMonospacedFont = new Font(Fonts.FallbackConsoleFont, 8.5f, FontStyle.Regular);

            _sharedContextMenu = new WP.ContextMenuStrip { ShowImageMargin = true, AllowTransparency = true };

            _copyAsHex = new();
            _copyAsRGB = new();
            _copyAsHSL = new();
            _copyAsDecimal = new();
            _copyAsRGBPercent = new();
            _copyAsARGB = new();
            _copyAsHSLA = new();
            _copyAsHSV = new();
            _copyAsCMYK = new();
            _copyAsWin32 = new();
            _copyAsKnownName = new();
            _copyAsCSS = new();

            _darken = new() { Image = ColorItemContextMenu.Darken };
            _lighten = new() { Image = ColorItemContextMenu.Lighten };
            _invert = new() { Image = ColorEffects.Invert_24 };
            _cut = new() { Image = ColorItemContextMenu.Cut };
            _delete = new() { Image = ColorItemContextMenu.Delete };
            _reset = new() { Image = ColorItemContextMenu.Default };
            _previousColor = new();
            _paste = new();
            _blend = new();
            _someEffects = new() { Image = ColorItemContextMenu.Effects };
            _sep0 = new();
            _sep1 = new();
            _sep2 = new();

            _copy = new()
            {
                Image = ColorItemContextMenu.Copy,
                DropDown = new UI.WP.ContextMenuStrip { ShowImageMargin = false }
            };
            _copy.DropDownItems.Add(_copyAsHex);
            _copy.DropDownItems.Add(_copyAsRGB);
            _copy.DropDownItems.Add(_copyAsHSL);
            _copy.DropDownItems.Add(_copyAsDecimal);
            _copy.DropDownItems.Add(_copyAsRGBPercent);
            _copy.DropDownItems.Add(_copyAsARGB);
            _copy.DropDownItems.Add(_copyAsHSLA);
            _copy.DropDownItems.Add(_copyAsHSV);
            _copy.DropDownItems.Add(_copyAsCMYK);
            _copy.DropDownItems.Add(_copyAsWin32);
            _copy.DropDownItems.Add(_copyAsKnownName);
            _copy.DropDownItems.Add(_copyAsCSS);

            _sharedContextMenu.Items.Add(_cut);
            _sharedContextMenu.Items.Add(_copy);
            _sharedContextMenu.Items.Add(_paste);
            _sharedContextMenu.Items.Add(_blend);
            _sharedContextMenu.Items.Add(_sep0);
            _sharedContextMenu.Items.Add(_delete);
            _sharedContextMenu.Items.Add(_reset);
            _sharedContextMenu.Items.Add(_previousColor);
            _sharedContextMenu.Items.Add(_sep1);
            _sharedContextMenu.Items.Add(_darken);
            _sharedContextMenu.Items.Add(_lighten);
            _sharedContextMenu.Items.Add(_sep2);
            _sharedContextMenu.Items.Add(_invert);

            _someEffects.DropDown = new WP.ContextMenuStrip();

            for (int i = 0; i < ColorEffect.RegisteredEffects.Count; i++)
            {
                ColorEffect effect = ColorEffect.RegisteredEffects[i].Clone();

                if (!effect.HasScrollbar)
                {
                    effect.Checked = true;

                    ToolStripMenuItem item = new()
                    {
                        Text = effect.Name,
                        Image = effect.SmallImage.Resize(20, 20),
                        Tag = effect
                    };

                    item.Click += Static_Item_Click;
                    _someEffects.DropDownItems.Add(item);
                }
            }

            _sharedContextMenu.Items.Add(_someEffects);

            _copy.Click += Static_Copy_Click;
            _cut.Click += Static_Cut_Click;
            _paste.Click += Static_Paste_Click;
            _blend.Click += Static_Blend_Click;
            _delete.Click += Static_Delete_Click;
            _darken.Click += Static_Darken_Click;
            _lighten.Click += Static_Lighten_Click;
            _invert.Click += Static_Invert_Click;
            _reset.Click += Static_Reset_Click;
            _previousColor.Click += Static_PreviousColor_Click;
            _copyAsHex.Click += Static_Copy_AsHEX;
            _copyAsRGB.Click += Static_Copy_AsRGB;
            _copyAsHSL.Click += Static_Copy_AsHSL;
            _copyAsDecimal.Click += Static_Copy_AsDec;
            _copyAsRGBPercent.Click += Static_Copy_AsRGBPercent;
            _copyAsARGB.Click += Static_Copy_AsARGB;
            _copyAsHSLA.Click += Static_Copy_AsHSLA;
            _copyAsHSV.Click += Static_Copy_AsHSV;
            _copyAsCMYK.Click += Static_Copy_AsCMYK;
            _copyAsWin32.Click += Static_Copy_AsWin32;
            _copyAsKnownName.Click += Static_Copy_AsKnownName;
            _copyAsCSS.Click += Static_Copy_AsCSS;

            _pasteImageEnabled = ColorItemContextMenu.Paste;
            _pasteImageDisabled = ColorItemContextMenu.Paste.Grayscale();
            _blendImageEnabled = ColorEffects.Blend_24;
            _blendImageDisabled = ColorEffects.Blend_24.Grayscale();
            _prevColorImageEnabled = ColorItemContextMenu.Reset;
            _prevColorImageDisabled = ColorItemContextMenu.Reset.Grayscale();
        }

        #region Static menu event handlers

        private static void Static_Copy_Click(object sender, EventArgs e)
        {
            if (_activeTarget == null) return;
            ColorClipboard.CopiedColor = _activeTarget.BackColor;
            _activeTarget.CopyColor(_activeTarget.BackColor.ToString(Program.Settings.NerdStats.Type));
            _sharedContextMenu.Close();
        }

        private static void Static_Copy_AsHEX(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.HEX));
        private static void Static_Copy_AsRGB(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.RGB));
        private static void Static_Copy_AsHSL(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.HSL));
        private static void Static_Copy_AsDec(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.Dec));
        private static void Static_Copy_AsRGBPercent(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.RGBPercent));
        private static void Static_Copy_AsARGB(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.ARGB));
        private static void Static_Copy_AsHSLA(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.HSLA));
        private static void Static_Copy_AsHSV(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.HSV));
        private static void Static_Copy_AsCMYK(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.CMYK));
        private static void Static_Copy_AsWin32(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.Win32));
        private static void Static_Copy_AsKnownName(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.KnownName));
        private static void Static_Copy_AsCSS(object sender, EventArgs e) => _activeTarget?.CopyColor(_activeTarget.BackColor.ToString(Formats.CSS));

        private static void Static_Cut_Click(object sender, EventArgs e)
        {
            if (_activeTarget == null) return;
            ColorClipboard.CopiedColor = _activeTarget.BackColor;
            _activeTarget.StartColorChangeAnimation(Color.Empty, sender);
        }

        private static void Static_Paste_Click(object sender, EventArgs e)
        {
            if (_activeTarget == null || ColorClipboard.CopiedColor == Color.Empty) return;
            _activeTarget.StartColorChangeAnimation(ColorClipboard.CopiedColor, sender);
        }

        private static void Static_Delete_Click(object sender, EventArgs e)
        {
            _activeTarget?.StartColorChangeAnimation(Color.Empty, sender);
        }

        private static void Static_Reset_Click(object sender, EventArgs e)
        {
            _activeTarget?.StartColorChangeAnimation(_activeTarget.DefaultBackColor, sender);
        }

        private static void Static_PreviousColor_Click(object sender, EventArgs e)
        {
            if (_activeTarget == null || _activeTarget.ColorsHistory.Count <= 1) return;
            _activeTarget.PauseColorsHistory = true;
            Color prev = _activeTarget.ColorsHistory[_activeTarget.ColorsHistory.Count - 2];
            _activeTarget.ColorsHistory.RemoveAt(_activeTarget.ColorsHistory.Count - 1);
            _activeTarget.StartColorChangeAnimation(prev, sender);
            _activeTarget.PauseColorsHistory = false;
        }

        private static void Static_Darken_Click(object sender, EventArgs e)
        {
            _activeTarget?.StartColorChangeAnimation(_activeTarget.BackColor.Dark(), sender);
        }

        private static void Static_Lighten_Click(object sender, EventArgs e)
        {
            _activeTarget?.StartColorChangeAnimation(_activeTarget.BackColor.Light(), sender);
        }

        private static void Static_Invert_Click(object sender, EventArgs e)
        {
            _activeTarget?.StartColorChangeAnimation(_activeTarget.BackColor.Invert(), sender);
        }

        private static void Static_Blend_Click(object sender, EventArgs e)
        {
            if (_activeTarget == null) return;
            _activeTarget.StartColorChangeAnimation(_activeTarget.BackColor.Blend(ColorClipboard.CopiedColor, 0.5f), sender);
        }

        private static void Static_Item_Click(object sender, EventArgs e)
        {
            if (_activeTarget == null) return;
            ColorEffect effect = (sender as ToolStripMenuItem).Tag as ColorEffect;
            _activeTarget.StartColorChangeAnimation(effect.Apply(_activeTarget.BackColor), sender);
        }

        #endregion

        #region Instance state

        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public bool ColorPickerOpened = false;
        public List<Color> ColorsHistory = [];
        private Color LineColor;
        public bool PauseColorsHistory = false;

        // Cached rectangles - updated only in OnSizeChanged
        private Rectangle Rect;
        private Rectangle RectInner;
        private Rectangle Rect_DefColor;
        private Rectangle Rect_DefColor_MouseHoverFixer;

        // Cached graphics paths - updated only in OnSizeChanged
        private GraphicsPath _roundedRectPath;
        private GraphicsPath _roundedRectInnerPath;

        private PointF mousePosition;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties

        public new Color DefaultBackColor { get; set; } = Color.Black;

        [DefaultValue(false)]
        public bool CancelShowingMenu { get; set; } = false;

        public bool DontShowInfo { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
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
        private Timer _animationTimer;
        private int _animationFactor = 0;

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
            mousePosition = PointToClient(MousePosition);

            if (InitializeDrag && mousePosition_beforeDrag != mousePosition_afterDrag)
            {
                DragDefaultColor = CanRaiseEventsForDefColorDot();
                DoDragDrop(this, DragDropEffects.Copy | DragDropEffects.Move);
            }

            if (!DesignMode && Program.Settings.NerdStats.DotDefaultChangedIndicator)
            {
                bool hover = CanRaiseEventsForDefColorDot();

                if (HoverOverDefColorDot != hover)
                {
                    HoverOverDefColorDot = hover;
                    Invalidate();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            // Stop any existing animation before starting a new one
            StopDropAnimation();

            if (AllowDrop && Program.Settings.NerdStats.DragAndDrop)
            {
                BeforeDropColor = BackColor;
                BeforeDropMousePosition = PointToClient(MousePosition);
                StartDropAnimation();

                if (e.Data.GetData(typeof(ColorItem)) is not ColorItem draggedColorItem) return;

                if (!SwapNotCopy)
                {
                    BackColor = ProcessDraggedColorEffect(draggedColorItem.DragDefaultColor ? draggedColorItem.DefaultBackColor : draggedColorItem.BackColor);
                }
                else
                {
                    BackColor = draggedColorItem.BackColor;
                    draggedColorItem.BackColor = ProcessDraggedColorEffect(BeforeDropColor);
                    draggedColorItem.ContextMenuMadeColorChangeInvoker?.Invoke(draggedColorItem, new ContextMenuMadeColorChangeEventArgs(draggedColorItem, null));
                }

                ColorClipboard.CopiedColor = BackColor;

                base.OnDragDrop(e);
            }

            AfterDropEffect = AfterDropEffects.None;
            DragDropMouseHovering = false;
            Invalidate();
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (DesignMode) { base.OnDragEnter(e); return; }

            if (!AllowDrop || !Program.Settings.NerdStats.DragAndDrop)
            {
                DragDropMouseHovering = false;
                BeginInvoke((Action)Invalidate);
                e.Effect = DragDropEffects.None;
                base.OnDragEnter(e);
                return;
            }

            DragDropMouseHovering = true;
            UpdateDragState(e);

            if (e.Data.GetData(typeof(ColorItem)) is ColorItem item)
            {
                DraggedColor = ProcessDraggedColorEffect(item.DragDefaultColor ? item.DefaultBackColor : item.BackColor);
            }

            base.OnDragEnter(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!AllowDrop || !Program.Settings.NerdStats.DragAndDrop)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            DragDropMouseHovering = true;

            if (e.Data.GetData(typeof(ColorItem)) is ColorItem item)
            {
                DraggedColor = ProcessDraggedColorEffect(item.DragDefaultColor ? item.DefaultBackColor : item.BackColor);
            }

            UpdateDragState(e);
            Invalidate();

            base.OnDragOver(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            StopDropAnimation();
            DragDropMouseHovering = false;
            AfterDropEffect = AfterDropEffects.None;
            SwapNotCopy = false;
            Invalidate();
        }

        private void UpdateDragState(DragEventArgs e)
        {
            const int MK_RBUTTON = 2;
            const int MK_SHIFT = 4;
            const int MK_CTRL = 8;
            const int MK_ALT = 32;

            SwapNotCopy = false;
            AfterDropEffect = AfterDropEffects.None;

            bool alt = (e.KeyState & MK_ALT) != 0;
            bool ctrl = (e.KeyState & MK_CTRL) != 0;
            bool shift = (e.KeyState & MK_SHIFT) != 0;
            bool rbtn = (e.KeyState & MK_RBUTTON) != 0;

            if (alt && ctrl) AfterDropEffect = AfterDropEffects.Mix;
            else if (alt) AfterDropEffect = AfterDropEffects.Invert;
            else if (ctrl) AfterDropEffect = AfterDropEffects.Darker;
            else if (shift) AfterDropEffect = AfterDropEffects.Lighter;

            if (rbtn) SwapNotCopy = true;

            e.Effect = SwapNotCopy ? DragDropEffects.Move : DragDropEffects.Copy;
        }

        private Color ProcessDraggedColorEffect(Color c)
        {
            switch (AfterDropEffect)
            {
                case AfterDropEffects.Invert: return c.Invert();
                case AfterDropEffects.Darker: return c.Dark();
                case AfterDropEffects.Lighter: return c.Light();
                case AfterDropEffects.Mix: return c.Blend(BeforeDropColor, 0.5f);
                default: return c;
            }
        }
        #endregion

        #region Constructor

        public ColorItem()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            _animationTimer = new Timer { Enabled = false, Interval = 1 };
            _animationTimer.Tick += AnimationTimer_Tick;

            // Initialize rectangles and paths
            UpdateCachedRects();
            UpdateCachedPaths();

            alpha = 0;
            Text = string.Empty;
            ColorsHistory.Clear();
        }

        private void UpdateCachedRects()
        {
            Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            RectInner = new Rectangle(1, 1, Width - 3, Height - 3);
            Rect_DefColor = new Rectangle(7, (Height - 7) / 2, 7, 7);
            Rect_DefColor_MouseHoverFixer = new Rectangle(Rect_DefColor.X - 3, Rect_DefColor.Y - 3,
                Rect_DefColor.Width + 6, Rect_DefColor.Height + 6);
        }

        private void UpdateCachedPaths()
        {
            DisposeCachedPaths();

            if (Width > 0 && Height > 0)
            {
                _roundedRectPath = Rect.Round(Program.Style.Radius);
                _roundedRectInnerPath = RectInner.Round(Program.Style.Radius);
            }
        }

        private void DisposeCachedPaths()
        {
            _roundedRectPath?.Dispose();
            _roundedRectInnerPath?.Dispose();
            _roundedRectPath = null;
            _roundedRectInnerPath = null;
        }

        #endregion

        #region Methods

        public void UpdateColorsHistory()
        {
            if (PauseColorsHistory) return;

            List<Color> h = ColorsHistory;
            int count = h.Count;

            if (count > 0)
            {
                if (h[count - 1] != BackColor) h.Add(BackColor);
            }
            else
            {
                h.Add(BackColor);
            }

            if (h.Count > 50) h.RemoveAt(0);
        }

        public static Size GetMiniColorItemSize()
        {
            return new Size(Program.Settings.NerdStats.Enabled ? 80 : 30, 24);
        }

        public bool CanRaiseEventsForDefColorDot()
        {
            return Program.Settings.NerdStats.DotDefaultChangedIndicator &&
                DefaultBackColor != Color.Empty && DefaultBackColor != Color.Transparent &&
                Rect_DefColor_MouseHoverFixer.Contains(PointToClient(MousePosition)) &&
                BackColor != DefaultBackColor;
        }

        #endregion

        #region Animation Timer

        private void StartDropAnimation()
        {
            // Stop any existing animation first to prevent multiple simultaneous animations
            StopDropAnimation();

            _animationFactor = 0;
            MakeAfterDropEffect = true;
            _animationTimer.Start();
        }

        private void StopDropAnimation()
        {
            _animationFactor = 0;
            MakeAfterDropEffect = false;
            _animationTimer.Stop();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Early exit if in design mode or animation should not be running
            if (DesignMode || !MakeAfterDropEffect)
            {
                StopDropAnimation();
                return;
            }

            _animationFactor = (int)(_animationFactor + Math.Min(Width, Height) * 3.5f);

            // Calculate the hover rectangle dimensions
            int i = Math.Max(Width, Height) + _animationFactor;
            Rectangle hoverRect = new(
                BeforeDropMousePosition.X - i / 2,
                BeforeDropMousePosition.Y - i / 2,
                i,
                i);

            // Check if the hover circle completely covers the control
            // The animation is complete when the circle's bounding box
            // completely encloses the control's rectangle
            bool animationComplete = hoverRect.Contains(RectInner);

            if (animationComplete)
            {
                StopDropAnimation();
                return;
            }

            // Only invalidate if animation is still in progress
            Invalidate();
        }

        #endregion

        #region Events / Overrides

        public delegate void ContextMenuMadeColorChange(object sender, ContextMenuMadeColorChangeEventArgs e);

        public event ContextMenuMadeColorChange ContextMenuMadeColorChangeInvoker;

        public class ContextMenuMadeColorChangeEventArgs : EventArgs
        {
            public ContextMenuMadeColorChangeEventArgs(ColorItem colorItem, object clickedItem)
            {
                ColorItem = colorItem;
                ClickedItem = clickedItem;
            }

            public object ClickedItem { get; set; }
            public ColorItem ColorItem { get; }
            public Color Color => ColorItem?.BackColor ?? Color.Empty;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)User32.WindowsMessage.RButtonUp && !CancelShowingMenu)
            {
                _activeTarget = this;

                // Update localised text
                _copyAsHex.Text = Program.Localization.Strings.General.Copy_HEX;
                _copyAsRGB.Text = Program.Localization.Strings.General.Copy_RGB;
                _copyAsHSL.Text = Program.Localization.Strings.General.Copy_HSL;
                _copyAsDecimal.Text = Program.Localization.Strings.General.Copy_Decimal;
                _copyAsRGBPercent.Text = Program.Localization.Strings.General.Copy_RGBPercent;
                _copyAsARGB.Text = Program.Localization.Strings.General.Copy_ARGB;
                _copyAsHSLA.Text = Program.Localization.Strings.General.Copy_HSLA;
                _copyAsHSV.Text = Program.Localization.Strings.General.Copy_HSV;
                _copyAsCMYK.Text = Program.Localization.Strings.General.Copy_CMYK;
                _copyAsWin32.Text = Program.Localization.Strings.General.Copy_Win32;
                _copyAsKnownName.Text = Program.Localization.Strings.General.Copy_KnownName;
                _copyAsCSS.Text = Program.Localization.Strings.General.Copy_CSS;
                _copy.Text = Program.Localization.Strings.General.Copy;
                _cut.Text = Program.Localization.Strings.General.Cut;
                _paste.Text = Program.Localization.Strings.General.Paste;
                _blend.Text = Program.Localization.Strings.General.PasteByBlending;
                _delete.Text = Program.Localization.Strings.General.Delete;
                _reset.Text = Program.Localization.Strings.General.Default + " (" + OS.Name + ")";
                _darken.Text = Program.Localization.Strings.General.Darken;
                _lighten.Text = Program.Localization.Strings.General.Lighten;
                _invert.Text = Program.Localization.Strings.General.Invert;
                _previousColor.Text = Program.Localization.Strings.General.PreviousColor;
                _someEffects.Text = Program.Localization.Strings.ColorEffects.SomeEffects;

                // Enable/disable items
                bool blendEnabled = ColorClipboard.CopiedColor != Color.Empty;
                _blend.Enabled = blendEnabled;
                _blend.Image = blendEnabled ? _blendImageEnabled : _blendImageDisabled;

                string clipBoard = Clipboard.GetText();
                bool clipBoardHasColor = clipBoard?.IsHexColor() ?? false;
                if (ColorClipboard.CopiedColor == Color.Empty && clipBoardHasColor)
                    ColorClipboard.CopiedColor = clipBoard.ToColor();

                bool pasteEnabled = ColorClipboard.CopiedColor != Color.Empty;
                _paste.Enabled = pasteEnabled;
                _paste.Image = pasteEnabled ? _pasteImageEnabled : _pasteImageDisabled;

                bool prevEnabled = ColorsHistory.Count > 2;
                _previousColor.Enabled = prevEnabled;
                _previousColor.Image = prevEnabled ? _prevColorImageEnabled : _prevColorImageDisabled;

                Point screenPoint = new((short)(m.LParam.ToInt32() & 0xFFFF), (short)(m.LParam.ToInt32() >> 16));

                User32.ReleaseCapture();
                _sharedContextMenu.Show(this, screenPoint);

                return;
            }

            base.WndProc(ref m);
        }

        private void CopyColor(string text)
        {
            Clipboard.SetText(text);
            ColorClipboard.CopiedColor = BackColor;
        }

        private void StartColorChangeAnimation(Color newColor, object sender)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new Point(_sharedContextMenu.Left, _sharedContextMenu.Top));
            StartDropAnimation();
            BackColor = newColor;

            if (BeforeDropColor != BackColor)
                ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateCachedRects();
            UpdateCachedPaths();
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

            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(Program.AnimationSpan); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            InitializeDrag = false;
            State = MouseState.Over;
            bool mouseOver = ClientRectangle.Contains(e.Location);
            if (CanAnimate) { Transition.With(this, nameof(alpha), ContainsFocus || mouseOver ? 255 : 0).CriticalDamp(Program.AnimationSpan); }
            else { alpha = ContainsFocus || mouseOver ? 255 : 0; }

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { Transition.With(this, nameof(alpha), 255).CriticalDamp(Program.AnimationSpan); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            InitializeDrag = false;
            HoverOverDefColorDot = false;
            State = MouseState.None;

            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(Program.AnimationSpan); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_activeTarget == this) _activeTarget = null;

                StopDropAnimation();
                _animationTimer?.Dispose();

                DisposeCachedPaths();
            }
            base.Dispose(disposing);
        }

        private int parentLevel = 0;

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
            set
            {
                if (_alpha == value) return;
                _alpha = value;
                Invalidate();
            }
        }

        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Left empty intentionally
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            SmoothingMode oldSmoothingMode = G.SmoothingMode;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            InvokePaintBackground(this, e);
            G.Clear(this.GetParentColor());

            if (Enabled)
            {
                byte a = State == MouseState.Down ? (byte)200 : State == MouseState.Over ? (byte)150 : (byte)100;
                a *= (byte)(BackColor.A / 255f);

                LineColor = Color.FromArgb(a, BackColor.IsDark() ? BackColor.Light() : BackColor.Dark());

                if (BackColor.A < 255)
                {
                    if (_roundedRectInnerPath != null)
                    {
                        using (TextureBrush br = new(Resources.BackgroundOpacity))
                        {
                            G.FillPath(br, _roundedRectInnerPath);
                        }
                    }

                    if (_roundedRectPath != null)
                    {
                        using (Bitmap b = Resources.BackgroundOpacity.Fade(alpha / 255f))
                        using (TextureBrush br = new(b))
                        {
                            G.FillPath(br, _roundedRectPath);
                        }
                    }
                }

                if (!DesignMode && MakeAfterDropEffect && CanAnimate)
                {
                    if (_roundedRectInnerPath != null)
                    {
                        using (SolidBrush br = new(BeforeDropColor))
                        {
                            G.FillPath(br, _roundedRectInnerPath);
                        }
                    }

                    int i = Math.Max(Width, Height) + _animationFactor;
                    Rectangle hoverRect = new(BeforeDropMousePosition.X - i / 2, BeforeDropMousePosition.Y - i / 2, i, i);
                    G.DrawHover(RectInner, hoverRect, BeforeDropMousePosition, BackColor);

                    if (_roundedRectInnerPath != null)
                    {
                        using (Pen P = new(LineColor))
                        {
                            G.DrawPath(P, _roundedRectInnerPath);
                        }
                    }
                }
                else if (!DesignMode && DragDropMouseHovering && CanAnimate)
                {
                    if (_roundedRectPath != null)
                    {
                        using (SolidBrush br = new(BackColor))
                        {
                            G.FillPath(br, _roundedRectPath);
                        }

                        int i = Math.Max(Width, Height);
                        Point px = PointToClient(MousePosition);
                        G.DrawHover(this, _roundedRectPath, DraggedColor, i);

                        using (Pen P = new(base.BackColor.IsDark() ? Color.White : Color.Black, 1.5f) { DashStyle = DashStyle.Dot })
                        {
                            G.DrawPath(P, _roundedRectPath);
                        }
                    }
                }
                else
                {
                    if (_roundedRectInnerPath != null)
                    {
                        using (SolidBrush br = new(BackColor))
                        {
                            G.FillPath(br, _roundedRectInnerPath);
                        }
                    }

                    if (_roundedRectPath != null)
                    {
                        using (SolidBrush br = new(Color.FromArgb((int)(alpha / 255f * BackColor.A), BackColor)))
                        {
                            G.FillPath(br, _roundedRectPath);
                        }
                    }

                    if (_roundedRectInnerPath != null)
                    {
                        using (Pen P = new(Color.FromArgb((int)((255f - alpha) / 255f * LineColor.A), LineColor)))
                        {
                            G.DrawPath(P, _roundedRectInnerPath);
                        }
                    }

                    if (_roundedRectPath != null)
                    {
                        using (Pen P = new(Color.FromArgb((int)(alpha / 255f * LineColor.A), LineColor)))
                        {
                            G.DrawPath(P, _roundedRectPath);
                        }
                    }
                }

                if (!DesignMode && Program.Settings.NerdStats.DotDefaultChangedIndicator)
                {
                    using (SolidBrush br = new(DefaultBackColor))
                    {
                        const float L = 7f;
                        float Y = RectInner.Y + (RectInner.Height - L) / 2f;
                        RectangleF defDotRect = HoverOverDefColorDot
                            ? new RectangleF(L - 1, Y - 1, L + 2, L + 2)
                            : new RectangleF(L, Y, L, L);

                        G.FillEllipse(br, defDotRect);
                    }
                }
            }
            else
            {
                if (_roundedRectInnerPath != null)
                {
                    using (SolidBrush br = new(Program.Style.Schemes.Disabled.Colors.Back(parentLevel)))
                    {
                        G.FillPath(br, _roundedRectInnerPath);
                    }
                }
            }

            if (!DesignMode && Program.Settings.NerdStats.Enabled && !DontShowInfo)
            {
                G.TextRenderingHint = Program.Style.TextRenderingHint;

                Color targetColor = Enabled
                    ? (!HoverOverDefColorDot | !Program.Settings.NerdStats.DotDefaultChangedIndicator ? BackColor : DefaultBackColor)
                    : Program.Style.Schemes.Disabled.Colors.Line(parentLevel);

                Color fc0 = targetColor.IsDark() ? LineColor.LightLight() : LineColor.Dark(0.9f);
                Color fc1 = fc0;

                fc0 = Color.FromArgb(Program.Settings.NerdStats.MoreLabelTransparency ? 75 : 125, fc0);
                fc1 = Color.FromArgb(alpha, fc1);

                Rectangle rectX = Rect;
                rectX.Y += 1;

                string s;
                if (AfterDropEffect != AfterDropEffects.None)
                {
                    s = AfterDropEffect == AfterDropEffects.Invert ? Program.Localization.Strings.General.Invert :
                        AfterDropEffect == AfterDropEffects.Darker ? Program.Localization.Strings.General.Darken :
                        AfterDropEffect == AfterDropEffects.Lighter ? Program.Localization.Strings.General.Lighten :
                        Program.Localization.Strings.General.Blend;
                }
                else
                {
                    s = Enabled
                        ? targetColor.ToString(default, Program.Settings.NerdStats.ShowHexHash, true)
                        : Program.Localization.Strings.General.Disabled;
                }

                using (SolidBrush br = new(fc0)) { G.DrawString(s, Font, br, rectX, middleCenter); }
                using (SolidBrush br = new(fc1)) { G.DrawString(s, Font, br, rectX, middleCenter); }

                if (!DesignMode && DragDropMouseHovering && CanAnimate)
                {
                    if (_roundedRectPath != null)
                    {
                        using (GraphicsPath gp = new())
                        {
                            float i = Math.Max(Width, Height);
                            RectangleF mouseCircle = new(mousePosition.X - 0.5f * i, mousePosition.Y - 0.5f * i, i, i);
                            gp.AddEllipse(mouseCircle);

                            G.SetClip(gp);

                            using (SolidBrush br = new(DraggedColor.IsDark() ? DraggedColor.Light() : DraggedColor.Dark()))
                            {
                                G.DrawString(s, Font, br, rectX, middleCenter);
                            }

                            G.ResetClip();
                        }
                    }
                }

                Rectangle chevronRect = new(rectX.X, rectX.Y, rectX.Width - 5, rectX.Height);
                string chevron = ColorPickerOpened ? "▼" : string.Empty;

                using (SolidBrush br = new(fc0)) { G.DrawString(chevron, Font, br, chevronRect, chevronSF); }
                using (SolidBrush br = new(fc1)) { G.DrawString(chevron, Font, br, chevronRect, chevronSF); }
            }

            G.SmoothingMode = oldSmoothingMode;
            base.OnPaint(e);
        }
    }
}