using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_DownloadProgress : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_DownloadProgress));
            ProgressBar1 = new UI.WP.ProgressBar();
            Label2 = new Label();
            Label3 = new Label();
            Label4 = new Label();
            Label5 = new Label();
            Label6 = new Label();
            Label7 = new Label();
            PictureBox2 = new PictureBox();
            PictureBox3 = new PictureBox();
            PictureBox4 = new PictureBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            AnimatedBox1 = new UI.WP.AnimatedBox();
            PictureBox1 = new PictureBox();
            Label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // ProgressBar1
            // 
            ProgressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProgressBar1.Location = new Point(12, 168);
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(660, 20);
            ProgressBar1.TabIndex = 0;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Location = new Point(165, 74);
            Label2.Name = "Label2";
            Label2.Size = new Size(511, 24);
            Label2.TabIndex = 2;
            Label2.Text = "0";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Location = new Point(165, 104);
            Label3.Name = "Label3";
            Label3.Size = new Size(511, 24);
            Label3.TabIndex = 3;
            Label3.Text = "0";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.Location = new Point(165, 134);
            Label4.Name = "Label4";
            Label4.Size = new Size(511, 24);
            Label4.TabIndex = 4;
            Label4.Text = "0";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(42, 134);
            Label5.Name = "Label5";
            Label5.Size = new Size(117, 24);
            Label5.TabIndex = 7;
            Label5.Text = "Remaining time:";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(42, 104);
            Label6.Name = "Label6";
            Label6.Size = new Size(117, 24);
            Label6.TabIndex = 6;
            Label6.Text = "Download speed:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            Label7.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label7.Location = new Point(42, 74);
            Label7.Name = "Label7";
            Label7.Size = new Size(117, 24);
            Label7.TabIndex = 5;
            Label7.Text = "Size:";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(12, 74);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.TabIndex = 34;
            PictureBox2.TabStop = false;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(12, 104);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.TabIndex = 35;
            PictureBox3.TabStop = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(12, 134);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.TabIndex = 36;
            PictureBox4.TabStop = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(199, 49, 61);
            Button3.Location = new Point(592, 200);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 34);
            Button3.TabIndex = 37;
            Button3.Text = "Cancel";
            Button3.UseVisualStyleBackColor = false;
            // 
            // AnimatedBox1
            // 
            AnimatedBox1.Color1 = Color.DodgerBlue;
            AnimatedBox1.Color2 = Color.Crimson;
            AnimatedBox1.Controls.Add(PictureBox1);
            AnimatedBox1.Controls.Add(Label1);
            AnimatedBox1.Dock = DockStyle.Top;
            AnimatedBox1.Location = new Point(0, 0);
            AnimatedBox1.Name = "AnimatedBox1";
            AnimatedBox1.Size = new Size(684, 68);
            AnimatedBox1.Style = UI.WP.AnimatedBox.Styles.SwapColors;
            AnimatedBox1.TabIndex = 33;
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
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(65, 10);
            Label1.Name = "Label1";
            Label1.Size = new Size(607, 48);
            Label1.TabIndex = 9;
            Label1.Text = "0";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Store_DownloadProgress
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(684, 246);
            ControlBox = false;
            Controls.Add(Button3);
            Controls.Add(PictureBox4);
            Controls.Add(PictureBox3);
            Controls.Add(PictureBox2);
            Controls.Add(Label5);
            Controls.Add(Label6);
            Controls.Add(Label7);
            Controls.Add(Label4);
            Controls.Add(Label3);
            Controls.Add(Label2);
            Controls.Add(ProgressBar1);
            Controls.Add(AnimatedBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Store_DownloadProgress";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Downloading theme resources pack";
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(Store_DownloadProgress_Load);
            FormClosed += new FormClosedEventHandler(Store_DownloadProgress_FormClosed);
            ResumeLayout(false);

        }

        internal UI.WP.ProgressBar ProgressBar1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal Label Label5;
        internal Label Label6;
        internal Label Label7;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal PictureBox PictureBox1;
        internal Label Label1;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox3;
        internal PictureBox PictureBox4;
        internal UI.WP.Button Button3;
    }
}