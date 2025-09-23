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
using WinPaletter.Theme;
using WinPaletter.TypesExtensions;
using WinPaletter.UI.Controllers;

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
        Thread thread;
        private List<Color> imageColors = [];

        public ColorPickerDlg()
        {
            InitializeComponent();
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
            ComboBox1.Items.AddRange(Schemes.ClassicColors.Split('\n').Select(f => f.Split('|')[0]).ToArray());
        }

        private void ColorPickerDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread is not null && thread.IsAlive) thread.Abort();
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
                ColorItem MiniColorItem = new();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
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

            foreach (Color c in TM.Palette)
            {

                ColorItem MiniColorItem = new();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
                MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                PaletteContainer.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_click;
            }

            PaletteContainer.ResumeLayout();
        }

        private void ScreenColorPicker1_MouseDown(object sender, MouseEventArgs e)
        {
            Forms_List.Clear();
            ChildControls_List.Clear();

            CloseOnLostFocus = false;

            foreach (Control ctrl in Controls)
            {
                if (ctrl is not ScreenColorPicker & ctrl.Visible)
                {
                    ctrl.Visible = false;
                    ChildControls_List.Add(ctrl);
                }
            }

            for (int ix = Application.OpenForms.Count - 1; ix >= 0; ix -= 1)
            {
                if (Application.OpenForms[ix].Visible & Application.OpenForms[ix] != this) Forms_List.Add(Application.OpenForms[ix]);
            }

            for (int ix = 0, loopTo = Forms_List.Count - 1; ix <= loopTo; ix++) Forms_List[ix].Visible = false;

            AllowTransparency = true;
            TransparencyKey = BackColor;
        }

        private void ScreenColorPicker1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Control ctrl in ChildControls_List) ctrl.Visible = true;

            for (int ix = 0, loopTo = Forms_List.Count - 1; ix <= loopTo; ix++) Forms_List[ix].Visible = true;

            Forms_List.Clear();
            ChildControls_List.Clear();

            CloseOnLostFocus = true;

            AllowTransparency = false;
            TransparencyKey = default;
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

            ColorItem colorItem = ControlsWithProperties.Keys.OfType<ColorItem>().FirstOrDefault();
            Control firstControl = ControlsWithProperties.Keys.FirstOrDefault();

            string colorItemProperty = ControlsWithProperties[colorItem]?.FirstOrDefault() ?? "BackColor";
            string firstControlProperty = ControlsWithProperties[firstControl]?.FirstOrDefault() ?? "BackColor";

            Color c = colorItem != null ? colorItem.GetProperty<Color>(colorItemProperty) : firstControl.GetProperty<Color>(firstControlProperty);

            if (!Program.Settings.NerdStats.Classic_Color_Picker)
            {
                InitColor = c;
                enableAlpha = EnableAlpha;
                ColorControls_List = ControlsWithProperties;

                ColorEditorManager1.Color = c;
                ColorEditorManager1.ColorEditor.ShowAlphaChannel = enableAlpha;

                ImagePath.Text = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty).ToString();

                effect_dark.BackColor = c.Dark(trackBar1.Value / 100f);
                effect_light.BackColor = c.Light(trackBar2.Value / 100f);
                effect_desaturate.BackColor = c.Desaturate(trackBar4.Value / 100f);
                effect_rotateHue.BackColor = c.RotateHue(trackBar5.Value);
                effect_invert.BackColor = c.Invert();
                effect_reverse.BackColor = c.Reverse();
                effect_sepia.BackColor = c.Sepia();
                effect_websafe.BackColor = c.ToWebSafe();
                effect_256.BackColor = c.To256Color();
                effect_frutigerAero.BackColor = c.ToFrutigerAero();
                effect_metro.BackColor = c.ToMetro();
                effect_monochrome.BackColor = c.Monochrome();
                effect_grayscale.BackColor = c.Grayscale();
                effect_macOS.BackColor = c.ToMacSemantic();
                effect_Material.BackColor = c.ToMaterial();
                effect_MaterialExpressive.BackColor = c.ToMaterialExpressive3();

                foreach (ColorItem effect in panel1.Controls.OfType<ColorItem>()) effect.DefaultBackColor = effect.BackColor;

                if (colorItem != null)
                {
                    GetColorsHistory(colorItem);
                    colorItem.PauseColorsHistory = true;
                    colorItem.ColorPickerOpened = true;
                    Location = colorItem.PointToScreen(Point.Empty) + (Size)new Point(-Width + colorItem.Width + 5, colorItem.Height - 1);
                }
                else
                {
                    Location = firstControl.PointToScreen(Point.Empty) + (Size)new Point(-Width + firstControl.Width + 5, firstControl.Height - 1);
                }

                if (Location.Y + Height > Screen.PrimaryScreen.Bounds.Height) Location = new(Location.X, Screen.PrimaryScreen.Bounds.Height - Height);
                if (Location.X + Width > Screen.PrimaryScreen.Bounds.Width) Location = new(Screen.PrimaryScreen.Bounds.Width - Width, Location.Y);

                if (Location.Y < 0) Location = new(Location.X, 0);
                if (Location.X < 0) Location = new(0, Location.Y);

                DialogResult result = ShowDialog();

                c = result == DialogResult.OK ? ColorEditorManager1.Color : InitColor;

                /* 
                 Change_Color_Preview uses FluentTransitions. If FluentTransitions starts and cancel is pressed, the color won't be reverted back to the original color
                ;so we need to revert it manually using FluentTransitions.
                 */
                foreach (KeyValuePair<Control, string[]> entry in ColorControls_List)
                {
                    foreach (string prop in entry.Value)
                    {
                        try
                        {
                            Transition
                                .With(entry.Key, prop ?? "BackColor", c)
                                .HookOnCompletion(() =>
                                {
                                    if (colorItem != null)
                                    {
                                        colorItem.Refresh();
                                        colorItem.PauseColorsHistory = false;
                                        colorItem.ColorPickerOpened = false;
                                        colorItem.UpdateColorsHistory();
                                    }
                                })
                                .Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                        }
                        catch
                        {
                            entry.Key.SetProperty(prop ?? "BackColor", c);

                            if (colorItem != null)
                            {
                                colorItem.Refresh();
                                colorItem.PauseColorsHistory = false;
                                colorItem.ColorPickerOpened = false;
                                colorItem.UpdateColorsHistory();
                            }
                        }
                    }
                }

                return enableAlpha ? c : Color.FromArgb(255, c);
            }

            else
            {
                int[] colors = colorItem.ColorsHistory.Select(x => ColorTranslator.ToWin32(x)).ToArray();

                using (ColorDialog CCP = new() { AllowFullOpen = true, AnyColor = true, Color = c, FullOpen = true, SolidColorOnly = false, CustomColors = colors })
                {
                    if (CCP.ShowDialog() == DialogResult.OK)
                    {
                        foreach (KeyValuePair<Control, string[]> entry in ColorControls_List)
                        {
                            foreach (string prop in entry.Value)
                            {
                                try { entry.Key.SetProperty(prop ?? "BackColor", CCP.Color); }
                                catch { } // Ignore setting BackColor in a control that doesn't have BackColor property
                            }
                        }

                        return CCP.Color;
                    }

                    else
                    {
                        foreach (KeyValuePair<Control, string[]> entry in ColorControls_List)
                        {
                            foreach (string prop in entry.Value)
                            {
                                try { entry.Key.SetProperty(prop ?? "BackColor", c); }
                                catch { } // Ignore setting BackColor in a control that doesn't have BackColor property
                            }
                        }

                        return c;
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
            img = RadioButton1.Checked ? Program.Wallpaper : BitmapMgr.Load(TextBox1.Text);

            if (CheckBox2.Checked) img = img.Resize(300, 300);

            if (img is not null)
            {
                Label4.Text = Program.Lang.Strings.Tips.PaletteExtraction;
                Program.Animator.HideSync(Button6, true);
                Program.Animator.HideSync(ImgPaletteContainer, true);
                ProgressBar1.Visible = true;
                Colors_List.Clear();

                if (thread is not null && thread.IsAlive) thread.Abort();

                thread = new(() =>
                {
                    if (img is not null)
                    {
                        Colors_List = img.ToPalette(trackBarX1.Value, trackBarX2.Value, CheckBox1.Checked);
                        img?.Dispose();
                    }

                    Invoke(() =>
                    {
                        foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>())
                        {
                            ctrl.Click -= MiniColorItem_click;
                            ctrl.Dispose();
                        }

                        ImgPaletteContainer.Controls.Clear();

                        Colors_List = Colors_List.Distinct().ToList();
                        Colors_List.Sort(new RGBColorComparer());

                        foreach (Color C in Colors_List)
                        {
                            ColorItem MiniColorItem = new();
                            MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                            MiniColorItem.AllowDrop = false;
                            MiniColorItem.PauseColorsHistory = true;
                            MiniColorItem.BackColor = Color.FromArgb(255, C);
                            MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                            ImgPaletteContainer.Controls.Add(MiniColorItem);
                            MiniColorItem.Click += MiniColorItem_click;
                        }

                        ProgressBar1.Visible = false;
                        Colors_List.Clear();
                        Program.Animator.ShowSync(ImgPaletteContainer, true);
                        Program.Animator.ShowSync(Button6, true);
                    });
                });

                thread.Start();
            }
        }


        private void MiniColorItem_click(object sender, EventArgs e)
        {
            ColorEditorManager1.Color = (sender as ColorItem).BackColor;
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.FileName;
                }
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Palettes, Title = Program.Lang.Strings.Extensions.OpenPalette })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ColorGrid1.Colors = ColorCollection.LoadPalette(dlg.FileName);
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Strings.Extensions.OpenVisualStyle })
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
                        GetColorsFromPalette(Default.Windows12());
                        break;
                    }
                case 2:
                    {
                        GetColorsFromPalette(Default.Windows11());
                        break;
                    }
                case 3:
                    {
                        GetColorsFromPalette(Default.Windows10());
                        break;
                    }
                case 4:
                    {
                        GetColorsFromPalette(Default.Windows81());
                        break;
                    }
                case 5:
                    {
                        GetColorsFromPalette(Default.Windows8());
                        break;
                    }
                case 6:
                    {
                        GetColorsFromPalette(Default.Windows7());
                        break;
                    }
                case 7:
                    {
                        GetColorsFromPalette(Default.WindowsVista());
                        break;
                    }
                case 8:
                    {
                        GetColorsFromPalette(Default.WindowsXP());
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
                        MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
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
                                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
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
                        MsgBox(Program.Lang.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (Path.GetExtension(TextBox2.Text).ToLower() == ".msstyles" || colors.Count == 0)
                {
                    try
                    {
                        using (VisualStyle visualStyle = new(TextBox2.Text))
                        {
                            Theme.Structures.Win32UI win32UI = visualStyle.ClassicColors();
                            foreach (FieldInfo field in typeof(Theme.Structures.Win32UI).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                            {
                                if (field.FieldType.Name.ToLower() == "color")
                                {
                                    ColorItem MiniColorItem = new();
                                    MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
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
                                        MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
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
                            MsgBox(Program.Lang.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void effects_Click(object sender, EventArgs e)
        {
            ColorEditorManager1.Color = (sender as ColorItem).BackColor;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            effect_dark.BackColor = InitColor.Dark(trackBar1.Value / 100f);
            effect_dark.DefaultBackColor = effect_dark.BackColor;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            effect_light.BackColor = InitColor.Light(trackBar2.Value / 100f);
            effect_light.DefaultBackColor = effect_light.BackColor;
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            effect_desaturate.BackColor = InitColor.Desaturate(trackBar4.Value / 100f);
            effect_desaturate.DefaultBackColor = effect_desaturate.BackColor;
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            effect_rotateHue.BackColor = InitColor.RotateHue(trackBar5.Value);
            effect_rotateHue.DefaultBackColor = effect_rotateHue.BackColor;
        }

        private void trackBarX3_ValueChanged(object sender, EventArgs e)
        {
            effect_brightness.BackColor = InitColor.CB((trackBar5.Value - 50f) / 50f);
            effect_brightness.DefaultBackColor = effect_brightness.BackColor;
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
                        imageColors = bmp.ToPalette(100);
                    }
                }
                else
                {
                    imageColors.Clear();
                }

                Invoke(() => 
                {
                    if (imageColors.Count > 0) effect_image.BackColor = InitColor.GetNearestColorFromPalette(imageColors);
                    else effect_image.BackColor = Color.Black;

                    effect_image.DefaultBackColor = effect_image.BackColor;
                });
            });
        }
    }
}