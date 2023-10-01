using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class PE_Warning : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(PE_Warning));
            SaveFileDialog1 = new SaveFileDialog();
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Separator1 = new UI.WP.SeparatorH();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            AlertBox1 = new UI.WP.AlertBox();
            CheckBox1 = new UI.WP.CheckBox();
            AnimatedBox1 = new UI.WP.AnimatedBox();
            PictureBox1 = new PictureBox();
            Label7 = new Label();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            GroupBox3 = new UI.WP.GroupBox();
            TreeView1 = new UI.WP.TreeView();
            TreeView1.DoubleClick += new EventHandler(TreeView1_DoubleClick);
            Label4 = new Label();
            PictureBox4 = new PictureBox();
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            SuspendLayout();
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.Filter = "Text File (*.txt)|*.txt";
            // 
            // Button4
            // 
            Button4.BackColor = Color.FromArgb(50, 50, 50);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.ImageAlign = ContentAlignment.MiddleLeft;
            Button4.LineColor = Color.FromArgb(134, 97, 68);
            Button4.Location = new Point(495, 400);
            Button4.Name = "Button4";
            Button4.Size = new Size(220, 30);
            Button4.TabIndex = 127;
            Button4.Text = "Restore PE file integrity (health)";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 440);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(850, 1);
            Separator1.TabIndex = 126;
            Separator1.TabStop = false;
            Separator1.Text = "Separator1";
            // 
            // Button3
            // 
            Button3.BackColor = Color.FromArgb(50, 50, 50);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.ImageAlign = ContentAlignment.MiddleLeft;
            Button3.LineColor = Color.FromArgb(29, 107, 147);
            Button3.Location = new Point(13, 400);
            Button3.Name = "Button3";
            Button3.Size = new Size(340, 30);
            Button3.TabIndex = 125;
            Button3.Text = "Know more about Windows Security (Defender) issue";
            Button3.UseVisualStyleBackColor = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(6, 47, 70);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = (Image)resources.GetObject("AlertBox1.Image");
            AlertBox1.Location = new Point(12, 346);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(850, 48);
            AlertBox1.TabIndex = 124;
            AlertBox1.TabStop = false;
            AlertBox1.Text = resources.GetString("AlertBox1.Text");
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(12, 453);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(608, 24);
            CheckBox1.TabIndex = 123;
            CheckBox1.Text = "Always ignore this dialog and do action selected in Settings > Theme applying beh" + "avior > PE pathcing";
            // 
            // AnimatedBox1
            // 
            AnimatedBox1.BackColor = Color.FromArgb(125, 93, 4);
            AnimatedBox1.Color = Color.FromArgb(125, 93, 4);
            AnimatedBox1.Color1 = Color.FromArgb(125, 93, 4);
            AnimatedBox1.Color2 = Color.FromArgb(254, 191, 10);
            AnimatedBox1.Controls.Add(PictureBox1);
            AnimatedBox1.Controls.Add(Label7);
            AnimatedBox1.Dock = DockStyle.Top;
            AnimatedBox1.Location = new Point(0, 0);
            AnimatedBox1.Name = "AnimatedBox1";
            AnimatedBox1.Size = new Size(874, 48);
            AnimatedBox1.Style = UI.WP.AnimatedBox.Styles.MixedColors;
            AnimatedBox1.TabIndex = 121;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 7);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(35, 35);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label7.Location = new Point(47, 7);
            Label7.Name = "Label7";
            Label7.Size = new Size(817, 35);
            Label7.TabIndex = 85;
            Label7.Text = "WinPaletter will modify a system PE file and this will change its resources and i" + "ntegrity.";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(215, 20, 20);
            Button2.Location = new Point(626, 448);
            Button2.Name = "Button2";
            Button2.Size = new Size(115, 34);
            Button2.TabIndex = 117;
            Button2.Text = "Don't modify";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(70, 83, 55);
            Button1.Location = new Point(747, 448);
            Button1.Name = "Button1";
            Button1.Size = new Size(115, 34);
            Button1.TabIndex = 116;
            Button1.Text = "Modify";
            Button1.UseVisualStyleBackColor = false;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(TreeView1);
            GroupBox3.Controls.Add(Label4);
            GroupBox3.Controls.Add(PictureBox4);
            GroupBox3.Location = new Point(13, 59);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(850, 280);
            GroupBox3.TabIndex = 114;
            // 
            // TreeView1
            // 
            TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TreeView1.BorderStyle = BorderStyle.None;
            TreeView1.Indent = 15;
            TreeView1.ItemHeight = 20;
            TreeView1.Location = new Point(3, 32);
            TreeView1.Name = "TreeView1";
            TreeView1.Size = new Size(843, 245);
            TreeView1.TabIndex = 122;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4.Location = new Point(33, 5);
            Label4.Name = "Label4";
            Label4.Size = new Size(813, 24);
            Label4.TabIndex = 86;
            Label4.Text = "Action details:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(3, 5);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 1;
            PictureBox4.TabStop = false;
            // 
            // Button5
            // 
            Button5.BackColor = Color.FromArgb(50, 50, 50);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.ImageAlign = ContentAlignment.MiddleLeft;
            Button5.LineColor = Color.FromArgb(29, 107, 147);
            Button5.Location = new Point(359, 400);
            Button5.Name = "Button5";
            Button5.Size = new Size(130, 30);
            Button5.TabIndex = 128;
            Button5.Text = "Help (Wiki)";
            Button5.UseVisualStyleBackColor = false;
            // 
            // PE_Warning
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(874, 494);
            Controls.Add(Button5);
            Controls.Add(Button4);
            Controls.Add(Separator1);
            Controls.Add(Button3);
            Controls.Add(AlertBox1);
            Controls.Add(CheckBox1);
            Controls.Add(AnimatedBox1);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(GroupBox3);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(645, 435);
            Name = "PE_Warning";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PE resources editor";
            TopMost = true;
            AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            Load += new EventHandler(BugReport_Load);
            FormClosing += new FormClosingEventHandler(PE_Warning_FormClosing);
            ResumeLayout(false);

        }
        internal PictureBox PictureBox1;
        internal Label Label7;
        internal UI.WP.GroupBox GroupBox3;
        internal Label Label4;
        internal PictureBox PictureBox4;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal SaveFileDialog SaveFileDialog1;
        internal UI.WP.TreeView TreeView1;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.Button Button3;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
    }
}