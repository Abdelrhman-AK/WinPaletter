using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_DownloadProgress : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_DownloadProgress));
            this.ProgressBar1 = new WinPaletter.UI.WP.ProgressBar();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar1.Appearance = WinPaletter.UI.WP.ProgressBar.ProgressBarAppearance.Bar;
            this.ProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBar1.Location = new System.Drawing.Point(12, 166);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(652, 20);
            this.ProgressBar1.State = WinPaletter.UI.WP.ProgressBar.ProgressBarState.Normal;
            this.ProgressBar1.Style = WinPaletter.UI.WP.ProgressBar.ProgressBarStyle.Continuous;
            this.ProgressBar1.TabIndex = 0;
            this.ProgressBar1.TaskbarBroadcast = true;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Location = new System.Drawing.Point(165, 74);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(511, 24);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "0";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Location = new System.Drawing.Point(165, 104);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(511, 24);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "0";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.Location = new System.Drawing.Point(165, 134);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(511, 24);
            this.Label4.TabIndex = 4;
            this.Label4.Text = "0";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(42, 134);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(117, 24);
            this.Label5.TabIndex = 7;
            this.Label5.Text = "Remaining time:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(42, 104);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(117, 24);
            this.Label6.TabIndex = 6;
            this.Label6.Text = "Download speed:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(42, 74);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(117, 24);
            this.Label7.TabIndex = 5;
            this.Label7.Text = "Size:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(12, 74);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.TabIndex = 34;
            this.PictureBox2.TabStop = false;
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(12, 104);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.TabIndex = 35;
            this.PictureBox3.TabStop = false;
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(12, 134);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.TabIndex = 36;
            this.PictureBox4.TabStop = false;
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.ImageGlyph = null;
            this.Button3.Location = new System.Drawing.Point(589, 7);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(80, 34);
            this.Button3.TabIndex = 37;
            this.Button3.Text = "Cancel";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.Transparent;
            this.AnimatedBox1.Color1 = System.Drawing.Color.DodgerBlue;
            this.AnimatedBox1.Color2 = System.Drawing.Color.Crimson;
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label1);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(676, 68);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.SwapColors;
            this.AnimatedBox1.TabIndex = 33;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(10, 10);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(48, 48);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 8;
            this.PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(65, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(599, 48);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "0";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 203);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(676, 48);
            this.bottom_buttons.TabIndex = 211;
            // 
            // Store_DownloadProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(676, 251);
            this.ControlBox = false;
            this.Controls.Add(this.PictureBox4);
            this.Controls.Add(this.PictureBox3);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.AnimatedBox1);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Store_DownloadProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Downloading theme resources pack";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Store_DownloadProgress_FormClosed);
            this.Load += new System.EventHandler(this.Store_DownloadProgress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal UI.WP.ProgressBar ProgressBar1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal Label Label5;
        internal Label Label6;
        internal Label Label7;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal PictureBox PictureBox1;
        internal Label Label1;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox3;
        internal PictureBox PictureBox4;
        internal UI.WP.Button Button3;
        private UI.WP.GroupBox bottom_buttons;
    }
}
