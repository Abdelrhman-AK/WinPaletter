using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [ToolboxItem(false)]
    public class ContextMenuStripRenderer : ToolStripRenderer
    {
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = Program.Style.DarkMode ? Color.White : Color.Black;
            base.OnRenderArrow(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                Graphics G = e.Graphics;
                G.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle itemRectangle = e.Item.ContentRectangle;
                itemRectangle.Height -= 1;

                G.FillRoundedRect(Program.Style.Schemes.Main.Brushes.Back_Checked_Hover, itemRectangle);
                G.DrawRoundedRectBeveled(Program.Style.Schemes.Main.Pens.Line_Checked_Hover, itemRectangle);
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text)) return;

            if (e.Item.Selected)
            {
                e.TextColor = Program.Style.Schemes.Main.Colors.ForeColor_Accent;
            }
            else
            {
                e.TextColor = Program.Style.DarkMode ? Color.White : Color.Black;
            }

            if (e.ToolStrip is ContextMenuStrip)
            {
                e.Item.AutoSize = false;

                int textHeight = ((ContextMenuStrip)e.ToolStrip).ItemHeight;
                Rectangle textRect = new(e.TextRectangle.Left, e.TextRectangle.Top, e.TextRectangle.Width, textHeight);
                e.TextRectangle = textRect;
                e.Item.Height = textHeight + 3;
            }

            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            if (!e.Item.Selected)
            {
                Graphics G = e.Graphics;
                G.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle itemRectangle = e.Item.ContentRectangle;

                G.FillRoundedRect(Program.Style.Schemes.Tertiary.Brushes.Back_Checked_Hover, itemRectangle);
                G.DrawRoundedRectBeveled(Program.Style.Schemes.Tertiary.Pens.Line_Checked_Hover, itemRectangle);
            }
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle rectangle = new(0, 0, e.Image.Width, e.Image.Height);
            Rectangle bounds = new(0, 0, Math.Min(e.Item.ContentRectangle.Width, e.Item.ContentRectangle.Height), Math.Min(e.Item.ContentRectangle.Width, e.Item.ContentRectangle.Height));
            rectangle.X = 2 + e.Item.ContentRectangle.X + (bounds.Width - rectangle.Width) / 2;
            rectangle.Y = e.Item.ContentRectangle.Y + (bounds.Height - rectangle.Height) / 2;

            e.Graphics.DrawImage(e.Image, rectangle);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle itemRectangle = e.Item.ContentRectangle;
            itemRectangle.Y += 1;

            using (Pen P = new(Program.Style.Schemes.Main.Colors.Line_Hover(e.ToolStrip.Level())))
            {
                G.DrawLine(P, itemRectangle.Location, itemRectangle.Location + new Size(e.Vertical ? 0 : itemRectangle.Width, e.Vertical ? itemRectangle.Height : 0));
            }

            base.OnRenderSeparator(e);
        }
    }

    public partial class ContextMenuStrip : System.Windows.Forms.ContextMenuStrip
    {
        public ContextMenuStrip()
        {
            AllowTransparency = true;
            AutoClose = true;
            DropShadowEnabled = true;

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Renderer = new ContextMenuStripRenderer();
        }

        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.6f));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int ItemHeight { get; set; } = 24;

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        private Bitmap BlurredBackground;
        private Bitmap Background;

        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            base.OnClosed(e);

            Background?.Dispose();
            Background = null;

            BlurredBackground?.Dispose();
            BlurredBackground = null;
        }

        protected override void OnOpening(CancelEventArgs e)
        {
            base.OnOpening(e);
            //CaptureFromScreen(null, Bounds.Location);

            Background?.Dispose();
            Background = GraphicsExtensions.CaptureFromScreen(Bounds);

            BlurredBackground?.Dispose();
            BlurredBackground = Background.Blur(8);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);
            Rectangle rect_fix = new(0, 0, Width, Height);

            if (Background != null)
            {
                G.DrawImage(Background, rect_fix);
            }

            if (BlurredBackground != null)
            {
                G.DrawRoundImage(BlurredBackground, rect);
                G.FillRectangle(Noise, rect);
            }

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            using (SolidBrush br = new(Color.FromArgb(180, scheme.Colors.Back(parentLevel))))
            using (Pen P = new(Color.FromArgb(128, 128, 128, 128)))
            {
                G.FillRoundedRect(br, rect);
                G.DrawRoundedRect(P, rect);
            }

            base.OnPaint(e);
        }
    }
}