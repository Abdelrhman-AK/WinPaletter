using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_Intro_New : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_Intro_New));
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TablessControl1 = new WinPaletter.UI.WP.TablessControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.AnimatedBox1 = new WinPaletter.UI.WP.AnimatedBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Label4 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.banner1 = new WinPaletter.UI.WP.Banner();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.banner2 = new WinPaletter.UI.WP.Banner();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.banner3 = new WinPaletter.UI.WP.Banner();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.banner4 = new WinPaletter.UI.WP.Banner();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.AnimatedBox2 = new WinPaletter.UI.WP.AnimatedBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.TablessControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.TabPage6.SuspendLayout();
            this.AnimatedBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox1.Checked = true;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(9, 12);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(524, 23);
            this.CheckBox1.TabIndex = 2;
            this.CheckBox1.Text = "Always show this on opening WinPaletter Store";
            this.CheckBox1.Visible = false;
            // 
            // Button2
            // 
            this.Button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(547, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 1;
            this.Button2.Text = "Back";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageGlyph = null;
            this.Button1.ImageGlyphEnabled = false;
            this.Button1.Location = new System.Drawing.Point(633, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 34);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Next";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TablessControl1
            // 
            this.TablessControl1.Controls.Add(this.TabPage1);
            this.TablessControl1.Controls.Add(this.TabPage2);
            this.TablessControl1.Controls.Add(this.TabPage4);
            this.TablessControl1.Controls.Add(this.TabPage7);
            this.TablessControl1.Controls.Add(this.TabPage3);
            this.TablessControl1.Controls.Add(this.TabPage6);
            this.TablessControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TablessControl1.Location = new System.Drawing.Point(0, 0);
            this.TablessControl1.Multiline = true;
            this.TablessControl1.Name = "TablessControl1";
            this.TablessControl1.SelectedIndex = 0;
            this.TablessControl1.Size = new System.Drawing.Size(720, 423);
            this.TablessControl1.TabIndex = 1;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Black;
            this.TabPage1.Controls.Add(this.AnimatedBox1);
            this.TabPage1.Location = new System.Drawing.Point(4, 24);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(712, 395);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "0";
            // 
            // AnimatedBox1
            // 
            this.AnimatedBox1.BackColor = System.Drawing.Color.Transparent;
            this.AnimatedBox1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(115)))), ((int)(((byte)(182)))));
            this.AnimatedBox1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(3)))), ((int)(((byte)(28)))));
            this.AnimatedBox1.Controls.Add(this.Label2);
            this.AnimatedBox1.Controls.Add(this.PictureBox1);
            this.AnimatedBox1.Controls.Add(this.Label1);
            this.AnimatedBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimatedBox1.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox1.Name = "AnimatedBox1";
            this.AnimatedBox1.Size = new System.Drawing.Size(712, 395);
            this.AnimatedBox1.Style = WinPaletter.UI.WP.AnimatedBox.Styles.SwapColors;
            this.AnimatedBox1.TabIndex = 0;
            this.AnimatedBox1.Text = "AnimatedBox1";
            // 
            // Label2
            // 
            this.Label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(147, 264);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(419, 24);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "You\'ll come across crucial tips as you advance";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(292, 89);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(128, 128);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(179, 224);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(355, 40);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Welcome to the new WinPaletter Store";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage2.Controls.Add(this.Label4);
            this.TabPage2.Controls.Add(this.PictureBox2);
            this.TabPage2.Controls.Add(this.banner1);
            this.TabPage2.Location = new System.Drawing.Point(4, 24);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(712, 395);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "1";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(0, 42);
            this.Label4.Name = "Label4";
            this.Label4.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.Label4.Size = new System.Drawing.Size(472, 353);
            this.Label4.TabIndex = 3;
            this.Label4.Text = resources.GetString("Label4.Text");
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(472, 42);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(240, 353);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 0;
            this.PictureBox2.TabStop = false;
            // 
            // banner1
            // 
            this.banner1.BackColor = System.Drawing.Color.Transparent;
            this.banner1.Dock = System.Windows.Forms.DockStyle.Top;
            this.banner1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner1.Image = null;
            this.banner1.Location = new System.Drawing.Point(0, 0);
            this.banner1.Name = "banner1";
            this.banner1.Size = new System.Drawing.Size(712, 42);
            this.banner1.TabIndex = 4;
            this.banner1.TabStop = false;
            this.banner1.Text = "WinPaletter Store operates through two distinct methods:";
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage4.Controls.Add(this.Button4);
            this.TabPage4.Controls.Add(this.Label7);
            this.TabPage4.Controls.Add(this.PictureBox4);
            this.TabPage4.Controls.Add(this.banner2);
            this.TabPage4.Location = new System.Drawing.Point(4, 24);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(712, 395);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "2";
            // 
            // Button4
            // 
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyph = null;
            this.Button4.ImageGlyphEnabled = false;
            this.Button4.Location = new System.Drawing.Point(23, 105);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(157, 33);
            this.Button4.TabIndex = 8;
            this.Button4.Text = "Documentation";
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(0, 42);
            this.Label7.Name = "Label7";
            this.Label7.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.Label7.Size = new System.Drawing.Size(472, 353);
            this.Label7.TabIndex = 7;
            this.Label7.Text = "Refer to this documentation to learn how to upload your themes to the WinPaletter" +
    " Store GitHub repository.";
            // 
            // PictureBox4
            // 
            this.PictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(472, 42);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(240, 353);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 9;
            this.PictureBox4.TabStop = false;
            // 
            // banner2
            // 
            this.banner2.BackColor = System.Drawing.Color.Transparent;
            this.banner2.Dock = System.Windows.Forms.DockStyle.Top;
            this.banner2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner2.Image = null;
            this.banner2.Location = new System.Drawing.Point(0, 0);
            this.banner2.Name = "banner2";
            this.banner2.Size = new System.Drawing.Size(712, 42);
            this.banner2.TabIndex = 10;
            this.banner2.TabStop = false;
            this.banner2.Text = "You have the option to upload your themes to the WinPaletter Store repository";
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage7.Controls.Add(this.Button5);
            this.TabPage7.Controls.Add(this.Label14);
            this.TabPage7.Controls.Add(this.PictureBox6);
            this.TabPage7.Controls.Add(this.banner3);
            this.TabPage7.Location = new System.Drawing.Point(4, 24);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(712, 395);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "3";
            // 
            // Button5
            // 
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = null;
            this.Button5.ImageGlyph = null;
            this.Button5.ImageGlyphEnabled = false;
            this.Button5.Location = new System.Drawing.Point(23, 126);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(157, 33);
            this.Button5.TabIndex = 10;
            this.Button5.Text = "Documentation";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label14.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(0, 42);
            this.Label14.Name = "Label14";
            this.Label14.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.Label14.Size = new System.Drawing.Size(472, 353);
            this.Label14.TabIndex = 9;
            this.Label14.Text = "You can include links to servers or GitHub repositories to access additional them" +
    "es through WinPaletter Store. Refer to this documentation for more information o" +
    "n extending Store sources.";
            // 
            // PictureBox6
            // 
            this.PictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox6.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(472, 42);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(240, 353);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 11;
            this.PictureBox6.TabStop = false;
            // 
            // banner3
            // 
            this.banner3.BackColor = System.Drawing.Color.Transparent;
            this.banner3.Dock = System.Windows.Forms.DockStyle.Top;
            this.banner3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner3.Image = null;
            this.banner3.Location = new System.Drawing.Point(0, 0);
            this.banner3.Name = "banner3";
            this.banner3.Size = new System.Drawing.Size(712, 42);
            this.banner3.TabIndex = 12;
            this.banner3.TabStop = false;
            this.banner3.Text = "You have the capability to expand the sources from which WinPaletter Store can ge" +
    "t themes!";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.Button3);
            this.TabPage3.Controls.Add(this.Label5);
            this.TabPage3.Controls.Add(this.PictureBox3);
            this.TabPage3.Controls.Add(this.banner4);
            this.TabPage3.Location = new System.Drawing.Point(4, 24);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(712, 395);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "4";
            // 
            // Button3
            // 
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyph = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.Location = new System.Drawing.Point(23, 101);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(157, 33);
            this.Button3.TabIndex = 6;
            this.Button3.Text = "Documentation";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(0, 42);
            this.Label5.Name = "Label5";
            this.Label5.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.Label5.Size = new System.Drawing.Size(472, 353);
            this.Label5.TabIndex = 5;
            this.Label5.Text = "This feature is optional. Explore this documentation to learn more about creating" +
    " a Store server or GitHub repository.";
            // 
            // PictureBox3
            // 
            this.PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(472, 42);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(240, 353);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 0;
            this.PictureBox3.TabStop = false;
            // 
            // banner4
            // 
            this.banner4.BackColor = System.Drawing.Color.Transparent;
            this.banner4.Dock = System.Windows.Forms.DockStyle.Top;
            this.banner4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banner4.Image = null;
            this.banner4.Location = new System.Drawing.Point(0, 0);
            this.banner4.Name = "banner4";
            this.banner4.Size = new System.Drawing.Size(712, 42);
            this.banner4.TabIndex = 13;
            this.banner4.TabStop = false;
            this.banner4.Text = "You have the ability to establish your personalized WinPaletter Store online sour" +
    "ce!";
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage6.Controls.Add(this.AnimatedBox2);
            this.TabPage6.Location = new System.Drawing.Point(4, 24);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(712, 395);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "5";
            // 
            // AnimatedBox2
            // 
            this.AnimatedBox2.BackColor = System.Drawing.Color.Transparent;
            this.AnimatedBox2.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(115)))), ((int)(((byte)(182)))));
            this.AnimatedBox2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(3)))), ((int)(((byte)(28)))));
            this.AnimatedBox2.Controls.Add(this.Label11);
            this.AnimatedBox2.Controls.Add(this.PictureBox5);
            this.AnimatedBox2.Controls.Add(this.Label12);
            this.AnimatedBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimatedBox2.Location = new System.Drawing.Point(0, 0);
            this.AnimatedBox2.Name = "AnimatedBox2";
            this.AnimatedBox2.Size = new System.Drawing.Size(712, 395);
            this.AnimatedBox2.Style = WinPaletter.UI.WP.AnimatedBox.Styles.SwapColors;
            this.AnimatedBox2.TabIndex = 1;
            this.AnimatedBox2.Text = "AnimatedBox2";
            // 
            // Label11
            // 
            this.Label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(206, 264);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(301, 24);
            this.Label11.TabIndex = 2;
            this.Label11.Text = "Thanks for using WinPaletter Store";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(292, 89);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(128, 128);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 0;
            this.PictureBox5.TabStop = false;
            // 
            // Label12
            // 
            this.Label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(206, 224);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(301, 40);
            this.Label12.TabIndex = 1;
            this.Label12.Text = "That\'s it!";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.CheckBox1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 423);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(720, 48);
            this.bottom_buttons.TabIndex = 213;
            // 
            // Store_Intro_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(720, 471);
            this.Controls.Add(this.TablessControl1);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Store_Intro_New";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WinPaletter Store New Experience";
            this.TopMost = true;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Store_Intro_New_FormClosing);
            this.Load += new System.EventHandler(this.Store_Intro_New_Load);
            this.TablessControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.TabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.AnimatedBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal UI.WP.TablessControl TablessControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal Label Label1;
        internal Label Label2;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal PictureBox PictureBox2;
        internal Label Label4;
        internal TabPage TabPage3;
        internal UI.WP.Button Button3;
        internal Label Label5;
        internal PictureBox PictureBox3;
        internal TabPage TabPage4;
        internal Label Label7;
        internal PictureBox PictureBox4;
        internal UI.WP.Button Button4;
        internal TabPage TabPage6;
        internal UI.WP.AnimatedBox AnimatedBox2;
        internal Label Label11;
        internal PictureBox PictureBox5;
        internal Label Label12;
        internal UI.WP.CheckBox CheckBox1;
        internal TabPage TabPage7;
        internal PictureBox PictureBox6;
        internal UI.WP.Button Button5;
        internal Label Label14;
        private UI.WP.GroupBox bottom_buttons;
        private UI.WP.Banner banner1;
        private UI.WP.Banner banner2;
        private UI.WP.Banner banner3;
        private UI.WP.Banner banner4;
    }
}
