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
        // Keyed by scheme name. Persists for the process lifetime.
        // Disposed only when the application exits or cache is explicitly invalidated.
        private static readonly Dictionary<string, Bitmap> _bitmapCache = new(StringComparer.OrdinalIgnoreCase);

        // Tracks the scheme string that was used to populate the cache.
        // If Schemes.ClassicColors ever changes at runtime, the cache is rebuilt.
        private static string _cachedSchemeSource = null;

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

            // Clear existing controls without destroying cached bitmaps.
            foreach (Control control in schemes.Controls.Cast<Control>().ToList())
            {
                if (control is RadioImage ri) ri.Image = null;
                control.Dispose();
            }
            schemes.Controls.Clear();

            string[] schemeNames = [.. Schemes.ClassicColors.Split('\n').Select(f => f.Split('|')[0])];

            // Invalidate cache if the source data changed since last load.
            if (!string.Equals(_cachedSchemeSource, Schemes.ClassicColors, StringComparison.Ordinal))
            {
                InvalidateBitmapCache();
                _cachedSchemeSource = Schemes.ClassicColors;
            }

            string selectedItem = Forms.Win32UI.ComboBox1?.SelectedItem?.ToString() ?? string.Empty;

            List<Control> controlList = new(schemeNames.Length);

            // Only open RetroDesktopColors if there are cache misses.
            string[] misses = [.. schemeNames.Where(name => !_bitmapCache.ContainsKey(name))];

            if (misses.Length > 0)
            {
                using (RetroDesktopColors rdc = new())
                {
                    rdc.LoadMetrics(Program.TM);
                    rdc.Size = new Size(350, 300);

                    foreach (string schemeName in misses)
                    {
                        rdc.LoadFromWinThemeString(Schemes.ClassicColors, schemeName);

                        Bitmap resized;
                        using (Bitmap full = rdc.ToBitmap(true))
                        {
                            resized = full.Resize(160, 135);
                        }

                        _bitmapCache[schemeName] = resized;
                    }
                }
            }

            foreach (string schemeName in schemeNames)
            {
                RadioImage radioImage = new()
                {
                    TextImageRelation = TextImageRelation.ImageAboveText,
                    Image = _bitmapCache[schemeName],
                    Size = new Size(250, 180),
                    Text = schemeName,
                    Checked = !string.IsNullOrEmpty(selectedItem) && string.Equals(selectedItem, schemeName, StringComparison.OrdinalIgnoreCase)
                };

                controlList.Add(radioImage);
            }

            schemes.Controls.AddRange(controlList.ToArray());
            Cursor = Cursors.Default;
        }

        // Call this if scheme data changes at runtime and you need fresh renders.
        public static void InvalidateBitmapCache()
        {
            foreach (Bitmap bmp in _bitmapCache.Values)
            {
                bmp.Dispose();
            }
            _bitmapCache.Clear();
            _cachedSchemeSource = null;
        }

        public string PickATheme(Size parentButtonSize, Point parentButtonLocation)
        {
            if (ShowDialog(parentButtonSize, parentButtonLocation) == DialogResult.OK)
            {
                string result = schemes.Controls.Cast<Control>().FirstOrDefault(f => f is RadioImage ri && ri.Checked)?.Text;

                // Dispose controls but NOT their images — images are owned by the cache.
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
    }
}