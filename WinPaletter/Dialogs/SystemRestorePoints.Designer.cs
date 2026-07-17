namespace WinPaletter.Dialogs
{
    partial class SystemRestorePoints
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemRestorePoints));
            this.titlebarExtender1 = new WinPaletter.Tabs.TitlebarExtender();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pin_button = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.button1 = new WinPaletter.UI.WP.Button();
            this.button2 = new WinPaletter.UI.WP.Button();
            this.respoints = new WinPaletter.UI.WP.ListView();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.button3 = new WinPaletter.UI.WP.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.titlebarExtender1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.bottom_buttons.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.flowLayoutPanel1);
            this.titlebarExtender1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebarExtender1.Flag = WinPaletter.Tabs.TitlebarExtender.Flags.Tabs_Extended;
            this.titlebarExtender1.FormFocused = true;
            this.titlebarExtender1.Location = new System.Drawing.Point(0, 0);
            this.titlebarExtender1.Name = "titlebarExtender1";
            this.titlebarExtender1.Size = new System.Drawing.Size(884, 52);
            this.titlebarExtender1.TabIndex = 210;
            this.titlebarExtender1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.pin_button);
            this.flowLayoutPanel1.Controls.Add(this.Button4);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(868, 40);
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
            // Button4
            // 
            this.Button4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.Button4.ImageGlyphEnabled = true;
            this.Button4.Location = new System.Drawing.Point(43, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(34, 34);
            this.Button4.TabIndex = 126;
            this.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // button1
            // 
            this.button1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.button1.Enabled = false;
            this.button1.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = null;
            this.button1.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Delete;
            this.button1.ImageGlyphEnabled = true;
            this.button1.Location = new System.Drawing.Point(83, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 34);
            this.button1.TabIndex = 127;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.button2.Enabled = false;
            this.button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = null;
            this.button2.ImageGlyph = null;
            this.button2.ImageGlyphEnabled = false;
            this.button2.Location = new System.Drawing.Point(123, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 34);
            this.button2.TabIndex = 216;
            this.button2.Text = "Delete all restorepoints";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // respoints
            // 
            this.respoints.AllowColumnReorder = true;
            this.respoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.respoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.respoints.FullRowSelect = true;
            this.respoints.HideSelection = false;
            this.respoints.LabelEdit = true;
            this.respoints.Location = new System.Drawing.Point(0, 52);
            this.respoints.MultiSelect = false;
            this.respoints.Name = "respoints";
            this.respoints.RowHeight = 28;
            this.respoints.Size = new System.Drawing.Size(884, 361);
            this.respoints.TabIndex = 211;
            this.respoints.UseCompatibleStateImageBehavior = false;
            this.respoints.View = System.Windows.Forms.View.Details;
            this.respoints.SelectedIndexChanged += new System.EventHandler(this.respoints_SelectedIndexChanged);
            this.respoints.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.respoints_MouseDoubleClick);
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.button3);
            this.bottom_buttons.Controls.Add(this.flowLayoutPanel2);
            this.bottom_buttons.Controls.Add(this.Button7);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 413);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(884, 48);
            this.bottom_buttons.TabIndex = 214;
            this.bottom_buttons.UseDecorationPattern = false;
            this.bottom_buttons.UseSharpStyle = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.button3.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = null;
            this.button3.ImageGlyph = null;
            this.button3.ImageGlyphEnabled = false;
            this.button3.Location = new System.Drawing.Point(726, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 34);
            this.button3.TabIndex = 216;
            this.button3.Text = "Manage by Windows";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Controls.Add(this.Label8);
            this.flowLayoutPanel2.Controls.Add(this.Label9);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(8, 8);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(626, 32);
            this.flowLayoutPanel2.TabIndex = 215;
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 8);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(318, 15);
            this.Label8.TabIndex = 69;
            this.Label8.Text = "Overall size (including ones not created by WinPaletter):";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(327, 8);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(0, 15);
            this.Label9.TabIndex = 71;
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.ImageGlyph = null;
            this.Button7.ImageGlyphEnabled = false;
            this.Button7.Location = new System.Drawing.Point(640, 6);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(80, 34);
            this.Button7.TabIndex = 205;
            this.Button7.Text = "Close";
            this.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // SystemRestorePoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.respoints);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.titlebarExtender1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SystemRestorePoints";
            this.Text = "System Restorepoints";
            this.Load += new System.EventHandler(this.SystemRestorePoints_Load);
            this.titlebarExtender1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.bottom_buttons.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Tabs.TitlebarExtender titlebarExtender1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        internal UI.WP.Button pin_button;
        private UI.WP.ListView respoints;
        internal UI.WP.Button Button4;
        internal UI.WP.Button button1;
        private UI.WP.GroupBox bottom_buttons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label9;
        internal UI.WP.Button Button7;
        internal UI.WP.Button button2;
        internal UI.WP.Button button3;
    }
}
