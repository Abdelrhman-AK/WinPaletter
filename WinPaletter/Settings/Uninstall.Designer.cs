using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Uninstall : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uninstall));
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.CheckBox2 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.RadioImage1 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage3 = new WinPaletter.UI.WP.RadioImage();
            this.Label4 = new System.Windows.Forms.Label();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.AlertBox4 = new WinPaletter.UI.WP.AlertBox();
            this.CheckBox3 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.AlertBox2 = new WinPaletter.UI.WP.AlertBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.AnimatedBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(89, 47);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(603, 24);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "This wizard will help you delete WinPaletter\'s data made during your usage";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label1.Location = new System.Drawing.Point(89, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(445, 35);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "WinPaletter uninstaller";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(80, 80);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(12, 94);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(421, 28);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Choose operations to be done:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(29, 125);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(24, 24);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 7;
            this.PictureBox6.TabStop = false;
            // 
            // CheckBox1
            // 
            this.CheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox1.Checked = true;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(59, 125);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(178, 24);
            this.CheckBox1.TabIndex = 8;
            this.CheckBox1.Text = "Delete registry associations";
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(29, 185);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 9;
            this.PictureBox2.TabStop = false;
            // 
            // CheckBox2
            // 
            this.CheckBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox2.Checked = true;
            this.CheckBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox2.ForeColor = System.Drawing.Color.White;
            this.CheckBox2.Location = new System.Drawing.Point(59, 185);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(624, 24);
            this.CheckBox2.TabIndex = 10;
            this.CheckBox2.Text = "Delete application data: %localappdata%\\Abdelrhman-AK\\WinPaletter and %ProgramFil" +
    "es%\\WinPaletter";
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(29, 292);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 11;
            this.PictureBox3.TabStop = false;
            // 
            // RadioImage1
            // 
            this.RadioImage1.Checked = true;
            this.RadioImage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage1.ForeColor = System.Drawing.Color.White;
            this.RadioImage1.Image = null;
            this.RadioImage1.ImageWithText = false;
            this.RadioImage1.Location = new System.Drawing.Point(156, 292);
            this.RadioImage1.Name = "RadioImage1";
            this.RadioImage1.ShowText = true;
            this.RadioImage1.Size = new System.Drawing.Size(280, 24);
            this.RadioImage1.TabIndex = 12;
            this.RadioImage1.Text = "Do nothing";
            this.RadioImage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioImage2
            // 
            this.RadioImage2.Checked = false;
            this.RadioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage2.ForeColor = System.Drawing.Color.White;
            this.RadioImage2.Image = null;
            this.RadioImage2.ImageWithText = false;
            this.RadioImage2.Location = new System.Drawing.Point(156, 322);
            this.RadioImage2.Name = "RadioImage2";
            this.RadioImage2.ShowText = true;
            this.RadioImage2.Size = new System.Drawing.Size(280, 24);
            this.RadioImage2.TabIndex = 13;
            this.RadioImage2.Text = "Restore from a theme file you backed-up before";
            this.RadioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioImage3
            // 
            this.RadioImage3.Checked = false;
            this.RadioImage3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage3.ForeColor = System.Drawing.Color.White;
            this.RadioImage3.Image = null;
            this.RadioImage3.ImageWithText = false;
            this.RadioImage3.Location = new System.Drawing.Point(156, 352);
            this.RadioImage3.Name = "RadioImage3";
            this.RadioImage3.ShowText = true;
            this.RadioImage3.Size = new System.Drawing.Size(280, 24);
            this.RadioImage3.TabIndex = 14;
            this.RadioImage3.Text = "Restore to default Windows";
            this.RadioImage3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(59, 292);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(91, 24);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "Theme restore:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 387);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(671, 1);
            this.Separator1.TabIndex = 16;
            this.Separator1.TabStop = false;
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.DrawOnGlass = false;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.Location = new System.Drawing.Point(578, 400);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(105, 34);
            this.Button6.TabIndex = 21;
            this.Button6.Text = "Uninstall";
            this.Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.DrawOnGlass = false;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.Location = new System.Drawing.Point(492, 400);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 22;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // AlertBox4
            // 
            this.AlertBox4.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox4.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox4.CenterText = false;
            this.AlertBox4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox4.Image = null;
            this.AlertBox4.Location = new System.Drawing.Point(59, 215);
            this.AlertBox4.Name = "AlertBox4";
            this.AlertBox4.Size = new System.Drawing.Size(624, 22);
            this.AlertBox4.TabIndex = 23;
            this.AlertBox4.TabStop = false;
            this.AlertBox4.Text = "Note: This will reset cursors to Aero as the custom cursors are rendered and save" +
    "d there";
            // 
            // CheckBox3
            // 
            this.CheckBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox3.Checked = true;
            this.CheckBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox3.ForeColor = System.Drawing.Color.White;
            this.CheckBox3.Location = new System.Drawing.Point(59, 155);
            this.CheckBox3.Name = "CheckBox3";
            this.CheckBox3.Size = new System.Drawing.Size(188, 24);
            this.CheckBox3.TabIndex = 25;
            this.CheckBox3.Text = "Delete WinPaletter\'s settings";
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(29, 155);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 24;
            this.PictureBox4.TabStop = false;
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = true;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(12, 405);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(474, 24);
            this.AlertBox1.TabIndex = 26;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "WinPaletter won\'t be able to delete itself, so delete the exe file after uninstal" +
    "l";
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.Transparent;
            this.AnimatedBox1.Color1 = System.Drawing.Color.Maroon;
            this.AnimatedBox1.Color2 = System.Drawing.Color.Crimson;
            this.AnimatedBox1.Controls.Add(this.Label2);
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label1);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(695, 86);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors;
            this.AnimatedBox1.TabIndex = 27;
            // 
            // AlertBox2
            // 
            this.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox2.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox2.CenterText = false;
            this.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox2.Image = null;
            this.AlertBox2.Location = new System.Drawing.Point(59, 243);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(624, 40);
            this.AlertBox2.TabIndex = 28;
            this.AlertBox2.TabStop = false;
            this.AlertBox2.Text = "Note: Windows (Vista and later) Startup backup wave file is located there. If you" +
    " delete this folder, you won\'t be able to restore this sound. Restore this sound" +
    " first then you can do an uninstall.";
            // 
            // Uninstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(695, 446);
            this.Controls.Add(this.AlertBox2);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.CheckBox3);
            this.Controls.Add(this.PictureBox4);
            this.Controls.Add(this.AlertBox4);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.RadioImage3);
            this.Controls.Add(this.RadioImage2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.RadioImage1);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.PictureBox3);
            this.Controls.Add(this.PictureBox6);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.AnimatedBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Uninstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Uninstall";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Uninstall_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.AnimatedBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal PictureBox PictureBox1;
        internal Label Label2;
        internal Label Label1;
        internal Label Label3;
        internal PictureBox PictureBox6;
        internal UI.WP.CheckBox CheckBox1;
        internal PictureBox PictureBox2;
        internal UI.WP.CheckBox CheckBox2;
        internal PictureBox PictureBox3;
        internal UI.WP.RadioImage RadioImage1;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.WP.RadioImage RadioImage3;
        internal Label Label4;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button2;
        internal UI.WP.AlertBox AlertBox4;
        internal UI.WP.CheckBox CheckBox3;
        internal PictureBox PictureBox4;
        internal UI.WP.AlertBox AlertBox1;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal UI.WP.AlertBox AlertBox2;
    }
}