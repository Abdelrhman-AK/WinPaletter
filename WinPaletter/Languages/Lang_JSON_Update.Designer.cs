using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_JSON_Update : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_JSON_Update));
            PictureBox1 = new PictureBox();
            Label1 = new Label();
            PictureBox2 = new PictureBox();
            Label2 = new Label();
            TextBox1 = new UI.WP.TextBox();
            TextBox2 = new UI.WP.TextBox();
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            AlertBox7 = new UI.WP.AlertBox();
            Separator1 = new UI.WP.SeparatorH();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            SaveJSONDlg = new SaveFileDialog();
            OpenJSONDlg = new OpenFileDialog();
            AlertBox1 = new UI.WP.AlertBox();
            CheckBox1 = new UI.WP.CheckBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            SuspendLayout();
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(12, 12);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 13;
            PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(42, 12);
            Label1.Name = "Label1";
            Label1.Size = new Size(106, 24);
            Label1.TabIndex = 12;
            Label1.Text = "Old JSON File:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(12, 42);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 15;
            PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(42, 42);
            Label2.Name = "Label2";
            Label2.Size = new Size(106, 24);
            Label2.TabIndex = 14;
            Label2.Text = "New JSON File:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(154, 12);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(515, 24);
            TextBox1.TabIndex = 16;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // TextBox2
            // 
            TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(154, 42);
            TextBox2.MaxLength = 32767;
            TextBox2.Multiline = false;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = false;
            TextBox2.Scrollbars = ScrollBars.None;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(352, 24);
            TextBox2.TabIndex = 17;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button8.BackColor = Color.FromArgb(34, 34, 34);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.LineColor = Color.FromArgb(164, 125, 25);
            Button8.Location = new Point(675, 12);
            Button8.Name = "Button8";
            Button8.Size = new Size(40, 24);
            Button8.TabIndex = 111;
            Button8.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.LineColor = Color.FromArgb(164, 125, 25);
            Button1.Location = new Point(675, 42);
            Button1.Name = "Button1";
            Button1.Size = new Size(40, 24);
            Button1.TabIndex = 112;
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button5.BackColor = Color.FromArgb(34, 34, 34);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.ImageAlign = ContentAlignment.MiddleRight;
            Button5.LineColor = Color.FromArgb(108, 138, 121);
            Button5.Location = new Point(512, 42);
            Button5.Name = "Button5";
            Button5.Size = new Size(157, 24);
            Button5.TabIndex = 113;
            Button5.Text = "Generate new (English)";
            Button5.UseVisualStyleBackColor = false;
            // 
            // AlertBox7
            // 
            AlertBox7.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox7.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox7.CenterText = false;
            AlertBox7.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox7.Font = new Font("Segoe UI", 9.0f);
            AlertBox7.Image = null;
            AlertBox7.Location = new Point(12, 138);
            AlertBox7.Name = "AlertBox7";
            AlertBox7.Size = new Size(703, 40);
            AlertBox7.TabIndex = 114;
            AlertBox7.TabStop = false;
            AlertBox7.Text = resources.GetString("AlertBox7.Text");
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 72);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(703, 1);
            Separator1.TabIndex = 119;
            Separator1.TabStop = false;
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
            Button7.Location = new Point(534, 241);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 203;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.ImageAlign = ContentAlignment.MiddleLeft;
            Button3.LineColor = Color.FromArgb(69, 110, 129);
            Button3.Location = new Point(620, 241);
            Button3.Name = "Button3";
            Button3.Size = new Size(95, 34);
            Button3.TabIndex = 202;
            Button3.Text = "Save as ...";
            Button3.UseVisualStyleBackColor = false;
            // 
            // SaveJSONDlg
            // 
            SaveJSONDlg.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*";
            // 
            // OpenJSONDlg
            // 
            OpenJSONDlg.Filter = "JSON File (*.json)|*.json";
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox1.CenterText = false;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(12, 110);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(703, 22);
            AlertBox1.TabIndex = 204;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "This feature is experimental, always create backups before starting. If any abnor" + "mal results happened, post an issue in GitHub";
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = false;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(12, 79);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(703, 23);
            CheckBox1.TabIndex = 205;
            CheckBox1.Text = "Exclude global strings not found in the new file (Not recommended in backward com" + "ptability)";
            // 
            // Lang_JSON_Update
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(727, 287);
            Controls.Add(CheckBox1);
            Controls.Add(AlertBox1);
            Controls.Add(Button7);
            Controls.Add(Button3);
            Controls.Add(Separator1);
            Controls.Add(AlertBox7);
            Controls.Add(Button5);
            Controls.Add(Button1);
            Controls.Add(Button8);
            Controls.Add(TextBox2);
            Controls.Add(TextBox1);
            Controls.Add(PictureBox2);
            Controls.Add(Label2);
            Controls.Add(PictureBox1);
            Controls.Add(Label1);
            Font = new Font("Segoe UI Historic", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Lang_JSON_Update";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Update language JSON file";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            Load += new EventHandler(Lang_JSON_Update_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal PictureBox PictureBox1;
        internal Label Label1;
        internal PictureBox PictureBox2;
        internal Label Label2;
        internal UI.WP.TextBox TextBox1;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.Button Button8;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button5;
        internal UI.WP.AlertBox AlertBox7;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button3;
        internal SaveFileDialog SaveJSONDlg;
        internal OpenFileDialog OpenJSONDlg;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.CheckBox CheckBox1;
    }
}