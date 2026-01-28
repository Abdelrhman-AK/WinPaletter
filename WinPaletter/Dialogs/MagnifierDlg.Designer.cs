namespace WinPaletter
{
    partial class MagnifierDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.magnifier1 = new WinPaletter.UI.Controllers.Magnifier();
            this.SuspendLayout();
            // 
            // magnifier1
            // 
            this.magnifier1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.magnifier1.Enabled = false;
            this.magnifier1.Location = new System.Drawing.Point(0, 0);
            this.magnifier1.Name = "magnifier1";
            this.magnifier1.Size = new System.Drawing.Size(96, 96);
            this.magnifier1.TabIndex = 121;
            this.magnifier1.Zoom = 10;
            // 
            // MagnifierDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(96, 96);
            this.Controls.Add(this.magnifier1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MagnifierDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Magnifier";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MagnifierDlg_FormClosed);
            this.Load += new System.EventHandler(this.MagnifierDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controllers.Magnifier magnifier1;
    }
}