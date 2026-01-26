using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinPaletter.Theme;

namespace WinPaletter
{
    /// <summary>
    /// The main form of WinPaletter.
    /// </summary>
    public partial class MainForm
    {
        /// <summary>
        /// Flag to indicate if the user is logging off.
        /// </summary>
        public bool LoggingOff = false;

        /// <summary>
        /// Flag to indicate if home page sent a close signal.
        /// </summary>
        public bool closeSignalReceivedFromHomePage = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // Reset the flag that indicates if the user is closing the application from the home page.
            closeSignalReceivedFromHomePage = false;

            // Set form properties.
            Size = new(Program.Settings.General.MainFormWidth, Program.Settings.General.MainFormHeight);
            CenterToScreen();
            WindowState = Program.Settings.General.MainFormStatus;

            // Start showing home page tab.
            tabControl1.Visible = false;
            Status_pnl.Visible = Program.Settings.AppLog.StatusPanel;
            Status_lbl.Font = Fonts.Console;
            tabsContainer1.AddFormIntoTab(Forms.Home);
            if (Program.ShowWhatsNew) Process.Start($"{Links.Releases}/tag/v{Program.Version}");
            if (Program.Settings.Miscellaneous.ShowWelcomeDialog) Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Welcome);

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
                DialogResult result = MsgBox(Program.Localization.Strings.Messages.SaveDialog_Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                bool sucess;

                if (result == DialogResult.Yes)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
                    {
                        if (File.Exists(dlg.FileName) || dlg.ShowDialog() == DialogResult.OK)
                        {
                            Program.TM.Save(Manager.Source.File, dlg.FileName);
                            Program.TM_Original = Program.TM.Clone();
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

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;

            if (!Forms.MainForm.LoggingOff && !closeSignalReceivedFromHomePage)
            {
                if (Forms.Home.Parent is TabPage && Forms.MainForm.tabsContainer1.TabsCount > 1)
                {
                    if (MsgBox(Program.Localization.Strings.Messages.OpenTabs_Close, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
                }

                if (!e.Cancel)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = Forms.Home.File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
                    {
                        bool result = Forms.MainForm.ExitWithChangedFileResponse(); //dlg,
                                                                                    //() => Forms.ThemeLog.Apply_Theme(Program.TM, false, true),
                                                                                    //() => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime, false, true),
                                                                                    //() => { using (Manager TMx = Default.Get()) { Forms.ThemeLog.Apply_Theme(TMx, false, true); } }
                                                                                    //);

                        e.Cancel = !result;
                    }
                }
            }

            closeSignalReceivedFromHomePage = false;

            base.OnFormClosing(e);
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Program.DeleteWinPaletteReg)
            {
                // Save the form's position and size to the registry.
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

                // Save WinPaletter appearance settings to the registry.
                Settings old = new(Settings.Source.Registry);
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

        private void Status_lbl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear((sender as Label).BackColor);
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            string status = (sender as Label).Text.Trim().Replace("\r\n", " <br> ");
            TextRenderer.DrawText(e.Graphics, status, (sender as Label).Font, (sender as Label).ClientRectangle, (sender as Label).ForeColor, flags);
        }
    }
}