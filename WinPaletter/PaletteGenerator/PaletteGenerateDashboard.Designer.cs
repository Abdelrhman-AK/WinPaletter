using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class PaletteGenerateDashboard : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerateDashboard));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.SuspendLayout();
            // 
            // ToolTip1
            // 
            this.ToolTip1.BackColor = System.Drawing.Color.Black;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.DrawOnGlass = false;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.Location = new System.Drawing.Point(5, 45);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(230, 32);
            this.Button4.TabIndex = 90;
            this.Button4.Text = "Generate a palette from a color";
            this.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.DrawOnGlass = false;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(5, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(230, 32);
            this.Button1.TabIndex = 86;
            this.Button1.Text = "Generate a palette from an image";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // PaletteGenerateDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(240, 83);
            this.ControlBox = false;
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteGenerateDashboard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Terminals Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubMenu_FormClosing);
            this.Load += new System.EventHandler(this.TerminalsDashboard_Load);
            this.Shown += new System.EventHandler(this.TerminalsDashboard_Shown);
            this.ResumeLayout(false);

        }
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button4;
        internal ToolTip ToolTip1;
    }
}