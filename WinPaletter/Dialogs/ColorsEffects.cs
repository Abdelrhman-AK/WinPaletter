using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.TypesExtensions;
using WinPaletter.UI.Controllers;

namespace WinPaletter.Dialogs
{
    public partial class ColorsEffects : WinPaletter.BorderlessForm
    {
        public ColorsEffects()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Represents a collection of color items and their default colors.
        /// </summary>
        /// <remarks>Each item in the collection is a tuple containing a <see
        /// cref="UI.Controllers.ColorItem"/>  and its corresponding <see cref="System.Drawing.Color"/>. This collection
        /// is used to manage  and associate color-related data.</remarks>
        private List<(UI.Controllers.ColorItem, Color)> colorItems = [];
        private List<Color> imageColors = [];
        private System.Windows.Forms.Form form;

        private void ColorsEffects_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();
        }

        public void Show(UI.WP.Button button)
        {
            Location = new Point(button.PointToScreen(Point.Empty).X + (button.Width - Width) / 2, button.PointToScreen(Point.Empty).Y + button.Height + 5);
            form = button.FindForm();

            colorItems.Clear();
            colorItems.AddRange(form.GetAllControls().OfType<UI.Controllers.ColorItem>().Select(ci => (ci, ci.BackColor)));

            foreach (UI.WP.Toggle t in this.GetAllControls().OfType<UI.WP.Toggle>()) t.Checked = false;

            ImagePath.Text = GetReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty).ToString();

