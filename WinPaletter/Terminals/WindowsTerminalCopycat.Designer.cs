using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class WindowsTerminalCopycat : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsTerminalCopycat));
            this.Label163 = new System.Windows.Forms.Label();
            this.PictureBox33 = new System.Windows.Forms.PictureBox();
            this.ComboBox1 = new WinPaletter.UI.WP.ComboBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox33)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label163
            // 
            this.Label163.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label163.BackColor = System.Drawing.Color.Transparent;
            this.Label163.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label163.Location = new System.Drawing.Point(42, 12);
            this.Label163.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label163.Name = "Label163";
            this.Label163.Size = new System.Drawing.Size(418, 23);
            this.Label163.TabIndex = 191;
            this.Label163.Text = "Copycat from:";
            this.Label163.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox33
            // 
            this.PictureBox33.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox33.Image")));
            this.PictureBox33.Location = new System.Drawing.Point(12, 12);
            this.PictureBox33.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PictureBox33.Name = "PictureBox33";
            this.PictureBox33.Size = new System.Drawing.Size(24, 24);
            this.PictureBox33.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox33.TabIndex = 192;
            this.PictureBox33.TabStop = false;
            // 
            // ComboBox1
            // 
            this.ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ComboBox1.ForeColor = System.Drawing.Color.White;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.ItemHeight = 20;
            this.ComboBox1.Location = new System.Drawing.Point(46, 38);
            this.ComboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(415, 26);
            this.ComboBox1.TabIndex = 193;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.ImageGlyph = null;
            this.Button2.Location = new System.Drawing.Point(299, 7);
            this.Button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 33);
            this.Button2.TabIndex = 195;
            this.Button2.Text = "Cancel";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.ImageGlyph = null;
            this.Button1.Location = new System.Drawing.Point(385, 7);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 33);
            this.Button1.TabIndex = 194;
            this.Button1.Text = "OK";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 73);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(472, 48);
            this.bottom_buttons.TabIndex = 213;
            // 
            // WindowsTerminalCopycat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(472, 121);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.Label163);
            this.Controls.Add(this.PictureBox33);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowsTerminalCopycat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Copycat";
            this.Load += new System.EventHandler(this.WindowsTerminalCopycat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox33)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal Label Label163;
        internal PictureBox PictureBox33;
        internal UI.WP.ComboBox ComboBox1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        private UI.WP.GroupBox bottom_buttons;
    }
}
