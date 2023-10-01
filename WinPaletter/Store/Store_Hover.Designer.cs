using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Store_Hover : Form
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
            SuspendLayout();
            // 
            // Store_Hover
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(528, 297);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Store_Hover";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Store Hover";
            TopMost = true;
            Load += new EventHandler(Store_Hover_Load);
            Shown += new EventHandler(Store_Hover_Shown);
            MouseClick += new MouseEventHandler(Store_Hover_MouseClick);
            KeyUp += new KeyEventHandler(Store_Hover_KeyUp);
            MouseWheel += new MouseEventHandler(Store_Hover_MouseWheel);
            FormClosed += new FormClosedEventHandler(Store_Hover_FormClosed);
            ResumeLayout(false);

        }
    }
}