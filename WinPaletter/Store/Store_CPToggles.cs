﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Store_CPToggles
    {
        public Theme.Manager TM;

        public Store_CPToggles()
        {
            InitializeComponent();
        }

        private void Store_CPToggles_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Opacity = 0d;
            Icon = My.MyProject.Forms.Store.Icon;

            CheckedListBox1.Items.Clear();

            if (TM.AppTheme.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_AppTheme, true);
            if (TM.LogonUI7.Enabled & (My.Env.W7 | My.Env.W8 | My.Env.W81))
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_LogonUI, true);
            if (TM.LogonUIXP.Enabled & My.Env.WXP)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_LogonUI, true);
            if (TM.Cursor_Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_Cursors, true);
            if (TM.Wallpaper.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_Wallpaper, true);
            if (TM.Sounds.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_Sounds, true);
            if (TM.ScreenSaver.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_ScreenSaver, true);
            if (TM.MetricsFonts.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_MetricsFonts, true);
            if (TM.CommandPrompt.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_CMD, true);
            if (TM.PowerShellx86.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_PS86, true);
            if (TM.PowerShellx64.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_PS64, true);
            if (TM.Terminal.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_TerminalStable, true);
            if (TM.TerminalPreview.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_TerminalPreview, true);
            if (TM.WindowsEffects.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_WindowsEffects, true);
            if (TM.AltTab.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_AltTab, true);

            if (CheckedListBox1.Items.Count == 0)
                Close();
            Opacity = 1d;

            CheckedListBox1.ForeColor = My.Env.Style.DarkMode ? Color.White : Color.Black;

            My.MyProject.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++)
            {
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_AppTheme)
                    TM.AppTheme.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_LogonUI)
                {
                    if (My.Env.W7 | My.Env.W8 | My.Env.W81)
                    {
                        TM.LogonUI7.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (My.Env.WXP)
                    {
                        TM.LogonUIXP.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                }

                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_Cursors)
                    TM.Cursor_Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_CMD)
                    TM.CommandPrompt.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_PS86)
                    TM.PowerShellx86.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_PS64)
                    TM.PowerShellx64.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_TerminalStable)
                    TM.Terminal.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_TerminalPreview)
                    TM.TerminalPreview.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_MetricsFonts)
                    TM.MetricsFonts.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_Wallpaper)
                    TM.Wallpaper.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_WindowsEffects)
                    TM.WindowsEffects.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_AltTab)
                    TM.AltTab.Enabled = CheckedListBox1.GetItemChecked(i);
            }

            My.MyProject.Forms.Store.selectedItem.TM = TM;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}