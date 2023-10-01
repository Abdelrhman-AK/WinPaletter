using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUI : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI));
            GroupBox3 = new UI.WP.GroupBox();
            GroupBox21 = new UI.WP.GroupBox();
            LogonUI_Lockscreen_Toggle = new UI.WP.Toggle();
            PictureBox22 = new PictureBox();
            Label20 = new Label();
            GroupBox19 = new UI.WP.GroupBox();
            LogonUI_Background_Toggle = new UI.WP.Toggle();
            PictureBox16 = new PictureBox();
            Label18 = new Label();
            GroupBox17 = new UI.WP.GroupBox();
            LogonUI_Acrylic_Toggle = new UI.WP.Toggle();
            PictureBox15 = new PictureBox();
            Label16 = new Label();
            PictureBox6 = new PictureBox();
            Label13 = new Label();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Separator1 = new UI.WP.SeparatorH();
            GroupBox12 = new UI.WP.GroupBox();
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label12 = new Label();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            OpenFileDialog1 = new OpenFileDialog();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            GroupBox3.SuspendLayout();
            GroupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            GroupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            GroupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            GroupBox12.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(GroupBox21);
            GroupBox3.Controls.Add(GroupBox19);
            GroupBox3.Controls.Add(GroupBox17);
            GroupBox3.Controls.Add(PictureBox6);
            GroupBox3.Controls.Add(Label13);
            GroupBox3.Location = new Point(12, 57);
            GroupBox3.Margin = new Padding(4, 3, 4, 3);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(495, 135);
            GroupBox3.TabIndex = 15;
            // 
            // GroupBox21
            // 
            GroupBox21.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox21.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox21.Controls.Add(LogonUI_Lockscreen_Toggle);
            GroupBox21.Controls.Add(PictureBox22);
            GroupBox21.Controls.Add(Label20);
            GroupBox21.Location = new Point(3, 103);
            GroupBox21.Margin = new Padding(4, 3, 4, 3);
            GroupBox21.Name = "GroupBox21";
            GroupBox21.Size = new Size(489, 29);
            GroupBox21.TabIndex = 12;
            // 
            // LogonUI_Lockscreen_Toggle
            // 
            LogonUI_Lockscreen_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LogonUI_Lockscreen_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            LogonUI_Lockscreen_Toggle.Checked = false;
            LogonUI_Lockscreen_Toggle.DarkLight_Toggler = false;
            LogonUI_Lockscreen_Toggle.Location = new Point(445, 4);
            LogonUI_Lockscreen_Toggle.Name = "LogonUI_Lockscreen_Toggle";
            LogonUI_Lockscreen_Toggle.Size = new Size(40, 20);
            LogonUI_Lockscreen_Toggle.TabIndex = 16;
            // 
            // PictureBox22
            // 
            PictureBox22.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(3, 1);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(30, 27);
            PictureBox22.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox22.TabIndex = 4;
            PictureBox22.TabStop = false;
            // 
            // Label20
            // 
            Label20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label20.BackColor = Color.Transparent;
            Label20.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label20.Location = new Point(36, 2);
            Label20.Name = "Label20";
            Label20.Size = new Size(396, 24);
            Label20.TabIndex = 13;
            Label20.Text = "Lockscreen";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox19
            // 
            GroupBox19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox19.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox19.Controls.Add(LogonUI_Background_Toggle);
            GroupBox19.Controls.Add(PictureBox16);
            GroupBox19.Controls.Add(Label18);
            GroupBox19.Location = new Point(3, 72);
            GroupBox19.Margin = new Padding(4, 3, 4, 3);
            GroupBox19.Name = "GroupBox19";
            GroupBox19.Size = new Size(489, 29);
            GroupBox19.TabIndex = 11;
            // 
            // LogonUI_Background_Toggle
            // 
            LogonUI_Background_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LogonUI_Background_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            LogonUI_Background_Toggle.Checked = false;
            LogonUI_Background_Toggle.DarkLight_Toggler = false;
            LogonUI_Background_Toggle.Location = new Point(445, 4);
            LogonUI_Background_Toggle.Name = "LogonUI_Background_Toggle";
            LogonUI_Background_Toggle.Size = new Size(40, 20);
            LogonUI_Background_Toggle.TabIndex = 16;
            // 
            // PictureBox16
            // 
            PictureBox16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(3, 1);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(30, 27);
            PictureBox16.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox16.TabIndex = 4;
            PictureBox16.TabStop = false;
            // 
            // Label18
            // 
            Label18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label18.BackColor = Color.Transparent;
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label18.Location = new Point(36, 2);
            Label18.Name = "Label18";
            Label18.Size = new Size(396, 24);
            Label18.TabIndex = 13;
            Label18.Text = "LogonUI background";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox17
            // 
            GroupBox17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox17.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox17.Controls.Add(LogonUI_Acrylic_Toggle);
            GroupBox17.Controls.Add(PictureBox15);
            GroupBox17.Controls.Add(Label16);
            GroupBox17.Location = new Point(3, 41);
            GroupBox17.Margin = new Padding(4, 3, 4, 3);
            GroupBox17.Name = "GroupBox17";
            GroupBox17.Size = new Size(489, 29);
            GroupBox17.TabIndex = 10;
            // 
            // LogonUI_Acrylic_Toggle
            // 
            LogonUI_Acrylic_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LogonUI_Acrylic_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            LogonUI_Acrylic_Toggle.Checked = false;
            LogonUI_Acrylic_Toggle.DarkLight_Toggler = false;
            LogonUI_Acrylic_Toggle.Location = new Point(445, 4);
            LogonUI_Acrylic_Toggle.Name = "LogonUI_Acrylic_Toggle";
            LogonUI_Acrylic_Toggle.Size = new Size(40, 20);
            LogonUI_Acrylic_Toggle.TabIndex = 16;
            // 
            // PictureBox15
            // 
            PictureBox15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(3, 1);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(30, 27);
            PictureBox15.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox15.TabIndex = 4;
            PictureBox15.TabStop = false;
            // 
            // Label16
            // 
            Label16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label16.BackColor = Color.Transparent;
            Label16.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label16.Location = new Point(36, 2);
            Label16.Name = "Label16";
            Label16.Size = new Size(396, 24);
            Label16.TabIndex = 13;
            Label16.Text = "Acrylic";
            Label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(3, 3);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(35, 35);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 1;
            PictureBox6.TabStop = false;
            // 
            // Label13
            // 
            Label13.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label13.Location = new Point(44, 3);
            Label13.Name = "Label13";
            Label13.Size = new Size(198, 35);
            Label13.TabIndex = 0;
            Label13.Text = "LogonUI and LockScreen";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(241, 215);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 17;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 204);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(495, 1);
            Separator1.TabIndex = 18;
            Separator1.TabStop = false;
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox12.Controls.Add(Button9);
            GroupBox12.Controls.Add(Label12);
            GroupBox12.Controls.Add(Button11);
            GroupBox12.Controls.Add(Button12);
            GroupBox12.Location = new Point(12, 12);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(495, 39);
            GroupBox12.TabIndex = 201;
            // 
            // Button9
            // 
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.ImageAlign = ContentAlignment.MiddleRight;
            Button9.LineColor = Color.FromArgb(90, 134, 117);
            Button9.Location = new Point(222, 5);
            Button9.Name = "Button9";
            Button9.Size = new Size(126, 29);
            Button9.TabIndex = 112;
            Button9.Text = "Current applied";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label12.Location = new Point(4, 4);
            Label12.Name = "Label12";
            Label12.Size = new Size(75, 31);
            Label12.TabIndex = 111;
            Label12.Text = "Open from:";
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            Button11.BackColor = Color.FromArgb(43, 43, 43);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.ImageAlign = ContentAlignment.MiddleRight;
            Button11.LineColor = Color.FromArgb(113, 122, 131);
            Button11.Location = new Point(85, 5);
            Button11.Name = "Button11";
            Button11.Size = new Size(135, 29);
            Button11.TabIndex = 110;
            Button11.Text = "WinPaletter theme";
            Button11.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button12.BackColor = Color.FromArgb(43, 43, 43);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = null;
            Button12.ImageAlign = ContentAlignment.MiddleRight;
            Button12.LineColor = Color.FromArgb(0, 66, 119);
            Button12.Location = new Point(351, 5);
            Button12.Name = "Button12";
            Button12.Size = new Size(135, 29);
            Button12.TabIndex = 108;
            Button12.Text = "Default Windows";
            Button12.UseVisualStyleBackColor = false;
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(52, 20, 64);
            Button1.Location = new Point(327, 215);
            Button1.Name = "Button1";
            Button1.Size = new Size(180, 34);
            Button1.TabIndex = 202;
            Button1.Text = "Load into current theme";
            Button1.UseVisualStyleBackColor = false;
            // 
            // LogonUI
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(519, 261);
            Controls.Add(Button1);
            Controls.Add(GroupBox12);
            Controls.Add(Separator1);
            Controls.Add(Button2);
            Controls.Add(GroupBox3);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogonUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogonUI";
            GroupBox3.ResumeLayout(false);
            GroupBox21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            GroupBox19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            GroupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            GroupBox12.ResumeLayout(false);
            Load += new EventHandler(LogonUI_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.GroupBox GroupBox21;
        internal UI.WP.Toggle LogonUI_Lockscreen_Toggle;
        internal PictureBox PictureBox22;
        internal Label Label20;
        internal UI.WP.GroupBox GroupBox19;
        internal UI.WP.Toggle LogonUI_Background_Toggle;
        internal PictureBox PictureBox16;
        internal Label Label18;
        internal UI.WP.GroupBox GroupBox17;
        internal UI.WP.Toggle LogonUI_Acrylic_Toggle;
        internal PictureBox PictureBox15;
        internal Label Label16;
        internal PictureBox PictureBox6;
        internal Label Label13;
        internal UI.WP.Button Button2;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button1;
    }
}