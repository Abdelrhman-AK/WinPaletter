using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using static WinPaletter.UI.Style.Config;

namespace WinPaletter.UI.Controllers
{
    public class TabsContainer : Control
    {
        public TabsContainer()
        {
            InitializeTabControl();

            InitializeContextMenu();
        }

        private void InitializeTabControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            AllowDrop = true;
        }

        private void InitializeContextMenu()
        {
            contextMenu.ItemHeight = 20;

            ToolStripMenuItem closeButton = new("Close") { Image = Assets.Tabs.ContextBox_Close };
            ToolStripMenuItem closeAllButThis = new("Close all but this") { Image = Assets.Tabs.ContextBox_CloseAllButThis };
            ToolStripMenuItem closeAllToTheRight = new("Close all to the right") { Image = Assets.Tabs.ContextBox_CloseRight };
            ToolStripMenuItem closeAllToTheLeft = new("Close all to the left") { Image = Assets.Tabs.ContextBox_CloseLeft };
            ToolStripMenuItem closeAll = new("Close all") { Image = Assets.Tabs.ContextBox_CloseAll };
            ToolStripSeparator toolStripSeparator = new();
            ToolStripMenuItem detach = new("Detach") { Image = Assets.Tabs.ContextBox_Detach };
            ToolStripMenuItem detachAll = new("Detach all") { Image = Assets.Tabs.ContextBox_DetachAll };
            ToolStripMenuItem detachAllButThis = new("Detach all but this") { Image = Assets.Tabs.ContextBox_DetachAllButThis };

            closeButton.Click += (s, e) => RemoveTab(contextItemDropped);
            closeAllButThis.Click += (s, e) => CloseAllTabsButThis();
            closeAllToTheRight.Click += (s, e) => CloseAllTabsToTheRight();
            closeAllToTheLeft.Click += (s, e) => CloseAllTabsToTheLeft();
            closeAll.Click += (s, e) => CloseAllTabs();
            detach.Click += (s, e) => DetachTab(contextItemDropped);
            detachAll.Click += (s, e) => DetachAllTabs();
            detachAllButThis.Click += (s, e) => DetachAllTabsButThis();

            contextMenu.Items.AddRange(new ToolStripItem[] { closeButton, closeAllToTheRight, closeAllToTheLeft, closeAll, closeAllButThis, toolStripSeparator, detach, detachAll, detachAllButThis });
        }

        private void CloseAllTabsButThis()
        {
            if (collection.Count > 1)
            {
                foreach (Tuple<TabPage, Rectangle, Bitmap> item in collection.ToArray().Clone() as Array)
                {
                    if (item.Item1 != contextItemDropped.Item1)
                    {
                        RemoveTab(item, false);
                    }
                }
            }
        }

        private void CloseAllTabsToTheRight()
        {
            int index = collection.IndexOf(contextItemDropped);

            for (int i = index + 1; i < collection.Count; i += 0)
            {
                RemoveTab(collection[i], false);
            }
        }

        private void CloseAllTabsToTheLeft()
        {
            int index = collection.IndexOf(contextItemDropped);

            for (int i = 0; i < index; i++)
            {
                RemoveTab(collection[0], false);
            }
        }

        private void CloseAllTabs()
        {
            int count = collection.Count;
            for (int i = 0; i < count; i++)
            {
                RemoveTab(collection[0], false);
            }
        }

        private void DetachTab(Tuple<TabPage, Rectangle, Bitmap> tab)
        {
            if (tab.Item1.Controls.OfType<Form>().Count() > 0)
            {
                Form form = tab.Item1.Controls.OfType<Form>().FirstOrDefault();
                DetachForm(form);
            }

            RemoveTab(tab, false);

            if (collection.Count == 0)
            {
                TabControl.FindForm().Visible = false;
            }
        }

        private void DetachAllTabs()
        {
            int count = collection.Count;
            for (int i = 0; i <= count - 1; i++)
            {
                if (collection[0].Item1.Controls.OfType<Form>().Count() > 0)
                    DetachTab(collection[0]);
            }
        }

