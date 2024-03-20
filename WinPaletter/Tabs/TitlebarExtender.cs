using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;

namespace WinPaletter.Tabs
{
    public partial class TitlebarExtender : ContainerControl
    {
        private bool _formFocused = true;

        public TitlebarExtender()
        {
            BackColor = Color.Black;
            DoubleBuffered = true;
        }

        Config.Scheme scheme => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

        private bool _dropDWMEffect = false;
        public bool DropDWMEffect
        {
            get => _dropDWMEffect;
            set
            {
                if (_dropDWMEffect != value)
                {
                    _dropDWMEffect = value;
                    updateBackDrop();
                    Refresh();
                }
            }
        }

        private Rectangle _tabLocation = Rectangle.Empty;
        public Rectangle TabLocation
        {
            get => _tabLocation;
            set
            {
                if (_tabLocation != value)
                {
                    _tabLocation = value;
                    Refresh();
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                if (FindForm() != null)
                {
                    FindForm().Activated += Form_Activated;
                    FindForm().Deactivate += Form_Deactivate; ;
                }

                updateBackDrop();
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated -= Form_Activated;
                FindForm().Deactivate -= Form_Deactivate; ;
            }

            base.OnHandleDestroyed(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (!DesignMode) updateBackDrop();

            base.OnSizeChanged(e);
        }

        protected override void OnDockChanged(EventArgs e)
        {
            if (!DesignMode) updateBackDrop();

            base.OnDockChanged(e);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is Panel || e.Control is FlowLayoutPanel)
            {
                e.Control.MouseDown += (s, e) => { OnMouseDown(e); };
                e.Control.MouseMove += (s, e) => { OnMouseMove(e); };
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            if (e.Control is Panel || e.Control is FlowLayoutPanel)
            {
                e.Control.MouseDown -= (s, e) => { OnMouseDown(e); };
                e.Control.MouseMove -= (s, e) => { OnMouseMove(e); };
            }
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            _formFocused = true;
            updateBackDrop();
        }

        private void Form_Deactivate(object sender, EventArgs e)
        {
            _formFocused = false;
            updateBackDrop();
        }

        private Point newPoint = new();
        private Point oldPoint = new();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            oldPoint = MousePosition - (Size)FindForm()?.Location;

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dropDWMEffect && FindForm() != null && e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)oldPoint;
                FindForm().Location = newPoint;
            }

            base.OnMouseMove(e);
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        private void updateBackDrop()
        {
            if (_dropDWMEffect)
            {
                Padding p = Padding.Empty;

                if (Dock == DockStyle.Top)
                {
                    p = new(0, Height, 0, 0);
                }
                else if (Dock == DockStyle.Bottom)
                {
                    p = new(0, 0, 0, Height);
                }
                else if (Dock == DockStyle.Left)
                {
                    p = new(Width, 0, 0, 0);
                }
                else if (Dock == DockStyle.Right)
                {
                    p = new(0, 0, Width, 0);
                }
                else if (Dock == DockStyle.Fill)
                {
                    p = new(0);
                }

                if (p != Padding.Empty)
                {
                    Form form = FindForm();

                    if (form is not null) Forms.MainForm.Text = form.Text;

                    if (form is not null && form.Parent is not null) form = form.Parent.FindForm();


                    if (DWMAPI.IsCompositionEnabled())
                    {
                        if (OS.WVista || OS.W7 || OS.W8x)
                        {
                            BackColor = Color.Black;
                        }
                        else
                        {
                            bool transparency = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 1)) == 1;
                            bool accentOnTitlebars = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", 1)) == 1;

                            if (transparency)
                            {
                                BackColor = Color.Black;

                                if (OS.W10)
                                {
                                    if (form is not null) form?.DropEffect(p, true, DWM.FormStyle.Aero);
                                }
                                else
                                {
                                    if (form is not null) form?.DropEffect(p);
                                }
                            }
                            else if (accentOnTitlebars)
                            {
                                if (form is not null)
                                {
                                    Color activeTtl, inactiveTtl;

                                    object y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Color.Black.Reverse().ToArgb());
                                    activeTtl = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                                    y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Color.Black.Reverse().ToArgb());
                                    inactiveTtl = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                                    BackColor = _formFocused ? activeTtl : inactiveTtl;

                                    form?.ResetEffect();
                                }
                            }
                            else
                            {
                                bool appUseLightMode = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 0)) == 1;

                                if (appUseLightMode)
                                {
                                    BackColor = Color.White;
                                }
                                else
                                {
                                    BackColor = Color.FromArgb(32, 32, 32);
                                }

                                form?.ResetEffect();
                            }
                        }
                    }
                    else
                    {
                        if (form is not null)
                        {
                            if (!Program.ClassicThemeRunning)
                            {
                                VisualStyleRenderer VS = new(_formFocused ? VisualStyleElement.Window.Caption.Active : VisualStyleElement.Window.Caption.Inactive);
                                using (Bitmap b = new(50, 50))
                                using (Graphics G = Graphics.FromImage(b))
                                {
                                    VS.DrawBackground(G, new Rectangle(0, 0, 50, 50));
                                    BackColor = b.GetPixel(47, 49);
                                }
                            }
                            else
                            {
                                BackColor = _formFocused ? SystemColors.ActiveCaption : SystemColors.InactiveCaption;
                            }
                        }
                    }
                }
            }
            else
            {
                BackColor = scheme.Colors.Back_Hover(parentLevel);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_dropDWMEffect)
            {
                if (!TabLocation.IsEmpty)
                {
                    Rectangle rect = new(-1, 0, Width + 1, Height - 1);
                    Rectangle rectExclude = new(TabLocation.X + 1, 0, TabLocation.Width - 1, 1);
                    e.Graphics.ExcludeClip(rectExclude);
                    using (Pen P = new(scheme.Colors.Line_Hover(parentLevel))) { e.Graphics.DrawRectangle(P, rect); }
                    e.Graphics.ResetClip();
                }
            }

            base.OnPaint(e);
        }
    }
}