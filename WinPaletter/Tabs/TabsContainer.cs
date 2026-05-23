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
using static WinPaletter.UI.Style.Config;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// Tabs container control that can handle multiple TabPages with forms inside them. For better appearance, use it with TabControl of <see cref="UI.WP.TablessControl"/> and forms having <see cref="TitlebarExtender"/>.
    /// </summary>
    public class TabsContainer : TitlebarExtender
    {
        /// <summary>
        /// Initialize TabsContainer
        /// </summary>
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

        /// <summary>
        /// Gets if the tabs container can be animated
        /// </summary>
        public bool CanAnimate_Global => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        /// <summary>
        /// List of tab data included in current control
        /// </summary>
        public List<TabData> TabDataList = [];

        // Static constants for UI element sizes and offsets
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

        // Overflow scrolling variables
        private int _scrollOffset = 0;
        private int _maxScrollOffset = 0;
        private int _animatedScrollOffset = 0;
        private static readonly int _scrollAmount = 100;
        private static readonly int _minimumIndicatorWidth = 20;
        private static readonly int _indicatorHeight = 2;
        private static readonly int _indicatorBottomOffset = 3;

        /// <summary>
        /// Animated scroll offset for smooth scrolling
        /// </summary>
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

        // Minimum tab width will be calculated dynamically based on tab height
        // Formula: left padding + icon (_iconSize) + gap (_paddingPostIcon) + close button (_closeButtonSize) + right padding
        private int minTabWidth => (tabHeight - _iconSize) / 2 + _iconSize + _paddingPostIcon + _closeButtonSize + (tabHeight - _closeButtonSize) / 2;

        private bool forceChangeSelectedIndex = true;
        private bool overCloseButton = false;

        private int moveFrom = -1;
        private int moveTo = -1;
        private bool isMovingTab = false;
        private bool isMovingToLast = false;
        private bool isMovingToFirst = false;

        private Point tabOldPoint = new();
        private Point locationOldPoint = new();

        // Client X position of the dragged tab's left edge, updated during MouseMove
        private int _dragX = 0;

        private readonly UI.WP.ContextMenuStrip contextMenu = new() { ShowImageMargin = true, AllowTransparency = true };
        private TabData contextItemDropped;

        private Point hoverPosition;
        private TabData _hoveredTabData;

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

            contextMenu.Items.AddRange([closeButton, closeAllToTheRight, closeAllToTheLeft, closeAll, closeAllButThis,
                toolStripSeparator0,
                detach, detachAll, detachAllButThis,
                toolStripSeparator1,
                helpButton
                ]);
        }

        /// <summary>
        /// Handle hover logic for a specific tab based on mouse position
        /// </summary>
        /// <param name="e">Mouse event args</param>
        private void HandleHoverForTab(MouseEventArgs e)
        {
            // Skip hover logic during tab dragging
            if (isMovingTab)
            {
                return;
            }

            Point mousePos = e.Location;
            TabData hoveredTab = null;
            Rectangle hoveredRect = Rectangle.Empty;

            // Find which tab is being hovered
            foreach (TabData tabData in TabDataList.ToList())
            {
                if (tabData != null && tabData.Rectangle.Contains(mousePos))
                {
                    hoveredTab = tabData;
                    hoveredRect = tabData.Rectangle;
                    break;
                }
            }

            // Handle tab hover state changes
            if (hoveredTab != null)
            {
                // Check if mouse is over close button
                bool overCloseBtn = closeRectangle(hoveredTab.Rectangle).Contains(mousePos);

                // Update close button alpha
                if (hoveredTab.CloseButtonAlpha != (overCloseBtn ? 255 : 0))
                {
                    if (Program.Style.Animations)
                    {
                        FluentTransitions.Transition.With(hoveredTab, nameof(TabData.CloseButtonAlpha), overCloseBtn ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                    }
                    else
                    {
                        hoveredTab.CloseButtonAlpha = overCloseBtn ? 255 : 0;
                    }
                }

                overCloseButton = overCloseBtn;

                // Update hover circle animation when entering a new tab
                if (_hoveredTabData != hoveredTab)
                {
                    _hoveredTabData = hoveredTab;
                    hoverPosition = mousePos;

                    int defaultHoverSize = Math.Max(hoveredTab.Rectangle.Width, hoveredTab.Rectangle.Height);
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(this, nameof(HoverSize), defaultHoverSize).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                    }
                    else
                    {
                        HoverSize = defaultHoverSize;
                    }
                }
                else if (_hoveredTabData == hoveredTab)
                {
                    // Update hover position while staying on the same tab
                    hoverPosition = mousePos;
                }

                // Set hovered state for the tab
                foreach (TabData tabData in TabDataList.ToList())
                {
                    if (tabData != null)
                    {
                        tabData.Hovered = (tabData == hoveredTab);
                    }
                }

                Invalidate(hoveredRect);
            }
            else
            {
                // Reset hover when not over any tab
                if (_hoveredTabData != null)
                {
                    _hoveredTabData = null;

                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                    }
                    else
                    {
                        HoverSize = 0;
                    }
                }

                // Reset all hover states
                foreach (TabData tabData in TabDataList.ToList())
                {
                    if (tabData != null)
                    {
                        tabData.Hovered = false;
                        tabData.CloseButtonAlpha = 0;
                    }
                }

                overCloseButton = false;
            }
        }


        /// <summary>
        /// Generate a tab page that have form inside, and add it into the tab control
        /// </summary>
        /// <param name="form"></param>
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

            // Show tab animation on background thread to prevent blocking form addition
            if (TabDataList.Count > 0 && TabDataList[TabDataList.Count - 1] != null)
            {
                _ = Task.Run(async () => await TabDataList[TabDataList.Count - 1].Show(() => Invalidate()));
            }

            // Then continue with form addition
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

                // Ensure availableForTabs is at least enough for minimum tab widths
                if (availableForTabs < minTabWidth * _tabControl.TabPages.Count)
                    availableForTabs = minTabWidth * _tabControl.TabPages.Count;

                // Calculate width that fits all tabs exactly using floating-point for precision
                double calculatedWidth = (double)availableForTabs / _tabControl.TabPages.Count;

                // Round down to ensure total width never exceeds available space
                int finalWidth = (int)Math.Floor(calculatedWidth);

                // Ensure the calculated width doesn't exceed _maxTabWidth
                // But also ensure total width doesn't exceed available space
                // Also ensure minimum width respects padding between tabs
                return Math.Max(minTabWidth, Math.Min(_maxTabWidth, finalWidth));
            }
        }
        private int tabHeight => Height - _upperTabPadding;

        /// <summary>
        /// Gets the effective left boundary for tabs (left padding)
        /// </summary>
        private int LeftBoundary => _paddingBetweenTabs;

        /// <summary>
        /// Gets the effective right boundary for tabs (right padding, or BETA badge left padding if present)
        /// </summary>
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

        /// <summary>
        /// Count of tabs in current TabContainer
        /// </summary>
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
            TabDataList.Clear();

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

        private void SwapTabs(int from, int to)
        {
            if (TabDataList != null)
            {
                TabData itemFrom = TabDataList[from];
                TabData itemTo = TabDataList[to];

                if (itemFrom == null || itemFrom.IsRemoving) return;
                if (itemTo == null || itemTo.IsRemoving) return;

                TabDataList[from] = itemTo;
                TabDataList[to] = itemFrom;

                // to avoid bug of non-selection
                forceChangeSelectedIndex = true;
                SelectedIndex = to;

                UpdateTabPositions(TabDataList, preserveSelectionAlpha: false, animateWidth: false);

                isMovingTab = false;

                ResetModifiersToNull();

                Refresh();
            }
        }

        private void MoveToLast(int from)
        {
            if (TabDataList != null)
            {
                TabData itemFrom = TabDataList[from];

                if (itemFrom == null || itemFrom.IsRemoving) return;

                TabDataList.RemoveAt(from);
                TabDataList.Add(itemFrom);

                // to avoid bug of non-selection
                forceChangeSelectedIndex = true;
                SelectedIndex = TabDataList.Count - 1;

                UpdateTabPositions(TabDataList, preserveSelectionAlpha: false, animateWidth: false);

                isMovingTab = false;

                ResetModifiersToNull();

                Refresh();
            }
        }

        private void MoveToFirst(int from)
        {
            if (TabDataList != null)
            {
                TabData itemFrom = TabDataList[from];

                if (itemFrom == null || itemFrom.IsRemoving) return;

                TabDataList.RemoveAt(from);
                TabDataList.Insert(0, itemFrom);

                // to avoid bug of non-selection
                forceChangeSelectedIndex = true;
                SelectedIndex = 0;

                UpdateTabPositions(TabDataList, preserveSelectionAlpha: false, animateWidth: false);

                ResetModifiersToNull();

                Refresh();
            }
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
            foreach (TabData tabData in TabDataList.ToList())
            {
                if (tabData.Rectangle.Contains(e.Location))
                {
                    return tabData;
                }
            }

            // Return an empty rectangle if no tabData is found at the current mouse position
            return null;
        }

        private TabData CreateTabData(TabPage page, int index)
        {
            int tabX = LeftBoundary + index * (tabWidth + _paddingBetweenTabs) - _animatedScrollOffset;
            int tabW = tabWidth;

            Rectangle tabRectangle = new(tabX, _upperTabPadding, tabW, tabHeight);
            return new TabData(this, page, tabRectangle);
        }

        /// <summary>
        /// Calculate the maximum scroll offset based on total tab width and available space
        /// </summary>
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
                // All tabs fit, no scrolling needed
                _maxScrollOffset = 0;
                _scrollOffset = 0;
            }
            else
            {
                // Calculate how much we need to scroll
                _maxScrollOffset = totalTabWidth - availableWidth;
                // Clamp scroll offset to valid range
                _scrollOffset = Math.Min(_scrollOffset, _maxScrollOffset);
            }

            // Ensure scroll offset doesn't cause tabs to extend beyond right boundary
            int lastTabRight = LeftBoundary + (TabDataList.Count - 1) * (tabWidth + _paddingBetweenTabs) + tabWidth - _scrollOffset;
            if (lastTabRight > RightBoundary)
            {
                _scrollOffset = lastTabRight - RightBoundary;
                _scrollOffset = Math.Min(_scrollOffset, _maxScrollOffset);
            }
        }

        /// <summary>
        /// Check if a tab is within or partially within the visible region
        /// </summary>
        private bool IsTabVisible(Rectangle tabRect)
        {
            // Tab is visible if it intersects with the visible region
            Rectangle visibleRegion = new(LeftBoundary, 0, RightBoundary - LeftBoundary, Height);
            return tabRect.IntersectsWith(visibleRegion);
        }

        /// <summary>
        /// Animate scroll offset to target value
        /// </summary>
        private void AnimateScrollOffset(int targetOffset)
        {
            _scrollOffset = Math.Max(0, Math.Min(targetOffset, _maxScrollOffset));

            if (CanAnimate_Global)
            {
                FluentTransitions.Transition.With(this, nameof(AnimatedScrollOffset), _scrollOffset)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                AnimatedScrollOffset = _scrollOffset;
            }
        }

        private void UpdateTabPositions(List<TabData> collection, bool preserveSelectionAlpha = true, bool animateWidth = false)
        {
            int collectionCount = collection.Count;

            if (collectionCount == 0)
            {
                // Handle the case where the TabDataList is empty
                return;
            }

            // Calculate scroll offset before updating positions
            CalculateScrollOffset();

            if (_selectedIndex >= 0 && _selectedIndex < collectionCount)
            {
                List<TabData> tabDatas = [with(collection)];
                collection.Clear();

                Point mousePos = PointToClient(MousePosition);

                int i = 0;
                foreach (TabData tabData in tabDatas)
                {
                    if (tabData != null && tabData.TabPage != null && !tabData.IsRemoving)
                    {
                        TabData tabDataX = CreateTabData(tabData.TabPage, i);
                        tabDataX.Selected = i == _selectedIndex;

                        // Preserve alpha values from old tab data
                        if (preserveSelectionAlpha)
                        {
                            tabDataX.SelectionAlpha = tabData.SelectionAlpha;
                        }
                        else
                        {
                            // Set correct initial selection alpha based on selection state
                            tabDataX.SelectionAlpha = tabDataX.Selected ? 255 : 0;
                        }

                        // Set selectionAlpha to 0 during width animation
                        if (animateWidth)
                        {
                            tabDataX.SelectionAlpha = 0;
                        }

                        tabDataX.RemovingAlpha = tabData.RemovingAlpha;
                        tabDataX.CloseButtonAlpha = tabData.CloseButtonAlpha;

                        // Initialize hover state based on current mouse position to prevent first-hover flicker
                        tabDataX.Hovered = tabDataX.Rectangle.Contains(mousePos);

                        // Animate tab width if requested
                        if (animateWidth && tabData.TabWidth != tabWidth)
                        {
                            if (CanAnimate_Global)
                            {
                                FluentTransitions.Transition.With(tabDataX, nameof(TabData.TabWidth), tabWidth)
                                    .HookOnCompletion(() =>
                                    {
                                        // Restore selection alpha after width animation
                                        if (tabDataX.Selected)
                                        {
                                            tabDataX.SelectionAlpha = 255;
                                        }
                                    })
                                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            }
                            else
                            {
                                tabDataX.TabWidth = tabWidth;
                                // Restore selection alpha immediately if not animating
                                if (tabDataX.Selected)
                                {
                                    tabDataX.SelectionAlpha = 255;
                                }
                            }
                        }

                        collection.Add(tabDataX);
                        i++;
                    }
                }
            }
        }

        private int AdjustSelectedIndex(int value)
        {
            if (TabDataList != null && TabDataList.Count > 0)
            {
                if (value > TabDataList.Count - 1)
                    return TabDataList.Count - 1;
            }

            return value;
        }

        private void UpdateSelectedTab()
        {
            if (TabDataList != null && TabDataList.Count > 0)
            {
                if (_selectedIndex > -1)
                {
                    // Directly set the tab control's selected tab to avoid circular dependency
                    if (_tabControl != null && _tabControl.SelectedTab != TabDataList[_selectedIndex].TabPage)
                    {
                        _tabControl.SelectedTab = TabDataList[_selectedIndex].TabPage;
                    }

                    foreach (TabData t in TabDataList)
                    {
                        t.Selected = TabDataList[_selectedIndex].Form == t.Form && !t.IsRemoving;
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
            return tabData.Rectangle.Contains(PointToClient(MousePosition));
        }

        private void ProcessTabMouseActions(TabData tabData, MouseEventArgs e)
        {
            // Prevent close button interactions during tab dragging
            if (isMovingTab)
            {
                return;
            }

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
                    foreach (TabData tabData in TabDataList.ToList())
                    {
                        if (!tabData.IsRemoving)
                        {
                            if (IsMouseOverTab(tabData) && !IsMouseOverCloseButton(tabData, e))
                            {
                                moveFrom = GetIndex(tabData);
                                moveTo = moveFrom;
                                _dragX = tabData.Rectangle.Left;
                                tabOldPoint = new Point(PointToClient(MousePosition).X - tabData.Rectangle.Left, PointToClient(MousePosition).Y - tabData.Rectangle.Top);

                                break;
                            }
                            else
                            {
                                locationOldPoint = MousePosition - (Size)FindForm()?.Location;
                            }
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
                foreach (TabData tabData in TabDataList.ToList())
                {
                    if (IsMouseOverTab(tabData))
                    {
                        // Store the mouse position for hover re-evaluation after removal
                        Point mousePos = e.Location;

                        tabData.Form.Close();

                        // Schedule hover re-evaluation after a short delay to allow tab removal to complete
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = _interval };
                        timer.Tick += (s, args) =>
                        {
                            timer.Stop();
                            timer.Dispose();

                            // Re-evaluate hover state
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
                foreach (TabData tabData in TabDataList.ToList())
                {
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

                // Scroll to the selected tab if there's overflow
                if (_maxScrollOffset > 0)
                {
                    int tabIndex = GetIndex(tabData);
                    int tabLeftPosition = tabIndex * tabWidth + _paddingBetweenTabs;
                    int tabRightPosition = tabLeftPosition + tabWidth;

                    // Calculate target scroll offset to show the tab
                    int targetScrollOffset = _scrollOffset;

                    if (tabLeftPosition - _animatedScrollOffset < LeftBoundary)
                    {
                        // Tab is off to the left, scroll to show it
                        targetScrollOffset = tabLeftPosition - LeftBoundary;
                    }
                    else if (tabRightPosition - _animatedScrollOffset > RightBoundary)
                    {
                        // Tab is off to the right, scroll to show it
                        targetScrollOffset = tabRightPosition - RightBoundary;
                    }

                    // Clamp to valid range
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

            // _dragX: client-space left edge of the dragged tab
            _dragX = PointToClient(MousePosition).X - tabOldPoint.X;

            // Constrain drag position to boundaries
            if (_dragX < LeftBoundary)
            {
                _dragX = LeftBoundary;
            }
            else if (_dragX + dragged.Rectangle.Width > RightBoundary)
            {
                _dragX = RightBoundary - dragged.Rectangle.Width;
            }

            int draggedCenter = _dragX + dragged.Rectangle.Width / 2;

            // Resolve which logical slot the dragged tab currently occupies based on its center position
            int newMoveTo = moveFrom;
            for (int i = 0; i < TabDataList.Count; i++)
            {
                if (i == moveFrom) continue;
                TabData other = TabDataList[i];
                if (other == null || other.IsRemoving) continue;

                // Use the tab's current logical slot X (not animated TabLeft) for midpoint comparison
                int logicalX = LeftBoundary + i * (tabWidth + _paddingBetweenTabs);
                int otherMid = logicalX + other.Rectangle.Width / 2;

                if (i < moveFrom && draggedCenter < otherMid)
                {
                    newMoveTo = i;
                    break;
                }
                else if (i > moveFrom && draggedCenter > otherMid)
                {
                    newMoveTo = i;
                }
            }

            // Detect edge cases for swap with first/last tab when dragging to borders
            int firstNonDraggedLogicalX = LeftBoundary + 0 * (tabWidth + _paddingBetweenTabs);
            int lastNonDraggedLogicalRight = LeftBoundary + (TabDataList.Count - 1) * (tabWidth + _paddingBetweenTabs) + dragged.Rectangle.Width;

            // Check if dragged to left border - swap with first tab
            if (_dragX <= LeftBoundary && moveFrom != 0)
            {
                isMovingToFirst = true;
                isMovingToLast = false;
                newMoveTo = 0;
            }
            // Check if dragged to right border - swap with last tab
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

            // When the target slot changes, animate non-dragged tabs to their new visual positions
            if (newMoveTo != moveTo)
            {
                moveTo = newMoveTo;
                AnimateNeighborTabsForDrag();
            }

            Invalidate();
        }

        /// <summary>
        /// Animate all non-dragged tabs to the visual positions they would occupy if the drag were committed now.
        /// The dragged tab's slot (moveFrom) is vacated; tabs between moveFrom and moveTo shift by one slot.
        /// </summary>
        private void AnimateNeighborTabsForDrag()
        {
            if (TabDataList == null || moveFrom < 0 || moveTo < 0) return;

            for (int i = 0; i < TabDataList.Count; i++)
            {
                if (i == moveFrom) continue;

                TabData tab = TabDataList[i];
                if (tab == null || tab.IsRemoving) continue;

                // Compute the logical slot this tab would occupy after the swap
                int logicalSlot = i;

                if (moveFrom < moveTo)
                {
                    // Dragged forward: tabs between (moveFrom+1..moveTo) shift one slot left
                    if (i > moveFrom && i <= moveTo) logicalSlot = i - 1;
                }
                else if (moveFrom > moveTo)
                {
                    // Dragged backward: tabs between (moveTo..moveFrom-1) shift one slot right
                    if (i >= moveTo && i < moveFrom) logicalSlot = i + 1;
                }

                int targetLeft = LeftBoundary + logicalSlot * (tabWidth + _paddingBetweenTabs);

                if (tab.TabLeft != targetLeft)
                {
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(tab, nameof(TabData.TabLeft), targetLeft)
                            .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                    }
                    else
                    {
                        tab.TabLeft = targetLeft;
                    }
                }
            }
        }

        /// <summary>
        /// Snap all tabs back to their natural grid positions after a cancelled drag.
        /// </summary>
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
                        FluentTransitions.Transition.With(tab, nameof(TabData.TabLeft), naturalLeft).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
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
            if (FindForm() is not null) FindForm().Location = locationNewPoint;
        }

        private void CloseAllTabsButThis()
        {
            if (TabDataList.Count <= 1) return;

            foreach (TabData tabData in TabDataList.ToList())
            {
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
            foreach (TabData tabData in TabDataList.ToList())
            {
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
            foreach (TabData tab in TabDataList.ToList())
            {
                if (tab.Form != null) DetachTab(tab);
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
                User32.SendMessage(intPtr, 0x0112 /*WM_SYSCOMMAND*/, 0xF180 /*SC_CONTEXTHELP*/, 0);
            }
        }

        private void DetachForm(System.Windows.Forms.Form form)
        {
            form.Visible = false;
            if (form.Parent != null) form.Parent.Controls.Remove(form);
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

        #endregion

        #region Properties

        /// <summary>
        /// Hooked TabControl. It is better to be <see cref="UI.WP.TablessControl"/>
        /// </summary>
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

        /// <summary>
        /// Selected index of tabs and tabpage in current TabControl
        /// </summary>
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
                    Refresh();
                }
            }
        }
        private int _selectedIndex;

        /// <summary>
        /// Selected tab in current TabControl
        /// </summary>
        public TabPage SelectedTab
        {
            get => _selectedIndex >= 0 && _selectedIndex < TabDataList?.Count
                ? TabDataList[_selectedIndex].TabPage
                : null;
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

        /// <summary>
        /// Selected tab data in current TabControl
        /// </summary>
        public TabData SelectedTabData => _selectedIndex >= 0 && _selectedIndex < TabDataList?.Count
            ? TabDataList[_selectedIndex]
            : null;

        private bool busy = false;

        /// <summary>
        /// Gets or sets if the tabs container is busy with adding/removing tabs
        /// </summary>
        public bool IsBusy => busy;

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Delegate to FormShown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FormShownDelegate(object sender, TabDataEventArgs e);

        /// <summary>
        /// Associated form in tab is shown
        /// </summary>
        public event FormShownDelegate FormShown;

        /// <summary>
        /// Delegate to FormClosed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FormClosedDelegate(object sender, TabDataEventArgs e);

        /// <summary>
        /// Associated form in tab is closed
        /// </summary>
        public event FormClosedDelegate FormClosed;

        /// <summary>
        /// Delegate to FormClosing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FormClosingDelegate(object sender, TabDataEventArgs e);

        /// <summary>
        /// Delegate to FormTextChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FormTextChangedDelegate(object sender, TabDataEventArgs e);

        /// <summary>
        /// Associated form's text in tab is changed
        /// </summary>
        public event FormTextChangedDelegate FormTextChanged;

        /// <summary>
        /// Associated form in tab is shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnFormShown(object sender, TabDataEventArgs e)
        {
            FormShown?.Invoke(sender, e);
        }

        /// <summary>
        /// Associated form's text in tab is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnFormTextChanged(object sender, TabDataEventArgs e)
        {
            FormTextChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Associated form in tab is being closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnFormClosing(object sender, TabDataEventArgs e)
        {
            //Don't use e.TabData.Selected as it causes bug here (TabControl is hidden and not shown again after SelectedIndex change)
            if (e.TabData.TabPage == SelectedTab) Program.Animator.HideSync(TabControl);
            FormClosed?.Invoke(sender, e);
        }

        /// <summary>
        /// Associated form in tab is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                // Animate tab widths when a new tab is added
                UpdateTabPositions(TabDataList, animateWidth: true);
            }
        }

        private void _tabControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage && TabDataList.Any(t => t.TabPage == e.Control))
            {
                TabPage page = e.Control as TabPage;
                RemoveTab(TabDataList.Where(t => t.TabPage == page).FirstOrDefault());
            }
        }

        private async void RemoveTab(TabData tabData, bool animate = true)
        {
            if (tabData == null) return;

            animate &= /*GetIndex(tabData) == SelectedIndex &&*/ animate;

            tabData.IsRemoving = true;

            // Animate removal alpha
            if (CanAnimate_Global && animate)
            {
                FluentTransitions.Transition.With(tabData, nameof(TabData.RemovingAlpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                tabData.RemovingAlpha = 0;
            }

            int SI = SelectedIndex;

            await tabData.Hide(animate, () => AfterRemovingTab(tabData, animate, SI));

            if (FindForm() is not null) FindForm().BackgroundImage = null;
        }

        private void AfterRemovingTab(TabData tabData, bool animate, int SI)
        {
            TabDataList.Remove(tabData);

            UpdateTabPositions(TabDataList, animateWidth: true);

            forceChangeSelectedIndex = true;
            SelectedIndex = SI;

            _tabControl.TabPages.Remove(tabData.TabPage);

            if (FindForm() is not null) FindForm().Visible = _tabControl.TabPages.Count > 0;

            tabData.Dispose();

            // Reset hover state after tab removal to fix hover not working after middle-click close
            State = MouseState.None;
            _hoveredTabData = null;
            overCloseButton = false;

            // Reset all tab hover states
            foreach (TabData t in TabDataList)
            {
                if (t != null)
                {
                    t.Hovered = false;
                    t.CloseButtonAlpha = 0;
                }
            }

            if (CanAnimate_Global)
            {
                FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                HoverSize = 0;
            }

            // Force hover re-evaluation after tab removal
            Point mousePos = PointToClient(MousePosition);
            TabData newHoveredTab = null;
            foreach (TabData tabDataX in TabDataList)
            {
                if (tabDataX != null && tabDataX.Rectangle.Contains(mousePos))
                {
                    newHoveredTab = tabDataX;
                    break;
                }
            }

            // If mouse is over a tab after removal, set hover state directly
            if (newHoveredTab != null)
            {
                _hoveredTabData = newHoveredTab;
                hoverPosition = mousePos;
                int defaultHoverSize = Math.Max(newHoveredTab.Rectangle.Width, newHoveredTab.Rectangle.Height);
                if (CanAnimate_Global)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), defaultHoverSize).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
                else
                {
                    HoverSize = defaultHoverSize;
                }
                newHoveredTab.Hovered = true;
            }

            Refresh();

            // try is made to bypass ex error of that object is in use elsewhere
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

        /// <summary>
        /// Void onResize the control and update the positions of the tabs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (TabDataList != null && TabDataList.Count > 0)
            {
                // Recalculate scroll offset based on new size
                CalculateScrollOffset();

                // Adjust scroll offset to ensure selected tab remains visible
                if (_selectedIndex >= 0 && _selectedIndex < TabDataList.Count)
                {
                    int selectedTabLeft = LeftBoundary + _selectedIndex * (tabWidth + _paddingBetweenTabs) - _animatedScrollOffset;
                    int selectedTabRight = selectedTabLeft + tabWidth;

                    if (selectedTabLeft < LeftBoundary)
                    {
                        // Selected tab is off to the left, scroll to show it
                        int targetOffset = _animatedScrollOffset + (LeftBoundary - selectedTabLeft);
                        AnimateScrollOffset(targetOffset);
                    }
                    else if (selectedTabRight > RightBoundary)
                    {
                        // Selected tab is off to the right, scroll to show it
                        int targetOffset = _animatedScrollOffset + (selectedTabRight - RightBoundary);
                        AnimateScrollOffset(targetOffset);
                    }
                }

                UpdateTabPositions(TabDataList, animateWidth: true);
                Refresh();
            }
        }

        /// <summary>
        /// Void onMouseMove to handle the movement of the tabs and also the associated form
        /// </summary>
        /// <param name="e"></param>
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

            // Handle hover logic for specific tab
            if (!IsBusy)
            {
                HandleHoverForTab(e);
            }

            base.OnMouseMove(e);
        }

        /// Void onMouseClick to process the mouse click on tabs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!IsBusy)
            {
                bool clickedOnTab = false;
                foreach (TabData tabData in TabDataList.ToList())
                {
                    if (IsMouseOverTab(tabData))
                    {
                        ProcessTabMouseActions(tabData, e);
                        clickedOnTab = true;
                        break;
                    }
                }

                // If right-clicked outside tabs, trigger system context menu
                if (!clickedOnTab && e.Button == MouseButtons.Right)
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
                                User32.SendMessage(formHandle, 0x0112 /*WM_SYSCOMMAND*/, new(result), IntPtr.Zero);
                            }
                        }
                    }
                }
            }

            base.OnMouseClick(e);
        }

        /// <summary>
        /// Void onMouseDown to handle the movement of the tabs and also the associated form
        /// </summary>
        /// <param name="e"></param>
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

            // Update hover state for click effect
            TabData clickedTab = GetTabAtMousePosition(e);
            if (clickedTab != null)
            {
                State = MouseState.Down;
                _hoveredTabData = clickedTab;
                hoverPosition = e.Location;

                // Increase hover size to fill the whole tab area (double the max dimension)
                int maxHoverSize = Math.Max(clickedTab.Rectangle.Width, clickedTab.Rectangle.Height) * 2;
                if (CanAnimate_Global)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), maxHoverSize).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                }
                else
                {
                    HoverSize = maxHoverSize;
                }
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Void onMouseDoubleClick to handle double click on tabs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (_tabControl != null && !IsBusy)
            {
                HandleTabControlMouseDoubleClick(e);
            }

            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Handle mouse wheel for tab scrolling when overflow exists
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_maxScrollOffset > 0)
            {
                int scrollDelta = e.Delta;
                int scrollAmount = _scrollAmount; // Amount to scroll per wheel tick

                if (scrollDelta > 0)
                {
                    // Scroll left
                    AnimateScrollOffset(_scrollOffset - scrollAmount);
                }
                else
                {
                    // Scroll right
                    AnimateScrollOffset(_scrollOffset + scrollAmount);
                }

                // Don't call base.OnMouseWheel to suppress default scrolling
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

                    // Animate the dragged tab from its current drag position to its target slot
                    if (capturedFrom < TabDataList.Count && TabDataList[capturedFrom] != null)
                    {
                        TabData dragged = TabDataList[capturedFrom];

                        int finalSlot = capturedToFirst ? 0
                            : capturedToLast ? TabDataList.Count - 1
                            : capturedTo;

                        int targetLeft = LeftBoundary + finalSlot * (tabWidth + _paddingBetweenTabs);

                        // Set TabLeft to current _dragX so the animation starts from the drag position
                        dragged.TabLeft = _dragX;

                        void CommitAfterAnimation()
                        {
                            if (capturedToLast)
                            {
                                // Swap with last tab at right border
                                SwapTabs(capturedFrom, TabDataList.Count - 1);
                            }
                            else if (capturedToFirst)
                            {
                                // Swap with first tab at left border
                                SwapTabs(capturedFrom, 0);
                            }
                            else if (capturedFrom != capturedTo)
                            {
                                SwapTabs(capturedFrom, capturedTo);
                            }
                            else
                            {
                                // No reorder needed; snap all tabs back to their natural positions
                                ResetAllTabLeftPositions();
                            }
                        }

                        if (CanAnimate_Global)
                        {
                            FluentTransitions.Transition
                                .With(dragged, nameof(TabData.TabLeft), targetLeft)
                                .HookOnCompletion(CommitAfterAnimation)
                                .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
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
                    // Not a drag operation - just reset modifiers
                    ResetModifiersToNull();
                }

                // Reset mouse state to allow hover animation to work
                State = MouseState.None;

                // Reset hover size to default value on mouse up
                TabData hoveredTab = GetTabAtMousePosition(new MouseEventArgs(MouseButtons.None, 0, PointToClient(MousePosition).X, PointToClient(MousePosition).Y, 0));
                if (hoveredTab != null)
                {
                    int defaultHoverSize = Math.Max(hoveredTab.Rectangle.Width, hoveredTab.Rectangle.Height);
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(this, nameof(HoverSize), defaultHoverSize).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                    }
                    else
                    {
                        HoverSize = defaultHoverSize;
                    }
                }

                // Re-evaluate hover state after mouse up to restore hover animation
                Point mousePos = PointToClient(MousePosition);
                HandleHoverForTab(new MouseEventArgs(MouseButtons.None, 0, mousePos.X, mousePos.Y, 0));
            }
        }

        /// <summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            // Handle hover on enter
            Point mousePos = PointToClient(MousePosition);
            HandleHoverForTab(new MouseEventArgs(MouseButtons.None, 0, mousePos.X, mousePos.Y, 0));

            base.OnMouseEnter(e);
        }

        /// <summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // Reset all hover states
            foreach (TabData tabData in TabDataList.ToList())
            {
                if (tabData != null)
                {
                    tabData.Hovered = false;
                    tabData.CloseButtonAlpha = 0;
                }
            }

            overCloseButton = false;
            State = MouseState.None;
            _hoveredTabData = null;

            // Reset hover size
            if (CanAnimate_Global)
            {
                FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                HoverSize = 0;
            }

            Invalidate();

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Void onDragEnter to handle the drag and drop of the tabs (for colors items controls: <see cref="ColorItem"/>)
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Void onDragDrop to handle the drag and drop of the tabs (for colors items controls: <see cref="ColorItem"/>)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragOver(DragEventArgs e)
        {
            if (!IsBusy)
            {
                if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
                {
                    Point MousePosition = new(e.X, e.Y);

                    foreach (TabData tabData in TabDataList.ToList())
                    {
                        if (tabData.Rectangle.Contains(PointToClient(MousePosition)))
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
        /// <summary>
        /// Void onParentChanged to get the parent level of the control
        /// </summary>
        /// <param name="e"></param>
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

        private void DrawHover(Graphics G, GraphicsPath path, Rectangle rect, Color color)
        {
            Point clientMousePos = PointToClient(MousePosition);

            using (GraphicsPath clipPath = new())
            {
                RectangleF bounds = path.GetBounds();
                clipPath.AddRectangle(new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height - _clipHeightOffset));

                using (Region clipRegion = new(path))
                {
                    clipRegion.Intersect(clipPath.GetBounds());
                    G.SetClip(clipRegion, CombineMode.Replace);
                }
            }

            using (GraphicsPath gp = new())
            {
                Rectangle circle = new(
                    clientMousePos.X - _hoverSize / 2,
                    clientMousePos.Y - _hoverSize / 2,
                    _hoverSize,
                    _hoverSize);

                gp.AddEllipse(circle);

                using (PathGradientBrush pgb = new(gp)
                {
                    CenterPoint = clientMousePos,
                    CenterColor = color,
                    SurroundColors = [Color.Transparent]
                })
                {
                    G.FillEllipse(pgb, circle);
                }
            }

            G.ResetClip();
        }

        #endregion

        #region Graphics

        private GraphicsPath RR(Rectangle r, int radius, bool rounded)
        {
            GraphicsPath path = new();

            // Extend bottom by 1 to push the open endpoints outside the drawn area, so the stroke between them is never rendered.
            int bottom = r.Bottom + 1;

            if (rounded)
            {
                int diameter = radius * 2;

                // Top-left arc
                path.AddArc(r.X, r.Y, diameter, diameter, 180, 90);

                // Top-right arc
                path.AddArc(r.Right - diameter, r.Y, diameter, diameter, 270, 90);

                // Right edge — goes past the clip boundary
                path.AddLine(r.Right, r.Y + radius, r.Right, bottom);

                // Left edge — goes past the clip boundary
                path.AddLine(r.X, bottom, r.X, r.Y + radius);
            }
            else
            {
                path.AddLine(r.X, bottom, r.X, r.Y);
                path.AddLine(r.X, r.Y, r.Right, r.Y);
                path.AddLine(r.Right, r.Y, r.Right, bottom);
            }

            return path;
        }

        #endregion

        /// <summary>
        /// Paint background of control
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        /// <summary>
        /// Paint control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            // Inferior border
            using (Pen P = new(!OS.W7 ? scheme.Colors.Line_Hover(0) : Color.Black)) { G.DrawLine(P, new Point(0, Height - 1), new Point(Width - 1, Height - 1)); }

            // Set clipping region to prevent tabs from drawing beyond RightBoundary
            Rectangle clipRect = new(LeftBoundary, 0, RightBoundary - LeftBoundary, Height);

            // Draw BETA badge before clipping (it's outside the tab region)
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
                List<TabData> tabsToDraw;
                try
                {
                    tabsToDraw = [.. TabDataList];
                }
                catch
                {
                    tabsToDraw = [];
                }

                if (isMovingTab)
                {
                    TabData movingTab = null;
                    if (tabsToDraw != null && moveFrom != -1 && moveFrom <= tabsToDraw.Count - 1 && tabsToDraw[moveFrom] != null)
                    {
                        movingTab = tabsToDraw[moveFrom];
                    }

                    foreach (TabData tabData in tabsToDraw)
                    {
                        // Only draw tabs that are within or partially within the visible region
                        if (tabData != movingTab && IsTabVisible(tabData.Rectangle)) DrawTab(G, tabData);
                    }

                    if (movingTab is not null)
                    {
                        DrawTab(G, movingTab, true);
                    }
                }
                else
                {
                    foreach (TabData tabData in tabsToDraw)
                    {
                        // Only draw tabs that are within or partially within the visible region
                        if (IsTabVisible(tabData.Rectangle)) DrawTab(G, tabData);
                    }
                }
            }

            // Reset clipping to allow beta badge and other elements to draw properly
            G.ResetClip();

            // Draw scroll indicator when overflow exists
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

            // Clip to everything except the bottom edge row of pixels
            Rectangle clip = new(r.X, r.Y, r.Width + 1, r.Height - 1);
            G.SetClip(clip);
            G.DrawPath(pen, path);

            G.Clip = oldClip;
        }

        /// <summary>
        /// Helper to draw tab element
        /// </summary>
        /// <param name="G"></param>
        /// <param name="tabData"></param>
        /// <param name="isMoving"></param>
        private void DrawTab(Graphics G, TabData tabData, bool isMoving = false)
        {
            // Save the old clip before drawing
            Region oldClip = G.Clip;

            Rectangle rect = !isMoving ? tabData.Rectangle : new Rectangle(_dragX, tabData.Rectangle.Y, tabData.Rectangle.Width, tabData.Rectangle.Height);
            Rectangle textRect = titleRectangle(rect);
            Rectangle closeRect = closeRectangle(rect);
            Rectangle iconRect = iconRectangle(rect);
            Bitmap icon = tabData.Image;

            using (GraphicsPath path = RR(rect, _radius, Program.Style.RoundedCorners))
            {
                if (isMoving)
                {
                    // Draw selected/opened tab background
                    using (SolidBrush br = new(Color.FromArgb(150, scheme_secondary.Colors.Back_Hover(parentLevel))))
                    {
                        G.FillPath(br, path);
                    }

                    // Draw radial hover effect
                    if (tabData == _hoveredTabData && _hoverSize > 0)
                    {
                        Color hoverColor = Color.FromArgb(150, scheme_secondary.Colors.Back_Checked_Hover);
                        DrawHover(G, path, rect, hoverColor);
                    }

                    // Draw border last
                    using (Pen P = new(Color.FromArgb(150, scheme_secondary.Colors.Line_Hover(parentLevel))))
                    using (Pen P_Hover = new(Color.FromArgb(Math.Min(100, tabData.HoverAlpha), scheme_secondary.Colors.ForeColor_Accent)))
                    {
                        DrawTabPath(G, path, P, rect, _radius, Program.Style.RoundedCorners);
                        DrawTabPath(G, path, P_Hover, rect, _radius, Program.Style.RoundedCorners);
                    }
                }
                else
                {
                    // Selection alpha overlay
                    if (tabData.SelectionAlpha > 0)
                    {
                        // Draw selected/opened tab background
                        using (SolidBrush br = new(Color.FromArgb(tabData.SelectionAlpha, scheme.Colors.Back_Hover(parentLevel))))
                        {
                            G.FillPath(br, path);
                        }
                    }

                    // Hover effect
                    if (tabData.HoverAlpha > 0)
                    {
                        // Draw radial hover effect
                        if (tabData == _hoveredTabData && _hoverSize > 0)
                        {
                            Color hoverColor = tabData.Selected ? scheme.Colors.Back_Checked_Hover : Color.FromArgb(170, scheme.Colors.Line_Hover(parentLevel));
                            DrawHover(G, path, rect, hoverColor);
                        }
                    }

                    // Draw border last
                    if (OS.WVista || OS.W7)
                    {
                        // Draw a line around the tab to fix appearance issue that does not fit Windows style
                        using (Pen Px = new(win7BorderColor))
                        {
                            DrawTabPath(G, path, Px, rect, _radius, Program.Style.RoundedCorners);

                            if (tabData.HoverAlpha == 0)
                            {
                                Rectangle rect_smaller = new(rect.X + _rectSmallerOffset, rect.Y + _rectSmallerOffset, rect.Width - _rectSmallerWidthReduction, rect.Height);
                                using (GraphicsPath path_smaller = RR(rect_smaller, _radius - _smallerRadiusOffset, Program.Style.RoundedCorners))
                                {
                                    DrawTabPath(G, path_smaller, Pens.Black, rect_smaller, _radius - _smallerRadiusOffset, Program.Style.RoundedCorners);
                                }
                            }
                        }
                    }
                    else if (OS.W8x)
                    {
                        Color lineColor = Color.White;

                        // Draw a line around the tab to fix appearance issue that does not fit Windows style
                        using (Pen Px = new(Color.FromArgb(50, lineColor)))
                        {
                            DrawTabPath(G, path, Px, rect, _radius, Program.Style.RoundedCorners);
                        }
                    }
                    else
                    {
                        Color normalLineColor = Color.FromArgb(tabData.SelectionAlpha, scheme.Colors.Line_Hover(parentLevel));

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

                    // Removing alpha overlay
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

                    // Draw close button with alpha animation
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

            // Draw close button on tab
            DrawTextOnGlass(G, closeStr, Fonts.ConsoleMedium, Color.FromArgb(255 - tabData.CloseButtonAlpha, ForeColor), closeRect, sf_middleCenter);

            if (tabData.CloseButtonAlpha > 0)
            {
                DrawTextOnGlass(G, closeStr, Fonts.ConsoleMedium, Color.FromArgb(tabData.CloseButtonAlpha, tabData.Selected ? scheme_secondary.Colors.ForeColor_Accent : ForeColor), closeRect, sf_middleCenter);
            }

            // Draw icon and text on tab
            if (icon != null) G.DrawImage(icon, iconRect);

            // Only draw title if tab is not condensed (has room for text)
            if (rect.Width > minTabWidth)
            {
                DrawTextOnGlass(G, tabData.Text, tabData.Selected ? selectedFont : Font, ForeColor, textRect, sf);
            }

            // Restore the old clip after drawing
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