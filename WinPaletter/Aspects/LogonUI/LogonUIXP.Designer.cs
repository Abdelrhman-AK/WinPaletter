using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUIXP : AspectsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUIXP));
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.color_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.RadioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage1 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.Size = new System.Drawing.Size(784, 52);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.CheckBox1);
            this.GroupBox1.Controls.Add(this.PictureBox2);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.PictureBox7);
            this.GroupBox1.Controls.Add(this.color_pick);
            this.GroupBox1.Controls.Add(this.PictureBox1);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Location = new System.Drawing.Point(9, 307);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(762, 100);
            this.GroupBox1.TabIndex = 212;
            this.GroupBox1.UseDecorationPattern = false;
            this.GroupBox1.UseSharpStyle = false;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(39, 70);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(717, 25);
            this.CheckBox1.TabIndex = 115;
            this.CheckBox1.Text = "Show more options (e.g. shutdown button)";
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(8, 70);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(25, 25);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 114;
            this.PictureBox2.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(39, 39);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(109, 25);
            this.Label3.TabIndex = 113;
            this.Label3.Text = "Background color:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            this.PictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(8, 39);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(25, 25);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 94;
            this.PictureBox7.TabStop = false;
            // 
            // color_pick
            // 
            this.color_pick.AllowDrop = true;
            this.color_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.color_pick.DefaultBackColor = System.Drawing.Color.Black;
            this.color_pick.DontShowInfo = false;
            this.color_pick.Location = new System.Drawing.Point(155, 39);
            this.color_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.color_pick.Name = "color_pick";
            this.color_pick.Size = new System.Drawing.Size(100, 25);
            this.color_pick.TabIndex = 93;
            this.color_pick.ContextMenuMadeColorChangeInvoker += new WinPaletter.UI.Controllers.ColorItem.ContextMenuMadeColorChange(this.color_pick_ContextMenuMadeColorChangeInvoker);
            this.color_pick.Click += new System.EventHandler(this.color_pick_Click);
            this.color_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.color_pick_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(30, 30);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(39, 3);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(717, 30);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Tweaks specific for Windows 2000 mode";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.Label2);
            this.GroupBox3.Controls.Add(this.Label1);
            this.GroupBox3.Controls.Add(this.RadioImage2);
            this.GroupBox3.Controls.Add(this.RadioImage1);
            this.GroupBox3.Controls.Add(this.PictureBox6);
            this.GroupBox3.Controls.Add(this.Label13);
            this.GroupBox3.Location = new System.Drawing.Point(9, 58);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(762, 243);
            this.GroupBox3.TabIndex = 211;
            this.GroupBox3.UseDecorationPattern = false;
            this.GroupBox3.UseSharpStyle = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(384, 212);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(170, 22);
            this.Label2.TabIndex = 113;
            this.Label2.Text = "Windows 2000";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(208, 212);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(170, 22);
            this.Label1.TabIndex = 112;
            this.Label1.Text = "Windows XP";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioImage2
            // 
            this.RadioImage2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RadioImage2.Checked = false;
            this.RadioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage2.ForeColor = System.Drawing.Color.White;
            this.RadioImage2.Image = null;
            this.RadioImage2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage2.Location = new System.Drawing.Point(384, 39);
            this.RadioImage2.Name = "RadioImage2";
            this.RadioImage2.Size = new System.Drawing.Size(170, 170);
            this.RadioImage2.TabIndex = 3;
            this.RadioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RadioImage2.CheckedChanged += new System.EventHandler(this.RadioImage2_CheckedChanged);
            // 
            // RadioImage1
            // 
            this.RadioImage1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RadioImage1.Checked = false;
            this.RadioImage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage1.ForeColor = System.Drawing.Color.White;
            this.RadioImage1.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage1.Image")));
            this.RadioImage1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage1.Location = new System.Drawing.Point(208, 39);
            this.RadioImage1.Name = "RadioImage1";
            this.RadioImage1.Size = new System.Drawing.Size(170, 170);
            this.RadioImage1.TabIndex = 2;
            this.RadioImage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RadioImage1.CheckedChanged += new System.EventHandler(this.RadioImage1_CheckedChanged);
            // 
            // PictureBox6
            // 
            this.PictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(3, 3);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(30, 30);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 1;
            this.PictureBox6.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label13.BackColor = System.Drawing.Color.Transparent;
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(39, 3);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(720, 30);
            this.Label13.TabIndex = 0;
            this.Label13.Text = "LogonUI screen type";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LogonUIXP
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CanGeneratePalette = true;
            this.ClientSize = new System.Drawing.Size(784, 471);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonUIXP";
            this.Text = "LogonUI";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.LogonUIXP_Load);
            this.Controls.SetChildIndex(this.GroupBox3, 0);
            this.Controls.SetChildIndex(this.GroupBox1, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            this.ResumeLayout(false);

        }
        internal UI.WP.GroupBox GroupBox3;
        internal Label Label2;
        internal Label Label1;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.WP.RadioImage RadioImage1;
        internal PictureBox PictureBox6;
        internal Label Label13;
        internal UI.WP.GroupBox GroupBox1;
        internal PictureBox PictureBox1;
        internal Label Label5;
        internal PictureBox PictureBox7;
        internal UI.Controllers.ColorItem color_pick;
        internal UI.WP.CheckBox CheckBox1;
        internal PictureBox PictureBox2;
        internal Label Label3;
    }
}
