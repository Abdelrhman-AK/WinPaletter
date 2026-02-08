using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class NewExtTerminal : UI.WP.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewExtTerminal));
            this.PictureBox17 = new System.Windows.Forms.PictureBox();
            this.Label102 = new System.Windows.Forms.Label();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.Button16 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox17
            // 
            this.PictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox17.Image")));
            this.PictureBox17.Location = new System.Drawing.Point(12, 12);
            this.PictureBox17.Name = "PictureBox17";
            this.PictureBox17.Size = new System.Drawing.Size(24, 24);
            this.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox17.TabIndex = 101;
            this.PictureBox17.TabStop = false;
            // 
            // Label102
            // 
            this.Label102.BackColor = System.Drawing.Color.Transparent;
            this.Label102.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label102.ForeColor = System.Drawing.Color.White;
            this.Label102.Location = new System.Drawing.Point(42, 12);
            this.Label102.Name = "Label102";
            this.Label102.Size = new System.Drawing.Size(56, 24);
            this.Label102.TabIndex = 100;
            this.Label102.Text = "Path:";
            this.Label102.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button2
            // 
            this.Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.ImageGlyph = null;
            this.Button2.Location = new System.Drawing.Point(250, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 201;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = ((WinPaletter.UI.WP.Button.Flags)((WinPaletter.UI.WP.Button.Flags.TintedOnHover | WinPaletter.UI.WP.Button.Flags.CustomColorOnHover)));
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.ImageGlyph = null;
            this.Button1.Location = new System.Drawing.Point(336, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(160, 34);
            this.Button1.TabIndex = 200;
            this.Button1.Text = "Add to registry entry";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = global::WinPaletter.Assets.Notifications.Warning;
            this.AlertBox1.Location = new System.Drawing.Point(12, 44);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(479, 49);
            this.AlertBox1.TabIndex = 199;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "This feature is experimental. Ensure that you are selecting a console application" +
    ", not a WinForm/Desktop application, and that it is located on the system drive " +
    "(C:).";
            // 
            // Button16
            // 
            this.Button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button16.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button16.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button16.ForeColor = System.Drawing.Color.White;
            this.Button16.Image = null;
            this.Button16.ImageGlyphEnabled = true;
            this.Button16.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button16.ImageVector")));
            this.Button16.Location = new System.Drawing.Point(461, 12);
            this.Button16.Name = "Button16";
            this.Button16.Size = new System.Drawing.Size(30, 24);
            this.Button16.TabIndex = 193;
            this.Button16.UseVisualStyleBackColor = false;
            this.Button16.Click += new System.EventHandler(this.Button16_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(104, 12);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(351, 24);
            this.TextBox1.TabIndex = 102;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 103);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(503, 48);
            this.bottom_buttons.TabIndex = 212;
            // 
            // NewExtTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(503, 151);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.Button16);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.PictureBox17);
            this.Controls.Add(this.Label102);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewExtTerminal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New external terminal";
            this.Load += new System.EventHandler(this.NewExtTerminal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox17;
        internal Label Label102;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.Button Button16;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        private UI.WP.GroupBox bottom_buttons;
    }
}
