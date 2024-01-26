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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsTerminalDecide));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label144 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.RadioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.RadioImage1 = new WinPaletter.UI.WP.RadioImage();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.AlertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.Panel1.SuspendLayout();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(12, 12);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(35, 35);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(53, 12);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(344, 107);
            this.Label7.TabIndex = 85;
            this.Label7.Text = "WinPaletter stores the Windows Terminal Stable and Preview configurations in dist" +
    "inct sections within the theme file.\r\n\r\nFrom which one would you prefer to impor" +
    "t into the currently open editor?";
            // 
            // Label144
            // 
            this.Label144.BackColor = System.Drawing.Color.Transparent;
            this.Label144.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label144.Location = new System.Drawing.Point(131, 211);
            this.Label144.Name = "Label144";
            this.Label144.Size = new System.Drawing.Size(64, 20);
            this.Label144.TabIndex = 88;
            this.Label144.Text = "Stable";
            this.Label144.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(213, 211);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(64, 20);
            this.Label1.TabIndex = 89;
            this.Label1.Text = "Preview";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageAsVector = false;
            this.Button2.ImageVector = null;
            this.Button2.Location = new System.Drawing.Point(236, 7);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(80, 34);
            this.Button2.TabIndex = 108;
            this.Button2.Text = "Cancel";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = null;
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.ImageAsVector = false;
            this.Button1.ImageVector = null;
            this.Button1.Location = new System.Drawing.Point(322, 7);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 34);
            this.Button1.TabIndex = 107;
            this.Button1.Text = "OK";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // RadioImage2
            // 
            this.RadioImage2.Checked = false;
            this.RadioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage2.ForeColor = System.Drawing.Color.White;
            this.RadioImage2.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage2.Image")));
            this.RadioImage2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage2.Location = new System.Drawing.Point(213, 144);
            this.RadioImage2.Name = "RadioImage2";
            this.RadioImage2.Size = new System.Drawing.Size(64, 64);
            this.RadioImage2.TabIndex = 87;
            this.RadioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // RadioImage1
            // 
            this.RadioImage1.Checked = false;
            this.RadioImage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioImage1.ForeColor = System.Drawing.Color.White;
            this.RadioImage1.Image = ((System.Drawing.Image)(resources.GetObject("RadioImage1.Image")));
            this.RadioImage1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage1.Location = new System.Drawing.Point(131, 144);
            this.RadioImage1.Name = "RadioImage1";
            this.RadioImage1.Size = new System.Drawing.Size(64, 64);
            this.RadioImage1.TabIndex = 86;
            this.RadioImage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioImage1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(92)))), ((int)(((byte)(144)))));
            this.Panel1.Controls.Add(this.PictureBox1);
            this.Panel1.Controls.Add(this.Label7);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(409, 125);
            this.Panel1.TabIndex = 109;
            // 
            // AlertBox1
            // 
            this.AlertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Adaptive;
            this.AlertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(51)))), ((int)(((byte)(2)))));
            this.AlertBox1.CenterText = false;
            this.AlertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox1.Image = global::WinPaletter.Assets.Notifications.Warning;
            this.AlertBox1.Location = new System.Drawing.Point(12, 248);
            this.AlertBox1.Name = "AlertBox1";
            this.AlertBox1.Size = new System.Drawing.Size(385, 48);
            this.AlertBox1.TabIndex = 199;
            this.AlertBox1.TabStop = false;
            this.AlertBox1.Text = "This will overwrite the current terminal settings, although it won\'t remove them," +
    " even if they are not currently visible.";
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.Button1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 303);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(409, 48);
            this.bottom_buttons.TabIndex = 212;
            // 
            // WindowsTerminalDecide
            // 
            this.AcceptButton = this.Button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(43)))), ((int)(((byte)(67)))));
            this.ClientSize = new System.Drawing.Size(409, 351);
            this.Controls.Add(this.AlertBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label144);
            this.Controls.Add(this.RadioImage2);
            this.Controls.Add(this.RadioImage1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowsTerminalDecide";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Decide";
            this.Load += new System.EventHandler(this.WindowsTerminalDecide_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private UI.WP.GroupBox bottom_buttons;
    }
}
