using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.Controllers;
using WinPaletter.Properties;
using static WinPaletter.UI.Style.Config;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// TabsContainer is a UI control that renders and manages a set of tab-like
    /// items (TabData) backed by a standard WinForms TabControl. Responsibilities:
    /// - Maintain a lightweight cached TabDataList representing current tabs.
    /// - Handle input (mouse move/click/drag/drop) to support selection,
    ///   reordering, detaching and close-button interactions.
    /// - Render tabs with optimized painting (two-pass rendering to reduce overdraw)
    /// - Manage lightweight animations (selection, hover, progress) while avoiding
    ///   unnecessary allocations and GC pressure.
    ///
    /// Performance notes for maintainers:
    /// - Avoid LINQ allocations in hot paths (MouseMove/OnPaint/Timers). Use indexed
    ///   loops over TabDataList instead. This control is optimized to reduce GC spikes
    ///   caused by frequent mouse events and paint passes.
    /// - Hover processing is throttled to `_hoverProcessIntervalMs` to reduce CPU
    ///   spikes on flood MouseMove events while preserving visual responsiveness.
    /// - When updating per-tab visual flags (Hover/CloseButtonAlpha) always
    ///   Invalidate the affected rectangle to ensure immediate repaint and avoid
    ///   stale visuals.
    ///
    /// Note: This class intentionally avoids changing visual behaviour. Refactors
    /// should keep semantics identical while improving perf.
    /// </summary>
    public class TabsContainer : TitlebarExtender
    {
        public TabsContainer()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.ContainerControl | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            AllowDrop = true;

            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            InitializeContextMenu();
        }

        #region Variables

        public bool CanAnimate_Global => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public List<TabData> TabDataList = new();

        private static readonly int _maxTabWidth = 245;
        private static readonly int _paddingBetweenTabs = 5;
        private static readonly int _radius = 5;
        public int _upperTabPadding = 4;
        private static readonly int _closeButtonSize = 17;
        private static readonly int _iconSize = 16;
        private static readonly int _paddingPostIcon = 4;
        private static readonly int _paddingPreCloseButton = 5;
        private static readonly int _interval = 50;
        private static readonly int _rectSmallerOffset = 1;
        private static readonly int _rectSmallerWidthReduction = 2;
        private static readonly int _clipHeightOffset = 2;
        private static readonly int _smallerRadiusOffset = 1;
        private int _hoverSize;
        // Progress animation (marquee + highlight)
        private System.Windows.Forms.Timer _tabMarqueeTimer;
        private System.Windows.Forms.Timer _tabProgressTimer;
        private float _marqueeOffset = 0f;
        private float _hoverOffset = 0f;

        private int _scrollOffset = 0;
        private int _maxScrollOffset = 0;
        private int _animatedScrollOffset = 0;
        private static readonly int _scrollAmount = 100;
        private static readonly int _minimumIndicatorWidth = 20;
        private static readonly int _indicatorHeight = 2;
        private static readonly int _indicatorBottomOffset = 3;

        public int AnimatedScrollOffset
        {
            get => _animatedScrollOffset;
            set
            {
                if (_animatedScrollOffset != value)
                {
                    _animatedScrollOffset = value;
                    UpdateTabPositions(TabDataList);
                    Invalidate();
                }
            }
        }

        private int minTabWidth => (tabHeight - _iconSize) / 2 + _iconSize + _paddingPostIcon + _closeButtonSize + (tabHeight - _closeButtonSize) / 2;

        private bool forceChangeSelectedIndex = true;
        private bool overCloseButton = false;
        private bool skipTabDataRecreation = false;
        private bool _isResizing = false;

        private int moveFrom = -1;
        private int moveTo = -1;
        private bool isMovingTab = false;
        private bool isMovingToLast = false;
        private bool isMovingToFirst = false;

        private Point tabOldPoint = new();
        private Point locationOldPoint = new();
        private Point mouseDownPoint = new();

        private int _dragX = 0;

        private readonly UI.WP.ContextMenuStrip contextMenu = new() { ShowImageMargin = true, AllowTransparency = true };
        private TabData contextItemDropped;

        private Point hoverPosition;
        private TabData _hoveredTabData;
        // Timestamp of last hover processing to reduce CPU spikes from MouseMove floods
        private int _lastHoverTick = 0;
        // Minimum interval between consecutive hover processing (ms). Small value to keep responsiveness.
        private const int _hoverProcessIntervalMs = 8;
        private static TextureBrush _noise = new(Resources.Noise);

        public enum MouseState { None, Over, Down }
        public MouseState State = MouseState.None;

        Scheme scheme => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
        Scheme scheme_secondary => Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;

        private static StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat();
        private static StringFormat sf_middleCenter = ContentAlignment.MiddleCenter.ToStringFormat();
        private static string closeStr = "✕";
        private static Color win7BorderColor = Color.FromArgb(159, 255, 255, 255);
        private Font selectedFont;

        #endregion

        #region Methods

        private void InitializeContextMenu()
        {
            contextMenu.ItemHeight = 24;

            ToolStripMenuItem closeButton = new(Program.Localization.Strings.General.Close) { Image = Assets.Tabs.ContextBox_Close };
            ToolStripMenuItem closeAllButThis = new(Program.Localization.Strings.Tabs.Context_CloseOthers) { Image = Assets.Tabs.ContextBox_CloseAllButThis };
            ToolStripMenuItem closeAllToTheRight = new(Program.Localization.Strings.Tabs.Context_CloseToTheRight) { Image = Assets.Tabs.ContextBox_CloseRight };
            ToolStripMenuItem closeAllToTheLeft = new(Program.Localization.Strings.Tabs.Context_CloseToTheLeft) { Image = Assets.Tabs.ContextBox_CloseLeft };
            ToolStripMenuItem closeAll = new(Program.Localization.Strings.Tabs.Context_CloseAll) { Image = Assets.Tabs.ContextBox_CloseAll };
            ToolStripSeparator toolStripSeparator0 = new();
            ToolStripMenuItem detach = new(Program.Localization.Strings.Tabs.Context_Unpin) { Image = Assets.Tabs.ContextBox_Detach };
            ToolStripMenuItem detachAll = new(Program.Localization.Strings.Tabs.Context_UnpinAll) { Image = Assets.Tabs.ContextBox_DetachAll };
            ToolStripMenuItem detachAllButThis = new(Program.Localization.Strings.Tabs.Context_UnpinOthers) { Image = Assets.Tabs.ContextBox_DetachAllButThis };
            ToolStripSeparator toolStripSeparator1 = new();
            ToolStripMenuItem helpButton = new(Program.Localization.Strings.General.Help) { Image = Assets.Tabs.ContextBox_Help };

            closeButton.Click += (s, e) => contextItemDropped.Form.Close();
            closeAllButThis.Click += (s, e) => CloseAllTabsButThis();
            closeAllToTheRight.Click += (s, e) => CloseAllTabsToTheRight();
            closeAllToTheLeft.Click += (s, e) => CloseAllTabsToTheLeft();
            closeAll.Click += (s, e) => CloseAllTabs();
            detach.Click += (s, e) => DetachTab(contextItemDropped);
            detachAll.Click += (s, e) => DetachAllTabs();
            detachAllButThis.Click += (s, e) => DetachAllTabsButThis();
            helpButton.Click += (s, e) => TriggerHelp();

            contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                closeButton, closeAllToTheRight, closeAllToTheLeft, closeAll, closeAllButThis,
                toolStripSeparator0,
                detach, detachAll, detachAllButThis,
                toolStripSeparator1,
                helpButton
            });
        }

        /// <summary>
        /// Reset hover-related state for all tabs. This cancels any running transitions,
        /// sets hover and close-button alpha to zero, and invalidates tab rectangles
        /// to ensure stale hover/close visuals are repainted immediately.
        /// This method avoids LINQ and minimizes allocations by using indexed loops.
        /// </summary>
        private void ResetHoverStates()
        {
            if (TabDataList == null) return;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                var tabData = TabDataList[i];
                if (tabData == null) continue;

                tabData.CancelTransition(nameof(TabData.HoverAlpha));
                tabData.CancelTransition(nameof(TabData.CloseButtonAlpha));
                tabData.Hovered = false;
                bool needsInvalidate = tabData.CloseButtonAlpha != 0 || tabData.HoverAlpha != 0;
                tabData.CloseButtonAlpha = 0;
                if (needsInvalidate)
                {
                    try { Invalidate(tabData.Rectangle); } catch { }
                }
            }

            overCloseButton = false;
            _hoveredTabData = null;
        }

        private void HandleHoverForTab(MouseEventArgs e)
        {
            // Minimize hover processing frequency to reduce CPU spikes during fast mouse moves.
            if (_lastHoverTick != 0 && Environment.TickCount - _lastHoverTick < _hoverProcessIntervalMs) return;
            _lastHoverTick = Environment.TickCount;

            if (isMovingTab) return;

            Point mousePos = e.Location;

            // If mouse is outside horizontal visible region, clear hover state quickly.
            if (!IsPointInVisibleRegion(mousePos))
            {
                if (_hoveredTabData != null)
                {
                    _hoveredTabData = null;
                    if (CanAnimate_Global)
                        FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(Program.AnimationSpan_Quick);
                    else
                        HoverSize = 0;
                }

                ResetHoverStates();
                return;
            }

            // Find the tab under mouse without allocating temporary lists.
            TabData hoveredTab = null;
            Rectangle hoveredRect = Rectangle.Empty;
            for (int i = 0; i < TabDataList.Count; i++)
            {
                var tabData = TabDataList[i];
                if (tabData == null) continue;
                if (tabData.Rectangle.Contains(mousePos))
                {
                    hoveredTab = tabData;
                    hoveredRect = tabData.Rectangle;
                    break;
                }
            }

            if (hoveredTab != null)
            {
                bool overCloseBtn = closeRectangle(hoveredTab.Rectangle).Contains(mousePos);

                // Update close button alpha if changed and invalidate its area.
                int targetCloseAlpha = overCloseBtn ? 255 : 0;
                if (hoveredTab.CloseButtonAlpha != targetCloseAlpha)
                {
                    if (Program.Style.Animations)
                        FluentTransitions.Transition.With(hoveredTab, nameof(TabData.CloseButtonAlpha), targetCloseAlpha).CriticalDamp(Program.AnimationSpan_Quick);
                    else
                        hoveredTab.CloseButtonAlpha = targetCloseAlpha;

                    try { Invalidate(closeRectangle(hoveredTab.Rectangle)); } catch { }
                }

                overCloseButton = overCloseBtn;

                if (_hoveredTabData != hoveredTab)
                {
                    var previous = _hoveredTabData;
                    _hoveredTabData = hoveredTab;
                    hoverPosition = mousePos;

                    int defaultHoverSize = Math.Max(hoveredTab.Rectangle.Width, hoveredTab.Rectangle.Height);
                    if (CanAnimate_Global)
                        FluentTransitions.Transition.With(this, nameof(HoverSize), defaultHoverSize).CriticalDamp(Program.AnimationSpan_Quick);
                    else
                        HoverSize = defaultHoverSize;

                    // Ensure previous hovered tab is invalidated (clear hover visuals)
                    if (previous != null) try { Invalidate(previous.Rectangle); } catch { }
                }
                else
                {
                    hoverPosition = mousePos;
                }

                // Update Hovered flag on tabs and invalidate only those changed.
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var tabData = TabDataList[i];
                    if (tabData == null) continue;
                    bool shouldBeHovered = tabData == hoveredTab;
                    if (tabData.Hovered != shouldBeHovered)
                    {
                        tabData.Hovered = shouldBeHovered;
                        try { Invalidate(tabData.Rectangle); } catch { }
                    }
                }

                try { Invalidate(hoveredRect); } catch { }
            }
            else
            {
                if (_hoveredTabData != null)
                {
                    _hoveredTabData = null;
                    if (CanAnimate_Global)
                        FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(Program.AnimationSpan_Quick);
                    else
                        HoverSize = 0;
                }

                ResetHoverStates();
            }
        }

        public void AddFormIntoTab(System.Windows.Forms.Form form)
        {
            Cursor = Cursors.WaitCursor;

            foreach (TabPage TPx in TabControl.TabPages)
            {
                if (TPx.Controls.Contains(form))
                {
                    TabControl.SelectedTab = TPx;
                    Cursor = Cursors.Default;
                    Program.Log?.Write(LogEventLevel.Information, $"`{form.Name}` form is already shown and added into tabs, re-focusing it.");
                    return;
                }
            }

            if (busy)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"`{form.Name}` form cannot be added into tabs as the tabs container is busy.");
                Cursor = Cursors.Default;
                return;
            }

            busy = true;

            TabPage TP = new() { BackColor = BackColor };

            form.TopLevel = false;
            form.Parent = TP;

            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.AllowDrop = true;
            form.WindowState = FormWindowState.Normal;
            form.Opacity = 0;
            form.Visible = false;

            TP.Text = form.Text;

            if (!DesignMode && !TabControl.IsInUse()) Program.Animator.HideSync(TabControl);

            forceChangeSelectedIndex = true;
            TabControl.TabPages.Add(TP);
            SelectedTab = TP;

            if (TabDataList.Count > 0 && TabDataList[TabDataList.Count - 1] != null)
            {
                skipTabDataRecreation = true;
                var finalTab = TabDataList[TabDataList.Count - 1];
                // Ensure Show() runs on UI thread so FluentTransitions executes properly
                _ = finalTab.Show(() => Invalidate()).ContinueWith(_ =>
                {
                    if (this.IsHandleCreated)
                        this.BeginInvoke(() => skipTabDataRecreation = false);
                });
            }

            TP.Controls.Add(form);
            form.Show();
            form.Opacity = 1;

            if (FindForm() is not null) TabControl.FindForm().Visible = true;

            if (form is AspectsTemplate)
            {
                (form as AspectsTemplate).titlebarExtender1.Flag = TitlebarExtender.Flags.Tabs_Extended;
            }
            else if (form is System.Windows.Forms.Form && form.Controls.OfType<TitlebarExtender>().Any())
            {
                form.Controls.OfType<TitlebarExtender>().FirstOrDefault().Flag = TitlebarExtender.Flags.Tabs_Extended;
            }

            if (!DesignMode && !TabControl.IsInUse()) Program.Animator.ShowSync(TabControl);

            Program.Log?.Write(LogEventLevel.Information, $"`{form.Name}` form has been shown and added into tabs.");

            busy = false;

            Cursor = Cursors.Default;
        }

        private int tabWidth
        {
            get
            {
                if (_tabControl == null || _tabControl.TabPages.Count == 0)
                    return _maxTabWidth;

                int availableWidth = RightBoundary - LeftBoundary;
                int totalPadding = _paddingBetweenTabs * (_tabControl.TabPages.Count - 1);
                int availableForTabs = availableWidth - totalPadding;

                if (availableForTabs < minTabWidth * _tabControl.TabPages.Count)
                    availableForTabs = minTabWidth * _tabControl.TabPages.Count;

                double calculatedWidth = (double)availableForTabs / _tabControl.TabPages.Count;
                int finalWidth = (int)Math.Floor(calculatedWidth);

                return Math.Max(minTabWidth, Math.Min(_maxTabWidth, finalWidth));
            }
        }
        private int tabHeight => Height - _upperTabPadding;

        private int LeftBoundary => _paddingBetweenTabs;

        private int RightBoundary
        {
            get
            {
                if (Program.IsBeta)
                {
                    SizeF betaSize = Program.Localization.Strings.General.Beta.ToUpper().Measure(Fonts.ConsoleMedium) + new SizeF(2, 2);
                    return Width - (int)betaSize.Width - 5 - _paddingBetweenTabs;
                }
                else
                {
                    return Width - _paddingBetweenTabs;
                }
            }
        }

        public int TabsCount => TabDataList != null ? TabDataList.Count : 0;

        private Rectangle closeRectangle(Rectangle rectangle)
        {
            return new Rectangle(rectangle.Right - _closeButtonSize - (rectangle.Height - _closeButtonSize) / 2, rectangle.Y + (rectangle.Height - _closeButtonSize) / 2, _closeButtonSize, _closeButtonSize);
        }

        private Rectangle iconRectangle(Rectangle rectangle)
        {
            int top = rectangle.Y + (rectangle.Height - _iconSize) / 2;
            int paddingLeft = (rectangle.Height - _iconSize) / 2;
            return new Rectangle(rectangle.X + paddingLeft, top, _iconSize, _iconSize);
        }

        private Rectangle titleRectangle(Rectangle rectangle)
        {
            Rectangle iconRect = iconRectangle(rectangle);
            Rectangle closeRect = closeRectangle(rectangle);

            return Rectangle.FromLTRB(iconRect.Right + _paddingPostIcon, iconRect.Top, closeRect.Left - _paddingPreCloseButton, iconRect.Bottom);
        }

        private int GetIndex(TabData tabData)
        {
            if (TabDataList == null || TabDataList.Count == 0)
                return -1;

            return TabDataList.FindIndex(t => t?.Rectangle == tabData.Rectangle);
        }

        private void UpdateTabs()
        {
            // Dispose any existing TabData to avoid leaking resources (timers, images, event handlers).
            DisposeAllTabs();

            if (_tabControl != null)
            {
                int i = 0;

                foreach (TabPage page in _tabControl.TabPages)
                {
                    TabDataList.Add(CreateTabData(page, i));
                    i++;
                }
            }
        }

        /// <summary>
        /// Safely dispose a TabData instance and release its resources.
        /// This centralizes disposal logic so all removal paths can use the same behavior.
        /// </summary>
        /// <param name="tabData">TabData to dispose. Safe to pass null.</param>
        private void DisposeTabData(TabData tabData)
        {
            if (tabData == null) return;

            try
            {
                tabData.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Dispose all TabData instances currently tracked and clear the list.
        /// Use this when rebuilding the list or when disposing the control.
        /// </summary>
        private void DisposeAllTabs()
        {
            if (TabDataList == null) return;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                var t = TabDataList[i];
                if (t == null) continue;
                try { t.Dispose(); } catch { }
            }

            TabDataList.Clear();
        }

        private void SwapTabs(int from, int to)
        {
            if (TabDataList == null) return;

            if (from < 0 || from >= TabDataList.Count) return;
            if (to < 0 || to >= TabDataList.Count) return;

            TabData itemFrom = TabDataList[from];
            TabData itemTo = TabDataList[to];

            if (itemFrom == null || itemTo == null) return;
            if (itemFrom.IsRemoving || itemTo.IsRemoving) return;

            TabDataList[from] = itemTo;
            TabDataList[to] = itemFrom;

            forceChangeSelectedIndex = true;
            SelectedIndex = to;

            // Don't reset SelectionAlpha during swap - preserve current state
            UpdateTabPositions(TabDataList, preserveSelectionAlpha: true, animateWidth: false, animateSelection: false);

            NormalizeSelectionAlpha();

            isMovingTab = false;

            ResetModifiersToNull();

            Refresh();
        }

        private void MoveToLast(int from)
        {
            if (TabDataList == null) return;

            TabData item = TabDataList[from];

            if (item == null || item.IsRemoving) return;

            TabDataList.RemoveAt(from);
            TabDataList.Add(item);

            forceChangeSelectedIndex = true;
            SelectedIndex = TabDataList.Count - 1;

            UpdateTabPositions(TabDataList, preserveSelectionAlpha: true, animateWidth: false, animateSelection: false);

            NormalizeSelectionAlpha();

            ResetModifiersToNull();

            Refresh();
        }

        private void MoveToFirst(int from)
        {
            if (TabDataList == null) return;

            TabData item = TabDataList[from];

            if (item == null || item.IsRemoving) return;

            TabDataList.RemoveAt(from);
            TabDataList.Insert(0, item);

            forceChangeSelectedIndex = true;
            SelectedIndex = 0;

            UpdateTabPositions(TabDataList, preserveSelectionAlpha: true, animateWidth: false, animateSelection: false);

            NormalizeSelectionAlpha();

            ResetModifiersToNull();

            Refresh();
        }

        private void ResetModifiersToNull()
        {
            moveFrom = -1;
            moveTo = -1;
            isMovingToLast = false;
            isMovingToFirst = false;
            isMovingTab = false;
            _dragX = 0;
            Invalidate();
        }

        private TabData GetTabAtMousePosition(MouseEventArgs e)
        {
            if (!IsPointInVisibleRegion(e.Location))
            {
                return null;
            }

            for (int i = 0; i < TabDataList.Count; i++)
            {
                var tabData = TabDataList[i];
                if (tabData == null) continue;
                if (tabData.Rectangle.Contains(e.Location))
                {
                    return tabData;
                }
            }

            return null;
        }

        private TabData CreateTabData(TabPage page, int index, int? tabTop = null)
        {
            int tabX = LeftBoundary + index * (tabWidth + _paddingBetweenTabs) - _animatedScrollOffset;
            int tabW = tabWidth;
            int tabY = tabTop ?? _upperTabPadding;

            Rectangle tabRectangle = new(tabX, tabY, tabW, tabHeight);
            TabData tabData = new(this, page, tabRectangle);

            tabData.TabLeft = tabX;
            tabData.TabWidth = tabW;

            return tabData;
        }

        private void CalculateScrollOffset()
        {
            if (TabDataList == null || TabDataList.Count == 0)
            {
                _maxScrollOffset = 0;
                _scrollOffset = 0;
                _animatedScrollOffset = 0;
                return;
            }

            int totalTabWidth = TabDataList.Count * tabWidth + _paddingBetweenTabs * (TabDataList.Count - 1);
            int availableWidth = RightBoundary - LeftBoundary;

            if (totalTabWidth <= availableWidth)
            {
                _maxScrollOffset = 0;
                _scrollOffset = 0;
            }
            else
            {
                _maxScrollOffset = totalTabWidth - availableWidth;
                _scrollOffset = Math.Min(_scrollOffset, _maxScrollOffset);
            }

            int lastTabRight = LeftBoundary + (TabDataList.Count - 1) * (tabWidth + _paddingBetweenTabs) + tabWidth - _scrollOffset;
            if (lastTabRight > RightBoundary)
            {
                _scrollOffset = lastTabRight - RightBoundary;
                _scrollOffset = Math.Min(_scrollOffset, _maxScrollOffset);
            }
        }

        private bool IsPointInVisibleRegion(Point point)
        {
            return point.X >= LeftBoundary && point.X <= RightBoundary;
        }

        private bool IsTabVisible(Rectangle tabRect)
        {
            Rectangle visibleRegion = new(LeftBoundary, 0, RightBoundary - LeftBoundary, Height);
            return tabRect.IntersectsWith(visibleRegion);
        }

        private void AnimateScrollOffset(int targetOffset)
        {
            _scrollOffset = Math.Max(0, Math.Min(targetOffset, _maxScrollOffset));

            if (CanAnimate_Global)
            {
                FluentTransitions.Transition.With(this, nameof(AnimatedScrollOffset), _scrollOffset).CriticalDamp(Program.AnimationSpan_Quick);
            }
            else
            {
                AnimatedScrollOffset = _scrollOffset;
            }
        }

        // Progress timer management for marquee animation across tabs
        private void EnsureTabTimersInitialized()
        {
            if (_tabMarqueeTimer != null && _tabProgressTimer != null) return;

            // Marquee timer: match ProgressBar default behaviour
            if (_tabMarqueeTimer == null)
            {
                int marqueeInterval = Math.Max(1, 2000 / 100); // MarqueeAnimationSpeed default = 100
                _tabMarqueeTimer = new System.Windows.Forms.Timer { Interval = marqueeInterval };
                _tabMarqueeTimer.Tick += (s, e) =>
                {
                    _marqueeOffset += 0.02f;
                    if (_marqueeOffset > 1f) _marqueeOffset = 0f;
                    // Reduce allocations by avoiding LINQ Any; use indexed loop.
                    if (TabDataList != null)
                    {
                        for (int ii = 0; ii < TabDataList.Count; ii++)
                        {
                            var t = TabDataList[ii];
                            if (t != null && t.ProgressMarquee)
                            {
                                Invalidate();
                                break;
                            }
                        }
                    }
                };
            }

            // Highlight/progress timer: match ProgressBar highlight behaviour
            if (_tabProgressTimer == null)
            {
                int progressInterval = Math.Max(1, 3500 / 300); // HighlightAnimationSpeed default = 300
                _tabProgressTimer = new System.Windows.Forms.Timer { Interval = progressInterval };
                _tabProgressTimer.Tick += (s, e) =>
                {
                    _hoverOffset += 0.01f;
                    if (_hoverOffset > 1f) _hoverOffset = 0f;
                    if (TabDataList != null)
                    {
                        for (int ii = 0; ii < TabDataList.Count; ii++)
                        {
                            var t = TabDataList[ii];
                            if (t != null && t.ProgressEnabled && t.ProgressValue > 0)
                            {
                                Invalidate();
                                break;
                            }
                        }
                    }
                };
            }
        }

        internal void StartProgressTimer()
        {
            EnsureTabTimersInitialized();
            try
            {
                if (_tabMarqueeTimer != null && !_tabMarqueeTimer.Enabled) _tabMarqueeTimer.Start();
                if (_tabProgressTimer != null && !_tabProgressTimer.Enabled) _tabProgressTimer.Start();
            }
            catch { }
        }

        internal void StopProgressTimer()
        {
            try
            {
                if (_tabMarqueeTimer != null && _tabMarqueeTimer.Enabled) _tabMarqueeTimer.Stop();
                if (_tabProgressTimer != null && _tabProgressTimer.Enabled) _tabProgressTimer.Stop();
            }
            catch { }
            _marqueeOffset = 0f;
            _hoverOffset = 0f;
        }

        private void NormalizeSelectionAlpha()
        {
            if (TabDataList == null) return;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                TabData tab = TabDataList[i];
                if (tab == null) continue;

                bool selected = i == _selectedIndex;

                if (!selected)
                {
                    tab.Selected = false;

                    if (!(CanAnimate_Global && !_isResizing))
                    {
                        tab.SelectionAlpha = 0;
                    }
                }
                else
                {
                    // Only set Selected flag, don't touch SelectionAlpha
                    tab.Selected = true;
                }
            }
        }

        /// <summary>
        /// Update tab positions with option to skip selection animation
        /// </summary>
        private void UpdateTabPositions(List<TabData> collection, bool preserveSelectionAlpha = true, bool animateWidth = false, bool preserveTabTop = false, bool animateSelection = true)
        {
            int collectionCount = collection.Count;

            if (collectionCount == 0) return;

            CalculateScrollOffset();

            if (_selectedIndex < 0 || _selectedIndex >= collectionCount) return;

            List<TabData> oldTabs = [.. collection];
            collection.Clear();

            Point currentMousePos = PointToClient(MousePosition);

            int i = 0;

            foreach (TabData oldTab in oldTabs)
            {
                if (oldTab == null || oldTab.TabPage == null || oldTab.IsRemoving) continue;

                int? preservedTabTop = null;

                if (preserveTabTop || skipTabDataRecreation)
                {
                    preservedTabTop = oldTab.TabTop;
                }

                TabData newTab = CreateTabData(oldTab.TabPage, i, preservedTabTop);

                bool shouldBeSelected = i == _selectedIndex;

                newTab.RemovingAlpha = oldTab.RemovingAlpha;
                newTab.CloseButtonAlpha = oldTab.CloseButtonAlpha;

                int naturalLeft = LeftBoundary + i * (tabWidth + _paddingBetweenTabs) - _animatedScrollOffset;
                newTab.TabLeft = naturalLeft;
                newTab.TabWidth = tabWidth;

                newTab.Hovered = IsPointInVisibleRegion(currentMousePos) && newTab.Rectangle.Contains(currentMousePos);

                // Cancel any existing selection alpha transitions
                newTab.CancelTransition(nameof(TabData.SelectionAlpha));

                if (shouldBeSelected)
                {
                    if (preserveSelectionAlpha)
                    {
                        // Keep existing alpha without resetting
                        newTab.SelectionAlpha = oldTab.SelectionAlpha;
                        newTab.Selected = true;
                    }
                    else
                    {
                        // For new tabs or initial setup, start from 0
                        newTab.SelectionAlpha = 0;
                        newTab.Selected = false;

                        if (animateSelection && CanAnimate_Global && !_isResizing)
                        {
                            FluentTransitions.Transition.With(newTab, nameof(TabData.SelectionAlpha), 255).CriticalDamp(Program.AnimationSpan_Quick);
                        }
                        else
                        {
                            // During resize or when animation is disabled, set immediately
                            newTab.SelectionAlpha = 255;
                            newTab.Selected = true;
                        }
                    }
                }
                else
                {
                    newTab.SelectionAlpha = 0;
                    newTab.Selected = false;
                }

                collection.Add(newTab);
                i++;
            }

            NormalizeSelectionAlpha();
        }

        private int AdjustSelectedIndex(int value)
        {
            if (TabDataList != null && TabDataList.Count > 0)
            {
                if (value > TabDataList.Count - 1) return TabDataList.Count - 1;
            }

            return value;
        }

        private void UpdateSelectedTab()
        {
            if (TabDataList != null && TabDataList.Count > 0)
            {
                if (_selectedIndex > -1)
                {
                    if (_tabControl != null && _tabControl.SelectedTab != TabDataList[_selectedIndex].TabPage)
                    {
                        _tabControl.SelectedTab = TabDataList[_selectedIndex].TabPage;
                    }

                    foreach (TabData t in TabDataList)
                    {
                        bool shouldBeSelected = TabDataList[_selectedIndex].Form == t.Form && !t.IsRemoving;

                        if (t.Selected != shouldBeSelected)
                        {
                            t.CancelTransition(nameof(TabData.SelectionAlpha));

                            if (shouldBeSelected)
                            {
                                if (CanAnimate_Global && !_isResizing)
                                {
                                    t.SelectionAlpha = 0; // start from 0 so TabData.Selected animates to 255
                                }
                                else
                                {
                                    t.SelectionAlpha = 255;
                                }

                                t.Selected = true;
                            }
                            else
                            {
                                t.Selected = false; // TabData.Selected will animate to 0 if allowed

                                if (!(CanAnimate_Global && !_isResizing))
                                {
                                    t.SelectionAlpha = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (_tabControl != null && _tabControl.SelectedTab != null)
                    {
                        _tabControl.SelectedTab = null;
                    }
                }
            }
        }

        private bool IsMouseOverTab(TabData tabData)
        {
            Point mousePos = PointToClient(MousePosition);
            return IsPointInVisibleRegion(mousePos) && tabData.Rectangle.Contains(mousePos);
        }

        private void ProcessTabMouseActions(TabData tabData, MouseEventArgs e)
        {
            if (isMovingTab) return;

            if (overCloseButton)
            {
                HandleCloseButtonClick(tabData);
            }
            else if (e.Button != MouseButtons.Right)
            {
                HandleTabLeftButtonClick(tabData);
            }
            else
            {
                HandleTabRightButtonClick(tabData);
            }
        }

        private void HandleCloseButtonClick(TabData tabData)
        {
            tabData.Form.Close();
        }

        private void HandleTabControlMouseButtonClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (TabDataList.Count > 0)
                {
                    for (int i = 0; i < TabDataList.Count; i++)
                    {
                        var tabData = TabDataList[i];
                        if (tabData == null || tabData.IsRemoving) continue;

                        if (IsMouseOverTab(tabData) && !IsMouseOverCloseButton(tabData, e))
                        {
                            moveFrom = GetIndex(tabData);
                            moveTo = moveFrom;
                            _dragX = tabData.Rectangle.Left;
                            tabOldPoint = new Point(PointToClient(MousePosition).X - tabData.Rectangle.Left, PointToClient(MousePosition).Y - tabData.Rectangle.Top);
                            mouseDownPoint = PointToClient(MousePosition);

                            // Selection is handled on mouse click/up instead of mouse down.

                            break;
                        }
                        else
                        {
                            locationOldPoint = MousePosition - (Size)FindForm()?.Location;
                        }
                    }
                }
                else
                {
                    locationOldPoint = MousePosition - (Size)FindForm()?.Location;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var tabData = TabDataList[i];
                    if (tabData == null) continue;
                    if (IsMouseOverTab(tabData))
                    {
                        Point mousePos = e.Location;
                        tabData.Form.Close();

                        System.Windows.Forms.Timer timer = new() { Interval = _interval };
                        timer.Tick += (s, args) =>
                        {
                            timer.Stop();
                            timer.Dispose();

                            Point currentMousePos = PointToClient(MousePosition);
                            HandleHoverForTab(new MouseEventArgs(MouseButtons.None, 0, currentMousePos.X, currentMousePos.Y, 0));
                        };
                        timer.Start();
                        break;
                    }
                }
            }
        }

        private void HandleTabControlMouseDoubleClick(MouseEventArgs e)
        {
            bool hasDetachedAnyTab = false;

            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var tabData = TabDataList[i];
                    if (tabData == null) continue;
                    if (IsMouseOverTab(tabData))
                    {
                        hasDetachedAnyTab = true;
                        DetachTab(tabData);
                        break;
                    }
                }
            }

            if (!hasDetachedAnyTab)
            {
                System.Windows.Forms.Form form = TabControl.FindForm();
                if (form != null)
                {
                    form.WindowState = form.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                }
            }
        }

        private void HandleTabLeftButtonClick(TabData tabData)
        {
            if (tabData.TabPage != _tabControl.SelectedTab)
            {
                SelectedIndex = GetIndex(tabData);

                if (_maxScrollOffset > 0)
                {
                    int tabIndex = GetIndex(tabData);
                    int tabLeftPosition = tabIndex * tabWidth + _paddingBetweenTabs;
                    int tabRightPosition = tabLeftPosition + tabWidth;

                    int targetScrollOffset = _scrollOffset;

                    if (tabLeftPosition - _animatedScrollOffset < LeftBoundary)
                    {
                        targetScrollOffset = tabLeftPosition - LeftBoundary;
                    }
                    else if (tabRightPosition - _animatedScrollOffset > RightBoundary)
                    {
                        targetScrollOffset = tabRightPosition - RightBoundary;
                    }

                    targetScrollOffset = Math.Max(0, Math.Min(targetScrollOffset, _maxScrollOffset));

                    if (targetScrollOffset != _scrollOffset)
                    {
                        AnimateScrollOffset(targetScrollOffset);
                    }
                }
            }
        }

        private void HandleTabRightButtonClick(TabData tabData)
        {
            contextItemDropped = tabData;
            contextMenu.Show(this, PointToClient(MousePosition));
        }

        private bool IsMouseOverCloseButton(TabData tabData, MouseEventArgs e)
        {
            return closeRectangle(tabData.Rectangle).Contains(e.Location);
        }

        private void HandleTabControlMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && moveFrom != -1)
            {
                if (!isMovingTab)
                {
                    Point current = PointToClient(MousePosition);
                    int dx = current.X - mouseDownPoint.X;
                    int dy = current.Y - mouseDownPoint.Y;
                    if (dx * dx + dy * dy < 4) // require ~2 pixels movement before starting drag
                    {
                        return;
                    }
                }

                HandleTabMove(e);
            }
            else if (e.Button == MouseButtons.Left && FindForm() != null && !overCloseButton)
            {
                HandleFormMove();
            }
        }

        private void HandleTabMove(MouseEventArgs e)
        {
            if (TabDataList == null || moveFrom < 0 || moveFrom >= TabDataList.Count) return;

            TabData dragged = TabDataList[moveFrom];
            if (dragged == null || dragged.IsRemoving) return;

            isMovingTab = true;

            _dragX = PointToClient(MousePosition).X - tabOldPoint.X;

            Point screenMousePos = MousePosition;
            Form parentForm = FindForm();

            if (parentForm != null)
            {
                Rectangle parentBounds = parentForm.Bounds;

                if (screenMousePos.X < parentBounds.Left || screenMousePos.X > parentBounds.Right || screenMousePos.Y < parentBounds.Top || screenMousePos.Y > parentBounds.Bottom)
                {
                    Capture = false;

                    TabData detachedTab = dragged;
                    int detachedIndex = moveFrom;

                    ResetModifiersToNull();

                    BeginInvoke(() =>
                    {
                        if (detachedTab != null && detachedIndex >= 0 && detachedIndex < TabDataList.Count)
                        {
                            // Remove the detached tab and animate remaining tabs to their new positions
                            TabDataList.RemoveAt(detachedIndex);

                            if (_selectedIndex >= TabDataList.Count)
                            {
                                _selectedIndex = Math.Max(0, TabDataList.Count - 1);
                            }

                            // Recalculate scroll offsets
                            CalculateScrollOffset();

                            for (int ii = 0; ii < TabDataList.Count; ii++)
                            {
                                var t = TabDataList[ii];
                                if (t == null) continue;

                                int naturalLeft = LeftBoundary + ii * (tabWidth + _paddingBetweenTabs) - _animatedScrollOffset;
                                t.TabWidth = tabWidth;

                                if (t.TabLeft != naturalLeft)
                                {
                                    if (CanAnimate_Global)
                                    {
                                        FluentTransitions.Transition.With(t, nameof(TabData.TabLeft), naturalLeft).CriticalDamp(Program.AnimationSpan_Quick);
                                    }
                                    else
                                    {
                                        t.TabLeft = naturalLeft;
                                    }
                                }
                            }

                            if (_tabControl != null && detachedTab.TabPage != null)
                            {
                                _tabControl.TabPages.Remove(detachedTab.TabPage);
                            }
                        }

                        moveFrom = -1;
                        moveTo = -1;
                        isMovingTab = false;
                        isMovingToFirst = false;
                        isMovingToLast = false;
                        _dragX = 0;

                        DetachTabWithPosition(detachedTab, screenMousePos);

                        if (detachedTab?.Form != null)
                        {
                            User32.ReleaseCapture();
                            User32.SendMessage(detachedTab.Form.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                        }

                        Invalidate();
                    });

                    return;
                }
            }

            if (_dragX < LeftBoundary)
            {
                _dragX = LeftBoundary;
            }
            else if (_dragX + dragged.Rectangle.Width > RightBoundary)
            {
                _dragX = RightBoundary - dragged.Rectangle.Width;
            }

            int newMoveTo = moveFrom;

            // Swap decision based on overlap threshold (half width) relative to drag direction.
            // When dragging left (moving a tab from right to left), require the dragged tab's left border to overlap the target (left neighbor) by at least half the neighbor's width.
            // When dragging right, require the dragged tab's right border to overlap the target (right neighbor) by at least half the neighbor's width.
            for (int i = 0; i < TabDataList.Count; i++)
            {
                if (i == moveFrom) continue;
                TabData other = TabDataList[i];
                if (other == null || other.IsRemoving) continue;

                // position of other tab in client coords
                int otherLeft = LeftBoundary + i * (tabWidth + _paddingBetweenTabs);
                int otherRight = otherLeft + other.Rectangle.Width;

                // dragged left and right in client coords
                int draggedLeft = _dragX;
                int draggedRight = _dragX + dragged.Rectangle.Width;

                if (i < moveFrom)
                {
                    // other is to the left; check overlap amount of other with dragged
                    int overlap = otherRight - draggedLeft; // positive when dragged overlaps into other
                    if (overlap >= other.Rectangle.Width / 2)
                    {
                        newMoveTo = i;
                        break;
                    }
                }
                else if (i > moveFrom)
                {
                    // other is to the right; check overlap of dragged's right with other
                    int overlap = draggedRight - otherLeft;
                    if (overlap >= other.Rectangle.Width / 2)
                    {
                        newMoveTo = i;
                    }
                }
            }

            if (_dragX <= LeftBoundary && moveFrom != 0)
            {
                isMovingToFirst = true;
                isMovingToLast = false;
                newMoveTo = 0;
            }
            else if (_dragX + dragged.Rectangle.Width >= RightBoundary && moveFrom != TabDataList.Count - 1)
            {
                isMovingToLast = true;
                isMovingToFirst = false;
                newMoveTo = TabDataList.Count - 1;
            }
            else
            {
                isMovingToFirst = false;
                isMovingToLast = false;
            }

            if (newMoveTo != moveTo)
            {
                moveTo = newMoveTo;
                AnimateNeighborTabsForDrag();
            }

            Invalidate();
        }

        private void AnimateNeighborTabsForDrag()
        {
            if (TabDataList == null || moveFrom < 0 || moveTo < 0) return;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                if (i == moveFrom) continue;

                TabData tab = TabDataList[i];
                if (tab == null || tab.IsRemoving) continue;

                int logicalSlot = i;

                if (moveFrom < moveTo)
                {
                    if (i > moveFrom && i <= moveTo) logicalSlot = i - 1;
                }
                else if (moveFrom > moveTo)
                {
                    if (i >= moveTo && i < moveFrom) logicalSlot = i + 1;
                }

                int targetLeft = LeftBoundary + logicalSlot * (tabWidth + _paddingBetweenTabs);

                if (tab.TabLeft != targetLeft)
                {
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(tab, nameof(TabData.TabLeft), targetLeft).CriticalDamp(Program.AnimationSpan_Quick);
                    }
                    else
                    {
                        tab.TabLeft = targetLeft;
                    }
                }
            }
        }

        private void ResetAllTabLeftPositions()
        {
            if (TabDataList == null) return;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                TabData tab = TabDataList[i];
                if (tab == null || tab.IsRemoving) continue;

                int naturalLeft = LeftBoundary + i * (tabWidth + _paddingBetweenTabs);

                if (tab.TabLeft != naturalLeft)
                {
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(tab, nameof(TabData.TabLeft), naturalLeft).CriticalDamp(Program.AnimationSpan_Quick);
                    }
                    else
                    {
                        tab.TabLeft = naturalLeft;
                    }
                }
            }
        }

        private void HandleFormMove()
        {
            Point locationNewPoint = MousePosition - (Size)locationOldPoint;
            FindForm()?.Location = locationNewPoint;
        }

        private void CloseAllTabsButThis()
        {
            if (TabDataList.Count <= 1) return;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                var tabData = TabDataList[i];
                if (tabData == null) continue;
                if (tabData.TabPage != contextItemDropped.TabPage)
                {
                    tabData.Form.Close();
                }
            }
        }

        private void CloseAllTabsToTheRight()
        {
            int index = TabDataList.IndexOf(contextItemDropped);
            List<TabData> tabsToClose = [];
            for (int i = index + 1; i < TabDataList.Count; i++)
            {
                tabsToClose.Add(TabDataList[i]);
            }
            foreach (TabData tabData in tabsToClose)
            {
                tabData.Form.Close();
            }
        }

        private void CloseAllTabsToTheLeft()
        {
            int index = TabDataList.IndexOf(contextItemDropped);
            List<TabData> tabsToClose = [];
            for (int i = 0; i < index; i++)
            {
                tabsToClose.Add(TabDataList[i]);
            }
            foreach (TabData tabData in tabsToClose)
            {
                tabData.Form.Close();
            }
        }

        private void CloseAllTabs()
        {
            for (int i = 0; i < TabDataList.Count; i++)
            {
                var tabData = TabDataList[i];
                if (tabData == null) continue;
                tabData.Form.Close();
            }
        }

        private void DetachTab(TabData tabData)
        {
            if (tabData.Form != null) DetachForm(tabData.Form);

            RemoveTab(tabData, false);

            if (TabDataList.Count == 0 && FindForm() is not null) TabControl.FindForm().Visible = false;

            tabData.Form.Visible = true;
        }

        private void DetachAllTabs()
        {
            for (int i = 0; i < TabDataList.Count; i++)
            {
                var tab = TabDataList[i];
                if (tab?.Form != null) DetachTab(tab);
            }
        }

        private void DetachAllTabsButThis()
        {
            if (TabDataList.Count > 1)
            {
                DetachAllTabs();
                if (contextItemDropped.Form != null) AddFormIntoTab(contextItemDropped.Form);
            }
        }

        private void TriggerHelp()
        {
            IntPtr intPtr = SelectedTabData?.Form?.Handle ?? IntPtr.Zero;
            if (intPtr != IntPtr.Zero)
            {
                User32.SendMessage(intPtr, 0x0112, 0xF180, 0);
            }
        }

        private void TriggerSystemContextMenu()
        {
            IntPtr formHandle = FindForm()?.Handle ?? IntPtr.Zero;
            if (formHandle != IntPtr.Zero)
            {
                IntPtr systemMenu = User32.GetSystemMenu(formHandle, false);
                if (systemMenu != IntPtr.Zero)
                {
                    Point cursorPos = Control.MousePosition;
                    const uint TPM_LEFTBUTTON = 0x0000;
                    const uint TPM_RETURNCMD = 0x0100;
                    uint result = User32.TrackPopupMenuEx(systemMenu, TPM_LEFTBUTTON | TPM_RETURNCMD, cursorPos.X, cursorPos.Y, formHandle, IntPtr.Zero);
                    if (result != 0)
                    {
                        User32.SendMessage(formHandle, 0x0112, new(result), IntPtr.Zero);
                    }
                }
            }
        }

        private void DetachForm(System.Windows.Forms.Form form)
        {
            form.Visible = false;
            form.Parent?.Controls.Remove(form);
            form.TopLevel = true;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.WindowState = FormWindowState.Normal;
            form.CenterToScreen();

            if (form is AspectsTemplate)
            {
                (form as AspectsTemplate).titlebarExtender1.Flag = TitlebarExtender.Flags.System;
            }
            else if (form is System.Windows.Forms.Form && form.Controls.OfType<TitlebarExtender>().Any())
            {
                form.Controls.OfType<TitlebarExtender>().FirstOrDefault().Flag = TitlebarExtender.Flags.System;
            }

            ApplyStyle(form);
            form.BringToFront();
            form?.Activate();
            form?.Focus();
            form?.BringToFront();
            form.Show();
        }

        private void DetachForm(System.Windows.Forms.Form form, Point screenPosition)
        {
            form.Visible = false;
            form.Parent?.Controls.Remove(form);
            form.TopLevel = true;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.WindowState = FormWindowState.Normal;

            form.Location = new Point(screenPosition.X - form.Width / 2, screenPosition.Y - 5);

            if (form is AspectsTemplate)
            {
                (form as AspectsTemplate).titlebarExtender1.Flag = TitlebarExtender.Flags.System;
            }
            else if (form is System.Windows.Forms.Form && form.Controls.OfType<TitlebarExtender>().Any())
            {
                form.Controls.OfType<TitlebarExtender>().FirstOrDefault().Flag = TitlebarExtender.Flags.System;
            }

            ApplyStyle(form);
            form.BringToFront();
            form?.Activate();
            form?.Focus();
            form?.BringToFront();
            form.Show();
        }

        private void DetachTabWithPosition(TabData tabData, Point screenPosition)
        {
            if (tabData.Form != null) DetachForm(tabData.Form, screenPosition);

            RemoveTab(tabData, false);

            if (TabDataList.Count == 0 && FindForm() is not null) TabControl.FindForm().Visible = false;

            tabData.Form.Visible = true;
        }

        #endregion

        #region Properties

        public TabControl TabControl
        {
            get => _tabControl;
            set
            {
                if (_tabControl != value)
                {
                    _tabControl = value;
                    _tabControl.SelectedIndexChanged += _tabControl_SelectedIndexChanged;
                    _tabControl.ControlAdded += _tabControl_ControlAdded;
                    _tabControl.ControlRemoved += _tabControl_ControlRemoved;
                    UpdateTabs();
                    SelectedIndex = 0;
                }
            }
        }
        private TabControl _tabControl;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex != value || forceChangeSelectedIndex)
                {
                    forceChangeSelectedIndex = false;
                    _selectedIndex = AdjustSelectedIndex(value);
                    UpdateSelectedTab();
                }
            }
        }
        private int _selectedIndex;

        public TabPage SelectedTab
        {
            get => _selectedIndex >= 0 && _selectedIndex < TabDataList?.Count ? TabDataList[_selectedIndex].TabPage : null;
            set
            {
                if (_tabControl != null)
                {
                    _tabControl.SelectedTab = value;
                    int index = TabDataList.FindIndex(t => t.TabPage == value && !t.IsRemoving);
                    if (index > -1) SelectedIndex = index;
                }
            }
        }

        public TabData SelectedTabData => _selectedIndex >= 0 && _selectedIndex < TabDataList?.Count ? TabDataList[_selectedIndex] : null;

        private bool busy = false;

        public bool IsBusy => busy;

        #endregion

        #region Events/Overrides

        public delegate void FormShownDelegate(object sender, TabDataEventArgs e);
        public event FormShownDelegate FormShown;

        public delegate void FormClosedDelegate(object sender, TabDataEventArgs e);
        public event FormClosedDelegate FormClosed;

        public delegate void FormClosingDelegate(object sender, TabDataEventArgs e);

        public delegate void FormTextChangedDelegate(object sender, TabDataEventArgs e);
        public event FormTextChangedDelegate FormTextChanged;

        public virtual void OnFormShown(object sender, TabDataEventArgs e)
        {
            FormShown?.Invoke(sender, e);
        }

        public virtual void OnFormTextChanged(object sender, TabDataEventArgs e)
        {
            FormTextChanged?.Invoke(sender, e);
        }

        public virtual void OnFormClosing(object sender, TabDataEventArgs e)
        {
            if (e.TabData.TabPage == SelectedTab) Program.Animator.HideSync(TabControl);
            FormClosed?.Invoke(sender, e);
        }

        public virtual void OnFormClosed(object sender, TabDataEventArgs e)
        {
            RemoveTab(e.TabData);
            FormClosed?.Invoke(sender, e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            selectedFont?.Dispose();
            selectedFont = new(Font.Name, Font.Size, FontStyle.Bold);
            base.OnFontChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            sf?.Dispose();
            sf_middleCenter?.Dispose();
            selectedFont?.Dispose();
            try { StopProgressTimer(); _tabMarqueeTimer?.Dispose(); _tabProgressTimer?.Dispose(); } catch { }

            // Ensure all TabData instances are disposed to release images, timers and handlers.
            try { DisposeAllTabs(); } catch { }
            base.Dispose(disposing);
        }

        private void _tabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage)
            {
                TabPage page = e.Control as TabPage;

                TabDataList.Add(CreateTabData(page, TabDataList.Count));
                TabDataList[TabDataList.Count - 1].TabTop = Height;
                SelectedIndex = TabDataList.Count - 1;

                // Animate tab showing (TabTop and SelectionAlpha) on UI thread
                if (TabDataList.Count > 0 && TabDataList[TabDataList.Count - 1] != null)
                {
                    skipTabDataRecreation = true;
                    var newTab = TabDataList[TabDataList.Count - 1];
                    _ = ShowTabAsync(newTab);
                }
            }
        }

        private async System.Threading.Tasks.Task ShowTabAsync(TabData tab)
        {
            try
            {
                await tab.Show(() => Invalidate());
            }
            catch
            {
                // Ignore animation exceptions
            }

            if (this.IsHandleCreated)
            {
                try { this.BeginInvoke(() => skipTabDataRecreation = false); }
                catch { skipTabDataRecreation = false; }
            }
            else
            {
                skipTabDataRecreation = false;
            }
        }

        private void _tabControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage)
            {
                TabPage page = e.Control as TabPage;
                TabData found = null;
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var t = TabDataList[i];
                    if (t != null && t.TabPage == page)
                    {
                        found = t;
                        break;
                    }
                }

                if (found != null) RemoveTab(found);
            }
        }

        private async void RemoveTab(TabData tabData, bool animate = true)
        {
            if (tabData == null) return;

            animate &= animate;

            tabData.IsRemoving = true;

            if (CanAnimate_Global && animate)
            {
                FluentTransitions.Transition.With(tabData, nameof(TabData.RemovingAlpha), 0).CriticalDamp(Program.AnimationSpan_Quick);
            }
            else
            {
                tabData.RemovingAlpha = 0;
            }

            int SI = SelectedIndex;
            bool removedWasSelected = (GetIndex(tabData) == SI) || (tabData.TabPage == SelectedTab);

            await tabData.Hide(animate, () => AfterRemovingTab(tabData, animate, SI, removedWasSelected));

            if (FindForm() is not null) FindForm().BackgroundImage = null;
        }

        private void AfterRemovingTab(TabData tabData, bool animate, int SI, bool removedWasSelected)
        {
            // Remove the tab from the list and animate remaining tabs to their new positions.
            int removedIndex = TabDataList.IndexOf(tabData);
            if (removedIndex >= 0)
            {
                TabDataList.RemoveAt(removedIndex);

                // Recalculate scroll offsets so natural positions are accurate.
                CalculateScrollOffset();

                // Animate TabLeft for tabs that are to the right of the removed index.
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var t = TabDataList[i];
                    if (t == null) continue;

                    int naturalLeft = LeftBoundary + i * (tabWidth + _paddingBetweenTabs) - _animatedScrollOffset;

                    // Update TabWidth immediately if changed
                    t.TabWidth = tabWidth;

                    if (t.TabLeft != naturalLeft)
                    {
                        if (CanAnimate_Global)
                        {
                            FluentTransitions.Transition.With(t, nameof(TabData.TabLeft), naturalLeft).CriticalDamp(Program.AnimationSpan_Quick);
                        }
                        else
                        {
                            t.TabLeft = naturalLeft;
                        }
                    }
                }
            }

            forceChangeSelectedIndex = true;
            SelectedIndex = SI;

            _tabControl.TabPages.Remove(tabData.TabPage);

            if (FindForm() is not null) FindForm().Visible = _tabControl.TabPages.Count > 0;

            DisposeTabData(tabData);

            State = MouseState.None;
            _hoveredTabData = null;
            overCloseButton = false;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                var t = TabDataList[i];
                if (t == null) continue;
                t.Hovered = false;
                t.CloseButtonAlpha = 0;
            }

            if (CanAnimate_Global)
            {
                FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(Program.AnimationSpan_Quick);
            }
            else
            {
                HoverSize = 0;
            }

            Point mousePos = PointToClient(MousePosition);
            TabData newHoveredTab = null;
            foreach (TabData tabDataX in TabDataList)
            {
                if (tabDataX != null && IsPointInVisibleRegion(mousePos) && tabDataX.Rectangle.Contains(mousePos))
                {
                    newHoveredTab = tabDataX;
                    break;
                }
            }

            if (newHoveredTab != null)
            {
                _hoveredTabData = newHoveredTab;
                hoverPosition = mousePos;
                int defaultHoverSize = Math.Max(newHoveredTab.Rectangle.Width, newHoveredTab.Rectangle.Height);
                if (CanAnimate_Global)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), defaultHoverSize).CriticalDamp(Program.AnimationSpan_Quick);
                }
                else
                {
                    HoverSize = defaultHoverSize;
                }
                newHoveredTab.Hovered = true;
            }

            // Animate SelectionAlpha (always allowed) and TabTop only if the removed tab was selected
            if (SI >= 0 && SI < TabDataList.Count && TabDataList[SI] != null)
            {
                TabData newlySelectedTab = TabDataList[SI];
                if (animate && CanAnimate_Global)
                {
                    skipTabDataRecreation = true;


                    // Always animate SelectionAlpha. If the removed tab was selected, start from 0.
                    var tcsSelection = new TaskCompletionSource<bool>();
                    if (removedWasSelected)
                    {
                        newlySelectedTab.SelectionAlpha = 0;
                    }

                    FluentTransitions.Transition.With(newlySelectedTab, nameof(TabData.SelectionAlpha), 255).HookOnCompletion(() => tcsSelection.TrySetResult(true))
                        .CriticalDamp(Program.AnimationSpan);

                    if (removedWasSelected)
                    {
                        // Only animate TabTop when the removed tab was the selected one
                        var tcsTop = new TaskCompletionSource<bool>();
                        newlySelectedTab.TabTop = Height;
                        FluentTransitions.Transition.With(newlySelectedTab, nameof(TabData.TabTop), _upperTabPadding).HookOnCompletion(() => tcsTop.TrySetResult(true))
                            .CriticalDamp(Program.AnimationSpan);

                        _ = Task.Run(async () =>
                        {
                            await Task.WhenAll(tcsSelection.Task, tcsTop.Task);
                            this.BeginInvoke(() => skipTabDataRecreation = false);
                            this.BeginInvoke(() => Invalidate());
                        });
                    }
                    else
                    {
                        _ = Task.Run(async () =>
                        {
                            await tcsSelection.Task;
                            this.BeginInvoke(() => skipTabDataRecreation = false);
                            this.BeginInvoke(() => Invalidate());
                        });
                    }
                }
                else
                {
                    // If not animating, ensure SelectionAlpha is set immediately
                    newlySelectedTab.SelectionAlpha = 255;
                }
            }

            Refresh();

            try
            {
                if (!DesignMode && animate) Program.Animator.ShowSync(TabControl);
            }
            catch { }
        }

        private void _tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTab = _tabControl.SelectedTab;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (TabDataList == null || TabDataList.Count == 0) return;

            _isResizing = true;

            CalculateScrollOffset();

            if (_selectedIndex >= 0 && _selectedIndex < TabDataList.Count)
            {
                int selectedTabLeft = LeftBoundary + _selectedIndex * (tabWidth + _paddingBetweenTabs) - _animatedScrollOffset;
                int selectedTabRight = selectedTabLeft + tabWidth;

                if (selectedTabLeft < LeftBoundary)
                {
                    int targetOffset = _animatedScrollOffset + (LeftBoundary - selectedTabLeft);
                    AnimateScrollOffset(targetOffset);
                }
                else if (selectedTabRight > RightBoundary)
                {
                    int targetOffset = _animatedScrollOffset + (selectedTabRight - RightBoundary);
                    AnimateScrollOffset(targetOffset);
                }
            }

            // Preserve selection alpha during resize to prevent flicker
            UpdateTabPositions(TabDataList, preserveSelectionAlpha: true, animateWidth: false, animateSelection: false);

            NormalizeSelectionAlpha();

            _isResizing = false;

            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_tabControl != null)
            {
                if (!IsBusy) HandleTabControlMouseMove(e);
            }
            else if (FindForm() != null && e.Button == MouseButtons.Left)
            {
                HandleFormMove();
            }

            if (!IsBusy)
            {
                HandleHoverForTab(e);
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!IsBusy)
            {
                bool clickedOnTab = false;
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var tabData = TabDataList[i];
                    if (tabData == null) continue;
                    if (IsMouseOverTab(tabData))
                    {
                        ProcessTabMouseActions(tabData, e);
                        clickedOnTab = true;
                        break;
                    }
                }

                // Trigger system menu on right-click of empty tab area (only if not clicking on a tab)
                if (!clickedOnTab && e.Button == MouseButtons.Right)
                {
                    TriggerSystemContextMenu();
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_tabControl != null && !IsBusy)
            {
                HandleTabControlMouseButtonClick(e);
            }
            else
            {
                locationOldPoint = MousePosition - (Size)FindForm()?.Location;
            }

            TabData clickedTab = GetTabAtMousePosition(e);
            if (clickedTab != null)
            {
                State = MouseState.Down;
                _hoveredTabData = clickedTab;
                hoverPosition = e.Location;

                int maxHoverSize = Math.Max(clickedTab.Rectangle.Width, clickedTab.Rectangle.Height) * 2;
                if (CanAnimate_Global)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), maxHoverSize).CriticalDamp(Program.AnimationSpan);
                }
                else
                {
                    HoverSize = maxHoverSize;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (_tabControl != null && !IsBusy)
            {
                HandleTabControlMouseDoubleClick(e);
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_maxScrollOffset > 0)
            {
                int scrollDelta = e.Delta;
                int scrollAmount = _scrollAmount;

                if (scrollDelta > 0)
                {
                    AnimateScrollOffset(_scrollOffset - scrollAmount);
                }
                else
                {
                    AnimateScrollOffset(_scrollOffset + scrollAmount);
                }

                return;
            }

            base.OnMouseWheel(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (IsDisposed) return;

            if (!IsBusy)
            {
                bool wasDragging = isMovingTab && moveFrom > -1;

                if (wasDragging)
                {
                    int capturedFrom = moveFrom;
                    int capturedTo = moveTo > -1 ? moveTo : moveFrom;
                    bool capturedToLast = isMovingToLast;
                    bool capturedToFirst = isMovingToFirst;

                    if (capturedFrom < TabDataList.Count && TabDataList[capturedFrom] != null)
                    {
                        TabData dragged = TabDataList[capturedFrom];

                        int finalSlot = capturedToFirst ? 0
                            : capturedToLast ? TabDataList.Count - 1
                            : capturedTo;

                        int targetLeft = LeftBoundary + finalSlot * (tabWidth + _paddingBetweenTabs);

                        dragged.TabLeft = _dragX;

                        void CommitAfterAnimation()
                        {
                            if (capturedToLast)
                            {
                                SwapTabs(capturedFrom, TabDataList.Count - 1);
                            }
                            else if (capturedToFirst)
                            {
                                SwapTabs(capturedFrom, 0);
                            }
                            else if (capturedFrom != capturedTo)
                            {
                                SwapTabs(capturedFrom, capturedTo);
                            }
                            else
                            {
                                ResetAllTabLeftPositions();
                            }
                        }

                        if (CanAnimate_Global)
                        {
                            FluentTransitions.Transition.With(dragged, nameof(TabData.TabLeft), targetLeft).HookOnCompletion(CommitAfterAnimation)
                                .CriticalDamp(Program.AnimationSpan_Quick);
                        }
                        else
                        {
                            dragged.TabLeft = targetLeft;
                            CommitAfterAnimation();
                        }
                    }

                    ResetModifiersToNull();
                }
                else
                {
                    ResetModifiersToNull();
                }

                State = MouseState.None;

                TabData hoveredTab = GetTabAtMousePosition(new MouseEventArgs(MouseButtons.None, 0, PointToClient(MousePosition).X, PointToClient(MousePosition).Y, 0));
                if (hoveredTab != null)
                {
                    int defaultHoverSize = Math.Max(hoveredTab.Rectangle.Width, hoveredTab.Rectangle.Height);
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(this, nameof(HoverSize), defaultHoverSize).CriticalDamp(Program.AnimationSpan_Quick);
                    }
                    else
                    {
                        HoverSize = defaultHoverSize;
                    }
                }

                Point mousePos = PointToClient(MousePosition);
                HandleHoverForTab(new MouseEventArgs(MouseButtons.None, 0, mousePos.X, mousePos.Y, 0));
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            Point mousePos = PointToClient(MousePosition);
            HandleHoverForTab(new MouseEventArgs(MouseButtons.None, 0, mousePos.X, mousePos.Y, 0));

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            for (int i = 0; i < TabDataList.Count; i++)
            {
                var tabData = TabDataList[i];
                if (tabData == null) continue;
                tabData.CancelTransition(nameof(TabData.HoverAlpha));
                tabData.CancelTransition(nameof(TabData.CloseButtonAlpha));
                tabData.Hovered = false;
                tabData.CloseButtonAlpha = 0;
            }

            overCloseButton = false;
            State = MouseState.None;
            _hoveredTabData = null;

            if (CanAnimate_Global)
            {
                FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(Program.AnimationSpan_Quick);
            }
            else
            {
                HoverSize = 0;
            }

            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (!IsBusy)
            {
                if (e.Data.GetDataPresent(typeof(System.Windows.Forms.Form)))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else if (e.Data.GetDataPresent(typeof(ColorItem)))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }

            base.OnDragEnter(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!IsBusy)
            {
                if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
                {
                    Point MousePosition = new(e.X, e.Y);

                    for (int i = 0; i < TabDataList.Count; i++)
                    {
                        var tabData = TabDataList[i];
                        if (tabData == null) continue;
                        Point clientPos = PointToClient(MousePosition);
                        if (IsPointInVisibleRegion(clientPos) && tabData.Rectangle.Contains(clientPos))
                        {
                            SelectedIndex = GetIndex(tabData);
                            break;
                        }
                    }
                }
            }

            base.OnDragOver(e);
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        #endregion

        #region Hover

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden), System.ComponentModel.Browsable(false)]
        public int HoverSize
        {
            get => _hoverSize;
            set
            {
                _hoverSize = value;
                Invalidate();
            }
        }

        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        /// <summary>
        /// Core paint routine. Uses two-pass rendering to draw non-selected tabs
        /// first and selected tabs on top. When dragging a tab, the dragged tab
        /// is drawn last with moving layout. This method minimizes allocations
        /// by iterating TabDataList directly.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            InvokePaintBackground(this, e);

            using (Pen P = new(!OS.W7 ? scheme.Colors.Line_Hover(0) : Color.Black)) { G.DrawLine(P, new Point(0, Height - 1), new Point(Width - 1, Height - 1)); }

            Rectangle clipRect = new(LeftBoundary, 0, RightBoundary - LeftBoundary, Height);

            if (Program.IsBeta)
            {
                Rectangle rect = new(0, 0, Width - 1, Height - 1);
                SizeF betaSize = Program.Localization.Strings.General.Beta.ToUpper().Measure(Fonts.ConsoleMedium) + new SizeF(2, 2);
                Rectangle betaRect = new(Width - (int)betaSize.Width - 5, rect.Y + (int)((rect.Height - betaSize.Height) / 2), (int)betaSize.Width, (int)betaSize.Height);
                G.FillRoundedRect(scheme_secondary.Brushes.Back_Checked, betaRect);
                G.DrawRoundedRectBeveled(scheme_secondary.Pens.Line_Checked, betaRect);
                betaRect.Y++;
                G.DrawString(Program.Localization.Strings.General.Beta.ToUpper(), Fonts.ConsoleMedium, scheme_secondary.Brushes.ForeColor_Accent, betaRect, sf_middleCenter);
            }

            G.SetClip(clipRect);

            if (_tabControl != null)
            {
                // Draw tabs in two passes: non-selected first, selected last.
                NormalizeSelectionAlpha();

                TabData movingTab = null;
                if (isMovingTab && moveFrom != -1 && moveFrom < TabDataList.Count)
                {
                    movingTab = TabDataList[moveFrom];
                }

                // First pass: draw non-selected tabs (and selected=false)
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var tabData = TabDataList[i];
                    if (tabData == null) continue;
                    if (tabData == movingTab) continue;
                    if (!tabData.Selected && IsTabVisible(tabData.Rectangle))
                    {
                        DrawTab(G, tabData);
                    }
                }

                // Second pass: draw selected tabs
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    var tabData = TabDataList[i];
                    if (tabData == null) continue;
                    if (tabData == movingTab) continue;
                    if (tabData.Selected && IsTabVisible(tabData.Rectangle))
                    {
                        DrawTab(G, tabData);
                    }
                }

                // If dragging a tab, draw it on top and with moving layout
                if (movingTab != null)
                {
                    DrawTab(G, movingTab, true);
                }
            }

            G.ResetClip();

            if (_maxScrollOffset > 0)
            {
                int availableWidth = RightBoundary - LeftBoundary;
                int tabDataListCount = TabDataList.Count;
                int totalTabWidth = tabDataListCount * tabWidth + _paddingBetweenTabs * (tabDataListCount - 1);
                int indicatorWidth = Math.Max(_minimumIndicatorWidth, (int)((double)availableWidth / totalTabWidth * availableWidth));
                int indicatorX = LeftBoundary + (int)((double)_animatedScrollOffset / _maxScrollOffset * (availableWidth - indicatorWidth));
                Rectangle indicatorRect = new(indicatorX, Height - _indicatorBottomOffset, indicatorWidth, _indicatorHeight);

                using (SolidBrush indicatorBrush = new(scheme.Colors.ForeColor_Accent))
                {
                    G.FillRectangle(indicatorBrush, indicatorRect);
                }
            }
        }

        private void DrawTabPath(Graphics G, GraphicsPath path, Pen pen, Rectangle r, int radius, bool rounded)
        {
            Region oldClip = G.Clip;

            Rectangle clip = new(r.X, r.Y, r.Width + 1, r.Height - 1);
            G.SetClip(clip);
            G.DrawPath(pen, path);

            G.Clip = oldClip;
        }

        /// <summary>
        /// Draw a single tab. Respects the TabData visual flags:
        /// - SelectionAlpha, HoverAlpha, CloseButtonAlpha, Progress* fields.
        /// The drawing uses rounded clipping paths and draws hover/selection
        /// overlays, progress fills/marquee, icon, text and the close button.
        /// </summary>
        private void DrawTab(Graphics G, TabData tabData, bool isMoving = false)
        {
            Region oldClip = G.Clip;

            Rectangle rect = !isMoving ? tabData.Rectangle : new Rectangle(_dragX, tabData.Rectangle.Y, tabData.Rectangle.Width, tabData.Rectangle.Height);
            Rectangle textRect = titleRectangle(rect);
            Rectangle closeRect = closeRectangle(rect);
            Rectangle iconRect = iconRectangle(rect);
            Bitmap icon = tabData.Image;
            bool shouldDrawProgress = tabData.ProgressMarquee || (tabData.ProgressEnabled && tabData.ProgressValue > 0);

            using (GraphicsPath path = rect.ToTabPath(_radius, Program.Style.RoundedCorners ? GraphicsExtensions.TabStyle.Rounded : GraphicsExtensions.TabStyle.Sharp))
            {
                if (isMoving)
                {
                    using (SolidBrush br = new(Color.FromArgb(150, scheme_secondary.Colors.Back_Hover(parentLevel))))
                    {
                        G.FillPath(br, path);
                    }

                    if (tabData == _hoveredTabData && _hoverSize > 0)
                    {
                        Color hoverColor = Color.FromArgb(150, scheme_secondary.Colors.Back_Checked_Hover);
                        G.DrawHover(this, path, hoverColor, HoverSize, _clipHeightOffset);
                    }

                    using (Pen P = new(Color.FromArgb(150, scheme_secondary.Colors.Line_Hover(parentLevel))))
                    using (Pen P_Hover = new(Color.FromArgb(Math.Min(100, tabData.HoverAlpha), scheme_secondary.Colors.ForeColor_Accent)))
                    {
                        DrawTabPath(G, path, P, rect, _radius, Program.Style.RoundedCorners);
                        DrawTabPath(G, path, P_Hover, rect, _radius, Program.Style.RoundedCorners);
                    }
                }
                else
                {
                    if (tabData.SelectionAlpha > 0)
                    {
                        using (SolidBrush br = new(Color.FromArgb(tabData.SelectionAlpha, scheme.Colors.Back_Hover(parentLevel))))
                        {
                            G.FillPath(br, path);
                        }
                    }

                    if (tabData.HoverAlpha > 0)
                    {
                        if (tabData == _hoveredTabData && _hoverSize > 0)
                        {
                            Color hoverColor = tabData.Selected ? scheme.Colors.Back_Checked_Hover : Color.FromArgb(170, scheme.Colors.Line_Hover(parentLevel));
                            G.DrawHover(this, path, hoverColor, HoverSize, _clipHeightOffset);
                        }
                    }

                    // Draw progress fill or marquee clipped to tab rounded path
                    if (shouldDrawProgress)
                    {
                        oldClip = G.Clip;
                        G.SetClip(path);

                        Color accent = scheme.Colors.ForeColor_Accent.Blend(scheme.Colors.Line_Hover(parentLevel), tabData.SelectionAlpha / 255f);

                        if (tabData.ProgressMarquee)
                        {
                            int marqueeWidth = Math.Max(16, (int)(rect.Width * 0.25f));
                            float marqueeLeft = -marqueeWidth + (rect.Width + marqueeWidth) * _marqueeOffset;
                            RectangleF marqueeRect = new(marqueeLeft + rect.X, rect.Y, marqueeWidth, rect.Height);

                            using (LinearGradientBrush brush = new(marqueeRect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
                            {
                                ColorBlend cb = new()
                                {
                                    Colors = [Color.Transparent, Color.FromArgb(120, accent), Color.Transparent],
                                    Positions = [0f, 0.5f, 1f]
                                };
                                brush.InterpolationColors = cb;
                                G.FillRoundedRect(brush, marqueeRect);
                            }
                        }
                        else
                        {
                            int w = (int)(rect.Width * tabData.ProgressValue / 100.0);
                            if (w > 0)
                            {
                                Rectangle fillRect = new(rect.X, rect.Y, w, rect.Height);

                                // Use gradient similar to ProgressBar normal fill
                                Color stateColor = scheme.Colors.ForeColor_Accent.Blend(Color.FromArgb(200, scheme.Colors.Line_Hover(parentLevel)), tabData.SelectionAlpha / 255f);
                                using (LinearGradientBrush br = new(fillRect, Program.Style.DarkMode ? stateColor.Dark(0.05f) : stateColor.Light(), stateColor, (float)(tabData.ProgressValue / 100.0) * 360f, true))
                                {
                                    G.FillRoundedRect(br, fillRect);
                                }

                                // Noise overlay
                                G.FillRoundedRect(_noise, fillRect);

                                // Moving highlight effect (only when progress in range)
                                if (tabData.ProgressEnabled && Program.Style.Animations)
                                {
                                    float highlightWidth = Math.Max(1, fillRect.Width * 0.3f);
                                    float offset = (_hoverOffset * (fillRect.Width + highlightWidth));
                                    float highlightLeft = rect.X + (offset - highlightWidth);

                                    RectangleF highlightRect = new(highlightLeft, fillRect.Top, highlightWidth, fillRect.Height);

                                    // Clip to fillRect region while drawing highlight
                                    Region oldClip3 = G.Clip;
                                    G.SetClip(fillRect);

                                    Color hilightColor = Program.Style.DarkMode ? stateColor.Light() : stateColor.Dark();
                                    using (LinearGradientBrush hbrush = new(highlightRect, Color.Transparent, hilightColor, LinearGradientMode.Horizontal))
                                    {
                                        ColorBlend cb2 = new()
                                        {
                                            Colors = [Color.Transparent, Color.FromArgb(160, hilightColor), Color.Transparent],
                                            Positions = [0f, 0.5f, 1f]
                                        };
                                        hbrush.InterpolationColors = cb2;
                                        G.FillRoundedRect(hbrush, highlightRect);
                                    }

                                    G.Clip = oldClip3;
                                }
                            }
                        }

                        G.Clip = oldClip;
                    }

                    if (OS.WVista || OS.W7)
                    {
                        using (Pen Px = new(win7BorderColor))
                        {
                            DrawTabPath(G, path, Px, rect, _radius, Program.Style.RoundedCorners);

                            if (tabData.HoverAlpha == 0)
                            {
                                Rectangle rect_smaller = new(rect.X + _rectSmallerOffset, rect.Y + _rectSmallerOffset, rect.Width - _rectSmallerWidthReduction, rect.Height);
                                using (GraphicsPath path_smaller = rect_smaller.ToTabPath(_radius - _smallerRadiusOffset, Program.Style.RoundedCorners ? GraphicsExtensions.TabStyle.Rounded : GraphicsExtensions.TabStyle.Sharp))
                                {
                                    DrawTabPath(G, path_smaller, Pens.Black, rect_smaller, _radius - _smallerRadiusOffset, Program.Style.RoundedCorners);
                                }
                            }
                        }
                    }
                    else if (OS.W8x)
                    {
                        Color lineColor = Color.White;

                        using (Pen Px = new(Color.FromArgb(50, lineColor)))
                        {
                            DrawTabPath(G, path, Px, rect, _radius, Program.Style.RoundedCorners);
                        }
                    }
                    else
                    {
                        Color normalLineColor = Color.FromArgb(shouldDrawProgress ? 255 : tabData.SelectionAlpha, scheme.Colors.Line_Hover(parentLevel));

                        using (Pen P_normal = new(normalLineColor))
                        {
                            DrawTabPath(G, path, P_normal, rect, _radius, Program.Style.RoundedCorners);
                        }
                    }

                    Color hoverLineColor = tabData.Selected ? scheme.Colors.Line_Checked_Hover : scheme.Colors.Line_Hover(parentLevel);

                    using (Pen P_hover = new(Color.FromArgb(tabData.HoverAlpha, hoverLineColor)))
                    {
                        DrawTabPath(G, path, P_hover, rect, _radius, Program.Style.RoundedCorners);
                    }

                    if (tabData.RemovingAlpha < 255)
                    {
                        using (LinearGradientBrush lgb_removing = new(rect, Color.FromArgb(255 - tabData.RemovingAlpha, scheme.Colors.Back(parentLevel)), Color.Transparent, LinearGradientMode.Vertical))
                        using (LinearGradientBrush lgb_removing_border = new(rect, Color.FromArgb(255 - tabData.RemovingAlpha, scheme.Colors.Line(parentLevel)), Color.Transparent, LinearGradientMode.Vertical))
                        using (Pen P_removing = new(lgb_removing_border))
                        {
                            G.FillPath(lgb_removing, path);
                            G.DrawPath(P_removing, path);
                        }
                    }

                    if (tabData.CloseButtonAlpha > 0)
                    {
                        Color back = tabData.Selected ? scheme_secondary.Colors.Back_Checked : scheme.Colors.Back(parentLevel);
                        Color line = tabData.Selected ? scheme_secondary.Colors.Line_Checked_Hover : scheme.Colors.Line_Hover(parentLevel);

                        using (SolidBrush br = new(Color.FromArgb(tabData.CloseButtonAlpha, back)))
                        using (Pen P = new(Color.FromArgb(tabData.CloseButtonAlpha, line)))
                        {
                            G.FillRoundedRect(br, closeRect);
                            G.DrawRoundedRectBeveled(P, closeRect);
                        }
                    }
                }
            }

            DrawTextOnGlass(G, closeStr, Fonts.ConsoleMedium, Color.FromArgb(255 - tabData.CloseButtonAlpha, ForeColor), closeRect, sf_middleCenter);

            if (tabData.CloseButtonAlpha > 0)
            {
                DrawTextOnGlass(G, closeStr, Fonts.ConsoleMedium, Color.FromArgb(tabData.CloseButtonAlpha, tabData.Selected ? scheme_secondary.Colors.ForeColor_Accent : ForeColor), closeRect, sf_middleCenter);
            }

            if (icon != null) G.DrawImage(icon, iconRect);

            if (rect.Width > minTabWidth)
            {
                DrawTextOnGlass(G, tabData.Text, tabData.Selected ? selectedFont : Font, ForeColor, textRect, sf);
            }

            G.Clip = oldClip;
        }

        void DrawTextOnGlass(Graphics g, string text, Font font, Color foreColor, Rectangle rect, StringFormat sf)
        {
            using (GraphicsPath path = new())
            {
                path.AddString(text, font.FontFamily, (int)font.Style, g.DpiY * font.Size / 72 * 0.97f, rect, sf);

                using (Brush fill = new SolidBrush(foreColor)) g.FillPath(fill, path);
            }
        }
    }
}