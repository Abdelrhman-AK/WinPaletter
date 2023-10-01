using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class VS2Metrics : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(VS2Metrics));
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click);
            TextBox1 = new UI.WP.TextBox();
            PictureBox17 = new PictureBox();
            Label102 = new Label();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            OpenFileDialog2 = new OpenFileDialog();
            Label1 = new Label();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox2 = new UI.WP.CheckBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            SuspendLayout();
            // 
            // Button16
            // 
            Button16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button16.BackColor = Color.FromArgb(34, 34, 34);
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = (Image)resources.GetObject("Button16.Image");
            Button16.LineColor = Color.FromArgb(164, 125, 25);
            Button16.Location = new Point(466, 12);
            Button16.Name = "Button16";
            Button16.Size = new Size(32, 24);
            Button16.TabIndex = 197;
            Button16.UseVisualStyleBackColor = false;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(87, 12);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(373, 24);
            TextBox1.TabIndex = 196;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // PictureBox17
            // 
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(12, 12);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(24, 24);
            PictureBox17.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox17.TabIndex = 195;
            PictureBox17.TabStop = false;
            // 
            // Label102
            // 
            Label102.BackColor = Color.Transparent;
            Label102.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label102.ForeColor = Color.White;
            Label102.Location = new Point(42, 12);
            Label102.Name = "Label102";
            Label102.Size = new Size(39, 24);
            Label102.TabIndex = 194;
            Label102.Text = "File:";
            Label102.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.LineColor = Color.FromArgb(199, 49, 61);
            Button7.Location = new Point(242, 155);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 209;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button8.BackColor = Color.FromArgb(34, 34, 34);
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleLeft;
            Button8.LineColor = Color.FromArgb(52, 20, 64);
            Button8.Location = new Point(328, 155);
            Button8.Name = "Button8";
            Button8.Size = new Size(170, 34);
            Button8.TabIndex = 208;
            Button8.Text = @"Load into metrics\fonts";
            Button8.UseVisualStyleBackColor = false;
            // 
            // OpenFileDialog2
            // 
            OpenFileDialog2.DefaultExt = "wpt";
            OpenFileDialog2.Filter = "Visual Styles File (*.msstyles)|*.msstyles|Theme File (*.theme)|*.theme";
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Italic, GraphicsUnit.Point, 0);
            Label1.ForeColor = Color.White;
            Label1.Location = new Point(84, 39);
            Label1.Name = "Label1";
            Label1.Size = new Size(376, 44);
            Label1.TabIndex = 210;
            Label1.Text = "It can be .msstyles or .theme file" + '\r' + '\n' + ".theme file will be used to use the associate" + "d .msstyles file in it";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(12, 86);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(486, 23);
            CheckBox1.TabIndex = 211;
            CheckBox1.Text = "Include metrics";
            // 
            // CheckBox2
            // 
            CheckBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = true;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(12, 115);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(486, 23);
            CheckBox2.TabIndex = 212;
            CheckBox2.Text = "Include fonts";
            // 
            // VS2Metrics
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(510, 201);
            Controls.Add(CheckBox2);
            Controls.Add(CheckBox1);
            Controls.Add(Label1);
            Controls.Add(Button7);
            Controls.Add(Button8);
            Controls.Add(Button16);
            Controls.Add(TextBox1);
            Controls.Add(PictureBox17);
            Controls.Add(Label102);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VS2Metrics";
            StartPosition = FormStartPosition.CenterParent;
            Text = @"Theme\Visual styles to metrics\fonts";
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            Load += new EventHandler(VS2Win32UI_Load);
            ResumeLayout(false);

        }

        internal UI.WP.Button Button16;
        internal UI.WP.TextBox TextBox1;
        internal PictureBox PictureBox17;
        internal Label Label102;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal OpenFileDialog OpenFileDialog2;
        internal Label Label1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
    }
}