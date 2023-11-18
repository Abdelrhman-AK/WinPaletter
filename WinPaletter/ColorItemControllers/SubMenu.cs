﻿using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public partial class SubMenu
    {

        private bool _shown;
        private Color _overrideColor;
        private bool _eventDone;
        private readonly int _Speed = 20;
        private readonly float _dark = 0.7f;
        private Color PreviousClr;

        #region Form Shadow
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                if (!DWMAPI.IsCompositionEnabled())
                {
                    cp.ClassStyle = cp.ClassStyle | DWMAPI.CS_DROPSHADOW;
                    cp.ExStyle = cp.ExStyle | 33554432;
                    return cp;
                }
                else
                {
                    return cp;
                }
            }
        }

        public SubMenu()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case DWMAPI.WM_NCPAINT:
                    {
                        int val = 2;
                        if (DWMAPI.IsCompositionEnabled())
                        {
                            DWMAPI.DwmSetWindowAttribute(Handle, Program.Style.RoundedCorners ? 2 : 1, ref val, 4);
                            DWMAPI.MARGINS bla = new();
                            {
                                bla.bottomHeight = 1;
                                bla.leftWidth = 1;
                                bla.rightWidth = 1;
                                bla.topHeight = 1;
                            }
                            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref bla);
                        }
                        break;
                    }
            }

            const uint WM_NCACTIVATE = 0x86U;

            if (m.Msg == WM_NCACTIVATE && m.WParam.ToInt32() == 0)
            {
                HandleDeactivate();
            }

            base.WndProc(ref m);
        }
        #endregion

        public Color ShowMenu(UI.Controllers.ColorItem ColorItem, bool EnableDelete = false)
        {
            Button5.Visible = EnableDelete;

            MainColor.BackColor = ColorItem.BackColor.CB((float)((Trackbar1.Value - 100) / 100d));
            DefaultColor.BackColor = ColorItem.DefaultColor.CB((float)((Trackbar2.Value - 100) / 100d));
            InvertedColor.BackColor = ColorItem.BackColor.Invert().CB((float)((Trackbar3.Value - 100) / 100d));

            MainColor.DefaultColor = ColorItem.BackColor;
            DefaultColor.DefaultColor = ColorItem.DefaultColor;
            InvertedColor.DefaultColor = ColorItem.BackColor.Invert();

            if (ColorItem.ColorsHistory.Count > 1)
            {
                PreviousColor.BackColor = ColorItem.ColorsHistory[ColorItem.ColorsHistory.Count - 2].CB((float)((Trackbar4.Value - 100) / 100d));
                PreviousColor.DefaultColor = ColorItem.ColorsHistory[ColorItem.ColorsHistory.Count - 2];
            }
            else
            {
                PreviousColor.BackColor = ColorItem.BackColor.CB((float)((Trackbar4.Value - 100) / 100d));
                PreviousColor.DefaultColor = ColorItem.BackColor;
            }

            PreviousClr = PreviousColor.DefaultColor;

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
                            return MainColor.DefaultColor;
                        }

                }
            }
            else
            {
                ColorClipboard.Event = ColorClipboard.MenuEvent.None;
                return MainColor.DefaultColor;
            } // Nothing

            return default;
        }

        private void SubMenu_Shown(object sender, EventArgs e)
        {
            _shown = true;

            PaletteContainer.Visible = false;
        }

        private void SubMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE | User32.AnimateWindowFlags.AW_BLEND);
        }

        public void HandleDeactivate()
        {
            if (_shown)
            {
                _shown = false;
                if (!_eventDone)
                    DialogResult = DialogResult.None;
                Close();
            }
        }

        public void Collapse_Expand()
        {

            Button4.Visible = false;

            switch (PaletteContainer.Visible)
            {
                case false:
                    {
                        Button4.Text = "<";

                        for (int i = PaletteContainer.Left + 3, loopTo = PaletteContainer.Right + 8; i <= loopTo; i += 2)
                            Width = i;

                        Width = PaletteContainer.Right + 8;

                        PaletteContainer.Visible = true;
                        Label2.Visible = true;
                        break;
                    }

                case true:
                    {
                        Button4.Text = ">";

                        PaletteContainer.Visible = false;
                        Label2.Visible = false;

                        for (int i = PaletteContainer.Right + 8, loopTo1 = PaletteContainer.Left + 3; i >= loopTo1; i -= 2)
                            Width = i;

                        Width = PaletteContainer.Left + 3;
                        break;
                    }

            }

            Button4.Visible = true;
        }

        private void SubMenu_Load(object sender, EventArgs e)
        {
            _shown = false;

            var p = MousePosition;

            if (p.Y + Height > Program.Computer.Screen.WorkingArea.Bottom)
            {
                p.Y = Program.Computer.Screen.WorkingArea.Bottom - Height - 5;
            }

            if (p.Y < Program.Computer.Screen.WorkingArea.Top)
            {
                p.Y = 0;
            }

            if (p.X + Width > Program.Computer.Screen.WorkingArea.Right)
            {
                p.X = Program.Computer.Screen.WorkingArea.Right - Width - 5;
            }

            if (p.X < Program.Computer.Screen.WorkingArea.Left)
            {
                p.X = 0;
            }


            Location = p;

            Width = PaletteContainer.Left + 3;

            PaletteContainer.Visible = false;
            Label2.Visible = false;
            Button4.Text = ">";

            this.LoadLanguage();
            ApplyStyle(this);

            if (ColorClipboard.CopiedColor == null)
            {

                Button3.Enabled = false;

                try
                {
                    if (Clipboard.GetData("Text") is not null)
                    {
                        string s = Clipboard.GetData("Text").ToString().ToLower();

                        if (s.StartsWith("color "))
                        {
                            var C = Color.FromArgb(255, 0, 0, 0);
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
                            Button3.Enabled = true;
                        }
                    }
                }
                catch
                {
                    Button3.Enabled = false;
                }
            }

            else
            {
                Button3.Enabled = true;
            }

            BackColor = Program.Style.DarkMode ? MainColor.BackColor.Dark(_dark) : MainColor.BackColor.LightLight();

            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE | User32.AnimateWindowFlags.AW_BLEND);

            Invalidate();
        }

        public void MiniColorItem_Clicked(object sender, EventArgs e)
        {
            MainColor.BackColor = ((UI.Controllers.ColorItem)sender).BackColor;
            MainColor.DefaultColor = ((UI.Controllers.ColorItem)sender).BackColor;

            InvertedColor.BackColor = MainColor.BackColor.Invert().CB((float)((Trackbar3.Value - 100) / 100d));
            InvertedColor.DefaultColor = MainColor.BackColor.Invert();

            Collapse_Expand();
        }

        public void GetHistoryColors(UI.Controllers.ColorItem ColorItem)
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
                UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
                MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                PaletteContainer.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_Clicked;
            }

            PaletteContainer.ResumeLayout();
        }

        public void Update_Variants()
        {
            InvertedColor.DefaultColor = MainColor.DefaultColor.Invert();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetData("Text", MainColor.BackColor);
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
            _overrideColor = ((UI.Controllers.ColorItem)sender).BackColor;
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

        private void Trackbar1_Scroll(object sender)
        {
            MainColor.BackColor = MainColor.DefaultColor.CB((float)((Trackbar1.Value - 100) / 100d));
        }

        private void Trackbar2_Scroll(object sender)
        {
            DefaultColor.BackColor = DefaultColor.DefaultColor.CB((float)((Trackbar2.Value - 100) / 100d));
        }

        private void Trackbar3_Scroll(object sender)
        {
            InvertedColor.BackColor = InvertedColor.DefaultColor.CB((float)((Trackbar3.Value - 100) / 100d));
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Trackbar1.Value = 100;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Trackbar2.Value = 100;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Trackbar3.Value = 100;
        }

        private void Trackbar4_Scroll(object sender)
        {
            PreviousColor.BackColor = PreviousColor.DefaultColor.CB((float)((Trackbar4.Value - 100) / 100d));
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Trackbar4.Value = 100;
        }

        private void PreviousColor_Click(object sender, EventArgs e)
        {
            _eventDone = true;
            ColorClipboard.Event = ColorClipboard.MenuEvent.Override;
            _overrideColor = ((UI.Controllers.ColorItem)sender).BackColor;
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
    }
}