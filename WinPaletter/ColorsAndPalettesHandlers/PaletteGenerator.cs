using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme.Structures;
using WinPaletter.TypesExtensions;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    /// <summary>
    /// This class is responsible for generating a color palette from an image.
    /// </summary>
    public partial class PaletteGenerator
    {
        /// <summary>
        /// List of colors generated from the image.
        /// </summary>
        private readonly List<Color> Colors_List = [];

        /// <summary>
        /// Represents a collection of color items and their default colors.
        /// </summary>
        /// <remarks>Each item in the collection is a tuple containing a <see
        /// cref="UI.Controllers.ColorItem"/>  and its corresponding <see cref="System.Drawing.Color"/>. This collection
        /// is used to manage  and associate color-related data.</remarks>
        private List<(ColorItem, Color)> colorItems = [];
        private Form form;
        bool isCreatingMiniColorItemInColors = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaletteGenerateFromImage"/> class.
        /// </summary>
        public PaletteGenerator()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
        }

        public void Show(UI.WP.Button button)
        {
            Location = new Point(button.PointToScreen(Point.Empty).X + (button.Width - Width) / 2, button.PointToScreen(Point.Empty).Y + button.Height + 5);
            form = button.FindForm();

            colorItems.Clear();
            colorItems.AddRange(form.GetAllControls().OfType<ColorItem>().Select(ci => (ci, ci.BackColor)));

            foreach (Toggle t in this.GetAllControls().OfType<Toggle>()) t.Checked = false;

            TextBox1.Text = Program.TM.Wallpaper.ImageFile;

            ShowDialog();
        }

        void ApplyPreview(Form f)
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
            else if (f == Forms.CursorsStudio) Forms.CursorsStudio.ApplyColorsToPreview(true);
            else if (f == Forms.Wallpaper_Editor) Forms.Wallpaper_Editor.pnl_preview.BackColor = Forms.Wallpaper_Editor.color_pick.BackColor;
            else if (f == Forms.ApplicationThemer) Forms.ApplicationThemer.AdjustPreview();
            else if (f == Forms.EditInfo)
            {
                Forms.EditInfo.StoreItem1.TM.Info.Color1 = Forms.EditInfo.color1.BackColor;
                Forms.EditInfo.StoreItem1.TM.Info.Color2 = Forms.EditInfo.color2.BackColor;
                Forms.EditInfo.StoreItem1.Invalidate();
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tablessControl1.Enabled = !radioImage2.Checked;
            GroupBox1.Enabled = true;
            groupBox4.Enabled = true;

            if ((sender as RadioImage).Checked)
            {
                tablessControl1.SelectedIndex = 0;

                List<Color> colors = [.. flowLayoutPanel1.Controls.OfType<ColorItem>().Select(ci => ci.BackColor)];
                GetColors(colors);
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tablessControl1.Enabled = !radioImage2.Checked;
            GroupBox1.Enabled = true;
            groupBox4.Enabled = true;

            if ((sender as RadioImage).Checked)
            {
                tablessControl1.SelectedIndex = 1;

                using (Bitmap src = BitmapMgr.Load(TextBox1.Text))
                {
                    GetColors(src);
                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            // If the radio button is checked, get the colors from the image.
            if (RadioButton2.Checked) GetColors(BitmapMgr.Load(TextBox1.Text));
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // Open the file dialog to select an image.
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK) TextBox1.Text = dlg.FileName;
            }
        }

        /// <summary>
        /// Extracts a palette of distinct colors from the specified bitmap and displays them as color items in the UI.
        /// </summary>
        /// <remarks>
        /// This method generates a palette of distinct colors from a resized version of the provided bitmap
        /// to improve performance. The colors are sorted by their RGB values and displayed as individual
        /// color items in the user interface.
        /// </remarks>
        /// <param name="source">
        /// The source <see cref="Bitmap"/> from which to extract colors. 
        /// If <paramref name="source"/> is <see langword="null"/>, no processing occurs.
        /// </param>
        public void GetColors(Bitmap source)
        {
            if (source is null) return;

            Bitmap thumbnail = source.Resize(Program.PreviewSize);

            Task.Run(() =>
            {
                try
                {
                    Invoke(() =>
                    {
                        Cursor = System.Windows.Forms.Cursors.AppStarting;
                        ImgPaletteContainer.Visible = false;

                        // Dispose existing color items
                        foreach (ColorItem item in ImgPaletteContainer.Controls.OfType<ColorItem>()) item.Dispose();

                        ImgPaletteContainer.Controls.Clear();
                    });

                    // Generate and sort color palette
                    Colors_List.Clear();
                    List<Color> extractedColors = [.. thumbnail.ToPalette(100, 10, false).OrderBy(c => c, new RGBColorComparer())];

                    Colors_List.AddRange(extractedColors);

                    // Prepare UI controls (off-UI thread)
                    ColorItem[] colorItems = [.. extractedColors.Select(c => new ColorItem
                    {
                        Size = ColorItem.GetMiniColorItemSize(),
                        AllowDrop = false,
                        PauseColorsHistory = true,
                        BackColor = Color.FromArgb(255, c),
                        DefaultBackColor = Color.FromArgb(255, c)
                    })];

                    // Apply results back to the UI
                    Invoke(() =>
                    {
                        ImgPaletteContainer.Controls.AddRange(colorItems);
                        ImgPaletteContainer.Visible = true;
                        Cursor = System.Windows.Forms.Cursors.Default;
                    });
                }
                catch (Exception ex)
                {
                    Invoke(() =>
                    {
                        Cursor = System.Windows.Forms.Cursors.Default;
                        ImgPaletteContainer.Visible = true;
                    });

                    if (Program.Settings.AppLog.Enabled) Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error in generating palette from image. {ex.Message}");
                }
                finally
                {
                    thumbnail?.Dispose();
                }
            });
        }

        public void GetColors(List<Color> colors)
        {
            Task.Run(() =>
            {
                Invoke(() =>
                {
                    Cursor = System.Windows.Forms.Cursors.AppStarting;
                    ImgPaletteContainer.Visible = false;

                    // Clear the list of colors and the color items.
                    foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>()) ctrl?.Dispose();
                    ImgPaletteContainer.Controls.Clear();
                });

                // Clear the list of colors.
                Colors_List.Clear();

                foreach (Color c in colors)
                {
                    for (float f = -1; f <= 1f; f += 0.05f)
                    {
                        Color result = Color.FromArgb(255, c.CB(f));
                        if (!Colors_List.Contains(result)) Colors_List.Add(result);

                        result = Color.FromArgb(255, c.Dark(f));
                        if (!Colors_List.Contains(result)) Colors_List.Add(result);

                        result = Color.FromArgb(255, c.Light(f));
                        if (!Colors_List.Contains(result)) Colors_List.Add(result);
                    }

                    Color result_1 = Color.FromArgb(255, c.DarkDark());
                    if (!Colors_List.Contains(result_1)) Colors_List.Add(result_1);

                    result_1 = Color.FromArgb(255, c.LightLight());
                    if (!Colors_List.Contains(result_1)) Colors_List.Add(result_1);
                }

                // Sort the list of colors.
                Colors_List.Sort(new RGBColorComparer());

                List<ColorItem> collection = [];

                // Add the colors to the color items as colors controls.
                foreach (Color c in Colors_List)
                {
                    Color processedColor = ApplyEffect(c);

                    ColorItem MiniColorItem = new()
                    {
                        Size = ColorItem.GetMiniColorItemSize(),
                        AllowDrop = false,
                        PauseColorsHistory = true,
                        BackColor = processedColor,
                        DefaultBackColor = processedColor
                    };

                    collection.Add(MiniColorItem);
                }

                Invoke(() =>
                {
                    Cursor = System.Windows.Forms.Cursors.Default;
                    ImgPaletteContainer.Controls.AddRange([.. collection]);
                    ImgPaletteContainer.Visible = true;
                });
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.AppStarting;

            if (Colors_List != null && Colors_List.Count > 0)
            {
                List<Color> processedColors = ImgPaletteContainer.Controls.OfType<ColorItem>().Select(ci => ci.BackColor).ToList();

                foreach ((ColorItem, Color) item in colorItems)
                {
                    item.Item1.BackColor = item.Item2.GetNearestColorFromPalette(processedColors);
                }
            }

            ApplyPreview(form);

            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.AppStarting;

            foreach ((ColorItem, Color) colorItem in colorItems)
            {
                if (colorItem.Item1.BackColor != colorItem.Item2) colorItem.Item1.BackColor = colorItem.Item2;
            }

            ApplyPreview(form);

            Cursor = System.Windows.Forms.Cursors.Default;

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();

            ApplyPreview(form);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            isCreatingMiniColorItemInColors = false;

            ColorItem colorItem = new()
            {
                Size = ColorItem.GetMiniColorItemSize(),
                AllowDrop = false,
                PauseColorsHistory = true,
            };

            Color InitColor = colorItem.BackColor;

            flowLayoutPanel1.Controls.Add(colorItem);

            Dictionary<Control, string[]> Dict = new()
            {
                { colorItem, [nameof(colorItem.BackColor)] }
            };

            Color c = Forms.ColorPickerDlg.Pick(Dict, true);

            //isCreatingMiniColorItemInColors = false;

            if (c == InitColor) colorItem?.Dispose();
            else
            {
                colorItem.BackColor = c;
                colorItem.DefaultBackColor = c;

                colorItem.Click += (s, ev) =>
                {
                    flowLayoutPanel1.Controls.Remove(s as ColorItem);
                    (s as ColorItem).Click -= null;
                    (s as ColorItem)?.Dispose();

                    List<Color> colors = [.. flowLayoutPanel1.Controls.OfType<ColorItem>().Select(ci => ci.BackColor)];
                    GetColors(colors);
                };

                List<Color> colors = [.. flowLayoutPanel1.Controls.OfType<ColorItem>().Select(ci => ci.BackColor)];
                GetColors(colors);
            }
        }

        private void radioImage2_CheckedChanged(object sender, EventArgs e)
        {
            tablessControl1.Enabled = !radioImage2.Checked;
            GroupBox1.Enabled = true;
            groupBox4.Enabled = true;

            if ((sender as UI.WP.RadioImage).Checked)
            {
                Task.Run(() =>
                {
                    Invoke(() =>
                    {
                        Cursor = System.Windows.Forms.Cursors.AppStarting;
                        ImgPaletteContainer.Visible = false;

                        // Clear the list of colors and the color items.
                        foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>()) ctrl?.Dispose();
                        ImgPaletteContainer.Controls.Clear();
                    });

                    // Clear the list of colors.
                    Colors_List.Clear();

                    foreach ((ColorItem, Color) item in colorItems)
                    {
                        Colors_List.Add(item.Item2);
                    }

                    // Sort the list of colors.
                    Colors_List.Sort(new RGBColorComparer());

                    List<ColorItem> collection = [];

                    // Add the colors to the color items as colors controls.
                    foreach (Color c in Colors_List)
                    {
                        Color processedColor = ApplyEffect(c);

                        ColorItem MiniColorItem = new()
                        {
                            Size = ColorItem.GetMiniColorItemSize(),
                            AllowDrop = false,
                            PauseColorsHistory = true,
                            BackColor = processedColor,
                            DefaultBackColor = processedColor
                        };

                        collection.Add(MiniColorItem);
                    }

                    Invoke(() =>
                    {
                        Cursor = System.Windows.Forms.Cursors.Default;
                        ImgPaletteContainer.Controls.AddRange([.. collection]);
                        ImgPaletteContainer.Visible = true;
                    });
                });

                ApplyPreview(form);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TextBox1.Text = Program.TM.Wallpaper.ImageFile;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "WallPaper", Program.TM.Wallpaper.ImageFile);
        }

        Color ApplyEffect(Color c)
        {
            if (!toggle1.Checked) return c;

            if (toggle_brightness.Checked) return c.CB((trackBarX7.Value - 50) / 50f);
            else if (toggle_darken.Checked) return c.Dark(trackBarX1.Value / 100f);
            else if (toggle_lighten.Checked) return c.Light(trackBarX2.Value / 100f);
            else if (toggle_desaturate.Checked) return c.Desaturate(trackBarX4.Value / 100f);
            else if (toggle_rotateHue.Checked) return c.RotateHue(trackBarX5.Value);
            else if (toggle_invert.Checked) return c.Invert();
            else if (toggle_reverse.Checked) return c.Reverse();
            else if (toggle_sepia.Checked) return c.Sepia();
            else if (toggle_webSafe.Checked) return c.ToWebSafe();
            else if (toggle_256.Checked) return c.To256Color();
            else if (toggle_frutigerAero.Checked) return c.ToFrutigerAero();
            else if (toggle_metro.Checked) return c.ToMetro();
            else if (toggle_monochrome.Checked) return c.Monochrome();
            else if (toggle_grayscale.Checked) return c.Grayscale();
            else if (toggle_androidMaterial.Checked) return c.ToMaterial();
            else if (toggle_androidMaterialExpressive.Checked) return c.ToMaterialExpressive3();
            else if (toggle_Mac.Checked) return c.ToMacSemantic();
            else return c;
        }

        void ApplyEffects(object sender)
        {
            Task.Run(() =>
            {
                Invoke(() =>
                {
                    Cursor = System.Windows.Forms.Cursors.AppStarting;
                    ImgPaletteContainer.Visible = false;

                    // Clear the list of colors and the color items.
                    foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>()) ctrl?.Dispose();
                    ImgPaletteContainer.Controls.Clear();
                });

                List<ColorItem> collection = [];

                // Add the colors to the color items as colors controls.
                foreach (Color c in Colors_List)
                {
                    Color processedColor = ApplyEffect(c);

                    ColorItem MiniColorItem = new()
                    {
                        Size = ColorItem.GetMiniColorItemSize(),
                        AllowDrop = false,
                        PauseColorsHistory = true,
                        BackColor = processedColor,
                        DefaultBackColor = processedColor
                    };

                    collection.Add(MiniColorItem);
                }

                Invoke(() =>
                {
                    Cursor = System.Windows.Forms.Cursors.Default;
                    ImgPaletteContainer.Controls.AddRange([.. collection]);
                    ImgPaletteContainer.Visible = true;
                });
            });
        }
        private void toggle_effects_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Toggle t in panel1.GetAllControls().OfType<Toggle>().Where(x => x != sender)) t.Checked = false;

            if ((sender as Toggle).Checked) ApplyEffects(sender);

            // Ensure if all toggles are not applied, if so, reapply effects
            bool anyChecked = panel1.GetAllControls().OfType<Toggle>().Any(t => t.Checked);

            if (!anyChecked) ApplyEffects(sender);
        }


        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            panel14.Enabled = (sender as Toggle).Checked;

            ApplyEffects(sender);
        }
    }
}