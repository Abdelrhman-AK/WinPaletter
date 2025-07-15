using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// The form to toggle the aspects of the <see cref="Theme.Manager"/> instance before being applied by WinPaletter Store.
    /// </summary>
    public partial class Store_CPToggles
    {
        /// <summary>
        /// The <see cref="Theme.Manager"/> instance to be used.
        /// </summary>
        public Theme.Manager TM;

        /// <summary>
        /// Creates a new instance of <see cref="Store_CPToggles"/>.
        /// </summary>
        public Store_CPToggles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the list of enabled aspects but not the disabled ones in the <see cref="Theme.Manager"/> instance depending on the current OS and by using strings from <see cref="Program.Lang"/>.
        /// </summary>
        /// <param name="TM"></param>
        /// <returns></returns>
        public static List<string> EnabledAspects(Theme.Manager TM)
        {
            List<string> aspects_list = [];
            aspects_list.Clear();

            if (OS.W12 && TM.Windows12.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W12));
            if (OS.W11 && TM.Windows11.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W11));
            if (OS.W10 && TM.Windows10.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W10));
            if (OS.W81 && TM.Windows81.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W81));
            if (OS.W8 && TM.Windows81.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W8));
            if (OS.W7 && TM.Windows7.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W7));
            if (OS.WVista && TM.WindowsVista.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WVista));
            if (OS.WXP && TM.WindowsXP.Enabled) aspects_list.Add(string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WXP));

            if (OS.W12 && TM.LogonUI12.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.LockScreen);
            if (OS.W11 && TM.LogonUI11.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.LockScreen);
            if (OS.W10 && TM.LogonUI10.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.LockScreen);

            if (TM.LogonUI81.Enabled & (OS.W8x)) aspects_list.Add(Program.Lang.Strings.Aspects.LockScreen);
            if (TM.LogonUI7.Enabled & (OS.W7)) aspects_list.Add(Program.Lang.Strings.Aspects.LogonUI);
            if (TM.LogonUIXP.Enabled & OS.WXP) aspects_list.Add(Program.Lang.Strings.Aspects.LogonUI);

            if (TM.Win32.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.ClassicColors);

            if (TM.Cursors.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.Cursors);
            if (TM.Wallpaper.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.Wallpaper);
            if (TM.Sounds.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.Sounds);
            if (TM.ScreenSaver.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.ScreenSaver);
            if (TM.MetricsFonts.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.MetricsFonts);
            if (TM.CommandPrompt.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.CommandPrompt);
            if (TM.PowerShellx86.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.PowerShellx86);
            if (TM.PowerShellx64.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.PowerShellx64);
            if (TM.Terminal.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.TerminalStable);
            if (TM.TerminalPreview.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.TerminalPreview);
            if (TM.WindowsEffects.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.WinEffects);
            if (TM.AltTab.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.AltTab);
            if (TM.Icons.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.Icons);
            if (TM.Accessibility.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.Accessibility);
            if (TM.AppTheme.Enabled) aspects_list.Add(Program.Lang.Strings.Aspects.AppTheme);

            return aspects_list;
        }

        private void Store_CPToggles_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<Store>();

            CheckedListBox1.Items.Clear();
            CheckedListBox1.Items.AddRange([.. EnabledAspects(TM)]);

            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++) CheckedListBox1.SetItemChecked(i, true);

            if (CheckedListBox1.Items.Count == 0) Close();

            CheckedListBox1.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;

            SystemSounds.Exclamation.Play();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++)
            {
                if (OS.W12 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W12).ToLower())
                    TM.Windows12.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W11 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W11).ToLower())
                    TM.Windows11.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W10 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W10).ToLower())
                    TM.Windows10.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W81 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W81).ToLower())
                    TM.Windows81.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W8 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W8).ToLower())
                    TM.Windows8.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.W7 && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W7).ToLower())
                    TM.Windows7.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.WVista && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WVista).ToLower())
                    TM.WindowsVista.Enabled = CheckedListBox1.GetItemChecked(i);

                if (OS.WXP && CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WXP).ToLower())
                    TM.WindowsXP.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.ClassicColors)
                    TM.Win32.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Accessibility)
                    TM.Accessibility.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.AppTheme)
                    TM.AppTheme.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.LogonUI)
                {
                    if (OS.W7)
                    {
                        TM.LogonUI7.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.WXP)
                    {
                        TM.LogonUIXP.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                }

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.LockScreen)
                {
                    if (OS.W8x)
                    {
                        TM.LogonUI81.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.W10)
                    {
                        TM.LogonUI10.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.W11)
                    {
                        TM.LogonUI11.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.W12)
                    {
                        TM.LogonUI12.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                }

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Cursors)
                    TM.Cursors.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.CommandPrompt)
                    TM.CommandPrompt.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.PowerShellx86)
                    TM.PowerShellx86.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.PowerShellx64)
                    TM.PowerShellx64.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.TerminalStable)
                    TM.Terminal.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.TerminalPreview)
                    TM.TerminalPreview.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.MetricsFonts)
                    TM.MetricsFonts.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Wallpaper)
                    TM.Wallpaper.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.WinEffects)
                    TM.WindowsEffects.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.AltTab)
                    TM.AltTab.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Icons)
                    TM.Icons.Enabled = CheckedListBox1.GetItemChecked(i);
            }

            Forms.Store.selectedItem.TM = TM;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++) CheckedListBox1.SetItemChecked(i, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++) CheckedListBox1.SetItemChecked(i, false);
        }
    }
}