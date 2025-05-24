namespace WinPaletter
{
    partial class BackupThemes_List
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupThemes_List));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.titlebarExtender1 = new WinPaletter.Tabs.TitlebarExtender();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pin_button = new WinPaletter.UI.WP.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.separatorV1 = new WinPaletter.UI.WP.SeparatorV();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Button20 = new WinPaletter.UI.WP.Button();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.button6 = new WinPaletter.UI.WP.Button();
            this.button5 = new WinPaletter.UI.WP.Button();
            this.button1 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.button4 = new WinPaletter.UI.WP.Button();
            this.button3 = new WinPaletter.UI.WP.Button();
            this.groupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.windowsDesktop1 = new WinPaletter.Templates.WindowsDesktop();
            this.titlebarExtender1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.bottom_buttons.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(13, 58);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(732, 525);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView1_DrawColumnHeader);
            this.listView1.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView1_DrawItem);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.SizeChanged += new System.EventHandler(this.listView1_SizeChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(755, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(534, 29);
            this.label1.TabIndex = 208;
            this.label1.Text = "Select an item to preview it";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.flowLayoutPanel1);
            this.titlebarExtender1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebarExtender1.Flag = WinPaletter.Tabs.TitlebarExtender.Flags.Tabs_Extended;
            this.titlebarExtender1.Location = new System.Drawing.Point(0, 0);
            this.titlebarExtender1.Name = "titlebarExtender1";
            this.titlebarExtender1.Size = new System.Drawing.Size(1298, 52);
            this.titlebarExtender1.TabIndex = 209;
            this.titlebarExtender1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.pin_button);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.separatorV1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.Button20);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1282, 40);
            this.flowLayoutPanel1.TabIndex = 124;
            // 
            // pin_button
            // 
            this.pin_button.CustomColor = System.Drawing.Color.Empty;
            this.pin_button.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.pin_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pin_button.ForeColor = System.Drawing.Color.White;
            this.pin_button.Image = ((System.Drawing.Image)(resources.GetObject("pin_button.Image")));
            this.pin_button.ImageGlyph = null;
            this.pin_button.ImageGlyphEnabled = false;
            this.pin_button.Location = new System.Drawing.Point(3, 3);
            this.pin_button.Name = "pin_button";
            this.pin_button.Size = new System.Drawing.Size(34, 34);
            this.pin_button.TabIndex = 125;
            this.pin_button.UseVisualStyleBackColor = false;
            this.pin_button.Visible = false;
            this.pin_button.Click += new System.EventHandler(this.pin_button_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(43, 11);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label4.Size = new System.Drawing.Size(14, 17);
            this.label4.TabIndex = 213;
            this.label4.Text = "0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // separatorV1
            // 
            this.separatorV1.AlternativeLook = false;
            this.separatorV1.BackColor = System.Drawing.Color.Transparent;
            this.separatorV1.Location = new System.Drawing.Point(63, 3);
            this.separatorV1.Name = "separatorV1";
            this.separatorV1.Size = new System.Drawing.Size(1, 34);
            this.separatorV1.TabIndex = 214;
            this.separatorV1.TabStop = false;
            this.separatorV1.Text = "separatorV1";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 11);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 209;
            this.label2.Text = "All backups size:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(172, 11);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 2, 5, 0);
            this.label3.Size = new System.Drawing.Size(18, 17);
            this.label3.TabIndex = 210;
            this.label3.Text = "0";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button20
            // 
            this.Button20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Button20.CustomColor = System.Drawing.Color.Empty;
            this.Button20.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button20.ForeColor = System.Drawing.Color.White;
            this.Button20.Image = null;
            this.Button20.ImageGlyph = null;
            this.Button20.ImageGlyphEnabled = false;
            this.Button20.Location = new System.Drawing.Point(196, 8);
            this.Button20.Name = "Button20";
            this.Button20.Size = new System.Drawing.Size(90, 24);
            this.Button20.TabIndex = 211;
            this.Button20.Text = "Delete all";
            this.Button20.UseVisualStyleBackColor = false;
            this.Button20.Click += new System.EventHandler(this.Button20_Click);
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.button6);
            this.bottom_buttons.Controls.Add(this.button5);
            this.bottom_buttons.Controls.Add(this.button1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.button4);
            this.bottom_buttons.Controls.Add(this.button3);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 589);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(1298, 48);
            this.bottom_buttons.TabIndex = 207;
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button6.CustomColor = System.Drawing.Color.Empty;
            this.button6.Enabled = false;
            this.button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageGlyph = null;
            this.button6.ImageGlyphEnabled = false;
            this.button6.Location = new System.Drawing.Point(976, 7);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(150, 34);
            this.button6.TabIndex = 213;
            this.button6.Text = "Restore and apply";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button5.CustomColor = System.Drawing.Color.Empty;
            this.button5.Enabled = false;
            this.button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageGlyph = null;
            this.button5.ImageGlyphEnabled = false;
            this.button5.Location = new System.Drawing.Point(548, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(160, 34);
            this.button5.TabIndex = 206;
            this.button5.Text = "Open in folder";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.CustomColor = System.Drawing.Color.Empty;
            this.button1.Enabled = false;
            this.button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.CustomColorOnHover)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageGlyph = null;
            this.button1.ImageGlyphEnabled = false;
            this.button1.Location = new System.Drawing.Point(1132, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 34);
            this.button1.TabIndex = 202;
            this.button1.Text = "Restore only";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(435, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(107, 34);
            this.Button2.TabIndex = 203;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button4.CustomColor = System.Drawing.Color.Empty;
            this.button4.Enabled = false;
            this.button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageGlyph = null;
            this.button4.ImageGlyphEnabled = false;
            this.button4.Location = new System.Drawing.Point(845, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 34);
            this.button4.TabIndex = 205;
            this.button4.Text = "Save as";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.CustomColor = System.Drawing.Color.Empty;
            this.button3.Enabled = false;
            this.button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageGlyph = null;
            this.button3.ImageGlyphEnabled = false;
            this.button3.Location = new System.Drawing.Point(714, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 34);
            this.button3.TabIndex = 204;
            this.button3.Text = "Delete";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox2.Controls.Add(this.windowsDesktop1);
            this.groupBox2.Location = new System.Drawing.Point(755, 58);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(534, 303);
            this.groupBox2.TabIndex = 206;
            // 
            // windowsDesktop1
            // 
            this.windowsDesktop1.AccentLevel = WinPaletter.Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            this.windowsDesktop1.ActiveBorder = System.Drawing.Color.Empty;
            this.windowsDesktop1.ActiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.AfterGlowColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.windowsDesktop1.AfterGlowColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.windowsDesktop1.BackColor = System.Drawing.SystemColors.Desktop;
            this.windowsDesktop1.Background = System.Drawing.Color.Empty;
            this.windowsDesktop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.windowsDesktop1.ButtonDkShadow = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonFace = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonHilight = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonLight = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonShadow = System.Drawing.Color.Empty;
            this.windowsDesktop1.ButtonText = System.Drawing.Color.Empty;
            this.windowsDesktop1.Classic = false;
            this.windowsDesktop1.Color1 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color2 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color3 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color4 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color5 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color6 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color7 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color8 = System.Drawing.Color.Empty;
            this.windowsDesktop1.Color9 = System.Drawing.Color.Empty;
            this.windowsDesktop1.DarkMode_App = true;
            this.windowsDesktop1.DarkMode_Win = true;
            this.windowsDesktop1.EnableEditingColors = false;
            this.windowsDesktop1.EnableGradient = true;
            this.windowsDesktop1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowsDesktop1.GradientActiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.GradientInactiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.GrayText = System.Drawing.Color.Empty;
            this.windowsDesktop1.InactiveBorder = System.Drawing.Color.Empty;
            this.windowsDesktop1.InactiveTitle = System.Drawing.Color.Empty;
            this.windowsDesktop1.InactiveTitleText = System.Drawing.Color.Empty;
            this.windowsDesktop1.IncreaseTBTransparency = false;
            this.windowsDesktop1.Location = new System.Drawing.Point(3, 3);
            this.windowsDesktop1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.windowsDesktop1.Metrics_BorderWidth = 1;
            this.windowsDesktop1.Metrics_CaptionFont = new System.Drawing.Font("Segoe UI", 9F);
            this.windowsDesktop1.Metrics_CaptionHeight = 22;
            this.windowsDesktop1.Metrics_CaptionWidth = 22;
            this.windowsDesktop1.Metrics_PaddedBorderWidth = 4;
            this.windowsDesktop1.Name = "windowsDesktop1";
            this.windowsDesktop1.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.windowsDesktop1.resVS = null;
            this.windowsDesktop1.Shadow = true;
            this.windowsDesktop1.Size = new System.Drawing.Size(528, 297);
            this.windowsDesktop1.TabIndex = 38;
            this.windowsDesktop1.TB_Blur = false;
            this.windowsDesktop1.TitlebarColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.windowsDesktop1.TitlebarColor_Enabled = false;
            this.windowsDesktop1.TitlebarColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.windowsDesktop1.TitleText = System.Drawing.Color.Empty;
            this.windowsDesktop1.Transparency = true;
            this.windowsDesktop1.UseWin11ORB_WithWin10 = false;
            this.windowsDesktop1.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.windowsDesktop1.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.windowsDesktop1.Visible = false;
            this.windowsDesktop1.Win7Alpha = 100;
            this.windowsDesktop1.Win7ColorBal = 100;
            this.windowsDesktop1.Win7GlowBal = 100;
            this.windowsDesktop1.Win7Noise = 1F;
            this.windowsDesktop1.Window = System.Drawing.Color.Empty;
            this.windowsDesktop1.WindowFrame = System.Drawing.Color.Empty;
            this.windowsDesktop1.Windows_10x_Theme = WinPaletter.Theme.Structures.Windows10x.Themes.Aero;
            this.windowsDesktop1.Windows_7_8_Theme = WinPaletter.Theme.Structures.Windows7.Themes.Aero;
            this.windowsDesktop1.WindowStyle = WinPaletter.PreviewHelpers.WindowStyle.W11;
            this.windowsDesktop1.WindowsXPTheme = WinPaletter.Theme.Structures.WindowsXP.Themes.LunaBlue;
            this.windowsDesktop1.WindowsXPThemeColorScheme = null;
            this.windowsDesktop1.WindowsXPThemePath = null;
            this.windowsDesktop1.WindowText = System.Drawing.Color.Empty;
            this.windowsDesktop1.WinVista = false;
            // 
            // BackupThemes_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1298, 637);
            this.Controls.Add(this.titlebarExtender1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupThemes_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Themes backups";
            this.Load += new System.EventHandler(this.BackupThemes_List_Load);
            this.titlebarExtender1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.bottom_buttons.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button button1;
        internal UI.WP.Button button3;
        internal UI.WP.Button button4;
        internal UI.WP.GroupBox groupBox2;
        public Templates.WindowsDesktop windowsDesktop1;
        private UI.WP.GroupBox bottom_buttons;
        internal System.Windows.Forms.Label label1;
        internal UI.WP.Button button5;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal UI.WP.Button Button20;
        internal UI.WP.Button button6;
        public Tabs.TitlebarExtender titlebarExtender1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        internal UI.WP.Button pin_button;
        private UI.WP.SeparatorV separatorV1;
        internal System.Windows.Forms.Label label4;
    }
}