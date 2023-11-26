using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class WindowsTerminalCopycat : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsTerminalCopycat));
            Label163 = new Label();
            PictureBox33 = new PictureBox();
            ComboBox1 = new UI.WP.ComboBox();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            ((System.ComponentModel.ISupportInitialize)PictureBox33).BeginInit();
            SuspendLayout();
            // 
            // Label163
            // 
            Label163.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label163.BackColor = Color.Transparent;
            Label163.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label163.Location = new Point(42, 12);
            Label163.Name = "Label163";
            Label163.Size = new Size(418, 24);
            Label163.TabIndex = 191;
            Label163.Text = "Copycat from:";
            Label163.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox33
            // 
            PictureBox33.Image = (Image)resources.GetObject("PictureBox33.Image");
            PictureBox33.Location = new Point(12, 12);
            PictureBox33.Name = "PictureBox33";
            PictureBox33.Size = new Size(24, 24);
            PictureBox33.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox33.TabIndex = 192;
            PictureBox33.TabStop = false;
            // 
            // ComboBox1
            // 
            ComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Location = new Point(45, 38);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(415, 26);
            ComboBox1.TabIndex = 193;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.Location = new Point(294, 75);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 195;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.Location = new Point(380, 75);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 34);
            Button1.TabIndex = 194;
            Button1.Text = "OK";
            Button1.UseVisualStyleBackColor = false;
            // 
            // WindowsTerminalCopycat
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(472, 121);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(ComboBox1);
            Controls.Add(Label163);
            Controls.Add(PictureBox33);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WindowsTerminalCopycat";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Copycat";
            ((System.ComponentModel.ISupportInitialize)PictureBox33).EndInit();
            Load += new EventHandler(WindowsTerminalCopycat_Load);
            ResumeLayout(false);

        }

        internal Label Label163;
        internal PictureBox PictureBox33;
        internal UI.WP.ComboBox ComboBox1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
    }
}
