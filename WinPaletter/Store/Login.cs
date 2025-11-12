using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using WinPaletter.Theme.Structures;
using WinPaletter.UI.AdvancedControls;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Simulation;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.BitmapExtensions;

namespace WinPaletter
{
    public partial class Login : Form
    {
        CancellationTokenSource cts;
        bool canCloseWithoutMsg = false;
        private GitHubLoginManager loginManager;
        private Octokit.User loggedInUser;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            NativeMethods.Helpers.RemoveFormTitlebarTextAndIcon(Handle);
            Icon = FormsExtensions.Icon<MainForm>();
            tablessControl1.SelectedIndex = 0;
            canCloseWithoutMsg = false;
            signin_btn.Visible = true;
            cts = new();
            labelAlt1.Text = Text;

            bool darkMode = Program.Style.DarkMode;

            pictureBox2.Image = darkMode ? Assets.Store.LoginDlg_Fork : Assets.Store.LoginDlg_Fork.Invert();
            pictureBox5.Image = darkMode ? Assets.Store.LoginDlg_Upload : Assets.Store.LoginDlg_Upload.Invert();
            pictureBox7.Image = darkMode ? Assets.Store.LoginDlg_PullRequest : Assets.Store.LoginDlg_PullRequest.Invert();
            pictureBox15.Image = darkMode ? Assets.Store.LoginDlg_Write : Assets.Store.LoginDlg_Write.Invert();
            pictureBox14.Image = darkMode ? Assets.Store.LoginDlg_Password : Assets.Store.LoginDlg_Password.Invert();
            pictureBox13.Image = darkMode ? Assets.Store.LoginDlg_Revoke : Assets.Store.LoginDlg_Revoke.Invert();
            pictureBox12.Image = darkMode ? Assets.Store.LoginDlg_Second : Assets.Store.LoginDlg_Second.Invert();
            pictureBox10.Image = darkMode ? Assets.Store.LoginDlg_Code : Assets.Store.LoginDlg_Code.Invert();
            pictureBox9.Image = darkMode ? Assets.Store.LoginDlg_Issues : Assets.Store.LoginDlg_Issues.Invert();
            pictureBox8.Image = darkMode ? Assets.Store.LoginDlg_Repository : Assets.Store.LoginDlg_Repository.Invert();
            pictureBox11.Image = darkMode ? Assets.Store.LoginDlg_Project : Assets.Store.LoginDlg_Project.Invert();

            c0.Font = new(Fonts.ConsoleLarge.FontFamily, 18f);
            c1.Font = c0.Font;
            c2.Font = c0.Font;
            c3.Font = c0.Font;
            c4.Font = c0.Font;
            c5.Font = c0.Font;
            c6.Font = c0.Font;
            c7.Font = c0.Font;

            githubLbl_elapsedSec.Font = Fonts.ConsoleMedium;

            loginManager = new();

            loginManager.OnCountdownStarted += LoginManager_OnCountdownStarted;
            loginManager.OnCountdownTick += LoginManager_OnCountdownTick;
            loginManager.OnCountdownEnded += LoginManager_OnCountdownEnded;
            loginManager.OnDeviceFlowInitiated += LoginManager_OnDeviceFlowInitiated;
            loginManager.OnAuthorizationSuccess += LoginManager_OnAuthorizationSuccess;
            loginManager.OnAuthorizationFailure += LoginManager_OnAuthorizationFailure;
            loginManager.OnSignedOut += LoginManager_OnSignedOut;

            Forms.GlassWindow.ShowWithGlassFocusedOnParent(Forms.MainForm);
        }

        private void LoginManager_OnAuthorizationFailure(string obj)
        {
            progressBar3.Visible = false;
        }

