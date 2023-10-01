using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ColorInfoDragDrop : Form
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
            Color_From = new Panel();
            Color_From.BackColorChanged += new EventHandler(Color_From_BackColorChanged);
            Label6 = new Label();
            Label7 = new Label();
            Label9 = new Label();
            Label8 = new Label();
            Label17 = new Label();
            Label19 = new Label();
            Label15 = new Label();
            Label20 = new Label();
            Label1 = new Label();
            Label18 = new Label();
            Label14 = new Label();
            Color_To = new Panel();
            Color_To.BackColorChanged += new EventHandler(Color_To_BackColorChanged);
            Label10 = new Label();
            Label13 = new Label();
            Label11 = new Label();
            Label12 = new Label();
            Color_From.SuspendLayout();
            Color_To.SuspendLayout();
            SuspendLayout();
            // 
            // Color_From
            // 
            Color_From.BorderStyle = BorderStyle.FixedSingle;
            Color_From.Controls.Add(Label6);
            Color_From.Controls.Add(Label7);
            Color_From.Controls.Add(Label9);
            Color_From.Controls.Add(Label8);
            Color_From.Location = new Point(60, 56);
            Color_From.Name = "Color_From";
            Color_From.Size = new Size(109, 84);
            Color_From.TabIndex = 33;
            // 
            // Label6
            // 
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.Location = new Point(1, 2);
            Label6.Name = "Label6";
            Label6.Size = new Size(105, 14);
            Label6.TabIndex = 23;
            Label6.Text = "0";
            Label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label7
            // 
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.Location = new Point(1, 23);
            Label7.Name = "Label7";
            Label7.Size = new Size(105, 14);
            Label7.TabIndex = 24;
            Label7.Text = "0";
            Label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label9
            // 
            Label9.BackColor = Color.Transparent;
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label9.Location = new Point(1, 65);
            Label9.Name = "Label9";
            Label9.Size = new Size(105, 14);
            Label9.TabIndex = 26;
            Label9.Text = "0";
            Label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label8
            // 
            Label8.BackColor = Color.Transparent;
            Label8.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8.Location = new Point(1, 44);
            Label8.Name = "Label8";
            Label8.Size = new Size(105, 14);
            Label8.TabIndex = 25;
            Label8.Text = "0";
            Label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label17
            // 
            Label17.BackColor = Color.Transparent;
            Label17.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label17.Location = new Point(1, 119);
            Label17.Name = "Label17";
            Label17.Size = new Size(55, 21);
            Label17.TabIndex = 30;
            Label17.Text = "Decimal";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label19
            // 
            Label19.BackColor = Color.Transparent;
            Label19.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label19.Location = new Point(2, 58);
            Label19.Name = "Label19";
            Label19.Size = new Size(55, 21);
            Label19.TabIndex = 27;
            Label19.Text = "RGB";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            Label15.BackColor = Color.Transparent;
            Label15.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label15.Location = new Point(169, 31);
            Label15.Name = "Label15";
            Label15.Size = new Size(109, 22);
            Label15.TabIndex = 32;
            Label15.Text = "To";
            Label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label20
            // 
            Label20.BackColor = Color.Transparent;
            Label20.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label20.Location = new Point(2, 78);
            Label20.Name = "Label20";
            Label20.Size = new Size(55, 21);
            Label20.TabIndex = 28;
            Label20.Text = "HEX";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            Label1.BackColor = Color.FromArgb(150, 25, 25, 25);
            Label1.Dock = DockStyle.Top;
            Label1.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(1, 1);
            Label1.Name = "Label1";
            Label1.Size = new Size(275, 30);
            Label1.TabIndex = 18;
            Label1.Text = "0";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label18
            // 
            Label18.BackColor = Color.Transparent;
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label18.Location = new Point(2, 99);
            Label18.Name = "Label18";
            Label18.Size = new Size(55, 21);
            Label18.TabIndex = 29;
            Label18.Text = "HSL";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label14
            // 
            Label14.BackColor = Color.Transparent;
            Label14.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label14.Location = new Point(60, 31);
            Label14.Name = "Label14";
            Label14.Size = new Size(109, 22);
            Label14.TabIndex = 32;
            Label14.Text = "From";
            Label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Color_To
            // 
            Color_To.BorderStyle = BorderStyle.FixedSingle;
            Color_To.Controls.Add(Label10);
            Color_To.Controls.Add(Label13);
            Color_To.Controls.Add(Label11);
            Color_To.Controls.Add(Label12);
            Color_To.Location = new Point(168, 56);
            Color_To.Name = "Color_To";
            Color_To.Size = new Size(109, 84);
            Color_To.TabIndex = 34;
            // 
            // Label10
            // 
            Label10.BackColor = Color.Transparent;
            Label10.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label10.Location = new Point(1, 65);
            Label10.Name = "Label10";
            Label10.Size = new Size(105, 14);
            Label10.TabIndex = 30;
            Label10.Text = "0";
            Label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label13
            // 
            Label13.BackColor = Color.Transparent;
            Label13.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label13.Location = new Point(1, 2);
            Label13.Name = "Label13";
            Label13.Size = new Size(105, 14);
            Label13.TabIndex = 27;
            Label13.Text = "0";
            Label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label11
            // 
            Label11.BackColor = Color.Transparent;
            Label11.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label11.Location = new Point(1, 44);
            Label11.Name = "Label11";
            Label11.Size = new Size(105, 14);
            Label11.TabIndex = 29;
            Label11.Text = "0";
            Label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label12
            // 
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label12.Location = new Point(1, 23);
            Label12.Name = "Label12";
            Label12.Size = new Size(105, 14);
            Label12.TabIndex = 28;
            Label12.Text = "0";
            Label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ColorInfoDragDrop
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(277, 140);
            Controls.Add(Label1);
            Controls.Add(Color_From);
            Controls.Add(Label17);
            Controls.Add(Label19);
            Controls.Add(Color_To);
            Controls.Add(Label15);
            Controls.Add(Label14);
            Controls.Add(Label20);
            Controls.Add(Label18);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "ColorInfoDragDrop";
            Padding = new Padding(1);
            StartPosition = FormStartPosition.Manual;
            Color_From.ResumeLayout(false);
            Color_To.ResumeLayout(false);
            Load += new EventHandler(ColorInfoDragDrop_Load);
            ResumeLayout(false);

        }
        internal Label Label1;
        internal Label Label6;
        internal Label Label7;
        internal Label Label8;
        internal Label Label9;
        internal Label Label10;
        internal Label Label11;
        internal Label Label12;
        internal Label Label13;
        internal Label Label14;
        internal Label Label15;
        internal Label Label17;
        internal Label Label18;
        internal Label Label19;
        internal Label Label20;
        internal Panel Color_To;
        internal Panel Color_From;
    }
}