        private void DetachAllTabsButThis()
        {
            if (collection.Count > 1)
            {
                Form form = contextItemDropped.Item1.Controls.OfType<Form>().FirstOrDefault();
                DetachAllTabs();
                if (form != null)
                {
                    AddFormIntoTab(form);
                }
            }
        }

        private void DetachForm(Form form)
        {
            form.Visible = false;
            if (form.Parent != null && form.Parent is TabPage) form.Parent.Controls.Remove(form);
            form.TopLevel = true;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.WindowState = FormWindowState.Normal;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Location = new(Screen.PrimaryScreen.WorkingArea.X + (Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2,
                Screen.PrimaryScreen.WorkingArea.Y + (Screen.PrimaryScreen.WorkingArea.Height - form.Height) / 2);

            if (form is AspectsTemplate)
            {
                (form as AspectsTemplate).titlebarExtender1.DropDWMEffect = true;
            }
            else if (form is Form && form.Controls.OfType<UI.WP.TitlebarExtender>().Count() > 0)
            {
                form.Controls.OfType<UI.WP.TitlebarExtender>().FirstOrDefault().DropDWMEffect = true;
            }

            form.Show();
            ApplyStyle(form);
            form.BringToFront();

        }

        #region Variables

        private List<Tuple<TabPage, Rectangle, Bitmap>> collection = new();
        private Rectangle hoveredRectangle;
        private Rectangle selectedRectangle;
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
        private Tuple<TabPage, Rectangle, Bitmap> contextItemDropped;

        Scheme scheme => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
        Scheme scheme_secondary => Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;

        #endregion

        #region Methods

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

            TP.Text = form.Text;
            TP.Controls.Add(form);

            if (!DesignMode) Program.Animator.HideSync(TabControl);

            form.Show();

            forceChangeSelectedIndex = true;
            TabControl.TabPages.Add(TP);
            SelectedTab = TP;

            TabControl.FindForm().Visible = true;

            form.FormClosed += TabForm_Closed;

            if (form is AspectsTemplate)
            {
                (form as AspectsTemplate).titlebarExtender1.DropDWMEffect = false;
            }
            else if (form is Form && form.Controls.OfType<UI.WP.TitlebarExtender>().Count() > 0)
            {
                form.Controls.OfType<UI.WP.TitlebarExtender>().FirstOrDefault().DropDWMEffect = false;
            }

            if (!DesignMode) Program.Animator.ShowSync(TabControl);

            Cursor = Cursors.Default;
        }

        private int tabWidth => Math.Min(MaxWidth, Width / _tabControl.TabPages.Count);
        private int tabHeight => Height - upperTabPadding;

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

