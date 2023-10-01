using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BugReport : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(BugReport));
            SaveFileDialog1 = new SaveFileDialog();
            AnimatedBox1 = new UI.WP.AnimatedBox();
            Label3 = new Label();
            PictureBox1 = new PictureBox();
            Label5 = new Label();
            Label7 = new Label();
            PictureBox5 = new PictureBox();
            PictureBox2 = new PictureBox();
            Label2 = new Label();
            Label1 = new Label();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Separator1 = new UI.WP.SeparatorH();
            GroupBox3 = new UI.WP.GroupBox();
            TreeView1 = new UI.WP.TreeView();
            TreeView1.DoubleClick += new EventHandler(TreeView1_DoubleClick);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Label4 = new Label();
            PictureBox4 = new PictureBox();
            AlertBox1 = new UI.WP.AlertBox();
            AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            SuspendLayout();
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.Filter = "Text File (*.txt)|*.txt";
            // 
            // AnimatedBox1
            // 
            AnimatedBox1.BackColor = Color.Maroon;
            AnimatedBox1.Color = Color.FromArgb(40, 0, 0);
            AnimatedBox1.Color1 = Color.FromArgb(40, 0, 0);
            AnimatedBox1.Color2 = Color.FromArgb(185, 1, 1);
            AnimatedBox1.Controls.Add(Label3);
            AnimatedBox1.Controls.Add(PictureBox1);
            AnimatedBox1.Controls.Add(Label5);
            AnimatedBox1.Controls.Add(Label7);
            AnimatedBox1.Controls.Add(PictureBox5);
            AnimatedBox1.Controls.Add(PictureBox2);
            AnimatedBox1.Controls.Add(Label2);
            AnimatedBox1.Controls.Add(Label1);
            AnimatedBox1.Dock = DockStyle.Top;
            AnimatedBox1.Location = new Point(0, 0);
            AnimatedBox1.Name = "AnimatedBox1";
            AnimatedBox1.Size = new Size(794, 110);
            AnimatedBox1.Style = UI.WP.AnimatedBox.Styles.MixedColors;
            AnimatedBox1.TabIndex = 121;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(217, 78);
            Label3.Name = "Label3";
            Label3.Size = new Size(567, 24);
            Label3.TabIndex = 90;
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 7);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(35, 35);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            Label5.BackColor = Color.Transparent;
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(47, 79);
            Label5.Name = "Label5";
            Label5.Size = new Size(164, 24);
            Label5.TabIndex = 89;
            Label5.Text = "WinPaletter version:";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label7.Location = new Point(47, 7);
            Label7.Name = "Label7";
            Label7.Size = new Size(737, 35);
            Label7.TabIndex = 85;
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            PictureBox5.BackColor = Color.Transparent;
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(17, 79);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 88;
            PictureBox5.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.BackColor = Color.Transparent;
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(17, 49);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 1;
            PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(217, 48);
            Label2.Name = "Label2";
            Label2.Size = new Size(567, 24);
            Label2.TabIndex = 87;
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(47, 49);
            Label1.Name = "Label1";
            Label1.Size = new Size(164, 24);
            Label1.TabIndex = 86;
            Label1.Text = "OS information:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(34, 34, 34);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = null;
            Button5.LineColor = Color.FromArgb(0, 81, 210);
            Button5.Location = new Point(425, 396);
            Button5.Name = "Button5";
            Button5.Size = new Size(115, 34);
            Button5.TabIndex = 120;
            Button5.Text = "GitHub issues";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(215, 20, 20);
            Button2.Location = new Point(546, 396);
            Button2.Name = "Button2";
            Button2.Size = new Size(115, 34);
            Button2.TabIndex = 117;
            Button2.Text = "Exit application";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.LineColor = Color.Green;
            Button1.Location = new Point(667, 396);
            Button1.Name = "Button1";
            Button1.Size = new Size(115, 34);
            Button1.TabIndex = 116;
            Button1.Text = "Continue";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 118);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(770, 1);
            Separator1.TabIndex = 115;
            Separator1.TabStop = false;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(TreeView1);
            GroupBox3.Controls.Add(Button6);
            GroupBox3.Controls.Add(Button4);
            GroupBox3.Controls.Add(Button3);
            GroupBox3.Controls.Add(Label4);
            GroupBox3.Controls.Add(PictureBox4);
            GroupBox3.Location = new Point(12, 128);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(770, 255);
            GroupBox3.TabIndex = 114;
            // 
            // TreeView1
            // 
            TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TreeView1.BorderStyle = BorderStyle.None;
            TreeView1.Indent = 15;
            TreeView1.ItemHeight = 20;
            TreeView1.Location = new Point(3, 32);
            TreeView1.Name = "TreeView1";
            TreeView1.Size = new Size(763, 220);
            TreeView1.TabIndex = 122;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = null;
            Button6.LineColor = Color.FromArgb(0, 81, 210);
            Button6.Location = new Point(666, 6);
            Button6.Name = "Button6";
            Button6.Size = new Size(100, 22);
            Button6.TabIndex = 121;
            Button6.Text = "Previous reports";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.LineColor = Color.FromArgb(69, 110, 129);
            Button4.Location = new Point(629, 6);
            Button4.Name = "Button4";
            Button4.Size = new Size(35, 22);
            Button4.TabIndex = 119;
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.LineColor = Color.FromArgb(157, 157, 157);
            Button3.Location = new Point(592, 6);
            Button3.Name = "Button3";
            Button3.Size = new Size(35, 22);
            Button3.TabIndex = 118;
            Button3.UseVisualStyleBackColor = false;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4.Location = new Point(33, 5);
            Label4.Name = "Label4";
            Label4.Size = new Size(424, 24);
            Label4.TabIndex = 86;
            Label4.Text = "Error details:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(3, 5);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 1;
            PictureBox4.TabStop = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(125, 20, 30);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(15, 396);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(404, 34);
            AlertBox1.TabIndex = 122;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "You won't be able to recover from this error. Switch to Release mode";
            AlertBox1.Visible = false;
            // 
            // BugReport
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(794, 442);
            Controls.Add(AlertBox1);
            Controls.Add(AnimatedBox1);
            Controls.Add(Button5);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(Separator1);
            Controls.Add(GroupBox3);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(645, 435);
            Name = "BugReport";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Exception Error";
            TopMost = true;
            AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            Load += new EventHandler(BugReport_Load);
            ResumeLayout(false);

        }
        internal PictureBox PictureBox1;
        internal Label Label7;
        internal Label Label2;
        internal Label Label1;
        internal PictureBox PictureBox2;
        internal UI.WP.GroupBox GroupBox3;
        internal Label Label4;
        internal PictureBox PictureBox4;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal Label Label3;
        internal Label Label5;
        internal PictureBox PictureBox5;
        internal SaveFileDialog SaveFileDialog1;
        internal UI.WP.Button Button6;
        internal UI.WP.TreeView TreeView1;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal UI.WP.AlertBox AlertBox1;
    }
}