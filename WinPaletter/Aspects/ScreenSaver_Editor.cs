using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Theme;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class ScreenSaver_Editor
    {
        private Process Proc;

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.ScreenSaver);
        }

        public ScreenSaver_Editor()
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
                        LoadFromTM(TMx);
                    }
                }
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

        private void LoadFromTHEME(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Themes, Title = Program.Lang.Filter_OpenTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager _Def = Theme.Default.Get(Program.WindowStyle))
                    {
                        GetFromClassicThemeFile(dlg.FileName, _Def.ScreenSaver);
                    }
                }
            }
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

        private void Apply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
                TMx.ScreenSaver.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void ScreenSaver_Editor_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Store_Toggle_ScreenSaver,
                Enabled = Program.TM.ScreenSaver.Enabled,
                Import_theme = true,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = true,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromTHEME = LoadFromTHEME,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromScheme_11 = ImportWin11Preset,
                OnImportFromScheme_10 = ImportWin10Preset,
                OnImportFromScheme_81 = ImportWin81Preset,
                OnImportFromScheme_7 = ImportWin7Preset,
                OnImportFromScheme_Vista = ImportWinVistaPreset,
                OnImportFromScheme_XP = ImportWinXPPreset,
            };

            LoadData(data);

            pnl_preview.DoubleBuffer();
            LoadFromTM(Program.TM);
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            AspectEnabled = TM.ScreenSaver.Enabled;
            TextBox1.Text = TM.ScreenSaver.File;
            trackbarX1.Value = TM.ScreenSaver.TimeOut;
            CheckBox1.Checked = TM.ScreenSaver.IsSecure;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.ScreenSaver.Enabled = AspectEnabled;
            TM.ScreenSaver.File = TextBox1.Text;
            TM.ScreenSaver.TimeOut = trackbarX1.Value;
            TM.ScreenSaver.IsSecure = CheckBox1.Checked;
        }

        public void GetFromClassicThemeFile(string File, Theme.Structures.ScreenSaver _DefaultScrSvr)
        {
            using (INI _ini = new(File))
            {
                TextBox1.Text = _ini.Read("boot", "SCRNSAVE.EXE", _DefaultScrSvr.File).PhrasePath();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                string filename = string.Empty;
                int previewHandle = 0;

                this.Invoke(() =>
                {
                    filename = TextBox1.Text;
                    previewHandle = pnl_preview.Handle.ToInt32();
                });

                if (System.IO.File.Exists(filename) && System.IO.Path.GetExtension(filename).ToUpper() == ".SCR")
                {
                    if (Proc is not null && !Proc.HasExited) Proc.Kill();

                    Proc = Process.GetProcessById(Interaction.Shell($"\"{filename}\" /p {previewHandle}"));
                }
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Screensavers, Title = Program.Lang.Filter_OpenScreensaver })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.FileName;
                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                string filename = string.Empty;
                int previewHandle = 0;

                this.Invoke(() =>
                {
                    filename = TextBox1.Text;
                    previewHandle = pnl_preview.Handle.ToInt32();
                });

                if (System.IO.File.Exists(filename) && System.IO.Path.GetExtension(filename).ToUpper() == ".SCR")
                {
                    if (Proc is not null && !Proc.HasExited) Proc.Kill();

                    Proc = Process.GetProcessById(Interaction.Shell($"\"{filename}\" /p {previewHandle}"));
                }
            });
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Proc is not null && !Proc.HasExited) Proc.Kill();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                string filename = string.Empty;

                this.Invoke(() => { filename = TextBox1.Text; });

                if (System.IO.File.Exists(filename) && System.IO.Path.GetExtension(filename).ToUpper() == ".SCR")
                {
                    if (Proc is not null && !Proc.HasExited) Proc.Kill();

                    Proc = Process.GetProcessById(Interaction.Shell($"\"{filename}\" /s"));
                }
            });
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                string filename = string.Empty;
                int previewHandle = 0;

                this.Invoke(() =>
                {
                    filename = TextBox1.Text;
                    previewHandle = pnl_preview.Handle.ToInt32();
                });

                if (System.IO.File.Exists(filename) && System.IO.Path.GetExtension(filename).ToUpper() == ".SCR")
                {
                    if (Proc is not null && !Proc.HasExited) Proc.Kill();

                    Proc = Process.GetProcessById(Interaction.Shell($"\"{filename}\" /c", AppWinStyle.NormalFocus));
                    Proc.WaitForExit();
                    Proc = Process.GetProcessById(Interaction.Shell($"\"{filename}\" /p {previewHandle}"));
                }
            });
        }

        private void ScreenSaver_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Proc is not null && !Proc.HasExited) Proc.Kill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = Forms.ScreenSavers_List.GetScreenSaver();

            if (!string.IsNullOrEmpty(result)) TextBox1.Text = result;
        }
    }
}