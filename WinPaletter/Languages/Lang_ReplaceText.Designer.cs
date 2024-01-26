using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_ReplaceText : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_ReplaceText));
            this.PictureBox25 = new System.Windows.Forms.PictureBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.PictureBox24 = new System.Windows.Forms.PictureBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.TextBox3 = new WinPaletter.UI.WP.TextBox();
            this.TextBox4 = new WinPaletter.UI.WP.TextBox();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.CheckBox2 = new WinPaletter.UI.WP.CheckBox();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox24)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox25
            // 
            this.PictureBox25.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox25.Image")));
            this.PictureBox25.Location = new System.Drawing.Point(12, 12);
            this.PictureBox25.Name = "PictureBox25";
            this.PictureBox25.Size = new System.Drawing.Size(24, 24);
            this.PictureBox25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox25.TabIndex = 63;
            this.PictureBox25.TabStop = false;
            // 
            // Label20
            // 
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(42, 12);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(109, 24);
            this.Label20.TabIndex = 64;
            this.Label20.Text = "Find what:";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox24
            // 
            this.PictureBox24.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox24.Image")));
            this.PictureBox24.Location = new System.Drawing.Point(12, 42);
            this.PictureBox24.Name = "PictureBox24";
            this.PictureBox24.Size = new System.Drawing.Size(24, 24);
            this.PictureBox24.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox24.TabIndex = 65;
            this.PictureBox24.TabStop = false;
            // 
            // Label18
            // 
            this.Label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(42, 42);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(109, 24);
            this.Label18.TabIndex = 66;
            this.Label18.Text = "Replace with:";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox3
            // 
            this.TextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox3.ForeColor = System.Drawing.Color.White;
            this.TextBox3.Location = new System.Drawing.Point(157, 12);
            this.TextBox3.MaxLength = 32767;
            this.TextBox3.Multiline = false;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.ReadOnly = false;
            this.TextBox3.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox3.SelectedText = "";
            this.TextBox3.SelectionLength = 0;
            this.TextBox3.SelectionStart = 0;
            this.TextBox3.Size = new System.Drawing.Size(275, 24);
            this.TextBox3.TabIndex = 67;
            this.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox3.UseSystemPasswordChar = false;
            this.TextBox3.WordWrap = true;
            // 
            // TextBox4
            // 
            this.TextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox4.ForeColor = System.Drawing.Color.White;
            this.TextBox4.Location = new System.Drawing.Point(157, 42);
            this.TextBox4.MaxLength = 32767;
            this.TextBox4.Multiline = false;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.ReadOnly = false;
            this.TextBox4.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox4.SelectedText = "";
            this.TextBox4.SelectionLength = 0;
            this.TextBox4.SelectionStart = 0;
            this.TextBox4.Size = new System.Drawing.Size(275, 24);
            this.TextBox4.TabIndex = 68;
            this.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox4.UseSystemPasswordChar = false;
            this.TextBox4.WordWrap = true;
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.ImageAsVector = false;
            this.Button7.ImageVector = null;
            this.Button7.Location = new System.Drawing.Point(272, 7);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(80, 34);
            this.Button7.TabIndex = 207;
            this.Button7.Text = "Cancel";
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.ErrorOnHover)));
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button2.ImageAsVector = false;
            this.Button2.ImageVector = null;
            this.Button2.Location = new System.Drawing.Point(358, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 206;
            this.Button2.Text = "Replace";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // CheckBox1
            // 
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(157, 72);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(190, 23);
            this.CheckBox1.TabIndex = 208;
            this.CheckBox1.Text = "Match case";
            // 
            // CheckBox2
            // 
            this.CheckBox2.Checked = false;
            this.CheckBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox2.ForeColor = System.Drawing.Color.White;
            this.CheckBox2.Location = new System.Drawing.Point(157, 101);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(190, 23);
            this.CheckBox2.TabIndex = 209;
            this.CheckBox2.Text = "Match whole word";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button7);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 173);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(444, 48);
            this.bottom_buttons.TabIndex = 212;
            // 
            // Lang_ReplaceText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(444, 221);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.PictureBox25);
            this.Controls.Add(this.Label20);
            this.Controls.Add(this.PictureBox24);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.TextBox4);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lang_ReplaceText";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Replace";
            this.Load += new System.EventHandler(this.Lang_ReplaceText_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox24)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox25;
        internal Label Label20;
        internal PictureBox PictureBox24;
        internal Label Label18;
        internal UI.WP.TextBox TextBox3;
        internal UI.WP.TextBox TextBox4;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button2;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
        private UI.WP.GroupBox bottom_buttons;
    }
}
