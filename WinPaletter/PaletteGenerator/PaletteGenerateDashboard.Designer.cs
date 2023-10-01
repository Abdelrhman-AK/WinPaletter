using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class PaletteGenerateDashboard : Form
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerateDashboard));
            ToolTip1 = new ToolTip(components);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            SuspendLayout();
            // 
            // ToolTip1
            // 
            ToolTip1.BackColor = Color.Black;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(35, 35, 35);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.ImageAlign = ContentAlignment.MiddleRight;
            Button4.LineColor = Color.FromArgb(104, 25, 31);
            Button4.Location = new Point(5, 45);
            Button4.Name = "Button4";
            Button4.Size = new Size(253, 32);
            Button4.TabIndex = 90;
            Button4.Text = "Generate a palette from a color";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(35, 35, 35);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleRight;
            Button1.LineColor = Color.FromArgb(41, 92, 141);
            Button1.Location = new Point(5, 7);
            Button1.Name = "Button1";
            Button1.Size = new Size(253, 32);
            Button1.TabIndex = 86;
            Button1.Text = "Generate a palette from an image";
            Button1.UseVisualStyleBackColor = false;
            // 
            // PaletteGenerateDashboard
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 24, 24);
            ClientSize = new Size(263, 83);
            ControlBox = false;
            Controls.Add(Button4);
            Controls.Add(Button1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PaletteGenerateDashboard";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Terminals Dashboard";
            Load += new EventHandler(TerminalsDashboard_Load);
            FormClosing += new FormClosingEventHandler(SubMenu_FormClosing);
            Shown += new EventHandler(TerminalsDashboard_Shown);
            ResumeLayout(false);

        }
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button4;
        internal ToolTip ToolTip1;
    }
}