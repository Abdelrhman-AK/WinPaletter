using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class About
    {
        public About()
        {
            InitializeComponent();
        }
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://icons8.com/app/windows");
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/cyotek/Cyotek.Windows.Forms.ColorPicker");
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/KSemenenko/ColorThief");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Repository);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.reddit.com/user/abdelrhman_ak");
        }

        private void About_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainFrm.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
            Label2.Text = Program.Version;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.codeproject.com/Articles/548769/Animator-for-WinForms");
        }

        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.reddit.com/r/Windows11/comments/sw15u0/dark_theme_did_you_notice_the_ugly_pale_accent");
            Process.Start("https://www.reddit.com/r/Windows11/comments/tkvet4/pitch_black_themereg_now_for_ctrlaltdel_as_well");
        }

        private void LinkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/awaescher/FluentTransitions");
        }

        private void LinkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.microsoft.com");
        }

        private void LinkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/JetBrains/JetBrainsMono");
        }

        private void LinkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/JamesNK/Newtonsoft.Json");
        }

        private void LinkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ookii-dialogs/ookii-dialogs-winforms");
        }

        private void LinkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.codeproject.com/Articles/18603/Advanced-UxTheme-wrapper");
        }

        private void LinkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://imageprocessor.org");
        }

        private void LinkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/evanolds/AnimCur");
        }

        private void LinkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Tyrrrz/Ressy");
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/nptr/msstyleEditor");
        }
    }
}