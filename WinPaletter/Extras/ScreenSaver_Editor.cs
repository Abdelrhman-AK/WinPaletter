using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class ScreenSaver_Editor
    {

        private Process Proc;

        public ScreenSaver_Editor()
        {
            InitializeComponent();
        }

        private void ScreenSaver_Editor_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
            pnl_preview.DoubleBuffer();
            ApplyFromCP(My.Env.CP);
        }


        public void ApplyFromCP(CP CP)
        {
            {
                ref var temp = ref CP.ScreenSaver;
                ScrSvrEnabled.Checked = temp.Enabled;
                TextBox1.Text = temp.File;
                Trackbar5.Value = temp.TimeOut;
                CheckBox1.Checked = temp.IsSecure;
            }

        }

        public void ApplyToCP(CP CP)
        {
            {
                ref var temp = ref CP.ScreenSaver;
                temp.Enabled = ScrSvrEnabled.Checked;
                temp.File = TextBox1.Text;
                temp.TimeOut = Trackbar5.Value;
                temp.IsSecure = CheckBox1.Checked;
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var CPx = new CP(CP.CP_Type.File, OpenFileDialog1.FileName);
                ApplyFromCP(CPx);
                CPx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var CPx = new CP(CP.CP_Type.Registry);
            ApplyFromCP(CPx);
            CPx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            using (var _Def = CP_Defaults.From(My.Env.PreviewStyle))
            {
                ApplyFromCP(_Def);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToCP(My.Env.CP);
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var CPx = new CP(CP.CP_Type.Registry);
            ApplyToCP(CPx);
            ApplyToCP(My.Env.CP);
            CPx.ScreenSaver.Apply();
            CPx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ScrSvrEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = ((UI.WP.Toggle)sender).Checked ? My.Resources.checker_enabled : My.Resources.checker_disabled;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(TextBox1.Text) && System.IO.Path.GetExtension(TextBox1.Text).ToUpper() == ".SCR")
            {
                if (Proc is not null && !Proc.HasExited)
                    Proc.Kill();
                Proc = Process.GetProcessById(Interaction.Shell("\"" + TextBox1.Text + "\"" + " /p " + pnl_preview.Handle.ToInt32()));
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenFileDialog2.FileName;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(TextBox1.Text) && System.IO.Path.GetExtension(TextBox1.Text).ToUpper() == ".SCR")
            {
                if (Proc is not null && !Proc.HasExited)
                    Proc.Kill();
                Proc = Process.GetProcessById(Interaction.Shell("\"" + TextBox1.Text + "\"" + " /p " + pnl_preview.Handle.ToInt32()));
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Proc is not null && !Proc.HasExited)
                Proc.Kill();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(TextBox1.Text) && System.IO.Path.GetExtension(TextBox1.Text).ToUpper() == ".SCR")
                Interaction.Shell("\"" + TextBox1.Text + "\"" + " /s");
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (Proc is not null && !Proc.HasExited)
                Proc.Kill();
            if (System.IO.File.Exists(TextBox1.Text) && System.IO.Path.GetExtension(TextBox1.Text).ToUpper() == ".SCR")
            {
                Proc = Process.GetProcessById(Interaction.Shell("\"" + TextBox1.Text + "\"" + " /c", AppWinStyle.NormalFocus));
                Proc.WaitForExit();
                Proc = Process.GetProcessById(Interaction.Shell("\"" + TextBox1.Text + "\"" + " /p " + pnl_preview.Handle.ToInt32()));
            }
        }

        private void ScreenSaver_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Proc is not null && !Proc.HasExited)
                Proc.Kill();
        }

        private void Trackbar5_Scroll(object sender)
        {
            Button4.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar5.Maximum), Trackbar5.Minimum).ToString();
            Trackbar5.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button259_Click(object sender, EventArgs e)
        {

            if (OpenThemeDialog.ShowDialog() == DialogResult.OK)
            {
                using (var _Def = CP_Defaults.From(My.Env.PreviewStyle))
                {
                    GetFromClassicThemeFile(OpenThemeDialog.FileName, _Def.ScreenSaver);
                }
            }
        }

        public void GetFromClassicThemeFile(string File, CP.Structures.ScreenSaver _DefaultScrSvr)
        {
            using (var _ini = new INI(File))
            {
                TextBox1.Text = _ini.IniReadValue("boot", "SCRNSAVE.EXE", _DefaultScrSvr.File).PhrasePath();
            }
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-Screen-Saver");
        }
    }
}