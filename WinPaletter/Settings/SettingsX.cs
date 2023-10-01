using Microsoft.VisualBasic;
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
            WPSettings sets;

            if (!_External)
                sets = My.Env.Settings;
            else
                sets = new WPSettings(WPSettings.Mode.File, _File);
            Read(sets);

            {
                ref var temp = ref My.Env.Lang;
                Label11.Text = temp.Name;
                Label12.Text = temp.TranslationVersion;
                Label14.Text = temp.AppVer + " " + My.Env.Lang.AndBelow;
                Label19.Text = temp.Lang;
                Label16.Text = temp.LangCode;
                Label22.Text = !temp.RightToLeft ? My.Env.Lang.Lang_HasLeftToRight : My.Env.Lang.Lang_HasRightToLeft;
            }

            if (_External)
                OpenFileDialog1.FileName = _File;
            TextBox3.Text = My.Env.Settings.Language.File;
        }
        public void Read(WPSettings Sets)
        {
            CheckBox1.Checked = Sets.FileTypeManagement.AutoAddExt;

            RadioButton1.Checked = Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt;
            RadioButton2.Checked = !Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt;

            CheckBox2.Checked = Sets.ThemeApplyingBehavior.AutoRestartExplorer;
            CheckBox7.Checked = Sets.ThemeApplyingBehavior.AutoApplyCursors;
            CheckBox16.Checked = Sets.ThemeApplyingBehavior.ResetCursorsToAero;

            CheckBox5.Checked = Sets.Updates.AutoCheck;
            CheckBox9.Checked = Sets.Miscellaneous.Win7LivePreview;

            ComboBox2.SelectedIndex = Sets.Updates.Channel == WPSettings.Structures.Updates.Channels.Stable ? 0 : 1;
            CheckBox17.Checked = Sets.ThemeApplyingBehavior.ShowSaveConfirmation;
            CheckBox33.Checked = Sets.FileTypeManagement.CompressThemeFile;

            RadioButton3.Checked = Sets.Appearance.DarkMode;
            RadioButton4.Checked = !Sets.Appearance.DarkMode;
            CheckBox6.Checked = Sets.Appearance.AutoDarkMode;
            CheckBox30.Checked = Sets.Appearance.ManagedByTheme;

            CheckBox8.Checked = Sets.Language.Enabled;
            TextBox3.Text = Sets.Language.File;

            CheckBox12.Checked = Sets.WindowsTerminals.Bypass;
            CheckBox13.Checked = Sets.WindowsTerminals.ListAllFonts;
            CheckBox14.Checked = Sets.WindowsTerminals.Path_Deflection;
            TextBox1.Text = Sets.WindowsTerminals.Terminal_Stable_Path;
            TextBox2.Text = Sets.WindowsTerminals.Terminal_Preview_Path;
            CheckBox15.Checked = Sets.ThemeApplyingBehavior.CMD_OverrideUserPreferences;

            switch (Sets.NerdStats.Type)
            {
                case WPSettings.Structures.NerdStats.Formats.HEX:
                    {
                        ComboBox3.SelectedIndex = 0;
                        break;
                    }
                case WPSettings.Structures.NerdStats.Formats.RGB:
                    {
                        ComboBox3.SelectedIndex = 1;
                        break;
                    }
                case WPSettings.Structures.NerdStats.Formats.HSL:
                    {
                        ComboBox3.SelectedIndex = 2;
                        break;
                    }
                case WPSettings.Structures.NerdStats.Formats.Dec:
                    {
                        ComboBox3.SelectedIndex = 3;
                        break;
                    }
            }
            CheckBox10.Checked = Sets.NerdStats.Enabled;
            CheckBox11.Checked = Sets.NerdStats.ShowHexHash;
            CheckBox3.Checked = Sets.NerdStats.MoreLabelTransparency;
            CheckBox31.Checked = Sets.NerdStats.UseWindowsMonospacedFont;
            CheckBox34.Checked = Sets.NerdStats.DotDefaultChangedIndicator;

            switch (Sets.ThemeLog.VerboseLevel)
            {
                case WPSettings.Structures.ThemeLog.VerboseLevels.Basic:
                    {
                        VL1.Checked = true;
                        break;
                    }

                case WPSettings.Structures.ThemeLog.VerboseLevels.Detailed:
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

            CheckBox18.Checked = Sets.ThemeLog.CountDown;
            NumericUpDown1.Value = Sets.ThemeLog.CountDown_Seconds;
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.Checked = Sets.ThemeLog.ShowSkippedItemsOnDetailedVerbose;

            CheckBox20.Checked = Sets.ExplorerPatcher.Enabled;
            CheckBox21.Checked = Sets.ExplorerPatcher.Enabled_Force;
            EP_Start_10.Checked = Sets.ExplorerPatcher.UseStart10;
            EP_Start_11.Checked = !Sets.ExplorerPatcher.UseStart10;
            EP_Start_10_Type.SelectedIndex = (int)Sets.ExplorerPatcher.StartStyle;
            EP_Taskbar_10.Checked = Sets.ExplorerPatcher.UseTaskbar10;
            EP_Taskbar_11.Checked = !Sets.ExplorerPatcher.UseTaskbar10;
            EP_ORB_10.Checked = Sets.ExplorerPatcher.TaskbarButton10;
            EP_ORB_11.Checked = !Sets.ExplorerPatcher.TaskbarButton10;

            CheckBox22.Checked = Sets.ThemeApplyingBehavior.DelayMetrics;

            RadioButton5.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton6.Checked = !(Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton8.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton10.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            RadioButton9.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;
            RadioButton7.Checked = Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase;
            CheckBox25.Checked = Sets.ThemeApplyingBehavior.UPM_HKU_DEFAULT;
            RadioButton12.Checked = Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton11.Checked = !(Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton14.Checked = Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton13.Checked = !(Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton16.Checked = Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton15.Checked = !(Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton18.Checked = Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton17.Checked = !(Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton20.Checked = Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton19.Checked = !(Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite);
            RadioButton22.Checked = Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            RadioButton23.Checked = Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;
            RadioButton21.Checked = Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            CheckBox35_SFC.Checked = Sets.ThemeApplyingBehavior.SFC_on_restoring_StartupSound;
            CheckBox36.Checked = Sets.ThemeApplyingBehavior.Ignore_PE_Modify_Alert;
            RadioButton25.Checked = Sets.ThemeApplyingBehavior.PE_ModifyByDefault;
            RadioButton24.Checked = !Sets.ThemeApplyingBehavior.PE_ModifyByDefault;
            CheckBox35.Checked = Sets.NerdStats.DragAndDrop;
            CheckBox37.Checked = Sets.NerdStats.DragAndDropColorsGuide;
            CheckBox38.Checked = Sets.NerdStats.DragAndDropRippleEffect;

            Label38.Text = CalcStoreCache().SizeString();
            Label43.Text = CalcThemesResCache().SizeString();

            RadioImage1.Checked = Sets.Store.Online_or_Offline;
            RadioImage2.Checked = !Sets.Store.Online_or_Offline;
            ListBox1.Items.Clear();
            foreach (var x in Sets.Store.Online_Repositories)
            {
                if (!string.IsNullOrWhiteSpace(x))
                    ListBox1.Items.Add(x);
            }
            ListBox2.Items.Clear();
            foreach (var x in Sets.Store.Offline_Directories)
            {
                if (!string.IsNullOrWhiteSpace(x))
                    ListBox2.Items.Add(x);
            }

            CheckBox28.Checked = Sets.Store.Search_ThemeNames;
            CheckBox26.Checked = Sets.Store.Search_Descriptions;
            CheckBox27.Checked = Sets.Store.Search_AuthorsNames;
            CheckBox29.Checked = Sets.Store.Offline_SubFolders;
            CheckBox4.Checked = Sets.Store.ShowTips;

            CheckBox32.Checked = Sets.Miscellaneous.Classic_Color_Picker;
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

            {
                ref var temp = ref My.Env.Settings;
                if (temp.Appearance.DarkMode != RadioButton3.Checked)
                    ch_appearance = true;
                if (temp.Appearance.AutoDarkMode != CheckBox6.Checked)
                    ch_appearance = true;
                if (temp.Appearance.ManagedByTheme != CheckBox30.Checked)
                    ch_appearance = true;

                if (temp.NerdStats.Enabled != CheckBox10.Checked)
                    ch_nerd = true;
                if ((int)temp.NerdStats.Type != ComboBox3.SelectedIndex)
                    ch_nerd = true;
                if (temp.NerdStats.ShowHexHash != CheckBox11.Checked)
                    ch_nerd = true;
                if (temp.NerdStats.MoreLabelTransparency != CheckBox3.Checked)
                    ch_nerd = true;
                if (temp.NerdStats.UseWindowsMonospacedFont != CheckBox31.Checked)
                    ch_nerd = true;
                if (temp.NerdStats.DotDefaultChangedIndicator != CheckBox34.Checked)
                    ch_nerd = true;

                if (temp.WindowsTerminals.Path_Deflection != CheckBox14.Checked)
                    ch_terminal = true;
                if ((temp.WindowsTerminals.Terminal_Stable_Path ?? "") != (TextBox1.Text ?? ""))
                    ch_terminal = true;
                if ((temp.WindowsTerminals.Terminal_Preview_Path ?? "") != (TextBox2.Text ?? ""))
                    ch_terminal = true;

                if (temp.Language.Enabled != CheckBox8.Checked)
                    ch_lang = true;
                if ((temp.Language.File ?? "") != (TextBox3.Text ?? ""))
                    ch_lang = true;

                if (temp.ExplorerPatcher.Enabled != CheckBox20.Checked)
                    ch_EP = true;
                if (temp.ExplorerPatcher.Enabled_Force != CheckBox21.Checked)
                    ch_EP = true;
                if (temp.ExplorerPatcher.UseStart10 != EP_Start_10.Checked)
                    ch_EP = true;
                if ((int)temp.ExplorerPatcher.StartStyle != EP_Start_10_Type.SelectedIndex)
                    ch_EP = true;
                if (temp.ExplorerPatcher.UseTaskbar10 != EP_Taskbar_10.Checked)
                    ch_EP = true;
                if (temp.ExplorerPatcher.TaskbarButton10 != EP_ORB_10.Checked)
                    ch_EP = true;
            }

            Write(My.Env.Settings, WPSettings.Mode.Registry);

            if (ch_appearance)
            {
                WPStyle.FetchDarkMode();
                WPStyle.ApplyStyle();
            }

            if (ch_nerd)
            {
                for (int ix = Application.OpenForms.Count - 1; ix >= 0; ix -= 1)
                    Application.OpenForms[ix].Refresh();
            }

            if (ch_terminal)
            {
                if (My.Env.W10 | My.Env.W11)
                {
                    string TerDir;
                    string TerPreDir;

                    if (!My.Env.Settings.WindowsTerminals.Path_Deflection)
                    {
                        TerDir = My.Env.PATH_TerminalJSON;
                        TerPreDir = My.Env.PATH_TerminalPreviewJSON;
                    }
                    else
                    {
                        if (File.Exists(My.Env.Settings.WindowsTerminals.Terminal_Stable_Path))
                        {
                            TerDir = My.Env.Settings.WindowsTerminals.Terminal_Stable_Path;
                        }
                        else
                        {
                            TerDir = My.Env.PATH_TerminalJSON;
                        }

                        if (File.Exists(My.Env.Settings.WindowsTerminals.Terminal_Preview_Path))
                        {
                            TerPreDir = My.Env.Settings.WindowsTerminals.Terminal_Preview_Path;
                        }
                        else
                        {
                            TerPreDir = My.Env.PATH_TerminalPreviewJSON;
                        }
                    }


                    if (File.Exists(TerDir))
                    {
                        My.Env.CP.Terminal = new WinTerminal(TerDir, WinTerminal.Mode.JSONFile);
                    }
                    else
                    {
                        My.Env.CP.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
                    }

                    if (File.Exists(TerPreDir))
                    {
                        My.Env.CP.TerminalPreview = new WinTerminal(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                    }
                    else
                    {
                        My.Env.CP.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                    }
                }

                else
                {
                    My.Env.CP.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
                    My.Env.CP.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                }
            }

            if (ch_lang)
            {
                if (CheckBox8.Checked)
                {
                    My.Env.Lang = new Localizer();
                    My.Env.Lang.LoadLanguageFromJSON(My.Env.Settings.Language.File);
                    foreach (Form f in My.MyProject.Application.OpenForms)
                        f.LoadLanguage();
                    My.MyProject.Forms.MainFrm.UpdateLegends();
                }
                else
                {
                    WPStyle.MsgBox(My.Env.Lang.LanguageRestart, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (ch_EP)
            {
                My.Env.EP = new ExplorerPatcher();
                My.MyProject.Forms.MainFrm.ApplyColorsToElements(My.Env.CP);
                My.MyProject.Forms.MainFrm.ApplyCPValues(My.Env.CP);
                My.MyProject.Forms.MainFrm.ApplyStylesToElements(My.Env.CP, false);
                PreviewHelpers.ReValidateLivePreview(My.MyProject.Forms.MainFrm.pnl_preview);
            }

            Cursor = Cursors.Default;

            WPStyle.MsgBox(My.Env.Lang.SettingsSaved, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Write(WPSettings Sets, WPSettings.Mode Mode, string File = "")
        {
            Sets.FileTypeManagement.AutoAddExt = CheckBox1.Checked;
            Sets.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt = RadioButton1.Checked;
            Sets.ThemeApplyingBehavior.AutoRestartExplorer = CheckBox2.Checked;
            Sets.ThemeApplyingBehavior.AutoApplyCursors = CheckBox7.Checked;
            Sets.ThemeApplyingBehavior.ResetCursorsToAero = CheckBox16.Checked;

            Sets.Updates.AutoCheck = CheckBox5.Checked;
            Sets.Miscellaneous.Win7LivePreview = CheckBox9.Checked;
            Sets.Updates.Channel = (WPSettings.Structures.Updates.Channels)ComboBox2.SelectedIndex;

            Sets.Appearance.DarkMode = RadioButton3.Checked;
            Sets.Appearance.AutoDarkMode = CheckBox6.Checked;
            Sets.Appearance.ManagedByTheme = CheckBox30.Checked;

            Sets.ThemeApplyingBehavior.ShowSaveConfirmation = CheckBox17.Checked;
            Sets.FileTypeManagement.CompressThemeFile = CheckBox33.Checked;

            Sets.Language.Enabled = CheckBox8.Checked;
            Sets.Language.File = TextBox3.Text;
            Sets.NerdStats.Enabled = CheckBox10.Checked;
            Sets.NerdStats.Type = (WPSettings.Structures.NerdStats.Formats)ComboBox3.SelectedIndex;
            Sets.NerdStats.ShowHexHash = CheckBox11.Checked;
            Sets.NerdStats.MoreLabelTransparency = CheckBox3.Checked;
            Sets.NerdStats.UseWindowsMonospacedFont = CheckBox31.Checked;
            Sets.NerdStats.DotDefaultChangedIndicator = CheckBox34.Checked;
            Sets.NerdStats.DragAndDrop = CheckBox35.Checked;
            Sets.NerdStats.DragAndDropColorsGuide = CheckBox37.Checked;
            Sets.NerdStats.DragAndDropRippleEffect = CheckBox38.Checked;

            Sets.WindowsTerminals.Bypass = CheckBox12.Checked;
            Sets.WindowsTerminals.ListAllFonts = CheckBox13.Checked;
            Sets.WindowsTerminals.Path_Deflection = CheckBox14.Checked;
            Sets.WindowsTerminals.Terminal_Stable_Path = TextBox1.Text;
            Sets.WindowsTerminals.Terminal_Preview_Path = TextBox2.Text;
            Sets.ThemeApplyingBehavior.CMD_OverrideUserPreferences = CheckBox15.Checked;

            if (VL0.Checked)
                Sets.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.None;
            if (VL1.Checked)
                Sets.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Basic;
            if (VL2.Checked)
                Sets.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;
            Sets.ThemeLog.CountDown = CheckBox18.Checked;
            Sets.ThemeLog.CountDown_Seconds = NumericUpDown1.Value;
            Sets.ThemeLog.ShowSkippedItemsOnDetailedVerbose = CheckBox19_ShowSkippedItemsOnDetailedVerbose.Checked;

            Sets.ExplorerPatcher.Enabled = CheckBox20.Checked;
            Sets.ExplorerPatcher.Enabled_Force = CheckBox21.Checked;
            Sets.ExplorerPatcher.UseStart10 = EP_Start_10.Checked;
            Sets.ExplorerPatcher.StartStyle = (ExplorerPatcher.StartStyles)EP_Start_10_Type.SelectedIndex;
            Sets.ExplorerPatcher.UseTaskbar10 = EP_Taskbar_10.Checked;
            Sets.ExplorerPatcher.TaskbarButton10 = EP_ORB_10.Checked;
            Sets.ThemeApplyingBehavior.DelayMetrics = CheckBox22.Checked;

            if (RadioButton5.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            if (RadioButton8.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            if (RadioButton10.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            if (RadioButton9.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;
            if (RadioButton7.Checked)
                Sets.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase;
            Sets.ThemeApplyingBehavior.UPM_HKU_DEFAULT = CheckBox25.Checked;
            if (RadioButton12.Checked)
                Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            if (RadioButton14.Checked)
                Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            if (RadioButton16.Checked)
                Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            if (RadioButton18.Checked)
                Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            if (RadioButton20.Checked)
                Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            else
                Sets.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            if (RadioButton22.Checked)
                Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;
            if (RadioButton23.Checked)
                Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults;
            if (RadioButton21.Checked)
                Sets.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;
            Sets.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = CheckBox35_SFC.Checked;
            Sets.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox36.Checked;
            Sets.ThemeApplyingBehavior.PE_ModifyByDefault = RadioButton25.Checked;

            Sets.Store.Online_or_Offline = RadioImage1.Checked;
            Sets.Store.Online_Repositories = ListBox1.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Sets.Store.Offline_Directories = ListBox2.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Sets.Store.Search_ThemeNames = CheckBox28.Checked;
            Sets.Store.Search_Descriptions = CheckBox26.Checked;
            Sets.Store.Search_AuthorsNames = CheckBox27.Checked;
            Sets.Store.Offline_SubFolders = CheckBox29.Checked;
            Sets.Store.ShowTips = CheckBox4.Checked;

            Sets.Miscellaneous.Classic_Color_Picker = CheckBox32.Checked;

            Sets.Save(Mode, File);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var NewSets = new WPSettings(WPSettings.Mode.Empty);

            Changed = false;

            {
                ref var temp = ref My.Env.Settings;
                if (temp.FileTypeManagement.AutoAddExt != CheckBox1.Checked)
                    Changed = true;
                if (temp.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt != RadioButton1.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.AutoRestartExplorer != CheckBox2.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.AutoApplyCursors != CheckBox7.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.ResetCursorsToAero != CheckBox16.Checked)
                    Changed = true;
                if (temp.Updates.AutoCheck != CheckBox5.Checked)
                    Changed = true;
                if ((int)temp.Updates.Channel != ComboBox2.SelectedIndex)
                    Changed = true;
                if (temp.Miscellaneous.Win7LivePreview != CheckBox9.Checked)
                    Changed = true;
                if (temp.Miscellaneous.Classic_Color_Picker != CheckBox32.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.ShowSaveConfirmation != CheckBox17.Checked)
                    Changed = true;
                if (temp.FileTypeManagement.CompressThemeFile != CheckBox33.Checked)
                    Changed = true;

                if (temp.Appearance.DarkMode != RadioButton3.Checked)
                    Changed = true;
                if (temp.Appearance.AutoDarkMode != CheckBox6.Checked)
                    Changed = true;
                if (temp.Appearance.ManagedByTheme != CheckBox30.Checked)
                    Changed = true;

                if (temp.Language.Enabled != CheckBox8.Checked)
                    Changed = true;
                if ((temp.Language.File ?? "") != (TextBox3.Text ?? ""))
                    Changed = true;

                if (temp.NerdStats.Enabled != CheckBox10.Checked)
                    Changed = true;
                if ((int)temp.NerdStats.Type != ComboBox3.SelectedIndex)
                    Changed = true;
                if (temp.NerdStats.ShowHexHash != CheckBox11.Checked)
                    Changed = true;
                if (temp.NerdStats.MoreLabelTransparency != CheckBox3.Checked)
                    Changed = true;
                if (temp.NerdStats.UseWindowsMonospacedFont != CheckBox31.Checked)
                    Changed = true;
                if (temp.NerdStats.DotDefaultChangedIndicator != CheckBox34.Checked)
                    Changed = true;
                if (temp.NerdStats.DragAndDrop != CheckBox35.Checked)
                    Changed = true;
                if (temp.NerdStats.DragAndDropColorsGuide != CheckBox37.Checked)
                    Changed = true;
                if (temp.NerdStats.DragAndDropRippleEffect != CheckBox38.Checked)
                    Changed = true;

                if (temp.WindowsTerminals.Bypass != CheckBox12.Checked)
                    Changed = true;
                if (temp.WindowsTerminals.ListAllFonts != CheckBox13.Checked)
                    Changed = true;
                if (temp.WindowsTerminals.Path_Deflection != CheckBox14.Checked)
                    Changed = true;
                if ((temp.WindowsTerminals.Terminal_Stable_Path ?? "") != (TextBox1.Text ?? ""))
                    Changed = true;
                if ((temp.WindowsTerminals.Terminal_Preview_Path ?? "") != (TextBox2.Text ?? ""))
                    Changed = true;
                if (temp.ThemeApplyingBehavior.CMD_OverrideUserPreferences != CheckBox15.Checked)
                    Changed = true;

                if (temp.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.None & !VL0.Checked)
                    Changed = true;
                if (temp.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Basic & !VL1.Checked)
                    Changed = true;
                if (temp.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed & !VL2.Checked)
                    Changed = true;
                if (temp.ThemeLog.CountDown != CheckBox18.Checked)
                    Changed = true;
                if (temp.ThemeLog.CountDown_Seconds != NumericUpDown1.Value)
                    Changed = true;
                if (temp.ThemeLog.ShowSkippedItemsOnDetailedVerbose != CheckBox19_ShowSkippedItemsOnDetailedVerbose.Checked)
                    Changed = true;

                if (temp.ExplorerPatcher.Enabled != CheckBox20.Checked)
                    Changed = true;
                if (temp.ExplorerPatcher.Enabled_Force != CheckBox21.Checked)
                    Changed = true;
                if (temp.ExplorerPatcher.UseStart10 != EP_Start_10.Checked)
                    Changed = true;
                if ((int)temp.ExplorerPatcher.StartStyle != EP_Start_10_Type.SelectedIndex)
                    Changed = true;
                if (temp.ExplorerPatcher.UseTaskbar10 != EP_Taskbar_10.Checked)
                    Changed = true;
                if (temp.ExplorerPatcher.TaskbarButton10 != EP_ORB_10.Checked)
                    Changed = true;

                if (temp.ThemeApplyingBehavior.DelayMetrics != CheckBox22.Checked)
                    Changed = true;

                if (temp.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton5.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton6.Checked)
                    Changed = true;

                if (temp.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton8.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton10.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults & !RadioButton9.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase & !RadioButton7.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.UPM_HKU_DEFAULT != CheckBox25.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton12.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton11.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton14.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton13.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton16.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton15.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton18.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton17.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton20.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton19.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite & !RadioButton22.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults & !RadioButton23.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange & !RadioButton21.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.SFC_on_restoring_StartupSound != CheckBox35_SFC.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.Ignore_PE_Modify_Alert != CheckBox36.Checked)
                    Changed = true;
                if (temp.ThemeApplyingBehavior.PE_ModifyByDefault != RadioButton25.Checked)
                    Changed = true;

                if (temp.Store.Online_or_Offline & !RadioImage1.Checked)
                    Changed = true;
                if (!temp.Store.Online_or_Offline & !RadioImage2.Checked)
                    Changed = true;

                if (temp.Store.Search_ThemeNames != CheckBox28.Checked)
                    Changed = true;
                if (temp.Store.Search_Descriptions != CheckBox26.Checked)
                    Changed = true;
                if (temp.Store.Search_AuthorsNames != CheckBox27.Checked)
                    Changed = true;
                if (temp.Store.Offline_SubFolders != CheckBox29.Checked)
                    Changed = true;
                if (temp.Store.ShowTips != CheckBox4.Checked)
                    Changed = true;

            }

            if (e.CloseReason == CloseReason.UserClosing & Changed)
            {
                switch (WPStyle.MsgBox(My.Env.Lang.SaveMsg, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
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
            ComboBox2.Items.Add(My.Env.Lang.Stable);
            ComboBox2.Items.Add(My.Env.Lang.Beta);
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            LoadSettings();

            int w = 19;
            EP_Start_11.Image = My.Resources.Native11.Resize(w, w);
            EP_Start_10.Image = My.Resources.Native10.Resize(w, w);
            EP_Taskbar_11.Image = EP_Start_11.Image;
            EP_Taskbar_10.Image = EP_Start_10.Image;

            if (My.Env.Style.DarkMode)
            {
                EP_ORB_11.Image = My.Resources.StartBtn_11_EP.Resize(w, w);
                EP_ORB_10.Image = My.Resources.StartBtn_10Dark.Resize(w, w);
            }
            else
            {
                EP_ORB_11.Image = My.Resources.StartBtn_11_EP.Invert().Resize(w, w);
                EP_ORB_10.Image = My.Resources.StartBtn_10Light.Resize(w, w);
            }

            if (My.Env.WXP)
            {
                AlertBox17.Visible = true;
                AlertBox17.Text = string.Format(My.Env.Lang.UpdatesOSNoTLS12, My.Env.Lang.OS_WinXP);
            }

            else if (My.Env.WVista)
            {
                AlertBox17.Visible = true;
                AlertBox17.Text = string.Format(My.Env.Lang.UpdatesOSNoTLS12, My.Env.Lang.OS_WinVista);
            }

            Label38.Font = My.MyProject.Application.ConsoleFontMedium;
            Label43.Font = My.MyProject.Application.ConsoleFontMedium;

        }

        public int CalcStoreCache()
        {
            if (Directory.Exists(My.Env.PATH_StoreCache))
            {
                return (int)Directory.EnumerateFiles(My.Env.PATH_StoreCache, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
        }

        public int CalcThemesResCache()
        {
            if (Directory.Exists(My.Env.PATH_ThemeResPackCache))
            {
                return (int)Directory.EnumerateFiles(My.Env.PATH_ThemeResPackCache, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);
            }
            else
            {
                return 0;
            }
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

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var sets = new WPSettings(WPSettings.Mode.Empty);
                Write(sets, WPSettings.Mode.File, SaveFileDialog1.FileName);
            }

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var sets = new WPSettings(WPSettings.Mode.File, OpenFileDialog1.FileName);
                Read(sets);
            }
        }

        private void Me_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (Path.GetExtension(files[0]).ToLower() == ".wpsf")
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainFrm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            var sets = new WPSettings(WPSettings.Mode.File, files[0]);
            Read(sets);

            OpenFileDialog1.FileName = files[0];
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (WPStyle.MsgBox(My.Env.Lang.RemoveExtMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question, "", My.Env.Lang.CollapseNote, My.Env.Lang.ExpandNote, My.Env.Lang.RemoveExtMsgNote) == DialogResult.Yes)
            {

                CheckBox1.Checked = false;
                My.MyProject.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
                My.MyProject.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
                My.MyProject.Application.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Uninstall.ShowDialog();
        }

        private void Button7_Click(object sender, EventArgs e)
        {

            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                TextBox3.Text = OpenFileDialog2.FileName;

                try
                {
                    var _File = new StreamReader(TextBox3.Text);
                    JObject J = JObject.Parse(_File.ReadToEnd());
                    _File.Close();

                    Label11.Text = J["Information"]["name"].ToString();
                    Label12.Text = J["Information"]["translationversion"].ToString();
                    Label14.Text = J["Information"]["appver"].ToString() + " " + My.Env.Lang.AndBelow;
                    Label19.Text = J["Information"]["lang"].ToString();
                    Label16.Text = J["Information"]["langcode"].ToString();
                    Label22.Text = !(bool)J["Information"]["righttoleft"] ? My.Env.Lang.Lang_HasLeftToRight : My.Env.Lang.Lang_HasRightToLeft;
                }
                catch
                {
                }

            }

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start(My.Resources.Link_Repository + "wiki/Language-creation");
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (OpenJSONDlg.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenJSONDlg.FileName;
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (OpenJSONDlg.ShowDialog() == DialogResult.OK)
            {
                TextBox2.Text = OpenJSONDlg.FileName;
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Process.Start(My.Resources.Link_Repository + "tree/master/Languages");
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Lang_Dashboard.ShowDialog();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            string inputText = "";
            if (ListBox1.SelectedItem is not null)
                inputText = ListBox1.SelectedItem.ToString();
            string response = WPStyle.InputBox(My.Env.Lang.InputThemeRepos, inputText, My.Env.Lang.InputThemeRepos_Notice);
            if (!ListBox1.Items.Contains(response))
                ListBox1.Items.Add(response);
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem is not null)
            {
                int i = ListBox1.SelectedIndex;

                if (!((ListBox1.SelectedItem.ToString().ToUpper() ?? "") == (My.Resources.Link_StoreReposDB.ToUpper() ?? "")) & !((ListBox1.SelectedItem.ToString().ToUpper() ?? "") == (My.Resources.Link_StoreMainDB.ToUpper() ?? "")))
                {
                    ListBox1.Items.RemoveAt(i);
                    if (i < ListBox1.Items.Count - 1)
                        ListBox1.SelectedIndex = i;
                    else
                        ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
                }
                else
                {
                    WPStyle.MsgBox(My.Env.Lang.Store_RemoveTip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {

            if (!My.Env.WXP)
            {
                var dlg = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ListBox2.Items.Contains(dlg.SelectedPath))
                        ListBox2.Items.Add(dlg.SelectedPath);
                }
                dlg.Dispose();
            }
            else if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!ListBox2.Items.Contains(FolderBrowserDialog1.SelectedPath))
                    ListBox2.Items.Add(FolderBrowserDialog1.SelectedPath);
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
                Directory.Delete(My.Env.PATH_StoreCache, true);
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
                Directory.Delete(My.Env.PATH_ThemeResPackCache, true);
            }
            catch
            {
            }
            Label43.Text = CalcThemesResCache().SizeString();
        }
    }
}