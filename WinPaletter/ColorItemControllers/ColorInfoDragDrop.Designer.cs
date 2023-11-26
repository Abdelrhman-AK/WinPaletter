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
            this.Color_From = new System.Windows.Forms.Panel();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Color_To = new System.Windows.Forms.Panel();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Color_From.SuspendLayout();
            this.Color_To.SuspendLayout();
            this.SuspendLayout();
            // 
            // Color_From
            // 
            this.Color_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Color_From.Controls.Add(this.Label6);
            this.Color_From.Controls.Add(this.Label7);
            this.Color_From.Controls.Add(this.Label9);
            this.Color_From.Controls.Add(this.Label8);
            this.Color_From.Location = new System.Drawing.Point(60, 56);
            this.Color_From.Name = "Color_From";
            this.Color_From.Size = new System.Drawing.Size(109, 84);
            this.Color_From.TabIndex = 33;
            this.Color_From.BackColorChanged += new System.EventHandler(this.Color_From_BackColorChanged);
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(1, 2);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(105, 14);
            this.Label6.TabIndex = 23;
            this.Label6.Text = "0";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(1, 23);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(105, 14);
            this.Label7.TabIndex = 24;
            this.Label7.Text = "0";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(1, 65);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(105, 14);
            this.Label9.TabIndex = 26;
            this.Label9.Text = "0";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(1, 44);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(105, 14);
            this.Label8.TabIndex = 25;
            this.Label8.Text = "0";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(1, 119);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(55, 21);
            this.Label17.TabIndex = 30;
            this.Label17.Text = "Decimal";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label19
            // 
            this.Label19.BackColor = System.Drawing.Color.Transparent;
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(2, 58);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(55, 21);
            this.Label19.TabIndex = 27;
            this.Label19.Text = "RGB";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.Transparent;
            this.Label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(169, 31);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(109, 22);
            this.Label15.TabIndex = 32;
            this.Label15.Text = "To";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(2, 78);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(55, 21);
            this.Label20.TabIndex = 28;
            this.Label20.Text = "HEX";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(1, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(275, 30);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "0";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(2, 99);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(55, 21);
            this.Label18.TabIndex = 29;
            this.Label18.Text = "HSL";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(60, 31);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(109, 22);
            this.Label14.TabIndex = 32;
            this.Label14.Text = "From";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Color_To
            // 
            this.Color_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Color_To.Controls.Add(this.Label10);
            this.Color_To.Controls.Add(this.Label13);
            this.Color_To.Controls.Add(this.Label11);
            this.Color_To.Controls.Add(this.Label12);
            this.Color_To.Location = new System.Drawing.Point(168, 56);
            this.Color_To.Name = "Color_To";
            this.Color_To.Size = new System.Drawing.Size(109, 84);
            this.Color_To.TabIndex = 34;
            this.Color_To.BackColorChanged += new System.EventHandler(this.Color_To_BackColorChanged);
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.Transparent;
            this.Label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(1, 65);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(105, 14);
            this.Label10.TabIndex = 30;
            this.Label10.Text = "0";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.Transparent;
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(1, 2);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(105, 14);
            this.Label13.TabIndex = 27;
            this.Label13.Text = "0";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(1, 44);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(105, 14);
            this.Label11.TabIndex = 29;
            this.Label11.Text = "0";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(1, 23);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(105, 14);
            this.Label12.TabIndex = 28;
            this.Label12.Text = "0";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ColorInfoDragDrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(277, 140);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Color_From);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.Color_To);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.Label20);
            this.Controls.Add(this.Label18);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ColorInfoDragDrop";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.ColorInfoDragDrop_Load);
            this.Color_From.ResumeLayout(false);
            this.Color_To.ResumeLayout(false);
            this.ResumeLayout(false);

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
