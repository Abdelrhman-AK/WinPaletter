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
    public class TabsContainer : Control
    {
        /// <summary>
        /// Initialize TabsContainer
        /// </summary>
        public TabsContainer()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            AllowDrop = true;

            InitializeContextMenu();

            Controls.Add(helpButton);
            helpButton.Click += HelpButton_Click; ;
        }

        #region Variables

        UI.WP.Button helpButton = new()
        {
            Visible = false,
            Text = string.Empty,
            Size = new(20, 20),
            Anchor = AnchorStyles.Right,
            ImageGlyphEnabled = true,
            ImageGlyph = Properties.Resources.Glyph_Help,
            Flag = UI.WP.Button.Flags.CustomColorOnHover,
            CustomColor = Color.FromArgb(193, 156, 0),
        };

        private bool CanAnimate_Global => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private readonly static TextureBrush Noise = new(Properties.Resources.Noise.Fade(0.55f));

        /// <summary>
        /// List of tab data included in current control
        /// </summary>
        public List<TabData> TabDataList = new();

        private Rectangle hoveredRectangle;
        private bool overCloseButton = false;

        private int MaxWidth = 245;
        private int paddingBetweenTabs = 5;

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

        private int upperTabPadding = 5;

        private UI.WP.ContextMenuStrip contextMenu = new() { ShowImageMargin = true, AllowTransparency = true };
        private TabData contextItemDropped;

        Scheme scheme => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
        Scheme scheme_secondary => Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;

        #endregion

        #region Methods

        private void InitializeContextMenu()
        {
            contextMenu.ItemHeight = 20;

            ToolStripMenuItem closeButton = new(Program.Lang.Close) { Image = Assets.Tabs.ContextBox_Close };
            ToolStripMenuItem closeAllButThis = new(Program.Lang.Tab_Context_CloseOthers) { Image = Assets.Tabs.ContextBox_CloseAllButThis };
            ToolStripMenuItem closeAllToTheRight = new(Program.Lang.Tab_Context_CloseToTheRight) { Image = Assets.Tabs.ContextBox_CloseRight };
            ToolStripMenuItem closeAllToTheLeft = new(Program.Lang.Tab_Context_CloseToTheLeft) { Image = Assets.Tabs.ContextBox_CloseLeft };
            ToolStripMenuItem closeAll = new(Program.Lang.Tab_Context_CloseAll) { Image = Assets.Tabs.ContextBox_CloseAll };
            ToolStripSeparator toolStripSeparator = new();
            ToolStripMenuItem detach = new(Program.Lang.Tab_Context_Detach) { Image = Assets.Tabs.ContextBox_Detach };
            ToolStripMenuItem detachAll = new(Program.Lang.Tab_Context_DetachAll) { Image = Assets.Tabs.ContextBox_DetachAll };
            ToolStripMenuItem detachAllButThis = new(Program.Lang.Tab_Context_DetachOthers) { Image = Assets.Tabs.ContextBox_DetachAllButThis };

            closeButton.Click += (s, e) => contextItemDropped.Form.Close();
            closeAllButThis.Click += (s, e) => CloseAllTabsButThis();
            closeAllToTheRight.Click += (s, e) => CloseAllTabsToTheRight();
            closeAllToTheLeft.Click += (s, e) => CloseAllTabsToTheLeft();
            closeAll.Click += (s, e) => CloseAllTabs();
            detach.Click += (s, e) => DetachTab(contextItemDropped);
            detachAll.Click += (s, e) => DetachAllTabs();
            detachAllButThis.Click += (s, e) => DetachAllTabsButThis();

            contextMenu.Items.AddRange(new ToolStripItem[] { closeButton, closeAllToTheRight, closeAllToTheLeft, closeAll, closeAllButThis, toolStripSeparator, detach, detachAll, detachAllButThis });
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
                    return;
                }
            }

            TabPage TP = new() { BackColor = BackColor };

            form.TopLevel = false;
            form.Parent = TP;

            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.AllowDrop = true;
            form.WindowState = FormWindowState.Normal;

            TP.Text = form.Text;
            // If there is a bug, make TP.Controls.Add(form); here, not after adding and selecting tab page
            TP.Controls.Add(form);

            if (!DesignMode) Program.Animator.HideSync(TabControl);

            // If there is a bug, make form.Show(); here, not after adding and selecting tab page
            form.Show();

            forceChangeSelectedIndex = true;
            TabControl.TabPages.Add(TP);
            SelectedTab = TP;

            TabControl.FindForm().Visible = true;

            if (form is AspectsTemplate)
            {
                (form as AspectsTemplate).titlebarExtender1.DropDWMEffect = false;
            }
            else if (form is Form && form.Controls.OfType<Tabs.TitlebarExtender>().Count() > 0)
            {
                form.Controls.OfType<Tabs.TitlebarExtender>().FirstOrDefault().DropDWMEffect = false;
            }

            if (!DesignMode) Program.Animator.ShowSync(TabControl);

            Cursor = Cursors.Default;
        }

        private int tabWidth => Math.Min(MaxWidth, Width / _tabControl.TabPages.Count);
        private int tabHeight => Height - upperTabPadding;

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

            return Rectangle.FromLTRB(iconRect.Right + 5, iconRect.Top, closeRect.Left - 5, iconRect.Bottom);
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

        private async void RemoveTab(TabData tabData, bool animate = true)
        {
            if (tabData == null) return;

            bool canAnimate = GetIndex(tabData) == SelectedIndex && animate;

            tabData.IsRemoving = true;

            int SI = SelectedIndex;

            if (!canAnimate) AfterRemovingTab(tabData, canAnimate, SI);
            else
            {
                await Task.Run(() =>
                {
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(tabData, nameof(tabData.TabTop), Height)
                        .HookOnCompletion(() => { Invoke(() => AfterRemovingTab(tabData, canAnimate, SI)); })
                        .CriticalDamp(TimeSpan.FromMilliseconds(animate ? Program.AnimationDuration_Quick : 1));
                    }
                    else
                    {
                        tabData.TabTop = Height;
                        AfterRemovingTab(tabData, canAnimate, SI);
                    }

                });
            }
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

            if (!DesignMode && animate) Program.Animator.ShowSync(TabControl);
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
            List<TabData> tabDatas = new(TabDataList);
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
                List<TabData> tabDatas = new(collection);
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

                    List<TabData> tabDatas = new(TabDataList);
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
                List<TabData> tabDatas = new(TabDataList);
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
            else if (e.Button == MouseButtons.Middle)
            {
                List<TabData> tabDatas = new(TabDataList);
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
                List<TabData> tabDatas = new(TabDataList);
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
            contextMenu.Show(this, tabData.Rectangle.Location + new Size(0, tabData.Rectangle.Height + 3));
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
                // Calculate the new position of the tabData based on mouse movement
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
            List<TabData> tabDatas = new(TabDataList);
            foreach (TabData tabData in tabDatas)
            {
                if (tabData.Rectangle.Contains(e.Location))
                {
                    SetHoverProperties(tabData.Rectangle, e);
                    break;
                }
                else
                {
                    ClearHoverProperties();
                }
            }
        }

        private void SetTabMoveProperties(int moveToIndex, bool moveTab, bool moveToFirst, bool moveToLast)
        {
            moveTo = moveToIndex;
            isMovingTab = moveTab;
            isMovingToFirst = moveToFirst;
            isMovingToLast = moveToLast;
        }

        private void SetHoverProperties(Rectangle tabRectangle, MouseEventArgs e)
        {
            hoveredRectangle = tabRectangle;
            overCloseButton = closeRectangle(tabRectangle).Contains(e.Location);
            Refresh();
        }

        private void ClearHoverProperties()
        {
            hoveredRectangle = Rectangle.Empty;
            overCloseButton = false;
            Invalidate();
        }

        private void CloseAllTabsButThis()
        {
            if (TabDataList.Count > 1)
            {
                List<TabData> tabDatas = new(TabDataList);
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
                (form as AspectsTemplate).titlebarExtender1.DropDWMEffect = true;
            }
            else if (form is Form && form.Controls.OfType<Tabs.TitlebarExtender>().Count() > 0)
            {
                form.Controls.OfType<Tabs.TitlebarExtender>().FirstOrDefault().DropDWMEffect = true;
            }

            form.Show();
            ApplyStyle(form);
            form.BringToFront();
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
                if ((_selectedIndex != value || forceChangeSelectedIndex))
                {
                    forceChangeSelectedIndex = false;
                    _selectedIndex = AdjustSelectedIndex(value);
                    helpButton.Visible = SelectedTabData?.Form != null && SelectedTabData.Form.HelpButton;
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

        private void HelpButton_Click(object sender, EventArgs e)
        {
            IntPtr intPtr = SelectedTabData?.Form?.Handle ?? IntPtr.Zero;
            if (intPtr != IntPtr.Zero)
            {
                User32.SendMessage(intPtr, 0x0112 /*WM_SYSCOMMAND*/, 0xF180 /*SC_CONTEXTHELP*/, 0);
            }
        }

        private async void _tabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage)
            {
                TabPage page = e.Control as TabPage;

                TabDataList.Add(CreateTabData(page, TabDataList.Count));

                TabDataList[TabDataList.Count - 1].TabTop = Height;

                SelectedIndex = TabDataList.Count - 1;

                await Task.Run(() =>
                {
                    if (CanAnimate_Global)
                    {
                        FluentTransitions.Transition.With(TabDataList[TabDataList.Count - 1], nameof(TabData.TabTop), upperTabPadding)
                            .HookOnCompletion(() =>
                            {
                                Invoke(() =>
                                {
                                    UpdateTabPositions(TabDataList);
                                    Refresh();
                                });
                            })
                            .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                    else
                    {
                        TabDataList[TabDataList.Count - 1].TabTop = upperTabPadding;
                        UpdateTabPositions(TabDataList);
                        Refresh();
                    }
                });
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

        private void _tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTab = _tabControl.SelectedTab;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            SizeF betaSize = Program.Lang.Beta.ToUpper().Measure(Font) + new SizeF(2, 3);
            Rectangle betaRect = new(0 + Width - (int)betaSize.Width - 5 - 1, 0 + (int)((Height - 1 - betaSize.Height) / 2), (int)betaSize.Width, (int)betaSize.Height);

            helpButton.Left = Program.IsBeta ? betaRect.Left - helpButton.Width - 5 : betaRect.Right - helpButton.Width;
            helpButton.Top = (Height - helpButton.Height) / 2;

            if (TabDataList != null && TabDataList.Count > 0)
            {
                UpdateTabPositions(TabDataList);

                Refresh();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_tabControl != null)
            {
                HandleTabControlMouseMove(e);
            }
            else if (FindForm() != null && e.Button == MouseButtons.Left)
            {
                HandleFormMove();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_tabControl != null)
            {
                HandleTabControlMouseButtonClick(e);
            }
            else
            {
                locationOldPoint = MousePosition - (Size)FindForm()?.Location;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (_tabControl != null)
            {
                HandleTabControlMouseDoubleClick(e);
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
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

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hoveredRectangle = Rectangle.Empty;

            Refresh();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            List<TabData> tabDatas = new(TabDataList);
            foreach (TabData tabData in tabDatas)
            {
                if (IsMouseOverTab(tabData))
                {
                    ProcessTabMouseActions(tabData, e);
                    break;
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnDragEnter(DragEventArgs e)
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

            base.OnDragEnter(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {

            base.OnDragDrop(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
            {
                Point MousePosition = new(e.X, e.Y);

                List<TabData> tabDatas = new(TabDataList);
                foreach (TabData tabData in tabDatas)
                {
                    if (tabData.Rectangle.Contains(PointToClient(MousePosition)))
                    {
                        SelectedIndex = GetIndex(tabData);
                        break;
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

        #region Graphics

        private GraphicsPath RR(Rectangle r, int radius, bool rounded)
        {
            GraphicsPath path = new();

            if (rounded)
            {
                // Create points for the path based on the provided Rectangle and radius
                int diameter = 2 * radius;
                Rectangle arcRect = new Rectangle(r.X, r.Y, diameter, diameter);

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
                path.AddPolygon(new[]
                {
            new Point(r.X, r.Y),
            new Point(r.Right, r.Y),
            new Point(r.Right, r.Bottom),
            new Point(r.X, r.Bottom),
        });
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
                SizeF betaSize = Program.Lang.Beta.ToUpper().Measure(Font) + new SizeF(2, 3);
                Rectangle betaRect = new(rect.X + rect.Width - (int)betaSize.Width - 5, rect.Y + (int)((rect.Height - betaSize.Height) / 2), (int)betaSize.Width, (int)betaSize.Height);
                G.FillRoundedRect(scheme_secondary.Brushes.Back_Checked, betaRect);
                G.DrawRoundedRect_LikeW11(scheme_secondary.Pens.Line_Checked, betaRect);
                using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                {
                    G.DrawString(Program.Lang.Beta.ToUpper(), Font, scheme_secondary.Brushes.ForeColor_Accent, new Rectangle(betaRect.X, betaRect.Y + 1, betaRect.Width, betaRect.Height), sf);
                }
            }

            if (_tabControl != null)
            {
                List<TabData> tabsToDraw = new(TabDataList);

                foreach (TabData tabData in tabsToDraw)
                {
                    DrawTab(G, tabData);
                }

                if (isMovingTab)
                {
                    if (tabsToDraw != null && moveFrom != -1 && moveFrom <= tabsToDraw.Count - 1 && tabsToDraw[moveFrom] != null)
                    {
                        DrawTab(G, tabsToDraw[moveFrom], true);
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
                    using (SolidBrush br = new(Color.FromArgb(128, scheme_secondary.Colors.Back_Checked)))
                    using (Pen P = new(Color.FromArgb(128, scheme_secondary.Colors.Line_Checked)))
                    {
                        G.FillPath(br, path);
                        G.DrawPath(P, path);
                    }
                }
                else
                {
                    if (hoveredRectangle == rect)
                    {
                        G.FillPath(scheme.Brushes.Back_Checked, path);
                        G.FillPath(Noise, path);
                        G.DrawPath(scheme.Pens.Line_Checked_Hover, path);

                        if (overCloseButton)
                        {
                            using (LinearGradientBrush lgb0 = new(closeRectangle(rect), scheme_secondary.Colors.Line_Checked_Hover, scheme_secondary.Colors.Back_Checked_Hover, LinearGradientMode.Vertical))
                            using (LinearGradientBrush lgb1 = new(closeRectangle(rect), scheme_secondary.Colors.Line_Checked, scheme_secondary.Colors.Line_Checked_Hover, LinearGradientMode.Vertical))
                            using (Pen P = new(lgb1))
                            {
                                G.FillRoundedRect(lgb0, closeRectangle(rect));
                                G.DrawRoundedRect_LikeW11(P, closeRectangle(rect));
                            }
                        }
                    }
                    else if (tabData.Selected)
                    {
                        using (SolidBrush br = new(scheme.Colors.Back_Hover(parentLevel)))
                        using (Pen P = new(scheme.Colors.Line_Hover(parentLevel)))
                        {
                            G.FillPath(br, path);

                            if (OS.WVista || OS.W7 || OS.W8x)
                            {
                                using (Pen Px = new(Color.FromArgb(OS.W8x ? 50 : 128, 0, 0, 0))) { G.DrawPath(Px, path); }
                            }
                            else
                            {
                                G.DrawPath(P, path);
                            }
                        }
                    }
                    else
                    {
                        if (Parent != null && Parent.FindForm() != null)
                        {
                            using (SolidBrush br = new(Parent.FindForm().BackColor))
                            {
                                G.FillPath(br, path);
                            }
                        }
                        else
                        {
                            using (SolidBrush br = new(scheme.Colors.Back(parentLevel)))
                            using (Pen P = new(scheme.Colors.Line(parentLevel)))
                            {
                                G.FillPath(br, path);
                                G.DrawPath(P, path);
                            }
                        }
                    }
                }
            }

            using (SolidBrush br = new(ForeColor))
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            using (StringFormat sf_close = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

                Rectangle closeRect = closeRectangle(rect);
                closeRect.X++;
                closeRect.Y++;

                G.DrawString("✕", Fonts.ConsoleMedium, br, closeRect, sf_close);

                if (icon != null) G.DrawImage(icon, iconRectangle(rect));

                G.DrawString(tabData.Text, Font, br, titleRectangle(rect), sf);
            }
        }
    }
}