using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter.Dialogs
{
    public partial class UserPassword : Form
    {
        public UserPassword()
        {
            InitializeComponent();
            button3.MouseDown += Button3_MouseDown;
            button3.MouseUp += Button3_MouseUp;
            button3.MouseLeave += Button3_MouseLeave;
            FormClosing += UserPassword_FormClosing;
        }

        private void UserPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            Forms.GlassWindow.Close();
        }

        private void Button3_MouseLeave(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = true;
        }

        private void Button3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.UseSystemPasswordChar = true;
        }

        private void Button3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.UseSystemPasswordChar = false;
        }

        private void UserPassword_Load(object sender, EventArgs e)
        {
            using (MainForm formIcon = new()) { Icon = formIcon.Icon; }
            this.LoadLanguage();
            ApplyStyle(this);
            panel1.BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            int r = button2.Right;
            button2.Text = string.Format(Program.Lang.UserSwitch_LoginAs, new SecurityIdentifier(User.AdminSID_GrantedUAC).Translate(typeof(NTAccount)).ToString().Split('\\').Last());
            button2.Width = (int)button2.Text.Measure(button2.Font).Width + 20;
            button2.Left = r - button2.Width;

            Forms.GlassWindow.Show();

            this.DropEffect(new Padding(0, 0, 0, panel1.Height), true, DWM.FormStyle.Tabbed);

            Refresh();
        }

        public Tuple<DialogResult, string> GetPassword(string SID)
        {
            labelAlt3.Text = string.Format(Program.Lang.UserSwitch_OnComputer, new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').First());
            labelAlt1.Text = new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').Last();
            pictureBox1.Image = ((Bitmap)NativeMethods.Shell32.GetUserAccountPicture(labelAlt1.Text)).Resize(128, 128).ToCircle();

            switch (ShowDialog())
            {
                case DialogResult.OK: return new Tuple<DialogResult, string>(DialogResult.OK, textBox1.Text);

                case DialogResult.Ignore: return new Tuple<DialogResult, string>(DialogResult.Ignore, null);

                default: return new Tuple<DialogResult, string>(DialogResult.Cancel, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (User.UpdateToken(textBox1.Text))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    throw new User.LogonFailureException($"ERROR_LOGON_FAILURE ({Marshal.GetLastWin32Error()}): {Program.Lang.UserSwitch_ERROR_LOGON_FAILURE}");
                }
            }
            catch (User.LogonFailureException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.LogonInvalidParameters ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (UnauthorizedAccessException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.NoTokenException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.LogonTypeNotGrantedException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.AccountRestrictionException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.InvalidLogonHoursException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.PasswordExpiredException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.AccountDisabledException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.AccountLockedOutException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.NoSuchUserException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.NoLogonServersException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
            catch (User.PasswordMustChangeException ex)
            {
                MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
        }
    }
}