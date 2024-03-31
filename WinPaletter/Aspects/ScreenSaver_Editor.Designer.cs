using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ScreenSaver_Editor : AspectsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenSaver_Editor));
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.button2 = new WinPaletter.UI.WP.Button();
            this.trackbarX1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox17 = new System.Windows.Forms.PictureBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.Button14 = new WinPaletter.UI.WP.Button();
            this.pnl_preview = new System.Windows.Forms.Panel();
            this.Button13 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.previewContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.titlebarExtender1.Size = new System.Drawing.Size(1004, 52);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.button2);
            this.GroupBox1.Controls.Add(this.trackbarX1);
            this.GroupBox1.Controls.Add(this.PictureBox4);
            this.GroupBox1.Controls.Add(this.CheckBox1);
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.PictureBox17);
            this.GroupBox1.Controls.Add(this.PictureBox1);
            this.GroupBox1.Controls.Add(this.TextBox1);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Location = new System.Drawing.Point(9, 58);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(439, 152);
            this.GroupBox1.TabIndex = 226;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = null;
            this.button2.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Menu;
            this.button2.ImageGlyphEnabled = true;
            this.button2.Location = new System.Drawing.Point(360, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 24);
            this.button2.TabIndex = 227;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // trackbarX1
            // 
            this.trackbarX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackbarX1.AnimateChanges = true;
            this.trackbarX1.BackColor = System.Drawing.Color.Transparent;
            this.trackbarX1.DefaultValue = 60;
            this.trackbarX1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackbarX1.Location = new System.Drawing.Point(36, 88);
            this.trackbarX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackbarX1.Maximum = 3600;
            this.trackbarX1.Minimum = 60;
            this.trackbarX1.Name = "trackbarX1";
            this.trackbarX1.Size = new System.Drawing.Size(399, 24);
            this.trackbarX1.TabIndex = 226;
            this.trackbarX1.Value = 60;
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(3, 3);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 217;
            this.PictureBox4.TabStop = false;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(36, 122);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(399, 24);
            this.CheckBox1.TabIndex = 223;
            this.CheckBox1.Text = "On resume, password protect";
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button1.ImageGlyphEnabled = true;
            this.Button1.Location = new System.Drawing.Point(401, 30);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(34, 24);
            this.Button1.TabIndex = 219;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Location = new System.Drawing.Point(33, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(402, 24);
            this.Label3.TabIndex = 216;
            this.Label3.Text = "File:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox17
            // 
            this.PictureBox17.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox17.Image")));
            this.PictureBox17.Location = new System.Drawing.Point(3, 61);
            this.PictureBox17.Name = "PictureBox17";
            this.PictureBox17.Size = new System.Drawing.Size(24, 24);
            this.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox17.TabIndex = 212;
            this.PictureBox17.TabStop = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(6, 122);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 222;
            this.PictureBox1.TabStop = false;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(36, 30);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(318, 24);
            this.TextBox1.TabIndex = 218;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(33, 61);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(402, 24);
            this.Label4.TabIndex = 213;
            this.Label4.Text = "Timeout (in seconds):";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.Button14);
            this.previewContainer.Controls.Add(this.pnl_preview);
            this.previewContainer.Controls.Add(this.Button13);
            this.previewContainer.Controls.Add(this.Button6);
            this.previewContainer.Controls.Add(this.Button5);
            this.previewContainer.Location = new System.Drawing.Point(455, 58);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(536, 340);
            this.previewContainer.TabIndex = 211;
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.CustomColor = System.Drawing.Color.Empty;
            this.Button14.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = null;
            this.Button14.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button14.ImageGlyph = null;
            this.Button14.ImageGlyphEnabled = true;
            this.Button14.Location = new System.Drawing.Point(213, 308);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(150, 24);
            this.Button14.TabIndex = 225;
            this.Button14.Text = "Configure its settings";
            this.Button14.UseVisualStyleBackColor = false;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // pnl_preview
            // 
            this.pnl_preview.BackColor = System.Drawing.Color.Black;
            this.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_preview.Location = new System.Drawing.Point(4, 4);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 2;
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(100)))));
            this.Button13.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button13.ForeColor = System.Drawing.Color.White;
            this.Button13.Image = null;
            this.Button13.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button13.ImageGlyph")));
            this.Button13.ImageGlyphEnabled = true;
            this.Button13.Location = new System.Drawing.Point(439, 308);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(90, 24);
            this.Button13.TabIndex = 224;
            this.Button13.Text = "Fullscreen";
            this.Button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button13.UseVisualStyleBackColor = false;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = null;
            this.Button6.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button6.ImageGlyph")));
            this.Button6.ImageGlyphEnabled = true;
            this.Button6.Location = new System.Drawing.Point(369, 308);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(29, 24);
            this.Button6.TabIndex = 223;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(100)))));
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button5.ImageGlyph")));
            this.Button5.ImageGlyphEnabled = true;
            this.Button5.Location = new System.Drawing.Point(404, 308);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(29, 24);
            this.Button5.TabIndex = 222;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // ScreenSaver_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1004, 461);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.previewContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScreenSaver_Editor";
            this.Text = "Screen Saver";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenSaver_Editor_FormClosing);
            this.Load += new System.EventHandler(this.ScreenSaver_Editor_Load);
            this.Controls.SetChildIndex(this.previewContainer, 0);
            this.Controls.SetChildIndex(this.GroupBox1, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.previewContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal UI.WP.GroupBox previewContainer;
        internal Panel pnl_preview;
        internal Label Label4;
        internal PictureBox PictureBox17;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox1;
        internal PictureBox PictureBox4;
        internal Label Label3;
        internal PictureBox PictureBox1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.Button Button14;
        internal UI.WP.Button Button13;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button5;
        internal UI.WP.GroupBox GroupBox1;
        private UI.Controllers.TrackBarX trackbarX1;
        internal UI.WP.Button button2;
    }
}
