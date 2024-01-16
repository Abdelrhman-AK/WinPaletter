using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    public partial class LabelR : Label
    {
        public LabelR()
        {
            DoubleBuffered = true;
            SetStyle();
        }

        #region Variables

        Rectangle rect;
        Rectangle itemText;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                if (CursorOnText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.ButtonText)));
            }

            base.OnClick(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            if (IsHandleCreated) SetStyle();

            base.OnFontChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            SetStyle();

            base.OnResize(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnText = itemText.Contains(e.Location);
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnText = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        #endregion

        #region Methods

        private void SetStyle()
        {
            rect = new Rectangle(0, 0, Width - 1, Height - 1);

            SizeF item0Size = Text.Measure(Font);

            if (AutoSize)
            {
                itemText = new Rectangle((Width / 2) - ((int)item0Size.Width / 2), (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1);
            }
            else
            {
                switch (TextAlign)
                {
                    case ContentAlignment.TopLeft:
                        itemText = new Rectangle(0, 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.TopCenter:
                        itemText = new Rectangle((Width / 2) - ((int)item0Size.Width / 2), 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.TopRight:
                        itemText = new Rectangle(Width - (int)item0Size.Width, 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.MiddleLeft:
                        itemText = new Rectangle(0, (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.MiddleCenter:
                        itemText = new Rectangle((Width / 2) - ((int)item0Size.Width / 2), (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.MiddleRight:
                        itemText = new Rectangle(Width - (int)item0Size.Width, (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.BottomLeft:
                        itemText = new Rectangle(0, Height - (int)item0Size.Height, (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.BottomCenter:
                        itemText = new Rectangle((Width / 2) - ((int)item0Size.Width / 2), Height - (int)item0Size.Height, (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    case ContentAlignment.BottomRight:
                        itemText = new Rectangle(Width - (int)item0Size.Width, Height - (int)item0Size.Height, (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;

                    default:
                        itemText = new Rectangle(0, 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1);
                        break;
                }
            }
        }

        #endregion

        #region Colors editor

        private bool CursorOnText;
        private bool _ColorEdit_Text => EnableEditingColors && CursorOnText;

        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Rectangle rectangle = new(0, 0, Width - 1, Height - 1);

            if (_ColorEdit_Text)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, itemText);
                    G.DrawRectangle(P, itemText);
                }
            }

            using (StringFormat sf = TextAlign.ToStringFormat())
            using (SolidBrush br = new(ForeColor)) { G.DrawString(Text, Font, br, rectangle, sf); }
        }
    }
}
