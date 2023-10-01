using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Converter_Form : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Converter_Form));
            PictureBox1 = new PictureBox();
            PictureBox2 = new PictureBox();
            Label1 = new Label();
            Label2 = new Label();
            Label3 = new Label();
            SaveFileDialog1 = new SaveFileDialog();
            OpenFileDialog1 = new OpenFileDialog();
            Label4 = new Label();
            PictureBox3 = new PictureBox();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            CheckBox2 = new UI.WP.CheckBox();
            CheckBox1 = new UI.WP.CheckBox();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            SuspendLayout();
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(12, 12);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(12, 42);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.TabIndex = 1;
            PictureBox2.TabStop = false;
            // 
            // Label1
            // 
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(42, 12);
            Label1.Name = "Label1";
            Label1.Size = new Size(81, 24);
            Label1.TabIndex = 2;
            Label1.Text = "Theme file:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(42, 42);
            Label2.Name = "Label2";
            Label2.Size = new Size(81, 24);
            Label2.TabIndex = 3;
            Label2.Text = "Detection:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(126, 47);
            Label3.Name = "Label3";
            Label3.Size = new Size(546, 150);
            Label3.TabIndex = 5;
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.DefaultExt = "wpt";
            SaveFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth";
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // Label4
            // 
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4.Location = new Point(42, 201);
            Label4.Name = "Label4";
            Label4.Size = new Size(81, 24);
            Label4.TabIndex = 29;
            Label4.Text = "Options:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(12, 201);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.TabIndex = 28;
            PictureBox3.TabStop = false;
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
            Button2.Location = new Point(506, 265);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 27;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.ImageAlign = ContentAlignment.MiddleLeft;
            Button3.LineColor = Color.FromArgb(111, 25, 50);
            Button3.Location = new Point(592, 265);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 34);
            Button3.TabIndex = 26;
            Button3.Text = "Export";
            Button3.UseVisualStyleBackColor = false;
            // 
            // CheckBox2
            // 
            CheckBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = false;
            CheckBox2.Enabled = false;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(129, 231);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(543, 24);
            CheckBox2.TabIndex = 8;
            CheckBox2.Text = "Make it valid for old version of WinPaletter less than 1.0.6.9 (old themes format" + ")";
            // 
            // CheckBox1
            // 
            CheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Enabled = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(129, 201);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(543, 24);
            CheckBox1.TabIndex = 7;
            CheckBox1.Text = "Compress contents (for new JSON-internally-formatted themes)";
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(50, 50, 50);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.LineColor = Color.FromArgb(164, 125, 25);
            Button1.Location = new Point(639, 12);
            Button1.Name = "Button1";
            Button1.Size = new Size(33, 24);
            Button1.TabIndex = 6;
            Button1.UseVisualStyleBackColor = false;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(129, 12);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(504, 24);
            TextBox1.TabIndex = 4;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // Converter_Form
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(684, 311);
            Controls.Add(Label4);
            Controls.Add(PictureBox3);
            Controls.Add(Button2);
            Controls.Add(Button3);
            Controls.Add(CheckBox2);
            Controls.Add(CheckBox1);
            Controls.Add(Button1);
            Controls.Add(Label3);
            Controls.Add(TextBox1);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(PictureBox2);
            Controls.Add(PictureBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(700, 350);
            Name = "Converter_Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinPaletter theme converter";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            Load += new EventHandler(Converter_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal PictureBox PictureBox2;
        internal Label Label1;
        internal Label Label2;
        internal UI.WP.TextBox TextBox1;
        internal Label Label3;
        internal UI.WP.Button Button1;
        internal SaveFileDialog SaveFileDialog1;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal Label Label4;
        internal PictureBox PictureBox3;
    }
}