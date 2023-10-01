using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_JSON_Manage : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_JSON_Manage));
            SaveJSONDlg = new SaveFileDialog();
            OpenJSONDlg = new OpenFileDialog();
            FontDialog1 = new FontDialog();
            Label5 = new Label();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            GroupBox3 = new UI.WP.GroupBox();
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            GroupBox1 = new UI.WP.GroupBox();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click_1);
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            PictureBox3 = new PictureBox();
            TextBox3 = new UI.WP.TextBox();
            Label3 = new Label();
            PictureBox2 = new PictureBox();
            PictureBox1 = new PictureBox();
            SeparatorVertical1 = new UI.WP.SeparatorV();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            TextBox2 = new UI.WP.TextBox();
            GroupBox2 = new UI.WP.GroupBox();
            Label6 = new Label();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            TextBox1 = new UI.WP.TextBox();
            Label4 = new Label();
            Label2 = new Label();
            Label1 = new Label();
            TreeView1 = new TreeView();
            TreeView1.BeforeLabelEdit += new NodeLabelEditEventHandler(TreeView1_BeforeLabelEdit);
            TreeView1.AfterLabelEdit += new NodeLabelEditEventHandler(TreeView1_AfterLabelEdit);
            TreeView1.AfterSelect += new TreeViewEventHandler(TreeView1_AfterSelect);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            GroupBox3.SuspendLayout();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            GroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // SaveJSONDlg
            // 
            SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*";
            // 
            // OpenJSONDlg
            // 
            OpenJSONDlg.Filter = "JSON File (*.json)|*.json";
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Label5.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(13, 579);
            Label5.Name = "Label5";
            Label5.Size = new Size(429, 29);
            Label5.TabIndex = 202;
            Label5.Text = "Numbers in curly brackets should be left unchanged, for example: {0}";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
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
            Button7.Location = new Point(578, 576);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 201;
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
            Button2.Location = new Point(750, 576);
            Button2.Name = "Button2";
            Button2.Size = new Size(95, 34);
            Button2.TabIndex = 200;
            Button2.Text = "Save as ...";
            Button2.UseVisualStyleBackColor = false;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(Button6);
            GroupBox3.Controls.Add(Button5);
            GroupBox3.Controls.Add(Button8);
            GroupBox3.Controls.Add(Button4);
            GroupBox3.Location = new Point(12, 13);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(833, 41);
            GroupBox3.TabIndex = 199;
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
            Button6.TabIndex = 113;
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
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(Button11);
            GroupBox1.Controls.Add(Button10);
            GroupBox1.Controls.Add(Button9);
            GroupBox1.Controls.Add(PictureBox3);
            GroupBox1.Controls.Add(TextBox3);
            GroupBox1.Controls.Add(Label3);
            GroupBox1.Controls.Add(PictureBox2);
            GroupBox1.Controls.Add(PictureBox1);
            GroupBox1.Controls.Add(SeparatorVertical1);
            GroupBox1.Controls.Add(Button3);
            GroupBox1.Controls.Add(TextBox2);
            GroupBox1.Controls.Add(GroupBox2);
            GroupBox1.Controls.Add(Button1);
            GroupBox1.Controls.Add(TextBox1);
            GroupBox1.Controls.Add(Label4);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(TreeView1);
            GroupBox1.Location = new Point(12, 60);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(833, 504);
            GroupBox1.TabIndex = 7;
            // 
            // Button11
            // 
            Button11.BackColor = Color.FromArgb(43, 43, 43);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = null;
            Button11.LineColor = Color.FromArgb(0, 81, 210);
            Button11.Location = new Point(319, 40);
            Button11.Name = "Button11";
            Button11.Size = new Size(73, 23);
            Button11.TabIndex = 21;
            Button11.Text = "Collapse all";
            Button11.UseVisualStyleBackColor = false;
            // 
            // Button10
            // 
            Button10.BackColor = Color.FromArgb(43, 43, 43);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = null;
            Button10.LineColor = Color.FromArgb(0, 81, 210);
            Button10.Location = new Point(247, 40);
            Button10.Name = "Button10";
            Button10.Size = new Size(66, 23);
            Button10.TabIndex = 20;
            Button10.Text = "Expand all";
            Button10.UseVisualStyleBackColor = false;
            // 
            // Button9
            // 
            Button9.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button9.BackColor = Color.FromArgb(43, 43, 43);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.ImageAlign = ContentAlignment.MiddleRight;
            Button9.LineColor = Color.FromArgb(32, 79, 131);
            Button9.Location = new Point(567, 466);
            Button9.Name = "Button9";
            Button9.Size = new Size(134, 28);
            Button9.TabIndex = 19;
            Button9.Text = "Language snippets";
            Button9.UseVisualStyleBackColor = false;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(406, 99);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 15;
            PictureBox3.TabStop = false;
            // 
            // TextBox3
            // 
            TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox3.BackColor = Color.FromArgb(55, 55, 55);
            TextBox3.DrawOnGlass = false;
            TextBox3.ForeColor = Color.White;
            TextBox3.Location = new Point(439, 129);
            TextBox3.MaxLength = 32767;
            TextBox3.Multiline = true;
            TextBox3.Name = "TextBox3";
            TextBox3.ReadOnly = true;
            TextBox3.Scrollbars = ScrollBars.Vertical;
            TextBox3.SelectedText = "";
            TextBox3.SelectionLength = 0;
            TextBox3.SelectionStart = 0;
            TextBox3.Size = new Size(387, 119);
            TextBox3.TabIndex = 14;
            TextBox3.TextAlign = HorizontalAlignment.Left;
            TextBox3.UseSystemPasswordChar = false;
            TextBox3.WordWrap = true;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(436, 99);
            Label3.Name = "Label3";
            Label3.Size = new Size(390, 24);
            Label3.TabIndex = 13;
            Label3.Text = "Old value:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(406, 258);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 12;
            PictureBox2.TabStop = false;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(406, 40);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 11;
            PictureBox1.TabStop = false;
            // 
            // SeparatorVertical1
            // 
            SeparatorVertical1.AlternativeLook = false;
            SeparatorVertical1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            SeparatorVertical1.Location = new Point(398, 38);
            SeparatorVertical1.Name = "SeparatorVertical1";
            SeparatorVertical1.Size = new Size(1, 456);
            SeparatorVertical1.TabIndex = 10;
            SeparatorVertical1.TabStop = false;
            SeparatorVertical1.Text = "SeparatorVertical1";
            // 
            // Button3
            // 
            Button3.BackColor = Color.FromArgb(43, 43, 43);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.LineColor = Color.FromArgb(59, 111, 122);
            Button3.Location = new Point(209, 40);
            Button3.Name = "Button3";
            Button3.Size = new Size(32, 23);
            Button3.TabIndex = 9;
            Button3.UseVisualStyleBackColor = false;
            // 
            // TextBox2
            // 
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(7, 40);
            TextBox2.MaxLength = 32767;
            TextBox2.Multiline = true;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = false;
            TextBox2.Scrollbars = ScrollBars.None;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(196, 23);
            TextBox2.TabIndex = 8;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox2.Controls.Add(Label6);
            GroupBox2.Location = new Point(7, 8);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Padding = new Padding(2);
            GroupBox2.Size = new Size(819, 24);
            GroupBox2.TabIndex = 7;
            // 
            // Label6
            // 
            Label6.Dock = DockStyle.Fill;
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.Location = new Point(2, 2);
            Label6.Name = "Label6";
            Label6.Size = new Size(815, 20);
            Label6.TabIndex = 114;
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button14
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(43, 43, 43);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleRight;
            Button1.LineColor = Color.FromArgb(15, 84, 128);
            Button1.Location = new Point(707, 466);
            Button1.Name = "Button1";
            Button1.Size = new Size(119, 28);
            Button1.TabIndex = 4;
            Button1.Text = "Submit change";
            Button1.UseVisualStyleBackColor = false;
            // 
            // TextBox2
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(439, 288);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = true;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.Vertical;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(387, 172);
            TextBox1.TabIndex = 3;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(436, 64);
            Label4.Name = "Label4";
            Label4.Size = new Size(390, 24);
            Label4.TabIndex = 2;
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(436, 258);
            Label2.Name = "Label2";
            Label2.Size = new Size(390, 24);
            Label2.TabIndex = 1;
            Label2.Text = "New value:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(436, 40);
            Label1.Name = "Label1";
            Label1.Size = new Size(390, 24);
            Label1.TabIndex = 0;
            Label1.Text = "Variable:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TreeView1
            // 
            TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            TreeView1.BackColor = Color.FromArgb(35, 35, 35);
            TreeView1.BorderStyle = BorderStyle.None;
            TreeView1.ForeColor = Color.White;
            TreeView1.FullRowSelect = true;
            TreeView1.ItemHeight = 20;
            TreeView1.LabelEdit = true;
            TreeView1.Location = new Point(7, 69);
            TreeView1.Name = "TreeView1";
            TreeView1.ShowLines = false;
            TreeView1.Size = new Size(385, 425);
            TreeView1.TabIndex = 6;
            // 
            // Button12
            // 
            Button12.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button12.BackColor = Color.FromArgb(34, 34, 34);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = (Image)resources.GetObject("Button1.Image");
            Button12.ImageAlign = ContentAlignment.MiddleLeft;
            Button12.LineColor = Color.FromArgb(30, 107, 146);
            Button12.Location = new Point(664, 576);
            Button12.Name = "Button12";
            Button12.Size = new Size(80, 34);
            Button12.TabIndex = 212;
            Button12.Text = "Help";
            Button12.UseVisualStyleBackColor = false;
            // 
            // Lang_JSON_Manage
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(857, 622);
            Controls.Add(Button12);
            Controls.Add(Label5);
            Controls.Add(Button7);
            Controls.Add(Button2);
            Controls.Add(GroupBox3);
            Controls.Add(GroupBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Lang_JSON_Manage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Language editor";
            GroupBox3.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox2.ResumeLayout(false);
            Load += new EventHandler(LangJSON_Manage_Load);
            ResumeLayout(false);

        }

        internal TreeView TreeView1;
        internal UI.WP.GroupBox GroupBox1;
        internal Label Label4;
        internal Label Label2;
        internal Label Label1;
        internal UI.WP.Button Button1;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.GroupBox GroupBox3;
        internal Label Label6;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button2;
        internal SaveFileDialog SaveJSONDlg;
        internal OpenFileDialog OpenJSONDlg;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.Button Button3;
        internal UI.WP.SeparatorV SeparatorVertical1;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button5;
        internal PictureBox PictureBox2;
        internal PictureBox PictureBox1;
        internal PictureBox PictureBox3;
        internal UI.WP.TextBox TextBox3;
        internal Label Label3;
        internal UI.WP.Button Button9;
        internal UI.WP.Button Button6;
        internal FontDialog FontDialog1;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button10;
        internal Label Label5;
        internal UI.WP.Button Button12;
    }
}