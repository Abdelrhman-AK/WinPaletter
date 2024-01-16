using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class CursorsStudio
    {
        private CursorControl _SelectedControl;
        private CursorControl _CopiedControl;
        private readonly List<CursorControl> AnimateList = new();

        private void CursorsStudio_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-Windows-cursors");
        }


        public CursorsStudio()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Theme.Manager TMx = new(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                LoadFromTM(TMx);
                TMx.Dispose();
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W11)) { LoadFromTM(TMx); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W10)) { LoadFromTM(TMx); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W81)) { LoadFromTM(TMx); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W7)) { LoadFromTM(TMx); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WVista)) { LoadFromTM(TMx); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WXP)) { LoadFromTM(TMx); }
        }

        private void QuickApply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                TMx.Apply_Cursors();
            }

            Cursor = Cursors.Default;
        }

        private void ModeSwitched(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = AdvancedMode ? 0 : 1;
        }

        public void CursorTM_to_Cursor(CursorControl CursorControl, Theme.Structures.Cursor Cursor)
        {
            CursorControl.Prop_UseFromFile = Cursor.UseFromFile;
            CursorControl.Prop_File = Cursor.File;
            CursorControl.Prop_ArrowStyle = Cursor.ArrowStyle;
            CursorControl.Prop_CircleStyle = Cursor.CircleStyle;
            CursorControl.Prop_PrimaryColor1 = Cursor.PrimaryColor1;
            CursorControl.Prop_PrimaryColor2 = Cursor.PrimaryColor2;
            CursorControl.Prop_PrimaryColorGradient = Cursor.PrimaryColorGradient;
            CursorControl.Prop_PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode;
            CursorControl.Prop_PrimaryNoise = Cursor.PrimaryColorNoise;
            CursorControl.Prop_PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity;
            CursorControl.Prop_SecondaryColor1 = Cursor.SecondaryColor1;
            CursorControl.Prop_SecondaryColor2 = Cursor.SecondaryColor2;
            CursorControl.Prop_SecondaryColorGradient = Cursor.SecondaryColorGradient;
            CursorControl.Prop_SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode;
            CursorControl.Prop_SecondaryNoise = Cursor.SecondaryColorNoise;
            CursorControl.Prop_SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity;
            CursorControl.Prop_LoadingCircleBack1 = Cursor.LoadingCircleBack1;
            CursorControl.Prop_LoadingCircleBack2 = Cursor.LoadingCircleBack2;
            CursorControl.Prop_LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient;
            CursorControl.Prop_LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode;
            CursorControl.Prop_LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity;
            CursorControl.Prop_LoadingCircleHot1 = Cursor.LoadingCircleHot1;
            CursorControl.Prop_LoadingCircleHot2 = Cursor.LoadingCircleHot2;
            CursorControl.Prop_LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient;
            CursorControl.Prop_LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode;
            CursorControl.Prop_LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity;
            CursorControl.Prop_Shadow_Enabled = Cursor.Shadow_Enabled;
            CursorControl.Prop_Shadow_Color = Cursor.Shadow_Color;
            CursorControl.Prop_Shadow_Blur = Cursor.Shadow_Blur;
            CursorControl.Prop_Shadow_Opacity = Cursor.Shadow_Opacity;
            CursorControl.Prop_Shadow_OffsetX = Cursor.Shadow_OffsetX;
            CursorControl.Prop_Shadow_OffsetY = Cursor.Shadow_OffsetY;
        }

        public Theme.Structures.Cursor Cursor_to_CursorTM(CursorControl CursorControl)
        {
            Theme.Structures.Cursor Cursor;
            Cursor.UseFromFile = CursorControl.Prop_UseFromFile;
            Cursor.File = CursorControl.Prop_File;
            Cursor.ArrowStyle = CursorControl.Prop_ArrowStyle;
            Cursor.CircleStyle = CursorControl.Prop_CircleStyle;
            Cursor.PrimaryColor1 = CursorControl.Prop_PrimaryColor1;
            Cursor.PrimaryColor2 = CursorControl.Prop_PrimaryColor2;
            Cursor.PrimaryColorGradient = CursorControl.Prop_PrimaryColorGradient;
            Cursor.PrimaryColorGradientMode = CursorControl.Prop_PrimaryColorGradientMode;
            Cursor.PrimaryColorNoise = CursorControl.Prop_PrimaryNoise;
            Cursor.PrimaryColorNoiseOpacity = CursorControl.Prop_PrimaryNoiseOpacity;
            Cursor.SecondaryColor1 = CursorControl.Prop_SecondaryColor1;
            Cursor.SecondaryColor2 = CursorControl.Prop_SecondaryColor2;
            Cursor.SecondaryColorGradient = CursorControl.Prop_SecondaryColorGradient;
            Cursor.SecondaryColorGradientMode = CursorControl.Prop_SecondaryColorGradientMode;
            Cursor.SecondaryColorNoise = CursorControl.Prop_SecondaryNoise;
            Cursor.SecondaryColorNoiseOpacity = CursorControl.Prop_SecondaryNoiseOpacity;
            Cursor.LoadingCircleBack1 = CursorControl.Prop_LoadingCircleBack1;
            Cursor.LoadingCircleBack2 = CursorControl.Prop_LoadingCircleBack2;
            Cursor.LoadingCircleBackGradient = CursorControl.Prop_LoadingCircleBackGradient;
            Cursor.LoadingCircleBackGradientMode = CursorControl.Prop_LoadingCircleBackGradientMode;
            Cursor.LoadingCircleBackNoise = CursorControl.Prop_LoadingCircleBackNoise;
            Cursor.LoadingCircleBackNoiseOpacity = CursorControl.Prop_LoadingCircleBackNoiseOpacity;
            Cursor.LoadingCircleHot1 = CursorControl.Prop_LoadingCircleHot1;
            Cursor.LoadingCircleHot2 = CursorControl.Prop_LoadingCircleHot2;
            Cursor.LoadingCircleHotGradient = CursorControl.Prop_LoadingCircleHotGradient;
            Cursor.LoadingCircleHotGradientMode = CursorControl.Prop_LoadingCircleHotGradientMode;
            Cursor.LoadingCircleHotNoise = CursorControl.Prop_LoadingCircleHotNoise;
            Cursor.LoadingCircleHotNoiseOpacity = CursorControl.Prop_LoadingCircleHotNoiseOpacity;
            Cursor.Shadow_Enabled = CursorControl.Prop_Shadow_Enabled;
            Cursor.Shadow_Color = CursorControl.Prop_Shadow_Color;
            Cursor.Shadow_Blur = CursorControl.Prop_Shadow_Blur;
            Cursor.Shadow_Opacity = CursorControl.Prop_Shadow_Opacity;
            Cursor.Shadow_OffsetX = CursorControl.Prop_Shadow_OffsetX;
            Cursor.Shadow_OffsetY = CursorControl.Prop_Shadow_OffsetY;

            return Cursor;
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            AspectEnabled = TM.Cursor_Enabled;
            CheckBox9.Checked = TM.Cursor_Shadow;
            trackBarX9.Value = TM.Cursor_Trails;
            CheckBox10.Checked = TM.Cursor_Sonar;
            CursorTM_to_Cursor(Arrow, TM.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursor_Cross);

            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                i.Invalidate();
            }
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.Cursor_Enabled = AspectEnabled;
            TM.Cursor_Shadow = CheckBox9.Checked;
            TM.Cursor_Trails = trackBarX9.Value;
            TM.Cursor_Sonar = CheckBox10.Checked;
            TM.Cursor_Arrow = Cursor_to_CursorTM(Arrow);
            TM.Cursor_Help = Cursor_to_CursorTM(Help);
            TM.Cursor_AppLoading = Cursor_to_CursorTM(AppLoading);
            TM.Cursor_Busy = Cursor_to_CursorTM(Busy);
            TM.Cursor_Move = Cursor_to_CursorTM(Move_Cur);
            TM.Cursor_NS = Cursor_to_CursorTM(NS);
            TM.Cursor_EW = Cursor_to_CursorTM(EW);
            TM.Cursor_NESW = Cursor_to_CursorTM(NESW);
            TM.Cursor_NWSE = Cursor_to_CursorTM(NWSE);
            TM.Cursor_Up = Cursor_to_CursorTM(Up);
            TM.Cursor_Pen = Cursor_to_CursorTM(Pen);
            TM.Cursor_None = Cursor_to_CursorTM(None);
            TM.Cursor_Link = Cursor_to_CursorTM(Link);
            TM.Cursor_Pin = Cursor_to_CursorTM(Pin);
            TM.Cursor_Person = Cursor_to_CursorTM(Person);
            TM.Cursor_IBeam = Cursor_to_CursorTM(IBeam);
            TM.Cursor_Cross = Cursor_to_CursorTM(Cross);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Store_Toggle_Cursors,
                Enabled = Program.TM.Cursor_Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = true,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnQuickApply = QuickApply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromScheme_11 = ImportWin11Preset,
                OnImportFromScheme_10 = ImportWin10Preset,
                OnImportFromScheme_81 = ImportWin81Preset,
                OnImportFromScheme_7 = ImportWin7Preset,
                OnImportFromScheme_Vista = ImportWinVistaPreset,
                OnImportFromScheme_XP = ImportWinXPPreset,

                OnModeAdvanced = ModeSwitched,
                OnModeSimple = ModeSwitched,
            };

            LoadData(data);

            cursorsConatiner.DoubleBuffer();

            _CopiedControl = null;

            Angle = 180f;
            Cycles = 0;
            Timer1.Enabled = false;
            Timer1.Stop();

            LoadFromTM(Program.TM);

            // Remove handler to avoid doubling/tripling events
            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                try { i.Click -= Clicked; } catch { }
            }

            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                i.Click += Clicked;
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(UI.Controllers.ColorItem).FullName) is UI.Controllers.ColorItem)
            {
                Focus();
                BringToFront();
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        public void Clicked(object sender, EventArgs e)
        {
            _SelectedControl = (CursorControl)sender;
            ApplyColorsFromCursor(_SelectedControl);
            Button1.Enabled = true;
            if (!tabControl1.Visible) Program.Animator.ShowSync(tabControl1);
        }

        public void ApplyColorsFromCursor(CursorControl CursorControl)
        {
            source1.Checked = CursorControl.Prop_UseFromFile;
            source0.Checked = !CursorControl.Prop_UseFromFile;
            textBox1.Text = CursorControl.Prop_File;

            ComboBox5.SelectedIndex = (int)CursorControl.Prop_ArrowStyle;
            ComboBox6.SelectedIndex = (int)CursorControl.Prop_CircleStyle;

            PrimaryColor1.BackColor = CursorControl.Prop_PrimaryColor1;
            PrimaryColor2.BackColor = CursorControl.Prop_PrimaryColor2;
            CheckBox1.Checked = CursorControl.Prop_PrimaryColorGradient;
            ComboBox1.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_PrimaryColorGradientMode);
            CheckBox5.Checked = CursorControl.Prop_PrimaryNoise;
            trackBarX1.Value = (int)Math.Round(CursorControl.Prop_PrimaryNoiseOpacity * 100f);

            SecondaryColor1.BackColor = CursorControl.Prop_SecondaryColor1;
            SecondaryColor2.BackColor = CursorControl.Prop_SecondaryColor2;
            CheckBox4.Checked = CursorControl.Prop_SecondaryColorGradient;
            ComboBox2.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_SecondaryColorGradientMode);
            CheckBox3.Checked = CursorControl.Prop_SecondaryNoise;
            trackBarX2.Value = (int)Math.Round(CursorControl.Prop_SecondaryNoiseOpacity * 100f);

            CircleColor1.BackColor = CursorControl.Prop_LoadingCircleBack1;
            CircleColor2.BackColor = CursorControl.Prop_LoadingCircleBack2;
            CheckBox8.Checked = CursorControl.Prop_LoadingCircleBackGradient;
            ComboBox4.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_LoadingCircleBackGradientMode);
            CheckBox7.Checked = CursorControl.Prop_LoadingCircleBackNoise;
            trackBarX3.Value = (int)Math.Round(CursorControl.Prop_LoadingCircleBackNoiseOpacity * 100f);

            LoadingColor1.BackColor = CursorControl.Prop_LoadingCircleHot1;
            LoadingColor2.BackColor = CursorControl.Prop_LoadingCircleHot2;
            CheckBox2.Checked = CursorControl.Prop_LoadingCircleHotGradient;
            ComboBox3.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_LoadingCircleHotGradientMode);
            CheckBox6.Checked = CursorControl.Prop_LoadingCircleHotNoise;
            trackBarX4.Value = (int)Math.Round(CursorControl.Prop_LoadingCircleHotNoiseOpacity * 100f);

            CheckBox11.Checked = CursorControl.Prop_Shadow_Enabled;
            ColorItem1.BackColor = CursorControl.Prop_Shadow_Color;
            trackBarX5.Value = CursorControl.Prop_Shadow_Blur;
            trackBarX6.Value = (int)Math.Round(CursorControl.Prop_Shadow_Opacity * 100f);
            trackBarX7.Value = CursorControl.Prop_Shadow_OffsetX;
            trackBarX8.Value = CursorControl.Prop_Shadow_OffsetY;
        }

        public void ApplyColorsToPreview(CursorControl CursorControl)
        {
            CursorControl.Prop_UseFromFile = source1.Checked;
            CursorControl.Prop_File = textBox1.Text;

            CursorControl.Prop_ArrowStyle = (Paths.ArrowStyle)ComboBox5.SelectedIndex;
            CursorControl.Prop_CircleStyle = (Paths.CircleStyle)ComboBox6.SelectedIndex;

            CursorControl.Prop_PrimaryColor1 = PrimaryColor1.BackColor;
            CursorControl.Prop_PrimaryColor2 = PrimaryColor2.BackColor;
            CursorControl.Prop_PrimaryColorGradient = CheckBox1.Checked;
            CursorControl.Prop_PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(ComboBox1.SelectedItem.ToString());
            CursorControl.Prop_PrimaryNoise = CheckBox5.Checked;
            CursorControl.Prop_PrimaryNoiseOpacity = (float)(trackBarX1.Value / 100d);

            CursorControl.Prop_SecondaryColor1 = SecondaryColor1.BackColor;
            CursorControl.Prop_SecondaryColor2 = SecondaryColor2.BackColor;
            CursorControl.Prop_SecondaryColorGradient = CheckBox4.Checked;
            CursorControl.Prop_SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(ComboBox2.SelectedItem.ToString());
            CursorControl.Prop_SecondaryNoise = CheckBox3.Checked;
            CursorControl.Prop_SecondaryNoiseOpacity = (float)(trackBarX2.Value / 100d);

            CursorControl.Prop_LoadingCircleBack1 = CircleColor1.BackColor;
            CursorControl.Prop_LoadingCircleBack2 = CircleColor2.BackColor;
            CursorControl.Prop_LoadingCircleBackGradient = CheckBox8.Checked;
            CursorControl.Prop_LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(ComboBox4.SelectedItem.ToString());
            CursorControl.Prop_LoadingCircleBackNoise = CheckBox7.Checked;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = (float)(trackBarX3.Value / 100d);

            CursorControl.Prop_LoadingCircleHot1 = LoadingColor1.BackColor;
            CursorControl.Prop_LoadingCircleHot2 = LoadingColor2.BackColor;
            CursorControl.Prop_LoadingCircleHotGradient = CheckBox2.Checked;
            CursorControl.Prop_LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(ComboBox3.SelectedItem.ToString());
            CursorControl.Prop_LoadingCircleHotNoise = CheckBox6.Checked;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = (float)(trackBarX4.Value / 100d);

            CursorControl.Prop_Shadow_Enabled = CheckBox11.Checked;
            CursorControl.Prop_Shadow_Color = ColorItem1.BackColor;
            CursorControl.Prop_Shadow_Blur = trackBarX5.Value;
            CursorControl.Prop_Shadow_Opacity = (float)(trackBarX6.Value / 100d);
            CursorControl.Prop_Shadow_OffsetX = trackBarX7.Value;
            CursorControl.Prop_Shadow_OffsetY = trackBarX8.Value;

        }

        private void TaskbarFrontAndFoldersOnStart_picker_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_PrimaryColor1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_PrimaryColor1 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_PrimaryColor1) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_PrimaryColor1 = C;
            _SelectedControl.Invalidate();

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void GroupBox3_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_PrimaryColor2 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_PrimaryColor2 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_PrimaryColor2) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_PrimaryColor2 = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();

        }

        private void GroupBox5_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_SecondaryColor1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_SecondaryColor1 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_SecondaryColor1) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_SecondaryColor1 = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();

        }

        private void GroupBox4_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_SecondaryColor2 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_SecondaryColor2 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_SecondaryColor2) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_SecondaryColor2 = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();

        }

        private void GroupBox10_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleBack1) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleBack1 = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_PrimaryColorGradient = CheckBox1.Checked;
            _SelectedControl.Invalidate();
            CheckBox1.Invalidate();
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_SecondaryColorGradient = CheckBox4.Checked;
            _SelectedControl.Invalidate();
            CheckBox4.Invalidate();

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();

        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_PrimaryNoise = CheckBox5.Checked;
            _SelectedControl.Invalidate();
            CheckBox5.Invalidate();

        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_SecondaryNoise = CheckBox3.Checked;
            _SelectedControl.Invalidate();
            CheckBox3.Invalidate();

        }

        private void Trackbar1_Scroll(object sender)
        {
            if (!IsShown) return;

            foreach (CursorControl i in cursorsConatiner.Controls)
            {
                i.Prop_Scale = ((float)((UI.WP.TrackBar)sender).Value) / 100;
                i.Width = (int)Math.Round(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }

            Label5.Text = $"{Program.Lang.Scaling} ({((float)((UI.WP.TrackBar)sender).Value) / 100}x)";
        }

        private float Angle = 180f;
        private readonly float Increment = 5f;
        private int Cycles = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!IsShown) return;

            try
            {
                foreach (CursorControl i in AnimateList)
                {
                    i.Angle = Angle;
                    i.Refresh();

                    if (Angle + Increment >= 360f)
                    {
                        Angle = 0f;
                    }

                    Angle += Increment;

                    if (Cycles >= 3)
                    {
                        i.Angle = 180f;
                        i.Refresh();

                        Timer1.Enabled = false;
                        Timer1.Stop();

                        Cycles = 0;
                    }
                    else
                    {
                        if (Angle == 180f) { Cycles += 1; }
                    }
                }
            }
            catch { }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            AnimateList.Clear();

            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                bool condition0 = !i.Prop_UseFromFile && (i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy);
                bool condition1 = i.Prop_UseFromFile && System.IO.File.Exists(i.Prop_File) && System.IO.Path.GetExtension(i.Prop_File).ToUpper() == ".ANI";
                if (condition0 || condition1) { AnimateList.Add(i); }
            }

            foreach (CursorControl i in panel2.Controls.OfType<CursorControl>())
            {
                bool condition0 = !i.Prop_UseFromFile && (i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy);
                bool condition1 = i.Prop_UseFromFile && System.IO.File.Exists(i.Prop_File) && System.IO.Path.GetExtension(i.Prop_File).ToUpper() == ".ANI";
                if (condition0 || condition1) { AnimateList.Add(i); }
            }

            Angle = 180f;
            Cycles = 0;
            Timer1.Enabled = true;
            Timer1.Start();
        }

        private void GroupBox9_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleBack2) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleBack2 = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();

        }

        private void GroupBox8_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleHot1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleHot1 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleHot1) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleHot1 = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();

        }

        private void GroupBox7_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleHot2 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleHot2 = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleHot2) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleHot2 = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();

        }

        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_LoadingCircleBackGradient = CheckBox8.Checked;
            _SelectedControl.Invalidate();
            CheckBox8.Invalidate();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_LoadingCircleHotGradient = CheckBox2.Checked;
            _SelectedControl.Invalidate();
            CheckBox2.Invalidate();
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_LoadingCircleBackNoise = CheckBox7.Checked;
            _SelectedControl.Invalidate();
            CheckBox7.Invalidate();

        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_LoadingCircleHotNoise = CheckBox6.Checked;
            _SelectedControl.Invalidate();
            CheckBox6.Invalidate();

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _CopiedControl = _SelectedControl;
            Button2.Enabled = true;
            Button6.Enabled = true;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ApplyColorsFromCursor(_CopiedControl);
            ApplyColorsToPreview(_SelectedControl);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            foreach (CursorControl i in cursorsConatiner.Controls)
            {
                if (i is CursorControl)
                {
                    ApplyColorsFromCursor(_CopiedControl);
                    ApplyColorsToPreview(i);
                    i.Invalidate();
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Program.ToolTip.ToolTipText = Program.Lang.ScalingTip;
            Program.ToolTip.ToolTipTitle = Program.Lang.Tip;
            Program.ToolTip.Image = Assets.Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 7000);
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_ArrowStyle = (Paths.ArrowStyle)ComboBox5.SelectedIndex;
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_CircleStyle = (Paths.CircleStyle)ComboBox6.SelectedIndex;
        }

        private void ColorItem1_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_Shadow_Color = ((ColorItem)sender).BackColor;
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_Shadow_Color = ((ColorItem)sender).BackColor;

                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_Shadow_Color) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_Shadow_Color = C;
            _SelectedControl.Invalidate();
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();

        }

        private void CheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _SelectedControl.Prop_Shadow_Enabled = CheckBox11.Checked;
            _SelectedControl.Invalidate();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog2.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _SelectedControl.Prop_File = textBox1.Text;
            _SelectedControl.Invalidate();
        }

        private void source0_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedControl.Prop_UseFromFile = !source0.Checked;
            _SelectedControl.Invalidate();

            if (source0.Checked)
            {
                ComboBox5.Enabled = true; ComboBox6.Enabled = true;
                tabControl1.TabPages[1].GetAllControls().ToList().ForEach(x => x.Enabled = true);
                tabControl1.TabPages[2].GetAllControls().ToList().ForEach(x => x.Enabled = true);
                tabControl1.TabPages[3].GetAllControls().ToList().ForEach(x => x.Enabled = false);
            }
        }

        private void source1_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedControl.Prop_UseFromFile = source1.Checked;
            _SelectedControl.Invalidate();

            if (source1.Checked)
            {
                ComboBox5.Enabled = false; ComboBox6.Enabled = false;
                tabControl1.TabPages[1].GetAllControls().ToList().ForEach(x => x.Enabled = false);
                tabControl1.TabPages[2].GetAllControls().ToList().ForEach(x => x.Enabled = false);
                tabControl1.TabPages[3].GetAllControls().ToList().ForEach(x => x.Enabled = true);
            }
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_PrimaryNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_SecondaryNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void trackBarX3_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_LoadingCircleBackNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void trackBarX4_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_LoadingCircleHotNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void trackBarX5_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 10f)
            {
                valX = 10f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_Blur = (int)Math.Round(valX);
            _SelectedControl.Invalidate();
        }

        private void trackBarX6_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_Opacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void trackBarX7_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 5f)
            {
                valX = 5f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_OffsetX = (int)Math.Round(valX);
            _SelectedControl.Invalidate();
        }

        private void trackBarX8_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            float valX = Conversions.ToSingle(((UI.Controllers.TrackBarX)sender).Value);
            if (valX > 5f)
            {
                valX = 5f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_OffsetY = (int)Math.Round(valX);
            _SelectedControl.Invalidate();
        }

        private void cursorControl1_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                cursorControl.Prop_ArrowStyle = Paths.ArrowStyle.Aero;
                cursorControl.Prop_PrimaryNoise = false;
                cursorControl.Prop_SecondaryNoise = false;
            }
        }

        private void cursorControl2_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                cursorControl.Prop_ArrowStyle = Paths.ArrowStyle.Modern;
                cursorControl.Prop_PrimaryNoise = false;
                cursorControl.Prop_SecondaryNoise = false;
            }
        }

        private void cursorControl3_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                cursorControl.Prop_ArrowStyle = Paths.ArrowStyle.Classic;
                cursorControl.Prop_PrimaryNoise = false;
                cursorControl.Prop_SecondaryNoise = false;
            }
        }

        private void cursorControl4_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                cursorControl.Prop_CircleStyle = Paths.CircleStyle.Aero;
                cursorControl.Prop_LoadingCircleBackNoise = false;
                cursorControl.Prop_LoadingCircleHotNoise = false;
            }
        }

        private void cursorControl5_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                cursorControl.Prop_CircleStyle = Paths.CircleStyle.Modern;
                cursorControl.Prop_LoadingCircleBackNoise = false;
                cursorControl.Prop_LoadingCircleHotNoise = false;
            }
        }

        private void cursorControl6_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                cursorControl.Prop_CircleStyle = Paths.CircleStyle.Fluid;
                cursorControl.Prop_LoadingCircleBackNoise = false;
                cursorControl.Prop_LoadingCircleHotNoise = false;
            }
        }

        private void cursorControl7_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                cursorControl.Prop_CircleStyle = Paths.CircleStyle.Classic;
                cursorControl.Prop_LoadingCircleBackNoise = false;
                cursorControl.Prop_LoadingCircleHotNoise = false;
            }
        }

        private void colorItem2_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            if (e is DragEventArgs)
            {
                foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                    Color border = back.IsDark() ? back.LightLight() : back.Dark();

                    cursorControl.Prop_PrimaryColor1 = back;
                    cursorControl.Prop_PrimaryColor2 = back;
                    cursorControl.Prop_SecondaryColor1 = border;
                    cursorControl.Prop_SecondaryColor2 = border;
                }

                foreach (CursorControl cursorControl in panel1.Controls.OfType<CursorControl>())
                {
                    Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                    Color border = back.IsDark() ? back.LightLight() : back.Dark();

                    cursorControl.Prop_PrimaryColor1 = back;
                    cursorControl.Prop_PrimaryColor2 = back;
                    cursorControl.Prop_SecondaryColor1 = border;
                    cursorControl.Prop_SecondaryColor2 = border;
                }

                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
                    {
                        Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                        Color border = back.IsDark() ? back.LightLight() : back.Dark();

                        cursorControl.Prop_PrimaryColor1 = back;
                        cursorControl.Prop_PrimaryColor2 = back;
                        cursorControl.Prop_SecondaryColor1 = border;
                        cursorControl.Prop_SecondaryColor2 = border;
                    }

                    foreach (CursorControl cursorControl in panel1.Controls.OfType<CursorControl>())
                    {
                        Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                        Color border = back.IsDark() ? back.LightLight() : back.Dark();

                        cursorControl.Prop_PrimaryColor1 = back;
                        cursorControl.Prop_PrimaryColor2 = back;
                        cursorControl.Prop_SecondaryColor1 = border;
                        cursorControl.Prop_SecondaryColor2 = border;
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
            };

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                CList.Add(cursorControl, new string[] { nameof(cursorControl.Prop_PrimaryColor1), nameof(cursorControl.Prop_PrimaryColor2) });
            }

            foreach (CursorControl cursorControl in panel1.Controls.OfType<CursorControl>())
            {
                CList.Add(cursorControl, new string[] { nameof(cursorControl.Prop_PrimaryColor1), nameof(cursorControl.Prop_PrimaryColor2) });
            }

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                Color border = C.IsDark() ? C.LightLight() : C.Dark();

                cursorControl.Prop_PrimaryColor1 = C;
                cursorControl.Prop_PrimaryColor2 = C;
                cursorControl.Prop_SecondaryColor1 = border;
                cursorControl.Prop_SecondaryColor2 = border;
            }

            foreach (CursorControl cursorControl in panel1.Controls.OfType<CursorControl>())
            {
                Color border = C.IsDark() ? C.LightLight() : C.Dark();

                cursorControl.Prop_PrimaryColor1 = C;
                cursorControl.Prop_PrimaryColor2 = C;
                cursorControl.Prop_SecondaryColor1 = border;
                cursorControl.Prop_SecondaryColor2 = border;
            }

            CList.Clear();
        }

        private void easy_busy_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            if (_SelectedControl != null) _SelectedControl.Focused = false;
            _SelectedControl = null;

            if (e is DragEventArgs)
            {
                foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                    Color border = back.IsDark() ? back.LightLight() : back.Dark();

                    cursorControl.Prop_LoadingCircleBack1 = back;
                    cursorControl.Prop_LoadingCircleBack2 = back;
                    cursorControl.Prop_LoadingCircleHot1 = border;
                    cursorControl.Prop_LoadingCircleHot2 = border;
                }

                foreach (CursorControl cursorControl in panel2.Controls.OfType<CursorControl>())
                {
                    Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                    Color border = back.IsDark() ? back.LightLight() : back.Dark();

                    cursorControl.Prop_LoadingCircleBack1 = back;
                    cursorControl.Prop_LoadingCircleBack2 = back;
                    cursorControl.Prop_LoadingCircleHot1 = border;
                    cursorControl.Prop_LoadingCircleHot2 = border;
                }

                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
                    {
                        Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                        Color border = back.IsDark() ? back.LightLight() : back.Dark();

                        cursorControl.Prop_LoadingCircleBack1 = back;
                        cursorControl.Prop_LoadingCircleBack2 = back;
                        cursorControl.Prop_LoadingCircleHot1 = border;
                        cursorControl.Prop_LoadingCircleHot2 = border;
                    }

                    foreach (CursorControl cursorControl in panel2.Controls.OfType<CursorControl>())
                    {
                        Color back = ((UI.Controllers.ColorItem)sender).BackColor;
                        Color border = back.IsDark() ? back.LightLight() : back.Dark();

                        cursorControl.Prop_LoadingCircleBack1 = back;
                        cursorControl.Prop_LoadingCircleBack2 = back;
                        cursorControl.Prop_LoadingCircleHot1 = border;
                        cursorControl.Prop_LoadingCircleHot2 = border;
                    }
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
            };

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                CList.Add(cursorControl, new string[] { nameof(cursorControl.Prop_LoadingCircleBack1), nameof(cursorControl.Prop_LoadingCircleBack2) });
            }

            foreach (CursorControl cursorControl in panel2.Controls.OfType<CursorControl>())
            {
                CList.Add(cursorControl, new string[] { nameof(cursorControl.Prop_LoadingCircleBack1), nameof(cursorControl.Prop_LoadingCircleBack2) });
            }

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            foreach (CursorControl cursorControl in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                Color border = C.IsDark() ? C.LightLight() : C.Dark();

                cursorControl.Prop_LoadingCircleBack1 = C;
                cursorControl.Prop_LoadingCircleBack2 = C;
                cursorControl.Prop_LoadingCircleHot1 = border;
                cursorControl.Prop_LoadingCircleHot2 = border;
            }

            foreach (CursorControl cursorControl in panel2.Controls.OfType<CursorControl>())
            {
                Color border = C.IsDark() ? C.LightLight() : C.Dark();

                cursorControl.Prop_LoadingCircleBack1 = C;
                cursorControl.Prop_LoadingCircleBack2 = C;
                cursorControl.Prop_LoadingCircleHot1 = border;
                cursorControl.Prop_LoadingCircleHot2 = border;
            }

            CList.Clear();
        }
    }
}