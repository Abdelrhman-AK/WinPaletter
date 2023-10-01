using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class TerminalsDashboard : Form
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalsDashboard));
            Label49 = new Label();
            Label2 = new Label();
            PictureBox1 = new PictureBox();
            ToolTip1 = new ToolTip(components);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Separator3 = new UI.WP.SeparatorH();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Separator1 = new UI.WP.SeparatorH();
            SeparatorVertical1 = new UI.WP.SeparatorV();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Label49
            // 
            Label49.BackColor = Color.Transparent;
            Label49.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label49.Location = new Point(6, 7);
            Label49.Name = "Label49";
            Label49.Size = new Size(149, 19);
            Label49.TabIndex = 84;
            Label49.Text = "Consoles:";
            Label49.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(170, 7);
            Label2.Name = "Label2";
            Label2.Size = new Size(116, 19);
            Label2.TabIndex = 92;
            Label2.Text = "Windows Terminal:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.Location = new Point(304, 7);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(18, 18);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 97;
            PictureBox1.TabStop = false;
            ToolTip1.SetToolTip(PictureBox1, "It is effective for Windows 10 and Windows 11 (If you have installed Windows Term" + "inal from the Store)");
            // 
            // Button5
            // 
            Button5.BackColor = Color.FromArgb(34, 34, 34);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.ImageAlign = ContentAlignment.MiddleRight;
            Button5.LineColor = Color.FromArgb(79, 105, 109);
            Button5.Location = new Point(173, 68);
            Button5.Name = "Button5";
            Button5.Size = new Size(149, 27);
            Button5.TabIndex = 95;
            Button5.Text = "Preview";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            Button6.BackColor = Color.FromArgb(34, 34, 34);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.ImageAlign = ContentAlignment.MiddleRight;
            Button6.LineColor = Color.FromArgb(55, 55, 55);
            Button6.Location = new Point(173, 37);
            Button6.Name = "Button6";
            Button6.Size = new Size(149, 27);
            Button6.TabIndex = 94;
            Button6.Text = "Stable";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Separator3
            // 
            Separator3.AlternativeLook = false;
            Separator3.Location = new Point(173, 30);
            Separator3.Name = "Separator3";
            Separator3.Size = new Size(149, 1);
            Separator3.TabIndex = 93;
            Separator3.TabStop = false;
            Separator3.Text = "Separator3";
            // 
            // Button3
            // 
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.ImageAlign = ContentAlignment.MiddleRight;
            Button3.LineColor = Color.FromArgb(24, 97, 147);
            Button3.Location = new Point(7, 98);
            Button3.Name = "Button3";
            Button3.Size = new Size(149, 27);
            Button3.TabIndex = 91;
            Button3.Text = "PowerShell x64";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.BackColor = Color.FromArgb(34, 34, 34);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.ImageAlign = ContentAlignment.MiddleRight;
            Button4.LineColor = Color.FromArgb(24, 97, 147);
            Button4.Location = new Point(7, 67);
            Button4.Name = "Button4";
            Button4.Size = new Size(149, 27);
            Button4.TabIndex = 90;
            Button4.Text = "PowerShell x86";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = (Image)resources.GetObject("Button2.Image");
            Button2.ImageAlign = ContentAlignment.MiddleRight;
            Button2.LineColor = Color.FromArgb(73, 76, 78);
            Button2.Location = new Point(7, 129);
            Button2.Name = "Button2";
            Button2.Size = new Size(149, 27);
            Button2.TabIndex = 87;
            Button2.Text = "External";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleRight;
            Button1.LineColor = Color.FromArgb(71, 71, 71);
            Button1.Location = new Point(7, 36);
            Button1.Name = "Button1";
            Button1.Size = new Size(149, 27);
            Button1.TabIndex = 86;
            Button1.Text = "Command Prompt";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Location = new Point(7, 30);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(149, 1);
            Separator1.TabIndex = 85;
            Separator1.TabStop = false;
            // 
            // SeparatorVertical1
            // 
            SeparatorVertical1.AlternativeLook = false;
            SeparatorVertical1.Location = new Point(162, 7);
            SeparatorVertical1.Name = "SeparatorVertical1";
            SeparatorVertical1.Size = new Size(1, 149);
            SeparatorVertical1.TabIndex = 102;
            SeparatorVertical1.TabStop = false;
            // 
            // TerminalsDashboard
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(331, 163);
            ControlBox = false;
            Controls.Add(SeparatorVertical1);
            Controls.Add(PictureBox1);
            Controls.Add(Button5);
            Controls.Add(Button6);
            Controls.Add(Separator3);
            Controls.Add(Button3);
            Controls.Add(Button4);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(Separator1);
            Controls.Add(Label49);
            Controls.Add(Label2);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TerminalsDashboard";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Terminals Dashboard";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(TerminalsDashboard_Load);
            FormClosing += new FormClosingEventHandler(SubMenu_FormClosing);
            Shown += new EventHandler(TerminalsDashboard_Shown);
            ResumeLayout(false);

        }

        internal Label Label49;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal UI.WP.SeparatorH Separator3;
        internal Label Label2;
        internal PictureBox PictureBox1;
        internal ToolTip ToolTip1;
        internal UI.WP.SeparatorV SeparatorVertical1;
    }
}