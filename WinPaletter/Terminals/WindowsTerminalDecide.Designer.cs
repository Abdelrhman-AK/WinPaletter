using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class WindowsTerminalDecide : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsTerminalDecide));
            PictureBox1 = new PictureBox();
            Label7 = new Label();
            Label144 = new Label();
            Label1 = new Label();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            RadioImage2 = new UI.WP.RadioImage();
            RadioImage1 = new UI.WP.RadioImage();
            Panel1 = new Panel();
            AlertBox1 = new UI.WP.AlertBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(12, 12);
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
            Label7.Location = new Point(53, 12);
            Label7.Name = "Label7";
            Label7.Size = new Size(344, 89);
            Label7.TabIndex = 85;
            Label7.Text = "WinPaletter saves the Windows Terminal Stable and Preview in separete areas in th" + "e theme file." + '\r' + '\n' + '\r' + '\n' + "From which one do you want to be imported into current open edi" + "tor?";
            // 
            // Label144
            // 
            Label144.BackColor = Color.Transparent;
            Label144.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label144.Location = new Point(131, 197);
            Label144.Name = "Label144";
            Label144.Size = new Size(64, 20);
            Label144.TabIndex = 88;
            Label144.Text = "Stable";
            Label144.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(213, 197);
            Label1.Name = "Label1";
            Label1.Size = new Size(64, 20);
            Label1.TabIndex = 89;
            Label1.Text = "Preview";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(14, 51, 75);
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(231, 305);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 108;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(14, 51, 75);
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(52, 20, 64);
            Button1.Location = new Point(317, 305);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 34);
            Button1.TabIndex = 107;
            Button1.Text = "OK";
            Button1.UseVisualStyleBackColor = false;
            // 
            // RadioImage2
            // 
            RadioImage2.Checked = false;
            RadioImage2.Font = new Font("Segoe UI", 9.0f);
            RadioImage2.ForeColor = Color.White;
            RadioImage2.Image = (Image)resources.GetObject("RadioImage2.Image");
            RadioImage2.Location = new Point(213, 130);
            RadioImage2.Name = "RadioImage2";
            RadioImage2.ShowText = false;
            RadioImage2.Size = new Size(64, 64);
            RadioImage2.TabIndex = 87;
            // 
            // RadioImage1
            // 
            RadioImage1.Checked = false;
            RadioImage1.Font = new Font("Segoe UI", 9.0f);
            RadioImage1.ForeColor = Color.White;
            RadioImage1.Image = (Image)resources.GetObject("RadioImage1.Image");
            RadioImage1.Location = new Point(131, 130);
            RadioImage1.Name = "RadioImage1";
            RadioImage1.ShowText = false;
            RadioImage1.Size = new Size(64, 64);
            RadioImage1.TabIndex = 86;
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.FromArgb(10, 92, 144);
            Panel1.Controls.Add(PictureBox1);
            Panel1.Controls.Add(Label7);
            Panel1.Dock = DockStyle.Top;
            Panel1.Location = new Point(0, 0);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(409, 110);
            Panel1.TabIndex = 109;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(70, 51, 2);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = My.Resources.notify_warning;
            AlertBox1.Location = new Point(12, 248);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(385, 49);
            AlertBox1.TabIndex = 199;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "This will override the already existing terminal settings (but won't delete them " + "even if they are not visible)";
            // 
            // WindowsTerminalDecide
            // 
            AcceptButton = Button1;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(4, 43, 67);
            ClientSize = new Size(409, 351);
            Controls.Add(AlertBox1);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(Label1);
            Controls.Add(Label144);
            Controls.Add(RadioImage2);
            Controls.Add(RadioImage1);
            Controls.Add(Panel1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WindowsTerminalDecide";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Decide";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Panel1.ResumeLayout(false);
            Load += new EventHandler(WindowsTerminalDecide_Load);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal Label Label7;
        internal UI.WP.RadioImage RadioImage1;
        internal UI.WP.RadioImage RadioImage2;
        internal Label Label144;
        internal Label Label1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button1;
        internal Panel Panel1;
        internal UI.WP.AlertBox AlertBox1;
    }
}