using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed TabControl for WinPaletter UI")]
    public class TabControl : System.Windows.Forms.TabControl
    {

        public TabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            ItemSize = new Size(40, 150);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            SizeMode = TabSizeMode.Fixed;
            Font = new Font("Segoe UI", 9f);
        }

        #region Variables

        private readonly TextureBrush Noise = new TextureBrush(Properties.Resources.GaussianBlur.Fade(0.4d));

        #endregion

        #region Properties

        public Color LineColor { get; set; } = Color.FromArgb(0, 81, 210);

        #endregion

        #region Events

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Controllers.ColorItem).FullName) is Controllers.ColorItem)
            {
                e.Effect = DragDropEffects.None;
                for (int i = 0, loopTo = TabCount - 1; i <= loopTo; i++)
                {
                    if (!(SelectedIndex == i) && GetTabRect(i).Contains(PointToClient(MousePosition)))
                    {
                        SelectedIndex = i;
                        Invalidate();
                    }
                }
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            int X1 = ItemSize.Width;
            int X2 = ItemSize.Height;

            if (Alignment == TabAlignment.Top | Alignment == TabAlignment.Bottom)
            {
                if (X1 >= X2)
                {
                    ItemSize = new Size(X1, X2);
                }
                else
                {
                    ItemSize = new Size(X2, X1);
                }
            }
            else if (X2 >= X1)
            {
                ItemSize = new Size(X1, X2);
            }
            else
            {
                ItemSize = new Size(X2, X1);
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            DoubleBuffered = true;

            Color SelectColor;
            Color TextColor;
            var ParentColor = this.GetParentColor();
            bool RTL = (int)RightToLeft == 1;
            Image img = null;

            G.Clear(ParentColor);
            bool Dark = Program.Style.DarkMode;

            var imgRect = default(Rectangle);
            for (int i = 0, loopTo = TabCount - 1; i <= loopTo; i++)
            {
                var TabRect = GetTabRect(i);
                int SideTapeH = (int)Math.Round(TabRect.Height * 0.5d);
                int SideTapeW = 3;
                Rectangle SideTape;

                if (Alignment == TabAlignment.Right | Alignment == TabAlignment.Left)
                {
                    SideTape = new Rectangle(TabRect.X + 1, (int)Math.Round(TabRect.Y + (TabRect.Height - SideTapeH) / 2d), SideTapeW, SideTapeH);
                }
                else if (Alignment == TabAlignment.Top)
                {
                    SideTape = new Rectangle((int)Math.Round(TabRect.X + TabRect.Width * 0.125d), TabRect.Y + TabRect.Height - SideTapeW - 1, (int)Math.Round(TabRect.Width * 0.75d), SideTapeW);
                }
                else
                {
                    SideTape = new Rectangle(TabRect.X, TabRect.Y, TabRect.Width, SideTapeW);
                }

                try
                {
                    if (ImageList is not null)
                    {
                        var ls = ImageList;
                        img = ls.Images[i];
                        SelectColor = img.AverageColor();
                        SelectColor = SelectColor.Light(0.5f);
                    }
                    else
                    {
                        SelectColor = Dark ? LineColor : LineColor.LightLight();
                    }
                }
                catch
                {
                    SelectColor = Dark ? LineColor : LineColor.LightLight();
                }

                if (i == SelectedIndex)
                {
                    using (var br = new SolidBrush(ParentColor.CB((float)(Dark ? 0.08d : -0.08d))))
                    {
                        G.FillRoundedRect(br, TabRect);
                    }
                    G.FillRoundedRect(Noise, TabRect);
                    using (var br = new SolidBrush(SelectColor))
                    {
                        G.FillRoundedRect(br, SideTape, 2);
                    }
                }

                TextColor = Dark ? Color.White : Color.Black;

                try
                {
                    if (!DesignMode)
                        TabPages[i].BackColor = ParentColor;
                }
                catch
                {
                }

                if (img is not null)
                {
                    imgRect = new Rectangle(TabRect.X + 10, (int)Math.Round(TabRect.Y + (TabRect.Height - img.Height) / 2d), img.Width, img.Height);
                    if (RTL)
                        img.RotateFlip(RotateFlipType.Rotate180FlipY);
                    G.DrawImage(img, imgRect);
                }

                if (img is not null & (Alignment == TabAlignment.Right | Alignment == TabAlignment.Left))
                {
                    if (!RTL)
                    {
                        var tr = new Rectangle(imgRect.Right + 10, TabRect.Y, TabRect.Width - imgRect.Width - 10, TabRect.Height);
                        using (var br = new SolidBrush(TextColor))
                        {
                            G.DrawString(TabPages[i].Text, Font, br, tr, ContentAlignment.MiddleLeft.ToStringFormat());
                        }
                    }
                    else
                    {
                        var b = new Bitmap(TabRect.Width, TabRect.Height);
                        var gx = Graphics.FromImage(b);
                        gx.SmoothingMode = G.SmoothingMode;
                        gx.TextRenderingHint = G.TextRenderingHint;
                        using (var br = new SolidBrush(TextColor))
                        {
                            gx.DrawString(TabPages[i].Text, Font, br, new Rectangle(0, 0, b.Width - imgRect.Right - 10, b.Height - 1), ContentAlignment.MiddleLeft.ToStringFormat(RTL));
                        }
                        gx.Flush();
                        b.RotateFlip(RotateFlipType.Rotate180FlipY);
                        G.DrawImage(b, TabRect);
                        gx.Dispose();
                        b.Dispose();
                    }
                }

                else if (!RTL)
                {
                    if (Alignment == TabAlignment.Right | Alignment == TabAlignment.Left)
                    {
                        using (var br = new SolidBrush(TextColor))
                        {
                            G.DrawString(TabPages[i].Text, Font, br, new Rectangle(TabRect.X + SideTape.Right + 2, TabRect.Y + 1, TabRect.Width - SideTape.Right - 2, TabRect.Height), ContentAlignment.MiddleLeft.ToStringFormat());
                        }
                    }
                    else
                    {
                        using (var br = new SolidBrush(TextColor))
                        {
                            G.DrawString(TabPages[i].Text, Font, br, TabRect, ContentAlignment.MiddleCenter.ToStringFormat());
                        }
                    }
                }
                else
                {
                    var b = new Bitmap(TabRect.Width, TabRect.Height);
                    var gx = Graphics.FromImage(b);
                    gx.SmoothingMode = G.SmoothingMode;
                    gx.TextRenderingHint = G.TextRenderingHint;
                    using (var br = new SolidBrush(TextColor))
                    {
                        gx.DrawString(TabPages[i].Text, Font, br, new Rectangle(0, 0, b.Width - 1, b.Height - 1), ContentAlignment.MiddleCenter.ToStringFormat(RTL));
                    }
                    gx.Flush();
                    b.RotateFlip(RotateFlipType.Rotate180FlipY);
                    G.DrawImage(b, TabRect);
                    gx.Dispose();
                    b.Dispose();
                }

            }
        }

    }

}