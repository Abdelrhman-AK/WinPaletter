using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class TerminalsDashboard : BorderlessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalsDashboard));
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.button7 = new WinPaletter.UI.WP.Button();
            this.toolTip1 = new WinPaletter.UI.WP.ToolTip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelAlt1 = new WinPaletter.UI.WP.LabelAlt();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button5
            // 
            this.Button5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = ((System.Drawing.Image)(resources.GetObject("Button5.Image")));
            this.Button5.ImageGlyph = null;
            this.Button5.ImageGlyphEnabled = false;
            this.Button5.Location = new System.Drawing.Point(179, 41);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(32, 79);
            this.Button5.TabIndex = 95;
            this.Button5.Tag = "Windows Terminal Preview";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            this.Button5.MouseEnter += new System.EventHandler(this.Button1_MouseEnter);
            this.Button5.MouseLeave += new System.EventHandler(this.Button1_MouseLeave);
            // 
            // Button6
            // 
            this.Button6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.ImageGlyph = null;
            this.Button6.ImageGlyphEnabled = false;
            this.Button6.Location = new System.Drawing.Point(141, 41);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(32, 79);
            this.Button6.TabIndex = 94;
            this.Button6.Tag = "Windows Terminal Stable";
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            this.Button6.MouseEnter += new System.EventHandler(this.Button1_MouseEnter);
            this.Button6.MouseLeave += new System.EventHandler(this.Button1_MouseLeave);
            // 
            // Button3
            // 
            this.Button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.ImageGlyph = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.Location = new System.Drawing.Point(103, 41);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(32, 79);
            this.Button3.TabIndex = 91;
            this.Button3.Tag = "PowerShell x64";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            this.Button3.MouseEnter += new System.EventHandler(this.Button1_MouseEnter);
            this.Button3.MouseLeave += new System.EventHandler(this.Button1_MouseLeave);
            // 
            // Button4
            // 
            this.Button4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.ImageGlyph = null;
            this.Button4.ImageGlyphEnabled = false;
            this.Button4.Location = new System.Drawing.Point(65, 41);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(32, 79);
            this.Button4.TabIndex = 90;
            this.Button4.Tag = "PowerShell x86";
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            this.Button4.MouseEnter += new System.EventHandler(this.Button1_MouseEnter);
            this.Button4.MouseLeave += new System.EventHandler(this.Button1_MouseLeave);
            // 
            // Button2
            // 
            this.Button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = ((System.Drawing.Image)(resources.GetObject("Button2.Image")));
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(217, 41);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(32, 79);
            this.Button2.TabIndex = 87;
            this.Button2.Tag = "External Terminal";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            this.Button2.MouseEnter += new System.EventHandler(this.Button1_MouseEnter);
            this.Button2.MouseLeave += new System.EventHandler(this.Button1_MouseLeave);
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(27, 41);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(32, 79);
            this.Button1.TabIndex = 86;
            this.Button1.Tag = "Command Prompt";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            this.Button1.MouseEnter += new System.EventHandler(this.Button1_MouseEnter);
            this.Button1.MouseLeave += new System.EventHandler(this.Button1_MouseLeave);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.button7.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = null;
            this.button7.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("button7.ImageGlyph")));
            this.button7.ImageGlyphEnabled = true;
            this.button7.Location = new System.Drawing.Point(247, 9);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(20, 20);
            this.button7.TabIndex = 103;
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BadgeColor = System.Drawing.Color.Empty;
            this.toolTip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolTip1.Font_Title = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolTip1.Image = null;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.ToolTipText = "It is effective for Windows 10 and Windows 11 (If you have installed Windows Term" +
    "inal from the Store)";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.Button6);
            this.panel1.Controls.Add(this.Button1);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.Button4);
            this.panel1.Controls.Add(this.Button5);
            this.panel1.Controls.Add(this.Button3);
            this.panel1.Controls.Add(this.Button2);
            this.panel1.Controls.Add(this.labelAlt1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 130);
            this.panel1.TabIndex = 106;
            this.panel1.Visible = false;
            // 
            // labelAlt1
            // 
            this.labelAlt1.BackColor = System.Drawing.Color.Transparent;
            this.labelAlt1.DrawOnGlass = true;
            this.labelAlt1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlt1.Location = new System.Drawing.Point(12, 10);
            this.labelAlt1.Name = "labelAlt1";
            this.labelAlt1.Size = new System.Drawing.Size(229, 19);
            this.labelAlt1.TabIndex = 105;
            this.labelAlt1.Text = "0";
            this.labelAlt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerminalsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(277, 130);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TerminalsDashboard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Terminals Dashboard";
            this.Load += new System.EventHandler(this.TerminalsDashboard_Load);
            this.Shown += new System.EventHandler(this.TerminalsDashboard_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal UI.WP.Button button7;
        private UI.WP.ToolTip toolTip1;
        private Panel panel1;
        private UI.WP.LabelAlt labelAlt1;
    }
}
