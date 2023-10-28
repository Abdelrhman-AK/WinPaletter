using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.WP;

namespace WinPaletter.Dialogs
{
    public partial class UserSelect : Form
    {
        public UserSelect()
        {
            InitializeComponent();
            this.FormClosing += UserSelect_FormClosing;
        }

        private Dictionary<string, string> _UsersList = new();

        private void UserSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { Forms.BK.Close(); } catch { }
        }

        private void UserSelect_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainFrm.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
            CheckForIllegalCrossThreadCalls = false;
            checkBox1.Checked = Program.Settings.UsersServices.RemeberLastUser;

            try { Forms.BK.Close(); } catch { }
            try { Forms.BK.Show(); } catch { }

            BringToFront();
        }

        public DialogResult PickUser(Dictionary<string, string> UsersList)
        {
            _UsersList = UsersList;

            foreach (RadioImage radio in flowLayoutPanel1.Controls.OfType<RadioImage>())
            {
                radio.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();

            foreach (KeyValuePair<string, string> user in _UsersList)
            {
                RadioImage radio = new()
                {
                    ImageWithText = true,
                    ShowText = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Checked = user.Key == Users.UserSID,
                    Size = new Size(250, 70),
                    Tag = user.Key,
                    Image = NativeMethods.Shell32.GetUserAccountPicture(user.Value.Split('\\').Last()).Resize(48, 48),
                    Text = $"{user.Value.Split('\\').Last()}\r\n{Program.Lang.UserSwitch_Computer}: {user.Value.Split('\\').First()}\r\n{(Users.IsAdmin(user.Key) ? Program.Lang.UserSwitch_TypeAdministrator : Program.Lang.UserSwitch_TypeLocalUser)}"
                };

                flowLayoutPanel1.Controls.Add(radio);
            }

            return this.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (RadioImage radio in flowLayoutPanel1.Controls.OfType<RadioImage>())
            {
                if (radio.Checked)
                {
                    Users.Domain_UserName = _UsersList[radio.Tag.ToString()];
                    Program.Settings.UsersServices.LastUserSID = radio.Tag.ToString();
                    Users.UserSID = radio.Tag.ToString();
                    Users.UpdatePathsFromSID(Users.UserSID);
                    break;
                }
            }

            Program.Settings.UsersServices.RemeberLastUser = checkBox1.Checked;
            Program.Settings.UsersServices.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
