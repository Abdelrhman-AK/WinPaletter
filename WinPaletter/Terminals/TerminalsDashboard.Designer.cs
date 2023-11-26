using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class TerminalsDashboard : Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalsDashboard));
            this.Label49 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Separator3 = new WinPaletter.UI.WP.SeparatorH();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.SeparatorVertical1 = new WinPaletter.UI.WP.SeparatorV();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label49
            // 
            this.Label49.BackColor = System.Drawing.Color.Transparent;
            this.Label49.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label49.Location = new System.Drawing.Point(6, 7);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(149, 19);
            this.Label49.TabIndex = 84;
            this.Label49.Text = "Consoles:";
            this.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(170, 7);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(116, 19);
            this.Label2.TabIndex = 92;
            this.Label2.Text = "Windows Terminal:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(304, 7);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(18, 18);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 97;
            this.PictureBox1.TabStop = false;
            this.ToolTip1.SetToolTip(this.PictureBox1, "It is effective for Windows 10 and Windows 11 (If you have installed Windows Term" +
        "inal from the Store)");
            // 
            // Button5
            // 
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = ((System.Drawing.Image)(resources.GetObject("Button5.Image")));
            this.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button5.Location = new System.Drawing.Point(173, 68);
            this.Button5.Name = "Button5";
            this.Button5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Button5.Size = new System.Drawing.Size(149, 27);
            this.Button5.TabIndex = 95;
            this.Button5.Text = "Preview";
            this.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button6
            // 
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button6.Location = new System.Drawing.Point(173, 37);
            this.Button6.Name = "Button6";
            this.Button6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Button6.Size = new System.Drawing.Size(149, 27);
            this.Button6.TabIndex = 94;
            this.Button6.Text = "Stable";
            this.Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Separator3
            // 
            this.Separator3.AlternativeLook = false;
            this.Separator3.BackColor = System.Drawing.Color.Transparent;
            this.Separator3.Location = new System.Drawing.Point(173, 30);
            this.Separator3.Name = "Separator3";
            this.Separator3.Size = new System.Drawing.Size(149, 1);
            this.Separator3.TabIndex = 93;
            this.Separator3.TabStop = false;
            this.Separator3.Text = "Separator3";
            // 
            // Button3
            // 
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button3.Location = new System.Drawing.Point(7, 98);
            this.Button3.Name = "Button3";
            this.Button3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Button3.Size = new System.Drawing.Size(149, 27);
            this.Button3.TabIndex = 91;
            this.Button3.Text = "PowerShell x64";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button4
            // 
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button4.Location = new System.Drawing.Point(7, 67);
            this.Button4.Name = "Button4";
            this.Button4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Button4.Size = new System.Drawing.Size(149, 27);
            this.Button4.TabIndex = 90;
            this.Button4.Text = "PowerShell x86";
            this.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button2
            // 
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = ((System.Drawing.Image)(resources.GetObject("Button2.Image")));
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button2.Location = new System.Drawing.Point(7, 129);
            this.Button2.Name = "Button2";
            this.Button2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Button2.Size = new System.Drawing.Size(149, 27);
            this.Button2.TabIndex = 87;
            this.Button2.Text = "External";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.Location = new System.Drawing.Point(7, 36);
            this.Button1.Name = "Button1";
            this.Button1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Button1.Size = new System.Drawing.Size(149, 27);
            this.Button1.TabIndex = 86;
            this.Button1.Text = "Command Prompt";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(7, 30);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(149, 1);
            this.Separator1.TabIndex = 85;
            this.Separator1.TabStop = false;
            // 
            // SeparatorVertical1
            // 
            this.SeparatorVertical1.AlternativeLook = false;
            this.SeparatorVertical1.BackColor = System.Drawing.Color.Transparent;
            this.SeparatorVertical1.Location = new System.Drawing.Point(162, 7);
            this.SeparatorVertical1.Name = "SeparatorVertical1";
            this.SeparatorVertical1.Size = new System.Drawing.Size(1, 149);
            this.SeparatorVertical1.TabIndex = 102;
            this.SeparatorVertical1.TabStop = false;
            // 
            // TerminalsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(331, 163);
            this.ControlBox = false;
            this.Controls.Add(this.SeparatorVertical1);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Separator3);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.Label49);
            this.Controls.Add(this.Label2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TerminalsDashboard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Terminals Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubMenu_FormClosing);
            this.Load += new System.EventHandler(this.TerminalsDashboard_Load);
            this.Shown += new System.EventHandler(this.TerminalsDashboard_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        internal Label Label49;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal UI.WP.SeparatorH Separator3;
        internal Label Label2;
        internal PictureBox PictureBox1;
        internal ToolTip ToolTip1;
        internal UI.WP.SeparatorV SeparatorVertical1;
    }
}