        private int IndexFromRectangle(Rectangle rectangle)
        {
            if (collection != null && collection.Count > 0)
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    if (collection[i]?.Item2 == rectangle)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private void UpdateTabs()
        {
            collection.Clear();

            if (_tabControl != null)
            {
                int i = 0;

                foreach (TabPage page in _tabControl.TabPages)
                {
                    Bitmap ico = GetTabPageIcon(page);

                    collection.Add(CreateTabDataTuple(page, i));

                    i++;
                }
            }
        }

        private void RemoveTab(Tuple<TabPage, Rectangle, Bitmap> item, bool animate = true)
        {
            bool canAnimate = IndexFromRectangle(item.Item2) == SelectedIndex && animate;

            if (!DesignMode && canAnimate) Program.Animator.HideSync(TabControl);

            int SI = SelectedIndex;

            if (item.Item1.Controls.OfType<Form>().Any())
            {
                Form form = item.Item1.Controls.OfType<Form>().FirstOrDefault();
                if (form != null)
                {
                    form.Close();

                    // Check if the form is successfully closed
                    if (!form.IsDisposed)
                    {
                        // Form is not closed, return without continuing
                        if (!DesignMode && canAnimate) Program.Animator.ShowSync(TabControl);
                        return;
                    }
                }
            }

            collection.Remove(item);

            UpdateTabPositions(collection);

            forceChangeSelectedIndex = true;
            SelectedIndex = SI;

            _tabControl.TabPages.Remove(item.Item1);

            Refresh();

            if (!DesignMode && canAnimate) Program.Animator.ShowSync(TabControl);
        }

        private void SwapTabs(int from, int to)
        {
            if (collection != null)
            {
                Tuple<TabPage, Rectangle, Bitmap> itemFrom = collection[from];
                Tuple<TabPage, Rectangle, Bitmap> itemTo = collection[to];

                collection[from] = itemTo;
                collection[to] = itemFrom;

                UpdateTabPositions(collection);

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
            if (collection != null)
            {
                Tuple<TabPage, Rectangle, Bitmap> itemFrom = collection.ElementAt(from);

                collection.RemoveAt(from);
                collection.Add(itemFrom);

                UpdateTabPositions(collection);

                // to avoid bug of non-selection
                forceChangeSelectedIndex = true;
                SelectedIndex = collection.Count - 1;

                isMovingTab = false;

                ResetModifiersToNull();

                Refresh();
            }
        }

        private void MoveToFirst(int from)
        {
            if (collection != null)
            {
                Tuple<TabPage, Rectangle, Bitmap> itemFrom = collection[from];

                collection.RemoveAt(from);
                collection.Insert(0, itemFrom);

                UpdateTabPositions(collection);

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

        private Bitmap GetTabPageIcon(TabPage page)
        {
            if (page.Controls.OfType<Form>().Any())
            {
                return new Icon(page.Controls.OfType<Form>().FirstOrDefault()?.Icon, 16, 16).ToBitmap();
            }
            else if (_tabControl.ImageList != null && _tabControl.ImageList.Images.Count >= page.ImageIndex)
            {
                return new Bitmap(_tabControl.ImageList.Images[page.ImageIndex], 16, 16);
            }

            return null;
        }

        private Rectangle GetTabRectangleAtMousePosition(MouseEventArgs e)
        {
            foreach (Tuple<TabPage, Rectangle, Bitmap> item in collection)
            {
                if (item.Item2.Contains(e.Location))
                {
                    return item.Item2;
                }
            }

            // Return an empty rectangle if no tab is found at the current mouse position
            return Rectangle.Empty;
        }

        private Tuple<TabPage, Rectangle, Bitmap> CreateTabDataTuple(TabPage page, int index)
        {
            Rectangle tabRectangle = new(index * tabWidth + paddingBetweenTabs, upperTabPadding, tabWidth - paddingBetweenTabs, tabHeight);
            return new Tuple<TabPage, Rectangle, Bitmap>(page, tabRectangle, GetTabPageIcon(page));
        }

        private void UpdateTabPositions(List<Tuple<TabPage, Rectangle, Bitmap>> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                collection[i] = CreateTabDataTuple(collection[i].Item1, i);
            }
        }

        private int AdjustSelectedIndex(int value)
        {
            if (collection != null && collection.Count > 0)
            {
                if (value > collection.Count - 1)
                    return collection.Count - 1;
            }

            return value;
        }

        private void UpdateSelectedTab()
        {
            if (collection != null && collection.Count > 0)
            {
                if (_selectedIndex > -1)
                {
                    selectedRectangle = collection[_selectedIndex].Item2;
                    SelectedTab = collection[_selectedIndex].Item1;

                    UpdateTitlebarExtenderTabLocation();
                }
                else
                {
                    selectedRectangle = Rectangle.Empty;
                    SelectedTab = null;
                }
            }
        }

        private void UpdateTitlebarExtenderTabLocation()
        {
            if (collection[_selectedIndex].Item1.Controls.OfType<Form>().Any())
            {
                Form form = collection[_selectedIndex].Item1.Controls.OfType<Form>().FirstOrDefault();

                if (form.Controls.OfType<UI.WP.TitlebarExtender>().Any())
                {
                    UI.WP.TitlebarExtender titlebar = form.Controls.OfType<UI.WP.TitlebarExtender>().FirstOrDefault();
                    titlebar.TabLocation = new(selectedRectangle.X + Left, selectedRectangle.Y, selectedRectangle.Width, selectedRectangle.Height);
                }
            }
        }

        private void AdjustSelectedRectangle()
        {
            if (_selectedIndex > -1)
            {
                selectedRectangle.Width = tabWidth - paddingBetweenTabs;
                selectedRectangle = collection[_selectedIndex].Item2;
            }
        }

        private bool IsMouseOverTab(Tuple<TabPage, Rectangle, Bitmap> item)
        {
            return item.Item2.Contains(PointToClient(MousePosition));
        }

        private void ProcessTabMouseActions(Tuple<TabPage, Rectangle, Bitmap> item, MouseEventArgs e)
        {
            if (overCloseButton)
            {
                HandleCloseButtonClick(item);
            }
            else if (e.Button != MouseButtons.Right)
            {
                HandleTabLeftButtonClick(item);
            }
            else
            {
                HandleTabRightButtonClick(item);
            }
        }

        private void HandleCloseButtonClick(Tuple<TabPage, Rectangle, Bitmap> item)
        {
            RemoveTab(item);
        }

        private void HandleTabControlLeftButtonClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Tuple<TabPage, Rectangle, Bitmap> item in collection)
                {
                    if (IsMouseOverTab(item) && !IsMouseOverCloseButton(item, e))
                    {
                        moveFrom = IndexFromRectangle(item.Item2);
                        tabOldPoint = MousePosition - (Size)item.Item2.Location;

                        break;
                    }
                    else
                    {
                        locationOldPoint = MousePosition - (Size)FindForm()?.Location;
                    }
                }
            }
        }

