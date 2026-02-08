using System.Windows.Forms;

namespace WinPaletter
{
    partial class GitHub_FilesCompare : UI.WP.Form
    {
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private UI.WP.ListView listViewSource;
        private UI.WP.ListView listViewDestination;
        private System.Windows.Forms.ColumnHeader colSourceName;
        private System.Windows.Forms.ColumnHeader colSourceSize;
        private System.Windows.Forms.ColumnHeader colDestName;
        private System.Windows.Forms.ColumnHeader colDestSize;

        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.listViewSource = new WinPaletter.UI.WP.ListView();
            this.colSourceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSourceSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewDestination = new WinPaletter.UI.WP.ListView();
            this.colDestName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDestSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bottom_buttons = new WinPaletter.UI.WP.GroupBox();
            this.button2 = new WinPaletter.UI.WP.Button();
            this.button1 = new WinPaletter.UI.WP.Button();
            this.groupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.radioButton2 = new WinPaletter.UI.WP.RadioButton();
            this.radioButton1 = new WinPaletter.UI.WP.RadioButton();
            this.tableLayoutPanel.SuspendLayout();
            this.bottom_buttons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.listViewSource, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.listViewDestination, 1, 1);
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(726, 284);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // listViewSource
            // 
            this.listViewSource.CheckBoxes = true;
            this.listViewSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSourceName,
            this.colSourceSize});
            this.listViewSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSource.FullRowSelect = true;
            this.listViewSource.HideSelection = false;
            this.listViewSource.Location = new System.Drawing.Point(3, 3);
            this.listViewSource.Name = "listViewSource";
            this.listViewSource.Size = new System.Drawing.Size(357, 278);
            this.listViewSource.TabIndex = 2;
            this.listViewSource.UseCompatibleStateImageBehavior = false;
            this.listViewSource.View = System.Windows.Forms.View.Details;
            this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewSource_ItemChecked);
            // 
            // colSourceName
            // 
            this.colSourceName.Text = "Name";
            this.colSourceName.Width = 273;
            // 
            // colSourceSize
            // 
            this.colSourceSize.Text = "Size";
            this.colSourceSize.Width = 80;
            // 
            // listViewDestination
            // 
            this.listViewDestination.CheckBoxes = true;
            this.listViewDestination.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDestName,
            this.colDestSize});
            this.listViewDestination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDestination.FullRowSelect = true;
            this.listViewDestination.HideSelection = false;
            this.listViewDestination.Location = new System.Drawing.Point(366, 3);
            this.listViewDestination.Name = "listViewDestination";
            this.listViewDestination.Size = new System.Drawing.Size(357, 278);
            this.listViewDestination.TabIndex = 3;
            this.listViewDestination.UseCompatibleStateImageBehavior = false;
            this.listViewDestination.View = System.Windows.Forms.View.Details;
            this.listViewDestination.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewDestination_ItemChecked);
            // 
            // colDestName
            // 
            this.colDestName.Text = "Name";
            this.colDestName.Width = 273;
            // 
            // colDestSize
            // 
            this.colDestSize.Text = "Size";
            this.colDestSize.Width = 80;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(732, 37);
            this.label2.TabIndex = 124;
            this.label2.Text = "Which files do you want to keep?";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(732, 22);
            this.label1.TabIndex = 125;
            this.label1.Text = "If you select both versions, the copied file will have a number added to its name" +
    ".";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bottom_buttons
            // 
            this.bottom_buttons.BackColor = System.Drawing.Color.Transparent;
            this.bottom_buttons.Controls.Add(this.button2);
            this.bottom_buttons.Controls.Add(this.button1);
            this.bottom_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_buttons.Location = new System.Drawing.Point(0, 403);
            this.bottom_buttons.Name = "bottom_buttons";
            this.bottom_buttons.Size = new System.Drawing.Size(756, 48);
            this.bottom_buttons.TabIndex = 123;
            this.bottom_buttons.UseDecorationPattern = false;
            this.bottom_buttons.UseSharpStyle = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(18)))), ((int)(((byte)(31)))));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Flag = WinPaletter.UI.WP.Button.Flags.ErrorOnHover;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = null;
            this.button2.ImageGlyph = null;
            this.button2.ImageGlyphEnabled = false;
            this.button2.Location = new System.Drawing.Point(635, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 34);
            this.button2.TabIndex = 18;
            this.button2.Text = "Cancel";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.button1.Flag = WinPaletter.UI.WP.Button.Flags.TintedOnHover;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = null;
            this.button1.ImageGlyph = null;
            this.button1.ImageGlyphEnabled = false;
            this.button1.Location = new System.Drawing.Point(514, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 34);
            this.button1.TabIndex = 17;
            this.button1.Text = "Continue";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.tableLayoutPanel);
            this.groupBox1.Location = new System.Drawing.Point(12, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 320);
            this.groupBox1.TabIndex = 126;
            this.groupBox1.UseDecorationPattern = false;
            this.groupBox1.UseSharpStyle = false;
            // 
            // radioButton2
            // 
            this.radioButton2.Checked = false;
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(369, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(357, 24);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "0";
            this.radioButton2.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = false;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(6, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(357, 24);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "0";
            this.radioButton1.CheckedChanged += new WinPaletter.UI.WP.RadioButton.CheckedChangedEventHandler(this.radioButton1_CheckedChanged);
            // 
            // GitHub_FilesCompare
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(756, 451);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bottom_buttons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GitHub_FilesCompare";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Files Conflict";
            this.tableLayoutPanel.ResumeLayout(false);
            this.bottom_buttons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private UI.WP.GroupBox bottom_buttons;
        internal UI.WP.Button button2;
        internal UI.WP.Button button1;
        private Label label2;
        private Label label1;
        private UI.WP.GroupBox groupBox1;
        private UI.WP.RadioButton radioButton2;
        private UI.WP.RadioButton radioButton1;
    }

}