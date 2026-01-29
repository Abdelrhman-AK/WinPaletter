using Cyotek.Windows.Forms;
using Devcorp.Controls.VisualStyles;
using FluentTransitions;
using libmsstyle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Dialogs;
using WinPaletter.Theme;
using WinPaletter.UI.AdvancedControls;
using WinPaletter.UI.Controllers;
using static WinPaletter.TypesExtensions.BitmapExtensions;

namespace WinPaletter
{
    public partial class ColorPickerDlg
    {
        private Color InitColor;
        private Image img;
        private readonly List<Control> ChildControls_List = [];
        private Dictionary<Control, string[]> ColorControls_List = [];
        private readonly List<Form> Forms_List = [];
        private List<Color> Colors_List = [];
        private bool enableAlpha;
        private Point newPoint = new();
        private Point xPoint = new();
        private bool _effectsInitialized;
        private Color _effectsBaseColor;
        CancellationTokenSource cts = new();
        private static readonly FieldInfo[] Win32UI_ColorFields = [.. typeof(Theme.Structures.Win32UI).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(f => f.FieldType == typeof(Color))];
        private static MagnifierDlg _magnifierForm;

        public ColorPickerDlg()
        {
            InitializeComponent();
        }

        private static bool CanInvoke(Control c)
        {
            return c != null && !c.IsDisposed && c.IsHandleCreated;
        }

        private void ColorPicker_MouseDown(object sender, MouseEventArgs e)
        {
            xPoint = MousePosition - (Size)Location;
        }

