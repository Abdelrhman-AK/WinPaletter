using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_SearchFilter : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_SearchFilter));
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Label6 = new Label();
            PictureBox16 = new PictureBox();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox2 = new UI.WP.CheckBox();
            CheckBox3 = new UI.WP.CheckBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            SuspendLayout();
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.Location = new Point(269, 245);
            Button1.Name = "Button1";
            Button1.Size = new Size(90, 34);
            Button1.TabIndex = 0;
            Button1.Text = "Save filter";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.Location = new Point(183, 245);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 207;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Label6
            // 
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(42, 12);
            Label6.Name = "Label6";
            Label6.Size = new Size(172, 24);
            Label6.TabIndex = 209;
            Label6.Text = "Search through:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox16
            // 
            PictureBox16.BackColor = Color.Transparent;
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(12, 12);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(24, 24);
            PictureBox16.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox16.TabIndex = 208;
            PictureBox16.TabStop = false;
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(45, 39);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(314, 24);
            CheckBox1.TabIndex = 210;
            CheckBox1.Text = "Themes names";
            // 
            // CheckBox2
            // 
            CheckBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = true;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(45, 99);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(314, 24);
            CheckBox2.TabIndex = 211;
            CheckBox2.Text = "Authors names";
            // 
            // CheckBox3
            // 
            CheckBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox3.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox3.Checked = true;
            CheckBox3.Font = new Font("Segoe UI", 9.0f);
            CheckBox3.ForeColor = Color.White;
            CheckBox3.Location = new Point(45, 69);
            CheckBox3.Name = "CheckBox3";
            CheckBox3.Size = new Size(314, 24);
            CheckBox3.TabIndex = 212;
            CheckBox3.Text = "Themes descriptions";
            // 
            // Store_SearchFilter
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(371, 291);
            Controls.Add(CheckBox3);
            Controls.Add(CheckBox2);
            Controls.Add(CheckBox1);
            Controls.Add(Label6);
            Controls.Add(PictureBox16);
            Controls.Add(Button7);
            Controls.Add(Button1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Store_SearchFilter";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Search filter";
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            Load += new EventHandler(Store_SearchFilter_Load);
            ResumeLayout(false);

        }

        internal UI.WP.Button Button1;
        internal UI.WP.Button Button7;
        internal Label Label6;
        internal PictureBox PictureBox16;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.CheckBox CheckBox3;
    }
}
