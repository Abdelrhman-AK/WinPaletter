namespace WinPaletter.Tabs
{
    partial class TabsForm
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
            this.tabsContainer1 = new WinPaletter.Tabs.TabsContainer();
            this.SuspendLayout();
            // 
            // tabsContainer1
            // 
            this.tabsContainer1.AllowDrop = true;
            this.tabsContainer1.BackColor = System.Drawing.Color.Transparent;
            this.tabsContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabsContainer1.Flag = WinPaletter.Tabs.TitlebarExtender.Flags.System;
            this.tabsContainer1.Location = new System.Drawing.Point(0, 0);
            this.tabsContainer1.Name = "tabsContainer1";
            this.tabsContainer1.SelectedIndex = 0;
            this.tabsContainer1.SelectedTab = null;
            this.tabsContainer1.Size = new System.Drawing.Size(933, 34);
            this.tabsContainer1.TabControl = null;
            this.tabsContainer1.TabIndex = 120;
            this.tabsContainer1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // TabsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.tabsContainer1);
            this.Name = "TabsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TabsForm";
            this.ResumeLayout(false);

        }

        #endregion
        public Tabs.TabsContainer tabsContainer1;
    }
}