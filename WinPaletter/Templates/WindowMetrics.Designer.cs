namespace WinPaletter.Templates
{
    partial class WindowMetrics
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs_preview = new WinPaletter.UI.WP.TablessControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.WXP_Alert = new WinPaletter.UI.WP.AlertBox();
            this.Window2 = new WinPaletter.UI.Simulation.Window();
            this.Window1 = new WinPaletter.UI.Simulation.Window();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.windowR1 = new WinPaletter.UI.Retro.WindowR();
            this.windowR2 = new WinPaletter.UI.Retro.WindowR();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Window3 = new WinPaletter.UI.Simulation.Window();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.msgLbl = new WinPaletter.UI.WP.LabelAlt();
            this.VScrollBar1 = new WinPaletter.UI.WP.ResizableVScrollBar();
            this.HScrollBar1 = new WinPaletter.UI.WP.ResizableHScrollBar();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new WinPaletter.UI.WP.ToolStripMenuItem();
            this.toolStripMenuItem2 = new WinPaletter.UI.WP.ToolStripMenuItem();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new WinPaletter.UI.WP.ToolStripStatusLabel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.windowR3 = new WinPaletter.UI.Retro.WindowR();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl = new WinPaletter.UI.Retro.LabelR();
            this.scrollBarR2 = new WinPaletter.UI.Retro.ScrollBarR();
            this.scrollBarR1 = new WinPaletter.UI.Retro.ScrollBarR();
            this.menuBarR1 = new WinPaletter.UI.Retro.MenuBarR();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PanelR1 = new WinPaletter.UI.Retro.PanelR();
            this.status = new WinPaletter.UI.Retro.LabelR();
            this.tabs_preview.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.Window3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.MenuStrip1.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.windowR3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PanelR1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs_preview
            // 
            this.tabs_preview.Controls.Add(this.tabPage1);
            this.tabs_preview.Controls.Add(this.tabPage2);
            this.tabs_preview.Controls.Add(this.tabPage3);
            this.tabs_preview.Controls.Add(this.tabPage4);
            this.tabs_preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs_preview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabs_preview.Location = new System.Drawing.Point(0, 0);
            this.tabs_preview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabs_preview.Name = "tabs_preview";
            this.tabs_preview.SelectedIndex = 0;
            this.tabs_preview.Size = new System.Drawing.Size(503, 353);
            this.tabs_preview.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.WXP_Alert);
            this.tabPage1.Controls.Add(this.Window2);
            this.tabPage1.Controls.Add(this.Window1);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Size = new System.Drawing.Size(495, 325);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "0";
            // 
            // WXP_Alert
            // 
            this.WXP_Alert.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.WXP_Alert.BackColor = System.Drawing.Color.Transparent;
            this.WXP_Alert.CenterText = true;
            this.WXP_Alert.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.WXP_Alert.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Alert.Image = null;
            this.WXP_Alert.Location = new System.Drawing.Point(7, 7);
            this.WXP_Alert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.WXP_Alert.Name = "WXP_Alert";
            this.WXP_Alert.Size = new System.Drawing.Size(62, 46);
            this.WXP_Alert.TabIndex = 67;
            this.WXP_Alert.TabStop = false;
            this.WXP_Alert.Text = "The classic theme is enabled. The preview won\'t work for other themes due to limi" +
    "tations in the visual styles previewer. Please apply another theme first, and th" +
    "en switch back to this theme.";
            this.WXP_Alert.Visible = false;
            // 
            // Window2
            // 
            this.Window2.AccentColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window2.AccentColor_Enabled = true;
            this.Window2.AccentColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window2.AccentColor2_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window2.AccentColor2_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window2.Active = false;
            this.Window2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Window2.BackColor = System.Drawing.Color.Transparent;
            this.Window2.DarkMode = true;
            this.Window2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Window2.Location = new System.Drawing.Point(308, 44);
            this.Window2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Window2.Metrics_BorderWidth = 1;
            this.Window2.Metrics_CaptionHeight = 22;
            this.Window2.Metrics_PaddedBorderWidth = 4;
            this.Window2.Name = "Window2";
            this.Window2.Padding = new System.Windows.Forms.Padding(11, 40, 11, 11);
            this.Window2.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.Window2.Radius = 3;
            this.Window2.Shadow = false;
            this.Window2.Size = new System.Drawing.Size(139, 233);
            this.Window2.TabIndex = 9;
            this.Window2.Text = "Tool";
            this.Window2.ToolWindow = true;
            this.Window2.Win7Alpha = 100;
            this.Window2.Win7ColorBal = 100;
            this.Window2.Win7GlowBal = 100;
            this.Window2.Win7Noise = 1F;
            this.Window2.WinVista = false;
            this.Window2.EditorInvoker += new WinPaletter.UI.Simulation.Window.EditorInvokerEventHandler(this.windowR2_EditorInvoker);
            // 
            // Window1
            // 
            this.Window1.AccentColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window1.AccentColor_Enabled = true;
            this.Window1.AccentColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window1.AccentColor2_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window1.AccentColor2_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window1.Active = true;
            this.Window1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Window1.BackColor = System.Drawing.Color.Transparent;
            this.Window1.DarkMode = true;
            this.Window1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Window1.Location = new System.Drawing.Point(46, 44);
            this.Window1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Window1.Metrics_BorderWidth = 1;
            this.Window1.Metrics_CaptionHeight = 22;
            this.Window1.Metrics_PaddedBorderWidth = 4;
            this.Window1.Name = "Window1";
            this.Window1.Padding = new System.Windows.Forms.Padding(11, 40, 11, 11);
            this.Window1.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.Window1.Radius = 5;
            this.Window1.Shadow = true;
            this.Window1.Size = new System.Drawing.Size(255, 233);
            this.Window1.TabIndex = 8;
            this.Window1.Text = "Application";
            this.Window1.ToolWindow = false;
            this.Window1.Win7Alpha = 100;
            this.Window1.Win7ColorBal = 100;
            this.Window1.Win7GlowBal = 100;
            this.Window1.Win7Noise = 1F;
            this.Window1.WinVista = false;
            this.Window1.EditorInvoker += new WinPaletter.UI.Simulation.Window.EditorInvokerEventHandler(this.windowR1_EditorInvoker);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.windowR1);
            this.tabPage2.Controls.Add(this.windowR2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Size = new System.Drawing.Size(495, 327);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "1";
            // 
            // windowR1
            // 
            this.windowR1.Active = true;
            this.windowR1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.windowR1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.windowR1.ButtonDkShadow = System.Drawing.SystemColors.ControlDark;
            this.windowR1.ButtonHilight = System.Drawing.SystemColors.ButtonHighlight;
            this.windowR1.ButtonLight = System.Drawing.SystemColors.ControlLight;
            this.windowR1.ButtonShadow = System.Drawing.SystemColors.ButtonShadow;
            this.windowR1.ButtonText = System.Drawing.SystemColors.ControlText;
            this.windowR1.Color1 = System.Drawing.SystemColors.ActiveCaption;
            this.windowR1.Color2 = System.Drawing.SystemColors.GradientActiveCaption;
            this.windowR1.ColorBorder = System.Drawing.SystemColors.ActiveBorder;
            this.windowR1.ColorGradient = true;
            this.windowR1.ControlBox = true;
            this.windowR1.Flat = false;
            this.windowR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.windowR1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.windowR1.Location = new System.Drawing.Point(56, 54);
            this.windowR1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.windowR1.MaximizeBox = true;
            this.windowR1.Metrics_BorderWidth = 1;
            this.windowR1.Metrics_CaptionHeight = 22;
            this.windowR1.Metrics_CaptionWidth = 22;
            this.windowR1.Metrics_PaddedBorderWidth = 4;
            this.windowR1.MinimizeBox = true;
            this.windowR1.Name = "windowR1";
            this.windowR1.Padding = new System.Windows.Forms.Padding(8, 31, 8, 8);
            this.windowR1.Size = new System.Drawing.Size(237, 215);
            this.windowR1.TabIndex = 0;
            this.windowR1.Text = "Application";
            this.windowR1.EditorInvoker += new WinPaletter.UI.Retro.WindowR.EditorInvokerEventHandler(this.windowR1_EditorInvoker);
            // 
            // windowR2
            // 
            this.windowR2.Active = true;
            this.windowR2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.windowR2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.windowR2.ButtonDkShadow = System.Drawing.SystemColors.ControlDark;
            this.windowR2.ButtonHilight = System.Drawing.SystemColors.ButtonHighlight;
            this.windowR2.ButtonLight = System.Drawing.SystemColors.ControlLight;
            this.windowR2.ButtonShadow = System.Drawing.SystemColors.ButtonShadow;
            this.windowR2.ButtonText = System.Drawing.SystemColors.ControlText;
            this.windowR2.Color1 = System.Drawing.SystemColors.ActiveCaption;
            this.windowR2.Color2 = System.Drawing.SystemColors.GradientActiveCaption;
            this.windowR2.ColorBorder = System.Drawing.SystemColors.ActiveBorder;
            this.windowR2.ColorGradient = true;
            this.windowR2.ControlBox = true;
            this.windowR2.Flat = false;
            this.windowR2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.windowR2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.windowR2.Location = new System.Drawing.Point(316, 54);
            this.windowR2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.windowR2.MaximizeBox = false;
            this.windowR2.Metrics_BorderWidth = 1;
            this.windowR2.Metrics_CaptionHeight = 22;
            this.windowR2.Metrics_CaptionWidth = 22;
            this.windowR2.Metrics_PaddedBorderWidth = 4;
            this.windowR2.MinimizeBox = false;
            this.windowR2.Name = "windowR2";
            this.windowR2.Padding = new System.Windows.Forms.Padding(8, 31, 8, 8);
            this.windowR2.Size = new System.Drawing.Size(120, 215);
            this.windowR2.TabIndex = 1;
            this.windowR2.Text = "Tool";
            this.windowR2.EditorInvoker += new WinPaletter.UI.Retro.WindowR.EditorInvokerEventHandler(this.windowR2_EditorInvoker);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Black;
            this.tabPage3.Controls.Add(this.Window3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage3.Size = new System.Drawing.Size(495, 327);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "2";
            // 
            // Window3
            // 
            this.Window3.AccentColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window3.AccentColor_Enabled = true;
            this.Window3.AccentColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window3.AccentColor2_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window3.AccentColor2_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window3.Active = true;
            this.Window3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Window3.BackColor = System.Drawing.Color.Transparent;
            this.Window3.Controls.Add(this.panel1);
            this.Window3.Controls.Add(this.VScrollBar1);
            this.Window3.Controls.Add(this.HScrollBar1);
            this.Window3.Controls.Add(this.MenuStrip1);
            this.Window3.Controls.Add(this.StatusStrip1);
            this.Window3.DarkMode = true;
            this.Window3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Window3.Location = new System.Drawing.Point(7, 45);
            this.Window3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Window3.Metrics_BorderWidth = 1;
            this.Window3.Metrics_CaptionHeight = 22;
            this.Window3.Metrics_PaddedBorderWidth = 4;
            this.Window3.Name = "Window3";
            this.Window3.Padding = new System.Windows.Forms.Padding(11, 40, 11, 11);
            this.Window3.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.Window3.Radius = 5;
            this.Window3.Shadow = true;
            this.Window3.Size = new System.Drawing.Size(479, 233);
            this.Window3.TabIndex = 3;
            this.Window3.Text = "Application";
            this.Window3.ToolWindow = false;
            this.Window3.Win7Alpha = 100;
            this.Window3.Win7ColorBal = 100;
            this.Window3.Win7GlowBal = 100;
            this.Window3.Win7Noise = 1F;
            this.Window3.WinVista = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pic);
            this.panel1.Controls.Add(this.msgLbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(11, 62);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 121);
            this.panel1.TabIndex = 204;
            // 
            // pic
            // 
            this.pic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pic.Location = new System.Drawing.Point(18, 34);
            this.pic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(56, 55);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic.TabIndex = 86;
            this.pic.TabStop = false;
            // 
            // msgLbl
            // 
            this.msgLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.msgLbl.BackColor = System.Drawing.Color.Transparent;
            this.msgLbl.DrawOnGlass = false;
            this.msgLbl.ForeColor = System.Drawing.Color.White;
            this.msgLbl.Location = new System.Drawing.Point(80, 32);
            this.msgLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(343, 57);
            this.msgLbl.TabIndex = 87;
            this.msgLbl.Text = "This is a text displayed as a message";
            this.msgLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.msgLbl.Click += new System.EventHandler(this.msgLbl_Click);
            // 
            // VScrollBar1
            // 
            this.VScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScrollBar1.Location = new System.Drawing.Point(451, 62);
            this.VScrollBar1.Name = "VScrollBar1";
            this.VScrollBar1.ResizeEnabled = true;
            this.VScrollBar1.Size = new System.Drawing.Size(17, 121);
            this.VScrollBar1.TabIndex = 206;
            this.VScrollBar1.SizeChanged += new System.EventHandler(this.VScrollBar1_SizeChanged);
            // 
            // HScrollBar1
            // 
            this.HScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HScrollBar1.Location = new System.Drawing.Point(11, 183);
            this.HScrollBar1.Name = "HScrollBar1";
            this.HScrollBar1.ResizeEnabled = true;
            this.HScrollBar1.Size = new System.Drawing.Size(457, 17);
            this.HScrollBar1.TabIndex = 207;
            this.HScrollBar1.SizeChanged += new System.EventHandler(this.VScrollBar1_SizeChanged);
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.AutoSize = false;
            this.MenuStrip1.BackColor = System.Drawing.Color.White;
            this.MenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.MenuStrip1.Location = new System.Drawing.Point(11, 40);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MenuStrip1.Size = new System.Drawing.Size(457, 22);
            this.MenuStrip1.TabIndex = 1;
            this.MenuStrip1.Text = "MenuStrip1";
            this.MenuStrip1.SizeChanged += new System.EventHandler(this.MenuStrip1_SizeChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(59, 22);
            this.toolStripMenuItem1.Text = "Normal";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(64, 22);
            this.toolStripMenuItem2.Text = "Disabled";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.StatusStrip1.Location = new System.Drawing.Point(11, 200);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusStrip1.Size = new System.Drawing.Size(457, 22);
            this.StatusStrip1.TabIndex = 205;
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(39, 17);
            this.statusLbl.Text = "Status";
            this.statusLbl.Click += new System.EventHandler(this.statusLbl_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Black;
            this.tabPage4.Controls.Add(this.windowR3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage4.Size = new System.Drawing.Size(495, 327);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "3";
            // 
            // windowR3
            // 
            this.windowR3.Active = true;
            this.windowR3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.windowR3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.windowR3.ButtonDkShadow = System.Drawing.Color.Black;
            this.windowR3.ButtonHilight = System.Drawing.Color.White;
            this.windowR3.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.windowR3.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.windowR3.ButtonText = System.Drawing.Color.Black;
            this.windowR3.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.windowR3.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(132)))), ((int)(((byte)(208)))));
            this.windowR3.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.windowR3.ColorGradient = true;
            this.windowR3.ControlBox = true;
            this.windowR3.Controls.Add(this.panel2);
            this.windowR3.Controls.Add(this.scrollBarR2);
            this.windowR3.Controls.Add(this.scrollBarR1);
            this.windowR3.Controls.Add(this.menuBarR1);
            this.windowR3.Controls.Add(this.panel3);
            this.windowR3.Controls.Add(this.PanelR1);
            this.windowR3.Flat = false;
            this.windowR3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.windowR3.ForeColor = System.Drawing.Color.White;
            this.windowR3.Location = new System.Drawing.Point(16, 54);
            this.windowR3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.windowR3.MaximizeBox = false;
            this.windowR3.Metrics_BorderWidth = 1;
            this.windowR3.Metrics_CaptionHeight = 22;
            this.windowR3.Metrics_CaptionWidth = 22;
            this.windowR3.Metrics_PaddedBorderWidth = 4;
            this.windowR3.MinimizeBox = false;
            this.windowR3.Name = "windowR3";
            this.windowR3.Padding = new System.Windows.Forms.Padding(8, 31, 8, 8);
            this.windowR3.Size = new System.Drawing.Size(461, 215);
            this.windowR3.TabIndex = 206;
            this.windowR3.Text = "Application";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(8, 51);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 110);
            this.panel2.TabIndex = 211;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Location = new System.Drawing.Point(18, 28);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 84;
            this.pictureBox1.TabStop = false;
            // 
            // lbl
            // 
            this.lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.ForeColor = System.Drawing.Color.Black;
            this.lbl.Location = new System.Drawing.Point(80, 27);
            this.lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(338, 57);
            this.lbl.TabIndex = 85;
            this.lbl.Text = "This is a text displayed as a message";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl.Click += new System.EventHandler(this.msgLbl_Click);
            // 
            // scrollBarR2
            // 
            this.scrollBarR2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.scrollBarR2.ButtonHilight = System.Drawing.Color.White;
            this.scrollBarR2.Dock = System.Windows.Forms.DockStyle.Right;
            this.scrollBarR2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.scrollBarR2.ForeColor = System.Drawing.Color.Black;
            this.scrollBarR2.Location = new System.Drawing.Point(434, 51);
            this.scrollBarR2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.scrollBarR2.Maximum = 100;
            this.scrollBarR2.Minimum = 0;
            this.scrollBarR2.Name = "scrollBarR2";
            this.scrollBarR2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.scrollBarR2.Size = new System.Drawing.Size(19, 110);
            this.scrollBarR2.TabIndex = 210;
            this.scrollBarR2.Value = 0;
            this.scrollBarR2.EditorInvoker += new WinPaletter.UI.Retro.ScrollBarR.EditorInvokerEventHandler(this.scrollBarR2_EditorInvoker);
            // 
            // scrollBarR1
            // 
            this.scrollBarR1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.scrollBarR1.ButtonHilight = System.Drawing.Color.White;
            this.scrollBarR1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrollBarR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.scrollBarR1.ForeColor = System.Drawing.Color.Black;
            this.scrollBarR1.Location = new System.Drawing.Point(8, 161);
            this.scrollBarR1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.scrollBarR1.Maximum = 100;
            this.scrollBarR1.Minimum = 0;
            this.scrollBarR1.Name = "scrollBarR1";
            this.scrollBarR1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.scrollBarR1.Size = new System.Drawing.Size(445, 18);
            this.scrollBarR1.TabIndex = 208;
            this.scrollBarR1.Value = 0;
            this.scrollBarR1.EditorInvoker += new WinPaletter.UI.Retro.ScrollBarR.EditorInvokerEventHandler(this.scrollBarR2_EditorInvoker);
            // 
            // menuBarR1
            // 
            this.menuBarR1.ButtonHilight = System.Drawing.SystemColors.ButtonHighlight;
            this.menuBarR1.ButtonShadow = System.Drawing.SystemColors.ButtonShadow;
            this.menuBarR1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuBarR1.Flat = false;
            this.menuBarR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.menuBarR1.GrayText = System.Drawing.SystemColors.GrayText;
            this.menuBarR1.Hilight = System.Drawing.SystemColors.Highlight;
            this.menuBarR1.HilightText = System.Drawing.SystemColors.HighlightText;
            this.menuBarR1.Location = new System.Drawing.Point(8, 31);
            this.menuBarR1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.menuBarR1.MenuBar = System.Drawing.SystemColors.MenuBar;
            this.menuBarR1.MenuHeight = 20;
            this.menuBarR1.MenuHilight = System.Drawing.SystemColors.MenuHighlight;
            this.menuBarR1.Name = "menuBarR1";
            this.menuBarR1.PreviewSelectionItem = false;
            this.menuBarR1.Size = new System.Drawing.Size(445, 20);
            this.menuBarR1.TabIndex = 206;
            this.menuBarR1.Visible = false;
            this.menuBarR1.EditorInvoker += new WinPaletter.UI.Retro.MenuBarR.EditorInvokerEventHandler(this.menuBarR1_EditorInvoker);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(8, 179);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(445, 5);
            this.panel3.TabIndex = 206;
            // 
            // PanelR1
            // 
            this.PanelR1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PanelR1.ButtonDkShadow = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.PanelR1.ButtonHilight = System.Drawing.Color.White;
            this.PanelR1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.PanelR1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.PanelR1.Controls.Add(this.status);
            this.PanelR1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelR1.Flat = false;
            this.PanelR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.PanelR1.ForeColor = System.Drawing.Color.Black;
            this.PanelR1.Location = new System.Drawing.Point(8, 184);
            this.PanelR1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelR1.Name = "PanelR1";
            this.PanelR1.Size = new System.Drawing.Size(445, 23);
            this.PanelR1.Style2 = false;
            this.PanelR1.TabIndex = 95;
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.Transparent;
            this.status.Dock = System.Windows.Forms.DockStyle.Top;
            this.status.ForeColor = System.Drawing.Color.Black;
            this.status.Location = new System.Drawing.Point(0, 0);
            this.status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(445, 23);
            this.status.TabIndex = 86;
            this.status.Text = "Status";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.status.Click += new System.EventHandler(this.statusLbl_Click);
            // 
            // WindowMetrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.tabs_preview);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WindowMetrics";
            this.Size = new System.Drawing.Size(503, 353);
            this.Load += new System.EventHandler(this.WindowMetrics_Load);
            this.BackgroundImageChanged += new System.EventHandler(this.WindowMetrics_BackgroundImageChanged);
            this.tabs_preview.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.Window3.ResumeLayout(false);
            this.Window3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.windowR3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.PanelR1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Retro.WindowR windowR1;
        private UI.Retro.WindowR windowR2;
        private UI.WP.TablessControl tabs_preview;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        internal UI.Simulation.Window Window2;
        internal UI.Simulation.Window Window1;
        internal UI.WP.AlertBox WXP_Alert;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        internal UI.Simulation.Window Window3;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.PictureBox pic;
        internal UI.WP.LabelAlt msgLbl;
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        public System.Windows.Forms.StatusStrip StatusStrip1;
        internal UI.Retro.WindowR windowR3;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.PictureBox pictureBox1;
        internal UI.Retro.LabelR lbl;
        internal UI.Retro.ScrollBarR scrollBarR2;
        internal UI.Retro.ScrollBarR scrollBarR1;
        public UI.Retro.MenuBarR menuBarR1;
        private System.Windows.Forms.Panel panel3;
        internal UI.Retro.PanelR PanelR1;
        internal UI.Retro.LabelR status;
        public UI.WP.ToolStripStatusLabel statusLbl;
        private UI.WP.ToolStripMenuItem toolStripMenuItem1;
        private UI.WP.ToolStripMenuItem toolStripMenuItem2;
        private UI.WP.ResizableVScrollBar VScrollBar1;
        private UI.WP.ResizableHScrollBar HScrollBar1;
    }
}
