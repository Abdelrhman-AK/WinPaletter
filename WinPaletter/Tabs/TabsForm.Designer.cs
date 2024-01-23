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
            this.tabsContainer1 = new Tabs.TabsContainer();
            this.BetaBadge = new WinPaletter.UI.WP.AlertBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.titlebarExtender1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.Black;
            this.titlebarExtender1.Controls.Add(this.tabsContainer1);
            this.titlebarExtender1.Controls.Add(this.panel1);
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
            this.tabsContainer1.BackColor = System.Drawing.Color.Black;
            this.tabsContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsContainer1.Location = new System.Drawing.Point(0, 0);
            this.tabsContainer1.Name = "tabsContainer1";
            this.tabsContainer1.SelectedIndex = 0;
            this.tabsContainer1.SelectedTab = null;
            this.tabsContainer1.Size = new System.Drawing.Size(878, 34);
            this.tabsContainer1.TabControl = null;
            this.tabsContainer1.TabIndex = 120;
            this.tabsContainer1.DoubleClick += new System.EventHandler(this.tabsContainer1_DoubleClick);
            // 
            // BetaBadge
            // 
            this.BetaBadge.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.BetaBadge.BackColor = System.Drawing.Color.Transparent;
            this.BetaBadge.CenterText = true;
            this.BetaBadge.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.BetaBadge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BetaBadge.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BetaBadge.Image = null;
            this.BetaBadge.Location = new System.Drawing.Point(5, 5);
            this.BetaBadge.Name = "BetaBadge";
            this.BetaBadge.Size = new System.Drawing.Size(45, 24);
            this.BetaBadge.TabIndex = 121;
            this.BetaBadge.TabStop = false;
            this.BetaBadge.Text = "BETA";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BetaBadge);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(878, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(55, 34);
            this.panel1.TabIndex = 122;
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
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Tabs.TitlebarExtender titlebarExtender1;
        public Tabs.TabsContainer tabsContainer1;
        internal UI.WP.AlertBox BetaBadge;
        private System.Windows.Forms.Panel panel1;
    }
}