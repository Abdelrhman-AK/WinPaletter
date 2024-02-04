using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class SettingsX
    {
        public bool _External = false;
        public string _File = null;
        private bool Changed = false;

        public SettingsX()
        {
            InitializeComponent();
        }

        public void LoadSettings()
        {
            Settings sets;

            if (!_External)
                sets = Program.Settings;
            else
                sets = new(Settings.Mode.File, _File);

            Read(sets);

            ref Localizer lang = ref Program.Lang;
            Label11.Text = lang.Name;
            Label12.Text = lang.TranslationVersion;
            Label14.Text = $"{lang.AppVer} {Program.Lang.AndBelow}";
            Label19.Text = lang.Lang;
            Label16.Text = lang.LangCode;
            Label22.Text = !lang.RightToLeft ? Program.Lang.Lang_HasLeftToRight : Program.Lang.Lang_HasRightToLeft;

            TextBox3.Text = Program.Settings.Language.File;
        }

        public void Read(Settings Sets)
        {
            toggle6.Checked = Sets.FileTypeManagement.AutoAddExt;
            
            RadioButton1.Checked = Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt;
            RadioButton2.Checked = !Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt;

            toggle8.Checked = Sets.ThemeApplyingBehavior.AutoRestartExplorer;
            toggle13.Checked = Sets.ThemeApplyingBehavior.ResetCursorsToAero;

            toggle1.Checked = Sets.Updates.AutoCheck;
            toggle35.Checked = Sets.Miscellaneous.Win7LivePreview;

            ComboBox2.SelectedIndex = Sets.Updates.Channel == Settings.Structures.Updates.Channels.Stable ? 0 : 1;
            toggle9.Checked = Sets.ThemeApplyingBehavior.ShowSaveConfirmation;
            toggle7.Checked = Sets.FileTypeManagement.CompressThemeFile;

            toggle3.Checked = Sets.Appearance.DarkMode;
            toggle4.Checked = Sets.Appearance.AutoDarkMode;
            toggle5.Checked = Sets.Appearance.ManagedByTheme;

            toggle2.Checked = Sets.Language.Enabled;
            TextBox3.Text = Sets.Language.File;

            toggle20.Checked = Sets.WindowsTerminals.Bypass;
            toggle21.Checked = Sets.WindowsTerminals.ListAllFonts;
            toggle22.Checked = Sets.WindowsTerminals.Path_Deflection;
            TextBox1.Text = Sets.WindowsTerminals.Terminal_Stable_Path;
            TextBox2.Text = Sets.WindowsTerminals.Terminal_Preview_Path;
            toggle14.Checked = Sets.ThemeApplyingBehavior.CMD_OverrideUserPreferences;

            switch (Sets.NerdStats.Type)
            {
                case Settings.Structures.NerdStats.Formats.HEX:
                    {
                        ComboBox3.SelectedIndex = 0;
                        break;
                    }
                case Settings.Structures.NerdStats.Formats.RGB:
                    {
                        ComboBox3.SelectedIndex = 1;
                        break;
                    }
                case Settings.Structures.NerdStats.Formats.HSL:
                    {
                        ComboBox3.SelectedIndex = 2;
                        break;
                    }
                case Settings.Structures.NerdStats.Formats.Dec:
                    {
                        ComboBox3.SelectedIndex = 3;
                        break;
                    }
            }
            toggle25.Checked = Sets.NerdStats.Enabled;
            CheckBox11.Checked = Sets.NerdStats.ShowHexHash;
            toggle27.Checked = Sets.NerdStats.MoreLabelTransparency;
            toggle28.Checked = Sets.NerdStats.UseWindowsMonospacedFont;
            toggle26.Checked = Sets.NerdStats.DotDefaultChangedIndicator;
            toggle30.Checked = Sets.NerdStats.Classic_Color_Picker;

            switch (Sets.ThemeLog.VerboseLevel)
            {
                case Settings.Structures.ThemeLog.VerboseLevels.Basic:
                    {
                        VL1.Checked = true;
                        break;
                    }

                case Settings.Structures.ThemeLog.VerboseLevels.Detailed:
                    {
                        VL2.Checked = true;
                        break;
                    }

                default:
                    {
                        VL0.Checked = true;
                        break;
                    }

            }

            toggle19.Checked = Sets.ThemeLog.CountDown;
            trackBarX1.Value = Sets.ThemeLog.CountDown_Seconds;
            toggle18.Checked = Sets.ThemeLog.ShowSkippedItemsOnDetailedVerbose;

            toggle23.Checked = Sets.ExplorerPatcher.Enabled;
            toggle24.Checked = Sets.ExplorerPatcher.Enabled_Force;
            EP_Start_10.Checked = Sets.ExplorerPatcher.UseStart10;
            EP_Start_11.Checked = !Sets.ExplorerPatcher.UseStart10;
            EP_Start_10_Type.SelectedIndex = (int)Sets.ExplorerPatcher.StartStyle;
            EP_Taskbar_10.Checked = Sets.ExplorerPatcher.UseTaskbar10;
            EP_Taskbar_11.Checked = !Sets.ExplorerPatcher.UseTaskbar10;
            EP_ORB_10.Checked = Sets.ExplorerPatcher.TaskbarButton10;
            EP_ORB_11.Checked = !Sets.ExplorerPatcher.TaskbarButton10;

            RadioButton5.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton6.Checked = !(Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton8.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton10.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            RadioButton9.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;
            RadioButton7.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase;
            toggle10.Checked = Sets.ThemeApplyingBehavior.UPM_HKU_DEFAULT;
            RadioButton12.Checked = Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton11.Checked = !(Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton14.Checked = Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton13.Checked = !(Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton16.Checked = Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton15.Checked = !(Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton18.Checked = Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton17.Checked = !(Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton20.Checked = Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton19.Checked = !(Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton22.Checked = Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton23.Checked = Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;
            RadioButton21.Checked = Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            toggle15.Checked = Sets.ThemeApplyingBehavior.SFC_on_restoring_StartupSound;
            toggle16.Checked = Sets.ThemeApplyingBehavior.Ignore_PE_Modify_Alert;
            RadioButton25.Checked = Sets.ThemeApplyingBehavior.PE_ModifyByDefault;
            RadioButton24.Checked = !Sets.ThemeApplyingBehavior.PE_ModifyByDefault;
            toggle29.Checked = Sets.NerdStats.DragAndDrop;

            Label38.Text = CalcStoreCache().SizeString();
            Label43.Text = CalcThemesResCache().SizeString();
            label2.Text = CalcBackupsCache().SizeString();
            label5.Text = CalcExErrors().SizeString();
            label7.Text = CalcAppCore().SizeString();

            RadioImage1.Checked = Sets.Store.Online_or_Offline;
            RadioImage2.Checked = !Sets.Store.Online_or_Offline;
            ListBox1.Items.Clear();
            foreach (string x in Sets.Store.Online_Repositories)
            {
                if (!string.IsNullOrWhiteSpace(x))
                    ListBox1.Items.Add(x);
            }
            ListBox2.Items.Clear();
            foreach (string x in Sets.Store.Offline_Directories)
            {
                if (!string.IsNullOrWhiteSpace(x))
                    ListBox2.Items.Add(x);
            }

            CheckBox28.Checked = Sets.Store.Search_ThemeNames;
            CheckBox26.Checked = Sets.Store.Search_Descriptions;
            CheckBox27.Checked = Sets.Store.Search_AuthorsNames;
            CheckBox29.Checked = Sets.Store.Offline_SubFolders;
            toggle17.Checked = Sets.Store.ShowTips;

            toggle31.Checked = Sets.BackupTheme.Enabled;
            toggle33.Checked = Sets.BackupTheme.AutoBackupOnAppOpen;
            toggle32.Checked = Sets.BackupTheme.AutoBackupOnApply;
            toggle34.Checked = Sets.BackupTheme.AutoBackupOnThemeLoad;
            textBox4.Text = Sets.BackupTheme.BackupPath;

            toggle11.Checked = Sets.AspectsControl.Enabled;
            checkBox1.Checked = Sets.AspectsControl.WinColors;
            checkBox2.Checked = Sets.AspectsControl.ClassicColors;
            checkBox13.Checked = Sets.AspectsControl.LogonUI;
            checkBox4.Checked = Sets.AspectsControl.Cursors;
            checkBox3.Checked = Sets.AspectsControl.MetricsFonts;
            checkBox5.Checked = Sets.AspectsControl.Wallpaper;
            checkBox6.Checked = Sets.AspectsControl.Consoles;
            checkBox7.Checked = Sets.AspectsControl.WinTerminals;
            checkBox8.Checked = Sets.AspectsControl.Effects;
            checkBox9.Checked = Sets.AspectsControl.Sounds;
            checkBox10.Checked = Sets.AspectsControl.ScreenSaver;
            checkBox12.Checked = Sets.AspectsControl.AltTab;
            checker_mode_advanced.Checked = Sets.AspectsControl.WinColors_Advanced;
            checker_mode_simple.Checked = !Sets.AspectsControl.WinColors_Advanced;
            radioImage4.Checked = Sets.AspectsControl.ClassicColors_Advanced;
            radioImage3.Checked = !Sets.AspectsControl.ClassicColors_Advanced;
            radioImage6.Checked = Sets.AspectsControl.Cursors_Advanced;
            radioImage5.Checked = !Sets.AspectsControl.Cursors_Advanced;
            radioImage8.Checked = Sets.AspectsControl.MetricsFonts_Advanced;
            radioImage7.Checked = !Sets.AspectsControl.MetricsFonts_Advanced;
            radioImage10.Checked = Sets.AspectsControl.Wallpaper_Advanced;
            radioImage9.Checked = !Sets.AspectsControl.Wallpaper_Advanced;
        }

        public void SaveSettings()
        {
            Cursor = Cursors.WaitCursor;

            // Ch = Change
            bool ch_nerd = false;
            bool ch_terminal = false;
            bool ch_lang = false;
            bool ch_appearance = false;
            bool ch_EP = false;
            //bool ch_WPElevator = false;

            {
                ref Settings Settings = ref Program.Settings;
                if (Settings.Appearance.DarkMode != toggle3.Checked)
                    ch_appearance = true;
                if (Settings.Appearance.AutoDarkMode != toggle4.Checked)
                    ch_appearance = true;
                if (Settings.Appearance.ManagedByTheme != toggle5.Checked)
                    ch_appearance = true;

                if (Settings.NerdStats.Enabled != toggle25.Checked)
                    ch_nerd = true;
                if ((int)Settings.NerdStats.Type != ComboBox3.SelectedIndex)
                    ch_nerd = true;
                if (Settings.NerdStats.ShowHexHash != CheckBox11.Checked)
                    ch_nerd = true;
                if (Settings.NerdStats.MoreLabelTransparency != toggle27.Checked)
                    ch_nerd = true;
                if (Settings.NerdStats.UseWindowsMonospacedFont != toggle28.Checked)
                    ch_nerd = true;
                if (Settings.NerdStats.DotDefaultChangedIndicator != toggle26.Checked)
                    ch_nerd = true;

                if (Settings.WindowsTerminals.Path_Deflection != toggle22.Checked)
                    ch_terminal = true;
                if ((Settings.WindowsTerminals.Terminal_Stable_Path ?? string.Empty) != (TextBox1.Text ?? string.Empty))
                    ch_terminal = true;
                if ((Settings.WindowsTerminals.Terminal_Preview_Path ?? string.Empty) != (TextBox2.Text ?? string.Empty))
                    ch_terminal = true;

                if (Settings.Language.Enabled != toggle2.Checked)
                    ch_lang = true;
                if ((Settings.Language.File ?? string.Empty) != (TextBox3.Text ?? string.Empty))
                    ch_lang = true;

                if (Settings.ExplorerPatcher.Enabled != toggle23.Checked)
                    ch_EP = true;
                if (Settings.ExplorerPatcher.Enabled_Force != toggle24.Checked)
                    ch_EP = true;
                if (Settings.ExplorerPatcher.UseStart10 != EP_Start_10.Checked)
                    ch_EP = true;
                if ((int)Settings.ExplorerPatcher.StartStyle != EP_Start_10_Type.SelectedIndex)
                    ch_EP = true;
                if (Settings.ExplorerPatcher.UseTaskbar10 != EP_Taskbar_10.Checked)
                    ch_EP = true;
                if (Settings.ExplorerPatcher.TaskbarButton10 != EP_ORB_10.Checked)
                    ch_EP = true;

                //if (Settings.Miscellaneous.DontUseWPElevatorConsole != checkBox19.Checked)
                //    ch_WPElevator = true;
            }

            Write(Program.Settings, Settings.Mode.Registry);

            if (ch_appearance)
            {
                GetRoundedCorners();
                GetDarkMode();
                ApplyStyle();
            }

            if (ch_nerd)
            {
                for (int ix = Application.OpenForms.Count - 1; ix >= 0; ix -= 1)
                    Application.OpenForms[ix].Refresh();
            }

            if (ch_terminal)
            {
                if (OS.W12 || OS.W11 || OS.W10)
                {
                    string TerDir;
                    string TerPreDir;

                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                    {
                        TerDir = PathsExt.TerminalJSON;
                        TerPreDir = PathsExt.TerminalPreviewJSON;
                    }
                    else
                    {
                        if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                        {
                            TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                        }
                        else
                        {
                            TerDir = PathsExt.TerminalJSON;
                        }

                        if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                        {
                            TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                        }
                        else
                        {
                            TerPreDir = PathsExt.TerminalPreviewJSON;
                        }
                    }


                    if (File.Exists(TerDir))
                    {
                        Program.TM.Terminal = new(TerDir, WinTerminal.Mode.JSONFile);
                    }
                    else
                    {
                        Program.TM.Terminal = new(string.Empty, WinTerminal.Mode.Empty);
                    }

                    if (File.Exists(TerPreDir))
                    {
                        Program.TM.TerminalPreview = new(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                    }
                    else
                    {
                        Program.TM.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                    }
                }

                else
                {
                    Program.TM.Terminal = new(string.Empty, WinTerminal.Mode.Empty);
                    Program.TM.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                }
            }

            if (ch_lang)
            {
                if (toggle2.Checked)
                {
                    Program.Lang = new();
                    Program.Lang.Load(Program.Settings.Language.File);
                    foreach (Form f in Application.OpenForms) f.LoadLanguage();
                }
                else
                {
                    MsgBox(Program.Lang.LanguageRestart, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (ch_EP)
            {
                Program.EP = new();
                Forms.Home.LoadFromTM(Program.TM);
            }

            Cursor = Cursors.Default;

            MsgBox(Program.Lang.SettingsSaved, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Write(Settings Sets, Settings.Mode Mode, string File = "")
        {
            Sets.FileTypeManagement.AutoAddExt = toggle6.Checked;
            Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt = RadioButton1.Checked;
            Sets.ThemeApplyingBehavior.AutoRestartExplorer = toggle8.Checked;
            Sets.ThemeApplyingBehavior.ResetCursorsToAero = toggle13.Checked;

            Sets.Updates.AutoCheck = toggle1.Checked;
            Sets.Miscellaneous.Win7LivePreview = toggle35.Checked;
            Sets.Updates.Channel = (Settings.Structures.Updates.Channels)ComboBox2.SelectedIndex;

            Sets.Appearance.DarkMode = toggle3.Checked;
            Sets.Appearance.AutoDarkMode = toggle4.Checked;
            Sets.Appearance.ManagedByTheme = toggle5.Checked;

            Sets.ThemeApplyingBehavior.ShowSaveConfirmation = toggle9.Checked;
            Sets.FileTypeManagement.CompressThemeFile = toggle7.Checked;

            Sets.Language.Enabled = toggle2.Checked;
            Sets.Language.File = TextBox3.Text;
            Sets.NerdStats.Enabled = toggle25.Checked;
            Sets.NerdStats.Type = (Settings.Structures.NerdStats.Formats)ComboBox3.SelectedIndex;
            Sets.NerdStats.ShowHexHash = CheckBox11.Checked;
            Sets.NerdStats.MoreLabelTransparency = toggle27.Checked;
            Sets.NerdStats.UseWindowsMonospacedFont = toggle28.Checked;
            Sets.NerdStats.DotDefaultChangedIndicator = toggle26.Checked;
            Sets.NerdStats.DragAndDrop = toggle29.Checked;
            Sets.NerdStats.Classic_Color_Picker = toggle30.Checked;

            Sets.WindowsTerminals.Bypass = toggle20.Checked;
            Sets.WindowsTerminals.ListAllFonts = toggle21.Checked;
            Sets.WindowsTerminals.Path_Deflection = toggle22.Checked;
            Sets.WindowsTerminals.Terminal_Stable_Path = TextBox1.Text;
            Sets.WindowsTerminals.Terminal_Preview_Path = TextBox2.Text;
            Sets.ThemeApplyingBehavior.CMD_OverrideUserPreferences = toggle14.Checked;

            if (VL0.Checked)
                Sets.ThemeLog.VerboseLevel = Settings.Structures.ThemeLog.VerboseLevels.None;

            if (VL1.Checked)
                Sets.ThemeLog.VerboseLevel = Settings.Structures.ThemeLog.VerboseLevels.Basic;

            if (VL2.Checked)
                Sets.ThemeLog.VerboseLevel = Settings.Structures.ThemeLog.VerboseLevels.Detailed;

            Sets.ThemeLog.CountDown = toggle19.Checked;
            Sets.ThemeLog.CountDown_Seconds = trackBarX1.Value;
            Sets.ThemeLog.ShowSkippedItemsOnDetailedVerbose = toggle18.Checked;

            Sets.ExplorerPatcher.Enabled = toggle23.Checked;
            Sets.ExplorerPatcher.Enabled_Force = toggle24.Checked;
            Sets.ExplorerPatcher.UseStart10 = EP_Start_10.Checked;
            Sets.ExplorerPatcher.StartStyle = (ExplorerPatcher.StartStyles)EP_Start_10_Type.SelectedIndex;
            Sets.ExplorerPatcher.UseTaskbar10 = EP_Taskbar_10.Checked;
            Sets.ExplorerPatcher.TaskbarButton10 = EP_ORB_10.Checked;

            Sets.BackupTheme.Enabled = toggle31.Checked;
            Sets.BackupTheme.AutoBackupOnAppOpen = toggle33.Checked;
            Sets.BackupTheme.AutoBackupOnApply = toggle32.Checked;
            Sets.BackupTheme.AutoBackupOnThemeLoad = toggle34.Checked;
            Sets.BackupTheme.BackupPath = textBox4.Text;

            if (RadioButton5.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            if (RadioButton8.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;

            if (RadioButton10.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            if (RadioButton9.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;

            if (RadioButton7.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase;

            Sets.ThemeApplyingBehavior.UPM_HKU_DEFAULT = toggle10.Checked;

            if (RadioButton12.Checked)
                Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            if (RadioButton14.Checked)
                Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            if (RadioButton16.Checked)
                Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            if (RadioButton18.Checked)
                Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            if (RadioButton20.Checked)
                Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            if (RadioButton22.Checked)
                Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;

            if (RadioButton23.Checked)
                Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;

            if (RadioButton21.Checked)
                Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

            Sets.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = toggle15.Checked;
            Sets.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = toggle16.Checked;
            Sets.ThemeApplyingBehavior.PE_ModifyByDefault = RadioButton25.Checked;

            Sets.Store.Online_or_Offline = RadioImage1.Checked;
            Sets.Store.Online_Repositories = ListBox1.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Sets.Store.Offline_Directories = ListBox2.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Sets.Store.Search_ThemeNames = CheckBox28.Checked;
            Sets.Store.Search_Descriptions = CheckBox26.Checked;
            Sets.Store.Search_AuthorsNames = CheckBox27.Checked;
            Sets.Store.Offline_SubFolders = CheckBox29.Checked;
            Sets.Store.ShowTips = toggle17.Checked;

            Sets.AspectsControl.Enabled = toggle11.Checked;
            Sets.AspectsControl.WinColors = checkBox1.Checked;
            Sets.AspectsControl.ClassicColors = checkBox2.Checked;
            Sets.AspectsControl.LogonUI = checkBox13.Checked;
            Sets.AspectsControl.Cursors = checkBox4.Checked;
            Sets.AspectsControl.MetricsFonts = checkBox3.Checked;
            Sets.AspectsControl.Wallpaper = checkBox5.Checked;
            Sets.AspectsControl.Consoles = checkBox6.Checked;
            Sets.AspectsControl.WinTerminals = checkBox7.Checked;
            Sets.AspectsControl.Effects = checkBox8.Checked;
            Sets.AspectsControl.Sounds = checkBox9.Checked;
            Sets.AspectsControl.ScreenSaver = checkBox10.Checked;
            Sets.AspectsControl.AltTab = checkBox12.Checked;
            Sets.AspectsControl.WinColors_Advanced = checker_mode_advanced.Checked;
            Sets.AspectsControl.ClassicColors_Advanced = radioImage4.Checked;
            Sets.AspectsControl.Cursors_Advanced = radioImage6.Checked;
            Sets.AspectsControl.MetricsFonts_Advanced = radioImage8.Checked;
            Sets.AspectsControl.Wallpaper_Advanced = radioImage10.Checked;


            Sets.Save(Mode, File);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Settings NewSets = new(Settings.Mode.Empty);

            Changed = false;

            {
                ref Settings Settings = ref Program.Settings;

                if (Settings.Updates.AutoCheck != toggle1.Checked)
                    Changed = true;
                if (Settings.FileTypeManagement.AutoAddExt != toggle6.Checked)
                    Changed = true;
                if (Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt != RadioButton1.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.AutoRestartExplorer != toggle8.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.ResetCursorsToAero != toggle13.Checked)
                    Changed = true;
                if ((int)Settings.Updates.Channel != ComboBox2.SelectedIndex)
                    Changed = true;
                if (Settings.Miscellaneous.Win7LivePreview != toggle35.Checked)
                    Changed = true;
                if (Settings.NerdStats.Classic_Color_Picker != toggle30.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.ShowSaveConfirmation != toggle9.Checked)
                    Changed = true;
                if (Settings.FileTypeManagement.CompressThemeFile != toggle7.Checked)
                    Changed = true;

                if (Settings.Appearance.DarkMode != toggle3.Checked)
                    Changed = true;
                if (Settings.Appearance.AutoDarkMode != toggle4.Checked)
                    Changed = true;
                if (Settings.Appearance.ManagedByTheme != toggle5.Checked)
                    Changed = true;

                if (Settings.Language.Enabled != toggle2.Checked)
                    Changed = true;
                if ((Settings.Language.File ?? string.Empty) != (TextBox3.Text ?? string.Empty))
                    Changed = true;

                if (Settings.NerdStats.Enabled != toggle25.Checked)
                    Changed = true;
                if ((int)Settings.NerdStats.Type != ComboBox3.SelectedIndex)
                    Changed = true;
                if (Settings.NerdStats.ShowHexHash != CheckBox11.Checked)
                    Changed = true;
                if (Settings.NerdStats.MoreLabelTransparency != toggle27.Checked)
                    Changed = true;
                if (Settings.NerdStats.UseWindowsMonospacedFont != toggle28.Checked)
                    Changed = true;
                if (Settings.NerdStats.DotDefaultChangedIndicator != toggle26.Checked)
                    Changed = true;
                if (Settings.NerdStats.DragAndDrop != toggle29.Checked)
                    Changed = true;

                if (Settings.WindowsTerminals.Bypass != toggle20.Checked)
                    Changed = true;
                if (Settings.WindowsTerminals.ListAllFonts != toggle21.Checked)
                    Changed = true;
                if (Settings.WindowsTerminals.Path_Deflection != toggle22.Checked)
                    Changed = true;
                if ((Settings.WindowsTerminals.Terminal_Stable_Path ?? string.Empty) != (TextBox1.Text ?? string.Empty))
                    Changed = true;
                if ((Settings.WindowsTerminals.Terminal_Preview_Path ?? string.Empty) != (TextBox2.Text ?? string.Empty))
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.CMD_OverrideUserPreferences != toggle14.Checked)
                    Changed = true;

                if (Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.None & !VL0.Checked)
                    Changed = true;
                if (Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Basic & !VL1.Checked)
                    Changed = true;
                if (Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed & !VL2.Checked)
                    Changed = true;
                if (Settings.ThemeLog.CountDown != toggle19.Checked)
                    Changed = true;
                if (Settings.ThemeLog.CountDown_Seconds != trackBarX1.Value)
                    Changed = true;
                if (Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose != toggle18.Checked)
                    Changed = true;

                if (Settings.ExplorerPatcher.Enabled != toggle23.Checked)
                    Changed = true;
                if (Settings.ExplorerPatcher.Enabled_Force != toggle24.Checked)
                    Changed = true;
                if (Settings.ExplorerPatcher.UseStart10 != EP_Start_10.Checked)
                    Changed = true;
                if ((int)Settings.ExplorerPatcher.StartStyle != EP_Start_10_Type.SelectedIndex)
                    Changed = true;
                if (Settings.ExplorerPatcher.UseTaskbar10 != EP_Taskbar_10.Checked)
                    Changed = true;
                if (Settings.ExplorerPatcher.TaskbarButton10 != EP_ORB_10.Checked)
                    Changed = true;

                if (Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton5.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton6.Checked)
                    Changed = true;

                if (Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton8.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton10.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults & !RadioButton9.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase & !RadioButton7.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT != toggle10.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton12.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton11.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton14.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton13.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton16.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton15.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton18.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton17.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton20.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton19.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton22.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults & !RadioButton23.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton21.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound != toggle15.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert != toggle16.Checked)
                    Changed = true;
                if (Settings.ThemeApplyingBehavior.PE_ModifyByDefault != RadioButton25.Checked)
                    Changed = true;

                if (Settings.Store.Online_or_Offline & !RadioImage1.Checked)
                    Changed = true;
                if (!Settings.Store.Online_or_Offline & !RadioImage2.Checked)
                    Changed = true;

                if (Settings.Store.Search_ThemeNames != CheckBox28.Checked)
                    Changed = true;
                if (Settings.Store.Search_Descriptions != CheckBox26.Checked)
                    Changed = true;
                if (Settings.Store.Search_AuthorsNames != CheckBox27.Checked)
                    Changed = true;
                if (Settings.Store.Offline_SubFolders != CheckBox29.Checked)
                    Changed = true;
                if (Settings.Store.ShowTips != toggle17.Checked)
                    Changed = true;

                if (Settings.BackupTheme.Enabled != toggle31.Checked)
                    Changed = true;
                if (Settings.BackupTheme.AutoBackupOnAppOpen != toggle33.Checked)
                    Changed = true;
                if (Settings.BackupTheme.AutoBackupOnApply != toggle32.Checked)
                    Changed = true;
                if (Settings.BackupTheme.AutoBackupOnThemeLoad != toggle34.Checked)
                    Changed = true;
                if (Settings.BackupTheme.BackupPath != textBox4.Text)
                    Changed = true;

                if (Settings.AspectsControl.Enabled != toggle11.Checked)
                    Changed = true;
                if (Settings.AspectsControl.WinColors != checkBox1.Checked)
                    Changed = true;
                if (Settings.AspectsControl.ClassicColors != checkBox2.Checked)
                    Changed = true;
                if (Settings.AspectsControl.LogonUI != checkBox13.Checked)
                    Changed = true;
                if (Settings.AspectsControl.Cursors != checkBox4.Checked)
                    Changed = true;
                if (Settings.AspectsControl.MetricsFonts != checkBox3.Checked)
                    Changed = true;
                if (Settings.AspectsControl.Wallpaper != checkBox5.Checked)
                    Changed = true;
                if (Settings.AspectsControl.Consoles != checkBox6.Checked)
                    Changed = true;
                if (Settings.AspectsControl.WinTerminals != checkBox7.Checked)
                    Changed = true;
                if (Settings.AspectsControl.Effects != checkBox8.Checked)
                    Changed = true;
                if (Settings.AspectsControl.Sounds != checkBox9.Checked)
                    Changed = true;
                if (Settings.AspectsControl.ScreenSaver != checkBox10.Checked)
                    Changed = true;
                if (Settings.AspectsControl.AltTab != checkBox12.Checked)
                    Changed = true;
                if (Settings.AspectsControl.WinColors_Advanced != checker_mode_advanced.Checked)
                    Changed = true;
                if (Settings.AspectsControl.ClassicColors_Advanced != radioImage4.Checked)
                    Changed = true;
                if (Settings.AspectsControl.Cursors_Advanced != radioImage6.Checked)
                    Changed = true;
                if (Settings.AspectsControl.MetricsFonts_Advanced != radioImage8.Checked)
                    Changed = true;
                if (Settings.AspectsControl.Wallpaper_Advanced != radioImage10.Checked)
                    Changed = true;

            }

            if (e.CloseReason == CloseReason.UserClosing & Changed)
            {
                switch (MsgBox(Program.Lang.SaveMsg, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                    case DialogResult.Yes:
                        {
                            SaveSettings();
                            _External = false;
                            _File = null;
                            e.Cancel = false;
                            base.OnFormClosing(e);
                            break;
                        }
                    case DialogResult.No:
                        {
                            _External = false;
                            _File = null;
                            e.Cancel = false;
                            base.OnFormClosing(e);
                            break;
                        }
                }
            }
            else if (e.CloseReason == CloseReason.UserClosing & !Changed)
            {
                e.Cancel = false;
                base.OnFormClosing(e);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsX_Load(object sender, EventArgs e)
        {
            ComboBox2.Items.Clear();
            ComboBox2.Items.Add(Program.Lang.Stable);
            ComboBox2.Items.Add(Program.Lang.Beta);
            this.LoadLanguage();
            ApplyStyle(this);
            LoadSettings();

            int w = 19;
            EP_Start_11.Image = Assets.WinLogos.Win11.Resize(w, w);
            EP_Start_10.Image = Assets.WinLogos.Win10.Resize(w, w);
            EP_Taskbar_11.Image = EP_Start_11.Image;
            EP_Taskbar_10.Image = EP_Start_10.Image;

            if (Program.Style.DarkMode)
            {
                EP_ORB_11.Image = Assets.Win11Preview.StartBtn_11_EP.Resize(w, w);
                EP_ORB_10.Image = Assets.Win10Preview.StartBtn_10Light.Invert().Resize(w, w);
            }
            else
            {
                EP_ORB_11.Image = Assets.Win11Preview.StartBtn_11_EP.Invert().Resize(w, w);
                EP_ORB_10.Image = Assets.Win10Preview.StartBtn_10Light.Resize(w, w);
            }

            if (OS.WXP)
            {
                AlertBox17.Visible = true;
                AlertBox17.Text = string.Format(Program.Lang.UpdatesOSNoTLS12, Program.Lang.OS_WinXP);
            }

            else if (OS.WVista)
            {
                AlertBox17.Visible = true;
                AlertBox17.Text = string.Format(Program.Lang.UpdatesOSNoTLS12, Program.Lang.OS_WinVista);
            }

            Label38.Font = Fonts.ConsoleMedium;
            Label43.Font = Fonts.ConsoleMedium;
        }

        public int CalcStoreCache()
        {
            if (Directory.Exists(PathsExt.StoreCache))
            {
                return (int)Directory.EnumerateFiles(PathsExt.StoreCache, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        public int CalcThemesResCache()
        {
            if (Directory.Exists(PathsExt.ThemeResPackCache))
            {
                return (int)Directory.EnumerateFiles(PathsExt.ThemeResPackCache, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        public int CalcBackupsCache()
        {
            if (Directory.Exists(Program.Settings.BackupTheme.BackupPath))
            {
                return (int)Directory.EnumerateFiles(Program.Settings.BackupTheme.BackupPath, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        public int CalcExErrors()
        {
            if (Directory.Exists($"{PathsExt.appData}\\Reports"))
            {
                return (int)Directory.EnumerateFiles($"{PathsExt.appData}\\Reports", "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        public int CalcAppCore()
        {
            int s0, s1;

            if (Directory.Exists(PathsExt.appData))
            {
                s0 = (int)Directory.EnumerateFiles(PathsExt.appData, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
                s0 -= CalcExErrors() - CalcThemesResCache();
                if (Program.Settings.BackupTheme.BackupPath.StartsWith(PathsExt.appData, StringComparison.OrdinalIgnoreCase)) s0 -= CalcBackupsCache();
            }
            else
            {
                s0 = 0;
            }

            if (Directory.Exists(PathsExt.ProgramFilesData))
            {
                s1 = (int)Directory.EnumerateFiles(PathsExt.ProgramFilesData, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                s1 = 0;
            }

            return s0 + s1;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterSettings, FileName = _File, Title = Program.Lang.Filter_SaveWinPaletterSettings })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Settings sets = new(Settings.Mode.Empty);
                    Write(sets, Settings.Mode.File, dlg.FileName);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterSettings, FileName = _File, Title = Program.Lang.Filter_OpenWinPaletterSettings })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Settings sets = new(Settings.Mode.File, dlg.FileName);
                    Read(sets);
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.RemoveExtMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question, string.Empty, Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.RemoveExtMsgNote) == DialogResult.Yes)
            {
                toggle6.Checked = false;
                Program.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
                Program.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
                Program.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Uninstall);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Lang.Filter_OpenJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox3.Text = dlg.FileName;

                    try
                    {
                        StreamReader _File = new(TextBox3.Text);
                        JObject J = JObject.Parse(_File.ReadToEnd());
                        _File.Close();

                        Label11.Text = J["Information"]["name"].ToString();
                        Label12.Text = J["Information"]["translationversion"].ToString();
                        Label14.Text = $"{J["Information"]["appver"]} {Program.Lang.AndBelow}";
                        Label19.Text = J["Information"]["lang"].ToString();
                        Label16.Text = J["Information"]["langcode"].ToString();
                        Label22.Text = !(bool)J["Information"]["righttoleft"] ? Program.Lang.Lang_HasLeftToRight : Program.Lang.Lang_HasRightToLeft;
                    }
                    catch { }
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Repository}wiki/Language-creation");
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Lang.Filter_OpenJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        TextBox1.Text = dlg.FileName;
                    }
                }
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Lang.Filter_OpenJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        TextBox2.Text = dlg.FileName;
                    }
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Repository}tree/master/Languages");
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Forms.Lang_Dashboard.ShowDialog();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            string inputText = string.Empty;
            if (ListBox1.SelectedItem is not null)
                inputText = ListBox1.SelectedItem.ToString();
            string response = InputBox(Program.Lang.InputThemeRepos, inputText, Program.Lang.InputThemeRepos_Notice);
            if (!ListBox1.Items.Contains(response))
                ListBox1.Items.Add(response);
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem is not null)
            {
                int i = ListBox1.SelectedIndex;

                if (!((ListBox1.SelectedItem.ToString().ToUpper() ?? string.Empty) == (Properties.Resources.Link_StoreReposDB.ToUpper() ?? string.Empty)) & !((ListBox1.SelectedItem.ToString().ToUpper() ?? string.Empty) == (Properties.Resources.Link_StoreMainDB.ToUpper() ?? string.Empty)))
                {
                    ListBox1.Items.RemoveAt(i);
                    if (i < ListBox1.Items.Count - 1)
                        ListBox1.SelectedIndex = i;
                    else
                        ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
                }
                else
                {
                    MsgBox(Program.Lang.Store_RemoveTip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {

            if (!OS.WXP)
            {
                Ookii.Dialogs.WinForms.VistaFolderBrowserDialog dlg = new();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ListBox2.Items.Contains(dlg.SelectedPath))
                        ListBox2.Items.Add(dlg.SelectedPath);
                }
                dlg.Dispose();
            }
            else
            {
                using (FolderBrowserDialog dlg = new())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (!ListBox2.Items.Contains(dlg.SelectedPath)) ListBox2.Items.Add(dlg.SelectedPath);
                    }
                }
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (ListBox2.SelectedItem is not null)
            {
                int i = ListBox2.SelectedIndex;
                ListBox2.Items.RemoveAt(i);
                if (i < ListBox2.Items.Count - 1)
                    ListBox2.SelectedIndex = i;
                else
                    ListBox2.SelectedIndex = ListBox2.Items.Count - 1;
            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(PathsExt.StoreCache, true);
            }
            catch
            {
            }
            Label38.Text = CalcStoreCache().SizeString();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(PathsExt.ThemeResPackCache, true);
            }
            catch
            {
            }
            Label43.Text = CalcThemesResCache().SizeString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Forms.SysEventsSndsInstaller.Install(true);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Forms.SysEventsSndsInstaller.Uninstall();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Close();
            User.Login(true);
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.BackupThemes_List);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists($@"{PathsExt.appData}\Reports"))
            {
                Process.Start($@"{PathsExt.appData}\Reports");
            }
            else
            {
                MsgBox(string.Format(Program.Lang.Bug_NoReport, $@"{PathsExt.appData}\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete($"{PathsExt.appData}\\Reports", true);
            }
            catch
            {
            }
            label5.Text = CalcExErrors().SizeString();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                using (Ookii.Dialogs.WinForms.VistaFolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) textBox4.Text = FD.SelectedPath;
                }
            }
            else
            {
                using (FolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) textBox4.Text = FD.SelectedPath;
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Read(new Settings(Settings.Mode.Empty));
        }

        private void SettingsX_ParentChanged(object sender, EventArgs e)
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
    }
}