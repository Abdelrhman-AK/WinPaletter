using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_SearchFilter : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_SearchFilter));
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.CheckBox2 = new WinPaletter.UI.WP.CheckBox();
            this.CheckBox3 = new WinPaletter.UI.WP.CheckBox();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.ImageGlyph = null;
            this.Button1.Location = new System.Drawing.Point(274, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(90, 34);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Save filter";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button7
            // 
            this.Button7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.ImageGlyphEnabled = false;
            this.Button7.ImageGlyph = null;
            this.Button7.Location = new System.Drawing.Point(188, 7);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(80, 34);
            this.Button7.TabIndex = 207;
            this.Button7.Text = "Cancel";
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(42, 12);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(172, 24);
            this.Label6.TabIndex = 209;
            this.Label6.Text = "Search through:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox16
            // 
            this.PictureBox16.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox16.Image")));
            this.PictureBox16.Location = new System.Drawing.Point(12, 12);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(24, 24);
            this.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox16.TabIndex = 208;
            this.PictureBox16.TabStop = false;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox1.Checked = true;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(45, 39);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(314, 24);
            this.CheckBox1.TabIndex = 210;
            this.CheckBox1.Text = "Themes names";
            // 
            // CheckBox2
            // 
            this.CheckBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox2.Checked = true;
            this.CheckBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox2.ForeColor = System.Drawing.Color.White;
            this.CheckBox2.Location = new System.Drawing.Point(45, 99);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(314, 24);
            this.CheckBox2.TabIndex = 211;
            this.CheckBox2.Text = "Authors names";
            // 
            // CheckBox3
            // 
            this.CheckBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox3.Checked = true;
            this.CheckBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox3.ForeColor = System.Drawing.Color.White;
            this.CheckBox3.Location = new System.Drawing.Point(45, 69);
            this.CheckBox3.Name = "CheckBox3";
            this.CheckBox3.Size = new System.Drawing.Size(314, 24);
            this.CheckBox3.TabIndex = 212;
            this.CheckBox3.Text = "Themes descriptions";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Controls.Add(this.Button7);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 243);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(371, 48);
            this.bottom_buttons.TabIndex = 213;
            // 
            // Store_SearchFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(371, 291);
            this.Controls.Add(this.CheckBox3);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.PictureBox16);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Store_SearchFilter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search filter";
            this.Load += new System.EventHandler(this.Store_SearchFilter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal UI.WP.Button Button1;
        internal UI.WP.Button Button7;
        internal Label Label6;
        internal PictureBox PictureBox16;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.CheckBox CheckBox3;
        private UI.WP.GroupBox bottom_buttons;
    }
}
