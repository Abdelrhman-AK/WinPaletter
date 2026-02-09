using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static WinPaletter.TypesExtensions.BitmapExtensions;

namespace WinPaletter
{
    public partial class GitHub_Login : UI.WP.Form
    {
        CancellationTokenSource cts;

        public GitHub_Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Icon = FormsExtensions.Icon<MainForm>();

            tablessControl1.SelectedIndex = 0;
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

            GitHub.Events.OnCountdownStarted += LoginManager_OnCountdownStarted;
            GitHub.Events.OnCountdownTick += LoginManager_OnCountdownTick;
            GitHub.Events.OnCountdownEnded += LoginManager_OnCountdownEnded;
            GitHub.Events.OnDeviceFlowInitiated += LoginManager_OnDeviceFlowInitiated;
            GitHub.Events.OnAuthorizationSuccess += LoginManager_OnAuthorizationSuccess;
            GitHub.Events.OnAuthorizationFailure += LoginManager_OnAuthorizationFailure;

            Forms.GlassWindow.Show(Forms.MainForm);
        }

        private void LoginManager_OnAuthorizationFailure(object sender, string obj)
        {
            if (IsHandleCreated && IsShown)
            {
                    Invoke(() =>
                {
                    label23.Text = obj;
                    signin_btn.Visible = true;
                    Program.Animator.HideSync(tablessControl1);
                    tablessControl1.SelectedIndex = 4;
                    Program.Animator.ShowSync(tablessControl1);
                });
            }
        }

        private async void LoginManager_OnAuthorizationSuccess(object sender, Octokit.User obj)
        {
            if (IsHandleCreated && IsShown)
            {
                progressBar2.Visible = true;

                Program.Animator.HideSync(tablessControl1);
                tablessControl1.SelectedIndex = 1;
                Program.Animator.ShowSync(tablessControl1);

                User.GitHub = await Program.GitHub.Client.User.Current();

                using (DownloadManager DM = new())
                {
                    byte[] data = await DM.ReadAsync(User.GitHub.AvatarUrl).ConfigureAwait(false);
                    using (MemoryStream ms = new(data))
                    using (Bitmap bmp = Image.FromStream(ms) as Bitmap)
                    using (Bitmap bmp_resized = bmp.Resize(pictureBox6.Size))
                    {
                        pictureBox6.Image = bmp_resized.ToCircular();
                    }
                }

                label13.Text = $"{Program.Localization.Strings.General.Welcome}, {User.GitHub.Login}!";

                Program.Animator.HideSync(tablessControl1);
                tablessControl1.SelectedIndex = 3;
                Program.Animator.ShowSync(tablessControl1);

                progressBar2.Visible = false;
            }
        }

        private void LoginManager_OnDeviceFlowInitiated(string arg1, int arg2, string arg3)
        {
            if (IsHandleCreated && IsShown)
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
            });
            }
        }

        private void LoginManager_OnCountdownEnded(object sender, EventArgs e)
        {
            if (IsHandleCreated && IsShown)
            {
                // Invoke is needed because this event is raised from a non-UI thread
                Invoke(() =>
            {
                progressBar3.Visible = false;
                tablessControl1.SelectedIndex = 0;
                signin_btn.Visible = true;
            });
            }
        }

        private void LoginManager_OnCountdownTick(object sender, int obj)
        {
            if (IsHandleCreated && IsShown)
            {
                progressBar3.Value = obj;
                githubLbl_elapsedSec.Text = $"{obj / 60:D2}:{obj % 60:D2}";
            }
        }

        private void LoginManager_OnCountdownStarted(object sender, int obj)
        {
            if (IsHandleCreated && IsShown)
            {
                Program.Animator.HideSync(tablessControl1);
                tablessControl1.SelectedIndex = 2;
                Program.Animator.ShowSync(tablessControl1);
            }
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tablessControl1);

            if (tablessControl1.SelectedIndex == 0) tablessControl1.SelectedIndex = 1;
            else if (tablessControl1.SelectedIndex == 4) tablessControl1.SelectedIndex = 0;

            Program.Animator.ShowSync(tablessControl1);

            if (tablessControl1.SelectedIndex == 1)
            {
                signin_btn.Visible = false;
                progressBar2.Visible = true;
                await Program.GitHub.StartLoggingInAsync();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            signin_btn.Visible = true;

            if (tablessControl1.SelectedIndex == 1 || tablessControl1.SelectedIndex == 2)
            {
                Program.GitHub.CancelLogin();
            }

            int amount = tablessControl1.SelectedIndex == 2 ? 2 : 1;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex - 1 < 0 ? 0 : tablessControl1.SelectedIndex - amount;
            Program.Animator.ShowSync(tablessControl1);
        }

        private async void tablessControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            back_btn.Enabled = tablessControl1.SelectedIndex > 0;
        }

        /// <summary>
        /// Void to handle the form closing event.
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            // Detach events
            GitHub.Events.OnCountdownStarted -= LoginManager_OnCountdownStarted;
            GitHub.Events.OnCountdownTick -= LoginManager_OnCountdownTick;
            GitHub.Events.OnCountdownEnded -= LoginManager_OnCountdownEnded;
            GitHub.Events.OnDeviceFlowInitiated -= LoginManager_OnDeviceFlowInitiated;
            GitHub.Events.OnAuthorizationSuccess -= LoginManager_OnAuthorizationSuccess;
            GitHub.Events.OnAuthorizationFailure -= LoginManager_OnAuthorizationFailure;

            Program.GitHub.CancelLogin();

            // Keep continuation on UI thread
            bool isLoggedIn = await Program.GitHub.IsLoggedInAsync();

            User.UpdateGitHubLoginStatus(isLoggedIn);

            // Only assign after ensuring the user object is ready
            User.GitHub = isLoggedIn ? await Program.GitHub.Client.User.Current() : null;

            // Update UI safely
            await Forms.UserSwitch.UpdateGitHubLoginData();

            base.OnFormClosing(e);
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