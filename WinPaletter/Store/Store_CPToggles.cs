using System;
using System.Drawing;
using System.Media;
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
            ApplyStyle(this);
            Opacity = 0d;
            using (Store formIcon = new()) { Icon = formIcon.Icon; }

            CheckedListBox1.Items.Clear();

            if (OS.W12 && TM.Windows12.Enabled) CheckedListBox1.Items.Add(string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win12), true);
            if (OS.W11 && TM.Windows11.Enabled) CheckedListBox1.Items.Add(string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win11), true);
            if (OS.W10 && TM.Windows10.Enabled) CheckedListBox1.Items.Add(string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win10), true);
            if (OS.W8x && TM.Windows81.Enabled) CheckedListBox1.Items.Add(string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win81), true);
            if (OS.W7 && TM.Windows7.Enabled) CheckedListBox1.Items.Add(string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win7), true);
            if (OS.WVista && TM.WindowsVista.Enabled) CheckedListBox1.Items.Add(string.Format(Program.Lang.WindowsColors, Program.Lang.OS_WinVista), true);
            if (OS.WXP && TM.WindowsXP.Enabled) CheckedListBox1.Items.Add(string.Format(Program.Lang.WindowsColors, Program.Lang.OS_WinXP), true);
            if (TM.Win32.Enabled) CheckedListBox1.Items.Add(Program.Lang.ClassicColors, true);

            if (TM.AppTheme.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_AppTheme, true);
            if (TM.LogonUI7.Enabled & (OS.W7 | OS.W8x))
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_LogonUI, true);
            if (TM.LogonUIXP.Enabled & OS.WXP)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_LogonUI, true);
            if (TM.Cursors.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_Cursors, true);
            if (TM.Wallpaper.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_Wallpaper, true);
            if (TM.Sounds.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_Sounds, true);
            if (TM.ScreenSaver.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_ScreenSaver, true);
            if (TM.MetricsFonts.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_MetricsFonts, true);
            if (TM.CommandPrompt.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_CMD, true);
            if (TM.PowerShellx86.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_PS86, true);
            if (TM.PowerShellx64.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_PS64, true);
            if (TM.Terminal.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_TerminalStable, true);
            if (TM.TerminalPreview.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_TerminalPreview, true);
            if (TM.WindowsEffects.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_WindowsEffects, true);
            if (TM.AltTab.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.Store_Toggle_AltTab, true);
            if (TM.Icons.Enabled)
                CheckedListBox1.Items.Add(Program.Lang.WindowsIcons, true);

            if (OS.W12 && TM.VisualStyles_12.Enabled) CheckedListBox1.Items.Add(Program.Lang.VisualStyles, true);
            if (OS.W11 && TM.VisualStyles_11.Enabled) CheckedListBox1.Items.Add(Program.Lang.VisualStyles, true);
            if (OS.W10 && TM.VisualStyles_10.Enabled) CheckedListBox1.Items.Add(Program.Lang.VisualStyles, true);
            if (OS.W8x && TM.VisualStyles_81.Enabled) CheckedListBox1.Items.Add(Program.Lang.VisualStyles, true);
            if (OS.W7 && TM.VisualStyles_7.Enabled) CheckedListBox1.Items.Add(Program.Lang.VisualStyles, true);
            if (OS.WVista && TM.VisualStyles_Vista.Enabled) CheckedListBox1.Items.Add(Program.Lang.VisualStyles, true);
            if (OS.WXP && TM.VisualStyles_XP.Enabled) CheckedListBox1.Items.Add(Program.Lang.VisualStyles, true);

            if (CheckedListBox1.Items.Count == 0)
                Close();
            Opacity = 1d;

            CheckedListBox1.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;

            SystemSounds.Exclamation.Play();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++)
            {
                if (OS.W12 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win12).ToLower())
                    TM.Windows12.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W11 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win11).ToLower())
                    TM.Windows11.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W10 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win10).ToLower())
                    TM.Windows10.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W8x && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win81).ToLower())
                    TM.Windows81.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W7 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.WindowsColors, Program.Lang.OS_Win7).ToLower())
                    TM.Windows7.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.WVista && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.WindowsColors, Program.Lang.OS_WinVista).ToLower())
                    TM.WindowsVista.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.WXP && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.WindowsColors, Program.Lang.OS_WinXP).ToLower())
                    TM.WindowsXP.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.ClassicColors)
                    TM.Win32.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_AppTheme)
                    TM.AppTheme.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_LogonUI)
                {
                    if (OS.W7 | OS.W8x)
                    {
                        TM.LogonUI7.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.WXP)
                    {
                        TM.LogonUIXP.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                }

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_Cursors)
                    TM.Cursors.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_CMD)
                    TM.CommandPrompt.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_PS86)
                    TM.PowerShellx86.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_PS64)
                    TM.PowerShellx64.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_TerminalStable)
                    TM.Terminal.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_TerminalPreview)
                    TM.TerminalPreview.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_MetricsFonts)
                    TM.MetricsFonts.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_Wallpaper)
                    TM.Wallpaper.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_WindowsEffects)
                    TM.WindowsEffects.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Store_Toggle_AltTab)
                    TM.AltTab.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.WindowsIcons)
                    TM.Icons.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W12 && CheckedListBox1.Items[i].ToString().ToLower() == Program.Lang.VisualStyles.ToLower())
                    TM.VisualStyles_12.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W11 && CheckedListBox1.Items[i].ToString().ToLower() == Program.Lang.VisualStyles.ToLower())
                    TM.VisualStyles_11.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W10 && CheckedListBox1.Items[i].ToString().ToLower() == Program.Lang.VisualStyles.ToLower())
                    TM.VisualStyles_10.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W8x && CheckedListBox1.Items[i].ToString().ToLower() == Program.Lang.VisualStyles.ToLower())
                    TM.VisualStyles_81.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W7 && CheckedListBox1.Items[i].ToString().ToLower() == Program.Lang.VisualStyles.ToLower())
                    TM.VisualStyles_7.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.WVista && CheckedListBox1.Items[i].ToString().ToLower() == Program.Lang.VisualStyles.ToLower())
                    TM.VisualStyles_Vista.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.WXP && CheckedListBox1.Items[i].ToString().ToLower() == Program.Lang.VisualStyles.ToLower())
                    TM.VisualStyles_XP.Enabled = CheckedListBox1.GetItemChecked(i);
            }

            Forms.Store.selectedItem.TM = TM;
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