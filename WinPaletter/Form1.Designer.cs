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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new WinPaletter.UI.WP.Button();
            this.button17 = new WinPaletter.UI.WP.Button();
            this.toggle1 = new WinPaletter.UI.WP.Toggle();
            this.toggle2 = new WinPaletter.UI.WP.Toggle();
            this.toggle3 = new WinPaletter.UI.WP.Toggle();
            this.SuspendLayout();
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "Updates");
            this.ImageList1.Images.SetKeyName(1, "Language");
            this.ImageList1.Images.SetKeyName(2, "Appearance");
            this.ImageList1.Images.SetKeyName(3, "ThemeFileManagement");
            this.ImageList1.Images.SetKeyName(4, "ThemeApplyingBehavior");
            this.ImageList1.Images.SetKeyName(5, "Store");
            this.ImageList1.Images.SetKeyName(6, "Log");
            this.ImageList1.Images.SetKeyName(7, "Terminals");
            this.ImageList1.Images.SetKeyName(8, "ExplorerPatcher");
            this.ImageList1.Images.SetKeyName(9, "ColorItemInfo");
            this.ImageList1.Images.SetKeyName(10, "Users");
            this.ImageList1.Images.SetKeyName(11, "Miscellaneous");
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.CustomColor = System.Drawing.Color.Empty;
            this.button1.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.Image = null;
            this.button1.Location = new System.Drawing.Point(347, 322);
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
            this.button17.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button17.Image = global::WinPaletter.Properties.Resources.add_win8;
            this.button17.Location = new System.Drawing.Point(537, 322);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(184, 52);
            this.button17.TabIndex = 21;
            this.button17.Text = "Switch dark mode";
            this.button17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // toggle1
            // 
            this.toggle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.toggle1.Checked = false;
            this.toggle1.DarkLight_Toggler = false;
            this.toggle1.Location = new System.Drawing.Point(351, 102);
            this.toggle1.Name = "toggle1";
            this.toggle1.Size = new System.Drawing.Size(40, 20);
            this.toggle1.TabIndex = 23;
            this.toggle1.Text = "toggle1";
            // 
            // toggle2
            // 
            this.toggle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.toggle2.Checked = true;
            this.toggle2.DarkLight_Toggler = false;
            this.toggle2.Location = new System.Drawing.Point(351, 128);
            this.toggle2.Name = "toggle2";
            this.toggle2.Size = new System.Drawing.Size(40, 20);
            this.toggle2.TabIndex = 24;
            this.toggle2.Text = "toggle2";
            // 
            // toggle3
            // 
            this.toggle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.toggle3.Checked = false;
            this.toggle3.DarkLight_Toggler = false;
            this.toggle3.Location = new System.Drawing.Point(351, 154);
            this.toggle3.Name = "toggle3";
            this.toggle3.Size = new System.Drawing.Size(40, 20);
            this.toggle3.TabIndex = 25;
            this.toggle3.Text = "toggle3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(733, 386);
            this.Controls.Add(this.toggle3);
            this.Controls.Add(this.toggle2);
            this.Controls.Add(this.toggle1);
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
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        internal System.Windows.Forms.ImageList ImageList1;
        private UI.WP.Toggle toggle1;
        private UI.WP.Toggle toggle2;
        private UI.WP.Toggle toggle3;
    }
}
