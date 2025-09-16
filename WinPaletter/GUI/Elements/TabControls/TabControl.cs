﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Properties;
using WinPaletter.UI.Controllers;

namespace WinPaletter.UI.WP
{
    [Description("Themed TabControl for WinPaletter UI")]
    public class TabControl : System.Windows.Forms.TabControl
    {
        public TabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            ItemSize = new(35, 140);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            SizeMode = TabSizeMode.Fixed;
            Font = new("Segoe UI", 9f);
        }

        #region Variables
        private readonly static TextureBrush Noise = new(Resources.Noise.Fade(0.6f));
        #endregion

        #region Events/Overrides

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            // Set an initial effect so the cursor updates as soon as the drag enters.
            e.Effect = e.Data.GetDataPresent(typeof(ColorItem)) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            if (!e.Data.GetDataPresent(typeof(ColorItem)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // Always set the allowed effect for a valid data type (so cursor updates).
            e.Effect = DragDropEffects.Copy;

            if (TabCount == 0) return;

            // e.X/e.Y are screen coords — convert to client coords for GetTabRect.
            var clientPoint = PointToClient(new Point(e.X, e.Y));

            for (int i = 0; i < TabCount; i++)
            {
                if (i == SelectedIndex) continue;

                var tabRect = GetTabRect(i); // bounding rectangle for that tab button.
                if (tabRect.Contains(clientPoint))
                {
                    SelectedIndex = i;    // only change when different
                                          // If you need immediate repaint of custom-drawn areas, call Invalidate() here.
                    break;
                }
            }
        }

        protected override void CreateHandle()
        {
            int X1 = ItemSize.Width;
            int X2 = ItemSize.Height;

            if (Alignment == TabAlignment.Top | Alignment == TabAlignment.Bottom)
            {
                if (X1 >= X2)
                {
                    ItemSize = new(X1, X2);
                }
                else
                {
                    ItemSize = new(X2, X1);
                }
            }
            else if (X2 >= X1)
            {
                ItemSize = new(X1, X2);
            }
            else
            {
                ItemSize = new(X2, X1);
            }

            base.CreateHandle();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            G.Clear(this.GetParentColor());

            Color SelectedColor = scheme.Colors.Back_Checked;
            Color SelectedColor2 = scheme.Colors.Back_Checked_Hover;
            Color SideTabeColor = scheme.Colors.ForeColor_Accent;
            bool RTL = (int)RightToLeft == 1;
            Image img = null;

            for (int i = 0; i <= TabPages.Count - 1; i++)
            {
                Color TextColor = ForeColor;

                int SideTapeW = 3;
                int SideTapeH = (int)G.MeasureString(TabPages[i].Text, Font).Width;
                Rectangle TabRect = GetTabRect(i);
                Rectangle SideTape = new(TabRect.X + (TabRect.Width - SideTapeH) / 2, TabRect.Y, SideTapeH, SideTapeW);

                if (Alignment == TabAlignment.Right | Alignment == TabAlignment.Left)
                {
                    SideTapeH = (int)Math.Round(TabRect.Height * 0.45d);
                    SideTape = new(!RTL ? TabRect.X + 4 : TabRect.X + TabRect.Width - 6, (int)Math.Round(TabRect.Y + (TabRect.Height - SideTapeH) / 2d), SideTapeW, SideTapeH);
                }
                else if (Alignment == TabAlignment.Top)
                {
                    SideTape = new(TabRect.X + (TabRect.Width - SideTapeH) / 2, TabRect.Y + TabRect.Height - SideTapeW - 1, SideTapeH, SideTapeW);
                }

                if (ImageList is not null && i <= ImageList.Images.Count - 1)
                {
                    img = ImageList.Images[i];
                }

                if (i == SelectedIndex)
                {
                    using (LinearGradientBrush br = new(TabRect, SelectedColor, SelectedColor2, LinearGradientMode.ForwardDiagonal))
                    {
                        G.FillRoundedRect(br, TabRect);
                    }

                    G.FillRoundedRect(Noise, TabRect);

                    using (SolidBrush br = new(SideTabeColor)) { G.FillRoundedRect(br, SideTape, 2); }

                    using (Pen p = new(SelectedColor)) { G.DrawRoundedRectBeveled(p, TabRect); }

                    TextColor = SelectedColor.IsDark() ? Color.White : Color.Black;
                }

                if (img != null)
                {
                    Rectangle imgRect;

                    if (!RTL)
                    {
                        imgRect = new(TabRect.X + 10, (int)Math.Round(TabRect.Y + (TabRect.Height - img.Height) / 2d) + 1, img.Width, img.Height);
                    }
                    else
                    {
                        imgRect = new(TabRect.X + TabRect.Width - img.Width - 8, (int)Math.Round(TabRect.Y + (TabRect.Height - img.Height) / 2d) + 1, img.Width, img.Height);
                    }

                    if (Alignment == TabAlignment.Right | Alignment == TabAlignment.Left)
                    {
                        int xPoint = !RTL ? imgRect.X + imgRect.Width + 5 : TabRect.X;
                        int Fixer = imgRect.Width + 15;

                        Rectangle tr = new(xPoint, TabRect.Y, TabRect.Width - Fixer, TabRect.Height);

                        using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat(RTL))
                        {
                            using (SolidBrush br = new(TextColor))
                            {
                                G.DrawString(TabPages[i].Text, Font, br, tr, sf);
                            }
                        }
                    }
                    else
                    {
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat(RTL))
                        {
                            using (SolidBrush br = new(TextColor))
                            {
                                G.DrawString(TabPages[i].Text, Font, br, TabRect, sf);
                            }
                        }
                    }

                    if (RTL) img.RotateFlip(RotateFlipType.Rotate180FlipY);
                    G.DrawImage(img, imgRect);
                }

                else
                {
                    if (Alignment == TabAlignment.Right | Alignment == TabAlignment.Left)
                    {
                        using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat(RTL))
                        {
                            using (SolidBrush br = new(TextColor))
                            {
                                int LTFixer0 = !RTL ? SideTape.Width + 7 : 0;
                                int LTFixer1 = SideTape.Width + 7;

                                int xPoint = TabRect.X + LTFixer0;

                                Rectangle tr = new(xPoint, TabRect.Y + 1, TabRect.Width - LTFixer1, TabRect.Height);

                                G.DrawString(TabPages[i].Text, Font, br, tr, sf);
                            }
                        }
                    }
                    else
                    {
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat(RTL))
                        {
                            using (SolidBrush br = new(TextColor))
                            {
                                G.DrawString(TabPages[i].Text, Font, br, TabRect, sf);
                            }
                        }
                    }
                }
            }

            base.OnPaint(e);


        }
    }
}