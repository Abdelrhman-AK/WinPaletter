using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class NewExtTerminal : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(NewExtTerminal));
            PictureBox17 = new PictureBox();
            Label102 = new Label();
            OpenFileDialog1 = new OpenFileDialog();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            AlertBox1 = new UI.WP.AlertBox();
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click);
            TextBox1 = new UI.WP.TextBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            SuspendLayout();
            // 
            // PictureBox17
            // 
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(12, 12);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(24, 24);
            PictureBox17.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox17.TabIndex = 101;
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
            Label102.TabIndex = 100;
            Label102.Text = "Path:";
            Label102.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.Filter = "Executable File (*.exe)|*.exe";
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(234, 105);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 201;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(28, 103, 64);
            Button1.Location = new Point(320, 105);
            Button1.Name = "Button1";
            Button1.Size = new Size(160, 34);
            Button1.TabIndex = 200;
            Button1.Text = "Add to registry entry";
            Button1.UseVisualStyleBackColor = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(70, 51, 2);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = My.Resources.notify_warning;
            AlertBox1.Location = new Point(12, 44);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(468, 49);
            AlertBox1.TabIndex = 199;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "This feature is experimental, you should be sure that you are selecting a console" + @" application not WinForm\Desktop application, and in system drive (C:\)";
            // 
            // Button16
            // 
            Button16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button16.BackColor = Color.FromArgb(34, 34, 34);
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = (Image)resources.GetObject("Button16.Image");
            Button16.LineColor = Color.FromArgb(164, 125, 25);
            Button16.Location = new Point(450, 12);
            Button16.Name = "Button16";
            Button16.Size = new Size(30, 24);
            Button16.TabIndex = 193;
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
            TextBox1.Size = new Size(357, 24);
            TextBox1.TabIndex = 102;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // NewExtTerminal
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(492, 151);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(AlertBox1);
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
            Name = "NewExtTerminal";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New external terminal";
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            Load += new EventHandler(NewExtTerminal_Load);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox17;
        internal Label Label102;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.Button Button16;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal OpenFileDialog OpenFileDialog1;
    }
}