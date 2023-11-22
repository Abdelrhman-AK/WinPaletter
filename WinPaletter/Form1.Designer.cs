namespace WinPaletter
{
    partial class Form1
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
            this.button1 = new WinPaletter.UI.WP.Button();
            this.button17 = new WinPaletter.UI.WP.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.CustomColor = System.Drawing.Color.Empty;
            this.button1.DrawOnGlass = false;
            this.button1.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.Image = null;
            this.button1.Location = new System.Drawing.Point(229, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 52);
            this.button1.TabIndex = 22;
            this.button1.Text = "Open main form";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button17
            // 
            this.button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button17.CustomColor = System.Drawing.Color.Empty;
            this.button17.DrawOnGlass = false;
            this.button17.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button17.Image = global::WinPaletter.Properties.Resources.add_win8;
            this.button17.Location = new System.Drawing.Point(419, 184);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(184, 52);
            this.button17.TabIndex = 21;
            this.button17.Text = "Switch dark mode";
            this.button17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(615, 248);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button17);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.WP.Button button17;
        private UI.WP.Button button1;
    }
}