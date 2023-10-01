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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalInfo));
            TerTabTitle = new UI.WP.TextBox();
            PictureBox47 = new PictureBox();
            Label174 = new Label();
            TerName = new UI.WP.TextBox();
            PictureBox38 = new PictureBox();
            Label164 = new Label();
            PictureBox28 = new PictureBox();
            Label153 = new Label();
            Button11 = new UI.WP.Button();
            TerTabIcon = new UI.WP.TextBox();
            Label166 = new Label();
            PictureBox36 = new PictureBox();
            TerTabColor = new UI.Controllers.ColorItem();
            TerTabColor.Click += new EventHandler(TerTabColor_Click);
            TerTabColor.DragDrop += new DragEventHandler(TerTabColor_Click);
            PictureBox40 = new PictureBox();
            TerAcrylic = new UI.WP.CheckBox();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Separator1 = new UI.WP.SeparatorH();
            AlertBox1 = new UI.WP.AlertBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox47).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox38).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).BeginInit();
            SuspendLayout();
            // 
            // TerTabTitle
            // 
            TerTabTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerTabTitle.BackColor = Color.FromArgb(55, 55, 55);
            TerTabTitle.DrawOnGlass = false;
            TerTabTitle.ForeColor = Color.White;
            TerTabTitle.Location = new Point(120, 42);
            TerTabTitle.MaxLength = 32767;
            TerTabTitle.Multiline = false;
            TerTabTitle.Name = "TerTabTitle";
            TerTabTitle.ReadOnly = false;
            TerTabTitle.Scrollbars = ScrollBars.None;
            TerTabTitle.SelectedText = "";
            TerTabTitle.SelectionLength = 0;
            TerTabTitle.SelectionStart = 0;
            TerTabTitle.Size = new Size(328, 24);
            TerTabTitle.TabIndex = 195;
            TerTabTitle.TextAlign = HorizontalAlignment.Left;
            TerTabTitle.UseSystemPasswordChar = false;
            TerTabTitle.WordWrap = true;
            // 
            // PictureBox47
            // 
            PictureBox47.Image = (Image)resources.GetObject("PictureBox47.Image");
            PictureBox47.Location = new Point(12, 42);
            PictureBox47.Name = "PictureBox47";
            PictureBox47.Size = new Size(24, 24);
            PictureBox47.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox47.TabIndex = 194;
            PictureBox47.TabStop = false;
            // 
            // Label174
            // 
            Label174.BackColor = Color.Transparent;
            Label174.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label174.Location = new Point(42, 42);
            Label174.Name = "Label174";
            Label174.Size = new Size(54, 24);
            Label174.TabIndex = 193;
            Label174.Text = "Tab title:";
            Label174.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TerName
            // 
            TerName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerName.BackColor = Color.FromArgb(55, 55, 55);
            TerName.DrawOnGlass = false;
            TerName.ForeColor = Color.White;
            TerName.Location = new Point(120, 12);
            TerName.MaxLength = 32767;
            TerName.Multiline = false;
            TerName.Name = "TerName";
            TerName.ReadOnly = false;
            TerName.Scrollbars = ScrollBars.None;
            TerName.SelectedText = "";
            TerName.SelectionLength = 0;
            TerName.SelectionStart = 0;
            TerName.Size = new Size(328, 24);
            TerName.TabIndex = 192;
            TerName.TextAlign = HorizontalAlignment.Left;
            TerName.UseSystemPasswordChar = false;
            TerName.WordWrap = true;
            // 
            // PictureBox38
            // 
            PictureBox38.Image = (Image)resources.GetObject("PictureBox38.Image");
            PictureBox38.Location = new Point(12, 12);
            PictureBox38.Name = "PictureBox38";
            PictureBox38.Size = new Size(24, 24);
            PictureBox38.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox38.TabIndex = 191;
            PictureBox38.TabStop = false;
            // 
            // Label164
            // 
            Label164.BackColor = Color.Transparent;
            Label164.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label164.Location = new Point(42, 12);
            Label164.Name = "Label164";
            Label164.Size = new Size(72, 24);
            Label164.TabIndex = 190;
            Label164.Text = "Name:";
            Label164.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox28
            // 
            PictureBox28.Image = (Image)resources.GetObject("PictureBox28.Image");
            PictureBox28.Location = new Point(12, 72);
            PictureBox28.Name = "PictureBox28";
            PictureBox28.Size = new Size(24, 24);
            PictureBox28.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox28.TabIndex = 187;
            PictureBox28.TabStop = false;
            // 
            // Label153
            // 
            Label153.BackColor = Color.Transparent;
            Label153.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label153.Location = new Point(42, 72);
            Label153.Name = "Label153";
            Label153.Size = new Size(62, 24);
            Label153.TabIndex = 186;
            Label153.Text = "Tab icon:";
            Label153.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            Button11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button11.BackColor = Color.FromArgb(34, 34, 34);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.LineColor = Color.FromArgb(164, 125, 25);
            Button11.Location = new Point(416, 72);
            Button11.Name = "Button11";
            Button11.Size = new Size(32, 24);
            Button11.TabIndex = 189;
            Button11.UseVisualStyleBackColor = false;
            // 
            // TerTabIcon
            // 
            TerTabIcon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TerTabIcon.BackColor = Color.FromArgb(55, 55, 55);
            TerTabIcon.DrawOnGlass = false;
            TerTabIcon.ForeColor = Color.White;
            TerTabIcon.Location = new Point(120, 72);
            TerTabIcon.MaxLength = 32767;
            TerTabIcon.Multiline = false;
            TerTabIcon.Name = "TerTabIcon";
            TerTabIcon.ReadOnly = false;
            TerTabIcon.Scrollbars = ScrollBars.None;
            TerTabIcon.SelectedText = "";
            TerTabIcon.SelectionLength = 0;
            TerTabIcon.SelectionStart = 0;
            TerTabIcon.Size = new Size(290, 24);
            TerTabIcon.TabIndex = 188;
            TerTabIcon.TextAlign = HorizontalAlignment.Left;
            TerTabIcon.UseSystemPasswordChar = false;
            TerTabIcon.WordWrap = true;
            // 
            // Label166
            // 
            Label166.BackColor = Color.Transparent;
            Label166.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label166.Location = new Point(42, 102);
            Label166.Name = "Label166";
            Label166.Size = new Size(75, 24);
            Label166.TabIndex = 197;
            Label166.Text = "Tab color:";
            Label166.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox36
            // 
            PictureBox36.Image = (Image)resources.GetObject("PictureBox36.Image");
            PictureBox36.Location = new Point(12, 102);
            PictureBox36.Name = "PictureBox36";
            PictureBox36.Size = new Size(24, 24);
            PictureBox36.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox36.TabIndex = 198;
            PictureBox36.TabStop = false;
            // 
            // TerTabColor
            // 
            TerTabColor.AllowDrop = true;
            TerTabColor.BackColor = Color.FromArgb(12, 12, 12);
            TerTabColor.DefaultColor = Color.FromArgb(12, 12, 12);
            TerTabColor.DontShowInfo = false;
            TerTabColor.Location = new Point(120, 101);
            TerTabColor.Margin = new Padding(4, 3, 4, 3);
            TerTabColor.Name = "TerTabColor";
            TerTabColor.Size = new Size(132, 25);
            TerTabColor.TabIndex = 196;
            // 
            // PictureBox40
            // 
            PictureBox40.Image = (Image)resources.GetObject("PictureBox40.Image");
            PictureBox40.Location = new Point(12, 132);
            PictureBox40.Name = "PictureBox40";
            PictureBox40.Size = new Size(24, 24);
            PictureBox40.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox40.TabIndex = 201;
            PictureBox40.TabStop = false;
            // 
            // TerAcrylic
            // 
            TerAcrylic.BackColor = Color.FromArgb(25, 25, 25);
            TerAcrylic.Checked = false;
            TerAcrylic.Font = new Font("Segoe UI", 9.0f);
            TerAcrylic.ForeColor = Color.White;
            TerAcrylic.Location = new Point(45, 131);
            TerAcrylic.Name = "TerAcrylic";
            TerAcrylic.Size = new Size(184, 24);
            TerAcrylic.TabIndex = 202;
            TerAcrylic.Text = "Acrylic titlebar (All profiles)";
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(283, 212);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 206;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(52, 20, 64);
            Button1.Location = new Point(369, 212);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 34);
            Button1.TabIndex = 205;
            Button1.Text = "Load";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 164);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(436, 1);
            Separator1.TabIndex = 207;
            Separator1.TabStop = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(17, 67, 91);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = My.Resources.notify_info;
            AlertBox1.Location = new Point(12, 174);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(437, 32);
            AlertBox1.TabIndex = 209;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "Tab Icon can be a file path or emoji/symbol from font \"Segoe Fluent Icons\"";
            // 
            // TerminalInfo
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(461, 258);
            Controls.Add(AlertBox1);
            Controls.Add(Separator1);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(PictureBox40);
            Controls.Add(TerAcrylic);
            Controls.Add(Label166);
            Controls.Add(PictureBox36);
            Controls.Add(TerTabColor);
            Controls.Add(TerTabTitle);
            Controls.Add(PictureBox47);
            Controls.Add(Label174);
            Controls.Add(TerName);
            Controls.Add(PictureBox38);
            Controls.Add(Label164);
            Controls.Add(PictureBox28);
            Controls.Add(Label153);
            Controls.Add(Button11);
            Controls.Add(TerTabIcon);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TerminalInfo";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Terminal info";
            ((System.ComponentModel.ISupportInitialize)PictureBox47).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox38).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).EndInit();
            Load += new EventHandler(TerminalInfo_Load);
            FormClosing += new FormClosingEventHandler(TerminalInfo_FormClosing);
            ResumeLayout(false);

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
    }
}