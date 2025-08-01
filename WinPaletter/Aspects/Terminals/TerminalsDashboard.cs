﻿using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class TerminalsDashboard
    {
        public TerminalsDashboard()
        {
            InitializeComponent();
        }

        private void TerminalsDashboard_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<CMD>();

            //Size targetSize = Size;
            //Point targetLocation = Forms.Home.card4.PointToScreen(Point.Empty) - new Size((Width - Forms.Home.card4.Width) / 2, (Height - Forms.Home.card4.Height) / 2);

            Size = Forms.Home.card4.Size;
            Location = Forms.Home.card4.PointToScreen(Point.Empty);

            BackColor = Color.Black;
            this.DropEffect(Padding.Empty, true, DWM.FormStyle.Acrylic);

            //FluentTransitions.Transition
                //.With(this, nameof(Width), targetSize.Width)
                //.With(this, nameof(Height), targetSize.Height)
                //.With(this, nameof(Left), targetLocation.X)
                //.With(this, nameof(Top), targetLocation.Y)
                //.HookOnCompletion(() => Program.Animator.ShowSync(panel1))
                //.CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration * 0.6));
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Program.Settings.WindowsTerminals.Bypass)
            {
                Forms.WindowsTerminal._Mode = WinTerminal.Version.Stable;
                Close();
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.WindowsTerminal);
            }

            else if (OS.W12 || OS.W11 || OS.W10)
            {
                string TerDir;

                if (!Program.Settings.WindowsTerminals.Path_Deflection)
                {
                    TerDir = SysPaths.TerminalJSON;
                }
                else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                {
                    TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                }
                else
                {
                    TerDir = SysPaths.TerminalJSON;
                }

                if (System.IO.File.Exists(TerDir))
                {
                    Forms.WindowsTerminal._Mode = WinTerminal.Version.Stable;
                    Close();
                    Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.WindowsTerminal);
                }
                else
                {
                    MsgBox(Program.Lang.Strings.Aspects.Terminals.TerminalStable_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, $"{Program.Lang.Strings.Aspects.Terminals.PathSupposed}\"{TerDir}\"", Program.Lang.Strings.General.CollapseNote, Program.Lang.Strings.General.ExpandNote, Program.Lang.Strings.Aspects.Terminals.Bypass);
                }
            }

            else
            {
                MsgBox(Program.Lang.Strings.Aspects.Terminals.CantRun, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, string.Empty, Program.Lang.Strings.General.CollapseNote, Program.Lang.Strings.General.ExpandNote, Program.Lang.Strings.Aspects.Terminals.Bypass);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (Program.Settings.WindowsTerminals.Bypass)
            {
                Forms.WindowsTerminal._Mode = WinTerminal.Version.Preview;
                Close();
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.WindowsTerminal);
            }
            else if (OS.W12 || OS.W11 || OS.W10)
            {
                string TerPreDir;

                if (!Program.Settings.WindowsTerminals.Path_Deflection)
                {
                    TerPreDir = SysPaths.TerminalPreviewJSON;
                }
                else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                {
                    TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                }
                else
                {
                    TerPreDir = SysPaths.TerminalPreviewJSON;
                }

                if (System.IO.File.Exists(TerPreDir))
                {
                    Forms.WindowsTerminal._Mode = WinTerminal.Version.Preview;
                    Close();
                    Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.WindowsTerminal);
                }
                else
                {
                    MsgBox(Program.Lang.Strings.Aspects.Terminals.TerminalPreview_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, $"{Program.Lang.Strings.Aspects.Terminals.PathSupposed}\"{TerPreDir}\"", Program.Lang.Strings.General.CollapseNote, Program.Lang.Strings.General.ExpandNote, Program.Lang.Strings.Aspects.Terminals.Bypass);
                }
            }

            else
            {
                MsgBox(Program.Lang.Strings.Aspects.Terminals.CantRun, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, string.Empty, Program.Lang.Strings.General.CollapseNote, Program.Lang.Strings.General.ExpandNote, Program.Lang.Strings.Aspects.Terminals.Bypass);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Forms.CMD._Edition = CMD.Edition.CMD;
            Close();
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.CMD);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.ExternalTerminal);
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (Program.Settings.WindowsTerminals.Bypass)
            {
                Forms.CMD._Edition = CMD.Edition.PowerShellx86;
                Close();
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.CMD);
            }
            else
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                string Dir = $@"{Environment.GetEnvironmentVariable("WINDIR")}\System32\WindowsPowerShell\v1.0";

                if (System.IO.Directory.Exists(Dir))
                {
                    Forms.CMD._Edition = CMD.Edition.PowerShellx86;
                    Close();
                    Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.CMD);
                }
                else
                {
                    MsgBox(Program.Lang.Strings.Aspects.Consoles.PowerShellx86_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, $"{Program.Lang.Strings.Aspects.Terminals.PathSupposed}\"{Dir}\"", Program.Lang.Strings.General.CollapseNote, Program.Lang.Strings.General.ExpandNote, Program.Lang.Strings.Aspects.Terminals.Bypass);
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
                Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.CMD);
            }
            else
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                string Dir = $@"{Environment.GetEnvironmentVariable("WINDIR")}\SysWOW64\WindowsPowerShell\v1.0";

                if (System.IO.Directory.Exists(Dir))
                {
                    Forms.CMD._Edition = CMD.Edition.PowerShellx64;
                    Close();
                    Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.CMD);
                }
                else
                {
                    MsgBox(Program.Lang.Strings.Aspects.Consoles.PowerShellx64_notFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, $"{Program.Lang.Strings.Aspects.Terminals.PathSupposed}\"{Dir}\"", Program.Lang.Strings.General.CollapseNote, Program.Lang.Strings.General.ExpandNote, Program.Lang.Strings.Aspects.Terminals.Bypass);
                }

                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            FluentTransitions.Transition.With(labelAlt1, nameof(labelAlt1.Text), (sender as UI.WP.Button).Tag.ToString()).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            FluentTransitions.Transition.With(labelAlt1, nameof(labelAlt1.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void TerminalsDashboard_Shown(object sender, EventArgs e)
        {
            Program.Animator.ShowSync(panel1);
        }
    }
}