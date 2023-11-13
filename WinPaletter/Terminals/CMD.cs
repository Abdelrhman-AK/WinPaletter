using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public partial class CMD
    {
        private Font F_cmd = new Font("Consolas", 18f, FontStyle.Regular);
        private bool _Shown = false;
        public Edition _Edition = Edition.CMD;

        public enum Edition
        {
            CMD,
            PowerShellx86,
            PowerShellx64
        }

        public CMD()
        {
            InitializeComponent();
        }

        #region    Voids not related to colors and shapes
        private void CMD_Load(object sender, EventArgs e)
        {
            _Shown = false;
            this.LoadLanguage();
            ApplyStyle(this);
            CheckBox1.Checked = Program.Settings.WindowsTerminals.ListAllFonts;
            RasterList.BringToFront();

            ApplyFromTM(Program.TM, _Edition);
            ApplyPreview();

            CMD_PopupForegroundLbl.Font = Fonts.Console;
            CMD_PopupBackgroundLbl.Font = Fonts.Console;
            CMD_AccentForegroundLbl.Font = Fonts.Console;
            CMD_AccentBackgroundLbl.Font = Fonts.Console;

            switch (_Edition)
            {
                case Edition.CMD:
                    {
                        Text = Program.Lang.CommandPrompt;
                        Icon = Properties.Resources.icons8_command_line;
                        Button4.Text = Program.Lang.Open_Testing_CMD;
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        Text = Program.Lang.PowerShellx86;
                        Icon = Properties.Resources.icons8_PowerShell;
                        Button4.Text = Program.Lang.Open_Testing_PowerShellx86;
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        Text = Program.Lang.PowerShellx64;
                        Icon = Properties.Resources.icons8_PowerShell;
                        Button4.Text = Program.Lang.Open_Testing_PowerShellx64;
                        break;
                    }

            }

            Button6.Image = Forms.MainFrm.Button20.Image.Resize(16, 16);
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

        private void CheckBox1_CheckedChanged(object sender)
        {
            if (_Shown)
            {
                Program.Settings.WindowsTerminals.ListAllFonts = CheckBox1.Checked;
                Program.Settings.WindowsTerminals.Save();
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {

            if (CMDEnabled.Checked)
            {
                Cursor = Cursors.WaitCursor;
                var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
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

                TMx.Dispose();

                Cursor = Cursors.Default;
            }
            else
            {
                MsgBox(Program.Lang.CMD_Enable, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM, _Edition);
            Close();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void CMD_Shown(object sender, EventArgs e)
        {
            _Shown = true;
            BringToFront();
        }
        #endregion

        #region    Voids to modify colors or shapes
        public void ApplyCursorShape()
        {
            CMD_PreviewCursorInner.Dock = DockStyle.Fill;
            CMD_PreviewCUR2.Padding = new Padding(1, 1, 1, 1);

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
                CMD_PreviewCUR2.Padding = new Padding(0, 0, 0, 0);
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
            CMD_PreviewCUR_Val.Text = CMD_CursorSizeBar.Value.ToString();
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
            CMD1.Font = new Font(F_cmd.Name, F_cmd.Size, F_cmd.Style);
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
            FontName.Font = new Font(F_cmd.Name, 9f, F_cmd.Style);

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

                var FC0 = CMD_PopupForegroundLbl.BackColor.IsDark() ? CMD_PopupForegroundLbl.BackColor.LightLight() : CMD_PopupForegroundLbl.BackColor.Dark(0.9f);
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

                var FC0 = CMD_PopupBackgroundLbl.BackColor.IsDark() ? CMD_PopupBackgroundLbl.BackColor.LightLight() : CMD_PopupBackgroundLbl.BackColor.Dark(0.9f);
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

                var FC0 = CMD_AccentBackgroundLbl.BackColor.IsDark() ? CMD_AccentBackgroundLbl.BackColor.LightLight() : CMD_AccentBackgroundLbl.BackColor.Dark(0.9f);
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

                var FC0 = CMD_AccentForegroundLbl.BackColor.IsDark() ? CMD_AccentForegroundLbl.BackColor.LightLight() : CMD_AccentForegroundLbl.BackColor.Dark(0.9f);
                CMD_AccentForegroundLbl.ForeColor = FC0;
                // CMD_AccentForegroundBar.AccentColor = CMD_AccentForegroundLbl.BackColor
                CMD_AccentForegroundBar.Invalidate();

            }
        }
        #endregion

        #region    TM Handling
        public void ApplyFromTM(Theme.Manager TM, Edition Edition)
        {
            switch (Edition)
            {
                case Edition.CMD:
                    {
                        SetFromTM(TM.CommandPrompt);
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        SetFromTM(TM.PowerShellx86);
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        SetFromTM(TM.PowerShellx64);
                        break;
                    }

            }

        }

        public void SetFromTM(Theme.Structures.Console Console)
        {
            CMDEnabled.Checked = Console.Enabled;

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

            ColorTable05.DefaultColor = Color.FromArgb(136, 23, 152);
            ColorTable06.DefaultColor = Color.FromArgb(193, 156, 0);

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
                {
                    var temp = Font.FromLogFont(new NativeMethods.GDI32.LogFont() { lfFaceName = Console.FaceName, lfWeight = Console.FontWeight });
                    F_cmd = new Font(temp.FontFamily, (int)Math.Round(Console.FontSize / 65536d), temp.Style);
                }
            }

            FontName.Text = F_cmd.Name;
            FontName.Font = new Font(F_cmd.Name, 9f, F_cmd.Style);
            CMD_FontSizeBar.Value = (int)Math.Round(F_cmd.Size);
            CMD_FontSizeVal.Text = F_cmd.Size.ToString();

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
            CMD_OpacityVal.Text = Conversion.Fix(Console.W10_1909_WindowAlpha / 255d * 100d).ToString();
            CMD_LineSelection.Checked = Console.W10_1909_LineSelection;
            CMD_TerminalScrolling.Checked = Console.W10_1909_TerminalScrolling;
            ApplyCursorShape();

            UpdateCurPreview();

        }

        public void ApplyToTM(Theme.Manager TM, Edition Edition)
        {
            var Console = new Theme.Structures.Console()
            {
                Enabled = CMDEnabled.Checked,
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
                var temp = CMD_PopupForegroundBar;
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
                var temp = CMD_PopupBackgroundBar;
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
                var temp = CMD_AccentForegroundBar;
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
                var temp = CMD_AccentBackgroundBar;
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
                        var prc = new Process()
                        {
                            StartInfo = new ProcessStartInfo()
                            {
                                FileName = PathsExt.CMD,
                                Verb = "runas",
                                WorkingDirectory = PathsExt.UserProfile
                            }
                        };
                        prc.Start();
                        break;
                    }

                case Edition.PowerShellx86:
                    {
                        var prc = new Process()
                        {
                            StartInfo = new ProcessStartInfo()
                            {
                                FileName = PathsExt.PS86_app,
                                Verb = "runas",
                                WorkingDirectory = PathsExt.UserProfile
                            }
                        };
                        prc.Start();
                        break;
                    }

                case Edition.PowerShellx64:
                    {
                        var prc = new Process()
                        {
                            StartInfo = new ProcessStartInfo()
                            {
                                FileName = PathsExt.PS64_app,
                                Verb = "runas",
                                WorkingDirectory = PathsExt.UserProfile
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

            if (_Shown)
            {
                RasterList.Visible = CMD_RasterToggle.Checked;
                ApplyPreview();
            }
        }

        private void CMD_FontWeightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;
            NativeMethods.GDI32.LogFont fx = new();
            F_cmd = new Font(F_cmd.Name, F_cmd.Size, F_cmd.Style);
            F_cmd.ToLogFont(fx);
            fx.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
            {
                var temp = Font.FromLogFont(fx);
                F_cmd = new Font(temp.Name, F_cmd.Size, temp.Style);
            }
            ApplyPreview();
        }

        private void CMD_FontsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                F_cmd = new Font(FontName.Font.Name, F_cmd.Size, F_cmd.Style);
                ApplyPreview();
            }

        }

        private void CMD_FontSizeBar_Scroll(object sender)
        {
            CMD_FontSizeVal.Text = CMD_FontSizeBar.Value.ToString();
            if (_Shown)
            {
                F_cmd = new Font(F_cmd.Name, CMD_FontSizeBar.Value, F_cmd.Style);
                ApplyPreview();
            }
        }

        private void RasterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                ApplyPreview();
            }
        }

        private void CMD_CursorSizeBar_Scroll(object sender)
        {
            UpdateCurPreview();
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

            var CList = new List<Control>() { (Control)sender, CMD_PreviewCUR2 };

            var C = Forms.ColorPickerDlg.Pick(CList);

            CMD_PreviewCUR2.BackColor = C;
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void CMD_OpacityBar_Scroll(object sender)
        {
            CMD_OpacityVal.Text = Conversion.Fix((((UI.WP.Trackbar)sender).Value / 255) * 100).ToString();
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

            var CList = new List<Control>() { (Control)sender, CMD1 };

            var _Conditions = new Conditions();
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable00".ToLower()))
                _Conditions.CMD_ColorTable00 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable01".ToLower()))
                _Conditions.CMD_ColorTable01 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable02".ToLower()))
                _Conditions.CMD_ColorTable02 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable03".ToLower()))
                _Conditions.CMD_ColorTable03 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable04".ToLower()))
                _Conditions.CMD_ColorTable04 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable05".ToLower()))
                _Conditions.CMD_ColorTable05 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable06".ToLower()))
                _Conditions.CMD_ColorTable06 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable07".ToLower()))
                _Conditions.CMD_ColorTable07 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable08".ToLower()))
                _Conditions.CMD_ColorTable08 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable09".ToLower()))
                _Conditions.CMD_ColorTable09 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable10".ToLower()))
                _Conditions.CMD_ColorTable10 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable11".ToLower()))
                _Conditions.CMD_ColorTable11 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable12".ToLower()))
                _Conditions.CMD_ColorTable12 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable13".ToLower()))
                _Conditions.CMD_ColorTable13 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable14".ToLower()))
                _Conditions.CMD_ColorTable14 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable15".ToLower()))
                _Conditions.CMD_ColorTable15 = true;

            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();
            ApplyPreview();

            UpdateFromTrack(1);
            UpdateFromTrack(2);
            UpdateFromTrack(3);
            UpdateFromTrack(4);

            CList.Clear();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            using (var _Def = Theme.Default.Get(Program.PreviewStyle))
            {
                bool ee = CMDEnabled.Checked;
                ApplyFromTM(_Def, _Edition);
                ApplyPreview();
                CMDEnabled.Checked = ee;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (OpenWPTHDlg.ShowDialog() == DialogResult.OK)
            {
                var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenWPTHDlg.FileName);
                bool ee = CMDEnabled.Checked;
                ApplyFromTM(TMx, _Edition);
                ApplyPreview();
                CMDEnabled.Checked = ee;
                TMx.Dispose();
            }
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            MsgBox(Program.Lang.CMD_NotAllWeights, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            bool ee = CMDEnabled.Checked;
            ApplyFromTM(TMx, _Edition);
            ApplyPreview();
            CMDEnabled.Checked = ee;
            TMx.Dispose();
        }

        private void CMD_FontSizeVal_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), CMD_FontSizeBar.Maximum), CMD_FontSizeBar.Minimum).ToString();
            CMD_FontSizeBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void CMD_PreviewCUR_Val_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), CMD_CursorSizeBar.Maximum), CMD_CursorSizeBar.Minimum).ToString();
            CMD_CursorSizeBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void CMD_OpacityVal_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), CMD_OpacityBar.Maximum), CMD_OpacityBar.Minimum).ToString();
            CMD_OpacityBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void CMDEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            FontDialog1.FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts;
            FontDialog1.Font = F_cmd;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                F_cmd = FontDialog1.Font;
                FontName.Text = FontName.Font.Name;
                CMD_FontSizeBar.Value = (int)Math.Round(FontDialog1.Font.Size);
                NativeMethods.GDI32.LogFont fx = new();
                F_cmd.ToLogFont(fx);
                fx.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
                {
                    var temp = Font.FromLogFont(fx);
                    F_cmd = new Font(temp.Name, F_cmd.Size, temp.Style);
                }
                FontName.Font = new Font(FontDialog1.Font.Name, 9f, F_cmd.Style);
            }

        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Edit-Windows-consoles-(Command-Prompt-and-PowerShell)");
        }
    }
}