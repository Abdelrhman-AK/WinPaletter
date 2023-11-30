using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Lang_Dashboard : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Lang_Dashboard));
            GroupBox1 = new UI.WP.GroupBox();
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Label2 = new Label();
            Label1 = new Label();
            PictureBox1 = new PictureBox();
            GroupBox2 = new UI.WP.GroupBox();
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Label3 = new Label();
            Label4 = new Label();
            PictureBox2 = new PictureBox();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            GroupBox3 = new UI.WP.GroupBox();
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Label5 = new Label();
            Label6 = new Label();
            PictureBox3 = new PictureBox();
            Separator1 = new UI.WP.SeparatorH();
            AlertBox1 = new UI.WP.AlertBox();
            AlertBox2 = new UI.WP.AlertBox();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(Button1);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(PictureBox1);
            GroupBox1.Location = new Point(12, 89);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(610, 64);
            GroupBox1.TabIndex = 0;
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(43, 43, 43);

            Button1.Font = new("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.Location = new Point(521, 20);
            Button1.Name = "Button1";
            Button1.Size = new Size(75, 24);
            Button1.TabIndex = 4;
            Button1.Text = "Go";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(75, 25);
            Label2.Name = "Label2";
            Label2.Size = new Size(440, 35);
            Label2.TabIndex = 3;
            Label2.Text = "This will help you serialize JSON language file into simple tree nodes, making it" + " easier to modify language files (If you can't use a text editor to modify JSON " + "files)";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.Font = new("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(65, 5);
            Label1.Name = "Label1";
            Label1.Size = new Size(413, 19);
            Label1.TabIndex = 2;
            Label1.Text = @"Create\Modify language files";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(58, 58);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 1;
            PictureBox1.TabStop = false;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(Button2);
            GroupBox2.Controls.Add(Label3);
            GroupBox2.Controls.Add(Label4);
            GroupBox2.Controls.Add(PictureBox2);
            GroupBox2.Location = new Point(12, 159);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(610, 64);
            GroupBox2.TabIndex = 1;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(43, 43, 43);

            Button2.Font = new("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.Location = new Point(521, 20);
            Button2.Name = "Button2";
            Button2.Size = new Size(75, 24);
            Button2.TabIndex = 4;
            Button2.Text = "Go";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(75, 25);
            Label3.Name = "Label3";
            Label3.Size = new Size(440, 35);
            Label3.TabIndex = 3;
            Label3.Text = "It will be helpful if a new version of WinPaletter has been released and you want" + " to update the JSON file with the new text entries included in the new version";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.Font = new("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4.Location = new Point(65, 5);
            Label4.Name = "Label4";
            Label4.Size = new Size(413, 19);
            Label4.TabIndex = 2;
            Label4.Text = "Update JSON language file";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(3, 3);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(58, 58);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 1;
            PictureBox2.TabStop = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);

            Button7.Font = new("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = null;
            Button7.Location = new Point(542, 305);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 34);
            Button7.TabIndex = 202;
            Button7.Text = "Cancel";
            Button7.UseVisualStyleBackColor = false;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox3.Controls.Add(AlertBox1);
            GroupBox3.Controls.Add(Button3);
            GroupBox3.Controls.Add(Label5);
            GroupBox3.Controls.Add(Label6);
            GroupBox3.Controls.Add(PictureBox3);
            GroupBox3.Location = new Point(12, 12);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(610, 64);
            GroupBox3.TabIndex = 203;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button3.BackColor = Color.FromArgb(43, 43, 43);

            Button3.Font = new("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = null;
            Button3.Location = new Point(521, 20);
            Button3.Name = "Button3";
            Button3.Size = new Size(75, 24);
            Button3.TabIndex = 4;
            Button3.Text = "Go";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5.Location = new Point(75, 25);
            Label5.Name = "Label5";
            Label5.Size = new Size(440, 35);
            Label5.TabIndex = 3;
            Label5.Text = "This will help you create, modify and update languages JSON files by showing mini" + "-forms that you can edit so that you can see all text items in real time";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label6.Font = new("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(65, 5);
            Label6.Name = "Label6";
            Label6.Size = new Size(230, 19);
            Label6.TabIndex = 2;
            Label6.Text = "GUI language editor (experimental)";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(3, 3);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(58, 58);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 1;
            PictureBox3.TabStop = false;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(12, 82);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(610, 1);
            Separator1.TabIndex = 204;
            Separator1.TabStop = false;
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Success;
            AlertBox1.BackColor = Color.FromArgb(60, 85, 79);
            AlertBox1.CenterText = true;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(302, 5);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(46, 19);
            AlertBox1.TabIndex = 205;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "NEW";
            // 
            // AlertBox2
            // 
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox2.BackColor = Color.FromArgb(125, 20, 30);
            AlertBox2.CenterText = false;
            AlertBox2.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox2.Font = new("Segoe UI", 9.0f);
            AlertBox2.Image = null;
            AlertBox2.Location = new Point(12, 229);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(610, 40);
            AlertBox2.TabIndex = 206;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "The last two features might be removed in the future if GUI language editor perfo" + "rmed better after its development";
            // 
            // Lang_Dashboard
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(634, 351);
            Controls.Add(AlertBox2);
            Controls.Add(Separator1);
            Controls.Add(GroupBox3);
            Controls.Add(Button7);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Margin = new(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Lang_Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Language dashboard";
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            Load += new EventHandler(Lang_Dashboard_Load);
            HelpButtonClicked += new System.ComponentModel.CancelEventHandler(Form_HelpButtonClicked);
            ResumeLayout(false);

        }

        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.Button Button1;
        internal Label Label2;
        internal Label Label1;
        internal PictureBox PictureBox1;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.Button Button2;
        internal Label Label3;
        internal Label Label4;
        internal PictureBox PictureBox2;
        internal UI.WP.Button Button7;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.AlertBox AlertBox1;
        internal UI.WP.Button Button3;
        internal Label Label5;
        internal Label Label6;
        internal PictureBox PictureBox3;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.AlertBox AlertBox2;
    }
}
