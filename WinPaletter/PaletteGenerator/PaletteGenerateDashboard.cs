﻿using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class PaletteGenerateDashboard
    {
        private readonly int _Speed = 20;
        private bool _shown;

        #region Form Shadow

        private bool aeroEnabled;

        protected override CreateParams CreateParams
        {
            get
            {
                CheckAeroEnabled();
                var cp = base.CreateParams;
                if (!aeroEnabled)
                {
                    cp.ClassStyle = cp.ClassStyle | Dwmapi.CS_DROPSHADOW;
                    cp.ExStyle = cp.ExStyle | 33554432;
                    return cp;
                }
                else
                {
                    return cp;
                }
            }
        }

        public PaletteGenerateDashboard()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Dwmapi.WM_NCPAINT:
                    {
                        int val = 2;
                        if (aeroEnabled)
                        {
                            Dwmapi.DwmSetWindowAttribute(Handle, WPStyle.GetRoundedCorners() ? 2 : 1, ref val, 4);
                            var bla = new Dwmapi.MARGINS();
                            {
                                ref var temp = ref bla;
                                temp.bottomHeight = 1;
                                temp.leftWidth = 1;
                                temp.rightWidth = 1;
                                temp.topHeight = 1;
                            }
                            Dwmapi.DwmExtendFrameIntoClientArea(Handle, ref bla);
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

        private void CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                var Com = default(bool);
                Dwmapi.DwmIsCompositionEnabled(ref Com);
                aeroEnabled = Com;
            }
            else
            {
                aeroEnabled = false;
            }
        }
        #endregion

        private void TerminalsDashboard_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.PaletteGenerateFromImage.Icon;
            _shown = false;

            Location = My.MyProject.Forms.MainFrm.Button40.PointToScreen(Point.Empty) + (Size)new Point(0, My.MyProject.Forms.MainFrm.Button40.Height);

            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE | User32.AnimateWindowFlags.AW_BLEND);

            Invalidate();
        }

        private void SubMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE | User32.AnimateWindowFlags.AW_BLEND);
        }

        private void TerminalsDashboard_Shown(object sender, EventArgs e)
        {
            _shown = true;
        }

        public void HandleDeactivate()
        {
            if (_shown)
            {
                _shown = false;
                DialogResult = DialogResult.None;
                Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
            My.MyProject.Forms.PaletteGenerateFromImage.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Close();
            My.MyProject.Forms.PaletteGenerateFromColor.ShowDialog();
        }
    }
}