using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_CPToggles : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_CPToggles));
            this.CheckedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Label17 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.button2 = new WinPaletter.UI.WP.Button();
            this.button4 = new WinPaletter.UI.WP.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckedListBox1
            // 
            this.CheckedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CheckedListBox1.FormattingEnabled = true;
            this.CheckedListBox1.Location = new System.Drawing.Point(45, 46);
            this.CheckedListBox1.Name = "CheckedListBox1";
            this.CheckedListBox1.Size = new System.Drawing.Size(482, 270);
            this.CheckedListBox1.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(340, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(187, 34);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Proceed with current selections";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label17
            // 
            this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(42, 12);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(495, 24);
            this.Label17.TabIndex = 207;
            this.Label17.Text = "To prevent unintended actions, these features will be adjusted:";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            this.PictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(12, 12);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(24, 24);
            this.PictureBox6.TabIndex = 206;
            this.PictureBox6.TabStop = false;
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyph = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.Location = new System.Drawing.Point(254, 7);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(80, 34);
            this.Button3.TabIndex = 208;
            this.Button3.Text = "Cancel";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.button4);
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Controls.Add(this.button2);
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 329);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(533, 48);
            this.bottom_buttons.TabIndex = 212;
            // 
            // button2
            // 
            this.button2.CustomColor = System.Drawing.Color.Empty;
            this.button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = null;
            this.button2.ImageGlyph = null;
            this.button2.ImageGlyphEnabled = false;
            this.button2.Location = new System.Drawing.Point(8, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 34);
            this.button2.TabIndex = 213;
            this.button2.Text = "Select all";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.CustomColor = System.Drawing.Color.Empty;
            this.button4.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = null;
            this.button4.ImageGlyph = null;
            this.button4.ImageGlyphEnabled = false;
            this.button4.Location = new System.Drawing.Point(94, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 34);
            this.button4.TabIndex = 214;
            this.button4.Text = "Unselect all";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Store_CPToggles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(533, 377);
            this.ControlBox = false;
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.PictureBox6);
            this.Controls.Add(this.CheckedListBox1);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Store_CPToggles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check the following items";
            this.Load += new System.EventHandler(this.Store_CPToggles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal CheckedListBox CheckedListBox1;
        internal UI.WP.Button Button1;
        internal Label Label17;
        internal PictureBox PictureBox6;
        internal UI.WP.Button Button3;
        private UI.WP.GroupBox bottom_buttons;
        internal UI.WP.Button button2;
        internal UI.WP.Button button4;
    }
}
