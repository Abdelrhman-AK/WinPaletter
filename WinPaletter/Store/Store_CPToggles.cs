using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Store_CPToggles
    {
        public CP CP;

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

            if (CP.AppTheme.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_AppTheme, true);
            if (CP.LogonUI7.Enabled & (My.Env.W7 | My.Env.W8 | My.Env.W81))
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_LogonUI, true);
            if (CP.LogonUIXP.Enabled & My.Env.WXP)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_LogonUI, true);
            if (CP.Cursor_Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_Cursors, true);
            if (CP.Wallpaper.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_Wallpaper, true);
            if (CP.Sounds.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_Sounds, true);
            if (CP.ScreenSaver.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_ScreenSaver, true);
            if (CP.MetricsFonts.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_MetricsFonts, true);
            if (CP.CommandPrompt.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_CMD, true);
            if (CP.PowerShellx86.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_PS86, true);
            if (CP.PowerShellx64.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_PS64, true);
            if (CP.Terminal.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_TerminalStable, true);
            if (CP.TerminalPreview.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_TerminalPreview, true);
            if (CP.WindowsEffects.Enabled)
                CheckedListBox1.Items.Add(My.Env.Lang.Store_Toggle_WindowsEffects, true);
            if (CP.AltTab.Enabled)
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
                    CP.AppTheme.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_LogonUI)
                {
                    if (My.Env.W7 | My.Env.W8 | My.Env.W81)
                    {
                        CP.LogonUI7.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (My.Env.WXP)
                    {
                        CP.LogonUIXP.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                }

                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_Cursors)
                    CP.Cursor_Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_CMD)
                    CP.CommandPrompt.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_PS86)
                    CP.PowerShellx86.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_PS64)
                    CP.PowerShellx64.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_TerminalStable)
                    CP.Terminal.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_TerminalPreview)
                    CP.TerminalPreview.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_MetricsFonts)
                    CP.MetricsFonts.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_Wallpaper)
                    CP.Wallpaper.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_WindowsEffects)
                    CP.WindowsEffects.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == My.Env.Lang.Store_Toggle_AltTab)
                    CP.AltTab.Enabled = CheckedListBox1.GetItemChecked(i);
            }

            My.MyProject.Forms.Store.selectedItem.CP = CP;
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