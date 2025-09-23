using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class CursorsStudio
    {
        private CursorControl _SelectedControl;
        private CursorControl _CopiedControl;
        private readonly List<CursorControl> AnimateList = [];

        private void CursorsStudio_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.Cursors);
        }


        public CursorsStudio()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Manager TMx = new(Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Manager TMx = Default.Get(Program.WindowStyle);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void ImportWin12Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W12)) { LoadFromTM(TMx); }
        }

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W11)) { LoadFromTM(TMx); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W10)) { LoadFromTM(TMx); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W81)) { LoadFromTM(TMx); }
        }

        private void ImportWin8Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W8)) { LoadFromTM(TMx); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W7)) { LoadFromTM(TMx); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.WVista)) { LoadFromTM(TMx); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.WXP)) { LoadFromTM(TMx); }
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Cursors)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Manager TMx = new(Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Source.File, filename);
                }

                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
                TMx.Cursors.Apply();
            }

            Cursor = Cursors.Default;
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
            CursorControl.Prop_LoadingCircleHot_AnimationSpeed = Cursor.LoadingCircleHot_AnimationSpeed;
            CursorControl.Prop_Shadow_Enabled = Cursor.Shadow_Enabled;
            CursorControl.Prop_Shadow_Color = Cursor.Shadow_Color;
            CursorControl.Prop_Shadow_Blur = Cursor.Shadow_Blur;
            CursorControl.Prop_Shadow_Opacity = Cursor.Shadow_Opacity;
            CursorControl.Prop_Shadow_OffsetX = Cursor.Shadow_OffsetX;
            CursorControl.Prop_Shadow_OffsetY = Cursor.Shadow_OffsetY;
            CursorControl.Prop_BorderThickness = Cursor.BorderThickness;
        }

        public Theme.Structures.Cursor Cursor_to_CursorTM(CursorControl CursorControl)
        {
            Theme.Structures.Cursor Cursor = new();
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
            Cursor.LoadingCircleHot_AnimationSpeed = CursorControl.Prop_LoadingCircleHot_AnimationSpeed;
            Cursor.Shadow_Enabled = CursorControl.Prop_Shadow_Enabled;
            Cursor.Shadow_Color = CursorControl.Prop_Shadow_Color;
            Cursor.Shadow_Blur = CursorControl.Prop_Shadow_Blur;
            Cursor.Shadow_Opacity = CursorControl.Prop_Shadow_Opacity;
            Cursor.Shadow_OffsetX = CursorControl.Prop_Shadow_OffsetX;
            Cursor.Shadow_OffsetY = CursorControl.Prop_Shadow_OffsetY;
            Cursor.BorderThickness = CursorControl.Prop_BorderThickness;

            return Cursor;
        }

        void setCursorsSizes()
        {
            foreach (CursorControl i in cursorsConatiner.Controls)
            {
                i.Prop_Scale = trackBarX10.Value / 32f;
                i.Width = (int)Math.Round(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.Cursors.Enabled;
            CheckBox9.Checked = TM.Cursors.Shadow;
            trackBarX9.Value = TM.Cursors.Trails;
            CheckBox10.Checked = TM.Cursors.Sonar;
            trackBarX10.Value = TM.Cursors.Size;
            setCursorsSizes();

            CursorTM_to_Cursor(Arrow, TM.Cursors.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursors.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursors.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursors.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursors.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursors.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursors.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursors.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursors.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursors.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursors.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursors.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursors.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursors.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursors.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursors.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursors.Cursor_Cross);

            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                i.Invalidate();
            }

            if (_SelectedControl is not null) Clicked(_SelectedControl, null);
        }

        public void ApplyToTM(Manager TM)
        {
            TM.Cursors.Enabled = AspectEnabled;
            TM.Cursors.Shadow = CheckBox9.Checked;
            TM.Cursors.Trails = trackBarX9.Value;
            TM.Cursors.Sonar = CheckBox10.Checked;
            TM.Cursors.Size = trackBarX10.Value;
            TM.Cursors.Cursor_Arrow = Cursor_to_CursorTM(Arrow);
            TM.Cursors.Cursor_Help = Cursor_to_CursorTM(Help);
            TM.Cursors.Cursor_AppLoading = Cursor_to_CursorTM(AppLoading);
            TM.Cursors.Cursor_Busy = Cursor_to_CursorTM(Busy);
            TM.Cursors.Cursor_Move = Cursor_to_CursorTM(Move_Cur);
            TM.Cursors.Cursor_NS = Cursor_to_CursorTM(NS);
            TM.Cursors.Cursor_EW = Cursor_to_CursorTM(EW);
            TM.Cursors.Cursor_NESW = Cursor_to_CursorTM(NESW);
            TM.Cursors.Cursor_NWSE = Cursor_to_CursorTM(NWSE);
            TM.Cursors.Cursor_Up = Cursor_to_CursorTM(Up);
            TM.Cursors.Cursor_Pen = Cursor_to_CursorTM(Pen);
            TM.Cursors.Cursor_None = Cursor_to_CursorTM(None);
            TM.Cursors.Cursor_Link = Cursor_to_CursorTM(Link);
            TM.Cursors.Cursor_Pin = Cursor_to_CursorTM(Pin);
            TM.Cursors.Cursor_Person = Cursor_to_CursorTM(Person);
            TM.Cursors.Cursor_IBeam = Cursor_to_CursorTM(IBeam);
            TM.Cursors.Cursor_Cross = Cursor_to_CursorTM(Cross);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.Cursors,
                Enabled = Program.TM.Cursors.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = true,
                CanSwitchMode = false,
                CanOpenColorsEffects = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromScheme_12 = ImportWin12Preset,
                OnImportFromScheme_11 = ImportWin11Preset,
                OnImportFromScheme_10 = ImportWin10Preset,
                OnImportFromScheme_81 = ImportWin81Preset,
                OnImportFromScheme_8 = ImportWin8Preset,
                OnImportFromScheme_7 = ImportWin7Preset,
                OnImportFromScheme_Vista = ImportWinVistaPreset,
                OnImportFromScheme_XP = ImportWinXPPreset,
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
                i.Click -= Clicked;
            }

            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                i.Click += Clicked;
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
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
            CheckComptability();
            Button1.Enabled = true;
            groupBox3.Visible = true;
            
        }

        private void CheckComptability()
        {
            bool animatingAppStarting = _SelectedControl is not null && _SelectedControl.Prop_Cursor == Paths.CursorType.AppLoading;
            bool animatingBusy = _SelectedControl is not null && _SelectedControl.Prop_Cursor == Paths.CursorType.Busy;
            bool globalSet = global.Checked;

            if (globalSet)
            {
                radioImage4.Enabled = true;
                radioImage6.Enabled = true;
                radioImage2.Enabled = true;
                radioImage3.Enabled = true;
            }
            else
            {
                radioImage4.Enabled = animatingAppStarting || animatingBusy;
                radioImage6.Enabled = animatingAppStarting || animatingBusy;
                radioImage2.Enabled = !animatingBusy;
                radioImage3.Enabled = !animatingBusy;

                if ((!animatingAppStarting || !animatingBusy) && (tablessControl2.SelectedIndex == 2 || tablessControl2.SelectedIndex == 3)) radioImage2.Checked = true;
                else if (animatingBusy && (tablessControl2.SelectedIndex == 0 || tablessControl2.SelectedIndex == 1)) radioImage4.Checked = true;
            }
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
            trackBarX12.Value = (int)(CursorControl.Prop_BorderThickness * (float)trackBarX12.Maximum / 3f);
            trackBarX11.Value = CursorControl.Prop_LoadingCircleHot_AnimationSpeed;

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

            toggle1.Checked = CursorControl.Prop_Shadow_Enabled;
            ColorItem1.BackColor = CursorControl.Prop_Shadow_Color;
            trackBarX5.Value = CursorControl.Prop_Shadow_Blur;
            trackBarX6.Value = (int)Math.Round(CursorControl.Prop_Shadow_Opacity * 100f);
            trackBarX7.Value = CursorControl.Prop_Shadow_OffsetX;
            trackBarX8.Value = CursorControl.Prop_Shadow_OffsetY;
        }

        public void ApplyColorsToPreview(bool ignoreSettingFile = false)
        {
            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                ApplyColorsToPreview(i, ignoreSettingFile);
                i.Invalidate();
            }
        }

        public void ApplyColorsToPreview(CursorControl CursorControl, bool ignoreSettingFile = false)
        {
            CursorControl.Prop_UseFromFile = source1.Checked;
            if (!ignoreSettingFile) CursorControl.Prop_File = textBox1.Text;

            CursorControl.Prop_ArrowStyle = (Paths.ArrowStyle)ComboBox5.SelectedIndex;
            CursorControl.Prop_CircleStyle = (Paths.CircleStyle)ComboBox6.SelectedIndex;

            CursorControl.Prop_PrimaryColor1 = PrimaryColor1.BackColor;
            CursorControl.Prop_PrimaryColor2 = PrimaryColor2.BackColor;
            CursorControl.Prop_PrimaryColorGradient = CheckBox1.Checked;
            CursorControl.Prop_PrimaryColorGradientMode = Paths.ReturnGradientModeFromString((ComboBox1.SelectedItem ?? "vertical").ToString());
            CursorControl.Prop_PrimaryNoise = CheckBox5.Checked;
            CursorControl.Prop_PrimaryNoiseOpacity = trackBarX1.Value;

            CursorControl.Prop_SecondaryColor1 = SecondaryColor1.BackColor;
            CursorControl.Prop_SecondaryColor2 = SecondaryColor2.BackColor;
            CursorControl.Prop_SecondaryColorGradient = CheckBox4.Checked;
            CursorControl.Prop_SecondaryColorGradientMode = Paths.ReturnGradientModeFromString((ComboBox2.SelectedItem ?? "vertical").ToString());
            CursorControl.Prop_SecondaryNoise = CheckBox3.Checked;
            CursorControl.Prop_SecondaryNoiseOpacity = trackBarX2.Value / 100f;
            CursorControl.Prop_BorderThickness = ((float)trackBarX12.Value / (float)trackBarX12.Maximum) * 3f;
            CursorControl.Prop_LoadingCircleHot_AnimationSpeed = trackBarX11.Value;

            CursorControl.Prop_LoadingCircleBack1 = CircleColor1.BackColor;
            CursorControl.Prop_LoadingCircleBack2 = CircleColor2.BackColor;
            CursorControl.Prop_LoadingCircleBackGradient = CheckBox8.Checked;
            CursorControl.Prop_LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString((ComboBox4.SelectedItem ?? "vertical").ToString());
            CursorControl.Prop_LoadingCircleBackNoise = CheckBox7.Checked;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = trackBarX3.Value / 100f;

            CursorControl.Prop_LoadingCircleHot1 = LoadingColor1.BackColor;
            CursorControl.Prop_LoadingCircleHot2 = LoadingColor2.BackColor;
            CursorControl.Prop_LoadingCircleHotGradient = CheckBox2.Checked;
            CursorControl.Prop_LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString((ComboBox3.SelectedItem ?? "vertical").ToString());
            CursorControl.Prop_LoadingCircleHotNoise = CheckBox6.Checked;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = trackBarX4.Value / 100f;

            CursorControl.Prop_Shadow_Enabled = toggle1.Checked;
            CursorControl.Prop_Shadow_Color = ColorItem1.BackColor;
            CursorControl.Prop_Shadow_Blur = trackBarX5.Value;
            CursorControl.Prop_Shadow_Opacity = trackBarX6.Value / 100f;
            CursorControl.Prop_Shadow_OffsetX = trackBarX7.Value;
            CursorControl.Prop_Shadow_OffsetY = trackBarX8.Value;
        }

        private void PrimaryColor1_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_PrimaryColor1 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_PrimaryColor1 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_PrimaryColor1) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_PrimaryColor1)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_PrimaryColor1 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_PrimaryColor1 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void GroupBox3_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_PrimaryColor2 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_PrimaryColor2 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_PrimaryColor2) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_PrimaryColor2)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_PrimaryColor2 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_PrimaryColor2 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void GroupBox5_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_SecondaryColor1 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_SecondaryColor1 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_SecondaryColor1) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_SecondaryColor1)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_SecondaryColor1 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_SecondaryColor1 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void GroupBox4_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_SecondaryColor2 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_SecondaryColor2 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_SecondaryColor2) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_SecondaryColor2)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_SecondaryColor2 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_SecondaryColor2 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void GroupBox10_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleBack1 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_LoadingCircleBack1 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleBack1) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_LoadingCircleBack1)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleBack1 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_LoadingCircleBack1 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            PrimaryColor2.Visible = (sender as UI.WP.CheckBox).Checked;

            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_PrimaryColorGradient = CheckBox1.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_PrimaryColorGradient = CheckBox1.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            SecondaryColor2.Visible = (sender as UI.WP.CheckBox).Checked;

            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_SecondaryColorGradient = CheckBox4.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_SecondaryColorGradient = CheckBox4.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    i.Prop_PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                    i.Invalidate();
                }
            }
            else
            {
                _SelectedControl.Prop_PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                _SelectedControl.Invalidate();
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_PrimaryNoise = CheckBox5.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_PrimaryNoise = CheckBox5.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_SecondaryNoise = CheckBox3.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_SecondaryNoise = CheckBox3.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private float Angle = 180f;
        private float Increment = 5f;
        private int Cycles = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!IsShown) return;

            foreach (CursorControl i in AnimateList)
            {
                Increment = i.Prop_LoadingCircleHot_AnimationSpeed / 2f;
                i.Angle = Angle;
                i.Refresh();

                if (Angle + Increment >= 360f)
                {
                    Angle = 0f;
                }

                Angle += Increment;

                if (Cycles >= numericUpDown1.Value)
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

        private void Button5_Click(object sender, EventArgs e)
        {
            AnimateList.Clear();

            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                bool condition0 = !i.Prop_UseFromFile && (i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy);
                bool condition1 = i.Prop_UseFromFile && File.Exists(i.Prop_File) && Path.GetExtension(i.Prop_File).ToUpper() == ".ANI";
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
                _SelectedControl.Prop_LoadingCircleBack2 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_LoadingCircleBack2 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleBack2) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_LoadingCircleBack2)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleBack2 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_LoadingCircleBack2 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void GroupBox8_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleHot1 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_LoadingCircleHot1 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleHot1) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_LoadingCircleHot1)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleHot1 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_LoadingCircleHot1 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void GroupBox7_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleHot2 = (sender as ColorItem).BackColor;
                _SelectedControl.Invalidate();

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_LoadingCircleHot2 = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }

                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_LoadingCircleHot2) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_LoadingCircleHot2)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_LoadingCircleHot2 = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_LoadingCircleHot2 = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            CircleColor2.Visible = (sender as UI.WP.CheckBox).Checked;

            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleBackGradient = CheckBox8.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleBackGradient = CheckBox8.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            LoadingColor2.Visible = (sender as UI.WP.CheckBox).Checked;

            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleHotGradient = CheckBox2.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleHotGradient = CheckBox2.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleBackNoise = CheckBox7.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleBackNoise = CheckBox7.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleHotNoise = CheckBox6.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleHotNoise = CheckBox6.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _CopiedControl = _SelectedControl;
            Button2.Enabled = true;
            Button6.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (_CopiedControl is null) return;

            string file = _SelectedControl.Prop_File; // Save the current file path
            ApplyColorsFromCursor(_CopiedControl);
            ApplyColorsToPreview(_SelectedControl);
            _SelectedControl.Prop_File = file; // Restore the file path after applying colors
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
            {
                string file = i.Prop_File; // Save the current file path
                ApplyColorsFromCursor(_CopiedControl);
                ApplyColorsToPreview(i);
                i.Prop_File = file; // Restore the file path after applying colors
                i.Invalidate();
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    i.Prop_ArrowStyle = (Paths.ArrowStyle)ComboBox5.SelectedIndex;
                }
            }
            else
            {
                _SelectedControl.Prop_ArrowStyle = (Paths.ArrowStyle)ComboBox5.SelectedIndex;
            }
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_CircleStyle = (Paths.CircleStyle)ComboBox6.SelectedIndex;
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_CircleStyle = (Paths.CircleStyle)ComboBox6.SelectedIndex;
                }
            }
        }

        private void ColorItem1_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_Shadow_Color = (sender as ColorItem).BackColor;

                if (global.Checked)
                {
                    foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                    {
                        i.Prop_Shadow_Color = (sender as ColorItem).BackColor;
                        i.Invalidate();
                    }
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { _SelectedControl, new string[] { nameof(_SelectedControl.Prop_Shadow_Color) } }
            };

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    CList.Add(i, [nameof(i.Prop_Shadow_Color)]);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList, true);

            _SelectedControl.Prop_Shadow_Color = C;
            _SelectedControl.Invalidate();

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>().Where(x => x != _SelectedControl))
                {
                    i.Prop_Shadow_Color = C;
                    i.Invalidate();
                }
            }

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Cursors, Title = Program.Lang.Strings.Extensions.OpenCursor })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dlg.FileName;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _SelectedControl.Prop_File = textBox1.Text;
            _SelectedControl.Invalidate();
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_PrimaryNoiseOpacity = (sender as TrackBarX).Value / 100f;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_PrimaryNoiseOpacity = (sender as TrackBarX).Value / 100f;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_SecondaryNoiseOpacity = (sender as TrackBarX).Value / 100f;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_SecondaryNoiseOpacity = (sender as TrackBarX).Value / 100f;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX3_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleBackNoiseOpacity = (sender as TrackBarX).Value / 100f;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleBackNoiseOpacity = (sender as TrackBarX).Value / 100f;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX4_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleHotNoiseOpacity = (sender as TrackBarX).Value / 100f;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleHotNoiseOpacity = (sender as TrackBarX).Value / 100f;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX5_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_Shadow_Blur = (sender as TrackBarX).Value;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_Shadow_Blur = (sender as TrackBarX).Value;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX6_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_Shadow_Opacity = (sender as TrackBarX).Value / 100f;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_Shadow_Opacity = (sender as TrackBarX).Value / 100f;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX7_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_Shadow_OffsetX = (sender as TrackBarX).Value;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_Shadow_OffsetX = (sender as TrackBarX).Value;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX8_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_Shadow_OffsetY = (sender as TrackBarX).Value;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_Shadow_OffsetY = (sender as TrackBarX).Value;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX10_ValueChanged(object sender, EventArgs e)
        {
            setCursorsSizes();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CanOpenColorsEffects = (sender as UI.WP.RadioButton).Checked;

            if ((sender as UI.WP.RadioButton).Checked)
            {
                tablessControl1.SelectedIndex = 0;
                tablessControl1.Visible = true;
                tablessControl2.Visible = true;
            }

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_UseFromFile = !source0.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_UseFromFile = !source0.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void radioImage2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked) tablessControl2.SelectedIndex = 0;
        }

        private void radioImage3_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked) tablessControl2.SelectedIndex = 1;
        }

        private void radioImage4_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked) tablessControl2.SelectedIndex = 2;
        }

        private void radioImage6_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked) tablessControl2.SelectedIndex = 3;
        }

        private void radioImage1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked) tablessControl2.SelectedIndex = 4;
        }

        private void source1_CheckedChanged_1(object sender, EventArgs e)
        {
            if ((sender as UI.WP.RadioButton).Checked)
            {
                tablessControl1.SelectedIndex = 1;
                tablessControl1.Visible = true;
                tablessControl2.Visible = false;
            }
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if ((sender as Toggle).Checked) CheckBox9.Checked = false;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_Shadow_Enabled = toggle1.Checked;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_Shadow_Enabled = toggle1.Checked;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX12_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_BorderThickness = ((float)(sender as TrackBarX).Value / (float)(sender as TrackBarX).Maximum) * 3f;
                        i.Invalidate();
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_BorderThickness = ((float)(sender as TrackBarX).Value / (float)(sender as TrackBarX).Maximum) * 3f;
                    _SelectedControl.Invalidate();
                }
            }
        }

        private void trackBarX11_ValueChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            if (global.Checked)
            {
                foreach (CursorControl i in cursorsConatiner.Controls.OfType<CursorControl>())
                {
                    if (i is not null)
                    {
                        i.Prop_LoadingCircleHot_AnimationSpeed = (sender as TrackBarX).Value;

                        AnimateList.Clear();

                        bool condition0 = !i.Prop_UseFromFile && (i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy);
                        bool condition1 = i.Prop_UseFromFile && File.Exists(i.Prop_File) && Path.GetExtension(i.Prop_File).ToUpper() == ".ANI";
                        if (condition0 || condition1) { AnimateList.Add(i); }
                    }
                }
            }
            else
            {
                if (_SelectedControl is not null)
                {
                    _SelectedControl.Prop_LoadingCircleHot_AnimationSpeed = (sender as TrackBarX).Value;

                    AnimateList.Clear();

                    bool condition0 = !_SelectedControl.Prop_UseFromFile && (_SelectedControl.Prop_Cursor == Paths.CursorType.AppLoading | _SelectedControl.Prop_Cursor == Paths.CursorType.Busy);
                    bool condition1 = _SelectedControl.Prop_UseFromFile && File.Exists(_SelectedControl.Prop_File) && Path.GetExtension(_SelectedControl.Prop_File).ToUpper() == ".ANI";
                    if (condition0 || condition1) { AnimateList.Add(_SelectedControl); }
                }
            }

            Increment = (sender as TrackBarX).Value / 2f;
            Timer1.Enabled = true;
            Timer1.Start();
        }

        private void global_CheckedChanged(object sender, EventArgs e)
        {
            CheckComptability();
        }

        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            radioImage1.Enabled = !(sender as UI.WP.CheckBox).Checked;

            if ((sender as UI.WP.CheckBox).Checked)
            {
                toggle1.Checked = !(sender as UI.WP.CheckBox).Checked;
                if (tablessControl2.SelectedIndex == 4 && (sender as UI.WP.CheckBox).Checked) radioImage2.Checked = true;
            }
        }

        private void PrimaryColor1_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_PrimaryColor1 = e.Color;
        }

        private void PrimaryColor2_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_PrimaryColor2 = e.Color;
        }

        private void SecondaryColor1_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_SecondaryColor1 = e.Color;
        }

        private void SecondaryColor2_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_SecondaryColor2 = e.Color;
        }

        private void CircleColor1_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_LoadingCircleBack1 = e.Color;
        }

        private void CircleColor2_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_LoadingCircleBack2 = e.Color;
        }

        private void LoadingColor1_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_LoadingCircleHot1 = e.Color;
        }

        private void LoadingColor2_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_LoadingCircleHot2 = e.Color;
        }

        private void ColorItem1_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            _SelectedControl.Prop_Shadow_Color = e.Color;
        }
    }
}