        private async void LoginManager_OnAuthorizationSuccess(Octokit.User obj)
        {
            progressBar2.Visible = true;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 1;
            Program.Animator.ShowSync(tablessControl1);

            loggedInUser = await loginManager.Client.User.Current();

            using (DownloadManager DM = new())
            {
                byte[] data = await DM.ReadAsync(loggedInUser.AvatarUrl).ConfigureAwait(false);
                using (MemoryStream ms = new(data))
                using (Bitmap bmp = Image.FromStream(ms) as Bitmap)
                using (Bitmap bmp_resized = bmp.Resize(pictureBox6.Size))
                {
                    pictureBox6.Image = bmp_resized.ToCircular();
                }
            }

            label13.Text = $"{Program.Lang.Strings.Store.Login_Welcome}, {loggedInUser.Login}!";

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 3;
            Program.Animator.ShowSync(tablessControl1);

            progressBar2.Visible = false;
        }

        private void LoginManager_OnDeviceFlowInitiated(string arg1, int arg2, string arg3)
        {
            // Invoke is needed because this event is raised from a non-UI thread
            Invoke(() =>
            {
                progressBar2.Visible = false;

                string code = arg1;
                c0.Text = code[0].ToString();
                c1.Text = code[1].ToString();
                c2.Text = code[2].ToString();
                c3.Text = code[3].ToString();
                c4.Text = code[5].ToString();
                c5.Text = code[6].ToString();
                c6.Text = code[7].ToString();
                c7.Text = code[8].ToString();

                int expiresIn = arg2;
                string verificationUrl = arg3;
                linkLabel1.Text = verificationUrl;

                progressBar3.Visible = true;
                progressBar3.Maximum = expiresIn;
                progressBar3.Value = expiresIn;
                githubLbl_elapsedSec.Text = $"{expiresIn / 60:D2}:{expiresIn % 60:D2}";

                Program.Animator.HideSync(tablessControl1);
                tablessControl1.SelectedIndex = 2;
                Program.Animator.ShowSync(tablessControl1);
            });
        }

        private void LoginManager_OnCountdownEnded()
        {
            progressBar3.Visible = false;
        }

        private void LoginManager_OnCountdownTick(int obj)
        {
            progressBar3.Value = obj;
            githubLbl_elapsedSec.Text = $"{obj / 60:D2}:{obj % 60:D2}";
        }

        private void LoginManager_OnCountdownStarted(int obj)
        {

        }

        private void LoginManager_OnSignedOut()
        {
            loggedInUser = null;
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            // If last tab, process and close
            if (tablessControl1.SelectedIndex == tablessControl1.TabCount - 1)
            {
                canCloseWithoutMsg = true;
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            if (tablessControl1.SelectedIndex == 0)
            {
                signin_btn.Visible = false;
                progressBar2.Visible = true;
            }
            else if (tablessControl1.SelectedIndex == 2)
            {
               
            }
            else if (tablessControl1.SelectedIndex == 3)
            {
               
            }

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex + 1 > tablessControl1.TabCount - 1 ? tablessControl1.TabCount - 1 : tablessControl1.SelectedIndex + 1;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            signin_btn.Visible = true;

            int amount = tablessControl1.SelectedIndex == 2 ? 2 : 1;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex - 1 < 0 ? 0 : tablessControl1.SelectedIndex - amount;
            Program.Animator.ShowSync(tablessControl1);
        }

        private async void tablessControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            back_btn.Enabled = tablessControl1.SelectedIndex > 0;

            if (tablessControl1.SelectedIndex == 1)
            {
                // Begin authentication process
                await loginManager.StartLoggingInAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Void to handle the form closing event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;

            if (!canCloseWithoutMsg)
            {
                if (MsgBox(Program.Lang.Strings.Messages.CloseWizard, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
            }

            // Continue with the closing event if the user has not cancelled it.
            if (!e.Cancel)
            {
                cts?.Cancel();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Forms.GlassWindow.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string code = $"{c0.Text}{c1.Text}{c2.Text}{c3.Text}-{c4.Text}{c5.Text}{c6.Text}{c7.Text}";
            Clipboard.SetText(code);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }
    }
}
