using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class VisualStyles : AspectsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualStyles));
            this.VS_ColorsList = new WinPaletter.UI.WP.ComboBox();
            this.WXP_VS_Browse = new WinPaletter.UI.WP.Button();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.PictureBox40 = new System.Windows.Forms.PictureBox();
            this.Label69 = new System.Windows.Forms.Label();
            this.Label67 = new System.Windows.Forms.Label();
            this.VS_textbox = new WinPaletter.UI.WP.TextBox();
            this.VS_SizesList = new WinPaletter.UI.WP.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button29 = new WinPaletter.UI.WP.Button();
            this.button28 = new WinPaletter.UI.WP.Button();
            this.alertBox2 = new WinPaletter.UI.WP.AlertBox();
            this.alertBox3 = new WinPaletter.UI.WP.AlertBox();
            this.alertBox4 = new WinPaletter.UI.WP.AlertBox();
            this.separatorH1 = new WinPaletter.UI.WP.SeparatorH();
            this.VS_ReplaceColors = new WinPaletter.UI.WP.CheckBox();
            this.VS_ReplaceMetrics = new WinPaletter.UI.WP.CheckBox();
            this.separatorH2 = new WinPaletter.UI.WP.SeparatorH();
            this.alertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.separatorH3 = new WinPaletter.UI.WP.SeparatorH();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.alertBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.titlebarExtender1.Size = new System.Drawing.Size(893, 52);
            // 
            // VS_ColorsList
            // 
            this.VS_ColorsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VS_ColorsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.VS_ColorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VS_ColorsList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VS_ColorsList.ForeColor = System.Drawing.Color.White;
            this.VS_ColorsList.FormattingEnabled = true;
            this.VS_ColorsList.ItemHeight = 20;
            this.VS_ColorsList.Location = new System.Drawing.Point(182, 99);
            this.VS_ColorsList.Name = "VS_ColorsList";
            this.VS_ColorsList.Size = new System.Drawing.Size(699, 26);
            this.VS_ColorsList.TabIndex = 123;
            // 
            // WXP_VS_Browse
            // 
            this.WXP_VS_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WXP_VS_Browse.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.WXP_VS_Browse.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.WXP_VS_Browse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_VS_Browse.ForeColor = System.Drawing.Color.White;
            this.WXP_VS_Browse.Image = null;
            this.WXP_VS_Browse.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.WXP_VS_Browse.ImageGlyphEnabled = true;
            this.WXP_VS_Browse.Location = new System.Drawing.Point(846, 63);
            this.WXP_VS_Browse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.WXP_VS_Browse.Name = "WXP_VS_Browse";
            this.WXP_VS_Browse.Size = new System.Drawing.Size(35, 24);
            this.WXP_VS_Browse.TabIndex = 125;
            this.WXP_VS_Browse.UseVisualStyleBackColor = false;
            this.WXP_VS_Browse.Click += new System.EventHandler(this.VS_Browse_Click);
            // 
            // PictureBox41
            // 
            this.PictureBox41.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(12, 100);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(24, 24);
            this.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox41.TabIndex = 121;
            this.PictureBox41.TabStop = false;
            // 
            // PictureBox40
            // 
            this.PictureBox40.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox40.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox40.Image")));
            this.PictureBox40.Location = new System.Drawing.Point(12, 63);
            this.PictureBox40.Name = "PictureBox40";
            this.PictureBox40.Size = new System.Drawing.Size(24, 24);
            this.PictureBox40.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox40.TabIndex = 122;
            this.PictureBox40.TabStop = false;
            // 
            // Label69
            // 
            this.Label69.AutoEllipsis = true;
            this.Label69.BackColor = System.Drawing.Color.Transparent;
            this.Label69.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label69.Location = new System.Drawing.Point(39, 102);
            this.Label69.Name = "Label69";
            this.Label69.Size = new System.Drawing.Size(137, 20);
            this.Label69.TabIndex = 119;
            this.Label69.Text = "Color scheme:";
            this.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label67
            // 
            this.Label67.AutoEllipsis = true;
            this.Label67.BackColor = System.Drawing.Color.Transparent;
            this.Label67.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label67.Location = new System.Drawing.Point(39, 65);
            this.Label67.Name = "Label67";
            this.Label67.Size = new System.Drawing.Size(137, 20);
            this.Label67.TabIndex = 120;
            this.Label67.Text = "Visual Styles file:";
            this.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VS_textbox
            // 
            this.VS_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VS_textbox.ForeColor = System.Drawing.Color.White;
            this.VS_textbox.Location = new System.Drawing.Point(182, 63);
            this.VS_textbox.MaxLength = 32767;
            this.VS_textbox.Multiline = false;
            this.VS_textbox.Name = "VS_textbox";
            this.VS_textbox.ReadOnly = false;
            this.VS_textbox.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.VS_textbox.SelectedText = "";
            this.VS_textbox.SelectionLength = 0;
            this.VS_textbox.SelectionStart = 0;
            this.VS_textbox.Size = new System.Drawing.Size(657, 24);
            this.VS_textbox.TabIndex = 124;
            this.VS_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.VS_textbox.UseSystemPasswordChar = false;
            this.VS_textbox.WordWrap = true;
            this.VS_textbox.TextChanged += new System.EventHandler(this.VS_textbox_TextChanged);
            // 
            // VS_SizesList
            // 
            this.VS_SizesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VS_SizesList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.VS_SizesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VS_SizesList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VS_SizesList.ForeColor = System.Drawing.Color.White;
            this.VS_SizesList.FormattingEnabled = true;
            this.VS_SizesList.ItemHeight = 20;
            this.VS_SizesList.Location = new System.Drawing.Point(182, 166);
            this.VS_SizesList.Name = "VS_SizesList";
            this.VS_SizesList.Size = new System.Drawing.Size(699, 26);
            this.VS_SizesList.TabIndex = 128;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 167);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 127;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(39, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 126;
            this.label1.Text = "Size scheme:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button29
            // 
            this.button29.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button29.CustomColor = System.Drawing.Color.Empty;
            this.button29.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button29.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button29.Image = null;
            this.button29.ImageGlyph = null;
            this.button29.ImageGlyphEnabled = false;
            this.button29.Location = new System.Drawing.Point(540, 10);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(240, 28);
            this.button29.TabIndex = 22;
            this.button29.Text = "Get an official release (recommended)";
            this.button29.UseVisualStyleBackColor = true;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // button28
            // 
            this.button28.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button28.CustomColor = System.Drawing.Color.Empty;
            this.button28.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button28.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button28.Image = null;
            this.button28.ImageGlyph = null;
            this.button28.ImageGlyphEnabled = false;
            this.button28.Location = new System.Drawing.Point(786, 10);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(75, 28);
            this.button28.TabIndex = 21;
            this.button28.Text = "Setup";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // alertBox2
            // 
            this.alertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.alertBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertBox2.BackColor = System.Drawing.Color.Transparent;
            this.alertBox2.CenterText = false;
            this.alertBox2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.alertBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.alertBox2.Image = null;
            this.alertBox2.Location = new System.Drawing.Point(10, 416);
            this.alertBox2.Name = "alertBox2";
            this.alertBox2.Size = new System.Drawing.Size(869, 30);
            this.alertBox2.TabIndex = 211;
            this.alertBox2.TabStop = false;
            this.alertBox2.Text = "0";
            // 
            // alertBox3
            // 
            this.alertBox3.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.alertBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertBox3.BackColor = System.Drawing.Color.Transparent;
            this.alertBox3.CenterText = false;
            this.alertBox3.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.alertBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.alertBox3.Image = null;
            this.alertBox3.Location = new System.Drawing.Point(10, 452);
            this.alertBox3.Name = "alertBox3";
            this.alertBox3.Size = new System.Drawing.Size(869, 30);
            this.alertBox3.TabIndex = 212;
            this.alertBox3.TabStop = false;
            this.alertBox3.Text = "Rendering a preview is not supported. If you want to test a visual styles, press " +
    "on \'Apply\' with the toggle above enabled.";
            // 
            // alertBox4
            // 
            this.alertBox4.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.alertBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertBox4.BackColor = System.Drawing.Color.Transparent;
            this.alertBox4.CenterText = false;
            this.alertBox4.Controls.Add(this.button28);
            this.alertBox4.Controls.Add(this.button29);
            this.alertBox4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.alertBox4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.alertBox4.Image = null;
            this.alertBox4.Location = new System.Drawing.Point(10, 362);
            this.alertBox4.Name = "alertBox4";
            this.alertBox4.Size = new System.Drawing.Size(869, 48);
            this.alertBox4.TabIndex = 213;
            this.alertBox4.TabStop = false;
            this.alertBox4.Text = "Your Windows must have UxTheme.dll patched to use unsigned and patched visual sty" +
    "les.\r\nYou can use SecureUxTheme setup wrapper (for Windows 8.1 and higher)";
            // 
            // separatorH1
            // 
            this.separatorH1.AlternativeLook = false;
            this.separatorH1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorH1.BackColor = System.Drawing.Color.Transparent;
            this.separatorH1.Location = new System.Drawing.Point(12, 93);
            this.separatorH1.Name = "separatorH1";
            this.separatorH1.Size = new System.Drawing.Size(869, 1);
            this.separatorH1.TabIndex = 214;
            this.separatorH1.TabStop = false;
            this.separatorH1.Text = "separatorH1";
            // 
            // VS_ReplaceColors
            // 
            this.VS_ReplaceColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VS_ReplaceColors.Checked = false;
            this.VS_ReplaceColors.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VS_ReplaceColors.ForeColor = System.Drawing.Color.White;
            this.VS_ReplaceColors.Location = new System.Drawing.Point(182, 130);
            this.VS_ReplaceColors.Name = "VS_ReplaceColors";
            this.VS_ReplaceColors.Size = new System.Drawing.Size(699, 24);
            this.VS_ReplaceColors.TabIndex = 217;
            this.VS_ReplaceColors.Text = "Override \'Classic Colors\' by selected color scheme in this theme when applying";
            // 
            // VS_ReplaceMetrics
            // 
            this.VS_ReplaceMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VS_ReplaceMetrics.Checked = false;
            this.VS_ReplaceMetrics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VS_ReplaceMetrics.ForeColor = System.Drawing.Color.White;
            this.VS_ReplaceMetrics.Location = new System.Drawing.Point(182, 198);
            this.VS_ReplaceMetrics.Name = "VS_ReplaceMetrics";
            this.VS_ReplaceMetrics.Size = new System.Drawing.Size(699, 24);
            this.VS_ReplaceMetrics.TabIndex = 218;
            this.VS_ReplaceMetrics.Text = "Override \'Metrics and Fonts\' by selected size scheme in this theme when applying";
            // 
            // separatorH2
            // 
            this.separatorH2.AlternativeLook = false;
            this.separatorH2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorH2.BackColor = System.Drawing.Color.Transparent;
            this.separatorH2.Location = new System.Drawing.Point(12, 160);
            this.separatorH2.Name = "separatorH2";
            this.separatorH2.Size = new System.Drawing.Size(869, 1);
            this.separatorH2.TabIndex = 220;
            this.separatorH2.TabStop = false;
            this.separatorH2.Text = "separatorH2";
            // 
            // alertBox1
            // 
            this.alertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.alertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertBox1.BackColor = System.Drawing.Color.Transparent;
            this.alertBox1.CenterText = false;
            this.alertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.alertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.alertBox1.Image = ((System.Drawing.Image)(resources.GetObject("alertBox1.Image")));
            this.alertBox1.Location = new System.Drawing.Point(12, 236);
            this.alertBox1.Name = "alertBox1";
            this.alertBox1.Size = new System.Drawing.Size(869, 30);
            this.alertBox1.TabIndex = 221;
            this.alertBox1.TabStop = false;
            this.alertBox1.Text = "0";
            this.alertBox1.Visible = false;
            // 
            // separatorH3
            // 
            this.separatorH3.AlternativeLook = false;
            this.separatorH3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorH3.BackColor = System.Drawing.Color.Transparent;
            this.separatorH3.Location = new System.Drawing.Point(12, 228);
            this.separatorH3.Name = "separatorH3";
            this.separatorH3.Size = new System.Drawing.Size(869, 1);
            this.separatorH3.TabIndex = 222;
            this.separatorH3.TabStop = false;
            this.separatorH3.Text = "separatorH3";
            // 
            // VisualStyles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(893, 542);
            this.Controls.Add(this.separatorH3);
            this.Controls.Add(this.alertBox1);
            this.Controls.Add(this.separatorH2);
            this.Controls.Add(this.VS_ReplaceMetrics);
            this.Controls.Add(this.VS_ReplaceColors);
            this.Controls.Add(this.separatorH1);
            this.Controls.Add(this.alertBox4);
            this.Controls.Add(this.alertBox3);
            this.Controls.Add(this.alertBox2);
            this.Controls.Add(this.VS_SizesList);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VS_ColorsList);
            this.Controls.Add(this.WXP_VS_Browse);
            this.Controls.Add(this.PictureBox41);
            this.Controls.Add(this.PictureBox40);
            this.Controls.Add(this.Label69);
            this.Controls.Add(this.Label67);
            this.Controls.Add(this.VS_textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualStyles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visual Styles";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.VisualStyles_Load);
            this.Controls.SetChildIndex(this.VS_textbox, 0);
            this.Controls.SetChildIndex(this.Label67, 0);
            this.Controls.SetChildIndex(this.Label69, 0);
            this.Controls.SetChildIndex(this.PictureBox40, 0);
            this.Controls.SetChildIndex(this.PictureBox41, 0);
            this.Controls.SetChildIndex(this.WXP_VS_Browse, 0);
            this.Controls.SetChildIndex(this.VS_ColorsList, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.VS_SizesList, 0);
            this.Controls.SetChildIndex(this.alertBox2, 0);
            this.Controls.SetChildIndex(this.alertBox3, 0);
            this.Controls.SetChildIndex(this.alertBox4, 0);
            this.Controls.SetChildIndex(this.separatorH1, 0);
            this.Controls.SetChildIndex(this.VS_ReplaceColors, 0);
            this.Controls.SetChildIndex(this.VS_ReplaceMetrics, 0);
            this.Controls.SetChildIndex(this.separatorH2, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
            this.Controls.SetChildIndex(this.alertBox1, 0);
            this.Controls.SetChildIndex(this.separatorH3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.alertBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal UI.WP.ComboBox VS_ColorsList;
        internal UI.WP.Button WXP_VS_Browse;
        internal PictureBox PictureBox41;
        internal PictureBox PictureBox40;
        internal Label Label69;
        internal Label Label67;
        internal UI.WP.TextBox VS_textbox;
        internal UI.WP.ComboBox VS_SizesList;
        internal PictureBox pictureBox1;
        internal Label label1;
        private UI.WP.Button button29;
        private UI.WP.Button button28;
        private UI.WP.AlertBox alertBox2;
        private UI.WP.AlertBox alertBox3;
        private UI.WP.AlertBox alertBox4;
        private UI.WP.SeparatorH separatorH1;
        internal UI.WP.CheckBox VS_ReplaceColors;
        internal UI.WP.CheckBox VS_ReplaceMetrics;
        private UI.WP.SeparatorH separatorH2;
        private UI.WP.AlertBox alertBox1;
        private UI.WP.SeparatorH separatorH3;
    }
}
