using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using WinPaletter.Theme;

namespace WinPaletter
{
    public partial class Sounds_Editor
    {
        private string snd;
        private SoundPlayer SP = new();
        private bool AltPlayingMethod = false;

        private void Sounds_Editor_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.Start(Links.Wiki.Sounds);
        }

        public Sounds_Editor()
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
                        GetFromClassicThemeFile(dlg.FileName, _Def.Sounds);
                    }
                }
            }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            Program.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = CheckBox35_SFC.Checked;
            Program.Settings.Save(Settings.Mode.Registry);

            ApplyToTM(Program.TM);

            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            Program.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = CheckBox35_SFC.Checked;
            Program.Settings.Save(Settings.Mode.Registry);

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
                TMx.Sounds.Apply();
            }

            Cursor = Cursors.Default;
        }


        #region Main Methods

        private void Button20_Click(object sender, EventArgs e) // imageres.dll player
        {
            AltPlayingMethod = false;
            snd = ((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0).Text;


            if (TextBox2.Text.ToUpper().Trim() == "CURRENT")
            {
                if (!OS.WXP)
                {

                    byte[] SoundBytes = PE.GetResource(SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                    try
                    {
                        using (MemoryStream ms = new(SoundBytes))
                        {
                            SP = new(ms);
                            SP.Load();
                            SP.Play();
                        }
                    }
                    catch // Use method #2
                    {
                        string tmp = Path.GetTempFileName();
                        File.WriteAllBytes(tmp, SoundBytes);
                        AltPlayingMethod = true;
                        NativeMethods.DLLFunc.PlayAudio(tmp);
                        if (File.Exists(tmp))
                            FileSystem.Kill(tmp);
                    }

                }
            }

            else if (TextBox2.Text.ToUpper().Trim() == "DEFAULT")
            {
                if (!OS.WXP)
                {
                    try
                    {
                        using (FileStream FS = new($@"{SysPaths.appData}\WindowsStartup_Backup.wav", FileMode.Open, FileAccess.Read))
                        {
                            SP = new(FS);
                            SP.Load();
                            SP.Play();
                        }
                    }
                    catch // Use method #2
                    {
                        AltPlayingMethod = true;
                        NativeMethods.DLLFunc.PlayAudio($@"{SysPaths.appData}\WindowsStartup_Backup.wav");
                    }

                }
            }

            else if ((snd ?? string.Empty) == (((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0).Text ?? string.Empty))
            {
                if (File.Exists(snd))
                {
                    if (SP is not null)
                    {
                        SP?.Stop();
                        SP?.Dispose();
                    }
                    try
                    {
                        using (FileStream FS = new(snd, FileMode.Open, FileAccess.Read))
                        {
                            SP = new(FS);
                            SP?.Load();
                            SP?.Play();
                        }
                    }
                    catch // Use method #2
                    {
                        AltPlayingMethod = true;
                        NativeMethods.DLLFunc.PlayAudio(snd);
                    }
                }

                else
                {
                    if (AltPlayingMethod) NativeMethods.DLLFunc.StopAudio();

                    if (SP is not null)
                    {
                        SP?.Stop();
                        SP?.Dispose();
                    }
                }
            }

            else
            {

            }
        }

        public void PressPlay(object sender, EventArgs e)
        {
            AltPlayingMethod = false;
            snd = ((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0).Text;

            if (File.Exists(snd))
            {
                if (SP is not null)
                {
                    SP?.Stop();
                    SP?.Dispose();
                }

                try
                {
                    using (FileStream FS = new(snd, FileMode.Open, FileAccess.Read))
                    {
                        SP = new(FS);
                        SP?.Load();
                        SP?.Play();
                    }
                }
                catch // Use method #2
                {
                    AltPlayingMethod = true;
                    NativeMethods.DLLFunc.PlayAudio(snd);
                }
            }

            else
            {
                if (AltPlayingMethod)
                    NativeMethods.DLLFunc.StopAudio();

                if (SP is not null)
                {
                    SP?.Stop();
                    SP?.Dispose();
                }
            }
        }

        public void StopPlayer(object sender, EventArgs e)
        {
            if (AltPlayingMethod) NativeMethods.DLLFunc.StopAudio();

            if (SP is not null)
            {
                SP?.Stop();
                SP?.Dispose();
            }
        }

        public void BrowseForWAV(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WAV, Title = Program.Lang.Filter_OpenWAV })
            {
                UI.WP.TextBox temp = ((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0);
                if (File.Exists(temp?.Text))
                {
                    dlg.FileName = new FileInfo(temp.Text).Name;
                    dlg.InitialDirectory = new FileInfo(temp.Text).DirectoryName;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    snd = dlg.FileName;

                    ((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0).Text = snd;
                    try
                    {
                        using (FileStream FS = new(snd, FileMode.Open, FileAccess.Read))
                        {
                            SP = new(FS);
                            SP?.Load();
                            SP?.Play();
                        }
                    }
                    catch // Use method #2
                    {
                        AltPlayingMethod = true;
                        NativeMethods.DLLFunc.PlayAudio(snd);
                    }
                }
            }

        }
        #endregion

        private void Sounds_Editor_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Store_Toggle_Sounds,
                Enabled = Program.TM.Sounds.Enabled,
                Import_theme = true,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromTHEME = LoadFromTHEME,
            };

            LoadData(data);

            LoadFromTM(Program.TM);
            CheckBox35_SFC.Checked = Program.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound;

            // Remove handler to avoid doubling/tripling events
            foreach (TabPage page in TabControl1.TabPages)
            {
                foreach (Panel pnl in page.Controls.OfType<Panel>())
                {
                    foreach (UI.WP.Button btn in pnl.Controls.OfType<UI.WP.Button>())
                    {
                        if ((btn.Tag ?? string.Empty).ToString() == "1") btn.Click -= PressPlay;
                        if ((btn.Tag ?? string.Empty).ToString() == "2") btn.Click -= StopPlayer;
                        if ((btn.Tag ?? string.Empty).ToString() == "3") btn.Click -= BrowseForWAV;
                    }
                }
            }

            foreach (TabPage page in TabControl1.TabPages)
            {
                foreach (Panel pnl in page.Controls.OfType<Panel>())
                {
                    foreach (UI.WP.Button btn in pnl.Controls.OfType<UI.WP.Button>())
                    {
                        if ((btn.Tag ?? string.Empty).ToString() == "1")
                            btn.Click += PressPlay;
                        if ((btn.Tag ?? string.Empty).ToString() == "2")
                            btn.Click += StopPlayer;
                        if ((btn.Tag ?? string.Empty).ToString() == "3")
                            btn.Click += BrowseForWAV;
                    }
                }
            }
        }

        private void Sounds_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage page in TabControl1.TabPages)
            {
                foreach (Panel pnl in page.Controls.OfType<Panel>())
                {
                    foreach (UI.WP.Button btn in pnl.Controls.OfType<UI.WP.Button>())
                    {
                        if ((btn.Tag ?? string.Empty).ToString() == "1")
                            btn.Click -= PressPlay;
                        if ((btn.Tag ?? string.Empty).ToString() == "2")
                            btn.Click -= StopPlayer;
                        if ((btn.Tag ?? string.Empty).ToString() == "3")
                            btn.Click -= BrowseForWAV;
                    }
                }
            }
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            LoadFromTM(TM.Sounds);
        }

        public void LoadFromTM(Theme.Structures.Sounds Sounds)
        {
            AspectEnabled = Sounds.Enabled;
            TextBox1.Text = Sounds.Snd_Win_SystemStart;
            TextBox2.Text = Sounds.Snd_Imageres_SystemStart;
            TextBox3.Text = Sounds.Snd_Win_SystemExit;
            TextBox4.Text = Sounds.Snd_Win_WindowsLogoff;
            TextBox5.Text = Sounds.Snd_Win_WindowsLogon;
            TextBox6.Text = Sounds.Snd_Win_WindowsUnlock;
            TextBox64.Text = Sounds.Snd_Win_ChangeTheme;
            TextBox7.Text = Sounds.Snd_Win_SystemQuestion;
            TextBox8.Text = Sounds.Snd_Win_SystemExclamation;
            TextBox9.Text = Sounds.Snd_Win_SystemAsterisk;
            TextBox10.Text = Sounds.Snd_Win_SystemNotification;
            TextBox11.Text = Sounds.Snd_Win_WindowsUAC;
            TextBox16.Text = Sounds.Snd_Win_Open;
            TextBox15.Text = Sounds.Snd_Win_Close;
            TextBox14.Text = Sounds.Snd_Win_Maximize;
            TextBox13.Text = Sounds.Snd_Win_Minimize;
            TextBox12.Text = Sounds.Snd_Win_RestoreDown;
            TextBox17.Text = Sounds.Snd_Win_RestoreUp;
            TextBox53.Text = Sounds.Snd_Win_MenuPopup;
            TextBox54.Text = Sounds.Snd_Win_MenuCommand;
            TextBox55.Text = Sounds.Snd_Win_Default;
            TextBox23.Text = Sounds.Snd_Win_Notification_Default;
            TextBox22.Text = Sounds.Snd_Win_Notification_IM;
            TextBox21.Text = Sounds.Snd_Win_MessageNudge;
            TextBox20.Text = Sounds.Snd_Win_Notification_Mail;
            TextBox65.Text = Sounds.Snd_Win_MailBeep;
            TextBox19.Text = Sounds.Snd_Win_Notification_Proximity;
            TextBox18.Text = Sounds.Snd_Win_Notification_Reminder;
            TextBox24.Text = Sounds.Snd_Win_Notification_SMS;
            TextBox31.Text = Sounds.Snd_Win_Notification_Looping_Alarm;
            TextBox30.Text = Sounds.Snd_Win_Notification_Looping_Alarm2;
            TextBox29.Text = Sounds.Snd_Win_Notification_Looping_Alarm3;
            TextBox28.Text = Sounds.Snd_Win_Notification_Looping_Alarm4;
            TextBox27.Text = Sounds.Snd_Win_Notification_Looping_Alarm5;
            TextBox26.Text = Sounds.Snd_Win_Notification_Looping_Alarm6;
            TextBox25.Text = Sounds.Snd_Win_Notification_Looping_Alarm7;
            TextBox34.Text = Sounds.Snd_Win_Notification_Looping_Alarm8;
            TextBox33.Text = Sounds.Snd_Win_Notification_Looping_Alarm9;
            TextBox32.Text = Sounds.Snd_Win_Notification_Looping_Alarm10;
            TextBox44.Text = Sounds.Snd_Win_Notification_Looping_Call;
            TextBox43.Text = Sounds.Snd_Win_Notification_Looping_Call2;
            TextBox42.Text = Sounds.Snd_Win_Notification_Looping_Call3;
            TextBox41.Text = Sounds.Snd_Win_Notification_Looping_Call4;
            TextBox40.Text = Sounds.Snd_Win_Notification_Looping_Call5;
            TextBox39.Text = Sounds.Snd_Win_Notification_Looping_Call6;
            TextBox38.Text = Sounds.Snd_Win_Notification_Looping_Call7;
            TextBox37.Text = Sounds.Snd_Win_Notification_Looping_Call8;
            TextBox36.Text = Sounds.Snd_Win_Notification_Looping_Call9;
            TextBox35.Text = Sounds.Snd_Win_Notification_Looping_Call10;
            TextBox45.Text = Sounds.Snd_Win_DeviceConnect;
            TextBox46.Text = Sounds.Snd_Win_DeviceDisconnect;
            TextBox47.Text = Sounds.Snd_Win_DeviceFail;
            TextBox48.Text = Sounds.Snd_Win_LowBatteryAlarm;
            TextBox49.Text = Sounds.Snd_Win_CriticalBatteryAlarm;
            TextBox50.Text = Sounds.Snd_Win_PrintComplete;
            TextBox51.Text = Sounds.Snd_Win_FaxBeep;
            TextBox52.Text = Sounds.Snd_Win_ProximityConnection;
            TextBox62.Text = Sounds.Snd_Explorer_Navigating;
            TextBox61.Text = Sounds.Snd_Explorer_EmptyRecycleBin;
            TextBox56.Text = Sounds.Snd_Explorer_MoveMenuItem;
            TextBox60.Text = Sounds.Snd_Explorer_ActivatingDocument;
            TextBox63.Text = Sounds.Snd_Win_ShowBand;
            TextBox59.Text = Sounds.Snd_Explorer_SecurityBand;
            TextBox58.Text = Sounds.Snd_Explorer_BlockedPopup;
            TextBox57.Text = Sounds.Snd_Explorer_FeedDiscovered;
            TextBox68.Text = Sounds.Snd_Win_AppGPFault;
            TextBox67.Text = Sounds.Snd_Win_CCSelect;
            TextBox66.Text = Sounds.Snd_Win_SystemHand;
            TextBox75.Text = Sounds.Snd_Explorer_SearchProviderDiscovered;
            TextBox76.Text = Sounds.Snd_Explorer_FaxNew;
            TextBox77.Text = Sounds.Snd_Explorer_FaxSent;
            TextBox79.Text = Sounds.Snd_Explorer_FaxLineRings;
            TextBox78.Text = Sounds.Snd_Explorer_FaxError;
            TextBox83.Text = Sounds.Snd_NetMeeting_PersonJoins;
            TextBox82.Text = Sounds.Snd_NetMeeting_PersonLeaves;
            TextBox80.Text = Sounds.Snd_NetMeeting_ReceiveCall;
            TextBox81.Text = Sounds.Snd_NetMeeting_ReceiveRequestToJoin;
            TextBox70.Text = Sounds.Snd_SpeechRec_DisNumbersSound;
            TextBox74.Text = Sounds.Snd_SpeechRec_PanelSound;
            TextBox69.Text = Sounds.Snd_SpeechRec_MisrecoSound;
            TextBox73.Text = Sounds.Snd_SpeechRec_HubOffSound;
            TextBox72.Text = Sounds.Snd_SpeechRec_HubOnSound;
            TextBox71.Text = Sounds.Snd_SpeechRec_HubSleepSound;

            TextBox84.Text = Sounds.Snd_ChargerConnected;
            textBox85.Text = Sounds.Snd_ChargerDisconnected;
            textBox86.Text = Sounds.Snd_Win_WindowsLock;
            textBox87.Text = Sounds.Snd_WiFiConnected;
            textBox88.Text = Sounds.Snd_WiFiDisconnected;
            textBox89.Text = Sounds.Snd_WiFiConnectionFailed;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            ref Theme.Structures.Sounds Sounds = ref TM.Sounds;
            Sounds.Enabled = AspectEnabled;
            Sounds.Snd_Win_SystemStart = TextBox1.Text;
            Sounds.Snd_Imageres_SystemStart = TextBox2.Text;
            Sounds.Snd_Win_SystemExit = TextBox3.Text;
            Sounds.Snd_Win_WindowsLogoff = TextBox4.Text;
            Sounds.Snd_Win_WindowsLogon = TextBox5.Text;
            Sounds.Snd_Win_WindowsUnlock = TextBox6.Text;
            Sounds.Snd_Win_ChangeTheme = TextBox64.Text;
            Sounds.Snd_Win_SystemQuestion = TextBox7.Text;
            Sounds.Snd_Win_SystemExclamation = TextBox8.Text;
            Sounds.Snd_Win_SystemAsterisk = TextBox9.Text;
            Sounds.Snd_Win_SystemHand = TextBox66.Text;
            Sounds.Snd_Win_SystemNotification = TextBox10.Text;
            Sounds.Snd_Win_WindowsUAC = TextBox11.Text;
            Sounds.Snd_Win_Open = TextBox16.Text;
            Sounds.Snd_Win_Close = TextBox15.Text;
            Sounds.Snd_Win_Maximize = TextBox14.Text;
            Sounds.Snd_Win_Minimize = TextBox13.Text;
            Sounds.Snd_Win_RestoreDown = TextBox12.Text;
            Sounds.Snd_Win_RestoreUp = TextBox17.Text;
            Sounds.Snd_Win_MenuPopup = TextBox53.Text;
            Sounds.Snd_Win_MenuCommand = TextBox54.Text;
            Sounds.Snd_Win_Default = TextBox55.Text;
            Sounds.Snd_Win_Notification_Default = TextBox23.Text;
            Sounds.Snd_Win_Notification_IM = TextBox22.Text;
            Sounds.Snd_Win_MessageNudge = TextBox21.Text;
            Sounds.Snd_Win_Notification_Mail = TextBox20.Text;
            Sounds.Snd_Win_MailBeep = TextBox65.Text;
            Sounds.Snd_Win_Notification_Proximity = TextBox19.Text;
            Sounds.Snd_Win_Notification_Reminder = TextBox18.Text;
            Sounds.Snd_Win_Notification_SMS = TextBox24.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm = TextBox31.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm2 = TextBox30.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm3 = TextBox29.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm4 = TextBox28.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm5 = TextBox27.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm6 = TextBox26.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm7 = TextBox25.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm8 = TextBox34.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm9 = TextBox33.Text;
            Sounds.Snd_Win_Notification_Looping_Alarm10 = TextBox32.Text;
            Sounds.Snd_Win_Notification_Looping_Call = TextBox44.Text;
            Sounds.Snd_Win_Notification_Looping_Call2 = TextBox43.Text;
            Sounds.Snd_Win_Notification_Looping_Call3 = TextBox42.Text;
            Sounds.Snd_Win_Notification_Looping_Call4 = TextBox41.Text;
            Sounds.Snd_Win_Notification_Looping_Call5 = TextBox40.Text;
            Sounds.Snd_Win_Notification_Looping_Call6 = TextBox39.Text;
            Sounds.Snd_Win_Notification_Looping_Call7 = TextBox38.Text;
            Sounds.Snd_Win_Notification_Looping_Call8 = TextBox37.Text;
            Sounds.Snd_Win_Notification_Looping_Call9 = TextBox36.Text;
            Sounds.Snd_Win_Notification_Looping_Call10 = TextBox35.Text;
            Sounds.Snd_Win_DeviceConnect = TextBox45.Text;
            Sounds.Snd_Win_DeviceDisconnect = TextBox46.Text;
            Sounds.Snd_Win_DeviceFail = TextBox47.Text;
            Sounds.Snd_Win_LowBatteryAlarm = TextBox48.Text;
            Sounds.Snd_Win_CriticalBatteryAlarm = TextBox49.Text;
            Sounds.Snd_Win_PrintComplete = TextBox50.Text;
            Sounds.Snd_Win_FaxBeep = TextBox51.Text;
            Sounds.Snd_Win_ProximityConnection = TextBox52.Text;
            Sounds.Snd_Explorer_Navigating = TextBox62.Text;
            Sounds.Snd_Explorer_EmptyRecycleBin = TextBox61.Text;
            Sounds.Snd_Explorer_MoveMenuItem = TextBox56.Text;
            Sounds.Snd_Explorer_ActivatingDocument = TextBox60.Text;
            Sounds.Snd_Win_ShowBand = TextBox63.Text;
            Sounds.Snd_Explorer_SecurityBand = TextBox59.Text;
            Sounds.Snd_Explorer_BlockedPopup = TextBox58.Text;
            Sounds.Snd_Explorer_FeedDiscovered = TextBox57.Text;
            Sounds.Snd_Win_AppGPFault = TextBox68.Text;
            Sounds.Snd_Win_CCSelect = TextBox67.Text;
            Sounds.Snd_Explorer_SearchProviderDiscovered = TextBox75.Text;
            Sounds.Snd_Explorer_FaxNew = TextBox76.Text;
            Sounds.Snd_Explorer_FaxSent = TextBox77.Text;
            Sounds.Snd_Explorer_FaxLineRings = TextBox79.Text;
            Sounds.Snd_Explorer_FaxError = TextBox78.Text;
            Sounds.Snd_NetMeeting_PersonJoins = TextBox83.Text;
            Sounds.Snd_NetMeeting_PersonLeaves = TextBox82.Text;
            Sounds.Snd_NetMeeting_ReceiveCall = TextBox80.Text;
            Sounds.Snd_NetMeeting_ReceiveRequestToJoin = TextBox81.Text;
            Sounds.Snd_SpeechRec_DisNumbersSound = TextBox70.Text;
            Sounds.Snd_SpeechRec_PanelSound = TextBox74.Text;
            Sounds.Snd_SpeechRec_MisrecoSound = TextBox69.Text;
            Sounds.Snd_SpeechRec_HubOffSound = TextBox73.Text;
            Sounds.Snd_SpeechRec_HubOnSound = TextBox72.Text;
            Sounds.Snd_SpeechRec_HubSleepSound = TextBox71.Text;

            Sounds.Snd_ChargerConnected = TextBox84.Text;
            Sounds.Snd_ChargerDisconnected = textBox85.Text;
            Sounds.Snd_Win_WindowsLock = textBox86.Text;
            Sounds.Snd_WiFiConnected = textBox87.Text;
            Sounds.Snd_WiFiDisconnected = textBox88.Text;
            Sounds.Snd_WiFiConnectionFailed = textBox89.Text;
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "Default";
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            TextBox2.Text = string.Empty;
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "Current";
        }

        public void GetFromClassicThemeFile(string File, Theme.Structures.Sounds _DefaultSounds)
        {
            using (INI _ini = new(File))
            {
                Theme.Structures.Sounds snd = new();

                string Scope_Win = @"AppEvents\Schemes\Apps\.Default\{0}\.Current";
                snd.Snd_Win_Default = _ini.Read(string.Format(Scope_Win, ".Default"), "DefaultValue", _DefaultSounds.Snd_Win_Default).PhrasePath();
                snd.Snd_Win_AppGPFault = _ini.Read(string.Format(Scope_Win, "AppGPFault"), "DefaultValue", _DefaultSounds.Snd_Win_AppGPFault).PhrasePath();
                snd.Snd_Win_CCSelect = _ini.Read(string.Format(Scope_Win, "CCSelect"), "DefaultValue", _DefaultSounds.Snd_Win_CCSelect).PhrasePath();
                snd.Snd_Win_ChangeTheme = _ini.Read(string.Format(Scope_Win, "ChangeTheme"), "DefaultValue", _DefaultSounds.Snd_Win_ChangeTheme).PhrasePath();
                snd.Snd_Win_Close = _ini.Read(string.Format(Scope_Win, "Close"), "DefaultValue", _DefaultSounds.Snd_Win_Close).PhrasePath();
                snd.Snd_Win_CriticalBatteryAlarm = _ini.Read(string.Format(Scope_Win, "CriticalBatteryAlarm"), "DefaultValue", _DefaultSounds.Snd_Win_CriticalBatteryAlarm).PhrasePath();
                snd.Snd_Win_DeviceConnect = _ini.Read(string.Format(Scope_Win, "DeviceConnect"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceConnect).PhrasePath();
                snd.Snd_Win_DeviceDisconnect = _ini.Read(string.Format(Scope_Win, "DeviceDisconnect"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceDisconnect).PhrasePath();
                snd.Snd_Win_DeviceFail = _ini.Read(string.Format(Scope_Win, "DeviceFail"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceFail).PhrasePath();
                snd.Snd_Win_FaxBeep = _ini.Read(string.Format(Scope_Win, "FaxBeep"), "DefaultValue", _DefaultSounds.Snd_Win_FaxBeep).PhrasePath();
                snd.Snd_Win_LowBatteryAlarm = _ini.Read(string.Format(Scope_Win, "LowBatteryAlarm"), "DefaultValue", _DefaultSounds.Snd_Win_LowBatteryAlarm).PhrasePath();
                snd.Snd_Win_MailBeep = _ini.Read(string.Format(Scope_Win, "MailBeep"), "DefaultValue", _DefaultSounds.Snd_Win_MailBeep).PhrasePath();
                snd.Snd_Win_Maximize = _ini.Read(string.Format(Scope_Win, "Maximize"), "DefaultValue", _DefaultSounds.Snd_Win_Maximize).PhrasePath();
                snd.Snd_Win_MenuCommand = _ini.Read(string.Format(Scope_Win, "MenuCommand"), "DefaultValue", _DefaultSounds.Snd_Win_MenuCommand).PhrasePath();
                snd.Snd_Win_MenuPopup = _ini.Read(string.Format(Scope_Win, "MenuPopup"), "DefaultValue", _DefaultSounds.Snd_Win_MenuPopup).PhrasePath();
                snd.Snd_Win_MessageNudge = _ini.Read(string.Format(Scope_Win, "MessageNudge"), "DefaultValue", _DefaultSounds.Snd_Win_MessageNudge).PhrasePath();
                snd.Snd_Win_Minimize = _ini.Read(string.Format(Scope_Win, "Minimize"), "DefaultValue", _DefaultSounds.Snd_Win_Minimize).PhrasePath();
                snd.Snd_Win_Notification_Default = _ini.Read(string.Format(Scope_Win, "Notification.Default"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Default).PhrasePath();
                snd.Snd_Win_Notification_IM = _ini.Read(string.Format(Scope_Win, "Notification.IM"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_IM).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm2 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm2"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm2).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm3 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm3"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm3).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm4 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm4"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm4).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm5 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm5"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm5).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm6 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm6"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm6).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm7 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm7"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm7).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm8 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm8"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm8).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm9 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm9"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm9).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm10 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Alarm10"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm10).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call2 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call2"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call2).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call3 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call3"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call3).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call4 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call4"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call4).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call5 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call5"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call5).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call6 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call6"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call6).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call7 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call7"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call7).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call8 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call8"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call8).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call9 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call9"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call9).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call10 = _ini.Read(string.Format(Scope_Win, "Notification.Looping.Call10"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call10).PhrasePath();
                snd.Snd_Win_Notification_Mail = _ini.Read(string.Format(Scope_Win, "Notification.Mail"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Mail).PhrasePath();
                snd.Snd_Win_Notification_Proximity = _ini.Read(string.Format(Scope_Win, "Notification.Proximity"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Proximity).PhrasePath();
                snd.Snd_Win_Notification_Reminder = _ini.Read(string.Format(Scope_Win, "Notification.Reminder"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Reminder).PhrasePath();
                snd.Snd_Win_Notification_SMS = _ini.Read(string.Format(Scope_Win, "Notification.SMS"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_SMS).PhrasePath();
                snd.Snd_Win_Open = _ini.Read(string.Format(Scope_Win, "Open"), "DefaultValue", _DefaultSounds.Snd_Win_Open).PhrasePath();
                snd.Snd_Win_PrintComplete = _ini.Read(string.Format(Scope_Win, "PrintComplete"), "DefaultValue", _DefaultSounds.Snd_Win_PrintComplete).PhrasePath();
                snd.Snd_Win_ProximityConnection = _ini.Read(string.Format(Scope_Win, "ProximityConnection"), "DefaultValue", _DefaultSounds.Snd_Win_ProximityConnection).PhrasePath();
                snd.Snd_Win_RestoreDown = _ini.Read(string.Format(Scope_Win, "RestoreDown"), "DefaultValue", _DefaultSounds.Snd_Win_RestoreDown).PhrasePath();
                snd.Snd_Win_RestoreUp = _ini.Read(string.Format(Scope_Win, "RestoreUp"), "DefaultValue", _DefaultSounds.Snd_Win_RestoreUp).PhrasePath();
                snd.Snd_Win_ShowBand = _ini.Read(string.Format(Scope_Win, "ShowBand"), "DefaultValue", _DefaultSounds.Snd_Win_ShowBand).PhrasePath();
                snd.Snd_Win_SystemAsterisk = _ini.Read(string.Format(Scope_Win, "SystemAsterisk"), "DefaultValue", _DefaultSounds.Snd_Win_SystemAsterisk).PhrasePath();
                snd.Snd_Win_SystemExclamation = _ini.Read(string.Format(Scope_Win, "SystemExclamation"), "DefaultValue", _DefaultSounds.Snd_Win_SystemExclamation).PhrasePath();
                snd.Snd_Win_SystemExit = _ini.Read(string.Format(Scope_Win, "SystemExit"), "DefaultValue", _DefaultSounds.Snd_Win_SystemExit).PhrasePath();
                snd.Snd_Win_SystemStart = _ini.Read(string.Format(Scope_Win, "SystemStart"), "DefaultValue", _DefaultSounds.Snd_Win_SystemStart).PhrasePath();
                snd.Snd_Imageres_SystemStart = System.IO.File.Exists(snd.Snd_Win_SystemStart) ? snd.Snd_Win_SystemStart : "Current";
                snd.Snd_Win_SystemHand = _ini.Read(string.Format(Scope_Win, "SystemHand"), "DefaultValue", _DefaultSounds.Snd_Win_SystemHand).PhrasePath();
                snd.Snd_Win_SystemNotification = _ini.Read(string.Format(Scope_Win, "SystemNotification"), "DefaultValue", _DefaultSounds.Snd_Win_SystemNotification).PhrasePath();
                snd.Snd_Win_SystemQuestion = _ini.Read(string.Format(Scope_Win, "SystemQuestion"), "DefaultValue", _DefaultSounds.Snd_Win_SystemQuestion).PhrasePath();
                snd.Snd_Win_WindowsLogoff = _ini.Read(string.Format(Scope_Win, "WindowsLogoff"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsLogoff).PhrasePath();
                snd.Snd_Win_WindowsLogon = _ini.Read(string.Format(Scope_Win, "WindowsLogon"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsLogon).PhrasePath();
                snd.Snd_Win_WindowsUAC = _ini.Read(string.Format(Scope_Win, "WindowsUAC"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsUAC).PhrasePath();
                snd.Snd_Win_WindowsUnlock = _ini.Read(string.Format(Scope_Win, "WindowsUnlock"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsUnlock).PhrasePath();

                string Scope_Explorer = @"AppEvents\Schemes\Apps\Explorer\{0}\.Current";
                snd.Snd_Explorer_ActivatingDocument = _ini.Read(string.Format(Scope_Explorer, "ActivatingDocument"), "DefaultValue", _DefaultSounds.Snd_Explorer_ActivatingDocument).PhrasePath();
                snd.Snd_Explorer_BlockedPopup = _ini.Read(string.Format(Scope_Explorer, "BlockedPopup"), "DefaultValue", _DefaultSounds.Snd_Explorer_BlockedPopup).PhrasePath();
                snd.Snd_Explorer_EmptyRecycleBin = _ini.Read(string.Format(Scope_Explorer, "EmptyRecycleBin"), "DefaultValue", _DefaultSounds.Snd_Explorer_EmptyRecycleBin).PhrasePath();
                snd.Snd_Explorer_FeedDiscovered = _ini.Read(string.Format(Scope_Explorer, "FeedDiscovered"), "DefaultValue", _DefaultSounds.Snd_Explorer_FeedDiscovered).PhrasePath();
                snd.Snd_Explorer_MoveMenuItem = _ini.Read(string.Format(Scope_Explorer, "MoveMenuItem"), "DefaultValue", _DefaultSounds.Snd_Explorer_MoveMenuItem).PhrasePath();
                snd.Snd_Explorer_Navigating = _ini.Read(string.Format(Scope_Explorer, "Navigating"), "DefaultValue", _DefaultSounds.Snd_Explorer_Navigating).PhrasePath();
                snd.Snd_Explorer_SecurityBand = _ini.Read(string.Format(Scope_Explorer, "SecurityBand"), "DefaultValue", _DefaultSounds.Snd_Explorer_SecurityBand).PhrasePath();
                snd.Snd_Explorer_SearchProviderDiscovered = _ini.Read(string.Format(Scope_Explorer, "SearchProviderDiscovered"), "DefaultValue", _DefaultSounds.Snd_Explorer_SearchProviderDiscovered).PhrasePath();
                snd.Snd_Explorer_FaxError = _ini.Read(string.Format(Scope_Explorer, "FaxError"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxError).PhrasePath();
                snd.Snd_Explorer_FaxLineRings = _ini.Read(string.Format(Scope_Explorer, "FaxLineRings"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxLineRings).PhrasePath();
                snd.Snd_Explorer_FaxNew = _ini.Read(string.Format(Scope_Explorer, "FaxNew"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxNew).PhrasePath();
                snd.Snd_Explorer_FaxSent = _ini.Read(string.Format(Scope_Explorer, "FaxSent"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxSent).PhrasePath();

                string Scope_NetMeeting = @"AppEvents\Schemes\Apps\Conf\{0}\.Current";
                snd.Snd_NetMeeting_PersonJoins = _ini.Read(string.Format(Scope_NetMeeting, "Person Joins"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_PersonJoins).PhrasePath();
                snd.Snd_NetMeeting_PersonLeaves = _ini.Read(string.Format(Scope_NetMeeting, "Person Leaves"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_PersonLeaves).PhrasePath();
                snd.Snd_NetMeeting_ReceiveCall = _ini.Read(string.Format(Scope_NetMeeting, "Receive Call"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_ReceiveCall).PhrasePath();
                snd.Snd_NetMeeting_ReceiveRequestToJoin = _ini.Read(string.Format(Scope_NetMeeting, "Receive Request to Join"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_ReceiveRequestToJoin).PhrasePath();

                string Scope_SpeechRec = @"AppEvents\Schemes\Apps\sapisvr\{0}\.current";
                snd.Snd_SpeechRec_DisNumbersSound = _ini.Read(string.Format(Scope_SpeechRec, "DisNumbersSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_DisNumbersSound).PhrasePath();
                snd.Snd_SpeechRec_HubOffSound = _ini.Read(string.Format(Scope_SpeechRec, "HubOffSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubOffSound).PhrasePath();
                snd.Snd_SpeechRec_HubOnSound = _ini.Read(string.Format(Scope_SpeechRec, "HubOnSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubOnSound).PhrasePath();
                snd.Snd_SpeechRec_HubSleepSound = _ini.Read(string.Format(Scope_SpeechRec, "HubSleepSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubSleepSound).PhrasePath();
                snd.Snd_SpeechRec_MisrecoSound = _ini.Read(string.Format(Scope_SpeechRec, "MisrecoSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_MisrecoSound).PhrasePath();
                snd.Snd_SpeechRec_PanelSound = _ini.Read(string.Format(Scope_SpeechRec, "PanelSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_PanelSound).PhrasePath();

                LoadFromTM(snd);
                GC.Collect();
                GC.SuppressFinalize(snd);
            }
        }

        #region Restore default values

        private void button259_Click_1(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox1.Text = TMx.Sounds.Snd_Win_SystemStart; }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox3.Text = TMx.Sounds.Snd_Win_SystemExit; }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox4.Text = TMx.Sounds.Snd_Win_WindowsLogoff; }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox5.Text = TMx.Sounds.Snd_Win_WindowsLogon; }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox6.Text = TMx.Sounds.Snd_Win_WindowsUnlock; }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { textBox86.Text = TMx.Sounds.Snd_Win_WindowsLock; }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox64.Text = TMx.Sounds.Snd_Win_ChangeTheme; }
        }

        private void button275_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox10.Text = TMx.Sounds.Snd_Win_SystemNotification; }
        }

        private void button274_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox7.Text = TMx.Sounds.Snd_Win_SystemQuestion; }
        }

        private void button273_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox8.Text = TMx.Sounds.Snd_Win_SystemExclamation; }
        }

        private void button272_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox9.Text = TMx.Sounds.Snd_Win_SystemAsterisk; }
        }

        private void button271_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox66.Text = TMx.Sounds.Snd_Win_SystemHand; }
        }

        private void button270_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox11.Text = TMx.Sounds.Snd_Win_WindowsUAC; }
        }

        private void button269_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox68.Text = TMx.Sounds.Snd_Win_AppGPFault; }
        }

        private void button283_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox16.Text = TMx.Sounds.Snd_Win_Open; }
        }

        private void button282_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox15.Text = TMx.Sounds.Snd_Win_Close; }
        }

        private void button281_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox14.Text = TMx.Sounds.Snd_Win_Maximize; }
        }

        private void button280_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox13.Text = TMx.Sounds.Snd_Win_Minimize; }
        }

        private void button279_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox12.Text = TMx.Sounds.Snd_Win_RestoreDown; }
        }

        private void button278_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox17.Text = TMx.Sounds.Snd_Win_RestoreUp; }
        }

        private void button292_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox45.Text = TMx.Sounds.Snd_Win_DeviceConnect; }
        }

        private void button291_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox46.Text = TMx.Sounds.Snd_Win_DeviceDisconnect; }
        }

        private void button290_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox47.Text = TMx.Sounds.Snd_Win_DeviceFail; }
        }

        private void button289_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox84.Text = TMx.Sounds.Snd_ChargerConnected; }
        }

        private void button288_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { textBox85.Text = TMx.Sounds.Snd_ChargerDisconnected; }
        }

        private void button287_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox48.Text = TMx.Sounds.Snd_Win_LowBatteryAlarm; }
        }

        private void button286_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox49.Text = TMx.Sounds.Snd_Win_CriticalBatteryAlarm; }
        }

        private void button285_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox50.Text = TMx.Sounds.Snd_Win_PrintComplete; }
        }

        private void button284_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox52.Text = TMx.Sounds.Snd_Win_ProximityConnection; }
        }

        private void button297_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox79.Text = TMx.Sounds.Snd_Explorer_FaxLineRings; }
        }

        private void button296_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox51.Text = TMx.Sounds.Snd_Win_FaxBeep; }
        }

        private void button295_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox76.Text = TMx.Sounds.Snd_Explorer_FaxNew; }
        }

        private void button294_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox77.Text = TMx.Sounds.Snd_Explorer_FaxSent; }
        }

        private void button293_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox78.Text = TMx.Sounds.Snd_Explorer_FaxError; }
        }

        private void button306_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox62.Text = TMx.Sounds.Snd_Explorer_Navigating; }
        }

        private void button305_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox61.Text = TMx.Sounds.Snd_Explorer_EmptyRecycleBin; }
        }

        private void button304_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox56.Text = TMx.Sounds.Snd_Explorer_MoveMenuItem; }
        }

        private void button303_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox60.Text = TMx.Sounds.Snd_Explorer_ActivatingDocument; }
        }

        private void button302_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox63.Text = TMx.Sounds.Snd_Win_ShowBand; }
        }

        private void button301_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox59.Text = TMx.Sounds.Snd_Explorer_SecurityBand; }
        }

        private void button300_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox58.Text = TMx.Sounds.Snd_Explorer_BlockedPopup; }
        }

        private void button299_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox57.Text = TMx.Sounds.Snd_Explorer_FeedDiscovered; }
        }

        private void button298_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox75.Text = TMx.Sounds.Snd_Explorer_SearchProviderDiscovered; }
        }

        private void button315_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox55.Text = TMx.Sounds.Snd_Win_Default; }
        }

        private void button314_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox23.Text = TMx.Sounds.Snd_Win_Notification_Default; }
        }

        private void button313_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox22.Text = TMx.Sounds.Snd_Win_Notification_IM; }
        }

        private void button312_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox21.Text = TMx.Sounds.Snd_Win_MessageNudge; }
        }

        private void button311_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox20.Text = TMx.Sounds.Snd_Win_Notification_Mail; }
        }

        private void button310_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox65.Text = TMx.Sounds.Snd_Win_MailBeep; }
        }

        private void button309_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox19.Text = TMx.Sounds.Snd_Win_Notification_Proximity; }
        }

        private void button308_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox18.Text = TMx.Sounds.Snd_Win_Notification_Reminder; }
        }

        private void button307_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox24.Text = TMx.Sounds.Snd_Win_Notification_SMS; }
        }

        private void button325_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox31.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm; }
        }

        private void button324_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox30.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm2; }
        }

        private void button323_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox29.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm3; }
        }

        private void button322_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox28.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm4; }
        }

        private void button321_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox27.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm5; }
        }

        private void button320_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox26.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm6; }
        }

        private void button319_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox25.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm7; }
        }

        private void button318_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox34.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm8; }
        }

        private void button317_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox33.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm9; }
        }

        private void button316_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox32.Text = TMx.Sounds.Snd_Win_Notification_Looping_Alarm10; }
        }

        private void button335_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox44.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call; }
        }

        private void button334_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox43.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call2; }
        }

        private void button333_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox42.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call3; }
        }

        private void button332_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox41.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call4; }
        }

        private void button331_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox40.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call5; }
        }

        private void button330_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox39.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call6; }
        }

        private void button329_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox38.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call7; }
        }

        private void button328_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox37.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call8; }
        }

        private void button327_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox36.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call9; }
        }

        private void button326_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox35.Text = TMx.Sounds.Snd_Win_Notification_Looping_Call10; }
        }

        private void button341_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox70.Text = TMx.Sounds.Snd_SpeechRec_DisNumbersSound; }
        }

        private void button340_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox74.Text = TMx.Sounds.Snd_SpeechRec_PanelSound; }
        }

        private void button339_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox69.Text = TMx.Sounds.Snd_SpeechRec_MisrecoSound; }
        }

        private void button338_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox73.Text = TMx.Sounds.Snd_SpeechRec_HubOffSound; }
        }

        private void button337_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox72.Text = TMx.Sounds.Snd_SpeechRec_HubOnSound; }
        }

        private void button336_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox71.Text = TMx.Sounds.Snd_SpeechRec_HubSleepSound; }
        }

        private void button345_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox83.Text = TMx.Sounds.Snd_NetMeeting_PersonJoins; }
        }

        private void button344_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox82.Text = TMx.Sounds.Snd_NetMeeting_PersonLeaves; }
        }

        private void button343_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox80.Text = TMx.Sounds.Snd_NetMeeting_ReceiveCall; }
        }

        private void button342_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox81.Text = TMx.Sounds.Snd_NetMeeting_ReceiveRequestToJoin; }
        }

        private void button346_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { TextBox67.Text = TMx.Sounds.Snd_Win_CCSelect; }
        }

        private void button348_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { textBox87.Text = TMx.Sounds.Snd_WiFiConnected; }
        }

        private void button352_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { textBox88.Text = TMx.Sounds.Snd_WiFiDisconnected; }
        }

        private void button356_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Default.Get(Program.WindowStyle)) { textBox89.Text = TMx.Sounds.Snd_WiFiConnectionFailed; }
        }

        #endregion

        private void button347_Click(object sender, EventArgs e)
        {
            Forms.SysEventsSndsInstaller.Install(true);
        }
    }
}