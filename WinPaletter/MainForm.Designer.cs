using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinPaletter.Tabs;

namespace WinPaletter
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class MainForm : TabsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new WinPaletter.UI.WP.TablessControl();
            this.Status_pnl = new System.Windows.Forms.Panel();
            this.Status_lbl = new System.Windows.Forms.Label();
            this.Status_pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabsContainer1
            // 
            this.tabsContainer1.ForeColor = System.Drawing.Color.White;
            this.tabsContainer1.Size = new System.Drawing.Size(1172, 34);
            this.tabsContainer1.TabControl = this.tabControl1;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl1.ItemSize = new System.Drawing.Size(140, 35);
            this.tabControl1.Location = new System.Drawing.Point(0, 34);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1172, 740);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 121;
            // 
            // Status_pnl
            // 
            this.Status_pnl.BackColor = System.Drawing.Color.Transparent;
            this.Status_pnl.Controls.Add(this.Status_lbl);
            this.Status_pnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Status_pnl.Location = new System.Drawing.Point(0, 774);
            this.Status_pnl.Name = "Status_pnl";
            this.Status_pnl.Size = new System.Drawing.Size(1172, 24);
            this.Status_pnl.TabIndex = 122;
            // 
            // Status_lbl
            // 
            this.Status_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Status_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status_lbl.Location = new System.Drawing.Point(0, 0);
            this.Status_lbl.Name = "Status_lbl";
            this.Status_lbl.Size = new System.Drawing.Size(1172, 24);
            this.Status_lbl.TabIndex = 39;
            this.Status_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Status_lbl.Paint += new System.Windows.Forms.PaintEventHandler(this.Status_lbl_Paint);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1172, 798);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Status_pnl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1172, 798);
            this.Name = "MainForm";
            this.ShowIconAndCaptionText = false;
            this.Text = "WinPaletter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.Controls.SetChildIndex(this.Status_pnl, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Status_pnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private UI.WP.TablessControl tabControl1;
        internal Panel Status_pnl;
        public Label Status_lbl;
    }
}
