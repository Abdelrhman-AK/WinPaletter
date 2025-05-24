namespace WinPaletter.Templates
{
    partial class DesktopIcons
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FakeIcon1 = new WinPaletter.UI.Simulation.WinIcon();
            this.FakeIcon2 = new WinPaletter.UI.Simulation.WinIcon();
            this.FakeIcon3 = new WinPaletter.UI.Simulation.WinIcon();
            this.SuspendLayout();
            // 
            // FakeIcon1
            // 
            this.FakeIcon1.BackColor = System.Drawing.Color.Transparent;
            this.FakeIcon1.ColorGlow = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FakeIcon1.ColorText = System.Drawing.Color.White;
            this.FakeIcon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FakeIcon1.Icon = null;
            this.FakeIcon1.IconSize = 32;
            this.FakeIcon1.Location = new System.Drawing.Point(4, 3);
            this.FakeIcon1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FakeIcon1.Name = "FakeIcon1";
            this.FakeIcon1.Size = new System.Drawing.Size(76, 70);
            this.FakeIcon1.TabIndex = 7;
            this.FakeIcon1.Text = "Icon 1";
            this.FakeIcon1.EditorInvoker += new WinPaletter.UI.Simulation.WinIcon.EditorInvokerEventHandler(this.FakeIconX_EditorInvoker);
            // 
            // FakeIcon2
            // 
            this.FakeIcon2.BackColor = System.Drawing.Color.Transparent;
            this.FakeIcon2.ColorGlow = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FakeIcon2.ColorText = System.Drawing.Color.White;
            this.FakeIcon2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FakeIcon2.Icon = null;
            this.FakeIcon2.IconSize = 32;
            this.FakeIcon2.Location = new System.Drawing.Point(4, 92);
            this.FakeIcon2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FakeIcon2.Name = "FakeIcon2";
            this.FakeIcon2.Size = new System.Drawing.Size(76, 70);
            this.FakeIcon2.TabIndex = 9;
            this.FakeIcon2.Text = "Icon 2";
            this.FakeIcon2.EditorInvoker += new WinPaletter.UI.Simulation.WinIcon.EditorInvokerEventHandler(this.FakeIconX_EditorInvoker);
            // 
            // FakeIcon3
            // 
            this.FakeIcon3.BackColor = System.Drawing.Color.Transparent;
            this.FakeIcon3.ColorGlow = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FakeIcon3.ColorText = System.Drawing.Color.White;
            this.FakeIcon3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FakeIcon3.Icon = null;
            this.FakeIcon3.IconSize = 32;
            this.FakeIcon3.Location = new System.Drawing.Point(105, 3);
            this.FakeIcon3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FakeIcon3.Name = "FakeIcon3";
            this.FakeIcon3.Size = new System.Drawing.Size(76, 70);
            this.FakeIcon3.TabIndex = 8;
            this.FakeIcon3.Tag = "";
            this.FakeIcon3.Text = "Icon 3";
            this.FakeIcon3.EditorInvoker += new WinPaletter.UI.Simulation.WinIcon.EditorInvokerEventHandler(this.FakeIconX_EditorInvoker);
            // 
            // DesktopIcons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.FakeIcon1);
            this.Controls.Add(this.FakeIcon2);
            this.Controls.Add(this.FakeIcon3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DesktopIcons";
            this.Size = new System.Drawing.Size(399, 271);
            this.Load += new System.EventHandler(this.DesktopIcons_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal UI.Simulation.WinIcon FakeIcon1;
        internal UI.Simulation.WinIcon FakeIcon2;
        internal UI.Simulation.WinIcon FakeIcon3;
    }
}
