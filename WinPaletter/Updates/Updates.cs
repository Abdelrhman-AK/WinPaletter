﻿using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class Updates
    {
        private WebClient WebCL;
        private WebClient _UC;

        private WebClient UC
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _UC;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_UC != null)
                {
                    _UC.DownloadProgressChanged -= UC_DownloadProgressChanged;
                    _UC.DownloadFileCompleted -= UC_DownloadFileCompleted;
                }

                _UC = value;
                if (_UC != null)
                {
                    _UC.DownloadProgressChanged += UC_DownloadProgressChanged;
                    _UC.DownloadFileCompleted += UC_DownloadFileCompleted;
                }
            }
        }
        public string url = null;
        public string ver;
        private int StableInt, BetaInt, UpdateChannel;
        private string OldName;
        public decimal UpdateSize;
        public DateTime ReleaseDate;
        private bool _Shown = false;
        public List<string> ls = new List<string>();

        public Updates()
        {
            WebCL = new WebClient();
            UC = new WebClient();
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.MainFrm.NotifyUpdates.Visible = false;

            if (url is null)
            {
                Cursor = Cursors.AppStarting;

                My.Env.Animator.HideSync(AlertBox2, true);
                My.Env.Animator.HideSync(Button1, true);
                My.Env.Animator.HideSync(Button3, true);
                My.Env.Animator.HideSync(Panel1, true);
                ProgressBar1.Visible = false;
                ProgressBar1.Value = 0;

                StableInt = 0;
                BetaInt = 0;
                UpdateChannel = 0;

                try
                {
                    if (IsNetworkAvailable())
                    {
                        Label17.SetText(My.Env.Lang.Checking);

                        ls = WebCL.DownloadString(My.Resources.Link_Updates).CList();

                        for (int x = 0, loopTo = ls.Count - 1; x <= loopTo; x++)
                        {
                            if (!string.IsNullOrEmpty(ls[x]) & !(ls[x].IndexOf("#") == 0))
                            {
                                if (ls[x].Split(' ')[0] == "Stable")
                                    StableInt = x;
                                if (ls[x].Split(' ')[0] == "Beta")
                                    BetaInt = x;
                            }
                        }

                        if (My.Env.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Stable)
                            UpdateChannel = StableInt;
                        if (My.Env.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Beta)
                            UpdateChannel = BetaInt;

                        ver = ls[UpdateChannel].Split(' ')[1];

                        if (new Version(ver) > new Version(My.Env.AppVersion))
                        {
                            url = ls[UpdateChannel].Split(' ')[4];
                            UpdateSize = Conversions.ToDecimal(ls[UpdateChannel].Split(' ')[2]);
                            ReleaseDate = DateTime.FromBinary(Conversions.ToLong(ls[UpdateChannel].Split(' ')[3]));

                            Label7.Text = UpdateSize + " " + My.Env.Lang.MBSizeUnit;
                            Label9.Text = ReleaseDate.ToLongDateString();

                            LinkLabel3.Visible = true;

                            My.Env.Animator.Show(Panel1, true);
                            Button1.Text = My.Env.Lang.DoAction_Update;
                            AlertBox2.Text = string.Format("{0}. {1} {2}", My.Env.Lang.NewUpdate, My.Env.Lang.Version, ver);
                            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Indigo;
                        }
                        else
                        {
                            Label7.Text = "";
                            Label9.Text = "";
                            url = null;
                            Button1.Text = My.Env.Lang.CheckForUpdates;
                            AlertBox2.Text = string.Format(My.Env.Lang.NoUpdateAvailable);
                            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Success;
                        }

                        Label17.SetText(Text);
                    }
                    else
                    {
                        Label7.Text = "";
                        Label9.Text = "";
                        url = null;
                        Button1.Text = My.Env.Lang.CheckForUpdates;
                        AlertBox2.Text = string.Format(My.Env.Lang.NetworkError);
                        AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
                    }
                }
                catch (Exception ex)
                {
                    Label7.Text = "";
                    Label9.Text = "";
                    url = null;
                    Button1.Text = My.Env.Lang.CheckForUpdates;
                    AlertBox2.Text = string.Format(My.Env.Lang.ServerError);
                    AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
                    My.MyProject.Forms.BugReport.ThrowError(ex);
                }

                My.Env.Animator.Show(AlertBox2, true);
                My.Env.Animator.Show(Button1, true);
                My.Env.Animator.ShowSync(Button3, true);

                Cursor = Cursors.Default;
            }
            else
            {
                ProgressBar1.Visible = false;
                ProgressBar1.Value = 0;

                if (RadioButton1.Checked)
                {
                    Button1.Enabled = false;
                    Panel1.Enabled = false;
                    ProgressBar1.Visible = true;
                    OldName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    try
                    {
                        if (System.IO.File.Exists("oldWinpaletter.trash"))
                            FileSystem.Kill("oldWinpaletter.trash");
                        if (System.IO.File.Exists("oldWinpaletter_2.trash"))
                            FileSystem.Kill("oldWinpaletter_2.trash");
                    }
                    catch
                    {
                    }
                    My.MyProject.Computer.FileSystem.RenameFile(OldName, "oldWinpaletter.trash");
                    UC.DownloadFileAsync(new Uri(url), OldName);
                }

                if (RadioButton2.Checked)
                {
                    SaveFileDialog1.FileName = string.Format("WinPaletter ({0})", ver);

                    if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Panel1.Enabled = false;
                        Button1.Enabled = false;
                        ProgressBar1.Visible = true;
                        UC.DownloadFileAsync(new Uri(url), SaveFileDialog1.FileName);
                    }
                    else
                    {
                        ProgressBar1.Visible = false;
                    }
                }

                if (RadioButton3.Checked)
                {
                    Process.Start(My.Resources.Link_Releases);
                }

            }

        }

        private void Updates_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            UC = new WebClient();
            LinkLabel3.Visible = false;
            string F = My.Env.Lang.RightToLeft ? "{1}: {0}" : "{0} {1}";
            Label3.Text = string.Format(F, My.Env.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Stable ? My.Env.Lang.Stable : My.Env.Lang.Beta, My.Env.Lang.Channel);
            CheckBox1.Checked = My.Env.Settings.Updates.AutoCheck;
            _Shown = false;
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox2.Visible = false;
            Panel1.Visible = false;
            Label7.Text = "";
            Label9.Text = "";
            url = null;
            Button1.Enabled = true;
            Panel1.Enabled = true;

            Button1.Text = My.Env.Lang.CheckForUpdates;
            Label2.Text = My.Env.AppVersion;

            if (ls.Count > 0)
            {
                StableInt = 0;
                BetaInt = 0;
                UpdateChannel = 0;

                for (int x = 0, loopTo = ls.Count - 1; x <= loopTo; x++)
                {
                    if (!string.IsNullOrEmpty(ls[x]) & !(ls[x].IndexOf("#") == 0))
                    {
                        if (ls[x].Split(' ')[0] == "Stable")
                            StableInt = x;
                        if (ls[x].Split(' ')[0] == "Beta")
                            BetaInt = x;
                    }
                }

                if (My.Env.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Stable)
                    UpdateChannel = StableInt;
                if (My.Env.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Beta)
                    UpdateChannel = BetaInt;

                url = ls[UpdateChannel].Split(' ')[4];
                UpdateSize = Conversions.ToDecimal(ls[UpdateChannel].Split(' ')[2]);
                ReleaseDate = DateTime.FromBinary(Conversions.ToLong(ls[UpdateChannel].Split(' ')[3]));
                ver = ls[UpdateChannel].Split(' ')[1];

                Label7.Text = UpdateSize + " " + My.Env.Lang.MBSizeUnit;
                Label9.Text = ReleaseDate.ToLongDateString() + " " + ReleaseDate.ToLongTimeString();

                LinkLabel3.Visible = true;

                My.Env.Animator.Show(Panel1, true);
                Button1.Text = My.Env.Lang.DoAction_Update;
                AlertBox2.Text = string.Format("{0}. {1} {2}", My.Env.Lang.NewUpdate, My.Env.Lang.Version, ver);
                AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Indigo;

                My.Env.Animator.Show(AlertBox2, true);
                My.Env.Animator.ShowSync(Button1, true);
            }

            if (My.Env.WXP)
            {
                AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
                AlertBox2.Visible = true;
                AlertBox2.Text = string.Format(My.Env.Lang.UpdatesOSNoTLS12, My.Env.Lang.OS_WinXP);
            }

            else if (My.Env.WVista)
            {
                AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
                AlertBox2.Visible = true;
                AlertBox2.Text = string.Format(My.Env.Lang.UpdatesOSNoTLS12, My.Env.Lang.OS_WinVista);
            }

        }

        private void Label3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var f = new Updates();
            Close();
            My.MyProject.Forms.SettingsX.ShowDialog();
            f.ShowDialog();
        }

        private void Updates_Shown(object sender, EventArgs e)
        {
            _Shown = true;
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            if (_Shown)
            {
                My.Env.Settings.Updates.AutoCheck = CheckBox1.Checked;
                My.Env.Settings.Updates.Save();
            }
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(My.Resources.Link_Changelog);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UC_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar1.Value = e.ProgressPercentage;
        }

        private void UC_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Button1.Enabled = true;
            Panel1.Enabled = true;
            ProgressBar1.Visible = false;
            ProgressBar1.Value = 0;
            if (RadioButton2.Checked)
                WPStyle.MsgBox(My.Env.Lang.Msgbox_Downloaded, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (RadioButton1.Checked & !Disturbed)
            {
                Process.Start(OldName);
                using (var Prc = Process.GetCurrentProcess())
                {
                    Prc.Kill();
                }
            }
        }

        private void Updates_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisturbActions();
            ls.Clear();
        }

        private void Updates_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisturbActions();
            ls.Clear();
        }

        private bool Disturbed = false;

        public void DisturbActions()
        {
            if (UC.IsBusy)
            {
                Disturbed = true;

                UC.CancelAsync();
                UC.Dispose();

                if (RadioButton1.Checked)
                {
                    try
                    {
                        if (System.IO.File.Exists("oldWinpaletter_2.trash"))
                            FileSystem.Kill("oldWinpaletter_2.trash");
                    }
                    catch
                    {
                    }
                    My.MyProject.Computer.FileSystem.RenameFile(OldName, "oldWinpaletter_2.trash");
                    My.MyProject.Computer.FileSystem.RenameFile("oldWinpaletter.trash", OldName.Split('\\').Last());
                    try
                    {
                        if (System.IO.File.Exists("oldWinpaletter_2.trash"))
                            FileSystem.Kill("oldWinpaletter_2.trash");
                        if (System.IO.File.Exists("oldWinpaletter.trash"))
                            FileSystem.Kill("oldWinpaletter.trash");
                    }
                    catch
                    {
                    }
                }

            }

            if (RadioButton2.Checked)
            {
                if (System.IO.File.Exists(SaveFileDialog1.FileName))
                    FileSystem.Kill(SaveFileDialog1.FileName);
            }
        }

    }
}