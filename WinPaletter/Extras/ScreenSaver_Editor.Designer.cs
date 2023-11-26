using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ScreenSaver_Editor : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenSaver_Editor));
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.OpenThemeDialog = new System.Windows.Forms.OpenFileDialog();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Trackbar5 = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox17 = new System.Windows.Forms.PictureBox();
            this.CheckBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.Button14 = new WinPaletter.UI.WP.Button();
            this.Button13 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.pnl_preview = new System.Windows.Forms.Panel();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.Button259 = new WinPaletter.UI.WP.Button();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.ScrSvrEnabled = new WinPaletter.UI.WP.Toggle();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.previewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // OpenFileDialog2
            // 
            this.OpenFileDialog2.DefaultExt = "wpt";
            this.OpenFileDialog2.Filter = "Screen Saver (*.scr)|*.scr";
            // 
            // OpenThemeDialog
            // 
            this.OpenThemeDialog.Filter = "Windows Theme (*.theme)|*.theme|All Files (*.*)|*.*";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.PictureBox4);
            this.GroupBox1.Controls.Add(this.Trackbar5);
            this.GroupBox1.Controls.Add(this.PictureBox17);
            this.GroupBox1.Controls.Add(this.CheckBox1);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.PictureBox1);
            this.GroupBox1.Controls.Add(this.Button4);
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.TextBox1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 408);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(750, 90);
            this.GroupBox1.TabIndex = 226;
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(3, 3);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 217;
            this.PictureBox4.TabStop = false;
            // 
            // Trackbar5
            // 
            this.Trackbar5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trackbar5.BackColor = System.Drawing.Color.Transparent;
            this.Trackbar5.LargeChange = 10;
            this.Trackbar5.Location = new System.Drawing.Point(129, 36);
            this.Trackbar5.Maximum = 3600;
            this.Trackbar5.Minimum = 60;
            this.Trackbar5.Name = "Trackbar5";
            this.Trackbar5.Size = new System.Drawing.Size(535, 19);
            this.Trackbar5.SmallChange = 1;
            this.Trackbar5.TabIndex = 214;
            this.Trackbar5.Value = 60;
            this.Trackbar5.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.Trackbar5_Scroll);
            // 
            // PictureBox17
            // 
            this.PictureBox17.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox17.Image")));
            this.PictureBox17.Location = new System.Drawing.Point(3, 33);
            this.PictureBox17.Name = "PictureBox17";
            this.PictureBox17.Size = new System.Drawing.Size(24, 24);
            this.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox17.TabIndex = 212;
            this.PictureBox17.TabStop = false;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CheckBox1.Checked = false;
            this.CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox1.ForeColor = System.Drawing.Color.White;
            this.CheckBox1.Location = new System.Drawing.Point(33, 63);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(713, 24);
            this.CheckBox1.TabIndex = 223;
            this.CheckBox1.Text = "On resume, password protect";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(33, 33);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(90, 24);
            this.Label4.TabIndex = 213;
            this.Label4.Text = "Timeout (sec):";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 63);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 222;
            this.PictureBox1.TabStop = false;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.Empty;
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.Location = new System.Drawing.Point(670, 33);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(76, 24);
            this.Button4.TabIndex = 215;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.CustomColor = System.Drawing.Color.Empty;
            this.Button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(712, 3);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(34, 24);
            this.Button1.TabIndex = 219;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(33, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(90, 24);
            this.Label3.TabIndex = 216;
            this.Label3.Text = "File:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(129, 3);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(576, 24);
            this.TextBox1.TabIndex = 218;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.previewContainer.Controls.Add(this.Button14);
            this.previewContainer.Controls.Add(this.Button13);
            this.previewContainer.Controls.Add(this.Button6);
            this.previewContainer.Controls.Add(this.Button5);
            this.previewContainer.Controls.Add(this.pnl_preview);
            this.previewContainer.Controls.Add(this.PictureBox41);
            this.previewContainer.Controls.Add(this.Label19);
            this.previewContainer.Location = new System.Drawing.Point(12, 57);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(750, 345);
            this.previewContainer.TabIndex = 211;
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.CustomColor = System.Drawing.Color.Empty;
            this.Button14.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = null;
            this.Button14.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button14.Location = new System.Drawing.Point(425, 8);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(150, 24);
            this.Button14.TabIndex = 225;
            this.Button14.Text = "Configure its settings";
            this.Button14.UseVisualStyleBackColor = false;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.CustomColor = System.Drawing.Color.Empty;
            this.Button13.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button13.ForeColor = System.Drawing.Color.White;
            this.Button13.Image = ((System.Drawing.Image)(resources.GetObject("Button13.Image")));
            this.Button13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button13.Location = new System.Drawing.Point(651, 8);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(90, 24);
            this.Button13.TabIndex = 224;
            this.Button13.Text = "Fullscreen";
            this.Button13.UseVisualStyleBackColor = false;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.CustomColor = System.Drawing.Color.Empty;
            this.Button6.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.Location = new System.Drawing.Point(581, 8);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(29, 24);
            this.Button6.TabIndex = 223;
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.CustomColor = System.Drawing.Color.Empty;
            this.Button5.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = ((System.Drawing.Image)(resources.GetObject("Button5.Image")));
            this.Button5.Location = new System.Drawing.Point(616, 8);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(29, 24);
            this.Button5.TabIndex = 222;
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // pnl_preview
            // 
            this.pnl_preview.BackColor = System.Drawing.Color.Black;
            this.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_preview.Location = new System.Drawing.Point(111, 42);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 2;
            // 
            // PictureBox41
            // 
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(4, 4);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(35, 32);
            this.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox41.TabIndex = 4;
            this.PictureBox41.TabStop = false;
            // 
            // Label19
            // 
            this.Label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(45, 5);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(374, 31);
            this.Label19.TabIndex = 3;
            this.Label19.Text = "Preview";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button10.CustomColor = System.Drawing.Color.Empty;
            this.Button10.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = ((System.Drawing.Image)(resources.GetObject("Button10.Image")));
            this.Button10.Location = new System.Drawing.Point(461, 510);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(115, 34);
            this.Button10.TabIndex = 210;
            this.Button10.Text = "Quick apply";
            this.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.CustomColor = System.Drawing.Color.Empty;
            this.Button7.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.Location = new System.Drawing.Point(375, 510);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(80, 34);
            this.Button7.TabIndex = 209;
            this.Button7.Text = "Cancel";
            this.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button8.CustomColor = System.Drawing.Color.Empty;
            this.Button8.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.Location = new System.Drawing.Point(582, 510);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(180, 34);
            this.Button8.TabIndex = 208;
            this.Button8.Text = "Load into current theme";
            this.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.Button259);
            this.GroupBox12.Controls.Add(this.Button9);
            this.GroupBox12.Controls.Add(this.Label12);
            this.GroupBox12.Controls.Add(this.Button11);
            this.GroupBox12.Controls.Add(this.Button12);
            this.GroupBox12.Controls.Add(this.ScrSvrEnabled);
            this.GroupBox12.Controls.Add(this.checker_img);
            this.GroupBox12.Location = new System.Drawing.Point(12, 12);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(750, 39);
            this.GroupBox12.TabIndex = 201;
            // 
            // Button259
            // 
            this.Button259.CustomColor = System.Drawing.Color.Empty;
            this.Button259.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button259.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button259.ForeColor = System.Drawing.Color.White;
            this.Button259.Image = ((System.Drawing.Image)(resources.GetObject("Button259.Image")));
            this.Button259.Location = new System.Drawing.Point(223, 5);
            this.Button259.Name = "Button259";
            this.Button259.Size = new System.Drawing.Size(144, 29);
            this.Button259.TabIndex = 114;
            this.Button259.Text = "Classic .theme file";
            this.Button259.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button259.UseVisualStyleBackColor = false;
            this.Button259.Click += new System.EventHandler(this.Button259_Click);
            // 
            // Button9
            // 
            this.Button9.CustomColor = System.Drawing.Color.Empty;
            this.Button9.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.Location = new System.Drawing.Point(370, 5);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(126, 29);
            this.Button9.TabIndex = 112;
            this.Button9.Text = "Current applied";
            this.Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(4, 4);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(75, 31);
            this.Label12.TabIndex = 111;
            this.Label12.Text = "Open from:";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            this.Button11.CustomColor = System.Drawing.Color.Empty;
            this.Button11.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = ((System.Drawing.Image)(resources.GetObject("Button11.Image")));
            this.Button11.Location = new System.Drawing.Point(85, 5);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(135, 29);
            this.Button11.TabIndex = 110;
            this.Button11.Text = "WinPaletter theme";
            this.Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button12
            // 
            this.Button12.CustomColor = System.Drawing.Color.Empty;
            this.Button12.Flag = WinPaletter.UI.WP.Button.Flags.None;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = null;
            this.Button12.Location = new System.Drawing.Point(499, 5);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(135, 29);
            this.Button12.TabIndex = 108;
            this.Button12.Text = "Default Windows";
            this.Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // ScrSvrEnabled
            // 
            this.ScrSvrEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrSvrEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ScrSvrEnabled.Checked = false;
            this.ScrSvrEnabled.DarkLight_Toggler = false;
            this.ScrSvrEnabled.Location = new System.Drawing.Point(705, 9);
            this.ScrSvrEnabled.Name = "ScrSvrEnabled";
            this.ScrSvrEnabled.Size = new System.Drawing.Size(40, 20);
            this.ScrSvrEnabled.TabIndex = 85;
            this.ScrSvrEnabled.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.ScrSvrEnabled_CheckedChanged);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(664, 4);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // ScreenSaver_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(774, 556);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.previewContainer);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.GroupBox12);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScreenSaver_Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Saver";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenSaver_Editor_FormClosing);
            this.Load += new System.EventHandler(this.ScreenSaver_Editor_Load);
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.previewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle ScrSvrEnabled;
        internal PictureBox checker_img;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal UI.WP.GroupBox previewContainer;
        internal Panel pnl_preview;
        internal PictureBox PictureBox41;
        internal Label Label19;
        internal UI.WP.Button Button4;
        internal Label Label4;
        internal PictureBox PictureBox17;
        internal UI.WP.Trackbar Trackbar5;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox1;
        internal PictureBox PictureBox4;
        internal Label Label3;
        internal PictureBox PictureBox1;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.Button Button14;
        internal UI.WP.Button Button13;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button5;
        internal OpenFileDialog OpenFileDialog2;
        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.Button Button259;
        internal OpenFileDialog OpenThemeDialog;
    }
}
