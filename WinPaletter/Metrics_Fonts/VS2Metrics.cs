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
            ApplyStyle(this);
            Icon = Forms.Metrics_Fonts.Icon;
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
                string theme = string.Empty;

                if (Path.GetExtension(TextBox1.Text) == ".theme")
                {
                    theme = TextBox1.Text;
                }

                else if (Path.GetExtension(TextBox1.Text) == ".msstyles")
                {
                    theme = PathsExt.appData + @"\VisualStyles\Luna\win32uischeme.theme";
                    File.WriteAllText(PathsExt.appData + @"\VisualStyles\Luna\win32uischeme.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", TextBox1.Text, "\r\n"));
                }

                if ((File.Exists(TextBox1.Text) && File.Exists(theme)) & !string.IsNullOrEmpty(theme))
                {
                    VisualStyleFile vs = new(theme);
                    LoadColors(vs.Metrics);
                    Forms.Win32UI.ApplyRetroPreview();
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
                Forms.Metrics_Fonts.Trackbar2.Value = vs.Sizes.CaptionBarHeight;
                Forms.Metrics_Fonts.Trackbar11.Value = vs.Sizes.ScrollbarHeight;
                Forms.Metrics_Fonts.Trackbar10.Value = vs.Sizes.ScrollbarWidth;
                Forms.Metrics_Fonts.Trackbar14.Value = vs.Sizes.SMCaptionBarHeight;
                Forms.Metrics_Fonts.Trackbar13.Value = vs.Sizes.SMCaptionBarWidth;
            }

            if (CheckBox2.Checked)
            {
                Forms.Metrics_Fonts.Label1.Font = vs.Fonts.CaptionFont;
                Forms.Metrics_Fonts.Window1.Font = vs.Fonts.CaptionFont;
                Forms.Metrics_Fonts.WindowR1.Font = vs.Fonts.CaptionFont;
                Forms.Metrics_Fonts.WindowR3.Font = vs.Fonts.CaptionFont;
                Forms.Metrics_Fonts.WindowR5.Font = vs.Fonts.CaptionFont;

                Forms.Metrics_Fonts.Label2.Font = vs.Fonts.IconTitleFont;
                Forms.Metrics_Fonts.FakeIcon1.Font = vs.Fonts.IconTitleFont;
                Forms.Metrics_Fonts.FakeIcon2.Font = vs.Fonts.IconTitleFont;
                Forms.Metrics_Fonts.FakeIcon3.Font = vs.Fonts.IconTitleFont;
                Forms.Metrics_Fonts.Label2.Text = vs.Fonts.IconTitleFont.Name;

                Forms.Metrics_Fonts.Label3.Font = vs.Fonts.MenuFont;
                Forms.Metrics_Fonts.MenuStrip1.Font = vs.Fonts.MenuFont;
                Forms.Metrics_Fonts.MenuStrip2.Font = vs.Fonts.MenuFont;
                Forms.Metrics_Fonts.Label3.Text = vs.Fonts.MenuFont.Name;

                Forms.Metrics_Fonts.Label5.Font = vs.Fonts.SmallCaptionFont;
                Forms.Metrics_Fonts.Window2.Font = vs.Fonts.SmallCaptionFont;
                Forms.Metrics_Fonts.WindowR2.Font = vs.Fonts.SmallCaptionFont;
                Forms.Metrics_Fonts.Label5.Text = vs.Fonts.SmallCaptionFont.Name;

                Forms.Metrics_Fonts.Label4.Font = vs.Fonts.MsgBoxFont;
                Forms.Metrics_Fonts.msgLbl.Font = vs.Fonts.MsgBoxFont;
                Forms.Metrics_Fonts.Label13.Font = vs.Fonts.MsgBoxFont;
                Forms.Metrics_Fonts.Label4.Text = vs.Fonts.MsgBoxFont.Name;

                Forms.Metrics_Fonts.Label6.Font = vs.Fonts.StatusFont;
                Forms.Metrics_Fonts.statusLbl.Font = vs.Fonts.StatusFont;
                Forms.Metrics_Fonts.Label14.Font = vs.Fonts.StatusFont;
                Forms.Metrics_Fonts.Label6.Text = vs.Fonts.StatusFont.Name;
                Forms.Metrics_Fonts.PanelR1.Height = Math.Max(Forms.Metrics_Fonts.GetTitleTextHeight(vs.Fonts.StatusFont), 20);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}