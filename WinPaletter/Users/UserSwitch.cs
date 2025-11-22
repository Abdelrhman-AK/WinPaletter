using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    public partial class UserSwitch : Form
    {
        public UserSwitch()
        {
            InitializeComponent();
            FormClosed += UserSwitch_FormClosed;
            Shown += UserSwitch_Shown;
        }

        private void UserSwitch_Shown(object sender, EventArgs e)
        {
            shown = true;
        }

        private bool shown = false;
        private Dictionary<string, string> _UsersList = [];

        private void UserSwitch_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (User.SID == null) User.SID = User.AdminSID_GrantedUAC;

            Forms.GlassWindow.Close();
        }

        private async void UserSwitch_Load(object sender, EventArgs e)
        {
            await UpdateGitHubLoginData();

            shown = false;
            this.LoadLanguage();
            ApplyStyle(this);
            checkBox1.Checked = false;
            CheckForIllegalCrossThreadCalls = false;

            User.GitHubUserSwitch += GitHub_OnSignedOut;

            Forms.GlassWindow.ShowWithGlassFocusedOnParent(Forms.MainForm);
            BringToFront();
        }

        private async void GitHub_OnSignedOut(User.GitHubUserChangeEventArgs e)
        {
            await UpdateGitHubLoginData();
        }

        public async Task UpdateGitHubLoginData()
        {
            if (User.GitHub_LoggedIn)
            {
                label2.Text = User.GitHub?.Login;

                // Start download only if avatar is null
                if (User.GitHub_Avatar is null)
                {
                    // Run download in background, don't await
                    _ = Task.Run(async () =>
                    {
                        await User.DownloadAvatarAsync().ConfigureAwait(false);

                        // Update UI safely
                        pictureBox2.Invoke(() =>
                        {
                            pictureBox2.Image = User.GitHub_Avatar?.ToCircular();
                        });
                    });
                }
                else
                {
                    pictureBox2.Image = User.GitHub_Avatar?.ToCircular();
                }

                button3.Text = Program.Lang.Strings.General.SignOut;
                button3.ImageGlyph = Properties.Resources.Glyph_SignOut;
            }
            else
            {
                label2.Text = Program.Lang.Strings.Users.GitHub_NotSigned;
                pictureBox2.Image = Properties.Resources.GitHub_SignInForFeatures;

                button3.Text = Program.Lang.Strings.General.SignIn;
                button3.ImageGlyph = Properties.Resources.Glyph_GitHub;
            }
        }

        private void ListUsers(Dictionary<string, string> UsersList)
        {
            _UsersList = UsersList;

            foreach (RadioImage radio in flowLayoutPanel1.Controls.OfType<RadioImage>())
            {
                radio.DoubleClick -= Radio_DoubleClick;
                radio.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();

            flowLayoutPanel1.Visible = false;

            RadioImage[] radios = new RadioImage[_UsersList.Count];

            foreach (KeyValuePair<string, string> user in _UsersList)
            {
                string Scheme = $"{user.Value.Split('\\').Last()}";

                if (user.Key.ToUpper() == "S-1-5-18") { Scheme += " (Default users settings)"; }

                Scheme += "\r\n" + $"{Program.Lang.Strings.Users.Computer}: {user.Value.Split('\\').First()}";

                if (user.Key.ToUpper() is not "S-1-5-18" and not "S-1-5-19" and not "S-1-5-20")
                {
                    Scheme += "\r\n" + $"{(User.IsAdmin(user.Key) ? Program.Lang.Strings.Users.TypeAdministrator : Program.Lang.Strings.Users.TypeLocalUser)}";
                }
                else
                {
                    Scheme += "\r\n" + $"{Program.Lang.Strings.Users.TypeSystem}";
                }

                RadioImage radio = new()
                {
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Padding = new(6, 0, 0, 0),
                    Checked = (User.SID != null) ? user.Key == User.SID : User.UserSID_OpenedWP != null ? user.Key == User.UserSID_OpenedWP : user.Key == User.AdminSID_GrantedUAC,
                    Size = new(250, 70),
                    Tag = user.Key,
                    Text = Scheme,
                    ForeColor = ForeColor
                };

                using (Bitmap bmp = Shell32.GetUserAccountPicture(user.Value.Split('\\').Last()) as Bitmap)
                using (Bitmap bmp_resized = bmp.Resize(48, 48))
                {
                    radio.Image = bmp_resized.ToCircular();
                }

                radio.DoubleClick += Radio_DoubleClick;
                radio.CheckedChanged += Radio_CheckedChanged;

                radios = (radios ?? Enumerable.Empty<RadioImage>()).Concat(new[] { radio }).ToArray();
            }

            flowLayoutPanel1.Controls.AddRange(radios);

            flowLayoutPanel1.Visible = true;
        }

        private void Radio_DoubleClick(object sender, EventArgs e)
        {
            Button1.PerformClick();
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioImage radio && radio.Checked)
            {
                Button1.Image = User.IsAdmin(radio.Tag.ToString()) ? Resources.Login_Admin : Resources.Login;
            }
        }

        /// <summary>
        /// Hide a dialog with users and switch into the selected one
        /// </summary>
        /// <param name="UsersList"><c>Dictionary(String, String)</c>: Key is SID, value is Domain\Username</param>
        /// <returns></returns>
        public void PickUser(Dictionary<string, string> UsersList)
        {
            ListUsers(UsersList);
            ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (RadioImage radio in flowLayoutPanel1.Controls.OfType<RadioImage>())
            {
                if (radio.Checked)
                {
                    Visible = false;

                    if (radio.Tag.ToString() == "S-1-5-18" || radio.Tag.ToString() == "S-1-5-19" || radio.Tag.ToString() == "S-1-5-20")
                    {
                        DialogResult msgResult = MsgBox(Program.Lang.Strings.Users.SYSTEM_Alert0, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, Program.Lang.Strings.Users.SYSTEM_Alert1);

                        if (msgResult == DialogResult.Yes && User.SID_Credentials_Result(radio.Tag.ToString()))
                        {
                            User.SID = radio.Tag.ToString();
                            DialogResult = DialogResult.OK;
                            Close();
                            break;
                        }
                        else
                        {
                            Visible = true;
                            return;
                        }
                    }

                    else if (User.SID_Credentials_Result(radio.Tag.ToString()))
                    {
                        User.SID = radio.Tag.ToString();
                        DialogResult = DialogResult.OK;
                        Close();
                        break;
                    }

                    Visible = true;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (shown)
            {
                if (checkBox1.Checked)
                {
                    ListUsers(User.GetUsers(true));
                }
                else
                {
                    ListUsers(User.GetUsers(false));
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (User.GitHub_LoggedIn)
            {
                await Program.GitHub.SignOutAsync();
            }
            else
            {
                Forms.GitHubLogin.ShowDialog();
            }
        }
    }
}
