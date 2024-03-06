using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class CMD
    {
        private Font F_cmd = new("Consolas", 18f, FontStyle.Regular);
        public Edition _Edition = Edition.CMD;

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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Filter_OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx, _Edition);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            LoadFromTM(TMx, _Edition);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle);
            LoadFromTM(TMx, _Edition);
            TMx.Dispose();
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM, _Edition);
            Close();
        }

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W11)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W10)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W81)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W7)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WVista)) { LoadFromTM(TMx, _Edition); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WXP)) { LoadFromTM(TMx, _Edition); }
        }

        private void Apply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                ApplyToTM(TMx, _Edition);
                ApplyToTM(Program.TM, _Edition);

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
                OnImportFromScheme_11 = ImportWin11Preset,
                OnImportFromScheme_10 = ImportWin10Preset,
                OnImportFromScheme_81 = ImportWin81Preset,
                OnImportFromScheme_7 = ImportWin7Preset,
                OnImportFromScheme_Vista = ImportWinVistaPreset,
                OnImportFromScheme_XP = ImportWinXPPreset,
            };

            switch (_Edition)
            {
                case Edition.CMD:
                    {
                        Text = Program.Lang.CommandPrompt;
                        Icon = Properties.Resources.icons8_command_line;
                        Button4.Text = Program.Lang.Open_Testing_CMD;
                        data.AspectName = Program.Lang.CommandPrompt;
                        data.Enabled = Program.TM.CommandPrompt.Enabled;
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        Text = Program.Lang.PowerShellx86;
                        Icon = Properties.Resources.icons8_PowerShell;
                        Button4.Text = Program.Lang.Open_Testing_PowerShellx86;
                        data.AspectName = Program.Lang.PowerShellx86;
                        data.Enabled = Program.TM.PowerShellx86.Enabled;
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        Text = Program.Lang.PowerShellx64;
                        Icon = Properties.Resources.icons8_PowerShell;
                        Button4.Text = Program.Lang.Open_Testing_PowerShellx64;
                        data.AspectName = Program.Lang.PowerShellx64;
                        data.Enabled = Program.TM.PowerShellx64.Enabled;
                        break;
                    }
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

            if (CMD_CursorStyle.SelectedIndex == 1)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;

                CMD_PreviewCUR2.Width = 1;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = all;
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            if (CMD_CursorStyle.SelectedIndex == 2)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;

                CMD_PreviewCUR2.Width = 10;
                CMD_PreviewCUR2.Height = 1;

                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            if (CMD_CursorStyle.SelectedIndex == 3)
            {
                CMD_PreviewCursorInner.BackColor = CMD_PreviewCUR.BackColor;

                CMD_PreviewCUR2.Width = 8;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = all;
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            if (CMD_CursorStyle.SelectedIndex == 4)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;

                CMD_PreviewCUR2.Width = 8;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = all;
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2;
            }

            if (CMD_CursorStyle.SelectedIndex == 5)
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
            CMD1.CMD_ColorTable00 = ColorTable00.BackColor;
            CMD1.CMD_ColorTable01 = ColorTable01.BackColor;
            CMD1.CMD_ColorTable02 = ColorTable02.BackColor;
            CMD1.CMD_ColorTable03 = ColorTable03.BackColor;
            CMD1.CMD_ColorTable04 = ColorTable04.BackColor;
            CMD1.CMD_ColorTable05 = ColorTable05.BackColor;
            CMD1.CMD_ColorTable06 = ColorTable06.BackColor;
            CMD1.CMD_ColorTable07 = ColorTable07.BackColor;
            CMD1.CMD_ColorTable08 = ColorTable08.BackColor;
            CMD1.CMD_ColorTable09 = ColorTable09.BackColor;
            CMD1.CMD_ColorTable10 = ColorTable10.BackColor;
            CMD1.CMD_ColorTable11 = ColorTable11.BackColor;
            CMD1.CMD_ColorTable12 = ColorTable12.BackColor;
            CMD1.CMD_ColorTable13 = ColorTable13.BackColor;
            CMD1.CMD_ColorTable14 = ColorTable14.BackColor;
            CMD1.CMD_ColorTable15 = ColorTable15.BackColor;
            CMD1.CMD_PopupForeground = CMD_PopupForegroundBar.Value;
            CMD1.CMD_PopupBackground = CMD_PopupBackgroundBar.Value;
            CMD1.CMD_ScreenColorsForeground = CMD_AccentForegroundBar.Value;
            CMD1.CMD_ScreenColorsBackground = CMD_AccentBackgroundBar.Value;
            CMD1.Font = new(F_cmd.Name, F_cmd.Size, F_cmd.Style);
            CMD1.PowerShell = _Edition == Edition.PowerShellx64 | _Edition == Edition.PowerShellx86;
            CMD1.Raster = CMD_RasterToggle.Checked;
            switch (RasterList.SelectedItem)
            {
                case "4x6":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._4x6;
                        break;
                    }

                case "6x8":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._6x8;
                        break;
                    }

                case "6x9":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._6x8;
                        break;
                    }

                case "8x8":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case "8x9":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case "16x8":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x8;
                        break;
                    }

                case "5x12":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._5x12;
                        break;
                    }

                case "7x12":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._7x12;
                        break;
                    }

                case "8x12":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

                case "16x12":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x12;
                        break;
                    }

                case "12x16":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._12x16;
                        break;
                    }

                case "10x18":
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._10x18;
                        break;
                    }

                default:
                    {
                        CMD1.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

            }

            FontName.Text = F_cmd.Name;
            FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);

            CMD1.Refresh();
        }

        public void UpdateFromTrack(int i)
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
        public void LoadFromTM(Theme.Manager TM, Edition Edition)
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
                using (Font temp = Font.FromLogFont(new NativeMethods.GDI32.LogFont() { lfFaceName = Console.FaceName, lfWeight = Console.FontWeight }))
                {
                    F_cmd = new(temp.FontFamily, (int)Math.Round(Console.FontSize / 65536d), temp.Style);
                }
            }

            FontName.Text = F_cmd.Name;
            FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);
            CMD_FontSizeBar.Value = (int)Math.Round(F_cmd.Size);

            if (Console.FontSize == 393220)
                RasterList.SelectedItem = "4x6";
            if (Console.FontSize == 524294)
                RasterList.SelectedItem = "6x8";
            if (Console.FontSize == 524296)
                RasterList.SelectedItem = "8x8";
            if (Console.FontSize == 524304)
                RasterList.SelectedItem = "16x8";
            if (Console.FontSize == 786437)
                RasterList.SelectedItem = "5x12";
            if (Console.FontSize == 786439)
                RasterList.SelectedItem = "7x12";
            if (Console.FontSize == 0)
                RasterList.SelectedItem = "8x12";
            if (Console.FontSize == 786448)
                RasterList.SelectedItem = "16x12";
            if (Console.FontSize == 1048588)
                RasterList.SelectedItem = "12x16";
            if (Console.FontSize == 1179658)
                RasterList.SelectedItem = "10x18";
            if (RasterList.SelectedItem == null)
                RasterList.SelectedItem = "8x12";

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

        public void ApplyToTM(Theme.Manager TM, Edition Edition)
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
                Console.FontSize = CMD_FontSizeBar.Value * 65536;
            }
            else
            {
                switch (RasterList.SelectedItem)
                {
                    case "4x6":
                        {
                            Console.FontSize = 393220;
                            break;
                        }

                    case "6x8":
                        {
                            Console.FontSize = 524294;
                            break;
                        }

                    case "6x9":
                        {
                            Console.FontSize = 524294;
                            break;
                        }

                    case "8x8":
                        {
                            Console.FontSize = 524296;
                            break;
                        }

                    case "8x9":
                        {
                            Console.FontSize = 524296;
                            break;
                        }

                    case "16x8":
                        {
                            Console.FontSize = 524304;
                            break;
                        }

                    case "5x12":
                        {
                            Console.FontSize = 786437;
                            break;
                        }

                    case "7x12":
                        {
                            Console.FontSize = 786439;
                            break;
                        }

                    case "8x12":
                        {
                            Console.FontSize = 0;
                            break;
                        }

                    case "16x12":
                        {
                            Console.FontSize = 786448;
                            break;
                        }

                    case "12x16":
                        {
                            Console.FontSize = 1048588;
                            break;
                        }

                    case "10x18":
                        {
                            Console.FontSize = 1179658;
                            break;
                        }

                    default:
                        {
                            Console.FontSize = 0;
                            break;
                        }

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
            {
                UI.WP.TrackBar temp = CMD_PopupForegroundBar;
                CMD_PopupForegroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    CMD_PopupForegroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    CMD_PopupForegroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    CMD_PopupForegroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    CMD_PopupForegroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    CMD_PopupForegroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    CMD_PopupForegroundLbl.Text += " (F)";
            }

            UpdateFromTrack(1);
            ApplyPreview();
        }

        private void CMD_PopupBackgroundBar_Scroll(object sender)
        {
            {
                UI.WP.TrackBar temp = CMD_PopupBackgroundBar;
                CMD_PopupBackgroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    CMD_PopupBackgroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    CMD_PopupBackgroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    CMD_PopupBackgroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    CMD_PopupBackgroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    CMD_PopupBackgroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    CMD_PopupBackgroundLbl.Text += " (F)";
            }

            UpdateFromTrack(2);
            ApplyPreview();
        }

        private void CMD_AccentForegroundBar_Scroll(object sender)
        {
            {
                UI.WP.TrackBar temp = CMD_AccentForegroundBar;
                CMD_AccentForegroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    CMD_AccentForegroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    CMD_AccentForegroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    CMD_AccentForegroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    CMD_AccentForegroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    CMD_AccentForegroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    CMD_AccentForegroundLbl.Text += " (F)";
            }

            UpdateFromTrack(3);
            UpdateFromTrack(4);
            ApplyPreview();
        }

        private void CMD_AccentBackgroundBar_Scroll(object sender)
        {
            {
                UI.WP.TrackBar temp = CMD_AccentBackgroundBar;
                CMD_AccentBackgroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    CMD_AccentBackgroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    CMD_AccentBackgroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    CMD_AccentBackgroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    CMD_AccentBackgroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    CMD_AccentBackgroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    CMD_AccentBackgroundLbl.Text += " (F)";
            }

            UpdateFromTrack(3);
            UpdateFromTrack(4);
            ApplyPreview();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            IntPtr intPtr = IntPtr.Zero;
            Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

            switch (_Edition)
            {
                case Edition.CMD:
                    {
                        Process prc = new()
                        {
                            StartInfo = new()
                            {
                                FileName = SysPaths.CMD,
                                Verb = "runas",
                                WorkingDirectory = SysPaths.UserProfile
                            }
                        };
                        prc.Start();
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        Process prc = new()
                        {
                            StartInfo = new()
                            {
                                FileName = SysPaths.PS86_app,
                                Verb = "runas",
                                WorkingDirectory = SysPaths.UserProfile
                            }
                        };
                        prc.Start();
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        Process prc = new()
                        {
                            StartInfo = new()
                            {
                                FileName = SysPaths.PS64_app,
                                Verb = "runas",
                                WorkingDirectory = SysPaths.UserProfile
                            }
                        };
                        prc.Start();
                        break;
                    }

            }

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
        }

        private void CMD_RasterToggle_CheckedChanged(object sender, EventArgs e)
        {
            Button5.Enabled = !CMD_RasterToggle.Checked;
            CMD_FontWeightBox.Enabled = !CMD_RasterToggle.Checked;

            if (IsShown)
            {
                RasterList.Visible = CMD_RasterToggle.Checked;
                ApplyPreview();
            }
        }

        private void CMD_FontWeightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            NativeMethods.GDI32.LogFont fx = new();
            F_cmd = new(F_cmd.Name, F_cmd.Size, F_cmd.Style);
            F_cmd.ToLogFont(fx);
            fx.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
            using (Font temp = Font.FromLogFont(fx))
            {
                F_cmd = new(temp.Name, F_cmd.Size, temp.Style);
            }
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

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                CMD_PreviewCUR2.BackColor = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
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

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
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

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable00".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable00) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable01".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable01) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable02".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable02) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable03".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable03) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable04".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable04) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable05".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable05) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable06".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable06) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable07".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable07) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable08".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable08) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable09".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable09) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable10".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable10) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable11".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable11) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable12".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable12) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable13".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable13) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable14".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable14) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable15".ToLower()))
                CList.Add(CMD1, new string[] { nameof(CMD1.CMD_ColorTable15) });

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
            Program.ToolTip.ToolTipText = Program.Lang.CMD_NotAllWeights;
            Program.ToolTip.ToolTipTitle = Program.Lang.Tip;
            Program.ToolTip.Image = Assets.Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = F_cmd, FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    F_cmd = dlg.Font;
                    FontName.Text = FontName.Font.Name;
                    CMD_FontSizeBar.Value = (int)Math.Round(dlg.Font.Size);
                    NativeMethods.GDI32.LogFont fx = new();
                    F_cmd.ToLogFont(fx);
                    fx.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
                    using (Font temp = Font.FromLogFont(fx))
                    {
                        F_cmd = new(temp.Name, F_cmd.Size, temp.Style);
                    }
                    FontName.Font = new(dlg.Font.Name, 9f, F_cmd.Style);
                }
            }
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                F_cmd = new(F_cmd.Name, CMD_FontSizeBar.Value, F_cmd.Style);
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
    }
}