        private void ColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)xPoint;
                Location = newPoint;
            }
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {
            ComboBox1.Items.Clear();
            ComboBox1.Items.AddRange([.. Schemes.ClassicColors.Split('\n').Select(f => f.Split('|')[0])]);
            ColorEditor1.Font = Fonts.ConsoleMedium;
            cts??= new();
        }

        private void ColorPickerDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts?.Cancel();
        }

        public void GetColorsHistory(ColorItem ColorItem)
        {
            FlowLayoutPanel1.SuspendLayout();

            foreach (ColorItem c in FlowLayoutPanel1.Controls.OfType<ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                FlowLayoutPanel1.Controls.Remove(c);
            }

            FlowLayoutPanel1.Controls.Clear();

            // Skip the first color because it's default gray and the second color is the current color
            foreach (Color c in ColorItem.ColorsHistory.Skip(1))
            {
                ColorItem MiniColorItem = new()
                {
                    Size = ColorItem.GetMiniColorItemSize(),
                    AllowDrop = false,
                    PauseColorsHistory = true,
                    BackColor = c
                };
                MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                FlowLayoutPanel1.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_click;
            }

            FlowLayoutPanel1.ResumeLayout();
        }

        public void GetColorsFromPalette(Manager TM)
        {
            PaletteContainer.SuspendLayout();

            foreach (ColorItem c in PaletteContainer.Controls.OfType<ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                PaletteContainer.Controls.Remove(c);
            }

            PaletteContainer.Controls.Clear();

            foreach (Color c in TM.Palette())
            {

                ColorItem MiniColorItem = new()
                {
                    Size = ColorItem.GetMiniColorItemSize(),
                    AllowDrop = false,
                    PauseColorsHistory = true,
                    BackColor = c,
                    DefaultBackColor = c
                };

                PaletteContainer.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_click;
            }

            PaletteContainer.ResumeLayout();
        }

        private void ScreenColorPicker1_MouseDown(object sender, MouseEventArgs e)
        {
            // 1. Hide all child controls of this form
            ChildControls_List.Clear();
            foreach (Control ctrl in Controls)
            {
                if (ctrl is not ScreenColorPicker && ctrl.Visible)
                {
                    ctrl.Visible = false;
                    ChildControls_List.Add(ctrl);
                }
            }

            // 2. Hide all open forms
            Forms_List.Clear();
            foreach (Form f in Application.OpenForms)
            {
                if (f != this && f.Visible)
                {
                    Forms_List.Add(f);
                    f.Visible = false;
                }
            }
            Opacity = 0d;

            // 3. Show the magnifier following cursor
            if (_magnifierForm == null || _magnifierForm.IsDisposed)
            {
                _magnifierForm = new()
                {
                    Size = new(96, 96)
                };
                _magnifierForm.Show();
            }
        }

        private void ScreenColorPicker1_MouseUp(object sender, MouseEventArgs e)
        {
            // Restore child controls
            foreach (Control ctrl in ChildControls_List) ctrl.Visible = true;

            // Restore other forms
            foreach (Form f in Forms_List) f.Visible = true;

            Forms_List.Clear();
            ChildControls_List.Clear();

            // Restore form transparency
            Opacity = 1d;

            // Close magnifier
            if (_magnifierForm != null && !_magnifierForm.IsDisposed) _magnifierForm.Close();
        }

        /// <summary>
        /// Pick a color to set it to the BackColor property of a control
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="propertyName"></param>
        /// <param name="EnableAlpha"></param>
        /// <returns></returns>
        public Color Pick(Control ctrl, string propertyName, bool EnableAlpha = false)
        {
            Dictionary<Control, string[]> dict = new()
            {
                { ctrl, [propertyName] }
            };

            return Pick(dict, EnableAlpha);
        }

        /// <summary>
        /// Pick a color to set it to the BackColor property of a control
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="EnableAlpha"></param>
        /// <returns></returns>
        public Color Pick(Control ctrl, bool EnableAlpha = false)
        {
            Dictionary<Control, string[]> dict = new()
            {
                { ctrl, [nameof(ctrl.BackColor)] }
            };

            return Pick(dict, EnableAlpha);
        }

        /// <summary>
        /// Pick a color to set it to properties of a set of controls provided by properties names
        /// </summary>
        /// <param name="ControlsWithProperties"></param>
        /// <param name="EnableAlpha"></param>
        /// <returns></returns>
        public Color Pick(Dictionary<Control, string[]> ControlsWithProperties, bool EnableAlpha = false)
        {
            if (ControlsWithProperties.Count == 0) return Color.Empty;

            // Early extraction of key controls to avoid repeated lookups
            ColorItem colorItem = null;
            Control firstControl = null;

            foreach (var key in ControlsWithProperties.Keys)
            {
                if (colorItem == null && key is ColorItem ci)
                {
                    colorItem = ci;
                    firstControl ??= key;
                }
                else if (firstControl == null)
                {
                    firstControl = key;
                }

                // If we found both, break early
                if (colorItem != null && firstControl != null && colorItem != firstControl) break;
            }

            if (firstControl == null) return Color.Empty;

            string colorItemProperty = colorItem != null
                ? (ControlsWithProperties[colorItem]?.FirstOrDefault() ?? "BackColor")
                : "BackColor";

            string firstControlProperty = ControlsWithProperties[firstControl]?.FirstOrDefault() ?? "BackColor";

            Color originalColor = colorItem != null
                ? colorItem.GetProperty<Color>(colorItemProperty)
                : firstControl.GetProperty<Color>(firstControlProperty);

            if (Program.Settings.NerdStats.Classic_Color_Picker)
            {
                return UseClassicColorPicker(ControlsWithProperties, colorItem, originalColor);
            }

            return UseCustomColorPicker(ControlsWithProperties, colorItem, firstControl, originalColor, EnableAlpha);
        }

        private Color UseCustomColorPicker(Dictionary<Control, string[]> controlsWithProperties, ColorItem colorItem, Control firstControl, Color originalColor, bool enableAlpha)
        {
            using (ColorPickerDlg dlg = new())
            {
                dlg.InitColor = originalColor;
                dlg.enableAlpha = enableAlpha;
                dlg.ColorControls_List = controlsWithProperties;

                dlg.ColorEditorManager1.Color = originalColor;
                dlg.ColorEditorManager1.ColorEditor.ShowAlphaChannel = enableAlpha;

                // Batch add color effects
                dlg._effectsBaseColor = originalColor;

                PositionDialog(dlg, colorItem, firstControl);

                DialogResult result = dlg.ShowDialog();
                Color selectedColor = result == DialogResult.OK ? dlg.ColorEditorManager1.Color : dlg.InitColor;

                UpdateControlsWithTransition(controlsWithProperties, colorItem, selectedColor);

                return enableAlpha ? selectedColor : Color.FromArgb(255, selectedColor);
            }
        }

        private Color UseClassicColorPicker(Dictionary<Control, string[]> controlsWithProperties, ColorItem colorItem, Color originalColor)
        {
            int[] colors = colorItem != null ? [.. colorItem.ColorsHistory.Select(ColorTranslator.ToWin32)] : [];

            using (ColorDialog CCP = new()
            {
                AllowFullOpen = true,
                AnyColor = true,
                Color = originalColor,
                FullOpen = true,
                SolidColorOnly = false,
                CustomColors = colors
            })
            {
                if (CCP.ShowDialog() == DialogResult.OK)
                {
                    UpdateControlsWithoutTransition(controlsWithProperties, CCP.Color);
                    return CCP.Color;
                }

                UpdateControlsWithoutTransition(controlsWithProperties, originalColor);
                return originalColor;
            }
        }

        private async Task AddColorEffects(ColorPickerDlg dlg, Color baseColor)
        {
            if (ColorEffect.RegisteredEffects.Count == 0 || dlg.IsDisposed)
                return;

            // Show loading indicator
            var loadingLabel = new Label
            {
                Text = Program.Localization.Strings.ColorEffects.LoadingEffects,
                Dock = DockStyle.Top,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 30
            };

            dlg.panel1.SuspendLayout();
            dlg.panel1.Controls.Add(loadingLabel);
            dlg.panel1.ResumeLayout();

            // Generate effect controls in background
            var controls = await Task.Run(() =>
            {
                var list = new List<ColorEffectMiniControl>();
                foreach (var effect in ColorEffect.RegisteredEffects)
                {
                    var cloned = effect.Clone();
                    cloned.Checked = true;
                    cloned.InputColor = baseColor;

                    list.Add(new ColorEffectMiniControl
                    {
                        ColorEffect = cloned,
                        Dock = DockStyle.Top
                    });
                }
                return list;
            });

            // Ensure dialog is still alive and handle exists
            if (dlg.IsDisposed || !dlg.IsHandleCreated)
                return;

            // Add controls on UI thread (we are already on UI thread after await)
            dlg.panel1.SuspendLayout();
            dlg.panel1.Controls.Remove(loadingLabel);

            foreach (var control in controls)
            {
                control.SendColorClick += (s, e) =>
                {
                    if (e is ColorEffectMiniControl.ColorEventArgs args)
                        dlg.ColorEditorManager1.Color = args.Color;
                };
                dlg.panel1.Controls.Add(control);
            }

            dlg.panel1.ResumeLayout();
        }

        private void PositionDialog(ColorPickerDlg dlg, ColorItem colorItem, Control firstControl)
        {
            Point location;

            if (colorItem != null)
            {
                dlg.GetColorsHistory(colorItem);
                colorItem.PauseColorsHistory = true;
                colorItem.ColorPickerOpened = true;
                location = colorItem.PointToScreen(Point.Empty) + new Size(-dlg.Width + colorItem.Width + 5, colorItem.Height - 1);
            }
            else
            {
                location = firstControl.PointToScreen(Point.Empty) + new Size(-dlg.Width + firstControl.Width + 5, firstControl.Height - 1);
            }

            // Ensure dialog is within screen bounds
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;

            // Adjust Y coordinate
            if (location.Y + dlg.Height > screenBounds.Height) location.Y = screenBounds.Height - dlg.Height;
            else if (location.Y < 0) location.Y = 0;

            // Adjust X coordinate
            if (location.X + dlg.Width > screenBounds.Width) location.X = screenBounds.Width - dlg.Width;
            else if (location.X < 0) location.X = 0;

            dlg.Location = location;
        }

        private void UpdateControlsWithTransition(Dictionary<Control, string[]> controlsWithProperties, ColorItem colorItem, Color targetColor)
        {
            List<Action> completionActions = [];

            if (colorItem != null)
            {
                completionActions.Add(() =>
                {
                    colorItem.Refresh();
                    colorItem.PauseColorsHistory = false;
                    colorItem.ColorPickerOpened = false;
                    colorItem.UpdateColorsHistory();
                });
            }

            foreach (KeyValuePair<Control, string[]> entry in controlsWithProperties)
            {
                string[] properties = entry.Value ?? ["BackColor"];

                foreach (string property in properties)
                {
                    string prop = string.IsNullOrEmpty(property) ? "BackColor" : property;

                    try
                    {
                        if (entry.Key.GetProperty<Color>(prop) != targetColor)
                        {
                            Transition.With(entry.Key, prop, targetColor)
                                      .HookOnCompletion(() =>
                                      {
                                          foreach (Action action in completionActions) action();
                                      })
                                      .Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                        }
                    }
                    catch
                    {
                        // Fallback without transition
                        try
                        {
                            if (entry.Key.GetProperty<Color>(prop) != targetColor)
                            {
                                entry.Key.SetProperty(prop, targetColor);
                            }
                        }
                        catch
                        {
                            // Ignore if property doesn't exist
                        }

                        // Execute completion actions immediately for fallback
                        foreach (Action action in completionActions) action();
                    }
                }
            }
        }

        private void UpdateControlsWithoutTransition(Dictionary<Control, string[]> controlsWithProperties, Color color)
        {
            foreach (KeyValuePair<Control, string[]> entry in controlsWithProperties)
            {
                string[] properties = entry.Value ?? ["BackColor"];

                foreach (string property in properties)
                {
                    try
                    {
                        if (entry.Key.GetProperty<Color>(property ?? "BackColor") != color)
                        {
                            entry.Key.SetProperty(property ?? "BackColor", color);
                        }
                    }
                    catch
                    {
                        // Ignore if property doesn't exist
                    }
                }
            }
        }

        private void Change_Color_Preview(object sender, EventArgs e)
        {
            Color color = enableAlpha ? ColorEditorManager1.Color : Color.FromArgb(255, ColorEditorManager1.Color);

            foreach (KeyValuePair<Control, string[]> entry in ColorControls_List)
            {
                foreach (string prop in entry.Value)
                {
                    try
                    {
                        Transition.With(entry.Key, prop ?? "BackColor", color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                    catch
                    {
                        entry.Key.SetProperty(prop ?? "BackColor", color);
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Deactivate();
            //DialogResult = DialogResult.No;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            img = RadioButton1.Checked ? Program.AppliedWallpaper : BitmapMgr.Load(TextBox1.Text);

            if (CheckBox2.Checked) img = img.Resize(300, 300);

            if (img is not null)
            {
                Label4.Text = Program.Localization.Strings.Tips.PaletteExtraction;
                Program.Animator.HideSync(Button6, true);
                Program.Animator.HideSync(ImgPaletteContainer, true);
                ProgressBar1.Visible = true;
                Colors_List.Clear();

                Task.Run(async () =>
                {
                    if (cts?.IsCancellationRequested ?? false) return;

                    if (img is not null)
                    {
                        PaletteGeneratorSettings settings = new()
                        {
                            ColorCount = (int)trackBarX1.Value,
                            ColorQuality = (int)trackBarX2.Value,
                            IgnoreWhiteColors = CheckBox1.Checked
                        };

                        if (cts?.IsCancellationRequested ?? false) return;
                        Colors_List = await (img as Bitmap).ToPalette(settings);
                        img?.Dispose();
                    }

                    if (CanInvoke(this))
                    {
                        BeginInvoke(() =>
                        {
                            if (cts?.IsCancellationRequested ?? false) return;

                            foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>())
                            {
                                ctrl.Click -= MiniColorItem_click;
                                ctrl.Dispose();
                            }

                            if (cts?.IsCancellationRequested ?? false) return;

                            ImgPaletteContainer.Controls.Clear();

                            Colors_List = [.. Colors_List.Distinct()];
                            Colors_List.Sort(new RGBColorComparer());

                            if (cts?.IsCancellationRequested ?? false) return;

                            ImgPaletteContainer.SuspendLayout();

                            List<ColorItem> items = [.. Colors_List.Distinct().OrderBy(c => c, new RGBColorComparer())
                                            .Select(c => new ColorItem
                                            {
                                                Size = ColorItem.GetMiniColorItemSize(),
                                                AllowDrop = false,
                                                PauseColorsHistory = true,
                                                BackColor = Color.FromArgb(255, c),
                                                DefaultBackColor = Color.FromArgb(255, c)
                                            })];

                            if (cts?.IsCancellationRequested ?? false) return;

                            ImgPaletteContainer.Controls.AddRange([.. items]);

                            if (cts?.IsCancellationRequested ?? false) return;

                            foreach (var item in items) item.Click += MiniColorItem_click;

                            ImgPaletteContainer.ResumeLayout();

                            if (cts?.IsCancellationRequested ?? false) return;

                            ProgressBar1.Visible = false;
                            Colors_List.Clear();
                            Program.Animator.ShowSync(ImgPaletteContainer, true);
                            Program.Animator.ShowSync(Button6, true);
                        });
                    }
                });
            }
        }

        private void MiniColorItem_click(object sender, EventArgs e)
        {
            ColorEditorManager1.Color = (sender as ColorItem).BackColor;
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Localization.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.FileName;
                }
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Palettes, Title = Program.Localization.Strings.Extensions.OpenPalette })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ColorGrid1.Colors = ColorCollection.LoadPalette(dlg.FileName);
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Localization.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox2.Text = dlg.FileName;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBox2.SelectedIndex)
            {
                case 0:
                    {
                        GetColorsFromPalette(Program.TM);
                        break;
                    }
                case 1:
                    {
                        GetColorsFromPalette(Default.Windows12);
                        break;
                    }
                case 2:
                    {
                        GetColorsFromPalette(Default.Windows11);
                        break;
                    }
                case 3:
                    {
                        GetColorsFromPalette(Default.Windows10);
                        break;
                    }
                case 4:
                    {
                        GetColorsFromPalette(Default.Windows81);
                        break;
                    }
                case 5:
                    {
                        GetColorsFromPalette(Default.Windows8);
                        break;
                    }
                case 6:
                    {
                        GetColorsFromPalette(Default.Windows7);
                        break;
                    }
                case 7:
                    {
                        GetColorsFromPalette(Default.WindowsVista);
                        break;
                    }
                case 8:
                    {
                        GetColorsFromPalette(Default.WindowsXP);
                        break;
                    }
                default:
                    {
                        GetColorsFromPalette(Program.TM);
                        break;
                    }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem is null)
                return;

            foreach (ColorItem c in ThemePaletteContainer.Controls.OfType<ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                ThemePaletteContainer.Controls.Remove(c);
            }

            ThemePaletteContainer.Controls.Clear();

            if (!string.IsNullOrWhiteSpace(ComboBox1.SelectedItem.ToString()))
            {
                List<Color> colors = Manager.ListColorsFromString(Schemes.ClassicColors, ComboBox1.SelectedItem.ToString());
                if (colors is not null && colors.Count > 0)
                {
                    foreach (Color C in colors)
                    {
                        ColorItem MiniColorItem = new();
                        MiniColorItem.Size = ColorItem.GetMiniColorItemSize();
                        MiniColorItem.AllowDrop = false;
                        MiniColorItem.PauseColorsHistory = true;
                        MiniColorItem.BackColor = C;
                        MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                        ThemePaletteContainer.Controls.Add(MiniColorItem);
                        MiniColorItem.Click += MiniColorItem_click;
                    }
                }
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(TextBox2.Text))
            {
                foreach (ColorItem c in ThemePaletteContainer.Controls.OfType<ColorItem>())
                {
                    c.Click -= MiniColorItem_click;
                    c.Dispose();
                    ThemePaletteContainer.Controls.Remove(c);
                }

                ThemePaletteContainer.Controls.Clear();

                List<Color> colors = [];
                colors.Clear();

                if (Path.GetExtension(TextBox2.Text).ToLower() == ".theme")
                {
                    try
                    {
                        colors = Manager.ListColorsFromMSTheme(TextBox2.Text);
                        if (colors is not null && colors.Count > 0)
                        {
                            foreach (Color C in colors)
                            {
                                ColorItem MiniColorItem = new();
                                MiniColorItem.Size = ColorItem.GetMiniColorItemSize();
                                MiniColorItem.AllowDrop = false;
                                MiniColorItem.PauseColorsHistory = true;
                                MiniColorItem.BackColor = C;
                                MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                                ThemePaletteContainer.Controls.Add(MiniColorItem);
                                MiniColorItem.Click += MiniColorItem_click;
                            }
                        }
                    }
                    catch
                    {
                        MsgBox(Program.Localization.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (Path.GetExtension(TextBox2.Text).ToLower() == ".msstyles" || colors.Count == 0)
                {
                    try
                    {
                        using (VisualStyle visualStyle = new(TextBox2.Text))
                        {
                            Theme.Structures.Win32UI win32UI = visualStyle.ClassicColors();
                            foreach (FieldInfo field in Win32UI_ColorFields)
                            {
                                if (field.FieldType.Name.ToLower() == "color")
                                {
                                    ColorItem MiniColorItem = new();
                                    MiniColorItem.Size = ColorItem.GetMiniColorItemSize();
                                    MiniColorItem.AllowDrop = false;
                                    MiniColorItem.PauseColorsHistory = true;
                                    MiniColorItem.BackColor = (Color)field.GetValue(win32UI);
                                    MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                                    ThemePaletteContainer.Controls.Add(MiniColorItem);
                                    MiniColorItem.Click += MiniColorItem_click;
                                }
                            }
                        }
                    }
                    catch
                    {
                        try
                        {
                            File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={TextBox2.Text}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");

                            using (VisualStyleFile vs = new($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme"))
                            {
                                foreach (FieldInfo field in typeof(VisualStyleMetricColors).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                                {
                                    if (field.FieldType.Name.ToLower() == "color")
                                    {
                                        ColorItem MiniColorItem = new();
                                        MiniColorItem.Size = ColorItem.GetMiniColorItemSize();
                                        MiniColorItem.AllowDrop = false;
                                        MiniColorItem.PauseColorsHistory = true;
                                        MiniColorItem.BackColor = (Color)field.GetValue(vs.Metrics.Colors);
                                        MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                                        ThemePaletteContainer.Controls.Add(MiniColorItem);
                                        MiniColorItem.Click += MiniColorItem_click;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MsgBox(Program.Localization.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void ColorPickerDlg_Shown(object sender, EventArgs e)
        {
            if (_effectsInitialized) return;

            _effectsInitialized = true;
            await AddColorEffects(this, _effectsBaseColor);
        }
    }
}