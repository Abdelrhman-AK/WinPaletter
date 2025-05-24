using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Updates : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updates));
            this.Label2 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.LinkLabel3 = new WinPaletter.UI.WP.LinkLabel();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton3 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.Label3 = new WinPaletter.UI.WP.LinkLabel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.ProgressBar1 = new WinPaletter.UI.WP.ProgressBar();
            this.AlertBox2 = new WinPaletter.UI.WP.AlertBox();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(174, 79);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(233, 24);
            this.Label2.TabIndex = 16;
            this.Label2.Text = "0";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.Controls.Add(this.LinkLabel3);
            this.Panel1.Controls.Add(this.RadioButton2);
            this.Panel1.Controls.Add(this.RadioButton3);
            this.Panel1.Controls.Add(this.RadioButton1);
            this.Panel1.Location = new System.Drawing.Point(11, 241);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(395, 117);
            this.Panel1.TabIndex = 18;
            this.Panel1.Visible = false;
            // 
            // LinkLabel3
            // 
            this.LinkLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabel3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.LinkLabel3.LinkColor = System.Drawing.Color.DodgerBlue;
            this.LinkLabel3.Location = new System.Drawing.Point(16, 90);
            this.LinkLabel3.Name = "LinkLabel3";
            this.LinkLabel3.Size = new System.Drawing.Size(376, 24);
            this.LinkLabel3.TabIndex = 33;
            this.LinkLabel3.TabStop = true;
            this.LinkLabel3.Text = "• What\'s new?";
            this.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LinkLabel3.Visible = false;
            this.LinkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel3_LinkClicked);
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Location = new System.Drawing.Point(3, 32);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(389, 23);
            this.RadioButton2.TabIndex = 2;
            this.RadioButton2.Text = "Save download as...";
            // 
            // RadioButton3
            // 
            this.RadioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton3.Checked = false;
            this.RadioButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton3.ForeColor = System.Drawing.Color.White;
            this.RadioButton3.Location = new System.Drawing.Point(3, 61);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(389, 23);
            this.RadioButton3.TabIndex = 1;
            this.RadioButton3.Text = "Just download from the browser";
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton1.Checked = true;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Location = new System.Drawing.Point(3, 3);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(389, 23);
            this.RadioButton1.TabIndex = 0;
            this.RadioButton1.Text = "Download then close WinPaletter and replace it by the new update";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(42, 78);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(126, 24);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "Current version:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(12, 78);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.TabIndex = 21;
            this.PictureBox2.TabStop = false;
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(12, 108);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.TabIndex = 27;
            this.PictureBox4.TabStop = false;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(42, 108);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(126, 24);
            this.Label6.TabIndex = 26;
            this.Label6.Text = "Update size:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(174, 109);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(233, 24);
            this.Label7.TabIndex = 25;
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(12, 138);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.TabIndex = 30;
            this.PictureBox5.TabStop = false;
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(42, 138);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(126, 24);
            this.Label8.TabIndex = 29;
            this.Label8.Text = "Release date:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(174, 139);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(233, 24);
            this.Label9.TabIndex = 28;
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.Transparent;
            this.AnimatedBox1.Color1 = System.Drawing.Color.DodgerBlue;
            this.AnimatedBox1.Color2 = System.Drawing.Color.Crimson;
            this.AnimatedBox1.Controls.Add(this.Label3);
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label17);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(421, 68);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.SwapColors;
            this.AnimatedBox1.TabIndex = 32;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Label3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.Label3.LinkColor = System.Drawing.Color.DodgerBlue;
            this.Label3.Location = new System.Drawing.Point(316, 22);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(93, 24);
            this.Label3.TabIndex = 17;
            this.Label3.TabStop = true;
            this.Label3.Text = "0";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Label3_LinkClicked);
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
            // Label17
            // 
            this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(65, 10);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(245, 48);
            this.Label17.TabIndex = 9;
            this.Label17.Text = "Updates";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Checked = true;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(12, 168);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(395, 23);
            this.CheckBox1.TabIndex = 31;
            this.CheckBox1.Text = "Automatic check for updates";
            this.CheckBox1.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox1_CheckedChanged);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Appearance = WinPaletter.UI.WP.ProgressBar.ProgressBarAppearance.Bar;
            this.ProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBar1.Location = new System.Drawing.Point(11, 365);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(395, 10);
            this.ProgressBar1.State = WinPaletter.UI.WP.ProgressBar.ProgressBarState.Normal;
            this.ProgressBar1.Style = WinPaletter.UI.WP.ProgressBar.ProgressBarStyle.Continuous;
            this.ProgressBar1.TabIndex = 3;
            this.ProgressBar1.TaskbarBroadcast = true;
            this.ProgressBar1.Visible = false;
            // 
            // AlertBox2
            // 
            this.AlertBox2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.AlertBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox2.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox2.CenterText = true;
            this.AlertBox2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlertBox2.Image = null;
            this.AlertBox2.Location = new System.Drawing.Point(12, 205);
            this.AlertBox2.Name = "AlertBox2";
            this.AlertBox2.Size = new System.Drawing.Size(395, 31);
            this.AlertBox2.TabIndex = 12;
            this.AlertBox2.TabStop = false;
            this.AlertBox2.Text = "Update available";
            this.AlertBox2.Visible = false;
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 198);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(395, 1);
            this.Separator1.TabIndex = 8;
            this.Separator1.TabStop = false;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 383);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(421, 48);
            this.bottom_buttons.TabIndex = 212;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.ErrorOnHover)));
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.ImageGlyph = null;
            this.Button1.Location = new System.Drawing.Point(254, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(161, 34);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Check for updates";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
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
            this.Button3.Location = new System.Drawing.Point(168, 7);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(80, 34);
            this.Button3.TabIndex = 2;
            this.Button3.Text = "Cancel";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Updates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(421, 431);
            this.Controls.Add(this.AnimatedBox1);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.PictureBox5);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.PictureBox4);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.AlertBox2);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updates";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Updates_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Updates_FormClosed);
            this.Load += new System.EventHandler(this.Updates_Load);
            this.Shown += new System.EventHandler(this.Updates_Shown);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal UI.WP.Button Button1;
        internal UI.WP.Button Button3;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.AlertBox AlertBox2;
        internal Label Label2;
        internal UI.WP.LinkLabel Label3;
        internal Panel Panel1;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.WP.RadioButton RadioButton1;
        internal UI.WP.ProgressBar ProgressBar1;
        internal Label Label1;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox4;
        internal Label Label6;
        internal Label Label7;
        internal PictureBox PictureBox5;
        internal Label Label8;
        internal Label Label9;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.LinkLabel LinkLabel3;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal PictureBox PictureBox1;
        internal Label Label17;
        private UI.WP.GroupBox bottom_buttons;
    }
}
