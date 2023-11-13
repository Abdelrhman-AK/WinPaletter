using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class ComplexSave
    {
        public ComplexSave()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void ComplexSave_FormClosing(object sender, FormClosingEventArgs e)
        {
            int i1 = 0;
            int i2 = 0;

            if (RadioImage1.Checked)
                i1 = 0;
            if (RadioImage2.Checked)
                i1 = 1;
            if (RadioImage3.Checked)
                i1 = 2;

            if (RadioImage6.Checked)
                i2 = 1;
            if (RadioImage5.Checked)
                i2 = 2;
            if (RadioImage7.Checked)
                i2 = 3;
            if (RadioImage4.Checked)
                i2 = 0;

            Program.Settings.General.ComplexSaveResult = i1 + "." + i2;
            Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation = CheckBox2.Checked;
            Program.Settings.General.Save();
            Program.Settings.ThemeApplyingBehavior.Save();
        }

        private void ComplexSave_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainFrm.Icon;
            this.LoadLanguage();
            ApplyStyle(this);

            var c = PictureBox1.Image.AverageColor();

            AnimatedBox1.Color1 = c;
            AnimatedBox1.Color2 = c;

            Program.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);

            string[] r = Program.Settings.General.ComplexSaveResult.Split('.');
            string r1 = r[0];
            string r2 = r[1];

            if (OS.W12)
            {
                RadioImage7.Image = Properties.Resources.Native12.Resize(20, 20);
            }

            if (OS.W11)
            {
                RadioImage7.Image = Properties.Resources.Native11.Resize(20, 20);
            }

            else if (OS.W10)
            {
                RadioImage7.Image = Properties.Resources.Native10.Resize(20, 20);
            }

            else if (OS.W8 | OS.W81)
            {
                RadioImage7.Image = Properties.Resources.Native8.Resize(20, 20);
            }

            else if (OS.W7)
            {
                RadioImage7.Image = Properties.Resources.Native7.Resize(20, 20);
            }

            else if (OS.WVista)
            {
                RadioImage7.Image = Properties.Resources.NativeVista.Resize(20, 20);
            }

            else if (OS.WXP)
            {
                RadioImage7.Image = Properties.Resources.NativeXP.Resize(20, 20);
            }

            else
            {
                RadioImage7.Image = Properties.Resources.Native11.Resize(20, 20);
            }


            if (Conversions.ToDouble(r1) == 0d)
            {
                RadioImage1.Checked = true;
            }
            else if (Conversions.ToDouble(r1) == 1d)
            {
                RadioImage2.Checked = true;
            }
            else if (Conversions.ToDouble(r1) == 2d)
            {
                RadioImage3.Checked = true;
            }
            else
            {
                RadioImage3.Checked = true;
            }

            if (Conversions.ToDouble(r2) == 0d)
            {
                RadioImage4.Checked = true;
            }
            else if (Conversions.ToDouble(r2) == 1d)
            {
                RadioImage6.Checked = true;
            }
            else if (Conversions.ToDouble(r2) == 2d)
            {
                RadioImage5.Checked = true;
            }
            else if (Conversions.ToDouble(r2) == 3d)
            {
                RadioImage7.Checked = true;
            }
            else
            {
                RadioImage6.Checked = true;
            }

            CheckBox2.Checked = Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation;

            DialogResult = DialogResult.None;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation = CheckBox2.Checked;
            Program.Settings.ThemeApplyingBehavior.Save();
            DialogResult = DialogResult.No;
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public bool GetResponse(SaveFileDialog SaveFileDialog, MethodInvoker Apply_Theme_Sub, MethodInvoker Apply_FirstTheme_Sub, MethodInvoker Apply_DefaultWin_Sub)
        {
            if ((Program.TM != Program.TM_Original) && Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation)
            {
                GroupBox2.Enabled = Apply_Theme_Sub is not null | Apply_FirstTheme_Sub is not null | Apply_DefaultWin_Sub is not null;

                switch (ShowDialog())
                {
                    case DialogResult.Yes:
                        {

                            string[] r = Program.Settings.General.ComplexSaveResult.Split('.');
                            string r1 = r[0];
                            string r2 = r[1];

                            switch (r1 ?? "")
                            {
                                case "0":              // ' Save
                                    {
                                        if (System.IO.File.Exists(SaveFileDialog.FileName))
                                        {
                                            Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog.FileName);
                                            Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                        }
                                        else if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                                        {
                                            Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog.FileName);
                                            Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                        }
                                        else
                                        {
                                            return false;
                                        }

                                        break;
                                    }
                                case "1":              // ' Save As
                                    {
                                        if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                                        {
                                            Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog.FileName);
                                            Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                        }
                                        else
                                        {
                                            return false;
                                        }

                                        break;
                                    }
                            }

                            switch (r2 ?? "")
                            {
                                case "1":
                                    {
                                        if (Apply_Theme_Sub is not null)
                                            Apply_Theme_Sub();
                                        break;
                                    }

                                case "2":
                                    {
                                        if (Apply_FirstTheme_Sub is not null)
                                            Apply_FirstTheme_Sub();
                                        break;
                                    }

                                case "3":
                                    {
                                        if (Apply_DefaultWin_Sub is not null)
                                            Apply_DefaultWin_Sub();
                                        break;
                                    }

                            }

                            break;
                        }

                    case DialogResult.No:
                        {
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            return false;
                        }
                }

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}