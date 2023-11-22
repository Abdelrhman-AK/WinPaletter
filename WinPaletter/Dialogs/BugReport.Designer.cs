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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BugReport));
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.TreeView1 = new WinPaletter.UI.WP.TreeView();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.Filter = "Text File (*.txt)|*.txt";
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.Transparent;
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = null;
            this.AlertBox1.Location = new System.Drawing.Point(15, 396);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(404, 34);
            this.AlertBox1.TabIndex = 122;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "You won\'t be able to recover from this error. Switch to Release mode";
            this.AlertBox1.Visible = false;
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.Maroon;
            this.AnimatedBox1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AnimatedBox1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.AnimatedBox1.Controls.Add(this.Label3);
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label5);
            this.AnimatedBox1.Controls.Add(this.Label7);
            this.AnimatedBox1.Controls.Add(this.PictureBox5);
            this.AnimatedBox1.Controls.Add(this.PictureBox2);
            this.AnimatedBox1.Controls.Add(this.Label2);
            this.AnimatedBox1.Controls.Add(this.Label1);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(794, 110);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.MixedColors;
            this.AnimatedBox1.TabIndex = 121;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(217, 78);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(567, 24);
            this.Label3.TabIndex = 90;
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(6, 7);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(35, 35);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(47, 79);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(164, 24);
            this.Label5.TabIndex = 89;
            this.Label5.Text = "WinPaletter version:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(47, 7);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(737, 35);
            this.Label7.TabIndex = 85;
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            this.PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(17, 79);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 88;
            this.PictureBox5.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(17, 49);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 1;
            this.PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(217, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(567, 24);
            this.Label2.TabIndex = 87;
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(47, 49);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(164, 24);
            this.Label1.TabIndex = 86;
            this.Label1.Text = "OS information:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button5.DrawOnGlass = false;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.Location = new System.Drawing.Point(425, 396);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(115, 34);
            this.Button5.TabIndex = 120;
            this.Button5.Text = "GitHub issues";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button2.DrawOnGlass = false;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.Location = new System.Drawing.Point(546, 396);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(115, 34);
            this.Button2.TabIndex = 117;
            this.Button2.Text = "Exit application";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button1.DrawOnGlass = false;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.Location = new System.Drawing.Point(667, 396);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(115, 34);
            this.Button1.TabIndex = 116;
            this.Button1.Text = "Continue";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.Location = new System.Drawing.Point(12, 118);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(770, 1);
            this.Separator1.TabIndex = 115;
            this.Separator1.TabStop = false;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.TreeView1);
            this.GroupBox3.Controls.Add(this.Button6);
            this.GroupBox3.Controls.Add(this.Button4);
            this.GroupBox3.Controls.Add(this.Button3);
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Controls.Add(this.PictureBox4);
            this.GroupBox3.Location = new System.Drawing.Point(12, 128);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(770, 255);
            this.GroupBox3.TabIndex = 114;
            // 
            // TreeView1
            // 
            this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView1.Indent = 15;
            this.TreeView1.ItemHeight = 20;
            this.TreeView1.Location = new System.Drawing.Point(3, 32);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(763, 220);
            this.TreeView1.TabIndex = 122;
            this.TreeView1.DoubleClick += new System.EventHandler(this.TreeView1_DoubleClick);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button6.DrawOnGlass = false;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = null;
            this.Button6.Location = new System.Drawing.Point(666, 6);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(100, 22);
            this.Button6.TabIndex = 121;
            this.Button6.Text = "Previous reports";
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button4.DrawOnGlass = false;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.Location = new System.Drawing.Point(629, 6);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(35, 22);
            this.Button4.TabIndex = 119;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button3.DrawOnGlass = false;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.Location = new System.Drawing.Point(592, 6);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(35, 22);
            this.Button3.TabIndex = 118;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(33, 5);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(424, 24);
            this.Label4.TabIndex = 86;
            this.Label4.Text = "Error details:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(3, 5);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 1;
            this.PictureBox4.TabStop = false;
            // 
            // BugReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(794, 442);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.AnimatedBox1);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Separator1);
            this.Controls.Add(this.GroupBox3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(645, 435);
            this.Name = "BugReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception Error";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BugReport_Load);
            this.AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.ResumeLayout(false);

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