using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Controllers
{
    public class Magnifier : Control
    {
        private Bitmap _cachedScreen;
        private Timer _refreshTimer;
        private Point _lastMousePos;

        private readonly MouseHook _mouseHook;

        private int _zoom = 3;
        public int Zoom
        {
            get => _zoom;
            set
            {
                if (value > 0)
                {
                    _zoom = value;
                    Invalidate();
                }
            }
        }

        private bool _livePreview = true;
        public bool LivePreview
        {
            get => _livePreview;
            set
            {
                if (_livePreview != value)
                {
                    _livePreview = value;
                    UpdateRefreshMode();
                }
            }
        }

        float DPI = Program.GetWindowsScreenScalingFactor(false);

        public Magnifier()
        {
            DoubleBuffered = true;

            // Timer for live preview (~60 FPS)
            _refreshTimer = new Timer { Interval = 16 };
            _refreshTimer.Tick += (s, e) =>
            {
                if (Enabled && _livePreview)
                    Invalidate();
            };

            // Initialize mouse hook
            _mouseHook = new MouseHook(this);
            _mouseHook.MouseMoved += (s, e) =>
            {
                if (Enabled && !_livePreview)
                {
                    if (_lastMousePos != e.Location)
                    {
                        _lastMousePos = e.Location;
                        Invalidate();
                    }
                }
            };
        }

        private void UpdateRefreshMode()
        {
            if (!DesignMode && Enabled)
            {
                if (_livePreview)
                {
                    // Stop hook, start timer
                    _mouseHook.Enabled = false;
                    _refreshTimer.Start();
                }
                else
                {
                    // Stop timer, start hook
                    _refreshTimer.Stop();
                    _mouseHook.Enabled = true;
                }
            }
        }

        private Bitmap GetScreen()
        {
            if (DesignMode) return null;

            Point mousePos = _livePreview ? Cursor.Position : _lastMousePos;
            mousePos = new Point((int)(mousePos.X * DPI), (int)(mousePos.Y * DPI));

            // Only recapture if size changed
            if (_cachedScreen == null || _cachedScreen.Width != Width / _zoom || _cachedScreen.Height != Height / _zoom)
            {
                _cachedScreen?.Dispose();
                _cachedScreen = new Bitmap(Width / _zoom, Height / _zoom);
            }

            using (Graphics g = Graphics.FromImage(_cachedScreen))
            {
                Rectangle sourceRect = new(
                    mousePos.X - _cachedScreen.Width / 2,
                    mousePos.Y - _cachedScreen.Height / 2,
                    _cachedScreen.Width,
                    _cachedScreen.Height
                );

                g.CopyFromScreen(sourceRect.Location, Point.Empty, sourceRect.Size);
            }

            return _cachedScreen;
        }

        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                if (value != base.Enabled)
                {
                    base.Enabled = value;

                    if (!DesignMode)
                    {
                        if (value)
                        {
                            DPI = Program.GetWindowsScreenScalingFactor(false);
                            UpdateRefreshMode();
                        }
                        else
                        {
                            _refreshTimer.Stop();
                            _mouseHook.Enabled = false;
                        }
                    }

                    Invalidate();
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _cachedScreen?.Dispose();
            _cachedScreen = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.InterpolationMode = InterpolationMode.NearestNeighbor;
            G.CompositingQuality = CompositingQuality.HighQuality;
            G.SmoothingMode = SmoothingMode.None;
            G.PixelOffsetMode = PixelOffsetMode.Half;

            if (!DesignMode && Enabled)
            {
                Bitmap screen = GetScreen();
                if (screen != null)
                {
                    // Draw magnified screen content
                    G.DrawImage(screen, new Rectangle(0, 0, Width, Height));

                    // Draw grid
                    using (Pen gridPen = new(Color.FromArgb(80, Color.Black), 1))
                    {
                        int gridSpacing = Zoom; // Grid spacing matches zoom level

                        // Vertical grid lines
                        for (int x = 0; x < Width; x += gridSpacing)
                        {
                            G.DrawLine(gridPen, x, 0, x, Height);
                        }

                        // Horizontal grid lines
                        for (int y = 0; y < Height; y += gridSpacing)
                        {
                            G.DrawLine(gridPen, 0, y, Width, y);
                        }
                    }

                    // Draw center rectangle (highlighting the exact pixel under cursor)
                    int centerX = Width / 2;
                    int centerY = Height / 2;

                    // Calculate rectangle dimensions to match one source pixel
                    int rectSize = Zoom;
                    int rectX = centerX - (rectSize / 2);
                    int rectY = centerY - (rectSize / 2);

                    // Draw the center rectangle with contrasting colors
                    using (Pen centerPen = new(Color.Red, 2))
                    using (Pen innerPen = new(Color.Yellow, 1))
                    {
                        // Outer rectangle
                        G.DrawRectangle(centerPen, rectX, rectY, rectSize, rectSize);

                        // Inner crosshair
                        int halfRect = rectSize / 2;
                        G.DrawLine(innerPen, centerX - halfRect, centerY, centerX + halfRect, centerY);
                        G.DrawLine(innerPen, centerX, centerY - halfRect, centerX, centerY + halfRect);

                        // Center dot
                        G.FillRectangle(Brushes.White, centerX - 1, centerY - 1, 2, 2);
                    }
                }
                else
                {
                    G.Clear(Color.Black);
                }
            }

            // Draw border
            using (Pen borderPen = new(Enabled ? Color.WhiteSmoke : Color.Gray))
            {
                borderPen.DashStyle = Enabled ? DashStyle.Solid : DashStyle.Dash;
                G.DrawRectangle(borderPen, 1, 1, Width - 1, Height - 1);
            }

            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            _refreshTimer?.Dispose();
            _cachedScreen?.Dispose();
            _mouseHook?.Dispose();
            base.Dispose(disposing);
        }
    }
}
