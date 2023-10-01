using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Saving_ex_list : Form
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
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            TreeView1 = new TreeView();
            SuspendLayout();
            // 
            // Button12
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = null;
            Button1.LineColor = Color.FromArgb(210, 20, 50);
            Button1.Location = new Point(485, 375);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 30);
            Button1.TabIndex = 1;
            Button1.Text = "Close";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(0, 81, 210);
            Button2.Location = new Point(189, 375);
            Button2.Name = "Button2";
            Button2.Size = new Size(290, 30);
            Button2.TabIndex = 2;
            Button2.Text = "Elicit selected error (Show exception error details)";
            Button2.UseVisualStyleBackColor = false;
            // 
            // TreeView1
            // 
            TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TreeView1.BackColor = Color.FromArgb(35, 35, 35);
            TreeView1.BorderStyle = BorderStyle.None;
            TreeView1.ForeColor = Color.White;
            TreeView1.FullRowSelect = true;
            TreeView1.Indent = 17;
            TreeView1.ItemHeight = 28;
            TreeView1.Location = new Point(12, 12);
            TreeView1.Name = "TreeView1";
            TreeView1.ShowLines = false;
            TreeView1.Size = new Size(553, 354);
            TreeView1.TabIndex = 6;
            // 
            // Saving_ex_list
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(577, 417);
            Controls.Add(TreeView1);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Saving_ex_list";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Errors";
            TopMost = true;
            Load += new EventHandler(Saving_exceptions_list_Load);
            ResumeLayout(false);

        }
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal TreeView TreeView1;
    }
}