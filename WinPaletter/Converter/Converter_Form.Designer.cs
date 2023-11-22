using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Converter_Form : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Converter_Form));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Label4 = new System.Windows.Forms.Label();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.CheckBox2 = new WinPaletter.UI.WP.CheckBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(12, 12);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(12, 42);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.TabIndex = 1;
            this.PictureBox2.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(42, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(81, 24);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Theme file:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(42, 42);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(81, 24);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Detection:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(126, 47);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(546, 150);
            this.Label3.TabIndex = 5;
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.DefaultExt = "wpt";
            this.SaveFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth";
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(42, 201);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(81, 24);
            this.Label4.TabIndex = 29;
            this.Label4.Text = "Options:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(12, 201);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.TabIndex = 28;
            this.PictureBox3.TabStop = false;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.DrawOnGlass = false;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.Location = new System.Drawing.Point(506, 265);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 27;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.DrawOnGlass = false;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.Location = new System.Drawing.Point(592, 265);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(80, 34);
            this.Button3.TabIndex = 26;
            this.Button3.Text = "Export";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // CheckBox2
            // 
            this.CheckBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox2.Checked = false;
            this.CheckBox2.Enabled = false;
            this.CheckBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox2.ForeColor = System.Drawing.Color.White;
            this.CheckBox2.Location = new System.Drawing.Point(129, 231);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(543, 24);
            this.CheckBox2.TabIndex = 8;
            this.CheckBox2.Text = "Make it valid for old version of WinPaletter less than 1.0.6.9 (old themes format" +
    ")";
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBox1.Checked = true;
            this.CheckBox1.Enabled = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(129, 201);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(543, 24);
            this.CheckBox1.TabIndex = 7;
            this.CheckBox1.Text = "Compress contents (for new JSON-internally-formatted themes)";
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.DrawOnGlass = false;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(639, 12);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(33, 24);
            this.Button1.TabIndex = 6;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(129, 12);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(504, 24);
            this.TextBox1.TabIndex = 4;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Converter_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(684, 311);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.PictureBox3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.PictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 350);
            this.Name = "Converter_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinPaletter theme converter";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Converter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal PictureBox PictureBox2;
        internal Label Label1;
        internal Label Label2;
        internal UI.WP.TextBox TextBox1;
        internal Label Label3;
        internal UI.WP.Button Button1;
        internal SaveFileDialog SaveFileDialog1;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal Label Label4;
        internal PictureBox PictureBox3;
    }
}