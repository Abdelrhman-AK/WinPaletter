using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [ToolboxItem(false)]
    public class ContextMenuStripRenderer : ToolStripRenderer
    {
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Config.Scheme scheme = e.ToolStrip.Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Rectangle rect = new(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);

            G.FillRectangle(scheme.Brushes.Back, rect);
            G.DrawRectangle(scheme.Pens.Line_Hover, rect);

            base.OnRenderToolStripBackground(e);
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
                G.DrawRoundedRect_LikeW11(Program.Style.Schemes.Main.Pens.Line_Checked_Hover, itemRectangle);
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
                G.DrawRoundedRect_LikeW11(Program.Style.Schemes.Tertiary.Pens.Line_Checked_Hover, itemRectangle);
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

            G.DrawLine(Program.Style.Schemes.Main.Pens.Line_Hover, itemRectangle.Location, itemRectangle.Location + new Size(e.Vertical ? 0 : itemRectangle.Width, e.Vertical ? itemRectangle.Height : 0));

            base.OnRenderSeparator(e);
        }
    }

    public partial class ContextMenuStrip : System.Windows.Forms.ContextMenuStrip
    {
        public ContextMenuStrip()
        {
            this.AllowTransparency = true;
            this.AutoClose = true;
            this.DropShadowEnabled = false;

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Renderer = new ContextMenuStripRenderer();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int ItemHeight { get; set; } = 25;

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            G.FillRectangle(scheme.Brushes.Back, rect);
            G.DrawRectangle(scheme.Pens.Line_Hover, rect);

            base.OnPaint(e);
        }
    }
}
