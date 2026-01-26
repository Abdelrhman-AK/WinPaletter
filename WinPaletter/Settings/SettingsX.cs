using Newtonsoft.Json.Linq;
using Ookii.Dialogs.WinForms;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Properties;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    /// <summary>
    /// Settings form for WinPaletter
    /// </summary>
    public partial class SettingsX
    {
        /// <summary>
        /// A flag to determine if the settings is to be loaded from a file externally
        /// </summary>
        public bool _External = false;

        /// <summary>
        /// The file to load the settings from
        /// </summary>
        public string _File = null;

        bool skipCheckingEquality = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsX"/> class.
        /// </summary>
        public SettingsX()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load WinPaletter settings
        /// </summary>
        private void LoadSettings()
        {
            Settings sets;

            if (!_External)
                sets = Program.Settings;
            else
                sets = new(Settings.Source.File, _File);

            Read(sets);

            Label11.Text = Program.Localization.Information.Name;
            Label12.Text = Program.Localization.Information.TranslationVersion;
            Label14.Text = $"{Program.Localization.Information.AppVer} {Program.Localization.Strings.General.AndBelow}";
            Label19.Text = Program.Localization.Information.Lang;
            Label16.Text = Program.Localization.Information.LangCode;
            Label22.Text = !Program.Localization.Information.RightToLeft ? Program.Localization.Strings.Languages.LeftToRight : Program.Localization.Strings.Languages.RightToLeft;

            TextBox3.Text = Program.Settings.Language.File;
        }

        /// <summary>
        /// Read settings from the settings object
        /// </summary>
        /// <param name="Sets"></param>
        private void Read(Settings Sets)
        {
            toggle6.Checked = Sets.FileTypeManagement.AutoAddExt;

            RadioButton1.Checked = Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt;
            RadioButton2.Checked = !Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt;

            toggle38.Checked = Sets.ThemeApplyingBehavior.CreateSystemRestore;
            toggle8.Checked = Sets.ThemeApplyingBehavior.AutoRestartExplorer;
            toggle13.Checked = Sets.ThemeApplyingBehavior.ResetCursorsToAero;

            toggle1.Checked = Sets.Updates.AutoCheck;
            toggle35.Checked = Sets.Miscellaneous.Win7LivePreview;
            toggle37.Checked = Sets.Miscellaneous.ShowWelcomeDialog;

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
            ComboBox3.SelectedIndex = (int)Sets.NerdStats.Type;
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

            toggle40.Checked = Sets.AppLog.Enabled;
            toggle41.Checked = Sets.AppLog.Reg;
            checkBox15.Checked = Sets.AppLog.RegRead;
            checkBox17.Checked = Sets.AppLog.RegWrite;
            checkBox18.Checked = Sets.AppLog.RegDelete;
            toggle42.Checked = Sets.AppLog.StatusPanel;

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
            toggle12.Checked = Sets.ThemeApplyingBehavior.Show_WinEffects_Alert;
            toggle29.Checked = Sets.NerdStats.DragAndDrop;

            UpdateStorageUI();

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
            toggle17.Checked = Sets.Store.ShowNewXPIntro;

            toggle31.Checked = Sets.BackupTheme.Enabled;
            toggle33.Checked = Sets.BackupTheme.AutoBackupOnAppOpen;
            toggle32.Checked = Sets.BackupTheme.AutoBackupOnApply;
            toggle36.Checked = Sets.BackupTheme.AutoBackupOnApplySingleAspect;
            toggle34.Checked = Sets.BackupTheme.AutoBackupOnThemeLoad;
            toggle39.Checked = Sets.BackupTheme.AutoBackupOnExError;

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
            checkBox14.Checked = Sets.AspectsControl.Icons;
            checkBox16.Checked = Sets.AspectsControl.Accessibility;

            radioImage4.Checked = Sets.AspectsControl.ClassicColors_Advanced;
            radioImage3.Checked = !Sets.AspectsControl.ClassicColors_Advanced;
            radioImage8.Checked = Sets.AspectsControl.MetricsFonts_Advanced;
            radioImage7.Checked = !Sets.AspectsControl.MetricsFonts_Advanced;

            toggle43.Checked = Sets.UsersServices.GitHub_AutoOperateOnLinkedFiles;
        }

        /// <summary>
        /// Save WinPaletter settings
        /// </summary>
        private void SaveSettings()
        {
            Cursor = Cursors.WaitCursor;

            // ch = Change
            bool ch_nerd = false;
            bool ch_terminal = false;
            bool ch_lang = false;
            bool ch_appearance = false;
            bool ch_EP = false;
            bool ch_logStatusPanel = false;

            // A quick check for settings change
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

                if (Settings.AppLog.StatusPanel != toggle42.Checked)
                    ch_logStatusPanel = true;

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
            }

            Write(Program.Settings, Settings.Source.Registry);

            if (ch_appearance)
            {
                Program.Style.RoundedCorners = GetRoundedCorners();
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
                        TerDir = SysPaths.TerminalJSON;
                        TerPreDir = SysPaths.TerminalPreviewJSON;
                    }
                    else
                    {
                        if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                        {
                            TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                        }
                        else
                        {
                            TerDir = SysPaths.TerminalJSON;
                        }

                        if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                        {
                            TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                        }
                        else
                        {
                            TerPreDir = SysPaths.TerminalPreviewJSON;
                        }
                    }


                    if (File.Exists(TerDir))
                    {
                        Program.TM.Terminal = new(TerDir, WinPaletter.Theme.Structures.WinTerminal.Mode.JSONFile);
                    }
                    else
                    {
                        Program.TM.Terminal = new(string.Empty, WinPaletter.Theme.Structures.WinTerminal.Mode.Empty);
                    }

                    if (File.Exists(TerPreDir))
                    {
                        Program.TM.TerminalPreview = new(TerPreDir, WinPaletter.Theme.Structures.WinTerminal.Mode.JSONFile, WinPaletter.Theme.Structures.WinTerminal.Version.Preview);
                    }
                    else
                    {
                        Program.TM.TerminalPreview = new(string.Empty, WinPaletter.Theme.Structures.WinTerminal.Mode.Empty, WinPaletter.Theme.Structures.WinTerminal.Version.Preview);
                    }
                }

                else
                {
                    Program.TM.Terminal = new(string.Empty, WinPaletter.Theme.Structures.WinTerminal.Mode.Empty);
                    Program.TM.TerminalPreview = new(string.Empty, WinPaletter.Theme.Structures.WinTerminal.Mode.Empty, WinPaletter.Theme.Structures.WinTerminal.Version.Preview);
                }
            }

            if (ch_lang)
            {
                if (toggle2.Checked)
                {
                    Program.Localization = new();
                    Program.Localization.Load(Program.Settings.Language.File);
                    foreach (Form f in Application.OpenForms) f.Localize();
                }
                else
                {
                    MsgBox(Program.Localization.Strings.Languages.Restart, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (ch_EP)
            {
                Program.EP = new();
                Forms.Home.LoadFromTM(Program.TM);
            }

            if (ch_logStatusPanel)
            {
                Forms.MainForm.Status_pnl.Visible = toggle42.Checked;
            }

            Cursor = Cursors.Default;

            MsgBox(Program.Localization.Strings.Messages.SettingsSaved, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Write settings to the settings object
        /// </summary>
        /// <param name="Sets"></param>
        /// <param name="Mode"></param>
        /// <param name="File"></param>
        private void Write(Settings Sets, Settings.Source Mode, string File = "")
        {
            Sets.FileTypeManagement.AutoAddExt = toggle6.Checked;
            Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt = RadioButton1.Checked;
            Sets.ThemeApplyingBehavior.CreateSystemRestore = toggle38.Checked;
            Sets.ThemeApplyingBehavior.AutoRestartExplorer = toggle8.Checked;
            Sets.ThemeApplyingBehavior.ResetCursorsToAero = toggle13.Checked;

            Sets.Updates.AutoCheck = toggle1.Checked;
            Sets.Miscellaneous.Win7LivePreview = toggle35.Checked;
            Sets.Miscellaneous.ShowWelcomeDialog = toggle37.Checked;
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
            Sets.ThemeLog.CountDown_Seconds = (int)trackBarX1.Value;
            Sets.ThemeLog.ShowSkippedItemsOnDetailedVerbose = toggle18.Checked;

            Sets.AppLog.Enabled = toggle40.Checked;
            Sets.AppLog.StatusPanel = toggle42.Checked;
            Sets.AppLog.Reg = toggle41.Checked;
            Sets.AppLog.RegRead = checkBox15.Checked;
            Sets.AppLog.RegWrite = checkBox17.Checked;
            Sets.AppLog.RegDelete = checkBox18.Checked;

            Sets.ExplorerPatcher.Enabled = toggle23.Checked;
            Sets.ExplorerPatcher.Enabled_Force = toggle24.Checked;
            Sets.ExplorerPatcher.UseStart10 = EP_Start_10.Checked;
            Sets.ExplorerPatcher.StartStyle = (ExplorerPatcher.StartStyles)EP_Start_10_Type.SelectedIndex;
            Sets.ExplorerPatcher.UseTaskbar10 = EP_Taskbar_10.Checked;
            Sets.ExplorerPatcher.TaskbarButton10 = EP_ORB_10.Checked;

            Sets.BackupTheme.Enabled = toggle31.Checked;
            Sets.BackupTheme.AutoBackupOnAppOpen = toggle33.Checked;
            Sets.BackupTheme.AutoBackupOnApply = toggle32.Checked;
            Sets.BackupTheme.AutoBackupOnApplySingleAspect = toggle36.Checked;
            Sets.BackupTheme.AutoBackupOnThemeLoad = toggle34.Checked;
            Sets.BackupTheme.AutoBackupOnExError = toggle39.Checked;
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
            Sets.ThemeApplyingBehavior.Show_WinEffects_Alert = toggle12.Checked;

            Sets.Store.Online_or_Offline = RadioImage1.Checked;
            Sets.Store.Online_Repositories = [.. ListBox1.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s))];
            Sets.Store.Offline_Directories = [.. ListBox2.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s))];
            Sets.Store.Search_ThemeNames = CheckBox28.Checked;
            Sets.Store.Search_Descriptions = CheckBox26.Checked;
            Sets.Store.Search_AuthorsNames = CheckBox27.Checked;
            Sets.Store.Offline_SubFolders = CheckBox29.Checked;
            Sets.Store.ShowNewXPIntro = toggle17.Checked;

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
            Sets.AspectsControl.Icons = checkBox14.Checked;
            Sets.AspectsControl.ClassicColors_Advanced = radioImage4.Checked;
            Sets.AspectsControl.MetricsFonts_Advanced = radioImage8.Checked;
            Sets.AspectsControl.Accessibility = checkBox16.Checked;

            Sets.UsersServices.GitHub_AutoOperateOnLinkedFiles = toggle43.Checked;

            Sets.Save(Mode, File);
        }

        /// <summary>
        /// Void to be executed when the form is being closed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Settings NewSets = new(Settings.Source.Empty);
            Write(NewSets, Settings.Source.Empty);

            bool Changed = Program.Settings != NewSets;

            if (e.CloseReason == CloseReason.UserClosing && Changed && !skipCheckingEquality)
            {
                switch (MsgBox(Program.Localization.Strings.Messages.SaveMsg, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
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

                skipCheckingEquality = false;
            }
            else if (e.CloseReason == CloseReason.UserClosing & !Changed)
            {
                skipCheckingEquality = false;
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
            ComboBox2.Items.Add(Program.Localization.Strings.General.Stable);
            ComboBox2.Items.Add(Program.Localization.Strings.General.Beta);
            this.Localize();
            ApplyStyle(this);
            LoadSettings();

            // Do some minor adjustments for appearance
            int w = 19;
            EP_Start_11.Image = WinLogos.Win11.Resize(w, w);
            EP_Start_10.Image = WinLogos.Win10.Resize(w, w);
            EP_Taskbar_11.Image = EP_Start_11.Image;
            EP_Taskbar_10.Image = EP_Start_10.Image;

            if (Program.Style.DarkMode)
            {
                EP_ORB_11.Image = Win11Preview.StartBtn_11_EP.Resize(w, w);
                EP_ORB_10.Image = Win10Preview.StartBtn_10Light.Invert().Resize(w, w);
            }
            else
            {
                EP_ORB_11.Image = Win11Preview.StartBtn_11_EP.Invert().Resize(w, w);
                EP_ORB_10.Image = Win10Preview.StartBtn_10Light.Resize(w, w);
            }

            if (OS.WXP)
            {
                AlertBox17.Visible = true;
                AlertBox17.Text = string.Format(Program.Localization.Strings.Updates.NoTLS12, Program.Localization.Strings.Windows.WXP);
            }

            else if (OS.WVista)
            {
                AlertBox17.Visible = true;
                AlertBox17.Text = string.Format(Program.Localization.Strings.Updates.NoTLS12, Program.Localization.Strings.Windows.WVista);
            }

            storage_data_lbl.Font = Fonts.ConsoleLarge;
            storage_backup_lbl.Font = Fonts.ConsoleLarge;
            storage_logs_lbl.Font = Fonts.ConsoleLarge;
            storage_errors_lbl.Font = Fonts.ConsoleLarge;
            storage_store_lbl.Font = Fonts.ConsoleLarge;
            storage_resPack_lbl.Font = Fonts.ConsoleLarge;
        }

        /// <summary>
        /// Gets the size of the store cache
        /// </summary>
        /// <returns></returns>
        private int CalcStoreCache()
        {
            if (Directory.Exists(SysPaths.StoreCache))
            {
                return (int)Directory.EnumerateFiles(SysPaths.StoreCache, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the size of the extracted themes resources cache
        /// </summary>
        /// <returns></returns>
        private int CalcThemesResCache()
        {
            if (Directory.Exists(SysPaths.ThemeResPackCache))
            {
                return (int)Directory.EnumerateFiles(SysPaths.ThemeResPackCache, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the size of the backups cache
        /// </summary>
        /// <returns></returns>
        private int CalcBackupsCache()
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

        /// <summary>
        /// Gets the size of the exceptions errors cache
        /// </summary>
        /// <returns></returns>
        private int CalcExErrors()
        {
            if (Directory.Exists($"{SysPaths.appData}\\Reports"))
            {
                return (int)Directory.EnumerateFiles($"{SysPaths.appData}\\Reports", "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the size of the exceptions errors cache
        /// </summary>
        /// <returns></returns>
        private int CalcLogs()
        {
            if (Directory.Exists($"{SysPaths.LogsDir}"))
            {
                return (int)Directory.EnumerateFiles($"{SysPaths.LogsDir}", "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the size of the app core (Application data and Program files data)
        /// </summary>
        /// <returns></returns>
        private int CalcAppCore()
        {
            int s0, s1;

            if (Directory.Exists(SysPaths.appData))
            {
                s0 = (int)Directory.EnumerateFiles(SysPaths.appData, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
                s0 -= CalcExErrors() - CalcThemesResCache() - CalcLogs();
                if (Program.Settings.BackupTheme.BackupPath.StartsWith(SysPaths.appData, StringComparison.OrdinalIgnoreCase)) s0 -= CalcBackupsCache();
            }
            else
            {
                s0 = 0;
            }

            if (Directory.Exists(SysPaths.ProgramFilesData))
            {
                s1 = (int)Directory.EnumerateFiles(SysPaths.ProgramFilesData, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                s1 = 0;
            }

            return s0 + s1;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            skipCheckingEquality = true;
            SaveSettings();
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterSettings, FileName = _File, Title = Program.Localization.Strings.Extensions.SaveWinPaletterSettings })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Settings sets = new(Settings.Source.Empty);
                    Write(sets, Settings.Source.File, dlg.FileName);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterSettings, FileName = _File, Title = Program.Localization.Strings.Extensions.OpenWinPaletterSettings })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Settings sets = new(Settings.Source.File, dlg.FileName);
                    Read(sets);
                }
            }
        }

        /// <summary>
        /// Unregister the file associations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Localization.Strings.Messages.RemoveExtMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question, string.Empty, Program.Localization.Strings.General.CollapseNote, Program.Localization.Strings.General.ExpandNote, Program.Localization.Strings.Messages.RemoveExtMsgNote) == DialogResult.Yes)
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

        /// <summary>
        /// Load JSON language file information to be displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.OpenJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox3.Text = dlg.FileName;
                    StreamReader _File = new(TextBox3.Text);
                    JObject J = JObject.Parse(_File.ReadToEnd());
                    _File.Close();

                    if (J["Information"] is not null)
                    {
                        Label11.Text = J["Information"]["name"] is not null ? J["Information"]["name"].ToString() : string.Empty;
                        Label12.Text = J["Information"]["translationversion"] is not null ? J["Information"]["translationversion"].ToString() : string.Empty;
                        Label14.Text = $"{(J["Information"]["appver"] is not null ? J["Information"]["appver"] : Program.Version)} {Program.Localization.Strings.General.AndBelow}";
                        Label19.Text = J["Information"]["lang"] is not null ? J["Information"]["lang"].ToString() : string.Empty;
                        Label16.Text = J["Information"]["langcode"] is not null ? J["Information"]["langcode"].ToString() : string.Empty;
                        Label22.Text = J["Information"]["righttoleft"] is not null ? (!(bool)J["Information"]["righttoleft"] ? Program.Localization.Strings.Languages.LeftToRight : Program.Localization.Strings.Languages.RightToLeft) : Program.Localization.Strings.Languages.LeftToRight;
                    }
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.LanguageCreation);
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.OpenJSON })
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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.OpenJSON })
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
            Process.Start(Links.Languages);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Lang_Editor);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            string inputText = string.Empty;
            if (ListBox1.SelectedItem is not null)
                inputText = ListBox1.SelectedItem.ToString();
            string response = InputBox(Program.Localization.Strings.Messages.InputThemeRepos, inputText, Program.Localization.Strings.Messages.InputThemeRepos_Notice);
            if (!ListBox1.Items.Contains(response))
                ListBox1.Items.Add(response);
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem is not null)
            {
                int i = ListBox1.SelectedIndex;

                if (!((ListBox1.SelectedItem.ToString().ToUpper() ?? string.Empty) == (Links.Store_2ndDB.ToUpper() ?? string.Empty)) & !((ListBox1.SelectedItem.ToString().ToUpper() ?? string.Empty) == (Links.Store_MainDB.ToUpper() ?? string.Empty)))
                {
                    ListBox1.Items.RemoveAt(i);
                    if (i < ListBox1.Items.Count - 1)
                        ListBox1.SelectedIndex = i;
                    else
                        ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
                }
                else
                {
                    MsgBox(Program.Localization.Strings.Store.RemoveTip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                VistaFolderBrowserDialog dlg = new();
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

        /// <summary>
        /// Clean WinPaletter Store cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button19_Click(object sender, EventArgs e)
        {
            Cache_Clean_Store();
            UpdateStorageUI();
        }

        /// <summary>
        /// Clean WinPaletter Themes extracted resources cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button20_Click(object sender, EventArgs e)
        {
            Cache_Clean_ThemesResPack();
            UpdateStorageUI();
        }

        /// <summary>
        /// Run WinPaletter System Events Sounds service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Localization.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, Resources.WinPaletter_SysEventsSounds, ServiceInstaller.RunMethods.Install);
        }

        /// <summary>
        /// Uninstall WinPaletter System Events Sounds service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Localization.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, null, ServiceInstaller.RunMethods.Uninstall);
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

        /// <summary>
        /// Open reports folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            if (Directory.Exists($@"{SysPaths.appData}\Reports"))
            {
                Process.Start($@"{SysPaths.appData}\Reports");
            }
            else
            {
                MsgBox(string.Format(Program.Localization.Strings.Messages.Bug_NoReport, $@"{SysPaths.appData}\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clean WinPaletter exceptions errors cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            Cache_Clean_ExErrors();
            UpdateStorageUI();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                using (VistaFolderBrowserDialog FD = new())
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
            Read(new Settings(Settings.Source.Empty));
        }

        private void SettingsX_ParentChanged(object sender, EventArgs e)
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

        private void toggle4_CheckedChanged(object sender, EventArgs e)
        {
            toggle3.Enabled = !toggle4.Checked;
        }

        private void toggle11_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UI.WP.CheckBox checkBox in (sender as Toggle).Parent.Controls.OfType<UI.WP.CheckBox>())
            {
                checkBox.Enabled = (sender as Toggle).Checked;
            }
        }

        private void VL2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox33.Enabled = VL2.Checked;
            groupBox36.Enabled = VL2.Checked || VL1.Checked;
        }

        private void toggle22_CheckedChanged(object sender, EventArgs e)
        {
            TextBox1.Enabled = toggle22.Checked;
            TextBox2.Enabled = toggle22.Checked;
            Button16.Enabled = toggle22.Checked;
            Button9.Enabled = toggle22.Checked;
        }

        private void toggle24_CheckedChanged(object sender, EventArgs e)
        {
            Panel1.Enabled = toggle24.Checked;
            Panel2.Enabled = toggle24.Checked;
            Panel3.Enabled = toggle24.Checked;
            EP_Start_10_Type.Enabled = toggle24.Checked;
        }

        private void toggle31_CheckedChanged(object sender, EventArgs e)
        {
            groupBox50.Enabled = toggle31.Checked;
            groupBox51.Enabled = toggle31.Checked;
        }

        private void toggle16_CheckedChanged(object sender, EventArgs e)
        {
            Panel12.Enabled = toggle16.Checked;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Program.Settings.General.SetupCompleted = false;
            Program.Settings.General.Save();

            if (MsgBox(Program.Localization.Strings.Messages.RerunSetup_Msg0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Localization.Strings.Messages.ExitWinPaletter) == DialogResult.Yes)
            {
                Program.ForceExit();
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Cache_Clean_Logs();
            UpdateStorageUI();
        }

        /// <summary>
        /// Updates all storage labels and progress bars with relative percentages
        /// </summary>
        private void UpdateStorageUI()
        {
            // Collect all storage sizes
            int store = CalcStoreCache();
            int resPack = CalcThemesResCache();
            int backups = CalcBackupsCache();
            int errors = CalcExErrors();
            int logs = CalcLogs();
            int appCore = CalcAppCore();

            // Dictionary to pair labels, progress bars, and values
            var storageItems = new[]
            {
                new { Value = store, Label = storage_store_lbl, Progress = storage_store_prg },
                new { Value = resPack, Label = storage_resPack_lbl, Progress = storage_resPack_prg },
                new { Value = backups, Label = storage_backup_lbl, Progress = storage_backup_prg },
                new { Value = errors, Label = storage_errors_lbl, Progress = storage_errors_prg },
                new { Value = logs, Label = storage_logs_lbl, Progress = storage_logs_prg },
                new { Value = appCore, Label = storage_data_lbl, Progress = storage_data_prg }
            };

            // Find the maximum size for relative percentage
            int maxSize = storageItems.Max(x => x.Value);
            if (maxSize == 0) maxSize = 1; // Avoid division by zero

            // Update each label and progress bar with float precision
            foreach (var item in storageItems)
            {
                item.Label.Text = item.Value.ToStringFileSize();

                float percentage = ((float)item.Value / maxSize) * 100f;
                item.Progress.Value = Math.Min(100, (int)percentage);

                // Update progress bar state based on relative size
                if (percentage < 33f)
                    item.Progress.State = UI.WP.ProgressBar.ProgressBarState.Normal;
                else if (percentage < 66f)
                    item.Progress.State = UI.WP.ProgressBar.ProgressBarState.Pause;
                else
                    item.Progress.State = UI.WP.ProgressBar.ProgressBarState.Error;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Process.Start(SysPaths.LogsDir);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Process.Start(SysPaths.LogsDir);
        }

        private void toggle40_CheckedChanged(object sender, EventArgs e)
        {
            groupBox57.Enabled = (sender as Toggle).Checked;
            groupBox59.Enabled = (sender as Toggle).Checked;
        }

        private void toggle41_CheckedChanged(object sender, EventArgs e)
        {
            checkBox15.Enabled = (sender as Toggle).Checked;
            checkBox17.Enabled = (sender as Toggle).Checked;
            checkBox18.Enabled = (sender as Toggle).Checked;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Cache_Clean_ThemesResPack();
            Cache_Clean_Logs();
            Cache_Clean_ExErrors();
            Cache_Clean_Store();
            UpdateStorageUI();
        }

        void Cache_Clean_ThemesResPack()
        {
            try
            {
                if (Directory.Exists(SysPaths.ThemeResPackCache)) Directory.Delete(SysPaths.ThemeResPackCache, true);
            }
            catch (UnauthorizedAccessException ex0)
            {
                Forms.BugReport.Throw(ex0);
            }
            catch (Exception ex)
            {
                Forms.BugReport.Throw(ex);
            }
        }

        void Cache_Clean_Logs()
        {
            if (Directory.Exists(SysPaths.LogsDir))
            {
                foreach (string file in Directory.GetFiles(SysPaths.LogsDir))
                {
                    try
                    {
                        if (file != Program.LogFile && File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                    catch (UnauthorizedAccessException ex0)
                    {
                        Forms.BugReport.Throw(ex0);
                    }
                    catch (Exception ex)
                    {
                        Forms.BugReport.Throw(ex);
                    }
                }
            }
        }

        void Cache_Clean_ExErrors()
        {
            try
            {
                if (Directory.Exists(SysPaths.Reports)) Directory.Delete(SysPaths.Reports, true);
            }
            catch (UnauthorizedAccessException ex0)
            {
                Forms.BugReport.Throw(ex0);
            }
            catch (Exception ex)
            {
                Forms.BugReport.Throw(ex);
            }
        }

        void Cache_Clean_Store()
        {
            if (MsgBox(Program.Localization.Strings.Messages.ClearStoreCache, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Localization.Strings.Messages.ConfirmClearStoreCache) == DialogResult.Yes)
            {
                try
                {
                    if (Directory.Exists(SysPaths.StoreCache)) Directory.Delete(SysPaths.StoreCache, true);
                }
                catch (UnauthorizedAccessException ex0)
                {
                    Forms.BugReport.Throw(ex0);
                }
                catch (Exception ex)
                {
                    Forms.BugReport.Throw(ex);
                }

            }
        }
    }
}