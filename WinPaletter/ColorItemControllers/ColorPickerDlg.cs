using Cyotek.Windows.Forms;
using Devcorp.Controls.VisualStyles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class ColorPickerDlg
    {
        private Color InitColor;
        private Image img;
        private readonly List<Control> ChildControls_List = new();
        private Dictionary<Control, string[]> ColorControls_List = new();
        private readonly List<Form> Forms_List = new();
        private List<Color> Colors_List = new();
        private readonly int _Speed = 40;
        private bool _EnableAlpha;

        #region Form Shadow
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DWMAPI.IsCompositionEnabled())
                {
                    cp.ClassStyle |= DWMAPI.CS_DROPSHADOW;
                    cp.ExStyle |= 33554432;
                    return cp;
                }
                else
                {
                    return cp;
                }
            }
        }

        public ColorPickerDlg()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case DWMAPI.WM_NCPAINT:
                    {
                        int val = 2;
                        if (DWMAPI.IsCompositionEnabled())
                        {
                            DWMAPI.DwmSetWindowAttribute(Handle, Program.Style.RoundedCorners ? 2 : 1, ref val, 4);
                            DWMAPI.MARGINS bla = new();
                            {
                                bla.bottomHeight = 1;
                                bla.leftWidth = 1;
                                bla.rightWidth = 1;
                                bla.topHeight = 1;
                            }
                            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref bla);
                        }

                        break;
                    }
            }

            base.WndProc(ref m);
        }
        #endregion

        private Point newPoint = new();
        private Point xPoint = new();

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

        private void ColorPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE | User32.AnimateWindowFlags.AW_BLEND);
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            ComboBox1.PopulateThemes();

            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE | User32.AnimateWindowFlags.AW_BLEND);
            Invalidate();
        }

        public void GetColorsHistory(UI.Controllers.ColorItem ColorItem)
        {
            FlowLayoutPanel1.SuspendLayout();

            foreach (UI.Controllers.ColorItem c in FlowLayoutPanel1.Controls.OfType<UI.Controllers.ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                FlowLayoutPanel1.Controls.Remove(c);
            }

            FlowLayoutPanel1.Controls.Clear();

            foreach (Color c in ColorItem.ColorsHistory)
            {

                UI.Controllers.ColorItem MiniColorItem = new();
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

        public void GetColorsFromPalette(Theme.Manager TM)
        {
            PaletteContainer.SuspendLayout();

            foreach (UI.Controllers.ColorItem c in PaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                PaletteContainer.Controls.Remove(c);
            }

            PaletteContainer.Controls.Clear();

            foreach (Color c in TM.Colors())
            {

                UI.Controllers.ColorItem MiniColorItem = new();
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

            foreach (Control ctrl in Controls)
            {
                if (!(ctrl is ScreenColorPicker) & ctrl.Visible)
                {
                    ctrl.Visible = false;
                    ChildControls_List.Add(ctrl);
                }
            }

            for (int ix = Application.OpenForms.Count - 1; ix >= 0; ix -= 1)
            {
                if (Application.OpenForms[ix].Visible & Application.OpenForms[ix] != this)
                    Forms_List.Add(Application.OpenForms[ix]);
            }
            for (int ix = 0, loopTo = Forms_List.Count - 1; ix <= loopTo; ix++)
                Forms_List[ix].Visible = false;

            AllowTransparency = true;
            TransparencyKey = BackColor;
        }

        private void ScreenColorPicker1_MouseUp(object sender, MouseEventArgs e)
        {

            foreach (Control ctrl in ChildControls_List)
                ctrl.Visible = true;
            for (int ix = 0, loopTo = Forms_List.Count - 1; ix <= loopTo; ix++)
                Forms_List[ix].Visible = true;
            Forms_List.Clear();
            ChildControls_List.Clear();

            AllowTransparency = false;
            TransparencyKey = default;
        }

        public Color Pick(Dictionary<Control, string[]> ControlsWithProperties, bool EnableAlpha = false)
        {
            if (ControlsWithProperties.Count == 0) return Color.Empty;

            ColorItem colorItem = ControlsWithProperties.Keys.Where(x => x is ColorItem)?.FirstOrDefault() as ColorItem;
            string colorItemProperty = colorItem != null ? ControlsWithProperties[colorItem].FirstOrDefault() : string.Empty;

            Control FirstControl = ControlsWithProperties.Keys.FirstOrDefault();
            string FirstControlProperty = ControlsWithProperties[FirstControl].FirstOrDefault();

            Color c = colorItem != null ?
                      colorItem.GetProperty<Color>(colorItemProperty ?? "BackColor") :
                      FirstControl.GetProperty<Color>(FirstControlProperty ?? "BackColor");

            if (!Program.Settings.NerdStats.Classic_Color_Picker)
            {
                ColorEditorManager1.Color = c;
                InitColor = c;
                _EnableAlpha = EnableAlpha;
                ColorControls_List = ControlsWithProperties;

                ColorEditorManager1.ColorEditor.ShowAlphaChannel = _EnableAlpha;

                if (colorItem != null)
                {
                    GetColorsHistory(colorItem);
                    colorItem.PauseColorsHistory = true;
                    colorItem.ColorPickerOpened = true;
                    colorItem.Refresh();

                    Location = colorItem.PointToScreen(Point.Empty) + (Size)new Point(-Width + colorItem.Width + 5, colorItem.Height - 1);
                }
                else
                {
                    Location = FirstControl.PointToScreen(Point.Empty) + (Size)new Point(-Width + FirstControl.Width + 5, FirstControl.Height - 1);
                }

                if (Location.Y + Height > Screen.PrimaryScreen.Bounds.Height)
                    Location = new(Location.X, Screen.PrimaryScreen.Bounds.Height - Height);
                if (Location.Y < 0)
                    Location = new(Location.X, 0);
                if (Location.X + Width > Screen.PrimaryScreen.Bounds.Width)
                    Location = new(Screen.PrimaryScreen.Bounds.Width - Width, Location.Y);
                if (Location.X < 0)
                    Location = new(0, Location.Y);

                DialogResult result = ShowDialog();

                c = result == DialogResult.OK ? ColorEditorManager1.Color : InitColor;

                if (colorItem != null)
                {
                    colorItem.Refresh();
                    colorItem.PauseColorsHistory = false;
                    colorItem.ColorPickerOpened = false;
                    colorItem.UpdateColorsHistory();
                }

                /* 
                 Change_Color_Preview uses FluentTransitions. If FluentTransitions starts and pressed cancel, the color won't be reverted back to the original color. 
                So we need to revert it manually using FluentTransitions.
                 */
                foreach (KeyValuePair<Control, string[]> entry in ColorControls_List)
                {
                    foreach (string prop in entry.Value)
                    {
                        try
                        {
                            FluentTransitions.Transition.With(entry.Key, prop ?? "BackColor", c).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                        }
                        catch
                        {
                            entry.Key.SetProperty(prop ?? "BackColor", c);
                        }
                    }
                }

                return _EnableAlpha ? c : Color.FromArgb(255, c);
            }

            else
            {
                using (ColorDialog CCP = new() { AllowFullOpen = true, AnyColor = true, Color = c, FullOpen = true, SolidColorOnly = false })
                {
                    if (CCP.ShowDialog() == DialogResult.OK)
                    {
                        foreach (KeyValuePair<Control, string[]> entry in ColorControls_List)
                        {
                            foreach (string prop in entry.Value)
                            {
                                try { entry.Key.SetProperty(prop ?? "BackColor", CCP.Color); } catch { }
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
                                try { entry.Key.SetProperty(prop ?? "BackColor", c); } catch { }
                            }
                        }

                        return c;
                    }
                }
            }
        }

        private void Change_Color_Preview(object sender, EventArgs e)
        {
            Color color = _EnableAlpha ? ColorEditorManager1.Color : Color.FromArgb(255, ColorEditorManager1.Color);

            foreach (KeyValuePair<Control, string[]> entry in ColorControls_List)
            {
                foreach (string prop in entry.Value)
                {
                    try
                    {
                        FluentTransitions.Transition.With(entry.Key, prop ?? "BackColor", color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                    catch
                    {
                        entry.Key.SetProperty(prop ?? "BackColor", color);
                    }
                }
            }


            //if ((OS.WVista || OS.W7 || OS.W8x) && Program.Settings.Miscellaneous.Win7LivePreview)
            //{
            //    if (_conditions.LivePreview_Colorization)
            //    {
            //        UpdateDWMPreview(ColorEditorManager1.Color, Program.TM.Windows7.ColorizationAfterglow);
            //    }

            //    if (_conditions.LivePreview_AfterGlow)
            //    {
            //        UpdateDWMPreview(Program.TM.Windows7.ColorizationColor, ColorEditorManager1.Color);
            //    }
            //}
        }

        #region DWM Windows 7 Live Preview
        public static void UpdateDWMPreview(Color Color1, Color Color2)
        {
            Task.Run(() =>
            {
                try
                {
                    using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                    {
                        if (DWMAPI.IsCompositionEnabled())
                        {
                            DWMAPI.DWM_COLORIZATION_PARAMS DCP = new()
                            {
                                clrColor = (uint)Color1.ToArgb(),
                                clrAfterGlow = (uint)Color2.ToArgb()
                            };

                            if (Program.WindowStyle == WindowStyle.W81)
                            {
                                DCP.nIntensity = (uint)Program.TM.Windows81.ColorizationColorBalance;
                                DWMAPI.DwmSetColorizationParameters(ref DCP, false);
                            }

                            else if (Program.WindowStyle == WindowStyle.W7)
                            {
                                DCP.nIntensity = (uint)Program.TM.Windows7.ColorizationColorBalance;

                                DCP.clrAfterGlowBalance = (uint)Program.TM.Windows7.ColorizationAfterglowBalance;
                                DCP.clrBlurBalance = (uint)Program.TM.Windows7.ColorizationBlurBalance;
                                DCP.clrGlassReflectionIntensity = (uint)Program.TM.Windows7.ColorizationGlassReflectionIntensity;
                                DCP.fOpaque = Program.TM.Windows7.Theme == Theme.Structures.Windows7.Themes.AeroOpaque;
                                DWMAPI.DwmSetColorizationParameters(ref DCP, false);
                            }
                        }
                        wic.Undo();
                    }
                }
                catch { }
            });
        }
        #endregion

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            img = RadioButton1.Checked ? Program.Wallpaper : Bitmap_Mgr.Load(TextBox1.Text);

            if (CheckBox2.Checked) img = img.Resize(300, 300);

            if (img is not null)
            {
                Label4.Text = Program.Lang.Extracting;
                Program.Animator.HideSync(Button6, true);
                Program.Animator.HideSync(ImgPaletteContainer, true);
                ProgressBar1.Visible = true;
                Colors_List.Clear();

                try
                {
                    BackgroundWorker1.CancelAsync();
                    BackgroundWorker1.RunWorkerAsync();
                }
                catch
                {
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (img is not null)
            {
                ColorThiefDotNet.ColorThief ColorThiefX = new();
                List<ColorThiefDotNet.QuantizedColor> Colors = ColorThiefX.GetPalette(img as Bitmap, trackBarX1.Value, trackBarX2.Value, CheckBox1.Checked);

                foreach (ColorThiefDotNet.QuantizedColor C in Colors)
                    Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B));

                GC.Collect();
                GC.SuppressFinalize(Colors);
                GC.SuppressFinalize(ColorThiefX);
                img.Dispose();
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            foreach (UI.Controllers.ColorItem ctrl in ImgPaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
            {
                ctrl.Click -= MiniColorItem_click;
                ctrl.Dispose();
            }
            ImgPaletteContainer.Controls.Clear();

            Colors_List = Colors_List.Distinct().ToList();
            Colors_List.Sort(new RGBColorComparer());

            foreach (Color C in Colors_List)
            {
                UI.Controllers.ColorItem MiniColorItem = new();
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
        }

        private void MiniColorItem_click(object sender, EventArgs e)
        {
            ColorEditorManager1.Color = ((UI.Controllers.ColorItem)sender).BackColor;
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Lang.Filter_OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.FileName;
                }
            
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Palettes, Title = Program.Lang.Filter_OpenPalette })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ColorGrid1.Colors = ColorCollection.LoadPalette(dlg.FileName);
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Themes, Title = Program.Lang.Filter_OpenTheme })
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
                        GetColorsFromPalette(Theme.Default.Windows11());
                        break;
                    }
                case 2:
                    {
                        GetColorsFromPalette(Theme.Default.Windows10());
                        break;
                    }
                case 3:
                    {
                        GetColorsFromPalette(Theme.Default.Windows81());
                        break;
                    }
                case 4:
                    {
                        GetColorsFromPalette(Theme.Default.WindowsVista());
                        break;
                    }
                case 5:
                    {
                        GetColorsFromPalette(Theme.Default.WindowsXP());
                        break;
                    }
                case 6:
                    {
                        GetColorsFromPalette(Theme.Default.Windows7());
                        break;
                    }

                default:
                    {
                        GetColorsFromPalette(Program.TM);
                        break;
                    }
            }
        }

        private void ColorPickerDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (DialogResult != DialogResult.OK & (OS.WVista | OS.W7 | OS.W8x) & Program.Settings.Miscellaneous.Win7LivePreview)
            //{
            //    if (_conditions.LivePreview_Colorization)
            //    {
            //        UpdateDWMPreview(InitColor, Program.TM.Windows7.ColorizationAfterglow);
            //    }

            //    if (_conditions.LivePreview_AfterGlow)
            //    {
            //        UpdateDWMPreview(Program.TM.Windows7.ColorizationColor, InitColor);
            //    }
            //}
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem is null)
                return;


            foreach (UI.Controllers.ColorItem c in ThemePaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                ThemePaletteContainer.Controls.Remove(c);
            }

            ThemePaletteContainer.Controls.Clear();

            try
            {
                if (!string.IsNullOrWhiteSpace(ComboBox1.SelectedItem.ToString()))
                {
                    foreach (Color C in Theme.Manager.GetPaletteFromString(Properties.Resources.RetroThemesDB, ComboBox1.SelectedItem.ToString()))
                    {
                        UI.Controllers.ColorItem MiniColorItem = new();
                        MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                        MiniColorItem.AllowDrop = false;
                        MiniColorItem.PauseColorsHistory = true;
                        MiniColorItem.BackColor = Color.FromArgb(255, C);
                        MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                        ThemePaletteContainer.Controls.Add(MiniColorItem);
                        MiniColorItem.Click += MiniColorItem_click;
                    }
                }
            }
            catch
            {

            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(TextBox2.Text))
            {

                foreach (UI.Controllers.ColorItem c in ThemePaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
                {
                    c.Click -= MiniColorItem_click;
                    c.Dispose();
                    ThemePaletteContainer.Controls.Remove(c);
                }

                ThemePaletteContainer.Controls.Clear();

                if (System.IO.Path.GetExtension(TextBox2.Text).ToLower() == ".theme")
                {
                    try
                    {
                        foreach (Color C in Theme.Manager.GetPaletteFromMSTheme(TextBox2.Text))
                        {
                            UI.Controllers.ColorItem MiniColorItem = new();
                            MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                            MiniColorItem.AllowDrop = false;
                            MiniColorItem.PauseColorsHistory = true;
                            MiniColorItem.BackColor = Color.FromArgb(255, C);
                            MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                            ThemePaletteContainer.Controls.Add(MiniColorItem);
                            MiniColorItem.Click += MiniColorItem_click;
                        }
                    }
                    catch
                    {
                        MsgBox(Program.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else if (System.IO.Path.GetExtension(TextBox2.Text).ToLower() == ".msstyles")
                {
                    try
                    {
                        System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={TextBox2.Text}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");

                        VisualStyleFile vs = new($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme");

                        foreach (FieldInfo field in typeof(VisualStyleMetricColors).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                        {
                            if (field.FieldType.Name.ToLower() == "color")
                            {

                                UI.Controllers.ColorItem MiniColorItem = new();
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
                    catch
                    {
                        MsgBox(Program.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MsgBox(Program.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}