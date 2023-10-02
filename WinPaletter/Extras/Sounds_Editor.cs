using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class Sounds_Editor
    {
        private string snd;
        private SoundPlayer SP = new SoundPlayer();
        private bool AltPlayingMethod = false;

        public Sounds_Editor()
        {
            InitializeComponent();
        }
        #region Main Subs

        private void Button20_Click(object sender, EventArgs e) // imageres.dll player
        {
            AltPlayingMethod = false;
            snd = ((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0).Text;


            if (TextBox2.Text.ToUpper().Trim() == "CURRENT")
            {
                if (!My.Env.WXP)
                {

                    byte[] SoundBytes = PE.GetResource(My.Env.PATH_imageres, "WAVE", My.Env.WVista ? 5051 : 5080);
                    try
                    {
                        using (var ms = new MemoryStream(SoundBytes))
                        {
                            SP = new SoundPlayer(ms);
                            SP.Load();
                            SP.Play();
                        }
                    }
                    catch
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
                if (!My.Env.WXP)
                {

                    try
                    {
                        using (var FS = new FileStream(My.Env.PATH_appData + @"\WindowsStartup_Backup.wav", FileMode.Open, FileAccess.Read))
                        {
                            SP = new SoundPlayer(FS);
                            SP.Load();
                            SP.Play();
                        }
                    }
                    catch (Exception ex)
                    {
                        AltPlayingMethod = true;
                        NativeMethods.DLLFunc.PlayAudio(My.Env.PATH_appData + @"\WindowsStartup_Backup.wav");
                    }

                }
            }

            else if ((snd ?? "") == (((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0).Text ?? ""))
            {

                if (File.Exists(snd))
                {

                    if (SP is not null)
                    {
                        SP.Stop();
                        SP.Dispose();
                    }

                    try
                    {
                        using (var FS = new FileStream(snd, FileMode.Open, FileAccess.Read))
                        {
                            SP = new SoundPlayer(FS);
                            SP.Load();
                            SP.Play();
                        }
                    }
                    catch (Exception ex)
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
                        SP.Stop();
                        SP.Dispose();
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
                    SP.Stop();
                    SP.Dispose();
                }

                try
                {
                    using (var FS = new FileStream(snd, FileMode.Open, FileAccess.Read))
                    {
                        SP = new SoundPlayer(FS);
                        SP.Load();
                        SP.Play();
                    }
                }
                catch
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
                    SP.Stop();
                    SP.Dispose();
                }
            }
        }

        public void StopPlayer(object sender, EventArgs e)
        {
            if (AltPlayingMethod)
                NativeMethods.DLLFunc.StopAudio();

            if (SP is not null)
            {
                SP.Stop();
                SP.Dispose();
            }
        }

        public void BrowseForWAV(object sender, EventArgs e)
        {
            {
                var temp = ((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0);
                if (File.Exists(temp.Text))
                {
                    OpenFileDialog2.FileName = new FileInfo(temp.Text).Name;
                    OpenFileDialog2.InitialDirectory = new FileInfo(temp.Text).DirectoryName;
                }
            }

            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                snd = OpenFileDialog2.FileName;

                ((UI.WP.Button)sender).Parent.Controls.OfType<UI.WP.TextBox>().ElementAt(0).Text = snd;
                try
                {
                    using (var FS = new FileStream(snd, FileMode.Open, FileAccess.Read))
                    {
                        SP = new SoundPlayer(FS);
                        SP.Load();
                        SP.Play();
                    }
                }
                catch (Exception ex)
                {
                    AltPlayingMethod = true;
                    NativeMethods.DLLFunc.PlayAudio(snd);
                }
            }
        }
        #endregion

        private void Sounds_Editor_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
            ApplyFromTM(My.Env.TM);
            CheckBox35_SFC.Checked = My.Env.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound;

            // Remove handler to avoid doubling/tripling events
            foreach (TabPage page in TabControl1.TabPages)
            {
                foreach (UI.WP.GroupBox pnl in page.Controls.OfType<UI.WP.GroupBox>())
                {
                    foreach (UI.WP.Button btn in pnl.Controls.OfType<UI.WP.Button>())
                    {
                        try
                        {
                            if ((btn.Tag ?? "").ToString() == "1")
                                btn.Click -= PressPlay;
                            if ((btn.Tag ?? "").ToString() == "2")
                                btn.Click -= StopPlayer;
                            if ((btn.Tag ?? "").ToString() == "3")
                                btn.Click -= BrowseForWAV;
                        }
                        catch
                        {
                        }
                    }
                }
            }

            foreach (TabPage page in TabControl1.TabPages)
            {
                foreach (UI.WP.GroupBox pnl in page.Controls.OfType<UI.WP.GroupBox>())
                {
                    foreach (UI.WP.Button btn in pnl.Controls.OfType<UI.WP.Button>())
                    {
                        if ((btn.Tag ?? "").ToString() == "1")
                            btn.Click += PressPlay;
                        if ((btn.Tag ?? "").ToString() == "2")
                            btn.Click += StopPlayer;
                        if ((btn.Tag ?? "").ToString() == "3")
                            btn.Click += BrowseForWAV;
                    }
                }
            }
        }

        private void Sounds_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage page in TabControl1.TabPages)
            {
                foreach (UI.WP.GroupBox pnl in page.Controls.OfType<UI.WP.GroupBox>())
                {
                    foreach (UI.WP.Button btn in pnl.Controls.OfType<UI.WP.Button>())
                    {
                        if ((btn.Tag ?? "").ToString() == "1")
                            btn.Click -= PressPlay;
                        if ((btn.Tag ?? "").ToString() == "2")
                            btn.Click -= StopPlayer;
                        if ((btn.Tag ?? "").ToString() == "3")
                            btn.Click -= BrowseForWAV;
                    }
                }
            }
        }

        public void ApplyFromTM(Theme.Manager TM)
        {
            ApplyFromTM(TM.Sounds);
        }

        public void ApplyFromTM(Theme.Structures.Sounds Sounds)
        {
            SoundsEnabled.Checked = Sounds.Enabled;
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

            CheckBox1.Checked = Sounds.Snd_Win_SystemExit_TaskMgmt;
            CheckBox2.Checked = Sounds.Snd_Win_WindowsLogoff_TaskMgmt;
            CheckBox3.Checked = Sounds.Snd_Win_WindowsLogon_TaskMgmt;
            CheckBox4.Checked = Sounds.Snd_Win_WindowsUnlock_TaskMgmt;

            TextBox84.Text = Sounds.Snd_ChargerConnected;

        }

        public void ApplyToTM(Theme.Manager TM)
        {
            {
                ref var temp = ref TM.Sounds;
                temp.Enabled = SoundsEnabled.Checked;
                temp.Snd_Win_SystemStart = TextBox1.Text;
                temp.Snd_Imageres_SystemStart = TextBox2.Text;
                temp.Snd_Win_SystemExit = TextBox3.Text;
                temp.Snd_Win_WindowsLogoff = TextBox4.Text;
                temp.Snd_Win_WindowsLogon = TextBox5.Text;
                temp.Snd_Win_WindowsUnlock = TextBox6.Text;
                temp.Snd_Win_ChangeTheme = TextBox64.Text;
                temp.Snd_Win_SystemQuestion = TextBox7.Text;
                temp.Snd_Win_SystemExclamation = TextBox8.Text;
                temp.Snd_Win_SystemAsterisk = TextBox9.Text;
                temp.Snd_Win_SystemHand = TextBox66.Text;
                temp.Snd_Win_SystemNotification = TextBox10.Text;
                temp.Snd_Win_WindowsUAC = TextBox11.Text;
                temp.Snd_Win_Open = TextBox16.Text;
                temp.Snd_Win_Close = TextBox15.Text;
                temp.Snd_Win_Maximize = TextBox14.Text;
                temp.Snd_Win_Minimize = TextBox13.Text;
                temp.Snd_Win_RestoreDown = TextBox12.Text;
                temp.Snd_Win_RestoreUp = TextBox17.Text;
                temp.Snd_Win_MenuPopup = TextBox53.Text;
                temp.Snd_Win_MenuCommand = TextBox54.Text;
                temp.Snd_Win_Default = TextBox55.Text;
                temp.Snd_Win_Notification_Default = TextBox23.Text;
                temp.Snd_Win_Notification_IM = TextBox22.Text;
                temp.Snd_Win_MessageNudge = TextBox21.Text;
                temp.Snd_Win_Notification_Mail = TextBox20.Text;
                temp.Snd_Win_MailBeep = TextBox65.Text;
                temp.Snd_Win_Notification_Proximity = TextBox19.Text;
                temp.Snd_Win_Notification_Reminder = TextBox18.Text;
                temp.Snd_Win_Notification_SMS = TextBox24.Text;
                temp.Snd_Win_Notification_Looping_Alarm = TextBox31.Text;
                temp.Snd_Win_Notification_Looping_Alarm2 = TextBox30.Text;
                temp.Snd_Win_Notification_Looping_Alarm3 = TextBox29.Text;
                temp.Snd_Win_Notification_Looping_Alarm4 = TextBox28.Text;
                temp.Snd_Win_Notification_Looping_Alarm5 = TextBox27.Text;
                temp.Snd_Win_Notification_Looping_Alarm6 = TextBox26.Text;
                temp.Snd_Win_Notification_Looping_Alarm7 = TextBox25.Text;
                temp.Snd_Win_Notification_Looping_Alarm8 = TextBox34.Text;
                temp.Snd_Win_Notification_Looping_Alarm9 = TextBox33.Text;
                temp.Snd_Win_Notification_Looping_Alarm10 = TextBox32.Text;
                temp.Snd_Win_Notification_Looping_Call = TextBox44.Text;
                temp.Snd_Win_Notification_Looping_Call2 = TextBox43.Text;
                temp.Snd_Win_Notification_Looping_Call3 = TextBox42.Text;
                temp.Snd_Win_Notification_Looping_Call4 = TextBox41.Text;
                temp.Snd_Win_Notification_Looping_Call5 = TextBox40.Text;
                temp.Snd_Win_Notification_Looping_Call6 = TextBox39.Text;
                temp.Snd_Win_Notification_Looping_Call7 = TextBox38.Text;
                temp.Snd_Win_Notification_Looping_Call8 = TextBox37.Text;
                temp.Snd_Win_Notification_Looping_Call9 = TextBox36.Text;
                temp.Snd_Win_Notification_Looping_Call10 = TextBox35.Text;
                temp.Snd_Win_DeviceConnect = TextBox45.Text;
                temp.Snd_Win_DeviceDisconnect = TextBox46.Text;
                temp.Snd_Win_DeviceFail = TextBox47.Text;
                temp.Snd_Win_LowBatteryAlarm = TextBox48.Text;
                temp.Snd_Win_CriticalBatteryAlarm = TextBox49.Text;
                temp.Snd_Win_PrintComplete = TextBox50.Text;
                temp.Snd_Win_FaxBeep = TextBox51.Text;
                temp.Snd_Win_ProximityConnection = TextBox52.Text;
                temp.Snd_Explorer_Navigating = TextBox62.Text;
                temp.Snd_Explorer_EmptyRecycleBin = TextBox61.Text;
                temp.Snd_Explorer_MoveMenuItem = TextBox56.Text;
                temp.Snd_Explorer_ActivatingDocument = TextBox60.Text;
                temp.Snd_Win_ShowBand = TextBox63.Text;
                temp.Snd_Explorer_SecurityBand = TextBox59.Text;
                temp.Snd_Explorer_BlockedPopup = TextBox58.Text;
                temp.Snd_Explorer_FeedDiscovered = TextBox57.Text;
                temp.Snd_Win_AppGPFault = TextBox68.Text;
                temp.Snd_Win_CCSelect = TextBox67.Text;
                temp.Snd_Explorer_SearchProviderDiscovered = TextBox75.Text;
                temp.Snd_Explorer_FaxNew = TextBox76.Text;
                temp.Snd_Explorer_FaxSent = TextBox77.Text;
                temp.Snd_Explorer_FaxLineRings = TextBox79.Text;
                temp.Snd_Explorer_FaxError = TextBox78.Text;
                temp.Snd_NetMeeting_PersonJoins = TextBox83.Text;
                temp.Snd_NetMeeting_PersonLeaves = TextBox82.Text;
                temp.Snd_NetMeeting_ReceiveCall = TextBox80.Text;
                temp.Snd_NetMeeting_ReceiveRequestToJoin = TextBox81.Text;
                temp.Snd_SpeechRec_DisNumbersSound = TextBox70.Text;
                temp.Snd_SpeechRec_PanelSound = TextBox74.Text;
                temp.Snd_SpeechRec_MisrecoSound = TextBox69.Text;
                temp.Snd_SpeechRec_HubOffSound = TextBox73.Text;
                temp.Snd_SpeechRec_HubOnSound = TextBox72.Text;
                temp.Snd_SpeechRec_HubSleepSound = TextBox71.Text;

                temp.Snd_Win_SystemExit_TaskMgmt = CheckBox1.Checked;
                temp.Snd_Win_WindowsLogoff_TaskMgmt = CheckBox2.Checked;
                temp.Snd_Win_WindowsLogon_TaskMgmt = CheckBox3.Checked;
                temp.Snd_Win_WindowsUnlock_TaskMgmt = CheckBox4.Checked;
                temp.Snd_ChargerConnected = TextBox84.Text;
            }
        }

        private void ScrSvrEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? My.Resources.checker_enabled : My.Resources.checker_disabled;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                ApplyFromTM(TMx);
                TMx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyFromTM(TMx);
            TMx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            using (var _Def = Theme.Default.From(My.Env.PreviewStyle))
            {
                ApplyFromTM(_Def);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            My.Env.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = CheckBox35_SFC.Checked;
            My.Env.Settings.Save(WPSettings.Mode.Registry);

            ApplyToTM(My.Env.TM);
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            My.Env.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = CheckBox35_SFC.Checked;
            My.Env.Settings.Save(WPSettings.Mode.Registry);

            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyToTM(TMx);
            ApplyToTM(My.Env.TM);
            TMx.Sounds.Apply();
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "Default";
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "Current";
        }

        private void Button259_Click(object sender, EventArgs e)
        {
            if (OpenThemeDialog.ShowDialog() == DialogResult.OK)
            {
                using (var _Def = Theme.Default.From(My.Env.PreviewStyle))
                {
                    GetFromClassicThemeFile(OpenThemeDialog.FileName, _Def.Sounds);
                }
            }
        }


        public void GetFromClassicThemeFile(string File, Theme.Structures.Sounds _DefaultSounds)
        {
            using (var _ini = new INI(File))
            {
                var snd = new Theme.Structures.Sounds();

                string Scope_Win = @"AppEvents\Schemes\Apps\.Default\{0}\.Current";
                snd.Snd_Win_Default = _ini.IniReadValue(string.Format(Scope_Win, ".Default"), "DefaultValue", _DefaultSounds.Snd_Win_Default).PhrasePath();
                snd.Snd_Win_AppGPFault = _ini.IniReadValue(string.Format(Scope_Win, "AppGPFault"), "DefaultValue", _DefaultSounds.Snd_Win_AppGPFault).PhrasePath();
                snd.Snd_Win_CCSelect = _ini.IniReadValue(string.Format(Scope_Win, "CCSelect"), "DefaultValue", _DefaultSounds.Snd_Win_CCSelect).PhrasePath();
                snd.Snd_Win_ChangeTheme = _ini.IniReadValue(string.Format(Scope_Win, "ChangeTheme"), "DefaultValue", _DefaultSounds.Snd_Win_ChangeTheme).PhrasePath();
                snd.Snd_Win_Close = _ini.IniReadValue(string.Format(Scope_Win, "Close"), "DefaultValue", _DefaultSounds.Snd_Win_Close).PhrasePath();
                snd.Snd_Win_CriticalBatteryAlarm = _ini.IniReadValue(string.Format(Scope_Win, "CriticalBatteryAlarm"), "DefaultValue", _DefaultSounds.Snd_Win_CriticalBatteryAlarm).PhrasePath();
                snd.Snd_Win_DeviceConnect = _ini.IniReadValue(string.Format(Scope_Win, "DeviceConnect"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceConnect).PhrasePath();
                snd.Snd_Win_DeviceDisconnect = _ini.IniReadValue(string.Format(Scope_Win, "DeviceDisconnect"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceDisconnect).PhrasePath();
                snd.Snd_Win_DeviceFail = _ini.IniReadValue(string.Format(Scope_Win, "DeviceFail"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceFail).PhrasePath();
                snd.Snd_Win_FaxBeep = _ini.IniReadValue(string.Format(Scope_Win, "FaxBeep"), "DefaultValue", _DefaultSounds.Snd_Win_FaxBeep).PhrasePath();
                snd.Snd_Win_LowBatteryAlarm = _ini.IniReadValue(string.Format(Scope_Win, "LowBatteryAlarm"), "DefaultValue", _DefaultSounds.Snd_Win_LowBatteryAlarm).PhrasePath();
                snd.Snd_Win_MailBeep = _ini.IniReadValue(string.Format(Scope_Win, "MailBeep"), "DefaultValue", _DefaultSounds.Snd_Win_MailBeep).PhrasePath();
                snd.Snd_Win_Maximize = _ini.IniReadValue(string.Format(Scope_Win, "Maximize"), "DefaultValue", _DefaultSounds.Snd_Win_Maximize).PhrasePath();
                snd.Snd_Win_MenuCommand = _ini.IniReadValue(string.Format(Scope_Win, "MenuCommand"), "DefaultValue", _DefaultSounds.Snd_Win_MenuCommand).PhrasePath();
                snd.Snd_Win_MenuPopup = _ini.IniReadValue(string.Format(Scope_Win, "MenuPopup"), "DefaultValue", _DefaultSounds.Snd_Win_MenuPopup).PhrasePath();
                snd.Snd_Win_MessageNudge = _ini.IniReadValue(string.Format(Scope_Win, "MessageNudge"), "DefaultValue", _DefaultSounds.Snd_Win_MessageNudge).PhrasePath();
                snd.Snd_Win_Minimize = _ini.IniReadValue(string.Format(Scope_Win, "Minimize"), "DefaultValue", _DefaultSounds.Snd_Win_Minimize).PhrasePath();
                snd.Snd_Win_Notification_Default = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Default"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Default).PhrasePath();
                snd.Snd_Win_Notification_IM = _ini.IniReadValue(string.Format(Scope_Win, "Notification.IM"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_IM).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm2 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm2"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm2).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm3 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm3"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm3).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm4 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm4"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm4).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm5 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm5"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm5).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm6 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm6"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm6).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm7 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm7"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm7).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm8 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm8"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm8).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm9 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm9"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm9).PhrasePath();
                snd.Snd_Win_Notification_Looping_Alarm10 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Alarm10"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm10).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call2 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call2"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call2).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call3 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call3"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call3).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call4 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call4"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call4).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call5 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call5"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call5).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call6 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call6"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call6).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call7 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call7"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call7).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call8 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call8"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call8).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call9 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call9"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call9).PhrasePath();
                snd.Snd_Win_Notification_Looping_Call10 = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Looping.Call10"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call10).PhrasePath();
                snd.Snd_Win_Notification_Mail = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Mail"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Mail).PhrasePath();
                snd.Snd_Win_Notification_Proximity = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Proximity"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Proximity).PhrasePath();
                snd.Snd_Win_Notification_Reminder = _ini.IniReadValue(string.Format(Scope_Win, "Notification.Reminder"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Reminder).PhrasePath();
                snd.Snd_Win_Notification_SMS = _ini.IniReadValue(string.Format(Scope_Win, "Notification.SMS"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_SMS).PhrasePath();
                snd.Snd_Win_Open = _ini.IniReadValue(string.Format(Scope_Win, "Open"), "DefaultValue", _DefaultSounds.Snd_Win_Open).PhrasePath();
                snd.Snd_Win_PrintComplete = _ini.IniReadValue(string.Format(Scope_Win, "PrintComplete"), "DefaultValue", _DefaultSounds.Snd_Win_PrintComplete).PhrasePath();
                snd.Snd_Win_ProximityConnection = _ini.IniReadValue(string.Format(Scope_Win, "ProximityConnection"), "DefaultValue", _DefaultSounds.Snd_Win_ProximityConnection).PhrasePath();
                snd.Snd_Win_RestoreDown = _ini.IniReadValue(string.Format(Scope_Win, "RestoreDown"), "DefaultValue", _DefaultSounds.Snd_Win_RestoreDown).PhrasePath();
                snd.Snd_Win_RestoreUp = _ini.IniReadValue(string.Format(Scope_Win, "RestoreUp"), "DefaultValue", _DefaultSounds.Snd_Win_RestoreUp).PhrasePath();
                snd.Snd_Win_ShowBand = _ini.IniReadValue(string.Format(Scope_Win, "ShowBand"), "DefaultValue", _DefaultSounds.Snd_Win_ShowBand).PhrasePath();
                snd.Snd_Win_SystemAsterisk = _ini.IniReadValue(string.Format(Scope_Win, "SystemAsterisk"), "DefaultValue", _DefaultSounds.Snd_Win_SystemAsterisk).PhrasePath();
                snd.Snd_Win_SystemExclamation = _ini.IniReadValue(string.Format(Scope_Win, "SystemExclamation"), "DefaultValue", _DefaultSounds.Snd_Win_SystemExclamation).PhrasePath();
                snd.Snd_Win_SystemExit = _ini.IniReadValue(string.Format(Scope_Win, "SystemExit"), "DefaultValue", _DefaultSounds.Snd_Win_SystemExit).PhrasePath();
                snd.Snd_Win_SystemStart = _ini.IniReadValue(string.Format(Scope_Win, "SystemStart"), "DefaultValue", _DefaultSounds.Snd_Win_SystemStart).PhrasePath();
                if (System.IO.File.Exists(snd.Snd_Win_SystemStart))
                    snd.Snd_Imageres_SystemStart = snd.Snd_Win_SystemStart;
                else
                    snd.Snd_Imageres_SystemStart = "Current";
                snd.Snd_Win_SystemHand = _ini.IniReadValue(string.Format(Scope_Win, "SystemHand"), "DefaultValue", _DefaultSounds.Snd_Win_SystemHand).PhrasePath();
                snd.Snd_Win_SystemNotification = _ini.IniReadValue(string.Format(Scope_Win, "SystemNotification"), "DefaultValue", _DefaultSounds.Snd_Win_SystemNotification).PhrasePath();
                snd.Snd_Win_SystemQuestion = _ini.IniReadValue(string.Format(Scope_Win, "SystemQuestion"), "DefaultValue", _DefaultSounds.Snd_Win_SystemQuestion).PhrasePath();
                snd.Snd_Win_WindowsLogoff = _ini.IniReadValue(string.Format(Scope_Win, "WindowsLogoff"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsLogoff).PhrasePath();
                snd.Snd_Win_WindowsLogon = _ini.IniReadValue(string.Format(Scope_Win, "WindowsLogon"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsLogon).PhrasePath();
                snd.Snd_Win_WindowsUAC = _ini.IniReadValue(string.Format(Scope_Win, "WindowsUAC"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsUAC).PhrasePath();
                snd.Snd_Win_WindowsUnlock = _ini.IniReadValue(string.Format(Scope_Win, "WindowsUnlock"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsUnlock).PhrasePath();

                string Scope_Explorer = @"AppEvents\Schemes\Apps\Explorer\{0}\.Current";
                snd.Snd_Explorer_ActivatingDocument = _ini.IniReadValue(string.Format(Scope_Explorer, "ActivatingDocument"), "DefaultValue", _DefaultSounds.Snd_Explorer_ActivatingDocument).PhrasePath();
                snd.Snd_Explorer_BlockedPopup = _ini.IniReadValue(string.Format(Scope_Explorer, "BlockedPopup"), "DefaultValue", _DefaultSounds.Snd_Explorer_BlockedPopup).PhrasePath();
                snd.Snd_Explorer_EmptyRecycleBin = _ini.IniReadValue(string.Format(Scope_Explorer, "EmptyRecycleBin"), "DefaultValue", _DefaultSounds.Snd_Explorer_EmptyRecycleBin).PhrasePath();
                snd.Snd_Explorer_FeedDiscovered = _ini.IniReadValue(string.Format(Scope_Explorer, "FeedDiscovered"), "DefaultValue", _DefaultSounds.Snd_Explorer_FeedDiscovered).PhrasePath();
                snd.Snd_Explorer_MoveMenuItem = _ini.IniReadValue(string.Format(Scope_Explorer, "MoveMenuItem"), "DefaultValue", _DefaultSounds.Snd_Explorer_MoveMenuItem).PhrasePath();
                snd.Snd_Explorer_Navigating = _ini.IniReadValue(string.Format(Scope_Explorer, "Navigating"), "DefaultValue", _DefaultSounds.Snd_Explorer_Navigating).PhrasePath();
                snd.Snd_Explorer_SecurityBand = _ini.IniReadValue(string.Format(Scope_Explorer, "SecurityBand"), "DefaultValue", _DefaultSounds.Snd_Explorer_SecurityBand).PhrasePath();
                snd.Snd_Explorer_SearchProviderDiscovered = _ini.IniReadValue(string.Format(Scope_Explorer, "SearchProviderDiscovered"), "DefaultValue", _DefaultSounds.Snd_Explorer_SearchProviderDiscovered).PhrasePath();
                snd.Snd_Explorer_FaxError = _ini.IniReadValue(string.Format(Scope_Explorer, "FaxError"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxError).PhrasePath();
                snd.Snd_Explorer_FaxLineRings = _ini.IniReadValue(string.Format(Scope_Explorer, "FaxLineRings"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxLineRings).PhrasePath();
                snd.Snd_Explorer_FaxNew = _ini.IniReadValue(string.Format(Scope_Explorer, "FaxNew"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxNew).PhrasePath();
                snd.Snd_Explorer_FaxSent = _ini.IniReadValue(string.Format(Scope_Explorer, "FaxSent"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxSent).PhrasePath();

                string Scope_NetMeeting = @"AppEvents\Schemes\Apps\Conf\{0}\.Current";
                snd.Snd_NetMeeting_PersonJoins = _ini.IniReadValue(string.Format(Scope_NetMeeting, "Person Joins"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_PersonJoins).PhrasePath();
                snd.Snd_NetMeeting_PersonLeaves = _ini.IniReadValue(string.Format(Scope_NetMeeting, "Person Leaves"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_PersonLeaves).PhrasePath();
                snd.Snd_NetMeeting_ReceiveCall = _ini.IniReadValue(string.Format(Scope_NetMeeting, "Receive Call"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_ReceiveCall).PhrasePath();
                snd.Snd_NetMeeting_ReceiveRequestToJoin = _ini.IniReadValue(string.Format(Scope_NetMeeting, "Receive Request to Join"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_ReceiveRequestToJoin).PhrasePath();

                string Scope_SpeechRec = @"AppEvents\Schemes\Apps\sapisvr\{0}\.current";
                snd.Snd_SpeechRec_DisNumbersSound = _ini.IniReadValue(string.Format(Scope_SpeechRec, "DisNumbersSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_DisNumbersSound).PhrasePath();
                snd.Snd_SpeechRec_HubOffSound = _ini.IniReadValue(string.Format(Scope_SpeechRec, "HubOffSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubOffSound).PhrasePath();
                snd.Snd_SpeechRec_HubOnSound = _ini.IniReadValue(string.Format(Scope_SpeechRec, "HubOnSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubOnSound).PhrasePath();
                snd.Snd_SpeechRec_HubSleepSound = _ini.IniReadValue(string.Format(Scope_SpeechRec, "HubSleepSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubSleepSound).PhrasePath();
                snd.Snd_SpeechRec_MisrecoSound = _ini.IniReadValue(string.Format(Scope_SpeechRec, "MisrecoSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_MisrecoSound).PhrasePath();
                snd.Snd_SpeechRec_PanelSound = _ini.IniReadValue(string.Format(Scope_SpeechRec, "PanelSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_PanelSound).PhrasePath();

                ApplyFromTM(snd);
                GC.Collect();
                GC.SuppressFinalize(snd);
            }

        }

        private void Sounds_Editor_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-Sounds");
        }
    }
}