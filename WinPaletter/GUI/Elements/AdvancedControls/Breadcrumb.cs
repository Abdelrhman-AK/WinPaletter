using FluentTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using WinPaletter.UI.WP;
using static Devcorp.Controls.VisualStyles.VisualStyleElement.ComboBox;

namespace WinPaletter.UI.WP
{
    public class BreadcrumbDesigner : ParentControlDesigner
    {
        private Breadcrumb _breadcrumb;
        private DesignerActionListCollection _actionLists;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            _breadcrumb = component as Breadcrumb;

            // Enable design mode for RightPanel
            if (_breadcrumb != null)
            {
                EnableDesignMode(_breadcrumb.RightPanel, "RightPanel");
            }
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.Add(new BreadcrumbActionList(_breadcrumb));
                }
                return _actionLists;
            }
        }
    }

    public class BreadcrumbActionList : DesignerActionList
    {
        private readonly Breadcrumb _breadcrumb;
        private readonly DesignerActionUIService _service;

        public BreadcrumbActionList(Breadcrumb breadcrumb) : base(breadcrumb)
        {
            _breadcrumb = breadcrumb;
            _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        public int RightPanelWidth
        {
            get => _breadcrumb.RightPanelWidth;
            set
            {
                // Use property descriptor to set value
                PropertyDescriptor prop = TypeDescriptor.GetProperties(_breadcrumb)["RightPanelWidth"];
                if (prop != null && prop.PropertyType == typeof(int))
                {
                    prop.SetValue(_breadcrumb, value);
                    _breadcrumb.PerformLayout();
                    _service?.Refresh(_breadcrumb);
                }
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items =
            [
                new DesignerActionHeaderItem("Layout"),
            new DesignerActionPropertyItem(nameof(RightPanelWidth),
                "Right Panel Width",
                "Layout",
                "Adjust the width of the right panel"),
        ];
            return items;
        }
    }

    /// <summary>
    /// A custom breadcrumb control for navigation and progress display.
    /// </summary>
    [Designer(typeof(BreadcrumbDesigner))]
    public partial class Breadcrumb : UserControl
    {
        #region Fields

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Layout")]
        [Description("Right panel for additional controls")]
        public Panel RightPanel => rightPanel;

        private SmoothFlowLayoutPanel breadcrumbPanel;
        private TextBox pathTextBox;
        private Button overflowButton;

        private Panel rightPanel;
        private Splitter rightSplitter;
        private int rightPanelWidth = 220;
        private bool _isDraggingSplitter = false;
        private int _dragStartX;
        private int _dragStartWidth;

        private int paddingStart = 28;
        private Bitmap icon_dark = Properties.Resources.Glyph_Browse;
        private Bitmap icon_light = Properties.Resources.Glyph_Browse.Invert();
        private Bitmap icon_wait_dark = Properties.Resources.Glyph_Wait;
        private Bitmap icon_wait_light = Properties.Resources.Glyph_Wait.Invert();

        private Timer _marqueeTimer;
        private Timer _progressTimer;
        private float _hoverOffset = 0;
        private float _marqueeOffset = 0;

        private bool hasPaths = false;

        private TreeView boundTreeView;
        private float progressValue;
        private float progressMinimum = 0;
        private float progressMaximum = 100;
        private float _animatedValue = 0;
        private int _alpha = 255;
        private int _hoverAlpha = 0;
        private int parentLevel = 0;
        private UI.WP.Button btn_Stop;

        public event Action StopRequested;
        #endregion

        #region Properties

        /// <summary>
        /// Determines if animations are allowed.
        /// </summary>
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        /// <summary>
        /// The TreeView bound to this breadcrumb control.
        /// </summary>
        public TreeView BoundTreeView
        {
            get => boundTreeView;
            set
            {
                hasPaths = false;

                if (boundTreeView != null) boundTreeView.AfterSelect -= TreeView_AfterSelect;

                boundTreeView = value;

                if (boundTreeView != null)
                {
                    boundTreeView.AfterSelect += TreeView_AfterSelect;

                    // Auto-select root if nothing is selected
                    if (boundTreeView.SelectedNode == null)
                    {
                        TreeNode root = boundTreeView.TopNode ?? boundTreeView.Nodes.Cast<TreeNode>().FirstOrDefault();
                        if (root != null)
                        {
                            boundTreeView.SelectedNode = root;
                            root.Expand();
                        }
                    }
                }

                UpdateBreadcrumb(boundTreeView?.SelectedNode);
            }
        }

        /// <summary>
        /// Minimum progress value.
        /// </summary>
        public float Minimum
        {
            get => progressMinimum;
            set
            {
                if (value > progressMaximum) value = progressMaximum;
                if (value != progressMaximum)
                {
                    progressMinimum = value;
                    if (progressValue < progressMinimum) progressValue = progressMinimum;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Maximum progress value.
        /// </summary>
        public float Maximum
        {
            get => progressMaximum;
            set
            {
                if (value < progressMinimum) value = progressMinimum;
                if (value != progressMinimum)
                {
                    progressMaximum = value;
                    if (progressValue > progressMaximum) progressValue = progressMaximum;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Current progress value.
        /// </summary>
        public float Value
        {
            get => progressValue;
            set
            {
                if (value != progressValue)
                {
                    progressValue = Math.Max(progressMinimum, Math.Min(progressMaximum, value));

                    if (CanAnimate)
                        Transition.With(this, nameof(Value_Animation), progressValue).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                    else
                        Value_Animation = progressValue;

                    if (value > progressMinimum && value < progressMaximum) btn_Stop.Visible = true;
                    else btn_Stop.Visible = false;

                    // Enable/disable hover timer based on value
                    if (_progressTimer != null && !DesignMode)
                    {
                        if (progressValue > progressMinimum && progressValue < progressMaximum)
                        {
                            if (!_progressTimer.Enabled) _progressTimer.Start();
                        }
                        else
                        {
                            if (_progressTimer.Enabled) _progressTimer.Stop();
                            _hoverOffset = 0; // reset hover when out of range
                        }
                    }
                }
            }
        }

        public bool IsMarquee => _isMarquee;
        private bool _isMarquee;

        /// <summary>
        /// Animated progress value for smooth transitions.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public float Value_Animation
        {
            get => _animatedValue;
            set
            {
                if (value != _animatedValue)
                {
                    _animatedValue = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Alpha transparency for progress bar rendering.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int Alpha
        {
            get => _alpha;
            set
            {
                if (value != _alpha)
                {
                    _alpha = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Alpha transparency for progress bar rendering.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int HoverAlpha
        {
            get => _hoverAlpha;
            set
            {
                if (value != _hoverAlpha)
                {
                    _hoverAlpha = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Layout")]
        [Description("Width of the right panel.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(220)]
        public int RightPanelWidth
        {
            get => rightPanelWidth;
            set
            {
                if (rightPanelWidth != value)
                {
                    rightPanelWidth = Math.Max(50, Math.Min(400, value));

                    if (rightPanel != null)
                    {
                        rightPanel.Width = rightPanelWidth;
                        PerformLayout();
                        Invalidate();
                    }
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Breadcrumb"/> class.
        /// </summary>
        public Breadcrumb()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;

            // Initialize RightPanel with proper docking
            rightPanel = new Panel
            {
                Width = rightPanelWidth,
                Dock = DockStyle.Right,  // Dock to right
                BackColor = Color.Transparent,
                Name = "RightPanel"
            };

            // Then initialize other controls
            InitializeControls();

            Resize += (s, e) => UpdateBreadcrumb(boundTreeView?.SelectedNode);
            SubscribeHoverEvents(this);
            InitProgressAnimation();
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes child controls for the breadcrumb.
        /// </summary>
        private void InitializeControls()
        {
            breadcrumbPanel = new()
            {
                Location = new Point(paddingStart, 0),
                AutoSize = true,
                WrapContents = false,
                Height = this.Height,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                BackColor = Color.Transparent
            };
            breadcrumbPanel.MouseDown += BreadcrumbPanel_MouseDown;

            rightSplitter = new Splitter
            {
                Width = 2,
                Dock = DockStyle.Right,  // Splitter docks right
                BackColor = Program.Style.Schemes.Main.Colors.Line(),
                Cursor = Cursors.VSplit,
            };
            rightSplitter.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    _isDraggingSplitter = true;
                    // Store initial panel width and mouse X in parent coordinates
                    _dragStartWidth = rightPanel.Width;
                    _dragStartX = rightSplitter.PointToScreen(e.Location).X;
                    rightSplitter.Capture = true;
                }
            };
            rightSplitter.MouseMove += (s, e) =>
            {
                if (_isDraggingSplitter && rightSplitter.Capture)
                {
                    // Current mouse X in screen coordinates
                    int mouseX = rightSplitter.PointToScreen(e.Location).X;

                    // Delta from starting point
                    int deltaX = mouseX - _dragStartX;

                    // For a right-docked panel, increasing mouse X should decrease width
                    int newWidth = Math.Max(24, _dragStartWidth - deltaX);

                    rightPanelWidth = newWidth;
                    rightPanel.Width = newWidth;

                    PerformLayout();
                    Invalidate();
                }
            };
            rightSplitter.MouseUp += (s, e) =>
            {
                _isDraggingSplitter = false;
                rightSplitter.Capture = false;
            };

            btn_Stop = new()
            {
                ImageGlyphEnabled = true,
                ImageGlyph = Properties.Resources.Glyph_Cancel,
                Flag = Button.Flags.CustomColorOnHover,
                CustomColor = Color.FromArgb(193, 18, 31),
                Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                Height = Height - 4,
                Width = 28,
                Visible = true,
            };
            btn_Stop.Click += (s, e) =>
            {
                StopRequested?.Invoke();
            };

            overflowButton = new()
            {
                Text = "...",
                AutoSize = true,
                Visible = false,
                Flag = Button.Flags.TintedOnHover,
                ImageGlyphEnabled = true,
                CustomColor = Program.Style.Schemes.Main.Colors.Accent,
                Width = 10
            };
            overflowButton.Click += (s, e) =>
            {
                overflowButton.Menu.Show(overflowButton, MousePosition);
                AnimateHover(false);
            };
            breadcrumbPanel.Controls.Add(overflowButton);

            pathTextBox = new UI.WP.TextBox
            {
                Visible = false,
            };

            pathTextBox.TB.AutoCompleteMode = AutoCompleteMode.Suggest;
            pathTextBox.TB.AutoCompleteSource = AutoCompleteSource.CustomSource;
            pathTextBox.TB.AcceptsReturn = true;
            pathTextBox.TB.KeyUp += TB_KeyUp;

            // 1. Breadcrumb panel
            Controls.Add(breadcrumbPanel);

            // 2. Splitter (dock right)
            rightSplitter.Dock = DockStyle.Right;
            Controls.Add(rightSplitter);

            // 3. Right panel (dock right)
            rightPanel.Dock = DockStyle.Right;
            Controls.Add(rightPanel);

            Controls.Add(pathTextBox);
            Controls.Add(btn_Stop);

            // Bring btn_Stop to front so it's visible
            btn_Stop.BringToFront();
        }

        #endregion

        #region Breadcrumb Update

        /// <summary>
        /// Updates the breadcrumb buttons for the specified <see cref="TreeNode"/>.
        /// </summary>
        /// <param name="node">The current node.</param>
        public void UpdateBreadcrumb(TreeNode node)
        {
            breadcrumbPanel.SuspendLayout();
            breadcrumbPanel.Controls.Clear();
            breadcrumbPanel.Controls.Add(overflowButton);
            overflowButton.Visible = false;
            overflowButton.Menu.Items.Clear();

            breadcrumbPanel.Location = new Point(paddingStart, 0);
            breadcrumbPanel.Height = this.Height;
            breadcrumbPanel.Width = Math.Max(0, this.Width - breadcrumbPanel.Left - 4);

            if (node == null)
            {
                breadcrumbPanel.ResumeLayout();
                return;
            }

            List<Button> buttons = [];
            string[] pathSegments = node.FullPath.Split('\\');
            hasPaths = pathSegments.Length > 0;

            TreeNode current = boundTreeView?.TopNode ?? boundTreeView?.Nodes.Cast<TreeNode>().FirstOrDefault();

            for (int i = 0; i < pathSegments.Length; i++)
            {
                string segment = pathSegments[i];
                TreeNode matching = current?.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == segment) ?? current;

                Button btn = new()
                {
                    Text = segment,
                    AutoSize = true,
                    Tag = matching,
                    Flag = Button.Flags.TintedOnHover,
                    ImageGlyphEnabled = true,
                    CustomColor = Program.Style.Schemes.Main.Colors.Accent,
                    Menu = new ContextMenuStrip()
                };
                if (matching != null && matching.Nodes.Count > 0)
                {
                    foreach (TreeNode child in matching.Nodes)
                    {
                        System.Windows.Forms.ToolStripMenuItem item = new(child.Text) { Tag = child, Image = Assets.GitHubMgr.folder_web_16 };
                        item.Click += (s, e) =>
                        {
                            AnimateHover(false);
                            TreeNode tn = ((System.Windows.Forms.ToolStripMenuItem)s).Tag as TreeNode;
                            if (boundTreeView != null) boundTreeView.SelectedNode = tn;
                        };
                        btn.Menu.Items.Add(item);
                    }
                }
                btn.Click += Btn_Click;

                int textWidth = (int)btn.Text.Measure(btn.Font).Width;
                btn.Width = textWidth + (btn.Menu.Items.Count > 0 ? 20 : 0);

                buttons.Add(btn);
                breadcrumbPanel.Controls.Add(btn);

                current = matching;
            }

            breadcrumbPanel.ResumeLayout();
            ApplyOverflow(buttons);
            Invalidate();
        }

        /// <summary>
        /// Applies overflow logic when breadcrumb buttons exceed available width.
        /// </summary>
        /// <param name="buttons">List of buttons in breadcrumb.</param>
        private void ApplyOverflow(List<Button> buttons)
        {
            if (buttons == null || buttons.Count == 0) { overflowButton.Visible = false; return; }

            int spacing = 2;
            int totalW = 0;
            foreach (Control c in breadcrumbPanel.Controls) totalW += c.Width + spacing;
            totalW += paddingStart + 10;

            if (totalW <= this.Width)
            {
                overflowButton.Visible = false;
                foreach (Button b in buttons) b.Visible = true;
                return;
            }

            overflowButton.Visible = true;
            overflowButton.Menu.Items.Clear();

            int available = this.Width - paddingStart - 8 - overflowButton.Width - 8;
            if (available < 0) available = 0;

            int accumulated = 0;
            int keepFromIndex = buttons.Count;
            for (int i = buttons.Count - 1; i >= 0; i--)
            {
                Button b = buttons[i];
                int w = b.Width + spacing;
                if (accumulated + w > available)
                {
                    keepFromIndex = i + 1;
                    break;
                }
                accumulated += w;
                keepFromIndex = i;
            }

            if (keepFromIndex >= buttons.Count) keepFromIndex = buttons.Count - 1;

            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Visible = i >= keepFromIndex;

            for (int i = 0; i < keepFromIndex; i++)
            {
                Button hidden = buttons[i];
                System.Windows.Forms.ToolStripMenuItem item = new(hidden.Text) { Tag = hidden.Tag, Image = Assets.GitHubMgr.folder_web_16 };
                item.Click += (s, e) =>
                {
                    TreeNode tn = (s as ToolStripMenuItem).Tag as TreeNode;
                    if (boundTreeView != null) boundTreeView.SelectedNode = tn;
                };
                overflowButton.Menu.Items.Add(item);
            }
        }

        #endregion

        #region Event Handlers

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e) => UpdateBreadcrumb(e.Node);

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is TreeNode node)
                if (boundTreeView != null) boundTreeView.SelectedNode = node;

            AnimateHover(false);
        }

        private void BreadcrumbPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isMarquee || _animatedValue > progressMinimum && _animatedValue < progressMaximum) return;

            Control child = breadcrumbPanel.GetChildAtPoint(e.Location);
            if (child == null && e.Button == MouseButtons.Left)
            {
                pathTextBox.Bounds = new Rectangle(breadcrumbPanel.Left + 4, breadcrumbPanel.Top + 4, Math.Max(120, breadcrumbPanel.Width - 6), 24);
                pathTextBox.Text = string.Join("\\", GetCurrentPathNodes());

                PopulateAutoComplete();

                pathTextBox.Visible = true;
                pathTextBox.BringToFront();
                pathTextBox.Focus();
                pathTextBox.SelectionStart = 0;
                pathTextBox.SelectionLength = pathTextBox.Text.Length;
            }
        }

        private void TB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string path = pathTextBox.Text;
                pathTextBox.Visible = false;
                NavigateToPath(path);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                pathTextBox.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Ensure right panel is properly positioned
            if (rightPanel != null)
            {
                rightPanel.Width = rightPanelWidth;
                rightPanel.Dock = DockStyle.Right;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            if (pathTextBox.Visible) pathTextBox.Visible = false;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            // Update breadcrumb panel dimensions
            breadcrumbPanel.Height = Height;

            // Position stop button to the left of the splitter
            btn_Stop.Height = Height - 4;
            btn_Stop.Top = 2;
            btn_Stop.Left = rightSplitter.Left - btn_Stop.Width - 4;

            // Update breadcrumb panel width (space between start and stop button)
            breadcrumbPanel.Width = Math.Max(0, btn_Stop.Left - breadcrumbPanel.Left - 4);

            // Ensure right panel maintains its width
            if (rightPanel.Width != rightPanelWidth)
            {
                rightPanel.Width = rightPanelWidth;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // When control resizes, update layout
            PerformLayout();
        }

        #endregion

        #region Navigation

        private void PopulateAutoComplete()
        {
            if (boundTreeView == null) return;

            AutoCompleteStringCollection ac = new();
            foreach (TreeNode root in boundTreeView.Nodes) AddNodesToAutoComplete(root, "", ac);

            pathTextBox.TB.AutoCompleteCustomSource = ac;
        }

        private void AddNodesToAutoComplete(TreeNode node, string prefix, AutoCompleteStringCollection ac)
        {
            string path = string.IsNullOrEmpty(prefix) ? node.Text : prefix + "\\" + node.Text;
            ac.Add(path);

            foreach (TreeNode child in node.Nodes) AddNodesToAutoComplete(child, path, ac);
        }

        private void NavigateToPath(string path)
        {
            if (boundTreeView == null || boundTreeView.Nodes.Count == 0) return;

            path = path?.Trim();
            if (string.IsNullOrWhiteSpace(path) || path.Trim('\\', '/').Length == 0)
            {
                TreeNode root = boundTreeView.TopNode ?? boundTreeView.Nodes.Cast<TreeNode>().FirstOrDefault();
                if (root != null)
                {
                    boundTreeView.SelectedNode = root;
                    root.Expand();
                }
                return;
            }

            string[] segs = path.Split(['\\', '/'], StringSplitOptions.RemoveEmptyEntries);
            TreeNode rootNode = boundTreeView.TopNode ?? boundTreeView.Nodes.Cast<TreeNode>().FirstOrDefault();
            TreeNode node = FindNodeByPath(segs, 0, rootNode);

            if (node != null)
            {
                boundTreeView.SelectedNode = node;
                boundTreeView.SelectedNode.Expand();
            }
            else
            {
                MsgBox(string.Format(Program.Localization.Strings.GitHubStrings.Explorer_FileNotFound, Application.ProductName, path), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TreeNode FindNodeByPath(string[] segments, int index, TreeNode current)
        {
            if (current == null || index >= segments.Length) return null;
            if (!string.Equals(current.Text, segments[index], StringComparison.OrdinalIgnoreCase)) return null;
            if (index == segments.Length - 1) return current;

            TreeNode next = current.Nodes.Cast<TreeNode>().FirstOrDefault(n => string.Equals(n.Text, segments[index + 1], StringComparison.OrdinalIgnoreCase));
            return FindNodeByPath(segments, index + 1, next);
        }

        private string[] GetCurrentPathNodes()
        {
            if (boundTreeView?.SelectedNode == null) return Array.Empty<string>();
            return boundTreeView.SelectedNode.FullPath.Split('\\');
        }

        #endregion

        #region Marquee & Animation

        /// <summary>
        /// Starts a marquee animation for indeterminate progress.
        /// </summary>
        public void StartMarquee()
        {
            if (_marqueeTimer != null || DesignMode) return;

            _isMarquee = true;

            _marqueeTimer = new() { Interval = 20 };
            _marqueeTimer.Tick += (s, e) =>
            {
                _marqueeOffset += 0.01f;
                if (_marqueeOffset > 1) _marqueeOffset = 0;
                Invalidate();
            };
            _marqueeTimer.Start();
            btn_Stop.Visible = true;
        }

        /// <summary>
        /// Stops the marquee animation.
        /// </summary>
        public void StopMarquee()
        {
            _marqueeTimer?.Stop();
            _marqueeTimer?.Dispose();
            _marqueeTimer = null;
            _isMarquee = false;
            _marqueeOffset = 0;
            Invalidate();
            btn_Stop.Visible = false;
        }

        private void InitProgressAnimation()
        {
            if (_progressTimer != null || DesignMode) return;

            _progressTimer = new() { Interval = 35 };
            _progressTimer.Tick += (s, e) =>
            {
                // Smoothly animate progress value toward target
                float diff = progressValue - _animatedValue;
                _animatedValue += diff * 0.1f; // easing factor

                // Move the highlight across the progress bar
                _hoverOffset += 0.01f;
                if (_hoverOffset > 1) _hoverOffset = 0;

                Invalidate();
            };

            // Start timer only if value is between minimum and maximum
            if (progressValue > progressMinimum && progressValue < progressMaximum)
                _progressTimer.Start();
        }

        /// <summary>
        /// Performs the finish-loading animation (hides progress bar).
        /// </summary>
        public void FinishLoadingAnimation()
        {
            if (CanAnimate)
            {
                Transition.With(this, nameof(Alpha), 0)
                          .HookOnCompletion(() =>
                          {
                              progressValue = 0;
                              _animatedValue = 0;
                              Invalidate();
                              _alpha = 255;
                              StopMarquee();
                          })
                          .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                Alpha = 0;
                progressValue = 0;
                _animatedValue = 0;
                Invalidate();
                _alpha = 255;
                StopMarquee();
            }
            btn_Stop.Visible = false;
        }

        /// <summary>
        /// Subscribes the specified control and all of its child controls to hover event handlers that trigger hover
        /// animations when the mouse enters or leaves the control.
        /// </summary>
        /// <remarks>This method recursively attaches event handlers to the provided control and its
        /// children to ensure consistent hover animation behavior throughout the control hierarchy.</remarks>
        /// <param name="ctrl">The control to which hover event handlers will be attached, including all of its child controls. Cannot be
        /// null.</param>
        private void SubscribeHoverEvents(Control ctrl)
        {
            ctrl.MouseEnter += (s, e) => CheckHover();
            ctrl.MouseLeave += (s, e) =>
            {
                // Check if mouse is still inside main control
                Point pt = this.PointToClient(Cursor.Position);
                if (!this.ClientRectangle.Contains(pt)) CheckHover();
            };

            foreach (Control child in ctrl.Controls) SubscribeHoverEvents(child);
        }

        private void CheckHover()
        {
            Point pt = this.PointToClient(Cursor.Position);

            // Check if mouse is inside main area (left of splitter)
            bool insideMain = pt.X >= 0 && pt.X < rightSplitter.Left && pt.Y >= 0 && pt.Y < this.Height;

            AnimateHover(insideMain);
        }

        /// <summary>
        /// Animates the hover effect by transitioning the hover alpha value when the pointer enters or leaves the
        /// control.
        /// </summary>
        /// <param name="enter">Indicates whether the pointer is entering (<see langword="true"/>) or leaving (<see langword="false"/>) the
        /// control. If <see langword="true"/>, the hover effect is shown; otherwise, it is hidden.</param>
        private void AnimateHover(bool enter)
        {
            if (CanAnimate)
            {
                Transition.With(this, nameof(HoverAlpha), enter ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                HoverAlpha = enter ? 255 : 0;
            }
        }

        #endregion

        #region Overrides

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            parentLevel = this.Level();
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            // Let parent paint behind and enable transparency
            if (Parent != null)
            {
                GraphicsState state = G.Save();
                G.TranslateTransform(-Left, -Top);
                PaintEventArgs pea = new(G, new Rectangle(Point.Empty, Parent.Size));
                InvokePaintBackground(Parent, pea);
                InvokePaint(Parent, pea);
                G.Restore(state);
            }

            int gap = 4; // gap between main and container

            Rectangle fullRect = new(0, 0, Width - 1, Height - 1);
            Rectangle mainRect = new(0, 0, rightSplitter.Left - gap + 1, Height - 1);
            Rectangle containerRect = new(rightSplitter.Left + gap, 0, Width - (rightSplitter.Left + gap) - 1, Height - 1);

            // -------------------
            // Paint Main Part
            // -------------------
            using (SolidBrush br = new(Program.Style.Schemes.Main.Colors.Back(parentLevel)))
            using (SolidBrush br_hover = new(Color.FromArgb(_hoverAlpha, Program.Style.Schemes.Main.Colors.Back_Checked)))
            {
                G.FillRoundedRect(br, mainRect);
                G.FillRoundedRect(br_hover, mainRect);
            }

            using (Pen p = new(Color.FromArgb(200, Program.Style.Schemes.Main.Colors.Line_Hover(parentLevel))))
            {
                G.DrawRoundedRect(p, mainRect);
            }

            bool isInProgress = _animatedValue > progressMinimum;

            if (_isMarquee)
            {
                float segmentWidth = mainRect.Width * 0.25f;
                float offsetX = mainRect.Width * _marqueeOffset;

                RectangleF marqueeRect = new(offsetX, 0, segmentWidth, mainRect.Height);
                using (LinearGradientBrush brush = new(marqueeRect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
                {
                    ColorBlend cb = new()
                    {
                        Colors = [Color.Transparent, Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Accent), Color.Transparent],
                        Positions = [0f, 0.5f, 1f]
                    };
                    brush.InterpolationColors = cb;

                    if (offsetX + segmentWidth > mainRect.Width)
                    {
                        float rightWidth = mainRect.Width - offsetX;
                        RectangleF rightRect = new(offsetX, 0, rightWidth, mainRect.Height);
                        G.FillRoundedRect(brush, rightRect);

                        float leftWidth = segmentWidth - rightWidth;
                        RectangleF leftRect = new(0, 0, leftWidth, mainRect.Height);
                        using (LinearGradientBrush leftBrush = new(leftRect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
                        {
                            leftBrush.InterpolationColors = cb;
                            G.FillRoundedRect(leftBrush, leftRect);
                        }
                    }
                    else
                    {
                        G.FillRoundedRect(brush, marqueeRect);
                    }
                }
            }
            else if (isInProgress)
            {
                float progressWidth = mainRect.Width * (_animatedValue - progressMinimum) / (progressMaximum - progressMinimum);
                RectangleF progRect = new(0, 0, progressWidth, mainRect.Height);

                using (SolidBrush br = new(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Accent)))
                    G.FillRoundedRect(br, progRect);

                float gradientWidth = Math.Min(100, mainRect.Width);
                float highlightPos = (_hoverOffset * (mainRect.Width + gradientWidth)) - gradientWidth;
                float visibleWidth = Math.Min(highlightPos + gradientWidth, progRect.Width) - Math.Max(highlightPos, 0);

                if (visibleWidth > 0)
                {
                    float drawX = Math.Max(highlightPos, 0);
                    RectangleF highlightRect = new(drawX, 0, visibleWidth, mainRect.Height);

                    using (LinearGradientBrush brush = new(highlightRect, Color.Transparent, Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover), LinearGradientMode.Horizontal))
                    {
                        ColorBlend cb = new()
                        {
                            Colors = [Color.Transparent, Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover), Color.Transparent],
                            Positions = [0f, 0.5f, 1f]
                        };
                        brush.InterpolationColors = cb;
                        G.FillRoundedRect(brush, highlightRect);
                    }
                }

                using (Pen p = new(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.ForeColor_Accent)))
                    G.DrawRoundedRect(p, progRect);
            }

            using (Pen p_hover = new(Color.FromArgb(_hoverAlpha, Program.Style.Schemes.Main.Colors.Line_Checked_Hover)))
            {
                G.DrawRoundedRect(p_hover, mainRect);
            }

            Bitmap img = !isInProgress && !_isMarquee
                ? hasPaths ? Program.Style.DarkMode ? icon_dark : icon_light : null
                : Program.Style.DarkMode ? icon_wait_dark : icon_wait_light;

            if (img is not null)
            {
                int imageWidth = 16;
                RectangleF imageRect = new(2f + (paddingStart - imageWidth) / 2f, (mainRect.Height - imageWidth) / 2f, imageWidth, imageWidth);
                G.DrawImage(img, imageRect);
            }

            // -------------------
            // Paint Container Part
            // -------------------
            using (SolidBrush br = new(Program.Style.Schemes.Main.Colors.Back(parentLevel)))
            using (Pen p = new(Color.FromArgb(200, Program.Style.Schemes.Main.Colors.Line_Hover(parentLevel))))
            {
                G.FillRoundedRect(br, containerRect);
                G.DrawRoundedRect(p, containerRect);
            }
        }

        #endregion
    }
}