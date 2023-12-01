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
                Text = string.Empty;

            using (Bitmap b = new(w, h))
            using (GraphicsPath gp = new())
            using (Graphics gx = Graphics.FromImage(b))
            using (Matrix m = new(1.0f / 5f, 0f, 0f, 1.0f / 5f, -(1.0f / 5f), -(1.0f / 5f)))
            using (SolidBrush br = new(ForeColor))
            using (Pen P = new(GlowColor, GlowSize))
            {
                gp.AddString(Text, Font.FontFamily, (int)Font.Style, emSize, Rect, FormatX);

                gx.SmoothingMode = SmoothingMode.AntiAlias;
                gx.Transform = m;

                gx.DrawPath(P, gp);
                gx.FillPath(P.Brush, gp);

                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.DrawImage(b, ClientRect, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel);

                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.DrawString(Text, Font, br, Rect, FormatX);
            }
        }

        public static void DrawGlow(this Graphics G, Rectangle R, Color GlowColor, int GlowSize = 5, int GlowFade = 7)
        {
            try
            {
                if (GlowSize <= 0) GlowSize = 1;
                if (GlowFade <= 0) GlowFade = 1;

                Rectangle Rect = new(R.X - GlowSize - 2, R.Y - GlowSize - 2, R.Width + GlowSize * 2 + 3, R.Height + GlowSize * 2 + 3);

                using (Bitmap bm = new((int)Math.Round(Rect.Width / (double)GlowFade), (int)Math.Round(Rect.Height / (double)GlowFade)))
                {
                    using (Graphics G2 = Graphics.FromImage(bm))
                    {
                        Rectangle Rect2 = new(1, 1, bm.Width, bm.Height);

                        using (SolidBrush br = new(GlowColor))
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
                if (BackgroundBlurred != null)
                    G.DrawRoundImage(BackgroundBlurred, Rect, Radius, true);

                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * 255), Color.Black)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * (ColorBalance * 255)), Color1)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }

                Color C1 = Color.FromArgb((int)Math.Round(ColorBalance * 255), Color1);
                Color C2 = Color.FromArgb((int)Math.Round(GlowBalance * 255), Color2);

                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 100)), Color2)))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 150)), C1.Blend(C2, 100d))))
                {
                    G.FillRoundedRect(br, Rect, Radius, true);
                }
            }
            else
            {
                if (BackgroundBlurred is not null)
                    G.DrawImage(BackgroundBlurred, Rect);

                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * 255), Color.Black)))
                {
                    G.FillRectangle(br, Rect);
                }
                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * (ColorBalance * 255)), Color1)))
                {
                    G.FillRectangle(br, Rect);
                }

                Color C1 = Color.FromArgb((int)Math.Round(ColorBalance * 255), Color1);
                Color C2 = Color.FromArgb((int)Math.Round(GlowBalance * 255), Color2);

                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 100)), Color2)))
                {
                    G.FillRectangle(br, Rect);
                }
                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(alpha * (GlowBalance * 150)), C1.Blend(C2, 100d))))
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
                    Radius = Program.Style.Radius;

                if (Graphics is null)
                    return;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if ((Program.Style.RoundedCorners || ForcedRoundCorner) && Radius > 0)
                {
                    using (GraphicsPath path = Rectangle.Round(Radius))
                    {
                        Graphics.FillPath(Brush, path);
                    }
                }
                else
                {
                    Graphics.FillRectangle(Brush, Rectangle);
                }
            }
            catch { }
        }

        public static void DrawRoundImage(this Graphics Graphics, Image Image, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = Program.Style.Radius;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                if ((Program.Style.RoundedCorners | ForcedRoundCorner) & Radius > 0)
                {
                    using (GraphicsPath path = Rectangle.Round(Radius))
                    {
                        Region reg = new(path);
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

        public static GraphicsPath Round(this Rectangle r, int Radius = -1)
        {
            try
            {
                Radius = Radius == -1 ? Program.Style.Radius * 2 : Radius *= 2;

                GraphicsPath path = new();

                path.AddLine(r.Left + Radius, r.Top, r.Right - Radius, r.Top);
                path.AddArc(Rectangle.FromLTRB(r.Right - Radius, r.Top, r.Right, r.Top + Radius), -90, 90f);

                path.AddLine(r.Right, r.Top + Radius, r.Right, r.Bottom - Radius);
                path.AddArc(Rectangle.FromLTRB(r.Right - Radius, r.Bottom - Radius, r.Right, r.Bottom), 0f, 90f);

                path.AddLine(r.Right - Radius, r.Bottom, r.Left + Radius, r.Bottom);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - Radius, r.Left + Radius, r.Bottom), 90f, 90f);

                path.AddLine(r.Left, r.Bottom - Radius, r.Left, r.Top + Radius);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + Radius, r.Top + Radius), 180f, 90f);

                path.CloseFigure();
                return path;
            }
            catch
            {
                return null;
            }
        }

        public static void DrawRoundedRect(this Graphics Graphics, Pen Pen, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = Program.Style.Radius;

                Radius *= 2;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if ((Program.Style.RoundedCorners | ForcedRoundCorner) & Radius > 0)
                {
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                    Graphics.DrawLine(Pen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2d));
                    Graphics.DrawLine(Pen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2d));
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);
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
                    Radius = Program.Style.Radius;

                Radius *= 2;

                bool Rounded = (Program.Style.RoundedCorners || ForcedRoundCorner) && Radius > 0;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen Pen = new(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                using (Pen Pen2 = new(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                {
                    Pen SidePen = new(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset };

                    if (Dark)
                    {
                        Pen.Color = PenX.Color.CB(0.06f);
                        Pen2.Color = PenX.Color;
                    }
                    else
                    {
                        Pen.Color = PenX.Color;
                        Pen2.Color = PenX.Color.CB(-0.12f);
                    }

                    LinearGradientBrush G;
                    Color CColor = Pen2.Color.CB((float)(Dark ? 0.03d : -0.05d));

                    if (Dark)
                    {
                        G = new(Rectangle, CColor, Pen.Color, 180f);
                        ColorBlend cblend = new(5)
                        {
                            Colors = new Color[5] { CColor, Pen.Color, Pen.Color, Pen.Color, CColor },
                            Positions = Rounded ?
                            new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f } :
                            new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f }
                        };
                        G.InterpolationColors = cblend;
                    }
                    else
                    {
                        G = new(Rectangle, Pen.Color, CColor, 180f);
                        ColorBlend cblend = new(5)
                        {
                            Colors = new Color[5] { Pen.Color, CColor, CColor, CColor, Pen.Color },
                            Positions = new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f }
                        };
                        G.InterpolationColors = cblend;
                    }

                    using (Pen PenG = new(G, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                    {

                        Point P_T1 = new((int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y);
                        Point P_T2 = new((int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);

                        Point P_B1 = new((int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height);
                        Point P_B2 = new((int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);

                        Point P_LB = new(Rectangle.X, Rectangle.Y + Rectangle.Height);
                        Point P_RB = new(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);

                        Point P_LT = new(Rectangle.X, Rectangle.Y);
                        Point P_RT = new(Rectangle.X + Rectangle.Width, Rectangle.Y);

                        Point P_L1 = new(Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d));
                        Point P_L2 = new(Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                        Point P_R1 = new(Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d));
                        Point P_R2 = new(Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                        if (Dark)
                        {
                            Graphics.DrawLine(Pen2, P_B1, P_B2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(Pen2, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                Graphics.DrawArc(Pen2, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen2, P_B1, P_LB);
                                Graphics.DrawLine(Pen2, P_LB, P_L1);

                                Graphics.DrawLine(Pen2, P_B2, P_RB);
                                Graphics.DrawLine(Pen2, P_RB, P_R1);
                            }

                            SidePen = Pen2;

                            Graphics.DrawLine(SidePen, P_L1, P_L2);

                            Graphics.DrawLine(SidePen, P_R1, P_R2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen, P_T1, P_LT);
                                Graphics.DrawLine(SidePen, P_LT, P_L1);

                                Graphics.DrawLine(Pen, P_T2, P_RT);
                                Graphics.DrawLine(SidePen, P_RT, P_R1);
                            }

                            Graphics.DrawLine(PenG, P_T1, P_T2);
                        }

                        else
                        {
                            Graphics.DrawLine(Pen2, P_B1, P_B2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen2, P_B1, P_LB);
                                Graphics.DrawLine(SidePen, P_LB, P_L1);

                                Graphics.DrawLine(Pen2, P_B2, P_RB);
                                Graphics.DrawLine(SidePen, P_RB, P_R1);
                            }

                            SidePen = Pen;

                            Graphics.DrawLine(SidePen, P_L1, P_L2);

                            Graphics.DrawLine(SidePen, P_R1, P_R2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen, P_T1, P_LT);
                                Graphics.DrawLine(Pen, P_LT, P_L1);

                                Graphics.DrawLine(Pen, P_T2, P_RT);
                                Graphics.DrawLine(Pen, P_RT, P_R1);
                            }

                            Graphics.DrawLine(Pen, P_T1, P_T2);
                        }
                    }

                    SidePen.Dispose();
                }
            }
            catch
            {
            }
        }
    }
}
