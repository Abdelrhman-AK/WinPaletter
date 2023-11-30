using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    public partial class UserSwitch : Form
    {
        public UserSwitch()
        {
            InitializeComponent();
            this.FormClosed += UserSwitch_FormClosed;
            this.Shown += UserSwitch_Shown;
        }

        private void UserSwitch_Shown(object sender, EventArgs e)
        {
            shown = true;
        }

        private bool shown = false;
        private Dictionary<string, string> _UsersList = new();

        private void UserSwitch_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (User.SID == null)
                User.SID = User.AdminSID_GrantedUAC;

            try { Forms.BK.Close(); } catch { }
        }

        private void UserSwitch_Load(object sender, EventArgs e)
        {
            shown = false;
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

                Scheme += "\r\n" + $"{Program.Lang.UserSwitch_Computer}: {user.Value.Split('\\').First()}";

                if (user.Key.ToUpper() != "S-1-5-18" && user.Key.ToUpper() != "S-1-5-19" && user.Key.ToUpper() != "S-1-5-20")
                {
                    Scheme += "\r\n" + $"{(User.IsAdmin(user.Key) ? Program.Lang.UserSwitch_TypeAdministrator : Program.Lang.UserSwitch_TypeLocalUser)}";
                }
                else
                {
                    Scheme += "\r\n" + $"{Program.Lang.UserSwitch_TypeSystem}";
                }

                RadioImage radio = new()
                {
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Padding = new(5, 0, 0, 0),
                    Checked = (User.SID != null) ? user.Key == User.SID : user.Key == User.AdminSID_GrantedUAC,
                    Size = new(250, 70),
                    Tag = user.Key,
                    Image = NativeMethods.Shell32.GetUserAccountPicture(user.Value.Split('\\').Last()).Resize(48, 48),
                    Text = Scheme,
                    ForeColor = this.ForeColor
                };

                radio.DoubleClick += Radio_DoubleClick;

                radios = (radios ?? Enumerable.Empty<RadioImage>()).Concat(new[] { radio }).ToArray();
            }

            flowLayoutPanel1.Controls.AddRange(radios);

            flowLayoutPanel1.Visible = true;
        }

        private void Radio_DoubleClick(object sender, EventArgs e)
        {
            Button1.PerformClick();
        }

        /// <summary>
        /// Show a dialog with users and switch into the selected one
        /// </summary>
        /// <param name="UsersList"><c>Dictionary(String, String)</c>: Key is SID, value is Domain\Username</param>
        /// <returns></returns>
        public void PickUser(Dictionary<string, string> UsersList)
        {
            ListUsers(UsersList);
            this.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (RadioImage radio in flowLayoutPanel1.Controls.OfType<RadioImage>())
            {
                if (radio.Checked)
                {
                    if (radio.Tag.ToString() == "S-1-5-18" || radio.Tag.ToString() == "S-1-5-19" || radio.Tag.ToString() == "S-1-5-20")
                    {
                        DialogResult msgResult = MsgBox(Program.Lang.UserSwitch_SYSTEM_Alert0, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, Program.Lang.UserSwitch_SYSTEM_Alert1);

                        if (msgResult == DialogResult.Yes)
                        {
                            User.SID = radio.Tag.ToString();
                            break;
                        }

                        else if (msgResult == DialogResult.No)
                        {
                            User.SID = User.AdminSID_GrantedUAC;
                            break;
                        }

                        else if (msgResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    else
                    {
                        User.SID = radio.Tag.ToString();
                        break;
                    }
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
                    ListUsers(User.GetUsers(true));
                }
                else
                {
                    ListUsers(User.GetUsers(false));
                }
            }
        }
    }
}
