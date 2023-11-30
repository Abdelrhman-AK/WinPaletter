using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_ReplaceText : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_ReplaceText));
            PictureBox25 = new PictureBox();
            Label20 = new Label();
            PictureBox24 = new PictureBox();
            Label18 = new Label();
            TextBox3 = new UI.WP.TextBox();
            TextBox4 = new UI.WP.TextBox();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox2 = new UI.WP.CheckBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).BeginInit();
            SuspendLayout();
            // 
            // PictureBox25
            // 
            PictureBox25.Image = (Image)resources.GetObject("PictureBox25.Image");
            PictureBox25.Location = new Point(12, 12);
            PictureBox25.Name = "PictureBox25";
            PictureBox25.Size = new Size(24, 24);
            PictureBox25.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox25.TabIndex = 63;
            PictureBox25.TabStop = false;
            // 
            // Label20
            // 
            Label20.Font = new("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label20.Location = new Point(42, 12);
            Label20.Name = "Label20";
            Label20.Size = new Size(109, 24);
            Label20.TabIndex = 64;
            Label20.Text = "Find what:";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox24
            // 
            PictureBox24.Image = (Image)resources.GetObject("PictureBox24.Image");
            PictureBox24.Location = new Point(12, 42);
            PictureBox24.Name = "PictureBox24";
            PictureBox24.Size = new Size(24, 24);
            PictureBox24.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox24.TabIndex = 65;
            PictureBox24.TabStop = false;
            // 
            // Label18
            // 
            Label18.Font = new("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label18.Location = new Point(42, 42);
            Label18.Name = "Label18";
            Label18.Size = new Size(109, 24);
            Label18.TabIndex = 66;
            Label18.Text = "Replace with:";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox3
            // 
            TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox3.ForeColor = Color.White;
            TextBox3.Location = new Point(157, 12);
            TextBox3.MaxLength = 32767;
            TextBox3.Multiline = false;
            TextBox3.Name = "TextBox3";
            TextBox3.ReadOnly = false;
            TextBox3.Scrollbars = ScrollBars.None;
            TextBox3.SelectedText = "";
            TextBox3.SelectionLength = 0;
            TextBox3.SelectionStart = 0;
            TextBox3.Size = new Size(275, 24);
            TextBox3.TabIndex = 67;
            TextBox3.TextAlign = HorizontalAlignment.Left;
            TextBox3.UseSystemPasswordChar = false;
            TextBox3.WordWrap = true;
            // 
            // TextBox4
            // 
            TextBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox4.ForeColor = Color.White;
            TextBox4.Location = new Point(157, 42);
            TextBox4.MaxLength = 32767;
            TextBox4.Multiline = false;
            TextBox4.Name = "TextBox4";
            TextBox4.ReadOnly = false;
            TextBox4.Scrollbars = ScrollBars.None;
            TextBox4.SelectedText = "";
            TextBox4.SelectionLength = 0;
            TextBox4.SelectionStart = 0;
            TextBox4.Size = new Size(275, 24);
            TextBox4.TabIndex = 68;
            TextBox4.TextAlign = HorizontalAlignment.Left;
            TextBox4.UseSystemPasswordChar = false;
            TextBox4.WordWrap = true;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);

            Button7.Font = new("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.Location = new Point(266, 175);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 207;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);

            Button2.Font = new("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.ImageAlign = ContentAlignment.MiddleLeft;
            Button2.Location = new Point(352, 175);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 206;
            Button2.Text = "Replace";
            Button2.UseVisualStyleBackColor = false;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = false;
            CheckBox1.Font = new("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(157, 72);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(190, 23);
            CheckBox1.TabIndex = 208;
            CheckBox1.Text = "Match case";
            // 
            // CheckBox2
            // 
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = false;
            CheckBox2.Font = new("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(157, 101);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(190, 23);
            CheckBox2.TabIndex = 209;
            CheckBox2.Text = "Match whole word";
            // 
            // Lang_ReplaceText
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(444, 221);
            Controls.Add(CheckBox2);
            Controls.Add(CheckBox1);
            Controls.Add(Button7);
            Controls.Add(Button2);
            Controls.Add(PictureBox25);
            Controls.Add(Label20);
            Controls.Add(PictureBox24);
            Controls.Add(Label18);
            Controls.Add(TextBox3);
            Controls.Add(TextBox4);
            Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Lang_ReplaceText";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Replace";
            ((System.ComponentModel.ISupportInitialize)PictureBox25).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).EndInit();
            Load += new EventHandler(Lang_ReplaceText_Load);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox25;
        internal Label Label20;
        internal PictureBox PictureBox24;
        internal Label Label18;
        internal UI.WP.TextBox TextBox3;
        internal UI.WP.TextBox TextBox4;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button2;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
    }
}
