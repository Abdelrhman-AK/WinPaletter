using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class MainForm
    {
        /// <summary>
        /// Flag to indicate if the user is logging off.
        /// </summary>
        public bool LoggingOff = false;

        public bool closeSignalReceivedFromHomePage = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            closeSignalReceivedFromHomePage = false;

            Size = new(Convert.ToInt32(Program.Settings.General.MainFormWidth), Convert.ToInt32(Program.Settings.General.MainFormHeight));
            CenterToScreen();
            WindowState = (FormWindowState)Convert.ToInt32(Program.Settings.General.MainFormStatus);

            tabControl1.Visible = false;
            tabsContainer1.AddFormIntoTab(Forms.Home);
            if (Program.ShowWhatsNew) Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Whatsnew);
            Program.Animator.ShowSync(tabControl1);
        }

        /// <summary>
        /// If the theme has been changed, this method will prompt the user to save the changes.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the form should close; otherwise, <c>false</c>.
        /// </returns>
        public bool ExitWithChangedFileResponse()
        {
            if (Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation && Program.TM != Program.TM_Original)
            {
                DialogResult result = MsgBox(Program.Lang.SaveDialog_Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                bool sucess;

                if (result == DialogResult.Yes)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.file, Title = Program.Lang.Filter_SaveWinPaletterTheme })
                    {
                        if (System.IO.File.Exists(dlg.FileName) || dlg.ShowDialog() == DialogResult.OK)
                        {
                            Program.TM.Save(Theme.Manager.Source.File, dlg.FileName);
                            Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                        }
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
            e.Cancel = false;

            if (!Forms.MainForm.LoggingOff && !closeSignalReceivedFromHomePage)
            {
                using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.file, Title = Program.Lang.Filter_SaveWinPaletterTheme })
                {
                    bool result = Forms.MainForm.ExitWithChangedFileResponse(); //dlg,
                                                                                //() => Forms.ThemeLog.Apply_Theme(Program.TM, false, true),
                                                                                //() => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime, false, true),
                                                                                //() => { using (Manager TMx = Default.Get()) { Forms.ThemeLog.Apply_Theme(TMx, false, true); } }
                                                                                //);

                    e.Cancel = !result;
                }

                if (Forms.Home.Parent is TabPage && Forms.MainForm.tabsContainer1.TabsCount > 1)
                {
                    if (MsgBox(Program.Lang.OpenTabs_Close, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
                }
            }

            closeSignalReceivedFromHomePage = false;

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