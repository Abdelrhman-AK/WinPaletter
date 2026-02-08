using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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
    public partial class ExternalTerminal
    {
        private bool _Shown = false;
        private Font F_cmd = new("Consolas", 18f, FontStyle.Regular);

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.Consoles);
        }

        public ExternalTerminal()
        {
            InitializeComponent();
        }


        private void LoadFromWPTH(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Localization.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx.CommandPrompt);
                        ApplyPreview();
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Manager TMx = new(Manager.Source.Registry);
            LoadFromTM(TMx.CommandPrompt);
            ApplyPreview();
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager _Def = Default.FromOS(Program.WindowStyle))
            {
                LoadFromTM(_Def.CommandPrompt);
                ApplyPreview();
            }
        }

        private void ImportWin12Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.W12)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.W11)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.W10)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.W81)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ImportWin8Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.W8)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.W7)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.WVista)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager TMx = Default.FromOS(WindowStyle.WXP)) { LoadFromTM(TMx.CommandPrompt); }
        }

        private void ExternalTerminal_Load(object sender, EventArgs e)
        {
            _Shown = false;
            FillTerminals(ComboBox1);
            RasterList.BringToFront();
            CMD_Preview.BackgroundImage = Program.WallpaperMonitor.Get(Program.TM, Program.WindowStyle);

            Icon = Resources.cmd;

            CMD_PopupForegroundLbl.Font = Fonts.Console;
            CMD_PopupBackgroundLbl.Font = Fonts.Console;
            CMD_AccentForegroundLbl.Font = Fonts.Console;
            CMD_AccentBackgroundLbl.Font = Fonts.Console;

            toggle1.Checked = Program.Settings.WindowsTerminals.ListAllFonts;

            IEnumerable<UI.WP.Button> buttons = flowLayoutPanel1.Controls.OfType<UI.WP.Button>();
            UI.WP.Button button_import = buttons.Where(b => b.Name.StartsWith("btn_import")).FirstOrDefault() ?? null;
            button_import.Text = Program.Localization.Strings.Previewer.Import_wpth;

            #region Menu _data
            button_import.Menu.Items.Clear();

            ToolStripMenuItem import_current = new();
            ToolStripMenuItem import_defaultWindows = new();
            ToolStripMenuItem import_scheme = new();
            ToolStripMenuItem import_scheme_12 = new();
            ToolStripMenuItem import_scheme_11 = new();
            ToolStripMenuItem import_scheme_10 = new();
            ToolStripMenuItem import_scheme_81 = new();
            ToolStripMenuItem import_scheme_8 = new();
            ToolStripMenuItem import_scheme_7 = new();
            ToolStripMenuItem import_scheme_Vista = new();
            ToolStripMenuItem import_scheme_XP = new();

            import_current.Text = Program.Localization.Strings.Previewer.Import_current;
            import_defaultWindows.Text = Program.Localization.Strings.Previewer.Import_defaultWindows;
            import_scheme.Text = Program.Localization.Strings.Previewer.Import_preset;
            import_scheme_12.Text = Program.Localization.Strings.Windows.W12;
            import_scheme_11.Text = Program.Localization.Strings.Windows.W11;
            import_scheme_10.Text = Program.Localization.Strings.Windows.W10;
            import_scheme_81.Text = Program.Localization.Strings.Windows.W81;
            import_scheme_8.Text = Program.Localization.Strings.Windows.W8;
            import_scheme_7.Text = Program.Localization.Strings.Windows.W7;
            import_scheme_Vista.Text = Program.Localization.Strings.Windows.WVista;
            import_scheme_XP.Text = Program.Localization.Strings.Windows.WXP;

            import_current.Image = AspectsResources.CurrentApplied;

            if (Program.WindowStyle == PreviewHelpers.WindowStyle.W12)
            {
                import_defaultWindows.Image = WinLogos.Add_Win12_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W11)
            {
                import_defaultWindows.Image = WinLogos.Add_Win11_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W10)
            {
                import_defaultWindows.Image = WinLogos.Add_Win10_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W81)
            {
                import_defaultWindows.Image = WinLogos.Add_Win81_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W8)
            {
                import_defaultWindows.Image = WinLogos.Add_Win8_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W7)
            {
                import_defaultWindows.Image = WinLogos.Add_Win7_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.WVista)
            {
                import_defaultWindows.Image = WinLogos.Add_WinVista_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.WXP)
            {
                import_defaultWindows.Image = WinLogos.Add_WinXP_20px;
            }

            import_scheme.Image = AspectsResources.Scheme;
            import_scheme_12.Image = WinLogos.Add_Win12_20px;
            import_scheme_11.Image = WinLogos.Add_Win11_20px;
            import_scheme_10.Image = WinLogos.Add_Win10_20px;
            import_scheme_81.Image = WinLogos.Add_Win81_20px;
            import_scheme_8.Image = WinLogos.Add_Win8_20px;
            import_scheme_7.Image = WinLogos.Add_Win7_20px;
            import_scheme_Vista.Image = WinLogos.Add_WinVista_20px;
            import_scheme_XP.Image = WinLogos.Add_WinXP_20px;

            import_scheme.DropDown.Renderer = button_import.Menu.Renderer;
            import_scheme.DropDown.Items.Add(import_scheme_12);
            import_scheme.DropDown.Items.Add(import_scheme_11);
            import_scheme.DropDown.Items.Add(import_scheme_10);
            import_scheme.DropDown.Items.Add(import_scheme_81);
            import_scheme.DropDown.Items.Add(import_scheme_8);
            import_scheme.DropDown.Items.Add(import_scheme_7);
            import_scheme.DropDown.Items.Add(import_scheme_Vista);
            import_scheme.DropDown.Items.Add(import_scheme_XP);

            button_import.Menu.Items.Add(import_current);
            button_import.Menu.Items.Add(import_defaultWindows);

            #endregion

            #region Events/Overrides

            if (button_import != null)
                button_import.Click += LoadFromWPTH;

            if (import_current != null)
                import_current.Click += LoadFromCurrent;

            if (import_defaultWindows != null)
                import_defaultWindows.Click += LoadFromDefault;

            if (import_scheme_12 != null)
                import_scheme_12.Click += ImportWin12Preset;

            if (import_scheme_11 != null)
                import_scheme_11.Click += ImportWin11Preset;

            if (import_scheme_10 != null)
                import_scheme_10.Click += ImportWin10Preset;

            if (import_scheme_81 != null)
                import_scheme_81.Click += ImportWin81Preset;

            if (import_scheme_8 != null)
                import_scheme_8.Click += ImportWin8Preset;

            if (import_scheme_7 != null)
                import_scheme_7.Click += ImportWin7Preset;

            if (import_scheme_Vista != null)
                import_scheme_Vista.Click += ImportWinVistaPreset;

            if (import_scheme_XP != null)
                import_scheme_XP.Click += ImportWinXPPreset;

            FormClosedEventHandler FormClosed = null;

            FormClosed = (sender, args) =>
            {
                if (button_import != null)
                    button_import.Click -= LoadFromWPTH;

                if (import_current != null)
                    import_current.Click -= LoadFromCurrent;

                if (import_defaultWindows != null)
                    import_defaultWindows.Click -= LoadFromDefault;

                if (import_scheme_12 != null)
                    import_scheme_12.Click -= ImportWin12Preset;

                if (import_scheme_11 != null)
                    import_scheme_11.Click -= ImportWin11Preset;

                if (import_scheme_10 != null)
                    import_scheme_10.Click -= ImportWin10Preset;

                if (import_scheme_81 != null)
                    import_scheme_81.Click -= ImportWin81Preset;

                if (import_scheme_8 != null)
                    import_scheme_8.Click -= ImportWin8Preset;

                if (import_scheme_7 != null)
                    import_scheme_7.Click -= ImportWin7Preset;

                if (import_scheme_Vista != null)
                    import_scheme_Vista.Click -= ImportWinVistaPreset;

                if (import_scheme_XP != null)
                    import_scheme_XP.Click -= ImportWinXPPreset;

                this.FormClosed -= FormClosed;
            };

            this.FormClosed += FormClosed;

            #endregion
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

        private void ExternalTerminal_Shown(object sender, EventArgs e)
        {
            _Shown = true;
        }

        public void FillTerminals(ComboBox ListBox)
        {
            ListBox.Items.Clear();

            foreach (string x in GetSubKeys("HKEY_CURRENT_USER\\Console"))
            {
                bool startupCondition = (x.ToLower() ?? string.Empty) == "%%Startup".ToLower();
                bool systemRootCondition = (x.ToLower() ?? string.Empty) == "%SystemRoot%".ToLower();
                bool cmdExeCondition = (x.ToLower() ?? string.Empty) == "%SystemRoot%_System32_cmd.exe".ToLower();
                bool powershellExeCondition = (x.ToLower() ?? string.Empty) == "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe".ToLower();
                bool sysWow64PowershellCondition = (x.ToLower() ?? string.Empty) == ("%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe".ToLower() ?? string.Empty);

                if (!startupCondition && !systemRootCondition && !cmdExeCondition && !powershellExeCondition && !sysWow64PowershellCondition)
                {
                    ListBox.Items.Add(x);
                }
            }
        }

        public void GetFromExtTerminal(string RegKey)
        {
            if (!GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(RegKey))
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager @default = Default.FromOS(Program.WindowStyle))
            {
                ColorTable00.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable00", @default.CommandPrompt.ColorTable00.Reverse()).Reverse());
                ColorTable01.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable01", @default.CommandPrompt.ColorTable01.Reverse()).Reverse());
                ColorTable02.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable02", @default.CommandPrompt.ColorTable02.Reverse()).Reverse());
                ColorTable03.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable03", @default.CommandPrompt.ColorTable03.Reverse()).Reverse());
                ColorTable04.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable04", @default.CommandPrompt.ColorTable04.Reverse()).Reverse());
                ColorTable05.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable05", @default.CommandPrompt.ColorTable05.Reverse()).Reverse());
                ColorTable06.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable06", @default.CommandPrompt.ColorTable06.Reverse()).Reverse());
                ColorTable07.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable07", @default.CommandPrompt.ColorTable07.Reverse()).Reverse());
                ColorTable08.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable08", @default.CommandPrompt.ColorTable08.Reverse()).Reverse());
                ColorTable09.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable09", @default.CommandPrompt.ColorTable09.Reverse()).Reverse());
                ColorTable10.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable10", @default.CommandPrompt.ColorTable10.Reverse()).Reverse());
                ColorTable11.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable11", @default.CommandPrompt.ColorTable11.Reverse()).Reverse());
                ColorTable12.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable12", @default.CommandPrompt.ColorTable12.Reverse()).Reverse());
                ColorTable13.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable13", @default.CommandPrompt.ColorTable13.Reverse()).Reverse());
                ColorTable14.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable14", @default.CommandPrompt.ColorTable14.Reverse()).Reverse());
                ColorTable15.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable15", @default.CommandPrompt.ColorTable15.Reverse()).Reverse());

                string d = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "PopupColors", Convert.ToInt32($"{@default.CommandPrompt.PopupBackground:X}{@default.CommandPrompt.PopupForeground:X}", 16)).ToString("X");
                if (d.Count() == 1) d = $"{0}{d}";

                CMD_PopupBackgroundBar.Value = Convert.ToInt32(d[0].ToString(), 16);
                CMD_PopupForegroundBar.Value = Convert.ToInt32(d[1].ToString(), 16);

                string dx = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ScreenColors", Convert.ToInt32($"{@default.CommandPrompt.ScreenColorsBackground:X}{@default.CommandPrompt.ScreenColorsForeground:X}", 16)).ToString("X");
                if (dx.Count() == 1) dx = $"{0}{dx}";

                CMD_AccentBackgroundBar.Value = Convert.ToInt32(dx[0].ToString(), 16);
                CMD_AccentForegroundBar.Value = Convert.ToInt32(dx[1].ToString(), 16);

                CMD_CursorSizeBar.Value = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorSize", 25);

                int fw = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontWeight", 400);
                switch (fw)
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

                string FaceName = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FaceName", @default.CommandPrompt.FaceName);

                FaceName = !Fonts.Exists(FaceName) ? @default.CommandPrompt.FaceName : FaceName;

                int fontFamily = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontFamily", !@default.CommandPrompt.FontRaster ? 54 : 1);
                const int TMPF_TRUETYPE = 0x04;

                CMD_RasterToggle.Checked = (fontFamily & TMPF_TRUETYPE) == 0;

                // Optional: always treat "Terminal" as raster bitmap
                if (FaceName.Equals("Terminal", StringComparison.OrdinalIgnoreCase)) CMD_RasterToggle.Checked = true;

                int fontSize = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", (@default.CommandPrompt.PixelHeight << 16) | (0 & 0xFFFF));

                int PixelHeight, PixelWidth;

                if (fontSize == 0 & !CMD_RasterToggle.Checked)
                {
                    PixelHeight = @default.CommandPrompt.PixelHeight;
                    PixelWidth = @default.CommandPrompt.PixelWidth;
                }
                else
                {
                    PixelHeight = (fontSize >> 16) & 0xFFFF;
                    PixelWidth = fontSize & 0xFFFF;
                }

                if (!CMD_RasterToggle.Checked)
                {
                    GDI32.LogFont logFont = new()
                    {
                        lfFaceName = FaceName,
                        lfHeight = -PixelHeight,
                        lfWidth = PixelWidth,
                        lfWeight = fw
                    };

                    F_cmd = Font.FromLogFont(logFont);

                    CMD_FontPxHeight.Value = Math.Abs(logFont.lfHeight);
                }

                FontName.Text = F_cmd.Name;
                FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);

                if (PixelWidth == 4 && PixelHeight == 6) RasterList.SelectedItem = "4x6";
                else if (PixelWidth == 6 && PixelHeight == 8) RasterList.SelectedItem = "6x8";
                else if (PixelWidth == 8 && PixelHeight == 8) RasterList.SelectedItem = "8x8";
                else if (PixelWidth == 16 && PixelHeight == 8) RasterList.SelectedItem = "16x8";
                else if (PixelWidth == 5 && PixelHeight == 12) RasterList.SelectedItem = "5x12";
                else if (PixelWidth == 7 && PixelHeight == 12) RasterList.SelectedItem = "7x12";
                else if (PixelWidth == 8 && PixelHeight == 12) RasterList.SelectedItem = "8x12";
                else if (PixelWidth == 16 && PixelHeight == 12) RasterList.SelectedItem = "16x12";
                else if (PixelWidth == 12 && PixelHeight == 16) RasterList.SelectedItem = "12x16";
                else if (PixelWidth == 10 && PixelHeight == 18) RasterList.SelectedItem = "10x18";
                else RasterList.SelectedItem = "8x12"; // default fallback

                CMD_CursorColor.BackColor = Color.FromArgb(255, ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorColor", @default.CommandPrompt.W10_1909_CursorColor.Reverse()).Reverse());
                CMD_PreviewCUR2.BackColor = CMD_CursorColor.BackColor;
                CMD_CursorStyle.SelectedIndex = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorType", @default.CommandPrompt.W10_1909_CursorType);
                CMD_EnhancedTerminal.Checked = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ForceV2", @default.CommandPrompt.W10_1909_ForceV2);
                CMD_LineSelection.Checked = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "LineSelection", @default.CommandPrompt.W10_1909_LineSelection);
                CMD_TerminalScrolling.Checked = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "TerminalScrolling", @default.CommandPrompt.W10_1909_TerminalScrolling);
                CMD_OpacityBar.Value = ReadReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "WindowAlpha", 255);

                UpdateFromTrack(1);
                UpdateFromTrack(2);
                UpdateFromTrack(3);
                UpdateFromTrack(4);
                ApplyPreview();
            }
        }

        public void SetToExtTerminal(string RegKey)
        {
            try
            {
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "EnableColorSelection", 1);
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable00", Color.FromArgb(0, ColorTable00.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable01", Color.FromArgb(0, ColorTable01.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable02", Color.FromArgb(0, ColorTable02.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable03", Color.FromArgb(0, ColorTable03.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable04", Color.FromArgb(0, ColorTable04.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable05", Color.FromArgb(0, ColorTable05.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable06", Color.FromArgb(0, ColorTable06.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable07", Color.FromArgb(0, ColorTable07.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable08", Color.FromArgb(0, ColorTable08.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable09", Color.FromArgb(0, ColorTable09.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable10", Color.FromArgb(0, ColorTable10.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable11", Color.FromArgb(0, ColorTable11.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable12", Color.FromArgb(0, ColorTable12.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable13", Color.FromArgb(0, ColorTable13.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable14", Color.FromArgb(0, ColorTable14.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable15", Color.FromArgb(0, ColorTable15.BackColor.Reverse()).ToArgb());

                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "PopupColors", Convert.ToInt32($"{CMD_PopupBackgroundBar.Value:X}{CMD_PopupForegroundBar.Value:X}", 16));
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ScreenColors", Convert.ToInt32($"{CMD_AccentBackgroundBar.Value:X}{CMD_AccentForegroundBar.Value:X}", 16));
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorSize", CMD_CursorSizeBar.Value);

                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorColor", Color.FromArgb(0, CMD_CursorColor.BackColor.Reverse()).ToArgb());
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorType", CMD_CursorStyle.SelectedIndex);
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "WindowAlpha", CMD_OpacityBar.Value);
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ForceV2", CMD_EnhancedTerminal.Checked ? 1 : 0);
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "LineSelection", CMD_LineSelection.Checked ? 1 : 0);
                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "TerminalScrolling", CMD_TerminalScrolling.Checked ? 1 : 0);

                WriteReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", FontName.Font.Name, RegistryValueKind.String);

                if (!CMD_RasterToggle.Checked)
                {
                    WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", ((int)CMD_FontPxHeight.Value << 16) | (0 & 0xFFFF));
                }
                else
                {
                    switch (RasterList.SelectedItem)
                    {
                        case "4x6":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 393220);
                                break;
                            }

                        case "6x8":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524294);
                                break;
                            }

                        case "6x9":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524294);
                                break;
                            }

                        case "8x8":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524296);
                                break;
                            }

                        case "8x9":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524296);
                                break;
                            }

                        case "16x8":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524304);
                                break;
                            }

                        case "5x12":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 786437);
                                break;
                            }

                        case "7x12":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 786439);
                                break;
                            }

                        case "8x12":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 0);
                                break;
                            }

                        case "16x12":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 786448);
                                break;
                            }

                        case "12x16":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 1048588);
                                break;
                            }

                        case "10x18":
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 1179658);
                                break;
                            }

                        default:
                            {
                                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 0);
                                break;
                            }

                    }
                }

                if (CMD_RasterToggle.Checked)
                {
                    WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontFamily", 48);
                    WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FaceName", "Terminal", RegistryValueKind.String);
                }
                else
                {
                    WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontFamily", CMD_RasterToggle.Checked ? 1 : 54);
                    WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FaceName", FontName.Font.Name, RegistryValueKind.String);
                }

                WriteReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontWeight", CMD_FontWeightBox.SelectedIndex * 100);

                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_Set, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Forms.BugReport.Throw(ex);
            }
        }

        #region Events

        private void CMD_PopupForegroundBar_Scroll(object sender)
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

        private void CMD_FontWeightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GDI32.LogFont logFont = new();
            F_cmd.ToLogFont(logFont);
            logFont.lfHeight = -(int)CMD_FontPxHeight.Value;
            logFont.lfWidth = 0;
            logFont.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
            F_cmd = Font.FromLogFont(logFont);
            CMD_Preview.Font = F_cmd;

            ApplyPreview();
        }

        private void CMD_CursorStyle_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplyCursorShape();
        }

        #endregion

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
                { colorItem, new string[] { nameof(colorItem.BackColor) } }
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

        #region Methods to modify colors or shapes

        public void ApplyCursorShape()
        {

            CMD_PreviewCursorInner.Dock = DockStyle.Fill;
            CMD_PreviewCUR2.Padding = new(1, 1, 1, 1);

            if (CMD_CursorStyle.SelectedIndex == 0)
            {
                CMD_PreviewCursorInner.BackColor = Color.Transparent;
                CMD_PreviewCUR2.Width = 8;

                int all = CMD_PreviewCUR.Height - 4;
                CMD_PreviewCUR2.Height = (int)(all * (CMD_CursorSizeBar.Value / (float)CMD_CursorSizeBar.Maximum));
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
            CMD_PreviewCUR2.Height = (int)(all * (CMD_CursorSizeBar.Value / (float)CMD_CursorSizeBar.Maximum));
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
            CMD_Preview.CMD_PopupForeground = (int)CMD_PopupForegroundBar.Value;
            CMD_Preview.CMD_PopupBackground = (int)CMD_PopupBackgroundBar.Value;
            CMD_Preview.CMD_ScreenColorsForeground = (int)CMD_AccentForegroundBar.Value;
            CMD_Preview.CMD_ScreenColorsBackground = (int)CMD_AccentBackgroundBar.Value;

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
            CMD_Preview.Font = F_cmd;
            CMD_Preview.Alpha = (int)CMD_OpacityBar.Value;
            CMD_Preview.Refresh();
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

                FontName.Text = F_cmd.Name;
                FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);
            }
        }

        #endregion

        private void Button6_Click(object sender, EventArgs e)
        {
            FillTerminals(ComboBox1);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem != null) GetFromExtTerminal(ComboBox1.SelectedItem?.ToString());
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem is not null)
                SetToExtTerminal(ComboBox1.SelectedItem.ToString());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Forms.NewExtTerminal.ShowDialog();
        }

        private void RasterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                ApplyPreview();
            }
        }

        public void LoadFromTM(Theme.Structures.Console Console)
        {
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

            Console.CursorSize = (int)CMD_CursorSizeBar.Value;
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

        private void Button5_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = F_cmd, FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FontName.Text = dlg.Font.Name;

                    CMD_FontPxHeight.Value = (int)(dlg.Font.Size);

                    GDI32.LogFont logFont = new();
                    dlg.Font.ToLogFont(logFont);
                    logFont.lfHeight = (int)-dlg.Font.Size; // Negative value for pixel height
                    logFont.lfWidth = 0; // Set to 0 for default width
                    logFont.lfWeight = CMD_FontWeightBox.SelectedIndex * 100;
                    F_cmd = Font.FromLogFont(logFont);
                    CMD_Preview.Font = F_cmd;

                    FontName.Font = new(F_cmd.Name, 9f, F_cmd.Style);
                }
            }
        }

        private void CMD_CursorSizeBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurPreview();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupBox73.Visible = ComboBox1.SelectedItem != null;
            TabControl1.Visible = ComboBox1.SelectedItem != null;
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            Program.ToolTip.ToolTipText = Program.Localization.Strings.Aspects.Consoles.CMD_NotAllWeights;
            Program.ToolTip.ToolTipTitle = Program.Localization.Strings.General.Tip;
            Program.ToolTip.Image = Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        private void pin_button_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
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


        private void toggle1_CheckedChanged_1(object sender, EventArgs e)
        {
            Program.Settings.WindowsTerminals.ListAllFonts = toggle1.Checked;
            Program.Settings.WindowsTerminals.Save();
        }

        private void CMD_RasterToggle_CheckedChanged(object sender, EventArgs e)
        {
            Button5.Enabled = !CMD_RasterToggle.Checked;
            CMD_FontWeightBox.Enabled = !CMD_RasterToggle.Checked;

            groupBox3.Enabled = !CMD_RasterToggle.Checked;
            RasterList.Visible = CMD_RasterToggle.Checked;

            ApplyPreview();
        }

        private void CMD_FontPxHeight_ValueChanged(object sender, EventArgs e)
        {
            GDI32.LogFont logFont = new();
            F_cmd.ToLogFont(logFont);
            logFont.lfHeight = -(int)CMD_FontPxHeight.Value;
            logFont.lfWidth = 0; // Set to 0 for default width
            F_cmd = Font.FromLogFont(logFont);
            CMD_Preview.Font = F_cmd;
            ApplyPreview();
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

        private void CMD_OpacityBar_ValueChanged(object sender, EventArgs e)
        {
            CMD_Preview.Alpha = (int)(sender as TrackBarX).Value;
        }

        private void ColorTable00_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            ApplyPreview();
            UpdateFromTrack(1);
            UpdateFromTrack(2);
            UpdateFromTrack(3);
            UpdateFromTrack(4);
        }

        private void CMD_CursorColor_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            CMD_PreviewCUR2.BackColor = e.Color;
        }
    }
}