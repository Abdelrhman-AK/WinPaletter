using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class SysEventsSndsInstaller
    {
        private bool uninstall = false;
        private bool install_or_update = true;

        public SysEventsSndsInstaller()
        {
            InitializeComponent();
            this.Shown += BugReport_Show;
        }

        const string SvcName = "WinPaletter.SystemEventsSounds";

        private void BugReport_Load(object sender, EventArgs e)
        {
            Icon = Forms.SettingsX.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
            CheckForIllegalCrossThreadCalls = false;

            Color c = PictureBox1.Image.AverageColor().CB((float)(Program.Style.DarkMode ? -0.35d : 0.35d));
            AnimatedBox1.Color1 = c;
            AnimatedBox1.Color2 = Program.Style.Schemes.Main.Colors.AccentAlt;

            textBox1.Text = string.Empty;
            textBox1.Font = Fonts.ConsoleMedium;
            textBox2.Font = Fonts.ConsoleMedium;

            Button1.Enabled = true;
            Button2.Enabled = true;
            CheckBox1.Enabled = true;
            CheckBox1.Checked = Program.Settings.UsersServices.ShowSysEventsSoundsInstaller;

            SystemSounds.Beep.Play();

            textBox2.Select();
            textBox2.Focus();

            BringToFront();
        }

        private void BugReport_Show(object sender, EventArgs e)
        {
            if (!uninstall)
            {
                if (install_or_update) { }  //Install (starts with user interaction) 

                else
                { Task.Run(Setup); }        //Update
            }
            else { Task.Run(Remove); }      //Uninstall
        }

        public void Setup()
        {
            textBox1.SetText(string.Empty);

            Button1.Enabled = false;
            Button2.Enabled = false;
            CheckBox1.Enabled = false;

            textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_Stopping, SvcName))}\r\n");
            Program.SendCommand("net stop WinPaletter.SystemEventsSounds");

            List<Process> Processes = Program.ProgramsRunning(SysPaths.SysEventsSounds);
            if (Processes.Count > 0) { foreach (Process process in Processes) { process.Kill(); }; Thread.Sleep(100); }

            try
            {
                textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_Extracting, SvcName))}\r\n");
                if (!System.IO.Directory.Exists(SysPaths.SysEventsSoundsDir)) { System.IO.Directory.CreateDirectory(SysPaths.SysEventsSoundsDir); }
                if (System.IO.File.Exists(SysPaths.SysEventsSounds))
                    System.IO.File.Delete(SysPaths.SysEventsSounds);

                System.IO.File.WriteAllBytes(SysPaths.SysEventsSounds, Properties.Resources.WinPaletter_SysEventsSounds);
            }
            catch (IOException io_ex) { Forms.BugReport.ThrowError(io_ex); }

            if (System.IO.File.Exists(SysPaths.SysEventsSounds))
            {
                IEnumerable<string> installutils = System.IO.Directory.EnumerateFiles(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "installutil.exe", System.IO.SearchOption.AllDirectories);

                if (installutils is not null && installutils.Count() != 0)
                {
                    string installutil = installutils.ElementAt(0);

                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_Uninstalling, SvcName))}\r\n");
                    Program.SendCommand($"\"{installutil}\" /u \"{SysPaths.SysEventsSounds}\"");

                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_Installing, SvcName))}\r\n");
                    Program.SendCommand($"\"{installutil}\" \"{SysPaths.SysEventsSounds}\"");

                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_Starting, SvcName))}\r\n");
                    Program.SendCommand("net start WinPaletter.SystemEventsSounds");

                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_InstallCompleted, SvcName))}\r\n");

                    Thread.Sleep(1000);
                }
                else
                {
                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_MissingInstallutil, SvcName))}\r\n");
                    MsgBox(Program.Lang.SvcInstaller_MissingInstallutil, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            DialogResult = DialogResult.OK;
        }

        public void Remove()
        {
            textBox1.SetText(string.Empty);

            Button1.Enabled = false;
            Button2.Enabled = false;
            CheckBox1.Enabled = false;

            textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_Stopping, SvcName))}\r\n");
            Program.SendCommand("net stop WinPaletter.SystemEventsSounds");

            List<Process> Processes = Program.ProgramsRunning(SysPaths.SysEventsSounds);
            if (Processes.Count > 0) { foreach (Process process in Processes) { process.Kill(); }; Thread.Sleep(100); }

            if (System.IO.File.Exists(SysPaths.SysEventsSounds))
            {
                IEnumerable<string> installutils = System.IO.Directory.EnumerateFiles(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "installutil.exe", System.IO.SearchOption.AllDirectories);

                if (installutils is not null && installutils.Count() != 0)
                {
                    string installutil = installutils.ElementAt(0);

                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_Uninstalling, SvcName))}\r\n");
                    Program.SendCommand($"\"{installutil}\" /u \"{SysPaths.SysEventsSounds}\"");

                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_UninstallCompleted, SvcName))}\r\n");

                    Thread.Sleep(1000);
                }
                else
                {
                    textBox1.SetText($"{textBox1.Text}• {(string.Format(Program.Lang.SvcInstaller_MissingInstallutil, SvcName))}\r\n");
                    MsgBox(Program.Lang.SvcInstaller_MissingInstallutil, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            try
            {
                if (!System.IO.Directory.Exists(SysPaths.SysEventsSoundsDir)) { System.IO.Directory.CreateDirectory(SysPaths.SysEventsSoundsDir); }
                if (System.IO.File.Exists(SysPaths.SysEventsSounds))
                    System.IO.File.Delete(SysPaths.SysEventsSounds);
            }
            catch (IOException io_ex) { Forms.BugReport.ThrowError(io_ex); }

            DialogResult = DialogResult.OK;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Program.Settings.UsersServices.ShowSysEventsSoundsInstaller = CheckBox1.Checked;
            Program.Settings.UsersServices.Save();
            Task.Run(Setup);
        }

        public void Install(bool install_or_update)
        {

            this.install_or_update = install_or_update;
            uninstall = false;

            title.Text = install_or_update ? Program.Lang.SvcInstaller_Title_Install : Program.Lang.SvcInstaller_Title_Update;
            textBox2.Text = Program.Lang.SvcInstaller_Description;

            this.ShowDialog();
        }

        public void Uninstall(bool quiet = false)
        {
            uninstall = true;

            title.Text = Program.Lang.SvcInstaller_Title_Uninstall;
            textBox2.Text = Program.Lang.SvcInstaller_Description;

            this.Opacity = quiet ? 0 : 1;
            this.ShowDialog();
            this.Opacity = 1;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Program.Settings.UsersServices.ShowSysEventsSoundsInstaller = CheckBox1.Checked;
            Program.Settings.UsersServices.Save();
            Close();
        }
    }
}