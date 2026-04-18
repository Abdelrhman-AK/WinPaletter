using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    public partial class Win32UI_Gallery : UI.WP.Form
    {
        private static readonly Dictionary<string, Bitmap> _previewCache = [];
        private static readonly object _cacheLock = new();

        private static RetroDesktopColors _renderer = null;
        private static readonly SemaphoreSlim _rendererSemaphore = new(1, 1);

        // Static — survives form re-creation, matches the lifetime of the static cache/renderer.
        // Without this, a new instance always sees Task.CompletedTask and never drains the
        // prior instance's still-running background task before touching shared state.
        private static CancellationTokenSource _loadCts = null;
        private static Task _priorLoadTask = Task.CompletedTask;

        // Tracks whether DisposeSubControls has already run for this instance, so that
        // LoadGalleryAsync can always safely call it before Clear() even on mid-load reopen.
        private bool _subControlsDisposed = false;

        public Win32UI_Gallery()
        {
            InitializeComponent();
        }

        private void Win32UI_Gallery_Load(object sender, EventArgs e)
        {
            Task previousTask = _priorLoadTask;
            _priorLoadTask = LoadGalleryAsync(previousTask);
        }

        private async Task LoadGalleryAsync(Task previousTask)
        {
            // Cancel → drain → reset, in strict order.
            // The prior task must be fully dead before the semaphore is reset or shared renderer state is touched.
            _loadCts?.Cancel();

            try { await previousTask.ConfigureAwait(false); }
            catch (OperationCanceledException) { }
            catch (Exception) { }

            _loadCts?.Dispose();
            ResetSemaphore();

            _loadCts = new CancellationTokenSource();
            CancellationToken token = _loadCts.Token;

            Cursor = Cursors.WaitCursor;

            try
            {
                token.ThrowIfCancellationRequested();

                // Always dispose any lingering subcontrols before clearing — even if the prior session was closed mid-load and PickATheme never ran DisposeSubControls.
                // Controls.Clear() without Dispose leaks GDI handles and causes the empty container on the third open.
                EnsureSubControlsDisposed();
                schemes.Controls.Clear();

                string[] schemeNames = [.. Schemes.ClassicColors.Split('\n').Select(f => f.Split('|')[0])];
                string selectedItem = Forms.Win32UI.ComboBox1?.SelectedItem?.ToString() ?? string.Empty;

                schemes.SuspendLayout();

                foreach (string schemeName in schemeNames)
                {
                    token.ThrowIfCancellationRequested();

                    RadioImage radioImage = new()
                    {
                        TextImageRelation = TextImageRelation.ImageAboveText,
                        Image = null,
                        Size = new Size(250, 180),
                        Text = schemeName,
                        Checked = !string.IsNullOrEmpty(selectedItem) && string.Equals(selectedItem, schemeName, StringComparison.OrdinalIgnoreCase)
                    };

                    schemes.Controls.Add(radioImage);
                }

                // Mark controls as live so EnsureSubControlsDisposed knows they need cleanup
                _subControlsDisposed = false;

                schemes.ResumeLayout(true);

                List<string> uncached = [];
                List<(string Name, Bitmap Bmp)> cached = [];

                lock (_cacheLock)
                {
                    foreach (string name in schemeNames)
                    {
                        if (_previewCache.TryGetValue(name, out Bitmap bmp) && IsValidBitmap(bmp)) cached.Add((name, bmp));
                        else
                        {
                            _previewCache.Remove(name);
                            uncached.Add(name);
                        }
                    }
                }

                if (cached.Count > 0)
                {
                    schemes.SuspendLayout();

                    foreach ((string name, Bitmap bmp) in cached)
                    {
                        token.ThrowIfCancellationRequested();
                        ApplyPreview(name, bmp);
                    }

                    schemes.ResumeLayout(true);
                }

                if (uncached.Count > 0)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Run(() => RenderUncachedAsync([.. uncached], token), token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                // Normal — form closed or reload triggered
            }
            finally
            {
                ResetCursor();
            }
        }

        // Disposes subcontrols exactly once per control population.
        // Safe to call multiple times — idempotent via _subControlsDisposed flag.
        private void EnsureSubControlsDisposed()
        {
            if (_subControlsDisposed) return;

            foreach (RadioImage control in schemes.Controls.OfType<RadioImage>().ToList())
            {
                control.Image = null;
                control.Dispose();
            }

            _subControlsDisposed = true;
        }

        private static bool IsValidBitmap(Bitmap bmp)
        {
            if (bmp == null) return false;

            try { _ = bmp.Width; return true; }
            catch (ArgumentException) { return false; }
        }

        private void ResetCursor()
        {
            if (!IsDisposed && IsHandleCreated)
            {
                try { BeginInvoke(() => { if (!IsDisposed) Cursor = Cursors.Default; }); }
                catch (ObjectDisposedException) { Cursor.Current = Cursors.Default; }
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private static void ResetSemaphore()
        {
            if (_rendererSemaphore.CurrentCount == 0)
            {
                try { _rendererSemaphore.Release(); }
                catch (SemaphoreFullException) { }
            }
        }

        private async Task RenderUncachedAsync(string[] schemeNames, CancellationToken token)
        {
            await _rendererSemaphore.WaitAsync(token).ConfigureAwait(false);

            try
            {
                if (_renderer == null || _renderer.IsDisposed)
                {
                    _renderer?.Dispose();
                    _renderer = new RetroDesktopColors { Size = new Size(350, 300) };
                }

                _renderer.LoadMetrics(Program.TM);
            }
            finally
            {
                _rendererSemaphore.Release();
            }

            foreach (string schemeName in schemeNames)
            {
                token.ThrowIfCancellationRequested();

                Bitmap preview = await GenerateAndCacheAsync(schemeName, token).ConfigureAwait(false);

                if (preview == null) continue;

                token.ThrowIfCancellationRequested();

                string name = schemeName;
                Bitmap bmp = preview;

                if (IsDisposed) break;

                try
                {
                    Invoke(() => ApplyPreview(name, bmp));
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }
        }

        private static async Task<Bitmap> GenerateAndCacheAsync(string schemeName, CancellationToken token)
        {
            lock (_cacheLock)
            {
                if (_previewCache.TryGetValue(schemeName, out Bitmap cached))
                {
                    if (IsValidBitmap(cached)) return cached;
                    _previewCache.Remove(schemeName);
                }
            }

            await _rendererSemaphore.WaitAsync(token).ConfigureAwait(false);

            try
            {
                lock (_cacheLock)
                {
                    if (_previewCache.TryGetValue(schemeName, out Bitmap existing) && IsValidBitmap(existing)) return existing;
                }

                _renderer.LoadFromWinThemeString(Schemes.ClassicColors, schemeName);

                using Bitmap full = _renderer.ToBitmap(true);
                Bitmap resized = full?.Resize(160, 135);

                if (resized != null)
                {
                    lock (_cacheLock)
                    {
                        _previewCache[schemeName] = resized;
                    }
                }

                return resized;
            }
            finally
            {
                _rendererSemaphore.Release();
            }
        }

        private void ApplyPreview(string schemeName, Bitmap preview)
        {
            if (IsDisposed) return;

            RadioImage target = schemes.Controls.OfType<RadioImage>().FirstOrDefault(r => string.Equals(r.Text, schemeName, StringComparison.OrdinalIgnoreCase));

            if (target == null || target.IsDisposed) return;

            target.Image = preview;
            target.Invalidate();
        }

        public string PickATheme(Size parentButtonSize, Point parentButtonLocation)
        {
            DialogResult result = ShowDialog(parentButtonSize, parentButtonLocation);

            RadioImage selected = schemes.Controls.OfType<RadioImage>().FirstOrDefault(r => r.Checked);
            string selectedName = selected?.Text ?? string.Empty;

            // Dispose subcontrols while form is still hidden — idempotent, safe to call here
            EnsureSubControlsDisposed();

            return result == DialogResult.OK ? selectedName : string.Empty;
        }

        public DialogResult ShowDialog(Size parentButtonSize, Point parentButtonLocation)
        {
            Location = parentButtonLocation + new Size(parentButtonSize.Width - Width, parentButtonSize.Height);
            return ShowDialog();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_load_into_theme_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            _loadCts?.Cancel();

            Task snapshot = _priorLoadTask;

            Task.Run(async () =>
            {
                try { await snapshot.ConfigureAwait(false); }
                catch (OperationCanceledException) { }
                catch (Exception) { }
                finally
                {
                    _loadCts?.Dispose();
                    _loadCts = null;
                    ResetSemaphore();
                }
            });
        }

        public static void DisposeSharedResources()
        {
            _loadCts?.Cancel();
            _loadCts?.Dispose();
            _loadCts = null;

            _renderer?.Dispose();
            _renderer = null;

            lock (_cacheLock)
            {
                foreach (Bitmap bmp in _previewCache.Values) bmp?.Dispose();
                _previewCache.Clear();
            }
        }
    }
}