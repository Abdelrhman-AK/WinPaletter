using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LogonUI7 : AspectsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonUI7));
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.trackBarX2 = new WinPaletter.UI.Controllers.TrackBarX();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.trackBarX1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.ComboBox1 = new WinPaletter.UI.WP.ComboBox();
            this.CheckBox7 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.CheckBox8 = new WinPaletter.UI.WP.CheckBox();
            this.CheckBox6 = new WinPaletter.UI.WP.CheckBox();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.GroupBox8 = new WinPaletter.UI.WP.GroupBox();
            this.pnl_preview = new System.Windows.Forms.Panel();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioButton();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.RadioButton3 = new WinPaletter.UI.WP.RadioButton();
            this.RadioButton4 = new WinPaletter.UI.WP.RadioButton();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.color_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            this.GroupBox8.SuspendLayout();
            this.pnl_preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // titlebarExtender1
            // 
            this.titlebarExtender1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.titlebarExtender1.Size = new System.Drawing.Size(964, 52);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox3.Controls.Add(this.trackBarX2);
            this.GroupBox3.Controls.Add(this.PictureBox3);
            this.GroupBox3.Controls.Add(this.Label2);
            this.GroupBox3.Controls.Add(this.trackBarX1);
            this.GroupBox3.Controls.Add(this.PictureBox8);
            this.GroupBox3.Controls.Add(this.ComboBox1);
            this.GroupBox3.Controls.Add(this.CheckBox7);
            this.GroupBox3.Controls.Add(this.PictureBox10);
            this.GroupBox3.Controls.Add(this.CheckBox8);
            this.GroupBox3.Controls.Add(this.CheckBox6);
            this.GroupBox3.Controls.Add(this.PictureBox9);
            this.GroupBox3.Location = new System.Drawing.Point(9, 259);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(399, 233);
            this.GroupBox3.TabIndex = 18;
            // 
            // trackBarX2
            // 
            this.trackBarX2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX2.AnimateChanges = false;
            this.trackBarX2.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX2.DefaultValue = 0;
            this.trackBarX2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX2.Location = new System.Drawing.Point(39, 197);
            this.trackBarX2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX2.Maximum = 100;
            this.trackBarX2.Minimum = 0;
            this.trackBarX2.Name = "trackBarX2";
            this.trackBarX2.Size = new System.Drawing.Size(352, 25);
            this.trackBarX2.TabIndex = 120;
            this.trackBarX2.Value = 0;
            this.trackBarX2.ValueChanged += new System.EventHandler(this.trackBarX2_ValueChanged);
            // 
            // PictureBox3
            // 
            this.PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(3, 3);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(30, 30);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 17;
            this.PictureBox3.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(39, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(357, 30);
            this.Label2.TabIndex = 81;
            this.Label2.Text = "Effects";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBarX1
            // 
            this.trackBarX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX1.AnimateChanges = false;
            this.trackBarX1.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX1.DefaultValue = 0;
            this.trackBarX1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX1.Location = new System.Drawing.Point(39, 101);
            this.trackBarX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX1.Maximum = 40;
            this.trackBarX1.Minimum = 0;
            this.trackBarX1.Name = "trackBarX1";
            this.trackBarX1.Size = new System.Drawing.Size(350, 25);
            this.trackBarX1.TabIndex = 119;
            this.trackBarX1.Value = 0;
            this.trackBarX1.ValueChanged += new System.EventHandler(this.trackBarX1_ValueChanged);
            // 
            // PictureBox8
            // 
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(8, 39);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(25, 25);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 90;
            this.PictureBox8.TabStop = false;
            // 
            // ComboBox1
            // 
            this.ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ComboBox1.ForeColor = System.Drawing.Color.White;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.ItemHeight = 20;
            this.ComboBox1.Items.AddRange(new object[] {
            "Acrylic (Looks Like Windows 10/11)",
            "Aero"});
            this.ComboBox1.Location = new System.Drawing.Point(39, 165);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(352, 26);
            this.ComboBox1.TabIndex = 85;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // CheckBox7
            // 
            this.CheckBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox7.Checked = false;
            this.CheckBox7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox7.ForeColor = System.Drawing.Color.White;
            this.CheckBox7.Location = new System.Drawing.Point(39, 70);
            this.CheckBox7.Name = "CheckBox7";
            this.CheckBox7.Size = new System.Drawing.Size(352, 25);
            this.CheckBox7.TabIndex = 83;
            this.CheckBox7.Text = "Blurred";
            this.CheckBox7.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox7_CheckedChanged);
            // 
            // PictureBox10
            // 
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(8, 134);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(25, 25);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 92;
            this.PictureBox10.TabStop = false;
            // 
            // CheckBox8
            // 
            this.CheckBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox8.Checked = false;
            this.CheckBox8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox8.ForeColor = System.Drawing.Color.White;
            this.CheckBox8.Location = new System.Drawing.Point(39, 39);
            this.CheckBox8.Name = "CheckBox8";
            this.CheckBox8.Size = new System.Drawing.Size(352, 25);
            this.CheckBox8.TabIndex = 82;
            this.CheckBox8.Text = "Gray-scale";
            this.CheckBox8.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox8_CheckedChanged);
            // 
            // CheckBox6
            // 
            this.CheckBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox6.Checked = false;
            this.CheckBox6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox6.ForeColor = System.Drawing.Color.White;
            this.CheckBox6.Location = new System.Drawing.Point(39, 134);
            this.CheckBox6.Name = "CheckBox6";
            this.CheckBox6.Size = new System.Drawing.Size(350, 25);
            this.CheckBox6.TabIndex = 84;
            this.CheckBox6.Text = "Noise";
            this.CheckBox6.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.CheckBox6_CheckedChanged);
            // 
            // PictureBox9
            // 
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(8, 70);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(25, 25);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 91;
            this.PictureBox9.TabStop = false;
            // 
            // GroupBox8
            // 
            this.GroupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox8.Controls.Add(this.pnl_preview);
            this.GroupBox8.Location = new System.Drawing.Point(415, 58);
            this.GroupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox8.Size = new System.Drawing.Size(536, 305);
            this.GroupBox8.TabIndex = 15;
            // 
            // pnl_preview
            // 
            this.pnl_preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_preview.Controls.Add(this.PictureBox11);
            this.pnl_preview.Location = new System.Drawing.Point(4, 4);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 2;
            // 
            // PictureBox11
            // 
            this.PictureBox11.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(0, 0);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(528, 297);
            this.PictureBox11.TabIndex = 0;
            this.PictureBox11.TabStop = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox2.Controls.Add(this.Button3);
            this.GroupBox2.Controls.Add(this.PictureBox2);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Controls.Add(this.PictureBox7);
            this.GroupBox2.Controls.Add(this.PictureBox4);
            this.GroupBox2.Controls.Add(this.Button7);
            this.GroupBox2.Controls.Add(this.PictureBox6);
            this.GroupBox2.Controls.Add(this.RadioButton2);
            this.GroupBox2.Controls.Add(this.RadioButton1);
            this.GroupBox2.Controls.Add(this.PictureBox5);
            this.GroupBox2.Controls.Add(this.RadioButton3);
            this.GroupBox2.Controls.Add(this.RadioButton4);
            this.GroupBox2.Controls.Add(this.TextBox1);
            this.GroupBox2.Controls.Add(this.color_pick);
            this.GroupBox2.Location = new System.Drawing.Point(9, 58);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(399, 195);
            this.GroupBox2.TabIndex = 17;
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.ImageGlyph = null;
            this.Button3.Location = new System.Drawing.Point(293, 39);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(98, 25);
            this.Button3.TabIndex = 93;
            this.Button3.Text = "Choose";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Visible = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(3, 3);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(30, 30);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 17;
            this.PictureBox2.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(39, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(357, 30);
            this.Label1.TabIndex = 81;
            this.Label1.Text = "Source";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            this.PictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(8, 101);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(25, 25);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 92;
            this.PictureBox7.TabStop = false;
            // 
            // PictureBox4
            // 
            this.PictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox4.Location = new System.Drawing.Point(8, 39);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(25, 25);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 89;
            this.PictureBox4.TabStop = false;
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.ImageGlyphEnabled = true;
            this.Button7.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button7.Location = new System.Drawing.Point(356, 163);
            this.Button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(35, 24);
            this.Button7.TabIndex = 87;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // PictureBox6
            // 
            this.PictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(8, 132);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(25, 25);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 91;
            this.PictureBox6.TabStop = false;
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Location = new System.Drawing.Point(39, 70);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(352, 25);
            this.RadioButton2.TabIndex = 83;
            this.RadioButton2.Text = "Current wallpaper";
            this.RadioButton2.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton2_CheckedChanged);
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton1.Checked = true;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Location = new System.Drawing.Point(39, 39);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(248, 25);
            this.RadioButton1.TabIndex = 82;
            this.RadioButton1.Text = "System default";
            this.RadioButton1.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton1_CheckedChanged);
            // 
            // PictureBox5
            // 
            this.PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(8, 70);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(25, 25);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 90;
            this.PictureBox5.TabStop = false;
            // 
            // RadioButton3
            // 
            this.RadioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton3.Checked = false;
            this.RadioButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton3.ForeColor = System.Drawing.Color.White;
            this.RadioButton3.Location = new System.Drawing.Point(39, 101);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(248, 25);
            this.RadioButton3.TabIndex = 85;
            this.RadioButton3.Text = "Solid color";
            this.RadioButton3.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton4
            // 
            this.RadioButton4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton4.Checked = false;
            this.RadioButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton4.ForeColor = System.Drawing.Color.White;
            this.RadioButton4.Location = new System.Drawing.Point(39, 132);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(352, 25);
            this.RadioButton4.TabIndex = 84;
            this.RadioButton4.Text = "Custom image";
            this.RadioButton4.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.RadioButton4_CheckedChanged);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(64, 163);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(285, 24);
            this.TextBox1.TabIndex = 86;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // color_pick
            // 
            this.color_pick.AllowDrop = true;
            this.color_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.color_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.color_pick.DefaultBackColor = System.Drawing.Color.Black;
            this.color_pick.DontShowInfo = false;
            this.color_pick.Location = new System.Drawing.Point(294, 101);
            this.color_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.color_pick.Name = "color_pick";
            this.color_pick.Size = new System.Drawing.Size(97, 25);
            this.color_pick.TabIndex = 88;
            this.color_pick.Click += new System.EventHandler(this.Color_pick_Click);
            this.color_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.Color_pick_Click);
            // 
            // LogonUI7
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(964, 561);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox8);
            this.Controls.Add(this.GroupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.IsShown = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonUI7";
            this.Text = "LogonUI";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.Load += new System.EventHandler(this.LogonUI7_Load);
            this.Controls.SetChildIndex(this.GroupBox2, 0);
            this.Controls.SetChildIndex(this.GroupBox8, 0);
            this.Controls.SetChildIndex(this.GroupBox3, 0);
            this.Controls.SetChildIndex(this.titlebarExtender1, 0);
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            this.GroupBox8.ResumeLayout(false);
            this.pnl_preview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox8;
        internal Panel pnl_preview;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.RadioButton RadioButton3;
        internal UI.WP.RadioButton RadioButton4;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton1;
        internal PictureBox PictureBox2;
        internal Label Label1;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.Button Button7;
        internal UI.Controllers.ColorItem color_pick;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.ComboBox ComboBox1;
        internal UI.WP.CheckBox CheckBox6;
        internal UI.WP.CheckBox CheckBox7;
        internal UI.WP.CheckBox CheckBox8;
        internal PictureBox PictureBox3;
        internal Label Label2;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox6;
        internal PictureBox PictureBox5;
        internal PictureBox PictureBox4;
        internal PictureBox PictureBox10;
        internal PictureBox PictureBox9;
        internal PictureBox PictureBox8;
        internal PictureBox PictureBox11;
        internal UI.WP.Button Button3;
        private UI.Controllers.TrackBarX trackBarX1;
        private UI.Controllers.TrackBarX trackBarX2;
    }
}
