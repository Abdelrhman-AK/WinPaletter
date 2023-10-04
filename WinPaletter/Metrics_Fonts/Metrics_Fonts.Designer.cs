using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Metrics_Fonts : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Metrics_Fonts));
            FontDialog1 = new FontDialog();
            OpenFileDialog1 = new OpenFileDialog();
            FontDialog2 = new FontDialog();
            AlertBox10 = new UI.WP.AlertBox();
            GroupBox12 = new UI.WP.GroupBox();
            Button20 = new UI.WP.Button();
            Button20.Click += new EventHandler(Button20_Click);
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Button13 = new UI.WP.Button();
            Button13.Click += new EventHandler(Button13_Click_2);
            Label12 = new Label();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            MetricsEnabled = new UI.WP.Toggle();
            MetricsEnabled.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(MetricsEnabled_CheckedChanged);
            checker_img = new PictureBox();
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            AlertBox14 = new UI.WP.AlertBox();
            AlertBox3 = new UI.WP.AlertBox();
            GroupBox4 = new UI.WP.GroupBox();
            PictureBox9 = new PictureBox();
            PictureBox7 = new PictureBox();
            Label39 = new Label();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Label5 = new Label();
            Label5.FontChanged += new EventHandler(Label1_FontChanged);
            Label38 = new Label();
            PictureBox3 = new PictureBox();
            Label59 = new Label();
            Label1 = new Label();
            Label1.FontChanged += new EventHandler(Label1_FontChanged);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            GroupBox2 = new UI.WP.GroupBox();
            tw_w = new UI.WP.Button();
            tw_w.Click += new EventHandler(Tw_w_Click);
            tw_h = new UI.WP.Button();
            tw_h.Click += new EventHandler(Tw_h_Click);
            PictureBox8 = new PictureBox();
            PictureBox10 = new PictureBox();
            Trackbar13 = new UI.WP.Trackbar();
            Trackbar13.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar13_Scroll);
            PictureBox11 = new PictureBox();
            Label44 = new Label();
            Trackbar14 = new UI.WP.Trackbar();
            Trackbar14.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar14_Scroll);
            Label45 = new Label();
            Label48 = new Label();
            previewContainer = new UI.WP.GroupBox();
            tabs_preview_1 = new UI.WP.TablessControl();
            TabPage6 = new TabPage();
            pnl_preview1 = new Panel();
            AlertBox11 = new UI.WP.AlertBox();
            Window2 = new UI.Simulation.Window();
            Window1 = new UI.Simulation.Window();
            Window1.MetricsChanged += new UI.Simulation.Window.MetricsChangedEventHandler(Window1_MetricsChanged);
            Window1.FontChanged += new EventHandler(Window1_FontChanged);
            TabPage7 = new TabPage();
            Classic_Preview1 = new Panel();
            WindowR2 = new UI.Retro.WindowR();
            WindowR1 = new UI.Retro.WindowR();
            PictureBox41 = new PictureBox();
            Label19 = new Label();
            GroupBox1 = new UI.WP.GroupBox();
            ttl_p = new UI.WP.Button();
            ttl_p.Click += new EventHandler(Button15_Click);
            ttl_b = new UI.WP.Button();
            ttl_b.Click += new EventHandler(Button14_Click);
            ttl_w = new UI.WP.Button();
            ttl_w.Click += new EventHandler(Button13_Click_1);
            ttl_h = new UI.WP.Button();
            ttl_h.Click += new EventHandler(Button13_Click);
            Label40 = new Label();
            PictureBox6 = new PictureBox();
            PictureBox5 = new PictureBox();
            PictureBox4 = new PictureBox();
            PictureBox2 = new PictureBox();
            Label7 = new Label();
            Trackbar12 = new UI.WP.Trackbar();
            Trackbar12.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar12_Scroll);
            PictureBox1 = new PictureBox();
            Label43 = new Label();
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            Label10 = new Label();
            Trackbar3 = new UI.WP.Trackbar();
            Trackbar3.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar3_Scroll);
            Label8 = new Label();
            Trackbar2 = new UI.WP.Trackbar();
            Trackbar2.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar2_Scroll);
            TabPage2 = new TabPage();
            GroupBox11 = new UI.WP.GroupBox();
            PictureBox32 = new PictureBox();
            Label29 = new Label();
            PictureBox12 = new PictureBox();
            Trackbar5 = new UI.WP.Trackbar();
            Trackbar5.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar5_Scroll);
            i_s_s = new UI.WP.Button();
            i_s_s.Click += new EventHandler(I_s_s_Click);
            Label22 = new Label();
            Label20 = new Label();
            i_s_s_s = new UI.WP.Button();
            i_s_s_s.Click += new EventHandler(I_s_s_s_Click);
            Trackbar15 = new UI.WP.Trackbar();
            Trackbar15.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar15_Scroll);
            PictureBox30 = new PictureBox();
            AlertBox1 = new UI.WP.AlertBox();
            GroupBox13 = new UI.WP.GroupBox();
            AlertBox2 = new UI.WP.AlertBox();
            PictureBox17 = new PictureBox();
            Label24 = new Label();
            pnl_preview2 = new Panel();
            FakeIcon1 = new UI.Simulation.WinIcon();
            FakeIcon2 = new UI.Simulation.WinIcon();
            FakeIcon3 = new UI.Simulation.WinIcon();
            GroupBox5 = new UI.WP.GroupBox();
            i_d_s = new UI.WP.Button();
            i_d_s.Click += new EventHandler(I_d_s_Click);
            PictureBox21 = new PictureBox();
            Label9 = new Label();
            PictureBox14 = new PictureBox();
            Trackbar7 = new UI.WP.Trackbar();
            Trackbar7.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar7_Scroll);
            Label16 = new Label();
            GroupBox6 = new UI.WP.GroupBox();
            PictureBox20 = new PictureBox();
            Label37 = new Label();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Label2 = new Label();
            Label2.FontChanged += new EventHandler(Label1_FontChanged);
            GroupBox3 = new UI.WP.GroupBox();
            i_s_h = new UI.WP.Button();
            i_s_h.Click += new EventHandler(I_s_h_Click);
            i_s_v = new UI.WP.Button();
            i_s_v.Click += new EventHandler(I_s_v_Click);
            PictureBox13 = new PictureBox();
            PictureBox15 = new PictureBox();
            PictureBox16 = new PictureBox();
            Label41 = new Label();
            Label42 = new Label();
            Label49 = new Label();
            Trackbar4 = new UI.WP.Trackbar();
            Trackbar4.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar4_Scroll);
            Trackbar6 = new UI.WP.Trackbar();
            Trackbar6.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar6_Scroll);
            TabPage3 = new TabPage();
            GroupBox14 = new UI.WP.GroupBox();
            tabs_preview_2 = new UI.WP.TablessControl();
            TabPage4 = new TabPage();
            pnl_preview3 = new Panel();
            AlertBox12 = new UI.WP.AlertBox();
            Window4 = new UI.Simulation.Window();
            MenuStrip1 = new MenuStrip();
            MenuStrip1.FontChanged += new EventHandler(MenuStrip1_FontChanged);
            MenuParentToolStripMenuItem = new UI.WP.ToolStripMenuItem();
            MenuParent2ToolStripMenuItem = new UI.WP.ToolStripMenuItem();
            TabPage8 = new TabPage();
            Classic_Preview3 = new Panel();
            WindowR3 = new UI.Retro.WindowR();
            PanelR2 = new UI.Retro.PanelR();
            MenuStrip2 = new MenuStrip();
            ToolStripMenuItem1 = new UI.WP.ToolStripMenuItem();
            ToolStripMenuItem4 = new UI.WP.ToolStripMenuItem();
            PictureBox33 = new PictureBox();
            Label27 = new Label();
            GroupBox7 = new UI.WP.GroupBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Label3 = new Label();
            Label3.FontChanged += new EventHandler(Label1_FontChanged);
            PictureBox18 = new PictureBox();
            Label17 = new Label();
            GroupBox8 = new UI.WP.GroupBox();
            m_w = new UI.WP.Button();
            m_w.Click += new EventHandler(Mw_Click);
            m_h = new UI.WP.Button();
            m_h.Click += new EventHandler(Mh_Click);
            PictureBox19 = new PictureBox();
            PictureBox22 = new PictureBox();
            Trackbar9 = new UI.WP.Trackbar();
            Trackbar9.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar9_Scroll);
            Trackbar8 = new UI.WP.Trackbar();
            Trackbar8.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar8_Scroll);
            PictureBox23 = new PictureBox();
            Label21 = new Label();
            Label46 = new Label();
            Label47 = new Label();
            TabPage5 = new TabPage();
            GroupBox17 = new UI.WP.GroupBox();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            PictureBox31 = new PictureBox();
            PictureBox44 = new PictureBox();
            Label26 = new Label();
            AlertBox4 = new UI.WP.AlertBox();
            GroupBox15 = new UI.WP.GroupBox();
            tabs_preview_3 = new UI.WP.TablessControl();
            TabPage9 = new TabPage();
            pnl_preview4 = new Panel();
            AlertBox13 = new UI.WP.AlertBox();
            Window6 = new UI.Simulation.Window();
            Panel2 = new Panel();
            msgLbl = new UI.WP.LabelAlt();
            PictureBox35 = new PictureBox();
            HScrollBar1 = new HScrollBar();
            VScrollBar1 = new VScrollBar();
            StatusStrip1 = new StatusStrip();
            statusLbl = new UI.WP.ToolStripStatusLabel();
            TabPage10 = new TabPage();
            Classic_Preview4 = new Panel();
            WindowR5 = new UI.Retro.WindowR();
            Panel3 = new Panel();
            Label13 = new UI.WP.LabelAlt();
            PictureBox36 = new PictureBox();
            ScrollBarR1 = new UI.Retro.ScrollBarR();
            ButtonR3 = new UI.Retro.ButtonR();
            ButtonR1 = new UI.Retro.ButtonR();
            ButtonR2 = new UI.Retro.ButtonR();
            ScrollBarR2 = new UI.Retro.ScrollBarR();
            ButtonR12 = new UI.Retro.ButtonR();
            ButtonR11 = new UI.Retro.ButtonR();
            ButtonR10 = new UI.Retro.ButtonR();
            Panel1 = new Panel();
            PanelR1 = new UI.Retro.PanelR();
            Label14 = new UI.WP.LabelAlt();
            PictureBox34 = new PictureBox();
            Label36 = new Label();
            GroupBox10 = new UI.WP.GroupBox();
            s_w = new UI.WP.Button();
            s_w.Click += new EventHandler(Sw_Click);
            s_h = new UI.WP.Button();
            s_h.Click += new EventHandler(Sh_Click);
            PictureBox27 = new PictureBox();
            PictureBox28 = new PictureBox();
            PictureBox29 = new PictureBox();
            Label51 = new Label();
            Trackbar10 = new UI.WP.Trackbar();
            Trackbar10.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar10_Scroll);
            Label53 = new Label();
            Label54 = new Label();
            Trackbar11 = new UI.WP.Trackbar();
            Trackbar11.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar11_Scroll);
            GroupBox9 = new UI.WP.GroupBox();
            PictureBox24 = new PictureBox();
            PictureBox25 = new PictureBox();
            Label6 = new Label();
            Label6.FontChanged += new EventHandler(Label1_FontChanged);
            Label4 = new Label();
            Label4.FontChanged += new EventHandler(Label1_FontChanged);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Label11 = new Label();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Label18 = new Label();
            PictureBox26 = new PictureBox();
            Label50 = new Label();
            TabPage11 = new TabPage();
            GroupBox16 = new UI.WP.GroupBox();
            AlertBox9 = new UI.WP.AlertBox();
            AlertBox7 = new UI.WP.AlertBox();
            Button19 = new UI.WP.Button();
            Button19.Click += new EventHandler(Button19_Click);
            Button18 = new UI.WP.Button();
            Button18.Click += new EventHandler(Button18_Click);
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click_1);
            AlertBox8 = new UI.WP.AlertBox();
            TextBox3 = new UI.WP.TextBox();
            TextBox3.TextChanged += new EventHandler(TextBox3_TextChanged);
            PictureBox37 = new PictureBox();
            Button17 = new UI.WP.Button();
            Button17.Click += new EventHandler(Button17_Click);
            AlertBox6 = new UI.WP.AlertBox();
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            PictureBox45 = new PictureBox();
            PictureBox46 = new PictureBox();
            Separator1 = new UI.WP.SeparatorH();
            Label28 = new Label();
            TextBox2 = new UI.WP.TextBox();
            TextBox2.TextChanged += new EventHandler(TextBox2_TextChanged);
            Button15 = new UI.WP.Button();
            Button15.Click += new EventHandler(Button15_Click_1);
            Label15 = new Label();
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click_1);
            AlertBox5 = new UI.WP.AlertBox();
            PictureBox42 = new PictureBox();
            PictureBox38 = new PictureBox();
            PictureBox40 = new PictureBox();
            Label23 = new Label();
            Label25 = new Label();
            PictureBox39 = new PictureBox();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checker_img).BeginInit();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            previewContainer.SuspendLayout();
            tabs_preview_1.SuspendLayout();
            TabPage6.SuspendLayout();
            pnl_preview1.SuspendLayout();
            TabPage7.SuspendLayout();
            Classic_Preview1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            TabPage2.SuspendLayout();
            GroupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox32).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).BeginInit();
            GroupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            pnl_preview2.SuspendLayout();
            GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            TabPage3.SuspendLayout();
            GroupBox14.SuspendLayout();
            tabs_preview_2.SuspendLayout();
            TabPage4.SuspendLayout();
            pnl_preview3.SuspendLayout();
            Window4.SuspendLayout();
            MenuStrip1.SuspendLayout();
            TabPage8.SuspendLayout();
            Classic_Preview3.SuspendLayout();
            WindowR3.SuspendLayout();
            MenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox33).BeginInit();
            GroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).BeginInit();
            GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).BeginInit();
            TabPage5.SuspendLayout();
            GroupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox44).BeginInit();
            GroupBox15.SuspendLayout();
            tabs_preview_3.SuspendLayout();
            TabPage9.SuspendLayout();
            pnl_preview4.SuspendLayout();
            Window6.SuspendLayout();
            Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox35).BeginInit();
            StatusStrip1.SuspendLayout();
            TabPage10.SuspendLayout();
            Classic_Preview4.SuspendLayout();
            WindowR5.SuspendLayout();
            Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).BeginInit();
            ScrollBarR1.SuspendLayout();
            ScrollBarR2.SuspendLayout();
            PanelR1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).BeginInit();
            GroupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).BeginInit();
            GroupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).BeginInit();
            TabPage11.SuspendLayout();
            GroupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox37).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox45).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox42).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox38).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox39).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // AlertBox10
            // 
            AlertBox10.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox10.BackColor = Color.FromArgb(17, 67, 91);
            AlertBox10.CenterText = false;
            AlertBox10.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox10.Font = new Font("Segoe UI", 9.0f);
            AlertBox10.Image = (Image)resources.GetObject("AlertBox10.Image");
            AlertBox10.Location = new Point(18, 501);
            AlertBox10.Name = "AlertBox10";
            AlertBox10.Size = new Size(1072, 30);
            AlertBox10.TabIndex = 203;
            AlertBox10.TabStop = false;
            AlertBox10.Text = null;
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(Button20);
            GroupBox12.Controls.Add(Button9);
            GroupBox12.Controls.Add(Button13);
            GroupBox12.Controls.Add(Label12);
            GroupBox12.Controls.Add(Button11);
            GroupBox12.Controls.Add(Button12);
            GroupBox12.Controls.Add(MetricsEnabled);
            GroupBox12.Controls.Add(checker_img);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(1078, 39);
            GroupBox12.TabIndex = 199;
            // 
            // Button20
            // 
            Button20.BackColor = Color.FromArgb(43, 43, 43);
            Button20.DrawOnGlass = false;
            Button20.Font = new Font("Segoe UI", 9.0f);
            Button20.ForeColor = Color.White;
            Button20.Image = (Image)resources.GetObject("Button20.Image");
            Button20.ImageAlign = ContentAlignment.MiddleLeft;
            Button20.LineColor = Color.FromArgb(136, 157, 165);
            Button20.Location = new Point(223, 5);
            Button20.Name = "Button20";
            Button20.Size = new Size(141, 29);
            Button20.TabIndex = 113;
            Button20.Text = "Visual styles file";
            Button20.UseVisualStyleBackColor = false;
            // 
            // Button9
            // 
            Button9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.ImageAlign = ContentAlignment.MiddleRight;
            Button9.LineColor = Color.FromArgb(90, 134, 117);
            Button9.Location = new Point(367, 5);
            Button9.Name = "Button9";
            Button9.Size = new Size(126, 29);
            Button9.TabIndex = 112;
            Button9.Text = "Current applied";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Button13
            // 
            Button13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button13.BackColor = Color.FromArgb(43, 43, 43);
            Button13.DrawOnGlass = false;
            Button13.Font = new Font("Segoe UI", 9.0f);
            Button13.ForeColor = Color.White;
            Button13.Image = null;
            Button13.ImageAlign = ContentAlignment.MiddleLeft;
            Button13.LineColor = Color.FromArgb(36, 81, 110);
            Button13.Location = new Point(794, 5);
            Button13.Name = "Button13";
            Button13.Size = new Size(189, 29);
            Button13.TabIndex = 85;
            Button13.Text = @"Toggle modern\classic preview";
            Button13.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            Label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label12.Location = new Point(4, 4);
            Label12.Name = "Label12";
            Label12.Size = new Size(75, 31);
            Label12.TabIndex = 111;
            Label12.Text = "Open From:";
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            Button11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button11.BackColor = Color.FromArgb(43, 43, 43);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.ImageAlign = ContentAlignment.MiddleRight;
            Button11.LineColor = Color.FromArgb(113, 122, 131);
            Button11.Location = new Point(85, 5);
            Button11.Name = "Button11";
            Button11.Size = new Size(135, 29);
            Button11.TabIndex = 110;
            Button11.Text = "WinPaletter theme";
            Button11.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button12.BackColor = Color.FromArgb(43, 43, 43);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = null;
            Button12.ImageAlign = ContentAlignment.MiddleRight;
            Button12.LineColor = Color.FromArgb(0, 66, 119);
            Button12.Location = new Point(496, 5);
            Button12.Name = "Button12";
            Button12.Size = new Size(130, 29);
            Button12.TabIndex = 108;
            Button12.Text = "Default Windows";
            Button12.UseVisualStyleBackColor = false;
            // 
            // MetricsEnabled
            // 
            MetricsEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MetricsEnabled.BackColor = Color.FromArgb(43, 43, 43);
            MetricsEnabled.Checked = false;
            MetricsEnabled.DarkLight_Toggler = false;
            MetricsEnabled.Location = new Point(1031, 9);
            MetricsEnabled.Name = "MetricsEnabled";
            MetricsEnabled.Size = new Size(40, 20);
            MetricsEnabled.TabIndex = 85;
            // 
            // checker_img
            // 
            checker_img.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checker_img.Image = Properties.Resources.checker_disabled;
            checker_img.Location = new Point(990, 4);
            checker_img.Name = "checker_img";
            checker_img.Size = new Size(35, 31);
            checker_img.SizeMode = PictureBoxSizeMode.CenterImage;
            checker_img.TabIndex = 83;
            checker_img.TabStop = false;
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Controls.Add(TabPage5);
            TabControl1.Controls.Add(TabPage11);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ItemSize = new Size(35, 120);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(12, 57);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(1086, 447);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 131;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(AlertBox14);
            TabPage1.Controls.Add(AlertBox3);
            TabPage1.Controls.Add(GroupBox4);
            TabPage1.Controls.Add(GroupBox2);
            TabPage1.Controls.Add(previewContainer);
            TabPage1.Controls.Add(GroupBox1);
            TabPage1.Location = new Point(124, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(958, 439);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Titlebar";
            // 
            // AlertBox14
            // 
            AlertBox14.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox14.BackColor = Color.FromArgb(68, 50, 2);
            AlertBox14.CenterText = false;
            AlertBox14.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox14.Font = new Font("Segoe UI", 9.0f);
            AlertBox14.Image = (Image)resources.GetObject("AlertBox14.Image");
            AlertBox14.Location = new Point(415, 388);
            AlertBox14.Name = "AlertBox14";
            AlertBox14.Size = new Size(536, 45);
            AlertBox14.TabIndex = 203;
            AlertBox14.TabStop = false;
            AlertBox14.Text = "Updated versions of Windows 11 won't change title bar font due to bug in Windows " + "itself (you can change its size only)";
            // 
            // AlertBox3
            // 
            AlertBox3.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox3.BackColor = Color.FromArgb(68, 50, 2);
            AlertBox3.CenterText = false;
            AlertBox3.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox3.Font = new Font("Segoe UI", 9.0f);
            AlertBox3.Image = (Image)resources.GetObject("AlertBox3.Image");
            AlertBox3.Location = new Point(415, 352);
            AlertBox3.Name = "AlertBox3";
            AlertBox3.Size = new Size(536, 30);
            AlertBox3.TabIndex = 202;
            AlertBox3.TabStop = false;
            AlertBox3.Text = "Changing these fonts requires logoff and logon";
            // 
            // GroupBox4
            // 
            GroupBox4.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox4.Controls.Add(PictureBox9);
            GroupBox4.Controls.Add(PictureBox7);
            GroupBox4.Controls.Add(Label39);
            GroupBox4.Controls.Add(Button5);
            GroupBox4.Controls.Add(Label5);
            GroupBox4.Controls.Add(Label38);
            GroupBox4.Controls.Add(PictureBox3);
            GroupBox4.Controls.Add(Label59);
            GroupBox4.Controls.Add(Label1);
            GroupBox4.Controls.Add(Button1);
            GroupBox4.Location = new Point(6, 6);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Size = new Size(402, 146);
            GroupBox4.TabIndex = 132;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(17, 99);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 119;
            PictureBox9.TabStop = false;
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(17, 55);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 118;
            PictureBox7.TabStop = false;
            // 
            // Label39
            // 
            Label39.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label39.Location = new Point(50, 99);
            Label39.Name = "Label39";
            Label39.Size = new Size(132, 24);
            Label39.TabIndex = 87;
            Label39.Text = "Tool window title bar:";
            Label39.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(43, 43, 43);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = null;
            Button5.LineColor = Color.FromArgb(0, 81, 210);
            Button5.Location = new Point(360, 100);
            Button5.Name = "Button5";
            Button5.Size = new Size(34, 23);
            Button5.TabIndex = 10;
            Button5.Text = "...";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.Location = new Point(188, 89);
            Label5.Name = "Label5";
            Label5.Size = new Size(166, 44);
            Label5.TabIndex = 4;
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label38
            // 
            Label38.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label38.Location = new Point(50, 55);
            Label38.Name = "Label38";
            Label38.Size = new Size(132, 24);
            Label38.TabIndex = 86;
            Label38.Text = "Title bar:";
            Label38.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(6, 7);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(35, 31);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 83;
            PictureBox3.TabStop = false;
            // 
            // Label59
            // 
            Label59.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label59.BackColor = Color.Transparent;
            Label59.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label59.Location = new Point(47, 7);
            Label59.Name = "Label59";
            Label59.Size = new Size(347, 31);
            Label59.TabIndex = 84;
            Label59.Text = "Fonts:";
            Label59.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.Location = new Point(188, 45);
            Label1.Name = "Label1";
            Label1.Size = new Size(166, 44);
            Label1.TabIndex = 0;
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(43, 43, 43);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.LineColor = Color.FromArgb(0, 81, 210);
            Button1.Location = new Point(360, 56);
            Button1.Name = "Button1";
            Button1.Size = new Size(34, 23);
            Button1.TabIndex = 6;
            Button1.Text = "...";
            Button1.UseVisualStyleBackColor = false;
            // 
            // GroupBox2
            // 
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(tw_w);
            GroupBox2.Controls.Add(tw_h);
            GroupBox2.Controls.Add(PictureBox8);
            GroupBox2.Controls.Add(PictureBox10);
            GroupBox2.Controls.Add(Trackbar13);
            GroupBox2.Controls.Add(PictureBox11);
            GroupBox2.Controls.Add(Label44);
            GroupBox2.Controls.Add(Trackbar14);
            GroupBox2.Controls.Add(Label45);
            GroupBox2.Controls.Add(Label48);
            GroupBox2.Location = new Point(6, 328);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(402, 105);
            GroupBox2.TabIndex = 134;
            // 
            // tw_w
            // 
            tw_w.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tw_w.BackColor = Color.FromArgb(43, 43, 43);
            tw_w.DrawOnGlass = false;
            tw_w.Font = new Font("Segoe UI", 9.0f);
            tw_w.ForeColor = Color.White;
            tw_w.Image = null;
            tw_w.LineColor = Color.FromArgb(0, 81, 210);
            tw_w.Location = new Point(360, 74);
            tw_w.Name = "tw_w";
            tw_w.Size = new Size(34, 24);
            tw_w.TabIndex = 204;
            tw_w.UseVisualStyleBackColor = false;
            // 
            // tw_h
            // 
            tw_h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tw_h.BackColor = Color.FromArgb(43, 43, 43);
            tw_h.DrawOnGlass = false;
            tw_h.Font = new Font("Segoe UI", 9.0f);
            tw_h.ForeColor = Color.White;
            tw_h.Image = null;
            tw_h.LineColor = Color.FromArgb(0, 81, 210);
            tw_h.Location = new Point(360, 44);
            tw_h.Name = "tw_h";
            tw_h.Size = new Size(34, 24);
            tw_h.TabIndex = 203;
            tw_h.UseVisualStyleBackColor = false;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(17, 74);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 24);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 119;
            PictureBox8.TabStop = false;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(17, 44);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(24, 24);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 117;
            PictureBox10.TabStop = false;
            // 
            // Trackbar13
            // 
            Trackbar13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar13.LargeChange = 10;
            Trackbar13.Location = new Point(188, 77);
            Trackbar13.Maximum = 50;
            Trackbar13.Minimum = 0;
            Trackbar13.Name = "Trackbar13";
            Trackbar13.Size = new Size(162, 19);
            Trackbar13.SmallChange = 1;
            Trackbar13.TabIndex = 114;
            Trackbar13.Value = 0;
            // 
            // PictureBox11
            // 
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(6, 7);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(35, 31);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 83;
            PictureBox11.TabStop = false;
            // 
            // Label44
            // 
            Label44.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label44.BackColor = Color.Transparent;
            Label44.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label44.Location = new Point(47, 7);
            Label44.Name = "Label44";
            Label44.Size = new Size(344, 31);
            Label44.TabIndex = 84;
            Label44.Text = "Tool window title bars:";
            Label44.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar14
            // 
            Trackbar14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar14.LargeChange = 10;
            Trackbar14.Location = new Point(188, 47);
            Trackbar14.Maximum = 50;
            Trackbar14.Minimum = 0;
            Trackbar14.Name = "Trackbar14";
            Trackbar14.Size = new Size(162, 19);
            Trackbar14.SmallChange = 1;
            Trackbar14.TabIndex = 113;
            Trackbar14.Value = 0;
            // 
            // Label45
            // 
            Label45.Location = new Point(47, 74);
            Label45.Name = "Label45";
            Label45.Size = new Size(138, 24);
            Label45.TabIndex = 87;
            Label45.Text = "Classic button width:";
            Label45.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label48
            // 
            Label48.Location = new Point(47, 44);
            Label48.Name = "Label48";
            Label48.Size = new Size(138, 24);
            Label48.TabIndex = 86;
            Label48.Text = "Height:";
            Label48.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // previewContainer
            // 
            previewContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            previewContainer.BackColor = Color.FromArgb(34, 34, 34);
            previewContainer.Controls.Add(tabs_preview_1);
            previewContainer.Controls.Add(PictureBox41);
            previewContainer.Controls.Add(Label19);
            previewContainer.Location = new Point(415, 6);
            previewContainer.Margin = new Padding(4, 3, 4, 3);
            previewContainer.Name = "previewContainer";
            previewContainer.Padding = new Padding(1);
            previewContainer.Size = new Size(536, 340);
            previewContainer.TabIndex = 130;
            // 
            // tabs_preview_1
            // 
            tabs_preview_1.Controls.Add(TabPage6);
            tabs_preview_1.Controls.Add(TabPage7);
            tabs_preview_1.Location = new Point(4, 39);
            tabs_preview_1.Name = "tabs_preview_1";
            tabs_preview_1.SelectedIndex = 0;
            tabs_preview_1.Size = new Size(528, 297);
            tabs_preview_1.TabIndex = 120;
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(pnl_preview1);
            TabPage6.Location = new Point(4, 24);
            TabPage6.Margin = new Padding(0);
            TabPage6.Name = "TabPage6";
            TabPage6.Size = new Size(520, 269);
            TabPage6.TabIndex = 0;
            TabPage6.Text = "0";
            // 
            // pnl_preview1
            // 
            pnl_preview1.BackColor = Color.Black;
            pnl_preview1.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview1.Controls.Add(AlertBox11);
            pnl_preview1.Controls.Add(Window2);
            pnl_preview1.Controls.Add(Window1);
            pnl_preview1.Location = new Point(0, 0);
            pnl_preview1.Name = "pnl_preview1";
            pnl_preview1.Size = new Size(528, 297);
            pnl_preview1.TabIndex = 2;
            // 
            // AlertBox11
            // 
            AlertBox11.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox11.BackColor = Color.FromArgb(125, 20, 30);
            AlertBox11.CenterText = true;
            AlertBox11.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox11.Font = new Font("Segoe UI", 9.0f);
            AlertBox11.Image = null;
            AlertBox11.Location = new Point(6, 7);
            AlertBox11.Name = "AlertBox11";
            AlertBox11.Size = new Size(24, 21);
            AlertBox11.TabIndex = 8;
            AlertBox11.TabStop = false;
            AlertBox11.Text = null;
            AlertBox11.Visible = false;
            // 
            // Window2
            // 
            Window2.AccentColor_Active = Color.FromArgb(0, 120, 212);
            Window2.AccentColor_Enabled = true;
            Window2.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window2.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window2.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window2.Active = true;
            Window2.Anchor = AnchorStyles.None;
            Window2.BackColor = Color.Transparent;
            Window2.DarkMode = true;
            Window2.Font = new Font("Segoe UI", 9.0f);
            Window2.Location = new Point(317, 47);
            Window2.Metrics_BorderWidth = 1;
            Window2.Metrics_CaptionHeight = 22;
            Window2.Metrics_PaddedBorderWidth = 4;
            Window2.Name = "Window2";
            Window2.Padding = new Padding(10, 40, 10, 10);
            Window2.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window2.Radius = 3;
            Window2.Shadow = false;
            Window2.Size = new Size(119, 202);
            Window2.SuspendRefresh = false;
            Window2.TabIndex = 7;
            Window2.Text = "Tool";
            Window2.ToolWindow = true;
            Window2.Win7Alpha = 100;
            Window2.Win7ColorBal = 100;
            Window2.Win7GlowBal = 100;
            Window2.Win7Noise = 1.0f;
            Window2.WinVista = false;
            // 
            // Window1
            // 
            Window1.AccentColor_Active = Color.FromArgb(0, 120, 212);
            Window1.AccentColor_Enabled = true;
            Window1.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window1.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window1.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window1.Active = true;
            Window1.Anchor = AnchorStyles.None;
            Window1.BackColor = Color.Transparent;
            Window1.DarkMode = true;
            Window1.Font = new Font("Segoe UI", 9.0f);
            Window1.Location = new Point(92, 47);
            Window1.Metrics_BorderWidth = 1;
            Window1.Metrics_CaptionHeight = 22;
            Window1.Metrics_PaddedBorderWidth = 4;
            Window1.Name = "Window1";
            Window1.Padding = new Padding(10, 40, 10, 10);
            Window1.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window1.Radius = 5;
            Window1.Shadow = true;
            Window1.Size = new Size(219, 202);
            Window1.SuspendRefresh = false;
            Window1.TabIndex = 2;
            Window1.Text = "Application";
            Window1.ToolWindow = false;
            Window1.Win7Alpha = 100;
            Window1.Win7ColorBal = 100;
            Window1.Win7GlowBal = 100;
            Window1.Win7Noise = 1.0f;
            Window1.WinVista = false;
            // 
            // TabPage7
            // 
            TabPage7.BackColor = Color.FromArgb(25, 25, 25);
            TabPage7.Controls.Add(Classic_Preview1);
            TabPage7.Location = new Point(4, 24);
            TabPage7.Margin = new Padding(0);
            TabPage7.Name = "TabPage7";
            TabPage7.Size = new Size(520, 269);
            TabPage7.TabIndex = 1;
            TabPage7.Text = "1";
            // 
            // Classic_Preview1
            // 
            Classic_Preview1.BackColor = Color.Black;
            Classic_Preview1.BackgroundImageLayout = ImageLayout.Center;
            Classic_Preview1.Controls.Add(WindowR2);
            Classic_Preview1.Controls.Add(WindowR1);
            Classic_Preview1.Location = new Point(0, 0);
            Classic_Preview1.Name = "Classic_Preview1";
            Classic_Preview1.Size = new Size(528, 297);
            Classic_Preview1.TabIndex = 3;
            // 
            // WindowR2
            // 
            WindowR2.BackColor = Color.FromArgb(192, 192, 192);
            WindowR2.ButtonDkShadow = Color.Black;
            WindowR2.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR2.ButtonHilight = Color.White;
            WindowR2.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR2.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR2.ButtonText = Color.Black;
            WindowR2.Color1 = Color.FromArgb(0, 0, 128);
            WindowR2.Color2 = Color.FromArgb(16, 132, 208);
            WindowR2.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR2.ColorGradient = true;
            WindowR2.ControlBox = true;
            WindowR2.Flat = false;
            WindowR2.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR2.ForeColor = Color.White;
            WindowR2.Location = new Point(307, 70);
            WindowR2.MaximizeBox = false;
            WindowR2.Metrics_BorderWidth = 2;
            WindowR2.Metrics_CaptionHeight = 19;
            WindowR2.Metrics_CaptionWidth = 19;
            WindowR2.Metrics_PaddedBorderWidth = 1;
            WindowR2.MinimizeBox = false;
            WindowR2.Name = "WindowR2";
            WindowR2.Padding = new Padding(6, 26, 6, 6);
            WindowR2.Size = new Size(100, 170);
            WindowR2.TabIndex = 9;
            WindowR2.Text = "Tool";
            WindowR2.UseItAsMenu = false;
            // 
            // WindowR1
            // 
            WindowR1.BackColor = Color.FromArgb(192, 192, 192);
            WindowR1.ButtonDkShadow = Color.Black;
            WindowR1.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR1.ButtonHilight = Color.White;
            WindowR1.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR1.ButtonText = Color.Black;
            WindowR1.Color1 = Color.FromArgb(0, 0, 128);
            WindowR1.Color2 = Color.FromArgb(16, 132, 208);
            WindowR1.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR1.ColorGradient = true;
            WindowR1.ControlBox = true;
            WindowR1.Flat = false;
            WindowR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR1.ForeColor = Color.White;
            WindowR1.Location = new Point(112, 70);
            WindowR1.MaximizeBox = true;
            WindowR1.Metrics_BorderWidth = 2;
            WindowR1.Metrics_CaptionHeight = 19;
            WindowR1.Metrics_CaptionWidth = 19;
            WindowR1.Metrics_PaddedBorderWidth = 1;
            WindowR1.MinimizeBox = true;
            WindowR1.Name = "WindowR1";
            WindowR1.Padding = new Padding(6, 26, 6, 6);
            WindowR1.Size = new Size(189, 170);
            WindowR1.TabIndex = 8;
            WindowR1.Text = "Application";
            WindowR1.UseItAsMenu = false;
            // 
            // PictureBox41
            // 
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(4, 4);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(35, 32);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 4;
            PictureBox41.TabStop = false;
            // 
            // Label19
            // 
            Label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label19.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label19.Location = new Point(45, 5);
            Label19.Name = "Label19";
            Label19.Size = new Size(270, 31);
            Label19.TabIndex = 3;
            Label19.Text = "Preview";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(ttl_p);
            GroupBox1.Controls.Add(ttl_b);
            GroupBox1.Controls.Add(ttl_w);
            GroupBox1.Controls.Add(ttl_h);
            GroupBox1.Controls.Add(Label40);
            GroupBox1.Controls.Add(PictureBox6);
            GroupBox1.Controls.Add(PictureBox5);
            GroupBox1.Controls.Add(PictureBox4);
            GroupBox1.Controls.Add(PictureBox2);
            GroupBox1.Controls.Add(Label7);
            GroupBox1.Controls.Add(Trackbar12);
            GroupBox1.Controls.Add(PictureBox1);
            GroupBox1.Controls.Add(Label43);
            GroupBox1.Controls.Add(Trackbar1);
            GroupBox1.Controls.Add(Label10);
            GroupBox1.Controls.Add(Trackbar3);
            GroupBox1.Controls.Add(Label8);
            GroupBox1.Controls.Add(Trackbar2);
            GroupBox1.Location = new Point(6, 157);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(402, 165);
            GroupBox1.TabIndex = 133;
            // 
            // ttl_p
            // 
            ttl_p.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ttl_p.BackColor = Color.FromArgb(43, 43, 43);
            ttl_p.DrawOnGlass = false;
            ttl_p.Font = new Font("Segoe UI", 9.0f);
            ttl_p.ForeColor = Color.White;
            ttl_p.Image = null;
            ttl_p.LineColor = Color.FromArgb(0, 81, 210);
            ttl_p.Location = new Point(360, 134);
            ttl_p.Name = "ttl_p";
            ttl_p.Size = new Size(34, 24);
            ttl_p.TabIndex = 202;
            ttl_p.UseVisualStyleBackColor = false;
            // 
            // ttl_b
            // 
            ttl_b.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ttl_b.BackColor = Color.FromArgb(43, 43, 43);
            ttl_b.DrawOnGlass = false;
            ttl_b.Font = new Font("Segoe UI", 9.0f);
            ttl_b.ForeColor = Color.White;
            ttl_b.Image = null;
            ttl_b.LineColor = Color.FromArgb(0, 81, 210);
            ttl_b.Location = new Point(360, 104);
            ttl_b.Name = "ttl_b";
            ttl_b.Size = new Size(34, 24);
            ttl_b.TabIndex = 201;
            ttl_b.UseVisualStyleBackColor = false;
            // 
            // ttl_w
            // 
            ttl_w.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ttl_w.BackColor = Color.FromArgb(43, 43, 43);
            ttl_w.DrawOnGlass = false;
            ttl_w.Font = new Font("Segoe UI", 9.0f);
            ttl_w.ForeColor = Color.White;
            ttl_w.Image = null;
            ttl_w.LineColor = Color.FromArgb(0, 81, 210);
            ttl_w.Location = new Point(360, 74);
            ttl_w.Name = "ttl_w";
            ttl_w.Size = new Size(34, 24);
            ttl_w.TabIndex = 200;
            ttl_w.UseVisualStyleBackColor = false;
            // 
            // ttl_h
            // 
            ttl_h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ttl_h.BackColor = Color.FromArgb(43, 43, 43);
            ttl_h.DrawOnGlass = false;
            ttl_h.Font = new Font("Segoe UI", 9.0f);
            ttl_h.ForeColor = Color.White;
            ttl_h.Image = null;
            ttl_h.LineColor = Color.FromArgb(0, 81, 210);
            ttl_h.Location = new Point(360, 44);
            ttl_h.Name = "ttl_h";
            ttl_h.Size = new Size(34, 24);
            ttl_h.TabIndex = 129;
            ttl_h.UseVisualStyleBackColor = false;
            // 
            // Label40
            // 
            Label40.Location = new Point(47, 134);
            Label40.Name = "Label40";
            Label40.Size = new Size(138, 24);
            Label40.TabIndex = 121;
            Label40.Text = "Padding:";
            Label40.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(17, 134);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 120;
            PictureBox6.TabStop = false;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(17, 74);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 119;
            PictureBox5.TabStop = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(17, 104);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 118;
            PictureBox4.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(17, 44);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 117;
            PictureBox2.TabStop = false;
            // 
            // Label7
            // 
            Label7.Location = new Point(47, 104);
            Label7.Name = "Label7";
            Label7.Size = new Size(138, 24);
            Label7.TabIndex = 85;
            Label7.Text = "Border:";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar12
            // 
            Trackbar12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar12.LargeChange = 10;
            Trackbar12.Location = new Point(191, 137);
            Trackbar12.Maximum = 50;
            Trackbar12.Minimum = 0;
            Trackbar12.Name = "Trackbar12";
            Trackbar12.Size = new Size(163, 19);
            Trackbar12.SmallChange = 1;
            Trackbar12.TabIndex = 109;
            Trackbar12.Value = 1;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 7);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(35, 31);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 83;
            PictureBox1.TabStop = false;
            // 
            // Label43
            // 
            Label43.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label43.BackColor = Color.Transparent;
            Label43.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label43.Location = new Point(47, 7);
            Label43.Name = "Label43";
            Label43.Size = new Size(348, 31);
            Label43.TabIndex = 84;
            Label43.Text = "Title bars:";
            Label43.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar1
            // 
            Trackbar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar1.LargeChange = 10;
            Trackbar1.Location = new Point(191, 107);
            Trackbar1.Maximum = 50;
            Trackbar1.Minimum = 0;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(163, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 100;
            Trackbar1.Value = 1;
            // 
            // Label10
            // 
            Label10.Location = new Point(47, 74);
            Label10.Name = "Label10";
            Label10.Size = new Size(138, 24);
            Label10.TabIndex = 87;
            Label10.Text = "Classic button width:";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar3
            // 
            Trackbar3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar3.LargeChange = 10;
            Trackbar3.Location = new Point(191, 77);
            Trackbar3.Maximum = 50;
            Trackbar3.Minimum = 18;
            Trackbar3.Name = "Trackbar3";
            Trackbar3.Size = new Size(163, 19);
            Trackbar3.SmallChange = 1;
            Trackbar3.TabIndex = 102;
            Trackbar3.Value = 18;
            // 
            // Label8
            // 
            Label8.Location = new Point(47, 44);
            Label8.Name = "Label8";
            Label8.Size = new Size(138, 24);
            Label8.TabIndex = 86;
            Label8.Text = "Height:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar2
            // 
            Trackbar2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar2.LargeChange = 10;
            Trackbar2.Location = new Point(191, 47);
            Trackbar2.Maximum = 50;
            Trackbar2.Minimum = 17;
            Trackbar2.Name = "Trackbar2";
            Trackbar2.Size = new Size(163, 19);
            Trackbar2.SmallChange = 1;
            Trackbar2.TabIndex = 101;
            Trackbar2.Value = 17;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(GroupBox11);
            TabPage2.Controls.Add(AlertBox1);
            TabPage2.Controls.Add(GroupBox13);
            TabPage2.Controls.Add(GroupBox5);
            TabPage2.Controls.Add(GroupBox6);
            TabPage2.Controls.Add(GroupBox3);
            TabPage2.ForeColor = Color.White;
            TabPage2.Location = new Point(124, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(958, 439);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Icons";
            // 
            // GroupBox11
            // 
            GroupBox11.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox11.Controls.Add(PictureBox32);
            GroupBox11.Controls.Add(Label29);
            GroupBox11.Controls.Add(PictureBox12);
            GroupBox11.Controls.Add(Trackbar5);
            GroupBox11.Controls.Add(i_s_s);
            GroupBox11.Controls.Add(Label22);
            GroupBox11.Controls.Add(Label20);
            GroupBox11.Controls.Add(i_s_s_s);
            GroupBox11.Controls.Add(Trackbar15);
            GroupBox11.Controls.Add(PictureBox30);
            GroupBox11.Location = new Point(6, 261);
            GroupBox11.Name = "GroupBox11";
            GroupBox11.Size = new Size(402, 103);
            GroupBox11.TabIndex = 213;
            // 
            // PictureBox32
            // 
            PictureBox32.Image = (Image)resources.GetObject("PictureBox32.Image");
            PictureBox32.Location = new Point(6, 7);
            PictureBox32.Name = "PictureBox32";
            PictureBox32.Size = new Size(35, 31);
            PictureBox32.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox32.TabIndex = 83;
            PictureBox32.TabStop = false;
            // 
            // Label29
            // 
            Label29.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label29.BackColor = Color.Transparent;
            Label29.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label29.Location = new Point(47, 7);
            Label29.Name = "Label29";
            Label29.Size = new Size(348, 31);
            Label29.TabIndex = 84;
            Label29.Text = "Shell icons sizes (for Windows XP):";
            Label29.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox12
            // 
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(17, 44);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox12.TabIndex = 132;
            PictureBox12.TabStop = false;
            // 
            // Trackbar5
            // 
            Trackbar5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar5.LargeChange = 10;
            Trackbar5.Location = new Point(118, 47);
            Trackbar5.Maximum = 256;
            Trackbar5.Minimum = 16;
            Trackbar5.Name = "Trackbar5";
            Trackbar5.Size = new Size(239, 19);
            Trackbar5.SmallChange = 1;
            Trackbar5.TabIndex = 133;
            Trackbar5.Value = 32;
            // 
            // i_s_s
            // 
            i_s_s.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            i_s_s.BackColor = Color.FromArgb(43, 43, 43);
            i_s_s.DrawOnGlass = false;
            i_s_s.Font = new Font("Segoe UI", 9.0f);
            i_s_s.ForeColor = Color.White;
            i_s_s.Image = null;
            i_s_s.LineColor = Color.FromArgb(0, 81, 210);
            i_s_s.Location = new Point(363, 44);
            i_s_s.Name = "i_s_s";
            i_s_s.Size = new Size(34, 24);
            i_s_s.TabIndex = 205;
            i_s_s.UseVisualStyleBackColor = false;
            // 
            // Label22
            // 
            Label22.Location = new Point(47, 44);
            Label22.Name = "Label22";
            Label22.Size = new Size(65, 24);
            Label22.TabIndex = 131;
            Label22.Text = "Normal:";
            Label22.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label20
            // 
            Label20.Location = new Point(47, 74);
            Label20.Name = "Label20";
            Label20.Size = new Size(65, 24);
            Label20.TabIndex = 206;
            Label20.Text = "Small:";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // i_s_s_s
            // 
            i_s_s_s.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            i_s_s_s.BackColor = Color.FromArgb(43, 43, 43);
            i_s_s_s.DrawOnGlass = false;
            i_s_s_s.Font = new Font("Segoe UI", 9.0f);
            i_s_s_s.ForeColor = Color.White;
            i_s_s_s.Image = null;
            i_s_s_s.LineColor = Color.FromArgb(0, 81, 210);
            i_s_s_s.Location = new Point(363, 74);
            i_s_s_s.Name = "i_s_s_s";
            i_s_s_s.Size = new Size(34, 24);
            i_s_s_s.TabIndex = 209;
            i_s_s_s.UseVisualStyleBackColor = false;
            // 
            // Trackbar15
            // 
            Trackbar15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar15.LargeChange = 10;
            Trackbar15.Location = new Point(118, 77);
            Trackbar15.Maximum = 256;
            Trackbar15.Minimum = 16;
            Trackbar15.Name = "Trackbar15";
            Trackbar15.Size = new Size(239, 19);
            Trackbar15.SmallChange = 1;
            Trackbar15.TabIndex = 208;
            Trackbar15.Value = 32;
            // 
            // PictureBox30
            // 
            PictureBox30.Image = (Image)resources.GetObject("PictureBox30.Image");
            PictureBox30.Location = new Point(17, 74);
            PictureBox30.Name = "PictureBox30";
            PictureBox30.Size = new Size(24, 24);
            PictureBox30.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox30.TabIndex = 207;
            PictureBox30.TabStop = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(68, 50, 2);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = (Image)resources.GetObject("AlertBox1.Image");
            AlertBox1.Location = new Point(415, 352);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(536, 30);
            AlertBox1.TabIndex = 201;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "Showing effects of these values requires Explorer Restart";
            // 
            // GroupBox13
            // 
            GroupBox13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox13.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox13.Controls.Add(AlertBox2);
            GroupBox13.Controls.Add(PictureBox17);
            GroupBox13.Controls.Add(Label24);
            GroupBox13.Controls.Add(pnl_preview2);
            GroupBox13.Location = new Point(415, 6);
            GroupBox13.Margin = new Padding(4, 3, 4, 3);
            GroupBox13.Name = "GroupBox13";
            GroupBox13.Padding = new Padding(1);
            GroupBox13.Size = new Size(536, 340);
            GroupBox13.TabIndex = 200;
            // 
            // AlertBox2
            // 
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AlertBox2.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox2.CenterText = true;
            AlertBox2.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox2.Font = new Font("Segoe UI", 9.0f);
            AlertBox2.Image = null;
            AlertBox2.Location = new Point(278, 8);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(253, 24);
            AlertBox2.TabIndex = 202;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "This preview won't give you accurate results";
            // 
            // PictureBox17
            // 
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(4, 4);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(35, 32);
            PictureBox17.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox17.TabIndex = 4;
            PictureBox17.TabStop = false;
            // 
            // Label24
            // 
            Label24.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label24.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label24.Location = new Point(45, 5);
            Label24.Name = "Label24";
            Label24.Size = new Size(227, 31);
            Label24.TabIndex = 3;
            Label24.Text = "Preview";
            Label24.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnl_preview2
            // 
            pnl_preview2.BackColor = Color.DimGray;
            pnl_preview2.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview2.Controls.Add(FakeIcon1);
            pnl_preview2.Controls.Add(FakeIcon2);
            pnl_preview2.Controls.Add(FakeIcon3);
            pnl_preview2.Location = new Point(4, 39);
            pnl_preview2.Name = "pnl_preview2";
            pnl_preview2.Size = new Size(528, 297);
            pnl_preview2.TabIndex = 2;
            // 
            // FakeIcon1
            // 
            FakeIcon1.BackColor = Color.Transparent;
            FakeIcon1.ColorGlow = Color.FromArgb(75, 0, 0, 0);
            FakeIcon1.ColorText = Color.White;
            FakeIcon1.Font = new Font("Segoe UI", 9.0f);
            FakeIcon1.Icon = null;
            FakeIcon1.IconSize = 32;
            FakeIcon1.Location = new Point(1, 3);
            FakeIcon1.Name = "FakeIcon1";
            FakeIcon1.Size = new Size(76, 70);
            FakeIcon1.TabIndex = 3;
            FakeIcon1.Text = "Icon 1";
            // 
            // FakeIcon2
            // 
            FakeIcon2.BackColor = Color.Transparent;
            FakeIcon2.ColorGlow = Color.FromArgb(75, 0, 0, 0);
            FakeIcon2.ColorText = Color.White;
            FakeIcon2.Font = new Font("Segoe UI", 9.0f);
            FakeIcon2.Icon = null;
            FakeIcon2.IconSize = 32;
            FakeIcon2.Location = new Point(1, 103);
            FakeIcon2.Name = "FakeIcon2";
            FakeIcon2.Size = new Size(76, 70);
            FakeIcon2.TabIndex = 6;
            FakeIcon2.Text = "Icon 2";
            // 
            // FakeIcon3
            // 
            FakeIcon3.BackColor = Color.Transparent;
            FakeIcon3.ColorGlow = Color.FromArgb(75, 0, 0, 0);
            FakeIcon3.ColorText = Color.White;
            FakeIcon3.Font = new Font("Segoe UI", 9.0f);
            FakeIcon3.Icon = null;
            FakeIcon3.IconSize = 32;
            FakeIcon3.Location = new Point(103, 3);
            FakeIcon3.Name = "FakeIcon3";
            FakeIcon3.Size = new Size(76, 70);
            FakeIcon3.TabIndex = 5;
            FakeIcon3.Tag = "";
            FakeIcon3.Text = "Icon 3";
            // 
            // GroupBox5
            // 
            GroupBox5.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox5.Controls.Add(i_d_s);
            GroupBox5.Controls.Add(PictureBox21);
            GroupBox5.Controls.Add(Label9);
            GroupBox5.Controls.Add(PictureBox14);
            GroupBox5.Controls.Add(Trackbar7);
            GroupBox5.Controls.Add(Label16);
            GroupBox5.Location = new Point(6, 181);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Size = new Size(402, 74);
            GroupBox5.TabIndex = 135;
            // 
            // i_d_s
            // 
            i_d_s.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            i_d_s.BackColor = Color.FromArgb(43, 43, 43);
            i_d_s.DrawOnGlass = false;
            i_d_s.Font = new Font("Segoe UI", 9.0f);
            i_d_s.ForeColor = Color.White;
            i_d_s.Image = null;
            i_d_s.LineColor = Color.FromArgb(0, 81, 210);
            i_d_s.Location = new Point(363, 44);
            i_d_s.Name = "i_d_s";
            i_d_s.Size = new Size(34, 24);
            i_d_s.TabIndex = 204;
            i_d_s.UseVisualStyleBackColor = false;
            // 
            // PictureBox21
            // 
            PictureBox21.Image = (Image)resources.GetObject("PictureBox21.Image");
            PictureBox21.Location = new Point(6, 7);
            PictureBox21.Name = "PictureBox21";
            PictureBox21.Size = new Size(35, 31);
            PictureBox21.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox21.TabIndex = 83;
            PictureBox21.TabStop = false;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label9.BackColor = Color.Transparent;
            Label9.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label9.Location = new Point(47, 7);
            Label9.Name = "Label9";
            Label9.Size = new Size(348, 31);
            Label9.TabIndex = 84;
            Label9.Text = "Sizes:";
            Label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox14
            // 
            PictureBox14.Image = (Image)resources.GetObject("PictureBox14.Image");
            PictureBox14.Location = new Point(17, 44);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox14.TabIndex = 117;
            PictureBox14.TabStop = false;
            // 
            // Trackbar7
            // 
            Trackbar7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar7.LargeChange = 10;
            Trackbar7.Location = new Point(118, 47);
            Trackbar7.Maximum = 256;
            Trackbar7.Minimum = 16;
            Trackbar7.Name = "Trackbar7";
            Trackbar7.Size = new Size(239, 19);
            Trackbar7.SmallChange = 1;
            Trackbar7.TabIndex = 129;
            Trackbar7.Value = 32;
            // 
            // Label16
            // 
            Label16.Location = new Point(47, 44);
            Label16.Name = "Label16";
            Label16.Size = new Size(65, 24);
            Label16.TabIndex = 86;
            Label16.Text = "Desktop:";
            Label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox6
            // 
            GroupBox6.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox6.Controls.Add(PictureBox20);
            GroupBox6.Controls.Add(Label37);
            GroupBox6.Controls.Add(Button2);
            GroupBox6.Controls.Add(Label2);
            GroupBox6.Location = new Point(6, 6);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Size = new Size(402, 58);
            GroupBox6.TabIndex = 133;
            // 
            // PictureBox20
            // 
            PictureBox20.Image = (Image)resources.GetObject("PictureBox20.Image");
            PictureBox20.Location = new Point(6, 14);
            PictureBox20.Name = "PictureBox20";
            PictureBox20.Size = new Size(35, 31);
            PictureBox20.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox20.TabIndex = 83;
            PictureBox20.TabStop = false;
            // 
            // Label37
            // 
            Label37.BackColor = Color.Transparent;
            Label37.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label37.Location = new Point(47, 14);
            Label37.Name = "Label37";
            Label37.Size = new Size(42, 31);
            Label37.TabIndex = 84;
            Label37.Text = "Font:";
            Label37.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(43, 43, 43);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(0, 81, 210);
            Button2.Location = new Point(363, 18);
            Button2.Name = "Button2";
            Button2.Size = new Size(34, 23);
            Button2.TabIndex = 7;
            Button2.Text = "...";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Location = new Point(95, 7);
            Label2.Name = "Label2";
            Label2.Size = new Size(262, 44);
            Label2.TabIndex = 1;
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox3
            // 
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(i_s_h);
            GroupBox3.Controls.Add(i_s_v);
            GroupBox3.Controls.Add(PictureBox13);
            GroupBox3.Controls.Add(PictureBox15);
            GroupBox3.Controls.Add(PictureBox16);
            GroupBox3.Controls.Add(Label41);
            GroupBox3.Controls.Add(Label42);
            GroupBox3.Controls.Add(Label49);
            GroupBox3.Controls.Add(Trackbar4);
            GroupBox3.Controls.Add(Trackbar6);
            GroupBox3.Location = new Point(6, 70);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(402, 105);
            GroupBox3.TabIndex = 134;
            // 
            // i_s_h
            // 
            i_s_h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            i_s_h.BackColor = Color.FromArgb(43, 43, 43);
            i_s_h.DrawOnGlass = false;
            i_s_h.Font = new Font("Segoe UI", 9.0f);
            i_s_h.ForeColor = Color.White;
            i_s_h.Image = null;
            i_s_h.LineColor = Color.FromArgb(0, 81, 210);
            i_s_h.Location = new Point(363, 74);
            i_s_h.Name = "i_s_h";
            i_s_h.Size = new Size(34, 24);
            i_s_h.TabIndex = 205;
            i_s_h.UseVisualStyleBackColor = false;
            // 
            // i_s_v
            // 
            i_s_v.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            i_s_v.BackColor = Color.FromArgb(43, 43, 43);
            i_s_v.DrawOnGlass = false;
            i_s_v.Font = new Font("Segoe UI", 9.0f);
            i_s_v.ForeColor = Color.White;
            i_s_v.Image = null;
            i_s_v.LineColor = Color.FromArgb(0, 81, 210);
            i_s_v.Location = new Point(363, 44);
            i_s_v.Name = "i_s_v";
            i_s_v.Size = new Size(34, 24);
            i_s_v.TabIndex = 204;
            i_s_v.UseVisualStyleBackColor = false;
            // 
            // PictureBox13
            // 
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(17, 74);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(24, 24);
            PictureBox13.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox13.TabIndex = 119;
            PictureBox13.TabStop = false;
            // 
            // PictureBox15
            // 
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(17, 44);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox15.TabIndex = 117;
            PictureBox15.TabStop = false;
            // 
            // PictureBox16
            // 
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(6, 7);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(35, 31);
            PictureBox16.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox16.TabIndex = 83;
            PictureBox16.TabStop = false;
            // 
            // Label41
            // 
            Label41.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label41.BackColor = Color.Transparent;
            Label41.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label41.Location = new Point(47, 7);
            Label41.Name = "Label41";
            Label41.Size = new Size(348, 31);
            Label41.TabIndex = 84;
            Label41.Text = "Spacing:";
            Label41.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label42
            // 
            Label42.Location = new Point(47, 74);
            Label42.Name = "Label42";
            Label42.Size = new Size(65, 24);
            Label42.TabIndex = 87;
            Label42.Text = "Horizontal:";
            Label42.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label49
            // 
            Label49.Location = new Point(47, 44);
            Label49.Name = "Label49";
            Label49.Size = new Size(65, 24);
            Label49.TabIndex = 86;
            Label49.Text = "Vertical:";
            Label49.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar4
            // 
            Trackbar4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar4.LargeChange = 10;
            Trackbar4.Location = new Point(118, 47);
            Trackbar4.Maximum = 100;
            Trackbar4.Minimum = 30;
            Trackbar4.Name = "Trackbar4";
            Trackbar4.Size = new Size(239, 19);
            Trackbar4.SmallChange = 1;
            Trackbar4.TabIndex = 105;
            Trackbar4.Value = 30;
            // 
            // Trackbar6
            // 
            Trackbar6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar6.LargeChange = 10;
            Trackbar6.Location = new Point(118, 77);
            Trackbar6.Maximum = 100;
            Trackbar6.Minimum = 30;
            Trackbar6.Name = "Trackbar6";
            Trackbar6.Size = new Size(239, 19);
            Trackbar6.SmallChange = 1;
            Trackbar6.TabIndex = 103;
            Trackbar6.Value = 30;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(GroupBox14);
            TabPage3.Controls.Add(GroupBox7);
            TabPage3.Controls.Add(GroupBox8);
            TabPage3.Location = new Point(124, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(958, 439);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Menus";
            // 
            // GroupBox14
            // 
            GroupBox14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox14.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox14.Controls.Add(tabs_preview_2);
            GroupBox14.Controls.Add(PictureBox33);
            GroupBox14.Controls.Add(Label27);
            GroupBox14.Location = new Point(415, 6);
            GroupBox14.Margin = new Padding(4, 3, 4, 3);
            GroupBox14.Name = "GroupBox14";
            GroupBox14.Padding = new Padding(1);
            GroupBox14.Size = new Size(536, 340);
            GroupBox14.TabIndex = 137;
            // 
            // tabs_preview_2
            // 
            tabs_preview_2.Controls.Add(TabPage4);
            tabs_preview_2.Controls.Add(TabPage8);
            tabs_preview_2.Location = new Point(4, 39);
            tabs_preview_2.Name = "tabs_preview_2";
            tabs_preview_2.SelectedIndex = 0;
            tabs_preview_2.Size = new Size(528, 297);
            tabs_preview_2.TabIndex = 138;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(pnl_preview3);
            TabPage4.Location = new Point(4, 24);
            TabPage4.Margin = new Padding(0);
            TabPage4.Name = "TabPage4";
            TabPage4.Size = new Size(520, 269);
            TabPage4.TabIndex = 0;
            TabPage4.Text = "0";
            // 
            // pnl_preview3
            // 
            pnl_preview3.BackColor = Color.Black;
            pnl_preview3.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview3.Controls.Add(AlertBox12);
            pnl_preview3.Controls.Add(Window4);
            pnl_preview3.Location = new Point(0, 0);
            pnl_preview3.Name = "pnl_preview3";
            pnl_preview3.Size = new Size(528, 297);
            pnl_preview3.TabIndex = 2;
            // 
            // AlertBox12
            // 
            AlertBox12.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox12.BackColor = Color.FromArgb(87, 71, 71);
            AlertBox12.CenterText = true;
            AlertBox12.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox12.Font = new Font("Segoe UI", 9.0f);
            AlertBox12.Image = null;
            AlertBox12.Location = new Point(6, 7);
            AlertBox12.Name = "AlertBox12";
            AlertBox12.Size = new Size(24, 21);
            AlertBox12.TabIndex = 9;
            AlertBox12.TabStop = false;
            AlertBox12.Text = null;
            AlertBox12.Visible = false;
            // 
            // Window4
            // 
            Window4.AccentColor_Active = Color.FromArgb(0, 120, 212);
            Window4.AccentColor_Enabled = true;
            Window4.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window4.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window4.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window4.Active = true;
            Window4.Anchor = AnchorStyles.None;
            Window4.BackColor = Color.Transparent;
            Window4.Controls.Add(MenuStrip1);
            Window4.DarkMode = true;
            Window4.Font = new Font("Segoe UI", 9.0f);
            Window4.Location = new Point(44, 61);
            Window4.Metrics_BorderWidth = 1;
            Window4.Metrics_CaptionHeight = 22;
            Window4.Metrics_PaddedBorderWidth = 4;
            Window4.Name = "Window4";
            Window4.Padding = new Padding(10, 40, 10, 10);
            Window4.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window4.Radius = 5;
            Window4.Shadow = true;
            Window4.Size = new Size(441, 175);
            Window4.SuspendRefresh = false;
            Window4.TabIndex = 2;
            Window4.Text = "Application";
            Window4.ToolWindow = false;
            Window4.Win7Alpha = 100;
            Window4.Win7ColorBal = 100;
            Window4.Win7GlowBal = 100;
            Window4.Win7Noise = 1.0f;
            Window4.WinVista = false;
            // 
            // MenuStrip1
            // 
            MenuStrip1.AutoSize = false;
            MenuStrip1.BackColor = Color.White;
            MenuStrip1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MenuStrip1.GripMargin = new Padding(2, 0, 0, 0);
            MenuStrip1.Items.AddRange(new ToolStripItem[] { MenuParentToolStripMenuItem, MenuParent2ToolStripMenuItem });
            MenuStrip1.Location = new Point(10, 40);
            MenuStrip1.Name = "MenuStrip1";
            MenuStrip1.Padding = new Padding(0);
            MenuStrip1.RenderMode = ToolStripRenderMode.System;
            MenuStrip1.Size = new Size(421, 19);
            MenuStrip1.TabIndex = 1;
            MenuStrip1.Text = "MenuStrip1";
            // 
            // MenuParentToolStripMenuItem
            // 
            MenuParentToolStripMenuItem.Name = "MenuParentToolStripMenuItem";
            MenuParentToolStripMenuItem.Size = new Size(86, 19);
            MenuParentToolStripMenuItem.Text = "Menu item 1";
            // 
            // MenuParent2ToolStripMenuItem
            // 
            MenuParent2ToolStripMenuItem.Name = "MenuParent2ToolStripMenuItem";
            MenuParent2ToolStripMenuItem.Size = new Size(86, 19);
            MenuParent2ToolStripMenuItem.Text = "Menu item 2";
            // 
            // TabPage8
            // 
            TabPage8.BackColor = Color.FromArgb(25, 25, 25);
            TabPage8.Controls.Add(Classic_Preview3);
            TabPage8.Location = new Point(4, 24);
            TabPage8.Margin = new Padding(0);
            TabPage8.Name = "TabPage8";
            TabPage8.Size = new Size(520, 269);
            TabPage8.TabIndex = 1;
            TabPage8.Text = "1";
            // 
            // Classic_Preview3
            // 
            Classic_Preview3.BackColor = Color.Black;
            Classic_Preview3.BackgroundImageLayout = ImageLayout.Center;
            Classic_Preview3.Controls.Add(WindowR3);
            Classic_Preview3.Location = new Point(0, 0);
            Classic_Preview3.Name = "Classic_Preview3";
            Classic_Preview3.Size = new Size(528, 297);
            Classic_Preview3.TabIndex = 3;
            // 
            // WindowR3
            // 
            WindowR3.BackColor = Color.FromArgb(192, 192, 192);
            WindowR3.ButtonDkShadow = Color.Black;
            WindowR3.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR3.ButtonHilight = Color.White;
            WindowR3.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR3.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR3.ButtonText = Color.Black;
            WindowR3.Color1 = Color.FromArgb(0, 0, 128);
            WindowR3.Color2 = Color.FromArgb(16, 132, 208);
            WindowR3.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR3.ColorGradient = true;
            WindowR3.ControlBox = true;
            WindowR3.Controls.Add(PanelR2);
            WindowR3.Controls.Add(MenuStrip2);
            WindowR3.Flat = false;
            WindowR3.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR3.ForeColor = Color.White;
            WindowR3.Location = new Point(52, 64);
            WindowR3.MaximizeBox = false;
            WindowR3.Metrics_BorderWidth = 2;
            WindowR3.Metrics_CaptionHeight = 19;
            WindowR3.Metrics_CaptionWidth = 19;
            WindowR3.Metrics_PaddedBorderWidth = 1;
            WindowR3.MinimizeBox = false;
            WindowR3.Name = "WindowR3";
            WindowR3.Padding = new Padding(6, 26, 6, 6);
            WindowR3.Size = new Size(424, 168);
            WindowR3.TabIndex = 9;
            WindowR3.Text = "Application";
            WindowR3.UseItAsMenu = false;
            // 
            // PanelR2
            // 
            PanelR2.BackColor = Color.FromArgb(192, 192, 192);
            PanelR2.ButtonDkShadow = Color.FromArgb(105, 105, 105);
            PanelR2.ButtonHilight = Color.White;
            PanelR2.ButtonLight = Color.FromArgb(227, 227, 227);
            PanelR2.ButtonShadow = Color.FromArgb(128, 128, 128);
            PanelR2.Dock = DockStyle.Fill;
            PanelR2.Flat = false;
            PanelR2.Font = new Font("Microsoft Sans Serif", 8.0f);
            PanelR2.ForeColor = Color.Black;
            PanelR2.Location = new Point(6, 45);
            PanelR2.Name = "PanelR2";
            PanelR2.Size = new Size(412, 117);
            PanelR2.Style2 = false;
            PanelR2.TabIndex = 15;
            // 
            // MenuStrip2
            // 
            MenuStrip2.AutoSize = false;
            MenuStrip2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MenuStrip2.GripMargin = new Padding(2, 0, 0, 0);
            MenuStrip2.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem1, ToolStripMenuItem4 });
            MenuStrip2.Location = new Point(6, 26);
            MenuStrip2.Name = "MenuStrip2";
            MenuStrip2.Padding = new Padding(0);
            MenuStrip2.RenderMode = ToolStripRenderMode.System;
            MenuStrip2.Size = new Size(412, 19);
            MenuStrip2.TabIndex = 11;
            MenuStrip2.Text = "MenuStrip2";
            // 
            // ToolStripMenuItem1
            // 
            ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            ToolStripMenuItem1.Size = new Size(86, 19);
            ToolStripMenuItem1.Text = "Menu item 1";
            // 
            // ToolStripMenuItem4
            // 
            ToolStripMenuItem4.Name = "ToolStripMenuItem4";
            ToolStripMenuItem4.Size = new Size(86, 19);
            ToolStripMenuItem4.Text = "Menu item 2";
            // 
            // PictureBox33
            // 
            PictureBox33.Image = (Image)resources.GetObject("PictureBox33.Image");
            PictureBox33.Location = new Point(4, 4);
            PictureBox33.Name = "PictureBox33";
            PictureBox33.Size = new Size(35, 32);
            PictureBox33.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox33.TabIndex = 4;
            PictureBox33.TabStop = false;
            // 
            // Label27
            // 
            Label27.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label27.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label27.Location = new Point(45, 5);
            Label27.Name = "Label27";
            Label27.Size = new Size(486, 31);
            Label27.TabIndex = 3;
            Label27.Text = "Preview";
            Label27.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox7
            // 
            GroupBox7.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox7.Controls.Add(Button3);
            GroupBox7.Controls.Add(Label3);
            GroupBox7.Controls.Add(PictureBox18);
            GroupBox7.Controls.Add(Label17);
            GroupBox7.Location = new Point(6, 6);
            GroupBox7.Name = "GroupBox7";
            GroupBox7.Size = new Size(402, 58);
            GroupBox7.TabIndex = 135;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(0, 81, 210);
            Button3.Location = new Point(363, 18);
            Button3.Name = "Button3";
            Button3.Size = new Size(34, 23);
            Button3.TabIndex = 8;
            Button3.Text = "...";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Location = new Point(95, 7);
            Label3.Name = "Label3";
            Label3.Size = new Size(262, 44);
            Label3.TabIndex = 2;
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox18
            // 
            PictureBox18.Image = (Image)resources.GetObject("PictureBox18.Image");
            PictureBox18.Location = new Point(6, 14);
            PictureBox18.Name = "PictureBox18";
            PictureBox18.Size = new Size(35, 31);
            PictureBox18.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox18.TabIndex = 83;
            PictureBox18.TabStop = false;
            // 
            // Label17
            // 
            Label17.BackColor = Color.Transparent;
            Label17.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label17.Location = new Point(47, 14);
            Label17.Name = "Label17";
            Label17.Size = new Size(42, 31);
            Label17.TabIndex = 84;
            Label17.Text = "Font:";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox8
            // 
            GroupBox8.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox8.Controls.Add(m_w);
            GroupBox8.Controls.Add(m_h);
            GroupBox8.Controls.Add(PictureBox19);
            GroupBox8.Controls.Add(PictureBox22);
            GroupBox8.Controls.Add(Trackbar9);
            GroupBox8.Controls.Add(Trackbar8);
            GroupBox8.Controls.Add(PictureBox23);
            GroupBox8.Controls.Add(Label21);
            GroupBox8.Controls.Add(Label46);
            GroupBox8.Controls.Add(Label47);
            GroupBox8.Location = new Point(6, 70);
            GroupBox8.Name = "GroupBox8";
            GroupBox8.Size = new Size(402, 107);
            GroupBox8.TabIndex = 136;
            // 
            // m_w
            // 
            m_w.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            m_w.BackColor = Color.FromArgb(43, 43, 43);
            m_w.DrawOnGlass = false;
            m_w.Font = new Font("Segoe UI", 9.0f);
            m_w.ForeColor = Color.White;
            m_w.Image = null;
            m_w.LineColor = Color.FromArgb(0, 81, 210);
            m_w.Location = new Point(363, 74);
            m_w.Name = "m_w";
            m_w.Size = new Size(34, 24);
            m_w.TabIndex = 206;
            m_w.UseVisualStyleBackColor = false;
            // 
            // m_h
            // 
            m_h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            m_h.BackColor = Color.FromArgb(43, 43, 43);
            m_h.DrawOnGlass = false;
            m_h.Font = new Font("Segoe UI", 9.0f);
            m_h.ForeColor = Color.White;
            m_h.Image = null;
            m_h.LineColor = Color.FromArgb(0, 81, 210);
            m_h.Location = new Point(363, 44);
            m_h.Name = "m_h";
            m_h.Size = new Size(34, 24);
            m_h.TabIndex = 205;
            m_h.UseVisualStyleBackColor = false;
            // 
            // PictureBox19
            // 
            PictureBox19.Image = (Image)resources.GetObject("PictureBox19.Image");
            PictureBox19.Location = new Point(17, 74);
            PictureBox19.Name = "PictureBox19";
            PictureBox19.Size = new Size(24, 24);
            PictureBox19.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox19.TabIndex = 119;
            PictureBox19.TabStop = false;
            // 
            // PictureBox22
            // 
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(17, 44);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(24, 24);
            PictureBox22.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox22.TabIndex = 117;
            PictureBox22.TabStop = false;
            // 
            // Trackbar9
            // 
            Trackbar9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar9.LargeChange = 10;
            Trackbar9.Location = new Point(118, 47);
            Trackbar9.Maximum = 50;
            Trackbar9.Minimum = 16;
            Trackbar9.Name = "Trackbar9";
            Trackbar9.Size = new Size(239, 19);
            Trackbar9.SmallChange = 1;
            Trackbar9.TabIndex = 106;
            Trackbar9.Value = 16;
            // 
            // Trackbar8
            // 
            Trackbar8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar8.LargeChange = 10;
            Trackbar8.Location = new Point(118, 77);
            Trackbar8.Maximum = 50;
            Trackbar8.Minimum = 16;
            Trackbar8.Name = "Trackbar8";
            Trackbar8.Size = new Size(239, 19);
            Trackbar8.SmallChange = 1;
            Trackbar8.TabIndex = 107;
            Trackbar8.Value = 16;
            // 
            // PictureBox23
            // 
            PictureBox23.Image = (Image)resources.GetObject("PictureBox23.Image");
            PictureBox23.Location = new Point(6, 7);
            PictureBox23.Name = "PictureBox23";
            PictureBox23.Size = new Size(35, 31);
            PictureBox23.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox23.TabIndex = 83;
            PictureBox23.TabStop = false;
            // 
            // Label21
            // 
            Label21.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label21.BackColor = Color.Transparent;
            Label21.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label21.Location = new Point(47, 7);
            Label21.Name = "Label21";
            Label21.Size = new Size(348, 31);
            Label21.TabIndex = 84;
            Label21.Text = "Metrics:";
            Label21.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label46
            // 
            Label46.Location = new Point(47, 74);
            Label46.Name = "Label46";
            Label46.Size = new Size(65, 24);
            Label46.TabIndex = 87;
            Label46.Text = "Width:";
            Label46.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label47
            // 
            Label47.Location = new Point(47, 44);
            Label47.Name = "Label47";
            Label47.Size = new Size(65, 24);
            Label47.TabIndex = 86;
            Label47.Text = "Height:";
            Label47.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(GroupBox17);
            TabPage5.Controls.Add(AlertBox4);
            TabPage5.Controls.Add(GroupBox15);
            TabPage5.Controls.Add(GroupBox10);
            TabPage5.Controls.Add(GroupBox9);
            TabPage5.Location = new Point(124, 4);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(3);
            TabPage5.Size = new Size(958, 439);
            TabPage5.TabIndex = 4;
            TabPage5.Text = "Micsellaneous";
            // 
            // GroupBox17
            // 
            GroupBox17.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox17.Controls.Add(CheckBox1);
            GroupBox17.Controls.Add(PictureBox31);
            GroupBox17.Controls.Add(PictureBox44);
            GroupBox17.Controls.Add(Label26);
            GroupBox17.Location = new Point(6, 271);
            GroupBox17.Name = "GroupBox17";
            GroupBox17.Size = new Size(402, 75);
            GroupBox17.TabIndex = 204;
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(34, 34, 34);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(47, 44);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(347, 24);
            CheckBox1.TabIndex = 121;
            CheckBox1.Text = "Single bit per pixel font";
            // 
            // PictureBox31
            // 
            PictureBox31.Image = (Image)resources.GetObject("PictureBox31.Image");
            PictureBox31.Location = new Point(17, 44);
            PictureBox31.Name = "PictureBox31";
            PictureBox31.Size = new Size(24, 24);
            PictureBox31.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox31.TabIndex = 120;
            PictureBox31.TabStop = false;
            // 
            // PictureBox44
            // 
            PictureBox44.Image = (Image)resources.GetObject("PictureBox44.Image");
            PictureBox44.Location = new Point(6, 7);
            PictureBox44.Name = "PictureBox44";
            PictureBox44.Size = new Size(35, 31);
            PictureBox44.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox44.TabIndex = 83;
            PictureBox44.TabStop = false;
            // 
            // Label26
            // 
            Label26.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label26.BackColor = Color.Transparent;
            Label26.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label26.Location = new Point(47, 7);
            Label26.Name = "Label26";
            Label26.Size = new Size(348, 31);
            Label26.TabIndex = 84;
            Label26.Text = "Others:";
            Label26.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AlertBox4
            // 
            AlertBox4.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox4.BackColor = Color.FromArgb(70, 51, 2);
            AlertBox4.CenterText = false;
            AlertBox4.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox4.Font = new Font("Segoe UI", 9.0f);
            AlertBox4.Image = (Image)resources.GetObject("AlertBox4.Image");
            AlertBox4.Location = new Point(415, 352);
            AlertBox4.Name = "AlertBox4";
            AlertBox4.Size = new Size(536, 30);
            AlertBox4.TabIndex = 203;
            AlertBox4.TabStop = false;
            AlertBox4.Text = "Changing Status Font requires logoff and logon";
            // 
            // GroupBox15
            // 
            GroupBox15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox15.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox15.Controls.Add(tabs_preview_3);
            GroupBox15.Controls.Add(PictureBox34);
            GroupBox15.Controls.Add(Label36);
            GroupBox15.Location = new Point(415, 6);
            GroupBox15.Margin = new Padding(4, 3, 4, 3);
            GroupBox15.Name = "GroupBox15";
            GroupBox15.Padding = new Padding(1);
            GroupBox15.Size = new Size(536, 340);
            GroupBox15.TabIndex = 139;
            // 
            // tabs_preview_3
            // 
            tabs_preview_3.Controls.Add(TabPage9);
            tabs_preview_3.Controls.Add(TabPage10);
            tabs_preview_3.Location = new Point(4, 39);
            tabs_preview_3.Name = "tabs_preview_3";
            tabs_preview_3.SelectedIndex = 0;
            tabs_preview_3.Size = new Size(528, 297);
            tabs_preview_3.TabIndex = 204;
            // 
            // TabPage9
            // 
            TabPage9.BackColor = Color.FromArgb(25, 25, 25);
            TabPage9.Controls.Add(pnl_preview4);
            TabPage9.Location = new Point(4, 24);
            TabPage9.Margin = new Padding(0);
            TabPage9.Name = "TabPage9";
            TabPage9.Size = new Size(520, 269);
            TabPage9.TabIndex = 0;
            TabPage9.Text = "0";
            // 
            // pnl_preview4
            // 
            pnl_preview4.BackColor = Color.Black;
            pnl_preview4.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview4.Controls.Add(AlertBox13);
            pnl_preview4.Controls.Add(Window6);
            pnl_preview4.Location = new Point(0, 0);
            pnl_preview4.Name = "pnl_preview4";
            pnl_preview4.Size = new Size(528, 297);
            pnl_preview4.TabIndex = 2;
            // 
            // AlertBox13
            // 
            AlertBox13.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox13.BackColor = Color.FromArgb(87, 71, 71);
            AlertBox13.CenterText = true;
            AlertBox13.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox13.Font = new Font("Segoe UI", 9.0f);
            AlertBox13.Image = null;
            AlertBox13.Location = new Point(6, 7);
            AlertBox13.Name = "AlertBox13";
            AlertBox13.Size = new Size(24, 21);
            AlertBox13.TabIndex = 9;
            AlertBox13.TabStop = false;
            AlertBox13.Text = null;
            AlertBox13.Visible = false;
            // 
            // Window6
            // 
            Window6.AccentColor_Active = Color.FromArgb(0, 120, 212);
            Window6.AccentColor_Enabled = true;
            Window6.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window6.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window6.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window6.Active = true;
            Window6.Anchor = AnchorStyles.None;
            Window6.BackColor = Color.Transparent;
            Window6.Controls.Add(Panel2);
            Window6.DarkMode = true;
            Window6.Font = new Font("Segoe UI", 9.0f);
            Window6.Location = new Point(62, 32);
            Window6.Metrics_BorderWidth = 1;
            Window6.Metrics_CaptionHeight = 22;
            Window6.Metrics_PaddedBorderWidth = 4;
            Window6.Name = "Window6";
            Window6.Padding = new Padding(10, 40, 10, 10);
            Window6.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window6.Radius = 5;
            Window6.Shadow = true;
            Window6.Size = new Size(405, 233);
            Window6.SuspendRefresh = false;
            Window6.TabIndex = 2;
            Window6.Text = "Application";
            Window6.ToolWindow = false;
            Window6.Win7Alpha = 100;
            Window6.Win7ColorBal = 100;
            Window6.Win7GlowBal = 100;
            Window6.Win7Noise = 1.0f;
            Window6.WinVista = false;
            // 
            // Panel2
            // 
            Panel2.BackColor = Color.Transparent;
            Panel2.Controls.Add(msgLbl);
            Panel2.Controls.Add(PictureBox35);
            Panel2.Controls.Add(HScrollBar1);
            Panel2.Controls.Add(VScrollBar1);
            Panel2.Controls.Add(StatusStrip1);
            Panel2.Dock = DockStyle.Fill;
            Panel2.Location = new Point(10, 40);
            Panel2.Name = "Panel2";
            Panel2.Padding = new Padding(2);
            Panel2.Size = new Size(385, 183);
            Panel2.TabIndex = 0;
            // 
            // msgLbl
            // 
            msgLbl.Dock = DockStyle.Fill;
            msgLbl.DrawOnGlass = false;
            msgLbl.Location = new Point(52, 2);
            msgLbl.Name = "msgLbl";
            msgLbl.Size = new Size(314, 140);
            msgLbl.TabIndex = 85;
            msgLbl.Text = "This is a text displayed as a message";
            msgLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox35
            // 
            PictureBox35.Dock = DockStyle.Left;
            PictureBox35.Location = new Point(2, 2);
            PictureBox35.Name = "PictureBox35";
            PictureBox35.Size = new Size(50, 140);
            PictureBox35.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox35.TabIndex = 84;
            PictureBox35.TabStop = false;
            // 
            // HScrollBar1
            // 
            HScrollBar1.Dock = DockStyle.Bottom;
            HScrollBar1.Location = new Point(2, 142);
            HScrollBar1.Name = "HScrollBar1";
            HScrollBar1.Size = new Size(364, 17);
            HScrollBar1.TabIndex = 89;
            // 
            // VScrollBar1
            // 
            VScrollBar1.Dock = DockStyle.Right;
            VScrollBar1.Location = new Point(366, 2);
            VScrollBar1.Name = "VScrollBar1";
            VScrollBar1.Size = new Size(17, 157);
            VScrollBar1.TabIndex = 88;
            // 
            // StatusStrip1
            // 
            StatusStrip1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            StatusStrip1.Items.AddRange(new ToolStripItem[] { statusLbl });
            StatusStrip1.Location = new Point(2, 159);
            StatusStrip1.Name = "StatusStrip1";
            StatusStrip1.Size = new Size(381, 22);
            StatusStrip1.TabIndex = 3;
            StatusStrip1.Text = "StatusStrip1";
            // 
            // statusLbl
            // 
            statusLbl.ForeColor = Color.Black;
            statusLbl.Name = "statusLbl";
            statusLbl.Size = new Size(39, 17);
            statusLbl.Text = "Status";
            // 
            // TabPage10
            // 
            TabPage10.BackColor = Color.FromArgb(25, 25, 25);
            TabPage10.Controls.Add(Classic_Preview4);
            TabPage10.Location = new Point(4, 24);
            TabPage10.Margin = new Padding(0);
            TabPage10.Name = "TabPage10";
            TabPage10.Size = new Size(520, 269);
            TabPage10.TabIndex = 1;
            TabPage10.Text = "1";
            // 
            // Classic_Preview4
            // 
            Classic_Preview4.BackColor = Color.Black;
            Classic_Preview4.BackgroundImageLayout = ImageLayout.Center;
            Classic_Preview4.Controls.Add(WindowR5);
            Classic_Preview4.Location = new Point(0, 0);
            Classic_Preview4.Name = "Classic_Preview4";
            Classic_Preview4.Size = new Size(528, 297);
            Classic_Preview4.TabIndex = 3;
            // 
            // WindowR5
            // 
            WindowR5.BackColor = Color.FromArgb(192, 192, 192);
            WindowR5.ButtonDkShadow = Color.Black;
            WindowR5.ButtonFace = Color.FromArgb(192, 192, 192);
            WindowR5.ButtonHilight = Color.White;
            WindowR5.ButtonLight = Color.FromArgb(192, 192, 192);
            WindowR5.ButtonShadow = Color.FromArgb(128, 128, 128);
            WindowR5.ButtonText = Color.Black;
            WindowR5.Color1 = Color.FromArgb(0, 0, 128);
            WindowR5.Color2 = Color.FromArgb(16, 132, 208);
            WindowR5.ColorBorder = Color.FromArgb(192, 192, 192);
            WindowR5.ColorGradient = true;
            WindowR5.ControlBox = true;
            WindowR5.Controls.Add(Panel3);
            WindowR5.Flat = false;
            WindowR5.Font = new Font("Microsoft Sans Serif", 8.0f);
            WindowR5.ForeColor = Color.White;
            WindowR5.Location = new Point(66, 39);
            WindowR5.MaximizeBox = false;
            WindowR5.Metrics_BorderWidth = 2;
            WindowR5.Metrics_CaptionHeight = 19;
            WindowR5.Metrics_CaptionWidth = 19;
            WindowR5.Metrics_PaddedBorderWidth = 1;
            WindowR5.MinimizeBox = false;
            WindowR5.Name = "WindowR5";
            WindowR5.Padding = new Padding(6, 26, 6, 6);
            WindowR5.Size = new Size(397, 218);
            WindowR5.TabIndex = 204;
            WindowR5.Text = "Application";
            WindowR5.UseItAsMenu = false;
            // 
            // Panel3
            // 
            Panel3.BackColor = Color.Transparent;
            Panel3.Controls.Add(Label13);
            Panel3.Controls.Add(PictureBox36);
            Panel3.Controls.Add(ScrollBarR1);
            Panel3.Controls.Add(ScrollBarR2);
            Panel3.Controls.Add(Panel1);
            Panel3.Controls.Add(PanelR1);
            Panel3.Dock = DockStyle.Fill;
            Panel3.Location = new Point(6, 26);
            Panel3.Name = "Panel3";
            Panel3.Padding = new Padding(2);
            Panel3.Size = new Size(385, 186);
            Panel3.TabIndex = 7;
            // 
            // Label13
            // 
            Label13.Dock = DockStyle.Fill;
            Label13.DrawOnGlass = false;
            Label13.ForeColor = Color.Black;
            Label13.Location = new Point(52, 2);
            Label13.Name = "Label13";
            Label13.Size = new Size(315, 141);
            Label13.TabIndex = 85;
            Label13.Text = "This is a text displayed as a message";
            Label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox36
            // 
            PictureBox36.Dock = DockStyle.Left;
            PictureBox36.Location = new Point(2, 2);
            PictureBox36.Name = "PictureBox36";
            PictureBox36.Size = new Size(50, 141);
            PictureBox36.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox36.TabIndex = 84;
            PictureBox36.TabStop = false;
            // 
            // ScrollBarR1
            // 
            ScrollBarR1.BackColor = Color.FromArgb(192, 192, 192);
            ScrollBarR1.ButtonHilight = Color.White;
            ScrollBarR1.Controls.Add(ButtonR3);
            ScrollBarR1.Controls.Add(ButtonR1);
            ScrollBarR1.Controls.Add(ButtonR2);
            ScrollBarR1.Dock = DockStyle.Bottom;
            ScrollBarR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            ScrollBarR1.ForeColor = Color.Black;
            ScrollBarR1.Location = new Point(2, 143);
            ScrollBarR1.Name = "ScrollBarR1";
            ScrollBarR1.Size = new Size(365, 16);
            ScrollBarR1.TabIndex = 94;
            // 
            // ButtonR3
            // 
            ButtonR3.AppearsAsPressed = false;
            ButtonR3.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR3.ButtonDkShadow = Color.Black;
            ButtonR3.ButtonHilight = Color.White;
            ButtonR3.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR3.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR3.Dock = DockStyle.Left;
            ButtonR3.FocusRectHeight = 1;
            ButtonR3.FocusRectWidth = 1;
            ButtonR3.Font = new Font("Marlett", 8.8f);
            ButtonR3.ForeColor = Color.Black;
            ButtonR3.HatchBrush = false;
            ButtonR3.Image = null;
            ButtonR3.Location = new Point(0, 0);
            ButtonR3.Name = "ButtonR3";
            ButtonR3.Size = new Size(16, 16);
            ButtonR3.TabIndex = 5;
            ButtonR3.Text = "3";
            ButtonR3.UseItAsScrollbar = false;
            ButtonR3.UseVisualStyleBackColor = false;
            ButtonR3.WindowFrame = Color.Black;
            // 
            // ButtonR1
            // 
            ButtonR1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            ButtonR1.AppearsAsPressed = false;
            ButtonR1.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR1.ButtonDkShadow = Color.Black;
            ButtonR1.ButtonHilight = Color.White;
            ButtonR1.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR1.FocusRectHeight = 1;
            ButtonR1.FocusRectWidth = 1;
            ButtonR1.Font = new Font("Marlett", 6.0f);
            ButtonR1.ForeColor = Color.Black;
            ButtonR1.HatchBrush = false;
            ButtonR1.Image = null;
            ButtonR1.Location = new Point(66, 0);
            ButtonR1.Name = "ButtonR1";
            ButtonR1.Size = new Size(96, 16);
            ButtonR1.TabIndex = 7;
            ButtonR1.UseItAsScrollbar = true;
            ButtonR1.UseVisualStyleBackColor = false;
            ButtonR1.WindowFrame = Color.Black;
            // 
            // ButtonR2
            // 
            ButtonR2.AppearsAsPressed = false;
            ButtonR2.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR2.ButtonDkShadow = Color.Black;
            ButtonR2.ButtonHilight = Color.White;
            ButtonR2.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR2.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR2.Dock = DockStyle.Right;
            ButtonR2.FocusRectHeight = 1;
            ButtonR2.FocusRectWidth = 1;
            ButtonR2.Font = new Font("Marlett", 8.8f);
            ButtonR2.ForeColor = Color.Black;
            ButtonR2.HatchBrush = false;
            ButtonR2.Image = null;
            ButtonR2.Location = new Point(349, 0);
            ButtonR2.Name = "ButtonR2";
            ButtonR2.Size = new Size(16, 16);
            ButtonR2.TabIndex = 6;
            ButtonR2.Text = "4";
            ButtonR2.UseItAsScrollbar = false;
            ButtonR2.UseVisualStyleBackColor = false;
            ButtonR2.WindowFrame = Color.Black;
            // 
            // ScrollBarR2
            // 
            ScrollBarR2.BackColor = Color.FromArgb(192, 192, 192);
            ScrollBarR2.ButtonHilight = Color.White;
            ScrollBarR2.Controls.Add(ButtonR12);
            ScrollBarR2.Controls.Add(ButtonR11);
            ScrollBarR2.Controls.Add(ButtonR10);
            ScrollBarR2.Dock = DockStyle.Right;
            ScrollBarR2.Font = new Font("Microsoft Sans Serif", 8.0f);
            ScrollBarR2.ForeColor = Color.Black;
            ScrollBarR2.Location = new Point(367, 2);
            ScrollBarR2.Name = "ScrollBarR2";
            ScrollBarR2.Size = new Size(16, 157);
            ScrollBarR2.TabIndex = 93;
            // 
            // ButtonR12
            // 
            ButtonR12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            ButtonR12.AppearsAsPressed = false;
            ButtonR12.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR12.ButtonDkShadow = Color.Black;
            ButtonR12.ButtonHilight = Color.White;
            ButtonR12.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR12.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR12.FocusRectHeight = 1;
            ButtonR12.FocusRectWidth = 1;
            ButtonR12.Font = new Font("Marlett", 6.0f);
            ButtonR12.ForeColor = Color.Black;
            ButtonR12.HatchBrush = false;
            ButtonR12.Image = null;
            ButtonR12.Location = new Point(0, 29);
            ButtonR12.Name = "ButtonR12";
            ButtonR12.Size = new Size(16, 82);
            ButtonR12.TabIndex = 7;
            ButtonR12.UseItAsScrollbar = true;
            ButtonR12.UseVisualStyleBackColor = false;
            ButtonR12.WindowFrame = Color.Black;
            // 
            // ButtonR11
            // 
            ButtonR11.AppearsAsPressed = false;
            ButtonR11.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR11.ButtonDkShadow = Color.Black;
            ButtonR11.ButtonHilight = Color.White;
            ButtonR11.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR11.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR11.Dock = DockStyle.Bottom;
            ButtonR11.FocusRectHeight = 1;
            ButtonR11.FocusRectWidth = 1;
            ButtonR11.Font = new Font("Marlett", 8.8f, FontStyle.Regular, GraphicsUnit.Point, 2);
            ButtonR11.ForeColor = Color.Black;
            ButtonR11.HatchBrush = false;
            ButtonR11.Image = null;
            ButtonR11.Location = new Point(0, 141);
            ButtonR11.Name = "ButtonR11";
            ButtonR11.Size = new Size(16, 16);
            ButtonR11.TabIndex = 6;
            ButtonR11.Text = "u";
            ButtonR11.UseItAsScrollbar = false;
            ButtonR11.UseVisualStyleBackColor = false;
            ButtonR11.WindowFrame = Color.Black;
            // 
            // ButtonR10
            // 
            ButtonR10.AppearsAsPressed = false;
            ButtonR10.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR10.ButtonDkShadow = Color.Black;
            ButtonR10.ButtonHilight = Color.White;
            ButtonR10.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR10.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR10.Dock = DockStyle.Top;
            ButtonR10.FocusRectHeight = 1;
            ButtonR10.FocusRectWidth = 1;
            ButtonR10.Font = new Font("Marlett", 8.8f, FontStyle.Regular, GraphicsUnit.Point, 2);
            ButtonR10.ForeColor = Color.Black;
            ButtonR10.HatchBrush = false;
            ButtonR10.Image = null;
            ButtonR10.Location = new Point(0, 0);
            ButtonR10.Name = "ButtonR10";
            ButtonR10.Size = new Size(16, 16);
            ButtonR10.TabIndex = 5;
            ButtonR10.Text = "t";
            ButtonR10.UseItAsScrollbar = false;
            ButtonR10.UseVisualStyleBackColor = false;
            ButtonR10.WindowFrame = Color.Black;
            // 
            // Panel1
            // 
            Panel1.Dock = DockStyle.Bottom;
            Panel1.Location = new Point(2, 159);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(381, 5);
            Panel1.TabIndex = 96;
            // 
            // PanelR1
            // 
            PanelR1.BackColor = Color.FromArgb(192, 192, 192);
            PanelR1.ButtonDkShadow = Color.FromArgb(105, 105, 105);
            PanelR1.ButtonHilight = Color.White;
            PanelR1.ButtonLight = Color.FromArgb(227, 227, 227);
            PanelR1.ButtonShadow = Color.FromArgb(128, 128, 128);
            PanelR1.Controls.Add(Label14);
            PanelR1.Dock = DockStyle.Bottom;
            PanelR1.Flat = false;
            PanelR1.Font = new Font("Microsoft Sans Serif", 8.0f);
            PanelR1.ForeColor = Color.Black;
            PanelR1.Location = new Point(2, 164);
            PanelR1.Name = "PanelR1";
            PanelR1.Size = new Size(381, 20);
            PanelR1.Style2 = false;
            PanelR1.TabIndex = 95;
            // 
            // Label14
            // 
            Label14.BackColor = Color.Transparent;
            Label14.Dock = DockStyle.Fill;
            Label14.DrawOnGlass = false;
            Label14.ForeColor = Color.Black;
            Label14.Location = new Point(0, 0);
            Label14.Name = "Label14";
            Label14.Size = new Size(381, 20);
            Label14.TabIndex = 86;
            Label14.Text = "Status";
            Label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox34
            // 
            PictureBox34.Image = (Image)resources.GetObject("PictureBox34.Image");
            PictureBox34.Location = new Point(4, 4);
            PictureBox34.Name = "PictureBox34";
            PictureBox34.Size = new Size(35, 32);
            PictureBox34.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox34.TabIndex = 4;
            PictureBox34.TabStop = false;
            // 
            // Label36
            // 
            Label36.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label36.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label36.Location = new Point(45, 5);
            Label36.Name = "Label36";
            Label36.Size = new Size(486, 31);
            Label36.TabIndex = 3;
            Label36.Text = "Preview";
            Label36.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox10
            // 
            GroupBox10.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox10.Controls.Add(s_w);
            GroupBox10.Controls.Add(s_h);
            GroupBox10.Controls.Add(PictureBox27);
            GroupBox10.Controls.Add(PictureBox28);
            GroupBox10.Controls.Add(PictureBox29);
            GroupBox10.Controls.Add(Label51);
            GroupBox10.Controls.Add(Trackbar10);
            GroupBox10.Controls.Add(Label53);
            GroupBox10.Controls.Add(Label54);
            GroupBox10.Controls.Add(Trackbar11);
            GroupBox10.Location = new Point(6, 158);
            GroupBox10.Name = "GroupBox10";
            GroupBox10.Size = new Size(402, 107);
            GroupBox10.TabIndex = 137;
            // 
            // s_w
            // 
            s_w.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            s_w.BackColor = Color.FromArgb(43, 43, 43);
            s_w.DrawOnGlass = false;
            s_w.Font = new Font("Segoe UI", 9.0f);
            s_w.ForeColor = Color.White;
            s_w.Image = null;
            s_w.LineColor = Color.FromArgb(0, 81, 210);
            s_w.Location = new Point(360, 74);
            s_w.Name = "s_w";
            s_w.Size = new Size(34, 24);
            s_w.TabIndex = 207;
            s_w.UseVisualStyleBackColor = false;
            // 
            // s_h
            // 
            s_h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            s_h.BackColor = Color.FromArgb(43, 43, 43);
            s_h.DrawOnGlass = false;
            s_h.Font = new Font("Segoe UI", 9.0f);
            s_h.ForeColor = Color.White;
            s_h.Image = null;
            s_h.LineColor = Color.FromArgb(0, 81, 210);
            s_h.Location = new Point(360, 44);
            s_h.Name = "s_h";
            s_h.Size = new Size(34, 24);
            s_h.TabIndex = 206;
            s_h.UseVisualStyleBackColor = false;
            // 
            // PictureBox27
            // 
            PictureBox27.Image = (Image)resources.GetObject("PictureBox27.Image");
            PictureBox27.Location = new Point(17, 74);
            PictureBox27.Name = "PictureBox27";
            PictureBox27.Size = new Size(24, 24);
            PictureBox27.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox27.TabIndex = 119;
            PictureBox27.TabStop = false;
            // 
            // PictureBox28
            // 
            PictureBox28.Image = (Image)resources.GetObject("PictureBox28.Image");
            PictureBox28.Location = new Point(17, 44);
            PictureBox28.Name = "PictureBox28";
            PictureBox28.Size = new Size(24, 24);
            PictureBox28.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox28.TabIndex = 117;
            PictureBox28.TabStop = false;
            // 
            // PictureBox29
            // 
            PictureBox29.Image = (Image)resources.GetObject("PictureBox29.Image");
            PictureBox29.Location = new Point(6, 7);
            PictureBox29.Name = "PictureBox29";
            PictureBox29.Size = new Size(35, 31);
            PictureBox29.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox29.TabIndex = 83;
            PictureBox29.TabStop = false;
            // 
            // Label51
            // 
            Label51.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label51.BackColor = Color.Transparent;
            Label51.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label51.Location = new Point(47, 7);
            Label51.Name = "Label51";
            Label51.Size = new Size(348, 31);
            Label51.TabIndex = 84;
            Label51.Text = "Scrollbar metrics:";
            Label51.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar10
            // 
            Trackbar10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar10.LargeChange = 10;
            Trackbar10.Location = new Point(118, 77);
            Trackbar10.Maximum = 50;
            Trackbar10.Minimum = 0;
            Trackbar10.Name = "Trackbar10";
            Trackbar10.Size = new Size(237, 19);
            Trackbar10.SmallChange = 1;
            Trackbar10.TabIndex = 111;
            Trackbar10.Text = "Trackbar10";
            Trackbar10.Value = 0;
            // 
            // Label53
            // 
            Label53.Location = new Point(47, 74);
            Label53.Name = "Label53";
            Label53.Size = new Size(65, 24);
            Label53.TabIndex = 87;
            Label53.Text = "Width:";
            Label53.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label54
            // 
            Label54.Location = new Point(47, 44);
            Label54.Name = "Label54";
            Label54.Size = new Size(65, 24);
            Label54.TabIndex = 86;
            Label54.Text = "Height:";
            Label54.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Trackbar11
            // 
            Trackbar11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Trackbar11.LargeChange = 10;
            Trackbar11.Location = new Point(118, 47);
            Trackbar11.Maximum = 50;
            Trackbar11.Minimum = 0;
            Trackbar11.Name = "Trackbar11";
            Trackbar11.Size = new Size(237, 19);
            Trackbar11.SmallChange = 1;
            Trackbar11.TabIndex = 110;
            Trackbar11.Text = "Trackbar11";
            Trackbar11.Value = 0;
            // 
            // GroupBox9
            // 
            GroupBox9.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox9.Controls.Add(PictureBox24);
            GroupBox9.Controls.Add(PictureBox25);
            GroupBox9.Controls.Add(Label6);
            GroupBox9.Controls.Add(Label4);
            GroupBox9.Controls.Add(Button6);
            GroupBox9.Controls.Add(Label11);
            GroupBox9.Controls.Add(Button4);
            GroupBox9.Controls.Add(Label18);
            GroupBox9.Controls.Add(PictureBox26);
            GroupBox9.Controls.Add(Label50);
            GroupBox9.Location = new Point(6, 6);
            GroupBox9.Name = "GroupBox9";
            GroupBox9.Size = new Size(402, 146);
            GroupBox9.TabIndex = 133;
            // 
            // PictureBox24
            // 
            PictureBox24.Image = (Image)resources.GetObject("PictureBox24.Image");
            PictureBox24.Location = new Point(17, 99);
            PictureBox24.Name = "PictureBox24";
            PictureBox24.Size = new Size(24, 24);
            PictureBox24.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox24.TabIndex = 119;
            PictureBox24.TabStop = false;
            // 
            // PictureBox25
            // 
            PictureBox25.Image = (Image)resources.GetObject("PictureBox25.Image");
            PictureBox25.Location = new Point(17, 55);
            PictureBox25.Name = "PictureBox25";
            PictureBox25.Size = new Size(24, 24);
            PictureBox25.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox25.TabIndex = 118;
            PictureBox25.TabStop = false;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label6.Location = new Point(118, 89);
            Label6.Name = "Label6";
            Label6.Size = new Size(236, 44);
            Label6.TabIndex = 3;
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.Location = new Point(118, 45);
            Label4.Name = "Label4";
            Label4.Size = new Size(236, 44);
            Label4.TabIndex = 5;
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = null;
            Button6.LineColor = Color.FromArgb(0, 81, 210);
            Button6.Location = new Point(360, 100);
            Button6.Name = "Button6";
            Button6.Size = new Size(34, 23);
            Button6.TabIndex = 11;
            Button6.Text = "...";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Label11
            // 
            Label11.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label11.Location = new Point(50, 99);
            Label11.Name = "Label11";
            Label11.Size = new Size(62, 24);
            Label11.TabIndex = 87;
            Label11.Text = "Status:";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = null;
            Button4.LineColor = Color.FromArgb(0, 81, 210);
            Button4.Location = new Point(360, 56);
            Button4.Name = "Button4";
            Button4.Size = new Size(34, 23);
            Button4.TabIndex = 9;
            Button4.Text = "...";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Label18
            // 
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label18.Location = new Point(50, 55);
            Label18.Name = "Label18";
            Label18.Size = new Size(62, 24);
            Label18.TabIndex = 86;
            Label18.Text = "Message:";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox26
            // 
            PictureBox26.Image = (Image)resources.GetObject("PictureBox26.Image");
            PictureBox26.Location = new Point(6, 7);
            PictureBox26.Name = "PictureBox26";
            PictureBox26.Size = new Size(35, 31);
            PictureBox26.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox26.TabIndex = 83;
            PictureBox26.TabStop = false;
            // 
            // Label50
            // 
            Label50.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label50.BackColor = Color.Transparent;
            Label50.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label50.Location = new Point(47, 7);
            Label50.Name = "Label50";
            Label50.Size = new Size(347, 31);
            Label50.TabIndex = 84;
            Label50.Text = "Fonts:";
            Label50.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage11
            // 
            TabPage11.BackColor = Color.FromArgb(25, 25, 25);
            TabPage11.Controls.Add(GroupBox16);
            TabPage11.Location = new Point(124, 4);
            TabPage11.Name = "TabPage11";
            TabPage11.Padding = new Padding(3);
            TabPage11.Size = new Size(958, 439);
            TabPage11.TabIndex = 5;
            TabPage11.Text = "Fonts substitutes";
            // 
            // GroupBox16
            // 
            GroupBox16.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox16.Controls.Add(AlertBox9);
            GroupBox16.Controls.Add(AlertBox7);
            GroupBox16.Controls.Add(Button19);
            GroupBox16.Controls.Add(Button18);
            GroupBox16.Controls.Add(Button16);
            GroupBox16.Controls.Add(AlertBox8);
            GroupBox16.Controls.Add(TextBox3);
            GroupBox16.Controls.Add(PictureBox37);
            GroupBox16.Controls.Add(Button17);
            GroupBox16.Controls.Add(AlertBox6);
            GroupBox16.Controls.Add(TextBox1);
            GroupBox16.Controls.Add(PictureBox45);
            GroupBox16.Controls.Add(PictureBox46);
            GroupBox16.Controls.Add(Separator1);
            GroupBox16.Controls.Add(Label28);
            GroupBox16.Controls.Add(TextBox2);
            GroupBox16.Controls.Add(Button15);
            GroupBox16.Controls.Add(Label15);
            GroupBox16.Controls.Add(Button14);
            GroupBox16.Controls.Add(AlertBox5);
            GroupBox16.Controls.Add(PictureBox42);
            GroupBox16.Controls.Add(PictureBox38);
            GroupBox16.Controls.Add(PictureBox40);
            GroupBox16.Controls.Add(Label23);
            GroupBox16.Controls.Add(Label25);
            GroupBox16.Controls.Add(PictureBox39);
            GroupBox16.Location = new Point(6, 6);
            GroupBox16.Name = "GroupBox16";
            GroupBox16.Size = new Size(946, 356);
            GroupBox16.TabIndex = 217;
            // 
            // AlertBox9
            // 
            AlertBox9.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox9.BackColor = Color.FromArgb(70, 51, 2);
            AlertBox9.CenterText = false;
            AlertBox9.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox9.Font = new Font("Segoe UI", 9.0f);
            AlertBox9.Image = (Image)resources.GetObject("AlertBox9.Image");
            AlertBox9.Location = new Point(47, 181);
            AlertBox9.Name = "AlertBox9";
            AlertBox9.Size = new Size(894, 30);
            AlertBox9.TabIndex = 223;
            AlertBox9.TabStop = false;
            AlertBox9.Text = "Only start menu of Windows 10 cannot be changed. It will stay with \"Segoe UI\" eve" + "n if it is substituted correctly";
            // 
            // AlertBox7
            // 
            AlertBox7.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox7.BackColor = Color.FromArgb(17, 70, 96);
            AlertBox7.CenterText = false;
            AlertBox7.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox7.Font = new Font("Segoe UI", 9.0f);
            AlertBox7.Image = (Image)resources.GetObject("AlertBox7.Image");
            AlertBox7.Location = new Point(47, 91);
            AlertBox7.Name = "AlertBox7";
            AlertBox7.Size = new Size(894, 48);
            AlertBox7.TabIndex = 222;
            AlertBox7.TabStop = false;
            AlertBox7.Text = resources.GetString("AlertBox7.Text");
            // 
            // Button19
            // 
            Button19.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button19.BackColor = Color.FromArgb(43, 43, 43);
            Button19.DrawOnGlass = false;
            Button19.Font = new Font("Segoe UI", 9.0f);
            Button19.ForeColor = Color.White;
            Button19.Image = (Image)resources.GetObject("Button19.Image");
            Button19.LineColor = Color.FromArgb(15, 74, 100);
            Button19.Location = new Point(867, 327);
            Button19.Name = "Button19";
            Button19.Size = new Size(34, 23);
            Button19.TabIndex = 221;
            Button19.UseVisualStyleBackColor = false;
            // 
            // Button18
            // 
            Button18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button18.BackColor = Color.FromArgb(43, 43, 43);
            Button18.DrawOnGlass = false;
            Button18.Font = new Font("Segoe UI", 9.0f);
            Button18.ForeColor = Color.White;
            Button18.Image = (Image)resources.GetObject("Button18.Image");
            Button18.LineColor = Color.FromArgb(15, 74, 100);
            Button18.Location = new Point(867, 297);
            Button18.Name = "Button18";
            Button18.Size = new Size(34, 23);
            Button18.TabIndex = 220;
            Button18.UseVisualStyleBackColor = false;
            // 
            // Button16
            // 
            Button16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button16.BackColor = Color.FromArgb(43, 43, 43);
            Button16.DrawOnGlass = false;
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = (Image)resources.GetObject("Button16.Image");
            Button16.LineColor = Color.FromArgb(15, 74, 100);
            Button16.Location = new Point(867, 267);
            Button16.Name = "Button16";
            Button16.Size = new Size(34, 23);
            Button16.TabIndex = 219;
            Button16.UseVisualStyleBackColor = false;
            // 
            // AlertBox8
            // 
            AlertBox8.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox8.BackColor = Color.FromArgb(101, 33, 38);
            AlertBox8.CenterText = false;
            AlertBox8.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox8.Font = new Font("Segoe UI", 9.0f);
            AlertBox8.Image = (Image)resources.GetObject("AlertBox8.Image");
            AlertBox8.Location = new Point(47, 145);
            AlertBox8.Name = "AlertBox8";
            AlertBox8.Size = new Size(894, 30);
            AlertBox8.TabIndex = 218;
            AlertBox8.TabStop = false;
            AlertBox8.Text = "You must be cautious in Windows 11 as changing \"Segoe UI\" font may cause instabil" + "ity in start menu and search (due to bug in Windows itself)";
            // 
            // TextBox3
            // 
            TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox3.BackColor = Color.FromArgb(55, 55, 55);
            TextBox3.DrawOnGlass = false;
            TextBox3.ForeColor = Color.White;
            TextBox3.Location = new Point(214, 326);
            TextBox3.MaxLength = 32767;
            TextBox3.Multiline = false;
            TextBox3.Name = "TextBox3";
            TextBox3.ReadOnly = false;
            TextBox3.Scrollbars = ScrollBars.None;
            TextBox3.SelectedText = "";
            TextBox3.SelectionLength = 0;
            TextBox3.SelectionStart = 0;
            TextBox3.Size = new Size(647, 24);
            TextBox3.TabIndex = 0;
            TextBox3.TextAlign = HorizontalAlignment.Left;
            TextBox3.UseSystemPasswordChar = false;
            TextBox3.WordWrap = true;
            // 
            // PictureBox37
            // 
            PictureBox37.Image = (Image)resources.GetObject("PictureBox37.Image");
            PictureBox37.Location = new Point(3, 3);
            PictureBox37.Name = "PictureBox37";
            PictureBox37.Size = new Size(35, 31);
            PictureBox37.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox37.TabIndex = 84;
            PictureBox37.TabStop = false;
            // 
            // Button17
            // 
            Button17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button17.BackColor = Color.FromArgb(43, 43, 43);
            Button17.DrawOnGlass = false;
            Button17.Font = new Font("Segoe UI", 9.0f);
            Button17.ForeColor = Color.White;
            Button17.Image = null;
            Button17.LineColor = Color.FromArgb(0, 81, 210);
            Button17.Location = new Point(907, 327);
            Button17.Name = "Button17";
            Button17.Size = new Size(34, 23);
            Button17.TabIndex = 211;
            Button17.Text = "...";
            Button17.UseVisualStyleBackColor = false;
            // 
            // AlertBox6
            // 
            AlertBox6.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox6.BackColor = Color.FromArgb(70, 51, 2);
            AlertBox6.CenterText = false;
            AlertBox6.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox6.Font = new Font("Segoe UI", 9.0f);
            AlertBox6.Image = (Image)resources.GetObject("AlertBox6.Image");
            AlertBox6.Location = new Point(47, 217);
            AlertBox6.Name = "AlertBox6";
            AlertBox6.Size = new Size(894, 30);
            AlertBox6.TabIndex = 216;
            AlertBox6.TabStop = false;
            AlertBox6.Text = "You must logoff and logon to apply the effect of substitution";
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(214, 266);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(647, 24);
            TextBox1.TabIndex = 0;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // PictureBox45
            // 
            PictureBox45.Image = (Image)resources.GetObject("PictureBox45.Image");
            PictureBox45.Location = new Point(47, 326);
            PictureBox45.Name = "PictureBox45";
            PictureBox45.Size = new Size(24, 24);
            PictureBox45.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox45.TabIndex = 205;
            PictureBox45.TabStop = false;
            // 
            // PictureBox46
            // 
            PictureBox46.Image = (Image)resources.GetObject("PictureBox46.Image");
            PictureBox46.Location = new Point(184, 326);
            PictureBox46.Name = "PictureBox46";
            PictureBox46.Size = new Size(24, 24);
            PictureBox46.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox46.TabIndex = 209;
            PictureBox46.TabStop = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(47, 253);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(894, 1);
            Separator1.TabIndex = 213;
            Separator1.TabStop = false;
            // 
            // Label28
            // 
            Label28.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label28.Location = new Point(77, 326);
            Label28.Name = "Label28";
            Label28.Size = new Size(101, 24);
            Label28.TabIndex = 206;
            Label28.Text = "Segoe UI";
            Label28.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox2
            // 
            TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(214, 296);
            TextBox2.MaxLength = 32767;
            TextBox2.Multiline = false;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = false;
            TextBox2.Scrollbars = ScrollBars.None;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(647, 24);
            TextBox2.TabIndex = 1;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // Button15
            // 
            Button15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button15.BackColor = Color.FromArgb(43, 43, 43);
            Button15.DrawOnGlass = false;
            Button15.Font = new Font("Segoe UI", 9.0f);
            Button15.ForeColor = Color.White;
            Button15.Image = null;
            Button15.LineColor = Color.FromArgb(0, 81, 210);
            Button15.Location = new Point(907, 297);
            Button15.Name = "Button15";
            Button15.Size = new Size(34, 23);
            Button15.TabIndex = 212;
            Button15.Text = "...";
            Button15.UseVisualStyleBackColor = false;
            // 
            // Label15
            // 
            Label15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label15.BackColor = Color.Transparent;
            Label15.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label15.Location = new Point(44, 3);
            Label15.Name = "Label15";
            Label15.Size = new Size(900, 31);
            Label15.TabIndex = 85;
            Label15.Text = "Fonts substitutes:";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(43, 43, 43);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = null;
            Button14.LineColor = Color.FromArgb(0, 81, 210);
            Button14.Location = new Point(907, 267);
            Button14.Name = "Button14";
            Button14.Size = new Size(34, 23);
            Button14.TabIndex = 211;
            Button14.Text = "...";
            Button14.UseVisualStyleBackColor = false;
            // 
            // AlertBox5
            // 
            AlertBox5.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox5.BackColor = Color.FromArgb(17, 70, 96);
            AlertBox5.CenterText = false;
            AlertBox5.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox5.Font = new Font("Segoe UI", 9.0f);
            AlertBox5.Image = (Image)resources.GetObject("AlertBox5.Image");
            AlertBox5.Location = new Point(47, 37);
            AlertBox5.Name = "AlertBox5";
            AlertBox5.Size = new Size(894, 48);
            AlertBox5.TabIndex = 204;
            AlertBox5.TabStop = false;
            AlertBox5.Text = resources.GetString("AlertBox5.Text");
            // 
            // PictureBox42
            // 
            PictureBox42.Image = (Image)resources.GetObject("PictureBox42.Image");
            PictureBox42.Location = new Point(184, 296);
            PictureBox42.Name = "PictureBox42";
            PictureBox42.Size = new Size(24, 24);
            PictureBox42.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox42.TabIndex = 210;
            PictureBox42.TabStop = false;
            // 
            // PictureBox38
            // 
            PictureBox38.Image = (Image)resources.GetObject("PictureBox38.Image");
            PictureBox38.Location = new Point(47, 266);
            PictureBox38.Name = "PictureBox38";
            PictureBox38.Size = new Size(24, 24);
            PictureBox38.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox38.TabIndex = 205;
            PictureBox38.TabStop = false;
            // 
            // PictureBox40
            // 
            PictureBox40.Image = (Image)resources.GetObject("PictureBox40.Image");
            PictureBox40.Location = new Point(184, 266);
            PictureBox40.Name = "PictureBox40";
            PictureBox40.Size = new Size(24, 24);
            PictureBox40.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox40.TabIndex = 209;
            PictureBox40.TabStop = false;
            // 
            // Label23
            // 
            Label23.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label23.Location = new Point(77, 266);
            Label23.Name = "Label23";
            Label23.Size = new Size(101, 24);
            Label23.TabIndex = 206;
            Label23.Text = "MS Shell Dlg";
            Label23.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label25
            // 
            Label25.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label25.Location = new Point(77, 296);
            Label25.Name = "Label25";
            Label25.Size = new Size(101, 24);
            Label25.TabIndex = 208;
            Label25.Text = "MS Shell Dlg 2";
            Label25.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox39
            // 
            PictureBox39.Image = (Image)resources.GetObject("PictureBox39.Image");
            PictureBox39.Location = new Point(47, 296);
            PictureBox39.Name = "PictureBox39";
            PictureBox39.Size = new Size(24, 24);
            PictureBox39.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox39.TabIndex = 207;
            PictureBox39.TabStop = false;
            // 
            // Button10
            // 
            Button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button10.BackColor = Color.FromArgb(34, 34, 34);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = (Image)resources.GetObject("Button10.Image");
            Button10.ImageAlign = ContentAlignment.MiddleLeft;
            Button10.LineColor = Color.FromArgb(36, 81, 110);
            Button10.Location = new Point(789, 539);
            Button10.Name = "Button10";
            Button10.Size = new Size(115, 34);
            Button10.TabIndex = 84;
            Button10.Text = "Quick apply";
            Button10.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.LineColor = Color.FromArgb(199, 49, 61);
            Button7.Location = new Point(703, 539);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 83;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button8.BackColor = Color.FromArgb(34, 34, 34);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleLeft;
            Button8.LineColor = Color.FromArgb(52, 20, 64);
            Button8.Location = new Point(910, 539);
            Button8.Name = "Button8";
            Button8.Size = new Size(180, 34);
            Button8.TabIndex = 82;
            Button8.Text = "Load into current theme";
            Button8.UseVisualStyleBackColor = false;
            // 
            // Metrics_Fonts
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(1102, 581);
            Controls.Add(AlertBox10);
            Controls.Add(GroupBox12);
            Controls.Add(TabControl1);
            Controls.Add(Button10);
            Controls.Add(Button7);
            Controls.Add(Button8);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MenuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Metrics_Fonts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Metrics & Fonts";
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checker_img).EndInit();
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            previewContainer.ResumeLayout(false);
            tabs_preview_1.ResumeLayout(false);
            TabPage6.ResumeLayout(false);
            pnl_preview1.ResumeLayout(false);
            TabPage7.ResumeLayout(false);
            Classic_Preview1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            TabPage2.ResumeLayout(false);
            GroupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox32).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).EndInit();
            GroupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            pnl_preview2.ResumeLayout(false);
            GroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox21).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox20).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            TabPage3.ResumeLayout(false);
            GroupBox14.ResumeLayout(false);
            tabs_preview_2.ResumeLayout(false);
            TabPage4.ResumeLayout(false);
            pnl_preview3.ResumeLayout(false);
            Window4.ResumeLayout(false);
            MenuStrip1.ResumeLayout(false);
            MenuStrip1.PerformLayout();
            TabPage8.ResumeLayout(false);
            Classic_Preview3.ResumeLayout(false);
            WindowR3.ResumeLayout(false);
            MenuStrip2.ResumeLayout(false);
            MenuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox33).EndInit();
            GroupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox18).EndInit();
            GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox19).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).EndInit();
            TabPage5.ResumeLayout(false);
            GroupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox31).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox44).EndInit();
            GroupBox15.ResumeLayout(false);
            tabs_preview_3.ResumeLayout(false);
            TabPage9.ResumeLayout(false);
            pnl_preview4.ResumeLayout(false);
            Window6.ResumeLayout(false);
            Panel2.ResumeLayout(false);
            Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox35).EndInit();
            StatusStrip1.ResumeLayout(false);
            StatusStrip1.PerformLayout();
            TabPage10.ResumeLayout(false);
            Classic_Preview4.ResumeLayout(false);
            WindowR5.ResumeLayout(false);
            Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox36).EndInit();
            ScrollBarR1.ResumeLayout(false);
            ScrollBarR2.ResumeLayout(false);
            PanelR1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox34).EndInit();
            GroupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox27).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).EndInit();
            GroupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox24).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).EndInit();
            TabPage11.ResumeLayout(false);
            GroupBox16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox37).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox45).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox46).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox42).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox38).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox39).EndInit();
            Load += new EventHandler(EditFonts_Load);
            FormClosed += new FormClosedEventHandler(Metrics_Fonts_FormClosed);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Metrics_Fonts_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal Label Label5;
        internal Label Label6;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal FontDialog FontDialog1;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal Label Label7;
        internal Label Label8;
        internal Label Label10;
        internal UI.WP.Trackbar Trackbar1;
        internal UI.WP.Trackbar Trackbar2;
        internal UI.WP.Trackbar Trackbar3;
        internal UI.WP.Trackbar Trackbar4;
        internal UI.WP.Trackbar Trackbar6;
        internal UI.WP.Trackbar Trackbar8;
        internal UI.WP.Trackbar Trackbar9;
        internal UI.WP.Trackbar Trackbar10;
        internal UI.WP.Trackbar Trackbar11;
        internal UI.WP.Trackbar Trackbar12;
        internal UI.WP.Trackbar Trackbar13;
        internal UI.WP.Trackbar Trackbar14;
        internal UI.WP.GroupBox previewContainer;
        internal Panel pnl_preview1;
        internal UI.Simulation.Window Window1;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage5;
        internal UI.WP.Trackbar Trackbar7;
        internal UI.Simulation.Window Window2;
        internal UI.WP.GroupBox GroupBox4;
        internal PictureBox PictureBox9;
        internal PictureBox PictureBox7;
        internal Label Label39;
        internal Label Label38;
        internal PictureBox PictureBox3;
        internal Label Label59;
        internal UI.WP.GroupBox GroupBox2;
        internal PictureBox PictureBox8;
        internal PictureBox PictureBox10;
        internal PictureBox PictureBox11;
        internal Label Label44;
        internal Label Label45;
        internal Label Label48;
        internal UI.WP.GroupBox GroupBox1;
        internal Label Label40;
        internal PictureBox PictureBox6;
        internal PictureBox PictureBox5;
        internal PictureBox PictureBox4;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox1;
        internal Label Label43;
        internal UI.WP.GroupBox GroupBox6;
        internal PictureBox PictureBox20;
        internal Label Label37;
        internal PictureBox PictureBox14;
        internal Label Label16;
        internal UI.WP.GroupBox GroupBox3;
        internal PictureBox PictureBox13;
        internal PictureBox PictureBox15;
        internal PictureBox PictureBox16;
        internal Label Label41;
        internal Label Label42;
        internal Label Label49;
        internal UI.WP.GroupBox GroupBox7;
        internal PictureBox PictureBox18;
        internal Label Label17;
        internal UI.WP.GroupBox GroupBox8;
        internal PictureBox PictureBox19;
        internal PictureBox PictureBox22;
        internal PictureBox PictureBox23;
        internal Label Label21;
        internal Label Label46;
        internal Label Label47;
        internal UI.WP.GroupBox GroupBox10;
        internal PictureBox PictureBox27;
        internal PictureBox PictureBox28;
        internal PictureBox PictureBox29;
        internal Label Label51;
        internal Label Label53;
        internal Label Label54;
        internal UI.WP.GroupBox GroupBox9;
        internal PictureBox PictureBox24;
        internal PictureBox PictureBox25;
        internal Label Label11;
        internal Label Label18;
        internal PictureBox PictureBox26;
        internal Label Label50;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle MetricsEnabled;
        internal PictureBox checker_img;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal UI.Retro.WindowR WindowR1;
        internal UI.WP.GroupBox GroupBox5;
        internal PictureBox PictureBox12;
        internal UI.WP.Trackbar Trackbar5;
        internal Label Label22;
        internal PictureBox PictureBox21;
        internal Label Label9;
        internal UI.WP.GroupBox GroupBox13;
        internal PictureBox PictureBox17;
        internal Label Label24;
        internal Panel pnl_preview2;
        internal UI.Simulation.WinIcon FakeIcon1;
        internal UI.Simulation.WinIcon FakeIcon2;
        internal UI.Simulation.WinIcon FakeIcon3;
        internal UI.WP.GroupBox GroupBox14;
        internal PictureBox PictureBox33;
        internal Label Label27;
        internal Panel pnl_preview3;
        internal UI.Simulation.Window Window4;
        internal UI.WP.GroupBox GroupBox15;
        internal PictureBox PictureBox34;
        internal Label Label36;
        internal Panel pnl_preview4;
        internal UI.Simulation.Window Window6;
        internal Panel Panel2;
        internal UI.WP.LabelAlt msgLbl;
        internal PictureBox PictureBox35;
        internal MenuStrip MenuStrip1;
        internal UI.WP.ToolStripMenuItem MenuParentToolStripMenuItem;
        internal UI.WP.ToolStripMenuItem MenuParent2ToolStripMenuItem;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button ttl_h;
        internal UI.WP.Button ttl_w;
        internal UI.WP.Button ttl_p;
        internal UI.WP.Button ttl_b;
        internal UI.WP.Button tw_w;
        internal UI.WP.Button tw_h;
        internal UI.WP.Button i_s_s;
        internal UI.WP.Button i_d_s;
        internal UI.WP.Button i_s_h;
        internal UI.WP.Button i_s_v;
        internal UI.WP.Button m_w;
        internal UI.WP.Button m_h;
        internal UI.WP.Button s_w;
        internal UI.WP.Button s_h;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.AlertBox AlertBox2;
        internal UI.WP.Button Button13;
        internal UI.WP.AlertBox AlertBox3;
        internal UI.WP.AlertBox AlertBox4;
        internal UI.WP.TablessControl tabs_preview_1;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal Panel Classic_Preview1;
        internal UI.Retro.WindowR WindowR2;
        internal UI.WP.TablessControl tabs_preview_2;
        internal TabPage TabPage4;
        internal TabPage TabPage8;
        internal Panel Classic_Preview3;
        internal UI.Retro.WindowR WindowR3;
        internal UI.WP.TablessControl tabs_preview_3;
        internal TabPage TabPage9;
        internal TabPage TabPage10;
        internal Panel Classic_Preview4;
        internal UI.Retro.WindowR WindowR5;
        internal Panel Panel3;
        internal UI.WP.LabelAlt Label13;
        internal PictureBox PictureBox36;
        internal HScrollBar HScrollBar1;
        internal VScrollBar VScrollBar1;
        internal StatusStrip StatusStrip1;
        internal UI.WP.ToolStripStatusLabel statusLbl;
        internal UI.Retro.ScrollBarR ScrollBarR2;
        internal UI.Retro.ButtonR ButtonR12;
        internal UI.Retro.ButtonR ButtonR11;
        internal UI.Retro.ButtonR ButtonR10;
        internal UI.Retro.ScrollBarR ScrollBarR1;
        internal UI.Retro.ButtonR ButtonR3;
        internal UI.Retro.ButtonR ButtonR1;
        internal UI.Retro.ButtonR ButtonR2;
        internal UI.Retro.PanelR PanelR1;
        internal UI.WP.LabelAlt Label14;
        internal Panel Panel1;
        internal MenuStrip MenuStrip2;
        internal UI.WP.ToolStripMenuItem ToolStripMenuItem1;
        internal UI.WP.ToolStripMenuItem ToolStripMenuItem4;
        internal TabPage TabPage11;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.TextBox TextBox1;
        internal Label Label15;
        internal PictureBox PictureBox37;
        internal UI.WP.AlertBox AlertBox5;
        internal PictureBox PictureBox42;
        internal PictureBox PictureBox40;
        internal Label Label25;
        internal PictureBox PictureBox39;
        internal Label Label23;
        internal PictureBox PictureBox38;
        internal UI.WP.Button Button15;
        internal UI.WP.Button Button14;
        internal FontDialog FontDialog2;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.AlertBox AlertBox6;
        internal UI.WP.GroupBox GroupBox16;
        internal UI.WP.TextBox TextBox3;
        internal UI.WP.Button Button17;
        internal PictureBox PictureBox45;
        internal PictureBox PictureBox46;
        internal Label Label28;
        internal UI.WP.AlertBox AlertBox8;
        internal UI.WP.Button Button19;
        internal UI.WP.Button Button18;
        internal UI.WP.Button Button16;
        internal UI.Retro.PanelR PanelR2;
        internal UI.WP.AlertBox AlertBox9;
        internal UI.WP.AlertBox AlertBox7;
        internal UI.WP.Button Button20;
        internal UI.WP.AlertBox AlertBox10;
        internal UI.WP.AlertBox AlertBox11;
        internal UI.WP.AlertBox AlertBox12;
        internal UI.WP.AlertBox AlertBox13;
        internal UI.WP.Button i_s_s_s;
        internal PictureBox PictureBox30;
        internal UI.WP.Trackbar Trackbar15;
        internal Label Label20;
        internal UI.WP.GroupBox GroupBox11;
        internal PictureBox PictureBox32;
        internal Label Label29;
        internal UI.WP.GroupBox GroupBox17;
        internal UI.WP.CheckBox CheckBox1;
        internal PictureBox PictureBox31;
        internal PictureBox PictureBox44;
        internal Label Label26;
        internal UI.WP.AlertBox AlertBox14;
    }
}