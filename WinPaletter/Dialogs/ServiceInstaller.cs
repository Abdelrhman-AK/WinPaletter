using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class ServiceInstaller
    {
        public ServiceInstaller()
        {
            InitializeComponent();
            Shown += BugReport_Show;
        }

        string _serviceName;
        byte[] _PE;
        string _destinationPath;

        private void BugReport_Load(object sender, EventArgs e)
        {
            Icon = FormsExtensions.Icon<SettingsX>();
            this.LoadLanguage();
            ApplyStyle(this);
            CheckForIllegalCrossThreadCalls = false;

            Color c = PictureBox1.Image.AverageColor().CB(Program.Style.DarkMode ? -0.35f : 0.35f);
            AnimatedBox1.Color1 = c;
            AnimatedBox1.Color2 = Program.Style.Schemes.Main.Colors.AccentAlt;

            textBox1.Text = string.Empty;
            textBox1.Font = Fonts.ConsoleMedium;
            textBox2.Font = Fonts.ConsoleMedium;

            Button1.Enabled = true;
            Button2.Enabled = true;

            CustomSystemSounds.Beep.Play();

            textBox2.Select();
            textBox2.Focus();

            BringToFront();
        }

        public enum RunMethods
        {
            Install,
            Update,
            Uninstall
        }
        private RunMethods runMethod;

        private void BugReport_Show(object sender, EventArgs e)
        {
            if (runMethod == RunMethods.Uninstall)
                Task.Run(Remove);
            else if (runMethod == RunMethods.Update)
                Task.Run(Setup);
            // Do nothing if runMethod is Run, as it will be handled by user interaction.
        }

        private void Setup()
        {
            textBox1.SetText(string.Empty);

            Button1.Enabled = false;
            Button2.Enabled = false;

            textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.Stopping, _serviceName)}\r\n");
            Program.SendCommand($"net stop {_serviceName}");

            List<Process> Processes = Program.ProgramsRunning(_destinationPath);
            if (Processes.Count > 0) { foreach (Process process in Processes) { process.Kill(); }; Thread.Sleep(100); }

            try
            {
                textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.Extracting, _serviceName)}\r\n");
                if (!Directory.Exists(Directory.GetParent(_destinationPath).FullName)) { Directory.CreateDirectory(Directory.GetParent(_destinationPath).FullName); }
                if (File.Exists(_destinationPath))
                    File.Delete(_destinationPath);

                File.WriteAllBytes(_destinationPath, _PE);
            }
            catch (IOException io_ex) { Forms.BugReport.ThrowError(io_ex); }

            if (File.Exists(_destinationPath))
            {
                IEnumerable<string> installutils = Directory.EnumerateFiles(RuntimeEnvironment.GetRuntimeDirectory(), "installutil.exe", SearchOption.AllDirectories);

                if (installutils is not null && installutils.Count() != 0)
                {
                    string installutil = installutils.ElementAt(0);

                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.Uninstalling, _serviceName)}\r\n");
                    Program.SendCommand($"\"{installutil}\" /u \"{_destinationPath}\"");

                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.Installing, _serviceName)}\r\n");
                    Program.SendCommand($"\"{installutil}\" \"{_destinationPath}\"");

                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.Starting, _serviceName)}\r\n");
                    Program.SendCommand($"net start {_serviceName}");

                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.InstallCompleted, _serviceName)}\r\n");

                    Thread.Sleep(1000);
                }
                else
                {
                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.MissingInstallutil, _serviceName)}\r\n");
                    MsgBox(Program.Lang.Strings.Services.MissingInstallutil, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void Remove()
        {
            textBox1.SetText(string.Empty);

            Button1.Enabled = false;
            Button2.Enabled = false;

            textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.Stopping, _serviceName)}\r\n");
            Program.SendCommand($"net stop {_serviceName}");

            List<Process> Processes = Program.ProgramsRunning(_destinationPath);
            if (Processes.Count > 0) { foreach (Process process in Processes) { process.Kill(); }; Thread.Sleep(100); }

            if (File.Exists(_destinationPath))
            {
                IEnumerable<string> installutils = Directory.EnumerateFiles(RuntimeEnvironment.GetRuntimeDirectory(), "installutil.exe", SearchOption.AllDirectories);

                if (installutils is not null && installutils.Count() != 0)
                {
                    string installutil = installutils.ElementAt(0);

                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.Uninstalling, _serviceName)}\r\n");
                    Program.SendCommand($"\"{installutil}\" /u \"{_destinationPath}\"");

                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.UninstallCompleted, _serviceName)}\r\n");

                    Thread.Sleep(1000);
                }
                else
                {
                    textBox1.SetText($"{textBox1.Text}• {string.Format(Program.Lang.Strings.Services.MissingInstallutil, _serviceName)}\r\n");
                    MsgBox(Program.Lang.Strings.Services.MissingInstallutil, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            try
            {
                if (!Directory.Exists(Directory.GetParent(_destinationPath).FullName)) { Directory.CreateDirectory(Directory.GetParent(_destinationPath).FullName); }
                if (File.Exists(_destinationPath))
                    File.Delete(_destinationPath);
            }
            catch (IOException io_ex) { Forms.BugReport.ThrowError(io_ex); }

            DialogResult = DialogResult.OK;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Task.Run(Setup);
        }

        /// <summary>
        /// Run a service installer with the specified parameters.
        /// </summary>
        /// <param name="serviceName">Name of service, for example: WinPaletter.SystemEventsSounds</param>
        /// <param name="serviceDescription">Description_SysEventsSounds to be shown for the user to give him an idea about the service</param>
        /// <param name="destinationPath">FileSystem of the PE byte array (.exe) file extracted</param>
        /// <param name="PE">Byte array of the service file (*.exe) to be installed/updated</param>
        /// <param name="_runMethod">It may be install, update or uninstall</param>
        /// <param name="quietUninstall">If run method is uninstall, make the process quiet without output</param>
        public void Run(string serviceName, string serviceDescription, string destinationPath, byte[] PE, RunMethods @_runMethod, bool quietUninstall = false)
        {
            runMethod = @_runMethod;
            _serviceName = serviceName;
            _PE = PE;
            _destinationPath = destinationPath;

            if (runMethod == RunMethods.Install)
                title.Text = string.Format(Program.Lang.Strings.Services.Title_Install, serviceName);
            else if (runMethod == RunMethods.Update)
                title.Text = string.Format(Program.Lang.Strings.Services.Title_Update, serviceName);
            else if (runMethod == RunMethods.Uninstall)
                title.Text = string.Format(Program.Lang.Strings.Services.Title_Uninstall, serviceName);

            textBox2.Text = serviceDescription;

            Opacity = quietUninstall ? 0 : 1;
            ShowDialog();
            Opacity = 1;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}