        private void HandleTabLeftButtonClick(Tuple<TabPage, Rectangle, Bitmap> item)
        {
            if (item.Item1 != _tabControl.SelectedTab)
            {
                SelectedIndex = IndexFromRectangle(item.Item2);
            }
        }

        private void HandleTabRightButtonClick(Tuple<TabPage, Rectangle, Bitmap> item)
        {
            contextItemDropped = item;
            contextMenu.Show(this, item.Item2.Location + new Size(0, item.Item2.Height + 3));
        }

        private bool IsMouseOverCloseButton(Tuple<TabPage, Rectangle, Bitmap> item, MouseEventArgs e)
        {
            return closeRectangle(item.Item2).Contains(e.Location);
        }

        private void HandleTabControlMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && moveFrom != -1)
            {
                HandleTabMove(e);
            }
            else if (e.Button == MouseButtons.Left && FindForm() != null)
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
            Rectangle tabRectangle = GetTabRectangleAtMousePosition(e);

            if (!tabRectangle.IsEmpty)
            {
                // Set properties for tab movement with index information
                SetTabMoveProperties(IndexFromRectangle(tabRectangle), true, false, false, false);
            }
            else
            {
                // Check if the cursor is to the right of the last tab
                if (e.X > collection.Last().Item2.Right)
                {
                    SetTabMoveProperties(-1, true, false, true, false);
                }
                // Check if the cursor is to the left of the first tab
                else if (e.X < collection.First().Item2.Left)
                {
                    SetTabMoveProperties(-1, true, true, false, false);
                }
            }

            // If tab movement is enabled, update the tab's position and refresh the UI
            if (isMovingTab)
            {
                // Calculate the new position of the tab based on mouse movement
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
            foreach (Tuple<TabPage, Rectangle, Bitmap> item in collection)
            {
                if (item.Item2.Contains(e.Location))
                {
                    SetHoverProperties(item.Item2, e);
                    break;
                }
                else
                {
                    ClearHoverProperties();
                }
            }
        }

        private void SetTabMoveProperties(int moveToIndex, bool moveTab, bool moveToFirst, bool moveToLast, bool moveBetweenTabs)
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

        #endregion

        #region Properties

        private TabControl _tabControl;
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


        private int _selectedIndex;
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

        public TabPage SelectedTab
        {
            get => _selectedIndex >= 0 && _selectedIndex < collection?.Count
                ? collection.ElementAt(_selectedIndex).Item1
                : null;
            set
            {
                if (_tabControl != null)
                {
                    _tabControl.SelectedTab = value;
                    int index = collection.FindIndex(t => t.Item1 == value);
                    if (index > -1) SelectedIndex = index;
                }
            }
        }

        #endregion

        #region Events/Overrides

        public void TabForm_Closed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).Parent?.Dispose();
            ((Form)sender).FormClosed -= TabForm_Closed;
        }

        private void _tabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage)
            {
                TabPage page = e.Control as TabPage;

                collection.Add(CreateTabDataTuple(page, collection.Count));

                UpdateTabPositions(collection);

                SelectedIndex = collection.Count - 1;

                Refresh();
            }
        }

        private void _tabControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage && collection.Any(key => key.Item1 == e.Control))
            {
                TabPage page = e.Control as TabPage;
                RemoveTab(collection.Where(t => t.Item1 == page).FirstOrDefault());
            }
        }

        private void _tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTab = _tabControl.SelectedTab;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (collection != null && collection.Count > 0)
            {
                UpdateTabPositions(collection);

                AdjustSelectedRectangle();

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
                HandleTabControlLeftButtonClick(e);
            }
            else
            {
                locationOldPoint = MousePosition - (Size)FindForm()?.Location;
            }

            base.OnMouseDown(e);
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
            foreach (Tuple<TabPage, Rectangle, Bitmap> item in collection)
            {
                if (IsMouseOverTab(item))
                {
                    ProcessTabMouseActions(item, e);
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
            else if (e.Data.GetDataPresent(typeof(UI.Controllers.ColorItem)))
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

                foreach (Tuple<TabPage, Rectangle, Bitmap> item in collection)
                {
                    if (item.Item2.Contains(PointToClient(MousePosition)))
                    {
                        SelectedIndex = IndexFromRectangle(item.Item2);
                        break;
                    }
                }
            }

            base.OnDragOver(e);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            if (_tabControl != null)
            {
                foreach (Tuple<TabPage, Rectangle, Bitmap> item in collection)
                {
                    DrawTab(G, item);
                }

                if (isMovingTab)
                {
                    if (collection != null && moveFrom != -1 && moveFrom <= collection.Count - 1 && collection[moveFrom] != null)
                    {
                        DrawTab(G, collection[moveFrom], true);
                    }
                }
            }
        }

        private void DrawTab(Graphics G, Tuple<TabPage, Rectangle, Bitmap> item, bool isMoving = false)
        {
            Rectangle rect = !isMoving ? item.Item2 : new Rectangle(tabNewPoint.X, item.Item2.Y, item.Item2.Width, item.Item2.Height);

            Bitmap icon = item.Item3;

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
                        G.DrawPath(scheme.Pens.Line_Checked, path);

                        if (overCloseButton)
                        {
                            G.FillRoundedRect(scheme_secondary.Brushes.Back_Checked_Hover, closeRectangle(rect), 3);
                        }
                    }
                    else if (selectedRectangle == rect)
                    {
                        G.FillPath(scheme.Brushes.Back_Hover, path);
                        G.DrawPath(scheme.Pens.Line_Hover, path);
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
                            G.FillPath(scheme.Brushes.Back, path);
                            G.DrawPath(scheme.Pens.Line, path);
                        }
                    }
                }
            }

            using (SolidBrush br = new(ForeColor))
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            {
                Rectangle closeRect = closeRectangle(rect);
                closeRect.X++;

                G.DrawString("x", Fonts.ConsoleMedium, br, closeRect, ContentAlignment.MiddleCenter.ToStringFormat());

                if (icon != null) G.DrawImage(icon, iconRectangle(rect));

                G.DrawString(item.Item1.Text, Font, br, titleRectangle(rect), sf);
            }
        }
    }
}