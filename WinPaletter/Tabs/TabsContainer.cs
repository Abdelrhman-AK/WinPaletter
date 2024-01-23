﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Retro;
using static WinPaletter.UI.Style.Config;

namespace WinPaletter.Tabs
{
    public class TabsContainer : Control
    {
        public TabsContainer()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            AllowDrop = true;

            InitializeContextMenu();
        }

        #region Variables

        private List<TabData> collection = new();
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
                    if (collection[i]?.Rectangle == rectangle)
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
                    collection.Add(CreateTabDataTuple(page, i));

                    i++;
                }
            }
        }

        private void RemoveTab(TabData tabData, bool animate = true)
        {
            bool canAnimate = IndexFromRectangle(tabData.Rectangle) == SelectedIndex && animate;

            if (!DesignMode && canAnimate) Program.Animator.HideSync(TabControl);

            int SI = SelectedIndex;

            if (tabData.Form != null)
            {
                tabData.Form?.Close();

                // Check if the form is successfully closed
                if (tabData.Form != null && !tabData.Form.IsDisposed)
                {
                    // Form is not closed, return without continuing
                    if (!DesignMode && canAnimate) Program.Animator.ShowSync(TabControl);
                    return;
                }
            }

            //if (!canAnimate) AfterRemovingTab(tabData, canAnimate, SI);
            //else
            //{
            //    FluentTransitions.Transition.With(tabData, nameof(tabData.TabTop), Height)
            //    .HookOnCompletion(() => { })
            //    .CriticalDamp(TimeSpan.FromMilliseconds(animate ? Program.AnimationDuration_Quick : 1));
            //}

            collection.Remove(tabData);

            UpdateTabPositions(collection);

            forceChangeSelectedIndex = true;
            SelectedIndex = SI;

            _tabControl.TabPages.Remove(tabData.TabPage);

            tabData.Dispose();

            Refresh();

            if (!DesignMode && animate) Program.Animator.ShowSync(TabControl);
        }

        private void SwapTabs(int from, int to)
        {
            if (collection != null)
            {
                TabData itemFrom = collection[from];
                TabData itemTo = collection[to];

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
                TabData itemFrom = collection.ElementAt(from);

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
                TabData itemFrom = collection[from];

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

        private Rectangle GetTabRectangleAtMousePosition(MouseEventArgs e)
        {
            List<TabData> tabDatas = new(collection);
            foreach (TabData tabData in tabDatas)
            {
                if (tabData.Rectangle.Contains(e.Location))
                {
                    return tabData.Rectangle;
                }
            }

            // Return an empty rectangle if no tabData is found at the current mouse position
            return Rectangle.Empty;
        }

        private TabData CreateTabDataTuple(TabPage page, int index)
        {
            Rectangle tabRectangle = new(index * tabWidth + paddingBetweenTabs, upperTabPadding, tabWidth - paddingBetweenTabs, tabHeight);
            return new TabData(this, page, tabRectangle);
        }

        private void UpdateTabPositions(List<TabData> collection)
        {
            int collectionCount = collection.Count;

            if (collectionCount == 0)
            {
                // Handle the case where the collection is empty
                return;
            }

            if (_selectedIndex >= 0 && _selectedIndex < collectionCount)
            {
                for (int i = 0; i < collectionCount; i++)
                {
                    collection[i] = CreateTabDataTuple(collection[i].TabPage, i);
                    collection[i].Selected = collection[i] == collection[_selectedIndex];
                }
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
                    SelectedTab = collection[_selectedIndex].TabPage;

                    List<TabData> tabDatas = new(collection);
                    foreach (TabData t in tabDatas)
                    {
                        t.Selected = collection[_selectedIndex] == t;
                    }
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
            RemoveTab(tabData);
        }

        private void HandleTabControlLeftButtonClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<TabData> tabDatas = new(collection);
                foreach (TabData tabData in tabDatas)
                {
                    if (IsMouseOverTab(tabData) && !IsMouseOverCloseButton(tabData, e))
                    {
                        moveFrom = IndexFromRectangle(tabData.Rectangle);
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

        private void HandleTabLeftButtonClick(TabData tabData)
        {
            if (tabData.TabPage != _tabControl.SelectedTab)
            {
                SelectedIndex = IndexFromRectangle(tabData.Rectangle);
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
                // Set properties for tabData movement with index information
                SetTabMoveProperties(IndexFromRectangle(tabRectangle), true, false, false, false);
            }
            else
            {
                // Check if the cursor is to the right of the last tabData
                if (e.X > collection.Last().Rectangle.Right)
                {
                    SetTabMoveProperties(-1, true, false, true, false);
                }
                // Check if the cursor is to the left of the first tabData
                else if (e.X < collection.First().Rectangle.Left)
                {
                    SetTabMoveProperties(-1, true, true, false, false);
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
            List<TabData> tabDatas = new(collection);
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

        private void CloseAllTabsButThis()
        {
            if (collection.Count > 1)
            {
                List<TabData> tabDatas = new(collection);
                foreach (TabData tabData in tabDatas)
                {
                    if (tabData.TabPage != contextItemDropped.TabPage)
                    {
                        RemoveTab(tabData, false);
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

        private void DetachTab(TabData tabData)
        {
            if (tabData.Form != null) DetachForm(tabData.Form);

            RemoveTab(tabData, false);

            if (collection.Count == 0) TabControl.FindForm().Visible = false;
        }

        private void DetachAllTabs()
        {
            int count = collection.Count;
            for (int i = 0; i <= count - 1; i++)
            {
                if (collection[0].Form != null) DetachTab(collection[0]);
            }
        }

        private void DetachAllTabsButThis()
        {
            if (collection.Count > 1)
            {
                DetachAllTabs();
                if (contextItemDropped.Form != null) AddFormIntoTab(contextItemDropped.Form);
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
                ? collection.ElementAt(_selectedIndex).TabPage
                : null;
            set
            {
                if (_tabControl != null)
                {
                    _tabControl.SelectedTab = value;
                    int index = collection.FindIndex(t => t.TabPage == value);
                    if (index > -1) SelectedIndex = index;
                }
            }
        }

        #endregion

        #region Events/Overrides

        public delegate void FormShownDelegate(object sender, TabDataEventArgs e);
        public event FormShownDelegate FormShown;

        public delegate void FormClosedDelegate(object sender, TabDataEventArgs e);
        public event FormClosedDelegate FormClosed;

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

        public virtual void OnFormClosed(object sender, TabDataEventArgs e)
        {
            FormClosed?.Invoke(sender, e);

        }

        private void _tabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage)
            {
                TabPage page = e.Control as TabPage;

                collection.Add(CreateTabDataTuple(page, collection.Count));

                collection[collection.Count - 1].TabTop = Height;

                SelectedIndex = collection.Count - 1;

                //FluentTransitions.Transition.With(collection[collection.Count - 1], nameof(TabData.TabTop), upperTabPadding)
                //    .HookOnCompletion(() =>
                //    {
                //        UpdateTabPositions(collection);
                //        Refresh();
                //    })
                //    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                UpdateTabPositions(collection);
                Refresh();
            }
        }

        private void _tabControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (_tabControl != null && e.Control is TabPage && collection.Any(key => key.TabPage == e.Control))
            {
                TabPage page = e.Control as TabPage;

                RemoveTab(collection.Where(t => t.TabPage == page).FirstOrDefault());
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
            List<TabData> tabDatas = new(collection);
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

                List<TabData> tabDatas = new(collection);
                foreach (TabData tabData in tabDatas)
                {
                    if (tabData.Rectangle.Contains(PointToClient(MousePosition)))
                    {
                        SelectedIndex = IndexFromRectangle(tabData.Rectangle);
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

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            if (_tabControl != null)
            {
                List<TabData> tabsToDraw = new(collection);

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
                        G.DrawPath(scheme.Pens.Line_Checked, path);

                        if (overCloseButton)
                        {
                            G.FillRoundedRect(scheme_secondary.Brushes.Back_Checked_Hover, closeRectangle(rect), 3);
                        }
                    }
                    else if (tabData.Selected)
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
            using (StringFormat sf_close = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

                Rectangle closeRect = closeRectangle(rect);
                closeRect.X++;

                G.DrawString("x", Fonts.ConsoleMedium, br, closeRect, sf_close);

                if (icon != null) G.DrawImage(icon, iconRectangle(rect));

                G.DrawString(tabData.Text, Font, br, titleRectangle(rect), sf);
            }
        }
    }
}