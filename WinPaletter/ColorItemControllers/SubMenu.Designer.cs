using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SubMenu : BorderlessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubMenu));
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.PaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Separator4 = new WinPaletter.UI.WP.SeparatorH();
            this.PreviousColor = new WinPaletter.UI.Controllers.ColorItem();
            this.Separator2 = new WinPaletter.UI.WP.SeparatorH();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Separator3 = new WinPaletter.UI.WP.SeparatorH();
            this.InvertedColor = new WinPaletter.UI.Controllers.ColorItem();
            this.DefaultColor = new WinPaletter.UI.Controllers.ColorItem();
            this.MainColor = new WinPaletter.UI.Controllers.ColorItem();
            this.trackBarX1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.trackBarX2 = new WinPaletter.UI.Controllers.TrackBarX();
            this.trackBarX3 = new WinPaletter.UI.Controllers.TrackBarX();
            this.trackBarX4 = new WinPaletter.UI.Controllers.TrackBarX();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(7, 209);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 9;
            this.PictureBox3.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(37, 209);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(53, 24);
            this.Label3.TabIndex = 10;
            this.Label3.Text = "Inverted";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(37, 96);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(61, 24);
            this.Label5.TabIndex = 14;
            this.Label5.Text = "Default";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(7, 96);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 13;
            this.PictureBox5.TabStop = false;
            // 
            // PaletteContainer
            // 
            this.PaletteContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PaletteContainer.AutoScroll = true;
            this.PaletteContainer.Location = new System.Drawing.Point(203, 43);
            this.PaletteContainer.Name = "PaletteContainer";
            this.PaletteContainer.Size = new System.Drawing.Size(202, 214);
            this.PaletteContainer.TabIndex = 46;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(37, 42);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(53, 24);
            this.Label1.TabIndex = 54;
            this.Label1.Text = "Current";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(7, 42);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 53;
            this.PictureBox1.TabStop = false;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.Button10);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Button2);
            this.Panel1.Controls.Add(this.Button1);
            this.Panel1.Controls.Add(this.Button3);
            this.Panel1.Controls.Add(this.Button5);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(410, 37);
            this.Panel1.TabIndex = 64;
            // 
            // Button10
            // 
            this.Button10.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Button10.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = null;
            this.Button10.ImageGlyphEnabled = true;
            this.Button10.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button10.ImageVector")));
            this.Button10.Location = new System.Drawing.Point(94, 4);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(29, 30);
            this.Button10.TabIndex = 57;
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(200, 7);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(207, 24);
            this.Label2.TabIndex = 56;
            this.Label2.Text = "Colors history:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button2
            // 
            this.Button2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyphEnabled = true;
            this.Button2.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button2.ImageVector")));
            this.Button2.Location = new System.Drawing.Point(4, 4);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(29, 30);
            this.Button2.TabIndex = 1;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyphEnabled = true;
            this.Button1.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button1.ImageVector")));
            this.Button1.Location = new System.Drawing.Point(34, 4);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(29, 30);
            this.Button1.TabIndex = 0;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button3
            // 
            this.Button3.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button3.Enabled = false;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyphEnabled = true;
            this.Button3.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button3.ImageVector")));
            this.Button3.Location = new System.Drawing.Point(64, 4);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(29, 30);
            this.Button3.TabIndex = 2;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button5
            // 
            this.Button5.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.ImageGlyphEnabled = true;
            this.Button5.ImageGlyph = ((System.Drawing.Image)(resources.GetObject("Button5.ImageVector")));
            this.Button5.Location = new System.Drawing.Point(124, 4);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(29, 30);
            this.Button5.TabIndex = 55;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(37, 153);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(53, 24);
            this.Label4.TabIndex = 70;
            this.Label4.Text = "Previous";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(7, 153);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 69;
            this.PictureBox2.TabStop = false;
            // 
            // Separator4
            // 
            this.Separator4.AlternativeLook = false;
            this.Separator4.BackColor = System.Drawing.Color.Transparent;
            this.Separator4.Location = new System.Drawing.Point(6, 202);
            this.Separator4.Name = "Separator4";
            this.Separator4.Size = new System.Drawing.Size(176, 1);
            this.Separator4.TabIndex = 74;
            this.Separator4.TabStop = false;
            // 
            // PreviousColor
            // 
            this.PreviousColor.BackColor = System.Drawing.Color.Crimson;
            this.PreviousColor.DefaultBackColor = System.Drawing.Color.Black;
            this.PreviousColor.DontShowInfo = false;
            this.PreviousColor.Location = new System.Drawing.Point(97, 156);
            this.PreviousColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PreviousColor.Name = "PreviousColor";
            this.PreviousColor.Size = new System.Drawing.Size(85, 20);
            this.PreviousColor.TabIndex = 71;
            this.PreviousColor.Click += new System.EventHandler(this.PreviousColor_Click);
            // 
            // Separator2
            // 
            this.Separator2.AlternativeLook = false;
            this.Separator2.BackColor = System.Drawing.Color.Transparent;
            this.Separator2.Location = new System.Drawing.Point(6, 146);
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(176, 1);
            this.Separator2.TabIndex = 48;
            this.Separator2.TabStop = false;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyphEnabled = false;
            this.Button4.ImageGlyph = null;
            this.Button4.Location = new System.Drawing.Point(187, 42);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(12, 215);
            this.Button4.TabIndex = 45;
            this.Button4.Text = ">";
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Separator3
            // 
            this.Separator3.AlternativeLook = false;
            this.Separator3.BackColor = System.Drawing.Color.Transparent;
            this.Separator3.Location = new System.Drawing.Point(6, 91);
            this.Separator3.Name = "Separator3";
            this.Separator3.Size = new System.Drawing.Size(176, 1);
            this.Separator3.TabIndex = 44;
            this.Separator3.TabStop = false;
            // 
            // InvertedColor
            // 
            this.InvertedColor.BackColor = System.Drawing.Color.Crimson;
            this.InvertedColor.DefaultBackColor = System.Drawing.Color.Black;
            this.InvertedColor.DontShowInfo = false;
            this.InvertedColor.Location = new System.Drawing.Point(97, 212);
            this.InvertedColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.InvertedColor.Name = "InvertedColor";
            this.InvertedColor.Size = new System.Drawing.Size(85, 20);
            this.InvertedColor.TabIndex = 25;
            this.InvertedColor.Click += new System.EventHandler(this.MainColor_Click);
            // 
            // DefaultColor
            // 
            this.DefaultColor.BackColor = System.Drawing.Color.Crimson;
            this.DefaultColor.DefaultBackColor = System.Drawing.Color.Black;
            this.DefaultColor.DontShowInfo = false;
            this.DefaultColor.Location = new System.Drawing.Point(97, 99);
            this.DefaultColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DefaultColor.Name = "DefaultColor";
            this.DefaultColor.Size = new System.Drawing.Size(85, 20);
            this.DefaultColor.TabIndex = 19;
            this.DefaultColor.Click += new System.EventHandler(this.MainColor_Click);
            // 
            // MainColor
            // 
            this.MainColor.BackColor = System.Drawing.Color.Crimson;
            this.MainColor.DefaultBackColor = System.Drawing.Color.Black;
            this.MainColor.DontShowInfo = false;
            this.MainColor.Location = new System.Drawing.Point(97, 44);
            this.MainColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MainColor.Name = "MainColor";
            this.MainColor.Size = new System.Drawing.Size(85, 20);
            this.MainColor.TabIndex = 4;
            this.MainColor.Click += new System.EventHandler(this.MainColor_Click);
            // 
            // trackBarX1
            // 
            this.trackBarX1.AnimateChanges = true;
            this.trackBarX1.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX1.DefaultValue = 100;
            this.trackBarX1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX1.Location = new System.Drawing.Point(7, 69);
            this.trackBarX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX1.Maximum = 200;
            this.trackBarX1.Minimum = 0;
            this.trackBarX1.Name = "trackBarX1";
            this.trackBarX1.Size = new System.Drawing.Size(175, 19);
            this.trackBarX1.TabIndex = 0;
            this.trackBarX1.Value = 100;
            this.trackBarX1.ValueChanged += new System.EventHandler(this.trackBarX1_ValueChanged);
            // 
            // trackBarX2
            // 
            this.trackBarX2.AnimateChanges = true;
            this.trackBarX2.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX2.DefaultValue = 100;
            this.trackBarX2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX2.Location = new System.Drawing.Point(7, 125);
            this.trackBarX2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX2.Maximum = 200;
            this.trackBarX2.Minimum = 0;
            this.trackBarX2.Name = "trackBarX2";
            this.trackBarX2.Size = new System.Drawing.Size(175, 19);
            this.trackBarX2.TabIndex = 75;
            this.trackBarX2.Value = 100;
            this.trackBarX2.ValueChanged += new System.EventHandler(this.trackBarX2_ValueChanged);
            // 
            // trackBarX3
            // 
            this.trackBarX3.AnimateChanges = true;
            this.trackBarX3.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX3.DefaultValue = 100;
            this.trackBarX3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX3.Location = new System.Drawing.Point(7, 180);
            this.trackBarX3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX3.Maximum = 200;
            this.trackBarX3.Minimum = 0;
            this.trackBarX3.Name = "trackBarX3";
            this.trackBarX3.Size = new System.Drawing.Size(175, 19);
            this.trackBarX3.TabIndex = 76;
            this.trackBarX3.Value = 100;
            this.trackBarX3.ValueChanged += new System.EventHandler(this.trackBarX3_ValueChanged);
            // 
            // trackBarX4
            // 
            this.trackBarX4.AnimateChanges = true;
            this.trackBarX4.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX4.DefaultValue = 100;
            this.trackBarX4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX4.Location = new System.Drawing.Point(7, 236);
            this.trackBarX4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX4.Maximum = 200;
            this.trackBarX4.Minimum = 0;
            this.trackBarX4.Name = "trackBarX4";
            this.trackBarX4.Size = new System.Drawing.Size(175, 19);
            this.trackBarX4.TabIndex = 77;
            this.trackBarX4.Value = 100;
            this.trackBarX4.ValueChanged += new System.EventHandler(this.trackBarX4_ValueChanged);
            // 
            // SubMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(410, 262);
            this.Controls.Add(this.trackBarX4);
            this.Controls.Add(this.trackBarX3);
            this.Controls.Add(this.trackBarX2);
            this.Controls.Add(this.trackBarX1);
            this.Controls.Add(this.Separator4);
            this.Controls.Add(this.PreviousColor);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Separator2);
            this.Controls.Add(this.PaletteContainer);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Separator3);
            this.Controls.Add(this.InvertedColor);
            this.Controls.Add(this.DefaultColor);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.PictureBox5);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.PictureBox3);
            this.Controls.Add(this.MainColor);
            this.Controls.Add(this.Panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubMenu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sub Menu";
            this.Load += new System.EventHandler(this.SubMenu_Load);
            this.Shown += new System.EventHandler(this.SubMenu_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.Controllers.ColorItem MainColor;
        internal PictureBox PictureBox3;
        internal Label Label3;
        internal Label Label5;
        internal PictureBox PictureBox5;
        internal UI.Controllers.ColorItem DefaultColor;
        internal UI.Controllers.ColorItem InvertedColor;
        internal UI.WP.SeparatorH Separator3;
        internal UI.WP.Button Button4;
        internal FlowLayoutPanel PaletteContainer;
        internal UI.WP.SeparatorH Separator2;
        internal Label Label1;
        internal PictureBox PictureBox1;
        internal UI.WP.Button Button5;
        internal Panel Panel1;
        internal Label Label2;
        internal UI.WP.SeparatorH Separator4;
        internal UI.Controllers.ColorItem PreviousColor;
        internal Label Label4;
        internal PictureBox PictureBox2;
        internal UI.WP.Button Button10;
        private UI.Controllers.TrackBarX trackBarX1;
        private UI.Controllers.TrackBarX trackBarX2;
        private UI.Controllers.TrackBarX trackBarX3;
        private UI.Controllers.TrackBarX trackBarX4;
    }
}
