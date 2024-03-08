using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Store_DownloadProgress
    {
        public string URL;
        public string File;
        public string ThemeName;
        public string ThemeVersion;

        private Stopwatch SW = new();
        private DownloadManager _ThemeDownloader;

        private DownloadManager ThemeDownloader
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ThemeDownloader;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ThemeDownloader != null)
                {
                    _ThemeDownloader.DownloadProgressChanged -= ThemeDownloader_DownloadProgressChanged;
                    _ThemeDownloader.DownloadFileCompleted -= ThemeDownloader_DownloadFileCompleted;
                }

                _ThemeDownloader = value;
                if (_ThemeDownloader != null)
                {
                    _ThemeDownloader.DownloadProgressChanged += ThemeDownloader_DownloadProgressChanged;
                    _ThemeDownloader.DownloadFileCompleted += ThemeDownloader_DownloadFileCompleted;
                }
            }
        }

        public Store_DownloadProgress()
        {
            ThemeDownloader = new();
            InitializeComponent();
        }

        private async void Store_DownloadProgress_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            using (Store formIcon = new()) { Icon = formIcon.Icon; }

            Label1.Text = string.Format(Program.Lang.Store_DownloadingPackForTheme, ThemeName, ThemeVersion);
            Label2.Text = string.Empty;
            Label3.Text = string.Empty;
            Label4.Text = string.Empty;
            ProgressBar1.Value = 0;

            SW.Reset();
            SW.Start();

            ThemeDownloader = new();
            await ThemeDownloader.DownloadFileAsync(URL, File);
        }

        private void ThemeDownloader_DownloadProgressChanged(object sender, DownloadManager.DownloadProgressEventArgs e)
        {
            long Speed = (long)Math.Round(e.BytesReceived / SW.Elapsed.TotalSeconds, 2);
            Label3.SetText(Speed.SizeString(true));

            if (e.TotalBytesToReceive > 0L)
            {
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Continuous;
                ProgressBar1.Value = (int)e.ProgressPercentage;
                Label2.SetText($"{e.BytesReceived.SizeString()}/{e.TotalBytesToReceive.SizeString()}");
                TimeSpan time = TimeSpan.FromSeconds((e.TotalBytesToReceive - e.BytesReceived) / (double)Speed);
                Label4.SetText(time.ToString(@"mm\:ss"));
            }
            else
            {
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Marquee;
                ProgressBar1.Value = 0;
                Label2.SetText(e.BytesReceived.SizeString());
                Label4.SetText(string.Empty);
            }
        }

        private void ThemeDownloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            SW.Stop();
            SW.Reset();

            if (e.Cancelled | e.Error is not null)
            {
                DialogResult = DialogResult.Abort;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }

            Close();
        }

        private void Store_DownloadProgress_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ThemeDownloader.IsBusy)
                ThemeDownloader.StopDownload();
            ThemeDownloader.Dispose();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (ThemeDownloader.IsBusy)
                ThemeDownloader.StopDownload();
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}