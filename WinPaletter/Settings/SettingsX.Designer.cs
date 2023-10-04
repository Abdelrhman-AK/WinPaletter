using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SettingsX : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsX));
            OpenFileDialog1 = new OpenFileDialog();
            SaveFileDialog1 = new SaveFileDialog();
            ImageList1 = new ImageList(components);
            OpenJSONDlg = new OpenFileDialog();
            OpenFileDialog2 = new OpenFileDialog();
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            TabControl1 = new UI.WP.TabControl();
            TabPage1 = new TabPage();
            AlertBox17 = new UI.WP.AlertBox();
            ComboBox2 = new UI.WP.ComboBox();
            Separator2 = new UI.WP.SeparatorH();
            PictureBox9 = new PictureBox();
            Label3 = new Label();
            Label4 = new Label();
            PictureBox6 = new PictureBox();
            PictureBox5 = new PictureBox();
            CheckBox5 = new UI.WP.CheckBox();
            AlertBox4 = new UI.WP.AlertBox();
            TabPage7 = new TabPage();
            AlertBox2 = new UI.WP.AlertBox();
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            TextBox3 = new UI.WP.TextBox();
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            AlertBox9 = new UI.WP.AlertBox();
            GroupBox1 = new UI.WP.GroupBox();
            Label14 = new Label();
            Label22 = new Label();
            Label15 = new Label();
            PictureBox26 = new PictureBox();
            PictureBox23 = new PictureBox();
            Label16 = new Label();
            Label12 = new Label();
            Label18 = new Label();
            Label13 = new Label();
            PictureBox24 = new PictureBox();
            PictureBox22 = new PictureBox();
            Label19 = new Label();
            Label11 = new Label();
            Label20 = new Label();
            Label10 = new Label();
            PictureBox25 = new PictureBox();
            PictureBox21 = new PictureBox();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Label9 = new Label();
            PictureBox20 = new PictureBox();
            PictureBox18 = new PictureBox();
            CheckBox8 = new UI.WP.CheckBox();
            Separator8 = new UI.WP.SeparatorH();
            PictureBox16 = new PictureBox();
            Label8 = new Label();
            TabPage4 = new TabPage();
            CheckBox30 = new UI.WP.CheckBox();
            PictureBox41 = new PictureBox();
            PictureBox36 = new PictureBox();
            Separator5 = new UI.WP.SeparatorH();
            PictureBox10 = new PictureBox();
            Label5 = new Label();
            PictureBox12 = new PictureBox();
            PictureBox11 = new PictureBox();
            CheckBox6 = new UI.WP.CheckBox();
            RadioButton3 = new UI.WP.RadioButton();
            RadioButton4 = new UI.WP.RadioButton();
            TabPage3 = new TabPage();
            AlertBox22 = new UI.WP.AlertBox();
            CheckBox33 = new UI.WP.CheckBox();
            PictureBox44 = new PictureBox();
            Separator4 = new UI.WP.SeparatorH();
            PictureBox17 = new PictureBox();
            Label1 = new Label();
            PictureBox19 = new PictureBox();
            CheckBox1 = new UI.WP.CheckBox();
            AlertBox1 = new UI.WP.AlertBox();
            PictureBox3 = new PictureBox();
            RadioButton2 = new UI.WP.RadioButton();
            RadioButton1 = new UI.WP.RadioButton();
            TabPage5 = new TabPage();
            TabControl2 = new UI.WP.TabControl();
            TabPage11 = new TabPage();
            AlertBox19 = new UI.WP.AlertBox();
            Label50 = new Label();
            PictureBox62 = new PictureBox();
            Panel11 = new Panel();
            RadioButton23 = new UI.WP.RadioButton();
            RadioButton21 = new UI.WP.RadioButton();
            RadioButton22 = new UI.WP.RadioButton();
            Label51 = new Label();
            AlertBox18 = new UI.WP.AlertBox();
            PictureBox61 = new PictureBox();
            CheckBox25 = new UI.WP.CheckBox();
            CheckBox2 = new UI.WP.CheckBox();
            PictureBox7 = new PictureBox();
            AlertBox3 = new UI.WP.AlertBox();
            AlertBox6 = new UI.WP.AlertBox();
            CheckBox17 = new UI.WP.CheckBox();
            PictureBox37 = new PictureBox();
            TabPage12 = new TabPage();
            AlertBox5 = new UI.WP.AlertBox();
            CheckBox24 = new Label();
            CheckBox23 = new Label();
            Panel5 = new Panel();
            RadioButton10 = new UI.WP.RadioButton();
            RadioButton9 = new UI.WP.RadioButton();
            RadioButton7 = new UI.WP.RadioButton();
            RadioButton8 = new UI.WP.RadioButton();
            PictureBox51 = new PictureBox();
            Panel4 = new Panel();
            RadioButton6 = new UI.WP.RadioButton();
            RadioButton5 = new UI.WP.RadioButton();
            Label36 = new Label();
            TabPage13 = new TabPage();
            AlertBox14 = new UI.WP.AlertBox();
            PictureBox55 = new PictureBox();
            Label39 = new Label();
            Panel6 = new Panel();
            RadioButton11 = new UI.WP.RadioButton();
            RadioButton12 = new UI.WP.RadioButton();
            Label40 = new Label();
            PictureBox50 = new PictureBox();
            CheckBox22 = new UI.WP.CheckBox();
            TabPage14 = new TabPage();
            PictureBox35 = new PictureBox();
            AlertBox15 = new UI.WP.AlertBox();
            CheckBox16 = new UI.WP.CheckBox();
            PictureBox56 = new PictureBox();
            Label41 = new Label();
            Panel7 = new Panel();
            RadioButton13 = new UI.WP.RadioButton();
            RadioButton14 = new UI.WP.RadioButton();
            Label42 = new Label();
            PictureBox14 = new PictureBox();
            CheckBox7 = new UI.WP.CheckBox();
            TabPage10 = new TabPage();
            PictureBox34 = new PictureBox();
            CheckBox15 = new UI.WP.CheckBox();
            AlertBox16 = new UI.WP.AlertBox();
            PictureBox60 = new PictureBox();
            Label48 = new Label();
            Panel10 = new Panel();
            RadioButton19 = new UI.WP.RadioButton();
            RadioButton20 = new UI.WP.RadioButton();
            Label49 = new Label();
            PictureBox59 = new PictureBox();
            Label46 = new Label();
            Panel9 = new Panel();
            RadioButton17 = new UI.WP.RadioButton();
            RadioButton18 = new UI.WP.RadioButton();
            Label47 = new Label();
            PictureBox52 = new PictureBox();
            Label35 = new Label();
            Panel8 = new Panel();
            RadioButton15 = new UI.WP.RadioButton();
            RadioButton16 = new UI.WP.RadioButton();
            Label44 = new Label();
            TabPage20 = new TabPage();
            AlertBox7 = new UI.WP.AlertBox();
            Panel12 = new Panel();
            RadioButton24 = new UI.WP.RadioButton();
            RadioButton25 = new UI.WP.RadioButton();
            CheckBox36 = new UI.WP.CheckBox();
            PictureBox67 = new PictureBox();
            CheckBox35_SFC = new UI.WP.CheckBox();
            PictureBox66 = new PictureBox();
            PictureBox8 = new PictureBox();
            Label2 = new Label();
            Separator6 = new UI.WP.SeparatorH();
            TabPage15 = new TabPage();
            Separator15 = new UI.WP.SeparatorH();
            PictureBox63 = new PictureBox();
            Label52 = new Label();
            TabControl3 = new UI.WP.TabControl();
            TabPage16 = new TabPage();
            PictureBox2 = new PictureBox();
            CheckBox4 = new UI.WP.CheckBox();
            PictureBox54 = new PictureBox();
            CheckBox29 = new UI.WP.CheckBox();
            Separator16 = new UI.WP.SeparatorH();
            PictureBox64 = new PictureBox();
            Button17 = new UI.WP.Button();
            Button17.Click += new EventHandler(Button17_Click);
            Label53 = new Label();
            Button18 = new UI.WP.Button();
            Button18.Click += new EventHandler(Button18_Click);
            RadioImage1 = new UI.WP.RadioImage();
            ListBox2 = new ListBox();
            RadioImage2 = new UI.WP.RadioImage();
            Button15 = new UI.WP.Button();
            Button15.Click += new EventHandler(Button15_Click);
            Label54 = new Label();
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click);
            Label55 = new Label();
            ListBox1 = new ListBox();
            TabPage17 = new TabPage();
            Button20 = new UI.WP.Button();
            Button20.Click += new EventHandler(Button20_Click);
            Label43 = new Label();
            PictureBox57 = new PictureBox();
            Label45 = new Label();
            AlertBox20 = new UI.WP.AlertBox();
            Button19 = new UI.WP.Button();
            Button19.Click += new EventHandler(Button19_Click);
            Label38 = new Label();
            PictureBox53 = new PictureBox();
            Label37 = new Label();
            TabPage18 = new TabPage();
            CheckBox26 = new UI.WP.CheckBox();
            PictureBox65 = new PictureBox();
            CheckBox27 = new UI.WP.CheckBox();
            Label56 = new Label();
            CheckBox28 = new UI.WP.CheckBox();
            TabPage8 = new TabPage();
            PictureBox71 = new PictureBox();
            CheckBox19_ShowSkippedItemsOnDetailedVerbose = new UI.WP.CheckBox();
            VL2 = new UI.WP.RadioImage();
            VL1 = new UI.WP.RadioImage();
            VL0 = new UI.WP.RadioImage();
            Label25 = new Label();
            AlertBox_Themelog = new UI.WP.AlertBox();
            Label27 = new Label();
            NumericUpDown1 = new UI.WP.NumericUpDown();
            CheckBox18 = new UI.WP.CheckBox();
            PictureBox39 = new PictureBox();
            PictureBox40 = new PictureBox();
            Separator10 = new UI.WP.SeparatorH();
            PictureBox38 = new PictureBox();
            Label26 = new Label();
            TabPage2 = new TabPage();
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click);
            TextBox2 = new UI.WP.TextBox();
            TextBox1 = new UI.WP.TextBox();
            Label24 = new Label();
            Label23 = new Label();
            AlertBox13 = new UI.WP.AlertBox();
            PictureBox33 = new PictureBox();
            PictureBox32 = new PictureBox();
            PictureBox31 = new PictureBox();
            CheckBox14 = new UI.WP.CheckBox();
            AlertBox12 = new UI.WP.AlertBox();
            AlertBox11 = new UI.WP.AlertBox();
            PictureBox30 = new PictureBox();
            CheckBox13 = new UI.WP.CheckBox();
            AlertBox8 = new UI.WP.AlertBox();
            PictureBox29 = new PictureBox();
            CheckBox12 = new UI.WP.CheckBox();
            Separator3 = new UI.WP.SeparatorH();
            PictureBox28 = new PictureBox();
            Label7 = new Label();
            TabPage9 = new TabPage();
            GroupBox2 = new UI.WP.GroupBox();
            Panel3 = new Panel();
            EP_ORB_11 = new UI.WP.RadioImage();
            EP_ORB_10 = new UI.WP.RadioImage();
            Panel2 = new Panel();
            EP_Taskbar_11 = new UI.WP.RadioImage();
            EP_Taskbar_10 = new UI.WP.RadioImage();
            Panel1 = new Panel();
            EP_Start_11 = new UI.WP.RadioImage();
            EP_Start_10 = new UI.WP.RadioImage();
            EP_Start_10_Type = new UI.WP.ComboBox();
            Label34 = new Label();
            Label32 = new Label();
            Label31 = new Label();
            Label33 = new Label();
            PictureBox49 = new PictureBox();
            CheckBox21 = new UI.WP.CheckBox();
            PictureBox48 = new PictureBox();
            CheckBox20 = new UI.WP.CheckBox();
            Separator9 = new UI.WP.SeparatorH();
            PictureBox47 = new PictureBox();
            Label30 = new Label();
            TabPage19 = new TabPage();
            PictureBox70 = new PictureBox();
            CheckBox38 = new UI.WP.CheckBox();
            PictureBox69 = new PictureBox();
            CheckBox37 = new UI.WP.CheckBox();
            PictureBox68 = new PictureBox();
            CheckBox35 = new UI.WP.CheckBox();
            PictureBox58 = new PictureBox();
            PictureBox46 = new PictureBox();
            PictureBox45 = new PictureBox();
            PictureBox42 = new PictureBox();
            PictureBox27 = new PictureBox();
            PictureBox43 = new PictureBox();
            CheckBox32 = new UI.WP.CheckBox();
            Separator1 = new UI.WP.SeparatorH();
            PictureBox4 = new PictureBox();
            Label21 = new Label();
            CheckBox34 = new UI.WP.CheckBox();
            CheckBox11 = new UI.WP.CheckBox();
            CheckBox3 = new UI.WP.CheckBox();
            CheckBox31 = new UI.WP.CheckBox();
            ComboBox3 = new UI.WP.ComboBox();
            CheckBox10 = new UI.WP.CheckBox();
            TabPage6 = new TabPage();
            PictureBox15 = new PictureBox();
            CheckBox9 = new UI.WP.CheckBox();
            Separator7 = new UI.WP.SeparatorH();
            PictureBox13 = new PictureBox();
            Label6 = new Label();
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click);
            AnimatedBox1 = new UI.WP.AnimatedBox();
            PictureBox1 = new PictureBox();
            Label17 = new Label();
            FolderBrowserDialog1 = new FolderBrowserDialog();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            TabPage7.SuspendLayout();
            GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox44).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            TabPage5.SuspendLayout();
            TabControl2.SuspendLayout();
            TabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox62).BeginInit();
            Panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox61).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox37).BeginInit();
            TabPage12.SuspendLayout();
            Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox51).BeginInit();
            Panel4.SuspendLayout();
            TabPage13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox55).BeginInit();
            Panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox50).BeginInit();
            TabPage14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox35).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox56).BeginInit();
            Panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            TabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox60).BeginInit();
            Panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox59).BeginInit();
            Panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox52).BeginInit();
            Panel8.SuspendLayout();
            TabPage20.SuspendLayout();
            Panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox67).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox66).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            TabPage15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox63).BeginInit();
            TabControl3.SuspendLayout();
            TabPage16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox54).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox64).BeginInit();
            TabPage17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox57).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox53).BeginInit();
            TabPage18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox65).BeginInit();
            TabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox71).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox39).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox38).BeginInit();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox32).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).BeginInit();
            TabPage9.SuspendLayout();
            GroupBox2.SuspendLayout();
            Panel3.SuspendLayout();
            Panel2.SuspendLayout();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox49).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox48).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox47).BeginInit();
            TabPage19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox70).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox69).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox68).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox58).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox45).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox42).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox43).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            AnimatedBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.Filter = "WinPaletter Settings File (*.wpsf)|*.wpsf";
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.Filter = "WinPaletter Settings File (*.wpsf)|*.wpsf";
            // 
            // ImageList1
            // 
            ImageList1.ImageStream = (ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
            ImageList1.TransparentColor = Color.Transparent;
            ImageList1.Images.SetKeyName(0, "Updates");
            ImageList1.Images.SetKeyName(1, "Language");
            ImageList1.Images.SetKeyName(2, "Appearance");
            ImageList1.Images.SetKeyName(3, "ThemeFileManagement");
            ImageList1.Images.SetKeyName(4, "ThemeApplyingBehavior");
            ImageList1.Images.SetKeyName(5, "Store");
            ImageList1.Images.SetKeyName(6, "Log");
            ImageList1.Images.SetKeyName(7, "Terminals");
            ImageList1.Images.SetKeyName(8, "ExplorerPatcher");
            ImageList1.Images.SetKeyName(9, "ColorItemInfo");
            ImageList1.Images.SetKeyName(10, "Miscellaneous");
            // 
            // OpenJSONDlg
            // 
            OpenJSONDlg.Filter = "JSON File (*.json)|*.json";
            // 
            // OpenFileDialog2
            // 
            OpenFileDialog2.Filter = "JSON File (*.json)|*.json";
            // 
            // Button12
            // 
            Button12.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button12.BackColor = Color.FromArgb(34, 34, 34);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = (Image)resources.GetObject("Button12.Image");
            Button12.ImageAlign = ContentAlignment.MiddleLeft;
            Button12.LineColor = Color.FromArgb(73, 123, 145);
            Button12.Location = new Point(897, 560);
            Button12.Name = "Button12";
            Button12.Size = new Size(130, 34);
            Button12.TabIndex = 24;
            Button12.Text = "Save and close";
            Button12.UseVisualStyleBackColor = false;
            // 
            // TabControl1
            // 
            TabControl1.Alignment = TabAlignment.Left;
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage7);
            TabControl1.Controls.Add(TabPage4);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Controls.Add(TabPage5);
            TabControl1.Controls.Add(TabPage15);
            TabControl1.Controls.Add(TabPage8);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage9);
            TabControl1.Controls.Add(TabPage19);
            TabControl1.Controls.Add(TabPage6);
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.Font = new Font("Segoe UI", 9.0f);
            TabControl1.ImageList = ImageList1;
            TabControl1.ItemSize = new Size(37, 195);
            TabControl1.LineColor = Color.FromArgb(0, 81, 210);
            TabControl1.Location = new Point(6, 74);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(1026, 479);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.TabIndex = 23;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(AlertBox17);
            TabPage1.Controls.Add(ComboBox2);
            TabPage1.Controls.Add(Separator2);
            TabPage1.Controls.Add(PictureBox9);
            TabPage1.Controls.Add(Label3);
            TabPage1.Controls.Add(Label4);
            TabPage1.Controls.Add(PictureBox6);
            TabPage1.Controls.Add(PictureBox5);
            TabPage1.Controls.Add(CheckBox5);
            TabPage1.Controls.Add(AlertBox4);
            TabPage1.Location = new Point(199, 4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(823, 471);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Updates";
            // 
            // AlertBox17
            // 
            AlertBox17.AlertStyle = UI.WP.AlertBox.Style.Warning;
            AlertBox17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox17.BackColor = Color.FromArgb(125, 20, 30);
            AlertBox17.CenterText = true;
            AlertBox17.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox17.Font = new Font("Segoe UI", 9.0f);
            AlertBox17.Image = null;
            AlertBox17.Location = new Point(150, 84);
            AlertBox17.Name = "AlertBox17";
            AlertBox17.Size = new Size(515, 19);
            AlertBox17.TabIndex = 20;
            AlertBox17.TabStop = false;
            AlertBox17.Text = null;
            AlertBox17.Visible = false;
            // 
            // ComboBox2
            // 
            ComboBox2.BackColor = Color.FromArgb(55, 55, 55);
            ComboBox2.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox2.Font = new Font("Segoe UI", 9.0f);
            ComboBox2.ForeColor = Color.White;
            ComboBox2.FormattingEnabled = true;
            ComboBox2.ItemHeight = 20;
            ComboBox2.Items.AddRange(new object[] { "Stable", "Beta" });
            ComboBox2.Location = new Point(47, 144);
            ComboBox2.Name = "ComboBox2";
            ComboBox2.Size = new Size(268, 26);
            ComboBox2.TabIndex = 19;
            // 
            // Separator2
            // 
            Separator2.AlternativeLook = false;
            Separator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator2.Location = new Point(6, 47);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(811, 1);
            Separator2.TabIndex = 18;
            Separator2.TabStop = false;
            // 
            // PictureBox9
            // 
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(6, 6);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(35, 35);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 3;
            PictureBox9.TabStop = false;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label3.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(47, 6);
            Label3.Name = "Label3";
            Label3.Size = new Size(770, 35);
            Label3.TabIndex = 2;
            Label3.Text = "Updates";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(47, 117);
            Label4.Name = "Label4";
            Label4.Size = new Size(770, 24);
            Label4.TabIndex = 17;
            Label4.Text = "Updates channel:";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(17, 54);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(24, 24);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 5;
            PictureBox6.TabStop = false;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(17, 117);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 16;
            PictureBox5.TabStop = false;
            // 
            // CheckBox5
            // 
            CheckBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox5.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox5.Checked = true;
            CheckBox5.Font = new Font("Segoe UI", 9.0f);
            CheckBox5.ForeColor = Color.White;
            CheckBox5.Location = new Point(47, 54);
            CheckBox5.Name = "CheckBox5";
            CheckBox5.Size = new Size(770, 24);
            CheckBox5.TabIndex = 6;
            CheckBox5.Text = "Automatic check for updates every time I open the application";
            // 
            // AlertBox4
            // 
            AlertBox4.AlertStyle = UI.WP.AlertBox.Style.Success;
            AlertBox4.BackColor = Color.FromArgb(60, 85, 79);
            AlertBox4.CenterText = true;
            AlertBox4.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox4.Font = new Font("Segoe UI", 9.0f);
            AlertBox4.Image = null;
            AlertBox4.Location = new Point(47, 84);
            AlertBox4.Name = "AlertBox4";
            AlertBox4.Size = new Size(97, 19);
            AlertBox4.TabIndex = 10;
            AlertBox4.TabStop = false;
            AlertBox4.Text = "Recommended";
            // 
            // TabPage7
            // 
            TabPage7.BackColor = Color.FromArgb(25, 25, 25);
            TabPage7.Controls.Add(AlertBox2);
            TabPage7.Controls.Add(Button11);
            TabPage7.Controls.Add(TextBox3);
            TabPage7.Controls.Add(Button10);
            TabPage7.Controls.Add(Button8);
            TabPage7.Controls.Add(AlertBox9);
            TabPage7.Controls.Add(GroupBox1);
            TabPage7.Controls.Add(Button7);
            TabPage7.Controls.Add(Label9);
            TabPage7.Controls.Add(PictureBox20);
            TabPage7.Controls.Add(PictureBox18);
            TabPage7.Controls.Add(CheckBox8);
            TabPage7.Controls.Add(Separator8);
            TabPage7.Controls.Add(PictureBox16);
            TabPage7.Controls.Add(Label8);
            TabPage7.Location = new Point(199, 4);
            TabPage7.Name = "TabPage7";
            TabPage7.Padding = new Padding(3);
            TabPage7.Size = new Size(823, 471);
            TabPage7.TabIndex = 6;
            TabPage7.Text = "Language";
            // 
            // AlertBox2
            // 
            AlertBox2.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox2.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox2.CenterText = false;
            AlertBox2.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox2.Font = new Font("Segoe UI", 9.0f);
            AlertBox2.Image = null;
            AlertBox2.Location = new Point(97, 390);
            AlertBox2.Name = "AlertBox2";
            AlertBox2.Size = new Size(500, 24);
            AlertBox2.TabIndex = 41;
            AlertBox2.TabStop = false;
            AlertBox2.Text = "WinPaletter language files (*.wplng) are not supported now, JSON is the new forma" + "t";
            // 
            // Button11
            // 
            Button11.BackColor = Color.FromArgb(34, 34, 34);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.ImageAlign = ContentAlignment.MiddleLeft;
            Button11.LineColor = Color.FromArgb(5, 62, 98);
            Button11.Location = new Point(353, 113);
            Button11.Name = "Button11";
            Button11.Size = new Size(244, 24);
            Button11.TabIndex = 40;
            Button11.Text = "Language development tools";
            Button11.UseVisualStyleBackColor = false;
            // 
            // TextBox3
            // 
            TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox3.BackColor = Color.FromArgb(55, 55, 55);
            TextBox3.DrawOnGlass = false;
            TextBox3.ForeColor = Color.White;
            TextBox3.Location = new Point(97, 84);
            TextBox3.MaxLength = 32767;
            TextBox3.Multiline = false;
            TextBox3.Name = "TextBox3";
            TextBox3.ReadOnly = false;
            TextBox3.Scrollbars = ScrollBars.None;
            TextBox3.SelectedText = "";
            TextBox3.SelectionLength = 0;
            TextBox3.SelectionStart = 0;
            TextBox3.Size = new Size(670, 24);
            TextBox3.TabIndex = 39;
            TextBox3.TextAlign = HorizontalAlignment.Left;
            TextBox3.UseSystemPasswordChar = false;
            TextBox3.WordWrap = true;
            // 
            // Button10
            // 
            Button10.BackColor = Color.FromArgb(34, 34, 34);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = (Image)resources.GetObject("Button10.Image");
            Button10.ImageAlign = ContentAlignment.MiddleLeft;
            Button10.LineColor = Color.FromArgb(7, 45, 66);
            Button10.Location = new Point(97, 113);
            Button10.Name = "Button10";
            Button10.Size = new Size(250, 24);
            Button10.TabIndex = 32;
            Button10.Text = "Download language";
            Button10.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.BackColor = Color.FromArgb(34, 34, 34);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = (Image)resources.GetObject("Button8.Image");
            Button8.ImageAlign = ContentAlignment.MiddleLeft;
            Button8.LineColor = Color.FromArgb(30, 107, 146);
            Button8.Location = new Point(97, 143);
            Button8.Name = "Button8";
            Button8.Size = new Size(500, 24);
            Button8.TabIndex = 31;
            Button8.Text = "See how to contribute in language development";
            Button8.UseVisualStyleBackColor = false;
            // 
            // AlertBox9
            // 
            AlertBox9.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox9.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox9.CenterText = false;
            AlertBox9.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox9.Font = new Font("Segoe UI", 9.0f);
            AlertBox9.Image = null;
            AlertBox9.Location = new Point(97, 360);
            AlertBox9.Name = "AlertBox9";
            AlertBox9.Size = new Size(500, 24);
            AlertBox9.TabIndex = 30;
            AlertBox9.TabStop = false;
            AlertBox9.Text = "To return to English, uncheck the check-box, save settings and restart the applic" + "ation";
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(Label14);
            GroupBox1.Controls.Add(Label22);
            GroupBox1.Controls.Add(Label15);
            GroupBox1.Controls.Add(PictureBox26);
            GroupBox1.Controls.Add(PictureBox23);
            GroupBox1.Controls.Add(Label16);
            GroupBox1.Controls.Add(Label12);
            GroupBox1.Controls.Add(Label18);
            GroupBox1.Controls.Add(Label13);
            GroupBox1.Controls.Add(PictureBox24);
            GroupBox1.Controls.Add(PictureBox22);
            GroupBox1.Controls.Add(Label19);
            GroupBox1.Controls.Add(Label11);
            GroupBox1.Controls.Add(Label20);
            GroupBox1.Controls.Add(Label10);
            GroupBox1.Controls.Add(PictureBox25);
            GroupBox1.Controls.Add(PictureBox21);
            GroupBox1.Location = new Point(97, 173);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(500, 181);
            GroupBox1.TabIndex = 29;
            // 
            // Label14
            // 
            Label14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label14.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label14.Location = new Point(182, 123);
            Label14.Name = "Label14";
            Label14.Size = new Size(309, 24);
            Label14.TabIndex = 35;
            Label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label22
            // 
            Label22.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label22.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label22.Location = new Point(33, 153);
            Label22.Name = "Label22";
            Label22.Size = new Size(458, 24);
            Label22.TabIndex = 43;
            Label22.Text = "It has left to right layout";
            Label22.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            Label15.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label15.Location = new Point(33, 123);
            Label15.Name = "Label15";
            Label15.Size = new Size(143, 24);
            Label15.TabIndex = 34;
            Label15.Text = "For app version:";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox26
            // 
            PictureBox26.Image = (Image)resources.GetObject("PictureBox26.Image");
            PictureBox26.Location = new Point(3, 153);
            PictureBox26.Name = "PictureBox26";
            PictureBox26.Size = new Size(24, 24);
            PictureBox26.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox26.TabIndex = 42;
            PictureBox26.TabStop = false;
            // 
            // PictureBox23
            // 
            PictureBox23.Image = (Image)resources.GetObject("PictureBox23.Image");
            PictureBox23.Location = new Point(3, 123);
            PictureBox23.Name = "PictureBox23";
            PictureBox23.Size = new Size(24, 24);
            PictureBox23.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox23.TabIndex = 33;
            PictureBox23.TabStop = false;
            // 
            // Label16
            // 
            Label16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label16.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label16.Location = new Point(182, 33);
            Label16.Name = "Label16";
            Label16.Size = new Size(309, 24);
            Label16.TabIndex = 41;
            Label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label12
            // 
            Label12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label12.Location = new Point(182, 93);
            Label12.Name = "Label12";
            Label12.Size = new Size(309, 24);
            Label12.TabIndex = 32;
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label18
            // 
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label18.Location = new Point(33, 33);
            Label18.Name = "Label18";
            Label18.Size = new Size(143, 24);
            Label18.TabIndex = 40;
            Label18.Text = "Language code:";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label13
            // 
            Label13.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label13.Location = new Point(33, 93);
            Label13.Name = "Label13";
            Label13.Size = new Size(143, 24);
            Label13.TabIndex = 31;
            Label13.Text = "Translation version:";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox24
            // 
            PictureBox24.Image = (Image)resources.GetObject("PictureBox24.Image");
            PictureBox24.Location = new Point(3, 33);
            PictureBox24.Name = "PictureBox24";
            PictureBox24.Size = new Size(24, 24);
            PictureBox24.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox24.TabIndex = 39;
            PictureBox24.TabStop = false;
            // 
            // PictureBox22
            // 
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(3, 93);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(24, 24);
            PictureBox22.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox22.TabIndex = 30;
            PictureBox22.TabStop = false;
            // 
            // Label19
            // 
            Label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label19.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label19.Location = new Point(182, 3);
            Label19.Name = "Label19";
            Label19.Size = new Size(309, 24);
            Label19.TabIndex = 38;
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label11
            // 
            Label11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label11.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label11.Location = new Point(182, 63);
            Label11.Name = "Label11";
            Label11.Size = new Size(309, 24);
            Label11.TabIndex = 29;
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label20
            // 
            Label20.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label20.Location = new Point(33, 3);
            Label20.Name = "Label20";
            Label20.Size = new Size(143, 24);
            Label20.TabIndex = 37;
            Label20.Text = "Language:";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            Label10.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label10.Location = new Point(33, 63);
            Label10.Name = "Label10";
            Label10.Size = new Size(143, 24);
            Label10.TabIndex = 28;
            Label10.Text = "Translator name:";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox25
            // 
            PictureBox25.Image = (Image)resources.GetObject("PictureBox25.Image");
            PictureBox25.Location = new Point(3, 3);
            PictureBox25.Name = "PictureBox25";
            PictureBox25.Size = new Size(24, 24);
            PictureBox25.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox25.TabIndex = 36;
            PictureBox25.TabStop = false;
            // 
            // PictureBox21
            // 
            PictureBox21.Image = (Image)resources.GetObject("PictureBox21.Image");
            PictureBox21.Location = new Point(3, 63);
            PictureBox21.Name = "PictureBox21";
            PictureBox21.Size = new Size(24, 24);
            PictureBox21.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox21.TabIndex = 27;
            PictureBox21.TabStop = false;
            // 
            // Button7
            // 
            Button7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button7.BackColor = Color.FromArgb(34, 34, 34);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = (Image)resources.GetObject("Button7.Image");
            Button7.LineColor = Color.FromArgb(164, 125, 25);
            Button7.Location = new Point(773, 84);
            Button7.Name = "Button7";
            Button7.Size = new Size(44, 25);
            Button7.TabIndex = 28;
            Button7.UseVisualStyleBackColor = false;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label9.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label9.Location = new Point(47, 84);
            Label9.Name = "Label9";
            Label9.Size = new Size(44, 24);
            Label9.TabIndex = 26;
            Label9.Text = "File:";
            Label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox20
            // 
            PictureBox20.Image = (Image)resources.GetObject("PictureBox20.Image");
            PictureBox20.Location = new Point(17, 84);
            PictureBox20.Name = "PictureBox20";
            PictureBox20.Size = new Size(24, 24);
            PictureBox20.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox20.TabIndex = 25;
            PictureBox20.TabStop = false;
            // 
            // PictureBox18
            // 
            PictureBox18.Image = (Image)resources.GetObject("PictureBox18.Image");
            PictureBox18.Location = new Point(17, 54);
            PictureBox18.Name = "PictureBox18";
            PictureBox18.Size = new Size(24, 24);
            PictureBox18.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox18.TabIndex = 23;
            PictureBox18.TabStop = false;
            // 
            // CheckBox8
            // 
            CheckBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox8.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox8.Checked = false;
            CheckBox8.Font = new Font("Segoe UI", 9.0f);
            CheckBox8.ForeColor = Color.White;
            CheckBox8.Location = new Point(47, 54);
            CheckBox8.Name = "CheckBox8";
            CheckBox8.Size = new Size(770, 24);
            CheckBox8.TabIndex = 24;
            CheckBox8.Text = "Enabled";
            // 
            // Separator8
            // 
            Separator8.AlternativeLook = false;
            Separator8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator8.Location = new Point(6, 47);
            Separator8.Name = "Separator8";
            Separator8.Size = new Size(811, 1);
            Separator8.TabIndex = 22;
            Separator8.TabStop = false;
            // 
            // PictureBox16
            // 
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(6, 6);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(35, 35);
            PictureBox16.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox16.TabIndex = 21;
            PictureBox16.TabStop = false;
            // 
            // Label8
            // 
            Label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label8.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label8.Location = new Point(47, 6);
            Label8.Name = "Label8";
            Label8.Size = new Size(770, 35);
            Label8.TabIndex = 20;
            Label8.Text = "Language";
            Label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(CheckBox30);
            TabPage4.Controls.Add(PictureBox41);
            TabPage4.Controls.Add(PictureBox36);
            TabPage4.Controls.Add(Separator5);
            TabPage4.Controls.Add(PictureBox10);
            TabPage4.Controls.Add(Label5);
            TabPage4.Controls.Add(PictureBox12);
            TabPage4.Controls.Add(PictureBox11);
            TabPage4.Controls.Add(CheckBox6);
            TabPage4.Controls.Add(RadioButton3);
            TabPage4.Controls.Add(RadioButton4);
            TabPage4.Location = new Point(199, 4);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(823, 471);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "Appearance";
            // 
            // CheckBox30
            // 
            CheckBox30.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox30.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox30.Checked = true;
            CheckBox30.Font = new Font("Segoe UI", 9.0f);
            CheckBox30.ForeColor = Color.White;
            CheckBox30.Location = new Point(47, 144);
            CheckBox30.Name = "CheckBox30";
            CheckBox30.Size = new Size(770, 24);
            CheckBox30.TabIndex = 22;
            CheckBox30.Text = "Make WinPaletter appearance is managed by the loaded theme if a custom appearance" + " inside the theme is enabled";
            // 
            // PictureBox41
            // 
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(17, 144);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(24, 24);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 21;
            PictureBox41.TabStop = false;
            // 
            // PictureBox36
            // 
            PictureBox36.Image = Properties.Resources.Native11;
            PictureBox36.Location = new Point(17, 114);
            PictureBox36.Name = "PictureBox36";
            PictureBox36.Size = new Size(24, 24);
            PictureBox36.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox36.TabIndex = 20;
            PictureBox36.TabStop = false;
            // 
            // Separator5
            // 
            Separator5.AlternativeLook = false;
            Separator5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator5.Location = new Point(6, 47);
            Separator5.Name = "Separator5";
            Separator5.Size = new Size(811, 1);
            Separator5.TabIndex = 19;
            Separator5.TabStop = false;
            // 
            // PictureBox10
            // 
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(6, 6);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(35, 35);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 3;
            PictureBox10.TabStop = false;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label5.Location = new Point(47, 6);
            Label5.Name = "Label5";
            Label5.Size = new Size(770, 35);
            Label5.TabIndex = 2;
            Label5.Text = "Appearance";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox12
            // 
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(17, 84);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox12.TabIndex = 15;
            PictureBox12.TabStop = false;
            // 
            // PictureBox11
            // 
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(17, 54);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 11;
            PictureBox11.TabStop = false;
            // 
            // CheckBox6
            // 
            CheckBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox6.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox6.Checked = true;
            CheckBox6.Font = new Font("Segoe UI", 9.0f);
            CheckBox6.ForeColor = Color.White;
            CheckBox6.Location = new Point(47, 114);
            CheckBox6.Name = "CheckBox6";
            CheckBox6.Size = new Size(770, 24);
            CheckBox6.TabIndex = 14;
            CheckBox6.Text = "Automatic get from current applied Windows mode";
            // 
            // RadioButton3
            // 
            RadioButton3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton3.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton3.Checked = true;
            RadioButton3.Font = new Font("Segoe UI", 9.0f);
            RadioButton3.ForeColor = Color.White;
            RadioButton3.Location = new Point(47, 54);
            RadioButton3.Name = "RadioButton3";
            RadioButton3.Size = new Size(770, 24);
            RadioButton3.TabIndex = 12;
            RadioButton3.Text = "Dark mode";
            // 
            // RadioButton4
            // 
            RadioButton4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RadioButton4.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton4.Checked = false;
            RadioButton4.Font = new Font("Segoe UI", 9.0f);
            RadioButton4.ForeColor = Color.White;
            RadioButton4.Location = new Point(47, 84);
            RadioButton4.Name = "RadioButton4";
            RadioButton4.Size = new Size(770, 24);
            RadioButton4.TabIndex = 13;
            RadioButton4.Text = "Light mode";
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(AlertBox22);
            TabPage3.Controls.Add(CheckBox33);
            TabPage3.Controls.Add(PictureBox44);
            TabPage3.Controls.Add(Separator4);
            TabPage3.Controls.Add(PictureBox17);
            TabPage3.Controls.Add(Label1);
            TabPage3.Controls.Add(PictureBox19);
            TabPage3.Controls.Add(CheckBox1);
            TabPage3.Controls.Add(AlertBox1);
            TabPage3.Controls.Add(PictureBox3);
            TabPage3.Controls.Add(RadioButton2);
            TabPage3.Controls.Add(RadioButton1);
            TabPage3.Location = new Point(199, 4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(823, 471);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Theme file management";
            // 
            // AlertBox22
            // 
            AlertBox22.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox22.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox22.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox22.CenterText = false;
            AlertBox22.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox22.Font = new Font("Segoe UI", 9.0f);
            AlertBox22.Image = null;
            AlertBox22.Location = new Point(51, 174);
            AlertBox22.Name = "AlertBox22";
            AlertBox22.Size = new Size(466, 40);
            AlertBox22.TabIndex = 33;
            AlertBox22.TabStop = false;
            AlertBox22.Text = "Compressed themes files won't work with WinPaletter versions less than 1.0.7.7" + '\r' + '\n' + "I" + "f you want to design a theme for older versions, use WinPaletter theme converter" + ".";
            // 
            // CheckBox33
            // 
            CheckBox33.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox33.Checked = true;
            CheckBox33.Font = new Font("Segoe UI", 9.0f);
            CheckBox33.ForeColor = Color.White;
            CheckBox33.Location = new Point(47, 144);
            CheckBox33.Name = "CheckBox33";
            CheckBox33.Size = new Size(770, 24);
            CheckBox33.TabIndex = 32;
            CheckBox33.Text = "Save theme files compressed (to save space and make loading a store theme quick)";
            // 
            // PictureBox44
            // 
            PictureBox44.Image = (Image)resources.GetObject("PictureBox44.Image");
            PictureBox44.Location = new Point(17, 144);
            PictureBox44.Name = "PictureBox44";
            PictureBox44.Size = new Size(24, 24);
            PictureBox44.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox44.TabIndex = 31;
            PictureBox44.TabStop = false;
            // 
            // Separator4
            // 
            Separator4.AlternativeLook = false;
            Separator4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator4.Location = new Point(6, 47);
            Separator4.Name = "Separator4";
            Separator4.Size = new Size(811, 1);
            Separator4.TabIndex = 19;
            Separator4.TabStop = false;
            // 
            // PictureBox17
            // 
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(6, 6);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(35, 35);
            PictureBox17.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox17.TabIndex = 3;
            PictureBox17.TabStop = false;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(47, 6);
            Label1.Name = "Label1";
            Label1.Size = new Size(770, 35);
            Label1.TabIndex = 2;
            Label1.Text = "Theme file type management";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox19
            // 
            PictureBox19.Image = (Image)resources.GetObject("PictureBox19.Image");
            PictureBox19.Location = new Point(17, 54);
            PictureBox19.Name = "PictureBox19";
            PictureBox19.Size = new Size(24, 24);
            PictureBox19.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox19.TabIndex = 5;
            PictureBox19.TabStop = false;
            // 
            // CheckBox1
            // 
            CheckBox1.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox1.Checked = true;
            CheckBox1.Font = new Font("Segoe UI", 9.0f);
            CheckBox1.ForeColor = Color.White;
            CheckBox1.Location = new Point(47, 54);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(637, 24);
            CheckBox1.TabIndex = 6;
            CheckBox1.Text = "Automatic add extension of theme file and setting file (*.wpth,*.wpsf) to registr" + "y every time I open the program";
            // 
            // AlertBox1
            // 
            AlertBox1.AlertStyle = UI.WP.AlertBox.Style.Success;
            AlertBox1.BackColor = Color.FromArgb(60, 85, 79);
            AlertBox1.CenterText = true;
            AlertBox1.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox1.Font = new Font("Segoe UI", 9.0f);
            AlertBox1.Image = null;
            AlertBox1.Location = new Point(690, 57);
            AlertBox1.Name = "AlertBox1";
            AlertBox1.Size = new Size(91, 19);
            AlertBox1.TabIndex = 10;
            AlertBox1.TabStop = false;
            AlertBox1.Text = "Recommended";
            // 
            // PictureBox3
            // 
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(17, 84);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 24);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 7;
            PictureBox3.TabStop = false;
            // 
            // RadioButton2
            // 
            RadioButton2.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton2.Checked = false;
            RadioButton2.Font = new Font("Segoe UI", 9.0f);
            RadioButton2.ForeColor = Color.White;
            RadioButton2.Location = new Point(47, 114);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(770, 24);
            RadioButton2.TabIndex = 9;
            RadioButton2.Text = "Opening theme file from explorer applies the theme without opening the applicatio" + "n";
            // 
            // RadioButton1
            // 
            RadioButton1.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton1.Checked = true;
            RadioButton1.Font = new Font("Segoe UI", 9.0f);
            RadioButton1.ForeColor = Color.White;
            RadioButton1.Location = new Point(47, 84);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(770, 24);
            RadioButton1.TabIndex = 8;
            RadioButton1.Text = "Opening theme file from explorer previews it in the application";
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(TabControl2);
            TabPage5.Controls.Add(PictureBox8);
            TabPage5.Controls.Add(Label2);
            TabPage5.Controls.Add(Separator6);
            TabPage5.Location = new Point(199, 4);
            TabPage5.Name = "TabPage5";
            TabPage5.Size = new Size(823, 471);
            TabPage5.TabIndex = 4;
            TabPage5.Text = "Theme applying behavior";
            // 
            // TabControl2
            // 
            TabControl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl2.Controls.Add(TabPage11);
            TabControl2.Controls.Add(TabPage12);
            TabControl2.Controls.Add(TabPage13);
            TabControl2.Controls.Add(TabPage14);
            TabControl2.Controls.Add(TabPage10);
            TabControl2.Controls.Add(TabPage20);
            TabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl2.Font = new Font("Segoe UI", 9.0f);
            TabControl2.ItemSize = new Size(115, 30);
            TabControl2.LineColor = Color.FromArgb(0, 81, 210);
            TabControl2.Location = new Point(0, 49);
            TabControl2.Multiline = true;
            TabControl2.Name = "TabControl2";
            TabControl2.SelectedIndex = 0;
            TabControl2.Size = new Size(823, 420);
            TabControl2.SizeMode = TabSizeMode.Fixed;
            TabControl2.TabIndex = 39;
            // 
            // TabPage11
            // 
            TabPage11.BackColor = Color.FromArgb(25, 25, 25);
            TabPage11.Controls.Add(AlertBox19);
            TabPage11.Controls.Add(Label50);
            TabPage11.Controls.Add(PictureBox62);
            TabPage11.Controls.Add(Panel11);
            TabPage11.Controls.Add(Label51);
            TabPage11.Controls.Add(AlertBox18);
            TabPage11.Controls.Add(PictureBox61);
            TabPage11.Controls.Add(CheckBox25);
            TabPage11.Controls.Add(CheckBox2);
            TabPage11.Controls.Add(PictureBox7);
            TabPage11.Controls.Add(AlertBox3);
            TabPage11.Controls.Add(AlertBox6);
            TabPage11.Controls.Add(CheckBox17);
            TabPage11.Controls.Add(PictureBox37);
            TabPage11.Location = new Point(4, 34);
            TabPage11.Name = "TabPage11";
            TabPage11.Size = new Size(815, 382);
            TabPage11.TabIndex = 0;
            TabPage11.Text = "General";
            // 
            // AlertBox19
            // 
            AlertBox19.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox19.BackColor = Color.FromArgb(44, 52, 35);
            AlertBox19.CenterText = false;
            AlertBox19.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox19.Font = new Font("Segoe UI", 9.0f);
            AlertBox19.Image = (Image)resources.GetObject("AlertBox19.Image");
            AlertBox19.Location = new Point(56, 308);
            AlertBox19.Name = "AlertBox19";
            AlertBox19.Size = new Size(723, 30);
            AlertBox19.TabIndex = 49;
            AlertBox19.TabStop = false;
            AlertBox19.Text = "Writing this value to registry without Adminstrator rights will take time more th" + "an usual";
            // 
            // Label50
            // 
            Label50.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label50.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label50.Location = new Point(56, 213);
            Label50.Name = "Label50";
            Label50.Size = new Size(754, 26);
            Label50.TabIndex = 48;
            Label50.Text = @"HKEY_USERS\.DEFAULT\Control Panel\Desktop (Wallpaper, WallpaperStyle, TileWallpap" + "er, Pattern)";
            Label50.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox62
            // 
            PictureBox62.Image = (Image)resources.GetObject("PictureBox62.Image");
            PictureBox62.Location = new Point(13, 188);
            PictureBox62.Name = "PictureBox62";
            PictureBox62.Size = new Size(24, 24);
            PictureBox62.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox62.TabIndex = 45;
            PictureBox62.TabStop = false;
            // 
            // Panel11
            // 
            Panel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel11.Controls.Add(RadioButton23);
            Panel11.Controls.Add(RadioButton21);
            Panel11.Controls.Add(RadioButton22);
            Panel11.Location = new Point(56, 241);
            Panel11.Name = "Panel11";
            Panel11.Size = new Size(723, 61);
            Panel11.TabIndex = 47;
            // 
            // RadioButton23
            // 
            RadioButton23.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton23.Checked = false;
            RadioButton23.Font = new Font("Segoe UI", 9.0f);
            RadioButton23.ForeColor = Color.White;
            RadioButton23.Location = new Point(297, 3);
            RadioButton23.Name = "RadioButton23";
            RadioButton23.Size = new Size(295, 24);
            RadioButton23.TabIndex = 2;
            RadioButton23.Text = "Restore defaults (No wallpaper on LogonUI)";
            // 
            // RadioButton21
            // 
            RadioButton21.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton21.Checked = true;
            RadioButton21.Font = new Font("Segoe UI", 9.0f);
            RadioButton21.ForeColor = Color.White;
            RadioButton21.Location = new Point(19, 33);
            RadioButton21.Name = "RadioButton21";
            RadioButton21.Size = new Size(272, 24);
            RadioButton21.TabIndex = 1;
            RadioButton21.Text = "Don't change";
            // 
            // RadioButton22
            // 
            RadioButton22.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton22.Checked = false;
            RadioButton22.Font = new Font("Segoe UI", 9.0f);
            RadioButton22.ForeColor = Color.White;
            RadioButton22.Location = new Point(19, 3);
            RadioButton22.Name = "RadioButton22";
            RadioButton22.Size = new Size(272, 24);
            RadioButton22.TabIndex = 0;
            RadioButton22.Text = "Copy from current desktop";
            // 
            // Label51
            // 
            Label51.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label51.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label51.Location = new Point(43, 188);
            Label51.Name = "Label51";
            Label51.Size = new Size(767, 24);
            Label51.TabIndex = 46;
            Label51.Text = "Desktop wallpaper for Classic LogonUI && all users ";
            Label51.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AlertBox18
            // 
            AlertBox18.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox18.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox18.CenterText = false;
            AlertBox18.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox18.Font = new Font("Segoe UI", 9.0f);
            AlertBox18.Image = null;
            AlertBox18.Location = new Point(52, 158);
            AlertBox18.Name = "AlertBox18";
            AlertBox18.Size = new Size(625, 22);
            AlertBox18.TabIndex = 44;
            AlertBox18.TabStop = false;
            AlertBox18.Text = "User Preference Mask has shared items between Classic Colors, Windows Effects and" + " Cursors";
            // 
            // PictureBox61
            // 
            PictureBox61.Image = (Image)resources.GetObject("PictureBox61.Image");
            PictureBox61.Location = new Point(13, 128);
            PictureBox61.Name = "PictureBox61";
            PictureBox61.Size = new Size(24, 24);
            PictureBox61.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox61.TabIndex = 25;
            PictureBox61.TabStop = false;
            // 
            // CheckBox25
            // 
            CheckBox25.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox25.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox25.Checked = false;
            CheckBox25.Font = new Font("Segoe UI", 9.0f);
            CheckBox25.ForeColor = Color.White;
            CheckBox25.Location = new Point(43, 128);
            CheckBox25.Name = "CheckBox25";
            CheckBox25.Size = new Size(767, 24);
            CheckBox25.TabIndex = 2;
            CheckBox25.Text = @"Include User Preference Mask for all users (HKEY_USERS\.DEFAULT\Control Panel\Des" + "ktop : UserPreferencesMask)";
            // 
            // CheckBox2
            // 
            CheckBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox2.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox2.Checked = false;
            CheckBox2.Font = new Font("Segoe UI", 9.0f);
            CheckBox2.ForeColor = Color.White;
            CheckBox2.Location = new Point(43, 6);
            CheckBox2.Name = "CheckBox2";
            CheckBox2.Size = new Size(767, 24);
            CheckBox2.TabIndex = 8;
            CheckBox2.Text = "Automatic restart explorer every time I apply a theme";
            // 
            // PictureBox7
            // 
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(13, 6);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(24, 24);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 5;
            PictureBox7.TabStop = false;
            // 
            // AlertBox3
            // 
            AlertBox3.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox3.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox3.CenterText = false;
            AlertBox3.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox3.Font = new Font("Segoe UI", 9.0f);
            AlertBox3.Image = null;
            AlertBox3.Location = new Point(52, 38);
            AlertBox3.Name = "AlertBox3";
            AlertBox3.Size = new Size(625, 22);
            AlertBox3.TabIndex = 11;
            AlertBox3.TabStop = false;
            AlertBox3.Text = "It's recommended. Don't worry; it won't close your work. If you are obsessed with" + " this, save your work first.";
            // 
            // AlertBox6
            // 
            AlertBox6.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox6.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox6.CenterText = false;
            AlertBox6.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox6.Font = new Font("Segoe UI", 9.0f);
            AlertBox6.Image = null;
            AlertBox6.Location = new Point(52, 66);
            AlertBox6.Name = "AlertBox6";
            AlertBox6.Size = new Size(625, 22);
            AlertBox6.TabIndex = 22;
            AlertBox6.TabStop = false;
            AlertBox6.Text = "It is used to apply accent colors correctly in Windows 10 and 11, Start Menu for " + "Windows 8.1";
            // 
            // CheckBox17
            // 
            CheckBox17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox17.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox17.Checked = true;
            CheckBox17.Font = new Font("Segoe UI", 9.0f);
            CheckBox17.ForeColor = Color.White;
            CheckBox17.Location = new Point(43, 98);
            CheckBox17.Name = "CheckBox17";
            CheckBox17.Size = new Size(767, 24);
            CheckBox17.TabIndex = 24;
            CheckBox17.Text = "Always show confirmation message on closing WinPaletter (If changes happened)";
            // 
            // PictureBox37
            // 
            PictureBox37.Image = (Image)resources.GetObject("PictureBox37.Image");
            PictureBox37.Location = new Point(13, 98);
            PictureBox37.Name = "PictureBox37";
            PictureBox37.Size = new Size(24, 24);
            PictureBox37.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox37.TabIndex = 23;
            PictureBox37.TabStop = false;
            // 
            // TabPage12
            // 
            TabPage12.BackColor = Color.FromArgb(25, 25, 25);
            TabPage12.Controls.Add(AlertBox5);
            TabPage12.Controls.Add(CheckBox24);
            TabPage12.Controls.Add(CheckBox23);
            TabPage12.Controls.Add(Panel5);
            TabPage12.Controls.Add(PictureBox51);
            TabPage12.Controls.Add(Panel4);
            TabPage12.Controls.Add(Label36);
            TabPage12.Location = new Point(4, 34);
            TabPage12.Name = "TabPage12";
            TabPage12.Size = new Size(815, 382);
            TabPage12.TabIndex = 1;
            TabPage12.Text = "Classic Colors";
            // 
            // AlertBox5
            // 
            AlertBox5.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox5.BackColor = Color.FromArgb(44, 52, 35);
            AlertBox5.CenterText = false;
            AlertBox5.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox5.Font = new Font("Segoe UI", 9.0f);
            AlertBox5.Image = (Image)resources.GetObject("AlertBox5.Image");
            AlertBox5.Location = new Point(56, 204);
            AlertBox5.Name = "AlertBox5";
            AlertBox5.Size = new Size(723, 30);
            AlertBox5.TabIndex = 39;
            AlertBox5.TabStop = false;
            AlertBox5.Text = "Writing all previous values to registry without Adminstrator rights will take tim" + "e more than usual";
            // 
            // CheckBox24
            // 
            CheckBox24.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox24.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CheckBox24.Location = new Point(56, 97);
            CheckBox24.Name = "CheckBox24";
            CheckBox24.Size = new Size(754, 35);
            CheckBox24.TabIndex = 38;
            CheckBox24.Text = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors" + @"\Standard (For LogonUI in Windows 8.1 and later)";
            CheckBox24.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox23
            // 
            CheckBox23.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox23.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CheckBox23.Location = new Point(56, 31);
            CheckBox23.Name = "CheckBox23";
            CheckBox23.Size = new Size(754, 26);
            CheckBox23.TabIndex = 37;
            CheckBox23.Text = @"HKEY_USERS\.DEFAULT\Control Panel\Colors (For all users && LogonUI)";
            CheckBox23.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel5
            // 
            Panel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel5.Controls.Add(RadioButton10);
            Panel5.Controls.Add(RadioButton9);
            Panel5.Controls.Add(RadioButton7);
            Panel5.Controls.Add(RadioButton8);
            Panel5.Location = new Point(56, 135);
            Panel5.Name = "Panel5";
            Panel5.Size = new Size(723, 61);
            Panel5.TabIndex = 36;
            // 
            // RadioButton10
            // 
            RadioButton10.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton10.Checked = false;
            RadioButton10.Font = new Font("Segoe UI", 9.0f);
            RadioButton10.ForeColor = Color.White;
            RadioButton10.Location = new Point(328, 33);
            RadioButton10.Name = "RadioButton10";
            RadioButton10.Size = new Size(272, 24);
            RadioButton10.TabIndex = 3;
            RadioButton10.Text = "Don't change";
            // 
            // RadioButton9
            // 
            RadioButton9.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton9.Checked = false;
            RadioButton9.Font = new Font("Segoe UI", 9.0f);
            RadioButton9.ForeColor = Color.White;
            RadioButton9.Location = new Point(328, 3);
            RadioButton9.Name = "RadioButton9";
            RadioButton9.Size = new Size(272, 24);
            RadioButton9.TabIndex = 2;
            RadioButton9.Text = "Restore defaults";
            // 
            // RadioButton7
            // 
            RadioButton7.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton7.Checked = true;
            RadioButton7.Font = new Font("Segoe UI", 9.0f);
            RadioButton7.ForeColor = Color.White;
            RadioButton7.Location = new Point(19, 33);
            RadioButton7.Name = "RadioButton7";
            RadioButton7.Size = new Size(272, 24);
            RadioButton7.TabIndex = 1;
            RadioButton7.Text = "Erase (Remove)";
            // 
            // RadioButton8
            // 
            RadioButton8.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton8.Checked = false;
            RadioButton8.Font = new Font("Segoe UI", 9.0f);
            RadioButton8.ForeColor = Color.White;
            RadioButton8.Location = new Point(19, 3);
            RadioButton8.Name = "RadioButton8";
            RadioButton8.Size = new Size(272, 24);
            RadioButton8.TabIndex = 0;
            RadioButton8.Text = "Overwrite";
            // 
            // PictureBox51
            // 
            PictureBox51.Image = (Image)resources.GetObject("PictureBox51.Image");
            PictureBox51.Location = new Point(13, 6);
            PictureBox51.Name = "PictureBox51";
            PictureBox51.Size = new Size(24, 24);
            PictureBox51.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox51.TabIndex = 27;
            PictureBox51.TabStop = false;
            // 
            // Panel4
            // 
            Panel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel4.Controls.Add(RadioButton6);
            Panel4.Controls.Add(RadioButton5);
            Panel4.Location = new Point(56, 59);
            Panel4.Name = "Panel4";
            Panel4.Size = new Size(723, 32);
            Panel4.TabIndex = 35;
            // 
            // RadioButton6
            // 
            RadioButton6.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton6.Checked = false;
            RadioButton6.Font = new Font("Segoe UI", 9.0f);
            RadioButton6.ForeColor = Color.White;
            RadioButton6.Location = new Point(328, 3);
            RadioButton6.Name = "RadioButton6";
            RadioButton6.Size = new Size(272, 24);
            RadioButton6.TabIndex = 1;
            RadioButton6.Text = "Don't change";
            // 
            // RadioButton5
            // 
            RadioButton5.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton5.Checked = true;
            RadioButton5.Font = new Font("Segoe UI", 9.0f);
            RadioButton5.ForeColor = Color.White;
            RadioButton5.Location = new Point(19, 3);
            RadioButton5.Name = "RadioButton5";
            RadioButton5.Size = new Size(272, 24);
            RadioButton5.TabIndex = 0;
            RadioButton5.Text = "Overwrite";
            // 
            // Label36
            // 
            Label36.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label36.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label36.Location = new Point(43, 6);
            Label36.Name = "Label36";
            Label36.Size = new Size(767, 24);
            Label36.TabIndex = 28;
            Label36.Text = "On applying classic colors:";
            Label36.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage13
            // 
            TabPage13.BackColor = Color.FromArgb(25, 25, 25);
            TabPage13.Controls.Add(AlertBox14);
            TabPage13.Controls.Add(PictureBox55);
            TabPage13.Controls.Add(Label39);
            TabPage13.Controls.Add(Panel6);
            TabPage13.Controls.Add(Label40);
            TabPage13.Controls.Add(PictureBox50);
            TabPage13.Controls.Add(CheckBox22);
            TabPage13.Location = new Point(4, 34);
            TabPage13.Name = "TabPage13";
            TabPage13.Size = new Size(815, 382);
            TabPage13.TabIndex = 2;
            TabPage13.Text = "Metrics and Fonts";
            // 
            // AlertBox14
            // 
            AlertBox14.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox14.BackColor = Color.FromArgb(46, 55, 36);
            AlertBox14.CenterText = false;
            AlertBox14.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox14.Font = new Font("Segoe UI", 9.0f);
            AlertBox14.Image = (Image)resources.GetObject("AlertBox14.Image");
            AlertBox14.Location = new Point(56, 97);
            AlertBox14.Name = "AlertBox14";
            AlertBox14.Size = new Size(723, 30);
            AlertBox14.TabIndex = 43;
            AlertBox14.TabStop = false;
            AlertBox14.Text = "Writing this value to registry without Adminstrator rights will take time more th" + "an usual";
            // 
            // PictureBox55
            // 
            PictureBox55.Image = (Image)resources.GetObject("PictureBox55.Image");
            PictureBox55.Location = new Point(13, 6);
            PictureBox55.Name = "PictureBox55";
            PictureBox55.Size = new Size(24, 24);
            PictureBox55.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox55.TabIndex = 42;
            PictureBox55.TabStop = false;
            // 
            // Label39
            // 
            Label39.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label39.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label39.Location = new Point(56, 31);
            Label39.Name = "Label39";
            Label39.Size = new Size(754, 26);
            Label39.TabIndex = 41;
            Label39.Text = @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics (For all users && LogonUI" + ")";
            Label39.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel6
            // 
            Panel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel6.Controls.Add(RadioButton11);
            Panel6.Controls.Add(RadioButton12);
            Panel6.Location = new Point(56, 59);
            Panel6.Name = "Panel6";
            Panel6.Size = new Size(723, 32);
            Panel6.TabIndex = 40;
            // 
            // RadioButton11
            // 
            RadioButton11.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton11.Checked = true;
            RadioButton11.Font = new Font("Segoe UI", 9.0f);
            RadioButton11.ForeColor = Color.White;
            RadioButton11.Location = new Point(328, 3);
            RadioButton11.Name = "RadioButton11";
            RadioButton11.Size = new Size(272, 24);
            RadioButton11.TabIndex = 1;
            RadioButton11.Text = "Don't change";
            // 
            // RadioButton12
            // 
            RadioButton12.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton12.Checked = false;
            RadioButton12.Font = new Font("Segoe UI", 9.0f);
            RadioButton12.ForeColor = Color.White;
            RadioButton12.Location = new Point(19, 3);
            RadioButton12.Name = "RadioButton12";
            RadioButton12.Size = new Size(272, 24);
            RadioButton12.TabIndex = 0;
            RadioButton12.Text = "Overwrite";
            // 
            // Label40
            // 
            Label40.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label40.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label40.Location = new Point(43, 6);
            Label40.Name = "Label40";
            Label40.Size = new Size(767, 24);
            Label40.TabIndex = 39;
            Label40.Text = "On applying Metrics && Fonts:";
            Label40.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox50
            // 
            PictureBox50.Image = (Image)resources.GetObject("PictureBox50.Image");
            PictureBox50.Location = new Point(13, 137);
            PictureBox50.Name = "PictureBox50";
            PictureBox50.Size = new Size(24, 24);
            PictureBox50.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox50.TabIndex = 25;
            PictureBox50.TabStop = false;
            // 
            // CheckBox22
            // 
            CheckBox22.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox22.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox22.Checked = false;
            CheckBox22.Font = new Font("Segoe UI", 9.0f);
            CheckBox22.ForeColor = Color.White;
            CheckBox22.Location = new Point(43, 137);
            CheckBox22.Name = "CheckBox22";
            CheckBox22.Size = new Size(767, 24);
            CheckBox22.TabIndex = 26;
            CheckBox22.Text = "Make applying Metrics & Fonts have effects only after logoff and logon (To fix it" + "s crashing issue)";
            // 
            // TabPage14
            // 
            TabPage14.BackColor = Color.FromArgb(25, 25, 25);
            TabPage14.Controls.Add(PictureBox35);
            TabPage14.Controls.Add(AlertBox15);
            TabPage14.Controls.Add(CheckBox16);
            TabPage14.Controls.Add(PictureBox56);
            TabPage14.Controls.Add(Label41);
            TabPage14.Controls.Add(Panel7);
            TabPage14.Controls.Add(Label42);
            TabPage14.Controls.Add(PictureBox14);
            TabPage14.Controls.Add(CheckBox7);
            TabPage14.Location = new Point(4, 34);
            TabPage14.Name = "TabPage14";
            TabPage14.Size = new Size(815, 382);
            TabPage14.TabIndex = 3;
            TabPage14.Text = "Cursors";
            // 
            // PictureBox35
            // 
            PictureBox35.Image = (Image)resources.GetObject("PictureBox35.Image");
            PictureBox35.Location = new Point(13, 167);
            PictureBox35.Name = "PictureBox35";
            PictureBox35.Size = new Size(24, 24);
            PictureBox35.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox35.TabIndex = 25;
            PictureBox35.TabStop = false;
            // 
            // AlertBox15
            // 
            AlertBox15.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox15.BackColor = Color.FromArgb(46, 55, 36);
            AlertBox15.CenterText = false;
            AlertBox15.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox15.Font = new Font("Segoe UI", 9.0f);
            AlertBox15.Image = (Image)resources.GetObject("AlertBox15.Image");
            AlertBox15.Location = new Point(56, 97);
            AlertBox15.Name = "AlertBox15";
            AlertBox15.Size = new Size(723, 30);
            AlertBox15.TabIndex = 50;
            AlertBox15.TabStop = false;
            AlertBox15.Text = "Writing this value to registry without Adminstrator rights will take time more th" + "an usual";
            // 
            // CheckBox16
            // 
            CheckBox16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox16.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox16.Checked = false;
            CheckBox16.Font = new Font("Segoe UI", 9.0f);
            CheckBox16.ForeColor = Color.White;
            CheckBox16.Location = new Point(43, 167);
            CheckBox16.Name = "CheckBox16";
            CheckBox16.Size = new Size(767, 24);
            CheckBox16.TabIndex = 26;
            CheckBox16.Text = "If Cursors Applying is disabled or skipped, automatic switch the cursors scheme t" + "o Windows Default \"Aero\"";
            // 
            // PictureBox56
            // 
            PictureBox56.Image = (Image)resources.GetObject("PictureBox56.Image");
            PictureBox56.Location = new Point(13, 6);
            PictureBox56.Name = "PictureBox56";
            PictureBox56.Size = new Size(24, 24);
            PictureBox56.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox56.TabIndex = 49;
            PictureBox56.TabStop = false;
            // 
            // Label41
            // 
            Label41.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label41.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label41.Location = new Point(56, 31);
            Label41.Name = "Label41";
            Label41.Size = new Size(754, 26);
            Label41.TabIndex = 48;
            Label41.Text = @"HKEY_USERS\.DEFAULT\Control Panel\Cursors (For all users && LogonUI)";
            Label41.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel7
            // 
            Panel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel7.Controls.Add(RadioButton13);
            Panel7.Controls.Add(RadioButton14);
            Panel7.Location = new Point(56, 59);
            Panel7.Name = "Panel7";
            Panel7.Size = new Size(723, 32);
            Panel7.TabIndex = 47;
            // 
            // RadioButton13
            // 
            RadioButton13.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton13.Checked = true;
            RadioButton13.Font = new Font("Segoe UI", 9.0f);
            RadioButton13.ForeColor = Color.White;
            RadioButton13.Location = new Point(328, 3);
            RadioButton13.Name = "RadioButton13";
            RadioButton13.Size = new Size(272, 24);
            RadioButton13.TabIndex = 1;
            RadioButton13.Text = "Don't change";
            // 
            // RadioButton14
            // 
            RadioButton14.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton14.Checked = false;
            RadioButton14.Font = new Font("Segoe UI", 9.0f);
            RadioButton14.ForeColor = Color.White;
            RadioButton14.Location = new Point(19, 3);
            RadioButton14.Name = "RadioButton14";
            RadioButton14.Size = new Size(272, 24);
            RadioButton14.TabIndex = 0;
            RadioButton14.Text = "Overwrite";
            // 
            // Label42
            // 
            Label42.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label42.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label42.Location = new Point(43, 6);
            Label42.Name = "Label42";
            Label42.Size = new Size(767, 24);
            Label42.TabIndex = 46;
            Label42.Text = "On applying Cursors:";
            Label42.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox14
            // 
            PictureBox14.Image = (Image)resources.GetObject("PictureBox14.Image");
            PictureBox14.Location = new Point(13, 137);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox14.TabIndex = 20;
            PictureBox14.TabStop = false;
            // 
            // CheckBox7
            // 
            CheckBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox7.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox7.Checked = true;
            CheckBox7.Font = new Font("Segoe UI", 9.0f);
            CheckBox7.ForeColor = Color.White;
            CheckBox7.Location = new Point(43, 137);
            CheckBox7.Name = "CheckBox7";
            CheckBox7.Size = new Size(767, 24);
            CheckBox7.TabIndex = 21;
            CheckBox7.Text = "Automatic apply custom cursors (Creates new scheme called WinPaletter in Control " + "Panel > Mouse)";
            // 
            // TabPage10
            // 
            TabPage10.BackColor = Color.FromArgb(25, 25, 25);
            TabPage10.Controls.Add(PictureBox34);
            TabPage10.Controls.Add(CheckBox15);
            TabPage10.Controls.Add(AlertBox16);
            TabPage10.Controls.Add(PictureBox60);
            TabPage10.Controls.Add(Label48);
            TabPage10.Controls.Add(Panel10);
            TabPage10.Controls.Add(Label49);
            TabPage10.Controls.Add(PictureBox59);
            TabPage10.Controls.Add(Label46);
            TabPage10.Controls.Add(Panel9);
            TabPage10.Controls.Add(Label47);
            TabPage10.Controls.Add(PictureBox52);
            TabPage10.Controls.Add(Label35);
            TabPage10.Controls.Add(Panel8);
            TabPage10.Controls.Add(Label44);
            TabPage10.Location = new Point(4, 34);
            TabPage10.Name = "TabPage10";
            TabPage10.Size = new Size(815, 382);
            TabPage10.TabIndex = 4;
            TabPage10.Text = "Consoles";
            // 
            // PictureBox34
            // 
            PictureBox34.Image = (Image)resources.GetObject("PictureBox34.Image");
            PictureBox34.Location = new Point(13, 331);
            PictureBox34.Name = "PictureBox34";
            PictureBox34.Size = new Size(24, 24);
            PictureBox34.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox34.TabIndex = 196;
            PictureBox34.TabStop = false;
            // 
            // CheckBox15
            // 
            CheckBox15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox15.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox15.Checked = false;
            CheckBox15.Font = new Font("Segoe UI", 9.0f);
            CheckBox15.ForeColor = Color.White;
            CheckBox15.Location = new Point(43, 331);
            CheckBox15.Name = "CheckBox15";
            CheckBox15.Size = new Size(767, 24);
            CheckBox15.TabIndex = 197;
            CheckBox15.Text = "Override Command Prompt custom user preferences (Manually edited preferences)";
            // 
            // AlertBox16
            // 
            AlertBox16.AlertStyle = UI.WP.AlertBox.Style.Adaptive;
            AlertBox16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox16.BackColor = Color.FromArgb(46, 55, 36);
            AlertBox16.CenterText = false;
            AlertBox16.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox16.Font = new Font("Segoe UI", 9.0f);
            AlertBox16.Image = (Image)resources.GetObject("AlertBox16.Image");
            AlertBox16.Location = new Point(56, 292);
            AlertBox16.Name = "AlertBox16";
            AlertBox16.Size = new Size(724, 30);
            AlertBox16.TabIndex = 68;
            AlertBox16.TabStop = false;
            AlertBox16.Text = "Writing all previous values to registry without Adminstrator rights will take tim" + "e more than usual";
            // 
            // PictureBox60
            // 
            PictureBox60.Image = (Image)resources.GetObject("PictureBox60.Image");
            PictureBox60.Location = new Point(13, 190);
            PictureBox60.Name = "PictureBox60";
            PictureBox60.Size = new Size(24, 24);
            PictureBox60.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox60.TabIndex = 67;
            PictureBox60.TabStop = false;
            // 
            // Label48
            // 
            Label48.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label48.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label48.Location = new Point(56, 215);
            Label48.Name = "Label48";
            Label48.Size = new Size(754, 36);
            Label48.TabIndex = 66;
            Label48.Text = @"HKEY_USERS\.DEFAULT\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershe" + "ll.exe (For all users && LogonUI)";
            Label48.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel10
            // 
            Panel10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel10.Controls.Add(RadioButton19);
            Panel10.Controls.Add(RadioButton20);
            Panel10.Location = new Point(56, 254);
            Panel10.Name = "Panel10";
            Panel10.Size = new Size(723, 32);
            Panel10.TabIndex = 65;
            // 
            // RadioButton19
            // 
            RadioButton19.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton19.Checked = true;
            RadioButton19.Font = new Font("Segoe UI", 9.0f);
            RadioButton19.ForeColor = Color.White;
            RadioButton19.Location = new Point(328, 3);
            RadioButton19.Name = "RadioButton19";
            RadioButton19.Size = new Size(272, 24);
            RadioButton19.TabIndex = 1;
            RadioButton19.Text = "Don't change";
            // 
            // RadioButton20
            // 
            RadioButton20.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton20.Checked = false;
            RadioButton20.Font = new Font("Segoe UI", 9.0f);
            RadioButton20.ForeColor = Color.White;
            RadioButton20.Location = new Point(19, 3);
            RadioButton20.Name = "RadioButton20";
            RadioButton20.Size = new Size(272, 24);
            RadioButton20.TabIndex = 0;
            RadioButton20.Text = "Overwrite";
            // 
            // Label49
            // 
            Label49.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label49.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label49.Location = new Point(43, 190);
            Label49.Name = "Label49";
            Label49.Size = new Size(767, 24);
            Label49.TabIndex = 64;
            Label49.Text = "On applying PowerShell x64:";
            Label49.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox59
            // 
            PictureBox59.Image = (Image)resources.GetObject("PictureBox59.Image");
            PictureBox59.Location = new Point(13, 98);
            PictureBox59.Name = "PictureBox59";
            PictureBox59.Size = new Size(24, 24);
            PictureBox59.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox59.TabIndex = 63;
            PictureBox59.TabStop = false;
            // 
            // Label46
            // 
            Label46.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label46.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label46.Location = new Point(56, 123);
            Label46.Name = "Label46";
            Label46.Size = new Size(754, 26);
            Label46.TabIndex = 62;
            Label46.Text = @"HKEY_USERS\.DEFAULT\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershe" + "ll.exe (For all users && LogonUI)";
            Label46.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel9
            // 
            Panel9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel9.Controls.Add(RadioButton17);
            Panel9.Controls.Add(RadioButton18);
            Panel9.Location = new Point(56, 151);
            Panel9.Name = "Panel9";
            Panel9.Size = new Size(723, 32);
            Panel9.TabIndex = 61;
            // 
            // RadioButton17
            // 
            RadioButton17.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton17.Checked = true;
            RadioButton17.Font = new Font("Segoe UI", 9.0f);
            RadioButton17.ForeColor = Color.White;
            RadioButton17.Location = new Point(328, 3);
            RadioButton17.Name = "RadioButton17";
            RadioButton17.Size = new Size(272, 24);
            RadioButton17.TabIndex = 1;
            RadioButton17.Text = "Don't change";
            // 
            // RadioButton18
            // 
            RadioButton18.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton18.Checked = false;
            RadioButton18.Font = new Font("Segoe UI", 9.0f);
            RadioButton18.ForeColor = Color.White;
            RadioButton18.Location = new Point(19, 3);
            RadioButton18.Name = "RadioButton18";
            RadioButton18.Size = new Size(272, 24);
            RadioButton18.TabIndex = 0;
            RadioButton18.Text = "Overwrite";
            // 
            // Label47
            // 
            Label47.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label47.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label47.Location = new Point(43, 98);
            Label47.Name = "Label47";
            Label47.Size = new Size(767, 24);
            Label47.TabIndex = 60;
            Label47.Text = "On applying PowerShell x86:";
            Label47.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox52
            // 
            PictureBox52.Image = (Image)resources.GetObject("PictureBox52.Image");
            PictureBox52.Location = new Point(13, 6);
            PictureBox52.Name = "PictureBox52";
            PictureBox52.Size = new Size(24, 24);
            PictureBox52.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox52.TabIndex = 57;
            PictureBox52.TabStop = false;
            // 
            // Label35
            // 
            Label35.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label35.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label35.Location = new Point(56, 31);
            Label35.Name = "Label35";
            Label35.Size = new Size(754, 26);
            Label35.TabIndex = 56;
            Label35.Text = @"HKEY_USERS\.DEFAULT\Console (For all users && LogonUI)";
            Label35.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel8
            // 
            Panel8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel8.Controls.Add(RadioButton15);
            Panel8.Controls.Add(RadioButton16);
            Panel8.Location = new Point(56, 59);
            Panel8.Name = "Panel8";
            Panel8.Size = new Size(723, 32);
            Panel8.TabIndex = 55;
            // 
            // RadioButton15
            // 
            RadioButton15.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton15.Checked = true;
            RadioButton15.Font = new Font("Segoe UI", 9.0f);
            RadioButton15.ForeColor = Color.White;
            RadioButton15.Location = new Point(328, 3);
            RadioButton15.Name = "RadioButton15";
            RadioButton15.Size = new Size(272, 24);
            RadioButton15.TabIndex = 1;
            RadioButton15.Text = "Don't change";
            // 
            // RadioButton16
            // 
            RadioButton16.BackColor = Color.FromArgb(25, 25, 25);
            RadioButton16.Checked = false;
            RadioButton16.Font = new Font("Segoe UI", 9.0f);
            RadioButton16.ForeColor = Color.White;
            RadioButton16.Location = new Point(19, 3);
            RadioButton16.Name = "RadioButton16";
            RadioButton16.Size = new Size(272, 24);
            RadioButton16.TabIndex = 0;
            RadioButton16.Text = "Overwrite";
            // 
            // Label44
            // 
            Label44.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label44.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label44.Location = new Point(43, 6);
            Label44.Name = "Label44";
            Label44.Size = new Size(767, 24);
            Label44.TabIndex = 54;
            Label44.Text = "On applying Command Prompt:";
            Label44.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage20
            // 
            TabPage20.BackColor = Color.FromArgb(24, 24, 24);
            TabPage20.Controls.Add(AlertBox7);
            TabPage20.Controls.Add(Panel12);
            TabPage20.Controls.Add(CheckBox36);
            TabPage20.Controls.Add(PictureBox67);
            TabPage20.Controls.Add(CheckBox35_SFC);
            TabPage20.Controls.Add(PictureBox66);
            TabPage20.Location = new Point(4, 34);
            TabPage20.Name = "TabPage20";
            TabPage20.Padding = new Padding(3);
            TabPage20.Size = new Size(815, 382);
            TabPage20.TabIndex = 5;
            TabPage20.Text = "PE  patching";
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
            AlertBox7.Location = new Point(47, 74);
            AlertBox7.Name = "AlertBox7";
            AlertBox7.Size = new Size(762, 60);
            AlertBox7.TabIndex = 56;
            AlertBox7.TabStop = false;
            AlertBox7.Text = resources.GetString("AlertBox7.Text");
            // 
            // Panel12
            // 
            Panel12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel12.Controls.Add(RadioButton24);
            Panel12.Controls.Add(RadioButton25);
            Panel12.Location = new Point(47, 36);
            Panel12.Name = "Panel12";
            Panel12.Size = new Size(762, 32);
            Panel12.TabIndex = 54;
            // 
            // RadioButton24
            // 
            RadioButton24.BackColor = Color.FromArgb(24, 24, 24);
            RadioButton24.Checked = false;
            RadioButton24.Font = new Font("Segoe UI", 9.0f);
            RadioButton24.ForeColor = Color.White;
            RadioButton24.Location = new Point(328, 3);
            RadioButton24.Name = "RadioButton24";
            RadioButton24.Size = new Size(272, 24);
            RadioButton24.TabIndex = 1;
            RadioButton24.Text = "Don't modify";
            // 
            // RadioButton25
            // 
            RadioButton25.BackColor = Color.FromArgb(24, 24, 24);
            RadioButton25.Checked = true;
            RadioButton25.Font = new Font("Segoe UI", 9.0f);
            RadioButton25.ForeColor = Color.White;
            RadioButton25.Location = new Point(19, 3);
            RadioButton25.Name = "RadioButton25";
            RadioButton25.Size = new Size(272, 24);
            RadioButton25.TabIndex = 0;
            RadioButton25.Text = "Modify";
            // 
            // CheckBox36
            // 
            CheckBox36.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox36.BackColor = Color.FromArgb(24, 24, 24);
            CheckBox36.Checked = false;
            CheckBox36.Font = new Font("Segoe UI", 9.0f);
            CheckBox36.ForeColor = Color.White;
            CheckBox36.Location = new Point(43, 6);
            CheckBox36.Name = "CheckBox36";
            CheckBox36.Size = new Size(767, 24);
            CheckBox36.TabIndex = 53;
            CheckBox36.Text = "Always ignore PE resources modification dialog alert and do the following action " + "on PE file resources without showing this alert";
            // 
            // PictureBox67
            // 
            PictureBox67.Image = (Image)resources.GetObject("PictureBox67.Image");
            PictureBox67.Location = new Point(13, 6);
            PictureBox67.Name = "PictureBox67";
            PictureBox67.Size = new Size(24, 24);
            PictureBox67.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox67.TabIndex = 52;
            PictureBox67.TabStop = false;
            // 
            // CheckBox35_SFC
            // 
            CheckBox35_SFC.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox35_SFC.BackColor = Color.FromArgb(24, 24, 24);
            CheckBox35_SFC.Checked = false;
            CheckBox35_SFC.Font = new Font("Segoe UI", 9.0f);
            CheckBox35_SFC.ForeColor = Color.White;
            CheckBox35_SFC.Location = new Point(43, 143);
            CheckBox35_SFC.Name = "CheckBox35_SFC";
            CheckBox35_SFC.Size = new Size(767, 24);
            CheckBox35_SFC.TabIndex = 51;
            CheckBox35_SFC.Text = "On restoring default startup sound, do a SFC scan on imageres.dll to restore its " + "integrity (health) (requires Windows restart)";
            // 
            // PictureBox66
            // 
            PictureBox66.Image = (Image)resources.GetObject("PictureBox66.Image");
            PictureBox66.Location = new Point(13, 143);
            PictureBox66.Name = "PictureBox66";
            PictureBox66.Size = new Size(24, 24);
            PictureBox66.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox66.TabIndex = 50;
            PictureBox66.TabStop = false;
            // 
            // PictureBox8
            // 
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(6, 6);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(35, 35);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 3;
            PictureBox8.TabStop = false;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(47, 6);
            Label2.Name = "Label2";
            Label2.Size = new Size(767, 35);
            Label2.TabIndex = 2;
            Label2.Text = "Theme applying behavior";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Separator6
            // 
            Separator6.AlternativeLook = false;
            Separator6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator6.Location = new Point(6, 47);
            Separator6.Name = "Separator6";
            Separator6.Size = new Size(808, 1);
            Separator6.TabIndex = 19;
            Separator6.TabStop = false;
            // 
            // TabPage15
            // 
            TabPage15.BackColor = Color.FromArgb(25, 25, 25);
            TabPage15.Controls.Add(Separator15);
            TabPage15.Controls.Add(PictureBox63);
            TabPage15.Controls.Add(Label52);
            TabPage15.Controls.Add(TabControl3);
            TabPage15.Location = new Point(199, 4);
            TabPage15.Name = "TabPage15";
            TabPage15.Padding = new Padding(3);
            TabPage15.Size = new Size(823, 471);
            TabPage15.TabIndex = 10;
            TabPage15.Text = "Store";
            // 
            // Separator15
            // 
            Separator15.AlternativeLook = false;
            Separator15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator15.Location = new Point(6, 47);
            Separator15.Name = "Separator15";
            Separator15.Size = new Size(811, 1);
            Separator15.TabIndex = 22;
            Separator15.TabStop = false;
            // 
            // PictureBox63
            // 
            PictureBox63.Image = (Image)resources.GetObject("PictureBox63.Image");
            PictureBox63.Location = new Point(6, 6);
            PictureBox63.Name = "PictureBox63";
            PictureBox63.Size = new Size(35, 35);
            PictureBox63.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox63.TabIndex = 21;
            PictureBox63.TabStop = false;
            // 
            // Label52
            // 
            Label52.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label52.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label52.Location = new Point(47, 6);
            Label52.Name = "Label52";
            Label52.Size = new Size(770, 35);
            Label52.TabIndex = 20;
            Label52.Text = "Store";
            Label52.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabControl3
            // 
            TabControl3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabControl3.Controls.Add(TabPage16);
            TabControl3.Controls.Add(TabPage17);
            TabControl3.Controls.Add(TabPage18);
            TabControl3.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl3.Font = new Font("Segoe UI", 9.0f);
            TabControl3.ItemSize = new Size(115, 30);
            TabControl3.LineColor = Color.FromArgb(0, 81, 210);
            TabControl3.Location = new Point(0, 49);
            TabControl3.Name = "TabControl3";
            TabControl3.SelectedIndex = 0;
            TabControl3.Size = new Size(823, 420);
            TabControl3.SizeMode = TabSizeMode.Fixed;
            TabControl3.TabIndex = 216;
            // 
            // TabPage16
            // 
            TabPage16.BackColor = Color.FromArgb(25, 25, 25);
            TabPage16.Controls.Add(PictureBox2);
            TabPage16.Controls.Add(CheckBox4);
            TabPage16.Controls.Add(PictureBox54);
            TabPage16.Controls.Add(CheckBox29);
            TabPage16.Controls.Add(Separator16);
            TabPage16.Controls.Add(PictureBox64);
            TabPage16.Controls.Add(Button17);
            TabPage16.Controls.Add(Label53);
            TabPage16.Controls.Add(Button18);
            TabPage16.Controls.Add(RadioImage1);
            TabPage16.Controls.Add(ListBox2);
            TabPage16.Controls.Add(RadioImage2);
            TabPage16.Controls.Add(Button15);
            TabPage16.Controls.Add(Label54);
            TabPage16.Controls.Add(Button14);
            TabPage16.Controls.Add(Label55);
            TabPage16.Controls.Add(ListBox1);
            TabPage16.Location = new Point(4, 34);
            TabPage16.Name = "TabPage16";
            TabPage16.Size = new Size(815, 382);
            TabPage16.TabIndex = 0;
            TabPage16.Text = "Sources";
            // 
            // PictureBox2
            // 
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(13, 292);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 67;
            PictureBox2.TabStop = false;
            // 
            // CheckBox4
            // 
            CheckBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox4.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox4.Checked = true;
            CheckBox4.Font = new Font("Segoe UI", 9.0f);
            CheckBox4.ForeColor = Color.White;
            CheckBox4.Location = new Point(43, 293);
            CheckBox4.Name = "CheckBox4";
            CheckBox4.Size = new Size(766, 23);
            CheckBox4.TabIndex = 66;
            CheckBox4.Text = "Always show tips on opening WinPaletter Store";
            // 
            // PictureBox54
            // 
            PictureBox54.Image = (Image)resources.GetObject("PictureBox54.Image");
            PictureBox54.Location = new Point(119, 250);
            PictureBox54.Name = "PictureBox54";
            PictureBox54.Size = new Size(24, 24);
            PictureBox54.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox54.TabIndex = 65;
            PictureBox54.TabStop = false;
            // 
            // CheckBox29
            // 
            CheckBox29.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox29.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox29.Checked = true;
            CheckBox29.Font = new Font("Segoe UI", 9.0f);
            CheckBox29.ForeColor = Color.White;
            CheckBox29.Location = new Point(149, 251);
            CheckBox29.Name = "CheckBox29";
            CheckBox29.Size = new Size(660, 23);
            CheckBox29.TabIndex = 64;
            CheckBox29.Text = "Make WinPaletter Store get themes files from all subfolders too";
            // 
            // Separator16
            // 
            Separator16.AlternativeLook = false;
            Separator16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator16.Location = new Point(40, 141);
            Separator16.Name = "Separator16";
            Separator16.Size = new Size(769, 1);
            Separator16.TabIndex = 63;
            Separator16.TabStop = false;
            // 
            // PictureBox64
            // 
            PictureBox64.Image = (Image)resources.GetObject("PictureBox64.Image");
            PictureBox64.Location = new Point(13, 6);
            PictureBox64.Name = "PictureBox64";
            PictureBox64.Size = new Size(24, 24);
            PictureBox64.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox64.TabIndex = 47;
            PictureBox64.TabStop = false;
            // 
            // Button17
            // 
            Button17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button17.BackColor = Color.FromArgb(34, 34, 34);
            Button17.DrawOnGlass = false;
            Button17.Font = new Font("Segoe UI", 9.0f);
            Button17.ForeColor = Color.White;
            Button17.Image = (Image)resources.GetObject("Button17.Image");
            Button17.LineColor = Color.FromArgb(151, 44, 52);
            Button17.Location = new Point(785, 201);
            Button17.Name = "Button17";
            Button17.Size = new Size(24, 44);
            Button17.TabIndex = 62;
            Button17.UseVisualStyleBackColor = false;
            // 
            // Label53
            // 
            Label53.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label53.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label53.Location = new Point(40, 6);
            Label53.Name = "Label53";
            Label53.Size = new Size(769, 24);
            Label53.TabIndex = 48;
            Label53.Text = "Select a source and edit it:";
            Label53.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button18
            // 
            Button18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button18.BackColor = Color.FromArgb(34, 34, 34);
            Button18.DrawOnGlass = false;
            Button18.Font = new Font("Segoe UI", 9.0f);
            Button18.ForeColor = Color.White;
            Button18.Image = (Image)resources.GetObject("Button18.Image");
            Button18.LineColor = Color.FromArgb(29, 103, 64);
            Button18.Location = new Point(785, 152);
            Button18.Name = "Button18";
            Button18.Size = new Size(24, 44);
            Button18.TabIndex = 61;
            Button18.UseVisualStyleBackColor = false;
            // 
            // RadioImage1
            // 
            RadioImage1.Checked = true;
            RadioImage1.Font = new Font("Segoe UI", 9.0f);
            RadioImage1.ForeColor = Color.White;
            RadioImage1.Image = (Image)resources.GetObject("RadioImage1.Image");
            RadioImage1.Location = new Point(41, 40);
            RadioImage1.Name = "RadioImage1";
            RadioImage1.ShowText = true;
            RadioImage1.Size = new Size(64, 64);
            RadioImage1.TabIndex = 49;
            RadioImage1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListBox2
            // 
            ListBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ListBox2.BackColor = Color.FromArgb(30, 30, 30);
            ListBox2.BorderStyle = BorderStyle.FixedSingle;
            ListBox2.ForeColor = Color.White;
            ListBox2.FormattingEnabled = true;
            ListBox2.ItemHeight = 15;
            ListBox2.Location = new Point(119, 152);
            ListBox2.Name = "ListBox2";
            ListBox2.Size = new Size(660, 92);
            ListBox2.TabIndex = 60;
            // 
            // RadioImage2
            // 
            RadioImage2.Checked = false;
            RadioImage2.Font = new Font("Segoe UI", 9.0f);
            RadioImage2.ForeColor = Color.White;
            RadioImage2.Image = (Image)resources.GetObject("RadioImage2.Image");
            RadioImage2.Location = new Point(41, 152);
            RadioImage2.Name = "RadioImage2";
            RadioImage2.ShowText = true;
            RadioImage2.Size = new Size(64, 64);
            RadioImage2.TabIndex = 50;
            RadioImage2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button15
            // 
            Button15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button15.BackColor = Color.FromArgb(34, 34, 34);
            Button15.DrawOnGlass = false;
            Button15.Font = new Font("Segoe UI", 9.0f);
            Button15.ForeColor = Color.White;
            Button15.Image = (Image)resources.GetObject("Button15.Image");
            Button15.LineColor = Color.FromArgb(151, 44, 52);
            Button15.Location = new Point(785, 89);
            Button15.Name = "Button15";
            Button15.Size = new Size(24, 44);
            Button15.TabIndex = 57;
            Button15.UseVisualStyleBackColor = false;
            // 
            // Label54
            // 
            Label54.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label54.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label54.Location = new Point(40, 107);
            Label54.Name = "Label54";
            Label54.Size = new Size(67, 25);
            Label54.TabIndex = 51;
            Label54.Text = "Online";
            Label54.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(34, 34, 34);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = (Image)resources.GetObject("Button14.Image");
            Button14.LineColor = Color.FromArgb(29, 103, 64);
            Button14.Location = new Point(785, 40);
            Button14.Name = "Button14";
            Button14.Size = new Size(24, 44);
            Button14.TabIndex = 56;
            Button14.UseVisualStyleBackColor = false;
            // 
            // Label55
            // 
            Label55.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label55.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label55.Location = new Point(41, 219);
            Label55.Name = "Label55";
            Label55.Size = new Size(67, 25);
            Label55.TabIndex = 52;
            Label55.Text = "Offline";
            Label55.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListBox1
            // 
            ListBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ListBox1.BackColor = Color.FromArgb(30, 30, 30);
            ListBox1.BorderStyle = BorderStyle.FixedSingle;
            ListBox1.ForeColor = Color.White;
            ListBox1.FormattingEnabled = true;
            ListBox1.ItemHeight = 15;
            ListBox1.Location = new Point(119, 40);
            ListBox1.Name = "ListBox1";
            ListBox1.Size = new Size(660, 92);
            ListBox1.TabIndex = 55;
            // 
            // TabPage17
            // 
            TabPage17.BackColor = Color.FromArgb(25, 25, 25);
            TabPage17.Controls.Add(Button20);
            TabPage17.Controls.Add(Label43);
            TabPage17.Controls.Add(PictureBox57);
            TabPage17.Controls.Add(Label45);
            TabPage17.Controls.Add(AlertBox20);
            TabPage17.Controls.Add(Button19);
            TabPage17.Controls.Add(Label38);
            TabPage17.Controls.Add(PictureBox53);
            TabPage17.Controls.Add(Label37);
            TabPage17.ForeColor = Color.White;
            TabPage17.Location = new Point(4, 34);
            TabPage17.Name = "TabPage17";
            TabPage17.Size = new Size(815, 382);
            TabPage17.TabIndex = 1;
            TabPage17.Text = "Cache";
            // 
            // Button20
            // 
            Button20.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button20.BackColor = Color.FromArgb(34, 34, 34);
            Button20.DrawOnGlass = false;
            Button20.Font = new Font("Segoe UI", 9.0f);
            Button20.ForeColor = Color.White;
            Button20.Image = null;
            Button20.LineColor = Color.FromArgb(199, 49, 61);
            Button20.Location = new Point(732, 127);
            Button20.Name = "Button20";
            Button20.Size = new Size(80, 25);
            Button20.TabIndex = 57;
            Button20.Text = "Clean";
            Button20.UseVisualStyleBackColor = false;
            // 
            // Label43
            // 
            Label43.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label43.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label43.Location = new Point(44, 127);
            Label43.Name = "Label43";
            Label43.Size = new Size(682, 24);
            Label43.TabIndex = 56;
            Label43.Text = "0";
            Label43.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox57
            // 
            PictureBox57.Image = (Image)resources.GetObject("PictureBox57.Image");
            PictureBox57.Location = new Point(13, 102);
            PictureBox57.Name = "PictureBox57";
            PictureBox57.Size = new Size(24, 24);
            PictureBox57.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox57.TabIndex = 54;
            PictureBox57.TabStop = false;
            // 
            // Label45
            // 
            Label45.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label45.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label45.Location = new Point(40, 102);
            Label45.Name = "Label45";
            Label45.Size = new Size(772, 24);
            Label45.TabIndex = 55;
            Label45.Text = "Unpacked themes resources (including WinPaletter Store themes and other themes):";
            Label45.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AlertBox20
            // 
            AlertBox20.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox20.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox20.CenterText = false;
            AlertBox20.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox20.Font = new Font("Segoe UI", 9.0f);
            AlertBox20.Image = null;
            AlertBox20.Location = new Point(43, 60);
            AlertBox20.Name = "AlertBox20";
            AlertBox20.Size = new Size(769, 22);
            AlertBox20.TabIndex = 53;
            AlertBox20.TabStop = false;
            AlertBox20.Text = "Cleaning the cache will make WinPaletter Store redownload the themes (without the" + "mes resources packs) again";
            // 
            // Button19
            // 
            Button19.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button19.BackColor = Color.FromArgb(34, 34, 34);
            Button19.DrawOnGlass = false;
            Button19.Font = new Font("Segoe UI", 9.0f);
            Button19.ForeColor = Color.White;
            Button19.Image = null;
            Button19.LineColor = Color.FromArgb(199, 49, 61);
            Button19.Location = new Point(732, 31);
            Button19.Name = "Button19";
            Button19.Size = new Size(80, 25);
            Button19.TabIndex = 52;
            Button19.Text = "Clean";
            Button19.UseVisualStyleBackColor = false;
            // 
            // Label38
            // 
            Label38.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label38.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label38.Location = new Point(44, 31);
            Label38.Name = "Label38";
            Label38.Size = new Size(682, 24);
            Label38.TabIndex = 51;
            Label38.Text = "0";
            Label38.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox53
            // 
            PictureBox53.Image = (Image)resources.GetObject("PictureBox53.Image");
            PictureBox53.Location = new Point(13, 6);
            PictureBox53.Name = "PictureBox53";
            PictureBox53.Size = new Size(24, 24);
            PictureBox53.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox53.TabIndex = 49;
            PictureBox53.TabStop = false;
            // 
            // Label37
            // 
            Label37.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label37.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label37.Location = new Point(40, 6);
            Label37.Name = "Label37";
            Label37.Size = new Size(772, 24);
            Label37.TabIndex = 50;
            Label37.Text = "Store cache:";
            Label37.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage18
            // 
            TabPage18.BackColor = Color.FromArgb(25, 25, 25);
            TabPage18.Controls.Add(CheckBox26);
            TabPage18.Controls.Add(PictureBox65);
            TabPage18.Controls.Add(CheckBox27);
            TabPage18.Controls.Add(Label56);
            TabPage18.Controls.Add(CheckBox28);
            TabPage18.Location = new Point(4, 34);
            TabPage18.Name = "TabPage18";
            TabPage18.Size = new Size(815, 382);
            TabPage18.TabIndex = 2;
            TabPage18.Text = "Search filter";
            // 
            // CheckBox26
            // 
            CheckBox26.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox26.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox26.Checked = true;
            CheckBox26.Font = new Font("Segoe UI", 9.0f);
            CheckBox26.ForeColor = Color.White;
            CheckBox26.Location = new Point(43, 63);
            CheckBox26.Name = "CheckBox26";
            CheckBox26.Size = new Size(769, 24);
            CheckBox26.TabIndex = 215;
            CheckBox26.Text = "Themes descriptions";
            // 
            // PictureBox65
            // 
            PictureBox65.Image = (Image)resources.GetObject("PictureBox65.Image");
            PictureBox65.Location = new Point(13, 6);
            PictureBox65.Name = "PictureBox65";
            PictureBox65.Size = new Size(24, 24);
            PictureBox65.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox65.TabIndex = 64;
            PictureBox65.TabStop = false;
            // 
            // CheckBox27
            // 
            CheckBox27.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox27.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox27.Checked = true;
            CheckBox27.Font = new Font("Segoe UI", 9.0f);
            CheckBox27.ForeColor = Color.White;
            CheckBox27.Location = new Point(43, 93);
            CheckBox27.Name = "CheckBox27";
            CheckBox27.Size = new Size(769, 24);
            CheckBox27.TabIndex = 214;
            CheckBox27.Text = "Authors names";
            // 
            // Label56
            // 
            Label56.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label56.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label56.Location = new Point(40, 6);
            Label56.Name = "Label56";
            Label56.Size = new Size(772, 24);
            Label56.TabIndex = 65;
            Label56.Text = "Search through (filter):";
            Label56.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox28
            // 
            CheckBox28.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox28.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox28.Checked = true;
            CheckBox28.Font = new Font("Segoe UI", 9.0f);
            CheckBox28.ForeColor = Color.White;
            CheckBox28.Location = new Point(43, 33);
            CheckBox28.Name = "CheckBox28";
            CheckBox28.Size = new Size(769, 24);
            CheckBox28.TabIndex = 213;
            CheckBox28.Text = "Themes names";
            // 
            // TabPage8
            // 
            TabPage8.BackColor = Color.FromArgb(25, 25, 25);
            TabPage8.Controls.Add(PictureBox71);
            TabPage8.Controls.Add(CheckBox19_ShowSkippedItemsOnDetailedVerbose);
            TabPage8.Controls.Add(VL2);
            TabPage8.Controls.Add(VL1);
            TabPage8.Controls.Add(VL0);
            TabPage8.Controls.Add(Label25);
            TabPage8.Controls.Add(AlertBox_Themelog);
            TabPage8.Controls.Add(Label27);
            TabPage8.Controls.Add(NumericUpDown1);
            TabPage8.Controls.Add(CheckBox18);
            TabPage8.Controls.Add(PictureBox39);
            TabPage8.Controls.Add(PictureBox40);
            TabPage8.Controls.Add(Separator10);
            TabPage8.Controls.Add(PictureBox38);
            TabPage8.Controls.Add(Label26);
            TabPage8.Location = new Point(199, 4);
            TabPage8.Name = "TabPage8";
            TabPage8.Padding = new Padding(3);
            TabPage8.Size = new Size(823, 471);
            TabPage8.TabIndex = 8;
            TabPage8.Text = "Theme logging";
            // 
            // PictureBox71
            // 
            PictureBox71.Image = (Image)resources.GetObject("PictureBox71.Image");
            PictureBox71.Location = new Point(17, 172);
            PictureBox71.Name = "PictureBox71";
            PictureBox71.Size = new Size(24, 24);
            PictureBox71.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox71.TabIndex = 54;
            PictureBox71.TabStop = false;
            // 
            // CheckBox19_ShowSkippedItemsOnDetailedVerbose
            // 
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.Checked = false;
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.Font = new Font("Segoe UI", 9.0f);
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.ForeColor = Color.White;
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.Location = new Point(47, 172);
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.Name = "CheckBox19_ShowSkippedItemsOnDetailedVerbose";
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.Size = new Size(770, 24);
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.TabIndex = 53;
            CheckBox19_ShowSkippedItemsOnDetailedVerbose.Text = "Include skipped registry items if advanced details verbose level is selected (app" + "lying operation will take longer time)";
            // 
            // VL2
            // 
            VL2.Checked = false;
            VL2.Font = new Font("Segoe UI", 9.0f);
            VL2.ForeColor = Color.White;
            VL2.Image = null;
            VL2.Location = new Point(53, 142);
            VL2.Name = "VL2";
            VL2.ShowText = true;
            VL2.Size = new Size(515, 24);
            VL2.TabIndex = 52;
            VL2.Text = "Advanced details; shows all registry keys modified (applying operation will take " + "longer time)";
            VL2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VL1
            // 
            VL1.Checked = false;
            VL1.Font = new Font("Segoe UI", 9.0f);
            VL1.ForeColor = Color.White;
            VL1.Image = null;
            VL1.Location = new Point(53, 112);
            VL1.Name = "VL1";
            VL1.ShowText = true;
            VL1.Size = new Size(515, 24);
            VL1.TabIndex = 51;
            VL1.Text = "Basic details; shows if applying, skipping Windows aspects or errors happened";
            VL1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VL0
            // 
            VL0.Checked = false;
            VL0.Font = new Font("Segoe UI", 9.0f);
            VL0.ForeColor = Color.White;
            VL0.Image = null;
            VL0.Location = new Point(53, 82);
            VL0.Name = "VL0";
            VL0.ShowText = true;
            VL0.Size = new Size(515, 24);
            VL0.TabIndex = 50;
            VL0.Text = "None";
            VL0.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label25
            // 
            Label25.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label25.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label25.Location = new Point(47, 54);
            Label25.Name = "Label25";
            Label25.Size = new Size(770, 24);
            Label25.TabIndex = 49;
            Label25.Text = "Verbose level";
            Label25.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AlertBox_Themelog
            // 
            AlertBox_Themelog.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox_Themelog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox_Themelog.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox_Themelog.CenterText = false;
            AlertBox_Themelog.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox_Themelog.Font = new Font("Segoe UI", 9.0f);
            AlertBox_Themelog.Image = null;
            AlertBox_Themelog.Location = new Point(53, 264);
            AlertBox_Themelog.Name = "AlertBox_Themelog";
            AlertBox_Themelog.Size = new Size(580, 22);
            AlertBox_Themelog.TabIndex = 29;
            AlertBox_Themelog.TabStop = false;
            AlertBox_Themelog.Text = "If there is an error or using advanced verbose details, automatic close won't sta" + "rt so you can read the log";
            // 
            // Label27
            // 
            Label27.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label27.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label27.Location = new Point(134, 232);
            Label27.Name = "Label27";
            Label27.Size = new Size(79, 26);
            Label27.TabIndex = 28;
            Label27.Text = "seconds";
            Label27.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // NumericUpDown1
            // 
            NumericUpDown1.Font = new Font("Segoe UI", 9.0f);
            NumericUpDown1.Location = new Point(53, 232);
            NumericUpDown1.Max = 300;
            NumericUpDown1.Min = 5;
            NumericUpDown1.Name = "NumericUpDown1";
            NumericUpDown1.Size = new Size(75, 26);
            NumericUpDown1.TabIndex = 27;
            NumericUpDown1.Text = "NumericUpDown1";
            NumericUpDown1.UpDownStep = 1;
            NumericUpDown1.Value = 5;
            // 
            // CheckBox18
            // 
            CheckBox18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox18.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox18.Checked = true;
            CheckBox18.Font = new Font("Segoe UI", 9.0f);
            CheckBox18.ForeColor = Color.White;
            CheckBox18.Location = new Point(47, 202);
            CheckBox18.Name = "CheckBox18";
            CheckBox18.Size = new Size(770, 24);
            CheckBox18.TabIndex = 26;
            CheckBox18.Text = "Countdown (for automatic closure for the log)";
            // 
            // PictureBox39
            // 
            PictureBox39.Image = (Image)resources.GetObject("PictureBox39.Image");
            PictureBox39.Location = new Point(17, 54);
            PictureBox39.Name = "PictureBox39";
            PictureBox39.Size = new Size(24, 24);
            PictureBox39.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox39.TabIndex = 23;
            PictureBox39.TabStop = false;
            // 
            // PictureBox40
            // 
            PictureBox40.Image = (Image)resources.GetObject("PictureBox40.Image");
            PictureBox40.Location = new Point(17, 202);
            PictureBox40.Name = "PictureBox40";
            PictureBox40.Size = new Size(24, 24);
            PictureBox40.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox40.TabIndex = 25;
            PictureBox40.TabStop = false;
            // 
            // Separator10
            // 
            Separator10.AlternativeLook = false;
            Separator10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator10.Location = new Point(6, 47);
            Separator10.Name = "Separator10";
            Separator10.Size = new Size(811, 1);
            Separator10.TabIndex = 22;
            Separator10.TabStop = false;
            // 
            // PictureBox38
            // 
            PictureBox38.Image = (Image)resources.GetObject("PictureBox38.Image");
            PictureBox38.Location = new Point(6, 6);
            PictureBox38.Name = "PictureBox38";
            PictureBox38.Size = new Size(35, 35);
            PictureBox38.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox38.TabIndex = 21;
            PictureBox38.TabStop = false;
            // 
            // Label26
            // 
            Label26.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label26.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label26.Location = new Point(47, 6);
            Label26.Name = "Label26";
            Label26.Size = new Size(770, 35);
            Label26.TabIndex = 20;
            Label26.Text = "Theme logging";
            Label26.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(Button9);
            TabPage2.Controls.Add(Button16);
            TabPage2.Controls.Add(TextBox2);
            TabPage2.Controls.Add(TextBox1);
            TabPage2.Controls.Add(Label24);
            TabPage2.Controls.Add(Label23);
            TabPage2.Controls.Add(AlertBox13);
            TabPage2.Controls.Add(PictureBox33);
            TabPage2.Controls.Add(PictureBox32);
            TabPage2.Controls.Add(PictureBox31);
            TabPage2.Controls.Add(CheckBox14);
            TabPage2.Controls.Add(AlertBox12);
            TabPage2.Controls.Add(AlertBox11);
            TabPage2.Controls.Add(PictureBox30);
            TabPage2.Controls.Add(CheckBox13);
            TabPage2.Controls.Add(AlertBox8);
            TabPage2.Controls.Add(PictureBox29);
            TabPage2.Controls.Add(CheckBox12);
            TabPage2.Controls.Add(Separator3);
            TabPage2.Controls.Add(PictureBox28);
            TabPage2.Controls.Add(Label7);
            TabPage2.Location = new Point(199, 4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(823, 471);
            TabPage2.TabIndex = 7;
            TabPage2.Text = "Terminals";
            // 
            // Button9
            // 
            Button9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button9.BackColor = Color.FromArgb(34, 34, 34);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.LineColor = Color.FromArgb(164, 125, 25);
            Button9.Location = new Point(787, 271);
            Button9.Name = "Button9";
            Button9.Size = new Size(30, 24);
            Button9.TabIndex = 195;
            Button9.UseVisualStyleBackColor = false;
            // 
            // Button16
            // 
            Button16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button16.BackColor = Color.FromArgb(34, 34, 34);
            Button16.DrawOnGlass = false;
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = (Image)resources.GetObject("Button16.Image");
            Button16.LineColor = Color.FromArgb(164, 125, 25);
            Button16.Location = new Point(787, 241);
            Button16.Name = "Button16";
            Button16.Size = new Size(30, 24);
            Button16.TabIndex = 194;
            Button16.UseVisualStyleBackColor = false;
            // 
            // TextBox2
            // 
            TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox2.BackColor = Color.FromArgb(55, 55, 55);
            TextBox2.DrawOnGlass = false;
            TextBox2.ForeColor = Color.White;
            TextBox2.Location = new Point(151, 271);
            TextBox2.MaxLength = 32767;
            TextBox2.Multiline = false;
            TextBox2.Name = "TextBox2";
            TextBox2.ReadOnly = false;
            TextBox2.Scrollbars = ScrollBars.None;
            TextBox2.SelectedText = "";
            TextBox2.SelectionLength = 0;
            TextBox2.SelectionStart = 0;
            TextBox2.Size = new Size(630, 24);
            TextBox2.TabIndex = 39;
            TextBox2.TextAlign = HorizontalAlignment.Left;
            TextBox2.UseSystemPasswordChar = false;
            TextBox2.WordWrap = true;
            // 
            // TextBox1
            // 
            TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox1.BackColor = Color.FromArgb(55, 55, 55);
            TextBox1.DrawOnGlass = false;
            TextBox1.ForeColor = Color.White;
            TextBox1.Location = new Point(151, 241);
            TextBox1.MaxLength = 32767;
            TextBox1.Multiline = false;
            TextBox1.Name = "TextBox1";
            TextBox1.ReadOnly = false;
            TextBox1.Scrollbars = ScrollBars.None;
            TextBox1.SelectedText = "";
            TextBox1.SelectionLength = 0;
            TextBox1.SelectionStart = 0;
            TextBox1.Size = new Size(630, 24);
            TextBox1.TabIndex = 38;
            TextBox1.TextAlign = HorizontalAlignment.Left;
            TextBox1.UseSystemPasswordChar = false;
            TextBox1.WordWrap = true;
            // 
            // Label24
            // 
            Label24.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label24.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label24.Location = new Point(85, 271);
            Label24.Name = "Label24";
            Label24.Size = new Size(60, 24);
            Label24.TabIndex = 37;
            Label24.Text = "Preview:";
            Label24.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label23
            // 
            Label23.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label23.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label23.Location = new Point(85, 241);
            Label23.Name = "Label23";
            Label23.Size = new Size(60, 24);
            Label23.TabIndex = 36;
            Label23.Text = "Stable:";
            Label23.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AlertBox13
            // 
            AlertBox13.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox13.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox13.CenterText = false;
            AlertBox13.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox13.Font = new Font("Segoe UI", 9.0f);
            AlertBox13.Image = null;
            AlertBox13.Location = new Point(56, 301);
            AlertBox13.Name = "AlertBox13";
            AlertBox13.Size = new Size(761, 22);
            AlertBox13.TabIndex = 35;
            AlertBox13.TabStop = false;
            AlertBox13.Text = "Use this if you installed Windows Terminal in another custom drive or directory. " + "This file must be correct or you will face errors";
            // 
            // PictureBox33
            // 
            PictureBox33.Image = (Image)resources.GetObject("PictureBox33.Image");
            PictureBox33.Location = new Point(55, 271);
            PictureBox33.Name = "PictureBox33";
            PictureBox33.Size = new Size(24, 24);
            PictureBox33.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox33.TabIndex = 34;
            PictureBox33.TabStop = false;
            // 
            // PictureBox32
            // 
            PictureBox32.Image = (Image)resources.GetObject("PictureBox32.Image");
            PictureBox32.Location = new Point(55, 241);
            PictureBox32.Name = "PictureBox32";
            PictureBox32.Size = new Size(24, 24);
            PictureBox32.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox32.TabIndex = 33;
            PictureBox32.TabStop = false;
            // 
            // PictureBox31
            // 
            PictureBox31.Image = (Image)resources.GetObject("PictureBox31.Image");
            PictureBox31.Location = new Point(17, 209);
            PictureBox31.Name = "PictureBox31";
            PictureBox31.Size = new Size(24, 24);
            PictureBox31.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox31.TabIndex = 31;
            PictureBox31.TabStop = false;
            // 
            // CheckBox14
            // 
            CheckBox14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox14.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox14.Checked = false;
            CheckBox14.Font = new Font("Segoe UI", 9.0f);
            CheckBox14.ForeColor = Color.White;
            CheckBox14.Location = new Point(47, 209);
            CheckBox14.Name = "CheckBox14";
            CheckBox14.Size = new Size(770, 24);
            CheckBox14.TabIndex = 32;
            CheckBox14.Text = "Allow for Windows Terminal \"Settings.json\" deflection";
            // 
            // AlertBox12
            // 
            AlertBox12.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox12.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox12.CenterText = false;
            AlertBox12.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox12.Font = new Font("Segoe UI", 9.0f);
            AlertBox12.Image = null;
            AlertBox12.Location = new Point(56, 176);
            AlertBox12.Name = "AlertBox12";
            AlertBox12.Size = new Size(761, 22);
            AlertBox12.TabIndex = 30;
            AlertBox12.TabStop = false;
            AlertBox12.Text = "Enhanced Command Prompt is found in Windows 10 19H2 (1909) and later";
            // 
            // AlertBox11
            // 
            AlertBox11.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox11.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox11.CenterText = false;
            AlertBox11.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox11.Font = new Font("Segoe UI", 9.0f);
            AlertBox11.Image = null;
            AlertBox11.Location = new Point(56, 148);
            AlertBox11.Name = "AlertBox11";
            AlertBox11.Size = new Size(761, 22);
            AlertBox11.TabIndex = 29;
            AlertBox11.TabStop = false;
            AlertBox11.Text = "May cause wrong renderering in Enhanced Command Prompt and Windows Terminal, and " + "they won't be used in legacy terminal";
            // 
            // PictureBox30
            // 
            PictureBox30.Image = (Image)resources.GetObject("PictureBox30.Image");
            PictureBox30.Location = new Point(17, 116);
            PictureBox30.Name = "PictureBox30";
            PictureBox30.Size = new Size(24, 24);
            PictureBox30.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox30.TabIndex = 26;
            PictureBox30.TabStop = false;
            // 
            // CheckBox13
            // 
            CheckBox13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox13.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox13.Checked = false;
            CheckBox13.Font = new Font("Segoe UI", 9.0f);
            CheckBox13.ForeColor = Color.White;
            CheckBox13.Location = new Point(47, 116);
            CheckBox13.Name = "CheckBox13";
            CheckBox13.Size = new Size(770, 24);
            CheckBox13.TabIndex = 27;
            CheckBox13.Text = "Allow for non-monospace fonts";
            // 
            // AlertBox8
            // 
            AlertBox8.AlertStyle = UI.WP.AlertBox.Style.Simple;
            AlertBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AlertBox8.BackColor = Color.FromArgb(50, 50, 50);
            AlertBox8.CenterText = false;
            AlertBox8.CustomColor = Color.FromArgb(0, 81, 210);
            AlertBox8.Font = new Font("Segoe UI", 9.0f);
            AlertBox8.Image = null;
            AlertBox8.Location = new Point(56, 86);
            AlertBox8.Name = "AlertBox8";
            AlertBox8.Size = new Size(761, 22);
            AlertBox8.TabIndex = 24;
            AlertBox8.TabStop = false;
            AlertBox8.Text = "In case you want to design a theme for all versions of Windows and save it as a f" + "ile for sharing (not applying it).";
            // 
            // PictureBox29
            // 
            PictureBox29.Image = (Image)resources.GetObject("PictureBox29.Image");
            PictureBox29.Location = new Point(17, 54);
            PictureBox29.Name = "PictureBox29";
            PictureBox29.Size = new Size(24, 24);
            PictureBox29.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox29.TabIndex = 22;
            PictureBox29.TabStop = false;
            // 
            // CheckBox12
            // 
            CheckBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox12.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox12.Checked = false;
            CheckBox12.Font = new Font("Segoe UI", 9.0f);
            CheckBox12.ForeColor = Color.White;
            CheckBox12.Location = new Point(47, 54);
            CheckBox12.Name = "CheckBox12";
            CheckBox12.Size = new Size(770, 24);
            CheckBox12.TabIndex = 23;
            CheckBox12.Text = "Bypass OS restriction";
            // 
            // Separator3
            // 
            Separator3.AlternativeLook = false;
            Separator3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator3.Location = new Point(6, 47);
            Separator3.Name = "Separator3";
            Separator3.Size = new Size(811, 1);
            Separator3.TabIndex = 21;
            Separator3.TabStop = false;
            // 
            // PictureBox28
            // 
            PictureBox28.Image = (Image)resources.GetObject("PictureBox28.Image");
            PictureBox28.Location = new Point(6, 6);
            PictureBox28.Name = "PictureBox28";
            PictureBox28.Size = new Size(35, 35);
            PictureBox28.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox28.TabIndex = 20;
            PictureBox28.TabStop = false;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label7.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label7.Location = new Point(47, 6);
            Label7.Name = "Label7";
            Label7.Size = new Size(770, 35);
            Label7.TabIndex = 19;
            Label7.Text = "Terminals";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage9
            // 
            TabPage9.BackColor = Color.FromArgb(25, 25, 25);
            TabPage9.Controls.Add(GroupBox2);
            TabPage9.Controls.Add(PictureBox49);
            TabPage9.Controls.Add(CheckBox21);
            TabPage9.Controls.Add(PictureBox48);
            TabPage9.Controls.Add(CheckBox20);
            TabPage9.Controls.Add(Separator9);
            TabPage9.Controls.Add(PictureBox47);
            TabPage9.Controls.Add(Label30);
            TabPage9.Location = new Point(199, 4);
            TabPage9.Name = "TabPage9";
            TabPage9.Padding = new Padding(3);
            TabPage9.Size = new Size(823, 471);
            TabPage9.TabIndex = 9;
            TabPage9.Text = "ExplorerPatcher";
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(Panel3);
            GroupBox2.Controls.Add(Panel2);
            GroupBox2.Controls.Add(Panel1);
            GroupBox2.Controls.Add(EP_Start_10_Type);
            GroupBox2.Controls.Add(Label34);
            GroupBox2.Controls.Add(Label32);
            GroupBox2.Controls.Add(Label31);
            GroupBox2.Controls.Add(Label33);
            GroupBox2.Location = new Point(53, 117);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(764, 140);
            GroupBox2.TabIndex = 200;
            GroupBox2.Text = "GroupBox2";
            // 
            // Panel3
            // 
            Panel3.BackColor = Color.Transparent;
            Panel3.Controls.Add(EP_ORB_11);
            Panel3.Controls.Add(EP_ORB_10);
            Panel3.Location = new Point(163, 102);
            Panel3.Name = "Panel3";
            Panel3.Size = new Size(72, 35);
            Panel3.TabIndex = 50;
            // 
            // EP_ORB_11
            // 
            EP_ORB_11.Checked = false;
            EP_ORB_11.Font = new Font("Segoe UI", 9.0f);
            EP_ORB_11.ForeColor = Color.White;
            EP_ORB_11.Image = null;
            EP_ORB_11.Location = new Point(3, 3);
            EP_ORB_11.Name = "EP_ORB_11";
            EP_ORB_11.ShowText = false;
            EP_ORB_11.Size = new Size(30, 30);
            EP_ORB_11.TabIndex = 45;
            EP_ORB_11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EP_ORB_10
            // 
            EP_ORB_10.Checked = false;
            EP_ORB_10.Font = new Font("Segoe UI", 9.0f);
            EP_ORB_10.ForeColor = Color.White;
            EP_ORB_10.Image = null;
            EP_ORB_10.Location = new Point(39, 3);
            EP_ORB_10.Name = "EP_ORB_10";
            EP_ORB_10.ShowText = false;
            EP_ORB_10.Size = new Size(30, 30);
            EP_ORB_10.TabIndex = 46;
            EP_ORB_10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Panel2
            // 
            Panel2.BackColor = Color.Transparent;
            Panel2.Controls.Add(EP_Taskbar_11);
            Panel2.Controls.Add(EP_Taskbar_10);
            Panel2.Location = new Point(163, 66);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(72, 35);
            Panel2.TabIndex = 49;
            // 
            // EP_Taskbar_11
            // 
            EP_Taskbar_11.Checked = false;
            EP_Taskbar_11.Font = new Font("Segoe UI", 9.0f);
            EP_Taskbar_11.ForeColor = Color.White;
            EP_Taskbar_11.Image = null;
            EP_Taskbar_11.Location = new Point(3, 3);
            EP_Taskbar_11.Name = "EP_Taskbar_11";
            EP_Taskbar_11.ShowText = false;
            EP_Taskbar_11.Size = new Size(30, 30);
            EP_Taskbar_11.TabIndex = 42;
            EP_Taskbar_11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EP_Taskbar_10
            // 
            EP_Taskbar_10.Checked = false;
            EP_Taskbar_10.Font = new Font("Segoe UI", 9.0f);
            EP_Taskbar_10.ForeColor = Color.White;
            EP_Taskbar_10.Image = null;
            EP_Taskbar_10.Location = new Point(39, 3);
            EP_Taskbar_10.Name = "EP_Taskbar_10";
            EP_Taskbar_10.ShowText = false;
            EP_Taskbar_10.Size = new Size(30, 30);
            EP_Taskbar_10.TabIndex = 43;
            EP_Taskbar_10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.Transparent;
            Panel1.Controls.Add(EP_Start_11);
            Panel1.Controls.Add(EP_Start_10);
            Panel1.Location = new Point(163, 30);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(72, 35);
            Panel1.TabIndex = 48;
            // 
            // EP_Start_11
            // 
            EP_Start_11.Checked = false;
            EP_Start_11.Font = new Font("Segoe UI", 9.0f);
            EP_Start_11.ForeColor = Color.White;
            EP_Start_11.Image = null;
            EP_Start_11.Location = new Point(3, 3);
            EP_Start_11.Name = "EP_Start_11";
            EP_Start_11.ShowText = false;
            EP_Start_11.Size = new Size(30, 30);
            EP_Start_11.TabIndex = 39;
            EP_Start_11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EP_Start_10
            // 
            EP_Start_10.Checked = false;
            EP_Start_10.Font = new Font("Segoe UI", 9.0f);
            EP_Start_10.ForeColor = Color.White;
            EP_Start_10.Image = null;
            EP_Start_10.Location = new Point(39, 3);
            EP_Start_10.Name = "EP_Start_10";
            EP_Start_10.ShowText = false;
            EP_Start_10.Size = new Size(30, 30);
            EP_Start_10.TabIndex = 40;
            EP_Start_10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EP_Start_10_Type
            // 
            EP_Start_10_Type.BackColor = Color.FromArgb(43, 43, 43);
            EP_Start_10_Type.DrawMode = DrawMode.OwnerDrawVariable;
            EP_Start_10_Type.DropDownStyle = ComboBoxStyle.DropDownList;
            EP_Start_10_Type.Font = new Font("Segoe UI", 9.0f);
            EP_Start_10_Type.ForeColor = Color.White;
            EP_Start_10_Type.FormattingEnabled = true;
            EP_Start_10_Type.ItemHeight = 20;
            EP_Start_10_Type.Items.AddRange(new object[] { "Not rounded", "Rounded corners with floating menu", "Rounded corners with docked menu" });
            EP_Start_10_Type.Location = new Point(241, 34);
            EP_Start_10_Type.Name = "EP_Start_10_Type";
            EP_Start_10_Type.Size = new Size(236, 26);
            EP_Start_10_Type.TabIndex = 47;
            // 
            // Label34
            // 
            Label34.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label34.Location = new Point(45, 102);
            Label34.Name = "Label34";
            Label34.Size = new Size(112, 35);
            Label34.TabIndex = 44;
            Label34.Text = "Taskbar ORB:";
            Label34.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label32
            // 
            Label32.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label32.Location = new Point(45, 30);
            Label32.Name = "Label32";
            Label32.Size = new Size(112, 35);
            Label32.TabIndex = 38;
            Label32.Text = "Start:";
            Label32.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label31
            // 
            Label31.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label31.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label31.Location = new Point(5, 4);
            Label31.Name = "Label31";
            Label31.Size = new Size(755, 23);
            Label31.TabIndex = 21;
            Label31.Text = "If deflected, make the previewer follow these settings:";
            Label31.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label33
            // 
            Label33.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label33.Location = new Point(45, 66);
            Label33.Name = "Label33";
            Label33.Size = new Size(112, 35);
            Label33.TabIndex = 41;
            Label33.Text = "Taskbar:";
            Label33.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox49
            // 
            PictureBox49.Image = (Image)resources.GetObject("PictureBox49.Image");
            PictureBox49.Location = new Point(17, 86);
            PictureBox49.Name = "PictureBox49";
            PictureBox49.Size = new Size(24, 24);
            PictureBox49.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox49.TabIndex = 198;
            PictureBox49.TabStop = false;
            // 
            // CheckBox21
            // 
            CheckBox21.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox21.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox21.Checked = false;
            CheckBox21.Font = new Font("Segoe UI", 9.0f);
            CheckBox21.ForeColor = Color.White;
            CheckBox21.Location = new Point(47, 86);
            CheckBox21.Name = "CheckBox21";
            CheckBox21.Size = new Size(770, 24);
            CheckBox21.TabIndex = 199;
            CheckBox21.Text = "Deflect (Force the preview of Windows 11 to simulate as if ExplorerPatcher is ins" + "talled even if you are not using Windows 11)";
            // 
            // PictureBox48
            // 
            PictureBox48.Image = (Image)resources.GetObject("PictureBox48.Image");
            PictureBox48.Location = new Point(17, 54);
            PictureBox48.Name = "PictureBox48";
            PictureBox48.Size = new Size(24, 24);
            PictureBox48.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox48.TabIndex = 26;
            PictureBox48.TabStop = false;
            // 
            // CheckBox20
            // 
            CheckBox20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox20.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox20.Checked = true;
            CheckBox20.Font = new Font("Segoe UI", 9.0f);
            CheckBox20.ForeColor = Color.White;
            CheckBox20.Location = new Point(47, 54);
            CheckBox20.Name = "CheckBox20";
            CheckBox20.Size = new Size(770, 24);
            CheckBox20.TabIndex = 25;
            CheckBox20.Text = "Synchronize with Windows 11 preview if installed";
            // 
            // Separator9
            // 
            Separator9.AlternativeLook = false;
            Separator9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator9.Location = new Point(6, 47);
            Separator9.Name = "Separator9";
            Separator9.Size = new Size(811, 1);
            Separator9.TabIndex = 22;
            Separator9.TabStop = false;
            // 
            // PictureBox47
            // 
            PictureBox47.Image = (Image)resources.GetObject("PictureBox47.Image");
            PictureBox47.Location = new Point(6, 6);
            PictureBox47.Name = "PictureBox47";
            PictureBox47.Size = new Size(35, 35);
            PictureBox47.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox47.TabIndex = 21;
            PictureBox47.TabStop = false;
            // 
            // Label30
            // 
            Label30.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label30.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label30.Location = new Point(47, 6);
            Label30.Name = "Label30";
            Label30.Size = new Size(770, 35);
            Label30.TabIndex = 20;
            Label30.Text = "ExplorerPatcher";
            Label30.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage19
            // 
            TabPage19.BackColor = Color.FromArgb(25, 25, 25);
            TabPage19.Controls.Add(PictureBox70);
            TabPage19.Controls.Add(CheckBox38);
            TabPage19.Controls.Add(PictureBox69);
            TabPage19.Controls.Add(CheckBox37);
            TabPage19.Controls.Add(PictureBox68);
            TabPage19.Controls.Add(CheckBox35);
            TabPage19.Controls.Add(PictureBox58);
            TabPage19.Controls.Add(PictureBox46);
            TabPage19.Controls.Add(PictureBox45);
            TabPage19.Controls.Add(PictureBox42);
            TabPage19.Controls.Add(PictureBox27);
            TabPage19.Controls.Add(PictureBox43);
            TabPage19.Controls.Add(CheckBox32);
            TabPage19.Controls.Add(Separator1);
            TabPage19.Controls.Add(PictureBox4);
            TabPage19.Controls.Add(Label21);
            TabPage19.Controls.Add(CheckBox34);
            TabPage19.Controls.Add(CheckBox11);
            TabPage19.Controls.Add(CheckBox3);
            TabPage19.Controls.Add(CheckBox31);
            TabPage19.Controls.Add(ComboBox3);
            TabPage19.Controls.Add(CheckBox10);
            TabPage19.Location = new Point(199, 4);
            TabPage19.Name = "TabPage19";
            TabPage19.Padding = new Padding(3);
            TabPage19.Size = new Size(823, 471);
            TabPage19.TabIndex = 11;
            TabPage19.Text = "Color item info";
            // 
            // PictureBox70
            // 
            PictureBox70.Image = (Image)resources.GetObject("PictureBox70.Image");
            PictureBox70.Location = new Point(47, 296);
            PictureBox70.Name = "PictureBox70";
            PictureBox70.Size = new Size(24, 24);
            PictureBox70.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox70.TabIndex = 46;
            PictureBox70.TabStop = false;
            // 
            // CheckBox38
            // 
            CheckBox38.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox38.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox38.Checked = false;
            CheckBox38.Font = new Font("Segoe UI", 9.0f);
            CheckBox38.ForeColor = Color.White;
            CheckBox38.Location = new Point(77, 296);
            CheckBox38.Name = "CheckBox38";
            CheckBox38.Size = new Size(740, 24);
            CheckBox38.TabIndex = 45;
            CheckBox38.Text = "Enable ripple effect on dragging and dropping a color";
            // 
            // PictureBox69
            // 
            PictureBox69.Image = (Image)resources.GetObject("PictureBox69.Image");
            PictureBox69.Location = new Point(47, 266);
            PictureBox69.Name = "PictureBox69";
            PictureBox69.Size = new Size(24, 24);
            PictureBox69.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox69.TabIndex = 44;
            PictureBox69.TabStop = false;
            // 
            // CheckBox37
            // 
            CheckBox37.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox37.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox37.Checked = false;
            CheckBox37.Font = new Font("Segoe UI", 9.0f);
            CheckBox37.ForeColor = Color.White;
            CheckBox37.Location = new Point(77, 266);
            CheckBox37.Name = "CheckBox37";
            CheckBox37.Size = new Size(740, 24);
            CheckBox37.TabIndex = 43;
            CheckBox37.Text = "Show guide (colors values) on dragging and dropping a color";
            // 
            // PictureBox68
            // 
            PictureBox68.Image = (Image)resources.GetObject("PictureBox68.Image");
            PictureBox68.Location = new Point(17, 236);
            PictureBox68.Name = "PictureBox68";
            PictureBox68.Size = new Size(24, 24);
            PictureBox68.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox68.TabIndex = 42;
            PictureBox68.TabStop = false;
            // 
            // CheckBox35
            // 
            CheckBox35.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox35.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox35.Checked = false;
            CheckBox35.Font = new Font("Segoe UI", 9.0f);
            CheckBox35.ForeColor = Color.White;
            CheckBox35.Location = new Point(47, 236);
            CheckBox35.Name = "CheckBox35";
            CheckBox35.Size = new Size(770, 24);
            CheckBox35.TabIndex = 41;
            CheckBox35.Text = "Enable drag and drop to copy/swap colors items";
            // 
            // PictureBox58
            // 
            PictureBox58.Image = (Image)resources.GetObject("PictureBox58.Image");
            PictureBox58.Location = new Point(17, 146);
            PictureBox58.Name = "PictureBox58";
            PictureBox58.Size = new Size(24, 24);
            PictureBox58.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox58.TabIndex = 40;
            PictureBox58.TabStop = false;
            // 
            // PictureBox46
            // 
            PictureBox46.Image = (Image)resources.GetObject("PictureBox46.Image");
            PictureBox46.Location = new Point(17, 206);
            PictureBox46.Name = "PictureBox46";
            PictureBox46.Size = new Size(24, 24);
            PictureBox46.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox46.TabIndex = 39;
            PictureBox46.TabStop = false;
            // 
            // PictureBox45
            // 
            PictureBox45.Image = (Image)resources.GetObject("PictureBox45.Image");
            PictureBox45.Location = new Point(17, 176);
            PictureBox45.Name = "PictureBox45";
            PictureBox45.Size = new Size(24, 24);
            PictureBox45.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox45.TabIndex = 38;
            PictureBox45.TabStop = false;
            // 
            // PictureBox42
            // 
            PictureBox42.Image = (Image)resources.GetObject("PictureBox42.Image");
            PictureBox42.Location = new Point(47, 116);
            PictureBox42.Name = "PictureBox42";
            PictureBox42.Size = new Size(24, 24);
            PictureBox42.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox42.TabIndex = 37;
            PictureBox42.TabStop = false;
            // 
            // PictureBox27
            // 
            PictureBox27.Image = (Image)resources.GetObject("PictureBox27.Image");
            PictureBox27.Location = new Point(17, 54);
            PictureBox27.Name = "PictureBox27";
            PictureBox27.Size = new Size(24, 24);
            PictureBox27.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox27.TabIndex = 36;
            PictureBox27.TabStop = false;
            // 
            // PictureBox43
            // 
            PictureBox43.Image = (Image)resources.GetObject("PictureBox43.Image");
            PictureBox43.Location = new Point(17, 326);
            PictureBox43.Name = "PictureBox43";
            PictureBox43.Size = new Size(24, 24);
            PictureBox43.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox43.TabIndex = 28;
            PictureBox43.TabStop = false;
            // 
            // CheckBox32
            // 
            CheckBox32.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox32.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox32.Checked = false;
            CheckBox32.Font = new Font("Segoe UI", 9.0f);
            CheckBox32.ForeColor = Color.White;
            CheckBox32.Location = new Point(47, 326);
            CheckBox32.Name = "CheckBox32";
            CheckBox32.Size = new Size(770, 24);
            CheckBox32.TabIndex = 29;
            CheckBox32.Text = "Use classic color picker instead of WinPaletter's default one on pressing on a co" + "lor palette item";
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(6, 47);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(811, 1);
            Separator1.TabIndex = 35;
            Separator1.TabStop = false;
            // 
            // PictureBox4
            // 
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(6, 6);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(35, 35);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 34;
            PictureBox4.TabStop = false;
            // 
            // Label21
            // 
            Label21.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label21.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label21.Location = new Point(47, 6);
            Label21.Name = "Label21";
            Label21.Size = new Size(770, 35);
            Label21.TabIndex = 33;
            Label21.Text = "Color item info";
            Label21.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CheckBox34
            // 
            CheckBox34.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox34.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox34.Checked = false;
            CheckBox34.Font = new Font("Segoe UI", 9.0f);
            CheckBox34.ForeColor = Color.White;
            CheckBox34.Location = new Point(47, 146);
            CheckBox34.Name = "CheckBox34";
            CheckBox34.Size = new Size(770, 24);
            CheckBox34.TabIndex = 32;
            CheckBox34.Text = "Make a dot inside color info rectangle to indicate that the choosen color is not " + "as the default color";
            // 
            // CheckBox11
            // 
            CheckBox11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox11.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox11.Checked = false;
            CheckBox11.Font = new Font("Segoe UI", 9.0f);
            CheckBox11.ForeColor = Color.White;
            CheckBox11.Location = new Point(77, 116);
            CheckBox11.Name = "CheckBox11";
            CheckBox11.Size = new Size(740, 24);
            CheckBox11.TabIndex = 26;
            CheckBox11.Text = "Show hash (#) if HEX mode is selected";
            // 
            // CheckBox3
            // 
            CheckBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox3.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox3.Checked = false;
            CheckBox3.Font = new Font("Segoe UI", 9.0f);
            CheckBox3.ForeColor = Color.White;
            CheckBox3.Location = new Point(47, 176);
            CheckBox3.Name = "CheckBox3";
            CheckBox3.Size = new Size(770, 24);
            CheckBox3.TabIndex = 30;
            CheckBox3.Text = "Make color label more transparent";
            // 
            // CheckBox31
            // 
            CheckBox31.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox31.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox31.Checked = false;
            CheckBox31.Font = new Font("Segoe UI", 9.0f);
            CheckBox31.ForeColor = Color.White;
            CheckBox31.Location = new Point(47, 206);
            CheckBox31.Name = "CheckBox31";
            CheckBox31.Size = new Size(770, 24);
            CheckBox31.TabIndex = 31;
            CheckBox31.Text = "Use default Windows monospaced font instead of JetBrains Mono";
            // 
            // ComboBox3
            // 
            ComboBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox3.BackColor = Color.FromArgb(34, 34, 34);
            ComboBox3.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox3.Font = new Font("Segoe UI", 9.0f);
            ComboBox3.ForeColor = Color.White;
            ComboBox3.FormattingEnabled = true;
            ComboBox3.ItemHeight = 20;
            ComboBox3.Items.AddRange(new object[] { "HEX", "RGB", "HSL", "Decimal" });
            ComboBox3.Location = new Point(47, 84);
            ComboBox3.Name = "ComboBox3";
            ComboBox3.Size = new Size(282, 26);
            ComboBox3.TabIndex = 25;
            // 
            // CheckBox10
            // 
            CheckBox10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox10.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox10.Checked = false;
            CheckBox10.Font = new Font("Segoe UI", 9.0f);
            CheckBox10.ForeColor = Color.White;
            CheckBox10.Location = new Point(47, 54);
            CheckBox10.Name = "CheckBox10";
            CheckBox10.Size = new Size(770, 24);
            CheckBox10.TabIndex = 24;
            CheckBox10.Text = "Show color info on a color palette item";
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(PictureBox15);
            TabPage6.Controls.Add(CheckBox9);
            TabPage6.Controls.Add(Separator7);
            TabPage6.Controls.Add(PictureBox13);
            TabPage6.Controls.Add(Label6);
            TabPage6.Location = new Point(199, 4);
            TabPage6.Name = "TabPage6";
            TabPage6.Padding = new Padding(3);
            TabPage6.Size = new Size(823, 471);
            TabPage6.TabIndex = 5;
            TabPage6.Text = "Miscellaneous";
            // 
            // PictureBox15
            // 
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(17, 54);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox15.TabIndex = 20;
            PictureBox15.TabStop = false;
            // 
            // CheckBox9
            // 
            CheckBox9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CheckBox9.BackColor = Color.FromArgb(25, 25, 25);
            CheckBox9.Checked = false;
            CheckBox9.Font = new Font("Segoe UI", 9.0f);
            CheckBox9.ForeColor = Color.White;
            CheckBox9.Location = new Point(47, 54);
            CheckBox9.Name = "CheckBox9";
            CheckBox9.Size = new Size(770, 24);
            CheckBox9.TabIndex = 21;
            CheckBox9.Text = "Preview every change I make to colors and values in real-time (Windows 7 and 8.1)" + "";
            // 
            // Separator7
            // 
            Separator7.AlternativeLook = false;
            Separator7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator7.Location = new Point(6, 47);
            Separator7.Name = "Separator7";
            Separator7.Size = new Size(811, 1);
            Separator7.TabIndex = 19;
            Separator7.TabStop = false;
            // 
            // PictureBox13
            // 
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(6, 6);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(35, 35);
            PictureBox13.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox13.TabIndex = 3;
            PictureBox13.TabStop = false;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label6.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label6.Location = new Point(47, 6);
            Label6.Name = "Label6";
            Label6.Size = new Size(770, 35);
            Label6.TabIndex = 2;
            Label6.Text = "Miscellaneous";
            Label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button6
            // 
            Button6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button6.BackColor = Color.FromArgb(34, 34, 34);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.ImageAlign = ContentAlignment.MiddleLeft;
            Button6.LineColor = Color.FromArgb(126, 88, 59);
            Button6.Location = new Point(213, 560);
            Button6.Name = "Button6";
            Button6.Size = new Size(100, 34);
            Button6.TabIndex = 20;
            Button6.Text = "Uninstall";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button5.BackColor = Color.FromArgb(34, 34, 34);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.ImageAlign = ContentAlignment.MiddleRight;
            Button5.LineColor = Color.FromArgb(152, 43, 51);
            Button5.Location = new Point(12, 560);
            Button5.Name = "Button5";
            Button5.Size = new Size(195, 34);
            Button5.TabIndex = 19;
            Button5.Text = "De-associate files extensions";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button4.BackColor = Color.FromArgb(34, 34, 34);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.ImageAlign = ContentAlignment.MiddleRight;
            Button4.LineColor = Color.FromArgb(125, 26, 54);
            Button4.Location = new Point(319, 560);
            Button4.Name = "Button4";
            Button4.Size = new Size(90, 34);
            Button4.TabIndex = 17;
            Button4.Text = "Import";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button3.BackColor = Color.FromArgb(34, 34, 34);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.ImageAlign = ContentAlignment.MiddleRight;
            Button3.LineColor = Color.FromArgb(126, 26, 54);
            Button3.Location = new Point(415, 560);
            Button3.Name = "Button3";
            Button3.Size = new Size(90, 34);
            Button3.TabIndex = 16;
            Button3.Text = "Export";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.BackColor = Color.FromArgb(34, 34, 34);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = null;
            Button2.LineColor = Color.FromArgb(199, 49, 61);
            Button2.Location = new Point(725, 560);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 34);
            Button2.TabIndex = 15;
            Button2.Text = "Cancel";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(34, 34, 34);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleLeft;
            Button1.LineColor = Color.FromArgb(73, 123, 145);
            Button1.Location = new Point(811, 560);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 34);
            Button1.TabIndex = 14;
            Button1.Text = "Save";
            Button1.UseVisualStyleBackColor = false;
            // 
            // AnimatedBox1
            // 
            AnimatedBox1.Color = Color.DodgerBlue;
            AnimatedBox1.Color1 = Color.DodgerBlue;
            AnimatedBox1.Color2 = Color.Crimson;
            AnimatedBox1.Controls.Add(PictureBox1);
            AnimatedBox1.Controls.Add(Label17);
            AnimatedBox1.Dock = DockStyle.Top;
            AnimatedBox1.Location = new Point(0, 0);
            AnimatedBox1.Name = "AnimatedBox1";
            AnimatedBox1.Size = new Size(1039, 68);
            AnimatedBox1.Style = UI.WP.AnimatedBox.Styles.SwapColors;
            AnimatedBox1.TabIndex = 25;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(10, 10);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(48, 48);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 8;
            PictureBox1.TabStop = false;
            // 
            // Label17
            // 
            Label17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label17.BackColor = Color.Transparent;
            Label17.Font = new Font("Segoe UI Semibold", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label17.Location = new Point(65, 10);
            Label17.Name = "Label17";
            Label17.Size = new Size(962, 48);
            Label17.TabIndex = 9;
            Label17.Text = "Settings";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SettingsX
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(1039, 606);
            Controls.Add(Button12);
            Controls.Add(TabControl1);
            Controls.Add(Button6);
            Controls.Add(Button5);
            Controls.Add(Button4);
            Controls.Add(Button3);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(AnimatedBox1);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1020, 615);
            Name = "SettingsX";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            TabPage7.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox26).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox44).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            TabPage5.ResumeLayout(false);
            TabControl2.ResumeLayout(false);
            TabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox62).EndInit();
            Panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox61).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox37).EndInit();
            TabPage12.ResumeLayout(false);
            Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox51).EndInit();
            Panel4.ResumeLayout(false);
            TabPage13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox55).EndInit();
            Panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox50).EndInit();
            TabPage14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox35).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox56).EndInit();
            Panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            TabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox34).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox60).EndInit();
            Panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox59).EndInit();
            Panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox52).EndInit();
            Panel8.ResumeLayout(false);
            TabPage20.ResumeLayout(false);
            Panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox67).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox66).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            TabPage15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox63).EndInit();
            TabControl3.ResumeLayout(false);
            TabPage16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox54).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox64).EndInit();
            TabPage17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox57).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox53).EndInit();
            TabPage18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox65).EndInit();
            TabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox71).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox39).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox38).EndInit();
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox33).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox32).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).EndInit();
            TabPage9.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            Panel3.ResumeLayout(false);
            Panel2.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox49).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox48).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox47).EndInit();
            TabPage19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox70).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox69).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox68).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox58).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox46).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox45).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox42).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox27).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox43).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            TabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            AnimatedBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(SettingsX_Load);
            DragEnter += new DragEventHandler(Me_DragEnter);
            DragDrop += new DragEventHandler(MainFrm_DragDrop);
            ResumeLayout(false);

        }
        internal Label Label17;
        internal PictureBox PictureBox1;
        internal PictureBox PictureBox17;
        internal Label Label1;
        internal UI.WP.CheckBox CheckBox1;
        internal PictureBox PictureBox19;
        internal UI.WP.CheckBox CheckBox2;
        internal UI.WP.RadioButton RadioButton1;
        internal PictureBox PictureBox3;
        internal UI.WP.RadioButton RadioButton2;
        internal UI.WP.AlertBox AlertBox1;
        internal PictureBox PictureBox7;
        internal PictureBox PictureBox8;
        internal Label Label2;
        internal UI.WP.AlertBox AlertBox3;
        internal UI.WP.AlertBox AlertBox4;
        internal UI.WP.CheckBox CheckBox5;
        internal PictureBox PictureBox6;
        internal PictureBox PictureBox9;
        internal Label Label3;
        internal UI.WP.Button Button1;
        internal UI.WP.Button Button2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal OpenFileDialog OpenFileDialog1;
        internal SaveFileDialog SaveFileDialog1;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal PictureBox PictureBox5;
        internal Label Label4;
        internal PictureBox PictureBox13;
        internal Label Label6;
        internal UI.WP.RadioButton RadioButton4;
        internal UI.WP.RadioButton RadioButton3;
        internal PictureBox PictureBox10;
        internal Label Label5;
        internal PictureBox PictureBox11;
        internal UI.WP.CheckBox CheckBox6;
        internal PictureBox PictureBox12;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal UI.WP.SeparatorH Separator2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal UI.WP.SeparatorH Separator4;
        internal UI.WP.SeparatorH Separator5;
        internal UI.WP.SeparatorH Separator6;
        internal UI.WP.SeparatorH Separator7;
        internal TabPage TabPage7;
        internal UI.WP.GroupBox GroupBox1;
        internal Label Label14;
        internal Label Label15;
        internal PictureBox PictureBox23;
        internal Label Label12;
        internal Label Label13;
        internal PictureBox PictureBox22;
        internal Label Label11;
        internal Label Label10;
        internal PictureBox PictureBox21;
        internal UI.WP.Button Button7;
        internal Label Label9;
        internal PictureBox PictureBox20;
        internal PictureBox PictureBox18;
        internal UI.WP.CheckBox CheckBox8;
        internal UI.WP.SeparatorH Separator8;
        internal PictureBox PictureBox16;
        internal Label Label8;
        internal Label Label16;
        internal Label Label18;
        internal PictureBox PictureBox24;
        internal Label Label19;
        internal Label Label20;
        internal PictureBox PictureBox25;
        internal Label Label22;
        internal PictureBox PictureBox26;
        internal UI.WP.AlertBox AlertBox9;
        internal UI.WP.Button Button8;
        internal ImageList ImageList1;
        internal PictureBox PictureBox14;
        internal UI.WP.CheckBox CheckBox7;
        internal PictureBox PictureBox15;
        internal UI.WP.CheckBox CheckBox9;
        internal UI.WP.AlertBox AlertBox6;
        internal UI.WP.CheckBox CheckBox10;
        internal UI.WP.ComboBox ComboBox3;
        internal UI.WP.CheckBox CheckBox11;
        internal TabPage TabPage2;
        internal PictureBox PictureBox29;
        internal UI.WP.CheckBox CheckBox12;
        internal UI.WP.SeparatorH Separator3;
        internal PictureBox PictureBox28;
        internal Label Label7;
        internal UI.WP.AlertBox AlertBox8;
        internal UI.WP.AlertBox AlertBox11;
        internal PictureBox PictureBox30;
        internal UI.WP.CheckBox CheckBox13;
        internal UI.WP.AlertBox AlertBox12;
        internal PictureBox PictureBox31;
        internal UI.WP.CheckBox CheckBox14;
        internal UI.WP.AlertBox AlertBox13;
        internal PictureBox PictureBox33;
        internal PictureBox PictureBox32;
        internal UI.WP.TextBox TextBox2;
        internal UI.WP.TextBox TextBox1;
        internal Label Label24;
        internal Label Label23;
        internal OpenFileDialog OpenJSONDlg;
        internal UI.WP.Button Button9;
        internal UI.WP.Button Button16;
        internal UI.WP.Button Button10;
        internal PictureBox PictureBox34;
        internal UI.WP.CheckBox CheckBox15;
        internal UI.WP.TextBox TextBox3;
        internal PictureBox PictureBox37;
        internal UI.WP.CheckBox CheckBox17;
        internal TabPage TabPage8;
        internal UI.WP.CheckBox CheckBox18;
        internal PictureBox PictureBox39;
        internal PictureBox PictureBox40;
        internal UI.WP.SeparatorH Separator10;
        internal PictureBox PictureBox38;
        internal Label Label26;
        internal UI.WP.AlertBox AlertBox_Themelog;
        internal Label Label27;
        internal UI.WP.NumericUpDown NumericUpDown1;
        internal PictureBox PictureBox35;
        internal UI.WP.CheckBox CheckBox16;
        internal OpenFileDialog OpenFileDialog2;
        internal UI.WP.AlertBox AlertBox2;
        internal UI.WP.Button Button11;
        internal UI.WP.ComboBox ComboBox2;
        internal UI.WP.Button Button12;
        internal UI.WP.AnimatedBox AnimatedBox1;
        internal TabPage TabPage9;
        internal UI.WP.SeparatorH Separator9;
        internal PictureBox PictureBox47;
        internal Label Label30;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.ComboBox EP_Start_10_Type;
        internal UI.WP.RadioImage EP_ORB_10;
        internal UI.WP.RadioImage EP_ORB_11;
        internal Label Label34;
        internal UI.WP.RadioImage EP_Taskbar_10;
        internal UI.WP.RadioImage EP_Taskbar_11;
        internal Label Label33;
        internal UI.WP.RadioImage EP_Start_10;
        internal UI.WP.RadioImage EP_Start_11;
        internal Label Label32;
        internal Label Label31;
        internal PictureBox PictureBox49;
        internal UI.WP.CheckBox CheckBox21;
        internal PictureBox PictureBox48;
        internal UI.WP.CheckBox CheckBox20;
        internal Panel Panel3;
        internal Panel Panel2;
        internal Panel Panel1;
        internal PictureBox PictureBox50;
        internal UI.WP.CheckBox CheckBox22;
        internal Label Label36;
        internal PictureBox PictureBox51;
        internal Panel Panel5;
        internal UI.WP.RadioButton RadioButton7;
        internal UI.WP.RadioButton RadioButton8;
        internal Panel Panel4;
        internal UI.WP.RadioButton RadioButton6;
        internal UI.WP.RadioButton RadioButton5;
        internal Label CheckBox24;
        internal Label CheckBox23;
        internal UI.WP.RadioButton RadioButton10;
        internal UI.WP.RadioButton RadioButton9;
        internal UI.WP.TabControl TabControl2;
        internal TabPage TabPage11;
        internal TabPage TabPage12;
        internal UI.WP.CheckBox CheckBox25;
        internal TabPage TabPage13;
        internal Label Label39;
        internal Panel Panel6;
        internal UI.WP.RadioButton RadioButton11;
        internal UI.WP.RadioButton RadioButton12;
        internal Label Label40;
        internal PictureBox PictureBox55;
        internal UI.WP.AlertBox AlertBox5;
        internal UI.WP.AlertBox AlertBox14;
        internal TabPage TabPage14;
        internal UI.WP.AlertBox AlertBox15;
        internal PictureBox PictureBox56;
        internal Label Label41;
        internal Panel Panel7;
        internal UI.WP.RadioButton RadioButton13;
        internal UI.WP.RadioButton RadioButton14;
        internal Label Label42;
        internal TabPage TabPage10;
        internal UI.WP.AlertBox AlertBox16;
        internal PictureBox PictureBox60;
        internal Label Label48;
        internal Panel Panel10;
        internal UI.WP.RadioButton RadioButton19;
        internal UI.WP.RadioButton RadioButton20;
        internal Label Label49;
        internal PictureBox PictureBox59;
        internal Label Label46;
        internal Panel Panel9;
        internal UI.WP.RadioButton RadioButton17;
        internal UI.WP.RadioButton RadioButton18;
        internal Label Label47;
        internal PictureBox PictureBox52;
        internal Label Label35;
        internal Panel Panel8;
        internal UI.WP.RadioButton RadioButton15;
        internal UI.WP.RadioButton RadioButton16;
        internal Label Label44;
        internal UI.WP.AlertBox AlertBox17;
        internal PictureBox PictureBox61;
        internal UI.WP.AlertBox AlertBox18;
        internal UI.WP.AlertBox AlertBox19;
        internal Label Label50;
        internal PictureBox PictureBox62;
        internal Panel Panel11;
        internal UI.WP.RadioButton RadioButton23;
        internal UI.WP.RadioButton RadioButton21;
        internal UI.WP.RadioButton RadioButton22;
        internal Label Label51;
        internal TabPage TabPage15;
        internal Label Label55;
        internal Label Label54;
        internal UI.WP.RadioImage RadioImage2;
        internal UI.WP.RadioImage RadioImage1;
        internal PictureBox PictureBox64;
        internal Label Label53;
        internal UI.WP.SeparatorH Separator15;
        internal PictureBox PictureBox63;
        internal Label Label52;
        internal UI.WP.Button Button17;
        internal UI.WP.Button Button18;
        internal ListBox ListBox2;
        internal UI.WP.Button Button15;
        internal UI.WP.Button Button14;
        internal ListBox ListBox1;
        internal FolderBrowserDialog FolderBrowserDialog1;
        internal UI.WP.SeparatorH Separator16;
        internal PictureBox PictureBox65;
        internal Label Label56;
        internal UI.WP.CheckBox CheckBox26;
        internal UI.WP.CheckBox CheckBox27;
        internal UI.WP.CheckBox CheckBox28;
        internal UI.WP.TabControl TabControl3;
        internal TabPage TabPage16;
        internal UI.WP.CheckBox CheckBox29;
        internal TabPage TabPage17;
        internal UI.WP.Button Button19;
        internal Label Label38;
        internal PictureBox PictureBox53;
        internal Label Label37;
        internal TabPage TabPage18;
        internal PictureBox PictureBox54;
        internal UI.WP.AlertBox AlertBox20;
        internal UI.WP.Button Button20;
        internal Label Label43;
        internal PictureBox PictureBox57;
        internal Label Label45;
        internal UI.WP.CheckBox CheckBox30;
        internal PictureBox PictureBox41;
        internal PictureBox PictureBox36;
        internal PictureBox PictureBox43;
        internal UI.WP.CheckBox CheckBox32;
        internal UI.WP.CheckBox CheckBox33;
        internal PictureBox PictureBox44;
        internal UI.WP.AlertBox AlertBox22;
        internal PictureBox PictureBox2;
        internal UI.WP.CheckBox CheckBox4;
        internal UI.WP.CheckBox CheckBox34;
        internal UI.WP.CheckBox CheckBox31;
        internal UI.WP.CheckBox CheckBox3;
        internal TabPage TabPage19;
        internal UI.WP.SeparatorH Separator1;
        internal PictureBox PictureBox4;
        internal Label Label21;
        internal PictureBox PictureBox58;
        internal PictureBox PictureBox46;
        internal PictureBox PictureBox45;
        internal PictureBox PictureBox42;
        internal PictureBox PictureBox27;
        internal UI.WP.CheckBox CheckBox35_SFC;
        internal PictureBox PictureBox66;
        internal TabPage TabPage20;
        internal UI.WP.CheckBox CheckBox36;
        internal PictureBox PictureBox67;
        internal Panel Panel12;
        internal UI.WP.RadioButton RadioButton24;
        internal UI.WP.RadioButton RadioButton25;
        internal UI.WP.AlertBox AlertBox7;
        internal PictureBox PictureBox70;
        internal UI.WP.CheckBox CheckBox38;
        internal PictureBox PictureBox69;
        internal UI.WP.CheckBox CheckBox37;
        internal PictureBox PictureBox68;
        internal UI.WP.CheckBox CheckBox35;
        internal UI.WP.RadioImage VL2;
        internal UI.WP.RadioImage VL1;
        internal UI.WP.RadioImage VL0;
        internal Label Label25;
        internal PictureBox PictureBox71;
        internal UI.WP.CheckBox CheckBox19_ShowSkippedItemsOnDetailedVerbose;
    }
}