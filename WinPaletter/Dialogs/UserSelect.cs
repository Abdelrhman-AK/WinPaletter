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
            this.Shown += UserSelect_Shown;
        }

        private void UserSelect_Shown(object sender, EventArgs e)
        {
            shown = true;
        }

        private bool shown = false;
        private Dictionary<string, string> _UsersList = new();

        private void UserSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { Forms.BK.Close(); } catch { }
        }

        private void UserSelect_Load(object sender, EventArgs e)
        {
            shown = false;
            Icon = Forms.MainFrm.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
            checkBox1.Checked = false;
            CheckForIllegalCrossThreadCalls = false;

            try { Forms.BK.Close(); } catch { }
            try { Forms.BK.Show(); } catch { }

            BringToFront();
        }

        private void ListUsers(Dictionary<string, string> UsersList)
        {
            _UsersList = UsersList;

            foreach (RadioImage radio in flowLayoutPanel1.Controls.OfType<RadioImage>())
            {
                radio.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();

            flowLayoutPanel1.Visible = false;

            RadioImage[] radios = new RadioImage[_UsersList.Count];

            foreach (KeyValuePair<string, string> user in _UsersList)
            {
                string Scheme = $"{user.Value.Split('\\').Last()}";

                if (user.Key.ToUpper() == "S-1-5-18") { Scheme += " (Default users settings)"; }

                Scheme += "\r\n" + $"{Program.Lang.UserSwitch_Computer}: {user.Value.Split('\\').First()}";

                if (user.Key.ToUpper() != "S-1-5-18" && user.Key.ToUpper() != "S-1-5-19" && user.Key.ToUpper() != "S-1-5-20")
                {
                    Scheme += "\r\n" + $"{(Users.IsAdmin(user.Key) ? Program.Lang.UserSwitch_TypeAdministrator : Program.Lang.UserSwitch_TypeLocalUser)}";
                }
                else
                {
                    Scheme += "\r\n" + $"{Program.Lang.UserSwitch_TypeSystem}";
                }

                RadioImage radio = new()
                {
                    ImageWithText = true,
                    ShowText = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Checked = user.Key == Users.SID,
                    Size = new Size(250, 70),
                    Tag = user.Key,
                    Image = NativeMethods.Shell32.GetUserAccountPicture(user.Value.Split('\\').Last()).Resize(48, 48),
                    Text = Scheme,
                    ForeColor = this.ForeColor
                };

                radios = (radios ?? Enumerable.Empty<RadioImage>()).Concat(new[] { radio }).ToArray();
            }

            flowLayoutPanel1.Controls.AddRange(radios);

            flowLayoutPanel1.Visible = true;
        }

        public DialogResult PickUser(Dictionary<string, string> UsersList)
        {
            ListUsers(UsersList);
            return this.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (RadioImage radio in flowLayoutPanel1.Controls.OfType<RadioImage>())
            {
                if (radio.Checked)
                {
                    Users.SID = radio.Tag.ToString();
                    break;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender)
        {
            if (shown)
            {
                if (checkBox1.Checked)
                {
                    ListUsers(Users.GetUsers(true));
                }
                else
                {
                    ListUsers(Users.GetUsers(false));
                }
            }
        }
    }
}
