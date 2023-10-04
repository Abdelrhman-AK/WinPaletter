using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_JSON_GUI : Form
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_JSON_GUI));
            FontDialog1 = new FontDialog();
            OpenJSONDlg = new OpenFileDialog();
            SaveJSONDlg = new SaveFileDialog();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            GroupBox3 = new UI.WP.GroupBox();
            AlertBox3 = new UI.WP.AlertBox();
            ProgressBar2 = new ProgressBar();
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            TabControl1 = new UI.WP.TabControl();
            TabControl1.SelectedIndexChanged += new EventHandler(TabControl1_SelectedIndexChanged);
            TabPage1 = new TabPage();
            GroupBox5 = new UI.WP.GroupBox();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            PictureBox21 = new PictureBox();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            PictureBox25 = new PictureBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Label10 = new Label();
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Label20 = new Label();
            PictureBox22 = new PictureBox();
            PictureBox24 = new PictureBox();
            Label13 = new Label();
            Label18 = new Label();
            RadioButton2 = new UI.WP.RadioButton();
            PictureBox23 = new PictureBox();
            RadioButton1 = new UI.WP.RadioButton();
            RadioButton1.CheckedChanged += new UI.WP.RadioButton.CheckedChangedEventHandler(RadioButton1_CheckedChanged);
            PictureBox26 = new PictureBox();
            TextBox7 = new UI.WP.TextBox();
            Label15 = new Label();
            TextBox6 = new UI.WP.TextBox();
            Label22 = new Label();
            TextBox5 = new UI.WP.TextBox();
            TextBox3 = new UI.WP.TextBox();
            TextBox4 = new UI.WP.TextBox();
            TabPage2 = new TabPage();
            GroupBox6 = new UI.WP.GroupBox();
            data = new DataGridView();
            data.CellPainting += new DataGridViewCellPaintingEventHandler(data_CellPainting);
            data.CellEndEdit += new DataGridViewCellEventHandler(data_CellEndEdit);
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            GroupBox9 = new UI.WP.GroupBox();
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            CheckBox2 = new UI.WP.CheckBox();
            CheckBox2.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            TextBox8 = new UI.WP.TextBox();
            TextBox8.TextChanged += new EventHandler(TextBox8_TextChanged);
            Label6 = new Label();
            CheckBox1 = new UI.WP.CheckBox();
            CheckBox1.CheckedChanged += new UI.WP.CheckBox.CheckedChangedEventHandler(CheckBox1_CheckedChanged);
            TabPage3 = new TabPage();
            GroupBox1 = new UI.WP.GroupBox();
            SplitContainer1 = new SplitContainer();
            GroupBox4 = new UI.WP.GroupBox();
            Label5 = new Label();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            ComboBox1 = new UI.WP.ComboBox();
            ProgressBar1 = new ProgressBar();
            GroupBox2 = new UI.WP.GroupBox();
            GroupBox7 = new UI.WP.GroupBox();
            TextBox9 = new UI.WP.TextBox();
            TextBox9.TextChanged += new EventHandler(TextBox9_TextChanged);
            Button13 = new UI.WP.Button();
            Button13.Click += new EventHandler(Button13_Click);
            PictureBox3 = new PictureBox();
            PictureBox1 = new PictureBox();
            Label2 = new Label();
            Label3 = new Label();
            TextBox1 = new UI.WP.TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            Label4 = new Label();
            Label1 = new Label();
            PictureBox2 = new PictureBox();
            TextBox2 = new UI.WP.TextBox();
            AlertBox1 = new UI.WP.AlertBox();
            GroupBox8 = new UI.WP.GroupBox();
            PictureBox4 = new PictureBox();
            Label9 = new Label();
            Label8 = new Label();
            ToolTip = new ToolTip(components);
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click);
            GroupBox3.SuspendLayout();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).BeginInit();
            TabPage2.SuspendLayout();
            GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)data).BeginInit();
            GroupBox9.SuspendLayout();
            TabPage3.SuspendLayout();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
            SplitContainer1.Panel1.SuspendLayout();
            SplitContainer1.Panel2.SuspendLayout();
            SplitContainer1.SuspendLayout();
            GroupBox4.SuspendLayout();
            GroupBox2.SuspendLayout();
            GroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            SuspendLayout();
            // 
            // OpenJSONDlg
            // 
            OpenJSONDlg.Filter = "JSON File (*.json)|*.json";
            // 
            // SaveJSONDlg
            // 
            SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*";
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.LineColor = Color.FromArgb(199, 49, 61);
            Button7.Location = new Point(668, 671);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 205;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = (Image)resources.GetObject("Button2.Image");
            Button2.ImageAlign = ContentAlignment.MiddleLeft;
            Button2.LineColor = Color.FromArgb(69, 110, 129);
            Button2.Location = new Point(840, 671);
            Button2.Name = "Button2";
            Button2.Size = new Size(150, 34);
            Button2.TabIndex = 204;
            Button2.Text = "Save into open file";
            Button2.UseVisualStyleBackColor = false;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(AlertBox3);
            GroupBox3.Controls.Add(ProgressBar2);
            GroupBox3.Controls.Add(Button6);
            GroupBox3.Controls.Add(Button5);
            GroupBox3.Controls.Add(Button8);
            GroupBox3.Controls.Add(Button4);
            GroupBox3.Location = new Point(12, 12);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(978, 110);
            GroupBox3.TabIndex = 200;
            // 
            // AlertBox3
            // 
            AlertBox3.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox3.BackColor = Color.FromArgb(17, 67, 91);
            AlertBox3.CenterText = false;
            AlertBox3.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox3.Font = new Font("Segoe UI", 9.0f);
            AlertBox3.Image = Properties.Resources.notify_info;
            AlertBox3.Location = new Point(7, 41);
            AlertBox3.Name = "AlertBox3";
            AlertBox3.Size = new Size(964, 62);
            AlertBox3.TabIndex = 208;
            AlertBox3.TabStop = false;
            AlertBox3.Text = resources.GetString("AlertBox3.Text");
            // 
            // ProgressBar2
            // 
            ProgressBar2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ProgressBar2.Location = new Point(742, 10);
            ProgressBar2.Name = "ProgressBar2";
            ProgressBar2.Size = new Size(229, 20);
            ProgressBar2.TabIndex = 208;
            ProgressBar2.Visible = false;
            // 
            // Button6
            // 
            Button6.BackColor = Color.FromArgb(43, 43, 43);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.ImageAlign = ContentAlignment.MiddleRight;
            Button6.LineColor = Color.FromArgb(51, 15, 67);
            Button6.Location = new Point(540, 6);
            Button6.Name = "Button6";
            Button6.Size = new Size(146, 29);
            Button6.TabIndex = 203;
            Button6.Text = "Change preview font";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.BackColor = Color.FromArgb(43, 43, 43);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.ImageAlign = ContentAlignment.MiddleRight;
            Button5.LineColor = Color.FromArgb(108, 138, 121);
            Button5.Location = new Point(344, 6);
            Button5.Name = "Button5";
            Button5.Size = new Size(190, 29);
            Button5.TabIndex = 112;
            Button5.Text = "Generate new (English) only";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.BackColor = Color.FromArgb(43, 43, 43);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleRight;
            Button8.LineColor = Color.FromArgb(164, 125, 25);
            Button8.Location = new Point(7, 6);
            Button8.Name = "Button8";
            Button8.Size = new Size(102, 29);
            Button8.TabIndex = 110;
            Button8.Text = "Open from";
            Button8.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.BackColor = Color.FromArgb(43, 43, 43);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.ImageAlign = ContentAlignment.MiddleRight;
            Button4.LineColor = Color.FromArgb(108, 138, 121);
            Button4.Location = new Point(115, 6);
            Button4.Name = "Button4";
            Button4.Size = new Size(223, 29);
            Button4.TabIndex = 111;
            Button4.Text = "Generate new (English) and open It";
            Button4.UseVisualStyleBackColor = false;
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ItemSize = new Size(40, 150);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(12, 128);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(978, 537);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 207;
            TabControl1.Visible = false;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(GroupBox5);
            TabPage1.Location = new Point(154, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(5, 3, 3, 3);
            TabPage1.Size = new Size(820, 529);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Language file info";
            // 
            // GroupBox5
            // 
            GroupBox5.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox5.Controls.Add(Button11);
            GroupBox5.Controls.Add(PictureBox21);
            GroupBox5.Controls.Add(Button10);
            GroupBox5.Controls.Add(PictureBox25);
            GroupBox5.Controls.Add(Button3);
            GroupBox5.Controls.Add(Label10);
            GroupBox5.Controls.Add(Button9);
            GroupBox5.Controls.Add(Label20);
            GroupBox5.Controls.Add(PictureBox22);
            GroupBox5.Controls.Add(PictureBox24);
            GroupBox5.Controls.Add(Label13);
            GroupBox5.Controls.Add(Label18);
            GroupBox5.Controls.Add(RadioButton2);
            GroupBox5.Controls.Add(PictureBox23);
            GroupBox5.Controls.Add(RadioButton1);
            GroupBox5.Controls.Add(PictureBox26);
            GroupBox5.Controls.Add(TextBox7);
            GroupBox5.Controls.Add(Label15);
            GroupBox5.Controls.Add(TextBox6);
            GroupBox5.Controls.Add(Label22);
            GroupBox5.Controls.Add(TextBox5);
            GroupBox5.Controls.Add(TextBox3);
            GroupBox5.Controls.Add(TextBox4);
            GroupBox5.Dock = DockStyle.Top;
            GroupBox5.Location = new Point(5, 3);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Size = new Size(812, 181);
            GroupBox5.TabIndex = 76;
            GroupBox5.Text = "GroupBox5";
            // 
            // Button11
            // 
            Button11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button11.BackColor = Color.FromArgb(43, 43, 43);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = null;
            Button11.LineColor = Color.FromArgb(32, 79, 131);
            Button11.Location = new Point(669, 63);
            Button11.Name = "Button11";
            Button11.Size = new Size(140, 24);
            Button11.TabIndex = 75;
            Button11.Text = "Insert current";
            Button11.UseVisualStyleBackColor = false;
            // 
            // PictureBox21
            // 
            PictureBox21.Image = (Image)resources.GetObject("PictureBox21.Image");
            PictureBox21.Location = new Point(3, 63);
            PictureBox21.Name = "PictureBox21";
            PictureBox21.Size = new Size(24, 24);
            PictureBox21.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox21.TabIndex = 44;
            PictureBox21.TabStop = false;
            // 
            // Button10
            // 
            Button10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button10.BackColor = Color.FromArgb(43, 43, 43);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = (Image)resources.GetObject("Button10.Image");
            Button10.ImageAlign = ContentAlignment.MiddleRight;
            Button10.LineColor = Color.FromArgb(32, 79, 131);
            Button10.Location = new Point(669, 33);
            Button10.Name = "Button10";
            Button10.Size = new Size(140, 24);
            Button10.TabIndex = 74;
            Button10.Text = "Language snippets";
            Button10.UseVisualStyleBackColor = false;
            // 
            // PictureBox25
            // 
            PictureBox25.Image = (Image)resources.GetObject("PictureBox25.Image");
            PictureBox25.Location = new Point(3, 3);
            PictureBox25.Name = "PictureBox25";
            PictureBox25.Size = new Size(24, 24);
            PictureBox25.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox25.TabIndex = 53;
            PictureBox25.TabStop = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.LineColor = Color.FromArgb(32, 79, 131);
            Button3.Location = new Point(669, 123);
            Button3.Name = "Button3";
            Button3.Size = new Size(140, 24);
            Button3.TabIndex = 73;
            Button3.Text = "Insert current";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Label10
            // 
            Label10.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label10.Location = new Point(33, 63);
            Label10.Name = "Label10";
            Label10.Size = new Size(143, 24);
            Label10.TabIndex = 45;
            Label10.Text = "Translator name:";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button9
            // 
            Button9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.ImageAlign = ContentAlignment.MiddleRight;
            Button9.LineColor = Color.FromArgb(32, 79, 131);
            Button9.Location = new Point(669, 3);
            Button9.Name = "Button9";
            Button9.Size = new Size(140, 24);
            Button9.TabIndex = 72;
            Button9.Text = "Language snippets";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Label20
            // 
            Label20.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label20.Location = new Point(33, 3);
            Label20.Name = "Label20";
            Label20.Size = new Size(143, 24);
            Label20.TabIndex = 54;
            Label20.Text = "Language:";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox22
            // 
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(3, 93);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(24, 24);
            PictureBox22.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox22.TabIndex = 47;
            PictureBox22.TabStop = false;
            // 
            // PictureBox24
            // 
            PictureBox24.Image = (Image)resources.GetObject("PictureBox24.Image");
            PictureBox24.Location = new Point(3, 33);
            PictureBox24.Name = "PictureBox24";
            PictureBox24.Size = new Size(24, 24);
            PictureBox24.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox24.TabIndex = 56;
            PictureBox24.TabStop = false;
            // 
            // Label13
            // 
            Label13.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label13.Location = new Point(33, 93);
            Label13.Name = "Label13";
            Label13.Size = new Size(143, 24);
            Label13.TabIndex = 48;
            Label13.Text = "Translation version:";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label18
            // 
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label18.Location = new Point(33, 33);
            Label18.Name = "Label18";
            Label18.Size = new Size(143, 24);
            Label18.TabIndex = 57;
            Label18.Text = "Language code:";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RadioButton2
            // 
            RadioButton2.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Location = new Point(330, 153);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(142, 24);
            RadioButton2.TabIndex = 67;
            RadioButton2.Text = "Right to left";
            // 
            // PictureBox23
            // 
            PictureBox23.Image = (Image)resources.GetObject("PictureBox23.Image");
            PictureBox23.Location = new Point(3, 123);
            PictureBox23.Name = "PictureBox23";
            PictureBox23.Size = new Size(24, 24);
            PictureBox23.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox23.TabIndex = 50;
            PictureBox23.TabStop = false;
            // 
            // RadioButton1
            // 
            RadioButton1.BackColor = Color.FromArgb(34, 34, 34);
            RadioButton1.Checked = true;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Location = new Point(182, 153);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(142, 24);
            RadioButton1.TabIndex = 66;
            RadioButton1.Text = "Left to right";
            // 
            // PictureBox26
            // 
            PictureBox26.Image = (Image)resources.GetObject("PictureBox26.Image");
            PictureBox26.Location = new Point(3, 153);
            PictureBox26.Name = "PictureBox26";
            PictureBox26.Size = new Size(24, 24);
            PictureBox26.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox26.TabIndex = 59;
            PictureBox26.TabStop = false;
            // 
            // TextBox7
            // 
            TextBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox7.BackColor = Color.FromArgb(55, 55, 55);
            TextBox7.DrawOnGlass = false;
            TextBox7.ForeColor = Color.White;
            TextBox7.Location = new Point(182, 123);
            TextBox7.MaxLength = 32767;
            TextBox7.Multiline = false;
            TextBox7.Name = "TextBox7";
            TextBox7.ReadOnly = false;
            TextBox7.Scrollbars = ScrollBars.None;
            TextBox7.SelectedText = "";
            TextBox7.SelectionLength = 0;
            TextBox7.SelectionStart = 0;
            TextBox7.Size = new Size(481, 24);
            TextBox7.TabIndex = 65;
            TextBox7.TextAlign = HorizontalAlignment.Left;
            TextBox7.UseSystemPasswordChar = false;
            TextBox7.WordWrap = true;
            // 
            // Label15
            // 
            Label15.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label15.Location = new Point(33, 123);
            Label15.Name = "Label15";
            Label15.Size = new Size(143, 24);
            Label15.TabIndex = 51;
            Label15.Text = "For app version:";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox6
            // 
            TextBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox6.BackColor = Color.FromArgb(55, 55, 55);
            TextBox6.DrawOnGlass = false;
            TextBox6.ForeColor = Color.White;
            TextBox6.Location = new Point(182, 93);
            TextBox6.MaxLength = 32767;
            TextBox6.Multiline = false;
            TextBox6.Name = "TextBox6";
            TextBox6.ReadOnly = false;
            TextBox6.Scrollbars = ScrollBars.None;
            TextBox6.SelectedText = "";
            TextBox6.SelectionLength = 0;
            TextBox6.SelectionStart = 0;
            TextBox6.Size = new Size(627, 24);
            TextBox6.TabIndex = 64;
            TextBox6.TextAlign = HorizontalAlignment.Left;
            TextBox6.UseSystemPasswordChar = false;
            TextBox6.WordWrap = true;
            // 
            // Label22
            // 
            Label22.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label22.Location = new Point(33, 153);
            Label22.Name = "Label22";
            Label22.Size = new Size(143, 24);
            Label22.TabIndex = 60;
            Label22.Text = "Layout";
            Label22.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox5
            // 
            TextBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox5.BackColor = Color.FromArgb(55, 55, 55);
            TextBox5.DrawOnGlass = false;
            TextBox5.ForeColor = Color.White;
            TextBox5.Location = new Point(182, 63);
            TextBox5.MaxLength = 32767;
            TextBox5.Multiline = false;
            TextBox5.Name = "TextBox5";
            TextBox5.ReadOnly = false;
            TextBox5.Scrollbars = ScrollBars.None;
            TextBox5.SelectedText = "";
            TextBox5.SelectionLength = 0;
            TextBox5.SelectionStart = 0;
            TextBox5.Size = new Size(481, 24);
            TextBox5.TabIndex = 63;
            TextBox5.TextAlign = HorizontalAlignment.Left;
            TextBox5.UseSystemPasswordChar = false;
            TextBox5.WordWrap = true;
            // 
            // TextBox3
            // 
            TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox3.BackColor = Color.FromArgb(55, 55, 55);
            TextBox3.DrawOnGlass = false;
            TextBox3.ForeColor = Color.White;
            TextBox3.Location = new Point(182, 3);
            TextBox3.MaxLength = 32767;
            TextBox3.Multiline = false;
            TextBox3.Name = "TextBox3";
            TextBox3.ReadOnly = false;
            TextBox3.Scrollbars = ScrollBars.None;
            TextBox3.SelectedText = "";
            TextBox3.SelectionLength = 0;
            TextBox3.SelectionStart = 0;
            TextBox3.Size = new Size(481, 24);
            TextBox3.TabIndex = 61;
            TextBox3.TextAlign = HorizontalAlignment.Left;
            TextBox3.UseSystemPasswordChar = false;
            TextBox3.WordWrap = true;
            // 
            // TextBox4
            // 
            TextBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox4.BackColor = Color.FromArgb(55, 55, 55);
            TextBox4.DrawOnGlass = false;
            TextBox4.ForeColor = Color.White;
            TextBox4.Location = new Point(182, 33);
            TextBox4.MaxLength = 32767;
            TextBox4.Multiline = false;
            TextBox4.Name = "TextBox4";
            TextBox4.ReadOnly = false;
            TextBox4.Scrollbars = ScrollBars.None;
            TextBox4.SelectedText = "";
            TextBox4.SelectionLength = 0;
            TextBox4.SelectionStart = 0;
            TextBox4.Size = new Size(481, 24);
            TextBox4.TabIndex = 62;
            TextBox4.TextAlign = HorizontalAlignment.Left;
            TextBox4.UseSystemPasswordChar = false;
            TextBox4.WordWrap = true;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(GroupBox6);
            TabPage2.Location = new Point(154, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(5, 3, 3, 3);
            TabPage2.Size = new Size(820, 529);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Global strings";
            // 
            // GroupBox6
            // 
            GroupBox6.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox6.Controls.Add(data);
            GroupBox6.Controls.Add(GroupBox9);
            GroupBox6.Dock = DockStyle.Fill;
            GroupBox6.Location = new Point(5, 3);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Padding = new Padding(3);
            GroupBox6.Size = new Size(812, 523);
            GroupBox6.TabIndex = 207;
            GroupBox6.Text = "GroupBox6";
            // 
            // data
            // 
            data.AllowUserToAddRows = false;
            data.AllowUserToDeleteRows = false;
            data.AllowUserToResizeColumns = false;
            data.AllowUserToResizeRows = false;
            data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            data.BackgroundColor = Color.FromArgb(25, 25, 25);
            data.BorderStyle = BorderStyle.None;
            data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            data.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            data.Dock = DockStyle.Fill;
            data.Location = new Point(3, 33);
            data.Name = "data";
            data.RowHeadersVisible = false;
            data.ShowCellErrors = false;
            data.ShowCellToolTips = false;
            data.ShowEditingIcon = false;
            data.ShowRowErrors = false;
            data.Size = new Size(806, 487);
            data.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Variable";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 255;
            // 
            // Column2
            // 
            Column2.HeaderText = "Value (red means not translated)";
            Column2.Name = "Column2";
            Column2.Width = 254;
            // 
            // Column3
            // 
            Column3.HeaderText = "English (original) value";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 255;
            // 
            // GroupBox9
            // 
            GroupBox9.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox9.Controls.Add(Button12);
            GroupBox9.Controls.Add(CheckBox2);
            GroupBox9.Controls.Add(TextBox8);
            GroupBox9.Controls.Add(Label6);
            GroupBox9.Controls.Add(CheckBox1);
            GroupBox9.Dock = DockStyle.Top;
            GroupBox9.Location = new Point(3, 3);
            GroupBox9.Name = "GroupBox9";
            GroupBox9.Size = new Size(806, 30);
            GroupBox9.TabIndex = 117;
            // 
            // Button12
            // 
            Button12.BackColor = Color.FromArgb(43, 43, 43);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = (Image)resources.GetObject("Button12.Image");
            Button12.ImageAlign = ContentAlignment.MiddleRight;
            Button12.LineColor = Color.FromArgb(10, 69, 101);
            Button12.Location = new Point(3, 3);
            Button12.Name = "Button12";
            Button12.Size = new Size(24, 24);
            Button12.TabIndex = 111;
            ToolTip.SetToolTip(Button12, "Replace");
            Button12.UseVisualStyleBackColor = false;
            // 
            // CheckBox2
            // 
            CheckBox2.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox2.Checked = false;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(487, 3);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(117, 24);
            CheckBox2.TabIndex = 116;
            CheckBox2.Text = "Variables";
            // 
            // TextBox8
            // 
            TextBox8.BackColor = Color.FromArgb(55, 55, 55);
            TextBox8.DrawOnGlass = false;
            TextBox8.ForeColor = Color.White;
            TextBox8.Location = new Point(33, 3);
            TextBox8.MaxLength = 32767;
            TextBox8.Multiline = false;
            TextBox8.Name = "TextBox8";
            TextBox8.ReadOnly = false;
            TextBox8.Scrollbars = ScrollBars.None;
            TextBox8.SelectedText = "";
            TextBox8.SelectionLength = 0;
            TextBox8.SelectionStart = 0;
            TextBox8.Size = new Size(213, 24);
            TextBox8.TabIndex = 112;
            TextBox8.TextAlign = HorizontalAlignment.Left;
            TextBox8.UseSystemPasswordChar = false;
            TextBox8.WordWrap = true;
            // 
            // Label6
            // 
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(252, 3);
            Label6.Name = "Label6";
            Label6.Size = new Size(106, 24);
            Label6.TabIndex = 115;
            Label6.Text = "Search also in:";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(43, 43, 43);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(364, 3);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(117, 24);
            CheckBox1.TabIndex = 114;
            CheckBox1.Text = "English values";
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(GroupBox1);
            TabPage3.Location = new Point(154, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(5, 0, 0, 0);
            TabPage3.Size = new Size(820, 529);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Forms strings";
            // 
            // GroupBox1
            // 
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(SplitContainer1);
            GroupBox1.Dock = DockStyle.Fill;
            GroupBox1.Location = new Point(5, 0);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(3);
            GroupBox1.Size = new Size(815, 529);
            GroupBox1.TabIndex = 201;
            // 
            // SplitContainer1
            // 
            SplitContainer1.Dock = DockStyle.Fill;
            SplitContainer1.FixedPanel = FixedPanel.Panel2;
            SplitContainer1.Location = new Point(3, 3);
            SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            SplitContainer1.Panel1.Controls.Add(GroupBox4);
            SplitContainer1.Panel1.Padding = new Padding(1);
            // 
            // SplitContainer1.Panel2
            // 
            SplitContainer1.Panel2.Controls.Add(GroupBox2);
            SplitContainer1.Size = new Size(809, 523);
            SplitContainer1.SplitterDistance = 479;
            SplitContainer1.TabIndex = 0;
            // 
            // GroupBox4
            // 
            GroupBox4.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox4.Controls.Add(Label5);
            GroupBox4.Controls.Add(Button1);
            GroupBox4.Controls.Add(ComboBox1);
            GroupBox4.Controls.Add(ProgressBar1);
            GroupBox4.Dock = DockStyle.Top;
            GroupBox4.Location = new Point(1, 1);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Padding = new Padding(1);
            GroupBox4.Size = new Size(477, 71);
            GroupBox4.TabIndex = 203;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(8, 7);
            Label5.Name = "Label5";
            Label5.Size = new Size(461, 24);
            Label5.TabIndex = 207;
            Label5.Text = "Choose a form then open it. When you finish translation, close the child form.";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(50, 50, 50);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.LineColor = Color.FromArgb(0, 81, 210);
            Button1.Location = new Point(393, 37);
            Button1.Name = "Button1";
            Button1.Size = new Size(76, 26);
            Button1.TabIndex = 1;
            Button1.Text = "Open";
            Button1.UseVisualStyleBackColor = false;
            // 
            // ComboBox1
            // 
            ComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox1.BackColor = Color.FromArgb(55, 55, 55);
            ComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox1.DropDownHeight = 242;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.IntegralHeight = false;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Location = new Point(9, 37);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(378, 26);
            ComboBox1.TabIndex = 0;
            // 
            // ProgressBar1
            // 
            ProgressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProgressBar1.Location = new Point(10, 38);
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(456, 24);
            ProgressBar1.TabIndex = 209;
            ProgressBar1.Visible = false;
            // 
            // GroupBox2
            // 
            GroupBox2.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox2.Controls.Add(GroupBox7);
            GroupBox2.Controls.Add(PictureBox3);
            GroupBox2.Controls.Add(PictureBox1);
            GroupBox2.Controls.Add(Label2);
            GroupBox2.Controls.Add(Label3);
            GroupBox2.Controls.Add(TextBox1);
            GroupBox2.Controls.Add(Label4);
            GroupBox2.Controls.Add(Label1);
            GroupBox2.Controls.Add(PictureBox2);
            GroupBox2.Controls.Add(TextBox2);
            GroupBox2.Dock = DockStyle.Fill;
            GroupBox2.Location = new Point(0, 0);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(326, 523);
            GroupBox2.TabIndex = 0;
            GroupBox2.Text = "GroupBox2";
            // 
            // GroupBox7
            // 
            GroupBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox7.BackColor = Color.FromArgb(51, 51, 51);
            GroupBox7.Controls.Add(TextBox9);
            GroupBox7.Controls.Add(Button13);
            GroupBox7.Location = new Point(3, 3);
            GroupBox7.Name = "GroupBox7";
            GroupBox7.Size = new Size(323, 30);
            GroupBox7.TabIndex = 25;
            GroupBox7.Text = "GroupBox7";
            // 
            // TextBox9
            // 
            TextBox9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox9.BackColor = Color.FromArgb(55, 55, 55);
            TextBox9.DrawOnGlass = false;
            TextBox9.ForeColor = Color.White;
            TextBox9.Location = new Point(33, 3);
            TextBox9.MaxLength = 32767;
            TextBox9.Multiline = false;
            TextBox9.Name = "TextBox9";
            TextBox9.ReadOnly = false;
            TextBox9.Scrollbars = ScrollBars.None;
            TextBox9.SelectedText = "";
            TextBox9.SelectionLength = 0;
            TextBox9.SelectionStart = 0;
            TextBox9.Size = new Size(286, 24);
            TextBox9.TabIndex = 112;
            TextBox9.TextAlign = HorizontalAlignment.Left;
            TextBox9.UseSystemPasswordChar = false;
            TextBox9.WordWrap = true;
            // 
            // Button13
            // 
            Button13.BackColor = Color.FromArgb(43, 43, 43);
            Button13.DrawOnGlass = false;
            Button13.Font = new Font("Segoe UI", 9.0f);
            Button13.ForeColor = Color.White;
            Button13.Image = (Image)resources.GetObject("Button13.Image");
            Button13.ImageAlign = ContentAlignment.MiddleRight;
            Button13.LineColor = Color.FromArgb(10, 69, 101);
            Button13.Location = new Point(3, 3);
            Button13.Name = "Button13";
            Button13.Size = new Size(24, 24);
            Button13.TabIndex = 112;
            ToolTip.SetToolTip(Button13, "Replace");
            Button13.UseVisualStyleBackColor = false;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(6, 69);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 24;
            PictureBox3.TabStop = false;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(6, 39);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 20;
            PictureBox1.TabStop = false;
            // 
            // Label2
            // 
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(36, 300);
            Label2.Name = "Label2";
            Label2.Size = new Size(285, 24);
            Label2.TabIndex = 17;
            Label2.Text = "Current value:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(36, 69);
            Label3.Name = "Label3";
            Label3.Size = new Size(285, 24);
            Label3.TabIndex = 22;
            Label3.Text = "English value:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox2
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(39, 327);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = true;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.Vertical;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(282, 193);
            TextBox1.TabIndex = 19;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(138, 40);
            Label4.Name = "Label4";
            Label4.Size = new Size(184, 24);
            Label4.TabIndex = 18;
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(36, 39);
            Label1.Name = "Label1";
            Label1.Size = new Size(95, 24);
            Label1.TabIndex = 16;
            Label1.Text = "Control name:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(5, 299);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 21;
            PictureBox2.TabStop = false;
            // 
            // TextBox2
            // 
            TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(40, 97);
            TextBox2.MaxLength = 32767;
            TextBox2.Multiline = true;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = true;
            TextBox2.Scrollbars = ScrollBars.Vertical;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(282, 200);
            TextBox2.TabIndex = 23;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox1.Anchor = AnchorStyles.None;
            AlertBox1.BackColor = Color.FromArgb(125, 20, 30);
            AlertBox1.CenterText = true;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(266, 334);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(470, 48);
            AlertBox1.TabIndex = 209;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "There is no open file, please open a file to show GUI editor";
            // 
            // GroupBox8
            // 
            GroupBox8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox8.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox8.Controls.Add(PictureBox4);
            GroupBox8.Controls.Add(Label9);
            GroupBox8.Controls.Add(Label8);
            GroupBox8.Location = new Point(12, 672);
            GroupBox8.Name = "GroupBox8";
            GroupBox8.Size = new Size(650, 32);
            GroupBox8.TabIndex = 210;
            GroupBox8.Visible = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(4, 4);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 24);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 68;
            PictureBox4.TabStop = false;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label9.Location = new Point(158, 5);
            Label9.Name = "Label9";
            Label9.Size = new Size(489, 22);
            Label9.TabIndex = 71;
            Label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label8
            // 
            Label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label8.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label8.Location = new Point(34, 5);
            Label8.Name = "Label8";
            Label8.Size = new Size(118, 22);
            Label8.TabIndex = 69;
            Label8.Text = "Current open file:";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(34, 34, 34);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = (Image)resources.GetObject("Button1.Image");
            Button14.ImageAlign = ContentAlignment.MiddleLeft;
            Button14.LineColor = Color.FromArgb(30, 107, 146);
            Button14.Location = new Point(754, 671);
            Button14.Name = "Button14";
            Button14.Size = new Size(80, 34);
            Button14.TabIndex = 211;
            Button14.Text = "Help";
            Button14.UseVisualStyleBackColor = false;
            // 
            // Lang_JSON_GUI
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(1002, 717);
            Controls.Add(Button7);
            Controls.Add(Button2);
            Controls.Add(GroupBox3);
            Controls.Add(TabControl1);
            Controls.Add(AlertBox1);
            Controls.Add(GroupBox8);
            Controls.Add(Button14);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Lang_JSON_GUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GUI language editor";
            WindowState = FormWindowState.Maximized;
            GroupBox3.ResumeLayout(false);
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            GroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox21).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).EndInit();
            TabPage2.ResumeLayout(false);
            GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)data).EndInit();
            GroupBox9.ResumeLayout(false);
            TabPage3.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            SplitContainer1.Panel1.ResumeLayout(false);
            SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
            SplitContainer1.ResumeLayout(false);
            GroupBox4.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            GroupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            ControlSelection += new ControlSelectionEventHandler(Lang_JSON_GUI_ControlSelection);
            FormClosing += new FormClosingEventHandler(Lang_JSON_GUI_FormClosing);
            Load += new EventHandler(Lang_JSON_GUI_Load);
            ResumeLayout(false);

        }
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button4;
        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.GroupBox GroupBox4;
        internal UI.WP.Button Button1;
        internal UI.WP.ComboBox ComboBox1;
        internal PictureBox PictureBox3;
        internal UI.WP.TextBox TextBox2;
        internal Label Label3;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox1;
        internal UI.WP.TextBox TextBox1;
        internal Label Label4;
        internal Label Label2;
        internal Label Label1;
        internal SplitContainer SplitContainer1;
        internal UI.WP.Button Button6;
        internal FontDialog FontDialog1;
        internal OpenFileDialog OpenJSONDlg;
        internal SaveFileDialog SaveJSONDlg;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button2;
        internal Label Label5;
        internal UI.WP.GroupBox GroupBox2;
        internal ProgressBar ProgressBar2;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal UI.WP.TextBox TextBox3;
        internal Label Label22;
        internal Label Label15;
        internal PictureBox PictureBox26;
        internal PictureBox PictureBox23;
        internal Label Label18;
        internal Label Label13;
        internal PictureBox PictureBox24;
        internal PictureBox PictureBox22;
        internal Label Label20;
        internal Label Label10;
        internal PictureBox PictureBox25;
        internal PictureBox PictureBox21;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.RadioButton RadioButton1;
        internal UI.WP.TextBox TextBox7;
        internal UI.WP.TextBox TextBox6;
        internal UI.WP.TextBox TextBox5;
        internal UI.WP.TextBox TextBox4;
        internal Label Label9;
        internal Label Label8;
        internal PictureBox PictureBox4;
        internal UI.WP.Button Button9;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button11;
        internal DataGridView data;
        internal UI.WP.GroupBox GroupBox5;
        internal UI.WP.GroupBox GroupBox6;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.AlertBox AlertBox3;
        internal ProgressBar ProgressBar1;
        internal UI.WP.TextBox TextBox8;
        internal UI.WP.Button Button12;
        internal UI.WP.CheckBox CheckBox1;
        internal UI.WP.CheckBox CheckBox2;
        internal Label Label6;
        internal UI.WP.Button Button13;
        internal UI.WP.TextBox TextBox9;
        internal UI.WP.GroupBox GroupBox7;
        internal UI.WP.GroupBox GroupBox8;
        internal UI.WP.GroupBox GroupBox9;
        internal ToolTip ToolTip;
        internal UI.WP.Button Button14;
    }
}