using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.GlobalVariables;

namespace WinPaletter
{
    public partial class DownloadManager_Dlg
    {
        private const int MinParallel = 1;
        private const int MaxParallel = 6;

        private readonly object _sync = new();

        private long _totalBytesAllFiles;
        private long _totalBytesDownloaded;

        private int _completedFiles;
        private int _activeParallel = 2;

        private DateTime _lastOverallUpdate = DateTime.UtcNow;
        private long _lastOverallBytes;
        private double _smoothedSpeedBps;

        private bool _hasFinished;

        private readonly Dictionary<DownloadManager, FileState> _activeDownloads = [];
        private readonly DownloadManager dm = new();

        private sealed class FileState
        {
            public ListViewItem Item;
            public long LastBytes;
            public long TotalBytes;
            public string FileName;
            public string SavePath;
            public Bitmap Icon;
        }

        public DownloadManager_Dlg()
        {
            InitializeComponent();
        }

        #region Public API

        /// <summary>
        /// Download a single file.
        /// </summary>
        public DialogResult DownloadFile(string url, string saveAs, long size = -1, Bitmap icon = null) => DownloadFile([(url, saveAs, size, icon)]);

        /// <summary>
        /// Download a list of files.
        /// </summary>
        public DialogResult DownloadFile(List<(string url, string saveAs, long size, Bitmap icon)> files)
        {
            if (files == null || files.Count == 0) return DialogResult.OK;

            PrepareListView(files);
            PrecalculateTotalSize(files);
            StartParallelDownloads();

            return ShowDialog();
        }

        #endregion

        #region Initialization

        private void PrepareListView(List<(string url, string saveAs, long size, Bitmap icon)> files)
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.SmallImageList??= new();
            listView1.SmallImageList?.Images.Clear();
            listView1.SmallImageList.ImageSize = new(18, 24);
            listView1.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

            listView1.Columns.Add("File", 220);
            listView1.Columns.Add("URL", 260);
            listView1.Columns.Add("Downloaded", 100);
            listView1.Columns.Add("Size", 100);

            foreach (var (url, saveAs, size, icon) in files)
            {
                // Use passed size if valid, otherwise fallback
                long finalSize = size >= 0 ? size : dm.GetFileSizeFromUrl(url);

                var item = new ListViewItem(Path.GetFileName(saveAs));
                item.SubItems.Add(url);
                item.SubItems.Add(0.ToStringFileSize());
                item.SubItems.Add(finalSize.ToStringFileSize());

                using (Icon ico = icon != null ? null : NativeMethods.Shell32.GetIconFromExtension(Path.GetExtension(saveAs), NativeMethods.Shell32.IconSize.Small))
                {
                    item.Tag = new FileState
                    {
                        Item = item,
                        TotalBytes = finalSize,
                        FileName = Path.GetFileName(saveAs),
                        SavePath = saveAs,
                        Icon = icon ?? ico.ToBitmap()
                    };
                }
                
                listView1.SmallImageList.AddWithAlpha(Path.GetExtension(saveAs), (item.Tag as FileState).Icon);
                item.ImageKey = Path.GetExtension(saveAs);
                listView1.Items.Add(item);
            }

            progressBar1.Value = 0;
            progressBar2.Value = 0;

            Label2.Text = "0 / 0";
            label10.Text = "0 / 0";
            Label3.Text = "0 B/s";
            Label4.Text = "--:--";
            label9.Text = string.Empty;
        }

        private void PrecalculateTotalSize(List<(string url, string saveAs, long size, Bitmap icon)> files)
        {
            _totalBytesAllFiles = files.Sum(f => f.size >= 0 ? f.size : dm.GetFileSizeFromUrl(f.url));

            label10.Text = $"0 / {_totalBytesAllFiles.ToStringFileSize()}";
        }

        #endregion

        #region Parallel Engine

