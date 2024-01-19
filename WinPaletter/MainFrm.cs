using Microsoft.VisualBasic;
using Ookii.Dialogs.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;

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

            tabsContainer1.AddFormIntoTab(Forms.Home);
        }

        public bool ExitWithChangedFileResponse(SaveFileDialog SaveFileDialog, MethodInvoker Apply_Theme_Sub, MethodInvoker Apply_FirstTheme_Sub, MethodInvoker Apply_DefaultWin_Sub)
        {
            /* return false; means that Windows should not close the form
               return true; means that Windows should close the form */

            if ((Program.TM != Program.TM_Original) && Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation)
            {
                if (!OS.WXP)
                {
                    TaskDialog TD = new()
                    {
                        RightToLeft = Program.Lang.RightToLeft,
                        ButtonStyle = TaskDialogButtonStyle.Standard,
                        CenterParent = true,
                        WindowTitle = Application.ProductName,
                        MainInstruction = Program.Lang.SaveDialog_Question,
                        MainIcon = TaskDialogIcon.Custom,
                        Content = Program.Lang.SaveDialog_Content,
                        AllowDialogCancellation = true,
                        CustomMainIcon = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.HELP, Shell32.SHGSI.ICON) ?? SystemIcons.Question,
                    };

                    TD.Created += TD_Created;

                    TaskDialogButton yesButton = new(ButtonType.Custom) { Text = Program.Lang.Yes };
                    TaskDialogButton noButton = new(ButtonType.Custom) { Text = Program.Lang.No };
                    TaskDialogButton cancelButton = new(ButtonType.Custom) { Text = Program.Lang.Cancel };

                    TD.Buttons.Add(yesButton);
                    TD.Buttons.Add(noButton);
                    TD.Buttons.Add(cancelButton);

                    TaskDialogRadioButton radio_dontapply = new() { Text = Program.Lang.SaveDialog_DontApply, Checked = true };
                    TaskDialogRadioButton radio_apply = new() { Text = Program.Lang.SaveDialog_Apply, Checked = false };
                    TaskDialogRadioButton radio_apply1stTheme = new() { Text = Program.Lang.SaveDialog_Apply1stTheme, Checked = false };
                    TaskDialogRadioButton radio_applyDefWindows = new() { Text = Program.Lang.SaveDialog_ApplyDefWindows, Checked = false };

                    TD.RadioButtons.Add(radio_dontapply);
                    TD.RadioButtons.Add(radio_apply);
                    TD.RadioButtons.Add(radio_apply1stTheme);
                    TD.RadioButtons.Add(radio_applyDefWindows);

                    //Cause of a try is to prevent a crash if there is no sound device
                    try { Program.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation); } catch { }

                    DialogResult result = DialogResult.Yes;
                    TaskDialogButton resultButton = TD.ShowDialog();

                    if (resultButton == yesButton)
                    {
                        result = DialogResult.Yes;
                    }
                    else if (resultButton == noButton)
                    {
                        result = DialogResult.No;
                    }
                    else if (resultButton == cancelButton)
                    {
                        result = DialogResult.Cancel;
                    }

                    TD.Created -= TD_Created;

                    resultButton.Dispose();
                    yesButton.Dispose();
                    noButton.Dispose();
                    cancelButton.Dispose();
                    TD.Dispose();

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

                    if (sucess)
                    {
                        if (radio_apply.Checked)
                        {
                            if (Apply_Theme_Sub is not null) Apply_Theme_Sub();
                        }
                        else if (radio_apply1stTheme.Checked)
                        {
                            if (Apply_FirstTheme_Sub is not null) Apply_FirstTheme_Sub();
                        }
                        else if (radio_applyDefWindows.Checked)
                        {
                            if (Apply_DefaultWin_Sub is not null) Apply_DefaultWin_Sub();
                        }
                    }

                    return sucess;
                }
                else
                {
                    MsgBoxResult result = Interaction.MsgBox(Program.Lang.SaveDialog_Question, MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, Application.ProductName);
                    if (result == MsgBoxResult.Yes)
                    {
                        if (System.IO.File.Exists(SaveFileDialog.FileName) || SaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog.FileName);
                            Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                            if (Apply_Theme_Sub is not null) Apply_Theme_Sub();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (result == MsgBoxResult.No)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
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