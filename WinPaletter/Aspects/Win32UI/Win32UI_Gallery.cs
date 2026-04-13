using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    public partial class Win32UI_Gallery : UI.WP.Form
    {
        // Single static instance of RetroDesktopColors reused for all preview generation
        private static RetroDesktopColors _previewRenderer = null;
        private static readonly object _renderLock = new();

        public Win32UI_Gallery()
        {
            InitializeComponent();
        }

        private void Win32UI_Gallery_Load(object sender, EventArgs e)
        {
            LoadGallery();
        }

        private void LoadGallery()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                // Clear existing controls
                foreach (Control control in schemes.Controls.Cast<Control>().ToList())
                {
                    if (control is RadioImage ri) ri.Image = null;
                    control.Dispose();
                }
                schemes.Controls.Clear();

                string[] schemeNames = [.. Schemes.ClassicColors.Split('\n').Select(f => f.Split('|')[0])];

                string selectedItem = Forms.Win32UI.ComboBox1?.SelectedItem?.ToString() ?? string.Empty;

                // Initialize or reset the preview renderer
                InitializePreviewRenderer();

                List<Control> controlList = [with(schemeNames.Length)];

                foreach (string schemeName in schemeNames)
                {
                    // Generate preview bitmap for this scheme
                    Bitmap preview = GeneratePreview(schemeName);

                    RadioImage radioImage = new()
                    {
                        TextImageRelation = TextImageRelation.ImageAboveText,
                        Image = preview,
                        Size = new Size(250, 180),
                        Text = schemeName,
                        Checked = !string.IsNullOrEmpty(selectedItem) && string.Equals(selectedItem, schemeName, StringComparison.OrdinalIgnoreCase)
                    };

                    controlList.Add(radioImage);
                }

                schemes.Controls.AddRange([.. controlList]);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void InitializePreviewRenderer()
        {
            lock (_renderLock)
            {
                if (_previewRenderer != null && !_previewRenderer.IsDisposed)
                {
                    // Reuse existing renderer
                    return;
                }

                _previewRenderer?.Dispose();
                _previewRenderer = new();
                _previewRenderer.LoadMetrics(Program.TM);
                _previewRenderer.Size = new Size(350, 300);
                _previewRenderer.Visible = false;
            }
        }

        private Bitmap GeneratePreview(string schemeName)
        {
            lock (_renderLock)
            {
                if (_previewRenderer == null || _previewRenderer.IsDisposed)
                {
                    InitializePreviewRenderer();
                }

                // Load theme colors into the renderer
                _previewRenderer.LoadFromWinThemeString(Schemes.ClassicColors, schemeName);

                // Generate bitmap
                using (Bitmap full = _previewRenderer.ToBitmap(true))
                {
                    return full.Resize(160, 135);
                }
            }
        }

        // Call this when the application is closing to clean up resources
        public static void CleanupPreviewRenderer()
        {
            lock (_renderLock)
            {
                _previewRenderer?.Dispose();
                _previewRenderer = null;
            }
        }

        public string PickATheme(Size parentButtonSize, Point parentButtonLocation)
        {
            if (ShowDialog(parentButtonSize, parentButtonLocation) == DialogResult.OK)
            {
                string result = schemes.Controls.Cast<Control>().FirstOrDefault(f => f is RadioImage ri && ri.Checked)?.Text;

                // Dispose controls but NOT their images (images will be disposed when the form closes)
                foreach (RadioImage control in schemes.Controls.OfType<RadioImage>())
                {
                    control.Image = null;
                    control.Dispose();
                }

                return result;
            }

            return string.Empty;
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
            // Clean up images when form closes
            foreach (RadioImage control in schemes.Controls.OfType<RadioImage>())
            {
                control.Image?.Dispose();
                control.Image = null;
            }

            base.OnFormClosed(e);
        }
    }
}