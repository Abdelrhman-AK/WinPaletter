using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_CPToggles : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Store_CPToggles));
            CheckedListBox1 = new CheckedListBox();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Label17 = new Label();
            PictureBox6 = new PictureBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            SuspendLayout();
            // 
            // CheckedListBox1
            // 
            CheckedListBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckedListBox1.BorderStyle = BorderStyle.None;
            CheckedListBox1.FormattingEnabled = true;
            CheckedListBox1.Location = new Point(45, 56);
            CheckedListBox1.Name = "CheckedListBox1";
            CheckedListBox1.Size = new Size(510, 234);
            CheckedListBox1.TabIndex = 0;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);

            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.Location = new Point(368, 303);
            Button1.Name = "Button1";
            Button1.Size = new Size(187, 34);
            Button1.TabIndex = 1;
            Button1.Text = "Proceed with current selections";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);

            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.Location = new Point(175, 303);
            Button7.Name = "Button7";
            Button7.Size = new Size(187, 34);
            Button7.TabIndex = 205;
            Button7.Text = "Proceed (with all are selected)";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Label17
            // 
            Label17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label17.BackColor = Color.Transparent;
            Label17.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label17.Location = new Point(42, 12);
            Label17.Name = "Label17";
            Label17.Size = new Size(513, 35);
            Label17.TabIndex = 207;
            Label17.Text = "To prevent accidental actions, these features will be modified (in addition to ma" + "in Windows colors):";
            // 
            // PictureBox6
            // 
            PictureBox6.BackColor = Color.Transparent;
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(12, 12);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.TabIndex = 206;
            PictureBox6.TabStop = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);

            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.Location = new Point(89, 303);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 34);
            Button3.TabIndex = 208;
            Button3.Text = "Cancel";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Store_CPToggles
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(567, 349);
            ControlBox = false;
            Controls.Add(Button3);
            Controls.Add(Label17);
            Controls.Add(PictureBox6);
            Controls.Add(Button7);
            Controls.Add(Button1);
            Controls.Add(CheckedListBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Store_CPToggles";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Check the following items";
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            Load += new EventHandler(Store_CPToggles_Load);
            ResumeLayout(false);

        }

        internal CheckedListBox CheckedListBox1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button7;
        internal Label Label17;
        internal PictureBox PictureBox6;
        internal UI.WP.Button Button3;
    }
}
