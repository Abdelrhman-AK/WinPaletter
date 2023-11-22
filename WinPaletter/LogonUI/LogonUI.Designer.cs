using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUI : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI));
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.GroupBox21 = new WinPaletter.UI.WP.GroupBox();
            this.LogonUI_Lockscreen_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox22 = new System.Windows.Forms.PictureBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.GroupBox19 = new WinPaletter.UI.WP.GroupBox();
            this.LogonUI_Background_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.GroupBox17 = new WinPaletter.UI.WP.GroupBox();
            this.LogonUI_Acrylic_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox15 = new System.Windows.Forms.PictureBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.GroupBox3.SuspendLayout();
            this.GroupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).BeginInit();
            this.GroupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            this.GroupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            this.GroupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.GroupBox21);
            this.GroupBox3.Controls.Add(this.GroupBox19);
            this.GroupBox3.Controls.Add(this.GroupBox17);
            this.GroupBox3.Controls.Add(this.PictureBox6);
            this.GroupBox3.Controls.Add(this.Label13);
            this.GroupBox3.Location = new System.Drawing.Point(12, 57);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(495, 135);
            this.GroupBox3.TabIndex = 15;
            // 
            // GroupBox21
            // 
            this.GroupBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox21.Controls.Add(this.LogonUI_Lockscreen_Toggle);
            this.GroupBox21.Controls.Add(this.PictureBox22);
            this.GroupBox21.Controls.Add(this.Label20);
            this.GroupBox21.Location = new System.Drawing.Point(3, 103);
            this.GroupBox21.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox21.Name = "GroupBox21";
            this.GroupBox21.Size = new System.Drawing.Size(489, 29);
            this.GroupBox21.TabIndex = 12;
            // 
            // LogonUI_Lockscreen_Toggle
            // 
            this.LogonUI_Lockscreen_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogonUI_Lockscreen_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.LogonUI_Lockscreen_Toggle.Checked = false;
            this.LogonUI_Lockscreen_Toggle.DarkLight_Toggler = false;
            this.LogonUI_Lockscreen_Toggle.Location = new System.Drawing.Point(445, 4);
            this.LogonUI_Lockscreen_Toggle.Name = "LogonUI_Lockscreen_Toggle";
            this.LogonUI_Lockscreen_Toggle.Size = new System.Drawing.Size(40, 20);
            this.LogonUI_Lockscreen_Toggle.TabIndex = 16;
            // 
            // PictureBox22
            // 
            this.PictureBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox22.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox22.Image")));
            this.PictureBox22.Location = new System.Drawing.Point(3, 1);
            this.PictureBox22.Name = "PictureBox22";
            this.PictureBox22.Size = new System.Drawing.Size(30, 27);
            this.PictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox22.TabIndex = 4;
            this.PictureBox22.TabStop = false;
            // 
            // Label20
            // 
            this.Label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(36, 2);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(396, 24);
            this.Label20.TabIndex = 13;
            this.Label20.Text = "Lockscreen";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox19
            // 
            this.GroupBox19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox19.Controls.Add(this.LogonUI_Background_Toggle);
            this.GroupBox19.Controls.Add(this.PictureBox16);
            this.GroupBox19.Controls.Add(this.Label18);
            this.GroupBox19.Location = new System.Drawing.Point(3, 72);
            this.GroupBox19.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox19.Name = "GroupBox19";
            this.GroupBox19.Size = new System.Drawing.Size(489, 29);
            this.GroupBox19.TabIndex = 11;
            // 
            // LogonUI_Background_Toggle
            // 
            this.LogonUI_Background_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogonUI_Background_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.LogonUI_Background_Toggle.Checked = false;
            this.LogonUI_Background_Toggle.DarkLight_Toggler = false;
            this.LogonUI_Background_Toggle.Location = new System.Drawing.Point(445, 4);
            this.LogonUI_Background_Toggle.Name = "LogonUI_Background_Toggle";
            this.LogonUI_Background_Toggle.Size = new System.Drawing.Size(40, 20);
            this.LogonUI_Background_Toggle.TabIndex = 16;
            // 
            // PictureBox16
            // 
            this.PictureBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox16.Image")));
            this.PictureBox16.Location = new System.Drawing.Point(3, 1);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(30, 27);
            this.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox16.TabIndex = 4;
            this.PictureBox16.TabStop = false;
            // 
            // Label18
            // 
            this.Label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(36, 2);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(396, 24);
            this.Label18.TabIndex = 13;
            this.Label18.Text = "LogonUI background";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox17
            // 
            this.GroupBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox17.Controls.Add(this.LogonUI_Acrylic_Toggle);
            this.GroupBox17.Controls.Add(this.PictureBox15);
            this.GroupBox17.Controls.Add(this.Label16);
            this.GroupBox17.Location = new System.Drawing.Point(3, 41);
            this.GroupBox17.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox17.Name = "GroupBox17";
            this.GroupBox17.Size = new System.Drawing.Size(489, 29);
            this.GroupBox17.TabIndex = 10;
            // 
            // LogonUI_Acrylic_Toggle
            // 
            this.LogonUI_Acrylic_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogonUI_Acrylic_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.LogonUI_Acrylic_Toggle.Checked = false;
            this.LogonUI_Acrylic_Toggle.DarkLight_Toggler = false;
            this.LogonUI_Acrylic_Toggle.Location = new System.Drawing.Point(445, 4);
            this.LogonUI_Acrylic_Toggle.Name = "LogonUI_Acrylic_Toggle";
            this.LogonUI_Acrylic_Toggle.Size = new System.Drawing.Size(40, 20);
            this.LogonUI_Acrylic_Toggle.TabIndex = 16;
            // 
            // PictureBox15
            // 
            this.PictureBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox15.Image")));
            this.PictureBox15.Location = new System.Drawing.Point(3, 1);
            this.PictureBox15.Name = "PictureBox15";
            this.PictureBox15.Size = new System.Drawing.Size(30, 27);
            this.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox15.TabIndex = 4;
            this.PictureBox15.TabStop = false;
            // 
            // Label16
            // 
            this.Label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(36, 2);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(396, 24);
            this.Label16.TabIndex = 13;
            this.Label16.Text = "Acrylic";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(3, 3);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(35, 35);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 1;
            this.PictureBox6.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(44, 3);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(198, 35);
            this.Label13.TabIndex = 0;
            this.Label13.Text = "LogonUI and LockScreen";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.Button2.Location = new System.Drawing.Point(241, 215);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 17;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 204);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(495, 1);
            this.Separator1.TabIndex = 18;
            this.Separator1.TabStop = false;
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.Button9);
            this.GroupBox12.Controls.Add(this.Label12);
            this.GroupBox12.Controls.Add(this.Button11);
            this.GroupBox12.Controls.Add(this.Button12);
            this.GroupBox12.Location = new System.Drawing.Point(12, 12);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(495, 39);
            this.GroupBox12.TabIndex = 201;
            // 
            // Button9
            // 
            this.Button9.CustomColor = System.Drawing.Color.Empty;
            this.Button9.DrawOnGlass = false;
            this.Button9.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.Location = new System.Drawing.Point(222, 5);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(126, 29);
            this.Button9.TabIndex = 112;
            this.Button9.Text = "Current applied";
            this.Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(4, 4);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(75, 31);
            this.Label12.TabIndex = 111;
            this.Label12.Text = "Open from:";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            this.Button11.CustomColor = System.Drawing.Color.Empty;
            this.Button11.DrawOnGlass = false;
            this.Button11.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = ((System.Drawing.Image)(resources.GetObject("Button11.Image")));
            this.Button11.Location = new System.Drawing.Point(85, 5);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(135, 29);
            this.Button11.TabIndex = 110;
            this.Button11.Text = "WinPaletter theme";
            this.Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button12
            // 
            this.Button12.CustomColor = System.Drawing.Color.Empty;
            this.Button12.DrawOnGlass = false;
            this.Button12.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = null;
            this.Button12.Location = new System.Drawing.Point(351, 5);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(135, 29);
            this.Button12.TabIndex = 108;
            this.Button12.Text = "Default Windows";
            this.Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.DrawOnGlass = false;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(327, 215);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(180, 34);
            this.Button1.TabIndex = 202;
            this.Button1.Text = "Load into current theme";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // LogonUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(519, 261);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.GroupBox12);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.GroupBox3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogonUI";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.LogonUI_Load);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).EndInit();
            this.GroupBox19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            this.GroupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            this.GroupBox12.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.GroupBox GroupBox21;
        internal UI.WP.Toggle LogonUI_Lockscreen_Toggle;
        internal PictureBox PictureBox22;
        internal Label Label20;
        internal UI.WP.GroupBox GroupBox19;
        internal UI.WP.Toggle LogonUI_Background_Toggle;
        internal PictureBox PictureBox16;
        internal Label Label18;
        internal UI.WP.GroupBox GroupBox17;
        internal UI.WP.Toggle LogonUI_Acrylic_Toggle;
        internal PictureBox PictureBox15;
        internal Label Label16;
        internal PictureBox PictureBox6;
        internal Label Label13;
        internal UI.WP.Button Button2;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button1;
    }
}