using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class TerminalsDashboard
    {
        private readonly int _Speed = 20;
        private bool _shown;

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

        public TerminalsDashboard()
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

        private void TerminalsDashboard_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Forms.CMD.Icon;

            _shown = false;

            Location = Forms.MainFrm.Button24.PointToScreen(Point.Empty) - (Size)new Point(0, Height);

            if (OS.W10)
                PictureBox1.Image = Properties.Resources.Native10;
            else
                PictureBox1.Image = Properties.Resources.Native11;

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

        private void Button6_Click(object sender, EventArgs e)
        {

            if (Program.Settings.WindowsTerminals.Bypass)
            {
                Forms.WindowsTerminal._Mode = WinTerminal.Version.Stable;
                Close();
                Forms.WindowsTerminal.Show();
            }

            else if (OS.W10 | OS.W11)
            {
                string TerDir;

                if (!Program.Settings.WindowsTerminals.Path_Deflection)
                {
                    TerDir = PathsExt.TerminalJSON;
                }
                else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                {
                    TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                }
                else
                {
                    TerDir = PathsExt.TerminalJSON;
                }

                if (System.IO.File.Exists(TerDir))
                {
                    Forms.WindowsTerminal._Mode = WinTerminal.Version.Stable;
                    Close();
                    Forms.WindowsTerminal.Show();
                }
                else
                {
                    MsgBox(Program.Lang.TerminalStable_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, Program.Lang.Terminal_supposed + "\"" + TerDir + "\"", Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.Terminal_Bypass);
                }
            }

            else
            {
                MsgBox(Program.Lang.Terminal_CantRun, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, "", Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.Terminal_Bypass);

            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (Program.Settings.WindowsTerminals.Bypass)
            {
                Forms.WindowsTerminal._Mode = WinTerminal.Version.Preview;
                Close();
                Forms.WindowsTerminal.Show();
            }
            else if (OS.W10 | OS.W11)
            {
                string TerPreDir;

                if (!Program.Settings.WindowsTerminals.Path_Deflection)
                {
                    TerPreDir = PathsExt.TerminalPreviewJSON;
                }
                else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                {
                    TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                }
                else
                {
                    TerPreDir = PathsExt.TerminalPreviewJSON;
                }

                if (System.IO.File.Exists(TerPreDir))
                {
                    Forms.WindowsTerminal._Mode = WinTerminal.Version.Preview;
                    Close();
                    Forms.WindowsTerminal.Show();
                }
                else
                {
                    MsgBox(Program.Lang.TerminalPreview_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, Program.Lang.Terminal_supposed + "\"" + TerPreDir + "\"", Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.Terminal_Bypass);
                }
            }

            else
            {
                MsgBox(Program.Lang.Terminal_CantRun, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, "", Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.Terminal_Bypass);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Forms.CMD._Edition = CMD.Edition.CMD;
            Close();
            Forms.CMD.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Forms.ExternalTerminal.Show();
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (Program.Settings.WindowsTerminals.Bypass)
            {
                Forms.CMD._Edition = CMD.Edition.PowerShellx86;
                Close();
                Forms.CMD.Show();
            }
            else
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                string Dir = Environment.GetEnvironmentVariable("WINDIR") + @"\System32\WindowsPowerShell\v1.0";

                if (System.IO.Directory.Exists(Dir))
                {
                    Forms.CMD._Edition = CMD.Edition.PowerShellx86;
                    Close();
                    Forms.CMD.Show();
                }
                else
                {
                    MsgBox(Program.Lang.PowerShellx86_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, Program.Lang.Terminal_supposed + "\"" + Dir + "\"", Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.Terminal_Bypass);
                }

                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Program.Settings.WindowsTerminals.Bypass)
            {
                Forms.CMD._Edition = CMD.Edition.PowerShellx64;
                Close();
                Forms.CMD.Show();
            }
            else
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                string Dir = Environment.GetEnvironmentVariable("WINDIR") + @"\SysWOW64\WindowsPowerShell\v1.0";

                if (System.IO.Directory.Exists(Dir))
                {
                    Forms.CMD._Edition = CMD.Edition.PowerShellx64;
                    Close();
                    Forms.CMD.Show();
                }
                else
                {
                    MsgBox(Program.Lang.PowerShellx64_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, Program.Lang.Terminal_supposed + "\"" + Dir + "\"", Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.Terminal_Bypass);
                }

                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

    }
}