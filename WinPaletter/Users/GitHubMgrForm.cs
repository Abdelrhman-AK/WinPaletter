using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHubMgrForm : Form
    {
        CancellationTokenSource cts = new();

        public GitHubMgrForm()
        {
            InitializeComponent();
        }

        private async void GitHubManager_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();

            AddViewsToButton();

            await UpdateGitHubLoginData(); // Aasync login method

            await GitHub.FileSystem.PopulateRepositoryAsync(treeView1, listView1, breadcrumbControl1, cts);

            // After populating the tree for the first time
            UpdateNavigationButtons();
        }

        void AddViewsToButton()
        {
            button7.Menu.Items.Clear();

            // Define the possible ListView views
            Dictionary<string, View> views = new()
            {
                { Program.Lang.Strings.GitHubStrings.Explorer_View_LargeIcons, View.LargeIcon },
                { Program.Lang.Strings.GitHubStrings.Explorer_View_SmallIcons, View.SmallIcon },
                { Program.Lang.Strings.GitHubStrings.Explorer_View_List, View.List },
                { Program.Lang.Strings.GitHubStrings.Explorer_View_Details, View.Details },
                { Program.Lang.Strings.GitHubStrings.Explorer_View_Tiles, View.Tile }
            };

            // Create ToolStripMenuItems as radio buttons
            foreach (KeyValuePair<string, View> kvp in views)
            {
                ToolStripMenuItem item = new(kvp.Key)
                {
                    CheckOnClick = true,
                    Checked = listView1.View == kvp.Value,
                    Tag = kvp.Value // store the View enum in Tag for easy access
                };

                // Click handler
                item.Click += (s, e) =>
                {
                    // Uncheck all other items
                    foreach (ToolStripMenuItem other in button7.Menu.Items)
                    {
                        if (other != item)
                            other.Checked = false;
                    }

                    // Set the ListView view
                    listView1.View = (View)item.Tag;

                    // Ensure this one is checked
                    item.Checked = true;
                };

                button7.Menu.Items.Add(item);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Define the ordered views
            View[] views = { View.LargeIcon, View.SmallIcon, View.List, View.Details, View.Tile };

            // Find the index of the current view
            int currentIndex = Array.IndexOf(views, listView1.View);
            int nextIndex = (currentIndex + 1) % views.Length;

            // Set the next view
            View nextView = views[nextIndex];
            listView1.View = nextView;

            // Update the menu items to match
            foreach (ToolStripMenuItem item in button7.Menu.Items) item.Checked = (View)item.Tag == nextView;
        }

        public async Task UpdateGitHubLoginData()
        {
            Bitmap avatar = null;

            if (User.GitHub_LoggedIn)
            {
                Invoke(() =>
                {
                    label1.Text = User.GitHub.Login;
                    label2.Text = User.GitHub.Login;

                    url_lbl.Text = User.GitHub.HtmlUrl;
                    bio_lbl.Text = User.GitHub.Bio;

                    followers_count_lbl.Font = Fonts.ConsoleLarge;
                    following_count_lbl.Font = Fonts.ConsoleLarge;
                    created_lbl.Font = Fonts.ConsoleMedium;
                    updated_lbl.Font = Fonts.ConsoleMedium;

                    followers_count_lbl.Text = User.GitHub.Followers.ToString();
                    following_count_lbl.Text = User.GitHub.Following.ToString();

                    email_lbl.Text = User.GitHub.Email ?? Program.Lang.Strings.GitHubStrings.Overview_NoEmail;
                    contry_lbl.Text = User.GitHub.Location ?? Program.Lang.Strings.GitHubStrings.Overview_NoLocation;

                    created_lbl.Text = ToFriendlyString(User.GitHub.CreatedAt);
                    updated_lbl.Text = ToFriendlyString(User.GitHub.UpdatedAt);
                });

                if (User.GitHub_LoggedIn)
                {
                    // Wait for avatar to exist
                    if (User.GitHub_Avatar is null)
                    {
                        await User.DownloadAvatarAsync();
                    }

                    if (User.GitHub_Avatar != null)
                    {
                        avatar = User.GitHub_Avatar;
                    }
                }
            }

            if (avatar is null)
            {
                avatar = Properties.Resources.GitHub_SignInForFeatures.Clone() as Bitmap;
            }

            Invoke(() =>
            {
                using (Bitmap avatar_resized = avatar.Resize(pictureBox1.Size))
                using (Bitmap avatar_resized_2 = avatar.Resize(pictureBox2.Size))
                {
                    pictureBox1.Image = avatar_resized.ToCircular();
                    pictureBox2.Image = avatar_resized_2.ToCircular();
                }
            });
        }

        public string ToFriendlyString(DateTimeOffset dateTime)
        {
            var now = DateTimeOffset.Now;
            var span = now - dateTime;

            if (span.TotalSeconds < 60)
                return $"{(int)span.TotalSeconds} {((int)span.TotalSeconds > 1 ? Program.Lang.Strings.GitHubStrings.Timing_Seconds : Program.Lang.Strings.GitHubStrings.Timing_Second)} {Program.Lang.Strings.GitHubStrings.Timing_Ago}";
            if (span.TotalMinutes < 60)
                return $"{(int)span.TotalMinutes} {((int)span.TotalMinutes > 1 ? Program.Lang.Strings.GitHubStrings.Timing_Minutes : Program.Lang.Strings.GitHubStrings.Timing_Minute)} {Program.Lang.Strings.GitHubStrings.Timing_Ago}";
            if (span.TotalHours < 24)
                return $"{(int)span.TotalHours} {((int)span.TotalHours > 1 ? Program.Lang.Strings.GitHubStrings.Timing_Hours : Program.Lang.Strings.GitHubStrings.Timing_Hour)} {Program.Lang.Strings.GitHubStrings.Timing_Ago}";

            var dayDiff = (now.Date - dateTime.Date).Days;
            if (dayDiff == 0)
                return $"{Program.Lang.Strings.GitHubStrings.Timing_Today} {dateTime:hh:mm tt}";
            if (dayDiff == 1)
                return $"{Program.Lang.Strings.GitHubStrings.Timing_Yesterday} {dateTime:hh:mm tt}";
            if (dayDiff == -1)
                return $"{Program.Lang.Strings.GitHubStrings.Timing_Tomorrow} {dateTime:hh:mm tt}";

            return dateTime.ToString("f");
        }

        private void url_lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = url_lbl.Text;
            if (!url.StartsWith("https://") || !url.StartsWith("http://")) url = $"https://{url}";
            Process.Start(url);
        }

        private void UpdateNavigationButtons()
        {
            button2.Enabled = GitHub.FileSystem.CanGoBack;
            button4.Enabled = GitHub.FileSystem.CanGoForward;
            button5.Enabled = GitHub.FileSystem.CanGoUp;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoBack(treeView1, listView1);
            UpdateNavigationButtons();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoForward(treeView1, listView1);
            UpdateNavigationButtons();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoUp(treeView1, listView1);
            UpdateNavigationButtons();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateNavigationButtons();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.RefreshAsync(treeView1, listView1, breadcrumbControl1, cts);
        }

        private void breadcrumbControl1_StopRequested()
        {
            cts?.Cancel();
            cts = new();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            explorer_controls.Visible = tabControl1.SelectedIndex == 1;
        }
    }
}
