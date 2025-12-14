using FluentTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    /// <summary>
    /// A custom breadcrumb control for navigation and progress display.
    /// </summary>
    public partial class Breadcrumb : UserControl
    {
        #region Fields

        private SmoothFlowLayoutPanel breadcrumbPanel;
        private TextBox pathTextBox;
        private Button overflowButton;

        private int paddingStart = 28;
        private Bitmap icon_dark = Properties.Resources.Glyph_Browse;
        private Bitmap icon_light = Properties.Resources.Glyph_Browse.Invert();
        private Bitmap icon_wait_dark = Properties.Resources.Glyph_Wait;
        private Bitmap icon_wait_light = Properties.Resources.Glyph_Wait.Invert();

        private bool _isMarquee;
        private float _marqueeOffset = 0;
        private Timer _marqueeTimer;
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
                }
            }
        }

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Breadcrumb"/> class.
        /// </summary>
        public Breadcrumb()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            InitializeControls();
            Resize += (s, e) => UpdateBreadcrumb(boundTreeView?.SelectedNode);
            SubscribeHoverEvents(this);
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
            Controls.Add(breadcrumbPanel);

            btn_Stop = new()
            {
                ImageGlyphEnabled = true,
                ImageGlyph = Properties.Resources.Glyph_Explorer_Stop,
                Flag = Button.Flags.CustomColorOnHover,
                CustomColor = Color.FromArgb(193, 18, 31),
                Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                Height = Height - 4,
                Width = 28,
                Visible = true,
                Location = new Point(this.Width - 30, 2),
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

            Controls.Add(pathTextBox);
            Controls.Add(btn_Stop);
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

        #region Marquee & Animation

        /// <summary>
        /// Starts a marquee animation for indeterminate progress.
        /// </summary>
        public void StartMarquee()
        {
            if (_marqueeTimer != null) return;

            _isMarquee = true;

            _marqueeTimer = new() { Interval = 10 };
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
            ctrl.MouseEnter += (s, e) => AnimateHover(true);
            ctrl.MouseLeave += (s, e) =>
            {
                // Check if mouse is still inside main control
                Point pt = this.PointToClient(Cursor.Position);
                if (!this.ClientRectangle.Contains(pt)) AnimateHover(false);
            };

            foreach (Control child in ctrl.Controls) SubscribeHoverEvents(child);
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
                pathTextBox.Bounds = new Rectangle(breadcrumbPanel.Left + 6, breadcrumbPanel.Top + 4, Math.Max(120, breadcrumbPanel.Width - 6), 22);
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

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            if (pathTextBox.Visible) pathTextBox.Visible = false;
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
                MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Explorer_FileNotFound, Application.ProductName, path), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region Overrides

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            parentLevel = this.Level();
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Let parent paint behind and hence transparent background
            if (Parent != null)
            {
                GraphicsState state = e.Graphics.Save();
                e.Graphics.TranslateTransform(-Left, -Top);
                PaintEventArgs pea = new(e.Graphics, new Rectangle(Point.Empty, Parent.Size));
                InvokePaintBackground(Parent, pea);
                InvokePaint(Parent, pea);
                e.Graphics.Restore(state);
            }

            Graphics G = e.Graphics;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);
            bool isInProgress = _animatedValue > progressMinimum;

            using (SolidBrush br = new(Program.Style.Schemes.Main.Colors.Back(parentLevel)))
            using (Pen p = new(Program.Style.Schemes.Main.Colors.Line_Hover(parentLevel)))
            using (SolidBrush br_hover = new(Color.FromArgb(_hoverAlpha, Program.Style.Schemes.Main.Colors.Back_Checked)))
            using (Pen p_hover = new(Color.FromArgb(_hoverAlpha, Program.Style.Schemes.Main.Colors.Line_Checked)))
            {
                G.FillRoundedRect(br, rect);
                G.FillRoundedRect(br_hover, rect);
                G.DrawRoundedRect(p, rect);
                G.DrawRoundedRect(p_hover, rect);
            }

            float progressWidth;

            if (_isMarquee)
            {
                float segmentWidth = rect.Width * 0.25f;
                float offsetX = rect.Width * _marqueeOffset;

                if (offsetX + segmentWidth > rect.Width)
                {
                    RectangleF leftRect = new(offsetX, 0, rect.Width - offsetX, Height - 1);
                    G.FillRoundedRect(new SolidBrush(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Accent)), leftRect);
                    G.DrawRoundedRect(new Pen(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.ForeColor_Accent)), leftRect);

                    RectangleF rightRect = new(0, 0, segmentWidth - (rect.Width - offsetX), Height - 1);
                    G.FillRoundedRect(new SolidBrush(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Accent)), rightRect);
                    G.DrawRoundedRect(new Pen(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.ForeColor_Accent)), rightRect);
                }
                else
                {
                    RectangleF progRect = new(offsetX, 0, segmentWidth, Height - 1);
                    G.FillRoundedRect(new SolidBrush(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Accent)), progRect);
                    G.DrawRoundedRect(new Pen(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.ForeColor_Accent)), progRect);
                }
            }
            else if (_animatedValue > progressMinimum)
            {
                progressWidth = rect.Width * (_animatedValue - progressMinimum) / (progressMaximum - progressMinimum);
                RectangleF progRect = new(0, 0, progressWidth, Height - 1);
                using (SolidBrush br = new(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.Accent)))
                using (Pen p = new(Color.FromArgb(_alpha, Program.Style.Schemes.Tertiary.Colors.ForeColor_Accent)))
                {
                    G.FillRoundedRect(br, progRect);
                    G.DrawRoundedRect(p, progRect);
                }
            }

            Bitmap img = !isInProgress && !_isMarquee
                ? hasPaths ? Program.Style.DarkMode ? icon_dark : icon_light : null
                : Program.Style.DarkMode ? icon_wait_dark : icon_wait_light;

            if (img is not null)
            {
                int imageWidth = 16;
                RectangleF imageRect = new(2f + (paddingStart - imageWidth) / 2f, (rect.Height - imageWidth) / 2f, imageWidth, imageWidth);
                G.DrawImage(img, imageRect);
            }
        }

        #endregion
    }
}