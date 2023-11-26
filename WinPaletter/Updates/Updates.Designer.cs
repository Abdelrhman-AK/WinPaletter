using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Updates : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Updates));
            Label2 = new Label();
            Panel1 = new Panel();
            LinkLabel3 = new UI.WP.LinkLabel();
            LinkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabel3_LinkClicked);
            RadioButton2 = new UI.WP.RadioButton();
            RadioButton3 = new UI.WP.RadioButton();
            RadioButton1 = new UI.WP.RadioButton();
            ProgressBar1 = new UI.WP.ProgressBar();
            SaveFileDialog1 = new SaveFileDialog();
            Label1 = new Label();
            PictureBox2 = new PictureBox();
            PictureBox4 = new PictureBox();
            Label6 = new Label();
            Label7 = new Label();
            PictureBox5 = new PictureBox();
            Label8 = new Label();
            Label9 = new Label();
            AnimatedBox1 = new UI.WP.AnimatedBox();
            Label3 = new UI.WP.LinkLabel();
            Label3.LinkClicked += new LinkLabelLinkClickedEventHandler(Label3_LinkClicked);
            PictureBox1 = new PictureBox();
            Label17 = new Label();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            AlertBox2 = new UI.WP.AlertBox();
            Separator1 = new UI.WP.SeparatorH();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(174, 78);
            Label2.Name = "Label2";
            Label2.Size = new Size(233, 24);
            Label2.TabIndex = 16;
            Label2.Text = "0.0.0.0";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel1
            // 
            Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel1.Controls.Add(LinkLabel3);
            Panel1.Controls.Add(RadioButton2);
            Panel1.Controls.Add(RadioButton3);
            Panel1.Controls.Add(RadioButton1);
            Panel1.Location = new Point(11, 241);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(395, 117);
            Panel1.TabIndex = 18;
            Panel1.Visible = false;
            // 
            // LinkLabel3
            // 
            LinkLabel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LinkLabel3.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            LinkLabel3.ForeColor = Color.DodgerBlue;
            LinkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
            LinkLabel3.LinkColor = Color.DodgerBlue;
            LinkLabel3.Location = new Point(16, 90);
            LinkLabel3.Name = "LinkLabel3";
            LinkLabel3.Size = new Size(376, 24);
            LinkLabel3.TabIndex = 33;
            LinkLabel3.TabStop = true;
            LinkLabel3.Text = "• What's new?";
            LinkLabel3.TextAlign = ContentAlignment.MiddleLeft;
            LinkLabel3.Visible = false;
            // 
            // RadioButton2
            // 
            RadioButton2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton2.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Location = new Point(3, 32);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(389, 23);
            RadioButton2.TabIndex = 2;
            RadioButton2.Text = "Save download as...";
            // 
            // RadioButton3
            // 
            RadioButton3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton3.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton3.Checked = false;
            RadioButton3.Font = new Font("Segoe UI", 9.0f);
            RadioButton3.ForeColor = Color.White;
            RadioButton3.Location = new Point(3, 61);
            RadioButton3.Name = "RadioButton3";
            RadioButton3.Size = new Size(389, 23);
            RadioButton3.TabIndex = 1;
            RadioButton3.Text = "Just download from the browser";
            // 
            // RadioButton1
            // 
            RadioButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton1.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton1.Checked = true;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Location = new Point(3, 3);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(389, 23);
            RadioButton1.TabIndex = 0;
            RadioButton1.Text = "Download then close WinPaletter and replace it by the new update";
            // 
            // ProgressBar1
            // 
            ProgressBar1.Location = new Point(11, 364);
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(395, 10);
            ProgressBar1.TabIndex = 3;
            ProgressBar1.Visible = false;
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.FileName = "WinPaletter";
            SaveFileDialog1.Filter = "Executable File|*.exe";
            // 
            // Label1
            // 
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(42, 78);
            Label1.Name = "Label1";
            Label1.Size = new Size(126, 24);
            Label1.TabIndex = 20;
            Label1.Text = "Current version:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(12, 78);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.TabIndex = 21;
            PictureBox2.TabStop = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(12, 108);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.TabIndex = 27;
            PictureBox4.TabStop = false;
            // 
            // Label6
            // 
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(42, 108);
            Label6.Name = "Label6";
            Label6.Size = new Size(126, 24);
            Label6.TabIndex = 26;
            Label6.Text = "Update size:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label7.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.Location = new Point(174, 108);
            Label7.Name = "Label7";
            Label7.Size = new Size(233, 24);
            Label7.TabIndex = 25;
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(12, 138);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.TabIndex = 30;
            PictureBox5.TabStop = false;
            // 
            // Label8
            // 
            Label8.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label8.Location = new Point(42, 138);
            Label8.Name = "Label8";
            Label8.Size = new Size(126, 24);
            Label8.TabIndex = 29;
            Label8.Text = "Release date:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label9.Location = new Point(174, 138);
            Label9.Name = "Label9";
            Label9.Size = new Size(233, 24);
            Label9.TabIndex = 28;
            Label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AnimatedBox1
            // 
            AnimatedBox1.Color1 = Color.DodgerBlue;
            AnimatedBox1.Color2 = Color.Crimson;
            AnimatedBox1.Controls.Add(Label3);
            AnimatedBox1.Controls.Add(PictureBox1);
            AnimatedBox1.Controls.Add(Label17);
            AnimatedBox1.Dock = DockStyle.Top;
            AnimatedBox1.Location = new Point(0, 0);
            AnimatedBox1.Name = "AnimatedBox1";
            AnimatedBox1.Size = new Size(421, 68);
            AnimatedBox1.Style = UI.WP.AnimatedBox.Styles.SwapColors;
            AnimatedBox1.TabIndex = 32;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.ForeColor = Color.DodgerBlue;
            Label3.LinkBehavior = LinkBehavior.NeverUnderline;
            Label3.LinkColor = Color.DodgerBlue;
            Label3.Location = new Point(316, 22);
            Label3.Name = "Label3";
            Label3.Size = new Size(93, 24);
            Label3.TabIndex = 17;
            Label3.TabStop = true;
            Label3.Text = "Stable channel";
            Label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(10, 10);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(48, 48);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 8;
            PictureBox1.TabStop = false;
            // 
            // Label17
            // 
            Label17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label17.BackColor = Color.Transparent;
            Label17.Font = new Font("Segoe UI Semibold", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label17.Location = new Point(65, 10);
            Label17.Name = "Label17";
            Label17.Size = new Size(245, 48);
            Label17.TabIndex = 9;
            Label17.Text = "Updates";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(12, 168);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(395, 23);
            CheckBox1.TabIndex = 31;
            CheckBox1.Text = "Automatic check for updates";
            // 
            // AlertBox2
            // 
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox2.BackColor = Color.FromArgb(125, 20, 30);
            AlertBox2.CenterText = true;
            AlertBox2.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            AlertBox2.Image = null;
            AlertBox2.Location = new Point(12, 205);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(395, 31);
            AlertBox2.TabIndex = 12;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "Update available";
            AlertBox2.Visible = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 198);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(395, 1);
            Separator1.TabIndex = 8;
            Separator1.TabStop = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);

            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.Location = new Point(162, 382);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 34);
            Button3.TabIndex = 2;
            Button3.Text = "Cancel";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);

            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.Location = new Point(248, 382);
            Button1.Name = "Button1";
            Button1.Size = new Size(161, 34);
            Button1.TabIndex = 0;
            Button1.Text = "Check for updates";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Updates
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(421, 428);
            Controls.Add(AnimatedBox1);
            Controls.Add(CheckBox1);
            Controls.Add(PictureBox5);
            Controls.Add(Label8);
            Controls.Add(Label9);
            Controls.Add(PictureBox4);
            Controls.Add(Label6);
            Controls.Add(Label7);
            Controls.Add(PictureBox2);
            Controls.Add(Label1);
            Controls.Add(ProgressBar1);
            Controls.Add(Panel1);
            Controls.Add(Label2);
            Controls.Add(AlertBox2);
            Controls.Add(Separator1);
            Controls.Add(Button3);
            Controls.Add(Button1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Updates";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Updates";
            TopMost = true;
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(Updates_Load);
            Shown += new EventHandler(Updates_Shown);
            FormClosing += new FormClosingEventHandler(Updates_FormClosing);
            FormClosed += new FormClosedEventHandler(Updates_FormClosed);
            ResumeLayout(false);

        }

        internal UI.WP.Button Button1;
        internal UI.WP.Button Button3;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.AlertBox AlertBox2;
        internal Label Label2;
        internal UI.WP.LinkLabel Label3;
        internal Panel Panel1;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.WP.RadioButton RadioButton1;
        internal UI.WP.ProgressBar ProgressBar1;
        internal SaveFileDialog SaveFileDialog1;
        internal Label Label1;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox4;
        internal Label Label6;
        internal Label Label7;
        internal PictureBox PictureBox5;
        internal Label Label8;
        internal Label Label9;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.LinkLabel LinkLabel3;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal PictureBox PictureBox1;
        internal Label Label17;
    }
}
