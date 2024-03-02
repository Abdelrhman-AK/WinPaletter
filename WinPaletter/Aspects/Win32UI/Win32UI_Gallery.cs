using System;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Templates;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    public partial class Win32UI_Gallery : Form
    {
        public Win32UI_Gallery()
        {
            InitializeComponent();
        }

        private void Win32UI_Gallery_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Forms.Win32UI.Icon;

            LoadGallery();
        }

        private void LoadGallery()
        {
            Cursor = Cursors.WaitCursor;

            Control[] controlCollection = new Control[] { };

            using (RetroDesktopColors RDC = new())
            {
                RDC.LoadMetrics(Program.TM);
                RDC.Size = new(350, 300);
                foreach (string item in Properties.Resources.RetroThemesDB.CList().Select(f => f.Split('|')[0]).ToArray())
                {
                    RDC.LoadFromWinThemeString(Properties.Resources.RetroThemesDB, item);

                    RadioImage radioImage = new()
                    {
                        TextImageRelation = TextImageRelation.ImageAboveText,
                        Image = RDC.ToBitmap(true).Resize(160, 135),
                        Size = new(250, 180),
                        Text = item
                    };

                    if (Forms.Win32UI.ComboBox1?.SelectedItem != null)
                    {
                        radioImage.Checked = Forms.Win32UI.ComboBox1.SelectedItem?.ToString().ToLower() == item?.ToLower();
                    }

                    controlCollection = (controlCollection ?? Enumerable.Empty<Control>()).Concat(new[] { radioImage }).ToArray();
                }

                schemes.Controls.AddRange(controlCollection);
            }

            Cursor = Cursors.Default;
        }

        public string PickATheme()
        {
            if (ShowDialog() == DialogResult.OK)
            {
                string result = schemes.Controls.Cast<Control>().Where(f => f is RadioImage && ((RadioImage)f).Checked).FirstOrDefault()?.Text;
                schemes.Controls.Cast<Control>().Where(f => f is RadioImage).ToList().ForEach(f => f.Dispose());
                return result;
            }
            else
            {
                return string.Empty;
            }
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
