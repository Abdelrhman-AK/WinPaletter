using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.Controllers;
using static WinPaletter.UI.Style.Config;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// Tabs container control that can handle multiple TabPages with forms inside them. For better appearance, use it with TabControl of <see cref="UI.WP.TablessControl"/> and forms having <see cref="TitlebarExtender"/>.
    /// </summary>
    public class TabsContainer : Control
    {
        /// <summary>
        /// Initialize TabsContainer
        /// </summary>
        public TabsContainer()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.ContainerControl | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            AllowDrop = true;

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

        private Rectangle hoveredRectangle;
        private bool overCloseButton = false;

        private readonly int MaxWidth = 245;
        private readonly int paddingBetweenTabs = 5;

        private bool forceChangeSelectedIndex = true;

        private int moveFrom = -1;
        private int moveTo = -1;
        private bool isMovingTab = false;
        private bool isMovingToLast = false;
        private bool isMovingToFirst = false;

        private Point tabNewPoint = new();
        private Point tabOldPoint = new();

        private Point locationNewPoint = new();
        private Point locationOldPoint = new();

        /// <summary>
        /// Padding of tabs from top of the control
        /// </summary>
        public readonly int upperTabPadding = 5;

        private readonly UI.WP.ContextMenuStrip contextMenu = new() { ShowImageMargin = true, AllowTransparency = true };
        private TabData contextItemDropped;

        Scheme scheme => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
        Scheme scheme_secondary => Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;

        #endregion

        #region Methods

        private void InitializeContextMenu()
        {
            contextMenu.ItemHeight = 24;

            ToolStripMenuItem closeButton = new(Program.Lang.Strings.General.Close) { Image = Assets.Tabs.ContextBox_Close };
            ToolStripMenuItem closeAllButThis = new(Program.Lang.Strings.Tabs.Context_CloseOthers) { Image = Assets.Tabs.ContextBox_CloseAllButThis };
            ToolStripMenuItem closeAllToTheRight = new(Program.Lang.Strings.Tabs.Context_CloseToTheRight) { Image = Assets.Tabs.ContextBox_CloseRight };
            ToolStripMenuItem closeAllToTheLeft = new(Program.Lang.Strings.Tabs.Context_CloseToTheLeft) { Image = Assets.Tabs.ContextBox_CloseLeft };
            ToolStripMenuItem closeAll = new(Program.Lang.Strings.Tabs.Context_CloseAll) { Image = Assets.Tabs.ContextBox_CloseAll };
            ToolStripSeparator toolStripSeparator0 = new();
            ToolStripMenuItem detach = new(Program.Lang.Strings.Tabs.Context_Unpin) { Image = Assets.Tabs.ContextBox_Detach };
            ToolStripMenuItem detachAll = new(Program.Lang.Strings.Tabs.Context_UnpinAll) { Image = Assets.Tabs.ContextBox_DetachAll };
            ToolStripMenuItem detachAllButThis = new(Program.Lang.Strings.Tabs.Context_UnpinOthers) { Image = Assets.Tabs.ContextBox_DetachAllButThis };
            ToolStripSeparator toolStripSeparator1 = new();
            ToolStripMenuItem helpButton = new(Program.Lang.Strings.General.Help) { Image = Assets.Tabs.ContextBox_Help };

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
        /// Generate a tab page that have form inside, and add it into the tab control
        /// </summary>
        /// <param name="form"></param>
        public void AddFormIntoTab(Form form)
        {
            Cursor = Cursors.WaitCursor;

            foreach (TabPage TPx in TabControl.TabPages)
            {
                if (TPx.Controls.Contains(form))
                {
                    TabControl.SelectedTab = TPx;
                    Cursor = Cursors.Default;
                    if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"`{form.Name}` form is already shown and added into tabs, re-focusing it.");
                    return;
                }
            }

            if (busy)
            {
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Warning, $"`{form.Name}` form cannot be added into tabs as the tabs container is busy.");
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

            TP.Text = form.Text;

            if (!DesignMode && !TabControl.IsInUse()) Program.Animator.HideSync(TabControl);

            TP.Controls.Add(form);

            //UI.WP.SkeletonOverlay skeleton = new() { AnimationSpeed = 16, TickInterval = 26 };

            //form.Load += (s, e) =>
            //{
            //    skeleton?.AttachTo(form);
            //    skeleton?.Start();
            //};

            //form.Shown += (s, e) =>
            //{
            //    skeleton?.Stop();
            //    skeleton?.Dispose();
            //    skeleton = null;
            //};

            form.Show();

            forceChangeSelectedIndex = true;
            TabControl.TabPages.Add(TP);
            SelectedTab = TP;

            TabControl.FindForm().Visible = true;

            if (form is AspectsTemplate)
            {
                (form as AspectsTemplate).titlebarExtender1.Flag = TitlebarExtender.Flags.Tabs_Extended;
            }
            else if (form is Form && form.Controls.OfType<TitlebarExtender>().Count() > 0)
            {
                form.Controls.OfType<TitlebarExtender>().FirstOrDefault().Flag = TitlebarExtender.Flags.Tabs_Extended;
            }

            if (!DesignMode && !TabControl.IsInUse()) Program.Animator.ShowSync(TabControl);

            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"`{form.Name}` form has been shown and added into tabs.");

            busy = false;

            Cursor = Cursors.Default;
        }

        private int tabWidth => Math.Min(MaxWidth, Width / _tabControl.TabPages.Count);
        private int tabHeight => Height - upperTabPadding;

        /// <summary>
        /// Count of tabs in current TabContainer
        /// </summary>
        public int TabsCount => TabDataList != null ? TabDataList.Count : 0;

        private Rectangle closeRectangle(Rectangle rectangle)
        {
            int size = 15;
            return new Rectangle(rectangle.Right - size - 5, rectangle.Y + (rectangle.Height - size) / 2, size, size);
        }

        private Rectangle iconRectangle(Rectangle rectangle)
        {
            int size = 16;
            return new Rectangle(rectangle.X + 5, rectangle.Y + (rectangle.Height - size) / 2, size, size);
        }

        private Rectangle titleRectangle(Rectangle rectangle)
        {
            Rectangle iconRect = iconRectangle(rectangle);
            Rectangle closeRect = closeRectangle(rectangle);

            return Rectangle.FromLTRB(iconRect.Right + 4, iconRect.Top, closeRect.Left - 5, iconRect.Bottom);
        }

        private int GetIndex(TabData tabData)
        {
            if (TabDataList != null && TabDataList.Count > 0)
            {
                for (int i = 0; i < TabDataList.Count; i++)
                {
                    if (TabDataList[i]?.Rectangle == tabData.Rectangle)
                    {
                        return i;
                    }
                }
            }

            return -1;
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

                UpdateTabPositions(TabDataList);

                // to avoid bug of non-selection
                forceChangeSelectedIndex = true;
                SelectedIndex = to;

                isMovingTab = false;

                ResetModifiersToNull();

                Refresh();
            }
        }

        private void MoveToLast(int from)
        {
            if (TabDataList != null)
            {
                TabData itemFrom = TabDataList.ElementAt(from);

                if (itemFrom == null || itemFrom.IsRemoving) return;

                TabDataList.RemoveAt(from);
                TabDataList.Add(itemFrom);

                UpdateTabPositions(TabDataList);

                // to avoid bug of non-selection
                forceChangeSelectedIndex = true;
                SelectedIndex = TabDataList.Count - 1;

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

                UpdateTabPositions(TabDataList);

                // to avoid bug of non-selection
                forceChangeSelectedIndex = true;
                SelectedIndex = 0;

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
        }

        private TabData GetTabAtMousePosition(MouseEventArgs e)
        {
            List<TabData> tabDatas = [.. TabDataList];
            foreach (TabData tabData in tabDatas)
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
            Rectangle tabRectangle = new(index * tabWidth + paddingBetweenTabs, upperTabPadding, tabWidth - paddingBetweenTabs, tabHeight);
            return new TabData(this, page, tabRectangle);
        }

        private void UpdateTabPositions(List<TabData> collection)
        {
            int collectionCount = collection.Count;

            if (collectionCount == 0)
            {
                // Handle the case where the TabDataList is empty
                return;
            }

            if (_selectedIndex >= 0 && _selectedIndex < collectionCount)
            {
                List<TabData> tabDatas = [.. collection];
                collection.Clear();

                int i = 0;
                foreach (TabData tabData in tabDatas)
                {
                    if (tabData != null && tabData.TabPage != null && !tabData.IsRemoving)
                    {
                        TabData tabDataX = CreateTabData(tabData.TabPage, i);
                        tabDataX.Selected = i == _selectedIndex;
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
                    SelectedTab = TabDataList[_selectedIndex].TabPage;

                    List<TabData> tabDatas = [.. TabDataList];
                    foreach (TabData t in tabDatas)
                    {
                        t.Selected = TabDataList[_selectedIndex].Form == t.Form && !t.IsRemoving;
                    }
                    TabDataList = tabDatas;
                }
                else
                {
                    SelectedTab = null;
                }
            }
        }

        private bool IsMouseOverTab(TabData tabData)
        {
            return tabData.Rectangle.Contains(PointToClient(MousePosition));
        }

        private void ProcessTabMouseActions(TabData tabData, MouseEventArgs e)
        {
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
                    List<TabData> tabDatas = [.. TabDataList];
                    foreach (TabData tabData in tabDatas)
                    {
                        if (!tabData.IsRemoving)
                        {
                            if (IsMouseOverTab(tabData) && !IsMouseOverCloseButton(tabData, e))
                            {
                                moveFrom = GetIndex(tabData);
                                tabOldPoint = MousePosition - (Size)tabData.Rectangle.Location;

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
                List<TabData> tabDatas = [.. TabDataList];
                foreach (TabData tabData in tabDatas)
                {
                    if (IsMouseOverTab(tabData))
                    {
                        tabData.Form.Close();
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
                List<TabData> tabDatas = [.. TabDataList];
                foreach (TabData tabData in tabDatas)
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
                Form form = TabControl.FindForm();
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
            else if (e.Button == MouseButtons.None)
            {
                HandleMouseHover(e);
            }
        }

        private void HandleTabMove(MouseEventArgs e)
        {
            TabData tabData = GetTabAtMousePosition(e);

            if (tabData != null)
            {
                // Set properties for tabData movement with index information
                SetTabMoveProperties(GetIndex(tabData), true, false, false);
            }
            else
            {
                // Check if the cursor is to the right of the last tabData
                if (e.X > TabDataList.Last().Rectangle.Right)
                {
                    SetTabMoveProperties(-1, true, false, true);
                }
                // Check if the cursor is to the left of the first tabData
                else if (e.X < TabDataList.First().Rectangle.Left)
                {
                    SetTabMoveProperties(-1, true, true, false);
                }
            }

            // If tabData movement is enabled, update the tabData's position and refresh the UI
            if (isMovingTab)
            {
                // GetTextAndImageRectangles the new position of the tabData based on mouse movement
                tabNewPoint = MousePosition - (Size)tabOldPoint;
                Refresh();
            }
        }

        private void HandleFormMove()
        {
            locationNewPoint = MousePosition - (Size)locationOldPoint;
            FindForm().Location = locationNewPoint;
        }

        private void HandleMouseHover(MouseEventArgs e)
        {
            TabData hoveredTab = null;

            foreach (TabData tabData in TabDataList)
            {
                if (tabData.Rectangle.Contains(e.Location))
                {
                    tabData.Hovered = true;
                    hoveredRectangle = tabData.Rectangle;
                    hoveredTab = tabData;
                }
                else
                {
                    tabData.Hovered = false;
                    hoveredRectangle = Rectangle.Empty;
                }
            }

            if (hoveredTab is not null) overCloseButton = closeRectangle(hoveredTab.Rectangle).Contains(e.Location); else overCloseButton = false;

            if (hoveredRectangle != Rectangle.Empty) Invalidate(hoveredRectangle);
        }

        private void SetTabMoveProperties(int moveToIndex, bool moveTab, bool moveToFirst, bool moveToLast)
        {
            moveTo = moveToIndex;
            isMovingTab = moveTab;
            isMovingToFirst = moveToFirst;
            isMovingToLast = moveToLast;
        }

        private void CloseAllTabsButThis()
        {
            if (TabDataList.Count > 1)
            {
                List<TabData> tabDatas = [.. TabDataList];
                foreach (TabData tabData in tabDatas)
                {
                    if (tabData.TabPage != contextItemDropped.TabPage)
                    {
                        tabData.Form.Close();
                    }
                }
            }
        }

        private void CloseAllTabsToTheRight()
        {
            int index = TabDataList.IndexOf(contextItemDropped);

            for (int i = index + 1; i < TabDataList.Count; i += 0)
            {
                TabDataList[i].Form.Close();
            }
        }

        private void CloseAllTabsToTheLeft()
        {
            int index = TabDataList.IndexOf(contextItemDropped);

            for (int i = 0; i < index; i++)
            {
                TabDataList[0].Form.Close();
            }
        }

        private void CloseAllTabs()
        {
            int count = TabDataList.Count;
            for (int i = 0; i < count; i++)
            {
                TabDataList[0].Form.Close();
            }
        }

        private void DetachTab(TabData tabData)
        {
            if (tabData.Form != null) DetachForm(tabData.Form);

            RemoveTab(tabData, false);

            if (TabDataList.Count == 0) TabControl.FindForm().Visible = false;

            tabData.Form.Visible = true;
        }

        private void DetachAllTabs()
        {
            int count = TabDataList.Count;
            for (int i = 0; i <= count - 1; i++)
            {
                if (TabDataList[0].Form != null) DetachTab(TabDataList[0]);
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

        private void DetachForm(Form form)
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
            else if (form is Form && form.Controls.OfType<TitlebarExtender>().Count() > 0)
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
                ? TabDataList.ElementAt(_selectedIndex).TabPage
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
            ? TabDataList.ElementAt(_selectedIndex)
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
        /// Associated form in tab is being closed
        /// </summary>
        public event FormClosingDelegate FormClosing;

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

        private async void _tabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage)
            {
                TabPage page = e.Control as TabPage;

                TabDataList.Add(CreateTabData(page, TabDataList.Count));

                TabDataList[TabDataList.Count - 1].TabTop = Height;

                SelectedIndex = TabDataList.Count - 1;

                await TabDataList[TabDataList.Count - 1].Show(() => UpdateTabPositions(TabDataList));
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

            int SI = SelectedIndex;

            await tabData.Hide(animate, () => AfterRemovingTab(tabData, animate, SI));
        }

        private void AfterRemovingTab(TabData tabData, bool animate, int SI)
        {
            TabDataList.Remove(tabData);

            UpdateTabPositions(TabDataList);

            forceChangeSelectedIndex = true;
            SelectedIndex = SI;

            _tabControl.TabPages.Remove(tabData.TabPage);

            tabData.Dispose();

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
                UpdateTabPositions(TabDataList);
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

            base.OnMouseMove(e);
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
        /// Void onMouseUp to handle the movement of the tabs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!IsBusy)
            {
                if (isMovingToLast)
                {
                    MoveToLast(moveFrom);
                }
                else if (isMovingToFirst)
                {
                    MoveToFirst(moveFrom);
                }
                else if (moveFrom > -1 && moveTo > -1 && moveFrom != moveTo)
                {
                    SwapTabs(moveFrom, moveTo);
                }

                ResetModifiersToNull();
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Void onMouseLeave to refresh tabs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            hoveredRectangle = Rectangle.Empty;

            foreach (TabData tabData in TabDataList)
            {
                tabData.Hovered = false;
                overCloseButton = false;
            }

            Refresh();

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Void onMouseClick to process the mouse click on tabs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!IsBusy)
            {
                List<TabData> tabDatas = [.. TabDataList];
                foreach (TabData tabData in tabDatas)
                {
                    if (IsMouseOverTab(tabData))
                    {
                        ProcessTabMouseActions(tabData, e);
                        break;
                    }
                }
            }

            base.OnMouseClick(e);
        }

        /// <summary>
        /// Void onDragEnter to handle the drag and drop of the tabs (for colors items controls: <see cref="ColorItem"/>)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragEnter(DragEventArgs e)
        {
            if (!IsBusy)
            {
                if (e.Data.GetDataPresent(typeof(Form)))
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

                    List<TabData> tabDatas = [.. TabDataList];
                    foreach (TabData tabData in tabDatas)
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

        #region Graphics

        private GraphicsPath RR(Rectangle r, int radius, bool rounded)
        {
            GraphicsPath path = new();

            if (rounded)
            {
                // Create points for the path based on the provided Rectangle and radius
                int diameter = 2 * radius;
                Rectangle arcRect = new(r.X, r.Y, diameter, diameter);

                // Top left corner
                path.AddArc(arcRect, 180, 90);

                // Top edge
                path.AddLine(r.X + radius, r.Y, r.Right - radius, r.Y);

                // Top right corner
                arcRect.X = r.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // Right edge
                path.AddLine(r.Right, r.Y + radius, r.Right, r.Bottom - radius);

                // Bottom right corner
                path.AddLine(r.Right, r.Bottom - radius, r.Right, r.Bottom);

                // Bottom edge
                path.AddLine(r.Right, r.Bottom, r.X, r.Bottom);

                // Bottom left corner
                path.AddLine(r.X, r.Bottom, r.X, r.Bottom - radius);

                // Left edge
                path.AddLine(r.X, r.Bottom - radius, r.X, r.Y + radius);

                // Top left corner
                path.CloseFigure();
            }
            else
            {
                // Create a path with sharp corners (no curves)
                path.AddPolygon(
                [
            new Point(r.X, r.Y),
            new Point(r.Right, r.Y),
            new Point(r.Right, r.Bottom),
            new Point(r.X, r.Bottom),
        ]);
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

            if (Program.IsBeta)
            {
                Rectangle rect = new(0, 0, Width - 1, Height - 1);
                SizeF betaSize = Program.Lang.Strings.General.Beta.ToUpper().Measure(Fonts.ConsoleMedium) + new SizeF(2, 2);
                Rectangle betaRect = new(Width - (int)betaSize.Width - 5, rect.Y + (int)((rect.Height - betaSize.Height) / 2), (int)betaSize.Width, (int)betaSize.Height);
                G.FillRoundedRect(scheme_secondary.Brushes.Back_Checked, betaRect);
                G.DrawRoundedRectBeveled(scheme_secondary.Pens.Line_Checked, betaRect);
                using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                {
                    betaRect.Y++;
                    G.DrawString(Program.Lang.Strings.General.Beta.ToUpper(), Fonts.ConsoleMedium, scheme_secondary.Brushes.ForeColor_Accent, betaRect, sf);
                }
            }

            // Inferior border
            using (Pen P = new(scheme.Colors.Line_Hover(0))) { G.DrawLine(P, new Point(0, Height - 1), new Point(Width - 1, Height - 1)); }

            if (_tabControl != null)
            {
                List<TabData> tabsToDraw = [.. TabDataList];

                if (isMovingTab)
                {
                    if (tabsToDraw != null && moveFrom != -1 && moveFrom <= tabsToDraw.Count - 1 && tabsToDraw[moveFrom] != null)
                    {
                        DrawTab(G, tabsToDraw[moveFrom], true);
                    }

                    foreach (TabData tabData in tabsToDraw)
                    {
                        if (tabData != tabsToDraw[moveFrom]) DrawTab(G, tabData);
                    }
                }
                else
                {
                    foreach (TabData tabData in tabsToDraw)
                    {
                        DrawTab(G, tabData);
                    }
                }
            }
        }

        /// <summary>
        /// Helper to draw tab element
        /// </summary>
        /// <param name="G"></param>
        /// <param name="tabData"></param>
        /// <param name="isMoving"></param>
        private void DrawTab(Graphics G, TabData tabData, bool isMoving = false)
        {
            Rectangle rect = !isMoving ? tabData.Rectangle : new Rectangle(tabNewPoint.X, tabData.Rectangle.Y, tabData.Rectangle.Width, tabData.Rectangle.Height);

            Bitmap icon = tabData.Image;

            using (GraphicsPath path = RR(rect, 5, Program.Style.RoundedCorners))
            {
                if (isMoving)
                {
                    using (LinearGradientBrush lgb_back = new(rect, Color.FromArgb(128, scheme_secondary.Colors.Back_Checked), Color.Transparent, LinearGradientMode.Vertical))
                    using (LinearGradientBrush lgb_border = new(rect, Color.FromArgb(128, scheme_secondary.Colors.Line_Checked_Hover), Color.Transparent, LinearGradientMode.Vertical))
                    using (Pen P_hover = new(lgb_border))
                    {
                        G.FillPath(lgb_back, path);
                        G.DrawPath(P_hover, path);
                    }
                }
                else
                {
                    if (tabData.Selected)
                    {
                        // Draw selected/opened tab

                        using (SolidBrush br = new(scheme.Colors.Back_Hover(parentLevel)))
                        using (Pen P = new(scheme.Colors.Line_Hover(parentLevel)))
                        {
                            G.FillPath(br, path);

                            if (OS.WVista || OS.W7 || OS.W8x)
                            {
                                // Draw a line around the tab to fix appearance issue that doesn't fit Windows style
                                using (Pen Px = new(Color.FromArgb(OS.W8x ? 50 : 128, 255, 255, 255)))
                                {
                                    G.ExcludeClip(new Rectangle(rect.X, rect.Y + rect.Height - 2, rect.Width + 1, 2));
                                    G.DrawPath(Px, path);
                                    G.ResetClip();
                                }
                            }
                            else
                            {
                                G.DrawPath(P, path);
                            }
                        }
                    }
                    else
                    {
                        // Draw normal tab
                        if (Parent is null || Parent.FindForm() is null)
                        {
                            using (SolidBrush br = new(scheme.Colors.Back(parentLevel)))
                            using (Pen P = new(scheme.Colors.Line(parentLevel)))
                            {
                                G.FillPath(br, path);
                                G.DrawPath(P, path);
                            }
                        }
                    }

                    using (LinearGradientBrush lgb_back = new(rect, Color.FromArgb(tabData.HoverAlpha, tabData.Selected ? scheme.Colors.Back_Checked : scheme.Colors.Back_Hover()), Color.Transparent, LinearGradientMode.Vertical))
                    using (LinearGradientBrush lgb_border = new(rect, Color.FromArgb(tabData.HoverAlpha, tabData.Selected ? scheme.Colors.Line_Checked_Hover : scheme.Colors.Line_Hover()), Color.Transparent, LinearGradientMode.Vertical))
                    using (Pen P_hover = new(lgb_border))
                    {
                        G.FillPath(lgb_back, path);
                        G.DrawPath(P_hover, path);
                    }

                    // Draw close button on hovered tab
                    if (tabData.Hovered & overCloseButton)
                    {
                        using (LinearGradientBrush lgb0 = new(closeRectangle(rect), scheme_secondary.Colors.Back_Checked, scheme_secondary.Colors.Back_Checked_Hover, LinearGradientMode.Vertical))
                        using (LinearGradientBrush lgb1 = new(closeRectangle(rect), scheme_secondary.Colors.Line_Checked, scheme_secondary.Colors.Line_Checked_Hover, LinearGradientMode.Vertical))
                        using (Pen P = new(lgb1))
                        {
                            G.FillRoundedRect(lgb0, closeRectangle(rect));
                            G.DrawRoundedRectBeveled(P, closeRectangle(rect));
                        }
                    }
                }
            }

            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            using (StringFormat sf_close = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

                Rectangle textRect = titleRectangle(rect);
                Rectangle closeRect = closeRectangle(rect);
                Rectangle iconRect = iconRectangle(rect);

                // Draw close button on tab
                DrawTextOnGlass(G, "✕", Fonts.ConsoleMedium, ForeColor, closeRect, sf_close);

                // Draw icon and text on tab
                if (icon != null) G.DrawImage(icon, iconRect);

                Font fontSelected = tabData.Selected ? new(Font.Name, Font.Size, FontStyle.Bold) : null;

                DrawTextOnGlass(G, tabData.Text, fontSelected ?? Font, ForeColor, textRect, sf);
            }
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