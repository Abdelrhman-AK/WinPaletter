namespace WinPaletter
{
    partial class AspectsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AspectsTemplate));
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.btn_apply = new WinPaletter.UI.WP.Button();
            this.checker_mode_simple = new WinPaletter.UI.WP.RadioImage();
            this.btn_cancel = new WinPaletter.UI.WP.Button();
            this.checker_mode_advanced = new WinPaletter.UI.WP.RadioImage();
            this.btn_load_into_theme = new WinPaletter.UI.WP.Button();
            this.titlebarExtender1 = new WinPaletter.Tabs.TitlebarExtender();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pin_button = new WinPaletter.UI.WP.Button();
            this.btn_import = new WinPaletter.UI.WP.Button();
            this.btn_palette_generate = new WinPaletter.UI.WP.Button();
            this.btn_saveas_MSTheme = new WinPaletter.UI.WP.Button();
            this.checker = new WinPaletter.UI.WP.Toggle();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.separatorV1 = new WinPaletter.UI.WP.SeparatorV();
            this.bottom_buttons.SuspendLayout();
            this.titlebarExtender1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.SuspendLayout();
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.btn_apply);
            this.bottom_buttons.Controls.Add(this.checker_mode_simple);
            this.bottom_buttons.Controls.Add(this.btn_cancel);
            this.bottom_buttons.Controls.Add(this.checker_mode_advanced);
            this.bottom_buttons.Controls.Add(this.btn_load_into_theme);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 513);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(884, 48);
            this.bottom_buttons.TabIndex = 118;
            // 
            // btn_apply
            // 
            this.btn_apply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_apply.CustomColor = System.Drawing.Color.Empty;
            this.btn_apply.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.btn_apply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_apply.ForeColor = System.Drawing.Color.White;
            this.btn_apply.Image = ((System.Drawing.Image)(resources.GetObject("btn_apply.Image")));
            this.btn_apply.ImageAsVector = false;
            this.btn_apply.ImageVector = null;
            this.btn_apply.Location = new System.Drawing.Point(498, 6);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(115, 36);
            this.btn_apply.TabIndex = 213;
            this.btn_apply.Text = "0";
            this.btn_apply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_apply.UseVisualStyleBackColor = false;
            // 
            // checker_mode_simple
            // 
            this.checker_mode_simple.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checker_mode_simple.Checked = false;
            this.checker_mode_simple.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checker_mode_simple.ForeColor = System.Drawing.Color.White;
            this.checker_mode_simple.Image = null;
            this.checker_mode_simple.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checker_mode_simple.Location = new System.Drawing.Point(157, 5);
            this.checker_mode_simple.Name = "checker_mode_simple";
            this.checker_mode_simple.Size = new System.Drawing.Size(145, 36);
            this.checker_mode_simple.TabIndex = 117;
            this.checker_mode_simple.Text = "0";
            this.checker_mode_simple.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checker_mode_simple.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.CustomColor = System.Drawing.Color.Empty;
            this.btn_cancel.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.btn_cancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_cancel.ForeColor = System.Drawing.Color.White;
            this.btn_cancel.Image = null;
            this.btn_cancel.ImageAsVector = false;
            this.btn_cancel.ImageVector = null;
            this.btn_cancel.Location = new System.Drawing.Point(412, 6);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(80, 36);
            this.btn_cancel.TabIndex = 212;
            this.btn_cancel.Text = "0";
            this.btn_cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // checker_mode_advanced
            // 
            this.checker_mode_advanced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checker_mode_advanced.Checked = true;
            this.checker_mode_advanced.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checker_mode_advanced.ForeColor = System.Drawing.Color.White;
            this.checker_mode_advanced.Image = null;
            this.checker_mode_advanced.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checker_mode_advanced.Location = new System.Drawing.Point(6, 5);
            this.checker_mode_advanced.Name = "checker_mode_advanced";
            this.checker_mode_advanced.Size = new System.Drawing.Size(145, 36);
            this.checker_mode_advanced.TabIndex = 116;
            this.checker_mode_advanced.Text = "0";
            this.checker_mode_advanced.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checker_mode_advanced.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btn_load_into_theme
            // 
            this.btn_load_into_theme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_load_into_theme.CustomColor = System.Drawing.Color.Empty;
            this.btn_load_into_theme.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.CustomColorOnHover)));
            this.btn_load_into_theme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_load_into_theme.ForeColor = System.Drawing.Color.White;
            this.btn_load_into_theme.Image = ((System.Drawing.Image)(resources.GetObject("btn_load_into_theme.Image")));
            this.btn_load_into_theme.ImageAsVector = false;
            this.btn_load_into_theme.ImageVector = null;
            this.btn_load_into_theme.Location = new System.Drawing.Point(619, 6);
            this.btn_load_into_theme.Name = "btn_load_into_theme";
            this.btn_load_into_theme.Size = new System.Drawing.Size(260, 36);
            this.btn_load_into_theme.TabIndex = 211;
            this.btn_load_into_theme.Text = "0";
            this.btn_load_into_theme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_load_into_theme.UseVisualStyleBackColor = false;
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.flowLayoutPanel1);
            this.titlebarExtender1.Controls.Add(this.checker);
            this.titlebarExtender1.Controls.Add(this.checker_img);
            this.titlebarExtender1.Controls.Add(this.separatorV1);
            this.titlebarExtender1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebarExtender1.DropDWMEffect = false;
            this.titlebarExtender1.Location = new System.Drawing.Point(0, 0);
            this.titlebarExtender1.Name = "titlebarExtender1";
            this.titlebarExtender1.Size = new System.Drawing.Size(884, 52);
            this.titlebarExtender1.TabIndex = 115;
            this.titlebarExtender1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.pin_button);
            this.flowLayoutPanel1.Controls.Add(this.btn_import);
            this.flowLayoutPanel1.Controls.Add(this.btn_palette_generate);
            this.flowLayoutPanel1.Controls.Add(this.btn_saveas_MSTheme);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(770, 40);
            this.flowLayoutPanel1.TabIndex = 124;
            // 
            // pin_button
            // 
            this.pin_button.CustomColor = System.Drawing.Color.Empty;
            this.pin_button.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.pin_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pin_button.ForeColor = System.Drawing.Color.White;
            this.pin_button.Image = ((System.Drawing.Image)(resources.GetObject("pin_button.Image")));
            this.pin_button.ImageAsVector = false;
            this.pin_button.ImageVector = null;
            this.pin_button.Location = new System.Drawing.Point(3, 3);
            this.pin_button.Name = "pin_button";
            this.pin_button.Size = new System.Drawing.Size(34, 34);
            this.pin_button.TabIndex = 124;
            this.pin_button.UseVisualStyleBackColor = false;
            this.pin_button.Visible = false;
            this.pin_button.Click += new System.EventHandler(this.pin_button_Click);
            // 
            // btn_import
            // 
            this.btn_import.CustomColor = System.Drawing.Color.Empty;
            this.btn_import.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.btn_import.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_import.ForeColor = System.Drawing.Color.White;
            this.btn_import.Image = ((System.Drawing.Image)(resources.GetObject("btn_import.Image")));
            this.btn_import.ImageAsVector = false;
            this.btn_import.ImageVector = null;
            this.btn_import.Location = new System.Drawing.Point(43, 3);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(235, 34);
            this.btn_import.TabIndex = 110;
            this.btn_import.Text = "0";
            this.btn_import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_import.UseVisualStyleBackColor = false;
            // 
            // btn_palette_generate
            // 
            this.btn_palette_generate.CustomColor = System.Drawing.Color.Empty;
            this.btn_palette_generate.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.btn_palette_generate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_palette_generate.ForeColor = System.Drawing.Color.White;
            this.btn_palette_generate.Image = ((System.Drawing.Image)(resources.GetObject("btn_palette_generate.Image")));
            this.btn_palette_generate.ImageAsVector = false;
            this.btn_palette_generate.ImageVector = null;
            this.btn_palette_generate.Location = new System.Drawing.Point(284, 3);
            this.btn_palette_generate.Name = "btn_palette_generate";
            this.btn_palette_generate.Size = new System.Drawing.Size(235, 34);
            this.btn_palette_generate.TabIndex = 122;
            this.btn_palette_generate.Text = "0";
            this.btn_palette_generate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_palette_generate.UseVisualStyleBackColor = false;
            // 
            // btn_saveas_MSTheme
            // 
            this.btn_saveas_MSTheme.CustomColor = System.Drawing.Color.Empty;
            this.btn_saveas_MSTheme.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.btn_saveas_MSTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_saveas_MSTheme.ForeColor = System.Drawing.Color.White;
            this.btn_saveas_MSTheme.Image = ((System.Drawing.Image)(resources.GetObject("btn_saveas_MSTheme.Image")));
            this.btn_saveas_MSTheme.ImageAsVector = false;
            this.btn_saveas_MSTheme.ImageVector = null;
            this.btn_saveas_MSTheme.Location = new System.Drawing.Point(525, 3);
            this.btn_saveas_MSTheme.Name = "btn_saveas_MSTheme";
            this.btn_saveas_MSTheme.Size = new System.Drawing.Size(200, 34);
            this.btn_saveas_MSTheme.TabIndex = 123;
            this.btn_saveas_MSTheme.Text = "0";
            this.btn_saveas_MSTheme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_saveas_MSTheme.UseVisualStyleBackColor = false;
            // 
            // checker
            // 
            this.checker.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checker.Checked = false;
            this.checker.DarkLight_Toggler = false;
            this.checker.Location = new System.Drawing.Point(836, 17);
            this.checker.Name = "checker";
            this.checker.Size = new System.Drawing.Size(40, 20);
            this.checker.TabIndex = 121;
            this.checker.CheckedChanged += new System.EventHandler(this.checker_CheckedChanged);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(794, 12);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 120;
            this.checker_img.TabStop = false;
            // 
            // separatorV1
            // 
            this.separatorV1.AlternativeLook = false;
            this.separatorV1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorV1.BackColor = System.Drawing.Color.Transparent;
            this.separatorV1.Location = new System.Drawing.Point(785, 8);
            this.separatorV1.Name = "separatorV1";
            this.separatorV1.Size = new System.Drawing.Size(1, 38);
            this.separatorV1.TabIndex = 113;
            this.separatorV1.TabStop = false;
            this.separatorV1.Text = "separatorV1";
            // 
            // AspectsTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.titlebarExtender1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(800, 300);
            this.Name = "AspectsTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aspect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AspectsTemplate_FormClosing);
            this.Load += new System.EventHandler(this.AspectsTemplate_Load);
            this.Shown += new System.EventHandler(this.AspectsTemplate_Shown);
            this.ParentChanged += new System.EventHandler(this.AspectsTemplate_ParentChanged);
            this.bottom_buttons.ResumeLayout(false);
            this.titlebarExtender1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private UI.WP.RadioImage checker_mode_simple;
        private UI.WP.RadioImage checker_mode_advanced;
        private UI.WP.GroupBox bottom_buttons;
        internal UI.WP.Button btn_apply;
        internal UI.WP.Button btn_cancel;
        internal UI.WP.Button btn_load_into_theme;
        internal UI.WP.Toggle Toggle;
        internal System.Windows.Forms.PictureBox checker_img;
        internal UI.WP.Button btn_import;
        private UI.WP.SeparatorV separatorV1;
        internal UI.WP.Button btn_palette_generate;
        internal UI.WP.Toggle checker;
        internal UI.WP.Button btn_saveas_MSTheme;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public Tabs.TitlebarExtender titlebarExtender1;
        internal UI.WP.Button pin_button;
    }
}