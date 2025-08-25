using FluentTransitions;
using Microsoft.VisualBasic;
using Serilog.Sinks.File;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    public partial class SubMenu
    {
        private Color _overrideColor;
        private bool _eventDone;
        private readonly float _dark = 0.7f;
        private Color PreviousClr;

        public SubMenu()
        {
            InitializeComponent();
        }

        public Color ShowMenu(ColorItem ColorItem, bool EnableDelete = false)
        {
            Button5.Visible = EnableDelete;

            MainColor.BackColor = ColorItem.BackColor.CB((float)((trackBarX1.Value - 100) / 100d));
            DefaultColor.BackColor = ColorItem.DefaultBackColor.CB((float)((trackBarX2.Value - 100) / 100d));
            InvertedColor.BackColor = ColorItem.BackColor.Invert().CB((float)((trackBarX4.Value - 100) / 100d));

            MainColor.DefaultBackColor = ColorItem.BackColor;
            DefaultColor.DefaultBackColor = ColorItem.DefaultBackColor;
            InvertedColor.DefaultBackColor = ColorItem.BackColor.Invert();

            if (ColorItem.ColorsHistory.Count > 1)
            {
                PreviousColor.BackColor = ColorItem.ColorsHistory[ColorItem.ColorsHistory.Count - 2].CB((float)((trackBarX3.Value - 100) / 100d));
                PreviousColor.DefaultBackColor = ColorItem.ColorsHistory[ColorItem.ColorsHistory.Count - 2];
            }
            else
            {
                PreviousColor.BackColor = ColorItem.BackColor.CB((float)((trackBarX3.Value - 100) / 100d));
                PreviousColor.DefaultBackColor = ColorItem.BackColor;
            }

            PreviousClr = PreviousColor.DefaultBackColor;

            GetHistoryColors(ColorItem);

            if (ShowDialog() == DialogResult.OK)
            {
                switch (ColorClipboard.Event)
                {
                    case ColorClipboard.MenuEvent.Copy:
                        {
                            ColorClipboard.CopiedColor = MainColor.BackColor;
                            return ColorItem.BackColor;
                        }

                    case ColorClipboard.MenuEvent.Cut:
                        {
                            ColorClipboard.CopiedColor = MainColor.BackColor;
                            ColorItem.BackColor = Color.Black;
                            break;
                        }

                    case ColorClipboard.MenuEvent.Paste:
                        {
                            ColorItem.BackColor = ColorClipboard.CopiedColor;
                            return ColorClipboard.CopiedColor;
                        }

                    case ColorClipboard.MenuEvent.Override:
                        {
                            ColorItem.BackColor = _overrideColor;
                            return ColorItem.BackColor;
                        }

                    case ColorClipboard.MenuEvent.Delete:
                        {
                            ColorItem.BackColor = Color.FromArgb(0, 0, 0, 0);
                            return ColorItem.BackColor;
                        }

                    case ColorClipboard.MenuEvent.None:
                        {
                            return MainColor.DefaultBackColor;
                        }

                }
            }
            else
            {
                ColorClipboard.Event = ColorClipboard.MenuEvent.None;
                return MainColor.DefaultBackColor;
            } // Nothing

            return default;
        }

        private void SubMenu_Shown(object sender, EventArgs e)
        {
            PaletteContainer.Visible = false;
        }

        public void Collapse_Expand()
        {
            Button4.Visible = false;

            if (PaletteContainer.Visible)
            {
                Button4.Text = ">";

                Transition
                    .With(this, nameof(Width), PaletteContainer.Left + 3)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                PaletteContainer.Visible = false;
                Label2.Visible = false;
            }
            else
            {
                Button4.Text = "<";

                Transition
                    .With(this, nameof(Width), PaletteContainer.Right + 8)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                PaletteContainer.Visible = true;
                Label2.Visible = true;
            }

            Button4.Visible = true;
        }

        private void SubMenu_Load(object sender, EventArgs e)
        {
            Icon = FormsExtensions.Icon<MainForm>();

            Point p = MousePosition;

            if (p.Y + Height > Screen.PrimaryScreen.WorkingArea.Bottom)
            {
                p.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Height - 5;
            }

            if (p.Y < Screen.PrimaryScreen.WorkingArea.Top)
            {
                p.Y = 0;
            }

            if (p.X + Width > Screen.PrimaryScreen.WorkingArea.Right)
            {
                p.X = Screen.PrimaryScreen.WorkingArea.Right - Width - 5;
            }

            if (p.X < Screen.PrimaryScreen.WorkingArea.Left)
            {
                p.X = 0;
            }

            Location = p;

            Width = PaletteContainer.Left + 3;

            PaletteContainer.Visible = false;
            Label2.Visible = false;
            Button4.Text = ">";

            if (Clipboard.GetData("Text") is not null)
            {
                string s = Clipboard.GetData("Text").ToString().ToLower();

                if (s.StartsWith("color "))
                {
                    Color C = Color.FromArgb(255, 0, 0, 0);
                    s = s.Remove(0, "color ".Count());
                    s = s.Replace("[", string.Empty);
                    s = s.Replace("]", string.Empty);
                    s = s.Replace(" ", string.Empty);

                    foreach (string x in s.Split(','))
                    {
                        int i = (int)Math.Round(Conversion.Val(x.Remove(0, 2)));
                        if (x.StartsWith("a="))
                            C = Color.FromArgb(i, C);
                        if (x.StartsWith("r="))
                            C = Color.FromArgb(C.A, i, C.G, C.B);
                        if (x.StartsWith("g="))
                            C = Color.FromArgb(C.A, C.R, i, C.B);
                        if (x.StartsWith("b="))
                            C = Color.FromArgb(C.A, C.R, C.G, i);
                    }

                    ColorClipboard.CopiedColor = C;
                }

                else if (s.IsHexColor())
                {
                    ColorClipboard.CopiedColor = s.ToColorFromHex();
                }
            }

            Button3.Enabled = ColorClipboard.CopiedColor != Color.Empty;

            BackColor = Program.Style.DarkMode ? MainColor.BackColor.Dark(_dark) : MainColor.BackColor.LightLight();
        }

        public void MiniColorItem_Clicked(object sender, EventArgs e)
        {
            MainColor.BackColor = ((ColorItem)sender).BackColor;
            MainColor.DefaultBackColor = ((ColorItem)sender).BackColor;

            InvertedColor.BackColor = MainColor.BackColor.Invert().CB((float)((trackBarX4.Value - 100) / 100d));
            InvertedColor.DefaultBackColor = MainColor.BackColor.Invert();

            Collapse_Expand();
        }

        public void GetHistoryColors(ColorItem ColorItem)
        {
            PaletteContainer.SuspendLayout();

            foreach (UI.Controllers.ColorItem c in PaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
            {
                c.Click -= MiniColorItem_Clicked;
                c.Dispose();
                PaletteContainer.Controls.Remove(c);
            }

            PaletteContainer.Controls.Clear();

            foreach (Color c in ColorItem.ColorsHistory)
            {
                UI.Controllers.ColorItem MiniColorItem = new();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
                MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                PaletteContainer.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_Clicked;
            }

            PaletteContainer.ResumeLayout();
        }

        public void Update_Variants()
        {
            InvertedColor.DefaultBackColor = MainColor.DefaultBackColor.Invert();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetData("Text", MainColor.BackColor.ToStringHex(true, true));
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Copy;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Cut;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Paste;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void MainColor_Click(object sender, EventArgs e)
        {
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Override;
            _overrideColor = ((ColorItem)sender).BackColor;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Collapse_Expand();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Delete;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PreviousColor_Click(object sender, EventArgs e)
        {
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Override;
            _overrideColor = ((ColorItem)sender).BackColor;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Override;
            _overrideColor = PreviousClr;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            MainColor.BackColor = MainColor.DefaultBackColor.CB((float)((trackBarX1.Value - 100) / 100d));
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            DefaultColor.BackColor = DefaultColor.DefaultBackColor.CB((float)((trackBarX2.Value - 100) / 100d));
        }

        private void trackBarX3_ValueChanged(object sender, EventArgs e)
        {
            PreviousColor.BackColor = PreviousColor.DefaultBackColor.CB((float)((trackBarX3.Value - 100) / 100d));
        }

        private void trackBarX4_ValueChanged(object sender, EventArgs e)
        {
            InvertedColor.BackColor = InvertedColor.DefaultBackColor.CB((float)((trackBarX4.Value - 100) / 100d));
        }
    }
}