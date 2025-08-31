using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class TerminalInfo : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalInfo));
            this.TerTabTitle = new WinPaletter.UI.WP.TextBox();
            this.PictureBox47 = new System.Windows.Forms.PictureBox();
            this.Label174 = new System.Windows.Forms.Label();
            this.TerName = new WinPaletter.UI.WP.TextBox();
            this.PictureBox38 = new System.Windows.Forms.PictureBox();
            this.Label164 = new System.Windows.Forms.Label();
            this.PictureBox28 = new System.Windows.Forms.PictureBox();
            this.Label153 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.TerTabIcon = new WinPaletter.UI.WP.TextBox();
            this.Label166 = new System.Windows.Forms.Label();
            this.PictureBox36 = new System.Windows.Forms.PictureBox();
            this.TerTabColor = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox40 = new System.Windows.Forms.PictureBox();
            this.TerAcrylic = new WinPaletter.UI.WP.CheckBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox47)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // TerTabTitle
            // 
            this.TerTabTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerTabTitle.ForeColor = System.Drawing.Color.White;
            this.TerTabTitle.Location = new System.Drawing.Point(120, 42);
            this.TerTabTitle.MaxLength = 32767;
            this.TerTabTitle.Multiline = false;
            this.TerTabTitle.Name = "TerTabTitle";
            this.TerTabTitle.ReadOnly = false;
            this.TerTabTitle.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TerTabTitle.SelectedText = "";
            this.TerTabTitle.SelectionLength = 0;
            this.TerTabTitle.SelectionStart = 0;
            this.TerTabTitle.Size = new System.Drawing.Size(361, 24);
            this.TerTabTitle.TabIndex = 195;
            this.TerTabTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TerTabTitle.UseSystemPasswordChar = false;
            this.TerTabTitle.WordWrap = true;
            // 
            // PictureBox47
            // 
            this.PictureBox47.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox47.Image")));
            this.PictureBox47.Location = new System.Drawing.Point(12, 42);
            this.PictureBox47.Name = "PictureBox47";
            this.PictureBox47.Size = new System.Drawing.Size(24, 24);
            this.PictureBox47.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox47.TabIndex = 194;
            this.PictureBox47.TabStop = false;
            // 
            // Label174
            // 
            this.Label174.BackColor = System.Drawing.Color.Transparent;
            this.Label174.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label174.Location = new System.Drawing.Point(42, 42);
            this.Label174.Name = "Label174";
            this.Label174.Size = new System.Drawing.Size(72, 24);
            this.Label174.TabIndex = 193;
            this.Label174.Text = "Tab title:";
            this.Label174.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TerName
            // 
            this.TerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerName.ForeColor = System.Drawing.Color.White;
            this.TerName.Location = new System.Drawing.Point(120, 12);
            this.TerName.MaxLength = 32767;
            this.TerName.Multiline = false;
            this.TerName.Name = "TerName";
            this.TerName.ReadOnly = false;
            this.TerName.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TerName.SelectedText = "";
            this.TerName.SelectionLength = 0;
            this.TerName.SelectionStart = 0;
            this.TerName.Size = new System.Drawing.Size(361, 24);
            this.TerName.TabIndex = 192;
            this.TerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TerName.UseSystemPasswordChar = false;
            this.TerName.WordWrap = true;
            // 
            // PictureBox38
            // 
            this.PictureBox38.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox38.Image")));
            this.PictureBox38.Location = new System.Drawing.Point(12, 12);
            this.PictureBox38.Name = "PictureBox38";
            this.PictureBox38.Size = new System.Drawing.Size(24, 24);
            this.PictureBox38.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox38.TabIndex = 191;
            this.PictureBox38.TabStop = false;
            // 
            // Label164
            // 
            this.Label164.BackColor = System.Drawing.Color.Transparent;
            this.Label164.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label164.Location = new System.Drawing.Point(42, 12);
            this.Label164.Name = "Label164";
            this.Label164.Size = new System.Drawing.Size(72, 24);
            this.Label164.TabIndex = 190;
            this.Label164.Text = "Name:";
            this.Label164.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox28
            // 
            this.PictureBox28.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox28.Image")));
            this.PictureBox28.Location = new System.Drawing.Point(12, 72);
            this.PictureBox28.Name = "PictureBox28";
            this.PictureBox28.Size = new System.Drawing.Size(24, 24);
            this.PictureBox28.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox28.TabIndex = 187;
            this.PictureBox28.TabStop = false;
            // 
            // Label153
            // 
            this.Label153.BackColor = System.Drawing.Color.Transparent;
            this.Label153.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label153.Location = new System.Drawing.Point(42, 72);
            this.Label153.Name = "Label153";
            this.Label153.Size = new System.Drawing.Size(62, 24);
            this.Label153.TabIndex = 186;
            this.Label153.Text = "Tab icon:";
            this.Label153.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            this.Button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button11.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button11.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = null;
            this.Button11.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button11.ImageGlyphEnabled = true;
            this.Button11.Location = new System.Drawing.Point(449, 72);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(32, 24);
            this.Button11.TabIndex = 189;
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // TerTabIcon
            // 
            this.TerTabIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerTabIcon.ForeColor = System.Drawing.Color.White;
            this.TerTabIcon.Location = new System.Drawing.Point(120, 72);
            this.TerTabIcon.MaxLength = 32767;
            this.TerTabIcon.Multiline = false;
            this.TerTabIcon.Name = "TerTabIcon";
            this.TerTabIcon.ReadOnly = false;
            this.TerTabIcon.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TerTabIcon.SelectedText = "";
            this.TerTabIcon.SelectionLength = 0;
            this.TerTabIcon.SelectionStart = 0;
            this.TerTabIcon.Size = new System.Drawing.Size(323, 24);
            this.TerTabIcon.TabIndex = 188;
            this.TerTabIcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TerTabIcon.UseSystemPasswordChar = false;
            this.TerTabIcon.WordWrap = true;
            // 
            // Label166
            // 
            this.Label166.BackColor = System.Drawing.Color.Transparent;
            this.Label166.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label166.Location = new System.Drawing.Point(42, 102);
            this.Label166.Name = "Label166";
            this.Label166.Size = new System.Drawing.Size(75, 24);
            this.Label166.TabIndex = 197;
            this.Label166.Text = "Tab color:";
            this.Label166.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox36
            // 
            this.PictureBox36.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox36.Image")));
            this.PictureBox36.Location = new System.Drawing.Point(12, 102);
            this.PictureBox36.Name = "PictureBox36";
            this.PictureBox36.Size = new System.Drawing.Size(24, 24);
            this.PictureBox36.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox36.TabIndex = 198;
            this.PictureBox36.TabStop = false;
            // 
            // TerTabColor
            // 
            this.TerTabColor.AllowDrop = true;
            this.TerTabColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTabColor.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.TerTabColor.DontShowInfo = false;
            this.TerTabColor.Location = new System.Drawing.Point(120, 101);
            this.TerTabColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TerTabColor.Name = "TerTabColor";
            this.TerTabColor.Size = new System.Drawing.Size(132, 25);
            this.TerTabColor.TabIndex = 196;
            this.TerTabColor.ContextMenuItemClickedInvoker += new WinPaletter.UI.Controllers.ColorItem.ContextMenuItemClicked(this.TerTabColor_ContextMenuItemClickedInvoker);
            this.TerTabColor.Click += new System.EventHandler(this.TerTabColor_Click);
            this.TerTabColor.DragDrop += new System.Windows.Forms.DragEventHandler(this.TerTabColor_Click);
            // 
            // PictureBox40
            // 
            this.PictureBox40.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox40.Image")));
            this.PictureBox40.Location = new System.Drawing.Point(12, 132);
            this.PictureBox40.Name = "PictureBox40";
            this.PictureBox40.Size = new System.Drawing.Size(24, 24);
            this.PictureBox40.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox40.TabIndex = 201;
            this.PictureBox40.TabStop = false;
            // 
            // TerAcrylic
            // 
            this.TerAcrylic.Checked = false;
            this.TerAcrylic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TerAcrylic.ForeColor = System.Drawing.Color.White;
            this.TerAcrylic.Location = new System.Drawing.Point(45, 131);
            this.TerAcrylic.Name = "TerAcrylic";
            this.TerAcrylic.Size = new System.Drawing.Size(184, 24);
            this.TerAcrylic.TabIndex = 202;
            this.TerAcrylic.Text = "Acrylic titlebar (All profiles)";
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(321, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 206;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(407, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 34);
            this.Button1.TabIndex = 205;
            this.Button1.Text = "Load";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.Location = new System.Drawing.Point(12, 164);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(469, 1);
            this.Separator1.TabIndex = 207;
            this.Separator1.TabStop = false;
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = global::WinPaletter.Assets.Notifications.Info;
            this.AlertBox1.Location = new System.Drawing.Point(12, 173);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(470, 32);
            this.AlertBox1.TabIndex = 209;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "Tab Icon can be a file path or an emoji/symbol from font \"Segoe Fluent Icons\"";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 213);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(494, 48);
            this.bottom_buttons.TabIndex = 214;
            // 
            // TerminalInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(494, 261);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.PictureBox40);
            this.Controls.Add(this.TerAcrylic);
            this.Controls.Add(this.Label166);
            this.Controls.Add(this.PictureBox36);
            this.Controls.Add(this.TerTabColor);
            this.Controls.Add(this.TerTabTitle);
            this.Controls.Add(this.PictureBox47);
            this.Controls.Add(this.Label174);
            this.Controls.Add(this.TerName);
            this.Controls.Add(this.PictureBox38);
            this.Controls.Add(this.Label164);
            this.Controls.Add(this.PictureBox28);
            this.Controls.Add(this.Label153);
            this.Controls.Add(this.Button11);
            this.Controls.Add(this.TerTabIcon);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TerminalInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminal info";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TerminalInfo_FormClosing);
            this.Load += new System.EventHandler(this.TerminalInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox47)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal UI.WP.TextBox TerTabTitle;
        internal PictureBox PictureBox47;
        internal Label Label174;
        internal UI.WP.TextBox TerName;
        internal PictureBox PictureBox38;
        internal Label Label164;
        internal PictureBox PictureBox28;
        internal Label Label153;
        internal UI.WP.Button Button11;
        internal UI.WP.TextBox TerTabIcon;
        internal Label Label166;
        internal PictureBox PictureBox36;
        internal UI.Controllers.ColorItem TerTabColor;
        internal PictureBox PictureBox40;
        internal UI.WP.CheckBox TerAcrylic;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.AlertBox AlertBox1;
        private UI.WP.GroupBox bottom_buttons;
    }
}
