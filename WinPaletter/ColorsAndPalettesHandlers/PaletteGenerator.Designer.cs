using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class PaletteGenerator : BorderlessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteGenerator));
            this.groupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.toggle1 = new WinPaletter.UI.WP.Toggle();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.toggle_grayscale = new WinPaletter.UI.WP.Toggle();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.groupBox11 = new WinPaletter.UI.WP.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.toggle_monochrome = new WinPaletter.UI.WP.Toggle();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.groupBox15 = new WinPaletter.UI.WP.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.toggle_androidMaterialExpressive = new WinPaletter.UI.WP.Toggle();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.groupBox9 = new WinPaletter.UI.WP.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.toggle_androidMaterial = new WinPaletter.UI.WP.Toggle();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.groupBox16 = new WinPaletter.UI.WP.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.toggle_Mac = new WinPaletter.UI.WP.Toggle();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.groupBox18 = new WinPaletter.UI.WP.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.toggle_metro = new WinPaletter.UI.WP.Toggle();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.label29 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox8 = new WinPaletter.UI.WP.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.toggle_frutigerAero = new WinPaletter.UI.WP.Toggle();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.groupBox10 = new WinPaletter.UI.WP.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.toggle_256 = new WinPaletter.UI.WP.Toggle();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.groupBox17 = new WinPaletter.UI.WP.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.toggle_webSafe = new WinPaletter.UI.WP.Toggle();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.label27 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox7 = new WinPaletter.UI.WP.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.toggle_sepia = new WinPaletter.UI.WP.Toggle();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toggle_reverse = new WinPaletter.UI.WP.Toggle();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox6 = new WinPaletter.UI.WP.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.toggle_invert = new WinPaletter.UI.WP.Toggle();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.label30 = new System.Windows.Forms.Label();
            this.panel20 = new System.Windows.Forms.Panel();
            this.groupBox13 = new WinPaletter.UI.WP.GroupBox();
            this.trackBarX4 = new WinPaletter.UI.Controllers.TrackBarX();
            this.toggle_desaturate = new WinPaletter.UI.WP.Toggle();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox20 = new WinPaletter.UI.WP.GroupBox();
            this.trackBarX5 = new WinPaletter.UI.Controllers.TrackBarX();
            this.toggle_rotateHue = new WinPaletter.UI.WP.Toggle();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.label33 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox21 = new WinPaletter.UI.WP.GroupBox();
            this.trackBarX2 = new WinPaletter.UI.Controllers.TrackBarX();
            this.toggle_lighten = new WinPaletter.UI.WP.Toggle();
            this.pictureBox21 = new System.Windows.Forms.PictureBox();
            this.label34 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox22 = new WinPaletter.UI.WP.GroupBox();
            this.trackBarX1 = new WinPaletter.UI.Controllers.TrackBarX();
            this.toggle_darken = new WinPaletter.UI.WP.Toggle();
            this.pictureBox22 = new System.Windows.Forms.PictureBox();
            this.label35 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.groupBox23 = new WinPaletter.UI.WP.GroupBox();
            this.trackBarX7 = new WinPaletter.UI.Controllers.TrackBarX();
            this.toggle_brightness = new WinPaletter.UI.WP.Toggle();
            this.pictureBox23 = new System.Windows.Forms.PictureBox();
            this.label36 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox14 = new WinPaletter.UI.WP.GroupBox();
            this.radioImage2 = new WinPaletter.UI.WP.RadioImage();
            this.tablessControl1 = new WinPaletter.UI.WP.TablessControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox19 = new WinPaletter.UI.WP.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button5 = new WinPaletter.UI.WP.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.button7 = new WinPaletter.UI.WP.Button();
            this.button6 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.RadioButton1 = new WinPaletter.UI.WP.RadioImage();
            this.Label2 = new System.Windows.Forms.Label();
            this.RadioButton2 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ImgPaletteContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.checkBox1 = new WinPaletter.UI.WP.CheckBox();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.groupBox4.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            this.groupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).BeginInit();
            this.groupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).BeginInit();
            this.groupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.tablessControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.bottom_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox4.Controls.Add(this.toggle1);
            this.groupBox4.Controls.Add(this.panel14);
            this.groupBox4.Controls.Add(this.pictureBox3);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(12, 157);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(486, 213);
            this.groupBox4.TabIndex = 177;
            this.groupBox4.Text = "groupBox4";
            // 
            // toggle1
            // 
            this.toggle1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggle1.Checked = false;
            this.toggle1.DarkLight_Toggler = false;
            this.toggle1.Location = new System.Drawing.Point(435, 14);
            this.toggle1.Name = "toggle1";
            this.toggle1.Size = new System.Drawing.Size(40, 20);
            this.toggle1.TabIndex = 163;
            this.toggle1.Text = "toggle1";
            this.toggle1.CheckedChanged += new System.EventHandler(this.toggle1_CheckedChanged);
            // 
            // panel14
            // 
            this.panel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel14.Controls.Add(this.panel1);
            this.panel14.Enabled = false;
            this.panel14.Location = new System.Drawing.Point(11, 46);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(463, 156);
            this.panel14.TabIndex = 162;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.groupBox12);
            this.panel1.Controls.Add(this.panel12);
            this.panel1.Controls.Add(this.groupBox11);
            this.panel1.Controls.Add(this.panel16);
            this.panel1.Controls.Add(this.groupBox15);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.groupBox9);
            this.panel1.Controls.Add(this.panel17);
            this.panel1.Controls.Add(this.groupBox16);
            this.panel1.Controls.Add(this.panel19);
            this.panel1.Controls.Add(this.groupBox18);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.groupBox10);
            this.panel1.Controls.Add(this.panel18);
            this.panel1.Controls.Add(this.groupBox17);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.panel20);
            this.panel1.Controls.Add(this.groupBox13);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.groupBox20);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.groupBox21);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox22);
            this.panel1.Controls.Add(this.panel15);
            this.panel1.Controls.Add(this.groupBox23);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(463, 156);
            this.panel1.TabIndex = 161;
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.Color.Transparent;
            this.groupBox12.Controls.Add(this.label18);
            this.groupBox12.Controls.Add(this.toggle_grayscale);
            this.groupBox12.Controls.Add(this.pictureBox12);
            this.groupBox12.Controls.Add(this.label19);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox12.Location = new System.Drawing.Point(2, 1010);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(442, 58);
            this.groupBox12.TabIndex = 144;
            this.groupBox12.Text = "groupBox12";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(50, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(338, 24);
            this.label18.TabIndex = 124;
            this.label18.Text = "Converts colors into shades of gray";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_grayscale
            // 
            this.toggle_grayscale.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_grayscale.Checked = false;
            this.toggle_grayscale.DarkLight_Toggler = false;
            this.toggle_grayscale.Location = new System.Drawing.Point(394, 19);
            this.toggle_grayscale.Name = "toggle_grayscale";
            this.toggle_grayscale.Size = new System.Drawing.Size(40, 20);
            this.toggle_grayscale.TabIndex = 123;
            this.toggle_grayscale.Text = "toggle10";
            this.toggle_grayscale.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox12
            // 
            this.pictureBox12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(4, 4);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(40, 50);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox12.TabIndex = 122;
            this.pictureBox12.TabStop = false;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(50, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(338, 24);
            this.label19.TabIndex = 120;
            this.label19.Text = "Grayscale";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(2, 1005);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(442, 5);
            this.panel12.TabIndex = 143;
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.Transparent;
            this.groupBox11.Controls.Add(this.label16);
            this.groupBox11.Controls.Add(this.toggle_monochrome);
            this.groupBox11.Controls.Add(this.pictureBox11);
            this.groupBox11.Controls.Add(this.label17);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox11.Location = new System.Drawing.Point(2, 947);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(442, 58);
            this.groupBox11.TabIndex = 142;
            this.groupBox11.Text = "groupBox11";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(50, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(338, 24);
            this.label16.TabIndex = 124;
            this.label16.Text = "Converts all colors to shades of one tone";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_monochrome
            // 
            this.toggle_monochrome.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_monochrome.Checked = false;
            this.toggle_monochrome.DarkLight_Toggler = false;
            this.toggle_monochrome.Location = new System.Drawing.Point(394, 19);
            this.toggle_monochrome.Name = "toggle_monochrome";
            this.toggle_monochrome.Size = new System.Drawing.Size(40, 20);
            this.toggle_monochrome.TabIndex = 123;
            this.toggle_monochrome.Text = "toggle9";
            this.toggle_monochrome.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox11
            // 
            this.pictureBox11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(4, 4);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(40, 50);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox11.TabIndex = 122;
            this.pictureBox11.TabStop = false;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(50, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(338, 24);
            this.label17.TabIndex = 120;
            this.label17.Text = "Monochrome";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Transparent;
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Location = new System.Drawing.Point(2, 942);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(442, 5);
            this.panel16.TabIndex = 153;
            // 
            // groupBox15
            // 
            this.groupBox15.BackColor = System.Drawing.Color.Transparent;
            this.groupBox15.Controls.Add(this.label22);
            this.groupBox15.Controls.Add(this.toggle_androidMaterialExpressive);
            this.groupBox15.Controls.Add(this.pictureBox15);
            this.groupBox15.Controls.Add(this.label23);
            this.groupBox15.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox15.Location = new System.Drawing.Point(2, 884);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(442, 58);
            this.groupBox15.TabIndex = 152;
            this.groupBox15.Text = "groupBox15";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(50, 30);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(338, 24);
            this.label22.TabIndex = 124;
            this.label22.Text = "Nearest Android Material Expressive color";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_androidMaterialExpressive
            // 
            this.toggle_androidMaterialExpressive.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_androidMaterialExpressive.Checked = false;
            this.toggle_androidMaterialExpressive.DarkLight_Toggler = false;
            this.toggle_androidMaterialExpressive.Location = new System.Drawing.Point(394, 19);
            this.toggle_androidMaterialExpressive.Name = "toggle_androidMaterialExpressive";
            this.toggle_androidMaterialExpressive.Size = new System.Drawing.Size(40, 20);
            this.toggle_androidMaterialExpressive.TabIndex = 123;
            this.toggle_androidMaterialExpressive.Text = "toggle8";
            this.toggle_androidMaterialExpressive.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox15
            // 
            this.pictureBox15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(4, 4);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(40, 50);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox15.TabIndex = 122;
            this.pictureBox15.TabStop = false;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(50, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(338, 24);
            this.label23.TabIndex = 120;
            this.label23.Text = "Android Material Expressive 3";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(2, 879);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(442, 5);
            this.panel10.TabIndex = 151;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.Transparent;
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.toggle_androidMaterial);
            this.groupBox9.Controls.Add(this.pictureBox9);
            this.groupBox9.Controls.Add(this.label13);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox9.Location = new System.Drawing.Point(2, 821);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(442, 58);
            this.groupBox9.TabIndex = 150;
            this.groupBox9.Text = "groupBox9";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(50, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(338, 24);
            this.label12.TabIndex = 124;
            this.label12.Text = "Nearest Android Material (2015) color";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_androidMaterial
            // 
            this.toggle_androidMaterial.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_androidMaterial.Checked = false;
            this.toggle_androidMaterial.DarkLight_Toggler = false;
            this.toggle_androidMaterial.Location = new System.Drawing.Point(394, 19);
            this.toggle_androidMaterial.Name = "toggle_androidMaterial";
            this.toggle_androidMaterial.Size = new System.Drawing.Size(40, 20);
            this.toggle_androidMaterial.TabIndex = 123;
            this.toggle_androidMaterial.Text = "toggle8";
            this.toggle_androidMaterial.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox9
            // 
            this.pictureBox9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(4, 4);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(40, 50);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox9.TabIndex = 122;
            this.pictureBox9.TabStop = false;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(50, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(338, 24);
            this.label13.TabIndex = 120;
            this.label13.Text = "Android Material";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.Transparent;
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(2, 816);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(442, 5);
            this.panel17.TabIndex = 155;
            // 
            // groupBox16
            // 
            this.groupBox16.BackColor = System.Drawing.Color.Transparent;
            this.groupBox16.Controls.Add(this.label24);
            this.groupBox16.Controls.Add(this.toggle_Mac);
            this.groupBox16.Controls.Add(this.pictureBox16);
            this.groupBox16.Controls.Add(this.label25);
            this.groupBox16.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox16.Location = new System.Drawing.Point(2, 758);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(442, 58);
            this.groupBox16.TabIndex = 154;
            this.groupBox16.Text = "groupBox16";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(50, 30);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(338, 24);
            this.label24.TabIndex = 124;
            this.label24.Text = "Nearest macOS semantic color";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_Mac
            // 
            this.toggle_Mac.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_Mac.Checked = false;
            this.toggle_Mac.DarkLight_Toggler = false;
            this.toggle_Mac.Location = new System.Drawing.Point(394, 19);
            this.toggle_Mac.Name = "toggle_Mac";
            this.toggle_Mac.Size = new System.Drawing.Size(40, 20);
            this.toggle_Mac.TabIndex = 123;
            this.toggle_Mac.Text = "toggle8";
            this.toggle_Mac.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox16
            // 
            this.pictureBox16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox16.Image")));
            this.pictureBox16.Location = new System.Drawing.Point(4, 4);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(40, 50);
            this.pictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox16.TabIndex = 122;
            this.pictureBox16.TabStop = false;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(50, 3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(338, 24);
            this.label25.TabIndex = 120;
            this.label25.Text = "macOS Semantic";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel19.Location = new System.Drawing.Point(2, 753);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(442, 5);
            this.panel19.TabIndex = 161;
            // 
            // groupBox18
            // 
            this.groupBox18.BackColor = System.Drawing.Color.Transparent;
            this.groupBox18.Controls.Add(this.label28);
            this.groupBox18.Controls.Add(this.toggle_metro);
            this.groupBox18.Controls.Add(this.pictureBox18);
            this.groupBox18.Controls.Add(this.label29);
            this.groupBox18.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox18.Location = new System.Drawing.Point(2, 695);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(442, 58);
            this.groupBox18.TabIndex = 160;
            this.groupBox18.Text = "groupBox18";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(50, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(338, 24);
            this.label28.TabIndex = 124;
            this.label28.Text = "Flat, bold, and solid color blocks";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_metro
            // 
            this.toggle_metro.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_metro.Checked = false;
            this.toggle_metro.DarkLight_Toggler = false;
            this.toggle_metro.Location = new System.Drawing.Point(394, 19);
            this.toggle_metro.Name = "toggle_metro";
            this.toggle_metro.Size = new System.Drawing.Size(40, 20);
            this.toggle_metro.TabIndex = 123;
            this.toggle_metro.Text = "toggle8";
            this.toggle_metro.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox18
            // 
            this.pictureBox18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox18.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox18.Image")));
            this.pictureBox18.Location = new System.Drawing.Point(4, 4);
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.Size = new System.Drawing.Size(40, 50);
            this.pictureBox18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox18.TabIndex = 122;
            this.pictureBox18.TabStop = false;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(50, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(338, 24);
            this.label29.TabIndex = 120;
            this.label29.Text = "Metro";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(2, 690);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(442, 5);
            this.panel9.TabIndex = 157;
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.Transparent;
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.toggle_frutigerAero);
            this.groupBox8.Controls.Add(this.pictureBox8);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(2, 632);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(442, 58);
            this.groupBox8.TabIndex = 156;
            this.groupBox8.Text = "groupBox8";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(50, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(338, 24);
            this.label10.TabIndex = 124;
            this.label10.Text = "Glossy gradients with vibrant blues and greens";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_frutigerAero
            // 
            this.toggle_frutigerAero.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_frutigerAero.Checked = false;
            this.toggle_frutigerAero.DarkLight_Toggler = false;
            this.toggle_frutigerAero.Location = new System.Drawing.Point(394, 19);
            this.toggle_frutigerAero.Name = "toggle_frutigerAero";
            this.toggle_frutigerAero.Size = new System.Drawing.Size(40, 20);
            this.toggle_frutigerAero.TabIndex = 123;
            this.toggle_frutigerAero.Text = "toggle8";
            this.toggle_frutigerAero.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox8
            // 
            this.pictureBox8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(4, 4);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(40, 50);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox8.TabIndex = 122;
            this.pictureBox8.TabStop = false;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(50, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(338, 24);
            this.label11.TabIndex = 120;
            this.label11.Text = "Frutiger Aero";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(2, 627);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(442, 5);
            this.panel11.TabIndex = 141;
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.Color.Transparent;
            this.groupBox10.Controls.Add(this.label14);
            this.groupBox10.Controls.Add(this.toggle_256);
            this.groupBox10.Controls.Add(this.pictureBox10);
            this.groupBox10.Controls.Add(this.label15);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox10.Location = new System.Drawing.Point(2, 569);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(442, 58);
            this.groupBox10.TabIndex = 140;
            this.groupBox10.Text = "groupBox10";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(50, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(338, 24);
            this.label14.TabIndex = 124;
            this.label14.Text = "Limits colors to a classic 256-color palette";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_256
            // 
            this.toggle_256.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_256.Checked = false;
            this.toggle_256.DarkLight_Toggler = false;
            this.toggle_256.Location = new System.Drawing.Point(394, 19);
            this.toggle_256.Name = "toggle_256";
            this.toggle_256.Size = new System.Drawing.Size(40, 20);
            this.toggle_256.TabIndex = 123;
            this.toggle_256.Text = "toggle8";
            this.toggle_256.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox10
            // 
            this.pictureBox10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(4, 4);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(40, 50);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox10.TabIndex = 122;
            this.pictureBox10.TabStop = false;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(50, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(338, 24);
            this.label15.TabIndex = 120;
            this.label15.Text = "256 color";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.Transparent;
            this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel18.Location = new System.Drawing.Point(2, 564);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(442, 5);
            this.panel18.TabIndex = 159;
            // 
            // groupBox17
            // 
            this.groupBox17.BackColor = System.Drawing.Color.Transparent;
            this.groupBox17.Controls.Add(this.label26);
            this.groupBox17.Controls.Add(this.toggle_webSafe);
            this.groupBox17.Controls.Add(this.pictureBox17);
            this.groupBox17.Controls.Add(this.label27);
            this.groupBox17.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox17.Location = new System.Drawing.Point(2, 506);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(442, 58);
            this.groupBox17.TabIndex = 158;
            this.groupBox17.Text = "groupBox17";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(50, 30);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(338, 24);
            this.label26.TabIndex = 124;
            this.label26.Text = "216-color legacy web palette";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_webSafe
            // 
            this.toggle_webSafe.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_webSafe.Checked = false;
            this.toggle_webSafe.DarkLight_Toggler = false;
            this.toggle_webSafe.Location = new System.Drawing.Point(394, 19);
            this.toggle_webSafe.Name = "toggle_webSafe";
            this.toggle_webSafe.Size = new System.Drawing.Size(40, 20);
            this.toggle_webSafe.TabIndex = 123;
            this.toggle_webSafe.Text = "toggle8";
            this.toggle_webSafe.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox17
            // 
            this.pictureBox17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox17.Image")));
            this.pictureBox17.Location = new System.Drawing.Point(4, 4);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(40, 50);
            this.pictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox17.TabIndex = 122;
            this.pictureBox17.TabStop = false;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(50, 3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(338, 24);
            this.label27.TabIndex = 120;
            this.label27.Text = "Web Safe";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(2, 501);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(442, 5);
            this.panel8.TabIndex = 135;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.Transparent;
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.toggle_sepia);
            this.groupBox7.Controls.Add(this.pictureBox7);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(2, 443);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(442, 58);
            this.groupBox7.TabIndex = 134;
            this.groupBox7.Text = "groupBox7";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(50, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(338, 24);
            this.label8.TabIndex = 124;
            this.label8.Text = "Applies a warm, vintage brown tone";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_sepia
            // 
            this.toggle_sepia.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_sepia.Checked = false;
            this.toggle_sepia.DarkLight_Toggler = false;
            this.toggle_sepia.Location = new System.Drawing.Point(394, 19);
            this.toggle_sepia.Name = "toggle_sepia";
            this.toggle_sepia.Size = new System.Drawing.Size(40, 20);
            this.toggle_sepia.TabIndex = 123;
            this.toggle_sepia.Text = "toggle5";
            this.toggle_sepia.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(4, 4);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(40, 50);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox7.TabIndex = 122;
            this.pictureBox7.TabStop = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(50, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(338, 24);
            this.label9.TabIndex = 120;
            this.label9.Text = "Sepia";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(2, 438);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 5);
            this.panel3.TabIndex = 165;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.toggle_reverse);
            this.groupBox5.Controls.Add(this.pictureBox4);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(2, 380);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(442, 58);
            this.groupBox5.TabIndex = 164;
            this.groupBox5.Text = "groupBox5";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(50, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(338, 24);
            this.label7.TabIndex = 124;
            this.label7.Text = "A, R. G, B channels become A, B, G, R";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_reverse
            // 
            this.toggle_reverse.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_reverse.Checked = false;
            this.toggle_reverse.DarkLight_Toggler = false;
            this.toggle_reverse.Location = new System.Drawing.Point(394, 19);
            this.toggle_reverse.Name = "toggle_reverse";
            this.toggle_reverse.Size = new System.Drawing.Size(40, 20);
            this.toggle_reverse.TabIndex = 123;
            this.toggle_reverse.Text = "toggle4";
            this.toggle_reverse.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(4, 4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(40, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 122;
            this.pictureBox4.TabStop = false;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(50, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(338, 24);
            this.label20.TabIndex = 120;
            this.label20.Text = "Reverse";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(2, 375);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(442, 5);
            this.panel7.TabIndex = 133;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.toggle_invert);
            this.groupBox6.Controls.Add(this.pictureBox13);
            this.groupBox6.Controls.Add(this.label30);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(2, 317);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(442, 58);
            this.groupBox6.TabIndex = 132;
            this.groupBox6.Text = "groupBox6";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(50, 30);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(338, 24);
            this.label21.TabIndex = 124;
            this.label21.Text = "Flips colors to their opposites";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggle_invert
            // 
            this.toggle_invert.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_invert.Checked = false;
            this.toggle_invert.DarkLight_Toggler = false;
            this.toggle_invert.Location = new System.Drawing.Point(394, 19);
            this.toggle_invert.Name = "toggle_invert";
            this.toggle_invert.Size = new System.Drawing.Size(40, 20);
            this.toggle_invert.TabIndex = 123;
            this.toggle_invert.Text = "toggle4";
            this.toggle_invert.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox13
            // 
            this.pictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox13.Image")));
            this.pictureBox13.Location = new System.Drawing.Point(4, 4);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(40, 50);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox13.TabIndex = 122;
            this.pictureBox13.TabStop = false;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(50, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(338, 24);
            this.label30.TabIndex = 120;
            this.label30.Text = "Invert";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Transparent;
            this.panel20.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel20.Location = new System.Drawing.Point(2, 312);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(442, 5);
            this.panel20.TabIndex = 163;
            // 
            // groupBox13
            // 
            this.groupBox13.BackColor = System.Drawing.Color.Transparent;
            this.groupBox13.Controls.Add(this.trackBarX4);
            this.groupBox13.Controls.Add(this.toggle_desaturate);
            this.groupBox13.Controls.Add(this.pictureBox14);
            this.groupBox13.Controls.Add(this.label32);
            this.groupBox13.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox13.Location = new System.Drawing.Point(2, 254);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(442, 58);
            this.groupBox13.TabIndex = 128;
            this.groupBox13.Text = "groupBox13";
            // 
            // trackBarX4
            // 
            this.trackBarX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX4.AnimateChanges = true;
            this.trackBarX4.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX4.DefaultValue = 50;
            this.trackBarX4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX4.Location = new System.Drawing.Point(51, 30);
            this.trackBarX4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX4.Maximum = 100;
            this.trackBarX4.Minimum = 0;
            this.trackBarX4.Name = "trackBarX4";
            this.trackBarX4.Size = new System.Drawing.Size(336, 24);
            this.trackBarX4.TabIndex = 124;
            this.trackBarX4.Value = 50;
            this.trackBarX4.ValueChanged += new System.EventHandler(this.effectsBars_ValueChanged);
            // 
            // toggle_desaturate
            // 
            this.toggle_desaturate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_desaturate.Checked = false;
            this.toggle_desaturate.DarkLight_Toggler = false;
            this.toggle_desaturate.Location = new System.Drawing.Point(394, 19);
            this.toggle_desaturate.Name = "toggle_desaturate";
            this.toggle_desaturate.Size = new System.Drawing.Size(40, 20);
            this.toggle_desaturate.TabIndex = 123;
            this.toggle_desaturate.Text = "toggle2";
            this.toggle_desaturate.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox14
            // 
            this.pictureBox14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.Location = new System.Drawing.Point(4, 4);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(40, 50);
            this.pictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox14.TabIndex = 122;
            this.pictureBox14.TabStop = false;
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(50, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(338, 24);
            this.label32.TabIndex = 120;
            this.label32.Text = "Desaturate";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(2, 249);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(442, 5);
            this.panel6.TabIndex = 131;
            // 
            // groupBox20
            // 
            this.groupBox20.BackColor = System.Drawing.Color.Transparent;
            this.groupBox20.Controls.Add(this.trackBarX5);
            this.groupBox20.Controls.Add(this.toggle_rotateHue);
            this.groupBox20.Controls.Add(this.pictureBox20);
            this.groupBox20.Controls.Add(this.label33);
            this.groupBox20.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox20.Location = new System.Drawing.Point(2, 191);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(442, 58);
            this.groupBox20.TabIndex = 130;
            this.groupBox20.Text = "groupBox20";
            // 
            // trackBarX5
            // 
            this.trackBarX5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX5.AnimateChanges = true;
            this.trackBarX5.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX5.DefaultValue = 0;
            this.trackBarX5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX5.Location = new System.Drawing.Point(51, 30);
            this.trackBarX5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX5.Maximum = 360;
            this.trackBarX5.Minimum = 0;
            this.trackBarX5.Name = "trackBarX5";
            this.trackBarX5.Size = new System.Drawing.Size(336, 24);
            this.trackBarX5.TabIndex = 124;
            this.trackBarX5.Value = 0;
            this.trackBarX5.ValueChanged += new System.EventHandler(this.effectsBars_ValueChanged);
            // 
            // toggle_rotateHue
            // 
            this.toggle_rotateHue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_rotateHue.Checked = false;
            this.toggle_rotateHue.DarkLight_Toggler = false;
            this.toggle_rotateHue.Location = new System.Drawing.Point(394, 19);
            this.toggle_rotateHue.Name = "toggle_rotateHue";
            this.toggle_rotateHue.Size = new System.Drawing.Size(40, 20);
            this.toggle_rotateHue.TabIndex = 123;
            this.toggle_rotateHue.Text = "toggle3";
            this.toggle_rotateHue.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox20
            // 
            this.pictureBox20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox20.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox20.Image")));
            this.pictureBox20.Location = new System.Drawing.Point(4, 4);
            this.pictureBox20.Name = "pictureBox20";
            this.pictureBox20.Size = new System.Drawing.Size(40, 50);
            this.pictureBox20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox20.TabIndex = 122;
            this.pictureBox20.TabStop = false;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(50, 3);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(338, 24);
            this.label33.TabIndex = 120;
            this.label33.Text = "Rotate hue";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(2, 186);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(442, 5);
            this.panel4.TabIndex = 127;
            // 
            // groupBox21
            // 
            this.groupBox21.BackColor = System.Drawing.Color.Transparent;
            this.groupBox21.Controls.Add(this.trackBarX2);
            this.groupBox21.Controls.Add(this.toggle_lighten);
            this.groupBox21.Controls.Add(this.pictureBox21);
            this.groupBox21.Controls.Add(this.label34);
            this.groupBox21.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox21.Location = new System.Drawing.Point(2, 128);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(442, 58);
            this.groupBox21.TabIndex = 124;
            this.groupBox21.Text = "groupBox21";
            // 
            // trackBarX2
            // 
            this.trackBarX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX2.AnimateChanges = true;
            this.trackBarX2.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX2.DefaultValue = 50;
            this.trackBarX2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX2.Location = new System.Drawing.Point(51, 30);
            this.trackBarX2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX2.Maximum = 100;
            this.trackBarX2.Minimum = 0;
            this.trackBarX2.Name = "trackBarX2";
            this.trackBarX2.Size = new System.Drawing.Size(336, 24);
            this.trackBarX2.TabIndex = 124;
            this.trackBarX2.Value = 50;
            this.trackBarX2.ValueChanged += new System.EventHandler(this.effectsBars_ValueChanged);
            // 
            // toggle_lighten
            // 
            this.toggle_lighten.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_lighten.Checked = false;
            this.toggle_lighten.DarkLight_Toggler = false;
            this.toggle_lighten.Location = new System.Drawing.Point(394, 19);
            this.toggle_lighten.Name = "toggle_lighten";
            this.toggle_lighten.Size = new System.Drawing.Size(40, 20);
            this.toggle_lighten.TabIndex = 123;
            this.toggle_lighten.Text = "toggle1";
            this.toggle_lighten.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox21
            // 
            this.pictureBox21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox21.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox21.Image")));
            this.pictureBox21.Location = new System.Drawing.Point(4, 4);
            this.pictureBox21.Name = "pictureBox21";
            this.pictureBox21.Size = new System.Drawing.Size(40, 50);
            this.pictureBox21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox21.TabIndex = 122;
            this.pictureBox21.TabStop = false;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(50, 3);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(338, 24);
            this.label34.TabIndex = 120;
            this.label34.Text = "Lighten";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(442, 5);
            this.panel2.TabIndex = 123;
            // 
            // groupBox22
            // 
            this.groupBox22.BackColor = System.Drawing.Color.Transparent;
            this.groupBox22.Controls.Add(this.trackBarX1);
            this.groupBox22.Controls.Add(this.toggle_darken);
            this.groupBox22.Controls.Add(this.pictureBox22);
            this.groupBox22.Controls.Add(this.label35);
            this.groupBox22.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox22.Location = new System.Drawing.Point(2, 65);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(442, 58);
            this.groupBox22.TabIndex = 121;
            this.groupBox22.Text = "groupBox22";
            // 
            // trackBarX1
            // 
            this.trackBarX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX1.AnimateChanges = true;
            this.trackBarX1.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX1.DefaultValue = 50;
            this.trackBarX1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX1.Location = new System.Drawing.Point(51, 30);
            this.trackBarX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX1.Maximum = 100;
            this.trackBarX1.Minimum = 0;
            this.trackBarX1.Name = "trackBarX1";
            this.trackBarX1.Size = new System.Drawing.Size(336, 24);
            this.trackBarX1.TabIndex = 124;
            this.trackBarX1.Value = 50;
            this.trackBarX1.ValueChanged += new System.EventHandler(this.effectsBars_ValueChanged);
            // 
            // toggle_darken
            // 
            this.toggle_darken.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_darken.Checked = false;
            this.toggle_darken.DarkLight_Toggler = false;
            this.toggle_darken.Location = new System.Drawing.Point(394, 19);
            this.toggle_darken.Name = "toggle_darken";
            this.toggle_darken.Size = new System.Drawing.Size(40, 20);
            this.toggle_darken.TabIndex = 123;
            this.toggle_darken.Text = "toggle1";
            this.toggle_darken.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox22
            // 
            this.pictureBox22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox22.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox22.Image")));
            this.pictureBox22.Location = new System.Drawing.Point(4, 4);
            this.pictureBox22.Name = "pictureBox22";
            this.pictureBox22.Size = new System.Drawing.Size(40, 50);
            this.pictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox22.TabIndex = 122;
            this.pictureBox22.TabStop = false;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(50, 3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(338, 24);
            this.label35.TabIndex = 120;
            this.label35.Text = "Darken";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(2, 60);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(442, 5);
            this.panel15.TabIndex = 149;
            // 
            // groupBox23
            // 
            this.groupBox23.BackColor = System.Drawing.Color.Transparent;
            this.groupBox23.Controls.Add(this.trackBarX7);
            this.groupBox23.Controls.Add(this.toggle_brightness);
            this.groupBox23.Controls.Add(this.pictureBox23);
            this.groupBox23.Controls.Add(this.label36);
            this.groupBox23.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox23.Location = new System.Drawing.Point(2, 2);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(442, 58);
            this.groupBox23.TabIndex = 148;
            this.groupBox23.Text = "groupBox23";
            // 
            // trackBarX7
            // 
            this.trackBarX7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarX7.AnimateChanges = true;
            this.trackBarX7.BackColor = System.Drawing.Color.Transparent;
            this.trackBarX7.DefaultValue = 50;
            this.trackBarX7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarX7.Location = new System.Drawing.Point(51, 31);
            this.trackBarX7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarX7.Maximum = 100;
            this.trackBarX7.Minimum = 0;
            this.trackBarX7.Name = "trackBarX7";
            this.trackBarX7.Size = new System.Drawing.Size(336, 24);
            this.trackBarX7.TabIndex = 124;
            this.trackBarX7.Value = 50;
            this.trackBarX7.ValueChanged += new System.EventHandler(this.effectsBars_ValueChanged);
            // 
            // toggle_brightness
            // 
            this.toggle_brightness.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggle_brightness.Checked = false;
            this.toggle_brightness.DarkLight_Toggler = false;
            this.toggle_brightness.Location = new System.Drawing.Point(394, 20);
            this.toggle_brightness.Name = "toggle_brightness";
            this.toggle_brightness.Size = new System.Drawing.Size(40, 20);
            this.toggle_brightness.TabIndex = 123;
            this.toggle_brightness.Text = "toggle1";
            this.toggle_brightness.CheckedChanged += new System.EventHandler(this.toggle_effects_CheckedChanged);
            // 
            // pictureBox23
            // 
            this.pictureBox23.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox23.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox23.Image")));
            this.pictureBox23.Location = new System.Drawing.Point(4, 4);
            this.pictureBox23.Name = "pictureBox23";
            this.pictureBox23.Size = new System.Drawing.Size(40, 50);
            this.pictureBox23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox23.TabIndex = 122;
            this.pictureBox23.TabStop = false;
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(50, 4);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(338, 24);
            this.label36.TabIndex = 120;
            this.label36.Text = "Change brightness";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(11, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 160;
            this.pictureBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(388, 24);
            this.label6.TabIndex = 143;
            this.label6.Text = "Apply a filter (effect) to the generated palette:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.BackColor = System.Drawing.Color.Transparent;
            this.groupBox14.Controls.Add(this.radioImage2);
            this.groupBox14.Controls.Add(this.tablessControl1);
            this.groupBox14.Controls.Add(this.RadioButton1);
            this.groupBox14.Controls.Add(this.Label2);
            this.groupBox14.Controls.Add(this.RadioButton2);
            this.groupBox14.Controls.Add(this.PictureBox2);
            this.groupBox14.Location = new System.Drawing.Point(12, 12);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(486, 139);
            this.groupBox14.TabIndex = 174;
            // 
            // radioImage2
            // 
            this.radioImage2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioImage2.Checked = true;
            this.radioImage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioImage2.ForeColor = System.Drawing.Color.White;
            this.radioImage2.Image = ((System.Drawing.Image)(resources.GetObject("radioImage2.Image")));
            this.radioImage2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage2.Location = new System.Drawing.Point(114, 5);
            this.radioImage2.Name = "radioImage2";
            this.radioImage2.Size = new System.Drawing.Size(165, 38);
            this.radioImage2.TabIndex = 144;
            this.radioImage2.Text = "Current preferences";
            this.radioImage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioImage2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radioImage2.CheckedChanged += new System.EventHandler(this.radioImage2_CheckedChanged);
            // 
            // tablessControl1
            // 
            this.tablessControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablessControl1.Controls.Add(this.tabPage3);
            this.tablessControl1.Controls.Add(this.tabPage1);
            this.tablessControl1.Controls.Add(this.tabPage2);
            this.tablessControl1.Location = new System.Drawing.Point(11, 49);
            this.tablessControl1.Multiline = true;
            this.tablessControl1.Name = "tablessControl1";
            this.tablessControl1.SelectedIndex = 0;
            this.tablessControl1.Size = new System.Drawing.Size(470, 77);
            this.tablessControl1.TabIndex = 176;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage3.Controls.Add(this.groupBox19);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(462, 49);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "0";
            // 
            // groupBox19
            // 
            this.groupBox19.BackColor = System.Drawing.Color.Transparent;
            this.groupBox19.Controls.Add(this.label31);
            this.groupBox19.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox19.Location = new System.Drawing.Point(3, 3);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(456, 77);
            this.groupBox19.TabIndex = 176;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(456, 77);
            this.label31.TabIndex = 141;
            this.label31.Text = "The palette is loaded based on the current aspect. Select a different source to c" +
    "hange the palette variation.";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(462, 49);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.flowLayoutPanel1);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 77);
            this.groupBox3.TabIndex = 176;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(441, 20);
            this.label4.TabIndex = 147;
            this.label4.Text = "Left-click a color to edit, or right-click to remove it.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 39);
            this.flowLayoutPanel1.TabIndex = 146;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button5.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.button5.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = null;
            this.button5.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.button5.ImageGlyphEnabled = true;
            this.button5.Location = new System.Drawing.Point(418, 14);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(34, 24);
            this.button5.TabIndex = 138;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(462, 49);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.TextBox1);
            this.groupBox2.Controls.Add(this.Button4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 77);
            this.groupBox2.TabIndex = 175;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.button7.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = null;
            this.button7.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.button7.ImageGlyphEnabled = true;
            this.button7.Location = new System.Drawing.Point(246, 42);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(206, 24);
            this.button7.TabIndex = 144;
            this.button7.Text = "Use the applied wallpaper";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.button6.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = null;
            this.button6.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Add;
            this.button6.ImageGlyphEnabled = true;
            this.button6.Location = new System.Drawing.Point(9, 42);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(229, 24);
            this.button6.TabIndex = 143;
            this.button6.Text = "Use wallpaper from current theme";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(12, 12);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(399, 24);
            this.TextBox1.TabIndex = 137;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.Button4.Flag = WinPaletter.UI.WP.Button.Flags.CustomColorOnHover;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = null;
            this.Button4.ImageGlyph = global::WinPaletter.Properties.Resources.Glyph_Browse;
            this.Button4.ImageGlyphEnabled = true;
            this.Button4.Location = new System.Drawing.Point(418, 12);
            this.Button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(34, 24);
            this.Button4.TabIndex = 138;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton1.Checked = false;
            this.RadioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton1.ForeColor = System.Drawing.Color.White;
            this.RadioButton1.Image = ((System.Drawing.Image)(resources.GetObject("RadioButton1.Image")));
            this.RadioButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.Location = new System.Drawing.Point(285, 5);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(95, 38);
            this.RadioButton1.TabIndex = 139;
            this.RadioButton1.Text = "Colors";
            this.RadioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(41, 7);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(67, 34);
            this.Label2.TabIndex = 141;
            this.Label2.Text = "Sources:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButton2.Checked = false;
            this.RadioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RadioButton2.ForeColor = System.Drawing.Color.White;
            this.RadioButton2.Image = ((System.Drawing.Image)(resources.GetObject("RadioButton2.Image")));
            this.RadioButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.Location = new System.Drawing.Point(386, 5);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(95, 38);
            this.RadioButton2.TabIndex = 140;
            this.RadioButton2.Text = "Image";
            this.RadioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(11, 12);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 142;
            this.PictureBox2.TabStop = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.PictureBox5);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.ImgPaletteContainer);
            this.GroupBox1.Location = new System.Drawing.Point(12, 376);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(486, 133);
            this.GroupBox1.TabIndex = 164;
            // 
            // PictureBox5
            // 
            this.PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(11, 12);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 160;
            this.PictureBox5.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(41, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(433, 24);
            this.Label1.TabIndex = 143;
            this.Label1.Text = "Generated palette (view-only):";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ImgPaletteContainer
            // 
            this.ImgPaletteContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImgPaletteContainer.AutoScroll = true;
            this.ImgPaletteContainer.Location = new System.Drawing.Point(11, 46);
            this.ImgPaletteContainer.Name = "ImgPaletteContainer";
            this.ImgPaletteContainer.Padding = new System.Windows.Forms.Padding(3);
            this.ImgPaletteContainer.Size = new System.Drawing.Size(463, 74);
            this.ImgPaletteContainer.TabIndex = 145;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.checkBox1);
            this.bottom_buttons.Controls.Add(this.Button2);
            this.bottom_buttons.Controls.Add(this.Button3);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 521);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(510, 48);
            this.bottom_buttons.TabIndex = 172;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Checked = false;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(12, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(280, 24);
            this.checkBox1.TabIndex = 159;
            this.checkBox1.Text = "Show me the generated palette";
            this.checkBox1.CheckedChanged += new WinPaletter.UI.WP.CheckBox.CheckedChangedEventHandler(this.checkBox1_CheckedChanged);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.CustomColor = System.Drawing.Color.Empty;
            this.Button2.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = null;
            this.Button2.ImageGlyph = null;
            this.Button2.ImageGlyphEnabled = false;
            this.Button2.Location = new System.Drawing.Point(404, 8);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(100, 32);
            this.Button2.TabIndex = 147;
            this.Button2.Text = "OK";
            this.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.CustomColor = System.Drawing.Color.Empty;
            this.Button3.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = null;
            this.Button3.ImageGlyph = null;
            this.Button3.ImageGlyphEnabled = false;
            this.Button3.Location = new System.Drawing.Point(298, 8);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(100, 32);
            this.Button3.TabIndex = 158;
            this.Button3.Text = "Cancel";
            this.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // PaletteGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(510, 569);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.bottom_buttons);
            this.Controls.Add(this.GroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteGenerator";
            this.Text = "Palette Generator";
            this.Load += new System.EventHandler(this.PaletteGenerateFromImage_Load);
            this.groupBox4.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.groupBox16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            this.groupBox18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.groupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.groupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            this.groupBox20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            this.groupBox21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).EndInit();
            this.groupBox22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).EndInit();
            this.groupBox23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.tablessControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.bottom_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal PictureBox PictureBox2;
        internal Label Label2;
        internal UI.WP.RadioImage RadioButton2;
        internal UI.WP.RadioImage RadioButton1;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox1;
        internal Label Label1;
        internal FlowLayoutPanel ImgPaletteContainer;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal PictureBox PictureBox5;
        internal UI.WP.GroupBox GroupBox1;
        private UI.WP.GroupBox bottom_buttons;
        private UI.WP.GroupBox groupBox14;
        private UI.WP.GroupBox groupBox2;
        private UI.WP.TablessControl tablessControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private UI.WP.GroupBox groupBox3;
        internal FlowLayoutPanel flowLayoutPanel1;
        internal UI.WP.Button button5;
        internal UI.WP.RadioImage radioImage2;
        internal UI.WP.Button button6;
        internal UI.WP.Button button7;
        internal Label label4;
        internal UI.WP.GroupBox groupBox4;
        internal PictureBox pictureBox3;
        internal Label label6;
        private Panel panel1;
        private UI.WP.GroupBox groupBox12;
        private Label label18;
        private UI.WP.Toggle toggle_grayscale;
        private PictureBox pictureBox12;
        private Label label19;
        private Panel panel12;
        private UI.WP.GroupBox groupBox11;
        private Label label16;
        private UI.WP.Toggle toggle_monochrome;
        private PictureBox pictureBox11;
        private Label label17;
        private Panel panel16;
        private UI.WP.GroupBox groupBox15;
        private Label label22;
        private UI.WP.Toggle toggle_androidMaterialExpressive;
        private PictureBox pictureBox15;
        private Label label23;
        private Panel panel10;
        private UI.WP.GroupBox groupBox9;
        private Label label12;
        private UI.WP.Toggle toggle_androidMaterial;
        private PictureBox pictureBox9;
        private Label label13;
        private Panel panel17;
        private UI.WP.GroupBox groupBox16;
        private Label label24;
        private UI.WP.Toggle toggle_Mac;
        private PictureBox pictureBox16;
        private Label label25;
        private Panel panel19;
        private UI.WP.GroupBox groupBox18;
        private Label label28;
        private UI.WP.Toggle toggle_metro;
        private PictureBox pictureBox18;
        private Label label29;
        private Panel panel9;
        private UI.WP.GroupBox groupBox8;
        private Label label10;
        private UI.WP.Toggle toggle_frutigerAero;
        private PictureBox pictureBox8;
        private Label label11;
        private Panel panel11;
        private UI.WP.GroupBox groupBox10;
        private Label label14;
        private UI.WP.Toggle toggle_256;
        private PictureBox pictureBox10;
        private Label label15;
        private Panel panel18;
        private UI.WP.GroupBox groupBox17;
        private Label label26;
        private UI.WP.Toggle toggle_webSafe;
        private PictureBox pictureBox17;
        private Label label27;
        private Panel panel8;
        private UI.WP.GroupBox groupBox7;
        private Label label8;
        private UI.WP.Toggle toggle_sepia;
        private PictureBox pictureBox7;
        private Label label9;
        private Panel panel3;
        private UI.WP.GroupBox groupBox5;
        private Label label7;
        private UI.WP.Toggle toggle_reverse;
        private PictureBox pictureBox4;
        private Label label20;
        private Panel panel7;
        private UI.WP.GroupBox groupBox6;
        private Label label21;
        private UI.WP.Toggle toggle_invert;
        private PictureBox pictureBox13;
        private Label label30;
        private Panel panel20;
        private UI.WP.GroupBox groupBox13;
        private UI.Controllers.TrackBarX trackBarX4;
        private UI.WP.Toggle toggle_desaturate;
        private PictureBox pictureBox14;
        private Label label32;
        private Panel panel6;
        private UI.WP.GroupBox groupBox20;
        private UI.Controllers.TrackBarX trackBarX5;
        private UI.WP.Toggle toggle_rotateHue;
        private PictureBox pictureBox20;
        private Label label33;
        private Panel panel4;
        private UI.WP.GroupBox groupBox21;
        private UI.Controllers.TrackBarX trackBarX2;
        private UI.WP.Toggle toggle_lighten;
        private PictureBox pictureBox21;
        private Label label34;
        private Panel panel2;
        private UI.WP.GroupBox groupBox22;
        private UI.Controllers.TrackBarX trackBarX1;
        private UI.WP.Toggle toggle_darken;
        private PictureBox pictureBox22;
        private Label label35;
        private Panel panel15;
        private UI.WP.GroupBox groupBox23;
        private UI.Controllers.TrackBarX trackBarX7;
        private UI.WP.Toggle toggle_brightness;
        private PictureBox pictureBox23;
        private Label label36;
        private UI.WP.Toggle toggle1;
        private Panel panel14;
        private TabPage tabPage3;
        private UI.WP.GroupBox groupBox19;
        internal Label label31;
        private UI.WP.CheckBox checkBox1;
    }
}
