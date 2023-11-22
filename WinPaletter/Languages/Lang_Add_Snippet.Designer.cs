using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_Add_Snippet : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_Add_Snippet));
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            ComboBox2 = new UI.WP.ComboBox();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            ComboBox1 = new UI.WP.ComboBox();
            PictureBox2 = new PictureBox();
            Label2 = new Label();
            Label1 = new Label();
            PictureBox1 = new PictureBox();
            AlertBox7 = new UI.WP.AlertBox();
            Label3 = new Label();
            Label4 = new Label();
            AlertBox1 = new UI.WP.AlertBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.Location = new Point(416, 83);
            Button3.Name = "Button3";
            Button3.Size = new Size(75, 23);
            Button3.TabIndex = 4;
            Button3.Text = "Add layout";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.Location = new Point(335, 83);
            Button2.Name = "Button2";
            Button2.Size = new Size(75, 23);
            Button2.TabIndex = 3;
            Button2.Text = "Add";
            Button2.UseVisualStyleBackColor = false;
            // 
            // ComboBox2
            // 
            ComboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox2.BackColor = Color.FromArgb(55, 55, 55);
            ComboBox2.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox2.DropDownHeight = 250;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox2.Font = new Font("Segoe UI", 9.0f);
            ComboBox2.ForeColor = Color.White;
            ComboBox2.FormattingEnabled = true;
            ComboBox2.IntegralHeight = false;
            ComboBox2.ItemHeight = 20;
            ComboBox2.Location = new Point(96, 82);
            ComboBox2.Name = "ComboBox2";
            ComboBox2.Size = new Size(230, 26);
            ComboBox2.TabIndex = 2;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.Location = new Point(416, 13);
            Button1.Name = "Button1";
            Button1.Size = new Size(75, 23);
            Button1.TabIndex = 1;
            Button1.Text = "Add";
            Button1.UseVisualStyleBackColor = false;
            // 
            // ComboBox1
            // 
            ComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox1.BackColor = Color.FromArgb(55, 55, 55);
            ComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox1.DropDownHeight = 250;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox1.Font = new Font("Segoe UI", 9.0f);
            ComboBox1.ForeColor = Color.White;
            ComboBox1.FormattingEnabled = true;
            ComboBox1.IntegralHeight = false;
            ComboBox1.ItemHeight = 20;
            ComboBox1.Location = new Point(96, 12);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(314, 26);
            ComboBox1.TabIndex = 0;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(12, 12);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 13;
            PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            Label2.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(42, 12);
            Label2.Name = "Label2";
            Label2.Size = new Size(48, 24);
            Label2.TabIndex = 14;
            Label2.Text = "Name:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(42, 82);
            Label1.Name = "Label1";
            Label1.Size = new Size(48, 24);
            Label1.TabIndex = 16;
            Label1.Text = "Code:";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(12, 82);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(24, 24);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 15;
            PictureBox1.TabStop = false;
            // 
            // AlertBox7
            // 
            AlertBox7.AlertStyle = UI.WP.AlertBox.Style.Notice;
            AlertBox7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox7.BackColor = Color.FromArgb(70, 91, 94);
            AlertBox7.CenterText = true;
            AlertBox7.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox7.Font = new Font("Segoe UI", 9.0f);
            AlertBox7.Image = null;
            AlertBox7.Location = new Point(12, 182);
            AlertBox7.Name = "AlertBox7";
            AlertBox7.Size = new Size(479, 22);
            AlertBox7.TabIndex = 28;
            AlertBox7.TabStop = false;
            AlertBox7.Text = "These are just snippets to help you input correct data";
            // 
            // Label3
            // 
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Italic, GraphicsUnit.Point, 0);
            Label3.Location = new Point(45, 41);
            Label3.Name = "Label3";
            Label3.Size = new Size(446, 24);
            Label3.TabIndex = 30;
            Label3.Text = @"Its correct place is (Information\lang)";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Italic, GraphicsUnit.Point, 0);
            Label4.Location = new Point(45, 113);
            Label4.Name = "Label4";
            Label4.Size = new Size(446, 56);
            Label4.TabIndex = 31;
            Label4.Text = @"Codes's correct place is (Information\langcode)" + '\r' + '\n' + "Add layout's correct place is (I" + @"nformation\righttoleft)" + '\r' + '\n' + "Add layout will insert \"True\" if language code layout i" + "s right to left";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Notice;
            AlertBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox1.BackColor = Color.FromArgb(70, 91, 94);
            AlertBox1.CenterText = true;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(12, 210);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(479, 22);
            AlertBox1.TabIndex = 29;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "It should be in its correct place before pressing \"Add\" or \"Add Layout\"";
            // 
            // Lang_Add_Snippet
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(503, 244);
            Controls.Add(Label4);
            Controls.Add(Label3);
            Controls.Add(AlertBox1);
            Controls.Add(AlertBox7);
            Controls.Add(Label1);
            Controls.Add(PictureBox1);
            Controls.Add(Label2);
            Controls.Add(PictureBox2);
            Controls.Add(Button3);
            Controls.Add(Button2);
            Controls.Add(ComboBox2);
            Controls.Add(Button1);
            Controls.Add(ComboBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Lang_Add_Snippet";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add language snippets";
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(Lang_Add_Snippet_Load);
            ResumeLayout(false);

        }

        internal UI.WP.ComboBox ComboBox1;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.ComboBox ComboBox2;
        internal UI.WP.Button Button3;
        internal PictureBox PictureBox2;
        internal Label Label2;
        internal Label Label1;
        internal PictureBox PictureBox1;
        internal UI.WP.AlertBox AlertBox7;
        internal Label Label3;
        internal Label Label4;
        internal UI.WP.AlertBox AlertBox1;
    }
}