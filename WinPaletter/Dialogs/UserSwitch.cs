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
    public partial class UserSwitch : UI.WP.Form
    {
        private bool shown = false;
        private Dictionary<string, string> _UsersList = [];

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

        private void UserSwitch_FormClosed(object sender, FormClosedEventArgs e)
        {
            User.SID ??= User.AdminSID_GrantedUAC;
            Forms.GlassWindow.Close();
        }

        private async void UserSwitch_Load(object sender, EventArgs e)
        {
            shown = false;

            checkBox1.Checked = false;

            User.GitHubUserSwitch += GitHub_OnSignedOut;

            Forms.GlassWindow.Show(Forms.MainForm);
            BringToFront();

            await UpdateGitHubLoginData();
        }

        private async void GitHub_OnSignedOut(User.GitHubUserChangeEventArgs e)
        {
            await UpdateGitHubLoginData();
        }

        public async Task UpdateGitHubLoginData()
        {
            // Prepare the glyph bitmap safely
            Image glyph = User.GitHub_LoggedIn ? (Image)Properties.Resources.Glyph_SignOut.Clone() : (Image)Properties.Resources.Glyph_GitHub.Clone();

            Forms.MainForm.Invoke(() =>
            {
                label2.Text = User.GitHub_LoggedIn ? User.GitHub?.Login ?? Program.Localization.Strings.Users.GitHub_NotSigned : Program.Localization.Strings.Users.GitHub_NotSigned;
                button3.Text = User.GitHub_LoggedIn ? Program.Localization.Strings.General.SignOut : Program.Localization.Strings.General.SignIn;

                button3.ImageGlyph?.Dispose();
                button3.ImageGlyph = glyph;
            });


            Bitmap avatar = null;

            if (User.GitHub_LoggedIn)
            {
                // Wait for avatar to exist
                if (User.GitHub_Avatar is null)
                {
                    await User.DownloadAvatarAsync();
                }

                if (User.GitHub_Avatar != null)
                {
                    avatar = User.GitHub_Avatar.ToCircular();
                }
            }

            if (avatar is null)
            {
                avatar = Properties.Resources.GitHub_SignInForFeatures.Clone() as Bitmap;
            }

            pictureBox2.Image?.Dispose();
            pictureBox2.Image = avatar;
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

                Scheme += "\r\n" + $"{Program.Localization.Strings.Users.Computer}: {user.Value.Split('\\').First()}";

                if (user.Key.ToUpper() is not "S-1-5-18" and not "S-1-5-19" and not "S-1-5-20")
                {
                    Scheme += "\r\n" + $"{(User.IsAdmin(user.Key) ? Program.Localization.Strings.Users.TypeAdministrator : Program.Localization.Strings.Users.TypeLocalUser)}";
                }
                else
                {
                    Scheme += "\r\n" + $"{Program.Localization.Strings.Users.TypeSystem}";
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
                        DialogResult msgResult = MsgBox(Program.Localization.Strings.Users.SYSTEM_Alert0, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, Program.Localization.Strings.Users.SYSTEM_Alert1);

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
                Forms.GitHub_Login.ShowDialog();
            }
        }
    }
}
