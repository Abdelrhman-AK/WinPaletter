using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BugReport : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BugReport));
            this.separatorH1 = new WinPaletter.UI.WP.SeparatorH();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.TreeView1 = new WinPaletter.UI.WP.TreeView();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.button10 = new WinPaletter.UI.WP.Button();
            this.button9 = new WinPaletter.UI.WP.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button8 = new WinPaletter.UI.WP.Button();
            this.button7 = new WinPaletter.UI.WP.Button();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // separatorH1
            // 
            this.separatorH1.AlternativeLook = false;
            this.separatorH1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorH1.BackColor = System.Drawing.Color.Transparent;
            this.separatorH1.Location = new System.Drawing.Point(12, 176);
            this.separatorH1.Name = "separatorH1";
            this.separatorH1.Size = new System.Drawing.Size(820, 1);
            this.separatorH1.TabIndex = 124;
            this.separatorH1.TabStop = false;
            this.separatorH1.Text = "separatorH1";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.Button3);
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Controls.Add(this.PictureBox4);
            this.GroupBox3.Controls.Add(this.Button4);
            this.GroupBox3.Controls.Add(this.Button6);
            this.GroupBox3.Controls.Add(this.TreeView1);
            this.GroupBox3.Controls.Add(this.Button5);
            this.GroupBox3.Location = new System.Drawing.Point(12, 183);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(820, 259);
            this.GroupBox3.TabIndex = 114;
            this.GroupBox3.UseDecorationPattern = false;
            this.GroupBox3.UseSharpStyle = false;
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Copy;
            this.Button3.ImageGlyphEnabled = true;
            this.Button3.Location = new System.Drawing.Point(310, 6);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(190, 24);
            this.Button3.TabIndex = 118;
            this.Button3.Text = "Copy log into clipboard";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(35, 6);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(134, 24);
            this.Label4.TabIndex = 86;
            this.Label4.Text = "Error details:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox4
            // 
            this.PictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(5, 6);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 1;
            this.PictureBox4.TabStop = false;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(89)))), ((int)(((byte)(174)))));
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Save;
            this.Button4.ImageGlyphEnabled = true;
            this.Button4.Location = new System.Drawing.Point(506, 6);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(140, 24);
            this.Button4.TabIndex = 119;
            this.Button4.Text = "Save as *.json";
            this.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = null;
            this.Button6.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button6.ImageGlyphEnabled = true;
            this.Button6.Location = new System.Drawing.Point(652, 6);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(160, 24);
            this.Button6.TabIndex = 121;
            this.Button6.Text = "Open previous reports";
            this.Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // TreeView1
            // 
            this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView1.Indent = 15;
            this.TreeView1.ItemHeight = 20;
            this.TreeView1.Location = new System.Drawing.Point(3, 36);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(813, 220);
            this.TreeView1.TabIndex = 122;
            this.TreeView1.DoubleClick += new System.EventHandler(this.TreeView1_DoubleClick);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button5.ImageGlyph")));
            this.Button5.ImageGlyphEnabled = true;
            this.Button5.Location = new System.Drawing.Point(175, 6);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(129, 24);
            this.Button5.TabIndex = 120;
            this.Button5.Text = "GitHub issues";
            this.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.button10);
            this.bottom_buttons.Controls.Add(this.button9);
            this.bottom_buttons.Controls.Add(this.label2);
            this.bottom_buttons.Controls.Add(this.button8);
            this.bottom_buttons.Controls.Add(this.button7);
            this.bottom_buttons.Controls.Add(this.AlertBox1);
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 453);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(844, 48);
            this.bottom_buttons.TabIndex = 123;
            this.bottom_buttons.UseDecorationPattern = false;
            this.bottom_buttons.UseSharpStyle = false;
            // 
            // button10
            // 
            this.button10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button10.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.button10.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Image = null;
            this.button10.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Stop;
            this.button10.ImageGlyphEnabled = true;
            this.button10.Location = new System.Drawing.Point(372, 7);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(32, 34);
            this.button10.TabIndex = 127;
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Visible = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button9.CustomColor = System.Drawing.Color.Empty;
            this.button9.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.ErrorOnHover)));
            this.button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = null;
            this.button9.ImageGlyph = null;
            this.button9.ImageGlyphEnabled = false;
            this.button9.Location = new System.Drawing.Point(410, 7);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(139, 34);
            this.button9.TabIndex = 126;
            this.button9.Text = "Save open theme as ...";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 30);
            this.label2.TabIndex = 125;
            this.label2.Text = "Expand details and copy log to report it in GitHub";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.button8.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Image = null;
            this.button8.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Down;
            this.button8.ImageGlyphEnabled = true;
            this.button8.Location = new System.Drawing.Point(8, 7);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(37, 34);
            this.button8.TabIndex = 124;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button7.CustomColor = System.Drawing.Color.Empty;
            this.button7.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.ErrorOnHover)));
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = null;
            this.button7.ImageGlyph = null;
            this.button7.ImageGlyphEnabled = false;
            this.button7.Location = new System.Drawing.Point(555, 7);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(80, 34);
            this.button7.TabIndex = 123;
            this.button7.Text = "SOS";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = true;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(51, 7);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(315, 34);
            this.AlertBox1.TabIndex = 122;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "Switch to Release mode to avoid irreversible errors";
            this.AlertBox1.Visible = false;
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.ErrorOnHover)));
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(723, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(115, 34);
            this.Button1.TabIndex = 116;
            this.Button1.Text = "Try continuing";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
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
            this.Button2.Location = new System.Drawing.Point(641, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(76, 34);
            this.Button2.TabIndex = 117;
            this.Button2.Text = "Exit";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.Maroon;
            this.AnimatedBox1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AnimatedBox1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.AnimatedBox1.Controls.Add(this.label1);
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label7);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(844, 170);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors;
            this.AnimatedBox1.TabIndex = 121;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(820, 50);
            this.label1.TabIndex = 86;
            this.label1.Text = "Oops! An unexpected error occurred, and WinPaletter can\'t complete the operation." +
    " Please try again later or report it on GitHub.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(390, 8);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(64, 64);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(12, 131);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(820, 30);
            this.Label7.TabIndex = 85;
            this.Label7.Text = "0";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BugReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(844, 501);
            this.Controls.Add(this.separatorH1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.AnimatedBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(645, 200);
            this.Name = "BugReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception Error";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BugReport_Load);
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        internal PictureBox PictureBox1;
        internal Label Label7;
        internal UI.WP.GroupBox GroupBox3;
        internal Label Label4;
        internal PictureBox PictureBox4;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal UI.WP.TreeView TreeView1;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal UI.WP.AlertBox AlertBox1;
        private UI.WP.GroupBox bottom_buttons;
        internal UI.WP.Button button7;
        private UI.WP.SeparatorH separatorH1;
        internal Label label1;
        internal UI.WP.Button button8;
        internal Label label2;
        internal UI.WP.Button button9;
        internal UI.WP.Button button10;
    }
}
