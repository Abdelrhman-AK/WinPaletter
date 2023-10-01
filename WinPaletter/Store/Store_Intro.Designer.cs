using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_Intro : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_Intro));
            Panel1 = new Panel();
            CheckBox1 = new UI.WP.CheckBox();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            TablessControl1 = new UI.WP.TablessControl();
            TabPage1 = new TabPage();
            AnimatedBox1 = new UI.WP.AnimatedBox();
            Label2 = new Label();
            PictureBox1 = new PictureBox();
            Label1 = new Label();
            TabPage2 = new TabPage();
            Label4 = new Label();
            PictureBox2 = new PictureBox();
            Label3 = new Label();
            TabPage4 = new TabPage();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Label7 = new Label();
            PictureBox4 = new PictureBox();
            Label8 = new Label();
            TabPage7 = new TabPage();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Label14 = new Label();
            PictureBox6 = new PictureBox();
            Label15 = new Label();
            TabPage3 = new TabPage();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Label5 = new Label();
            PictureBox3 = new PictureBox();
            Label6 = new Label();
            TabPage6 = new TabPage();
            AnimatedBox2 = new UI.WP.AnimatedBox();
            Label11 = new Label();
            PictureBox5 = new PictureBox();
            Label12 = new Label();
            Panel1.SuspendLayout();
            TablessControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            TabPage6.SuspendLayout();
            AnimatedBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Controls.Add(CheckBox1);
            Panel1.Controls.Add(Button2);
            Panel1.Controls.Add(Button1);
            Panel1.Dock = DockStyle.Bottom;
            Panel1.Location = new Point(0, 330);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(660, 50);
            Panel1.TabIndex = 2;
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(12, 13);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(464, 23);
            CheckBox1.TabIndex = 2;
            CheckBox1.Text = "Always show this on opening WinPaletter Store";
            CheckBox1.Visible = false;
            // 
            // Button2
            // 
            Button2.BackColor = Color.FromArgb(50, 50, 50);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(192, 3, 28);
            Button2.Location = new Point(482, 10);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 28);
            Button2.TabIndex = 1;
            Button2.Text = "Back";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.BackColor = Color.FromArgb(50, 50, 50);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.LineColor = Color.FromArgb(0, 81, 210);
            Button1.Location = new Point(568, 10);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 28);
            Button1.TabIndex = 0;
            Button1.Text = "Next";
            Button1.UseVisualStyleBackColor = false;
            // 
            // TablessControl1
            // 
            TablessControl1.Controls.Add(TabPage1);
            TablessControl1.Controls.Add(TabPage2);
            TablessControl1.Controls.Add(TabPage4);
            TablessControl1.Controls.Add(TabPage7);
            TablessControl1.Controls.Add(TabPage3);
            TablessControl1.Controls.Add(TabPage6);
            TablessControl1.Dock = DockStyle.Fill;
            TablessControl1.Location = new Point(0, 0);
            TablessControl1.Name = "TablessControl1";
            TablessControl1.SelectedIndex = 0;
            TablessControl1.Size = new Size(660, 330);
            TablessControl1.TabIndex = 1;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.Black;
            TabPage1.Controls.Add(AnimatedBox1);
            TabPage1.Location = new Point(4, 24);
            TabPage1.Name = "TabPage1";
            TabPage1.Size = new Size(652, 302);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "0";
            // 
            // AnimatedBox1
            // 
            AnimatedBox1.Color = Color.FromArgb(21, 115, 182);
            AnimatedBox1.Color1 = Color.FromArgb(21, 115, 182);
            AnimatedBox1.Color2 = Color.FromArgb(192, 3, 28);
            AnimatedBox1.Controls.Add(Label2);
            AnimatedBox1.Controls.Add(PictureBox1);
            AnimatedBox1.Controls.Add(Label1);
            AnimatedBox1.Dock = DockStyle.Fill;
            AnimatedBox1.Location = new Point(0, 0);
            AnimatedBox1.Name = "AnimatedBox1";
            AnimatedBox1.Size = new Size(652, 302);
            AnimatedBox1.Style = UI.WP.AnimatedBox.Styles.SwapColors;
            AnimatedBox1.TabIndex = 0;
            AnimatedBox1.Text = "AnimatedBox1";
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(117, 227);
            Label2.Name = "Label2";
            Label2.Size = new Size(419, 24);
            Label2.TabIndex = 2;
            Label2.Text = "You will know important tips as you proceed";
            Label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(262, 52);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(128, 128);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(149, 187);
            Label1.Name = "Label1";
            Label1.Size = new Size(355, 40);
            Label1.TabIndex = 1;
            Label1.Text = "Welcome to WinPaletter Store";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(Label4);
            TabPage2.Controls.Add(PictureBox2);
            TabPage2.Controls.Add(Label3);
            TabPage2.Location = new Point(4, 24);
            TabPage2.Name = "TabPage2";
            TabPage2.Size = new Size(652, 302);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "1";
            // 
            // Label4
            // 
            Label4.BackColor = Color.Transparent;
            Label4.Dock = DockStyle.Fill;
            Label4.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(0, 40);
            Label4.Name = "Label4";
            Label4.Padding = new Padding(20, 10, 0, 0);
            Label4.Size = new Size(412, 262);
            Label4.TabIndex = 3;
            Label4.Text = resources.GetString("Label4.Text");
            // 
            // PictureBox2
            // 
            PictureBox2.BackColor = Color.Transparent;
            PictureBox2.Dock = DockStyle.Right;
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(412, 40);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(240, 262);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 0;
            PictureBox2.TabStop = false;
            // 
            // Label3
            // 
            Label3.BackColor = Color.FromArgb(50, 100, 100, 100);
            Label3.Dock = DockStyle.Top;
            Label3.Font = new Font("Segoe UI", 12.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(0, 0);
            Label3.Name = "Label3";
            Label3.Padding = new Padding(5, 5, 0, 0);
            Label3.Size = new Size(652, 40);
            Label3.TabIndex = 2;
            Label3.Text = "There are two ways for WinPaletter Store";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(Button4);
            TabPage4.Controls.Add(Label7);
            TabPage4.Controls.Add(PictureBox4);
            TabPage4.Controls.Add(Label8);
            TabPage4.Location = new Point(4, 24);
            TabPage4.Name = "TabPage4";
            TabPage4.Size = new Size(652, 302);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "2";
            // 
            // Button4
            // 
            Button4.BackColor = Color.FromArgb(50, 50, 50);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = null;
            Button4.LineColor = Color.FromArgb(0, 81, 210);
            Button4.Location = new Point(23, 105);
            Button4.Name = "Button4";
            Button4.Size = new Size(157, 33);
            Button4.TabIndex = 8;
            Button4.Text = "Documentation";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Label7
            // 
            Label7.BackColor = Color.Transparent;
            Label7.Dock = DockStyle.Fill;
            Label7.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.Location = new Point(0, 40);
            Label7.Name = "Label7";
            Label7.Padding = new Padding(20, 10, 0, 0);
            Label7.Size = new Size(412, 262);
            Label7.TabIndex = 7;
            Label7.Text = "Visit this documentation to know how to upload your themes to WinPaletter Store G" + "itHub repository";
            // 
            // PictureBox4
            // 
            PictureBox4.BackColor = Color.Transparent;
            PictureBox4.Dock = DockStyle.Right;
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(412, 40);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(240, 262);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 9;
            PictureBox4.TabStop = false;
            // 
            // Label8
            // 
            Label8.BackColor = Color.FromArgb(50, 100, 100, 100);
            Label8.Dock = DockStyle.Top;
            Label8.Font = new Font("Segoe UI", 12.75f, FontStyle.Bold);
            Label8.Location = new Point(0, 0);
            Label8.Name = "Label8";
            Label8.Padding = new Padding(5, 5, 0, 0);
            Label8.Size = new Size(652, 40);
            Label8.TabIndex = 6;
            Label8.Text = "You can upload your themes to WinPaletter Store repository";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage7
            // 
            TabPage7.BackColor = Color.FromArgb(25, 25, 25);
            TabPage7.Controls.Add(Button5);
            TabPage7.Controls.Add(Label14);
            TabPage7.Controls.Add(PictureBox6);
            TabPage7.Controls.Add(Label15);
            TabPage7.Location = new Point(4, 24);
            TabPage7.Name = "TabPage7";
            TabPage7.Size = new Size(652, 302);
            TabPage7.TabIndex = 6;
            TabPage7.Text = "3";
            // 
            // Button5
            // 
            Button5.BackColor = Color.FromArgb(50, 50, 50);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = null;
            Button5.LineColor = Color.FromArgb(0, 81, 210);
            Button5.Location = new Point(23, 145);
            Button5.Name = "Button5";
            Button5.Size = new Size(157, 33);
            Button5.TabIndex = 10;
            Button5.Text = "Documentation";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Label14
            // 
            Label14.BackColor = Color.Transparent;
            Label14.Dock = DockStyle.Fill;
            Label14.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label14.Location = new Point(0, 40);
            Label14.Name = "Label14";
            Label14.Padding = new Padding(20, 10, 0, 0);
            Label14.Size = new Size(412, 262);
            Label14.TabIndex = 9;
            Label14.Text = @"You can add links of servers\GitHub repositories to get more themes through WinPa" + "letter Store. Visit this documentation to know more about Store source extension" + "";
            // 
            // PictureBox6
            // 
            PictureBox6.BackColor = Color.Transparent;
            PictureBox6.Dock = DockStyle.Right;
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(412, 40);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(240, 262);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 11;
            PictureBox6.TabStop = false;
            // 
            // Label15
            // 
            Label15.BackColor = Color.FromArgb(50, 100, 100, 100);
            Label15.Dock = DockStyle.Top;
            Label15.Font = new Font("Segoe UI", 12.75f, FontStyle.Bold);
            Label15.Location = new Point(0, 0);
            Label15.Name = "Label15";
            Label15.Padding = new Padding(5, 5, 0, 0);
            Label15.Size = new Size(652, 40);
            Label15.TabIndex = 8;
            Label15.Text = "You can extend the sources from which WinPaletter Store can get themes!";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(Button3);
            TabPage3.Controls.Add(Label5);
            TabPage3.Controls.Add(PictureBox3);
            TabPage3.Controls.Add(Label6);
            TabPage3.Location = new Point(4, 24);
            TabPage3.Name = "TabPage3";
            TabPage3.Size = new Size(652, 302);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "4";
            // 
            // Button3
            // 
            Button3.BackColor = Color.FromArgb(50, 50, 50);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(0, 81, 210);
            Button3.Location = new Point(23, 121);
            Button3.Name = "Button3";
            Button3.Size = new Size(157, 33);
            Button3.TabIndex = 6;
            Button3.Text = "Documentation";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Label5
            // 
            Label5.BackColor = Color.Transparent;
            Label5.Dock = DockStyle.Fill;
            Label5.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5.Location = new Point(0, 40);
            Label5.Name = "Label5";
            Label5.Padding = new Padding(20, 10, 0, 0);
            Label5.Size = new Size(412, 262);
            Label5.TabIndex = 5;
            Label5.Text = "This is an optional feature. Visit this documentation to know more about Store se" + @"rver\GitHub repository creation.";
            // 
            // PictureBox3
            // 
            PictureBox3.BackColor = Color.Transparent;
            PictureBox3.Dock = DockStyle.Right;
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(412, 40);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(240, 262);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 0;
            PictureBox3.TabStop = false;
            // 
            // Label6
            // 
            Label6.BackColor = Color.FromArgb(50, 100, 100, 100);
            Label6.Dock = DockStyle.Top;
            Label6.Font = new Font("Segoe UI", 12.75f, FontStyle.Bold);
            Label6.Location = new Point(0, 0);
            Label6.Name = "Label6";
            Label6.Padding = new Padding(5, 5, 0, 0);
            Label6.Size = new Size(652, 40);
            Label6.TabIndex = 4;
            Label6.Text = "You can create your own WinPaletter Store online source!";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(AnimatedBox2);
            TabPage6.Location = new Point(4, 24);
            TabPage6.Name = "TabPage6";
            TabPage6.Size = new Size(652, 302);
            TabPage6.TabIndex = 5;
            TabPage6.Text = "5";
            // 
            // AnimatedBox2
            // 
            AnimatedBox2.Color = Color.FromArgb(21, 115, 182);
            AnimatedBox2.Color1 = Color.FromArgb(21, 115, 182);
            AnimatedBox2.Color2 = Color.FromArgb(192, 3, 28);
            AnimatedBox2.Controls.Add(Label11);
            AnimatedBox2.Controls.Add(PictureBox5);
            AnimatedBox2.Controls.Add(Label12);
            AnimatedBox2.Dock = DockStyle.Fill;
            AnimatedBox2.Location = new Point(0, 0);
            AnimatedBox2.Name = "AnimatedBox2";
            AnimatedBox2.Size = new Size(652, 302);
            AnimatedBox2.Style = UI.WP.AnimatedBox.Styles.SwapColors;
            AnimatedBox2.TabIndex = 1;
            AnimatedBox2.Text = "AnimatedBox2";
            // 
            // Label11
            // 
            Label11.BackColor = Color.Transparent;
            Label11.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label11.Location = new Point(176, 227);
            Label11.Name = "Label11";
            Label11.Size = new Size(301, 24);
            Label11.TabIndex = 2;
            Label11.Text = "Thanks for using WinPaletter Store";
            Label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox5
            // 
            PictureBox5.BackColor = Color.Transparent;
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(262, 52);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(128, 128);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 0;
            PictureBox5.TabStop = false;
            // 
            // Label12
            // 
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label12.Location = new Point(176, 187);
            Label12.Name = "Label12";
            Label12.Size = new Size(301, 40);
            Label12.TabIndex = 1;
            Label12.Text = "That's it!";
            Label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Store_Intro
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(660, 380);
            Controls.Add(TablessControl1);
            Controls.Add(Panel1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Store_Intro";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Store tips";
            TopMost = true;
            Panel1.ResumeLayout(false);
            TablessControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            TabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            TabPage6.ResumeLayout(false);
            AnimatedBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            Load += new EventHandler(Store_Intro_Load);
            FormClosing += new FormClosingEventHandler(Store_Intro_FormClosing);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal UI.WP.TablessControl TablessControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Panel Panel1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal Label Label1;
        internal Label Label2;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal PictureBox PictureBox2;
        internal Label Label4;
        internal Label Label3;
        internal TabPage TabPage3;
        internal UI.WP.Button Button3;
        internal Label Label5;
        internal Label Label6;
        internal PictureBox PictureBox3;
        internal TabPage TabPage4;
        internal Label Label7;
        internal Label Label8;
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
        internal Label Label15;
    }
}