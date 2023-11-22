using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SubMenu : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SubMenu));
            PictureBox3 = new PictureBox();
            Label3 = new Label();
            Label5 = new Label();
            PictureBox5 = new PictureBox();
            PaletteContainer = new FlowLayoutPanel();
            Label1 = new Label();
            PictureBox1 = new PictureBox();
            Panel1 = new Panel();
            Label2 = new Label();
            Label4 = new Label();
            PictureBox2 = new PictureBox();
            Separator4 = new UI.WP.SeparatorH();
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Trackbar4 = new UI.WP.Trackbar();
            Trackbar4.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar4_Scroll);
            PreviousColor = new UI.Controllers.ColorItem();
            PreviousColor.Click += new EventHandler(PreviousColor_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Trackbar3 = new UI.WP.Trackbar();
            Trackbar3.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar3_Scroll);
            Trackbar2 = new UI.WP.Trackbar();
            Trackbar2.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar2_Scroll);
            Trackbar1 = new UI.WP.Trackbar();
            Trackbar1.Scroll += new UI.WP.Trackbar.ScrollEventHandler(Trackbar1_Scroll);
            Separator2 = new UI.WP.SeparatorH();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Separator3 = new UI.WP.SeparatorH();
            InvertedColor = new UI.Controllers.ColorItem();
            InvertedColor.Click += new EventHandler(MainColor_Click);
            DefaultColor = new UI.Controllers.ColorItem();
            DefaultColor.Click += new EventHandler(MainColor_Click);
            MainColor = new UI.Controllers.ColorItem();
            MainColor.Click += new EventHandler(MainColor_Click);
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            SuspendLayout();
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(7, 209);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 9;
            PictureBox3.TabStop = false;
            // 
            // Label3
            // 
            Label3.Location = new Point(37, 209);
            Label3.Name = "Label3";
            Label3.Size = new Size(53, 24);
            Label3.TabIndex = 10;
            Label3.Text = "Inverted";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            Label5.Location = new Point(37, 96);
            Label5.Name = "Label5";
            Label5.Size = new Size(61, 24);
            Label5.TabIndex = 14;
            Label5.Text = "Default";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(7, 96);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 13;
            PictureBox5.TabStop = false;
            // 
            // PaletteContainer
            // 
            PaletteContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PaletteContainer.AutoScroll = true;
            PaletteContainer.Location = new Point(203, 43);
            PaletteContainer.Name = "PaletteContainer";
            PaletteContainer.Size = new Size(202, 214);
            PaletteContainer.TabIndex = 46;
            // 
            // Label1
            // 
            Label1.Location = new Point(37, 42);
            Label1.Name = "Label1";
            Label1.Size = new Size(53, 24);
            Label1.TabIndex = 54;
            Label1.Text = "Current";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(7, 42);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 53;
            PictureBox1.TabStop = false;
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.Transparent;
            Panel1.Controls.Add(Button10);
            Panel1.Controls.Add(Label2);
            Panel1.Controls.Add(Button2);
            Panel1.Controls.Add(Button1);
            Panel1.Controls.Add(Button3);
            Panel1.Controls.Add(Button5);
            Panel1.Dock = DockStyle.Top;
            Panel1.Location = new Point(0, 0);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(410, 37);
            Panel1.TabIndex = 64;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(200, 7);
            Label2.Name = "Label2";
            Label2.Size = new Size(207, 24);
            Label2.TabIndex = 56;
            Label2.Text = "Colors history:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Location = new Point(37, 153);
            Label4.Name = "Label4";
            Label4.Size = new Size(53, 24);
            Label4.TabIndex = 70;
            Label4.Text = "Previous";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(7, 153);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 69;
            PictureBox2.TabStop = false;
            // 
            // Separator4
            // 
            Separator4.AlternativeLook = false;
            Separator4.Location = new Point(6, 202);
            Separator4.Name = "Separator4";
            Separator4.Size = new Size(176, 1);
            Separator4.TabIndex = 74;
            Separator4.TabStop = false;
            // 
            // Button9
            // 
            Button9.BackColor = Color.FromArgb(72, 10, 10);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.Location = new Point(157, 180);
            Button9.Name = "Button9";
            Button9.Size = new Size(25, 19);
            Button9.TabIndex = 73;
            Button9.UseVisualStyleBackColor = false;
            // 
            // Trackbar4
            // 
            Trackbar4.LargeChange = 10;
            Trackbar4.Location = new Point(6, 180);
            Trackbar4.Maximum = 200;
            Trackbar4.Minimum = 0;
            Trackbar4.Name = "Trackbar4";
            Trackbar4.Size = new Size(145, 19);
            Trackbar4.SmallChange = 1;
            Trackbar4.TabIndex = 72;
            Trackbar4.Value = 100;
            // 
            // PreviousColor
            // 
            PreviousColor.BackColor = Color.Crimson;
            PreviousColor.DefaultColor = Color.Black;
            PreviousColor.DontShowInfo = false;
            PreviousColor.Location = new Point(97, 156);
            PreviousColor.Margin = new Padding(4, 3, 4, 3);
            PreviousColor.Name = "PreviousColor";
            PreviousColor.Size = new Size(85, 20);
            PreviousColor.TabIndex = 71;
            // 
            // Button8
            // 
            Button8.BackColor = Color.FromArgb(72, 10, 10);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.Location = new Point(157, 236);
            Button8.Name = "Button8";
            Button8.Size = new Size(25, 19);
            Button8.TabIndex = 63;
            Button8.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.BackColor = Color.FromArgb(72, 10, 10);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = (Image)resources.GetObject("Button7.Image");
            Button7.Location = new Point(157, 123);
            Button7.Name = "Button7";
            Button7.Size = new Size(25, 19);
            Button7.TabIndex = 62;
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            Button6.BackColor = Color.FromArgb(72, 10, 10);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.Location = new Point(157, 68);
            Button6.Name = "Button6";
            Button6.Size = new Size(25, 19);
            Button6.TabIndex = 61;
            Button6.UseVisualStyleBackColor = false;
            // 
            // Trackbar3
            // 
            Trackbar3.LargeChange = 10;
            Trackbar3.Location = new Point(6, 236);
            Trackbar3.Maximum = 200;
            Trackbar3.Minimum = 0;
            Trackbar3.Name = "Trackbar3";
            Trackbar3.Size = new Size(145, 19);
            Trackbar3.SmallChange = 1;
            Trackbar3.TabIndex = 60;
            Trackbar3.Value = 100;
            // 
            // Trackbar2
            // 
            Trackbar2.LargeChange = 10;
            Trackbar2.Location = new Point(6, 123);
            Trackbar2.Maximum = 200;
            Trackbar2.Minimum = 0;
            Trackbar2.Name = "Trackbar2";
            Trackbar2.Size = new Size(145, 19);
            Trackbar2.SmallChange = 1;
            Trackbar2.TabIndex = 59;
            Trackbar2.Value = 100;
            // 
            // Trackbar1
            // 
            Trackbar1.LargeChange = 10;
            Trackbar1.Location = new Point(6, 68);
            Trackbar1.Maximum = 200;
            Trackbar1.Minimum = 0;
            Trackbar1.Name = "Trackbar1";
            Trackbar1.Size = new Size(145, 19);
            Trackbar1.SmallChange = 1;
            Trackbar1.TabIndex = 57;
            Trackbar1.Value = 100;
            // 
            // Separator2
            // 
            Separator2.AlternativeLook = false;
            Separator2.Location = new Point(6, 146);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(176, 1);
            Separator2.TabIndex = 48;
            Separator2.TabStop = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Button4.BackColor = Color.FromArgb(72, 10, 10);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = null;
            Button4.Location = new Point(187, 42);
            Button4.Name = "Button4";
            Button4.Size = new Size(12, 215);
            Button4.TabIndex = 45;
            Button4.Text = ">";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Separator3
            // 
            Separator3.AlternativeLook = false;
            Separator3.Location = new Point(6, 91);
            Separator3.Name = "Separator3";
            Separator3.Size = new Size(176, 1);
            Separator3.TabIndex = 44;
            Separator3.TabStop = false;
            // 
            // InvertedColor
            // 
            InvertedColor.BackColor = Color.Crimson;
            InvertedColor.DefaultColor = Color.Black;
            InvertedColor.DontShowInfo = false;
            InvertedColor.Location = new Point(97, 212);
            InvertedColor.Margin = new Padding(4, 3, 4, 3);
            InvertedColor.Name = "InvertedColor";
            InvertedColor.Size = new Size(85, 20);
            InvertedColor.TabIndex = 25;
            // 
            // DefaultColor
            // 
            DefaultColor.BackColor = Color.Crimson;
            DefaultColor.DefaultColor = Color.Black;
            DefaultColor.DontShowInfo = false;
            DefaultColor.Location = new Point(97, 99);
            DefaultColor.Margin = new Padding(4, 3, 4, 3);
            DefaultColor.Name = "DefaultColor";
            DefaultColor.Size = new Size(85, 20);
            DefaultColor.TabIndex = 19;
            // 
            // MainColor
            // 
            MainColor.BackColor = Color.Crimson;
            MainColor.DefaultColor = Color.Black;
            MainColor.DontShowInfo = false;
            MainColor.Location = new Point(97, 44);
            MainColor.Margin = new Padding(4, 3, 4, 3);
            MainColor.Name = "MainColor";
            MainColor.Size = new Size(85, 20);
            MainColor.TabIndex = 4;
            // 
            // Button10
            // 
            Button10.BackColor = Color.FromArgb(72, 10, 10);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = (Image)resources.GetObject("Button10.Image");
            Button10.Location = new Point(100, 4);
            Button10.Name = "Button10";
            Button10.Size = new Size(30, 30);
            Button10.TabIndex = 57;
            Button10.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.BackColor = Color.FromArgb(72, 10, 10);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = (Image)resources.GetObject("Button2.Image");
            Button2.Location = new Point(4, 4);
            Button2.Name = "Button2";
            Button2.Size = new Size(30, 30);
            Button2.TabIndex = 1;
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.BackColor = Color.FromArgb(72, 10, 10);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.Location = new Point(36, 4);
            Button1.Name = "Button1";
            Button1.Size = new Size(30, 30);
            Button1.TabIndex = 0;
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.BackColor = Color.FromArgb(72, 10, 10);
            Button3.DrawOnGlass = false;
            Button3.Enabled = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.Location = new Point(68, 4);
            Button3.Name = "Button3";
            Button3.Size = new Size(30, 30);
            Button3.TabIndex = 2;
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.BackColor = Color.FromArgb(72, 10, 10);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.Location = new Point(132, 4);
            Button5.Name = "Button5";
            Button5.Size = new Size(30, 30);
            Button5.TabIndex = 55;
            Button5.UseVisualStyleBackColor = false;
            // 
            // SubMenu
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 0, 0);
            ClientSize = new Size(410, 262);
            Controls.Add(Separator4);
            Controls.Add(Button9);
            Controls.Add(Trackbar4);
            Controls.Add(PreviousColor);
            Controls.Add(Label4);
            Controls.Add(PictureBox2);
            Controls.Add(Button8);
            Controls.Add(Button7);
            Controls.Add(Button6);
            Controls.Add(Trackbar3);
            Controls.Add(Trackbar2);
            Controls.Add(Trackbar1);
            Controls.Add(Label1);
            Controls.Add(PictureBox1);
            Controls.Add(Separator2);
            Controls.Add(PaletteContainer);
            Controls.Add(Button4);
            Controls.Add(Separator3);
            Controls.Add(InvertedColor);
            Controls.Add(DefaultColor);
            Controls.Add(Label5);
            Controls.Add(PictureBox5);
            Controls.Add(Label3);
            Controls.Add(PictureBox3);
            Controls.Add(MainColor);
            Controls.Add(Panel1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SubMenu";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sub Menu";
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            Shown += new EventHandler(SubMenu_Shown);
            FormClosing += new FormClosingEventHandler(SubMenu_FormClosing);
            Load += new EventHandler(SubMenu_Load);
            ResumeLayout(false);

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
        internal UI.WP.Trackbar Trackbar1;
        internal UI.WP.Trackbar Trackbar2;
        internal UI.WP.Trackbar Trackbar3;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal Panel Panel1;
        internal Label Label2;
        internal UI.WP.SeparatorH Separator4;
        internal UI.WP.Button Button9;
        internal UI.WP.Trackbar Trackbar4;
        internal UI.Controllers.ColorItem PreviousColor;
        internal Label Label4;
        internal PictureBox PictureBox2;
        internal UI.WP.Button Button10;
    }
}