        private void StartParallelDownloads()
        {
            _hasFinished = false;
            _completedFiles = 0;
            _totalBytesDownloaded = 0;
            _lastOverallBytes = 0;
            _lastOverallUpdate = DateTime.UtcNow;
            _smoothedSpeedBps = 0;

            Task.Run(async () =>
            {
                var queue = new Queue<ListViewItem>(listView1.Items.Cast<ListViewItem>());
                var running = new List<Task>();

                while (!_hasFinished && (queue.Count > 0 || running.Count > 0))
                {
                    while (!_hasFinished && queue.Count > 0 && running.Count < _activeParallel)
                    {
                        var item = queue.Dequeue();
                        var state = (FileState)item.Tag;

                        var task = Task.Run(async () =>
                        {
                            var dm = new DownloadManager();
                            dm.DownloadProgressChanged += DownloadProgressChanged;
                            dm.DownloadFileCompleted += DownloadFileCompleted;

                            lock (_sync)
                                _activeDownloads[dm] = state;

                            Invoke(() =>
                            {
                                label9.Text = state.FileName;
                                pictureBox5.Image = state.Icon;
                                progressBar1.Value = 0;
                            });

                            await dm.DownloadFileAsync(item.SubItems[1].Text, state.SavePath);
                        });

                        running.Add(task);
                    }

                    if (running.Count > 0)
                    {
                        var finished = await Task.WhenAny(running);
                        running.Remove(finished);
                    }
                }

                if (_hasFinished) return;

                Invoke(() =>
                {
                    DialogResult = DialogResult.OK;
                    Close();
                });
            });
        }

        #endregion

        #region Progress Handling

        private void DownloadProgressChanged(object sender, DownloadManager.DownloadProgressEventArgs e)
        {
            if (_hasFinished) return;
            if (sender is not DownloadManager dm) return;

            FileState state;
            lock (_sync)
                if (!_activeDownloads.TryGetValue(dm, out state)) return;

            long delta = e.BytesReceived - state.LastBytes;
            if (delta <= 0) return;

            state.LastBytes = e.BytesReceived;

            lock (_sync)
                _totalBytesDownloaded += delta;

            DateTime now = DateTime.UtcNow;
            TimeSpan elapsed = now - _lastOverallUpdate;

            if (elapsed.TotalMilliseconds >= 500)
            {
                long bytesDelta = _totalBytesDownloaded - _lastOverallBytes;
                double speedBps = bytesDelta / elapsed.TotalSeconds;

                AdjustParallelism(speedBps);

                _lastOverallBytes = _totalBytesDownloaded;
                _lastOverallUpdate = now;

                double remainingSeconds = (_totalBytesAllFiles - _totalBytesDownloaded) / Math.Max(1, speedBps);

                Invoke(() =>
                {
                    progressBar2.Value = (int)(_totalBytesDownloaded * 100 / _totalBytesAllFiles);
                    label10.Text = $"{_totalBytesDownloaded.ToStringFileSize()} / {_totalBytesAllFiles.ToStringFileSize()}";
                    Label3.Text = ((long)speedBps).ToStringFileSize(true);
                    Label4.Text = TimeSpan.FromSeconds(Math.Max(0, remainingSeconds)).ToString(@"mm\:ss");
                });
            }

            Invoke(() =>
            {
                state.Item.SubItems[2].Text = e.BytesReceived.ToStringFileSize();
                progressBar1.Value = e.TotalBytesToReceive > 0 ? (int)(e.BytesReceived * 100 / e.TotalBytesToReceive) : 0;
                Label2.Text = $"{e.BytesReceived.ToStringFileSize()} / {state.TotalBytes.ToStringFileSize()}";
            });
        }

        private void AdjustParallelism(double currentSpeed)
        {
            if (_smoothedSpeedBps == 0)
            {
                _smoothedSpeedBps = currentSpeed;
                return;
            }

            double ratio = currentSpeed / _smoothedSpeedBps;

            if (ratio > 1.15 && _activeParallel < MaxParallel)
                _activeParallel++;
            else if (ratio < 0.75 && _activeParallel > MinParallel)
                _activeParallel--;

            _smoothedSpeedBps = (_smoothedSpeedBps * 0.7) + (currentSpeed * 0.3);
        }

        #endregion

        #region Completion / Cleanup

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (sender is not DownloadManager dm) return;

            Invoke(() =>
            {
                if (_activeDownloads.TryGetValue(dm, out var state))
                {
                    listView1.Items.Remove(state.Item);
                    _activeDownloads.Remove(dm);
                }

                _completedFiles++;
            });

            dm.Dispose();
        }

        private void DownloadManager_Dlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            _hasFinished = true;

            lock (_sync)
            {
                foreach (var dm in _activeDownloads.Keys.ToArray())
                {
                    try { dm.StopDownload(true); } catch { }
                }
            }
        }

        #endregion

        private void Button3_Click(object sender, EventArgs e)
        {
            _hasFinished = true;

            lock (_sync)
            {
                foreach (var dm in _activeDownloads.Keys.ToArray())
                {
                    try { dm.StopDownload(true); } catch { }
                }

                _activeDownloads.Clear();
            }

            DialogResult = DialogResult.Abort;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void DownloadManager_Dlg_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();
        }
    }
}
