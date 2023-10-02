using Devcorp.Controls.VisualStyles;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class VS2Win32UI
    {
        public VS2Win32UI()
        {
            InitializeComponent();
        }
        private void VS2Win32UI_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.Win32UI.Icon;
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
            My.MyProject.Forms.Win32UI.Toggle1.Checked = vs.FlatMenus;
            // Win32UI.ActiveBorder_pick.BackColor = vs.colors.ActiveBorder
            My.MyProject.Forms.Win32UI.activetitle_pick.BackColor = vs.Colors.ActiveCaption;
            My.MyProject.Forms.Win32UI.AppWorkspace_pick.BackColor = vs.Colors.AppWorkspace;
            My.MyProject.Forms.Win32UI.background_pick.BackColor = vs.Colors.Background;
            // Win32UI.btnaltface_pick.BackColor = vs.colors.ButtonAlternateFace
            My.MyProject.Forms.Win32UI.btndkshadow_pick.BackColor = vs.Colors.DkShadow3d;
            My.MyProject.Forms.Win32UI.btnface_pick.BackColor = vs.Colors.Btnface;
            My.MyProject.Forms.Win32UI.btnhilight_pick.BackColor = vs.Colors.BtnHighlight;
            My.MyProject.Forms.Win32UI.btnlight_pick.BackColor = vs.Colors.Light3d;
            My.MyProject.Forms.Win32UI.btnshadow_pick.BackColor = vs.Colors.BtnShadow;
            // Win32UI.btntext_pick.BackColor = vs.colors.WindowText
            My.MyProject.Forms.Win32UI.GActivetitle_pick.BackColor = vs.Colors.GradientActiveCaption;
            My.MyProject.Forms.Win32UI.GInactivetitle_pick.BackColor = vs.Colors.GradientInactiveCaption;
            My.MyProject.Forms.Win32UI.GrayText_pick.BackColor = vs.Colors.GrayText;
            My.MyProject.Forms.Win32UI.hilighttext_pick.BackColor = vs.Colors.HighlightText;
            My.MyProject.Forms.Win32UI.hottracking_pick.BackColor = vs.Colors.HotTracking;
            // Win32UI.InactiveBorder_pick.BackColor = vs.colors.InactiveBorder
            My.MyProject.Forms.Win32UI.InactiveTitle_pick.BackColor = vs.Colors.InactiveCaption;
            My.MyProject.Forms.Win32UI.InactivetitleText_pick.BackColor = vs.Colors.InactiveCaptionText;
            // Win32UI.InfoText_pick.BackColor = vs.colors.InfoText
            // Win32UI.InfoWindow_pick.BackColor = vs.colors.InfoWindow
            My.MyProject.Forms.Win32UI.menu_pick.BackColor = vs.Colors.Menu;
            My.MyProject.Forms.Win32UI.menubar_pick.BackColor = vs.Colors.MenuBar;
            My.MyProject.Forms.Win32UI.menutext_pick.BackColor = vs.Colors.MenuText;
            // Win32UI.Scrollbar_pick.BackColor = vs.colors.Scrollbar
            My.MyProject.Forms.Win32UI.TitleText_pick.BackColor = vs.Colors.CaptionText;
            My.MyProject.Forms.Win32UI.Window_pick.BackColor = vs.Colors.Window;
            // Win32UI.Frame_pick.BackColor = vs.colors.WindowFrame
            My.MyProject.Forms.Win32UI.WindowText_pick.BackColor = vs.Colors.WindowText;
            My.MyProject.Forms.Win32UI.hilight_pick.BackColor = vs.Colors.Highlight;
            My.MyProject.Forms.Win32UI.menuhilight_pick.BackColor = vs.Colors.MenuHilight;
            My.MyProject.Forms.Win32UI.desktop_pick.BackColor = vs.Colors.Background;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}