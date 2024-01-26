using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_JSON_Update : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_JSON_Update));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.TextBox2 = new WinPaletter.UI.WP.TextBox();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.AlertBox7 = new WinPaletter.UI.WP.AlertBox();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.SaveJSONDlg = new System.Windows.Forms.SaveFileDialog();
            this.OpenJSONDlg = new System.Windows.Forms.OpenFileDialog();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(12, 12);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 13;
            this.PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(42, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(106, 24);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Old JSON File:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(12, 42);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 15;
            this.PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(42, 42);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(106, 24);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "New JSON File:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(154, 12);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(515, 24);
            this.TextBox1.TabIndex = 16;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            // 
            // TextBox2
            // 
            this.TextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox2.ForeColor = System.Drawing.Color.White;
            this.TextBox2.Location = new System.Drawing.Point(154, 42);
            this.TextBox2.MaxLength = 32767;
            this.TextBox2.Multiline = false;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = false;
            this.TextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox2.SelectedText = "";
            this.TextBox2.SelectionLength = 0;
            this.TextBox2.SelectionStart = 0;
            this.TextBox2.Size = new System.Drawing.Size(341, 24);
            this.TextBox2.TabIndex = 17;
            this.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox2.UseSystemPasswordChar = false;
            this.TextBox2.WordWrap = true;
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button8.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(138)))), ((int)(((byte)(0)))));
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = null;
            this.Button8.ImageAsVector = true;
            this.Button8.ImageVector = ((System.Drawing.Image)(resources.GetObject("Button8.ImageVector")));
            this.Button8.Location = new System.Drawing.Point(675, 12);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(40, 24);
            this.Button8.TabIndex = 111;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(138)))), ((int)(((byte)(0)))));
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageAsVector = true;
            this.Button1.ImageVector = ((System.Drawing.Image)(resources.GetObject("Button1.ImageVector")));
            this.Button1.Location = new System.Drawing.Point(675, 42);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(40, 24);
            this.Button1.TabIndex = 112;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(153)))), ((int)(((byte)(95)))));
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.ImageAsVector = true;
            this.Button5.ImageVector = ((System.Drawing.Image)(resources.GetObject("Button5.ImageVector")));
            this.Button5.Location = new System.Drawing.Point(501, 42);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(168, 24);
            this.Button5.TabIndex = 113;
            this.Button5.Text = "Generate new (English)";
            this.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // AlertBox7
            // 
            this.AlertBox7.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox7.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox7.CenterText = false;
            this.AlertBox7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox7.Image = null;
            this.AlertBox7.Location = new System.Drawing.Point(12, 156);
            this.AlertBox7.Name = "AlertBox7";
            this.AlertBox7.Size = new System.Drawing.Size(703, 40);
            this.AlertBox7.TabIndex = 114;
            this.AlertBox7.TabStop = false;
            this.AlertBox7.Text = resources.GetString("AlertBox7.Text");
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 72);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(703, 1);
            this.Separator1.TabIndex = 119;
            this.Separator1.TabStop = false;
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
            this.Button7.Location = new System.Drawing.Point(514, 7);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(90, 34);
            this.Button7.TabIndex = 203;
            this.Button7.Text = "Cancel";
            this.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.ImageAsVector = false;
            this.Button3.ImageVector = null;
            this.Button3.Location = new System.Drawing.Point(610, 7);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(110, 34);
            this.Button3.TabIndex = 202;
            this.Button3.Text = "Save as ...";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // SaveJSONDlg
            // 
            this.SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*";
            // 
            // OpenJSONDlg
            // 
            this.OpenJSONDlg.Filter = "JSON File (*.json)|*.json";
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(12, 110);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(703, 40);
            this.AlertBox1.TabIndex = 204;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "This experimental feature requires caution; always create backups before initiati" +
    "ng. In case of any unexpected outcomes, please report the issue on GitHub";
            // 
            // CheckBox1
            // 
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(12, 79);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(703, 23);
            this.CheckBox1.TabIndex = 205;
            this.CheckBox1.Text = "Exclude global strings not found in the new file (Not recommended in backward com" +
    "ptability)";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Controls.Add(this.Button7);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 239);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(727, 48);
            this.bottom_buttons.TabIndex = 211;
            // 
            // Lang_JSON_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(727, 287);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.AlertBox7);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI Historic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lang_JSON_Update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update language JSON file";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Lang_JSON_Update_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal Label Label1;
        internal PictureBox PictureBox2;
        internal Label Label2;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button5;
        internal UI.WP.AlertBox AlertBox7;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button3;
        internal SaveFileDialog SaveJSONDlg;
        internal OpenFileDialog OpenJSONDlg;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.CheckBox CheckBox1;
        private UI.WP.GroupBox bottom_buttons;
    }
}
