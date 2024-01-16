using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class MainFrm
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Size = new(Convert.ToInt32(Program.Settings.General.MainFormWidth), Convert.ToInt32(Program.Settings.General.MainFormHeight));
            Location = new(Program.Computer.Screen.Bounds.Width / 2 - Size.Width / 2, Program.Computer.Screen.Bounds.Height / 2 - Size.Height / 2);
            WindowState = (FormWindowState)Convert.ToInt32(Program.Settings.General.MainFormStatus);

            tabsContainer1.AddFormIntoTab(Forms.Dashboard);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Program.TM != Program.TM_Original)
            {
                if (Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation && !Forms.Dashboard.LoggingOff)
                {

                    switch (Forms.ComplexSave.ShowDialog())
                    {
                        case DialogResult.Yes:
                            {

                                string[] r = Program.Settings.General.ComplexSaveResult.Split('.');
                                string r1 = r[0];
                                string r2 = r[1];

                                switch (r1 ?? string.Empty)
                                {
                                    case "0":              // ' ApplyToTM
                                        {
                                            if (System.IO.File.Exists(Forms.Dashboard.SaveFileDialog1.FileName))
                                            {
                                                Program.TM.Save(Theme.Manager.Source.File, Forms.Dashboard.SaveFileDialog1.FileName);
                                                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                            }
                                            else if (Forms.Dashboard.SaveFileDialog1.ShowDialog() == DialogResult.OK)
                                            {
                                                Program.TM.Save(Theme.Manager.Source.File, Forms.Dashboard.SaveFileDialog1.FileName);
                                                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }

                                            break;
                                        }
                                    case "1":              // ' ApplyToTM As
                                        {
                                            if (Forms.Dashboard.SaveFileDialog1.ShowDialog() == DialogResult.OK)
                                            {
                                                Program.TM.Save(Theme.Manager.Source.File, Forms.Dashboard.SaveFileDialog1.FileName);
                                                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }

                                            break;
                                        }
                                }

                                switch (r2 ?? string.Empty)
                                {
                                    case "1":
                                        {
                                            Forms.ThemeLog.Apply_Theme();
                                            break;
                                        }

                                    case "2":
                                        {
                                            Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime);
                                            break;
                                        }

                                    case "3":
                                        {
                                            Forms.ThemeLog.Apply_Theme(Theme.Default.Get());
                                            break;
                                        }

                                }

                                break;
                            }

                        case DialogResult.No:
                            {
                                e.Cancel = false;
                                if ((OS.W7 | OS.W8 | OS.W81) & Program.Settings.Miscellaneous.Win7LivePreview)
                                    Program.RefreshDWM(Program.TM_Original);
                                base.OnFormClosing(e);
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                e.Cancel = true;
                                break;
                            }
                    }
                }
                else
                {
                    e.Cancel = false;
                    base.OnFormClosing(e);
                }
            }
            else
            {
                e.Cancel = false;
                base.OnFormClosing(e);
            }
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Program.Settings.General.MainFormWidth = Size.Width;
                Program.Settings.General.MainFormHeight = Size.Height;
            }
            if (WindowState != FormWindowState.Minimized)
            {
                Program.Settings.General.MainFormStatus = WindowState;
            }
            Program.Settings.General.Save();

            Settings old = new(Settings.Mode.Registry);
            {
                ref Settings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = old.Appearance.CustomColors;
                Appearance.BackColor = old.Appearance.BackColor;
                Appearance.AccentColor = old.Appearance.AccentColor;
                Appearance.CustomTheme_DarkMode = old.Appearance.CustomTheme_DarkMode;
                Appearance.RoundedCorners = old.Appearance.RoundedCorners;
                Appearance.Save();
            }
        }
    }
}