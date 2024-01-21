using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Saving_ex_list : Form
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
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.alertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.ErrorOnHover)));
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageAsVector = false;
            this.Button1.ImageVector = null;
            this.Button1.Location = new System.Drawing.Point(742, 415);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 34);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Continue";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageAsVector = false;
            this.Button2.ImageVector = null;
            this.Button2.Location = new System.Drawing.Point(446, 415);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(290, 34);
            this.Button2.TabIndex = 2;
            this.Button2.Text = "Elicit selected error (Show exception error details)";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TreeView1
            // 
            this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView1.ForeColor = System.Drawing.Color.White;
            this.TreeView1.FullRowSelect = true;
            this.TreeView1.Indent = 17;
            this.TreeView1.ItemHeight = 28;
            this.TreeView1.Location = new System.Drawing.Point(12, 13);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.ShowLines = false;
            this.TreeView1.Size = new System.Drawing.Size(810, 392);
            this.TreeView1.TabIndex = 6;
            // 
            // alertBox1
            // 
            this.alertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Success;
            this.alertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertBox1.BackColor = System.Drawing.Color.Transparent;
            this.alertBox1.CenterText = false;
            this.alertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.alertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.alertBox1.Image = null;
            this.alertBox1.Location = new System.Drawing.Point(12, 415);
            this.alertBox1.Name = "alertBox1";
            this.alertBox1.Size = new System.Drawing.Size(428, 34);
            this.alertBox1.TabIndex = 7;
            this.alertBox1.TabStop = false;
            this.alertBox1.Text = "You can continue, the theme has been loaded without these elements";
            // 
            // Saving_ex_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.alertBox1);
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimizeBox = false;
            this.Name = "Saving_ex_list";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Errors";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ThemeApply_list_Load);
            this.ResumeLayout(false);

        }
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal TreeView TreeView1;
        private UI.WP.AlertBox alertBox1;
    }
}
