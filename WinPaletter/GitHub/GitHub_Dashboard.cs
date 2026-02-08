using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_Dashboard : UI.WP.Form
    {
        public GitHub_Dashboard()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.UseDecorationPattern = true;
            groupBox2.UseDecorationPattern = true;

            if (!User.GitHub_LoggedIn || !Program.IsNetworkAvailable)
            {
                tablessControl1.SelectedIndex = 1;
            }
            else
            {
                label2.Text = User.GitHub.Login;
                User_GitHubAvatarUpdated();
                tablessControl1.SelectedIndex = 0;
            }
        }

        public void Show(Size parentButtonSize, Point parentButtonLocation)
        {
            Location = parentButtonLocation + new Size(parentButtonSize.Width - Width, parentButtonSize.Height);
            base.Show();
        }

        private void User_GitHubAvatarUpdated()
        {
            Bitmap avatar = null;

            // Wait for avatar to exist
            avatar = User.GitHub_Avatar;

            if (avatar is null)
            {
                avatar = Properties.Resources.GitHub_SignInForFeatures.Clone() as Bitmap;
            }

            using (Bitmap avatar_resized = avatar.Resize(pictureBox2.Size))
            {
                pictureBox2.Image = avatar_resized.ToCircular();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            Forms.GitHub_Login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.GitHub_Mgr);
            Close();
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            await Program.GitHub.SignOutAsync();
            Close();
        }
    }
}
