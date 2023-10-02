using Devcorp.Controls.VisualStyles;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class VS2Metrics
    {
        public VS2Metrics()
        {
            InitializeComponent();
        }
        private void VS2Win32UI_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.Metrics_Fonts.Icon;
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenFileDialog2.FileName;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            try
            {
                string theme = "";

                if (Path.GetExtension(TextBox1.Text) == ".theme")
                {
                    theme = TextBox1.Text;
                }

                else if (Path.GetExtension(TextBox1.Text) == ".msstyles")
                {
                    theme = My.Env.PATH_appData + @"\VisualStyles\Luna\win32uischeme.theme";
                    File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\win32uischeme.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", TextBox1.Text, "\r\n"));
                }

                if ((File.Exists(TextBox1.Text) && File.Exists(theme)) & !string.IsNullOrEmpty(theme))
                {
                    var vs = new VisualStyleFile(theme);
                    LoadColors(vs.Metrics);
                    My.MyProject.Forms.Win32UI.ApplyRetroPreview();
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadColors(VisualStyleMetrics vs)
        {
            if (CheckBox1.Checked)
            {
                My.MyProject.Forms.Metrics_Fonts.Trackbar2.Value = vs.Sizes.CaptionBarHeight;
                My.MyProject.Forms.Metrics_Fonts.Trackbar11.Value = vs.Sizes.ScrollbarHeight;
                My.MyProject.Forms.Metrics_Fonts.Trackbar10.Value = vs.Sizes.ScrollbarWidth;
                My.MyProject.Forms.Metrics_Fonts.Trackbar14.Value = vs.Sizes.SMCaptionBarHeight;
                My.MyProject.Forms.Metrics_Fonts.Trackbar13.Value = vs.Sizes.SMCaptionBarWidth;
            }

            if (CheckBox2.Checked)
            {
                My.MyProject.Forms.Metrics_Fonts.Label1.Font = vs.Fonts.CaptionFont;
                My.MyProject.Forms.Metrics_Fonts.Window1.Font = vs.Fonts.CaptionFont;
                My.MyProject.Forms.Metrics_Fonts.WindowR1.Font = vs.Fonts.CaptionFont;
                My.MyProject.Forms.Metrics_Fonts.WindowR3.Font = vs.Fonts.CaptionFont;
                My.MyProject.Forms.Metrics_Fonts.WindowR5.Font = vs.Fonts.CaptionFont;

                My.MyProject.Forms.Metrics_Fonts.Label2.Font = vs.Fonts.IconTitleFont;
                My.MyProject.Forms.Metrics_Fonts.FakeIcon1.Font = vs.Fonts.IconTitleFont;
                My.MyProject.Forms.Metrics_Fonts.FakeIcon2.Font = vs.Fonts.IconTitleFont;
                My.MyProject.Forms.Metrics_Fonts.FakeIcon3.Font = vs.Fonts.IconTitleFont;
                My.MyProject.Forms.Metrics_Fonts.Label2.Text = vs.Fonts.IconTitleFont.Name;

                My.MyProject.Forms.Metrics_Fonts.Label3.Font = vs.Fonts.MenuFont;
                My.MyProject.Forms.Metrics_Fonts.MenuStrip1.Font = vs.Fonts.MenuFont;
                My.MyProject.Forms.Metrics_Fonts.MenuStrip2.Font = vs.Fonts.MenuFont;
                My.MyProject.Forms.Metrics_Fonts.Label3.Text = vs.Fonts.MenuFont.Name;

                My.MyProject.Forms.Metrics_Fonts.Label5.Font = vs.Fonts.SmallCaptionFont;
                My.MyProject.Forms.Metrics_Fonts.Window2.Font = vs.Fonts.SmallCaptionFont;
                My.MyProject.Forms.Metrics_Fonts.WindowR2.Font = vs.Fonts.SmallCaptionFont;
                My.MyProject.Forms.Metrics_Fonts.Label5.Text = vs.Fonts.SmallCaptionFont.Name;

                My.MyProject.Forms.Metrics_Fonts.Label4.Font = vs.Fonts.MsgBoxFont;
                My.MyProject.Forms.Metrics_Fonts.msgLbl.Font = vs.Fonts.MsgBoxFont;
                My.MyProject.Forms.Metrics_Fonts.Label13.Font = vs.Fonts.MsgBoxFont;
                My.MyProject.Forms.Metrics_Fonts.Label4.Text = vs.Fonts.MsgBoxFont.Name;

                My.MyProject.Forms.Metrics_Fonts.Label6.Font = vs.Fonts.StatusFont;
                My.MyProject.Forms.Metrics_Fonts.statusLbl.Font = vs.Fonts.StatusFont;
                My.MyProject.Forms.Metrics_Fonts.Label14.Font = vs.Fonts.StatusFont;
                My.MyProject.Forms.Metrics_Fonts.Label6.Text = vs.Fonts.StatusFont.Name;
                My.MyProject.Forms.Metrics_Fonts.PanelR1.Height = Math.Max(My.MyProject.Forms.Metrics_Fonts.GetTitleTextHeight(vs.Fonts.StatusFont), 20);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}