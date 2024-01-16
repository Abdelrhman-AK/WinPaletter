namespace WinPaletter
{
    partial class Win32UI_Fullscreen
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
            this.retroDesktopColors1 = new WinPaletter.Templates.RetroDesktopColors();
            this.SuspendLayout();
            // 
            // retroDesktopColors1
            // 
            this.retroDesktopColors1.ActiveBorder = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ActiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.AppWorkspace = System.Drawing.Color.Empty;
            this.retroDesktopColors1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.retroDesktopColors1.Background = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonAlternateFace = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonDkShadow = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonFace = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonHilight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonLight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonShadow = System.Drawing.Color.Empty;
            this.retroDesktopColors1.ButtonText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Desktop = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.retroDesktopColors1.EnableEditingColors = true;
            this.retroDesktopColors1.EnableGradient = true;
            this.retroDesktopColors1.EnableTheming = false;
            this.retroDesktopColors1.GradientActiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.GradientInactiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.GrayText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Hilight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.HilightText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.HotTrackingColor = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InactiveBorder = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InactiveTitle = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InactiveTitleText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InfoText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.InfoWindow = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Location = new System.Drawing.Point(0, 0);
            this.retroDesktopColors1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.retroDesktopColors1.Menu = System.Drawing.Color.Empty;
            this.retroDesktopColors1.MenuBar = System.Drawing.Color.Empty;
            this.retroDesktopColors1.MenuHilight = System.Drawing.Color.Empty;
            this.retroDesktopColors1.MenuText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Name = "retroDesktopColors1";
            this.retroDesktopColors1.Scrollbar = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Size = new System.Drawing.Size(721, 476);
            this.retroDesktopColors1.TabIndex = 0;
            this.retroDesktopColors1.TitleText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.Window = System.Drawing.Color.Empty;
            this.retroDesktopColors1.WindowFrame = System.Drawing.Color.Empty;
            this.retroDesktopColors1.WindowText = System.Drawing.Color.Empty;
            this.retroDesktopColors1.EditorInvoker += new WinPaletter.Templates.RetroDesktopColors.EditorInvokerEventHandler(this.retroDesktopColors1_EditorInvoker);
            this.retroDesktopColors1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.retroDesktopColors1_KeyDown);
            // 
            // Win32UI_Fullscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(721, 476);
            this.Controls.Add(this.retroDesktopColors1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Win32UI_Fullscreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fullscreen preview for Classic Colors";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Win32UI_Fullscreen_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Win32UI_Fullscreen_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public Templates.RetroDesktopColors retroDesktopColors1;
    }
}