            ShowDialog();
        }


        void ApplyPreview(System.Windows.Forms.Form f)
        {
            if (f == Forms.Win12Colors) Forms.Win12Colors.UpdatePreview();
            else if (f == Forms.Win11Colors) Forms.Win11Colors.UpdatePreview();
            else if (f == Forms.Win10Colors) Forms.Win10Colors.UpdatePreview();
            else if (f == Forms.Win81Colors) Forms.Win81Colors.UpdatePreview();
            else if (f == Forms.Win8Colors) Forms.Win8Colors.UpdatePreview();
            else if (f == Forms.Win7Colors) Forms.Win7Colors.UpdatePreview();
            else if (f == Forms.WinVistaColors) Forms.WinVistaColors.UpdatePreview();
            else if (f == Forms.Win32UI) Forms.Win32UI.ApplyRetroPreview();
            else if (f == Forms.CMD) Forms.CMD.ApplyPreview();
            else if (f == Forms.WindowsTerminal) Forms.WindowsTerminal.ApplyPreview(Forms.WindowsTerminal._Terminal);
            else if (f == Forms.LogonUI81) Forms.LogonUI81.ApplyPreview();
            else if (f == Forms.LogonUI7) Forms.LogonUI7.ApplyPreview();
            else if (f == Forms.LogonUIXP) Forms.LogonUIXP.UpdateWin2000Preview(Forms.LogonUIXP.color_pick.BackColor);
            else if (f == Forms.CursorsStudio) Forms.CursorsStudio.ApplyColorsToPreview();
            else if (f == Forms.Wallpaper_Editor) Forms.Wallpaper_Editor.pnl_preview.BackColor = Forms.Wallpaper_Editor.color_pick.BackColor;
            else if (f == Forms.ApplicationThemer) Forms.ApplicationThemer.AdjustPreview();
            else if (f == Forms.EditInfo)
            {
                Forms.EditInfo.StoreItem1.TM.Info.Color1 = Forms.EditInfo.color1.BackColor;
                Forms.EditInfo.StoreItem1.TM.Info.Color2 = Forms.EditInfo.color2.BackColor;
                Forms.EditInfo.StoreItem1.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;

            foreach ((ColorItem, Color) colorItem in colorItems)
            {
                if (colorItem.Item1.BackColor != colorItem.Item2) colorItem.Item1.BackColor = colorItem.Item2;
            }

            ApplyPreview(form);

            Cursor = Cursors.Default;

            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();

            ApplyPreview(form);
        }

        private void toggle_effects_CheckedChanged(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            if ((sender as UI.WP.Toggle).Checked)
            {
                foreach (UI.WP.Toggle t in this.GetAllControls().OfType<UI.WP.Toggle>().Where(x => x != sender)) t.Checked = false;
                foreach ((ColorItem, Color) item in colorItems)
                {
                    Color resultColor;

                    if (sender == toggle_image && imageColors.Count > 0) resultColor = item.Item2.GetNearestColorFromPalette(imageColors);
                    else if (sender == toggle_brightness) resultColor = item.Item2.CB((trackBarX7.Value - 50) / 50f);
                    else if (sender == toggle_darken) resultColor = item.Item2.Dark(trackBarX1.Value / 100f);
                    else if (sender == toggle_lighten) resultColor = item.Item2.Light(trackBarX2.Value / 100f);
                    else if (sender == toggle_desaturate) resultColor = item.Item2.Desaturate(trackBarX4.Value / 100f);
                    else if (sender == toggle_rotateHue) resultColor = item.Item2.RotateHue(trackBarX5.Value);
                    else if (sender == toggle_invert) resultColor = item.Item2.Invert();
                    else if (sender == toggle_reverse) resultColor = item.Item2.Reverse();
                    else if (sender == toggle_sepia) resultColor = item.Item2.Sepia();
                    else if (sender == toggle_webSafe) resultColor = item.Item2.ToWebSafe();
                    else if (sender == toggle_256) resultColor = item.Item2.To256Color();
                    else if (sender == toggle_frutigerAero) resultColor = item.Item2.ToFrutigerAero();
                    else if (sender == toggle_metro) resultColor = item.Item2.ToMetro();
                    else if (sender == toggle_monochrome) resultColor = item.Item2.Monochrome();
                    else if (sender == toggle_grayscale) resultColor = item.Item2.Grayscale();
                    else if (sender == toggle_androidMaterial) resultColor = item.Item2.ToMaterial();
                    else if (sender == toggle_androidMaterialExpressive) resultColor = item.Item2.ToMaterialExpressive3();
                    else if (sender == toggle_Mac) resultColor = item.Item2.ToMacSemantic();
                    else resultColor = item.Item2;

                    item.Item1.BackColor = resultColor;
                }

                ApplyPreview(form);
            }
            else if (!this.GetAllControls().OfType<UI.WP.Toggle>().Any(x => x.Checked))
            {
                foreach ((ColorItem, Color) colorItem in colorItems) if (colorItem.Item1.BackColor != colorItem.Item2) colorItem.Item1.BackColor = colorItem.Item2;

                ApplyPreview(form);
            }

            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void trackBarX7_ValueChanged(object sender, EventArgs e)
        {
            if (toggle_brightness.Checked)
            {
                foreach ((ColorItem, Color) item in colorItems) item.Item1.BackColor = item.Item2.CB((trackBarX7.Value - 50) / 50f);

                ApplyPreview(form);
            }
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            if (toggle_darken.Checked)
            {
                foreach ((ColorItem, Color) item in colorItems) item.Item1.BackColor = item.Item2.Dark(trackBarX1.Value / 100f);
                ApplyPreview(form);
            }
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            if (toggle_lighten.Checked)
            {
                foreach ((ColorItem, Color) item in colorItems) item.Item1.BackColor = item.Item2.Light(trackBarX2.Value / 100f);
                ApplyPreview(form);
            }
        }

        private void trackBarX4_ValueChanged(object sender, EventArgs e)
        {
            if (toggle_desaturate.Checked)
            {
                foreach ((ColorItem, Color) item in colorItems) item.Item1.BackColor = item.Item2.Desaturate(trackBarX4.Value / 100f);
                ApplyPreview(form);
            }
        }

        private void trackBarX5_ValueChanged(object sender, EventArgs e)
        {
            if (toggle_rotateHue.Checked)
            {
                foreach ((ColorItem, Color) item in colorItems) item.Item1.BackColor = item.Item2.RotateHue(trackBarX5.Value);
                ApplyPreview(form);
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { FileName = ImagePath.Text, Filter = Program.Filters.Images, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ImagePath.Text = dlg.FileName;
                }
            }
        }

        private void ImagePath_TextChanged(object sender, EventArgs e)
        {
            Task.Run(() => 
            {
                if (System.IO.File.Exists(ImagePath.Text))
                {
                    using (Bitmap bmp = BitmapMgr.Load(ImagePath.Text))
                    {
                        Cursor = System.Windows.Forms.Cursors.WaitCursor;
                        imageColors = bmp.ToPalette(100);
                        Cursor = System.Windows.Forms.Cursors.Default;
                    }
                }
                else
                {
                    imageColors.Clear();
                }

                Invoke(() => 
                {
                    if (toggle_image.Checked && imageColors.Count > 0)
                    {
                        foreach ((ColorItem, Color) item in colorItems)
                        {
                            Color c = item.Item2.GetNearestColorFromPalette(imageColors);
                            if (c != item.Item1.BackColor) item.Item1.BackColor = c;
                        }
                        ApplyPreview(form);
                    }
                });
            });
        }
    }
}