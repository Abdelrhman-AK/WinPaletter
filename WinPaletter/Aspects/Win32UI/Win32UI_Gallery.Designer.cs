namespace WinPaletter
{
    partial class Win32UI_Gallery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win32UI_Gallery));
            this.schemes = new System.Windows.Forms.FlowLayoutPanel();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.btn_cancel = new WinPaletter.UI.WP.Button();
            this.btn_load_into_theme = new WinPaletter.UI.WP.Button();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // schemes
            // 
            this.schemes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schemes.AutoScroll = true;
            this.schemes.Location = new System.Drawing.Point(0, 0);
            this.schemes.Name = "schemes";
            this.schemes.Padding = new System.Windows.Forms.Padding(3);
            this.schemes.Size = new System.Drawing.Size(1049, 503);
            this.schemes.TabIndex = 120;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.btn_cancel);
            this.bottom_buttons.Controls.Add(this.btn_load_into_theme);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 511);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(1049, 48);
            this.bottom_buttons.TabIndex = 119;
            this.bottom_buttons.UseDecorationPattern = false;
            this.bottom_buttons.UseSharpStyle = false;
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
            this.btn_cancel.ImageGlyph = null;
            this.btn_cancel.ImageGlyphEnabled = false;
            this.btn_cancel.Location = new System.Drawing.Point(858, 6);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(80, 36);
            this.btn_cancel.TabIndex = 212;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_load_into_theme
            // 
            this.btn_load_into_theme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_load_into_theme.CustomColor = System.Drawing.Color.Empty;
            this.btn_load_into_theme.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.btn_load_into_theme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_load_into_theme.ForeColor = System.Drawing.Color.White;
            this.btn_load_into_theme.Image = ((System.Drawing.Image)(resources.GetObject("btn_load_into_theme.Image")));
            this.btn_load_into_theme.ImageGlyph = null;
            this.btn_load_into_theme.ImageGlyphEnabled = false;
            this.btn_load_into_theme.Location = new System.Drawing.Point(944, 6);
            this.btn_load_into_theme.Name = "btn_load_into_theme";
            this.btn_load_into_theme.Size = new System.Drawing.Size(100, 36);
            this.btn_load_into_theme.TabIndex = 211;
            this.btn_load_into_theme.Text = "Pick";
            this.btn_load_into_theme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_load_into_theme.UseVisualStyleBackColor = false;
            this.btn_load_into_theme.Click += new System.EventHandler(this.btn_load_into_theme_Click);
            // 
            // Win32UI_Gallery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1049, 559);
            this.Controls.Add(this.schemes);
            this.Controls.Add(this.bottom_buttons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Win32UI_Gallery";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Classic Colors schemes gallery";
            this.Load += new System.EventHandler(this.Win32UI_Gallery_Load);
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.WP.GroupBox bottom_buttons;
        internal UI.WP.Button btn_cancel;
        internal UI.WP.Button btn_load_into_theme;
        private System.Windows.Forms.FlowLayoutPanel schemes;
    }
}