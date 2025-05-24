using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Download progress form for downloading themes from the store.
    /// </summary>
    public partial class Store_DownloadProgress
    {
        /// <summary>
        /// URL of the theme to download.
        /// </summary>
        public string URL;

        /// <summary>
        /// File to save the downloaded theme.
        /// </summary>
        public string File;

        /// <summary>
        /// Name of the theme.
        /// </summary>
        public string ThemeName;

        /// <summary>
        /// Version of the theme.
        /// </summary>
        public string ThemeVersion;

        /// <summary>
        /// Stopwatch to calculate the download speed.
        /// </summary>
        private readonly Stopwatch SW = new();


        private DownloadManager _ThemeDownloader;
        /// <summary>
        /// Download manager for downloading the theme.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Store_DownloadProgress"/> class.
        /// </summary>
        public Store_DownloadProgress()
        {
            ThemeDownloader = new();
            InitializeComponent();
        }

        private async void Store_DownloadProgress_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<Store>();

            Label1.Text = string.Format(Program.Lang.Strings.Store.DownloadingPackForTheme, ThemeName, ThemeVersion);
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
            // Calculate the download speed
            long Speed = (long)Math.Round(e.BytesReceived / SW.Elapsed.TotalSeconds, 2);
            Label3.SetText(Speed.ToStringFileSize(true));

            // Update the progress bar and labels
            if (e.TotalBytesToReceive > 0L)
            {
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Continuous;
                ProgressBar1.Value = (int)e.ProgressPercentage;
                Label2.SetText($"{e.BytesReceived.ToStringFileSize()}/{e.TotalBytesToReceive.ToStringFileSize()}");
                TimeSpan time = TimeSpan.FromSeconds((e.TotalBytesToReceive - e.BytesReceived) / (double)Speed);
                Label4.SetText(time.ToString(@"mm\:ss"));
            }
            else
            {
                // Marquee style for indeterminate progress
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Marquee;
                ProgressBar1.Value = 0;
                Label2.SetText(e.BytesReceived.ToStringFileSize());
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