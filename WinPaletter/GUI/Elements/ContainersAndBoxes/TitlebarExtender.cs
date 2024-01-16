using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.WP
{
    public partial class TitlebarExtender : ContainerControl
    {
        private bool _formFocused = true;

        public TitlebarExtender()
        {
            BackColor = Color.Black;
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
                    update_DWM();
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

                update_DWM();
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
            if (!DesignMode) update_DWM();

            base.OnSizeChanged(e);
        }

        protected override void OnDockChanged(EventArgs e)
        {
            if (!DesignMode) update_DWM();

            base.OnDockChanged(e);
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            _formFocused = true;
            update_DWM();
        }

        private void Form_Deactivate(object sender, EventArgs e)
        {
            _formFocused = false;
            update_DWM();
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
            if (FindForm() != null && e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)oldPoint;
                FindForm().Location = newPoint;
            }

            base.OnMouseMove(e);
        }

        private void update_DWM()
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
                    if (DWMAPI.IsCompositionEnabled())
                    {
                        BackColor = Color.Black;
                        if (OS.W10)
                        {
                            FindForm()?.DropEffect(p, true, DWM.FormStyle.Aero);
                        }
                        else
                        {
                            FindForm()?.DropEffect(p);
                        }
                    }
                    else
                    {
                        if (FindForm() != null)
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
                BackColor = scheme.Colors.Back_Hover;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_dropDWMEffect)
            {
                if (!TabLocation.IsEmpty)
                {
                    Rectangle rect = new(-1, 0, Width + 1, Height - 1);
                    Rectangle rectExclude = new(TabLocation.X, 0, TabLocation.Width, 1);
                    e.Graphics.ExcludeClip(rectExclude);
                    using (Pen P = new(scheme.Colors.Line_Hover)) { e.Graphics.DrawRectangle(P, rect); }
                    e.Graphics.ResetClip();
                }
            }

            base.OnPaint(e);
        }
    }
}