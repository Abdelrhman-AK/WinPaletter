using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed toggle for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class Toggle : UserControl
    {

        public Toggle()
        {
            DoubleBuffered = true;
            Size = new Size(40, 20);
            Text = string.Empty;
            Resize += Toggle_Resize;
            HandleCreated += Toggle_HandleCreated;
            HandleDestroyed += Toggle_HandleDestroyed;
            MouseMove += Toggle_MouseMove;
            MouseUp += Toggle_MouseUp;
            MouseDown += Toggle_MouseDown;
        }

        #region Variables
        private Rectangle CheckC = new Rectangle(4, 4, 11, 11);
        private Rectangle CheckedC;
        private int MouseState = 0;
        private bool WasMoving = false;
        private readonly int DarkLight_TogglerSize = 13;
        private bool _Shown = false;
        private bool AnimateOnClick = false;
        #endregion

        #region Properties
        public bool DarkLight_Toggler { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        private bool _checked;
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (!_checked.Equals(value))
                {
                    _checked = value;
                    OnCheckedChanged();
                }
            }
        }
        #endregion

        #region Events
        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

        protected virtual void OnCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);

            if (!DesignMode & _Shown & AnimateOnClick)
            {
                if (Checked)
                {

                    int s = (int)Math.Round((Width - 17) * 0.5d);
                    for (int i = CheckC.Left, loopTo = Width - 17; i <= loopTo; i += +5)
                    {
                        CheckC.X = i + s;
                        System.Threading.Thread.Sleep(1);
                        Refresh();
                        if (i + s >= Width - 17)
                            break;
                        s -= 1;
                        if (s < 0)
                            s = 0;
                    }
                    CheckC.X = Width - 17;
                }

                else
                {

                    int s = 10;
                    for (int i = CheckC.Left; i >= 4; i -= 5)
                    {
                        CheckC.X = i - s;
                        System.Threading.Thread.Sleep(1);
                        Refresh();
                        if (i - s <= 4)
                            break;
                        s -= 1;
                        if (s < 0)
                            s = 0;
                    }
                    CheckC.X = 4;

                }
            }

            else if (Checked)
            {
                CheckC.X = Width - 17;
            }
            else
            {
                CheckC.X = 4;
            }

            if (DarkLight_Toggler)
            {
                CheckC.Width = DarkLight_TogglerSize;
                CheckC.Height = DarkLight_TogglerSize;
            }

            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            AnimateOnClick = true;
            Checked = !Checked;
            AnimateOnClick = false;
            Invalidate();
            base.OnMouseClick(e);
        }

        private void Toggle_Resize(object sender, EventArgs e)
        {
            Height = 20;
            if (Width < 40)
                Width = 40;

            if (DarkLight_Toggler)
            {
                CheckC.Width = DarkLight_TogglerSize;
                CheckC.Height = DarkLight_TogglerSize;
            }

            Refresh();
        }

        private void Toggle_HandleCreated(object sender, EventArgs e)
        {

            if (Checked)
            {
                CheckC = new Rectangle(Width - 17, 4, 11, 11);
            }
            else
            {
                CheckC = new Rectangle(4, 4, 11, 11);
            }

            if (DarkLight_Toggler)
            {
                CheckC.Width = DarkLight_TogglerSize;
                CheckC.Height = DarkLight_TogglerSize;
            }

            if (!DesignMode)
            {
                try
                {
                    FindForm().Load += Loaded;
                    FindForm().Shown += Showed;
                    Parent.BackColorChanged += RefreshColorPalette;
                }
                catch
                {
                }
            }
        }

        private void Toggle_HandleDestroyed(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    FindForm().Load -= Loaded;
                    FindForm().Shown -= Showed;
                    Parent.BackColorChanged -= RefreshColorPalette;
                }
                catch
                {
                }
            }
        }

        public void Loaded(object sender, EventArgs e)
        {
            _Shown = false;
        }

        public void Showed(object sender, EventArgs e)
        {
            _Shown = true;
            Invalidate();
        }

        public void RefreshColorPalette(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Invalidate();
            }
        }

        private void Toggle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                int i = (int)Math.Round(e.X - 0.5d * CheckC.Width);
                WasMoving = true;
                MouseState = 1;

                if (i <= 4)
                {
                    CheckC.X = 4;
                }
                else if (i >= Width - 17)
                {
                    CheckC.X = Width - 17;
                }
                else
                {
                    CheckC.X = i;
                }

                CheckC.Y = 4;
                Refresh();
            }
        }

        private void Toggle_MouseUp(object sender, MouseEventArgs e)
        {
            MouseState = 0;
            CheckC.Width = 11;

            if (DarkLight_Toggler)
            {
                CheckC.Width = DarkLight_TogglerSize;
                CheckC.Height = DarkLight_TogglerSize;
            }

            if (WasMoving)
            {
                if (e.X < Width * 0.5d)
                    Checked = false;
                if (e.X >= Width * 0.5d)
                    Checked = true;
                WasMoving = false;
            }
            Refresh();
        }

        private void Toggle_MouseDown(object sender, MouseEventArgs e)
        {
            MouseState = 1;
            CheckC.Width = 13;

            Refresh();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            OnPaintBackground(e);
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            DoubleBuffered = true;

            if (Parent is null)
                return;

            BackColor = Program.Style.Schemes.Main.Colors.Back;

            G.Clear(this.GetParentColor());

            // ################################################################################# Customizer
            var MainRect = new Rectangle(0, 0, Width - 1, Height - 1);
            Color BorderColor;

            if (Program.Style.DarkMode)
                BorderColor = base.BackColor.LightLight();
            else
                BorderColor = base.BackColor.Dark(0.5f);

            Color CheckColor;
            if (MouseState == 0)
                CheckColor = Program.Style.Schemes.Main.Colors.AccentAlt;
            else
                CheckColor = base.BackColor.CB((float)(Program.Style.DarkMode ? 0.3d : -0.5d));

            // #################################################################################
            int min = 4;
            int max = Width - 17;
            decimal val = (decimal)(CheckC.X / (double)max);

            if (val < 0m)
                val = 0m;
            if (val > 1m)
                val = 1m;
            if (CheckC.X <= min)
                val = 0m;
            if (CheckC.X >= max)
                val = 1m;

            LinearGradientBrush lgbChecked, lgbNonChecked, lgborderChecked, lgborderNonChecked;

            lgbChecked = new LinearGradientBrush(MainRect, Color.FromArgb((int)Math.Round(255m * val), Program.Style.Schemes.Main.Colors.Line_CheckedHover), Color.FromArgb((int)Math.Round(255m * val), Program.Style.Schemes.Main.Colors.Back_Checked), LinearGradientMode.ForwardDiagonal);
            lgborderChecked = new LinearGradientBrush(MainRect, Color.FromArgb((int)Math.Round(255m * val), Program.Style.Schemes.Main.Colors.Line_Checked), Color.FromArgb((int)Math.Round(255m * val), Program.Style.Schemes.Main.Colors.Back_Checked), LinearGradientMode.BackwardDiagonal);
            lgbNonChecked = new LinearGradientBrush(MainRect, Color.FromArgb((int)Math.Round(255m * (1m - val)), Program.Style.Schemes.Main.Colors.Back_Checked), Color.FromArgb((int)Math.Round(255m * (1m - val)), Program.Style.Schemes.Main.Colors.Line_CheckedHover), LinearGradientMode.BackwardDiagonal);
            lgborderNonChecked = new LinearGradientBrush(MainRect, Color.FromArgb((int)Math.Round(255m * (1m - val)), Program.Style.Schemes.Main.Colors.Line_Checked), Color.FromArgb((int)Math.Round(255m * (1m - val)), Program.Style.Schemes.Main.Colors.Back_Checked), LinearGradientMode.ForwardDiagonal);

            if (!DarkLight_Toggler)
            {

                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(255m * val), Program.Style.Schemes.Main.Colors.Line_CheckedHover)))
                {
                    e.Graphics.FillRoundedRect(br, MainRect, 9, true);
                }

                using (var P = new Pen(Color.FromArgb((int)Math.Round(255m * val), Program.Style.Schemes.Main.Colors.Line_Checked)))
                {
                    e.Graphics.DrawRoundedRect(P, MainRect, 9, true);
                }

                using (var P = new Pen(Color.FromArgb((int)Math.Round(255m * (1m - val)), BorderColor)))
                {
                    e.Graphics.DrawRoundedRect(P, MainRect, 9, true);
                }

                if (Checked)
                {
                    using (var br = new SolidBrush(Program.Style.DarkMode ? Color.Black : Color.White))
                    {
                        G.FillEllipse(br, CheckC);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(BorderColor))
                    {
                        G.FillEllipse(br, CheckC);
                    }
                }
            }

            else
            {

                e.Graphics.FillRoundedRect(lgbChecked, MainRect, 9, true);
                e.Graphics.FillRoundedRect(lgbNonChecked, MainRect, 9, true);

                if (Checked)
                {
                    G.DrawImage((BorderColor.IsDark() ? Properties.Resources.darkmode_dark : Properties.Resources.darkmode_light).Fade((double)val), CheckC);
                    G.DrawImage((BorderColor.IsDark() ? Properties.Resources.lightmode_dark : Properties.Resources.lightmode_light).Fade((double)(1m - val)), CheckC);
                }
                else
                {
                    G.DrawImage((Program.Style.Schemes.Main.Colors.AccentAlt.IsDark() ? Properties.Resources.darkmode_dark : Properties.Resources.darkmode_light).Fade((double)val), CheckC);
                    G.DrawImage((Program.Style.Schemes.Main.Colors.AccentAlt.IsDark() ? Properties.Resources.lightmode_dark : Properties.Resources.lightmode_light).Fade((double)(1m - val)), CheckC);
                }

                using (var P = new Pen(lgborderChecked))
                {
                    e.Graphics.DrawRoundedRect(P, MainRect, 9, true);
                }
                using (var P = new Pen(lgborderNonChecked))
                {
                    e.Graphics.DrawRoundedRect(P, MainRect, 9, true);
                }
            }
        }

    }

}