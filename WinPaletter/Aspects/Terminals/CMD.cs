using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Simulation;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class CMD
    {
        private Font F_cmd = new("Consolas", 16f, FontStyle.Regular);
        public Edition _Edition = Edition.CMD;

        private Theme.Structures.WinTerminal terminal => Program.TM.Terminal;
        private Theme.Structures.WinTerminal terminalPreview => Program.TM.TerminalPreview;

        public enum Edition
        {
            CMD,
            PowerShellx86,
            PowerShellx64
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.Consoles);
        }

        public CMD()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx, _Edition);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Manager TMx = new(Manager.Source.Registry);
            LoadFromTM(TMx, _Edition);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Manager TMx = Default.Get(Program.WindowStyle);
            LoadFromTM(TMx, _Edition);
            TMx.Dispose();
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM, _Edition);
            Close();
        }
        private void ImportWin12Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W12)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W11)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W10)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W81)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin8Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W8)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.W7)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.WVista)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get(WindowStyle.WXP)) { LoadFromTM(TMx, _Edition); }
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Consoles)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Manager TMx = new(Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Manager.Source.File, filename);
                }

                ApplyToTM(TMx, _Edition);
                ApplyToTM(Program.TM, _Edition);
                ApplyToTM(Program.TM_Original, _Edition);

                switch (_Edition)
                {
                    case Edition.CMD:
                        {
                            TMx.Apply_CommandPrompt();
                            break;
                        }

                    case Edition.PowerShellx86:
                        {
                            TMx.Apply_PowerShell86();
                            break;
                        }

                    case Edition.PowerShellx64:
                        {
                            TMx.Apply_PowerShell64();
                            break;
                        }

                    default:
                        {
                            TMx.Apply_CommandPrompt();
                            break;
                        }

                }
            }

            Cursor = Cursors.Default;
        }

        private void CMD_Load(object sender, EventArgs e)
        {
            CMD_Preview.BackgroundImage = Program.FetchSuitableWallpaper(Program.TM, Program.WindowStyle);

            DesignerData data = new(this)
            {
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = true,
                CanSwitchMode = false,

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

            switch (_Edition)
            {
                case Edition.CMD:
                    {
                        Text = Program.Lang.Strings.Aspects.CommandPrompt;
                        Icon = Resources.cmd;
                        data.AspectName = Program.Lang.Strings.Aspects.CommandPrompt;
                        data.Enabled = Program.TM.CommandPrompt.Enabled;
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        Text = Program.Lang.Strings.Aspects.PowerShellx86;
                        Icon = Resources.ps;
                        data.AspectName = Program.Lang.Strings.Aspects.PowerShellx86;
                        data.Enabled = Program.TM.PowerShellx86.Enabled;
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        Text = Program.Lang.Strings.Aspects.PowerShellx64;
                        Icon = Resources.ps;
                        data.AspectName = Program.Lang.Strings.Aspects.PowerShellx64;
                        data.Enabled = Program.TM.PowerShellx64.Enabled;
                        break;
                    }
            }

            comboBox1.Items.Clear();

            if (terminal is not null)
            {
                foreach (Theme.Structures.WinTerminal.Types.Scheme scheme in terminal.Schemes.Where(x => !x.Name.Contains("Campbell"))) comboBox1.Items.Add($"{Program.Lang.Strings.Aspects.TerminalStable} > {scheme.Name}");
                comboBox1.SelectedIndex = -1;
            }

            if (terminalPreview is not null)
            {
                foreach (Theme.Structures.WinTerminal.Types.Scheme scheme in terminalPreview.Schemes.Where(x => !x.Name.Contains("Campbell"))) comboBox1.Items.Add($"{Program.Lang.Strings.Aspects.TerminalPreview} > {scheme.Name}");
                comboBox1.SelectedIndex = -1;
            }

            LoadData(data);

            toggle1.Checked = Program.Settings.WindowsTerminals.ListAllFonts;
            RasterList.BringToFront();

            LoadFromTM(Program.TM, _Edition);
            ApplyPreview();

            CMD_PopupForegroundLbl.Font = Fonts.Console;
            CMD_PopupBackgroundLbl.Font = Fonts.Console;
            CMD_AccentForegroundLbl.Font = Fonts.Console;
            CMD_AccentBackgroundLbl.Font = Fonts.Console;
        }

        #region    Methods to modify colors or shapes
        public void ApplyCursorShape()
        {
            CMD_PreviewCursorInner.Dock = DockStyle.Fill;
            CMD_PreviewCUR2.Padding = new(1, 1, 1, 1);

            if (CMD_CursorStyle.SelectedIndex == 0)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;
                CMD_PreviewCUR2.Width = 8;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = (int)Math.Round(all * (CMD_CursorSizeBar.Value / (double)CMD_CursorSizeBar.Maximum));
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            else if (CMD_CursorStyle.SelectedIndex == 1)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;

                CMD_PreviewCUR2.Width = 1;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = all;
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            else if (CMD_CursorStyle.SelectedIndex == 2)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;

                CMD_PreviewCUR2.Width = 10;
                CMD_PreviewCUR2.Height = 1;

                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            else if (CMD_CursorStyle.SelectedIndex == 3)
            {
                CMD_PreviewCursorInner.BackColor = CMD_PreviewCUR.BackColor;

                CMD_PreviewCUR2.Width = 8;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = all;
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            else if (CMD_CursorStyle.SelectedIndex == 4)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;

                CMD_PreviewCUR2.Width = 8;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = all;
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            else if (CMD_CursorStyle.SelectedIndex == 5)
            {
                CMD_PreviewCursorInner.Dock = DockStyle.None;
                CMD_PreviewCUR2.Padding = new(0, 0, 0, 0);
                CMD_PreviewCursorInner.Width = CMD_PreviewCUR2.Width;
                CMD_PreviewCursorInner.Height = 1;
                CMD_PreviewCursorInner.BackColor = CMD_PreviewCUR.BackColor;
                CMD_PreviewCursorInner.Top = 1;
                CMD_PreviewCursorInner.Left = 0;
                CMD_PreviewCUR2.Width = 8;
                CMD_PreviewCUR2.Height = 3;
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }
        }
        public void UpdateCurPreview()
        {
            int all = CMD_PreviewCUR.Height - 4;
            CMD_PreviewCUR2.Height = (int)Math.Round(all * (CMD_CursorSizeBar.Value / (double)CMD_CursorSizeBar.Maximum));
            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            ApplyCursorShape();
        }

        public void ApplyPreview()
        {
            CMD_Preview.CMD_ColorTable00 = ColorTable00.BackColor;
            CMD_Preview.CMD_ColorTable01 = ColorTable01.BackColor;
            CMD_Preview.CMD_ColorTable02 = ColorTable02.BackColor;
            CMD_Preview.CMD_ColorTable03 = ColorTable03.BackColor;
            CMD_Preview.CMD_ColorTable04 = ColorTable04.BackColor;
            CMD_Preview.CMD_ColorTable05 = ColorTable05.BackColor;
            CMD_Preview.CMD_ColorTable06 = ColorTable06.BackColor;
            CMD_Preview.CMD_ColorTable07 = ColorTable07.BackColor;
            CMD_Preview.CMD_ColorTable08 = ColorTable08.BackColor;
            CMD_Preview.CMD_ColorTable09 = ColorTable09.BackColor;
            CMD_Preview.CMD_ColorTable10 = ColorTable10.BackColor;
            CMD_Preview.CMD_ColorTable11 = ColorTable11.BackColor;
            CMD_Preview.CMD_ColorTable12 = ColorTable12.BackColor;
            CMD_Preview.CMD_ColorTable13 = ColorTable13.BackColor;
            CMD_Preview.CMD_ColorTable14 = ColorTable14.BackColor;
            CMD_Preview.CMD_ColorTable15 = ColorTable15.BackColor;
            CMD_Preview.CMD_PopupForeground = CMD_PopupForegroundBar.Value;
            CMD_Preview.CMD_PopupBackground = CMD_PopupBackgroundBar.Value;
            CMD_Preview.CMD_ScreenColorsForeground = CMD_AccentForegroundBar.Value;
            CMD_Preview.CMD_ScreenColorsBackground = CMD_AccentBackgroundBar.Value;
            CMD_Preview.Font = F_cmd;

            CMD_Preview.PowerShell = _Edition == Edition.PowerShellx64 | _Edition == Edition.PowerShellx86;
            CMD_Preview.Raster = CMD_RasterToggle.Checked;
            switch (RasterList.SelectedItem)
            {
                case "4x6":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._4x6;
                        break;
                    }

                case "6x8":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._6x8;
                        break;
                    }

                case "6x9":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._6x8;
                        break;
                    }

                case "8x8":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case "8x9":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case "16x8":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._16x8;
                        break;
                    }

                case "5x12":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._5x12;
                        break;
                    }

                case "7x12":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._7x12;
                        break;
                    }

                case "8x12":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._8x12;
                        break;
                    }

                case "16x12":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._16x12;
                        break;
                    }

                case "12x16":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._12x16;
                        break;
                    }

                case "10x18":
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._10x18;
                        break;
                    }

                default:
                    {
                        CMD_Preview.RasterSize = WinCMD.Raster_Sizes._8x12;
                        break;
                    }

            }

            FontName.Text = F_cmd.Name;
            FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);

            CMD_Preview.Alpha = CMD_OpacityBar.Value;
            CMD_Preview.Refresh();
        }

        private void UpdateFromTrack(int i)
        {
            if (i == 1)
            {
                switch (CMD_PopupForegroundBar.Value)
                {
                    case 0:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable00.BackColor;
                            break;
                        }
                    case 1:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            CMD_PopupForegroundLbl.BackColor = ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = CMD_PopupForegroundLbl.BackColor.IsDark() ? CMD_PopupForegroundLbl.BackColor.LightLight() : CMD_PopupForegroundLbl.BackColor.Dark(0.9f);
                CMD_PopupForegroundLbl.ForeColor = FC0;

                // CMD_PopupForegroundBar.AccentColor = CMD_PopupForegroundLbl.BackColor
                CMD_PopupForegroundBar.Invalidate();
            }
            else if (i == 2)
            {

                switch (CMD_PopupBackgroundBar.Value)
                {
                    case 0:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable00.BackColor;
                            break;
                        }
                    case 1:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            CMD_PopupBackgroundLbl.BackColor = ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = CMD_PopupBackgroundLbl.BackColor.IsDark() ? CMD_PopupBackgroundLbl.BackColor.LightLight() : CMD_PopupBackgroundLbl.BackColor.Dark(0.9f);
                CMD_PopupBackgroundLbl.ForeColor = FC0;
                // CMD_PopupBackgroundBar.AccentColor = CMD_PopupBackgroundLbl.BackColor
                CMD_PopupBackgroundBar.Invalidate();
            }

            else if (i == 3)
            {

                switch (CMD_AccentBackgroundBar.Value)
                {
                    case 0:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable00.BackColor;
                            break;
                        }
                    case 1:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            CMD_AccentBackgroundLbl.BackColor = ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = CMD_AccentBackgroundLbl.BackColor.IsDark() ? CMD_AccentBackgroundLbl.BackColor.LightLight() : CMD_AccentBackgroundLbl.BackColor.Dark(0.9f);
                CMD_AccentBackgroundLbl.ForeColor = FC0;
                // CMD_AccentBackgroundBar.AccentColor = CMD_AccentBackgroundLbl.BackColor
                CMD_AccentBackgroundBar.Invalidate();
                CMD_PreviewCUR.BackColor = CMD_AccentBackgroundLbl.BackColor;
            }

            else if (i == 4)
            {

                switch (CMD_AccentForegroundBar.Value)
                {
                    case 0:
                        {
                            if (CMD_AccentBackgroundBar.Value == CMD_AccentForegroundBar.Value)
                            {
                                CMD_AccentForegroundLbl.BackColor = ColorTable07.BackColor;
                            }
                            else
                            {
                                CMD_AccentForegroundLbl.BackColor = ColorTable00.BackColor;
                            }

                            break;
                        }
                    case 1:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            CMD_AccentForegroundLbl.BackColor = ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = CMD_AccentForegroundLbl.BackColor.IsDark() ? CMD_AccentForegroundLbl.BackColor.LightLight() : CMD_AccentForegroundLbl.BackColor.Dark(0.9f);
                CMD_AccentForegroundLbl.ForeColor = FC0;
                // CMD_AccentForegroundBar.AccentColor = CMD_AccentForegroundLbl.BackColor
                CMD_AccentForegroundBar.Invalidate();

            }
        }

        #endregion

        #region    TM Handling
        public void LoadFromTM(Manager TM, Edition Edition)
        {
            switch (Edition)
            {
                case Edition.CMD:
                    {
                        LoadFromTM(TM.CommandPrompt);
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        LoadFromTM(TM.PowerShellx86);
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        LoadFromTM(TM.PowerShellx64);
                        break;
                    }
            }
        }

        public void LoadFromTM(Theme.Structures.Console Console)
        {
            AspectEnabled = Console.Enabled;

            ColorTable00.BackColor = Console.ColorTable00;
            ColorTable01.BackColor = Console.ColorTable01;
            ColorTable02.BackColor = Console.ColorTable02;
            ColorTable03.BackColor = Console.ColorTable03;
            ColorTable04.BackColor = Console.ColorTable04;
            ColorTable05.BackColor = Console.ColorTable05;
            ColorTable06.BackColor = Console.ColorTable06;
            ColorTable07.BackColor = Console.ColorTable07;
            ColorTable08.BackColor = Console.ColorTable08;
            ColorTable09.BackColor = Console.ColorTable09;
            ColorTable10.BackColor = Console.ColorTable10;
            ColorTable11.BackColor = Console.ColorTable11;
            ColorTable12.BackColor = Console.ColorTable12;
            ColorTable13.BackColor = Console.ColorTable13;
            ColorTable14.BackColor = Console.ColorTable14;
            ColorTable15.BackColor = Console.ColorTable15;

            ColorTable05.DefaultBackColor = Color.FromArgb(136, 23, 152);
            ColorTable06.DefaultBackColor = Color.FromArgb(193, 156, 0);

            CMD_PopupForegroundBar.Value = Console.PopupForeground;
            CMD_PopupBackgroundBar.Value = Console.PopupBackground;
            CMD_AccentForegroundBar.Value = Console.ScreenColorsForeground;
            CMD_AccentBackgroundBar.Value = Console.ScreenColorsBackground;
            CMD_RasterToggle.Checked = Console.FontRaster;
            RasterList.Visible = Console.FontRaster;

            switch (Console.FontWeight)
            {
                case 0:
                    {
                        CMD_FontWeightBox.SelectedIndex = 0;
                        break;
                    }

                case 100:
                    {
                        CMD_FontWeightBox.SelectedIndex = 1;
                        break;
                    }

                case 200:
                    {
                        CMD_FontWeightBox.SelectedIndex = 2;
                        break;
                    }

                case 300:
                    {
                        CMD_FontWeightBox.SelectedIndex = 3;
                        break;
                    }

                case 400:
                    {
                        CMD_FontWeightBox.SelectedIndex = 4;
                        break;
                    }

                case 500:
                    {
                        CMD_FontWeightBox.SelectedIndex = 5;
                        break;
                    }

                case 600:
                    {
                        CMD_FontWeightBox.SelectedIndex = 6;
                        break;
                    }

                case 700:
                    {
                        CMD_FontWeightBox.SelectedIndex = 7;
                        break;
                    }

                case 800:
                    {
                        CMD_FontWeightBox.SelectedIndex = 8;
                        break;
                    }

                case 900:
                    {
                        CMD_FontWeightBox.SelectedIndex = 9;
                        break;
                    }

                default:
                    {
                        CMD_FontWeightBox.SelectedIndex = 4;
                        break;
                    }

            }

            if (!Console.FontRaster)
            {
                GDI32.LogFont logFont = new()
                {
                    lfFaceName = Console.FaceName,
                    lfHeight = -Console.PixelHeight,
                    lfWidth = Console.PixelWidth,
                    lfWeight = Console.FontWeight
                };

                F_cmd = Font.FromLogFont(logFont);

                CMD_FontPxHeight.Value = Math.Abs(logFont.lfHeight);
            }

            FontName.Text = F_cmd.Name;
            FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);

            if (Console.PixelWidth == 4 && Console.PixelHeight == 6) RasterList.SelectedItem = "4x6";
            else if (Console.PixelWidth == 6 && Console.PixelHeight == 8) RasterList.SelectedItem = "6x8";
            else if (Console.PixelWidth == 8 && Console.PixelHeight == 8) RasterList.SelectedItem = "8x8";
            else if (Console.PixelWidth == 16 && Console.PixelHeight == 8) RasterList.SelectedItem = "16x8";
            else if (Console.PixelWidth == 5 && Console.PixelHeight == 12) RasterList.SelectedItem = "5x12";
            else if (Console.PixelWidth == 7 && Console.PixelHeight == 12) RasterList.SelectedItem = "7x12";
            else if (Console.PixelWidth == 8 && Console.PixelHeight == 12) RasterList.SelectedItem = "8x12";
            else if (Console.PixelWidth == 16 && Console.PixelHeight == 12) RasterList.SelectedItem = "16x12";
            else if (Console.PixelWidth == 12 && Console.PixelHeight == 16) RasterList.SelectedItem = "12x16";
            else if (Console.PixelWidth == 10 && Console.PixelHeight == 18) RasterList.SelectedItem = "10x18";
            else RasterList.SelectedItem = "8x12"; // default fallback

            Console.CursorSize = CMD_CursorSizeBar.Value;
            if (CMD_CursorSizeBar.Value > 100)
                CMD_CursorSizeBar.Value = 100;
            if (CMD_CursorSizeBar.Value < 20)
                CMD_CursorSizeBar.Value = 20;

            CMD_CursorStyle.SelectedIndex = Console.W10_1909_CursorType;
            CMD_CursorColor.BackColor = Console.W10_1909_CursorColor;
            CMD_PreviewCUR2.BackColor = Console.W10_1909_CursorColor;
            CMD_EnhancedTerminal.Checked = Console.W10_1909_ForceV2;
            CMD_OpacityBar.Value = Console.W10_1909_WindowAlpha;
            CMD_LineSelection.Checked = Console.W10_1909_LineSelection;
            CMD_TerminalScrolling.Checked = Console.W10_1909_TerminalScrolling;

            ApplyCursorShape();
            UpdateCurPreview();
        }

        public void ApplyToTM(Manager TM, Edition Edition)
        {
            Theme.Structures.Console Console = new()
            {
                Enabled = AspectEnabled,
                ColorTable00 = ColorTable00.BackColor,
                ColorTable01 = ColorTable01.BackColor,
                ColorTable02 = ColorTable02.BackColor,
                ColorTable03 = ColorTable03.BackColor,
                ColorTable04 = ColorTable04.BackColor,
                ColorTable05 = ColorTable05.BackColor,
                ColorTable06 = ColorTable06.BackColor,
                ColorTable07 = ColorTable07.BackColor,
                ColorTable08 = ColorTable08.BackColor,
                ColorTable09 = ColorTable09.BackColor,
                ColorTable10 = ColorTable10.BackColor,
                ColorTable11 = ColorTable11.BackColor,
                ColorTable12 = ColorTable12.BackColor,
                ColorTable13 = ColorTable13.BackColor,
                ColorTable14 = ColorTable14.BackColor,
                ColorTable15 = ColorTable15.BackColor,
                PopupForeground = CMD_PopupForegroundBar.Value,
                PopupBackground = CMD_PopupBackgroundBar.Value,
                ScreenColorsForeground = CMD_AccentForegroundBar.Value,
                ScreenColorsBackground = CMD_AccentBackgroundBar.Value,
                FaceName = F_cmd.Name,
                FontRaster = CMD_RasterToggle.Checked,
                FontWeight = CMD_FontWeightBox.SelectedIndex * 100
            };

            if (!CMD_RasterToggle.Checked)
            {
                Console.PixelHeight = CMD_FontPxHeight.Value;
            }

            else
            {
                switch (RasterList.SelectedItem)
                {
                    case "4x6": Console.PixelWidth = 4; Console.PixelHeight = 6; break;
                    case "5x8": Console.PixelWidth = 5; Console.PixelHeight = 8; break;
                    case "6x8": Console.PixelWidth = 6; Console.PixelHeight = 8; break;
                    case "6x9": Console.PixelWidth = 6; Console.PixelHeight = 9; break;
                    case "8x8": Console.PixelWidth = 8; Console.PixelHeight = 8; break;
                    case "8x9": Console.PixelWidth = 8; Console.PixelHeight = 9; break;
                    case "16x8": Console.PixelWidth = 16; Console.PixelHeight = 8; break;
                    case "5x12": Console.PixelWidth = 5; Console.PixelHeight = 12; break;
                    case "7x12": Console.PixelWidth = 7; Console.PixelHeight = 12; break;
                    case "8x12": Console.PixelWidth = 8; Console.PixelHeight = 12; break;
                    case "16x12": Console.PixelWidth = 16; Console.PixelHeight = 12; break;
                    case "12x16": Console.PixelWidth = 12; Console.PixelHeight = 16; break;
                    case "10x18": Console.PixelWidth = 10; Console.PixelHeight = 18; break;
                    default: Console.PixelWidth = 0; Console.PixelHeight = 0; break;
                }
            }

            if (CMD_CursorSizeBar.Value < 20)
            {
                Console.CursorSize = 20;
            }
            else if (CMD_CursorSizeBar.Value > 100)
            {
                Console.CursorSize = 100;
            }
            else
            {
                Console.CursorSize = CMD_CursorSizeBar.Value;
            }

            Console.W10_1909_CursorColor = CMD_CursorColor.BackColor;
            Console.W10_1909_CursorType = CMD_CursorStyle.SelectedIndex;
            Console.W10_1909_ForceV2 = CMD_EnhancedTerminal.Checked;
            Console.W10_1909_WindowAlpha = CMD_OpacityBar.Value;
            Console.W10_1909_LineSelection = CMD_LineSelection.Checked;
            Console.W10_1909_TerminalScrolling = CMD_TerminalScrolling.Checked;

            switch (Edition)
            {
                case Edition.CMD:
                    {
                        TM.CommandPrompt = Console;
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        TM.PowerShellx86 = Console;
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        TM.PowerShellx64 = Console;
                        break;
                    }
            }
        }
        #endregion

        private void CommandPrompt_PopupForegroundBar_Scroll(object sender)
        {
            UI.WP.TrackBar temp = CMD_PopupForegroundBar;
            CMD_PopupForegroundLbl.Text = temp.Value.ToString();
            if (temp.Value == 10)
                CMD_PopupForegroundLbl.Text += " (A)";
            else if (temp.Value == 11)
                CMD_PopupForegroundLbl.Text += " (B)";
            else if (temp.Value == 12)
                CMD_PopupForegroundLbl.Text += " (C)";
            else if (temp.Value == 13)
                CMD_PopupForegroundLbl.Text += " (D)";
            else if (temp.Value == 14)
                CMD_PopupForegroundLbl.Text += " (E)";
            else if (temp.Value == 15)
                CMD_PopupForegroundLbl.Text += " (F)";

            UpdateFromTrack(1);
            ApplyPreview();
        }

        private void CMD_PopupBackgroundBar_Scroll(object sender)
        {
            UI.WP.TrackBar temp = CMD_PopupBackgroundBar;
            CMD_PopupBackgroundLbl.Text = temp.Value.ToString();

            if (temp.Value == 10)
                CMD_PopupBackgroundLbl.Text += " (A)";
            else if (temp.Value == 11)
                CMD_PopupBackgroundLbl.Text += " (B)";
            else if (temp.Value == 12)
                CMD_PopupBackgroundLbl.Text += " (C)";
            else if (temp.Value == 13)
                CMD_PopupBackgroundLbl.Text += " (D)";
            else if (temp.Value == 14)
                CMD_PopupBackgroundLbl.Text += " (E)";
            else if (temp.Value == 15)
                CMD_PopupBackgroundLbl.Text += " (F)";

            UpdateFromTrack(2);
            ApplyPreview();
        }

        private void CMD_AccentForegroundBar_Scroll(object sender)
        {
            UI.WP.TrackBar temp = CMD_AccentForegroundBar;
            CMD_AccentForegroundLbl.Text = temp.Value.ToString();

            if (temp.Value == 10)
                CMD_AccentForegroundLbl.Text += " (A)";
            else if (temp.Value == 11)
                CMD_AccentForegroundLbl.Text += " (B)";
            else if (temp.Value == 12)
                CMD_AccentForegroundLbl.Text += " (C)";
            else if (temp.Value == 13)
                CMD_AccentForegroundLbl.Text += " (D)";
            else if (temp.Value == 14)
                CMD_AccentForegroundLbl.Text += " (E)";
            else if (temp.Value == 15)
                CMD_AccentForegroundLbl.Text += " (F)";

            UpdateFromTrack(3);
            UpdateFromTrack(4);
            ApplyPreview();
        }

        private void CMD_AccentBackgroundBar_Scroll(object sender)
        {
            UI.WP.TrackBar temp = CMD_AccentBackgroundBar;
            CMD_AccentBackgroundLbl.Text = temp.Value.ToString();

            if (temp.Value == 10)
                CMD_AccentBackgroundLbl.Text += " (A)";
            else if (temp.Value == 11)
                CMD_AccentBackgroundLbl.Text += " (B)";
            else if (temp.Value == 12)
                CMD_AccentBackgroundLbl.Text += " (C)";
            else if (temp.Value == 13)
                CMD_AccentBackgroundLbl.Text += " (D)";
            else if (temp.Value == 14)
                CMD_AccentBackgroundLbl.Text += " (E)";
            else if (temp.Value == 15)
                CMD_AccentBackgroundLbl.Text += " (F)";

            UpdateFromTrack(3);
            UpdateFromTrack(4);
            ApplyPreview();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            IntPtr intPtr = IntPtr.Zero;
            Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
            string tempcmd = SysPaths.appData + "\\testcolors.cmd";

            if (_Edition == Edition.CMD)
            {
                int initBg = CMD_AccentBackgroundBar.Value;
                int initFg = CMD_AccentForegroundBar.Value;

                string _testCommand = $@"@echo off
                    title {this.Text}

                    REM --- Set initial background and foreground colors ---
                    set initBg={initBg}
                    set initFg={initFg}

                    REM Convert decimal 10-15 to hex A-F for initial background
                    if ""%initBg%""==""10"" set initBg=A
                    if ""%initBg%""==""11"" set initBg=B
                    if ""%initBg%""==""12"" set initBg=C
                    if ""%initBg%""==""13"" set initBg=D
                    if ""%initBg%""==""14"" set initBg=E
                    if ""%initBg%""==""15"" set initBg=F

                    REM Convert decimal 10-15 to hex A-F for initial foreground
                    if ""%initFg%""==""10"" set initFg=A
                    if ""%initFg%""==""11"" set initFg=B
                    if ""%initFg%""==""12"" set initFg=C
                    if ""%initFg%""==""13"" set initFg=D
                    if ""%initFg%""==""14"" set initFg=E
                    if ""%initFg%""==""15"" set initFg=F

                    set initBg=%initBg:~-1%
                    set initFg=%initFg:~-1%
                    color %initBg%%initFg%
                    cls
                    REM --- End initial color setup ---

                    :colorloop
                    cls

                    REM Prompt for background and foreground
                    set /p bg=""{Program.Lang.Strings.Aspects.Consoles.Backgrounds}: ""
                    set /p fg=""{Program.Lang.Strings.Aspects.Consoles.Foregrounds}: ""

                    REM Convert decimal 10-15 to hex A-F for background
                    if ""%bg%""==""10"" set bg=A
                    if ""%bg%""==""11"" set bg=B
                    if ""%bg%""==""12"" set bg=C
                    if ""%bg%""==""13"" set bg=D
                    if ""%bg%""==""14"" set bg=E
                    if ""%bg%""==""15"" set bg=F

                    REM Convert decimal 10-15 to hex A-F for foreground
                    if ""%fg%""==""10"" set fg=A
                    if ""%fg%""==""11"" set fg=B
                    if ""%fg%""==""12"" set fg=C
                    if ""%fg%""==""13"" set fg=D
                    if ""%fg%""==""14"" set fg=E
                    if ""%fg%""==""15"" set fg=F

                    REM Use only last character in case input is like 0A, 0B, etc.
                    set bg=%bg:~-1%
                    set fg=%fg:~-1%

                    REM Set main console color
                    color %bg%%fg%
                    cls

                    REM Display chosen colors
                    echo Main Background: %bg%
                    echo Main Foreground: %fg%
                    echo.
                    echo {Program.Lang.Strings.Aspects.Consoles.Popup_Note}
                    echo.
                    pause
                    goto colorloop";

                ProcessStartInfo info = new()
                {
                    FileName = SysPaths.CMD,
                    Verb = "runas",
                    WorkingDirectory = SysPaths.UserProfile,
                    Arguments = $"/k \"{tempcmd}\""
                };

                File.WriteAllText(tempcmd, _testCommand);

                using Process process = Process.Start(info);
            }
            else
            {
                int initBg = CMD_AccentBackgroundBar.Value;
                int initFg = CMD_AccentForegroundBar.Value;

                string[] colorNames = {
    "Black", "DarkBlue", "DarkGreen", "DarkCyan",
    "DarkRed", "DarkMagenta", "DarkYellow", "Gray",
    "DarkGray", "Blue", "Green", "Cyan",
    "Red", "Magenta", "Yellow", "White"
};

                string initBgName = colorNames[initBg];
                string initFgName = colorNames[initFg];

                string curBackground = Program.Lang.Strings.Aspects.Consoles.CurrentBackground;
                string curForeground = Program.Lang.Strings.Aspects.Consoles.CurrentForeground;
                string tableHeader = Program.Lang.Strings.Aspects.Consoles.ColorName;
                string promptBackground = Program.Lang.Strings.Aspects.Consoles.Backgrounds_PS;
                string promptForeground = Program.Lang.Strings.Aspects.Consoles.Foregrounds_PS;
                string errorMessage = Program.Lang.Strings.Aspects.Consoles.InvalidColors;
                string optionalRefresh = Program.Lang.Strings.Aspects.Consoles.OptTitleRefresh;
                string thanksto = $"{Program.Lang.Strings.General.ThanksTo} neilpa/cmd-colors-solarized";
                string popupNote = Program.Lang.Strings.Aspects.Consoles.Popup_Note;

                string psScript = $@"
$Host.UI.RawUI.BackgroundColor = [System.ConsoleColor]::{initBgName}
$Host.UI.RawUI.ForegroundColor = [System.ConsoleColor]::{initFgName}
Clear-Host

function Show-PreferencesAndTable {{
    # Standard ConsoleColor order for reference
    $colorNames = @(
        'Black', 'Blue', 'Green', 'Cyan',
        'Red', 'Magenta', 'Yellow', 'Gray',
        'DarkGray', 'DarkBlue', 'DarkGreen', 'DarkCyan',
        'DarkRed', 'DarkMagenta', 'DarkYellow', 'White'
    )

    $Colors = [Enum]::GetValues([System.ConsoleColor])
    $fgID = [int]($Colors | Where-Object {{ $_ -eq $Host.UI.RawUI.ForegroundColor }})
    $bgID = [int]($Colors | Where-Object {{ $_ -eq $Host.UI.RawUI.BackgroundColor }})

    Write-Host ('{thanksto}')
    Write-Host ('https://github.com/neilpa/cmd-colors-solarized/blob/master/Out-Colors.ps1')
    Write-Host ''
    Write-Host ('{curBackground}: {{0}} (ID {{1}})' -f $Host.UI.RawUI.BackgroundColor, $bgID)
    Write-Host ('{curForeground}: {{0}} (ID {{1}})' -f $Host.UI.RawUI.ForegroundColor, $fgID)
    Write-Host ''
    Write-Host ('{{0,-13}} | {{1,-4}} | {{2,-10}} | {{3,-4}} | {{4,-10}}' -f '{tableHeader}', 'ID', '', 'ID', '')
    Write-Host ('-' * 60)
    for ($i=0; $i -lt 8; $i++) {{
        $name0 = $colorNames[$i]
        $id0 = $i
        $id8 = $i+8
        $name8 = $colorNames[$id8]
        Write-Host -NoNewline ('{{0,-13}} | {{1,-4}} | ' -f $name0, $id0)
        Write-Host -NoNewline ('          ') -BackgroundColor $Colors[$id0]
        Write-Host -NoNewline (' | {{0,-4}} | ' -f $id8)
        Write-Host ('          ') -BackgroundColor $Colors[$id8]
    }}
    Write-Host ''
}}

while ($true) {{
    Show-PreferencesAndTable
    $bg = Read-Host '{promptBackground}'
    if ([string]::IsNullOrWhiteSpace($bg)) {{ break }}
    $fg = Read-Host '{promptForeground}'
    if ([string]::IsNullOrWhiteSpace($fg)) {{ break }}
    try {{
        $Host.UI.RawUI.ForegroundColor = $fg
        $Host.UI.RawUI.BackgroundColor = $bg
        $rawui = $Host.UI.RawUI
        $rawui.WindowTitle = '' # {optionalRefresh}
        $rawui.ForegroundColor = $fg
        $rawui.BackgroundColor = $bg
        Clear-Host
    }} catch {{
        Write-Host '{errorMessage}' -ForegroundColor Red
        Start-Sleep -Seconds 1
    }}
}}
";

                // Prepare the start info
                var psi = new ProcessStartInfo
                {
                    FileName = _Edition == Edition.PowerShellx86 ? SysPaths.PS86_app : SysPaths.PS64_app,
                    WorkingDirectory = SysPaths.UserProfile,
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{psScript.Replace("\"", "`\"")}\"",
                };

                using (Process.Start(psi)) { }
            }

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
        }

        private void CMD_RasterToggle_CheckedChanged(object sender, EventArgs e)
        {
            Button5.Enabled = !CMD_RasterToggle.Checked;
            CMD_FontWeightBox.Enabled = !CMD_RasterToggle.Checked;

            groupBox3.Enabled = !CMD_RasterToggle.Checked;
            RasterList.Visible = CMD_RasterToggle.Checked;

            if (IsShown)
            {
                ApplyPreview();
            }
        }

        private void CMD_FontWeightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            GDI32.LogFont logFont = new();
            F_cmd.ToLogFont(logFont);
            logFont.lfHeight = -CMD_FontPxHeight.Value;
            logFont.lfWidth = 0;
            logFont.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
            F_cmd = Font.FromLogFont(logFont);

            ApplyPreview();
        }

        private void CMD_FontsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                F_cmd = new(FontName.Font.Name, F_cmd.Size, F_cmd.Style);
                ApplyPreview();
            }
        }

        private void RasterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsShown) ApplyPreview();
        }

        private void CMD_CursorStyle_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplyCursorShape();
        }

        private void CMD_CursorColor_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                CMD_PreviewCUR2.BackColor = CMD_CursorColor.BackColor;
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { CMD_PreviewCUR2, new string[] { nameof(CMD_PreviewCUR2.BackColor) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            CMD_PreviewCUR2.BackColor = C;
            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void ColorTable00_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                ApplyPreview();
                UpdateFromTrack(1);
                UpdateFromTrack(2);
                UpdateFromTrack(3);
                UpdateFromTrack(4);
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) }},
            };

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable00".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable00)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable01".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable01)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable02".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable02)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable03".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable03)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable04".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable04)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable05".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable05)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable06".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable06)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable07".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable07)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable08".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable08)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable09".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable09)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable10".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable10)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable11".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable11)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable12".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable12)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable13".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable13)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable14".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable14)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable15".ToLower()))
                CList.Add(CMD_Preview, [nameof(CMD_Preview.CMD_ColorTable15)]);

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();
            ApplyPreview();

            UpdateFromTrack(1);
            UpdateFromTrack(2);
            UpdateFromTrack(3);
            UpdateFromTrack(4);

            CList.Clear();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            Program.ToolTip.ToolTipText = Program.Lang.Strings.Aspects.Consoles.CMD_NotAllWeights;
            Program.ToolTip.ToolTipTitle = Program.Lang.Strings.General.Tip;
            Program.ToolTip.Image = Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = F_cmd, FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FontName.Text = dlg.Font.Name;

                    CMD_FontPxHeight.Value = (int)Math.Round(dlg.Font.Size);

                    GDI32.LogFont logFont = new();
                    dlg.Font.ToLogFont(logFont);
                    logFont.lfHeight = (int)-dlg.Font.Size; // Negative value for pixel height
                    logFont.lfWidth = 0; // Set to 0 for default width
                    logFont.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
                    F_cmd = Font.FromLogFont(logFont);

                    FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);
                }
            }
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                GDI32.LogFont logFont = new();
                F_cmd.ToLogFont(logFont);
                logFont.lfHeight = -CMD_FontPxHeight.Value;
                logFont.lfWidth = 0; // Set to 0 for default width
                F_cmd = Font.FromLogFont(logFont);
                ApplyPreview();
            }
        }

        private void trackBarX1_ValueChanged_1(object sender, EventArgs e)
        {
            UpdateCurPreview();
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                Program.Settings.WindowsTerminals.ListAllFonts = toggle1.Checked;
                Program.Settings.WindowsTerminals.Save();
            }
        }

        private void CMD_AccentForegroundBar_DragDrop(object sender, DragEventArgs e)
        {
            ColorItem colorItem = e.Data.GetData(typeof(ColorItem).FullName) as ColorItem;

            if (sender == Label49 || sender == CMD_AccentForegroundLbl || sender == CMD_AccentForegroundBar)
                CMD_AccentForegroundBar.Value = int.Parse(colorItem.Name.Replace("ColorTable", string.Empty));

            else if (sender == Label50 || sender == CMD_AccentBackgroundLbl || sender == CMD_AccentBackgroundBar)
                CMD_AccentBackgroundBar.Value = int.Parse(colorItem.Name.Replace("ColorTable", string.Empty));

            else if (sender == Label17 || sender == CMD_PopupForegroundLbl || sender == CMD_PopupForegroundBar)
                CMD_PopupForegroundBar.Value = int.Parse(colorItem.Name.Replace("ColorTable", string.Empty));

            else if (sender == Label18 || sender == CMD_PopupBackgroundLbl || sender == CMD_PopupBackgroundBar)
                CMD_PopupBackgroundBar.Value = int.Parse(colorItem.Name.Replace("ColorTable", string.Empty));
        }

        private void Label49_DragOver(object sender, DragEventArgs e)
        {
            ColorItem colorItem = e.Data.GetData(typeof(ColorItem).FullName) as ColorItem;
            e.Effect = colorItem is not null ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void CMD_OpacityBar_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                CMD_Preview.Alpha = (sender as TrackBarX).Value;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                Theme.Structures.WinTerminal.Types.Scheme scheme = null;

                string selected = comboBox1.SelectedItem.ToString();
                if (selected.StartsWith($"{Program.Lang.Strings.Aspects.TerminalStable} > "))
                {
                    selected = selected.Replace($"{Program.Lang.Strings.Aspects.TerminalStable} > ", string.Empty);
                    scheme = Program.TM.Terminal.Schemes.FirstOrDefault(x => x.Name == selected);
                }
                else if (selected.StartsWith($"{Program.Lang.Strings.Aspects.TerminalPreview} > "))
                {
                    selected = selected.Replace($"{Program.Lang.Strings.Aspects.TerminalPreview} > ", string.Empty);
                    scheme = Program.TM.TerminalPreview.Schemes.FirstOrDefault(x => x.Name == selected);
                }

                if (scheme is not null)
                {
                    ColorTable00.BackColor = scheme.Black;
                    ColorTable01.BackColor = scheme.Blue;
                    ColorTable02.BackColor = scheme.Green;
                    ColorTable03.BackColor = scheme.Cyan;
                    ColorTable04.BackColor = scheme.Red;
                    ColorTable05.BackColor = scheme.Purple;
                    ColorTable06.BackColor = scheme.Yellow;
                    ColorTable07.BackColor = scheme.White;
                    ColorTable08.BackColor = scheme.BrightBlack;
                    ColorTable09.BackColor = scheme.BrightBlue;
                    ColorTable10.BackColor = scheme.BrightGreen;
                    ColorTable11.BackColor = scheme.BrightCyan;
                    ColorTable12.BackColor = scheme.BrightRed;
                    ColorTable13.BackColor = scheme.BrightPurple;
                    ColorTable14.BackColor = scheme.BrightYellow;
                    ColorTable15.BackColor = scheme.BrightWhite;
                    CMD_CursorColor.BackColor = scheme.CursorColor;
                }

                UpdateCurPreview();
                UpdateFromTrack(1);
                UpdateFromTrack(2);
                UpdateFromTrack(3);
                UpdateFromTrack(4);
                ApplyPreview();
            }
        }

        private void CMD_CursorColor_ContextMenuItemClickedInvoker(object sender, ColorItem.ContextMenuItemClickedEventArgs e)
        {
            CMD_PreviewCUR2.BackColor = e.ColorItem.BackColor;
        }

        private void ColorTable06_ContextMenuItemClickedInvoker(object sender, ColorItem.ContextMenuItemClickedEventArgs e)
        {
            ApplyPreview();
            UpdateFromTrack(1);
            UpdateFromTrack(2);
            UpdateFromTrack(3);
            UpdateFromTrack(4);
        }
    }
}