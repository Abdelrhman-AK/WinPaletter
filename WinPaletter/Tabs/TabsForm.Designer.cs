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
            this.titlebarExtender1 = new WinPaletter.Tabs.TitlebarExtender();
            this.tabsContainer1 = new WinPaletter.Tabs.TabsContainer();
            this.titlebarExtender1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.tabsContainer1);
            this.titlebarExtender1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebarExtender1.DropDWMEffect = true;
            this.titlebarExtender1.Location = new System.Drawing.Point(0, 0);
            this.titlebarExtender1.Name = "titlebarExtender1";
            this.titlebarExtender1.Size = new System.Drawing.Size(933, 34);
            this.titlebarExtender1.TabIndex = 58;
            this.titlebarExtender1.TabLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.titlebarExtender1.Text = "titlebarExtender1";
            this.titlebarExtender1.DoubleClick += new System.EventHandler(this.tabsContainer1_DoubleClick);
            // 
            // tabsContainer1
            // 
            this.tabsContainer1.AllowDrop = true;
            this.tabsContainer1.BackColor = System.Drawing.Color.Transparent;
            this.tabsContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsContainer1.Location = new System.Drawing.Point(0, 0);
            this.tabsContainer1.Name = "tabsContainer1";
            this.tabsContainer1.SelectedIndex = 0;
            this.tabsContainer1.SelectedTab = null;
            this.tabsContainer1.Size = new System.Drawing.Size(933, 34);
            this.tabsContainer1.TabControl = null;
            this.tabsContainer1.TabIndex = 120;
            this.tabsContainer1.DoubleClick += new System.EventHandler(this.tabsContainer1_DoubleClick);
            // 
            // TabsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.titlebarExtender1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TabsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TabsForm";
            this.Load += new System.EventHandler(this.TabsForm_Load);
            this.titlebarExtender1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Tabs.TitlebarExtender titlebarExtender1;
        public Tabs.TabsContainer tabsContainer1;
    }
}