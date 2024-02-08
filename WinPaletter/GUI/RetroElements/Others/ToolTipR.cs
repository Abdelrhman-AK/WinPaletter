using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    public class ToolTipR : ContainerControl
    {

        public ToolTipR()
        {
            AutoSize = false;
        }

        #region Variables

        Rectangle Rect;
        Rectangle RectText;

        #endregion

        #region Properties


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        protected override void OnFontChanged(EventArgs e)
        {
            SetStyle();

            base.OnFontChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetStyle();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnFace = false;
                CursorOnText = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnText = RectText.Contains(e.Location);
                CursorOnFace = Rect.Contains(e.Location) && !CursorOnText;

                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                if (CursorOnText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.InfoText)));
                else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.InfoWindow)));
            }

            base.OnClick(e);
        }

        #endregion

        #region Methods

        private void SetStyle()
        {
            SizeF TextSize = Text.Measure(Font) - new Size(10, 10);
            Size = new Size((int)TextSize.Width, (int)TextSize.Height) + new Size(10, 8);
            Rect = new(0, 0, Width - 1, Height - 1);

            RectText = new Rectangle(Rect.X + (Rect.Width - (int)TextSize.Width) / 2 + 1, Rect.Y + (Rect.Height - (int)TextSize.Height) / 2 + 1, (int)TextSize.Width, (int)TextSize.Height);
        }

        #endregion

        #region Colors editor

        private bool CursorOnFace, CursorOnText;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _ColorEdit_Text => EnableEditingColors && CursorOnText;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.Clear(BackColor);

            #region Editor

            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, Rect); }
            }

            #endregion

            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            using (SolidBrush br = new(ForeColor))
            {
                G.DrawString(Text, Font, br, new Rectangle(Rect.X, Rect.Y + 1, Rect.Width, Rect.Height), sf);
            }

            #region Editor

            if (_ColorEdit_Text)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, RectText);
                    G.DrawRectangle(P, RectText);
                }
            }

            #endregion

            G.DrawRectangle(Pens.Black, Rect);
        }
    }
}
