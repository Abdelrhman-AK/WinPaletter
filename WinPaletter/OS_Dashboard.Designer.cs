using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class OS_Dashboard : BorderlessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OS_Dashboard));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioImage6 = new WinPaletter.UI.WP.RadioImage();
            this.radioImage5 = new WinPaletter.UI.WP.RadioImage();
            this.button7 = new WinPaletter.UI.WP.Button();
            this.radioImage4 = new WinPaletter.UI.WP.RadioImage();
            this.radioImage3 = new WinPaletter.UI.WP.RadioImage();
            this.radioImage1 = new WinPaletter.UI.WP.RadioImage();
            this.radioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.radioImage7 = new WinPaletter.UI.WP.RadioImage();
            this.radioImage8 = new WinPaletter.UI.WP.RadioImage();
            this.SuspendLayout();
            // 
            // radioImage6
            // 
            this.radioImage6.Checked = false;
            this.radioImage6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage6.ForeColor = System.Drawing.Color.White;
            this.radioImage6.Image = ((System.Drawing.Image)(Assets.WinLogos.Win11));
            this.radioImage6.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage6.Location = new System.Drawing.Point(386, 39);
            this.radioImage6.Name = "radioImage6";
            this.radioImage6.Size = new System.Drawing.Size(57, 68);
            this.radioImage6.TabIndex = 109;
            this.radioImage6.Text = "11";
            this.radioImage6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radioImage5
            // 
            this.radioImage5.Checked = false;
            this.radioImage5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage5.ForeColor = System.Drawing.Color.White;
            this.radioImage5.Image = ((System.Drawing.Image)(Assets.WinLogos.Win10));
            this.radioImage5.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage5.Location = new System.Drawing.Point(323, 39);
            this.radioImage5.Name = "radioImage5";
            this.radioImage5.Size = new System.Drawing.Size(57, 68);
            this.radioImage5.TabIndex = 108;
            this.radioImage5.Text = "10";
            this.radioImage5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.button7.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button7.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = null;
            this.button7.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("button7.ImageGlyph")));
            this.button7.ImageGlyphEnabled = true;
            this.button7.Location = new System.Drawing.Point(485, 10);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(20, 20);
            this.button7.TabIndex = 103;
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // radioImage4
            // 
            this.radioImage4.Checked = false;
            this.radioImage4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage4.ForeColor = System.Drawing.Color.White;
            this.radioImage4.Image = ((System.Drawing.Image)(Assets.WinLogos.Win8_1));
            this.radioImage4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage4.Location = new System.Drawing.Point(260, 39);
            this.radioImage4.Name = "radioImage4";
            this.radioImage4.Size = new System.Drawing.Size(57, 68);
            this.radioImage4.TabIndex = 107;
            this.radioImage4.Text = "8.1";
            this.radioImage4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radioImage3
            // 
            this.radioImage3.Checked = false;
            this.radioImage3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage3.ForeColor = System.Drawing.Color.White;
            this.radioImage3.Image = ((System.Drawing.Image)(Assets.WinLogos.Win7));
            this.radioImage3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage3.Location = new System.Drawing.Point(134, 39);
            this.radioImage3.Name = "radioImage3";
            this.radioImage3.Size = new System.Drawing.Size(57, 68);
            this.radioImage3.TabIndex = 106;
            this.radioImage3.Text = "7";
            this.radioImage3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radioImage1
            // 
            this.radioImage1.Checked = false;
            this.radioImage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage1.ForeColor = System.Drawing.Color.White;
            this.radioImage1.Image = ((System.Drawing.Image)(Assets.WinLogos.WinXP));
            this.radioImage1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage1.Location = new System.Drawing.Point(8, 39);
            this.radioImage1.Name = "radioImage1";
            this.radioImage1.Size = new System.Drawing.Size(57, 68);
            this.radioImage1.TabIndex = 104;
            this.radioImage1.Text = "XP";
            this.radioImage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radioImage2
            // 
            this.radioImage2.Checked = false;
            this.radioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage2.ForeColor = System.Drawing.Color.White;
            this.radioImage2.Image = ((System.Drawing.Image)(Assets.WinLogos.WinVista));
            this.radioImage2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage2.Location = new System.Drawing.Point(71, 39);
            this.radioImage2.Name = "radioImage2";
            this.radioImage2.Size = new System.Drawing.Size(57, 68);
            this.radioImage2.TabIndex = 105;
            this.radioImage2.Text = "Vista";
            this.radioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radioImage7
            // 
            this.radioImage7.Checked = false;
            this.radioImage7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage7.ForeColor = System.Drawing.Color.White;
            this.radioImage7.Image = ((System.Drawing.Image)(Assets.WinLogos.Win12));
            this.radioImage7.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage7.Location = new System.Drawing.Point(449, 39);
            this.radioImage7.Name = "radioImage7";
            this.radioImage7.Size = new System.Drawing.Size(57, 68);
            this.radioImage7.TabIndex = 110;
            this.radioImage7.Text = "12*";
            this.radioImage7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radioImage8
            // 
            this.radioImage8.Checked = false;
            this.radioImage8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage8.ForeColor = System.Drawing.Color.White;
            this.radioImage8.Image = ((System.Drawing.Image)(Assets.WinLogos.Win8));
            this.radioImage8.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage8.Location = new System.Drawing.Point(197, 39);
            this.radioImage8.Name = "radioImage8";
            this.radioImage8.Size = new System.Drawing.Size(57, 68);
            this.radioImage8.TabIndex = 111;
            this.radioImage8.Text = "8";
            this.radioImage8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // OS_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CancelButton = this.button7;
            this.ClientSize = new System.Drawing.Size(513, 115);
            this.CloseOnLostFocus = false;
            this.ControlBox = false;
            this.Controls.Add(this.radioImage8);
            this.Controls.Add(this.radioImage7);
            this.Controls.Add(this.radioImage6);
            this.Controls.Add(this.radioImage5);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.radioImage4);
            this.Controls.Add(this.radioImage3);
            this.Controls.Add(this.radioImage1);
            this.Controls.Add(this.radioImage2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OS_Dashboard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "OS Preview selection";
            this.Load += new System.EventHandler(this.OS_Dashboard_Load);
            this.ResumeLayout(false);

        }
        internal ToolTip ToolTip1;
        internal UI.WP.Button button7;
        private UI.WP.RadioImage radioImage6;
        private UI.WP.RadioImage radioImage5;
        private UI.WP.RadioImage radioImage4;
        private UI.WP.RadioImage radioImage3;
        private UI.WP.RadioImage radioImage2;
        private UI.WP.RadioImage radioImage1;
        private UI.WP.RadioImage radioImage7;
        private UI.WP.RadioImage radioImage8;
    }
}
