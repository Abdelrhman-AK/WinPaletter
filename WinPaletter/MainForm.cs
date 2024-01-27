using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Theme;

namespace WinPaletter
{
    public partial class MainForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Size = new(Convert.ToInt32(Program.Settings.General.MainFormWidth), Convert.ToInt32(Program.Settings.General.MainFormHeight));
            Location = new(Screen.PrimaryScreen.Bounds.Width / 2 - Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - Size.Height / 2);
            WindowState = (FormWindowState)Convert.ToInt32(Program.Settings.General.MainFormStatus);

            tabsContainer1.AddFormIntoTab(Forms.Home);
        }

        public bool ExitWithChangedFileResponse(SaveFileDialog SaveFileDialog, MethodInvoker Apply_Theme_Sub, MethodInvoker Apply_FirstTheme_Sub, MethodInvoker Apply_DefaultWin_Sub)
        {
            /* return false; means that Windows should not close the form
               return true; means that Windows should close the form */

            if (Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation && Program.TM != Program.TM_Original)
            {
                DialogResult result = MsgBox(Program.Lang.SaveDialog_Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                bool sucess;

                if (result == DialogResult.Yes)
                {
                    if (System.IO.File.Exists(SaveFileDialog.FileName) || SaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog.FileName);
                        Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                    }

                    sucess = true;
                }
                else if (result == DialogResult.No)
                {
                    sucess = true;
                }
                else
                {
                    sucess = false;
                }

                return sucess;
            }
            else
            {
                return true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            bool result = ExitWithChangedFileResponse(Forms.Home.SaveFileDialog1,
                            () => Forms.ThemeLog.Apply_Theme(Program.TM, false, true),
                            () => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime, false, true),
                            () => { using (Manager TMx = Default.Get()) { Forms.ThemeLog.Apply_Theme(TMx, false, true); } }
                            );

            e.Cancel = !result;

            base.OnFormClosing(e);
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Program.DeleteWinPaletteReg)
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
}