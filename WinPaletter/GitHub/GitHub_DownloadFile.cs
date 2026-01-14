using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_DownloadFile : Form
    {
        CancellationTokenSource cts;
        private bool _isClosing;
        private static readonly TimeSpan SpeedUpdateInterval = TimeSpan.FromMilliseconds(400);
        private DateTime _lastSpeedCheckTime = DateTime.UtcNow;
        private long _lastBytesReceived = 0;

        public GitHub_DownloadFile()
        {
            InitializeComponent();
        }

        private void GitHub_Download_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();
            Icon = FormsExtensions.Icon<GitHubMgrForm>();
        }

        public async void DownloadFileAsync(string GitHubPath, string DestinationPath)
        {
            cts ??= new();
            _lastSpeedCheckTime = DateTime.UtcNow;
            _lastBytesReceived = 0;
            string fileName = GitHub.FileSystem.FileName(GitHubPath);
            pictureBox5.Image = GitHub.FileSystem.GetIconByFileName(fileName, false);

            label8.Text = $"{DestinationPath}\\{fileName}";
            Label2.Text = 0.ToStringFileSize();

            Label3.Text = $"{0:F2} KB/s";
            Label4.Text = "--:--:--";
            ProgressBar1.Value = ProgressBar1.Minimum;

            _isClosing = false;

            Show();

            await GitHub.FileSystem.DownloadFileAsync(GitHubPath, $"{DestinationPath}\\{fileName}", cts, OnByteReceived, Close, ex => throw ex /*Forms.BugReport.ThrowError(ex)*/, Close).ConfigureAwait(false);
        }

        void OnByteReceived(long bytesReceived, long bytesToReceive)
        {
            if (_isClosing || bytesToReceive <= 0) return;

            // Always update progress bar
            int progress = (int)((bytesReceived * 100L) / bytesToReceive);
            ProgressBar1.Value = Math.Min(100, Math.Max(0, progress));

            Label2.Text = $"{bytesReceived.ToStringFileSize()}/{bytesToReceive.ToStringFileSize()}";

            DateTime now = DateTime.UtcNow;
            TimeSpan elapsed = now - _lastSpeedCheckTime;

            // Throttle speed + ETA updates
            if (elapsed < SpeedUpdateInterval) return;

            long bytesDelta = bytesReceived - _lastBytesReceived;
            if (bytesDelta <= 0) return;

            double speedBps = bytesDelta / elapsed.TotalSeconds;
            double speedKBps = speedBps / 1024d;

            double remainingBytes = bytesToReceive - bytesReceived;
            double remainingSeconds = speedBps > 0 ? remainingBytes / speedBps : 0;

            TimeSpan remaining = TimeSpan.FromSeconds(remainingSeconds);

            Label3.Text = $"{speedKBps:F2} KB/s";
            Label4.Text = remaining.TotalSeconds > 0 ? remaining.ToString(@"hh\:mm\:ss") : "--:--:--";

            // Update previous values
            _lastBytesReceived = bytesReceived;
            _lastSpeedCheckTime = now;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _isClosing = true;
            cts?.Cancel();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}