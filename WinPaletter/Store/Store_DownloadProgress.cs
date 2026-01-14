using System;
using System.ComponentModel;
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

        private static readonly TimeSpan SpeedUpdateInterval = TimeSpan.FromMilliseconds(400);
        private DateTime _lastSpeedUpdate = DateTime.UtcNow;
        private long _lastBytes = 0;

        private DownloadManager _ThemeDownloader;
        /// <summary>
        /// Download manager for downloading the theme.
        /// </summary>
        private DownloadManager ThemeDownloader
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get => _ThemeDownloader;

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

        private void ThemeDownloader_DownloadProgressChanged(
          object sender,
          DownloadManager.DownloadProgressEventArgs e)
        {
            // Progress bar is cheap → update always
            if (e.TotalBytesToReceive > 0L)
            {
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Continuous;
                ProgressBar1.Value = (int)e.ProgressPercentage;
                Label2.SetText($"{e.BytesReceived.ToStringFileSize()}/{e.TotalBytesToReceive.ToStringFileSize()}");
            }
            else
            {
                ProgressBar1.Style = UI.WP.ProgressBar.ProgressBarStyle.Marquee;
                ProgressBar1.Value = 0;
                Label2.SetText(e.BytesReceived.ToStringFileSize());
                Label4.SetText(string.Empty);
                return;
            }

            // Throttle speed + ETA updates
            DateTime now = DateTime.UtcNow;
            TimeSpan elapsed = now - _lastSpeedUpdate;

            if (elapsed < SpeedUpdateInterval) return;

            long bytesDelta = e.BytesReceived - _lastBytes;
            if (bytesDelta <= 0) return;

            double speedBps = bytesDelta / elapsed.TotalSeconds;
            if (speedBps <= 0) return;

            // Speed
            long speed = (long)speedBps;
            Label3.SetText(speed.ToStringFileSize(true));

            // Remaining time
            double remainingSeconds = (e.TotalBytesToReceive - e.BytesReceived) / speedBps;

            TimeSpan remaining = TimeSpan.FromSeconds(Math.Max(0, remainingSeconds));
            Label4.SetText(remaining.ToString(@"mm\:ss"));

            // Update tracking values
            _lastBytes = e.BytesReceived;
            _lastSpeedUpdate = now;
        }

        private void ThemeDownloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
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
            if (ThemeDownloader.IsBusy) ThemeDownloader.StopDownload();
            ThemeDownloader.Dispose();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (ThemeDownloader.IsBusy) ThemeDownloader.StopDownload(true);
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}