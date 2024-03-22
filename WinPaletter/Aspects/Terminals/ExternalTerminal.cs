using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class ExternalTerminal
    {
        private bool _Shown = false;
        private Font f_extterminal = new("Consolas", 18f, FontStyle.Regular);

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
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (System.Windows.Forms.OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Filter_OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                        ApplyPreview();
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            LoadFromTM(TMx);
            ApplyPreview();
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Manager _Def = Theme.Default.Get(Program.WindowStyle))
            {
                LoadFromTM(_Def);
                ApplyPreview();
            }
        }

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W11)) { LoadFromTM(TMx); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W10)) { LoadFromTM(TMx); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W81)) { LoadFromTM(TMx); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W7)) { LoadFromTM(TMx); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WVista)) { LoadFromTM(TMx); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == null || (ComboBox1.SelectedItem != null && !GetSubKeys("HKEY_CURRENT_USER\\Console").Contains(ComboBox1.SelectedItem.ToString())))
            {
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WXP)) { LoadFromTM(TMx); }
        }

        private void ExternalTerminal_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_command_line;
            this.LoadLanguage();
            ApplyStyle(this);
            _Shown = false;
            FillTerminals(ComboBox1);
            RasterList.BringToFront();

            ExtTerminal_PopupForegroundLbl.Font = Fonts.Console;
            ExtTerminal_PopupBackgroundLbl.Font = Fonts.Console;
            ExtTerminal_AccentForegroundLbl.Font = Fonts.Console;
            ExtTerminal_AccentBackgroundLbl.Font = Fonts.Console;

            toggle1.Checked = Program.Settings.WindowsTerminals.ListAllFonts;

            IEnumerable<UI.WP.Button> buttons = flowLayoutPanel1.Controls.OfType<UI.WP.Button>();
            UI.WP.Button button_import = buttons.Where(b => b.Name.StartsWith("btn_import")).FirstOrDefault() ?? null;
            button_import.Text = Program.Lang.Designer_import_wpth;

            #region Menu _data
            button_import.Menu.Items.Clear();

            ToolStripMenuItem import_current = new();
            ToolStripMenuItem import_defaultWindows = new();
            ToolStripMenuItem import_scheme = new();
            //ToolStripMenuItem import_scheme_12 = new();
            ToolStripMenuItem import_scheme_11 = new();
            ToolStripMenuItem import_scheme_10 = new();
            ToolStripMenuItem import_scheme_81 = new();
            ToolStripMenuItem import_scheme_7 = new();
            ToolStripMenuItem import_scheme_Vista = new();
            ToolStripMenuItem import_scheme_XP = new();

            import_current.Text = Program.Lang.Designer_import_current;
            import_defaultWindows.Text = Program.Lang.Designer_import_defaultWindows;
            import_scheme.Text = Program.Lang.Designer_import_preset;
            import_scheme_11.Text = Program.Lang.OS_Win11;
            import_scheme_10.Text = Program.Lang.OS_Win10;
            import_scheme_81.Text = Program.Lang.OS_Win81;
            import_scheme_7.Text = Program.Lang.OS_Win7;
            import_scheme_Vista.Text = Program.Lang.OS_WinVista;
            import_scheme_XP.Text = Program.Lang.OS_WinXP;
            //import_scheme_12.Text = Program.Lang.OS_Win12;

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
            //import_scheme_12.Image = WinLogos.add_win12_20px;
            import_scheme_11.Image = WinLogos.Add_Win11_20px;
            import_scheme_10.Image = WinLogos.Add_Win10_20px;
            import_scheme_81.Image = WinLogos.Add_Win81_20px;
            import_scheme_7.Image = WinLogos.Add_Win7_20px;
            import_scheme_Vista.Image = WinLogos.Add_WinVista_20px;
            import_scheme_XP.Image = WinLogos.Add_WinXP_20px;

            import_scheme.DropDown.Renderer = button_import.Menu.Renderer;
            import_scheme.DropDown.Items.Add(import_scheme_11);
            import_scheme.DropDown.Items.Add(import_scheme_10);
            import_scheme.DropDown.Items.Add(import_scheme_81);
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

            if (import_scheme_11 != null)
                import_scheme_11.Click += ImportWin11Preset;

            if (import_scheme_10 != null)
                import_scheme_10.Click += ImportWin10Preset;

            if (import_scheme_81 != null)
                import_scheme_81.Click += ImportWin81Preset;

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

                if (import_scheme_11 != null)
                    import_scheme_11.Click -= ImportWin11Preset;

                if (import_scheme_10 != null)
                    import_scheme_10.Click -= ImportWin10Preset;

                if (import_scheme_81 != null)
                    import_scheme_81.Click -= ImportWin81Preset;

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
                MsgBox(Program.Lang.ExtTer_NotFound, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object y_cmd;

            using (Manager _Def = Theme.Default.Get(Program.WindowStyle))
            {
                ExtTerminal_ColorTable00.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable00", _Def.CommandPrompt.ColorTable00.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable01.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable01", _Def.CommandPrompt.ColorTable01.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable02.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable02", _Def.CommandPrompt.ColorTable02.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable03.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable03", _Def.CommandPrompt.ColorTable03.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable04.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable04", _Def.CommandPrompt.ColorTable04.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable05.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable05", _Def.CommandPrompt.ColorTable05.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable06.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable06", _Def.CommandPrompt.ColorTable06.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable07.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable07", _Def.CommandPrompt.ColorTable07.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable08.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable08", _Def.CommandPrompt.ColorTable08.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable09.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable09", _Def.CommandPrompt.ColorTable09.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable10.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable10", _Def.CommandPrompt.ColorTable10.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable11.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable11", _Def.CommandPrompt.ColorTable11.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable12.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable12", _Def.CommandPrompt.ColorTable12.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable13.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable13", _Def.CommandPrompt.ColorTable13.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable14.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable14", _Def.CommandPrompt.ColorTable14.Reverse().ToArgb()))).Reverse());
                ExtTerminal_ColorTable15.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable15", _Def.CommandPrompt.ColorTable15.Reverse().ToArgb()))).Reverse());

                y_cmd = GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "PopupColors", Convert.ToInt32($"{_Def.CommandPrompt.PopupBackground:X}{_Def.CommandPrompt.PopupForeground:X}", 16));
                string d = Conversions.ToInteger(y_cmd).ToString("X");
                if (d.Count() == 1)
                    d = $"{0}{d}";
                ExtTerminal_PopupBackgroundBar.Value = Convert.ToInt32(d[0].ToString(), 16);
                ExtTerminal_PopupForegroundBar.Value = Convert.ToInt32(d[1].ToString(), 16);

                y_cmd = GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ScreenColors", Convert.ToInt32($"{_Def.CommandPrompt.ScreenColorsBackground:X}{_Def.CommandPrompt.ScreenColorsForeground:X}", 16));
                string dx = Conversions.ToInteger(y_cmd).ToString("X");
                if (dx.Count() == 1)
                    dx = $"{0}{dx}";
                ExtTerminal_AccentBackgroundBar.Value = Convert.ToInt32(dx[0].ToString(), 16);
                ExtTerminal_AccentForegroundBar.Value = Convert.ToInt32(dx[1].ToString(), 16);

                ExtTerminal_CursorSizeBar.Value = Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorSize", 25));

                int fw = Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontWeight", 400));
                switch (fw)
                {
                    case 0:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 0;
                            break;
                        }

                    case 100:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 1;
                            break;
                        }

                    case 200:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 2;
                            break;
                        }

                    case 300:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 3;
                            break;
                        }

                    case 400:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 4;
                            break;
                        }

                    case 500:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 5;
                            break;
                        }

                    case 600:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 6;
                            break;
                        }

                    case 700:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 7;
                            break;
                        }

                    case 800:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 8;
                            break;
                        }

                    case 900:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 9;
                            break;
                        }

                    default:
                        {
                            ExtTerminal_FontWeightBox.SelectedIndex = 4;
                            break;
                        }
                }

                y_cmd = GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontFamily", !_Def.CommandPrompt.FontRaster ? 54 : 1);
                ExtTerminal_RasterToggle.Checked = (int)y_cmd == 1 | (int)y_cmd == 0 | (int)y_cmd == 48;
                ExtTerminal_RasterToggle.Checked = GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FaceName", "Consolas").ToString().ToLower() == "terminal";

                y_cmd = GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 18 * 65536);
                if ((int)y_cmd == 0 & !ExtTerminal_RasterToggle.Checked)
                    ExtTerminal_FontSizeBar.Value = (int)Math.Round(_Def.CommandPrompt.FontSize / 65536d);
                else
                    ExtTerminal_FontSizeBar.Value = (int)y_cmd / 65536;

                if ((int)y_cmd == 393220)
                    RasterList.SelectedItem = "4x6";
                if ((int)y_cmd == 524294)
                    RasterList.SelectedItem = "6x8";
                if ((int)y_cmd == 524296)
                    RasterList.SelectedItem = "8x8";
                if ((int)y_cmd == 524304)
                    RasterList.SelectedItem = "16x8";
                if ((int)y_cmd == 786437)
                    RasterList.SelectedItem = "5x12";
                if ((int)y_cmd == 786439)
                    RasterList.SelectedItem = "7x12";
                if ((int)y_cmd == 0)
                    RasterList.SelectedItem = "8x12";
                if ((int)y_cmd == 786448)
                    RasterList.SelectedItem = "16x12";
                if ((int)y_cmd == 1048588)
                    RasterList.SelectedItem = "12x16";
                if ((int)y_cmd == 1179658)
                    RasterList.SelectedItem = "10x18";
                if (RasterList.SelectedItem.ToString() == null)
                    RasterList.SelectedItem = "8x12";

                y_cmd = GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FaceName", "Consolas");
                if (Fonts.Exists(y_cmd.ToString()))
                {
                    if (!ExtTerminal_RasterToggle.Checked)
                    {
                        using (Font temp = Font.FromLogFont(new NativeMethods.GDI32.LogFont() { lfFaceName = y_cmd.ToString(), lfWeight = fw }))
                        {
                            f_extterminal = new(temp.FontFamily, ExtTerminal_FontSizeBar.Value, temp.Style);
                        }
                        FontName.Text = f_extterminal.Name;
                        FontName.Font = new(f_extterminal.Name, 9f, f_extterminal.Style);
                    }
                }
                else
                {
                    FontName.Text = "Consolas";
                    FontName.Font = new("Consolas", 9f, f_extterminal.Style);
                }

                ExtTerminal_CursorColor.BackColor = Color.FromArgb(255, Color.FromArgb(Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorColor", _Def.CommandPrompt.W10_1909_CursorColor.Reverse().ToArgb()))).Reverse());
                ExtTerminal_PreviewCUR2.BackColor = ExtTerminal_CursorColor.BackColor;
                ExtTerminal_CursorStyle.SelectedIndex = Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorType", _Def.CommandPrompt.W10_1909_CursorType));
                ExtTerminal_EnhancedTerminal.Checked = Conversions.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ForceV2", _Def.CommandPrompt.W10_1909_ForceV2));
                ExtTerminal_LineSelection.Checked = Conversions.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "LineSelection", _Def.CommandPrompt.W10_1909_LineSelection));
                ExtTerminal_TerminalScrolling.Checked = Conversions.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "TerminalScrolling", _Def.CommandPrompt.W10_1909_TerminalScrolling));
                ExtTerminal_OpacityBar.Value = Conversions.ToInteger(GetReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "WindowAlpha", 100));

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
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "EnableColorSelection", 1);
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable00", Color.FromArgb(0, ExtTerminal_ColorTable00.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable01", Color.FromArgb(0, ExtTerminal_ColorTable01.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable02", Color.FromArgb(0, ExtTerminal_ColorTable02.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable03", Color.FromArgb(0, ExtTerminal_ColorTable03.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable04", Color.FromArgb(0, ExtTerminal_ColorTable04.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable05", Color.FromArgb(0, ExtTerminal_ColorTable05.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable06", Color.FromArgb(0, ExtTerminal_ColorTable06.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable07", Color.FromArgb(0, ExtTerminal_ColorTable07.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable08", Color.FromArgb(0, ExtTerminal_ColorTable08.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable09", Color.FromArgb(0, ExtTerminal_ColorTable09.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable10", Color.FromArgb(0, ExtTerminal_ColorTable10.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable11", Color.FromArgb(0, ExtTerminal_ColorTable11.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable12", Color.FromArgb(0, ExtTerminal_ColorTable12.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable13", Color.FromArgb(0, ExtTerminal_ColorTable13.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable14", Color.FromArgb(0, ExtTerminal_ColorTable14.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ColorTable15", Color.FromArgb(0, ExtTerminal_ColorTable15.BackColor.Reverse()).ToArgb());

                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "PopupColors", Convert.ToInt32($"{ExtTerminal_PopupBackgroundBar.Value:X}{ExtTerminal_PopupForegroundBar.Value:X}", 16));
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ScreenColors", Convert.ToInt32($"{ExtTerminal_AccentBackgroundBar.Value:X}{ExtTerminal_AccentForegroundBar.Value:X}", 16));
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorSize", ExtTerminal_CursorSizeBar.Value);

                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorColor", Color.FromArgb(0, ExtTerminal_CursorColor.BackColor.Reverse()).ToArgb());
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "CursorType", ExtTerminal_CursorStyle.SelectedIndex);
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "WindowAlpha", ExtTerminal_OpacityBar.Value);
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "ForceV2", ExtTerminal_EnhancedTerminal.Checked ? 1 : 0);
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "LineSelection", ExtTerminal_LineSelection.Checked ? 1 : 0);
                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "TerminalScrolling", ExtTerminal_TerminalScrolling.Checked ? 1 : 0);

                EditReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", FontName.Font.Name, RegistryValueKind.String);

                if (!ExtTerminal_RasterToggle.Checked)
                {
                    EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", ExtTerminal_FontSizeBar.Value * 65536);
                }
                else
                {
                    switch (RasterList.SelectedItem)
                    {
                        case "4x6":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 393220);
                                break;
                            }

                        case "6x8":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524294);
                                break;
                            }

                        case "6x9":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524294);
                                break;
                            }

                        case "8x8":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524296);
                                break;
                            }

                        case "8x9":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524296);
                                break;
                            }

                        case "16x8":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 524304);
                                break;
                            }

                        case "5x12":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 786437);
                                break;
                            }

                        case "7x12":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 786439);
                                break;
                            }

                        case "8x12":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 0);
                                break;
                            }

                        case "16x12":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 786448);
                                break;
                            }

                        case "12x16":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 1048588);
                                break;
                            }

                        case "10x18":
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 1179658);
                                break;
                            }

                        default:
                            {
                                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontSize", 0);
                                break;
                            }

                    }
                }

                if (ExtTerminal_RasterToggle.Checked)
                {
                    EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontFamily", 48);
                    EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FaceName", "Terminal", RegistryValueKind.String);
                }
                else
                {
                    EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontFamily", ExtTerminal_RasterToggle.Checked ? 1 : 54);
                    EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FaceName", FontName.Font.Name, RegistryValueKind.String);
                }



                EditReg($@"HKEY_CURRENT_USER\Console\{RegKey}", "FontWeight", ExtTerminal_FontWeightBox.SelectedIndex * 100);

                MsgBox(Program.Lang.ExtTer_Set, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        #region Events

        private void ExtTerminal_PopupForegroundBar_Scroll(object sender)
        {
            {
                UI.WP.TrackBar temp = ExtTerminal_PopupForegroundBar;
                ExtTerminal_PopupForegroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    ExtTerminal_PopupForegroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    ExtTerminal_PopupForegroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    ExtTerminal_PopupForegroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    ExtTerminal_PopupForegroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    ExtTerminal_PopupForegroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    ExtTerminal_PopupForegroundLbl.Text += " (F)";
            }

            UpdateFromTrack(1);
            ApplyPreview();
        }

        private void ExtTerminal_PopupBackgroundBar_Scroll(object sender)
        {
            {
                UI.WP.TrackBar temp = ExtTerminal_PopupBackgroundBar;
                ExtTerminal_PopupBackgroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    ExtTerminal_PopupBackgroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    ExtTerminal_PopupBackgroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    ExtTerminal_PopupBackgroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    ExtTerminal_PopupBackgroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    ExtTerminal_PopupBackgroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    ExtTerminal_PopupBackgroundLbl.Text += " (F)";
            }

            UpdateFromTrack(2);
            ApplyPreview();
        }

        private void ExtTerminal_AccentForegroundBar_Scroll(object sender)
        {
            {
                UI.WP.TrackBar temp = ExtTerminal_AccentForegroundBar;
                ExtTerminal_AccentForegroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    ExtTerminal_AccentForegroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    ExtTerminal_AccentForegroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    ExtTerminal_AccentForegroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    ExtTerminal_AccentForegroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    ExtTerminal_AccentForegroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    ExtTerminal_AccentForegroundLbl.Text += " (F)";
            }

            UpdateFromTrack(3);
            UpdateFromTrack(4);
            ApplyPreview();
        }

        private void ExtTerminal_AccentBackgroundBar_Scroll(object sender)
        {
            {
                UI.WP.TrackBar temp = ExtTerminal_AccentBackgroundBar;
                ExtTerminal_AccentBackgroundLbl.Text = temp.Value.ToString();
                if (temp.Value == 10)
                    ExtTerminal_AccentBackgroundLbl.Text += " (A)";
                if (temp.Value == 11)
                    ExtTerminal_AccentBackgroundLbl.Text += " (B)";
                if (temp.Value == 12)
                    ExtTerminal_AccentBackgroundLbl.Text += " (C)";
                if (temp.Value == 13)
                    ExtTerminal_AccentBackgroundLbl.Text += " (D)";
                if (temp.Value == 14)
                    ExtTerminal_AccentBackgroundLbl.Text += " (E)";
                if (temp.Value == 15)
                    ExtTerminal_AccentBackgroundLbl.Text += " (F)";
            }

            UpdateFromTrack(3);
            UpdateFromTrack(4);
            ApplyPreview();
        }

        private void ExtTerminal_RasterToggle_CheckedChanged(object sender, EventArgs e)
        {
            Button5.Enabled = !ExtTerminal_RasterToggle.Checked;
            ExtTerminal_FontWeightBox.Enabled = !ExtTerminal_RasterToggle.Checked;

            if (_Shown)
            {
                RasterList.Visible = ExtTerminal_RasterToggle.Checked;
                ApplyPreview();
            }
        }

        private void ExtTerminal_FontWeightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            NativeMethods.GDI32.LogFont fx = new();
            f_extterminal = new(FontName.Font.Name, f_extterminal.Size, f_extterminal.Style);
            f_extterminal.ToLogFont(fx);
            fx.lfWeight = ExtTerminal_FontWeightBox.SelectedIndex * 100;

            using (Font temp = Font.FromLogFont(fx))
            {
                f_extterminal = new(temp.Name, f_extterminal.Size, temp.Style);
            }
            FontName.Text = f_extterminal.Name;
            ApplyPreview();
        }

        private void ExtTerminal_CursorStyle_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplyCursorShape();
        }

        private void ExtTerminal_CursorColor_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                ExtTerminal_PreviewCUR2.BackColor = ExtTerminal_CursorColor.BackColor;
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { ExtTerminal_PreviewCUR2, new string[] { nameof(ExtTerminal_PreviewCUR2.BackColor) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
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
                { colorItem, new string[] { nameof(colorItem.BackColor) } }
            };

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable00".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable00) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable01".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable01) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable02".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable02) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable03".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable03) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable04".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable04) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable05".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable05) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable06".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable06) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable07".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable07) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable08".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable08) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable09".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable09) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable10".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable10) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable11".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable11) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable12".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable12) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable13".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable13) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable14".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable14) });

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable15".ToLower()))
                CList.Add(CMD4, new string[] { nameof(CMD4.CMD_ColorTable15) });


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

            ExtTerminal_PreviewCursorInner.Dock = DockStyle.Fill;
            ExtTerminal_PreviewCUR2.Padding = new(1, 1, 1, 1);

            if (ExtTerminal_CursorStyle.SelectedIndex == 0)
            {
                ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent;
                ExtTerminal_PreviewCUR2.Width = 8;

                int all = ExtTerminal_PreviewCUR.Height - 4;
                ExtTerminal_PreviewCUR2.Height = (int)Math.Round(all * (ExtTerminal_CursorSizeBar.Value / (double)ExtTerminal_CursorSizeBar.Maximum));
                ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2;
            }

            if (ExtTerminal_CursorStyle.SelectedIndex == 1)
            {
                ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent;

                ExtTerminal_PreviewCUR2.Width = 1;

                int all = ExtTerminal_PreviewCUR.Height - 4;
                ExtTerminal_PreviewCUR2.Height = all;
                ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2;
            }

            if (ExtTerminal_CursorStyle.SelectedIndex == 2)
            {
                ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent;

                ExtTerminal_PreviewCUR2.Width = 10;
                ExtTerminal_PreviewCUR2.Height = 1;

                ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2;
            }

            if (ExtTerminal_CursorStyle.SelectedIndex == 3)
            {
                ExtTerminal_PreviewCursorInner.BackColor = ExtTerminal_PreviewCUR.BackColor;

                ExtTerminal_PreviewCUR2.Width = 8;

                int all = ExtTerminal_PreviewCUR.Height - 4;
                ExtTerminal_PreviewCUR2.Height = all;
                ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2;
            }

            if (ExtTerminal_CursorStyle.SelectedIndex == 4)
            {
                ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent;

                ExtTerminal_PreviewCUR2.Width = 8;

                int all = ExtTerminal_PreviewCUR.Height - 4;
                ExtTerminal_PreviewCUR2.Height = all;
                ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2;
            }

            if (ExtTerminal_CursorStyle.SelectedIndex == 5)
            {
                ExtTerminal_PreviewCursorInner.Dock = DockStyle.None;
                ExtTerminal_PreviewCUR2.Padding = new(0, 0, 0, 0);
                ExtTerminal_PreviewCursorInner.Width = ExtTerminal_PreviewCUR2.Width;
                ExtTerminal_PreviewCursorInner.Height = 1;
                ExtTerminal_PreviewCursorInner.BackColor = ExtTerminal_PreviewCUR.BackColor;
                ExtTerminal_PreviewCursorInner.Top = 1;
                ExtTerminal_PreviewCursorInner.Left = 0;
                ExtTerminal_PreviewCUR2.Width = 8;
                ExtTerminal_PreviewCUR2.Height = 3;
                ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2;
            }
        }

        public void UpdateCurPreview()
        {
            int all = ExtTerminal_PreviewCUR.Height - 4;
            ExtTerminal_PreviewCUR2.Height = (int)Math.Round(all * (ExtTerminal_CursorSizeBar.Value / (double)ExtTerminal_CursorSizeBar.Maximum));
            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2;
            ApplyCursorShape();
        }

        public void ApplyPreview()
        {
            CMD4.CMD_ColorTable00 = ExtTerminal_ColorTable00.BackColor;
            CMD4.CMD_ColorTable01 = ExtTerminal_ColorTable01.BackColor;
            CMD4.CMD_ColorTable02 = ExtTerminal_ColorTable02.BackColor;
            CMD4.CMD_ColorTable03 = ExtTerminal_ColorTable03.BackColor;
            CMD4.CMD_ColorTable04 = ExtTerminal_ColorTable04.BackColor;
            CMD4.CMD_ColorTable05 = ExtTerminal_ColorTable05.BackColor;
            CMD4.CMD_ColorTable06 = ExtTerminal_ColorTable06.BackColor;
            CMD4.CMD_ColorTable07 = ExtTerminal_ColorTable07.BackColor;
            CMD4.CMD_ColorTable08 = ExtTerminal_ColorTable08.BackColor;
            CMD4.CMD_ColorTable09 = ExtTerminal_ColorTable09.BackColor;
            CMD4.CMD_ColorTable10 = ExtTerminal_ColorTable10.BackColor;
            CMD4.CMD_ColorTable11 = ExtTerminal_ColorTable11.BackColor;
            CMD4.CMD_ColorTable12 = ExtTerminal_ColorTable12.BackColor;
            CMD4.CMD_ColorTable13 = ExtTerminal_ColorTable13.BackColor;
            CMD4.CMD_ColorTable14 = ExtTerminal_ColorTable14.BackColor;
            CMD4.CMD_ColorTable15 = ExtTerminal_ColorTable15.BackColor;
            CMD4.CMD_PopupForeground = ExtTerminal_PopupForegroundBar.Value;
            CMD4.CMD_PopupBackground = ExtTerminal_PopupBackgroundBar.Value;
            CMD4.CMD_ScreenColorsForeground = ExtTerminal_AccentForegroundBar.Value;
            CMD4.CMD_ScreenColorsBackground = ExtTerminal_AccentBackgroundBar.Value;
            CMD4.Font = new(f_extterminal.Name, f_extterminal.Size, f_extterminal.Style);
            CMD4.Raster = ExtTerminal_RasterToggle.Checked;

            switch (RasterList.SelectedItem)
            {
                case "4x6":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._4x6;
                        break;
                    }

                case "6x8":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._6x8;
                        break;
                    }

                case "6x9":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._6x8;
                        break;
                    }

                case "8x8":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case "8x9":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case "16x8":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x8;
                        break;
                    }

                case "5x12":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._5x12;
                        break;
                    }

                case "7x12":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._7x12;
                        break;
                    }

                case "8x12":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

                case "16x12":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x12;
                        break;
                    }

                case "12x16":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._12x16;
                        break;
                    }

                case "10x18":
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._10x18;
                        break;
                    }

                default:
                    {
                        CMD4.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

            }

            CMD4.Refresh();
        }

        public void UpdateFromTrack(int i)
        {

            if (i == 1)
            {
                switch (ExtTerminal_PopupForegroundBar.Value)
                {
                    case 0:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor;
                            break;
                        }
                    case 1:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = ExtTerminal_PopupForegroundLbl.BackColor.IsDark() ? ExtTerminal_PopupForegroundLbl.BackColor.LightLight() : ExtTerminal_PopupForegroundLbl.BackColor.Dark(0.9f);
                ExtTerminal_PopupForegroundLbl.ForeColor = FC0;
                // ExtTerminal_PopupForegroundBar.AccentColor = ExtTerminal_PopupForegroundLbl.BackColor
                ExtTerminal_PopupForegroundBar.Invalidate();
            }

            else if (i == 2)
            {

                switch (ExtTerminal_PopupBackgroundBar.Value)
                {
                    case 0:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor;
                            break;
                        }
                    case 1:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = ExtTerminal_PopupBackgroundLbl.BackColor.IsDark() ? ExtTerminal_PopupBackgroundLbl.BackColor.LightLight() : ExtTerminal_PopupBackgroundLbl.BackColor.Dark(0.9f);
                ExtTerminal_PopupBackgroundLbl.ForeColor = FC0;
                // ExtTerminal_PopupBackgroundBar.AccentColor = ExtTerminal_PopupBackgroundLbl.BackColor
                ExtTerminal_PopupBackgroundBar.Invalidate();
            }

            else if (i == 3)
            {

                switch (ExtTerminal_AccentBackgroundBar.Value)
                {
                    case 0:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor;
                            break;
                        }
                    case 1:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = ExtTerminal_AccentBackgroundLbl.BackColor.IsDark() ? ExtTerminal_AccentBackgroundLbl.BackColor.LightLight() : ExtTerminal_AccentBackgroundLbl.BackColor.Dark(0.9f);
                ExtTerminal_AccentBackgroundLbl.ForeColor = FC0;
                // ExtTerminal_AccentBackgroundBar.AccentColor = ExtTerminal_AccentBackgroundLbl.BackColor
                ExtTerminal_AccentBackgroundBar.Invalidate();
                ExtTerminal_PreviewCUR.BackColor = ExtTerminal_AccentBackgroundLbl.BackColor;
            }
            else if (i == 4)
            {

                switch (ExtTerminal_AccentForegroundBar.Value)
                {
                    case 0:
                        {
                            if (ExtTerminal_AccentBackgroundBar.Value == ExtTerminal_AccentForegroundBar.Value)
                            {
                                ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor;
                            }
                            else
                            {
                                ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor;
                            }

                            break;
                        }
                    case 1:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor;
                            break;
                        }
                    case 2:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor;
                            break;
                        }
                    case 3:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor;
                            break;
                        }
                    case 4:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor;
                            break;
                        }
                    case 5:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor;
                            break;
                        }
                    case 6:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor;
                            break;
                        }
                    case 7:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor;
                            break;
                        }
                    case 8:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor;
                            break;
                        }
                    case 9:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor;
                            break;
                        }
                    case 10:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor;
                            break;
                        }
                    case 11:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor;
                            break;
                        }
                    case 12:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor;
                            break;
                        }
                    case 13:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor;
                            break;
                        }
                    case 14:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor;
                            break;
                        }
                    case 15:
                        {
                            ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor;
                            break;
                        }
                }

                Color FC0 = ExtTerminal_AccentForegroundLbl.BackColor.IsDark() ? ExtTerminal_AccentForegroundLbl.BackColor.LightLight() : ExtTerminal_AccentForegroundLbl.BackColor.Dark(0.9f);
                ExtTerminal_AccentForegroundLbl.ForeColor = FC0;
                // ExtTerminal_AccentForegroundBar.AccentColor = ExtTerminal_AccentForegroundLbl.BackColor
                ExtTerminal_AccentForegroundBar.Invalidate();

                FontName.Text = f_extterminal.Name;
                FontName.Font = new(f_extterminal.Name, 9f, f_extterminal.Style);
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

        public void LoadFromTM(Theme.Manager TM)
        {
            ExtTerminal_ColorTable00.BackColor = TM.CommandPrompt.ColorTable00;
            ExtTerminal_ColorTable01.BackColor = TM.CommandPrompt.ColorTable01;
            ExtTerminal_ColorTable02.BackColor = TM.CommandPrompt.ColorTable02;
            ExtTerminal_ColorTable03.BackColor = TM.CommandPrompt.ColorTable03;
            ExtTerminal_ColorTable04.BackColor = TM.CommandPrompt.ColorTable04;
            ExtTerminal_ColorTable05.BackColor = TM.CommandPrompt.ColorTable05;
            ExtTerminal_ColorTable06.BackColor = TM.CommandPrompt.ColorTable06;
            ExtTerminal_ColorTable07.BackColor = TM.CommandPrompt.ColorTable07;
            ExtTerminal_ColorTable08.BackColor = TM.CommandPrompt.ColorTable08;
            ExtTerminal_ColorTable09.BackColor = TM.CommandPrompt.ColorTable09;
            ExtTerminal_ColorTable10.BackColor = TM.CommandPrompt.ColorTable10;
            ExtTerminal_ColorTable11.BackColor = TM.CommandPrompt.ColorTable11;
            ExtTerminal_ColorTable12.BackColor = TM.CommandPrompt.ColorTable12;
            ExtTerminal_ColorTable13.BackColor = TM.CommandPrompt.ColorTable13;
            ExtTerminal_ColorTable14.BackColor = TM.CommandPrompt.ColorTable14;
            ExtTerminal_ColorTable15.BackColor = TM.CommandPrompt.ColorTable15;

            ExtTerminal_ColorTable05.DefaultBackColor = Color.FromArgb(136, 23, 152);
            ExtTerminal_ColorTable06.DefaultBackColor = Color.FromArgb(193, 156, 0);

            ExtTerminal_PopupForegroundBar.Value = TM.CommandPrompt.PopupForeground;
            ExtTerminal_PopupBackgroundBar.Value = TM.CommandPrompt.PopupBackground;
            ExtTerminal_AccentForegroundBar.Value = TM.CommandPrompt.ScreenColorsForeground;
            ExtTerminal_AccentBackgroundBar.Value = TM.CommandPrompt.ScreenColorsBackground;
            ExtTerminal_RasterToggle.Checked = TM.CommandPrompt.FontRaster;
            RasterList.Visible = TM.CommandPrompt.FontRaster;

            switch (TM.CommandPrompt.FontWeight)
            {
                case 0:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 0;
                        break;
                    }

                case 100:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 1;
                        break;
                    }

                case 200:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 2;
                        break;
                    }

                case 300:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 3;
                        break;
                    }

                case 400:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 4;
                        break;
                    }

                case 500:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 5;
                        break;
                    }

                case 600:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 6;
                        break;
                    }

                case 700:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 7;
                        break;
                    }

                case 800:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 8;
                        break;
                    }

                case 900:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 9;
                        break;
                    }

                default:
                    {
                        ExtTerminal_FontWeightBox.SelectedIndex = 4;
                        break;
                    }

            }

            if (!TM.CommandPrompt.FontRaster)
            {
                using (Font temp = Font.FromLogFont(new NativeMethods.GDI32.LogFont() { lfFaceName = TM.CommandPrompt.FaceName, lfWeight = TM.CommandPrompt.FontWeight }))
                {
                    f_extterminal = new(temp.FontFamily, (int)Math.Round(TM.CommandPrompt.FontSize / 65536d), temp.Style);
                }
            }

            FontName.Text = f_extterminal.Name;
            FontName.Font = new(f_extterminal.Name, f_extterminal.Size, f_extterminal.Style);
            ExtTerminal_FontSizeBar.Value = (int)Math.Round(f_extterminal.Size);

            if (TM.CommandPrompt.FontSize == 393220)
                RasterList.SelectedItem = "4x6";
            if (TM.CommandPrompt.FontSize == 524294)
                RasterList.SelectedItem = "6x8";
            if (TM.CommandPrompt.FontSize == 524296)
                RasterList.SelectedItem = "8x8";
            if (TM.CommandPrompt.FontSize == 524304)
                RasterList.SelectedItem = "16x8";
            if (TM.CommandPrompt.FontSize == 786437)
                RasterList.SelectedItem = "5x12";
            if (TM.CommandPrompt.FontSize == 786439)
                RasterList.SelectedItem = "7x12";
            if (TM.CommandPrompt.FontSize == 0)
                RasterList.SelectedItem = "8x12";
            if (TM.CommandPrompt.FontSize == 786448)
                RasterList.SelectedItem = "16x12";
            if (TM.CommandPrompt.FontSize == 1048588)
                RasterList.SelectedItem = "12x16";
            if (TM.CommandPrompt.FontSize == 1179658)
                RasterList.SelectedItem = "10x18";
            if (RasterList.SelectedItem == null)
                RasterList.SelectedItem = "8x12";

            TM.CommandPrompt.CursorSize = ExtTerminal_CursorSizeBar.Value;
            if (ExtTerminal_CursorSizeBar.Value > 100)
                ExtTerminal_CursorSizeBar.Value = 100;
            if (ExtTerminal_CursorSizeBar.Value < 20)
                ExtTerminal_CursorSizeBar.Value = 20;
            ExtTerminal_CursorStyle.SelectedIndex = TM.CommandPrompt.W10_1909_CursorType;
            ExtTerminal_CursorColor.BackColor = TM.CommandPrompt.W10_1909_CursorColor;
            ExtTerminal_PreviewCUR2.BackColor = TM.CommandPrompt.W10_1909_CursorColor;
            ExtTerminal_EnhancedTerminal.Checked = TM.CommandPrompt.W10_1909_ForceV2;
            ExtTerminal_OpacityBar.Value = TM.CommandPrompt.W10_1909_WindowAlpha;
            ExtTerminal_LineSelection.Checked = TM.CommandPrompt.W10_1909_LineSelection;
            ExtTerminal_TerminalScrolling.Checked = TM.CommandPrompt.W10_1909_TerminalScrolling;
            ApplyCursorShape();
            UpdateCurPreview();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = f_extterminal, FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    f_extterminal = dlg.Font;
                    FontName.Text = FontName.Font.Name;
                    ExtTerminal_FontSizeBar.Value = (int)Math.Round(dlg.Font.Size);
                    NativeMethods.GDI32.LogFont fx = new();
                    f_extterminal.ToLogFont(fx);
                    fx.lfWeight = ExtTerminal_FontWeightBox.SelectedIndex * 100;
                    using (Font temp = Font.FromLogFont(fx))
                    {
                        f_extterminal = new(temp.Name, f_extterminal.Size, temp.Style);
                    }
                    FontName.Font = new(dlg.Font.Name, 9f, f_extterminal.Style);
                }
            }
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.Settings.WindowsTerminals.ListAllFonts = toggle1.Checked;
                Program.Settings.WindowsTerminals.Save();
            }
        }

        private void CMD_FontSizeBar_ValueChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                f_extterminal = new(f_extterminal.Name, ExtTerminal_FontSizeBar.Value, f_extterminal.Style);
                ApplyPreview();
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
            Program.ToolTip.ToolTipText = Program.Lang.CMD_NotAllWeights;
            Program.ToolTip.ToolTipTitle = Program.Lang.Tip;
            Program.ToolTip.Image = Assets.Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        private void pin_button_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }

        private void ColorTable00_Click(object sender, DragEventArgs e)
        {

        }

        private void ExtTerminal_CursorColor_Click(object sender, DragEventArgs e)
        {

        }
    }
}