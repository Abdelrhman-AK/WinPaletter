using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Sounds_Editor : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sounds_Editor));
            this.OpenFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.OpenThemeDialog = new System.Windows.Forms.OpenFileDialog();
            this.alertBox1 = new WinPaletter.UI.WP.AlertBox();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button8 = new WinPaletter.UI.WP.Button();
            this.TabControl1 = new WinPaletter.UI.WP.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox88 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox92 = new System.Windows.Forms.PictureBox();
            this.button266 = new WinPaletter.UI.WP.Button();
            this.button267 = new WinPaletter.UI.WP.Button();
            this.button268 = new WinPaletter.UI.WP.Button();
            this.textBox86 = new WinPaletter.UI.WP.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.pictureBox87 = new System.Windows.Forms.PictureBox();
            this.Separator1 = new WinPaletter.UI.WP.SeparatorH();
            this.Separator3 = new WinPaletter.UI.WP.SeparatorH();
            this.GroupBox85 = new WinPaletter.UI.WP.GroupBox();
            this.Label85 = new System.Windows.Forms.Label();
            this.PictureBox84 = new System.Windows.Forms.PictureBox();
            this.CheckBox35_SFC = new WinPaletter.UI.WP.CheckBox();
            this.GroupBox65 = new WinPaletter.UI.WP.GroupBox();
            this.Button199 = new WinPaletter.UI.WP.Button();
            this.Button200 = new WinPaletter.UI.WP.Button();
            this.Button201 = new WinPaletter.UI.WP.Button();
            this.TextBox64 = new WinPaletter.UI.WP.TextBox();
            this.Label65 = new System.Windows.Forms.Label();
            this.PictureBox64 = new System.Windows.Forms.PictureBox();
            this.GroupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.Button24 = new WinPaletter.UI.WP.Button();
            this.Button23 = new WinPaletter.UI.WP.Button();
            this.Button22 = new WinPaletter.UI.WP.Button();
            this.Button19 = new WinPaletter.UI.WP.Button();
            this.Button20 = new WinPaletter.UI.WP.Button();
            this.Button21 = new WinPaletter.UI.WP.Button();
            this.TextBox2 = new WinPaletter.UI.WP.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox90 = new System.Windows.Forms.PictureBox();
            this.Button15 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.TextBox5 = new WinPaletter.UI.WP.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.GroupBox6 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox91 = new System.Windows.Forms.PictureBox();
            this.Button25 = new WinPaletter.UI.WP.Button();
            this.Button26 = new WinPaletter.UI.WP.Button();
            this.Button27 = new WinPaletter.UI.WP.Button();
            this.TextBox6 = new WinPaletter.UI.WP.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.GroupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox89 = new System.Windows.Forms.PictureBox();
            this.Button16 = new WinPaletter.UI.WP.Button();
            this.Button13 = new WinPaletter.UI.WP.Button();
            this.Button14 = new WinPaletter.UI.WP.Button();
            this.TextBox4 = new WinPaletter.UI.WP.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox88 = new System.Windows.Forms.PictureBox();
            this.Button17 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.TextBox3 = new WinPaletter.UI.WP.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.Button18 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.TextBox1 = new WinPaletter.UI.WP.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.GroupBox67 = new WinPaletter.UI.WP.GroupBox();
            this.Button205 = new WinPaletter.UI.WP.Button();
            this.Button206 = new WinPaletter.UI.WP.Button();
            this.Button207 = new WinPaletter.UI.WP.Button();
            this.TextBox66 = new WinPaletter.UI.WP.TextBox();
            this.Label67 = new System.Windows.Forms.Label();
            this.PictureBox66 = new System.Windows.Forms.PictureBox();
            this.GroupBox69 = new WinPaletter.UI.WP.GroupBox();
            this.Button211 = new WinPaletter.UI.WP.Button();
            this.Button212 = new WinPaletter.UI.WP.Button();
            this.Button213 = new WinPaletter.UI.WP.Button();
            this.TextBox68 = new WinPaletter.UI.WP.TextBox();
            this.Label69 = new System.Windows.Forms.Label();
            this.PictureBox68 = new System.Windows.Forms.PictureBox();
            this.GroupBox11 = new WinPaletter.UI.WP.GroupBox();
            this.Button40 = new WinPaletter.UI.WP.Button();
            this.Button41 = new WinPaletter.UI.WP.Button();
            this.Button42 = new WinPaletter.UI.WP.Button();
            this.TextBox11 = new WinPaletter.UI.WP.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.GroupBox10 = new WinPaletter.UI.WP.GroupBox();
            this.Button37 = new WinPaletter.UI.WP.Button();
            this.Button38 = new WinPaletter.UI.WP.Button();
            this.Button39 = new WinPaletter.UI.WP.Button();
            this.TextBox10 = new WinPaletter.UI.WP.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.GroupBox9 = new WinPaletter.UI.WP.GroupBox();
            this.Button34 = new WinPaletter.UI.WP.Button();
            this.Button35 = new WinPaletter.UI.WP.Button();
            this.Button36 = new WinPaletter.UI.WP.Button();
            this.TextBox9 = new WinPaletter.UI.WP.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.GroupBox8 = new WinPaletter.UI.WP.GroupBox();
            this.Button31 = new WinPaletter.UI.WP.Button();
            this.Button32 = new WinPaletter.UI.WP.Button();
            this.Button33 = new WinPaletter.UI.WP.Button();
            this.TextBox8 = new WinPaletter.UI.WP.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.GroupBox7 = new WinPaletter.UI.WP.GroupBox();
            this.Button28 = new WinPaletter.UI.WP.Button();
            this.Button29 = new WinPaletter.UI.WP.Button();
            this.Button30 = new WinPaletter.UI.WP.Button();
            this.TextBox7 = new WinPaletter.UI.WP.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.GroupBox55 = new WinPaletter.UI.WP.GroupBox();
            this.Button169 = new WinPaletter.UI.WP.Button();
            this.Button170 = new WinPaletter.UI.WP.Button();
            this.Button171 = new WinPaletter.UI.WP.Button();
            this.TextBox54 = new WinPaletter.UI.WP.TextBox();
            this.Label55 = new System.Windows.Forms.Label();
            this.PictureBox54 = new System.Windows.Forms.PictureBox();
            this.Separator2 = new WinPaletter.UI.WP.SeparatorH();
            this.GroupBox54 = new WinPaletter.UI.WP.GroupBox();
            this.Button166 = new WinPaletter.UI.WP.Button();
            this.Button167 = new WinPaletter.UI.WP.Button();
            this.Button168 = new WinPaletter.UI.WP.Button();
            this.TextBox53 = new WinPaletter.UI.WP.TextBox();
            this.Label54 = new System.Windows.Forms.Label();
            this.PictureBox53 = new System.Windows.Forms.PictureBox();
            this.GroupBox18 = new WinPaletter.UI.WP.GroupBox();
            this.Button58 = new WinPaletter.UI.WP.Button();
            this.Button59 = new WinPaletter.UI.WP.Button();
            this.Button60 = new WinPaletter.UI.WP.Button();
            this.TextBox17 = new WinPaletter.UI.WP.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.PictureBox17 = new System.Windows.Forms.PictureBox();
            this.GroupBox13 = new WinPaletter.UI.WP.GroupBox();
            this.Button43 = new WinPaletter.UI.WP.Button();
            this.Button44 = new WinPaletter.UI.WP.Button();
            this.Button45 = new WinPaletter.UI.WP.Button();
            this.TextBox12 = new WinPaletter.UI.WP.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.PictureBox12 = new System.Windows.Forms.PictureBox();
            this.GroupBox14 = new WinPaletter.UI.WP.GroupBox();
            this.Button46 = new WinPaletter.UI.WP.Button();
            this.Button47 = new WinPaletter.UI.WP.Button();
            this.Button48 = new WinPaletter.UI.WP.Button();
            this.TextBox13 = new WinPaletter.UI.WP.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.PictureBox13 = new System.Windows.Forms.PictureBox();
            this.GroupBox15 = new WinPaletter.UI.WP.GroupBox();
            this.Button49 = new WinPaletter.UI.WP.Button();
            this.Button50 = new WinPaletter.UI.WP.Button();
            this.Button51 = new WinPaletter.UI.WP.Button();
            this.TextBox14 = new WinPaletter.UI.WP.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.PictureBox14 = new System.Windows.Forms.PictureBox();
            this.GroupBox16 = new WinPaletter.UI.WP.GroupBox();
            this.Button52 = new WinPaletter.UI.WP.Button();
            this.Button53 = new WinPaletter.UI.WP.Button();
            this.Button54 = new WinPaletter.UI.WP.Button();
            this.TextBox15 = new WinPaletter.UI.WP.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.PictureBox15 = new System.Windows.Forms.PictureBox();
            this.GroupBox17 = new WinPaletter.UI.WP.GroupBox();
            this.Button55 = new WinPaletter.UI.WP.Button();
            this.Button56 = new WinPaletter.UI.WP.Button();
            this.Button57 = new WinPaletter.UI.WP.Button();
            this.TextBox16 = new WinPaletter.UI.WP.TextBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox87 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox94 = new System.Windows.Forms.PictureBox();
            this.button263 = new WinPaletter.UI.WP.Button();
            this.button264 = new WinPaletter.UI.WP.Button();
            this.button265 = new WinPaletter.UI.WP.Button();
            this.textBox85 = new WinPaletter.UI.WP.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.pictureBox86 = new System.Windows.Forms.PictureBox();
            this.GroupBox86 = new WinPaletter.UI.WP.GroupBox();
            this.Button260 = new WinPaletter.UI.WP.Button();
            this.pictureBox93 = new System.Windows.Forms.PictureBox();
            this.Button261 = new WinPaletter.UI.WP.Button();
            this.Button262 = new WinPaletter.UI.WP.Button();
            this.TextBox84 = new WinPaletter.UI.WP.TextBox();
            this.Label86 = new System.Windows.Forms.Label();
            this.PictureBox85 = new System.Windows.Forms.PictureBox();
            this.GroupBox53 = new WinPaletter.UI.WP.GroupBox();
            this.Button163 = new WinPaletter.UI.WP.Button();
            this.Button164 = new WinPaletter.UI.WP.Button();
            this.Button165 = new WinPaletter.UI.WP.Button();
            this.TextBox52 = new WinPaletter.UI.WP.TextBox();
            this.Label53 = new System.Windows.Forms.Label();
            this.PictureBox52 = new System.Windows.Forms.PictureBox();
            this.GroupBox51 = new WinPaletter.UI.WP.GroupBox();
            this.Button157 = new WinPaletter.UI.WP.Button();
            this.Button158 = new WinPaletter.UI.WP.Button();
            this.Button159 = new WinPaletter.UI.WP.Button();
            this.TextBox50 = new WinPaletter.UI.WP.TextBox();
            this.Label51 = new System.Windows.Forms.Label();
            this.PictureBox50 = new System.Windows.Forms.PictureBox();
            this.GroupBox50 = new WinPaletter.UI.WP.GroupBox();
            this.Button154 = new WinPaletter.UI.WP.Button();
            this.Button155 = new WinPaletter.UI.WP.Button();
            this.Button156 = new WinPaletter.UI.WP.Button();
            this.TextBox49 = new WinPaletter.UI.WP.TextBox();
            this.Label50 = new System.Windows.Forms.Label();
            this.PictureBox49 = new System.Windows.Forms.PictureBox();
            this.GroupBox49 = new WinPaletter.UI.WP.GroupBox();
            this.Button151 = new WinPaletter.UI.WP.Button();
            this.Button152 = new WinPaletter.UI.WP.Button();
            this.Button153 = new WinPaletter.UI.WP.Button();
            this.TextBox48 = new WinPaletter.UI.WP.TextBox();
            this.Label49 = new System.Windows.Forms.Label();
            this.PictureBox48 = new System.Windows.Forms.PictureBox();
            this.GroupBox48 = new WinPaletter.UI.WP.GroupBox();
            this.Button148 = new WinPaletter.UI.WP.Button();
            this.Button149 = new WinPaletter.UI.WP.Button();
            this.Button150 = new WinPaletter.UI.WP.Button();
            this.TextBox47 = new WinPaletter.UI.WP.TextBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.PictureBox47 = new System.Windows.Forms.PictureBox();
            this.GroupBox47 = new WinPaletter.UI.WP.GroupBox();
            this.Button145 = new WinPaletter.UI.WP.Button();
            this.Button146 = new WinPaletter.UI.WP.Button();
            this.Button147 = new WinPaletter.UI.WP.Button();
            this.TextBox46 = new WinPaletter.UI.WP.TextBox();
            this.Label47 = new System.Windows.Forms.Label();
            this.PictureBox46 = new System.Windows.Forms.PictureBox();
            this.GroupBox46 = new WinPaletter.UI.WP.GroupBox();
            this.Button142 = new WinPaletter.UI.WP.Button();
            this.Button143 = new WinPaletter.UI.WP.Button();
            this.Button144 = new WinPaletter.UI.WP.Button();
            this.TextBox45 = new WinPaletter.UI.WP.TextBox();
            this.Label46 = new System.Windows.Forms.Label();
            this.PictureBox45 = new System.Windows.Forms.PictureBox();
            this.TabPage11 = new System.Windows.Forms.TabPage();
            this.GroupBox79 = new WinPaletter.UI.WP.GroupBox();
            this.Button241 = new WinPaletter.UI.WP.Button();
            this.Button242 = new WinPaletter.UI.WP.Button();
            this.Button243 = new WinPaletter.UI.WP.Button();
            this.TextBox78 = new WinPaletter.UI.WP.TextBox();
            this.Label79 = new System.Windows.Forms.Label();
            this.PictureBox78 = new System.Windows.Forms.PictureBox();
            this.GroupBox80 = new WinPaletter.UI.WP.GroupBox();
            this.Button244 = new WinPaletter.UI.WP.Button();
            this.Button245 = new WinPaletter.UI.WP.Button();
            this.Button246 = new WinPaletter.UI.WP.Button();
            this.TextBox79 = new WinPaletter.UI.WP.TextBox();
            this.Label80 = new System.Windows.Forms.Label();
            this.PictureBox79 = new System.Windows.Forms.PictureBox();
            this.GroupBox78 = new WinPaletter.UI.WP.GroupBox();
            this.Button238 = new WinPaletter.UI.WP.Button();
            this.Button239 = new WinPaletter.UI.WP.Button();
            this.Button240 = new WinPaletter.UI.WP.Button();
            this.TextBox77 = new WinPaletter.UI.WP.TextBox();
            this.Label78 = new System.Windows.Forms.Label();
            this.PictureBox77 = new System.Windows.Forms.PictureBox();
            this.GroupBox77 = new WinPaletter.UI.WP.GroupBox();
            this.Button235 = new WinPaletter.UI.WP.Button();
            this.Button236 = new WinPaletter.UI.WP.Button();
            this.Button237 = new WinPaletter.UI.WP.Button();
            this.TextBox76 = new WinPaletter.UI.WP.TextBox();
            this.Label77 = new System.Windows.Forms.Label();
            this.PictureBox76 = new System.Windows.Forms.PictureBox();
            this.GroupBox52 = new WinPaletter.UI.WP.GroupBox();
            this.Button160 = new WinPaletter.UI.WP.Button();
            this.Button161 = new WinPaletter.UI.WP.Button();
            this.Button162 = new WinPaletter.UI.WP.Button();
            this.TextBox51 = new WinPaletter.UI.WP.TextBox();
            this.Label52 = new System.Windows.Forms.Label();
            this.PictureBox51 = new System.Windows.Forms.PictureBox();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.GroupBox76 = new WinPaletter.UI.WP.GroupBox();
            this.Button232 = new WinPaletter.UI.WP.Button();
            this.Button233 = new WinPaletter.UI.WP.Button();
            this.Button234 = new WinPaletter.UI.WP.Button();
            this.TextBox75 = new WinPaletter.UI.WP.TextBox();
            this.Label76 = new System.Windows.Forms.Label();
            this.PictureBox75 = new System.Windows.Forms.PictureBox();
            this.GroupBox64 = new WinPaletter.UI.WP.GroupBox();
            this.Button196 = new WinPaletter.UI.WP.Button();
            this.Button197 = new WinPaletter.UI.WP.Button();
            this.Button198 = new WinPaletter.UI.WP.Button();
            this.TextBox63 = new WinPaletter.UI.WP.TextBox();
            this.Label64 = new System.Windows.Forms.Label();
            this.PictureBox63 = new System.Windows.Forms.PictureBox();
            this.GroupBox57 = new WinPaletter.UI.WP.GroupBox();
            this.Button175 = new WinPaletter.UI.WP.Button();
            this.Button176 = new WinPaletter.UI.WP.Button();
            this.Button177 = new WinPaletter.UI.WP.Button();
            this.TextBox56 = new WinPaletter.UI.WP.TextBox();
            this.Label57 = new System.Windows.Forms.Label();
            this.PictureBox56 = new System.Windows.Forms.PictureBox();
            this.GroupBox58 = new WinPaletter.UI.WP.GroupBox();
            this.Button178 = new WinPaletter.UI.WP.Button();
            this.Button179 = new WinPaletter.UI.WP.Button();
            this.Button180 = new WinPaletter.UI.WP.Button();
            this.TextBox57 = new WinPaletter.UI.WP.TextBox();
            this.Label58 = new System.Windows.Forms.Label();
            this.PictureBox57 = new System.Windows.Forms.PictureBox();
            this.GroupBox59 = new WinPaletter.UI.WP.GroupBox();
            this.Button181 = new WinPaletter.UI.WP.Button();
            this.Button182 = new WinPaletter.UI.WP.Button();
            this.Button183 = new WinPaletter.UI.WP.Button();
            this.TextBox58 = new WinPaletter.UI.WP.TextBox();
            this.Label59 = new System.Windows.Forms.Label();
            this.PictureBox58 = new System.Windows.Forms.PictureBox();
            this.GroupBox60 = new WinPaletter.UI.WP.GroupBox();
            this.Button184 = new WinPaletter.UI.WP.Button();
            this.Button185 = new WinPaletter.UI.WP.Button();
            this.Button186 = new WinPaletter.UI.WP.Button();
            this.TextBox59 = new WinPaletter.UI.WP.TextBox();
            this.Label60 = new System.Windows.Forms.Label();
            this.PictureBox59 = new System.Windows.Forms.PictureBox();
            this.GroupBox61 = new WinPaletter.UI.WP.GroupBox();
            this.Button187 = new WinPaletter.UI.WP.Button();
            this.Button188 = new WinPaletter.UI.WP.Button();
            this.Button189 = new WinPaletter.UI.WP.Button();
            this.TextBox60 = new WinPaletter.UI.WP.TextBox();
            this.Label61 = new System.Windows.Forms.Label();
            this.PictureBox60 = new System.Windows.Forms.PictureBox();
            this.GroupBox62 = new WinPaletter.UI.WP.GroupBox();
            this.Button190 = new WinPaletter.UI.WP.Button();
            this.Button191 = new WinPaletter.UI.WP.Button();
            this.Button192 = new WinPaletter.UI.WP.Button();
            this.TextBox61 = new WinPaletter.UI.WP.TextBox();
            this.Label62 = new System.Windows.Forms.Label();
            this.PictureBox61 = new System.Windows.Forms.PictureBox();
            this.GroupBox63 = new WinPaletter.UI.WP.GroupBox();
            this.Button193 = new WinPaletter.UI.WP.Button();
            this.Button194 = new WinPaletter.UI.WP.Button();
            this.Button195 = new WinPaletter.UI.WP.Button();
            this.TextBox62 = new WinPaletter.UI.WP.TextBox();
            this.Label63 = new System.Windows.Forms.Label();
            this.PictureBox62 = new System.Windows.Forms.PictureBox();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.GroupBox66 = new WinPaletter.UI.WP.GroupBox();
            this.Button202 = new WinPaletter.UI.WP.Button();
            this.Button203 = new WinPaletter.UI.WP.Button();
            this.Button204 = new WinPaletter.UI.WP.Button();
            this.TextBox65 = new WinPaletter.UI.WP.TextBox();
            this.Label66 = new System.Windows.Forms.Label();
            this.PictureBox65 = new System.Windows.Forms.PictureBox();
            this.GroupBox56 = new WinPaletter.UI.WP.GroupBox();
            this.Button172 = new WinPaletter.UI.WP.Button();
            this.Button173 = new WinPaletter.UI.WP.Button();
            this.Button174 = new WinPaletter.UI.WP.Button();
            this.TextBox55 = new WinPaletter.UI.WP.TextBox();
            this.Label56 = new System.Windows.Forms.Label();
            this.PictureBox55 = new System.Windows.Forms.PictureBox();
            this.GroupBox25 = new WinPaletter.UI.WP.GroupBox();
            this.Button79 = new WinPaletter.UI.WP.Button();
            this.Button80 = new WinPaletter.UI.WP.Button();
            this.Button81 = new WinPaletter.UI.WP.Button();
            this.TextBox24 = new WinPaletter.UI.WP.TextBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.PictureBox24 = new System.Windows.Forms.PictureBox();
            this.GroupBox19 = new WinPaletter.UI.WP.GroupBox();
            this.Button61 = new WinPaletter.UI.WP.Button();
            this.Button62 = new WinPaletter.UI.WP.Button();
            this.Button63 = new WinPaletter.UI.WP.Button();
            this.TextBox18 = new WinPaletter.UI.WP.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.PictureBox18 = new System.Windows.Forms.PictureBox();
            this.GroupBox20 = new WinPaletter.UI.WP.GroupBox();
            this.Button64 = new WinPaletter.UI.WP.Button();
            this.Button65 = new WinPaletter.UI.WP.Button();
            this.Button66 = new WinPaletter.UI.WP.Button();
            this.TextBox19 = new WinPaletter.UI.WP.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.PictureBox19 = new System.Windows.Forms.PictureBox();
            this.GroupBox21 = new WinPaletter.UI.WP.GroupBox();
            this.Button67 = new WinPaletter.UI.WP.Button();
            this.Button68 = new WinPaletter.UI.WP.Button();
            this.Button69 = new WinPaletter.UI.WP.Button();
            this.TextBox20 = new WinPaletter.UI.WP.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.PictureBox20 = new System.Windows.Forms.PictureBox();
            this.GroupBox22 = new WinPaletter.UI.WP.GroupBox();
            this.Button70 = new WinPaletter.UI.WP.Button();
            this.Button71 = new WinPaletter.UI.WP.Button();
            this.Button72 = new WinPaletter.UI.WP.Button();
            this.TextBox21 = new WinPaletter.UI.WP.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.PictureBox21 = new System.Windows.Forms.PictureBox();
            this.GroupBox23 = new WinPaletter.UI.WP.GroupBox();
            this.Button73 = new WinPaletter.UI.WP.Button();
            this.Button74 = new WinPaletter.UI.WP.Button();
            this.Button75 = new WinPaletter.UI.WP.Button();
            this.TextBox22 = new WinPaletter.UI.WP.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.PictureBox22 = new System.Windows.Forms.PictureBox();
            this.GroupBox24 = new WinPaletter.UI.WP.GroupBox();
            this.Button76 = new WinPaletter.UI.WP.Button();
            this.Button77 = new WinPaletter.UI.WP.Button();
            this.Button78 = new WinPaletter.UI.WP.Button();
            this.TextBox23 = new WinPaletter.UI.WP.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.PictureBox23 = new System.Windows.Forms.PictureBox();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.GroupBox33 = new WinPaletter.UI.WP.GroupBox();
            this.Button103 = new WinPaletter.UI.WP.Button();
            this.Button104 = new WinPaletter.UI.WP.Button();
            this.Button105 = new WinPaletter.UI.WP.Button();
            this.TextBox32 = new WinPaletter.UI.WP.TextBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.PictureBox32 = new System.Windows.Forms.PictureBox();
            this.GroupBox34 = new WinPaletter.UI.WP.GroupBox();
            this.Button106 = new WinPaletter.UI.WP.Button();
            this.Button107 = new WinPaletter.UI.WP.Button();
            this.Button108 = new WinPaletter.UI.WP.Button();
            this.TextBox33 = new WinPaletter.UI.WP.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.PictureBox33 = new System.Windows.Forms.PictureBox();
            this.GroupBox35 = new WinPaletter.UI.WP.GroupBox();
            this.Button109 = new WinPaletter.UI.WP.Button();
            this.Button110 = new WinPaletter.UI.WP.Button();
            this.Button111 = new WinPaletter.UI.WP.Button();
            this.TextBox34 = new WinPaletter.UI.WP.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.PictureBox34 = new System.Windows.Forms.PictureBox();
            this.GroupBox26 = new WinPaletter.UI.WP.GroupBox();
            this.Button82 = new WinPaletter.UI.WP.Button();
            this.Button83 = new WinPaletter.UI.WP.Button();
            this.Button84 = new WinPaletter.UI.WP.Button();
            this.TextBox25 = new WinPaletter.UI.WP.TextBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.PictureBox25 = new System.Windows.Forms.PictureBox();
            this.GroupBox27 = new WinPaletter.UI.WP.GroupBox();
            this.Button85 = new WinPaletter.UI.WP.Button();
            this.Button86 = new WinPaletter.UI.WP.Button();
            this.Button87 = new WinPaletter.UI.WP.Button();
            this.TextBox26 = new WinPaletter.UI.WP.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.PictureBox26 = new System.Windows.Forms.PictureBox();
            this.GroupBox28 = new WinPaletter.UI.WP.GroupBox();
            this.Button88 = new WinPaletter.UI.WP.Button();
            this.Button89 = new WinPaletter.UI.WP.Button();
            this.Button90 = new WinPaletter.UI.WP.Button();
            this.TextBox27 = new WinPaletter.UI.WP.TextBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.PictureBox27 = new System.Windows.Forms.PictureBox();
            this.GroupBox29 = new WinPaletter.UI.WP.GroupBox();
            this.Button91 = new WinPaletter.UI.WP.Button();
            this.Button92 = new WinPaletter.UI.WP.Button();
            this.Button93 = new WinPaletter.UI.WP.Button();
            this.TextBox28 = new WinPaletter.UI.WP.TextBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.PictureBox28 = new System.Windows.Forms.PictureBox();
            this.GroupBox30 = new WinPaletter.UI.WP.GroupBox();
            this.Button94 = new WinPaletter.UI.WP.Button();
            this.Button95 = new WinPaletter.UI.WP.Button();
            this.Button96 = new WinPaletter.UI.WP.Button();
            this.TextBox29 = new WinPaletter.UI.WP.TextBox();
            this.Label30 = new System.Windows.Forms.Label();
            this.PictureBox29 = new System.Windows.Forms.PictureBox();
            this.GroupBox31 = new WinPaletter.UI.WP.GroupBox();
            this.Button97 = new WinPaletter.UI.WP.Button();
            this.Button98 = new WinPaletter.UI.WP.Button();
            this.Button99 = new WinPaletter.UI.WP.Button();
            this.TextBox30 = new WinPaletter.UI.WP.TextBox();
            this.Label31 = new System.Windows.Forms.Label();
            this.PictureBox30 = new System.Windows.Forms.PictureBox();
            this.GroupBox32 = new WinPaletter.UI.WP.GroupBox();
            this.Button100 = new WinPaletter.UI.WP.Button();
            this.Button101 = new WinPaletter.UI.WP.Button();
            this.Button102 = new WinPaletter.UI.WP.Button();
            this.TextBox31 = new WinPaletter.UI.WP.TextBox();
            this.Label32 = new System.Windows.Forms.Label();
            this.PictureBox31 = new System.Windows.Forms.PictureBox();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.GroupBox36 = new WinPaletter.UI.WP.GroupBox();
            this.Button112 = new WinPaletter.UI.WP.Button();
            this.Button113 = new WinPaletter.UI.WP.Button();
            this.Button114 = new WinPaletter.UI.WP.Button();
            this.TextBox35 = new WinPaletter.UI.WP.TextBox();
            this.Label36 = new System.Windows.Forms.Label();
            this.PictureBox35 = new System.Windows.Forms.PictureBox();
            this.GroupBox37 = new WinPaletter.UI.WP.GroupBox();
            this.Button115 = new WinPaletter.UI.WP.Button();
            this.Button116 = new WinPaletter.UI.WP.Button();
            this.Button117 = new WinPaletter.UI.WP.Button();
            this.TextBox36 = new WinPaletter.UI.WP.TextBox();
            this.Label37 = new System.Windows.Forms.Label();
            this.PictureBox36 = new System.Windows.Forms.PictureBox();
            this.GroupBox38 = new WinPaletter.UI.WP.GroupBox();
            this.Button118 = new WinPaletter.UI.WP.Button();
            this.Button119 = new WinPaletter.UI.WP.Button();
            this.Button120 = new WinPaletter.UI.WP.Button();
            this.TextBox37 = new WinPaletter.UI.WP.TextBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.PictureBox37 = new System.Windows.Forms.PictureBox();
            this.GroupBox39 = new WinPaletter.UI.WP.GroupBox();
            this.Button121 = new WinPaletter.UI.WP.Button();
            this.Button122 = new WinPaletter.UI.WP.Button();
            this.Button123 = new WinPaletter.UI.WP.Button();
            this.TextBox38 = new WinPaletter.UI.WP.TextBox();
            this.Label39 = new System.Windows.Forms.Label();
            this.PictureBox38 = new System.Windows.Forms.PictureBox();
            this.GroupBox40 = new WinPaletter.UI.WP.GroupBox();
            this.Button124 = new WinPaletter.UI.WP.Button();
            this.Button125 = new WinPaletter.UI.WP.Button();
            this.Button126 = new WinPaletter.UI.WP.Button();
            this.TextBox39 = new WinPaletter.UI.WP.TextBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.PictureBox39 = new System.Windows.Forms.PictureBox();
            this.GroupBox41 = new WinPaletter.UI.WP.GroupBox();
            this.Button127 = new WinPaletter.UI.WP.Button();
            this.Button128 = new WinPaletter.UI.WP.Button();
            this.Button129 = new WinPaletter.UI.WP.Button();
            this.TextBox40 = new WinPaletter.UI.WP.TextBox();
            this.Label41 = new System.Windows.Forms.Label();
            this.PictureBox40 = new System.Windows.Forms.PictureBox();
            this.GroupBox42 = new WinPaletter.UI.WP.GroupBox();
            this.Button130 = new WinPaletter.UI.WP.Button();
            this.Button131 = new WinPaletter.UI.WP.Button();
            this.Button132 = new WinPaletter.UI.WP.Button();
            this.TextBox41 = new WinPaletter.UI.WP.TextBox();
            this.Label42 = new System.Windows.Forms.Label();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.GroupBox43 = new WinPaletter.UI.WP.GroupBox();
            this.Button133 = new WinPaletter.UI.WP.Button();
            this.Button134 = new WinPaletter.UI.WP.Button();
            this.Button135 = new WinPaletter.UI.WP.Button();
            this.TextBox42 = new WinPaletter.UI.WP.TextBox();
            this.Label43 = new System.Windows.Forms.Label();
            this.PictureBox42 = new System.Windows.Forms.PictureBox();
            this.GroupBox44 = new WinPaletter.UI.WP.GroupBox();
            this.Button136 = new WinPaletter.UI.WP.Button();
            this.Button137 = new WinPaletter.UI.WP.Button();
            this.Button138 = new WinPaletter.UI.WP.Button();
            this.TextBox43 = new WinPaletter.UI.WP.TextBox();
            this.Label44 = new System.Windows.Forms.Label();
            this.PictureBox43 = new System.Windows.Forms.PictureBox();
            this.GroupBox45 = new WinPaletter.UI.WP.GroupBox();
            this.Button139 = new WinPaletter.UI.WP.Button();
            this.Button140 = new WinPaletter.UI.WP.Button();
            this.Button141 = new WinPaletter.UI.WP.Button();
            this.TextBox44 = new WinPaletter.UI.WP.TextBox();
            this.Label45 = new System.Windows.Forms.Label();
            this.PictureBox44 = new System.Windows.Forms.PictureBox();
            this.TabPage10 = new System.Windows.Forms.TabPage();
            this.GroupBox70 = new WinPaletter.UI.WP.GroupBox();
            this.Button214 = new WinPaletter.UI.WP.Button();
            this.Button215 = new WinPaletter.UI.WP.Button();
            this.Button216 = new WinPaletter.UI.WP.Button();
            this.TextBox69 = new WinPaletter.UI.WP.TextBox();
            this.Label70 = new System.Windows.Forms.Label();
            this.PictureBox69 = new System.Windows.Forms.PictureBox();
            this.GroupBox71 = new WinPaletter.UI.WP.GroupBox();
            this.Button217 = new WinPaletter.UI.WP.Button();
            this.Button218 = new WinPaletter.UI.WP.Button();
            this.Button219 = new WinPaletter.UI.WP.Button();
            this.TextBox70 = new WinPaletter.UI.WP.TextBox();
            this.Label71 = new System.Windows.Forms.Label();
            this.PictureBox70 = new System.Windows.Forms.PictureBox();
            this.GroupBox72 = new WinPaletter.UI.WP.GroupBox();
            this.Button220 = new WinPaletter.UI.WP.Button();
            this.Button221 = new WinPaletter.UI.WP.Button();
            this.Button222 = new WinPaletter.UI.WP.Button();
            this.TextBox71 = new WinPaletter.UI.WP.TextBox();
            this.Label72 = new System.Windows.Forms.Label();
            this.PictureBox71 = new System.Windows.Forms.PictureBox();
            this.GroupBox73 = new WinPaletter.UI.WP.GroupBox();
            this.Button223 = new WinPaletter.UI.WP.Button();
            this.Button224 = new WinPaletter.UI.WP.Button();
            this.Button225 = new WinPaletter.UI.WP.Button();
            this.TextBox72 = new WinPaletter.UI.WP.TextBox();
            this.Label73 = new System.Windows.Forms.Label();
            this.PictureBox72 = new System.Windows.Forms.PictureBox();
            this.GroupBox74 = new WinPaletter.UI.WP.GroupBox();
            this.Button226 = new WinPaletter.UI.WP.Button();
            this.Button227 = new WinPaletter.UI.WP.Button();
            this.Button228 = new WinPaletter.UI.WP.Button();
            this.TextBox73 = new WinPaletter.UI.WP.TextBox();
            this.Label74 = new System.Windows.Forms.Label();
            this.PictureBox73 = new System.Windows.Forms.PictureBox();
            this.GroupBox75 = new WinPaletter.UI.WP.GroupBox();
            this.Button229 = new WinPaletter.UI.WP.Button();
            this.Button230 = new WinPaletter.UI.WP.Button();
            this.Button231 = new WinPaletter.UI.WP.Button();
            this.TextBox74 = new WinPaletter.UI.WP.TextBox();
            this.Label75 = new System.Windows.Forms.Label();
            this.PictureBox74 = new System.Windows.Forms.PictureBox();
            this.TabPage12 = new System.Windows.Forms.TabPage();
            this.AlertBox3 = new WinPaletter.UI.WP.AlertBox();
            this.GroupBox81 = new WinPaletter.UI.WP.GroupBox();
            this.Button247 = new WinPaletter.UI.WP.Button();
            this.Button248 = new WinPaletter.UI.WP.Button();
            this.Button249 = new WinPaletter.UI.WP.Button();
            this.TextBox80 = new WinPaletter.UI.WP.TextBox();
            this.Label81 = new System.Windows.Forms.Label();
            this.PictureBox80 = new System.Windows.Forms.PictureBox();
            this.GroupBox82 = new WinPaletter.UI.WP.GroupBox();
            this.Button250 = new WinPaletter.UI.WP.Button();
            this.Button251 = new WinPaletter.UI.WP.Button();
            this.Button252 = new WinPaletter.UI.WP.Button();
            this.TextBox81 = new WinPaletter.UI.WP.TextBox();
            this.Label82 = new System.Windows.Forms.Label();
            this.PictureBox81 = new System.Windows.Forms.PictureBox();
            this.GroupBox83 = new WinPaletter.UI.WP.GroupBox();
            this.Button253 = new WinPaletter.UI.WP.Button();
            this.Button254 = new WinPaletter.UI.WP.Button();
            this.Button255 = new WinPaletter.UI.WP.Button();
            this.TextBox82 = new WinPaletter.UI.WP.TextBox();
            this.Label83 = new System.Windows.Forms.Label();
            this.PictureBox82 = new System.Windows.Forms.PictureBox();
            this.GroupBox84 = new WinPaletter.UI.WP.GroupBox();
            this.Button256 = new WinPaletter.UI.WP.Button();
            this.Button257 = new WinPaletter.UI.WP.Button();
            this.Button258 = new WinPaletter.UI.WP.Button();
            this.TextBox83 = new WinPaletter.UI.WP.TextBox();
            this.Label84 = new System.Windows.Forms.Label();
            this.PictureBox83 = new System.Windows.Forms.PictureBox();
            this.TabPage9 = new System.Windows.Forms.TabPage();
            this.GroupBox68 = new WinPaletter.UI.WP.GroupBox();
            this.Button208 = new WinPaletter.UI.WP.Button();
            this.Button209 = new WinPaletter.UI.WP.Button();
            this.Button210 = new WinPaletter.UI.WP.Button();
            this.TextBox67 = new WinPaletter.UI.WP.TextBox();
            this.Label68 = new System.Windows.Forms.Label();
            this.PictureBox67 = new System.Windows.Forms.PictureBox();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.Button259 = new WinPaletter.UI.WP.Button();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.SoundsEnabled = new WinPaletter.UI.WP.Toggle();
            this.checker_img = new System.Windows.Forms.PictureBox();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.groupBox88.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox92)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox87)).BeginInit();
            this.GroupBox85.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox84)).BeginInit();
            this.GroupBox65.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox64)).BeginInit();
            this.GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox90)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox91)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox89)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox88)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.GroupBox67.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox66)).BeginInit();
            this.GroupBox69.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox68)).BeginInit();
            this.GroupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            this.GroupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            this.GroupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            this.GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            this.GroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.GroupBox55.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox54)).BeginInit();
            this.GroupBox54.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox53)).BeginInit();
            this.GroupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).BeginInit();
            this.GroupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).BeginInit();
            this.GroupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).BeginInit();
            this.GroupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).BeginInit();
            this.GroupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).BeginInit();
            this.GroupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            this.TabPage7.SuspendLayout();
            this.groupBox87.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox94)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox86)).BeginInit();
            this.GroupBox86.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox93)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox85)).BeginInit();
            this.GroupBox53.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox52)).BeginInit();
            this.GroupBox51.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox50)).BeginInit();
            this.GroupBox50.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox49)).BeginInit();
            this.GroupBox49.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox48)).BeginInit();
            this.GroupBox48.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox47)).BeginInit();
            this.GroupBox47.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox46)).BeginInit();
            this.GroupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).BeginInit();
            this.TabPage11.SuspendLayout();
            this.GroupBox79.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox78)).BeginInit();
            this.GroupBox80.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox79)).BeginInit();
            this.GroupBox78.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox77)).BeginInit();
            this.GroupBox77.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox76)).BeginInit();
            this.GroupBox52.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox51)).BeginInit();
            this.TabPage8.SuspendLayout();
            this.GroupBox76.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox75)).BeginInit();
            this.GroupBox64.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox63)).BeginInit();
            this.GroupBox57.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox56)).BeginInit();
            this.GroupBox58.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox57)).BeginInit();
            this.GroupBox59.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox58)).BeginInit();
            this.GroupBox60.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox59)).BeginInit();
            this.GroupBox61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox60)).BeginInit();
            this.GroupBox62.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox61)).BeginInit();
            this.GroupBox63.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox62)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.GroupBox66.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox65)).BeginInit();
            this.GroupBox56.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox55)).BeginInit();
            this.GroupBox25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox24)).BeginInit();
            this.GroupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox18)).BeginInit();
            this.GroupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox19)).BeginInit();
            this.GroupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox20)).BeginInit();
            this.GroupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox21)).BeginInit();
            this.GroupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).BeginInit();
            this.GroupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox23)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.GroupBox33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox32)).BeginInit();
            this.GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox33)).BeginInit();
            this.GroupBox35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox34)).BeginInit();
            this.GroupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).BeginInit();
            this.GroupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox26)).BeginInit();
            this.GroupBox28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox27)).BeginInit();
            this.GroupBox29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).BeginInit();
            this.GroupBox30.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox29)).BeginInit();
            this.GroupBox31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox30)).BeginInit();
            this.GroupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox31)).BeginInit();
            this.TabPage6.SuspendLayout();
            this.GroupBox36.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox35)).BeginInit();
            this.GroupBox37.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).BeginInit();
            this.GroupBox38.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox37)).BeginInit();
            this.GroupBox39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox38)).BeginInit();
            this.GroupBox40.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox39)).BeginInit();
            this.GroupBox41.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).BeginInit();
            this.GroupBox42.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            this.GroupBox43.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox42)).BeginInit();
            this.GroupBox44.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox43)).BeginInit();
            this.GroupBox45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox44)).BeginInit();
            this.TabPage10.SuspendLayout();
            this.GroupBox70.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox69)).BeginInit();
            this.GroupBox71.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox70)).BeginInit();
            this.GroupBox72.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox71)).BeginInit();
            this.GroupBox73.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox72)).BeginInit();
            this.GroupBox74.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox73)).BeginInit();
            this.GroupBox75.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox74)).BeginInit();
            this.TabPage12.SuspendLayout();
            this.GroupBox81.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox80)).BeginInit();
            this.GroupBox82.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox81)).BeginInit();
            this.GroupBox83.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox82)).BeginInit();
            this.GroupBox84.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox83)).BeginInit();
            this.TabPage9.SuspendLayout();
            this.GroupBox68.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox67)).BeginInit();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileDialog2
            // 
            this.OpenFileDialog2.DefaultExt = "wav";
            this.OpenFileDialog2.Filter = "WAV Audio File (*.wav)|*.wav";
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // BackgroundWorker1
            // 
            this.BackgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // OpenThemeDialog
            // 
            this.OpenThemeDialog.Filter = "Windows Theme (*.theme)|*.theme|All Files (*.*)|*.*";
            // 
            // alertBox1
            // 
            this.alertBox1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.alertBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.alertBox1.CenterText = false;
            this.alertBox1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.alertBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.alertBox1.Image = ((System.Drawing.Image)(resources.GetObject("alertBox1.Image")));
            this.alertBox1.Location = new System.Drawing.Point(12, 439);
            this.alertBox1.Name = "alertBox1";
            this.alertBox1.Size = new System.Drawing.Size(909, 80);
            this.alertBox1.TabIndex = 214;
            this.alertBox1.TabStop = false;
            this.alertBox1.Text = resources.GetString("alertBox1.Text");
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button10.DrawOnGlass = false;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = ((System.Drawing.Image)(resources.GetObject("Button10.Image")));
            this.Button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button10.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(81)))), ((int)(((byte)(110)))));
            this.Button10.Location = new System.Drawing.Point(620, 525);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(115, 34);
            this.Button10.TabIndex = 213;
            this.Button10.Text = "Quick apply";
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button7.DrawOnGlass = false;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = null;
            this.Button7.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.Button7.Location = new System.Drawing.Point(534, 525);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(80, 34);
            this.Button7.TabIndex = 212;
            this.Button7.Text = "Cancel";
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Button8.DrawOnGlass = false;
            this.Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button8.ForeColor = System.Drawing.Color.White;
            this.Button8.Image = ((System.Drawing.Image)(resources.GetObject("Button8.Image")));
            this.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button8.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(20)))), ((int)(((byte)(64)))));
            this.Button8.Location = new System.Drawing.Point(741, 525);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(180, 34);
            this.Button8.TabIndex = 211;
            this.Button8.Text = "Load into current theme";
            this.Button8.UseVisualStyleBackColor = false;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // TabControl1
            // 
            this.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage7);
            this.TabControl1.Controls.Add(this.TabPage11);
            this.TabControl1.Controls.Add(this.TabPage8);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.Controls.Add(this.TabPage5);
            this.TabControl1.Controls.Add(this.TabPage6);
            this.TabControl1.Controls.Add(this.TabPage10);
            this.TabControl1.Controls.Add(this.TabPage12);
            this.TabControl1.Controls.Add(this.TabPage9);
            this.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TabControl1.ItemSize = new System.Drawing.Size(30, 140);
            this.TabControl1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.TabControl1.Location = new System.Drawing.Point(12, 57);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(909, 376);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.TabIndex = 203;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TabPage1.Controls.Add(this.groupBox88);
            this.TabPage1.Controls.Add(this.Separator1);
            this.TabPage1.Controls.Add(this.Separator3);
            this.TabPage1.Controls.Add(this.GroupBox85);
            this.TabPage1.Controls.Add(this.GroupBox65);
            this.TabPage1.Controls.Add(this.GroupBox5);
            this.TabPage1.Controls.Add(this.GroupBox3);
            this.TabPage1.Controls.Add(this.GroupBox6);
            this.TabPage1.Controls.Add(this.GroupBox4);
            this.TabPage1.Controls.Add(this.GroupBox2);
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Location = new System.Drawing.Point(144, 4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(761, 368);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Main events";
            // 
            // groupBox88
            // 
            this.groupBox88.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox88.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.groupBox88.Controls.Add(this.pictureBox92);
            this.groupBox88.Controls.Add(this.button266);
            this.groupBox88.Controls.Add(this.button267);
            this.groupBox88.Controls.Add(this.button268);
            this.groupBox88.Controls.Add(this.textBox86);
            this.groupBox88.Controls.Add(this.label87);
            this.groupBox88.Controls.Add(this.pictureBox87);
            this.groupBox88.Location = new System.Drawing.Point(4, 273);
            this.groupBox88.Name = "groupBox88";
            this.groupBox88.Size = new System.Drawing.Size(754, 30);
            this.groupBox88.TabIndex = 13;
            // 
            // pictureBox92
            // 
            this.pictureBox92.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox92.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox92.Image")));
            this.pictureBox92.Location = new System.Drawing.Point(30, 3);
            this.pictureBox92.Name = "pictureBox92";
            this.pictureBox92.Size = new System.Drawing.Size(24, 24);
            this.pictureBox92.TabIndex = 118;
            this.pictureBox92.TabStop = false;
            // 
            // button266
            // 
            this.button266.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button266.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.button266.DrawOnGlass = false;
            this.button266.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button266.ForeColor = System.Drawing.Color.White;
            this.button266.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.button266.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.button266.Location = new System.Drawing.Point(639, 3);
            this.button266.Name = "button266";
            this.button266.Size = new System.Drawing.Size(36, 24);
            this.button266.TabIndex = 115;
            this.button266.Tag = "2";
            this.button266.UseVisualStyleBackColor = false;
            // 
            // button267
            // 
            this.button267.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button267.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.button267.DrawOnGlass = false;
            this.button267.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button267.ForeColor = System.Drawing.Color.White;
            this.button267.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.button267.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.button267.Location = new System.Drawing.Point(677, 3);
            this.button267.Name = "button267";
            this.button267.Size = new System.Drawing.Size(36, 24);
            this.button267.TabIndex = 114;
            this.button267.Tag = "1";
            this.button267.UseVisualStyleBackColor = false;
            // 
            // button268
            // 
            this.button268.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button268.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.button268.DrawOnGlass = false;
            this.button268.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button268.ForeColor = System.Drawing.Color.White;
            this.button268.Image = ((System.Drawing.Image)(resources.GetObject("button268.Image")));
            this.button268.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.button268.Location = new System.Drawing.Point(715, 3);
            this.button268.Name = "button268";
            this.button268.Size = new System.Drawing.Size(36, 24);
            this.button268.TabIndex = 113;
            this.button268.Tag = "3";
            this.button268.UseVisualStyleBackColor = false;
            // 
            // textBox86
            // 
            this.textBox86.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox86.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.textBox86.DrawOnGlass = false;
            this.textBox86.ForeColor = System.Drawing.Color.White;
            this.textBox86.Location = new System.Drawing.Point(175, 3);
            this.textBox86.MaxLength = 32767;
            this.textBox86.Multiline = false;
            this.textBox86.Name = "textBox86";
            this.textBox86.ReadOnly = false;
            this.textBox86.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.textBox86.SelectedText = "";
            this.textBox86.SelectionLength = 0;
            this.textBox86.SelectionStart = 0;
            this.textBox86.Size = new System.Drawing.Size(460, 24);
            this.textBox86.TabIndex = 1;
            this.textBox86.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox86.UseSystemPasswordChar = false;
            this.textBox86.WordWrap = true;
            // 
            // label87
            // 
            this.label87.BackColor = System.Drawing.Color.Transparent;
            this.label87.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(60, 4);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(109, 22);
            this.label87.TabIndex = 112;
            this.label87.Text = "Lock:";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox87
            // 
            this.pictureBox87.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox87.Image")));
            this.pictureBox87.Location = new System.Drawing.Point(3, 3);
            this.pictureBox87.Name = "pictureBox87";
            this.pictureBox87.Size = new System.Drawing.Size(24, 24);
            this.pictureBox87.TabIndex = 1;
            this.pictureBox87.TabStop = false;
            // 
            // Separator1
            // 
            this.Separator1.AlternativeLook = false;
            this.Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator1.Location = new System.Drawing.Point(4, 309);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(754, 1);
            this.Separator1.TabIndex = 12;
            this.Separator1.TabStop = false;
            this.Separator1.Text = "Separator1";
            // 
            // Separator3
            // 
            this.Separator3.AlternativeLook = false;
            this.Separator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator3.Location = new System.Drawing.Point(4, 130);
            this.Separator3.Name = "Separator3";
            this.Separator3.Size = new System.Drawing.Size(754, 1);
            this.Separator3.TabIndex = 11;
            this.Separator3.TabStop = false;
            this.Separator3.Text = "Separator3";
            // 
            // GroupBox85
            // 
            this.GroupBox85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox85.Controls.Add(this.Label85);
            this.GroupBox85.Controls.Add(this.PictureBox84);
            this.GroupBox85.Controls.Add(this.CheckBox35_SFC);
            this.GroupBox85.Location = new System.Drawing.Point(4, 71);
            this.GroupBox85.Name = "GroupBox85";
            this.GroupBox85.Size = new System.Drawing.Size(754, 53);
            this.GroupBox85.TabIndex = 10;
            this.GroupBox85.Text = "GroupBox85";
            // 
            // Label85
            // 
            this.Label85.BackColor = System.Drawing.Color.Transparent;
            this.Label85.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label85.Location = new System.Drawing.Point(50, 29);
            this.Label85.Name = "Label85";
            this.Label85.Size = new System.Drawing.Size(701, 20);
            this.Label85.TabIndex = 113;
            this.Label85.Text = "*This item is shared bewteen WinPaletter settings (not a theme option)";
            this.Label85.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox84
            // 
            this.PictureBox84.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox84.Image")));
            this.PictureBox84.Location = new System.Drawing.Point(3, 3);
            this.PictureBox84.Name = "PictureBox84";
            this.PictureBox84.Size = new System.Drawing.Size(24, 24);
            this.PictureBox84.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox84.TabIndex = 52;
            this.PictureBox84.TabStop = false;
            // 
            // CheckBox35_SFC
            // 
            this.CheckBox35_SFC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox35_SFC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.CheckBox35_SFC.Checked = false;
            this.CheckBox35_SFC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheckBox35_SFC.ForeColor = System.Drawing.Color.White;
            this.CheckBox35_SFC.Location = new System.Drawing.Point(33, 3);
            this.CheckBox35_SFC.Name = "CheckBox35_SFC";
            this.CheckBox35_SFC.Size = new System.Drawing.Size(718, 24);
            this.CheckBox35_SFC.TabIndex = 53;
            this.CheckBox35_SFC.Text = "On restoring default startup sound, do a SFC scan on imageres.dll to restore its " +
    "integrity (health) (requires Windows restart)";
            // 
            // GroupBox65
            // 
            this.GroupBox65.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox65.Controls.Add(this.Button199);
            this.GroupBox65.Controls.Add(this.Button200);
            this.GroupBox65.Controls.Add(this.Button201);
            this.GroupBox65.Controls.Add(this.TextBox64);
            this.GroupBox65.Controls.Add(this.Label65);
            this.GroupBox65.Controls.Add(this.PictureBox64);
            this.GroupBox65.Location = new System.Drawing.Point(4, 316);
            this.GroupBox65.Name = "GroupBox65";
            this.GroupBox65.Size = new System.Drawing.Size(754, 30);
            this.GroupBox65.TabIndex = 7;
            // 
            // Button199
            // 
            this.Button199.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button199.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button199.DrawOnGlass = false;
            this.Button199.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button199.ForeColor = System.Drawing.Color.White;
            this.Button199.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button199.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button199.Location = new System.Drawing.Point(639, 3);
            this.Button199.Name = "Button199";
            this.Button199.Size = new System.Drawing.Size(36, 24);
            this.Button199.TabIndex = 115;
            this.Button199.Tag = "2";
            this.Button199.UseVisualStyleBackColor = false;
            // 
            // Button200
            // 
            this.Button200.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button200.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button200.DrawOnGlass = false;
            this.Button200.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button200.ForeColor = System.Drawing.Color.White;
            this.Button200.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button200.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button200.Location = new System.Drawing.Point(677, 3);
            this.Button200.Name = "Button200";
            this.Button200.Size = new System.Drawing.Size(36, 24);
            this.Button200.TabIndex = 114;
            this.Button200.Tag = "1";
            this.Button200.UseVisualStyleBackColor = false;
            // 
            // Button201
            // 
            this.Button201.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button201.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button201.DrawOnGlass = false;
            this.Button201.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button201.ForeColor = System.Drawing.Color.White;
            this.Button201.Image = ((System.Drawing.Image)(resources.GetObject("Button201.Image")));
            this.Button201.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button201.Location = new System.Drawing.Point(715, 3);
            this.Button201.Name = "Button201";
            this.Button201.Size = new System.Drawing.Size(36, 24);
            this.Button201.TabIndex = 113;
            this.Button201.Tag = "3";
            this.Button201.UseVisualStyleBackColor = false;
            // 
            // TextBox64
            // 
            this.TextBox64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox64.DrawOnGlass = false;
            this.TextBox64.ForeColor = System.Drawing.Color.White;
            this.TextBox64.Location = new System.Drawing.Point(175, 3);
            this.TextBox64.MaxLength = 32767;
            this.TextBox64.Multiline = false;
            this.TextBox64.Name = "TextBox64";
            this.TextBox64.ReadOnly = false;
            this.TextBox64.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox64.SelectedText = "";
            this.TextBox64.SelectionLength = 0;
            this.TextBox64.SelectionStart = 0;
            this.TextBox64.Size = new System.Drawing.Size(460, 24);
            this.TextBox64.TabIndex = 1;
            this.TextBox64.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox64.UseSystemPasswordChar = false;
            this.TextBox64.WordWrap = true;
            // 
            // Label65
            // 
            this.Label65.BackColor = System.Drawing.Color.Transparent;
            this.Label65.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label65.Location = new System.Drawing.Point(33, 4);
            this.Label65.Name = "Label65";
            this.Label65.Size = new System.Drawing.Size(101, 22);
            this.Label65.TabIndex = 112;
            this.Label65.Text = "Theme change:";
            this.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox64
            // 
            this.PictureBox64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox64.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox64.Image")));
            this.PictureBox64.Location = new System.Drawing.Point(3, 3);
            this.PictureBox64.Name = "PictureBox64";
            this.PictureBox64.Size = new System.Drawing.Size(24, 24);
            this.PictureBox64.TabIndex = 1;
            this.PictureBox64.TabStop = false;
            // 
            // GroupBox5
            // 
            this.GroupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox5.Controls.Add(this.Button24);
            this.GroupBox5.Controls.Add(this.Button23);
            this.GroupBox5.Controls.Add(this.Button22);
            this.GroupBox5.Controls.Add(this.Button19);
            this.GroupBox5.Controls.Add(this.Button20);
            this.GroupBox5.Controls.Add(this.Button21);
            this.GroupBox5.Controls.Add(this.TextBox2);
            this.GroupBox5.Controls.Add(this.Label5);
            this.GroupBox5.Controls.Add(this.PictureBox5);
            this.GroupBox5.Location = new System.Drawing.Point(4, 3);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(754, 30);
            this.GroupBox5.TabIndex = 4;
            // 
            // Button24
            // 
            this.Button24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button24.DrawOnGlass = false;
            this.Button24.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button24.ForeColor = System.Drawing.Color.White;
            this.Button24.Image = null;
            this.Button24.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button24.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(119)))));
            this.Button24.Location = new System.Drawing.Point(495, 3);
            this.Button24.Name = "Button24";
            this.Button24.Size = new System.Drawing.Size(68, 24);
            this.Button24.TabIndex = 119;
            this.Button24.Tag = "";
            this.Button24.Text = "Current";
            this.Button24.UseVisualStyleBackColor = false;
            this.Button24.Click += new System.EventHandler(this.Button24_Click);
            // 
            // Button23
            // 
            this.Button23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button23.DrawOnGlass = false;
            this.Button23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button23.ForeColor = System.Drawing.Color.White;
            this.Button23.Image = null;
            this.Button23.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button23.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(119)))));
            this.Button23.Location = new System.Drawing.Point(406, 3);
            this.Button23.Name = "Button23";
            this.Button23.Size = new System.Drawing.Size(85, 24);
            this.Button23.TabIndex = 118;
            this.Button23.Tag = "";
            this.Button23.Text = "Play nothing";
            this.Button23.UseVisualStyleBackColor = false;
            this.Button23.Click += new System.EventHandler(this.Button23_Click);
            // 
            // Button22
            // 
            this.Button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button22.DrawOnGlass = false;
            this.Button22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button22.ForeColor = System.Drawing.Color.White;
            this.Button22.Image = null;
            this.Button22.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button22.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(119)))));
            this.Button22.Location = new System.Drawing.Point(567, 3);
            this.Button22.Name = "Button22";
            this.Button22.Size = new System.Drawing.Size(68, 24);
            this.Button22.TabIndex = 117;
            this.Button22.Tag = "";
            this.Button22.Text = "Default";
            this.Button22.UseVisualStyleBackColor = false;
            this.Button22.Click += new System.EventHandler(this.Button22_Click);
            // 
            // Button19
            // 
            this.Button19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button19.DrawOnGlass = false;
            this.Button19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button19.ForeColor = System.Drawing.Color.White;
            this.Button19.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button19.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button19.Location = new System.Drawing.Point(639, 3);
            this.Button19.Name = "Button19";
            this.Button19.Size = new System.Drawing.Size(36, 24);
            this.Button19.TabIndex = 116;
            this.Button19.Tag = "2";
            this.Button19.UseVisualStyleBackColor = false;
            // 
            // Button20
            // 
            this.Button20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button20.DrawOnGlass = false;
            this.Button20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button20.ForeColor = System.Drawing.Color.White;
            this.Button20.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button20.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button20.Location = new System.Drawing.Point(677, 3);
            this.Button20.Name = "Button20";
            this.Button20.Size = new System.Drawing.Size(36, 24);
            this.Button20.TabIndex = 114;
            this.Button20.Tag = "";
            this.Button20.UseVisualStyleBackColor = false;
            this.Button20.Click += new System.EventHandler(this.Button20_Click);
            // 
            // Button21
            // 
            this.Button21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button21.DrawOnGlass = false;
            this.Button21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button21.ForeColor = System.Drawing.Color.White;
            this.Button21.Image = ((System.Drawing.Image)(resources.GetObject("Button21.Image")));
            this.Button21.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button21.Location = new System.Drawing.Point(715, 3);
            this.Button21.Name = "Button21";
            this.Button21.Size = new System.Drawing.Size(36, 24);
            this.Button21.TabIndex = 113;
            this.Button21.Tag = "3";
            this.Button21.UseVisualStyleBackColor = false;
            // 
            // TextBox2
            // 
            this.TextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox2.DrawOnGlass = false;
            this.TextBox2.ForeColor = System.Drawing.Color.White;
            this.TextBox2.Location = new System.Drawing.Point(175, 3);
            this.TextBox2.MaxLength = 32767;
            this.TextBox2.Multiline = false;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = false;
            this.TextBox2.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox2.SelectedText = "";
            this.TextBox2.SelectionLength = 0;
            this.TextBox2.SelectionStart = 0;
            this.TextBox2.Size = new System.Drawing.Size(227, 24);
            this.TextBox2.TabIndex = 1;
            this.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox2.UseSystemPasswordChar = false;
            this.TextBox2.WordWrap = true;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(33, 4);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(136, 22);
            this.Label5.TabIndex = 112;
            this.Label5.Text = "Startup (imageres.dll):";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(3, 3);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.TabIndex = 1;
            this.PictureBox5.TabStop = false;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox3.Controls.Add(this.pictureBox90);
            this.GroupBox3.Controls.Add(this.Button15);
            this.GroupBox3.Controls.Add(this.Button5);
            this.GroupBox3.Controls.Add(this.Button6);
            this.GroupBox3.Controls.Add(this.TextBox5);
            this.GroupBox3.Controls.Add(this.Label3);
            this.GroupBox3.Controls.Add(this.PictureBox3);
            this.GroupBox3.Location = new System.Drawing.Point(4, 205);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(754, 30);
            this.GroupBox3.TabIndex = 3;
            // 
            // pictureBox90
            // 
            this.pictureBox90.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox90.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox90.Image")));
            this.pictureBox90.Location = new System.Drawing.Point(30, 3);
            this.pictureBox90.Name = "pictureBox90";
            this.pictureBox90.Size = new System.Drawing.Size(24, 24);
            this.pictureBox90.TabIndex = 118;
            this.pictureBox90.TabStop = false;
            // 
            // Button15
            // 
            this.Button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button15.DrawOnGlass = false;
            this.Button15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button15.ForeColor = System.Drawing.Color.White;
            this.Button15.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button15.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button15.Location = new System.Drawing.Point(639, 3);
            this.Button15.Name = "Button15";
            this.Button15.Size = new System.Drawing.Size(36, 24);
            this.Button15.TabIndex = 115;
            this.Button15.Tag = "2";
            this.Button15.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button5.DrawOnGlass = false;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button5.Location = new System.Drawing.Point(677, 3);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(36, 24);
            this.Button5.TabIndex = 114;
            this.Button5.Tag = "1";
            this.Button5.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button6.DrawOnGlass = false;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button6.Location = new System.Drawing.Point(715, 3);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(36, 24);
            this.Button6.TabIndex = 113;
            this.Button6.Tag = "3";
            this.Button6.UseVisualStyleBackColor = false;
            // 
            // TextBox5
            // 
            this.TextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox5.DrawOnGlass = false;
            this.TextBox5.ForeColor = System.Drawing.Color.White;
            this.TextBox5.Location = new System.Drawing.Point(175, 3);
            this.TextBox5.MaxLength = 32767;
            this.TextBox5.Multiline = false;
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.ReadOnly = false;
            this.TextBox5.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox5.SelectedText = "";
            this.TextBox5.SelectionLength = 0;
            this.TextBox5.SelectionStart = 0;
            this.TextBox5.Size = new System.Drawing.Size(460, 24);
            this.TextBox5.TabIndex = 1;
            this.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox5.UseSystemPasswordChar = false;
            this.TextBox5.WordWrap = true;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(60, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(109, 22);
            this.Label3.TabIndex = 112;
            this.Label3.Text = "Logon:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(3, 3);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 24);
            this.PictureBox3.TabIndex = 1;
            this.PictureBox3.TabStop = false;
            // 
            // GroupBox6
            // 
            this.GroupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox6.Controls.Add(this.pictureBox91);
            this.GroupBox6.Controls.Add(this.Button25);
            this.GroupBox6.Controls.Add(this.Button26);
            this.GroupBox6.Controls.Add(this.Button27);
            this.GroupBox6.Controls.Add(this.TextBox6);
            this.GroupBox6.Controls.Add(this.Label6);
            this.GroupBox6.Controls.Add(this.PictureBox6);
            this.GroupBox6.Location = new System.Drawing.Point(4, 239);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(754, 30);
            this.GroupBox6.TabIndex = 6;
            // 
            // pictureBox91
            // 
            this.pictureBox91.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox91.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox91.Image")));
            this.pictureBox91.Location = new System.Drawing.Point(30, 3);
            this.pictureBox91.Name = "pictureBox91";
            this.pictureBox91.Size = new System.Drawing.Size(24, 24);
            this.pictureBox91.TabIndex = 118;
            this.pictureBox91.TabStop = false;
            // 
            // Button25
            // 
            this.Button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button25.DrawOnGlass = false;
            this.Button25.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button25.ForeColor = System.Drawing.Color.White;
            this.Button25.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button25.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button25.Location = new System.Drawing.Point(639, 3);
            this.Button25.Name = "Button25";
            this.Button25.Size = new System.Drawing.Size(36, 24);
            this.Button25.TabIndex = 115;
            this.Button25.Tag = "2";
            this.Button25.UseVisualStyleBackColor = false;
            // 
            // Button26
            // 
            this.Button26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button26.DrawOnGlass = false;
            this.Button26.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button26.ForeColor = System.Drawing.Color.White;
            this.Button26.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button26.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button26.Location = new System.Drawing.Point(677, 3);
            this.Button26.Name = "Button26";
            this.Button26.Size = new System.Drawing.Size(36, 24);
            this.Button26.TabIndex = 114;
            this.Button26.Tag = "1";
            this.Button26.UseVisualStyleBackColor = false;
            // 
            // Button27
            // 
            this.Button27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button27.DrawOnGlass = false;
            this.Button27.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button27.ForeColor = System.Drawing.Color.White;
            this.Button27.Image = ((System.Drawing.Image)(resources.GetObject("Button27.Image")));
            this.Button27.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button27.Location = new System.Drawing.Point(715, 3);
            this.Button27.Name = "Button27";
            this.Button27.Size = new System.Drawing.Size(36, 24);
            this.Button27.TabIndex = 113;
            this.Button27.Tag = "3";
            this.Button27.UseVisualStyleBackColor = false;
            // 
            // TextBox6
            // 
            this.TextBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox6.DrawOnGlass = false;
            this.TextBox6.ForeColor = System.Drawing.Color.White;
            this.TextBox6.Location = new System.Drawing.Point(175, 3);
            this.TextBox6.MaxLength = 32767;
            this.TextBox6.Multiline = false;
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.ReadOnly = false;
            this.TextBox6.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox6.SelectedText = "";
            this.TextBox6.SelectionLength = 0;
            this.TextBox6.SelectionStart = 0;
            this.TextBox6.Size = new System.Drawing.Size(460, 24);
            this.TextBox6.TabIndex = 1;
            this.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox6.UseSystemPasswordChar = false;
            this.TextBox6.WordWrap = true;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(60, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(109, 22);
            this.Label6.TabIndex = 112;
            this.Label6.Text = "Unlock:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(3, 3);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(24, 24);
            this.PictureBox6.TabIndex = 1;
            this.PictureBox6.TabStop = false;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox4.Controls.Add(this.pictureBox89);
            this.GroupBox4.Controls.Add(this.Button16);
            this.GroupBox4.Controls.Add(this.Button13);
            this.GroupBox4.Controls.Add(this.Button14);
            this.GroupBox4.Controls.Add(this.TextBox4);
            this.GroupBox4.Controls.Add(this.Label4);
            this.GroupBox4.Controls.Add(this.PictureBox4);
            this.GroupBox4.Location = new System.Drawing.Point(4, 171);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(754, 30);
            this.GroupBox4.TabIndex = 2;
            // 
            // pictureBox89
            // 
            this.pictureBox89.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox89.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox89.Image")));
            this.pictureBox89.Location = new System.Drawing.Point(30, 2);
            this.pictureBox89.Name = "pictureBox89";
            this.pictureBox89.Size = new System.Drawing.Size(24, 24);
            this.pictureBox89.TabIndex = 118;
            this.pictureBox89.TabStop = false;
            // 
            // Button16
            // 
            this.Button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button16.DrawOnGlass = false;
            this.Button16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button16.ForeColor = System.Drawing.Color.White;
            this.Button16.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button16.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button16.Location = new System.Drawing.Point(639, 3);
            this.Button16.Name = "Button16";
            this.Button16.Size = new System.Drawing.Size(36, 24);
            this.Button16.TabIndex = 116;
            this.Button16.Tag = "2";
            this.Button16.UseVisualStyleBackColor = false;
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button13.DrawOnGlass = false;
            this.Button13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button13.ForeColor = System.Drawing.Color.White;
            this.Button13.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button13.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button13.Location = new System.Drawing.Point(677, 3);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(36, 24);
            this.Button13.TabIndex = 114;
            this.Button13.Tag = "1";
            this.Button13.UseVisualStyleBackColor = false;
            // 
            // Button14
            // 
            this.Button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button14.DrawOnGlass = false;
            this.Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button14.ForeColor = System.Drawing.Color.White;
            this.Button14.Image = ((System.Drawing.Image)(resources.GetObject("Button14.Image")));
            this.Button14.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button14.Location = new System.Drawing.Point(715, 3);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(36, 24);
            this.Button14.TabIndex = 113;
            this.Button14.Tag = "3";
            this.Button14.UseVisualStyleBackColor = false;
            // 
            // TextBox4
            // 
            this.TextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox4.DrawOnGlass = false;
            this.TextBox4.ForeColor = System.Drawing.Color.White;
            this.TextBox4.Location = new System.Drawing.Point(175, 3);
            this.TextBox4.MaxLength = 32767;
            this.TextBox4.Multiline = false;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.ReadOnly = false;
            this.TextBox4.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox4.SelectedText = "";
            this.TextBox4.SelectionLength = 0;
            this.TextBox4.SelectionStart = 0;
            this.TextBox4.Size = new System.Drawing.Size(460, 24);
            this.TextBox4.TabIndex = 1;
            this.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox4.UseSystemPasswordChar = false;
            this.TextBox4.WordWrap = true;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(60, 4);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(109, 22);
            this.Label4.TabIndex = 112;
            this.Label4.Text = "Logoff:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(3, 3);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 24);
            this.PictureBox4.TabIndex = 1;
            this.PictureBox4.TabStop = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox2.Controls.Add(this.pictureBox88);
            this.GroupBox2.Controls.Add(this.Button17);
            this.GroupBox2.Controls.Add(this.Button3);
            this.GroupBox2.Controls.Add(this.Button4);
            this.GroupBox2.Controls.Add(this.TextBox3);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.PictureBox2);
            this.GroupBox2.Location = new System.Drawing.Point(4, 137);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(754, 30);
            this.GroupBox2.TabIndex = 1;
            // 
            // pictureBox88
            // 
            this.pictureBox88.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox88.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox88.Image")));
            this.pictureBox88.Location = new System.Drawing.Point(30, 3);
            this.pictureBox88.Name = "pictureBox88";
            this.pictureBox88.Size = new System.Drawing.Size(24, 24);
            this.pictureBox88.TabIndex = 117;
            this.pictureBox88.TabStop = false;
            // 
            // Button17
            // 
            this.Button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button17.DrawOnGlass = false;
            this.Button17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button17.ForeColor = System.Drawing.Color.White;
            this.Button17.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button17.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button17.Location = new System.Drawing.Point(639, 3);
            this.Button17.Name = "Button17";
            this.Button17.Size = new System.Drawing.Size(36, 24);
            this.Button17.TabIndex = 116;
            this.Button17.Tag = "2";
            this.Button17.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button3.DrawOnGlass = false;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button3.Location = new System.Drawing.Point(677, 3);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(36, 24);
            this.Button3.TabIndex = 114;
            this.Button3.Tag = "1";
            this.Button3.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button4.DrawOnGlass = false;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button4.Location = new System.Drawing.Point(715, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(36, 24);
            this.Button4.TabIndex = 113;
            this.Button4.Tag = "3";
            this.Button4.UseVisualStyleBackColor = false;
            // 
            // TextBox3
            // 
            this.TextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox3.DrawOnGlass = false;
            this.TextBox3.ForeColor = System.Drawing.Color.White;
            this.TextBox3.Location = new System.Drawing.Point(175, 3);
            this.TextBox3.MaxLength = 32767;
            this.TextBox3.Multiline = false;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.ReadOnly = false;
            this.TextBox3.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox3.SelectedText = "";
            this.TextBox3.SelectionLength = 0;
            this.TextBox3.SelectionStart = 0;
            this.TextBox3.Size = new System.Drawing.Size(460, 24);
            this.TextBox3.TabIndex = 1;
            this.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox3.UseSystemPasswordChar = false;
            this.TextBox3.WordWrap = true;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(60, 4);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(109, 22);
            this.Label2.TabIndex = 112;
            this.Label2.Text = "Shutdown:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(3, 3);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.TabIndex = 1;
            this.PictureBox2.TabStop = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.GroupBox1.Controls.Add(this.Button18);
            this.GroupBox1.Controls.Add(this.Button2);
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Controls.Add(this.TextBox1);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.PictureBox1);
            this.GroupBox1.Location = new System.Drawing.Point(4, 37);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(754, 30);
            this.GroupBox1.TabIndex = 0;
            // 
            // Button18
            // 
            this.Button18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button18.DrawOnGlass = false;
            this.Button18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button18.ForeColor = System.Drawing.Color.White;
            this.Button18.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button18.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button18.Location = new System.Drawing.Point(639, 3);
            this.Button18.Name = "Button18";
            this.Button18.Size = new System.Drawing.Size(36, 24);
            this.Button18.TabIndex = 116;
            this.Button18.Tag = "2";
            this.Button18.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button2.DrawOnGlass = false;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button2.Location = new System.Drawing.Point(677, 3);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(36, 24);
            this.Button2.TabIndex = 114;
            this.Button2.Tag = "1";
            this.Button2.UseVisualStyleBackColor = false;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button1.DrawOnGlass = false;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button1.Location = new System.Drawing.Point(715, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(36, 24);
            this.Button1.TabIndex = 113;
            this.Button1.Tag = "3";
            this.Button1.UseVisualStyleBackColor = false;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox1.DrawOnGlass = false;
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(175, 3);
            this.TextBox1.MaxLength = 32767;
            this.TextBox1.Multiline = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = false;
            this.TextBox1.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox1.SelectedText = "";
            this.TextBox1.SelectionLength = 0;
            this.TextBox1.SelectionStart = 0;
            this.TextBox1.Size = new System.Drawing.Size(460, 24);
            this.TextBox1.TabIndex = 1;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox1.UseSystemPasswordChar = false;
            this.TextBox1.WordWrap = true;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(33, 4);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(136, 22);
            this.Label1.TabIndex = 112;
            this.Label1.Text = "Startup (registry):";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(24, 24);
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage2.Controls.Add(this.GroupBox67);
            this.TabPage2.Controls.Add(this.GroupBox69);
            this.TabPage2.Controls.Add(this.GroupBox11);
            this.TabPage2.Controls.Add(this.GroupBox10);
            this.TabPage2.Controls.Add(this.GroupBox9);
            this.TabPage2.Controls.Add(this.GroupBox8);
            this.TabPage2.Controls.Add(this.GroupBox7);
            this.TabPage2.Location = new System.Drawing.Point(144, 4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(761, 368);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Dialogs";
            // 
            // GroupBox67
            // 
            this.GroupBox67.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox67.Controls.Add(this.Button205);
            this.GroupBox67.Controls.Add(this.Button206);
            this.GroupBox67.Controls.Add(this.Button207);
            this.GroupBox67.Controls.Add(this.TextBox66);
            this.GroupBox67.Controls.Add(this.Label67);
            this.GroupBox67.Controls.Add(this.PictureBox66);
            this.GroupBox67.Location = new System.Drawing.Point(4, 139);
            this.GroupBox67.Name = "GroupBox67";
            this.GroupBox67.Size = new System.Drawing.Size(754, 30);
            this.GroupBox67.TabIndex = 126;
            // 
            // Button205
            // 
            this.Button205.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button205.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button205.DrawOnGlass = false;
            this.Button205.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button205.ForeColor = System.Drawing.Color.White;
            this.Button205.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button205.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button205.Location = new System.Drawing.Point(639, 3);
            this.Button205.Name = "Button205";
            this.Button205.Size = new System.Drawing.Size(36, 24);
            this.Button205.TabIndex = 115;
            this.Button205.Tag = "2";
            this.Button205.UseVisualStyleBackColor = false;
            // 
            // Button206
            // 
            this.Button206.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button206.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button206.DrawOnGlass = false;
            this.Button206.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button206.ForeColor = System.Drawing.Color.White;
            this.Button206.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button206.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button206.Location = new System.Drawing.Point(677, 3);
            this.Button206.Name = "Button206";
            this.Button206.Size = new System.Drawing.Size(36, 24);
            this.Button206.TabIndex = 114;
            this.Button206.Tag = "1";
            this.Button206.UseVisualStyleBackColor = false;
            // 
            // Button207
            // 
            this.Button207.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button207.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button207.DrawOnGlass = false;
            this.Button207.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button207.ForeColor = System.Drawing.Color.White;
            this.Button207.Image = ((System.Drawing.Image)(resources.GetObject("Button207.Image")));
            this.Button207.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button207.Location = new System.Drawing.Point(715, 3);
            this.Button207.Name = "Button207";
            this.Button207.Size = new System.Drawing.Size(36, 24);
            this.Button207.TabIndex = 113;
            this.Button207.Tag = "3";
            this.Button207.UseVisualStyleBackColor = false;
            // 
            // TextBox66
            // 
            this.TextBox66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox66.DrawOnGlass = false;
            this.TextBox66.ForeColor = System.Drawing.Color.White;
            this.TextBox66.Location = new System.Drawing.Point(195, 3);
            this.TextBox66.MaxLength = 32767;
            this.TextBox66.Multiline = false;
            this.TextBox66.Name = "TextBox66";
            this.TextBox66.ReadOnly = false;
            this.TextBox66.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox66.SelectedText = "";
            this.TextBox66.SelectionLength = 0;
            this.TextBox66.SelectionStart = 0;
            this.TextBox66.Size = new System.Drawing.Size(440, 24);
            this.TextBox66.TabIndex = 1;
            this.TextBox66.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox66.UseSystemPasswordChar = false;
            this.TextBox66.WordWrap = true;
            // 
            // Label67
            // 
            this.Label67.BackColor = System.Drawing.Color.Transparent;
            this.Label67.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label67.Location = new System.Drawing.Point(33, 4);
            this.Label67.Name = "Label67";
            this.Label67.Size = new System.Drawing.Size(156, 22);
            this.Label67.TabIndex = 112;
            this.Label67.Text = "Critical stop:";
            this.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox66
            // 
            this.PictureBox66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox66.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox66.Image")));
            this.PictureBox66.Location = new System.Drawing.Point(3, 3);
            this.PictureBox66.Name = "PictureBox66";
            this.PictureBox66.Size = new System.Drawing.Size(24, 24);
            this.PictureBox66.TabIndex = 1;
            this.PictureBox66.TabStop = false;
            // 
            // GroupBox69
            // 
            this.GroupBox69.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox69.Controls.Add(this.Button211);
            this.GroupBox69.Controls.Add(this.Button212);
            this.GroupBox69.Controls.Add(this.Button213);
            this.GroupBox69.Controls.Add(this.TextBox68);
            this.GroupBox69.Controls.Add(this.Label69);
            this.GroupBox69.Controls.Add(this.PictureBox68);
            this.GroupBox69.Location = new System.Drawing.Point(4, 207);
            this.GroupBox69.Name = "GroupBox69";
            this.GroupBox69.Size = new System.Drawing.Size(754, 30);
            this.GroupBox69.TabIndex = 124;
            // 
            // Button211
            // 
            this.Button211.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button211.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button211.DrawOnGlass = false;
            this.Button211.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button211.ForeColor = System.Drawing.Color.White;
            this.Button211.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button211.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button211.Location = new System.Drawing.Point(639, 3);
            this.Button211.Name = "Button211";
            this.Button211.Size = new System.Drawing.Size(36, 24);
            this.Button211.TabIndex = 115;
            this.Button211.Tag = "2";
            this.Button211.UseVisualStyleBackColor = false;
            // 
            // Button212
            // 
            this.Button212.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button212.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button212.DrawOnGlass = false;
            this.Button212.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button212.ForeColor = System.Drawing.Color.White;
            this.Button212.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button212.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button212.Location = new System.Drawing.Point(677, 3);
            this.Button212.Name = "Button212";
            this.Button212.Size = new System.Drawing.Size(36, 24);
            this.Button212.TabIndex = 114;
            this.Button212.Tag = "1";
            this.Button212.UseVisualStyleBackColor = false;
            // 
            // Button213
            // 
            this.Button213.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button213.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button213.DrawOnGlass = false;
            this.Button213.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button213.ForeColor = System.Drawing.Color.White;
            this.Button213.Image = ((System.Drawing.Image)(resources.GetObject("Button213.Image")));
            this.Button213.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button213.Location = new System.Drawing.Point(715, 3);
            this.Button213.Name = "Button213";
            this.Button213.Size = new System.Drawing.Size(36, 24);
            this.Button213.TabIndex = 113;
            this.Button213.Tag = "3";
            this.Button213.UseVisualStyleBackColor = false;
            // 
            // TextBox68
            // 
            this.TextBox68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox68.DrawOnGlass = false;
            this.TextBox68.ForeColor = System.Drawing.Color.White;
            this.TextBox68.Location = new System.Drawing.Point(195, 3);
            this.TextBox68.MaxLength = 32767;
            this.TextBox68.Multiline = false;
            this.TextBox68.Name = "TextBox68";
            this.TextBox68.ReadOnly = false;
            this.TextBox68.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox68.SelectedText = "";
            this.TextBox68.SelectionLength = 0;
            this.TextBox68.SelectionStart = 0;
            this.TextBox68.Size = new System.Drawing.Size(440, 24);
            this.TextBox68.TabIndex = 1;
            this.TextBox68.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox68.UseSystemPasswordChar = false;
            this.TextBox68.WordWrap = true;
            // 
            // Label69
            // 
            this.Label69.BackColor = System.Drawing.Color.Transparent;
            this.Label69.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label69.Location = new System.Drawing.Point(33, 4);
            this.Label69.Name = "Label69";
            this.Label69.Size = new System.Drawing.Size(156, 22);
            this.Label69.TabIndex = 112;
            this.Label69.Text = "General protection fault:";
            this.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox68
            // 
            this.PictureBox68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox68.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox68.Image")));
            this.PictureBox68.Location = new System.Drawing.Point(3, 3);
            this.PictureBox68.Name = "PictureBox68";
            this.PictureBox68.Size = new System.Drawing.Size(24, 24);
            this.PictureBox68.TabIndex = 1;
            this.PictureBox68.TabStop = false;
            // 
            // GroupBox11
            // 
            this.GroupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox11.Controls.Add(this.Button40);
            this.GroupBox11.Controls.Add(this.Button41);
            this.GroupBox11.Controls.Add(this.Button42);
            this.GroupBox11.Controls.Add(this.TextBox11);
            this.GroupBox11.Controls.Add(this.Label11);
            this.GroupBox11.Controls.Add(this.PictureBox11);
            this.GroupBox11.Location = new System.Drawing.Point(4, 173);
            this.GroupBox11.Name = "GroupBox11";
            this.GroupBox11.Size = new System.Drawing.Size(754, 30);
            this.GroupBox11.TabIndex = 12;
            // 
            // Button40
            // 
            this.Button40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button40.DrawOnGlass = false;
            this.Button40.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button40.ForeColor = System.Drawing.Color.White;
            this.Button40.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button40.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button40.Location = new System.Drawing.Point(639, 3);
            this.Button40.Name = "Button40";
            this.Button40.Size = new System.Drawing.Size(36, 24);
            this.Button40.TabIndex = 115;
            this.Button40.Tag = "2";
            this.Button40.UseVisualStyleBackColor = false;
            // 
            // Button41
            // 
            this.Button41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button41.DrawOnGlass = false;
            this.Button41.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button41.ForeColor = System.Drawing.Color.White;
            this.Button41.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button41.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button41.Location = new System.Drawing.Point(677, 3);
            this.Button41.Name = "Button41";
            this.Button41.Size = new System.Drawing.Size(36, 24);
            this.Button41.TabIndex = 114;
            this.Button41.Tag = "1";
            this.Button41.UseVisualStyleBackColor = false;
            // 
            // Button42
            // 
            this.Button42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button42.DrawOnGlass = false;
            this.Button42.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button42.ForeColor = System.Drawing.Color.White;
            this.Button42.Image = ((System.Drawing.Image)(resources.GetObject("Button42.Image")));
            this.Button42.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button42.Location = new System.Drawing.Point(715, 3);
            this.Button42.Name = "Button42";
            this.Button42.Size = new System.Drawing.Size(36, 24);
            this.Button42.TabIndex = 113;
            this.Button42.Tag = "3";
            this.Button42.UseVisualStyleBackColor = false;
            // 
            // TextBox11
            // 
            this.TextBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox11.DrawOnGlass = false;
            this.TextBox11.ForeColor = System.Drawing.Color.White;
            this.TextBox11.Location = new System.Drawing.Point(195, 3);
            this.TextBox11.MaxLength = 32767;
            this.TextBox11.Multiline = false;
            this.TextBox11.Name = "TextBox11";
            this.TextBox11.ReadOnly = false;
            this.TextBox11.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox11.SelectedText = "";
            this.TextBox11.SelectionLength = 0;
            this.TextBox11.SelectionStart = 0;
            this.TextBox11.Size = new System.Drawing.Size(440, 24);
            this.TextBox11.TabIndex = 1;
            this.TextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox11.UseSystemPasswordChar = false;
            this.TextBox11.WordWrap = true;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(33, 4);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(156, 22);
            this.Label11.TabIndex = 112;
            this.Label11.Text = "UAC:";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox11
            // 
            this.PictureBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(3, 3);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(24, 24);
            this.PictureBox11.TabIndex = 1;
            this.PictureBox11.TabStop = false;
            // 
            // GroupBox10
            // 
            this.GroupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox10.Controls.Add(this.Button37);
            this.GroupBox10.Controls.Add(this.Button38);
            this.GroupBox10.Controls.Add(this.Button39);
            this.GroupBox10.Controls.Add(this.TextBox10);
            this.GroupBox10.Controls.Add(this.Label10);
            this.GroupBox10.Controls.Add(this.PictureBox10);
            this.GroupBox10.Location = new System.Drawing.Point(4, 3);
            this.GroupBox10.Name = "GroupBox10";
            this.GroupBox10.Size = new System.Drawing.Size(754, 30);
            this.GroupBox10.TabIndex = 11;
            // 
            // Button37
            // 
            this.Button37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button37.DrawOnGlass = false;
            this.Button37.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button37.ForeColor = System.Drawing.Color.White;
            this.Button37.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button37.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button37.Location = new System.Drawing.Point(639, 3);
            this.Button37.Name = "Button37";
            this.Button37.Size = new System.Drawing.Size(36, 24);
            this.Button37.TabIndex = 115;
            this.Button37.Tag = "2";
            this.Button37.UseVisualStyleBackColor = false;
            // 
            // Button38
            // 
            this.Button38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button38.DrawOnGlass = false;
            this.Button38.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button38.ForeColor = System.Drawing.Color.White;
            this.Button38.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button38.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button38.Location = new System.Drawing.Point(677, 3);
            this.Button38.Name = "Button38";
            this.Button38.Size = new System.Drawing.Size(36, 24);
            this.Button38.TabIndex = 114;
            this.Button38.Tag = "1";
            this.Button38.UseVisualStyleBackColor = false;
            // 
            // Button39
            // 
            this.Button39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button39.DrawOnGlass = false;
            this.Button39.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button39.ForeColor = System.Drawing.Color.White;
            this.Button39.Image = ((System.Drawing.Image)(resources.GetObject("Button39.Image")));
            this.Button39.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button39.Location = new System.Drawing.Point(715, 3);
            this.Button39.Name = "Button39";
            this.Button39.Size = new System.Drawing.Size(36, 24);
            this.Button39.TabIndex = 113;
            this.Button39.Tag = "3";
            this.Button39.UseVisualStyleBackColor = false;
            // 
            // TextBox10
            // 
            this.TextBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox10.DrawOnGlass = false;
            this.TextBox10.ForeColor = System.Drawing.Color.White;
            this.TextBox10.Location = new System.Drawing.Point(195, 3);
            this.TextBox10.MaxLength = 32767;
            this.TextBox10.Multiline = false;
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.ReadOnly = false;
            this.TextBox10.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox10.SelectedText = "";
            this.TextBox10.SelectionLength = 0;
            this.TextBox10.SelectionStart = 0;
            this.TextBox10.Size = new System.Drawing.Size(440, 24);
            this.TextBox10.TabIndex = 1;
            this.TextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox10.UseSystemPasswordChar = false;
            this.TextBox10.WordWrap = true;
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.Transparent;
            this.Label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(33, 4);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(156, 22);
            this.Label10.TabIndex = 112;
            this.Label10.Text = "Information:";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox10
            // 
            this.PictureBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(3, 3);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(24, 24);
            this.PictureBox10.TabIndex = 1;
            this.PictureBox10.TabStop = false;
            // 
            // GroupBox9
            // 
            this.GroupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox9.Controls.Add(this.Button34);
            this.GroupBox9.Controls.Add(this.Button35);
            this.GroupBox9.Controls.Add(this.Button36);
            this.GroupBox9.Controls.Add(this.TextBox9);
            this.GroupBox9.Controls.Add(this.Label9);
            this.GroupBox9.Controls.Add(this.PictureBox9);
            this.GroupBox9.Location = new System.Drawing.Point(4, 105);
            this.GroupBox9.Name = "GroupBox9";
            this.GroupBox9.Size = new System.Drawing.Size(754, 30);
            this.GroupBox9.TabIndex = 10;
            // 
            // Button34
            // 
            this.Button34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button34.DrawOnGlass = false;
            this.Button34.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button34.ForeColor = System.Drawing.Color.White;
            this.Button34.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button34.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button34.Location = new System.Drawing.Point(639, 3);
            this.Button34.Name = "Button34";
            this.Button34.Size = new System.Drawing.Size(36, 24);
            this.Button34.TabIndex = 115;
            this.Button34.Tag = "2";
            this.Button34.UseVisualStyleBackColor = false;
            // 
            // Button35
            // 
            this.Button35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button35.DrawOnGlass = false;
            this.Button35.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button35.ForeColor = System.Drawing.Color.White;
            this.Button35.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button35.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button35.Location = new System.Drawing.Point(677, 3);
            this.Button35.Name = "Button35";
            this.Button35.Size = new System.Drawing.Size(36, 24);
            this.Button35.TabIndex = 114;
            this.Button35.Tag = "1";
            this.Button35.UseVisualStyleBackColor = false;
            // 
            // Button36
            // 
            this.Button36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button36.DrawOnGlass = false;
            this.Button36.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button36.ForeColor = System.Drawing.Color.White;
            this.Button36.Image = ((System.Drawing.Image)(resources.GetObject("Button36.Image")));
            this.Button36.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button36.Location = new System.Drawing.Point(715, 3);
            this.Button36.Name = "Button36";
            this.Button36.Size = new System.Drawing.Size(36, 24);
            this.Button36.TabIndex = 113;
            this.Button36.Tag = "3";
            this.Button36.UseVisualStyleBackColor = false;
            // 
            // TextBox9
            // 
            this.TextBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox9.DrawOnGlass = false;
            this.TextBox9.ForeColor = System.Drawing.Color.White;
            this.TextBox9.Location = new System.Drawing.Point(195, 3);
            this.TextBox9.MaxLength = 32767;
            this.TextBox9.Multiline = false;
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.ReadOnly = false;
            this.TextBox9.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox9.SelectedText = "";
            this.TextBox9.SelectionLength = 0;
            this.TextBox9.SelectionStart = 0;
            this.TextBox9.Size = new System.Drawing.Size(440, 24);
            this.TextBox9.TabIndex = 1;
            this.TextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox9.UseSystemPasswordChar = false;
            this.TextBox9.WordWrap = true;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(33, 4);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(156, 22);
            this.Label9.TabIndex = 112;
            this.Label9.Text = "Asterisk:";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox9
            // 
            this.PictureBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(3, 3);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.TabIndex = 1;
            this.PictureBox9.TabStop = false;
            // 
            // GroupBox8
            // 
            this.GroupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox8.Controls.Add(this.Button31);
            this.GroupBox8.Controls.Add(this.Button32);
            this.GroupBox8.Controls.Add(this.Button33);
            this.GroupBox8.Controls.Add(this.TextBox8);
            this.GroupBox8.Controls.Add(this.Label8);
            this.GroupBox8.Controls.Add(this.PictureBox8);
            this.GroupBox8.Location = new System.Drawing.Point(4, 71);
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.Size = new System.Drawing.Size(754, 30);
            this.GroupBox8.TabIndex = 9;
            // 
            // Button31
            // 
            this.Button31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button31.DrawOnGlass = false;
            this.Button31.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button31.ForeColor = System.Drawing.Color.White;
            this.Button31.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button31.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button31.Location = new System.Drawing.Point(639, 3);
            this.Button31.Name = "Button31";
            this.Button31.Size = new System.Drawing.Size(36, 24);
            this.Button31.TabIndex = 115;
            this.Button31.Tag = "2";
            this.Button31.UseVisualStyleBackColor = false;
            // 
            // Button32
            // 
            this.Button32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button32.DrawOnGlass = false;
            this.Button32.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button32.ForeColor = System.Drawing.Color.White;
            this.Button32.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button32.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button32.Location = new System.Drawing.Point(677, 3);
            this.Button32.Name = "Button32";
            this.Button32.Size = new System.Drawing.Size(36, 24);
            this.Button32.TabIndex = 114;
            this.Button32.Tag = "1";
            this.Button32.UseVisualStyleBackColor = false;
            // 
            // Button33
            // 
            this.Button33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button33.DrawOnGlass = false;
            this.Button33.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button33.ForeColor = System.Drawing.Color.White;
            this.Button33.Image = ((System.Drawing.Image)(resources.GetObject("Button33.Image")));
            this.Button33.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button33.Location = new System.Drawing.Point(715, 3);
            this.Button33.Name = "Button33";
            this.Button33.Size = new System.Drawing.Size(36, 24);
            this.Button33.TabIndex = 113;
            this.Button33.Tag = "3";
            this.Button33.UseVisualStyleBackColor = false;
            // 
            // TextBox8
            // 
            this.TextBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox8.DrawOnGlass = false;
            this.TextBox8.ForeColor = System.Drawing.Color.White;
            this.TextBox8.Location = new System.Drawing.Point(195, 3);
            this.TextBox8.MaxLength = 32767;
            this.TextBox8.Multiline = false;
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.ReadOnly = false;
            this.TextBox8.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox8.SelectedText = "";
            this.TextBox8.SelectionLength = 0;
            this.TextBox8.SelectionStart = 0;
            this.TextBox8.Size = new System.Drawing.Size(440, 24);
            this.TextBox8.TabIndex = 1;
            this.TextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox8.UseSystemPasswordChar = false;
            this.TextBox8.WordWrap = true;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(33, 4);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(156, 22);
            this.Label8.TabIndex = 112;
            this.Label8.Text = "Exclamation:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox8
            // 
            this.PictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(3, 3);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 24);
            this.PictureBox8.TabIndex = 1;
            this.PictureBox8.TabStop = false;
            // 
            // GroupBox7
            // 
            this.GroupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox7.Controls.Add(this.Button28);
            this.GroupBox7.Controls.Add(this.Button29);
            this.GroupBox7.Controls.Add(this.Button30);
            this.GroupBox7.Controls.Add(this.TextBox7);
            this.GroupBox7.Controls.Add(this.Label7);
            this.GroupBox7.Controls.Add(this.PictureBox7);
            this.GroupBox7.Location = new System.Drawing.Point(4, 37);
            this.GroupBox7.Name = "GroupBox7";
            this.GroupBox7.Size = new System.Drawing.Size(754, 30);
            this.GroupBox7.TabIndex = 8;
            // 
            // Button28
            // 
            this.Button28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button28.DrawOnGlass = false;
            this.Button28.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button28.ForeColor = System.Drawing.Color.White;
            this.Button28.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button28.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button28.Location = new System.Drawing.Point(639, 3);
            this.Button28.Name = "Button28";
            this.Button28.Size = new System.Drawing.Size(36, 24);
            this.Button28.TabIndex = 115;
            this.Button28.Tag = "2";
            this.Button28.UseVisualStyleBackColor = false;
            // 
            // Button29
            // 
            this.Button29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button29.DrawOnGlass = false;
            this.Button29.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button29.ForeColor = System.Drawing.Color.White;
            this.Button29.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button29.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button29.Location = new System.Drawing.Point(677, 3);
            this.Button29.Name = "Button29";
            this.Button29.Size = new System.Drawing.Size(36, 24);
            this.Button29.TabIndex = 114;
            this.Button29.Tag = "1";
            this.Button29.UseVisualStyleBackColor = false;
            // 
            // Button30
            // 
            this.Button30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button30.DrawOnGlass = false;
            this.Button30.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button30.ForeColor = System.Drawing.Color.White;
            this.Button30.Image = ((System.Drawing.Image)(resources.GetObject("Button30.Image")));
            this.Button30.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button30.Location = new System.Drawing.Point(715, 3);
            this.Button30.Name = "Button30";
            this.Button30.Size = new System.Drawing.Size(36, 24);
            this.Button30.TabIndex = 113;
            this.Button30.Tag = "3";
            this.Button30.UseVisualStyleBackColor = false;
            // 
            // TextBox7
            // 
            this.TextBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox7.DrawOnGlass = false;
            this.TextBox7.ForeColor = System.Drawing.Color.White;
            this.TextBox7.Location = new System.Drawing.Point(195, 3);
            this.TextBox7.MaxLength = 32767;
            this.TextBox7.Multiline = false;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.ReadOnly = false;
            this.TextBox7.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox7.SelectedText = "";
            this.TextBox7.SelectionLength = 0;
            this.TextBox7.SelectionStart = 0;
            this.TextBox7.Size = new System.Drawing.Size(440, 24);
            this.TextBox7.TabIndex = 1;
            this.TextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox7.UseSystemPasswordChar = false;
            this.TextBox7.WordWrap = true;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(33, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(156, 22);
            this.Label7.TabIndex = 112;
            this.Label7.Text = "Question:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox7
            // 
            this.PictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(3, 3);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(24, 24);
            this.PictureBox7.TabIndex = 1;
            this.PictureBox7.TabStop = false;
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.GroupBox55);
            this.TabPage3.Controls.Add(this.Separator2);
            this.TabPage3.Controls.Add(this.GroupBox54);
            this.TabPage3.Controls.Add(this.GroupBox18);
            this.TabPage3.Controls.Add(this.GroupBox13);
            this.TabPage3.Controls.Add(this.GroupBox14);
            this.TabPage3.Controls.Add(this.GroupBox15);
            this.TabPage3.Controls.Add(this.GroupBox16);
            this.TabPage3.Controls.Add(this.GroupBox17);
            this.TabPage3.Location = new System.Drawing.Point(144, 4);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(761, 368);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Window & menus";
            // 
            // GroupBox55
            // 
            this.GroupBox55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox55.Controls.Add(this.Button169);
            this.GroupBox55.Controls.Add(this.Button170);
            this.GroupBox55.Controls.Add(this.Button171);
            this.GroupBox55.Controls.Add(this.TextBox54);
            this.GroupBox55.Controls.Add(this.Label55);
            this.GroupBox55.Controls.Add(this.PictureBox54);
            this.GroupBox55.Location = new System.Drawing.Point(4, 250);
            this.GroupBox55.Name = "GroupBox55";
            this.GroupBox55.Size = new System.Drawing.Size(754, 30);
            this.GroupBox55.TabIndex = 21;
            // 
            // Button169
            // 
            this.Button169.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button169.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button169.DrawOnGlass = false;
            this.Button169.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button169.ForeColor = System.Drawing.Color.White;
            this.Button169.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button169.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button169.Location = new System.Drawing.Point(639, 3);
            this.Button169.Name = "Button169";
            this.Button169.Size = new System.Drawing.Size(36, 24);
            this.Button169.TabIndex = 115;
            this.Button169.Tag = "2";
            this.Button169.UseVisualStyleBackColor = false;
            // 
            // Button170
            // 
            this.Button170.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button170.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button170.DrawOnGlass = false;
            this.Button170.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button170.ForeColor = System.Drawing.Color.White;
            this.Button170.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button170.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button170.Location = new System.Drawing.Point(677, 3);
            this.Button170.Name = "Button170";
            this.Button170.Size = new System.Drawing.Size(36, 24);
            this.Button170.TabIndex = 114;
            this.Button170.Tag = "1";
            this.Button170.UseVisualStyleBackColor = false;
            // 
            // Button171
            // 
            this.Button171.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button171.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button171.DrawOnGlass = false;
            this.Button171.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button171.ForeColor = System.Drawing.Color.White;
            this.Button171.Image = ((System.Drawing.Image)(resources.GetObject("Button171.Image")));
            this.Button171.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button171.Location = new System.Drawing.Point(715, 3);
            this.Button171.Name = "Button171";
            this.Button171.Size = new System.Drawing.Size(36, 24);
            this.Button171.TabIndex = 113;
            this.Button171.Tag = "3";
            this.Button171.UseVisualStyleBackColor = false;
            // 
            // TextBox54
            // 
            this.TextBox54.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox54.DrawOnGlass = false;
            this.TextBox54.ForeColor = System.Drawing.Color.White;
            this.TextBox54.Location = new System.Drawing.Point(140, 3);
            this.TextBox54.MaxLength = 32767;
            this.TextBox54.Multiline = false;
            this.TextBox54.Name = "TextBox54";
            this.TextBox54.ReadOnly = false;
            this.TextBox54.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox54.SelectedText = "";
            this.TextBox54.SelectionLength = 0;
            this.TextBox54.SelectionStart = 0;
            this.TextBox54.Size = new System.Drawing.Size(495, 24);
            this.TextBox54.TabIndex = 1;
            this.TextBox54.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox54.UseSystemPasswordChar = false;
            this.TextBox54.WordWrap = true;
            // 
            // Label55
            // 
            this.Label55.BackColor = System.Drawing.Color.Transparent;
            this.Label55.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label55.Location = new System.Drawing.Point(33, 4);
            this.Label55.Name = "Label55";
            this.Label55.Size = new System.Drawing.Size(101, 22);
            this.Label55.TabIndex = 112;
            this.Label55.Text = "Menu click:";
            this.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox54
            // 
            this.PictureBox54.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox54.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox54.Image")));
            this.PictureBox54.Location = new System.Drawing.Point(3, 3);
            this.PictureBox54.Name = "PictureBox54";
            this.PictureBox54.Size = new System.Drawing.Size(24, 24);
            this.PictureBox54.TabIndex = 1;
            this.PictureBox54.TabStop = false;
            // 
            // Separator2
            // 
            this.Separator2.AlternativeLook = false;
            this.Separator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator2.Location = new System.Drawing.Point(4, 209);
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(754, 1);
            this.Separator2.TabIndex = 20;
            this.Separator2.TabStop = false;
            this.Separator2.Text = "Separator2";
            // 
            // GroupBox54
            // 
            this.GroupBox54.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox54.Controls.Add(this.Button166);
            this.GroupBox54.Controls.Add(this.Button167);
            this.GroupBox54.Controls.Add(this.Button168);
            this.GroupBox54.Controls.Add(this.TextBox53);
            this.GroupBox54.Controls.Add(this.Label54);
            this.GroupBox54.Controls.Add(this.PictureBox53);
            this.GroupBox54.Location = new System.Drawing.Point(4, 216);
            this.GroupBox54.Name = "GroupBox54";
            this.GroupBox54.Size = new System.Drawing.Size(754, 30);
            this.GroupBox54.TabIndex = 19;
            // 
            // Button166
            // 
            this.Button166.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button166.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button166.DrawOnGlass = false;
            this.Button166.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button166.ForeColor = System.Drawing.Color.White;
            this.Button166.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button166.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button166.Location = new System.Drawing.Point(639, 3);
            this.Button166.Name = "Button166";
            this.Button166.Size = new System.Drawing.Size(36, 24);
            this.Button166.TabIndex = 115;
            this.Button166.Tag = "2";
            this.Button166.UseVisualStyleBackColor = false;
            // 
            // Button167
            // 
            this.Button167.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button167.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button167.DrawOnGlass = false;
            this.Button167.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button167.ForeColor = System.Drawing.Color.White;
            this.Button167.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button167.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button167.Location = new System.Drawing.Point(677, 3);
            this.Button167.Name = "Button167";
            this.Button167.Size = new System.Drawing.Size(36, 24);
            this.Button167.TabIndex = 114;
            this.Button167.Tag = "1";
            this.Button167.UseVisualStyleBackColor = false;
            // 
            // Button168
            // 
            this.Button168.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button168.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button168.DrawOnGlass = false;
            this.Button168.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button168.ForeColor = System.Drawing.Color.White;
            this.Button168.Image = ((System.Drawing.Image)(resources.GetObject("Button168.Image")));
            this.Button168.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button168.Location = new System.Drawing.Point(715, 3);
            this.Button168.Name = "Button168";
            this.Button168.Size = new System.Drawing.Size(36, 24);
            this.Button168.TabIndex = 113;
            this.Button168.Tag = "3";
            this.Button168.UseVisualStyleBackColor = false;
            // 
            // TextBox53
            // 
            this.TextBox53.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox53.DrawOnGlass = false;
            this.TextBox53.ForeColor = System.Drawing.Color.White;
            this.TextBox53.Location = new System.Drawing.Point(140, 3);
            this.TextBox53.MaxLength = 32767;
            this.TextBox53.Multiline = false;
            this.TextBox53.Name = "TextBox53";
            this.TextBox53.ReadOnly = false;
            this.TextBox53.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox53.SelectedText = "";
            this.TextBox53.SelectionLength = 0;
            this.TextBox53.SelectionStart = 0;
            this.TextBox53.Size = new System.Drawing.Size(495, 24);
            this.TextBox53.TabIndex = 1;
            this.TextBox53.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox53.UseSystemPasswordChar = false;
            this.TextBox53.WordWrap = true;
            // 
            // Label54
            // 
            this.Label54.BackColor = System.Drawing.Color.Transparent;
            this.Label54.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label54.Location = new System.Drawing.Point(33, 4);
            this.Label54.Name = "Label54";
            this.Label54.Size = new System.Drawing.Size(101, 22);
            this.Label54.TabIndex = 112;
            this.Label54.Text = "Menu popup:";
            this.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox53
            // 
            this.PictureBox53.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox53.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox53.Image")));
            this.PictureBox53.Location = new System.Drawing.Point(3, 3);
            this.PictureBox53.Name = "PictureBox53";
            this.PictureBox53.Size = new System.Drawing.Size(24, 24);
            this.PictureBox53.TabIndex = 1;
            this.PictureBox53.TabStop = false;
            // 
            // GroupBox18
            // 
            this.GroupBox18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox18.Controls.Add(this.Button58);
            this.GroupBox18.Controls.Add(this.Button59);
            this.GroupBox18.Controls.Add(this.Button60);
            this.GroupBox18.Controls.Add(this.TextBox17);
            this.GroupBox18.Controls.Add(this.Label18);
            this.GroupBox18.Controls.Add(this.PictureBox17);
            this.GroupBox18.Location = new System.Drawing.Point(4, 173);
            this.GroupBox18.Name = "GroupBox18";
            this.GroupBox18.Size = new System.Drawing.Size(754, 30);
            this.GroupBox18.TabIndex = 18;
            // 
            // Button58
            // 
            this.Button58.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button58.DrawOnGlass = false;
            this.Button58.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button58.ForeColor = System.Drawing.Color.White;
            this.Button58.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button58.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button58.Location = new System.Drawing.Point(639, 3);
            this.Button58.Name = "Button58";
            this.Button58.Size = new System.Drawing.Size(36, 24);
            this.Button58.TabIndex = 115;
            this.Button58.Tag = "2";
            this.Button58.UseVisualStyleBackColor = false;
            // 
            // Button59
            // 
            this.Button59.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button59.DrawOnGlass = false;
            this.Button59.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button59.ForeColor = System.Drawing.Color.White;
            this.Button59.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button59.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button59.Location = new System.Drawing.Point(677, 3);
            this.Button59.Name = "Button59";
            this.Button59.Size = new System.Drawing.Size(36, 24);
            this.Button59.TabIndex = 114;
            this.Button59.Tag = "1";
            this.Button59.UseVisualStyleBackColor = false;
            // 
            // Button60
            // 
            this.Button60.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button60.DrawOnGlass = false;
            this.Button60.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button60.ForeColor = System.Drawing.Color.White;
            this.Button60.Image = ((System.Drawing.Image)(resources.GetObject("Button60.Image")));
            this.Button60.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button60.Location = new System.Drawing.Point(715, 3);
            this.Button60.Name = "Button60";
            this.Button60.Size = new System.Drawing.Size(36, 24);
            this.Button60.TabIndex = 113;
            this.Button60.Tag = "3";
            this.Button60.UseVisualStyleBackColor = false;
            // 
            // TextBox17
            // 
            this.TextBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox17.DrawOnGlass = false;
            this.TextBox17.ForeColor = System.Drawing.Color.White;
            this.TextBox17.Location = new System.Drawing.Point(140, 3);
            this.TextBox17.MaxLength = 32767;
            this.TextBox17.Multiline = false;
            this.TextBox17.Name = "TextBox17";
            this.TextBox17.ReadOnly = false;
            this.TextBox17.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox17.SelectedText = "";
            this.TextBox17.SelectionLength = 0;
            this.TextBox17.SelectionStart = 0;
            this.TextBox17.Size = new System.Drawing.Size(495, 24);
            this.TextBox17.TabIndex = 1;
            this.TextBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox17.UseSystemPasswordChar = false;
            this.TextBox17.WordWrap = true;
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(33, 4);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(101, 22);
            this.Label18.TabIndex = 112;
            this.Label18.Text = "Restore up:";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox17
            // 
            this.PictureBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox17.Image")));
            this.PictureBox17.Location = new System.Drawing.Point(3, 3);
            this.PictureBox17.Name = "PictureBox17";
            this.PictureBox17.Size = new System.Drawing.Size(24, 24);
            this.PictureBox17.TabIndex = 1;
            this.PictureBox17.TabStop = false;
            // 
            // GroupBox13
            // 
            this.GroupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox13.Controls.Add(this.Button43);
            this.GroupBox13.Controls.Add(this.Button44);
            this.GroupBox13.Controls.Add(this.Button45);
            this.GroupBox13.Controls.Add(this.TextBox12);
            this.GroupBox13.Controls.Add(this.Label13);
            this.GroupBox13.Controls.Add(this.PictureBox12);
            this.GroupBox13.Location = new System.Drawing.Point(4, 139);
            this.GroupBox13.Name = "GroupBox13";
            this.GroupBox13.Size = new System.Drawing.Size(754, 30);
            this.GroupBox13.TabIndex = 17;
            // 
            // Button43
            // 
            this.Button43.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button43.DrawOnGlass = false;
            this.Button43.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button43.ForeColor = System.Drawing.Color.White;
            this.Button43.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button43.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button43.Location = new System.Drawing.Point(639, 3);
            this.Button43.Name = "Button43";
            this.Button43.Size = new System.Drawing.Size(36, 24);
            this.Button43.TabIndex = 115;
            this.Button43.Tag = "2";
            this.Button43.UseVisualStyleBackColor = false;
            // 
            // Button44
            // 
            this.Button44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button44.DrawOnGlass = false;
            this.Button44.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button44.ForeColor = System.Drawing.Color.White;
            this.Button44.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button44.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button44.Location = new System.Drawing.Point(677, 3);
            this.Button44.Name = "Button44";
            this.Button44.Size = new System.Drawing.Size(36, 24);
            this.Button44.TabIndex = 114;
            this.Button44.Tag = "1";
            this.Button44.UseVisualStyleBackColor = false;
            // 
            // Button45
            // 
            this.Button45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button45.DrawOnGlass = false;
            this.Button45.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button45.ForeColor = System.Drawing.Color.White;
            this.Button45.Image = ((System.Drawing.Image)(resources.GetObject("Button45.Image")));
            this.Button45.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button45.Location = new System.Drawing.Point(715, 3);
            this.Button45.Name = "Button45";
            this.Button45.Size = new System.Drawing.Size(36, 24);
            this.Button45.TabIndex = 113;
            this.Button45.Tag = "3";
            this.Button45.UseVisualStyleBackColor = false;
            // 
            // TextBox12
            // 
            this.TextBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox12.DrawOnGlass = false;
            this.TextBox12.ForeColor = System.Drawing.Color.White;
            this.TextBox12.Location = new System.Drawing.Point(140, 3);
            this.TextBox12.MaxLength = 32767;
            this.TextBox12.Multiline = false;
            this.TextBox12.Name = "TextBox12";
            this.TextBox12.ReadOnly = false;
            this.TextBox12.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox12.SelectedText = "";
            this.TextBox12.SelectionLength = 0;
            this.TextBox12.SelectionStart = 0;
            this.TextBox12.Size = new System.Drawing.Size(495, 24);
            this.TextBox12.TabIndex = 1;
            this.TextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox12.UseSystemPasswordChar = false;
            this.TextBox12.WordWrap = true;
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.Transparent;
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(33, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(101, 22);
            this.Label13.TabIndex = 112;
            this.Label13.Text = "Restore down:";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox12
            // 
            this.PictureBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox12.Image")));
            this.PictureBox12.Location = new System.Drawing.Point(3, 3);
            this.PictureBox12.Name = "PictureBox12";
            this.PictureBox12.Size = new System.Drawing.Size(24, 24);
            this.PictureBox12.TabIndex = 1;
            this.PictureBox12.TabStop = false;
            // 
            // GroupBox14
            // 
            this.GroupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox14.Controls.Add(this.Button46);
            this.GroupBox14.Controls.Add(this.Button47);
            this.GroupBox14.Controls.Add(this.Button48);
            this.GroupBox14.Controls.Add(this.TextBox13);
            this.GroupBox14.Controls.Add(this.Label14);
            this.GroupBox14.Controls.Add(this.PictureBox13);
            this.GroupBox14.Location = new System.Drawing.Point(4, 105);
            this.GroupBox14.Name = "GroupBox14";
            this.GroupBox14.Size = new System.Drawing.Size(754, 30);
            this.GroupBox14.TabIndex = 16;
            // 
            // Button46
            // 
            this.Button46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button46.DrawOnGlass = false;
            this.Button46.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button46.ForeColor = System.Drawing.Color.White;
            this.Button46.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button46.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button46.Location = new System.Drawing.Point(639, 3);
            this.Button46.Name = "Button46";
            this.Button46.Size = new System.Drawing.Size(36, 24);
            this.Button46.TabIndex = 115;
            this.Button46.Tag = "2";
            this.Button46.UseVisualStyleBackColor = false;
            // 
            // Button47
            // 
            this.Button47.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button47.DrawOnGlass = false;
            this.Button47.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button47.ForeColor = System.Drawing.Color.White;
            this.Button47.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button47.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button47.Location = new System.Drawing.Point(677, 3);
            this.Button47.Name = "Button47";
            this.Button47.Size = new System.Drawing.Size(36, 24);
            this.Button47.TabIndex = 114;
            this.Button47.Tag = "1";
            this.Button47.UseVisualStyleBackColor = false;
            // 
            // Button48
            // 
            this.Button48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button48.DrawOnGlass = false;
            this.Button48.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button48.ForeColor = System.Drawing.Color.White;
            this.Button48.Image = ((System.Drawing.Image)(resources.GetObject("Button48.Image")));
            this.Button48.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button48.Location = new System.Drawing.Point(715, 3);
            this.Button48.Name = "Button48";
            this.Button48.Size = new System.Drawing.Size(36, 24);
            this.Button48.TabIndex = 113;
            this.Button48.Tag = "3";
            this.Button48.UseVisualStyleBackColor = false;
            // 
            // TextBox13
            // 
            this.TextBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox13.DrawOnGlass = false;
            this.TextBox13.ForeColor = System.Drawing.Color.White;
            this.TextBox13.Location = new System.Drawing.Point(140, 3);
            this.TextBox13.MaxLength = 32767;
            this.TextBox13.Multiline = false;
            this.TextBox13.Name = "TextBox13";
            this.TextBox13.ReadOnly = false;
            this.TextBox13.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox13.SelectedText = "";
            this.TextBox13.SelectionLength = 0;
            this.TextBox13.SelectionStart = 0;
            this.TextBox13.Size = new System.Drawing.Size(495, 24);
            this.TextBox13.TabIndex = 1;
            this.TextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox13.UseSystemPasswordChar = false;
            this.TextBox13.WordWrap = true;
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(33, 4);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(101, 22);
            this.Label14.TabIndex = 112;
            this.Label14.Text = "Minimize:";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox13
            // 
            this.PictureBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox13.Image")));
            this.PictureBox13.Location = new System.Drawing.Point(3, 3);
            this.PictureBox13.Name = "PictureBox13";
            this.PictureBox13.Size = new System.Drawing.Size(24, 24);
            this.PictureBox13.TabIndex = 1;
            this.PictureBox13.TabStop = false;
            // 
            // GroupBox15
            // 
            this.GroupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox15.Controls.Add(this.Button49);
            this.GroupBox15.Controls.Add(this.Button50);
            this.GroupBox15.Controls.Add(this.Button51);
            this.GroupBox15.Controls.Add(this.TextBox14);
            this.GroupBox15.Controls.Add(this.Label15);
            this.GroupBox15.Controls.Add(this.PictureBox14);
            this.GroupBox15.Location = new System.Drawing.Point(4, 71);
            this.GroupBox15.Name = "GroupBox15";
            this.GroupBox15.Size = new System.Drawing.Size(754, 30);
            this.GroupBox15.TabIndex = 15;
            // 
            // Button49
            // 
            this.Button49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button49.DrawOnGlass = false;
            this.Button49.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button49.ForeColor = System.Drawing.Color.White;
            this.Button49.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button49.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button49.Location = new System.Drawing.Point(639, 3);
            this.Button49.Name = "Button49";
            this.Button49.Size = new System.Drawing.Size(36, 24);
            this.Button49.TabIndex = 115;
            this.Button49.Tag = "2";
            this.Button49.UseVisualStyleBackColor = false;
            // 
            // Button50
            // 
            this.Button50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button50.DrawOnGlass = false;
            this.Button50.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button50.ForeColor = System.Drawing.Color.White;
            this.Button50.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button50.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button50.Location = new System.Drawing.Point(677, 3);
            this.Button50.Name = "Button50";
            this.Button50.Size = new System.Drawing.Size(36, 24);
            this.Button50.TabIndex = 114;
            this.Button50.Tag = "1";
            this.Button50.UseVisualStyleBackColor = false;
            // 
            // Button51
            // 
            this.Button51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button51.DrawOnGlass = false;
            this.Button51.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button51.ForeColor = System.Drawing.Color.White;
            this.Button51.Image = ((System.Drawing.Image)(resources.GetObject("Button51.Image")));
            this.Button51.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button51.Location = new System.Drawing.Point(715, 3);
            this.Button51.Name = "Button51";
            this.Button51.Size = new System.Drawing.Size(36, 24);
            this.Button51.TabIndex = 113;
            this.Button51.Tag = "3";
            this.Button51.UseVisualStyleBackColor = false;
            // 
            // TextBox14
            // 
            this.TextBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox14.DrawOnGlass = false;
            this.TextBox14.ForeColor = System.Drawing.Color.White;
            this.TextBox14.Location = new System.Drawing.Point(140, 3);
            this.TextBox14.MaxLength = 32767;
            this.TextBox14.Multiline = false;
            this.TextBox14.Name = "TextBox14";
            this.TextBox14.ReadOnly = false;
            this.TextBox14.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox14.SelectedText = "";
            this.TextBox14.SelectionLength = 0;
            this.TextBox14.SelectionStart = 0;
            this.TextBox14.Size = new System.Drawing.Size(495, 24);
            this.TextBox14.TabIndex = 1;
            this.TextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox14.UseSystemPasswordChar = false;
            this.TextBox14.WordWrap = true;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.Transparent;
            this.Label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(33, 4);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(101, 22);
            this.Label15.TabIndex = 112;
            this.Label15.Text = "Maximize:";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox14
            // 
            this.PictureBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox14.Image")));
            this.PictureBox14.Location = new System.Drawing.Point(3, 3);
            this.PictureBox14.Name = "PictureBox14";
            this.PictureBox14.Size = new System.Drawing.Size(24, 24);
            this.PictureBox14.TabIndex = 1;
            this.PictureBox14.TabStop = false;
            // 
            // GroupBox16
            // 
            this.GroupBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox16.Controls.Add(this.Button52);
            this.GroupBox16.Controls.Add(this.Button53);
            this.GroupBox16.Controls.Add(this.Button54);
            this.GroupBox16.Controls.Add(this.TextBox15);
            this.GroupBox16.Controls.Add(this.Label16);
            this.GroupBox16.Controls.Add(this.PictureBox15);
            this.GroupBox16.Location = new System.Drawing.Point(4, 37);
            this.GroupBox16.Name = "GroupBox16";
            this.GroupBox16.Size = new System.Drawing.Size(754, 30);
            this.GroupBox16.TabIndex = 14;
            // 
            // Button52
            // 
            this.Button52.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button52.DrawOnGlass = false;
            this.Button52.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button52.ForeColor = System.Drawing.Color.White;
            this.Button52.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button52.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button52.Location = new System.Drawing.Point(639, 3);
            this.Button52.Name = "Button52";
            this.Button52.Size = new System.Drawing.Size(36, 24);
            this.Button52.TabIndex = 115;
            this.Button52.Tag = "2";
            this.Button52.UseVisualStyleBackColor = false;
            // 
            // Button53
            // 
            this.Button53.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button53.DrawOnGlass = false;
            this.Button53.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button53.ForeColor = System.Drawing.Color.White;
            this.Button53.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button53.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button53.Location = new System.Drawing.Point(677, 3);
            this.Button53.Name = "Button53";
            this.Button53.Size = new System.Drawing.Size(36, 24);
            this.Button53.TabIndex = 114;
            this.Button53.Tag = "1";
            this.Button53.UseVisualStyleBackColor = false;
            // 
            // Button54
            // 
            this.Button54.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button54.DrawOnGlass = false;
            this.Button54.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button54.ForeColor = System.Drawing.Color.White;
            this.Button54.Image = ((System.Drawing.Image)(resources.GetObject("Button54.Image")));
            this.Button54.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button54.Location = new System.Drawing.Point(715, 3);
            this.Button54.Name = "Button54";
            this.Button54.Size = new System.Drawing.Size(36, 24);
            this.Button54.TabIndex = 113;
            this.Button54.Tag = "3";
            this.Button54.UseVisualStyleBackColor = false;
            // 
            // TextBox15
            // 
            this.TextBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox15.DrawOnGlass = false;
            this.TextBox15.ForeColor = System.Drawing.Color.White;
            this.TextBox15.Location = new System.Drawing.Point(140, 3);
            this.TextBox15.MaxLength = 32767;
            this.TextBox15.Multiline = false;
            this.TextBox15.Name = "TextBox15";
            this.TextBox15.ReadOnly = false;
            this.TextBox15.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox15.SelectedText = "";
            this.TextBox15.SelectionLength = 0;
            this.TextBox15.SelectionStart = 0;
            this.TextBox15.Size = new System.Drawing.Size(495, 24);
            this.TextBox15.TabIndex = 1;
            this.TextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox15.UseSystemPasswordChar = false;
            this.TextBox15.WordWrap = true;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(33, 4);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(101, 22);
            this.Label16.TabIndex = 112;
            this.Label16.Text = "Close:";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox15
            // 
            this.PictureBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox15.Image")));
            this.PictureBox15.Location = new System.Drawing.Point(3, 3);
            this.PictureBox15.Name = "PictureBox15";
            this.PictureBox15.Size = new System.Drawing.Size(24, 24);
            this.PictureBox15.TabIndex = 1;
            this.PictureBox15.TabStop = false;
            // 
            // GroupBox17
            // 
            this.GroupBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox17.Controls.Add(this.Button55);
            this.GroupBox17.Controls.Add(this.Button56);
            this.GroupBox17.Controls.Add(this.Button57);
            this.GroupBox17.Controls.Add(this.TextBox16);
            this.GroupBox17.Controls.Add(this.Label17);
            this.GroupBox17.Controls.Add(this.PictureBox16);
            this.GroupBox17.Location = new System.Drawing.Point(4, 3);
            this.GroupBox17.Name = "GroupBox17";
            this.GroupBox17.Size = new System.Drawing.Size(754, 30);
            this.GroupBox17.TabIndex = 13;
            // 
            // Button55
            // 
            this.Button55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button55.DrawOnGlass = false;
            this.Button55.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button55.ForeColor = System.Drawing.Color.White;
            this.Button55.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button55.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button55.Location = new System.Drawing.Point(639, 3);
            this.Button55.Name = "Button55";
            this.Button55.Size = new System.Drawing.Size(36, 24);
            this.Button55.TabIndex = 115;
            this.Button55.Tag = "2";
            this.Button55.UseVisualStyleBackColor = false;
            // 
            // Button56
            // 
            this.Button56.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button56.DrawOnGlass = false;
            this.Button56.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button56.ForeColor = System.Drawing.Color.White;
            this.Button56.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button56.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button56.Location = new System.Drawing.Point(677, 3);
            this.Button56.Name = "Button56";
            this.Button56.Size = new System.Drawing.Size(36, 24);
            this.Button56.TabIndex = 114;
            this.Button56.Tag = "1";
            this.Button56.UseVisualStyleBackColor = false;
            // 
            // Button57
            // 
            this.Button57.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button57.DrawOnGlass = false;
            this.Button57.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button57.ForeColor = System.Drawing.Color.White;
            this.Button57.Image = ((System.Drawing.Image)(resources.GetObject("Button57.Image")));
            this.Button57.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button57.Location = new System.Drawing.Point(715, 3);
            this.Button57.Name = "Button57";
            this.Button57.Size = new System.Drawing.Size(36, 24);
            this.Button57.TabIndex = 113;
            this.Button57.Tag = "3";
            this.Button57.UseVisualStyleBackColor = false;
            // 
            // TextBox16
            // 
            this.TextBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox16.DrawOnGlass = false;
            this.TextBox16.ForeColor = System.Drawing.Color.White;
            this.TextBox16.Location = new System.Drawing.Point(140, 3);
            this.TextBox16.MaxLength = 32767;
            this.TextBox16.Multiline = false;
            this.TextBox16.Name = "TextBox16";
            this.TextBox16.ReadOnly = false;
            this.TextBox16.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox16.SelectedText = "";
            this.TextBox16.SelectionLength = 0;
            this.TextBox16.SelectionStart = 0;
            this.TextBox16.Size = new System.Drawing.Size(495, 24);
            this.TextBox16.TabIndex = 1;
            this.TextBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox16.UseSystemPasswordChar = false;
            this.TextBox16.WordWrap = true;
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(33, 4);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(101, 22);
            this.Label17.TabIndex = 112;
            this.Label17.Text = "Open:";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox16
            // 
            this.PictureBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox16.Image")));
            this.PictureBox16.Location = new System.Drawing.Point(3, 3);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(24, 24);
            this.PictureBox16.TabIndex = 1;
            this.PictureBox16.TabStop = false;
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage7.Controls.Add(this.groupBox87);
            this.TabPage7.Controls.Add(this.GroupBox86);
            this.TabPage7.Controls.Add(this.GroupBox53);
            this.TabPage7.Controls.Add(this.GroupBox51);
            this.TabPage7.Controls.Add(this.GroupBox50);
            this.TabPage7.Controls.Add(this.GroupBox49);
            this.TabPage7.Controls.Add(this.GroupBox48);
            this.TabPage7.Controls.Add(this.GroupBox47);
            this.TabPage7.Controls.Add(this.GroupBox46);
            this.TabPage7.Location = new System.Drawing.Point(144, 4);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(761, 368);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Devices";
            // 
            // groupBox87
            // 
            this.groupBox87.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox87.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox87.Controls.Add(this.pictureBox94);
            this.groupBox87.Controls.Add(this.button263);
            this.groupBox87.Controls.Add(this.button264);
            this.groupBox87.Controls.Add(this.button265);
            this.groupBox87.Controls.Add(this.textBox85);
            this.groupBox87.Controls.Add(this.label89);
            this.groupBox87.Controls.Add(this.pictureBox86);
            this.groupBox87.Location = new System.Drawing.Point(4, 139);
            this.groupBox87.Name = "groupBox87";
            this.groupBox87.Size = new System.Drawing.Size(754, 30);
            this.groupBox87.TabIndex = 119;
            // 
            // pictureBox94
            // 
            this.pictureBox94.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox94.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox94.Image")));
            this.pictureBox94.Location = new System.Drawing.Point(30, 3);
            this.pictureBox94.Name = "pictureBox94";
            this.pictureBox94.Size = new System.Drawing.Size(24, 24);
            this.pictureBox94.TabIndex = 121;
            this.pictureBox94.TabStop = false;
            // 
            // button263
            // 
            this.button263.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button263.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.button263.DrawOnGlass = false;
            this.button263.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button263.ForeColor = System.Drawing.Color.White;
            this.button263.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.button263.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.button263.Location = new System.Drawing.Point(639, 3);
            this.button263.Name = "button263";
            this.button263.Size = new System.Drawing.Size(36, 24);
            this.button263.TabIndex = 115;
            this.button263.Tag = "2";
            this.button263.UseVisualStyleBackColor = false;
            // 
            // button264
            // 
            this.button264.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button264.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.button264.DrawOnGlass = false;
            this.button264.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button264.ForeColor = System.Drawing.Color.White;
            this.button264.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.button264.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.button264.Location = new System.Drawing.Point(677, 3);
            this.button264.Name = "button264";
            this.button264.Size = new System.Drawing.Size(36, 24);
            this.button264.TabIndex = 114;
            this.button264.Tag = "1";
            this.button264.UseVisualStyleBackColor = false;
            // 
            // button265
            // 
            this.button265.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button265.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.button265.DrawOnGlass = false;
            this.button265.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button265.ForeColor = System.Drawing.Color.White;
            this.button265.Image = ((System.Drawing.Image)(resources.GetObject("button265.Image")));
            this.button265.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.button265.Location = new System.Drawing.Point(715, 3);
            this.button265.Name = "button265";
            this.button265.Size = new System.Drawing.Size(36, 24);
            this.button265.TabIndex = 113;
            this.button265.Tag = "3";
            this.button265.UseVisualStyleBackColor = false;
            // 
            // textBox85
            // 
            this.textBox85.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.textBox85.DrawOnGlass = false;
            this.textBox85.ForeColor = System.Drawing.Color.White;
            this.textBox85.Location = new System.Drawing.Point(201, 3);
            this.textBox85.MaxLength = 32767;
            this.textBox85.Multiline = false;
            this.textBox85.Name = "textBox85";
            this.textBox85.ReadOnly = false;
            this.textBox85.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.textBox85.SelectedText = "";
            this.textBox85.SelectionLength = 0;
            this.textBox85.SelectionStart = 0;
            this.textBox85.Size = new System.Drawing.Size(434, 24);
            this.textBox85.TabIndex = 1;
            this.textBox85.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox85.UseSystemPasswordChar = false;
            this.textBox85.WordWrap = true;
            // 
            // label89
            // 
            this.label89.BackColor = System.Drawing.Color.Transparent;
            this.label89.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(60, 4);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(135, 22);
            this.label89.TabIndex = 112;
            this.label89.Text = "Charger disconnected:";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox86
            // 
            this.pictureBox86.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox86.Image")));
            this.pictureBox86.Location = new System.Drawing.Point(3, 3);
            this.pictureBox86.Name = "pictureBox86";
            this.pictureBox86.Size = new System.Drawing.Size(24, 24);
            this.pictureBox86.TabIndex = 1;
            this.pictureBox86.TabStop = false;
            // 
            // GroupBox86
            // 
            this.GroupBox86.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox86.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox86.Controls.Add(this.Button260);
            this.GroupBox86.Controls.Add(this.pictureBox93);
            this.GroupBox86.Controls.Add(this.Button261);
            this.GroupBox86.Controls.Add(this.Button262);
            this.GroupBox86.Controls.Add(this.TextBox84);
            this.GroupBox86.Controls.Add(this.Label86);
            this.GroupBox86.Controls.Add(this.PictureBox85);
            this.GroupBox86.Location = new System.Drawing.Point(4, 105);
            this.GroupBox86.Name = "GroupBox86";
            this.GroupBox86.Size = new System.Drawing.Size(754, 30);
            this.GroupBox86.TabIndex = 118;
            // 
            // Button260
            // 
            this.Button260.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button260.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button260.DrawOnGlass = false;
            this.Button260.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button260.ForeColor = System.Drawing.Color.White;
            this.Button260.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button260.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button260.Location = new System.Drawing.Point(639, 3);
            this.Button260.Name = "Button260";
            this.Button260.Size = new System.Drawing.Size(36, 24);
            this.Button260.TabIndex = 115;
            this.Button260.Tag = "2";
            this.Button260.UseVisualStyleBackColor = false;
            // 
            // pictureBox93
            // 
            this.pictureBox93.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox93.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox93.Image")));
            this.pictureBox93.Location = new System.Drawing.Point(30, 3);
            this.pictureBox93.Name = "pictureBox93";
            this.pictureBox93.Size = new System.Drawing.Size(24, 24);
            this.pictureBox93.TabIndex = 120;
            this.pictureBox93.TabStop = false;
            // 
            // Button261
            // 
            this.Button261.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button261.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button261.DrawOnGlass = false;
            this.Button261.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button261.ForeColor = System.Drawing.Color.White;
            this.Button261.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button261.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button261.Location = new System.Drawing.Point(677, 3);
            this.Button261.Name = "Button261";
            this.Button261.Size = new System.Drawing.Size(36, 24);
            this.Button261.TabIndex = 114;
            this.Button261.Tag = "1";
            this.Button261.UseVisualStyleBackColor = false;
            // 
            // Button262
            // 
            this.Button262.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button262.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button262.DrawOnGlass = false;
            this.Button262.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button262.ForeColor = System.Drawing.Color.White;
            this.Button262.Image = ((System.Drawing.Image)(resources.GetObject("Button262.Image")));
            this.Button262.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button262.Location = new System.Drawing.Point(715, 3);
            this.Button262.Name = "Button262";
            this.Button262.Size = new System.Drawing.Size(36, 24);
            this.Button262.TabIndex = 113;
            this.Button262.Tag = "3";
            this.Button262.UseVisualStyleBackColor = false;
            // 
            // TextBox84
            // 
            this.TextBox84.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox84.DrawOnGlass = false;
            this.TextBox84.ForeColor = System.Drawing.Color.White;
            this.TextBox84.Location = new System.Drawing.Point(201, 3);
            this.TextBox84.MaxLength = 32767;
            this.TextBox84.Multiline = false;
            this.TextBox84.Name = "TextBox84";
            this.TextBox84.ReadOnly = false;
            this.TextBox84.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox84.SelectedText = "";
            this.TextBox84.SelectionLength = 0;
            this.TextBox84.SelectionStart = 0;
            this.TextBox84.Size = new System.Drawing.Size(434, 24);
            this.TextBox84.TabIndex = 1;
            this.TextBox84.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox84.UseSystemPasswordChar = false;
            this.TextBox84.WordWrap = true;
            // 
            // Label86
            // 
            this.Label86.BackColor = System.Drawing.Color.Transparent;
            this.Label86.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label86.Location = new System.Drawing.Point(60, 4);
            this.Label86.Name = "Label86";
            this.Label86.Size = new System.Drawing.Size(135, 22);
            this.Label86.TabIndex = 112;
            this.Label86.Text = "Charger connected:";
            this.Label86.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox85
            // 
            this.PictureBox85.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox85.Image")));
            this.PictureBox85.Location = new System.Drawing.Point(3, 3);
            this.PictureBox85.Name = "PictureBox85";
            this.PictureBox85.Size = new System.Drawing.Size(24, 24);
            this.PictureBox85.TabIndex = 1;
            this.PictureBox85.TabStop = false;
            // 
            // GroupBox53
            // 
            this.GroupBox53.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox53.Controls.Add(this.Button163);
            this.GroupBox53.Controls.Add(this.Button164);
            this.GroupBox53.Controls.Add(this.Button165);
            this.GroupBox53.Controls.Add(this.TextBox52);
            this.GroupBox53.Controls.Add(this.Label53);
            this.GroupBox53.Controls.Add(this.PictureBox52);
            this.GroupBox53.Location = new System.Drawing.Point(4, 275);
            this.GroupBox53.Name = "GroupBox53";
            this.GroupBox53.Size = new System.Drawing.Size(754, 30);
            this.GroupBox53.TabIndex = 117;
            // 
            // Button163
            // 
            this.Button163.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button163.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button163.DrawOnGlass = false;
            this.Button163.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button163.ForeColor = System.Drawing.Color.White;
            this.Button163.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button163.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button163.Location = new System.Drawing.Point(639, 3);
            this.Button163.Name = "Button163";
            this.Button163.Size = new System.Drawing.Size(36, 24);
            this.Button163.TabIndex = 115;
            this.Button163.Tag = "2";
            this.Button163.UseVisualStyleBackColor = false;
            // 
            // Button164
            // 
            this.Button164.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button164.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button164.DrawOnGlass = false;
            this.Button164.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button164.ForeColor = System.Drawing.Color.White;
            this.Button164.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button164.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button164.Location = new System.Drawing.Point(677, 3);
            this.Button164.Name = "Button164";
            this.Button164.Size = new System.Drawing.Size(36, 24);
            this.Button164.TabIndex = 114;
            this.Button164.Tag = "1";
            this.Button164.UseVisualStyleBackColor = false;
            // 
            // Button165
            // 
            this.Button165.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button165.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button165.DrawOnGlass = false;
            this.Button165.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button165.ForeColor = System.Drawing.Color.White;
            this.Button165.Image = ((System.Drawing.Image)(resources.GetObject("Button165.Image")));
            this.Button165.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button165.Location = new System.Drawing.Point(715, 3);
            this.Button165.Name = "Button165";
            this.Button165.Size = new System.Drawing.Size(36, 24);
            this.Button165.TabIndex = 113;
            this.Button165.Tag = "3";
            this.Button165.UseVisualStyleBackColor = false;
            // 
            // TextBox52
            // 
            this.TextBox52.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox52.DrawOnGlass = false;
            this.TextBox52.ForeColor = System.Drawing.Color.White;
            this.TextBox52.Location = new System.Drawing.Point(201, 3);
            this.TextBox52.MaxLength = 32767;
            this.TextBox52.Multiline = false;
            this.TextBox52.Name = "TextBox52";
            this.TextBox52.ReadOnly = false;
            this.TextBox52.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox52.SelectedText = "";
            this.TextBox52.SelectionLength = 0;
            this.TextBox52.SelectionStart = 0;
            this.TextBox52.Size = new System.Drawing.Size(434, 24);
            this.TextBox52.TabIndex = 1;
            this.TextBox52.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox52.UseSystemPasswordChar = false;
            this.TextBox52.WordWrap = true;
            // 
            // Label53
            // 
            this.Label53.BackColor = System.Drawing.Color.Transparent;
            this.Label53.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label53.Location = new System.Drawing.Point(33, 4);
            this.Label53.Name = "Label53";
            this.Label53.Size = new System.Drawing.Size(135, 22);
            this.Label53.TabIndex = 112;
            this.Label53.Text = "Proximity connection:";
            this.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox52
            // 
            this.PictureBox52.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox52.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox52.Image")));
            this.PictureBox52.Location = new System.Drawing.Point(3, 3);
            this.PictureBox52.Name = "PictureBox52";
            this.PictureBox52.Size = new System.Drawing.Size(24, 24);
            this.PictureBox52.TabIndex = 1;
            this.PictureBox52.TabStop = false;
            // 
            // GroupBox51
            // 
            this.GroupBox51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox51.Controls.Add(this.Button157);
            this.GroupBox51.Controls.Add(this.Button158);
            this.GroupBox51.Controls.Add(this.Button159);
            this.GroupBox51.Controls.Add(this.TextBox50);
            this.GroupBox51.Controls.Add(this.Label51);
            this.GroupBox51.Controls.Add(this.PictureBox50);
            this.GroupBox51.Location = new System.Drawing.Point(4, 241);
            this.GroupBox51.Name = "GroupBox51";
            this.GroupBox51.Size = new System.Drawing.Size(754, 30);
            this.GroupBox51.TabIndex = 52;
            // 
            // Button157
            // 
            this.Button157.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button157.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button157.DrawOnGlass = false;
            this.Button157.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button157.ForeColor = System.Drawing.Color.White;
            this.Button157.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button157.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button157.Location = new System.Drawing.Point(639, 3);
            this.Button157.Name = "Button157";
            this.Button157.Size = new System.Drawing.Size(36, 24);
            this.Button157.TabIndex = 115;
            this.Button157.Tag = "2";
            this.Button157.UseVisualStyleBackColor = false;
            // 
            // Button158
            // 
            this.Button158.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button158.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button158.DrawOnGlass = false;
            this.Button158.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button158.ForeColor = System.Drawing.Color.White;
            this.Button158.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button158.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button158.Location = new System.Drawing.Point(677, 3);
            this.Button158.Name = "Button158";
            this.Button158.Size = new System.Drawing.Size(36, 24);
            this.Button158.TabIndex = 114;
            this.Button158.Tag = "1";
            this.Button158.UseVisualStyleBackColor = false;
            // 
            // Button159
            // 
            this.Button159.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button159.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button159.DrawOnGlass = false;
            this.Button159.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button159.ForeColor = System.Drawing.Color.White;
            this.Button159.Image = ((System.Drawing.Image)(resources.GetObject("Button159.Image")));
            this.Button159.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button159.Location = new System.Drawing.Point(715, 3);
            this.Button159.Name = "Button159";
            this.Button159.Size = new System.Drawing.Size(36, 24);
            this.Button159.TabIndex = 113;
            this.Button159.Tag = "3";
            this.Button159.UseVisualStyleBackColor = false;
            // 
            // TextBox50
            // 
            this.TextBox50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox50.DrawOnGlass = false;
            this.TextBox50.ForeColor = System.Drawing.Color.White;
            this.TextBox50.Location = new System.Drawing.Point(201, 3);
            this.TextBox50.MaxLength = 32767;
            this.TextBox50.Multiline = false;
            this.TextBox50.Name = "TextBox50";
            this.TextBox50.ReadOnly = false;
            this.TextBox50.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox50.SelectedText = "";
            this.TextBox50.SelectionLength = 0;
            this.TextBox50.SelectionStart = 0;
            this.TextBox50.Size = new System.Drawing.Size(434, 24);
            this.TextBox50.TabIndex = 1;
            this.TextBox50.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox50.UseSystemPasswordChar = false;
            this.TextBox50.WordWrap = true;
            // 
            // Label51
            // 
            this.Label51.BackColor = System.Drawing.Color.Transparent;
            this.Label51.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label51.Location = new System.Drawing.Point(33, 4);
            this.Label51.Name = "Label51";
            this.Label51.Size = new System.Drawing.Size(135, 22);
            this.Label51.TabIndex = 112;
            this.Label51.Text = "Print completed:";
            this.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox50
            // 
            this.PictureBox50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox50.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox50.Image")));
            this.PictureBox50.Location = new System.Drawing.Point(3, 3);
            this.PictureBox50.Name = "PictureBox50";
            this.PictureBox50.Size = new System.Drawing.Size(24, 24);
            this.PictureBox50.TabIndex = 1;
            this.PictureBox50.TabStop = false;
            // 
            // GroupBox50
            // 
            this.GroupBox50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox50.Controls.Add(this.Button154);
            this.GroupBox50.Controls.Add(this.Button155);
            this.GroupBox50.Controls.Add(this.Button156);
            this.GroupBox50.Controls.Add(this.TextBox49);
            this.GroupBox50.Controls.Add(this.Label50);
            this.GroupBox50.Controls.Add(this.PictureBox49);
            this.GroupBox50.Location = new System.Drawing.Point(4, 207);
            this.GroupBox50.Name = "GroupBox50";
            this.GroupBox50.Size = new System.Drawing.Size(754, 30);
            this.GroupBox50.TabIndex = 51;
            // 
            // Button154
            // 
            this.Button154.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button154.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button154.DrawOnGlass = false;
            this.Button154.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button154.ForeColor = System.Drawing.Color.White;
            this.Button154.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button154.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button154.Location = new System.Drawing.Point(639, 3);
            this.Button154.Name = "Button154";
            this.Button154.Size = new System.Drawing.Size(36, 24);
            this.Button154.TabIndex = 115;
            this.Button154.Tag = "2";
            this.Button154.UseVisualStyleBackColor = false;
            // 
            // Button155
            // 
            this.Button155.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button155.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button155.DrawOnGlass = false;
            this.Button155.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button155.ForeColor = System.Drawing.Color.White;
            this.Button155.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button155.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button155.Location = new System.Drawing.Point(677, 3);
            this.Button155.Name = "Button155";
            this.Button155.Size = new System.Drawing.Size(36, 24);
            this.Button155.TabIndex = 114;
            this.Button155.Tag = "1";
            this.Button155.UseVisualStyleBackColor = false;
            // 
            // Button156
            // 
            this.Button156.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button156.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button156.DrawOnGlass = false;
            this.Button156.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button156.ForeColor = System.Drawing.Color.White;
            this.Button156.Image = ((System.Drawing.Image)(resources.GetObject("Button156.Image")));
            this.Button156.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button156.Location = new System.Drawing.Point(715, 3);
            this.Button156.Name = "Button156";
            this.Button156.Size = new System.Drawing.Size(36, 24);
            this.Button156.TabIndex = 113;
            this.Button156.Tag = "3";
            this.Button156.UseVisualStyleBackColor = false;
            // 
            // TextBox49
            // 
            this.TextBox49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox49.DrawOnGlass = false;
            this.TextBox49.ForeColor = System.Drawing.Color.White;
            this.TextBox49.Location = new System.Drawing.Point(201, 3);
            this.TextBox49.MaxLength = 32767;
            this.TextBox49.Multiline = false;
            this.TextBox49.Name = "TextBox49";
            this.TextBox49.ReadOnly = false;
            this.TextBox49.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox49.SelectedText = "";
            this.TextBox49.SelectionLength = 0;
            this.TextBox49.SelectionStart = 0;
            this.TextBox49.Size = new System.Drawing.Size(434, 24);
            this.TextBox49.TabIndex = 1;
            this.TextBox49.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox49.UseSystemPasswordChar = false;
            this.TextBox49.WordWrap = true;
            // 
            // Label50
            // 
            this.Label50.BackColor = System.Drawing.Color.Transparent;
            this.Label50.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label50.Location = new System.Drawing.Point(33, 4);
            this.Label50.Name = "Label50";
            this.Label50.Size = new System.Drawing.Size(135, 22);
            this.Label50.TabIndex = 112;
            this.Label50.Text = "Critical battery alarm:";
            this.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox49
            // 
            this.PictureBox49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox49.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox49.Image")));
            this.PictureBox49.Location = new System.Drawing.Point(3, 3);
            this.PictureBox49.Name = "PictureBox49";
            this.PictureBox49.Size = new System.Drawing.Size(24, 24);
            this.PictureBox49.TabIndex = 1;
            this.PictureBox49.TabStop = false;
            // 
            // GroupBox49
            // 
            this.GroupBox49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox49.Controls.Add(this.Button151);
            this.GroupBox49.Controls.Add(this.Button152);
            this.GroupBox49.Controls.Add(this.Button153);
            this.GroupBox49.Controls.Add(this.TextBox48);
            this.GroupBox49.Controls.Add(this.Label49);
            this.GroupBox49.Controls.Add(this.PictureBox48);
            this.GroupBox49.Location = new System.Drawing.Point(4, 173);
            this.GroupBox49.Name = "GroupBox49";
            this.GroupBox49.Size = new System.Drawing.Size(754, 30);
            this.GroupBox49.TabIndex = 50;
            // 
            // Button151
            // 
            this.Button151.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button151.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button151.DrawOnGlass = false;
            this.Button151.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button151.ForeColor = System.Drawing.Color.White;
            this.Button151.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button151.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button151.Location = new System.Drawing.Point(639, 3);
            this.Button151.Name = "Button151";
            this.Button151.Size = new System.Drawing.Size(36, 24);
            this.Button151.TabIndex = 115;
            this.Button151.Tag = "2";
            this.Button151.UseVisualStyleBackColor = false;
            // 
            // Button152
            // 
            this.Button152.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button152.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button152.DrawOnGlass = false;
            this.Button152.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button152.ForeColor = System.Drawing.Color.White;
            this.Button152.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button152.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button152.Location = new System.Drawing.Point(677, 3);
            this.Button152.Name = "Button152";
            this.Button152.Size = new System.Drawing.Size(36, 24);
            this.Button152.TabIndex = 114;
            this.Button152.Tag = "1";
            this.Button152.UseVisualStyleBackColor = false;
            // 
            // Button153
            // 
            this.Button153.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button153.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button153.DrawOnGlass = false;
            this.Button153.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button153.ForeColor = System.Drawing.Color.White;
            this.Button153.Image = ((System.Drawing.Image)(resources.GetObject("Button153.Image")));
            this.Button153.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button153.Location = new System.Drawing.Point(715, 3);
            this.Button153.Name = "Button153";
            this.Button153.Size = new System.Drawing.Size(36, 24);
            this.Button153.TabIndex = 113;
            this.Button153.Tag = "3";
            this.Button153.UseVisualStyleBackColor = false;
            // 
            // TextBox48
            // 
            this.TextBox48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox48.DrawOnGlass = false;
            this.TextBox48.ForeColor = System.Drawing.Color.White;
            this.TextBox48.Location = new System.Drawing.Point(201, 3);
            this.TextBox48.MaxLength = 32767;
            this.TextBox48.Multiline = false;
            this.TextBox48.Name = "TextBox48";
            this.TextBox48.ReadOnly = false;
            this.TextBox48.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox48.SelectedText = "";
            this.TextBox48.SelectionLength = 0;
            this.TextBox48.SelectionStart = 0;
            this.TextBox48.Size = new System.Drawing.Size(434, 24);
            this.TextBox48.TabIndex = 1;
            this.TextBox48.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox48.UseSystemPasswordChar = false;
            this.TextBox48.WordWrap = true;
            // 
            // Label49
            // 
            this.Label49.BackColor = System.Drawing.Color.Transparent;
            this.Label49.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label49.Location = new System.Drawing.Point(33, 4);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(135, 22);
            this.Label49.TabIndex = 112;
            this.Label49.Text = "Low battery:";
            this.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox48
            // 
            this.PictureBox48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox48.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox48.Image")));
            this.PictureBox48.Location = new System.Drawing.Point(3, 3);
            this.PictureBox48.Name = "PictureBox48";
            this.PictureBox48.Size = new System.Drawing.Size(24, 24);
            this.PictureBox48.TabIndex = 1;
            this.PictureBox48.TabStop = false;
            // 
            // GroupBox48
            // 
            this.GroupBox48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox48.Controls.Add(this.Button148);
            this.GroupBox48.Controls.Add(this.Button149);
            this.GroupBox48.Controls.Add(this.Button150);
            this.GroupBox48.Controls.Add(this.TextBox47);
            this.GroupBox48.Controls.Add(this.Label48);
            this.GroupBox48.Controls.Add(this.PictureBox47);
            this.GroupBox48.Location = new System.Drawing.Point(4, 71);
            this.GroupBox48.Name = "GroupBox48";
            this.GroupBox48.Size = new System.Drawing.Size(754, 30);
            this.GroupBox48.TabIndex = 49;
            // 
            // Button148
            // 
            this.Button148.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button148.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button148.DrawOnGlass = false;
            this.Button148.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button148.ForeColor = System.Drawing.Color.White;
            this.Button148.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button148.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button148.Location = new System.Drawing.Point(639, 3);
            this.Button148.Name = "Button148";
            this.Button148.Size = new System.Drawing.Size(36, 24);
            this.Button148.TabIndex = 115;
            this.Button148.Tag = "2";
            this.Button148.UseVisualStyleBackColor = false;
            // 
            // Button149
            // 
            this.Button149.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button149.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button149.DrawOnGlass = false;
            this.Button149.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button149.ForeColor = System.Drawing.Color.White;
            this.Button149.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button149.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button149.Location = new System.Drawing.Point(677, 3);
            this.Button149.Name = "Button149";
            this.Button149.Size = new System.Drawing.Size(36, 24);
            this.Button149.TabIndex = 114;
            this.Button149.Tag = "1";
            this.Button149.UseVisualStyleBackColor = false;
            // 
            // Button150
            // 
            this.Button150.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button150.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button150.DrawOnGlass = false;
            this.Button150.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button150.ForeColor = System.Drawing.Color.White;
            this.Button150.Image = ((System.Drawing.Image)(resources.GetObject("Button150.Image")));
            this.Button150.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button150.Location = new System.Drawing.Point(715, 3);
            this.Button150.Name = "Button150";
            this.Button150.Size = new System.Drawing.Size(36, 24);
            this.Button150.TabIndex = 113;
            this.Button150.Tag = "3";
            this.Button150.UseVisualStyleBackColor = false;
            // 
            // TextBox47
            // 
            this.TextBox47.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox47.DrawOnGlass = false;
            this.TextBox47.ForeColor = System.Drawing.Color.White;
            this.TextBox47.Location = new System.Drawing.Point(201, 3);
            this.TextBox47.MaxLength = 32767;
            this.TextBox47.Multiline = false;
            this.TextBox47.Name = "TextBox47";
            this.TextBox47.ReadOnly = false;
            this.TextBox47.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox47.SelectedText = "";
            this.TextBox47.SelectionLength = 0;
            this.TextBox47.SelectionStart = 0;
            this.TextBox47.Size = new System.Drawing.Size(434, 24);
            this.TextBox47.TabIndex = 1;
            this.TextBox47.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox47.UseSystemPasswordChar = false;
            this.TextBox47.WordWrap = true;
            // 
            // Label48
            // 
            this.Label48.BackColor = System.Drawing.Color.Transparent;
            this.Label48.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label48.Location = new System.Drawing.Point(33, 4);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(135, 22);
            this.Label48.TabIndex = 112;
            this.Label48.Text = "Device failure:";
            this.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox47
            // 
            this.PictureBox47.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox47.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox47.Image")));
            this.PictureBox47.Location = new System.Drawing.Point(3, 3);
            this.PictureBox47.Name = "PictureBox47";
            this.PictureBox47.Size = new System.Drawing.Size(24, 24);
            this.PictureBox47.TabIndex = 1;
            this.PictureBox47.TabStop = false;
            // 
            // GroupBox47
            // 
            this.GroupBox47.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox47.Controls.Add(this.Button145);
            this.GroupBox47.Controls.Add(this.Button146);
            this.GroupBox47.Controls.Add(this.Button147);
            this.GroupBox47.Controls.Add(this.TextBox46);
            this.GroupBox47.Controls.Add(this.Label47);
            this.GroupBox47.Controls.Add(this.PictureBox46);
            this.GroupBox47.Location = new System.Drawing.Point(4, 37);
            this.GroupBox47.Name = "GroupBox47";
            this.GroupBox47.Size = new System.Drawing.Size(754, 30);
            this.GroupBox47.TabIndex = 48;
            // 
            // Button145
            // 
            this.Button145.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button145.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button145.DrawOnGlass = false;
            this.Button145.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button145.ForeColor = System.Drawing.Color.White;
            this.Button145.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button145.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button145.Location = new System.Drawing.Point(639, 3);
            this.Button145.Name = "Button145";
            this.Button145.Size = new System.Drawing.Size(36, 24);
            this.Button145.TabIndex = 115;
            this.Button145.Tag = "2";
            this.Button145.UseVisualStyleBackColor = false;
            // 
            // Button146
            // 
            this.Button146.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button146.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button146.DrawOnGlass = false;
            this.Button146.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button146.ForeColor = System.Drawing.Color.White;
            this.Button146.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button146.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button146.Location = new System.Drawing.Point(677, 3);
            this.Button146.Name = "Button146";
            this.Button146.Size = new System.Drawing.Size(36, 24);
            this.Button146.TabIndex = 114;
            this.Button146.Tag = "1";
            this.Button146.UseVisualStyleBackColor = false;
            // 
            // Button147
            // 
            this.Button147.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button147.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button147.DrawOnGlass = false;
            this.Button147.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button147.ForeColor = System.Drawing.Color.White;
            this.Button147.Image = ((System.Drawing.Image)(resources.GetObject("Button147.Image")));
            this.Button147.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button147.Location = new System.Drawing.Point(715, 3);
            this.Button147.Name = "Button147";
            this.Button147.Size = new System.Drawing.Size(36, 24);
            this.Button147.TabIndex = 113;
            this.Button147.Tag = "3";
            this.Button147.UseVisualStyleBackColor = false;
            // 
            // TextBox46
            // 
            this.TextBox46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox46.DrawOnGlass = false;
            this.TextBox46.ForeColor = System.Drawing.Color.White;
            this.TextBox46.Location = new System.Drawing.Point(201, 3);
            this.TextBox46.MaxLength = 32767;
            this.TextBox46.Multiline = false;
            this.TextBox46.Name = "TextBox46";
            this.TextBox46.ReadOnly = false;
            this.TextBox46.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox46.SelectedText = "";
            this.TextBox46.SelectionLength = 0;
            this.TextBox46.SelectionStart = 0;
            this.TextBox46.Size = new System.Drawing.Size(434, 24);
            this.TextBox46.TabIndex = 1;
            this.TextBox46.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox46.UseSystemPasswordChar = false;
            this.TextBox46.WordWrap = true;
            // 
            // Label47
            // 
            this.Label47.BackColor = System.Drawing.Color.Transparent;
            this.Label47.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label47.Location = new System.Drawing.Point(33, 4);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(135, 22);
            this.Label47.TabIndex = 112;
            this.Label47.Text = "Device disconnection:";
            this.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox46
            // 
            this.PictureBox46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox46.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox46.Image")));
            this.PictureBox46.Location = new System.Drawing.Point(3, 3);
            this.PictureBox46.Name = "PictureBox46";
            this.PictureBox46.Size = new System.Drawing.Size(24, 24);
            this.PictureBox46.TabIndex = 1;
            this.PictureBox46.TabStop = false;
            // 
            // GroupBox46
            // 
            this.GroupBox46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox46.Controls.Add(this.Button142);
            this.GroupBox46.Controls.Add(this.Button143);
            this.GroupBox46.Controls.Add(this.Button144);
            this.GroupBox46.Controls.Add(this.TextBox45);
            this.GroupBox46.Controls.Add(this.Label46);
            this.GroupBox46.Controls.Add(this.PictureBox45);
            this.GroupBox46.Location = new System.Drawing.Point(4, 3);
            this.GroupBox46.Name = "GroupBox46";
            this.GroupBox46.Size = new System.Drawing.Size(754, 30);
            this.GroupBox46.TabIndex = 47;
            // 
            // Button142
            // 
            this.Button142.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button142.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button142.DrawOnGlass = false;
            this.Button142.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button142.ForeColor = System.Drawing.Color.White;
            this.Button142.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button142.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button142.Location = new System.Drawing.Point(639, 3);
            this.Button142.Name = "Button142";
            this.Button142.Size = new System.Drawing.Size(36, 24);
            this.Button142.TabIndex = 115;
            this.Button142.Tag = "2";
            this.Button142.UseVisualStyleBackColor = false;
            // 
            // Button143
            // 
            this.Button143.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button143.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button143.DrawOnGlass = false;
            this.Button143.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button143.ForeColor = System.Drawing.Color.White;
            this.Button143.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button143.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button143.Location = new System.Drawing.Point(677, 3);
            this.Button143.Name = "Button143";
            this.Button143.Size = new System.Drawing.Size(36, 24);
            this.Button143.TabIndex = 114;
            this.Button143.Tag = "1";
            this.Button143.UseVisualStyleBackColor = false;
            // 
            // Button144
            // 
            this.Button144.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button144.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button144.DrawOnGlass = false;
            this.Button144.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button144.ForeColor = System.Drawing.Color.White;
            this.Button144.Image = ((System.Drawing.Image)(resources.GetObject("Button144.Image")));
            this.Button144.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button144.Location = new System.Drawing.Point(715, 3);
            this.Button144.Name = "Button144";
            this.Button144.Size = new System.Drawing.Size(36, 24);
            this.Button144.TabIndex = 113;
            this.Button144.Tag = "3";
            this.Button144.UseVisualStyleBackColor = false;
            // 
            // TextBox45
            // 
            this.TextBox45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox45.DrawOnGlass = false;
            this.TextBox45.ForeColor = System.Drawing.Color.White;
            this.TextBox45.Location = new System.Drawing.Point(201, 3);
            this.TextBox45.MaxLength = 32767;
            this.TextBox45.Multiline = false;
            this.TextBox45.Name = "TextBox45";
            this.TextBox45.ReadOnly = false;
            this.TextBox45.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox45.SelectedText = "";
            this.TextBox45.SelectionLength = 0;
            this.TextBox45.SelectionStart = 0;
            this.TextBox45.Size = new System.Drawing.Size(434, 24);
            this.TextBox45.TabIndex = 1;
            this.TextBox45.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox45.UseSystemPasswordChar = false;
            this.TextBox45.WordWrap = true;
            // 
            // Label46
            // 
            this.Label46.BackColor = System.Drawing.Color.Transparent;
            this.Label46.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label46.Location = new System.Drawing.Point(33, 4);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(135, 22);
            this.Label46.TabIndex = 112;
            this.Label46.Text = "Device connection:";
            this.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox45
            // 
            this.PictureBox45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox45.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox45.Image")));
            this.PictureBox45.Location = new System.Drawing.Point(3, 3);
            this.PictureBox45.Name = "PictureBox45";
            this.PictureBox45.Size = new System.Drawing.Size(24, 24);
            this.PictureBox45.TabIndex = 1;
            this.PictureBox45.TabStop = false;
            // 
            // TabPage11
            // 
            this.TabPage11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage11.Controls.Add(this.GroupBox79);
            this.TabPage11.Controls.Add(this.GroupBox80);
            this.TabPage11.Controls.Add(this.GroupBox78);
            this.TabPage11.Controls.Add(this.GroupBox77);
            this.TabPage11.Controls.Add(this.GroupBox52);
            this.TabPage11.Location = new System.Drawing.Point(144, 4);
            this.TabPage11.Name = "TabPage11";
            this.TabPage11.Size = new System.Drawing.Size(761, 368);
            this.TabPage11.TabIndex = 10;
            this.TabPage11.Text = "Fax";
            // 
            // GroupBox79
            // 
            this.GroupBox79.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox79.Controls.Add(this.Button241);
            this.GroupBox79.Controls.Add(this.Button242);
            this.GroupBox79.Controls.Add(this.Button243);
            this.GroupBox79.Controls.Add(this.TextBox78);
            this.GroupBox79.Controls.Add(this.Label79);
            this.GroupBox79.Controls.Add(this.PictureBox78);
            this.GroupBox79.Location = new System.Drawing.Point(4, 139);
            this.GroupBox79.Name = "GroupBox79";
            this.GroupBox79.Size = new System.Drawing.Size(754, 30);
            this.GroupBox79.TabIndex = 120;
            // 
            // Button241
            // 
            this.Button241.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button241.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button241.DrawOnGlass = false;
            this.Button241.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button241.ForeColor = System.Drawing.Color.White;
            this.Button241.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button241.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button241.Location = new System.Drawing.Point(639, 3);
            this.Button241.Name = "Button241";
            this.Button241.Size = new System.Drawing.Size(36, 24);
            this.Button241.TabIndex = 115;
            this.Button241.Tag = "2";
            this.Button241.UseVisualStyleBackColor = false;
            // 
            // Button242
            // 
            this.Button242.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button242.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button242.DrawOnGlass = false;
            this.Button242.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button242.ForeColor = System.Drawing.Color.White;
            this.Button242.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button242.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button242.Location = new System.Drawing.Point(677, 3);
            this.Button242.Name = "Button242";
            this.Button242.Size = new System.Drawing.Size(36, 24);
            this.Button242.TabIndex = 114;
            this.Button242.Tag = "1";
            this.Button242.UseVisualStyleBackColor = false;
            // 
            // Button243
            // 
            this.Button243.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button243.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button243.DrawOnGlass = false;
            this.Button243.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button243.ForeColor = System.Drawing.Color.White;
            this.Button243.Image = ((System.Drawing.Image)(resources.GetObject("Button243.Image")));
            this.Button243.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button243.Location = new System.Drawing.Point(715, 3);
            this.Button243.Name = "Button243";
            this.Button243.Size = new System.Drawing.Size(36, 24);
            this.Button243.TabIndex = 113;
            this.Button243.Tag = "3";
            this.Button243.UseVisualStyleBackColor = false;
            // 
            // TextBox78
            // 
            this.TextBox78.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox78.DrawOnGlass = false;
            this.TextBox78.ForeColor = System.Drawing.Color.White;
            this.TextBox78.Location = new System.Drawing.Point(174, 3);
            this.TextBox78.MaxLength = 32767;
            this.TextBox78.Multiline = false;
            this.TextBox78.Name = "TextBox78";
            this.TextBox78.ReadOnly = false;
            this.TextBox78.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox78.SelectedText = "";
            this.TextBox78.SelectionLength = 0;
            this.TextBox78.SelectionStart = 0;
            this.TextBox78.Size = new System.Drawing.Size(461, 24);
            this.TextBox78.TabIndex = 1;
            this.TextBox78.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox78.UseSystemPasswordChar = false;
            this.TextBox78.WordWrap = true;
            // 
            // Label79
            // 
            this.Label79.BackColor = System.Drawing.Color.Transparent;
            this.Label79.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label79.Location = new System.Drawing.Point(33, 4);
            this.Label79.Name = "Label79";
            this.Label79.Size = new System.Drawing.Size(135, 22);
            this.Label79.TabIndex = 112;
            this.Label79.Text = "Error:";
            this.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox78
            // 
            this.PictureBox78.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox78.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox78.Image")));
            this.PictureBox78.Location = new System.Drawing.Point(3, 3);
            this.PictureBox78.Name = "PictureBox78";
            this.PictureBox78.Size = new System.Drawing.Size(24, 24);
            this.PictureBox78.TabIndex = 1;
            this.PictureBox78.TabStop = false;
            // 
            // GroupBox80
            // 
            this.GroupBox80.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox80.Controls.Add(this.Button244);
            this.GroupBox80.Controls.Add(this.Button245);
            this.GroupBox80.Controls.Add(this.Button246);
            this.GroupBox80.Controls.Add(this.TextBox79);
            this.GroupBox80.Controls.Add(this.Label80);
            this.GroupBox80.Controls.Add(this.PictureBox79);
            this.GroupBox80.Location = new System.Drawing.Point(4, 3);
            this.GroupBox80.Name = "GroupBox80";
            this.GroupBox80.Size = new System.Drawing.Size(754, 30);
            this.GroupBox80.TabIndex = 119;
            // 
            // Button244
            // 
            this.Button244.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button244.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button244.DrawOnGlass = false;
            this.Button244.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button244.ForeColor = System.Drawing.Color.White;
            this.Button244.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button244.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button244.Location = new System.Drawing.Point(639, 3);
            this.Button244.Name = "Button244";
            this.Button244.Size = new System.Drawing.Size(36, 24);
            this.Button244.TabIndex = 115;
            this.Button244.Tag = "2";
            this.Button244.UseVisualStyleBackColor = false;
            // 
            // Button245
            // 
            this.Button245.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button245.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button245.DrawOnGlass = false;
            this.Button245.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button245.ForeColor = System.Drawing.Color.White;
            this.Button245.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button245.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button245.Location = new System.Drawing.Point(677, 3);
            this.Button245.Name = "Button245";
            this.Button245.Size = new System.Drawing.Size(36, 24);
            this.Button245.TabIndex = 114;
            this.Button245.Tag = "1";
            this.Button245.UseVisualStyleBackColor = false;
            // 
            // Button246
            // 
            this.Button246.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button246.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button246.DrawOnGlass = false;
            this.Button246.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button246.ForeColor = System.Drawing.Color.White;
            this.Button246.Image = ((System.Drawing.Image)(resources.GetObject("Button246.Image")));
            this.Button246.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button246.Location = new System.Drawing.Point(715, 3);
            this.Button246.Name = "Button246";
            this.Button246.Size = new System.Drawing.Size(36, 24);
            this.Button246.TabIndex = 113;
            this.Button246.Tag = "3";
            this.Button246.UseVisualStyleBackColor = false;
            // 
            // TextBox79
            // 
            this.TextBox79.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox79.DrawOnGlass = false;
            this.TextBox79.ForeColor = System.Drawing.Color.White;
            this.TextBox79.Location = new System.Drawing.Point(174, 3);
            this.TextBox79.MaxLength = 32767;
            this.TextBox79.Multiline = false;
            this.TextBox79.Name = "TextBox79";
            this.TextBox79.ReadOnly = false;
            this.TextBox79.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox79.SelectedText = "";
            this.TextBox79.SelectionLength = 0;
            this.TextBox79.SelectionStart = 0;
            this.TextBox79.Size = new System.Drawing.Size(461, 24);
            this.TextBox79.TabIndex = 1;
            this.TextBox79.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox79.UseSystemPasswordChar = false;
            this.TextBox79.WordWrap = true;
            // 
            // Label80
            // 
            this.Label80.BackColor = System.Drawing.Color.Transparent;
            this.Label80.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label80.Location = new System.Drawing.Point(33, 4);
            this.Label80.Name = "Label80";
            this.Label80.Size = new System.Drawing.Size(135, 22);
            this.Label80.TabIndex = 112;
            this.Label80.Text = "Line rings:";
            this.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox79
            // 
            this.PictureBox79.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox79.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox79.Image")));
            this.PictureBox79.Location = new System.Drawing.Point(3, 3);
            this.PictureBox79.Name = "PictureBox79";
            this.PictureBox79.Size = new System.Drawing.Size(24, 24);
            this.PictureBox79.TabIndex = 1;
            this.PictureBox79.TabStop = false;
            // 
            // GroupBox78
            // 
            this.GroupBox78.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox78.Controls.Add(this.Button238);
            this.GroupBox78.Controls.Add(this.Button239);
            this.GroupBox78.Controls.Add(this.Button240);
            this.GroupBox78.Controls.Add(this.TextBox77);
            this.GroupBox78.Controls.Add(this.Label78);
            this.GroupBox78.Controls.Add(this.PictureBox77);
            this.GroupBox78.Location = new System.Drawing.Point(4, 105);
            this.GroupBox78.Name = "GroupBox78";
            this.GroupBox78.Size = new System.Drawing.Size(754, 30);
            this.GroupBox78.TabIndex = 118;
            // 
            // Button238
            // 
            this.Button238.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button238.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button238.DrawOnGlass = false;
            this.Button238.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button238.ForeColor = System.Drawing.Color.White;
            this.Button238.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button238.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button238.Location = new System.Drawing.Point(639, 3);
            this.Button238.Name = "Button238";
            this.Button238.Size = new System.Drawing.Size(36, 24);
            this.Button238.TabIndex = 115;
            this.Button238.Tag = "2";
            this.Button238.UseVisualStyleBackColor = false;
            // 
            // Button239
            // 
            this.Button239.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button239.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button239.DrawOnGlass = false;
            this.Button239.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button239.ForeColor = System.Drawing.Color.White;
            this.Button239.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button239.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button239.Location = new System.Drawing.Point(677, 3);
            this.Button239.Name = "Button239";
            this.Button239.Size = new System.Drawing.Size(36, 24);
            this.Button239.TabIndex = 114;
            this.Button239.Tag = "1";
            this.Button239.UseVisualStyleBackColor = false;
            // 
            // Button240
            // 
            this.Button240.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button240.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button240.DrawOnGlass = false;
            this.Button240.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button240.ForeColor = System.Drawing.Color.White;
            this.Button240.Image = ((System.Drawing.Image)(resources.GetObject("Button240.Image")));
            this.Button240.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button240.Location = new System.Drawing.Point(715, 3);
            this.Button240.Name = "Button240";
            this.Button240.Size = new System.Drawing.Size(36, 24);
            this.Button240.TabIndex = 113;
            this.Button240.Tag = "3";
            this.Button240.UseVisualStyleBackColor = false;
            // 
            // TextBox77
            // 
            this.TextBox77.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox77.DrawOnGlass = false;
            this.TextBox77.ForeColor = System.Drawing.Color.White;
            this.TextBox77.Location = new System.Drawing.Point(174, 3);
            this.TextBox77.MaxLength = 32767;
            this.TextBox77.Multiline = false;
            this.TextBox77.Name = "TextBox77";
            this.TextBox77.ReadOnly = false;
            this.TextBox77.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox77.SelectedText = "";
            this.TextBox77.SelectionLength = 0;
            this.TextBox77.SelectionStart = 0;
            this.TextBox77.Size = new System.Drawing.Size(461, 24);
            this.TextBox77.TabIndex = 1;
            this.TextBox77.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox77.UseSystemPasswordChar = false;
            this.TextBox77.WordWrap = true;
            // 
            // Label78
            // 
            this.Label78.BackColor = System.Drawing.Color.Transparent;
            this.Label78.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label78.Location = new System.Drawing.Point(33, 4);
            this.Label78.Name = "Label78";
            this.Label78.Size = new System.Drawing.Size(135, 22);
            this.Label78.TabIndex = 112;
            this.Label78.Text = "Sent:";
            this.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox77
            // 
            this.PictureBox77.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox77.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox77.Image")));
            this.PictureBox77.Location = new System.Drawing.Point(3, 3);
            this.PictureBox77.Name = "PictureBox77";
            this.PictureBox77.Size = new System.Drawing.Size(24, 24);
            this.PictureBox77.TabIndex = 1;
            this.PictureBox77.TabStop = false;
            // 
            // GroupBox77
            // 
            this.GroupBox77.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox77.Controls.Add(this.Button235);
            this.GroupBox77.Controls.Add(this.Button236);
            this.GroupBox77.Controls.Add(this.Button237);
            this.GroupBox77.Controls.Add(this.TextBox76);
            this.GroupBox77.Controls.Add(this.Label77);
            this.GroupBox77.Controls.Add(this.PictureBox76);
            this.GroupBox77.Location = new System.Drawing.Point(4, 71);
            this.GroupBox77.Name = "GroupBox77";
            this.GroupBox77.Size = new System.Drawing.Size(754, 30);
            this.GroupBox77.TabIndex = 117;
            // 
            // Button235
            // 
            this.Button235.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button235.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button235.DrawOnGlass = false;
            this.Button235.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button235.ForeColor = System.Drawing.Color.White;
            this.Button235.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button235.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button235.Location = new System.Drawing.Point(639, 3);
            this.Button235.Name = "Button235";
            this.Button235.Size = new System.Drawing.Size(36, 24);
            this.Button235.TabIndex = 115;
            this.Button235.Tag = "2";
            this.Button235.UseVisualStyleBackColor = false;
            // 
            // Button236
            // 
            this.Button236.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button236.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button236.DrawOnGlass = false;
            this.Button236.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button236.ForeColor = System.Drawing.Color.White;
            this.Button236.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button236.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button236.Location = new System.Drawing.Point(677, 3);
            this.Button236.Name = "Button236";
            this.Button236.Size = new System.Drawing.Size(36, 24);
            this.Button236.TabIndex = 114;
            this.Button236.Tag = "1";
            this.Button236.UseVisualStyleBackColor = false;
            // 
            // Button237
            // 
            this.Button237.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button237.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button237.DrawOnGlass = false;
            this.Button237.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button237.ForeColor = System.Drawing.Color.White;
            this.Button237.Image = ((System.Drawing.Image)(resources.GetObject("Button237.Image")));
            this.Button237.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button237.Location = new System.Drawing.Point(715, 3);
            this.Button237.Name = "Button237";
            this.Button237.Size = new System.Drawing.Size(36, 24);
            this.Button237.TabIndex = 113;
            this.Button237.Tag = "3";
            this.Button237.UseVisualStyleBackColor = false;
            // 
            // TextBox76
            // 
            this.TextBox76.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox76.DrawOnGlass = false;
            this.TextBox76.ForeColor = System.Drawing.Color.White;
            this.TextBox76.Location = new System.Drawing.Point(174, 3);
            this.TextBox76.MaxLength = 32767;
            this.TextBox76.Multiline = false;
            this.TextBox76.Name = "TextBox76";
            this.TextBox76.ReadOnly = false;
            this.TextBox76.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox76.SelectedText = "";
            this.TextBox76.SelectionLength = 0;
            this.TextBox76.SelectionStart = 0;
            this.TextBox76.Size = new System.Drawing.Size(461, 24);
            this.TextBox76.TabIndex = 1;
            this.TextBox76.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox76.UseSystemPasswordChar = false;
            this.TextBox76.WordWrap = true;
            // 
            // Label77
            // 
            this.Label77.BackColor = System.Drawing.Color.Transparent;
            this.Label77.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label77.Location = new System.Drawing.Point(33, 4);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(135, 22);
            this.Label77.TabIndex = 112;
            this.Label77.Text = "New:";
            this.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox76
            // 
            this.PictureBox76.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox76.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox76.Image")));
            this.PictureBox76.Location = new System.Drawing.Point(3, 3);
            this.PictureBox76.Name = "PictureBox76";
            this.PictureBox76.Size = new System.Drawing.Size(24, 24);
            this.PictureBox76.TabIndex = 1;
            this.PictureBox76.TabStop = false;
            // 
            // GroupBox52
            // 
            this.GroupBox52.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox52.Controls.Add(this.Button160);
            this.GroupBox52.Controls.Add(this.Button161);
            this.GroupBox52.Controls.Add(this.Button162);
            this.GroupBox52.Controls.Add(this.TextBox51);
            this.GroupBox52.Controls.Add(this.Label52);
            this.GroupBox52.Controls.Add(this.PictureBox51);
            this.GroupBox52.Location = new System.Drawing.Point(4, 37);
            this.GroupBox52.Name = "GroupBox52";
            this.GroupBox52.Size = new System.Drawing.Size(754, 30);
            this.GroupBox52.TabIndex = 116;
            // 
            // Button160
            // 
            this.Button160.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button160.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button160.DrawOnGlass = false;
            this.Button160.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button160.ForeColor = System.Drawing.Color.White;
            this.Button160.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button160.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button160.Location = new System.Drawing.Point(639, 3);
            this.Button160.Name = "Button160";
            this.Button160.Size = new System.Drawing.Size(36, 24);
            this.Button160.TabIndex = 115;
            this.Button160.Tag = "2";
            this.Button160.UseVisualStyleBackColor = false;
            // 
            // Button161
            // 
            this.Button161.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button161.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button161.DrawOnGlass = false;
            this.Button161.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button161.ForeColor = System.Drawing.Color.White;
            this.Button161.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button161.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button161.Location = new System.Drawing.Point(677, 3);
            this.Button161.Name = "Button161";
            this.Button161.Size = new System.Drawing.Size(36, 24);
            this.Button161.TabIndex = 114;
            this.Button161.Tag = "1";
            this.Button161.UseVisualStyleBackColor = false;
            // 
            // Button162
            // 
            this.Button162.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button162.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button162.DrawOnGlass = false;
            this.Button162.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button162.ForeColor = System.Drawing.Color.White;
            this.Button162.Image = ((System.Drawing.Image)(resources.GetObject("Button162.Image")));
            this.Button162.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button162.Location = new System.Drawing.Point(715, 3);
            this.Button162.Name = "Button162";
            this.Button162.Size = new System.Drawing.Size(36, 24);
            this.Button162.TabIndex = 113;
            this.Button162.Tag = "3";
            this.Button162.UseVisualStyleBackColor = false;
            // 
            // TextBox51
            // 
            this.TextBox51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox51.DrawOnGlass = false;
            this.TextBox51.ForeColor = System.Drawing.Color.White;
            this.TextBox51.Location = new System.Drawing.Point(174, 3);
            this.TextBox51.MaxLength = 32767;
            this.TextBox51.Multiline = false;
            this.TextBox51.Name = "TextBox51";
            this.TextBox51.ReadOnly = false;
            this.TextBox51.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox51.SelectedText = "";
            this.TextBox51.SelectionLength = 0;
            this.TextBox51.SelectionStart = 0;
            this.TextBox51.Size = new System.Drawing.Size(461, 24);
            this.TextBox51.TabIndex = 1;
            this.TextBox51.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox51.UseSystemPasswordChar = false;
            this.TextBox51.WordWrap = true;
            // 
            // Label52
            // 
            this.Label52.BackColor = System.Drawing.Color.Transparent;
            this.Label52.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label52.Location = new System.Drawing.Point(33, 4);
            this.Label52.Name = "Label52";
            this.Label52.Size = new System.Drawing.Size(135, 22);
            this.Label52.TabIndex = 112;
            this.Label52.Text = "Beep:";
            this.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox51
            // 
            this.PictureBox51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox51.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox51.Image")));
            this.PictureBox51.Location = new System.Drawing.Point(3, 3);
            this.PictureBox51.Name = "PictureBox51";
            this.PictureBox51.Size = new System.Drawing.Size(24, 24);
            this.PictureBox51.TabIndex = 1;
            this.PictureBox51.TabStop = false;
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage8.Controls.Add(this.GroupBox76);
            this.TabPage8.Controls.Add(this.GroupBox64);
            this.TabPage8.Controls.Add(this.GroupBox57);
            this.TabPage8.Controls.Add(this.GroupBox58);
            this.TabPage8.Controls.Add(this.GroupBox59);
            this.TabPage8.Controls.Add(this.GroupBox60);
            this.TabPage8.Controls.Add(this.GroupBox61);
            this.TabPage8.Controls.Add(this.GroupBox62);
            this.TabPage8.Controls.Add(this.GroupBox63);
            this.TabPage8.Location = new System.Drawing.Point(144, 4);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(761, 368);
            this.TabPage8.TabIndex = 7;
            this.TabPage8.Text = "Explorer events";
            // 
            // GroupBox76
            // 
            this.GroupBox76.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox76.Controls.Add(this.Button232);
            this.GroupBox76.Controls.Add(this.Button233);
            this.GroupBox76.Controls.Add(this.Button234);
            this.GroupBox76.Controls.Add(this.TextBox75);
            this.GroupBox76.Controls.Add(this.Label76);
            this.GroupBox76.Controls.Add(this.PictureBox75);
            this.GroupBox76.Location = new System.Drawing.Point(4, 275);
            this.GroupBox76.Name = "GroupBox76";
            this.GroupBox76.Size = new System.Drawing.Size(754, 30);
            this.GroupBox76.TabIndex = 126;
            // 
            // Button232
            // 
            this.Button232.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button232.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button232.DrawOnGlass = false;
            this.Button232.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button232.ForeColor = System.Drawing.Color.White;
            this.Button232.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button232.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button232.Location = new System.Drawing.Point(639, 3);
            this.Button232.Name = "Button232";
            this.Button232.Size = new System.Drawing.Size(36, 24);
            this.Button232.TabIndex = 115;
            this.Button232.Tag = "2";
            this.Button232.UseVisualStyleBackColor = false;
            // 
            // Button233
            // 
            this.Button233.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button233.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button233.DrawOnGlass = false;
            this.Button233.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button233.ForeColor = System.Drawing.Color.White;
            this.Button233.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button233.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button233.Location = new System.Drawing.Point(677, 3);
            this.Button233.Name = "Button233";
            this.Button233.Size = new System.Drawing.Size(36, 24);
            this.Button233.TabIndex = 114;
            this.Button233.Tag = "1";
            this.Button233.UseVisualStyleBackColor = false;
            // 
            // Button234
            // 
            this.Button234.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button234.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button234.DrawOnGlass = false;
            this.Button234.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button234.ForeColor = System.Drawing.Color.White;
            this.Button234.Image = ((System.Drawing.Image)(resources.GetObject("Button234.Image")));
            this.Button234.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button234.Location = new System.Drawing.Point(715, 3);
            this.Button234.Name = "Button234";
            this.Button234.Size = new System.Drawing.Size(36, 24);
            this.Button234.TabIndex = 113;
            this.Button234.Tag = "3";
            this.Button234.UseVisualStyleBackColor = false;
            // 
            // TextBox75
            // 
            this.TextBox75.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox75.DrawOnGlass = false;
            this.TextBox75.ForeColor = System.Drawing.Color.White;
            this.TextBox75.Location = new System.Drawing.Point(203, 3);
            this.TextBox75.MaxLength = 32767;
            this.TextBox75.Multiline = false;
            this.TextBox75.Name = "TextBox75";
            this.TextBox75.ReadOnly = false;
            this.TextBox75.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox75.SelectedText = "";
            this.TextBox75.SelectionLength = 0;
            this.TextBox75.SelectionStart = 0;
            this.TextBox75.Size = new System.Drawing.Size(432, 24);
            this.TextBox75.TabIndex = 1;
            this.TextBox75.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox75.UseSystemPasswordChar = false;
            this.TextBox75.WordWrap = true;
            // 
            // Label76
            // 
            this.Label76.BackColor = System.Drawing.Color.Transparent;
            this.Label76.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label76.Location = new System.Drawing.Point(33, 4);
            this.Label76.Name = "Label76";
            this.Label76.Size = new System.Drawing.Size(164, 22);
            this.Label76.TabIndex = 112;
            this.Label76.Text = "Search provider discovered:";
            this.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox75
            // 
            this.PictureBox75.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox75.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox75.Image")));
            this.PictureBox75.Location = new System.Drawing.Point(3, 3);
            this.PictureBox75.Name = "PictureBox75";
            this.PictureBox75.Size = new System.Drawing.Size(24, 24);
            this.PictureBox75.TabIndex = 1;
            this.PictureBox75.TabStop = false;
            // 
            // GroupBox64
            // 
            this.GroupBox64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox64.Controls.Add(this.Button196);
            this.GroupBox64.Controls.Add(this.Button197);
            this.GroupBox64.Controls.Add(this.Button198);
            this.GroupBox64.Controls.Add(this.TextBox63);
            this.GroupBox64.Controls.Add(this.Label64);
            this.GroupBox64.Controls.Add(this.PictureBox63);
            this.GroupBox64.Location = new System.Drawing.Point(4, 139);
            this.GroupBox64.Name = "GroupBox64";
            this.GroupBox64.Size = new System.Drawing.Size(754, 30);
            this.GroupBox64.TabIndex = 125;
            // 
            // Button196
            // 
            this.Button196.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button196.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button196.DrawOnGlass = false;
            this.Button196.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button196.ForeColor = System.Drawing.Color.White;
            this.Button196.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button196.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button196.Location = new System.Drawing.Point(639, 3);
            this.Button196.Name = "Button196";
            this.Button196.Size = new System.Drawing.Size(36, 24);
            this.Button196.TabIndex = 115;
            this.Button196.Tag = "2";
            this.Button196.UseVisualStyleBackColor = false;
            // 
            // Button197
            // 
            this.Button197.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button197.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button197.DrawOnGlass = false;
            this.Button197.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button197.ForeColor = System.Drawing.Color.White;
            this.Button197.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button197.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button197.Location = new System.Drawing.Point(677, 3);
            this.Button197.Name = "Button197";
            this.Button197.Size = new System.Drawing.Size(36, 24);
            this.Button197.TabIndex = 114;
            this.Button197.Tag = "1";
            this.Button197.UseVisualStyleBackColor = false;
            // 
            // Button198
            // 
            this.Button198.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button198.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button198.DrawOnGlass = false;
            this.Button198.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button198.ForeColor = System.Drawing.Color.White;
            this.Button198.Image = ((System.Drawing.Image)(resources.GetObject("Button198.Image")));
            this.Button198.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button198.Location = new System.Drawing.Point(715, 3);
            this.Button198.Name = "Button198";
            this.Button198.Size = new System.Drawing.Size(36, 24);
            this.Button198.TabIndex = 113;
            this.Button198.Tag = "3";
            this.Button198.UseVisualStyleBackColor = false;
            // 
            // TextBox63
            // 
            this.TextBox63.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox63.DrawOnGlass = false;
            this.TextBox63.ForeColor = System.Drawing.Color.White;
            this.TextBox63.Location = new System.Drawing.Point(203, 3);
            this.TextBox63.MaxLength = 32767;
            this.TextBox63.Multiline = false;
            this.TextBox63.Name = "TextBox63";
            this.TextBox63.ReadOnly = false;
            this.TextBox63.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox63.SelectedText = "";
            this.TextBox63.SelectionLength = 0;
            this.TextBox63.SelectionStart = 0;
            this.TextBox63.Size = new System.Drawing.Size(432, 24);
            this.TextBox63.TabIndex = 1;
            this.TextBox63.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox63.UseSystemPasswordChar = false;
            this.TextBox63.WordWrap = true;
            // 
            // Label64
            // 
            this.Label64.BackColor = System.Drawing.Color.Transparent;
            this.Label64.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label64.Location = new System.Drawing.Point(33, 4);
            this.Label64.Name = "Label64";
            this.Label64.Size = new System.Drawing.Size(164, 22);
            this.Label64.TabIndex = 112;
            this.Label64.Text = "Show band:";
            this.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox63
            // 
            this.PictureBox63.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox63.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox63.Image")));
            this.PictureBox63.Location = new System.Drawing.Point(3, 3);
            this.PictureBox63.Name = "PictureBox63";
            this.PictureBox63.Size = new System.Drawing.Size(24, 24);
            this.PictureBox63.TabIndex = 1;
            this.PictureBox63.TabStop = false;
            // 
            // GroupBox57
            // 
            this.GroupBox57.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox57.Controls.Add(this.Button175);
            this.GroupBox57.Controls.Add(this.Button176);
            this.GroupBox57.Controls.Add(this.Button177);
            this.GroupBox57.Controls.Add(this.TextBox56);
            this.GroupBox57.Controls.Add(this.Label57);
            this.GroupBox57.Controls.Add(this.PictureBox56);
            this.GroupBox57.Location = new System.Drawing.Point(4, 71);
            this.GroupBox57.Name = "GroupBox57";
            this.GroupBox57.Size = new System.Drawing.Size(754, 30);
            this.GroupBox57.TabIndex = 124;
            // 
            // Button175
            // 
            this.Button175.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button175.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button175.DrawOnGlass = false;
            this.Button175.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button175.ForeColor = System.Drawing.Color.White;
            this.Button175.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button175.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button175.Location = new System.Drawing.Point(639, 3);
            this.Button175.Name = "Button175";
            this.Button175.Size = new System.Drawing.Size(36, 24);
            this.Button175.TabIndex = 115;
            this.Button175.Tag = "2";
            this.Button175.UseVisualStyleBackColor = false;
            // 
            // Button176
            // 
            this.Button176.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button176.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button176.DrawOnGlass = false;
            this.Button176.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button176.ForeColor = System.Drawing.Color.White;
            this.Button176.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button176.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button176.Location = new System.Drawing.Point(677, 3);
            this.Button176.Name = "Button176";
            this.Button176.Size = new System.Drawing.Size(36, 24);
            this.Button176.TabIndex = 114;
            this.Button176.Tag = "1";
            this.Button176.UseVisualStyleBackColor = false;
            // 
            // Button177
            // 
            this.Button177.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button177.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button177.DrawOnGlass = false;
            this.Button177.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button177.ForeColor = System.Drawing.Color.White;
            this.Button177.Image = ((System.Drawing.Image)(resources.GetObject("Button177.Image")));
            this.Button177.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button177.Location = new System.Drawing.Point(715, 3);
            this.Button177.Name = "Button177";
            this.Button177.Size = new System.Drawing.Size(36, 24);
            this.Button177.TabIndex = 113;
            this.Button177.Tag = "3";
            this.Button177.UseVisualStyleBackColor = false;
            // 
            // TextBox56
            // 
            this.TextBox56.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox56.DrawOnGlass = false;
            this.TextBox56.ForeColor = System.Drawing.Color.White;
            this.TextBox56.Location = new System.Drawing.Point(203, 3);
            this.TextBox56.MaxLength = 32767;
            this.TextBox56.Multiline = false;
            this.TextBox56.Name = "TextBox56";
            this.TextBox56.ReadOnly = false;
            this.TextBox56.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox56.SelectedText = "";
            this.TextBox56.SelectionLength = 0;
            this.TextBox56.SelectionStart = 0;
            this.TextBox56.Size = new System.Drawing.Size(432, 24);
            this.TextBox56.TabIndex = 1;
            this.TextBox56.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox56.UseSystemPasswordChar = false;
            this.TextBox56.WordWrap = true;
            // 
            // Label57
            // 
            this.Label57.BackColor = System.Drawing.Color.Transparent;
            this.Label57.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label57.Location = new System.Drawing.Point(33, 4);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(164, 22);
            this.Label57.TabIndex = 112;
            this.Label57.Text = "Move menu item:";
            this.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox56
            // 
            this.PictureBox56.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox56.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox56.Image")));
            this.PictureBox56.Location = new System.Drawing.Point(3, 3);
            this.PictureBox56.Name = "PictureBox56";
            this.PictureBox56.Size = new System.Drawing.Size(24, 24);
            this.PictureBox56.TabIndex = 1;
            this.PictureBox56.TabStop = false;
            // 
            // GroupBox58
            // 
            this.GroupBox58.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox58.Controls.Add(this.Button178);
            this.GroupBox58.Controls.Add(this.Button179);
            this.GroupBox58.Controls.Add(this.Button180);
            this.GroupBox58.Controls.Add(this.TextBox57);
            this.GroupBox58.Controls.Add(this.Label58);
            this.GroupBox58.Controls.Add(this.PictureBox57);
            this.GroupBox58.Location = new System.Drawing.Point(4, 241);
            this.GroupBox58.Name = "GroupBox58";
            this.GroupBox58.Size = new System.Drawing.Size(754, 30);
            this.GroupBox58.TabIndex = 123;
            // 
            // Button178
            // 
            this.Button178.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button178.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button178.DrawOnGlass = false;
            this.Button178.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button178.ForeColor = System.Drawing.Color.White;
            this.Button178.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button178.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button178.Location = new System.Drawing.Point(639, 3);
            this.Button178.Name = "Button178";
            this.Button178.Size = new System.Drawing.Size(36, 24);
            this.Button178.TabIndex = 115;
            this.Button178.Tag = "2";
            this.Button178.UseVisualStyleBackColor = false;
            // 
            // Button179
            // 
            this.Button179.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button179.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button179.DrawOnGlass = false;
            this.Button179.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button179.ForeColor = System.Drawing.Color.White;
            this.Button179.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button179.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button179.Location = new System.Drawing.Point(677, 3);
            this.Button179.Name = "Button179";
            this.Button179.Size = new System.Drawing.Size(36, 24);
            this.Button179.TabIndex = 114;
            this.Button179.Tag = "1";
            this.Button179.UseVisualStyleBackColor = false;
            // 
            // Button180
            // 
            this.Button180.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button180.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button180.DrawOnGlass = false;
            this.Button180.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button180.ForeColor = System.Drawing.Color.White;
            this.Button180.Image = ((System.Drawing.Image)(resources.GetObject("Button180.Image")));
            this.Button180.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button180.Location = new System.Drawing.Point(715, 3);
            this.Button180.Name = "Button180";
            this.Button180.Size = new System.Drawing.Size(36, 24);
            this.Button180.TabIndex = 113;
            this.Button180.Tag = "3";
            this.Button180.UseVisualStyleBackColor = false;
            // 
            // TextBox57
            // 
            this.TextBox57.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox57.DrawOnGlass = false;
            this.TextBox57.ForeColor = System.Drawing.Color.White;
            this.TextBox57.Location = new System.Drawing.Point(203, 3);
            this.TextBox57.MaxLength = 32767;
            this.TextBox57.Multiline = false;
            this.TextBox57.Name = "TextBox57";
            this.TextBox57.ReadOnly = false;
            this.TextBox57.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox57.SelectedText = "";
            this.TextBox57.SelectionLength = 0;
            this.TextBox57.SelectionStart = 0;
            this.TextBox57.Size = new System.Drawing.Size(432, 24);
            this.TextBox57.TabIndex = 1;
            this.TextBox57.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox57.UseSystemPasswordChar = false;
            this.TextBox57.WordWrap = true;
            // 
            // Label58
            // 
            this.Label58.BackColor = System.Drawing.Color.Transparent;
            this.Label58.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label58.Location = new System.Drawing.Point(33, 4);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(164, 22);
            this.Label58.TabIndex = 112;
            this.Label58.Text = "Feed discovered:";
            this.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox57
            // 
            this.PictureBox57.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox57.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox57.Image")));
            this.PictureBox57.Location = new System.Drawing.Point(3, 3);
            this.PictureBox57.Name = "PictureBox57";
            this.PictureBox57.Size = new System.Drawing.Size(24, 24);
            this.PictureBox57.TabIndex = 1;
            this.PictureBox57.TabStop = false;
            // 
            // GroupBox59
            // 
            this.GroupBox59.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox59.Controls.Add(this.Button181);
            this.GroupBox59.Controls.Add(this.Button182);
            this.GroupBox59.Controls.Add(this.Button183);
            this.GroupBox59.Controls.Add(this.TextBox58);
            this.GroupBox59.Controls.Add(this.Label59);
            this.GroupBox59.Controls.Add(this.PictureBox58);
            this.GroupBox59.Location = new System.Drawing.Point(4, 207);
            this.GroupBox59.Name = "GroupBox59";
            this.GroupBox59.Size = new System.Drawing.Size(754, 30);
            this.GroupBox59.TabIndex = 122;
            // 
            // Button181
            // 
            this.Button181.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button181.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button181.DrawOnGlass = false;
            this.Button181.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button181.ForeColor = System.Drawing.Color.White;
            this.Button181.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button181.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button181.Location = new System.Drawing.Point(639, 3);
            this.Button181.Name = "Button181";
            this.Button181.Size = new System.Drawing.Size(36, 24);
            this.Button181.TabIndex = 115;
            this.Button181.Tag = "2";
            this.Button181.UseVisualStyleBackColor = false;
            // 
            // Button182
            // 
            this.Button182.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button182.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button182.DrawOnGlass = false;
            this.Button182.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button182.ForeColor = System.Drawing.Color.White;
            this.Button182.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button182.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button182.Location = new System.Drawing.Point(677, 3);
            this.Button182.Name = "Button182";
            this.Button182.Size = new System.Drawing.Size(36, 24);
            this.Button182.TabIndex = 114;
            this.Button182.Tag = "1";
            this.Button182.UseVisualStyleBackColor = false;
            // 
            // Button183
            // 
            this.Button183.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button183.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button183.DrawOnGlass = false;
            this.Button183.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button183.ForeColor = System.Drawing.Color.White;
            this.Button183.Image = ((System.Drawing.Image)(resources.GetObject("Button183.Image")));
            this.Button183.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button183.Location = new System.Drawing.Point(715, 3);
            this.Button183.Name = "Button183";
            this.Button183.Size = new System.Drawing.Size(36, 24);
            this.Button183.TabIndex = 113;
            this.Button183.Tag = "3";
            this.Button183.UseVisualStyleBackColor = false;
            // 
            // TextBox58
            // 
            this.TextBox58.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox58.DrawOnGlass = false;
            this.TextBox58.ForeColor = System.Drawing.Color.White;
            this.TextBox58.Location = new System.Drawing.Point(203, 3);
            this.TextBox58.MaxLength = 32767;
            this.TextBox58.Multiline = false;
            this.TextBox58.Name = "TextBox58";
            this.TextBox58.ReadOnly = false;
            this.TextBox58.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox58.SelectedText = "";
            this.TextBox58.SelectionLength = 0;
            this.TextBox58.SelectionStart = 0;
            this.TextBox58.Size = new System.Drawing.Size(432, 24);
            this.TextBox58.TabIndex = 1;
            this.TextBox58.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox58.UseSystemPasswordChar = false;
            this.TextBox58.WordWrap = true;
            // 
            // Label59
            // 
            this.Label59.BackColor = System.Drawing.Color.Transparent;
            this.Label59.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label59.Location = new System.Drawing.Point(33, 4);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(164, 22);
            this.Label59.TabIndex = 112;
            this.Label59.Text = "Blocked popup:";
            this.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox58
            // 
            this.PictureBox58.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox58.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox58.Image")));
            this.PictureBox58.Location = new System.Drawing.Point(3, 3);
            this.PictureBox58.Name = "PictureBox58";
            this.PictureBox58.Size = new System.Drawing.Size(24, 24);
            this.PictureBox58.TabIndex = 1;
            this.PictureBox58.TabStop = false;
            // 
            // GroupBox60
            // 
            this.GroupBox60.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox60.Controls.Add(this.Button184);
            this.GroupBox60.Controls.Add(this.Button185);
            this.GroupBox60.Controls.Add(this.Button186);
            this.GroupBox60.Controls.Add(this.TextBox59);
            this.GroupBox60.Controls.Add(this.Label60);
            this.GroupBox60.Controls.Add(this.PictureBox59);
            this.GroupBox60.Location = new System.Drawing.Point(4, 173);
            this.GroupBox60.Name = "GroupBox60";
            this.GroupBox60.Size = new System.Drawing.Size(754, 30);
            this.GroupBox60.TabIndex = 121;
            // 
            // Button184
            // 
            this.Button184.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button184.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button184.DrawOnGlass = false;
            this.Button184.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button184.ForeColor = System.Drawing.Color.White;
            this.Button184.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button184.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button184.Location = new System.Drawing.Point(639, 3);
            this.Button184.Name = "Button184";
            this.Button184.Size = new System.Drawing.Size(36, 24);
            this.Button184.TabIndex = 115;
            this.Button184.Tag = "2";
            this.Button184.UseVisualStyleBackColor = false;
            // 
            // Button185
            // 
            this.Button185.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button185.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button185.DrawOnGlass = false;
            this.Button185.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button185.ForeColor = System.Drawing.Color.White;
            this.Button185.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button185.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button185.Location = new System.Drawing.Point(677, 3);
            this.Button185.Name = "Button185";
            this.Button185.Size = new System.Drawing.Size(36, 24);
            this.Button185.TabIndex = 114;
            this.Button185.Tag = "1";
            this.Button185.UseVisualStyleBackColor = false;
            // 
            // Button186
            // 
            this.Button186.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button186.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button186.DrawOnGlass = false;
            this.Button186.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button186.ForeColor = System.Drawing.Color.White;
            this.Button186.Image = ((System.Drawing.Image)(resources.GetObject("Button186.Image")));
            this.Button186.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button186.Location = new System.Drawing.Point(715, 3);
            this.Button186.Name = "Button186";
            this.Button186.Size = new System.Drawing.Size(36, 24);
            this.Button186.TabIndex = 113;
            this.Button186.Tag = "3";
            this.Button186.UseVisualStyleBackColor = false;
            // 
            // TextBox59
            // 
            this.TextBox59.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox59.DrawOnGlass = false;
            this.TextBox59.ForeColor = System.Drawing.Color.White;
            this.TextBox59.Location = new System.Drawing.Point(203, 3);
            this.TextBox59.MaxLength = 32767;
            this.TextBox59.Multiline = false;
            this.TextBox59.Name = "TextBox59";
            this.TextBox59.ReadOnly = false;
            this.TextBox59.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox59.SelectedText = "";
            this.TextBox59.SelectionLength = 0;
            this.TextBox59.SelectionStart = 0;
            this.TextBox59.Size = new System.Drawing.Size(432, 24);
            this.TextBox59.TabIndex = 1;
            this.TextBox59.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox59.UseSystemPasswordChar = false;
            this.TextBox59.WordWrap = true;
            // 
            // Label60
            // 
            this.Label60.BackColor = System.Drawing.Color.Transparent;
            this.Label60.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label60.Location = new System.Drawing.Point(33, 4);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(164, 22);
            this.Label60.TabIndex = 112;
            this.Label60.Text = "Security band:";
            this.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox59
            // 
            this.PictureBox59.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox59.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox59.Image")));
            this.PictureBox59.Location = new System.Drawing.Point(3, 3);
            this.PictureBox59.Name = "PictureBox59";
            this.PictureBox59.Size = new System.Drawing.Size(24, 24);
            this.PictureBox59.TabIndex = 1;
            this.PictureBox59.TabStop = false;
            // 
            // GroupBox61
            // 
            this.GroupBox61.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox61.Controls.Add(this.Button187);
            this.GroupBox61.Controls.Add(this.Button188);
            this.GroupBox61.Controls.Add(this.Button189);
            this.GroupBox61.Controls.Add(this.TextBox60);
            this.GroupBox61.Controls.Add(this.Label61);
            this.GroupBox61.Controls.Add(this.PictureBox60);
            this.GroupBox61.Location = new System.Drawing.Point(4, 105);
            this.GroupBox61.Name = "GroupBox61";
            this.GroupBox61.Size = new System.Drawing.Size(754, 30);
            this.GroupBox61.TabIndex = 120;
            // 
            // Button187
            // 
            this.Button187.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button187.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button187.DrawOnGlass = false;
            this.Button187.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button187.ForeColor = System.Drawing.Color.White;
            this.Button187.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button187.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button187.Location = new System.Drawing.Point(639, 3);
            this.Button187.Name = "Button187";
            this.Button187.Size = new System.Drawing.Size(36, 24);
            this.Button187.TabIndex = 115;
            this.Button187.Tag = "2";
            this.Button187.UseVisualStyleBackColor = false;
            // 
            // Button188
            // 
            this.Button188.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button188.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button188.DrawOnGlass = false;
            this.Button188.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button188.ForeColor = System.Drawing.Color.White;
            this.Button188.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button188.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button188.Location = new System.Drawing.Point(677, 3);
            this.Button188.Name = "Button188";
            this.Button188.Size = new System.Drawing.Size(36, 24);
            this.Button188.TabIndex = 114;
            this.Button188.Tag = "1";
            this.Button188.UseVisualStyleBackColor = false;
            // 
            // Button189
            // 
            this.Button189.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button189.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button189.DrawOnGlass = false;
            this.Button189.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button189.ForeColor = System.Drawing.Color.White;
            this.Button189.Image = ((System.Drawing.Image)(resources.GetObject("Button189.Image")));
            this.Button189.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button189.Location = new System.Drawing.Point(715, 3);
            this.Button189.Name = "Button189";
            this.Button189.Size = new System.Drawing.Size(36, 24);
            this.Button189.TabIndex = 113;
            this.Button189.Tag = "3";
            this.Button189.UseVisualStyleBackColor = false;
            // 
            // TextBox60
            // 
            this.TextBox60.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox60.DrawOnGlass = false;
            this.TextBox60.ForeColor = System.Drawing.Color.White;
            this.TextBox60.Location = new System.Drawing.Point(203, 3);
            this.TextBox60.MaxLength = 32767;
            this.TextBox60.Multiline = false;
            this.TextBox60.Name = "TextBox60";
            this.TextBox60.ReadOnly = false;
            this.TextBox60.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox60.SelectedText = "";
            this.TextBox60.SelectionLength = 0;
            this.TextBox60.SelectionStart = 0;
            this.TextBox60.Size = new System.Drawing.Size(432, 24);
            this.TextBox60.TabIndex = 1;
            this.TextBox60.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox60.UseSystemPasswordChar = false;
            this.TextBox60.WordWrap = true;
            // 
            // Label61
            // 
            this.Label61.BackColor = System.Drawing.Color.Transparent;
            this.Label61.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label61.Location = new System.Drawing.Point(33, 4);
            this.Label61.Name = "Label61";
            this.Label61.Size = new System.Drawing.Size(164, 22);
            this.Label61.TabIndex = 112;
            this.Label61.Text = "Activating document:";
            this.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox60
            // 
            this.PictureBox60.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox60.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox60.Image")));
            this.PictureBox60.Location = new System.Drawing.Point(3, 3);
            this.PictureBox60.Name = "PictureBox60";
            this.PictureBox60.Size = new System.Drawing.Size(24, 24);
            this.PictureBox60.TabIndex = 1;
            this.PictureBox60.TabStop = false;
            // 
            // GroupBox62
            // 
            this.GroupBox62.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox62.Controls.Add(this.Button190);
            this.GroupBox62.Controls.Add(this.Button191);
            this.GroupBox62.Controls.Add(this.Button192);
            this.GroupBox62.Controls.Add(this.TextBox61);
            this.GroupBox62.Controls.Add(this.Label62);
            this.GroupBox62.Controls.Add(this.PictureBox61);
            this.GroupBox62.Location = new System.Drawing.Point(4, 37);
            this.GroupBox62.Name = "GroupBox62";
            this.GroupBox62.Size = new System.Drawing.Size(754, 30);
            this.GroupBox62.TabIndex = 119;
            // 
            // Button190
            // 
            this.Button190.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button190.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button190.DrawOnGlass = false;
            this.Button190.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button190.ForeColor = System.Drawing.Color.White;
            this.Button190.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button190.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button190.Location = new System.Drawing.Point(639, 3);
            this.Button190.Name = "Button190";
            this.Button190.Size = new System.Drawing.Size(36, 24);
            this.Button190.TabIndex = 115;
            this.Button190.Tag = "2";
            this.Button190.UseVisualStyleBackColor = false;
            // 
            // Button191
            // 
            this.Button191.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button191.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button191.DrawOnGlass = false;
            this.Button191.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button191.ForeColor = System.Drawing.Color.White;
            this.Button191.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button191.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button191.Location = new System.Drawing.Point(677, 3);
            this.Button191.Name = "Button191";
            this.Button191.Size = new System.Drawing.Size(36, 24);
            this.Button191.TabIndex = 114;
            this.Button191.Tag = "1";
            this.Button191.UseVisualStyleBackColor = false;
            // 
            // Button192
            // 
            this.Button192.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button192.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button192.DrawOnGlass = false;
            this.Button192.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button192.ForeColor = System.Drawing.Color.White;
            this.Button192.Image = ((System.Drawing.Image)(resources.GetObject("Button192.Image")));
            this.Button192.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button192.Location = new System.Drawing.Point(715, 3);
            this.Button192.Name = "Button192";
            this.Button192.Size = new System.Drawing.Size(36, 24);
            this.Button192.TabIndex = 113;
            this.Button192.Tag = "3";
            this.Button192.UseVisualStyleBackColor = false;
            // 
            // TextBox61
            // 
            this.TextBox61.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox61.DrawOnGlass = false;
            this.TextBox61.ForeColor = System.Drawing.Color.White;
            this.TextBox61.Location = new System.Drawing.Point(203, 3);
            this.TextBox61.MaxLength = 32767;
            this.TextBox61.Multiline = false;
            this.TextBox61.Name = "TextBox61";
            this.TextBox61.ReadOnly = false;
            this.TextBox61.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox61.SelectedText = "";
            this.TextBox61.SelectionLength = 0;
            this.TextBox61.SelectionStart = 0;
            this.TextBox61.Size = new System.Drawing.Size(432, 24);
            this.TextBox61.TabIndex = 1;
            this.TextBox61.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox61.UseSystemPasswordChar = false;
            this.TextBox61.WordWrap = true;
            // 
            // Label62
            // 
            this.Label62.BackColor = System.Drawing.Color.Transparent;
            this.Label62.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label62.Location = new System.Drawing.Point(33, 4);
            this.Label62.Name = "Label62";
            this.Label62.Size = new System.Drawing.Size(164, 22);
            this.Label62.TabIndex = 112;
            this.Label62.Text = "Empty Recycle Bin:";
            this.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox61
            // 
            this.PictureBox61.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox61.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox61.Image")));
            this.PictureBox61.Location = new System.Drawing.Point(3, 3);
            this.PictureBox61.Name = "PictureBox61";
            this.PictureBox61.Size = new System.Drawing.Size(24, 24);
            this.PictureBox61.TabIndex = 1;
            this.PictureBox61.TabStop = false;
            // 
            // GroupBox63
            // 
            this.GroupBox63.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox63.Controls.Add(this.Button193);
            this.GroupBox63.Controls.Add(this.Button194);
            this.GroupBox63.Controls.Add(this.Button195);
            this.GroupBox63.Controls.Add(this.TextBox62);
            this.GroupBox63.Controls.Add(this.Label63);
            this.GroupBox63.Controls.Add(this.PictureBox62);
            this.GroupBox63.Location = new System.Drawing.Point(4, 3);
            this.GroupBox63.Name = "GroupBox63";
            this.GroupBox63.Size = new System.Drawing.Size(754, 30);
            this.GroupBox63.TabIndex = 118;
            // 
            // Button193
            // 
            this.Button193.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button193.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button193.DrawOnGlass = false;
            this.Button193.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button193.ForeColor = System.Drawing.Color.White;
            this.Button193.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button193.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button193.Location = new System.Drawing.Point(639, 3);
            this.Button193.Name = "Button193";
            this.Button193.Size = new System.Drawing.Size(36, 24);
            this.Button193.TabIndex = 115;
            this.Button193.Tag = "2";
            this.Button193.UseVisualStyleBackColor = false;
            // 
            // Button194
            // 
            this.Button194.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button194.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button194.DrawOnGlass = false;
            this.Button194.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button194.ForeColor = System.Drawing.Color.White;
            this.Button194.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button194.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button194.Location = new System.Drawing.Point(677, 3);
            this.Button194.Name = "Button194";
            this.Button194.Size = new System.Drawing.Size(36, 24);
            this.Button194.TabIndex = 114;
            this.Button194.Tag = "1";
            this.Button194.UseVisualStyleBackColor = false;
            // 
            // Button195
            // 
            this.Button195.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button195.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button195.DrawOnGlass = false;
            this.Button195.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button195.ForeColor = System.Drawing.Color.White;
            this.Button195.Image = ((System.Drawing.Image)(resources.GetObject("Button195.Image")));
            this.Button195.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button195.Location = new System.Drawing.Point(715, 3);
            this.Button195.Name = "Button195";
            this.Button195.Size = new System.Drawing.Size(36, 24);
            this.Button195.TabIndex = 113;
            this.Button195.Tag = "3";
            this.Button195.UseVisualStyleBackColor = false;
            // 
            // TextBox62
            // 
            this.TextBox62.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox62.DrawOnGlass = false;
            this.TextBox62.ForeColor = System.Drawing.Color.White;
            this.TextBox62.Location = new System.Drawing.Point(203, 3);
            this.TextBox62.MaxLength = 32767;
            this.TextBox62.Multiline = false;
            this.TextBox62.Name = "TextBox62";
            this.TextBox62.ReadOnly = false;
            this.TextBox62.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox62.SelectedText = "";
            this.TextBox62.SelectionLength = 0;
            this.TextBox62.SelectionStart = 0;
            this.TextBox62.Size = new System.Drawing.Size(432, 24);
            this.TextBox62.TabIndex = 1;
            this.TextBox62.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox62.UseSystemPasswordChar = false;
            this.TextBox62.WordWrap = true;
            // 
            // Label63
            // 
            this.Label63.BackColor = System.Drawing.Color.Transparent;
            this.Label63.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label63.Location = new System.Drawing.Point(33, 4);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(164, 22);
            this.Label63.TabIndex = 112;
            this.Label63.Text = "Folders navigation:";
            this.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox62
            // 
            this.PictureBox62.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox62.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox62.Image")));
            this.PictureBox62.Location = new System.Drawing.Point(3, 3);
            this.PictureBox62.Name = "PictureBox62";
            this.PictureBox62.Size = new System.Drawing.Size(24, 24);
            this.PictureBox62.TabIndex = 1;
            this.PictureBox62.TabStop = false;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage4.Controls.Add(this.GroupBox66);
            this.TabPage4.Controls.Add(this.GroupBox56);
            this.TabPage4.Controls.Add(this.GroupBox25);
            this.TabPage4.Controls.Add(this.GroupBox19);
            this.TabPage4.Controls.Add(this.GroupBox20);
            this.TabPage4.Controls.Add(this.GroupBox21);
            this.TabPage4.Controls.Add(this.GroupBox22);
            this.TabPage4.Controls.Add(this.GroupBox23);
            this.TabPage4.Controls.Add(this.GroupBox24);
            this.TabPage4.Location = new System.Drawing.Point(144, 4);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(761, 368);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Notifications";
            // 
            // GroupBox66
            // 
            this.GroupBox66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox66.Controls.Add(this.Button202);
            this.GroupBox66.Controls.Add(this.Button203);
            this.GroupBox66.Controls.Add(this.Button204);
            this.GroupBox66.Controls.Add(this.TextBox65);
            this.GroupBox66.Controls.Add(this.Label66);
            this.GroupBox66.Controls.Add(this.PictureBox65);
            this.GroupBox66.Location = new System.Drawing.Point(4, 173);
            this.GroupBox66.Name = "GroupBox66";
            this.GroupBox66.Size = new System.Drawing.Size(754, 30);
            this.GroupBox66.TabIndex = 27;
            // 
            // Button202
            // 
            this.Button202.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button202.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button202.DrawOnGlass = false;
            this.Button202.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button202.ForeColor = System.Drawing.Color.White;
            this.Button202.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button202.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button202.Location = new System.Drawing.Point(639, 3);
            this.Button202.Name = "Button202";
            this.Button202.Size = new System.Drawing.Size(36, 24);
            this.Button202.TabIndex = 115;
            this.Button202.Tag = "2";
            this.Button202.UseVisualStyleBackColor = false;
            // 
            // Button203
            // 
            this.Button203.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button203.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button203.DrawOnGlass = false;
            this.Button203.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button203.ForeColor = System.Drawing.Color.White;
            this.Button203.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button203.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button203.Location = new System.Drawing.Point(677, 3);
            this.Button203.Name = "Button203";
            this.Button203.Size = new System.Drawing.Size(36, 24);
            this.Button203.TabIndex = 114;
            this.Button203.Tag = "1";
            this.Button203.UseVisualStyleBackColor = false;
            // 
            // Button204
            // 
            this.Button204.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button204.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button204.DrawOnGlass = false;
            this.Button204.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button204.ForeColor = System.Drawing.Color.White;
            this.Button204.Image = ((System.Drawing.Image)(resources.GetObject("Button204.Image")));
            this.Button204.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button204.Location = new System.Drawing.Point(715, 3);
            this.Button204.Name = "Button204";
            this.Button204.Size = new System.Drawing.Size(36, 24);
            this.Button204.TabIndex = 113;
            this.Button204.Tag = "3";
            this.Button204.UseVisualStyleBackColor = false;
            // 
            // TextBox65
            // 
            this.TextBox65.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox65.DrawOnGlass = false;
            this.TextBox65.ForeColor = System.Drawing.Color.White;
            this.TextBox65.Location = new System.Drawing.Point(140, 3);
            this.TextBox65.MaxLength = 32767;
            this.TextBox65.Multiline = false;
            this.TextBox65.Name = "TextBox65";
            this.TextBox65.ReadOnly = false;
            this.TextBox65.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox65.SelectedText = "";
            this.TextBox65.SelectionLength = 0;
            this.TextBox65.SelectionStart = 0;
            this.TextBox65.Size = new System.Drawing.Size(495, 24);
            this.TextBox65.TabIndex = 1;
            this.TextBox65.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox65.UseSystemPasswordChar = false;
            this.TextBox65.WordWrap = true;
            // 
            // Label66
            // 
            this.Label66.BackColor = System.Drawing.Color.Transparent;
            this.Label66.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label66.Location = new System.Drawing.Point(33, 4);
            this.Label66.Name = "Label66";
            this.Label66.Size = new System.Drawing.Size(101, 22);
            this.Label66.TabIndex = 112;
            this.Label66.Text = "Mail beep:";
            this.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox65
            // 
            this.PictureBox65.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox65.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox65.Image")));
            this.PictureBox65.Location = new System.Drawing.Point(3, 3);
            this.PictureBox65.Name = "PictureBox65";
            this.PictureBox65.Size = new System.Drawing.Size(24, 24);
            this.PictureBox65.TabIndex = 1;
            this.PictureBox65.TabStop = false;
            // 
            // GroupBox56
            // 
            this.GroupBox56.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox56.Controls.Add(this.Button172);
            this.GroupBox56.Controls.Add(this.Button173);
            this.GroupBox56.Controls.Add(this.Button174);
            this.GroupBox56.Controls.Add(this.TextBox55);
            this.GroupBox56.Controls.Add(this.Label56);
            this.GroupBox56.Controls.Add(this.PictureBox55);
            this.GroupBox56.Location = new System.Drawing.Point(4, 3);
            this.GroupBox56.Name = "GroupBox56";
            this.GroupBox56.Size = new System.Drawing.Size(754, 30);
            this.GroupBox56.TabIndex = 26;
            // 
            // Button172
            // 
            this.Button172.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button172.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button172.DrawOnGlass = false;
            this.Button172.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button172.ForeColor = System.Drawing.Color.White;
            this.Button172.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button172.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button172.Location = new System.Drawing.Point(639, 3);
            this.Button172.Name = "Button172";
            this.Button172.Size = new System.Drawing.Size(36, 24);
            this.Button172.TabIndex = 115;
            this.Button172.Tag = "2";
            this.Button172.UseVisualStyleBackColor = false;
            // 
            // Button173
            // 
            this.Button173.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button173.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button173.DrawOnGlass = false;
            this.Button173.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button173.ForeColor = System.Drawing.Color.White;
            this.Button173.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button173.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button173.Location = new System.Drawing.Point(677, 3);
            this.Button173.Name = "Button173";
            this.Button173.Size = new System.Drawing.Size(36, 24);
            this.Button173.TabIndex = 114;
            this.Button173.Tag = "1";
            this.Button173.UseVisualStyleBackColor = false;
            // 
            // Button174
            // 
            this.Button174.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button174.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button174.DrawOnGlass = false;
            this.Button174.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button174.ForeColor = System.Drawing.Color.White;
            this.Button174.Image = ((System.Drawing.Image)(resources.GetObject("Button174.Image")));
            this.Button174.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button174.Location = new System.Drawing.Point(715, 3);
            this.Button174.Name = "Button174";
            this.Button174.Size = new System.Drawing.Size(36, 24);
            this.Button174.TabIndex = 113;
            this.Button174.Tag = "3";
            this.Button174.UseVisualStyleBackColor = false;
            // 
            // TextBox55
            // 
            this.TextBox55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox55.DrawOnGlass = false;
            this.TextBox55.ForeColor = System.Drawing.Color.White;
            this.TextBox55.Location = new System.Drawing.Point(140, 3);
            this.TextBox55.MaxLength = 32767;
            this.TextBox55.Multiline = false;
            this.TextBox55.Name = "TextBox55";
            this.TextBox55.ReadOnly = false;
            this.TextBox55.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox55.SelectedText = "";
            this.TextBox55.SelectionLength = 0;
            this.TextBox55.SelectionStart = 0;
            this.TextBox55.Size = new System.Drawing.Size(495, 24);
            this.TextBox55.TabIndex = 1;
            this.TextBox55.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox55.UseSystemPasswordChar = false;
            this.TextBox55.WordWrap = true;
            // 
            // Label56
            // 
            this.Label56.BackColor = System.Drawing.Color.Transparent;
            this.Label56.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label56.Location = new System.Drawing.Point(33, 4);
            this.Label56.Name = "Label56";
            this.Label56.Size = new System.Drawing.Size(101, 22);
            this.Label56.TabIndex = 112;
            this.Label56.Text = "Default beep:";
            this.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox55
            // 
            this.PictureBox55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox55.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox55.Image")));
            this.PictureBox55.Location = new System.Drawing.Point(3, 3);
            this.PictureBox55.Name = "PictureBox55";
            this.PictureBox55.Size = new System.Drawing.Size(24, 24);
            this.PictureBox55.TabIndex = 1;
            this.PictureBox55.TabStop = false;
            // 
            // GroupBox25
            // 
            this.GroupBox25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox25.Controls.Add(this.Button79);
            this.GroupBox25.Controls.Add(this.Button80);
            this.GroupBox25.Controls.Add(this.Button81);
            this.GroupBox25.Controls.Add(this.TextBox24);
            this.GroupBox25.Controls.Add(this.Label25);
            this.GroupBox25.Controls.Add(this.PictureBox24);
            this.GroupBox25.Location = new System.Drawing.Point(4, 275);
            this.GroupBox25.Name = "GroupBox25";
            this.GroupBox25.Size = new System.Drawing.Size(754, 30);
            this.GroupBox25.TabIndex = 25;
            // 
            // Button79
            // 
            this.Button79.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button79.DrawOnGlass = false;
            this.Button79.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button79.ForeColor = System.Drawing.Color.White;
            this.Button79.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button79.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button79.Location = new System.Drawing.Point(639, 3);
            this.Button79.Name = "Button79";
            this.Button79.Size = new System.Drawing.Size(36, 24);
            this.Button79.TabIndex = 115;
            this.Button79.Tag = "2";
            this.Button79.UseVisualStyleBackColor = false;
            // 
            // Button80
            // 
            this.Button80.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button80.DrawOnGlass = false;
            this.Button80.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button80.ForeColor = System.Drawing.Color.White;
            this.Button80.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button80.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button80.Location = new System.Drawing.Point(677, 3);
            this.Button80.Name = "Button80";
            this.Button80.Size = new System.Drawing.Size(36, 24);
            this.Button80.TabIndex = 114;
            this.Button80.Tag = "1";
            this.Button80.UseVisualStyleBackColor = false;
            // 
            // Button81
            // 
            this.Button81.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button81.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button81.DrawOnGlass = false;
            this.Button81.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button81.ForeColor = System.Drawing.Color.White;
            this.Button81.Image = ((System.Drawing.Image)(resources.GetObject("Button81.Image")));
            this.Button81.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button81.Location = new System.Drawing.Point(715, 3);
            this.Button81.Name = "Button81";
            this.Button81.Size = new System.Drawing.Size(36, 24);
            this.Button81.TabIndex = 113;
            this.Button81.Tag = "3";
            this.Button81.UseVisualStyleBackColor = false;
            // 
            // TextBox24
            // 
            this.TextBox24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox24.DrawOnGlass = false;
            this.TextBox24.ForeColor = System.Drawing.Color.White;
            this.TextBox24.Location = new System.Drawing.Point(140, 3);
            this.TextBox24.MaxLength = 32767;
            this.TextBox24.Multiline = false;
            this.TextBox24.Name = "TextBox24";
            this.TextBox24.ReadOnly = false;
            this.TextBox24.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox24.SelectedText = "";
            this.TextBox24.SelectionLength = 0;
            this.TextBox24.SelectionStart = 0;
            this.TextBox24.Size = new System.Drawing.Size(495, 24);
            this.TextBox24.TabIndex = 1;
            this.TextBox24.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox24.UseSystemPasswordChar = false;
            this.TextBox24.WordWrap = true;
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.Transparent;
            this.Label25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(33, 4);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(101, 22);
            this.Label25.TabIndex = 112;
            this.Label25.Text = "SMS:";
            this.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox24
            // 
            this.PictureBox24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox24.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox24.Image")));
            this.PictureBox24.Location = new System.Drawing.Point(3, 3);
            this.PictureBox24.Name = "PictureBox24";
            this.PictureBox24.Size = new System.Drawing.Size(24, 24);
            this.PictureBox24.TabIndex = 1;
            this.PictureBox24.TabStop = false;
            // 
            // GroupBox19
            // 
            this.GroupBox19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox19.Controls.Add(this.Button61);
            this.GroupBox19.Controls.Add(this.Button62);
            this.GroupBox19.Controls.Add(this.Button63);
            this.GroupBox19.Controls.Add(this.TextBox18);
            this.GroupBox19.Controls.Add(this.Label19);
            this.GroupBox19.Controls.Add(this.PictureBox18);
            this.GroupBox19.Location = new System.Drawing.Point(4, 241);
            this.GroupBox19.Name = "GroupBox19";
            this.GroupBox19.Size = new System.Drawing.Size(754, 30);
            this.GroupBox19.TabIndex = 24;
            // 
            // Button61
            // 
            this.Button61.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button61.DrawOnGlass = false;
            this.Button61.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button61.ForeColor = System.Drawing.Color.White;
            this.Button61.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button61.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button61.Location = new System.Drawing.Point(639, 3);
            this.Button61.Name = "Button61";
            this.Button61.Size = new System.Drawing.Size(36, 24);
            this.Button61.TabIndex = 115;
            this.Button61.Tag = "2";
            this.Button61.UseVisualStyleBackColor = false;
            // 
            // Button62
            // 
            this.Button62.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button62.DrawOnGlass = false;
            this.Button62.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button62.ForeColor = System.Drawing.Color.White;
            this.Button62.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button62.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button62.Location = new System.Drawing.Point(677, 3);
            this.Button62.Name = "Button62";
            this.Button62.Size = new System.Drawing.Size(36, 24);
            this.Button62.TabIndex = 114;
            this.Button62.Tag = "1";
            this.Button62.UseVisualStyleBackColor = false;
            // 
            // Button63
            // 
            this.Button63.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button63.DrawOnGlass = false;
            this.Button63.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button63.ForeColor = System.Drawing.Color.White;
            this.Button63.Image = ((System.Drawing.Image)(resources.GetObject("Button63.Image")));
            this.Button63.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button63.Location = new System.Drawing.Point(715, 3);
            this.Button63.Name = "Button63";
            this.Button63.Size = new System.Drawing.Size(36, 24);
            this.Button63.TabIndex = 113;
            this.Button63.Tag = "3";
            this.Button63.UseVisualStyleBackColor = false;
            // 
            // TextBox18
            // 
            this.TextBox18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox18.DrawOnGlass = false;
            this.TextBox18.ForeColor = System.Drawing.Color.White;
            this.TextBox18.Location = new System.Drawing.Point(140, 3);
            this.TextBox18.MaxLength = 32767;
            this.TextBox18.Multiline = false;
            this.TextBox18.Name = "TextBox18";
            this.TextBox18.ReadOnly = false;
            this.TextBox18.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox18.SelectedText = "";
            this.TextBox18.SelectionLength = 0;
            this.TextBox18.SelectionStart = 0;
            this.TextBox18.Size = new System.Drawing.Size(495, 24);
            this.TextBox18.TabIndex = 1;
            this.TextBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox18.UseSystemPasswordChar = false;
            this.TextBox18.WordWrap = true;
            // 
            // Label19
            // 
            this.Label19.BackColor = System.Drawing.Color.Transparent;
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(33, 4);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(101, 22);
            this.Label19.TabIndex = 112;
            this.Label19.Text = "Reminder:";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox18
            // 
            this.PictureBox18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox18.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox18.Image")));
            this.PictureBox18.Location = new System.Drawing.Point(3, 3);
            this.PictureBox18.Name = "PictureBox18";
            this.PictureBox18.Size = new System.Drawing.Size(24, 24);
            this.PictureBox18.TabIndex = 1;
            this.PictureBox18.TabStop = false;
            // 
            // GroupBox20
            // 
            this.GroupBox20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox20.Controls.Add(this.Button64);
            this.GroupBox20.Controls.Add(this.Button65);
            this.GroupBox20.Controls.Add(this.Button66);
            this.GroupBox20.Controls.Add(this.TextBox19);
            this.GroupBox20.Controls.Add(this.Label20);
            this.GroupBox20.Controls.Add(this.PictureBox19);
            this.GroupBox20.Location = new System.Drawing.Point(4, 207);
            this.GroupBox20.Name = "GroupBox20";
            this.GroupBox20.Size = new System.Drawing.Size(754, 30);
            this.GroupBox20.TabIndex = 23;
            // 
            // Button64
            // 
            this.Button64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button64.DrawOnGlass = false;
            this.Button64.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button64.ForeColor = System.Drawing.Color.White;
            this.Button64.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button64.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button64.Location = new System.Drawing.Point(639, 3);
            this.Button64.Name = "Button64";
            this.Button64.Size = new System.Drawing.Size(36, 24);
            this.Button64.TabIndex = 115;
            this.Button64.Tag = "2";
            this.Button64.UseVisualStyleBackColor = false;
            // 
            // Button65
            // 
            this.Button65.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button65.DrawOnGlass = false;
            this.Button65.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button65.ForeColor = System.Drawing.Color.White;
            this.Button65.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button65.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button65.Location = new System.Drawing.Point(677, 3);
            this.Button65.Name = "Button65";
            this.Button65.Size = new System.Drawing.Size(36, 24);
            this.Button65.TabIndex = 114;
            this.Button65.Tag = "1";
            this.Button65.UseVisualStyleBackColor = false;
            // 
            // Button66
            // 
            this.Button66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button66.DrawOnGlass = false;
            this.Button66.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button66.ForeColor = System.Drawing.Color.White;
            this.Button66.Image = ((System.Drawing.Image)(resources.GetObject("Button66.Image")));
            this.Button66.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button66.Location = new System.Drawing.Point(715, 3);
            this.Button66.Name = "Button66";
            this.Button66.Size = new System.Drawing.Size(36, 24);
            this.Button66.TabIndex = 113;
            this.Button66.Tag = "3";
            this.Button66.UseVisualStyleBackColor = false;
            // 
            // TextBox19
            // 
            this.TextBox19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox19.DrawOnGlass = false;
            this.TextBox19.ForeColor = System.Drawing.Color.White;
            this.TextBox19.Location = new System.Drawing.Point(140, 3);
            this.TextBox19.MaxLength = 32767;
            this.TextBox19.Multiline = false;
            this.TextBox19.Name = "TextBox19";
            this.TextBox19.ReadOnly = false;
            this.TextBox19.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox19.SelectedText = "";
            this.TextBox19.SelectionLength = 0;
            this.TextBox19.SelectionStart = 0;
            this.TextBox19.Size = new System.Drawing.Size(495, 24);
            this.TextBox19.TabIndex = 1;
            this.TextBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox19.UseSystemPasswordChar = false;
            this.TextBox19.WordWrap = true;
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(33, 4);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(101, 22);
            this.Label20.TabIndex = 112;
            this.Label20.Text = "Proximity:";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox19
            // 
            this.PictureBox19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox19.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox19.Image")));
            this.PictureBox19.Location = new System.Drawing.Point(3, 3);
            this.PictureBox19.Name = "PictureBox19";
            this.PictureBox19.Size = new System.Drawing.Size(24, 24);
            this.PictureBox19.TabIndex = 1;
            this.PictureBox19.TabStop = false;
            // 
            // GroupBox21
            // 
            this.GroupBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox21.Controls.Add(this.Button67);
            this.GroupBox21.Controls.Add(this.Button68);
            this.GroupBox21.Controls.Add(this.Button69);
            this.GroupBox21.Controls.Add(this.TextBox20);
            this.GroupBox21.Controls.Add(this.Label21);
            this.GroupBox21.Controls.Add(this.PictureBox20);
            this.GroupBox21.Location = new System.Drawing.Point(4, 139);
            this.GroupBox21.Name = "GroupBox21";
            this.GroupBox21.Size = new System.Drawing.Size(754, 30);
            this.GroupBox21.TabIndex = 22;
            // 
            // Button67
            // 
            this.Button67.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button67.DrawOnGlass = false;
            this.Button67.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button67.ForeColor = System.Drawing.Color.White;
            this.Button67.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button67.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button67.Location = new System.Drawing.Point(639, 3);
            this.Button67.Name = "Button67";
            this.Button67.Size = new System.Drawing.Size(36, 24);
            this.Button67.TabIndex = 115;
            this.Button67.Tag = "2";
            this.Button67.UseVisualStyleBackColor = false;
            // 
            // Button68
            // 
            this.Button68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button68.DrawOnGlass = false;
            this.Button68.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button68.ForeColor = System.Drawing.Color.White;
            this.Button68.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button68.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button68.Location = new System.Drawing.Point(677, 3);
            this.Button68.Name = "Button68";
            this.Button68.Size = new System.Drawing.Size(36, 24);
            this.Button68.TabIndex = 114;
            this.Button68.Tag = "1";
            this.Button68.UseVisualStyleBackColor = false;
            // 
            // Button69
            // 
            this.Button69.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button69.DrawOnGlass = false;
            this.Button69.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button69.ForeColor = System.Drawing.Color.White;
            this.Button69.Image = ((System.Drawing.Image)(resources.GetObject("Button69.Image")));
            this.Button69.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button69.Location = new System.Drawing.Point(715, 3);
            this.Button69.Name = "Button69";
            this.Button69.Size = new System.Drawing.Size(36, 24);
            this.Button69.TabIndex = 113;
            this.Button69.Tag = "3";
            this.Button69.UseVisualStyleBackColor = false;
            // 
            // TextBox20
            // 
            this.TextBox20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox20.DrawOnGlass = false;
            this.TextBox20.ForeColor = System.Drawing.Color.White;
            this.TextBox20.Location = new System.Drawing.Point(140, 3);
            this.TextBox20.MaxLength = 32767;
            this.TextBox20.Multiline = false;
            this.TextBox20.Name = "TextBox20";
            this.TextBox20.ReadOnly = false;
            this.TextBox20.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox20.SelectedText = "";
            this.TextBox20.SelectionLength = 0;
            this.TextBox20.SelectionStart = 0;
            this.TextBox20.Size = new System.Drawing.Size(495, 24);
            this.TextBox20.TabIndex = 1;
            this.TextBox20.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox20.UseSystemPasswordChar = false;
            this.TextBox20.WordWrap = true;
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.Transparent;
            this.Label21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.Location = new System.Drawing.Point(33, 4);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(101, 22);
            this.Label21.TabIndex = 112;
            this.Label21.Text = "Mail notification:";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox20
            // 
            this.PictureBox20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox20.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox20.Image")));
            this.PictureBox20.Location = new System.Drawing.Point(3, 3);
            this.PictureBox20.Name = "PictureBox20";
            this.PictureBox20.Size = new System.Drawing.Size(24, 24);
            this.PictureBox20.TabIndex = 1;
            this.PictureBox20.TabStop = false;
            // 
            // GroupBox22
            // 
            this.GroupBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox22.Controls.Add(this.Button70);
            this.GroupBox22.Controls.Add(this.Button71);
            this.GroupBox22.Controls.Add(this.Button72);
            this.GroupBox22.Controls.Add(this.TextBox21);
            this.GroupBox22.Controls.Add(this.Label22);
            this.GroupBox22.Controls.Add(this.PictureBox21);
            this.GroupBox22.Location = new System.Drawing.Point(4, 105);
            this.GroupBox22.Name = "GroupBox22";
            this.GroupBox22.Size = new System.Drawing.Size(754, 30);
            this.GroupBox22.TabIndex = 21;
            // 
            // Button70
            // 
            this.Button70.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button70.DrawOnGlass = false;
            this.Button70.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button70.ForeColor = System.Drawing.Color.White;
            this.Button70.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button70.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button70.Location = new System.Drawing.Point(639, 3);
            this.Button70.Name = "Button70";
            this.Button70.Size = new System.Drawing.Size(36, 24);
            this.Button70.TabIndex = 115;
            this.Button70.Tag = "2";
            this.Button70.UseVisualStyleBackColor = false;
            // 
            // Button71
            // 
            this.Button71.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button71.DrawOnGlass = false;
            this.Button71.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button71.ForeColor = System.Drawing.Color.White;
            this.Button71.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button71.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button71.Location = new System.Drawing.Point(677, 3);
            this.Button71.Name = "Button71";
            this.Button71.Size = new System.Drawing.Size(36, 24);
            this.Button71.TabIndex = 114;
            this.Button71.Tag = "1";
            this.Button71.UseVisualStyleBackColor = false;
            // 
            // Button72
            // 
            this.Button72.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button72.DrawOnGlass = false;
            this.Button72.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button72.ForeColor = System.Drawing.Color.White;
            this.Button72.Image = ((System.Drawing.Image)(resources.GetObject("Button72.Image")));
            this.Button72.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button72.Location = new System.Drawing.Point(715, 3);
            this.Button72.Name = "Button72";
            this.Button72.Size = new System.Drawing.Size(36, 24);
            this.Button72.TabIndex = 113;
            this.Button72.Tag = "3";
            this.Button72.UseVisualStyleBackColor = false;
            // 
            // TextBox21
            // 
            this.TextBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox21.DrawOnGlass = false;
            this.TextBox21.ForeColor = System.Drawing.Color.White;
            this.TextBox21.Location = new System.Drawing.Point(140, 3);
            this.TextBox21.MaxLength = 32767;
            this.TextBox21.Multiline = false;
            this.TextBox21.Name = "TextBox21";
            this.TextBox21.ReadOnly = false;
            this.TextBox21.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox21.SelectedText = "";
            this.TextBox21.SelectionLength = 0;
            this.TextBox21.SelectionStart = 0;
            this.TextBox21.Size = new System.Drawing.Size(495, 24);
            this.TextBox21.TabIndex = 1;
            this.TextBox21.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox21.UseSystemPasswordChar = false;
            this.TextBox21.WordWrap = true;
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.Color.Transparent;
            this.Label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label22.Location = new System.Drawing.Point(33, 4);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(101, 22);
            this.Label22.TabIndex = 112;
            this.Label22.Text = "Message nudge:";
            this.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox21
            // 
            this.PictureBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox21.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox21.Image")));
            this.PictureBox21.Location = new System.Drawing.Point(3, 3);
            this.PictureBox21.Name = "PictureBox21";
            this.PictureBox21.Size = new System.Drawing.Size(24, 24);
            this.PictureBox21.TabIndex = 1;
            this.PictureBox21.TabStop = false;
            // 
            // GroupBox23
            // 
            this.GroupBox23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox23.Controls.Add(this.Button73);
            this.GroupBox23.Controls.Add(this.Button74);
            this.GroupBox23.Controls.Add(this.Button75);
            this.GroupBox23.Controls.Add(this.TextBox22);
            this.GroupBox23.Controls.Add(this.Label23);
            this.GroupBox23.Controls.Add(this.PictureBox22);
            this.GroupBox23.Location = new System.Drawing.Point(4, 71);
            this.GroupBox23.Name = "GroupBox23";
            this.GroupBox23.Size = new System.Drawing.Size(754, 30);
            this.GroupBox23.TabIndex = 20;
            // 
            // Button73
            // 
            this.Button73.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button73.DrawOnGlass = false;
            this.Button73.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button73.ForeColor = System.Drawing.Color.White;
            this.Button73.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button73.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button73.Location = new System.Drawing.Point(639, 3);
            this.Button73.Name = "Button73";
            this.Button73.Size = new System.Drawing.Size(36, 24);
            this.Button73.TabIndex = 115;
            this.Button73.Tag = "2";
            this.Button73.UseVisualStyleBackColor = false;
            // 
            // Button74
            // 
            this.Button74.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button74.DrawOnGlass = false;
            this.Button74.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button74.ForeColor = System.Drawing.Color.White;
            this.Button74.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button74.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button74.Location = new System.Drawing.Point(677, 3);
            this.Button74.Name = "Button74";
            this.Button74.Size = new System.Drawing.Size(36, 24);
            this.Button74.TabIndex = 114;
            this.Button74.Tag = "1";
            this.Button74.UseVisualStyleBackColor = false;
            // 
            // Button75
            // 
            this.Button75.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button75.DrawOnGlass = false;
            this.Button75.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button75.ForeColor = System.Drawing.Color.White;
            this.Button75.Image = ((System.Drawing.Image)(resources.GetObject("Button75.Image")));
            this.Button75.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button75.Location = new System.Drawing.Point(715, 3);
            this.Button75.Name = "Button75";
            this.Button75.Size = new System.Drawing.Size(36, 24);
            this.Button75.TabIndex = 113;
            this.Button75.Tag = "3";
            this.Button75.UseVisualStyleBackColor = false;
            // 
            // TextBox22
            // 
            this.TextBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox22.DrawOnGlass = false;
            this.TextBox22.ForeColor = System.Drawing.Color.White;
            this.TextBox22.Location = new System.Drawing.Point(140, 3);
            this.TextBox22.MaxLength = 32767;
            this.TextBox22.Multiline = false;
            this.TextBox22.Name = "TextBox22";
            this.TextBox22.ReadOnly = false;
            this.TextBox22.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox22.SelectedText = "";
            this.TextBox22.SelectionLength = 0;
            this.TextBox22.SelectionStart = 0;
            this.TextBox22.Size = new System.Drawing.Size(495, 24);
            this.TextBox22.TabIndex = 1;
            this.TextBox22.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox22.UseSystemPasswordChar = false;
            this.TextBox22.WordWrap = true;
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.Transparent;
            this.Label23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(33, 4);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(101, 22);
            this.Label23.TabIndex = 112;
            this.Label23.Text = "Internet message:";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox22
            // 
            this.PictureBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox22.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox22.Image")));
            this.PictureBox22.Location = new System.Drawing.Point(3, 3);
            this.PictureBox22.Name = "PictureBox22";
            this.PictureBox22.Size = new System.Drawing.Size(24, 24);
            this.PictureBox22.TabIndex = 1;
            this.PictureBox22.TabStop = false;
            // 
            // GroupBox24
            // 
            this.GroupBox24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox24.Controls.Add(this.Button76);
            this.GroupBox24.Controls.Add(this.Button77);
            this.GroupBox24.Controls.Add(this.Button78);
            this.GroupBox24.Controls.Add(this.TextBox23);
            this.GroupBox24.Controls.Add(this.Label24);
            this.GroupBox24.Controls.Add(this.PictureBox23);
            this.GroupBox24.Location = new System.Drawing.Point(4, 37);
            this.GroupBox24.Name = "GroupBox24";
            this.GroupBox24.Size = new System.Drawing.Size(754, 30);
            this.GroupBox24.TabIndex = 19;
            // 
            // Button76
            // 
            this.Button76.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button76.DrawOnGlass = false;
            this.Button76.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button76.ForeColor = System.Drawing.Color.White;
            this.Button76.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button76.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button76.Location = new System.Drawing.Point(639, 3);
            this.Button76.Name = "Button76";
            this.Button76.Size = new System.Drawing.Size(36, 24);
            this.Button76.TabIndex = 115;
            this.Button76.Tag = "2";
            this.Button76.UseVisualStyleBackColor = false;
            // 
            // Button77
            // 
            this.Button77.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button77.DrawOnGlass = false;
            this.Button77.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button77.ForeColor = System.Drawing.Color.White;
            this.Button77.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button77.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button77.Location = new System.Drawing.Point(677, 3);
            this.Button77.Name = "Button77";
            this.Button77.Size = new System.Drawing.Size(36, 24);
            this.Button77.TabIndex = 114;
            this.Button77.Tag = "1";
            this.Button77.UseVisualStyleBackColor = false;
            // 
            // Button78
            // 
            this.Button78.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button78.DrawOnGlass = false;
            this.Button78.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button78.ForeColor = System.Drawing.Color.White;
            this.Button78.Image = ((System.Drawing.Image)(resources.GetObject("Button78.Image")));
            this.Button78.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button78.Location = new System.Drawing.Point(715, 3);
            this.Button78.Name = "Button78";
            this.Button78.Size = new System.Drawing.Size(36, 24);
            this.Button78.TabIndex = 113;
            this.Button78.Tag = "3";
            this.Button78.UseVisualStyleBackColor = false;
            // 
            // TextBox23
            // 
            this.TextBox23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox23.DrawOnGlass = false;
            this.TextBox23.ForeColor = System.Drawing.Color.White;
            this.TextBox23.Location = new System.Drawing.Point(140, 3);
            this.TextBox23.MaxLength = 32767;
            this.TextBox23.Multiline = false;
            this.TextBox23.Name = "TextBox23";
            this.TextBox23.ReadOnly = false;
            this.TextBox23.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox23.SelectedText = "";
            this.TextBox23.SelectionLength = 0;
            this.TextBox23.SelectionStart = 0;
            this.TextBox23.Size = new System.Drawing.Size(495, 24);
            this.TextBox23.TabIndex = 1;
            this.TextBox23.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox23.UseSystemPasswordChar = false;
            this.TextBox23.WordWrap = true;
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.Transparent;
            this.Label24.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label24.Location = new System.Drawing.Point(33, 4);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(101, 22);
            this.Label24.TabIndex = 112;
            this.Label24.Text = "Notification:";
            this.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox23
            // 
            this.PictureBox23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox23.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox23.Image")));
            this.PictureBox23.Location = new System.Drawing.Point(3, 3);
            this.PictureBox23.Name = "PictureBox23";
            this.PictureBox23.Size = new System.Drawing.Size(24, 24);
            this.PictureBox23.TabIndex = 1;
            this.PictureBox23.TabStop = false;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage5.Controls.Add(this.GroupBox33);
            this.TabPage5.Controls.Add(this.GroupBox34);
            this.TabPage5.Controls.Add(this.GroupBox35);
            this.TabPage5.Controls.Add(this.GroupBox26);
            this.TabPage5.Controls.Add(this.GroupBox27);
            this.TabPage5.Controls.Add(this.GroupBox28);
            this.TabPage5.Controls.Add(this.GroupBox29);
            this.TabPage5.Controls.Add(this.GroupBox30);
            this.TabPage5.Controls.Add(this.GroupBox31);
            this.TabPage5.Controls.Add(this.GroupBox32);
            this.TabPage5.Location = new System.Drawing.Point(144, 4);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(761, 368);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Alarms";
            // 
            // GroupBox33
            // 
            this.GroupBox33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox33.Controls.Add(this.Button103);
            this.GroupBox33.Controls.Add(this.Button104);
            this.GroupBox33.Controls.Add(this.Button105);
            this.GroupBox33.Controls.Add(this.TextBox32);
            this.GroupBox33.Controls.Add(this.Label33);
            this.GroupBox33.Controls.Add(this.PictureBox32);
            this.GroupBox33.Location = new System.Drawing.Point(4, 309);
            this.GroupBox33.Name = "GroupBox33";
            this.GroupBox33.Size = new System.Drawing.Size(754, 30);
            this.GroupBox33.TabIndex = 35;
            // 
            // Button103
            // 
            this.Button103.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button103.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button103.DrawOnGlass = false;
            this.Button103.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button103.ForeColor = System.Drawing.Color.White;
            this.Button103.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button103.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button103.Location = new System.Drawing.Point(639, 3);
            this.Button103.Name = "Button103";
            this.Button103.Size = new System.Drawing.Size(36, 24);
            this.Button103.TabIndex = 115;
            this.Button103.Tag = "2";
            this.Button103.UseVisualStyleBackColor = false;
            // 
            // Button104
            // 
            this.Button104.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button104.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button104.DrawOnGlass = false;
            this.Button104.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button104.ForeColor = System.Drawing.Color.White;
            this.Button104.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button104.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button104.Location = new System.Drawing.Point(677, 3);
            this.Button104.Name = "Button104";
            this.Button104.Size = new System.Drawing.Size(36, 24);
            this.Button104.TabIndex = 114;
            this.Button104.Tag = "1";
            this.Button104.UseVisualStyleBackColor = false;
            // 
            // Button105
            // 
            this.Button105.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button105.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button105.DrawOnGlass = false;
            this.Button105.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button105.ForeColor = System.Drawing.Color.White;
            this.Button105.Image = ((System.Drawing.Image)(resources.GetObject("Button105.Image")));
            this.Button105.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button105.Location = new System.Drawing.Point(715, 3);
            this.Button105.Name = "Button105";
            this.Button105.Size = new System.Drawing.Size(36, 24);
            this.Button105.TabIndex = 113;
            this.Button105.Tag = "3";
            this.Button105.UseVisualStyleBackColor = false;
            // 
            // TextBox32
            // 
            this.TextBox32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox32.DrawOnGlass = false;
            this.TextBox32.ForeColor = System.Drawing.Color.White;
            this.TextBox32.Location = new System.Drawing.Point(140, 3);
            this.TextBox32.MaxLength = 32767;
            this.TextBox32.Multiline = false;
            this.TextBox32.Name = "TextBox32";
            this.TextBox32.ReadOnly = false;
            this.TextBox32.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox32.SelectedText = "";
            this.TextBox32.SelectionLength = 0;
            this.TextBox32.SelectionStart = 0;
            this.TextBox32.Size = new System.Drawing.Size(495, 24);
            this.TextBox32.TabIndex = 1;
            this.TextBox32.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox32.UseSystemPasswordChar = false;
            this.TextBox32.WordWrap = true;
            // 
            // Label33
            // 
            this.Label33.BackColor = System.Drawing.Color.Transparent;
            this.Label33.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(33, 4);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(101, 22);
            this.Label33.TabIndex = 112;
            this.Label33.Text = "Alarm 10:";
            this.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox32
            // 
            this.PictureBox32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox32.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox32.Image")));
            this.PictureBox32.Location = new System.Drawing.Point(3, 3);
            this.PictureBox32.Name = "PictureBox32";
            this.PictureBox32.Size = new System.Drawing.Size(24, 24);
            this.PictureBox32.TabIndex = 1;
            this.PictureBox32.TabStop = false;
            // 
            // GroupBox34
            // 
            this.GroupBox34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox34.Controls.Add(this.Button106);
            this.GroupBox34.Controls.Add(this.Button107);
            this.GroupBox34.Controls.Add(this.Button108);
            this.GroupBox34.Controls.Add(this.TextBox33);
            this.GroupBox34.Controls.Add(this.Label34);
            this.GroupBox34.Controls.Add(this.PictureBox33);
            this.GroupBox34.Location = new System.Drawing.Point(4, 275);
            this.GroupBox34.Name = "GroupBox34";
            this.GroupBox34.Size = new System.Drawing.Size(754, 30);
            this.GroupBox34.TabIndex = 34;
            // 
            // Button106
            // 
            this.Button106.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button106.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button106.DrawOnGlass = false;
            this.Button106.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button106.ForeColor = System.Drawing.Color.White;
            this.Button106.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button106.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button106.Location = new System.Drawing.Point(639, 3);
            this.Button106.Name = "Button106";
            this.Button106.Size = new System.Drawing.Size(36, 24);
            this.Button106.TabIndex = 115;
            this.Button106.Tag = "2";
            this.Button106.UseVisualStyleBackColor = false;
            // 
            // Button107
            // 
            this.Button107.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button107.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button107.DrawOnGlass = false;
            this.Button107.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button107.ForeColor = System.Drawing.Color.White;
            this.Button107.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button107.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button107.Location = new System.Drawing.Point(677, 3);
            this.Button107.Name = "Button107";
            this.Button107.Size = new System.Drawing.Size(36, 24);
            this.Button107.TabIndex = 114;
            this.Button107.Tag = "1";
            this.Button107.UseVisualStyleBackColor = false;
            // 
            // Button108
            // 
            this.Button108.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button108.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button108.DrawOnGlass = false;
            this.Button108.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button108.ForeColor = System.Drawing.Color.White;
            this.Button108.Image = ((System.Drawing.Image)(resources.GetObject("Button108.Image")));
            this.Button108.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button108.Location = new System.Drawing.Point(715, 3);
            this.Button108.Name = "Button108";
            this.Button108.Size = new System.Drawing.Size(36, 24);
            this.Button108.TabIndex = 113;
            this.Button108.Tag = "3";
            this.Button108.UseVisualStyleBackColor = false;
            // 
            // TextBox33
            // 
            this.TextBox33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox33.DrawOnGlass = false;
            this.TextBox33.ForeColor = System.Drawing.Color.White;
            this.TextBox33.Location = new System.Drawing.Point(140, 3);
            this.TextBox33.MaxLength = 32767;
            this.TextBox33.Multiline = false;
            this.TextBox33.Name = "TextBox33";
            this.TextBox33.ReadOnly = false;
            this.TextBox33.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox33.SelectedText = "";
            this.TextBox33.SelectionLength = 0;
            this.TextBox33.SelectionStart = 0;
            this.TextBox33.Size = new System.Drawing.Size(495, 24);
            this.TextBox33.TabIndex = 1;
            this.TextBox33.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox33.UseSystemPasswordChar = false;
            this.TextBox33.WordWrap = true;
            // 
            // Label34
            // 
            this.Label34.BackColor = System.Drawing.Color.Transparent;
            this.Label34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(33, 4);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(101, 22);
            this.Label34.TabIndex = 112;
            this.Label34.Text = "Alarm 9:";
            this.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox33
            // 
            this.PictureBox33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox33.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox33.Image")));
            this.PictureBox33.Location = new System.Drawing.Point(3, 3);
            this.PictureBox33.Name = "PictureBox33";
            this.PictureBox33.Size = new System.Drawing.Size(24, 24);
            this.PictureBox33.TabIndex = 1;
            this.PictureBox33.TabStop = false;
            // 
            // GroupBox35
            // 
            this.GroupBox35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox35.Controls.Add(this.Button109);
            this.GroupBox35.Controls.Add(this.Button110);
            this.GroupBox35.Controls.Add(this.Button111);
            this.GroupBox35.Controls.Add(this.TextBox34);
            this.GroupBox35.Controls.Add(this.Label35);
            this.GroupBox35.Controls.Add(this.PictureBox34);
            this.GroupBox35.Location = new System.Drawing.Point(4, 241);
            this.GroupBox35.Name = "GroupBox35";
            this.GroupBox35.Size = new System.Drawing.Size(754, 30);
            this.GroupBox35.TabIndex = 33;
            // 
            // Button109
            // 
            this.Button109.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button109.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button109.DrawOnGlass = false;
            this.Button109.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button109.ForeColor = System.Drawing.Color.White;
            this.Button109.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button109.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button109.Location = new System.Drawing.Point(639, 3);
            this.Button109.Name = "Button109";
            this.Button109.Size = new System.Drawing.Size(36, 24);
            this.Button109.TabIndex = 115;
            this.Button109.Tag = "2";
            this.Button109.UseVisualStyleBackColor = false;
            // 
            // Button110
            // 
            this.Button110.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button110.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button110.DrawOnGlass = false;
            this.Button110.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button110.ForeColor = System.Drawing.Color.White;
            this.Button110.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button110.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button110.Location = new System.Drawing.Point(677, 3);
            this.Button110.Name = "Button110";
            this.Button110.Size = new System.Drawing.Size(36, 24);
            this.Button110.TabIndex = 114;
            this.Button110.Tag = "1";
            this.Button110.UseVisualStyleBackColor = false;
            // 
            // Button111
            // 
            this.Button111.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button111.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button111.DrawOnGlass = false;
            this.Button111.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button111.ForeColor = System.Drawing.Color.White;
            this.Button111.Image = ((System.Drawing.Image)(resources.GetObject("Button111.Image")));
            this.Button111.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button111.Location = new System.Drawing.Point(715, 3);
            this.Button111.Name = "Button111";
            this.Button111.Size = new System.Drawing.Size(36, 24);
            this.Button111.TabIndex = 113;
            this.Button111.Tag = "3";
            this.Button111.UseVisualStyleBackColor = false;
            // 
            // TextBox34
            // 
            this.TextBox34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox34.DrawOnGlass = false;
            this.TextBox34.ForeColor = System.Drawing.Color.White;
            this.TextBox34.Location = new System.Drawing.Point(140, 3);
            this.TextBox34.MaxLength = 32767;
            this.TextBox34.Multiline = false;
            this.TextBox34.Name = "TextBox34";
            this.TextBox34.ReadOnly = false;
            this.TextBox34.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox34.SelectedText = "";
            this.TextBox34.SelectionLength = 0;
            this.TextBox34.SelectionStart = 0;
            this.TextBox34.Size = new System.Drawing.Size(495, 24);
            this.TextBox34.TabIndex = 1;
            this.TextBox34.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox34.UseSystemPasswordChar = false;
            this.TextBox34.WordWrap = true;
            // 
            // Label35
            // 
            this.Label35.BackColor = System.Drawing.Color.Transparent;
            this.Label35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(33, 4);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(101, 22);
            this.Label35.TabIndex = 112;
            this.Label35.Text = "Alarm 8:";
            this.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox34
            // 
            this.PictureBox34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox34.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox34.Image")));
            this.PictureBox34.Location = new System.Drawing.Point(3, 3);
            this.PictureBox34.Name = "PictureBox34";
            this.PictureBox34.Size = new System.Drawing.Size(24, 24);
            this.PictureBox34.TabIndex = 1;
            this.PictureBox34.TabStop = false;
            // 
            // GroupBox26
            // 
            this.GroupBox26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox26.Controls.Add(this.Button82);
            this.GroupBox26.Controls.Add(this.Button83);
            this.GroupBox26.Controls.Add(this.Button84);
            this.GroupBox26.Controls.Add(this.TextBox25);
            this.GroupBox26.Controls.Add(this.Label26);
            this.GroupBox26.Controls.Add(this.PictureBox25);
            this.GroupBox26.Location = new System.Drawing.Point(4, 207);
            this.GroupBox26.Name = "GroupBox26";
            this.GroupBox26.Size = new System.Drawing.Size(754, 30);
            this.GroupBox26.TabIndex = 32;
            // 
            // Button82
            // 
            this.Button82.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button82.DrawOnGlass = false;
            this.Button82.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button82.ForeColor = System.Drawing.Color.White;
            this.Button82.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button82.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button82.Location = new System.Drawing.Point(639, 3);
            this.Button82.Name = "Button82";
            this.Button82.Size = new System.Drawing.Size(36, 24);
            this.Button82.TabIndex = 115;
            this.Button82.Tag = "2";
            this.Button82.UseVisualStyleBackColor = false;
            // 
            // Button83
            // 
            this.Button83.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button83.DrawOnGlass = false;
            this.Button83.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button83.ForeColor = System.Drawing.Color.White;
            this.Button83.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button83.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button83.Location = new System.Drawing.Point(677, 3);
            this.Button83.Name = "Button83";
            this.Button83.Size = new System.Drawing.Size(36, 24);
            this.Button83.TabIndex = 114;
            this.Button83.Tag = "1";
            this.Button83.UseVisualStyleBackColor = false;
            // 
            // Button84
            // 
            this.Button84.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button84.DrawOnGlass = false;
            this.Button84.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button84.ForeColor = System.Drawing.Color.White;
            this.Button84.Image = ((System.Drawing.Image)(resources.GetObject("Button84.Image")));
            this.Button84.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button84.Location = new System.Drawing.Point(715, 3);
            this.Button84.Name = "Button84";
            this.Button84.Size = new System.Drawing.Size(36, 24);
            this.Button84.TabIndex = 113;
            this.Button84.Tag = "3";
            this.Button84.UseVisualStyleBackColor = false;
            // 
            // TextBox25
            // 
            this.TextBox25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox25.DrawOnGlass = false;
            this.TextBox25.ForeColor = System.Drawing.Color.White;
            this.TextBox25.Location = new System.Drawing.Point(140, 3);
            this.TextBox25.MaxLength = 32767;
            this.TextBox25.Multiline = false;
            this.TextBox25.Name = "TextBox25";
            this.TextBox25.ReadOnly = false;
            this.TextBox25.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox25.SelectedText = "";
            this.TextBox25.SelectionLength = 0;
            this.TextBox25.SelectionStart = 0;
            this.TextBox25.Size = new System.Drawing.Size(495, 24);
            this.TextBox25.TabIndex = 1;
            this.TextBox25.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox25.UseSystemPasswordChar = false;
            this.TextBox25.WordWrap = true;
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.Transparent;
            this.Label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label26.Location = new System.Drawing.Point(33, 4);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(101, 22);
            this.Label26.TabIndex = 112;
            this.Label26.Text = "Alarm 7:";
            this.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox25
            // 
            this.PictureBox25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox25.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox25.Image")));
            this.PictureBox25.Location = new System.Drawing.Point(3, 3);
            this.PictureBox25.Name = "PictureBox25";
            this.PictureBox25.Size = new System.Drawing.Size(24, 24);
            this.PictureBox25.TabIndex = 1;
            this.PictureBox25.TabStop = false;
            // 
            // GroupBox27
            // 
            this.GroupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox27.Controls.Add(this.Button85);
            this.GroupBox27.Controls.Add(this.Button86);
            this.GroupBox27.Controls.Add(this.Button87);
            this.GroupBox27.Controls.Add(this.TextBox26);
            this.GroupBox27.Controls.Add(this.Label27);
            this.GroupBox27.Controls.Add(this.PictureBox26);
            this.GroupBox27.Location = new System.Drawing.Point(4, 173);
            this.GroupBox27.Name = "GroupBox27";
            this.GroupBox27.Size = new System.Drawing.Size(754, 30);
            this.GroupBox27.TabIndex = 31;
            // 
            // Button85
            // 
            this.Button85.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button85.DrawOnGlass = false;
            this.Button85.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button85.ForeColor = System.Drawing.Color.White;
            this.Button85.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button85.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button85.Location = new System.Drawing.Point(639, 3);
            this.Button85.Name = "Button85";
            this.Button85.Size = new System.Drawing.Size(36, 24);
            this.Button85.TabIndex = 115;
            this.Button85.Tag = "2";
            this.Button85.UseVisualStyleBackColor = false;
            // 
            // Button86
            // 
            this.Button86.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button86.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button86.DrawOnGlass = false;
            this.Button86.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button86.ForeColor = System.Drawing.Color.White;
            this.Button86.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button86.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button86.Location = new System.Drawing.Point(677, 3);
            this.Button86.Name = "Button86";
            this.Button86.Size = new System.Drawing.Size(36, 24);
            this.Button86.TabIndex = 114;
            this.Button86.Tag = "1";
            this.Button86.UseVisualStyleBackColor = false;
            // 
            // Button87
            // 
            this.Button87.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button87.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button87.DrawOnGlass = false;
            this.Button87.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button87.ForeColor = System.Drawing.Color.White;
            this.Button87.Image = ((System.Drawing.Image)(resources.GetObject("Button87.Image")));
            this.Button87.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button87.Location = new System.Drawing.Point(715, 3);
            this.Button87.Name = "Button87";
            this.Button87.Size = new System.Drawing.Size(36, 24);
            this.Button87.TabIndex = 113;
            this.Button87.Tag = "3";
            this.Button87.UseVisualStyleBackColor = false;
            // 
            // TextBox26
            // 
            this.TextBox26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox26.DrawOnGlass = false;
            this.TextBox26.ForeColor = System.Drawing.Color.White;
            this.TextBox26.Location = new System.Drawing.Point(140, 3);
            this.TextBox26.MaxLength = 32767;
            this.TextBox26.Multiline = false;
            this.TextBox26.Name = "TextBox26";
            this.TextBox26.ReadOnly = false;
            this.TextBox26.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox26.SelectedText = "";
            this.TextBox26.SelectionLength = 0;
            this.TextBox26.SelectionStart = 0;
            this.TextBox26.Size = new System.Drawing.Size(495, 24);
            this.TextBox26.TabIndex = 1;
            this.TextBox26.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox26.UseSystemPasswordChar = false;
            this.TextBox26.WordWrap = true;
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.Transparent;
            this.Label27.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(33, 4);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(101, 22);
            this.Label27.TabIndex = 112;
            this.Label27.Text = "Alarm 6:";
            this.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox26
            // 
            this.PictureBox26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox26.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox26.Image")));
            this.PictureBox26.Location = new System.Drawing.Point(3, 3);
            this.PictureBox26.Name = "PictureBox26";
            this.PictureBox26.Size = new System.Drawing.Size(24, 24);
            this.PictureBox26.TabIndex = 1;
            this.PictureBox26.TabStop = false;
            // 
            // GroupBox28
            // 
            this.GroupBox28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox28.Controls.Add(this.Button88);
            this.GroupBox28.Controls.Add(this.Button89);
            this.GroupBox28.Controls.Add(this.Button90);
            this.GroupBox28.Controls.Add(this.TextBox27);
            this.GroupBox28.Controls.Add(this.Label28);
            this.GroupBox28.Controls.Add(this.PictureBox27);
            this.GroupBox28.Location = new System.Drawing.Point(4, 139);
            this.GroupBox28.Name = "GroupBox28";
            this.GroupBox28.Size = new System.Drawing.Size(754, 30);
            this.GroupBox28.TabIndex = 30;
            // 
            // Button88
            // 
            this.Button88.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button88.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button88.DrawOnGlass = false;
            this.Button88.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button88.ForeColor = System.Drawing.Color.White;
            this.Button88.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button88.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button88.Location = new System.Drawing.Point(639, 3);
            this.Button88.Name = "Button88";
            this.Button88.Size = new System.Drawing.Size(36, 24);
            this.Button88.TabIndex = 115;
            this.Button88.Tag = "2";
            this.Button88.UseVisualStyleBackColor = false;
            // 
            // Button89
            // 
            this.Button89.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button89.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button89.DrawOnGlass = false;
            this.Button89.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button89.ForeColor = System.Drawing.Color.White;
            this.Button89.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button89.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button89.Location = new System.Drawing.Point(677, 3);
            this.Button89.Name = "Button89";
            this.Button89.Size = new System.Drawing.Size(36, 24);
            this.Button89.TabIndex = 114;
            this.Button89.Tag = "1";
            this.Button89.UseVisualStyleBackColor = false;
            // 
            // Button90
            // 
            this.Button90.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button90.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button90.DrawOnGlass = false;
            this.Button90.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button90.ForeColor = System.Drawing.Color.White;
            this.Button90.Image = ((System.Drawing.Image)(resources.GetObject("Button90.Image")));
            this.Button90.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button90.Location = new System.Drawing.Point(715, 3);
            this.Button90.Name = "Button90";
            this.Button90.Size = new System.Drawing.Size(36, 24);
            this.Button90.TabIndex = 113;
            this.Button90.Tag = "3";
            this.Button90.UseVisualStyleBackColor = false;
            // 
            // TextBox27
            // 
            this.TextBox27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox27.DrawOnGlass = false;
            this.TextBox27.ForeColor = System.Drawing.Color.White;
            this.TextBox27.Location = new System.Drawing.Point(140, 3);
            this.TextBox27.MaxLength = 32767;
            this.TextBox27.Multiline = false;
            this.TextBox27.Name = "TextBox27";
            this.TextBox27.ReadOnly = false;
            this.TextBox27.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox27.SelectedText = "";
            this.TextBox27.SelectionLength = 0;
            this.TextBox27.SelectionStart = 0;
            this.TextBox27.Size = new System.Drawing.Size(495, 24);
            this.TextBox27.TabIndex = 1;
            this.TextBox27.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox27.UseSystemPasswordChar = false;
            this.TextBox27.WordWrap = true;
            // 
            // Label28
            // 
            this.Label28.BackColor = System.Drawing.Color.Transparent;
            this.Label28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label28.Location = new System.Drawing.Point(33, 4);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(101, 22);
            this.Label28.TabIndex = 112;
            this.Label28.Text = "Alarm 5:";
            this.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox27
            // 
            this.PictureBox27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox27.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox27.Image")));
            this.PictureBox27.Location = new System.Drawing.Point(3, 3);
            this.PictureBox27.Name = "PictureBox27";
            this.PictureBox27.Size = new System.Drawing.Size(24, 24);
            this.PictureBox27.TabIndex = 1;
            this.PictureBox27.TabStop = false;
            // 
            // GroupBox29
            // 
            this.GroupBox29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox29.Controls.Add(this.Button91);
            this.GroupBox29.Controls.Add(this.Button92);
            this.GroupBox29.Controls.Add(this.Button93);
            this.GroupBox29.Controls.Add(this.TextBox28);
            this.GroupBox29.Controls.Add(this.Label29);
            this.GroupBox29.Controls.Add(this.PictureBox28);
            this.GroupBox29.Location = new System.Drawing.Point(4, 105);
            this.GroupBox29.Name = "GroupBox29";
            this.GroupBox29.Size = new System.Drawing.Size(754, 30);
            this.GroupBox29.TabIndex = 29;
            // 
            // Button91
            // 
            this.Button91.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button91.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button91.DrawOnGlass = false;
            this.Button91.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button91.ForeColor = System.Drawing.Color.White;
            this.Button91.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button91.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button91.Location = new System.Drawing.Point(639, 3);
            this.Button91.Name = "Button91";
            this.Button91.Size = new System.Drawing.Size(36, 24);
            this.Button91.TabIndex = 115;
            this.Button91.Tag = "2";
            this.Button91.UseVisualStyleBackColor = false;
            // 
            // Button92
            // 
            this.Button92.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button92.DrawOnGlass = false;
            this.Button92.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button92.ForeColor = System.Drawing.Color.White;
            this.Button92.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button92.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button92.Location = new System.Drawing.Point(677, 3);
            this.Button92.Name = "Button92";
            this.Button92.Size = new System.Drawing.Size(36, 24);
            this.Button92.TabIndex = 114;
            this.Button92.Tag = "1";
            this.Button92.UseVisualStyleBackColor = false;
            // 
            // Button93
            // 
            this.Button93.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button93.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button93.DrawOnGlass = false;
            this.Button93.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button93.ForeColor = System.Drawing.Color.White;
            this.Button93.Image = ((System.Drawing.Image)(resources.GetObject("Button93.Image")));
            this.Button93.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button93.Location = new System.Drawing.Point(715, 3);
            this.Button93.Name = "Button93";
            this.Button93.Size = new System.Drawing.Size(36, 24);
            this.Button93.TabIndex = 113;
            this.Button93.Tag = "3";
            this.Button93.UseVisualStyleBackColor = false;
            // 
            // TextBox28
            // 
            this.TextBox28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox28.DrawOnGlass = false;
            this.TextBox28.ForeColor = System.Drawing.Color.White;
            this.TextBox28.Location = new System.Drawing.Point(140, 3);
            this.TextBox28.MaxLength = 32767;
            this.TextBox28.Multiline = false;
            this.TextBox28.Name = "TextBox28";
            this.TextBox28.ReadOnly = false;
            this.TextBox28.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox28.SelectedText = "";
            this.TextBox28.SelectionLength = 0;
            this.TextBox28.SelectionStart = 0;
            this.TextBox28.Size = new System.Drawing.Size(495, 24);
            this.TextBox28.TabIndex = 1;
            this.TextBox28.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox28.UseSystemPasswordChar = false;
            this.TextBox28.WordWrap = true;
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.Transparent;
            this.Label29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(33, 4);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(101, 22);
            this.Label29.TabIndex = 112;
            this.Label29.Text = "Alarm 4:";
            this.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox28
            // 
            this.PictureBox28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox28.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox28.Image")));
            this.PictureBox28.Location = new System.Drawing.Point(3, 3);
            this.PictureBox28.Name = "PictureBox28";
            this.PictureBox28.Size = new System.Drawing.Size(24, 24);
            this.PictureBox28.TabIndex = 1;
            this.PictureBox28.TabStop = false;
            // 
            // GroupBox30
            // 
            this.GroupBox30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox30.Controls.Add(this.Button94);
            this.GroupBox30.Controls.Add(this.Button95);
            this.GroupBox30.Controls.Add(this.Button96);
            this.GroupBox30.Controls.Add(this.TextBox29);
            this.GroupBox30.Controls.Add(this.Label30);
            this.GroupBox30.Controls.Add(this.PictureBox29);
            this.GroupBox30.Location = new System.Drawing.Point(4, 71);
            this.GroupBox30.Name = "GroupBox30";
            this.GroupBox30.Size = new System.Drawing.Size(754, 30);
            this.GroupBox30.TabIndex = 28;
            // 
            // Button94
            // 
            this.Button94.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button94.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button94.DrawOnGlass = false;
            this.Button94.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button94.ForeColor = System.Drawing.Color.White;
            this.Button94.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button94.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button94.Location = new System.Drawing.Point(639, 3);
            this.Button94.Name = "Button94";
            this.Button94.Size = new System.Drawing.Size(36, 24);
            this.Button94.TabIndex = 115;
            this.Button94.Tag = "2";
            this.Button94.UseVisualStyleBackColor = false;
            // 
            // Button95
            // 
            this.Button95.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button95.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button95.DrawOnGlass = false;
            this.Button95.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button95.ForeColor = System.Drawing.Color.White;
            this.Button95.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button95.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button95.Location = new System.Drawing.Point(677, 3);
            this.Button95.Name = "Button95";
            this.Button95.Size = new System.Drawing.Size(36, 24);
            this.Button95.TabIndex = 114;
            this.Button95.Tag = "1";
            this.Button95.UseVisualStyleBackColor = false;
            // 
            // Button96
            // 
            this.Button96.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button96.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button96.DrawOnGlass = false;
            this.Button96.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button96.ForeColor = System.Drawing.Color.White;
            this.Button96.Image = ((System.Drawing.Image)(resources.GetObject("Button96.Image")));
            this.Button96.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button96.Location = new System.Drawing.Point(715, 3);
            this.Button96.Name = "Button96";
            this.Button96.Size = new System.Drawing.Size(36, 24);
            this.Button96.TabIndex = 113;
            this.Button96.Tag = "3";
            this.Button96.UseVisualStyleBackColor = false;
            // 
            // TextBox29
            // 
            this.TextBox29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox29.DrawOnGlass = false;
            this.TextBox29.ForeColor = System.Drawing.Color.White;
            this.TextBox29.Location = new System.Drawing.Point(140, 3);
            this.TextBox29.MaxLength = 32767;
            this.TextBox29.Multiline = false;
            this.TextBox29.Name = "TextBox29";
            this.TextBox29.ReadOnly = false;
            this.TextBox29.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox29.SelectedText = "";
            this.TextBox29.SelectionLength = 0;
            this.TextBox29.SelectionStart = 0;
            this.TextBox29.Size = new System.Drawing.Size(495, 24);
            this.TextBox29.TabIndex = 1;
            this.TextBox29.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox29.UseSystemPasswordChar = false;
            this.TextBox29.WordWrap = true;
            // 
            // Label30
            // 
            this.Label30.BackColor = System.Drawing.Color.Transparent;
            this.Label30.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label30.Location = new System.Drawing.Point(33, 4);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(101, 22);
            this.Label30.TabIndex = 112;
            this.Label30.Text = "Alarm 3:";
            this.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox29
            // 
            this.PictureBox29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox29.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox29.Image")));
            this.PictureBox29.Location = new System.Drawing.Point(3, 3);
            this.PictureBox29.Name = "PictureBox29";
            this.PictureBox29.Size = new System.Drawing.Size(24, 24);
            this.PictureBox29.TabIndex = 1;
            this.PictureBox29.TabStop = false;
            // 
            // GroupBox31
            // 
            this.GroupBox31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox31.Controls.Add(this.Button97);
            this.GroupBox31.Controls.Add(this.Button98);
            this.GroupBox31.Controls.Add(this.Button99);
            this.GroupBox31.Controls.Add(this.TextBox30);
            this.GroupBox31.Controls.Add(this.Label31);
            this.GroupBox31.Controls.Add(this.PictureBox30);
            this.GroupBox31.Location = new System.Drawing.Point(4, 37);
            this.GroupBox31.Name = "GroupBox31";
            this.GroupBox31.Size = new System.Drawing.Size(754, 30);
            this.GroupBox31.TabIndex = 27;
            // 
            // Button97
            // 
            this.Button97.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button97.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button97.DrawOnGlass = false;
            this.Button97.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button97.ForeColor = System.Drawing.Color.White;
            this.Button97.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button97.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button97.Location = new System.Drawing.Point(639, 3);
            this.Button97.Name = "Button97";
            this.Button97.Size = new System.Drawing.Size(36, 24);
            this.Button97.TabIndex = 115;
            this.Button97.Tag = "2";
            this.Button97.UseVisualStyleBackColor = false;
            // 
            // Button98
            // 
            this.Button98.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button98.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button98.DrawOnGlass = false;
            this.Button98.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button98.ForeColor = System.Drawing.Color.White;
            this.Button98.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button98.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button98.Location = new System.Drawing.Point(677, 3);
            this.Button98.Name = "Button98";
            this.Button98.Size = new System.Drawing.Size(36, 24);
            this.Button98.TabIndex = 114;
            this.Button98.Tag = "1";
            this.Button98.UseVisualStyleBackColor = false;
            // 
            // Button99
            // 
            this.Button99.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button99.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button99.DrawOnGlass = false;
            this.Button99.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button99.ForeColor = System.Drawing.Color.White;
            this.Button99.Image = ((System.Drawing.Image)(resources.GetObject("Button99.Image")));
            this.Button99.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button99.Location = new System.Drawing.Point(715, 3);
            this.Button99.Name = "Button99";
            this.Button99.Size = new System.Drawing.Size(36, 24);
            this.Button99.TabIndex = 113;
            this.Button99.Tag = "3";
            this.Button99.UseVisualStyleBackColor = false;
            // 
            // TextBox30
            // 
            this.TextBox30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox30.DrawOnGlass = false;
            this.TextBox30.ForeColor = System.Drawing.Color.White;
            this.TextBox30.Location = new System.Drawing.Point(140, 3);
            this.TextBox30.MaxLength = 32767;
            this.TextBox30.Multiline = false;
            this.TextBox30.Name = "TextBox30";
            this.TextBox30.ReadOnly = false;
            this.TextBox30.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox30.SelectedText = "";
            this.TextBox30.SelectionLength = 0;
            this.TextBox30.SelectionStart = 0;
            this.TextBox30.Size = new System.Drawing.Size(495, 24);
            this.TextBox30.TabIndex = 1;
            this.TextBox30.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox30.UseSystemPasswordChar = false;
            this.TextBox30.WordWrap = true;
            // 
            // Label31
            // 
            this.Label31.BackColor = System.Drawing.Color.Transparent;
            this.Label31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label31.Location = new System.Drawing.Point(33, 4);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(101, 22);
            this.Label31.TabIndex = 112;
            this.Label31.Text = "Alarm 2:";
            this.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox30
            // 
            this.PictureBox30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox30.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox30.Image")));
            this.PictureBox30.Location = new System.Drawing.Point(3, 3);
            this.PictureBox30.Name = "PictureBox30";
            this.PictureBox30.Size = new System.Drawing.Size(24, 24);
            this.PictureBox30.TabIndex = 1;
            this.PictureBox30.TabStop = false;
            // 
            // GroupBox32
            // 
            this.GroupBox32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox32.Controls.Add(this.Button100);
            this.GroupBox32.Controls.Add(this.Button101);
            this.GroupBox32.Controls.Add(this.Button102);
            this.GroupBox32.Controls.Add(this.TextBox31);
            this.GroupBox32.Controls.Add(this.Label32);
            this.GroupBox32.Controls.Add(this.PictureBox31);
            this.GroupBox32.Location = new System.Drawing.Point(4, 3);
            this.GroupBox32.Name = "GroupBox32";
            this.GroupBox32.Size = new System.Drawing.Size(754, 30);
            this.GroupBox32.TabIndex = 26;
            // 
            // Button100
            // 
            this.Button100.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button100.DrawOnGlass = false;
            this.Button100.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button100.ForeColor = System.Drawing.Color.White;
            this.Button100.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button100.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button100.Location = new System.Drawing.Point(639, 3);
            this.Button100.Name = "Button100";
            this.Button100.Size = new System.Drawing.Size(36, 24);
            this.Button100.TabIndex = 115;
            this.Button100.Tag = "2";
            this.Button100.UseVisualStyleBackColor = false;
            // 
            // Button101
            // 
            this.Button101.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button101.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button101.DrawOnGlass = false;
            this.Button101.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button101.ForeColor = System.Drawing.Color.White;
            this.Button101.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button101.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button101.Location = new System.Drawing.Point(677, 3);
            this.Button101.Name = "Button101";
            this.Button101.Size = new System.Drawing.Size(36, 24);
            this.Button101.TabIndex = 114;
            this.Button101.Tag = "1";
            this.Button101.UseVisualStyleBackColor = false;
            // 
            // Button102
            // 
            this.Button102.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button102.DrawOnGlass = false;
            this.Button102.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button102.ForeColor = System.Drawing.Color.White;
            this.Button102.Image = ((System.Drawing.Image)(resources.GetObject("Button102.Image")));
            this.Button102.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button102.Location = new System.Drawing.Point(715, 3);
            this.Button102.Name = "Button102";
            this.Button102.Size = new System.Drawing.Size(36, 24);
            this.Button102.TabIndex = 113;
            this.Button102.Tag = "3";
            this.Button102.UseVisualStyleBackColor = false;
            // 
            // TextBox31
            // 
            this.TextBox31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox31.DrawOnGlass = false;
            this.TextBox31.ForeColor = System.Drawing.Color.White;
            this.TextBox31.Location = new System.Drawing.Point(140, 3);
            this.TextBox31.MaxLength = 32767;
            this.TextBox31.Multiline = false;
            this.TextBox31.Name = "TextBox31";
            this.TextBox31.ReadOnly = false;
            this.TextBox31.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox31.SelectedText = "";
            this.TextBox31.SelectionLength = 0;
            this.TextBox31.SelectionStart = 0;
            this.TextBox31.Size = new System.Drawing.Size(495, 24);
            this.TextBox31.TabIndex = 1;
            this.TextBox31.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox31.UseSystemPasswordChar = false;
            this.TextBox31.WordWrap = true;
            // 
            // Label32
            // 
            this.Label32.BackColor = System.Drawing.Color.Transparent;
            this.Label32.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label32.Location = new System.Drawing.Point(33, 4);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(101, 22);
            this.Label32.TabIndex = 112;
            this.Label32.Text = "Alarm 1:";
            this.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox31
            // 
            this.PictureBox31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox31.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox31.Image")));
            this.PictureBox31.Location = new System.Drawing.Point(3, 3);
            this.PictureBox31.Name = "PictureBox31";
            this.PictureBox31.Size = new System.Drawing.Size(24, 24);
            this.PictureBox31.TabIndex = 1;
            this.PictureBox31.TabStop = false;
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage6.Controls.Add(this.GroupBox36);
            this.TabPage6.Controls.Add(this.GroupBox37);
            this.TabPage6.Controls.Add(this.GroupBox38);
            this.TabPage6.Controls.Add(this.GroupBox39);
            this.TabPage6.Controls.Add(this.GroupBox40);
            this.TabPage6.Controls.Add(this.GroupBox41);
            this.TabPage6.Controls.Add(this.GroupBox42);
            this.TabPage6.Controls.Add(this.GroupBox43);
            this.TabPage6.Controls.Add(this.GroupBox44);
            this.TabPage6.Controls.Add(this.GroupBox45);
            this.TabPage6.Location = new System.Drawing.Point(144, 4);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(761, 368);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Ringtones";
            // 
            // GroupBox36
            // 
            this.GroupBox36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox36.Controls.Add(this.Button112);
            this.GroupBox36.Controls.Add(this.Button113);
            this.GroupBox36.Controls.Add(this.Button114);
            this.GroupBox36.Controls.Add(this.TextBox35);
            this.GroupBox36.Controls.Add(this.Label36);
            this.GroupBox36.Controls.Add(this.PictureBox35);
            this.GroupBox36.Location = new System.Drawing.Point(4, 309);
            this.GroupBox36.Name = "GroupBox36";
            this.GroupBox36.Size = new System.Drawing.Size(754, 30);
            this.GroupBox36.TabIndex = 55;
            // 
            // Button112
            // 
            this.Button112.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button112.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button112.DrawOnGlass = false;
            this.Button112.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button112.ForeColor = System.Drawing.Color.White;
            this.Button112.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button112.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button112.Location = new System.Drawing.Point(639, 3);
            this.Button112.Name = "Button112";
            this.Button112.Size = new System.Drawing.Size(36, 24);
            this.Button112.TabIndex = 115;
            this.Button112.Tag = "2";
            this.Button112.UseVisualStyleBackColor = false;
            // 
            // Button113
            // 
            this.Button113.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button113.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button113.DrawOnGlass = false;
            this.Button113.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button113.ForeColor = System.Drawing.Color.White;
            this.Button113.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button113.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button113.Location = new System.Drawing.Point(677, 3);
            this.Button113.Name = "Button113";
            this.Button113.Size = new System.Drawing.Size(36, 24);
            this.Button113.TabIndex = 114;
            this.Button113.Tag = "1";
            this.Button113.UseVisualStyleBackColor = false;
            // 
            // Button114
            // 
            this.Button114.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button114.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button114.DrawOnGlass = false;
            this.Button114.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button114.ForeColor = System.Drawing.Color.White;
            this.Button114.Image = ((System.Drawing.Image)(resources.GetObject("Button114.Image")));
            this.Button114.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button114.Location = new System.Drawing.Point(715, 3);
            this.Button114.Name = "Button114";
            this.Button114.Size = new System.Drawing.Size(36, 24);
            this.Button114.TabIndex = 113;
            this.Button114.Tag = "3";
            this.Button114.UseVisualStyleBackColor = false;
            // 
            // TextBox35
            // 
            this.TextBox35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox35.DrawOnGlass = false;
            this.TextBox35.ForeColor = System.Drawing.Color.White;
            this.TextBox35.Location = new System.Drawing.Point(140, 3);
            this.TextBox35.MaxLength = 32767;
            this.TextBox35.Multiline = false;
            this.TextBox35.Name = "TextBox35";
            this.TextBox35.ReadOnly = false;
            this.TextBox35.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox35.SelectedText = "";
            this.TextBox35.SelectionLength = 0;
            this.TextBox35.SelectionStart = 0;
            this.TextBox35.Size = new System.Drawing.Size(495, 24);
            this.TextBox35.TabIndex = 1;
            this.TextBox35.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox35.UseSystemPasswordChar = false;
            this.TextBox35.WordWrap = true;
            // 
            // Label36
            // 
            this.Label36.BackColor = System.Drawing.Color.Transparent;
            this.Label36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label36.Location = new System.Drawing.Point(33, 4);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(101, 22);
            this.Label36.TabIndex = 112;
            this.Label36.Text = "Ringtone 10:";
            this.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox35
            // 
            this.PictureBox35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox35.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox35.Image")));
            this.PictureBox35.Location = new System.Drawing.Point(3, 3);
            this.PictureBox35.Name = "PictureBox35";
            this.PictureBox35.Size = new System.Drawing.Size(24, 24);
            this.PictureBox35.TabIndex = 1;
            this.PictureBox35.TabStop = false;
            // 
            // GroupBox37
            // 
            this.GroupBox37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox37.Controls.Add(this.Button115);
            this.GroupBox37.Controls.Add(this.Button116);
            this.GroupBox37.Controls.Add(this.Button117);
            this.GroupBox37.Controls.Add(this.TextBox36);
            this.GroupBox37.Controls.Add(this.Label37);
            this.GroupBox37.Controls.Add(this.PictureBox36);
            this.GroupBox37.Location = new System.Drawing.Point(4, 275);
            this.GroupBox37.Name = "GroupBox37";
            this.GroupBox37.Size = new System.Drawing.Size(754, 30);
            this.GroupBox37.TabIndex = 54;
            // 
            // Button115
            // 
            this.Button115.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button115.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button115.DrawOnGlass = false;
            this.Button115.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button115.ForeColor = System.Drawing.Color.White;
            this.Button115.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button115.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button115.Location = new System.Drawing.Point(639, 3);
            this.Button115.Name = "Button115";
            this.Button115.Size = new System.Drawing.Size(36, 24);
            this.Button115.TabIndex = 115;
            this.Button115.Tag = "2";
            this.Button115.UseVisualStyleBackColor = false;
            // 
            // Button116
            // 
            this.Button116.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button116.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button116.DrawOnGlass = false;
            this.Button116.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button116.ForeColor = System.Drawing.Color.White;
            this.Button116.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button116.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button116.Location = new System.Drawing.Point(677, 3);
            this.Button116.Name = "Button116";
            this.Button116.Size = new System.Drawing.Size(36, 24);
            this.Button116.TabIndex = 114;
            this.Button116.Tag = "1";
            this.Button116.UseVisualStyleBackColor = false;
            // 
            // Button117
            // 
            this.Button117.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button117.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button117.DrawOnGlass = false;
            this.Button117.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button117.ForeColor = System.Drawing.Color.White;
            this.Button117.Image = ((System.Drawing.Image)(resources.GetObject("Button117.Image")));
            this.Button117.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button117.Location = new System.Drawing.Point(715, 3);
            this.Button117.Name = "Button117";
            this.Button117.Size = new System.Drawing.Size(36, 24);
            this.Button117.TabIndex = 113;
            this.Button117.Tag = "3";
            this.Button117.UseVisualStyleBackColor = false;
            // 
            // TextBox36
            // 
            this.TextBox36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox36.DrawOnGlass = false;
            this.TextBox36.ForeColor = System.Drawing.Color.White;
            this.TextBox36.Location = new System.Drawing.Point(140, 3);
            this.TextBox36.MaxLength = 32767;
            this.TextBox36.Multiline = false;
            this.TextBox36.Name = "TextBox36";
            this.TextBox36.ReadOnly = false;
            this.TextBox36.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox36.SelectedText = "";
            this.TextBox36.SelectionLength = 0;
            this.TextBox36.SelectionStart = 0;
            this.TextBox36.Size = new System.Drawing.Size(495, 24);
            this.TextBox36.TabIndex = 1;
            this.TextBox36.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox36.UseSystemPasswordChar = false;
            this.TextBox36.WordWrap = true;
            // 
            // Label37
            // 
            this.Label37.BackColor = System.Drawing.Color.Transparent;
            this.Label37.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label37.Location = new System.Drawing.Point(33, 4);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(101, 22);
            this.Label37.TabIndex = 112;
            this.Label37.Text = "Ringtone 9:";
            this.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox36
            // 
            this.PictureBox36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox36.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox36.Image")));
            this.PictureBox36.Location = new System.Drawing.Point(3, 3);
            this.PictureBox36.Name = "PictureBox36";
            this.PictureBox36.Size = new System.Drawing.Size(24, 24);
            this.PictureBox36.TabIndex = 1;
            this.PictureBox36.TabStop = false;
            // 
            // GroupBox38
            // 
            this.GroupBox38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox38.Controls.Add(this.Button118);
            this.GroupBox38.Controls.Add(this.Button119);
            this.GroupBox38.Controls.Add(this.Button120);
            this.GroupBox38.Controls.Add(this.TextBox37);
            this.GroupBox38.Controls.Add(this.Label38);
            this.GroupBox38.Controls.Add(this.PictureBox37);
            this.GroupBox38.Location = new System.Drawing.Point(4, 241);
            this.GroupBox38.Name = "GroupBox38";
            this.GroupBox38.Size = new System.Drawing.Size(754, 30);
            this.GroupBox38.TabIndex = 53;
            // 
            // Button118
            // 
            this.Button118.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button118.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button118.DrawOnGlass = false;
            this.Button118.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button118.ForeColor = System.Drawing.Color.White;
            this.Button118.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button118.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button118.Location = new System.Drawing.Point(639, 3);
            this.Button118.Name = "Button118";
            this.Button118.Size = new System.Drawing.Size(36, 24);
            this.Button118.TabIndex = 115;
            this.Button118.Tag = "2";
            this.Button118.UseVisualStyleBackColor = false;
            // 
            // Button119
            // 
            this.Button119.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button119.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button119.DrawOnGlass = false;
            this.Button119.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button119.ForeColor = System.Drawing.Color.White;
            this.Button119.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button119.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button119.Location = new System.Drawing.Point(677, 3);
            this.Button119.Name = "Button119";
            this.Button119.Size = new System.Drawing.Size(36, 24);
            this.Button119.TabIndex = 114;
            this.Button119.Tag = "1";
            this.Button119.UseVisualStyleBackColor = false;
            // 
            // Button120
            // 
            this.Button120.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button120.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button120.DrawOnGlass = false;
            this.Button120.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button120.ForeColor = System.Drawing.Color.White;
            this.Button120.Image = ((System.Drawing.Image)(resources.GetObject("Button120.Image")));
            this.Button120.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button120.Location = new System.Drawing.Point(715, 3);
            this.Button120.Name = "Button120";
            this.Button120.Size = new System.Drawing.Size(36, 24);
            this.Button120.TabIndex = 113;
            this.Button120.Tag = "3";
            this.Button120.UseVisualStyleBackColor = false;
            // 
            // TextBox37
            // 
            this.TextBox37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox37.DrawOnGlass = false;
            this.TextBox37.ForeColor = System.Drawing.Color.White;
            this.TextBox37.Location = new System.Drawing.Point(140, 3);
            this.TextBox37.MaxLength = 32767;
            this.TextBox37.Multiline = false;
            this.TextBox37.Name = "TextBox37";
            this.TextBox37.ReadOnly = false;
            this.TextBox37.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox37.SelectedText = "";
            this.TextBox37.SelectionLength = 0;
            this.TextBox37.SelectionStart = 0;
            this.TextBox37.Size = new System.Drawing.Size(495, 24);
            this.TextBox37.TabIndex = 1;
            this.TextBox37.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox37.UseSystemPasswordChar = false;
            this.TextBox37.WordWrap = true;
            // 
            // Label38
            // 
            this.Label38.BackColor = System.Drawing.Color.Transparent;
            this.Label38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label38.Location = new System.Drawing.Point(33, 4);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(101, 22);
            this.Label38.TabIndex = 112;
            this.Label38.Text = "Ringtone 8:";
            this.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox37
            // 
            this.PictureBox37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox37.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox37.Image")));
            this.PictureBox37.Location = new System.Drawing.Point(3, 3);
            this.PictureBox37.Name = "PictureBox37";
            this.PictureBox37.Size = new System.Drawing.Size(24, 24);
            this.PictureBox37.TabIndex = 1;
            this.PictureBox37.TabStop = false;
            // 
            // GroupBox39
            // 
            this.GroupBox39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox39.Controls.Add(this.Button121);
            this.GroupBox39.Controls.Add(this.Button122);
            this.GroupBox39.Controls.Add(this.Button123);
            this.GroupBox39.Controls.Add(this.TextBox38);
            this.GroupBox39.Controls.Add(this.Label39);
            this.GroupBox39.Controls.Add(this.PictureBox38);
            this.GroupBox39.Location = new System.Drawing.Point(4, 207);
            this.GroupBox39.Name = "GroupBox39";
            this.GroupBox39.Size = new System.Drawing.Size(754, 30);
            this.GroupBox39.TabIndex = 52;
            // 
            // Button121
            // 
            this.Button121.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button121.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button121.DrawOnGlass = false;
            this.Button121.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button121.ForeColor = System.Drawing.Color.White;
            this.Button121.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button121.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button121.Location = new System.Drawing.Point(639, 3);
            this.Button121.Name = "Button121";
            this.Button121.Size = new System.Drawing.Size(36, 24);
            this.Button121.TabIndex = 115;
            this.Button121.Tag = "2";
            this.Button121.UseVisualStyleBackColor = false;
            // 
            // Button122
            // 
            this.Button122.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button122.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button122.DrawOnGlass = false;
            this.Button122.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button122.ForeColor = System.Drawing.Color.White;
            this.Button122.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button122.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button122.Location = new System.Drawing.Point(677, 3);
            this.Button122.Name = "Button122";
            this.Button122.Size = new System.Drawing.Size(36, 24);
            this.Button122.TabIndex = 114;
            this.Button122.Tag = "1";
            this.Button122.UseVisualStyleBackColor = false;
            // 
            // Button123
            // 
            this.Button123.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button123.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button123.DrawOnGlass = false;
            this.Button123.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button123.ForeColor = System.Drawing.Color.White;
            this.Button123.Image = ((System.Drawing.Image)(resources.GetObject("Button123.Image")));
            this.Button123.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button123.Location = new System.Drawing.Point(715, 3);
            this.Button123.Name = "Button123";
            this.Button123.Size = new System.Drawing.Size(36, 24);
            this.Button123.TabIndex = 113;
            this.Button123.Tag = "3";
            this.Button123.UseVisualStyleBackColor = false;
            // 
            // TextBox38
            // 
            this.TextBox38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox38.DrawOnGlass = false;
            this.TextBox38.ForeColor = System.Drawing.Color.White;
            this.TextBox38.Location = new System.Drawing.Point(140, 3);
            this.TextBox38.MaxLength = 32767;
            this.TextBox38.Multiline = false;
            this.TextBox38.Name = "TextBox38";
            this.TextBox38.ReadOnly = false;
            this.TextBox38.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox38.SelectedText = "";
            this.TextBox38.SelectionLength = 0;
            this.TextBox38.SelectionStart = 0;
            this.TextBox38.Size = new System.Drawing.Size(495, 24);
            this.TextBox38.TabIndex = 1;
            this.TextBox38.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox38.UseSystemPasswordChar = false;
            this.TextBox38.WordWrap = true;
            // 
            // Label39
            // 
            this.Label39.BackColor = System.Drawing.Color.Transparent;
            this.Label39.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label39.Location = new System.Drawing.Point(33, 4);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(101, 22);
            this.Label39.TabIndex = 112;
            this.Label39.Text = "Ringtone 7:";
            this.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox38
            // 
            this.PictureBox38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox38.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox38.Image")));
            this.PictureBox38.Location = new System.Drawing.Point(3, 3);
            this.PictureBox38.Name = "PictureBox38";
            this.PictureBox38.Size = new System.Drawing.Size(24, 24);
            this.PictureBox38.TabIndex = 1;
            this.PictureBox38.TabStop = false;
            // 
            // GroupBox40
            // 
            this.GroupBox40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox40.Controls.Add(this.Button124);
            this.GroupBox40.Controls.Add(this.Button125);
            this.GroupBox40.Controls.Add(this.Button126);
            this.GroupBox40.Controls.Add(this.TextBox39);
            this.GroupBox40.Controls.Add(this.Label40);
            this.GroupBox40.Controls.Add(this.PictureBox39);
            this.GroupBox40.Location = new System.Drawing.Point(4, 173);
            this.GroupBox40.Name = "GroupBox40";
            this.GroupBox40.Size = new System.Drawing.Size(754, 30);
            this.GroupBox40.TabIndex = 51;
            // 
            // Button124
            // 
            this.Button124.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button124.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button124.DrawOnGlass = false;
            this.Button124.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button124.ForeColor = System.Drawing.Color.White;
            this.Button124.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button124.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button124.Location = new System.Drawing.Point(639, 3);
            this.Button124.Name = "Button124";
            this.Button124.Size = new System.Drawing.Size(36, 24);
            this.Button124.TabIndex = 115;
            this.Button124.Tag = "2";
            this.Button124.UseVisualStyleBackColor = false;
            // 
            // Button125
            // 
            this.Button125.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button125.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button125.DrawOnGlass = false;
            this.Button125.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button125.ForeColor = System.Drawing.Color.White;
            this.Button125.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button125.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button125.Location = new System.Drawing.Point(677, 3);
            this.Button125.Name = "Button125";
            this.Button125.Size = new System.Drawing.Size(36, 24);
            this.Button125.TabIndex = 114;
            this.Button125.Tag = "1";
            this.Button125.UseVisualStyleBackColor = false;
            // 
            // Button126
            // 
            this.Button126.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button126.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button126.DrawOnGlass = false;
            this.Button126.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button126.ForeColor = System.Drawing.Color.White;
            this.Button126.Image = ((System.Drawing.Image)(resources.GetObject("Button126.Image")));
            this.Button126.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button126.Location = new System.Drawing.Point(715, 3);
            this.Button126.Name = "Button126";
            this.Button126.Size = new System.Drawing.Size(36, 24);
            this.Button126.TabIndex = 113;
            this.Button126.Tag = "3";
            this.Button126.UseVisualStyleBackColor = false;
            // 
            // TextBox39
            // 
            this.TextBox39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox39.DrawOnGlass = false;
            this.TextBox39.ForeColor = System.Drawing.Color.White;
            this.TextBox39.Location = new System.Drawing.Point(140, 3);
            this.TextBox39.MaxLength = 32767;
            this.TextBox39.Multiline = false;
            this.TextBox39.Name = "TextBox39";
            this.TextBox39.ReadOnly = false;
            this.TextBox39.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox39.SelectedText = "";
            this.TextBox39.SelectionLength = 0;
            this.TextBox39.SelectionStart = 0;
            this.TextBox39.Size = new System.Drawing.Size(495, 24);
            this.TextBox39.TabIndex = 1;
            this.TextBox39.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox39.UseSystemPasswordChar = false;
            this.TextBox39.WordWrap = true;
            // 
            // Label40
            // 
            this.Label40.BackColor = System.Drawing.Color.Transparent;
            this.Label40.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label40.Location = new System.Drawing.Point(33, 4);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(101, 22);
            this.Label40.TabIndex = 112;
            this.Label40.Text = "Ringtone 6:";
            this.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox39
            // 
            this.PictureBox39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox39.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox39.Image")));
            this.PictureBox39.Location = new System.Drawing.Point(3, 3);
            this.PictureBox39.Name = "PictureBox39";
            this.PictureBox39.Size = new System.Drawing.Size(24, 24);
            this.PictureBox39.TabIndex = 1;
            this.PictureBox39.TabStop = false;
            // 
            // GroupBox41
            // 
            this.GroupBox41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox41.Controls.Add(this.Button127);
            this.GroupBox41.Controls.Add(this.Button128);
            this.GroupBox41.Controls.Add(this.Button129);
            this.GroupBox41.Controls.Add(this.TextBox40);
            this.GroupBox41.Controls.Add(this.Label41);
            this.GroupBox41.Controls.Add(this.PictureBox40);
            this.GroupBox41.Location = new System.Drawing.Point(4, 139);
            this.GroupBox41.Name = "GroupBox41";
            this.GroupBox41.Size = new System.Drawing.Size(754, 30);
            this.GroupBox41.TabIndex = 50;
            // 
            // Button127
            // 
            this.Button127.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button127.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button127.DrawOnGlass = false;
            this.Button127.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button127.ForeColor = System.Drawing.Color.White;
            this.Button127.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button127.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button127.Location = new System.Drawing.Point(639, 3);
            this.Button127.Name = "Button127";
            this.Button127.Size = new System.Drawing.Size(36, 24);
            this.Button127.TabIndex = 115;
            this.Button127.Tag = "2";
            this.Button127.UseVisualStyleBackColor = false;
            // 
            // Button128
            // 
            this.Button128.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button128.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button128.DrawOnGlass = false;
            this.Button128.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button128.ForeColor = System.Drawing.Color.White;
            this.Button128.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button128.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button128.Location = new System.Drawing.Point(677, 3);
            this.Button128.Name = "Button128";
            this.Button128.Size = new System.Drawing.Size(36, 24);
            this.Button128.TabIndex = 114;
            this.Button128.Tag = "1";
            this.Button128.UseVisualStyleBackColor = false;
            // 
            // Button129
            // 
            this.Button129.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button129.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button129.DrawOnGlass = false;
            this.Button129.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button129.ForeColor = System.Drawing.Color.White;
            this.Button129.Image = ((System.Drawing.Image)(resources.GetObject("Button129.Image")));
            this.Button129.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button129.Location = new System.Drawing.Point(715, 3);
            this.Button129.Name = "Button129";
            this.Button129.Size = new System.Drawing.Size(36, 24);
            this.Button129.TabIndex = 113;
            this.Button129.Tag = "3";
            this.Button129.UseVisualStyleBackColor = false;
            // 
            // TextBox40
            // 
            this.TextBox40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox40.DrawOnGlass = false;
            this.TextBox40.ForeColor = System.Drawing.Color.White;
            this.TextBox40.Location = new System.Drawing.Point(140, 3);
            this.TextBox40.MaxLength = 32767;
            this.TextBox40.Multiline = false;
            this.TextBox40.Name = "TextBox40";
            this.TextBox40.ReadOnly = false;
            this.TextBox40.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox40.SelectedText = "";
            this.TextBox40.SelectionLength = 0;
            this.TextBox40.SelectionStart = 0;
            this.TextBox40.Size = new System.Drawing.Size(495, 24);
            this.TextBox40.TabIndex = 1;
            this.TextBox40.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox40.UseSystemPasswordChar = false;
            this.TextBox40.WordWrap = true;
            // 
            // Label41
            // 
            this.Label41.BackColor = System.Drawing.Color.Transparent;
            this.Label41.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label41.Location = new System.Drawing.Point(33, 4);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(101, 22);
            this.Label41.TabIndex = 112;
            this.Label41.Text = "Ringtone 5:";
            this.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox40
            // 
            this.PictureBox40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox40.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox40.Image")));
            this.PictureBox40.Location = new System.Drawing.Point(3, 3);
            this.PictureBox40.Name = "PictureBox40";
            this.PictureBox40.Size = new System.Drawing.Size(24, 24);
            this.PictureBox40.TabIndex = 1;
            this.PictureBox40.TabStop = false;
            // 
            // GroupBox42
            // 
            this.GroupBox42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox42.Controls.Add(this.Button130);
            this.GroupBox42.Controls.Add(this.Button131);
            this.GroupBox42.Controls.Add(this.Button132);
            this.GroupBox42.Controls.Add(this.TextBox41);
            this.GroupBox42.Controls.Add(this.Label42);
            this.GroupBox42.Controls.Add(this.PictureBox41);
            this.GroupBox42.Location = new System.Drawing.Point(4, 105);
            this.GroupBox42.Name = "GroupBox42";
            this.GroupBox42.Size = new System.Drawing.Size(754, 30);
            this.GroupBox42.TabIndex = 49;
            // 
            // Button130
            // 
            this.Button130.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button130.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button130.DrawOnGlass = false;
            this.Button130.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button130.ForeColor = System.Drawing.Color.White;
            this.Button130.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button130.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button130.Location = new System.Drawing.Point(639, 3);
            this.Button130.Name = "Button130";
            this.Button130.Size = new System.Drawing.Size(36, 24);
            this.Button130.TabIndex = 115;
            this.Button130.Tag = "2";
            this.Button130.UseVisualStyleBackColor = false;
            // 
            // Button131
            // 
            this.Button131.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button131.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button131.DrawOnGlass = false;
            this.Button131.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button131.ForeColor = System.Drawing.Color.White;
            this.Button131.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button131.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button131.Location = new System.Drawing.Point(677, 3);
            this.Button131.Name = "Button131";
            this.Button131.Size = new System.Drawing.Size(36, 24);
            this.Button131.TabIndex = 114;
            this.Button131.Tag = "1";
            this.Button131.UseVisualStyleBackColor = false;
            // 
            // Button132
            // 
            this.Button132.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button132.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button132.DrawOnGlass = false;
            this.Button132.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button132.ForeColor = System.Drawing.Color.White;
            this.Button132.Image = ((System.Drawing.Image)(resources.GetObject("Button132.Image")));
            this.Button132.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button132.Location = new System.Drawing.Point(715, 3);
            this.Button132.Name = "Button132";
            this.Button132.Size = new System.Drawing.Size(36, 24);
            this.Button132.TabIndex = 113;
            this.Button132.Tag = "3";
            this.Button132.UseVisualStyleBackColor = false;
            // 
            // TextBox41
            // 
            this.TextBox41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox41.DrawOnGlass = false;
            this.TextBox41.ForeColor = System.Drawing.Color.White;
            this.TextBox41.Location = new System.Drawing.Point(140, 3);
            this.TextBox41.MaxLength = 32767;
            this.TextBox41.Multiline = false;
            this.TextBox41.Name = "TextBox41";
            this.TextBox41.ReadOnly = false;
            this.TextBox41.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox41.SelectedText = "";
            this.TextBox41.SelectionLength = 0;
            this.TextBox41.SelectionStart = 0;
            this.TextBox41.Size = new System.Drawing.Size(495, 24);
            this.TextBox41.TabIndex = 1;
            this.TextBox41.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox41.UseSystemPasswordChar = false;
            this.TextBox41.WordWrap = true;
            // 
            // Label42
            // 
            this.Label42.BackColor = System.Drawing.Color.Transparent;
            this.Label42.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label42.Location = new System.Drawing.Point(33, 4);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(101, 22);
            this.Label42.TabIndex = 112;
            this.Label42.Text = "Ringtone 4:";
            this.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox41
            // 
            this.PictureBox41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(3, 3);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(24, 24);
            this.PictureBox41.TabIndex = 1;
            this.PictureBox41.TabStop = false;
            // 
            // GroupBox43
            // 
            this.GroupBox43.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox43.Controls.Add(this.Button133);
            this.GroupBox43.Controls.Add(this.Button134);
            this.GroupBox43.Controls.Add(this.Button135);
            this.GroupBox43.Controls.Add(this.TextBox42);
            this.GroupBox43.Controls.Add(this.Label43);
            this.GroupBox43.Controls.Add(this.PictureBox42);
            this.GroupBox43.Location = new System.Drawing.Point(4, 71);
            this.GroupBox43.Name = "GroupBox43";
            this.GroupBox43.Size = new System.Drawing.Size(754, 30);
            this.GroupBox43.TabIndex = 48;
            // 
            // Button133
            // 
            this.Button133.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button133.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button133.DrawOnGlass = false;
            this.Button133.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button133.ForeColor = System.Drawing.Color.White;
            this.Button133.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button133.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button133.Location = new System.Drawing.Point(639, 3);
            this.Button133.Name = "Button133";
            this.Button133.Size = new System.Drawing.Size(36, 24);
            this.Button133.TabIndex = 115;
            this.Button133.Tag = "2";
            this.Button133.UseVisualStyleBackColor = false;
            // 
            // Button134
            // 
            this.Button134.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button134.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button134.DrawOnGlass = false;
            this.Button134.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button134.ForeColor = System.Drawing.Color.White;
            this.Button134.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button134.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button134.Location = new System.Drawing.Point(677, 3);
            this.Button134.Name = "Button134";
            this.Button134.Size = new System.Drawing.Size(36, 24);
            this.Button134.TabIndex = 114;
            this.Button134.Tag = "1";
            this.Button134.UseVisualStyleBackColor = false;
            // 
            // Button135
            // 
            this.Button135.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button135.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button135.DrawOnGlass = false;
            this.Button135.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button135.ForeColor = System.Drawing.Color.White;
            this.Button135.Image = ((System.Drawing.Image)(resources.GetObject("Button135.Image")));
            this.Button135.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button135.Location = new System.Drawing.Point(715, 3);
            this.Button135.Name = "Button135";
            this.Button135.Size = new System.Drawing.Size(36, 24);
            this.Button135.TabIndex = 113;
            this.Button135.Tag = "3";
            this.Button135.UseVisualStyleBackColor = false;
            // 
            // TextBox42
            // 
            this.TextBox42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox42.DrawOnGlass = false;
            this.TextBox42.ForeColor = System.Drawing.Color.White;
            this.TextBox42.Location = new System.Drawing.Point(140, 3);
            this.TextBox42.MaxLength = 32767;
            this.TextBox42.Multiline = false;
            this.TextBox42.Name = "TextBox42";
            this.TextBox42.ReadOnly = false;
            this.TextBox42.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox42.SelectedText = "";
            this.TextBox42.SelectionLength = 0;
            this.TextBox42.SelectionStart = 0;
            this.TextBox42.Size = new System.Drawing.Size(495, 24);
            this.TextBox42.TabIndex = 1;
            this.TextBox42.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox42.UseSystemPasswordChar = false;
            this.TextBox42.WordWrap = true;
            // 
            // Label43
            // 
            this.Label43.BackColor = System.Drawing.Color.Transparent;
            this.Label43.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label43.Location = new System.Drawing.Point(33, 4);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(101, 22);
            this.Label43.TabIndex = 112;
            this.Label43.Text = "Ringtone 3:";
            this.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox42
            // 
            this.PictureBox42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox42.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox42.Image")));
            this.PictureBox42.Location = new System.Drawing.Point(3, 3);
            this.PictureBox42.Name = "PictureBox42";
            this.PictureBox42.Size = new System.Drawing.Size(24, 24);
            this.PictureBox42.TabIndex = 1;
            this.PictureBox42.TabStop = false;
            // 
            // GroupBox44
            // 
            this.GroupBox44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox44.Controls.Add(this.Button136);
            this.GroupBox44.Controls.Add(this.Button137);
            this.GroupBox44.Controls.Add(this.Button138);
            this.GroupBox44.Controls.Add(this.TextBox43);
            this.GroupBox44.Controls.Add(this.Label44);
            this.GroupBox44.Controls.Add(this.PictureBox43);
            this.GroupBox44.Location = new System.Drawing.Point(4, 37);
            this.GroupBox44.Name = "GroupBox44";
            this.GroupBox44.Size = new System.Drawing.Size(754, 30);
            this.GroupBox44.TabIndex = 47;
            // 
            // Button136
            // 
            this.Button136.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button136.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button136.DrawOnGlass = false;
            this.Button136.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button136.ForeColor = System.Drawing.Color.White;
            this.Button136.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button136.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button136.Location = new System.Drawing.Point(639, 3);
            this.Button136.Name = "Button136";
            this.Button136.Size = new System.Drawing.Size(36, 24);
            this.Button136.TabIndex = 115;
            this.Button136.Tag = "2";
            this.Button136.UseVisualStyleBackColor = false;
            // 
            // Button137
            // 
            this.Button137.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button137.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button137.DrawOnGlass = false;
            this.Button137.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button137.ForeColor = System.Drawing.Color.White;
            this.Button137.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button137.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button137.Location = new System.Drawing.Point(677, 3);
            this.Button137.Name = "Button137";
            this.Button137.Size = new System.Drawing.Size(36, 24);
            this.Button137.TabIndex = 114;
            this.Button137.Tag = "1";
            this.Button137.UseVisualStyleBackColor = false;
            // 
            // Button138
            // 
            this.Button138.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button138.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button138.DrawOnGlass = false;
            this.Button138.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button138.ForeColor = System.Drawing.Color.White;
            this.Button138.Image = ((System.Drawing.Image)(resources.GetObject("Button138.Image")));
            this.Button138.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button138.Location = new System.Drawing.Point(715, 3);
            this.Button138.Name = "Button138";
            this.Button138.Size = new System.Drawing.Size(36, 24);
            this.Button138.TabIndex = 113;
            this.Button138.Tag = "3";
            this.Button138.UseVisualStyleBackColor = false;
            // 
            // TextBox43
            // 
            this.TextBox43.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox43.DrawOnGlass = false;
            this.TextBox43.ForeColor = System.Drawing.Color.White;
            this.TextBox43.Location = new System.Drawing.Point(140, 3);
            this.TextBox43.MaxLength = 32767;
            this.TextBox43.Multiline = false;
            this.TextBox43.Name = "TextBox43";
            this.TextBox43.ReadOnly = false;
            this.TextBox43.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox43.SelectedText = "";
            this.TextBox43.SelectionLength = 0;
            this.TextBox43.SelectionStart = 0;
            this.TextBox43.Size = new System.Drawing.Size(495, 24);
            this.TextBox43.TabIndex = 1;
            this.TextBox43.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox43.UseSystemPasswordChar = false;
            this.TextBox43.WordWrap = true;
            // 
            // Label44
            // 
            this.Label44.BackColor = System.Drawing.Color.Transparent;
            this.Label44.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label44.Location = new System.Drawing.Point(33, 4);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(101, 22);
            this.Label44.TabIndex = 112;
            this.Label44.Text = "Ringtone 2:";
            this.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox43
            // 
            this.PictureBox43.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox43.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox43.Image")));
            this.PictureBox43.Location = new System.Drawing.Point(3, 3);
            this.PictureBox43.Name = "PictureBox43";
            this.PictureBox43.Size = new System.Drawing.Size(24, 24);
            this.PictureBox43.TabIndex = 1;
            this.PictureBox43.TabStop = false;
            // 
            // GroupBox45
            // 
            this.GroupBox45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox45.Controls.Add(this.Button139);
            this.GroupBox45.Controls.Add(this.Button140);
            this.GroupBox45.Controls.Add(this.Button141);
            this.GroupBox45.Controls.Add(this.TextBox44);
            this.GroupBox45.Controls.Add(this.Label45);
            this.GroupBox45.Controls.Add(this.PictureBox44);
            this.GroupBox45.Location = new System.Drawing.Point(4, 3);
            this.GroupBox45.Name = "GroupBox45";
            this.GroupBox45.Size = new System.Drawing.Size(754, 30);
            this.GroupBox45.TabIndex = 46;
            // 
            // Button139
            // 
            this.Button139.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button139.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button139.DrawOnGlass = false;
            this.Button139.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button139.ForeColor = System.Drawing.Color.White;
            this.Button139.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button139.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button139.Location = new System.Drawing.Point(639, 3);
            this.Button139.Name = "Button139";
            this.Button139.Size = new System.Drawing.Size(36, 24);
            this.Button139.TabIndex = 115;
            this.Button139.Tag = "2";
            this.Button139.UseVisualStyleBackColor = false;
            // 
            // Button140
            // 
            this.Button140.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button140.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button140.DrawOnGlass = false;
            this.Button140.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button140.ForeColor = System.Drawing.Color.White;
            this.Button140.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button140.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button140.Location = new System.Drawing.Point(677, 3);
            this.Button140.Name = "Button140";
            this.Button140.Size = new System.Drawing.Size(36, 24);
            this.Button140.TabIndex = 114;
            this.Button140.Tag = "1";
            this.Button140.UseVisualStyleBackColor = false;
            // 
            // Button141
            // 
            this.Button141.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button141.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button141.DrawOnGlass = false;
            this.Button141.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button141.ForeColor = System.Drawing.Color.White;
            this.Button141.Image = ((System.Drawing.Image)(resources.GetObject("Button141.Image")));
            this.Button141.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button141.Location = new System.Drawing.Point(715, 3);
            this.Button141.Name = "Button141";
            this.Button141.Size = new System.Drawing.Size(36, 24);
            this.Button141.TabIndex = 113;
            this.Button141.Tag = "3";
            this.Button141.UseVisualStyleBackColor = false;
            // 
            // TextBox44
            // 
            this.TextBox44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox44.DrawOnGlass = false;
            this.TextBox44.ForeColor = System.Drawing.Color.White;
            this.TextBox44.Location = new System.Drawing.Point(140, 3);
            this.TextBox44.MaxLength = 32767;
            this.TextBox44.Multiline = false;
            this.TextBox44.Name = "TextBox44";
            this.TextBox44.ReadOnly = false;
            this.TextBox44.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox44.SelectedText = "";
            this.TextBox44.SelectionLength = 0;
            this.TextBox44.SelectionStart = 0;
            this.TextBox44.Size = new System.Drawing.Size(495, 24);
            this.TextBox44.TabIndex = 1;
            this.TextBox44.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox44.UseSystemPasswordChar = false;
            this.TextBox44.WordWrap = true;
            // 
            // Label45
            // 
            this.Label45.BackColor = System.Drawing.Color.Transparent;
            this.Label45.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(33, 4);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(101, 22);
            this.Label45.TabIndex = 112;
            this.Label45.Text = "Ringtone 1:";
            this.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox44
            // 
            this.PictureBox44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox44.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox44.Image")));
            this.PictureBox44.Location = new System.Drawing.Point(3, 3);
            this.PictureBox44.Name = "PictureBox44";
            this.PictureBox44.Size = new System.Drawing.Size(24, 24);
            this.PictureBox44.TabIndex = 1;
            this.PictureBox44.TabStop = false;
            // 
            // TabPage10
            // 
            this.TabPage10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage10.Controls.Add(this.GroupBox70);
            this.TabPage10.Controls.Add(this.GroupBox71);
            this.TabPage10.Controls.Add(this.GroupBox72);
            this.TabPage10.Controls.Add(this.GroupBox73);
            this.TabPage10.Controls.Add(this.GroupBox74);
            this.TabPage10.Controls.Add(this.GroupBox75);
            this.TabPage10.Location = new System.Drawing.Point(144, 4);
            this.TabPage10.Name = "TabPage10";
            this.TabPage10.Size = new System.Drawing.Size(761, 368);
            this.TabPage10.TabIndex = 9;
            this.TabPage10.Text = "Speech recognition";
            // 
            // GroupBox70
            // 
            this.GroupBox70.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox70.Controls.Add(this.Button214);
            this.GroupBox70.Controls.Add(this.Button215);
            this.GroupBox70.Controls.Add(this.Button216);
            this.GroupBox70.Controls.Add(this.TextBox69);
            this.GroupBox70.Controls.Add(this.Label70);
            this.GroupBox70.Controls.Add(this.PictureBox69);
            this.GroupBox70.Location = new System.Drawing.Point(4, 71);
            this.GroupBox70.Name = "GroupBox70";
            this.GroupBox70.Size = new System.Drawing.Size(754, 30);
            this.GroupBox70.TabIndex = 131;
            // 
            // Button214
            // 
            this.Button214.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button214.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button214.DrawOnGlass = false;
            this.Button214.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button214.ForeColor = System.Drawing.Color.White;
            this.Button214.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button214.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button214.Location = new System.Drawing.Point(639, 3);
            this.Button214.Name = "Button214";
            this.Button214.Size = new System.Drawing.Size(36, 24);
            this.Button214.TabIndex = 115;
            this.Button214.Tag = "2";
            this.Button214.UseVisualStyleBackColor = false;
            // 
            // Button215
            // 
            this.Button215.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button215.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button215.DrawOnGlass = false;
            this.Button215.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button215.ForeColor = System.Drawing.Color.White;
            this.Button215.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button215.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button215.Location = new System.Drawing.Point(677, 3);
            this.Button215.Name = "Button215";
            this.Button215.Size = new System.Drawing.Size(36, 24);
            this.Button215.TabIndex = 114;
            this.Button215.Tag = "1";
            this.Button215.UseVisualStyleBackColor = false;
            // 
            // Button216
            // 
            this.Button216.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button216.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button216.DrawOnGlass = false;
            this.Button216.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button216.ForeColor = System.Drawing.Color.White;
            this.Button216.Image = ((System.Drawing.Image)(resources.GetObject("Button216.Image")));
            this.Button216.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button216.Location = new System.Drawing.Point(715, 3);
            this.Button216.Name = "Button216";
            this.Button216.Size = new System.Drawing.Size(36, 24);
            this.Button216.TabIndex = 113;
            this.Button216.Tag = "3";
            this.Button216.UseVisualStyleBackColor = false;
            // 
            // TextBox69
            // 
            this.TextBox69.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox69.DrawOnGlass = false;
            this.TextBox69.ForeColor = System.Drawing.Color.White;
            this.TextBox69.Location = new System.Drawing.Point(203, 3);
            this.TextBox69.MaxLength = 32767;
            this.TextBox69.Multiline = false;
            this.TextBox69.Name = "TextBox69";
            this.TextBox69.ReadOnly = false;
            this.TextBox69.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox69.SelectedText = "";
            this.TextBox69.SelectionLength = 0;
            this.TextBox69.SelectionStart = 0;
            this.TextBox69.Size = new System.Drawing.Size(432, 24);
            this.TextBox69.TabIndex = 1;
            this.TextBox69.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox69.UseSystemPasswordChar = false;
            this.TextBox69.WordWrap = true;
            // 
            // Label70
            // 
            this.Label70.BackColor = System.Drawing.Color.Transparent;
            this.Label70.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label70.Location = new System.Drawing.Point(33, 4);
            this.Label70.Name = "Label70";
            this.Label70.Size = new System.Drawing.Size(164, 22);
            this.Label70.TabIndex = 112;
            this.Label70.Text = "Misrecognition:";
            this.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox69
            // 
            this.PictureBox69.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox69.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox69.Image")));
            this.PictureBox69.Location = new System.Drawing.Point(3, 3);
            this.PictureBox69.Name = "PictureBox69";
            this.PictureBox69.Size = new System.Drawing.Size(24, 24);
            this.PictureBox69.TabIndex = 1;
            this.PictureBox69.TabStop = false;
            // 
            // GroupBox71
            // 
            this.GroupBox71.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox71.Controls.Add(this.Button217);
            this.GroupBox71.Controls.Add(this.Button218);
            this.GroupBox71.Controls.Add(this.Button219);
            this.GroupBox71.Controls.Add(this.TextBox70);
            this.GroupBox71.Controls.Add(this.Label71);
            this.GroupBox71.Controls.Add(this.PictureBox70);
            this.GroupBox71.Location = new System.Drawing.Point(4, 3);
            this.GroupBox71.Name = "GroupBox71";
            this.GroupBox71.Size = new System.Drawing.Size(754, 30);
            this.GroupBox71.TabIndex = 130;
            // 
            // Button217
            // 
            this.Button217.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button217.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button217.DrawOnGlass = false;
            this.Button217.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button217.ForeColor = System.Drawing.Color.White;
            this.Button217.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button217.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button217.Location = new System.Drawing.Point(639, 3);
            this.Button217.Name = "Button217";
            this.Button217.Size = new System.Drawing.Size(36, 24);
            this.Button217.TabIndex = 115;
            this.Button217.Tag = "2";
            this.Button217.UseVisualStyleBackColor = false;
            // 
            // Button218
            // 
            this.Button218.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button218.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button218.DrawOnGlass = false;
            this.Button218.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button218.ForeColor = System.Drawing.Color.White;
            this.Button218.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button218.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button218.Location = new System.Drawing.Point(677, 3);
            this.Button218.Name = "Button218";
            this.Button218.Size = new System.Drawing.Size(36, 24);
            this.Button218.TabIndex = 114;
            this.Button218.Tag = "1";
            this.Button218.UseVisualStyleBackColor = false;
            // 
            // Button219
            // 
            this.Button219.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button219.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button219.DrawOnGlass = false;
            this.Button219.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button219.ForeColor = System.Drawing.Color.White;
            this.Button219.Image = ((System.Drawing.Image)(resources.GetObject("Button219.Image")));
            this.Button219.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button219.Location = new System.Drawing.Point(715, 3);
            this.Button219.Name = "Button219";
            this.Button219.Size = new System.Drawing.Size(36, 24);
            this.Button219.TabIndex = 113;
            this.Button219.Tag = "3";
            this.Button219.UseVisualStyleBackColor = false;
            // 
            // TextBox70
            // 
            this.TextBox70.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox70.DrawOnGlass = false;
            this.TextBox70.ForeColor = System.Drawing.Color.White;
            this.TextBox70.Location = new System.Drawing.Point(203, 3);
            this.TextBox70.MaxLength = 32767;
            this.TextBox70.Multiline = false;
            this.TextBox70.Name = "TextBox70";
            this.TextBox70.ReadOnly = false;
            this.TextBox70.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox70.SelectedText = "";
            this.TextBox70.SelectionLength = 0;
            this.TextBox70.SelectionStart = 0;
            this.TextBox70.Size = new System.Drawing.Size(432, 24);
            this.TextBox70.TabIndex = 1;
            this.TextBox70.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox70.UseSystemPasswordChar = false;
            this.TextBox70.WordWrap = true;
            // 
            // Label71
            // 
            this.Label71.BackColor = System.Drawing.Color.Transparent;
            this.Label71.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label71.Location = new System.Drawing.Point(33, 4);
            this.Label71.Name = "Label71";
            this.Label71.Size = new System.Drawing.Size(164, 22);
            this.Label71.TabIndex = 112;
            this.Label71.Text = "Disambiguation numbers:";
            this.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox70
            // 
            this.PictureBox70.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox70.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox70.Image")));
            this.PictureBox70.Location = new System.Drawing.Point(3, 3);
            this.PictureBox70.Name = "PictureBox70";
            this.PictureBox70.Size = new System.Drawing.Size(24, 24);
            this.PictureBox70.TabIndex = 1;
            this.PictureBox70.TabStop = false;
            // 
            // GroupBox72
            // 
            this.GroupBox72.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox72.Controls.Add(this.Button220);
            this.GroupBox72.Controls.Add(this.Button221);
            this.GroupBox72.Controls.Add(this.Button222);
            this.GroupBox72.Controls.Add(this.TextBox71);
            this.GroupBox72.Controls.Add(this.Label72);
            this.GroupBox72.Controls.Add(this.PictureBox71);
            this.GroupBox72.Location = new System.Drawing.Point(4, 173);
            this.GroupBox72.Name = "GroupBox72";
            this.GroupBox72.Size = new System.Drawing.Size(754, 30);
            this.GroupBox72.TabIndex = 129;
            // 
            // Button220
            // 
            this.Button220.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button220.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button220.DrawOnGlass = false;
            this.Button220.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button220.ForeColor = System.Drawing.Color.White;
            this.Button220.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button220.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button220.Location = new System.Drawing.Point(639, 3);
            this.Button220.Name = "Button220";
            this.Button220.Size = new System.Drawing.Size(36, 24);
            this.Button220.TabIndex = 115;
            this.Button220.Tag = "2";
            this.Button220.UseVisualStyleBackColor = false;
            // 
            // Button221
            // 
            this.Button221.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button221.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button221.DrawOnGlass = false;
            this.Button221.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button221.ForeColor = System.Drawing.Color.White;
            this.Button221.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button221.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button221.Location = new System.Drawing.Point(677, 3);
            this.Button221.Name = "Button221";
            this.Button221.Size = new System.Drawing.Size(36, 24);
            this.Button221.TabIndex = 114;
            this.Button221.Tag = "1";
            this.Button221.UseVisualStyleBackColor = false;
            // 
            // Button222
            // 
            this.Button222.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button222.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button222.DrawOnGlass = false;
            this.Button222.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button222.ForeColor = System.Drawing.Color.White;
            this.Button222.Image = ((System.Drawing.Image)(resources.GetObject("Button222.Image")));
            this.Button222.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button222.Location = new System.Drawing.Point(715, 3);
            this.Button222.Name = "Button222";
            this.Button222.Size = new System.Drawing.Size(36, 24);
            this.Button222.TabIndex = 113;
            this.Button222.Tag = "3";
            this.Button222.UseVisualStyleBackColor = false;
            // 
            // TextBox71
            // 
            this.TextBox71.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox71.DrawOnGlass = false;
            this.TextBox71.ForeColor = System.Drawing.Color.White;
            this.TextBox71.Location = new System.Drawing.Point(203, 3);
            this.TextBox71.MaxLength = 32767;
            this.TextBox71.Multiline = false;
            this.TextBox71.Name = "TextBox71";
            this.TextBox71.ReadOnly = false;
            this.TextBox71.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox71.SelectedText = "";
            this.TextBox71.SelectionLength = 0;
            this.TextBox71.SelectionStart = 0;
            this.TextBox71.Size = new System.Drawing.Size(432, 24);
            this.TextBox71.TabIndex = 1;
            this.TextBox71.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox71.UseSystemPasswordChar = false;
            this.TextBox71.WordWrap = true;
            // 
            // Label72
            // 
            this.Label72.BackColor = System.Drawing.Color.Transparent;
            this.Label72.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label72.Location = new System.Drawing.Point(33, 4);
            this.Label72.Name = "Label72";
            this.Label72.Size = new System.Drawing.Size(164, 22);
            this.Label72.TabIndex = 112;
            this.Label72.Text = "Sleep:";
            this.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox71
            // 
            this.PictureBox71.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox71.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox71.Image")));
            this.PictureBox71.Location = new System.Drawing.Point(3, 3);
            this.PictureBox71.Name = "PictureBox71";
            this.PictureBox71.Size = new System.Drawing.Size(24, 24);
            this.PictureBox71.TabIndex = 1;
            this.PictureBox71.TabStop = false;
            // 
            // GroupBox73
            // 
            this.GroupBox73.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox73.Controls.Add(this.Button223);
            this.GroupBox73.Controls.Add(this.Button224);
            this.GroupBox73.Controls.Add(this.Button225);
            this.GroupBox73.Controls.Add(this.TextBox72);
            this.GroupBox73.Controls.Add(this.Label73);
            this.GroupBox73.Controls.Add(this.PictureBox72);
            this.GroupBox73.Location = new System.Drawing.Point(4, 139);
            this.GroupBox73.Name = "GroupBox73";
            this.GroupBox73.Size = new System.Drawing.Size(754, 30);
            this.GroupBox73.TabIndex = 128;
            // 
            // Button223
            // 
            this.Button223.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button223.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button223.DrawOnGlass = false;
            this.Button223.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button223.ForeColor = System.Drawing.Color.White;
            this.Button223.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button223.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button223.Location = new System.Drawing.Point(639, 3);
            this.Button223.Name = "Button223";
            this.Button223.Size = new System.Drawing.Size(36, 24);
            this.Button223.TabIndex = 115;
            this.Button223.Tag = "2";
            this.Button223.UseVisualStyleBackColor = false;
            // 
            // Button224
            // 
            this.Button224.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button224.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button224.DrawOnGlass = false;
            this.Button224.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button224.ForeColor = System.Drawing.Color.White;
            this.Button224.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button224.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button224.Location = new System.Drawing.Point(677, 3);
            this.Button224.Name = "Button224";
            this.Button224.Size = new System.Drawing.Size(36, 24);
            this.Button224.TabIndex = 114;
            this.Button224.Tag = "1";
            this.Button224.UseVisualStyleBackColor = false;
            // 
            // Button225
            // 
            this.Button225.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button225.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button225.DrawOnGlass = false;
            this.Button225.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button225.ForeColor = System.Drawing.Color.White;
            this.Button225.Image = ((System.Drawing.Image)(resources.GetObject("Button225.Image")));
            this.Button225.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button225.Location = new System.Drawing.Point(715, 3);
            this.Button225.Name = "Button225";
            this.Button225.Size = new System.Drawing.Size(36, 24);
            this.Button225.TabIndex = 113;
            this.Button225.Tag = "3";
            this.Button225.UseVisualStyleBackColor = false;
            // 
            // TextBox72
            // 
            this.TextBox72.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox72.DrawOnGlass = false;
            this.TextBox72.ForeColor = System.Drawing.Color.White;
            this.TextBox72.Location = new System.Drawing.Point(203, 3);
            this.TextBox72.MaxLength = 32767;
            this.TextBox72.Multiline = false;
            this.TextBox72.Name = "TextBox72";
            this.TextBox72.ReadOnly = false;
            this.TextBox72.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox72.SelectedText = "";
            this.TextBox72.SelectionLength = 0;
            this.TextBox72.SelectionStart = 0;
            this.TextBox72.Size = new System.Drawing.Size(432, 24);
            this.TextBox72.TabIndex = 1;
            this.TextBox72.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox72.UseSystemPasswordChar = false;
            this.TextBox72.WordWrap = true;
            // 
            // Label73
            // 
            this.Label73.BackColor = System.Drawing.Color.Transparent;
            this.Label73.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label73.Location = new System.Drawing.Point(33, 4);
            this.Label73.Name = "Label73";
            this.Label73.Size = new System.Drawing.Size(164, 22);
            this.Label73.TabIndex = 112;
            this.Label73.Text = "On:";
            this.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox72
            // 
            this.PictureBox72.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox72.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox72.Image")));
            this.PictureBox72.Location = new System.Drawing.Point(3, 3);
            this.PictureBox72.Name = "PictureBox72";
            this.PictureBox72.Size = new System.Drawing.Size(24, 24);
            this.PictureBox72.TabIndex = 1;
            this.PictureBox72.TabStop = false;
            // 
            // GroupBox74
            // 
            this.GroupBox74.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox74.Controls.Add(this.Button226);
            this.GroupBox74.Controls.Add(this.Button227);
            this.GroupBox74.Controls.Add(this.Button228);
            this.GroupBox74.Controls.Add(this.TextBox73);
            this.GroupBox74.Controls.Add(this.Label74);
            this.GroupBox74.Controls.Add(this.PictureBox73);
            this.GroupBox74.Location = new System.Drawing.Point(4, 105);
            this.GroupBox74.Name = "GroupBox74";
            this.GroupBox74.Size = new System.Drawing.Size(754, 30);
            this.GroupBox74.TabIndex = 127;
            // 
            // Button226
            // 
            this.Button226.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button226.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button226.DrawOnGlass = false;
            this.Button226.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button226.ForeColor = System.Drawing.Color.White;
            this.Button226.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button226.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button226.Location = new System.Drawing.Point(639, 3);
            this.Button226.Name = "Button226";
            this.Button226.Size = new System.Drawing.Size(36, 24);
            this.Button226.TabIndex = 115;
            this.Button226.Tag = "2";
            this.Button226.UseVisualStyleBackColor = false;
            // 
            // Button227
            // 
            this.Button227.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button227.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button227.DrawOnGlass = false;
            this.Button227.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button227.ForeColor = System.Drawing.Color.White;
            this.Button227.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button227.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button227.Location = new System.Drawing.Point(677, 3);
            this.Button227.Name = "Button227";
            this.Button227.Size = new System.Drawing.Size(36, 24);
            this.Button227.TabIndex = 114;
            this.Button227.Tag = "1";
            this.Button227.UseVisualStyleBackColor = false;
            // 
            // Button228
            // 
            this.Button228.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button228.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button228.DrawOnGlass = false;
            this.Button228.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button228.ForeColor = System.Drawing.Color.White;
            this.Button228.Image = ((System.Drawing.Image)(resources.GetObject("Button228.Image")));
            this.Button228.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button228.Location = new System.Drawing.Point(715, 3);
            this.Button228.Name = "Button228";
            this.Button228.Size = new System.Drawing.Size(36, 24);
            this.Button228.TabIndex = 113;
            this.Button228.Tag = "3";
            this.Button228.UseVisualStyleBackColor = false;
            // 
            // TextBox73
            // 
            this.TextBox73.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox73.DrawOnGlass = false;
            this.TextBox73.ForeColor = System.Drawing.Color.White;
            this.TextBox73.Location = new System.Drawing.Point(203, 3);
            this.TextBox73.MaxLength = 32767;
            this.TextBox73.Multiline = false;
            this.TextBox73.Name = "TextBox73";
            this.TextBox73.ReadOnly = false;
            this.TextBox73.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox73.SelectedText = "";
            this.TextBox73.SelectionLength = 0;
            this.TextBox73.SelectionStart = 0;
            this.TextBox73.Size = new System.Drawing.Size(432, 24);
            this.TextBox73.TabIndex = 1;
            this.TextBox73.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox73.UseSystemPasswordChar = false;
            this.TextBox73.WordWrap = true;
            // 
            // Label74
            // 
            this.Label74.BackColor = System.Drawing.Color.Transparent;
            this.Label74.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label74.Location = new System.Drawing.Point(33, 4);
            this.Label74.Name = "Label74";
            this.Label74.Size = new System.Drawing.Size(164, 22);
            this.Label74.TabIndex = 112;
            this.Label74.Text = "Off:";
            this.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox73
            // 
            this.PictureBox73.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox73.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox73.Image")));
            this.PictureBox73.Location = new System.Drawing.Point(3, 3);
            this.PictureBox73.Name = "PictureBox73";
            this.PictureBox73.Size = new System.Drawing.Size(24, 24);
            this.PictureBox73.TabIndex = 1;
            this.PictureBox73.TabStop = false;
            // 
            // GroupBox75
            // 
            this.GroupBox75.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox75.Controls.Add(this.Button229);
            this.GroupBox75.Controls.Add(this.Button230);
            this.GroupBox75.Controls.Add(this.Button231);
            this.GroupBox75.Controls.Add(this.TextBox74);
            this.GroupBox75.Controls.Add(this.Label75);
            this.GroupBox75.Controls.Add(this.PictureBox74);
            this.GroupBox75.Location = new System.Drawing.Point(4, 37);
            this.GroupBox75.Name = "GroupBox75";
            this.GroupBox75.Size = new System.Drawing.Size(754, 30);
            this.GroupBox75.TabIndex = 126;
            // 
            // Button229
            // 
            this.Button229.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button229.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button229.DrawOnGlass = false;
            this.Button229.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button229.ForeColor = System.Drawing.Color.White;
            this.Button229.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button229.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button229.Location = new System.Drawing.Point(639, 3);
            this.Button229.Name = "Button229";
            this.Button229.Size = new System.Drawing.Size(36, 24);
            this.Button229.TabIndex = 115;
            this.Button229.Tag = "2";
            this.Button229.UseVisualStyleBackColor = false;
            // 
            // Button230
            // 
            this.Button230.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button230.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button230.DrawOnGlass = false;
            this.Button230.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button230.ForeColor = System.Drawing.Color.White;
            this.Button230.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button230.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button230.Location = new System.Drawing.Point(677, 3);
            this.Button230.Name = "Button230";
            this.Button230.Size = new System.Drawing.Size(36, 24);
            this.Button230.TabIndex = 114;
            this.Button230.Tag = "1";
            this.Button230.UseVisualStyleBackColor = false;
            // 
            // Button231
            // 
            this.Button231.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button231.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button231.DrawOnGlass = false;
            this.Button231.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button231.ForeColor = System.Drawing.Color.White;
            this.Button231.Image = ((System.Drawing.Image)(resources.GetObject("Button231.Image")));
            this.Button231.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button231.Location = new System.Drawing.Point(715, 3);
            this.Button231.Name = "Button231";
            this.Button231.Size = new System.Drawing.Size(36, 24);
            this.Button231.TabIndex = 113;
            this.Button231.Tag = "3";
            this.Button231.UseVisualStyleBackColor = false;
            // 
            // TextBox74
            // 
            this.TextBox74.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox74.DrawOnGlass = false;
            this.TextBox74.ForeColor = System.Drawing.Color.White;
            this.TextBox74.Location = new System.Drawing.Point(203, 3);
            this.TextBox74.MaxLength = 32767;
            this.TextBox74.Multiline = false;
            this.TextBox74.Name = "TextBox74";
            this.TextBox74.ReadOnly = false;
            this.TextBox74.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox74.SelectedText = "";
            this.TextBox74.SelectionLength = 0;
            this.TextBox74.SelectionStart = 0;
            this.TextBox74.Size = new System.Drawing.Size(432, 24);
            this.TextBox74.TabIndex = 1;
            this.TextBox74.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox74.UseSystemPasswordChar = false;
            this.TextBox74.WordWrap = true;
            // 
            // Label75
            // 
            this.Label75.BackColor = System.Drawing.Color.Transparent;
            this.Label75.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label75.Location = new System.Drawing.Point(33, 4);
            this.Label75.Name = "Label75";
            this.Label75.Size = new System.Drawing.Size(164, 22);
            this.Label75.TabIndex = 112;
            this.Label75.Text = "Disambiguation panels:";
            this.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox74
            // 
            this.PictureBox74.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox74.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox74.Image")));
            this.PictureBox74.Location = new System.Drawing.Point(3, 3);
            this.PictureBox74.Name = "PictureBox74";
            this.PictureBox74.Size = new System.Drawing.Size(24, 24);
            this.PictureBox74.TabIndex = 1;
            this.PictureBox74.TabStop = false;
            // 
            // TabPage12
            // 
            this.TabPage12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage12.Controls.Add(this.AlertBox3);
            this.TabPage12.Controls.Add(this.GroupBox81);
            this.TabPage12.Controls.Add(this.GroupBox82);
            this.TabPage12.Controls.Add(this.GroupBox83);
            this.TabPage12.Controls.Add(this.GroupBox84);
            this.TabPage12.Location = new System.Drawing.Point(144, 4);
            this.TabPage12.Name = "TabPage12";
            this.TabPage12.Size = new System.Drawing.Size(761, 368);
            this.TabPage12.TabIndex = 11;
            this.TabPage12.Text = "NetMeeting";
            // 
            // AlertBox3
            // 
            this.AlertBox3.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.AlertBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.AlertBox3.CenterText = false;
            this.AlertBox3.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.AlertBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AlertBox3.Image = null;
            this.AlertBox3.Location = new System.Drawing.Point(4, 141);
            this.AlertBox3.Name = "AlertBox3";
            this.AlertBox3.Size = new System.Drawing.Size(754, 22);
            this.AlertBox3.TabIndex = 131;
            this.AlertBox3.TabStop = false;
            this.AlertBox3.Text = "This application is installed by defaults in Windows XP and it has sounds entries" +
    ".";
            // 
            // GroupBox81
            // 
            this.GroupBox81.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox81.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox81.Controls.Add(this.Button247);
            this.GroupBox81.Controls.Add(this.Button248);
            this.GroupBox81.Controls.Add(this.Button249);
            this.GroupBox81.Controls.Add(this.TextBox80);
            this.GroupBox81.Controls.Add(this.Label81);
            this.GroupBox81.Controls.Add(this.PictureBox80);
            this.GroupBox81.Location = new System.Drawing.Point(4, 71);
            this.GroupBox81.Name = "GroupBox81";
            this.GroupBox81.Size = new System.Drawing.Size(754, 30);
            this.GroupBox81.TabIndex = 130;
            // 
            // Button247
            // 
            this.Button247.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button247.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button247.DrawOnGlass = false;
            this.Button247.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button247.ForeColor = System.Drawing.Color.White;
            this.Button247.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button247.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button247.Location = new System.Drawing.Point(639, 3);
            this.Button247.Name = "Button247";
            this.Button247.Size = new System.Drawing.Size(36, 24);
            this.Button247.TabIndex = 115;
            this.Button247.Tag = "2";
            this.Button247.UseVisualStyleBackColor = false;
            // 
            // Button248
            // 
            this.Button248.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button248.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button248.DrawOnGlass = false;
            this.Button248.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button248.ForeColor = System.Drawing.Color.White;
            this.Button248.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button248.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button248.Location = new System.Drawing.Point(677, 3);
            this.Button248.Name = "Button248";
            this.Button248.Size = new System.Drawing.Size(36, 24);
            this.Button248.TabIndex = 114;
            this.Button248.Tag = "1";
            this.Button248.UseVisualStyleBackColor = false;
            // 
            // Button249
            // 
            this.Button249.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button249.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button249.DrawOnGlass = false;
            this.Button249.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button249.ForeColor = System.Drawing.Color.White;
            this.Button249.Image = ((System.Drawing.Image)(resources.GetObject("Button249.Image")));
            this.Button249.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button249.Location = new System.Drawing.Point(715, 3);
            this.Button249.Name = "Button249";
            this.Button249.Size = new System.Drawing.Size(36, 24);
            this.Button249.TabIndex = 113;
            this.Button249.Tag = "3";
            this.Button249.UseVisualStyleBackColor = false;
            // 
            // TextBox80
            // 
            this.TextBox80.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox80.DrawOnGlass = false;
            this.TextBox80.ForeColor = System.Drawing.Color.White;
            this.TextBox80.Location = new System.Drawing.Point(190, 3);
            this.TextBox80.MaxLength = 32767;
            this.TextBox80.Multiline = false;
            this.TextBox80.Name = "TextBox80";
            this.TextBox80.ReadOnly = false;
            this.TextBox80.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox80.SelectedText = "";
            this.TextBox80.SelectionLength = 0;
            this.TextBox80.SelectionStart = 0;
            this.TextBox80.Size = new System.Drawing.Size(445, 24);
            this.TextBox80.TabIndex = 1;
            this.TextBox80.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox80.UseSystemPasswordChar = false;
            this.TextBox80.WordWrap = true;
            // 
            // Label81
            // 
            this.Label81.BackColor = System.Drawing.Color.Transparent;
            this.Label81.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label81.Location = new System.Drawing.Point(33, 4);
            this.Label81.Name = "Label81";
            this.Label81.Size = new System.Drawing.Size(151, 22);
            this.Label81.TabIndex = 112;
            this.Label81.Text = "Receive call:";
            this.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox80
            // 
            this.PictureBox80.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox80.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox80.Image")));
            this.PictureBox80.Location = new System.Drawing.Point(3, 3);
            this.PictureBox80.Name = "PictureBox80";
            this.PictureBox80.Size = new System.Drawing.Size(24, 24);
            this.PictureBox80.TabIndex = 1;
            this.PictureBox80.TabStop = false;
            // 
            // GroupBox82
            // 
            this.GroupBox82.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox82.Controls.Add(this.Button250);
            this.GroupBox82.Controls.Add(this.Button251);
            this.GroupBox82.Controls.Add(this.Button252);
            this.GroupBox82.Controls.Add(this.TextBox81);
            this.GroupBox82.Controls.Add(this.Label82);
            this.GroupBox82.Controls.Add(this.PictureBox81);
            this.GroupBox82.Location = new System.Drawing.Point(4, 105);
            this.GroupBox82.Name = "GroupBox82";
            this.GroupBox82.Size = new System.Drawing.Size(754, 30);
            this.GroupBox82.TabIndex = 129;
            // 
            // Button250
            // 
            this.Button250.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button250.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button250.DrawOnGlass = false;
            this.Button250.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button250.ForeColor = System.Drawing.Color.White;
            this.Button250.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button250.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button250.Location = new System.Drawing.Point(639, 3);
            this.Button250.Name = "Button250";
            this.Button250.Size = new System.Drawing.Size(36, 24);
            this.Button250.TabIndex = 115;
            this.Button250.Tag = "2";
            this.Button250.UseVisualStyleBackColor = false;
            // 
            // Button251
            // 
            this.Button251.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button251.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button251.DrawOnGlass = false;
            this.Button251.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button251.ForeColor = System.Drawing.Color.White;
            this.Button251.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button251.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button251.Location = new System.Drawing.Point(677, 3);
            this.Button251.Name = "Button251";
            this.Button251.Size = new System.Drawing.Size(36, 24);
            this.Button251.TabIndex = 114;
            this.Button251.Tag = "1";
            this.Button251.UseVisualStyleBackColor = false;
            // 
            // Button252
            // 
            this.Button252.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button252.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button252.DrawOnGlass = false;
            this.Button252.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button252.ForeColor = System.Drawing.Color.White;
            this.Button252.Image = ((System.Drawing.Image)(resources.GetObject("Button252.Image")));
            this.Button252.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button252.Location = new System.Drawing.Point(715, 3);
            this.Button252.Name = "Button252";
            this.Button252.Size = new System.Drawing.Size(36, 24);
            this.Button252.TabIndex = 113;
            this.Button252.Tag = "3";
            this.Button252.UseVisualStyleBackColor = false;
            // 
            // TextBox81
            // 
            this.TextBox81.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox81.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox81.DrawOnGlass = false;
            this.TextBox81.ForeColor = System.Drawing.Color.White;
            this.TextBox81.Location = new System.Drawing.Point(190, 3);
            this.TextBox81.MaxLength = 32767;
            this.TextBox81.Multiline = false;
            this.TextBox81.Name = "TextBox81";
            this.TextBox81.ReadOnly = false;
            this.TextBox81.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox81.SelectedText = "";
            this.TextBox81.SelectionLength = 0;
            this.TextBox81.SelectionStart = 0;
            this.TextBox81.Size = new System.Drawing.Size(445, 24);
            this.TextBox81.TabIndex = 1;
            this.TextBox81.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox81.UseSystemPasswordChar = false;
            this.TextBox81.WordWrap = true;
            // 
            // Label82
            // 
            this.Label82.BackColor = System.Drawing.Color.Transparent;
            this.Label82.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label82.Location = new System.Drawing.Point(33, 4);
            this.Label82.Name = "Label82";
            this.Label82.Size = new System.Drawing.Size(151, 22);
            this.Label82.TabIndex = 112;
            this.Label82.Text = "Receive request to join:";
            this.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox81
            // 
            this.PictureBox81.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox81.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox81.Image")));
            this.PictureBox81.Location = new System.Drawing.Point(3, 3);
            this.PictureBox81.Name = "PictureBox81";
            this.PictureBox81.Size = new System.Drawing.Size(24, 24);
            this.PictureBox81.TabIndex = 1;
            this.PictureBox81.TabStop = false;
            // 
            // GroupBox83
            // 
            this.GroupBox83.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox83.Controls.Add(this.Button253);
            this.GroupBox83.Controls.Add(this.Button254);
            this.GroupBox83.Controls.Add(this.Button255);
            this.GroupBox83.Controls.Add(this.TextBox82);
            this.GroupBox83.Controls.Add(this.Label83);
            this.GroupBox83.Controls.Add(this.PictureBox82);
            this.GroupBox83.Location = new System.Drawing.Point(4, 37);
            this.GroupBox83.Name = "GroupBox83";
            this.GroupBox83.Size = new System.Drawing.Size(754, 30);
            this.GroupBox83.TabIndex = 128;
            // 
            // Button253
            // 
            this.Button253.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button253.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button253.DrawOnGlass = false;
            this.Button253.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button253.ForeColor = System.Drawing.Color.White;
            this.Button253.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button253.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button253.Location = new System.Drawing.Point(639, 3);
            this.Button253.Name = "Button253";
            this.Button253.Size = new System.Drawing.Size(36, 24);
            this.Button253.TabIndex = 115;
            this.Button253.Tag = "2";
            this.Button253.UseVisualStyleBackColor = false;
            // 
            // Button254
            // 
            this.Button254.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button254.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button254.DrawOnGlass = false;
            this.Button254.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button254.ForeColor = System.Drawing.Color.White;
            this.Button254.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button254.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button254.Location = new System.Drawing.Point(677, 3);
            this.Button254.Name = "Button254";
            this.Button254.Size = new System.Drawing.Size(36, 24);
            this.Button254.TabIndex = 114;
            this.Button254.Tag = "1";
            this.Button254.UseVisualStyleBackColor = false;
            // 
            // Button255
            // 
            this.Button255.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button255.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button255.DrawOnGlass = false;
            this.Button255.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button255.ForeColor = System.Drawing.Color.White;
            this.Button255.Image = ((System.Drawing.Image)(resources.GetObject("Button255.Image")));
            this.Button255.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button255.Location = new System.Drawing.Point(715, 3);
            this.Button255.Name = "Button255";
            this.Button255.Size = new System.Drawing.Size(36, 24);
            this.Button255.TabIndex = 113;
            this.Button255.Tag = "3";
            this.Button255.UseVisualStyleBackColor = false;
            // 
            // TextBox82
            // 
            this.TextBox82.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox82.DrawOnGlass = false;
            this.TextBox82.ForeColor = System.Drawing.Color.White;
            this.TextBox82.Location = new System.Drawing.Point(190, 3);
            this.TextBox82.MaxLength = 32767;
            this.TextBox82.Multiline = false;
            this.TextBox82.Name = "TextBox82";
            this.TextBox82.ReadOnly = false;
            this.TextBox82.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox82.SelectedText = "";
            this.TextBox82.SelectionLength = 0;
            this.TextBox82.SelectionStart = 0;
            this.TextBox82.Size = new System.Drawing.Size(445, 24);
            this.TextBox82.TabIndex = 1;
            this.TextBox82.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox82.UseSystemPasswordChar = false;
            this.TextBox82.WordWrap = true;
            // 
            // Label83
            // 
            this.Label83.BackColor = System.Drawing.Color.Transparent;
            this.Label83.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label83.Location = new System.Drawing.Point(33, 4);
            this.Label83.Name = "Label83";
            this.Label83.Size = new System.Drawing.Size(151, 22);
            this.Label83.TabIndex = 112;
            this.Label83.Text = "Person leaves:";
            this.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox82
            // 
            this.PictureBox82.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox82.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox82.Image")));
            this.PictureBox82.Location = new System.Drawing.Point(3, 3);
            this.PictureBox82.Name = "PictureBox82";
            this.PictureBox82.Size = new System.Drawing.Size(24, 24);
            this.PictureBox82.TabIndex = 1;
            this.PictureBox82.TabStop = false;
            // 
            // GroupBox84
            // 
            this.GroupBox84.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox84.Controls.Add(this.Button256);
            this.GroupBox84.Controls.Add(this.Button257);
            this.GroupBox84.Controls.Add(this.Button258);
            this.GroupBox84.Controls.Add(this.TextBox83);
            this.GroupBox84.Controls.Add(this.Label84);
            this.GroupBox84.Controls.Add(this.PictureBox83);
            this.GroupBox84.Location = new System.Drawing.Point(4, 3);
            this.GroupBox84.Name = "GroupBox84";
            this.GroupBox84.Size = new System.Drawing.Size(754, 30);
            this.GroupBox84.TabIndex = 127;
            // 
            // Button256
            // 
            this.Button256.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button256.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button256.DrawOnGlass = false;
            this.Button256.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button256.ForeColor = System.Drawing.Color.White;
            this.Button256.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button256.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button256.Location = new System.Drawing.Point(639, 3);
            this.Button256.Name = "Button256";
            this.Button256.Size = new System.Drawing.Size(36, 24);
            this.Button256.TabIndex = 115;
            this.Button256.Tag = "2";
            this.Button256.UseVisualStyleBackColor = false;
            // 
            // Button257
            // 
            this.Button257.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button257.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button257.DrawOnGlass = false;
            this.Button257.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button257.ForeColor = System.Drawing.Color.White;
            this.Button257.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button257.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button257.Location = new System.Drawing.Point(677, 3);
            this.Button257.Name = "Button257";
            this.Button257.Size = new System.Drawing.Size(36, 24);
            this.Button257.TabIndex = 114;
            this.Button257.Tag = "1";
            this.Button257.UseVisualStyleBackColor = false;
            // 
            // Button258
            // 
            this.Button258.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button258.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button258.DrawOnGlass = false;
            this.Button258.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button258.ForeColor = System.Drawing.Color.White;
            this.Button258.Image = ((System.Drawing.Image)(resources.GetObject("Button258.Image")));
            this.Button258.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button258.Location = new System.Drawing.Point(715, 3);
            this.Button258.Name = "Button258";
            this.Button258.Size = new System.Drawing.Size(36, 24);
            this.Button258.TabIndex = 113;
            this.Button258.Tag = "3";
            this.Button258.UseVisualStyleBackColor = false;
            // 
            // TextBox83
            // 
            this.TextBox83.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox83.DrawOnGlass = false;
            this.TextBox83.ForeColor = System.Drawing.Color.White;
            this.TextBox83.Location = new System.Drawing.Point(190, 3);
            this.TextBox83.MaxLength = 32767;
            this.TextBox83.Multiline = false;
            this.TextBox83.Name = "TextBox83";
            this.TextBox83.ReadOnly = false;
            this.TextBox83.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox83.SelectedText = "";
            this.TextBox83.SelectionLength = 0;
            this.TextBox83.SelectionStart = 0;
            this.TextBox83.Size = new System.Drawing.Size(445, 24);
            this.TextBox83.TabIndex = 1;
            this.TextBox83.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox83.UseSystemPasswordChar = false;
            this.TextBox83.WordWrap = true;
            // 
            // Label84
            // 
            this.Label84.BackColor = System.Drawing.Color.Transparent;
            this.Label84.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label84.Location = new System.Drawing.Point(33, 4);
            this.Label84.Name = "Label84";
            this.Label84.Size = new System.Drawing.Size(151, 22);
            this.Label84.TabIndex = 112;
            this.Label84.Text = "Person joins:";
            this.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox83
            // 
            this.PictureBox83.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox83.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox83.Image")));
            this.PictureBox83.Location = new System.Drawing.Point(3, 3);
            this.PictureBox83.Name = "PictureBox83";
            this.PictureBox83.Size = new System.Drawing.Size(24, 24);
            this.PictureBox83.TabIndex = 1;
            this.PictureBox83.TabStop = false;
            // 
            // TabPage9
            // 
            this.TabPage9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage9.Controls.Add(this.GroupBox68);
            this.TabPage9.Location = new System.Drawing.Point(144, 4);
            this.TabPage9.Name = "TabPage9";
            this.TabPage9.Size = new System.Drawing.Size(761, 368);
            this.TabPage9.TabIndex = 8;
            this.TabPage9.Text = "Others";
            // 
            // GroupBox68
            // 
            this.GroupBox68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox68.Controls.Add(this.Button208);
            this.GroupBox68.Controls.Add(this.Button209);
            this.GroupBox68.Controls.Add(this.Button210);
            this.GroupBox68.Controls.Add(this.TextBox67);
            this.GroupBox68.Controls.Add(this.Label68);
            this.GroupBox68.Controls.Add(this.PictureBox67);
            this.GroupBox68.Location = new System.Drawing.Point(4, 3);
            this.GroupBox68.Name = "GroupBox68";
            this.GroupBox68.Size = new System.Drawing.Size(754, 30);
            this.GroupBox68.TabIndex = 125;
            // 
            // Button208
            // 
            this.Button208.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button208.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button208.DrawOnGlass = false;
            this.Button208.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button208.ForeColor = System.Drawing.Color.White;
            this.Button208.Image = global::WinPaletter.Properties.Resources.Sound_Stop;
            this.Button208.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(3)))), ((int)(((byte)(34)))));
            this.Button208.Location = new System.Drawing.Point(639, 3);
            this.Button208.Name = "Button208";
            this.Button208.Size = new System.Drawing.Size(36, 24);
            this.Button208.TabIndex = 115;
            this.Button208.Tag = "2";
            this.Button208.UseVisualStyleBackColor = false;
            // 
            // Button209
            // 
            this.Button209.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button209.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button209.DrawOnGlass = false;
            this.Button209.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button209.ForeColor = System.Drawing.Color.White;
            this.Button209.Image = global::WinPaletter.Properties.Resources.Sound_Play;
            this.Button209.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(45)))), ((int)(((byte)(77)))));
            this.Button209.Location = new System.Drawing.Point(677, 3);
            this.Button209.Name = "Button209";
            this.Button209.Size = new System.Drawing.Size(36, 24);
            this.Button209.TabIndex = 114;
            this.Button209.Tag = "1";
            this.Button209.UseVisualStyleBackColor = false;
            // 
            // Button210
            // 
            this.Button210.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button210.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button210.DrawOnGlass = false;
            this.Button210.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button210.ForeColor = System.Drawing.Color.White;
            this.Button210.Image = ((System.Drawing.Image)(resources.GetObject("Button210.Image")));
            this.Button210.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(125)))), ((int)(((byte)(25)))));
            this.Button210.Location = new System.Drawing.Point(715, 3);
            this.Button210.Name = "Button210";
            this.Button210.Size = new System.Drawing.Size(36, 24);
            this.Button210.TabIndex = 113;
            this.Button210.Tag = "3";
            this.Button210.UseVisualStyleBackColor = false;
            // 
            // TextBox67
            // 
            this.TextBox67.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TextBox67.DrawOnGlass = false;
            this.TextBox67.ForeColor = System.Drawing.Color.White;
            this.TextBox67.Location = new System.Drawing.Point(140, 3);
            this.TextBox67.MaxLength = 32767;
            this.TextBox67.Multiline = false;
            this.TextBox67.Name = "TextBox67";
            this.TextBox67.ReadOnly = false;
            this.TextBox67.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.TextBox67.SelectedText = "";
            this.TextBox67.SelectionLength = 0;
            this.TextBox67.SelectionStart = 0;
            this.TextBox67.Size = new System.Drawing.Size(495, 24);
            this.TextBox67.TabIndex = 1;
            this.TextBox67.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox67.UseSystemPasswordChar = false;
            this.TextBox67.WordWrap = true;
            // 
            // Label68
            // 
            this.Label68.BackColor = System.Drawing.Color.Transparent;
            this.Label68.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label68.Location = new System.Drawing.Point(33, 4);
            this.Label68.Name = "Label68";
            this.Label68.Size = new System.Drawing.Size(101, 22);
            this.Label68.TabIndex = 112;
            this.Label68.Text = "CC select:";
            this.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox67
            // 
            this.PictureBox67.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox67.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox67.Image")));
            this.PictureBox67.Location = new System.Drawing.Point(3, 3);
            this.PictureBox67.Name = "PictureBox67";
            this.PictureBox67.Size = new System.Drawing.Size(24, 24);
            this.PictureBox67.TabIndex = 1;
            this.PictureBox67.TabStop = false;
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox12.Controls.Add(this.Button259);
            this.GroupBox12.Controls.Add(this.Button9);
            this.GroupBox12.Controls.Add(this.Label12);
            this.GroupBox12.Controls.Add(this.Button11);
            this.GroupBox12.Controls.Add(this.Button12);
            this.GroupBox12.Controls.Add(this.SoundsEnabled);
            this.GroupBox12.Controls.Add(this.checker_img);
            this.GroupBox12.Location = new System.Drawing.Point(12, 12);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(909, 39);
            this.GroupBox12.TabIndex = 202;
            // 
            // Button259
            // 
            this.Button259.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button259.DrawOnGlass = false;
            this.Button259.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button259.ForeColor = System.Drawing.Color.White;
            this.Button259.Image = ((System.Drawing.Image)(resources.GetObject("Button259.Image")));
            this.Button259.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button259.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(111)))), ((int)(((byte)(91)))));
            this.Button259.Location = new System.Drawing.Point(223, 5);
            this.Button259.Name = "Button259";
            this.Button259.Size = new System.Drawing.Size(144, 29);
            this.Button259.TabIndex = 113;
            this.Button259.Text = "Classic .theme file";
            this.Button259.UseVisualStyleBackColor = false;
            this.Button259.Click += new System.EventHandler(this.Button259_Click);
            // 
            // Button9
            // 
            this.Button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button9.DrawOnGlass = false;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button9.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(134)))), ((int)(((byte)(117)))));
            this.Button9.Location = new System.Drawing.Point(370, 5);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(126, 29);
            this.Button9.TabIndex = 112;
            this.Button9.Text = "Current applied";
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(4, 4);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(75, 31);
            this.Label12.TabIndex = 111;
            this.Label12.Text = "Open from:";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button11
            // 
            this.Button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button11.DrawOnGlass = false;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = ((System.Drawing.Image)(resources.GetObject("Button11.Image")));
            this.Button11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button11.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(122)))), ((int)(((byte)(131)))));
            this.Button11.Location = new System.Drawing.Point(85, 5);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(135, 29);
            this.Button11.TabIndex = 110;
            this.Button11.Text = "WinPaletter theme";
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button12
            // 
            this.Button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button12.DrawOnGlass = false;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = null;
            this.Button12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button12.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(119)))));
            this.Button12.Location = new System.Drawing.Point(499, 5);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(135, 29);
            this.Button12.TabIndex = 108;
            this.Button12.Text = "Default Windows";
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // SoundsEnabled
            // 
            this.SoundsEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SoundsEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.SoundsEnabled.Checked = false;
            this.SoundsEnabled.DarkLight_Toggler = false;
            this.SoundsEnabled.Location = new System.Drawing.Point(864, 9);
            this.SoundsEnabled.Name = "SoundsEnabled";
            this.SoundsEnabled.Size = new System.Drawing.Size(40, 20);
            this.SoundsEnabled.TabIndex = 85;
            this.SoundsEnabled.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.ScrSvrEnabled_CheckedChanged);
            // 
            // checker_img
            // 
            this.checker_img.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checker_img.Image = global::WinPaletter.Properties.Resources.checker_disabled;
            this.checker_img.Location = new System.Drawing.Point(823, 4);
            this.checker_img.Name = "checker_img";
            this.checker_img.Size = new System.Drawing.Size(35, 31);
            this.checker_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.checker_img.TabIndex = 83;
            this.checker_img.TabStop = false;
            // 
            // Sounds_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(933, 571);
            this.Controls.Add(this.alertBox1);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.GroupBox12);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Sounds_Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sounds";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Sounds_Editor_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sounds_Editor_FormClosing);
            this.Load += new System.EventHandler(this.Sounds_Editor_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.groupBox88.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox92)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox87)).EndInit();
            this.GroupBox85.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox84)).EndInit();
            this.GroupBox65.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox64)).EndInit();
            this.GroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox90)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox91)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox89)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox88)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.GroupBox67.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox66)).EndInit();
            this.GroupBox69.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox68)).EndInit();
            this.GroupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            this.GroupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            this.GroupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            this.GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            this.GroupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.GroupBox55.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox54)).EndInit();
            this.GroupBox54.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox53)).EndInit();
            this.GroupBox18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).EndInit();
            this.GroupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).EndInit();
            this.GroupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).EndInit();
            this.GroupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).EndInit();
            this.GroupBox16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).EndInit();
            this.GroupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            this.TabPage7.ResumeLayout(false);
            this.groupBox87.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox94)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox86)).EndInit();
            this.GroupBox86.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox93)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox85)).EndInit();
            this.GroupBox53.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox52)).EndInit();
            this.GroupBox51.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox50)).EndInit();
            this.GroupBox50.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox49)).EndInit();
            this.GroupBox49.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox48)).EndInit();
            this.GroupBox48.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox47)).EndInit();
            this.GroupBox47.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox46)).EndInit();
            this.GroupBox46.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).EndInit();
            this.TabPage11.ResumeLayout(false);
            this.GroupBox79.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox78)).EndInit();
            this.GroupBox80.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox79)).EndInit();
            this.GroupBox78.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox77)).EndInit();
            this.GroupBox77.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox76)).EndInit();
            this.GroupBox52.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox51)).EndInit();
            this.TabPage8.ResumeLayout(false);
            this.GroupBox76.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox75)).EndInit();
            this.GroupBox64.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox63)).EndInit();
            this.GroupBox57.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox56)).EndInit();
            this.GroupBox58.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox57)).EndInit();
            this.GroupBox59.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox58)).EndInit();
            this.GroupBox60.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox59)).EndInit();
            this.GroupBox61.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox60)).EndInit();
            this.GroupBox62.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox61)).EndInit();
            this.GroupBox63.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox62)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.GroupBox66.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox65)).EndInit();
            this.GroupBox56.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox55)).EndInit();
            this.GroupBox25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox24)).EndInit();
            this.GroupBox19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox18)).EndInit();
            this.GroupBox20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox19)).EndInit();
            this.GroupBox21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox20)).EndInit();
            this.GroupBox22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox21)).EndInit();
            this.GroupBox23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).EndInit();
            this.GroupBox24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox23)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.GroupBox33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox32)).EndInit();
            this.GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox33)).EndInit();
            this.GroupBox35.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox34)).EndInit();
            this.GroupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).EndInit();
            this.GroupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox26)).EndInit();
            this.GroupBox28.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox27)).EndInit();
            this.GroupBox29.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).EndInit();
            this.GroupBox30.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox29)).EndInit();
            this.GroupBox31.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox30)).EndInit();
            this.GroupBox32.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox31)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.GroupBox36.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox35)).EndInit();
            this.GroupBox37.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox36)).EndInit();
            this.GroupBox38.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox37)).EndInit();
            this.GroupBox39.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox38)).EndInit();
            this.GroupBox40.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox39)).EndInit();
            this.GroupBox41.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).EndInit();
            this.GroupBox42.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            this.GroupBox43.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox42)).EndInit();
            this.GroupBox44.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox43)).EndInit();
            this.GroupBox45.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox44)).EndInit();
            this.TabPage10.ResumeLayout(false);
            this.GroupBox70.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox69)).EndInit();
            this.GroupBox71.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox70)).EndInit();
            this.GroupBox72.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox71)).EndInit();
            this.GroupBox73.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox72)).EndInit();
            this.GroupBox74.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox73)).EndInit();
            this.GroupBox75.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox74)).EndInit();
            this.TabPage12.ResumeLayout(false);
            this.GroupBox81.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox80)).EndInit();
            this.GroupBox82.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox81)).EndInit();
            this.GroupBox83.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox82)).EndInit();
            this.GroupBox84.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox83)).EndInit();
            this.TabPage9.ResumeLayout(false);
            this.GroupBox68.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox67)).EndInit();
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checker_img)).EndInit();
            this.ResumeLayout(false);

        }

        internal OpenFileDialog OpenFileDialog2;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.GroupBox GroupBox12;
        internal UI.WP.Button Button9;
        internal Label Label12;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button12;
        internal UI.WP.Toggle SoundsEnabled;
        internal PictureBox checker_img;
        internal UI.WP.TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button8;
        internal UI.WP.GroupBox GroupBox1;
        internal UI.WP.TextBox TextBox1;
        internal Label Label1;
        internal PictureBox PictureBox1;
        internal UI.WP.Button Button1;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal UI.WP.TextBox TextBox5;
        internal Label Label3;
        internal PictureBox PictureBox3;
        internal UI.WP.GroupBox GroupBox4;
        internal UI.WP.Button Button13;
        internal UI.WP.Button Button14;
        internal UI.WP.TextBox TextBox4;
        internal Label Label4;
        internal PictureBox PictureBox4;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button4;
        internal UI.WP.TextBox TextBox3;
        internal Label Label2;
        internal PictureBox PictureBox2;
        internal UI.WP.Button Button2;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
        internal UI.WP.Button Button15;
        internal UI.WP.Button Button16;
        internal UI.WP.Button Button17;
        internal UI.WP.Button Button18;
        internal UI.WP.GroupBox GroupBox5;
        internal UI.WP.Button Button19;
        internal UI.WP.Button Button20;
        internal UI.WP.Button Button21;
        internal UI.WP.TextBox TextBox2;
        internal Label Label5;
        internal PictureBox PictureBox5;
        internal UI.WP.Button Button23;
        internal UI.WP.Button Button22;
        internal UI.WP.Button Button24;
        internal UI.WP.GroupBox GroupBox6;
        internal UI.WP.Button Button25;
        internal UI.WP.Button Button26;
        internal UI.WP.Button Button27;
        internal UI.WP.TextBox TextBox6;
        internal Label Label6;
        internal PictureBox PictureBox6;
        internal UI.WP.GroupBox GroupBox11;
        internal UI.WP.Button Button40;
        internal UI.WP.Button Button41;
        internal UI.WP.Button Button42;
        internal UI.WP.TextBox TextBox11;
        internal Label Label11;
        internal PictureBox PictureBox11;
        internal UI.WP.GroupBox GroupBox10;
        internal UI.WP.Button Button37;
        internal UI.WP.Button Button38;
        internal UI.WP.Button Button39;
        internal UI.WP.TextBox TextBox10;
        internal Label Label10;
        internal PictureBox PictureBox10;
        internal UI.WP.GroupBox GroupBox9;
        internal UI.WP.Button Button34;
        internal UI.WP.Button Button35;
        internal UI.WP.Button Button36;
        internal UI.WP.TextBox TextBox9;
        internal Label Label9;
        internal PictureBox PictureBox9;
        internal UI.WP.GroupBox GroupBox8;
        internal UI.WP.Button Button31;
        internal UI.WP.Button Button32;
        internal UI.WP.Button Button33;
        internal UI.WP.TextBox TextBox8;
        internal Label Label8;
        internal PictureBox PictureBox8;
        internal UI.WP.GroupBox GroupBox7;
        internal UI.WP.Button Button28;
        internal UI.WP.Button Button29;
        internal UI.WP.Button Button30;
        internal UI.WP.TextBox TextBox7;
        internal Label Label7;
        internal PictureBox PictureBox7;
        internal TabPage TabPage3;
        internal UI.WP.GroupBox GroupBox18;
        internal UI.WP.Button Button58;
        internal UI.WP.Button Button59;
        internal UI.WP.Button Button60;
        internal UI.WP.TextBox TextBox17;
        internal Label Label18;
        internal PictureBox PictureBox17;
        internal UI.WP.GroupBox GroupBox13;
        internal UI.WP.Button Button43;
        internal UI.WP.Button Button44;
        internal UI.WP.Button Button45;
        internal UI.WP.TextBox TextBox12;
        internal Label Label13;
        internal PictureBox PictureBox12;
        internal UI.WP.GroupBox GroupBox14;
        internal UI.WP.Button Button46;
        internal UI.WP.Button Button47;
        internal UI.WP.Button Button48;
        internal UI.WP.TextBox TextBox13;
        internal Label Label14;
        internal PictureBox PictureBox13;
        internal UI.WP.GroupBox GroupBox15;
        internal UI.WP.Button Button49;
        internal UI.WP.Button Button50;
        internal UI.WP.Button Button51;
        internal UI.WP.TextBox TextBox14;
        internal Label Label15;
        internal PictureBox PictureBox14;
        internal UI.WP.GroupBox GroupBox16;
        internal UI.WP.Button Button52;
        internal UI.WP.Button Button53;
        internal UI.WP.Button Button54;
        internal UI.WP.TextBox TextBox15;
        internal Label Label16;
        internal PictureBox PictureBox15;
        internal UI.WP.GroupBox GroupBox17;
        internal UI.WP.Button Button55;
        internal UI.WP.Button Button56;
        internal UI.WP.Button Button57;
        internal UI.WP.TextBox TextBox16;
        internal Label Label17;
        internal PictureBox PictureBox16;
        internal TabPage TabPage4;
        internal UI.WP.GroupBox GroupBox25;
        internal UI.WP.Button Button79;
        internal UI.WP.Button Button80;
        internal UI.WP.Button Button81;
        internal UI.WP.TextBox TextBox24;
        internal Label Label25;
        internal PictureBox PictureBox24;
        internal UI.WP.GroupBox GroupBox19;
        internal UI.WP.Button Button61;
        internal UI.WP.Button Button62;
        internal UI.WP.Button Button63;
        internal UI.WP.TextBox TextBox18;
        internal Label Label19;
        internal PictureBox PictureBox18;
        internal UI.WP.GroupBox GroupBox20;
        internal UI.WP.Button Button64;
        internal UI.WP.Button Button65;
        internal UI.WP.Button Button66;
        internal UI.WP.TextBox TextBox19;
        internal Label Label20;
        internal PictureBox PictureBox19;
        internal UI.WP.GroupBox GroupBox21;
        internal UI.WP.Button Button67;
        internal UI.WP.Button Button68;
        internal UI.WP.Button Button69;
        internal UI.WP.TextBox TextBox20;
        internal Label Label21;
        internal PictureBox PictureBox20;
        internal UI.WP.GroupBox GroupBox22;
        internal UI.WP.Button Button70;
        internal UI.WP.Button Button71;
        internal UI.WP.Button Button72;
        internal UI.WP.TextBox TextBox21;
        internal Label Label22;
        internal PictureBox PictureBox21;
        internal UI.WP.GroupBox GroupBox23;
        internal UI.WP.Button Button73;
        internal UI.WP.Button Button74;
        internal UI.WP.Button Button75;
        internal UI.WP.TextBox TextBox22;
        internal Label Label23;
        internal PictureBox PictureBox22;
        internal UI.WP.GroupBox GroupBox24;
        internal UI.WP.Button Button76;
        internal UI.WP.Button Button77;
        internal UI.WP.Button Button78;
        internal UI.WP.TextBox TextBox23;
        internal Label Label24;
        internal PictureBox PictureBox23;
        internal TabPage TabPage5;
        internal UI.WP.GroupBox GroupBox33;
        internal UI.WP.Button Button103;
        internal UI.WP.Button Button104;
        internal UI.WP.Button Button105;
        internal UI.WP.TextBox TextBox32;
        internal Label Label33;
        internal PictureBox PictureBox32;
        internal UI.WP.GroupBox GroupBox34;
        internal UI.WP.Button Button106;
        internal UI.WP.Button Button107;
        internal UI.WP.Button Button108;
        internal UI.WP.TextBox TextBox33;
        internal Label Label34;
        internal PictureBox PictureBox33;
        internal UI.WP.GroupBox GroupBox35;
        internal UI.WP.Button Button109;
        internal UI.WP.Button Button110;
        internal UI.WP.Button Button111;
        internal UI.WP.TextBox TextBox34;
        internal Label Label35;
        internal PictureBox PictureBox34;
        internal UI.WP.GroupBox GroupBox26;
        internal UI.WP.Button Button82;
        internal UI.WP.Button Button83;
        internal UI.WP.Button Button84;
        internal UI.WP.TextBox TextBox25;
        internal Label Label26;
        internal PictureBox PictureBox25;
        internal UI.WP.GroupBox GroupBox27;
        internal UI.WP.Button Button85;
        internal UI.WP.Button Button86;
        internal UI.WP.Button Button87;
        internal UI.WP.TextBox TextBox26;
        internal Label Label27;
        internal PictureBox PictureBox26;
        internal UI.WP.GroupBox GroupBox28;
        internal UI.WP.Button Button88;
        internal UI.WP.Button Button89;
        internal UI.WP.Button Button90;
        internal UI.WP.TextBox TextBox27;
        internal Label Label28;
        internal PictureBox PictureBox27;
        internal UI.WP.GroupBox GroupBox29;
        internal UI.WP.Button Button91;
        internal UI.WP.Button Button92;
        internal UI.WP.Button Button93;
        internal UI.WP.TextBox TextBox28;
        internal Label Label29;
        internal PictureBox PictureBox28;
        internal UI.WP.GroupBox GroupBox30;
        internal UI.WP.Button Button94;
        internal UI.WP.Button Button95;
        internal UI.WP.Button Button96;
        internal UI.WP.TextBox TextBox29;
        internal Label Label30;
        internal PictureBox PictureBox29;
        internal UI.WP.GroupBox GroupBox31;
        internal UI.WP.Button Button97;
        internal UI.WP.Button Button98;
        internal UI.WP.Button Button99;
        internal UI.WP.TextBox TextBox30;
        internal Label Label31;
        internal PictureBox PictureBox30;
        internal UI.WP.GroupBox GroupBox32;
        internal UI.WP.Button Button100;
        internal UI.WP.Button Button101;
        internal UI.WP.Button Button102;
        internal UI.WP.TextBox TextBox31;
        internal Label Label32;
        internal PictureBox PictureBox31;
        internal TabPage TabPage6;
        internal UI.WP.GroupBox GroupBox36;
        internal UI.WP.Button Button112;
        internal UI.WP.Button Button113;
        internal UI.WP.Button Button114;
        internal UI.WP.TextBox TextBox35;
        internal Label Label36;
        internal PictureBox PictureBox35;
        internal UI.WP.GroupBox GroupBox37;
        internal UI.WP.Button Button115;
        internal UI.WP.Button Button116;
        internal UI.WP.Button Button117;
        internal UI.WP.TextBox TextBox36;
        internal Label Label37;
        internal PictureBox PictureBox36;
        internal UI.WP.GroupBox GroupBox38;
        internal UI.WP.Button Button118;
        internal UI.WP.Button Button119;
        internal UI.WP.Button Button120;
        internal UI.WP.TextBox TextBox37;
        internal Label Label38;
        internal PictureBox PictureBox37;
        internal UI.WP.GroupBox GroupBox39;
        internal UI.WP.Button Button121;
        internal UI.WP.Button Button122;
        internal UI.WP.Button Button123;
        internal UI.WP.TextBox TextBox38;
        internal Label Label39;
        internal PictureBox PictureBox38;
        internal UI.WP.GroupBox GroupBox40;
        internal UI.WP.Button Button124;
        internal UI.WP.Button Button125;
        internal UI.WP.Button Button126;
        internal UI.WP.TextBox TextBox39;
        internal Label Label40;
        internal PictureBox PictureBox39;
        internal UI.WP.GroupBox GroupBox41;
        internal UI.WP.Button Button127;
        internal UI.WP.Button Button128;
        internal UI.WP.Button Button129;
        internal UI.WP.TextBox TextBox40;
        internal Label Label41;
        internal PictureBox PictureBox40;
        internal UI.WP.GroupBox GroupBox42;
        internal UI.WP.Button Button130;
        internal UI.WP.Button Button131;
        internal UI.WP.Button Button132;
        internal UI.WP.TextBox TextBox41;
        internal Label Label42;
        internal PictureBox PictureBox41;
        internal UI.WP.GroupBox GroupBox43;
        internal UI.WP.Button Button133;
        internal UI.WP.Button Button134;
        internal UI.WP.Button Button135;
        internal UI.WP.TextBox TextBox42;
        internal Label Label43;
        internal PictureBox PictureBox42;
        internal UI.WP.GroupBox GroupBox44;
        internal UI.WP.Button Button136;
        internal UI.WP.Button Button137;
        internal UI.WP.Button Button138;
        internal UI.WP.TextBox TextBox43;
        internal Label Label44;
        internal PictureBox PictureBox43;
        internal UI.WP.GroupBox GroupBox45;
        internal UI.WP.Button Button139;
        internal UI.WP.Button Button140;
        internal UI.WP.Button Button141;
        internal UI.WP.TextBox TextBox44;
        internal Label Label45;
        internal PictureBox PictureBox44;
        internal TabPage TabPage7;
        internal UI.WP.GroupBox GroupBox48;
        internal UI.WP.Button Button148;
        internal UI.WP.Button Button149;
        internal UI.WP.Button Button150;
        internal UI.WP.TextBox TextBox47;
        internal Label Label48;
        internal PictureBox PictureBox47;
        internal UI.WP.GroupBox GroupBox47;
        internal UI.WP.Button Button145;
        internal UI.WP.Button Button146;
        internal UI.WP.Button Button147;
        internal UI.WP.TextBox TextBox46;
        internal Label Label47;
        internal PictureBox PictureBox46;
        internal UI.WP.GroupBox GroupBox46;
        internal UI.WP.Button Button142;
        internal UI.WP.Button Button143;
        internal UI.WP.Button Button144;
        internal UI.WP.TextBox TextBox45;
        internal Label Label46;
        internal PictureBox PictureBox45;
        internal UI.WP.GroupBox GroupBox53;
        internal UI.WP.Button Button163;
        internal UI.WP.Button Button164;
        internal UI.WP.Button Button165;
        internal UI.WP.TextBox TextBox52;
        internal Label Label53;
        internal PictureBox PictureBox52;
        internal UI.WP.GroupBox GroupBox52;
        internal UI.WP.Button Button160;
        internal UI.WP.Button Button161;
        internal UI.WP.Button Button162;
        internal UI.WP.TextBox TextBox51;
        internal Label Label52;
        internal PictureBox PictureBox51;
        internal UI.WP.GroupBox GroupBox51;
        internal UI.WP.Button Button157;
        internal UI.WP.Button Button158;
        internal UI.WP.Button Button159;
        internal UI.WP.TextBox TextBox50;
        internal Label Label51;
        internal PictureBox PictureBox50;
        internal UI.WP.GroupBox GroupBox50;
        internal UI.WP.Button Button154;
        internal UI.WP.Button Button155;
        internal UI.WP.Button Button156;
        internal UI.WP.TextBox TextBox49;
        internal Label Label50;
        internal PictureBox PictureBox49;
        internal UI.WP.GroupBox GroupBox49;
        internal UI.WP.Button Button151;
        internal UI.WP.Button Button152;
        internal UI.WP.Button Button153;
        internal UI.WP.TextBox TextBox48;
        internal Label Label49;
        internal PictureBox PictureBox48;
        internal UI.WP.GroupBox GroupBox55;
        internal UI.WP.Button Button169;
        internal UI.WP.Button Button170;
        internal UI.WP.Button Button171;
        internal UI.WP.TextBox TextBox54;
        internal Label Label55;
        internal PictureBox PictureBox54;
        internal UI.WP.SeparatorH Separator2;
        internal UI.WP.GroupBox GroupBox54;
        internal UI.WP.Button Button166;
        internal UI.WP.Button Button167;
        internal UI.WP.Button Button168;
        internal UI.WP.TextBox TextBox53;
        internal Label Label54;
        internal PictureBox PictureBox53;
        internal UI.WP.GroupBox GroupBox56;
        internal UI.WP.Button Button172;
        internal UI.WP.Button Button173;
        internal UI.WP.Button Button174;
        internal UI.WP.TextBox TextBox55;
        internal Label Label56;
        internal PictureBox PictureBox55;
        internal TabPage TabPage8;
        internal UI.WP.GroupBox GroupBox58;
        internal UI.WP.Button Button178;
        internal UI.WP.Button Button179;
        internal UI.WP.Button Button180;
        internal UI.WP.TextBox TextBox57;
        internal Label Label58;
        internal PictureBox PictureBox57;
        internal UI.WP.GroupBox GroupBox59;
        internal UI.WP.Button Button181;
        internal UI.WP.Button Button182;
        internal UI.WP.Button Button183;
        internal UI.WP.TextBox TextBox58;
        internal Label Label59;
        internal PictureBox PictureBox58;
        internal UI.WP.GroupBox GroupBox60;
        internal UI.WP.Button Button184;
        internal UI.WP.Button Button185;
        internal UI.WP.Button Button186;
        internal UI.WP.TextBox TextBox59;
        internal Label Label60;
        internal PictureBox PictureBox59;
        internal UI.WP.GroupBox GroupBox61;
        internal UI.WP.Button Button187;
        internal UI.WP.Button Button188;
        internal UI.WP.Button Button189;
        internal UI.WP.TextBox TextBox60;
        internal Label Label61;
        internal PictureBox PictureBox60;
        internal UI.WP.GroupBox GroupBox62;
        internal UI.WP.Button Button190;
        internal UI.WP.Button Button191;
        internal UI.WP.Button Button192;
        internal UI.WP.TextBox TextBox61;
        internal Label Label62;
        internal PictureBox PictureBox61;
        internal UI.WP.GroupBox GroupBox63;
        internal UI.WP.Button Button193;
        internal UI.WP.Button Button194;
        internal UI.WP.Button Button195;
        internal UI.WP.TextBox TextBox62;
        internal Label Label63;
        internal PictureBox PictureBox62;
        internal UI.WP.GroupBox GroupBox57;
        internal UI.WP.Button Button175;
        internal UI.WP.Button Button176;
        internal UI.WP.Button Button177;
        internal UI.WP.TextBox TextBox56;
        internal Label Label57;
        internal PictureBox PictureBox56;
        internal UI.WP.GroupBox GroupBox65;
        internal UI.WP.Button Button199;
        internal UI.WP.Button Button200;
        internal UI.WP.Button Button201;
        internal UI.WP.TextBox TextBox64;
        internal Label Label65;
        internal PictureBox PictureBox64;
        internal UI.WP.GroupBox GroupBox64;
        internal UI.WP.Button Button196;
        internal UI.WP.Button Button197;
        internal UI.WP.Button Button198;
        internal UI.WP.TextBox TextBox63;
        internal Label Label64;
        internal PictureBox PictureBox63;
        internal TabPage TabPage9;
        internal UI.WP.GroupBox GroupBox66;
        internal UI.WP.Button Button202;
        internal UI.WP.Button Button203;
        internal UI.WP.Button Button204;
        internal UI.WP.TextBox TextBox65;
        internal Label Label66;
        internal PictureBox PictureBox65;
        internal UI.WP.GroupBox GroupBox67;
        internal UI.WP.Button Button205;
        internal UI.WP.Button Button206;
        internal UI.WP.Button Button207;
        internal UI.WP.TextBox TextBox66;
        internal Label Label67;
        internal PictureBox PictureBox66;
        internal UI.WP.GroupBox GroupBox68;
        internal UI.WP.Button Button208;
        internal UI.WP.Button Button209;
        internal UI.WP.Button Button210;
        internal UI.WP.TextBox TextBox67;
        internal Label Label68;
        internal PictureBox PictureBox67;
        internal UI.WP.GroupBox GroupBox69;
        internal UI.WP.Button Button211;
        internal UI.WP.Button Button212;
        internal UI.WP.Button Button213;
        internal UI.WP.TextBox TextBox68;
        internal Label Label69;
        internal PictureBox PictureBox68;
        internal TabPage TabPage10;
        internal UI.WP.GroupBox GroupBox70;
        internal UI.WP.Button Button214;
        internal UI.WP.Button Button215;
        internal UI.WP.Button Button216;
        internal UI.WP.TextBox TextBox69;
        internal Label Label70;
        internal PictureBox PictureBox69;
        internal UI.WP.GroupBox GroupBox71;
        internal UI.WP.Button Button217;
        internal UI.WP.Button Button218;
        internal UI.WP.Button Button219;
        internal UI.WP.TextBox TextBox70;
        internal Label Label71;
        internal PictureBox PictureBox70;
        internal UI.WP.GroupBox GroupBox72;
        internal UI.WP.Button Button220;
        internal UI.WP.Button Button221;
        internal UI.WP.Button Button222;
        internal UI.WP.TextBox TextBox71;
        internal Label Label72;
        internal PictureBox PictureBox71;
        internal UI.WP.GroupBox GroupBox73;
        internal UI.WP.Button Button223;
        internal UI.WP.Button Button224;
        internal UI.WP.Button Button225;
        internal UI.WP.TextBox TextBox72;
        internal Label Label73;
        internal PictureBox PictureBox72;
        internal UI.WP.GroupBox GroupBox74;
        internal UI.WP.Button Button226;
        internal UI.WP.Button Button227;
        internal UI.WP.Button Button228;
        internal UI.WP.TextBox TextBox73;
        internal Label Label74;
        internal PictureBox PictureBox73;
        internal UI.WP.GroupBox GroupBox75;
        internal UI.WP.Button Button229;
        internal UI.WP.Button Button230;
        internal UI.WP.Button Button231;
        internal UI.WP.TextBox TextBox74;
        internal Label Label75;
        internal PictureBox PictureBox74;
        internal UI.WP.GroupBox GroupBox76;
        internal UI.WP.Button Button232;
        internal UI.WP.Button Button233;
        internal UI.WP.Button Button234;
        internal UI.WP.TextBox TextBox75;
        internal Label Label76;
        internal PictureBox PictureBox75;
        internal TabPage TabPage11;
        internal UI.WP.GroupBox GroupBox79;
        internal UI.WP.Button Button241;
        internal UI.WP.Button Button242;
        internal UI.WP.Button Button243;
        internal UI.WP.TextBox TextBox78;
        internal Label Label79;
        internal PictureBox PictureBox78;
        internal UI.WP.GroupBox GroupBox80;
        internal UI.WP.Button Button244;
        internal UI.WP.Button Button245;
        internal UI.WP.Button Button246;
        internal UI.WP.TextBox TextBox79;
        internal Label Label80;
        internal PictureBox PictureBox79;
        internal UI.WP.GroupBox GroupBox78;
        internal UI.WP.Button Button238;
        internal UI.WP.Button Button239;
        internal UI.WP.Button Button240;
        internal UI.WP.TextBox TextBox77;
        internal Label Label78;
        internal PictureBox PictureBox77;
        internal UI.WP.GroupBox GroupBox77;
        internal UI.WP.Button Button235;
        internal UI.WP.Button Button236;
        internal UI.WP.Button Button237;
        internal UI.WP.TextBox TextBox76;
        internal Label Label77;
        internal PictureBox PictureBox76;
        internal TabPage TabPage12;
        internal UI.WP.GroupBox GroupBox81;
        internal UI.WP.Button Button247;
        internal UI.WP.Button Button248;
        internal UI.WP.Button Button249;
        internal UI.WP.TextBox TextBox80;
        internal Label Label81;
        internal PictureBox PictureBox80;
        internal UI.WP.GroupBox GroupBox82;
        internal UI.WP.Button Button250;
        internal UI.WP.Button Button251;
        internal UI.WP.Button Button252;
        internal UI.WP.TextBox TextBox81;
        internal Label Label82;
        internal PictureBox PictureBox81;
        internal UI.WP.GroupBox GroupBox83;
        internal UI.WP.Button Button253;
        internal UI.WP.Button Button254;
        internal UI.WP.Button Button255;
        internal UI.WP.TextBox TextBox82;
        internal Label Label83;
        internal PictureBox PictureBox82;
        internal UI.WP.GroupBox GroupBox84;
        internal UI.WP.Button Button256;
        internal UI.WP.Button Button257;
        internal UI.WP.Button Button258;
        internal UI.WP.TextBox TextBox83;
        internal Label Label84;
        internal PictureBox PictureBox83;
        internal UI.WP.AlertBox AlertBox3;
        internal UI.WP.Button Button259;
        internal OpenFileDialog OpenThemeDialog;
        internal UI.WP.GroupBox GroupBox85;
        internal Label Label85;
        internal PictureBox PictureBox84;
        internal UI.WP.CheckBox CheckBox35_SFC;
        internal UI.WP.SeparatorH Separator3;
        internal UI.WP.SeparatorH Separator1;
        internal UI.WP.GroupBox GroupBox86;
        internal UI.WP.Button Button260;
        internal UI.WP.Button Button261;
        internal UI.WP.Button Button262;
        internal UI.WP.TextBox TextBox84;
        internal Label Label86;
        internal PictureBox PictureBox85;
        internal UI.WP.GroupBox groupBox87;
        internal UI.WP.Button button263;
        internal UI.WP.Button button264;
        internal UI.WP.Button button265;
        internal UI.WP.TextBox textBox85;
        internal Label label89;
        internal PictureBox pictureBox86;
        internal UI.WP.GroupBox groupBox88;
        internal UI.WP.Button button266;
        internal UI.WP.Button button267;
        internal UI.WP.Button button268;
        internal UI.WP.TextBox textBox86;
        internal Label label87;
        internal PictureBox pictureBox87;
        internal PictureBox pictureBox92;
        internal PictureBox pictureBox90;
        internal PictureBox pictureBox91;
        internal PictureBox pictureBox89;
        internal PictureBox pictureBox88;
        internal PictureBox pictureBox94;
        internal PictureBox pictureBox93;
        internal UI.WP.AlertBox alertBox1;
    }
}