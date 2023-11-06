using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinPaletter.TypesExtensions
{
    public static class GraphicsExtensions
    {
        public static void DrawGlowString(this Graphics G, int GlowSize, string Text, Font Font, Color ForeColor, Color GlowColor, Rectangle ClientRect, Rectangle Rect, StringFormat FormatX)
        {
            int w = (int)Math.Round(Math.Max(8d, ClientRect.Width / 5d));
            int h = (int)Math.Round(Math.Max(8d, ClientRect.Height / 5d));
            float emSize = G.DpiY * Font.SizeInPoints / 72f;
            if (Text is null | string.IsNullOrWhiteSpace(Text))
                Text = "";

            using (var b = new Bitmap(w, h))
            {
                using (var gp = new GraphicsPath())
                {
                    gp.AddString(Text, Font.FontFamily, (int)Font.Style, emSize, Rect, FormatX);

                    using (var gx = Graphics.FromImage(b))
                    {
                        using (var m = new Matrix(1.0f / 5f, 0f, 0f, 1.0f / 5f, -(1.0f / 5f), -(1.0f / 5f)))
                        {
                            gx.SmoothingMode = SmoothingMode.AntiAlias;
                            gx.Transform = m;
                            using (var pn = new Pen(GlowColor, GlowSize))
                            {
                                gx.DrawPath(pn, gp);
                                gx.FillPath(pn.Brush, gp);
                            }
                        }
                    }

                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.DrawImage(b, ClientRect, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel);

                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var br = new SolidBrush(ForeColor))
                    {
                        G.DrawString(Text, Font, br, Rect, FormatX);
                    }

                }
            }
        }

        public static void DrawGlow(this Graphics G, Rectangle R, Color GlowColor, int GlowSize = 5, int GlowFade = 7)
        {
            try
            {
                if (GlowSize <= 0)
                    GlowSize = 1;
                if (GlowFade <= 0)
                    GlowFade = 1;

                Rectangle Rect;
                Rect = new Rectangle(R.X - GlowSize - 2, R.Y - GlowSize - 2, R.Width + GlowSize * 2 + 3, R.Height + GlowSize * 2 + 3);

                using (var bm = new Bitmap((int)Math.Round(Rect.Width / (double)GlowFade), (int)Math.Round(Rect.Height / (double)GlowFade)))
                {
                    using (var G2 = Graphics.FromImage(bm))
                    {
                        var Rect2 = new Rectangle(1, 1, bm.Width, bm.Height);

                        using (var br = new SolidBrush(GlowColor))
                        {
                            G2.FillRectangle(br, Rect2);
                        }

                        G.DrawImage(bm, Rect);
                    }
                }
            }
            catch
            {
            }
        }

        public static void DrawAeroEffect(this Graphics G, Rectangle Rect, Bitmap BackgroundBlurred, Color Color1, decimal ColorBalance, Color Color2, decimal GlowBalance, decimal alpha, int Radius, bool RoundedCorners)
        {

            if (RoundedCorners)
            {
                if (BackgroundBlurred is not null)
                    G.DrawRoundImage(BackgroundBlurred, Rect, Radius, true);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * 255), Color.Black)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (ColorBalance * 255)), Color1)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }

                var C1 = Color.FromArgb((int)Math.Round(ColorBalance * 255), Color1);
                var C2 = Color.FromArgb((int)Math.Round(GlowBalance * 255), Color2);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 100)), Color2)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 150)), C1.Blend(C2, 100d))))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
            }
            else
            {
                if (BackgroundBlurred is not null)
                    G.DrawImage(BackgroundBlurred, Rect);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * 255), Color.Black)))
                {
                    G.FillRectangle(br, Rect);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (ColorBalance * 255)), Color1)))
                {
                    G.FillRectangle(br, Rect);
                }

                var C1 = Color.FromArgb((int)Math.Round(ColorBalance * 255), Color1);
                var C2 = Color.FromArgb((int)Math.Round(GlowBalance * 255), Color2);

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 100)), Color2)))
                {
                    G.FillRectangle(br, Rect);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 150)), C1.Blend(C2, 100d))))
                {
                    G.FillRectangle(br, Rect);
                }
            }
        }

        public static void FillRoundedRect(this Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = 5;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if ((GetRoundedCorners() | ForcedRoundCorner) & Radius > 0)
                {
                    using (var path = Rectangle.Round(Radius))
                    {
                        Graphics.FillPath(Brush, path);
                    }
                }
                else
                {
                    Graphics.FillRectangle(Brush, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static void DrawRoundImage(this Graphics Graphics, Image Image, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = 5;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                if ((GetRoundedCorners() | ForcedRoundCorner) & Radius > 0)
                {
                    using (var path = Rectangle.Round(Radius))
                    {
                        var reg = new Region(path);
                        Graphics.Clip = reg;
                        Graphics.DrawImage(Image, Rectangle);
                        Graphics.ResetClip();
                    }
                }
                else
                {
                    Graphics.DrawImage(Image, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static GraphicsPath Round(this Rectangle r, int radius)
        {
            try
            {
                var path = new GraphicsPath();
                int d = radius * 2;

                path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top);
                path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90f);

                path.AddLine(r.Right, r.Top + d, r.Right, r.Bottom - d);
                path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Bottom - d, r.Right, r.Bottom), 0f, 90f);

                path.AddLine(r.Right - d, r.Bottom, r.Left + d, r.Bottom);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - d, r.Left + d, r.Bottom), 90f, 90f);

                path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180f, 90f);

                path.CloseFigure();
                return path;
            }
            catch
            {
                return null;
            }
        }

        public static void DrawRoundedRect(this Graphics Graphics, Pen Pen, Rectangle Rectangle, int Radius_willbe_x2 = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius_willbe_x2 == -1)
                    Radius_willbe_x2 = 5;
                Radius_willbe_x2 *= 2;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if ((GetRoundedCorners() | ForcedRoundCorner) & Radius_willbe_x2 > 0)
                {
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius_willbe_x2, Radius_willbe_x2, 180, 90);
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius_willbe_x2 / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius_willbe_x2 / 2d), Rectangle.Y);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius_willbe_x2, Rectangle.Y, Radius_willbe_x2, Radius_willbe_x2, 270, 90);
                    Graphics.DrawLine(Pen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius_willbe_x2 / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius_willbe_x2 / 2d));
                    Graphics.DrawLine(Pen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius_willbe_x2 / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius_willbe_x2 / 2d));
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius_willbe_x2 / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius_willbe_x2 / 2d), Rectangle.Y + Rectangle.Height);
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius_willbe_x2, Radius_willbe_x2, Radius_willbe_x2, 90, 90);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius_willbe_x2, Rectangle.Y + Rectangle.Height - Radius_willbe_x2, Radius_willbe_x2, Radius_willbe_x2, 0, 90);
                }
                else
                {
                    Graphics.DrawRectangle(Pen, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static void DrawRoundedRect_LikeW11(this Graphics Graphics, Pen PenX, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                bool Dark = Program.Style.DarkMode;

                if (Radius == -1)
                    Radius = 5;
                Radius *= 2;
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var Pen = new Pen(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                {
                    using (var Pen2 = new Pen(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                    {
                        var SidePen = new Pen(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset };

                        if (Dark)
                        {
                            Pen.Color = PenX.Color.CB(0.1f);
                            Pen2.Color = PenX.Color;
                        }
                        else
                        {
                            Pen.Color = PenX.Color.CB((float)-0.02d);
                            Pen2.Color = PenX.Color.CB((float)-0.05d);
                        }

                        LinearGradientBrush G;
                        var CColor = Pen2.Color.CB((float)(Dark ? 0.03d : -0.05d));

                        if (Dark)
                        {
                            G = new LinearGradientBrush(Rectangle, CColor, Pen.Color, 180f);
                            var cblend = new ColorBlend(3)
                            {
                                Colors = new Color[3] { CColor, Pen.Color, CColor },
                                Positions = new float[3] { 0f, 0.5f, 1.0f }
                            };
                            G.InterpolationColors = cblend;
                        }
                        else
                        {
                            G = new LinearGradientBrush(Rectangle, Pen.Color, CColor, 180f);
                            var cblend = new ColorBlend(3)
                            {
                                Colors = new Color[3] { Pen.Color, CColor, Pen.Color },
                                Positions = new float[3] { 0f, 0.5f, 1.0f }
                            };
                            G.InterpolationColors = cblend;
                        }

                        using (var PenG = new Pen(G, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                        {

                            if ((GetRoundedCorners() | ForcedRoundCorner) & Radius > 0)
                            {

                                if (Dark)
                                {
                                    Graphics.DrawLine(Pen2, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);
                                    Graphics.DrawArc(Pen2, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                    Graphics.DrawArc(Pen2, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);

                                    SidePen = Pen2;

                                    Graphics.DrawLine(SidePen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));
                                    Graphics.DrawLine(SidePen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                                    Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                    Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                                    Graphics.DrawLine(PenG, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);
                                }

                                else
                                {
                                    Graphics.DrawLine(PenG, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);
                                    Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                    Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);

                                    SidePen = Pen;

                                    Graphics.DrawLine(SidePen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));
                                    Graphics.DrawLine(SidePen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);
                                }
                            }

                            else
                            {

                                if (Dark)
                                {
                                    Pen.Color = PenX.Color.CB(0.05f);
                                }
                                else
                                {
                                    Pen.Color = PenX.Color.CB((float)-0.02d);
                                }

                                Graphics.DrawRectangle(Pen, Rectangle);

                            }

                            SidePen.Dispose();
                        }
                    }
                }
            }
            catch
            {
            }

        }
    }
}
