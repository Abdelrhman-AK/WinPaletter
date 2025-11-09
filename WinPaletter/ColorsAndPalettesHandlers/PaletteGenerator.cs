using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme.Structures;
using WinPaletter.TypesExtensions;
using WinPaletter.UI.AdvancedControls;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;
using static WinPaletter.TypesExtensions.BitmapExtensions;

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
        private List<Color> Colors_List = [];

        /// <summary>
        /// Represents a collection of color items and their default colors.
        /// </summary>
        /// <remarks>Each item in the collection is a tuple containing a <see
        /// cref="UI.Controllers.ColorItem"/>  and its corresponding <see cref="System.Drawing.Color"/>. This collection
        /// is used to manage  and associate color-related data.</remarks>
        private List<(ColorItem, Color)> colorItems = [];
        private Form form;
        ColorEffectControl[] colorEffectControls = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaletteGenerateFromImage"/> class.
        /// </summary>
        public PaletteGenerator()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.DoubleBuffer();

            SuspendLayout();

            Height = checkBox1.Checked ? Height : GroupBox1.Top + bottom_buttons.Height;
            label3.Font = Fonts.ConsoleMedium;

            if (colorEffectControls == null)
            {
                colorEffectControls = new ColorEffectControl[ColorEffect.RegisteredEffects.Count];

                for (int i = 0; i < ColorEffect.RegisteredEffects.Count; i++)
                {
                    ColorEffect effect = ColorEffect.RegisteredEffects[i].Clone();
                    ColorEffectControl colorEffectControl = new() { ColorEffect = effect, Dock = DockStyle.Top };
                    colorEffectControls[i] = colorEffectControl;
                    colorEffectControl.ProcessColorEffect += ColorEffectControl_ProcessColorEffect;
                }

                smoothPanel1.Controls.AddRange(colorEffectControls);
            }

            label3.Text = EffectsSummary();

            ResumeLayout();
        }

        private string EffectsSummary()
        {
            int checkedCount = toggle1.Checked ? smoothPanel1.Controls.OfType<ColorEffectControl>().Count(c => c.ColorEffect.Checked) : 0;

            return $"{Program.Lang.Strings.ColorEffects.TotalEffects}: " + smoothPanel1.Controls.OfType<ColorEffectControl>().Count().ToString() + ", " +
                   $"{(checkedCount > 0 ? ($"{Program.Lang.Strings.ColorEffects.Applied}: {checkedCount}") : Program.Lang.Strings.ColorEffects.NoEffects)}.";
        }

        public void Show(UI.WP.Button button)
        {
            Location = new Point(button.PointToScreen(Point.Empty).X + (button.Width - Width) / 2, button.PointToScreen(Point.Empty).Y + button.Height + 5);
            form = button.FindForm();

            colorItems.Clear();
            colorItems.AddRange(form.GetAllControls().OfType<ColorItem>().Select(ci => (ci, ci.BackColor)));

            foreach (Toggle t in this.GetAllControls().OfType<Toggle>()) t.Checked = false;

            TextBox1.Text = Program.TM.Wallpaper.ImageFile;

            GetColors();

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
            GroupBox1.Enabled = true;

            if ((sender as RadioImage).Checked)
            {
                tablessControl1.SelectedIndex = 1;

                List<Color> colors = [.. flowLayoutPanel1.Controls.OfType<ColorItem>().Select(ci => ci.BackColor)];
                GetColors(colors);
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GroupBox1.Enabled = true;

            if ((sender as RadioImage).Checked)
            {
                tablessControl1.SelectedIndex = 2;

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
        /// Processes a collection of colors, applies effects, and updates the UI with the resulting color items.
        /// </summary>
        /// <remarks>
        /// This method performs the following operations:
        /// <list type="bullet">
        /// <item>Clears the existing color list and UI elements.</item>
        /// <item>Processes a predefined collection of colors, applies an effect to each color, and creates corresponding UI controls.</item>
        /// <item>Updates the UI with the newly created color items.</item>
        /// </list>
        /// The method runs asynchronously and updates the UI on the main thread only when necessary.
        /// </remarks>
        public void GetColors()
        {
            // Run asynchronously to avoid blocking the UI
            _ = Task.Run(() =>
            {
                try
                {
                    if (IsHandleCreated)
                    {
                        // Prepare UI before heavy work
                        Invoke(() =>
                        {
                            Cursor = System.Windows.Forms.Cursors.AppStarting;
                            ImgPaletteContainer.Visible = false;

                            foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>()) ctrl.Dispose();

                            ImgPaletteContainer.Controls.Clear();
                        });
                    }

                    // Extract base colors from the predefined collection
                    Colors_List.Clear();

                    foreach (var (ctrl, color) in colorItems) Colors_List.Add(color);

                    // Sort efficiently (threaded if large)
                    if (Colors_List.Count > 1000)
                        Colors_List = Colors_List.AsParallel().OrderBy(c => c, new RGBColorComparer()).ToList();
                    else
                        Colors_List.Sort(new RGBColorComparer());

                    // Build controls in parallel (off the UI thread)
                    int colorCount = Colors_List.Count;
                    ColorItem[] collection = new ColorItem[colorCount];

                    Parallel.For(0, colorCount, i =>
                    {
                        Color original = Colors_List[i];
                        Color processed = ApplyEffect(original);

                        collection[i] = new()
                        {
                            Size = ColorItem.GetMiniColorItemSize(),
                            AllowDrop = false,
                            PauseColorsHistory = true,
                            BackColor = processed,
                            DefaultBackColor = original // keep original color
                        };
                    });

                    if (IsHandleCreated)
                    {
                        // Apply all UI changes in one batch
                        Invoke(() =>
                        {
                            ImgPaletteContainer.SuspendLayout();
                            ImgPaletteContainer.Controls.AddRange(collection);
                            ImgPaletteContainer.Visible = true;
                            ImgPaletteContainer.ResumeLayout();
                            Cursor = System.Windows.Forms.Cursors.Default;

                            Distribute(false);
                        });
                    }
                }
                catch (Exception ex)
                {
                    if (IsHandleCreated)
                    {
                        // Fail-safe: restore UI state even on error
                        Invoke(() =>
                        {
                            Cursor = System.Windows.Forms.Cursors.Default;
                            ImgPaletteContainer.Visible = true;
                        });
                    }

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error in GetColors: {ex}");
                }
            });
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

            // Create a downscaled copy early
            Bitmap thumbnail = source.Resize(Program.PreviewSize);

            // Start asynchronous background task
            _ = Task.Run(async () =>
            {
                try
                {
                    if (IsHandleCreated)
                    {
                        // Update UI before heavy work
                        Invoke(() =>
                        {
                            Cursor = System.Windows.Forms.Cursors.AppStarting;
                            ImgPaletteContainer.Visible = false;

                            foreach (var item in ImgPaletteContainer.Controls.OfType<ColorItem>()) item.Dispose();

                            ImgPaletteContainer.Controls.Clear();
                        });
                    }

                    PaletteGeneratorSettings settings = new()
                    {
                        ColorCount = 300,
                        ColorQuality = 10,
                        IgnoreWhiteColors = false
                        // All other settings will use their default values
                    };

                    // Await the async palette extraction
                    List<Color> palette = await thumbnail.ToPalette(settings);

                    // Use PLINQ to sort in parallel
                    List<Color> extractedColors = palette.AsParallel().OrderBy(c => c, new RGBColorComparer()).ToList();

                    thumbnail?.Dispose();

                    // Store globally
                    Colors_List.Clear();
                    Colors_List.AddRange(extractedColors);

                    // Build UI controls off the main thread, in parallel
                    ColorItem[] colorItems = new ColorItem[extractedColors.Count];
                    Parallel.For(0, extractedColors.Count, i =>
                    {
                        Color c = extractedColors[i];
                        colorItems[i] = new()
                        {
                            Size = ColorItem.GetMiniColorItemSize(),
                            AllowDrop = false,
                            PauseColorsHistory = true,
                            BackColor = Color.FromArgb(255, ApplyEffect(c)),
                            DefaultBackColor = Color.FromArgb(255, c)
                        };
                    });

                    if (IsHandleCreated)
                    {
                        Invoke(() =>
                        {
                            ImgPaletteContainer.SuspendLayout();
                            ImgPaletteContainer.Controls.AddRange(colorItems);
                            ImgPaletteContainer.Visible = true;
                            ImgPaletteContainer.ResumeLayout();
                            Distribute();
                            Cursor = System.Windows.Forms.Cursors.Default;
                        });
                    }
                }
                catch (Exception ex)
                {
                    if (IsHandleCreated)
                    {
                        Invoke(() =>
                        {
                            Cursor = System.Windows.Forms.Cursors.Default;
                            ImgPaletteContainer.Visible = true;
                        });
                    }

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error in generating palette from image. {ex}");
                }
            });
        }

        public void GetColors(List<Color> colors)
        {
            // Immediately disable UI and show loading state on the UI thread
            Cursor = System.Windows.Forms.Cursors.AppStarting;
            ImgPaletteContainer.Visible = false;

            // Dispose existing controls safely
            foreach (var ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>()) ctrl.Dispose();
            ImgPaletteContainer.Controls.Clear();

            // Heavy work runs fully asynchronously
            _ = Task.Run(() =>
            {
                ConcurrentDictionary<int, byte> colorSet = new();
                ConcurrentBag<Color> colorsList = [];

                Parallel.ForEach(colors, c =>
                {
                    for (float f = -1f; f <= 1f; f += 0.05f)
                    {
                        Color result;

                        result = Color.FromArgb(255, c.CB(f));
                        if (colorSet.TryAdd(result.ToArgb(), 0)) colorsList.Add(result);

                        result = Color.FromArgb(255, c.Dark(f));
                        if (colorSet.TryAdd(result.ToArgb(), 0)) colorsList.Add(result);

                        result = Color.FromArgb(255, c.Light(f));
                        if (colorSet.TryAdd(result.ToArgb(), 0)) colorsList.Add(result);
                    }

                    Color dark = Color.FromArgb(255, c.DarkDark());
                    if (colorSet.TryAdd(dark.ToArgb(), 0)) colorsList.Add(dark);

                    Color light = Color.FromArgb(255, c.LightLight());
                    if (colorSet.TryAdd(light.ToArgb(), 0)) colorsList.Add(light);
                });

                // Convert to List and sort
                List<Color> Colors_List = [.. colorsList];
                Colors_List.Sort(new RGBColorComparer());

                List<ColorItem> collection = new(Colors_List.Count);
                Parallel.ForEach(Colors_List, c =>
                {
                    Color processedColor = ApplyEffect(c);
                    ColorItem item = new()
                    {
                        Size = ColorItem.GetMiniColorItemSize(),
                        AllowDrop = false,
                        PauseColorsHistory = true,
                        BackColor = processedColor,
                        DefaultBackColor = c
                    };

                    lock (collection) // prevent race conditions when adding
                        collection.Add(item);
                });

                if (IsHandleCreated)
                {
                    Invoke(() =>
                    {
                        Cursor = System.Windows.Forms.Cursors.Default;
                        ImgPaletteContainer.SuspendLayout();
                        ImgPaletteContainer.Controls.AddRange([.. collection]);
                        ImgPaletteContainer.Visible = true;
                        ImgPaletteContainer.ResumeLayout();

                        Distribute();
                    });
                }
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Distributes colors to items by matching each color to the nearest color in the provided palette.
        /// </summary>
        /// <remarks>This method updates the background color of each <see cref="ColorItem"/> in the
        /// collection  by finding the closest matching color from the palette. The method assumes that the palette  is
        /// derived from the current state of the <see cref="ImgPaletteContainer"/> controls.</remarks>
        /// <param name="nearing">A boolean value indicating whether the distribution process should prioritize finding the nearest color. The
        /// default value is <see langword="true"/>.</param>
        void Distribute(bool nearing = true)
        {
            if (Colors_List != null && Colors_List.Count > 0)
            {
                List<Color> processedColors = [.. ImgPaletteContainer.Controls.OfType<ColorItem>().Select(ci => ci.BackColor)];

                if (processedColors.Count > 0)
                {
                    foreach ((ColorItem, Color) item in colorItems)
                    {
                        Color color = item.Item2;
                        if (checkBox2.Checked && flowLayoutPanel1.Controls.OfType<ColorEffectControl>().Any(c => c.ColorEffect.Checked)) color = color.Grayscale();
                        item.Item1.BackColor = Color.FromArgb(255, nearing ? color.GetNearestColorFromPalette(processedColors) : ApplyEffect(color));
                    }
                }
            }

            ApplyPreview(form);
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
            CreateColorItem(Program.Style.Schemes.Main.Colors.Accent);
        }

        void CreateColorItem(Color c, bool skipUpdatingPalette = false)
        {
            ColorItem colorItem = new()
            {
                Size = ColorItem.GetMiniColorItemSize(),
                AllowDrop = false,
                PauseColorsHistory = true,
                CancelShowingMenu = true,
                BackColor = c,
                DefaultBackColor = c
            };

            colorItem.MouseClick += (s, ev) =>
            {
                if (ev.Button == MouseButtons.Right)
                {
                    flowLayoutPanel1.Controls.Remove(s as ColorItem);
                    (s as ColorItem).Click -= null;
                    (s as ColorItem)?.Dispose();
                }
                else
                {
                    Dictionary<Control, string[]> Dict = new()
                        {
                            { (s as ColorItem), [nameof(colorItem.BackColor)] }
                        };

                    (s as ColorItem).BackColor = Forms.ColorPickerDlg.Pick(Dict);
                }

                List<Color> colors = [.. flowLayoutPanel1.Controls.OfType<ColorItem>().Select(ci => ci.BackColor)];
                GetColors(colors);
            };

            flowLayoutPanel1.Controls.Add(colorItem);

            if (!skipUpdatingPalette)
            {
                List<Color> colors = [.. flowLayoutPanel1.Controls.OfType<ColorItem>().Select(ci => ci.BackColor)];
                GetColors(colors);
            }
        }

        private void radioImage2_CheckedChanged(object sender, EventArgs e)
        {
            GroupBox1.Enabled = true;

            if ((sender as UI.WP.RadioImage).Checked)
            {
                tablessControl1.SelectedIndex = 0;

                GetColors();
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

        private void ColorEffectControl_ProcessColorEffect(object sender, EventArgs e)
        {
            label3.Text = EffectsSummary();
            ApplyEffects();
            Distribute(!radioImage2.Checked);
        }

        Color ApplyEffect(Color c)
        {
            if (!toggle1.Checked) return c;

            // Apply all enabled effects in order
            foreach (ColorEffectControl ctrl in smoothPanel1.Controls.OfType<ColorEffectControl>())
            {
                if (ctrl.ColorEffect.Checked)
                {
                    c = ctrl.ColorEffect.Apply(c);
                }
            }

            return c;
        }

        void ApplyEffects()
        {
            foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>())
            {
                ctrl.BackColor = ApplyEffect(ctrl.DefaultBackColor);
            }
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            panel14.Enabled = (sender as Toggle).Checked;
            label3.Text = EffectsSummary();

            ApplyEffects();
            Distribute(!radioImage2.Checked);
        }

        private void effectsBars_ValueChanged(object sender, EventArgs e)
        {
            if (toggle1.Checked)
            {
                ApplyEffects();
                Distribute(!radioImage2.Checked);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (Program.Style.Animations)
                {
                    FluentTransitions.Transition.With(this, nameof(Height), GroupBox1.Bottom + bottom_buttons.Height + 5)
                        .HookOnCompletion(() => Program.Animator.ShowSync(GroupBox1))
                        .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
                else
                {
                    GroupBox1.Visible = true;
                    Height = GroupBox1.Bottom + bottom_buttons.Height + 5;
                }
            }
            else
            {
                if (Program.Style.Animations)
                {
                    FluentTransitions.Transition.With(this, nameof(Height), GroupBox1.Top + bottom_buttons.Height)
                        .HookOnCompletion(() => Program.Animator.HideSync(GroupBox1))
                        .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
                else
                {
                    GroupBox1.Visible = false;
                    Height = GroupBox1.Top + bottom_buttons.Height;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Distribute();
        }
    }
}