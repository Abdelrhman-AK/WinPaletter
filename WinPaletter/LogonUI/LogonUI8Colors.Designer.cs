using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUI8Colors : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI8Colors));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.color24 = new WinPaletter.UI.WP.RadioImage();
            this.color23 = new WinPaletter.UI.WP.RadioImage();
            this.color22 = new WinPaletter.UI.WP.RadioImage();
            this.color21 = new WinPaletter.UI.WP.RadioImage();
            this.color20 = new WinPaletter.UI.WP.RadioImage();
            this.color19 = new WinPaletter.UI.WP.RadioImage();
            this.color18 = new WinPaletter.UI.WP.RadioImage();
            this.color17 = new WinPaletter.UI.WP.RadioImage();
            this.color16 = new WinPaletter.UI.WP.RadioImage();
            this.color15 = new WinPaletter.UI.WP.RadioImage();
            this.color14 = new WinPaletter.UI.WP.RadioImage();
            this.color13 = new WinPaletter.UI.WP.RadioImage();
            this.color12 = new WinPaletter.UI.WP.RadioImage();
            this.color11 = new WinPaletter.UI.WP.RadioImage();
            this.color10 = new WinPaletter.UI.WP.RadioImage();
            this.color9 = new WinPaletter.UI.WP.RadioImage();
            this.color8 = new WinPaletter.UI.WP.RadioImage();
            this.color7 = new WinPaletter.UI.WP.RadioImage();
            this.color6 = new WinPaletter.UI.WP.RadioImage();
            this.color5 = new WinPaletter.UI.WP.RadioImage();
            this.color4 = new WinPaletter.UI.WP.RadioImage();
            this.color3 = new WinPaletter.UI.WP.RadioImage();
            this.color2 = new WinPaletter.UI.WP.RadioImage();
            this.color1 = new WinPaletter.UI.WP.RadioImage();
            this.color0 = new WinPaletter.UI.WP.RadioImage();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(12, 12);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(35, 35);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 3;
            this.PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(53, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(359, 35);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Background color (Only for Windows 8, not 8.1)";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.Button2.Location = new System.Drawing.Point(146, 315);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 70;
            this.Button2.Text = "Cancel";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.DrawOnGlass = false;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(232, 315);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(180, 34);
            this.Button1.TabIndex = 69;
            this.Button1.Text = "Load into current theme";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // color24
            // 
            this.color24.Checked = false;
            this.color24.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color24.ForeColor = System.Drawing.Color.White;
            this.color24.Image = null;
            this.color24.ImageWithText = false;
            this.color24.Location = new System.Drawing.Point(284, 245);
            this.color24.Name = "color24";
            this.color24.ShowText = false;
            this.color24.Size = new System.Drawing.Size(40, 40);
            this.color24.TabIndex = 54;
            this.color24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color23
            // 
            this.color23.Checked = false;
            this.color23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color23.ForeColor = System.Drawing.Color.White;
            this.color23.Image = null;
            this.color23.ImageWithText = false;
            this.color23.Location = new System.Drawing.Point(238, 245);
            this.color23.Name = "color23";
            this.color23.ShowText = false;
            this.color23.Size = new System.Drawing.Size(40, 40);
            this.color23.TabIndex = 53;
            this.color23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color22
            // 
            this.color22.Checked = false;
            this.color22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color22.ForeColor = System.Drawing.Color.White;
            this.color22.Image = null;
            this.color22.ImageWithText = false;
            this.color22.Location = new System.Drawing.Point(192, 245);
            this.color22.Name = "color22";
            this.color22.ShowText = false;
            this.color22.Size = new System.Drawing.Size(40, 40);
            this.color22.TabIndex = 52;
            this.color22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color21
            // 
            this.color21.Checked = false;
            this.color21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color21.ForeColor = System.Drawing.Color.White;
            this.color21.Image = null;
            this.color21.ImageWithText = false;
            this.color21.Location = new System.Drawing.Point(146, 245);
            this.color21.Name = "color21";
            this.color21.ShowText = false;
            this.color21.Size = new System.Drawing.Size(40, 40);
            this.color21.TabIndex = 51;
            this.color21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color20
            // 
            this.color20.Checked = false;
            this.color20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color20.ForeColor = System.Drawing.Color.White;
            this.color20.Image = null;
            this.color20.ImageWithText = false;
            this.color20.Location = new System.Drawing.Point(100, 245);
            this.color20.Name = "color20";
            this.color20.ShowText = false;
            this.color20.Size = new System.Drawing.Size(40, 40);
            this.color20.TabIndex = 50;
            this.color20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color19
            // 
            this.color19.Checked = false;
            this.color19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color19.ForeColor = System.Drawing.Color.White;
            this.color19.Image = null;
            this.color19.ImageWithText = false;
            this.color19.Location = new System.Drawing.Point(284, 199);
            this.color19.Name = "color19";
            this.color19.ShowText = false;
            this.color19.Size = new System.Drawing.Size(40, 40);
            this.color19.TabIndex = 49;
            this.color19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color18
            // 
            this.color18.Checked = false;
            this.color18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color18.ForeColor = System.Drawing.Color.White;
            this.color18.Image = null;
            this.color18.ImageWithText = false;
            this.color18.Location = new System.Drawing.Point(238, 199);
            this.color18.Name = "color18";
            this.color18.ShowText = false;
            this.color18.Size = new System.Drawing.Size(40, 40);
            this.color18.TabIndex = 48;
            this.color18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color17
            // 
            this.color17.Checked = false;
            this.color17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color17.ForeColor = System.Drawing.Color.White;
            this.color17.Image = null;
            this.color17.ImageWithText = false;
            this.color17.Location = new System.Drawing.Point(192, 199);
            this.color17.Name = "color17";
            this.color17.ShowText = false;
            this.color17.Size = new System.Drawing.Size(40, 40);
            this.color17.TabIndex = 47;
            this.color17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color16
            // 
            this.color16.Checked = false;
            this.color16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color16.ForeColor = System.Drawing.Color.White;
            this.color16.Image = null;
            this.color16.ImageWithText = false;
            this.color16.Location = new System.Drawing.Point(146, 199);
            this.color16.Name = "color16";
            this.color16.ShowText = false;
            this.color16.Size = new System.Drawing.Size(40, 40);
            this.color16.TabIndex = 46;
            this.color16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color15
            // 
            this.color15.Checked = false;
            this.color15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color15.ForeColor = System.Drawing.Color.White;
            this.color15.Image = null;
            this.color15.ImageWithText = false;
            this.color15.Location = new System.Drawing.Point(100, 199);
            this.color15.Name = "color15";
            this.color15.ShowText = false;
            this.color15.Size = new System.Drawing.Size(40, 40);
            this.color15.TabIndex = 45;
            this.color15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color14
            // 
            this.color14.Checked = false;
            this.color14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color14.ForeColor = System.Drawing.Color.White;
            this.color14.Image = null;
            this.color14.ImageWithText = false;
            this.color14.Location = new System.Drawing.Point(284, 153);
            this.color14.Name = "color14";
            this.color14.ShowText = false;
            this.color14.Size = new System.Drawing.Size(40, 40);
            this.color14.TabIndex = 44;
            this.color14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color13
            // 
            this.color13.Checked = false;
            this.color13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color13.ForeColor = System.Drawing.Color.White;
            this.color13.Image = null;
            this.color13.ImageWithText = false;
            this.color13.Location = new System.Drawing.Point(238, 153);
            this.color13.Name = "color13";
            this.color13.ShowText = false;
            this.color13.Size = new System.Drawing.Size(40, 40);
            this.color13.TabIndex = 43;
            this.color13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color12
            // 
            this.color12.Checked = false;
            this.color12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color12.ForeColor = System.Drawing.Color.White;
            this.color12.Image = null;
            this.color12.ImageWithText = false;
            this.color12.Location = new System.Drawing.Point(192, 153);
            this.color12.Name = "color12";
            this.color12.ShowText = false;
            this.color12.Size = new System.Drawing.Size(40, 40);
            this.color12.TabIndex = 42;
            this.color12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color11
            // 
            this.color11.Checked = false;
            this.color11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color11.ForeColor = System.Drawing.Color.White;
            this.color11.Image = null;
            this.color11.ImageWithText = false;
            this.color11.Location = new System.Drawing.Point(146, 153);
            this.color11.Name = "color11";
            this.color11.ShowText = false;
            this.color11.Size = new System.Drawing.Size(40, 40);
            this.color11.TabIndex = 41;
            this.color11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color10
            // 
            this.color10.Checked = false;
            this.color10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color10.ForeColor = System.Drawing.Color.White;
            this.color10.Image = null;
            this.color10.ImageWithText = false;
            this.color10.Location = new System.Drawing.Point(100, 153);
            this.color10.Name = "color10";
            this.color10.ShowText = false;
            this.color10.Size = new System.Drawing.Size(40, 40);
            this.color10.TabIndex = 40;
            this.color10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color9
            // 
            this.color9.Checked = false;
            this.color9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color9.ForeColor = System.Drawing.Color.White;
            this.color9.Image = null;
            this.color9.ImageWithText = false;
            this.color9.Location = new System.Drawing.Point(284, 107);
            this.color9.Name = "color9";
            this.color9.ShowText = false;
            this.color9.Size = new System.Drawing.Size(40, 40);
            this.color9.TabIndex = 39;
            this.color9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color8
            // 
            this.color8.Checked = false;
            this.color8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color8.ForeColor = System.Drawing.Color.White;
            this.color8.Image = null;
            this.color8.ImageWithText = false;
            this.color8.Location = new System.Drawing.Point(238, 107);
            this.color8.Name = "color8";
            this.color8.ShowText = false;
            this.color8.Size = new System.Drawing.Size(40, 40);
            this.color8.TabIndex = 38;
            this.color8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color7
            // 
            this.color7.Checked = false;
            this.color7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color7.ForeColor = System.Drawing.Color.White;
            this.color7.Image = null;
            this.color7.ImageWithText = false;
            this.color7.Location = new System.Drawing.Point(192, 107);
            this.color7.Name = "color7";
            this.color7.ShowText = false;
            this.color7.Size = new System.Drawing.Size(40, 40);
            this.color7.TabIndex = 37;
            this.color7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color6
            // 
            this.color6.Checked = false;
            this.color6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color6.ForeColor = System.Drawing.Color.White;
            this.color6.Image = null;
            this.color6.ImageWithText = false;
            this.color6.Location = new System.Drawing.Point(146, 107);
            this.color6.Name = "color6";
            this.color6.ShowText = false;
            this.color6.Size = new System.Drawing.Size(40, 40);
            this.color6.TabIndex = 36;
            this.color6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color5
            // 
            this.color5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(0)))), ((int)(((byte)(3)))));
            this.color5.Checked = false;
            this.color5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color5.ForeColor = System.Drawing.Color.White;
            this.color5.Image = null;
            this.color5.ImageWithText = false;
            this.color5.Location = new System.Drawing.Point(100, 107);
            this.color5.Name = "color5";
            this.color5.ShowText = false;
            this.color5.Size = new System.Drawing.Size(40, 40);
            this.color5.TabIndex = 35;
            this.color5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color4
            // 
            this.color4.Checked = false;
            this.color4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color4.ForeColor = System.Drawing.Color.White;
            this.color4.Image = null;
            this.color4.ImageWithText = false;
            this.color4.Location = new System.Drawing.Point(284, 61);
            this.color4.Name = "color4";
            this.color4.ShowText = false;
            this.color4.Size = new System.Drawing.Size(40, 40);
            this.color4.TabIndex = 34;
            this.color4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color3
            // 
            this.color3.Checked = false;
            this.color3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color3.ForeColor = System.Drawing.Color.White;
            this.color3.Image = null;
            this.color3.ImageWithText = false;
            this.color3.Location = new System.Drawing.Point(238, 61);
            this.color3.Name = "color3";
            this.color3.ShowText = false;
            this.color3.Size = new System.Drawing.Size(40, 40);
            this.color3.TabIndex = 33;
            this.color3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color2
            // 
            this.color2.Checked = false;
            this.color2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color2.ForeColor = System.Drawing.Color.White;
            this.color2.Image = null;
            this.color2.ImageWithText = false;
            this.color2.Location = new System.Drawing.Point(192, 61);
            this.color2.Name = "color2";
            this.color2.ShowText = false;
            this.color2.Size = new System.Drawing.Size(40, 40);
            this.color2.TabIndex = 32;
            this.color2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color1
            // 
            this.color1.Checked = false;
            this.color1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color1.ForeColor = System.Drawing.Color.White;
            this.color1.Image = null;
            this.color1.ImageWithText = false;
            this.color1.Location = new System.Drawing.Point(146, 61);
            this.color1.Name = "color1";
            this.color1.ShowText = false;
            this.color1.Size = new System.Drawing.Size(40, 40);
            this.color1.TabIndex = 31;
            this.color1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // color0
            // 
            this.color0.Checked = true;
            this.color0.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.color0.ForeColor = System.Drawing.Color.White;
            this.color0.Image = null;
            this.color0.ImageWithText = false;
            this.color0.Location = new System.Drawing.Point(100, 61);
            this.color0.Name = "color0";
            this.color0.ShowText = false;
            this.color0.Size = new System.Drawing.Size(40, 40);
            this.color0.TabIndex = 30;
            this.color0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogonUI8Colors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(424, 361);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.color24);
            this.Controls.Add(this.color23);
            this.Controls.Add(this.color22);
            this.Controls.Add(this.color21);
            this.Controls.Add(this.color20);
            this.Controls.Add(this.color19);
            this.Controls.Add(this.color18);
            this.Controls.Add(this.color17);
            this.Controls.Add(this.color16);
            this.Controls.Add(this.color15);
            this.Controls.Add(this.color14);
            this.Controls.Add(this.color13);
            this.Controls.Add(this.color12);
            this.Controls.Add(this.color11);
            this.Controls.Add(this.color10);
            this.Controls.Add(this.color9);
            this.Controls.Add(this.color8);
            this.Controls.Add(this.color7);
            this.Controls.Add(this.color6);
            this.Controls.Add(this.color5);
            this.Controls.Add(this.color4);
            this.Controls.Add(this.color3);
            this.Controls.Add(this.color2);
            this.Controls.Add(this.color1);
            this.Controls.Add(this.color0);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonUI8Colors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogonUI color";
            this.Load += new System.EventHandler(this.LogonUI8Colors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        internal PictureBox PictureBox1;
        internal Label Label1;
        internal UI.WP.RadioImage color0;
        internal UI.WP.RadioImage color1;
        internal UI.WP.RadioImage color3;
        internal UI.WP.RadioImage color2;
        internal UI.WP.RadioImage color4;
        internal UI.WP.RadioImage color9;
        internal UI.WP.RadioImage color8;
        internal UI.WP.RadioImage color7;
        internal UI.WP.RadioImage color6;
        internal UI.WP.RadioImage color5;
        internal UI.WP.RadioImage color19;
        internal UI.WP.RadioImage color18;
        internal UI.WP.RadioImage color17;
        internal UI.WP.RadioImage color16;
        internal UI.WP.RadioImage color15;
        internal UI.WP.RadioImage color14;
        internal UI.WP.RadioImage color13;
        internal UI.WP.RadioImage color12;
        internal UI.WP.RadioImage color11;
        internal UI.WP.RadioImage color10;
        internal UI.WP.RadioImage color24;
        internal UI.WP.RadioImage color23;
        internal UI.WP.RadioImage color22;
        internal UI.WP.RadioImage color21;
        internal UI.WP.RadioImage color20;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
    }
}